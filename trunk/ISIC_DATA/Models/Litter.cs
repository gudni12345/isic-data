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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'/'MM'/'yy}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date of birth is missing")]

        public Nullable<DateTime> DateOfBirth { get; set; }   // Nullable required for the import.  

        public string Reg_F { get; set; }
        public string Reg_M { get; set; }

        public int MotherId { get; set; }
        public virtual Dog Mother { get; set; }

        public int FatherId { get; set; }
        public virtual Dog Father { get; set; }

        public Nullable<int> BreederId { get; set; }
        public virtual Breeder Breeder { get; set; }
    }
}
