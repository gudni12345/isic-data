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
   
        [MaxLength(50)]
        public string Reg { get; set; }     // - provider by FCI 
        public string Name { get; set; }
        public string ColorComment { get; set; }
        [MaxLength(1)]
        public string Sex { get; set; }

        public int LitterId { get; set; }
        public virtual Litter Litter { get; set; }

        public Nullable<int> ColorId { get; set; }
        public virtual Color Color { get; set; }

        public Nullable<int> DetailedInfoId { get; set; }
        public virtual DetailedInfo DetailedInfo { get; set; }

        public Nullable<int> PersonId { get; set; }
        public virtual Person Person { get; set; }

        public Nullable<int> CountryId { get; set; }
        public virtual Country Country { get; set; }
        
        

    }
}

