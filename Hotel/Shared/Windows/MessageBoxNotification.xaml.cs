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
using System.Windows.Threading;

namespace Hotel.Shared.Windows
{
    /// <summary>
    /// Interaction logic for MessageBoxNotification.xaml
    /// </summary>
    public partial class MessageBoxNotification : Window
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        //int tick = 0;
        //string message = "";
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public MessageBoxNotification()
        {
            InitializeComponent();
        }

        public MessageBoxNotification(string message)
        {
            InitializeComponent();
            this.blkMessage.Text = message;
        }

        //private void dispatcherTimer_Tick(object sender, EventArgs e)
        //{
        //    tick++;
        //    if (tick == 2)
        //    {
        //        this.Close();
        //    }
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            //dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            //dispatcherTimer.Start();

            //txtWelcome.Text = message;
            #region animation onLoading
            double screenHeight = Application.Current.MainWindow.Height;
            if (screenTopEdge > 0 || screenTopEdge < -8) { screenHeight += screenTopEdge; }
            DoubleAnimation animation = new DoubleAnimation(0, 92, (Duration)TimeSpan.FromSeconds(0.3));
            this.BeginAnimation(Window.HeightProperty, animation);
            #endregion
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dispatcherTimer.Stop();

            #region animation onClosing
            double screenHeight = Application.Current.MainWindow.Height;
            if (screenTopEdge > 0 || screenTopEdge < -8) { screenHeight += screenTopEdge; }
            Closing -= Window_Closing;
            e.Cancel = true;
            var anim = new DoubleAnimation(92, 0, (Duration)TimeSpan.FromSeconds(0.3));
            anim.Completed += (s, _) =>
            {
                this.Close();
                dispatcherTimer.Stop();
            };
            this.BeginAnimation(Window.HeightProperty, anim);
            #endregion
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //for emergency out
            if (e.Key == Key.Escape)
            {
                Variables.emergencyOut = true;
                this.Close();
            }
        }
    }
}
