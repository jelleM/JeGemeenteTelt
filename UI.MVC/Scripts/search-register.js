$(document).ready(function () {
    $('#zip').blur(function () {
        console.log($('#zip').val().length);
        if ($('#zip').val().length > 4) {
            $('#zip').val($('#zip').val().substring(0,4));

        }
    });

    var cities;
    //postcode en namen ophalen
    function getCities(handleData) {
        $.ajax("/api/city/all", { type: 'GET', dataType: 'json' })
 .done(function (data) {
     handleData(data);
 })
 .fail(function () { console.log('Call failed') });

    }
    getCities(function (output) {
        cities = output;
        //autocomplete 
        $(function () {
            $(".search-city").autocomplete({
                source: function (request, response) {
                    var filteredArray = $.map(cities, function (item) {
                        if (item.toLowerCase().indexOf(request.term.toLowerCase()) == 5) {
                            return item;
                        } else if ($.isNumeric(request.term.substr(0, 4)) && item.toLowerCase().indexOf(request.term.toLowerCase()) == 0) {
                            return item;
                        } else {
                            return null;
                        }
                    });
                    response(filteredArray);
                }
            });
        })
    });
    
});
