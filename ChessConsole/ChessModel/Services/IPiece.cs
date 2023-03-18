using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel.Services
{
    public interface IPiece
    {
        public string PieceName { get; set; }
        public string PieceImage { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public (int x, int y) CurrentPosition { get; set; }
    }
}
