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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hotel.Booking.Windows
{
    /// <summary>
    /// Interaction logic for PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        int selectedId = 0;

        public PaymentWindow()
        {
            InitializeComponent();
        }

        public PaymentWindow(int id)
        {
            InitializeComponent();
            this.selectedId = id;
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
            
            using (var context = new DatabaseContext())
            {
                var room = context.Rooms.FirstOrDefault(c => c.RoomId == selectedId);
                var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == room.RoomTypeId);
                var transaction = context.Transactions.FirstOrDefault(c => c.RoomId == room.RoomId);
                var staff = context.Staffs.FirstOrDefault(c => c.StaffId == transaction.StaffId);
                var discount = context.Discounts.FirstOrDefault(c => c.DiscountId == transaction.DiscountId);
                spnRoomCharge.Text = transaction.RoomCharge.ToString();
                txtDiscountCardNumber.Text = transaction.DiscountCard.ToString();
          

                    List<String> discountname = context.Discounts.Select(c => c.DiscountName).ToList();
                    cmbApplyDiscount.ItemsSource = discountname;
                }           
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (MessageBox.Show("Confirm Payment?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var aa = new Transaction();
                    //var selected = dgRentList.SelectedItem as Rent;
                    var transaction = context.Transactions.FirstOrDefault(c => c.RoomId == selectedId);
                    var room = context.Rooms.FirstOrDefault(c => c.RoomId == transaction.RoomId);
                    transaction.CheckOutDate = DateTime.Now;
                    transaction.CheckOutTime = DateTime.Now.ToString("hh:mm tt");
                    room.Status = "Available";
                    transaction.Status = "Done";
                    transaction.AmountPaid = decimal.Parse(spnTotal.Text);
                    transaction.NetAmount = decimal.Parse(spnNetAmount.Text);
                    transaction.Change = decimal.Parse(spnChange.Text);

                    MethodsClass.ShowNotification("Successfully Checked Out!");
                    
                    context.SaveChanges();
                    this.Close();
            
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            cmbApplyDiscount.IsEnabled = true;
        }

        private void spnTotal_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            decimal total = decimal.Parse(spnNetAmount.Text);
            decimal cash = decimal.Parse(spnTotal.Text);
           
            if (cash < total)
            {
                spnChange.Text = "0";
            }
            else {
             decimal etona = cash - total;
            spnChange.Text = etona.ToString();
        }
        }

    }
}
