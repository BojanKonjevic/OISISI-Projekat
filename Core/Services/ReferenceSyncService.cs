using Core.DAO;
using Core.Models;

namespace Core.Services;

public class ReferenceSyncService
{
    private readonly VisitorDao _visitorDao;
    private readonly PurchaseDao _purchaseDao;
    private readonly PublisherDao _publisherDao;
    private readonly AuthorDao _authorDao;
    private readonly BookDao _bookDao;

    public ReferenceSyncService(VisitorDao visitorDao, PurchaseDao purchaseDao, PublisherDao publisherDao, AuthorDao authorDao, BookDao bookDao)
    {
        _visitorDao = visitorDao;
        _purchaseDao = purchaseDao;
        _publisherDao = publisherDao;
        _authorDao = authorDao;
        _bookDao = bookDao;
    }

    public void SyncAll()
    {
        List<Purchase> purchases = _purchaseDao.GetAll().ToList();
        List<Visitor> visitors = _visitorDao.GetAll().ToList();
        List<Book> books = _bookDao.GetAll().ToList();
        List<Author> authors = _authorDao.GetAll().ToList();
        List<Publisher> publishers = _publisherDao.GetAll().ToList();

        foreach (Visitor visitor in visitors)
        {
            List<Purchase> visitorPurchases = purchases.Where(p => p.VisitorId == visitor.Id).ToList();
            visitor.PurchaseIds = visitorPurchases.Select(p => p.Id).ToList();
            visitor.AverageRating = visitorPurchases.Count == 0 ? 0 : visitorPurchases.Average(p => p.Rating);
            visitor.WishlistBookIds = visitor.WishlistBookIds.Where(id => books.Any(b => b.Id == id)).ToList();
            _visitorDao.Update(visitor);
        }

        foreach (Book book in books)
        {
            book.AuthorIds = book.AuthorIds
                .Where(id => authors.Any(a => a.Id == id))
                .Distinct()
                .ToList();
            book.BuyerVisitorIds = purchases
                .Where(p => p.BookId == book.Id)
                .Select(p => p.VisitorId)
                .Distinct()
                .ToList();
            book.WishlistVisitorIds = visitors
                .Where(v => v.WishlistBookIds.Contains(book.Id))
                .Select(v => v.Id)
                .ToList();
            _bookDao.Update(book);
        }

        foreach (Author author in authors)
        {
            author.BookIds = books
                .Where(b => b.AuthorIds.Contains(author.Id))
                .Select(b => b.Id)
                .ToList();
            _authorDao.Update(author);
        }

        foreach (Publisher publisher in publishers)
        {
            List<Book> publisherBooks = books
                .Where(b => b.PublisherId == publisher.Id)
                .ToList();

            publisher.BookIds = publisherBooks
                .Select(b => b.Id)
                .ToList();
            publisher.AuthorIds = publisherBooks
                .SelectMany(b => b.AuthorIds)
                .Distinct()
                .ToList();
            _publisherDao.Update(publisher);
        }
    }
}
