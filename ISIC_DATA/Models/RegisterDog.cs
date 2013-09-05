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
        public long Id { get; set; }
        [Key]
        //, Column(TypeName = "varchar(15)")]
        public string Reg { get; set; }

    //    [ForeignKey("Dog")]
        public string Reg_M { get; set; }
     //   [ForeignKey("Dog1")]
        public string Reg_F { get; set; }

        public int LitterId { get; set; }
        public char Sex { get; set; }

   //     [Column(TypeName = "nvarchar(100)")]
        public string name { get; set; }

        public virtual Dog Dog { get; set; }
        public virtual Dog Dog1 { get; set; }

    }
}

