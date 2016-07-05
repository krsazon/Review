using DevExpress.XtraEditors;
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
    /// Interaction logic for RoomTypeRateWindow.xaml
    /// </summary>
    public partial class RoomTypeRateWindow : Window
    {
        int selectedid = 0;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;

        public RoomTypeRateWindow()
        {
            InitializeComponent();
        }
        public RoomTypeRateWindow(int id)
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
            MessageBox.Show(selectedid.ToString());

            txtRateTime.Items.Add("Minutes");
            txtRateTime.Items.Add("Hours");
            txtRateTime.Items.Add("Days");
            txtRateTime.Items.Add("Months");

            if (selectedid > 0)
            {
                using (var context = new DatabaseContext())
                {
                    var roomrate = context.RoomTypeRates.FirstOrDefault(c => c.RoomTypeRateId == selectedid);
                    //var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == Page.RoomTypePage.select);
                    txtRateAmount.Text = roomrate.Amount.ToString();
                    txtRateTime.Text = roomrate.AmountTime;
                    txtRateNumberTime.Text = roomrate.AmountNumberTime.ToString();
                }
            }
            else
            {
                txtRoomType.Text = Page.RoomTypePage.name;
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

                    if (txtRoomType.Text == "" || txtRateAmount.Text == "" || txtRateTime.Text == "" || txtRateNumberTime.Text == "")
                    {
                        MethodsClass.ShowNotification("Please fill up the fields correctly.");
                    }
                    else
                    {
                        var roomtyperate = context.RoomTypeRates.FirstOrDefault(c => c.RoomTypeRateId == selectedid);
                        int ratenumbertime = Int32.Parse(txtRateNumberTime.Text);
                        decimal rateamount = Decimal.Parse(txtRateAmount.Text);
                        //   var roomty = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == selectedid);
                        // roomtype.RoomTypeRateNameId = roomty.RoomTypeId;
                        roomtyperate.Amount = rateamount;
                        roomtyperate.AmountTime = txtRateTime.Text;
                        roomtyperate.AmountNumberTime = ratenumbertime;
                        roomtyperate.RoomTypeId = selectedid;
                        MethodsClass.ShowNotification("Record successfully updated!");
                        context.SaveChanges();
                        this.Close();

                        // }
                    }
                }


                else
                {
                    var roomtype = new RoomTypeRate();
                    int amounttime = Int32.Parse(txtRateAmount.Text);
                        int amountnumber = Int32.Parse(txtRateNumberTime.Text);
                    var duplicates = context.RoomTypeRates.Where(c => c.AmountTime == txtRateTime.Text && c.AmountNumberTime == amountnumber).ToList();

                    if (txtRoomType.Text == "" || txtRateAmount.Text == "" || txtRateTime.Text == "" || txtRateNumberTime.Text == "")
                    {
                        MethodsClass.ShowNotification("Please fill up the fields correctly.");
                    }
                    else
                    {
                     
                            int ratenumbertime = Int32.Parse(txtRateNumberTime.Text);
                            decimal rateamount = Decimal.Parse(txtRateAmount.Text);
                            var roomty = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == selectedid);
                            roomtype.RoomTypeId = Page.RoomTypePage.selecttype;
                            var roomtypes = context.RoomTypeRates.Where(c => c.RoomTypeId == Page.RoomTypePage.selecttype).Select(c => c.Amount).ToList();

                          

                           if (roomtypes.Count() == 0)
                            {
                                roomtype.Amount = rateamount;
                                roomtype.AmountTime = txtRateTime.Text;
                                roomtype.AmountNumberTime = ratenumbertime;
                                context.RoomTypeRates.Add(roomtype);
                                context.SaveChanges();
                                MethodsClass.ShowNotification("New record successfully saved!");
                                this.Close();
                            }
                             else if (roomtypes.Max() < rateamount)
                            {
                                roomtype.Amount = rateamount;
                                roomtype.AmountTime = txtRateTime.Text;
                                roomtype.AmountNumberTime = ratenumbertime;
                                context.RoomTypeRates.Add(roomtype);
                                context.SaveChanges();
                                MethodsClass.ShowNotification("New record successfully saved!");
                                this.Close();
                            }
                             else
                            {
                                MessageBox.Show("You can only input number larger than the data in records");
                            }
                        
                    }
                }
            }
        }
        private void txtRateTime_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (txtRateTime.Text == "Minutes")
            {
                ComboBoxEdit combo = new ComboBoxEdit();
                txtRateNumberTime.Items.Clear();
                txtRateNumberTime.Text = "30";
                txtRateNumberTime.Items.Add("30");
                txtRateNumberTime.Items.Add("40");
                txtRateNumberTime.Items.Add("50");
            }
            if (txtRateTime.Text == "Hours")
            {
                ComboBoxEdit combo = new ComboBoxEdit();
                txtRateNumberTime.Items.Clear();
                txtRateNumberTime.Text = "1";
                txtRateNumberTime.Items.Add("1");
                txtRateNumberTime.Items.Add("2");
                txtRateNumberTime.Items.Add("3");
                txtRateNumberTime.Items.Add("4");
                txtRateNumberTime.Items.Add("5");
                txtRateNumberTime.Items.Add("6");
                txtRateNumberTime.Items.Add("7");
                txtRateNumberTime.Items.Add("8");
                txtRateNumberTime.Items.Add("9");
                txtRateNumberTime.Items.Add("10");
                txtRateNumberTime.Items.Add("11");
                txtRateNumberTime.Items.Add("12");
                txtRateNumberTime.Items.Add("13");
                txtRateNumberTime.Items.Add("14");
                txtRateNumberTime.Items.Add("15");
                txtRateNumberTime.Items.Add("16");
                txtRateNumberTime.Items.Add("17");
                txtRateNumberTime.Items.Add("18");
                txtRateNumberTime.Items.Add("19");
                txtRateNumberTime.Items.Add("20");
                txtRateNumberTime.Items.Add("21");
                txtRateNumberTime.Items.Add("22");
                txtRateNumberTime.Items.Add("23");
                txtRateNumberTime.Items.Add("24");
            }

            if (txtRateTime.Text == "Days")
            {
                ComboBoxEdit combo = new ComboBoxEdit();
                txtRateNumberTime.Items.Clear();
                txtRateNumberTime.Text = "1";
                txtRateNumberTime.Items.Add("1");
                txtRateNumberTime.Items.Add("2");
                txtRateNumberTime.Items.Add("3");
                txtRateNumberTime.Items.Add("4");
                txtRateNumberTime.Items.Add("5");
                txtRateNumberTime.Items.Add("6");
                txtRateNumberTime.Items.Add("7");
                txtRateNumberTime.Items.Add("8");
                txtRateNumberTime.Items.Add("9");
                txtRateNumberTime.Items.Add("10");
                txtRateNumberTime.Items.Add("11");
                txtRateNumberTime.Items.Add("12");
                txtRateNumberTime.Items.Add("13");
                txtRateNumberTime.Items.Add("14");
                txtRateNumberTime.Items.Add("15");
                txtRateNumberTime.Items.Add("16");
                txtRateNumberTime.Items.Add("17");
                txtRateNumberTime.Items.Add("18");
                txtRateNumberTime.Items.Add("19");
                txtRateNumberTime.Items.Add("20");
                txtRateNumberTime.Items.Add("21");
                txtRateNumberTime.Items.Add("22");
                txtRateNumberTime.Items.Add("23");
                txtRateNumberTime.Items.Add("24");
                txtRateNumberTime.Items.Add("25");
                txtRateNumberTime.Items.Add("26");
                txtRateNumberTime.Items.Add("27");
                txtRateNumberTime.Items.Add("28");
                txtRateNumberTime.Items.Add("29");
                txtRateNumberTime.Items.Add("30");
                txtRateNumberTime.Items.Add("31");
            }
            if (txtRateTime.Text == "Months")
            {
                ComboBoxEdit combo = new ComboBoxEdit();
                txtRateNumberTime.Items.Clear();
                txtRateNumberTime.Text = "1";
                txtRateNumberTime.Items.Add("1");
                txtRateNumberTime.Items.Add("2");
                txtRateNumberTime.Items.Add("3");
                txtRateNumberTime.Items.Add("4");
                txtRateNumberTime.Items.Add("5");
                txtRateNumberTime.Items.Add("6");
                txtRateNumberTime.Items.Add("7");
                txtRateNumberTime.Items.Add("8");
                txtRateNumberTime.Items.Add("9");
                txtRateNumberTime.Items.Add("10");
                txtRateNumberTime.Items.Add("11");
                txtRateNumberTime.Items.Add("12");
            }
        }

        private void txtAddTime_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
        }
    }
}
