using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using group8my.Models;
using AutoMapper;
using group8my.DTOs;
using group8my.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace group8my.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobOffersController : ControllerBase
    {
        private readonly ManyJobsContext _context;
        private IJobOfferRepository _jobOfferRepository;
        private readonly IMapper _mapper;

        public JobOffersController(ManyJobsContext context,IJobOfferRepository jobOfferRepository, IMapper mapper)
        {
            _jobOfferRepository = jobOfferRepository;
            _mapper = mapper;
            _context = context;
        }

        // GET: api/JobOffers (get all)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobOfferDTO>>> GetAllJobOffers()
        {
            var connectionString = "Data Source =manyjobs.cbd2mddl8saz.ca-central-1.rds.amazonaws.com,1433;Database=ManyJobs;User ID=db8project; Password=password "; // Replace with your actual connection string
            var query = "SELECT * FROM JobOffer"; // SQL query
            var resultList = new List<JobOffers>();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    try
                    {
                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var data = new JobOffers
                                {

                                    JobId = reader.IsDBNull(reader.GetOrdinal("JobId")) ? null : reader.GetString(reader.GetOrdinal("JobId")),
                                    JobName = reader.IsDBNull(reader.GetOrdinal("JobName")) ? null : reader.GetString(reader.GetOrdinal("JobName")),
                                    JobTitle = reader.IsDBNull(reader.GetOrdinal("JobTitle")) ? null : reader.GetString(reader.GetOrdinal("JobTitle")),
                                    JobExperience = reader.IsDBNull(reader.GetOrdinal("JobExperience")) ? null : reader.GetString(reader.GetOrdinal("JobExperience")),
                                    Skill = reader.IsDBNull(reader.GetOrdinal("Skill")) ? null : reader.GetString(reader.GetOrdinal("Skill")),
                                    JobAddress = reader.IsDBNull(reader.GetOrdinal("JobAddress")) ? null : reader.GetString(reader.GetOrdinal("JobAddress")),
                                    JobSalary = reader.IsDBNull(reader.GetOrdinal("JobSalary")) ? null : reader.GetString(reader.GetOrdinal("JobSalary")),



                                    // Map each column from your table to the corresponding property in YourDataType
                                };
                                resultList.Add(data);
                            }
                        }
                    }
                    catch (Exception ex) { }
                }
            }

            // Asynchronously get all job offers from the database
            //var jobOfferItems = await _context.JobOffer.ToListAsync();

            // Use AutoMapper to map the list of JobOffer entities to a list of JobOfferDTOs
            var result = _mapper.Map<IEnumerable<JobOfferDTO>>(resultList);

            // Return the mapped job offers as an OK (200) response
            return Ok(result);
        }

        // GET: api/JobOffers/5 (get by id)
        //[Authorize]
        [HttpGet("{id}", Name = "GetJobOfferById")]
        public async Task<ActionResult<JobOfferDTO>> GetJobOfferById(string id)
        {
            var connectionString = "Data Source =manyjobs.cbd2mddl8saz.ca-central-1.rds.amazonaws.com,1433;Database=ManyJobs;User ID=db8project; Password=password "; // Replace with your actual connection string
            var query = "SELECT * FROM JobOffer WHERE JobId = @JobId"; // Parameterized SQL query

            JobOffers jobOfferData = null;
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@JobId", id); // Safe parameter binding

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                jobOfferData = new JobOffers
                                {
                                   
                                    
                                    JobId = reader.IsDBNull(reader.GetOrdinal("JobId")) ? null : reader.GetString(reader.GetOrdinal("JobId")),
                                    JobName = reader.IsDBNull(reader.GetOrdinal("JobName")) ? null : reader.GetString(reader.GetOrdinal("JobName")),
                                    JobTitle = reader.IsDBNull(reader.GetOrdinal("JobTitle")) ? null : reader.GetString(reader.GetOrdinal("JobTitle")),
                                    JobExperience = reader.IsDBNull(reader.GetOrdinal("JobExperience")) ? null : reader.GetString(reader.GetOrdinal("JobExperience")),
                                    Skill = reader.IsDBNull(reader.GetOrdinal("Skill")) ? null : reader.GetString(reader.GetOrdinal("Skill")),
                                    JobAddress = reader.IsDBNull(reader.GetOrdinal("JobAddress")) ? null : reader.GetString(reader.GetOrdinal("JobAddress")),
                                    JobSalary = reader.IsDBNull(reader.GetOrdinal("JobSalary")) ? null : reader.GetString(reader.GetOrdinal("JobSalary")),
                                    // ... continue mapping other properties
                                };

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle or log the exception
                        return StatusCode(500, "Internal server error");
                    }
                }
            }

            if (jobOfferData == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<JobOfferDTO>(jobOfferData);
            return Ok(result);
        }

        // POST: api/JobOffers
        [HttpPost]
        public ActionResult<JobOfferDTO> CreateJobOffer (JobOfferCreateDTO jobOfferCreateDto)
        {
            var jobOfferModel = _mapper.Map<JobOffers>(jobOfferCreateDto);
            _jobOfferRepository.CreateJobOffer(jobOfferModel);
            _jobOfferRepository.SaveChanges();


            //_context.JobOffer.Add(jobOfferModel);
            //_context.SaveChanges();

            var jobOfferDto = _mapper.Map<JobOfferDTO>(jobOfferModel);

            //return Ok(JobOfferDTO);
            return CreatedAtRoute(nameof(GetJobOfferById), new { Id = jobOfferDto.JobId },jobOfferDto);
        }


        // PUT: api/JobOffers/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateJobOffer(string id, JobOfferUpdateDTO jobOfferUpdateDto)
        {
            ////var jobOfferModelFromRepo = _jobOfferRepository.GetJobOfferById(id);
            ////if(jobOfferModelFromRepo == null)
            //{
            //    return NotFound();
            //}

            // _mapper.Map(jobOfferUpdateDto, jobOfferModelFromRepo);
            var jobOfferModel = _mapper.Map<JobOffers>(jobOfferUpdateDto);
            _jobOfferRepository.UpdateJobOffer(jobOfferModel);
            _jobOfferRepository.SaveChanges();

            return NoContent();
        }


        // DELETE: api/JobOffers/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteJobOfferAsync(string id)
        {
            var connectionString = "Data Source =manyjobs.cbd2mddl8saz.ca-central-1.rds.amazonaws.com,1433;Database=ManyJobs;User ID=db8project; Password=password "; // Replace with your actual connection string
            var query = "SELECT * FROM JobOffer WHERE JobId = @JobId"; // Parameterized SQL query

            JobOffers jobOfferData = null;
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@JobId", id); // Safe parameter binding

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                jobOfferData = new JobOffers
                                {


                                    JobId = reader.IsDBNull(reader.GetOrdinal("JobId")) ? null : reader.GetString(reader.GetOrdinal("JobId")),
                                    JobName = reader.IsDBNull(reader.GetOrdinal("JobName")) ? null : reader.GetString(reader.GetOrdinal("JobName")),
                                    JobTitle = reader.IsDBNull(reader.GetOrdinal("JobTitle")) ? null : reader.GetString(reader.GetOrdinal("JobTitle")),
                                    JobExperience = reader.IsDBNull(reader.GetOrdinal("JobExperience")) ? null : reader.GetString(reader.GetOrdinal("JobExperience")),
                                    Skill = reader.IsDBNull(reader.GetOrdinal("Skill")) ? null : reader.GetString(reader.GetOrdinal("Skill")),
                                    JobAddress = reader.IsDBNull(reader.GetOrdinal("JobAddress")) ? null : reader.GetString(reader.GetOrdinal("JobAddress")),
                                    JobSalary = reader.IsDBNull(reader.GetOrdinal("JobSalary")) ? null : reader.GetString(reader.GetOrdinal("JobSalary")),
                                    // ... continue mapping other properties
                                };

                            }
                        }
                    }

                    catch (Exception ex) { }
                }
                }
            //var jobOfferModelFromRepo = GetJobOfferById(id);
            
            //if (jobOfferModelFromRepo == null)
            //{
            //    return NotFound();
            //}

            _jobOfferRepository.DeleteJobOffer(jobOfferData);
            _jobOfferRepository.SaveChanges();

            return NoContent();
        }

    }
}
