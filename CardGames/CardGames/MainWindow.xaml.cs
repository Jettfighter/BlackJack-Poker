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
            string player1 = nameSelect.tbxP1_Name.Text;
            string player2 = nameSelect.tbxP2_Name.Text;
            string player3 = nameSelect.tbxP3_Name.Text;
            string player4 = nameSelect.tbxP4_Name.Text;
            string player5 = nameSelect.tbxP5_Name.Text;

            ShowGameArea();
        }

        //Takes user to the gameplay screen
        private void ShowGameArea()
        {
            gameOver.Visibility = Visibility.Hidden;
            nameSelect.Visibility = Visibility.Hidden;
            gamePlay.Visibility = Visibility.Visible;
        }
    }
}
