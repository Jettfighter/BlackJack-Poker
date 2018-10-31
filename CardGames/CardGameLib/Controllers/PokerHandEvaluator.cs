using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CardGameLib.Models;

namespace CardGameLib.Controllers
{
    public enum Ranking
    {
        HighCard, OnePair, TwoPairs, ThreeOfAKind, Straight,
        Flush, FullHouse, StraightFlush, RoyalFlush
    }
    public static class PokerHandEvaluator
    {
        static List<List<Card>> playerCardsPlusCommunityCards;
        static List<List<Card>> playersPossibleHands;


        public static Player DetermineWinner(List<Card> communityCards, List<PokerPlayer> players)
        {
            playerCardsPlusCommunityCards = new List<List<Card>>();
            playersPossibleHands = new List<List<Card>>();
            
            foreach (var player in players)
            {
                playerCardsPlusCommunityCards.Add(new List<Card>());

                playerCardsPlusCommunityCards.Last().Add(player.Hand[0]);
                playerCardsPlusCommunityCards.Last().Add(player.Hand[1]);

                foreach (var communityCard in communityCards)
                {
                    playerCardsPlusCommunityCards.Last().Add(communityCard);
                }
            }
            SetUpPlayersAllPossibleHands();

            return new Player();
        }

        private static void SetUpPlayersAllPossibleHands()
        {
            foreach(var listOfPlayerCards in playerCardsPlusCommunityCards)
            {
                playersPossibleHands.Add(new List<Card>());


                List<List<Card>> listOfAllCombinations = GetAllCombos(listOfPlayerCards);

                foreach(var list in listOfAllCombinations)
                {
                    if (list.Count == 5)
                    {
                        //playersPossibleHands.Last().Last().
                    }
                }
            }
        }

        public static List<List<T>> GetAllCombos<T>(List<T> list)
        {
            int comboCount = (int)Math.Pow(2, list.Count) - 1;
            List<List<T>> result = new List<List<T>>();
            for (int i = 1; i < comboCount + 1; i++)
            {
                // make each combo here
                result.Add(new List<T>());
                for (int j = 0; j < list.Count; j++)
                {
                    if ((i >> j) % 2 != 0)
                        result.Last().Add(list[j]);
                }
            }
            return result;
        }
    }
}
