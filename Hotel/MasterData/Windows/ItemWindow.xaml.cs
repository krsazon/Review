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
    /// Interaction logic for ItemWindow.xaml
    /// </summary>
    public partial class ItemWindow : Window
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        int SelectedId = 0;
        public ItemWindow()
        {
            InitializeComponent();
        }

        public ItemWindow(int id)
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
            
            if (SelectedId > 0)
            {
                using (var context = new DatabaseContext())
                {
                    var item = context.Items.FirstOrDefault(c => c.ItemId == SelectedId);
                    txtItemName.Text = item.ItemName;
                    List<String> list = context.ItemGroups.Select(c => c.ItemCategory).ToList();
                    cmbItemCategory.ItemsSource = list;
                    var category = context.ItemGroups.FirstOrDefault(c => c.ItemGroupId == item.ItemGroupId);
                    cmbItemCategory.Text = category.ItemCategory;
                    txtItemAmount.Text = item.ItemAmount.ToString();
                }
            }
            else
            {
                using (var context = new DatabaseContext())
                {
                    List<String> list = context.ItemGroups.Select(c => c.ItemCategory).ToList();
                    cmbItemCategory.ItemsSource = list;
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (SelectedId > 0)
                {
                    var duplicates = context.Items.Where(c => c.ItemName == txtItemName.Text).ToList();
                    if (txtItemName.Text == "")
                    {
                        MethodsClass.ShowNotification("Please input item name");
                    }
                    else if (duplicates.Count() > 0)
                    {
                        MethodsClass.ShowNotification("The item is already exist");
                    }

                    else
                    {
                        if (txtItemName.Text != "" && txtItemAmount.Text != "" && cmbItemCategory.Text != "")
                        {
                            var item = context.Items.FirstOrDefault(c => c.ItemId == SelectedId);
                            var category = context.ItemGroups.FirstOrDefault(c => c.ItemCategory == cmbItemCategory.Text);
                            decimal price;
                            bool amt = decimal.TryParse(txtItemAmount.Text, out price);
                            item.ItemAmount = price;
                            item.ItemGroupId = category.ItemGroupId;
                            item.ItemName = txtItemName.Text;
                            context.SaveChanges();
                            MethodsClass.ShowNotification("Successfully Updated");
                            DialogResult = true;
                        }
                        
                        else if (txtItemName.Text == "" || txtItemAmount.Text == "" || cmbItemCategory.Text == "")
                        {
                            MethodsClass.ShowNotification("Please input data in the necessary fields");
                        }
                    }
                }
                else
                {
                    var duplicates = context.Items.Where(c => c.ItemName == txtItemName.Text ).ToList();
                    if (duplicates.Count() > 0)
                    {
                        MethodsClass.ShowNotification("The item is already exist");
                    }
                    else
                    {
                        if (txtItemName.Text != "" && txtItemAmount.Text != "" && cmbItemCategory.Text != "")
                        {
                            var item = new Item();
                            var category = context.ItemGroups.FirstOrDefault(c => c.ItemCategory == cmbItemCategory.Text);
                            decimal price;
                            bool amt = decimal.TryParse(txtItemAmount.Text, out price);
                            item.ItemAmount = price;
                            item.ItemGroupId = category.ItemGroupId;
                            item.ItemName = txtItemName.Text;
                            context.Items.Add(item);
                            context.SaveChanges();
                            MethodsClass.ShowNotification("Successfully Added");
                            DialogResult = true;
                        }
                       
                        else if (txtItemName.Text == "" || txtItemAmount.Text == "" || cmbItemCategory.Text == "")
                        {
                            MethodsClass.ShowNotification("Please input data in the necessary fields");
                        }
                    }
                }
            }
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
    }
}
