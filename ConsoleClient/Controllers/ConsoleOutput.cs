namespace ConsoleClient.Controllers;

public static class ConsoleOutput
{
    public static void Header(string text)
    {
        WriteLine(text, ConsoleColor.Cyan);
    }

    public static void Divider()
    {
        WriteLine("-------------------------", ConsoleColor.DarkGray);
    }

    public static void Success(string text)
    {
        WriteLine(text, ConsoleColor.Green);
    }

    public static void Error(string text)
    {
        WriteLine(text, ConsoleColor.Red);
    }

    private static void WriteLine(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}
