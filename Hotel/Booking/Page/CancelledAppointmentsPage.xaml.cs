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
    /// Interaction logic for CancelledAppointments.xaml
    /// </summary>
    public partial class CancelledAppointments : UserControl
    {
        bool checkin = true;
        bool reservation = false;

        public CancelledAppointments()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            txtDateReserved.Text = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
            txtArrivalDate.Text = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
            using (var context = new DatabaseContext())
            {
                if (dgReservations.Visibility == System.Windows.Visibility.Visible && dgCheckOut.Visibility == System.Windows.Visibility.Hidden)
                {
                    var Reservations = context.Reservations.ToList();
                    var viewList = new List<ReservationView>();
                    foreach (var reservation in Reservations)
                    {
                        var room = context.Rooms.FirstOrDefault(c => c.RoomId == reservation.RoomId);

                        if (reservation.Status == "Cancelled")
                        {
                            viewList.Add(new ReservationView()
                            {
                                ReservationId = reservation.ReservationId,
                                ReservationFee = reservation.ReservationFee.ToString(),
                                CustomerName = reservation.CustomerName,
                                RoomNumber = room.RoomNumber.ToString(),
                                DateReserved = reservation.DateReserved.ToString(),
                                ArrivalDate = reservation.ArrivalDate.ToString(),
                                ArrivalTime = reservation.ArrivalTime,
                                RequestDate = reservation.RequestDate.ToString(),
                                RequestTime = reservation.RequestTime,
                                CardNumber = reservation.CardNumber,

                            });
                        }
                        dgReservations.ItemsSource = viewList;
                        //dgReservations.BestFitColumns();
                    }
                }
                else if (dgReservations.Visibility == System.Windows.Visibility.Hidden && dgCheckOut.Visibility == System.Windows.Visibility.Visible)
                {
                    var Transactions = context.Transactions.ToList();
                    var viewList = new List<CheckOutView>();
                    foreach (var transaction in Transactions)
                    {

                        var room = context.Rooms.FirstOrDefault(c => c.RoomId == transaction.RoomId);
                        var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == transaction.RoomTypeId);
                        if (transaction.Status == "Cancelled")
                        {
                            viewList.Add(new CheckOutView()
                            {
                                RoomNumber = room.RoomNumber,
                                GuestNumber = transaction.GuestNumber.ToString(),
                                ReservationId = transaction.ReservationId.ToString(),
                                RoomSlipNumber = transaction.RoomSlipNumber.ToString(),
                                CheckInDate = transaction.CheckInDate.ToString(),
                                CheckInTime = transaction.CheckInTime.ToString(),
                                // NumberOfHours = transaction.,
                                // NumberOfDays = transaction.GuestNumber,
                                DiscountCard = transaction.DiscountCard.ToString(),
                                DiscountId = transaction.DiscountId.ToString(),
                                CheckOutDate = transaction.CheckOutDate.ToString(),
                                RoomCharge = transaction.RoomCharge.ToString(),
                                Username = transaction.Username,
                                DiscountAmount = transaction.DiscountAmount.ToString(),
                                NetAmount = transaction.NetAmount.ToString(),
                                AmountPaid = transaction.AmountPaid.ToString(),
                                Change = transaction.GuestNumber.ToString(),
                                Status = transaction.Status,
                                RoomTypeId = roomtype.RoomTypeName,
                            });
                        }
                    }
                    dgCheckOut.ItemsSource = viewList;
                    //dgReservations.BestFitColumns();
                }
            }
        }

        private void btnCheckIn_Click(object sender, RoutedEventArgs e)
        {
            reservation = false;
            checkin = true;
            if (reservation == false && checkin == true)
            {
                CancelledCheckOut.Visibility = System.Windows.Visibility.Visible;
                lblRoomNumber.Visibility = System.Windows.Visibility.Visible;
                lblGuestNumber.Visibility = System.Windows.Visibility.Visible;
                lblRoomSlipNumber.Visibility = System.Windows.Visibility.Visible;
                lblCheckInDate.Visibility = System.Windows.Visibility.Visible;
                lblCheckInTime.Visibility = System.Windows.Visibility.Visible;
                lblNumberOfDays.Visibility = System.Windows.Visibility.Visible;
                lblNumberOfHours.Visibility = System.Windows.Visibility.Visible;
                lblDiscountCard.Visibility = System.Windows.Visibility.Visible;
                lblDiscountId.Visibility = System.Windows.Visibility.Visible;
                lblCheckOutDate.Visibility = System.Windows.Visibility.Visible;
                lblRoomCharge.Visibility = System.Windows.Visibility.Visible;
                lblUsername.Visibility = System.Windows.Visibility.Visible;
                lblNetAmount.Visibility = System.Windows.Visibility.Visible;
                lblAmountPaid.Visibility = System.Windows.Visibility.Visible;
                lblStatus.Visibility = System.Windows.Visibility.Visible;
                lblChange.Visibility = System.Windows.Visibility.Visible;
                lblRoomTypeName.Visibility = System.Windows.Visibility.Visible;
                txtRoomNumber.Visibility = System.Windows.Visibility.Visible;
                txtGuestNumber.Visibility = System.Windows.Visibility.Visible;
                txtRoomSlipNumber.Visibility = System.Windows.Visibility.Visible;
                txtCheckInDate.Visibility = System.Windows.Visibility.Visible;
                txtCheckInTime.Visibility = System.Windows.Visibility.Visible;
                txtNumberOfDays.Visibility = System.Windows.Visibility.Visible;
                txtNumberOfHours.Visibility = System.Windows.Visibility.Visible;
                txtDiscountCard.Visibility = System.Windows.Visibility.Visible;
                txtDiscountId.Visibility = System.Windows.Visibility.Visible;
                txtCheckOutDate.Visibility = System.Windows.Visibility.Visible;
                txtRoomCharge.Visibility = System.Windows.Visibility.Visible;
                txtUsername.Visibility = System.Windows.Visibility.Visible;
                txtNetAmount.Visibility = System.Windows.Visibility.Visible;
                txtAmountPaid.Visibility = System.Windows.Visibility.Visible;
                txtStatus.Visibility = System.Windows.Visibility.Visible;
                txtChange.Visibility = System.Windows.Visibility.Visible;
                txtRoomTypeName.Visibility = System.Windows.Visibility.Visible;

                CancelledReservations.Visibility = System.Windows.Visibility.Hidden;
                lblReservationId1.Visibility = System.Windows.Visibility.Hidden;
                lblReservationFee.Visibility = System.Windows.Visibility.Hidden;
                lblCustomerName.Visibility = System.Windows.Visibility.Hidden;
                lblRoomNumber1.Visibility = System.Windows.Visibility.Hidden;
                lblDateReserved.Visibility = System.Windows.Visibility.Hidden;
                lblArrivalDate.Visibility = System.Windows.Visibility.Hidden;
                lblArrivalTime.Visibility = System.Windows.Visibility.Hidden;
                lblRequestDate.Visibility = System.Windows.Visibility.Hidden;
                lblRequestTime.Visibility = System.Windows.Visibility.Hidden;
                lblCardNumber.Visibility = System.Windows.Visibility.Hidden;
                lblReservationNumber.Visibility = System.Windows.Visibility.Hidden;
                txtReservationId1.Visibility = System.Windows.Visibility.Hidden;
                txtReservationFee.Visibility = System.Windows.Visibility.Hidden;
                txtCustomerName.Visibility = System.Windows.Visibility.Hidden;
                txtRoomNumber1.Visibility = System.Windows.Visibility.Hidden;
                txtDateReserved.Visibility = System.Windows.Visibility.Hidden;
                txtArrivalDate.Visibility = System.Windows.Visibility.Hidden;
                txtArrivalTime.Visibility = System.Windows.Visibility.Hidden;
                txtRequestDate.Visibility = System.Windows.Visibility.Hidden;
                txtRequestTime.Visibility = System.Windows.Visibility.Hidden;
                txtCardNumber.Visibility = System.Windows.Visibility.Hidden;
                txtReservationNumber.Visibility = System.Windows.Visibility.Hidden;

                dgReservations.Visibility = System.Windows.Visibility.Hidden;
                dgCheckOut.Visibility = System.Windows.Visibility.Visible;
                hehe.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void btnViewReserve_Click(object sender, RoutedEventArgs e)
        {
            reservation = true;
            checkin = false;

            if (reservation == true && checkin == false)
            {
                CancelledCheckOut.Visibility = System.Windows.Visibility.Hidden;
                lblRoomNumber.Visibility = System.Windows.Visibility.Hidden;
                lblGuestNumber.Visibility = System.Windows.Visibility.Hidden;
                lblRoomSlipNumber.Visibility = System.Windows.Visibility.Hidden;
                lblCheckInDate.Visibility = System.Windows.Visibility.Hidden;
                lblCheckInTime.Visibility = System.Windows.Visibility.Hidden;
                lblNumberOfDays.Visibility = System.Windows.Visibility.Hidden;
                lblNumberOfHours.Visibility = System.Windows.Visibility.Hidden;
                lblDiscountCard.Visibility = System.Windows.Visibility.Hidden;
                lblDiscountId.Visibility = System.Windows.Visibility.Hidden;
                lblCheckOutDate.Visibility = System.Windows.Visibility.Hidden;
                lblRoomCharge.Visibility = System.Windows.Visibility.Hidden;
                lblUsername.Visibility = System.Windows.Visibility.Hidden;
                lblNetAmount.Visibility = System.Windows.Visibility.Hidden;
                lblAmountPaid.Visibility = System.Windows.Visibility.Hidden;
                lblStatus.Visibility = System.Windows.Visibility.Hidden;
                lblChange.Visibility = System.Windows.Visibility.Hidden;
                lblRoomTypeName.Visibility = System.Windows.Visibility.Hidden;
                txtRoomNumber.Visibility = System.Windows.Visibility.Hidden;
                txtGuestNumber.Visibility = System.Windows.Visibility.Hidden;
                txtRoomSlipNumber.Visibility = System.Windows.Visibility.Hidden;
                txtCheckInDate.Visibility = System.Windows.Visibility.Hidden;
                txtCheckInTime.Visibility = System.Windows.Visibility.Hidden;
                txtNumberOfDays.Visibility = System.Windows.Visibility.Hidden;
                txtNumberOfHours.Visibility = System.Windows.Visibility.Hidden;
                txtDiscountCard.Visibility = System.Windows.Visibility.Hidden;
                txtDiscountId.Visibility = System.Windows.Visibility.Hidden;
                txtCheckOutDate.Visibility = System.Windows.Visibility.Hidden;
                txtRoomCharge.Visibility = System.Windows.Visibility.Hidden;
                txtUsername.Visibility = System.Windows.Visibility.Hidden;
                txtNetAmount.Visibility = System.Windows.Visibility.Hidden;
                txtAmountPaid.Visibility = System.Windows.Visibility.Hidden;
                txtStatus.Visibility = System.Windows.Visibility.Hidden;
                txtChange.Visibility = System.Windows.Visibility.Hidden;
                txtRoomTypeName.Visibility = System.Windows.Visibility.Hidden;

                CancelledReservations.Visibility = System.Windows.Visibility.Visible;
                lblReservationId1.Visibility = System.Windows.Visibility.Visible;
                lblReservationFee.Visibility = System.Windows.Visibility.Visible;
                lblCustomerName.Visibility = System.Windows.Visibility.Visible;
                lblRoomNumber1.Visibility = System.Windows.Visibility.Visible;
                lblDateReserved.Visibility = System.Windows.Visibility.Visible;
                lblArrivalDate.Visibility = System.Windows.Visibility.Visible;
                lblArrivalTime.Visibility = System.Windows.Visibility.Visible;
                lblRequestDate.Visibility = System.Windows.Visibility.Visible;
                lblRequestTime.Visibility = System.Windows.Visibility.Visible;
                lblCardNumber.Visibility = System.Windows.Visibility.Visible;
                lblReservationNumber.Visibility = System.Windows.Visibility.Visible;
                txtReservationId1.Visibility = System.Windows.Visibility.Visible;
                txtReservationFee.Visibility = System.Windows.Visibility.Visible;
                txtCustomerName.Visibility = System.Windows.Visibility.Visible;
                txtRoomNumber1.Visibility = System.Windows.Visibility.Visible;
                txtDateReserved.Visibility = System.Windows.Visibility.Visible;
                txtArrivalDate.Visibility = System.Windows.Visibility.Visible;
                txtArrivalTime.Visibility = System.Windows.Visibility.Visible;
                txtRequestDate.Visibility = System.Windows.Visibility.Visible;
                txtRequestTime.Visibility = System.Windows.Visibility.Visible;
                txtCardNumber.Visibility = System.Windows.Visibility.Visible;
                txtReservationNumber.Visibility = System.Windows.Visibility.Visible;

                dgCheckOut.Visibility = System.Windows.Visibility.Hidden;
                dgReservations.Visibility = System.Windows.Visibility.Visible;
                hehe.Visibility = System.Windows.Visibility.Hidden;

            }
        }

        private void dgCheckOut_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var transaction = context.Transactions.Where(c => c.Status == "Cancelled").ToList();
                if (dgCheckOut.SelectedItem != null)
                {
                    var checkout = dgCheckOut.SelectedItem as CheckOutView;
                    var room = context.Rooms.FirstOrDefault(c => c.RoomNumber == checkout.RoomNumber);
                    txtRoomNumber.Text = room.RoomNumber;
                    txtGuestNumber.Text = checkout.GuestNumber;
                    txtRoomSlipNumber.Text = checkout.RoomSlipNumber;
                    txtCheckInDate.Text = checkout.CheckInDate;
                    txtCheckInTime.Text = checkout.CheckInTime;
                    // txtNumberOfDays.Text = checkout.Numbe;
                    txtDiscountCard.Text = checkout.DiscountCard;
                    txtDiscountId.Text = checkout.DiscountId;
                    txtCheckOutDate.Text = checkout.CheckOutDate;
                    txtRoomCharge.Text = checkout.RoomCharge;
                    txtUsername.Text = checkout.Username;
                    txtNetAmount.Text = checkout.NetAmount;
                    txtAmountPaid.Text = checkout.AmountPaid;
                    txtStatus.Text = checkout.Status;
                    txtChange.Text = checkout.Change;
                    txtRoomTypeName.Text = checkout.RoomTypeId;
                }
                else
                {
                    txtRoomNumber.Text = "";
                    txtGuestNumber.Text = "";
                    txtRoomSlipNumber.Text = "";
                    txtCheckInDate.Text = "";
                    txtCheckInTime.Text = "";
                    // txtNumberOfDays.Text = checkout.Numbe;
                    txtDiscountCard.Text = "";
                    txtDiscountId.Text = "";
                    txtCheckOutDate.Text = "";
                    txtRoomCharge.Text = "";
                    txtUsername.Text = "";
                    txtNetAmount.Text = "";
                    txtAmountPaid.Text = "";
                    txtStatus.Text = "";
                    txtChange.Text = "";
                }
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);

            frame.BackNavigationMode = BackNavigationMode.Root;
            frame.GoBack();
        }

        private void dgReservations_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            if (dgReservations.SelectedItem != null)
            {
                using (var context = new DatabaseContext())
                {
                    var reservation = dgReservations.SelectedItem as ReservationView;
                    txtRoomNumber1.Text = reservation.RoomNumber.ToString();
                    txtReservationFee.Text = reservation.ReservationFee;
                    txtCustomerName.Text = reservation.CustomerName;
                    txtDateReserved.Text = reservation.DateReserved;
                    txtArrivalDate.Text = reservation.ArrivalDate;
                    txtArrivalTime.Text = reservation.ArrivalTime;
                    txtRequestDate.Text = reservation.RequestDate;
                    txtRequestTime.Text = reservation.RequestTime;
                    txtCardNumber.Text = reservation.CardNumber;
                    txtReservationId1.Text = reservation.ReservationId.ToString();
                }
            }
            else
            {
                txtRoomNumber1.Text = "";
                txtReservationFee.Text = "";
                txtCustomerName.Text = "";
                txtRoomNumber.Text = "";
                txtDateReserved.Text = "";
                txtArrivalDate.Text = "";
                txtArrivalTime.Text = "";
                txtRequestDate.Text = "";
                txtRequestTime.Text = "";
                txtCardNumber.Text = "";

            }
        }

    }
}

