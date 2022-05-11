using DAL.EF;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly HotelDbContext _hotelDbContext;
        public Repository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return _hotelDbContext.Set<TEntity>().ToList();
        }
        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await _hotelDbContext.Set<TEntity>().FindAsync(id);
            return entity;
        }
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            var newEntity = await _hotelDbContext.Set<TEntity>().AddAsync(entity);
            return newEntity.Entity;
        }
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var newEntity =  _hotelDbContext.Set<TEntity>().Update(entity);
            return newEntity.Entity;
        }
        public virtual async Task<TEntity> DeleteByIdAsync(int id)
        {
            var entity = await _hotelDbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
                _hotelDbContext.Set<TEntity>().Remove(entity);
            return entity;
        }
    }
}
