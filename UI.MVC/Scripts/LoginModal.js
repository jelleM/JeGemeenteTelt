$(document).ready(function () {
    var loginLink = $("a[id='loginLink']");
    loginLink.attr({ "href": "#", "data-toggle": "modal", "data-target": "#ModalLogin" })
    $("#ModalLogin").on("shown.bs.modal", function (e) {
        $("#Email").focus();
    });
});
