using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISIC_DATA.Models
{
    public class RegisterDogs
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string Reg { get; set; }

        [ForeignKey("Reg")]
        public string Reg_M { get; set; }
        [ForeignKey("Reg")]
        public string Reg_F { get; set; }

        public int LitterId { get; set; }
        public char Sex { get; set; }

        [ForeignKey("AdminId")]
        public int AdminRegDogId { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string name { get; set; }

    }
}