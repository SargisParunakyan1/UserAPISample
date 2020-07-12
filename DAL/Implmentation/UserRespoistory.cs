using DAL.Abstract;
using DAL.DataAccess;
using DAL.Enities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Implmentation
{
    public class UserRespoistory : BaseRepository<User>, IUserRepository
    {
        #region Constructor

        public UserRespoistory(UserContext context) : base(context)
        {

        }

        #endregion

        #region Operations

        public async Task<User> GetByEmailAsync(string email)
        {
            User user = await _dbSet.AsNoTracking().Where(em => em.Email.Equals(email))?.FirstOrDefaultAsync();

            return user;
        }

        #endregion
    }
}