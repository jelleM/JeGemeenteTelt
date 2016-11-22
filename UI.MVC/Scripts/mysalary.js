$(document).ready(init);
var type;

function init() {
    $("#calc-salary").on("click", salaryChanged);
    //Hide b & c standaard
    $(".level-b").hide();
    $(".level-c").hide();
    $(".arrow-a").on("click", showsubniveauA);
    $(".arrow-b").on("click", showsubniveauB);
    $("#workertype").on("change", typeChanged);
    $("#workertype").val($("#worktypehidden").text());
    $("#my-salary-input").on("change", typeChanged);
}
function typeChanged() {
    type = $("#workertype :selected").text();
}

function salaryChanged() {
    var salary = parseInt($("#salary").val());
    var zip = parseInt($("#city").text());
    if (isNaN(salary)) {
        alert("Gelieve een geldig salaris in te voeren.");
    }
    window.location.href = '/Transparance/MySalary?zip=' + zip + '&type=' + type + '&mySalary=' + salary;
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

