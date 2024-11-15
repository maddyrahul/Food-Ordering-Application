using System.ComponentModel.DataAnnotations;

namespace Data_Accesss_Layer.Models
{
    
    public class Menu
    {
        [Key]
        // public int MenuId { get; set; }
        public string MenuId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        //  public int RestaurantId { get; set; }
        public string RestaurantId { get; set; } = Guid.NewGuid().ToString();
        public Restaurant Restaurant { get; set; }
    }

}
