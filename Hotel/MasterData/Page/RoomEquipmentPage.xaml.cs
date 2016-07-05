using DevExpress.Xpf.WindowsUI;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel.MasterData.Page
{
    /// <summary>
    /// Interaction logic for RoomEquipmentView.xaml
    /// </summary>
    public partial class RoomEquipmentPage : UserControl
    {
        List<Equipment> Equipments = new List<Equipment>();
        public RoomEquipmentPage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        public void Refresh()
        {
            using (var context = new DatabaseContext())
            {
                Equipments = context.Equipments.ToList();
                if (txtSearch.Text != "")
                {
                    Equipments = Equipments.Where(c => c.EquipmentName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
                }
                dgEquip.ItemsSource = null;
                dgEquip.ItemsSource = Equipments;
                viewEquip.BestFitColumns();
                var total = context.Equipments.Select(c => c.EquipmentId);
                blkTotal.Text = total.Count().ToString();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.RoomEquipmentWindow();
            MethodsClass.ShowPropertyWindow(window);
            Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgEquip.SelectedItem == null)
            {
                MethodsClass.ShowNotification("Empty");
            }
            else
            {
                if (MethodsClass.ShowConfirmation("Do you really want to remove this record?"))
                {
                    using (var context = new DatabaseContext())
                    {
                        var selected = dgEquip.SelectedItem as Equipment;

                        var equipment = context.Equipments.FirstOrDefault(c => c.EquipmentId == selected.EquipmentId);
                        context.Equipments.Remove(equipment);
                        context.SaveChanges();

                    }
                    Refresh();
                }
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgEquip.SelectedItem != null)
            {
                var selectedRow = dgEquip.SelectedItem as Equipment;
                var window = new Windows.RoomEquipmentWindow(selectedRow.EquipmentId);
                MethodsClass.ShowPropertyWindow(window);
                Refresh();
            }
            else
            {
                MethodsClass.ShowNotification("Please select a row.");
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Variables.collapseUserContent = true;
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            frame.BackNavigationMode = BackNavigationMode.Root;
            frame.GoBack();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void dgDistricts_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
                if (dgEquip.SelectedItem != null)
                {
                    var equipments = dgEquip.SelectedItem as Equipment;
                    txtEquipmentName.Text = equipments.EquipmentName;
                }
                else
                {
                    txtEquipmentName.Text = "";
                }
            }
        }
    }
