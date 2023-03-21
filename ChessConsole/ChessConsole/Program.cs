using ChessModel;
using ChessModel.Model;
using ChessModel.Repositories;
using ChessModel.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;
PlayerRipository playerRepo = new PlayerRipository();
PieceRepository pieceRepo = new PieceRepository(); 
Board board = new Board(8);
board.Player1 = (Player)playerRepo.GetPlayer(new Player(ConsoleColor.DarkRed), ConsoleColor.DarkRed, pieceRepo);
board.Player2 = (Player)playerRepo.GetPlayer(new Player(ConsoleColor.DarkBlue), ConsoleColor.DarkBlue, pieceRepo, true);

board.AddPlayersToBoard();
board.Print();

ConsoleColor color = board.Player1.Color;
int turn = 1;
while(true)
{
   // board.Print();
    if (turn % 2 == 1)
    {
        color = board.Player1.Color;
    }
    else
    {
        color = board.Player2.Color;
    }
    Console.ForegroundColor = color;
    Console.WriteLine($"{color}'s Turn. Select piece like(x, y):");
    var x = Console.ReadLine().Trim().Split().ToList().Select(x => Convert.ToInt32(x) - 1).ToList();
    if (x.Count == 2 && board.IsValidCell(x[0], x[1]))
    {
        if (board.Grid[x[0], x[1]].IsCurrentlyOcupied)
        {
            board.MarkNextLegalMoves(board.Grid[x[0], x[1]], board.Grid[x[0], x[1]].CurrentPiece);
            board.Print();
            Console.WriteLine("Select Desitination (x, y): ");
            var y = Console.ReadLine().Trim().Split().ToList().Select(x => Convert.ToInt32(x) - 1).ToList();
            if (y.Count==2 && board.Move(x[0], x[1], y[0], y[1], color))
            {
                turn++;
            }
            else
            {
                Console.WriteLine("Invalid Move");
            }

            board.ClearNextLegalMoves();
            board.Print();
        }

    }
    else
    {
        Console.WriteLine("Invalid Input");
    }
    if (board.IsInCheckMate(board.Player1.Color))
    {
        board.ClearNextLegalMoves();
        Console.WriteLine($"{board.Player1.Color} is in CheckMate");
    }else if (board.IsInCheckMate(board.Player2.Color))
    {
        board.ClearNextLegalMoves();
        Console.WriteLine($"{board.Player2.Color} is in CheckMate");
    }

}
