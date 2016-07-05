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
    /// Interaction logic for HolidayRateWindow.xaml
    /// </summary>
    public partial class HolidayRateWindow : Window
    {
        int SelectedId = 0;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        public HolidayRateWindow()
        {
            InitializeComponent();
        }

        public HolidayRateWindow(int id)
        {
            InitializeComponent();
            this.SelectedId = id;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtDate.Text = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();

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
            txtPercent.Visibility = Visibility.Hidden;  
            using (var context = new DatabaseContext())
            {
                if (SelectedId > 0)
                {
                    var holiday = context.HolidayRates.FirstOrDefault(c => c.RateId == SelectedId);
                    holiday.RateType = btnType.Content.ToString();
                    holiday.RateName = txtHolidayName.Text;
                    txtPercent.Value = holiday.Rate;
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (SelectedId > 0)
                {
                    var duplicates = context.HolidayRates.Where(c => c.RateName.ToLower().Contains(txtHolidayName.Text.ToLower()) && c.Rate.Equals(txtPercent.Value)).ToList();
                    if (txtHolidayName.Text == "")
                    {
                        MethodsClass.ShowNotification("Please input categoryname");
                    }
                    else if (duplicates.Count() > 0)
                    {
                        MethodsClass.ShowNotification("The item is already exist");
                    }
                    else
                    {
                        if (txtHolidayName.Text == "" || txtPercent.Value != 0)
                        {
                            var hol = context.HolidayRates.FirstOrDefault(c => c.RateId == SelectedId);
                            hol.RateName = txtHolidayName.Text;
                            hol.RateType = btnType.Content.ToString();
                            hol.Rate = txtPercent.Value;
                          
                            MethodsClass.ShowNotification("Successfully updated");
                            context.SaveChanges();
                            this.Close();
                        }
                    }
                }
                else
                {
                    var duplicates = context.HolidayRates.Where(c => c.RateName.ToLower().Contains(txtHolidayName.Text.ToLower()) && c.Rate.Equals(txtPercent.Value)).ToList();
                    if (txtHolidayName.Text == "")
                    {
                        MethodsClass.ShowNotification("Please input categoryname");
                    }
                    else if (duplicates.Count() > 0)
                    {
                        MethodsClass.ShowNotification("The item is already exist");
                    }
                    else
                    {
                        if (txtHolidayName.Text != null|| txtPercent.Value != 0 || dtDate.DateTime != null )
                        {
                            var Hrate = new HolidayRate();
                            Hrate.RateName = txtHolidayName.Text;
                            Hrate.Rate = txtPercent.Value;
                            Hrate.RateType = btnType.Content.ToString();
                            Hrate.HolidayDate = dtDate.DateTime.ToString("MMM dd, yyyy");


                            if (btnType.Content.ToString() == "Amount")
                            {
                                Hrate.Rate = txtAmount.Value;
                            }
                            else
                            {
                                if (btnType.Content.ToString() == "Percent")
                                {
                                    Hrate.Rate = txtPercent.Value;
                                }
                            }
                            context.HolidayRates.Add(Hrate);
                            context.SaveChanges();
                            MethodsClass.ShowNotification("Successfully Added!");
                            this.Close();
                        }
                    }
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
             DialogResult = false;
        }

        private void btnType_Click(object sender, RoutedEventArgs e)
        {
            if (btnType.Content.ToString() == "Amount")
            {
                txtPercent.Visibility = Visibility.Hidden;
                txtAmount.Visibility = Visibility.Visible;
            }
            else
            {
                if (btnType.Content.ToString() == "Percent")
                {
                    txtAmount.Visibility = Visibility.Hidden;
                    txtPercent.Visibility = Visibility.Visible;

                }
            }
        }

    }
}
