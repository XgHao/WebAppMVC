using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMVC.Models
{
    public interface IDiscountHelper
    {
        decimal ApplyDiscount(decimal total);
    }

    public class DefaultDiscountHelper : IDiscountHelper
    {
        public decimal discountSize;

        public DefaultDiscountHelper(decimal _discounterSize)
        {
            discountSize = _discounterSize;
        }

        public decimal ApplyDiscount(decimal total)
        {
            return total - (discountSize / 100m * total);
        }
    }
}