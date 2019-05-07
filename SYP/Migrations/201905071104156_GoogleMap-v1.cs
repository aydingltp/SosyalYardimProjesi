namespace SYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GoogleMapv1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Muhtacs", "GoogleMap", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Muhtacs", "GoogleMap");
        }
    }
}
