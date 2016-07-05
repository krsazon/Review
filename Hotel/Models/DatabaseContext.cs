using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;

namespace Hotel.Models
{
    class DatabaseContext : DbContext
    {
        public DbSet<RoomType> RoomTypes
        {
            get;
            set;
        }

        public DbSet<RoomTypeRate> RoomTypeRates
        {
            get;
            set;
        }

        public DbSet<Room> Rooms
        {
            get;
            set;
        }

        public DbSet<Equipment> Equipments
        {
            get;
            set;
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<RoomEquipment> RoomEquipments { get; set; }
        public DbSet<ItemGroup> ItemGroups { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<TransactionItem> TransactionItems { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<OccupancyRate> OccupancyRates { get; set; }
        public DbSet<HolidayRate> HolidayRates { get; set; }
        public DbSet<StaffPosition> StaffPositions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new Initializer());
            modelBuilder.Entity<RoomType>().ToTable("RoomTypes", "public");
            modelBuilder.Entity<Room>().ToTable("Rooms", "public");
            modelBuilder.Entity<Equipment>().ToTable("Equipments", "public");
            modelBuilder.Entity<Item>().ToTable("Items", "public");
            modelBuilder.Entity<ItemGroup>().ToTable("ItemGroup", "public");
            modelBuilder.Entity<Transaction>().ToTable("Transactions", "public");
            modelBuilder.Entity<TransactionItem>().ToTable("TransactionItems", "public");
            modelBuilder.Entity<Reservation>().ToTable("Reservations", "public");
            modelBuilder.Entity<Staff>().ToTable("Staffs", "public");
            modelBuilder.Entity<Discount>().ToTable("Discounts", "public");
            modelBuilder.Entity<RoomEquipment>().ToTable("RoomEquipments", "public");
            modelBuilder.Entity<Parameter>().ToTable("Parameters", "public");
            modelBuilder.Entity<User>().ToTable("Users", "public");
            modelBuilder.Entity<UserGroup>().ToTable("UserGroups", "public");
            modelBuilder.Entity<RoomTypeRate>().ToTable("RoomTypeRates", "public");
            modelBuilder.Entity<OccupancyRate>().ToTable("OccupancyRates", "public");
            modelBuilder.Entity<HolidayRate>().ToTable("HolidayRates", "public");
            modelBuilder.Entity<StaffPosition>().ToTable("StaffPositions", "public");
        }

        public class Initializer : IDatabaseInitializer<DatabaseContext>
        {
            public void InitializeDatabase(DatabaseContext context)
            {
                if (!context.Database.Exists())
                {
                    context.Database.Create();
                    Seed(context);
                    context.SaveChanges();
                }
            }

            private void Seed(DatabaseContext context)
            {
                throw new NotImplementedException();
            }
        }
    }
}