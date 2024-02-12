using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class JobSeeker
    {
        [Key]
        public string SeekerId { get; set; }

        public string SeekerName { get; set; }
        public string SeekerEmail { get; set; }
        public string SeekerMajor { get; set; }
        public string Skill { get; set; }
        public string SeekerCity { get; set; }
        public string SeekerCountry { get; set; }
    }
}
