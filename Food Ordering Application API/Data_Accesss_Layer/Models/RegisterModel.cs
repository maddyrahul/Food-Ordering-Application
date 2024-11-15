/*using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accesss_Layer.Models
{
    public class RegisterModel
    {
        [Key]  // Primary Key
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string? Username { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Role { get; set; }  // Role can be "Organizer" or "Attendee"

        public bool IsActive { get; set; } = true;

        public List<Order> Orders { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
*/