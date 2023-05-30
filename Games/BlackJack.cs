using Models;
using Helpers;

namespace Games
{
    public class Blackjack
    {
        private List<Card> _deck = new List<Card>();

        public Blackjack(List<Card> deck)
        {
            _deck = deck;
        }

        public void Run()
        {
            Random rng = new Random();

            List<Card> tempDeck = _deck;

            List<Card> playerHand;
            List<Card> dealerHand;

            bool playerBust;
            bool dealerBust;

            // Start game loop
            while (true)
            {
                // reset vars
                _deck = tempDeck;
                playerHand = new List<Card>();
                dealerHand = new List<Card>();

                playerBust = false;
                dealerBust = false;

                // shuffle deck
                _deck.Shuffle();
                // Console.WriteLine(deck.List());
                // deal cards to dealer and player from the top of the deck
                Console.WriteLine("One card for you.");
                _deck.Deal(playerHand);
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine($"*He deals you a {playerHand[0].ToString()} face-up*");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("And one for me.");
                _deck.Deal(dealerHand);
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine($"*He deals himself a {dealerHand[0].ToString()} face-up*");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("*He deals you each one more card face down, you, then himself.");
                _deck.Deal(playerHand);
                _deck.Deal(dealerHand);
                System.Threading.Thread.Sleep(1000);

                // loop untill player decides to stay or busts
                while (true)
                {
                    // Player turn
                    Console.WriteLine($"Your hand is:\n{playerHand.List()}");
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
                        _deck.Deal(playerHand);
                        System.Threading.Thread.Sleep(1000);
                        if (Busted(playerHand))
                        {
                            Console.WriteLine($"Aw, dammn, {CalculateTotal(playerHand).ToString()}, you busted.");
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
                    if (CalculateTotal(dealerHand) <= 16)
                    {
                        Console.WriteLine("I'll hit.");
                        System.Threading.Thread.Sleep(1000);
                        Console.WriteLine($"A {_deck[0].ToString()}. *He plays face up.*");
                        _deck.Deal(dealerHand);
                        System.Threading.Thread.Sleep(1000);
                        if (Busted(dealerHand))
                        {
                            Console.WriteLine($"That brings my total to {CalculateTotal(dealerHand).ToString()}, I busted.");
                            System.Threading.Thread.Sleep(1000);
                            dealerBust = true;
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("I'll Stay");
                        System.Threading.Thread.Sleep(1000);
                        Console.WriteLine($"He reveals his face down card: {dealerHand[1]}");
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
                    Console.WriteLine($"Your Total: {CalculateTotal(playerHand)}\nMy Total: {CalculateTotal(dealerHand)}");
                    System.Threading.Thread.Sleep(1000);
                    if (CalculateTotal(playerHand) > CalculateTotal(dealerHand))
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
            // If they are over 21, change aces from an 11 to a 1
            if (total > 21)
            {
                total -= 10*NumberOfAces;
            }
            //Console.WriteLine($"CALCULATED TOTAL: {total}");
            return total;
        }
    }
}