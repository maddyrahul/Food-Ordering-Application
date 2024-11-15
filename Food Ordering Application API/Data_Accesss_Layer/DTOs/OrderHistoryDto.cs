using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accesss_Layer.DTOs
{
    public class OrderHistoryDto
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public decimal MenuPrice { get; set; }
    }
}
