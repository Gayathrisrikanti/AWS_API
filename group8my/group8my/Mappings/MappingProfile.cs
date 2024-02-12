using AutoMapper;
using group8my.DTOs;
using group8my.Models;

namespace group8my.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Job Offer Profile
            CreateMap<JobOffers, JobOfferDTO>(); // Read All 
            CreateMap<JobOfferCreateDTO, JobOffers>();
            CreateMap<JobOfferUpdateDTO, JobOffers>();
            CreateMap<JobOffers, JobOfferUpdateDTO>();

            // Job Seeker Profile
            CreateMap<JobSeekers, JobSeekerDTO>();
            CreateMap<JobSeekerCreateDTO, JobSeekers>();
            CreateMap<JobSeekerUpdateDTO, JobSeekers>();
            CreateMap<JobSeekers, JobSeekerUpdateDTO>();

        }
    }
}
