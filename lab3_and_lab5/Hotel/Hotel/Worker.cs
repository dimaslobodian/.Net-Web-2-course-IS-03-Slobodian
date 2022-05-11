using BLL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Hosting;

namespace Hotel
{
    public class Worker : BackgroundService
    {
        private readonly IHost _host;
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;
        public Worker(IHost host, IRoomService roomService, IBookingService bookingService)
        {
            _host = host;
            _roomService = roomService;
            _bookingService = bookingService;
        }

        public static void GenerateUserChoice()
        {
            Console.WriteLine("\nWho are you?");
            Console.WriteLine("1. Guest");
            Console.WriteLine("2. Administrator");
            Console.WriteLine("3. Exit");
            Console.Write(">> ");
        }

        public static void GenerateGuestMenu()
        {
            Console.WriteLine("\nChoose the option:");
            Console.WriteLine("1. Book a room");
            Console.WriteLine("2. See available rooms for date");
            Console.WriteLine("3. See list rooms with prices");
            Console.WriteLine("4. Exit\n");
        }

        public async Task<Booking> BookRoom()
        {
            string roomNumber = default;

            string checkInYear = default;
            string checkInMonth = default;
            string checkInDay = default;

            string checkOutYear = default;
            string checkOutMonth = default;
            string checkOutDay = default;

            Console.WriteLine("What room do you want to book? (enter number)");
            Console.Write(">> ");
            roomNumber = Console.ReadLine();

            Console.WriteLine("What date do you want to check-in?\n");
            Console.Write(">>(year) ");
            checkInYear = Console.ReadLine();

            Console.Write("\n>>(month) ");
            checkInMonth = Console.ReadLine();

            Console.Write("\n>>(day) ");
            checkInDay = Console.ReadLine();

            Console.WriteLine("What date do you want to check-out?\n");
            Console.Write(">>(year) ");
            checkOutYear = Console.ReadLine();

            Console.Write("\n>>(month) ");
            checkOutMonth = Console.ReadLine();

            Console.Write("\n>>(day) ");
            checkOutDay = Console.ReadLine();

            Console.WriteLine("\n Booking process...\n");

            int roomId = (await _roomService.GetAllAsync()).Where(q => q.Number == roomNumber).Select(x => x.Id).FirstOrDefault();

            var booking = await _bookingService.CreateAsync(new Booking
            {
                RoomId = roomId,
                IsBooked = true,
                IsOccupied = false,
                DateOfCheckIn = new DateTime(Convert.ToInt32(checkInYear), Convert.ToInt32(checkInMonth), Convert.ToInt32(checkInDay)),
                DateOfCheckOut = new DateTime(Convert.ToInt32(checkOutYear), Convert.ToInt32(checkOutMonth), Convert.ToInt32(checkOutDay)),
            });
            return booking.Id == 0 ? null : booking;
        }


        public async Task<IEnumerable<Room>> GetFreeRoomsOfPeriod()
        {
            string checkInYear = default;
            string checkInMonth = default;
            string checkInDay = default;

            string checkOutYear = default;
            string checkOutMonth = default;
            string checkOutDay = default;

            Console.WriteLine("\nWhat date do you want to check-in?\n");
            Console.Write(">>(year) ");
            checkInYear = Console.ReadLine();

            Console.Write(">>(month) ");
            checkInMonth = Console.ReadLine();

            Console.Write(">>(day) ");
            checkInDay = Console.ReadLine();

            Console.WriteLine("\nWhat date do you want to check-out?\n");
            Console.Write(">>(year) ");
            checkOutYear = Console.ReadLine();

            Console.Write(">>(month) ");
            checkOutMonth = Console.ReadLine();

            Console.Write(">>(day) ");
            checkOutDay = Console.ReadLine();


            List<Room> freeRooms = (List<Room>)await _bookingService.GetFreeRoomsInPeriod(
                new DateTime(Convert.ToInt32(checkInYear), Convert.ToInt32(checkInMonth), Convert.ToInt32(checkInDay)),
                new DateTime(Convert.ToInt32(checkOutYear), Convert.ToInt32(checkOutMonth), Convert.ToInt32(checkOutDay)));
            if(freeRooms.Count > 0)
            {
                Console.WriteLine("\nFree Rooms:");
                foreach (Room room in freeRooms)
                {
                    Console.WriteLine(room.Number + " " + room.PricePerDay + " " + room.RoomType.Type);
                }

            }
            return freeRooms;
        }
        public async Task<IEnumerable<Room>> GetAllRoomsWithPrice()
        {
            List<Room> rooms = (await _roomService.GetAllAsync()).ToList();
            
            if(rooms.Count > 0)
            {
                Console.WriteLine("All Rooms In Hotel:\n");
                rooms.ForEach(x => Console.WriteLine(x.Number + " " + x.PricePerDay + " " + x.RoomType.Type));
            }

            Console.WriteLine();

            return rooms;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Welcome to 83qp38 Holel!\n\n");


            while (true)
            {
                string choice = "";
                GenerateUserChoice();
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        bool spinner = true;
                        while (spinner)
                        {
                            string guestChoice = "";
                            GenerateGuestMenu();
                            guestChoice = Console.ReadLine();
                            switch (guestChoice)
                            {
                                case "1":
                                    var res = await BookRoom();
                                    if (res != null)
                                        Console.WriteLine("\nBooking Successful!\n");
                                    break;
                                case "2":
                                    await GetFreeRoomsOfPeriod();
                                    break;
                                case "3":
                                    await GetAllRoomsWithPrice();
                                    break;
                                case "4":
                                    spinner = false;
                                    break;
                                default:
                                    Console.WriteLine("Enter appropriate number!");
                                    break;
                            }
                        }
                        break;
                    case "2":
                        Console.WriteLine("\nHave fun :)\n");
                        break;
                    case "3":
                        Console.WriteLine("Thank you for visiting our hotel :)\n\n\n");
                        return;
                    default:
                        Console.WriteLine("\nPleace, write coorect number!\n");
                        break;
                }
            }

            await _host.StopAsync();
        }
    }
}