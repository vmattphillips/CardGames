using System;
using Models;
using Games;

class Program
{
    static void Main(string[] args)
    {
        List<Card> deck = CreateDeck();

        Console.WriteLine("Welcome to the table, what would you like to play?\n");
        Console.WriteLine("[B]lackjack\n[T]exas Hold'em\n");
        String? input = Console.ReadLine();

        switch (input.ToUpper())
        {
            case "B":
                Console.WriteLine("Blackjack? Nice choice.\n");
                System.Threading.Thread.Sleep(1000);
                Blackjack bj = new Blackjack(deck);
                bj.Run();
                return;
            case "T":
                Console.WriteLine("Not implemented yet, closing application");
                System.Threading.Thread.Sleep(1000);
                return;
            default:
                Console.WriteLine("Invalid input, closing application");
                System.Threading.Thread.Sleep(1000);
                return;
        }
    }

    static List<Card> CreateDeck()
    {
        List<Card> deck = new List<Card>();

        //Add Clubs
        for (int i = 1; i <= 13; i++)
        {
            Card c = new Card(i, Suit.Clubs);
            deck.Add(c);
        }

        //Add Diamonds
        for (int i = 1; i <= 13; i++)
        {
            Card c = new Card(i, Suit.Diamonds);
            deck.Add(c);
        }

        //Add Hearts
        for (int i = 1; i <= 13; i++)
        {
            Card c = new Card(i, Suit.Hearts);
            deck.Add(c);
        }

        //Add Spades
        for (int i = 1; i <= 13; i++)
        {
            Card c = new Card(i, Suit.Spades);
            deck.Add(c);
        }

        return deck;
    }
}