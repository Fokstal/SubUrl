using Microsoft.EntityFrameworkCore;
using SubUrl.Data;
using SubUrl.Models.Entities;

namespace SubUrl.Repositories
{
    public abstract class DataRepositoryBase<TEntity, TDTO>
        where TEntity : Entity
        where TDTO : class
    {
        protected readonly AppDbContext _db;

        public DataRepositoryBase(AppDbContext db)
        {
            _db = db;
        }

        public virtual async Task<TEntity[]> GetListAsync()
            => await _db.Set<TEntity>().ToArrayAsync();

        public virtual async Task<TEntity?> GetByIdAsync(int id)
            => await _db.Set<TEntity>().FirstOrDefaultAsync(v => v.Id == id);

        public abstract TEntity CreateAsync(TDTO valueDTO);

        public virtual async Task AddAsync(TEntity valueToAdd)
        {
            await _db.Set<TEntity>().AddAsync(valueToAdd);

            await _db.SaveChangesAsync();
        }

        public abstract Task UpdateAsync(TEntity valueToUpdate, TDTO valueDTO);

        public virtual async Task RemoveAsync(TEntity valueToRemove)
        {
            _db.Set<TEntity>().Remove(valueToRemove);

            await _db.SaveChangesAsync();
        }
    }
}