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
    /// Interaction logic for RoomTypeView.xaml
    /// </summary>
    public partial class RoomTypePage : UserControl
    {
        public static int select = 0;
               public static int selecttype = 0;
        public static string name = "";
        List<RoomType> RoomTypes = new List<RoomType>();
        List<RoomTypeRate> RoomTypeRates = new List<RoomTypeRate>();
        public RoomTypePage()
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
                RoomTypes = context.RoomTypes.ToList();

                if (txtSearch.Text != "")
                {
                    RoomTypes = RoomTypes.Where(c => c.RoomTypeName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
                }
                dgRoomType.ItemsSource = null;
                dgRoomType.ItemsSource = RoomTypes;
                viewRoomType.BestFitColumns();

                var total = context.RoomTypes.Select(c => c.RoomTypeId);
                blkTotal.Text = total.Count().ToString();
            }
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.RoomTypeWindow();
            MethodsClass.ShowPropertyWindow(window);
            Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgRoomType.SelectedItem == null)
            {
                MethodsClass.ShowNotification("Empty");
            }
            else
            {
                if (MethodsClass.ShowConfirmation("Do you really want to remove this record?"))
                {
                    using (var context = new DatabaseContext())
                    {
                        var selected = dgRoomType.SelectedItem as RoomType;
                        var delete = context.Rooms.Where(c => c.RoomTypeId == selected.RoomTypeId).ToList();
                        if (delete.Count() > 0)
                        {
                            MethodsClass.ShowNotification("Cannot Delete.");
                        }
                        else
                        {
                            var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == selected.RoomTypeId);
                            context.RoomTypes.Remove(roomtype);
                            context.SaveChanges();
                        }
                    }
                    Refresh();
                }
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Variables.collapseUserContent = true;
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            frame.BackNavigationMode = BackNavigationMode.Root;
            frame.GoBack();
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgRoomType.SelectedItem != null)
            {
                var selectedRow = dgRoomType.SelectedItem as RoomType;
                var window = new Windows.RoomTypeWindow(selectedRow.RoomTypeId);
                MethodsClass.ShowPropertyWindow(window);
                Refresh();
            }
            else
            {
                MethodsClass.ShowNotification("Please select a row.");
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSchools_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgDistricts_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            if (dgRoomType.SelectedItem != null)
            {
                var roomtype = dgRoomType.SelectedItem as RoomType;
                txtRoomType.Text = roomtype.RoomTypeName;
                txtAddAmount.Text = roomtype.AdditionalChargesAmount.ToString();
                txtAddTime.Text = roomtype.AdditionalChargesTime;
                txtAddNumberTime.Text = roomtype.AdditionalChargesNumberTime.ToString();
                using (var context = new DatabaseContext())
                {
                    RoomTypeRates = context.RoomTypeRates.Where(c => c.RoomTypeId == roomtype.RoomTypeId).ToList();
                    dgRoomTypeRate.ItemsSource = null;
                    dgRoomTypeRate.ItemsSource = RoomTypeRates;

                    viewRoomTypeRate.BestFitColumns();
                }
            }
            else
            {
                txtRoomType.Text = "";
                txtAddAmount.Text = "";
                txtAddTime.Text = "";
                txtAddNumberTime.Text = "";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnAddRate_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dgRoomType.SelectedItem as RoomType;
            name = selectedRow.RoomTypeName;
            var window = new Windows.RoomTypeRateWindow(selectedRow.RoomTypeId);
            window.txtRoomType.Text = selectedRow.RoomTypeName;
            MethodsClass.ShowPropertyWindow(window);
            Refresh();
        }

        private void dgRoomTypeRate_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            if (dgRoomTypeRate.SelectedItem != null)
            {
                var roomtype = dgRoomTypeRate.SelectedItem as RoomTypeRate;

                txtAmount.Text = roomtype.Amount.ToString();
                txtRateTime.Text = roomtype.AmountTime;
                txtTime.Text = roomtype.AmountNumberTime.ToString();
            }
            else
            {
                txtAmount.Text = "";
                txtRateTime.Text = "";
                txtTime.Text = "";
            }
        }

        private void btnEditRate_Click(object sender, RoutedEventArgs e)
        {
            if (dgRoomTypeRate.SelectedItem != null)
            {
                var selectedRow = dgRoomTypeRate.SelectedItem as RoomTypeRate;
                var window = new Windows.RoomTypeRateWindow(selectedRow.RoomTypeRateId);
                select = selectedRow.RoomTypeId;
                MethodsClass.ShowPropertyWindow(window);
                Refresh();
            }
            else
            {
                MethodsClass.ShowNotification("Please select a row.");
            }
        }

        private void btnDeleteRate_Click(object sender, RoutedEventArgs e)
        {
            if (dgRoomTypeRate.SelectedItem != null)
            {
                if (MethodsClass.ShowConfirmation("Do you really want to delete this record?") == true)
                {
                    var selected = dgRoomTypeRate.SelectedItem as RoomTypeRate;
                    using (var context = new DatabaseContext())
                    {
                        var rate = context.RoomTypeRates.FirstOrDefault(c => c.RoomTypeRateId == selected.RoomTypeRateId);
                        context.RoomTypeRates.Remove(rate);
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

    }
}


