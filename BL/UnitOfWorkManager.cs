using BEP.DAL.EF;

namespace BEP.BL
{
    public class UnitOfWorkManager
    {
        private UnitOfWork uof;
        internal UnitOfWork UnitOfWork
        {
            get
            {
                if (uof == null) uof = new UnitOfWork();
                return uof;
            }
        }
        public void Save()
        {
            UnitOfWork.CommitChanges();
        }
    }
}
