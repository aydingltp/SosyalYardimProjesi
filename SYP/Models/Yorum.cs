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

        public int? KullaniciId { get; set; }
        public int? MuhtacId { get; set; }


        [Display(Name = "Yorum Yap")]
        [StringLength(500)]
        public string YorumIcerik { get; set; }
        public DateTime? YorumTarihi { get; set; }
        public Portal Portal { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public virtual Muhtac Muhtac { get; set; }

    }
}