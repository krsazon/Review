using DevExpress.Xpf.WindowsUI;
using Hotel.Models;
using Hotel.Page;
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

namespace Hotel.Shared.Windows
{
    /// <summary>
    /// Interaction logic for SystemParameterWindow.xaml
    /// </summary>
    public partial class SystemParameterWindow : Window
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        public SystemParameterWindow()
        {
            InitializeComponent();
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


                var parameters = context.Parameters.FirstOrDefault(c => c.ParameterId == c.ParameterId);
                txtName.Text = parameters.HotelName;
                txtAddress.Text = parameters.HotelAddress;
                txtDescription.Text = parameters.HotelDescription;
                if (parameters.TimePolicyEnabled == false)
                {
                    chTime.IsChecked = false;
                }
                else if (parameters.TimePolicyEnabled == true)
                {
                    chTime.IsChecked = true;
                    txtCheckInTime1.DateTime = DateTime.Parse(parameters.CheckInTimeStart);
                    txtCheckInTime2.DateTime = DateTime.Parse(parameters.CheckInTimeEnd);
                    txtCheckOutTime1.DateTime = DateTime.Parse(parameters.CheckOutTimeStart);
                    txtCheckOutTime2.DateTime = DateTime.Parse(parameters.CheckOutTimeEnd);
                }
                if (parameters.PetPolicyEnable == false)
                {
                    chPet.IsChecked = false;
                }
                else if (parameters.PetPolicyEnable == true)
                {
                    chPet.IsChecked = true;
                    txtPet.Text = parameters.PetRate.ToString();
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
            using (var context = new DatabaseContext())
            {
                var parameter = context.Parameters.FirstOrDefault(c => c.ParameterId == c.ParameterId);
                if (txtName.Text != "" && txtAddress.Text != "" && txtDescription.Text != "")
                {
                    parameter.HotelName = txtName.Text;
                    parameter.HotelAddress = txtAddress.Text;
                    parameter.HotelDescription = txtDescription.Text;

                    parameter.CheckInTimeStart = txtCheckInTime1.DateTime.ToString("hh:mm tt");
                    parameter.CheckInTimeEnd = txtCheckInTime2.DateTime.ToString("hh:mm tt");
                    parameter.CheckOutTimeStart = txtCheckOutTime1.DateTime.ToString("hh:mm tt");
                    parameter.CheckOutTimeEnd = txtCheckOutTime2.DateTime.ToString("hh:mm tt");
                    //parameter.PetRate = Int32.Parse(txtPet.Text);

                    if (chTime.IsChecked == true)
                    {
                        var time = "TRUE";

                        parameter.TimePolicyEnabled = bool.Parse(time);
                    }
                    else if (chTime.IsChecked == false)
                    {
                        var time = "FALSE";
                        parameter.TimePolicyEnabled = bool.Parse(time);
                    }

                    if (chPet.IsChecked == true)
                    {
                        var time = "TRUE";

                        parameter.PetPolicyEnable = bool.Parse(time);
                    }
                    else if (chPet.IsChecked == false)
                    {
                        var time = "FALSE";

                        parameter.PetPolicyEnable = bool.Parse(time);
                    }

                    MethodsClass.ShowNotification("Record successfully updated!");
                    context.SaveChanges();
                    this.Close();

                }
                else
                {
                    MethodsClass.ShowNotification("Please fill up the fields correctly.");
                }
            }
        }

        private void CheckEdit_Checked(object sender, RoutedEventArgs e)
        {
            txtCheckInTime1.IsEnabled = true;
            txtCheckInTime2.IsEnabled = true;
            txtCheckOutTime1.IsEnabled = true;
            txtCheckOutTime2.IsEnabled = true;
        }

        private void CheckEdit_Unchecked(object sender, RoutedEventArgs e)
        {
            txtCheckInTime1.IsEnabled = false;
            txtCheckInTime2.IsEnabled = false;
            txtCheckOutTime1.IsEnabled = false;
            txtCheckOutTime2.IsEnabled = false;
        }

        private void CheckEdit_Checked_1(object sender, RoutedEventArgs e)
        {
            txtPet.IsEnabled = true;
        }

        private void CheckEdit_Unchecked_1(object sender, RoutedEventArgs e)
        {
            txtPet.IsEnabled = false;
        }

        private void txtCheckInTime1_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {

        }
    }
}
