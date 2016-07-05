using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class ItemGroup
    {
        [Key]
        public int ItemGroupId { get; set; }
        public string ItemCategory { get; set; }
    }
}
