using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISIC_DATA.Models
{
    public class DetailedInfo
    {
        [Key]
        public int Id { get; set; }
        public string OldColor { get; set; }
        public String Eyes { get; set; }
        public string Comment { get; set; }
        public string Hair { get; set; }
        
        [MaxLength(1)]
        public string HD { get; set; }

        [MaxLength(1)]
        public string HD2 { get; set; }

        public string RS { get; set; }
        public string MT { get; set; }
        public string AD { get; set; }
        public string HT { get; set; }
        public string NewReg { get; set; }
    }

}