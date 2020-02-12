using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProgrammer.Api.Data.Entities
{
    public class Contexto : DbContext
    {
        public DbSet<Positions> Positions { get; set; }

        public DbSet<Profiles> Profiles { get; set; }

        public DbSet<Employees> Employees { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }
    }
}
