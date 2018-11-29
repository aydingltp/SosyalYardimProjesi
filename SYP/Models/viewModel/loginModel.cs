using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SYP.Models.viewModel
{
    public class loginModel
    {
        [Required(ErrorMessage ="Cep Telefonu Giriniz.")]
        [Phone]
        public String Tel { get; set; }

        
        [Required(ErrorMessage = "Şifreyi Giriniz.")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public String Sifre { get; set; }
    }
}