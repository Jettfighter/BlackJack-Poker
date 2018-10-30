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

        public int NumPlayers = BlackJackController.blackjack.GetAllPlayers().Count();

        Player[] Players = BlackJackController.blackjack.GetAllPlayers();

        public BlackjackUC(List<string> names)
        {
            InitializeComponent();
        }

        private void UC_Loaded(object sender, RoutedEventArgs e)
        {
            PreparePlayerElements();
            ResetPlayers();

            ConvertToPicture(new Card('2', "Hearts"));
        }

        private void ResetPlayers()
        {
            for (int i = 5; i < NumPlayers; i--)
            {
                PlayerNameDisplays[i - 1].Content = "";
                PlayerBankDisplays[i - 1].Content = "";
                PlayerHandDisplays[i - 1].Children.Clear();

                /*
                 * trying a different way of clearing players
                var NameElement = PlayerNameDisplays[i - 1];
                ((Grid)NameElement.Parent).Children.Remove(NameElement);

                var BankElement = PlayerBankDisplays[i - 1];
                ((Grid)BankElement.Parent).Children.Remove(BankElement);

                var HandElement = PlayerHandDisplays[i - 1];
                ((Grid)HandElement.Parent).Children.Remove(HandElement);
                */
            }

            for (int i = 0; i < NumPlayers; i++)
            {
                PlayerNameDisplays[i].Content = Players[i].Name;
                PlayerBankDisplays[i].Content = $"${Players[i].Bank}";
            }
            //TODO: Update bet amount - OPTIONAL
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
    }
}
