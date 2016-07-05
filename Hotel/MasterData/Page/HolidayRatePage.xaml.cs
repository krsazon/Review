using DevExpress.Xpf.WindowsUI;
using DevExpress.XtraReports.UI;
using Hotel.MasterData.Reports;
using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel.MasterData.Page
{
    /// <summary>
    /// Interaction logic for HolidayRatePage.xaml
    /// </summary>
    public partial class HolidayRatePage : UserControl
    {
        List<HolidayRate> Holiday = new List<HolidayRate>();

        int selectedId = 0;
        public HolidayRatePage()
        {
            InitializeComponent();
        }

        public HolidayRatePage(int id)
        {
            InitializeComponent();
            this.selectedId = id;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.HolidayRateWindow();
            MethodsClass.ShowPropertyWindow(window);
             Refresh();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        public void Refresh()
        {
            using (var context = new DatabaseContext())
            {
                Holiday = context.HolidayRates.ToList();
                if (txtSearch.Text != "")
                {
                    Holiday = Holiday.Where(c => c.RateName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
                }
                dgRateView.ItemsSource = null;
                dgRateView.ItemsSource = Holiday;
                viewRates.BestFitColumns();
                var total = context.HolidayRates.Select(c => c.RateId);
                blkTotal.Text = total.Count().ToString();
            }
        }

        private void dgRateView_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            if (dgRateView.SelectedItem != null)
            {
                var vr = dgRateView.SelectedItem as HolidayRate;
                txtHolidayName.Text = vr.RateName;
                txtDate.Text = vr.HolidayDate;
                txtRateType.Text = vr.RateType;
                txtRate.Text = vr.Rate.ToString();

            }
            else
            {
                txtHolidayName.Text = "";
                txtDate.Text = "";
                txtRateType.Text = "";
                txtRate.Text = "";
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            List<HolidayView> ViewList = new List<HolidayView>();
            using (var context = new DatabaseContext())
            {
                foreach (var hr in Holiday)
                {
                    ViewList.Add(new HolidayView()
                    {
                        RateId = hr.RateId,
                        HolidayName = hr.RateName,
                        HolidayDate = hr.HolidayDate,
                        RateType = hr.RateType,
                        Rate = hr.Rate
                    });
                }
            }
            var dataList = new List<HolidayRateReportData>();
            dataList.Add(new HolidayRateReportData()
            {
                Title = "Holiday Rates",
                ReportHeader = "Header",
                details = ViewList
            });

            var report = new HolidayReportDesign() { DataSource = dataList, Name = "Report" };
            using (var printTool = new ReportPrintTool(report))
            {
                printTool.ShowRibbonPreviewDialog();
            }
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Variables.collapseUserContent = true;
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            frame.BackNavigationMode = BackNavigationMode.Root;
            frame.GoBack();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgRateView.SelectedItem != null)
            {
                var selected = dgRateView.SelectedItem as HolidayRate;
                var window = new Windows.HolidayRateWindow(selected.RateId);
                MethodsClass.ShowPropertyWindow(window);
                Refresh();
            }
            else
            {
                MessageBox.Show("Select a row");
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgRateView.SelectedItem != null)
            {
                if (MethodsClass.ShowConfirmation("Do you really want to delete this record?") == true)
                {
                    var selected = dgRateView.SelectedItem as HolidayRate;
                    using (var context = new DatabaseContext())
                    {
                        var hol = context.HolidayRates.FirstOrDefault(c => c.RateId == selected.RateId);
                        context.HolidayRates.Remove(hol);
                        context.SaveChanges();
                    }
                    Refresh();
                }
            }
        }
    }
}

