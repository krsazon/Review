using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.MasterData.Reports
{
    class ItemReportData
    {
        public string ReportHeader { get; set; }
        public string Title { get; set; }
        public List<ItemListView> details { get; set; }
    }
}
