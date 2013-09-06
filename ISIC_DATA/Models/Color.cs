using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISIC_DATA.Models
{
    public class Color
    {
        [Key]
        public int Id { get; set; } 
        public string ColorFile { get; set; }
        public string ColorEn { get; set; }
        public string ColorWeb { get; set; }
        public string ColorComment { get; set; }
        public virtual ICollection<Dog> Dog { get; set; }
    }
}

