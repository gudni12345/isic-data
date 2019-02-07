using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using PagedList;

namespace ISIC_DATA.Models
{
    public class Dog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
   //     [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required, MaxLength(50), MinLength(4, ErrorMessage = "Reg number is required.")]
        public string Reg { get; set; }     // - provided by FCI 

        [Required(ErrorMessage="Name is required.")]
        public string Name { get; set; }

        public string ColorComment { get; set; }

        public string PicturePath { get; set; }

        [Required(ErrorMessage = "Gender is required."), MaxLength(10)]
        public string Sex { get; set; }

        public Nullable<int> LitterId { get; set; }
        public virtual Litter Litter { get; set; }

        public Nullable<int> ColorId { get; set; }
        public virtual Color Color { get; set; }

        public Nullable<int> PersonId { get; set; }
        public virtual Person Person { get; set; } 

        public Nullable<int> BornInCountryId { get; set; }
        public virtual Country BornInCountry { get; set; }

        public Nullable<int> LivesInCountryId { get; set; }
        public virtual Country LivesInCountry { get; set; }

        public virtual ICollection<Litter> Father { get; set; }
        public virtual ICollection<Litter> Mother { get; set; }
        
        public String Eyes { get; set; }
        public string Comment { get; set; }
        public string Gender { get; set; }
        public string Hair { get; set; }

        [MaxLength(20)]
        public string HD { get; set; }

        [MaxLength(20)]
        public string HD2 { get; set; }

        public string MT { get; set; }
        public string NewReg { get; set; }

        public Nullable<float> Inbreeding { get; set; }
        public Nullable<float> Size { get; set; }

    }
}

