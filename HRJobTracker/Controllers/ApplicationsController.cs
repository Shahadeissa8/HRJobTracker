using HRJobTracker.Data;
using HRJobTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRJobTracker.Controllers
{
    //https://localhost:5001/api/Applications
    ///api/Account/Register
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ApplicationsController(AppDbContext context)
        {
            _context = context;
        }


        // GET: api/Application/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetApplicationById(string id)
        {
            //var application = await _context.Applicantions.FindAsync(id);

            var application = await _context.Applicantions
                .Include(a => a.Job)  
                .FirstOrDefaultAsync(a => a.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }

            return application;
        }

        //approving a job application
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveApplication(string id)
        {
            var application = await _context.Applicantions.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            application.Status = "Approved";
            await _context.SaveChangesAsync();
            return Ok(application);
        }

        //rejecting an application
        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectApplication(string id)
        {
            var application = await _context.Applicantions.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            application.Status = "Rejected";
            await _context.SaveChangesAsync();
            return Ok(application);
        }

        //show pending applications
        [HttpGet("pending")]
        public IActionResult GetPendingApplications()
        {
            var pendingApplicants = _context.Applicantions
                .Where(a => a.Status == "Pending")
                .ToList();

            var jobs = _context.Jobs.ToList();

            var pending = pendingApplicants.Join(jobs, job => job.JobId, pendingApplicants => pendingApplicants.JobId, (pendingApplicants, jobs) => new
            {
                jobCategory = jobs.Category,
                applicationId = pendingApplicants.ApplicationId,
                applicantName = pendingApplicants.ApplicantName,
                applicantImage = pendingApplicants.ApplicantImage,
                applicantCVUrl = pendingApplicants.ApplicantCVUrl,
                applicantMajor = pendingApplicants.ApplicantMajor,
                applicantSkills = pendingApplicants.ApplicantSkills,
                applicantDOB = pendingApplicants.ApplicantDOB,
                status = pendingApplicants.Status,
                email = pendingApplicants.Email,
                phoneNumber  = pendingApplicants.PhoneNumber,
                jobId = pendingApplicants.JobId

                //"applicantName": "Harry Sasquatch",
                //"applicantImage": "https://static.wikia.nocookie.net/harrypotter/images/c/ce/Harry_Potter_DHF1.jpg/revision/latest/thumbnail/width/360/height/360?cb=20140603201724",
                //"applicantCVUrl": "https://career.oregonstate.edu/sites/career.oregonstate.edu/files/2024-09/environmental_sciences_cv.pdf",
                //"applicantMajor": "Ph.D. in Environmental Sciences",
                //"applicantSkills": "Working knowledge of ArcMap for GIS analysis, Web development experience, including knowledge of HTML, CSS, and JavaScript",
                //"applicantDOB": "1979-09-07T00:00:00",
                //"status": "Pending",
                //"email": "HarrySasquatch@yahoo.com",
                //"phoneNumber": 60006600,
            });


            Console.WriteLine("📡 Request received for pending applications.");
            return Ok(pending);
        }
        //show approved applications
        [HttpGet("approved")]
        public IActionResult GetApprovedApplications()
        {
            var approvedApplicants = _context.Applicantions
                .Where(a => a.Status == "Approved")
                .ToList();
            return Ok(approvedApplicants);
        }
        [HttpGet("archived")]
        public IActionResult GetArchivedApplications()
        {
            var rejectedApplicants = _context.Applicantions
                .Where(a => a.Status == "Rejected")
                .ToList();
            return Ok(rejectedApplicants);
        }



        //[HttpGet("AllApplicants")]
        //public IActionResult AllApplicants()
        //{
        //    var applicants = new List<Application> 
        //    {
        //    new Application
        //    {
        //        ApplicationId = "A",
        //        ApplicantName = "Beaver Lodge",
        //        ApplicantImage = "https://www.gold.ac.uk/media/images-by-section/departments/music/staff/Guy-Baron.jpg",
        //        ApplicantCVUrl= "https://career.oregonstate.edu/sites/career.oregonstate.edu/files/2024-09/two_page_scientific_resume_marine_resource_management.pdf",
        //        ApplicantMajor= "Marine e Resource Management",
        //        ApplicantSkills= "R Studio, ArcGIS, Public Science Education",
        //        ApplicantDOB= DateTime.Now,
        //        JobId="6975d703-19a8-4bf5-81bb-ffd59d09ea77",
        //    },
        //    new Application
        //    {
        //        ApplicationId = "B",
        //        ApplicantName = "Ana M. Banana",
        //        ApplicantImage = "https://www.refinery29.com/images/10267701.jpg",
        //        ApplicantCVUrl= "https://career.oregonstate.edu/sites/career.oregonstate.edu/files/2024-09/phd_cv_-_anthropology_example_brief.pdf",
        //        ApplicantMajor= "Doctor of Philosophy in Applied Anthropology",
        //        ApplicantSkills= "Proficient in Microsoft Office, Atlas.ti, SPSS, SAS, JMP, Adobe Dreamweaver",
        //        ApplicantDOB= DateTime.Now,
        //        JobId= "8cb4c1f6-0679-4591-a7c5-4e4dbaec144d",
        //    },
        //    new Application
        //    {
        //        ApplicationId = "C",
        //        ApplicantName = "Cathy Counselor",
        //        ApplicantImage = "https://m.media-amazon.com/images/M/MV5BMjMzODQzNjk3NF5BMl5BanBnXkFtZTgwOTE5MDI2MDI@._V1_.jpg",
        //        ApplicantCVUrl= "https://career.oregonstate.edu/sites/career.oregonstate.edu/files/2024-09/phd_cv_-_counseling_psychology.pdf",
        //        ApplicantMajor= "Doctorate of Philosophy",
        //        ApplicantSkills= "Microsoft Word",
        //        ApplicantDOB= DateTime.Now,
        //        JobId= "8cb4c1f6-0679-4591-a7c5-4e4dbaec144d",
        //    },
        //    new Application
        //    {
        //        ApplicationId = "D",
        //        ApplicantName = "Harry Sasquatch",
        //        ApplicantImage = "https://static.wikia.nocookie.net/harrypotter/images/c/ce/Harry_Potter_DHF1.jpg/revision/latest/thumbnail/width/360/height/360?cb=20140603201724",
        //        ApplicantCVUrl= "https://career.oregonstate.edu/sites/career.oregonstate.edu/files/2024-09/environmental_sciences_cv.pdf",
        //        ApplicantMajor= "Ph.D. in Environmental Sciences",
        //        ApplicantSkills= "Working knowledge of ArcMap for GIS analysis, Web development experience, including knowledge of HTML, CSS, and JavaScript",
        //        ApplicantDOB= DateTime.Now,
        //        JobId= "6975d703-19a8-4bf5-81bb-ffd59d09ea77",
        //    },
        //    new Application
        //    {
        //        ApplicationId = "E",
        //        ApplicantName = "Jake R. Nelson",
        //        ApplicantImage = "https://a.espncdn.com/combiner/i?img=/i/headshots/nhl/players/full/3042083.png",
        //        ApplicantCVUrl= "https://career.oregonstate.edu/sites/career.oregonstate.edu/files/2024-09/tenure_track_faculty_geography_gis_degree.pdf",
        //        ApplicantMajor= "Public Administration and Policy",
        //        ApplicantSkills= "Public Speaking, Microsoft Power Point",
        //        ApplicantDOB= DateTime.Now,
        //        JobId= "72921322-5acc-4a02-8489-4a534a3b3086",
        //    },
        //    new Application
        //    {
        //        ApplicationId = "F",
        //        ApplicantName = "Ryan N. Contreras",
        //        ApplicantImage = "https://upload.wikimedia.org/wikipedia/commons/1/14/Deadpool_2_Japan_Premiere_Red_Carpet_Ryan_Reynolds_%28cropped%29.jpg",
        //        ApplicantCVUrl= "https://career.oregonstate.edu/sites/career.oregonstate.edu/files/2024-09/osu_tenured_faculty_cv_horticulture.pdf",
        //        ApplicantMajor= "genetic analysis",
        //        ApplicantSkills= "Public Speaking, Microsoft Power Point",
        //        ApplicantDOB= DateTime.Now,
        //        JobId= "6975d703-19a8-4bf5-81bb-ffd59d09ea77",
        //    },
        //    };
        //    return Ok(applicants);
        //}

    }
}
