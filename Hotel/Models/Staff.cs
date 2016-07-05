using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
   public class Staff
    {
       [Key]
        public int StaffId { get; set; }

        public string StaffName { get; set; }
        public string StaffPosition { get; set; }
    }
}
