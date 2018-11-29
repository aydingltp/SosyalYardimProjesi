using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace SYP.Models
{
    public class Kullanici
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "İsim")]
        public string Isim { get; set; }

        [Required]
        public string Soyisim { get; set; }

        [Required(ErrorMessage ="Cep Telefonu Giriniz.")]
        [Display(Name = "Cep Telefonu")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage ="Uygun bir cep telefonu giriniz.")]
        public string Cep { get; set; }

        [Required]
        [Display(Name = "E-Posta")]
        [DataType(DataType.EmailAddress,ErrorMessage = "Geçerli bir E-Posta hesabı giriniz.")]
        [EmailAddress(ErrorMessage = "Geçersiz E-Posta")]
        public string Eposta { get; set; }

        [Required(ErrorMessage ="Şifreyi Giriniz.")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string  Sifre { get; set; }


        [Required]
        [Compare("Sifre",ErrorMessage="Şifreler Uyuşmuyor. Tekrar Deneyiniz...")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre Doğrulama")]
        public string SifreConfirm { get; set; }
        public bool Adminmi { get; set; }
        public virtual List<Muhtac> Muhtaclar { get; set; }
     

    }
}