using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ISIC_DATA.Models
{
    public class ISIC_DATAEntities : DbContext
    {
         public DbSet<IsicDog> IsicDogs { get; set; }
         public DbSet<ISDog> ISDogs { get; set; }
    }
}