namespace BEP.DAL.Migrations.BEPDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actie",
                c => new
                    {
                        ActieId = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false),
                        Informatie = c.String(maxLength: 500),
                        Bedrag = c.Double(nullable: false),
                        Termijn = c.Byte(nullable: false),
                        Bestuur_BestuurId = c.Int(),
                        Categorie_CategorieId = c.Int(),
                        BegrotingsType_BegrotingsTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.ActieId)
                .ForeignKey("dbo.Bestuur", t => t.Bestuur_BestuurId)
                .ForeignKey("dbo.Categorie", t => t.Categorie_CategorieId)
                .ForeignKey("dbo.BegrotingsType", t => t.BegrotingsType_BegrotingsTypeId)
                .Index(t => t.Bestuur_BestuurId)
                .Index(t => t.Categorie_CategorieId)
                .Index(t => t.BegrotingsType_BegrotingsTypeId);
            
            CreateTable(
                "dbo.Bestuur",
                c => new
                    {
                        BestuurId = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false),
                        Type = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.BestuurId);
            
            CreateTable(
                "dbo.Categorie",
                c => new
                    {
                        CategorieId = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false),
                        CategorieNiveau = c.Byte(nullable: false),
                        Categorie_CategorieId = c.Int(),
                    })
                .PrimaryKey(t => t.CategorieId)
                .ForeignKey("dbo.Categorie", t => t.Categorie_CategorieId)
                .Index(t => t.Categorie_CategorieId);
            
            CreateTable(
                "dbo.CategorieInformatie",
                c => new
                    {
                        CategorieInformatieId = c.Int(nullable: false, identity: true),
                        Informatie = c.String(),
                        YoutubeLink = c.String(),
                        Bedrag = c.Double(nullable: false),
                        Categorie_CategorieId = c.Int(),
                        BegrotingsType_BegrotingsTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.CategorieInformatieId)
                .ForeignKey("dbo.Categorie", t => t.Categorie_CategorieId)
                .ForeignKey("dbo.BegrotingsType", t => t.BegrotingsType_BegrotingsTypeId)
                .Index(t => t.Categorie_CategorieId)
                .Index(t => t.BegrotingsType_BegrotingsTypeId);
            
            CreateTable(
                "dbo.BegrotingsType",
                c => new
                    {
                        BegrotingsTypeId = c.Int(nullable: false, identity: true),
                        Jaartal = c.Int(nullable: false),
                        Totaal = c.Double(nullable: false),
                        BegrotingId = c.Int(),
                        RekeningId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Gemeente_GemeenteId = c.Int(),
                    })
                .PrimaryKey(t => t.BegrotingsTypeId)
                .ForeignKey("dbo.Gemeente", t => t.Gemeente_GemeenteId)
                .Index(t => t.Gemeente_GemeenteId);
            
            CreateTable(
                "dbo.Gemeente",
                c => new
                    {
                        GemeenteId = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false),
                        Provincie = c.String(),
                        Postcode = c.Short(nullable: false),
                        Burgemeester = c.String(),
                        AantalInwoners = c.Int(nullable: false),
                        Bevolkingsdichtheid = c.Int(nullable: false),
                        Deelgemeente = c.Boolean(nullable: false),
                        Deelgemeentes = c.String(),
                        GemeenteBelasting = c.Double(nullable: false),
                        TotaleOppervlakte = c.Double(nullable: false),
                        TotaalBestuursZetels = c.Int(nullable: false),
                        AantalVrouwen = c.Double(nullable: false),
                        AantalMannen = c.Double(nullable: false),
                        AantalKind = c.Double(nullable: false),
                        AantalAchtienVierenzestig = c.Double(nullable: false),
                        AantalVijfenzestigPlus = c.Double(nullable: false),
                        Cluster = c.String(),
                    })
                .PrimaryKey(t => t.GemeenteId);
            
            CreateTable(
                "dbo.ParticipatieProject",
                c => new
                    {
                        ParticipatieProjectId = c.Int(nullable: false, identity: true),
                        Titel = c.String(nullable: false, maxLength: 100),
                        Informatie = c.Int(nullable: false),
                        Type = c.Byte(nullable: false),
                        Startmoment = c.DateTime(nullable: false),
                        Eindmoment = c.DateTime(nullable: false),
                        Bedrag = c.Double(nullable: false),
                        Begroting_BegrotingsTypeId = c.Int(),
                        Gemeente_GemeenteId = c.Int(),
                    })
                .PrimaryKey(t => t.ParticipatieProjectId)
                .ForeignKey("dbo.BegrotingsType", t => t.Begroting_BegrotingsTypeId)
                .ForeignKey("dbo.Gemeente", t => t.Gemeente_GemeenteId)
                .Index(t => t.Begroting_BegrotingsTypeId)
                .Index(t => t.Gemeente_GemeenteId);
            
            CreateTable(
                "dbo.Begrotingsvoorstel",
                c => new
                    {
                        BegrotingsVoorstelId = c.Int(nullable: false, identity: true),
                        KorteBeschrijving = c.String(nullable: false, maxLength: 500),
                        LangeBeschrijving = c.String(maxLength: 1000),
                        GoedKeuringsStaat = c.Byte(nullable: false),
                        Draagvlak = c.Int(nullable: false),
                        Datum = c.DateTime(nullable: false),
                        Afbeelding = c.Binary(),
                        TotaalBudgetwijziging = c.Double(nullable: false),
                        Voorsteller = c.String(),
                        UserId = c.String(),
                        Begroting_BegrotingsTypeId = c.Int(),
                        ParticipatieProject_ParticipatieProjectId = c.Int(),
                    })
                .PrimaryKey(t => t.BegrotingsVoorstelId)
                .ForeignKey("dbo.BegrotingsType", t => t.Begroting_BegrotingsTypeId)
                .ForeignKey("dbo.ParticipatieProject", t => t.ParticipatieProject_ParticipatieProjectId)
                .Index(t => t.Begroting_BegrotingsTypeId)
                .Index(t => t.ParticipatieProject_ParticipatieProjectId);
            
            CreateTable(
                "dbo.Budget",
                c => new
                    {
                        BudgetId = c.Int(nullable: false, identity: true),
                        Bedrag = c.Double(nullable: false),
                        BudgetWijziging = c.Double(nullable: false),
                        CategorieInformatie_CategorieInformatieId = c.Int(),
                        Begrotingsvoorstel_BegrotingsVoorstelId = c.Int(),
                    })
                .PrimaryKey(t => t.BudgetId)
                .ForeignKey("dbo.CategorieInformatie", t => t.CategorieInformatie_CategorieInformatieId)
                .ForeignKey("dbo.Begrotingsvoorstel", t => t.Begrotingsvoorstel_BegrotingsVoorstelId)
                .Index(t => t.CategorieInformatie_CategorieInformatieId)
                .Index(t => t.Begrotingsvoorstel_BegrotingsVoorstelId);
            
            CreateTable(
                "dbo.Like",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Begrotingsvoorstel_BegrotingsVoorstelId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Begrotingsvoorstel", t => t.Begrotingsvoorstel_BegrotingsVoorstelId)
                .Index(t => t.Begrotingsvoorstel_BegrotingsVoorstelId);
            
            CreateTable(
                "dbo.Reactie",
                c => new
                    {
                        ReactieId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        Tekst = c.String(nullable: false, maxLength: 1000),
                        Datum = c.DateTime(nullable: false),
                        Draagvlak = c.Int(nullable: false),
                        Reactie_ReactieId = c.Int(),
                        Begrotingsvoorstel_BegrotingsVoorstelId = c.Int(),
                    })
                .PrimaryKey(t => t.ReactieId)
                .ForeignKey("dbo.Reactie", t => t.Reactie_ReactieId)
                .ForeignKey("dbo.Begrotingsvoorstel", t => t.Begrotingsvoorstel_BegrotingsVoorstelId)
                .Index(t => t.Reactie_ReactieId)
                .Index(t => t.Begrotingsvoorstel_BegrotingsVoorstelId);
            
            CreateTable(
                "dbo.ReactieLike",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Reactie_ReactieId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reactie", t => t.Reactie_ReactieId)
                .Index(t => t.Reactie_ReactieId);
            
            CreateTable(
                "dbo.StartBudget",
                c => new
                    {
                        StartBudgetId = c.Int(nullable: false, identity: true),
                        Bedrag = c.Double(nullable: false),
                        MinimumBedrag = c.Double(nullable: false),
                        MaximumBedrag = c.Double(nullable: false),
                        Aanpasbaarheid = c.Byte(nullable: false),
                        CategorieInformatie_CategorieInformatieId = c.Int(),
                        ParticipatieProject_ParticipatieProjectId = c.Int(),
                    })
                .PrimaryKey(t => t.StartBudgetId)
                .ForeignKey("dbo.CategorieInformatie", t => t.CategorieInformatie_CategorieInformatieId)
                .ForeignKey("dbo.ParticipatieProject", t => t.ParticipatieProject_ParticipatieProjectId)
                .Index(t => t.CategorieInformatie_CategorieInformatieId)
                .Index(t => t.ParticipatieProject_ParticipatieProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParticipatieProject", "Gemeente_GemeenteId", "dbo.Gemeente");
            DropForeignKey("dbo.StartBudget", "ParticipatieProject_ParticipatieProjectId", "dbo.ParticipatieProject");
            DropForeignKey("dbo.StartBudget", "CategorieInformatie_CategorieInformatieId", "dbo.CategorieInformatie");
            DropForeignKey("dbo.Reactie", "Begrotingsvoorstel_BegrotingsVoorstelId", "dbo.Begrotingsvoorstel");
            DropForeignKey("dbo.Reactie", "Reactie_ReactieId", "dbo.Reactie");
            DropForeignKey("dbo.ReactieLike", "Reactie_ReactieId", "dbo.Reactie");
            DropForeignKey("dbo.Begrotingsvoorstel", "ParticipatieProject_ParticipatieProjectId", "dbo.ParticipatieProject");
            DropForeignKey("dbo.Like", "Begrotingsvoorstel_BegrotingsVoorstelId", "dbo.Begrotingsvoorstel");
            DropForeignKey("dbo.Budget", "Begrotingsvoorstel_BegrotingsVoorstelId", "dbo.Begrotingsvoorstel");
            DropForeignKey("dbo.Budget", "CategorieInformatie_CategorieInformatieId", "dbo.CategorieInformatie");
            DropForeignKey("dbo.Begrotingsvoorstel", "Begroting_BegrotingsTypeId", "dbo.BegrotingsType");
            DropForeignKey("dbo.ParticipatieProject", "Begroting_BegrotingsTypeId", "dbo.BegrotingsType");
            DropForeignKey("dbo.BegrotingsType", "Gemeente_GemeenteId", "dbo.Gemeente");
            DropForeignKey("dbo.CategorieInformatie", "BegrotingsType_BegrotingsTypeId", "dbo.BegrotingsType");
            DropForeignKey("dbo.Actie", "BegrotingsType_BegrotingsTypeId", "dbo.BegrotingsType");
            DropForeignKey("dbo.Actie", "Categorie_CategorieId", "dbo.Categorie");
            DropForeignKey("dbo.Categorie", "Categorie_CategorieId", "dbo.Categorie");
            DropForeignKey("dbo.CategorieInformatie", "Categorie_CategorieId", "dbo.Categorie");
            DropForeignKey("dbo.Actie", "Bestuur_BestuurId", "dbo.Bestuur");
            DropIndex("dbo.StartBudget", new[] { "ParticipatieProject_ParticipatieProjectId" });
            DropIndex("dbo.StartBudget", new[] { "CategorieInformatie_CategorieInformatieId" });
            DropIndex("dbo.ReactieLike", new[] { "Reactie_ReactieId" });
            DropIndex("dbo.Reactie", new[] { "Begrotingsvoorstel_BegrotingsVoorstelId" });
            DropIndex("dbo.Reactie", new[] { "Reactie_ReactieId" });
            DropIndex("dbo.Like", new[] { "Begrotingsvoorstel_BegrotingsVoorstelId" });
            DropIndex("dbo.Budget", new[] { "Begrotingsvoorstel_BegrotingsVoorstelId" });
            DropIndex("dbo.Budget", new[] { "CategorieInformatie_CategorieInformatieId" });
            DropIndex("dbo.Begrotingsvoorstel", new[] { "ParticipatieProject_ParticipatieProjectId" });
            DropIndex("dbo.Begrotingsvoorstel", new[] { "Begroting_BegrotingsTypeId" });
            DropIndex("dbo.ParticipatieProject", new[] { "Gemeente_GemeenteId" });
            DropIndex("dbo.ParticipatieProject", new[] { "Begroting_BegrotingsTypeId" });
            DropIndex("dbo.BegrotingsType", new[] { "Gemeente_GemeenteId" });
            DropIndex("dbo.CategorieInformatie", new[] { "BegrotingsType_BegrotingsTypeId" });
            DropIndex("dbo.CategorieInformatie", new[] { "Categorie_CategorieId" });
            DropIndex("dbo.Categorie", new[] { "Categorie_CategorieId" });
            DropIndex("dbo.Actie", new[] { "BegrotingsType_BegrotingsTypeId" });
            DropIndex("dbo.Actie", new[] { "Categorie_CategorieId" });
            DropIndex("dbo.Actie", new[] { "Bestuur_BestuurId" });
            DropTable("dbo.StartBudget");
            DropTable("dbo.ReactieLike");
            DropTable("dbo.Reactie");
            DropTable("dbo.Like");
            DropTable("dbo.Budget");
            DropTable("dbo.Begrotingsvoorstel");
            DropTable("dbo.ParticipatieProject");
            DropTable("dbo.Gemeente");
            DropTable("dbo.BegrotingsType");
            DropTable("dbo.CategorieInformatie");
            DropTable("dbo.Categorie");
            DropTable("dbo.Bestuur");
            DropTable("dbo.Actie");
        }
    }
}
