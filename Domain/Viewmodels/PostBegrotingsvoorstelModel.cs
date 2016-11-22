namespace BEP.BL.Models
{
    public class PostBegrotingsvoorstelModel
    {
        public int BegrotingsVoorstelId { get; set; }
        public string KorteBeschrijving { get; set; }
        public string LangeBeschrijving { get; set; }
        public int GoedKeuringsStaat { get; set; }
        public int Draagvlak { get; set; }
        public string Afbeelding { get; set; }
        public int TotaalBudgetwijziging { get; set; }
        public int Postcode { get; set; }
        public int ParticipatieProjectId { get; set; }
        public int begrotingJaartal { get; set; }
        public int[] Budgetten { get; set; }
    }
}
