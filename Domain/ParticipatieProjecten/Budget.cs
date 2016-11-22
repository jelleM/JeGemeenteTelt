using BEP.BL.Models.Begrotingen;
using System.ComponentModel.DataAnnotations;

namespace BEP.BL.Models.ParticipatieProjecten
{
    public class Budget
    {
        public int BudgetId { get; set; }
        [Required(ErrorMessage = "Het veld Bedrag moet ingevuld zijn.")]
        public double Bedrag { get; set; }
        public double BudgetWijziging { get; set; }
        public CategorieInformatie CategorieInformatie { get; set; }
        public Budget()
        {

        }
    }
}
