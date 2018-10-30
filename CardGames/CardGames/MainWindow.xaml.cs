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
using CardGames.CardGames_UCs.GameModesUC;

namespace CardGames
{
    enum Game
    {
        BlackJack, Poker
    }

    public partial class MainWindow : Window
    {
        private List<string> Names;

        private Game game;

        public MainWindow()
        {
            InitializeComponent();
            AddUserControl(new TitleScreenUC());
        }

        public void AddUserControl(IButtonClicked userControl, bool IsGameUserControl = false)
        {
            UserControl userControlToAdd = null;
            MainGrid.Children.Clear();

            if (IsGameUserControl)
            {
                var gamePlay = new GamePlayUC();
                gamePlay.GameButtonClicked += GameButtonClicked;
                gamePlay.gCardArea.Children.Add(userControl as UserControl);
                userControlToAdd = gamePlay;
            }
            else if (userControl.GetType() == typeof(NameSelectUC))
            {
                ((NameSelectUC)userControl).GameStarting += StartGame;
                userControlToAdd = userControl as UserControl;
            }
            else
            {
                userControlToAdd = userControl as UserControl;
            }

            userControl.GameButtonClicked += GameButtonClicked;
            MainGrid.Children.Add(userControlToAdd);
        }

        private void StartGame(List<string> names)
        {
            switch (game)
            {
                case Game.BlackJack:
                    AddUserControl(new BlackjackUC(names), true);
                    break;
                case Game.Poker:
                    AddUserControl(new PokerUC(names), true);
                    break;

            }
        }
        private void GameButtonClicked(string sender)
        {
            switch (sender)
            {
                case "btnBlackjack":
                    AddUserControl(new NameSelectUC(min: 1, max: 5));
                    game = Game.BlackJack;
                    break;
                case "btnPoker":
                    AddUserControl(new NameSelectUC(min: 2, max: 4));
                    game = Game.Poker;
                    break;
                case "btnBack":
                case "mainMenu":
                    AddUserControl(new TitleScreenUC());
                    break;
            }

        }
    }
}
