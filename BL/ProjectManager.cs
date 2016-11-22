using System;
using System.Collections.Generic;
using BEP.DAL;
using BEP.DAL.EF;
using BEP.BL.Models.ParticipatieProjecten;

namespace BEP.BL
{
  public class ProjectManager : IProjectManager
  {
    private readonly IProjectRepository projectrepo;

    public ProjectManager()
    {
      projectrepo = new ProjectRepository();
    }
    public ProjectManager(UnitOfWorkManager uofMgr)
    {
      projectrepo = new ProjectRepository(uofMgr.UnitOfWork);
    }

    public Begrotingsvoorstel AddBegrotingsvoorstel(Begrotingsvoorstel begrotingsVoorstel)
    {
      return projectrepo.CreateBegrotingsvoorstel(begrotingsVoorstel);
    }

    public Budget AddBudget(double bedrag, double budgetwijziging)
    {
      var budget = new Budget
      {
        Bedrag = bedrag,
        BudgetWijziging = budgetwijziging
      };
      return projectrepo.CreateBudget(budget);
    }

    public void AddLikesToBegrotingsvoorstel(string likerid, int begrotingsvoorstelid)
    {
      projectrepo.AddLikesToBegrotingsvoorstel(likerid, begrotingsvoorstelid);
    }

    public void AddLikesToReactie(string likerid, int reactieId)
    {
      projectrepo.AddLikesToReactie(likerid, reactieId);
    }

    public ParticipatieProject AddParticipatieProjectToGemeente(ParticipatieProject project, short zip)
    {

      return projectrepo.CreateParticipatieProject(project, zip);
    }

    public void AddReactie(string tekst, DateTime datum, string userId, int begrotingsvoorstelId)
    {
      var reactie = new Reactie
      {
        Tekst = tekst,
        Datum = datum,
        UserId = userId
      };
      projectrepo.CreateReactie(reactie, begrotingsvoorstelId);
    }

    public StartBudget AddStartBudget(double bedrag, double minimumbedrag, double maximumbedrag, StartBudgetAanpasbaarheid aanpasbaarheid)
    {
      var startbudget = new StartBudget
      {
        Bedrag = bedrag,
        MinimumBedrag = minimumbedrag,
        MaximumBedrag = maximumbedrag,
        Aanpasbaarheid = aanpasbaarheid
      };
      return projectrepo.CreateStartBudget(startbudget);
    }

    public void AddSubReactie(string tekst, DateTime datum, string userId, int reactieId)
    {
      var reactie = new Reactie
      {
        Tekst = tekst,
        Datum = datum,
        UserId = userId
      };
      projectrepo.CreateSubReactie(reactie, reactieId);
    }

    public void ChangeBegrotingsvoorstel(Begrotingsvoorstel voorstel)
    {
      projectrepo.UpdateBegrotingsvoorstel(voorstel);
    }

    public void ChangeBegrotingsvoorstelStaatToAfgekeurd(int begrotingsvoorstelid)
    {
      projectrepo.UpdateBegrotingsvoorstelStaatToAfgekeurd(begrotingsvoorstelid);
    }

    public void ChangeBegrotingsvoorstelStaatToGoedgekeurd(int begrotingsvoorstelid)
    {
      projectrepo.UpdateBegrotingsvoorstelStaatToGoedgekeurd(begrotingsvoorstelid);
    }

    public void ChangeBegrotingsvoorstelStaatToInBehandeling(int begrotingsvoorstelid)
    {
      projectrepo.UpdateBegrotingsvoorstelStaatToInBehandeling(begrotingsvoorstelid);
    }

    public void ChangeBudget(Budget budget)
    {
      projectrepo.UpdateBudget(budget);
    }

    public void ChangeParticipatieProject(ParticipatieProject project)
    {
      projectrepo.UpdateParticipatieProject(project);
    }

    public void ChangeProjectTypeToBesparing(int projectid)
    {
      projectrepo.UpdateProjectTypeToBesparing(projectid);
    }

    public void ChangeProjectTypeToBestemming(int projectid)
    {
      projectrepo.UpdateProjectTypeToBestemming(projectid);
    }

    public void ChangeProjectTypeToHerschikking(int projectid)
    {
      projectrepo.UpdateProjectTypeToHerschikking(projectid);
    }

