namespace BEP.DAL.Migrations.BEPDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Gemeente", "Bevolkingsdichtheid", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Gemeente", "Bevolkingsdichtheid", c => c.Int(nullable: false));
        }
    }
}
