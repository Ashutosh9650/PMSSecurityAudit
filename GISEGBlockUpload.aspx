<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Culture="en-GB" CodeFile="GISEGBlockUpload.aspx.cs" Inherits="GISEGBlockUpload" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <%--<link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.4/dist/leaflet.css" />--%>
    <link rel="stylesheet" href="css/CSS/leaflet.css" />
    <%--<script src="https://unpkg.com/leaflet@1.9.4/dist/leaflet.js"></script>--%>
    <script type="text/javascript" src="js/JS/leaflet.js"></script>
    <script type="text/javascript" src="js/JS/shp.min.js"></script>
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css">--%>
    <link rel="stylesheet" href="css/CSS/all.min.css">
    <link href="leaflet2/leaflet.fullscreen.css" rel="stylesheet" type="text/css" />
    <script src="leaflet2/Leaflet.fullscreen.js" type="text/javascript"></script>

    <style>
        #map {
            height: 62vh;
            width: 100%;
        }
    </style>

    <style>
        .page-header-bar {
            width: 100%;
            background: #005a9e;
            color: #fff;
            padding: 12px 20px;
            border-bottom: 4px solid #003f6b;
        }

        .page-title {
            margin: 0;
            font-weight: bold;
        }

        .header-desc {
            float: right;
            font-size: 14px;
            background: rgba(255,255,255,0.2);
            padding: 6px 12px;
            border-radius: 4px;
            margin-top: 3px;
        }
    </style>

    <style>
        #layerList {
            max-height: 320px;
            min-height: 400px;
            overflow-y: auto;
            padding: 8px;
            border-radius: 10px;
            background: #f9f9f9;
            box-shadow: inset 0 0 8px rgba(0,0,0,0.08);
        }

        /* Each layer item */
        .layer-item {
            display: flex;
            align-items: center;
            background: #ffffff;
            padding: 10px 12px;
            margin-bottom: 8px;
            border-radius: 8px;
            cursor: pointer;
            border: 1px solid #e5e5e5;
            transition: 0.25s ease;
        }

            /* Hover effect */
            .layer-item:hover {
                background: #e9f5ff;
                border-color: #bcdfff;
                transform: translateX(4px);
            }

        /* Icon circle */
        .layer-icon {
            width: 32px;
            height: 32px;
            border-radius: 50%;
            background: #007bff;
            display: flex;
            align-items: center;
            justify-content: center;
            color: white;
            margin-right: 12px;
            font-size: 16px;
            font-weight: bold;
        }

        /* Layer text */
        .layer-text {
            font-size: 15px;
            font-weight: 600;
            color: #333;
        }

        /* Active selected */
        .layer-item.active {
            background: #d8ecff;
            border-color: #7bbcff;
        }

        .layer-item.selected {
            background: #d0ebff !important; /* light blue */
            border-left: 4px solid #007bff;
            font-weight: bold;
        }
        /* File upload styling */
        .btn-file {
            position: relative;
            overflow: hidden;
        }

            .btn-file input[type=file] {
                position: absolute;
                top: 0;
                right: 0;
                min-width: 100%;
                min-height: 100%;
                font-size: 100px;
                text-align: right;
                opacity: 0;
                cursor: inherit;
            }
    </style>
    <style type="text/css">
        .update_overlay {
            position: fixed;
            width: 100%;
            height: 100vh;
            top: 0px;
            bottom: 0px;
            background-color: rgb(151 142 142 / 90%);
            z-index: 10000;
        }

        .update_div {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100%;
        }

            .update_div img {
                height: 120px;
            }

        .table-primary {
            background-color: #dbe9ff !important;
        }

        .layer-table-wrapper {
            max-height: 453px; /* adjust as needed */
            overflow-y: auto;
            border: 1px solid #ddd;
        }

        #layerTable thead th {
            position: sticky;
            top: 0;
            background: #f8f9fa;
            z-index: 2;
        }
    </style>
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <!-- ===========================
     PAGE CONTAINER
=========================== -->
    <div class="container-fluid" style="padding: 0; margin: 0;">
        <div class="row">
            <!-- ================= HEADER ================= -->
            <div class="col-sm-12">
                <div class="panel panel-default" style="background: linear-gradient(to bottom,  #ffffff 1%,#ffffff 1%,#ebf1fd 100%) !important; margin-bottom: 8px;">
                    <div class="panel-heading" style="background-color: transparent; padding: 5px 10px;">

                        <div class="row" style="margin-left: -15px; margin-right: -15px">
                            <div class="col-sm-12">
                                <div style="display:flex; justify-content:space-between; align-items:center;flex-flow:wrap">
                                    <asp:Label ID="lblMain" runat="server" Text="Layer Upload" Style="margin: 3px 0px 5px 5px; font-weight: bold; font-size: medium;"></asp:Label>
                                <%--<h3 class="page-title pull-left">Shapefile Upload & Publish</h3>--%>
                                 <asp:LinkButton ID="btnexport" runat="server" class="" Text="Export" OnClick="btnexport_Click">Export Layer Data to Excel</asp:LinkButton>
