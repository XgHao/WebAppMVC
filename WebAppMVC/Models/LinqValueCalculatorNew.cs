using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebAppMVC.Models
{
    public class LinqValueCalculatorNew : IValueCalculator
    {
        private IDiscountHelper discounter;
        private static int counter = 0;

        public LinqValueCalculatorNew(IDiscountHelper _discounter)
        {
            discounter = _discounter;
            Debug.WriteLine($"Instance {++counter} created");
        }
        public decimal ValueProducts(IEnumerable<Product> products)
        {
            return discounter.ApplyDiscount(products.Sum(p => p.Price));
        }
    }
}