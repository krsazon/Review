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

namespace Hotel.Booking.Windows
{
    /// <summary>
    /// Interaction logic for ReservationWindow.xaml
    /// </summary>
    public partial class ReservationWindow : Window
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenWidth = Application.Current.MainWindow.Width;
        int selectedId = 0;
        String selectedRoomNumber = "";

        public ReservationWindow()
        {
            InitializeComponent();
        }

        public ReservationWindow(int id)
        {
            InitializeComponent();
            this.selectedId = id;
        }

        public ReservationWindow(String RoomNumber)
        {
            InitializeComponent();
            this.selectedRoomNumber = RoomNumber;
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

           
            Refresh();
        }

        public void Refresh()
        {
            //using (var context = new DatabaseContext())
            //{
            //    List<String> roomtype = context.RoomTypes.Select(c => c.RoomTypeName).ToList();
            //    cmbRoomType.ItemsSource = roomtype;
            //}


            using (var context = new DatabaseContext())
            {
                var reservationcount = context.Reservations.ToList();
                var reservationmax = context.Reservations.Max(c => c.ReservationNumber);
                int max = Int32.Parse(reservationmax) + 1;
                if (reservationcount.Count() == 0)
                {
                    txtReservationNumber.Text = "000001";
                    dtDateReserved.Text = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
                    if (selectedId > 0)
                    {
                        var room = context.Rooms.FirstOrDefault(c => c.RoomId == selectedId);
                        var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == room.RoomTypeId);
                        txtRoomNumber.Text = room.RoomNumber;
                        cmbRoomType.Text = roomtype.RoomTypeName;
                    }
                }
                else if (reservationcount.Count() > 0)
                {
                    //var room = context.Rooms.FirstOrDefault(c => c.RoomNumber == reservation.RoomNumber);
                    //var roomtypes = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == reservation.RoomTypeId);

                    ////List<String> roomtype = context.RoomTypes.Select(c => c.RoomTypeName).ToList();
                    //// cmbRoomType.ItemsSource = roomtype;

                    // cmbRoomType.Text = roomtypes.RoomTypeNyoyo.schedulerControl1.SelectedInterval.Start.Month.ToString() + "/" + yoyo.schedulerControl1.SelectedInterval.Start.Day.ToString() + "/" + yoyo.schedulerControl1.SelectedInterval.Start.Year.ToString();ame;
  
                    txtReservationNumber.Text = max.ToString();
                    dtDateReserved.Text = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
                    if (selectedId > 0)
                    {
                        var room = context.Rooms.FirstOrDefault(c => c.RoomId == selectedId);
                        var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == room.RoomTypeId);
                        txtRoomNumber.Text = room.RoomNumber;
                        cmbRoomType.Text = roomtype.RoomTypeName;
                    }
                    //cmbRoomType.Text = roomtypes.RoomTypeName;

                    //var yoyo = new Booking.SubViews.ViewReservation();
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

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (txtCustomerName.Text == "" || txtCardNumber.Text == "" || txtReservationFee.Text == "" || txtRoomNumber.Text == "" || cmbRoomType.Text == "" || dtRequestTime.Text == "")
                {
                    MethodsClass.ShowNotification("Please fill up the following fields.");
                }
                else if (txtCustomerName.Text != "" || txtCardNumber.Text != "" || txtReservationFee.Text != "" || txtRoomNumber.Text != "" || cmbRoomType.Text != "" || dtRequestTime.Text != "")
                {
                    var reservation = new Reservation();
                    var room = context.Rooms.FirstOrDefault(c => c.RoomNumber == txtRoomNumber.Text);

                    reservation.ReservationNumber = txtReservationNumber.Text;
                    reservation.CustomerName = txtCustomerName.Text;
                    reservation.RoomId = room.RoomId;
                    reservation.DateReserved = DateTime.Parse(dtDateReserved.Text);
                    reservation.CardNumber = txtCardNumber.Text;
                    reservation.RequestDate = DateTime.Parse(dtRequestDate.Text);
                    reservation.RequestTime = dtRequestTime.Text;
                    reservation.ReservationFee = Int32.Parse(txtReservationFee.Text);

                    MethodsClass.ShowNotification("Successfully Reserved!");
                    context.Reservations.Add(reservation);
                    context.SaveChanges();
                    this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {

            var window = new Windows.RoomListWindow(dtRequestDate.Text, txtCustomerName.Text, txtCardNumber.Text, txtReservationFee.Text, txtRoomNumber.Text, cmbRoomType.Text);
            MethodsClass.ShowPropertyWindow(window);
            this.Close();
            Refresh();

            //cmbReservation.Text = Windows.ReservationListWindow.txt;
        }

        private void DateEdit_CustomDisplayText(object sender, DevExpress.Xpf.Editors.CustomDisplayTextEventArgs e)
        {

        }
    }
}