</div>
                            </div>

                        </div>

                    </div>
                    <div>
                    </div>
                </div>
            </div>
            <!-- ================= HEADER END ================= -->

            <div class="col-sm-12">
                <div class="panel panel-default" style="background: linear-gradient(to bottom,  #ffffff 1%,#ffffff 1%,#ebf1fd 100%) !important; margin-bottom: 8px;">
                    <div class="panel-heading" style="background-color: transparent; padding: 5px 10px;">

                        <div class="row" style="margin-left: -15px; margin-right: -15px">
                            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding-right: 0px;">
                                <div class="form-group">
                                    <label style="padding-top: 2px; font-weight: bold !important;">Select ShapeFile </label>
                                    <div class="input-group">
                                        <span class="input-group-btn">
                                            <span class="btn btn-primary btn-file">Browse…
                                    <%--<asp:FileUpload ID="fuFile" runat="server" OnChange="updateFileName()" />--%>
                                                <input type="file" id="fuFile" accept=".geojson" />
                                            </span>
                                        </span>
                                        <input type="text" class="form-control" id="fileNameDisplay" readonly>
                                    </div>
                                </div>

                            </div>

                            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding-right: 0px;">
                                <div class="form-group">
                                    <label style="padding-top: 2px; font-weight: bold !important;">Shapefile Name</label>
                                    <input type="text" class="form-control" id="txt_shpFileName" placeholder="Enter shapefile name">
                                    <input type="text" class="form-control" id="shapefileNameInput" style="display: none;" placeholder="Enter shapefile name">
                                </div>
                            </div>

                            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding-right: 0px;">
                                <div class="form-group">
                                    <label style="padding-top: 2px; font-weight: bold !important;">Layer Type</label>
                                    <select id="ddlLayerType" class="form-control">
                                        <option value="">-- Select Type --</option>
                                        <option value="1">State</option>
                                        <option value="2">District</option>
                                        <option value="3">Block</option>
                                        <option value="4">Village</option>
                                    </select>
                                    <select id="LayerType" class="form-control" style="display: none;">
                                        <option value="">-- Select Type --</option>
                                        <option value="1">State</option>
                                        <option value="2">District</option>
                                        <option value="3">Block</option>
                                        <option value="4">Village</option>
                                    </select>
                                </div>
                            </div>

                            <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:HiddenField ID="hidGeoJson" runat="server" />
                                    <button type="button" id="btnUpload" class="btn btn-info btn-block">Upload</button>
                                    <button type="button" id="uploadBtn" class="btn btn-info btn-block" style="display: none;">
                                        <i class="glyphicon glyphicon-upload"></i>Upload & Render
                                    </button>
                                    <asp:Label ID="lblMsg" runat="server" />
                                    <button type="button" id="publishBtn" class="btn btn-success btn-block" style="margin-top: 10px; display: none;">
                                        <i class="glyphicon glyphicon-cloud"></i>Publish to GeoServer
                                    </button>
                                </div>
                            </div>

                        </div>

                    </div>
                    <div>
                    </div>
                </div>
            </div>
        </div>




        <div class="update_overlay">
            <div class="update_div">
                <img src="images/progress2.gif" />
            </div>
        </div>



        <div class="row" style="margin: 0;">

            <!-- ===========================
             LEFT SIDE — BLANK CARD
        ============================ -->
           <div class="col-sm-4" style="background:#f9f9f9; height:56vh; border-right:1px solid #ddd;">

    <div class="card p-2">

        <!-- Layer Type + Dropdown + Search (Single Row) -->
        <div class="form-group row align-items-center mb-2">
            <label class="col-sm-2 col-form-label p-0" style="padding-top: 2px; margin-left: 3px;font-weight: bold !important;">
                <b>Layer Type:</b>
            </label>

            <div class="col-sm-3 p-0 pe-2">
                <select id="layerTypeFilter" class="form-control form-control-sm">
                    <option value="">All Types</option>
                    <option value="1">State</option>
                    <option value="2">District</option>
                    <option value="3">Block</option>
                    <option value="4">Village</option>
                </select>
            </div>

            <div class="col-sm-6 p-0" style="margin-left:30px;">
                <input type="text"
                       id="layerSearch"
                       class="form-control form-control-sm"
                       placeholder="Search layer...">
            </div>
        </div>

        <asp:HiddenField ID="hiddenLayerId" runat="server" />

        <button type="button"
                id="btnMaplayer"
                class="btn btn-info btn-sm mb-2"
                style="display:none;"
                onclick="Map_savedLayer();">
            Map Layer
        </button>

        <asp:Label ID="Label1" runat="server" style="display:none;"/>
       

        <!-- Layer Table -->
        <div class="layer-table-wrapper" style="max-height:56vh; overflow-y:auto;">
            <table class="table table-sm table-hover" id="layerTable">
                <thead class="table-light">
                    <tr>
                        <th style="width:20%">Layer Type</th>
                        <th>Layer Name</th>
                        <th style="width:10%" class="text-center">Delete</th>
                    </tr>
                </thead>
                <tbody id="layerList">
                    <!-- dynamically populated -->
                </tbody>
            </table>
        </div>

    </div>

