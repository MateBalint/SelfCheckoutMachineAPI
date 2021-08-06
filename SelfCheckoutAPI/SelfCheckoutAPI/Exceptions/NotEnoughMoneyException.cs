using System;

namespace SelfCheckoutAPI.Exceptions
{
    public class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException()
        {
        }

        public NotEnoughMoneyException(string message) : base(message)
        {
        }
    }
}