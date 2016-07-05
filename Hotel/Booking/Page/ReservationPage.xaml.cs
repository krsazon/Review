using DevExpress.Xpf.WindowsUI;
using DevExpress.XtraReports.UI;
using Hotel.Booking.Reports;
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
    /// Interaction logic for Reservation.xaml
    /// </summary>
    public partial class ReservationPage : UserControl
    {
        int selectedId = 0;
        public ReservationPage()
        {
            InitializeComponent();
        }

        public ReservationPage(int id)
        {
            InitializeComponent();
            this.selectedId = id;
        }

        public void Refresh()
        {
            dtReserved.Text = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
            dtArrival.Text = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
            using (var context = new DatabaseContext())
            {
                var Reservations = context.Reservations.ToList();
                var viewList = new List<ReservationView>();
                foreach (var reservation in Reservations)
                {
                    var room = context.Rooms.FirstOrDefault(c => c.RoomId == reservation.RoomId);
                    viewList.Add(new ReservationView()
                    {
                        ReservationId = reservation.ReservationId,
                        ReservationFee = reservation.ReservationFee.ToString(),
                        CustomerName = reservation.CustomerName,
                        RoomNumber = room.RoomNumber.ToString(),
                        DateReserved = reservation.DateReserved.ToString("MM/dd/yyyy"),
                        ArrivalDate = reservation.ArrivalDate.ToString("MM/dd/yyyy"),
                        ArrivalTime = reservation.ArrivalTime,
                        RequestDate = reservation.RequestDate.ToString("MM/dd/yyyy"),
                        RequestTime = reservation.RequestTime,
                        CardNumber = reservation.CardNumber,
                    });
                }
                dgReservations.ItemsSource = viewList;
                //dgReservations.BestFitColumns();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btnInHouse_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new Booking.Page.EntriesPage();
            frame.Navigate(page);
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new Booking.Page.CheckOut();
            frame.Navigate(page);
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            frame.BackNavigationMode = BackNavigationMode.Root;
            frame.GoBack();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // var selectedId = dgReservations.SelectedItem as ReservationView;
            // var window = new Booking.Windows.EditReservationWindow(Int32.Parse(selectedId.RoomId));
            // MethodsClass.ShowPropertyWindow(window);
            //Refresh();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (txtReservationId.Text != "")
                {
                    var selecteditem = dgReservations.SelectedItem as ReservationView;
                    var reservation = context.Reservations.FirstOrDefault(c => c.ReservationId == selecteditem.ReservationId);
                    reservation.Status = "Cancelled";
                    context.SaveChanges();
                    Refresh();
                }
            }
        }

        private void dgReservations_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            if (dgReservations.SelectedItem != null)
            {
                using (var context = new DatabaseContext())
                {
                    var reservation = dgReservations.SelectedItem as ReservationView;
                    txtRoomNumber.Text = reservation.RoomNumber.ToString();
                    txtReservationFee.Text = reservation.ReservationFee;
                    txtCustomerName.Text = reservation.CustomerName;
                    txtRoomNumber.Text = reservation.RoomNumber.ToString();
                    txtDateReserved.Text = reservation.DateReserved;
                    txtArrivalDate.Text = reservation.ArrivalDate;
                    txtArrivalTime.Text = reservation.ArrivalTime;
                    txtRequestDate.Text = reservation.RequestDate;
                    txtRequestTime.Text = reservation.RequestTime;
                    txtCardNumber.Text = reservation.CardNumber;
                    txtReservationId.Text = reservation.ReservationId.ToString();
                }
            }
            else
            {
                txtRoomNumber.Text = "";
                txtReservationFee.Text = "";
                txtCustomerName.Text = "";
                txtRoomNumber.Text = "";
                txtDateReserved.Text = "";
                txtArrivalDate.Text = "";
                txtArrivalTime.Text = "";
                txtRequestDate.Text = "";
                txtRequestTime.Text = "";
                txtCardNumber.Text = "";
                txtReservationId.Text = "";
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        { 
            List<ReservationView> details = new List<ReservationView>();
            using (var context = new DatabaseContext())
            {
                var Reservations = context.Reservations.ToList();
                foreach (var forreservation in Reservations)
                {
                    var room = context.Rooms.FirstOrDefault(c => c.RoomId == forreservation.RoomId);
                    var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == room.RoomTypeId);
                    if (forreservation.ArrivalDate.ToString("MM/dd/yyyy") == "01/01/0001"){
                    details.Add(new ReservationView() { 
                    ReservationFee = "₱ " + forreservation.ReservationFee.ToString(),
                    CustomerName = forreservation.CustomerName,
                    RoomNumber = room.RoomNumber, 
                    DateReserved = forreservation.DateReserved.ToString("MM/dd/yyyy"), 
                    ArrivalTime = "Ongoing", 
                    ArrivalDate = "Ongoing",
                    RequestDate = forreservation.RequestDate.ToString("MM/dd/yyyy"), 
                    CardNumber = forreservation.CardNumber, 
                    RoomType = "Rm. " + roomtype.RoomTypeName, 
                    Status = forreservation.Status,
                    RequestTime = forreservation.RequestTime
                    });
                    } else if (forreservation.ArrivalDate.ToString("MM/dd/yyyy") != "01/01/0001")
                    {
                    details.Add(new ReservationView() { 
                    ReservationFee = "₱ " + forreservation.ReservationFee.ToString(),
                    CustomerName = forreservation.CustomerName,
                    RoomNumber = room.RoomNumber, 
                    DateReserved = forreservation.DateReserved.ToString("MM/dd/yyyy"), 
                    ArrivalDate = forreservation.ArrivalDate.ToString("MM/dd/yyyy"),
                    ArrivalTime = forreservation.ArrivalTime, 
                    RequestDate = forreservation.RequestDate.ToString("MM/dd/yyyy"), 
                    CardNumber = forreservation.CardNumber, 
                    RoomType = "Rm. " + roomtype.RoomTypeName, 
                    Status = forreservation.Status,
                    RequestTime = forreservation.RequestTime
                    });
                    }
                }

                var dataList = new List<ReservationsReportData>();
                dataList.Add(new ReservationsReportData() { 
                    Title = "Title",
                    Header = "Report Header",
                    details = details
                });

                var report = new ReservationsReportDesign() { DataSource = dataList, Name = "Report Ko"};
                using (var printTool = new ReportPrintTool(report)) {
                    printTool.ShowRibbonPreviewDialog();
                }
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            var hello = DateTime.DaysInMonth(DateTime.Today.Year, 2);
            MessageBox.Show(hello.ToString());
        }
    }

}




