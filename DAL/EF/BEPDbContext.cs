using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BEP.BL.Models.Begrotingen;
using BEP.BL.Models.Gemeentes;
using BEP.BL.Models.ParticipatieProjecten;

namespace BEP.DAL.EF
{

    [DbConfigurationType(typeof(BEPDbConfiguration))]
    internal class BEPDbContext : DbContext
    {
        private readonly bool delaySave;
        public BEPDbContext(bool unitOfWorkPresent = false) : base("BEPDB-EF")
        {
            Database.SetInitializer(
          new MigrateDatabaseToLatestVersion<BEPDbContext, Migrations.BEPDbContext.Configuration>("BEPDB-EF"));
            delaySave = unitOfWorkPresent;
        }

        public BEPDbContext() : base("BEPDB-EF")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BEPDbContext, Migrations.BEPDbContext.Configuration>("BEPDB-EF"));
            delaySave = false;
        }
        public override int SaveChanges()
        {
            if (delaySave) return -1;
            return base.SaveChanges();
        }

        internal int CommitChanges()
        {
            if (delaySave) return base.SaveChanges();
            throw new InvalidOperationException("No UnitOfWork present, use SaveChanges instead");
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        //DbSet op Begrotingen

        public DbSet<BegrotingsType> BegrotingsTypes { get; set; }
        public DbSet<Actie> Acties { get; set; }
        public DbSet<Categorie> Categorieen { get; set; }
        public DbSet<CategorieInformatie> CategorieInformaties { get; set; }
        public DbSet<Rekening> Rekeningen { get; set; }
        public DbSet<Bestuur> Bestuur { get; set; }

        //DbSet op Gemeentes

        public DbSet<Gemeente> Gemeentes { get; set; }

        //DbSet op ParticipatieProjecten

        public DbSet<Begrotingsvoorstel> Begrotingsvoorstellen { get; set; }
        public DbSet<Budget> Budgetten { get; set; }
        public DbSet<ParticipatieProject> ParticipatieProjecten { get; set; }
        public DbSet<Reactie> Reacties { get; set; }
        public DbSet<StartBudget> StartBudgetten { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<ReactieLike> ReactieLikes { get; set; }
    }
}
