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
    /// Interaction logic for RoomListWindow.xaml
    /// </summary>
    public partial class RoomListWindow : Window
    {
        String selectedRequestDate = "";
        String selectedCustomerName = "";
        String selectedCardNumber = "";
        String selectedReservationFee = "";
        String selectedRoomNumber = "";
        String selectedRoomType = "";
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;

        public RoomListWindow()
        {
            InitializeComponent();
        }

        public RoomListWindow(String RequestDate, String CustomerName, String CardNumber, String ReservationFee, String RoomNumber, String RoomType)
        {
            InitializeComponent();
            this.selectedRequestDate = RequestDate;
            this.selectedCustomerName = CustomerName;
            this.selectedCardNumber = CardNumber;
            this.selectedReservationFee = ReservationFee;
            this.selectedRoomNumber = RoomNumber;
            this.selectedRoomType = RoomType;
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
            using(var context = new DatabaseContext()){
            var Rooms = context.Rooms.ToList();
            var viewList = new List<BookingView>();
            foreach (var booking in Rooms)
            {
                var roomtypes = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == booking.RoomTypeId);
                    var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == booking.RoomTypeId);
                    viewList.Add(new BookingView()
                    {
                        RoomId = booking.RoomId,
                        RoomNumber = booking.RoomNumber,
                        RoomType = roomtype.RoomTypeName,
                        RoomEquipmentId = booking.RoomEquipmentId.ToString(),
                    });
                }
            dgRoom.ItemsSource = viewList;
            viewRoom.BestFitColumns();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.ReservationWindow();
            window.dtRequestDate.Text = selectedRequestDate;
            window.txtCustomerName.Text = selectedCustomerName;
            window.txtCardNumber.Text = selectedCardNumber;
            window.txtReservationFee.Text = selectedReservationFee;
            window.txtRoomNumber.Text = selectedRoomNumber;
            window.cmbRoomType.Text = selectedRoomType;
            this.Close();
            MethodsClass.ShowPropertyWindow(window);   
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            var selectedRoomId = dgRoom.SelectedItem as BookingView;
            var window = new Windows.ReservationWindow(selectedRoomId.RoomId);
            window.dtRequestDate.Text = selectedRequestDate;
            window.txtCustomerName.Text = selectedCustomerName;
            window.txtCardNumber.Text = selectedCardNumber;
            window.txtReservationFee.Text = selectedReservationFee;
 
            this.Close();
            MethodsClass.ShowPropertyWindow(window);    
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgRoom_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
