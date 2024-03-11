
class CodeMaker : IGuess
{
    private readonly List<Color> _masterCode;

    public CodeMaker()
    {
        _masterCode = CodeHelper.GetRandomColor();
        
    }
    public void FinalResult(bool condition)
    {
        if (condition)
            Console.WriteLine("You Win!!!\n");
        else
            Console.WriteLine("You Lose. Try harder next time\n");
    }

    public List<Color> GetGuess()
    {
        return CodeHelper.GetInput();
    }

    public Dictionary<MastermindGuessType, int> HandleGuess(List<Color> guess)
    {
        Dictionary<MastermindGuessType, int> result = CodeHelper.EvaluateMastermindGuess(_masterCode, guess);
        return result;
    }
}