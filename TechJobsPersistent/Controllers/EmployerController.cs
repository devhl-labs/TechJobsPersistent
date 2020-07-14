using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistent.Controllers
{
    public class EmployerController : Controller
    {
        private readonly JobDbContext _jobDbContext;

        public EmployerController(JobDbContext jobDbContext)
        {
            _jobDbContext = jobDbContext;
        }

        public IActionResult Index()
        {
            return View(_jobDbContext.Employers.ToList());
        }

        public IActionResult Add()
        {
            return View(new AddEmployerViewModel());
        }

        [HttpPost]
        public IActionResult Add(AddEmployerViewModel addEmployerViewModel)
        {
            if (ModelState.IsValid == false)
                return View(addEmployerViewModel);

            _jobDbContext.Employers.Add(addEmployerViewModel.ToEmployer());

            _jobDbContext.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult About(int id)
        {
            Employer employer = _jobDbContext.Employers.SingleOrDefault(e => e.Id == id);

            if (employer == null)
                return Redirect("Index");
            else
                return View(employer);
        }
    }
}
