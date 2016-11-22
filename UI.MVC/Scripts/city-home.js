$(document).ready(init);

function init() {
    mobileOrDesktop();
    addResponsivePanelButtonListener();
    addBudgetToggleListeners();

    //Hide b & c standaard
    $(".level-b").hide();
    $(".level-c").hide();
    $(".arrow-a").on("click", showsubniveauA);
    $(".arrow-b").on("click", showsubniveauB);
}

function mobileOrDesktop() {
    if ($(window).innerWidth() < 992) {
        changeClassForMobile();
    } else {
        changeClassForDesktop();
    }
}

function changeClassForMobile() {
    console.log("Switching to mobile version");
    if ($(window).innerWidth() < 992) {
        $(window).off("resize");

        $("#extra-info").removeClass().addClass("collapse");
        if ($("#extra-info").hasClass("collapse in")) {
            $("#info-toggle-text").html("Minder info");
            $("#info-toggle-glyphicon").removeClass("glyphicon-chevron-up").addClass("glyphicon-chevron-down");
        } else {
            $("#info-toggle-text").html("Meer info");
            $("#info-toggle-glyphicon").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-up");
        }
        $(window).on("resize", changeClassForDesktop);
    }
}

function changeClassForDesktop() {
    console.log("Switching to desktop version");
    if ($(window).innerWidth() >= 992) {
        $(window).off("resize");
        $("#extra-info").removeClass("collapsed").addClass("collapsed in");
        $(window).on("resize", changeClassForMobile);
    }
}

function addResponsivePanelButtonListener() {
    $("#info-link").click(function () {
        if ($("#info-toggle-text").html() === "Meer info") {
            $("#info-toggle-text").html("Minder info");
            $("#info-toggle-glyphicon").addClass("glyphicon-chevron-down").removeClass("glyphicon-chevron-up");
        } else {
            $("#info-toggle-text").html("Meer info");
            $("#info-toggle-glyphicon").addClass("glyphicon-chevron-up").removeClass("glyphicon-chevron-down");
        }
    });
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
