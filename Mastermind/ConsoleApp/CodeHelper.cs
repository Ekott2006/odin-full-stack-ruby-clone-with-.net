internal class CodeHelper
{

    public static IGuess GuessGenerator(string optionInput)
    {
        // return 
        // TODO: Change Implementation
        if (int.TryParse(optionInput, out int option))
            option = option == 1 ? 1 : 2;
        else option = 2;

        return option == 1 ? new CodeMaker() : new CodeBreaker();
    }

    public static Dictionary<MastermindGuessType, int> EvaluateMastermindGuess(List<Color> guessList, List<Color> secretArray)
    {
        List<Color> guessListClone = [.. guessList];
        Dictionary<MastermindGuessType, int> result = new() {
            {MastermindGuessType.WrongLocation, 0},
            {MastermindGuessType.CorrectLocation, 0}
        };
        if (secretArray.Count != guessList.Count) throw new Exception("Error!!! Contact Developer");
        for (int i = 0; i < secretArray.Count; i++)
        {
            if (secretArray[i].Equals(guessList[i])) result[MastermindGuessType.CorrectLocation] += 1;
        }

        for (int i = 0; i < secretArray.Count; i++)
        {
            Color value = secretArray[i];
            int index = guessListClone.IndexOf(value);
            if (index == -1) continue;
            result[MastermindGuessType.WrongLocation] += 1;
            guessListClone.Remove(value);
        }
        result[MastermindGuessType.WrongLocation] -= result[MastermindGuessType.CorrectLocation];
        return result;
    }

    public static List<Color> GetInput()
    {
        List<Color>? masterCode = null;
        while (masterCode == null)
        {
            Console.Write("Enter a valid code (e.g., 1123 or 'b b y c' or 'blue red cyan green'): ");
            masterCode = TextHelpers.NormalizeUserInput((Console.ReadLine() ?? "").Trim());
        }
        return masterCode;
    }

    public static readonly List<Color> Colors = [.. ((Color[])Enum.GetValues(typeof(Color)))];
    private static readonly Random _random = new();

    public static List<Color> GetRandomColor()
    {
        List<Color> randomColor = [];
        for (int i = 0; i < 4; i++)
        {
            randomColor.Add(Colors[_random.Next(Colors.Count - 1)]);
        }
        return randomColor;
    }
}
