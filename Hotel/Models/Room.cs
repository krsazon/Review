using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        public String RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomDescription { get; set; }
        public string RoomEquipmentId { get; set; }
        public string Status { get; set; }
        public string BuildingFloor { get; set; }
        public int Capacity { get; set; }
        public string RoomSize { get; set; }
        public string Smoking { get; set; }
    }
}
