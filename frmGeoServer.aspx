<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmGeoServer.aspx.cs" Inherits="frmGeoServer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script src="leaflet/leaflet.js" type="text/javascript"></script>
    <link href="leaflet/leaflet.css" rel="stylesheet" type="text/css" />
    <link href="leaflet/leaflet-search.css" rel="stylesheet" type="text/css" />
    <script src="leaflet/leaflet-search.js" type="text/javascript"></script>
    <script src="leaflet/leaflet-search.min.js" type="text/javascript"></script>
    <link href="leaflet/leaflet.fullscreen.css" rel="stylesheet" type="text/css" />
    <script src="leaflet/Leaflet.fullscreen.js" type="text/javascript"></script>
    <link href="Leaflet/leaflet.zoomhome.css" rel="stylesheet" />
    <script type="text/javascript" src="Leaflet/leaflet.zoomhome.js"></script>
    <script type="text/javascript" src="Leaflet/leaflet.zoomhome.min.js"></script>
    
    <script src="leaflet/spin.min.js" type="text/javascript"></script>
    <script src="leaflet/leaflet.spin.min.js" type="text/javascript"></script>

    <script src="leaflet/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <%--<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css" />--%>
    <link rel="stylesheet" type="text/css" href="css/font-awesome.min.css" />
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.2.0/jquery.min.js"></script>
    

</head>
<body>
    <form id="form1" runat="server">
    <div>
       <div>
        <div class="row" style="margin: 0px;">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="panel panel-default">
                    <div id="weathermap">

                    </div>
                
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        document.getElementById('weathermap').innerHTML = "<div id='map'></div>";

        var map = L.map('map', { fullscreenControl: { pseudoFullscreen: false }, zoomControl: false, loadingControl: true }).setView(new L.LatLng(22.6284408, 74.1108299), 7);

        var zoomHome = L.Control.zoomHome({ position: 'topleft' });
        zoomHome.addTo(map);

        L.tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png?{foo}', { foo: 'bar', fillOpacity: 0.1 }).addTo(map);



        function getColor(d) {


            return d >= 75 ? '#008000' :
           d >= 50 ? '#0000ff' :

           d >= 25 ? '#FF0000' :
           d >= 0 ? '#FD8D3C' :
           '#FFEDA0';
        }

        function style(feature) {

            return {

                fillColor: getColor(feature.properties.AchGirlspercentage),
                weight: 2,
                opacity: 1,
                color: 'white',
                dashArray: '3',
                fillOpacity: 0.7
            };
        }
        var myLayerDistrict = new L.geoJson(null, { pointToLayer: function (feature, latlng) { return L.circleMarker(latlng, { color: getColor(feature.properties.AchGirlspercentage) }); },

            onEachFeature: function (feature, layer) {
                layer.bindPopup(
                "<b>Village Name: </b>" +
                feature.properties.VillageName +
                "</br>"

            )
            }



        }).addTo(map);

        var geoJsonUrl = "http://103.11.85.149:8080/geoserver/BiharUniceff/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=BiharUniceff:EducateGirlMP&maxFeatures=2000&outputFormat=application%2Fjson";

        function loadGeoJson(data) {
            myLayerDistrict.addData(data);
        };

        $.ajax({
            url: geoJsonUrl,
            dataType: 'json',
            success: loadGeoJson
        });


    </script>
    
    </div>
    </form>
</body>
</html>
