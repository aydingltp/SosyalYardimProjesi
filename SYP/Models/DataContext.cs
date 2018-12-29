using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SYP.Models
{
    public class DataContext :DbContext
    {
        public DataContext():base("SYP")
        {
            Database.SetInitializer(new DataInitializer());
        }

        public DbSet<Muhtac> Muhtaclar { get; set; }
        public DbSet<Il> Iller { get; set; }
        public DbSet<Adres> Adresler { get; set; }
        public DbSet<Portal> Portallar { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }
        public DbSet<YardimTuru> YardimTurler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }




    }
}