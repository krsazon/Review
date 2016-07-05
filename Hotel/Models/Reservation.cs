using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
   public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        public String ReservationNumber { get; set; }
        public decimal ReservationFee { get; set; }
        public string CustomerName { get; set; }
        public int RoomId { get; set; }
        public DateTime DateReserved { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string ArrivalTime { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestTime { get; set; }
        public string CardNumber { get; set; }
        public string Status { get; set; }
        

    }
}
