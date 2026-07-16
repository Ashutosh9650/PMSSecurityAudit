<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Culture="en-GB" CodeFile="GISHM.aspx.cs" Inherits="GISHM" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="Scripts/jquery-3.6.0.min.js"></script>
    <script src="leaflet2/leaflet.js" type="text/javascript"></script>
    <link href="leaflet2/leaflet.css" rel="stylesheet" type="text/css" />
    <link href="leaflet2/leaflet.fullscreen.css" rel="stylesheet" type="text/css" />
    <script src="leaflet2/Leaflet.fullscreen.js" type="text/javascript"></script>
    <script src="leaflet2/leaflet.zoomhome.min.js" type="text/javascript"></script>
    <link href="Leaflet2/LeafletClustersMarkers/MarkerCluster.Default.css" rel="stylesheet" />
    <link href="Leaflet2/LeafletClustersMarkers/MarkerCluster.css" rel="stylesheet" />

    <script type="text/javascript" src="Leaflet2/LeafletClustersMarkers/leaflet.markercluster.js"></script>
    <script src="Scripts/comman.js" type="text/javascript"></script>
    <%--     <script src="https://cdn.datatables.net/1.10.24/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.24/js/dataTables.bootstrap4.min.js"></script>--%>
    <%--<script src="https://cdn.jsdelivr.net/npm/leaflet-easybutton@2/src/easy-button.js"></script>--%>
    <script type="text/javascript" src="js/JS/easy-button.js"></script>

    <%--<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/leaflet-easybutton@2/src/easy-button.css">--%>
    <link rel="stylesheet" href="css/CSS/easy-button.css">
    <script src="Leaflet2/bundle.js"></script>

    <script src="Leaflet2/leaflet.groupedlayercontrol.min.js"></script>

    <script src="Leaflet2/leaflet.spin.min.js" charset="utf-8"></script>
    <script src="Leaflet2/L.Control.Locate.js"></script>
    <script src="Leaflet2/leaflet-search.js"></script>
    <link href="Leaflet2/leaflet-search.css" rel="stylesheet" type="text/css" />


    <%--<link type="text/css" href="https://cdn.datatables.net/1.13.7/css/dataTables.bootstrap.min.css">--%>
    <link type="text/css" href="css/CSS/dataTables.bootstrap.min.css">

    <%--<link type="text/css" href="https://cdn.datatables.net/fixedheader/3.4.0/css/fixedHeader.bootstrap.min.css">--%>
    <link type="text/css" href="css/CSS/fixedHeader.bootstrap.min.css">

    <%--<script type="text/javascript" src="https://code.jquery.com/jquery-3.7.0.js"></script>--%>
    <script type="text/javascript" src="js/JS/jquery-3.7.0.js"></script>

    <script type="text/javascript" src="Scripts/jquery.dataTables.min.js"></script>
    <%--<script src="https://cdn.datatables.net/1.13.7/js/jquery.dataTables.min.js"></script>--%>
    <%--<script type="text/javascript" src="https://cdn.datatables.net/1.13.7/js/dataTables.bootstrap.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/dataTables.bootstrap.min.js"></script>
    <%--<script type="text/javascript" src="https://cdn.datatables.net/fixedheader/3.4.0/js/dataTables.fixedHeader.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/dataTables.fixedHeader.min.js"></script>

    <!-- Esri Leaflet CSS and JS -->
    <%--<link rel="stylesheet" href="https://unpkg.com/esri-leaflet-geocoder/dist/esri-leaflet-geocoder.css" />--%>
    <link rel="stylesheet" href="css/CSS/esri-leaflet-geocoder.css" />

    <%--<script src="https://unpkg.com/esri-leaflet/dist/esri-leaflet.js"></script>--%>
    <script type="text/javascript" src="js/JS/esri-leaflet.js"></script>
        <!-- map Loader -->
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/spin.js/2.3.2/spin.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/spin.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#search_box').show(500);
            $('.show-1-yes').hide(0);
            $('.hide-1-yes').show(0);

            $(".zoom_div").click(function () {
                $(".grid-2").toggleClass("grid-2_zoom")
                $(".asd").toggleClass("grid-2_hide")
                $(this).find('i').toggleClass('fa fa-expand fa-lg fa fa-compress fa-lg')
            });
            $('.show-1-yes').click(function () {
                $('#search_box').show(500);
                $('.show-1-yes').hide(0);
                $('.hide-1-yes').show(0);
            });
            $('.hide-1-yes').click(function () {
                $('#search_box').hide(500);
                $('.show-1-yes').show(0);
                $('.hide-1-yes').hide(0);
            });
        })
    </script>

    <style type="text/css">
        #map {
            min-height: 387px;
            /*min-height: calc(100vh - 212px);*/
            width: 100%;
            /*top: 96px !important;*/
            /*height: 450px;*/
        }

        #myButton {
            background-image: url('images/search-29.png');
            background-color: transparent; /* Adjust as needed */
            width: 30px;
            height: 30px; /* Set the height of the button */
            border: none; /* Remove the default button border */
            cursor: pointer;
        }
        .mandatory-label::after {
            content: "*";
            color: red;
            margin-left: 4px; /* Adjust spacing as needed */
        }

        .legendCSS {
            /* min-height:200px;
            height:495px;
            width:450px;
            overflow:scroll;*/
            text-align: right;
            min-height: 100px;
            max-height: 495px;
            min-width: 100px;
            max-width: 350px;
            overflow: auto;
        }

        .leg {
            background-color: #fff;
            color: #333;
            text-align: center;
            border: 0px solid #ddd;
            border-bottom-width: 0px;
            border-bottom-style: solid;
            border-bottom-color: rgb(221, 221, 221);
            border-radius: 4px;
            position: absolute !important;
            z-index: 800;
            box-shadow: 1px 1px 2px #6D6D6D;
            border-bottom: 1px solid #ccc;
            font-size: 20px;
            padding: 0px;
            right: 23px;
            top: 16px;
        }


        .squarered {
            width: 25px;
            height: 25px;
            background-color: #f50000;
            display: inline-block;
        }

        .squaregreen {
            width: 25px;
            height: 25px;
            background-color: #ffff00;
            display: inline-block;
        }

        .squarepurple {
            width: 25px;
            height: 25px;
            background-color: #00ff00;
            display: inline-block;
        }

        #leaflet-slider {
            margin-bottom: 30px !important;
        }

        .slider:before {
            background-color: transparent !important;
        }

        .my-label {
            position: absolute;
            width: 1000px;
            font-size: 20px;
        }

        .leaflet-control-layers-base label, .leaflet-control-layers-overlays label {
            display: flex;
        }

            .leaflet-control-layers-base label input[type=radio], .leaflet-control-layers-overlays label input[type=checkbox] {
                margin: -1px 9px 0px 0px !important;
            }

        .leaflet-control-zoom.leaflet-bar.leaflet-control {
            display: none;
        }


        .info {
            padding: 6px 8px;
            font: 14px/16px Arial, Helvetica, sans-serif;
            background: white;
            background: rgba(255,255,255,0.8);
            box-shadow: 0 0 15px rgba(0,0,0,0.2);
            border-radius: 5px;
        }

            .info h4 {
                margin: 0 0 5px;
                color: #777;
            }

        .legend {
            line-height: 18px;
            color: #555;
        }

            .legend i {
                width: 18px;
                height: 18px;
                float: left;
                margin-right: 8px;
                opacity: 0.7;
            }

        .leaflet-control-attribution a {
            display: none
        }

        .leaflet-control-layers-overlays label:nth-child(4) {
            border-bottom: 1px solid red
        }

        #container-target {
        }

        ::-webkit-scrollbar {
            width: 10px;
            height: 10px;
        }

        ::-webkit-scrollbar-track {
            -webkit-box-shadow: inset 0 0 6px red;
            -webkit-border-radius: 10px;
            border-radius: 10px;
        }

        ::-webkit-scrollbar-thumb {
            -webkit-border-radius: 10px;
            border-radius: 10px;
            background: #fff8f8;
            -webkit-box-shadow: inset 0 0 6px #6D6D6D;
        }

            ::-webkit-scrollbar-thumb:window-inactive {
                background: #333;
            }

        #search_box {
            background: transparent;
            width: 100%;
            height: auto;
            display: none;
        }

        #tblLocDetails_wrapper .row:nth-child(2) .col-sm-12 {
            padding-left: 0px;
            padding-right: 0px;
        }

        .hide {
            display: none;
        }

        /*  added this - not working  */

        .hide-1-yes {
            display: none;
        }

        a.leaflet-control-zoomhome-home, a.leaflet-bar-part.leaflet-bar-part-single {
            display: flex;
            justify-content: center;
            align-items: center;
        }

        div#tblLocDetails_filter {
            text-align: end;
        }

        .search-bg {
            background: linear-gradient(to bottom, #ebf1fd 0%,#ffffff 100%) !important;
            /*background-color: rgb(241, 241, 241)!important;*/
            padding-top: 7px !important;
            border: 1px solid rgb(221, 221, 221) !important;
            border-top-left-radius: 4px !important;
            border-top-right-radius: 4px !important;
            margin-bottom: 0px !important;
        }

        .common-header {
            min-width: 150px;
        }

        .common-cell {
            min-width: 150px;
        }

        #tblLocDetails_wrapper {
            position: relative;
            z-index: 1;
        }

            #tblLocDetails_wrapper .row:nth-child(1) {
                display: none;
            }

            #tblLocDetails_wrapper .row:nth-child(1) {
                width: 50%;
                left: 50%;
                position: relative;
                z-index: 5;
                transform: translateX(-50%);
            }

            #tblLocDetails_wrapper .row:nth-child(2) {
                width: 100%;
                margin: 0px 0px;
            }

            #tblLocDetails_wrapper .row .col-sm-6 {
                width: 100%;
                text-align: center;
            }
        /*====================================================*/
        #MapSummary table thead {
            width: calc(100% - 10px) !important;
        }

        #MapSummary table {
            background: linear-gradient(to bottom, #ffe5e6 0%,#fff8f8 100%);
        }

            #MapSummary table thead::-webkit-scrollbar {
                width: 10px;
                height: 10px;
            }

            #MapSummary table thead tr th:nth-last-child(1) {
                border-right: 0px;
            }

            #MapSummary table thead::-webkit-scrollbar-track {
                -webkit-box-shadow: inset 0 0 6px red;
                -webkit-border-radius: 10px;
                border-radius: 10px;
            }

            #MapSummary table thead::-webkit-scrollbar-thumb {
                -webkit-border-radius: 10px;
                border-radius: 10px;
                background: var(--scroll-bg-2);
                -webkit-box-shadow: inset 0 0 6px red;
            }

                #MapSummary table thead::-webkit-scrollbar-thumb:window-inactive {
                    background: blue;
                }

            /*======================================*/

            #MapSummary table tbody::-webkit-scrollbar {
                width: 10px;
                height: 10px;
            }

            #MapSummary table tbody::-webkit-scrollbar-track {
                -webkit-box-shadow: inset 0 0 6px red;
                -webkit-border-radius: 10px;
                border-radius: 10px;
            }

            #MapSummary table tbody::-webkit-scrollbar-thumb {
                -webkit-border-radius: 10px;
                border-radius: 10px;
                background: #fff8f8;
                -webkit-box-shadow: inset 0 0 6px #6D6D6D;
            }

                #MapSummary table tbody::-webkit-scrollbar-thumb:window-inactive {
                    background: #333;
                }


            #MapSummary table tbody {
                display: block;
                height: 260px;
                overflow: auto;
            }

            #MapSummary table thead, tbody tr {
                display: table;
                width: 100%;
                table-layout: fixed;
            }

                #MapSummary table thead tr th {
                    width: 80px !important;
                    background: linear-gradient(to bottom, #ffe5e6 0%,#fff8f8 100%);
                }

        table#tblLocDetails {
            margin: 0px;
        }

        #MapSummary table tbody tr td {
            width: 80px !important
        }

        #MapSummary table thead tr th:nth-last-child(1) {
            width: 200px !important;
        }

        #MapSummary table tbody tr td:nth-last-child(1) {
            width: 200px !important
        }

        #MapSummary table thead tr th:nth-child(1) {
            width: 120px !important
        }

        #MapSummary table tbody tr td:nth-child(1) {
            width: 120px !important
        }

        #MapSummary table tbody tr td, #MapSummary table thead tr th {
            vertical-align: middle;
            text-align: center
        }

        .inner-section {
            background: #fbfbfb;
        }

        @media (min-width:991px) and (max-width:1134px) {
            #MapSummary table thead tr th:nth-last-child(1) {
                width: 150px !important;
            }

            #MapSummary table tbody tr td:nth-last-child(1) {
                width: 150px !important
            }

            #MapSummary table thead tr th:nth-child(1) {
                width: 100px !important
            }

            #MapSummary table tbody tr td:nth-child(1) {
                width: 100px !important
            }
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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="updpnlgis">
        <ContentTemplate>
            <script type="text/javascript">
                    //setTimeout(function () {

                    //    $("#tblLocDetails_filter").clone().appendTo("#asdp");
                    //}, 2500);
                    (function ($) {
                        'use strict';

                        $(document).ready(function () {
                            $('.table-filter').on('input', function () {
                                var value = $(this).val().toLowerCase();

                                $('.filtered-table tbody tr').filter(function () {
                                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);

                                });

                            });

                        });
                    })(window.jQuery);


                $(document).ready(function () {
                    bindMaster();
                    getmap('', '');
                    Get_Details();
                    //setTimeout(logMe, 500);
                });

                var logMe = function () {
                    $('#tblLocDetails').DataTable({
                        paging: false,
                        searching: false,
                        "bDestroy": true
                    });
                };

                function showloader() {
                    $(".update_overlay").show();
                }

                function hideloader() {
                    setTimeout(function () {
                        $(".update_overlay").hide();
                    }, 4000);

                }

                //setTimeout(function () {
                //    $('#tblLocDetails').dataTable();
                //}, 1000);
                function bindMaster() {
                    Fill_FYear("ddlYear");
                    $('[id$=ddlYear]').val("2026");
                    Fill_State("ddlState");
                    var FYear = $("[id$=ddlYear] option:selected").text();
                    var UserlevelRole = '<%= Session["user_level_Role"] %>';
                    if (FYear == '2026-2027' && UserlevelRole== '1') {
                        $('[id$=ddlState]').val("23");
                    }
                    //else {
                    //    $('[id$=ddlState]').val("9");
                    //}
                    Fill_District("ddlDistrict");
                    var distvalue = '<%= Session["DistrictCodeGIS2026"] %>';
                    if (distvalue == '') {
                        if (FYear == '2026-2027') {
                            $('[id$=ddlDistrict]').val("715EA2AFF7CE4AF080AF7CD81#22.6094#74.5233");
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
                    Fill_GroupBy("ddlGroupby");
                }
                function bindMasterYear() {
                    

                    Fill_State("ddlState");
                    var FYear = $("[id$=ddlYear] option:selected").text();
                    var UserlevelRole = '<%= Session["user_level_Role"] %>';
                    if (FYear == '2026-2027' && UserlevelRole=='1') {
                        $('[id$=ddlState]').val("23");
                    }
                    //else {
                    //    $('[id$=ddlState]').val("9");
                    //}
                    Fill_District("ddlDistrict");
                   
                    var distvalue = '<%= Session["DistrictCodeGIS2026"] %>';
                    if (distvalue == '') {
                        if (FYear == '2026-2027') {
                            $('[id$=ddlDistrict]').val("715EA2AFF7CE4AF080AF7CD81#22.6094#74.5233");
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
                    //Get_Details();
                }
                function Go_to_Location(locid, th) {
                    ZoomToLatLong_Click(locid);
                }
                function BindLabel() {
                    var lbltext = $("[id$=ddlReportType] option:selected").text();
                    $("[id$=lbl_Rtype]").html("Report: " + lbltext);

                }
                function Fill_FYear(ddlID) {

                    var objvr = {};
                    objvr.ValidID = "";

                    _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_FYear_NextFY", "", objvr, true);
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

                    _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_District2026", "Select", objvr, true);
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

                    _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_Block2026", "All", objvr, true);
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

                    _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_Cluster_cluster2025", "All", objvr, true);
                }
                function Fill_GroupBy(ddlID) {


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

                    var c = $("[id$=ddlGP]").val();
                    var cid = "";
                    var ClusterID = "";
                    if (c.length > 10) {
                        cid = $("[id$=ddlGP]").val().split("#");
                        ClusterID = cid[0];
                    }
                    else {
                        ClusterID = c;
                    }
                    var objvr = {};
                    objvr.ValidID = BlockID;
                    objvr.ValidID1 = ClusterID;

                    _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_GroupBy", "", objvr, true);
                }
                function Get_Details() {
                    $(".update_overlay").show();
                    try {
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

                        var c = $("[id$=ddlGP]").val();
                        var cid = "";
                        var ClusterID = "";
                        if (c.length > 10) {
                            cid = $("[id$=ddlGP]").val().split("#");
                            ClusterID = cid[0];
                        }
                        else {
                            ClusterID = c;
                        }
                        var Groupby = $("[id$=ddlGroupby]").val();
                        var Reporttype = $("[id$=ddlReportType]").val();
                        var FYearID = $("[id$=ddlYear]").val();
                        if (DistrictID == "" || DistrictID == null) {
                            Show_ModalAlert("Please Select District !!");
                            return;
                        }


                        var objvr = {};
                        objvr.ValidID = FYear;
                        objvr.ValidID1 = StateID;
                        objvr.ValidID2 = DistrictID;
                        objvr.ValidID3 = BlockID;
                        objvr.ValidID4 = ClusterID;
                        objvr.ValidID5 = Groupby;
                        objvr.ValidID6 = Reporttype;
                        objvr.ValidID7 = FYearID;
                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            url: 'GISHM.aspx/Get_MapDetails',
                            data: JSON.stringify(objvr),
                            success: function (response) {
                                $("#MapSummary").html(response.d);
                                ZoomToLatLong();
                                BindLabel();
                                $(".update_overlay").hide();
                            },
                            error: function () {
                                $(".update_overlay").hide();
                                Show_ModalAlert("Please try again !!");
                                return false;
                            }
                        });
                    }


                    catch (e) {
                        $(".update_overlay").hide();
                        Show_ModalAlert("Please try again !!");
                    }

                    setTimeout(logMe, 1000);
                }
                function gotolatlong(value) {
                    debugger;
                    var a = value.split("#");

                    //var Lat = a[1];
                    //var Long = a[2];
                    var zoomlevel = 9;
                    if (state != "" && dis == "") {

                    }
                    else if (dis != "" && blk == "") {
                        zoomlevel = 9;
                        var initPosition = [a[1], a[2]];
                        map.setView(initPosition, zoomlevel);
                    }
                    else if (blk != "" && gp == "") {
                        zoomlevel = 10;
                        var initPosition = [a[1], a[2]];
                        map.setView(initPosition, zoomlevel);
                    }
                    else if (gp != "" && blk != "") {
                        zoomlevel = 10;
                        var initPosition = [a[1], a[2]];
                        map.setView(initPosition, zoomlevel);
                    }
                    else {

                    }

                }
                function ZoomToLatLong() {
                    // 
                    state = $("[id$=ddlState]").val();
                    dis = $("[id$=ddlDistrict]").val();
                    blk = $("[id$=ddlBlock]").val();
                    gp = $("[id$=ddlGP]").val();
                    groupby = $("[id$=ddlGroupby]").val();
                    setTimeout(function () {
                        if (state != "" && dis == "") {
                            map.removeLayer(BlockMap);
                            map.removeLayer(HeatMap);
                            if (state == "9" || state == "9A" || state == "9B" || state == "9C") {
                                var initPosition = [25.3903, 80.8913];
                                map.setView(initPosition, 9);
                            }
                            if (state == "23") {
                                var initPosition = [23.065833940118736, 74.62120056152345];
                                map.setView(initPosition, 9);
                            }
                        }
                        else if (dis != "" && blk == "") {
                            if (groupby == "2" || groupby == "3") {
                                alert("Please Select Block !!");
                            }
                            else {
                                map.addLayer(BlockMap);
                                map.removeLayer(HeatMap);
                                gotolatlong(dis);
                            }
                        }
                        else if (blk != "" && gp == "") {
                            if (groupby == "2" || groupby == "3") {
                                map.removeLayer(BlockMap);
                                map.addLayer(HeatMap);
                                gotolatlong(blk);
                            }
                            else {
                               
                                map.removeLayer(BlockMap);
                                map.addLayer(HeatMap);
                                gotolatlong(dis);
                            }
                        }
                        else if (gp != "" && blk != "") {
                            
                            map.removeLayer(BlockMap);
                            map.addLayer(HeatMap);
                            gotolatlong(gp);
                            
                        }
                        else {
                            var initPosition = [23.473324, 77.947998];
                            map.setView(initPosition, 4.5);
                            map.removeLayer(BlockMap);
                            map.removeLayer(HeatMap);
                        }
                        //if (state == "9" || state == "9A" || state == "9B" || state == "9C") {
                        //    var initPosition = [25.082797868, 81.053105818];
                        //    map.setView(initPosition, 9);
                        //}
                        //if (state == "23") {
                        //    var initPosition = [23.065833940118736, 74.62120056152345];
                        //    map.setView(initPosition, 9);
                        //}
                    }, 1000);

                }

                function showSearch() {
                    $('#map-search').show();
                }
                function toggleSearchType() {
                    

                    var searchType = $("[id$=ddlSearchType]").val();
                    if (searchType == "1") {
                        $('.school-type').show();
                        $('.village-status').hide();
                        $('.location').hide();
                    }
                    if (searchType == "2") {
                        $('.village-status').show();
                        $('.school-type').hide();
                        $('.location').hide();
                    }
                    if (searchType == "3") {
                        $('.location').show();
                        $('.school-type').hide();
                        $('.village-status').hide();
                    }


                }

                function addLayers() {
                    debugger;
                    $('#map-search').hide();

                    state = $("[id$=ddlState]").val();
                    dis = $("[id$=ddlDistrict]").val();
                    blk = $("[id$=ddlBlock]").val();
                    gp = $("[id$=ddlGP]").val();
                    groupby = $("[id$=ddlGroupby]").val();
                    var v_status = $("[id$=ddlVillageStatus]").val();
                    setTimeout(function () {
                        if (state != "" && dis == "") {
                            map.removeLayer(BlockMap);
                            map.removeLayer(HeatMap);
                        }
                        else if (dis != "" && blk == "") {
                            if (groupby == "2" || groupby == "3") {
                                alert("Please Select Block !!");
                            }
                            else {
                                if (v_status != "0") {
                                    map.removeLayer(BlockMap);
                                    map.addLayer(HeatMap);
                                }
                                else {
                                    map.addLayer(BlockMap);
                                    map.removeLayer(HeatMap);
                                }
                               
                            }
                        }
                        else if (blk != "" && gp == "") {
                            if (groupby == "2" || groupby == "3") {
                                map.removeLayer(BlockMap);
                                map.addLayer(HeatMap);
                            }
                            else {
                                map.removeLayer(BlockMap);
                                map.addLayer(HeatMap);
                            }
                        }
                        else if (gp != "" && blk != "") {
                            map.removeLayer(BlockMap);
                            map.addLayer(HeatMap);
                        }
                        else {
                            var initPosition = [23.473324, 77.947998];
                            map.setView(initPosition, 4.5);
                            map.removeLayer(BlockMap);
                            map.removeLayer(HeatMap);
                        }

                    }, 1000);

                }


                function ZoomToLatLong_Click(loc) {
                    debugger;
                    state = $("[id$=ddlState]").val();
                    dis = $("[id$=ddlDistrict]").val();
                    blk = $("[id$=ddlBlock]").val();
                    gp = $("[id$=ddlGP]").val();
                    groupby = $("[id$=ddlGroupby]").val();
                    if (state != "" && dis == "") {
                        if (state == "9" || state == "9A" || state == "9B" || state == "9C") {
                            var initPosition = [25.082797868, 81.053105818];
                            map.setView(initPosition, 10);
                        }
                        if (state == "23") {
                            var initPosition = [23.065833940118736, 74.62120056152345];
                            map.setView(initPosition, 10);
                        }
                        //map.addLayer(StateMap);
                        map.removeLayer(BlockMap);
                        map.removeLayer(HeatMap);
                    }
                    else if (dis != "" && blk == "") {
                        if (groupby == "2" || groupby == "3") {
                            var a = loc.split("#");

                            var Lat = a[1];
                            var Long = a[2];

                            var initPosition = [a[1], a[2]];
                            map.setView(initPosition, 12);
                            map.removeLayer(BlockMap);
                            map.addLayer(HeatMap);
                        }
                        else {
                            if (loc == "24480A1B18FA40019CA46AEF6") {
                                var initPosition = [23.065833940118736, 74.62120056152345];
                                map.setView(initPosition, 10);
                                map.addLayer(BlockMap);
                                map.removeLayer(HeatMap);
                            }
                            else if (loc == "4CAB33CBCEF74D88AB553E86C") {
                                var initPosition = [25.082797868, 81.053105818];
                                map.setView(initPosition, 10);
                                map.addLayer(BlockMap);
                                map.removeLayer(HeatMap);
                            }
                            else {
                                var initPosition = [25.082797868, 81.053105818];
                                map.setView(initPosition, 10);
                                map.removeLayer(BlockMap);
                                map.removeLayer(HeatMap);
                            }
                        }
                    }
                    else if (blk != "" && gp == "") {

                        if (groupby == "2" || groupby == "3") {
                            var a = loc.split("#");

                            var Lat = a[1];
                            var Long = a[2];

                            var initPosition = [a[1], a[2]];
                            map.setView(initPosition, 12);
                            map.removeLayer(BlockMap);
                            map.addLayer(HeatMap);
                        }
                        else {



                            //var a = loc.split("#");

                            //var Lat = a[1];
                            //var Long = a[2];

                            //var initPosition = [a[1], a[2]];
                            //map.setView(initPosition, 11);


                            if (blk == "24480A1B18FA40019CA46AEF6") {
                                var initPosition = [23.065833940118736, 74.62120056152345];
                                map.setView(initPosition, 10);
                                map.removeLayer(BlockMap);
                                map.addLayer(HeatMap);
                            }
                            else if (blk == "4CAB33CBCEF74D88AB553E86C") {
                                var initPosition = [25.082797868, 81.053105818];
                                map.setView(initPosition, 10);
                                map.removeLayer(BlockMap);
                                map.addLayer(HeatMap);
                            }
                            else {
                                var initPosition = [25.082797868, 81.053105818];
                                map.setView(initPosition, 10);
                                map.removeLayer(BlockMap);
                                map.removeLayer(HeatMap);
                            }
                        }
                    }
                    else if (gp != "" && blk != "") {
                        var a = loc.split("#");

                        var Lat = a[1];
                        var Long = a[2];

                        var initPosition = [a[1], a[2]];
                        map.setView(initPosition, 12);
                        //if (blk == "24480A1B18FA40019CA46AEF6") {
                        //    var initPosition = [23.065833940118736, 74.62120056152345];
                        //    map.setView(initPosition, 12);
                        //}
                        //if (blk == "4CAB33CBCEF74D88AB553E86C") {
                        //    var initPosition = [25.3003467, 81.0217817];
                        //    map.setView(initPosition, 12);
                        //}
                        map.removeLayer(BlockMap);
                        map.addLayer(HeatMap);
                        //var a = $("[id$=ddlGP]").val().split("#");

                        //var Lat = a[1];
                        //var Long = a[2];

                        //var initPosition = [a[1], a[2]];
                        //map.setView(initPosition, 11);
                    }
                    else {
                        var initPosition = [23.473324, 77.947998];
                        map.setView(initPosition, 4.5);
                        map.removeLayer(BlockMap);
                        map.removeLayer(HeatMap);
                    }

                }
            </script>
            <script type="text/javascript">
                function Show_ModalAlert(msg) {
                    $('[id*="lbl_messages"]').text(msg);
                    $find("ModalAlertA").show();
                }
            </script>
            <div class="update_overlay">
                <div class="update_div">
                    <img src="images/progress2.gif" />
                </div>
            </div>
            <div class="container-fluid" style="margin-top: 0px;">

                <div class="row" style="margin-top: 0px;">
                    <div class="col-sm-12">
                        <div class="panel panel-default" style="background: linear-gradient(to bottom,  #ffffff 1%,#ffffff 1%,#ebf1fd 100%) !important; margin-bottom: 8px;">
                            <div class="panel-heading" style="background-color: transparent;">

                                <div class="row" style="margin-left: -15px; margin-right: -15px">
                                    <div class="col-sm-12" style="padding: 0px">
                                        <div class="dis-flex" style="padding: 0px">
                                            <h3 class="text-danger1" style="margin: 0px;">
                                                <asp:Label ID="lblMain" runat="server" Text="Heat Map" Style="margin: 3px 0px 5px 5px; font-weight: bold; font-size: medium;"></asp:Label>
                                            </h3>
                                            <button type="button" class="show-1-yes">
                                                <i class="fa fa-caret-square-o-down text-danger"></i>
                                            </button>
                                            <button type="button" class="hide-1-yes">
                                                <i class="fa fa-caret-square-o-up text-danger"></i>
                                            </button>
                                        </div>

                                    </div>

                                </div>

                            </div>
                            <div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div id="search_box">
                            <div class="panel panel-default" style="margin-bottom: 8px;">
                                <div class="panel-body" style="padding-top: 0px; padding-bottom: 0px;">
                                    <div class="row" style="margin: 0px -15px;">


                                        <div class="col-lg-12  search-bg">
                                            <div id="container-target">

                                                <div class="form-horizontal">
                                                    <div class="row marg" style="margin-left: -15px; margin-right: -15px">
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="ddlYear" class="col-sm-3  linhei" style="padding-top: 2px; font-weight: bold !important;">
                                                                    Year:<span class="mandatory-label"></span>
                                                                </label>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList ID="ddlYear" onchange="bindMasterYear();" runat="server" class="form-control ">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="ddlState" class="col-sm-3  linhei" style="padding-top: 2px; font-weight: bold !important;">State:<span class="mandatory-label"></span></label>
                                                                <div class="col-sm-9 ">
                                                                    <asp:DropDownList ID="ddlState" runat="server" onchange="Fill_District('ddlDistrict');Fill_Block('ddlBlock');Fill_Cluster('ddlGP');" class="form-control ">
                                                                    </asp:DropDownList>
                                                                    <%--<asp:Label ID="lblNumNo" Visible="false" runat="server" Text="Label"></asp:Label>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="ddlDistrict" class="col-sm-3 linhei" style="padding-top: 2px; font-weight: bold !important;">District:<span class="mandatory-label"></span></label>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList ID="ddlDistrict" runat="server" onchange="Fill_Block('ddlBlock');" class="form-control " />

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="ddlBlock" class="col-sm-3  linhei" style="padding-top: 2px; font-weight: bold !important;">Block:</label>
                                                                <div class="col-sm-9 ">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlBlock" runat="server" onchange="Fill_Cluster('ddlGP');Fill_GroupBy('ddlGroupby');" class="form-control" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin-left: -15px; margin-right: -15px">



                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="ddlGP" class="col-sm-3  linhei" style="padding-top: 2px; font-weight: bold !important;">Cluster:</label>
                                                                <div class="col-sm-9 ">
                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlGP" runat="server" class="form-control" onchange="Fill_GroupBy('ddlGroupby');" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="ddlGroupby" class="col-sm-3  linhei" style="padding-top: 2px; padding-right: 0px; font-weight: bold !important;">GroupBy:</label>
                                                                <div class="col-sm-9 ">
                                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlGroupby" runat="server" class="form-control">
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-4 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="ddlReportType" class="col-sm-2  linhei" style="padding-top: 2px; font-weight: bold !important;">Report:</label>
                                                                <div class="col-sm-9" style="padding-left: 25px;">
                                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlReportType" runat="server" class="form-control">
                                                                                <asp:ListItem Text="Quality Enrolment" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="FC Run Rate" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Quality of D2D Contact" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="Enrolment CV Error Rate" Value="4"></asp:ListItem>
                                                                                <asp:ListItem Text="D2D Contact" Value="5"></asp:ListItem>
                                                                                <asp:ListItem Text="RTE" Value="6"></asp:ListItem>
                                                                                <asp:ListItem Text="RTE with Document" Value="8"></asp:ListItem>
                                                                                <asp:ListItem Text="RTE without Document" Value="9"></asp:ListItem>
                                                                                <asp:ListItem Text="%GKP Average Attendance per Session" Value="7"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <input type="button" id="myButton" class="btn btn-danger btn-paddd" style="margin-left: -5rem;" onclick="getmap('','');Get_Details();" />
                                                                    <%--<asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-right" OnClientClick="getmap();" OnClick="btnSerach_Click" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />--%>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>


                                                </div>





                                            </div>



                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12">

                        <div class="grid-2">
                            <div class="bg-white panel panel-default">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div class="panel-heading" style="padding-left: 0px; padding-right: 0px; background-color: white; position: relative;">
                                            <div class="dis-flex">
                                                <asp:Label ID="lbl_Rtype" runat="server" Style="margin: 3px 0px 5px 5px; font-weight: bold;"></asp:Label>
                                                <%--<h4 style="margin: 3px 0px 5px 0px;"></h4>--%>

                                                <div>
                                                    <div class="row">
                                                        <%--<label class="col-sm-4" style="line-height: 25px;">Search:</label>--%>
                                                        <div class="col-sm-12">
                                                            <input type="search" class="form-control table-filter" placeholder="Search..." />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div>

                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text="" CssClass="btn btn-link btn-sm" OnClick="LinkButton1_Click"
                                                        class="pull-right1"><i class="fa fa-file-excel-o" aria-hidden="true"></i> Export</asp:LinkButton>

                                                    <%--<button type="button" class="zoom_div" style="padding: 0px 0px 0px 12px;background-color:white;border:none;">
                                                <i class="fa fa-expand fa-lg text-danger"></i>
                                            </button>--%>
                                                </div>
                                            </div>

                                            <div id="MapSummary" style="overflow: auto; height: 100%; width: 100%; margin-top: 6px">
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="asd" style="height: 400px; overflow: auto;">
                                <%--MAP--%>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <div class="panel panel-default">
                                            <div id='GISAnalyticals'>
                                            </div>
                                            <%--<div id="map">
                                                 </div>--%>
                                               <div class="map-search-wrp">

                                                    <div class="form-map-on">
                                                        <div class="row form-map-on-rwo">
                                                            <div class="col">
                                                                <div class="form-group">
                                                                    <%--<label for="SearchType" class="linhei">Search Type:  </label>--%>
                                                                    <asp:DropDownList ID="ddlSearchType" runat="server" class="form-control" onchange="toggleSearchType();">
                                                                        <asp:ListItem Text="-- Search Type --" Value="0"></asp:ListItem>
                                                                        <%--<asp:ListItem Text="School Type" Value="1"></asp:ListItem>--%>
                                                                        <asp:ListItem Text="Village Status" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Location" Value="3"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col school-type" style="display: none;">
                                                                <div class="form-group">
                                                                    <%--<label for="SchoolType" class="linhei">School Type:   </label>--%>
                                                                    <asp:DropDownList ID="ddlSchoolType" runat="server" class="form-control">
                                                                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Primary" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Upper Primary" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Secondary" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="Senior Secondary" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="KGBV" Value="5"></asp:ListItem>
                                                                        <%--<asp:ListItem Text="KGBV without school" Value="9"></asp:ListItem>--%>
                                                                        <asp:ListItem Text="Madarsa" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="Maa-Baadi" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="Anganwari" Value="8"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col  village-status" style="display: none;">
                                                                <div class="form-group">
                                                                    <%--<label for="VillageStatus" class="linhei">Village Status: </label>--%>
                                                                    <asp:DropDownList ID="ddlVillageStatus" runat="server" class="form-control ">
                                                                        <asp:ListItem Text="--All--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Operational" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Non-Operational" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col  location" style="display: none;">
                                                                <div class="form-group">
                                                                    <%--<label for="latlong" class="linhei">
                                                                        Location:
                                                                    </label>--%>
                                                                    <input type="text" class="form-control" id="latitudelongitudeInput" placeholder="Latitude,Longitude" />
                                                                </div>
                                                            </div>

                                                            <div class="btn-primary-searh-map">
                                                                 <%--<label class="visibility-hidden"> Search   </label>--%>
                                                            <div class="position-relative">
                                                                    <input type="button" class="btn btn-sm search-mp btn-primary" onclick="getmap('','');addLayers();" />
                                                                <i class="fa fa-search" aria-hidden="true"></i>
                                                            </div>
                                                            </div>
                                                        </div>
                                            </div>
                                        </div>
                                           
                                        </div>

                                        <script type="text/javascript">
                                            $(document).ready(function () {
                                                //getmap();
                                            });
                                            var map;
                                            var BlockMap = L.layerGroup();
                                            // var GPMap = L.layerGroup();
                                            var HeatMap = L.layerGroup();
                                            function getmap(flag, locationid) {
                                                showloader();
                                                if (map) {
                                                    map.remove();  // This will remove the existing map instance
                                                }

                                                state = $("[id$=ddlState]").val();

                                                document.getElementById('GISAnalyticals').innerHTML = "";
                                                document.getElementById('GISAnalyticals').innerHTML = "<div id='map'></div>";

                                                if (state == "9" || state == "9A" || state == "9B" || state == "9C") {
                                                    map = L.map('map', { maxZoom: 18, minZoom: 4, dragging: true, fullscreenControl: { pseudoFullscreen: false } }).setView(new L.LatLng(25.3903, 80.8913), 4.5);
                                                }
                                                if (state == "23") {
                                                    map = L.map('map', { maxZoom: 18, minZoom: 4, dragging: true, fullscreenControl: { pseudoFullscreen: false } }).setView(new L.LatLng(23.065833940118736, 74.62120056152345), 4.5);
                                                }

                                                //map = L.map('map', { maxZoom: 18, minZoom: 4, dragging: true, fullscreenControl: { pseudoFullscreen: false } }).setView(new L.LatLng(25.082797868, 81.053105818), 4.5);
                                                // rest of your code...

                                                //var container = L.DomUtil.get('map'); if (container != null) { container._leaflet_id = null; }

                                                //document.getElementById('map').innerHTML = "<div id='map'></div>";
                                                //var map = L.map('map', { fullscreenControl: { pseudoFullscreen: false } }).setView(new L.LatLng(25.3003467, 81.0217817), 9);
                                                //var map = L.map('map', { maxZoom: 18, minZoom: 4, dragging: true, fullscreenControl: { pseudoFullscreen: false } }).setView(new L.LatLng(25.3003467, 81.0217817), 4.5);

                                                //var tiles = L.tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png?{foo}', { foo: 'bar', fillOpacity: 0.1 }).addTo(map);

                                                var zoomHome = L.Control.zoomHome({ position: 'topleft' });
                                                zoomHome.addTo(map);
                                                map.setZoom(9);

                                                var mbAttr = "";
                                                var BaseUrls = 'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1Ijoia3dyaXNhY2l3cm0iLCJhIjoiY2xma3p3NmpoMDBhaTNwbnV1NnVkMGp2ZCJ9.1ASKnwxbjSZxZGiXn0xl4Q';

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

                                                var Terrain = L.tileLayer(BaseUrls, BaseLyrOptionsM('mapbox/outdoors-v11')),
                                                    StreetLyr = L.tileLayer(BaseUrls, BaseLyrOptionsM('mapbox/streets-v11')),
                                                    GrayLyr = L.tileLayer(BaseUrls, BaseLyrOptionsM('mapbox/light-v9'));
                                                satelliteLyr = L.tileLayer(BaseUrls, BaseLyrOptionsM('mapbox/satellite-v9'));

                                                // Add Esri Imagery Basemap Layer
                                                ImageryLyr = L.esri.basemapLayer('Imagery').addTo(map);

                                                debugger;
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
                                                if (DistrictID == "" || DistrictID == null) {
                                                    hideloader();
                                                }
                                                else {


                                                    var state = 'Uttar Pradesh'
                                                    var SateJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3ASTATE_BOUNDARY&maxFeatures=5000&outputFormat=application%2Fjson';
                                                    var DistrictJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_District_Layer_ViewNWS&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';DistrictID:' + DistrictID + '';
                                                    var thandla_Block = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Thandla_Block_Boundary&maxFeatures=5000&outputFormat=application%2Fjson';
                                                    var thandla_Village = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AThandla_Village_Block_Boundary&maxFeatures=5000&outputFormat=application%2Fjson';

                                                    //var BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Block_Lyr_View&maxFeatures=5000&outputFormat=application%2Fjson';
                                                    //var GPJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_GP_View&maxFeatures=5000&outputFormat=application%2Fjson';

                                                    //// Example URL to GeoJSON data
                                                    //var geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_HM_View&maxFeatures=5000&outputFormat=application%2Fjson';
                                                    

                                                    var BlockJSONURL = "";
                                                    var GPJSONURL = "";
                                                    var geoJSONURL = "";


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

                                                    // Use Leaflet.spin to integrate the spinner with the map
                                                    //map.spin(true, spinnerOptions);


                                                    //// Define overlayMaps before using it in the fetch block
                                                    //var overlayMaps = {};

                                                    //var geoserverWMS = 'https://geo1server.educategirls.ngo/geoserver/EG/wms';

                                                    //// GeoServer layer name
                                                    ////var geoserverLayer = 'DISTRICT_BOUNDARY'; 
                                                    //var geoserverLayer = 'Chitrakoot_District_Boundary';

                                                    //// GeoServer WMS parameters
                                                    //var wmsParams = {
                                                    //    layers: geoserverLayer,
                                                    //    format: 'image/png',
                                                    //    transparent: true
                                                    //};

                                                    //// Create a GeoServer WMS layer and add it to the map
                                                    //var wmsLayer = L.tileLayer.wms(geoserverWMS, wmsParams).addTo(map);
                                                    //Fetch GeoJSON data using fetch API
                                                    //Fetch GeoJSON data using fetch API
                                                    var District_Map = L.layerGroup();
                                                    //fetch(DistrictJSONURL)
                                                    //    .then(response => response.json())
                                                    //    .then(data => {
                                                    //        District_Map = new L.geoJson(data, { style: PLVDistrictstyle });
                                                    //        District_Map.addTo(map);

                                                    //    })
                                                    //    .catch(error => {
                                                    //        console.error('Error fetching GeoJSON data3:', error);
                                                    //    });
                                                        //--------Gauravnew-----------------//
                                                    fetch('/GISHM.aspx/GetGeoJson', {
                                                        method: 'POST',
                                                        headers: { 'Content-Type': 'application/json' },
                                                        body: JSON.stringify({ url: DistrictJSONURL })
                                                    })
                                                        .then(response => response.json())
                                                        .then(data => {
                                                            const geojson = JSON.parse(data.d);
                                                            District_Map = new L.geoJson(geojson, { style: PLVDistrictstyle });
                                                            District_Map.addTo(map);

                                                        })
                                                        .catch(error => {
                                                            console.error('Error fetching GeoJSON data3:', error);
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
                                                    console.log("BLJ_" + thandla_Block);
                                                    var BlockMapThandla = L.layerGroup();
                                                    fetch('/GISHM.aspx/GetGeoJson', {
                                                        method: 'POST',
                                                        headers: { 'Content-Type': 'application/json' },
                                                        body: JSON.stringify({ url: thandla_Block })
                                                    })
                                                        .then(res => res.json())
                                                        .then(data => {
                                                            const geojson = JSON.parse(data.d);
                                                            BlockMapThandla = new L.geoJson(geojson, { style: PLVBlockkstyle });
                                                            //BlockMapThandla.addTo(map);
                                                        })
                                                        .catch(error => {
                                                            console.error('Error fetching GeoJSON data4:', error);
                                                        });

                                                    function PLVBlockkstyle(feature) {
                                                        return {
                                                            fillColor: '#eeeee4',
                                                            weight: 2,
                                                            opacity: 0.5,
                                                            color: 'red',
                                                            //dashArray: '3',
                                                            fillOpacity: 0.5
                                                        };
                                                    }

                                                    var VillageMapThandla = L.layerGroup();
                                                    fetch('/GISHM.aspx/GetGeoJson', {
                                                        method: 'POST',
                                                        headers: { 'Content-Type': 'application/json' },
                                                        body: JSON.stringify({ url: thandla_Village })
                                                    })
                                                        .then(res => res.json())
                                                        .then(data => {
                                                            const geojson = JSON.parse(data.d);
                                                            VillageMapThandla = new L.geoJson(geojson, { style: PLV_Vilgstyle });
                                                            //VillageMapThandla.addTo(map);
                                                        })
                                                        .catch(error => {
                                                            console.error('Error fetching GeoJSON data5:', error);
                                                        });

                                                    function PLV_Vilgstyle(feature) {
                                                        return {
                                                            fillColor: '#eeeee4',
                                                            weight: 2,
                                                            opacity: 0.5,
                                                            color: 'black',
                                                            //dashArray: '3',
                                                            fillOpacity: 0.5
                                                        };
                                                    }

                                                    //dis = $("[id$=ddlDistrict]").val();
                                                    //blk = $("[id$=ddlBlock]").val();
                                                    //gp = $("[id$=ddlGP]").val();
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
                                                    var _Fyear = $("[id$=ddlYear]").val();
                                                    var _grididblock = flag;
                                                    var _locid = locationid;
                                                    var b = _locid.split("#");
                                                    var _locguidBlock = b[0];

                                                    

                                                    var info = L.control();

                                                    if (_blockcode == "" || _blockcode == null) {
                                                        if (_grididblock == "blockclick") {

                                                            BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Block_HM_View_Filter_FYNWS&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + _Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _locguidBlock + '';
                                                        } else {
                                                            //var BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Block_HM_View_NEW&maxFeatures=5000&outputFormat=application%2Fjson';
                                                            var BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Block_HM_View_NEW_FYNWS&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + _Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + '';
                                                        }
                                                    }
                                                    else {
                                                        BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Block_HM_View_Filter_FYNWS&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + _Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + '';

                                                        //BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Block_HM_View_Filter&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + '';
                                                    }

                                                    /*if (dis != "" && blk == "") {*/
                                                    //var BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Block_HM_View&maxFeatures=5000&outputFormat=application%2Fjson';
                                                    //var BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Block_HM_View_NEW&maxFeatures=5000&outputFormat=application%2Fjson';
                                                    // var BlockMap = L.layerGroup();
                                                    if (BlockMap) {
                                                        map.removeLayer(BlockMap); // Remove the existing layer
                                                    }
                                                    ("BJL_" + BlockJSONURL);
                                                    //fetch(BlockJSONURL)
                                                    //    .then(response => response.json())
                                                    //    .then(data => {
                                                    //        // Create a GeoJSON layer and add it to the map
                                                    //        BlockMap = new L.geoJson(data, {
                                                    //            style: PLVstyle,
                                                    //            onEachFeature: onEachFeatureBlock
                                                    //        });
                                                    //        if (_gridid == "blockclick") {
                                                    //            BlockMap.addTo(map);
                                                    //            //map.spin(false);
                                                    //        }
                                                    //        if (_districtcode!="" && _blockcode == "") {
                                                    //            BlockMap.addTo(map);
                                                    //        }
                                                    //        //BlockMap = new L.geoJson(data, { style: PLVBlockstyle });
                                                    //        //BlockMap.addTo(map);
                                                    //    })
                                                    //    .catch(error => {
                                                    //        console.error('Error fetching GeoJSON data6:', error);
                                                    //    });
                                                    //--------------gauravnew-//
                                                    fetch('/GISHM.aspx/GetGeoJson', {
                                                        method: 'POST',
                                                        headers: { 'Content-Type': 'application/json' },
                                                        body: JSON.stringify({ url: BlockJSONURL })
                                                    })
                                                        .then(res => res.json())
                                                        .then(data => {
                                                            const geojson = JSON.parse(data.d);
                                                            // Create a GeoJSON layer and add it to the map
                                                            BlockMap = new L.geoJson(geojson, {
                                                                style: PLVstyle,
                                                                onEachFeature: onEachFeatureBlock
                                                            });
                                                            if (_gridid == "blockclick") {
                                                                BlockMap.addTo(map);
                                                                //map.spin(false);
                                                            }
                                                            if (_districtcode != "" && _blockcode == "") {
                                                                BlockMap.addTo(map);
                                                            }
                                                            //BlockMap = new L.geoJson(data, { style: PLVBlockstyle });
                                                            //BlockMap.addTo(map);
                                                        })
                                                        .catch(error => {
                                                            console.error('Error fetching GeoJSON data6:', error);
                                                        });

                                                    function PLVBlockstyle(feature) {
                                                        return {
                                                            fillColor: '#D3D3D3',
                                                            weight: 2,
                                                            opacity: 0.5,
                                                            color: 'green',
                                                            //dashArray: '3',
                                                            fillOpacity: 0
                                                        };
                                                    }
                                                    var rTypes = $("[id$=ddlReportType]").val();
                                                    function onEachFeatureBlock(feature, layer) {
                                                        if (rTypes == "1") {

                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Block: " + feature.properties.BlockName + "<br />Quality Enrolment:" + feature.properties.EnrolmentAch + "<br /> Enrolment Universe: " + feature.properties.TotalENrollment + "</b><br/>% Quality Enrolment: " + feature.properties.Achivement + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });
                                                        }
                                                        if (rTypes == "2") {

                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Block: " + feature.properties.BlockName + "<br />Enrolment Achievement:" + feature.properties.FCRate + "<br /> Enrolment Universe: " + feature.properties.UniTarget + "</b><br/>% FC Run Rate: " + feature.properties.FCRunRate + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });


                                                        }
                                                        if (rTypes == "3") {

                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Block: " + feature.properties.BlockName + "<br />#OOSG with High Risk Error:" + feature.properties.D2DContactHighRisk + "<br /> #OOSC with CV Done: " + feature.properties.D2DContactTotal + "</b><br/>% Enrolment High Risk Error: " + feature.properties.QualityofD2DContact + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });


                                                        }
                                                        if (rTypes == "4") {

                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Block: " + feature.properties.BlockName + "<br />#OOSC with High Risk Error:" + feature.properties.EnrollHighRisk + "<br /> #OOSC with CV Done: " + feature.properties.CVEnrollTotal + "</b><br/>% Enrolment High Risk Error: " + feature.properties.CVErrorRate + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });

                                                        }
                                                        if (rTypes == "5") {
                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Block: " + feature.properties.BlockName + "<br />#OOSG Universe (5 to 14):" + feature.properties.UniTarget + "<br /> #OOSG Contacted(5 to 14): " + feature.properties.TotalContact + "</b><br/>% OOSG Contacted(5 to 14): " + feature.properties.D2DContact + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });

                                                        }
                                                        if (rTypes == "6") {

                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Block: " + feature.properties.BlockName + "<br />#OOSG with RTE Status:" + feature.properties.TotalContactRTE + "<br /> #OOSG Contacted(Home Visit): " + feature.properties.TotalHomeVisit + "</b><br/>% RTE: " + feature.properties.RTE + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });


                                                        }
                                                        if (rTypes == "7") {

                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Block: " + feature.properties.BlockName + "<br />Average Attendance per Session:" + feature.properties.AverageAttendanceperSession + "<br /> Total Children Registered: " + feature.properties.TotalChildrenRegistered + "</b><br/>%: " + feature.properties.CL3 + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });


                                                        }

                                                        if (rTypes == "8") {

                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Block: " + feature.properties.BlockName + "<br />#OOSG RTE with Document:" + feature.properties.RTEwithDoc + "<br /> #OOSG Contacted(Home Visit): " + feature.properties.TotalHomeVisit + "</b><br/>% RTE with Document: " + feature.properties.RTEwithDocument + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });


                                                        }
                                                        if (rTypes == "9" || rTypes == "9A" || rTypes == "9B" || rTypes == "9C")
                                                         {

                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Block: " + feature.properties.BlockName + "<br />#OOSG RTE without Document:" + feature.properties.RTEwithoutDoc + "<br /> #OOSG Contacted(Home Visit): " + feature.properties.TotalHomeVisit + "</b><br/>% RTE without Document: " + feature.properties.RTEwithoutDocument + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });


                                                        }


                                                        layer.on({
                                                            mouseover: highlightFeatureCluster,
                                                            mouseout: resetHighlightBlock,
                                                            preclick: resetStyleBlock,
                                                            click: zoomToFeatureCluster
                                                        });
                                                    }
                                                    function highlightFeatureCluster(e) {
                                                        var layer = e.target;
                                                        layer.setStyle({
                                                            weight: 4,
                                                            color: '#666',
                                                            dashArray: '',
                                                            fillOpacity: 0.7
                                                            //fillColor: '',

                                                        });
                                                    }
                                                    function resetHighlightBlock(e) {
                                                        BlockMap.resetStyle(e.target);
                                                    }
                                                    function resetStyleBlock(e) {
                                                        BlockMap.resetStyle(e.target);
                                                    }
                                                    function zoomToFeatureCluster(e) {
                                                        map.fitBounds(e.target.getBounds());
                                                    }
                                                    //var initPosition = [25.082797868, 81.053105818];
                                                    //map.setView(initPosition, 10);
                                                    //}
                                                    var rType = $("[id$=ddlReportType]").val();
                                                    var geoJSONURL = "";
                                                    //if (blk != "") {
                                                    //var HeatMap;
                                                    /*if (blk == "4CAB33CBCEF74D88AB553E86C") {*/
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

                                                    var vstatus = $("[id$=ddlVillageStatus]").val();

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
                                                    var Groupby = $("[id$=ddlGroupby]").val();

                                                    var _gridid = flag;
                                                    var _locid = locationid;
                                                    var b = "";
                                                    var _locguid = "";


                                                    if (Groupby == "3") {
                                                        if (_gridid == "villageclick") {
                                                            b = _locid.split("#");
                                                            _clusterid = b[0];
                                                            _locguid = b[1];
                                                        }
                                                    }
                                                    else {
                                                        b = _locid.split("#");
                                                        _locguid = b[0];
                                                    }
                                                    var Fyear = $("[id$=ddlYear]").val();
                                                    if (_clusterid == "" || _clusterid == null) {

                                                        if (Groupby == "2") {
                                                            if (_gridid == "clusterclick") {
                                                                geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HM_Cluster_Filter_FYNWS&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _locguid + ';vstatus:' + vstatus + '';

                                                                //geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HM_Cluster_Filter&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _locguid + ';vstatus:' + vstatus + '';
                                                            } else {
                                                                if (_blockcode == "" || _blockcode == null) {
                                                                    geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HM_District_FYNWS&maxFeatures=500&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';vstatus:' + vstatus + '';

                                                                    //geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HM_District&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';vstatus:' + vstatus + '';
                                                                }
                                                                else {
                                                                 
                                                                    geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HM_Cluster_FYNWS&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';vstatus:' + vstatus + '';

                                                                   // geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HM_Cluster&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';vstatus:' + vstatus + '';
                                                                }

                                                            }
                                                            //geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HM_Cluster&maxFeatures=5000&outputFormat=application%2Fjson';
                                                        }
                                                        if (Groupby == "3") {
                                                            if (_gridid == "villageclick") {
                                                                geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_HM_View_Village_FY&maxFeatures=50&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid + ';vil:' + _locguid + ';vstatus:' + vstatus + '';

                                                                //geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_HM_View_Village&maxFeatures=50&outputFormat=application%2Fjson&viewparams=StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid + ';vil:' + _locguid + ';vstatus:' + vstatus + '';
                                                            } else {
                                                             //   geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_HM_View_New&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';vstatus:' + vstatus + '';
                                                                geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_HM_View_New_FY&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';vstatus:' + vstatus + '';

                                                            }

                                                        }
                                                    }

                                                    else {
                                                        if (Groupby == "2") {
                                                            if (_gridid == "clusterclick") {
                                                                geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HM_Cluster_Filter_FY&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid + ';vstatus:' + vstatus + '';

                                                               // geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HM_Cluster_Filter&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid + ';vstatus:' + vstatus + '';
                                                            } else {
                                                                geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HM_Cluster_Filter_FY&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid + ';vstatus:' + vstatus + '';


                                                                //geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HM_Cluster_Filter&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid + ';vstatus:' + vstatus + '';
                                                            }
                                                            //geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HM_Cluster&maxFeatures=5000&outputFormat=application%2Fjson';
                                                        }
                                                        if (Groupby == "3") {
                                                            if (_gridid == "villageclick") {
                                                                geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_HM_View_Village_FY&maxFeatures=50&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid + ';vil:' + _locguid + ';vstatus:' + vstatus + '';
                                                            } else {
                                                                geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_HM_View_Filter_FY&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid + ';vstatus:' + vstatus + '';

                                                               // geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_HM_View_Filter&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid + ';vstatus:' + vstatus + '';
                                                            }

                                                        }

                                                        //geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_HM_View_Filter&maxFeatures=50&outputFormat=application%2Fjson&&viewparams=Loc:' + _clusterid + '';
                                                    }




                                                    //if (_clusterid == "" || _clusterid == null) {
                                                    //    if (Groupby == "2") {
                                                    //        geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HM_Cluster&maxFeatures=5000&outputFormat=application%2Fjson';
                                                    //    }
                                                    //    if (Groupby == "3") {
                                                    //        geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_HM_View_New&maxFeatures=5000&outputFormat=application%2Fjson';
                                                    //    }
                                                    //}
                                                    //else {
                                                    //    geoJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_HM_View_Filter&maxFeatures=50&outputFormat=application%2Fjson&&viewparams=Loc:' + _clusterid + '';
                                                    //}

                                                    debugger;
                                                    //function resetVillageLayer() {
                                                    //    if (HeatMap) {
                                                    //        map.removeLayer(HeatMap); // Remove the existing layer
                                                    //    }
                                                    //}
                                                    if (HeatMap) {
                                                        map.removeLayer(HeatMap); // Remove the existing layer
                                                    }
                                                    // Fetch GeoJSON data using fetch API

                                                    console.log("HTM_" + geoJSONURL);

                                                    //fetch(geoJSONURL)
                                                    //    .then(response => response.json())
                                                    //    .then(data => {
                                                    //        // Create a GeoJSON layer and add it to the map
                                                    //        HeatMap = new L.geoJson(data, {
                                                    //            style: PLVstyle,
                                                    //            onEachFeature: onEachFeatureCluster
                                                    //        });
                                                    //        //if (_gridid == "clusterclick" || _gridid == "villageclick") {
                                                    //        //    HeatMap.addTo(map);
                                                    //        //}
                                                    //        if (_blockcode != "" || _blockcode != null) {
                                                    //            HeatMap.addTo(map);
                                                    //        }
                                                    //        if (vstatus != "0") {
                                                    //            if (BlockMap) {
                                                    //                map.removeLayer(BlockMap); // Remove the block map layer if it exists
                                                    //            }
                                                    //            HeatMap.addTo(map);
                                                    //        }

                                                    //    })
                                                    //    .catch(error => {
                                                    //        debugger;
                                                    //        console.error('Error fetching GeoJSON data1:', error);
                                                    //    });

                                                    //------------------gauravnew-----------//
                                                    fetch('/GISHM.aspx/GetGeoJson', {
                                                        method: 'POST',
                                                        headers: { 'Content-Type': 'application/json' },
                                                        body: JSON.stringify({ url: geoJSONURL })
                                                    })
                                                        .then(res => res.json())
                                                        .then(data => {
                                                            const geojson = JSON.parse(data.d);
                                                            // Create a GeoJSON layer and add it to the map
                                                            HeatMap = new L.geoJson(geojson, {
                                                                style: PLVstyle,
                                                                onEachFeature: onEachFeatureCluster
                                                            });
                                                            //if (_gridid == "clusterclick" || _gridid == "villageclick") {
                                                            //    HeatMap.addTo(map);
                                                            //}
                                                            if (_blockcode != "" || _blockcode != null) {
                                                                HeatMap.addTo(map);
                                                            }
                                                            if (vstatus != "0") {
                                                                if (BlockMap) {
                                                                    map.removeLayer(BlockMap); // Remove the block map layer if it exists
                                                                }
                                                                HeatMap.addTo(map);
                                                            }

                                                        }).catch(error => {
                                                            console.error('Error fetching District GeoJSON data:', error);
                                                        });


                                                    //----------------------//



                                                        //resetVillageLayer();
                                                    function onEachFeatureCluster(feature, layer) {
                                                        if (rType == "1") {//props. + ':' + props. 

                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Cluster: " + feature.properties.ClusterName + "<br />Village: " + feature.properties.VillageName + "<br/> Village Operational Status: " + feature.properties.OperationalStatus + "<br />Quality Enrolment:" + feature.properties.EnrolmentAch + "<br /> Enrolment Universe: " + feature.properties.TotalENrollment + "</b><br/>% Quality Enrolment: " + feature.properties.Achivement + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });
                                                        }
                                                        if (rType == "2") {

                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Cluster: " + feature.properties.ClusterName + "<br />Village: " + feature.properties.VillageName + "<br/> Village Operational Status: " + feature.properties.OperationalStatus + "<br />Enrolment Achievement:" + feature.properties.FCRate + "<br /> Enrolment Universe: " + feature.properties.UniTarget + "</b><br/>% FC Run Rate: " + feature.properties.FCRunRate + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });


                                                        }
                                                        if (rType == "3") {

                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Cluster: " + feature.properties.ClusterName + "<br />Village: " + feature.properties.VillageName + "<br/> Village Operational Status: " + feature.properties.OperationalStatus + "<br />#OOSG with High Risk Error:" + feature.properties.D2DContactHighRisk + "<br /> #OOSC with CV Done: " + feature.properties.D2DContactTotal + "</b><br/>% Enrolment High Risk Error: " + feature.properties.QualityofD2DContact + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });


                                                        }
                                                        if (rType == "4") {

                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Cluster: " + feature.properties.ClusterName + "<br />Village: " + feature.properties.VillageName + "<br/> Village Operational Status: " + feature.properties.OperationalStatus + "<br />#OOSC with High Risk Error:" + feature.properties.EnrollHighRisk + "<br /> #OOSC with CV Done: " + feature.properties.CVEnrollTotal + "</b><br/>% Enrolment High Risk Error: " + feature.properties.CVErrorRate + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });

                                                        }
                                                        if (rType == "5") {
                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Cluster: " + feature.properties.ClusterName + "<br />Village: " + feature.properties.VillageName + "<br/> Village Operational Status: " + feature.properties.OperationalStatus + "<br />#OOSG Universe (5 to 14):" + feature.properties.UniTarget + "<br /> #OOSG Contacted(5 to 14): " + feature.properties.TotalContact + "</b><br/>% OOSG Contacted(5 to 14): " + feature.properties.D2DContact + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });

                                                        }
                                                        if (rType == "6") {

                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Cluster: " + feature.properties.ClusterName + "<br />Village: " + feature.properties.VillageName + "<br/> Village Operational Status: " + feature.properties.OperationalStatus + "<br />#OOSG with RTE Status:" + feature.properties.TotalContactRTE + "<br /> #OOSG Contacted(Home Visit): " + feature.properties.TotalHomeVisit + "</b><br/>% RTE: " + feature.properties.RTE + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });


                                                        }
                                                        if (rType == "7") {

                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Cluster: " + feature.properties.ClusterName + "<br />Village: " + feature.properties.VillageName + "<br/> Village Operational Status: " + feature.properties.OperationalStatus + "<br />Average Attendance per Session:" + feature.properties.AverageAttendanceperSession + "<br /> Total Children Registered: " + feature.properties.TotalChildrenRegistered + "</b><br/>%: " + feature.properties.CL3 + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });


                                                        }
                                                        if (rType == "8") {

                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Cluster: " + feature.properties.ClusterName + "<br />Village: " + feature.properties.VillageName + "<br/> Village Operational Status: " + feature.properties.OperationalStatus + "<br />#OOSG RTE with Document:" + feature.properties.RTEwithDoc + "<br /> #OOSG Contacted(Home Visit): " + feature.properties.TotalHomeVisit + "</b><br/>% RTE with Document: " + feature.properties.RTEwithDocument + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });


                                                        }
                                                        if (rTypes == "9" || rTypes == "9A" || rTypes == "9B" || rTypes == "9C") {

                                                            layer.bindTooltip("<p style='color: #2954A2;font-size: 12px;'>Cluster: " + feature.properties.ClusterName + "<br />Village: " + feature.properties.VillageName + "<br/> Village Operational Status: " + feature.properties.OperationalStatus + "<br />#OOSG RTE without Document:" + feature.properties.RTEwithoutDoc + "<br /> #OOSG Contacted(Home Visit): " + feature.properties.TotalHomeVisit + "</b><br/>% RTE without Document: " + feature.properties.RTEwithoutDocument + "%</p>",
                                                                {
                                                                    //direction: 'right',
                                                                    permanent: false,
                                                                    sticky: true,
                                                                    offset: [10, 0],
                                                                    opacity: 2,

                                                                    //className: 'leaflet-tooltip-own'
                                                                });


                                                        }

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
                                                            weight: 4,
                                                            color: '#666',
                                                            dashArray: '',
                                                            fillOpacity: 0.4
                                                            //fillColor: '',

                                                        });
                                                    }
                                                    function resetHighlightCluster(e) {
                                                        HeatMap.resetStyle(e.target);
                                                    }
                                                    function resetStyleCluster(e) {
                                                        HeatMap.resetStyle(e.target);
                                                    }
                                                    function zoomToFeatureCluster(e) {
                                                        map.fitBounds(e.target.getBounds());
                                                    }

                                                    //}


                                                    //}

                                                    if (rType == "1") {
                                                        function PLVstyle(feature) {
                                                            return {
                                                                fillColor: getColor(feature.properties.Achivement),
                                                                weight: 2,
                                                                opacity: 1,
                                                                color: 'black',
                                                                dashArray: '3',
                                                                fillOpacity: 0.4
                                                            };
                                                        }
                                                    }
                                                    if (rType == "2") {
                                                        function PLVstyle(feature) {
                                                            return {
                                                                fillColor: getColor(feature.properties.FCRunRate),
                                                                weight: 2,
                                                                opacity: 1,
                                                                color: 'black',
                                                                dashArray: '3',
                                                                fillOpacity: 0.7
                                                            };
                                                        }
                                                    }
                                                    if (rType == "3") {
                                                        function PLVstyle(feature) {
                                                            return {
                                                                fillColor: getColor4(feature.properties.QualityofD2DContact),
                                                                weight: 2,
                                                                opacity: 1,
                                                                color: 'black',
                                                                dashArray: '3',
                                                                fillOpacity: 0.7
                                                            };
                                                        }
                                                    }
                                                    if (rType == "4") {
                                                        function PLVstyle(feature) {
                                                            return {
                                                                fillColor: getColor4(feature.properties.CVErrorRate),
                                                                weight: 2,
                                                                opacity: 1,
                                                                color: 'black',
                                                                dashArray: '3',
                                                                fillOpacity: 0.7
                                                            };
                                                        }
                                                    }
                                                    if (rType == "5") {
                                                        function PLVstyle(feature) {
                                                            return {
                                                                fillColor: getColor(feature.properties.D2DContact),
                                                                weight: 2,
                                                                opacity: 1,
                                                                color: 'black',
                                                                dashArray: '3',
                                                                fillOpacity: 0.7
                                                            };
                                                        }
                                                    }
                                                    if (rType == "6") {
                                                        function PLVstyle(feature) {
                                                            return {
                                                                fillColor: getColor6(feature.properties.RTE),
                                                                weight: 2,
                                                                opacity: 1,
                                                                color: 'black',
                                                                dashArray: '3',
                                                                fillOpacity: 0.7
                                                            };
                                                        }
                                                    }
                                                    if (rType == "7") {
                                                        function PLVstyle(feature) {
                                                            return {
                                                                fillColor: getColor(feature.properties.CL3),
                                                                weight: 2,
                                                                opacity: 1,
                                                                color: 'black',
                                                                dashArray: '3',
                                                                fillOpacity: 0.7
                                                            };
                                                        }
                                                    }
                                                    if (rType == "8") {
                                                        function PLVstyle(feature) {
                                                            return {
                                                                fillColor: getColor8(feature.properties.RTEwithDocument),
                                                                weight: 2,
                                                                opacity: 1,
                                                                color: 'black',
                                                                dashArray: '3',
                                                                fillOpacity: 0.7
                                                            };
                                                        }
                                                    }
                                                    if (rTypes == "9" || rTypes == "9A" || rTypes == "9B" || rTypes == "9C") {
                                                        function PLVstyle(feature) {
                                                            return {
                                                                fillColor: getColor9(feature.properties.RTEwithoutDocument),
                                                                weight: 2,
                                                                opacity: 1,
                                                                color: 'black',
                                                                dashArray: '3',
                                                                fillOpacity: 0.7
                                                            };
                                                        }
                                                    }

                                                    //function getColor(d) {
                                                    //    return d >= 75 ? '#03b5fc' :
                                                    //        d >= 50 ? '#80fc03' :
                                                    //            d >= 25 ? '#fc8c03' :
                                                    //                d >= 0 ? '#fc0303' :
                                                    //                    '#c4c4c4';
                                                    //}
                                                    function getColor(d) {
                                                        return d >= 75 ? '#03b5fc' :
                                                            d >= 50 ? '#80fc03' :
                                                                d >= 25 ? '#fc8c03' :
                                                                    d > 0 ? '#fc0303' :
                                                                        d === 0 ? '#fc0303' : // specify a color for when d is exactly 0
                                                                            '#FFFFFF';
                                                    }
                                                    function getColor4(d) {
                                                        return d >= 10 ? '#FF0000' :
                                                            d >= 8 ? '#FFBF00' :
                                                                d > 0 ? '#90EE90' :
                                                                    d === 0 ? '#90EE90' : // specify a color for when d is exactly 0
                                                                    '#FFFFFF';
                                                    }
                                                    function getColor6(d) {
                                                        return d >= 70 ? '#FF0000' :
                                                            d < 20 ? '#FFBF00' :
                                                                d === 0 ? '#FFBF00' :
                                                                '#FFFFFF';
                                                    }

                                                    function getColor8(d) {
                                                        return d > 70 ? '#FF0000' :
                                                            '#c4c4c4';
                                                    }
                                                    function getColor9(d) {
                                                        return d > 10 ? '#FF0000' :
                                                            '#c4c4c4';
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




                                                    var legend = L.control({ position: 'bottomright' });
                                                    //
                                                    if (rType == "1" || rType == "2" || rType == "5" || rType == "7") {
                                                        legend.onAdd = function (map) {

                                                            var div = L.DomUtil.create('div', 'info legend'),
                                                                grades = [0, 25, 50, 75],
                                                                labels = [];

                                                            // loop through our density intervals and generate a label with a colored square for each interval
                                                            for (var i = 0; i < grades.length; i++) {
                                                                div.innerHTML +=
                                                                    '<i style="background:' + getColor(grades[i] + 1) + '"></i> ' +
                                                                    grades[i] + (grades[i + 1] ? '&ndash;' + grades[i + 1] + '%<br>' : '%+');
                                                            }

                                                            return div;
                                                        };
                                                    }
                                                    if (rType == "3") {
                                                        legend.onAdd = function (map) {

                                                            var div = L.DomUtil.create('div', 'info legend'),
                                                                grades = [0, 8, 10],
                                                                labels = [];

                                                            // loop through our density intervals and generate a label with a colored square for each interval
                                                            for (var i = 0; i < grades.length; i++) {
                                                                div.innerHTML +=
                                                                    '<i style="background:' + getColor4(grades[i] + 1) + '"></i> ' +
                                                                    grades[i] + (grades[i + 1] ? '&ndash;' + grades[i + 1] + '%<br>' : '%+');
                                                            }

                                                            return div;
                                                        };
                                                    }
                                                    if (rType == "4") {
                                                        legend.onAdd = function (map) {

                                                            var div = L.DomUtil.create('div', 'info legend'),
                                                                grades = [0, 8, 10],
                                                                labels = [];

                                                            // loop through our density intervals and generate a label with a colored square for each interval
                                                            for (var i = 0; i < grades.length; i++) {
                                                                div.innerHTML +=
                                                                    '<i style="background:' + getColor4(grades[i] + 1) + '"></i> ' +
                                                                    grades[i] + (grades[i + 1] ? '&ndash;' + grades[i + 1] + '%<br>' : '%+');
                                                            }

                                                            return div;
                                                        };
                                                    }

                                                    if (rType == "6") {
                                                        legend.onAdd = function (map) {

                                                            var div = L.DomUtil.create('div', 'info legend'),
                                                                grades = [0, 20, 70],
                                                                labels = [];

                                                            // loop through our density intervals and generate a label with a colored square for each interval
                                                            for (var i = 0; i < grades.length; i++) {
                                                                div.innerHTML +=
                                                                    '<i style="background:' + getColor6(grades[i] + 1) + '"></i> ' +
                                                                    grades[i] + (grades[i + 1] ? '&ndash;' + grades[i + 1] + '%<br>' : '%+');
                                                            }

                                                            return div;
                                                        };
                                                    }

                                                    if (rType == "8") {
                                                        legend.onAdd = function (map) {

                                                            var div = L.DomUtil.create('div', 'info legend'),
                                                                grades = [0, 70],
                                                                labels = [];

                                                            // loop through our density intervals and generate a label with a colored square for each interval
                                                            for (var i = 0; i < grades.length; i++) {
                                                                div.innerHTML +=
                                                                    '<i style="background:' + getColor8(grades[i] + 1) + '"></i> ' +
                                                                    grades[i] + (grades[i + 1] ? '&ndash;' + grades[i + 1] + '%<br>' : '%+');
                                                            }

                                                            return div;
                                                        };
                                                    }

                                                    if (rTypes == "9" || rTypes == "9A" || rTypes == "9B" || rTypes == "9C") {
                                                        legend.onAdd = function (map) {

                                                            var div = L.DomUtil.create('div', 'info legend'),
                                                                grades = [0, 10],
                                                                labels = [];

                                                            // loop through our density intervals and generate a label with a colored square for each interval
                                                            for (var i = 0; i < grades.length; i++) {
                                                                div.innerHTML +=
                                                                    '<i style="background:' + getColor9(grades[i] + 1) + '"></i> ' +
                                                                    grades[i] + (grades[i + 1] ? '&ndash;' + grades[i + 1] + '%<br>' : '%+');
                                                            }

                                                            return div;
                                                        };
                                                    }

                                                    legend.addTo(map);



                                                    //var baseLayers = {
                                                    //    "Gray": GrayLyr,
                                                    //    "Street": StreetLyr,
                                                    //    "Terrain": Terrain,
                                                    //    "Satellite": satelliteLyr
                                                    //};

                                                    

                                                    ////print
                                                    var printer = L.easyPrint({
                                                        tileLayer: BaseUrls,
                                                        sizeModes: ['Current'],
                                                        filename: 'myMap',
                                                        exportOnly: true,
                                                        hideControlContainer: true
                                                    }).addTo(map);

                                                    ///locatio
                                                    lc = L.control.locate({
                                                        strings: {
                                                            title: "Show me where I am!"
                                                        }
                                                    }).addTo(map);

                                                    var latitudelongitudeInput = document.getElementById('latitudelongitudeInput').value;

                                                    // Check if input value is not empty
                                                    if (latitudelongitudeInput.trim() !== '') {
                                                        var latlong = latitudelongitudeInput.split(",");
                                                        var lat = parseFloat(latlong[0]);
                                                        var long = parseFloat(latlong[1]);

                                                        if (!isNaN(lat) && !isNaN(long)) {
                                                            var newLatLng = [lat, long];

                                                            // Remove existing marker (if any)
                                                            if (typeof smarker !== 'undefined') {
                                                                map.removeLayer(smarker);
                                                            }

                                                            // Set marker at new coordinates
                                                            var smarker = L.marker(newLatLng).addTo(map);

                                                            // Set map view to new coordinates
                                                            map.setView(newLatLng, 13);
                                                        } else {
                                                            console.error("Invalid latitude or longitude input.");
                                                        }
                                                    }
                                                    //else {
                                                    //    console.error("Latitude and longitude input is empty.");
                                                    //}


                                                }
                                                var overlayMaps = {};

                                                var StateMap = L.layerGroup();
                                                map.spin(true, spinnerOptions);
                                                fetch('/GISHM.aspx/GetGeoJson', {
                                                    method: 'POST',
                                                    headers: { 'Content-Type': 'application/json' },
                                                    body: JSON.stringify({ url: SateJSONURL })
                                                })
                                                    .then(res => res.json())
                                                    .then(data => {
                                                        const geojson = JSON.parse(data.d);
                                                        StateMap = new L.geoJson(geojson, { style: PLVSatestyle });
                                                        //StateMap.addTo(map);
                                                        map.spin(false);
                                                        var overlayMaps = {
                                                            "Gray": GrayLyr,
                                                            "Street": StreetLyr,
                                                            "Terrain": Terrain,
                                                            "Satellite": ImageryLyr,
                                                            "State": StateMap,
                                                            "District": District_Map,
                                                            "Block": BlockMap,
                                                            //"GP": GPMap,
                                                            "HeatMap": HeatMap
                                                        };
                                                        var Count = 0;


                                                        function addLayerControl() {

                                                            window.layerControl = L.control.layers(null, overlayMaps).addTo(map);
                                                            Count = 1;

                                                        }
                                                        if (Count == 0) {
                                                            addLayerControl();
                                                        }
                                                        
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
                                                //hideloader();
                                            }
                                            $(".update_overlay").hide();
                                        </script>


                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>

                </div>
            </div>

            <div>
                <asp:ModalPopupExtender ID="ModalAlert" runat="server" TargetControlID="hdn_alertmodal" BehaviorID="ModalAlertA"
                    PopupControlID="pnl_alert" CancelControlID="btn_cancelalert" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnl_alert" runat="server" Style="display: none; background-color: #fff;border: 1px solid transparent;border-radius: 4px;" class="modalPopup alert-pop-main panel-default" >
                    <div class="alert-pop-body">
                        <div class="header">
                            <asp:Label ID="lbl_PopUpMessages" runat="server" CssClass="LabelHeader" Font-Bold="True"
                                 ></asp:Label>
                        </div>
                        <div class="body">
                            <h4>
                                <asp:Label ID="lbl_messages" runat="server" CssClass="LabelHeader" Font-Bold="True"
                                     ></asp:Label>
                            </h4>
                            <div class="text-center">
                                <asp:Button ID="btn_cancelalert" runat="server" CssClass="myButton" Text="  OK  " />
                            </div>
                        </div>
                    </div>
                    <%--     <div class="footerCategory" align="right">
                    </div>--%>
                </asp:Panel>
                <asp:HiddenField ID="hdn_alertmodal" runat="server" />
                <asp:Button ID="DoNothing" runat="server" Text="" Style="display: none" />

            </div>

        </ContentTemplate>
        <Triggers>

            <asp:PostBackTrigger ControlID="LinkButton1" />
        </Triggers>
    </asp:UpdatePanel>


</asp:Content>

