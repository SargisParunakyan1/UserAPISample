using DAL.Enities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataAccess
{
    public class UserContext : DbContext
    {
        #region Constructor

        public UserContext(DbContextOptions options) : base(options)
        {

        }

        #endregion

        #region Properties

        public DbSet<User> Users { get; set; }

        #endregion
    }
}