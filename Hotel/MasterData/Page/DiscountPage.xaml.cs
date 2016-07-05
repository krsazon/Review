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
    /// Interaction logic for DiscountView.xaml
    /// </summary>
    public partial class DiscountPage : UserControl
    {
        List<Discount> discountList = new List<Discount>();

        public DiscountPage()
        {
            InitializeComponent();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            discountList.Clear();
            using (var context = new DatabaseContext())
            {
                var Discounts = context.Discounts.ToList();
                Discounts = context.Discounts.ToList();

                if (txtSearch.Text != "")
                {
                 Discounts = Discounts.Where(c => c.DiscountName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
                }
                dgDiscount.ItemsSource = null;
                dgDiscount.ItemsSource = Discounts;
                viewDiscounts.BestFitColumns();

                var total = context.ItemGroups.Select(c => c.ItemGroupId);               
                blkTotal.Text = total.Count().ToString();
            }
        }
     private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.DiscountsWindow();
            MethodsClass.ShowPropertyWindow(window);
            Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgDiscount.SelectedItem != null)
            {
                if (MethodsClass.ShowConfirmation("Do you really want to delete this record?") == true)
                {
                    var selected = dgDiscount.SelectedItem as Discount;
                    using (var context = new DatabaseContext())
                    {
                        var discount = context.Discounts.FirstOrDefault(c => c.DiscountId == selected.DiscountId);
                        context.Discounts.Remove(discount);
                        context.SaveChanges();
                        MethodsClass.ShowNotification("Record successfully removed.");
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
            if (dgDiscount.SelectedItem != null)
            {
                var selected = dgDiscount.SelectedItem as Discount;
                var discount = new Windows.DiscountsWindow(selected.DiscountId);
                MethodsClass.ShowPropertyWindow(discount);
                Refresh();
            }
            else if (dgDiscount.SelectedItem == null)
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
            List<DiscountReportDetail> details = new List<DiscountReportDetail>();

            using (var context = new DatabaseContext())
            {
                //list records to details 
                foreach (var discount in discountList)
                {
                    details.Add(new DiscountReportDetail()
                    {
                        DiscountId = discount.DiscountId,
                        DiscountType = discount.DiscountType,
                        DiscountName = discount.DiscountName,
                        DiscountAmount = discount.DiscountAmount

                    });
                }


                //create data list as datasource
                var dataList = new List<DiscountReportData>();
                dataList.Add(new DiscountReportData()
                {
                    ReportHeader = "DISCOUNT",
                    PageHeader = "Discount Report ",
                    Title = "Discounts",
                    details = details,
                });

                //
                var report = new DiscountReportDesign() { DataSource = dataList, Name = "Discounts List" };
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

        private void dgDiscount_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            if (dgDiscount.SelectedItem != null)
            {
                var discount = dgDiscount.SelectedItem as Discount;
                txtDiscountType.Text = discount.DiscountType;
                txtDiscountName.Text = discount.DiscountName;
                txtDiscountAmount.Text = discount.DiscountAmount.ToString();
            }
            else
            {
                txtDiscountType.Text = "";
                txtDiscountName.Text = "";
                txtDiscountAmount.Text = "";
            }
        }

    }
}
  
