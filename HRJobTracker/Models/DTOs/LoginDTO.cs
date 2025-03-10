using System.ComponentModel.DataAnnotations;

namespace HRJobTracker.Models.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Enter Your UserName")]
        [MinLength(7, ErrorMessage = "Invalid UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter Your Password")]
        [DataType(DataType.Password)]
        [MinLength(3, ErrorMessage = "Invalid password")]
        public string Password { get; set; }
        //public bool RememberMe { get; set; }

    }
}
