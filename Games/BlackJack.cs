using Models;
using Helpers;

namespace Games
{
    public class Blackjack
    {
        private List<Card> _deck = new List<Card>();

        private List<Card> _playerHand = new List<Card>();
        private List<Card> _dealerHand = new List<Card>();

        public Blackjack(List<Card> deck)
        {
            _deck = deck;
        }

        // The monolith function that handles the flow of the game.
        // Might might break it up later
        public void Run()
        {
            Random rng = new Random();

            // Deck to reset the cards once a new game starts
            List<Card> savedDeck = _deck;

            bool playerBust;
            bool dealerBust;

            // Start game loop
            while (true)
            {
                // reset vars
                _deck = savedDeck;
                _playerHand = new List<Card>();
                _dealerHand = new List<Card>();

                playerBust = false;
                dealerBust = false;

                // shuffle deck
                _deck.Shuffle();
                // Methods.Print(deck.List());
                // deal cards to dealer and player from the top of the deck
                Methods.Print("One card for you.");
                _deck.Deal(_playerHand);

                Methods.Print($"*He deals you a {_playerHand[0].ToString()} face-up*");

                Methods.Print("And one for me.");
                _deck.Deal(_dealerHand);

                Methods.Print($"*He deals himself a {_dealerHand[0].ToString()} face-up*");

                Methods.Print("*He deals you each one more card face down, you, then himself.");
                _deck.Deal(_playerHand);
                _deck.Deal(_dealerHand);

                // loop untill player decides to stay or busts
                while (true)
                {
                    // Player turn
                    Methods.Print($"Your hand is:\n{_playerHand.List()}");
                    Methods.Print("What would you like to do?\n[H]it or [S]tay?");
                    String? playerDecision = Console.ReadLine();
                    if (String.IsNullOrEmpty(playerDecision))
                    {
                        Methods.Print("INVALID INPUT.");
                        continue;
                    }
                    else if (playerDecision.ToUpper() == "S")
                    {
                        // Do Nothing, mark flag true
                        Methods.Print("Stay, gotcha.");
                        break;
                    }
                    else if (playerDecision.ToUpper() == "H")
                    {
                        Methods.Print($"Hit? Sure thing.\nHe deals you a {_deck[0].GetValue()}.");
                        _deck.Deal(_playerHand);

                        if (Busted(_playerHand))
                        {
                            Methods.Print($"Aw, dammn, {CalculateTotal(_playerHand).ToString()}, you busted.");
                            playerBust = true;
                            break;
                        }
                    }
                    else
                    {
                        Methods.Print("INVALID INPUT.");
                        continue;
                    }
                }

                while (true)
                {
                    // dealer turn
                    if (playerBust)
                    {
                        break;
                    }
                    // If their total is >=17 they must stay, <= 16 they must hit
                    if (CalculateTotal(_dealerHand) <= 16)
                    {
                        Methods.Print("I'll hit.");
                        Methods.Print($"A {_deck[0].ToString()}. *He plays face up.*");
                        _deck.Deal(_dealerHand);

                        if (Busted(_dealerHand))
                        {
                            Methods.Print($"That brings my total to {CalculateTotal(_dealerHand).ToString()}, I busted.");
                            dealerBust = true;
                            break;
                        }
                    }
                    else
                    {
                        Methods.Print("I'll Stay");
                        Methods.Print($"He reveals his face down card: {_dealerHand[1]}");
                        break;
                    }
                }

                if (playerBust)
                {
                    Methods.Print("Tough luck.");
                }
                else if (dealerBust)
                {
                    Methods.Print("Luck wasn't on my side, good game.");
                }
                else
                {
                    Methods.Print($"Your Total: {CalculateTotal(_playerHand)}\nMy Total: {CalculateTotal(_dealerHand)}");

                    if (CalculateTotal(_playerHand) > CalculateTotal(_dealerHand))
                    {
                        Methods.Print("You got the higher total without busting, good game!");
                    }
                    else
                    {
                        Methods.Print("I got the higher total without busting, you played well though!");
                    }
                }

                Methods.Print("Play again? Y/n");

                String? playAgain = Console.ReadLine();
                if (playAgain.ToUpper() != "Y")
                {
                    break;
                }
            }
        }

        private static bool Busted(List<Card> hand)
        {
            return CalculateTotal(hand) > 21;
        }

        private static int CalculateTotal(List<Card> hand)
        {
            int total = 0;
            int NumberOfAces = 0;
            foreach (Card c in hand)
            {
                if (c.GetValueAsInt() == 1) // Ace
                {
                    total += 11;
                    NumberOfAces++;
                }
                else if (c.GetValueAsInt() == 11 || c.GetValueAsInt() == 12 || c.GetValueAsInt() == 13)
                {
                    total += 10;
                }
                else
                {
                    total += c.GetValueAsInt();
                }
            }

            // If they are over 21, change aces from an 11 to a 1 until they are under
            // This way some aces can be calculated as 11 and some a 1
            for (int i = 0; i < NumberOfAces; i++)
            {
                if (total > 21)
                {
                    total -= 10;
                }
                else
                {
                    break;
                }
            }
            //Methods.Print($"CALCULATED TOTAL: {total}");
            return total;
        }
    }
}