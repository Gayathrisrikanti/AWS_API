using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Client.Models;

namespace Client.Data
{
    public class ClientContext : DbContext
    {
        public ClientContext (DbContextOptions<ClientContext> options)
            : base(options)
        {
        }

        public DbSet<Client.Models.JobOffer> JobOffer { get; set; }

        public DbSet<Client.Models.JobSeeker> JobSeeker { get; set; }
    }
}
