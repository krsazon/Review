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
    /// Interaction logic for OccupancyRateWindow.xaml
    /// </summary>
    public partial class OccupancyRateWindow : Window
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        int SelectedId = 0;
        public OccupancyRateWindow(int id)
        {
            InitializeComponent();
            this.SelectedId = id;
        }
        public OccupancyRateWindow()
        {
            InitializeComponent();
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
            txtOccupancyRatePercent.Visibility = Visibility.Hidden;
           
            using (var context = new DatabaseContext())
            {
                var total = context.Rooms.Select(c => c.RoomId);
                blkTotalRooms.Text = total.Count().ToString();
              
                if (SelectedId > 0)
                {
                    var occupancy = context.OccupancyRates.FirstOrDefault(c => c.OccupancyRateId == SelectedId);
                    txtOccupancyRateName.Text = occupancy.OccupancyRateName;
                    occupancy.OccupancyRateType = btnOccupancyRateType.Content.ToString();
                    txtOccupancyRateAmount.Value = occupancy.OccupancyRateAmount;
                }
            }
        }
         private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var occu = context.OccupancyRates.FirstOrDefault(c => c.OccupancyRateId == SelectedId);
                var room = context.Rooms.FirstOrDefault(c => c.RoomId == SelectedId);
                var duplicates = context.OccupancyRates.Where(c => c.OccupancyRateName.ToLower().Contains(txtOccupancyRateName.Text.ToLower()) && c.OccupancyRateAmount.Equals(txtOccupancyRateAmount.Value)).ToList();
                if (txtOccupancyRateName.Text == "" || txtOccupancyRateAmount.Value != 0)
                { 
                    if (SelectedId > 0)
                    {
                        if (duplicates.Count() > 0)
                        {
                            MethodsClass.ShowNotification("This name already exists!");
                        }
                        else
                        {
                          
                            var occupan = context.OccupancyRates.FirstOrDefault(c => c.OccupancyRateId == SelectedId);
                            occupan.OccupancyRateType = btnOccupancyRateType.Content.ToString();
                            occupan.OccupancyRateName = txtOccupancyRateName.Text;
                            if (btnOccupancyRateType.Content.ToString() == "Percent")
                            {
                                occupan.OccupancyRateAmount = txtOccupancyRatePercent.Value;
                            }
                            else
                            {
                                if (btnOccupancyRateType.Content.ToString() == "Amount")
                                {
                                    occupan.OccupancyRateAmount = txtOccupancyRateAmount.Value;

                                }
                            }
                            MethodsClass.ShowNotification("Successfully Updated Record!");
                            context.SaveChanges();
                            this.Close();
                        }
                    }

                    else
                    {
                        if (duplicates.Count() > 0)
                        {
                            MethodsClass.ShowNotification("This name already exists!");
                        }
                        else
                        {
                            var occup = new OccupancyRate();
                            occup.OccupancyRateType = btnOccupancyRateType.Content.ToString();
                            occup.OccupancyRateName = txtOccupancyRateName.Text;
                          
                            if (btnOccupancyRateType.Content.ToString() == "Percent")
                            {
                                occup.OccupancyRateAmount = txtOccupancyRatePercent.Value;
                            }
                            else
                            {
                                if (btnOccupancyRateType.Content.ToString() == "Amount")
                                {
                                    occup.OccupancyRateAmount = txtOccupancyRateAmount.Value;

                                }
                            }
                            context.OccupancyRates.Add(occup);
                            context.SaveChanges();
                            MethodsClass.ShowNotification("Successfully Added !");
                            this.Close();
                        }
                    }
                }
                else
                {
                    if (txtOccupancyRateName.Text =="")
                    {
                        MethodsClass.ShowNotification("Please fill up the fields correctly.");

                    }
                }
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void btnOccupancyRateType_Click(object sender, RoutedEventArgs e)
        {
            if (btnOccupancyRateType.Content.ToString() == "Percent")
            {
                txtOccupancyRateAmount.Visibility = Visibility.Hidden;
                txtOccupancyRatePercent.Visibility = Visibility.Visible;
            }
            else
            {
                if (btnOccupancyRateType.Content.ToString() == "Amount")
                {
                    txtOccupancyRatePercent.Visibility = Visibility.Hidden;
                    txtOccupancyRateAmount.Visibility = Visibility.Visible;
                }
            }
        }
    
    }
       
        }
   