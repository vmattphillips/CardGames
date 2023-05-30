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
                // Console.WriteLine(deck.List());
                // deal cards to dealer and player from the top of the deck
                Console.WriteLine("One card for you.");
                _deck.Deal(_playerHand);
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine($"*He deals you a {_playerHand[0].ToString()} face-up*");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("And one for me.");
                _deck.Deal(_dealerHand);
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine($"*He deals himself a {_dealerHand[0].ToString()} face-up*");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("*He deals you each one more card face down, you, then himself.");
                _deck.Deal(_playerHand);
                _deck.Deal(_dealerHand);
                System.Threading.Thread.Sleep(1000);

                // loop untill player decides to stay or busts
                while (true)
                {
                    // Player turn
                    Console.WriteLine($"Your hand is:\n{_playerHand.List()}");
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("What would you like to do?\n[H]it or [S]tay?");
                    String? playerDecision = Console.ReadLine();
                    if (String.IsNullOrEmpty(playerDecision))
                    {
                        Console.WriteLine("INVALID INPUT.");
                        System.Threading.Thread.Sleep(1000);
                        continue;
                    }
                    else if (playerDecision.ToUpper() == "S")
                    {
                        // Do Nothing, mark flag true
                        Console.WriteLine("Stay, gotcha.");
                        System.Threading.Thread.Sleep(1000);
                        break;
                    }
                    else if (playerDecision.ToUpper() == "H")
                    {
                        Console.WriteLine($"Hit? Sure thing.\nHe deals you a {_deck[0].GetValue()}.");
                        _deck.Deal(_playerHand);
                        System.Threading.Thread.Sleep(1000);
                        if (Busted(_playerHand))
                        {
                            Console.WriteLine($"Aw, dammn, {CalculateTotal(_playerHand).ToString()}, you busted.");
                            System.Threading.Thread.Sleep(1000);
                            playerBust = true;
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("INVALID INPUT.");
                        System.Threading.Thread.Sleep(1000);
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
                        Console.WriteLine("I'll hit.");
                        System.Threading.Thread.Sleep(1000);
                        Console.WriteLine($"A {_deck[0].ToString()}. *He plays face up.*");
                        _deck.Deal(_dealerHand);
                        System.Threading.Thread.Sleep(1000);
                        if (Busted(_dealerHand))
                        {
                            Console.WriteLine($"That brings my total to {CalculateTotal(_dealerHand).ToString()}, I busted.");
                            System.Threading.Thread.Sleep(1000);
                            dealerBust = true;
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("I'll Stay");
                        System.Threading.Thread.Sleep(1000);
                        Console.WriteLine($"He reveals his face down card: {_dealerHand[1]}");
                        System.Threading.Thread.Sleep(1000);
                        break;
                    }
                }

                if (playerBust)
                {
                    Console.WriteLine("Tough luck.");
                    System.Threading.Thread.Sleep(1000);
                }
                else if (dealerBust)
                {
                    Console.WriteLine("Luck wasn't on my side, good game.");
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine($"Your Total: {CalculateTotal(_playerHand)}\nMy Total: {CalculateTotal(_dealerHand)}");
                    System.Threading.Thread.Sleep(1000);
                    if (CalculateTotal(_playerHand) > CalculateTotal(_dealerHand))
                    {
                        Console.WriteLine("You got the higher total without busting, good game!");
                        System.Threading.Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("I got the higher total without busting, you played well though!");
                        System.Threading.Thread.Sleep(1000);
                    }
                }

                Console.WriteLine("Play again? Y/n");
                System.Threading.Thread.Sleep(1000);
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
            //Console.WriteLine($"CALCULATED TOTAL: {total}");
            return total;
        }
    }
}