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
        public DbSet<RegisterDog> RegisterDog { get; set; }
        public DbSet<Dog> Dog { get; set; }
        public DbSet<Color> Color { get; set; }
    }
}

