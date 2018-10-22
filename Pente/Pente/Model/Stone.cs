using PenteLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Pente
{
    public class Stone : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private BitmapImage _image;
        private PieceColor _stoneColor;

        private static BitmapImage blackStone = new BitmapImage(new Uri("../Resource/BlackCircle.png", UriKind.Relative));
        private static BitmapImage whiteStone = new BitmapImage(new Uri("../Resource/WhiteCircle.png", UriKind.Relative));
        private static BitmapImage emptyStone = new BitmapImage(new Uri("../Resource/EmptyCircle.png", UriKind.Relative));

        public BitmapImage Image
        {
            get => _image;
            set
            {
                _image = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Image"));
            }
        }

        public PieceColor PieceColor
        {
            get => _stoneColor;
            set
            {
                _stoneColor = value;

                if (value == PieceColor.Black)
                {
                    Image = blackStone;
                }
                else if (value == PieceColor.White)
                {
                    Image = whiteStone;
                }
                else
                {
                    Image = emptyStone;
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StoneColor"));
            }
        }
    }
}
