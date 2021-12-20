using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MOTILab4.Models;

namespace MOTILab4.Data
{
    public class MOTILab4Context : DbContext
    {
        public MOTILab4Context (DbContextOptions<MOTILab4Context> options)
            : base(options)
        {
        }

        public DbSet<MOTILab4.Models.Elector> Elector { get; set; }

        public DbSet<MOTILab4.Models.VotingObject> VotingObject { get; set; }

        public DbSet<MOTILab4.Models.ElectorChoise> ElectorChoise { get; set; }
    }
}
