using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Scheduler;
using DevExpress.Xpf.Scheduler.Menu;
using DevExpress.Xpf.Scheduler.UI;
using DevExpress.Xpf.WindowsUI;
using DevExpress.XtraScheduler;
using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ViewReservation.xaml
    /// </summary>
    public partial class ViewReservationPage : UserControl
    {

        bool timeline = true;
        bool elsemonths = false;

        int selectedId = 0;
        int nextpage = 0;
        public ViewReservationPage()
        {
            InitializeComponent();
        }

        public ViewReservationPage(int id)
        {
            InitializeComponent();
            this.selectedId = id;
        }



        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {

                cmbStatus.Items.Add("All");
                cmbStatus.Items.Add("Checked In");
                cmbStatus.Items.Add("Reserved");
                cmbStatus.Items.Add("Critical");

                cmbRoomNumber.Items.Add("All");

                var Rooms = context.Rooms.ToList();

                foreach (var forrooms in Rooms)
                {
                    cmbRoomNumber.Items.Add(forrooms.RoomNumber);
                }

                var room1 = context.Rooms.FirstOrDefault(c => c.RoomId == selectedId);
                cmbRoomNumber.Text = room1.RoomNumber;
                //Start of the month
                schedulerControl1.MonthView.ShowWeekend = true;
                schedulerControl1.MonthView.WeekCount = 5;
                schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Timeline;
                DateTime startDate = schedulerControl1.SelectedInterval.Start;
                schedulerControl1.Start = new DateTime(startDate.Year, startDate.Month, 1);

                cmbRoomNumber.IsEnabled = false;
                cmbStatus.IsEnabled = false;
                btnLoad.IsEnabled = false;

                timeline = true;
                elsemonths = false;

                cmbRoomNumber.Text = "All";
                cmbStatus.Text = "All";

                Refresh();
        
                var Room = context.Rooms.ToList();
                foreach (var forroom in Room)
                {
                    var schedulerStorage = schedulerControl1.Storage;

                    var res = schedulerStorage.CreateResource(forroom.RoomId);
                    
                    res.Caption = "Rm. " + forroom.RoomNumber;

                    schedulerStorage.ResourceStorage.Add(res);
                }



            
        }
   }


        
        public void Refresh()
        {

            using (var context = new DatabaseContext())
            {
                var reservationcount = context.Reservations.ToList();
                var transactioncount = context.Transactions.Count(c => c.Status == "CheckedIn");
                var transactionroot = context.Transactions.ToList();

                var total = transactionroot.Count() + reservationcount.Count();

                var schedulerStorage = schedulerControl1.Storage;
                //Appointment apt = schedulerStorage.AppointmentStorage[0];

                schedulerStorage.AppointmentStorage.Clear();


                AddAppointments();

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
            }
        }

        private void SchedulerControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void schedulerControl1_PopupMenuShowing(object sender, DevExpress.Xpf.Scheduler.SchedulerMenuEventArgs e)
        {
            if (e.Menu.Name == SchedulerMenuItemName.DefaultMenu)
            {
                for (int i = 0; i < e.Menu.Items.Count; i++)
                {
                    SchedulerBarItem menuItem = e.Menu.Items[i] as SchedulerBarItem;
                    if (menuItem != null)
                    {
                        if (menuItem != null && menuItem.Name == SchedulerMenuItemName.NewAllDayEvent)
                        {
                            menuItem.Content = "New Task for the Entire Day";
                            break;
                        }
                    }
                }

                e.Customizations.Add(new RemoveBarItemAndLinkAction()
                {
                    ItemName = SchedulerMenuItemName.NewRecurringEvent
                });

                BarButtonItem myMenuItem = new BarButtonItem();
                myMenuItem.Name = "customItem";
                myMenuItem.Content = "Appoint";

                BarButtonItem myMenuItem1 = new BarButtonItem();
                myMenuItem1.Name = "customItem1";
                myMenuItem1.Content = "King";

                BarButtonItem myMenuItem2 = new BarButtonItem();
                myMenuItem2.Name = "customItem2";
                myMenuItem2.Content = "Sweet";

                e.Customizations.Add(myMenuItem);
                e.Customizations.Add(myMenuItem1);
                e.Customizations.Add(myMenuItem2);

                myMenuItem.ItemClick += new ItemClickEventHandler(customItem_ItemClick);
                myMenuItem1.ItemClick += new ItemClickEventHandler(customItem1_ItemClick);
                myMenuItem2.ItemClick += new ItemClickEventHandler(Sweet_ItemClick);
            }
        }

        private void customItem_ItemClick(object sender, ItemClickEventArgs e)
        {

            String date = schedulerControl1.SelectedInterval.Start.Month.ToString() + "/" + schedulerControl1.SelectedInterval.Start.Day.ToString() + "/" + schedulerControl1.SelectedInterval.Start.Year.ToString();
            var window = new Booking.Windows.ReservationWindow();

            MethodsClass.ShowPropertyWindow(window);

        }

        private void customItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            String date = schedulerControl1.SelectedInterval.Start.Month.ToString() + "/" + schedulerControl1.SelectedInterval.Start.Day.ToString() + "/" + schedulerControl1.SelectedInterval.Start.Year.ToString();
            var window = new Booking.Windows.ReservationWindow();

            MethodsClass.ShowPropertyWindow(window);
        }

        private void Sweet_ItemClick(object sender, ItemClickEventArgs e)
        {
            String date = schedulerControl1.SelectedInterval.Start.Month.ToString() + "/" + schedulerControl1.SelectedInterval.Start.Day.ToString() + "/" + schedulerControl1.SelectedInterval.Start.Year.ToString();
            var window = new Booking.Windows.ReservationWindow();

            MethodsClass.ShowPropertyWindow(window);
        }

        public void schedulerControl1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (timeline == true && elsemonths == false)
                {
                    var selectedAppointments = schedulerControl1.SelectedResource.Id;
                    String date = schedulerControl1.SelectedInterval.Start.Month.ToString() + "/" + schedulerControl1.SelectedInterval.Start.Day.ToString() + "/" + schedulerControl1.SelectedInterval.Start.Year.ToString();

                    var window2 = new Booking.Windows.ReservationWindow(Int32.Parse(selectedAppointments.ToString()));
                    window2.dtRequestDate.Text = date;
                    MethodsClass.ShowPropertyWindow(window2);
                    Refresh();
                    cmbRoomNumber.Text = "All";
                    cmbStatus.Text = "All";
                }
                else if (timeline == false && elsemonths == true)
                {
                    String date = schedulerControl1.SelectedInterval.Start.Month.ToString() + "/" + schedulerControl1.SelectedInterval.Start.Day.ToString() + "/" + schedulerControl1.SelectedInterval.Start.Year.ToString();

                    if (cmbRoomNumber.Text != "All")
                    {
                        var room = context.Rooms.FirstOrDefault(c => c.RoomNumber == cmbRoomNumber.Text);
                        var window2 = new Booking.Windows.ReservationWindow(room.RoomId);
                        window2.dtRequestDate.Text = date;
                        MethodsClass.ShowPropertyWindow(window2);
                    }
                    else if (cmbRoomNumber.Text == "All")
                    {
                        var window2 = new Booking.Windows.ReservationWindow();
                        window2.dtRequestDate.Text = date;
                        MethodsClass.ShowPropertyWindow(window2);
                    }
                    Refresh();
                    cmbRoomNumber.Text = "All";
                    cmbStatus.Text = "All";
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);

            frame.BackNavigationMode = BackNavigationMode.PreviousScreen;
            frame.GoBack();
        }

        private void btnReserve_Click(object sender, RoutedEventArgs e)
        {
            using(var context = new DatabaseContext()){
            if (timeline == true && elsemonths == false)
            {
                var selectedAppointments = schedulerControl1.SelectedResource.Id;
                String date = schedulerControl1.SelectedInterval.Start.Month.ToString() + "/" + schedulerControl1.SelectedInterval.Start.Day.ToString() + "/" + schedulerControl1.SelectedInterval.Start.Year.ToString();
       
                var window2 = new Booking.Windows.ReservationWindow(Int32.Parse(selectedAppointments.ToString()));
                window2.dtRequestDate.Text = date;
                MethodsClass.ShowPropertyWindow(window2);
                Refresh();
                cmbRoomNumber.Text = "All";
                cmbStatus.Text = "All";
            }
            else if (timeline == false && elsemonths == true)
            {
                String date = schedulerControl1.SelectedInterval.Start.Month.ToString() + "/" + schedulerControl1.SelectedInterval.Start.Day.ToString() + "/" + schedulerControl1.SelectedInterval.Start.Year.ToString();
                
                if (cmbRoomNumber.Text != "All"){
                var room = context.Rooms.FirstOrDefault(c => c.RoomNumber == cmbRoomNumber.Text);
                var window2 = new Booking.Windows.ReservationWindow(room.RoomId);
                window2.dtRequestDate.Text = date;
                MethodsClass.ShowPropertyWindow(window2);
                } else if (cmbRoomNumber.Text == "All"){
                    var window2 = new Booking.Windows.ReservationWindow();
                    window2.dtRequestDate.Text = date;
                    MethodsClass.ShowPropertyWindow(window2);
                }
                Refresh();
                cmbRoomNumber.Text = "All";
                cmbStatus.Text = "All";
            }
            }
        }

        private void btnTimeline_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var Room = context.Rooms.ToList();
                foreach (var forroom in Room)
                {
                    var schedulerStorage = schedulerControl1.Storage;

                    var res = schedulerStorage.CreateResource(forroom.RoomId);

                    res.Caption = "Rm. " + forroom.RoomNumber;

                    schedulerStorage.ResourceStorage.Add(res);
                }

                timeline = true;
                elsemonths = false;

                cmbRoomNumber.IsEnabled = false;
                cmbStatus.IsEnabled = false;
                btnLoad.IsEnabled = false;

                cmbRoomNumber.Text = "All";
                cmbStatus.Text = "All";

                Refresh();
                schedulerControl1.ActiveViewType = SchedulerViewType.Timeline;
            }
        }

        private void btnMonthView_Click(object sender, RoutedEventArgs e)
        {
            var schedulerStorage = schedulerControl1.Storage;
            schedulerStorage.ResourceStorage.Clear();
            timeline = false;
            elsemonths = true;

            cmbRoomNumber.IsEnabled = true;
            cmbStatus.IsEnabled = true;
            btnLoad.IsEnabled = true;

            AllAppointments();
            schedulerControl1.ActiveViewType = SchedulerViewType.Month;
        }

        private void btnWeekView_Click(object sender, RoutedEventArgs e)
        {
            var schedulerStorage = schedulerControl1.Storage;
            schedulerStorage.ResourceStorage.Clear();
            timeline = false;
            elsemonths = true;

            cmbRoomNumber.IsEnabled = true;
            cmbStatus.IsEnabled = true;
            btnLoad.IsEnabled = true;

            AllAppointments();
            schedulerControl1.ActiveViewType = SchedulerViewType.Week;

        }

        private void btnDayView_Click(object sender, RoutedEventArgs e)
        {
            var schedulerStorage = schedulerControl1.Storage;
            schedulerStorage.ResourceStorage.Clear();
            timeline = false;
            elsemonths = true;

            cmbRoomNumber.IsEnabled = true;
            cmbStatus.IsEnabled = true;
            btnLoad.IsEnabled = true;

            AllAppointments();
            schedulerControl1.ActiveViewType = SchedulerViewType.Day;
        }





        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            AllAppointments();
        }


        public void AllAppointments()
        {
            using (var context = new DatabaseContext())
            {
                var schedulerStorage = schedulerControl1.Storage;
                schedulerStorage.AppointmentStorage.Clear();

                if (cmbStatus.Text == "All" && cmbRoomNumber.Text == "All")
                {
                    Refresh();
                }
                else if (cmbStatus.Text == "All")
                {
                    AllStatus();
                }
                else if (cmbStatus.Text == "Checked In" && cmbRoomNumber.Text == "All")
                {
                    var counttransaction = context.Transactions.ToList();
                    foreach (var fortransaction in counttransaction)
                    {
                        var apt = schedulerStorage.CreateAppointment(AppointmentType.Pattern);
                        var reminder = apt.CreateNewReminder();
                        var room3 = context.Rooms.FirstOrDefault(c => c.RoomNumber == cmbRoomNumber.Text);

                        var transactionSelect = context.Transactions.Select(c => c.ReservationId).FirstOrDefault();
                        var roomCheckedIn = context.Rooms.FirstOrDefault(c => c.RoomId == fortransaction.RoomId);

                        DateTime exp = DateTime.Parse(fortransaction.CheckInTime);
                        DateTime exp2 = DateTime.Parse(fortransaction.CheckOutTime);

                        String CheckIn = fortransaction.CheckInDate.ToString("MM/dd/yyyy") + " " + exp.ToString("hh:mm tt");
                        String CheckOut = fortransaction.CheckOutDate.ToString("MM/dd/yyyy") + " " + exp2.ToString("hh:mm tt");
                        //MessageBox.Show("Checked In");
                        String CheckOutDateString = fortransaction.CheckOutDate.ToString("MM/dd/yyyy") + " " + fortransaction.CheckOutTime;
                        if (DateTime.Today <= DateTime.Parse(CheckOutDateString))
                        {
                            if (fortransaction.Status == "CheckedIn")
                            {
                                //if (DateTime.Now < DateTime.Parse(fortransaction.CheckOutTime))
                                //{
                                apt.Start = DateTime.Parse(CheckIn);
                                apt.End = DateTime.Parse(CheckOut);
                                apt.Subject = fortransaction.Status;
                                apt.Location = "Room " + roomCheckedIn.RoomNumber;
                                if (timeline == true && elsemonths == false)
                                {
                                    apt.ResourceId = fortransaction.RoomId;
                                }
                                apt.Description = fortransaction.RoomId.ToString();

                                //apt.RecurrenceInfo.Type = RecurrenceType.Hourly;
                                apt.RecurrenceInfo.Start = apt.Start;
                                //apt.RecurrenceInfo.Periodicity = 1;
                                apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                                apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start;
                                apt.LabelId = 3;
                                //apt.RecurrencePattern.LabelId = 2;

                                //apt.Reminders.Add(reminder);
                                schedulerStorage.AppointmentStorage.Add(apt);
                                //}
                            }
                        }
                    }
                }
                else if (cmbStatus.Text == "Reserved" && cmbRoomNumber.Text == "All")
                {
                    var countreservationAdd = context.Reservations.ToList();
                    foreach (var forreservation in countreservationAdd)
                    {
                        var apt = schedulerStorage.CreateAppointment(AppointmentType.Pattern);
                        var reminder = apt.CreateNewReminder();
                        var roomreservation = context.Rooms.FirstOrDefault(c => c.RoomId == forreservation.RoomId);

                        apt.Start = forreservation.RequestDate;
                        apt.End = forreservation.RequestDate;
                        apt.Subject = "Reserved";
                        apt.Location = "Room " + roomreservation.RoomNumber;
                        if (timeline == true && elsemonths == false)
                        {
                            apt.ResourceId = forreservation.RoomId;
                        }
                        apt.Description = forreservation.RoomId.ToString();

                        //apt.RecurrenceInfo.Type = RecurrenceType.Hourly;
                        apt.RecurrenceInfo.Start = apt.Start;
                        //apt.RecurrenceInfo.Periodicity = 1;
                        apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                        apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start;
                        apt.LabelId = 2;
                        //apt.RecurrencePattern.LabelId = 2;

                        //apt.Reminders.Add(reminder);
                        schedulerStorage.AppointmentStorage.Add(apt);
                    }
                }
                else if (cmbStatus.Text == "Critical" && cmbRoomNumber.Text == "All")
                {
                    var counttransactionAdd = context.Transactions.ToList();

                    foreach (var fortransaction in counttransactionAdd)
                    {
                        var room3 = context.Rooms.FirstOrDefault(c => c.RoomNumber == cmbRoomNumber.Text);
                        var apt = schedulerStorage.CreateAppointment(AppointmentType.Pattern);
                        var reminder = apt.CreateNewReminder();

                        var transactionSelect = context.Transactions.Select(c => c.ReservationId).FirstOrDefault();
                        var room = context.Rooms.FirstOrDefault(c => c.RoomId == fortransaction.RoomId);

                        DateTime exp = DateTime.Parse(fortransaction.CheckInTime);
                        DateTime exp2 = DateTime.Parse(fortransaction.CheckOutTime);

                        String CheckIn = fortransaction.CheckInDate.ToString("MM/dd/yyyy") + " " + exp.ToString("hh:mm tt");
                        String CheckOut = fortransaction.CheckOutDate.ToString("MM/dd/yyyy") + " " + exp2.ToString("hh:mm tt");

                        String CheckOutDateString = fortransaction.CheckOutDate.ToString("MM/dd/yyyy") + " " + fortransaction.CheckOutTime;

                        if (DateTime.Today > DateTime.Parse(CheckOutDateString))
                        {
                            apt.Start = DateTime.Parse(CheckIn);
                            apt.End = DateTime.Parse(CheckOut);
                            apt.Subject = "Critical";
                            apt.Location = "Room " + room.RoomNumber;
                            if (timeline == true && elsemonths == false)
                            {
                                apt.ResourceId = fortransaction.RoomId;
                            }
                            apt.Description = fortransaction.RoomId.ToString();

                            //apt.RecurrenceInfo.Type = RecurrenceType.Hourly;
                            apt.RecurrenceInfo.Start = apt.Start;
                            //apt.RecurrenceInfo.Periodicity = 1;
                            apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                            apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start;
                            apt.LabelId = 1;
                            //apt.RecurrencePattern.LabelId = 2;

                            //apt.Reminders.Add(reminder);
                            schedulerStorage.AppointmentStorage.Add(apt);
                        }

                    }
                }
                else if (cmbStatus.Text == "Checked In")
                {
                    var counttransaction = context.Transactions.ToList();
                    foreach (var fortransaction in counttransaction)
                    {
                        var apt = schedulerStorage.CreateAppointment(AppointmentType.Pattern);
                        var reminder = apt.CreateNewReminder();
                        var room3 = context.Rooms.FirstOrDefault(c => c.RoomNumber == cmbRoomNumber.Text);

                        var transactionSelect = context.Transactions.Select(c => c.ReservationId).FirstOrDefault();
                        var roomCheckedIn = context.Rooms.FirstOrDefault(c => c.RoomId == fortransaction.RoomId);

                        DateTime exp = DateTime.Parse(fortransaction.CheckInTime);
                        DateTime exp2 = DateTime.Parse(fortransaction.CheckOutTime);

                        String CheckIn = fortransaction.CheckInDate.ToString("MM/dd/yyyy") + " " + exp.ToString("hh:mm tt");
                        String CheckOut = fortransaction.CheckOutDate.ToString("MM/dd/yyyy") + " " + exp2.ToString("hh:mm tt");
                        //MessageBox.Show("Checked In");
                        String CheckOutDateString = fortransaction.CheckOutDate.ToString("MM/dd/yyyy") + " " + fortransaction.CheckOutTime;
                        if (fortransaction.RoomId == room3.RoomId)
                        {

                            if (DateTime.Today <= DateTime.Parse(CheckOutDateString))
                            {
                                if (fortransaction.Status == "CheckedIn")
                                {
                                apt.Start = DateTime.Parse(CheckIn);
                                apt.End = DateTime.Parse(CheckOut);
                                apt.Subject = fortransaction.Status;
                                apt.Location = "Room " + roomCheckedIn.RoomNumber;
                                if (timeline == true && elsemonths == false)
                                {
                                    apt.ResourceId = fortransaction.RoomId;
                                }
                                apt.Description = fortransaction.RoomId.ToString();

                                //apt.RecurrenceInfo.Type = RecurrenceType.Hourly;
                                apt.RecurrenceInfo.Start = apt.Start;
                                //apt.RecurrenceInfo.Periodicity = 1;
                                apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                                apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start;
                                apt.LabelId = 3;
                                //apt.RecurrencePattern.LabelId = 2;

                                //apt.Reminders.Add(reminder);
                                schedulerStorage.AppointmentStorage.Add(apt);
                           
                                }
                            }
                        }

                    }
                }
                else if (cmbStatus.Text == "Reserved")
                {
                    var countreservationAdd = context.Reservations.ToList();

                    foreach (var forreservation in countreservationAdd)
                    {
                        var room3 = context.Rooms.FirstOrDefault(c => c.RoomNumber == cmbRoomNumber.Text);

                        if (forreservation.RoomId == room3.RoomId)
                        {
                            var apt = schedulerStorage.CreateAppointment(AppointmentType.Pattern);
                            var reminder = apt.CreateNewReminder();
                            var roomreservation = context.Rooms.FirstOrDefault(c => c.RoomId == forreservation.RoomId);

                            apt.Start = forreservation.RequestDate;
                            apt.End = forreservation.RequestDate;
                            apt.Subject = "Reserved";
                            apt.Location = "Room " + roomreservation.RoomNumber;
                            if (timeline == true && elsemonths == false)
                            {
                                apt.ResourceId = forreservation.RoomId;
                            }
                            apt.Description = forreservation.RoomId.ToString();

                            //apt.RecurrenceInfo.Type = RecurrenceType.Hourly;
                            apt.RecurrenceInfo.Start = apt.Start;
                            //apt.RecurrenceInfo.Periodicity = 1;
                            apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                            apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start;
                            apt.LabelId = 2;
                            //apt.RecurrencePattern.LabelId = 2;

                            //apt.Reminders.Add(reminder);
                            schedulerStorage.AppointmentStorage.Add(apt);
                        }
                    }
                }
                else if (cmbStatus.Text == "Critical")
                {
                    var counttransactionAdd = context.Transactions.ToList();

                    foreach (var fortransaction in counttransactionAdd)
                    {
                        var room3 = context.Rooms.FirstOrDefault(c => c.RoomNumber == cmbRoomNumber.Text);
                        var apt = schedulerStorage.CreateAppointment(AppointmentType.Pattern);
                        var reminder = apt.CreateNewReminder();

                        var transactionSelect = context.Transactions.Select(c => c.ReservationId).FirstOrDefault();
                        var room = context.Rooms.FirstOrDefault(c => c.RoomId == fortransaction.RoomId);

                        DateTime exp = DateTime.Parse(fortransaction.CheckInTime);
                        DateTime exp2 = DateTime.Parse(fortransaction.CheckOutTime);

                        String CheckIn = fortransaction.CheckInDate.ToString("MM/dd/yyyy") + " " + exp.ToString("hh:mm tt");
                        String CheckOut = fortransaction.CheckOutDate.ToString("MM/dd/yyyy") + " " + exp2.ToString("hh:mm tt");

                        String CheckOutDateString = fortransaction.CheckOutDate.ToString("MM/dd/yyyy") + " " + fortransaction.CheckOutTime;
                        if (fortransaction.RoomId == room3.RoomId)
                        {
                            if (DateTime.Today > DateTime.Parse(CheckOutDateString))
                            {
                                apt.Start = DateTime.Parse(CheckIn);
                                apt.End = DateTime.Parse(CheckOut);
                                apt.Subject = "Critical";
                                apt.Location = "Room " + room.RoomNumber;
                                if (timeline == true && elsemonths == false)
                                {
                                    apt.ResourceId = fortransaction.RoomId;
                                }
                                apt.Description = fortransaction.RoomId.ToString();

                                //apt.RecurrenceInfo.Type = RecurrenceType.Hourly;
                                apt.RecurrenceInfo.Start = apt.Start;
                                //apt.RecurrenceInfo.Periodicity = 1;
                                apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                                apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start;
                                apt.LabelId = 1;
                                //apt.RecurrencePattern.LabelId = 2;

                                //apt.Reminders.Add(reminder);
                                schedulerStorage.AppointmentStorage.Add(apt);

                            }

                        }
                    }
                }

            }
        }

        
        public void AllStatus()
        {
            using (var context = new DatabaseContext())
            {
                var counttransactionAdd = context.Transactions.ToList();

                foreach (var fortransaction in counttransactionAdd)
                {
                    var room3 = context.Rooms.FirstOrDefault(c => c.RoomNumber == cmbRoomNumber.Text);

                    if (fortransaction.RoomId == room3.RoomId)
                    {
                        var schedulerStorage = schedulerControl1.Storage;
                        schedulerStorage.AppointmentStorage.Clear();
                        var apt = schedulerStorage.CreateAppointment(AppointmentType.Pattern);
                        var reminder = apt.CreateNewReminder();

                        var transactionSelect = context.Transactions.Select(c => c.ReservationId).FirstOrDefault();
                        var room = context.Rooms.FirstOrDefault(c => c.RoomId == fortransaction.RoomId);

                        DateTime exp = DateTime.Parse(fortransaction.CheckInTime);
                        DateTime exp2 = DateTime.Parse(fortransaction.CheckOutTime);

                        String CheckIn = fortransaction.CheckInDate.ToString("MM/dd/yyyy") + " " + exp.ToString("hh:mm tt");
                        String CheckOut = fortransaction.CheckOutDate.ToString("MM/dd/yyyy") + " " + exp2.ToString("hh:mm tt");

                        if (DateTime.Today == fortransaction.CheckOutDate)
                        {
                            if (DateTime.Now < DateTime.Parse(fortransaction.CheckOutTime))
                            {
                                if (fortransaction.Status == "CheckedIn")
                                {
                                    apt.Start = DateTime.Parse(CheckIn);
                                    apt.End = DateTime.Parse(CheckOut);
                                    apt.Subject = fortransaction.Status;
                                    apt.Location = "Room " + room.RoomNumber;
                                    if (timeline == true && elsemonths == false)
                                    {
                                        apt.ResourceId = fortransaction.RoomId;
                                    }
                                    apt.Description = fortransaction.RoomId.ToString();

                                    //apt.RecurrenceInfo.Type = RecurrenceType.Hourly;
                                    apt.RecurrenceInfo.Start = apt.Start;
                                    //apt.RecurrenceInfo.Periodicity = 1;
                                    apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                                    apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start;
                                    apt.LabelId = 3;
                                    //apt.RecurrencePattern.LabelId = 2;

                                    //apt.Reminders.Add(reminder);
                                    schedulerStorage.AppointmentStorage.Add(apt);

                                }
                            }
                            else if (DateTime.Now >= DateTime.Parse(fortransaction.CheckOutTime))
                            {
                                if (fortransaction.Status == "CheckedIn")
                                {
                                    apt.Start = DateTime.Parse(CheckIn);
                                    apt.End = DateTime.Parse(CheckOut);
                                    apt.Subject = "Critical";
                                    apt.Location = "Room " + room.RoomNumber;
                                    if (timeline == true && elsemonths == false)
                                    {
                                        apt.ResourceId = fortransaction.RoomId;
                                    }
                                    apt.Description = fortransaction.RoomId.ToString();

                                    //apt.RecurrenceInfo.Type = RecurrenceType.Hourly;
                                    apt.RecurrenceInfo.Start = apt.Start;
                                    //apt.RecurrenceInfo.Periodicity = 1;
                                    apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                                    apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start;
                                    apt.LabelId = 1;
                                    //apt.RecurrencePattern.LabelId = 2;

                                    //apt.Reminders.Add(reminder);
                                    schedulerStorage.AppointmentStorage.Add(apt);
                                }
                            }
                        }
                        else if (DateTime.Today > fortransaction.CheckOutDate)
                        {
                            if (fortransaction.Status == "CheckedIn")
                            {
                                apt.Start = DateTime.Parse(CheckIn);
                                apt.End = DateTime.Parse(CheckOut);
                                apt.Subject = "Critical";
                                apt.Location = "Room " + room.RoomNumber;
                                if (timeline == true && elsemonths == false)
                                {
                                    apt.ResourceId = fortransaction.RoomId;
                                }
                                apt.Description = fortransaction.RoomId.ToString();

                                //apt.RecurrenceInfo.Type = RecurrenceType.Hourly;
                                apt.RecurrenceInfo.Start = apt.Start;
                                //apt.RecurrenceInfo.Periodicity = 1;
                                apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                                apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start;
                                apt.LabelId = 1;
                                //apt.RecurrencePattern.LabelId = 2;

                                //apt.Reminders.Add(reminder);
                                schedulerStorage.AppointmentStorage.Add(apt);
                            }

                        }
                        else if (DateTime.Today < fortransaction.CheckOutDate)
                        {

                            if (fortransaction.Status == "CheckedIn")
                            {
                                apt.Start = DateTime.Parse(CheckIn);
                                apt.End = DateTime.Parse(CheckOut);
                                apt.Subject = fortransaction.Status;
                                apt.Location = "Room " + room.RoomNumber;
                                apt.ResourceId = fortransaction.RoomId;
                                apt.Description = fortransaction.RoomId.ToString();

                                //apt.RecurrenceInfo.Type = RecurrenceType.Hourly;
                                apt.RecurrenceInfo.Start = apt.Start;
                                //apt.RecurrenceInfo.Periodicity = 1;
                                apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                                apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start;
                                apt.LabelId = 3;
                                //apt.RecurrencePattern.LabelId = 2;

                                //apt.Reminders.Add(reminder);
                                schedulerStorage.AppointmentStorage.Add(apt);

                            }
                        }
                    }
                }
                var countreservationAdd = context.Reservations.ToList();
                foreach (var forreservation in countreservationAdd)
                {
                    var schedulerStorage = schedulerControl1.Storage;
                    var apt = schedulerStorage.CreateAppointment(AppointmentType.Pattern);
                    var reminder = apt.CreateNewReminder();
                    var roomreservation = context.Rooms.FirstOrDefault(c => c.RoomId == forreservation.RoomId);
                    var room3 = context.Rooms.FirstOrDefault(c => c.RoomNumber == cmbRoomNumber.Text);

                    if (forreservation.RoomId == room3.RoomId)
                    {

                        apt.Start = forreservation.RequestDate;
                        apt.End = forreservation.RequestDate;
                        apt.Subject = "Reserved";
                        apt.Location = "Room " + roomreservation.RoomNumber;
                        if (timeline == true && elsemonths == false)
                        {
                            apt.ResourceId = forreservation.RoomId;
                        }
                        apt.Description = forreservation.RoomId.ToString();

                        //apt.RecurrenceInfo.Type = RecurrenceType.Hourly;
                        apt.RecurrenceInfo.Start = apt.Start;
                        //apt.RecurrenceInfo.Periodicity = 1;
                        apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                        apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start;
                        apt.LabelId = 2;
                        //apt.RecurrencePattern.LabelId = 2;

                        //apt.Reminders.Add(reminder);
                        schedulerStorage.AppointmentStorage.Add(apt);
                    }
                }
            }
        }




        public void AddAppointments()
        {
            using (var context = new DatabaseContext())
            {
                var counttransactionAdd = context.Transactions.ToList();

                foreach (var fortransaction in counttransactionAdd)
                {
                    var schedulerStorage = schedulerControl1.Storage;
                    var apt = schedulerStorage.CreateAppointment(AppointmentType.Pattern);
                    var reminder = apt.CreateNewReminder();

                    var transactionSelect = context.Transactions.Select(c => c.ReservationId).FirstOrDefault();
                    var room = context.Rooms.FirstOrDefault(c => c.RoomId == fortransaction.RoomId);

                    DateTime exp = DateTime.Parse(fortransaction.CheckInTime);
                    DateTime exp2 = DateTime.Parse(fortransaction.CheckOutTime);

                    String CheckIn = fortransaction.CheckInDate.ToString("MM/dd/yyyy") + " " + exp.ToString("hh:mm tt");
                    String CheckOut = fortransaction.CheckOutDate.ToString("MM/dd/yyyy") + " " + exp2.ToString("hh:mm tt");

                    if (DateTime.Today == fortransaction.CheckOutDate)
                    {
                        if (DateTime.Now < DateTime.Parse(fortransaction.CheckOutTime))
                        {
                            if (fortransaction.Status == "CheckedIn")
                            {
                                apt.Start = DateTime.Parse(CheckIn);
                                apt.End = DateTime.Parse(CheckOut);
                                apt.Subject = fortransaction.Status;
                                apt.Location = "Room " + room.RoomNumber;
                                if (timeline == true && elsemonths == false)
                                {
                                    apt.ResourceId = fortransaction.RoomId;
                                }
                                apt.Description = fortransaction.RoomId.ToString();

                                //apt.RecurrenceInfo.Type = RecurrenceType.Hourly;
                                apt.RecurrenceInfo.Start = apt.Start;
                                //apt.RecurrenceInfo.Periodicity = 1;
                                apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                                apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start;
                                apt.LabelId = 3;
                                //apt.RecurrencePattern.LabelId = 2;

                                //apt.Reminders.Add(reminder);
                                schedulerStorage.AppointmentStorage.Add(apt);
                            }
                        }
                        else if (DateTime.Now >= DateTime.Parse(fortransaction.CheckOutTime))
                        {
                            if (fortransaction.Status == "Critical")
                            {
                                apt.Start = DateTime.Parse(CheckIn);
                                apt.End = DateTime.Parse(CheckOut);
                                apt.Subject = "Critical";
                                apt.Location = "Room " + room.RoomNumber;
                                if (timeline == true && elsemonths == false)
                                {
                                    apt.ResourceId = fortransaction.RoomId;
                                }
                                apt.Description = fortransaction.RoomId.ToString();

                                //apt.RecurrenceInfo.Type = RecurrenceType.Hourly;
                                apt.RecurrenceInfo.Start = apt.Start;
                                //apt.RecurrenceInfo.Periodicity = 1;
                                apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                                apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start;
                                apt.LabelId = 1;
                                //apt.RecurrencePattern.LabelId = 2;

                                //apt.Reminders.Add(reminder);
                                schedulerStorage.AppointmentStorage.Add(apt);
                            }
                        }
                    }
                    else if (DateTime.Today > fortransaction.CheckOutDate)
                    {
                        if (fortransaction.Status == "Critical")
                        {
                            apt.Start = DateTime.Parse(CheckIn);
                            apt.End = DateTime.Parse(CheckOut);
                            apt.Subject = "Critical";
                            apt.Location = "Room " + room.RoomNumber;
                            if (timeline == true && elsemonths == false)
                            {
                                apt.ResourceId = fortransaction.RoomId;
                            }
                            apt.Description = fortransaction.RoomId.ToString();

                            //apt.RecurrenceInfo.Type = RecurrenceType.Hourly;
                            apt.RecurrenceInfo.Start = apt.Start;
                            //apt.RecurrenceInfo.Periodicity = 1;
                            apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                            apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start;
                            apt.LabelId = 1;
                            //apt.RecurrencePattern.LabelId = 2;

                            //apt.Reminders.Add(reminder);
                            schedulerStorage.AppointmentStorage.Add(apt);

                        }
                    }
                    else if (DateTime.Today < fortransaction.CheckOutDate)
                    {
                        if (fortransaction.Status == "CheckedIn")
                        {

                            apt.Start = DateTime.Parse(CheckIn);
                            apt.End = DateTime.Parse(CheckOut);
                            apt.Subject = fortransaction.Status;
                            apt.Location = "Room " + room.RoomNumber;
                            if (timeline == true && elsemonths == false)
                            {
                                apt.ResourceId = fortransaction.RoomId;
                            }
                            apt.Description = fortransaction.RoomId.ToString();

                            //apt.RecurrenceInfo.Type = RecurrenceType.Hourly;
                            apt.RecurrenceInfo.Start = apt.Start;
                            //apt.RecurrenceInfo.Periodicity = 1;
                            apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                            apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start;
                            apt.LabelId = 3;
                            //apt.RecurrencePattern.LabelId = 2;

                            //apt.Reminders.Add(reminder);
                            schedulerStorage.AppointmentStorage.Add(apt);

                        }
                    }
                }

                var countreservationAdd = context.Reservations.ToList();
                //MessageBox.Show("Add Transaction: " + counttransactionAdd.Count().ToString());
                //MessageBox.Show("Add Reservation: " + countreservationAdd.Count().ToString());
                foreach (var forreservation in countreservationAdd)
                {
                    var schedulerStorage = schedulerControl1.Storage;
                    var apt = schedulerStorage.CreateAppointment(AppointmentType.Pattern);
                    var reminder = apt.CreateNewReminder();

                    var room = context.Rooms.FirstOrDefault(c => c.RoomId == forreservation.RoomId);

                    apt.Start = forreservation.RequestDate;
                    apt.End = forreservation.RequestDate;
                    apt.Subject = "Reserved";
                    apt.Location = "Room " + room.RoomNumber;
                    if (timeline == true && elsemonths == false)
                    {
                        apt.ResourceId = forreservation.RoomId;
                    }
                    apt.Description = forreservation.RoomId.ToString();

                    //apt.RecurrenceInfo.Type = RecurrenceType.Hourly;
                    apt.RecurrenceInfo.Start = apt.Start;
                    //apt.RecurrenceInfo.Periodicity = 1;
                    apt.RecurrenceInfo.Range = RecurrenceRange.EndByDate;
                    apt.RecurrenceInfo.End = apt.RecurrenceInfo.Start;
                    apt.LabelId = 2;
                    //apt.RecurrencePattern.LabelId = 2;

                    //apt.Reminders.Add(reminder);
                    schedulerStorage.AppointmentStorage.Add(apt);

                }
            }
        }

        private void schedulerControl1_SelectionChanged(object sender, EventArgs e)
        {
            using (var context = new DatabaseContext())
            {

                    var selectedAppointments = schedulerControl1.SelectedAppointments.FirstOrDefault();
                    var selectexperiment = schedulerControl1.SelectedAppointments.Count();

                   if(selectexperiment <= 0){
                       //MessageBox.Show("Wala");
                   }
                   else if (selectexperiment > 0)
                   {
                       int roomId = Int32.Parse(selectedAppointments.Description);
                       var room = context.Rooms.FirstOrDefault(c => c.RoomId == roomId);
                       var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == room.RoomTypeId);
                       //int roomEquipmentId = Int32.Parse(room.RoomEquipmentId);
                       //var roomequipment = context.RoomEquipments.FirstOrDefault(c => c.RoomEquipmentId == roomEquipmentId);
                       var transaction = context.Transactions.FirstOrDefault(c => c.RoomId == room.RoomId);
                       var reservation = context.Reservations.FirstOrDefault(c => c.RoomId == room.RoomId);
                       if (selectedAppointments.Subject != null)
                       {

                           //lblEquipments.Text = roomequipment.Equipment;

                           if (selectedAppointments.Subject == "CheckedIn")
                           {

                               stkCheckIn.Visibility = System.Windows.Visibility.Visible;
                               stkReserved.Visibility = System.Windows.Visibility.Hidden;

                               lblStatus.Text = "Checked In";

                               lblRoomNumber.Text = room.RoomNumber;
                               lblRoomType.Text = roomtype.RoomTypeName;
                               lblDescription.Text = room.RoomDescription;
                               lblGuestNumber.Text = transaction.GuestNumber.ToString();
                               lblRoomSlipNumber.Text = transaction.RoomSlipNumber.ToString();
                               lblCheckInDate.Text = transaction.CheckInDate.ToString();
                               lblCheckInTime.Text = transaction.CheckInTime;
                               //lblDesiredTime.Text = transaction
                               //lblTimeType.Text = transaction
                               lblCheckOutDate.Text = transaction.CheckOutDate.ToString();
                               lblCheckOutTime.Text = transaction.CheckOutTime;
                               //lblRoomBoy.Text = transaction.StaffId


                           }
                           else if (selectedAppointments.Subject == "Reserved")
                           {

                               stkReserved.Visibility = System.Windows.Visibility.Visible;
                               stkCheckIn.Visibility = System.Windows.Visibility.Hidden;

                               lblStatus1.Text = "Reserved";

                               lblRoomNumber1.Text = room.RoomNumber;
                               lblRoomType1.Text = roomtype.RoomTypeName;
                               lblDescription1.Text = room.RoomDescription;
                               lblReservationNumber.Text = reservation.ReservationNumber;
                               lblReservationFee.Text = reservation.ReservationFee.ToString();
                               lblCustomer.Text = reservation.CustomerName;
                               lblDateReserved.Text = reservation.DateReserved.ToString();
                               lblRequestDate.Text = reservation.RequestDate.ToString();
                               lblRequestTime.Text = reservation.RequestTime;
                               lblCardNumber.Text = reservation.CardNumber;
                               
                           }

                       }
                   }

            }
        }

        private void schedulerControl1_EditAppointmentFormShowing(object sender, DevExpress.Xpf.Scheduler.EditAppointmentFormEventArgs e)
        {
            String date = schedulerControl1.SelectedInterval.Start.Month.ToString() + "/" + schedulerControl1.SelectedInterval.Start.Day.ToString() + "/" + schedulerControl1.SelectedInterval.Start.Year.ToString();

            var window = new Booking.Windows.ReservationWindow(selectedId);
            window.dtRequestDate.Text = date;
            MethodsClass.ShowPropertyWindow(window);
        }

        private void btnIncreaseResource_Click(object sender, RoutedEventArgs e)
        {
            SchedulerViewBase view = schedulerControl1.ActiveView;
            var schedulerStorage = schedulerControl1.Storage;
            var res = schedulerStorage.ResourceStorage.Items.Count();
            
       
            if (view.ResourcesPerPage == res)
            {
                MethodsClass.ShowNotification("Increase Limit Reached.");
            }
            else
            {
                view.ResourcesPerPage++;
            }
        }

        private void btnDecreaseResource_Click(object sender, RoutedEventArgs e)
        {
            SchedulerViewBase view = schedulerControl1.ActiveView;
            if (view.ResourcesPerPage == 1)
            {
                MethodsClass.ShowNotification("Decrease Limit Reached.");
            }
            else
            {
                view.ResourcesPerPage--;
            }
        }

        private void btnPreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            int answer = DateTime.Today.Day - DateTime.Today.Day + 1;
            String StartMonthString = DateTime.Today.Month.ToString() + "/" + answer.ToString() + "/" + DateTime.Today.Year.ToString();
            DateTime StartMonth = DateTime.Parse(StartMonthString);

            schedulerControl1.Start = StartMonth.AddMonths(nextpage--); 
        }

        private void btnNextMonth_Click(object sender, RoutedEventArgs e)
        {
            int answer = DateTime.Today.Day - DateTime.Today.Day + 1;
            String StartMonthString = DateTime.Today.Month.ToString() + "/" + answer.ToString() + "/" + DateTime.Today.Year.ToString();
            DateTime StartMonth = DateTime.Parse(StartMonthString);

            schedulerControl1.Start = StartMonth.AddMonths(nextpage++); 
        }
    }

}