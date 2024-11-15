using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accesss_Layer.DTOs
{
    public class AddToCartDto
    {
        //public int CustomerId { get; set; }
        public string CustomerId { get; set; } = Guid.NewGuid().ToString();
        // public int MenuId { get; set; }
        public string MenuId { get; set; } = Guid.NewGuid().ToString();

        public int Quantity { get; set; }
    }
}
