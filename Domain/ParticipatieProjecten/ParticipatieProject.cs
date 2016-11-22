using System;
using System.Collections.Generic;
using BEP.BL.Models.Begrotingen;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BEP.BL.Models.ParticipatieProjecten
{
  public class ParticipatieProject : IValidatableObject
  {
    public int ParticipatieProjectId { get; set; }
    [Required(ErrorMessage = "Het veld Titel moet ingevuld zijn")]
    [StringLength(100, ErrorMessage = "De titel mag niet langer zijn dan 50 karakters")]
    public string Titel { get; set; }
    public string Informatie { get; set; }
    [Required(ErrorMessage = "Het projecttype moet ingevuld zijn")]
    public ProjectType Type { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "Het startmoment moet aangegeven zijn")]
    public DateTime? Startmoment { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "Het eindmoment moet aangegeven zijn")]
    public DateTime? Eindmoment { get; set; }
    [Required(ErrorMessage = "Het bedrag moet ingevuld zijn")]
    public double Bedrag { get; set; }
    public Begroting Begroting { get; set; }

    [IgnoreDataMember]
    public ICollection<Begrotingsvoorstel> Begrotingsvoorstellen { get; set; }
    public ICollection<StartBudget> StartBudgetten { get; set; }

    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
      var errors = new List<ValidationResult>();

      if (Startmoment >= Eindmoment)
      {
        string errorMessage = "Het startmoment mag niet na het eindmoment komen.";
        errors.Add(new ValidationResult(errorMessage
            , new string[] { "Startmoment", "Eindmoment" }));
      }
      return errors;
    }

    public ParticipatieProject()
    {
      Begrotingsvoorstellen = new List<Begrotingsvoorstel>();
      StartBudgetten = new List<StartBudget>();
    }

  }
}
