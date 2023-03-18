using ChessModel.Model;
using ChessModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel.Repositories
{
    public class PlayerRipository
    {
        public IPlayer GetPlayer(IPlayer player, ConsoleColor color, PieceRepository pieceRepo, bool opponent=false)
        {
            player.PieceSet = new Piece[16];
            if (!opponent)
            {
                player.PieceSet[0] = (Piece)pieceRepo.GetPiece(new Piece(), "Pawn", "♙", color, 1, 0);
                player.PieceSet[1] = (Piece)pieceRepo.GetPiece(new Piece(), "Pawn", "♙", color, 1, 1); 
                player.PieceSet[2] = (Piece)pieceRepo.GetPiece(new Piece(), "Pawn", "♙", color, 1, 2);
                player.PieceSet[3] = (Piece)pieceRepo.GetPiece(new Piece(), "Pawn", "♙", color, 1, 3);
                player.PieceSet[4] = (Piece)pieceRepo.GetPiece(new Piece(), "Pawn", "♙", color, 1, 4);
                player.PieceSet[5] = (Piece)pieceRepo.GetPiece(new Piece(), "Pawn", "♙", color, 1, 5);
                player.PieceSet[6] = (Piece)pieceRepo.GetPiece(new Piece(), "Pawn", "♙", color, 1, 6);
                player.PieceSet[7] = (Piece)pieceRepo.GetPiece(new Piece(), "Pawn", "♙", color, 1, 7);

                player.PieceSet[12] = (Piece)pieceRepo.GetPiece(new Piece(), "Rook", "♜", color, 0, 0);
                player.PieceSet[10] = (Piece)pieceRepo.GetPiece(new Piece(), "Knight", "♞", color, 0, 1);
                player.PieceSet[8] = (Piece)pieceRepo.GetPiece(new Piece(), "Bishop", "♝", color, 0, 2);
                player.PieceSet[14] = (Piece)pieceRepo.GetPiece(new Piece(), "Queen", "♛", color, 0, 3);
                player.PieceSet[15] = (Piece)pieceRepo.GetPiece(new Piece(), "King", "♚", color, 0, 4);
                player.PieceSet[9] = (Piece)pieceRepo.GetPiece(new Piece(), "Bishop", "♝", color, 0, 5);
                player.PieceSet[11] = (Piece)pieceRepo.GetPiece(new Piece(), "Knight", "♞", color, 0, 6);
                player.PieceSet[13] = (Piece)pieceRepo.GetPiece(new Piece(), "Rook", "♜", color, 0, 7);

            }
            else
            {
                player.PieceSet[0] = (Piece)pieceRepo.GetPiece(new Piece(), "Pawn", "♙", color, 6, 0);
                player.PieceSet[1] = (Piece)pieceRepo.GetPiece(new Piece(), "Pawn", "♙", color, 6, 1);
                player.PieceSet[2] = (Piece)pieceRepo.GetPiece(new Piece(), "Pawn", "♙", color, 6, 2);
                player.PieceSet[3] = (Piece)pieceRepo.GetPiece(new Piece(), "Pawn", "♙", color, 6, 3);
                player.PieceSet[4] = (Piece)pieceRepo.GetPiece(new Piece(), "Pawn", "♙", color, 6, 4);
                player.PieceSet[5] = (Piece)pieceRepo.GetPiece(new Piece(), "Pawn", "♙", color, 6, 5);
                player.PieceSet[6] = (Piece)pieceRepo.GetPiece(new Piece(), "Pawn", "♙", color, 6, 6);
                player.PieceSet[7] = (Piece)pieceRepo.GetPiece(new Piece(), "Pawn", "♙", color, 6, 7);

                player.PieceSet[12] = (Piece)pieceRepo.GetPiece(new Piece(), "Rook", "♜", color, 7, 0);
                player.PieceSet[10] = (Piece)pieceRepo.GetPiece(new Piece(), "Knight", "♞", color, 7, 1);
                player.PieceSet[8] = (Piece)pieceRepo.GetPiece(new Piece(), "Bishop", "♝", color, 7, 2);
                player.PieceSet[14] = (Piece)pieceRepo.GetPiece(new Piece(), "Queen", "♛", color, 7, 3);
                player.PieceSet[15] = (Piece)pieceRepo.GetPiece(new Piece(), "King", "♚", color, 7, 4);
                player.PieceSet[9] = (Piece)pieceRepo.GetPiece(new Piece(), "Bishop", "♝", color, 7, 5);
                player.PieceSet[11] = (Piece)pieceRepo.GetPiece(new Piece(), "Knight", "♞", color, 7, 6);
                player.PieceSet[13] = (Piece)pieceRepo.GetPiece(new Piece(), "Rook", "♜", color, 7, 7);

            }
            return player;
                        
        }

    }
}
