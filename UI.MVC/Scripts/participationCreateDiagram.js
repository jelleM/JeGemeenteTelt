$(document).ready(function () {


    /*
         * D3.JS
         * -----
         * belangrijkste functionaliteiten:
         *   d3.select("element")            selecteren van 1ste element
         *   d3.selectAll("element")         selecteren van alle elementen
         *   selection.append("element")     toevoegen van een element
         *   selection.attr("key",["value"]) attribuut toevoegen, aanpassen (zonder value = attribute getten)
         *   selection.on("event",function)  eventlistener toevoegen
         *   selection.transition.attr("key","value")    transition zorgt voor animatie
         * */


    // ---------------------------- //

    // OPSTARTVARIABELEN
    var w = 750, //width
        h = 350, //height
        r = w / 4, //radius
        ir = r / 4, // inner-radius
        pi = Math.PI, // PI
        color = d3.scale.category20c(), // color
        start = -90 * (pi / 180), // Start Angle
        stop = 90 * (pi / 180), // End Angle
        tempTitle = [], // tijdelijk <p> waarde opslagen (maakt het mogelijk om aangeklikte waarde terug te laten zien na hover)
        tempColor = []; // tijdelijk kleur opslagen van geselecteerde segmenten (om circle de juiste kleur te geven)

    // ---------------------------- //

    // JSON (json to array) 
    var arr = [];
    var city = $("#zip").text();

    $.getJSON("../Api/Budget/" + city, function (data) {

        for (var x in data) {
            arr.push(data[x]);
        }
        function addCommas(nStr) {
            nStr += '';
            x = nStr.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? ',' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + ' ' + '$2');
            }
            return x1 + x2;
        }
        function stylingBedrag(bedrag) {
            return "€ " + addCommas(bedrag);
        }

        function arrayMaker(a, x) {
            var arrNew = [];
            for (var obj in a) {
                if (x == null) {
                    if (a[obj].Categorie.CategorieNiveau == 1) {
                        arrNew.push(a[obj]);
                    }
                } else {
                    for (var subcat in x.Categorie.SubCategorieen) {
                        if (x.Categorie.SubCategorieen[subcat].CategorieId == a[obj].Categorie.CategorieId) {
                            arrNew.push(a[obj]);
                        }
                    }
                }
            }
            return arrNew;
        }

        function isInternetExplorer() {

            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE ");

            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))  // If Internet Explorer, return version number
            {
                return true;
            }
            return false;
        }

        // ---------------------------- //

        // BOOGDIAGRAM
        function makeArc(input, radius, innerRadius) {

            // SVG selecteren en element g toevoegen (met data)
            var gGroup = d3.select("#budgetchart")
                .select("svg")
                .data([input])
                //.attr("width", w)
                //.attr("height", h)
                .append("svg:g")
                .attr("transform", "translate(" + w / 2 + "," + (h - 40) + ")")
                .attr("class", "open"); // klasse open toevoegen (enkel klasse open kan geselecteerd worden)


            // inner en outer radius van de arc berekenen.
            var arc = d3.svg.arc()
                .outerRadius(radius)
                .innerRadius(innerRadius);

            // radiuses bereken bij mouseOver
            var arcExtended = d3.svg.arc()
                .outerRadius(radius + 20)
                .innerRadius(innerRadius);


            // Onderdeling in diagram maken
            var pie = d3.layout.pie()
                .value(function (d) {
                    return d.Bedrag;
                })
                .sort(null)
                .startAngle(start)
                .endAngle(stop);

            // toevoegen van path-objecten
            var arcs = gGroup.selectAll("g.slice");
            arcs.data(pie)
                .enter().append("svg:path")
                .attr("fill", function (d, i) {
                    return color(i); // toevoegen van kleur
                })
                .attr("d", arc)
                .on("mouseover", mouseover)
                .on("mouseout", mouseout)
                .on("click", click);

            // center button
            if (d3.select("circle").empty()) { // enkel aanmaken als het nog niet bestaat
                gGroup.append("circle")
                    .attr("cx", 0)
                    .attr("cy", 0)
                    .attr("r", ir - 20)
                    .attr("fill", "#999999")
                    .on("mouseover", circleover)
                    .on("mouseout", circleout)
                    .on("click", circleclick);
            }

            // p-element (tekst)
            if (d3.select("#budgetchart").select("h4").empty()) {
                d3.select("#budgetchart").append("h4").text("Totale uitgaven: " + stylingBedrag(arr[3])).attr("id", "BudgetText").attr("class","text-center");
                tempTitle.push("Totale uitgaven: " + stylingBedrag(arr[3]));
            }

            // ---------------------------- //

            // INTERACTIES
            function mouseover(d, i) {
                if (d3.select(this.parentNode).attr("class") == "open") { //enkel hoverbaar als klasse open.

                    d3.select(this).transition()
                        .attr("d", arcExtended);
                    // toon het geselecteerde element.
                    if (input[i].Naam == undefined) {
                        d3.select("#budgetchart").select("h4").text(input[i].Categorie.Naam + ": " + stylingBedrag(input[i].Bedrag));
                    } else {
                        d3.select("#budgetchart").select("h4").text(input[i].Naam + ": " + stylingBedrag(input[i].Bedrag));
                    }
                }
            }

            function mouseout(d) {
                d3.select(this).transition()
                    .attr("d", arc);
                d3.select("#budgetchart").select("h4").text(tempTitle[tempTitle.length - 1]); //<p> terug zetten naar de tempTitle
            }

            function click(d, i) {
                if (d3.select(this.parentNode).attr("class") == "open") { // enkel klasse open is klikbaar
                    tempTitle.push(d3.select("#budgetchart").select("h4").text()); // waarde geven aan temptitle
                    d3.selectAll("g").attr("class", "closed"); // klasse op closed zetten
                    if (d3.selectAll("g").size() < 3) {
                        makeArc(arrayMaker(arr[4], input[i]), radius * 1.2, radius);
                    }
                    // maak cirkel de kleur van het geselecteerde element
                    tempColor.push(d3.select("circle").attr("fill"));
                    d3.select("circle").transition().attr("fill", d3.select(this).attr("fill"));

                    if (d3.select("polygon").empty()) { // enkel aanmaken als het nog niet bestaat
                        gGroup.append("polygon")
                            .attr("points", "31.945,19.96 16.955,19.96 21.108,15.807 18.931,13.629 11.055,21.493 18.931,29.371 21.108,27.191 16.955,23.037 31.945,23.037 ")
                            .attr("fill", "#FFFFFF")
                            .attr("transform", "translate(-21.5,-21.5)")
                            .on("click", circleclick);
                    }

                    // op grijs zetten
                    d3.select(this.parentElement).selectAll("path").transition().attr("fill", function (d, x) {
                        if (i != x) {
                            return "#cccccc"; // toevoegen van kleur
                        } else {
                            return d3.select(this).attr("fill");
                        }
                    });
                }
            }

            function circleover(d) {
                if (d3.select(this).attr("fill") != "#999999") { // circle is enkel hoverable als hij niet grijs is
                    d3.select(this).transition().attr("r", ir - 13);
                }

            }

            function circleout(d) {
                d3.select(this).transition()
                    .attr("r", ir - 20);
            }

            function circleclick(d) {
                if (d3.selectAll("g")[0].length > 1) { // als er > 1 g-element is kan er een g-element worden verwijdert
                    d3.selectAll("g.open").remove(); // het (met klasse open) g-element verwijderen
                    d3.select(d3.selectAll("g.closed")[0][d3.selectAll("g.closed")[0].length - 1]).attr("class", "open"); // het vorige g-element terug op open zetten.
                    d3.select("circle").transition().attr("fill", tempColor.pop()); // circle terug op de voorgaande kleur zetten.

                    d3.select("#budgetchart").select("h4").text(tempTitle.pop());
                    d3.select("#budgetchart").select("h4").text(tempTitle[tempTitle.length - 1]); //<p> terug zetten naar de tempTitle

                    // op grijs zetten
                    d3.select(".open").selectAll("path").transition().attr("fill", function (d, i) {
                        return color(i); // toevoegen van kleur
                    });

                }

                if (d3.selectAll("g")[0].length < 2) {
                    d3.select("circle").attr("fill", "#999999");
                    d3.select("polygon").remove();
                }
            }
        }

        // ---------------------------- //

        // OPROEPING VAN DE FUNCTIE
        if (isInternetExplorer()) {
            d3.select("svg").attr("height", "50em").attr("margin", "auto");
        }

        if (arr.length != 0) {
            makeArc(arrayMaker(arr[4], null), r, ir);
            $("#salary").on("change", function () {
                d3.select("#budgetchart").select("h4").text("Totale uitgaven: " + stylingBedrag(arr[3]));
            });

        } else {
            d3.select("#budgetchart").append("p").attr("class", "text-center").text("Geen begroting gevonden.");
            d3.select("#budgetchart").select("svg").remove();
        }
    }).fail(function () {
        d3.select("#budgetchart").append("p").text("Geen begroting gevonden.");
    });
});
