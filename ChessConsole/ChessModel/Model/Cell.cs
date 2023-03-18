using ChessModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel.Model
{
    public class Cell : ICell
    {
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public bool IsCurrentlyOcupied { get; set; }
        public bool NextLegalMove { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public Piece CurrentPiece { get; set; }
        public Cell(int i, int j)
        {
            RowNumber = i;
            ColumnNumber = j;
        }
    }
}
