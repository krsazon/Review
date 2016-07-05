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
    /// Interaction logic for ItemView.xaml
    /// </summary>
    public partial class ItemPage : UserControl
    {
  List<Item> Items = new List<Item>();
       public ItemPage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {

            using (var context = new DatabaseContext())
            {
               Items = context.Items.ToList();

                var viewList = new List<ItemListView>();
                if (txtSearch.Text != "")
                {
                    Items = Items.Where(c => c.ItemName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
                }
                foreach (var list in Items)
                {
                    var category = context.ItemGroups.FirstOrDefault(c => c.ItemGroupId == list.ItemGroupId);

                    viewList.Add(new ItemListView()
                    {
                       ItemId = list.ItemId,
                       ItemGroupId = category.ItemCategory,
                       ItemName = list.ItemName,
                       Amount = list.ItemAmount
                    });
                }
                dgItemView.ItemsSource = null;
                dgItemView.ItemsSource = viewList;
                viewItems.BestFitColumns();
                var total = context.Items.Select(c => c.ItemId);
                blkTotal.Text = total.Count().ToString();
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.ItemWindow();
            MethodsClass.ShowPropertyWindow(window);
            Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgItemView.SelectedItem != null)
            {
                if (MethodsClass.ShowConfirmation("Do you really want to delete this record?") == true)
                {
                    var selected = dgItemView.SelectedItem as ItemListView;
                    using (var context = new DatabaseContext())
                    {
                        var item = context.Items.FirstOrDefault(c => c.ItemId == selected.ItemId);
                        context.Items.Remove(item);
                        context.SaveChanges();
                    }
                    Refresh();
                }
            }
            else
            {
                MethodsClass.ShowNotification("Empty");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgItemView.SelectedItem != null)
            {
                var selected = dgItemView.SelectedItem as ItemListView;
                var window = new Windows.ItemWindow(selected.ItemId);
                MethodsClass.ShowPropertyWindow(window);
                Refresh();
            }
            else
            {
                MessageBox.Show("Select a row");
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
            using (var context = new DatabaseContext())
            {
                var ItemList = new List<ItemListView>();
                var items = context.Items.ToList();
                foreach (var list in items)
                {
                    var category = context.ItemGroups.FirstOrDefault(c => c.ItemGroupId == list.ItemGroupId);

                    ItemList.Add(new ItemListView()
                    {
                        ItemId = list.ItemId,
                        ItemName = list.ItemName,
                        ItemGroupId = category.ItemCategory,
                        Amount = list.ItemAmount
                    });
                }
            
            var dataList = new List<ItemReportData>();
            dataList.Add(new ItemReportData()
            {
                Title = "Items",
                ReportHeader = "Header",
                details = ItemList
            });

            var report = new ItemReportDesign() { DataSource = dataList, Name = "Report" };
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

        private void dgItemView_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            if (dgItemView.SelectedItem != null)
            {
                using (var context = new DatabaseContext())
                {
                    var item = dgItemView.SelectedItem as ItemListView;
                    txtItemName.Text = item.ItemName;
                    txtAmount.Text = item.Amount.ToString();
                    cmbCategory.Text = item.ItemGroupId;
                }
            }
            else
            {
                txtItemName.Text = "";
                txtAmount.Text = "";
                cmbCategory.Text = "";
            }
        }
    }
}