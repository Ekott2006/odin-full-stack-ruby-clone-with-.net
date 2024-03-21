
using System.Text;
using System.Text.RegularExpressions;

partial class TextHelpers
{
    public static readonly string BlackCircle = "\u25CB";
    public static readonly string BlackDot = "\u25CF";
    public static string ColorizeText(string text, int textColor, int backgroundColor, bool bold = false, bool underline = false)
    {

        StringBuilder style = new();
        if (bold) style.Append("1;");
        if (underline) style.Append("4;");
        string colorCode = $"{style}{textColor};{backgroundColor}";
        return $"\x1b[{colorCode}m{text}\x1b[0m";
    }

    public static void PrintCodeToColor(List<Color> list, Dictionary<MastermindGuessType, int> clues, 
     bool addClues = true) =>
        PrintCodeToColor(list, [clues[MastermindGuessType.CorrectLocation], clues[MastermindGuessType.WrongLocation]], addClues);

    public static void PrintCodeToColor(List<Color> list, List<int> clues, 
     bool addClues = true)
    {
        List<Color> colors = CodeHelper.Colors;
        list.ForEach(value =>
        {
            int i = colors.ToList().IndexOf(value);
            Console.Write(ColorizeText($"  {i + 1}  ", 30, 41 + i, true));
            Console.Write(" ");
        });
        if (addClues) Console.Write("Clues: ");
        for (int i = 0; i < clues[0]; i++)
        {
            Console.Write($" {BlackDot} ");
        }
        for (int i = 0; i < clues[1]; i++)
        {
            Console.Write($" {BlackCircle} ");
        }
    }
    public static string TextHeading(string text)
    {
        return $"\n{ColorizeText(text, 30, 37, true, true)}\n";
    }

    public static List<Color>? NormalizeUserInput(string text)
    {
        List<Color> colors = CodeHelper.Colors;
        string[]? listFromText = MyRegex().Replace(text, " ${0} ").Split(' ', StringSplitOptions.RemoveEmptyEntries);

        List<Color> result = [];

        foreach (string value in listFromText)
        {
            int fullColorIndex = colors.FindIndex(val => val.ToString().Equals(value, StringComparison.OrdinalIgnoreCase));
            int firstLetterIndex = colors.FindIndex(val => val.ToString()[0].ToString().Equals(value, StringComparison.OrdinalIgnoreCase));

            if (fullColorIndex != -1) result.Add(colors[fullColorIndex]);
            else if (firstLetterIndex != -1) result.Add(colors[firstLetterIndex]);
            else if (int.TryParse(value, out int i) && i <= 6 && i >= 1) result.Add(colors[i - 1]);
        }
        return result.Count == 4 ? result : null;
    }

    [GeneratedRegex(@"\d")]
    private static partial Regex MyRegex();
}