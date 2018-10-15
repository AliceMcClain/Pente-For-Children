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
    }
}
