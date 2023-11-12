using System.ComponentModel.DataAnnotations;

namespace VivesShop.Cyb.Ui.Mvc.Models
{
    public class CartItem
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
