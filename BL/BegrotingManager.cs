using System;
using System.Collections.Generic;
using System.Linq;
using BEP.BL.Models.Begrotingen;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using BEP.DAL;
using BEP.DAL.EF;
using BEP.BL.Models.Gemeentes;
using System.IO;

namespace BEP.BL
{
    public class BegrotingManager : IBegrotingManager
    {
        private readonly IBegrotingRepository begrotingrepo;
        UnitOfWorkManager uowMgr = new UnitOfWorkManager();
        public BegrotingManager()
        {
            begrotingrepo = new BegrotingRepository();
        }
        public BegrotingManager(UnitOfWorkManager uofMgr)
        {
            begrotingrepo = new BegrotingRepository(uofMgr.UnitOfWork);
        }

        public Actie AddActie(string actienaam, string informatie, double bedrag, BegrotingsType begrotingstype)
        {
            var actie = new Actie(actienaam, informatie, bedrag);

            if (begrotingstype.Acties == null)
                begrotingstype.Acties = new List<Actie>();
            begrotingstype.Acties.Add(actie);
            return begrotingrepo.CreateActie(actie);

        }

        public BegrotingsType AddBegrotingsType(BegrotingsType begrotingstype)
        {
            return begrotingrepo.CreateBegrotingsType(begrotingstype);
        }

        public Bestuur AddBestuur(Bestuur bestuur)
        {
            return begrotingrepo.CreateBestuur(bestuur);
        }

        public Categorie AddCategorie(string naam, CategorieNiveau categorieniveau)
        {
            var categorie = new Categorie
            {
                Naam = naam,
                CategorieNiveau = categorieniveau,

            };
            return begrotingrepo.CreateCategorie(categorie);
        }

        public CategorieInformatie AddCategorieInformatie(string informatie, string afbeelding, string youtubelink, double bedrag)
        {
            var categorieinformatie = new CategorieInformatie
            {
                Informatie = informatie,
                Afbeelding = afbeelding,
                YoutubeLink = youtubelink,
                Bedrag = bedrag
            };
            return begrotingrepo.CreateCategorieInformatie(categorieinformatie);
        }

        public void ChangeActie(Actie actie)
        {
            begrotingrepo.UpdateActie(actie);
        }

        public void ChangeActieTermijnToKort(int actieid)
        {
            begrotingrepo.UpdateActieTermijnToKort(actieid);
        }

        public void ChangeActieTermijnToLang(int actieid)
        {
            begrotingrepo.UpdateActieTermijnToLang(actieid);
        }

        public void ChangeBegrotingsType(BegrotingsType begrotingstype)
        {
            begrotingrepo.UpdateBegrotingsType(begrotingstype);
        }

        public void ChangeBestuur(Bestuur bestuur)
        {
            begrotingrepo.UpdateBestuur(bestuur);
        }

        public void ChangeCategorie(Categorie categorie)
        {
            begrotingrepo.UpdateCategorie(categorie);
        }

        public void ChangeCategorieInformatie(CategorieInformatie informatie)
        {
            begrotingrepo.UpdateCategorieInformatie(informatie);
        }

        public Actie GetActie(int actieid)
        {
            return begrotingrepo.ReadActie(actieid);
        }

        public BegrotingsType GetBegrotingsTypeFromGemeente(int jaartal, short postcode)
        {

            return begrotingrepo.ReadBegrotingsTypeFromGemeente(jaartal, postcode);
        }

        public IEnumerable<Actie> GetAllActies()
        {
            return begrotingrepo.ReadActies();
        }

        public IEnumerable<BegrotingsType> GetAllBegrotingsTypes()
        {
            return begrotingrepo.ReadBegrotingsTypes();
        }

        public IEnumerable<Bestuur> GetAllBesturen()
        {
            return begrotingrepo.ReadBesturen();
        }

        public IEnumerable<Categorie> GetAllCategorieën()
        {
            return begrotingrepo.ReadCategorieën();
        }

