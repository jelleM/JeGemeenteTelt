namespace BEP.DAL.Migrations.BEPDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gemeente", "Afbeelding", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gemeente", "Afbeelding");
        }
    }
}
