using System.ComponentModel.DataAnnotations;

namespace HRJobTracker.Models.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Enter Your Name")]
        [MinLength(7, ErrorMessage = "Invalid UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter Your Password")]
        [DataType(DataType.Password)]
        [MinLength(3, ErrorMessage = "Invalid password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Enter Your Employee ID")]
        public int HREmployeeId { get; set; }
    }
}
