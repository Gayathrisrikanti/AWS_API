using System;
using System.Collections.Generic;
using group8my.Models;
using System.Linq;

namespace group8my.Services
{
    public class JobOfferRepository : IJobOfferRepository
    {
        private readonly ManyJobsContext _context;

        public JobOfferRepository(ManyJobsContext context)
        {
            _context = context;
        }

        // All data of JobOffer
        public IEnumerable<JobOffers> GetAllJobOffers()
        {
            return _context.JobOffer.ToList();
        }

        // Get data by  JobOffer ID
        public JobOffers GetJobOfferById(string  id)
        {
            return _context.JobOffer.FirstOrDefault(p => p.JobId == id);
        }

        // POST 
        public void CreateJobOffer(JobOffers jobOffer)
        {
            if(jobOffer == null)
            {
                throw new ArgumentNullException(nameof(jobOffer));
            }
            _context.JobOffer.Add(jobOffer);
        }

        //PUT
        public void UpdateJobOffer(JobOffers jobOffer) {
            if (jobOffer == null)
            {
                throw new ArgumentNullException(nameof(jobOffer));
            }
            _context.JobOffer.Update(jobOffer);


        }
   

        // DELETE
        public void DeleteJobOffer(JobOffers jobOffer)
        {
            if (jobOffer == null)
            {
                throw new ArgumentNullException(nameof(jobOffer));
            }
            _context.JobOffer.Remove(jobOffer);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
