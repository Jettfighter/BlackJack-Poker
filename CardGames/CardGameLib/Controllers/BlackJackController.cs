using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGameLib.Models;

namespace CardGameLib.Controllers
{
    class BlackJackController
    {
        Blackjack blackjack;

        public BlackjackPlayer[] ConvertPlayerToBlackjackPlayer(string[] playerNames)
        {
            BlackjackPlayer[] players = new BlackjackPlayer[playerNames.Length];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new BlackjackPlayer()
                {
                    Name = playerNames[i],
                    Bank = 100
                };
            }
            return players;
        }

        public bool CheckTwentyOne(List<Card> hand)
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

        public bool CheckNatural(List<Card> hand)
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

        public void StartGame(string[] playerNames)
        {
            blackjack = new Blackjack(ConvertPlayerToBlackjackPlayer(playerNames));
        }

        public bool TakeTurn(bool hitMe, string playerName)
        {
            bool yes = true;

            return yes;
        }

        public void HouseTurn()
        {

        }
    }
}
