using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ISIC_DATA.Models;
using System.ComponentModel.DataAnnotations;

namespace ISIC_DATA.Models
{
    public class LitterDogs
    {
        [Key]
        public Litter Litter { get; set; }
        public List<Dog> Dogs { get; set; }
    }
}


