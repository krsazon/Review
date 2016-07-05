using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class RoomEquipment
    {
        [Key]
        public int RoomEquipmentId { get; set; }
        public int Quantity { get; set; }
        public int RoomId { get; set; }
        public string Equipment { get; set; }
    }
}
