using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pente
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StoneBoard board;

        public MainWindow()
        {
            InitializeComponent();
            board = new StoneBoard();
            SetGameSquares();
            SetUpStoneBoard();
            AddRandomPieces();
        }

        //Temporary method just to show that we can add stones to the board
        private void AddRandomPieces()
        {
            board[0, 0].StoneColor = StoneColor.BLUE;
            board[0, 1].StoneColor = StoneColor.YELLOW;
            board[0, 2].StoneColor = StoneColor.BLUE;
            board[0, 3].StoneColor = StoneColor.YELLOW;
            board[0, 4].StoneColor = StoneColor.BLUE;
            board[0, 5].StoneColor = StoneColor.YELLOW;


            board[10, 0].StoneColor = StoneColor.YELLOW;
            board[10, 1].StoneColor = StoneColor.BLUE;
            board[10, 2].StoneColor = StoneColor.BLUE;
            board[10, 3].StoneColor = StoneColor.YELLOW;
            board[10, 4].StoneColor = StoneColor.BLUE;
            board[0, 15].StoneColor = StoneColor.YELLOW;

        }

        //Creates the Green squares you see
        private void SetGameSquares()
        {
            UniformGrid squares = new UniformGrid { Rows = 18, Columns = 18, Margin = new Thickness(25) };
            SolidColorBrush color = new SolidColorBrush(Colors.Green);
            Rectangle square;

            int numberOfSquares = squares.Rows * squares.Columns;

            for (int i = 0; i < numberOfSquares; i++)
            {
                square = new Rectangle
                {
                    StrokeThickness = 1,
                    Margin = new Thickness(1),
                    Fill = color
                };

                squares.Children.Add(square);
            }

            MainGrid.Children.Add(squares);
        }

        //Creats a board to set the stones on.
        private void SetUpStoneBoard()
        {
            UniformGrid stoneGrid = new UniformGrid { Rows = 19, Columns = 19, Margin = new Thickness(11) };
            StoneSpace stoneSpace;
            Binding imageBinding;

            int stoneRows = stoneGrid.Rows;
            int stoneColumns = stoneGrid.Columns;
            
            for (int i = 0; i < stoneRows; i++)
            {
                for (int j = 0; j < stoneColumns; j++)
                {
                    stoneSpace = new StoneSpace
                    {
                        Margin = new Thickness(1)
                    };

                    imageBinding = new Binding
                    {
                        Path = new PropertyPath("Image"),
                        Source = board[i, j],
                    };

                    stoneSpace.CircleImage.SetBinding(Image.SourceProperty, imageBinding);
                    stoneGrid.Children.Add(stoneSpace);
                }
            }

            MainGrid.Children.Add(stoneGrid);
        }
    }
}
