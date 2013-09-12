using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISIC_DATA.Models
{
    public class Litter
    {
        [Key]
        public int LitterId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<DateTime> DateOfBirth { get; set; }

        public virtual Dog Mother_Id { get; set; }
        public virtual Dog Father_Id { get; set; }

        public Nullable<int> BreederId { get; set; }
        public virtual Breeder Breeder { get; set; }
    }
}

//public string Reg_Mother { get; set; }
//public string Reg_Father { get; set; }

// public virtual List<Dog> dogs { get; set; }