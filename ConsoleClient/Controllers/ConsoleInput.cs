namespace ConsoleClient.Controllers;

public static class ConsoleInput
{
    private const string PhoneChars = "+-() ";

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

            ConsoleOutput.Error("Unos ne sme biti prazan.");
        }
    }

    public static int ReadInt(string prompt, int min = 0, int? max = null)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            if (int.TryParse(Console.ReadLine()?.Trim(), out int value) && value >= min && (max is null || value <= max))
            {
                return value;
            }

            ConsoleOutput.Error(max is null
                ? $"Unesite ceo broj najmanje {min}."
                : $"Unesite broj od {min} do {max}.");
        }
    }

    public static DateOnly ReadDate(string prompt, DateOnly? min = null, DateOnly? max = null)
    {
        while (true)
        {
            Console.Write($"{prompt} (gggg-mm-dd): ");
            if (DateOnly.TryParseExact(Console.ReadLine()?.Trim(), "yyyy-MM-dd", out DateOnly value))
            {
                if (min is not null && value < min)
                {
                    ConsoleOutput.Error($"Datum ne sme biti pre {min.Value:yyyy-MM-dd}.");
                    continue;
                }

                if (max is not null && value > max)
                {
                    ConsoleOutput.Error("Datum ne sme biti u buducnosti.");
                    continue;
                }

                return value;
            }

            ConsoleOutput.Error("Unesite datum u formatu gggg-mm-dd.");
        }
    }

    public static string ReadEmail(string prompt)
    {
        while (true)
        {
            string value = ReadText(prompt);
            if (value.Contains('@'))
            {
                return value;
            }

            ConsoleOutput.Error("Email mora sadrzati znak @.");
        }
    }

    public static string ReadPhone(string prompt)
    {
        while (true)
        {
            string value = ReadText(prompt);
            if (value.All(c => char.IsAsciiDigit(c) || PhoneChars.Contains(c)))
            {
                return value;
            }

            ConsoleOutput.Error("Telefon moze sadrzati samo cifre i znakove + - ( ).");
        }
    }

    public static string ReadUniqueText(string prompt, IEnumerable<string> taken)
    {
        List<string> takenList = taken.ToList();
        while (true)
        {
            string value = ReadText(prompt);
            if (!takenList.Contains(value))
            {
                return value;
            }

            ConsoleOutput.Error("Vec postoji entitet sa unetom vrednoscu.");
        }
    }

    public static int ReadExistingId(string prompt, Func<int, bool> exists)
    {
        while (true)
        {
            int id = ReadInt(prompt);
            if (exists(id))
            {
                return id;
            }

            ConsoleOutput.Error($"Entitet sa Id {id} ne postoji.");
        }
    }

    public static List<int> ReadExistingIds(string prompt, Func<int, bool> exists)
    {
        while (true)
        {
            List<int> ids = ReadIds(prompt);
            if (ids.All(exists))
            {
                return ids;
            }

            foreach (int id in ids.Where(id => !exists(id)))
            {
                ConsoleOutput.Error($"Entitet sa Id {id} ne postoji.");
            }
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
