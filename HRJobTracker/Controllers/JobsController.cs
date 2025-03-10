using HRJobTracker.Data;
using HRJobTracker.Models;
using HRJobTracker.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRJobTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<JobsController> _logger;

        // Inject the DbContext into the controller
        public JobsController(AppDbContext context, ILogger<JobsController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpPost("CreateJob")]
        public async Task<IActionResult> CreateJob([FromBody] JobDTO jobDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Calculate the ClosingDate based on DurationInDays
            DateTime closingDate = DateTime.Now.AddDays(jobDto.DurationInDays);

            // Create a new Job using the JobCreateDTO
            var job = new Job
            {
                JobId = Guid.NewGuid().ToString(), // Generate a new JobId
                Title = jobDto.Title,
                Description = jobDto.Description,
                Salary = jobDto.Salary,
                Requirements = jobDto.Requirements,
                IsActive = jobDto.IsActive,
                CreatedDate = DateTime.Now,  // Current date when job is created
                ClosingDate = closingDate,  // Set the calculated ClosingDate
                Category = jobDto.Category
            };

            // Add the job to the database
            _context.Jobs.Add(job);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the created job
            return CreatedAtAction(nameof(GetJobById), new { id = job.JobId }, job);
        }

        // GET: api/Job/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJobById(string id)
        {
            var job = await _context.Jobs.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }

        [HttpGet("job/AllApplicants/{jobId}")]
        public IActionResult GetApplicantsByJobId(string jobId)
        {
            var applicants = _context.Applicantions
                .Where(a => a.JobId == jobId)
                .ToList();

            if (!applicants.Any())
            {
                return NotFound("No applicants found for this job.");
            }

            return Ok(applicants);
        }

        [HttpGet("active")]
        public IActionResult GetActiveJobs()
        {
            var activeJobs = _context.Jobs
                .Where(j => j.IsActive==1) // Assuming there's an IsActive field
                .ToList();

            if (!activeJobs.Any())
            {
                return NotFound("No active job openings available.");
            }

            return Ok(activeJobs);
        }


        //[HttpGet("AllJobsHardCoded")]
        //public IActionResult AllJobsHardCoded()
        //{
        //    var jobs = new List<Job>
        //    {
        //    new Job
        //    {
        //        JobId ="8cb4c1f6-0679-4591-a7c5-4e4dbaec144d",
        //        Title = "Philosophy Instructor",
        //        Description = "A job opening in KU",
        //        Salary = 2000,
        //        Requirements = "3 years experience in teaching, Masters degree or higher in Philosophy",
        //        IsActive = 1,
        //        CreatedDate = DateTime.Now.AddDays(10),
        //    },
        //    new Job
        //    {
        //        JobId = "6975d703-19a8-4bf5-81bb-ffd59d09ea77",
        //        Title = "Biology Reaserch Manager",
        //        Description = "You will assist and supervise the research team.",
        //        Salary = 1500,
        //        Requirements = "6 years experience in Research Teams,PHD degree in biology or related fields",
        //        IsActive = 1,
        //        CreatedDate = DateTime.Now.AddDays(30),
        //    },
        //    new Job
        //    {
        //        JobId = "72921322-5acc-4a02-8489-4a534a3b3086",
        //        Title = "Reporter",
        //        Description = "Reporting for NBC channel",
        //        Salary = 1500,
        //        Requirements = "4 years experience in the fields, public speaking skills and ability to talk infront of cameras",
        //        IsActive = 1,
        //        CreatedDate = DateTime.Now.AddDays(20),
        //    },

        //    };
        //    return Ok(jobs);
        //}

    }
}