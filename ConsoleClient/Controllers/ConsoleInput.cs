namespace ConsoleClient.Controllers;

public static class ConsoleInput
{
    public static string ReadText(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;
            if (input.Length > 0)
            {
                return input;
            }

            Console.WriteLine("Unos ne sme biti prazan.");
        }
    }

    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            if (int.TryParse(Console.ReadLine()?.Trim(), out int value) && value >= 0)
            {
                return value;
            }

            Console.WriteLine("Unesite ceo nenegativan broj.");
        }
    }

    public static DateOnly ReadDate(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (gggg-mm-dd): ");
            if (DateOnly.TryParseExact(Console.ReadLine()?.Trim(), "yyyy-MM-dd", out DateOnly value))
            {
                return value;
            }

            Console.WriteLine("Unesite datum u formatu gggg-mm-dd.");
        }
    }

    public static List<int> ReadIds(string prompt)
    {
        Console.Write($"{prompt} (odvojite zarezima, prazno za preskakanje): ");
        string input = Console.ReadLine()?.Trim() ?? string.Empty;
        if (input.Length == 0)
        {
            return [];
        }

        List<int> ids = [];
        foreach (string part in input.Split(','))
        {
            if (int.TryParse(part.Trim(), out int id))
            {
                ids.Add(id);
            }
        }

        return ids;
    }
}
