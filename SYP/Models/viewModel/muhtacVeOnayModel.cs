using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYP.Models.viewModel
{
    public class muhtacVeOnayModel
    {
        public List<Muhtac> Muhtacs { get; set; }
        public List<onayCheckModel> onayCheckModels { get; set; }
    }
}