using DAL.Enities;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}