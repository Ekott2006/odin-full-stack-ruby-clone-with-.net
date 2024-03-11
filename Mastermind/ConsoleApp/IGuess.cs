interface IGuess
{
    List<Color> GetGuess();
    Dictionary<MastermindGuessType, int> HandleGuess(List<Color> guess);
    void FinalResult(bool condition);
}
