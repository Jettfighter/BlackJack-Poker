using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLib.Models
{
    class PokerPlayer : Player
    {
        public int AmountBet { get; set; }
        public bool SmallBlind { get; set; }
        public bool BigBlind { get; set; }
        public bool Folded { get; set; }
    }
}
