using ChessModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel.Repositories
{
    public class PieceRepository
    {
        public IPiece GetPiece(IPiece piece, string name, string image, ConsoleColor color, int x, int y)
        {
            piece.PieceName = name;
            piece.PieceImage = image;
            piece.CurrentPosition = (x, y);
            piece.ForegroundColor = color;
            return piece;
        }
    }
}
