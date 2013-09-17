using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISIC_DATA.Models
{
    public class Breeder
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}