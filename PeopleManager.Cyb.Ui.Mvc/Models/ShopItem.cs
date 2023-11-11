using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VivesShop.Cyb.Ui.Mvc.Models
{
    [Table(nameof(ShopItem))]
    public class ShopItem
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public required string ItemName { get; set; }

        [Required]
        [Display(Name = "Price")]
        public required float Price { get; set; }
    }
}
