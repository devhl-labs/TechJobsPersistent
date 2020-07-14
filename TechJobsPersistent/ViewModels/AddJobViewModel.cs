using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        public string Name { get; set; }

        public int EmployerId { get; set; }

        public List<int> SkillId { get; set; } = new List<int>();

        public List<Skill> Skills { get; set; } = new List<Skill>();

        public List<SelectListItem> Employers { get; set; } = new List<SelectListItem>();

        public Job ToJob() => new Job(Name, EmployerId);

        public AddJobViewModel(List<Employer> employers, List<Skill> skills)
        {
            foreach (Employer employer in employers)
                Employers.Add(new SelectListItem(employer.Name, employer.Id.ToString()));

            foreach (Skill skill in skills)
                Skills.Add(skill);
        }

        public AddJobViewModel()
        {

        }
    }
}
