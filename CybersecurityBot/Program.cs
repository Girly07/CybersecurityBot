using System;
using System.Media; // for audio greeting
using System.Threading;

namespace CybersecurityBot
{
    interface IResponder
    {
        string GetResponse(string userInput);
    }

    abstract class BaseResponder : IResponder
    {
        public abstract string GetResponse(string userInput);

        protected string NormalizeInput(string input)
        {
            if (input == null)
                return "";

            return input.Trim().ToLower();
        }
    }
}
