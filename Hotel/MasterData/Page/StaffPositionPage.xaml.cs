using DevExpress.Xpf.WindowsUI;
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
    /// Interaction logic for StaffPositionPage.xaml
    /// </summary>
    public partial class StaffPositionPage : UserControl
    {
        List<StaffPosition> StaffPositions = new List<StaffPosition>();
        public StaffPositionPage()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.StaffPositionWindow();
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
                StaffPositions = context.StaffPositions.ToList();
                if (txtSearch.Text != "")
                {
                    StaffPositions = StaffPositions.Where(c => c.StaffPositionName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
                }
                dgStaffPosition.ItemsSource = null;
                dgStaffPosition.ItemsSource = StaffPositions;
                viewStaffPosition.BestFitColumns();
                var total = context.StaffPositions.Select(c => c.StaffPositionId);
                blkTotal.Text = total.Count().ToString();
            }
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

            if (dgStaffPosition.SelectedItem == null)
            {
                MethodsClass.ShowNotification("Please select a row.");
            }
            else if (dgStaffPosition.SelectedItem != null)
            {
                var selected = dgStaffPosition.SelectedItem as StaffPosition;
                var staffpositionid = new Windows.StaffPositionWindow(selected.StaffPositionId);
                MethodsClass.ShowPropertyWindow(staffpositionid);
                Refresh();
            }
        }

        private void dgStaffPosition_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            if (dgStaffPosition.SelectedItem != null)
            {
                var staffposition = dgStaffPosition.SelectedItem as StaffPosition;
                txtStaffPosition.Text = staffposition.StaffPositionName;
                chkAssist.IsChecked = staffposition.Assist;
               
            }
            else
            {
                txtStaffPosition.Text = "";
                chkAssist.IsChecked = false;

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgStaffPosition.SelectedItem != null)
            {
                if (MethodsClass.ShowConfirmation("Do you really want to delete this record?") == true)
                {
                    var selected = dgStaffPosition.SelectedItem as StaffPosition;
                    using (var context = new DatabaseContext())
                    {
                        var staffposition = context.StaffPositions.FirstOrDefault(c => c.StaffPositionId == selected.StaffPositionId);
                        context.StaffPositions.Remove(staffposition);
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

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
