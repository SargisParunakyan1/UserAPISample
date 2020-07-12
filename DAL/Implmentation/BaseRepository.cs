using DAL.Abstract;
using DAL.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Implmentation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Fields

        protected readonly UserContext _applicationContext;

        protected readonly DbSet<T> _dbSet;

        #endregion

        #region Constructor

        public BaseRepository(UserContext context)
        {
            _applicationContext = context;
            _dbSet = context.Set<T>();
        }

        #endregion

        #region Operations

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T item = await _dbSet.FindAsync(id);

            return item;
        }

        public async Task InsertAsync(T item)
        {
            await _dbSet.AddAsync(item);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T item)
        {
            _applicationContext.Entry(item).State = EntityState.Modified;
            await _applicationContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T item)
        {
            _dbSet.Remove(item);
            await _applicationContext.SaveChangesAsync();
        }

        #endregion
    }
}