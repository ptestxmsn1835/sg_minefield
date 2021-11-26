using System;
using System.Collections.Generic;

namespace MinefieldConsoleGame
{
    public class IOHelper : IIOHelper
    {
        public ConsoleKey GetCharInput(string msg, List<ConsoleKey> allowedKeys)
        {
            while (true)
            {
                if (!string.IsNullOrEmpty(msg))
                {
                    Write(msg);
                }

                ConsoleKey input = Console.ReadKey().Key;
                if (!allowedKeys.Contains(input))
                {
                    Write($"Error - Char must be one of the following - {string.Join(" ,", allowedKeys)}");
                }
                else
                {
                    return input;
                }
            }
        }

        public int GetNumericInput(string msg, int minNumber = 0, int maxNumber = 10000)
        {
            while (true)
            {
                if (!string.IsNullOrEmpty(msg))
                {
                    Write(msg);
                }

                string input = Console.ReadLine().Trim();
                bool result = int.TryParse(input, out int number);
                if (!result)
                {
                    Write("Error - Numeric Input required");
                }
                else if (number < minNumber || number > maxNumber)
                {
                    Write($"Error - Number must be between {minNumber} and {maxNumber}");
                }
                else
                {
                    return number;
                }
            }
        }

        public string GetTextInput(string msg, int minLength = 1, int maxLength = 100)
        {
            while (true)
            {
                if (!string.IsNullOrEmpty(msg))
                {
                    Write(msg);
                }

                string input = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Write("Error - Text required. Empty text or text consisting of spaces are invalid.");
                }
                else if (input.Length < minLength || input.Length > maxLength)
                {
                    Write($"Error - Text must be between {minLength} and {maxLength}.");
                }
                else
                {
                    return input;
                }
            }
        }

        public void Write(string output)
        {
            Console.WriteLine(output);
        }
    }
}