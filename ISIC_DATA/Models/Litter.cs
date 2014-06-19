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
   //     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'.'MM'.'yyyy}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date of birth is missing")]
        public Nullable<DateTime> DateOfBirth { get; set; }   // Nullable required for the import.  

        public string Reg_F { get; set; }
        public string Reg_M { get; set; }

        [Required(ErrorMessage = "Mother is required.")]
        public int MotherId { get; set; }
        public virtual Dog Mother { get; set; }

        [Required(ErrorMessage = "Father is required.")]
        public int FatherId { get; set; }
        public virtual Dog Father { get; set; }

        [Required(ErrorMessage = "Breeder is required.")]  // Nullable required for the import.  
        public Nullable<int> PersonId { get; set; }     //Breeder Person
        public virtual Person Person { get; set; }

        public Nullable<int> UsersId { get; set; }  // The user who added the litter
        public virtual Users Users { get; set; } 
    }
}
