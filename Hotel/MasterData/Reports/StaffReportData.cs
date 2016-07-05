using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.MasterData.Reports
{
   public class StaffReportData
    {

        public string ReportHeader { get; set; }
        public string PageHeader { get; set; }
        public string Title { get; set; }
        public List<StaffReportDetail> details { get; set; }
    }

   public class StaffReportDetail
   {
       public int StaffId { get; set; }
       public String StaffName { get; set; }
       public String StaffPosition { get; set; }

   }
}
