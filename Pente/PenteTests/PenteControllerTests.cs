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
        public void FirstPlayerTakeTurnTest()
        {
            int row = 1;
            int column = 1;

            PenteController.StartGame();
            PenteController.TakeTurn(row, column);

            PieceColor expected = PieceColor.Black;
            PieceColor actual = PenteController.game.GetPieceAt(row, column);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ChangePlayerTurnTest()
        {
            var expected = !PenteController.game.isFirstPlayersTurn;
            bool actual;

            PenteController.TakeTurn(1, 1);
            actual = PenteController.game.isFirstPlayersTurn;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetPieceTest()
        {
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
        public void CheckForWinnerTest()
        {
            PenteController.StartGame();

            //set up win
            PenteController.game.SetPieceAt(0, 0, PieceColor.Black);
            PenteController.game.SetPieceAt(0, 1, PieceColor.Black);
            PenteController.game.SetPieceAt(0, 2, PieceColor.Black);
            PenteController.game.SetPieceAt(0, 3, PieceColor.Black);

            PenteController.TakeTurn(0, 4);
            bool expected = true;
            bool actual = PenteController.game.IsGameOver;

            Assert.AreEqual(expected, actual);


        }
    }
}
