﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenteLib.Models
{
    

    public class Pente
    {
        public bool Tria { get; set; }

        public bool Tessera { get; set; }

        private PieceColor[,] board = new PieceColor[19,19];

        public bool isFirstPlayersTurn { get; set; }

        public int FirstPlayerCaptures { get; set; }

        public int SecondPlayerCaptures { get; set; }

        public bool IsGameOver { get; set; }
        public PieceColor[,] Board { get => board; set => board = value; }
        public PlayMode PlayMode { get; set; }

        public Pente(PlayMode playMode)
        {
            PlayMode = playMode;
            isFirstPlayersTurn = true;
            IsGameOver = false;
            FirstPlayerCaptures = 0;
            SecondPlayerCaptures = 0;
            Tria = false;
            Tessera = false;
        }

        public PieceColor GetPieceAt(int row, int column)
        {
            return Board[row, column];
        }

        public void SetPieceAt(int row, int column, PieceColor pieceColor)
        {
            Board[row, column] = pieceColor;
        }
    }
}
