using ChessModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel.Model
{
    public class Board
    {
        public int Size { get; set; } = 8;
        public Cell[,] Grid { get; set; }
        public ConsoleColor Color1 { get; set; } = ConsoleColor.Yellow;
        public ConsoleColor Color2 { get; set; } = ConsoleColor.Green;
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public Board(int s)
        {
            Size = s;
            Grid = new Cell[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    //initialize cell in board
                    Grid[i, j] = new Cell(i, j);
                    Grid[i, j].BackgroundColor = Color1;
                    if (j != Size - 1)
                    {
                        // alter color in sequential cell
                        ConsoleColor tempColor = Color1;
                        Color1 = Color2;
                        Color2 = tempColor;
                    }

                }
            }

        }
        public void AddPlayersToBoard()
        {
            var pieceset1 = Player1.PieceSet;
            var pieceset2 = Player2.PieceSet;
            for (int i = 0; i < 16; i++)
            {
                int x = pieceset1[i].CurrentPosition.x;
                int y = pieceset1[i].CurrentPosition.y;
                //Invalid postition.
                if (x < 0 || x > 7 || y < 0 || y > 7)
                {
                    continue;
                }
                Grid[x, y].IsCurrentlyOcupied = true;
                Grid[x, y].CurrentPiece = pieceset1[i];
            }

            for (int i = 0; i < 16; i++)
            {
                int x = pieceset2[i].CurrentPosition.x;
                int y = pieceset2[i].CurrentPosition.y;
                //Invalid postition.
                if (x < 0 || x > 7 || y < 0 || y > 7)
                {
                    continue;
                }
                Grid[x, y].IsCurrentlyOcupied = true;
                Grid[x, y].CurrentPiece = pieceset2[i];
            }

        }


        public void Print()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("  1  2  3  4  5  6  7  8");
            for (int i = 0; i < Size; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(i + 1);
                for (int j = 0; j < Size; j++)
                {

                    if (Grid[i, j].IsCurrentlyOcupied)
                    {
                        Console.BackgroundColor = Grid[i, j].BackgroundColor;
                        Console.ForegroundColor = Grid[i, j].CurrentPiece.ForegroundColor;
                        if (Grid[i, j].NextLegalMove == true)
                        {
                            Console.BackgroundColor = ConsoleColor.Magenta;
                        }
                        Console.Write($" {Grid[i, j].CurrentPiece.PieceImage} ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = Grid[i, j].BackgroundColor;
                        if (Grid[i, j].NextLegalMove == true)
                        {
                            Console.BackgroundColor = ConsoleColor.Magenta;
                        }
                        Console.Write("   ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
                Console.WriteLine();
            }

        }

        public bool IsValidCell(int x, int y)
        {
            if ((x >= 0 && x < 8) && (y >= 0 && y < 8)) return true;
            return false;
        }
        public bool Move(int x, int y, int destX, int destY, ConsoleColor color)
        {
            if (IsValidCell(x, y) && IsValidCell(destX, destY))
            {
                if (Grid[x, y].IsCurrentlyOcupied)
                {
                    if (Grid[x, y].CurrentPiece.ForegroundColor == color)
                    {
                        Cell currentCell = Grid[x, y];
                        Piece piece = currentCell.CurrentPiece;
                        Cell destCell = Grid[destX, destY];
                        bool destOcupied = Grid[destX, destY].IsCurrentlyOcupied;
                        Piece destPiece = null;
                        if (destOcupied)
                        {
                            destPiece = Grid[destX, destY].CurrentPiece;
                        }
                        MarkNextLegalMoves(currentCell, piece);
                        if (Grid[destX, destY].NextLegalMove == true)
                        {
                            
                            if (Grid[destX, destY].IsCurrentlyOcupied && Grid[destX, destY].CurrentPiece.PieceName == "King")
                            {
                                Console.WriteLine("Cannot hit King");
                                return false;
                            }
                   //         bool checkMate = IsInCheckMate(color);
                            piece.CurrentPosition = (destX, destY);
                            Grid[destX, destY].IsCurrentlyOcupied = true;
                            Grid[destX, destY].CurrentPiece = piece;
                            Grid[x, y].IsCurrentlyOcupied = false;
                            Grid[x, y].CurrentPiece = null;
                            ClearNextLegalMoves();
                            if(IsInCheckMate(color))
                            {
                                piece.CurrentPosition = (x, y);
                                Grid[x, y] = currentCell;
                                Grid[x, y].IsCurrentlyOcupied = true;
                                Grid[x, y].CurrentPiece = piece;
                                Grid[destX, destY] = destCell;
                                Grid[destX, destY].IsCurrentlyOcupied = destOcupied;
                                Grid[destX, destY].CurrentPiece = destPiece;
                                return false;
                            }
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Destination");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Wrong piece Selected");
                    }
                }
                else
                {
                    Console.WriteLine("No piece Selected");
                }
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }

            return false;
        }
        public void ClearNextLegalMoves()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Grid[i, j].NextLegalMove = false;
                }
            }
        }
        public void MarkNextLegalMoves(Cell currentCell, Piece piece)
        {

            switch (piece.PieceName)
            {
                case "King":
                    if (IsValidCell(currentCell.RowNumber + 1, currentCell.ColumnNumber))
                    {
                        if (piece.ForegroundColor != Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber].CurrentPiece?.ForegroundColor)
                            Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber].NextLegalMove = true;
                    }
                    if (IsValidCell(currentCell.RowNumber - 1, currentCell.ColumnNumber))
                    {
                        if (piece.ForegroundColor != Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber].CurrentPiece?.ForegroundColor)
                            Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber].NextLegalMove = true;
                    }
                    if (IsValidCell(currentCell.RowNumber, currentCell.ColumnNumber + 1))
                    {
                        if (piece.ForegroundColor != Grid[currentCell.RowNumber, currentCell.ColumnNumber + 1].CurrentPiece?.ForegroundColor)
                            Grid[currentCell.RowNumber, currentCell.ColumnNumber + 1].NextLegalMove = true;
                    }
                    if (IsValidCell(currentCell.RowNumber, currentCell.ColumnNumber - 1))
                    {
                        if (piece.ForegroundColor != Grid[currentCell.RowNumber, currentCell.ColumnNumber - 1].CurrentPiece?.ForegroundColor)
                            Grid[currentCell.RowNumber, currentCell.ColumnNumber - 1].NextLegalMove = true;
                    }
                    if (IsValidCell(currentCell.RowNumber + 1, currentCell.ColumnNumber + 1))
                    {
                        if (piece.ForegroundColor != Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].CurrentPiece?.ForegroundColor)
                            Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].NextLegalMove = true;
                    }
                    if (IsValidCell(currentCell.RowNumber + 1, currentCell.ColumnNumber - 1))
                    {
                        if (piece.ForegroundColor != Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].CurrentPiece?.ForegroundColor)
                            Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].NextLegalMove = true;
                    }

                    if (IsValidCell(currentCell.RowNumber - 1, currentCell.ColumnNumber + 1))
                    {
                        if (piece.ForegroundColor != Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].CurrentPiece?.ForegroundColor)
                            Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].NextLegalMove = true;
                    }
                    if (IsValidCell(currentCell.RowNumber - 1, currentCell.ColumnNumber - 1))
                    {
                        if (piece.ForegroundColor != Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].CurrentPiece?.ForegroundColor)
                            Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].NextLegalMove = true;
                    }

                    break;


                case "Queen":
                    //check all row in same column above the current cell
                    for (int i = piece.CurrentPosition.x - 1; i >= 0; i--)
                    {
                        if (Grid[i, currentCell.ColumnNumber].IsCurrentlyOcupied)
                        {
                            // if color not matched then can hit first piece
                            if (Grid[i, currentCell.ColumnNumber].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[i, currentCell.ColumnNumber].NextLegalMove = true;
                                break;
                            }
                            else
                            {
                                //if color match movement is illeagal
                                break;
                            }
                        }
                        else
                        {
                            Grid[i, currentCell.ColumnNumber].NextLegalMove = true;
                        }
                    }

                    //check all row in same column below the current cell
                    for (int i = piece.CurrentPosition.x + 1; i < 8; i++)
                    {
                        if (Grid[i, currentCell.ColumnNumber].IsCurrentlyOcupied)
                        {
                            // if color not matched then can hit first piece
                            if (Grid[i, currentCell.ColumnNumber].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[i, currentCell.ColumnNumber].NextLegalMove = true;
                                break;
                            }
                            else
                            {
                                //if color match movement is illeagal
                                break;
                            }
                        }
                        else
                        {
                            Grid[i, currentCell.ColumnNumber].NextLegalMove = true;
                        }
                    }
                    //check all row in same row at left of the current cell
                    for (int i = piece.CurrentPosition.y - 1; i >= 0; i--)
                    {
                        if (Grid[currentCell.RowNumber, i].IsCurrentlyOcupied)
                        {
                            // if color not matched then can hit first piece
                            if (Grid[currentCell.RowNumber, i].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[currentCell.RowNumber, i].NextLegalMove = true;
                                break;
                            }
                            else
                            {
                                //if color match movement is illeagal
                                break;
                            }
                        }
                        else
                        {
                            Grid[currentCell.RowNumber, i].NextLegalMove = true;
                        }
                    }

                    //check all row in same column below the current cell
                    for (int i = piece.CurrentPosition.y + 1; i < 8; i++)
                    {
                        if (Grid[currentCell.RowNumber, i].IsCurrentlyOcupied)
                        {
                            // if color not matched then can hit first piece
                            if (Grid[currentCell.RowNumber, i].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[currentCell.RowNumber, i].NextLegalMove = true;
                                break;
                            }
                            else
                            {
                                //if color match movement is illeagal
                                break;
                            }
                        }
                        else
                        {
                            Grid[currentCell.RowNumber, i].NextLegalMove = true;
                        }
                    }


                    //upper left corner
                    for (int row = piece.CurrentPosition.x - 1, col = piece.CurrentPosition.y - 1; row >= 0 && col >= 0; row--, col--)
                    {
                        if (Grid[row, col].IsCurrentlyOcupied)
                        {
                            if (Grid[row, col].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[row, col].NextLegalMove = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            Grid[row, col].NextLegalMove = true;
                        }
                    }
                    //upper right conrner
                    for (int row = piece.CurrentPosition.x - 1, col = piece.CurrentPosition.y + 1; row >= 0 && col < 8; row--, col++)
                    {
                        if (Grid[row, col].IsCurrentlyOcupied)
                        {
                            if (Grid[row, col].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[row, col].NextLegalMove = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            Grid[row, col].NextLegalMove = true;
                        }
                    }
                    // lower left corner
                    for (int row = piece.CurrentPosition.x + 1, col = piece.CurrentPosition.y - 1; row < 8 && col >= 0; row++, col--)
                    {
                        if (Grid[row, col].IsCurrentlyOcupied)
                        {
                            if (Grid[row, col].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[row, col].NextLegalMove = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            Grid[row, col].NextLegalMove = true;
                        }
                    }

                    // lower right corner
                    for (int row = piece.CurrentPosition.x + 1, col = piece.CurrentPosition.y + 1; row < 8 && col < 8; row++, col++)
                    {
                        if (Grid[row, col].IsCurrentlyOcupied)
                        {
                            if (Grid[row, col].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[row, col].NextLegalMove = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            Grid[row, col].NextLegalMove = true;
                        }
                    }

                    break;


                case "Rook":
                    //check all row in same column above the current cell
                    for (int i = piece.CurrentPosition.x - 1; i >= 0; i--)
                    {
                        if (Grid[i, currentCell.ColumnNumber].IsCurrentlyOcupied)
                        {
                            // if color not matched then can hit first piece
                            if (Grid[i, currentCell.ColumnNumber].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[i, currentCell.ColumnNumber].NextLegalMove = true;
                                break;
                            }
                            else
                            {
                                //if color match movement is illeagal
                                break;
                            }
                        }
                        else
                        {
                            Grid[i, currentCell.ColumnNumber].NextLegalMove = true;
                        }
                    }

                    //check all row in same column below the current cell
                    for (int i = piece.CurrentPosition.x + 1; i < 8; i++)
                    {
                        if (Grid[i, currentCell.ColumnNumber].IsCurrentlyOcupied)
                        {
                            // if color not matched then can hit first piece
                            if (Grid[i, currentCell.ColumnNumber].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[i, currentCell.ColumnNumber].NextLegalMove = true;
                                break;
                            }
                            else
                            {
                                //if color match movement is illeagal
                                break;
                            }
                        }
                        else
                        {
                            Grid[i, currentCell.ColumnNumber].NextLegalMove = true;
                        }
                    }
                    //check all row in same row at left of the current cell
                    for (int i = piece.CurrentPosition.y - 1; i >= 0; i--)
                    {
                        if (Grid[currentCell.RowNumber, i].IsCurrentlyOcupied)
                        {
                            // if color not matched then can hit first piece
                            if (Grid[currentCell.RowNumber, i].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[currentCell.RowNumber, i].NextLegalMove = true;
                                break;
                            }
                            else
                            {
                                //if color match movement is illeagal
                                break;
                            }
                        }
                        else
                        {
                            Grid[currentCell.RowNumber, i].NextLegalMove = true;
                        }
                    }

                    //check all row in same column below the current cell
                    for (int i = piece.CurrentPosition.y + 1; i < 8; i++)
                    {
                        if (Grid[currentCell.RowNumber, i].IsCurrentlyOcupied)
                        {
                            // if color not matched then can hit first piece
                            if (Grid[currentCell.RowNumber, i].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[currentCell.RowNumber, i].NextLegalMove = true;
                                break;
                            }
                            else
                            {
                                //if color match movement is illeagal
                                break;
                            }
                        }
                        else
                        {
                            Grid[currentCell.RowNumber, i].NextLegalMove = true;
                        }
                    }


                    break;
                case "Bishop":
                    //upper left corner
                    for (int row = piece.CurrentPosition.x - 1, col = piece.CurrentPosition.y - 1; row >= 0 && col >= 0; row--, col--)
                    {
                        if (Grid[row, col].IsCurrentlyOcupied)
                        {
                            if (Grid[row, col].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[row, col].NextLegalMove = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            Grid[row, col].NextLegalMove = true;
                        }
                    }
                    //upper right conrner
                    for (int row = piece.CurrentPosition.x - 1, col = piece.CurrentPosition.y + 1; row >= 0 && col < 8; row--, col++)
                    {
                        if (Grid[row, col].IsCurrentlyOcupied)
                        {
                            if (Grid[row, col].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[row, col].NextLegalMove = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            Grid[row, col].NextLegalMove = true;
                        }
                    }
                    // lower left corner
                    for (int row = piece.CurrentPosition.x + 1, col = piece.CurrentPosition.y - 1; row < 8 && col >= 0; row++, col--)
                    {
                        if (Grid[row, col].IsCurrentlyOcupied)
                        {
                            if (Grid[row, col].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[row, col].NextLegalMove = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            Grid[row, col].NextLegalMove = true;
                        }
                    }

                    // lower right corner
                    for (int row = piece.CurrentPosition.x + 1, col = piece.CurrentPosition.y + 1; row < 8 && col < 8; row++, col++)
                    {
                        if (Grid[row, col].IsCurrentlyOcupied)
                        {
                            if (Grid[row, col].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[row, col].NextLegalMove = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            Grid[row, col].NextLegalMove = true;
                        }
                    }

                    break;
                case "Knight":
                    // x, y are the coordinates of next position 
                    int x = piece.CurrentPosition.x + 1;
                    int y = piece.CurrentPosition.y + 2;
                    if (IsValidCell(x, y))
                    {
                        if (Grid[x, y].IsCurrentlyOcupied)
                        {
                            if (Grid[x, y].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[x, y].NextLegalMove = true;
                            }
                        }
                        else
                        {
                            Grid[x, y].NextLegalMove = true;
                        }
                    }
                    x = piece.CurrentPosition.x + 1;
                    y = piece.CurrentPosition.y - 2;
                    if (IsValidCell(x, y))
                    {
                        if (Grid[x, y].IsCurrentlyOcupied)
                        {
                            if (Grid[x, y].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[x, y].NextLegalMove = true;
                            }
                        }
                        else
                        {
                            Grid[x, y].NextLegalMove = true;
                        }
                    }
                    x = piece.CurrentPosition.x - 1;
                    y = piece.CurrentPosition.y + 2;
                    if (IsValidCell(x, y))
                    {
                        if (Grid[x, y].IsCurrentlyOcupied)
                        {
                            if (Grid[x, y].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[x, y].NextLegalMove = true;
                            }
                        }
                        else
                        {
                            Grid[x, y].NextLegalMove = true;
                        }
                    }
                    x = piece.CurrentPosition.x - 1;
                    y = piece.CurrentPosition.y - 2;
                    if (IsValidCell(x, y))
                    {
                        if (Grid[x, y].IsCurrentlyOcupied)
                        {
                            if (Grid[x, y].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[x, y].NextLegalMove = true;
                            }
                        }
                        else
                        {
                            Grid[x, y].NextLegalMove = true;
                        }
                    }
                    x = piece.CurrentPosition.x + 2;
                    y = piece.CurrentPosition.y + 1;
                    if (IsValidCell(x, y))
                    {
                        if (Grid[x, y].IsCurrentlyOcupied)
                        {
                            if (Grid[x, y].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[x, y].NextLegalMove = true;
                            }
                        }
                        else
                        {
                            Grid[x, y].NextLegalMove = true;
                        }
                    }
                    x = piece.CurrentPosition.x + 2;
                    y = piece.CurrentPosition.y - 1;
                    if (IsValidCell(x, y))
                    {
                        if (Grid[x, y].IsCurrentlyOcupied)
                        {
                            if (Grid[x, y].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[x, y].NextLegalMove = true;
                            }
                        }
                        else
                        {
                            Grid[x, y].NextLegalMove = true;
                        }
                    }
                    x = piece.CurrentPosition.x - 2;
                    y = piece.CurrentPosition.y + 1;
                    if (IsValidCell(x, y))
                    {
                        if (Grid[x, y].IsCurrentlyOcupied)
                        {
                            if (Grid[x, y].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[x, y].NextLegalMove = true;
                            }
                        }
                        else
                        {
                            Grid[x, y].NextLegalMove = true;
                        }
                    }
                    x = piece.CurrentPosition.x - 2;
                    y = piece.CurrentPosition.y - 1;
                    if (IsValidCell(x, y))
                    {
                        if (Grid[x, y].IsCurrentlyOcupied)
                        {
                            if (Grid[x, y].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                            {
                                Grid[x, y].NextLegalMove = true;
                            }
                        }
                        else
                        {
                            Grid[x, y].NextLegalMove = true;
                        }
                    }
                    break;
                case "Pawn":
                    if (piece.ForegroundColor == Player1.Color)
                    {

                        x = piece.CurrentPosition.x + 1;
                        y = piece.CurrentPosition.y;
                        if (IsValidCell(x, y))
                        {
                            if (!Grid[x, y].IsCurrentlyOcupied)
                            {
                                Grid[x, y].NextLegalMove = true;
                            }
                        }
                        x = piece.CurrentPosition.x + 1;
                        y = piece.CurrentPosition.y - 1;
                        if (IsValidCell(x, y))
                        {
                            if (Grid[x, y].IsCurrentlyOcupied)
                            {
                                if (Grid[x, y].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                                    Grid[x, y].NextLegalMove = true;
                            }
                        }
                        x = piece.CurrentPosition.x + 1;
                        y = piece.CurrentPosition.y + 1;
                        if (IsValidCell(x, y))
                        {
                            if (Grid[x, y].IsCurrentlyOcupied)
                            {
                                if (Grid[x, y].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                                    Grid[x, y].NextLegalMove = true;
                            }
                        }
                    }
                    else
                    {
                        x = piece.CurrentPosition.x - 1;
                        y = piece.CurrentPosition.y;
                        if (IsValidCell(x, y))
                        {
                            if (!Grid[x, y].IsCurrentlyOcupied)
                            {
                                Grid[x, y].NextLegalMove = true;
                            }
                        }
                        x = piece.CurrentPosition.x - 1;
                        y = piece.CurrentPosition.y - 1;
                        if (IsValidCell(x, y))
                        {
                            if (Grid[x, y].IsCurrentlyOcupied)
                            {
                                if (Grid[x, y].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                                    Grid[x, y].NextLegalMove = true;
                            }
                        }
                        x = piece.CurrentPosition.x - 1;
                        y = piece.CurrentPosition.y + 1;
                        if (IsValidCell(x, y))
                        {
                            if (Grid[x, y].IsCurrentlyOcupied)
                            {
                                if (Grid[x, y].CurrentPiece.ForegroundColor != piece.ForegroundColor)
                                    Grid[x, y].NextLegalMove = true;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public bool IsInCheckMate(ConsoleColor color)
        {
            if(color == Player2.Color)
            {
                ClearNextLegalMoves();
                Piece king2 = new Piece();
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        if (Grid[i, j].IsCurrentlyOcupied)
                        {
                            if (Grid[i, j].CurrentPiece.ForegroundColor == Player1.Color)
                            {
                                MarkNextLegalMoves(Grid[i, j], Grid[i, j].CurrentPiece);
                            }
                            else if (Grid[i, j].CurrentPiece.ForegroundColor == Player2.Color && Grid[i, j].CurrentPiece.PieceName == "King")
                            {
                                king2 = Grid[i, j].CurrentPiece;
                            }
                        }
                    }
                }

                if (Grid[king2.CurrentPosition.x, king2.CurrentPosition.y].NextLegalMove == true) return true;
            }
            else
            {
                Piece king1 = new Piece();

                ClearNextLegalMoves();
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        if (Grid[i, j].IsCurrentlyOcupied)
                        {
                            if (Grid[i, j].CurrentPiece.ForegroundColor == Player2.Color)
                            {
                                MarkNextLegalMoves(Grid[i, j], Grid[i, j].CurrentPiece);
                            }
                            else if (Grid[i, j].CurrentPiece.ForegroundColor == Player1.Color && Grid[i, j].CurrentPiece.PieceName == "King")
                            {
                                king1 = Grid[i, j].CurrentPiece;
                            }
                        }
                    }
                }
                if (Grid[king1.CurrentPosition.x, king1.CurrentPosition.y].NextLegalMove == true) return true;
            }
            


            
            ClearNextLegalMoves();
            return false;
        }
    }
}
