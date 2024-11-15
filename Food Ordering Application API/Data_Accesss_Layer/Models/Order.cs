 
using System.ComponentModel.DataAnnotations;
 
namespace Data_Accesss_Layer.Models
{
    public class Order
    {
        [Key]
       // public int OrderId { get; set; }
        public string OrderId { get; set; } = Guid.NewGuid().ToString();

        //public int CustomerId { get; set; }
        public string CustomerId { get; set; } = Guid.NewGuid().ToString();

        public ApplicationUser Customer { get; set; }

        //public int RestaurantId { get; set; }
        public string RestaurantId { get; set; } = Guid.NewGuid().ToString();
        public Restaurant Restaurant { get; set; }

       // public string MenuId { get; set; } = Guid.NewGuid().ToString();
        public DateTime OrderDate { get; set; }

        [Required]
        public string Status { get; set; }

        public decimal TotalAmount { get; set; }

        public string? DisputeResolution { get; set; } // For admin dispute handling

        public ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();
    }
}
