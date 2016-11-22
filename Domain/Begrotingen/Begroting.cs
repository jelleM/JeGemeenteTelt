namespace BEP.BL.Models.Begrotingen
{
    public class Begroting : BegrotingsType
    {
        public int BegrotingId { get; set; }

        public Begroting(int jaar) : base(jaar)
        {

        }
        public Begroting() : base()
        {

        }
    }
}
