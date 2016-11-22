$(document).ready(init);

function init() {
    initialize();
    $("#show-cluster-avg").change(showClusterAvg)
    //Layout juist zetten als laad
    if (parseInt($("#city-right").text()) == 9999) {
        $("#show-cluster-avg").prop("checked", true);
        $("#city-2").prop('disabled', true);
        $("#city-2-year").prop('disabled', true);
    } else {
        $("#show-cluster-avg").prop("checked", false);
        $("#city-2").prop('disabled', false);
        $("#city-2-year").prop('disabled', false);
    }
    if ($("#found-budget-left").text() == 'N') {
        $("#avg-input-group").hide();
    }
}
function showClusterAvg() {
    if ($(this).is(":checked")) {
        $("#city-2").prop('disabled', true);
        $("#city-2-year").prop('disabled', true);
        var city1 = parseInt($("#city-1").val().substr(0, 4));
        var city2 = 9999;
        var year1 = parseInt($('#city-1-year :selected').text());;

        if (isNaN(city1)) {
            city1 = parseInt($("#city-left").text())
        }
        if (isNaN(year1)) {
            year1 = parseInt($("#year-left").text())
            if (year1 == 0) {
                year1 = (new Date).getFullYear();
            }
        }
        window.location.href = '/Transparance/Compare/?zip1=' + city1 + '&zip2=' + city2 + '&year1=' + year1 + '&year2=' + year1;
    } else {
        $("#city-2").prop('disabled', false);
        $("#city-2-year").prop('disabled', false);
    }
};
function initialize() {
    $('#city-1-year').val((new Date).getFullYear().toString());
    $('#city-2-year').val((new Date).getFullYear().toString());
    $('.btn-compare-city').on("click", getBudget);
}

function getBudget() {
    var city1 = parseInt($("#city-1").val().substr(0, 4));
    var city2 = parseInt($("#city-2").val().substr(0, 4));
    var year1 = parseInt($('#city-1-year :selected').text());;
    var year2 = parseInt($('#city-2-year :selected').text());;
    if (isNaN(city1)) {
        city1 = parseInt($("#city-left").text())
    }
    if (isNaN(city2)) {
        city2 = parseInt($("#city-right").text())
    }
    if (isNaN(year1)) {
        year1 = parseInt($("#year-left").text())
        if (year1 == 0) {
            year1 = (new Date).getFullYear();
        }
    }
    if (isNaN(year2)) {
        year2 = parseInt($("#year-right").text())
        if (year2 == 0) {
            year2 = (new Date).getFullYear();
        }
    }

    window.location.href = '/Transparance/Compare/?zip1=' + city1 + '&zip2=' + city2 + '&year1=' + year1 + '&year2=' + year2;
}