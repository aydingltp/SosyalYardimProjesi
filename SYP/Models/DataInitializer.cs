using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SYP.Models
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {


            string[] illerdizi = {"Adana", "Adıyaman", "Afyon", "Ağrı", "Amasya", "Ankara", "Antalya",
                "Artvin", "Aydın", "Balıkesir", "Bilecik", "Bingöl", "Bitlis", "Bolu", "Burdur", "Bursa",
                "Çanakkale", "Çankırı", "Çorum", "Denizli", "Diyarbakır", "Edirne", "Elazığ", "Erzincan",
                "Erzurum", "Eskişehir", "Gaziantep", "Giresun", "Gümüşhane", "Hakkari", "Hatay", "Isparta",
                "İçel(Mersin)", "İstanbul", "İzmir", "Kars", "Kastamonu", "Kayseri", "Kırklareli", "Kırşehir",
                "Kocaeli", "Konya", "Kütahya", "Malatya", "Manisa", "K.maraş", "Mardin", "Muğla", "Muş",
                "Nevşehir", "Niğde", "Ordu", "Rize", "Sakarya", "Samsun", "Siirt", "Sinop", "Sivas",
                "Tekirdağ", "Tokat", "Trabzon", "Tunceli", "Şanlıurfa", "Uşak", "Van", "Yozgat",
                "Zonguldak", "Aksaray", "Bayburt", "Karaman", "Kırıkkale", "Batman", "Şırnak",
                "Bartın", "Ardahan", "Iğdır", "Yalova", "Karabük", "Kilis", "Osmaniye", "Düzce"};


            for (int i = 0; i < illerdizi.Length; i++)
            {
                String il = illerdizi[i].ToString();
                context.Iller.Add(new Il
                {
                    IlAdi = il
                });

            }
            context.SaveChanges();
            List<Kullanici> kullanici = new List<Kullanici>()
            {
                new Kullanici(){KullaniciAdi="admin", Isim="Aydın",Soyisim="Gültepe",Tel="5541407046",Eposta="aydngltp@gmail.com",Sifre="aa",SifreConfirm="aa",Adminmi=true},
                new Kullanici(){KullaniciAdi="kullanici1",Isim="Ahmet",Soyisim="Gültepe",Tel="123",Eposta="aydngltp@gmail.com",Sifre="a1",SifreConfirm="a1",Adminmi=false},
                new Kullanici(){KullaniciAdi="kullanici2",Isim="Yusuf",Soyisim="Gültepe",Tel="123",Eposta="aydngltp@gmail.com",Sifre="a2",SifreConfirm="a2",Adminmi=false}
            };
            foreach (var item in kullanici)
            {
                context.Kullanicilar.Add(item);
            }

            List<YardimTuru> turler = new List<YardimTuru>()
            {
                new YardimTuru(){YardimTuruAdi="Eğitim"},
                new YardimTuru(){YardimTuruAdi="Maddi"},
                new YardimTuru(){YardimTuruAdi="Sağlık"},
                new YardimTuru(){YardimTuruAdi="Gıda"},
                new YardimTuru(){YardimTuruAdi="Giyim"}
            };
            foreach (var item in turler)
            {
                context.YardimTurler.Add(item);
            }
            context.SaveChanges();

            List<Muhtac> muhtaclar = new List<Muhtac>()
            {
                new Muhtac()
                {
                    Baslik ="Okul",
                    Aciklama ="Çocuklar için okuma kitabı gerekiyor.",
                    YardimTuru=context.YardimTurler.FirstOrDefault(i=>i.Id==1),
                    Aciliyet =1,
                    Adres =new Adres{Ilce="ilce1",AdresDetay ="ataşehir mah. çankaya sk. no:12"},
                    Il=context.Iller.FirstOrDefault(p=>p.Id==1),
                    MuhtacAdiSoyadi ="Muhtac1", AdminOnay=true,
                    Kullanici =context.Kullanicilar.FirstOrDefault(p=>p.Id==2),
                    Okunma=0,
                    EklenmeTarihi=DateTime.Today
                },
                new Muhtac()
                {
                    Baslik ="Acil Maddi Destek",
                    Aciklama ="Maddi yardım gerekiyor.",
                    YardimTuru=context.YardimTurler.FirstOrDefault(i=>i.Id==2),
                    Aciliyet =5,
                    Adres =new Adres{Ilce="ilce2",
                    AdresDetay ="süzen mah. elbas sk. no:22"},
                    Il=context.Iller.FirstOrDefault(p=>p.Id==22),
                    MuhtacAdiSoyadi ="Muhtac2",
                    AdminOnay =true,
                    Kullanici =context.Kullanicilar.FirstOrDefault(p=>p.Id==3),
                    Okunma=0,
                    EklenmeTarihi=DateTime.Today
                },
                new Muhtac()
                {
                    Baslik ="Sağlık Yardımı",
                    Aciklama ="Sağlık kontrolü için yardıma ihtiyacımız var.",
                    YardimTuru=context.YardimTurler.FirstOrDefault(i=>i.Id==3),
                    Aciliyet =5,
                    Adres =new Adres{Ilce="ilce3",
                    AdresDetay ="sağlık mah. sanat sk. no:43"},
                    Il=context.Iller.FirstOrDefault(p=>p.Id==43),
                    MuhtacAdiSoyadi ="Muhtac3",
                    AdminOnay =true,
                    Kullanici =context.Kullanicilar.FirstOrDefault(p=>p.Id==2),
                    Okunma=0,
                    EklenmeTarihi=DateTime.Today
                }

            };
            foreach (var item in muhtaclar)
            {
                context.Muhtaclar.Add(item);
            }



            context.SaveChanges();

            base.Seed(context);
        }

    }
}