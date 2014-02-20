
function maps() { }
maps.mapInstance = null;
maps.marker = null;
maps.mapInstanceId = "map_canvas";
maps.markerToSet = null;

function initialize() {
    //48.7417008797654, 19.295768737793
    var latlng = new google.maps.LatLng(48.741700, 19.295768); // default
    var options = {
        zoom: 24, center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        maxZoom: 14 //so extents zoom doesn't go nuts
    };
    maps.mapInstance = new google.maps.Map(document.getElementById(maps.mapInstanceId), options);

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