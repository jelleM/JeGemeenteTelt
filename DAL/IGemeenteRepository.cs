using BEP.BL.Models.Gemeentes;
using System.Collections.Generic;

namespace BEP.DAL
{
    public interface IGemeenteRepository
    {
        //Read & Update op Gemeente.cs

        IEnumerable<Gemeente> ReadGemeentes();
        Gemeente ReadGemeente(short postcode);
        Gemeente ReadGemeente(string naam);
        void UpdateGemeente(Gemeente gemeente);
        void ChangeImage(short zip, string image);

        //Create gemeente

        void CreateGemeente(Gemeente gemeente);

    }
}
