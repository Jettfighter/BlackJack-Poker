using CardGames.CardGames_UCs;
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
using System.Linq;
using CardGames.CardGames_UCs.GameModesUC;

namespace CardGames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TitleScreenUC titleScreen = new TitleScreenUC();
        NameSelectUC nameSelect = new NameSelectUC();
        GamePlayUC gamePlay = new GamePlayUC();
        GameOverUC gameOver = new GameOverUC();
        string gameMode = "Blackjack";
        SolidColorBrush selected = new SolidColorBrush(Color.FromRgb(144, 209, 209));
        SolidColorBrush unselected = new SolidColorBrush(Color.FromRgb(76, 127, 127));
        BlackjackUC blackjack = new BlackjackUC();
        PokerUC poker = new PokerUC();
        GoFishUC goFish = new GoFishUC();
        WarUC war = new WarUC();

        public MainWindow()
        {
            InitializeComponent();
            mainWindow.Children.Add(titleScreen);
            mainWindow.Children.Add(nameSelect);
            mainWindow.Children.Add(gamePlay);
            mainWindow.Children.Add(gameOver);
            nameSelect.Visibility = Visibility.Hidden;
            gamePlay.Visibility = Visibility.Hidden;
            gameOver.Visibility = Visibility.Hidden;
            blackjack.Visibility = Visibility.Hidden;
            poker.Visibility = Visibility.Hidden;
            goFish.Visibility = Visibility.Hidden;
            war.Visibility = Visibility.Hidden;
            titleScreen.window = this;
            nameSelect.window = this;
            gamePlay.window = this;
            gameOver.window = this;
        }

        //Takes User to Name Select Screen
        internal void StartGame()
        {
            titleScreen.Visibility = Visibility.Hidden;
            nameSelect.Visibility = Visibility.Visible;
            switch (gameMode)
            {
                case "Blackjack":
                    blackjack.Visibility = Visibility.Visible;
                    gamePlay.gCardArea.Children.Clear();
                    gamePlay.gCardArea.Children.Add(blackjack);
                    break;

                case "Poker":
                    poker.Visibility = Visibility.Visible;
                    gamePlay.gCardArea.Children.Clear();
                    gamePlay.gCardArea.Children.Add(poker);
                    break;

                case "GoFish":
                    goFish.Visibility = Visibility.Visible;
                    gamePlay.gCardArea.Children.Clear();
                    gamePlay.gCardArea.Children.Add(goFish);
                    break;

                case "War":
                    war.Visibility = Visibility.Visible;
                    gamePlay.gCardArea.Children.Clear();
                    gamePlay.gCardArea.Children.Add(war);
                    break;
            }
        }

        //Returns user to Main Menu screen.
        internal void MainMenu()
        {
            titleScreen.Visibility = Visibility.Visible;
            nameSelect.Visibility = Visibility.Hidden;
            gamePlay.Visibility = Visibility.Hidden;
            gameOver.Visibility = Visibility.Hidden;
        }

        //Generates game with parameter and takes user to game screen
        internal void Go()
        {
            TextBox[] p = new TextBox[5];
            for (int i = 0; i < 5; i++)
            {
                p[i] = (TextBox)nameSelect.spPlayers.FindName($"tbxP{i + 1}Name");
            }   

            ShowGameArea();
        }

        //Takes user to the gameplay screen
        private void ShowGameArea()
        {
            gameOver.Visibility = Visibility.Hidden;
            nameSelect.Visibility = Visibility.Hidden;
            gamePlay.Visibility = Visibility.Visible;
        }

        //Displays the winner when the game ends.
        //internal void GameOver(Player currentPlayer)
        internal void GameOver()
        {
            gamePlay.Visibility = Visibility.Hidden;
            gameOver.Visibility = Visibility.Visible;
            //gameOver.lb_Winner.Content = $"{currentPlayer.Name} wins!";
        }

        internal void GameMode(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Name == "btnBlackjack")
            {
                titleScreen.btnBlackjack.Background = selected;
                titleScreen.btnPoker.Background = unselected;
                titleScreen.btnGoFish.Background = unselected;
                titleScreen.btnWar.Background = unselected;
                gameMode = "Blackjack";

            }
            else if (button.Name == "btnPoker")
            {
                titleScreen.btnBlackjack.Background = unselected;
                titleScreen.btnPoker.Background = selected;
                titleScreen.btnGoFish.Background = unselected;
                titleScreen.btnWar.Background = unselected;
                gameMode = "Poker";
            }
            else if (button.Name == "btnGoFish")
            {
                titleScreen.btnBlackjack.Background = unselected;
                titleScreen.btnPoker.Background = unselected;
                titleScreen.btnGoFish.Background = selected;
                titleScreen.btnWar.Background = unselected;
                gameMode = "GoFish";
            }
            else
            {
                titleScreen.btnBlackjack.Background = unselected;
                titleScreen.btnPoker.Background = unselected;
                titleScreen.btnGoFish.Background = unselected;
                titleScreen.btnWar.Background = selected;
                gameMode = "War";
            }
        }
    }
}
