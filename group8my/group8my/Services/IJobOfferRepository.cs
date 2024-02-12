using System.Collections.Generic;
using group8my.Models;

namespace group8my.Services
{
    public interface IJobOfferRepository
    {
        bool SaveChanges();
        IEnumerable<JobOffers> GetAllJobOffers();
        JobOffers GetJobOfferById(string JobId);
        void CreateJobOffer(JobOffers jobOffer); //POST
        void UpdateJobOffer(JobOffers jobOffer); //PUT
        void DeleteJobOffer(JobOffers jobOffer);

    }
}
