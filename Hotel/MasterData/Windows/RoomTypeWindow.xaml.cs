using DevExpress.Xpf.Editors;
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
    /// Interaction logic for RoomType.xaml
    /// </summary>
    public partial class RoomTypeWindow : Window
    {
        int selectedid = 0;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;

        public RoomTypeWindow()
        {
            InitializeComponent();
        }
        public RoomTypeWindow(int id)
        {
            InitializeComponent();
            this.selectedid = id;
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

            txtAddTime.Items.Add("Minutes");
            txtAddTime.Items.Add("Hours");
            txtAddTime.Items.Add("Days");
            txtAddTime.Items.Add("Months"); 

            if (selectedid > 0)
            {
                using (var context = new DatabaseContext())
                {
                    var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == selectedid);
                    txtRoomType.Text = roomtype.RoomTypeName;
                   txtAddAmount.Text = roomtype.AdditionalChargesAmount.ToString();
                 txtAddTime.Text = roomtype.AdditionalChargesTime;
                txtAddNumberTime.Text = roomtype.AdditionalChargesNumberTime.ToString();
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
                if (selectedid > 0)
                {
                    if (txtRoomType.Text != "")
                    {
                            var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == selectedid);


                            int addnumbertime = Int32.Parse(txtAddNumberTime.Text);
                            decimal addamount = Decimal.Parse(txtAddAmount.Text);
                            roomtype.RoomTypeName = txtRoomType.Text;
                            roomtype.AdditionalChargesAmount = addamount;
                            roomtype.AdditionalChargesTime = txtAddTime.Text;
                            roomtype.AdditionalChargesNumberTime = addnumbertime;
                            MethodsClass.ShowNotification("Record successfully updated!");

                            context.SaveChanges();
                            this.Close();
                    }
                    else
                    {
                        MethodsClass.ShowNotification("Please fill up the fields correctly.");
                    }
                }
                else
                {
                        var roomtype = new RoomType();
                        if (txtRoomType.Text == "")
                        {
                            MethodsClass.ShowNotification("Please fill up the fields correctly.");
                        }
                        else
                        {
                            var duplicates = context.RoomTypes.Where(c => c.RoomTypeName.ToLower() == txtRoomType.Text.ToLower()).ToList();
                            if (duplicates.Count() > 0)
                            {
                                MethodsClass.ShowNotification("Cannot add duplicate records.");
                            }
                            else
                            {

                                int addnumbertime = Int32.Parse(txtAddNumberTime.Text);
                                decimal addamount = Decimal.Parse(txtAddAmount.Text);
                                roomtype.AdditionalChargesAmount = addamount;
                                roomtype.AdditionalChargesTime = txtAddTime.Text;
                                roomtype.AdditionalChargesNumberTime = addnumbertime;
                            roomtype.RoomTypeName = txtRoomType.Text;

                            context.RoomTypes.Add(roomtype);
                            context.SaveChanges();
                            MethodsClass.ShowNotification("New record successfully saved!");
                            this.Close();
                        }
                    }
                }
            }
        }

        private void txtAddTime_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (txtAddTime.Text == "Minutes")
            {
                ComboBoxEdit combo = new ComboBoxEdit();
                txtAddNumberTime.Items.Clear();
                txtAddNumberTime.Text = "30";
                txtAddNumberTime.Items.Add("30");
                txtAddNumberTime.Items.Add("40");
                txtAddNumberTime.Items.Add("50");
            }

            if (txtAddTime.Text == "Hours")
            {
                ComboBoxEdit combo = new ComboBoxEdit();
                txtAddNumberTime.Items.Clear();
                txtAddNumberTime.Text = "1";
                txtAddNumberTime.Items.Add("1");
                txtAddNumberTime.Items.Add("2");
                txtAddNumberTime.Items.Add("3");
                txtAddNumberTime.Items.Add("4");
                txtAddNumberTime.Items.Add("5");
                txtAddNumberTime.Items.Add("6");
                txtAddNumberTime.Items.Add("7");
                txtAddNumberTime.Items.Add("8");
                txtAddNumberTime.Items.Add("9");
                txtAddNumberTime.Items.Add("10");
                txtAddNumberTime.Items.Add("11");
                txtAddNumberTime.Items.Add("12");
                txtAddNumberTime.Items.Add("13");
                txtAddNumberTime.Items.Add("14");
                txtAddNumberTime.Items.Add("15");
                txtAddNumberTime.Items.Add("16");
                txtAddNumberTime.Items.Add("17");
                txtAddNumberTime.Items.Add("18");
                txtAddNumberTime.Items.Add("19");
                txtAddNumberTime.Items.Add("20");
                txtAddNumberTime.Items.Add("21");
                txtAddNumberTime.Items.Add("22");
                txtAddNumberTime.Items.Add("23");
                txtAddNumberTime.Items.Add("24");
            }

            if (txtAddTime.Text == "Days")
            {
                ComboBoxEdit combo = new ComboBoxEdit();
                txtAddNumberTime.Items.Clear();
                txtAddNumberTime.Text = "1";
                txtAddNumberTime.Items.Add("1");
                txtAddNumberTime.Items.Add("2");
                txtAddNumberTime.Items.Add("3");
                txtAddNumberTime.Items.Add("4");
                txtAddNumberTime.Items.Add("5");
                txtAddNumberTime.Items.Add("6");
                txtAddNumberTime.Items.Add("7");
                txtAddNumberTime.Items.Add("8");
                txtAddNumberTime.Items.Add("9");
                txtAddNumberTime.Items.Add("10");
                txtAddNumberTime.Items.Add("11");
                txtAddNumberTime.Items.Add("12");
                txtAddNumberTime.Items.Add("13");
                txtAddNumberTime.Items.Add("14");
                txtAddNumberTime.Items.Add("15");
                txtAddNumberTime.Items.Add("16");
                txtAddNumberTime.Items.Add("17");
                txtAddNumberTime.Items.Add("18");
                txtAddNumberTime.Items.Add("19");
                txtAddNumberTime.Items.Add("20");
                txtAddNumberTime.Items.Add("21");
                txtAddNumberTime.Items.Add("22");
                txtAddNumberTime.Items.Add("23");
                txtAddNumberTime.Items.Add("24");
                txtAddNumberTime.Items.Add("25");
                txtAddNumberTime.Items.Add("26");
                txtAddNumberTime.Items.Add("27");
                txtAddNumberTime.Items.Add("28");
                txtAddNumberTime.Items.Add("29");
                txtAddNumberTime.Items.Add("30");
                txtAddNumberTime.Items.Add("31");
            }

            if (txtAddTime.Text == "Months")
            {
                ComboBoxEdit combo = new ComboBoxEdit();
                txtAddNumberTime.Items.Clear();
                txtAddNumberTime.Text = "1";
                txtAddNumberTime.Items.Add("1");
                txtAddNumberTime.Items.Add("2");
                txtAddNumberTime.Items.Add("3");
                txtAddNumberTime.Items.Add("4");
                txtAddNumberTime.Items.Add("5");
                txtAddNumberTime.Items.Add("6");
                txtAddNumberTime.Items.Add("7");
                txtAddNumberTime.Items.Add("8");
                txtAddNumberTime.Items.Add("9");
                txtAddNumberTime.Items.Add("10");
                txtAddNumberTime.Items.Add("11");
                txtAddNumberTime.Items.Add("12");
            }
        }

      
    }
}
