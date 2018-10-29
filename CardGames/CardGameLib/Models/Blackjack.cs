using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLib.Models
{
    public class Blackjack
    {
        private Player[] players;
        public Deck Deck { get; set; }
        public  Blackjack(BlackjackPlayer[] players)
        {
            this.players = players;
            Deck = new Deck();
        }

        public Player GetPlayer(string name)
        {
            foreach (var player in players)
            {
                if(player.Name == name)
                {
                    return player;
                }
            }

            return null;
        }


        

    }
}
