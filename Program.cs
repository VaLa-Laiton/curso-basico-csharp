// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!... Att: VaLa_Laiton \nThis is a small exercise to take my first steps in C#. \nNext, I will calculate the area of a rectangle and a circle. \n\n");

// Rectangle Area Calculator
double sideA;  // Declares a variable for side A of the rectangle
double sideB;  // Declares a variable for side B of the rectangle
double rectangleArea;  // Declares a variable for the area of the rectangle

Console.WriteLine("Calculate the area of a rectangle! \n");
Console.Write("Enter the value of side A of the rectangle: ");  // Prompt for side A
sideA = Convert.ToDouble(Console.ReadLine());  // Reads and converts input to a double

Console.Write("Enter the value of side B of the rectangle: ");  // Prompt for side B
sideB = Convert.ToDouble(Console.ReadLine());  // Reads and converts input to a double

rectangleArea = sideA * sideB;  // Calculates the area of the rectangle
Console.WriteLine($"The area of your rectangle is: {rectangleArea} \n\n");  // Outputs the area of the rectangle

// Circle Area Calculator
const double NumberPi = 3.1416;  // Defines the constant value of Pi
double radius;  // Declares a variable for the radius of the circle
double circleArea;  // Declares a variable for the area of the circle

Console.WriteLine("Calculate the area of a circle! \n");
Console.Write("Enter the value of the radius of the circle: ");  // Prompt for radius
radius = Convert.ToDouble(Console.ReadLine());  // Reads and converts input to a double

circleArea = NumberPi * Math.Pow(radius, 2);  // Calculates the area of the circle using Math.Pow for exponentiation
Console.WriteLine($"The area of your circle is: {circleArea} \n\n");  // Outputs the area of the circle
