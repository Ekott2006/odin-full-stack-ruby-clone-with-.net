internal static class Printer
{
    internal static void PrintResult(Hangman hangman)
    {
        System.Console.WriteLine("----------------------------------------------------------------------------------------------");
        System.Console.WriteLine($"Player name: {hangman.Name}");
        System.Console.WriteLine($"Score: {hangman.Score} \t\t\t\t Guesses left: {12 - hangman.WrongGuesses.Count}");
        System.Console.Write("Wrong Guesses: ");
        PrintList(hangman.WrongGuesses);
        System.Console.WriteLine();
        System.Console.WriteLine($"\n{hangman.GuessWord}\n\n");
    }

    internal static void PrintEnd(Hangman hangman)
    {
        System.Console.WriteLine("------------------------------------------------------------------------------------------------");
        System.Console.WriteLine();
        Printer.PrintList(hangman.WrongGuesses);
        System.Console.WriteLine("\n");

        if (hangman.GuessWord.Equals(hangman.SecretWord)) System.Console.WriteLine("You won!!!!");
        else System.Console.WriteLine("Try harder next time");
    }
    internal static void PrintList<T>(IEnumerable<T> list)
    {
        Console.Write("[ ");
        bool first = true;

        foreach (T item in list)
        {
            if (!first)
            {
                Console.Write(", ");
            }
            Console.Write(item);
            first = false;
        }
        Console.WriteLine(" ]");
    }
}
