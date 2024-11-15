using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accesss_Layer.DTOs
{
    public class ReviewResponseDto
    {
       // public int ReviewId { get; set; }
        public string ReviewId { get; set; } = Guid.NewGuid().ToString();
        public string Response { get; set; }
    }
}
