using System;

public class Example
{
    public static void Main()
    {
        string input = string.Empty;
        
        char first = input[0];


        while (true)
        {
            input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                break;
            }
        }

        Console.WriteLine("Hello World!");
    }
}