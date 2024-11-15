using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accesss_Layer.DTOs
{
    public class OrderDetailsDto
    {
        public string CustomerId { get; set; }
        public string RestaurantId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string DisputeResolution { get; set; }
        public List<OrderHistoryDto> OrderHistory { get; set; }
    }
}
