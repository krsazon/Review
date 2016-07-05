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
    /// Interaction logic for ReservationListWindow.xaml
    /// </summary>
    public partial class ReservationListWindow : Window
    {
        int selectedId = 0;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
          List<Reservation> Reservations = new List<Reservation>();
          public static string txt = "";

        public ReservationListWindow()
        {
            InitializeComponent();
        }

        public ReservationListWindow(int id)
        {
            InitializeComponent();
            this.selectedId = id;
        }

        public void Refresh()
        {
            using (var context = new DatabaseContext())
            {
                Reservations = context.Reservations.Where(c => c.RoomId == selectedId).ToList();
                if (txtSearch.Text != "")
                {


                }

                dgReservation.ItemsSource = null;
                dgReservation.ItemsSource = Reservations;
                viewReservation.BestFitColumns();
            }
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

       

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            var equipment = dgReservation.SelectedItem as Reservation;
            var window = new Windows.CheckInWindow();
            List<string> customer = new List<string>();
            foreach (Reservation c in dgReservation.SelectedItems)
            {
                customer.Add(c.CustomerName);
            }
            txt = string.Join(" ", customer);
            this.Close();
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgReservation_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
