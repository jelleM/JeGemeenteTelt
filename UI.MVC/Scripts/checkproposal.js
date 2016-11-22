$(document).ready(function () {
    var id = $("#voorstel_id").val();
    $('#edit_lb').on("click", editLb);
    $('#edit_kb').on("click", editKb);
    $('#accept').on("click", accept);
    $('#decline').on("click", decline);
    $('#dropdown').change(selected);

    function selected() {
        $("#send").removeAttr("disabled");
        var content = $("#dropdown :selected").text();
        var subject = $("#dropdown").val();

        $('#subject').text("");
        if (content == "Afwijzen") {
            $('#subject').text("Voorstel afgewezen!");
        } else {
            $('#subject').text("Voorstel goedgekeurd!");
        }
        $('#content').text("");
        if (content == "Afwijzen") {
            $('#content').text("Uw voorstel werd afgewezen omdat...");
        } else {
            $('#content').text("Proficiat uw voorstel werd goedgekeurd! \nBedankt voor uw medewerking!");
        }
    }
    function editLb() {
        if ($("#edit_lb").text() == "Bewerk") {
            $("#text_lb").removeAttr("readonly");
            $("#edit_lb").text("Bewerking voltooid");
        } else {
            $("#text_lb").attr("readonly", "readonly");
            $("#edit_lb").text("Bewerk");
        }
    };
    function decline() {
        $.ajax({
            url: '/Api/budgetproposalapi/status/' + id + '/decline',
            success: function (result) {
                console.log("voorstel " + id + " declined")
            }
        });
    }
    function accept() {
        $.ajax({
            url: '/Api/budgetproposalapi/status/' + id + '/accept',
            success: function (result) {
                console.log("voorstel " + id + " accepted")
            }
        });
    }
    function editKb() {
        if ($("#edit_kb").text() == "Bewerk") {
            $("#text_kb").removeAttr("readonly");
            $("#edit_kb").text("Bewerking voltooid");
        } else {
            $("#text_kb").attr("readonly", "readonly");
            $("#edit_kb").text("Bewerk");
        }
    };
    function save() {
        $("#text_kb").attr("readonly", "readonly");
        $("#text_lb").attr("readonly", "readonly");
    }
})