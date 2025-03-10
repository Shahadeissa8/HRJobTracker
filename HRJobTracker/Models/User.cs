using Microsoft.AspNetCore.Identity;

namespace HRJobTracker.Models
{
    public class User : IdentityUser
    {
        public int HREmployeeId { get; set; }
    }
}
