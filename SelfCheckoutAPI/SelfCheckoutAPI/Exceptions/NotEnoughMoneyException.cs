using System;

namespace SelfCheckoutAPI.Exceptions
{
    /// <summary>
    /// Custome exception to throw when the supplied money is not enough for the payment process.
    /// </summary>
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