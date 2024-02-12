using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class JobOffer
    {
        [Key]
        public string JobId { get; set; }

        public string JobName { get; set; }
        public string JobTitle { get; set; }
        public string JobExperience { get; set; }
        public string Skill { get; set; }
        public string JobAddress { get; set; }
        public string JobSalary { get; set; }
    }
}
