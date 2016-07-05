using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class OccupancyRate
    {

    [Key]
        public int OccupancyRateId { get; set; }

    public string OccupancyRateName { get; set; }
    public string OccupancyRateType { get; set; }
    public decimal OccupancyRateAmount  { get; set; }
    }
}
