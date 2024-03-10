List<string> list = ["below", "down", "go", "going", "horn", "how", "howdy", "it", "i", "low", "own", "part", "partner", "sit"];

var result = SubStrings.ValidSubStringsCounter("below", list);
PrintDictionary(result);
result = SubStrings.ValidSubStringsCounter("Howdy partner, sit down! How's it going?", list);
// PrintDictionary(result); // Uncomment this line

static void PrintDictionary(Dictionary<string, int> dict)
{
  Console.Write("{ ");
  bool first = true; // Flag to control comma placement

  foreach (KeyValuePair<string, int> item in dict)
  {
    if (!first)
    {
      Console.Write(", "); // Add comma after the first key-value pair
    }
    Console.Write($"\"{item.Key}\" => {item.Value}");
    first = false;
  }
  Console.WriteLine(" }");
}