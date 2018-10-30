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
        public static int ConvertValue(Card card)
        {
            int total = 0;
            if (card.Value == 'K' || card.Value == 'Q' || card.Value == 'J')
            {
                total = 10;
            }
            else if (card.Value == 'A')
            {
                total = 11;
            }
            else if (card.Value > '0' && card.Value <= '9')
            {
                total = card.Value - 48;
            }
            return total;
        }

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
                    BoughtIn = false,
                    DoubledDown = false,
                    SecondHandInitiated = false
                };
            }
            return players;
        }
        public static void StartGame(string[] playerNames)
        {
            blackjack = new Blackjack(ConvertPlayerToBlackjackPlayer(playerNames));
            round = 1;

            //give out the first card to each player
            foreach (var player in blackjack.GetAllPlayers())
            {
                player.Hand = new List<Card>();
                player.Hand.Add(blackjack.Deck.GetCard());
            }
            house.Hand = new List<Card>();
            house.Hand.Add(blackjack.Deck.GetCard());

            //give out the second card to each player
            foreach (var player in blackjack.GetAllPlayers())
            {
                player.Hand.Add(blackjack.Deck.GetCard());
            }
            house.Hand.Add(blackjack.Deck.GetCard());
        }

        public static bool IsBusted(string playerName, bool secondHandCheck)
        {
            BlackjackPlayer player = blackjack.GetPlayer(playerName);
            bool isBusted = false;

            int total = 0;
            bool hasAce = false;

            foreach (Card card in secondHandCheck ? player.SecondHand : player.Hand)
            {
                if (card.Value == 'A')
                {
                    hasAce = true;
                }
                total += ConvertValue(card);
            }

            if (total > 21 && hasAce)
            {
                total -= 10;
            }

            if (total > 21)
            {
                isBusted = true;
            }

            return isBusted;
        }

        public static int PlayerBet(string playerName, int amount)
        {
            BlackjackPlayer player = blackjack.GetPlayer(playerName);
            player.FirstBet = amount;
            player.Bank -= amount;
            return player.Bank;
        }

        public static bool CheckTwentyOne(string player)
        {
            Player p = blackjack.GetPlayer(player);
            List<Card> hand = p.Hand;
            bool isTwentyOne = false;
            int total = 0;

            foreach (Card c in hand)
            {
                total += ConvertValue(c);
            }

            isTwentyOne = (total == 21 && hand.Count > 2);

            return isTwentyOne;
        }
        
        public static bool CheckNatural(string player)
        {
            Player p = blackjack.GetPlayer(player);
            List<Card> hand = p.Hand;
            bool isNatural = false;

            int total = 0;

            foreach (Card c in hand)
            {
                total += ConvertValue(c);
            }

            isNatural = (total == 21 && hand.Count == 2);

            return isNatural;
        }

        public static void TakeTurn(bool hitMe, string playerName, bool secondHand)
        {
            BlackjackPlayer player = blackjack.GetPlayer(playerName);
            if(secondHand)
            {
                player.SecondHand.Add(blackjack.Deck.GetCard());
            }
            else
            {
                player.Hand.Add(blackjack.Deck.GetCard());
            }
        }

        public static void HouseTurn()
        {
            int houseTotal = 0;
            bool hasAce = false;

            foreach (Card card in house.Hand)
            {
                if (card.Value == 'A')
                {
                    hasAce = true;
                }
                houseTotal += ConvertValue(card);
            }

            if (houseTotal > 21 && hasAce)
            {
                houseTotal -= 10;
            }

            while (houseTotal < 17)
            {
                Card card = blackjack.Deck.GetCard();
                house.Hand.Add(card);
                houseTotal += ConvertValue(card);
                if (houseTotal > 21 && card.Value == 'A')
                {
                    houseTotal -= 10;
                }
            }

        }

        public static bool CanSplitPairs(string player)
        {
            BlackjackPlayer p = blackjack.GetPlayer(player);
            
            bool splitting = (p.Hand.Count == 2 &&
                              p.Hand.ToArray()[0].Value == p.Hand.ToArray()[1].Value);

            return splitting;
        }
        
        public static void SplitPairs(string player)
        {
            BlackjackPlayer p = blackjack.GetPlayer(player);
            bool aces = (p.Hand.ToArray()[0].Value == 'A' && p.Hand.ToArray()[1].Value == 'A');
            if (aces)
            {
                p.HitLimit = 1;
            }          

            p.SecondHand.Add(p.Hand.Last<Card>());
            p.Hand.Remove(p.Hand.Last<Card>());

            p.Hand.Add(blackjack.Deck.GetCard());
            p.SecondHand.Add(blackjack.Deck.GetCard());

            p.SecondBet = p.FirstBet;

        }
        
        public static bool CanDoubleDown(string name) 
        {
            bool possible = false;
            BlackjackPlayer player = blackjack.GetPlayer(name);
            if (round == 1 && player.Hand.Count == 2)
            {
                int total = 0;
                foreach (var card in player.Hand)
                {
                    if (card.Value == 'A')
                    {
                        total += 1;
                    }
                    else
                    {
                        total += ConvertValue(card);
                    }
                }
                if (total >= 9 && total <= 11)
                {
                    possible = true;
                }
            }

            return possible;
        }

        public static bool DoublingDown(string name)
        {
            bool successful = CanDoubleDown(name);
            if (successful)
            {

                BlackjackPlayer player = blackjack.GetPlayer(name);
                player.FirstBet *= 2;
                player.DoubledDown = true;
                player.Hand.Add(blackjack.Deck.GetCard());
            }
            return successful;
        }
    }
}
