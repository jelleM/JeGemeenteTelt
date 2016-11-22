using System;
using System.Collections.Generic;
using System.Linq;
using BEP.BL.Models.Begrotingen;
using System.Data.Entity;

namespace BEP.DAL.EF
{
    public class BegrotingRepository : IBegrotingRepository
    {
        private readonly BEPDbContext ctx = new BEPDbContext();
        public BegrotingRepository()
        {
            ctx = new BEPDbContext();
        }

        public BegrotingRepository(UnitOfWork uow)
        {
            ctx = uow.Context;
        }

        public Actie CreateActie(Actie actie)
        {
            ctx.Acties.Add(actie);
            ctx.SaveChanges();
            return actie;
        }

        public BegrotingsType CreateBegrotingsType(BegrotingsType begrotingstype)
        {
            ctx.BegrotingsTypes.Add(begrotingstype);
            ctx.SaveChanges();
            return begrotingstype;
        }

        public Bestuur CreateBestuur(Bestuur bestuur)
        {
            ctx.Bestuur.Add(bestuur);
            ctx.SaveChanges();
            return bestuur;
        }

        public Categorie CreateCategorie(Categorie categorie)
        {
            ctx.Categorieen.Add(categorie);
            ctx.SaveChanges();
            return categorie;
        }

        public CategorieInformatie CreateCategorieInformatie(CategorieInformatie categorieinformatie)
        {
            ctx.CategorieInformaties.Add(categorieinformatie);
            ctx.SaveChanges();
            return categorieinformatie;
        }

        public void DeleteActie(int actieid)
        {
            Actie actie = ctx.Acties.Find(actieid);
            ctx.Acties.Remove(actie);
            ctx.SaveChanges();
        }

        public void DeleteBegrotingsType(int begrotingNummer)
        {
            BegrotingsType begrotingstype = ctx.BegrotingsTypes.Find(begrotingNummer);
            ctx.BegrotingsTypes.Remove(begrotingstype);
            ctx.SaveChanges();
        }

        public void DeleteBestuur(int bestuurid)
        {
            Bestuur bestuur = ctx.Bestuur.Find(bestuurid);
            ctx.Bestuur.Remove(bestuur);
            ctx.SaveChanges();
        }

        public void DeleteCategorie(int categorienummer)
        {
            Categorie categorie = ctx.Categorieen.Find(categorienummer);
            ctx.Categorieen.Remove(categorie);
            ctx.SaveChanges();
        }

        public void DeleteCategorieInformatie(int categorienummer)
        {
            CategorieInformatie categorieinfo = ctx.CategorieInformaties.Find(categorienummer);
            ctx.CategorieInformaties.Remove(categorieinfo);
            ctx.SaveChanges();
        }

        public Actie ReadActie(int actieid)
        {
            return ctx.Acties.Find(actieid);
        }

        public IEnumerable<Actie> ReadActies()
        {
            return ctx.Acties;
        }

        public BegrotingsType ReadBegrotingsTypeFromGemeente(int jaartal, short postcode)
        {
            return ctx.BegrotingsTypes.Include(bbb => bbb.Acties).Include(bbbb => bbbb.Gemeente).Include(bb => bb.CategorieInformaties.Select(c => c.Categorie)).Include(bbbbbb => bbbbbb.Acties.Select(aa => aa.Bestuur)).SingleOrDefault(bbb => bbb.Gemeente.Postcode == postcode && bbb.Jaartal == jaartal);
        }

        public IEnumerable<BegrotingsType> ReadBegrotingsTypes()
        {
            return ctx.BegrotingsTypes.Include(bb => bb.Acties).Include(bb => bb.CategorieInformaties).Include(bbb => bbb.Gemeente);
        }

        public IEnumerable<Bestuur> ReadBesturen()
        {
            return ctx.Bestuur;
        }

        public Bestuur ReadBestuur(int bestuurid)
        {
            return ctx.Bestuur.Find(bestuurid);
        }

        public Categorie ReadCategorie(int categorienummer)
        {
            return ctx.Categorieen.Find(categorienummer);
        }

        public CategorieInformatie ReadCategorieInformatie(int categorienummer)
        {
            return ctx.CategorieInformaties.Find(categorienummer);
        }

        public IEnumerable<Categorie> ReadCategorieën()
        {
            return ctx.Categorieen.ToList();
        }

        public IEnumerable<CategorieInformatie> ReadInformaties()
        {
            return ctx.CategorieInformaties.Include(info => info.Categorie);
        }

        public void UpdateActie(Actie actie)
        {
            ctx.Entry(actie).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void UpdateActieTermijnToKort(int actieid)
        {
            throw new NotImplementedException();
        }

        public void UpdateActieTermijnToLang(int actieid)
        {
            throw new NotImplementedException();
        }

        public void UpdateBegrotingsType(BegrotingsType begrotingstype)
        {
            ctx.Entry(begrotingstype).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void UpdateBestuur(Bestuur bestuur)
        {
            ctx.Entry(bestuur).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void UpdateCategorie(Categorie categorie)
        {
            ctx.Entry(categorie).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void UpdateCategorieInformatie(CategorieInformatie categorieinformatie)
        {
            ctx.Entry(categorieinformatie).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
