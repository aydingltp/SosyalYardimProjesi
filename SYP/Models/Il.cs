using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SYP.Models
{
    public class Il
    {
        public int Id { get; set; }
        
        [Display(Name ="İl")]
        public string IlAdi { get; set; }
        
        
    }
}