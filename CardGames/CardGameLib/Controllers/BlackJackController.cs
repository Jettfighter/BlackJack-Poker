using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGameLib.Models;

namespace CardGameLib.Controllers
{
    public static class BlackJackController
    {
        public static Blackjack blackjack;
        private static BlackjackPlayer house = new BlackjackPlayer();
        private static int round = 0;
        
        public static BlackjackPlayer[] ConvertPlayerToBlackjackPlayer(string[] playerNames)
        {
            BlackjackPlayer[] players = new BlackjackPlayer[playerNames.Length];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new BlackjackPlayer()
                {
                    Name = playerNames[i],
                    Bank = 100,
                    BoughtIn = false
                };
            }
            return players;
        }

        public static bool CheckTwentyOne(List<Card> hand)
        {
            bool isTwentyOne = false;
            int total = 0;

            foreach(Card c in hand)
            {
                total += c.Value;
            }

            isTwentyOne = (total == 21 && hand.Count > 2);

            return isTwentyOne;
        }

        public static bool CheckNatural(List<Card> hand)
        {
            bool isNatural = false;

            int total = 0;

            foreach (Card c in hand)
            {
                total += c.Value;
            }

            isNatural = (total == 21 && hand.Count == 2);

            return isNatural;
        }

        public static void StartGame(string[] playerNames)
        {
            blackjack = new Blackjack(ConvertPlayerToBlackjackPlayer(playerNames));
            round = 1;
        }

        public static bool TakeTurn(bool hitMe, string playerName)
        {
            bool yes = true;

            return yes;
        }

        public static void HouseTurn()
        {

        }

        public static bool SplittingPairs(BlackjackPlayer player)
        {
            if (player.Hand.Count == 2)
            {

            }
            return false;
        }

        public static bool DoublingDown()
        {
            //if(player.Hand.Count==2)
            {

            }
            return false;
        }
    }
}