</div>


            <!-- ===========================
             RIGHT SIDE — ALL PANELS
        ============================ -->
            <div class="col-sm-8" style="padding: 0; margin: 0;">

                <div class="row" style="margin: 0;">

                    <!-- MAP PANEL -->
                    <div class="col-sm-12" style="margin: 0;">
                        <div id="map" style="width: 100%; height: 62vh;"></div>
                    </div>
                    <!-- RIGHT CONTROL PANEL
                    <div class="col-sm-4" style="background: #f7f7f7; height: 100vh; overflow-y: auto; padding: 15px; border-left: 1px solid #ddd;">

                        
                    </div> -->



                </div>

            </div>

        </div>
    </div>

    <script type="text/javascript">

        function Map_savedLayer() {
            <%--var layerType = $('#<%= ddlLayerType.ClientID %>').val();  // Get the LayerType value from dropdown
            var layerName = $('#<%= txtLayerName.ClientID %>').val();  // Get the LayerName value from textbox--%>
            var storedlayerid = sessionStorage.getItem('layerid');
            var layerType = $("#layerTypeFilter").val() || "";
            var layerid = storedlayerid;

            if (!storedlayerid || storedlayerid === "") {
                alert("Please select a Layer");
                return;  // Exit function if no layer is selected
            }

            // Make the AJAX call
            $.ajax({
                type: "POST",
                url: "<%= ResolveUrl("GISEGBlockUpload.aspx/Map_SavedLayer") %>",  // Point to the server-side method
                data: JSON.stringify({ LayerType: layerType, layerid: layerid }),  // Send the data
                contentType: "application/json; charset=utf-8",  // Specify content type
                dataType: "json",  // Expect a JSON response
                success: function (response) {
                    // Handle success - display the returned InsertedId or success message
                    alert("Mapping Successful");
                },
                error: function (xhr, status, error) {
                    // Handle errors
                    //alert("Error occurred: " + error);
                    console.log("Error occurred: " + error);
                }
            });
        }



        function ExportLayer() {
    <%--var layerType = $('#<%= ddlLayerType.ClientID %>').val();  // Get the LayerType value from dropdown
    var layerName = $('#<%= txtLayerName.ClientID %>').val();  // Get the LayerName value from textbox--%>
            var storedlayerid = sessionStorage.getItem('layerid');
            var layerType = $("#layerTypeFilter").val() || "";
            var layerid = storedlayerid;

            if (!storedlayerid || storedlayerid === "") {
                alert("Please select a Layer");
                return;  // Exit function if no layer is selected
            }

            // Make the AJAX call
            $.ajax({
                type: "POST",
                url: "<%= ResolveUrl("GISEGBlockUpload.aspx/ExportLayer") %>",  // Point to the server-side method
                data: JSON.stringify({ LayerType: layerType, layerid: layerid }),  // Send the data
                contentType: "application/json; charset=utf-8",  // Specify content type
                dataType: "json",  // Expect a JSON response
                success: function (response) {
                    // Handle success - display the returned InsertedId or success message
                    //alert(response.d);
                    console.log(response.d);
                },
                error: function (xhr, status, error) {
                    // Handle errors
                    //alert("Error occurred: " + error);
                    console.log("Error occurred: " + error);
                }
            });
        }



        function showloader() {
            $(".update_overlay").show();
        }
        function hideloader() {
            $(".update_overlay").fadeOut(300);
        }

        function showhideloader() {
            setTimeout(function () {
                $(".update_overlay").hide();
            }, 4000);

        }

        //function MapLayerWithLoader() {
        //    $(".update_overlay").show(); // START loader

        //    // Call server-side method via __doPostBack
        //    __doPostBack('btnUpload', '');

        //    return false; // prevent double postback
        //}
        //function ExportWithLoader() {
        //    $(".update_overlay").show(); // START loader

        //    // Call server-side method via __doPostBack
        //    __doPostBack('btnexport', '');

        //    return false; // prevent double postback
        //}
        //function showAlert(msg) {
        //    $(".update_overlay").hide();
        //    alert(msg);
        //}
    </script>

    <script type="text/javascript">
        /* ===========================
           INIT MAP
        =========================== */
        //const map = L.map("map", {
        //    zoomControl: false   // hide default zoom
        //}).setView([22.5, 78.9], 5);

        //L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png").addTo(map);

        /* Add Zoom Control (Visible Now) */
        //L.control.zoom({
        //    position: "topleft"
        //}).addTo(map);

        const map = L.map('map', {
            maxZoom: 18,
            minZoom: 4,
            dragging: true,
            fullscreenControl: { pseudoFullscreen: false }
        }).setView([22.5, 78.9], 4.4);   // <-- India default view

        var BaseUrls = 'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1Ijoia3dyaXNhY2l3cm0iLCJhIjoiY2xma3p3NmpoMDBhaTNwbnV1NnVkMGp2ZCJ9.1ASKnwxbjSZxZGiXn0xl4Q';

        function BaseLyrOptionsM(styleId) {
            return {
                maxZoom: 18,
                tileSize: 512,
                zoomOffset: -1,
                attribution: 'A',
                id: styleId
            };
        }

        // Base map
        const baseLayer = L.tileLayer(BaseUrls, BaseLyrOptionsM('mapbox/outdoors-v11'));
        baseLayer.addTo(map);

        //L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        //    maxZoom: 18
        //}).addTo(map);
        //var url = "https://geo1server.educategirls.ngo/geoserver/wms?service=WMS&version=1.1.0&request=GetMap&layers=EG:vw_Uploaded_Layers&bbox=MINX,MINY,MAXX,MAXY&srs=EPSG:4326&width=800&height=600&format=image/png";

        //url.addTo(map);

        // GeoServer WMS layer
        //var villageWMS = L.tileLayer.wms(
        //    "https://geo1server.educategirls.ngo/geoserver/EG/wms",
        //    {
        //        layers: 'cv_639028000343104550',
        //        format: 'image/png',
        //        transparent: true,
        //        version: '1.1.1',
        //        attribution: "GeoServer"
        //    }
        //);

        //villageWMS.addTo(map);

        let geojsonLayer = null;
        let shapefileBlob = null;
        let geojsonData = null;

        function rgbToHexSafe(color) {

            if (!color) return null;

            // already hex
            if (color.startsWith("#")) return color.toUpperCase();

            // rgb(r,g,b)
            if (color.startsWith("rgb")) {
                const m = color.match(/\d+/g);
                if (!m || m.length < 3) return null;

                return "#" + m.slice(0, 3)
                    .map(x => parseInt(x).toString(16).padStart(2, "0"))
                    .join("")
                    .toUpperCase();
            }

            return null;
        }

        /* ===========================================================
           1. UPLOAD + RENDER SHAPEFILE
        =========================================================== */
        document.getElementById("uploadBtn").onclick = async () => {

            const file = document.getElementById("shapefileInput").files[0];

            if (!file) {
                alert("Please select a shapefile (.zip)");
                return;
            }

            shapefileBlob = file;

            try {
                const arrayBuffer = await file.arrayBuffer();
                geojsonData = await shp(arrayBuffer);

                if (geojsonLayer) map.removeLayer(geojsonLayer);

                geojsonLayer = L.geoJSON(geojsonData, {

                    style: function (feature) {

                        let color = rgbToHexSafe(feature.properties?.COLOR_HE);

                        // fallback ONLY if invalid
                        if (!color) {
                            color = "#666666";
                        }

                        return {
                            color: "#000000",
                            fillColor: color,
                            weight: 1,
                            fillOpacity: 1
                        };
                    },
                    onEachFeature: function (feature, layer) {
                        layer.bindPopup(`<pre>${JSON.stringify(feature.properties, null, 2)}</pre>`);
                    }

                }).addTo(map);

                map.fitBounds(geojsonLayer.getBounds());

                alert("✔ Shapefile rendered successfully!");

            } catch (err) {
                console.error(err);
                //alert("❌ Error reading shapefile. Check ZIP contents.");
            }
        };


        //"Upload failed");
        document.getElementById("publishBtn").onclick = async function () {

            if (!shapefileBlob) {
                alert("Upload shapefile first.");
                return;
            }

            const inputName = document.getElementById("shapefileNameInput").value.trim();

            if (!inputName) {
                alert("Please enter a shapefile name for GeoServer.");
                return;
            }

            const finalName = inputName.endsWith(".zip") ? inputName : inputName + ".zip";

            // Convert file → base64
            const arrayBuffer = await shapefileBlob.arrayBuffer();
            const uint8 = new Uint8Array(arrayBuffer);

            let binary = "";
            uint8.forEach(b => binary += String.fromCharCode(b));

            const base64 = btoa(binary);

            // AJAX to ASP.NET WebMethod
            $.ajax({
                type: "POST",
                url: "GISEGBlockUpload.aspx/UploadToGeoServer",
                data: JSON.stringify({
                    fileName: $("#shapefileNameInput").val(),
                    base64: base64,
                    layetype: $("#LayerType").val()
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    loadLayerList();
                    //alert("✔ " + res.d);
                },
                error: function (err) {
                    console.log(err);
                    //alert("❌ Upload failed.");
                }
            });

        };

        $(document).on("change", "#shapefileInput", function () {
            let fileName = $(this).val().split("\\").pop();
            $("#fileNameDisplay").val(fileName);
        });
        //----------------------------------//
        $(document).ready(function () {
            debugger;

            loadLayerList();
        });
        $("#layerTypeFilter").on("change", function () {
            loadLayerList();
        });

        var wmsLayer = null;

        function loadLayerList() {

            $(".update_overlay").show();

            let layerType = $("#layerTypeFilter").val() || "";

            $.ajax({
                type: "POST",
                url: "GISEGBlockUpload.aspx/GetLayers",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ layerType: layerType }),

                success: function (res) {

                    let layers = res.d;
                    let tbody = $("#layerList");
                    tbody.empty();

                    layers.forEach(l => {

                        tbody.append(`
                <tr class="layer-row"
                    data-layerid="${l.LayerID}"
                    data-workspace="${l.Workspace}"
                    data-layer="${l.GeoServerLayer}"
                    data-url="${l.GeoServerURL}"
                    data-type="${l.LayerType}"
                    style="cursor:pointer;">

                    <td>${l.LayerType}</td>
                    <td><b>${l.LayerName}</b></td>
                    <td class="text-center">
                        <button class="btn btn-sm btn-danger delete-layer"
                                data-layerid="${l.LayerID}">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    </td>
                </tr>
            `);
                    });

                    // ---------------- SELECT LAYER ----------------
                    $(".layer-row").off("click").on("click", function () {

                        $(".update_overlay").show();

                        $(".layer-row").removeClass("table-primary");
                        $(this).addClass("table-primary");

                        let layerid = $(this).data("layerid");
                        let geourl = $(this).data("url");
                        let geolayer = $(this).data("layer");

                        sessionStorage.setItem('layerid', layerid);
                        document.getElementById('<%= hiddenLayerId.ClientID %>').value = layerid;

                        loadWMSLayer(geourl, geolayer);

                        function loadWMSLayer(geourl, geolayer) {

                            // Remove previous layer if exists
                            if (wmsLayer && map.hasLayer(wmsLayer)) {
                                wmsLayer.off();
                                map.removeLayer(wmsLayer);
                            }

                            // Create new WMS layer
                            wmsLayer = L.tileLayer.wms(geourl + "/geoserver/EG/wms", {
                                layers: geolayer,
                                format: "image/png",
                                transparent: true,
                                version: "1.1.1",
                                attribution: "GeoServer"
                            });

                            wmsLayer.addTo(map);
                        }

                        $(".update_overlay").hide();
                    });

                    // ---------------- DELETE LAYER ----------------
                    $(".delete-layer").off("click").on("click", function (e) {
                        debugger;
                        e.stopPropagation();

                        let layerid = $(this).data("layerid");

                        if (!confirm("Are you sure you want to delete this layer?")) return;

                        $(".update_overlay").show();

                        $.ajax({
                            type: "POST",
                            url: "GISEGBlockUpload.aspx/DeleteLayer",
                            contentType: "application/json; charset=utf-8",
                            data: JSON.stringify({ layerid: layerid }),

                            success: function () {
                                alert("Layer deleted successfully.");
                                loadLayerList();

                                if (geojsonLayer !== null) {
                                    map.removeLayer(geojsonLayer);
                                    geojsonLayer = null;
                                }
                            },

                            error: function () {
                                console.log("Error deleting layer.");
                            },

                            complete: function () {
                                $(".update_overlay").hide();
                            }
                        });
                    });

                    //-------- SEARCH FILTER --------
                    $("#layerSearch").on("keyup", function () {

                        let value = $(this).val().toLowerCase();

                        $("#layerList tr").filter(function () {
                            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
                        });
                    });

                },

                error: function () {
                    console.log("Failed to load layer list.");
                },

                complete: function () {
                    $(".update_overlay").hide();
                }
            });
        }

      <%--  function loadLayerList() {

            $(".update_overlay").show(); // START loader for layer list

            let layerType = $("#layerTypeFilter").val() || "";

            $.ajax({
                type: "POST",
                url: "GISEGBlockUpload.aspx/GetLayers",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ layerType: layerType }),

                success: function (res) {
                    let layers = res.d;
                    $("#layerList").empty();

                    layers.forEach(l => {
                        $("#layerList").append(`
                    <div class="layer-item d-flex justify-content-between align-items-center"
                         data-layerid="${l.LayerID}"
                         data-workspace="${l.Workspace}"
                         data-layer="${l.GeoServerLayer}"
                         data-url="${l.GeoServerURL}"
                         data-type="${l.LayerType}"
                         style="padding:6px; cursor:pointer; border-bottom:1px solid #eee;">

                        <span class="layer-name"><b>${l.LayerName}</b></span>

                        <button class="btn btn-sm btn-danger delete-layer ms-auto" style="margin-left: auto;"
                                data-layerid="${l.LayerID}">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    </div>
                `);
                    });

                    // ---------------- SELECT LAYER ----------------
                    $(".layer-item").off("click").on("click", function () {
                        debugger;
                        $(".update_overlay").show(); // START loader for WFS

                        $(".layer-item").removeClass("selected");
                        $(this).addClass("selected");

                        let layerid = $(this).data("layerid");
                        let geourl = $(this).data("url");
                        let geolayer = $(this).data("layer");

                        console.log(layerid, geourl, geolayer);

                        sessionStorage.setItem('layerid', layerid);
                        document.getElementById('<%= hiddenLayerId.ClientID %>').value = layerid;
                        //var wmsLayer = null; 
                        //if (wmsLayer) {
                        //    wmsLayer.off();
                        //    map.removeLayer(wmsLayer);
                        //}
                        //console.log("Current WMS:", wmsLayer);


                        //wmsLayer = L.tileLayer.wms(geourl+"/geoserver/EG/wms",
                        //    {
                        //        layers: geolayer,
                        //        format: "image/png",
                        //        transparent: true,
                        //        version: "1.1.1",
                        //        attribution: "GeoServer"
                        //    }
                        //).addTo(map);

                        loadWMSLayer(geourl, geolayer);


                        function loadWMSLayer(geourl, geolayer) {

                            // Remove previous layer if exists
                            if (wmsLayer && map.hasLayer(wmsLayer)) {
                                wmsLayer.off();
                                map.removeLayer(wmsLayer);
                            }

                            // Create new WMS layer
                            wmsLayer = L.tileLayer.wms(geourl + "/geoserver/EG/wms", {
                                layers: geolayer,
                                format: "image/png",
                                transparent: true,
                                version: "1.1.1",
                                attribution: "GeoServer"
                            });

                            wmsLayer.addTo(map);
                        }
                        //zoomToWMSLayer(geourl, geolayer);

                        //wmsLayer = L.tileLayer.wms(geourl + "/geoserver/EG/wms", {
                        //    layers: geolayer,
                        //    format: "image/png",
                        //    transparent: true,
                        //    version: "1.1.1",
                        //    attribution: "GeoServer",
                        //    tiled: false,
                        //    crossOrigin: true
                        //}).addTo(map);

                        $(".update_overlay").hide();

                        //enableWMSHover(geourl, geolayer);

                //let wfsUrl =
                //    'https://geo1server.educategirls.ngo/geoserver/EG/ows?' +
                //    'service=WFS&version=1.0.0&request=GetFeature' +
                //    '&typeName=EG%3Avw_Uploaded_Layers' +
                //    '&maxFeatures=50000&outputFormat=application%2Fjson' +
                //    '&viewparams=LayerID:' + layerid;

                //$.ajax({
                //    type: "POST",
                //    url: "GISEGBlockUpload.aspx/GetWFSLayer",
                //    contentType: "application/json; charset=utf-8",
                //    data: JSON.stringify({ wfsUrl: wfsUrl }),

                //    success: function (res) {
                //        let data = JSON.parse(res.d);

                //        geojsonLayer = L.geoJSON(data, {
                //            style: function (feature) {
                //                let mapped = feature.properties?.mapped;
                //                return {
                //                    color: "#000",
                //                    fillColor: mapped == "1" ? "#ADEBB3" : "#D3D3D3",
                //                    weight: 1,
                //                    fillOpacity: 1
                //                };
                //            },

                //            onEachFeature: function (feature, layer) {
                //                layer.on("click", function () {
                //                    let props = feature.properties;

                //                    let popupHtml = `
                //                        <div class="panel panel-primary">
                //                            <div class="panel-heading"><h4>Attributes</h4></div>
                //                            <table class="table table-bordered table-striped">
                //                                ${Object.keys(props).map(k =>
                //                        `<tr><th>${k}</th><td>${props[k] ?? ""}</td></tr>`
                //                    ).join('')}
                //                            </table>
                //                        </div>
                //                    `;
                //                    layer.bindPopup(popupHtml).openPopup();
                //                });
                //            }
                //        }).addTo(map);

                //        map.fitBounds(geojsonLayer.getBounds());
                //    },

                //    error: function () {
                //        //alert("Failed to load layer");
                //        console.log("Failed to load layer");
                //    },

                //    complete: function () {
                //        $(".update_overlay").hide(); // END loader for WFS
                //    }
                //});
            });

            // ---------------- DELETE LAYER ----------------
            $(".delete-layer").off("click").on("click", function (e) {

                e.stopPropagation();

                let layerid = $(this).data("layerid");

                if (!confirm("Are you sure you want to delete this layer?")) return;

                $(".update_overlay").show(); // START loader for delete

                $.ajax({
                    type: "POST",
                    url: "GISEGBlockUpload.aspx/DeleteLayer",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ layerid: layerid }),

                    success: function () {
                        alert("Layer deleted successfully.");
                        loadLayerList();

                        if (geojsonLayer !== null) {
                            map.removeLayer(geojsonLayer);
                            geojsonLayer = null;
                        }
                    },

                    error: function () {
                        //alert("Error deleting layer.");
                        console.log("Error deleting layer.");
                    },

                    complete: function () {
                        $(".update_overlay").hide(); // END loader for delete
                    }
                });
            });
        },

        error: function () {
            //alert("Failed to load layer list.");
            console.log("Failed to load layer list.");
        },

        complete: function () {
            $(".update_overlay").hide(); // END loader for layer list
        }
    });
        }--%>



    </script>

    <script type="text/javascript">

        $(document).on("change", "#fuFile", function () {
            let fileName = $(this).val().split("\\").pop();
            $("#fileNameDisplay").val(fileName);
        });

        $("#btnUpload").click(function (e) {
            e.preventDefault();

            let fileInput = document.getElementById("fuFile");
            let file = fileInput.files[0];
            let fileName = $("#txt_shpFileName").val();
            let layerType = $("#ddlLayerType").val();

            if (!file) {
                alert("Select GeoJSON file");
                return;
            }

            // 🔒 File extension validation
            let ext = file.name.split('.').pop().toLowerCase();
            if (ext !== "geojson") {
                alert("Only .geojson files are allowed");
                //fileInput.value = ""; // reset file input
                return;
            }

            if (!fileName) {
                alert("Please enter shape file name");
                return;
            }

            if (!layerType) {
                alert("Please Select Layer Type");
                return;
            }

            if (layerType == '1') {
                alert("State layer import is under progress, please upload village layer");
                return;
            }
            if (layerType == '2') {
                alert("District layer import is under progress, please upload village layer");
                return;
            }
            if (layerType == '3') {
                alert("Block layer import is under progress, please upload village layer");
                return;
            }

            if (layerType == '4') {
                uploadAndExport();
            }
            else {
                alert("Please select village layer");
                return;
            }
        });


        //$("#btnUpload").click(function (e) {
        //    e.preventDefault();

        //    let file = document.getElementById("fuFile").files[0];
        //    let fileName = $("#txt_shpFileName").val();
        //    let layerType = $("#ddlLayerType").val();

        //    if (!file) { alert("Select GeoJSON file"); return; }
        //    if (!fileName) { alert("Please enter shape file name"); return; }
        //    if (!layerType) { alert("Please Select Layer Type"); return; }
        //    if (layerType=='1') { alert("State layer import is under progress, please upload village layer"); return; }
        //    if (layerType== '2') { alert("District layer import is under progress, please upload village layer"); return; }
        //    if (layerType== '3') { alert("Block layer import is under progress, please upload village layer"); return; }
        //    if (layerType == '4') {
        //        uploadAndExport();
        //    }
        //    else
        //    {
        //        alert("please select village layer"); return;
        //    }
            
        //});

        function uploadAndExport() {

            $(".update_overlay").show();   // 🟢 START loader

            let file = document.getElementById("fuFile").files[0];
            let formData = new FormData();
            formData.append("file", file);

            // 1️⃣ Upload file
            $.ajax({
                url: "UploadShapefile.ashx",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                timeout: 0,

                success: function (geoJsonText) {
                    // 2️⃣ Call Export after upload finishes
                    callExport(geoJsonText);
                },

                error: function () {
                    $(".update_overlay").hide();  // 🔴 STOP loader
                    alert("GeoJSON upload failed");
                }
            });
        }

        function callExport(geoJsonText) {
            $(".update_overlay").show();
            let fileName = $("#txt_shpFileName").val();
            let layerType = $("#ddlLayerType").val();

            $.ajax({
                url: "GISEGBlockUpload.aspx/ExportShapefile",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    fileName: fileName,
                    geojson: JSON.parse(geoJsonText),    // 🔥 send raw string
                    layertype: layerType
                }),

                success: function (res) {
                    loadLayerList();
                    $(".update_overlay").hide();  // 🔴 STOP loader
                    alert(res.d);
                },

                error: function (err) {
                    console.log(err);
                    $(".update_overlay").hide();  // 🔴 STOP loader
                    alert("Geoserver Export failed!");
                }
            });
            
        }
        function enableWMSHover(geourl, layer) {

            let popup = L.popup({ closeButton: false, autoClose: true });

            let lastRequestTime = 0;

            map.on("mousemove", function (e) {

                if (!wmsLayer) return;

                let now = Date.now();
                if (now - lastRequestTime < 120) return;   // throttle
                lastRequestTime = now;

                let point = map.latLngToContainerPoint(e.latlng, map.getZoom());
                let size = map.getSize();
                let bounds = map.getBounds();

                // Correct bbox order for WMS 1.1.1 → minx,miny,maxx,maxy
                let bbox = [
                    bounds.getWest(),
                    bounds.getSouth(),
                    bounds.getEast(),
                    bounds.getNorth()
                ].join(",");

                let params = {
                    request: "GetFeatureInfo",
                    service: "WMS",
                    srs: "EPSG:4326",
                    styles: "",
                    transparent: true,
                    version: "1.1.1",
                    format: "image/png",
                    bbox: bbox,
                    height: size.y,
                    width: size.x,
                    layers: layer,
                    query_layers: layer,
                    info_format: "application/json",
                    feature_count: 1,
                    x: Math.round(point.x),
                    y: Math.round(point.y)
                };

                let url = geourl + "/geoserver/EG/wms" +
                    L.Util.getParamString(params);

                fetch(url)
                    .then(r => r.json())
                    .then(data => {

                        if (!data.features || !data.features.length) {
                            popup.remove();
                            return;
                        }

                        let props = data.features[0].properties;

                        let html = `<b>Attributes</b><table class="table table-bordered table-sm">`;
                        for (let k in props) {
                            html += `<tr><th>${k}</th><td>${props[k] ?? ""}</td></tr>`;
                        }
                        html += `</table>`;

                        popup.setLatLng(e.latlng)
                            .setContent(html)
                            .openOn(map);
                    })
                    .catch(() => popup.remove());
            });
        }

        function zoomToWMSLayer(geourl, geolayer) {

            let url = geourl + "/geoserver/EG/ows?service=WMS&version=1.1.1" +
                "&request=GetCapabilities";

            fetch(url)
                .then(res => res.text())
                .then(xmlText => {
                    let parser = new DOMParser();
                    let xml = parser.parseFromString(xmlText, "text/xml");

                    let layers = xml.getElementsByTagName("Layer");

                    for (let i = 0; i < layers.length; i++) {
                        let name = layers[i].getElementsByTagName("Name")[0]?.textContent;

                        if (name === geolayer) {

                            let bbox = layers[i].getElementsByTagName("LatLonBoundingBox")[0];

                            let west = parseFloat(bbox.getAttribute("minx"));
                            let south = parseFloat(bbox.getAttribute("miny"));
                            let east = parseFloat(bbox.getAttribute("maxx"));
                            let north = parseFloat(bbox.getAttribute("maxy"));

                            let bounds = L.latLngBounds(
                                [south, west],
                                [north, east]
                            );

                            map.fitBounds(bounds, { padding: [30, 30] });
                            break;
                        }
                    }
                });
        }





    </script>


    <%-- <script type="text/javascript">
        $(document).on("change", "#fuFile", function () {
            let fileName = $(this).val().split("\\").pop();
            $("#fileNameDisplay").val(fileName);
        });
        $("#btnUpload").click(async function (e) {
            let file = document.getElementById("fuFile").files[0];
            if (!file) {

                alert("Select GeoJSON file");
                return;
            }
            let fileName = $("#txt_shpFileName").val();
            let layerType = $("#ddlLayerType").val();

            if (fileName == "") { alert("Please enter shape file name"); return; }
            if (layerType == "") { alert("Please Select Layer Type"); return; }
            e.preventDefault();
            uploadAndExport();
        });
        function uploadAndExport() {
            $(".update_overlay").show();
            let file = document.getElementById("fuFile").files[0];
            if (!file) {
                $(".update_overlay").hide();
                alert("Select GeoJSON file");
                return;
            }
            $(".update_overlay").show();
            let formData = new FormData();
            formData.append("file", file);

            // 1️⃣ Upload file
            $.ajax({
                url: "UploadShapefile.ashx",     // file receiver
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                timeout: 0,
                success: function (geoJsonText) {
                    $(".update_overlay").show();
                    // 2️⃣ Call your ExportShapefile WebMethod
                    callExport(geoJsonText);
                },
                error: function () {
                    $(".update_overlay").hide();
                    alert("Geoserver upload failed");
                }
            });
           
        }

        function callExport(geoJsonText) {
            $(".update_overlay").show();
            let fileName = $("#txt_shpFileName").val();
            let layerType = $("#ddlLayerType").val();

            $.ajax({
                url: "GISEGBlockUpload.aspx/ExportShapefile",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    fileName: fileName,
                    geojson: JSON.parse(geoJsonText),
                    layertype: layerType
                }),
                success: function (res) {
                    $(".update_overlay").hide();
                    alert(res.d);
                },
                error: function (err) {
                    console.log(err);
                    $(".update_overlay").hide();
                    alert("Geoserver Export failed!");
                }
            });
            $(".update_overlay").hide();
        }



    </script>--%>
</asp:Content>
