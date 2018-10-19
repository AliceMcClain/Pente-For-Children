using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenteLib.Models
{
    public enum PlayMode
    {
        SinglePlayer,
        MultiPlayer
    }

    public enum PieceColor
    {
        Empty,
        White,
        Black
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight
    }
}
