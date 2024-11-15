using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accesss_Layer.DTOs
{
    public class UpdateUserRequest
    {
        public string NewEmail { get; set; }
        public string NewUsername { get; set; }
        public bool IsActive { get; set; }
    }
}
