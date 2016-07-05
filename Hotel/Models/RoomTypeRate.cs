using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
  public class RoomTypeRate
    {
        [Key]
        public int RoomTypeRateId { get; set; }

        public int RoomTypeId { get; set; }
        public decimal Amount { get; set; }
        public string AmountTime { get; set; }
        public int AmountNumberTime { get; set; }
    }
}
