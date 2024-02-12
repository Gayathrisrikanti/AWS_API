
using System.ComponentModel.DataAnnotations;

namespace group8my.DTOs
{
    public class JobSeekerCreateDTO
    {
        public string SeekerId { get; set; }
        public string SeekerName { get; set; }
        public string SeekerEmail { get; set; }
        public string SeekerMajor { get; set; }
        public string Skill { get; set; }
        public string SeekerCity { get; set; }
        public string SeekerCountry { get; set; }
    }
}