        public IEnumerable<CategorieInformatie> GetAllInformaties()
        {
            return begrotingrepo.ReadInformaties();
        }


        public Bestuur GetBestuur(int bestuurid)
        {
            return begrotingrepo.ReadBestuur(bestuurid);
        }

        public Categorie GetCategorie(int categorienummer)
        {
            return begrotingrepo.ReadCategorie(categorienummer);
        }

        public CategorieInformatie GetCategorieInformatie(int categorienummer)
        {
            return begrotingrepo.ReadCategorieInformatie(categorienummer);
        }

        public void RemoveActie(int actieid)
        {
            begrotingrepo.DeleteActie(actieid);
        }

        public void RemoveBegrotingsType(int begrotingnummer)
        {
            begrotingrepo.DeleteBegrotingsType(begrotingnummer);
        }

        public void RemoveBestuur(int bestuurid)
        {
            begrotingrepo.DeleteBestuur(bestuurid);
        }

        public void RemoveCategorie(int categorienummer)
        {
            begrotingrepo.DeleteCategorie(categorienummer);
        }

        public void RemoveCategorieInformatie(int categorienummer)
        {
            begrotingrepo.DeleteCategorieInformatie(categorienummer);
        }


        public void GetDataVanExcel(string fileName)
        {
            GemeenteManager gemeenteMgr = new GemeenteManager(uowMgr);
            // Open het Excel document met read-only acces.
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(fileName, false))
            {
                // Referentie maken naar het Workbook Part.
                WorkbookPart workbookPart = document.WorkbookPart;
                // Vind de eerste sheet en maak een referentie naar de worksheet.
                Sheet sheet = workbookPart.Workbook.Descendants<Sheet>().First();
                // Gooi een exception als de sheet niet wordt gevonden.
                if (sheet == null)
                {
                    throw new ArgumentException("De sheet met begrotingen werd niet gevonden!");
                }
                // Maak een referentie naar de Worksheet Part.
                WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);

