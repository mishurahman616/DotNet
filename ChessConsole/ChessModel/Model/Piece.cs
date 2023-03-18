using ChessModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel.Model
{
    public class Piece : IPiece
    {
        public string PieceName { get; set; }
        public string PieceImage { get; set; }
        public ConsoleColor ForegroundColor { get; set; }

        public (int x, int y) CurrentPosition { get; set; }
        public Piece()
        {

        }
        public Piece(string pName, string pImage, ConsoleColor fg, int x, int y)
        {
            PieceName= pName;
            PieceImage= pImage;
            ForegroundColor = fg;
            CurrentPosition = (x, y);
        }
    }
}
