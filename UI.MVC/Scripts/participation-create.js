$(window).load(init());
function init() {
    initialize();
    locked();
    addBudgetToggleListeners();
    //Hide b & c standaard
    $(".level-b").hide();
    $(".level-c").hide();
    $(".arrow-a").on("click", showsubniveauA);
    $(".arrow-b").on("click", showsubniveauB);
    if ($("#big-amount").text().substr(2) > 0) {
        $("#big-amount").css("color", "green");
    } else if ($("#big-amount").text().substr(2) < 0) {
        $("#big-amount").css("color", "red");
    }
}

function initialize() {
    //Alle elementen van de slider klasse worden hier aangemaakt
    $(".slider").each(function (index) {
        // Sliders met tooltip initialiseren.
        $(this).slider({
            formatter: function (value) {
                return 'Current value: ' + value;
            },
            animate: "fast"

        }).css(".slider-selection", "rgba(129, 212, 115, 1)");

        //De waarde van een slider bijhouden voor het beginnen met sliden
        var valueBefore = 0;
        $(this).on('slideStart', function (ev) {
            valueBefore = $(this).data('slider').getValue();
        });

        //Als de slider stopt, andere sliders aanpassens
        $(this).on('slideStop', function (ev) {
            var id = $(this).attr("id").toString().substr(6);
            var difference = valueBefore - $(this).data('slider').getValue();

            //Aanpassen label van level-a als er iets veranderd aan level-b
            if ($(this).hasClass("slider-b")) {
                var LevelAid = $("#" + $(this).attr("id") + "-id-a").text();
                var oldValA = $("#" + LevelAid).children(".slider-a").data('slider').getValue();
                var newValueA = oldValA - difference;
                $("#" + LevelAid).children(".slider-a").slider().slider('setValue', newValueA)
            }

            //Aanpassen label van level-a & level-c als er iets veranderd aan level-b
            if ($(this).hasClass("slider-c")) {
                var LevelAid = $("#" + $(this).attr("id") + "-id-a").text();
                var oldValA = $("#" + LevelAid).children(".slider-a").data('slider').getValue();
                var newValueA = oldValA - difference;
                $("#" + LevelAid).children(".slider-a").slider().slider('setValue', newValueA);

                var LevelBid = $("#" + $(this).attr("id") + "-id-b").text();
                var oldValB = $("#" + LevelBid).children(".slider-b").data('slider').getValue();
                var newValueB = oldValB - difference;
                $("#" + LevelBid).children(".slider-b").slider().slider('setValue', newValueB);
            }

            //Aanpassen sliders van level-b & level-c als slider van level-a aangepast wordt
            if ($(this).hasClass("slider-a")) {
                var parentId = $(this).parent().attr("id");
                //Aanpassen sliders level-b
                var bSliders = [];
                var difference = valueBefore - $(this).data('slider').getValue();
                $(".slider-b").each(function () {
                    var LevelAid = $("#" + $(this).attr("id") + "-id-a").text();
                    if (parentId == LevelAid) {
                        bSliders.push($(this));
                    }
                });
                var count = bSliders.length;
                for (i = 0; i < count; i++) {
                    var initialValue = bSliders[i].data('slider').getValue();
                    var percent = difference / valueBefore;
                    //Percentueel dalen/stijgen zoals percentueel van parent
                    var newValue = Math.round((initialValue * (1 - percent))).toFixed(2);
                    bSliders[i].slider().slider('setValue', parseInt(newValue));
                    //Aanpassen labels level-b
                    bSliders[i].parent().children(".level-budget").children("span").text("€ " + parseFloat(newValue));
                }

                //Aanpassen sliders level-c
                var cSliders = [];
                $(".slider-c").each(function () {
                    var LevelAid = $("#" + $(this).attr("id") + "-id-a").text();
                    if (parentId == LevelAid) {
                        cSliders.push($(this));
                    }
                });
                var count = cSliders.length;
                for (i = 0; i < count; i++) {
                    var initialValue = cSliders[i].data('slider').getValue();
                    var percent = difference / valueBefore;
                    //Percentueel dalen/stijgen zoals percentueel van parent
                    var newValue = Math.round((initialValue * (1 - percent))).toFixed(2);
                    cSliders[i].slider().slider('setValue', parseInt(newValue));
                    //Aanpassen labels level-c
                    cSliders[i].parent().children(".level-budget").children("span").text("€ " + parseFloat(newValue));
                }
            };
            //Aanpassen sliders C als B veranderd
            if ($(this).hasClass("slider-b")) {
                var parentId = $(this).parent().attr("id");
                //Aanpassen slider-c
                var cSliders = [];
                $(".slider-c").each(function () {
                    var LevelBid = $("#" + $(this).attr("id") + "-id-b").text();
                    if (parentId == LevelBid) {
                        cSliders.push($(this));
                    }
                });
                var count = cSliders.length;
                for (i = 0; i < count; i++) {
                    var initialValue = cSliders[i].data('slider').getValue();
                    var percent = difference / valueBefore;
                    //Percentueel dalen/stijgen zoals percentueel van parent
                    var newValue = Math.round((initialValue * (1 - percent))).toFixed(2);

                    cSliders[i].slider().slider('setValue', parseInt(newValue));
                    //Aanpassen labels level-c
                    cSliders[i].parent().children(".level-budget").children("span").text("€ " + parseFloat(newValue));
                }
            };
        });

        // Bedrag meegeven met span
        $(this).on("slide", function (slidePosition) {
            var id = $(this).attr("id").toString().substr(6);
            if (slidePosition.value > 0) {
                var initialValue = parseFloat($("#amount" + id).text().substr(2));
                var difference = slidePosition.value - initialValue;
                $("#amount" + id).text("€ " + slidePosition.value).css("color", "green");
                $("#big-amount").text("€ " + (parseFloat($("#big-amount").text().substr(2)) - difference));
                //Aanpassen bedrag level A als level B aangepast wordt
                if ($(this).parent().hasClass("level-b")) {
                    var LevelAid = $("#" + $(this).attr("id") + "-id-a").text();
                    var newValueA = (parseFloat($("#" + LevelAid).children(".level-budget").children("span").text().substr(2)) + difference);
                    $("#" + LevelAid).children(".level-budget").children("span").text("€ " + newValueA);
                }
                //Aanpassen bedrag level B & A als level C aangepast wordt
                if ($(this).parent().hasClass("level-c")) {
                    var LevelAid = $("#" + $(this).attr("id") + "-id-a").text();
                    var newValueA = (parseFloat($("#" + LevelAid).children(".level-budget").children("span").text().substr(2)) + difference);
                    $("#" + LevelAid).children(".level-budget").children("span").text("€ " + newValueA);

                    var LevelBid = $("#" + $(this).attr("id") + "-id-b").text();
                    var newValueB = (parseFloat($("#" + LevelBid).children(".level-budget").children("span").text().substr(2)) + difference);
                    $("#" + LevelBid).children(".level-budget").children("span").text("€ " + newValueB);
                }
            } else {
                $("#amount" + id).text("€ " + slidePosition.value).css("color", "blue");
            }
        });
    });

    //Verander kleur big amount bij veranderingen
    $('#big-amount').bind("DOMSubtreeModified", function () {
        if ((parseFloat($("#big-amount").text().substr(2)) >= 0)) {
            $("#big-amount").css("color", "green");
        }
        else {
            $("#big-amount").css("color", "red");
        }
    });

    //Countdown bij korte & lange beschrijving
    //Countdown lange beschrijving
    function updateCountdownLongDescription() {
        var remaining = 1000 - $('#long-description').val().length;
        $("#remainingchars-long-description").text(remaining);
    }

    updateCountdownLongDescription();
    $('#long-description').change(updateCountdownLongDescription);
    $('#long-description').keyup(updateCountdownLongDescription);
    //Countdown korte beschrijving
    function updateCountdownShortDescription() {
        var remaining = 500 - $($(this)).val().length;
        $("#remainingchars-short-description").text(remaining);
    }
    $('#short-description').change(updateCountdownShortDescription);
    $('#short-description').keyup(updateCountdownShortDescription);
}

