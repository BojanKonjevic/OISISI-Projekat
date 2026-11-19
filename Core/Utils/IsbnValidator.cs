using System.Text;

namespace Core.Utils;

public static class IsbnValidator
{
    public static bool TryNormalize(string input, out string isbn)
    {
        isbn = string.Empty;
        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        StringBuilder builder = new(input.Length);
        foreach (char character in input)
        {
            if (character == '-' || char.IsWhiteSpace(character))
            {
                continue;
            }

            if (char.IsAsciiDigit(character))
            {
                builder.Append(character);
                continue;
            }

            if (character is 'x' or 'X')
            {
                builder.Append('X');
                continue;
            }

            return false;
        }

        string candidate = builder.ToString();
        bool valid = candidate.Length switch
        {
            10 => IsValidIsbn10(candidate),
            13 => IsValidIsbn13(candidate),
            _ => false
        };

        if (!valid)
        {
            return false;
        }

        isbn = candidate;
        return true;
    }

    private static bool IsValidIsbn10(string value)
    {
        int sum = 0;
        for (int i = 0; i < value.Length; i++)
        {
            char character = value[i];
            int digit;
            if (i == 9 && character == 'X')
            {
                digit = 10;
            }
            else if (char.IsAsciiDigit(character))
            {
                digit = character - '0';
            }
            else
            {
                return false;
            }

            sum += (10 - i) * digit;
        }

        return sum % 11 == 0;
    }

    private static bool IsValidIsbn13(string value)
    {
        if (!value.StartsWith("978", StringComparison.Ordinal) &&
            !value.StartsWith("979", StringComparison.Ordinal))
        {
            return false;
        }

        int sum = 0;
        for (int i = 0; i < value.Length; i++)
        {
            char character = value[i];
            if (!char.IsAsciiDigit(character))
            {
                return false;
            }

            int digit = character - '0';
            sum += (i % 2 == 0 ? 1 : 3) * digit;
        }

        return sum % 10 == 0;
    }
}
