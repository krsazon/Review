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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hotel.MasterData.Windows
{
    /// <summary>
    /// Interaction logic for RoomWindow.xaml
    /// </summary>
    public partial class RoomWindow : Window
    {
        int selectedid = 0;
    
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
       
        public RoomWindow()
        {
            InitializeComponent();
        }

        public RoomWindow(int id)
        {
            InitializeComponent();
            this.selectedid = id;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region animation onLoading
            if (screenLeftEdge > 0 || screenLeftEdge < -8)
            {
                screenWidth += screenLeftEdge;
            }
            DoubleAnimation animation = new DoubleAnimation(0, this.Width, (Duration)TimeSpan.FromSeconds(0.3));
            DoubleAnimation animation2 = new DoubleAnimation(screenWidth, screenWidth - this.Width, (Duration)TimeSpan.FromSeconds(0.3));
            this.BeginAnimation(Window.WidthProperty, animation);
            this.BeginAnimation(Window.LeftProperty, animation2);
            #endregion

            txtStatus.Items.Add("Available");
            txtStatus.Items.Add("Occupied");
            txtStatus.Items.Add("Maintenance");

            txtSmoking.Items.Add("Smoking");
            txtSmoking.Items.Add("Non-Smoking");

            if (selectedid > 0)
            {
                using (var context = new DatabaseContext())
                {
                    var room = context.Rooms.FirstOrDefault(c => c.RoomId == selectedid);
                    txtRoomNumber.Text = room.RoomNumber.ToString();
                    txtEquipments.Text = room.RoomEquipmentId.ToString();
                    txtDescription.Text = room.RoomDescription;
                    txtStatus.Text = room.Status;
                    txtFloor.Text = room.BuildingFloor;
                    txtRoomSize.Text = room.RoomSize;
                    txtSmoking.Text = room.Smoking;
                    txtCapacity.Text = room.Capacity.ToString();
                    List<String> list = context.RoomTypes.Select(c => c.RoomTypeName).ToList();
                    txtRoomType.ItemsSource = list;
                    var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == room.RoomTypeId);
                    txtRoomType.Text = roomtype.RoomTypeName;
                }
            }
            else
            {
                using (var context = new DatabaseContext())
                {
                    txtStatus.Text = "Available";
                    txtRoomNumber.Text = "1";
                    List<String> list = context.RoomTypes.Select(c => c.RoomTypeName).ToList();
                    txtRoomType.ItemsSource = list;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            #region animation onClosing
            if (screenLeftEdge > 0 || screenLeftEdge < -8)
            {
                screenWidth += screenLeftEdge;
            }
            Closing -= Window_Closing;
            e.Cancel = true;
            var anim = new DoubleAnimation(screenWidth, (Duration)TimeSpan.FromSeconds(0.3));
            var anim2 = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.3));
            anim.Completed += (s, _) => this.Close();
            this.BeginAnimation(Window.LeftProperty, anim);
            this.BeginAnimation(Window.WidthProperty, anim2);
            #endregion
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var room = context.Rooms.FirstOrDefault(c => c.RoomId == selectedid);
                var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeName == txtRoomType.Text);
                var Room = new Room();
                var Transaction = new Transaction();
                var RoomCount = context.Rooms.ToList();

                if (selectedid > 0)
                {
                        Room.RoomTypeId = roomtype.RoomTypeId;
                        Room.RoomDescription = txtDescription.Text;
                        Room.Status = room.Status;
                        Room.BuildingFloor = txtFloor.Text;
                        Room.RoomEquipmentId = txtEquipments.Text;
                        Room.Capacity = Int32.Parse(txtRoomNumber.Text);
                        Room.RoomSize = txtRoomSize.Text;
                        Room.Smoking = txtSmoking.Text;
                        Room.RoomNumber = txtRoomNumber.Text;

                        context.SaveChanges();
                        MethodsClass.ShowNotification("Successfully Updated!");
                        this.Close();
                    }
                    else if (selectedid <= 0)
                    {

                        if (RoomCount.Count() > 0) {
                        var RoomIdMax = context.Rooms.Max(c => c.RoomId);
                        var max = RoomIdMax + 1;

                        Room.RoomTypeId = roomtype.RoomTypeId;
                        Room.RoomDescription = txtDescription.Text;
                        Room.Status = "Available";
                        Room.BuildingFloor = txtFloor.Text;
                        Room.RoomEquipmentId = txtEquipments.Text;
                        Room.Capacity = Int32.Parse(txtCapacity.Text);
                        Room.RoomSize = txtRoomSize.Text;
                        Room.Smoking = txtSmoking.Text;
                        Room.RoomNumber = txtRoomNumber.Text;

                        Transaction.RoomId = max;
                        Transaction.GuestNumber = 0;
                        Transaction.ReservationId = 0;
                        Transaction.RoomSlipNumber = 0;
                        Transaction.CheckInDate = DateTime.Parse("1/1/2001");
                        Transaction.CheckInTime = "12:00 AM";
                        Transaction.DiscountCard = 0;
                        Transaction.DiscountId = 0;
                        Transaction.StaffId = 0;
                        Transaction.CheckOutDate = DateTime.Parse("1/1/2001");
                        Transaction.CheckOutTime = "12:00 AM";
                        Transaction.RoomCharge = 0;
                        Transaction.Username = "";
                        Transaction.DiscountAmount = 0;
                        Transaction.NetAmount = 0;
                        Transaction.AmountPaid = 0;
                        Transaction.Change = 0;
                        Transaction.Status = "Root";
                        Transaction.RoomTypeId = 0;

                        context.Transactions.Add(Transaction);
                        context.Rooms.Add(Room);
                        context.SaveChanges();
                        MethodsClass.ShowNotification("Successfully Added!");
                        this.Close();
                    } else if (RoomCount.Count <= 0){

                        Room.RoomTypeId = roomtype.RoomTypeId;
                        Room.RoomDescription = txtDescription.Text;
                        Room.Status = "Available";
                        Room.BuildingFloor = txtFloor.Text;
                        Room.RoomEquipmentId = txtEquipments.Text;
                        Room.Capacity = Int32.Parse(txtCapacity.Text);
                        Room.RoomSize = txtRoomSize.Text;
                        Room.Smoking = txtSmoking.Text;
                        Room.RoomNumber = txtRoomNumber.Text;

                        Transaction.RoomId = 1;
                        Transaction.GuestNumber = 0;
                        Transaction.ReservationId = 0;
                        Transaction.RoomSlipNumber = 0;
                        Transaction.CheckInDate = DateTime.Parse("1/1/2001");
                        Transaction.CheckInTime = "12:00 AM";
                        Transaction.DiscountCard = 0;
                        Transaction.DiscountId = 0;
                        Transaction.StaffId = 0;
                        Transaction.CheckOutDate = DateTime.Parse("1/1/2001");
                        Transaction.CheckOutTime = "12:00 AM";
                        Transaction.RoomCharge = 0;
                        Transaction.Username = "";
                        Transaction.DiscountAmount = 0;
                        Transaction.NetAmount = 0;
                        Transaction.AmountPaid = 0;
                        Transaction.Change = 0;
                        Transaction.Status = "Root";
                        Transaction.RoomTypeId = 0;

                        context.Transactions.Add(Transaction);
                        context.Rooms.Add(Room);
                        context.SaveChanges();
                        MethodsClass.ShowNotification("Successfully Added!");
                        this.Close();

                    }
                }
            }

            //    if (selectedid > 0)
            //    {
            //        if (txtRoomType.Text != "" && txtRoomNumber.Text != "" && txtRoomType.Text != "" && txtStatus.Text != "" && txtFloor.Text != "" && txtEquipments.Text != "" && txtDescription.Text != "")
            //        {
            //                int cap = Int32.Parse(txtRoomNumber.Text);
            //                var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeName == txtRoomType.Text);
            //                List<RoomTypeListView> viewList = new List<RoomTypeListView>();
            //                var room = context.Rooms.FirstOrDefault(c => c.RoomId == selectedid);
            //                room.Capacity = cap;
            //                room.RoomSize = txtRoomSize.Text;
            //                room.RoomNumber = txtRoomNumber.Text;
            //                room.RoomTypeId = roomtype.RoomTypeId;
            //                room.RoomEquipmentId = txtEquipments.Text;
            //                room.RoomDescription = txtDescription.Text;
            //                room.Smoking = txtSmoking.Text;
            //                room.Status = txtStatus.Text;
            //                room.BuildingFloor = txtFloor.Text;
            //                MethodsClass.ShowNotification("Record successfully updated!");
            //                context.SaveChanges();
            //                this.Close();
            //            }
            //        else
            //        {
            //            MethodsClass.ShowNotification("Please fill up the fields correctly.");
            //        }
            //    }
            //    else
            //    {
            //            var room = new Room();
            //            var transaction = new Transaction();
            //            if (txtRoomNumber.Text == "" || txtRoomType.Text == "" || txtStatus.Text == "" || txtFloor.Text == "" || txtEquipments.Text == "" || txtDescription.Text == "")
            //            {
            //                MethodsClass.ShowNotification("Please fill up the fields correctly.");
            //            }
            //            else
            //            {
            //                var duplicates = context.Rooms.Where(c => c.RoomNumber == txtRoomNumber.Text).ToList();
            //                if (duplicates.Count() > 0)
            //                {
            //                    MethodsClass.ShowNotification("Cannot add duplicate records.");
            //                }
            //                else
            //                {
            //                //int roomno = Int32.Parse(txtRoomNumber.Text);
            //                var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeName == txtRoomType.Text);
            //                room.Capacity = Int32.Parse(txtCapacity.Text);
            //                room.RoomSize = txtRoomSize.Text;
            //                //transaction.RoomId = roomno;
            //                room.RoomNumber = txtRoomNumber.Text;
            //                room.RoomTypeId = roomtype.RoomTypeId;
            //                room.Smoking = txtSmoking.Text;
            //                room.RoomEquipmentId = txtEquipments.Text;
            //                room.RoomDescription = txtDescription.Text;
            //                room.Status = txtStatus.Text;
            //                room.BuildingFloor = txtFloor.Text;                         
            //                context.Rooms.Add(room);
            //                context.SaveChanges();
            //                MethodsClass.ShowNotification("New record successfully saved!");
            //                this.Close();
            //            }
            //        }
            //    }
            //}        
        }

        public void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

      public void btnBrowse1_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var RoomCount = context.Rooms.ToList();

                if (RoomCount.Count() > 0)
                {

                    var roommax = context.Rooms.Max(c => c.RoomId);
                    int max = roommax + 1;

                    var window = new Windows.ImportEquipmentWindow(max);
                    window.roomequipmentid = txtRoomNumber.Text;
                    MethodsClass.ShowPropertyWindow(window);
                    txtEquipments.Text = Windows.ImportEquipmentWindow.txt;
                }
                else if (RoomCount.Count() <= 0) {
                    int max = 1;

                    var window = new Windows.ImportEquipmentWindow(max);
                    window.roomequipmentid = txtRoomNumber.Text;
                    MethodsClass.ShowPropertyWindow(window);
                    txtEquipments.Text = Windows.ImportEquipmentWindow.txt;
                }
        }
        }

      private void txtRoomNumber_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
      {
          if (selectedid > 0)
          {

          }
          else { 
          using (var context = new DatabaseContext())
          {
              int roomnos = Int32.Parse(txtRoomNumber.Text);
              var duplicates = context.Rooms.Where(c => c.RoomId == roomnos).ToList();
              if (duplicates.Count() > 0)
              {
                  imgCheck.Visibility = Visibility.Hidden;
                  imgClose.Visibility = Visibility.Visible;
                  txtDescription.IsEnabled = false;
                  txtRoomType.IsEnabled = false;
                  txtStatus.IsEnabled = false;
                  txtFloor.IsEnabled = false;
                  txtEquipments.IsEnabled = false;
                  btnBrowse1.IsEnabled = false;
         
              }
              else
              {
                  imgClose.Visibility = Visibility.Hidden;
                  imgCheck.Visibility = Visibility.Visible;
                  txtDescription.IsEnabled = true;
                  txtRoomType.IsEnabled = true;
                  txtStatus.IsEnabled = true;
                  txtFloor.IsEnabled = true;
                  txtEquipments.IsEnabled = true;
                  btnBrowse1.IsEnabled = true;
                
              }
            }
          }
       }
    }
}
