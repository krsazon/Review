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
    /// Interaction logic for StaffView.xaml
    /// </summary>
    public partial class StaffPage : UserControl
    {
        List<Staff> staffList = new List<Staff>();

        public StaffPage()
        {
            InitializeComponent();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
           staffList.Clear();
            using (var context = new DatabaseContext())
            {
                var Staffs = context.Staffs.ToList();
                if (txtSearch.Text != "")
                {
                    Staffs = Staffs.Where(c => c.StaffName.ToLower().Contains(txtSearch.Text.ToLower()) || c.StaffPosition.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
                }

                foreach (var staff in Staffs)
                {
                    staffList.Add(new Staff()
                    {
                        StaffId = staff.StaffId,
                        StaffName = staff.StaffName,
                        StaffPosition = staff.StaffPosition,

                    });
                }
                dgStaffs.ItemsSource = null;
                dgStaffs.ItemsSource = Staffs;
                viewStaffs.BestFitColumns();
                var total = context.Staffs.Select(c => c.StaffId);
                blkTotal.Text = total.Count().ToString();
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.StaffWindow();
            MethodsClass.ShowPropertyWindow(window);
            Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgStaffs.SelectedItem != null)
            {
                if (MethodsClass.ShowConfirmation("Do you really want to delete this record?") == true)
                {
                    var selected = dgStaffs.SelectedItem as Staff;
                    using (var context = new DatabaseContext())
                    {
                        var staff = context.Staffs.FirstOrDefault(c => c.StaffId == selected.StaffId);
                        context.Staffs.Remove(staff);
                        context.SaveChanges();
                        Refresh();
                    }

                }
            }
            else
            {
               MethodsClass.ShowNotification("Please select a row.");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgStaffs.SelectedItem != null)
            {
                var selected = dgStaffs.SelectedItem as Staff;
                var staff = new Windows.StaffWindow(selected.StaffId);
                MethodsClass.ShowPropertyWindow(staff);
                Refresh();
            }
            else if (dgStaffs.SelectedItem == null)
            {
               MethodsClass.ShowNotification("Please select a row.");
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Variables.collapseUserContent = true;
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            frame.BackNavigationMode = BackNavigationMode.Root;
            frame.GoBack();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            List<StaffReportDetail> details = new List<StaffReportDetail>();

            using (var context = new DatabaseContext())
            {
                //list records to details 
                foreach (var staff in staffList)
                {
                    details.Add(new StaffReportDetail()
                    {
                        StaffId = staff.StaffId,
                        StaffName = staff.StaffName,
                        StaffPosition = staff.StaffPosition,

                    });
                }


                //create data list as datasource
                var dataList = new List<StaffReportData>();
                dataList.Add(new StaffReportData()
                {
                    ReportHeader = "STAFFs",
                    PageHeader = "Staffs Report ",
                    Title = "Staffs",
                    details = details
                });

                //
                var report = new StaffReportDesign() { DataSource = dataList, Name = "Staffs List" };
                using (var printTool = new ReportPrintTool(report))
                {
                    printTool.ShowRibbonPreviewDialog();
                }
            }
        }
        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void dgStaffs_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            if (dgStaffs.SelectedItem != null)
            {
                var staff = dgStaffs.SelectedItem as Staff;
                txtStaffName.Text = staff.StaffName;
                txtStaffPosition.Text = staff.StaffPosition;
            }
            else
            {
                txtStaffName.Text = "";
                txtStaffPosition.Text = "";

            }
        }
    }
}