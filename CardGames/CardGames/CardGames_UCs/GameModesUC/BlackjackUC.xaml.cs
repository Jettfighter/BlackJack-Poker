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
        public List<StackPanel> PlayerContinuedHandDisplays = new List<StackPanel>();

        public List<Label> PlayerBustDisplays = new List<Label>();

        public List<Label> PlayerTotalDisplays = new List<Label>();

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
            //UpdateHands();
            AddBackOfCardsToAllPlayers();
        }

        private void ResetPlayers()
        {
            for (int i = 5; i > NumPlayers; i--)
            {
                PlayerNameDisplays[i - 1].Content = "";
                PlayerBankDisplays[i - 1].Content = "";
                PlayerHandDisplays[i - 1].Children.Clear();
                PlayerBustDisplays[i - 1].Content = "";
                PlayerTotalDisplays[i - 1].Content = "";

            }

            for (int i = 0; i < NumPlayers; i++)
            {
                PlayerNameDisplays[i].Content = Players[i].Name;
                PlayerBankDisplays[i].Content = $"${Players[i].Bank}";
                PlayerBustDisplays[i].Content = "";
                PlayerTotalDisplays[i].Content = "";
            }

        }
        private void BetButton_Click(object sender, RoutedEventArgs e)
        {
            int bet = 0;
            if (((Button)sender).Content.ToString() == "$1")
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
            if (BlackJackController.CanDoubleDown(playerName))
            {
                DoubleDownButton.Visibility = Visibility.Visible;
            }
            if (BlackJackController.CanSplitPairs(playerName))
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
            DisplayCurrentHand();
            PlayerTotalDisplays[playerTurn].Content = $":{BlackJackController.GetTotal(GetCurrentPlayerName(),false)}";
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

            PlayerBustDisplays.Add(Player1Busted);
            PlayerBustDisplays.Add(Player2Busted);
            PlayerBustDisplays.Add(Player3Busted);
            PlayerBustDisplays.Add(Player4Busted);
            PlayerBustDisplays.Add(Player5Busted);

            PlayerContinuedHandDisplays.Add(Player1HandContinued);
            PlayerContinuedHandDisplays.Add(Player2HandContinued);
            PlayerContinuedHandDisplays.Add(Player3HandContinued);
            PlayerContinuedHandDisplays.Add(Player4HandContinued);
            PlayerContinuedHandDisplays.Add(Player5HandContinued);

            PlayerTotalDisplays.Add(Player1Total);
            PlayerTotalDisplays.Add(Player2Total);
            PlayerTotalDisplays.Add(Player3Total);
            PlayerTotalDisplays.Add(Player4Total);
            PlayerTotalDisplays.Add(Player5Total);
        }

        private void AddBackOfCardsToAllPlayers()
        {
            string currentDir = Environment.CurrentDirectory;
            List<char> fixedPath = currentDir.ToList();
            fixedPath.RemoveRange(currentDir.Count() - 9, 9);
            string path = "";
            foreach (char c in fixedPath)
            {
                path += c;
            }
            path += "Resources\\green_back.png";
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            b.EndInit();

            for (int i = 0; i < NumPlayers; i++)
            {
                PlayerHandDisplays[i].Children.Clear();

                int numCards = BlackJackController.blackjack.GetAllPlayers()[i].Hand.Count;
                for (int j = 0; j < numCards; j++)
                {
                    Image pic = new Image();
                    pic.Source = b;
                    pic.Height = 60;
                    PlayerHandDisplays[i].Children.Add(pic);
                }
            }
            Image pic1 = new Image();
            pic1.Source = b;
            pic1.Height = 60;
            DealerHand.Children.Add(pic1);
            Image pic2 = new Image();
            pic2.Source = b;
            pic2.Height = 60;
            DealerHand.Children.Add(pic2);
        }

        public void UpdateHands()
        {
            for (int i = 0; i < NumPlayers; i++)
            {
                PlayerHandDisplays[i].Children.Clear();
                
                string name = PlayerNameDisplays[i].Content.ToString();
                int total = BlackJackController.GetTotal(name, false);
                PlayerTotalDisplays[i].Content = $":{total}";
                if(BlackJackController.IsBusted(name,false))
                {
                    PlayerBustDisplays[i].Content = "Busted";
                }
                var hand = BlackJackController.blackjack.GetPlayer(PlayerNameDisplays[i].Content.ToString()).Hand;
                foreach (var card in hand)
                {
                    var pic = ConvertToPicture(card);
                    PlayerHandDisplays[i].Children.Add(pic);
                }
            }
        }

        private Image ConvertToPicture(Card card)
        {
            Image pic = new Image();

            string extention = "";
            if (card.Value == ':')
            {
                extention += "10";
            }
            else
            {
                extention += card.Value;
            }
            extention += card.Suit[0];
            Console.WriteLine(extention);
            string currentDir = Environment.CurrentDirectory;
            List<char> fixedPath = currentDir.ToList();
            fixedPath.RemoveRange(currentDir.Count() - 9, 9);
            string path = "";
            foreach (char c in fixedPath)
            {
                path += c;
            }
            path += "Resources\\" + extention + ".png";
            Console.WriteLine(path);
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            b.EndInit();
            Console.WriteLine(b.UriSource);
            pic.Source = b;
            pic.Height = 60;

            //test, remove later
            //DealerHand.Children.Add(pic);

            return pic;
        }

        private void StandButton_Click(object sender, RoutedEventArgs e)
        {
            playerTurn--;

            if (playerTurn < 0)
            {
                HitButton.Visibility = Visibility.Hidden;
                DoubleDownButton.Visibility = Visibility.Hidden;
                StandButton.Visibility = Visibility.Hidden;
                SplitButton.Visibility = Visibility.Hidden;

                TurnLabel.Content = "Turn:";
                UpdateHands();
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
                HideCurrentHand(false);
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
            HideCurrentHand(true);
        }

        private void HitButton_Click(object sender, RoutedEventArgs e)
        {
            DoubleDownButton.Visibility = Visibility.Hidden;
            SplitButton.Visibility = Visibility.Hidden;

            string playerName = GetCurrentPlayerName();
            BlackJackController.HitMe(playerName, false);
            if (BlackJackController.IsBusted(playerName, false))
            {
                HitButton.Visibility = Visibility.Hidden;
                PlayerBustDisplays[playerTurn].Content = "Busted";
            }
            //UpdateHands();
            DisplayCurrentHand();
            PlayerTotalDisplays[playerTurn].Content = $":{BlackJackController.GetTotal(GetCurrentPlayerName(), false)}";
        }


        private void DisplayCurrentHand()
        {
            PlayerHandDisplays[playerTurn].Children.Clear();
            PlayerContinuedHandDisplays[playerTurn].Children.Clear();

            var hand = BlackJackController.blackjack.GetPlayer(PlayerNameDisplays[playerTurn].Content.ToString()).Hand;
            int count = 0;
            foreach (var card in hand)
            {
                count++;
                var pic = ConvertToPicture(card);
                if (count <= 3)
                {

                    PlayerHandDisplays[playerTurn].Children.Add(pic);
                }
                else
                {
                    PlayerContinuedHandDisplays[playerTurn].Children.Add(pic);

                }
            }
        }

        private void HideCurrentHand(bool IsDoubleDown)
        {
            playerTurn++;
            PlayerHandDisplays[playerTurn].Children.Clear();
            if (IsDoubleDown)
            {
                string currentDir = Environment.CurrentDirectory;
                List<char> fixedPath = currentDir.ToList();
                fixedPath.RemoveRange(currentDir.Count() - 9, 9);
                string path = "";
                foreach (char c in fixedPath)
                {
                    path += c;
                }
                path += "Resources\\green_back.png";
                BitmapImage b = new BitmapImage();
                b.BeginInit();
                b.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                b.EndInit();
                int numCards = BlackJackController.blackjack.GetAllPlayers()[playerTurn].Hand.Count;
                for (int j = 0; j < numCards; j++)
                {
                    Image pic = new Image();
                    pic.Source = b;
                    pic.Height = 60;
                    PlayerHandDisplays[playerTurn].Children.Add(pic);
                }
            }
            else
            {
                string currentDir = Environment.CurrentDirectory;
                List<char> fixedPath = currentDir.ToList();
                fixedPath.RemoveRange(currentDir.Count() - 9, 9);
                string path = "";
                foreach (char c in fixedPath)
                {
                    path += c;
                }
                path += "Resources\\green_back.png";
                BitmapImage b = new BitmapImage();
                b.BeginInit();
                b.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                b.EndInit();
                int numCards = BlackJackController.blackjack.GetAllPlayers()[playerTurn].Hand.Count;
                for (int j = 0; j < 2; j++)
                {
                    Image pic = new Image();
                    pic.Source = b;
                    pic.Height = 60;
                    PlayerHandDisplays[playerTurn].Children.Add(pic);
                }
                var hand = BlackJackController.blackjack.GetPlayer(PlayerNameDisplays[playerTurn].Content.ToString()).Hand;
                if (hand.Count >= 3)
                {
                    var pic = ConvertToPicture(hand[2]);
                    PlayerHandDisplays[playerTurn].Children.Add(pic);
                }
            }
            playerTurn--;
        }

        private string GetCurrentPlayerName()
        {
            return PlayerNameDisplays[playerTurn].Content.ToString();
        }
    }
}
