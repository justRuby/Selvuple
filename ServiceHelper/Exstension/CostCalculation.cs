using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHelper.Exstension
{
    public static class CostCalculation
    {
        public static double GetCostWithDiscount(int score)
        {
            double discountMAX = 0.1;
            double discount = 0;

            switch (score)
            {
                case 10:
                    discount = 0.02; //2%
                    break;
                case 50:
                    discount = 0.04; //4%
                    break;
                case 100:
                    discount = 0.07; //7%
                    break;
                case 250:
                    discount = 0.1; //10%
                    break;

                default:
                    break;
            }

            if (discount > discountMAX)
                discount = discountMAX;



            return discount;
        }
    }
}
