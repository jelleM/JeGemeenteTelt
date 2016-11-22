using System.ComponentModel.DataAnnotations;
namespace BEP.BL.Models.Begrotingen
{
    public class Actie
    {
        public int ActieId { get; set; }
        [Required(ErrorMessage = "Het veld Name moet ingevuld zijn")]
        public string Naam { get; set; }
        public string Informatie { get; set; }
        [Required(ErrorMessage = "Het veld Bedrag moet ingevuld zijn")]
        public double Bedrag { get; set; }
        public ActieTermijn Termijn { get; set; }
        public Bestuur Bestuur { get; set; }

        public Categorie Categorie { get; set; }

        public Actie(string naam, string info, double bedrag)
        {
            Naam = naam;
            Informatie = info;
            Bedrag = bedrag;
        }

        public Actie()
        {

        }

    }
}
