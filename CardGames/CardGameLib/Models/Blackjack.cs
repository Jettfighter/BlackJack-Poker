using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLib.Models
{
    public class Blackjack
    {
        Player[] players;
        public Blackjack(Player[] players)
        {
            this.players = players;
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
