using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }

        public string DiscountName { get; set; }
        public string DiscountType { get; set; }  
        public decimal DiscountAmount { get; set; }
    }
}
