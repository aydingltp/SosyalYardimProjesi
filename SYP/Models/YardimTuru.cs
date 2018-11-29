using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SYP.Models
{
    public class YardimTuru
    {
        [Required]
        public int Id { get; set; }
        
        [Display(Name = "Yardım Türü")]
        public string  YardimTuruAdi { get; set; }

    }
}