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
    /// Interaction logic for NameSelectUC.xaml
    /// </summary>
    public partial class NameSelectUC : UserControl
    {
        internal MainWindow window;

        public NameSelectUC()
        {
            InitializeComponent();
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            window.MainMenu();
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            window.Go();
        }
    }
}
