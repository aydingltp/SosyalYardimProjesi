using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string  Resim { get; set; }
        [Required]
        [Display(Name = "İçerik Giriniz")]
        public string Icerik { get; set; }
        public DateTime? EklenmeTarihi { get; set; }
        [Required]
        public Kategori Kategori { get; set; }
        public List<Yorum> Yorum { get; set; }

    }
}