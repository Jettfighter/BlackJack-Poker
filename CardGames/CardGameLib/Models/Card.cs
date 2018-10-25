using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLib.Models
{
    public class Card
    {
        public char Value { get; set; }
        public string Suit { get; set; }

        public Card(char value, string suit)
        {
            this.Value = value;
            this.Suit = suit;
        }
    }
}
