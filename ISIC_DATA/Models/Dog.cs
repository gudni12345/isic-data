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
     
        public Dog()
        {
            this.RegisterDog = new HashSet<RegisterDog>();
            this.RegisterDog1 = new HashSet<RegisterDog>();
        }

        public int Id { get; set; }
        [Key]
//        , Column(TypeName = "varchar(15)")]
        public string Reg { get; set; }
   //     [Column(TypeName = "varchar(15)")]
        public string NewReg { get; set; }

      //  [ForeignKey("Color")]
        public int ColorId { get; set; }

       // public virtual List<OLDColor> colors { get; set; }
        public virtual ICollection<RegisterDog> RegisterDog { get; set; }
        public virtual ICollection<RegisterDog> RegisterDog1 { get; set; }
    }
}

