using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessModel.Model;

namespace ChessModel.Services
{
    public interface ICell
    {
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public bool IsCurrentlyOcupied { get; set; }
        public bool NextLegalMove { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public Piece CurrentPiece { get; set; }
    }
}
