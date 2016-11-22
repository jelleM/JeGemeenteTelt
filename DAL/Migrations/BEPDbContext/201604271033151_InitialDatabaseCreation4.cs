namespace BEP.DAL.Migrations.BEPDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Actie", "Informatie", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Actie", "Informatie", c => c.String(maxLength: 500));
        }
    }
}
