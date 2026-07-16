var GLoc = {};
GLoc.Map = {};
GLoc.Map.Options = null;
GLoc.Map.Object = null;
GLoc.Map.PolyArray = new Array();
GLoc.Map.Poly = null;
var data = null;
var dataaa = [];
var locations = [];
var j = [];
GLoc.Init = function () {

};
function Replace(mainstr, strtoreplace, replacewith) {

    do {
        mainstr = mainstr.replace(strtoreplace, replacewith);
    } while (mainstr.indexOf(strtoreplace) !== -1);
    return mainstr;
};
function Display(data, defLoc, villAdd) {
    debugger;
    GLoc.Map.PolyArray = new Array();
    Log("Received glo data");
    Log(data);
    var bounds = new google.maps.LatLngBounds();
    if (data == "" || data == null || data == undefined || data == "undefined") {
        GLoc.Map.PolyArray = new Array();
    } else {
        defLoc = "";
        data = Replace(data, "jb", "lb");
        data = Replace(data, "kb", "mb");

        data = Replace(data, "lb", "lat");
        data = Replace(data, "mb", "lng");
        /*var aData = eval(data);*/
        var aData = JSON.parse(data);
        for (var k = 0; k < aData.length; k++) {
            mData = aData[k];
            var nlatlang;

            var nlatlang = new google.maps.LatLng(mData.lat, mData.lng);
            GLoc.Map.PolyArray.push(nlatlang);
            bounds.extend(nlatlang);

        }
    }

    if (defLoc == "") {
        defLoc = "26.386948928734135,72.9766845703125";
    }
    else {
        defLoc = defLoc.lat + "," + defLoc.long;
    }

    locData = defLoc;
    var latlngStr = locData.split(',', 2);
    var lat = parseFloat(latlngStr[0]);
    var lng = parseFloat(latlngStr[1]);
    var latlng = new google.maps.LatLng(lat, lng);
    GLoc.Map.Options = {
        zoom: 12,
        center: latlng,
        mapTypeId: 'roadmap'
    }
    GLoc.Map.Object = new google.maps.Map(document.getElementById('mapcanv'), GLoc.Map.Options);

    GLoc.Map.Poly = null;
    GLoc.Map.Poly = new google.maps.Polygon({
        paths: GLoc.Map.PolyArray,
        strokeColor: '#FF0000',
        strokeOpacity: 0.8,
        strokeWeight: 3,
        fillColor: '#FF0000',
        fillOpacity: 0.35
    });
    GLoc.Map.Poly.setEditable(true);
    if (GLoc.Map.PolyArray.length > 0) {
        debugger;
        if (isNaN(GLoc.Map.PolyArray[0].lat())) {
            GLoc.Map.PolyArray = new Array();
        }

    }    
    if (GLoc.Map.PolyArray.length > 0) {
        GLoc.Map.Poly.setPath(GLoc.Map.PolyArray);
        GLoc.Map.Poly.setMap(GLoc.Map.Object);

        GLoc.Map.PolyLine = new google.maps.Polyline();
        GLoc.Map.PolyLine.setPath(GLoc.Map.PolyArray);

        GLoc.Map.Object.fitBounds(bounds);
    } else {
        var geocoder = new google.maps.Geocoder();
        geocoder.geocode({ 'address': villAdd }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                GLoc.Map.Object.setCenter(results[0].geometry.location);
                var marker = new google.maps.Marker({
                    map: GLoc.Map.Object,
                    position: results[0].geometry.location
                });
            }
        });

    }
    google.maps.event.addListener(GLoc.Map.Object, 'click', OnMapClicked);
    $("#btnMapReset").bind("click", GLoc.Reset);
    $("#BtnSave").bind("click", GLoc.GetPathAsString);

};

function OnMapClicked(e) {
    if (GLoc.Map.PolyArray.length <= 2) {
        GLoc.Map.PolyArray.push(e.latLng);
    }
    if (GLoc.Map.PolyArray.length > 2) {
        GLoc.Map.Poly.setPath(GLoc.Map.PolyArray);
        GLoc.Map.Poly.setMap(GLoc.Map.Object);
    } else {

    }
};
GLoc.Reset = function () {
    debugger;
    GLoc.Map.PolyArray = new Array();
    GLoc.Map.Poly.setPath(GLoc.Map.PolyArray);
    GLoc.Map.Poly.setMap(GLoc.Map.Object);
};
GLoc.GetPath = function () {
    var VillageEditMode = true;
    if (VillageEditMode) {
        if (GLoc.Map.PolyArray.length <= 2)  // done
        {
            return "";
        }
        else {
            return GLoc.Map.Poly.getPath().getArray();
        }
    } else {
        return "";
    }
};
GLoc.GetPathAsString = function () {
    var mPath = GLoc.GetPath();
    var mArray = null;
    var latlong;

    for (var i = 0; i < mPath.length; i++) {
        if (i == 0) {
            latlong = "[{'lat':" + mPath[i].lat() + "," + "'lng':" + mPath[i].lng() + "}";
        }
        else {
            latlong += ",{" + "'lat':" + mPath[i].lat() + "," + "'lng':" + mPath[i].lng() + "}";
        }

    }   
    mArray = latlong+"]";
    Log("Saving Gloc data");
    Log(mArray);
    $.ajax({
        type: "POST",
        url: "FrmLatLong.aspx/SaveUpdateVillage",
        data: '{latlong: "' + mArray + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {

        }
    });
    return (mArray);
};
function Log(str) {
    try {
        console.log(str);
    } catch (e) {

    }
};
function OnSuccess(response) {
    //alert(response.d);
}
