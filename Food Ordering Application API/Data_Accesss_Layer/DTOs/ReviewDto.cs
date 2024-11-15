using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accesss_Layer.DTOs
{
    public class ReviewDto
    {
     //   public int Id { get; set; }
        public string Id { get; set; } = Guid.NewGuid().ToString();

        //  public int RestaurantId { get; set; }
        public string RestaurantId { get; set; } = Guid.NewGuid().ToString();

        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string Response { get; set; }
    }
}
