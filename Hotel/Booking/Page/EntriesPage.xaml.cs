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

namespace Hotel.Booking.Page
{
    /// <summary>
    /// Interaction logic for Entries.xaml
    /// </summary>
    public partial class EntriesPage : UserControl
    {
        public EntriesPage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();

        }

        private void Refresh()
        {
            using (var context = new DatabaseContext())
            {
                var Transaction = context.Transactions.ToList();
                var Room = context.Rooms.ToList();

                foreach (var forroom in Room)
                {
                    forroom.Status = "Available";
                    context.SaveChanges();
                }

                foreach (var fortransaction in Transaction)
                {
                    var room = context.Rooms.FirstOrDefault(c => c.RoomId == fortransaction.RoomId);

                    if (fortransaction.Status == "CheckedIn")
                    {
                        room.Status = "Occupied";
                        context.SaveChanges();
                    }
                }
                var Rooms = context.Rooms.ToList();
                var viewList = new List<BookingView>();
                foreach (var booking in Rooms)
                {
                    var roomtypes = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == booking.RoomTypeId);
                    var transactions = context.Transactions.FirstOrDefault(c => c.RoomId == booking.RoomId);
                    if (booking.Status == "Available")
                    {
                        var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == booking.RoomTypeId);
                        viewList.Add(new BookingView()
                        {
                            RoomId = booking.RoomId,
                            RoomNumber = booking.RoomNumber,
                            RoomType = roomtype.RoomTypeName,
                            Status = booking.Status,
                            RoomEquipmentId = booking.RoomEquipmentId.ToString(),
                            RoomDescription = booking.RoomDescription,
                            GuestNumber = "",
                            RoomSlipNumber = "",
                            TimeType = "",
                            DiscountId = "",
                            StaffId = "",
                            RoomCharge = "",
                            Username = "",
                            DiscountAmount = "",
                        });
                    }
                    if (booking.Status == "Occupied")
                    {
                        var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == booking.RoomTypeId);
                        //var staff = context.Staffs.FirstOrDefault(c => c.StaffId == transactions.StaffId);
                        viewList.Add(new BookingView()
                        {
                            RoomId = booking.RoomId,
                            RoomNumber = booking.RoomNumber,
                            RoomType = roomtype.RoomTypeName,
                            Status = booking.Status,
                            RoomEquipmentId = booking.RoomEquipmentId.ToString(),
                            RoomDescription = booking.RoomDescription,
                            GuestNumber = transactions.GuestNumber.ToString(),
                            RoomSlipNumber = transactions.RoomSlipNumber.ToString(),
                            CheckInDate = transactions.CheckInDate.ToString("MMM dd, yyyy"),
                            CheckInTime = transactions.CheckInTime,
                            //NumberTime = transactions.NumberTime.ToString(),
                            //TimeType = transactions.TimeType,
                            //DiscountId = transactions.DiscountId.ToString(),
                            //StaffId = staff.StaffName.ToString(),
                            //CheckOutDate = transactions.CheckOutDate.ToString("MMM dd, yyyy"),
                            //CheckOutTime = transactions.CheckOutTime,
                            //RoomCharge = transactions.RoomCharge.ToString(),
                            //Username = transactions.Username,
                            //DiscountCard = transactions.DiscountCard.ToString(),
                            //DiscountAmount = transactions.DiscountAmount.ToString(),
                            //Floor = booking.BuildingFloor,
                        });
                    }
                }
                dgTransactions.ItemsSource = viewList;
                viewTransactions.BestFitColumns();

                var total = context.Rooms.Select(c => c.RoomId);
                blkTotal.Text = total.Count().ToString();

                var avail = context.Rooms.Where(c => c.Status == "Available").Select(c => c.RoomId);
                blkAvailable.Text = avail.Count().ToString();

                var occu = context.Rooms.Where(c => c.Status == "Occupied").Select(c => c.RoomId);
                blkOccupied.Text = occu.Count().ToString();

                int occupancy = Int32.Parse(blkTotal.Text) - Int32.Parse(blkOccupied.Text);
                if (blkTotal.Text == "0")
                {
                    blkOccupancyRate.Text = "0%";
                }
                else
                {
                    decimal rate = occupancy / Decimal.Parse(blkTotal.Text) * 100;
                    blkOccupancyRate.Text = rate.ToString("n0") + "%";
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);

            frame.BackNavigationMode = BackNavigationMode.Root;
            frame.GoBack();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSchools_Click(object sender, RoutedEventArgs e)
        {

        }     

        private void btnCheckIn_Click(object sender, RoutedEventArgs e)
        {
            using(var context = new DatabaseContext())
	        {
             var check = context.Parameters.FirstOrDefault();

             if (check.TimePolicyEnabled == true)
             {
                 DateTime nowDate = DateTime.Now;
                 DateTime startDate = new DateTime(nowDate.Hour);
                 DateTime time = DateTime.Parse(DateTime.Now.ToString("hh:mm tt"));
                 DateTime start = DateTime.Parse(check.CheckInTimeStart);
                 DateTime end = DateTime.Parse(check.CheckInTimeEnd);
          //       if (start >= time)
                 if (start <= time && end <= time || start >= time && end >= time)    
                 {
                    MethodsClass.ShowNotification("You can't check in at this time");
                 }
                 else
                 {
                     var selectedId = dgTransactions.SelectedItem as BookingView;
                     var room = context.Rooms.FirstOrDefault(c => c.RoomId == selectedId.RoomId);
                     if (dgTransactions.SelectedItem == null)
                     {
                         MethodsClass.ShowNotification("No Rooms");
                     }
                     else
                     {
                         if (selectedId.Status == "Occupied")
                         {
                             MethodsClass.ShowNotification("This room is already occupied!");
                         }
                         else
                         {
                             //var window = new Booking.Windows.CheckInWindow(selectedId.RoomId);
                             //MethodsClass.ShowPropertyWindow(window);
                             //Refresh();

                             var window = new Booking.Windows.CheckInWindow(room.RoomId);
                             MethodsClass.ShowPropertyWindow(window);
                             Refresh();
                         }
                     }
                 }
             }
                
             else if (check.TimePolicyEnabled == false) 
                { 
            var selectedId = dgTransactions.SelectedItem as BookingView;
            if (dgTransactions.SelectedItem == null)
            {
                MethodsClass.ShowNotification("No Rooms");
            }
            else { 
            if (selectedId.Status == "Occupied")
            {
                MethodsClass.ShowNotification("This room is already occupied!");
            }
            else
            {
                var window = new Booking.Windows.CheckInWindow(selectedId.RoomId);
                MethodsClass.ShowPropertyWindow(window);
                Refresh();
            }
            }
          }
        }
        }

        private void btnReserve_Click(object sender, RoutedEventArgs e)
        {
            var selectedId = dgTransactions.SelectedItem as BookingView;
            var window = new Booking.Windows.ReservationWindow(selectedId.RoomNumber);
            MethodsClass.ShowPropertyWindow(window);
            Refresh();
        }

        private void btnFolio_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (dgTransactions.SelectedItem == null)
                {
                    MethodsClass.ShowNotification("Please Select a Room!");
                }
                else { 
                var selectedId = dgTransactions.SelectedItem as BookingView;
                var room = context.Rooms.FirstOrDefault(c => c.RoomNumber == selectedId.RoomNumber);
                if (room.Status == "Available")
                {
                    MethodsClass.ShowNotification("Warning: Room is Available!");
                }
                else if (room.Status == "Occupied")
                {
                    var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
                    var page = new Booking.SubPage.FolioPage(selectedId.RoomId);
                    page.txtOccupancyPercent.Text = blkOccupancyRate.Text;
                    frame.Navigate(page);
                    Refresh();
                }
            }
            }
        }

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            var selectedId = dgTransactions.SelectedItem as BookingView;
            if (dgTransactions.SelectedItem == null)
            {
                MethodsClass.ShowNotification("No Rooms");
            }
            else
            {
                if (selectedId.Status == "Available")
                {
                    MethodsClass.ShowNotification("This room is empty!");
                }
                else
                {
                    var window = new Windows.AdminPermissionWindow(selectedId.RoomId);
                    MethodsClass.ShowPropertyWindow(window);
                    Refresh();
                }
            }
        }

        private void btnInHouse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new Booking.Page.CheckOut();
            frame.Navigate(page);
        }

        private void btnReservation_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new Booking.Page.ReservationPage();
            frame.Navigate(page);
        }

        private void dgTransactions_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            //var room = dgTransactions.SelectedItem as BookingView;
        
            //using (var context = new DatabaseContext())
            //{
            //    if (dgTransactions.SelectedItem != null)
            //    {
            //        if (room.Status == "Occupied")
            //        {
            //            txtRoomNumber.Text = room.RoomNumber.ToString();
            //            txtStatus.Text = room.Status;
            //            txtDescription.Text = room.RoomDescription;
            //            txtEquipment.Text = room.RoomEquipmentId.ToString();
            //            txtRoomType.Text = room.RoomType;
            //            txtGuestNumber.Text = room.GuestNumber;
            //            txtCheckInDate.Text = room.CheckInDate;
            //            txtCheckInTime.Text = room.CheckInTime;
            //            txtDiscountAmount.Text = room.DiscountAmount;
            //            txtDiscountType.Text = room.DiscountId;
            //            txtRoomBoy.Text = room.StaffId.ToString();
            //            txtUsername.Text = room.Username;
            //            txtTimeType.Text = room.TimeType;
            //            txtTime.Text = room.NumberTime.ToString();
            //            txtRoomSlipNumber.Text = room.RoomSlipNumber;
            //            txtFloor.Text = room.Floor;
            //            txtCheckOutDate.Text = room.CheckOutDate;
            //            txtCheckOutTime.Text = room.CheckOutTime;
            //        }

            //        else if (room.Status == "Available")
            //        {
            //            var check = context.Parameters.FirstOrDefault();
            //            txtRoomType.Text = room.RoomType;
            //            txtRoomNumber.Text = room.RoomNumber.ToString();
            //            txtStatus.Text = room.Status;
            //            txtFloor.Text = room.Floor;
            //            txtEquipment.Text = room.RoomEquipmentId.ToString();
            //            txtDescription.Text = room.RoomDescription;
            //            txtTime.Text = "";
            //            txtCheckInTime.Text = "";
            //            txtDiscountAmount.Text = "";
            //            txtDiscountType.Text = "";
            //            txtRoomBoy.Text = "";
            //            txtGuestNumber.Text = "";
            //            txtCheckInTime.Text = "";
            //            txtCheckOutTime.Text = "";
            //            txtCheckOutDate.Text = "";
            //            txtCheckInDate.Text = "";
            //            txtUsername.Text = "";
            //            txtRoomSlipNumber.Text = "";
            //        }
            //    }
            //}
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {

                var selectedId = dgTransactions.SelectedItem as BookingView;
                var room = context.Rooms.FirstOrDefault(c => c.RoomNumber == selectedId.RoomNumber);

                    var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
                    var page = new Booking.SubPage.RoomDescriptionPage(selectedId.RoomId);
                    frame.Navigate(page);
                    Refresh();
               
            }
        }
      
        private void btnViewReserve_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var selectedId = dgTransactions.SelectedItem as BookingView;
                var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);

                var page = new Booking.SubPage.ViewReservationPage(selectedId.RoomId);
                var room = context.Rooms.FirstOrDefault(c => c.RoomId == selectedId.RoomId);

          
                frame.Navigate(page);
                Refresh();

            }
        }



    }
}
