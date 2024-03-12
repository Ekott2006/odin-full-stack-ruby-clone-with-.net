using System.Text.RegularExpressions;

Console.WriteLine("HANGMAN");
Console.Write("Do you want to load a saved game? Press 'y' to continue or 'n' to quit (or any other key is 'y'): ");
string option = (Console.ReadLine() ?? "y").Trim();
Hangman hangman = Hangman.CreateHangman(option.Equals("n", StringComparison.CurrentCultureIgnoreCase), Hangman.GetDataFromFile());
while (true)
{
    while (!hangman.GuessWord.Equals(hangman.SecretWord) && hangman.WrongGuesses.Count < 12)
    {
        Printer.PrintResult(hangman);
        hangman.GetUserInput();
    }

    // Ending
    Printer.PrintEnd(hangman);
    System.Console.Write("\n Do you want to play again? Press 'y' to continue or 'n' to quit (or any other key is 'y'): ");
    option = (Console.ReadLine() ?? "y").Trim(); // I don't like redefining variable but no risk here
    if (option.Equals("n", StringComparison.CurrentCultureIgnoreCase)) return;
    hangman = Hangman.Restart(hangman, hangman.GuessWord.Equals(hangman.SecretWord));
}

partial class Program
{
    [GeneratedRegex(@"\s+")]
    private static partial Regex MyRegex();
}