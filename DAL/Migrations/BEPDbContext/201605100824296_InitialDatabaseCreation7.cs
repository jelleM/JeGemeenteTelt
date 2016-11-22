namespace BEP.DAL.Migrations.BEPDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Gemeente", "Afbeelding", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Gemeente", "Afbeelding", c => c.Binary());
        }
    }
}
