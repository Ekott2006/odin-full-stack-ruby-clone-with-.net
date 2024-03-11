internal class TicTacToeGame
{
    public List<List<CellState?>> GameArray { get; private set; }
    public TicTacToeGame()
    {
        GameArray = [
            [CellState.Empty, CellState.Empty, CellState.Empty],
            [CellState.Empty, CellState.Empty, CellState.Empty],
            [CellState.Empty, CellState.Empty, CellState.Empty]
        ];
    }
    public bool AddUserInput(int row, int col, CellState user)
    {
        if (GameArray.ElementAtOrDefault(row) == null || GameArray[row].ElementAtOrDefault(col) == null || GameArray[row][col] != CellState.Empty) return false;

        GameArray[row][col] = user;
        return true;

    }

    public GameState GetGameState()
    {
        Dictionary<GameState, Dictionary<string, int>> result = new() {
            {GameState.Player1, new Dictionary<string, int>()},
            {GameState.Player2, new Dictionary<string, int>()},
        };
        int filledCells = 0;
        for (int i = 0; i < GameArray.Count; i++)
        {
            List<CellState?> row = GameArray[i];

            if (GameArray[i][i] == CellState.Player1) DictHelper(result, GameState.Player1, "DL");
            if (GameArray[i][i] == CellState.Player2) DictHelper(result, GameState.Player2, "DL");
            for (int j = 0; j < row.Count; j++)
            {
                CellState? cell = GameArray[i][j];
                if (cell == CellState.Player1 || cell == CellState.Player2) filledCells++;
                // Left to Right
                if (cell == CellState.Player1)
                    DictHelper(result, GameState.Player1, $"LTR{i}");
                if (cell == CellState.Player2)
                    DictHelper(result, GameState.Player2, $"LTR{i}");

                // Bottom to Down
                if (GameArray[j][i] == CellState.Player1)
                    DictHelper(result, GameState.Player1, $"TTB{i}");
                if (GameArray[j][i] == CellState.Player2)
                    DictHelper(result, GameState.Player2, $"TTB{i}");

                // Diagonal Right
                if ((i == 0 && j == 2) || (i == 1 && j == 1) || (i == 2 && j == 0))
                {
                    if (cell == CellState.Player1) DictHelper(result, GameState.Player1, "DR"); // Diagonal right
                    if (cell == CellState.Player2) DictHelper(result, GameState.Player2, "DR");
                }
            }
        }
    
        if (result[GameState.Player1].Values.Count != 0 && result[GameState.Player1].Values.Max() == 3) return GameState.Player1;
        if (result[GameState.Player2].Values.Count != 0 && result[GameState.Player2].Values.Max() == 3) return GameState.Player2;
        return filledCells == 9 ? GameState.Draw : GameState.InProgress;
    }
    

    private static void DictHelper(Dictionary<GameState, Dictionary<string, int>> dict, GameState row, string col)
    {
        if (dict[row].TryGetValue(col, out int value)) dict[row][col]++;
        else dict[row][col] = 1;
    }

    public void PrintBoard()
    {
        Console.WriteLine("|     |  1  |  2  |  3  |");
        Console.WriteLine("+------------------------");
        for (int i = 0; i < GameArray.Count; i++)
        {
            List<CellState?> row = GameArray[i];
            string printRow = $"|  {i + 1}  |";
            foreach (CellState? cell in row)
            {
                if (cell == CellState.Player1 || cell == CellState.Player2)
                    printRow += $"  {(cell == CellState.Player1 ? "X" : "O")}  |";

                else printRow += $"     |";
            }
            Console.WriteLine(printRow);
            Console.WriteLine("+------------------------");
        }
    }
}
