using ChessModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel.Model
{
    public class Player : IPlayer
    {
        public ConsoleColor Color { get; set; }
        public Piece[] PieceSet { get; set; }
        public Player(ConsoleColor color)
        {
            Color = color;

        }


    }
}
