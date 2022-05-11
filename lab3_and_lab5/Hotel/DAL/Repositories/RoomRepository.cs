using DAL.EF;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public readonly HotelDbContext _hotelDbContext;
        public RoomRepository(HotelDbContext dbContext) : base(dbContext)
        {
            _hotelDbContext = dbContext;
        }
        public override async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _hotelDbContext.Rooms.Include(x => x.RoomType).ToListAsync();
        }
        public override async Task<Room> UpdateAsync(Room entity)
        {
            var element = await _hotelDbContext.Rooms.FindAsync(entity.Id);
            if (element == null)
                throw new Exception("There is no room to update!");
            if (element != null)
            {
                element.Number = entity.Number;
                element.PricePerDay = entity.PricePerDay;
                element.TypeId = entity.TypeId;
                element.RoomType = entity.RoomType; 
            }

            _hotelDbContext.Entry(element).State = EntityState.Modified;

            return element;
        }
    }
}
