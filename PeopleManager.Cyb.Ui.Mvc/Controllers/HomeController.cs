using Microsoft.AspNetCore.Mvc;
using VivesShop.Cyb.Ui.Mvc.Models;
using System.Diagnostics;
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
            var people = _dbContext.Items.ToList();
            return View(people);
        }

        public IActionResult Details(int id)
        {
            var person = _dbContext.Items.SingleOrDefault(p => p.Id == id);
            if (person is null)
            {
                return RedirectToAction("Index");
            }
            return View(person);
        }

        public IActionResult About()
        {
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