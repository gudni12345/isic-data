﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PagedList;

namespace ISIC_DATA.Models
{
    public class TestMateViewModel
    {
        public Dog Father { get; set; }
        public Dog Mother { get; set; }      
    }
}