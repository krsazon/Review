using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hotel.Models
{
    public static class MethodsClass
    {
       
        public static void ShowPropertyWindow(Window window)
        {
            double screenLeftEdge = Application.Current.MainWindow.Left;
            double screenTopEdge = Application.Current.MainWindow.Top;
            double screenHeight = Application.Current.MainWindow.Height;
            double screenWidth = Application.Current.MainWindow.Width;

            window.Top = screenTopEdge + 93 + 8;
            window.Height = screenHeight - 93 - 35;
            window.Width = (screenWidth - 90) * 0.35;
                window.ShowDialog();
        }

        public static void ShowPropertyWindowPayment(Window window)
        {
            double screenLeftEdge = Application.Current.MainWindow.Left;
            double screenTopEdge = Application.Current.MainWindow.Top;
            double screenHeight = Application.Current.MainWindow.Height;
            double screenWidth = Application.Current.MainWindow.Width;

            window.Top = screenTopEdge + 93 + 8;
            window.Height = screenHeight - 93 - 35;
            window.Width = (screenWidth - 90) * 0.5;
            window.ShowDialog();
        }

        public static void ShowNotification(string message)
        {
            double screenLeftEdge = Application.Current.MainWindow.Left;
            double screenTopEdge = Application.Current.MainWindow.Top;
            double screenHeight = Application.Current.MainWindow.Height;
            double screenWidth = Application.Current.MainWindow.Width;

            var window = new Shared.Windows.MessageBoxNotification(message);
            window.Height = 0;
            window.Top = Application.Current.MainWindow.Top + 8;
            window.Left = (screenWidth / 2) - (window.Width / 2);
            if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
            window.ShowDialog();
        }

        public static bool ShowConfirmation(string message)
        {
            double screenLeftEdge = Application.Current.MainWindow.Left;
            double screenTopEdge = Application.Current.MainWindow.Top;
            double screenHeight = Application.Current.MainWindow.Height;
            double screenWidth = Application.Current.MainWindow.Width;

            var window = new Shared.Windows.MessageBoxYesNo(message);
            window.Height = 0;
            window.Top = Application.Current.MainWindow.Top + 8;
            window.Left = (screenWidth / 2) - (window.Width / 2);
            if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
            window.ShowDialog();

            return Shared.Windows.MessageBoxYesNo.isApproved;
        }
      
    }
}
