using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        public int ItemGroupId { get; set; }
        public string ItemName { get; set; }
        public decimal ItemAmount { get; set; }
    }
}
