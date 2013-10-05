using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISIC_DATA.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public bool Owner { get; set; }
        public bool Breeder { get; set; }

        public Nullable<int> CountryId { get; set; }
        public virtual Country Country { get; set; }
        
    }
}