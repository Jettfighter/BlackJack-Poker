using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGameLib.Models;

namespace CardGameLib.Controllers
{
    public enum Round
    {
        Preflop, Flop, Turn, River, GameOver
    }
    public static class PokerController
    {
        public static Deck Deck = new Deck();
        private static List<PokerPlayer> Players = new List<PokerPlayer>();

        public static Round Round { get => s_round; set => s_round = value; }

        private static int Pot = 0;
        private static int Blind = 4;
        private static int CurrentBet = 0;

        //This variable determines which player's turn it is
        private static int PlayerCounter = 0;
        private static Round s_round;

        public static Player CurrentPlayer { get; set; }

        //In the window when the number of players and player names are determined
        //Player names can be added to a list<string> which will be passed into NewGame to determine number of players and player names.
        public static void NewGame(List<string> PlayerNames)
        {
            ResetGame();
            foreach (string name in PlayerNames)
            {
                List<Card> cards = new List<Card>();
                cards.Add(Deck.GetCard());
                cards.Add(Deck.GetCard());
                PokerPlayer player = new PokerPlayer()
                {
                    Name = name,
                    Bank = 100,
                    SmallBlind = false,
                    BigBlind = false,
                    Folded = false,
                    Hand = cards
                };

                Players.Add(player);
            }

            CurrentPlayer = Players[0];
            Round = Round.Preflop;

            //Players[0].SmallBlind = true;
            //Players[1].BigBlind = true;
        }

        private static void ResetGame()
        {
            Deck = new Deck();
            Players = new List<PokerPlayer>();
            Round = Round.Preflop;
            Pot = 0;
            Blind = 4;
            CurrentBet = 0;
            PlayerCounter = 0;
            CurrentPlayer = null;
        }

        //
        public static void IncrementCounter()
        {
            if (Players[PlayerCounter].Folded)
            {
                PlayerCounter++;
            }

            if (PlayerCounter == Players.Count - 1)
            {
                PlayerCounter = 0;
                NextPhase();
            }
            else
            {
                PlayerCounter++;
            }


            CurrentPlayer = Players[PlayerCounter];
        }

        public static bool NextPhase()
        {
            SetNextRound();

            bool done = false;

            if (Players[PlayerCounter].AmountBet == CurrentBet)
            {
                done = true;
            }

            return done;
        }

        private static void SetNextRound()
        {
            switch (Round)
            {
                case Round.Preflop:
                    Round = Round.Flop;
                    break;
                case Round.Flop:
                    Round = Round.Turn;
                    break;
                case Round.Turn:
                    Round = Round.River;
                    break;
                case Round.River:
                    Round = Round.GameOver;
                    break;
                default:
                    Round = Round.GameOver;
                    break;
            }
        }

        public static int GetBank()
        {
            return Players[PlayerCounter].Bank;
        }

        public static int GetPot()
        {
            return Pot;
        }

        public static void Fold()
        {
            Players[PlayerCounter].Folded = true;
            IncrementCounter();
        }

        public static void Call()
        {
            Players[PlayerCounter].Bank -= CurrentBet;
            Players[PlayerCounter].AmountBet = CurrentBet;
            Pot += CurrentBet;
            IncrementCounter();
        }

        public static void Raise(int raise)
        {
            //Player calls first
            Players[PlayerCounter].Bank -= CurrentBet;
            Players[PlayerCounter].AmountBet = CurrentBet;
            Pot += CurrentBet;

            //Then Player raises
            CurrentBet += raise;
            Players[PlayerCounter].Bank -= raise;
            Pot += raise;
            IncrementCounter();
        }

        public static bool Bet(int betAmount)
        {
            if ((GetBank() - betAmount) > 0)
            {
                Players[PlayerCounter].Bank -= betAmount;
                Pot += betAmount;
                CurrentBet = betAmount;
                IncrementCounter();
                return true;
            }
            return false;
        }

        public static bool ValidateFunds(int betAmount)
        {
            if ((GetBank() - betAmount) > 0)
                return true;

            return false;
        }
    }
}
