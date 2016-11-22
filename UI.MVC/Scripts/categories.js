$(document).ready(function () {
    var naam = $("#dropdown :selected").text();
    var CategorieId = $("#dropdown").val()

    var zip = $("#zip").val();
    $('#budget-year').change(selectedYear);
    $('#dropdown').change(selected);
    $('#save-button').click(submitCategories);

    var currentYear = parseInt($("#selected-year").text());
    if (currentYear != 0) {
        $('#budget-year').val(currentYear);
    }

    function selectedYear() {
        var content = $("#budget-year :selected").text();
        window.location.href = '/manage/categories/?year=' + content;
    }
    function selected() {

        naam = $("#dropdown :selected").text();
        CategorieId = $("#dropdown").val()
        $('#title').html("Categorieinformatie <span class=\"blue\"> nr. " + naam + "</span> wijzigen");
        getCategoryInfo(function (output) {
            $("#edit_id").val(output.CategorieInformatieId);
            $("#edit_info").val(output.Informatie);
            $("#edit_yt").val(output.YoutubeLink);
            $("#edit_bedrag").val(output.Bedrag);
            console.log(output);
        });
    }

    function getCategoryInfo(handleData) {
        $.ajax("/api/budget/Category/" + CategorieId + "/" + zip, { type: 'GET', dataType: 'json' })
    .done(function (data) {
        handleData(data);
    })
    .fail(function () { console.log('Call failed') });
    }
});

function submitCategories() {

    //Image

    input = document.getElementById('file-upload');
    file = input.files[0];
    console.log(file);
    var reader = new window.FileReader();
    if (file) {
        reader.readAsDataURL(file);
        reader.onloadend = function () {
            base64data = reader.result;
            console.log(base64data.substr(base64data.indexOf(',') + 1));
            var categoreinformatie = {
                CategorieInformatieId: $("#edit_id").val(),
                Informatie: $("#edit_info").val(),
                Afbeelding: base64data.substr(base64data.indexOf(',') + 1),
                YoutubeLink: $("#edit_yt").val(),
                Bedrag: $("#edit_bedrag").val(),
            };
            $.post('/Manage/Categories', categoreinformatie).done(function (r) {
                window.location.href = '/Manage/Categories?selected=' + $("#edit_id").val();
            });
        }

    } else {
        var categoreinformatie = {
            CategorieInformatieId: $("#edit_id").val(),
            Informatie: $("#edit_info").val(),
            YoutubeLink: $("#edit_yt").val(),
            Bedrag: $("#edit_bedrag").val(),
        };
        $.post('/Manage/Categories', categoreinformatie).done(function (r) {
            window.location.href = '/Manage/Categories?selected=' + $("#edit_id").val();
        });
    }
};