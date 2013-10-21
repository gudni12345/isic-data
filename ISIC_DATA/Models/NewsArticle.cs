﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ISIC_DATA.Models
{
    public class NewsArticle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'.'MM'.'yyyy}")]
        public DateTime Date { get; set; }

        public string Title { get; set; }
                        
        public bool Valid { get; set; }
        
        public Nullable <int> AdminId { get; set; }
        public virtual Person Person { get; set; }

        public Nullable<int> DogId { get; set; }
        public virtual Dog Dog { get; set; }

       [UIHint("tinymce_full"), AllowHtml]
        public string Content { get; set; }

        
    }
}