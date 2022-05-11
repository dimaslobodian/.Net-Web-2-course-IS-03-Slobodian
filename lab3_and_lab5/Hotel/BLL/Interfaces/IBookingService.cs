using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBookingService : IService<Booking>
    {
        public Task<IEnumerable<Room>> GetFreeRoomsInPeriod(DateTime dateTime1, DateTime dateTime2);
    }
}
