<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VillageMapping.aspx.cs" Inherits="VillageMapping" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Village Mapping — Drag & Drop + AutoSuggest (Many-to-Many)</title>

    <!-- jQuery + jQuery UI -->
    <script type="text/javascript" src="Scripts/jquery-3.6.0.min.js"></script>
    <%--<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>--%>
    <link href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" rel="stylesheet" />
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
    <script src="Scripts/comman.js" type="text/javascript"></script>
    <link rel="stylesheet"
        href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css" />

    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>


      
  <script src="leaflet2/leaflet.js" type="text/javascript"></script>
  <link href="leaflet2/leaflet.css" rel="stylesheet" type="text/css" />
  <link href="leaflet2/leaflet.fullscreen.css" rel="stylesheet" type="text/css" />
  <script src="leaflet2/Leaflet.fullscreen.js" type="text/javascript"></script>
  <script src="leaflet2/leaflet.zoomhome.min.js" type="text/javascript"></script>
  <link href="Leaflet2/LeafletClustersMarkers/MarkerCluster.Default.css" rel="stylesheet" />
  <link href="Leaflet2/LeafletClustersMarkers/MarkerCluster.css" rel="stylesheet" />

  <script src="Leaflet2/leaflet.spin.js"></script>
  <script src="Leaflet2/leaflet.spin.min.js"></script>

  <script src="Leaflet2/LeafletClustersMarkers/leaflet.markercluster.js"></script>
  <script src="https://cdn.jsdelivr.net/npm/leaflet-easybutton@2/src/easy-button.js"></script>
  <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/leaflet-easybutton@2/src/easy-button.css">
  <script src="Leaflet2/bundle.js"></script>

  <script src="Leaflet2/leaflet.groupedlayercontrol.min.js"></script>

  <script src="Leaflet2/leaflet.spin.min.js" charset="utf-8"></script>
  <script src="Leaflet2/L.Control.Locate.js"></script>
  <script src="Leaflet2/leaflet-search.js"></script>
  <link href="Leaflet2/leaflet-search.css" rel="stylesheet" type="text/css" />

