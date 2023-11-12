using Microsoft.EntityFrameworkCore;
using VivesShop.Cyb.Ui.Mvc.Models;

namespace VivesShop.Cyb.Ui.Mvc.Core
{
    public class VivesShopDbContext: DbContext
    {
        public VivesShopDbContext(DbContextOptions<VivesShopDbContext> options): base(options) { }

        public DbSet<ShopItem> Items => Set<ShopItem>();
        public DbSet<CartItem> Cart => Set<CartItem>();

        public void Seed()
        {
            Items.AddRange(new List<ShopItem>
            {
                new ShopItem {ItemName = "Bicky Burger", Price = 4.5f},
                new ShopItem {ItemName = "Brasilsaus", Price = 1.7f},
                new ShopItem {ItemName = "Medium friet", Price = 3f},
                new ShopItem {ItemName = "Frikandel", Price = 2f},
                new ShopItem {ItemName = "Cola Zero", Price = 2f},
                new ShopItem {ItemName = "Water", Price = 1.5f},
                new ShopItem {ItemName = "Mayonaisse", Price = 0.5f},
            });

            SaveChanges();
        }
    }
}
