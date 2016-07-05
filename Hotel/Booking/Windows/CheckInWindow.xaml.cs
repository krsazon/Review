using DevExpress.XtraEditors;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hotel.Booking.Windows
{
    /// <summary>
    /// Interaction logic for CheckInWindow.xaml
    /// </summary>
    public partial class CheckInWindow : Window
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        int selectedId = 0;

        public CheckInWindow()
        {
            InitializeComponent();
        }

        public CheckInWindow(int id)
        {
            InitializeComponent();
            this.selectedId = id;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        //Experiment
            //DateTime checkinToday = DateTime.Parse("7/2/2016");
            //String checkinTicks = checkinToday.Ticks.ToString();
            //decimal decimalcheckin = decimal.Parse(checkinTicks);

            //DateTime bon = DateTime.Parse("7/1/2016");
            //String bon2 = bon.Ticks.ToString();
            //long ticks = long.Parse(bon2);
            //DateTime bon3 = new DateTime(ticks);

            ////decimal sagot = bon3 + decimalcheckin;
            ////decimal sagot2 = sagot / 864000000000;

            //String test = bon3.ToString("MM/dd/yyyy");

            //MessageBox.Show("Test: " + test);

            //String objectstring1 = "636030144000000000";
            //long object1 = long.Parse(objectstring1);
            //DateTime object1date = new DateTime(object1);

            //String objectstring2 = "8640000000";
            //long object2 = long.Parse(objectstring2);
            //DateTime object2date = new DateTime(object2);

            //long pinakasagot = 636030144000000000 + 864000000000;
            //DateTime sagotdate = new DateTime(pinakasagot);
       
            //MessageBox.Show("Tick1: " + object1.ToString());
            //MessageBox.Show("Tick2: " + object2.ToString());
            //MessageBox.Show("Pinakasagot: "+ pinakasagot.ToString());
       

            //MessageBox.Show("object1: " + object1date);
            //MessageBox.Show("object2: " + object2date);
            //MessageBox.Show("SagotDate: " + sagotdate);
            //MessageBox.Show("Sagot2: " + sagot2);
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

        public void Refresh()
        {
            using (var context = new DatabaseContext())
            {
                if (selectedId > 0)
                {
              
                    // var transaction = context.Transactions.FirstOrDefault(c => c.RoomId == selectedId);
                    var room = context.Rooms.FirstOrDefault(c => c.RoomId == selectedId);
                    var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == room.RoomTypeId);

                    if (room.Status == "Available")
                    {
                        dtCheckInDate.Text = DateTime.Now.ToString("MMM dd, yyyy");

                        String sweet = DateTime.Now.Hour.ToString();
                        Double king = Double.Parse(sweet);

                        if (king >= 13)
                        {
                            Double military = king - 12;
                            Double minute = Double.Parse(DateTime.Now.Minute.ToString());
                            if (minute < 10)
                            {
                                txtCheckInTime.Text = military + ":0" + DateTime.Now.Minute.ToString() + " PM";
                            }
                            else if (minute >= 10)
                            {
                                txtCheckInTime.Text = military + ":" + DateTime.Now.Minute.ToString() + " PM";
                            }
                        }
                        else if (king < 13)
                        {
                            txtCheckInTime.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + " AM";
                        }
                    }
                    txtRoomType.Text = roomtype.RoomTypeName;
                    txtRoomNo.Text = room.RoomNumber.ToString();
                    List<String> StaffList = context.Staffs.Where(c => c.StaffPosition == "Room Boy").Select(c => c.StaffName).ToList();
                    cmbRoomBoy.ItemsSource = StaffList;
                    //cmbTimeType.Items.Add("Month");
                    //List<String> DiscountList = context.Discounts.Select(c => c.DiscountName).ToList();
                    //cmbApplyDiscount.ItemsSource = DiscountList;
                    var roomtyperate = context.RoomTypeRates.FirstOrDefault(c => c.RoomTypeId == room.RoomTypeId);
                    var roomtyperatecount = context.RoomTypeRates.Count(c => c.RoomTypeId == room.RoomTypeId);
     
                    //MessageBox.Show(roomtyperatecount.ToString());
                    if (roomtyperatecount <= 0)
                    {
                        MessageBox.Show("Room Type Doesn't have rate");
                        this.Close();
                    }
                    else if (roomtyperatecount > 0) {
                        var RoomTypeRates = context.RoomTypeRates.ToList();
                            //MessageBox.Show(forroomtyperate.AmountTime);
                            var selectedRoom = context.Rooms.FirstOrDefault(c => c.RoomId == selectedId);
                            var roomtypes = context.RoomTypeRates.FirstOrDefault(c => c.RoomTypeId == selectedRoom.RoomTypeId);
                            var roomtypescount = context.RoomTypeRates.Count(c => c.RoomTypeId == selectedRoom.RoomTypeId);

                            MessageBox.Show(roomtypescount.ToString());
                            //MessageBox.Show(roomtypes.RoomTypeRateId.ToString());

                            foreach (var forroomtyperatemonths in RoomTypeRates)
                            {
                                if (forroomtyperatemonths.AmountTime == "Months" && forroomtyperatemonths.RoomTypeId == selectedRoom.RoomTypeId)
                                {
                                    cmbTimeType.Items.Add("Months");
                                    break;
                                }
                            }
                            foreach (var forroomtyperatedays in RoomTypeRates)
                            {
                                 if (forroomtyperatedays.AmountTime == "Days" && forroomtyperatedays.RoomTypeId == selectedRoom.RoomTypeId)
                                {
                                    cmbTimeType.Items.Add("Days");
                                    break;  
                                }   
                                }
                            

                            foreach (var forroomtyperatemhours in RoomTypeRates)
                            {
                            if (forroomtyperatemhours.AmountTime == "Hours" && forroomtyperatemhours.RoomTypeId == selectedRoom.RoomTypeId)
                                {
                                    cmbTimeType.Items.Add("Hours");
                                    break;
                                }
                            }
                         
   
                        }

                    }
                    }
                }
            
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (selectedId > 0)
                {

                    var Basetransaction = new Transaction();
                    var room = context.Rooms.FirstOrDefault(c => c.RoomNumber == txtRoomNo.Text);
                    var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeName == txtRoomType.Text);

                    Basetransaction.RoomId = room.RoomId;
                    Basetransaction.RoomTypeId = roomtype.RoomTypeId;
                    Basetransaction.RoomSlipNumber = Int32.Parse(txtRoomSlip.Text);
                    Basetransaction.CheckInDate = DateTime.Today;
                    Basetransaction.CheckInTime = txtCheckInTime.Text;
                    //Basetransaction.StaffId = staff.StaffId;
                    Basetransaction.Status = "CheckedIn";

                    //var staff = context.Staffs.FirstOrDefault(c => c.StaffName == cmbRoomBoy.Text);


                    //if(cmbTimeType.Text == "Days"){

                    DateTime currentdate = DateTime.Today;
                    String currentdatestring = currentdate.Ticks.ToString();
                    long currentdateticks = long.Parse(currentdatestring);

                    long timeticks = 864000000000 * long.Parse(cmbTime.Text);

                    long overallanswer = currentdateticks + timeticks;
                    DateTime CheckOutDateTime = new DateTime(overallanswer);


                    String CheckOut = CheckOutDateTime.ToString("MM/dd/yyyy");
                    //MessageBox.Show(CheckOut);
                    //MessageBox.Show("Time Text: " + cmbTime.Text);
                    //MessageBox.Show("Time Ticks: "+ timeticks.ToString());
                    if (cmbTimeType.Text == "Days")
                    {
                        Basetransaction.CheckOutDate = DateTime.Parse(CheckOut);
                        String checkouttime = DateTime.Now.ToString("hh:mm tt");
                        Basetransaction.CheckOutTime = checkouttime;
                    }
                    else if (cmbTimeType.Text == "Hours")
                    {

                        int answer = Int32.Parse(cmbTime.Text) + Int32.Parse(DateTime.Now.Hour.ToString());
                        if (answer > 23)
                        {
                            DateTime checkout = DateTime.Now;
                            checkout = checkout.AddHours(Int32.Parse(cmbTime.Text));
                            Basetransaction.CheckOutDate = DateTime.Today.AddDays(1);
                            Basetransaction.CheckOutTime = checkout.ToString("h:mm tt");
                            MessageBox.Show(checkout.ToString("h:mm tt"));
                        }
                        else if (answer <= 23)
                        {
                            DateTime checkout = DateTime.Now;
                            checkout = checkout.AddHours(Int32.Parse(cmbTime.Text));
                            Basetransaction.CheckOutDate = DateTime.Today;
                            Basetransaction.CheckOutTime = checkout.ToString("h:mm tt");
                        }
                    }
                    else if (cmbTimeType.Text == "Months")
                    {
                        int answer = Int32.Parse(cmbTime.Text) + Int32.Parse(DateTime.Now.Month.ToString());
                        DateTime checkoutdate = DateTime.Now;
                        checkoutdate = checkoutdate.AddMonths(Int32.Parse(cmbTime.Text));
                        Basetransaction.CheckOutDate = checkoutdate;
                        var checkouttime = DateTime.Now;
                        Basetransaction.CheckOutTime = checkouttime.ToString("h:mm tt");
                    }
                    //Basetransaction.CheckOutTime = ;
                    //}
                    //if (cmbTimeType.Text == "Days")
                    //{

                    //    Basetransaction.CheckOutDate = DateTime.Parse(DateTime.Today.Month.ToString() + "/" + DateTime.Today.Day.ToString() + "/" + DateTime.Today.Year.ToString());

                    //}

                    MethodsClass.ShowNotification("Succesfully Checked In!");
                    context.Transactions.Add(Basetransaction);
                    context.SaveChanges();
                    Refresh();
                    this.Close();
                }


 
            }
        }
        
        //}

        private void cmbTimeType_SelectedIndexChanged_1(object sender, RoutedEventArgs e)
        {
            if (cmbTimeType.Text != "Select Type...")
            {
                cmbTime.IsEnabled = true;
            }
            if (cmbTimeType.Text == "Hours")
            {
                ComboBoxEdit combo = new ComboBoxEdit();
                cmbTime.Items.Clear();
                cmbTime.Text = "1";
                cmbTime.Items.Add("1");
                cmbTime.Items.Add("2");
                cmbTime.Items.Add("3");
                cmbTime.Items.Add("4");
                cmbTime.Items.Add("5");
                cmbTime.Items.Add("6");
                cmbTime.Items.Add("7");
                cmbTime.Items.Add("8");
                cmbTime.Items.Add("9");
                cmbTime.Items.Add("10");
                cmbTime.Items.Add("11");
                cmbTime.Items.Add("12");
                cmbTime.Items.Add("13");
                cmbTime.Items.Add("14");
                cmbTime.Items.Add("15");
                cmbTime.Items.Add("16");
                cmbTime.Items.Add("17");
                cmbTime.Items.Add("18");
                cmbTime.Items.Add("19");
                cmbTime.Items.Add("20");
                cmbTime.Items.Add("21");
                cmbTime.Items.Add("22");
                cmbTime.Items.Add("23");
            }

            if (cmbTimeType.Text == "Days")
            {
                ComboBoxEdit combo = new ComboBoxEdit();
                cmbTime.Items.Clear();
                cmbTime.Text = "1";
                cmbTime.Items.Add("1");
                cmbTime.Items.Add("2");
                cmbTime.Items.Add("3");
                cmbTime.Items.Add("4");
                cmbTime.Items.Add("5");
                cmbTime.Items.Add("6");
                cmbTime.Items.Add("7");
                cmbTime.Items.Add("8");
                cmbTime.Items.Add("9");
                cmbTime.Items.Add("10");
                cmbTime.Items.Add("11");
                cmbTime.Items.Add("12");
                cmbTime.Items.Add("13");
                cmbTime.Items.Add("14");
                cmbTime.Items.Add("15");
                cmbTime.Items.Add("16");
                cmbTime.Items.Add("17");
                cmbTime.Items.Add("18");
                cmbTime.Items.Add("19");
                cmbTime.Items.Add("20");
                cmbTime.Items.Add("21");
                cmbTime.Items.Add("22");
                cmbTime.Items.Add("23");
                cmbTime.Items.Add("24");
                cmbTime.Items.Add("25");
                cmbTime.Items.Add("26");
                cmbTime.Items.Add("27");
                cmbTime.Items.Add("28");
                cmbTime.Items.Add("29");
                cmbTime.Items.Add("30");
                cmbTime.Items.Add("31");
            }
            if (cmbTimeType.Text == "Months")
            {
                ComboBoxEdit combo = new ComboBoxEdit();
                cmbTime.Items.Clear();
                cmbTime.Text = "1";
                cmbTime.Items.Add("1");
                cmbTime.Items.Add("2");
                cmbTime.Items.Add("3");
                cmbTime.Items.Add("4");
                cmbTime.Items.Add("5");
                cmbTime.Items.Add("6");
                cmbTime.Items.Add("7");
                cmbTime.Items.Add("8");
                cmbTime.Items.Add("9");
                cmbTime.Items.Add("10");
                cmbTime.Items.Add("11");
                cmbTime.Items.Add("12");
            }
        }

        private void chPet_Checked(object sender, RoutedEventArgs e)
        {
            txtPet.IsEnabled = true;
        }

        private void chPet_Unchecked(object sender, RoutedEventArgs e)
        {
            txtPet.IsEnabled = false;
        }

        private void cmbApplyDiscount_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            txtDiscountCard.IsEnabled = true;
        }

        private void btnBrowse1_Click(object sender, RoutedEventArgs e)
        {
         //   var window = new Windows.ReservationListWindow(selectedId);
         ////   window.roomequipmentid = txtRoomNumber.Text;
         //   MethodsClass.ShowPropertyWindow(window);
         //  cmbReservation.Text = Windows.ReservationListWindow.txt;

           
            //foreach (object item in cmbTimeType.Items)
            //{
            //    if (item.ToString() == "Hours")
            //    {
            //        MessageBox.Show("Hours: " + item.ToString());

            //    }

            //    MessageBox.Show("All: " + item.ToString());
            //}

            //var count = cmbTimeType.Items.Count;
                

            //for (int i = 0; i < 3; i++)
            //{
            //    cmbTimeType.Items.Remove("Hours");
            //}

            //for (int i = 0; i < 3; i++)
            //{
            //    cmbTimeType.Items.Remove("Hours");                
            //}

        }
    }
}
