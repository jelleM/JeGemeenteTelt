using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEP.BL;
using BEP.BL.Models.Begrotingen;
using BEP.BL.Models.ParticipatieProjecten;

namespace BEP.UI.CA
{
  public class Program
  {

    public static void Main(string[] args)
    {
      BegrotingManager manager = new BegrotingManager();
      GemeenteManager gemeentemgr = new GemeenteManager();


      /*Uitlezen begrotingen*/
      
      /*
      manager.GetDataVanExcel(@"C:\Users\Wout\Documents\Toegepaste Informatica 2\Integratieproject\gemeente_categorie_acties_jaartal_uitgaven.xlsx");
      Console.WriteLine("Finished");
      Console.ReadLine();  */




      /*Toevoegen gemeente data*/

      //Gemeentes seeden
      /*
      Console.WriteLine("Start seeden gemeente data...");
      gemeentemgr.GetGemeenteDataVanExcel(@"C:\Users\Wout\Documents\Toegepaste Informatica 2\Integratieproject\Gemeentes\gemeentes_postcodes.xlsx");
      Console.WriteLine("finished"); */
      /*
      //Clusters toevoegen
      
      Console.WriteLine("Start seeden clusters data...");
      gemeentemgr.AddClustersToGemeente(@"C:\Users\Wout\Documents\Toegepaste Informatica 2\Integratieproject\Gemeentes\clusters.xlsx");
      Console.WriteLine("finished");   */

      //Belastingen toevoegen
      /*Console.WriteLine("Start seeden clusters data...");
      gemeentemgr.AddBelastingpercentagesToGemeente(@"C:\Users\jelly\Documents\Integratieproject\gemeentebelasting_percentages.xlsx");
      Console.WriteLine("finished"); */



      /* Test begroting */
      /*
      Begroting begroting2016 = (Begroting)manager.GetAllBegrotingsTypes().Single(b => b.Gemeente.Naam.Equals("Antwerpen") && b.Jaartal == 2016);
      Console.WriteLine("Begroting van Antwerpen 2016: ");
      Console.WriteLine("Totaal: " + begroting2016.Totaal);
      foreach (Actie a in begroting2016.Acties)
      {
        Console.WriteLine(a.Naam + " van categorie " + a.Categorie + "==> " + a.Bedrag + " euro.");
      }
      Console.ReadLine(); */
    }
  }
}
