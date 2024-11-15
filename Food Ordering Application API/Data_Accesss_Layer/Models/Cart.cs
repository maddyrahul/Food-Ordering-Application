 
using System.ComponentModel.DataAnnotations;

namespace Data_Accesss_Layer.Models
{
   
    public class Cart
    {
        [Key]
        //  public int CartId { get; set; }
        public string CartId { get; set; } = Guid.NewGuid().ToString();

      //  public int CustomerId { get; set; }

        public string CustomerId { get; set; } = Guid.NewGuid().ToString();
        public ApplicationUser Customer { get; set; }

        //  public int MenuId { get; set; }
        public string MenuId { get; set; } = Guid.NewGuid().ToString();
        public Menu Menu { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
