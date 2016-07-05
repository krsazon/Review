using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
   public class HolidayRate
    {
       [Key]
        public int RateId { get; set; }

       public decimal Rate { get; set; }
       public string RateName { get; set; }
       public string RateType { get; set; }
       public string HolidayDate { get; set; }
    }
}
