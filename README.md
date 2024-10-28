# Curso Básico de C# - ES

Este repositorio contiene mis notas y ejercicios del curso básico de programación con C# de la plataforma académica Platzi. Documentaré mi progreso, conceptos clave y soluciones a ejercicios prácticos con el objetivo de consolidar mi aprendizaje en C# y tener un recurso de consulta personal.

## Contenido

- [Introducción a la programación con C#](#introducción)
- [Primeros pasos con C#](#primeros-pasos)
- [Bucles y estructuras de control en C#](#bucles-y-estructuras-de-control)
- [Documentación del Programa en C#](#documentación-del-programa)
  - [Cálculo de Área](#cálculo-de-área)
    - [Área de un Rectángulo](#cálculo-del-área-de-un-rectángulo)
    - [Área de un Círculo](#cálculo-del-área-de-un-círculo)
  - [Juego de Blackjack](#juego-de-blackjack)

## Introducción

Este programa es un ejercicio introductorio en C#. Realiza las siguientes acciones:

1. Calcula el área de un rectángulo.
2. Calcula el área de un círculo.
3. Inicia un mini juego de Blackjack con las siguientes reglas:

### Reglas del Juego

- **Blackjack Inmediato**: Si el jugador o el dealer obtienen Blackjack al recibir sus cartas iniciales, el juego termina automáticamente.
- **Busted**: Si el jugador o el dealer exceden los 21 puntos, pierden de inmediato.
- **Empate con 21 puntos**: Si ambos tienen 21, se declara un empate.
- **Comparación Final**: Si nadie supera 21 puntos, se compara quién tiene la mano más alta.
- **Lógica del Dealer**: El dealer deja de recibir cartas si llega a 17 puntos o más. Si alcanza 21 puntos, se procede a comparar con el jugador.

## Cálculo de Área

### Cálculo del Área de un Rectángulo

```csharp
// Calculadora de área del rectángulo
double sideA;  // Declara una variable para el lado A del rectángulo
double sideB;  // Declara una variable para el lado B del rectángulo
double rectangleArea;  // Declara una variable para el área del rectángulo

Console.WriteLine("¡Calcula el área de un rectángulo! \n");
Console.Write("Ingresa el valor del lado A del rectángulo: ");
sideA = Convert.ToDouble(Console.ReadLine());  // Lee y convierte la entrada a double
Console.Write("Ingresa el valor del lado B del rectángulo: ");
sideB = Convert.ToDouble(Console.ReadLine());  // Lee y convierte la entrada a double
rectangleArea = sideA * sideB;  // Calcula el área del rectángulo
Console.WriteLine($"El área de tu rectángulo es: {rectangleArea} \n\n");  // Muestra el resultado
```

### Cálculo del Área de un Círculo

```csharp
// Calculadora de área del círculo
const double NumberPi = 3.1416;  // Define el valor constante de Pi
double radius;  // Declara una variable para el radio del círculo
double circleArea;  // Declara una variable para el área del círculo

Console.WriteLine("¡Calcula el área de un círculo! \n");
Console.Write("Ingresa el valor del radio del círculo: ");
radius = Convert.ToDouble(Console.ReadLine());  // Lee y convierte la entrada a double
circleArea = NumberPi * Math.Pow(radius, 2);  // Calcula el área del círculo usando Math.Pow
Console.WriteLine($"El área de tu círculo es: {circleArea} \n\n");  // Muestra el resultado
```

## Juego de Blackjack

### Código del Juego

```csharp
static void StartBlackjackGame()
{
    // Crear una nueva baraja de cartas
    Deck deck = new Deck();
    List<Card> shuffleDeck = deck.GetDeck();

    // Crear la instancia del Dealer para gestionar el juego
    Dealer dealer = new Dealer();
    dealer.StartGame(shuffleDeck);
}

// Definir la clase Card
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
        return $"{Value} de {Suit}";
    }
}

// Definir la clase Deck
public class Deck
{
    private List<Card> cards;

    public Deck()
    {
        cards = new List<Card>();
        string[] values = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        string[] suits = { "Corazones", "Diamantes", "Tréboles", "Picas" };

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

// Definir la clase Dealer
public class Dealer
{
    public List<Card> dealerGame = new List<Card>();
    public List<Card> playerGame = new List<Card>();

    public void StartGame(List<Card> cards)
    {
        playerGame.Add(cards[0]);
        playerGame.Add(cards[1]);
        dealerGame.Add(cards[2]);

        Console.WriteLine("Cartas del Jugador:");
        foreach (var card in playerGame)
        {
            Console.WriteLine(card);
        }

        Console.WriteLine("\nCarta del Dealer:");
        Console.WriteLine(dealerGame[0]);
        cards.RemoveRange(0, 3);

        if (IsBlackJack(playerGame))
        {
            Console.WriteLine("¡El jugador gana con un Blackjack!");
            return;
        }

        PlayerTurn(cards);
        if (IsBusted(playerGame))
        {
            Console.WriteLine("¡El jugador se ha pasado! El dealer gana.");
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
            Console.WriteLine("¿Quieres otra carta? (y/n)");
            string answer = (Console.ReadLine() ?? "").Trim().ToLower();
            if (answer == "y")
            {
                Card newCard = cards[0];
                playerGame.Add(newCard);
                Console.WriteLine($"Has recibido: {newCard}");
                cards.RemoveAt(0);
                if (CalculateTotal(playerGame) == 21)
                {
                    Console.WriteLine("¡Tienes 21 puntos!");
                    break;
                }
                if (IsBusted(playerGame))
                    break;
            }
            else if (answer == "n")
            {
                Console.WriteLine("Has dejado de tomar cartas.");
                break;
            }
            else
            {
                Console.WriteLine("Respuesta inválida. Por favor ingresa 'y' o 'n'.");
            }
        }
    }

    private void DealerTurn(List<Card> cards)
    {
        dealerGame.Add(cards[0]);
        Console.WriteLine($"\nSegunda carta del Dealer: {cards[0]}");
        cards.RemoveAt(0);

        if (IsBlackJack(dealerGame))
        {
            Console.WriteLine("¡El dealer gana con un Blackjack!");
            return;
        }

        while (CalculateTotal(dealerGame) < 17)
        {
            Card newCard = cards[0];
            dealerGame.Add(newCard);
            Console.WriteLine($"El dealer toma: {newCard}");
            cards.RemoveAt(0);
        }

        if (IsBusted(dealerGame))
        {
            Console.WriteLine("¡El dealer se ha pasado! El jugador gana.");
        }
    }

    private void DetermineWinner()
    {
        int playerTotal = CalculateTotal(playerGame);
        int dealerTotal = CalculateTotal(dealerGame);
        // Lógica para determinar el ganador
    }

    // Métodos adicionales para calcular totales y verificar condiciones
    // ...
}
```

## Notas Finales

Esta documentación es un recurso útil para aquellos que están aprendiendo C#. Se recomienda a los estudiantes revisar el código, probarlo y modificarlo para reforzar su comprensión.

# Basic C# Course - EN

This repository contains my notes and exercises from the basic programming course with C# on the academic platform Platzi. I will document my progress, key concepts, and solutions to practical exercises with the goal of consolidating my learning in C# and having a personal reference resource.

## Content

- [Introduction to Programming with C#](#introduction)
- [Getting Started with C#](#getting-started)
- [Loops and Control Structures in C#](#loops-and-control-structures)
- [Program Documentation in C#](#program-documentation)
  - [Area Calculation](#area-calculation)
    - [Area of a Rectangle](#area-of-a-rectangle)
    - [Area of a Circle](#area-of-a-circle)
  - [Blackjack Game](#blackjack-game)

## Introduction

This program is an introductory exercise in C#. It performs the following actions:

1. Calculates the area of a rectangle.
2. Calculates the area of a circle.
3. Starts a mini Blackjack game with the following rules:

### Game Rules

- **Instant Blackjack**: If the player or dealer gets Blackjack when receiving their initial cards, the game ends automatically.
- **Busted**: If the player or dealer exceeds 21 points, they lose immediately.
- **Tie with 21 points**: If both have 21, it's declared a tie.
- **Final Comparison**: If no one exceeds 21 points, it compares who has the highest hand.
- **Dealer Logic**: The dealer stops taking cards when they reach 17 points or more. If they reach 21 points, the comparison with the player proceeds.

## Area Calculation

### Area of a Rectangle

```csharp
// Rectangle area calculator
double sideA;  // Declare a variable for the A side of the rectangle
double sideB;  // Declare a variable for the B side of the rectangle
double rectangleArea;  // Declare a variable for the area of the rectangle

Console.WriteLine("Calculate the area of a rectangle! \n");
Console.Write("Enter the value of the A side of the rectangle: ");
sideA = Convert.ToDouble(Console.ReadLine());  // Read and convert the input to double
Console.Write("Enter the value of the B side of the rectangle: ");
sideB = Convert.ToDouble(Console.ReadLine());  // Read and convert the input to double
rectangleArea = sideA * sideB;  // Calculate the area of the rectangle
Console.WriteLine($"The area of your rectangle is: {rectangleArea} \n\n");  // Display the result
```

### Area of a Circle

```csharp
// Circle area calculator
const double NumberPi = 3.1416;  // Define the constant value of Pi
double radius;  // Declare a variable for the radius of the circle
double circleArea;  // Declare a variable for the area of the circle

Console.WriteLine("Calculate the area of a circle! \n");
Console.Write("Enter the value of the radius of the circle: ");
radius = Convert.ToDouble(Console.ReadLine());  // Read and convert the input to double
circleArea = NumberPi * Math.Pow(radius, 2);  // Calculate the area of the circle using Math.Pow
Console.WriteLine($"The area of your circle is: {circleArea} \n\n");  // Display the result
```

## Blackjack Game

### Game Code

```csharp
static void StartBlackjackGame()
{
    // Create a new deck of cards
    Deck deck = new Deck();
    List<Card> shuffleDeck = deck.GetDeck();

    // Create an instance of the Dealer to manage the game
    Dealer dealer = new Dealer();
    dealer.StartGame(shuffleDeck);
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

        Console.WriteLine("Player's Cards:");
        foreach (var card in playerGame)
        {
            Console.WriteLine(card);
        }

        Console.WriteLine("\nDealer's Card:");
        Console.WriteLine(dealerGame[0]);
        cards.RemoveRange(0, 3);

        if (IsBlackJack(playerGame))
        {
            Console.WriteLine("The player wins with a Blackjack!");
            return;
        }

        PlayerTurn(cards);
        if (IsBusted(playerGame))
        {
            Console.WriteLine("The player has busted! The dealer wins.");
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
                Console.WriteLine($"You received: {newCard}");
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
                Console.WriteLine("You have stopped taking cards.");
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
            Console.WriteLine("The dealer wins with a Blackjack!");
            return;
        }

        while (CalculateTotal(dealerGame) < 17)
        {
            Card newCard = cards[0];
            dealerGame.Add(newCard);
            Console.WriteLine($"The dealer takes: {newCard}");
            cards.RemoveAt(0);
        }

        if (IsBusted(dealerGame))
        {
            Console.WriteLine("The dealer has busted! The player wins.");
        }
    }

    private void DetermineWinner()
    {
        int playerTotal = CalculateTotal(playerGame);
        int dealerTotal = CalculateTotal(dealerGame);
        // Logic to determine the winner
    }

    // Additional methods to calculate totals and check conditions
    // ...
}
```

## Final Notes

This documentation serves as a useful resource for those learning C#. Students are encouraged to review the code, test it, and modify it to reinforce their understanding.
