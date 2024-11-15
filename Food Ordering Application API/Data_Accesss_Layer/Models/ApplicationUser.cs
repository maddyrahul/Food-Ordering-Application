using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Data_Accesss_Layer.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Key]
       // public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Role { get; set; }

        public bool IsActive { get; set; } = true;

        public List<Order> Orders { get; set; }
        public List<Review> Reviews { get; set; }
    }

}
