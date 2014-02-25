
var map, marker, defaultCenter;

function maps() { }
maps.mapInstance = null;
maps.marker = marker;
maps.mapInstanceId = "map_canvas";
maps.markerToSet = null;

function initialize(id) {
    //48.7417008797654, 19.295768737793
    //48.716389, 19.458611
    
    defaultCenter = new google.maps.LatLng(48.716389, 19.458611); // default center position 

    var options = {
        zoom: 7, center: defaultCenter,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        maxZoom: 14 //so extents zoom doesn't go nuts
    };

    map = new google.maps.Map(document.getElementById(maps.mapInstanceId), options);
    maps.mapInstance = map;

    //autocomplete
    var input = document.getElementById('pac-input');
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    var autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.bindTo('bounds', maps.mapInstance);

    var infowindow = new google.maps.InfoWindow();
    var marker = new google.maps.Marker({ map: map});

    // add listener for autocomplete
    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        infowindow.close();
        marker.setVisible(false);
        var place = autocomplete.getPlace();
        if (!place.geometry) {
            return;
        }

        // If the place has a geometry, then present it on a map.
        //if (place.geometry.viewport) {
        //    map.fitBounds(place.geometry.viewport);
        //} else {
        //    map.setCenter(place.geometry.location);
        //    map.setZoom(17);  // Why 17? Because it looks good.
        //}
        //marker.setIcon(/** @type {google.maps.Icon} */({
        //    url: place.icon,
        //    size: new google.maps.Size(71, 71),
        //    origin: new google.maps.Point(0, 0),
        //    anchor: new google.maps.Point(17, 34),
        //    scaledSize: new google.maps.Size(35, 35)
        //}));
        //marker.setPosition(place.geometry.location);
        //marker.setVisible(true);

        placeMarker(place.geometry.location);

        //var address = '';
        //if (place.address_components) {
        //    address = [
        //      (place.address_components[0] && place.address_components[0].short_name || ''),
        //      (place.address_components[1] && place.address_components[1].short_name || ''),
        //      (place.address_components[2] && place.address_components[2].short_name || '')
        //    ].join(' ');
        //}

        //infowindow.setContent('<div><strong>' + place.name + '</strong><br>' + address);
        //infowindow.open(map, marker);
    });


    google.maps.event.addListener(maps.mapInstance, 'click', function (event) {
        placeMarker(event.latLng);
        SetStaticImageLocation(id, event.latLng);
    });

    google.maps.event.addListenerOnce(maps.mapInstance, 'idle', function (event) {
        boundPosition();
    });

    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        infowindow.close();
        marker.setVisible(false);
        var place = autocomplete.getPlace();
        if (!place.geometry) {
            return;
        }

        // If the place has a geometry, then present it on a map.
        if (place.geometry.viewport) {
            map.fitBounds(place.geometry.viewport);
        } else {
            map.setCenter(place.geometry.location);
            map.setZoom(17);  // Why 17? Because it looks good.
        }

        marker.setIcon(/** @type {google.maps.Icon} */({
            url: place.icon,
            size: new google.maps.Size(71, 71),
            origin: new google.maps.Point(0, 0),
            anchor: new google.maps.Point(17, 34),
            scaledSize: new google.maps.Size(35, 35)
        }));
        marker.setPosition(place.geometry.location);
        marker.setVisible(true);

        var address = '';
        if (place.address_components) {
            address = [
              (place.address_components[0] && place.address_components[0].short_name || ''),
              (place.address_components[1] && place.address_components[1].short_name || ''),
              (place.address_components[2] && place.address_components[2].short_name || '')
            ].join(' ');
        }
    });
}

function placeMarker(location) {
    if (maps.marker) {
        maps.marker.setPosition(location);
    } else {
        maps.marker = new google.maps.Marker({
            position: location,
            map: maps.mapInstance
        });
    }

    if (maps.marker) { //What's a better way than this dance?
        var textboxid = $("#" + maps.mapInstanceId).data("textboxid");
        $("#" + textboxid).val(maps.marker.getPosition().toUrlValue(13));
    }
}

function boundPosition() {
    if (maps.markerToSet) {
        placeMarker(maps.markerToSet);
        var bound = new google.maps.LatLngBounds();
        bound.extend(maps.markerToSet);
        maps.mapInstance.fitBounds(bound);
    }

}

function SetStaticImageLocation(id, location) {
    $("img[data-static-image]").attr('src', 'http://maps.googleapis.com/maps/api/staticmap?center=' + location.lat() + ',' + location.lng() + '&zoom=9&size=300x200&maptype=roadmap&sensor=false&markers=color:blue%7Clabel:S%7C' + location.lat() + ',' + location.lng());


}


$(function () {
    $('.mapOpen').click(function () {
        var $dialog = $('#' + $(this).attr('id') + '_map');// dialog ID based on click element ID
        //var dialogType = $(this).attr('dialogType');
        $dialog.dialog(dialogOptions); // pass the appropriate options object to the dialog call dialogOptions[dialogType]
        $(".ui-dialog-titlebar").hide();

        return false;
    });

    initialize($(this).attr('id')); //init map
});

var dialogOptions = {
    modal: true,
    height: 700,
    width: 1000,
    draggable: false,
    resizable: false,
    buttons: {
        Close: function () {
            $(this).dialog("close");
        }
    },
    resizeStop: function (event, ui) { google.maps.event.trigger(map, 'resize') },
    open: function (event, ui) {
        google.maps.event.trigger(map, 'resize');

        if (typeof maps.marker === 'undefined') 
            map.setCenter(defaultCenter);
        
        boundPosition();
    }
};