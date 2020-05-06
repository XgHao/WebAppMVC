using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMVC.Models
{
    public class MinimumDiscountHelper : IDiscountHelper
    {
        public int Minimum { get; set; }

        public decimal ApplyDiscount(decimal total)
        {
            if (total < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (total < 10)
            {
                return total;
            }
            else if (total <= 100)
            {
                return total - 5;
            }
            else
            {
                return total * 0.9M;
            }
        }
    }
}