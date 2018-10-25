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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CardGames.CardGames_UCs
{
    /// <summary>
    /// Interaction logic for GamePlayUC.xaml
    /// </summary>
    public partial class GamePlayUC : UserControl
    {
        internal MainWindow window;

        public GamePlayUC()
        {
            InitializeComponent();
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            window.MainMenu();
        }

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            window.Go();
        }
    }
}
