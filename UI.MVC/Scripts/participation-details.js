$(window).load(init());

function init() {
    initialize();
    addBudgetToggleListeners();
    //Hide b & c standaard
    $(".level-b").hide();
    $(".level-c").hide();
    $(".arrow-a").on("click", showsubniveauA);
    $(".arrow-b").on("click", showsubniveauB);
    $('#budgetchart').bind("DOMSubtreeModified", showDifference);
}
function showDifference() {
    //Verander kleur bij veranderingen
    if ((parseFloat($("#BudgetChangeText").text().substr(2)) > 0)) {
        $("#BudgetChangeText").css("color", "green").css("font-weight", "bold");;
    }
    else if ((parseFloat($("#BudgetChangeText").text().substr(2)) < 0)) {
        $("#BudgetChangeText").css("color", "red").css("font-weight", "bold");
    } else {
        $("#BudgetChangeText").css("color", "blue");
    }
}

function initialize() {
    //For each
    $(".slider").each(function (index) {
        // Sliders met tooltip initialiseren.
        $(this).slider({
            formatter: function (value) {
                return 'Current value: ' + value;
            },
            animate: "fast"
        }).css(".slider-selection", "rgba(129, 212, 115, 1)");
        $(this).slider("disable");
    })
}

function addBudgetToggleListeners() {
    $(".budget-link").click(function () {
        if (($(this).html()).toString().trim() === "Meer info") {
            $(this).html("Minder info");
        } else {
            $(this).html("Meer info");
        }
    });
}

function showsubniveauA() {
    var parentId = $(this).parent().attr('id');

    if ($(this).children("span").hasClass("glyphicon-chevron-up")) {
        $(this).children("span").removeClass("glyphicon-chevron-up").addClass("glyphicon-chevron-down");
        $(".level-b").each(function () {
            var currentAId = $("#" + $(this).attr("id") + "-id-a").text();
            if (currentAId == parentId) {
                $(this).show();
            }
        });
    } else {
        $(this).children("span").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-up");
        $(".level-b").each(function () {
            var currentAId = $("#" + $(this).attr("id") + "-id-a").text();
            if (currentAId == parentId) {
                $(this).hide(function () {
                    if ($(this).children("span").hasClass("glyphicon-chevron-down")) {
                        $(this).children("span").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-up");
                    };
                });
            }
        });
        $(".level-c").each(function () {
            var currentAId = $("#" + $(this).attr("id") + "-id-a").text();
            if (currentAId == parentId) {
                $(this).hide();
            }
        });
    }
};

function showsubniveauB() {
    var parentId = $(this).parent().attr('id');

    if ($(this).children("span").hasClass("glyphicon-chevron-up")) {
        $(this).children("span").removeClass("glyphicon-chevron-up").addClass("glyphicon-chevron-down");
        $(".level-c").each(function () {
            var currentBId = $("#" + $(this).attr("id") + "-id-b").text();
            if (currentBId == parentId) {
                $(this).show();
            }
        });
    } else {
        $(this).children("span").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-up");
        $(".level-c").each(function () {
            var currentBId = $("#" + $(this).attr("id") + "-id-b").text();
            if (currentBId == parentId) {
                $(this).hide();
            }
        });
    }
};

