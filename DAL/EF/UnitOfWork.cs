namespace BEP.DAL.EF
{
    public class UnitOfWork
  {
    private BEPDbContext ctx;
    internal BEPDbContext Context
    {
      get
      {
        if (ctx == null) ctx = new BEPDbContext(true);
        return ctx;
      }
    }
    public void CommitChanges()
    {
      ctx.CommitChanges();
    }
  }
}
