using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Room> CreateAsync(Room entity)
        {
            try
            {
                List<Room> roomList = (List<Room>)await _unitOfWork.Rooms.GetAllAsync();
                Room room = roomList.Where(r => r.Number == entity.Number).FirstOrDefault();

                if (room != null)
                    throw new Exception("Room already exist in hotel!");

                if (entity.TypeId == 1)
                    entity.PricePerDay = 250;
                else if (entity.TypeId == 2)
                    entity.PricePerDay = 550;
                else if (entity.TypeId == 3)
                    entity.PricePerDay = 750;

                await _unitOfWork.Rooms.CreateAsync(entity);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return entity;
        }

        public async Task<Room> DeleteByIdAsync(int id)
        {
            Room result = null;
            try
            {
                result = await _unitOfWork.Rooms.DeleteByIdAsync(id);
                if (result == null)
                    throw new Exception("There is no room with such id!");
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            IEnumerable<Room> result = null;
            try
            {
                result = await _unitOfWork.Rooms.GetAllAsync();
                if (!result.Any())
                    throw new Exception("There is no registered room in hotel! Add at least one!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            Room result = null;
            try
            {
                result = await _unitOfWork.Rooms.GetByIdAsync(id);
                if (result == null)
                    throw new ArgumentException("There is no room with such id!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public async Task<Room> UpdateAsync(Room entity)
        {
            try
            {

                if (entity.TypeId == 1)
                    entity.PricePerDay = 250;
                else if (entity.TypeId == 2)
                    entity.PricePerDay = 550;
                else if (entity.TypeId == 3)
                    entity.PricePerDay = 750;

                var result = await _unitOfWork.Rooms.UpdateAsync(entity);

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
