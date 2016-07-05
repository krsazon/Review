using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    class RoomType
    {
        [Key]
        public int RoomTypeId { get; set; }

        public string RoomTypeName { get; set; }
        public decimal AdditionalChargesAmount { get; set; }
        public string AdditionalChargesTime { get; set; }
        public int AdditionalChargesNumberTime { get; set; }

    }
}
