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

namespace Hotel.Booking.SubPage
{
    /// <summary>
    /// Interaction logic for RoomDescriptionPage.xaml
    /// </summary>
    public partial class RoomDescriptionPage : UserControl
    {
        List<RoomEquipment> RoomEquipments = new List<RoomEquipment>();
        List<RoomTypeRate> RoomTypeRates = new List<RoomTypeRate>();

        int SelectedId = 0;
        public RoomDescriptionPage()
        {
            InitializeComponent();
        }
        public RoomDescriptionPage(int id)
        {
            InitializeComponent();
            this.SelectedId = id;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
            
                var room = context.Rooms.FirstOrDefault(c => c.RoomId == SelectedId);
                var Rooms = context.Rooms.ToList();
                var roomtype = context.RoomTypes.FirstOrDefault(c => c.RoomTypeId == room.RoomTypeId);

                if (SelectedId > 0)
                {
                    txtRoomStatus.Text = room.Status;
                    txtRoomSize.Text = room.RoomSize;
                    txtRoomCapacity.Text = room.Capacity.ToString();
                    txtRoomType.Text = roomtype.RoomTypeName;
                    txtRoomFloor.Text = room.BuildingFloor;
                    txtRoomDescription.Text = room.RoomDescription;
                    txtRoomNumber.Text = room.RoomNumber.ToString();
                }

                RoomEquipments = context.RoomEquipments.Where(c => c.RoomId == SelectedId).ToList();
     
                dgEquipRoom.ItemsSource = null;
                dgEquipRoom.ItemsSource = RoomEquipments;
    
                viewEquipRoom.BestFitColumns();

                RoomTypeRates = context.RoomTypeRates.Where(c => c.RoomTypeId == room.RoomTypeId).ToList();
     

                dgRoomTypeRate.ItemsSource = null;
                dgRoomTypeRate.ItemsSource = RoomTypeRates;
                viewRoomTypeRate.BestFitColumns();
            }
            
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);

            frame.BackNavigationMode = BackNavigationMode.PreviousScreen;
            frame.GoBack();
        }
    }
}
