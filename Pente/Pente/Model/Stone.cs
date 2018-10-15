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

        private static BitmapImage blueStone = new BitmapImage(new Uri("../Resource/BlueCircle.png", UriKind.Relative));
        private static BitmapImage yellowStone = new BitmapImage(new Uri("../Resource/YellowCircle.png", UriKind.Relative));

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
                    Image = blueStone;
                }
                else if (value == PieceColor.White)
                {
                    Image = yellowStone;
                }
                else
                {
                    Image = new BitmapImage();
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StoneColor"));
            }
        }
    }
}
