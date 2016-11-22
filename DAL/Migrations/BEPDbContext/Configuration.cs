using System.Collections.Generic;

namespace BEP.DAL.Migrations.BEPDbContext
{
  using System;
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.Linq;
  using BEP.BL.Models.Begrotingen;
  using BEP.BL.Models.ParticipatieProjecten;
  using BEP.BL.Models.Gemeentes;

  internal sealed class Configuration : DbMigrationsConfiguration<BEP.DAL.EF.BEPDbContext>
  {

    public Configuration()
    {
      AutomaticMigrationsEnabled = true;
      //AutomaticMigrationDataLossAllowed = true;
      MigrationsDirectory = @"Migrations\BEPDbContext";

    }

    protected override void Seed(BEP.DAL.EF.BEPDbContext context)
    {
      /*Data dat al geseed is plaats je in commentaar!*/



      //#region seedProjecten

      ////Projecten

      //ParticipatieProject project1 = new ParticipatieProject
      //{
      //  Type = ProjectType.Besparing,
      //  Titel = "Cultuur in Turnhout",
      //  Startmoment = new DateTime(2016, 04, 16),
      //  Eindmoment = new DateTime(2016, 05, 16),
      //  Bedrag = 1500


      //};
      //ParticipatieProject project2 = new ParticipatieProject
      //{
      //  Titel = "Onderwijsplannen",
      //  Type = ProjectType.Bestemming,
      //  Startmoment = new DateTime(2016, 04, 16),
      //  Eindmoment = new DateTime(2016, 05, 16),
      //  Bedrag = 1500

      //};
      //ParticipatieProject project3 = new ParticipatieProject
      //{
      //  Titel = "Burgerfeest",
      //  Type = ProjectType.Herschikking,
      //  Startmoment = new DateTime(2016, 04, 16),
      //  Eindmoment = new DateTime(2016, 05, 16),
      //  Bedrag = 1500

      //};

      //ParticipatieProject project4 = new ParticipatieProject
      //{
      //  Titel = "Cultuur in Turnhout",
      //  Type = ProjectType.Herschikking,
      //  Startmoment = new DateTime(2016, 04, 16),
      //  Eindmoment = new DateTime(2016, 05, 16),
      //  Bedrag = 1500

      //};

     
            
      

      //#endregion
      //#region seedVoorstel

      //Begrotingsvoorstel voorstel1 = new Begrotingsvoorstel
      //{
      //  KorteBeschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin",
      //  Draagvlak = 4,
      //  Datum = new DateTime(2016, 03, 18),
      //  Afbeelding = null,
      //  TotaalBudgetwijziging = 1545,
      //  Voorsteller = "Jan Janssens",
      //  ParticipatieProject = project1,
      //  Reacties = new List<Reactie> { new Reactie { Tekst = "Knap voorstel!", Datum = DateTime.Now, Draagvlak = 5, UserId = "f7078e63-bb51-4850-9a3b-d635d2307137" } }
      //};

      //Begrotingsvoorstel voorstel2 = new Begrotingsvoorstel
      //{
      //  KorteBeschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin",
      //  Draagvlak = 40,
      //  Datum = new DateTime(2016, 02, 18),
      //  Afbeelding = null,
      //  TotaalBudgetwijziging = 4500,
      //  Voorsteller = "Jos van den Berg",
      //  ParticipatieProject = project2

      //};
      //Begrotingsvoorstel voorstel3 = new Begrotingsvoorstel
      //{
      //  KorteBeschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin",
      //  Draagvlak = 85,
      //  Datum = new DateTime(2016, 01, 18),
      //  Afbeelding = null,
      //  TotaalBudgetwijziging = 150,
      //  Voorsteller = "Peter Peeters",
      //  ParticipatieProject = project3


      //};
      //Begrotingsvoorstel voorstel4 = new Begrotingsvoorstel
      //{
      //  KorteBeschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin",
      //  Draagvlak = 15,
      //  Datum = new DateTime(2016, 09, 18),
      //  Afbeelding = null,
      //  TotaalBudgetwijziging = 1545,
      //  Voorsteller = "Jan Janssens",
      //  ParticipatieProject = project4

      //};
      //Begrotingsvoorstel voorstel5 = new Begrotingsvoorstel
      //{
      //  KorteBeschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin",
      //  Draagvlak = 4,
      //  Datum = new DateTime(2016, 02, 18),
      //  Afbeelding = null,
      //  TotaalBudgetwijziging = 945,
      //  Voorsteller = "Jos van den Berg",
      //  ParticipatieProject = project4

      //};
      //Begrotingsvoorstel voorstel6 = new Begrotingsvoorstel
      //{
      //  KorteBeschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin Lorem ipsum dolor sit amet, consectetur adipiscin",
      //  Draagvlak = 450,
      //  Datum = new DateTime(2016, 02, 14),
      //  Afbeelding = null,
      //  TotaalBudgetwijziging = 450,
      //  Voorsteller = "Peter Peeters",
      //  ParticipatieProject = project1
      //};

      //context.Begrotingsvoorstellen.Add(voorstel1);
      //context.Begrotingsvoorstellen.Add(voorstel2);
      //context.Begrotingsvoorstellen.Add(voorstel3);
      //context.Begrotingsvoorstellen.Add(voorstel4);
      //context.Begrotingsvoorstellen.Add(voorstel5);
      //context.Begrotingsvoorstellen.Add(voorstel6);
      //context.SaveChanges();

      //#endregion
      
      //#region seedCategorie



      //// 100. Algemene financiering
      ////   110. Algemene financiering
      ////     111. Algemene overdrachten tussen de verschillende bestuurlijke niveaus
      ////     112. Fiscale aangelegenheden
      ////     113. Financiële aangelegenheden
      ////     114. Transacties in verband met de openbare schuld
      ////     115. Patrimonium zonder maatschappelijk doel
      ////     116. Overige algemene financiering

      //#region 1. Algemene financiering

      //#region Object initialisatie

      //Categorie c111 = new Categorie();
      //c111.CategorieNiveau = CategorieNiveau.C;
      //c111.CategorieId = 111;
      //c111.Naam = "Algemene overdrachten tussen de verschillende bestuurlijke niveaus";

      //Categorie c112 = new Categorie();
      //c112.CategorieNiveau = CategorieNiveau.C;
      //c112.CategorieId = 112;
      //c112.Naam = "Fiscale aangelegenheden";

      //Categorie c113 = new Categorie();
      //c113.CategorieNiveau = CategorieNiveau.C;
      //c113.CategorieId = 113;
      //c113.Naam = "Financiële aangelegenheden";

      //Categorie c114 = new Categorie();
      //c114.CategorieNiveau = CategorieNiveau.C;
      //c114.CategorieId = 114;
      //c114.Naam = "Transacties in verband met de openbare schuld";

      //Categorie c115 = new Categorie();
      //c115.CategorieNiveau = CategorieNiveau.C;
      //c115.CategorieId = 115;
      //c115.Naam = "Patrimonium zonder maatschappelijk doel";

      //Categorie c116 = new Categorie();
      //c116.CategorieNiveau = CategorieNiveau.C;
      //c116.CategorieId = 116;
      //c116.Naam = "Overige algemene financiering";

      //Categorie b110 = new Categorie();
      //b110.CategorieNiveau = CategorieNiveau.B;
      //b110.CategorieId = 110;
      //b110.Naam = "Algemene financiering";

      //Categorie a100 = new Categorie();
      //a100.CategorieNiveau = CategorieNiveau.A;
      //a100.CategorieId = 100;
      //a100.Naam = "Algemene financiering";

      //#endregion

      //#region Categorieën koppelen

      //// Alle categorie C objecten in de lijst van de respectievelijke categorie B plaatsen.
      //b110.SubCategorieen = new List<Categorie>();
      //b110.SubCategorieen.Add(c111);
      //b110.SubCategorieen.Add(c112);
      //b110.SubCategorieen.Add(c113);
      //b110.SubCategorieen.Add(c114);
      //b110.SubCategorieen.Add(c115);
      //b110.SubCategorieen.Add(c116);

      //// Alle categorie B objecten in de lijst van de respectievelijke categorie A plaatsen.
      //a100.SubCategorieen = new List<Categorie>();
      //a100.SubCategorieen.Add(b110);

      //// Alle categorieën toevoegen aan de databank.
      //context.Categorieen.Add(c111);
      //context.Categorieen.Add(c112);
      //context.Categorieen.Add(c113);
      //context.Categorieen.Add(c114);
      //context.Categorieen.Add(c115);
      //context.Categorieen.Add(c116);
      //context.Categorieen.Add(b110);
      //context.Categorieen.Add(a100);

      //#endregion

      //#endregion

      //// 200. Algemeen bestuur
      ////   210 Politieke organen
      ////     211 Politieke organen
      ////   220 Algemene diensten
      ////     221 Secretariaat
      ////     222 Fiscale en financiële diensten
      ////     223 Personeelsdienst en vorming
      ////     224 Archief
      ////     225 Organisatiebeheersing
      ////     226 Welzijn op het werk
      ////     227 Overige algemene diensten
      ////   230 Administratieve dienstverlening
      ////     231 Administratieve dienstverlening
      ////   240 Internationale samenwerking
      ////     241 Internationale relaties
      ////   250 Hulp aan het buitenland
      ////     251 Hulp aan het buitenland
      ////   260 Binnengemeentelijke decentralisatie
      ////     261 Binnengemeentelijke decentralisatie
      ////     262 Gemeentelijk/stedelijk wijkoverleg
      ////   270 Overig algemeen bestuur
      ////     271 Overig algemeen bestuur

      //#region 2. Algemeen bestuur

      //#region Object initialisatie

      //Categorie c211 = new Categorie();
      //c211.CategorieNiveau = CategorieNiveau.C;
      //c211.CategorieId = 211;
      //c211.Naam = "Politieke organen";

      //Categorie b210 = new Categorie();
      //b210.CategorieNiveau = CategorieNiveau.B;
      //b210.CategorieId = 210;
      //b210.Naam = "Politieke organen";

      //Categorie c221 = new Categorie();
      //c221.CategorieNiveau = CategorieNiveau.C;
      //c221.CategorieId = 221;
      //c221.Naam = "Secretariaat";

      //Categorie c222 = new Categorie();
      //c222.CategorieNiveau = CategorieNiveau.C;
      //c222.CategorieId = 222;
      //c222.Naam = "Fiscale en financiële diensten";

      //Categorie c223 = new Categorie();
      //c223.CategorieNiveau = CategorieNiveau.C;
      //c223.CategorieId = 223;
      //c223.Naam = "Personeelsdienst en vorming";

      //Categorie c224 = new Categorie();
      //c224.CategorieNiveau = CategorieNiveau.C;
      //c224.CategorieId = 224;
      //c224.Naam = "Archief";

      //Categorie c225 = new Categorie();
      //c225.CategorieNiveau = CategorieNiveau.C;
      //c225.CategorieId = 225;
      //c225.Naam = "Organisatiebeheersing";

      //Categorie c226 = new Categorie();
      //c226.CategorieNiveau = CategorieNiveau.C;
      //c226.CategorieId = 226;
      //c226.Naam = "Welzijn op het werk";

      //Categorie c227 = new Categorie();
      //c227.CategorieNiveau = CategorieNiveau.C;
      //c227.CategorieId = 227;
      //c227.Naam = "Overige algemene diensten";

      //Categorie b220 = new Categorie();
      //b220.CategorieNiveau = CategorieNiveau.B;
      //b220.CategorieId = 220;
      //b220.Naam = "Algemene diensten";

      //Categorie c231 = new Categorie();
      //c231.CategorieNiveau = CategorieNiveau.C;
      //c231.CategorieId = 231;
      //c231.Naam = "Administratieve dienstverlening";

      //Categorie b230 = new Categorie();
      //b230.CategorieNiveau = CategorieNiveau.B;
      //b230.CategorieId = 230;
      //b230.Naam = "Administratieve dienstverlening";

      //Categorie c241 = new Categorie();
      //c241.CategorieNiveau = CategorieNiveau.C;
      //c241.CategorieId = 241;
      //c241.Naam = "Internationale relaties";

      //Categorie b240 = new Categorie();
      //b240.CategorieNiveau = CategorieNiveau.B;
      //b240.CategorieId = 240;
      //b240.Naam = "Internationale samenwerking";

      //Categorie c251 = new Categorie();
      //c251.CategorieNiveau = CategorieNiveau.C;
      //c251.CategorieId = 251;
      //c251.Naam = "Hulp aan het buitenland";

      //Categorie b250 = new Categorie();
      //b250.CategorieNiveau = CategorieNiveau.B;
      //b250.CategorieId = 250;
      //b250.Naam = "Hulp aan het buitenland";

      //Categorie c261 = new Categorie();
      //c261.CategorieNiveau = CategorieNiveau.C;
      //c261.CategorieId = 261;
      //c261.Naam = "Binnengemeentelijke decentralisatie";

      //Categorie c262 = new Categorie();
      //c262.CategorieNiveau = CategorieNiveau.C;
      //c262.CategorieId = 262;
      //c262.Naam = "Gemeentelijk/stedelijk wijkoverleg";

      //Categorie b260 = new Categorie();
      //b260.CategorieNiveau = CategorieNiveau.B;
      //b260.CategorieId = 260;
      //b260.Naam = "Binnengemeentelijke decentralisatie";

      //Categorie c271 = new Categorie();
      //c271.CategorieNiveau = CategorieNiveau.C;
      //c271.CategorieId = 271;
      //c271.Naam = "Overig algemeen bestuur";

      //Categorie b270 = new Categorie();
      //b270.CategorieNiveau = CategorieNiveau.B;
      //b270.CategorieId = 270;
      //b270.Naam = "Overig algemeen bestuur";

      //Categorie a200 = new Categorie();
      //a200.CategorieNiveau = CategorieNiveau.A;
      //a200.CategorieId = 200;
      //a200.Naam = "Algemeen bestuur";

      //#endregion

      //#region Categorieën koppelen

      //// Alle categorie C objecten in de lijst van de respectievelijke categorie B plaatsen.
      //b210.SubCategorieen = new List<Categorie>();
      //b210.SubCategorieen.Add(c211);

      //b220.SubCategorieen = new List<Categorie>();
      //b220.SubCategorieen.Add(c221);
      //b220.SubCategorieen.Add(c222);
      //b220.SubCategorieen.Add(c223);
      //b220.SubCategorieen.Add(c224);
      //b220.SubCategorieen.Add(c225);
      //b220.SubCategorieen.Add(c226);
      //b220.SubCategorieen.Add(c227);

      //b230.SubCategorieen = new List<Categorie>();
      //b230.SubCategorieen.Add(c231);

      //b240.SubCategorieen = new List<Categorie>();
      //b240.SubCategorieen.Add(c241);

      //b250.SubCategorieen = new List<Categorie>();
      //b250.SubCategorieen.Add(c251);

      //b260.SubCategorieen = new List<Categorie>();
      //b260.SubCategorieen.Add(c261);
      //b260.SubCategorieen.Add(c262);

      //b270.SubCategorieen = new List<Categorie>();
      //b270.SubCategorieen.Add(c271);

      //// Alle categorie B objecten in de lijst van de respectievelijke categorie A plaatsen.
      //a200.SubCategorieen = new List<Categorie>();
      //a200.SubCategorieen.Add(b210);
      //a200.SubCategorieen.Add(b220);
      //a200.SubCategorieen.Add(b230);
      //a200.SubCategorieen.Add(b240);
      //a200.SubCategorieen.Add(b250);
      //a200.SubCategorieen.Add(b260);
      //a200.SubCategorieen.Add(b270);

      //// Alle categorieën toevoegen aan de databank.
      //context.Categorieen.Add(c211);
      //context.Categorieen.Add(b210);

      //context.Categorieen.Add(c221);
      //context.Categorieen.Add(c222);
      //context.Categorieen.Add(c223);
      //context.Categorieen.Add(c224);
      //context.Categorieen.Add(c225);
      //context.Categorieen.Add(c226);
      //context.Categorieen.Add(c227);
      //context.Categorieen.Add(b220);

      //context.Categorieen.Add(c231);
      //context.Categorieen.Add(b230);

      //context.Categorieen.Add(c241);
      //context.Categorieen.Add(b240);

      //context.Categorieen.Add(c251);
      //context.Categorieen.Add(b250);

      //context.Categorieen.Add(c261);
      //context.Categorieen.Add(c262);
      //context.Categorieen.Add(b260);

      //context.Categorieen.Add(c271);
      //context.Categorieen.Add(b270);

      //context.Categorieen.Add(a200);

      //#endregion

      //#endregion

      //// 300. Zich verplaatsen en mobiliteit
      ////   310. Zich verplaatsen en mobiliteit
      ////     311. Wegen
      ////     312. Openbaar vervoer
      ////     313. Parkeren
      ////     314. Overige mobiliteit en verkeer

      //#region 3. Zich verplaatsen en mobiliteit

      //#region Object initialisatie

      //Categorie c311 = new Categorie();
      //c311.CategorieNiveau = CategorieNiveau.C;
      //c311.CategorieId = 311;
      //c311.Naam = "Wegen";

      //Categorie c312 = new Categorie();
      //c312.CategorieNiveau = CategorieNiveau.C;
      //c312.CategorieId = 312;
      //c312.Naam = "Openbaar vervoer";

      //Categorie c313 = new Categorie();
      //c313.CategorieNiveau = CategorieNiveau.C;
      //c313.CategorieId = 313;
      //c313.Naam = "Parkeren";

      //Categorie c314 = new Categorie();
      //c314.CategorieNiveau = CategorieNiveau.C;
      //c314.CategorieId = 314;
      //c314.Naam = "Overige mobiliteit en verkeer";

      //Categorie b310 = new Categorie();
      //b310.CategorieNiveau = CategorieNiveau.B;
      //b310.CategorieId = 310;
      //b310.Naam = "Zich verplaatsen en mobiliteit";

      //Categorie a300 = new Categorie();
      //a300.CategorieNiveau = CategorieNiveau.A;
      //a300.CategorieId = 300;
      //a300.Naam = "Zich verplaatsen en mobiliteit";

      //#endregion

      //#region Categorieën koppelen

      //// Alle categorie C objecten in de lijst van de respectievelijke categorie B plaatsen.
      //b310.SubCategorieen = new List<Categorie>();
      //b310.SubCategorieen.Add(c311);
      //b310.SubCategorieen.Add(c312);
      //b310.SubCategorieen.Add(c313);
      //b310.SubCategorieen.Add(c314);

      //// Alle categorie B objecten in de lijst van de respectievelijke categorie A plaatsen.
      //a300.SubCategorieen = new List<Categorie>();
      //a300.SubCategorieen.Add(b310);

      //// Alle categorieën toevoegen aan de databank.
      //context.Categorieen.Add(c311);
      //context.Categorieen.Add(c312);
      //context.Categorieen.Add(c313);
      //context.Categorieen.Add(c314);
      //context.Categorieen.Add(b310);

      //context.Categorieen.Add(a300);

      //#endregion

      //#endregion

      //// 400. Natuur en milieubeheer
      ////   410. Afval- en materialenbeheer
      ////     411. Ophalen en verwerken van huishoudelijk afval
      ////     412. Overig afval- en materialenbeheer
      ////   420. Waterbeheer
      ////     421. Beheer van regen- en afvalwater
      ////     422. Overig waterbeheer
      ////   430. Vermindering van de milieuverontreiniging
      ////     431. Sanering van bodemverontreiniging
      ////     432. Overige vermindering van milieuverontreiniging
      ////   440. Bescherming van biodiversiteit, landschappen en bodem
      ////     441. Aankoop, inrichting en beheer van natuur, groen en bos
      ////     442. Overige bescherming van biodiversiteit, landschappen en bodem
      ////   450. Klimaat en energie
      ////     451. Klimaat en energie
      ////   460. Overige milieubescherming
      ////     461. Participatie en sensibilisatie
      ////     462. Geïntegreerde milieuprojecten
      ////     463. Overige milieubescherming

      //#region 4. Natuur en milieubeheer

      //#region Object initialisatie

      //Categorie c411 = new Categorie();
      //c411.CategorieNiveau = CategorieNiveau.C;
      //c411.CategorieId = 411;
      //c411.Naam = "Ophalen en verwerken van huishoudelijk afval";

      //Categorie c412 = new Categorie();
      //c412.CategorieNiveau = CategorieNiveau.C;
      //c412.CategorieId = 412;
      //c412.Naam = "Overig afval- en materialenbeheer";

      //Categorie b410 = new Categorie();
      //b410.CategorieNiveau = CategorieNiveau.B;
      //b410.CategorieId = 410;
      //b410.Naam = "Afval- en materialenbeheer";

      //Categorie c421 = new Categorie();
      //c421.CategorieNiveau = CategorieNiveau.C;
      //c421.CategorieId = 421;
      //c421.Naam = "Beheer van regen- en afvalwater";

      //Categorie c422 = new Categorie();
      //c422.CategorieNiveau = CategorieNiveau.C;
      //c422.CategorieId = 422;
      //c422.Naam = "Overig waterbeheer";

      //Categorie b420 = new Categorie();
      //b420.CategorieNiveau = CategorieNiveau.B;
      //b420.CategorieId = 420;
      //b420.Naam = "Waterbeheer";

      //Categorie c431 = new Categorie();
      //c431.CategorieNiveau = CategorieNiveau.C;
      //c431.CategorieId = 431;
      //c431.Naam = "Sanering van bodemverontreiniging";

      //Categorie c432 = new Categorie();
      //c432.CategorieNiveau = CategorieNiveau.C;
      //c432.CategorieId = 432;
      //c432.Naam = "Overige vermindering van milieuverontreiniging";

      //Categorie b430 = new Categorie();
      //b430.CategorieNiveau = CategorieNiveau.B;
      //b430.CategorieId = 430;
      //b430.Naam = "Vermindering van de milieuverontreiniging";

      //Categorie c441 = new Categorie();
      //c441.CategorieNiveau = CategorieNiveau.C;
      //c441.CategorieId = 441;
      //c441.Naam = "Aankoop, inrichting en beheer van natuur, groen en bos";

      //Categorie c442 = new Categorie();
      //c442.CategorieNiveau = CategorieNiveau.C;
      //c442.CategorieId = 442;
      //c442.Naam = "Overige bescherming van biodiversiteit, landschappen en bodem";

      //Categorie b440 = new Categorie();
      //b440.CategorieNiveau = CategorieNiveau.B;
      //b440.CategorieId = 440;
      //b440.Naam = "Bescherming van biodiversiteit, landschappen en bodem";

      //Categorie c451 = new Categorie();
      //c451.CategorieNiveau = CategorieNiveau.C;
      //c451.CategorieId = 451;
      //c451.Naam = "Klimaat en energie";

      //Categorie b450 = new Categorie();
      //b450.CategorieNiveau = CategorieNiveau.B;
      //b450.CategorieId = 450;
      //b450.Naam = "Klimaat en energie";

      //Categorie c461 = new Categorie();
      //c461.CategorieNiveau = CategorieNiveau.C;
      //c461.CategorieId = 461;
      //c461.Naam = "Participatie en sensibilisatie";

      //Categorie c462 = new Categorie();
      //c462.CategorieNiveau = CategorieNiveau.C;
      //c462.CategorieId = 462;
      //c462.Naam = "Geïntegreerde milieuprojecten";

      //Categorie c463 = new Categorie();
      //c463.CategorieNiveau = CategorieNiveau.C;
      //c463.CategorieId = 463;
      //c463.Naam = "Overige milieubescherming";

      //Categorie b460 = new Categorie();
      //b460.CategorieNiveau = CategorieNiveau.B;
      //b460.CategorieId = 460;
      //b460.Naam = "Overige milieubescherming";

      //Categorie a400 = new Categorie();
      //a400.CategorieNiveau = CategorieNiveau.A;
      //a400.CategorieId = 400;
      //a400.Naam = "Natuur en milieubeheer";

      //#endregion

      //#region Categorieën koppelen

      //// Alle categorie C objecten in de lijst van de respectievelijke categorie B plaatsen.
      //b410.SubCategorieen = new List<Categorie>();
      //b410.SubCategorieen.Add(c411);
      //b410.SubCategorieen.Add(c412);

      //b420.SubCategorieen = new List<Categorie>();
      //b420.SubCategorieen.Add(c421);
      //b420.SubCategorieen.Add(c422);

      //b430.SubCategorieen = new List<Categorie>();
      //b430.SubCategorieen.Add(c431);
      //b430.SubCategorieen.Add(c432);

      //b440.SubCategorieen = new List<Categorie>();
      //b440.SubCategorieen.Add(c441);
      //b440.SubCategorieen.Add(c442);

      //b450.SubCategorieen = new List<Categorie>();
      //b450.SubCategorieen.Add(c451);

      //b460.SubCategorieen = new List<Categorie>();
      //b460.SubCategorieen.Add(c461);
      //b460.SubCategorieen.Add(c462);
      //b460.SubCategorieen.Add(c463);

      //// Alle categorie B objecten in de lijst van de respectievelijke categorie A plaatsen.
      //a400.SubCategorieen = new List<Categorie>();
      //a400.SubCategorieen.Add(b410);
      //a400.SubCategorieen.Add(b420);
      //a400.SubCategorieen.Add(b430);
      //a400.SubCategorieen.Add(b440);
      //a400.SubCategorieen.Add(b450);
      //a400.SubCategorieen.Add(b460);

      //// Alle categorieën toevoegen aan de databank.
      //context.Categorieen.Add(c411);
      //context.Categorieen.Add(c412);
      //context.Categorieen.Add(b410);

      //context.Categorieen.Add(c421);
      //context.Categorieen.Add(c422);
      //context.Categorieen.Add(b420);

      //context.Categorieen.Add(c431);
      //context.Categorieen.Add(c432);
      //context.Categorieen.Add(b430);

      //context.Categorieen.Add(c441);
      //context.Categorieen.Add(c442);
      //context.Categorieen.Add(b440);

      //context.Categorieen.Add(c451);
      //context.Categorieen.Add(b450);

      //context.Categorieen.Add(c461);
      //context.Categorieen.Add(c462);
      //context.Categorieen.Add(c463);
      //context.Categorieen.Add(b460);

      //context.Categorieen.Add(a400);

      //#endregion

      //#endregion

      //// 500. Veiligheidszorg
      ////   510. Politiediensten
      ////     511. Politiediensten
      ////   520. Brandweer
      ////     521. Brandweer
      ////   530. Overige hulpdiensten
      ////     531. Dienst 100
      ////     532. Civiele bescherming
      ////     533. Overige hulpdiensten
      ////   540. Overige elementen van openbare orde en veiligheid
      ////     541. Rechtspleging
      ////     542. Dierenbescherming
      ////     543. Bestuurlijke preventie (incl. GAS)
      ////     544. Overige elementen van openbare orde en veiligheid

      //#region 5. Veiligheidszorg

      //#region Object initialisatie

      //Categorie c511 = new Categorie();
      //c511.CategorieNiveau = CategorieNiveau.C;
      //c511.CategorieId = 511;
      //c511.Naam = "Politiediensten";

      //Categorie b510 = new Categorie();
      //b510.CategorieNiveau = CategorieNiveau.B;
      //b510.CategorieId = 510;
      //b510.Naam = "Politiediensten";

      //Categorie c521 = new Categorie();
      //c521.CategorieNiveau = CategorieNiveau.C;
      //c521.CategorieId = 521;
      //c521.Naam = "Brandweer";

      //Categorie b520 = new Categorie();
      //b520.CategorieNiveau = CategorieNiveau.B;
      //b520.CategorieId = 520;
      //b520.Naam = "Brandweer";

      //Categorie c531 = new Categorie();
      //c531.CategorieNiveau = CategorieNiveau.C;
      //c531.CategorieId = 531;
      //c531.Naam = "Dienst 100";

      //Categorie c532 = new Categorie();
      //c532.CategorieNiveau = CategorieNiveau.C;
      //c532.CategorieId = 532;
      //c532.Naam = "Civiele bescherming";

      //Categorie c533 = new Categorie();
      //c533.CategorieNiveau = CategorieNiveau.C;
      //c533.CategorieId = 533;
      //c533.Naam = "Overige hulpdiensten";

      //Categorie b530 = new Categorie();
      //b530.CategorieNiveau = CategorieNiveau.B;
      //b530.CategorieId = 530;
      //b530.Naam = "Overige hulpdiensten";

      //Categorie c541 = new Categorie();
      //c541.CategorieNiveau = CategorieNiveau.C;
      //c541.CategorieId = 541;
      //c541.Naam = "Rechtspleging";

      //Categorie c542 = new Categorie();
      //c542.CategorieNiveau = CategorieNiveau.C;
      //c542.CategorieId = 542;
      //c542.Naam = "Dierenbescherming";

      //Categorie c543 = new Categorie();
      //c543.CategorieNiveau = CategorieNiveau.C;
      //c543.CategorieId = 543;
      //c543.Naam = "Bestuurlijke preventie (incl. GAS)";

      //Categorie c544 = new Categorie();
      //c544.CategorieNiveau = CategorieNiveau.C;
      //c544.CategorieId = 544;
      //c544.Naam = "Overige elementen van openbare orde en veiligheid";

      //Categorie b540 = new Categorie();
      //b540.CategorieNiveau = CategorieNiveau.B;
      //b540.CategorieId = 540;
      //b540.Naam = "Overige elementen van openbare orde en veiligheid";

      //Categorie a500 = new Categorie();
      //a500.CategorieNiveau = CategorieNiveau.A;
      //a500.CategorieId = 500;
      //a500.Naam = "Veiligheidszorg";

      //#endregion

      //#region Categorieën koppelen

      //// Alle categorie C objecten in de lijst van de respectievelijke categorie B plaatsen.
      //b510.SubCategorieen = new List<Categorie>();
      //b510.SubCategorieen.Add(c511);

      //b520.SubCategorieen = new List<Categorie>();
      //b520.SubCategorieen.Add(c521);

      //b530.SubCategorieen = new List<Categorie>();
      //b530.SubCategorieen.Add(c531);
      //b530.SubCategorieen.Add(c532);
      //b530.SubCategorieen.Add(c533);

      //b540.SubCategorieen = new List<Categorie>();
      //b540.SubCategorieen.Add(c541);
      //b540.SubCategorieen.Add(c542);
      //b540.SubCategorieen.Add(c543);
      //b540.SubCategorieen.Add(c544);

      //// Alle categorie B objecten in de lijst van de respectievelijke categorie A plaatsen.
      //a500.SubCategorieen = new List<Categorie>();
      //a500.SubCategorieen.Add(b510);
      //a500.SubCategorieen.Add(b520);
      //a500.SubCategorieen.Add(b530);
      //a500.SubCategorieen.Add(b540);

      //// Alle categorieën toevoegen aan de databank.
      //context.Categorieen.Add(c511);
      //context.Categorieen.Add(b510);

      //context.Categorieen.Add(c521);
      //context.Categorieen.Add(b520);

      //context.Categorieen.Add(c531);
      //context.Categorieen.Add(c532);
      //context.Categorieen.Add(c533);
      //context.Categorieen.Add(b530);

      //context.Categorieen.Add(c541);
      //context.Categorieen.Add(c542);
      //context.Categorieen.Add(c543);
      //context.Categorieen.Add(c544);
      //context.Categorieen.Add(b540);

      //context.Categorieen.Add(a500);

      //#endregion

      //#endregion

      //// 600. Ondernemen en werken
      ////   610. Handel en middenstand
      ////     611. Handel en middenstand
      ////   620. Nijverheid
      ////     621. Nijverheid
      ////   630. Toerisme
      ////     631. Toerisme - Onthaal en promotie
      ////     632. Toerisme - Sectorondersteuning
      ////     633. Toerisme - Infrastructuur
      ////     634. Overige activiteiten inzake toerisme
      ////   640. Land-, tuin- en bosbouw
      ////     641. Land-, tuin- & bosbouw
      ////   650. Visvangst
      ////     651. Visvangst
      ////   660. Werkgelegenheid
      ////     661. Werkgelegenheid
      ////   670. Overige economische zaken
      ////     671. Overige economische zaken

      //#region 6. Ondernemen en werken

      //#region Object initialisatie

      //Categorie c611 = new Categorie();
      //c611.CategorieNiveau = CategorieNiveau.C;
      //c611.CategorieId = 611;
      //c611.Naam = "Handel en middenstand";

      //Categorie b610 = new Categorie();
      //b610.CategorieNiveau = CategorieNiveau.B;
      //b610.CategorieId = 610;
      //b610.Naam = "Handel en middenstand";

      //Categorie c621 = new Categorie();
      //c621.CategorieNiveau = CategorieNiveau.C;
      //c621.CategorieId = 621;
      //c621.Naam = "Nijverheid";

      //Categorie b620 = new Categorie();
      //b620.CategorieNiveau = CategorieNiveau.B;
      //b620.CategorieId = 620;
      //b620.Naam = "Nijverheid";

      //Categorie c631 = new Categorie();
      //c631.CategorieNiveau = CategorieNiveau.C;
      //c631.CategorieId = 631;
      //c631.Naam = "Toerisme - Onthaal en promotie";

      //Categorie c632 = new Categorie();
      //c632.CategorieNiveau = CategorieNiveau.C;
      //c632.CategorieId = 632;
      //c632.Naam = "Toerisme - Sectorondersteuning";

      //Categorie c633 = new Categorie();
      //c633.CategorieNiveau = CategorieNiveau.C;
      //c633.CategorieId = 633;
      //c633.Naam = "Toerisme - Infrastructuur";

      //Categorie c634 = new Categorie();
      //c634.CategorieNiveau = CategorieNiveau.C;
      //c634.CategorieId = 634;
      //c634.Naam = "Overige activiteiten inzake toerisme";

      //Categorie b630 = new Categorie();
      //b630.CategorieNiveau = CategorieNiveau.B;
      //b630.CategorieId = 630;
      //b630.Naam = "Toerisme";

      //Categorie c641 = new Categorie();
      //c641.CategorieNiveau = CategorieNiveau.C;
      //c641.CategorieId = 641;
      //c641.Naam = "Land-, tuin- & bosbouw";

      //Categorie b640 = new Categorie();
      //b640.CategorieNiveau = CategorieNiveau.B;
      //b640.CategorieId = 640;
      //b640.Naam = "Land-, tuin- en bosbouw";

      //Categorie c651 = new Categorie();
      //c651.CategorieNiveau = CategorieNiveau.C;
      //c651.CategorieId = 651;
      //c651.Naam = "Visvangst";

      //Categorie b650 = new Categorie();
      //b650.CategorieNiveau = CategorieNiveau.B;
      //b650.CategorieId = 650;
      //b650.Naam = "Visvangst";

      //Categorie c661 = new Categorie();
      //c661.CategorieNiveau = CategorieNiveau.C;
      //c661.CategorieId = 661;
      //c661.Naam = "Werkgelegenheid";

      //Categorie b660 = new Categorie();
      //b660.CategorieNiveau = CategorieNiveau.B;
      //b660.CategorieId = 660;
      //b660.Naam = "Werkgelegenheid";

      //Categorie c671 = new Categorie();
      //c671.CategorieNiveau = CategorieNiveau.C;
      //c671.CategorieId = 671;
      //c671.Naam = "Overige economische zaken";

      //Categorie b670 = new Categorie();
      //b670.CategorieNiveau = CategorieNiveau.B;
      //b670.CategorieId = 670;
      //b670.Naam = "Overige economische zaken";

      //Categorie a600 = new Categorie();
      //a600.CategorieNiveau = CategorieNiveau.A;
      //a600.CategorieId = 600;
      //a600.Naam = "Ondernemen en werken";

      //#endregion

      //#region Categorieën koppelen

      //// Alle categorie C objecten in de lijst van de respectievelijke categorie B plaatsen.
      //b610.SubCategorieen = new List<Categorie>();
      //b610.SubCategorieen.Add(c611);

      //b620.SubCategorieen = new List<Categorie>();
      //b620.SubCategorieen.Add(c621);

      //b630.SubCategorieen = new List<Categorie>();
      //b630.SubCategorieen.Add(c631);
      //b630.SubCategorieen.Add(c632);
      //b630.SubCategorieen.Add(c633);
      //b630.SubCategorieen.Add(c634);

      //b640.SubCategorieen = new List<Categorie>();
      //b640.SubCategorieen.Add(c641);

      //b650.SubCategorieen = new List<Categorie>();
      //b650.SubCategorieen.Add(c651);

      //b660.SubCategorieen = new List<Categorie>();
      //b660.SubCategorieen.Add(c661);

      //b670.SubCategorieen = new List<Categorie>();
      //b670.SubCategorieen.Add(c671);

      //// Alle categorie B objecten in de lijst van de respectievelijke categorie A plaatsen.
      //a600.SubCategorieen = new List<Categorie>();
      //a600.SubCategorieen.Add(b610);
      //a600.SubCategorieen.Add(b620);
      //a600.SubCategorieen.Add(b630);
      //a600.SubCategorieen.Add(b640);
      //a600.SubCategorieen.Add(b650);
      //a600.SubCategorieen.Add(b660);
      //a600.SubCategorieen.Add(b670);

      //// Alle categorieën toevoegen aan de databank.
      //context.Categorieen.Add(c611);
      //context.Categorieen.Add(b610);

      //context.Categorieen.Add(c621);
      //context.Categorieen.Add(b620);

      //context.Categorieen.Add(c631);
      //context.Categorieen.Add(c632);
      //context.Categorieen.Add(c633);
      //context.Categorieen.Add(c634);
      //context.Categorieen.Add(b630);

      //context.Categorieen.Add(c641);
      //context.Categorieen.Add(b640);

      //context.Categorieen.Add(c651);
      //context.Categorieen.Add(b650);

      //context.Categorieen.Add(c661);
      //context.Categorieen.Add(b660);

      //context.Categorieen.Add(c671);
      //context.Categorieen.Add(b670);

      //context.Categorieen.Add(a600);

      //#endregion

      //#endregion

      //// 700. Wonen en ruimtelijke ordening
      ////   710. Ruimtelijke planning
      ////     711. Ruimtelijke planning
      ////   720. Gebiedsontwikkeling
      ////     721. Gebiedsontwikkeling
      ////   730. Woonbeleid
      ////     731. Grondbeleid voor wonen
      ////     732. Bestrijding van krotwoningen
      ////     733. Woonwagenterreinen
      ////     734. Overig woonbeleid
      ////   740. Watervoorziening
      ////     741. Watervoorziening
      ////   750. Elektriciteitsvoorziening
      ////     751. Elektriciteitsvoorziening
      ////   760. Gasvoorziening
      ////     761. Gasvoorziening
      ////   770. Straatverlichting
      ////     771. Straatverlichting
      ////   780. Groene ruimte
      ////     781. Groene ruimte
      ////   790. Overige nutsvoorzieningen
      ////     791. Overige nutsvoorzieningen

      //#region 7. Wonen en ruimtelijke ordening

      //#region Object initialisatie

      //Categorie c711 = new Categorie();
      //c711.CategorieNiveau = CategorieNiveau.C;
      //c711.CategorieId = 711;
      //c711.Naam = "Ruimtelijke planning";

      //Categorie b710 = new Categorie();
      //b710.CategorieNiveau = CategorieNiveau.B;
      //b710.CategorieId = 710;
      //b710.Naam = "Ruimtelijke planning";

      //Categorie c721 = new Categorie();
      //c721.CategorieNiveau = CategorieNiveau.C;
      //c721.CategorieId = 721;
      //c721.Naam = "Gebiedsontwikkeling";

      //Categorie b720 = new Categorie();
      //b720.CategorieNiveau = CategorieNiveau.B;
      //b720.CategorieId = 720;
      //b720.Naam = "Gebiedsontwikkeling";

      //Categorie c731 = new Categorie();
      //c731.CategorieNiveau = CategorieNiveau.C;
      //c731.CategorieId = 731;
      //c731.Naam = "Grondbeleid voor wonen";

      //Categorie c732 = new Categorie();
      //c732.CategorieNiveau = CategorieNiveau.C;
      //c732.CategorieId = 732;
      //c732.Naam = "Bestrijding van krotwoningen";

      //Categorie c733 = new Categorie();
      //c733.CategorieNiveau = CategorieNiveau.C;
      //c733.CategorieId = 733;
      //c733.Naam = "Woonwagenterreinen";

      //Categorie c734 = new Categorie();
      //c734.CategorieNiveau = CategorieNiveau.C;
      //c734.CategorieId = 734;
      //c734.Naam = "Overig woonbeleid";

      //Categorie b730 = new Categorie();
      //b730.CategorieNiveau = CategorieNiveau.B;
      //b730.CategorieId = 730;
      //b730.Naam = "Woonbeleid";

      //Categorie c741 = new Categorie();
      //c741.CategorieNiveau = CategorieNiveau.C;
      //c741.CategorieId = 741;
      //c741.Naam = "Watervoorziening";

      //Categorie b740 = new Categorie();
      //b740.CategorieNiveau = CategorieNiveau.B;
      //b740.CategorieId = 740;
      //b740.Naam = "Watervoorziening";

      //Categorie c751 = new Categorie();
      //c751.CategorieNiveau = CategorieNiveau.C;
      //c751.CategorieId = 751;
      //c751.Naam = "Elektriciteitsvoorziening";

      //Categorie b750 = new Categorie();
      //b750.CategorieNiveau = CategorieNiveau.B;
      //b750.CategorieId = 750;
      //b750.Naam = "Elektriciteitsvoorziening";

      //Categorie c761 = new Categorie();
      //c761.CategorieNiveau = CategorieNiveau.C;
      //c761.CategorieId = 761;
      //c761.Naam = "Gasvoorziening";

      //Categorie b760 = new Categorie();
      //b760.CategorieNiveau = CategorieNiveau.B;
      //b760.CategorieId = 760;
      //b760.Naam = "Gasvoorziening";

      //Categorie c771 = new Categorie();
      //c771.CategorieNiveau = CategorieNiveau.C;
      //c771.CategorieId = 771;
      //c771.Naam = "Straatverlichting";

      //Categorie b770 = new Categorie();
      //b770.CategorieNiveau = CategorieNiveau.B;
      //b770.CategorieId = 770;
      //b770.Naam = "Straatverlichting";

      //Categorie c781 = new Categorie();
      //c781.CategorieNiveau = CategorieNiveau.C;
      //c781.CategorieId = 781;
      //c781.Naam = "Groene ruimte";

      //Categorie b780 = new Categorie();
      //b780.CategorieNiveau = CategorieNiveau.B;
      //b780.CategorieId = 780;
      //b780.Naam = "Groene ruimte";

      //Categorie c791 = new Categorie();
      //c791.CategorieNiveau = CategorieNiveau.C;
      //c791.CategorieId = 791;
      //c791.Naam = "Overige nutsvoorzieningen";

      //Categorie b790 = new Categorie();
      //b790.CategorieNiveau = CategorieNiveau.B;
      //b790.CategorieId = 790;
      //b790.Naam = "Overige nutsvoorzieningen";

      //Categorie a700 = new Categorie();
      //a700.CategorieNiveau = CategorieNiveau.A;
      //a700.CategorieId = 700;
      //a700.Naam = "Wonen en ruimtelijke ordening";

      //#endregion

      //#region Categorieën koppelen

      //// Alle categorie C objecten in de lijst van de respectievelijke categorie B plaatsen.
      //b710.SubCategorieen = new List<Categorie>();
      //b710.SubCategorieen.Add(c711);

      //b720.SubCategorieen = new List<Categorie>();
      //b720.SubCategorieen.Add(c721);

      //b730.SubCategorieen = new List<Categorie>();
      //b730.SubCategorieen.Add(c731);
      //b730.SubCategorieen.Add(c732);
      //b730.SubCategorieen.Add(c733);
      //b730.SubCategorieen.Add(c734);

      //b740.SubCategorieen = new List<Categorie>();
      //b740.SubCategorieen.Add(c741);

      //b750.SubCategorieen = new List<Categorie>();
      //b750.SubCategorieen.Add(c751);

      //b760.SubCategorieen = new List<Categorie>();
      //b760.SubCategorieen.Add(c761);

      //b770.SubCategorieen = new List<Categorie>();
      //b770.SubCategorieen.Add(c771);

      //b780.SubCategorieen = new List<Categorie>();
      //b780.SubCategorieen.Add(c781);

      //b790.SubCategorieen = new List<Categorie>();
      //b790.SubCategorieen.Add(c791);

      //// Alle categorie B objecten in de lijst van de respectievelijke categorie A plaatsen.
      //a700.SubCategorieen = new List<Categorie>();
      //a700.SubCategorieen.Add(b710);
      //a700.SubCategorieen.Add(b720);
      //a700.SubCategorieen.Add(b730);
      //a700.SubCategorieen.Add(b740);
      //a700.SubCategorieen.Add(b750);
      //a700.SubCategorieen.Add(b760);
      //a700.SubCategorieen.Add(b770);
      //a700.SubCategorieen.Add(b780);
      //a700.SubCategorieen.Add(b790);

      //// Alle categorieën toevoegen aan de databank.
      //context.Categorieen.Add(c711);
      //context.Categorieen.Add(b710);

      //context.Categorieen.Add(c721);
      //context.Categorieen.Add(b720);

      //context.Categorieen.Add(c731);
      //context.Categorieen.Add(c732);
      //context.Categorieen.Add(c733);
      //context.Categorieen.Add(c734);
      //context.Categorieen.Add(b730);

      //context.Categorieen.Add(c741);
      //context.Categorieen.Add(b740);

      //context.Categorieen.Add(c751);
      //context.Categorieen.Add(b750);

      //context.Categorieen.Add(c761);
      //context.Categorieen.Add(b760);

      //context.Categorieen.Add(c771);
      //context.Categorieen.Add(b770);

      //context.Categorieen.Add(c781);
      //context.Categorieen.Add(b780);

      //context.Categorieen.Add(c791);
      //context.Categorieen.Add(b790);

      //context.Categorieen.Add(a700);

      //#endregion

      //#endregion

      //// 800. Cultuur en vrije tijd
      ////   810. Kunst- en cultuur
      ////     811. Musea
      ////     812. Cultuurcentrum
      ////     813. Openbare bibliotheken
      ////     814. Gemeenschapscentrum
      ////     815. Overige culturele instellingen
      ////     816. Feesten en plechtigheden
      ////     817. Overige evenementen
      ////     818. Monumentenzorg
      ////     819. Archeologie
      ////     8110. Overig beleid inzake het erfgoed
      ////     8111. Overig kunst- en cultuurbeleid
      ////   820. Sport
      ////     821. Sport
      ////   830. Jeugd
      ////     831. Jeugd
      ////   840. Erediensten  en niet-confessionele levensbeschouwelijke
      ////     841. Erediensten
      ////     842. Niet-confessionele levensbeschouwelijke gemeenschappen

      //#region 8. Cultuur en vrije tijd

      //#region Object initialisatie

      //Categorie c811 = new Categorie();
      //c811.CategorieNiveau = CategorieNiveau.C;
      //c811.CategorieId = 811;
      //c811.Naam = "Musea";

      //Categorie c812 = new Categorie();
      //c812.CategorieNiveau = CategorieNiveau.C;
      //c812.CategorieId = 812;
      //c812.Naam = "Cultuurcentrum";

      //Categorie c813 = new Categorie();
      //c813.CategorieNiveau = CategorieNiveau.C;
      //c813.CategorieId = 813;
      //c813.Naam = "Openbare bibliotheken";

      //Categorie c814 = new Categorie();
      //c814.CategorieNiveau = CategorieNiveau.C;
      //c814.CategorieId = 814;
      //c814.Naam = "Gemeenschapscentrum";

      //Categorie c815 = new Categorie();
      //c815.CategorieNiveau = CategorieNiveau.C;
      //c815.CategorieId = 815;
      //c815.Naam = "Overige culturele instellingen";

      //Categorie c816 = new Categorie();
      //c816.CategorieNiveau = CategorieNiveau.C;
      //c816.CategorieId = 816;
      //c816.Naam = "Feesten en plechtigheden";

      //Categorie c817 = new Categorie();
      //c817.CategorieNiveau = CategorieNiveau.C;
      //c817.CategorieId = 817;
      //c817.Naam = "Overige evenementen";

      //Categorie c818 = new Categorie();
      //c818.CategorieNiveau = CategorieNiveau.C;
      //c818.CategorieId = 818;
      //c818.Naam = "Monumentenzorg";

      //Categorie c819 = new Categorie();
      //c819.CategorieNiveau = CategorieNiveau.C;
      //c819.CategorieId = 819;
      //c819.Naam = "Archeologie";

      //Categorie c8110 = new Categorie();
      //c8110.CategorieNiveau = CategorieNiveau.C;
      //c8110.CategorieId = 8110;
      //c8110.Naam = "Overig beleid inzake het erfgoed";

      //Categorie c8111 = new Categorie();
      //c8111.CategorieNiveau = CategorieNiveau.C;
      //c8111.CategorieId = 8111;
      //c8111.Naam = "Overig kunst- en cultuurbeleid";

      //Categorie b810 = new Categorie();
      //b810.CategorieNiveau = CategorieNiveau.B;
      //b810.CategorieId = 810;
      //b810.Naam = "Kunst- en cultuur";

      //Categorie c821 = new Categorie();
      //c821.CategorieNiveau = CategorieNiveau.C;
      //c821.CategorieId = 821;
      //c821.Naam = "Sport";

      //Categorie b820 = new Categorie();
      //b820.CategorieNiveau = CategorieNiveau.B;
      //b820.CategorieId = 820;
      //b820.Naam = "Sport";

      //Categorie c831 = new Categorie();
      //c831.CategorieNiveau = CategorieNiveau.C;
      //c831.CategorieId = 831;
      //c831.Naam = "Jeugd";

      //Categorie b830 = new Categorie();
      //b830.CategorieNiveau = CategorieNiveau.B;
      //b830.CategorieId = 830;
      //b830.Naam = "Jeugd";

      //Categorie c841 = new Categorie();
      //c841.CategorieNiveau = CategorieNiveau.C;
      //c841.CategorieId = 841;
      //c841.Naam = "Erediensten";

      //Categorie c842 = new Categorie();
      //c842.CategorieNiveau = CategorieNiveau.C;
      //c842.CategorieId = 842;
      //c842.Naam = "Niet-confessionele levensbeschouwelijke gemeenschappen";

      //Categorie b840 = new Categorie();
      //b840.CategorieNiveau = CategorieNiveau.B;
      //b840.CategorieId = 840;
      //b840.Naam = "Erediensten  en niet-confessionele levensbeschouwelijke";

      //Categorie a800 = new Categorie();
      //a800.CategorieNiveau = CategorieNiveau.A;
      //a800.CategorieId = 800;
      //a800.Naam = "Cultuur en vrije tijd";

      //#endregion

      //#region Categorieën koppelen

      //// Alle categorie C objecten in de lijst van de respectievelijke categorie B plaatsen.
      //b810.SubCategorieen = new List<Categorie>();
      //b810.SubCategorieen.Add(c811);
      //b810.SubCategorieen.Add(c812);
      //b810.SubCategorieen.Add(c813);
      //b810.SubCategorieen.Add(c814);
      //b810.SubCategorieen.Add(c815);
      //b810.SubCategorieen.Add(c816);
      //b810.SubCategorieen.Add(c817);
      //b810.SubCategorieen.Add(c818);
      //b810.SubCategorieen.Add(c819);
      //b810.SubCategorieen.Add(c8110);
      //b810.SubCategorieen.Add(c8111);

      //b820.SubCategorieen = new List<Categorie>();
      //b820.SubCategorieen.Add(c821);

      //b830.SubCategorieen = new List<Categorie>();
      //b830.SubCategorieen.Add(c831);

      //b840.SubCategorieen = new List<Categorie>();
      //b840.SubCategorieen.Add(c841);
      //b840.SubCategorieen.Add(c842);

      //// Alle categorie B objecten in de lijst van de respectievelijke categorie A plaatsen.
      //a800.SubCategorieen = new List<Categorie>();
      //a800.SubCategorieen.Add(b810);
      //a800.SubCategorieen.Add(b820);
      //a800.SubCategorieen.Add(b830);
      //a800.SubCategorieen.Add(b840);

      //// Alle categorieën toevoegen aan de databank.
      //context.Categorieen.Add(c811);
      //context.Categorieen.Add(c812);
      //context.Categorieen.Add(c813);
      //context.Categorieen.Add(c814);
      //context.Categorieen.Add(c815);
      //context.Categorieen.Add(c816);
      //context.Categorieen.Add(c817);
      //context.Categorieen.Add(c818);
      //context.Categorieen.Add(c819);
      //context.Categorieen.Add(c8110);
      //context.Categorieen.Add(c8111);
      //context.Categorieen.Add(b810);

      //context.Categorieen.Add(c821);
      //context.Categorieen.Add(b820);

      //context.Categorieen.Add(c831);
      //context.Categorieen.Add(b830);

      //context.Categorieen.Add(c841);
      //context.Categorieen.Add(c842);
      //context.Categorieen.Add(b840);

      //context.Categorieen.Add(a800);

      //#endregion

      //#endregion

      //// 900. Leren en onderwijs
      ////   910. Basisonderwijs
      ////     911. Gewoon basisonderwijs
      ////     912. Buitengewoon basisonderwijs
      ////   920. Secundair onderwijs
      ////     921. Gewoon secundair onderwijs
      ////     922. Leren en werken
      ////     923. Buitengewoon secundair onderwijs
      ////   930. Deeltijds kunstonderwijs
      ////     931. Deeltijds kunstonderwijs
      ////   940. Volwassenenonderwijs
      ////     941. Centra voor volwassenenonderwijs
      ////   950. Hoger onderwijs
      ////     951. Hoger onderwijs
      ////   960. Ondersteunende diensten voor het onderwijs
      ////     961. Centra voor leerlingenbegeleiding
      ////     962. Administratieve dienst voor het onderwijs
      ////     963. Huisvesting voor schoolgaanden
      ////     964. Overige ondersteunende diensten voor het onderwijs
      ////   970. Sociale  en andere voordelen
      ////     971. Ochtend- en avondtoezicht
      ////     972. Leerlingenvervoer
      ////     973. Andere voordelen
      ////   980. Ondersteunende diensten voor het lokaal flankerend onderwijs
      ////     981. Administratieve dienst voor het lokaal flankerend onderwijs
      ////     982. Overige ondersteunende diensten voor het lokaal flankerend

      //#region 9. Leren en onderwijs

      //#region Object initialisatie

      //Categorie c911 = new Categorie();
      //c911.CategorieNiveau = CategorieNiveau.C;
      //c911.CategorieId = 911;
      //c911.Naam = "Gewoon basisonderwijs";

      //Categorie c912 = new Categorie();
      //c912.CategorieNiveau = CategorieNiveau.C;
      //c912.CategorieId = 912;
      //c912.Naam = "Buitengewoon basisonderwijs";

      //Categorie b910 = new Categorie();
      //b910.CategorieNiveau = CategorieNiveau.B;
      //b910.CategorieId = 910;
      //b910.Naam = "Basisonderwijs";

      //Categorie c921 = new Categorie();
      //c921.CategorieNiveau = CategorieNiveau.C;
      //c921.CategorieId = 921;
      //c921.Naam = "Gewoon secundair onderwijs";

      //Categorie c922 = new Categorie();
      //c922.CategorieNiveau = CategorieNiveau.C;
      //c922.CategorieId = 922;
      //c922.Naam = "Leren en werken";

      //Categorie c923 = new Categorie();
      //c923.CategorieNiveau = CategorieNiveau.C;
      //c923.CategorieId = 923;
      //c923.Naam = "Leren en werken";

      //Categorie b920 = new Categorie();
      //b920.CategorieNiveau = CategorieNiveau.B;
      //b920.CategorieId = 920;
      //b920.Naam = "Secundair onderwijs";

      //Categorie c931 = new Categorie();
      //c931.CategorieNiveau = CategorieNiveau.C;
      //c931.CategorieId = 931;
      //c931.Naam = "Deeltijds kunstonderwijs";

      //Categorie b930 = new Categorie();
      //b930.CategorieNiveau = CategorieNiveau.B;
      //b930.CategorieId = 930;
      //b930.Naam = "Deeltijds kunstonderwijs";

      //Categorie c941 = new Categorie();
      //c941.CategorieNiveau = CategorieNiveau.C;
      //c941.CategorieId = 941;
      //c941.Naam = "Centra voor volwassenenonderwijs";

      //Categorie b940 = new Categorie();
      //b940.CategorieNiveau = CategorieNiveau.B;
      //b940.CategorieId = 940;
      //b940.Naam = "Volwassenenonderwijs";

      //Categorie c951 = new Categorie();
      //c951.CategorieNiveau = CategorieNiveau.C;
      //c951.CategorieId = 951;
      //c951.Naam = "Hoger onderwijs";

      //Categorie b950 = new Categorie();
      //b950.CategorieNiveau = CategorieNiveau.B;
      //b950.CategorieId = 950;
      //b950.Naam = "Hoger onderwijs";

      //Categorie c961 = new Categorie();
      //c961.CategorieNiveau = CategorieNiveau.C;
      //c961.CategorieId = 961;
      //c961.Naam = "Centra voor leerlingenbegeleiding";

      //Categorie c962 = new Categorie();
      //c962.CategorieNiveau = CategorieNiveau.C;
      //c962.CategorieId = 962;
      //c962.Naam = "Administratieve dienst voor het onderwijs";

      //Categorie c963 = new Categorie();
      //c963.CategorieNiveau = CategorieNiveau.C;
      //c963.CategorieId = 963;
      //c963.Naam = "Huisvesting voor schoolgaanden";

      //Categorie c964 = new Categorie();
      //c964.CategorieNiveau = CategorieNiveau.C;
      //c964.CategorieId = 964;
      //c964.Naam = "Overige ondersteunende diensten voor het onderwijs";

      //Categorie b960 = new Categorie();
      //b960.CategorieNiveau = CategorieNiveau.B;
      //b960.CategorieId = 960;
      //b960.Naam = "Ondersteunende diensten voor het onderwijs";

      //Categorie c971 = new Categorie();
      //c971.CategorieNiveau = CategorieNiveau.C;
      //c971.CategorieId = 971;
      //c971.Naam = "Ochtend- en avondtoezicht";

      //Categorie c972 = new Categorie();
      //c972.CategorieNiveau = CategorieNiveau.C;
      //c972.CategorieId = 972;
      //c972.Naam = "Leerlingenvervoer";

      //Categorie c973 = new Categorie();
      //c973.CategorieNiveau = CategorieNiveau.C;
      //c973.CategorieId = 973;
      //c973.Naam = "Andere voordelen";

      //Categorie b970 = new Categorie();
      //b970.CategorieNiveau = CategorieNiveau.B;
      //b970.CategorieId = 970;
      //b970.Naam = "Sociale  en andere voordelen";

      //Categorie c981 = new Categorie();
      //c981.CategorieNiveau = CategorieNiveau.C;
      //c981.CategorieId = 981;
      //c981.Naam = "Administratieve dienst voor het lokaal flankerend onderwijs";

      //Categorie c982 = new Categorie();
      //c982.CategorieNiveau = CategorieNiveau.C;
      //c982.CategorieId = 982;
      //c982.Naam = "Overige ondersteunende diensten voor het lokaal flankerend";

      //Categorie b980 = new Categorie();
      //b980.CategorieNiveau = CategorieNiveau.B;
      //b980.CategorieId = 980;
      //b980.Naam = "Ondersteunende diensten voor het lokaal flankerend onderwijs";

      //Categorie a900 = new Categorie();
      //a900.CategorieNiveau = CategorieNiveau.A;
      //a900.CategorieId = 900;
      //a900.Naam = "Leren en onderwijs";

      //#endregion

      //#region Categorieën koppelen

      //// Alle categorie C objecten in de lijst van de respectievelijke categorie B plaatsen.
      //b910.SubCategorieen = new List<Categorie>();
      //b910.SubCategorieen.Add(c911);
      //b910.SubCategorieen.Add(c912);

      //b920.SubCategorieen = new List<Categorie>();
      //b920.SubCategorieen.Add(c921);
      //b920.SubCategorieen.Add(c922);
      //b920.SubCategorieen.Add(c923);

      //b930.SubCategorieen = new List<Categorie>();
      //b930.SubCategorieen.Add(c931);

      //b940.SubCategorieen = new List<Categorie>();
      //b940.SubCategorieen.Add(c941);

      //b950.SubCategorieen = new List<Categorie>();
      //b950.SubCategorieen.Add(c951);

      //b960.SubCategorieen = new List<Categorie>();
      //b960.SubCategorieen.Add(c961);
      //b960.SubCategorieen.Add(c962);
      //b960.SubCategorieen.Add(c963);
      //b960.SubCategorieen.Add(c964);

      //b970.SubCategorieen = new List<Categorie>();
      //b970.SubCategorieen.Add(c971);
      //b970.SubCategorieen.Add(c972);
      //b970.SubCategorieen.Add(c973);

      //b980.SubCategorieen = new List<Categorie>();
      //b980.SubCategorieen.Add(c981);
      //b980.SubCategorieen.Add(c982);

      //// Alle categorie B objecten in de lijst van de respectievelijke categorie A plaatsen.
      //a900.SubCategorieen = new List<Categorie>();
      //a900.SubCategorieen.Add(b910);
      //a900.SubCategorieen.Add(b920);
      //a900.SubCategorieen.Add(b930);
      //a900.SubCategorieen.Add(b940);
      //a900.SubCategorieen.Add(b950);
      //a900.SubCategorieen.Add(b960);
      //a900.SubCategorieen.Add(b970);
      //a900.SubCategorieen.Add(b980);

      //// Alle categorieën toevoegen aan de databank.
      //context.Categorieen.Add(c911);
      //context.Categorieen.Add(c912);
      //context.Categorieen.Add(b910);

      //context.Categorieen.Add(c921);
      //context.Categorieen.Add(c922);
      //context.Categorieen.Add(c923);
      //context.Categorieen.Add(b920);

      //context.Categorieen.Add(c931);
      //context.Categorieen.Add(b930);

      //context.Categorieen.Add(c941);
      //context.Categorieen.Add(b940);

      //context.Categorieen.Add(c951);
      //context.Categorieen.Add(b950);

      //context.Categorieen.Add(c961);
      //context.Categorieen.Add(c962);
      //context.Categorieen.Add(c963);
      //context.Categorieen.Add(c964);
      //context.Categorieen.Add(b960);

      //context.Categorieen.Add(c971);
      //context.Categorieen.Add(c972);
      //context.Categorieen.Add(c973);
      //context.Categorieen.Add(b970);

      //context.Categorieen.Add(c981);
      //context.Categorieen.Add(c982);
      //context.Categorieen.Add(b980);

      //context.Categorieen.Add(a900);

      //#endregion

      //#endregion

      //// 1000. Zorg en opvang
      ////   1010. Sociaal beleid
      ////     1011. Sociale bijstand
      ////     1012. Voorschotten
      ////     1013. Integratie van personen met vreemde herkomst
      ////     1014. Lokale opvanginitiatieven voor asielzoekers
      ////     1015. Activering van tewerkstelling
      ////     1016. Dienst voor juridische informatie en advies
      ////     1017. Overige verrichtingen inzake sociaal beleid
      ////   1020. Ziekte- en invaliditeit
      ////     1021. Diensten en voorzieningen voor personen met een handicap
      ////     1022. Overige activiteiten inzake ziekte en invaliditeit
      ////   1030. Sociale huisvesting
      ////     1031. Sociale huisvesting
      ////   1040. Gezin en kinderen
      ////     1041. Gezinshulp
      ////     1042. Opvoedingsondersteuning
      ////     1043. Kinderopvang
      ////     1044. Thuisbezorgde maaltijden
      ////     1045. Klusjesdienst
      ////     1046. Poetsdienst
      ////     1047. Overige gezinshulp
      ////   1050. Ouderen
      ////     1051. Ouderenwoningen
      ////     1052. Dienstencentra
      ////     1053. Assistentiewoningen
      ////     1054. Woon- en zorgcentra
      ////     1055. Dagzorgcentra
      ////     1056. Overige verrichtingen betreffende ouderen
      ////   1060. Dienstverlening inzake volksgezondheid
      ////     1061. Sociale geneeskunde
      ////     1062. Ziekenhuizen
      ////     1063. Ontsmetting en openbare reiniging
      ////     1064. Gezondheidspromotie en ziektepreventie
      ////     1065. Eerstelijnsgezondheidszorg
      ////     1066. Overige dienstverlening inzake volksgezondheid
      ////   1070. Begraafplaatsen, crematoria en lijkbezorging
      ////     1071. Begraafplaatsen
      ////     1072. Lijkbezorging

      //#region 10. Zorg en opvang

      //#region Object initialisatie

      //Categorie c1011 = new Categorie();
      //c1011.CategorieNiveau = CategorieNiveau.C;
      //c1011.CategorieId = 1011;
      //c1011.Naam = "Sociale bijstand";

      //Categorie c1012 = new Categorie();
      //c1012.CategorieNiveau = CategorieNiveau.C;
      //c1012.CategorieId = 1012;
      //c1012.Naam = "Voorschotten";

      //Categorie c1013 = new Categorie();
      //c1013.CategorieNiveau = CategorieNiveau.C;
      //c1013.CategorieId = 1013;
      //c1013.Naam = "Integratie van personen met vreemde herkomst";

      //Categorie c1014 = new Categorie();
      //c1014.CategorieNiveau = CategorieNiveau.C;
      //c1014.CategorieId = 1014;
      //c1014.Naam = "Lokale opvanginitiatieven voor asielzoekers";

      //Categorie c1015 = new Categorie();
      //c1015.CategorieNiveau = CategorieNiveau.C;
      //c1015.CategorieId = 1015;
      //c1015.Naam = "Activering van tewerkstelling";

      //Categorie c1016 = new Categorie();
      //c1016.CategorieNiveau = CategorieNiveau.C;
      //c1016.CategorieId = 1016;
      //c1016.Naam = "Dienst voor juridische informatie en advies";

      //Categorie c1017 = new Categorie();
      //c1017.CategorieNiveau = CategorieNiveau.C;
      //c1017.CategorieId = 1017;
      //c1017.Naam = "Overige verrichtingen inzake sociaal beleid";

      //Categorie b1010 = new Categorie();
      //b1010.CategorieNiveau = CategorieNiveau.B;
      //b1010.CategorieId = 1010;
      //b1010.Naam = "Sociaal beleid";

      //Categorie c1021 = new Categorie();
      //c1021.CategorieNiveau = CategorieNiveau.C;
      //c1021.CategorieId = 1021;
      //c1021.Naam = "Diensten en voorzieningen voor personen met een handicap";

      //Categorie c1022 = new Categorie();
      //c1022.CategorieNiveau = CategorieNiveau.C;
      //c1022.CategorieId = 1022;
      //c1022.Naam = "Overige activiteiten inzake ziekte en invaliditeit";

      //Categorie b1020 = new Categorie();
      //b1020.CategorieNiveau = CategorieNiveau.B;
      //b1020.CategorieId = 1020;
      //b1020.Naam = "Ziekte- en invaliditeit";

      //Categorie c1031 = new Categorie();
      //c1031.CategorieNiveau = CategorieNiveau.C;
      //c1031.CategorieId = 1031;
      //c1031.Naam = "Sociale huisvesting";

      //Categorie b1030 = new Categorie();
      //b1030.CategorieNiveau = CategorieNiveau.B;
      //b1030.CategorieId = 1030;
      //b1030.Naam = "Sociale huisvesting";

      //Categorie c1041 = new Categorie();
      //c1041.CategorieNiveau = CategorieNiveau.C;
      //c1041.CategorieId = 1041;
      //c1041.Naam = "Gezinshulp";

      //Categorie c1042 = new Categorie();
      //c1042.CategorieNiveau = CategorieNiveau.C;
      //c1042.CategorieId = 1042;
      //c1042.Naam = "Opvoedingsondersteuning";

      //Categorie c1043 = new Categorie();
      //c1043.CategorieNiveau = CategorieNiveau.C;
      //c1043.CategorieId = 1043;
      //c1043.Naam = "Kinderopvang";

      //Categorie c1044 = new Categorie();
      //c1044.CategorieNiveau = CategorieNiveau.C;
      //c1044.CategorieId = 1044;
      //c1044.Naam = "Thuisbezorgde maaltijden";

      //Categorie c1045 = new Categorie();
      //c1045.CategorieNiveau = CategorieNiveau.C;
      //c1045.CategorieId = 1045;
      //c1045.Naam = "Klusjesdienst";

      //Categorie c1046 = new Categorie();
      //c1046.CategorieNiveau = CategorieNiveau.C;
      //c1046.CategorieId = 1046;
      //c1046.Naam = "Poetsdienst";

      //Categorie c1047 = new Categorie();
      //c1047.CategorieNiveau = CategorieNiveau.C;
      //c1047.CategorieId = 1047;
      //c1047.Naam = "Overige gezinshulp";

      //Categorie b1040 = new Categorie();
      //b1040.CategorieNiveau = CategorieNiveau.B;
      //b1040.CategorieId = 1030;
      //b1040.Naam = "Gezin en kinderen";

      //Categorie c1051 = new Categorie();
      //c1051.CategorieNiveau = CategorieNiveau.C;
      //c1051.CategorieId = 1051;
      //c1051.Naam = "Ouderenwoningen";

      //Categorie c1052 = new Categorie();
      //c1052.CategorieNiveau = CategorieNiveau.C;
      //c1052.CategorieId = 1052;
      //c1052.Naam = "Dienstencentra";

      //Categorie c1053 = new Categorie();
      //c1053.CategorieNiveau = CategorieNiveau.C;
      //c1053.CategorieId = 1053;
      //c1053.Naam = "Assistentiewoningen";

      //Categorie c1054 = new Categorie();
      //c1054.CategorieNiveau = CategorieNiveau.C;
      //c1054.CategorieId = 1054;
      //c1054.Naam = "Woon- en zorgcentra";

      //Categorie c1055 = new Categorie();
      //c1055.CategorieNiveau = CategorieNiveau.C;
      //c1055.CategorieId = 1055;
      //c1055.Naam = "Dagzorgcentra";

      //Categorie c1056 = new Categorie();
      //c1056.CategorieNiveau = CategorieNiveau.C;
      //c1056.CategorieId = 1056;
      //c1056.Naam = "Overige verrichtingen betreffende ouderen";

      //Categorie b1050 = new Categorie();
      //b1050.CategorieNiveau = CategorieNiveau.B;
      //b1050.CategorieId = 1050;
      //b1050.Naam = "Ouderen";

      //Categorie c1061 = new Categorie();
      //c1061.CategorieNiveau = CategorieNiveau.C;
      //c1061.CategorieId = 1061;
      //c1061.Naam = "Sociale geneeskunde";

      //Categorie c1062 = new Categorie();
      //c1062.CategorieNiveau = CategorieNiveau.C;
      //c1062.CategorieId = 1062;
      //c1062.Naam = "Ziekenhuizen";

      //Categorie c1063 = new Categorie();
      //c1063.CategorieNiveau = CategorieNiveau.C;
      //c1063.CategorieId = 1063;
      //c1063.Naam = "Ontsmetting en openbare reiniging";

      //Categorie c1064 = new Categorie();
      //c1064.CategorieNiveau = CategorieNiveau.C;
      //c1064.CategorieId = 1064;
      //c1064.Naam = "Gezondheidspromotie en ziektepreventie";

      //Categorie c1065 = new Categorie();
      //c1065.CategorieNiveau = CategorieNiveau.C;
      //c1065.CategorieId = 1065;
      //c1065.Naam = "Eerstelijnsgezondheidszorg";

      //Categorie c1066 = new Categorie();
      //c1066.CategorieNiveau = CategorieNiveau.C;
      //c1066.CategorieId = 1066;
      //c1066.Naam = "Overige dienstverlening inzake volksgezondheid";

      //Categorie b1060 = new Categorie();
      //b1060.CategorieNiveau = CategorieNiveau.B;
      //b1060.CategorieId = 1060;
      //b1060.Naam = "Dienstverlening inzake volksgezondheid";

      //Categorie c1071 = new Categorie();
      //c1071.CategorieNiveau = CategorieNiveau.C;
      //c1071.CategorieId = 1071;
      //c1071.Naam = "Begraafplaatsen";

      //Categorie c1072 = new Categorie();
      //c1072.CategorieNiveau = CategorieNiveau.C;
      //c1072.CategorieId = 1072;
      //c1072.Naam = "Lijkbezorging";

      //Categorie b1070 = new Categorie();
      //b1070.CategorieNiveau = CategorieNiveau.B;
      //b1070.CategorieId = 1070;
      //b1070.Naam = "Begraafplaatsen, crematoria en lijkbezorging";

      //Categorie a1000 = new Categorie();
      //a1000.CategorieNiveau = CategorieNiveau.A;
      //a1000.CategorieId = 1000;
      //a1000.Naam = "Zorg en opvang";

      //#endregion

      //#region Categorieën koppelen

      //// Alle categorie C objecten in de lijst van de respectievelijke categorie B plaatsen.
      //b1010.SubCategorieen = new List<Categorie>();
      //b1010.SubCategorieen.Add(c1011);
      //b1010.SubCategorieen.Add(c1012);
      //b1010.SubCategorieen.Add(c1013);
      //b1010.SubCategorieen.Add(c1014);
      //b1010.SubCategorieen.Add(c1015);
      //b1010.SubCategorieen.Add(c1016);
      //b1010.SubCategorieen.Add(c1017);

      //b1020.SubCategorieen = new List<Categorie>();
      //b1020.SubCategorieen.Add(c1021);
      //b1020.SubCategorieen.Add(c1022);

      //b1030.SubCategorieen = new List<Categorie>();
      //b1030.SubCategorieen.Add(c1031);

      //b1040.SubCategorieen = new List<Categorie>();
      //b1040.SubCategorieen.Add(c1041);
      //b1040.SubCategorieen.Add(c1042);
      //b1040.SubCategorieen.Add(c1043);
      //b1040.SubCategorieen.Add(c1044);
      //b1040.SubCategorieen.Add(c1045);
      //b1040.SubCategorieen.Add(c1046);
      //b1040.SubCategorieen.Add(c1047);

      //b1050.SubCategorieen = new List<Categorie>();
      //b1050.SubCategorieen.Add(c1051);
      //b1050.SubCategorieen.Add(c1052);
      //b1050.SubCategorieen.Add(c1053);
      //b1050.SubCategorieen.Add(c1054);
      //b1050.SubCategorieen.Add(c1055);
      //b1050.SubCategorieen.Add(c1056);

      //b1060.SubCategorieen = new List<Categorie>();
      //b1060.SubCategorieen.Add(c1061);
      //b1060.SubCategorieen.Add(c1062);
      //b1060.SubCategorieen.Add(c1063);
      //b1060.SubCategorieen.Add(c1064);
      //b1060.SubCategorieen.Add(c1065);
      //b1060.SubCategorieen.Add(c1066);

      //b1070.SubCategorieen = new List<Categorie>();
      //b1070.SubCategorieen.Add(c1071);
      //b1070.SubCategorieen.Add(c1072);

      //// Alle categorie B objecten in de lijst van de respectievelijke categorie A plaatsen.
      //a1000.SubCategorieen = new List<Categorie>();
      //a1000.SubCategorieen.Add(b1010);
      //a1000.SubCategorieen.Add(b1020);
      //a1000.SubCategorieen.Add(b1030);
      //a1000.SubCategorieen.Add(b1040);
      //a1000.SubCategorieen.Add(b1050);
      //a1000.SubCategorieen.Add(b1060);
      //a1000.SubCategorieen.Add(b1070);

      //// Alle categorieën toevoegen aan de databank.
      //context.Categorieen.Add(c1011);
      //context.Categorieen.Add(c1012);
      //context.Categorieen.Add(c1013);
      //context.Categorieen.Add(c1014);
      //context.Categorieen.Add(c1015);
      //context.Categorieen.Add(c1016);
      //context.Categorieen.Add(c1017);
      //context.Categorieen.Add(b1010);

      //context.Categorieen.Add(c1021);
      //context.Categorieen.Add(c1022);
      //context.Categorieen.Add(b1020);

      //context.Categorieen.Add(c1031);
      //context.Categorieen.Add(b1030);

      //context.Categorieen.Add(c1041);
      //context.Categorieen.Add(c1042);
      //context.Categorieen.Add(c1043);
      //context.Categorieen.Add(c1044);
      //context.Categorieen.Add(c1045);
      //context.Categorieen.Add(c1046);
      //context.Categorieen.Add(b1040);

      //context.Categorieen.Add(c1051);
      //context.Categorieen.Add(c1052);
      //context.Categorieen.Add(c1053);
      //context.Categorieen.Add(c1054);
      //context.Categorieen.Add(c1055);
      //context.Categorieen.Add(c1056);
      //context.Categorieen.Add(b1050);

      //context.Categorieen.Add(c1061);
      //context.Categorieen.Add(c1062);
      //context.Categorieen.Add(c1063);
      //context.Categorieen.Add(c1064);
      //context.Categorieen.Add(c1065);
      //context.Categorieen.Add(c1066);
      //context.Categorieen.Add(b1060);

      //context.Categorieen.Add(c1071);
      //context.Categorieen.Add(c1072);
      //context.Categorieen.Add(b1070);

      //context.Categorieen.Add(a1000);

      //#endregion

      //#endregion


      //#endregion
      //context.SaveChanges();

  
  

    }
  }
}
