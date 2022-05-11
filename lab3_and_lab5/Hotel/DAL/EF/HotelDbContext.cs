using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext()
            : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=HotelDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>().HasData(new[]
            {
                new Room
                {
                    Id = 1,
                    Number = "322",
                    TypeId = 3,
                    PricePerDay = 750
                },
                new Room
                {
                    Id = 2,
                    Number = "315",
                    TypeId = 2,
                    PricePerDay = 550
                },
                new Room
                {
                    Id = 3,
                    Number = "317",
                    TypeId = 3,
                    PricePerDay = 750
                },
                new Room
                {
                    Id = 4,
                    Number = "215",
                    TypeId = 1,
                    PricePerDay = 250
                },
                new Room
                {
                    Id = 5,
                    Number = "208",
                    TypeId = 3,
                    PricePerDay = 750
                },
                new Room
                {
                    Id = 6,
                    Number = "522",
                    TypeId = 2,
                    PricePerDay = 550
                },
                new Room
                {
                    Id = 7,
                    Number = "666",
                    TypeId = 1,
                    PricePerDay = 250
                }
            });

            modelBuilder.Entity<Booking>().HasData(new[]
            {
                new Booking
                {
                    Id = 1,
                    RoomId = 1,
                    IsOccupied = true,
                    DateOfCheckIn = DateTime.Now,
                    DateOfCheckOut = DateTime.Now.AddMonths(2)
                }
            });

            modelBuilder.Entity<RoomType>().HasData(new[]
            {
                new RoomType
                {
                    Id = 1,
                    Type = "Single"
                },
                new RoomType
                {
                    Id = 2,
                    Type = "Double"
                },
                new RoomType
                {
                    Id = 3,
                    Type = "Triple"
                },
            });
        }


        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

    }
}
