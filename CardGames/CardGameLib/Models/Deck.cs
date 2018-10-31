using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLib.Models
{
    public class Deck
    {
        private List<Card> CardsInDeck { get; set; }
        private List<Card> CardsPlayed { get; set; }

        public Deck()
        {
            CardsInDeck = new List<Card>();
            CardsPlayed = new List<Card>();

            string suit = "";
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        suit = "Spades";
                        break;
                    case 1:
                        suit = "Hearts";
                        break;
                    case 2:
                        suit = "Clubs";
                        break;
                    case 3:
                        suit = "Diamonds";
                        break;
                }

                //13 Cards per suit
                for (int ii = 1; ii < 14; ii++)
                {
                    Card card;
                    switch (ii)
                    {
                        case 1:
                            card = new Card('A', suit);
                            CardsInDeck.Add(card);
                            break;
                        case 10:
                            card = new Card('X', suit);
                            CardsInDeck.Add(card);
                            break;
                        case 11:
                            card = new Card('J', suit);
                            CardsInDeck.Add(card);
                            break;
                        case 12:
                            card = new Card('Q', suit);
                            CardsInDeck.Add(card);
                            break;
                        case 13:
                            card = new Card('K', suit);
                            CardsInDeck.Add(card);
                            break;
                        default:
                            char value = (char)(ii + 48);
                            card = new Card(value, suit);
                            CardsInDeck.Add(card);
                            break;
                    }
                }
            }
        }

        public Card GetCard()
        {
            Random r = new Random();
            int select = r.Next(0, CardsInDeck.Count);
            Card card = CardsInDeck.ElementAt(select);
            CardsInDeck.Remove(card);
            CardsPlayed.Add(card);

            return card;
        }

        public void Reshuffle()
        {
            foreach (Card card in CardsPlayed)
            {
                CardsInDeck.Add(card);
                CardsPlayed.Remove(card);
            }
        }
    }
}
