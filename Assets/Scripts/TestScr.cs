using System;
using System.Collections.Generic;

public class Example
{
    public static void Main()
    {
        int count = 0;
        string input = string.Empty;
        
        char first = input[0];

        List<string> list = new List<string>();
        while (true)
        {
            input = Console.ReadLine();
            list.Add(input);
            if (string.IsNullOrWhiteSpace(input))
            {
                break;
            }
        }

        int index = 0;
        string[] arr = new string[list.Count];
        arr[index] = list[0];
        index++;
        int left = 0;
        int right = list.Count - 1;
        int mid = (left + right) / 2;
        for(int i = 1; i < list.Count; i++)
        {
            mid = (left + right) / 2;
            if (list[i][0] == arr[mid][0])
            {
                if(list[i][1] == arr[mid][1])
                {

                }
            }
            else if(list[i][0] < arr[mid][0])
            {
                right = mid - 1;
            }
            else if (list[i][0] > arr[mid][0])
            {
                left = mid + 1;
            }
            arr[index] = list[i];

        }


        Console.WriteLine("Hello World!");
    }
}