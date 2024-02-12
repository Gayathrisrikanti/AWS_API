using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using group8my.Models;
using group8my.DTOs;
using group8my.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Diagnostics;


namespace group8my.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobSeekersController : ControllerBase
    {
        private readonly ManyJobsContext _context;
        private IJobSeekerRepository _jobSeekerRepository;
        private readonly IMapper _mapper;


        public JobSeekersController(ManyJobsContext context, IJobSeekerRepository jobSeekerRepository, IMapper mapper)
        {
            _jobSeekerRepository = jobSeekerRepository;
            _mapper = mapper;
            _context = context;
        }
        // GET: api/JobSeeker (get all)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobSeekerDTO>>> GetAllJobSeekers()
        {
            var connectionString = "Data Source =manyjobs.cbd2mddl8saz.ca-central-1.rds.amazonaws.com,1433;Database=ManyJobs;User ID=db8project; Password=password "; // Replace with your actual connection string
            var query = "SELECT * FROM JobSeeker"; // SQL query
            var resultList = new List<JobSeekers>();
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
                                var data = new JobSeekers
                                {

                                    SeekerId = reader.IsDBNull(reader.GetOrdinal("SeekerId")) ? null : reader.GetString(reader.GetOrdinal("SeekerId")),
                                    SeekerName = reader.IsDBNull(reader.GetOrdinal("SeekerName")) ? null : reader.GetString(reader.GetOrdinal("SeekerName")),
                                    SeekerEmail = reader.IsDBNull(reader.GetOrdinal("SeekerEmail")) ? null : reader.GetString(reader.GetOrdinal("SeekerEmail")),
                                    SeekerMajor = reader.IsDBNull(reader.GetOrdinal("SeekerMajor")) ? null : reader.GetString(reader.GetOrdinal("SeekerMajor")),
                                    Skill = reader.IsDBNull(reader.GetOrdinal("Skill")) ? null : reader.GetString(reader.GetOrdinal("Skill")),
                                    SeekerCity = reader.IsDBNull(reader.GetOrdinal("SeekerCity")) ? null : reader.GetString(reader.GetOrdinal("SeekerCity")),
                                    SeekerCountry = reader.IsDBNull(reader.GetOrdinal("SeekerCountry")) ? null : reader.GetString(reader.GetOrdinal("SeekerCountry")),



                                    // Map each column from your table to the corresponding property in YourDataType
                                };
                                resultList.Add(data);
                            }
                        }
                    }
                    catch (Exception ex) { }
                }
            }


            var result = _mapper.Map<IEnumerable<JobSeekerDTO>>(resultList);

            // Return the mapped job offers as an OK (200) response
            return Ok(result);
        }




        // GET: api/JobSeeker/5 (get by id)
        //[Authorize]
        [HttpGet("{id}", Name = "GetJobSeekerById")]
        public async Task<ActionResult<JobSeekerDTO>> GetJobSeekerById(string id)
        {
            var connectionString = "Data Source =manyjobs.cbd2mddl8saz.ca-central-1.rds.amazonaws.com,1433;Database=ManyJobs;User ID=db8project; Password=password "; // Replace with your actual connection string
            var query = "SELECT * FROM JobSeeker WHERE SeekerId = @SeekerId"; // Parameterized SQL query

            JobSeekers jobSeekerData = null;
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SeekerId", id); // Safe parameter binding

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                jobSeekerData = new JobSeekers
                                {


                                    SeekerId = reader.IsDBNull(reader.GetOrdinal("SeekerId")) ? null : reader.GetString(reader.GetOrdinal("SeekerId")),
                                    SeekerName = reader.IsDBNull(reader.GetOrdinal("SeekerName")) ? null : reader.GetString(reader.GetOrdinal("SeekerName")),
                                    SeekerEmail = reader.IsDBNull(reader.GetOrdinal("SeekerEmail")) ? null : reader.GetString(reader.GetOrdinal("SeekerEmail")),
                                    SeekerMajor = reader.IsDBNull(reader.GetOrdinal("SeekerMajor")) ? null : reader.GetString(reader.GetOrdinal("SeekerMajor")),
                                    Skill = reader.IsDBNull(reader.GetOrdinal("Skill")) ? null : reader.GetString(reader.GetOrdinal("Skill")),
                                    SeekerCity = reader.IsDBNull(reader.GetOrdinal("SeekerCity")) ? null : reader.GetString(reader.GetOrdinal("SeekerCity")),
                                    SeekerCountry = reader.IsDBNull(reader.GetOrdinal("SeekerCountry")) ? null : reader.GetString(reader.GetOrdinal("SeekerCountry")),
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

            if (jobSeekerData == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<JobSeekerDTO>(jobSeekerData);
            return Ok(result);
        }
    




        // POST: api/JobSeekers
        [HttpPost]
        public ActionResult<JobSeekerDTO> CreateJobSeeker(JobSeekerCreateDTO jobSeekerCreateDto)
        {
            var jobSeekerModel = _mapper.Map<JobSeekers>(jobSeekerCreateDto);
            _jobSeekerRepository.CreateJobSeeker(jobSeekerModel);
            _jobSeekerRepository.SaveChanges();

            var jobSeekerDto = _mapper.Map<JobSeekerDTO>(jobSeekerModel);

            //return Ok(JobSeekerDTO);
            return CreatedAtRoute(nameof(GetJobSeekerById), new { Id = jobSeekerDto.SeekerId }, jobSeekerDto);
        }
        
       

    




// PUT: api/JobSeekers/{id}
[HttpPut("{id}")]
        public ActionResult UpdateJobSeeker(string id, JobSeekerUpdateDTO jobSeekerUpdateDto)
        {
            
            var jobSeekerModel = _mapper.Map<JobSeekers>(jobSeekerUpdateDto);
            _jobSeekerRepository.UpdateJobSeeker(jobSeekerModel);
            _jobSeekerRepository.SaveChanges();

            return NoContent();
        }




        // DELETE: api/JobOffers/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteJobOfferAsync(string id)
        {
            var connectionString = "Data Source =manyjobs.cbd2mddl8saz.ca-central-1.rds.amazonaws.com,1433;Database=ManyJobs;User ID=db8project; Password=password "; // Replace with your actual connection string
            var query = "SELECT * FROM JobSeeker WHERE SeekerId = @SeekerId"; // Parameterized SQL query

            JobSeekers jobSeekerData = null;
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SeekerId", id); // Safe parameter binding

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                jobSeekerData = new JobSeekers
                                {


                                    SeekerId = reader.IsDBNull(reader.GetOrdinal("SeekerId")) ? null : reader.GetString(reader.GetOrdinal("SeekerId")),
                                    SeekerName = reader.IsDBNull(reader.GetOrdinal("SeekerName")) ? null : reader.GetString(reader.GetOrdinal("SeekerName")),
                                    SeekerEmail = reader.IsDBNull(reader.GetOrdinal("SeekerEmail")) ? null : reader.GetString(reader.GetOrdinal("SeekerEmail")),
                                    SeekerMajor = reader.IsDBNull(reader.GetOrdinal("SeekerMajor")) ? null : reader.GetString(reader.GetOrdinal("SeekerMajor")),
                                    Skill = reader.IsDBNull(reader.GetOrdinal("Skill")) ? null : reader.GetString(reader.GetOrdinal("Skill")),
                                    SeekerCity = reader.IsDBNull(reader.GetOrdinal("SeekerCity")) ? null : reader.GetString(reader.GetOrdinal("SeekerCity")),
                                    SeekerCountry = reader.IsDBNull(reader.GetOrdinal("SeekerCountry")) ? null : reader.GetString(reader.GetOrdinal("SeekerCountry")),
                                };

                            }
                        }
                    }

                    catch (Exception ex) { }
                }
            }
           
            
            _jobSeekerRepository.DeleteJobSeeker(jobSeekerData);
            _jobSeekerRepository.SaveChanges();

            return NoContent();
        }

    }
}






