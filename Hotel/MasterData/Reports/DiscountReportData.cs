using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.MasterData.Reports
{
   public  class DiscountReportData
    {
       
            public string ReportHeader { get; set; }
            public string PageHeader { get; set; }
            public string Title { get; set; }
            public List<DiscountReportDetail> details { get; set; }
        }

        public class DiscountReportDetail
        {
            public int DiscountId { get; set; }
            public String DiscountType { get; set; }
            public String DiscountName { get; set; }
            public decimal DiscountAmount { get; set; }


    }
}
