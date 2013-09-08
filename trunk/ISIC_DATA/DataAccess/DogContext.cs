using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ISIC_DATA.Models;

namespace ISIC_DATA.DataAccess
{
    public class DogContext : DbContext
    {
        public DbSet<Dog> Dog { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Litter> Litter { get; set; }

        public DbSet<Breeder> Breeder { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<DetailedInfo> DetailedInfo { get; set; }
        public DbSet<Person> Person { get; set; }
    }
}

