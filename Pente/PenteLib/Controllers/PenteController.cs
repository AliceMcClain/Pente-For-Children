using PenteLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenteLib.Controllers
{
    public static class PenteController
    {
        public static Pente game;

        public static void StartGame(PlayMode playMode = PlayMode.SinglePlayer)
        {
            game = new Pente(playMode);
        }

        private static bool ValidateMove(int row, int column)
        {
            bool result = false;
            if(row >= 0 && row < 19 && column >= 0 && column < 19)
            {
                if(game.GetPieceAt(row, column) == PieceColor.Empty)
                {
                    result = true;
                }
            }
            return result;
        }

        private static void ProcessMove(int row, int column)
        {
            PieceColor color = game.isFirstPlayersTurn ? PieceColor.Black : PieceColor.White;

            game.SetPieceAt(row, column, color);
        }

        public static void TakeTurn(int row, int column)
        {
            bool turnIsValid = ValidateMove(row, column);

            if (turnIsValid)
            {
                ProcessMove(row, column);
            }
        }
    }
}
