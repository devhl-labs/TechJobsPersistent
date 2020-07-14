using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using AspNetCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private readonly JobDbContext _context;

        public HomeController(JobDbContext dbContext)
        {
            _context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = _context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        public IActionResult AddJob()
        {
            return View(new AddJobViewModel(_context.Employers.ToList(), _context.Skills.ToList()));
        }

        [HttpPost]
        public IActionResult AddJob(AddJobViewModel addJobViewModel, string[] selectedSkills)
        {
            if (ModelState.IsValid == false)
                return View(addJobViewModel);

            Job job = addJobViewModel.ToJob();

            foreach (string selectedSkill in selectedSkills)
                job.JobSkills.Add(new JobSkill { SkillId = int.Parse(selectedSkill) });

            _context.Jobs.Add(job);

            _context.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult Detail(int id)
        {
            Job theJob = _context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = _context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
