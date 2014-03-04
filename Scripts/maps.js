
function maps() { }
maps.mapInstance = null;
maps.marker = null;
maps.mapInstanceId = "map_canvas";
maps.markerToSet = null;

function initialize() {
    //48.7417008797654, 19.295768737793
    var latlng = new google.maps.LatLng(48.741700, 19.295768); // default
    var options = {
        zoom: 7, center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        maxZoom: 14 //so extents zoom doesn't go nuts
    };

    var mapDiv = document.getElementById(maps.mapInstanceId);
    maps.mapInstance = new google.maps.Map(mapDiv, options);
    mapDiv.style.width = '100%';
    mapDiv.style.height = '100%';


    //autocomplete
    var input = document.getElementById('pac-input');
    var autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.bindTo('bounds', maps.mapInstance);

    google.maps.event.addListener(maps.mapInstance, 'click', function (event) {
        placeMarker(event.latLng);
    });

    google.maps.event.addListenerOnce(maps.mapInstance, 'idle', function (event) {
        if (maps.markerToSet) {
            placeMarker(maps.markerToSet);
            var bound = new google.maps.LatLngBounds();
            bound.extend(maps.markerToSet);
            maps.mapInstance.fitBounds(bound);
        }
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

$(function () {
    initialize();
});

