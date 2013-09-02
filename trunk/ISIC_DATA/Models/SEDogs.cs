using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISIC_DATA.Models
{
    public class SEDogs
    {
        public int ID { get; set; }
        public string Today_reg { get; set; }
        public string Reg { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public DateTime Born { get; set; }
        public DateTime Date_Estimated { get; set; }
        public string Reg_F { get; set; }
        public string Father { get; set; }
        public string Reg_M { get; set; }
        public string Mother { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string Color { get; set; }
        public string Breeder { get; set; }
        public string Owner { get; set; }
        public string HD { get; set; }
        public string HD2 { get; set; }
        public double Inbreeding { get; set; }
        
    }
}