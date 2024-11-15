using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Data_Accesss_Layer.Models
{
    
    public class Review
    {
        [Key]
       // public int ReviewId { get; set; }

        public string ReviewId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        //public int CustomerId { get; set; }
        public string CustomerId { get; set; } = Guid.NewGuid().ToString();

        [JsonIgnore]
        public ApplicationUser? Customer { get; set; }

        [Required]
        //public int RestaurantId { get; set; }
        public string RestaurantId { get; set; } = Guid.NewGuid().ToString();

        [JsonIgnore]
        public Restaurant? Restaurant { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; } 

        public string? Response {  get; set; }// Restaurant owner's response

        public DateTime DatePosted { get; set; } = DateTime.UtcNow;
    }
}