$('#submit-budget').on("click", submitBudgetproposal);


function submitBudgetproposal() {
    var bigAmount = parseFloat($("#big-amount").text().substr(2));
    if (bigAmount >= 0 && $("#short-description").val().length > 0 && $("#long-description").val().length > 0) {
        $(this).attr("disabled", "disabled")
        var budgetten = [];
        $(".amount").each(function () {
            if (isNaN(parseFloat($(this).text().substr(2)))) {
                budgetten.push(0)
            } else {
                budgetten.push(parseFloat($(this).text().substr(2)))
            }
        })
        //Image

        input = document.getElementById('file-upload');
        file = input.files[0];
        var reader = new window.FileReader();
        if (file) {
            reader.readAsDataURL(file);
            reader.onloadend = function () {
                base64data = reader.result;
                //Als afbeelding is upgeload dan afbeelding meegeven anders niet
                var voorstel =
{
    begrotingJaartal: parseFloat($("#begrotingJaartal").text()),
    ParticipatieProjectId: parseFloat($("#projectId").text()),
    GoedKeuringsStaat: 1,
    Draagvlak: 0,
    Afbeelding: base64data.substr(base64data.indexOf(',') + 1),
    TotaalBudgetwijziging: parseFloat($("#big-amount").text().substr(2)),
    Postcode: parseFloat($("#zip").text()),
    Budgetten: budgetten,
    KorteBeschrijving: $("#short-description").val(),
    LangeBeschrijving: $("#long-description").val()
};
                $.post('/Participation/Create', voorstel).done(function (r) {
                    window.location.href = '/Participation/Index'
                });
            }

        } else {
            var voorstel =
        {
            begrotingJaartal: parseFloat($("#begrotingJaartal").text()),
            ParticipatieProjectId: parseFloat($("#projectId").text()),
            GoedKeuringsStaat: 1,
            Draagvlak: 0,
            TotaalBudgetwijziging: parseFloat($("#big-amount").text().substr(2)),
            Postcode: parseFloat($("#zip").text()),
            Budgetten: budgetten,
            KorteBeschrijving: $("#short-description").val(),
            LangeBeschrijving: $("#long-description").val()
        };

            $.post('/Participation/Create', voorstel).done(function (r) {
                window.location.href = '/Participation/Index'
            });
        }
    }
    else {
        if (!(bigAmount >= 0)) {
            alert("Een begrotingsvoorstel mag geen negatieve totale waarde hebben!");
        }
        else if ($("#short-description").val().length == 0 || $("#long-description").val().length == 0 == 0) {
            alert("Gelieve een korte en lange beschrijving in te vullen.")
        }
    }
};

