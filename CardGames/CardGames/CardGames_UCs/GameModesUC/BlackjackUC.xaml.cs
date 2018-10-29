﻿using System;
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
    public partial class BlackjackUC : UserControl
    {
        public List<Label> PlayerNameDisplays = new List<Label>();

        public List<Label> PlayerBankDisplays = new List<Label>();

        public List<StackPanel> PlayerHandDisplays = new List<StackPanel>();

        public BlackjackUC()
        {
            InitializeComponent();
        }

        private void UC_Loaded(object sender, RoutedEventArgs e)
        {
            PreparePlayerElements();
            ResetPlayers();

        }

        private void ResetPlayers()
        {
            //TODO: Update names
            //TODO: Update player banks
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
    }
}
