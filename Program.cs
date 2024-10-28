using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Message to explain the logic or rules of Blackjack
            string blackjackRules = @"
Blackjack Rules:
1. Immediate Blackjack: If the player or dealer gets a Blackjack after receiving their initial cards, the game ends immediately.
2. Busted: If the player or dealer exceeds 21 points, they lose immediately.
3. Tie with 21 points: If both have 21, it is declared a tie.
4. Final Comparison: If no one exceeds 21 points, whoever has the higher hand wins.
5. Dealer Logic: The dealer stops receiving cards if they reach 17 points or more. If they reach 21 points, the comparison with the player proceeds.
";
            Console.WriteLine("Hello, World!... Att: VaLa_Laiton \nThis is a small exercise to take my first steps in C#. \nNext, I will calculate the area of a rectangle and a circle. \n\n");

            // Rectangle Area Calculator
            double sideA;
            double sideB;
            double rectangleArea;

            Console.WriteLine("Calculate the area of a rectangle! \n");
            Console.Write("Enter the value of side A of the rectangle: ");
            sideA = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter the value of side B of the rectangle: ");
            sideB = Convert.ToDouble(Console.ReadLine());
            rectangleArea = sideA * sideB;
            Console.WriteLine($"The area of your rectangle is: {rectangleArea} \n\n");

            // Circle Area Calculator
            const double NumberPi = 3.1416;
            double radius;
            double circleArea;

            Console.WriteLine("Calculate the area of a circle! \n");
            Console.Write("Enter the value of the radius of the circle: ");
            radius = Convert.ToDouble(Console.ReadLine());
            circleArea = NumberPi * Math.Pow(radius, 2);
            Console.WriteLine($"The area of your circle is: {circleArea} \n\n");

            // Blackjack Introduction
            Console.WriteLine("Starting the mini Blackjack game!\n");
            Console.WriteLine(blackjackRules);

            StartBlackjackGame();

            // Allow player to replay Blackjack
            while (true)
            {
                Console.WriteLine("\nDo you want to play Blackjack again? (y/n)");
                string playAgain = (Console.ReadLine() ?? "").Trim().ToLower();
                if (playAgain == "y")
                {
                    StartBlackjackGame();
                }
                else if (playAgain == "n")
                {
                    Console.WriteLine("Thank you for playing! Goodbye!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                }
            }
        }

        static void StartBlackjackGame()
        {
            // Create a new deck of cards
            Deck deck = new Deck();
            List<Card> shuffleDeck = deck.GetDeck();

            // Create the Dealer instance to manage the game
            Dealer dealer = new Dealer();
            dealer.StartGame(shuffleDeck);
        }
    }

    // Define the Card class
    public class Card
    {
        public string Value { get; set; }
        public string Suit { get; set; }

        public Card(string value, string suit)
        {
            Value = value;
            Suit = suit;
        }

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }
    }

    // Define the Deck class
    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();
            string[] values = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };

            foreach (string suit in suits)
            {
                foreach (string value in values)
                {
                    cards.Add(new Card(value, suit));
                }
            }
            Shuffle();
        }

        private void Shuffle()
        {
            Random rng = new Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card temp = cards[k];
                cards[k] = cards[n];
                cards[n] = temp;
            }
        }

        public List<Card> GetDeck()
        {
            return cards;
        }
    }

    // Define the Dealer class
    public class Dealer
    {
        public List<Card> dealerGame = new List<Card>();
        public List<Card> playerGame = new List<Card>();

        public void StartGame(List<Card> cards)
        {
            playerGame.Add(cards[0]);
            playerGame.Add(cards[1]);
            dealerGame.Add(cards[2]);

            Console.WriteLine("Player Cards:");
            foreach (var card in playerGame)
            {
                Console.WriteLine(card);
            }

            Console.WriteLine("\nDealer Card:");
            Console.WriteLine(dealerGame[0]);
            cards.RemoveRange(0, 3);

            if (IsBlackJack(playerGame))
            {
                Console.WriteLine("Player wins with a Blackjack!");
                return;
            }

            PlayerTurn(cards);
            if (IsBusted(playerGame))
            {
                Console.WriteLine("Player has busted! Dealer wins.");
                return;
            }

            DealerTurn(cards);

            if (!IsBusted(dealerGame))
            {
                DetermineWinner();
            }
        }

        private void PlayerTurn(List<Card> cards)
        {
            while (true)
            {
                Console.WriteLine("Do you want another card? (y/n)");
                string answer = (Console.ReadLine() ?? "").Trim().ToLower();
                if (answer == "y")
                {
                    Card newCard = cards[0];
                    playerGame.Add(newCard);
                    Console.WriteLine($"You have received: {newCard}");
                    cards.RemoveAt(0);
                    if (CalculateTotal(playerGame) == 21)
                    {
                        Console.WriteLine("You have 21 points!");
                        break;
                    }
                    if (IsBusted(playerGame))
                        break;
                }
                else if (answer == "n")
                {
                    Console.WriteLine("You have stopped drawing cards.");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid answer. Please enter 'y' or 'n'.");
                }
            }
        }

        private void DealerTurn(List<Card> cards)
        {
            dealerGame.Add(cards[0]);
            Console.WriteLine($"\nDealer's second card: {cards[0]}");
            cards.RemoveAt(0);

            if (IsBlackJack(dealerGame))
            {
                Console.WriteLine("Dealer wins with a Blackjack!");
                return;
            }

            while (CalculateTotal(dealerGame) < 17)
            {
                Card newCard = cards[0];
                dealerGame.Add(newCard);
                Console.WriteLine($"Dealer draws: {newCard}");
                cards.RemoveAt(0);
            }

            if (IsBusted(dealerGame))
            {
                Console.WriteLine("Dealer has busted! Player wins.");
            }
        }

        private void DetermineWinner()
        {
            int playerTotal = CalculateTotal(playerGame);
            int dealerTotal = CalculateTotal(dealerGame);
            if (playerTotal > dealerTotal)
            {
                Console.WriteLine("Player wins with higher points!");
            }
            else if (dealerTotal > playerTotal)
            {
                Console.WriteLine("Dealer wins with higher points!");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }
        }

        private bool IsBlackJack(List<Card> hand)
        {
            return hand.Count == 2 && CalculateTotal(hand) == 21;
        }

        private bool IsBusted(List<Card> hand)
        {
            return CalculateTotal(hand) > 21;
        }

        private int CalculateTotal(List<Card> hand)
        {
            int total = 0;
            int aceCount = 0;
            foreach (var card in hand)
            {
                int value = GetCardValue(card.Value);
                total += value;
                if (card.Value == "A")
                    aceCount++;
            }
            while (total > 21 && aceCount > 0)
            {
                total -= 10;
                aceCount--;
            }
            return total;
        }

        private int GetCardValue(string valueCard)
        {
            switch (valueCard)
            {
                case "A": return 11;
                case "J":
                case "Q":
                case "K": return 10;
                default: return int.TryParse(valueCard, out int value) ? value : 0;
            }
        }
    }
}
