internal class Program
{
#pragma warning disable IDE0051 // Remove unused private members
    private readonly string secretMessage = "SigmaGrindset";
#pragma warning restore IDE0051 // Remove unused private members

    private static void Main()
    {
        DisplayMessage();
        PerformCalculation();
        Console.WriteLine("Done.");
        Console.ReadKey();
    }

    private static void DisplayMessage()
    {
        Console.Write("Enter a number: ");
        string? input = Console.ReadLine();
        Console.WriteLine("You entered: " + input);
    }

    private static void PerformCalculation()
    {
        int number = 42;
        int result = Calculate(number);
        Console.WriteLine("Result is: " + result);
    }

    private static int Calculate(int value)
    {
        if (value > 10)
        {
            return value * 2;
        }
        else
        {
            return value + 10;
        }
    }
}