<%--  <link type="text/css" href="https://cdn.datatables.net/1.13.7/css/dataTables.bootstrap.min.css">
  <link type="text/css" href="https://cdn.datatables.net/fixedheader/3.4.0/css/fixedHeader.bootstrap.min.css">--%>

  <%--<script type="text/javascript" src="https://code.jquery.com/jquery-3.7.0.js"></script>--%>
  <%--<script type="text/javascript" src="Scripts/jquery.dataTables.min.js"></script>--%>
  <script src="https://cdn.datatables.net/1.13.7/js/jquery.dataTables.min.js"></script>
  <script type="text/javascript" src="https://cdn.datatables.net/1.13.7/js/dataTables.bootstrap.min.js"></script>
  <script type="text/javascript" src="https://cdn.datatables.net/fixedheader/3.4.0/js/dataTables.fixedHeader.min.js"></script>

  <!-- Esri Leaflet CSS and JS -->
  <link rel="stylesheet" href="https://unpkg.com/esri-leaflet-geocoder/dist/esri-leaflet-geocoder.css" />
  <script src="https://unpkg.com/esri-leaflet/dist/esri-leaflet.js"></script>
  <!-- map Loader -->
  <script src="https://cdnjs.cloudflare.com/ajax/libs/spin.js/2.3.2/spin.min.js"></script>

    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 12px;
        }

        .container {
            display: flex;
            gap: 18px;
            margin-top: 12px;
        }

        .panel {
            width: 40%;
            border: 1px solid #ddd;
            border-radius: 6px;
            padding: 12px;
            min-height: 480px;
            box-sizing: border-box;
        }
        .panel1 {
            width: 60%;
            border: 1px solid #ddd;
            border-radius: 6px;
            padding: 12px;
            min-height: 480px;
            box-sizing: border-box;
        }

        h3 {
            margin-top: 0;
        }

        .list {
            min-height: 405px;
            max-height: 405px;
            overflow: auto;
            border: 1px dashed #ccc;
            padding: 6px;
            border-radius: 4px;
            background: #fafafa;
        }

        .item {
            padding: 8px;
            margin: 6px 0;
            background: #fff;
            border: 1px solid #bbb;
            border-radius: 4px;
            cursor: move;
        }

            .item.selected {
                outline: 2px solid #3399ff;
                background: #e9f3ff;
            }

        .drop-highlight {
            background: #e8ffe8 !important;
        }

        .suggest-item {
            padding: 6px;
            border-bottom: 1px solid #eee;
            cursor: pointer;
        }

        #mappingTable {
            width: 100%;
            border-collapse: collapse;
            margin-top: 12px;
        }

            #mappingTable th, #mappingTable td {
                border: 1px solid #ccc;
                padding: 6px;
                text-align: left;
            }

        .btn {
            padding: 6px 10px;
            border-radius: 4px;
            border: 1px solid #2d6;
            background: #2d8;
            cursor: pointer;
            display: inline-block;
            margin-right: 6px;
        }

        .btn1 {
            padding: 6px 10px;
            border-radius: 4px;
            border: 1px solid #2d6;
            background: #FFBF00;
            cursor: pointer;
            display: inline-block;
            margin-right: 6px;
        }

        .small {
            font-size: 0.9em;
            color: #555;
        }

        .search {
            width: 100%;
            padding: 6px;
            margin-bottom: 8px;
            box-sizing: border-box;
        }

        .suggested {
            background: #fff7cc !important;
            font-weight: 600;
        }

        .controls {
            margin-top: 8px;
        }

        .link {
            color: #007bff;
            cursor: pointer;
            text-decoration: underline;
        }
    </style>
    <style>
        .filter-row {
            display: flex;
            flex-wrap: nowrap;
            gap: 10px;
            align-items: center;
            width: 100%;
            overflow-x: auto;
            padding: 5px 0;
        }

        .filter-item {
            min-width: 200px; /* keeps nice layout */
        }

            .filter-item label {
                font-weight: bold;
                margin-bottom: 3px;
                display: block;
            }

        .selected {
            background-color: #d5ebff !important;
        }

        .suggested {
            border: 2px solid #007bff;
            background: #eaf4ff;
        }

        .match-green {
            background-color: #d4f5d2 !important; /* light green */
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="ScriptManager1" />

        <h2>Village Mapping </h2>
        <div class="filter-row">

            <div class="filter-item">
                <label>Year:</label>
                <asp:DropDownList ID="ddlYear" runat="server" onchange="bindMasterYear();" class="form-control"></asp:DropDownList>
            </div>

            <div class="filter-item">
                <label>State:</label>
                <asp:DropDownList ID="ddlState" runat="server"
                    onchange="Fill_District('ddlDistrict'); Fill_Block('ddlBlock'); Fill_Cluster('ddlGP');"
                    class="form-control">
                </asp:DropDownList>
            </div>

            <div class="filter-item">
                <label>District:</label>
                <asp:DropDownList ID="ddlDistrict" runat="server"
                    onchange="Fill_Block('ddlBlock');"
                    class="form-control">
                </asp:DropDownList>
            </div>

            <div class="filter-item">
                <label>Block:</label>
                <asp:DropDownList ID="ddlBlock" runat="server"
                    onchange="Fill_Cluster('ddlGP'); loadVillages(); bindClusterVillage('', '');"
                    class="form-control">
                </asp:DropDownList>
            </div>

            <div class="filter-item" style="display: none;">
                <label>Cluster:</label>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlGP" runat="server" class="form-control"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="filter-item" style="display: none;">
                <label>&nbsp;</label>
                <input type="button" id="myButton" class="btn btn-danger btn-block"
                    value="Search"
                    onclick="call_function('', ''); Get_Details();" />
            </div>

        </div>



        <div class="container">
            <!-- MIS list (left) -->
            <div class="panel">
                <div class="row">
                    <h3>MST Villages</h3>
                    <label>Status:</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" onchange="loadAll();" class="form-control">
                        <asp:ListItem Text="Unmapped" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Mapped" Value="1"></asp:ListItem>
                        
                    </asp:DropDownList>
                </div>
                <br />
                <input id="txtSearchMIS" class="search" style="display:none;" placeholder="Search MIS villages..." />
                <div id="misList" class="list"></div>
                <div class="controls">
                    <button type="button" onclick="saveVillages()" class="btn btn-primary">
                        Save
                    </button>
                    <%--<span class="small">Click a MIS to get suggested Layer villages. Drag a MIS onto a Layer item to map.</span>--%>
                </div>
            </div>

            <!-- Middle: suggestions & actions -->
            <div class="panel1">
                <h3>GIS Villages: Suggested Matches</h3>
                <input id="txtSearchSuggest" class="search" style="display: none;" placeholder="Type to find suggestions (or click MIS/Layer)..." />
                <div id="suggestList" class="list"></div>

                <div style="margin-top: 10px;">
                    <button id="btnSaveAll" type="button" class="btn">Save</button>
                    <%--<button id="btnRefresh" type="button" class="btn1">Un-Map  Village</button>--%>
                </div>

                <%--<h3 style="margin-top: 16px;">Instructions</h3>
                <ul>
                    <li>Click MIS or Layer to see suggestions on the opposite side.</li>
                    <li>Drag a MIS and drop onto a Layer item (or vice versa) to create mapping.</li>
                    <li>Edit or Unlink mappings from the table below.</li>
                </ul>--%>
            </div>

            <!-- Layer list (right) -->
            <div class="panel" style="display: none;">
                <h3>Layer Villages</h3>
                <input id="txtSearchLayer" class="search" placeholder="Search layer villages..." />
                <div id="layerList" class="list"></div>

                <h4 style="margin-top: 12px;">Mapped Pairs</h4>
                <table id="mappingTable">
                    <thead>
                        <tr>
                            <th>MIS Village</th>
                            <th>Layer Village</th>
                            <%--<th>CreatedOn</th>--%>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody id="mappingBody"></tbody>
                </table>
            </div>
        </div>
        <div id="map" style="height: 520px; margin-top: 12px; border: 1px solid #ddd; border-radius:6px;"></div>
        <!-- Hidden edit modal (simple prompt-like UI) -->
        <div id="editModal" title="Edit Mapping" style="display: none;">
            <div>
                <label>MIS Village</label><br />
                <select id="editMisSelect" style="width: 100%; padding: 6px;"></select>
            </div>
            <div style="margin-top: 8px;">
                <label>Layer Village</label><br />
                <select id="editLayerSelect" style="width: 100%; padding: 6px;"></select>
            </div>
        </div>
        


        <script type="text/javascript">

            $(document).ready(function () {

                bindMaster();
                initMap();
            });

            function bindMaster() {
                Fill_FYear("ddlYear");
                $('[id$=ddlYear]').val("2025");
                Fill_State("ddlState");
                var FYear = $("[id$=ddlYear] option:selected").text();
                var UserlevelRole = '<%= Session["user_level_Role"] %>';
                if (FYear == '2025-2026' && UserlevelRole == '1') {
                    $('[id$=ddlState]').val("9A");
                }
                //else {
                //    $('[id$=ddlState]').val("9");
                //}
                Fill_District("ddlDistrict");

                var distvalue = '<%= Session["DistrictCodeGIS"] %>';
                if (distvalue == '') {
                    if (FYear == '2025-2026') {
                        $('[id$=ddlDistrict]').val("17A9C3FD23A049BAB30ED17E9#26.2455#80.8294");
                    }
                    else {
                        $('[id$=ddlDistrict]').val("2EB646C9A3BA423EB9C8D49E8#25.3903#80.8913");
                    }
                }
                else {
                    $('[id$=ddlDistrict]').val(distvalue);
                }

                Fill_Block("ddlBlock");

                var Blockvalue = '<%= Session["BlockCodeGIS"] %>';
                var UserlevelRole = '<%= Session["user_level_Role"] %>';
                if (UserlevelRole == '4') {
                    $('[id$=ddlBlock]').val(Blockvalue);
                }
                else {
                    //$('[id$=ddlDistrict]').val(distvalue);
                }


                //$('[id$=ddlBlock]').val("2EB646C9A3BA423EB9C8D49E8");
                Fill_Cluster("ddlGP");
            }

            function bindMasterYear() {

                Fill_State("ddlState");
                var FYear = $("[id$=ddlYear] option:selected").text();
                var UserlevelRole = '<%= Session["user_level_Role"] %>';
                if (FYear == '2025-2026' && UserlevelRole == '1') {
                    $('[id$=ddlState]').val("9A");
                }
                //else {
                //    $('[id$=ddlState]').val("9");
                //}
                Fill_District("ddlDistrict");

                var distvalue =  '<%= Session["DistrictCodeGIS"] %>';
                if (distvalue == '') {
                    if (FYear == '2025-2026') {
                        $('[id$=ddlDistrict]').val("17A9C3FD23A049BAB30ED17E9#26.2455#80.8294");
                    }
                    else {
                        $('[id$=ddlDistrict]').val("2EB646C9A3BA423EB9C8D49E8#25.3903#80.8913");
                    }
                }
                else {
                    $('[id$=ddlDistrict]').val(distvalue);
                }

                Fill_Block("ddlBlock");


                var Blockvalue = '<%= Session["BlockCodeGIS"] %>';
                var UserlevelRole = '<%= Session["user_level_Role"] %>';
                if (UserlevelRole == '4') {
                    $('[id$=ddlBlock]').val(Blockvalue);
                }
                else {
                    //$('[id$=ddlDistrict]').val(distvalue);
                }



                //$('[id$=ddlBlock]').val("2EB646C9A3BA423EB9C8D49E8");
                Fill_Cluster("ddlGP");
                call_function('', '');
                Get_Details();
            }

            function Fill_FYear(ddlID) {

                var objvr = {};
                objvr.ValidID = "";

                _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_FYear", "", objvr, true);
            }
            function Fill_State(ddlID) {

                var objvr = {};
                var FYear = $("[id$=ddlYear]").val();
                objvr.ValidID = FYear;

                _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_State", "", objvr, true);
            }
            function Fill_District(ddlID) {
                var FYear = $("[id$=ddlYear] option:selected").text();
                var StateID = $("[id$=ddlState]").val();
                var objvr = {};
                objvr.ValidID = FYear;
                objvr.ValidID1 = StateID;

                _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_District", "Select", objvr, true);
            }
            function Fill_Block(ddlID) {
                var FYear = $("[id$=ddlYear] option:selected").text();
                var StateID = $("[id$=ddlState]").val();

                var d = $("[id$=ddlDistrict]").val();
                var did = "";
                var DistrictID = "";
                if (d.length > 10) {
                    did = $("[id$=ddlDistrict]").val().split("#");
                    DistrictID = did[0];
                }
                else {
                    DistrictID = d;
                }
                var objvr = {};
                objvr.ValidID = FYear;
                objvr.ValidID1 = StateID;
                objvr.ValidID2 = DistrictID;

                _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_Block", "All", objvr, true);
            }
            function Fill_Cluster(ddlID) {
                var FYear = $("[id$=ddlYear] option:selected").text();
                var StateID = $("[id$=ddlState]").val();


                var d = $("[id$=ddlDistrict]").val();
                var did = "";
                var DistrictID = "";
                if (d.length > 10) {
                    did = $("[id$=ddlDistrict]").val().split("#");
                    DistrictID = did[0];
                }
                else {
                    DistrictID = d;
                }

                var b = $("[id$=ddlBlock]").val();
                var bid = "";
                var BlockID = "";
                if (b.length > 10) {
                    bid = $("[id$=ddlBlock]").val().split("#");
                    BlockID = bid[0];
                }
                else {
                    BlockID = b;
                }

                var objvr = {};
                objvr.ValidID = FYear;
                objvr.ValidID1 = StateID;
                objvr.ValidID2 = DistrictID;
                objvr.ValidID3 = BlockID;

                _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_Cluster", "All", objvr, true);
            }
            // small helper for AJAX POST to page WebMethods
            function ajaxPost(url, data, success, error) {
                $.ajax({
                    url: url,
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(data || {}),
                    dataType: 'json',
                    success: function (d) { if (success) success(d.d); },
                    error: function (xhr) { if (error) error(xhr); }
                });
            }


            function onlyNumbers(e) {
                var c = e.which ? e.which : e.keyCode;
                return (c >= 48 && c <= 57); // only digits
            }

            function renderMis(list) {
                var c = $('#misList').empty();

                var table = $(`
        <table class="table table-hover table-bordered" id="misTable">
            <thead>
                <tr>
                    <th>Village Name</th>
                    <th>EG Village Code</th>
                    <th>Layer Village Code</th>
                </tr>
            </thead>
            <tbody></tbody>
        </table>
    `);

                var tbody = table.find("tbody");

                (list || []).forEach(function (v) {
                    tbody.append(`
            <tr class="mis-row"
                data-id="${v.VillageCode}"
                data-name="${v.VillageName}">
                <td>${v.VillageName}</td>
                <td>${v.VillageCode}</td>
                <td>
                    <input type="text"
                           class="form-control gis-code"
                           maxlength="10"
                           onkeypress="return onlyNumbers(event)" />
                </td>
            </tr>
        `);
                });

                c.append(table);

                $("#misTable").DataTable({
                    paging: true,
                    searching: true,
                    ordering: true,
                    pageLength: 10,     // default rows per page
                    lengthMenu: [5, 10, 20, 50, 100],
                    autoWidth: false
                });

                bindClick(); // attach your click event
            }
            
            function bindClick() {

                var fyear = $("[id$=ddlYear]").val();
                var district = $("[id$=ddlDistrict] option:selected").text();
                var block = $("[id$=ddlBlock] option:selected").text();

                


                $('#misList').off('click', '.mis-row').on('click', '.mis-row', function () {

                    $('#misList .mis-row').removeClass('selected');
                    $(this).addClass('selected');

                    var misName = $(this).data('name');
                    var egVillageCode = $(this).data('id');

                    var lat = $(this).data('lat');
                    var lon = $(this).data('lon');

                    sessionStorage.setItem('lat', lat);
                    var storedlat = sessionStorage.getItem('lat');

                    console.log("lat:", lat);

                    

                    sessionStorage.setItem('long', lon);
                    var storedlong = sessionStorage.getItem('lon');

                    console.log("long:", lon);

                    sessionStorage.setItem('misName', misName);
                    var storedmisName = sessionStorage.getItem('misName');

                    //var egVillageCode = $(this).find('.id').val().trim();

                    if (egVillageCode === "") egVillageCode = null;

                    console.log("Clicked MIS Name:", misName);
                    console.log("EG Village Code:", egVillageCode);

                    sessionStorage.setItem('egVillageCode', egVillageCode);
                    var storedEgVillageCode = sessionStorage.getItem('egVillageCode');
                    console.log(storedEgVillageCode); 

                    ajaxPost('VillageMapping.aspx/GetMappingVillages',
                        {
                            misName: misName,
                            egCode: egVillageCode,   
                            fyear: fyear,
                            district: district,
                            block: block
                        },
                        function (res) {
                            renderSuggest(res);
                            bindMappingSuggestions();
                            highlightSuggestedInLayer(res);
                        }
                    );
                });
            }

            function saveVillages() {
                var results = [];

                $("#misTable tbody tr").each(function () {

                    var VillageCode = $(this).find(".gis-code").val().trim();

                    // ❌ Skip empty rows
                    if (VillageCode === "") {
                        return; // continue loop, skip this row
                    }

                    // Validate numeric only for filled rows
                    if (!/^\d+$/.test(VillageCode)) {
                        alert("EG Village Code must be numeric!");
                        $(this).find(".eg-code").focus();
                        results = [];
                        return false;   // stop loop completely
                    }

                    // Push only rows with value
                    results.push({
                        egVillageCode: $(this).data("id"),
                        VillageName: $(this).data("name"),
                        VillageCode: VillageCode
                    });
                });

                console.log(results);

                // If nothing entered
                if (results.length === 0) {
                    alert("Please enter at least one EG Village Code.");
                    return;
                }

                // Send to server
             
                $.ajax({
                    url: "VillageMapping.aspx/SaveVillages",
                    type: "POST",
                    data: JSON.stringify({ villages: results }),
                    contentType: "application/json; charset=utf-8",
                    success: function (r) {
                        alert("Saved!");
                        loadAll();
                    }
                });
             
            }



            function renderLayer(list) {
                var c = $('#layerList').empty();
                (list || []).forEach(function (v) {
                    $('<div class="item layer-item" />')
                        .text(v.VillageName)
                        .attr('data-id', v.LayerVillageID)
                        .attr('data-name', v.VillageName)
                        .appendTo(c);
                });
                bindDrag();
            }



            function renderSuggest(list) {

                var container = $('#suggestList').empty();

                if (!list || list.length === 0) {
                    container.append('<div class="small">No suggestions</div>');
                    return;
                }

                var html = `
    <table id="suggestTable" class="display compact" style="width:100%">
        <thead>
            <tr>
                <th>Select</th>
                <th>SN</th>
                <th>VillageID</th>
                <th>Village</th>
                <th>District</th>
                <th>Block</th>
                <th>Distance (KM)</th>
                <th>EG VillageCode</th>
                <th>Match Score</th>
            </tr>
        </thead>
        <tbody>
    `;

                list.forEach(function (s) {

                    var greenClass = (s.EG_VillageCode && s.Flag == "1") ? "match-green" : "";

                    html += `
        <tr class="suggest-row ${greenClass}"
            data-layer-id="${s.VillageID}"
            data-eg-code="${s.EG_VillageCode || ''}"
        >
            <td>
                <input type="checkbox" class="row-check"
                       data-village-id="${s.VillageID}"
                       data-eg-code="${s.EG_VillageCode || ''}">
            </td>
            <td>${s.SlNo}</td>
            <td>${s.VillageID}</td>
            <td>${s.GISVillageName}</td>
            <td>${s.DistrictName}</td>
            <td>${s.BlockName}</td>
            <td>${s.DistanceKM}</td>
            <td>${s.EG_VillageCode}</td>
            <td>${Math.round(parseFloat(s.MatchScore))}%</td>
        </tr>
        `;
                });

                html += `</tbody></table>`;
                container.append(html);

                // Initialize DataTable
                $('#suggestTable').DataTable({
                    pageLength: 10,
                    ordering: true,
                    searching: true,
                    destroy: true,
                    lengthChange: false
                });

                // Row click highlight
                $('#suggestTable tbody').on('click', 'tr', function (e) {

                    // ignore if clicking checkbox
                    if ($(e.target).is('input[type=checkbox]')) return;

                    $('#suggestTable tr').removeClass('selected');
                    $(this).addClass('selected');

                    var layerId = $(this).data('layer-id');
                    highlightByTable(layerId);
                });
            }
            function getSelectedSuggestions() {
                var storedEgVillageCode = sessionStorage.getItem('egVillageCode');
                var selected = [];

                $('#suggestTable .row-check:checked').each(function () {
                    selected.push({
                        VillageID: $(this).data('village-id'),
                        //EGVillageCode: $(this).data('eg-code')
                        EGVillageCode: storedEgVillageCode
                    });
                });

                return selected;
            }
            $('#btnSaveAll').on('click', function () {

                var rows = getSelectedSuggestions();

                console.log(rows);   

                if (rows.length === 0) {
                    alert("No rows selected!");
                    return;
                }

                ajaxPost("VillageMapping.aspx/SaveVillageMappings",
                    { list: rows },
                    function (res) {
                        alert(res);
                        loadAll();
                    });

                

                //var msg = rows.map(r => `VillageID: ${r.VillageID}, EGCode: ${r.EGVillageCode}`).join("\n");

                //alert(msg);
            });

 
            function highlightByTable(layerId) {
                $('#layerList .item').removeClass('suggested');
                $('#layerList .item[data-id="' + layerId + '"]').addClass('suggested');
            }

            function highlightSuggestedInLayer(suggestions) {
                $('#layerList .item').removeClass('suggested');

                suggestions.forEach(s => {
                    $('#layerList .item[data-id="' + s.VillageID + '"]').addClass('suggested');
                });
            }

            function renderMappings(list) {
                var c = $('#mappingBody').empty();
                (list || []).forEach(function (m) {
                    var tr = $('<tr data-mapid="' + m.MapID + '"></tr>');
                    tr.append('<td>' + m.MISVillageName + '</td>');
                    tr.append('<td>' + m.LayerVillageName + '</td>');
                    //tr.append('<td>' + (new Date(m.CreatedOn)).toLocaleString() + '</td>');
                    tr.append('<td><span class="link edit" data-mapid="' + m.MapID + '">Edit</span> | <span class="link delete" data-mapid="' + m.MapID + '">Unlink</span></td>');
                    c.append(tr);
                });
            }

            // global lists cached for edit selects
            var MIS_CACHE = [], LAYER_CACHE = [];

            function bindDrag() {
                $('.item').draggable({
                    helper: 'clone',
                    revert: 'invalid',
                    start: function (e, ui) {
                        $(ui.helper).css('z-index', 1000);
                    }
                });

                $('.list').droppable({
                    accept: '.item, .suggest-item',
                    tolerance: 'pointer',
                    over: function (event, ui) {
                        $(this).addClass('drop-highlight');
                    },
                    out: function (event, ui) {
                        $(this).removeClass('drop-highlight');
                    },
                    drop: function (event, ui) {
                        $(this).removeClass('drop-highlight');

                        var srcIsSuggest = ui.draggable.hasClass('suggest-item');
                        var droppedOnList = $(this).attr('id'); // misList, layerList, suggestList
                        var isDropOnLayerList = droppedOnList === 'layerList';
                        var isDropOnMISList = droppedOnList === 'misList';

                        // get dragged data
                        var dragged = ui.draggable;
                        var misId = null, misName = null, layerId = null, layerName = null;

                        if (srcIsSuggest) {
                            // suggestion contains layer info; find MIS under mouse
                            layerId = dragged.data('layer-id');
                            layerName = dragged.data('layer-name');
                            // find MIS under mouse position (if any)
                            var tgtMIS = getItemUnderMouse('#misList', event.pageX, event.pageY);
                            if (!tgtMIS) {
                                // fallback to first MIS
                                tgtMIS = $('#misList .item').first();
                            }
                            misId = tgtMIS.data('id');
                            misName = tgtMIS.data('name');
                        } else {
                            // normal item from misList or layerList
                            if (dragged.closest('#misList').length) {
                                misId = dragged.data('id');
                                misName = dragged.data('name');
                                // we dropped onto a layer item: find the specific layer item under mouse
                                var tgtLayer = getItemUnderMouse('#layerList', event.pageX, event.pageY);
                                if (!tgtLayer) { alert('Drop onto a specific Layer village to map.'); return; }
                                layerId = tgtLayer.data('id');
                                layerName = tgtLayer.data('name');
                            } else if (dragged.closest('#layerList').length) {
                                layerId = dragged.data('id');
                                layerName = dragged.data('name');
                                var tgtMIS = getItemUnderMouse('#misList', event.pageX, event.pageY);
                                if (!tgtMIS) { alert('Drop onto a specific MIS village to map.'); return; }
                                misId = tgtMIS.data('id');
                                misName = tgtMIS.data('name');
                            } else {
                                return;
                            }
                        }

                        if (!misId || !layerId) { console.warn('missing ids', misId, layerId); return; }

                        // Save mapping (AJAX)
                        ajaxPost('VillageMapping.aspx/SaveMapping', { misVillageId: misId, layerVillageId: layerId }, function (res) {
                            if (res && res.Success) {
                                addMappingRow(res.Mapping);
                            } else {
                                alert(res.Message || 'Mapping saved or already exists.');
                                refreshMappings();
                            }
                        }, function () { alert('Error saving mapping.'); });
                    }
                });
            }

            function getItemUnderMouse(listSelector, x, y) {
                var found = null;
                $(listSelector + ' .item').each(function () {
                    var rect = this.getBoundingClientRect();
                    var left = rect.left + window.scrollX;
                    var top = rect.top + window.scrollY;
                    var right = left + rect.width;
                    var bottom = top + rect.height;
                    if (x >= left && x <= right && y >= top && y <= bottom) {
                        found = $(this);
                        return false;
                    }
                });
                return found;
            }

            function addMappingRow(m) {
                // append mapping if not present
                if ($('#mappingBody tr[data-mapid="' + m.MapID + '"]').length) return;
                var tr = $('<tr data-mapid="' + m.MapID + '"></tr>');
                tr.append('<td>' + m.MISVillageName + '</td>');
                tr.append('<td>' + m.LayerVillageName + '</td>');
                //tr.append('<td>' + (new Date(m.CreatedOn)).toLocaleString() + '</td>');
                tr.append('<td><span class="link edit" data-mapid="' + m.MapID + '">Edit</span> | <span class="link delete" data-mapid="' + m.MapID + '">Unlink</span></td>');
                $('#mappingBody').prepend(tr);
            }
            function loadVillages() {
                loadAll();
            }
            function loadAll() {

                var did = $("[id$=ddlDistrict]").val().split("#");
                var district = did[0];

                var bid = $("[id$=ddlBlock]").val().split("#");
                var block = bid[0];

                var filters = {
                    query: null,   // must include this if your WebMethod expects it
                    year: $('#<%= ddlYear.ClientID %>').val(),
                    state: $('#<%= ddlState.ClientID %>').val(),
                    district: district,
                    block: block,
                    status: $('#<%= ddlStatus.ClientID %>').val(),
                };

                ajaxPost('VillageMapping.aspx/GetMISVillages', filters, function (res) {
                    MIS_CACHE = res || [];
                    renderMis(res);
                    //populateEditSelects();
                    //updateMapFromCaches();
                });

                //ajaxPost('VillageMapping.aspx/GetLayerVillages', filters, function (res) {
                //    LAYER_CACHE = res || [];
                //    renderLayer(res);
                //    //populateEditSelects();
                //    //updateMapFromCaches();
                //});

                //ajaxPost('VillageMapping.aspx/GetMappings', filters, function (res) {
                //    renderMappings(res);
                //});
            }



            function refreshMappings() {
                ajaxPost('VillageMapping.aspx/GetMappings', {}, function (res) { renderMappings(res); });
            }

            // Populate selects used in edit modal
            function populateEditSelects() {
                var misSel = $('#editMisSelect').empty();
                MIS_CACHE.forEach(function (m) { misSel.append('<option value="' + m.MISVillageID + '">' + m.VillageName + '</option>'); });

                var layerSel = $('#editLayerSelect').empty();
                LAYER_CACHE.forEach(function (l) { layerSel.append('<option value="' + l.LayerVillageID + '">' + l.VillageName + '</option>'); });
            }

            // events
            $(function () {
                loadAll();
                // searches
                $('#txtSearchMIS').on('input', function () {
                    var q = $(this).val();

                    var did = $("[id$=ddlDistrict]").val().split("#");
                    var district = did[0];

                    var bid = $("[id$=ddlBlock]").val().split("#");
                    var block = bid[0];

                    var filters = {
                        query: q,   // must include this if your WebMethod expects it
                        year: $('#<%= ddlYear.ClientID %>').val(),
                        state: $('#<%= ddlState.ClientID %>').val(),
                        district: district,
                        block: block
                    };

                    ajaxPost('VillageMapping.aspx/GetMISVillages', filters, function (res) { MIS_CACHE = res || []; renderMis(res); });
                });
                $('#txtSearchLayer').on('input', function () {
                    var q = $(this).val();

                    var did = $("[id$=ddlDistrict]").val().split("#");
                    var district = did[0];

                    var bid = $("[id$=ddlBlock]").val().split("#");
                    var block = bid[0];

                    var filters = {
                        query: q,   // must include this if your WebMethod expects it
                        year: $('#<%= ddlYear.ClientID %>').val(),
                        state: $('#<%= ddlState.ClientID %>').val(),
                        district: district,
                        block: block
                    };

                    ajaxPost('VillageMapping.aspx/GetLayerVillages', filters, function (res) { LAYER_CACHE = res || []; renderLayer(res); });
                });

                // suggestions typed
                $('#txtSearchSuggest').on('input', function () {
                    var q = $(this).val();
                    if (!q || q.length < 2) { $('#suggestList').empty(); return; }
                    // default: treat input as MIS name suggestions for Layers
                    ajaxPost('VillageMapping.aspx/GetSuggestionsForMIS', { misName: q, topN: 15 }, function (res) { renderSuggest(res); });
                });

                function onMISClick(misName) {

                    var year = $("#ddlYear").val();
                    var state = $("#ddlState").val();

                    var did = $("[id$=ddlDistrict]").val().split("#");
                    var district = did[0];

                    var bid = $("[id$=ddlBlock]").val().split("#");
                    var block = bid[0];

                    ajaxPost('VillageMapping.aspx/GetSuggestionsForMIS', {
                        misName: misName,
                        year: year,
                        state: state,
                        district: district,
                        block: block
                    }, function (res) {
                        renderSuggestions(res);
                    });
                }
                function onLayerClick(layerName) {

                    var year = $("#ddlYear").val();
                    var state = $("#ddlState").val();

                    var did = $("[id$=ddlDistrict]").val().split("#");
                    var district = did[0];

                    var bid = $("[id$=ddlBlock]").val().split("#");
                    var block = bid[0];

                    ajaxPost('VillageMapping.aspx/GetSuggestionsForLayer', {
                        layerName: layerName,
                        year: year,
                        state: state,
                        district: district,
                        block: block
                    }, function (res) {
                        renderSuggestions(res);
                    });
                }


   
                $('#layerList').on('click', '.item', function () {
                    $('#layerList .item').removeClass('selected');
                    $(this).addClass('selected');
                    var name = $(this).data('name');
                    ajaxPost('VillageMapping.aspx/GetSuggestionsForLayer', { layerName: name, topN: 20 }, function (res) {
                        // we render suggestions as MIS items in suggestList - reuse renderSuggest semantics by swapping fields
                        var mapped = (res || []).map(function (s) { return { LayerVillage: { LayerVillageID: s.MISVillage.MISVillageID, VillageName: s.MISVillage.VillageName }, Score: s.Score }; });
                        // but highlight MIS on left based on suggestions
                        highlightSuggestedInMIS(res);
                        // show suggestion list (we'll show as MIS suggestions)
                        var c = $('#suggestList').empty();
                        (res || []).forEach(function (s) {
                            $('<div class="suggest-item"/>').html('<strong>' + s.MISVillage.VillageName + '</strong> <span class="small">(' + Math.round(s.Score * 100) + '%)</span>')
                                .data('mis-id', s.MISVillage.MISVillageID)
                                .data('mis-name', s.MISVillage.VillageName)
                                .appendTo(c);
                        });
                    });
                });

                // clicking a suggestion (when suggestions are layer-via-mis) -> map to selected MIS
                $('#suggestList').on('click', '.suggest-item', function () {
                    var sid = $(this).data('layer-id') || $(this).data('mis-id');
                    var sName = $(this).data('layer-name') || $(this).data('mis-name');

                    // if this suggestion has layer-id, it's a Layer suggestion; map to selected MIS
                    var selectedMIS = $('#misList .item.selected');
                    if (selectedMIS.length && $(this).data('layer-id')) {
                        var misId = selectedMIS.data('id');
                        var layerId = $(this).data('layer-id');
                        ajaxPost('VillageMapping.aspx/SaveMapping', { misVillageId: misId, layerVillageId: layerId }, function (res) {
                            if (res && res.Success) addMappingRow(res.Mapping);
                            else refreshMappings();
                        });
                        return;
                    }

                    // if suggestion is MIS (clicked from layer->mis suggestions), and a layer is selected, map
                    var selectedLayer = $('#layerList .item.selected');
                    if (selectedLayer.length && $(this).data('mis-id')) {
                        var layerId = selectedLayer.data('id');
                        var misId = $(this).data('mis-id');
                        ajaxPost('VillageMapping.aspx/SaveMapping', { misVillageId: misId, layerVillageId: layerId }, function (res) {
                            if (res && res.Success) addMappingRow(res.Mapping);
                            else refreshMappings();
                        });
                        return;
                    }

                    alert('Select an item on the opposite list (click a MIS or Layer) before using suggestion to map.');
                });

                // unlink
                $('#mappingBody').on('click', '.delete', function () {
                    var id = $(this).data('mapid');
                    if (!confirm('Unlink mapping?')) return;
                    ajaxPost('VillageMapping.aspx/DeleteMapping', { mapId: id }, function (res) {
                        if (res && res.Success) refreshMappings();
                    });
                });

                // edit
                $('#mappingBody').on('click', '.edit', function () {
                    var mapId = $(this).data('mapid');
                    // load mapping details to prefill
                    var row = $('#mappingBody tr[data-mapid="' + mapId + '"]');
                    var misName = row.children().eq(0).text();
                    var layerName = row.children().eq(1).text();

                    // populate selects already done in loadAll/populateEditSelects
                    $('#editMisSelect').val(MIS_CACHE.find(m => m.VillageName === misName)?.MISVillageID || '');
                    $('#editLayerSelect').val(LAYER_CACHE.find(l => l.VillageName === layerName)?.LayerVillageID || '');

                    $('#editModal').data('mapid', mapId).dialog({
                        modal: true,
                        width: 520,
                        buttons: {
                            "Save": function () {
                                var newMis = $('#editMisSelect').val();
                                var newLayer = $('#editLayerSelect').val();
                                ajaxPost('VillageMapping.aspx/UpdateMapping', { mapId: mapId, newMISId: parseInt(newMis), newLayerId: parseInt(newLayer) }, function (res) {
                                    if (res && res.Success) {
                                        refreshMappings();
                                        $('#editModal').dialog('close');
                                    } else {
                                        alert('Update failed');
                                    }
                                });
                            },
                            "Cancel": function () { $(this).dialog('close'); }
                        }
                    });
                });

                //$('#btnRefresh').click(function () { loadAll(); });
                //$('#btnSaveAll').click(function () { alert('Mappings are saved per action. Use Refresh to reload.'); });
            });
        </script>

        <script type="text/javascript">

            var map;
            var StateMap = L.layerGroup();
            var District_Map = L.layerGroup();
            var BlockMap = L.layerGroup();
            var VillageMap = L.layerGroup();
            function initMap() {
                if (map) {
                    map.remove();  
                }

                // default view - will be fitted later to district / markers
                map = L.map('map', { preferCanvas: true }).setView([25.3903, 80.8913], 4.5);

               StreetLyr= L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                    maxZoom: 19,
                    attribution: '&copy; OpenStreetMap contributors'
                })

                map.setZoom(9);
                var layerControl;
                var overlayMaps = {};
                
                var BaseUrls = 'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1Ijoia3dyaXNhY2l3cm0iLCJhIjoiY2xma3p3NmpoMDBhaTNwbnV1NnVkMGp2ZCJ9.1ASKnwxbjSZxZGiXn0xl4Q';
                var mbAttr = "";

                // Define spinner options
                var spinnerOptions = {
                    lines: 8, // The number of lines to draw
                    length: 5, // The length of each line
                    width: 4, // The line thickness
                    radius: 10, // The radius of the inner circle
                    color: '#000', // Spinner color
                    speed: 1, // Rounds per second
                    trail: 60, // Afterglow percentage
                    shadow: true // Whether to render a shadow
                };

                // Create a spinner instance
                var spinner = new Spinner(spinnerOptions);

                function initializeBaseLayers() {
                    //var mbAttr = "";
                    //var BaseUrls = 'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1Ijoia3dyaXNhY2l3cm0iLCJhIjoiY2xma3p3NmpoMDBhaTNwbnV1NnVkMGp2ZCJ9.1ASKnwxbjSZxZGiXn0xl4Q';

                    function BaseLyrOptionsM(ids) {
                        return {
                            maxZoom: 18,
                            attribution: mbAttr,
                            id: ids,
                            tileSize: 512,
                            zoomOffset: -1
                        };
                    }

                    // Initialize the layers
                    GrayLyr = L.tileLayer(BaseUrls, BaseLyrOptionsM('mapbox/light-v9'));
                    StreetLyr = StreetLyr;
                    Terrain = L.tileLayer(BaseUrls, BaseLyrOptionsM('mapbox/outdoors-v11')).addTo(map);
                    ImageryLyr = L.esri.basemapLayer('Imagery');

                    function BaseLyrOptionsM(ids) {
                        return {
                            maxZoom: 18,
                            attribution: mbAttr,
                            id: ids,
                            tileSize: 512,
                            zoomOffset: -1
                        };
                    }

                    var BaseLyrOptions = {
                        maxZoom: 18,
                        subdomains: ['mt0', 'mt1', 'mt2', 'mt3'],
                        foo: 'bar',
                        fillOpacity: 0.1,
                        zIndex: -1
                    };
                }



                


               
                

                function addLayerToControl() {
                    const overlayMaps = {
                        "Gray": GrayLyr,
                        "Street": StreetLyr,
                        "Terrain": Terrain,
                        "Satellite": ImageryLyr
                        //"State": StateMap,
                        //"District": District_Map,
                        //"Block": BlockMap
                        //"Cluster": VillageMap
                    };

                    if (!window.layerControl) {
                        window.layerControl = L.control.layers(null, overlayMaps).addTo(map);
                    }
                }

                initializeBaseLayers();
                //bindStateLyr();
                //bindDistrict();
                //bindBlock('', '');
               
                addLayerToControl();
            }

            function bindStateLyr() {



                map.spin(true, spinnerOptions);
                var SateJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3ASTATE_BOUNDARY&maxFeatures=5000&outputFormat=application%2Fjson';
                fetch(SateJSONURL)
                    .then(response => response.json())
                    .then(data => {
                        StateMap = new L.geoJson(data, { style: PLVSatestyle });
                        //StateMap.addTo(map);
                        map.spin(false);
                    })
                    .catch(error => {
                        console.error('Error fetching GeoJSON data2:', error);
                    });

                function PLVSatestyle(feature) {
                    return {
                        fillColor: '#eeeee4',
                        weight: 2,
                        opacity: 0.5,
                        color: 'blue',
                        //dashArray: '3',
                        fillOpacity: 0
                    };
                }
            }


            function bindDistrict() {
                District_Map = "";
                var Fyear = $("[id$=ddlYear]").val();
                var d = $("[id$=ddlDistrict]").val();
                var did = "";
                var DistrictID = "";
                if (d.length > 10) {
                    did = $("[id$=ddlDistrict]").val().split("#");
                    DistrictID = did[0];
                }
                else {
                    DistrictID = d;
                }
                var DistrictJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_District_Layer_View&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';DistrictID:' + DistrictID + '';

                fetch(DistrictJSONURL)
                    .then(response => response.json())
                    .then(data => {

                        // Remove the existing district layer if it exists
                        if (District_Map) {
                            map.removeLayer(District_Map);
                        }

                        // Create a new district layer and add it to the map
                        District_Map = L.geoJson(data, { style: PLVDistrictstyle });
                        District_Map.addTo(map);

                        //District_Map = new L.geoJson(data, { style: PLVDistrictstyle });
                        //District_Map.addTo(map);

                    })
                    .catch(error => {
                        console.error('Error fetching GeoJSON data:', error);
                    });

                function PLVDistrictstyle(feature) {
                    return {
                        fillColor: '#FFFFFF',
                        weight: 2,
                        opacity: 0.5,
                        color: 'black',
                        //dashArray: '3',
                        fillOpacity: 0.1
                    };
                }
            }

            function bindBlock(flag, locationid) {
                debugger;
                var _gridid = "";
                var BlockJSONURL = "";

                var Fyear = $("[id$=ddlYear]").val();
                var _statecode = $("[id$=ddlState]").val();
                var d = $("[id$=ddlDistrict]").val();
                var did = "";
                var _districtcode = "";
                if (d.length > 10) {
                    did = $("[id$=ddlDistrict]").val().split("#");
                    _districtcode = did[0];
                }
                else {
                    _districtcode = d;
                }


                var b = $("[id$=ddlBlock]").val();
                var bid = "";
                var _blockcode = "";
                if (b.length > 10) {
                    bid = $("[id$=ddlBlock]").val().split("#");
                    _blockcode = bid[0];
                }
                else {
                    _blockcode = b;
                }

                var _grididblock = flag;
                var _locid = locationid;
                var b = _locid.split("#");
                var _locguidBlock = b[0];

                if (_blockcode == "" || _blockcode == null) {
                    if (_grididblock == "blockclick") {
                        BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Block_Lyr_View_Filter&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _locguidBlock + '';
                    } else {
                        BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Block_Lyr_View_New&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + '';
                    }
                }
                else {
                    BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Block_Lyr_View_Filter&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + '';
                }
                if (BlockMap) {
                    map.removeLayer(BlockMap); // Remove the existing layer
                }
                //BlockMap = L.layerGroup();
                fetch(BlockJSONURL)
                    .then(response => response.json())
                    .then(data => {
                        // Create a GeoJSON layer and add it to the map
                        BlockMap = new L.geoJson(data, {
                            style: PLVBlockstyle,
                            onEachFeature: onEachFeatureBlock
                        });
                        if (_gridid == "blockclick" || _blockcode == "" || _blockcode == null) {
                            BlockMap.addTo(map);
                            //map.spin(false);
                        }
                        //BlockMap = new L.geoJson(data, { style: PLVBlockstyle });
                        //BlockMap.addTo(map);
                    })
                    .catch(error => {
                        console.error('Error fetching GeoJSON data:', error);
                    });

                function PLVBlockstyle(feature) {
                    return {

                        fillColor: feature.properties.ColorCode,
                        weight: 2,
                        opacity: 1,
                        color: 'black',
                        dashArray: '3',
                        fillOpacity: 0.4
                    };
                }

                function onEachFeatureBlock(feature, layer) {
                    layer.bindTooltip("<b style='color: #2954A2;font-size: 12px;'>Block: " + feature.properties.BlockName + "</b>",
                        {
                            //direction: 'right',
                            permanent: false,
                            sticky: true,
                            offset: [10, 0],
                            opacity: 2,

                            //className: 'leaflet-tooltip-own'
                        });

                    layer.on({
                        mouseover: highlightFeatureCluster,
                        mouseout: resetHighlightBlock,
                        preclick: resetStyleBlock,
                        click: zoomToFeatureCluster
                    });
                }
                function resetHighlightBlock(e) {
                    BlockMap.resetStyle(e.target);
                }
                function resetStyleBlock(e) {
                    BlockMap.resetStyle(e.target);
                }
                function highlightFeatureCluster(e) {
                    var layer = e.target;
                    layer.setStyle({
                        weight: 4,
                        color: '#666',
                        dashArray: '',
                        fillOpacity: 0.4
                        //fillColor: '',

                    });
                }
                function zoomToFeatureCluster(e) {
                    map.fitBounds(e.target.getBounds());
                }
            }

            function bindClusterVillage(flag, locationid) {
                debugger;
                click = 0;
                var VlgClusterJSONURL = "";
                VillageMap.clearLayers();

                var _statecode = $("[id$=ddlState]").val();
                var d = $("[id$=ddlDistrict]").val();
                var did = "";
                var _districtcode = "";
                if (d.length > 10) {
                    did = $("[id$=ddlDistrict]").val().split("#");
                    _districtcode = did[0];
                }
                else {
                    _districtcode = d;
                }


                var b = $("[id$=ddlBlock]").val();
                var bid = "";
                var _blockcode = "";
                if (b.length > 10) {
                    bid = $("[id$=ddlBlock]").val().split("#");
                    _blockcode = bid[0];
                   
                }
                else {
                    _blockcode = b;
                }

                var district = $("[id$=ddlDistrict] option:selected").text();
                var block = $("[id$=ddlBlock] option:selected").text();

                var vstatus = 0;//$("[id$=ddlVillageStatus]").val();

                var c = $("[id$=ddlGP]").val();
                var cid = "";
                var _clusterid = "";
                if (c.length > 10) {
                    cid = $("[id$=ddlGP]").val().split("#");
                    _clusterid = cid[0];
                }
                else {
                    _clusterid = c;
                }
                _clusterid = _clusterid.replace(/-/g, '');
                var _gridid = flag;
                var _locid = locationid;
                //var b = _locid.split("#");
                //var _locguid = b[0];
                var b = "";
                var _locguid = "";
                var Fyear = $("[id$=ddlYear]").val();
                //_clusterid = _clusterid.replace(/-/g, '');

                //_locguid = _locguid.replace(/-/g, '');



                if (_gridid == "villageclick") {
                    b = _locid.split("#");
                    //_clusterid = b[0];
                    _locguid = b[0];
                    _clusterid = _clusterid.replace(/-/g, '');
                    _locguid = _locguid.replace(/-/g, '');
                }
                else {
                    b = _locid.split("#");
                    _locguid = b[0];
                    _locguid = _locguid.replace(/-/g, '');
                }

                if (_clusterid == "" || _clusterid == null) {
                    if (_gridid == "clusterclick") {
                        VlgClusterJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_Cluster_View_Filter_FY&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _locguid + ';vstatus:' + vstatus;
                    } else {
                        if (_blockcode == "" || _blockcode == null) {
                            VlgClusterJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_Cluster_FY&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';vstatus:' + vstatus;
                        } else {
                            VlgClusterJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EGTest/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EGTest%3Alyr_VillageMapping&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';DistrictName:' + district;
                        }
                    }
                } else {
                    if (_gridid == "villageclick") {
                        VlgClusterJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_Cluster_View_Filter_Village_FY&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid + ';vil:' + _locguid + ';vstatus:' + vstatus;
                    } else {
                        VlgClusterJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_Cluster_View_Filter_FY&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid + ';vstatus:' + vstatus;
                    }
                }

                resetVillageLayer();

                /////////////////////////Cluster//////////////
                // Fetch GeoJSON data using fetch API
                //VillageMap = L.layerGroup();
                fetch(VlgClusterJSONURL)
                    .then(response => response.json())
                    .then(data => {
                        // Create a GeoJSON layer and add it to the map
                        VillageMap = new L.geoJson(data, {
                            style: PLVstyleCluster,
                            onEachFeature: onEachFeatureCluster
                        });
                        if (_gridid == "clusterclick" || _gridid == "villageclick") {
                            click = 1;
                            VillageMap.addTo(map);
                        }
                        else if (_blockcode != "") {

                            VillageMap.addTo(map);
                        }
                        else if (vstatus != "0") {
                            if (BlockMap) {
                                map.removeLayer(BlockMap); // Remove the block map layer if it exists
                            }
                            VillageMap.addTo(map);
                        }
                        // Attach the layer to the layer control
                        addLayerToControl();
                    })
                    .catch(error => {
                        console.error('Error fetching GeoJSON data:', error);
                    });

                // Function to add the layer to the layer control
                function addLayerToControl() {
                    const overlayMaps = {
                        "Gray": GrayLyr,
                        "Street": StreetLyr,
                        "Terrain": Terrain,
                        "Satellite": ImageryLyr,
                        "State": StateMap,
                        "District": District_Map,
                        "Block": BlockMap,
                        "Cluster": VillageMap
                    };

                    if (!window.layerControl) {
                        window.layerControl = L.control.layers(null, overlayMaps).addTo(map);
                    }
                }

                function resetVillageLayer() {
                    if (VillageMap) {
                        map.removeLayer(VillageMap); // Remove the existing layer
                    }
                }
                function PLVstyleCluster(feature) {
                    return {

                        //fillColor: getColorCluster(feature.properties.ClusterCode),
                        fillColor: feature.properties.ColorCode,
                        weight: 2,
                        opacity: 1,
                        color: 'black',
                        dashArray: '3',
                        fillOpacity: 0.4
                    };
                }

                function getColorCluster(d) {
                    return d == "4A67BB68F1804CD78C1CB791D" ? '#800000' :
                        d == "4FF3210281EA458EA007BDA57" ? '#9A6324' :
                            d == "7564F272DAAA444AB09D112EE" ? '#808000' :
                                d == "7962F0F36ABF47F4991809272" ? '#000075' :
                                    d == "8632DD0BF68D466AB40103D0C" ? '#e6194B' :
                                        d == "9C61588ABFC24D58BAB798127" ? '#f58231' :
                                            d == "AA722DC830104BD38F782E526" ? '#ffe119' :
                                                d == "F289C3AF20DA404CBC7F4C149" ? '#f032e6' :
                                                    d == "FCB91B1A401F453992785C5D1" ? '#42d4f4' :
                                                        '#017f7e';
                }

                function getColorClusterNew(d) {
                    const colors = ['#bfff00', '#800000', '#9A6324', '#808000', '#000075', '#e6194B', '#f58231', '#ffe119', '#f032e6', '#42d4f4'];

                    // Ensure d is within the range of colors array
                    const index = parseInt(d);
                    if (index >= 0 && index < colors.length) {
                        return colors[index];
                    } else {
                        // If d is out of range, return a default color
                        return '#017f7e';
                    }
                }

                function onEachFeatureCluster(feature, layer) {
                    layer.bindTooltip("<b style='color: #2954A2;font-size: 12px;'>Cluster: " + feature.properties.ClusterName + "<br/> Village: " + feature.properties.VillageName + "<br/> Village Operational Status: " + feature.properties.OperationalStatus + "</b>",
                        {
                            //direction: 'right',
                            permanent: false,
                            sticky: true,
                            offset: [10, 0],
                            opacity: 3,
                            //className: 'leaflet-tooltip-own'
                        });

                    layer.on({
                        mouseover: highlightFeatureCluster,
                        mouseout: resetHighlightCluster,
                        preclick: resetStyleCluster,
                        click: zoomToFeatureCluster
                    });
                }

                function highlightFeatureCluster(e) {
                    var layer = e.target;
                    layer.setStyle({
                        weight: 2,
                        color: '#666',
                        dashArray: '',
                        opacity: 1,
                        fillOpacity: 0.4
                        //fillColor: '',



                    });
                }

                function resetHighlightCluster(e) {
                    VillageMap.resetStyle(e.target);
                }
                function resetStyleCluster(e) {
                    VillageMap.resetStyle(e.target);
                }
                function zoomToFeatureCluster(e) {
                    map.fitBounds(e.target.getBounds());
                }


                function PLVstyle(feature) {
                    return {
                        fillColor: '#eeeee4',
                        weight: 2,
                        opacity: 0.5,
                        color: 'black',
                        dashArray: '3',
                        fillOpacity: 0
                    };
                }

                function getColor(d) {
                    return d > 75 ? '#03b5fc' :
                        d > 50 ? '#80fc03' :
                            d > 25 ? '#fc8c03' :
                                d > 0 ? '#fc0303' :
                                    '#c4c4c4';
                }


                function highlightFeature(e) {
                    var layer = e.target;

                    layer.setStyle({
                        weight: 5,
                        color: '#666',
                        dashArray: '',
                        fillOpacity: 0.7
                    });

                    layer.bringToFront();
                    info.update(layer.feature.properties);
                }

                function resetHighlight(e) {
                    VillageMap.resetStyle(e.target);
                    info.update();
                }

                function zoomToFeature(e) {
                    map.fitBounds(e.target.getBounds());
                }

                function onEachFeature(feature, layer) {
                    layer.on({
                        mouseover: highlightFeature,
                        mouseout: resetHighlight,
                        click: zoomToFeature
                    });
                }
            }


            function bindMappingSuggestions() {
                debugger;
                var _gridid = "";
                var vlgJSONURL = "";

                var fyear = $("[id$=ddlYear]").val();
                var district = $("[id$=ddlDistrict] option:selected").text();
                var block = $("[id$=ddlBlock] option:selected").text();
                var storedEgVillageCode = sessionStorage.getItem('egVillageCode');
                var storedmisName = sessionStorage.getItem('misName');

                var storedlat = sessionStorage.getItem('lat');
                var storedlong = sessionStorage.getItem('lon');

                BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EGTest/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EGTest%3Alyr_Village_Mapping_Suggestions&maxFeatures=50&outputFormat=application%2Fjson&viewparams=villagename:' + storedmisName + ';egvillagecode:' + storedEgVillageCode + ';fyear:' + fyear + ';districtname:' + district +';blockname:'+block+'';
                
                if (BlockMap) {
                    map.removeLayer(BlockMap); // Remove the existing layer
                }
                //BlockMap = L.layerGroup();
                fetch(BlockJSONURL)
                    .then(response => response.json())
                    .then(data => {
                        // Create a GeoJSON layer and add it to the map
                        BlockMap = new L.geoJson(data, {
                            style: vlgstyle,
                            onEachFeature: onEachFeaturevlg
                        });
                        BlockMap.addTo(map);
                        
                        //if (_gridid == "blockclick" || _blockcode == "" || _blockcode == null) {
                        //    BlockMap.addTo(map);
                        //    //map.spin(false);
                        //}
                        //BlockMap = new L.geoJson(data, { style: PLVBlockstyle });
                        //BlockMap.addTo(map);
                    })
                    .catch(error => {
                        console.error('Error fetching GeoJSON data:', error);
                    });

                L.marker([storedlat, storedlong]).addTo(map)
                    .bindPopup("Your Location")
                    .openPopup();

                function vlgstyle(feature) {
                    return {
                        weight: 2,
                        opacity: 1,
                        color: '#769cf5',
                        //dashArray: '3',
                        fillOpacity: 0.7,
                        fillColor: CircleColors(feature.properties.MatchScore)
                    };
                }

                //function PLVBlockstyle(feature) {
                //    return {

                //        fillColor: feature.properties.MatchScore,
                //        weight: 2,
                //        opacity: 1,
                //        color: 'black',
                //        dashArray: '3',
                //        fillOpacity: 0.4
                //    };
                //}
                function CircleColors(e) {
                    return (e >= 100 ? '#008000' : e >= 80 && e <= 90 ? '#0000FF' : e >= 70 && e < 80 ? '#FFFF00' : e >= 50 && e < 70 ? '#FFA500' : e < 50 ? '#FF0000' : e = null ? '#FFFFFF' : '"#FFFFFF"')

                }
                function onEachFeaturevlg(feature, layer) {
                    layer.bindTooltip("<b style='color: #2954A2;font-size: 12px;'>Village: " + feature.properties.GISVillageName + "<br/> Block: " + feature.properties.BlockName + "<br/> District: " + feature.properties.DistrictName + "</b>",
                        {

                            //direction: 'right',
                            permanent: false,
                            sticky: true,
                            offset: [10, 0],
                            opacity: 2,

                            //className: 'leaflet-tooltip-own'
                        });

                    layer.on({
                        mouseover: highlightFeatureCluster,
                        mouseout: resetHighlightBlock,
                        preclick: resetStyleBlock,
                        click: zoomToFeatureCluster
                    });
                }
                function resetHighlightBlock(e) {
                    BlockMap.resetStyle(e.target);
                }
                function resetStyleBlock(e) {
                    BlockMap.resetStyle(e.target);
                }
                function highlightFeatureCluster(e) {
                    var layer = e.target;
                    layer.setStyle({
                        weight: 4,
                        color: '#666',
                        dashArray: '',
                        fillOpacity: 0.4
                        //fillColor: '',

                    });
                }
                function zoomToFeatureCluster(e) {
                    map.fitBounds(e.target.getBounds());
                }
            }
        </script>

    </form>
</body>
</html>
