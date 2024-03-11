
TicTacToeGame game = new();
Console.WriteLine("Tic-Tac-Toe Game on the command line");

//  Get Player 2 Input if he will be X or O
Console.Write("Player 1, choose X or O (default is X): ");
string result = (Console.ReadLine() ?? "").Trim();
Console.WriteLine(result);
CellState user1 = result == "O" ? CellState.Player2 : CellState.Player1;
CellState user2 = user1 == CellState.Player1 ? CellState.Player2 : CellState.Player1;
Console.WriteLine($"Player 1 is {(user1 == CellState.Player1 ? 'X' : 'O')} and Player 2 is {(user2 == CellState.Player1 ? 'X' : 'O')}");
game.PrintBoard();

while (true)
{
    List<(string name, CellState input)> players = [("Player 1", user1), ("Player 2", user2)];
    for (int i = 0; i < players.Count; i++)
    {
        (string name, CellState input) = players[i];
        // Get User Input
        while (true)
        {
            try
            {
                Console.Write($"{name}, enter your move (row): ");
                int row = Convert.ToInt32(Console.ReadLine());
                Console.Write($"{name}, enter your move (column): ");
                int column = Convert.ToInt32(Console.ReadLine());
                bool inputCorrect = game.AddUserInput(row - 1, column - 1, input);
                if (!inputCorrect) throw new Exception();
                break;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 3. Try again!!!");
            }
        }
        game.PrintBoard();
        // Check for winner
        GameState gameState = game.GetGameState();
        if (gameState == GameState.InProgress) continue;
        if (gameState == GameState.Draw) Console.WriteLine("DRAW!!!");
        else Console.WriteLine($"{name} Wins!!!");
        return 0;
    }
}