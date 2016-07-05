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
    public partial class StaffWindow : Window
    {
        int SelectedId = 0;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        public StaffWindow()
        {
            InitializeComponent();
        }

        public StaffWindow(int id)
        {
            InitializeComponent();
            this.SelectedId = id;
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
                if (SelectedId > 0)
                {
                    var staff = context.Staffs.FirstOrDefault(c => c.StaffId == SelectedId);
                    txtStaffName.Text = staff.StaffName;
                    txtStaffPosition.Text = staff.StaffPosition;
                    
                }
            }

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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedId > 0)
            {
                using (var context = new DatabaseContext())
                {
                    var duplicates = context.Staffs.Where(c => c.StaffName.ToLower().Contains(txtStaffName.Text.ToLower()) && c.StaffPosition.ToLower().Contains(txtStaffPosition.Text.ToLower())).ToList();
                    if (txtStaffName.Text != "" && txtStaffPosition.Text != "")
                    {
                        if (duplicates.Count() > 0)
                        {
                            MethodsClass.ShowNotification("This name already exists!");
                        }
                        else
                        {
                            var staf = context.Staffs.FirstOrDefault(c => c.StaffId == SelectedId);
                            staf.StaffName = txtStaffName.Text;
                            staf.StaffPosition = txtStaffPosition.Text;
                            MethodsClass.ShowNotification("Successfully Updated Record!");
                            context.SaveChanges();
                            this.Close();
                        }
                    }
                }
            }

            else
            {
                using (var context = new DatabaseContext())
                {
                    var duplicates = context.Staffs.Where(c => c.StaffName.ToLower().Contains(txtStaffName.Text.ToLower()) && c.StaffPosition.ToLower().Contains(txtStaffPosition.Text.ToLower())).ToList();
                    if (duplicates.Count() > 0)
                    {
                        MethodsClass.ShowNotification("This name already exists!");
                    }
                    else
                    {
                        var staff = new Staff();
                        staff.StaffName = txtStaffName.Text;
                        staff.StaffPosition = txtStaffPosition.Text;
                        context.Staffs.Add(staff);
                        context.SaveChanges();
                        MethodsClass.ShowNotification("Successfully Added !");
                        this.Close();
                    }
                }
            }
            }

    }
}
           
