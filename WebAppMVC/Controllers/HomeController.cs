using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IValueCalculator calc;
        public HomeController(IValueCalculator _calc, IValueCalculator _calc2, IValueCalculator _calc3)
        {
            calc = _calc;
        }


        private List<Product> products = new List<Product>
        {
            new Product{ ProductID=1,Name="Kayak",Category="Watersports",Description="Name1_q",Price=275M },
            new Product{ ProductID=2,Name="Lifejacket",Category="Watersports",Description="Name1_q",Price=48.95M },
            new Product{ ProductID=3,Name="Soccer ball",Category="Soccer",Description="Name1_q",Price=19.50M },
            new Product{ ProductID=4,Name="Corner flag",Category="Soccer",Description="Name1_q",Price=34.95M },
        };

        // GET: Home
        public ActionResult Index()
        {
            ShoppingCart cart = new ShoppingCart(calc) { products = products };
            decimal totalValue = cart.CalculateProductTotal();
            return View(totalValue);
        }
    }
}