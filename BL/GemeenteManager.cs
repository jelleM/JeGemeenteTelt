using System;
using System.Collections.Generic;
using System.Linq;
using BEP.BL.Models.Gemeentes;
using BEP.DAL;
using BEP.DAL.EF;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;

namespace BEP.BL
{
    public class GemeenteManager : IGemeenteManager
    {
        private readonly IGemeenteRepository gemeenterepo;

        public GemeenteManager()
        {
            gemeenterepo = new GemeenteRepository();
        }

        public GemeenteManager(UnitOfWorkManager uofMgr)
        {
            gemeenterepo = new GemeenteRepository(uofMgr.UnitOfWork);
        }

        public void ChangeGemeente(Gemeente gemeente)
        {
            gemeenterepo.UpdateGemeente(gemeente);
        }

        //Profile pic van user veranderen

        public void ChangeImageOfCity(short postcode, string image)
        {
            gemeenterepo.ChangeImage(postcode, image);
        }

        public IEnumerable<Gemeente> GetAllGemeentes()
        {
            return gemeenterepo.ReadGemeentes();
        }

        public Gemeente GetGemeente(short postcode)
        {
            return gemeenterepo.ReadGemeente(postcode);
        }

        public Gemeente GetGemeente(string naam)
        {
            return gemeenterepo.ReadGemeente(naam);
        }

        //Gemeentes uitlezen
        public void GetGemeenteDataVanExcel(string fileName)
        {
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
                List<Gemeente> gemeentes = new List<Gemeente>();
                string postcodeString = "";
                do
                {
                    Console.WriteLine(i);
                    //Postcode hoofdgemeente
                    postcodeString = GetCellValue(worksheetPart, workbookPart, "C" + i);
                    short postcode = 0;
                    if (postcodeString != null)
                    {
                        postcode = short.Parse(postcodeString);
                    }

                    //Hoofdgemeente
                    string gemeenteString = GetCellValue(worksheetPart, workbookPart, "D" + i);

                    //Provincie
                    string provincieString = GetCellValue(worksheetPart, workbookPart, "E" + i);

                    //Hoofdgemeente toevoegen
                    if (postcode != 0)
                    {
                        if (!(gemeenterepo.ReadGemeentes().ToList<Gemeente>().FindIndex(g => g.Postcode == postcode) >= 0))
                        {

                            gemeenterepo.CreateGemeente(new Gemeente { Postcode = postcode, Naam = gemeenteString, Provincie = provincieString });
                        }
                    }
                    i++;
                } while (postcodeString != null && !postcodeString.Equals(""));
            }
        }

        //Clusters toevoegen
        public void AddClustersToGemeente(string fileName)
        {
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
                List<Gemeente> gemeentes = new List<Gemeente>();
                string gemeenteString = "";
                do
                {
                    Console.WriteLine(i);
                    //Gemeente
                    gemeenteString = GetCellValue(worksheetPart, workbookPart, "A" + i);

                    //Provincie
                    string provincieString = GetCellValue(worksheetPart, workbookPart, "B" + i);

                    //Cluster
                    string clusterString = GetCellValue(worksheetPart, workbookPart, "C" + i);

                    //Cluster toevoegen
                    Gemeente g = gemeenterepo.ReadGemeente(gemeenteString);
                    if (g != null)
                    {
                        g.Cluster = clusterString;
                        gemeenterepo.UpdateGemeente(g);
                    }
                    i++;
                } while (gemeenteString != null && !gemeenteString.Equals(""));
            }
        }


        //Belastingpercentages toevoegen
        public void AddBelastingpercentagesToGemeente(string fileName)
        {
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
                List<Gemeente> gemeentes = new List<Gemeente>();
                string gemeenteString = "";
                do
                {
                    Console.WriteLine(i);
                    //Gemeente
                    gemeenteString = GetCellValue(worksheetPart, workbookPart, "A" + i);

                    //Belasting
                    string belastingString = GetCellValue(worksheetPart, workbookPart, "B" + i);

                    double gemeentebelasting = 0;
                    if (belastingString != null)
                    {
                        belastingString = belastingString.Replace(".", ",");
                        Double.TryParse(belastingString, out gemeentebelasting);
                        gemeentebelasting = gemeentebelasting / 100;
                    }

                    //Belastingen toevoegen
                    Gemeente g = gemeenterepo.ReadGemeente(gemeenteString);
                    if (g != null)
                    {
                        g.GemeenteBelasting = gemeentebelasting;
                        gemeenterepo.UpdateGemeente(g);
                    }
                    i++;
                } while (gemeenteString != null && !gemeenteString.Equals(""));
            }
        }


        //Belastingpercentages toevoegen
        public void AddBelastingpercentagesToGemeenteViaStream(Stream stream)
        {
            // Open het Excel document met read-only acces.
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(stream, false))
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
                List<Gemeente> gemeentes = new List<Gemeente>();
                string gemeenteString = "";
                do
                {
                    Console.WriteLine(i);
                    //Gemeente
                    gemeenteString = GetCellValue(worksheetPart, workbookPart, "A" + i);

                    //Belasting
                    string belastingString = GetCellValue(worksheetPart, workbookPart, "B" + i);

                    double gemeentebelasting = 0;
                    if (belastingString != null)
                    {
                        belastingString = belastingString.Replace(".", ",");
                        Double.TryParse(belastingString, out gemeentebelasting);
                        gemeentebelasting = gemeentebelasting / 100;
                    }

                    //Belastingen toevoegen
                    Gemeente g = gemeenterepo.ReadGemeente(gemeenteString);
                    if (g != null)
                    {
                        g.GemeenteBelasting = gemeentebelasting;
                        gemeenterepo.UpdateGemeente(g);
                    }
                    i++;
                } while (gemeenteString != null && !gemeenteString.Equals(""));
            }
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
