using PenteLib.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenteLib.Models
{
    

    public class Pente : INotifyPropertyChanged
    {
        public bool Tria { get; set; }

        public bool Tessera { get; set; }

        private PieceColor[,] board;

        public event PropertyChangedEventHandler PropertyChanged;

        private bool isFirstPlayersTurn;

        public bool IsFirstPlayersTurn { get => isFirstPlayersTurn; set {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsFirstPlayersTurn"));
                isFirstPlayersTurn = value;
            }
        }

        public int FirstPlayerCaptures { get; set; }

        public int SecondPlayerCaptures { get; set; }
        private int turn;
        public int Turn
        {
            get => turn;
            set {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Turn"));
                turn = value;
            }
        }

        public bool IsGameOver { get; set; }
        public PieceColor[,] Board { get => board; set => board = value; }
        public PlayMode PlayMode { get; set; }

        public Pente(PlayMode playMode, int boardSize)
        {
            board = new PieceColor[boardSize, boardSize];

            PlayMode = playMode;
            IsFirstPlayersTurn = true;
            IsGameOver = false;
            FirstPlayerCaptures = 0;
            SecondPlayerCaptures = 0;
            Tria = false;
            Tessera = false;
            Turn = 1;
        }

        public PieceColor GetPieceAt(int row, int column)
        {
            return Board[row, column];
        }

        public PieceColor GetPieceAt(Point point)
        {
            return GetPieceAt(point.row, point.column);
        }

        public void SetPieceAt(int row, int column, PieceColor pieceColor)
        {
            Board[row, column] = pieceColor;
        }

        public void SetPieceAt(Point point, PieceColor pieceColor)
        {
            SetPieceAt(point.row, point.column, pieceColor);
        }
    }
}
