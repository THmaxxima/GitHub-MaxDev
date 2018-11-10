using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodStore.Web.Models;
using FoodStore.Services;
using FoodStore.Models;

namespace FoodStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly App app;

        public HomeController(App app)
        {
            this.app = app;
        }
    
        public IActionResult Index(int count)
        {
            var products = app.Products.All; //app.Products.MostExpensiveProducts(count);
            return View(products);
        }

        public IActionResult About()
        {
            var ms = DateTime.Now.Millisecond;

            for (int i = 0; i < 5; i++)
            {
                var p = new Product
                {
                    Name = $"Product {(ms * 10) + i}",
                    Price = 100
                };
                app.Products.Add(p);
            }

            app.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
