namespace BEP.DAL.Migrations.BEPDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CategorieInformatie", "Afbeelding", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CategorieInformatie", "Afbeelding", c => c.Binary());
        }
    }
}
