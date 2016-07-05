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
    /// Interaction logic for TransactionItemWindow.xaml
    /// </summary>
    public partial class TransactionItemWindow : Window
    {
        int selectedId = 0;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenWidth = Application.Current.MainWindow.Width;
        public static decimal fbo = 0;
        public TransactionItemWindow()
        {
            InitializeComponent();
        }

        public TransactionItemWindow(int id)
        {
            InitializeComponent();
            this.selectedId = id;
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

            using (var context = new DatabaseContext())
            {
                List<String> itemlist = context.ItemGroups.Select(c => c.ItemCategory).ToList();
                cmbItemGroup.ItemsSource = itemlist;
                cmbItemGroup.SelectedIndex = 0;
            }
        }


        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            { 
                var transactionitem = new TransactionItem();
                var item = context.Items.FirstOrDefault(c => c.ItemName == cmbItems.Text);
                var transaction = context.Transactions.FirstOrDefault(c => c.RoomId == selectedId);
             

                transactionitem.ItemId = item.ItemId;
                transactionitem.ItemQuantity = Int32.Parse(spnQuantity.Text);
                transactionitem.ItemTotal = decimal.Parse(txtPrice.Text);
                transactionitem.Cancelled = false;
                transactionitem.Username = Hotel.Page.LoginPage.tx;
                transactionitem.UnitPrice = decimal.Parse(txtUnit.Text);
                transactionitem.TransactionId = transaction.TransactionId;
                transactionitem.RoomId = transaction.RoomId;
                var count = context.TransactionItems.Where(c => c.RoomId == selectedId).Where(c => c.Cancelled == false).Select(c => c.ItemTotal).ToList();
               fbo = count.Sum();

                context.TransactionItems.Add(transactionitem);
                context.SaveChanges();
  
                this.Close();

            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbItemGroup_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
               
                var itemgroup = context.ItemGroups.FirstOrDefault(c => c.ItemCategory == cmbItemGroup.Text);

                List<String> itemlist = context.Items.Where(c => c.ItemGroupId == itemgroup.ItemGroupId).Select(x => x.ItemName).ToList();
                cmbItems.ItemsSource = itemlist;
                cmbItems.SelectedIndex = 0;

            }
        }

        private void cmbItems_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {

                var item = context.Items.FirstOrDefault(c => c.ItemName == cmbItems.Text);
                txtUnit.Text = item.ItemAmount.ToString();

            }
        }

        private void spnQuantity_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            decimal unit = decimal.Parse(txtUnit.Text) * Int32.Parse(spnQuantity.Text);

            txtPrice.Text = unit.ToString();
        }
        
    }
}
