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

namespace Hotel.MasterData.Windows
{
    /// <summary>
    /// Interaction logic for StaffWindow.xaml
    /// </summary>
    public partial class StaffPositionWindow : Window
    {
        int selectedid = 0;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        public StaffPositionWindow()
        {
            InitializeComponent();
        }
        public StaffPositionWindow(int id)
        {
            InitializeComponent();
            this.selectedid = id;

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
            Refresh();
        }
        public void Refresh()
        {
            using (var context = new DatabaseContext())
            {
                var positionid = context.StaffPositions.Count(c => c.StaffPositionId == selectedid);
                if (positionid > 0)
                {
                    var staffposition = context.StaffPositions.FirstOrDefault(c => c.StaffPositionId == selectedid);
                    txtStaffPosition.Text = staffposition.StaffPositionName;
                    if (staffposition.Assist == true)
                    {
                        chkAssist.IsChecked = true;
                    }
                    else
                    {
                        chkAssist.IsChecked = false;
                    }
                }
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var duplicates = context.StaffPositions.Where(c => c.StaffPositionName.Contains(txtStaffPosition.Text)).ToList();
                var editpos = context.StaffPositions.Count(c => c.StaffPositionId == selectedid);
                if (editpos > 0)
                {
                    var newposition = context.StaffPositions.FirstOrDefault(c => c.StaffPositionId == selectedid);
                    newposition.StaffPositionName = txtStaffPosition.Text;
                    if (chkAssist.IsChecked == true)
                    {
                        newposition.Assist = true;
                    }
                    else
                    {
                        newposition.Assist = false;
                    }
                    context.SaveChanges();
                    MethodsClass.ShowNotification("Successfully Updated!");
                    this.Close();
                }
                else if (duplicates.Count() > 0)
                {
                    MethodsClass.ShowNotification("The Position already exists!");
                }
                else
                {
                    var staffposition = new StaffPosition();
                    staffposition.StaffPositionName = txtStaffPosition.Text;
                    if (chkAssist.IsChecked == false)
                    {
                        staffposition.Assist = false;
                    }
                    else if (chkAssist.IsChecked == true)
                    {
                        staffposition.Assist = true;
                    }
                    context.StaffPositions.Add(staffposition);
                    context.SaveChanges();
                    MethodsClass.ShowNotification("Successfully Added !");
                    this.Close();

                }
            }
        }


    }
}

