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
    /// Interaction logic for ItemGroupView.xaml
    /// </summary>
    public partial class ItemGroupPage : UserControl
    {
        List<ItemGroup> ItemGroups = new List<ItemGroup>();
        public ItemGroupPage()
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
                ItemGroups = context.ItemGroups.ToList();
                if (txtSearch.Text != "")
                {
                    ItemGroups = ItemGroups.Where(c => c.ItemCategory.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
                }
                dgItemGroup.ItemsSource = null;
                dgItemGroup.ItemsSource = ItemGroups;
                viewItemGroup.BestFitColumns();
                var total = context.ItemGroups.Select(c => c.ItemGroupId);
                blkTotal.Text = total.Count().ToString();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.ItemGroupListWindow();
            MethodsClass.ShowPropertyWindow(window);
            Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgItemGroup.SelectedItem != null)
            {
                using (var context = new DatabaseContext())
                {
                    var selected = dgItemGroup.SelectedItem as ItemGroup;
                    var category = context.ItemGroups.FirstOrDefault(c => c.ItemGroupId == selected.ItemGroupId);
                    var items = context.Items.Where(c => c.ItemGroupId == category.ItemGroupId);
                    if (items.Count() > 0)
                    {
                        MethodsClass.ShowNotification("Related records will be affected.\nDeletion cancelled.");
                    }
                    else if (MethodsClass.ShowConfirmation("Do you really want to delete this record?") == true)
                    {
                        context.ItemGroups.Remove(category);
                        context.SaveChanges();
                        Refresh();
                    }
                }
            }
            else
            {
                MethodsClass.ShowNotification("Empty");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgItemGroup.SelectedItem != null)
            {
                var selected = dgItemGroup.SelectedItem as ItemGroup;
                var window = new Windows.ItemGroupListWindow(selected.ItemGroupId);
                MethodsClass.ShowPropertyWindow(window);
                Refresh();
            }
            else {
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
            List<ItemGroupListView> ViewList = new List<ItemGroupListView>();
            using (var context = new DatabaseContext())
            {
                foreach (var itemgroup in ItemGroups)
                {
                    ViewList.Add(new ItemGroupListView()
                    {
                        ItemGroupId = itemgroup.ItemGroupId,
                        ItemName = itemgroup.ItemCategory
                       
                    });
                }
            }
            var dataList = new List<ItemGroupReportData>();
            dataList.Add(new ItemGroupReportData()
            {
                Title = "Products",
                ReportHeader = "Header",
                details = ViewList
            });

            var report = new ItemGroupReportDesign() { DataSource = dataList, Name = "Report" };
            using (var printTool = new ReportPrintTool(report))
            {
                printTool.ShowRibbonPreviewDialog();
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

        private void dgItemGroup_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            if (dgItemGroup.SelectedItem != null)
            {
                var item = dgItemGroup.SelectedItem as ItemGroup;
                txtCategoryName.Text = item.ItemCategory;
            }
            else
            {
                txtCategoryName.Text = "";
            }
        }
    }
}
