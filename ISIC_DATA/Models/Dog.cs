using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISIC_DATA.Models
{
    public class Dog
    {
        public int Id { get; set; }
        [Key, Column(Order = 1)]
        public string Reg { get; set; }
        public string NewReg { get; set; }
        public string Name { get; set; }

        public int LitterId { get; set; }
        public virtual Litter Litter { get; set; }

        public int ColorId { get; set; }
        public virtual Color Color { get; set; }

    }
}

