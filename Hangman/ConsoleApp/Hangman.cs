
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

internal partial class Hangman(string name, string secretWord, string guessWord, int score = 0, List<string>? wrongGuesses = null)
{
    internal string Name { get; } = name;
    internal string SecretWord { get; } = secretWord;
    internal string GuessWord { get; set; } = guessWord;
    internal int Score { get; set; } = score;
    internal List<string> WrongGuesses { get; set; } = wrongGuesses ?? [];

    internal static Hangman CreateHangman(bool option, List<Hangman> array)
    {
        if (option)
        {
            string name = GetDataFromConsole("NOTE: If a player with the same name exists when saving, it will override it\nEnter your name: ", (name) => name.Length > 0);
            (string secret_word, string guess_word) = GenerateRandomWord();
            return new Hangman(name, secret_word, guess_word);
        }

        int max = array.Count;
        if (max == 0)
        {
            Console.WriteLine("Saved Game is empty, create a new user");
            return CreateHangman(true, array);
        }
        for (int i = 0; i < max; i++)
        {
            System.Console.WriteLine($"{i + 1}: {array[i].Name}");
        }
        int num = GetDataFromConsole($"Choose a user [1 - {max}]: ", (n) => n >= 1 && n <= max);
        // new Hangman()
        return array[num - 1];
    }

    internal static Hangman Restart(Hangman hangman, bool condition)
    {
        (string secret_word, string guess_word) = GenerateRandomWord();
        Hangman hangman1 = new(hangman.Name, secret_word, guess_word)
        {
            Score = condition ? hangman.Score + 1 : hangman.Score
        };
        return hangman1;
    }

    const string fileName = "saved_game.json";
    internal static List<Hangman> GetDataFromFile()
    {
        if (!File.Exists(fileName)) File.WriteAllText(fileName, "[]");
        string text = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<IEnumerable<Hangman>>(text)?.ToList() ?? [];
    }

    internal void GetUserInput()
    {
        string answer = GetDataFromConsole("Enter a letter or the full words or 'save' to save progress (e.g 'l' or 'man'): ", (data) => data.Length > 0 && MyRegex().IsMatch(data));
        if (answer.Equals("save", StringComparison.OrdinalIgnoreCase)) Save();
        else if (answer.Length == 1) UpdateChar(answer[0]);
        else UpdateWord(answer);
    }

    private void UpdateWord(string answer)
    {
        if (SecretWord.Equals(answer, StringComparison.OrdinalIgnoreCase))
            GuessWord = answer;
        else
            WrongGuesses.Add(answer);
    }

    void UpdateChar(char data)
    {
        StringBuilder builder = new(GuessWord);
        for (int i = 0; i < SecretWord.Length; i++)
        {
            if (SecretWord[i] != data) continue;
            builder[i] = data;
        }
        bool notUpdated = GuessWord.Equals(builder.ToString(), StringComparison.OrdinalIgnoreCase);

        if (!notUpdated) GuessWord = builder.ToString();
        else if (!WrongGuesses.Contains(data.ToString())) WrongGuesses.Add(data.ToString());
    }

    private void Save()
    {
        List<Hangman> hangmen = GetDataFromFile();
        bool isUpdated = false;
        for (int i = 0; i < hangmen.Count; i++)
        {
            var item = hangmen[i];
            if (item.Name == Name)
            {
                hangmen[i] = item;
                isUpdated = true;
                break;
            }
        }
        if (!isUpdated) hangmen.Add(this);

        string text = JsonSerializer.Serialize(hangmen);
        File.WriteAllText(fileName, text);
        System.Console.WriteLine("-------------------------------------------------------------");
        System.Console.WriteLine("SAVE SUCCESSFUL");
    }

    private static (string, string) GenerateRandomWord()
    {
        var lines = (File.ReadAllLines("google-10000-english-no-swears.txt").Where(v => v.Length >= 5 && v.Length <= 12) ?? []).ToList();
        var secretWord = lines[new Random().Next(lines.Count - 1)];
        var guessWord = MyRegex().Replace(secretWord, "_");
        return (secretWord, guessWord);
    }

    private static string GetDataFromConsole(string text, Func<string, bool> conditionFunc)
    {
        while (true)
        {
            Console.Write(text);
            string name = (Console.ReadLine() ?? "").Trim();
            bool condition = conditionFunc(name);
            if (condition) return name;
        }
    }
    private static int GetDataFromConsole(string text, Func<int, bool> conditionFunc)
    {
        while (true)
        {
            Console.Write(text);
            string t = (Console.ReadLine() ?? "").Trim();
            if (!int.TryParse(t, out int data)) continue;
            bool condition = conditionFunc(data);
            if (condition) return data;
        }
    }

    [GeneratedRegex(@"[A-Za-z]")]
    private static partial Regex MyRegex();
}
