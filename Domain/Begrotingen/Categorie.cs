using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BEP.BL.Models.Begrotingen
{
    public class Categorie
    {
        public int? CategorieId { get; set; }
        [Required(ErrorMessage = "Het veld Naam moet ingevuld zijn.")]
        public string Naam { get; set; }
        public CategorieNiveau CategorieNiveau { get; set; }
        public ICollection<Categorie> SubCategorieen { get; set; }

        public Categorie()
        {

        }
    }
}
