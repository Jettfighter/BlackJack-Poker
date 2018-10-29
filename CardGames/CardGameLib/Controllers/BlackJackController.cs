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
        private BlackjackPlayer house = new BlackjackPlayer();
        private int round = 0;

        public BlackjackPlayer[] ConvertPlayerToBlackjackPlayer(string[] playerNames)
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

        public void StartGame(string[] playerNames)
        {
            blackjack = new Blackjack(ConvertPlayerToBlackjackPlayer(playerNames));
            round = 1;
        }

        public bool TakeTurn(bool hitMe, string playerName)
        {
            bool yes = true;

            return yes;
        }

        public void HouseTurn()
        {

        }

        public bool SplittingPairs()
        {
            
            return false;
        }

        public bool DoublingDown(BlackjackPlayer player)
        {
            if(player.Hand.Count==2)
            {

            }
            return false;
        }
    }
}
