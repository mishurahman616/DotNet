using ChessModel.Model;
using ChessModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel.Repositories
{
    public class CellRepository
    {
        public ICell GetCell(int x, int y)
        {
            return new Cell(x, y);
        }
    }
}
