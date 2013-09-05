using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISIC_DATA.Models
{
    public class Color
    {

        public Color()
        {
            this.Dog = new HashSet<Dog>();
        }

        [Key]
        public int Id { get; set; }
      //  [Column(TypeName = "nvarchar(40)")]
        public string ColorFile { get; set; }
    //    [Column(TypeName = "varchar(40)")]
        public string ColorEn { get; set; }
     //   [Column(TypeName = "varchar(40)")]
        public string ColorWeb { get; set; }
    //    [Column(TypeName = "nvarchar(255)")]
        public string ColorComment { get; set; }

        public virtual ICollection<Dog> Dog { get; set; }
    }
}
