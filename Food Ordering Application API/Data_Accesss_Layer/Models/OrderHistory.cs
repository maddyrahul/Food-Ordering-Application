using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accesss_Layer.Models
{
    public class OrderHistory
    {
     

        //  public int OrderHistoryId { get; set; }
        public string OrderHistoryId { get; set; } = Guid.NewGuid().ToString();
        // public int OrderId { get; set; }
        public string OrderId { get; set; } = Guid.NewGuid().ToString();
        public Order? Order { get; set; }
       // public int MenuId { get; set; }
        public string MenuId { get; set; } = Guid.NewGuid().ToString();
        public Menu? Menu { get; set; }

        public int Quantity { get; set; }
    }
}
