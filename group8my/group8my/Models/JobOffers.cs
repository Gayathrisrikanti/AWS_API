

using System.ComponentModel.DataAnnotations;

namespace group8my.Models
{
    public  class JobOffers
    {
        
        public string JobId { get; set; }
        public string JobName { get; set; }
        public string JobTitle { get; set; }
        public string JobExperience { get; set; }
        public string Skill { get; set; }
        public string JobAddress { get; set; }
        public string JobSalary { get; set; }
    }
}
