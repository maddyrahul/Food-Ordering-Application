using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accesss_Layer.DTOs
{
    public class RegisterModelDTO
    {
        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
