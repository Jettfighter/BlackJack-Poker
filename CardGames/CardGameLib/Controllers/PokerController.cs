﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGameLib.Models;

namespace CardGameLib.Controllers
{
    class PokerController
    {
        private Deck Deck = new Deck();
        private List<PokerPlayer> Players = new List<PokerPlayer>();

        private int Pot = 0;

        //
        private int PlayerCounter = 0;

        //In the window when the number of players and player names are determined
        //Player names can be added to a list<string> which will be passed into NewGame to determine number of players and player names.
        public void NewGame(List<string> PlayerNames)
        {
            foreach(string name in PlayerNames)
            {
                List<Card> cards = new List<Card>();
                cards.Add(Deck.GetCard());
                cards.Add(Deck.GetCard());
                PokerPlayer player = new PokerPlayer()
                {
                    Name = name,
                    Bank = 500,
                    SmallBlind = false,
                    BigBlind = false,
                    Folded = false,
                    Hand = cards
                };

                Players.Add(player);
            }

            Players[0].SmallBlind = true;
            Players[1].BigBlind = true;
        }

        //
        private void IncrementCounter()
        {
            if(PlayerCounter == Players.Count)
            {
                PlayerCounter = 0;
            }
            else
            {
                PlayerCounter++;
            }
        }

        public void Fold()
        {
            Players[PlayerCounter].Folded = true;
            IncrementCounter();
        }

        public void Call()
        {

        }

        public void Raise()
        {

        }
    }
}
