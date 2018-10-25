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
    }
}
