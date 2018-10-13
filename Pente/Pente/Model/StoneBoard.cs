using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pente
{
    public class StoneBoard
    {
        private Stone[,] board;
        private int rows = 20;
        private int columns = 20;
        
        //puts Stones in the 2D array
        public StoneBoard()
        {
            board = new Stone[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    board[i, j] = new Stone();
                }
            }
        }

        public Stone this[int row, int column]
        {
            get { return board [row, column]; }
            set { board[row, column] = value; }
        }
    }
}
