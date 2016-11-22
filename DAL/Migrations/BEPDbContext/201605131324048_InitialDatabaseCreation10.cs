namespace BEP.DAL.Migrations.BEPDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Begrotingsvoorstel", "Afbeelding", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Begrotingsvoorstel", "Afbeelding", c => c.Binary());
        }
    }
}
