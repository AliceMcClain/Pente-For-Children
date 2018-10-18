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

        public static int boardSize = 19;

        public static void StartGame(PlayMode playMode = PlayMode.SinglePlayer)
        {
            game = new Pente(playMode);
        }

        private static bool ValidateMove(int row, int column)
        {
            bool result = false;
            if(row >= 0 && row < boardSize && column >= 0 && column < boardSize)
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

            // Alternates player turn
            game.isFirstPlayersTurn = !game.isFirstPlayersTurn;

            game.IsGameOver = CheckForWinner();
        }

        public static bool TakeTurn(int row, int column)
        {
            bool turnIsValid = ValidateMove(row, column);

            if (turnIsValid)
            {
                ProcessMove(row, column);

                // If you are vs AI, the game isn't over, and it is the AI's turn: take the AI's Turn
                if(game.PlayMode == PlayMode.SinglePlayer && !game.IsGameOver && !game.isFirstPlayersTurn)
                {
                    AITurn();
                }
            }
            return turnIsValid;
        }

        private static bool CheckForWinner()
        {
            return false;
        }

        //private static int NumInARow()
        //{

        //}

        private static void AITurn()
        {
            Random rand = new Random();

            int rowChoice;
            int colChoice;

            bool turnIsValid;
            do
            {
                rowChoice = rand.Next(boardSize);
                colChoice = rand.Next(boardSize);

                turnIsValid = TakeTurn(rowChoice, colChoice);
            } while (!turnIsValid);
        }
    }
}
