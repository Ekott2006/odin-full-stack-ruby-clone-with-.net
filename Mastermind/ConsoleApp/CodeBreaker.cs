class CodeBreaker : IGuess
{
    private List<List<Color>> _possibleCombination;
    private readonly List<Color> _masterCode;

    public CodeBreaker()
    {
        _masterCode = CodeHelper.GetInput();
        _possibleCombination = [];
        foreach (Color color1 in CodeHelper.Colors)
        {
            foreach (Color color2 in CodeHelper.Colors)
            {
                foreach (Color color3 in CodeHelper.Colors)
                {
                    foreach (Color color4 in CodeHelper.Colors)
                    {
                        _possibleCombination.Add([color1, color2, color3, color4]);
                    }
                }
            }
        }

    }
    private bool isFirstGuess = true;
    private readonly Random _random = new();
    public List<Color> GetGuess()
    {
        if (isFirstGuess)
        {
            isFirstGuess = false;
            return [Color.Red, Color.Red, Color.Green, Color.Green];
        }
        return _possibleCombination[_random.Next(_possibleCombination.Count - 1)];
    }



    public Dictionary<MastermindGuessType, int> HandleGuess(List<Color> guess)
    {
        Dictionary<MastermindGuessType, int> result = CodeHelper.EvaluateMastermindGuess(_masterCode, guess);

        _possibleCombination = _possibleCombination.Where(value =>
        {
            Dictionary<MastermindGuessType, int> combRes = CodeHelper.EvaluateMastermindGuess(value, guess);
            return combRes[MastermindGuessType.WrongLocation] == result[MastermindGuessType.WrongLocation] && combRes[MastermindGuessType.CorrectLocation] == result[MastermindGuessType.CorrectLocation];
        }).ToList();

        return result;
    }



    public void FinalResult(bool condition)
    {
        if (condition)
            Console.WriteLine("Game Over. The computer broke your code\n");
        else
            Console.WriteLine("You Win!!!. The computer failed to break your code\n");
    }

}