                int i = 2;
                string cityString = "";
                List<Categorie> categories = GetAllCategorieën().ToList();
                do
                {
                    // CITY & BUDGET
                    string actionBudgetString = GetCellValue(worksheetPart, workbookPart, "O" + i);
                    double actionBudget;
                    Double.TryParse(actionBudgetString, out actionBudget);
                    cityString = GetCellValue(worksheetPart, workbookPart, "A" + i);
                    Gemeente city = gemeenteMgr.GetGemeente(cityString);
                    if (actionBudget != 0)
                    {
                        if (city != null)
                        {

                            int year = int.Parse(GetCellValue(worksheetPart, workbookPart, "M" + i));
                            BegrotingsType begrotingsType;
                            if (city.BegrotingsTypes.SingleOrDefault(b => b.Jaartal == year && b.Gemeente.Postcode == city.Postcode) != null)
                            {
                                begrotingsType = city.BegrotingsTypes.SingleOrDefault(b => b.Jaartal == year && b.Gemeente.Postcode == city.Postcode);
                            }
                            else
                            {
                                if (year < DateTime.Now.Year)
                                {
                                    begrotingsType = new Rekening(year);

                                    //Alle categorieinformaties aanmaken
                                    foreach (Categorie c in categories)
                                    {
                                        begrotingsType.CategorieInformaties.Add(new CategorieInformatie { Categorie = c, Bedrag = 0 });
                                    }
                                }
                                else
                                {
                                    begrotingsType = new Begroting(year);

                                    //Alle categorieinformaties aanmaken
                                    foreach (Categorie c in categories)
                                    {
                                        begrotingsType.CategorieInformaties.Add(new CategorieInformatie { Categorie = c, Bedrag = 0 });
                                    }
                                }
                            }
                            if (!city.BegrotingsTypes.Exists(x => x.Jaartal.Equals(year)))
                            {
                                city.BegrotingsTypes.Add(begrotingsType);
                            }
                            else
                            {
                                begrotingsType = city.BegrotingsTypes.Find(x => x.Jaartal.Equals(year));
                            }

                            // MAAK ACTIES
                            if (begrotingsType != null)
                            {
                                //ACTIE
                                string actionName = GetCellValue(worksheetPart, workbookPart, "D" + i);
                                string actionDescription = GetCellValue(worksheetPart, workbookPart, "E" + i);

                                Actie action = new Actie(actionName, actionDescription, actionBudget);
                                if (begrotingsType.Acties == null)
                                {
                                    begrotingsType.Acties = new List<Actie>();
                                }

                                // BESTUUR
                                string bestuurString = GetCellValue(worksheetPart, workbookPart, "B" + i);
                                Bestuur bestuur;
                                if (bestuurString.Contains("OCMW"))
                                {
                                    bestuur = new Bestuur(bestuurString, BestuurType.OCMW);
                                }
                                else if (bestuurString.Equals(city.Naam))
                                {
                                    bestuur = new Bestuur(bestuurString, BestuurType.Gemeente);
                                }
                                else
                                {
                                    bestuur = new Bestuur(bestuurString, BestuurType.AutonomeGemeentebedrijven);
                                }
                                if (GetAllBesturen().ToList().Exists(x => bestuurString.Equals(x.Naam)))
                                {
                                    bestuur = GetAllBesturen().ToList().Find(x => bestuurString.Equals(x.Naam));
                                }
                                action.Bestuur = bestuur;

                                // CATEGORIE
                                string categoryString = GetCellValue(worksheetPart, workbookPart, "H" + i);
                                Categorie categorie = categories.Single(x => categoryString.Contains(x.Naam) && x.CategorieNiveau.Equals(CategorieNiveau.C));
                                action.Categorie = categorie;

                                // ADD ACTIE BIJ BUDGET
                                if (actionBudget > 0 && !begrotingsType.Acties.ToList().Exists(x => x.Naam.Equals(actionName)))
                                {
                                    begrotingsType.Acties.Add(action);



                                    // CATEGORYINFO
                                    if (action.Categorie != null)
                                    {


                                        // C
                                        CategorieInformatie infoC;

                                        infoC = begrotingsType.CategorieInformaties.ToList().Find(x => x.Categorie.Naam.Equals(action.Categorie.Naam) && x.Categorie.CategorieNiveau == action.Categorie.CategorieNiveau);
                                        infoC.Bedrag += actionBudget;



                                        // B
                                        string catBString = GetCellValue(worksheetPart, workbookPart, "G" + i);
                                        Categorie catB = categories.Single(x => catBString.Contains(x.Naam) && x.CategorieNiveau == CategorieNiveau.B);
                                        if (catB == null) { throw new Exception(); }
                                        CategorieInformatie infoB;

                                        infoB = begrotingsType.CategorieInformaties.ToList().Find(x => x.Categorie.Naam.Equals(catB.Naam) && x.Categorie.CategorieNiveau == catB.CategorieNiveau);
                                        infoB.Bedrag += actionBudget;


                                        // A
                                        string catAString = GetCellValue(worksheetPart, workbookPart, "F" + i);
                                        Categorie catA = categories.Single(x => catAString.Contains(x.Naam) && x.CategorieNiveau == CategorieNiveau.A);
                                        if (catA == null) { throw new Exception(); }
                                        CategorieInformatie infoA;

                                        infoA = begrotingsType.CategorieInformaties.ToList().Find(x => x.Categorie.Naam.Equals(catA.Naam) && x.Categorie.CategorieNiveau == catA.CategorieNiveau);
                                        infoA.Bedrag += actionBudget;

                                        begrotingsType.Totaal += actionBudget;
                                    }
                                }
                            }
                            // UPDATE
                            gemeenteMgr.ChangeGemeente(city);
                        }
                    }
                    Console.WriteLine("Rij " + i + "ingelezen.");
                    i++;

                } while (cityString != null && !cityString.Equals(""));
            }
        }

        public void GetBegrotingenFromExcelViaStream(Stream fileStream)
        {
            GemeenteManager gemeenteMgr = new GemeenteManager(uowMgr);
            // Open het Excel document met read-only acces.
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(fileStream, false))
            {
                // Referentie maken naar het Workbook Part.
                WorkbookPart workbookPart = document.WorkbookPart;
                // Vind de eerste sheet en maak een referentie naar de worksheet.
                Sheet sheet = workbookPart.Workbook.Descendants<Sheet>().First();
                // Gooi een exception als de sheet niet wordt gevonden.
                if (sheet == null)
                {
                    throw new ArgumentException("De sheet met begrotingen werd niet gevonden!");
                }
                // Maak een referentie naar de Worksheet Part.
                WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);

                int i = 2;
                string cityString = "";
                List<Categorie> categories = GetAllCategorieën().ToList();
                do
                {
                    // CITY & BUDGET
                    string actionBudgetString = GetCellValue(worksheetPart, workbookPart, "O" + i);
                    double actionBudget;
                    Double.TryParse(actionBudgetString, out actionBudget);
                    cityString = GetCellValue(worksheetPart, workbookPart, "A" + i);
                    Gemeente city = gemeenteMgr.GetGemeente(cityString);
                    if (actionBudget != 0)
                    {
                        if (city != null)
                        {
                            if (city.BegrotingsTypes == null)
                            {
                                city.BegrotingsTypes = new List<BegrotingsType>();
                            }
                            int year = Int32.Parse(GetCellValue(worksheetPart, workbookPart, "M" + i));
                            BegrotingsType begrotingsType;
                            if (year < DateTime.Now.Year)
                            {
                                begrotingsType = new Rekening(year);

                                //Alle categorieinformaties aanmaken
                                foreach (Categorie c in categories)
                                {
                                    begrotingsType.CategorieInformaties.Add(new CategorieInformatie { Categorie = c, Bedrag = 0 });
                                }
                            }
                            else
                            {
                                begrotingsType = new Begroting(year);

                                //Alle categorieinformaties aanmaken
                                foreach (Categorie c in categories)
                                {
                                    begrotingsType.CategorieInformaties.Add(new CategorieInformatie { Categorie = c, Bedrag = 0 });
                                }
                            }
                            if (!city.BegrotingsTypes.Exists(x => x.Jaartal.Equals(year)))
                            {
                                city.BegrotingsTypes.Add(begrotingsType);
                            }
                            else
                            {
                                begrotingsType = city.BegrotingsTypes.Find(x => x.Jaartal.Equals(year));
                            }

                            // MAAK ACTIES
                            if (begrotingsType != null)
                            {
                                //ACTIE
                                string actionName = GetCellValue(worksheetPart, workbookPart, "D" + i);
                                string actionDescription = GetCellValue(worksheetPart, workbookPart, "E" + i);

                                Actie action = new Actie(actionName, actionDescription, actionBudget);
                                if (begrotingsType.Acties == null)
                                {
                                    begrotingsType.Acties = new List<Actie>();
                                }

                                // BESTUUR
                                string bestuurString = GetCellValue(worksheetPart, workbookPart, "B" + i);
                                Bestuur bestuur;
                                if (bestuurString.Contains("OCMW"))
                                {
                                    bestuur = new Bestuur(bestuurString, BestuurType.OCMW);
                                }
                                else if (bestuurString.Equals(city.Naam))
                                {
                                    bestuur = new Bestuur(bestuurString, BestuurType.Gemeente);
                                }
                                else
                                {
                                    bestuur = new Bestuur(bestuurString, BestuurType.AutonomeGemeentebedrijven);
                                }
                                if (GetAllBesturen().ToList().Exists(x => bestuurString.Equals(x.Naam)))
                                {
                                    bestuur = GetAllBesturen().ToList().Find(x => bestuurString.Equals(x.Naam));
                                }
                                action.Bestuur = bestuur;

                                // CATEGORIE
                                string categoryString = GetCellValue(worksheetPart, workbookPart, "H" + i);
                                Categorie categorie = categories.Single(x => categoryString.Contains(x.Naam) && x.CategorieNiveau.Equals(CategorieNiveau.C));
                                action.Categorie = categorie;

                                // ADD ACTIE BIJ BUDGET
                                if (actionBudget > 0 && !begrotingsType.Acties.ToList().Exists(x => x.Naam.Equals(actionName)))
                                {
                                    begrotingsType.Acties.Add(action);
                                    // CATEGORYINFO
                                    if (action.Categorie != null)
                                    {
                                        // C
                                        CategorieInformatie infoC;

                                        infoC = begrotingsType.CategorieInformaties.ToList().Find(x => x.Categorie.Naam.Equals(action.Categorie.Naam) && x.Categorie.CategorieNiveau == action.Categorie.CategorieNiveau);
                                        infoC.Bedrag += actionBudget;
                                        // B
                                        string catBString = GetCellValue(worksheetPart, workbookPart, "G" + i);
                                        Categorie catB = categories.Single(x => catBString.Contains(x.Naam) && x.CategorieNiveau == CategorieNiveau.B);
                                        if (catB == null) { throw new Exception(); }
                                        CategorieInformatie infoB;
                                        infoB = begrotingsType.CategorieInformaties.ToList().Find(x => x.Categorie.Naam.Equals(catB.Naam) && x.Categorie.CategorieNiveau == catB.CategorieNiveau);
                                        infoB.Bedrag += actionBudget;
                                        // A
                                        string catAString = GetCellValue(worksheetPart, workbookPart, "F" + i);
                                        Categorie catA = categories.Single(x => catAString.Contains(x.Naam) && x.CategorieNiveau == CategorieNiveau.A);
                                        if (catA == null) { throw new Exception(); }
                                        CategorieInformatie infoA;
                                        infoA = begrotingsType.CategorieInformaties.ToList().Find(x => x.Categorie.Naam.Equals(catA.Naam) && x.Categorie.CategorieNiveau == catA.CategorieNiveau);
                                        infoA.Bedrag += actionBudget;
                                        begrotingsType.Totaal += actionBudget;
                                    }
                                }
                            }

                            // UPDATE
                            gemeenteMgr.ChangeGemeente(city);
                        }
                    }
                    Console.WriteLine("Rij " + i + "ingelezen.");
                    i++;

                } while (cityString != null && !cityString.Equals(""));
            }
            uowMgr.Save();
        }



        string GetCellValue(WorksheetPart worksheetPart, WorkbookPart workbookPart, string cellAddress)
        {
            string cellValue = null;
            // Gebruik het meegegeven adres om de cel te vinden.
            Cell cell = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellAddress);

            // Als de cell leeg is, geef je een lege string terug.
            if (cell != null)
            {
                // Inlezen van de value in de variabele.
                // Als het een getal of een datum is ben je klaar.
                cellValue = cell.InnerText;
                // Als het niet een getal of een datum is, doe je de volgende stappen.
                // DataType == null -> numerische en datum types
                // DataType != null -> CellValues.SharedString voor strings, CellValues.Boolean voor booleans
                if (cell.DataType != null)
                {
                    switch (cell.DataType.Value)
                    {
                        case CellValues.SharedString:
                            var stringTable = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                            if (stringTable != null)
                            {
                                cellValue = stringTable.SharedStringTable.ElementAt(int.Parse(cellValue)).InnerText;
                            }
                            break;
                        case CellValues.Boolean:
                            switch (cellValue)
                            {
                                case "0":
                                    cellValue = "FALSE";
                                    break;
                                default:
                                    cellValue = "TRUE";
                                    break;
                            }
                            break;
                    }
                }
            }
            return cellValue;
        }
    }
}
