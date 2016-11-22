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
        h = w, //height
        r = w / 4, //radius
        ir = r / 4, // inner-radius
        pi = Math.PI, // PI
        color = d3.scale.category20c(), // color
        start = -180 * (pi / 180), // Start Angle
        stop = 0 * (pi / 180), // End Angle
        stop2 = 180 * (pi / 180), // End Angle
        tempTitle = [], // tijdelijk <p> waarde opslagen (maakt het mogelijk om aangeklikte waarde terug te laten zien na hover)
        tempColor = []; // tijdelijk kleur opslagen van geselecteerde segmenten (om circle de juiste kleur te geven)

    // ---------------------------- //

    // JSON (json to array) 
    var arr = [];
    var arr2 = [];
    var city = $("#city-left").text();
    var city2 = $("#city-right").text();
    var year1 = $("#year-left").text();
    var year2 = $("#year-right").text();
    
    $.getJSON("../../Api/Budget/" + city + "/" + year1, function (data) {
        //Bij gemiddelde
        if (city2 == 9999) {
            $.getJSON("../../Api/Budget/" + city + "/" + year1 + "/" + true, function (data2) {

                for (var x in data) {
                    arr.push(data[x]);
                }

                for (var y in data2) {
                    arr2.push(data2[y]);
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
                    if (bedrag == null) {
                        return "0 €"
                    } else {
                        return (addCommas(bedrag) + " €").replace(".", ",");
                    }

                }

                function getBedrag(a, input) {
                    for (var obj in a) {
                        if (input.Categorie.CategorieId == a[obj].Categorie.CategorieId) {
                            return a[obj].Bedrag;
                        }
                    }
                    return null;
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
                function makeArc(input, radius, innerRadius, startangle, stopangle, leftRight) {    //left = 0 right = 1

                    var offset = 5;
                    if (leftRight == 0) {
                        offset = -5;
                    }
                    // SVG selecteren en element g toevoegen (met data)
                    var gGroup = d3.select("#budgetchart")
                        .select("svg")
                        .data([input])
                        //.attr("width", w)
                        //.attr("height", h)
                        .append("svg:g")
                        .attr("transform", "translate(" + ((w - 150) / 2 + offset) + "," + (h - 150) / 2 + ")")
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
                        .startAngle(startangle)
                        .endAngle(stopangle);

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
                            .attr("transform", "translate(" + 5 + "," + 0 + ")")
                            .on("mouseover", circleover)
                            .on("mouseout", circleout)
                            .on("click", circleclick);
                    }

                    // p-element (tekst)
                    if (d3.select("#budgetchart").select("h4").empty()) {
                        d3.select("#budgetchart").append("h4").text("Totale uitgaven: " + stylingBedrag(arr[3]) + " - " + stylingBedrag(arr2[3])).attr("id", "BudgetText").attr("class", "text-center");
                        tempTitle.push("Totale uitgaven: " + stylingBedrag(arr[3]) + " - " + stylingBedrag(arr2[3]));
                    }

                    // ---------------------------- //

                    // INTERACTIES
                    function mouseover(d, i) {
                        if (d3.select(this.parentNode).attr("class") == "open") { //enkel hoverbaar als klasse open.
                            // d3.select(this).transition()
                            //     .attr("d", arcExtended);
                            d3.select(d3.selectAll(".open").selectAll("path")[0][i]).transition()
                                .attr("d", arcExtended);
                            d3.select(d3.selectAll(".open").selectAll("path")[1][i]).transition()
                                .attr("d", arcExtended);

                            // toon het geselecteerde element.
                            if (input[i].Naam == undefined) {
                                d3.select("#budgetchart").select("h4").text(input[i].Categorie.Naam + ": " + stylingBedrag(getBedrag(arr[4], input[i])) + " - " + stylingBedrag(getBedrag(arr2[4], input[i])));
                            } else {
                                d3.select("#budgetchart").select("h4").text(input[i].Naam + ": " + stylingBedrag(getBedrag(arr[4], input[i])) + " - " + stylingBedrag(getBedrag(arr2[4], input[i])));
                            }
                        }
                    }

                    function mouseout(d, i) {
                        if (d3.select(this.parentNode).attr("class") == "open") {
                            d3.select(d3.selectAll(".open").selectAll("path")[0][i]).transition()
                                .attr("d", arc);
                            d3.select(d3.selectAll(".open").selectAll("path")[1][i]).transition()
                                .attr("d", arc);
                            d3.select("#budgetchart").select("h4").text(tempTitle[tempTitle.length - 1]); //<p> terug zetten naar de tempTitle
                        }
                    }

                    function click(d, i) {
                        if (d3.select(this.parentNode).attr("class") == "open") { // enkel klasse open is klikbaar

                            // op grijs zetten
                            d3.select(d3.selectAll(".open").selectAll("path")[0].parentNode).selectAll("path").transition().attr("fill", function (d, x) {
                                if (i != x) {
                                    return "#cccccc"; // toevoegen van kleur
                                } else {
                                    return d3.select(d3.selectAll(".open").selectAll("path")[0][i]).attr("fill");
                                }
                            });
                            d3.select(d3.selectAll(".open").selectAll("path")[1].parentNode).selectAll("path").transition().attr("fill", function (d, x) {
                                if (i != x) {
                                    return "#cccccc"; // toevoegen van kleur
                                } else {
                                    return d3.select(d3.selectAll(".open").selectAll("path")[1][i]).attr("fill");
                                }
                            });
                            d3.select(d3.selectAll(".open").selectAll("path")[0][i]).transition()
                                .attr("d", arc);
                            d3.select(d3.selectAll(".open").selectAll("path")[1][i]).transition()
                                .attr("d", arc);

                            tempTitle.push(d3.select("#budgetchart").select("h4").text()); // waarde geven aan temptitle
                            d3.selectAll("g").attr("class", "closed"); // klasse op closed zetten
                            if (d3.selectAll("g").size() < 6) {
                                makeArc(arrayMaker(arr[4], input[i]), radius * 1.2, radius, stop, start, 0);
                                makeArc(arrayMaker(arr2[4], input[i]), radius * 1.2, radius, stop, stop2, 1);
                            }
                            // maak cirkel de kleur van het geselecteerde element
                            tempColor.push(d3.select("circle").attr("fill"));
                            d3.select("circle").transition().attr("fill", d3.select(this).attr("fill"));

                            if (d3.select("polygon").empty()) { // enkel aanmaken als het nog niet bestaat
                                if (leftRight === 0) {
                                    gGroup.append("polygon")
                                    .attr("points", "31.945,19.96 16.955,19.96 21.108,15.807 18.931,13.629 11.055,21.493 18.931,29.371 21.108,27.191 16.955,23.037 31.945,23.037 ")
                                    .attr("fill", "#FFFFFF")
                                    .attr("transform", "translate(-16.5,-21.5)")
                                    .on("click", circleclick);
                                } else if (leftRight === 1) {
                                    gGroup.append("polygon")
                                    .attr("points", "31.945,19.96 16.955,19.96 21.108,15.807 18.931,13.629 11.055,21.493 18.931,29.371 21.108,27.191 16.955,23.037 31.945,23.037 ")
                                    .attr("fill", "#FFFFFF")
                                    .attr("transform", "translate(-24.5, -21.5)")
                                    .on("click", circleclick);
                                }
                            }
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
                        if (d3.selectAll("g")[0].length > 2) { // als er > 1 g-element is kan er een g-element worden verwijdert
                            d3.selectAll("g.open").remove(); // het (met klasse open) g-element verwijderen
                            d3.select(d3.selectAll("g.closed")[0][d3.selectAll("g.closed")[0].length - 1]).attr("class", "open"); // het vorige g-element terug op open zetten.
                            d3.select(d3.selectAll("g.closed")[0][d3.selectAll("g.closed")[0].length - 1]).attr("class", "open"); // het vorige g-element terug op open zetten.
                            d3.select("circle").transition().attr("fill", tempColor.pop()); // circle terug op de voorgaande kleur zetten.

                            d3.select("#budgetchart").select("h4").text(tempTitle.pop());
                            d3.select("#budgetchart").select("h4").text(tempTitle[tempTitle.length - 1]); //<p> terug zetten naar de tempTitle

                            // op grijs zetten
                            d3.select(d3.selectAll(".open").selectAll("path")[0].parentNode).selectAll("path").transition().attr("fill", function (d, i) {
                                return color(i); // toevoegen van kleur
                            });
                            d3.select(d3.selectAll(".open").selectAll("path")[1].parentNode).selectAll("path").transition().attr("fill", function (d, i) {
                                return color(i); // toevoegen van kleur
                            });

                        }

                        if (d3.selectAll("g")[0].length <= 2) {
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
                    makeArc(arrayMaker(arr[4], null), r, ir, stop, start, 0);
                    makeArc(arrayMaker(arr2[4], null), r, ir, stop, stop2, 1);
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
        } else if(city2.length > 0) {
            $.getJSON("../../Api/Budget/" + city2 + "/" + year2, function (data2) {

                for (var x in data) {
                    arr.push(data[x]);
                }

                for (var y in data2) {
                    arr2.push(data2[y]);
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
                    if (bedrag == null) {
                        return "0 €"
                    } else {
                        return (addCommas(bedrag) + " €").replace(".", ",");
                    }

                }

                function getBedrag(a, input) {
                    for (var obj in a) {
                        if (input.Categorie.CategorieId == a[obj].Categorie.CategorieId) {
                            return a[obj].Bedrag;
                        }
                    }
                    return null;
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
                function makeArc(input, radius, innerRadius, startangle, stopangle, leftRight) {    //left = 0 right = 1

                    var offset = 5;
                    if (leftRight == 0) {
                        offset = -5;
                    }
                    // SVG selecteren en element g toevoegen (met data)
                    var gGroup = d3.select("#budgetchart")
                        .select("svg")
                        .data([input])
                        //.attr("width", w)
                        //.attr("height", h)
                        .append("svg:g")
                        .attr("transform", "translate(" + ((w - 150) / 2 + offset) + "," + (h - 150) / 2 + ")")
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
                        .startAngle(startangle)
                        .endAngle(stopangle);

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
                            .attr("transform", "translate(" + 5 + "," + 0 + ")")
                            .on("mouseover", circleover)
                            .on("mouseout", circleout)
                            .on("click", circleclick);
                    }

                    // p-element (tekst)
                    if (d3.select("#budgetchart").select("h4").empty()) {
                        d3.select("#budgetchart").append("h4").text("Totale uitgaven: " + stylingBedrag(arr[3]) + " - " + stylingBedrag(arr2[3])).attr("id", "BudgetText").attr("class", "text-center");
                        tempTitle.push("Totale uitgaven: " + stylingBedrag(arr[3]) + " - " + stylingBedrag(arr2[3]));
                    }

                    // ---------------------------- //

                    // INTERACTIES
                    function mouseover(d, i) {
                        if (d3.select(this.parentNode).attr("class") == "open") { //enkel hoverbaar als klasse open.
                            // d3.select(this).transition()
                            //     .attr("d", arcExtended);
                            d3.select(d3.selectAll(".open").selectAll("path")[0][i]).transition()
                                .attr("d", arcExtended);
                            d3.select(d3.selectAll(".open").selectAll("path")[1][i]).transition()
                                .attr("d", arcExtended);

                            // toon het geselecteerde element.
                            if (input[i].Naam == undefined) {
                                d3.select("#budgetchart").select("h4").text(input[i].Categorie.Naam + ": " + stylingBedrag(getBedrag(arr[4], input[i])) + " - " + stylingBedrag(getBedrag(arr2[4], input[i])));
                            } else {
                                d3.select("#budgetchart").select("h4").text(input[i].Naam + ": " + stylingBedrag(getBedrag(arr[4], input[i])) + " - " + stylingBedrag(getBedrag(arr2[4], input[i])));
                            }
                        }
                    }

                    function mouseout(d, i) {
                        if (d3.select(this.parentNode).attr("class") == "open") {
                            d3.select(d3.selectAll(".open").selectAll("path")[0][i]).transition()
                                .attr("d", arc);
                            d3.select(d3.selectAll(".open").selectAll("path")[1][i]).transition()
                                .attr("d", arc);
                            d3.select("#budgetchart").select("h4").text(tempTitle[tempTitle.length - 1]); //<p> terug zetten naar de tempTitle
                        }
                    }

                    function click(d, i) {
                        if (d3.select(this.parentNode).attr("class") == "open") { // enkel klasse open is klikbaar

                            // op grijs zetten
                            d3.select(d3.selectAll(".open").selectAll("path")[0].parentNode).selectAll("path").transition().attr("fill", function (d, x) {
                                if (i != x) {
                                    return "#cccccc"; // toevoegen van kleur
                                } else {
                                    return d3.select(d3.selectAll(".open").selectAll("path")[0][i]).attr("fill");
                                }
                            });
                            d3.select(d3.selectAll(".open").selectAll("path")[1].parentNode).selectAll("path").transition().attr("fill", function (d, x) {
                                if (i != x) {
                                    return "#cccccc"; // toevoegen van kleur
                                } else {
                                    return d3.select(d3.selectAll(".open").selectAll("path")[1][i]).attr("fill");
                                }
                            });
                            d3.select(d3.selectAll(".open").selectAll("path")[0][i]).transition()
                                .attr("d", arc);
                            d3.select(d3.selectAll(".open").selectAll("path")[1][i]).transition()
                                .attr("d", arc);

                            tempTitle.push(d3.select("#budgetchart").select("h4").text()); // waarde geven aan temptitle
                            d3.selectAll("g").attr("class", "closed"); // klasse op closed zetten
                            if (d3.selectAll("g").size() < 6) {
                                makeArc(arrayMaker(arr[4], input[i]), radius * 1.2, radius, stop, start, 0);
                                makeArc(arrayMaker(arr2[4], input[i]), radius * 1.2, radius, stop, stop2, 1);
                            }
                            // maak cirkel de kleur van het geselecteerde element
                            tempColor.push(d3.select("circle").attr("fill"));
                            d3.select("circle").transition().attr("fill", d3.select(this).attr("fill"));

                            if (d3.select("polygon").empty()) { // enkel aanmaken als het nog niet bestaat
                                gGroup.append("polygon")
                                    .attr("points", "31.945,19.96 16.955,19.96 21.108,15.807 18.931,13.629 11.055,21.493 18.931,29.371 21.108,27.191 16.955,23.037 31.945,23.037 ")
                                    .attr("fill", "#FFFFFF")
                                    .attr("transform", "translate(-16.5,-21.5)")
                                    .on("click", circleclick);
                            }
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
                        if (d3.selectAll("g")[0].length > 2) { // als er > 1 g-element is kan er een g-element worden verwijdert
                            d3.selectAll("g.open").remove(); // het (met klasse open) g-element verwijderen
                            d3.select(d3.selectAll("g.closed")[0][d3.selectAll("g.closed")[0].length - 1]).attr("class", "open"); // het vorige g-element terug op open zetten.
                            d3.select(d3.selectAll("g.closed")[0][d3.selectAll("g.closed")[0].length - 1]).attr("class", "open"); // het vorige g-element terug op open zetten.
                            d3.select("circle").transition().attr("fill", tempColor.pop()); // circle terug op de voorgaande kleur zetten.

                            d3.select("#budgetchart").select("h4").text(tempTitle.pop());
                            d3.select("#budgetchart").select("h4").text(tempTitle[tempTitle.length - 1]); //<p> terug zetten naar de tempTitle

                            // op grijs zetten
                            d3.select(d3.selectAll(".open").selectAll("path")[0].parentNode).selectAll("path").transition().attr("fill", function (d, i) {
                                return color(i); // toevoegen van kleur
                            });
                            d3.select(d3.selectAll(".open").selectAll("path")[1].parentNode).selectAll("path").transition().attr("fill", function (d, i) {
                                return color(i); // toevoegen van kleur
                            });

                        }

                        if (d3.selectAll("g")[0].length <= 2) {
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
                    makeArc(arrayMaker(arr[4], null), r, ir, stop, start, 0);
                    makeArc(arrayMaker(arr2[4], null), r, ir, stop, stop2, 1);
                    $("#salary").on("change", function () {
                        d3.select("#budgetchart").select("h4").text("Totale uitgaven: " + stylingBedrag(arr[3]));
                    });

                } else {
                    d3.select("#budgetchart").append("p").attr("class", "text-center").text("Geen begroting gevonden.");
                    d3.select("#budgetchart").select("svg").remove();
                }
            }).fail(function () {
                d3.select("#budgetchart").append("p").text("Geen begroting gevonden.");
                $("#budgetchart").addClass("text-center");
            });
        }else{
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
            if (bedrag == null) {
                return "0 €"
            } else {
                return (addCommas(bedrag) + " €").replace(".", ",");
            }

        }

        function getBedrag(a, input) {
            for (var obj in a) {
                if (input.Categorie.CategorieId == a[obj].Categorie.CategorieId) {
                    return a[obj].Bedrag;
                }
            }
            return null;
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
        function makeArc(input, radius, innerRadius, startangle, stopangle, leftRight) {    //left = 0 right = 1

            var offset = 5;
            if (leftRight == 0) {
                offset = -5;
            }
            // SVG selecteren en element g toevoegen (met data)
            var gGroup = d3.select("#budgetchart")
                .select("svg")
                .data([input])
                //.attr("width", w)
                //.attr("height", h)
                .append("svg:g")
                .attr("transform", "translate(" + ((w - 150) / 2 + offset) + "," + (h - 150) / 2 + ")")
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
                .startAngle(startangle)
                .endAngle(stopangle);

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
                    .attr("transform", "translate(" + 5 + "," + 0 + ")")
                    .on("mouseover", circleover)
                    .on("mouseout", circleout)
                    .on("click", circleclick);
            }

            // p-element (tekst)
            if (d3.select("#budgetchart").select("h4").empty()) {
                d3.select("#budgetchart").append("h4").text("Totale uitgaven: " + stylingBedrag(arr[3]) + " - " + stylingBedrag(arr2[3])).attr("id", "BudgetText").attr("class", "text-center");
                tempTitle.push("Totale uitgaven: " + stylingBedrag(arr[3]) + " - " + stylingBedrag(arr2[3]));
            }

            // ---------------------------- //

            // INTERACTIES
            function mouseover(d, i) {
                if (d3.select(this.parentNode).attr("class") == "open") { //enkel hoverbaar als klasse open.
                    // d3.select(this).transition()
                    //     .attr("d", arcExtended);
                    d3.select(d3.selectAll(".open").selectAll("path")[0][i]).transition()
                        .attr("d", arcExtended);
                    d3.select(d3.selectAll(".open").selectAll("path")[1][i]).transition()
                        .attr("d", arcExtended);

                    // toon het geselecteerde element.
                    if (input[i].Naam == undefined) {
                        d3.select("#budgetchart").select("h4").text(input[i].Categorie.Naam + ": " + stylingBedrag(getBedrag(arr[4], input[i])) + " - " + stylingBedrag(getBedrag(arr2[4], input[i])));
                    } else {
                        d3.select("#budgetchart").select("h4").text(input[i].Naam + ": " + stylingBedrag(getBedrag(arr[4], input[i])) + " - " + stylingBedrag(getBedrag(arr2[4], input[i])));
                    }
                }
            }

            function mouseout(d, i) {
                if (d3.select(this.parentNode).attr("class") == "open") {
                    d3.select(d3.selectAll(".open").selectAll("path")[0][i]).transition()
                        .attr("d", arc);
                    d3.select(d3.selectAll(".open").selectAll("path")[1][i]).transition()
                        .attr("d", arc);
                    d3.select("#budgetchart").select("h4").text(tempTitle[tempTitle.length - 1]); //<p> terug zetten naar de tempTitle
                }
            }

            function click(d, i) {
                if (d3.select(this.parentNode).attr("class") == "open") { // enkel klasse open is klikbaar

                    // op grijs zetten
                    d3.select(d3.selectAll(".open").selectAll("path")[0].parentNode).selectAll("path").transition().attr("fill", function (d, x) {
                        if (i != x) {
                            return "#cccccc"; // toevoegen van kleur
                        } else {
                            return d3.select(d3.selectAll(".open").selectAll("path")[0][i]).attr("fill");
                        }
                    });
                    d3.select(d3.selectAll(".open").selectAll("path")[1].parentNode).selectAll("path").transition().attr("fill", function (d, x) {
                        if (i != x) {
                            return "#cccccc"; // toevoegen van kleur
                        } else {
                            return d3.select(d3.selectAll(".open").selectAll("path")[1][i]).attr("fill");
                        }
                    });
                    d3.select(d3.selectAll(".open").selectAll("path")[0][i]).transition()
                        .attr("d", arc);
                    d3.select(d3.selectAll(".open").selectAll("path")[1][i]).transition()
                        .attr("d", arc);

                    tempTitle.push(d3.select("#budgetchart").select("h4").text()); // waarde geven aan temptitle
                    d3.selectAll("g").attr("class", "closed"); // klasse op closed zetten
                    if (d3.selectAll("g").size() < 6) {
                        makeArc(arrayMaker(arr[4], input[i]), radius * 1.2, radius, stop, start, 0);
                        makeArc(arrayMaker(arr2[4], input[i]), radius * 1.2, radius, stop, stop2, 1);
                    }
                    // maak cirkel de kleur van het geselecteerde element
                    tempColor.push(d3.select("circle").attr("fill"));
                    d3.select("circle").transition().attr("fill", d3.select(this).attr("fill"));

                    if (d3.select("polygon").empty()) { // enkel aanmaken als het nog niet bestaat
                        if (leftRight == 0) {
                            gGroup.append("polygon")
                            .attr("points", "31.945,19.96 16.955,19.96 21.108,15.807 18.931,13.629 11.055,21.493 18.931,29.371 21.108,27.191 16.955,23.037 31.945,23.037 ")
                            .attr("fill", "#FFFFFF")
                            .attr("transform", "translate(-16.5,-21.5)")
                            .on("click", circleclick);
                        }else {
                            gGroup.append("polygon")
                           .attr("points", "31.945,19.96 16.955,19.96 21.108,15.807 18.931,13.629 11.055,21.493 18.931,29.371 21.108,27.191 16.955,23.037 31.945,23.037 ")
                           .attr("fill", "#FFFFFF")
                           .attr("transform", "translate(-26,-21.5)")
                           .on("click", circleclick);
                        }
                        
                    }
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
                if (d3.selectAll("g")[0].length > 2) { // als er > 1 g-element is kan er een g-element worden verwijdert
                    d3.selectAll("g.open").remove(); // het (met klasse open) g-element verwijderen
                    d3.select(d3.selectAll("g.closed")[0][d3.selectAll("g.closed")[0].length - 1]).attr("class", "open"); // het vorige g-element terug op open zetten.
                    d3.select(d3.selectAll("g.closed")[0][d3.selectAll("g.closed")[0].length - 1]).attr("class", "open"); // het vorige g-element terug op open zetten.
                    d3.select("circle").transition().attr("fill", tempColor.pop()); // circle terug op de voorgaande kleur zetten.

                    d3.select("#budgetchart").select("h4").text(tempTitle.pop());
                    d3.select("#budgetchart").select("h4").text(tempTitle[tempTitle.length - 1]); //<p> terug zetten naar de tempTitle

                    // op grijs zetten
                    d3.select(d3.selectAll(".open").selectAll("path")[0].parentNode).selectAll("path").transition().attr("fill", function (d, i) {
                        return color(i); // toevoegen van kleur
                    });
                    d3.select(d3.selectAll(".open").selectAll("path")[1].parentNode).selectAll("path").transition().attr("fill", function (d, i) {
                        return color(i); // toevoegen van kleur
                    });

                }

                if (d3.selectAll("g")[0].length <= 2) {
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
            makeArc(arrayMaker(arr[4], null), r, ir, stop, start, 0);
            makeArc(arrayMaker(arr2[4], null), r, ir, stop, stop2, 1);
            $("#salary").on("change", function () {
                d3.select("#budgetchart").select("h4").text("Totale uitgaven: " + stylingBedrag(arr[3]));
            });

        } else {
            d3.select("#budgetchart").append("p").attr("class", "text-center").text("Geen begroting gevonden.");
            d3.select("#budgetchart").select("svg").remove();
        }
    }
    }).fail(function () {
        d3.select("#budgetchart").append("p").text("Geen begroting gevonden.");
        $("#budgetchart").addClass("text-center");
    });
});
