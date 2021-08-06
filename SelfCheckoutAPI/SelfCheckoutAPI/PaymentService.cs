using SelfCheckoutAPI.Exceptions;

namespace SelfCheckoutAPI
{
    public static class PaymentService
    {
        public static int CalculateChange(int insertedMoney, int price)
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

    }
}