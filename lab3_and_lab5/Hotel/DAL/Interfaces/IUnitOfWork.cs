using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRoomRepository Rooms { get; }
        IBookingRepository Bookings { get; }
        Task CompleteAsync();
        void Dispose();
    }
}
