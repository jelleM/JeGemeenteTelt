using System.Collections.Generic;
using System.Linq;
using BEP.BL.Models.Begrotingen;
using BEP.BL.Models.Gemeentes;
using System.Data.Entity;

namespace BEP.DAL.EF
{
    public class GemeenteRepository : IGemeenteRepository
    {
        private readonly BEPDbContext ctx = new BEPDbContext();
        private List<Categorie> categorien;

        public GemeenteRepository()
        {
            ctx = new BEPDbContext();
            categorien = ctx.Categorieen.ToList();
        }

        public GemeenteRepository(UnitOfWork uow)
        {
            ctx = uow.Context;
        }

        public void CreateGemeente(Gemeente gemeente)
        {
            ctx.Gemeentes.Add(gemeente);
            ctx.SaveChanges();
        }

        public Gemeente ReadGemeente(string naam)
        {
            return ctx.Gemeentes.Include(g => g.ParticipatieProjecten.Select(p => p.Begroting)).Include(gg => gg.BegrotingsTypes.Select(b => b.Acties)).Include(gg => gg.BegrotingsTypes.Select(b => b.CategorieInformaties)).SingleOrDefault(g => g.Naam == naam);
        }

        public Gemeente ReadGemeente(short postcode)
        {
            return ctx.Gemeentes.Include(g => g.ParticipatieProjecten.Select(p => p.Begroting)).Include(gg => gg.BegrotingsTypes.Select(b => b.Acties)).Include(gg => gg.BegrotingsTypes.Select(b => b.CategorieInformaties)).Include(gg => gg.BegrotingsTypes.Select(b => b.CategorieInformaties)).SingleOrDefault(g => g.Postcode == postcode);
        }

        public IEnumerable<Gemeente> ReadGemeentes()
        {
            return ctx.Gemeentes.Include(g => g.ParticipatieProjecten.Select(p => p.Begroting)).Include(gg => gg.BegrotingsTypes.Select(b => b.Acties)).Include(gg => gg.BegrotingsTypes.Select(b => b.CategorieInformaties)).Include(gg => gg.BegrotingsTypes.Select(b => b.CategorieInformaties));
        }

        public void UpdateGemeente(Gemeente gemeente)
        {
            ctx.Entry(gemeente).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void ChangeImage(short zip, string image)
        {
            Gemeente gemeente = ctx.Gemeentes.Single(g => g.Postcode == zip);
            gemeente.Afbeelding = image;
            ctx.SaveChanges();
        }
    }
}
