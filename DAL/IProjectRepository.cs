using BEP.BL.Models.ParticipatieProjecten;
using System.Collections.Generic;

namespace BEP.DAL
{
  public interface IProjectRepository
  {
    //CRUD + Begrotingsstaat wijziging op Begrotingsvoorstel.cs
    Begrotingsvoorstel CreateBegrotingsvoorstel(Begrotingsvoorstel voorstel);
    IEnumerable<Begrotingsvoorstel> ReadBegrotingsvoorstellen();
    IEnumerable<Begrotingsvoorstel> ReadBegrotingsvoorstellenAndroid();
    Begrotingsvoorstel ReadBegrotingsvoorstel(int begrotingsvoorstelid);
    void UpdateBegrotingsvoorstel(Begrotingsvoorstel begrotingsvoorstel);
    void UpdateBegrotingsvoorstelStaatToInBehandeling(int begrotingsvoorstelid);
    void UpdateBegrotingsvoorstelStaatToGoedgekeurd(int begrotingsvoorstelid);
    void UpdateBegrotingsvoorstelStaatToAfgekeurd(int begrotingsvoorstelid);
    void DeleteBegrotingsvoorstel(int begrotingsvoorstelid);

    //CRUD op Budget.cs
    Budget CreateBudget(Budget budget);
    IEnumerable<Budget> ReadBudgetten();
    Budget ReadBudget(int budgetId);
    void UpdateBudget(Budget budget);
    void DeleteBudget(int budgetId);

    //CRUD + Typewijziging op ParticipatieProject.cs
    ParticipatieProject CreateParticipatieProject(ParticipatieProject project, short zip);
    IEnumerable<ParticipatieProject> ReadParticipatieProjecten();
    ParticipatieProject ReadParticipatieProject(int projectid);
    void UpdateParticipatieProject(ParticipatieProject project);
    void DeleteParticipatieProject(int projectid);
    void UpdateProjectTypeToBestemming(int projectid);
    void UpdateProjectTypeToBesparing(int projectid);
    void UpdateProjectTypeToHerschikking(int projectid);

    //CRUD op Reactie.cs
    void CreateReactie(Reactie reactie, int begrootingsvoorstelId);
    IEnumerable<Reactie> ReadReacties();
    void UpdateReactie(Reactie reactie);
    void DeleteReactie(int reactieid);
    void CreateSubReactie(Reactie reactie, int reactieId);

    //CRUD + Aanpasbaarheid wijziging op StartBudget.cs
    StartBudget CreateStartBudget(StartBudget startbudget);
    IEnumerable<StartBudget> ReadStartBudgetten();
    StartBudget ReadStartBudget(int startbudgetid);
    void UpdateStartBudget(StartBudget startbudget);
    void UpdateStartBudgetAanpasbaarheidToManueel(int startbudgetid);
    void UpdateStartBudgetAanpasbaarheidToAutomatisch(int startbudgetid);
    void UpdateStartBudgetAanpasbaarheidToGesloten(int startbudgetid);
    void DeleteStartBudget(int startbudgetid);

    //Extra functionaliteiten nodig voor liken
    void AddLikesToBegrotingsvoorstel(string likerid, int begrotingsvoorstelid);
    void DeleteLikesFromBegrotingsvoorstel(string likerid, int begrotingsvoorstelid);
    void PlusDraagvlak(int begrotingsvoorstelid);
    void MinDraagvlak(int begrotingsvoorstelid);

    //Extra functionaliteiten nodig voor liken van reactie
    void AddLikesToReactie(string likerid, int reactieId);
    void DeleteLikesFromReactie(string likerid, int reactieId);
    void PlusDraagvlakReactie(int reactieId);
    void MinDraagvlakReactie(int reactieId);

  }
}
