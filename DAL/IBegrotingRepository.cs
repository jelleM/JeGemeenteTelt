using BEP.BL.Models.Begrotingen;
using System.Collections.Generic;

namespace BEP.DAL
{
    public interface IBegrotingRepository
    {
        //CRUD op BegrotingsType.cs
        BegrotingsType CreateBegrotingsType(BegrotingsType begrotingstype);
        IEnumerable<BegrotingsType> ReadBegrotingsTypes();
        void UpdateBegrotingsType(BegrotingsType begrotingstype);
        void DeleteBegrotingsType(int jaartal);
        BegrotingsType ReadBegrotingsTypeFromGemeente(int jaartal, short postcode);

        //CRUD op Actie.cs
        Actie CreateActie(Actie actie);
        IEnumerable<Actie> ReadActies();
        Actie ReadActie(int actieid);
        void UpdateActie(Actie actie);
        void DeleteActie(int actieid);
        void UpdateActieTermijnToKort(int actieid);
        void UpdateActieTermijnToLang(int actieid);

        //CRUD op Bestuur.cs
        Bestuur CreateBestuur(Bestuur bestuur);
        IEnumerable<Bestuur> ReadBesturen();
        Bestuur ReadBestuur(int bestuurid);
        void UpdateBestuur(Bestuur bestuur);
        void DeleteBestuur(int bestuurid);

        //CRUD op Categorie.cs
        Categorie CreateCategorie(Categorie categorie);
        IEnumerable<Categorie> ReadCategorieën();
        Categorie ReadCategorie(int categorienummer);
        void UpdateCategorie(Categorie categorie);
        void DeleteCategorie(int categorienummer);

        //CRUD op CategorieInformatie.cs
        CategorieInformatie CreateCategorieInformatie(CategorieInformatie categorieinformatie);
        IEnumerable<CategorieInformatie> ReadInformaties();
        CategorieInformatie ReadCategorieInformatie(int categorienummer);
        void UpdateCategorieInformatie(CategorieInformatie categorieinformatie);
        void DeleteCategorieInformatie(int categorienummer);
    }
}
