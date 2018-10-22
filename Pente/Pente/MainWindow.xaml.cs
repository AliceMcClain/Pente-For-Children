using PenteLib.Controllers;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PenteController.InstructionsOpen = false;
        }
        private void Instructions_Click(object sender, RoutedEventArgs e)
        {
            if(!PenteController.InstructionsOpen )
            {
                PenteController.InstructionsOpen = true;
                InstructionsWindow window = new InstructionsWindow();
                window.Show();
            }
            
        }
        private void Single_Click(object sender, RoutedEventArgs e)
        {
            GameWindow window = new GameWindow((int)sldrBoardSize.Value, PenteLib.Models.PlayMode.SinglePlayer);
            window.Show();
            this.Close();
        }
        private void Multi_Click(object sender, RoutedEventArgs e)
        {
            GameWindow window = new GameWindow((int)sldrBoardSize.Value, PenteLib.Models.PlayMode.MultiPlayer);
            window.Show();
            this.Close();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
