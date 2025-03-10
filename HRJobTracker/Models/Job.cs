using System.ComponentModel.DataAnnotations.Schema;

namespace HRJobTracker.Models
{
    public class Job
    {
        public string JobId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Salary { get; set; }
        public string Requirements { get; set; }
        public int IsActive { get; set; } = 1;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ClosingDate { get; set; }
        public string Category { get; set; }
    }
}
