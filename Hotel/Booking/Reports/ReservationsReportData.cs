using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Booking.Reports
{
    class ReservationsReportData
    {
        public String Title { get; set; }
        public String Header { get; set; }
        public List<ReservationView> details { get; set; }
    }
}
