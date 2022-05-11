using DAL.EF;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public readonly HotelDbContext _hotelDbContext;
        public BookingRepository(HotelDbContext dbContext) : base(dbContext)
        {
            _hotelDbContext = dbContext;
        }

    }
}
