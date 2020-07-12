using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IBaseRepository<T> where T : class
    {
        #region Operations

        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task InsertAsync(T item);

        Task UpdateAsync(T item);

        Task DeleteAsync(T item);

        #endregion
    }
}
