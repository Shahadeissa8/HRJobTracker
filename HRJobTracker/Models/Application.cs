using System.ComponentModel.DataAnnotations.Schema;

namespace HRJobTracker.Models
{
    public class Application
    {
        public string ApplicationId { get; set; }
        public string ApplicantName { get; set; }
        public string? ApplicantImage { get; set; }
        public string ApplicantCVUrl { get; set; }
        public string ApplicantMajor { get; set; }
        public string ApplicantSkills { get; set; }
        public DateTime ApplicantDOB { get; set; }
        public string Status { get; set; } = "Pending"; // New field
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        [ForeignKey("Job")]
        public string JobId { get; set; }
        public Job Job { get; set; }

    }
}
