using group8my.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace group8my.Services
{
    public class JobSeekerRepository : IJobSeekerRepository
    {
        private readonly ManyJobsContext _context;

        public JobSeekerRepository(ManyJobsContext context)
        {
            _context = context;
        }

        // All data of JobSeeker
        public IEnumerable<JobSeekers> GetAllJobSeekers()
        {
            return _context.JobSeeker.ToList();
        }



        // Get data by JobSeeker ID
        public JobSeekers GetJobSeekerById(String id)
        {
            return _context.JobSeeker.FirstOrDefault(p => p.SeekerId == id);
        }

        // POST 
        public void CreateJobSeeker(JobSeekers jobSeeker)
        {
            if (jobSeeker == null)
            {
                throw new ArgumentNullException(nameof(jobSeeker));
            }
            _context.JobSeeker.Add(jobSeeker);
        }

        //PUT
        public void UpdateJobSeeker(JobSeekers JobSeeker)
        {
            if (JobSeeker == null)
            {
                throw new ArgumentNullException(nameof(JobSeeker));
            }
            _context.JobSeeker.Update(JobSeeker);


        }

     

        //DELETE
        public void DeleteJobSeeker(JobSeekers jobSeeker)
        {
            if (jobSeeker == null)
            {
                throw new ArgumentNullException(nameof(jobSeeker));
            }
            _context.JobSeeker.Remove(jobSeeker);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
