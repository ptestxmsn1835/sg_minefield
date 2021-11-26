using System;
using System.Collections.Generic;

namespace MinefieldConsoleGame
{
    public interface IIOHelper
    {
        ConsoleKey GetCharInput(string msg, List<ConsoleKey> allowedKeys);
        int GetNumericInput(string msg, int minNumber = 0, int maxNumber = 10000);
        string GetTextInput(string msg, int minLength = 1, int maxLength = 100);
        void Write(string output);
    }
}