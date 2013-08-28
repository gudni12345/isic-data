using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ISIC_DATA.Models
{
    public class dog
    {
    
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Born { get; set; }
        public string Pedegreenumber { get; set; }
        public string sex { get; set; }
    }
    public class DogDBContext : DbContext
    {
        public DbSet<dog> Dogs { get; set; }
    }
}
