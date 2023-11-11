using Microsoft.AspNetCore.Mvc;
using VivesShop.Cyb.Ui.Mvc.Core;
using VivesShop.Cyb.Ui.Mvc.Models;

namespace VivesShop.Cyb.Ui.Mvc.Controllers
{
    public class ItemController : Controller
    {
        private readonly VivesShopDbContext _dbContext;

        public ItemController(VivesShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var items = _dbContext.Items.ToList();
            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ShopItem item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            _dbContext.Items.Add(item);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int id)
        {
            var item = _dbContext.Items.FirstOrDefault(p => p.Id == id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, ShopItem item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            var dbItem = _dbContext.Items.FirstOrDefault(p => p.Id == id);

            if (dbItem == null)
            {
                return RedirectToAction("Index");
            }

            dbItem.ItemName = item.ItemName;
            dbItem.Price = item.Price;

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete([FromRoute]int id)
        {
            var item = _dbContext.Items.FirstOrDefault(p => p.Id == id);
            return View(item);
        }

        [HttpPost("/item/delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed([FromRoute]int id)
        {
            var dbItem = _dbContext.Items.FirstOrDefault(p => p.Id == id);

            if (dbItem is null)
            {
                return RedirectToAction("Index");
            }

            _dbContext.Items.Remove(dbItem);

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
