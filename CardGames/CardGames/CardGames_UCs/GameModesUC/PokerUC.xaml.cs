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
using CardGameLib.Models;

namespace CardGames.CardGames_UCs.GameModesUC
{
    /// <summary>
    /// Interaction logic for PokerUC.xaml
    /// </summary>

    public partial class PokerUC : UserControl, IButtonClicked
    {
        public event ButtonClicked GameButtonClicked;
        private bool Flop;
        private bool Turn;
        private bool River;
        private bool flopCardsShowing = false;
        private bool turnCardShowing = false;
        private bool riverCardShowing = false;

        private bool firstBet = true;

        public PokerUC(List<string> names)
        {
            InitializeComponent();
            NewGame(names);
        }

        public void NewGame(List<string> names)
        {
            PokerController.NewGame(names);
            UpdateGameStats();
            UpdateStatusBar(PokerController.CurrentPlayer.Name + " make a bet");
            DisableOrEnableButtons();
        }

        private void DisableOrEnableButtons()
        {
            btnCall.IsEnabled = !btnCall.IsEnabled;
            btnRaise.IsEnabled = !btnRaise.IsEnabled;
            btnFold.IsEnabled = !btnFold.IsEnabled;
        }

        private void UpdateStatusBar(string status)
        {
            txtBoxStatus.Text = status;
        }

        private void UpdateGameStats()
        {
            lbBank.Content = $"Amount in Bank: {PokerController.GetBank()}.";
            lbPot.Content = $"Amount in Pot: {PokerController.GetPot()}.";
        }

        private void NextPhase()
        {
            if (PokerController.NextPhase());
            //if (PokerController.NextPhase())
            //{
            //    if (Flop)
            //    {

            //    }
            //    else if (Turn)
            //    {

            //    }
            //    else if (River)
            //    {

            //    }
            //}

        }

        private void Bet_Click(object sender, RoutedEventArgs e)
        {
            if (sBetAmount.Value < 0)
            {
                UpdateStatusBar("Bet must be greter than 0, Try again");
            }

            if (PokerController.Bet((int)sBetAmount.Value))
            {
                if (firstBet)
                {
                    DisableOrEnableButtons();
                    firstBet = false;
                }

                UpdateStatusBar("Betting Successful!");
                UpdateGameStats();
                NextPlayerTurn();
            }
            else
            {
                UpdateStatusBar("Don't have enough funds!, Try again!");
            }
        }

        private void NextPlayerTurn()
        {
            UpdateStatusBar(PokerController.CurrentPlayer.Name + "'s Turn!");

            if (PokerController.Round == Round.Flop)
            {
                //btnBet.IsEnabled = false;
                if (!flopCardsShowing)
                    ShowFlopCards();

            }
            else if (PokerController.Round == Round.Turn)
            {
                if (!turnCardShowing)
                    ShowTurnCard();
            }

            else if (PokerController.Round == Round.River)
            {
                if (!riverCardShowing)
                    ShowRiverCard();
            }
            else if (PokerController.Round == Round.GameOver)
            {
                MessageBox.Show("winnder");
            }
        }

        private void ShowFlopCards()
        {
            flopCardsShowing = true;

            com1.Source = GetCardPath(PokerController.CommunityCards[0]);
            com2.Source = GetCardPath(PokerController.CommunityCards[1]);
            com3.Source = GetCardPath(PokerController.CommunityCards[2]);
        }

        private void ShowTurnCard()
        {
            turnCardShowing = true;
            com4.Source = GetCardPath(PokerController.CommunityCards[3]);
        }

        private void ShowRiverCard()
        {
            riverCardShowing = true;
            com5.Source = GetCardPath(PokerController.CommunityCards[4]);
        }

        private void Call_Click(object sender, RoutedEventArgs e)
        {
            PokerController.Call();
            UpdateGameStats();
        }

        private void Raise_Click(object sender, RoutedEventArgs e)
        {
            //PokerController.Raise(0);
            int betAmount = (int)sBetAmount.Value;
            if (betAmount > 0 && PokerController.ValidateFunds(betAmount))
            {
                PokerController.Raise(betAmount);
                UpdateGameStats();
            }
        }

        private void Fold_Click(object sender, RoutedEventArgs e)
        {
            //PokerController.Fold();
            PokerController.Fold();
            NextPlayerTurn();
        }

        private void All_In_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowCards(object sender, MouseEventArgs e)
        {
            ph1.Source = GetCardPath(PokerController.CurrentPlayer.Hand[0]);
            ph2.Source = GetCardPath(PokerController.CurrentPlayer.Hand[1]);
        }

        private BitmapImage GetCardPath(Card card)
        {
            string imagePath = $"../../Resources/{card.Value}{card.Suit[0].ToString()}.png";
            return new BitmapImage(new Uri(imagePath, UriKind.Relative));
        }

        private void HideCards(object sender, MouseEventArgs e)
        {
            string imagePath = $"../../Resources/gray_back.png";
            var image = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            ph1.Source = image;
            ph2.Source = image;
        }
    }
}
