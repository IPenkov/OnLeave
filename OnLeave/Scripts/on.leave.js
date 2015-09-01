$(document).ready($.proxy(function () {
    this.addActionConfirmation();
    this.collapseLegend();
    this.makeCarousel();

    // Correct date validation error in Chrome and Safari.
    jQuery.validator.methods["date"] = function (value, element) {
        var shortDateFormat = "dd/mm/yy";
        var res = true;
        try {
            $.datepicker.parseDate(shortDateFormat, value);
        } catch (error) {
            res = false;
        }
        return res;
    };

}, this));




/*
 * Set default value
 */
function setZeroAmount(obj) {

    if (obj.value == '') obj.value = '0.00';
};

/*
 * Adds confimaiton before post any form data
 */
function addActionConfirmation() {
    $("form[data-confirm]").off().on("submit", function () {
        return confirm($(this).data("confirm"));
    });
};

/*
 * Makes Carousel
 */
function makeCarousel() {
    $('div.image-caroucel').jcarousel({
        vertical: false,
        visible: 3,
        scroll: 1
    });
}

function search() {
    $("#search").off().ajaxForm({
        iframe: true,
        type: 'POST',
        dataType: "html",
        cache: false,
        timeout: 1200000,
        async: false,
        beforeSubmit: function () {
            //Do something here if needed like show in progress message
        },
        success: function (result) {
            $("#photoes").html(result);
            attachAddPicture();
            addActionConfirmation();
        },
        error: function (xhr, textStatus, errorThrown) {
            alert(xhr);
        }
    });
}

/*
 * Adds photo async.
 */
function attachAddPicture() {
    $("#add_picture").off().ajaxForm({
        iframe: true,
        type: 'POST',
        dataType: "html",
        cache: false,
        timeout: 1200000,
        async: false,
        beforeSubmit: function () {
            //Do something here if needed like show in progress message
        },
        success: function (result) {
            $("#photoes").html(result);
            attachAddPicture();
            addActionConfirmation();
        },
        error: function (xhr, textStatus, errorThrown) {
            alert(xhr);
        }
    });
};

/*
 * Romove's deleted photo.
 */
function removePhoto(photoId) {
    var possition = 0,
        photoes;

    $('#photo_' + photoId).parent().remove();

    photoes = $('.addhotel-content ul li');
    photoes.removeClass("first-in-line");
    photoes.removeClass("last-in-line");
    for (var i = 0; i < photoes.length; i++) {
        var possition = i % 3;
        if (possition == 0) {

            photoes.eq(i).removeClass("last-in-line");
            photoes.eq(i).addClass("first-in-line");
        } else if (possition == 2) {

            photoes.eq(i).removeClass("first-in-line");
            photoes.eq(i).addClass("last-in-line");
        }
    }

    // show add button
    $('#add_picture').css('display', 'block');
}

function collapseLegend() {
    $("legend").off().on("click", function () {
        var $legend = $(this);
        if ($legend.attr("rel") == "open") {
            // Collapse the section.
            $legend.attr("rel", "closed");
            $legend.closest("fieldset").addClass("closed");
            $legend.closest("fieldset").find("span").hide();
            
            //gen.noFocusElements($elem);
            // Hide the placeholders if any.
            //$elem.find("span.placeholder").hide();
            // Hide button holder
            //$elem.find("p.fset-submit").hide();
        }
        else {
            // Expand the section.
            $legend.attr("rel", "open");
            $legend.closest("fieldset").removeClass("closed");
            $legend.closest("fieldset").find("span").css("display", "inline-block");
            //gen.canFocusElements($elem);
            // Show the placeholders if any and the browser doesn't support placeholders.
            //if (!Modernizr.input.placeholder) {
            //    var $holders = $elem.find("span.placeholder");
            //    for (var i = 0; i < $holders.length; i++) {
            //        var $span = $($holders[i]);
            //        var $item = $span.parent().find("input[placeholder]");
            //        // Control must be empty to display the placeholder.
            //        if ($item.val().trim() == "") {
            //            $span.show();
            //        }
            //    }
            //}
            // Show button holder
            //$elem.find("p.fset-submit").show();

            // Fire a new close event.
            //$elem.trigger("ffExpandEvent", {
               // action: "Expand"
            //});
        }
    });
}

/*
 * Handle ajax errors
 */
function handleError(errorMessage) {
    
    alert(errorMessage);
}

/*
 * Initialize Google Maps
 */
function initialize(isDraggable) {

    // don't init map if coordinates missing
    if ($("#latitude").length === 0) return;

    var mapOptions = {
        zoom: 10,
        center: new google.maps.LatLng($("#latitude").val(), $("#longitude").val())
    };

    var map = new google.maps.Map(document.getElementById('map-canvas'),
        mapOptions);

    var marker = new google.maps.Marker({
        position: map.getCenter(),
        map: map,
        draggable: isDraggable === false ? false : true,
        title: $("#locationName").val()
    });
    
    google.maps.event.addListener(marker, 'dragend', function () {
        var possision = marker.getPosition();
        $("#longitude").val(possision.K);
        $("#latitude").val(possision.G);
    });

    $("#tab-map").on("TabSelected", function ()
    {
        google.maps.event.trigger(map, 'resize');
        map.setCenter(marker.position);
    });
}

function loadScript() {
    var script = document.createElement('script');
    script.type = 'text/javascript';
    script.src = 'https://maps.googleapis.com/maps/api/js?v=3.exp&' +
        'callback=initialize';
    document.body.appendChild(script);
}