function locked() {
    $(".locked").text(" (locked)");

    if ($(".locked-slider").length > 0) {
        console.log("locked slider");
        $(".locked-slider").slider("disable");
    }
}

function addBudgetToggleListeners() {
    $(".budget-link").click(function () {
        if (($(this).html()).toString().trim() === "Meer info") {
            $(this).html("Minder info");
        } else {
            $(this).html("Meer info");
        }
    });
}

function showsubniveauA() {
    var parentId = $(this).parent().attr('id');

    if ($(this).children("span").hasClass("glyphicon-chevron-up")) {
        $(this).children("span").removeClass("glyphicon-chevron-up").addClass("glyphicon-chevron-down");
        $(".level-b").each(function () {
            var currentAId = $("#" + $(this).attr("id") + "-id-a").text();
            if (currentAId == parentId) {
                $(this).show();
            }
        });
    } else {
        $(this).children("span").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-up");
        $(".level-b").each(function () {
            var currentAId = $("#" + $(this).attr("id") + "-id-a").text();
            if (currentAId == parentId) {
                $(this).hide(function () {
                    if ($(this).children("span").hasClass("glyphicon-chevron-down")) {
                        $(this).children("span").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-up");
                    };
                });
            }
        });
        $(".level-c").each(function () {
            var currentAId = $("#" + $(this).attr("id") + "-id-a").text();
            if (currentAId == parentId) {
                $(this).hide();
            }
        });
    }
};

function showsubniveauB() {
    var parentId = $(this).parent().attr('id');

    if ($(this).children("span").hasClass("glyphicon-chevron-up")) {
        $(this).children("span").removeClass("glyphicon-chevron-up").addClass("glyphicon-chevron-down");
        $(".level-c").each(function () {
            var currentBId = $("#" + $(this).attr("id") + "-id-b").text();
            if (currentBId == parentId) {
                $(this).show();
            }
        });
    } else {
        $(this).children("span").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-up");
        $(".level-c").each(function () {
            var currentBId = $("#" + $(this).attr("id") + "-id-b").text();
            if (currentBId == parentId) {
                $(this).hide();
            }
        });
    }
};


