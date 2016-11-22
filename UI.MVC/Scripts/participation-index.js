//= require bootstrap
$(document).ready(init());

function init() {
    $('[data-toggle="tooltip"]').tooltip({});
    $('#see-more-button').click(function() {
        if ($('#see-more').hasClass("glyphicon-chevron-down")) {
            $('#see-more').removeClass("glyphicon-chevron-down");
            $('#see-more').addClass("glyphicon-chevron-up");
            $('#see-more-button p').text("Laad minder voorstellen ");
        } else {
            $('#see-more').removeClass("glyphicon-chevron-up");
            $('#see-more').addClass("glyphicon-chevron-down");
            $('#see-more-button p').text("Laad meer voorstellen ");
        }
    });
}