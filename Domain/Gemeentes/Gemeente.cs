using System.Collections.Generic;
using BEP.BL.Models.Begrotingen;
using BEP.BL.Models.ParticipatieProjecten;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BEP.BL.Models.Gemeentes
{
    [DataContract]
    public class Gemeente
    {
        [DataMember]
        public int GemeenteId { get; set; }
        [Required(ErrorMessage = "De naam moet ingevuld worden.")]
        [DataMember]
        public string Naam { get; set; }
        [DataMember]
        public string Provincie { get; set; }
        [Range(1000, 9999)]
        [DataMember]
        public short Postcode { get; set; }
        [DataMember]
        public string Burgemeester { get; set; }
        [Display(Name = "Aantal inwoners")]
        [DataMember]
        public int AantalInwoners { get; set; }
        [DataMember]
        public double Bevolkingsdichtheid { get; set; }
        [DataMember]
        public bool Deelgemeente { get; set; }
        [DataMember]
        public string Deelgemeentes { get; set; }
        [DataMember]
        public double GemeenteBelasting { get; set; }
        [Display(Name = "Totale oppervlakte")]
        [DataMember]
        public double TotaleOppervlakte { get; set; }
        [Display(Name = "Totaal bestuurszetels")]
        [DataMember]
        public int TotaalBestuursZetels { get; set; }
        [Display(Name = "Aantal vrouwen")]
        [DataMember]
        public double AantalVrouwen { get; set; }
        [Display(Name = "Aantal mannen")]
        [DataMember]
        public double AantalMannen { get; set; }
        [Display(Name = "Aantal 0-17")]
        [DataMember]
        public double AantalKind { get; set; }
        [Display(Name = "Aantal 18-64")]
        [DataMember]
        public double AantalAchtienVierenzestig { get; set; }
        [Display(Name = "Aantal 65+")]
        [DataMember]
        public double AantalVijfenzestigPlus { get; set; }
        [DataMember]
        public string Cluster { get; set; }
        [DataMember]
        public string Afbeelding { get; set; }
        public List<ParticipatieProject> ParticipatieProjecten { get; set; }
        public List<BegrotingsType> BegrotingsTypes { get; set; }

        public Gemeente()
        {
            ParticipatieProjecten = new List<ParticipatieProject>();
            BegrotingsTypes = new List<BegrotingsType>();
        }
    }
}
