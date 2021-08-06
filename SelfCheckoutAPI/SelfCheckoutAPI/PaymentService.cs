using SelfCheckoutAPI.Exceptions;
using System;
using System.Collections.Generic;

namespace SelfCheckoutAPI
{
    public static class PaymentService
    {
        public static int CalculateChange(int insertedMoney, int price, List<int> money)
        {
            int change = 0;
            if (insertedMoney == price)
            {
                change = 0;
            }
            else if (insertedMoney < price)
            {
                throw new NotEnoughMoneyException("The inserted money is not enough to cover the price!");
            }

            return change;
        }


        public static int CalculateInsertedMoney(Dictionary<string, int> insertedMoney)
        {
            int money = 0;
            foreach (var item in insertedMoney)
            {
                money += Int32.Parse(item.Key) * item.Value;
            }

            return money;
        }
    }
}