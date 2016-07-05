using DevExpress.Xpf.WindowsUI;
using DevExpress.XtraReports.UI;
using Hotel.MasterData.Reports;
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
    /// Interaction logic for RoomView.xaml
    /// </summary>
    public partial class RoomPage : UserControl
    {
        //List<RoomTypeListView> viewList = new List<RoomTypeListView>();
        List<Room> Rooms = new List<Room>();
        int selectedId = 0;
        public RoomPage()
        {
            InitializeComponent();
        }

        public RoomPage(int id)
        {
            InitializeComponent();
            this.selectedId = id;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        public void Refresh()
        {
            using (var context = new DatabaseContext())
            {
                 Rooms = context.Rooms.ToList();
                 var viewList = new List<RoomTypeListView>();
                 //if (txtSearch.Text != "")
                 //{
                 //    Rooms = Rooms.Where(c => c.Status.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
                 //}
                 foreach (var room in Rooms)
                 {
                     var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == room.RoomTypeId);
                     viewList.Add(new RoomTypeListView()
                     {
                         RoomTypeName = roomtype.RoomTypeName,
                         RoomNumber = room.RoomNumber,
                         RoomEquipmentId = room.RoomEquipmentId,
                         Status = room.Status,
                         BuildingFloor = room.BuildingFloor,
                         RoomDescription = room.RoomDescription,
                         RoomSize = room.RoomSize,
                         Capacity=room.Capacity,
                         Smoking = room.Smoking
                     });
                 }
                 //dgRooms.ItemsSource = null;
                 dgRooms.ItemsSource = viewList;
                 viewRooms.BestFitColumns();
                 //var total = context.Rooms.Select(c => c.RoomId);
                 //blkTotal.Text = total.Count().ToString();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.RoomWindow();
            MethodsClass.ShowPropertyWindow(window);
            Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgRooms.SelectedItem == null)
            {
                MethodsClass.ShowNotification("Empty");
            }
            else
            {
                if (MethodsClass.ShowConfirmation("Do you really want to remove this record?"))
                {
                    using (var context = new DatabaseContext())
                    {
                        var selected = dgRooms.SelectedItem as RoomTypeListView;                
                            var roomtype = context.Rooms.FirstOrDefault(c => c.RoomNumber == selected.RoomNumber);
                            var equip = context.RoomEquipments.FirstOrDefault(c => c.RoomId == selected.RoomId);
                            context.RoomEquipments.Remove(equip);
                            context.Rooms.Remove(roomtype);
                            context.SaveChanges(); 
                    }
                    dgRooms.ItemsSource = null;
                    Refresh();
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgRooms.SelectedItem != null)
            {
                var selectedRow = dgRooms.SelectedItem as RoomTypeListView;
                var window = new Windows.RoomWindow(selectedRow.RoomId);
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
            List<RoomListsView> ViewList = new List<RoomListsView>();
            using (var context = new DatabaseContext())
            {
                foreach (var list in Rooms)
                {
                    ViewList.Add(new RoomListsView()
                    {
                        RoomId = list.RoomId,
                        RoomDescription = list.RoomDescription,
                        RoomEquipment = list.RoomEquipmentId,
                        RoomSize = list.RoomSize,
                        RoomNumber = list.RoomNumber,
                        BuildingFloor = list.BuildingFloor,
                        Capacity = list.Capacity.ToString(),
                        RoomTypeId = list.RoomTypeId,
                        Smoking = list.Smoking,
                        Status = list.Status
                    });
                }
            }
            var dataList = new List<RoomReportData>();
            dataList.Add(new RoomReportData()
            {
                Title = "Rooms",
                ReportHeader = "Header",
                details = ViewList
            });

            var report = new RoomReportDesign() { DataSource = dataList, Name = "Report" };
            using (var printTool = new ReportPrintTool(report))
            {
                printTool.ShowRibbonPreviewDialog();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void dgDistricts_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            if (dgRooms.SelectedItem != null)
            {
                using (var context = new DatabaseContext())
                {
                    var room = dgRooms.SelectedItem as RoomTypeListView;
                    txtRoomNumber.Text = room.RoomNumber.ToString();
                    txtStatus.Text = room.Status;
                    txtFloor.Text = room.BuildingFloor;
                    txtEquipments.Text = room.RoomEquipmentId.ToString();
                    txtDescription.Text = room.RoomDescription;
                    txtRoomType.Text = room.RoomTypeName;
                    txtRoomSize.Text = room.RoomSize;
                    txtCapacity.Text = room.Capacity.ToString();
                    txtSmoking.Text = room.Smoking;
                }
            }
            else
            {
                txtRoomType.Text = "";
                txtRoomNumber.Text = "";
                txtStatus.Text = "";
                txtFloor.Text = "";
                txtEquipments.Text = "";
                txtDescription.Text = "";
            }
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            Refresh();            
        }
    }
}
