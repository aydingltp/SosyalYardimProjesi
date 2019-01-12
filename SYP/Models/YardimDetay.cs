using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SYP.Models
{
    public class YardimDetay
    {
        public int Id { get; set; }

        [Display(Name = "Yardım Yap")]
        [StringLength(500)]
        public string  YapilanYardim { get; set; }
        public bool Onay { get; set; }
        public DateTime? Tarih { get; set; }
        public int? KullaniciId { get; set; }
        public int? MuhtacId { get; set; }

        public virtual Kullanici Kullanici { get; set; }
        public virtual Muhtac Muhtac { get; set; }
    }
}