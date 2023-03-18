using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessModel.Model;

namespace ChessModel.Services
{
    public interface IPlayer
    {
        public ConsoleColor Color { get; set; }
        public Piece[] PieceSet { get; set; }
    }
}
