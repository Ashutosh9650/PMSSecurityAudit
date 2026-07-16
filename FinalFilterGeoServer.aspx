<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FinalFilterGeoServer.aspx.cs" Inherits="FilterGeoServer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <script src="leaflet/leaflet.js" type="text/javascript"></script>
    <link href="leaflet/leaflet.css" rel="stylesheet" type="text/css" />
    <link href="leaflet/leaflet-search.css" rel="stylesheet" type="text/css" />
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/leaflet-search/3.0.2/leaflet-search.min.js" type="text/javascript" ></script>--%>
    <script type="text/javascript" src="js/JS/leaflet-search.min.js"></script>
  
    <link href="leaflet/leaflet.fullscreen.css" rel="stylesheet" type="text/css" />
    <script src="leaflet/Leaflet.fullscreen.js" type="text/javascript"></script>
    <link href="Leaflet/leaflet.zoomhome.css" rel="stylesheet" />
    <script type="text/javascript" src="Leaflet/leaflet.zoomhome.js"></script>
    <script type="text/javascript" src="Leaflet/leaflet.zoomhome.min.js"></script>
    

    <script src="leaflet/spin.min.js" type="text/javascript"></script>
    <script src="leaflet/leaflet.spin.min.js" type="text/javascript"></script>

    <script src="leaflet/jquery-1.4.1.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/jquery.min-3.2.1.js"></script>
    <%--<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css" />--%>
    <link rel="stylesheet" type="text/css" href="css/font-awesome.min.css" />
    <%--<script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>
    <script type="text/jscript" src="js/JS/bootstrap.min.js"></script>
    <%--<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.2.0/jquery.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/jquery.min-2.2.0.js"></script>

    

    
   

    <style type="text/css">
        #ton1
        {
            float: left;
            width: auto;
            height: auto;
            background-color: #920000;
            color: #fff;
            position: fixed;
            z-index: 800;
            top: 106px;
            padding: 10px;
            left:0px;
        }
        
        #div-show1
        {
            float: left;
            width: 30%;
            height: 500px;
            background-color: #08c;
            color: #fff;
            text-align: center;
            text-decoration: underline;
            border: 2px solid #ddd;
            border-radius: 4px;
            display: none;
            position: fixed;
            left:34px;
            z-index: 1000;
            top:106px;
        }
        .butt
        {
            width: 100px;
            height: 34px;
            background-color: #08c;
            color: #fff;
            text-align: center;
            border: 2px solid #ddd;
            border-radius: 4px;
        }
        
        
        
        #ton-new
        {
            float: right;
            width: 100px;
            height: 34px;
            background-color: #920000;
            color: #fff;
        }
        
        #div-show-new
        {
            float: right;
            width: 84%;
            height: 100px;
            background-color: #08c;
            color: #fff;
            text-align: center;
            text-decoration: underline;
            border: 2px solid #ddd;
            border-radius: 4px;
            display: block;
            position: fixed;
            right: 108px;
        }
    </style>
    <style type="text/css">
        #map
        {
            height: 650px;
            width: 100%;
        }
        .info
        {
            padding: 6px 8px;
            font: 14px/16px Arial, Helvetica, sans-serif;
            background: white;
            background: rgba(255,255,255,0.8);
            box-shadow: 0 0 15px rgba(0,0,0,0.2);
            border-radius: 5px;
            width: 250px;
        }
        .info h4
        {
            margin: 0 0 5px;
            color: #777;
        }
    </style>
    <script type="text/javascript">
      

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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


        var legend = L.control({ position: 'bottomright' });

        legend.onAdd = function (map) {

            var div = L.DomUtil.create('div', 'info legend'),
    grades = [0, 25,  50,   75],
    labels = [];

            // loop through our density intervals and generate a label with a colored square for each interval
            for (var i = 0; i < grades.length; i++) {
                div.innerHTML +=
            '<i style="float: left;width: 15px;height: 15px;margin-right: 5px;background:' + getColor(grades[i] + 1) + '"></i> ' +
            grades[i] + (grades[i + 1] ? '&ndash;' + grades[i + 1] + '<br>' : '+');
            }

            return div;
        };

        legend.addTo(map);
        function getColor(d) {


            return d >= 75 ? '#008000' :
           d >= 50 ? '#0000ff' :

           d >= 26 ? '#FD8D3C' :
           d >= 0 ? '#FF0000' :
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
                + "<b>Ach Girl : </b>" +
                feature.properties.AchGirlspercentage + "%"+
                "</br>" 
            )
    }



}).addTo(map);

        var geoJsonUrl = "http://103.11.85.149:8080/geoserver/MW_Grasp_UP/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=MW_Grasp_UP:EducateGirlMP&maxFeatures=2000&outputFormat=application%2Fjson";

        function loadGeoJson(data) {
            myLayerDistrict.addData(data);
        };

        $.ajax({
            url: geoJsonUrl,
            dataType: 'json',
            success: loadGeoJson
        });


    </script>
    
   
</asp:Content>
