<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Culture="en-GB" CodeFile="GISEGBlock.aspx.cs" Inherits="GIS" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <!-- Bootstrap + Leaflet -->
    <%--<link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.4/dist/leaflet.css" />--%>
    <link rel="stylesheet" href="css/CSS/leaflet.css" />
    <%--<link rel="stylesheet" href="https://unpkg.com/@geoman-io/leaflet-geoman-free@2.14.0/dist/leaflet-geoman.css" />--%>
    <link rel="stylesheet" href="css/CSS/leaflet-geoman.css" />
    <script src="Scripts/comman.js" type="text/javascript"></script>

    <style>
        .form-group {
            margin-bottom: 5px;
        }

        html, body {
            height: 100%;
            margin: 0;
            padding: 0;
        }

        /* Scrollable layer container */
        #layerList {
            max-height: 320px;
            overflow-y: auto;
            padding: 8px;
            border-radius: 10px;
            background: #f5f7fa;
            box-shadow: inset 0 0 8px rgba(0,0,0,0.08);
        }

        .leaflet-control-container {
            z-index: 9999 !important;
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

        .container-fluid {
            height: 100%;
        }

        .row.no-gutter {
            height: 100%;
            margin: 0 !important;
        }

        .col-sm-3,
        .col-sm-9 {
            height: 100%;
            padding: 0;
        }

        .sidebar {
            height: 100%;
            overflow-y: auto;
            background: #f8f8f8;
            border-right: 1px solid #ccc;
            padding: 0px 5px 10px 15px;
        }

        #map {
            height: 100%;
            width: 100%;
            background: #ddd;

        }

        .color-box {
            width: 18px;
            height: 18px;
            display: inline-block;
            border: 1px solid #999;
            border-radius: 3px;
        }

        .mode-btn {
            width: 48%;
            margin-bottom: 5px;
        }

        .card {
            background: #fff;
            border: 1px solid #ddd;
            border-radius: 4px;
            padding: 10px;
            margin-bottom: 12px;
        }

        .pm-disabled {
            opacity: 0.3;
            pointer-events: none;
        }

        /* Loader overlay INSIDE the map */
        .map-loader-overlay {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            background: rgba(255,255,255,0.8);
            padding: 30px 40px;
            border-radius: 12px;
            display: none;
            align-items: center;
            justify-content: center;
            flex-direction: column;
            z-index: 9999;
            box-shadow: 0 0 10px rgba(0,0,0,0.15);
        }

        /* Spinner circle */
        .map-loader-spinner {
            width: 55px;
            height: 55px;
            border: 6px solid #ddd;
            border-top-color: #3498db;
            border-radius: 50%;
            animation: spin 1s linear infinite;
        }

        /* Text below spinner */
        .map-loader-text {
            margin-top: 12px;
            font-size: 15px;
            font-weight: 600;
            color: #333;
        }

        /* Spin animation */
        @keyframes spin {
            from {
                transform: rotate(0deg);
            }

            to {
                transform: rotate(360deg);
            }
        }
    </style>
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
            /*min-height: 384px;*/
            min-height: calc(100vh - 263px);
            width: 100%;
            border-radius: 6px;
            border: 1px solid #edf2fe;
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

        /*  .leaflet-control-zoom.leaflet-bar.leaflet-control {
            display: none;
        }*/


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
    <style type="text/css">
        /* Scrollable EG Block area */
        #groupList {
            max-height: 250px; /* You can increase/decrease */
            min-height: 150px; /* Minimum height */
            overflow-y: auto; /* Scroll enabled */
            border: 1px solid #ddd;
            padding: 5px;
            background: #f8f8f8;
        }

        /* Make full layout use height */
        #map {
            height: auto; /* Full height map */
            width: 100%;
        }
    </style>
    <style>
        /* .page-header-bar {
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

    .page-subtitle {
        font-size: 13px;
        opacity: 0.8;
    }

    .page-body {
        margin-top: 10px;
    }*/

        .layer-dropdown {
            position: relative;
            width: 200px;
            font-family: Arial, sans-serif;
        }

        .layer-btn {
            width: 100%;
            padding: 8px 10px;
            border: 1px solid #ccc;
            background: white;
            cursor: pointer;
            text-align: left;
        }

        .layer-list {
            display: none; /* 🔴 hidden by default */
            position: absolute;
            top: 100%;
            left: 0;
            width: 100%;
            background: white;
            border: 1px solid #ccc;
            box-shadow: 0 4px 10px rgba(0,0,0,.15);
            z-index: 999;
        }

        .layer-item {
            padding: 8px 10px;
            cursor: pointer;
        }

            .layer-item:hover {
                background: #f1f1f1;
            }


        .info-panel {
            position: absolute;
            right: 16px;
            top: 16px; /* use top instead of bottom */
            width: 260px;
            background: #ffffff;
            border-radius: 10px;
            padding: 12px;
            z-index: 3000;
            box-shadow: 0 6px 18px rgba(0,0,0,.25);
            font-family: "Segoe UI", sans-serif;
            font-size: medium;
        }

        #infoPanel.visible {
            opacity: 1;
            transform: translateY(0);
            pointer-events: auto;
        }

        .info-header {
            display: flex;
            align-items: center;
            padding: 10px 12px;
            border-bottom: 1px solid #eee;
            font-weight: 600;
        }

        .color-dot {
            width: 14px;
            height: 14px;
            border-radius: 50%;
            margin-right: 8px;
            border: 1px solid #999;
        }

        .info-title {
            font-size: 14px;
            color: #222;
        }

        .info-table {
            width: 100%;
            border-collapse: collapse;
        }

            .info-table th {
                font-size: 12px;
                color: #666;
                padding: 6px 10px;
                text-align: left;
            }

            .info-table td {
                font-size: 13px;
                padding: 6px 10px;
                font-weight: 600;
            }

        @media (max-width: 600px) {
            #infoPanel {
                width: 90%;
                left: 5%;
                right: 5%;
                bottom: 10px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row">
        <!-- ================= HEADER ================= -->
        <div class="col-sm-12">
            <div class="panel panel-default" style="background: linear-gradient(to bottom,  #ffffff 1%,#ffffff 1%,#ebf1fd 100%) !important; margin-bottom: 8px;">
                <div class="panel-heading" style="background-color: transparent; padding: 0px 5px;">

                    <div class="row" style="margin-left: -15px; margin-right: -15px">
                        <div class="col-sm-12">
                            <div style="display: flex; justify-content: space-between; align-items: center; flex-flow: wrap">
                                <span id="ctl00_MainContent_lblMain" style="margin: 3px 0px 5px 5px; font-weight: bold; font-size: medium;">Map Digitization </span>

                                <button type="button" id="btnexportMapped" style="margin-left: auto;" class="btn-link" onclick="ExportMapped();">Export Mapping Data to Excel</button>
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
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12" style="padding-right: 0px;">
                            <div class="form-group">
                                <label><b>Layer Type</b></label><span class="mandatory-label"></span>

                                <select id="layerDropdown" class="form-control">
                                    <option value="">-- Select Layer --</option>
                                    <option value="3">EG District</option>
                                    <option value="4">EG Block</option>
                                    <option value="5">Cluster</option>
                                </select>


                                <button id="layerBtn" type="button" class="btn btn-light w-100 text-start" style="display: none;">
                                    Select Layer ▾
                                </button>

                                <div id="layerList" class="layer-list" style="display: none;">
                                    <div class="layer-item" data-layer="3">EG District</div>
                                    <div class="layer-item" data-layer="4">EG Block</div>
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12" style="padding-right: 0px;">
                            <div class="form-group">

                                <label><b>Year</b></label><span class="mandatory-label"></span>
                                <select id="YearSelect" class="form-control">
                                    <option value="">-- Select Year --</option>
                                </select>




                            </div>

                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12" style="padding-right: 0px;">
                            <div class="form-group">
                                <label><b>State</b></label><span class="mandatory-label"></span>
                                <select id="stateSelect" class="form-control">
                                    <option value="">-- Select State --</option>
                                </select>
                            </div>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <label><b>District</b></label><span class="mandatory-label"></span>
                                <select id="districtSelect" class="form-control">
                                    <option value="">-- Select District --</option>
                                </select>



                                <div id="drdist" class="dropdown w-100" style="display: none;">

                                    <button class="btn btn-light w-100 text-start dropdown-toggle"
                                        type="button"
                                        id="districtBtn">
                                        Select Districts
                                    </button>

                                    <div id="districtPanel" class="dropdown-menu district-menu p-2" style="max-height: 180px; overflow: auto;">
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <label id="lblblock1" style="display: none;"><b>Block</b><span class="mandatory-label"></span></label>
                                <select id="groupDropdown"  class="form-control">
                                    <option value="">-- Select Block --</option>
                                </select>
                            
                                  <label id="lblblock"  style="display: none;"><b>Block</b><span class="mandatory-label"></span></label>
                                <select id="blockSelect" class="form-control" style="display: none;">
                                    <option value="">-- Select Block --</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <label id="lblCluster"><b>Cluster</b><span class="mandatory-label"></span></label>
                                <select id="clusterDropdown" class="form-control">
                                    <option value="">-- Select Cluster --</option>
                                </select>

                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div style="display: flex; justify-content: flex-end; align-items: center; gap: 10px">
                                <button type="button" id="exportBtn" class="btn btn-info btn-block1">
                                    <span class="glyphicon glyphicon-floppy-disk"></span> Save
                                </button>
                                <button type="button" id="resetLayerBtn1" class="btn btn-warning btn-block1">
                                    <span class="glyphicon glyphicon-repeat"></span> Reset
                                </button>
                                <button type="button" id="deleteLayerBtn" class="btn btn-danger btn-block1">
                                   <span class="glyphicon glyphicon-trash"></span> Delete Layer
                                </button>
                            </div>
                        </div>
                       <%-- <div class="col-lg-1 col-md-1 col-sm-6 col-xs-12" id="divexportBtn">
                            <div class="form-group">
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6 col-xs-12" id="divresetLayerBtn1">
                            <div class="form-group">
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6 col-xs-12" id="divdeleteLayerBtn">
                            <div class="form-group">
                            </div>
                        </div>--%>
                    </div>

                </div>
                <div>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div class="row no-gutter">

            <!-- LEFT PANEL -->
            <div class="col-sm-3 sidebar">

                <%--<div class="card" style="padding:10px; margin-top:10px;">
  <h5><b>Upload Shapefile (.zip)</b></h5>
  <input type="file" style="padding:3px" id="fileInput" accept=".zip" class="form-control">
</div>--%>

                <div class="card" style="padding: 10px; display: none;">

                    <div class="card" style="margin-top: 10px; padding: 10px;display: none;">
                        <h5 id="EGListHeading"><b>EG Blocks</b></h5>

                        <!-- Scrollable EG block list -->
                        <%-- <div id="groupList"></div>--%>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 8px; display: none">

                    <div class="col-xs-6" style="padding-right: 5px;">
                    </div>

                    <div class="col-xs-4" style="padding-left: 5px; display: none">
                        <button style="margin-top: 5px" type="button" id="publishBtn" class="btn btn-success btn-block">
                            <span class="glyphicon glyphicon-upload"></span>Publish
                        </button>
                    </div>
                    <div class="col-xs-6" style="padding-left: 5px;">
                    </div>
                </div>
                <div class="card text-center" style="padding: 12px; margin-top: 0px;">

                    <!-- ROW: CLICK + DRAW -->
                    <div class="row" style="margin-bottom: 0px;">

                        <div class="col-xs-6" style="padding-right: 5px;">
                            <button type="button" id="clickModeBtn" style="width: 100%" class="btn btn-info btn-block mode-btn1">
                                <span class="glyphicon glyphicon-hand-up"></span>Select
                            </button>
                        </div>

                        <div class="col-xs-6" style="padding-left: 5px;">
                            <button type="button" id="drawModeBtn" class="btn btn-warning btn-block mode-btn1" style="width: 100%">
                                <span class="glyphicon glyphicon-th"></span>Select Area
                            </button>
                        </div>

                    </div>

                </div>




                <div class="card" style="padding: 10px; margin-top: 10px;">
                    <h5 style="display: none"><b>Hover Attribute</b></h5>
                    <select style="display: none" id="attributeSelect" class="form-control">
                        <option value="">-- Select Attribute --</option>
                    </select>
                    <!-- ROW: CLICK + DRAW -->

                    <div class="row" style="margin-bottom: 8px;">

                        <div class="col-xs-6" style="padding-right: 5px;">
                            <!-- FULL-WIDTH MERGE MODE BUTTON -->
                            <button type="button" id="selectGroupPolygonsBtn" class="btn btn-success btn-block">
                                <span class="glyphicon glyphicon-retweet"></span>&nbsp; Select All 
                               
                            </button>
                            <button type="button" id="mergeModeBtn" class="btn btn-info btn-block" style="display: none;">
                                <span class="glyphicon glyphicon-scissors"></span> Merge Mode
                            </button>
                        </div>

                        <div class="col-xs-6" style="padding-left: 5px;">
                            <!-- MERGE SELECTED -->
                            <button type="button" id="resetLayerBtn" class="btn btn-info btn-block">
                                <span class="glyphicon glyphicon-remove"></span> Unselect All
                            </button>

                        </div>

                    </div>
                    <div class="row" style="margin-bottom: 0px;">

                        <div class="col-xs-6" style="padding-right: 5px;">
                            <button type="button" id="mergeNowBtn" class="btn btn-warning btn-block">
                                <span class="glyphicon glyphicon-plus"></span> MERGE 
                            </button>
                        </div>

                        <div class="col-xs-6" style="padding-left: 5px;">
                            <button type="button" id="undoMergeBtn" class="btn btn-danger btn-block">
                                <span class="glyphicon glyphicon-minus"></span> Undo MERGE  
                            </button>
                        </div>

                    </div>


                </div>

            </div>


            <!-- MAP IN CENTER -->
            <div class="col-sm-9" style="padding-right: 15px;">
                <div id="map">
                    <div id="infoPanel" class="info-panel">
                        <b>Hover over a feature</b>
                    </div>
                    <div id="mapLoader" class="map-loader-overlay">
                        <div class="map-loader-spinner"></div>
                        <div class="map-loader-text">Loading map...</div>
                    </div>
                </div>
            </div>




        </div>
    </div>
    <%-- <div id="mapLoader" class="loader-overlay">
    <div class="loader-spinner"></div>
    <div class="loader-text">Loading map...</div>
</div>--%>


    <!-- jQuery (must be first) -->
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.0/jquery.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/jquery.min.js"></script>

    <%--<!-- Bootstrap -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>--%>

    <!-- Leaflet -->
    <%--<script src="https://unpkg.com/leaflet@1.9.4/dist/leaflet.js"></script>--%>
    <script type="text/javascript" src="js/JS/leaflet.js"></script>

    <!-- Leaflet Geoman -->
    <%--<script src="https://unpkg.com/@geoman-io/leaflet-geoman-free@2.14.0/dist/leaflet-geoman.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/leaflet-geoman.min.js"></script>

    <!-- Turf -->
    <%--<script src="https://unpkg.com/@turf/turf@6.5.0/turf.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/turf.min.js"></script>

    <!-- JSZip (LOCKED VERSION) -->
    <%--<script src="Scripts/jszip-2.7.0.min.js"></script>--%>

    <%--<!-- shp-write (GeoJSON → Shapefile) -->
<script src="Scripts/shpwrite.js"></script>--%>
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.1/jszip.min.js"></script>--%>
    
    <script type="text/javascript" src="js/JS/jszip.min.js"></script>

    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.7.0/jszip.min.js"></script>--%>
    <%--<script src="https://cdn.jsdelivr.net/gh/mapbox/shp-write@0.2.3/shpwrite.js"></script>--%>
    <script type="text/javascript" src="js/JS/shpwrite.js"></script>

    <!-- Optional helpers -->
    <%--<script src="https://cdn.jsdelivr.net/npm/rbush@3.0.1/rbush.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/rbush.min.js"></script>
    <%--<script src="https://unpkg.com/terraformer@1.0.12"></script>--%>
    <script type="text/javascript" src="js/JS/terraformer.js"></script>
    <%--<script src="https://unpkg.com/terraformer-wkt-parser@1.2.1"></script>--%>
    <script type="text/javascript" src="js/JS/terraformer-wkt-parser.js"></script>

    <link href="leaflet2/leaflet.fullscreen.css" rel="stylesheet" type="text/css" />
    <script src="leaflet2/Leaflet.fullscreen.js" type="text/javascript"></script>




    <script type="text/javascript">

    let activeGroupId = null;
    let activeGroupName = null;
    //let selectedBlockColor = null;
    let activeDistrictId = null;
    let activeBlockId = null;
    let selectedColor = null;
    let activeEGDistrict = null;
    let activeEGBlock = null;
    let ismappeddistrict = null;
    let isdigitalize = null;
    /* ===========================================================
       CORE VARIABLES
    ===========================================================*/
    //var map = L.map('map', {
    //    center: [23.5, 78.5],
    //    zoom: 5,
    //    zoomControl: false
    //});
    var map = L.map('map', {
        maxZoom: 18,
        minZoom: 4.3,
        dragging: true,
        fullscreenControl: { pseudoFullscreen: false }
    }).setView([23.5, 78.5], 4.3);   // <-- India default view


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

    // Add OpenStreetMap tiles
    /*L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png").addTo(map);*/

    /* Add Zoom Control (Visible Now) */
    //L.control.zoom({
    //    position: "topleft"
    //}).addTo(map);

    //L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png").addTo(map);

    var geoData;
    var fullLayer;

    var groups = {};
    var activeGroup = null;
    var selectedAttribute = "";
    var currentMode = "click";

    var rtree = new RBush();
    var shapesIndex = {};
    var mergeSelection = [];   // <<---- NEW FOR MERGE MODE
    let activeMergeGroupName = null;
    let mergeUndoStack = [];
    let deletedFeatureBackup = {};   // 🔑 needed for undo
    /* ===========================================================
       MAP CONTROLS
    ===========================================================*/
    //map.pm.addControls({
    //    position: "topright",
    //    drawMarker: false,
    //    drawCircle: false,
    //    drawCircleMarker: false,
    //    drawPolyline: false,
    //    editMode: false,
    //    dragMode: false
    //});

    function disableDrawButtons() {
        document.querySelectorAll(".leaflet-pm-icon-polygon,.leaflet-pm-icon-rectangle")
            .forEach(b => b.classList.add("pm-disabled"));
    }
    function enableDrawButtons() {
        document.querySelectorAll(".leaflet-pm-icon-polygon,.leaflet-pm-icon-rectangle")
            .forEach(b => b.classList.remove("pm-disabled"));
    }
    function resetPM() {
        map.pm.disableDraw();
        map.pm.disableGlobalEditMode();
        map.pm.disableGlobalDragMode();
        map.pm.disableGlobalRemovalMode();
    }
    $("#clickModeBtn").on("click", function (e) {
        e.preventDefault();
        e.stopPropagation();

        resetPM();
        currentMode = "click";

        disableDrawButtons();

        console.log("MODE = CLICK");

        if (!fullLayer) return;

        //fullLayer.eachLayer(layer => {
        //    const f = layer.feature;
        //    if (!f || !f.properties) return;

        //    const id = f.properties._id;
        //    const isSelected = mergeSelection.includes(id);

        //    // 🟩 SELECTED → DO NOTHING
        //    if (isSelected) {
        //        layer.setStyle({
        //            weight: 5,
        //            //dashArray: "5,5"
        //            dashArray: null
        //        });
        //        return;
        //    }

        //    // 🟦 NOT SELECTED → reset to normal
        //    layer.setStyle({
        //        color: "#666666",
        //        weight: 1,
        //        dashArray: null,
        //        fill: true,
        //        fillColor: f.properties.color || "#666666",
        //        fillOpacity: 0.3
        //    });
        //});

        //// Reapply colors from feature properties
        //if (fullLayer) {
        //    fullLayer.eachLayer(layer => {
        //        const f = layer.feature;
        //        if (!f || !f.properties) return;

        //        const isSelected = mergeSelection.includes(f.properties._id);
        //        console.log("isSelected", isSelected)
        //        layer.setStyle({
        //            color: selectedBlockColor || "#666666",      // boundary color
        //            weight: isSelected ? 3 : 1,                  // thicker if selected
        //            dashArray: null,                              // solid line
        //            fill: true,
        //            fillColor: selectedBlockColor || "#666666",  // fill same as boundary
        //            fillOpacity: isSelected ? 0.4 : 0.2          // lighter if not selected
        //        });
        //    });
        //}
    });

    $("#drawModeBtn").on("click", function (e) {
        e.preventDefault();
        e.stopPropagation();

        resetPM();
        currentMode = "draw";

        enableDrawButtons();

        // 🔥 IMPORTANT: enable AFTER reset
        map.pm.enableDraw("Rectangle", {
            snappable: false,
            allowSelfIntersection: false
        });

        console.log("MODE = DRAW");
    });
    $("#mergeModeBtn").click(e => {
        e.preventDefault();
        currentMode = "merge";
        map.pm.disableDraw();

        if (mergeSelection.length === 0) {
            alert("No polygons selected. Use Draw or Click first.");
        }
    });

    function addGroup(name, color, id, EGBlock, isMapped) {

        if (!name || groups[id]) return;

        groups[id] = { name, color, EGBlock, features: [] };

        $("#groupDropdown").append(
            `<option 
            value="${id}" 
            data-color="${color}" 
            data-egblock="${EGBlock}"
            data-ismapped="${isMapped}">
            ${name}
        </option>`
        );
    }

    function addClusterGroup(name, color, id, EGBlock, isMapped) {

        if (!name || groups[id]) return;

        groups[id] = { name, color, EGBlock, features: [] };

        $("#clusterDropdown").append(
            `<option 
    value="${id}" 
    data-color="${color}" 
    data-egblock="${EGBlock}"
    data-ismapped="${isMapped}">
    ${name}
</option>`
        );
    }

    $("#addGroupBtn").click(() => {
        addGroup($("#newGroupName").val(), $("#newGroupColor").val());
        $("#newGroupName").val("");
    });

    //addGroup("EG Block 1", "#ff0000");
    //addGroup("EG Block 2", "#007bff");


    /* ===========================================================
       SPATIAL INDEX
    ===========================================================*/
    function indexAllFeatures() {
        rtree.clear();
        shapesIndex = {};

        geoData.features.forEach(f => {
            if (!f.properties._id)
                f.properties._id = Math.random().toString(36).substr(2, 9);

            let bbox = turf.bbox(f);
            rtree.insert({
                minX: bbox[0], minY: bbox[1],
                maxX: bbox[2], maxY: bbox[3],
                id: f.properties._id
            });

            shapesIndex[f.properties._id] = f;
        });
    }

    /* ===========================================================
       FILTERS: STATE → DISTRICT
    ===========================================================*/

    $(document).ready(function () {
        $("#deleteLayerBtn").hide();
        $("#FilterDiv").hide();
        loadYear();        
        $("#groupDropdown").hide();
        $("#clusterDropdown").hide();
        $("#lblCluster").hide();
        $("#lblblock").hide();
        $("#lblblock1").hide();
        $("#drdist").hide();
        $("#stateSelect").change(function () {
            loadDistricts($(this).val());
            $("#groupList").html(""); // Clear EG Block list
        });
        $("#YearSelect").change(function () {
            loadStates($(this).val());
            $("#groupList").html(""); // Clear EG Block list
        });

        

        //$("#districtSelect").change(function () {
        //    loadBlocks($(this).val());
        //});


        //let activeDistrictId = null;
        $("#districtSelect").on("change", function () {

            activeDistrictId = $(this).val();

            const opt = $("#districtSelect").find(":selected");
            selectedColor = opt.data("color");
            selectedBlockColor = selectedColor;

            if (!activeDistrictId) return;

            console.log("District selected:", activeDistrictId);

            // Reset Block dropdown & selection
            $("#groupDropdown").empty()
                .append(`<option value="">-- Select Block --</option>`);

            activeGroupId = null;
            mergeSelection = [];

            if (fullLayer) {
                map.removeLayer(fullLayer);
                fullLayer = null;
            }

            // 🔹 THIS WAS MISSING
            loadBlocks(activeDistrictId);
        });





        $("#layerDropdown").on("change", function () {

            const layer = $(this).val();
            if (!layer) return;

            layertype = parseInt(layer);

            console.log("Layer selected:", layertype);

            // Reset everything
            activeDistrictId = null;
            activeGroupId = null;
            mergeSelection = [];

            //$("#districtSelect").empty()
            //    .append(`<option value="">-- Select District --</option>`);

            $("#groupDropdown").empty()
                .append(`<option value="">-- Select Block --</option>`);

            if (fullLayer) {
                map.removeLayer(fullLayer);
                fullLayer = null;
            }

            // Toggle UI
            if (layertype == 3) {
                $("#groupDropdown").hide();
                $("#lblblock").hide();
                $("#lblblock1").hide();
                $("#clusterDropdown").empty()
                    .append(`<option value="">-- Select Cluster --</option>`);
                $("#lblCluster").hide();
                $("#clusterDropdown").hide();
                $("#districtSelect").empty()
                    .append(`<option value="">-- Select District --</option>`);
                var state = $("#stateSelect").val();
                loadDistricts(state);
                //$("#districtSection").show();
                $("#blockSelect").hide();
            }
            else if (layertype == 4) {
                $("#groupDropdown").empty()
                    .append(`<option value="">-- Select Block --</option>`);
                var state = $("#stateSelect").val();
                loadDistricts(state);

                var dist = $("#districtSelect").val();
                loadBlocks(dist);

                $("#lblblock").hide();
                $("#groupDropdown").show();

                $("#clusterDropdown").empty()
                    .append(`<option value="">-- Select Cluster --</option>`);
                $("#lblCluster").hide();
                $("#clusterDropdown").hide();
                $("#blockSelect").hide();
                $("#lblblock1").show();
                //$("#districtSection").show();
            }
            else if (layertype == 5) {

                $("#groupDropdown").empty()
                    .append(`<option value="">-- Select Block --</option>`);
                var state = $("#stateSelect").val();
                loadDistricts(state);
                var dist = $("#districtSelect").val();
                loadBlocks(dist);

                $("#lblblock").hide();
                $("#groupDropdown").show();

                $("#clusterDropdown").empty()
                    .append(`<option value="">-- Select Cluster --</option>`);
                var block = $("#groupDropdown").val();
                //loadCluster(block);

                $("#lblCluster").show();
                $("#clusterDropdown").show();
                $("#blockSelect").hide();
                $("#lblblock1").show();
                //$("#districtSection").show();
            }

            // 🔹 THIS is what was missing
            //loadDistricts(dist);
        });


        $('#layerBtn').on('click', function (event) {
            event.stopPropagation();
            $('#layerList').toggle();
        });
        $('#districtBtn').on('click', function (e) {
            e.stopPropagation();
            $('.district-menu').toggle();
        });
    });
    $(document).click(function (e) {
        if (!$(e.target).closest('#layerBtn, #layerList').length) {

        }
    });

    function ExportMapped() {

        var layertype = $('#layerDropdown').val();
        var fyear = $('#YearSelect').val();
        var state = $('#stateSelect').val();
        var district = $('#districtSelect').val();
        var block = $('#groupDropdown').val();
        var cluster = $('#clusterDropdown').val();


        if (!layertype) { alert("please select layer type"); return; }
        if (!fyear) { alert("please select year"); return; }
        if (!state) { alert("please select state"); return; }
        if (layertype == '4') {
            if (!district) { alert("please select district"); return; }
            if (!block) { alert("please select block"); return; }
        }
        else if (layertype == '3') {
            if (!district) { alert("please select district"); return; }
        }
        else if (layertype == '3') {
            if (!district) { alert("please select district"); return; }
            if (!block) { alert("please select block"); return; }
            if (!cluster) { alert("please select cluster"); return; }
        }




        $(".update_overlay").show();

        $.ajax({
            type: "POST",
            url: "GISEGBlock.aspx/ExportMappedData",
            data: JSON.stringify({ fyear: fyear, district: district, block: block, cluster: cluster }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",

            success: function (res) {
                console.log(res);

                if (res.d === "READY") {
                    window.location.href = "GISEGBlock.aspx?download=1";
                } else {
                    alert("No data found.");
                }
            },

            error: function (err) {
                console.error(err);
                alert("Something went wrong while processing.");
            },

            complete: function () {
                $(".update_overlay").hide();
            }
        });
    }

        //----------Bind masters

        function Fill_FYear_NextFY(ddlID) {

            var objvr = {};
            objvr.ValidID = "";

            _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_FYear_NextFY", "", objvr, true);
        }
        function Fill_State(ddlID,Year) {

            var objvr = {};
            objvr.ValidID = Year;

            _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_State", "", objvr, true);
        }
        function Fill_District(ddlID,stateid) {
            var FYear = $("[id$=YearSelect] option:selected").text();
            var StateID = stateid;
            var objvr = {};
            objvr.ValidID = FYear;
            objvr.ValidID1 = StateID;

            _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_District2026", "Select", objvr, true);
        }
        function Fill_Block(ddlID, Distid) {
            var FYear = $("[id$=YearSelect] option:selected").text();
            var StateID = $("[id$=stateSelect]").val();
            var d = Distid;
            var did = "";
            var DistrictID = "";
            if (d && d.length > 10) {
                did = $("[id$=districtSelect]").val().split("#");
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
        function Fill_Cluster(ddlID,Blkid) {
            var FYear = $("[id$=YearSelect] option:selected").text();
            var StateID = $("[id$=districtSelect]").val();
            var d = $("[id$=districtSelect]").val();
            var did = "";
            var DistrictID = "";
            if (d && d.length > 10) {
                did = $("[id$=districtSelect]").val().split("#");
                DistrictID = did[0];
            }
            else {
                DistrictID = d || "";
            }

            var b = $("[id$=blockSelect]").val();
            var bid = "";
            var BlockID = "";
            if (b && b.length > 10) {
                bid = $("[id$=blockSelect]").val().split("#");
                BlockID = bid[0];
            }
            else {
                BlockID = b || "";
            }
            var objvr = {};
            objvr.ValidID = FYear;
            objvr.ValidID1 = StateID;
            objvr.ValidID2 = DistrictID;
            objvr.ValidID3 = BlockID;

            _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_Cluster_cluster2025", "All", objvr, true);
        }

    // ----------------------------
    // Load States
    // ----------------------------
    function loadYear() {
        $.ajax({
            type: "POST",
            url: "GISEGBlock.aspx/GetYear",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                let data = JSON.parse(result.d);
                $("#YearSelect").empty().append(`<option value="0">-- Select Year --</option>`);

                data.forEach(r => {
                    $("#YearSelect").append(`<option value="${r.Fyear}">${r.Fyear}</option>`);
                });
                //$('[id$=YearSelect]').val("2025-2026");
                //loadStates();
            }
            
        });
       
    }

    // ----------------------------
    // Load States
    // ----------------------------
    function loadStates() {
        let yearselect = $("#YearSelect").val();
        let fyear = yearselect.substring(0, 4);
        Fill_State("stateSelect", fyear);
       
        <%--$.ajax({
            type: "POST",
            url: "GISEGBlock.aspx/GetStates",
            data: JSON.stringify({ ValidID: fyear }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                let data = JSON.parse(result.d);
                $("#stateSelect").empty().append(`<option value="0">-- Select State --</option>`);

                data.forEach(r => {
                    $("#stateSelect").append(`<option value="${r.StateCode}">${r.StateName}</option>`);
                });
                <%--var FYear = $("[id$=YearSelect] option:selected").text();
                var UserlevelRole = '<%= Session["user_level_Role"] %>';
                if (FYear == '2025-2026' && UserlevelRole == '1') {
                    $('[id$=stateSelect]').val("9A");
                }
                loadDistricts($("#stateSelect").val());//comment this 
            }
        });--%>
        loadDistricts($("#stateSelect").val());
    }

    // ----------------------------
    // Load Districts
    // ----------------------------
    function loadDistricts(stateId) {
        let layertype = $("#layerDropdown").val();
        $.ajax({
            type: "POST",
            url: "GISEGBlock.aspx/GetDistricts",
            data: JSON.stringify({ stateId: stateId, Fyear: $("#YearSelect").val(), layertype: layertype }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                let data = JSON.parse(result.d);
                if (layertype == 4 || layertype == 5) {
                    //$("#districtSelect").show();
                    $("#drdist").hide();
                    $("#districtSelect").empty().append(`<option value="0">-- Select District --</option>`);

                    data.forEach(r => {
                        $("#districtSelect").append(
                            `<option value="${r.DistrictId}">${r.DistrictName}</option>`
                        );
                    });

                    $("#groupList").html(""); // clear EG block list
                }
                else if (layertype == 3) {

                    //$("#drdist").show();
                    $("#districtSelect").empty()
                        .append(`<option value="">-- Select District --</option>`);

                    data.forEach(d => {
                        $("#districtSelect").append(
                            `<option 
    value="${d.DistrictId}" 
    data-color="${d.color}" 
    data-egdistrict="${d.EGDistrictCode}"
    data-ismappeddistrict="${d.ismapped}">
    ${d.DistrictName}
</option>`
                        );
                        //$("#districtSelect").append(
                        //    `<option value="${d.DistrictId}">${d.DistrictName}</option>`
                        //);
                    });

                    // Reset everything
                    groups = {};
                    activeGroup = null;
                    usedColors.clear();
                    mergeSelection = [];
                }
            }
        });
    }
        //function loadDistricts(stateId) {
        //    Fill_District("districtSelect", stateId);
        //    if (layertype == 4 || layertype == 5) {
        //        //$("#districtSelect").show();
        //        $("#drdist").hide();
                

        //        $("#groupList").html(""); // clear EG block list
        //    }
        //    else if (layertype == 3) {

        //        groups = {};
        //        activeGroup = null;
        //        usedColors.clear();
        //        mergeSelection = [];
        //    }

                   
        //}
    let usedColors = new Set();

    function getUniqueRandomColor() {
        let color;

        do {
            color = "#" + Math.floor(Math.random() * 16777215)
                .toString(16).padStart(6, "0");
        } while (usedColors.has(color));

        usedColors.add(color);
        return color;
    }
    // ----------------------------
    // Load EG BLOCKS as RADIO LIST
    // ----------------------------
    function loadBlocks(districtId) {
        var Fyear = $('#YearSelect').val();
        ////alert(Fyear);
        $.ajax({
            type: "POST",
            url: "GISEGBlock.aspx/GetBlocks",
            data: JSON.stringify({ districtId: districtId, Fyear: Fyear }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                let data = JSON.parse(result.d);
                if (layertype == 4 || layertype == 5) {

                    //$("#blockSelect").show();
                    //$("#drdist").hide();
                    $("#blockSelect").empty().append(`<option value="0">-- Select Block --</option>`);

                    data.forEach(r => {
                        $("#blockSelect").append(
                            `<option value="${r.BlockId}">${r.BlockName}</option>`
                        );
                    });

                    $("#groupList").html(""); // clear EG block list
                    // IMPORTANT FIX
                    groups = {};
                    activeGroup = null;
                    usedColors.clear();
                    mergeSelection = [];
                    // Clear existing list
                    let container = $("#groupList");
                    container.empty();

                    if (data.length === 0) {
                        container.html("<p>No blocks found.</p>");
                        return;
                    }
                    console.log(data);
                    $("#EGListHeading").text("EG Blocks");
                    data.forEach(g => {

                        console.log(g.color);
                        addGroup(g.BlockName, g.color, g.BlockId, g.EGBlock, g.ismapped);
                    });
                }



                loadAllVillagesOnce(districtId);
            }
        });
        //Fill_Block("groupDropdown", districtId);
        //loadAllVillagesOnce(districtId);
    }


    //Load EG Cluster//

    function loadCluster(blockid) {
        var Fyear = $('#YearSelect').val();
       /* Fill_Cluster("clusterDropdown", blockid);*/
        $.ajax({
            type: "POST",
            url: "GISEGBlock.aspx/GetCluster",
            data: JSON.stringify({ blockid: blockid, Fyear: Fyear }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                let data = JSON.parse(result.d);
                //let container = $("#groupList");
                //container.empty();
                //if (data.length === 0) {
                //    container.html("<p>No blocks found.</p>");
                //    return;
                //}
                if (layertype == 4) {
                    $("#clusterDropdown").empty().append(`<option value="0">-- Select Cluster --</option>`);

                    data.forEach(r => {
                        $("#blockSelect").append(
                            `<option value="${r.ClusterId}">${r.ClusterName}</option>`
                        );
                    });
                }

                console.log(data);
                data.forEach(g => {

                    console.log(g.color);
                    addClusterGroup(g.ClusterName, g.color, g.ClusterId, g.EGClusterCode, g.ismapped);
                });

                /* loadAllVillagesOnce(blockid);*/
            }
        });
    }

    /* ===========================================================
       ATTRIBUTE DROPDOWN
    ===========================================================*/
    function populateAttributeDropdown() {
        let props = Object.keys(geoData.features[0].properties);
        let sel = $("#attributeSelect");
        sel.empty().append(`<option value="">-- Select Attribute --</option>`);
        props.forEach(p => sel.append(`<option>${p}</option>`));

        $("#attributeSelect").change(function () {
            selectedAttribute = $(this).val();
        });
    }

    /* ===========================================================
       DRAW MAP
    ===========================================================*/

    function drawMap(features = null) {

        if (!geoData) return;
        if (fullLayer) map.removeLayer(fullLayer);

        let coll = {
            type: "FeatureCollection",
            features: features || geoData.features
        };

        fullLayer = L.geoJSON(coll, {
            style: f => ({
                color: "#000",
                fillColor: f.properties.color || "#666",
                weight: 1,
                fillOpacity: 0.7
            }),
            onEachFeature: (f, l) => {

                l.on("click", () => {

                    if (currentMode === "click") {
                        //handleFeatureClick(f, l);
                        toggleMergeSelection(f, l);
                        return;
                    }

                    if (currentMode === "merge") {
                        toggleMergeSelection(f, l);
                        return;
                    }

                    // draw mode clicks are ignored
                });

                l.on("mouseover", e => showHover(e, f));
                l.on("mouseout", e => showHoverreset(e, f));
            }
        }).addTo(map);
        console.log("DRAW Map -" + fullLayer);
        fullLayer = fullLayer;
    }
    function resetGroupColor(groupId) {
        fullLayer.eachLayer(layer => {
            const f = layer.feature;
            if (layertype == 3) {
                if (f.properties.DistrictCode === groupId) {
                    // restore original
                    f.properties.color = f.properties._baseColor;

                    layer.setStyle({
                        fillColor: f.properties._baseColor || "#666",
                        fillOpacity: 0.7
                    });
                }
            }
            else if (layertype == 4) {
                if (f.properties.BlockCode === groupId) {
                    // restore original
                    f.properties.color = f.properties._baseColor;

                    layer.setStyle({
                        fillColor: f.properties._baseColor || "#666",
                        fillOpacity: 0.7
                    });
                }
            }
            else if (layertype == 5) {
                if (f.properties.ClusterCode === groupId) {
                    // restore original
                    f.properties.color = f.properties._baseColor;

                    layer.setStyle({
                        fillColor: f.properties._baseColor || "#666",
                        fillOpacity: 0.7
                    });
                }
            }

        });
        drawMap(); // redraw
    }


    $("input[name='activeGroup']")
        .off("change")
        .on("change", function () {
            activeGroup = $(this).val();
            console.log("Active Group:", activeGroup);

        });

    /* ===========================================================
      CLICK SELECT & DESELECT
   ===========================================================*/
    function handleFeatureClick(feature, layer) {

        // 🚫 ALERT ONLY IN CLICK MODE
        if (currentMode === "click" && !activeGroup) {
            alert("Select a group first.");
            return;
        }

        // 🟦 DRAW MODE → visual selection only
        if (currentMode === "draw") {
            layer.setStyle({
                weight: 3,
                //dashArray: "5,5"
                dashArray: null
            });
            return;
        }

        // 🟧 MERGE MODE → handled elsewhere
        if (currentMode === "merge") {
            toggleMergeSelection(feature, layer);
            return;
        }

        // ---------------------------
        // CLICK MODE LOGIC
        // ---------------------------

        const currentColor = layer.options.fillColor;

        if (feature.properties.group === activeGroup) {
            feature.properties.group = null;
            feature.properties.color = "#666666";
            layer.setStyle({
                fillColor: "#666666",
                fillOpacity: 0.4
            });
            return;
        }

        feature.properties.group = activeGroup;
        feature.properties.color = selectedBlockColor;

        layer.setStyle({
            fillColor: selectedBlockColor,
            fillOpacity: 0.7
        });
    }

    map.on("pm:create", function (e) {

        console.log("pm:create fired, mode =", currentMode);

        if (currentMode !== "draw") {
            map.removeLayer(e.layer);
            return;
        }

        let drawn = e.layer.toGeoJSON();
        let drawnBbox = turf.bbox(drawn);

        let candidates = rtree.search({
            minX: drawnBbox[0],
            minY: drawnBbox[1],
            maxX: drawnBbox[2],
            maxY: drawnBbox[3]
        });

        let added = 0;

        candidates.forEach(bb => {

            let feature = shapesIndex[bb.id];
            let layer = getLayerById(bb.id);

            if (!feature || !layer) return;

            try {
                if (turf.booleanIntersects(feature, drawn)) {

                    if (!mergeSelection.includes(bb.id)) {
                        mergeSelection.push(bb.id);
                        console.log("Added to mergeSelection:", bb.id);
                    }

                    layer.setStyle({
                        weight: 3,
                        //dashArray: "5,5"
                        dashArray: null
                    });

                    added++;
                }
            } catch (err) {
                console.error("Intersect error:", err);
            }
        });

        map.removeLayer(e.layer);

        if (added === 0) {
            alert("No polygons found inside selection.");
        } else {
            console.log("Final mergeSelection:", mergeSelection);
        }
    });


    function getLayerById(id) {
        let found = null;
        console.log("df", fullLayer);
        fullLayer.eachLayer(l => {
            if (l.feature.properties._id === id)
                found = l;
        });
        return found;
    }


    /* ===========================================================
       MERGE MODE (CLICK SELECTION)
    ===========================================================*/
    function toggleMergeSelection(feature, layer) {
        debugger;
        const id = feature.properties._id;
        const idx = mergeSelection.indexOf(id);
        const opt = $("#districtSelect").find(":selected");
        const optb = $("#groupDropdown").find(":selected");
        let selectedColor = "";


        if (layertype == "3")
            selectedColor = opt.data("color");
        else if (layertype == "4" || layertype == "5")
            selectedColor = optb.data("color");
        else
            selectedColor = "#666666";

        if (idx === -1) {
            // ✅ SELECT
            mergeSelection.push(id);

            layer.setStyle({
                weight: 3,
                color: selectedColor,
                fillColor: selectedColor,
                //dashArray: "5,5"
                dashArray: null
            });
        } else {
            // ❌ UNSELECT
            mergeSelection.splice(idx, 1);

            layer.setStyle({
                weight: 1,
                color: "#666666",
                fillColor: "#666666",
                dashArray: null
            });
        }

        console.log("mergeSelection:", mergeSelection.length);
    }

    //function toggleMergeSelection(feature, layer) {
    //    let id = feature.properties._id;

    //    // Already selected → unselect
    //    if (mergeSelection.includes(id)) {
    //        mergeSelection = mergeSelection.filter(x => x !== id);
    //        layer.setStyle({ weight: 1, dashArray: null });
    //        return;
    //    }

    //    // Select it
    //    mergeSelection.push(id);
    //    layer.setStyle({ weight: 3, dashArray: "5,5" });
    //}

    /* ===========================================================
       MERGE NOW (UNION GEOMETRY)
    ===========================================================*/
    /* ===========================================================
       FAST CHUNKED UNION (REQUIRED)
    =========================================================== */
    function batchUnion(features, batchSize = 50) {
        let queue = features.slice();

        while (queue.length > 1) {
            let next = [];

            for (let i = 0; i < queue.length; i += batchSize) {
                let batch = queue.slice(i, i + batchSize);
                let merged = batch.reduce((a, b) => {
                    try {
                        return a ? turf.union(a, b) : b;
                    } catch {
                        return a || b;
                    }
                }, null);

                if (merged) next.push(merged);
            }

            queue = next;
        }

        return queue[0] || null;
    }


    $("#mergeNowBtn").click(() => {

        $("#mapLoader").show();

        setTimeout(() => {

            // 🛑 validation
            if (mergeSelection.length < 2) {
                alert("Select at least 2 polygons for merging.");
                $("#mapLoader").hide();
                return;
            }

            // 🔹 get selected features
            const selectedFeatures = mergeSelection
                .map(id => shapesIndex[id])
                .filter(Boolean);

            if (selectedFeatures.length < 2) {
                alert("Invalid selection.");
                $("#mapLoader").hide();
                return;
            }

            // 🔑 base properties
            const base =
                selectedFeatures.find(f => f?.properties?.isMapped === true)
                || selectedFeatures.find(f => f?.properties?.color)
                || selectedFeatures[0];
            const blockCode = base.properties.BlockCode;
            const blockName = base.properties.BlockName;
            const blockColor = base.properties.color || "#666666";

            try {
                // 🧼 clean geometries once
                const clean = selectedFeatures.map(f =>
                    turf.cleanCoords(turf.rewind(f))
                );

                // 🚀 fast merge
                const merged = batchUnion(clean, 50);

                if (!merged || !merged.geometry) {
                    alert("Merge failed due to geometry issue.");
                    $("#mapLoader").hide();
                    return;
                }

                const mergedId = "M_" + Date.now();

                const newMerged = {
                    type: "Feature",
                    geometry: merged.geometry,
                    properties: {
                        _id: mergedId,
                        BlockCode: blockCode,
                        BlockName: blockName,
                        color: blockColor,
                        isMapped: true,
                        MERGED_FROM: mergeSelection.join(",")
                    }
                };

                // 🧠 BACKUP originals for undo
                mergeSelection.forEach(id => {
                    deletedFeatureBackup[id] = shapesIndex[id];
                });

                // 🧠 delta undo record
                mergeUndoStack.push({
                    removed: [...mergeSelection],
                    added: mergedId
                });

                // ❌ remove merged villages
                geoData.features = geoData.features.filter(
                    f => !mergeSelection.includes(f.properties._id)
                );

                mergeSelection.forEach(id => {
                    delete shapesIndex[id];
                    rtree.remove({ id });
                });

                // ✅ add merged feature
                geoData.features.push(newMerged);
                shapesIndex[mergedId] = newMerged;

                const b = turf.bbox(newMerged);
                rtree.insert({
                    minX: b[0],
                    minY: b[1],
                    maxX: b[2],
                    maxY: b[3],
                    id: mergedId
                });

                // 🔄 reset state
                mergeSelection = [];
                currentMode = "click";

                drawMap();
                alert("Merge completed successfully!");

            } catch (err) {
                console.error(err);
                alert("Merge failed due to geometry error.");
            }

            $("#mapLoader").hide();

        }, 30);
    });


    /* ===========================================================
MERGE Working
===========================================================*/
    //$("#mergeNowBtn").click(() => {

    //    $("#mapLoader").show();

    //    setTimeout(() => {

    //        if (mergeSelection.length < 2) {
    //            alert("Select at least 2 polygons for merging.");
    //            $("#mapLoader").hide();
    //            return;
    //        }

    //        let selectedFeatures = mergeSelection
    //            .map(id => shapesIndex[id])
    //            .filter(Boolean);

    //        if (selectedFeatures.length < 2) {
    //            alert("Invalid selection.");
    //            $("#mapLoader").hide();
    //            return;
    //        }

    //        let base = selectedFeatures[0];
    //        let blockCode = base.properties.BlockCode;
    //        let blockName = base.properties.BlockName;
    //        let blockColor = base.properties.color || "#666666";

    //        try {
    //            let mergedGeometry = selectedFeatures
    //                .map(f => turf.cleanCoords(turf.rewind(f)))
    //                .reduce((acc, cur) => turf.union(acc, cur));

    //            if (!mergedGeometry) {
    //                alert("Merge failed: invalid geometry.");
    //                $("#mapLoader").hide();
    //                return;
    //            }

    //            let newMerged = {
    //                type: "Feature",
    //                geometry: mergedGeometry.geometry,
    //                properties: {
    //                    _id: "M_" + Date.now(),
    //                    BlockCode: blockCode,
    //                    BlockName: blockName,
    //                    color: blockColor,
    //                    MERGED_FROM: mergeSelection.join(",")
    //                }
    //            };

    //            // undo support
    //            mergeUndoStack.push(
    //                JSON.parse(JSON.stringify(geoData.features))
    //            );

    //            geoData.features = geoData.features.filter(
    //                f => !mergeSelection.includes(f.properties._id)
    //            );

    //            geoData.features.push(newMerged);

    //            // rebuild index
    //            shapesIndex = {};
    //            rtree.clear();

    //            geoData.features.forEach(f => {
    //                shapesIndex[f.properties._id] = f;
    //                let b = turf.bbox(f);
    //                rtree.insert({
    //                    minX: b[0],
    //                    minY: b[1],
    //                    maxX: b[2],
    //                    maxY: b[3],
    //                    id: f.properties._id
    //                });
    //            });

    //            mergeSelection = [];
    //            currentMode = "click";

    //            drawMap();
    //            alert("Merge completed successfully!");

    //        } catch (e) {
    //            console.error(e);
    //            alert("Merge failed due to invalid shapes.");
    //        }

    //        $("#mapLoader").hide();

    //    }, 50);
    //});

    /* ===========================================================
MERGE Working
===========================================================*/
    $(document).ready(function () {
        /*loadLayerList();*/
        loadStates();
        $("#EGListHeading").text("");
    });

    function loadLayerList() {
        $.ajax({
            type: "POST",
            url: "GISEGBlock.aspx/GetLayers",
            contentType: "application/json; charset=utf-8",
            data: "{}",
            success: function (res) {

                let layers = res.d;
                $("#layerList").empty();

                layers.forEach(l => {
                    $("#layerList").append(`
                    <div class="layer-item"
                         data-workspace="${l.Workspace}"
                         data-layer="${l.GeoServerLayer}"
                         data-url="${l.GeoServerURL}"
                         data-type="${l.LayerType}"
                         style="padding:6px; cursor:pointer; border-bottom:1px solid #eee;">
                        <b>${l.LayerName}</b>
                    </div>
                `);
                });

                $(".layer-item").click(function () {
                    // Remove highlight from all
                    $(".layer-item").removeClass("selected");

                    // Add highlight to clicked
                    $(this).addClass("selected");

                    let workspace = $(this).data("workspace");
                    let layer = $(this).data("layer");
                    let url = $(this).data("url");
                    let type = $(this).data("type");

                    loadGeoServerLayer(workspace, layer, url, type);
                });
            }
        });
    }
    let layertype;
    $('.layer-item').on('click', function (event) {
        event.stopPropagation();
        $(".layer-item").removeClass("selected");

        // Add highlight to clicked
        $(this).addClass("selected");


        layertype = $(this).data("layer");
        $("#FilterDiv").show();
        $("#stateSelect").val("0");
        $("#districtSelect").val("0");
        $("#groupList").html("");
        // 🔥 CLEAR PREVIOUS MAP DATA
        if (villageLayer) {
            map.removeLayer(villageLayer);
            villageLayer = null;
            fullLayer = null;
        }
        clearGeoLayers();

        if (layertype == 4) {
            //$("#stateSelect").show();
            //$("#districtSelect").show();
            $("#districtcheck").hide();
        }
        else if (layertype == 3) {
            //$("#stateSelect").show();
            //$("#districtSelect").hide();
            $("#districtcheck").show();
        }
        if (layertype == 5) {
            //$("#stateSelect").show();
            //$("#districtSelect").show();
            $("#districtcheck").hide();
        }

        let text = $(this).text().trim();
        let layerId = $(this).data('layer');

        $('#layerBtn').text(text);     // change button text
        $('#layerList').hide();        // close dropdown
    });

    /* ===========================================================
   SHAPEFILE UPLOAD
===========================================================*/


    function loadGeoServerLayer(workspace, layerName, url, type) {
        // Construct the WFS URL for GeoJSON
        let wfsUrl = `${url}/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=${workspace}%3A${layerName}&maxFeatures=5000&outputFormat=application%2Fjson`;
        alert(wfsUrl);
        // Call server-side WebMethod to bypass CORS
        $.ajax({
            type: "POST",
            url: "GISEGBlock.aspx/GetGeoServerLayer",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ url: wfsUrl }),
            success: function (res) {
                if (res.d.startsWith("ERROR:")) {
                    alert(res.d);
                    return;
                }

                let geojson = JSON.parse(res.d);
                console.log(geojson);

                // Remove previous layer if needed
                if (window.currentLayer) {
                    map.removeLayer(window.currentLayer);
                }
                geoData = geojson;

                selectedAttribute = "";

                populateAttributeDropdown();
                /*populateStateDropdown();*/

                indexAllFeatures();
                drawMap();

            },
            error: function (err) {
                console.error("Error loading layer:", err);
            }
        });
    }
    // Helper function to convert RGB to Hex
    function rgbToHex(rgb) {
        if (!rgb || rgb[0] === '#') return rgb;

        let res = rgb.match(/\d+/g);
        if (!res) return "#666";

        return "#" + res.slice(0, 3).map(x => {
            let h = parseInt(x).toString(16);
            return h.length === 1 ? "0" + h : h;
        }).join("");
    }
    var selectedBlockColor = "#000000"; // global variable

    var lastSelectedGroup = null; // track last clicked EG Block


    $("#groupDropdown").on("change", function () {

        const blockCode = $(this).val();
        const blockName = $("#groupDropdown option:selected").text().trim();

        const opt = $("#groupDropdown").find(":selected");
        let selected_Color = opt.data("color");


        if (!blockCode) {
            activeGroup = null;
            return;
        }

        // ✅ SET ACTIVE GROUP
        activeGroup = blockName;        // OR blockCode (pick one & be consistent)
        activeGroupId = blockCode;
        selectedBlockColor = selected_Color;
        console.log("ACTIVE GROUP SET:", activeGroup);
        if (layertype == '4') {
            // Load / highlight villages
            loadAllVillagesOnce(blockCode);
        }
        else if (layertype == '5') {

            if (!blockCode) return;

            console.log("Block selected:", blockCode);

            // Reset Block dropdown & selection
            $("#clusterDropdown").empty()
                .append(`<option value="">-- Select Cluster --</option>`);

            if (fullLayer) {
                map.removeLayer(fullLayer);
                fullLayer = null;
            }

            // 🔹 THIS WAS MISSING
            loadCluster(blockCode);
        }
    });


    $("#clusterDropdown").on("change", function () {

        const ClusterCode = $(this).val();
        const ClusterName = $("#clusterDropdown option:selected").text().trim();

        const opt = $("#clusterDropdown").find(":selected");
        let selected_Color = opt.data("color");


        if (!ClusterCode) {
            activeGroup = null;
            return;
        }

        // ✅ SET ACTIVE GROUP
        activeGroup = ClusterName;        // OR blockCode (pick one & be consistent)
        activeGroupId = ClusterCode;
        selectedBlockColor = selected_Color;
        console.log("ACTIVE GROUP SET:", activeGroup);
        if (layertype == '5') {

            if (!ClusterCode) return;

            console.log("Cluster selected:", ClusterCode);

            if (fullLayer) {
                map.removeLayer(fullLayer);
                fullLayer = null;
            }

            loadAllVillagesOnce(ClusterCode);
        }
    });

    function autoSelectAllLayers() {

        if (!fullLayer) return;

        fullLayer.eachLayer(layer => {
            const f = layer.feature;
            if (!f || !f.properties) return;

            // ✅ Add every feature
            if (!mergeSelection.includes(f.properties._id)) {
                mergeSelection.push(f.properties._id);
            }

            // ✅ Visual feedback
            layer.setStyle({
                weight: 3,
                //dashArray: "5,5"
                dashArray: null
            });
        });

        console.log("Select ALL layers:", mergeSelection.length);
    }


    function autoSelectCurrentBlock() {

        mergeSelection = [];

        if (!fullLayer) return;

        fullLayer.eachLayer(layer => {
            const f = layer.feature;
            if (!f || !f.properties) return;

            if (
                (layertype == 4 && f.properties.BlockCode === activeGroupId) ||
                (layertype == 3 && f.properties.DistrictCode === activeGroupId) ||
                (layertype == 5 && f.properties.ClusterCode === activeGroupId)
            ) {
                mergeSelection.push(f.properties._id);

                layer.setStyle({
                    weight: 3,
                    //dashArray: "5,5"
                    dashArray: null
                });
            }
            else {
                layer.setStyle({
                    weight: 1,
                    dashArray: null
                });
            }
        });
        console.log("Auto selected:", mergeSelection.length);
    }

    function autoSelectCurrentDistrict() {

        if (!fullLayer || !activeDistrictId) return;

        mergeSelection = [];

        fullLayer.eachLayer(layer => {

            const f = layer.feature;
            if (!f || !f.properties) return;

            if (
                (layertype == 4 && f.properties.BlockCode === activeGroupId) ||
                (layertype == 3 && f.properties.DistrictCode === activeGroupId) ||
                (layertype == 5 && f.properties.ClusterCode === activeGroupId)
            ) {

                mergeSelection.push(f.properties._id);

                layer.setStyle({
                    weight: 3,
                    //dashArray: "5,5"
                    dashArray: null
                });
            }
            else {
                layer.setStyle({
                    weight: 1,
                    dashArray: null
                });
            }
        });

        console.log("District auto selected:", mergeSelection.length);
    }

    var villageLayerGroup = L.featureGroup().addTo(map);

    function loadselectedvillage(BlockId) {


    }



    document.getElementById("publishBtn").onclick = async function () {

        // 1. Extract GeoJSON from Leaflet layer
        const geojson = villageLayerGroup.toGeoJSON();

        if (!geojson || !geojson.features || geojson.features.length === 0) {
            alert("No polygons on map to publish.");
            return;
        }

        // 2. Get layer name input
        const layerName = document.getElementById("shapefileNameInput").value.trim();
        if (!layerName) {
            alert("Enter layer name.");
            return;
        }

        const zipName = layerName + ".zip";

        // 3. Convert GeoJSON → Shapefile ZIP (browser)
        const zipBlob = shpwrite.zip(geojson);

        // 4. Convert ZIP to Base64 for ASP.NET
        const arrayBuffer = await zipBlob.arrayBuffer();
        const bytes = new Uint8Array(arrayBuffer);
        let binary = "";
        bytes.forEach(b => binary += String.fromCharCode(b));
        const base64 = btoa(binary);

        // 5. Upload to ASP.NET → GeoServer
        $.ajax({
            type: "POST",
            url: "GISEGBlock.aspx/UploadToGeoServer",
            data: JSON.stringify({
                fileName: zipName,
                base64: base64
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (res) {
                alert("✔ " + res.d);
            },
            error: function (err) {
                console.log(err);
                alert("❌ Failed to publish.");
            }
        });
    };


    let villageLayer;

    let coloredBlocks = {};

    function clearGeoLayers() {
        map.eachLayer(layer => {
            if (layer instanceof L.GeoJSON) {
                map.removeLayer(layer);
            }
        });
    }
    let allVillageFeatures = [];
    let loadedDistricts = new Set();        // 🚫 prevent duplicates
    let districtIndex = {};

    async function loadAllVillagesOnce(DistrictCode) {
        let _districtcode = null;
        let _blockcode = null;
        let digitalizationValue = null;
        let isdigitalize = null;
        if (villageLayer) {
            map.removeLayer(villageLayer);
            villageLayer = null;
            fullLayer = null;
        }
        clearGeoLayers();
        mergeSelection = [];
        if (layertype == 3) {
            const opt = $("#districtSelect").find(":selected");
            activeGroupId = opt.val();
            activeGroupName = opt.text();
            selectedColor = opt.data("color");
            activeEGDistrict = opt.data("egdistrict");
            ismappeddistrict = opt.data("ismappeddistrict");

            const response = await checkDigitilazation(DistrictCode, "", "");
            digitalizationValue = response.d;
        }



        if (layertype == 4) {
            const response = await checkDigitilazation("", DistrictCode, "");
            digitalizationValue = response.d;




        }

        if (layertype == 5) {
            const response = await checkDigitilazation("", "", DistrictCode);
            digitalizationValue = response.d;




        }
        _districtcode = $('#districtSelect').val();

        const opt = $("#groupDropdown").find(":selected");
        let active_GroupId = opt.val();
        let active_GroupName = opt.text().trim();
        let selected_Color = opt.data("color");
        let active_EGBlock = opt.data("egblock");
        let isblockmapped = opt.data("ismapped");


        _blockcode = $('#groupDropdown').val();
        //if (layertype == 4) { 
        //// 🔥 CLEAR PREVIOUS MAP DATA
        //if (villageLayer) {
        //    map.removeLayer(villageLayer);
        //    villageLayer = null;
        //    fullLayer = null;
        //}
        //clearGeoLayers();
        //// optional: clear selection & UI
        //    mergeSelection = [];
        //}
        // 🔒 Lock checkbox while loading
        setDistrictLoading(DistrictCode, true);
        $("#mapLoader").show();
        let wfsUrl = ``;
        var Fyear = $('#YearSelect').val();




        console.log("Digitalization Value:", digitalizationValue);

        if (layertype == 4) {



            if (digitalizationValue == '1') {
                wfsUrl = `https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG:lyr_Block_NW&maxFeatures=500000&outputFormat=application/json&viewparams=BlockCode:${DistrictCode};DistrictCode:${_districtcode}`;
                $("#exportBtn").hide();
                $("#resetLayerBtn1").hide();
                $("#deleteLayerBtn").show();
            }
            else {
                wfsUrl = `https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG:Test_GIS_Village_Block_Raw_NW&maxFeatures=500000&outputFormat=application/json&viewparams=BlockCode:${DistrictCode};DistrictCode:${_districtcode}`;
                $("#exportBtn").show();
                $("#resetLayerBtn1").show();
                $("#deleteLayerBtn").hide();
            }
        }



        else if (layertype == 3) {

            if (digitalizationValue == '1') {
                wfsUrl = `https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG:lyr_District&maxFeatures=500000&outputFormat=application/json&viewparams=DistrictCode:${DistrictCode}`;
                $("#exportBtn").hide();
                $("#resetLayerBtn1").hide();
                $("#deleteLayerBtn").show();
            }
            else {
                wfsUrl = `https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG:Test_GIS_Village_Raw&maxFeatures=500000&outputFormat=application/json&viewparams=DistrictCode:${DistrictCode}`;
                $("#exportBtn").show();
                $("#resetLayerBtn1").show();
                $("#deleteLayerBtn").hide();
            }

        }

        else if (layertype == 5) {

            if (digitalizationValue == '1') {
                wfsUrl = `https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG:lyr_Cluster_NW&maxFeatures=500000&outputFormat=application/json&viewparams=ClusterCode:${DistrictCode};BlockCode:${_blockcode}`;
                $("#exportBtn").hide();
                $("#resetLayerBtn1").hide();
                $("#deleteLayerBtn").show();
            }
            else {
                wfsUrl = `https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG:lyr_Cluster_Village&maxFeatures=500000&outputFormat=application/json&viewparams=ClusterCode:${DistrictCode};BlockCode:${_blockcode}`;
                $("#exportBtn").show();
                $("#resetLayerBtn1").show();
                $("#deleteLayerBtn").hide();
            }
        }
        console.log(wfsUrl);

        $.ajax({
            type: "POST",
            url: "GISEGBlock.aspx/GetGeoServerLayer",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ url: wfsUrl }),

            success: function (res) {

                $("#mapLoader").hide();

                let rawGeoData = JSON.parse(res.d);
                if (layertype == 4) {
                    geoData = {
                        type: "FeatureCollection",
                        features: rawGeoData.features.map((f, idx) => ({
                            type: "Feature",
                            geometry: f.geometry,
                            properties: {
                                _id: f.id || f.properties?.EGVillageCode || idx,  // ✅ VERY IMPORTANT
                                MappingStatus: f.properties?.mapped || "",
                                EGVillageCode: f.properties?.EGVillageCode || "",
                                EGBlockCode: f.properties?.EGBlockCode || "",
                                EGDistrictCode: f.properties?.EGDistrictCode || "",
                                AdminDistrict: f.properties?.AdminDistrictName || "",
                                AdminBlock: f.properties?.MainBlockName || "",
                                VillageID: f.properties?.VillageID || "",
                                lyr_VillageName: f.properties?.lyr_VillageName || "",
                                StateName: f.properties?.ST_Name || "",
                                DistrictCode: f.properties?.DistrictCode || "",
                                DistrictName: f.properties?.DT_Name || f.properties?.DistrictName || "",
                                BlockCode: f.properties?.BlockCode || "",
                                BlockName: f.properties?.BK_Name || "",
                                VillageName: f.properties?.VL_Name || "",
                                color: f.properties?.colorCode || "#666666",
                                _baseColor: f.properties?.colorCode || "#666666"

                                //VillageName: f.properties?.VillageName || "",
                                //DistrictCode: f.properties?.DistrictCode || "",
                                //BlockCode: f.properties?.BlockCode || "",
                                //BlockName: f.properties?.BlockName || "",
                                //group: f.properties?.BlockName || null,
                                //✅ STORE ORIGINAL

                            }
                        }))
                    };
                }
                else if (layertype == 3) {

                    geoData = {
                        type: "FeatureCollection",
                        features: rawGeoData.features.map((f, idx) => ({
                            type: "Feature",
                            geometry: f.geometry,
                            properties: {
                                _id: f.id || f.properties?.EGVillageCode || `${DistrictCode}_${idx}`,
                                MappingStatus: f.properties?.mapped || "",
                                EGVillageCode: f.properties?.EGVillageCode || "",
                                EGBlockCode: f.properties?.EGBlockCode || "",
                                EGDistrictCode: f.properties?.EGDistrictCode || "",
                                AdminDistrict: f.properties?.AdminDistrictName || "",
                                AdminBlock: f.properties?.MainBlockName || "",
                                VillageID: f.properties?.VillageID || "",
                                lyr_VillageName: f.properties?.lyr_VillageName || "",
                                StateName: f.properties?.ST_Name || "",
                                DistrictCode: f.properties?.DistrictCode || "",
                                DistrictName: f.properties?.DT_Name || f.properties?.DistrictName || "",
                                BlockCode: f.properties?.BlockCode || "",
                                BlockName: f.properties?.BK_Name || "",
                                VillageName: f.properties?.VL_Name || "",
                                color: f.properties?.colorCode || "#666666",
                                _baseColor: f.properties?.colorCode || "#666666"
                                //MappingStatus: f.properties?.mapped || "",
                                //StateName: f.properties?.ST_Name || "",
                                //EGVillageCode: f.properties?.EGVillageCode || "",
                                //EGBlockCode: f.properties?.EGBlockCode || "",
                                //EGDistrictCode: f.properties?.EGDistrictCode || "",
                                //AdminDistrict: f.properties?.AdminDistrictName || "",
                                //AdminBlock: f.properties?.MainBlockName || "",
                                //VillageID: f.properties?.VillageID || "",
                                //lyr_VillageName: f.properties?.lyr_VillageName || "",
                                //DistrictCode: f.properties?.DistrictCode || DistrictCode,
                                //DistrictName: f.properties?.DT_Name || f.properties?.DistrictName || "",
                                //BlockCode: f.properties?.BlockCode || "",
                                //BlockName: f.properties?.BK_Name || "",
                                //VillageName: f.properties?.VL_Name || "",
                                //color: selectedColor || "#666666",
                                //_baseColor: f.properties?.colorCode || "#666666"
                            }
                        }))
                    };
                }

                else if (layertype == 5) {

                    geoData = {
                        type: "FeatureCollection",
                        features: rawGeoData.features.map((f, idx) => ({
                            type: "Feature",
                            geometry: f.geometry,
                            properties: {
                                _id: f.id || f.properties?.EGVillageCode || `${DistrictCode}_${idx}`,
                                MappingStatus: f.properties?.mapped || "",
                                EGVillageCode: f.properties?.EGVillageCode || "",
                                EGBlockCode: f.properties?.EGBlockCode || "",
                                EGDistrictCode: f.properties?.EGDistrictCode || "",
                                AdminDistrict: f.properties?.AdminDistrictName || "",
                                AdminBlock: f.properties?.MainBlockName || "",
                                VillageID: f.properties?.VillageID || "",
                                lyr_VillageName: f.properties?.lyr_VillageName || "",
                                StateName: f.properties?.ST_Name || "",
                                DistrictCode: f.properties?.DistrictCode || "",
                                DistrictName: f.properties?.DT_Name || f.properties?.DistrictName || "",
                                BlockCode: f.properties?.BlockCode || "",
                                BlockName: f.properties?.BK_Name || "",
                                VillageName: f.properties?.VL_Name || "",
                                ClusterCode: f.properties?.ClusterCode || "",
                                ClusterName: f.properties?.ClusterName || "",
                                color: f.properties?.colorCode || "#666666",
                                _baseColor: f.properties?.colorCode || "#666666"
                                //MappingStatus: f.properties?.mapped || "",
                                //StateName: f.properties?.ST_Name || "",
                                //EGVillageCode: f.properties?.EGVillageCode || "",
                                //EGBlockCode: f.properties?.EGBlockCode || "",
                                //EGDistrictCode: f.properties?.EGDistrictCode || "",
                                //AdminDistrict: f.properties?.AdminDistrictName || "",
                                //AdminBlock: f.properties?.MainBlockName || "",
                                //VillageID: f.properties?.VillageID || "",
                                //lyr_VillageName: f.properties?.lyr_VillageName || "",
                                //DistrictCode: f.properties?.DistrictCode || DistrictCode,
                                //DistrictName: f.properties?.DT_Name || f.properties?.DistrictName || "",
                                //BlockCode: f.properties?.BlockCode || "",
                                //BlockName: f.properties?.BK_Name || "",
                                //VillageName: f.properties?.VL_Name || "",
                                //color: selectedColor || "#666666",
                                //_baseColor: f.properties?.colorCode || "#666666"
                            }
                        }))
                    };
                }

                //else if (layertype == 3) {
                //    //if (loadedDistricts.has(DistrictCode)) {
                //    //    console.log("District already loaded:", DistrictCode);
                //    //    return;
                //    //}

                //    let rawGeoData = JSON.parse(res.d);

                //    // 🧱 Build new features
                //    let newFeatures = rawGeoData.features.map((f, idx) => ({
                //        type: "Feature",
                //        geometry: f.geometry,
                //        properties: {
                //            _id: f.id || f.properties?.EGVillageCode || `${DistrictCode}_${idx}`,
                //            StateName: f.properties?.ST_Name || "",
                //            EGBlockCode: f.properties?.EGBlockCode || "",
                //            EGDistrictCode: f.properties?.EGDistrictCode || "",
                //            DistrictCode: f.properties?.DistrictCode || DistrictCode,
                //            DistrictName: f.properties?.DT_Name || "",
                //            BlockCode: f.properties?.BlockCode || "",
                //            BlockName: f.properties?.BK_Name || "",
                //            VillageName: f.properties?.VL_Name || "",
                //            color: selectedColor || "#666666",
                //            _baseColor: f.properties?.colorCode || "#666666"
                //        }
                //    }));

                //    // 🧭 Store
                //    allVillageFeatures.push(...newFeatures);
                //    districtIndex[DistrictCode] = newFeatures;
                //    loadedDistricts.add(DistrictCode);

                //    // 🗺️ Rebuild map layer
                //    /*if (villageLayer) map.removeLayer(villageLayer);*/

                //    geoData = {
                //        type: "FeatureCollection",
                //        features: allVillageFeatures
                //    };
                //}
                //if (villageLayer) map.removeLayer(villageLayer);
                villageLayer = L.geoJSON(geoData, {
                    style: defaultVillageStyle,
                    onEachFeature: (f, l) => {

                        l.on("click", () => {

                            if (currentMode === "click") {
                                //handleFeatureClick(f, l);
                                toggleMergeSelection(f, l);
                                return;
                            }

                            if (currentMode === "merge") {
                                toggleMergeSelection(f, l);
                                return;
                            }
                        });

                        l.on("mouseover", e => showHover(e, f, DistrictCode));
                        l.on("mouseout", e => showHoverreset(e, f));
                    }
                }).addTo(map);

                fullLayer = villageLayer;
                applyCurrentGroupColoring(); ///ak
                if (activeDistrictId) autoSelectCurrentDistrict();
                if (activeGroupId) autoSelectCurrentBlock();
                console.log("activeGroupId", activeGroupId);
                // ✅ ZOOM TO ALL VILLAGES
                if (geoData.features.length) {
                    map.fitBounds(fullLayer.getBounds(), {
                        padding: [20, 20]
                    });
                }
                //populateAttributeDropdown();
                indexAllFeatures();
                console.log("layer loaded once_" + geoData.features);
                // 🔓 Unlock checkbox after load
                setDistrictLoading(DistrictCode, false);

            },

            error: function () {
                $("#mapLoader").hide();
                setDistrictLoading(DistrictCode, false);
                console.error("Village load failed");
            }
        });
    }
    function unloadDistrict(code) {

        if (!loadedDistricts.has(code)) return;

        // Remove features from memory
        allVillageFeatures = allVillageFeatures.filter(f =>
            f.properties.DistrictCode !== code
        );

        delete districtIndex[code];
        loadedDistricts.delete(code);

        // Rebuild map
        if (villageLayer) map.removeLayer(villageLayer);

        geoData = {
            type: "FeatureCollection",
            features: allVillageFeatures
        };

        villageLayer = L.geoJSON(geoData, {
            style: defaultVillageStyle,
            onEachFeature: (f, l) => {
                l.on("click", () => {
                    //if (currentMode === "click") handleFeatureClick(f, l);
                    if (currentMode === "click") toggleMergeSelection(f, l);
                    if (currentMode === "merge") toggleMergeSelection(f, l);
                });
            }
        }).addTo(map);
        setDistrictLoading(code, false);
    }
    function setDistrictLoading(code, loading) {
        $(".districtCheck[value='" + code + "']").prop("disabled", loading);
    }
    async function updateInfoPanel(props, DistrictCode) {
        let html = '';
        console.log("props", props)
        if (layertype == 4) {
            const response = await checkDigitilazation("", DistrictCode, "");
            digitalizationValue = response.d;

            if (digitalizationValue == '1') {
                let color = props.color || "#666";

                html = `
                        <div class="info-header">
                            <span class="color-dot" style="background:${color}"></span>
                            <span class="info-title">EG Block - ${props.BlockName || "-"}</span>
                        </div>

                        <table class="info-table">
                            <tr><th>State</th><td>${props.StateName || "-"}</td></tr>
                            <tr><th>District</th><td>${props.DistrictName || "-"}</td></tr>
                        </table>
                    `;
            }

            else {
                let ismapped = "";
                let color = props.color || "#666";
                let MappingStatus = props.MappingStatus;
                if (MappingStatus == '1')
                    ismapped = 'Mapped'
                else
                    ismapped = 'UnMapped'

                html = `
                            <div class="info-header">
                                <span class="color-dot" style="background:${color}"></span>
                                <span class="info-title">Mapping Status: ${ismapped || "-"}</span>
                            </div>

                            <table class="info-table">
                        <tr><th>EG Village Code</th><td>${props.EGVillageCode || "-"}</td></tr>
                        <tr><th>EG Village Name</th><td>${props.VillageName || "-"}</td></tr>
                        <tr><th>Admin District</th><td>${props.AdminDistrict || "-"}</td></tr>
                        <tr><th>Admin Block</th><td>${props.AdminBlock || "-"}</td></tr>
                            <tr><th>Layer VillageID</th><td>${props.VillageID || "-"}</td></tr>
                            <tr><th>Layer Village Name</th><td>${props.lyr_VillageName || "-"}</td></tr>
                             <tr><th>Layer District</th><td>${props.DistrictName || "-"}</td></tr>
                            <tr><th>Layer Block</th><td>${props.BlockName || "-"}</td></tr>
                        </table>
                        `;
            }
        }
        if (layertype == 3) {
            const response = await checkDigitilazation(DistrictCode, "", "");
            digitalizationValue = response.d;

            if (digitalizationValue == '1') {
                let color = props.color || "#666";

                html = `
    <div class="info-header">
        <span class="color-dot" style="background:${color}"></span>
        <span class="info-title">EG District - ${props.DistrictName || "-"}</span>
    </div>

    <table class="info-table">
        <tr><th>State</th><td>${props.StateName || "-"}</td></tr>
    </table>
`;
            }

            else {
                let ismapped = "";
                let color = props.color || "#666";
                let MappingStatus = props.MappingStatus;
                if (MappingStatus == '1')
                    ismapped = 'Mapped'
                else
                    ismapped = 'UnMapped'

                html = `
                        <div class="info-header">
                            <span class="color-dot" style="background:${color}"></span>
                            <span class="info-title">Mapping Status: ${ismapped || "-"}</span>
                        </div>

                        <table class="info-table">
                        <tr><th>EG Village Code</th><td>${props.EGVillageCode || "-"}</td></tr>
                        <tr><th>EG Village Name</th><td>${props.VillageName || "-"}</td></tr>
                        <tr><th>Admin District</th><td>${props.AdminDistrict || "-"}</td></tr>
                        <tr><th>Admin Block</th><td>${props.AdminBlock || "-"}</td></tr>
                        <tr><th>Layer VillageID</th><td>${props.VillageID || "-"}</td></tr>
                        <tr><th>Layer Village Name</th><td>${props.lyr_VillageName || "-"}</td></tr>
                         <tr><th>Layer District</th><td>${props.DistrictName || "-"}</td></tr>
                        <tr><th>Layer Block</th><td>${props.BlockName || "-"}</td></tr>
                    </table>
                    `;
            }
        }


        if (layertype == 5) {
            const response = await checkDigitilazation("", "", DistrictCode);
            digitalizationValue = response.d;

            if (digitalizationValue == '1') {
                let color = props.color || "#666";

                html = `
                        <div class="info-header">
                            <span class="color-dot" style="background:${color}"></span>
                            <span class="info-title">Cluster - ${props.ClusterName || "-"}</span>
                        </div>

                        <table class="info-table">
                            <tr><th>State</th><td>${props.StateName || "-"}</td></tr>
                            <tr><th>District</th><td>${props.DistrictName || "-"}</td></tr>
                            <tr><th>Block</th><td>${props.BlockName || "-"}</td></tr>
                        </table>
                    `;
            }

            else {
                let ismapped = "";
                let color = props.color || "#666";
                let MappingStatus = props.MappingStatus;
                if (MappingStatus == '1')
                    ismapped = 'Mapped'
                else
                    ismapped = 'UnMapped'

                html = `
                        <div class="info-header">
                            <span class="color-dot" style="background:${color}"></span>
                            <span class="info-title">Mapping Status: ${ismapped || "-"}</span>
                        </div>

                        <table class="info-table">
                        <tr><th>EG Village Code</th><td>${props.EGVillageCode || "-"}</td></tr>
                        <tr><th>EG Village Name</th><td>${props.VillageName || "-"}</td></tr>
                        <tr><th>Admin District</th><td>${props.AdminDistrict || "-"}</td></tr>
                        <tr><th>Admin Block</th><td>${props.AdminBlock || "-"}</td></tr>
                        <tr><th>Layer VillageID</th><td>${props.VillageID || "-"}</td></tr>
                        <tr><th>Layer Village Name</th><td>${props.lyr_VillageName || "-"}</td></tr>
                         <tr><th>Layer District</th><td>${props.DistrictName || "-"}</td></tr>
                        <tr><th>Layer Block</th><td>${props.BlockName || "-"}</td></tr>
                        </table>
                    `;
            }
        }

        let panel = document.getElementById("infoPanel");
        panel.innerHTML = html;
        panel.classList.add("visible");
    }


    function clearInfoPanel() {
        document.getElementById("infoPanel").innerHTML = "<b>Hover over a feature</b>";
    }

    function showHover(e, f, DistrictCode) {
        let layer = e.target;

        //highlightFeature(layer);

        //layer.setStyle({
        //    //weight: 3,
        //    ////color: f.properties.color,
        //    ////fillColor: f.properties.color,
        //    //fillOpacity: 1
        //});

        updateInfoPanel(f.properties, DistrictCode);
    }

    function showHoverreset(e, f) {

        //villageLayer.resetStyle(e.target);
        let layer = e.target;
        //layer.setStyle({
        //   /* fillColor: f.properties.color,*/
        //    /*fillOpacity: 1*/
        //});
        clearInfoPanel();
    }



    function applyBlockColor(blockCode, newColor) {

        geoData.features.forEach(f => {
            if (f.properties.BlockCode === blockCode) {
                f.properties.color = newColor; // ✅ persists forever
            }
        });

        drawMap(); // redraw without recalculating
    }

    function defaultVillageStyle(feature) {
        return {
            color: feature.properties.color || "#666666",
            weight: 1,
            fill: true,
            fillOpacity: 0.4,
            fillColor: feature.properties.color || "#666666"
        };
    }

    //function defaultVillageStyle(feature) {
    //    return {
    //        color: "#444",
    //        weight: 1,
    //        fillColor: "#666",
    //        fillOpacity: 0.6
    //    };
    //}
    function colorVillagesByBlock(BlockCode, BlockName, BlockColorSE, EGBlock) {
        if (!fullLayer) return;

        fullLayer.eachLayer(layer => {
            let f = layer.feature;
            if (!f || !f.properties) return;

            // 🟦 Village layer
            if (layertype == 4 && f.properties.BlockCode === BlockCode) {
                f.properties.color = BlockColorSE;
                f.properties.group = BlockName;
                f.properties.EGBlockCod = EGBlock;

                layer.setStyle({
                    fillColor: BlockColorSE
                });
            }

            // 🟥 District / Block layer
            if (layertype == 3 && f.properties.DistrictCode === BlockCode) {
                f.properties.color = BlockColorSE;
                f.properties.group = BlockName;
                f.properties.EGDistCode = EGBlock;

                layer.setStyle({
                    fillColor: BlockColorSE
                });
            }
        });



        //if (!geoData || !fullLayer) return;

        // if (layertype == 4) {
        //     geoData.features.forEach(f => {
        //         console.log("color geo " + BlockCode + "_" + f.properties.BlockCode + "_" + BlockColorSE);
        //         if (f.properties.BlockCode === BlockCode) {
        //             f.properties.color = BlockColorSE;   // ✅ PERSIST COLOR
        //             f.properties.group = BlockName;
        //             f.properties.EGBlockCod = EGBlock;
        //         }
        //     });
        // }
        // else if (layertype==3) {
        //     geoData.features.forEach(f => {
        //         console.log("color geo " + BlockCode + "_" + f.properties.DistrictCode + "_" + BlockColorSE);
        //         if (f.properties.DistrictCode === BlockCode) {
        //             f.properties.color = rgbToHex(BlockColorSE);   // ✅ PERSIST COLOR
        //             f.properties.group = BlockName;
        //         }
        //     });
        // }
        // // 🔥 FORCE LEAFLET TO REPAINT
        // fullLayer.eachLayer(layer => {
        //     let f = layer.feature;
        //     layer.setStyle({ fillColor: f.properties.color });
        // });
        // console.log("after color " + fullLayer);
        // //drawMap(); // ✅ redraw from geoData
    }

    $("#resetLayerBtn").click(function (e) {
        var Layer = $('#layerDropdown').val();
        var Fyear = $('#YearSelect').val();
        var State = $('#stateSelect').val();
        var District = $('#districtSelect').val();
        var Block = $('#groupDropdown').val();
        var Cluster = $('#clusterDropdown').val();


        if (!Layer) { alert("please select layer type"); return; }
        if (!Fyear) { alert("please select year"); return; }
        if (!State) { alert("please select state"); return; }
        if (!District && layertype === 3) { alert("please select district"); return; }
        if (!Block && layertype === 4) { alert("please select block"); return; }
        if (!Cluster && layertype === 5) { alert("please select cluster"); return; }

        e.preventDefault();
        resetVillageLayer();
    });

    $("#resetLayerBtn1").click(function (e) {
        var Layer = $('#layerDropdown').val();
        var Fyear = $('#YearSelect').val();
        var State = $('#stateSelect').val();
        var District = $('#districtSelect').val();
        var Block = $('#groupDropdown').val();
        var Cluster = $('#clusterDropdown').val();

        if (!Layer) { alert("please select layer type"); return; }
        if (!Fyear) { alert("please select year"); return; }
        if (!State) { alert("please select state"); return; }
        if (!District && layertype === 3) { alert("please select district"); return; }
        if (!Block && layertype === 4) { alert("please select block"); return; }
        if (!Cluster && layertype === 5) { alert("please select cluster"); return; }

        e.preventDefault();
        resetVillageLayer1();
    });

    function resetVillageLayer() {

        if (!geoData || !geoData.features) {
            alert("Layer not loaded.");
            return;
        }

        // 1️⃣ Reset properties
        geoData.features.forEach(f => {
            f.properties.color = "#666666";
            f.properties.group = null;
            delete f.properties.MERGED_FROM;
        });

        // 2️⃣ Reset UI states
        mergeSelection = [];
        currentMode = "click";
        activeGroup = null;
        coloredBlocks = {};
        usedColors.clear();

        // Uncheck block radios
        $("input[name='activeGroup']").prop("checked", false);
        $(".group-item").removeClass("selected");

        // 3️⃣ Rebuild spatial index
        indexAllFeatures();

        // 4️⃣ Redraw map
        drawMap();

        Console.log("Layer reset successfully.");
    }

    function resetVillageLayer1() {
        var Layer = $('#layerDropdown').val();
        var Fyear = $('#YearSelect').val();
        var State = $('#stateSelect').val();
        var District = $('#districtSelect').val();
        var Block = $('#groupDropdown').val();
        var Cluster = $('#clusterDropdown').val();

        if (!geoData || !geoData.features) {
            alert("Layer not loaded.");
            return;
        }
        if (Layer == '4') {
            loadAllVillagesOnce(Block);
        }
        else if (Layer == '3') {
            loadAllVillagesOnce(District);
        }
        else if (Layer == '5') {
            loadAllVillagesOnce(Cluster);
        }


        alert("Layer reset successfully.");
    }

    function selectPolygonsByGroupName(groupName) {

        mergeSelection = [];

        map.eachLayer(layer => {
            if (!layer.feature) return;

            let f = layer.feature;

            if (f.properties.BlockName === groupName) {

                mergeSelection.push(f.properties._id);

                layer.setStyle({
                    weight: 3,
                    //dashArray: "5,5"
                    dashArray: null
                });

            } else {
                layer.setStyle({
                    weight: 1,
                    dashArray: null
                });
            }
        });

        console.log("Selected for merge:", mergeSelection.length);

        if (mergeSelection.length < 2) {
            alert("Not enough polygons to merge for this group.");
        }
    }

    function selectPolygonsByColor(color) {

        if (!color) return;

        mergeSelection = [];

        map.eachLayer(layer => {
            if (!layer.feature) return;

            let f = layer.feature;

            // ✅ MATCH BY COLOR
            if (f.properties.color === color) {

                mergeSelection.push(f.properties._id);

                layer.setStyle({
                    weight: 3,
                    //dashArray: "5,5"
                    dashArray: null
                });

            } else {
                layer.setStyle({
                    weight: 1,
                    dashArray: null
                });
            }
        });

        console.log("Selected for merge:", mergeSelection.length);

        if (mergeSelection.length < 2) {
            alert("Not enough polygons to merge for this color.");
        }
    }

    $("#selectGroupPolygonsBtn").on("click", function () {
        autoSelectAllLayers();
        //if (layertype == 4) {
        //    if (!activeGroupId) {
        //        alert("Please select a block first.");
        //        return;
        //    }
        //    autoSelectCurrentBlock(); 
        //}

        //else if (layertype == 3) {
        //    if (!activeDistrictId) {
        //        alert("Please select a district first.");
        //        return;
        //    }
        //    autoSelectCurrentDistrict();
        //}
    });


    //NEW UNDO MERGE

    $("#undoMergeBtn").click(() => {

        if (mergeUndoStack.length === 0) {
            alert("Nothing to undo.");
            return;
        }

        const undo = mergeUndoStack.pop();

        const mergedId = undo.added;
        const removedIds = undo.removed;

        // 🔁 Remove merged feature
        geoData.features = geoData.features.filter(
            f => f.properties._id !== mergedId
        );

        delete shapesIndex[mergedId];
        rtree.remove({ id: mergedId });

        // 🔄 Restore originals from backup
        removedIds.forEach(id => {
            const original = deletedFeatureBackup[id];
            if (!original) return;

            geoData.features.push(original);
            shapesIndex[id] = original;

            const b = turf.bbox(original);
            rtree.insert({
                minX: b[0],
                minY: b[1],
                maxX: b[2],
                maxY: b[3],
                id: id
            });
        });

        mergeSelection = [];
        currentMode = "click";

        drawMap();
        alert("Merge undone successfully!");
    });



    //UNDO MERGE WORKING 

    //$("#undoMergeBtn").click(() => {

    //    if (mergeUndoStack.length === 0) {
    //        alert("Nothing to undo.");
    //        return;
    //    }

    //    geoData.features = mergeUndoStack.pop();

    //    rebuildSpatialIndex();
    //    drawMap();

    //    mergeSelection = [];
    //    //updateMergePreview();

    //    alert("Merge undone successfully!");
    //});

    function rebuildSpatialIndex() {
        rtree.clear();
        shapesIndex = {};

        geoData.features.forEach(f => {
            let id = f.properties._id;
            shapesIndex[id] = f;

            let bbox = turf.bbox(f);
            rtree.insert({
                minX: bbox[0],
                minY: bbox[1],
                maxX: bbox[2],
                maxY: bbox[3],
                id
            });
        });
    }
    function applyCurrentGroupColoring() {

        const opt = $("#groupDropdown").find(":selected");
        let active_GroupId = opt.val();
        let active_GroupName = opt.text().trim();
        let selected_Color = opt.data("color");
        let active_EGBlock = opt.data("egblock");
        let isblockmapped = opt.data("ismapped");

        if (!geoData || !geoData.features) return;

        geoData.features.forEach(f => {

            if (!f.properties) f.properties = {};

            if (layertype === 4 && activeGroupId) {

                if (f.properties.BlockCode === activeGroupId) {
                    f.properties.color = selectedBlockColor;
                }
            }
            if (layertype === 5 && activeGroupId) {

                if (f.properties.ClusterCode === activeGroupId) {
                    f.properties.color = selected_Color;
                }
            }
            if (layertype === 3 && activeDistrictId) {
                if (f.properties.DistrictCode === activeDistrictId) {
                    f.properties.color = selectedBlockColor;
                }
            }
        });

        if (fullLayer) {
            fullLayer.eachLayer(layer => {
                const f = layer.feature;
                if (!f || !f.properties) return;

                layer.setStyle({
                    color: f.properties.color || "#FFFFFFFF",
                    fillColor: f.properties.color || "#666666",
                    fillOpacity: 0.7
                });
            });
        }
    }

    /* ===========================================================
   EXPORT SHAPEFILE
===========================================================*/


    $("#exportBtn").click(async function (e) {



        var Layer = $('#layerDropdown').val();
        var Fyear = $('#YearSelect').val();
        var State = $('#stateSelect').val();
        var District = $('#districtSelect').val();
        var Block = $('#groupDropdown').val();
        var Cluster = $('#clusterDropdown').val();

        if (!Layer) { alert("please select layer type"); return; }
        if (!Fyear) { alert("please select year"); return; }
        if (!State) { alert("please select state"); return; }
        if (!District && layertype === 3) { alert("please select district"); return; }
        if (!Block && layertype === 4) { alert("please select block"); return; }
        if (!Cluster && layertype === 5) { alert("please select cluster"); return; }

        e.preventDefault();

        $("#mapLoader").show();

        if (!geoData || !geoData.features?.length) {
            alert("No polygons to Save!");
            $("#mapLoader").hide();
            return;
        }

        var coloredFeatures = geoData.features.filter(f =>
            f.geometry &&
            f.properties?.color &&
            f.properties.color !== "#666666"
        );

        if (!coloredFeatures.length) {
            //alert("No colored villages to export!");
            alert("Village mapping is not available for this layer!");
            $("#mapLoader").hide();
            return;
        }

        // 🔹 New export naming logic
        let exportName = "EG_Export";

        if (layertype === 3 && activeDistrictId) {
            exportName = $("#districtSelect option:selected").text().trim();

            const opt = $("#districtSelect").find(":selected");

            activeGroupId = opt.val();
            activeGroupName = opt.text();
            selectedColor = opt.data("color");
            activeEGDistrict = opt.data("egdistrict");

        }
        else if (layertype === 4 && activeGroupId) {
            exportName = $("#groupDropdown option:selected").text().trim();

            const opt = $("#groupDropdown").find(":selected");

            activeGroupId = opt.val();
            activeGroupName = opt.text();
            selectedColor = opt.data("color");
            activeEGBlock = opt.data("egblock");
        }
        else if (layertype === 5 && activeGroupId) {
            exportName = $("#clusterDropdown option:selected").text().trim();

            const opt = $("#clusterDropdown").find(":selected");

            activeGroupId = opt.val();
            activeGroupName = opt.text();
            selectedColor = opt.data("color");
            activeEGBlock = opt.data("egblock");
        }


        let exportGeoJSON = {
            type: "FeatureCollection",
            features: coloredFeatures.map(f => {

                let props = { FYear: $('#YearSelect').val() || "" };

                props.EGBLOCKCOD = activeEGBlock || "";
                props.EGDISTCOD = activeEGDistrict || "";
                props.CLUSTERCODE = Cluster || "";

                return {
                    type: "Feature",
                    geometry: f.geometry,
                    properties: props
                };
            })
        };



        $.ajax({
            url: "GISEGBlock.aspx/ExportShapefile",
            type: "POST",
            data: JSON.stringify({
                fileName: exportName,
                geojson: exportGeoJSON,
                layertype: layertype
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (res) {
                $("#mapLoader").hide();
                alert(res.d);
                if (layertype === 3)
                    loadAllVillagesOnce(activeDistrictId);
                if (layertype === 4)
                    loadAllVillagesOnce(activeGroupId);
                if (layertype === 5)
                    loadAllVillagesOnce(activeGroupId);

            },
            error: function (err) {
                $("#mapLoader").hide();
                console.error(err.responseText);
            }
        });
    });

    /* ===========================================================
DELETE SHAPEFILE
===========================================================*/

    $("#deleteLayerBtn").click(async function (e) {



        let districtcode = "";
        let blockcode = "";
        let fyear = "";
        let DistrictCode = "";
        let BlockCode = "";
        let ClusterCode = "";

        var Fyear = $('#YearSelect').val();
        var State = $('#stateSelect').val();

        if (!layertype) { alert("please select layer type"); return; }
        if (!Fyear) { alert("please select year"); return; }
        if (!State) { alert("please select state"); return; }


        $("#mapLoader").show();

        if (layertype === 3) {

            const opt = $("#districtSelect").find(":selected");

            DistrictCode = opt.val();
            if (!DistrictCode) { alert("please select District"); return; }
        }
        else if (layertype === 4) {
            const optd = $("#districtSelect").find(":selected");

            DistrictCode = optd.val();

            const opt = $("#groupDropdown").find(":selected");

            BlockCode = opt.val();

            if (!BlockCode) { alert("please select Block"); return; }
        }
        else if (layertype === 5) {
            const optd = $("#districtSelect").find(":selected");

            DistrictCode = optd.val();

            const opt = $("#groupDropdown").find(":selected");

            BlockCode = opt.val();

            const optc = $("#clusterDropdown").find(":selected");

            ClusterCode = optc.val();

            if (!BlockCode) { alert("please select Block"); return; }

            if (!ClusterCode) { alert("please select Cluster"); return; }
        }
        else {
            alert("please select layer type"); return;
        }


        $.ajax({
            url: "GISEGBlock.aspx/DeleteLayer",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                districtcode: DistrictCode,
                blockcode: BlockCode,
                ClusterCode: ClusterCode,
                fyear: Fyear
            }),
            success: function (response) {
                alert("Layer deleted successfully.");

                if (layertype === 3) {
                    loadAllVillagesOnce(DistrictCode);

                } else if (layertype === 4) {
                    loadAllVillagesOnce(BlockCode);
                }
                else if (layertype === 5) {
                    loadAllVillagesOnce(ClusterCode);
                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {
                $("#mapLoader").hide();
            }
        });

    });

    function checkDigitilazation(district, block, cluster) {
        return $.ajax({
            url: "GISEGBlock.aspx/isDigitalize",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                districtcode: district,
                blockcode: block,
                clustercode: cluster,
                fyear: $('#YearSelect').val()
            })
        });
    }

    </script>

</asp:Content>

