using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLib.Models
{
    public class BlackjackPlayer : Player
    {
        public List<Card> SecondHand { get; set; }
        public int FirstBet { get; set; }
        public int SecondBet { get; set; }
        public bool SecondHandInitiated { get; set; } 
        public bool BoughtIn { get; set; }
        public int HitLimit { get; set; }
        public bool DoubledDown { get; set; }

    }
}
