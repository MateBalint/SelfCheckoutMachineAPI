using SelfCheckoutAPI.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace SelfCheckoutAPI
{
    public static class PaymentService
    {
        private static List<int> Denominations = new List<int>() { 5, 10, 20, 50, 100, 200, 500, 1000, 5000, 10000, 20000 };

        /// <summary>
        /// Determines the amount of the change.
        /// </summary>
        /// <param name="insertedMoney">The money that was inserted into the machine.</param>
        /// <param name="price">Amount of money the user has to pay.</param>
        /// <returns>A dictionary whose key is the denomination and the value is the amount of the denomination.</returns>
        public static Dictionary<string, int> CalculateChange(int insertedMoney, int price)
        {
            var changeDict = new Dictionary<string, int>();
            if (insertedMoney == price)
            {
                changeDict.Add("No change", 0);
            }
            else if (insertedMoney < price)
            {
                throw new NotEnoughMoneyException("The inserted money is not enough to cover the price!");
            }
            else if (insertedMoney > price)
            {
                int difference = insertedMoney - price;
                changeDict = CalculateChangeDenomintations(difference);
            }

            return changeDict;
        }

        /// <summary>
        /// Calculates the sum of the given dictionary values using the keys.
        /// </summary>
        /// <param name="insertedMoney">Dicionary whose key is the denomination and the value is the amount of the denomination.</param>
        /// <returns>The sum of the dictionary values.</returns>
        public static int CalculateInsertedMoney(Dictionary<string, int> insertedMoney)
        {
            int money = 0;
            foreach (var item in insertedMoney)
            {
                money += int.Parse(item.Key) * item.Value;
            }

            return money;
        }

        /// <summary>
        /// Calculates the denominations and the amount of them that will be given back as a change.
        /// </summary>
        /// <param name="changeAmount">The sum of the change.</param>
        /// <returns>A dictionary whose key is the denomination and the value is the amount of the denomination.</returns>
        public static Dictionary<string, int> CalculateChangeDenomintations(int changeAmount)
        {
            var changeDict = new Dictionary<string, int>();
            List<int> descendingList = Denominations.OrderByDescending(x => x).ToList();
            foreach (var item in descendingList)
            {
                if (changeAmount > 0)
                {
                    int amount = changeAmount / item;
                    if (amount > 0)
                    {
                        changeDict.Add(item.ToString(), amount);
                        changeAmount -= (item * amount); 
                    }
                }
            }

            return changeDict;
        }

        /// <summary>
        /// Calculates the change based on the available denominations.
        /// </summary>
        /// <param name="changeAmount">The amount of the change that should be given back.</param>
        /// <param name="availableDenominations">The currently available denominations the program can give back change from.</param>
        /// <returns>
        /// The amount of the change in a dictionary if no exception happened, otherwise throws exception.
        /// </returns>
        public static Dictionary<string, int> CalculateChangeDenomintations(int changeAmount, Dictionary<string, int> availableDenominations)
        {
            var changeDict = new Dictionary<string, int>();
            List<int> availableDenominationList = DictionaryService.CreateList(availableDenominations);
            foreach (var item in availableDenominationList)
            {
                if (changeAmount > 0)
                {
                    int amount = changeAmount / item;
                    if (amount > 0)
                    {
                        changeDict.Add(item.ToString(), amount);
                        changeAmount -= (item * amount);
                    }
                }
            }

            if (changeAmount != 0)
            {
                throw new NotEnoughMoneyException("There's not enough money to give proper change!");
            }


            return changeDict;
        }
    }
}