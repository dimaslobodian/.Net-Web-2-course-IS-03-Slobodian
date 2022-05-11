using DAL.EF;
using DAL.Interfaces;
using DAL.Repositories;

namespace DAL.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly HotelDbContext _hotelDbContext;

        public IRoomRepository Rooms { get; private set; }

        public IBookingRepository Bookings { get; private set; }

        public UnitOfWork(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;

            Rooms = new RoomRepository(_hotelDbContext);
            Bookings = new BookingRepository(_hotelDbContext);
        }

        public async Task CompleteAsync()
        {
            await _hotelDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _hotelDbContext.Dispose();
        }
    }
}
