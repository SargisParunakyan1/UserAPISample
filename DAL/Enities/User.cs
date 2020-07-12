
using System.ComponentModel.DataAnnotations;

namespace DAL.Enities
{
    public class User
    {
        #region Properties

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        #endregion
    }
}
