$(document).ready(function () {
    $('#btnComment').on("click", submitReaction);

    function submitReaction() {
        var id = $("#proposal-id").text();
        $.post('/Participation/AddReactie', { tekst: $("#commentText").val(), begrotingsvoorstelId: id }).done(function (r) {
            window.location.href = '/Participation/Details?begrotingsVoorstelId=' + id;
        });
    };


    if ($("#commentText").length > 0) {
        //Countdown
        function updateCountdown() {
            var remaining = 1000 - $('#commentText').val().length;
            $('#remainingChars').text(remaining);
        }
        updateCountdown();
        $('#commentText').change(updateCountdown);
        $('#commentText').keyup(updateCountdown);
        //Countdown-sub
        function updateCountdownSub() {
            var remaining = 300 - $($(this)).val().length;
            $($(this)).next().text(remaining);
        }
        $('.commentTextArea-sub').change(updateCountdownSub);
        $('.commentTextArea-sub').keyup(updateCountdownSub);
    }
    //Like reaction
    $('.draagvlak-reacties').on("click", AddLike);
    $('.draagvlak-reacties-selected').on("click", AddLike);

    function AddLike() {
        var element = $(this);
        if ($(this).is(".draagvlak-reacties")) {
            //Aanpassen hart gekleurd/niet
            element.removeClass("draagvlak-reacties");
            element.addClass("draagvlak-reacties-selected");
            //Aanpassen aantal stemmen
            var length = element.html().length - 48;
            var nr = parseInt($(this).html().substr(0, length), 10) + 1;
            element.html(nr + "  <span class='glyphicon glyphicon-heart'></span>");
            //Veranderingen posten
            var id = element.parent().parent().attr('id');
            $.post('/api/Reactionlike', { ReactieId: id, Increment: true })

        } else {
            //Aanpassen hart gekleurd/niet
            element.removeClass("draagvlak-reacties-selected");
            element.addClass("draagvlak-reacties");

            var length = element.html().length - 48;
            var nr = parseInt($(this).html().substr(0, length), 10) - 1;
            element.html(nr + "  <span class='glyphicon glyphicon-heart'></span>");

            //Veranderingen posten
            var id = element.parent().parent().attr('id');
            $.post('/api/Reactionlike', { ReactieId: id, Increment: false })
        }
    }

    //Meer/minder reacties
    $("#show-extra").click(function () {
        $(this).next("#extra-reactions").slideToggle(500);
        if ($("#show-extra").text() == "Meer reacties weergeven") {
            $("#show-extra").html("Minder reacties weergeven");
        } else {
            ($("#show-extra").html("Meer reacties weergeven"))
        }

    });
    //Beantwoorden

    $(".subreactions").hide();
    $(".react").click(function () {
        $(this).next().next().text("Subreacties verbergen");
        $(this).parent().parent().next(".subreactions").show(500);
        $(this).parent().parent().parent().next(".subreactions").show(500);
    });
    //Subreactie toevoegen
    $('.btnComment-sub').on("click", submitReactionSub);
    function submitReactionSub() {
        var id = $("#proposal-id").text();
        $.post('/Participation/AddSubReactie', { tekst: $(this).parent().find('textarea').val(), reactieId: $(this).parent().parent().parent().parent().prev("li").attr("id") }).done(function (r) {
            window.location.href = '/Participation/Details?begrotingsVoorstelId=' + id;
        });
    };

    //Verberg subreacties

    $(".noshow-sub").click(function () {
        $(this).parent().parent().parent().hide(500);
    });
    //Toon subreacties

    $(".show-sub").click(function () {
        if ($(this).text() == "Subreacties weergeven") {
            $(this).parent().parent().parent().next(".subreactions").show(500);
            $(this).html("Subreacties verbergen");
        } else {
            $(this).parent().parent().parent().next(".subreactions").hide(500);
            $(this).html("Subreacties weergeven")
        }
    });

    //FB share
    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/nl_NL/sdk.js#xfbml=1&version=v2.6&appId=854215898057329";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));
});