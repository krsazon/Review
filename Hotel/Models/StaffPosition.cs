using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    class StaffPosition
    {
        [Key]
        public int StaffPositionId { get; set; }

        public string StaffPositionName { get; set; }
        public Boolean Assist { get; set; }
    }
}
