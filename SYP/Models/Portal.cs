using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SYP.Models
{
    public class Portal
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Başlık")]
        public string Baslik { get; set; }

        [StringLength(500)]
        public string  Resim { get; set; }
        [Required]
        [Display(Name = "İçerik Giriniz")]
        [DataType(DataType.MultilineText)]
        public string Icerik { get; set; }
        public DateTime? EklenmeTarihi { get; set; }

        public List<Yorum> Yorum { get; set; }

    }
}