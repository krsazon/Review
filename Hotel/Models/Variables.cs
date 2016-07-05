using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
    class Variables
    {
        public static double screenWidth { get; set; }
        public static double screenHeight { get; set; }
        public static double screenLeftEdge { get; set; }
        public static double screenTopEdge { get; set; }

        //for logout and exit messageboxes
        public static bool yesClicked { get; set; }

        //for animation of menu
        public static bool collapseUserContent { get; set; }
        public static bool collapseMenu { get; set; }

        //for browsing image
        public static bool hasSearched { get; set; }

        //for emergency out
        public static bool emergencyOut = false;
    }

    public class RoomTypeListView
    {
        public int RoomId { get; set; }

        public String RoomNumber { get; set; }
        public string RoomTypeName { get; set; }
        public string RoomDescription { get; set; }
        public string RoomEquipmentId { get; set; }
        public string Status { get; set; }
        public string BuildingFloor { get; set; }
        public int Capacity { get; set; }
        public string RoomSize { get; set; }
        public string Smoking { get; set; }
    }

    public class ItemGroupListView
    {
        public int ItemGroupId { get; set; }
        public string ItemName { get; set; }
    }

    public class ItemListView
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemGroupId { get; set; }
        public decimal Amount { get; set; }
    }

    public class BookingView
    {
        public int RoomId { get; set; }

        public String RoomNumber { get; set; }
        public String RoomDescription { get; set; }
        public String GuestNumber { get; set; }
        public String RoomSlipNumber { get; set; }
        public String RoomType { get; set; }
        public String CheckInDate { get; set; }
        public String Floor { get; set; }
        public String CheckInTime { get; set; }
        public String NumberTime { get; set; }
        public String TimeType { get; set; }
        public String DiscountId { get; set; }
        public String StaffId { get; set; }
        public String CheckOutDate { get; set; }
        public String CheckOutTime { get; set; }
        public String RoomCharge { get; set; }
        public String Username { get; set; }
        public String DiscountCard { get; set; }
        public String DiscountAmount { get; set; }
        public String Status { get; set; }
        public String RoomEquipmentId { get; set; }
        public int TransactionId { get; set; }
        public String NetAmount { get; set; }
        public String AmountPaid { get; set; }
        public String Change { get; set; }
    }

    public class TransactionItemView
    {
        public int TransactionItemId { get; set; }
        public String TransactionId { get; set; }
        public String ItemId { get; set; }
        public String ItemQuantity { get; set; }
        public String UnitPrice { get; set; }
        public String Cancelled { get; set; }
        public String ItemTotal { get; set; }
        public String Username { get; set; }
        public int RoomNumber { get; set; }
        //public String EntryDate { get; set; }
        //public String EntryTime { get; set; }
    }

    public class ReservationView
    {
        public int ReservationId { get; set; }

        public String RoomId { get; set; }
        public String ReservationFee { get; set; }
        public String CustomerName { get; set; }
        public String RoomNumber { get; set; }
        public String RoomType { get; set; }
        public String DateReserved { get; set; }
        public String ArrivalDate { get; set; }
        public String ArrivalTime { get; set; }
        public String RequestDate { get; set; }
        public String RequestTime { get; set; }
        public String CardNumber { get; set; }
        public String Status { get; set; }
    }

    public class UsersView
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string UserGroupId { get; set; }
    }
    public class OccupancyRateView
    {
        public int OccupancyRateId { get; set; }
        public string OccupancyRateName { get; set; }
        public string OccupancyRateType { get; set; }
        public decimal OccupancyRateAmount { get; set; }

    }
    public class HolidayView
    {
        public int RateId { get; set; }
        public string HolidayName { get; set; }
        public string RateType { get; set; }
        public decimal Rate { get; set; }
        public string HolidayDate { get; set; }

    }
    public class RoomListsView
    {
        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomDescription { get; set; }
        public string Status { get; set; }
        public string BuildingFloor { get; set; }
        public String RoomNumber { get; set; }
        public string RoomEquipment { get; set; }
        public string RoomSize { get; set; }
        public string Capacity { get; set; }
        public string Smoking { get; set; }
    }
    public class CheckOutView
    {
        public int TransactionId { get; set; }
        public String RoomTypeId { get; set; }
        public String RoomNumber { get; set; }
        public String GuestNumber { get; set; }
        public String ReservationId { get; set; }
        public String RoomSlipNumber { get; set; }
        public String CheckInDate { get; set; }
        public String CheckInTime { get; set; }
        public String DiscountCard { get; set; }
        public String DiscountId { get; set; }
        public String StaffId { get; set; }
        public String CheckOutDate { get; set; }
        public String CheckOutTime { get; set; }
        public String RoomCharge { get; set; }
        public String Username { get; set; }
        public String DiscountAmount { get; set; }
        public String NetAmount { get; set; }
        public String AmountPaid { get; set; }
        public String Change { get; set; }
        public String Status { get; set; }
    }
}
