using Microsoft.EntityFrameworkCore;
using VivesShop.Cyb.Ui.Mvc.Models;

namespace VivesShop.Cyb.Ui.Mvc.Core
{
    public class VivesShopDbContext: DbContext
    {
        public VivesShopDbContext(DbContextOptions<VivesShopDbContext> options): base(options)
        {
            
        }

        public DbSet<ShopItem> Items => Set<ShopItem>();

        public void Seed()
        {
            Items.AddRange(new List<ShopItem>
            {
                new ShopItem {ItemName = "Bicky Burger", Price = 4.5f},
                new ShopItem {ItemName = "Brasilsaus", Price = 1.7f},
            });
            SaveChanges();
        }
    }
}
