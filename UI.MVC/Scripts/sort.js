$(document).ready(function () {
    $('.sort').on("click", Sort);
    $("#arrow-extra").on("click", changeArrow);

    //Juiste active zetten
        $("#draagvlak").removeClass("active");
        if ($("#sortmethod").text() == "Draagvlak") {
            $("#draagvlak").addClass("active");
        } else if (($("#sortmethod").text() == "Datum")) {
            $("#datum").addClass("active");
        } else {
            $("#budgetwijziging").addClass("active");
        }

        function changeArrow() {
            if ($("#arrow-extra").hasClass("glyphicon-menu-down")) {
                $("#arrow-extra").removeClass("glyphicon-menu-down");
                $("#arrow-extra").addClass("glyphicon-menu-up");
            } else {
                $("#arrow-extra").removeClass("glyphicon-menu-up");
                $("#arrow-extra").addClass("glyphicon-menu-down");
            }
        }
   

    function Sort() {
        var sortMethod = $(this).text();
        if (sortMethod == "Draagvlak") {
            window.location.href = '/participation/?zip=' + $("#zip").text() + '&sortMethod=Draagvlak';

        } else if (sortMethod == "Datum") {
            window.location.href = '/participation/?zip=' + $("#zip").text() + '&sortMethod=Datum';
        } else {
            window.location.href = '/participation/?zip=' + $("#zip").text() + '&sortMethod=Budgetwijziging';
        }
            
     

    }

});