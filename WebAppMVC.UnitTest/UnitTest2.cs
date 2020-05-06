using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebAppMVC.Models;

namespace WebAppMVC.UnitTest
{
    [TestClass]
    public class UnitTest2
    {
        private readonly List<Product> products = new List<Product>
        {
            new Product{ ProductID=1,Name="Kayak",Category="Watersports",Description="Name1_q",Price=275M },
            new Product{ ProductID=2,Name="Lifejacket",Category="Watersports",Description="Name1_q",Price=48.95M },
            new Product{ ProductID=3,Name="Soccer ball",Category="Soccer",Description="Name1_q",Price=19.50M },
            new Product{ ProductID=4,Name="Corner flag",Category="Soccer",Description="Name1_q",Price=34.95M },
        };

        [TestMethod]
        public void Sum_Products_Correctly()
        {
            //准备
            IDiscountHelper discounter = new MinimumDiscountHelper();
            IValueCalculator target = new LinqValueCalculatorNew(discounter);
            decimal goalTotal = products.Sum(e => e.Price);

            //准备-使用Moq
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            var newTarget = new LinqValueCalculatorNew(mock.Object);

            //动作
            var result = newTarget.ValueProducts(products);

            //断言
            Assert.AreEqual(goalTotal, result);
            Assert.AreEqual(products.Sum(e => e.Price), result);
        }

        private List<Product> createProduct(decimal value)
        {
            return new List<Product> { new Product { Price = value } };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Pass_Through_Variable_Discounts()
        {
            //准备
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == 0))).Throws<ArgumentOutOfRangeException>();
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100))).Returns<decimal>(total => total * 0.9M);
            mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10, 100, Range.Inclusive))).Returns<decimal>(total => total - 5);
            var target = new LinqValueCalculatorNew(mock.Object);

            //动作
            decimal FiveDollarDiscount = target.ValueProducts(createProduct(5));
            decimal TenDollarDiscount = target.ValueProducts(createProduct(10));
            decimal FiftyDollarDiscount = target.ValueProducts(createProduct(50));
            decimal HundredDollarDiscount = target.ValueProducts(createProduct(100));
            decimal FiveHundredDollarDiscount = target.ValueProducts(createProduct(500));

            //断言
            Assert.AreEqual(5, FiveDollarDiscount, "$5 Fail");
            Assert.AreEqual(5, TenDollarDiscount, "$10 Fail");
            Assert.AreEqual(45, FiftyDollarDiscount, "$50 Fail");
            Assert.AreEqual(95, HundredDollarDiscount, "$100 Fail");
            Assert.AreEqual(450, FiveHundredDollarDiscount, "$500 Fail");
            target.ValueProducts(createProduct(0));
        }
    }
}
