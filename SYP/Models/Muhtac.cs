using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SYP.Models
{
    public class Muhtac
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Başlık")]
        public string Baslik { get; set; }

        [Display(Name = "Adı ve Soyadı")]
        public string MuhtacAdiSoyadi { get; set; }
        
        [Display(Name = "Yardım Türü")]
        public YardimTuru YardimTuru { get; set; } //foreign key

        [Required]
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }

        [Display(Name = "İl")]
        public Il Il { get; set; }  //foreign key
        public Adres Adres { get; set; }  //foreign key

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EklenmeTarihi { get; set; }

        [Range(1, 5,ErrorMessage ="Aciliyet derecesi 1-5 arasında olmalı. 1: Acil Değil 5: Çok Acil")]
        [Display(Name = "Aciliyet")]
        [Required(ErrorMessage = "1-5 arasında olmalı. 1: Acil Değil 5: Çok Acil")]
        public int Aciliyet { get; set; }
        
        public bool YardimYapildimi { get; set; }
        public bool Arsivmi { get; set; }
        public bool AdminOnay { get; set; }
        public int? Okunma { get; set; }

        public Kullanici Kullanici { get; set; }
        public virtual ICollection<YardimDetay> Yardimlar { get; set; }


        //public List<Yorum> Yorum { get; set; }



    }
}