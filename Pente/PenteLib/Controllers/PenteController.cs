using PenteLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenteLib.Controllers
{
    public struct Point
    {
        public int row;
        public int column;

        public Point(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }
    struct Capture
    {
        public bool captured;
        public Point firstPoint;
        public Point secondPoint;
    }
    struct TesseraResult
    {
        public bool isTria;
        public bool isTessera;
    }
    public static class PenteController
    {
        public static Pente game;

        public static int boardSize = 19;
        public static int boardCenter = boardSize / 2;

        private static bool isDebug;

        public static void StartGame(PlayMode playMode = PlayMode.SinglePlayer, bool isDebug = false, int BoardSize = 19)
        {
            game = new Pente(playMode, BoardSize);
            PenteController.isDebug = isDebug;
            boardSize = BoardSize;
            boardCenter = boardSize / 2;
    }

        private static bool ValidateMove(int row, int column)
        {
            bool result = false;
            if (game.Turn == 1 && !isDebug)
            {
                result = row == boardCenter && column == boardCenter;
            }
            else if (game.Turn == 3 && !isDebug)
            {
                if (row >= 0 && row < boardSize && column >= 0 && column < boardSize)
                {
                    if (row >= boardCenter + 3 || row <= boardCenter - 3)
                    {
                        result = true;
                    }
                    else if (column >= boardCenter + 3 || column <= boardCenter - 3)
                    {
                        result = true;
                    }
                }
            }
            else if (row >= 0 && row < boardSize && column >= 0 && column < boardSize)
            {
                if (game.GetPieceAt(row, column) == PieceColor.Empty)
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

            CheckTessera(row, column, color);

            game.IsGameOver = CheckForWinner(row, column, color);

            game.Turn++;

            // Alternates player turn if the game isn't over
            if (!game.IsGameOver)
            {
                game.isFirstPlayersTurn = !game.isFirstPlayersTurn;
            }
        }

        public static bool TakeTurn(int row, int column)
        {
            bool turnIsValid = ValidateMove(row, column);

            if (!game.IsGameOver && turnIsValid)
            {
                ProcessMove(row, column);

                // If you are vs AI, the game isn't over, and it is the AI's turn: take the AI's Turn
                if (game.PlayMode == PlayMode.SinglePlayer && !game.IsGameOver && !game.isFirstPlayersTurn)
                {
                    AITurn();
                }
            }
            return turnIsValid;
        }

        private static bool CheckForWinner(int row, int col, PieceColor color)
        {
            //Checks all directions around the locations specified for a capture, and removes the captures pieces
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                Capture capture = CheckCapture(direction, row, col, color);
                if (capture.captured)
                {
                    // Adds to the capture amount of the player whose turn it is
                    if (game.isFirstPlayersTurn)
                    {
                        game.FirstPlayerCaptures++;
                    }
                    else
                    {
                        game.SecondPlayerCaptures++;
                    }

                    //Removes the captured pieces
                    game.SetPieceAt(capture.firstPoint, PieceColor.Empty);
                    game.SetPieceAt(capture.secondPoint, PieceColor.Empty);

                    CheckTessera(capture.firstPoint.row, capture.firstPoint.column, color == PieceColor.Black ? PieceColor.White : PieceColor.Black);
                    CheckTessera(capture.secondPoint.row, capture.secondPoint.column, color == PieceColor.Black ? PieceColor.White : PieceColor.Black);
                }
            }

            //5 captures means the player has won
            if (game.FirstPlayerCaptures >= 5 || game.SecondPlayerCaptures >= 5)
            {
                return true;
            }

            #region Five in a row checks
            // Up-Down win
            int upCount = NumInARow(Direction.Up, row, col, color);
            int downCount = NumInARow(Direction.Down, row, col, color);
            //Add one for the current piece
            if (upCount + downCount + 1 >= 5)
            {
                return true;
            }

            // Left-Right win
            int leftCount = NumInARow(Direction.Left, row, col, color);
            int rightCount = NumInARow(Direction.Right, row, col, color);
            //Add one for the current piece
            if (leftCount + rightCount + 1 >= 5)
            {
                return true;
            }

            // Diagonal \
            int upLeftCount = NumInARow(Direction.UpLeft, row, col, color);
            int downRightCount = NumInARow(Direction.DownRight, row, col, color);
            //Add one for the current piece
            if (upLeftCount + downRightCount + 1 >= 5)
            {
                return true;
            }

            // Diagonal /
            int upRightCount = NumInARow(Direction.UpRight, row, col, color);
            int downLeftCount = NumInARow(Direction.DownLeft, row, col, color);
            //Add one for the current piece
            if (upRightCount + downLeftCount + 1 >= 5)
            {
                return true;
            }
            #endregion

            return false;
        }

        public static void SkipTurn()
        {
            game.isFirstPlayersTurn = !game.isFirstPlayersTurn;
            game.Turn++;
        }

        private static Capture CheckCapture(Direction direction, int row, int col, PieceColor color)
        {
            int rowChange = 0;
            int colChange = 0;
            Capture capture = new Capture();
            DirectionChange(direction, out rowChange, out colChange);
            PieceColor opposing = color == PieceColor.White ? PieceColor.Black : PieceColor.White;
            // If the edge case is out of bound, there isn't a capture
            if (row + (rowChange * 3) >= 0 && row + (rowChange * 3) < boardSize && col + (colChange * 3) >= 0 && col + (colChange * 3) < boardSize)
            {
                bool firstPiece = game.Board[row + rowChange, col + colChange] == opposing;
                bool secondPiece = game.Board[row + (rowChange * 2), col + (colChange * 2)] == opposing;
                bool closingPiece = game.Board[row + (rowChange * 3), col + (colChange * 3)] == color;
                if (firstPiece && secondPiece && closingPiece)
                {
                    capture.captured = true;
                    capture.firstPoint = new Point(row + rowChange, col + colChange);
                    capture.secondPoint = new Point(row + (rowChange * 2), col + (colChange * 2));
                }
                else
                {
                    capture.captured = false;
                }
            }
            else
            {
                capture.captured = false;
            }
            return capture;
        }

        private static void DirectionChange(Direction direction, out int rowChange, out int colChange)
        {
            rowChange = 0;
            colChange = 0;
            if (direction == Direction.Up || direction == Direction.UpLeft || direction == Direction.UpRight)//Checking for up
            {
                rowChange = -1;
            }
            else if (direction == Direction.Down || direction == Direction.DownLeft || direction == Direction.DownRight)//Checking for down
            {
                rowChange = 1;
            }
            if (direction == Direction.Right || direction == Direction.DownRight || direction == Direction.UpRight)//Checking for right
            {
                colChange = 1;
            }
            else if (direction == Direction.Left || direction == Direction.DownLeft || direction == Direction.UpLeft)//Checking for left
            {
                colChange = -1;
            }
        }
        private static int NumInARow(Direction direction, int row, int col, PieceColor color)
        {

            int inARow = 0;
            int rowChange = 0;
            int colChange = 0;
            DirectionChange(direction, out rowChange, out colChange);
            PieceColor opposing = color == PieceColor.White ? PieceColor.Black : PieceColor.White;

            //Makes sure our row and column are in range of the board
            bool inRange = row + rowChange >= 0 && row + rowChange < boardSize && col + colChange >= 0 && col + colChange < boardSize;
            while (inRange && game.Board[row + rowChange, col + colChange] == color)
            {
                inARow++;
                row += rowChange;
                col += colChange;

                inRange = row + rowChange >= 0 && row + rowChange < boardSize && col + colChange >= 0 && col + colChange < boardSize;
            }

            return inARow;
        }

        private static bool CheckTessera(int row, int col, PieceColor pieceColor)
        {
            bool result = false;

            // Up-Down
            for(int i = 0; i < 4 && !result; i++)
            {
                Point[] points = new Point[6];
                for(int j = 0; j < 6; j++)
                {
                    points[j] = new Point(row - j + i + 1, col);
                }
                TesseraResult tesseraResult = CheckInRange(points, pieceColor);
                game.Tessera = tesseraResult.isTessera;
                game.Tria = tesseraResult.isTria;

                result = tesseraResult.isTessera || tesseraResult.isTria;
            }
            // Left-Right
            for (int i = 0; i < 4 && !result; i++)
            {
                Point[] points = new Point[6];
                for (int j = 0; j < 6; j++)
                {
                    points[j] = new Point(row, col - j + i + 1);
                }
                TesseraResult tesseraResult = CheckInRange(points, pieceColor);
                game.Tessera = tesseraResult.isTessera;
                game.Tria = tesseraResult.isTria;

                result = tesseraResult.isTessera || tesseraResult.isTria;
            }
            // Diagonal \
            for (int i = 0; i < 4 && !result; i++)
            {
                Point[] points = new Point[6];
                for (int j = 0; j < 6; j++)
                {
                    points[j] = new Point(row - j + i + 1, col - j + i + 1);
                }
                TesseraResult tesseraResult = CheckInRange(points, pieceColor);
                game.Tessera = tesseraResult.isTessera;
                game.Tria = tesseraResult.isTria;

                result = tesseraResult.isTessera || tesseraResult.isTria;
            }
            // Diagonal /
            for (int i = 0; i < 4 && !result; i++)
            {
                Point[] points = new Point[6];
                for (int j = 0; j < 6; j++)
                {
                    points[j] = new Point(row + j - i - 1, col - j + i + 1);
                }
                TesseraResult tesseraResult = CheckInRange(points, pieceColor);
                game.Tessera = tesseraResult.isTessera;
                game.Tria = tesseraResult.isTria;

                result = tesseraResult.isTessera || tesseraResult.isTria;
            }

            return result;
        }

        private static TesseraResult CheckInRange(Point[] points, PieceColor pieceColor)
        {
            TesseraResult result = new TesseraResult();
            if(points.Length == 6)
            {
                //makes sure the end points are open
                if(game.GetPieceAt(points[0]) == PieceColor.Empty && game.GetPieceAt(points[5]) == PieceColor.Empty)
                {
                    result.isTria = true;
                    result.isTessera = true;
                    for(int i = 1; i < 5; i++)
                    {
                        if(game.GetPieceAt(points[i]) != pieceColor)
                        {
                            result.isTria = result.isTessera ? true : false;
                            result.isTessera = false;
                        }
                    }
                }
            }
            return result;
        }

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
