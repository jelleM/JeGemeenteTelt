using BEP.BL.Models.Gemeentes;
using System.Collections.Generic;

namespace BEP.BL
{
    interface IGemeenteManager
    {
        //Get en Change methodes op Gemeente.cs

        Gemeente GetGemeente(string naam);
        IEnumerable<Gemeente> GetAllGemeentes();
        Gemeente GetGemeente(short postcode);
        void ChangeGemeente(Gemeente gemeente);
        void ChangeImageOfCity(short postcode, string image);
    }
}
