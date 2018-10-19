using PenteLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pente
{
    /// <summary>
    /// Interaction logic for WinWindow.xaml
    /// </summary>
    public partial class WinWindow : Window
    {
        PlayMode playMode;
        int boardSize;
        public WinWindow(PlayMode playMode, string WinnerName, int boardSize)
        {
            InitializeComponent();

            this.boardSize = boardSize;
            this.playMode = playMode;
            lblWin.Content = $"{WinnerName} Wins!";
        }

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            GameWindow window = new GameWindow(boardSize, playMode);
            window.Show();
            this.Close();
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
