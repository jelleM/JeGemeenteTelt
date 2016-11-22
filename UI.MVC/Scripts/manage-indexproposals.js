$(document).ready(init());

function init() {
    $('#see-more-button-1').click(function () {
        if ($('#see-more-1').hasClass("glyphicon-chevron-down")) {
            $('#see-more-1').removeClass("glyphicon-chevron-down");
            $('#see-more-1').addClass("glyphicon-chevron-up");
            $('#see-more-button-1 p').text("Laad minder voorstellen ");
        } else {
            $('#see-more-1').removeClass("glyphicon-chevron-up");
            $('#see-more-1').addClass("glyphicon-chevron-down");
            $('#see-more-button-1 p').text("Laad meer voorstellen ");
        }
    });
    $('#see-more-button-2').click(function () {
        if ($('#see-more-2').hasClass("glyphicon-chevron-down")) {
            $('#see-more-2').removeClass("glyphicon-chevron-down");
            $('#see-more-2').addClass("glyphicon-chevron-up");
            $('#see-more-button-2 p').text("Laad minder voorstellen ");
        } else {
            $('#see-more-2').removeClass("glyphicon-chevron-up");
            $('#see-more-2').addClass("glyphicon-chevron-down");
            $('#see-more-button-2 p').text("Laad meer voorstellen ");
        }
    });
    $('#see-more-button-3').click(function () {
        if ($('#see-more-3').hasClass("glyphicon-chevron-down")) {
            $('#see-more-3').removeClass("glyphicon-chevron-down");
            $('#see-more-3').addClass("glyphicon-chevron-up");
            $('#see-more-button-3 p').text("Laad minder voorstellen ");
        } else {
            $('#see-more-3').removeClass("glyphicon-chevron-up");
            $('#see-more-3').addClass("glyphicon-chevron-down");
            $('#see-more-button-3 p').text("Laad meer voorstellen ");
        }
    });
}