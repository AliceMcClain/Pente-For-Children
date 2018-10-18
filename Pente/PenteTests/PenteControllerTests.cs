using Microsoft.VisualStudio.TestTools.UnitTesting;
using PenteLib.Controllers;
using PenteLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenteTests
{
    [TestClass]
    public class PenteControllerTests
    {
        [TestMethod]
        public void GetPieceTest()
        {
            int row = 1;
            int colum = 1;
            PenteController.StartGame();

            PieceColor actual = PenteController.game.GetPieceAt(row, colum);
            PieceColor expected = PieceColor.Empty;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FirstPlayerTakesTurn()
        {
            int row = 1;
            int column = 1;

            PenteController.StartGame();

            PenteController.TakeTurn(row, column);

            PieceColor actual = PenteController.game.GetPieceAt(row, column);
            PieceColor expected = PieceColor.Black;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SecondPlayerTakesTurn()
        {
            int row = 5;
            int column = 5;

            PenteController.StartGame();

            PenteController.TakeTurn(1, 1);
            PenteController.TakeTurn(row, column);

            PieceColor actual = PenteController.game.GetPieceAt(row, column);
            PieceColor expected = PieceColor.White;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ChangePlayerTurnAfterTakingATurn_MultiPlayerMode()
        {
            PenteController.StartGame(PlayMode.MultiPlayer);
            var expected = !PenteController.game.isFirstPlayersTurn;
            bool actual;

            PenteController.TakeTurn(1, 1);
            actual = PenteController.game.isFirstPlayersTurn;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetPieceTest()
        {
            PenteController.StartGame();
            PenteController.game.SetPieceAt(0, 1, PieceColor.Black);
            PieceColor expected = PieceColor.Black;
            PieceColor actual = PenteController.game.Board[0, 1];

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CaptureTest()
        {
            PenteController.StartGame();
            //set up capture
            PenteController.TakeTurn(0, 1);
            PenteController.TakeTurn(0, 2);
            PenteController.TakeTurn(15, 15);
            PenteController.TakeTurn(0, 3);

            //Capture
            PenteController.TakeTurn(0, 4);

            var board = PenteController.game.Board;

            var actual = board[0, 2] == PieceColor.Empty && board[0, 3] == PieceColor.Empty;
            var expected = true;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FirstPlayerMove_WinningMoveHorizontal_GameOver()
        {
            PenteController.StartGame();

            //set up win
            PenteController.game.SetPieceAt(0, 0, PieceColor.Black);
            PenteController.game.SetPieceAt(0, 1, PieceColor.Black);
            PenteController.game.SetPieceAt(0, 2, PieceColor.Black);
            PenteController.game.SetPieceAt(0, 3, PieceColor.Black);

            //Make winning move
            PenteController.TakeTurn(0, 4);

            bool expected = true;
            bool actual = PenteController.game.IsGameOver;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FirstPlayerMove_WinningMoveDiagonal_GameOver()
        {
            //Set up win
            PenteController.game.SetPieceAt(0, 0, PieceColor.Black);
            PenteController.game.SetPieceAt(1, 1, PieceColor.Black);
            PenteController.game.SetPieceAt(2, 2, PieceColor.Black);
            PenteController.game.SetPieceAt(3, 3, PieceColor.Black);

            //Make winning move
            PenteController.TakeTurn(4, 4);

            bool expected = true;
            bool actual = PenteController.game.IsGameOver;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FirstPlayerMove_WinningMoveVertical_GameOver()
        {
            //set up win
            PenteController.game.SetPieceAt(0, 0, PieceColor.Black);
            PenteController.game.SetPieceAt(1, 0, PieceColor.Black);
            PenteController.game.SetPieceAt(2, 0, PieceColor.Black);
            PenteController.game.SetPieceAt(3, 0, PieceColor.Black);

            //Make winning move
            PenteController.TakeTurn(4, 0);

            bool expected = true;
            bool actual = PenteController.game.IsGameOver;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FirstPlayerMakes5thCapture_GameOver()
        {
            //Set up
            PenteController.StartGame();
            PenteController.game.FirstPlayerCaptures = 4;

            PenteController.game.SetPieceAt(0, 0, PieceColor.Black);
            PenteController.game.SetPieceAt(0, 1, PieceColor.White);
            PenteController.game.SetPieceAt(0, 2, PieceColor.White);

            //Capture
            PenteController.TakeTurn(0, 3);

            bool expected = true;
            bool actual = PenteController.game.IsGameOver;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SecondPlayerMakes5thCapture_GameOver()
        {
            //Set up
            PenteController.StartGame();
            PenteController.game.SecondPlayerCaptures = 4;

            PenteController.game.SetPieceAt(0, 0, PieceColor.White);
            PenteController.game.SetPieceAt(0, 1, PieceColor.Black);
            PenteController.game.SetPieceAt(0, 2, PieceColor.Black);
            PenteController.game.isFirstPlayersTurn = false;

            //Capture
            PenteController.TakeTurn(0, 3);

            bool expected = true;
            bool actual = PenteController.game.IsGameOver;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OutOfBoundsCheck()
        {
            PenteController.StartGame();

            bool expected = false;
            bool actual = PenteController.TakeTurn(-50, -50);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void Tria_Event()
        {
            PenteController.StartGame();

            //Set up
            PenteController.game.SetPieceAt(5, 5, PieceColor.Black);
            PenteController.game.SetPieceAt(5, 6, PieceColor.Black);

            //Make Tria move
            PenteController.TakeTurn(5, 7);

            bool expected = true;
            bool actual = PenteController.game.Tria;

            Assert.AreEqual(expected, actual);
        }
    }
}
