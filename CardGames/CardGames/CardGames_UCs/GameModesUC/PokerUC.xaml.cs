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

        public PokerUC(List<string> names)
        {
            InitializeComponent();
        }

        public void NewGame(List<string> names)
        {
            PokerController.NewGame(names);
            GetPlayerInfo();
        }

        private void GetPlayerInfo()
        {
            lbBank.Content = $"Amount in Bank: {PokerController.GetBank()}.";
            lbPot.Content = $"Amount in Pot: {PokerController.GetPot()}.";
        }

        private void NextPhase()
        {
            if (PokerController.NextPhase())
            {
                if(Flop)
                {

                }
                else if (Turn)
                {

                }
                else if (River)
                {

                }
            }
        }

        private void Bet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Call_Click(object sender, RoutedEventArgs e)
        {
            PokerController.Call();
            NextPhase();
            GetPlayerInfo();
        }

        private void Raise_Click(object sender, RoutedEventArgs e)
        {
            //PokerController.Raise(0);
        }

        private void Fold_Click(object sender, RoutedEventArgs e)
        {
            //PokerController.Fold();
        }

        private void All_In_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
