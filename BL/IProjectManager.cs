using BEP.BL.Models.ParticipatieProjecten;
using System;
using System.Collections.Generic;

namespace BEP.BL
{
  interface IProjectManager
  {
    //Methodes likes begrotingsvoorstel
    void RemoveLikesFromBegrotingsvoorstel(string likerid, int begrotingsvoorstelid);
    void AddLikesToBegrotingsvoorstel(string likerid, int begrotingsvoorstelid);
    void VermeerderDraagvlak(int begrotingsvoorstelid);
    void VerminderDraagvlak(int begrotingsvoorstelid);

    //Methodes likes reactie
    void RemoveLikesFromReactie(string likerid, int reactieId);
    void AddLikesToReactie(string likerid, int reactieId);
    void VermeerderDraagvlakReactie(int reactieId);
    void VerminderDraagvlakReactie(int reactieId);

    //Methodes op Begrotingsvoorstel.cs

    Begrotingsvoorstel AddBegrotingsvoorstel(Begrotingsvoorstel voorstel);
    IEnumerable<Begrotingsvoorstel> GetAllBegrotingsvoorstellen();
    IEnumerable<Begrotingsvoorstel> GetAllBegrotingsvoorstellenAndroid();
    Begrotingsvoorstel GetBegrotingsvoorstel(int begrotingsvoorstelid);
    void ChangeBegrotingsvoorstel(Begrotingsvoorstel voorstel);
    void ChangeBegrotingsvoorstelStaatToInBehandeling(int begrotingsvoorstelid);
    void ChangeBegrotingsvoorstelStaatToGoedgekeurd(int begrotingsvoorstelid);
    void ChangeBegrotingsvoorstelStaatToAfgekeurd(int begrotingsvoorstelid);
    void RemoveBegrotingsvoorstel(int begrotingsvoorstelid);

    //Methodes op Budget.cs

    Budget AddBudget(double bedrag, double budgetwijziging);
    IEnumerable<Budget> GetAllBudgetten();
    Budget GetBudget(int budgetid);
    void ChangeBudget(Budget budget);
    void RemoveBudget(int budgetid);

    //Methodes op ParticipatieProject.cs

    ParticipatieProject AddParticipatieProjectToGemeente(ParticipatieProject project, short zip);

    IEnumerable<ParticipatieProject> GetAllParticipatieProjecten();
    ParticipatieProject GetParticipatieProject(int projectid);

    void ChangeParticipatieProject(ParticipatieProject project);
    void RemoveParticipatieProject(int projectid);
    void ChangeProjectTypeToBestemming(int projectid);
    void ChangeProjectTypeToBesparing(int projectid);
    void ChangeProjectTypeToHerschikking(int projectid);

    //Methodes op Reactie.cs

    void AddReactie(string tekst, DateTime datum, string userId, int begrotingsvoorstelId);
    IEnumerable<Reactie> GetAllReacties();
    void ChangeReactie(Reactie reactie);
    void RemoveReactie(int reactieid);
    void AddSubReactie(string tekst, DateTime datum, string userId, int reactieId);

    //Methodes op StartBudget.cs

    StartBudget AddStartBudget(double bedrag, double minimumbedrag, double maximumbedrag, StartBudgetAanpasbaarheid aanpasbaarheid);
    IEnumerable<StartBudget> GetAllStartBudgetten();
    StartBudget GetStartBudget(int startbudgetid);
    void ChangeStartBudget(StartBudget startbudget);
    void ChangeStartBudgetAanpasbaarheidToManueel(int startbudgetid);
    void ChangeStartBudgetAanpasbaarheidToAutomatisch(int startbudgetid);
    void ChangeStartBudgetAanpasbaarheidToGesloten(int startbudgetid);
    void RemoveStartBudget(int startbudgetid);

  }
}
