using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
   public class Parameter
    {
        [Key]
        public int ParameterId { get; set; }

        public string HotelName { get; set; }
        public string HotelDescription { get; set; }
        public string HotelAddress { get; set; }
        public String CheckInTimeStart { get; set; }
        public String CheckInTimeEnd { get; set; }
        public String CheckOutTimeStart { get; set; }
        public String CheckOutTimeEnd { get; set; }
        public Boolean TimePolicyEnabled { get; set; }
        public Boolean PetPolicyEnable { get; set; }
        public int PetRate { get; set; }
    }
}
