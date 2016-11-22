using BEP.BL.Models.Begrotingen;
using System.Collections.Generic;

namespace BEP.BL
{
    public interface IBegrotingManager
    {

        void GetDataVanExcel(string fileName);

        //Methodes op BegrotingsType.cs
        BegrotingsType AddBegrotingsType(BegrotingsType begrotingsType);
        IEnumerable<BegrotingsType> GetAllBegrotingsTypes();
        void ChangeBegrotingsType(BegrotingsType begrotingstype);
        void RemoveBegrotingsType(int begrotingNummer);
        BegrotingsType GetBegrotingsTypeFromGemeente(int jaartal, short postcode);

        //Methodes op Actie.cs
        Actie AddActie(string actienaam, string informatie, double bedrag, BegrotingsType begrotingstype);
        IEnumerable<Actie> GetAllActies();
        Actie GetActie(int actieid);
        void ChangeActie(Actie actie);
        void RemoveActie(int actieid);
        void ChangeActieTermijnToKort(int actieid);
        void ChangeActieTermijnToLang(int actieid);

        //Methodes op Bestuur.cs        
        Bestuur AddBestuur(Bestuur bestuur);
        IEnumerable<Bestuur> GetAllBesturen();
        Bestuur GetBestuur(int bestuurid);
        void ChangeBestuur(Bestuur bestuur);
        void RemoveBestuur(int bestuurid);

        //Methodes op Categorie.cs
        Categorie AddCategorie(string naam, CategorieNiveau categorieniveau);
        IEnumerable<Categorie> GetAllCategorieën();
        Categorie GetCategorie(int categorienummer);
        void ChangeCategorie(Categorie categorie);
        void RemoveCategorie(int categorienummer);

        //Methodes op CategorieInformatie.cs
        CategorieInformatie AddCategorieInformatie(string informatie, string afbeelding, string youtubelink, double bedrag);
        IEnumerable<CategorieInformatie> GetAllInformaties();
        CategorieInformatie GetCategorieInformatie(int categorienummer);
        void ChangeCategorieInformatie(CategorieInformatie informatie);
        void RemoveCategorieInformatie(int categorienummer);
    }
}
