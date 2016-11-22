using System.Collections.Generic;
using System.Linq;
using BEP.BL.Models.ParticipatieProjecten;
using System.Data.Entity;
using BEP.BL.Models.Gemeentes;

namespace BEP.DAL.EF
{
  public class ProjectRepository : IProjectRepository
  {
    private readonly BEPDbContext ctx = new BEPDbContext();
    public ProjectRepository()
    {
      ctx = new BEPDbContext();
    }

    public ProjectRepository(UnitOfWork uow)
    {
      ctx = uow.Context;
    }

    public void AddLikesToBegrotingsvoorstel(string likerid, int begrotingsvoorstelid)
    {
      ctx.Begrotingsvoorstellen.Find(begrotingsvoorstelid).Likes.Add(new Like { UserId = likerid });
      ctx.SaveChanges();
    }

    public void AddLikesToReactie(string likerid, int reactieId)
    {
      Reactie r = ctx.Reacties.Find(reactieId);

      if (r != null)
      {
        r.Likes.Add(new ReactieLike { UserId = likerid });

      }

      ctx.SaveChanges();
    }

    public Begrotingsvoorstel CreateBegrotingsvoorstel(Begrotingsvoorstel voorstel)
    {
      ctx.Begrotingsvoorstellen.Add(voorstel);
      ctx.SaveChanges();
      return voorstel;
    }

    public Budget CreateBudget(Budget budget)
    {
      ctx.Budgetten.Add(budget);
      ctx.SaveChanges();
      return budget;
    }

    public ParticipatieProject CreateParticipatieProject(ParticipatieProject project, short zip)
    {
      Gemeente g = ctx.Gemeentes.SingleOrDefault(gg => gg.Postcode == zip);
      if (g != null)
      {
        g.ParticipatieProjecten.Add(project);
        ctx.Entry(g).State = EntityState.Modified;
        ctx.SaveChanges();
      }
      return project;
    }

    public void CreateReactie(Reactie reactie, int begrotingsvoorstelId)
    {
      Begrotingsvoorstel v = ctx.Begrotingsvoorstellen.Find(begrotingsvoorstelId);
      if (v != null)
      {
        v.Reacties.Add(reactie);
      }
      ctx.SaveChanges();
    }

    public StartBudget CreateStartBudget(StartBudget startbudget)
    {
      ctx.StartBudgetten.Add(startbudget);
      ctx.SaveChanges();
      return startbudget;
    }

    public void CreateSubReactie(Reactie reactie, int reactieId)
    {
      Reactie r = ctx.Reacties.Find(reactieId);
      if (r != null)
      {
        r.SubReacties.Add(reactie);
      }
      ctx.SaveChanges();
    }

    public void DeleteBegrotingsvoorstel(int voorstelId)
    {
      Begrotingsvoorstel voorstel = ctx.Begrotingsvoorstellen.Find(voorstelId);
      ctx.Begrotingsvoorstellen.Remove(voorstel);
      ctx.SaveChanges();
    }

    public void DeleteBudget(int budgetid)
    {
      Budget budget = ctx.Budgetten.Find();
      ctx.Budgetten.Remove(budget);
      ctx.SaveChanges();
    }

    public void DeleteLikesFromBegrotingsvoorstel(string likerid, int begrotingsvoorstelid)
    {

      IQueryable<Begrotingsvoorstel> voorstellen = ctx.Begrotingsvoorstellen.Include(v => v.Likes).AsQueryable();
      Begrotingsvoorstel voorstel = voorstellen.Single(a => a.BegrotingsVoorstelId == begrotingsvoorstelid);

      Like like = voorstel.Likes.Single(l => l.UserId == likerid);
      if (like != null)
      {
        voorstel.Likes.Remove(like);
        ctx.Likes.Remove(like);
      }

      ctx.SaveChanges();
    }

    public void DeleteLikesFromReactie(string likerid, int reactieId)
    {
      Reactie r = ctx.Reacties.Find(reactieId);
      ReactieLike rl = ctx.ReactieLikes.ToList<ReactieLike>().SingleOrDefault(l => l.UserId == likerid);
      if (rl != null)
      {
        r.Likes.Remove(rl);
        ctx.ReactieLikes.Remove(rl);

      }
      ctx.SaveChanges();
    }

