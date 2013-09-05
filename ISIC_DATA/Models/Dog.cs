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
     
    /*    public Dog()
        {
            this.Reg_M = new HashSet<RegisterDog>();
            this.Reg_F = new HashSet<RegisterDog>();
        }
        */
        public int Id { get; set; }
        [Key]
        public string Reg { get; set; }
        public string NewReg { get; set; }
        public int ColorId { get; set; }

        public virtual ICollection<RegisterDog> Reg_M { get; set; }
        public virtual ICollection<RegisterDog> Reg_F { get; set; }
    }
}

// public virtual List<OLDColor> colors { get; set; }

//  [ForeignKey("Color")]
//      [Column(TypeName = "varchar(15)")]