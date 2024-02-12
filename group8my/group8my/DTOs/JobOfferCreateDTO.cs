

using System.ComponentModel.DataAnnotations;

namespace group8my.DTOs
{
    public class JobOfferCreateDTO
    {
        
        public string JobId { get; set; } //g
        public string JobName { get; set; }
        public string JobTitle { get; set; }
        public string JobExperience { get; set; }
        public string Skill { get; set; }
        public string JobAddress { get; set; }
        public string JobSalary { get; set; }
    }
}
