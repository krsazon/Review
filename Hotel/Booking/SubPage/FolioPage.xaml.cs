using DevExpress.DataAccess.Native.DB;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.WindowsUI;
using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Hotel.Booking.SubPage
{
    /// <summary>
    /// Interaction logic for Folio.xaml
    /// </summary>
    public partial class FolioPage : UserControl
    {
        int selectedId = 0;

        public FolioPage()
        {
            InitializeComponent();
        }

        public FolioPage(int id)
        {
            InitializeComponent();
            this.selectedId = id;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            Refresh();

        }

        public void Refresh()
        {

            using (var context = new DatabaseContext())
            {
                var room = context.Rooms.FirstOrDefault(c => c.RoomId == selectedId);
                var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == room.RoomTypeId);
                var roomtyperate = context.RoomTypeRates.FirstOrDefault(c => c.RoomTypeId == room.RoomTypeId);
                var transaction = context.Transactions.FirstOrDefault(c => c.RoomId == room.RoomId);
                var staff = context.Staffs.FirstOrDefault(c => c.StaffId == transaction.StaffId);
                var count = context.TransactionItems.Where(c => c.RoomId == selectedId).Where(c => c.Cancelled == false).Select(c => c.ItemTotal).ToList();
                txtTotalFBO.Text = count.Sum().ToString();
                txtRoomNumber.Text = room.RoomNumber.ToString();
                txtGuestNumber.Text = transaction.GuestNumber.ToString();
                txtRoomSlip.Text = transaction.RoomSlipNumber.ToString();
                dtCheckInDate.Text = transaction.CheckInDate.ToString("MMM dd, yyyy");
                txtCheckInTime.Text = transaction.CheckInTime.ToString();
                dtCheckOutDate.Text = transaction.CheckOutDate.ToString("MMM dd, yyyy");
                txtCheckOutTime.Text = transaction.CheckOutTime.ToString();
                txtRoomBoy.Text = staff.StaffName;
                rdAdvised.IsChecked = true;
                //var holidayselect = context.HolidayRates.Select(c => c.RateId);
                //var occuholidayselect = context.OccupancyRates.Select(c => c.OccupancyRateId);

             //   if (occuholidayselect.Count() > 0)
               // {
                 //   var occupancy = context.OccupancyRates.FirstOrDefault(c => c.OccupancyRateName == txtOccupancyPercent.Text);
                  //  txtOccupancyRate.Text = occupancy.OccupancyRateAmount.ToString();
                //}


                #region ViewData
                var Transactions = context.Transactions.ToList();
                var TransactionItems = context.TransactionItems.ToList();
                var viewList = new List<TransactionItemView>();
                foreach (var transactionitem in TransactionItems)
                {
                    if (transactionitem.TransactionId == transaction.TransactionId)
                    {
                        viewList.Add(new TransactionItemView()
                        {

                            TransactionItemId = transactionitem.TransactionItemId,
                            TransactionId = transactionitem.TransactionId.ToString(),
                            ItemId = transactionitem.ItemId.ToString(),
                            ItemQuantity = transactionitem.ItemQuantity.ToString(),
                            UnitPrice = transactionitem.UnitPrice.ToString(),
                            Cancelled = transactionitem.Cancelled.ToString(),
                            ItemTotal = transactionitem.ItemTotal.ToString(),
                            Username = transactionitem.Username,
                            //   EntryDate = transactionitem.EntryDate.ToString(),
                            //   EntryTime = transactionitem.EntryTime,

                        });
                    }
                    dgTransactionItem.ItemsSource = null;
                    dgTransactionItem.ItemsSource = viewList;
                }
                #endregion
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new Booking.Windows.TransactionItemWindow(selectedId);
            MethodsClass.ShowPropertyWindow(window);
            txtTotalFBO.Text = Windows.TransactionItemWindow.fbo.ToString();
            Refresh();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            frame.BackNavigationMode = BackNavigationMode.Root;
            frame.GoBack();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            frame.BackNavigationMode = BackNavigationMode.PreviousScreen;
            frame.GoBack();
        }

        private void dgTransactionItem_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (selectedId > 0)
                {
                    var selecteditem = dgTransactionItem.SelectedItem as TransactionItemView;
                    var transactionitem = context.TransactionItems.FirstOrDefault(c => c.TransactionItemId == selecteditem.TransactionItemId);
                    transactionitem.Cancelled = true;
                    context.SaveChanges();
                    Refresh();
                }
            }
        }

        private void btnPayment_Click(object sender, RoutedEventArgs e)
        {
            var window = new Booking.Windows.PaymentWindow(selectedId);
            window.spnNetAmount.Text = txtNetDue.Text;
            window.spnLessDiscount.Text = txtLessDiscount.Text;
            window.spnFBOCharge.Text = txtTotalFBO.Text;
            MethodsClass.ShowPropertyWindowPayment(window);
            Refresh();
        }

        private void rdNext_Checked(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                #region Amount
                var room = context.Rooms.FirstOrDefault(c => c.RoomId == selectedId);
                var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == room.RoomTypeId);
                var roomtyperate = context.RoomTypeRates.FirstOrDefault(c => c.RoomTypeId == room.RoomTypeId);
                var transaction = context.Transactions.FirstOrDefault(c => c.RoomId == room.RoomId);
                var staff = context.Staffs.FirstOrDefault(c => c.StaffId == transaction.StaffId);
                //     decimal a = roomtype.Amount * transaction.NumberTime;
                //if (transaction.TimeType == "Hours")
                //{
                //    #region Hour
                //    List<string> datelist = new List<string>();
                //    datelist.Add(transaction.CheckInDate.ToString("MMM dd, yyyy"));
                //    datelist.Add(transaction.CheckInTime.ToString()); ;
                //    DateTime date = DateTime.Parse(String.Join(" ", datelist));
                //    DateTime start = date;
                //    DateTime end = DateTime.Parse(DateTime.Now.ToString("MMM dd, yyyy hh:mm tt"));
                //    TimeSpan duration = end - start;
                //    double dec = duration.TotalHours;
                //    Decimal dif = Decimal.Parse(dec.ToString("n0"));
                //    var roomtyperatehour = context.RoomTypeRates.FirstOrDefault(c => c.AmountNumberTime == dif && c.AmountTime == "Hours" && c.RoomTypeRateNameId == room.RoomTypeId);
                //    if (roomtyperate.AmountNumberTime == dif && roomtyperate.AmountTime == "Hours")
                //    {
                //        txtRoomCharge.Text = roomtyperatehour.Amount.ToString();
                //    }
                //    else if (dif == 0)
                //    {
                //        txtRoomCharge.Text = roomtyperate.Amount.ToString();
                //    }
                //    else if (roomtyperate.AmountNumberTime != dif)
                //    {
                //        {
                //            var roomtyperatemax = context.RoomTypeRates.Where(c => c.RoomTypeRateNameId == room.RoomTypeId && c.AmountNumberTime < dif && c.AmountTime == "Hours").Max(c => c.Amount);
                //            var roomtyperateselect = context.RoomTypeRates.Where(c => c.RoomTypeRateNameId == room.RoomTypeId && c.AmountNumberTime < dif && c.AmountTime == "Hours").Max(c => c.AmountNumberTime);

                //            List<string> datelists = new List<string>();
                //            datelists.Add(transaction.CheckInDate.ToString("MMM dd, yyyy"));
                //            datelists.Add(DateTime.Parse(transaction.CheckInTime).AddHours(roomtyperateselect).ToString("HH:mm tt")); ;
                //            DateTime datejoin = DateTime.Parse(String.Join(" ", datelists));
                //            DateTime startad = datejoin;
                //            DateTime ends = DateTime.Parse(DateTime.Now.ToString("MMM dd, yyyy hh:mm tt"));
                //            TimeSpan durationad = ends - startad;
                //            double difs = durationad.TotalMinutes / roomtype.AdditionalChargesNumberTime;
                //            double difper = difs + 0.5;
                //            if (roomtype.AdditionalChargesTime == "Minutes")
                //            {
                //                decimal text = roomtype.AdditionalChargesAmount * Decimal.Parse(difper.ToString("n0"));
                //                decimal totalcharge = text + roomtyperatemax;
                //                txtRoomCharge.Text = totalcharge.ToString();
                //            }
                //        }
                //    }
                //    #endregion
                //}
                //else if (transaction.TimeType == "Days")
                //{
                //    List<string> datelist = new List<string>();
                //    datelist.Add(transaction.CheckInDate.ToString("MMM dd, yyyy"));
                //    datelist.Add(transaction.CheckInTime.ToString()); ;
                //    DateTime date = DateTime.Parse(String.Join(" ", datelist));
                //    DateTime start = date;
                //    DateTime end = DateTime.Parse(DateTime.Now.ToString("MMM dd, yyyy hh:mm tt"));
                //    TimeSpan duration = end - start;
                //    double dif = duration.TotalHours;
                //    Decimal dec = Decimal.Parse(dif.ToString("n0"));
                //    decimal totalroom = transaction.RoomCharge * dec;
                //    if (dec == 0)
                //    {
                //        txtRoomCharge.Text = transaction.RoomCharge.ToString();
                //    }
                //    else
                //    {
                //        txtRoomCharge.Text = totalroom.ToString();
                //    }
                //}
                decimal roomz = Decimal.Parse(txtRoomCharge.Text);
                decimal fbo = decimal.Parse(txtTotalFBO.Text);
                var occupancy = context.OccupancyRates.FirstOrDefault(c => c.OccupancyRateName == txtOccupancyPercent.Text);
                decimal holidayrate = 0;
                decimal occupancyrate = 0;
                var dateholiday = DateTime.Now.ToString("yyyy-dd-MM");
                var holiday = context.HolidayRates.FirstOrDefault(c => c.HolidayDate == dateholiday);
                if (holiday != null)
                {
                    holidayrate = holiday.Rate;
                }
                if (occupancy != null)
                {
                    occupancyrate = occupancy.OccupancyRateAmount;
                }
                decimal ass = roomz + fbo + occupancyrate + holidayrate;
                decimal totalamountdue = roomz + fbo;
                txtHolidayRate.Text = holidayrate.ToString();
                txtOccupancyRate.Text = occupancyrate.ToString();
                txtTotal.Text = totalamountdue.ToString();
                var discount = context.Discounts.FirstOrDefault(c => c.DiscountId == transaction.DiscountId);

                if (transaction.DiscountId > 0)
                {
                    if (discount.DiscountType == "Amount")
                    {
                        txtLessDiscount.Text = discount.DiscountAmount.ToString();
                        decimal c = decimal.Parse(txtTotal.Text);
                        decimal disc = c - discount.DiscountAmount;
                        decimal totalamount = disc + occupancyrate + holidayrate;
                        txtNetDue.Text = totalamount.ToString();
                    }
                    else if (discount.DiscountType == "Percent")
                    {
                        txtLessDiscount.Text = discount.DiscountAmount.ToString();
                        decimal value = discount.DiscountAmount;
                        decimal c = decimal.Parse(txtTotal.Text);
                        decimal d = value * c;
                        decimal disco = c - d;
                        decimal total = disco + occupancyrate + holidayrate;
                        txtNetDue.Text = total.ToString();
                    }
                }
                else
                {
                    txtLessDiscount.Text = "";
                    txtNetDue.Text = ass.ToString();
                }

                //txtActualHours.Text = transaction.NumberTime.ToString();

                #endregion
            }
        }

        private void rdAdvised_Checked(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                #region Amount
                var room = context.Rooms.FirstOrDefault(c => c.RoomId == selectedId);
                var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == room.RoomTypeId);
                var roomtyperate = context.RoomTypeRates.FirstOrDefault(c => c.RoomTypeId == room.RoomTypeId);
                var roomtyperatemin = context.RoomTypeRates.Where(c => c.RoomTypeId == room.RoomTypeId && c.AmountTime == "Hours").Min(c => c.Amount);
                var roomtyperatenumbermin = context.RoomTypeRates.Where(c => c.RoomTypeId == room.RoomTypeId).Min(c => c.AmountNumberTime);
                var transaction = context.Transactions.FirstOrDefault(c => c.RoomId == room.RoomId);
                var staff = context.Staffs.FirstOrDefault(c => c.StaffId == transaction.StaffId);
                if (roomtyperate.AmountTime == "Hours")
                {
                    List<string> datelist = new List<string>();
                    datelist.Add(transaction.CheckInDate.ToString("MMM dd, yyyy"));
                    datelist.Add(transaction.CheckInTime.ToString()); ;
                    DateTime date = DateTime.Parse(String.Join(" ", datelist));
                    DateTime start = date;
                    DateTime end = DateTime.Parse(DateTime.Now.ToString("MMM dd, yyyy hh:mm tt"));
                    TimeSpan duration = end - start;
                    double dec = duration.TotalHours / roomtyperatenumbermin;
                    Decimal dif = Decimal.Parse(dec.ToString("n0"));
                    decimal totalroom = roomtyperatemin * dif;
                    if (dif == 0)
                    {
                        txtRoomCharge.Text = roomtyperatemin.ToString();
                    }
                    else
                    {
                        txtRoomCharge.Text = totalroom.ToString();
                    }
                }
                #endregion
                var holidayselect = context.HolidayRates.Select(c => c.RateId);
                var occuholidayselect = context.OccupancyRates.Select(c => c.OccupancyRateId);
                decimal roomz = Decimal.Parse(txtRoomCharge.Text);
                decimal fbo = decimal.Parse(txtTotalFBO.Text);
                var occupancy = context.OccupancyRates.FirstOrDefault(c => c.OccupancyRateName == txtOccupancyPercent.Text);
                decimal holidayrate = 0;
                decimal occupancyrate = 0;
                var dateholiday = DateTime.Now.ToString("yyyy-dd-MM");
                var holiday = context.HolidayRates.FirstOrDefault(c => c.HolidayDate == dateholiday);
                 if (holiday != null)
                {
                    holidayrate = holiday.Rate;
                }
                if (occupancy != null)
                {
                   occupancyrate = occupancy.OccupancyRateAmount;
                }

                decimal totalamountdue = roomz + fbo;
                decimal ass = roomz + fbo + occupancyrate + holidayrate;
                txtHolidayRate.Text = holidayrate.ToString();
                txtOccupancyRate.Text = occupancyrate.ToString();
                txtTotal.Text = totalamountdue.ToString();
                var discount = context.Discounts.FirstOrDefault(c => c.DiscountId == transaction.DiscountId);
                if (transaction.DiscountId > 0)
                {
                    if (discount.DiscountType == "Amount")
                    {
                        txtLessDiscount.Text = discount.DiscountAmount.ToString();
                        decimal c = decimal.Parse(txtTotal.Text);
                        decimal disc = c - discount.DiscountAmount;
                        decimal totalamount = disc + occupancyrate + holidayrate;
                        txtNetDue.Text = totalamount.ToString();
                    }
                    else if (discount.DiscountType == "Percent")
                    {
                        txtLessDiscount.Text = discount.DiscountAmount.ToString();
                        decimal value = discount.DiscountAmount;
                        decimal c = decimal.Parse(txtTotal.Text);
                        decimal d = value * c;
                        decimal disco = c - d;
                        decimal total = disco + occupancyrate + holidayrate;
                        txtNetDue.Text = total.ToString();
                    }
                }
                else
                {
                    txtLessDiscount.Text = "";
                    txtNetDue.Text = ass.ToString();
                }
                //txtActualHours.Text = transaction.NumberTime.ToString();
            }
        }
    }
}