    public void DeleteParticipatieProject(int projectid)
    {
      ParticipatieProject project = ctx.ParticipatieProjecten.Find(projectid);
      ctx.ParticipatieProjecten.Remove(project);
      ctx.SaveChanges();
    }

    public void DeleteReactie(int reactieid)
    {
      Reactie reactie = ctx.Reacties.Find();
      ctx.Reacties.Remove(reactie);
      ctx.SaveChanges();
    }

    public void DeleteStartBudget(int startbudgetid)
    {
      StartBudget startbudget = ctx.StartBudgetten.Find();
      ctx.StartBudgetten.Remove(startbudget);
      ctx.SaveChanges();
    }

    public void MinDraagvlak(int begrotingsvoorstelid)
    {
      ctx.Begrotingsvoorstellen.Find(begrotingsvoorstelid).Draagvlak = ctx.Begrotingsvoorstellen.Find(begrotingsvoorstelid).Draagvlak - 1;
      ctx.SaveChanges();
    }

    public void MinDraagvlakReactie(int reactieId)
    {
      ctx.Reacties.Find(reactieId).Draagvlak = ctx.Reacties.Find(reactieId).Draagvlak - 1;
      ctx.SaveChanges();
    }

    public void PlusDraagvlak(int begrotingsvoorstelid)
    {
      ctx.Begrotingsvoorstellen.Find(begrotingsvoorstelid).Draagvlak = ctx.Begrotingsvoorstellen.Find(begrotingsvoorstelid).Draagvlak + 1;
      ctx.SaveChanges();
    }

    public void PlusDraagvlakReactie(int reactieId)
    {
      ctx.Reacties.Find(reactieId).Draagvlak = ctx.Reacties.Find(reactieId).Draagvlak + 1;
      ctx.SaveChanges();
    }

    public Begrotingsvoorstel ReadBegrotingsvoorstel(int begrotingsvoorstelid)
    {
      return ctx.Begrotingsvoorstellen.Include(b => b.ParticipatieProject).Include(bb => bb.Likes).Include(bbbb => bbbb.Reacties.Select(r => r.Likes)).Include(bbbbb => bbbbb.Reacties.Select(rr => rr.SubReacties)).Include(bbbbbbb => bbbbbbb.Budgetten.Select(bud => bud.CategorieInformatie)).Include(bbbbbbbb => bbbbbbbb.Budgetten.Select(budg => budg.CategorieInformatie.Categorie)).Include(beg => beg.Begroting).Single<Begrotingsvoorstel>(bbb => bbb.BegrotingsVoorstelId == begrotingsvoorstelid);
    }

    public IEnumerable<Begrotingsvoorstel> ReadBegrotingsvoorstellen()
    {
      return ctx.Begrotingsvoorstellen.Include(b => b.ParticipatieProject).Include(bb => bb.Likes).Include(bbbb => bbbb.Reacties).Include(bbbbb => bbbbb.Begroting).Include(bbbbbb => bbbbbb.Begroting.Gemeente);
    }

    public IEnumerable<Begrotingsvoorstel> ReadBegrotingsvoorstellenAndroid()
    {
      return ctx.Begrotingsvoorstellen.Include(b => b.ParticipatieProject).Include(bb => bb.Likes).Include(bbbbbb => bbbbbb.Begroting.Gemeente).AsEnumerable();
    }

    public Budget ReadBudget(int budgetid)
    {
      return ctx.Budgetten.Find();
    }

    public IEnumerable<Budget> ReadBudgetten()
    {
      return ctx.Budgetten;
    }

    public ParticipatieProject ReadParticipatieProject(int projectid)
    {
      return ctx.ParticipatieProjecten.Include(p => p.Begroting).Include(ppp => ppp.Begroting.Gemeente).Include(pppp => pppp.StartBudgetten.Select(sb => sb.CategorieInformatie)).Include(pppp => pppp.StartBudgetten.Select(sbb => sbb.CategorieInformatie.Categorie)).SingleOrDefault(pp => pp.ParticipatieProjectId == projectid);
    }

