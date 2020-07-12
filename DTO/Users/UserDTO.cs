
using System.ComponentModel.DataAnnotations;

namespace Users.DTO
{
    public class UserDTO
    {
        #region  Properties

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