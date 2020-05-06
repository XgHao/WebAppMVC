using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMVC.Models
{
    public class ShoppingCart
    {
        private IValueCalculator calc;

        public ShoppingCart(IValueCalculator _calc)
        {
            calc = _calc;
        }

        public IEnumerable<Product> products { get; set; }

        public decimal CalculateProductTotal()
        {
            return calc.ValueProducts(products);
        }

        //public List<Product> Products { get; set; }

        //public IEnumerator<Product> GetEnumerator()
        //{
        //    return Products.GetEnumerator();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}
    }
}