    public IEnumerable<ParticipatieProject> ReadParticipatieProjecten()
    {
      return ctx.ParticipatieProjecten.Include(p => p.Begroting).Include(ppp => ppp.Begroting.Gemeente).Include(ppp => ppp.Begroting.Gemeente).Include(pppp => pppp.StartBudgetten.Select(sb => sb.CategorieInformatie));
    }

    public IEnumerable<Reactie> ReadReacties()
    {
      return ctx.Reacties;
    }

    public StartBudget ReadStartBudget(int startbudgetid)
    {
      return ctx.StartBudgetten.Find();
    }

    public IEnumerable<StartBudget> ReadStartBudgetten()
    {
      return ctx.StartBudgetten;
    }

    public void UpdateBegrotingsvoorstel(Begrotingsvoorstel begrotingsvoorstel)
    {
      ctx.Entry(begrotingsvoorstel).State = System.Data.Entity.EntityState.Modified;
      ctx.SaveChanges();
    }

    public void UpdateBegrotingsvoorstelStaatToAfgekeurd(int begrotingsvoorstelid)
    {
      ctx.Begrotingsvoorstellen.Find(begrotingsvoorstelid).GoedKeuringsStaat = BegrotingsvoorstelStaat.Afgekeurd;
      ctx.SaveChanges();
    }

    public void UpdateBegrotingsvoorstelStaatToGoedgekeurd(int begrotingsvoorstelid)
    {
      ctx.Begrotingsvoorstellen.Find(begrotingsvoorstelid).GoedKeuringsStaat = BegrotingsvoorstelStaat.Goedgekeurd;
      ctx.SaveChanges();
    }

    public void UpdateBegrotingsvoorstelStaatToInBehandeling(int begrotingsvoorstelid)
    {
      ctx.Begrotingsvoorstellen.Find(begrotingsvoorstelid).GoedKeuringsStaat = BegrotingsvoorstelStaat.InBehandeling;
      ctx.SaveChanges();
    }

    public void UpdateBudget(Budget budget)
    {
      ctx.Entry(budget).State = EntityState.Modified;
      ctx.SaveChanges();
    }

    public void UpdateParticipatieProject(ParticipatieProject project)
    {
      ctx.Entry(project).State = EntityState.Modified;
      ctx.SaveChanges();
    }

    public void UpdateProjectTypeToBesparing(int projectid)
    {
      ctx.ParticipatieProjecten.Find(projectid).Type = ProjectType.Besparing;
      ctx.SaveChanges();
    }

    public void UpdateProjectTypeToBestemming(int projectid)
    {
      ctx.ParticipatieProjecten.Find(projectid).Type = ProjectType.Bestemming;
      ctx.SaveChanges();
    }

    public void UpdateProjectTypeToHerschikking(int projectid)
    {
      ctx.ParticipatieProjecten.Find(projectid).Type = ProjectType.Herschikking;
      ctx.SaveChanges();
    }

    public void UpdateReactie(Reactie reactie)
    {
      ctx.Entry(reactie).State = EntityState.Modified;
      ctx.SaveChanges();
    }

    public void UpdateStartBudget(StartBudget startbudget)
    {
      ctx.Entry(startbudget).State = EntityState.Modified;
      ctx.SaveChanges();
    }

    public void UpdateStartBudgetAanpasbaarheidToAutomatisch(int startbudgetid)
    {
      ctx.StartBudgetten.Find(startbudgetid).Aanpasbaarheid = StartBudgetAanpasbaarheid.Automatisch;
      ctx.SaveChanges();
    }

    public void UpdateStartBudgetAanpasbaarheidToGesloten(int startbudgetid)
    {
      ctx.StartBudgetten.Find(startbudgetid).Aanpasbaarheid = StartBudgetAanpasbaarheid.Gesloten;
      ctx.SaveChanges();
    }

    public void UpdateStartBudgetAanpasbaarheidToManueel(int startbudgetid)
    {
      ctx.StartBudgetten.Find(startbudgetid).Aanpasbaarheid = StartBudgetAanpasbaarheid.Manueel;
      ctx.SaveChanges();
    }
  }
}
