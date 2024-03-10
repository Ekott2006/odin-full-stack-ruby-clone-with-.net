using System.Text.RegularExpressions;

internal static class SubStrings
{
  public static Dictionary<string, int> ValidSubStringsCounter(string word, List<string> validWordList)
  {
    Dictionary<string, int> dict = [];
    foreach (string item in validWordList)
    {
      foreach (var s in word.Split(" "))
      {
        if (!Regex.IsMatch(s, item, RegexOptions.IgnoreCase)) continue;

        if (!dict.ContainsKey(item)) dict[item] = 1;
        else dict[item] += 1;
      }
    }
    return dict;
  }
}
