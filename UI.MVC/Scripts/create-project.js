$(document).ready(init);

function init() {
    $('#Type').change(selected);
    $('#budget-year').val($("#selected-year").text());
    $('#budget-year').change(selectedYear);
    //Hide b & c standaard
    $(".level-b-cat").hide();
    $(".level-c-cat").hide();
    $(".arrow-a").on("click", showsubniveauA);
    $(".arrow-b").on("click", showsubniveauB);
    $(".modifiable-a").on("change", modifiableAClicked)
    $(".modifiable-b").on("change", modifiableBClicked)
    $("#Eindmoment").on("focusout",checkEindmoment)
}
function checkEindmoment() {
    if ($("#Eindmoment").val() < $("#Startmoment").val()) {
        alert("Eindmoment kan niet voor startmoment liggen.")
    }
}
function selectedYear() {
    var content = $("#budget-year :selected").text();
    window.location.href = '/manage/createproject/?year=' + content;
}
function selected() {
    var content = $("#Type :selected").text();
    if (content.toLowerCase() == "herschikking") {
        $("#Bedrag").val(0);
        $("#Bedrag").prop('disabled', true);
    } else {
        $("#Bedrag").prop('disabled', false);
    }
}
function modifiableAClicked() {
    var parentId = $(this).parent().parent().parent().attr('id')
    var valueA = $(this).val();
    $(".modifiable-b").each(function () {
        var currentAId = $("#" + $(this).parent().parent().parent().attr("id") + "-id-a").text();
        if (currentAId == parentId) {
            $(this).val(valueA);
        }
    })
    $(".modifiable-c").each(function () {
        var currentBId = $("#" + $(this).parent().parent().parent().attr("id") + "-id-a").text();
        if (currentBId == parentId) {
            $(this).val(valueA);
        }
    })
}
function modifiableBClicked() {
    var parentId = $(this).parent().parent().parent().attr('id')
    var valueB = $(this).val();
    $(".modifiable-c").each(function () {
        var currentBId = $("#" + $(this).parent().parent().parent().attr("id") + "-id-b").text();
        if (currentBId == parentId) {
            $(this).val(valueB);
        }
    })
}
function showsubniveauA() {
    var parentId = $(this).parent().parent().parent().attr('id')
    if ($(this).hasClass("glyphicon-arrow-down")) {
        $(this).removeClass("glyphicon-arrow-down").addClass("glyphicon-arrow-up");
        $(".level-b-cat").each(function () {
            var currentAId = $("#" + $(this).attr("id") + "-id-a").text();
            if (currentAId == parentId) {
                $(this).show();
            }
        })
    } else {
        $(this).removeClass("glyphicon-arrow-up").addClass("glyphicon-arrow-down");
        $(".level-b-cat").each(function () {
            var currentAId = $("#" + $(this).attr("id") + "-id-a").text();
            if (currentAId == parentId) {
                $(this).hide(function () {
                    if ($(this).children(".form-group").children(".arrow-link").children("span").hasClass("glyphicon-arrow-up")) {
                        $(this).children(".form-group").children(".arrow-link").children("span").removeClass("glyphicon-arrow-up").addClass("glyphicon-arrow-down");
                    };
                });
            }
        })
        $(".level-c-cat").each(function () {
            var currentAId = $("#" + $(this).attr("id") + "-id-a").text();
            if (currentAId == parentId) {
                $(this).hide();
            }
        })
    }
};
function showsubniveauB() {
    var parentId = $(this).parent().parent().parent().attr('id')
    if ($(this).hasClass("glyphicon-arrow-down")) {
        $(this).removeClass("glyphicon-arrow-down").addClass("glyphicon-arrow-up");
        $(".level-c-cat").each(function () {
            var currentBId = $("#" + $(this).attr("id") + "-id-b").text();
            if (currentBId == parentId) {
                $(this).show();
            }
        })
    } else {
        $(this).removeClass("glyphicon-arrow-up").addClass("glyphicon-arrow-down");
        $(".level-c-cat").each(function () {
            var currentBId = $("#" + $(this).attr("id") + "-id-b").text();
            if (currentBId == parentId) {
                $(this).hide();
            }
        })
    }
}