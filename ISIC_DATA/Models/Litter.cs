using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISIC_DATA.Models
{
    public class Litter
    {
        public int LitterId { get; set; }
        public string Reg_Mother { get; set; }
        public string Reg_Father { get; set; }

        public virtual List<Dog> dogs { get; set; }
    }
}