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
    /// Interaction logic for EditReservationWindow.xaml
    /// </summary>
    public partial class EditReservationWindow : Window
    {
        int selectedId = 0;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenWidth = Application.Current.MainWindow.Width;
        public EditReservationWindow()
        {
            InitializeComponent();
        }

        public EditReservationWindow(int id)
        {
            InitializeComponent();
            this.selectedId = id;
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
            using (var context = new DatabaseContext())
            {

                if (selectedId > 0)
                {
                    var reservation = context.Reservations.FirstOrDefault(c => c.ReservationId == selectedId);
                    var room = context.Rooms.FirstOrDefault(c => c.RoomId == reservation.RoomId);
                    var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == room.RoomTypeId);


                    List<String> roomtypelist = context.RoomTypes.Select(c => c.RoomTypeName).ToList();
                    cmbRoomType.ItemsSource = roomtypelist;
                    cmbRoomType.SelectedIndex = 0;
                    cmbRoomType.Text = roomtype.RoomTypeName;
                    var reservations = context.Reservations.FirstOrDefault(c => c.ReservationId == selectedId);
                    txtReservationNumber.Text = reservations.ReservationId.ToString();
                    txtCustomerName.Text = reservations.CustomerName;
                    txtReservationFee.Text = reservations.ReservationFee.ToString();
                    dtDateReserved.Text = reservations.DateReserved.ToString();
                    dtArrivalDate.Text = reservations.ArrivalDate.ToString();
                    dtRequestDate.Text = reservations.RequestDate.ToString();
                    txtArrivalTime.Text = reservations.ArrivalTime.ToString();
                    txtRequestTime.Text = reservations.RequestTime.ToString();
                    txtCardNumber.Text = reservations.CardNumber.ToString();



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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (selectedId > 0)
                {
                    var reservation = context.Reservations.FirstOrDefault(c => c.ReservationId == selectedId);

                    reservation.RequestTime = txtRequestTime.Text;
                    reservation.CardNumber = txtCardNumber.Text;
                    reservation.ArrivalDate = DateTime.Parse(dtArrivalDate.Text);
                    reservation.ArrivalTime = txtArrivalTime.Text;
                    reservation.ReservationFee = Int32.Parse(txtReservationFee.Text);
                    reservation.CustomerName = txtCustomerName.Text;

                    //transaction.DiscountCard = Int32.Parse(txtDiscountCard.Text);



                    context.SaveChanges();
                    this.Close();
                }
            }
        }
    }
}
