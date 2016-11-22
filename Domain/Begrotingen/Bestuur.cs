using System.ComponentModel.DataAnnotations;

namespace BEP.BL.Models.Begrotingen
{
    public class Bestuur
    {
        public int BestuurId { get; set; }
        [Required(ErrorMessage = "Het veld Naam moet ingevuld zijn.")]
        public string Naam { get; set; }
        public BestuurType Type { get; set; }

        public Bestuur(string naam, BestuurType type)
        {
            Naam = naam;
            Type = type;
        }

        public Bestuur()
        {

        }
    }
}
