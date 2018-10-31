using CardGameLib.Controllers;
using CardGameLib.Models;
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

namespace CardGames.CardGames_UCs.GameModesUC
{
    /// <summary>
    /// Interaction logic for BlackjackUC.xaml
    /// </summary>
    public partial class BlackjackUC : UserControl, IButtonClicked
    {
        public event ButtonClicked GameButtonClicked;

        public List<Label> PlayerNameDisplays = new List<Label>();

        public List<Label> PlayerBankDisplays = new List<Label>();

        public List<StackPanel> PlayerHandDisplays = new List<StackPanel>();

        private int NumPlayers = BlackJackController.blackjack.GetAllPlayers().Count();

        private int playerTurn = 4;

        Player[] Players = BlackJackController.blackjack.GetAllPlayers();

        public BlackjackUC(List<string> names)
        {
            InitializeComponent();
        }

        private void UC_Loaded(object sender, RoutedEventArgs e)
        {
            PreparePlayerElements();
            ResetPlayers();

            //ConvertToPicture(new Card('2', "Hearts"));
            playerTurn = NumPlayers - 1;
            TurnLabel.Content = $"Turn: {GetCurrentPlayerName()}";
        }

        private void ResetPlayers()
        {
            for (int i = 5; i > NumPlayers; i--)
            {
                PlayerNameDisplays[i - 1].Content = "";
                PlayerBankDisplays[i - 1].Content = "";
                PlayerHandDisplays[i - 1].Children.Clear();
                
            }

            for (int i = 0; i < NumPlayers; i++)
            {
                PlayerNameDisplays[i].Content = Players[i].Name;
                PlayerBankDisplays[i].Content = $"${Players[i].Bank}";
            }
            //TODO: Update bet amount - OPTIONAL
        }
        private void BetButton_Click(object sender, RoutedEventArgs e)
        {
            int bet = 0;
            if(((Button)sender).Content.ToString()=="$1")
            {
                bet = 1;
            }
            else if (((Button)sender).Content.ToString() == "$5")
            {
                bet = 5;
            }
            else if (((Button)sender).Content.ToString() == "$10")
            {
                bet = 10;
            }
            string playerName = GetCurrentPlayerName();
            PlayerBankDisplays[playerTurn].Content = $"${BlackJackController.PlayerBet(playerName, bet)}";
            HitButton.Visibility = Visibility.Visible;
            StandButton.Visibility = Visibility.Visible;
            if(BlackJackController.CanDoubleDown(playerName))
            {
                DoubleDownButton.Visibility = Visibility.Visible;
            }
            if(BlackJackController.CanSplitPairs(playerName))
            {
                SplitButton.Visibility = Visibility.Visible;
            }

            HitButton.IsEnabled = true;
            DoubleDownButton.IsEnabled = true;
            StandButton.IsEnabled = true;
            SplitButton.IsEnabled = true;

            D1Button.Visibility = Visibility.Hidden;
            D5Button.Visibility = Visibility.Hidden;
            D10Button.Visibility = Visibility.Hidden;
        }

        private void PreparePlayerElements()
        {
            PlayerNameDisplays.Add(Player1Name);
            PlayerNameDisplays.Add(Player2Name);
            PlayerNameDisplays.Add(Player3Name);
            PlayerNameDisplays.Add(Player4Name);
            PlayerNameDisplays.Add(Player5Name);

            PlayerBankDisplays.Add(Player1Bank);
            PlayerBankDisplays.Add(Player2Bank);
            PlayerBankDisplays.Add(Player3Bank);
            PlayerBankDisplays.Add(Player4Bank);
            PlayerBankDisplays.Add(Player5Bank);

            PlayerHandDisplays.Add(Player1Hand);
            PlayerHandDisplays.Add(Player2Hand);
            PlayerHandDisplays.Add(Player3Hand);
            PlayerHandDisplays.Add(Player4Hand);
            PlayerHandDisplays.Add(Player5Hand);

        }
        
        public void UpdateHands()
        {
            for (int i = 0; i < NumPlayers; i++)
            {
                PlayerHandDisplays[i].Children.Clear();

                int numCards = BlackJackController.blackjack.GetAllPlayers()[i].Hand.Count;
                for (int j = 0; j < numCards; j++)
                {
                     var pic = ConvertToPicture(BlackJackController.blackjack.GetAllPlayers()[i].Hand[j]);
                    PlayerHandDisplays[i].Children.Add(pic);
                }
            }
        }

        private Image ConvertToPicture(Card card)
        {
            Image pic = new Image();

            string extention = "";

            extention += card.Value;
            extention += card.Suit[0];
            Console.WriteLine(extention);

            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri($"C:\\Source\\BlackJack-Poker\\CardGames\\CardGames\\Resources\\{extention}.png", UriKind.RelativeOrAbsolute);
            b.EndInit();

            pic.Source = b;
            pic.Height = 80;

            //test, remove later
            DealerHand.Children.Add(pic);

            return pic;
        }

        private void StandButton_Click(object sender, RoutedEventArgs e)
        {
            playerTurn--;

            if(playerTurn<0)
            {
                HitButton.Visibility = Visibility.Hidden;
                DoubleDownButton.Visibility = Visibility.Hidden;
                StandButton.Visibility = Visibility.Hidden;
                SplitButton.Visibility = Visibility.Hidden;

                TurnLabel.Content = "Turn:";
            }
            else
            {
                HitButton.Visibility = Visibility.Hidden;
                DoubleDownButton.Visibility = Visibility.Hidden;
                StandButton.Visibility = Visibility.Hidden;
                SplitButton.Visibility = Visibility.Hidden;

                D1Button.Visibility = Visibility.Visible;
                D5Button.Visibility = Visibility.Visible;
                D10Button.Visibility = Visibility.Visible;

                TurnLabel.Content = $"Turn: {PlayerNameDisplays[playerTurn].Content.ToString()}";
            }

            
            
        }

        private void SplitButton_Click(object sender, RoutedEventArgs e)
        {
            string playerName = GetCurrentPlayerName();

        }

        private void DoubleDownButton_Click(object sender, RoutedEventArgs e)
        {
            string playerName = GetCurrentPlayerName();
            BlackJackController.DoublingDown(playerName);
            StandButton_Click(sender, e);
        }

        private void HitButton_Click(object sender, RoutedEventArgs e)
        {
            DoubleDownButton.Visibility = Visibility.Hidden;
            SplitButton.Visibility = Visibility.Hidden;

            string playerName = GetCurrentPlayerName();
            BlackJackController.HitMe(playerName, false);
            if(BlackJackController.IsBusted(playerName,false))
            {
                HitButton.Visibility = Visibility.Hidden;
            }
        }

        private string GetCurrentPlayerName()
        {
            return PlayerNameDisplays[playerTurn].Content.ToString();
        }
    }
}
