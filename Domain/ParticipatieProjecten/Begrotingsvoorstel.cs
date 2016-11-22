using System;
using System.Collections.Generic;
using BEP.BL.Models.Begrotingen;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BEP.BL.Models.ParticipatieProjecten
{
  [DataContract]
  public class Begrotingsvoorstel
  {
    [DataMember]
    public int BegrotingsVoorstelId { get; set; }
    [Required]
    [StringLength(500, ErrorMessage = "Het maximum aantal karakters moet kleiner zijn dan 500")]
    [DataMember]
    public string KorteBeschrijving { get; set; }
    [StringLength(1000, ErrorMessage = "Het maximum aantal karakters moet kleiner zijn dan 1000")]
    [DataMember]
    public string LangeBeschrijving { get; set; }
    [DataMember]
    public BegrotingsvoorstelStaat GoedKeuringsStaat { get; set; }
    [DataMember]
    public int Draagvlak { get; set; }
    [DataMember]
    public DateTime Datum { get; set; }
    [DataMember]
    public string Afbeelding { get; set; }
    [DataMember]
    public double TotaalBudgetwijziging { get; set; }
    [DataMember]
    public string Voorsteller { get; set; }
    [DataMember]
    public string UserId { get; set; }
    [DataMember]
    public ICollection<Like> Likes { get; set; }
    [DataMember]
    public ICollection<Reactie> Reacties { get; set; }
    [DataMember]
    public ParticipatieProject ParticipatieProject { get; set; }
    [DataMember]
    public Begroting Begroting { get; set; }
    [DataMember]
    public ICollection<Budget> Budgetten { get; set; }

    public Begrotingsvoorstel()
    {
      Likes = new List<Like>();
      Reacties = new List<Reactie>();
      Budgetten = new List<Budget>();
    }
  }
}
