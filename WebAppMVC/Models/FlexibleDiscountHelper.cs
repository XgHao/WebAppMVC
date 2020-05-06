using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMVC.Models
{
    public class FlexibleDiscountHelper : IDiscountHelper
    {
        public int Flexible { get; set; }
        public decimal ApplyDiscount(decimal total)
        {
            decimal discount = total > 100 ? 70 : 25;
            return total - (discount / 100m * total);
        }
    }
}