    public void ChangeReactie(Reactie reactie)
    {
      projectrepo.UpdateReactie(reactie);
    }

    public void ChangeStartBudget(StartBudget startbudget)
    {
      projectrepo.UpdateStartBudget(startbudget);
    }

    public void ChangeStartBudgetAanpasbaarheidToAutomatisch(int startbudgetid)
    {
      projectrepo.UpdateStartBudgetAanpasbaarheidToAutomatisch(startbudgetid);
    }

    public void ChangeStartBudgetAanpasbaarheidToGesloten(int startbudgetid)
    {
      projectrepo.UpdateStartBudgetAanpasbaarheidToGesloten(startbudgetid);
    }

    public void ChangeStartBudgetAanpasbaarheidToManueel(int startbudgetid)
    {
      projectrepo.UpdateStartBudgetAanpasbaarheidToManueel(startbudgetid);
    }

    public IEnumerable<Begrotingsvoorstel> GetAllBegrotingsvoorstellen()
    {
      return projectrepo.ReadBegrotingsvoorstellen();
    }

    public IEnumerable<Begrotingsvoorstel> GetAllBegrotingsvoorstellenAndroid()
    {
      return projectrepo.ReadBegrotingsvoorstellenAndroid();
    }

    public IEnumerable<Budget> GetAllBudgetten()
    {
      return projectrepo.ReadBudgetten();
    }

    public IEnumerable<ParticipatieProject> GetAllParticipatieProjecten()
    {
      return projectrepo.ReadParticipatieProjecten();
    }

    public IEnumerable<Reactie> GetAllReacties()
    {
      return projectrepo.ReadReacties();
    }

    public IEnumerable<StartBudget> GetAllStartBudgetten()
    {
      return projectrepo.ReadStartBudgetten();
    }

    public Begrotingsvoorstel GetBegrotingsvoorstel(int begrotingsvoorstelid)
    {
      return projectrepo.ReadBegrotingsvoorstel(begrotingsvoorstelid);
    }

    public Budget GetBudget(int budgetid)
    {
      return projectrepo.ReadBudget(budgetid);
    }

    public IEnumerable<Like> GetLikesFromBegrotingsvoorstel(int begrotingsvoorstelid)
    {
      return projectrepo.ReadBegrotingsvoorstel(begrotingsvoorstelid).Likes;
    }

    public ParticipatieProject GetParticipatieProject(int projectid)
    {
      return projectrepo.ReadParticipatieProject(projectid);
    }

    public StartBudget GetStartBudget(int startbudgetid)
    {
      return projectrepo.ReadStartBudget(startbudgetid);
    }

    public void RemoveBegrotingsvoorstel(int begrotingsvoorstelid)
    {
      projectrepo.DeleteBegrotingsvoorstel(begrotingsvoorstelid);
    }

    public void RemoveBudget(int budgetid)
    {
      projectrepo.DeleteBudget(budgetid);
    }

    public void RemoveLikesFromBegrotingsvoorstel(string likerid, int begrotingsvoorstelid)
    {
      projectrepo.DeleteLikesFromBegrotingsvoorstel(likerid, begrotingsvoorstelid);
    }

    public void RemoveLikesFromReactie(string likerid, int reactieId)
    {
      projectrepo.DeleteLikesFromReactie(likerid, reactieId);
    }

    public void RemoveParticipatieProject(int projectid)
    {
      projectrepo.DeleteParticipatieProject(projectid);
    }

    public void RemoveReactie(int reactieid)
    {
      projectrepo.DeleteReactie(reactieid);
    }

    public void RemoveStartBudget(int startbudgetid)
    {
      projectrepo.DeleteStartBudget(startbudgetid);
    }

    public void VermeerderDraagvlak(int begrotingsvoorstelid)
    {
      projectrepo.PlusDraagvlak(begrotingsvoorstelid);
    }

    public void VermeerderDraagvlakReactie(int reactieId)
    {
      projectrepo.PlusDraagvlakReactie(reactieId);
    }

    public void VerminderDraagvlak(int begrotingsvoorstelid)
    {
      projectrepo.MinDraagvlak(begrotingsvoorstelid);
    }

    public void VerminderDraagvlakReactie(int reactieId)
    {
      projectrepo.MinDraagvlakReactie(reactieId);
    }
  }
}
