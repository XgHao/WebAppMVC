using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMVC.Models
{
    public static class ExtendMethods
    {
        public static decimal TotalPrices(this IEnumerable<Product> productEnum)
        {
            decimal total = 0;
            foreach (var item in productEnum)
            {
                total += item.Price;
            }
            return total;
        }

        public static IEnumerable<Product> FilterByCategory(this IEnumerable<Product> products, Func<Product, bool> selectorParam)
        {
            foreach (var item in products)
            {
                if (selectorParam(item)) 
                {
                    yield return item;
                }
            }
        }
    }
}