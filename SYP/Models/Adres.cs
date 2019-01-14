using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SYP.Models
{
    public class Adres
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="İlçe")]
        public string Ilce { get; set; }

        [Required]
        [Display(Name = "Adres Detayı")]
        public string AdresDetay { get; set; }

    }
}