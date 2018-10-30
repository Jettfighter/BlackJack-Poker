using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGameLib.Models;

namespace CardGameLib.Controllers
{
    public static  class PokerController
    {
        private static Deck Deck = new Deck();
        private static List<PokerPlayer> Players = new List<PokerPlayer>();

        private static int Pot = 0;
        private static int Blind = 4;
        private static int CurrentBet = 0;

        //This variable determines which player's turn it is
        private static int PlayerCounter = 0;

        public static Player CurrentPlayer { get; set; }

        //In the window when the number of players and player names are determined
        //Player names can be added to a list<string> which will be passed into NewGame to determine number of players and player names.
        public static void NewGame(List<string> PlayerNames)
        {
            foreach(string name in PlayerNames)
            {
                List<Card> cards = new List<Card>();
                cards.Add(Deck.GetCard());
                cards.Add(Deck.GetCard());
                PokerPlayer player = new PokerPlayer()
                {
                    Name = name,
                    Bank = 100,
                    SmallBlind = false,
                    BigBlind = false,
                    Folded = false,
                    Hand = cards
                };

                Players.Add(player);
            }

            //Players[0].SmallBlind = true;
            //Players[1].BigBlind = true;
        }

        //
        private static void IncrementCounter()
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

        public static int GetBank()
        {
            return Players[PlayerCounter].Bank;
        }

        public static int GetPot()
        {
            return Pot;
        }

        public static void Fold()
        {
            Players[PlayerCounter].Folded = true;
            IncrementCounter();
        }

        public static void Call()
        {
            Players[PlayerCounter].Bank -= CurrentBet;
            Pot += CurrentBet;
            IncrementCounter();
        }

        public static void Raise(int raise)
        {
            CurrentBet += raise;
            Players[PlayerCounter].Bank -= CurrentBet;
            Pot += CurrentBet;
            IncrementCounter();
        }
    }
}
