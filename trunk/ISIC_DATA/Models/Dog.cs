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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(50), MinLength(4, ErrorMessage = "Reg number is required.")]
        public string Reg { get; set; }     // - provided by FCI 

        [Required(ErrorMessage="Name is required.")]
        public string Name { get; set; }

        public string ColorComment { get; set; }

        public string PicturePath { get; set; }

        [Required, MaxLength(10)]
        public string Sex { get; set; }

        public Nullable<int> LitterId { get; set; }
        public virtual Litter Litter { get; set; }

        public Nullable<int> ColorId { get; set; }
        public virtual Color Color { get; set; }

        public Nullable<int> DetailedInfoId { get; set; }
        public virtual DetailedInfo DetailedInfo { get; set; }

        public Nullable<int> PersonId { get; set; }
        public virtual Person Person { get; set; }

        public Nullable<int> BornInCountryId { get; set; }
        public virtual Country BornInCountry { get; set; }

        public Nullable<int> LivesInCountryId { get; set; }
        public virtual Country LivesInCountry { get; set; }

        public virtual ICollection<Litter> Father { get; set; }
        public virtual ICollection<Litter> Mother { get; set; }

    }
}

