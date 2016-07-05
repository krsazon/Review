using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        public int RoomTypeId { get; set; }
        public int RoomId { get; set; }
        public int GuestNumber { get; set; }
        public int ReservationId { get; set; }
        public int RoomSlipNumber { get; set; }
        public DateTime CheckInDate { get; set; }
        public String CheckInTime { get; set; }
        public int DiscountCard { get; set; }
        public int DiscountId { get; set; }
        public int StaffId { get; set; }
        public DateTime CheckOutDate { get; set; }
        public String CheckOutTime { get; set; }
        public Decimal RoomCharge { get; set; }
        public String Username { get; set; }
        public Decimal DiscountAmount { get; set; }
        public Decimal NetAmount { get; set; }
        public Decimal AmountPaid { get; set; }
        public Decimal Change { get; set; }
             public String Status { get; set; }
    }
}
