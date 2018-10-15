using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Pente
{
    public enum StoneColor
    {
        BLACK, WHITE
    }

    public class Stone : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private BitmapImage _image;
        private StoneColor _stoneColor;

        private BitmapImage blueStone = new BitmapImage(new Uri("../Resource/BlueCircle.png", UriKind.Relative));
        private BitmapImage yellowStone = new BitmapImage(new Uri("../Resource/YellowCircle.png", UriKind.Relative));

        public BitmapImage Image
        {
            get => _image;
            set
            {
                _image = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Image"));
            }
        }

        public StoneColor StoneColor
        {
            get => _stoneColor;
            set
            {
                _stoneColor = value;

                if (value == StoneColor.BLUE)
                    Image = blueStone;
                else
                    Image = yellowStone;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StoneColor"));
            }
        }
    }
}
