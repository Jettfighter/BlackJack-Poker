using CardGames.CardGames_UCs.GameModesUC;
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
using CardGameLib.Controllers;

namespace CardGames.CardGames_UCs
{
    /// <summary>
    /// Interaction logic for GamePlayUC.xaml
    /// </summary>
    public partial class GamePlayUC : UserControl
    {
        internal MainWindow window;
        //BlackjackUC blackjack = new BlackjackUC();
        //PokerUC poker = new PokerUC();
        GoFishUC goFish = new GoFishUC();
        WarUC war = new WarUC();
        public List<string> names = new List<string>();

        public GamePlayUC()
        {
            InitializeComponent();

            //Temporary example from PokerUC (displays on the GamePlayUC):
            //gCardArea.Children.Add(poker);
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            //window.MainMenu();
        }

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            //window.Go();
        }

        public void NewGame(string game)
        {
            //Insert code to create a new game depending on which game is being played
            switch (game)
            {
                case "Blackjack":
                    //BlackJackController.StartGame(names.ToArray());
                    break;

                case "Poker":
                    //poker.NewGame(names);
                    break;

                case "GoFish":

                    break;

                case "War":

                    break;
            }
        }
    }
}
