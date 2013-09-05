using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISIC_DATA.Models
{
    public class RegisterDog
    {  
        [Key]
        public string Reg { get; set; }
        public long Id { get; set; }
        public int LitterId { get; set; }
        public char Sex { get; set; }

        public string name { get; set; }
        public virtual Dog Reg_M { get; set; }
        public virtual Dog Reg_F { get; set; }

    }
}

//, Column(TypeName = "varchar(15)")]
//    [ForeignKey("Dog")]
//   public string Reg_M { get; set; }
//   [ForeignKey("Dog1")]
//   public string Reg_F { get; set; }


//     [Column(TypeName = "nvarchar(100)")]
