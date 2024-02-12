using Microsoft.EntityFrameworkCore;

namespace group8my.Models
{
    public partial class ManyJobsContext : DbContext
    {
        public ManyJobsContext(DbContextOptions<ManyJobsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<JobOffers> JobOffer { get; set; }
        public virtual DbSet<JobSeekers> JobSeeker { get; set; }

        // Remove the OnConfiguring method entirely if you're using dependency injection.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobOffers>().HasKey(j => j.JobId);
            modelBuilder.Entity<JobSeekers>().HasKey(j => j.SeekerId);
            // Configure additional entities and relationships here
        }
    }
}
