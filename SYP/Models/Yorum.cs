using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SYP.Models
{
    public class Yorum
    {
        public int Id { get; set; }
        public Kullanici Kullanici { get; set; }
        public Muhtac Muhtac { get; set; }

        [Display(Name = "Yorum Yap")]
        public string YorumIcerik { get; set; }
        public DateTime? YorumTarihi { get; set; }
        public Portal Portal { get; set; }
        


    }
}