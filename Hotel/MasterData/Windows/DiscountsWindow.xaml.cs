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
    /// Interaction logic for DiscountWindow.xaml
    /// </summary>
    public partial class DiscountsWindow : Window
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        int SelectedId = 0;

        public DiscountsWindow()
        {
            InitializeComponent();
        }

        public DiscountsWindow(int id)
        {
            InitializeComponent();
            this.SelectedId = id;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDiscountPercent.Visibility = Visibility.Hidden;
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
                    var discount = context.Discounts.FirstOrDefault(c => c.DiscountId == SelectedId);
                    discount.DiscountType = btnDiscountType.Content.ToString();
                    discount.DiscountName = txtDiscountName.Text;
                    txtDiscountPercent.Value = discount.DiscountAmount;
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
                var duplicates = context.Discounts.Where(c => c.DiscountName.ToLower().Contains(txtDiscountName.Text.ToLower()) && c.DiscountAmount.Equals(txtDiscountPercent.Value)).ToList();
                if (txtDiscountName.Text == "" || txtDiscountPercent.Value != 0)
                { 
                    if (SelectedId > 0)
                    {
                        if (duplicates.Count() > 0)
                        {
                            MethodsClass.ShowNotification("This name already exists!");
                        }
                        else
                        {
                            var discount = context.Discounts.FirstOrDefault(c => c.DiscountId == SelectedId);
                            discount.DiscountName = txtDiscountName.Text;
                            discount.DiscountType = btnDiscountType.Content.ToString();
                            discount.DiscountAmount = txtDiscountPercent.Value;
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
                            var discount = new Discount();
                            discount.DiscountName = txtDiscountName.Text;
                            discount.DiscountAmount = txtDiscountPercent.Value;
                            discount.DiscountType = btnDiscountType.Content.ToString(); 
                            
                            if (btnDiscountType.Content.ToString() == "Amount")
                            {
                                discount.DiscountAmount = txtDiscountAmount.Value;
                            }
                            else
                            {
                                if (btnDiscountType.Content.ToString() == "Percent")
                                {
                                    discount.DiscountAmount = txtDiscountPercent.Value;

                                }
                            }
                            context.Discounts.Add(discount);
                            context.SaveChanges();
                            MethodsClass.ShowNotification("Successfully Added !");
                            this.Close();
                        }
                    }
                }
                else
                {
                    if (txtDiscountName.Text =="")
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

        private void btnDiscountType_Click(object sender, RoutedEventArgs e)
        {
            if (btnDiscountType.Content.ToString() == "Amount")
            {
               txtDiscountPercent.Visibility = Visibility.Hidden;
                 txtDiscountAmount.Visibility = Visibility.Visible;
            
            }
            else
            {
                if (btnDiscountType.Content.ToString() == "Percent")
                {
                   txtDiscountAmount.Visibility = Visibility.Hidden;
                  txtDiscountPercent.Visibility = Visibility.Visible;
                
                }
            }
        }

        private void chTime_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
