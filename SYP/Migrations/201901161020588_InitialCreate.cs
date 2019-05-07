namespace SYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ilce = c.String(nullable: false),
                        AdresDetay = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ils",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IlAdi = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kullanicis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullaniciAdi = c.String(nullable: false),
                        Isim = c.String(nullable: false),
                        Soyisim = c.String(nullable: false),
                        Tel = c.String(nullable: false),
                        Eposta = c.String(nullable: false),
                        Sifre = c.String(nullable: false),
                        SifreConfirm = c.String(nullable: false),
                        Adminmi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Muhtacs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false),
                        MuhtacAdiSoyadi = c.String(),
                        Aciklama = c.String(nullable: false),
                        EklenmeTarihi = c.DateTime(),
                        Aciliyet = c.Int(nullable: false),
                        YardimYapildimi = c.Boolean(nullable: false),
                        Arsivmi = c.Boolean(nullable: false),
                        AdminOnay = c.Boolean(nullable: false),
                        Okunma = c.Int(),
                        Adres_Id = c.Int(),
                        Il_Id = c.Int(),
                        Kullanici_Id = c.Int(),
                        YardimTuru_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adres", t => t.Adres_Id)
                .ForeignKey("dbo.Ils", t => t.Il_Id)
                .ForeignKey("dbo.Kullanicis", t => t.Kullanici_Id)
                .ForeignKey("dbo.YardimTurus", t => t.YardimTuru_Id)
                .Index(t => t.Adres_Id)
                .Index(t => t.Il_Id)
                .Index(t => t.Kullanici_Id)
                .Index(t => t.YardimTuru_Id);
            
            CreateTable(
                "dbo.YardimDetays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        YapilanYardim = c.String(maxLength: 500),
                        Onay = c.Boolean(nullable: false),
                        Tarih = c.DateTime(),
                        KullaniciId = c.Int(),
                        MuhtacId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kullanicis", t => t.KullaniciId)
                .ForeignKey("dbo.Muhtacs", t => t.MuhtacId)
                .Index(t => t.KullaniciId)
                .Index(t => t.MuhtacId);
            
            CreateTable(
                "dbo.YardimTurus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        YardimTuruAdi = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Portals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false),
                        Resim = c.String(maxLength: 500),
                        Icerik = c.String(nullable: false),
                        EklenmeTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Yorums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullaniciId = c.Int(),
                        MuhtacId = c.Int(),
                        YorumIcerik = c.String(maxLength: 500),
                        YorumTarihi = c.DateTime(),
                        Onay = c.Boolean(nullable: false),
                        Portal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kullanicis", t => t.KullaniciId)
                .ForeignKey("dbo.Muhtacs", t => t.MuhtacId)
                .ForeignKey("dbo.Portals", t => t.Portal_Id)
                .Index(t => t.KullaniciId)
                .Index(t => t.MuhtacId)
                .Index(t => t.Portal_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Yorums", "Portal_Id", "dbo.Portals");
            DropForeignKey("dbo.Yorums", "MuhtacId", "dbo.Muhtacs");
            DropForeignKey("dbo.Yorums", "KullaniciId", "dbo.Kullanicis");
            DropForeignKey("dbo.Muhtacs", "YardimTuru_Id", "dbo.YardimTurus");
            DropForeignKey("dbo.YardimDetays", "MuhtacId", "dbo.Muhtacs");
            DropForeignKey("dbo.YardimDetays", "KullaniciId", "dbo.Kullanicis");
            DropForeignKey("dbo.Muhtacs", "Kullanici_Id", "dbo.Kullanicis");
            DropForeignKey("dbo.Muhtacs", "Il_Id", "dbo.Ils");
            DropForeignKey("dbo.Muhtacs", "Adres_Id", "dbo.Adres");
            DropIndex("dbo.Yorums", new[] { "Portal_Id" });
            DropIndex("dbo.Yorums", new[] { "MuhtacId" });
            DropIndex("dbo.Yorums", new[] { "KullaniciId" });
            DropIndex("dbo.YardimDetays", new[] { "MuhtacId" });
            DropIndex("dbo.YardimDetays", new[] { "KullaniciId" });
            DropIndex("dbo.Muhtacs", new[] { "YardimTuru_Id" });
            DropIndex("dbo.Muhtacs", new[] { "Kullanici_Id" });
            DropIndex("dbo.Muhtacs", new[] { "Il_Id" });
            DropIndex("dbo.Muhtacs", new[] { "Adres_Id" });
            DropTable("dbo.Yorums");
            DropTable("dbo.Portals");
            DropTable("dbo.YardimTurus");
            DropTable("dbo.YardimDetays");
            DropTable("dbo.Muhtacs");
            DropTable("dbo.Kullanicis");
            DropTable("dbo.Ils");
            DropTable("dbo.Adres");
        }
    }
}
