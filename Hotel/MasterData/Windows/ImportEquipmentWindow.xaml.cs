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
    /// Interaction logic for ImportEquipmentWindow.xaml
    /// </summary>
    public partial class ImportEquipmentWindow : Window
    {
        public string roomequipmentid;
        int roomidlastrow = 0;
        List<Equipment> Equipments = new List<Equipment>();
        List<RoomEquipment> RoomEquipments = new List<RoomEquipment>();
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        public static string txt="";


        public ImportEquipmentWindow()
        {
            InitializeComponent();
        }

        public ImportEquipmentWindow(int id)
        {
            InitializeComponent();
            this.roomidlastrow = id;
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
            //int roomid = Int32.Parse(roomequipmentid);
            MessageBox.Show(roomidlastrow.ToString());
            using (var context = new DatabaseContext())
            {
                Equipments = context.Equipments.ToList();
                RoomEquipments = context.RoomEquipments.Where(c => c.RoomId == roomidlastrow).ToList();
                if (txtSearch.Text != "")
                {


                }
                dgEquip.ItemsSource = null;
                dgEquip.ItemsSource = Equipments;
                dgEquipRoom.ItemsSource = null;
                dgEquipRoom.ItemsSource = RoomEquipments;
                viewEquip.BestFitColumns();
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
            //if (selectedid > 0)
            //{
            //    if (dgEquipRoom.SelectedItem == null)
            //    {
            //        this.Close();
            //    }
            //    else
            //    {
            //        this.Close();
            //    }
            //}
            //else
            //{
                if (dgEquipRoom.SelectedItem == null)
                {
                    this.Close();
                }
                else
                {
                    dgEquipRoom.SelectAll();

                    foreach (RoomEquipment d in dgEquipRoom.SelectedItems)
                    {
                        var item = context.RoomEquipments.FirstOrDefault(c => c.RoomId == d.RoomId);
                        context.RoomEquipments.Remove(item);
                        context.SaveChanges();
                    }
                    this.Close();
                }
              }
           }
        //}

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            dgEquipRoom.SelectAll();
                var equipment = dgEquip.SelectedItem as Equipment;
                var window = new Windows.RoomWindow();
                List<string> equipmentselect = new List<string>();
                List<string> equipmentq = new List<string>();
                foreach (RoomEquipment c in dgEquipRoom.SelectedItems)
                {
                    equipmentselect.Add(c.Equipment);
                    equipmentselect.Add(c.Quantity.ToString());
                }
                txt = string.Join(" ", equipmentselect);
                this.Close();
            }
        
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dgEquip_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            if (dgEquip.SelectedItem != null)
            {
                using (var context = new DatabaseContext())
                {
                    var room = dgEquip.SelectedItem as Equipment;
                    txtEquipment.Text = room.EquipmentName;
                }
            }
            else
            {
                txtEquipment.Text = "";
            }
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        { 
            if (txtStock.Text == "" )
                {
                    MethodsClass.ShowNotification("Please fill up the fields correctly.");
                }

            else
            {
            using (var context = new DatabaseContext())
            {
                var room = new RoomEquipment();
              
                        room.Equipment = txtEquipment.Text;
                        room.Quantity = Int32.Parse(txtStock.Text);
                        room.RoomId = roomidlastrow;
                        context.RoomEquipments.Add(room);
                        context.SaveChanges();
                        Refresh();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgEquipRoom.SelectedItem != null)
            {
                if (MethodsClass.ShowConfirmation("Do you really want to delete this record?") == true)
                {
                    var selected = dgEquipRoom.SelectedItem as RoomEquipment;
                    using (var context = new DatabaseContext())
                    {
                        var equip = context.RoomEquipments.FirstOrDefault(c => c.RoomEquipmentId == selected.RoomEquipmentId);
                        context.RoomEquipments.Remove(equip);
                        context.SaveChanges();
                    }
                    Refresh();
                }
            }
            else
            {
                MethodsClass.ShowNotification("Empty");
            }
        }
    }
}
