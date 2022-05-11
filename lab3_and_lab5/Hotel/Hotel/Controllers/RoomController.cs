using BLL.Interfaces;
using DAL.Models;
using Hotel.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Hotel
{
    [ApiController]
    [Route("[controller]/")]
    public class HotelController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;
        public HotelController(IRoomService roomService, IBookingService bookingService)
        {
            _roomService = roomService;
            _bookingService = bookingService;
        }

        [HttpGet]
        [Route("rooms")]
        public async Task<IActionResult> GetAllRooms()
        {
            return Ok(await _roomService.GetAllAsync());
        }

        [HttpGet]
        [Route("rooms/date")]
        public async Task<IActionResult> GetRooms(int checkInYear, int checkInMonth, int checkInDay,
                                                  int checkOutYear, int checkOutMonth, int checkOutDay)
        {
            return Ok(await _bookingService.GetFreeRoomsInPeriod(new DateTime(checkInYear, checkInMonth, checkInDay),
                                                       new DateTime(checkOutYear, checkOutMonth, checkOutDay)));

        }

        [HttpPost]
        [Route("book")]
        public async Task<IActionResult> BookRoom(BookingDto booking)
        {
            int roomId = (await _roomService.GetAllAsync()).Where(q => q.Number == booking.RoomNumber).Select(x => x.Id).FirstOrDefault();

            var bookingResult = await _bookingService.CreateAsync(new Booking
            {
                RoomId = roomId,
                IsBooked = true,
                IsOccupied = false,
                DateOfCheckIn = new DateTime(Convert.ToInt32(booking.CheckInYear), Convert.ToInt32(booking.CheckInMonth), Convert.ToInt32(booking.CheckInDay)),
                DateOfCheckOut = new DateTime(Convert.ToInt32(booking.CheckOutYear), Convert.ToInt32(booking.CheckOutMonth), Convert.ToInt32(booking.CheckOutDay)),
            });

            return bookingResult.Id == 0 ? BadRequest(string.Format("Failed to book a room {0}", booking.RoomNumber)) : Ok(bookingResult);
        }
    }
}
