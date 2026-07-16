<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Culture="en-GB" CodeFile="GIS.aspx.cs" Inherits="GIS" EnableEventValidation="false" %>

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

    <script src="Leaflet2/leaflet.spin.js"></script>
    <script src="Leaflet2/leaflet.spin.min.js"></script>

    <script src="Leaflet2/LeafletClustersMarkers/leaflet.markercluster.js"></script>
    <script src="Scripts/comman.js" type="text/javascript"></script>
    <%--<script src="https://cdn.datatables.net/1.10.24/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.24/js/dataTables.bootstrap4.min.js"></script>--%>
    <%--<script src="https://cdn.jsdelivr.net/npm/leaflet-easybutton@2/src/easy-button.js"></script>--%>
    <script type="text/javascript" src="js/JS/easy-button.js"></script>
    <%--<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/leaflet-easybutton@2/src/easy-button.css">--%>
    <link rel="stylesheet" href="css/CSS/easy-button.css" />
    <script src="Leaflet2/bundle.js"></script>

    <script src="Leaflet2/leaflet.groupedlayercontrol.min.js"></script>

    <script src="Leaflet2/leaflet.spin.min.js" charset="utf-8"></script>
    <script src="Leaflet2/L.Control.Locate.js"></script>
    <script src="Leaflet2/leaflet-search.js"></script>
    <link href="Leaflet2/leaflet-search.css" rel="stylesheet" type="text/css" />

    <%--<link type="text/css" href="https://cdn.datatables.net/1.13.7/css/dataTables.bootstrap.min.css">--%>
    <link type="text/css" href="css/CSS/dataTables.bootstrap.min.css" />
    <%--<link type="text/css" href="https://cdn.datatables.net/fixedheader/3.4.0/css/fixedHeader.bootstrap.min.css">--%>
    <link type="text/css" href="css/CSS/fixedHeader.bootstrap.min.css" />

    <%--<script type="text/javascript" src="https://code.jquery.com/jquery-3.7.0.js"></script>--%>
    <script type="text/javascript" src="js/JS/jquery-3.7.0.js"></script>
    <script type="text/javascript" src="Scripts/jquery.dataTables.min.js"></script>
    <%--<script src="https://cdn.datatables.net/1.13.7/js/jquery.dataTables.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/jquery.dataTables.min.js"></script>
    <%--<script type="text/javascript" src="https://cdn.datatables.net/1.13.7/js/dataTables.bootstrap.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/dataTables.bootstrap.min.js"></script>
    <%--<script type="text/javascript" src="https://cdn.datatables.net/fixedheader/3.4.0/js/dataTables.fixedHeader.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/dataTables.fixedHeader.min.js"></script>

    <!-- Esri Leaflet CSS and JS -->
    <%--<link rel="stylesheet" href="https://unpkg.com/esri-leaflet-geocoder/dist/esri-leaflet-geocoder.css" />--%>
    <link rel="stylesheet" href="css/CSS/esri-leaflet-geocoder.css" />
    <%--<script src="https://unpkg.com/esri-leaflet/dist/esri-leaflet.js"></script>--%>
    <script src="js/JS/esri-leaflet.js" type="text/javascript"></script>
    <!-- map Loader -->
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/spin.js/2.3.2/spin.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/spin.min.js"></script>

    <style type="text/css">
        .bg-white_1 {
            height: 422px;
            overflow: hidden;
            width: 100%;
        }

        .form-control.table-filter {
            height: 28px;
        }

        .bg-white.panel.panel-default.bg-white_1 {
        }

        .bg-white_1 .dis-flex {
            margin-bottom: 5px;
        }

        #map {
            min-height: 422px;
            /*min-height: calc(100vh - 212px);*/
            width: 100%;
            /*top: 20px !important;*/
            /*height: 450px;*/
        }


        #myButton2 {
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

        #myButton {
            background-image: url('images/search-29.png');
            background-color: transparent; /* Adjust as needed */
            width: 30px;
            height: 30px; /* Set the height of the button */
            border: none; /* Remove the default button border */
            cursor: pointer;
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

        #search_box {
            background: transparent;
            width: 100%;
            height: auto;
            display: none;
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

        #tblLocDetails_wrapper row:nth-child(2) {
            margin: 0px !important;
        }

        .search-bg {
            background: linear-gradient(to bottom, #ebf1fd 0%,#ffffff 100%) !important;
            /* background-color: rgb(241, 241, 241)!important; */
            padding-top: 7px !important;
            border: 1px solid rgb(221, 221, 221) !important;
            border-top-left-radius: 4px !important;
            border-top-right-radius: 4px !important;
            margin-bottom: 0px !important;
        }

        .common-header {
            min-width: 130px;
        }

        .common-cell {
            min-width: 130px;
        }

        .panel-heading {
            padding: 15px 15px;
        }

        .MapSummary-wrp .dataTables_wrapper .row:nth-child(2) .col-sm-12 {
            overflow: auto;
        }

        .MapSummary-wrp .dataTables_wrapper .row:nth-child(1) {
            display: none;
        }

        .MapSummary-wrp .dataTables_wrapper .row:nth-child(3) .col-sm-7 {
            display: none;
        }
        /*====================================================*/
        /*        #tblLocDetails_wrapper .row:nth-child(1), #tblLocDetails_wrapper .row:nth-child(3) {
            display: none !important;
        }
*/
        #MapSummary table thead {
            width: calc(100% - 10px) !important;
        }

        #MapSummary table {
            background: linear-gradient(to bottom, #ffe5e6 0%,#fff8f8 100%);
        }

        ::-webkit-scrollbar {
            width: 10px;
            height: 10px;
        }

        #MapSummary table thead tr th:nth-last-child(1) {
            border-right: 0px;
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
            height: 280px;
            width: 100%;
            overflow-y: auto;
            overflow-x: hidden !important
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

        #tblLocDetails_wrapper row:nth-child(2) col-sm-12 {
            padding-left: 0px !important;
            padding-right: 0px !important;
        }

        .MapSummary-wrp .dataTables_wrapper .row:nth-child(2) {
            overflow: hidden;
        }


        .dis-flex h4 {
            font-size: 14px;
            margin: 0;
            font-weight: 700;
        }




        .dis-flex {
            padding-left: 0;
            padding-right: 15px;
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
        .layer-separator {
    margin: 6px 0;
    padding: 4px;
    font-weight: bold;
    background: #f2f2f2;
    border-top: 1px solid #999;
    border-bottom: 1px solid #999;
}

    </style>
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
        });

    </script>
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



                //setTimeout(function () {
                //    $('#tblLocDetails').dataTable();
                //}, 3000);
                function bindMaster() {
                    
                    Fill_FYear("ddlYear");
                    $('[id$=ddlYear]').val("2026");
                    Fill_State("ddlState");
                    var FYear = $("[id$=ddlYear] option:selected").text();
                    var UserlevelRole = '<%= Session["user_level_Role"] %>';
                    if (FYear == '2026-2027' && UserlevelRole == '1') {
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
                    }

                function bindMasterYear() {
                        
                      
                        Fill_State("ddlState");
                        var FYear = $("[id$=ddlYear] option:selected").text();
                        var UserlevelRole = '<%= Session["user_level_Role"] %>';
                        if (FYear == '2026-2027' && UserlevelRole == '1') {
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
                        call_function('', '');
                        Get_Details();
                }

                function showloader() {
                    $(".update_overlay").show();
                }

                //function hideloader() {
                //    setTimeout(function () {
                //        $(".update_overlay").hide();
                //    }, 3000);

                //}
                function hideloader() {
                    setTimeout(function () {
                        $(".update_overlay").hide();
                    }, 4000);

                }

                function Go_to_Location(locid, th) {
                    ZoomToLatLong_Click(locid);
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
                        objvr.ValidID5 = FYearID;
                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            url: 'GIS.aspx/Get_MapDetails',
                            data: JSON.stringify(objvr),
                            success: function (response) {
                                $("#MapSummary").html(response.d);
                                ZoomToLatLong();
                                $(".update_overlay").hide();
                            },
                            error: function () {
                                Show_ModalAlert("Please try again !!");
                                $(".update_overlay").hide();
                                return false;

                            }
                        });
                    }


                    catch (e) {
                        $(".update_overlay").hide();
                        Show_ModalAlert("Please try again !!");
                    }
                    setTimeout(logMe, 7000);

                }
                function gotolatlong(value) {
                    
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
                    
                    state = $("[id$=ddlState]").val();
                    
                    
                    dis = $("[id$=ddlDistrict]").val();
                    blk = $("[id$=ddlBlock]").val();
                    gp = $("[id$=ddlGP]").val();
                    setTimeout(function () {
                        if (state != "" && dis == "") {
                            map.removeLayer(BlockMap);
                            map.removeLayer(VillageMap);

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
                            map.addLayer(BlockMap);
                            map.removeLayer(VillageMap);
                            gotolatlong(dis);
                        }
                        else if (blk != "" && gp == "") {

                            map.addLayer(VillageMap);
                            map.removeLayer(BlockMap);
                            gotolatlong(blk);
                        }
                        else if (gp != "" && blk != "") {
                            
                            map.addLayer(VillageMap);
                            map.removeLayer(BlockMap);
                            gotolatlong(gp);
                        }
                        else {
                            var initPosition = [23.473324, 77.947998];
                            map.setView(initPosition, 4.5);
                            map.removeLayer(BlockMap);
                            map.removeLayer(VillageMap);
                        }
                        
                    }, 1000);
                    
                }

                function showSearch() {
                    $('#map-search').show();
                }

                function addLayers() {
                    
                    $('#map-search').hide();
                    
                    state = $("[id$=ddlState]").val();
                    dis = $("[id$=ddlDistrict]").val();
                    blk = $("[id$=ddlBlock]").val();
                    gp = $("[id$=ddlGP]").val();
                   var v_status = $("[id$=ddlVillageStatus]").val();
                    if (state != "" && dis == "") {
                        map.removeLayer(BlockMap);
                        map.removeLayer(VillageMap);
                    }
                    else if (dis != "" && blk == "") {
                        if (v_status != "0") {
                            map.removeLayer(BlockMap);
                            map.addLayer(VillageMap);
                        }
                        else {
                            map.addLayer(BlockMap);
                            map.removeLayer(VillageMap);
                        }
                       

                    }
                    else if (blk != "" && gp == "") {
                        map.addLayer(VillageMap);
                        map.removeLayer(BlockMap);

                    }
                    else if (gp != "" && blk != "") {

                        map.addLayer(VillageMap);
                        map.removeLayer(BlockMap);
                    }
                    else {
                        var initPosition = [23.473324, 77.947998];
                        map.setView(initPosition, 4.5);
                        map.removeLayer(BlockMap);
                        map.removeLayer(VillageMap);
                    }

                }


                function ZoomToLatLong_Click(loc) {
                    
                    state = $("[id$=ddlState]").val();
                    dis = $("[id$=ddlDistrict]").val();
                    blk = $("[id$=ddlBlock]").val();
                    gp = $("[id$=ddlGP]").val();
                    if (state != "" && dis == "") {
                        
                        map.removeLayer(BlockMap);
                        map.removeLayer(VillageMap);
                    }
                    else if (dis != "" && blk == "") {

                        
                        map.addLayer(BlockMap);
                        map.removeLayer(VillageMap);
                    }
                    else if (blk != "" && gp == "") {

                        map.removeLayer(BlockMap);
                        map.removeLayer(VillageMap);
                        map.addLayer(VillageMap);
                    }
                    else if (gp != "" && blk != "") {
                        
                        map.removeLayer(BlockMap);
                        map.removeLayer(VillageMap);
                        map.addLayer(VillageMap);
                    }
                    else {
                        var initPosition = [23.473324, 77.947998];
                        map.setView(initPosition, 4.5);
                        map.removeLayer(BlockMap);
                        map.removeLayer(VillageMap);
                    }

                    if ((state == "9" || state == "9A" || state == "9B" || state == "9C")) {
                        var initPosition = [25.082797868, 81.053105818];
                        map.setView(initPosition, 9);
                    }
                    if (state == "23") {
                        var initPosition = [23.065833940118736, 74.62120056152345];
                        map.setView(initPosition, 9);
                    }
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
                            <div class="panel-heading" style="background-color: transparent; padding: 5px 10px;">

                                <div class="row" style="margin-left: -15px; margin-right: -15px">
                                    <div class="col-sm-12" style="padding-right: 0px;">
                                        <div class="dis-flex">
                                            <h3 class="text-danger1" style="margin: 0px;">
                                                <asp:Label ID="lblMain" runat="server" Text="Coverage" Style="margin: 3px 0px 5px 5px; font-weight: bold; font-size: medium;"></asp:Label>
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
                            <div class="panel panel-default">
                                <div class="panel-body" style="padding-top: 0px; padding-bottom: 0px;">
                                    <div class="row" style="margin: 0px -15px;">
                                        <div class="col-lg-12  search-bg">
                                            <div id="container-target">
                                                <div class="form-horizontal">
                                                    <div class="row" style="margin: 9px -15px 0px -15px;">
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3  linhei" style="padding-top: 2px; font-weight: bold !important;">
                                                                    Year:<span class="mandatory-label"></span>
                                                                </label>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList ID="ddlYear" runat="server" onchange="bindMasterYear();" class="form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="ddlState" class="col-sm-3  linhei" style="padding-top: 2px; font-weight: bold !important;">State:<span class="mandatory-label"></span></label>
                                                                <div class="col-sm-9 ">
                                                                    <asp:DropDownList ID="ddlState" runat="server" onchange="Fill_District('ddlDistrict');Fill_Block('ddlBlock');Fill_Cluster('ddlGP');" class="form-control ">
                                                                    </asp:DropDownList>
                                                                    
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="ddlDistrict" class="col-sm-3 linhei" style="padding-top: 2px; font-weight: bold !important;">District:<span class="mandatory-label"></span></label>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList ID="ddlDistrict" runat="server" onchange="Fill_Block('ddlBlock');" class="form-control " />

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="ddlBlock" class="col-sm-3  linhei" style="padding-top: 2px; font-weight: bold !important;">Block:</label>
                                                                <div class="col-sm-9 ">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlBlock" runat="server" onchange="Fill_Cluster('ddlGP');" class="form-control" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="ddlGP" class="col-sm-3  linhei" style="padding-top: 2px; font-weight: bold !important;">Cluster:</label>
                                                                <div class="col-sm-9 ">
                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlGP" runat="server" class="form-control" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <input type="button" id="myButton" class="btn btn-danger btn-paddd" style="margin-left: -4rem;" onclick="call_function('', '');Get_Details();" />
                                                                   
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
                            <div class="bg-white panel panel-default bg-white_1">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div class="panel-heading" style="padding-top: 5px;">
                                            <div class="dis-flex">
                                                <h4>Report: Coverage</h4>
                                                <div>
                                                    <input type="search" class="form-control table-filter" placeholder="Search..." />
                                                </div>
                                                <div>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Export" OnClick="LinkButton1_Click"
                                                        class=""></asp:LinkButton>
                                                    <button type="button" class="zoom_div" style="padding: 0px 0px 0px 12px; background-color: white; border: none;">
                                                        <i class="fa fa-expand fa-lg text-danger"></i>
                                                    </button>
                                                </div>
                                            </div>
                                            <div class="MapSummary-wrp">
                                                <div id="MapSummary" class="">
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="asd" style="height: 424px; overflow: hidden;">

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
                                                                    <asp:DropDownList ID="ddlSearchType" runat="server" class="form-control" onchange="toggleSearchType();">
                                                                        <asp:ListItem Text="-- Search Type --" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="School Type" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Village Status" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Location" Value="3"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col school-type" style="display: none;">
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddlSchoolType" runat="server" class="form-control">
                                                                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Primary" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Upper Primary" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Secondary" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="Senior Secondary" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="KGBV" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="Madarsa" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="Maa-Baadi" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="Anganwari" Value="8"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col  village-status" style="display: none;">
                                                                <div class="form-group">
                                                                    
                                                                    <asp:DropDownList ID="ddlVillageStatus" runat="server" class="form-control ">
                                                                        <asp:ListItem Text="--All--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Operational" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Non-Operational" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col  location" style="display: none;">
                                                                <div class="form-group">
                                                                   
                                                                    <input type="text" class="form-control" id="latitudelongitudeInput" placeholder="Latitude,Longitude" />
                                                                </div>
                                                            </div>

                                                            <div class="btn-primary-searh-map">
                                                            <div class="position-relative">
                                                                    <input type="button" class="btn btn-sm search-mp btn-primary" onclick="call_function('', '');addLayers();" />
                                                                <i class="fa fa-search" aria-hidden="true"></i>
                                                            </div>
                                                            </div>
                                                        </div>
                                            </div>
                                        </div>
                                            </div>
                                        <%--//Gaurav--%>
                                        <script type="text/javascript">

                                            $(document).ready(function () {
                                                //getmap();
                                            });
                                            var map;
                                            var StateMap = L.layerGroup();
                                            var District_Map = L.layerGroup();
                                            var BlockMap = L.layerGroup();
                                            var VillageMap = L.layerGroup();
                                            var schoolMarkers;
                                            var layerControl;
                                            var addedLayers = {};

                                            var GrayLyr, StreetLyr, Terrain, ImageryLyr;
                                            var mbAttr = "";
                                            var BaseUrls = 'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1Ijoia3dyaXNhY2l3cm0iLCJhIjoiY2xma3p3NmpoMDBhaTNwbnV1NnVkMGp2ZCJ9.1ASKnwxbjSZxZGiXn0xl4Q';

                                            var overlayMaps = {};
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
                                                StreetLyr = L.tileLayer(BaseUrls, BaseLyrOptionsM('mapbox/streets-v11'));
                                                Terrain = L.tileLayer(BaseUrls, BaseLyrOptionsM('mapbox/outdoors-v11'));
                                                ImageryLyr = L.esri.basemapLayer('Imagery').addTo(map);

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

                                                District_Map.clearLayers();
                                                BlockMap.clearLayers();
                                                VillageMap.clearLayers();

                                                // Clear previous layer control (if any)
                                                if (window.layerControl) {
                                                    map.removeControl(window.layerControl);
                                                }

                                                var zoomHome = L.Control.zoomHome({ position: 'topleft' });
                                                zoomHome.addTo(map);

                                                map.setZoom(9);


                                                initializeBaseLayers();

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

                                                    var legend = L.control({ position: 'bottomright' });

                                                    legend.onAdd = function (map) {

                                                        var div = L.DomUtil.create('div', 'info legend')
                                                        div.innerHTML += ' <img src="images/Primary.png" alt="Primary" style="width: 20px;height: 23px;"> Primary';
                                                        div.innerHTML += '<br/> <img src="images/Upper_Primary.png" alt="Upper_Primary" style="width: 20px;height: 23px;"> Upper Primary';
                                                        div.innerHTML += '<br/> <img src="images/Secondary.png" alt="Secondary" style="width: 20px;height: 23px;"> Secondary';
                                                        div.innerHTML += '<br/> <img src="images/Senior_Secondary.png" alt="Senior_Secondary" style="width: 20px;height: 23px;"> Senior Secondary';
                                                        div.innerHTML += '<br/> <img src="images/KGBV_with_school.png" alt="KGBV_with_school" style="width: 20px;height: 23px;"> KGBV with school';
                                                        div.innerHTML += '<br/> <img src="images/KGBV_without_school.png" alt="KGBV_without_school" style="width: 20px;height: 23px;"> KGBV without school';
                                                        div.innerHTML += '<br/> <img src="images/Madarsa.png" alt="Madarsa" style="width: 20px;height: 23px;"> Madarsa';
                                                        div.innerHTML += '<br/> <img src="images/Maa-Baadi.png" alt="Maa-Baadi" style="width: 20px;height: 23px;"> Maa-Baadi';
                                                        div.innerHTML += '<br/> <img src="images/Anganwari.png" alt="Anganwari" style="width: 20px;height: 23px;"> Anganwari';

                                                        return div;
                                                    };

                                                    //legend.addTo(map);

                                                    var animatedToggle = L.easyButton({
                                                        id: 'animated-marker-toggle',
                                                        type: 'animate',
                                                        position: 'bottomright',
                                                        states: [{
                                                            stateName: 'add-markers',
                                                            icon: '<img src="images/L.png" width="20" height="20">',
                                                            title: 'add legend',
                                                            onClick: function (control) {
                                                                legend.addTo(map);
                                                                control.state('remove-markers');
                                                            }
                                                        }, {
                                                            stateName: 'remove-markers',
                                                            title: 'remove legend',
                                                            icon: 'fa fa-times-circle',
                                                            onClick: function (control) {
                                                                legend.remove();
                                                                control.state('add-markers');
                                                            }
                                                        }]
                                                    });
                                                    animatedToggle.addTo(map);

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

                                                }
                                                var overlayMaps = {};
                                                call_function(flag, locationid);
                                                
                                                //loadDBLayers();
                                            }
                                            //------------gaurav pathak-----//
                                            function loadDBLayers() {
                                                fetch('GIS.aspx/GetActiveLayers', {
                                                    method: 'POST',
                                                    headers: { 'Content-Type': 'application/json' }
                                                })
                                                    .then(r => r.json())
                                                    .then(r => buildDynamicLayers(r.d));
                                            }
                                            function buildDynamicLayers(layerList) {

                                                layerList.forEach(l => {

                                                    let url = `https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0` +
                                                        `&request=GetFeature&typeName=${l.LayerName}&outputFormat=application/json`;
                                                   
                                                    fetch(url)
                                                        .then(res => res.json())
                                                        .then(data => {

                                                            let layer = L.geoJSON(data, {
                                                                style: function (feature) {
                                                                    return {
                                                                        color: "#000000",
                                                                        weight: 1,
                                                                        fillColor: feature.properties.COLOR_HEX,
                                                                        fillOpacity:1
                                                                    };
                                                                }
                                                            });

                                                            // 🧠 Register with your existing control
                                                            safeAddToControl(layer, l.DisplayName);
                                                        });
                                                });
                                            }
                                            function safeAddToControl(layer, name) {

                                                // wait until your existing control is ready
                                                if (typeof layerControl === "undefined" || !layerControl) {
                                                    console.warn("Layer control not ready yet. Retrying:", name);

                                                    setTimeout(function () {
                                                        safeAddToControl(layer, name);
                                                    }, 300);

                                                    return;
                                                }

                                                if (!window.addedLayers) window.addedLayers = {};

                                                if (!window.addedLayers[name]) {
                                                    layerControl.addOverlay(layer, name);
                                                    window.addedLayers[name] = true;
                                                    console.log("Added to control:", name);
                                                }
                                            }


                                            //---------------------------------//

                                            

                                            $(".update_overlay").hide();




                                            function goto_lat_long() {
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
                                                var DistrictJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_District_Layer_ViewNWS&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';DistrictID:' + DistrictID + '';

                                                //fetch(DistrictJSONURL)
                                                //    .then(response => response.json())
                                                //    .then(data => {

                                                //        // Remove the existing district layer if it exists
                                                //        if (District_Map) {
                                                //            map.removeLayer(District_Map);
                                                //        }

                                                //        // Create a new district layer and add it to the map
                                                //        District_Map = L.geoJson(data, { style: PLVDistrictstyle });
                                                //        District_Map.addTo(map);

                                                //        //District_Map = new L.geoJson(data, { style: PLVDistrictstyle });
                                                //        //District_Map.addTo(map);

                                                //    })
                                                //    .catch(error => {
                                                //        console.error('Error fetching GeoJSON data:', error);
                                                //    });
                                                //------gauravnew----//

                                                fetch('/GIS.aspx/GetGeoJson', {
                                                    method: 'POST',
                                                    headers: { 'Content-Type': 'application/json' },
                                                    body: JSON.stringify({ url: DistrictJSONURL })
                                                })
                                                    .then(res => res.json())
                                                    .then(data => {
                                                        const geojson = JSON.parse(data.d);
                                                        //Remove the existing district layer if it exists
                                                        if (District_Map) {
                                                            map.removeLayer(District_Map);
                                                        }

                                                        // Create a new district layer and add it to the map
                                                        District_Map = L.geoJson(geojson, { style: PLVDistrictstyle });
                                                        District_Map.addTo(map);
                                                                
                                                    }).catch(error => {
                                                       console.error('Error fetching District GeoJSON data:', error);
                                                    });


                                                //----------------------//

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
                                                        BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Block_Lyr_View_FilterNWS&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _locguidBlock + '';
                                                    } else {
                                                        BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Block_Lyr_View_NewNWS&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + '';
                                                    }
                                                }
                                                else {
                                                    BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Block_Lyr_View_FilterNWS&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + '';
                                                }
                                                if (BlockMap) {
                                                    map.removeLayer(BlockMap); // Remove the existing layer
                                                }
                                                console.log("gaurav+" + BlockJSONURL);
                                                ////BlockMap = L.layerGroup();
                                                //fetch(BlockJSONURL)
                                                //    .then(response => response.json())
                                                //    .then(data => {
                                                //        // Create a GeoJSON layer and add it to the map
                                                //        BlockMap = new L.geoJson(data, {
                                                //            style: PLVBlockstyle,
                                                //            onEachFeature: onEachFeatureBlock
                                                //        });
                                                //        if (_gridid == "blockclick" || _blockcode == "" || _blockcode == null) {
                                                //            BlockMap.addTo(map);
                                                //            //map.spin(false);
                                                //        }
                                                //        //BlockMap = new L.geoJson(data, { style: PLVBlockstyle });
                                                //        //BlockMap.addTo(map);
                                                //    })
                                                //    .catch(error => {
                                                //        console.error('Error fetching GeoJSON data:', error);
                                                //    });

                                                //------gauravnew----//

                                                fetch('/GIS.aspx/GetGeoJson', {
                                                    method: 'POST',
                                                    headers: { 'Content-Type': 'application/json' },
                                                    body: JSON.stringify({ url: BlockJSONURL })
                                                })
                                                    .then(res => res.json())
                                                    .then(data => {
                                                        const geojson = JSON.parse(data.d);

                                                        BlockMap = L.geoJSON(geojson, {
                                                            style: PLVBlockstyle,
                                                            onEachFeature: onEachFeatureBlock
                                                        })
                                                        if (_gridid == "blockclick" || _blockcode == "" || _blockcode == null) {
                                                            BlockMap.addTo(map);
                                                            //map.spin(false);
                                                        }
                                                    });
                                               

                                                //----------------------//


                                                function PLVBlockstyle(feature) {
                                                    return {

                                                        fillColor: feature.properties.colorCode,
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



                                            // Reset the VillageMap layer before adding a new one
                                            function resetVillageLayer() {
                                                if (VillageMap) {
                                                    map.removeLayer(VillageMap); // Remove the existing layer
                                                }
                                            }

                                            function bindSchools(flag, locationid) {
                                                var overlayMaps = {};
                                                
                                                var b = "";
                                                var _locguid = "";
                                                var _statecode = $("[id$=ddlState]").val();
                                                var _districtcode = extractCode("[id$=ddlDistrict]");
                                                var _blockcode = extractCode("[id$=ddlBlock]");
                                                var _clusterid = extractCode("[id$=ddlGP]").replace(/-/g, "");
                                                var Fyear = $("[id$=ddlYear]").val();
                                                var schooltype = $("[id$=ddlSchoolType]").val();
                                                var _gridid = flag;
                                                var _locid = locationid;


                                                if (_gridid == "blockclick") {
                                                    b = _locid.split("#");
                                                    _blockcode = b[0];
                                                }

                                                if (_gridid == "clusterclick") {
                                                    b = _locid.split("#");
                                                    _clusterid = b[0];
                                                }
                                                if (_gridid == "villageclick") {
                                                    b = _locid.split("#");
                                                    _locguid = b[0];
                                                }


                                                // Remove existing markers if they exist
                                                if (schoolMarkers) {
                                                    map.removeLayer(schoolMarkers);
                                                    schoolMarkers.clearLayers();
                                                }

                                                // Initialize a new marker cluster group
                                                schoolMarkers = new L.MarkerClusterGroup({
                                                    iconCreateFunction: function (cluster) {
                                                        var childCount = cluster.getChildCount();
                                                        return new L.DivIcon({
                                                            html: '<div style="background-color: rgba(240, 194, 12, 0.6); border: 2px solid black; border-radius: 50%; width: 40px; height: 40px; display: flex; align-items: center; justify-content: center; font-weight: bold;">' + childCount + '</div>',
                                                            className: 'custom-cluster-icon'
                                                        });
                                                    }
                                                });

                                                var pUrl = generateGeoServerURL(_gridid, _statecode, _districtcode, _blockcode, _clusterid, _locguid, Fyear, schooltype);
                                                

                                                //$.getJSON(pUrl, function (data) {
                                                //    L.geoJson(data, {
                                                //        pointToLayer: function (feature, latlng) {
                                                //            var schoolIcon = L.icon({
                                                //                iconUrl: getIconUrl(feature.properties.SchoolLevel),
                                                //                iconSize: [40, 40]
                                                //            });

                                                //            var pMarker = L.marker(latlng, { icon: schoolIcon, title: feature.properties.georefcode })
                                                //                .bindPopup(createPopupContent(feature));

                                                //            schoolMarkers.addLayer(pMarker);
                                                //            return pMarker;
                                                //        }
                                                //    });

                                                //    map.addLayer(schoolMarkers);
                                                //    overlayMaps["School Markers"] = schoolMarkers;
                                                //});
                                                
                                                console.log(pUrl);
                                                //-------------gauravnew--//
                                                fetch('/GIS.aspx/GetGeoJson', {
                                                    method: 'POST',
                                                    headers: {
                                                        'Content-Type': 'application/json'
                                                    },
                                                    body: JSON.stringify({ url: pUrl })
                                                })
                                                    .then(res => {
                                                        if (!res.ok) throw new Error('Server error');
                                                        return res.json();
                                                    })
                                                    .then(data => {
                                                        const geojson = JSON.parse(data.d);
                                                        L.geoJSON(geojson, {
                                                            pointToLayer: function (feature, latlng) {

                                                                const schoolIcon = L.icon({
                                                                    iconUrl: getIconUrl(feature.properties.SchoolLevel),
                                                                    iconSize: [40, 40]
                                                                });

                                                                const pMarker = L.marker(latlng, {
                                                                    icon: schoolIcon,
                                                                    title: feature.properties.georefcode
                                                                }).bindPopup(createPopupContent(feature));

                                                                schoolMarkers.addLayer(pMarker);
                                                                return pMarker;
                                                            }
                                                        });

                                                        map.addLayer(schoolMarkers);
                                                        overlayMaps["School Markers"] = schoolMarkers;
                                                    })
                                                    .catch(err => {
                                                        console.error('Fetch proxy error:', err);
                                                    });
                                                //---------//
                                            }

                                            function extractCode(selector) {
                                                var value = $(selector).val();
                                                return value.length > 10 ? value.split("#")[0] : value;
                                            }

                                            function generateGeoServerURL(gridId, stateCode, districtCode, blockCode, clusterId, locguId, year, schoolType) {
                                                
                                                var baseUrl = "https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&outputFormat=application%2Fjson&maxFeatures=5000";
                                                var typeName = "EG%3AEG_Schools_Chitrakoot_";

                                                if (!blockCode) {
                                                    typeName += gridId === "blockclick" ? "FY" : "State_FY";
                                                } else if (!clusterId) {
                                                    typeName += gridId === "clusterclick" ? "Cluster_FY" : "FY";
                                                } else {
                                                    typeName += gridId === "villageclick" ? "Village_FY" : "Cluster_FY";
                                                }

                                                return `${baseUrl}&typeName=${typeName}&viewparams=Fyear:${year};StateCode:${stateCode};DistrictCode:${districtCode};BlockCode:${blockCode || ""};Loc:${clusterId || ""};vil:${locguId || ""};stype:${schoolType}`;
                                            }

                                            function getIconUrl(SchoolLevel) {
                                                console.log(SchoolLevel)
                                                var iconUrl = {
                                                    1: 'images/Primary.png',
                                                    2: 'images/Upper_Primary.png',
                                                    3: 'images/Secondary.png',
                                                    4: 'images/Senior_Secondary.png',
                                                    5: 'images/KGBV_with_school.png',
                                                    6: 'images/Madarsa.png',
                                                    7: 'images/Maa-Baadi.png',
                                                    8: 'images/KGBV_without_school.png',
                                                    9: 'images/Anganwari.png'
                                                };
                                                return iconUrl[SchoolLevel] || 'images/default.png';
                                                console.log(iconUrl[SchoolLevel]);
                                            }

                                            function createPopupContent(feature) {
                                                return `
        <div>
            <table class='table table-bordered' style='margin-bottom:0px !important;'>
                <tr><td class='popuptablefont'>School Name:</td><td class='popuptablefont'>${feature.properties.Name}</td></tr>
                <tr><td class='popuptablefont'>DISE Code:</td><td class='popuptablefont'>${feature.properties.DISECode}</td></tr>
                <tr><td class='popuptablefont'>Village:</td><td class='popuptablefont'>${feature.properties.VillageName}</td></tr>
                <tr><td class='popuptablefont'>School Level:</td><td class='popuptablefont'>${feature.properties.SchoolLevelName}</td></tr>
                <tr><td class='popuptablefont'>Operational Status:</td><td class='popuptablefont'>${feature.properties.OperationalStatus}</td></tr>
                <tr><td class='popuptablefont'>GKP Status:</td><td class='popuptablefont'>${feature.properties.GKP_Status}</td></tr>
                <tr><td class='popuptablefont'>GKP Level:</td><td class='popuptablefont'>${feature.properties.GKP_Level}</td></tr>
                <tr><td class='popuptablefont'>Enrolled OOSG's:</td><td class='popuptablefont'>${feature.properties.EnOSCG}</td></tr>
                <tr><td class='popuptablefont'>Location:</td><td class='popuptablefont'>${feature.properties.sLatitude},<br/>${feature.properties.sLongitute}</td></tr>
            </table>
        </div>
    `;
                                            }


                                            // Global variable for HH MarkerClusterGroup
                                            var HHmarkers = null;

                                            function bindHHLayer(flag, locationid) {
                                                
                                                var b = "";
                                                var _locguid = "";
                                                // Fetch dropdown values automatically
                                                var _grididblock = flag;
                                                var _gridid = flag;
                                                var _locid = locationid;
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
                                                var _clusterid = extractCode("[id$=ddlGP]").replace(/-/g, "");
                                                //var _locguid = $("[id$=ddlVillage]").val();

                                                //var _locid = locationid;
                                                //var b = _locid.split("#");
                                                //var _locguidBlock = b[0];

                                                //var _locid = locationid;
                                                //var b = _locid.split("#");
                                                //var _locguid = b[0];

                                                if (_gridid == "blockclick") {
                                                    b = _locid.split("#");
                                                    _blockcode = b[0];
                                                }

                                                if (_gridid == "clusterclick") {
                                                    b = _locid.split("#");
                                                    _clusterid = b[0];
                                                }
                                                if (_gridid == "villageclick") {
                                                    b = _locid.split("#");
                                                    _locguid = b[0];
                                                }
                                                
                                                // These should already exist as global variables from your app
                                                // If they don't, set them accordingly or remove if unused
                                                //var _gridid = window._gridid || "";
                                                //var _grididblock = window._grididblock || "";
                                                //var _locguidBlock = window._locguidBlock || "";

                                                var hhJSONURL = "";

                                                // Build WFS URL dynamically based on dropdown selection
                                                if (!_blockcode) {
                                                    if (_grididblock === "blockclick") {
                                                        hhJSONURL =
                                                            'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HH_View_FY&maxFeatures=5000&outputFormat=application%2Fjson' +
                                                        '&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode;
                                                    } else {
                                                        hhJSONURL =
                                                            'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HH_View_State_FY&maxFeatures=5000&outputFormat=application%2Fjson' +
                                                            '&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode;
                                                    }
                                                } else if (!_clusterid) {
                                                    if (_gridid === "clusterclick") {
                                                        hhJSONURL =
                                                            'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HH_View_Cluster_FY&maxFeatures=5000&outputFormat=application%2Fjson' +
                                                        '&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid;
                                                    } else {
                                                        hhJSONURL =
                                                            'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HH_View_FY&maxFeatures=5000&outputFormat=application%2Fjson' +
                                                            '&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode;
                                                    }
                                                } else {
                                                    if (_gridid === "villageclick") {
                                                        hhJSONURL =
                                                            'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HH_View_Village_FY&maxFeatures=5000&outputFormat=application%2Fjson' +
                                                        '&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid + ';vil:' + _locguid;
                                                    } else {
                                                        hhJSONURL =
                                                            'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_HH_View_Cluster_FY&maxFeatures=5000&outputFormat=application%2Fjson' +
                                                            '&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid;
                                                    }
                                                }

                                                // 🧹 Remove old HH markers if they exist
                                                if (HHmarkers) {
                                                    map.removeLayer(HHmarkers);
                                                    HHmarkers = null;
                                                }

                                                // Create MarkerClusterGroup for HH markers
                                                HHmarkers = new L.MarkerClusterGroup({
                                                    iconCreateFunction: function (cluster) {
                                                        var childCount = cluster.getChildCount();
                                                        return new L.DivIcon({
                                                            html: '<div style="background-color: rgba(253,156,115,0.7); border:2px solid black; width:35px; height:35px; display:flex; align-items:center; justify-content:center; font-weight:bold;">' + childCount + '</div>',
                                                            className: 'custom-cluster-icon'
                                                        });
                                                    }
                                                });

                                                // Fetch HH GeoJSON data
                    //                            $.getJSON(hhJSONURL, function (data) {
                    //                                L.geoJson(data, {
                    //                                    pointToLayer: function (feature, latlng) {
                    //                                        var popupContent = `
                    //<div>
                    //    <table class='table table-bordered' style='margin-bottom:0px !important;'>
                    //        <tr><td class='popuptablefont'>HH No:</td><td class='popuptablefont'>${feature.properties.HHNo}</td></tr>
                    //        <tr><td class='popuptablefont'>#OOSG:</td><td class='popuptablefont'>${feature.properties.OOSG}</td></tr>
                    //        <tr><td class='popuptablefont'>Location:</td><td class='popuptablefont'>${feature.properties.lat},<br/>${feature.properties.long}</td></tr>
                    //    </table>
                    //</div>`;

                    //                                        var LeafIcon = L.Icon.extend({ options: { iconSize: [20, 20] } });
                    //                                        var hhIcon = new LeafIcon({
                    //                                            iconUrl: feature.properties.EducationStatus === "DropOut"
                    //                                                ? 'images/criteria_village_Icon_2.png'
                    //                                                : 'images/criteria_village_Icon.png'
                    //                                        });

                    //                                        var hhMarker = L.marker(latlng, { icon: hhIcon });
                    //                                        hhMarker.bindPopup(popupContent, { maxWidth: 560 });

                    //                                        HHmarkers.addLayer(hhMarker);
                    //                                        return hhMarker;
                    //                                    }
                    //                                });

                    //                                // Add HH layer to map
                    //                                map.addLayer(HHmarkers);

                    //                                // Add to layer control dynamically
                    //                                if (layerControl) {
                    //                                    layerControl.addOverlay(HHmarkers, "Households");
                    //                                }
                    //                            });

                                                fetch('/GIS.aspx/GetGeoJson', {
                                                    method: 'POST',
                                                    headers: {
                                                        'Content-Type': 'application/json'
                                                    },
                                                    body: JSON.stringify({ url: hhJSONURL })
                                                })
                                                    .then(res => {
                                                        if (!res.ok) throw new Error('Server error');
                                                        return res.json();
                                                    })
                                                    .then(data => {
                                                        const geojson = JSON.parse(data.d);
                                                        L.geoJson(geojson, {
                                                            pointToLayer: function (feature, latlng) {
                                                                var popupContent = `
<div>
    <table class='table table-bordered' style='margin-bottom:0px !important;'>
        <tr><td class='popuptablefont'>HH No:</td><td class='popuptablefont'>${feature.properties.HHNo}</td></tr>
        <tr><td class='popuptablefont'>#OOSG:</td><td class='popuptablefont'>${feature.properties.OOSG}</td></tr>
        <tr><td class='popuptablefont'>Location:</td><td class='popuptablefont'>${feature.properties.lat},<br/>${feature.properties.long}</td></tr>
    </table>
</div>`;

                                                                var LeafIcon = L.Icon.extend({ options: { iconSize: [20, 20] } });
                                                                var hhIcon = new LeafIcon({
                                                                    iconUrl: feature.properties.EducationStatus === "DropOut"
                                                                        ? 'images/criteria_village_Icon_2.png'
                                                                        : 'images/criteria_village_Icon.png'
                                                                });

                                                                var hhMarker = L.marker(latlng, { icon: hhIcon });
                                                                hhMarker.bindPopup(popupContent, { maxWidth: 560 });

                                                                HHmarkers.addLayer(hhMarker);
                                                                return hhMarker;
                                                            }
                                                        });

                                                        // Add HH layer to map
                                                        map.addLayer(HHmarkers);

                                                        // Add to layer control dynamically
                                                        if (layerControl) {
                                                            layerControl.addOverlay(HHmarkers, "Households");
                                                        }
                                                    })
                                                    .catch(err => {
                                                        console.error('Fetch proxy error:', err);
                                                    });





                                            }



                                            function callStateMap(GrayLyr, StreetLyr, Terrain, ImageryLyr) {
                                                var state = 'Uttar Pradesh'
                                                // Example URL to GeoJSON data

                                                var SateJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3ASTATE_BOUNDARY&maxFeatures=5000&outputFormat=application%2Fjson';

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

                                                map.spin(true, spinnerOptions);
                                                fetch('/GIS.aspx/GetGeoJson', {
                                                    method: 'POST',
                                                    headers: { 'Content-Type': 'application/json' },
                                                    body: JSON.stringify({ url: SateJSONURL })
                                                })
                                                    .then(response => response.json())
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
                                                            "Cluster": VillageMap

                                                        };
                                                        window.layerControlCount = 0;


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
                                            }
                                            var click = 0;
                                            function bindClusterVillage(flag, locationid) {
                                                
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
                                                        VlgClusterJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_Cluster_View_Filter_FYNWS&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _locguid + ';vstatus:' + vstatus;
                                                    } else {
                                                        if (_blockcode == "" || _blockcode == null) {
                                                            VlgClusterJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_Cluster_FYNWS&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';vstatus:' + vstatus;
                                                        } else {
                                                            VlgClusterJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_Cluster_View_FYNWS&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';vstatus:' + vstatus;
                                                        }
                                                    }
                                                } else {
                                                    if (_gridid == "villageclick") {
                                                        VlgClusterJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_Cluster_View_Filter_Village_FYNWS&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid + ';vil:' + _locguid + ';vstatus:' + vstatus;
                                                    } else {
                                                        VlgClusterJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_Cluster_View_Filter_FYNWS&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid + ';vstatus:' + vstatus;
                                                    }
                                                }
                                                console.log("cul_" + VlgClusterJSONURL);
                                                resetVillageLayer();

                                                /////////////////////////Cluster//////////////
                                                // Fetch GeoJSON data using fetch API
                                                //VillageMap = L.layerGroup();
                                                fetch('/GIS.aspx/GetGeoJson', {
                                                    method: 'POST',
                                                    headers: { 'Content-Type': 'application/json' },
                                                    body: JSON.stringify({ url: VlgClusterJSONURL })
                                                })
                                                    .then(response => response.json())
                                                    .then(data => {
                                                        const geojson = JSON.parse(data.d);
                                                        // Create a GeoJSON layer and add it to the map
                                                        VillageMap = new L.geoJson(geojson, {
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
                                                        fillColor: feature.properties.colorCode,
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
                                                    
                                                    layer.bindTooltip("<b style='color: #2954A2;font-size: 12px;'>Cluster: " + feature.properties.ClusterName + "<br/> Village: " + feature.properties.villageName + "<br/> Village Operational Status: " + feature.properties.OperationalStatus + "</b>",
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
                                            function call_function(flag, locationid) {
                                                
                                                District_Map.clearLayers();
                                                BlockMap.clearLayers();
                                                VillageMap.clearLayers();
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
                                                bindDistrict(Fyear, DistrictID);
                                                bindBlock(flag, locationid);
                                                blk = $("[id$=ddlBlock]").val();
                                                if (blk != "") {
                                                    bindClusterVillage(flag, locationid);
                                                }


                                                if (window.layerControl) {
                                                    map.removeControl(window.layerControl); // Remove the old layer control
                                                }
                                                initializeBaseLayers();

                                                callStateMap(GrayLyr, StreetLyr, Terrain, ImageryLyr);
                                                bindSchools(flag, locationid);
                                                bindHHLayer(flag, locationid);
                                            }
                                            
                                            function refreshLayerControl() {
                                                // Remove the existing layer control from the map
                                                if (window.layerControl) {
                                                    map.removeControl(window.layerControl);
                                                }

                                                // Re-initialize the layer control with updated overlayMaps
                                                window.layerControl = L.control.layers(null, overlayMaps).addTo(map);
                                            }

                                            function callSerchType(flag, locationid) {
                                                var searchtype = $("[id$=ddlSearchType]").val();
                                                if (searchtype == "1") {
                                                    bindSchools(flag, locationid);
                                                }
                                                if (searchtype == "2") {
                                                    bindClusterVillage(flag, locationid);
                                                }
                                                if (searchtype == "3") {
                                                    goto_lat_long();
                                                }

                                            }
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
                <asp:Panel ID="pnl_alert" runat="server" Style="display: none; background-color: #fff; border: 1px solid transparent; border-radius: 4px;" class="modalPopup alert-pop-main panel-default">
                    <div class="alert-pop-body">
                        <div class="header">
                            <asp:Label ID="lbl_PopUpMessages" runat="server" CssClass="LabelHeader" Font-Bold="True"></asp:Label>
                        </div>
                        <div class="body">
                            <h4>
                                <asp:Label ID="lbl_messages" runat="server" CssClass="LabelHeader" Font-Bold="True"></asp:Label>
                            </h4>
                            <div class="text-center">
                                <asp:Button ID="btn_cancelalert" runat="server" CssClass="myButton" Text="  OK  " />
                            </div>
                        </div>
                    </div>
                    <%--          <div class="footerCategory" align="right">  </div>--%>
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

