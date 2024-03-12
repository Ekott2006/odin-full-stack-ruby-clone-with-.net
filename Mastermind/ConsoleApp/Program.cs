// ! Introduction
Console.WriteLine(TextHelpers.TextHeading("MASTERMIND GAME"));
System.Console.WriteLine("●●●●●●😢 \u4323");
Console.WriteLine(TextHelpers.TextHeading("About"));
Console.WriteLine("This is a 1-player game against the computer.You can choose to be the code maker or the code breaker.");

Console.WriteLine(TextHelpers.TextHeading("Instruction"));
Console.WriteLine("The code maker makes a secret color combination, and the code breaker tries to guess it.");
Console.WriteLine("After each guess, the code maker gives feedback about the correct colors and positions. ");
Console.WriteLine("The game goes on until the code breaker figures out the whole combination, using up to four clues marked with a check (●) for correct number and position and a circle (○) for correct number but wrong position.");

Console.WriteLine(TextHelpers.TextHeading("Examples:"));

// Six letters for master code
Console.WriteLine("There are six different number/color combinations:");
List<Color> colors = CodeHelper.Colors;
for (int i = 0; i < colors.Count; i++)
{
  Console.Write(TextHelpers.ColorizeText($"  {i + 1}  ", 30, 41 + i, true));
  Console.Write(" ");

}
Console.WriteLine("\nYou can also use their colors name");
for (int i = 0; i < colors.Count; i++)
{
  Console.Write(TextHelpers.ColorizeText(colors[i].ToString() + " ", 30, 31 + i, true));
}

Console.WriteLine("\n\nCODE MAKER");
// Four letters for master code
TextHelpers.PrintCodeToColor([Color.Red, Color.Green, Color.Red, Color.Purple], [0, 0], false);

Console.WriteLine("\n\nCODE BREAKER");
// Four letters and clue
TextHelpers.PrintCodeToColor([Color.Red, Color.Red, Color.Green, Color.Green], [1, 2]);
Console.WriteLine("\nFor the 'secret code' above. The guess had 1 correct number in the correct position and 2 correct numbers in a wrong position.");

while (true)
{
  //! Main Part
  Console.WriteLine(TextHelpers.TextHeading("Choose your role:"));
  Console.WriteLine("1: Codemaker (create a secret code)");
  Console.WriteLine("2: Codebreaker (try to guess the secret code)");
  // User input
  Console.Write("Press '1' or '2' or 'q' to quit (any other key is 2): ");
  string option = (Console.ReadLine() ?? "2").Trim();
  if (option.Equals("q", StringComparison.CurrentCultureIgnoreCase)) return;

  Console.Write("Enter number of maximum guesses or 'q' to quit (any other key is 12): ");
  string maxGuessesInput = (Console.ReadLine() ?? "12").Trim();
  if (maxGuessesInput.Equals("q", StringComparison.CurrentCultureIgnoreCase)) return;

  if (int.TryParse(maxGuessesInput, out int maxGuesses))
  {
    maxGuesses = maxGuesses < 1 ? 12 : maxGuesses;
  }
  else maxGuesses = 12;

  IGuess guess = CodeHelper.GuessGenerator(option);
  Dictionary<MastermindGuessType, int>? result = [];
  for (int i = 0; i < maxGuesses; i++)
  {
    List<Color> aGuess = guess.GetGuess();
    result = guess.HandleGuess(aGuess);

    TextHelpers.PrintCodeToColor(aGuess, result);
    Console.WriteLine("\n\n");

    if (result[MastermindGuessType.CorrectLocation] == 4) break;
  }
  guess.FinalResult(result[MastermindGuessType.CorrectLocation] == 4);

  //! Ending
  Console.Write("\nDo you want to play again? Press 'y' to continue or 'n' to quit (or any other key is 'y'): ");
  if ((Console.ReadLine() ?? "y").Trim().Equals("n", StringComparison.CurrentCultureIgnoreCase)) return;
}
