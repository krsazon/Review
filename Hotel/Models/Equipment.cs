using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    class Equipment
    {

        [Key]
        public int EquipmentId { get; set; }

        public string EquipmentName { get; set; }

    }
}
