using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class TransactionItem
    {
        [Key]
        public int TransactionItemId { get; set; }

        public int TransactionId { get; set; }
        public int ItemId { get; set; }
        public int ItemQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Cancelled { get; set; }
        public decimal ItemTotal { get; set; }
        public string Username { get; set; }
        public int RoomId { get; set; }
        //public DateTime EntryDate { get; set; }
        //public string EntryTime { get; set; }

    }
}
