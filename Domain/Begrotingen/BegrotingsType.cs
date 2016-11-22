using System.Collections.Generic;
using BEP.BL.Models.Gemeentes;

namespace BEP.BL.Models.Begrotingen
{
    public class BegrotingsType
    {
        public int BegrotingsTypeId { get; set; }
        public int Jaartal { get; set; }
        public double Totaal { get; set; }
        public ICollection<CategorieInformatie> CategorieInformaties { get; set; }
        public ICollection<Actie> Acties { get; set; }
        public Gemeente Gemeente { get; set; }

        public BegrotingsType(int jaar)
        {
            Jaartal = jaar;
            CategorieInformaties = new List<CategorieInformatie>();
            Acties = new List<Actie>();
        }
        public BegrotingsType()
        {
            CategorieInformaties = new List<CategorieInformatie>();
            Acties = new List<Actie>();
        }
    }
}
