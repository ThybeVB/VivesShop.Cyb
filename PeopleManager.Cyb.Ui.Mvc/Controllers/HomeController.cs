using Microsoft.AspNetCore.Mvc;
using VivesShop.Cyb.Ui.Mvc.Models;
using System.Diagnostics;
using System.Dynamic;
using VivesShop.Cyb.Ui.Mvc.Core;

namespace VivesShop.Cyb.Ui.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly VivesShopDbContext _dbContext;

        public HomeController(VivesShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            dynamic models = new ExpandoObject();
            models.Items = _dbContext.Items.ToList();
            models.Cart = _dbContext.Cart.ToList();
            ViewBag.Price = CalculatePrice();
            return View(models);
        }

        [HttpGet]
        public IActionResult AddToCart([FromRoute] int id)
        {
            var dbItem = _dbContext.Items.FirstOrDefault(p => p.Id == id);
            if (!ModelState.IsValid || dbItem == null)
            {
                return RedirectToAction("Index");
            }

            _dbContext.Cart.Add(new CartItem{ItemName = dbItem.ItemName, Price = dbItem.Price});
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult RemoveFromCart([FromRoute] int id)
        {
            var dbItem = _dbContext.Cart.FirstOrDefault(p => p.Id == id);
            if (!ModelState.IsValid || dbItem == null)
            {
                return RedirectToAction("Index");
            }

            _dbContext.Cart.Remove(dbItem);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = _dbContext.Cart.ToList();
            ViewBag.Price = CalculatePrice();
            return View(cart);
        }

        private float CalculatePrice()
        {
            float price = 0;
            foreach (var shopItem in _dbContext.Cart)
            {
                price += shopItem.Price;
            }
            
            return price;
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