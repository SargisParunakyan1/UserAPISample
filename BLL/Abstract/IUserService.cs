using DomainModels.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IUserService
    {
        #region Operations

        Task<List<UserModel>> GetAllAsync(string category = null);

        Task<UserModel> GetByIdAsync(int id);

        Task InsertAsync(UserModel user);

        Task UpdateAsync(UserModel user);

        Task DeleteAsync(int userId);

        #endregion
    }
}