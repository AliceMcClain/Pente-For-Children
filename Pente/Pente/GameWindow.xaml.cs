using PenteLib.Controllers;
using PenteLib.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Timers;
using System.ComponentModel;

namespace Pente
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window, INotifyPropertyChanged
    {
        StoneBoard board;
        UniformGrid stoneGrid;
        private static int time = 0;
        private int boardSize;
        private Timer timer;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Time {
            get { return time; }
            set
            {
                time = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Time"));
            }
        }

        public GameWindow(int boardSize, PlayMode playMode)
        {
            InitializeComponent();
            Time = 20;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += UpdateTimer;
            timer.Start();

            Binding b = new Binding()
            {
                Path = new PropertyPath("Time"),
                Source = this
            };
            lblTime.SetBinding(ContentProperty, b);

            this.boardSize = boardSize;

            PenteController.StartGame(playMode, BoardSize: boardSize, isDebug: true);
            board = new StoneBoard(boardSize);

            
            SetGameSquares();
            SetUpStoneBoard();
            UpdateBoard();
           // AddRandomPieces();

        }

        private void UpdateTimer(object sender = null, ElapsedEventArgs e = null)
        {
            if(Time-- == 0)
            {
                PenteController.SkipTurn();
                Time = 20;
            }

        }

        //Temporary method just to show that we can add stones to the board
        private void AddRandomPieces()
        {
            PenteController.game.SetPieceAt(0, 0, PieceColor.Black);
            PenteController.game.SetPieceAt(0, 1, PieceColor.White);
            PenteController.game.SetPieceAt(0, 2, PieceColor.Black);
            PenteController.game.SetPieceAt(0, 3, PieceColor.White);
            PenteController.game.SetPieceAt(0, 4, PieceColor.Black);
            PenteController.game.SetPieceAt(0, 5, PieceColor.White);
            PenteController.game.SetPieceAt(10, 0, PieceColor.White);
            PenteController.game.SetPieceAt(10, 1, PieceColor.Black);
            PenteController.game.SetPieceAt(10, 2, PieceColor.White);
            PenteController.game.SetPieceAt(10, 3, PieceColor.Black);
            PenteController.game.SetPieceAt(10, 4, PieceColor.White);
            PenteController.game.SetPieceAt(0, 15, PieceColor.Black);

            UpdateBoard();
        }

        //Creates the Green squares you see
        private void SetGameSquares()
        {
            UniformGrid squares = new UniformGrid { Rows = boardSize-1, Columns = boardSize-1, Margin = new Thickness(25) };
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
            stoneGrid = new UniformGrid { Rows = boardSize, Columns = boardSize, Margin = new Thickness(11) };
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

                    stoneSpace.MouseDown += BoardPiece_Click;

                    stoneGrid.Children.Add(stoneSpace);
                }
            }

            MainGrid.Children.Add(stoneGrid);
        }

        private void UpdateBoard()
        {
            var penteBoard = PenteController.game.Board;
            for (int i = 0; i < penteBoard.GetLength(0); i++)
            {
                for (int j = 0; j < penteBoard.GetLength(1); j++)
                {
                    board[i, j].PieceColor = penteBoard[i, j];
                }
            }
        }

        private void BoardPiece_Click(object sender, MouseButtonEventArgs e)
        {
            int columns = stoneGrid.Columns;
            int index = stoneGrid.Children.IndexOf((StoneSpace)sender);

            int row = index / columns;
            int col = index % columns;

            bool validMove = PenteController.TakeTurn(row, col);
            if (validMove)
            {
                UpdateBoard();
                if (PenteController.game.IsGameOver)
                {
                    //Gets the name of the winner, which is the current player's turn
                    string winner = PenteController.game.isFirstPlayersTurn ? lblPlayer1Name.Content.ToString() : lblPlayer2Name.Content.ToString();
                    WinWindow winWindow = new WinWindow(PenteController.game.PlayMode, winner, boardSize);
                    winWindow.Show();
                    this.Close();
                }
                Time = 20;
            }
           
        }

        #region Player name editting
        private void Player1Name_LostFocus(object sender, RoutedEventArgs e)
        {
            //Checks if textbox is empty
            if (tbxPlayer1Name.Text.Length != 0)
            {
                lblPlayer1Name.Content = tbxPlayer1Name.Text;
            }
            else
            {
                tbxPlayer1Name.Text = "Player 1";
            }
            //Sets player 1 text box invisible and unusable after losing focus
            tbxPlayer1Name.IsEnabled = false;
            tbxPlayer1Name.Visibility = Visibility.Hidden;
            tbxPlayer1Name.Focusable = false;
        }

        private void Player1Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //Checks if textbox is empty
                if (tbxPlayer1Name.Text.Length != 0)
                {
                    lblPlayer1Name.Content = tbxPlayer1Name.Text;
                }
                else
                {
                    tbxPlayer1Name.Text = "Player 1";
                }
                //Sets player 1 text box invisible and unusable after clicking enter
                tbxPlayer1Name.IsEnabled = false;
                tbxPlayer1Name.Visibility = Visibility.Hidden;
                tbxPlayer1Name.Focusable = true;
            }
        }

        private void Player2Name_LostFocus(object sender, RoutedEventArgs e)
        {
            //Checks if textbox is empty
            if (tbxPlayer2Name.Text.Length != 0)
            {
                lblPlayer2Name.Content = tbxPlayer2Name.Text;
            }
            else
            {
                tbxPlayer2Name.Text = "Player 2";
            }
            //Sets player 2 text box invisible and unusable after losing focus
            tbxPlayer2Name.IsEnabled = false;
            tbxPlayer2Name.Visibility = Visibility.Hidden;
            tbxPlayer2Name.Focusable = false;
        }

        private void Player2Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //Checks if textbox is empty
                if (tbxPlayer2Name.Text.Length != 0)
                {
                    lblPlayer2Name.Content = tbxPlayer2Name.Text;
                }
                else
                {
                    tbxPlayer2Name.Text = "Player 2";
                }
                //Sets player 2 text box invisible and unusable after clicking enter
                tbxPlayer2Name.IsEnabled = false;
                tbxPlayer2Name.Visibility = Visibility.Hidden;
                tbxPlayer2Name.Focusable = false;
            }
        }

        private void Player2NameEdit_Click(object sender, MouseButtonEventArgs e)
        {
            //When pencil is clicked textbox becomes available 
            tbxPlayer2Name.IsEnabled = true;
            tbxPlayer2Name.Visibility = Visibility.Visible;
            tbxPlayer2Name.Focusable = true;
            tbxPlayer2Name.Focus();
        }

        private void Player1NameEdit_Click(object sender, MouseButtonEventArgs e)
        {
            //When pencil is clicked textbox becomes available 
            tbxPlayer1Name.IsEnabled = true;
            tbxPlayer1Name.Visibility = Visibility.Visible;
            tbxPlayer1Name.Focusable = true;
            tbxPlayer1Name.Focus();
        }
        #endregion
    }
}
