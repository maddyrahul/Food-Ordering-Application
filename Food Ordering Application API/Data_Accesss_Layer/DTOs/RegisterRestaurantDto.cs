using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accesss_Layer.DTOs
{
    public class RegisterRestaurantDto
    {
       // public int OwnerId { get; set; }
        public string OwnerId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Location { get; set; }
        public string Cuisine { get; set; }
    }
}
