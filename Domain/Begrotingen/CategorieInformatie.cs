using System.ComponentModel.DataAnnotations;

namespace BEP.BL.Models.Begrotingen
{
    public class CategorieInformatie
    {
        public int CategorieInformatieId { get; set; }
        public string Informatie { get; set; }
        public string Afbeelding { get; set; }
        public string YoutubeLink { get; set; }
        [Required(ErrorMessage = "Het veld Bedrag moet ingevuld zijn")]
        public double Bedrag { get; set; }
        public Categorie Categorie { get; set; }

        public CategorieInformatie(Categorie categorie)
        {
            Bedrag = 0;
            Categorie = categorie;
        }

        public CategorieInformatie()
        {
        }
    }
}
