using System.Collections.Generic;
using BEP.BL.Models.Begrotingen;
using System.ComponentModel.DataAnnotations;

namespace BEP.BL.Models.ParticipatieProjecten
{
    public class StartBudget : IValidatableObject
    {
        public int StartBudgetId { get; set; }
        [Required(ErrorMessage = "Het bedrag moet ingevuld zijn")]
        public double Bedrag { get; set; }

        public double MinimumBedrag { get; set; }
        public double MaximumBedrag { get; set; }
        public StartBudgetAanpasbaarheid Aanpasbaarheid { get; set; }
        public CategorieInformatie CategorieInformatie { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (MinimumBedrag > MaximumBedrag)
            {
                string errorMessage = "Het minimumbedrag kan niet groter zijn dan het maximumbedrag";
                errors.Add(new ValidationResult(errorMessage
                    , new string[] { "MinimumBedrag", "MaximumBedrag" }));
            }
            return errors;
        }

        public StartBudget()
        {

        }
    }
}
