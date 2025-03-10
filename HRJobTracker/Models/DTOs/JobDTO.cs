using System.ComponentModel.DataAnnotations;

namespace HRJobTracker.Models.DTOs
{
    public class JobDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "Job title is too long.")]
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Salary { get; set; }
        public string Requirements { get; set; }
        public int IsActive { get; set; } = 1;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int DurationInDays { get; set; }
        public string Category { get; set; }
    }
}
