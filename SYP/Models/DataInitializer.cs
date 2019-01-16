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
                new Kullanici(){KullaniciAdi="admin", Isim="Aydın",Soyisim="Gültepe",Tel="5554440000",Eposta="aydngltp@gmail.com",Sifre="admin",SifreConfirm="admin",Adminmi=true},
                new Kullanici(){KullaniciAdi="erdi34",Isim="Erdi",Soyisim="Gültepe",Tel="5551110000",Eposta="erdigltp@gmail.com",Sifre="a1",SifreConfirm="a1",Adminmi=false},
                new Kullanici(){KullaniciAdi="yusuf65",Isim="Yusuf",Soyisim="Süpürgeci",Tel="5552220000",Eposta="yusuf@gmail.com",Sifre="a2",SifreConfirm="a2",Adminmi=false}
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
                    Aciklama ="Süzen orta okulumuzdaki çocuklar için okuma kitabı gerekiyor.",
                    YardimTuru=context.YardimTurler.FirstOrDefault(i=>i.Id==1),
                    Aciliyet =1,
                    Adres =new Adres{Ilce="merkez",AdresDetay ="ataşehir mah. çankaya sk. no:12"},
                    Il=context.Iller.FirstOrDefault(p=>p.Id==1),
                    MuhtacAdiSoyadi ="Yusuf Ali", AdminOnay=true,
                    Kullanici =context.Kullanicilar.FirstOrDefault(p=>p.Id==2),
                    Okunma=0,
                    EklenmeTarihi=DateTime.Today
                },
                new Muhtac()
                {
                    Baslik ="Acil Maddi Destek",
                    Aciklama ="Faturaların ödenmesi için maddi yardım gerekiyor.",
                    YardimTuru=context.YardimTurler.FirstOrDefault(i=>i.Id==2),
                    Aciliyet =5,
                    Adres =new Adres{Ilce="Keşan",
                    AdresDetay ="süzen mah. elbas sk. no:22"},
                    Il=context.Iller.FirstOrDefault(p=>p.Id==22),
                    MuhtacAdiSoyadi ="Yavuz Emre",
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
                    Adres =new Adres{Ilce="Simav",
                    AdresDetay ="sağlık mah. sanat sk. no:43"},
                    Il=context.Iller.FirstOrDefault(p=>p.Id==43),
                    MuhtacAdiSoyadi ="Ali Kılıç",
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