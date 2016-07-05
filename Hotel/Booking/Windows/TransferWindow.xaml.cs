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
    /// Interaction logic for TransferWindow.xaml
    /// </summary>
    public partial class TransferWindow : Window
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        int selectedId = 0;
 

        public TransferWindow()
        {
            InitializeComponent();
        }

        public TransferWindow(int id)
        {
            InitializeComponent();
            this.selectedId = id;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
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
                List<int> rooms = context.Rooms.Where(c => c.Status == "Available").Select(c => c.RoomId).ToList();
                txtTransfer.ItemsSource = rooms;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int num = Int32.Parse(txtTransfer.Text);
            using (var context = new DatabaseContext())
            {
                var room = context.Rooms.FirstOrDefault(c => c.RoomId == num);
                var rooms = context.Rooms.FirstOrDefault(c => c.RoomId == selectedId);
                var tran = context.Transactions.FirstOrDefault(c => c.RoomId == selectedId);
                     rooms.Status = "Available";
                room.Status = "Occupied";
                    tran.RoomId = Int32.Parse(txtTransfer.Text);
         
                MethodsClass.ShowNotification("Record successfully updated!");
                context.SaveChanges();
                this.Close();
            }
        }
    }
}
