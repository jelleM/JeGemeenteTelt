$(document).ready(function () {
    $('.draagvlak').on("click", EventClick);
    $('.draagvlak-selected').on("click", EventClick);

    function EventClick() {
        if ($(this).is(".draagvlak")) {
            //Aanpassen hart gekleurd/niet
            $(this).removeClass("draagvlak");
            $(this).addClass("draagvlak-selected");
            //Aanpassen aantal stemmen
            var length = $(this).html().length - 48;
            var nr = parseInt($(this).html().substr(0, length), 10) + 1;
            $(this).html(nr + " <span class='glyphicon glyphicon-heart'></span>");
            //Veranderingen posten
            var id = $(this).attr('id');
            $.post('/api/Budgetproposal', { BegrotingsvoorstelId: id, Increment: true })

        } else {
            //Aanpassen hart gekleurd/niet
            $(this).removeClass("draagvlak-selected");
            $(this).addClass("draagvlak");
            //Aanpassen aantal stemmen
            var length = $(this).html().length - 48;
            var nr = parseInt($(this).html().substr(0, length), 10) - 1;
            $(this).html(nr + " <span class='glyphicon glyphicon-heart'></span>");
            //Veranderingen posten
            var id = $(this).attr('id');
            $.post('/api/Budgetproposal', { BegrotingsvoorstelId: id, Increment: false })
        }
    }
});