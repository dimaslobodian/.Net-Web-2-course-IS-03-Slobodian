using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using Itenso.TimePeriod;

namespace BLL.Services
{
    public class BookingService : IBookingService
    {
        public IUnitOfWork _unitOfWork;
        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Room>> GetFreeRoomsInPeriod(DateTime dateTime1, DateTime dateTime2)
        {
            TimeInterval userPeriod = new(dateTime1, dateTime2);

            var ids = (await _unitOfWork.Bookings.GetAllAsync()).ToList()
                .Where(x => new TimeInterval(x.DateOfCheckIn.Value, x.DateOfCheckOut.Value).OverlapsWith(userPeriod))
                .Select(q => q.RoomId);

            List<Room> bookedRooms = (await _unitOfWork.Rooms.GetAllAsync()).ToList().Join(ids,
                roomId => roomId.Id,
                bookingRoomId => bookingRoomId,
                (roomId, bookingRoomId) => new Room
                {
                    Id = bookingRoomId,
                    Number = roomId.Number,
                    TypeId = roomId.TypeId,
                    PricePerDay = roomId.PricePerDay,
                    RoomType = roomId.RoomType,
                }).ToList();

            List<Room> freeRooms = (List<Room>)(await _unitOfWork.Rooms.GetAllAsync()).Except(bookedRooms).ToList();

            return freeRooms;
        }

        public async Task<Booking> CreateAsync(Booking entity)
        {
            try
            {
                if (entity.DateOfCheckIn > entity.DateOfCheckOut || (entity.DateOfCheckIn < DateTime.Now
                    || entity.DateOfCheckOut < DateTime.Now))
                    throw new ArgumentException("Check-in date must be lower that check-out");
                List<Booking> bookings = ((List<Booking>)await _unitOfWork.Bookings.GetAllAsync())
                    .Where(x => x.RoomId == entity.RoomId).Select(x => x).ToList();

                if(bookings.Count == 0)
                {
                    await _unitOfWork.Bookings.CreateAsync(entity);
                    await _unitOfWork.CompleteAsync();
                    return entity;
                }

                TimeInterval entityTimeInterval = new (
                    entity.DateOfCheckIn.Value, entity.DateOfCheckOut.Value);

                foreach (var item in bookings)
                {
                    TimeInterval itemTimeInterval = new(
                        item.DateOfCheckIn.Value, item.DateOfCheckOut.Value);
                    if (entityTimeInterval.OverlapsWith(itemTimeInterval))
                        throw new Exception(string.Format("Sorry, this room is not free on that period : {0}", entityTimeInterval));
                }
                await _unitOfWork.Bookings.CreateAsync(entity);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return entity;
        }

        public async Task<Booking> DeleteByIdAsync(int id)
        {
            Booking booking = null;
            try
            {
                booking = await _unitOfWork.Bookings.DeleteByIdAsync(id);
                if (booking == null)
                    throw new ArgumentException("There is no booking with such id!");
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return booking;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            IEnumerable<Booking> bookings = null;
            try
            {
                bookings = await _unitOfWork.Bookings.GetAllAsync();
                if (!bookings.Any())
                    throw new Exception("There is no bookings in hotel!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return bookings;
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            Booking booking = null;
            try
            {
                booking = await _unitOfWork.Bookings.GetByIdAsync(id);
                if (booking == null)
                    throw new ArgumentException("There is no booking with such id!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return booking;
        }

        public async Task<Booking> UpdateAsync(Booking entity)
        {
            try
            {
                if (entity.DateOfCheckIn > entity.DateOfCheckOut)
                    throw new ArgumentException("Check-in date must be lower that check-out");
                await _unitOfWork.Bookings.UpdateAsync(entity);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return entity;
        }
    }
}
