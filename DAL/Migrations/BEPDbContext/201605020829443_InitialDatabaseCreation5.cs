namespace BEP.DAL.Migrations.BEPDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategorieInformatie", "Afbeelding", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CategorieInformatie", "Afbeelding");
        }
    }
}
