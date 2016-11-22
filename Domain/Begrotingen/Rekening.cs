namespace BEP.BL.Models.Begrotingen
{
    public class Rekening : BegrotingsType
    {
        public int RekeningId { get; set; }

        public Rekening(int jaar) : base(jaar)
        {

        }

        public Rekening() : base()
        {

        }

    }
}
