<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Culture="en-GB" CodeFile="GISVillageMapping.aspx.cs" Inherits="GISVillageMapping" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%--<link href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" rel="stylesheet" />--%>
    <link href="css/CSS/jquery-ui.css" rel="stylesheet" />

    <%--<script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/jquery-ui.min.js"></script>

    <script src="Scripts/comman.js" type="text/javascript"></script>
    <%--<link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css" />--%>

    <link rel="stylesheet" href="css/CSS/jquery.dataTables.min.css" />

    <%--<script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/jquery.dataTables.min.js"></script>



    <script src="leaflet2/leaflet.js" type="text/javascript"></script>
    <link href="leaflet2/leaflet.css" rel="stylesheet" type="text/css" />
    <link href="leaflet2/leaflet.fullscreen.css" rel="stylesheet" type="text/css" />
    <script src="leaflet2/Leaflet.fullscreen.js" type="text/javascript"></script>
    <script src="leaflet2/leaflet.zoomhome.min.js" type="text/javascript"></script>
    <link href="Leaflet2/LeafletClustersMarkers/MarkerCluster.Default.css" rel="stylesheet" />
    <link href="Leaflet2/LeafletClustersMarkers/MarkerCluster.css" rel="stylesheet" />

    <script type="text/javascript" src="Leaflet2/leaflet.spin.js"></script>
    <script type="text/javascript" src="Leaflet2/leaflet.spin.min.js"></script>

    <script type="text/javascript" src="Leaflet2/LeafletClustersMarkers/leaflet.markercluster.js"></script>
    <%--<script src="https://cdn.jsdelivr.net/npm/leaflet-easybutton@2/src/easy-button.js"></script>--%>
    <script type="text/javascript" src="js/JS/easy-button.js"></script>

    <%--<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/leaflet-easybutton@2/src/easy-button.css">--%>
    <link rel="stylesheet" href="css/CSS/easy-button.css">

    <script type="text/javascript" src="Leaflet2/bundle.js"></script>

    <script type="text/javascript" src="Leaflet2/leaflet.groupedlayercontrol.min.js"></script>

    <script type="text/javascript" src="Leaflet2/leaflet.spin.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="Leaflet2/L.Control.Locate.js"></script>
    <script type="text/javascript" src="Leaflet2/leaflet-search.js"></script>
    <link href="Leaflet2/leaflet-search.css" rel="stylesheet" type="text/css" />

    <%--<script type="text/javascript" src="https://cdn.datatables.net/1.13.7/js/jquery.dataTables.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/jquery.dataTables.min.js"></script>

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


    <style>
        /* ✅ FIXED MAP STYLING */
        #map {
            width: 100% !important;
            height: calc(100vh - 300px) !important;
            margin-top: -15px;
            margin-bottom: -15px;
            border: 1px solid #ddd;
            border-radius: 6px;
            position: relative;
            z-index: 1;
        }

        #map2 {
            width: 100% !important;
            height: calc(100vh - 310px) !important;
            margin-top: 0px;
            margin-bottom: 0px;
            border: 1px solid #ddd;
            border-radius: 6px;
            position: relative;
            z-index: 1;
        }

        /* Parent container bhi proper size mein hona chahiye */
        .panel-body {
            width: 100%;
        }

        /* Legend styling */
        .info.legend {
            padding: 6px 8px;
            background: white;
            background: rgba(255,255,255,0.8);
            box-shadow: 0 0 15px rgba(0,0,0,0.2);
            border-radius: 5px;
            line-height: 18px;
            color: #555;
        }

            .info.legend i {
                width: 18px;
                height: 18px;
                float: left;
                margin-right: 8px;
                opacity: 0.7;
                border: 1px solid #999;
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

        #myButton2 {
            background-image: url('images/search-29.png');
            background-color: transparent;
            width: 30px;
            height: 30px;
            border: none;
            cursor: pointer;
        }

        .mandatory-label::after {
            content: "*";
            color: red;
            margin-left: 4px;
        }

        #myButton {
            background-image: url('images/search-29.png');
            background-color: transparent;
            width: 30px;
            height: 30px;
            border: none;
            cursor: pointer;
        }

        .legendCSS {
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

        div#tblLocDetails1_filter {
            text-align: end;
        }

        div#suggestTable_filter {
            text-align: end;
        }

        #tblLocDetails_wrapper row:nth-child(2) {
            margin: 0px !important;
        }

        #tblLocDetails1_wrapper row:nth-child(2) {
            margin: 0px !important;
        }

        #suggestTable_wrapper row:nth-child(2) {
            margin: 0px !important;
        }

        .search-bg {
            background: linear-gradient(to bottom, #ebf1fd 0%,#ffffff 100%) !important;
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
            padding: 0px 15px;
        }

        .MapSummary-wrp .dataTables_wrapper .row:nth-child(2) .col-sm-12 {
            overflow: auto;
        }

        #MapSummary table thead {
            width: calc(100% - 10px) !important;
        }

        #MapSummary1 table thead {
            width: calc(100% - 10px) !important;
        }

        #MapSummary2 table thead {
            width: calc(100% - 10px) !important;
        }

        /* #MapSummary table {
            background: linear-gradient(to bottom, #ffe5e6 0%,#fff8f8 100%);
        }

        #MapSummary1 table {
            background: linear-gradient(to bottom, #ffe5e6 0%,#fff8f8 100%);
        }

        #MapSummary2 table {
            background: linear-gradient(to bottom, #ffe5e6 0%,#fff8f8 100%);
        }*/

        ::-webkit-scrollbar {
            width: 10px;
            height: 10px;
        }

        #MapSummary table thead tr th:nth-last-child(1) {
            border-right: 0px;
        }

        #MapSummary1 table thead tr th:nth-last-child(1) {
            border-right: 0px;
        }

        #MapSummary2 table thead tr th:nth-last-child(1) {
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

        #MapSummary table tbody::-webkit-scrollbar {
            width: 10px;
            height: 10px;
        }

        #MapSummary1 table tbody::-webkit-scrollbar {
            width: 10px;
            height: 10px;
        }

        #MapSummary2 table tbody::-webkit-scrollbar {
            width: 10px;
            height: 10px;
        }

        #MapSummary table tbody::-webkit-scrollbar-track {
            -webkit-box-shadow: inset 0 0 6px red;
            -webkit-border-radius: 10px;
            border-radius: 10px;
        }

        #MapSummary1 table tbody::-webkit-scrollbar-track {
            -webkit-box-shadow: inset 0 0 6px red;
            -webkit-border-radius: 10px;
            border-radius: 10px;
        }

        #MapSummary2 table tbody::-webkit-scrollbar-track {
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

        #MapSummary1 table tbody::-webkit-scrollbar-thumb {
            -webkit-border-radius: 10px;
            border-radius: 10px;
            background: #fff8f8;
            -webkit-box-shadow: inset 0 0 6px #6D6D6D;
        }

        #MapSummary2 table tbody::-webkit-scrollbar-thumb {
            -webkit-border-radius: 10px;
            border-radius: 10px;
            background: #fff8f8;
            -webkit-box-shadow: inset 0 0 6px #6D6D6D;
        }

        #MapSummary table tbody::-webkit-scrollbar-thumb:window-inactive {
            background: #333;
        }

        #MapSummary1 table tbody::-webkit-scrollbar-thumb:window-inactive {
            background: #333;
        }

        #MapSummary2 table tbody::-webkit-scrollbar-thumb:window-inactive {
            background: #333;
        }

        #MapSummary table tbody {
            display: block;
            height: calc(100vh - 385px);
            width: 100%;
            /* overf
        low-y: auto; */
            /* overflow-x: hidden !important; */
        }

        /*        #MapSummary table tbody {
            display: block;
            height: 280px;
            width: 100%;
            overflow-y: auto;
            overflow-x: hidden !important
        }*/

        #MapSummary1 table tbody {
            display: block;
            height: calc(100vh - 412px);
            width: 100%;
            /* overflow-y: auto;
            overflow-x: hidden !important*/
        }

        #MapSummary2 table tbody {
            display: block;
            height: calc(100vh - 369px);
            width: 100%;
            /* overflow-y: auto;
            overflow-x: hidden !important*/
        }

        #MapSummary table thead, tbody tr {
            display: table;
            width: 100%;
            table-layout: fixed;
        }

        #MapSummary1 table thead, tbody tr {
            display: table;
            width: 100%;
            table-layout: fixed;
        }

        #MapSummary2 table thead, tbody tr {
            /*display: table;*/
            width: 100%;
            table-layout: fixed;
        }

        #MapSummary table thead tr th {
            width: 80px !important;
            /*background: linear-gradient(to bottom, #ffe5e6 0%,#fff8f8 100%);*/
        }

        #MapSummary1 table thead tr th {
            width: 80px !important;
            /*background: linear-gradient(to bottom, #ffe5e6 0%,#fff8f8 100%);*/
        }

        #MapSummary2 table thead tr th {
            width: 80px !important;
            /*background: linear-gradient(to bottom, #ffe5e6 0%,#fff8f8 100%);*/
        }

        table#tblLocDetails {
            margin: 0px;
        }

        #MapSummary table tbody tr td {
            width: 80px !important
        }

        #MapSummary1 table tbody tr td {
            width: 80px !important
        }

        #MapSummary2 table tbody tr td {
            width: 80px !important
        }


        #MapSummary table thead tr th:nth-last-child(1) {
            width: 200px !important;
        }

        #MapSummary1 table thead tr th:nth-last-child(1) {
            width: 200px !important;
        }

        #MapSummary2 table thead tr th:nth-last-child(1) {
            width: 200px !important;
        }

        #MapSummary table tbody tr td:nth-last-child(1) {
            width: 200px !important
        }

        #MapSummary1 table tbody tr td:nth-last-child(1) {
            width: 200px !important
        }

        #MapSummary2 table tbody tr td:nth-last-child(1) {
            width: 200px !important
        }

        #MapSummary table thead tr th:nth-child(1) {
            width: 60px !important
        }

        #MapSummary1 table thead tr th:nth-child(1) {
            width: 60px !important
        }

        #MapSummary2 table thead tr th:nth-child(1) {
            width: 60px !important
        }

        #MapSummary table tbody tr td:nth-child(1) {
            width: 60px !important
        }

        #MapSummary1 table tbody tr td:nth-child(1) {
            width: 60px !important
        }

        #MapSummary2 table tbody tr td:nth-child(1) {
            width: 60px !important
        }




         #MapSummary table thead tr th:nth-child(2) {
     width: 120px !important
 }

 #MapSummary1 table thead tr th:nth-child(2) {
     width: 120px !important
 }

 #MapSummary2 table thead tr th:nth-child(2) {
     width: 120px !important
 }

 #MapSummary table tbody tr td:nth-child(2) {
     width: 120px !important
 }

 #MapSummary1 table tbody tr td:nth-child(2) {
     width: 120px !important
 }

 #MapSummary2 table tbody tr td:nth-child(2) {
     width: 120px !important
 }






        #MapSummary table tbody tr td, #MapSummary table thead tr th {
            vertical-align: middle;
            text-align: center
        }

        #MapSummary1 table tbody tr td, #MapSummary1 table thead tr th {
            vertical-align: middle;
            text-align: center
        }

        #MapSummary2 table tbody tr td, #MapSummary2 table thead tr th {
            vertical-align: middle;
            text-align: center
        }

        .inner-section {
            background: #fbfbfb;
        }



        .MapSummary-wrp .dataTables_wrapper .row:nth-child(2) {
            overflow: hidden;
            margin-left: -15px !important;
            margin-right: -15px !important;
        }

            .MapSummary-wrp .dataTables_wrapper .row:nth-child(2) .col-sm-12 {
                padding: 0px 4px;
            }

        .form-group {
            margin-bottom: 5px;
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

            #MapSummary1 table thead tr th:nth-last-child(1) {
                width: 150px !important;
            }

            #MapSummary2 table thead tr th:nth-last-child(1) {
                width: 150px !important;
            }

            #MapSummary table tbody tr td:nth-last-child(1) {
                width: 150px !important
            }

            #MapSummary1 table tbody tr td:nth-last-child(1) {
                width: 150px !important
            }

            #MapSummary2 table tbody tr td:nth-last-child(1) {
                width: 150px !important
            }

            #MapSummary table thead tr th:nth-child(1) {
                width: 100px !important
            }

            #MapSummary1 table thead tr th:nth-child(1) {
                width: 100px !important
            }

            #MapSummary2 table thead tr th:nth-child(1) {
                width: 100px !important
            }

            #MapSummary table tbody tr td:nth-child(1) {
                width: 100px !important
            }

            #MapSummary1 table tbody tr td:nth-child(1) {
                width: 100px !important
            }

            #MapSummary2 table tbody tr td:nth-child(1) {
                width: 100px !important
            }
        }

        .leaflet-control-zoom {
            display: block !important;
        }

        table.dataTable thead > tr > th.sorting, table.dataTable thead > tr > th.sorting_asc {
            padding-right: 10px !important;
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

        .match-green {
            background-color: #d4f5d2 !important;
            font-weight: bold;
        }

        .pagination > li > a,
        .pagination > li > span {
            position: relative;
            float: left;
            padding: 4px 12px;
            margin-left: -30px !important;
            line-height: 1.42857143;
            color: #337ab7;
            text-decoration: none;
            background-color: #fff;
            border: 1px solid #ddd;
        }
    </style>

    <style>
        .legend {
            background: white;
            padding: 8px;
            line-height: 18px;
            color: #555;
            border-radius: 5px;
            box-shadow: 0 0 5px rgba(0,0,0,0.3);
            font-size: 12px;
        }

            .legend i {
                width: 14px;
                height: 14px;
                float: left;
                margin-right: 6px;
                opacity: 0.9;
            }
    </style>

    <style type="text/css">
        div#tblLocDetails_paginate {
            display: none;
        }

        div#tblLocDetails1_paginate {
            display: none;
        }

        div#suggestTable_paginate {
            display: none;
        }

        div#tblLocDetails_info {
            display: none;
        }

        div#tblLocDetails1_info {
            display: none;
        }

        div#suggestTable_info {
            display: none;
        }

        #suggestTable_wrapper .row .col-sm-12 {
            padding: 0px !important
        }

        table.dataTable tbody th, table.dataTable tbody td {
            padding: 4px 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="updpnlgis">
        <ContentTemplate>


            <div class="container-fluid" style="margin-top: 0px;">

                <div class="update_overlay">
                    <div class="update_div">
                        <img src="images/progress2.gif" />
                    </div>
                </div>

                <div class="row" style="margin-top: 0px;">
                    <div class="col-sm-12">
                        <div class="panel panel-default" style="background: linear-gradient(to bottom,  #ffffff 1%,#ffffff 1%,#ebf1fd 100%) !important; margin-bottom: 3px;">
                            <div class="panel-heading" style="background-color: transparent; padding: 5px 10px;">

                                <div class="row" style="margin-left: -15px; margin-right: -15px">
                                    <div class="col-sm-12" style="padding-right: 0px;">
                                        <div class="dis-flex">
                                            <h3 class="text-danger1" style="margin: 0px;">
                                                <asp:Label ID="lblMain" runat="server" Text="GIS Village Mapping" Style="margin: 3px 0px 5px 5px; font-weight: bold; font-size: medium;"></asp:Label>
                                            </h3>
                                            <button type="button" id="btnexportMapped" style="margin-left: auto;" class="btn-link" onclick="ExportMapped();">Export Mapping Data to Excel</button>
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
                            <div class="panel panel-default" style="margin-bottom: 4px;">
                                <div class="panel-body" style="padding-top: 0px; padding-bottom: 0px;">
                                    <div class="row" style="margin: 0px -15px;">
                                        <div class="col-lg-12  search-bg">
                                            <div id="container-target">
                                                <div class="form-horizontal">
                                                    <div class="row" style="margin: 0px -15px 0px -15px;">
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3  linhei" style="padding-top: 2px; font-weight: bold !important;">
                                                                    Year:<span class="mandatory-label"></span>
                                                                </label>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList ID="ddlYear" runat="server" onchange="bindMasterYear();" class="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="ddlState" class="col-sm-3  linhei" style="padding-top: 2px; font-weight: bold !important;">State:<span class="mandatory-label"></span></label>
                                                                <div class="col-sm-9 ">
                                                                    <asp:DropDownList ID="ddlState" runat="server"
                                                                        onchange="Fill_District('ddlDistrict'); Fill_Block('ddlBlock');"
                                                                        class="form-control">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="ddlDistrict" class="col-sm-3 linhei" style="padding-top: 2px; font-weight: bold !important;">District:<span class="mandatory-label"></span></label>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList ID="ddlDistrict" runat="server"
                                                                        onchange="Fill_Block('ddlBlock');getAdminDistrict();"
                                                                        class="form-control">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="ddlBlock" class="col-sm-3  linhei" style="padding-top: 2px; font-weight: bold !important;">Block:<span class="mandatory-label"></span></label>
                                                                <div class="col-sm-9 ">
                                                                    <asp:DropDownList ID="ddlBlock" runat="server"
                                                                        onchange="show_hide_div();Run_functions();"
                                                                        class="form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="ddlMatchingType" class="col-sm-5  linhei" style="padding-top: 2px; padding-right: 0; font-weight: bold !important;">Matching Type:</label>
                                                                <div class="col-sm-7 " style="padding-left: 10px; padding-right: 0px;">
                                                                    <asp:DropDownList ID="ddlMatchingType" runat="server" class="form-control" onchange="show_hide_div();Run_functions();">
                                                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                                        <asp:ListItem Text="Fuzzy logic" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Bulk Mapping" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Manual Mapping" Value="3"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12" style="display: none;">
                                                            <div class="form-group">
                                                                <label for="ddlGP" class="col-sm-3  linhei" style="padding-top: 2px; font-weight: bold !important;">Cluster:</label>
                                                                <div class="col-sm-9 ">
                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlGP" runat="server" class="form-control"></asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <input type="button" id="myButton" class="btn btn-danger btn-paddd" style="margin-left: -4rem; display: none;" />
                                                                    <button type="button" id="btnMaplayer" class="btn-link" onclick="runFuzzy();">Run fuzzy mapping</button>
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


                    <asp:Panel ID="Panel1" runat="server">
                        <div class="col-sm-12">
                            <div class="panel panel-default" style="background: linear-gradient(to bottom,  #ffffff 1%,#ffffff 1%,#ebf1fd 100%) !important; margin-bottom: 4px;">
                                <div class="panel-heading" style="background-color: transparent; padding: 5px 10px;">

                                    <div class="row" style="margin-left: -15px; margin-right: -15px">
                                        <div class="col-sm-12" style="padding-right: 0px;">
                                            <div class="dis-flex">
                                                <h3 class="text-danger1" style="margin: 0px;">
                                                    <asp:Label ID="Label1" runat="server" Text="Bulk Mapping" Style="margin: 3px 0px 5px 5px; font-weight: bold; font-size: medium;"></asp:Label>
                                                </h3>

                                            </div>

                                        </div>

                                    </div>

                                </div>
                                <div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="panel panel-default" style="margin-bottom: 0px">
                                <div class="panel-body" style="padding: 4px;">
                                    <div class="row" style="margin-left: -15px; margin-right: -15px;">
                                        <div class="col-lg-2 col-md-2 col-sm-12" style="padding-right: 3px">
                                            <div class="">
                                                <div class="panel-heading search-bg">
                                                    <h5><b>Enter Matching Code</b></h5>
                                                </div>
                                                <div style="padding-left: 0px; padding-right: 0px">
                                                    <div class="row">
                                                        <div class="col-sm-12" style="padding-left: 0px; padding-right: 0px">
                                                            <textarea rows="20" id="txt_MachingCode" style="height: calc(100vh - 340px);" class="form-control"></textarea>
                                                            <div style="margin-top: 10px; text-align: center;">
                                                                <button type="button"
                                                                    id="btnRunMapping"
                                                                    class="btn-link">
                                                                    Update Matching
                                                                </button>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>




                                        <div class="col-lg-6 col-md-6 col-sm-12" style="padding-right: 3px; padding-left: 3px">
                                            <div class="panel panel-default" style="margin-bottom: 0px">
                                                <div class="row panel-heading search-bg">
                                                    <div style="padding-left: 0px; display: flex; justify-content: flex-start; align-items: center; gap: 10px; width: 100%;">
                                                        <h5><b>MAP</b></h5>

                                                        <div style="display: flex; justify-content: flex-start; flex-direction: row; width: 100%; gap: 10px;">
                                                            <input type="text" class="form-control" id="latitudelongitudeInput" placeholder="Search: Latitude,Longitude" />
                                                            <div class="btn-primary-searh-map">
                                                                <div class="position-relative">
                                                                    <input type="button" class="btn btn-sm search-mp btn-primary" onclick="gotolatlong()" />
                                                                    <i class="fa fa-search" aria-hidden="true"></i>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>




                                                </div>
                                                <div class="panel-body" style="padding-left: 0px; padding-right: 0px">
                                                    <div class="row">
                                                        <div class="col-sm-12" style="padding-left: 0px; padding-right: 0px">
                                                            <div id="map"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-lg-4 col-md-4 col-sm-12" style="padding-left: 3px">
                                            <div class="panel panel-default" style="margin-bottom: 0px">
                                                <div class="panel-heading search-bg dis-flex" style="padding-left: 15px">
                                                    <h5 style="white-space: nowrap;margin-top: 5px;"><b>Matching Detail</b></h5>
                                                    <input type="text" id="txtSearch" style="margin-left: 13px; margin-bottom: 7px; padding: 5px; width: 80%; padding-left: 11px;" class="form-control" placeholder="Search..." onkeyup="searchTable()" />
                                                </div>
                                                <div class="panel-body" style="padding: 4px">
                                                    <div class="row" style="margin-left: -4px; margin-right: -4px;">

                                                        <div class="col-sm-12">

                                                            <div class="MapSummary-wrp">
                                                                <div id="MapSummary" class="">
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

                        </div>
                    </asp:Panel>




                    <asp:Panel ID="Panel2" runat="server">
                        <div class="col-sm-12">
                            <div class="panel panel-default" style="background: linear-gradient(to bottom,  #ffffff 1%,#ffffff 1%,#ebf1fd 100%) !important; margin-bottom: 8px;">
                                <div class="panel-heading" style="background-color: transparent; padding: 5px 10px;">

                                    <div class="row" style="margin-left: -15px; margin-right: -15px">
                                        <div class="col-sm-12" style="padding-right: 0px;">
                                            <div class="dis-flex">
                                                <h3 class="text-danger1" style="margin: 0px;">
                                                    <asp:Label ID="Label2" runat="server" Text="Manual Mapping" Style="margin: 3px 0px 5px 5px; font-weight: bold; font-size: medium;"></asp:Label>
                                                </h3>

                                            </div>

                                        </div>

                                    </div>

                                </div>
                                <div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="panel panel-default" style="margin-bottom: 0px;">
                                <div class="panel-body" style="padding: 4px">
                                    <div class="row" style="margin-left: -15px; margin-right: -15px;">
                                        <div class="col-lg-4 col-md-4 col-sm-12" style="padding-right: 3px;">
                                            <div class="panel panel-default" style="margin: 0px;">
                                                <div class="panel-heading search-bg dis-flex" style="padding-left: 15px;">
                                                    <h5><b>PMS Data</b></h5>
                                                    <input type="text" id="txtSearch2" style="margin-left: 13px; margin-bottom: 7px; padding: 5px; width: 80%; padding-left: 11px;" class="form-control" placeholder="Search..." onkeyup="searchTable2()" />
                                                </div>
                                                <div class="panel-body" style="padding: 4px;">


                                                    <div class="row" style="margin: 0px -4px;">
                                                        <div class="col-sm-12" style="margin-bottom: 12px; padding-left: 0px; padding-right: 0px; display: none;">
                                                            <label class="col-sm-1 linhei" style="padding-top: 2px; font-weight: bold !important;">Status:</label>
                                                            <div class="col-sm-3">
                                                                <asp:DropDownList ID="DropDownList1" runat="server" onchange="loadAll();" class="form-control">
                                                                    <asp:ListItem Text="Unmapped" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Mapped" Value="1"></asp:ListItem>

                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12">
                                                            <input id="txtSearchMIS1" class="search" style="display: none;" placeholder="Search MIS villages..." />
                                                            <div class="MapSummary-wrp">
                                                                <div id="MapSummary1" class="">
                                                                </div>
                                                            </div>
                                                            <div class="margin-top: 10px;" style="text-align: center;">
                                                                <button type="button" id="btnMapVillages" onclick="saveVillages();" class="btn-link" style="margin-left: 25px;">
                                                                    Save Mapping
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-4 col-md-4 col-sm-12" style="padding-left: 3px; padding-right: 3px;">
                                            <div class="panel panel-default" style="margin: 0px;">
                                                <div class="row panel-heading search-bg">

                                                    <div style="padding-left: 0px; display: flex; justify-content:flex-start; align-items: center; gap: 10px; width: 100%;">
                                                        <h5><b>MAP</b></h5>

                                                        <div style="display: flex; justify-content: flex-start; flex-direction: row; width: 100%; gap: 10px;">
                                                            <input type="text" class="form-control" id="latitudelongitudeInput1" placeholder="Search: Latitude,Longitude" />
                                                            <div class="btn-primary-searh-map">
                                                                <div class="position-relative">
                                                                    <input type="button" class="btn btn-sm search-mp btn-primary" onclick="gotolatlong1()" />
                                                                    <i class="fa fa-search" aria-hidden="true"></i>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>





                                                </div>
                                                <div class="panel-body" style="padding: 4px;">
                                                    <div class="row">
                                                        <div class="col-sm-12" style="padding-left: 0px; padding-right: 0px">
                                                            <div id="map2"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-lg-4 col-md-4 col-sm-12" style="padding-left: 3px;">
                                            <div class="panel panel-default" style="margin: 0px;">
                                                <div class="panel-heading search-bg dis-flex" style="padding-left: 15px;gap: 15px;">
                                                    <h5 style="white-space: nowrap;margin-top: 0px;"><b>GIS Data</b></h5>

                                                    <input type="text" id="txtSearch1" style="margin-left: 0px; margin-bottom: 10px; padding: 5px; width: 100%; padding-left: 11px;" class="form-control" placeholder="Search..." onkeyup="searchTable1()" />
                                                </div>
                                                <div class="panel-body" style="padding-bottom: 0px; padding: 0px">
                                                    <div class="row" style="margin-left: 0px;">
                                                        <div class="col-sm-12" style="padding: 4px 18px">

                                                            <input id="txtSearchSuggest1" class="search" style="display: none;" placeholder="Type to find suggestions (or click MIS/Layer)..." />

                                                            <div class="MapSummary-wrp">


                                                                <div id="MapSummary2">
                                                                </div>
                                                            </div>


                                                            <div style="margin-top: 10px; margin-left: 30px; display: none;">
                                                                <button type="button" onclick="saveSuggestedVillages();" class="btn btn-primary">Save</button>
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
                    </asp:Panel>

                </div>
            </div>


            <script type="text/javascript">

                // ✅ FIXED Run_functions
                async function Run_functions() {
                    var fyear = $("[id$=ddlYear]").val();
                    var state = $("[id$=ddlState]").val();
                    var district = $("[id$=ddlDistrict]").val()?.split("#")[0];
                    var block = $("[id$=ddlBlock]").val()?.split("#")[0];

                    if (!fyear) { alert("please select year"); return; }
                    if (!state) { alert("please select state"); return; }
                    if (!district) { alert("please select district"); return; }
                    if (!block) { alert("please select block"); return; }

                    var ddlMatchingType = $("[id$=ddlMatchingType]").val();

                    const response = await checkDigitilazation("", block, "");
                    digitalizationValue = response.d;
                    console.log("digitalizationValue", digitalizationValue);


                    if (digitalizationValue === "1") {
                        $("#btnRunMapping").hide();
                        $("#btnMapVillages").hide();
                    } else {
                        $("#btnRunMapping").show();
                        $("#btnMapVillages").show();
                    }

                    //if (!ddlMatchingType) { alert("please select matching type"); return; }
                    if (ddlMatchingType === '1') {
                        /*runFuzzy();*/
                    }
                    else if (ddlMatchingType === '2') {
                        bindBlockVillage('', '');
                        loadVillages();
                    }
                    else if (ddlMatchingType === '3') {
                        bindBlockVillage2('', '');
                        bindGISVillages();
                        loadUnmappedVillages();
                    }
                }

                // ✅ FIXED show_hide_div
                function show_hide_div() {
                    var panel1 = $("#<%= Panel1.ClientID %>");
                    var panel2 = $("#<%= Panel2.ClientID %>");

                    panel1.hide();
                    panel2.hide();

                    var fyear = $("[id$=ddlYear]").val();
                    var state = $("[id$=ddlState]").val();
                    var district = $("[id$=ddlDistrict]").val()?.split("#")[0];
                    var block = $("[id$=ddlBlock]").val()?.split("#")[0];

                    if (!fyear || !state || !district || !block) {
                        return;
                    }

                    var ddlMatchingType = $("[id$=ddlMatchingType]").val();

                    if (ddlMatchingType === '1') {
                        panel1.hide();
                        panel2.hide();
                        $("#btnMaplayer").show();
                    }
                    else if (ddlMatchingType === '2') {
                        panel1.show();
                        panel2.hide();
                        $("#btnMaplayer").hide();
                    }
                    else if (ddlMatchingType === '3') {
                        panel1.hide();
                        panel2.show();
                        $("#btnMaplayer").hide();
                    }

                    // ✅ CRITICAL FIX: map invalidate  
                    setTimeout(function () {
                        if (map) {
                            map.invalidateSize();
                            console.log("Map invalidated after panel toggle");
                            if (map2) {
                                map2.invalidateSize();
                                console.log("Map invalidated on document ready - 2");
                            }
                            if (window.defaultMapCenter && window.defaultMapZoom) {
                                map.setView(window.defaultMapCenter, window.defaultMapZoom);
                            }
                        }
                    }, 350);
                }

                function checkDigitilazation(district, block, cluster) {
                    var fyear = $("[id$=ddlYear] option:selected").text();
                    return $.ajax({
                        url: "GISEGBlock.aspx/isDigitalize",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({
                            districtcode: district,
                            blockcode: block,
                            clustercode: cluster,
                            fyear: fyear
                        })
                    });
                }

                //function checkDigitilazation() {

                //    var fyear = $("[id$=ddlYear] option:selected").text();
                //    var district = $("[id$=ddlDistrict]").val()?.split("#")[0];
                //    var block = $("[id$=ddlBlock]").val()?.split("#")[0];

                //    $.ajax({
                //        url: "GISEGBlock.aspx/isDigitalize",
                //        type: "POST",
                //        cache: false,
                //        contentType: "application/json; charset=utf-8",
                //        dataType: "json",
                //        data: JSON.stringify({
                //            districtcode: district,
                //            blockcode: block,
                //            clustercode: '',
                //            fyear: fyear
                //        }),
                //        success: function (response) {

                //            var isDigital = response && response.d != null
                //                ? String(response.d)
                //                : "0";
                //            console.log("isDigital", isDigital);

                //            if (isDigital === "1") {
                //                $("#btnRunMapping").hide();
                //                $("#btnMapVillages").hide();
                //            } else {
                //                $("#btnRunMapping").show();
                //                $("#btnMapVillages").show();
                //            }
                //        },
                //        error: function () {
                //            $("#btnRunMapping").show();
                //            $("#btnMapVillages").show();
                //        }
                //    });
                //}



                // ✅ Dropdown change events with map invalidation
                //$("[id$=ddlState]").change(function () {
                //    initMap();
                //    setTimeout(function () {
                //        if (map) {
                //            map.invalidateSize();
                //            console.log("Map invalidated after state change");
                //        }
                //    }, 300);
                //});

                //$("[id$=ddlDistrict]").change(function () {
                //    bindDistrictVillages();
                //    setTimeout(function () {
                //        if (map) {
                //            map.invalidateSize();
                //            console.log("Map invalidated after district change");
                //        }
                //    }, 300);
                //});

                //$("[id$=ddlBlock]").change(function () {
                //    bindBlockVillage();
                //    setTimeout(function () {
                //        if (map) {
                //            map.invalidateSize();
                //            console.log("Map invalidated after block change");
                //        }
                //    }, 300);
                //});

                //$("[id$=ddlMatchingType]").change(function () {
                //    show_hide_div();
                //});


                function runFuzzy() {
                    var fyear = $("[id$=ddlYear]").val();
                    var state = $("[id$=ddlState]").val();
                    var district = $("[id$=ddlDistrict]").val()?.split("#")[0];
                    var block = $("[id$=ddlBlock]").val()?.split("#")[0];

                    if (!fyear) { alert("please select year"); return; }
                    if (!state) { alert("please select state"); return; }
                    if (!district) { alert("please select district"); return; }
                    if (!block) { alert("please select block"); return; }

                    $(".update_overlay").show();

                    $.ajax({
                        type: "POST",
                        url: "GISVillageMapping.aspx/RunFuzzyLogic",
                        data: JSON.stringify({ fyear: fyear, district: district, block: block }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",

                        success: function (res) {
                            console.log(res);

                            if (res.d === "READY") {
                                window.location.href = "GISVillageMapping.aspx?download=1";
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



                function ExportMapped() {
                    var fyear = $("[id$=ddlYear]").val();
                    var state = $("[id$=ddlState]").val();
                    var district = $("[id$=ddlDistrict]").val()?.split("#")[0];
                    var block = $("[id$=ddlBlock]").val()?.split("#")[0];

                    if (!fyear) { alert("please select year"); return; }
                    if (!state) { alert("please select state"); return; }
                    if (!district) { alert("please select district"); return; }
                    if (!block) { alert("please select block"); return; }

                    $(".update_overlay").show();

                    $.ajax({
                        type: "POST",
                        url: "GISVillageMapping.aspx/ExportMappedData",
                        data: JSON.stringify({ fyear: fyear, district: district, block: block }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",

                        success: function (res) {
                            console.log(res);

                            if (res.d === "READY") {
                                window.location.href = "GISVillageMapping.aspx?download=2";
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


                $('#btnRunMapping').click(function () {

                    var text = $('#txt_MachingCode').val();
                    if (!text || text.trim() === '') {
                        alert('Please enter values');
                        return;
                    }

                    var fyear = $("[id$=ddlYear]").val();
                    var state = $("[id$=ddlState]").val();
                    var district = $("[id$=ddlDistrict]").val()?.split("#")[0];
                    var block = $("[id$=ddlBlock]").val()?.split("#")[0];

                    $(".update_overlay").show();
                    $.ajax({
                        type: "POST",
                        url: "GISVillageMapping.aspx/SaveMappings",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ csvValues: text, fyear: fyear }),
                        success: function (response) {
                            alert(response.d);
                        },
                        error: function (err) {
                            console.error(err);
                            alert('Error saving data');
                        },

                        complete: function () {
                            bindBlockVillage('', '');
                            loadVillages();
                            $(".update_overlay").hide();
                        }
                    });

                });


            </script>

            <script type="text/javascript">

                function searchTable() {
                    var input = document.getElementById("txtSearch");
                    var filter = input.value.toUpperCase();
                    var table = document.getElementById("tblLocDetails"); // change to your table ID
                    var tr = table.getElementsByTagName("tr");

                    for (var i = 1; i < tr.length; i++) {
                        var tds = tr[i].getElementsByTagName("td");
                        var found = false;

                        for (var j = 0; j < tds.length; j++) {
                            if (tds[j]) {
                                var txtValue = tds[j].textContent || tds[j].innerText;
                                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                                    found = true;
                                    break;
                                }
                            }
                        }

                        tr[i].style.display = found ? "" : "none";
                    }
                }
                function searchTable1() {
                    var input = document.getElementById("txtSearch1");
                    var filter = input.value.toUpperCase();
                    var table = document.getElementById("suggestTable"); // change to your table ID
                    var tr = table.getElementsByTagName("tr");

                    for (var i = 1; i < tr.length; i++) {
                        var tds = tr[i].getElementsByTagName("td");
                        var found = false;

                        for (var j = 0; j < tds.length; j++) {
                            if (tds[j]) {
                                var txtValue = tds[j].textContent || tds[j].innerText;
                                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                                    found = true;
                                    break;
                                }
                            }
                        }

                        tr[i].style.display = found ? "" : "none";
                    }
                }
                function searchTable2() {
                    var input = document.getElementById("txtSearch2");
                    var filter = input.value.toUpperCase();
                    var table = document.getElementById("tblLocDetails1_wrapper"); // change to your table ID
                    var tr = table.getElementsByTagName("tr");

                    for (var i = 1; i < tr.length; i++) {
                        var tds = tr[i].getElementsByTagName("td");
                        var found = false;

                        for (var j = 0; j < tds.length; j++) {
                            if (tds[j]) {
                                var txtValue = tds[j].textContent || tds[j].innerText;
                                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                                    found = true;
                                    break;
                                }
                            }
                        }

                        tr[i].style.display = found ? "" : "none";
                    }
                }


                // ✅ FIXED document ready
                $(document).ready(function () {
                    $(".update_overlay").show();
                    $("#btnMaplayer").hide();
                    bindMaster();

                    setTimeout(function () {
                        // first map
                        initMap('map', 'map');

                        // second map
                        initMap('map2', 'map2');

                        // Multiple invalidate calls
                        setTimeout(function () {
                            if (map) {
                                map.invalidateSize();
                                console.log("Map invalidated on document ready - 1");
                            }
                            if (map2) {
                                map2.invalidateSize();
                                console.log("Map invalidated on document ready - 1");
                            }
                        }, 200);

                        setTimeout(function () {
                            if (map) {
                                map.invalidateSize();
                                console.log("Map invalidated on document ready - 2");
                            }
                            if (map2) {
                                map2.invalidateSize();
                                console.log("Map invalidated on document ready - 2");
                            }
                        }, 500);

                    }, 100);

                    show_hide_div();
                    $(".update_overlay").hide();
                });

                // ✅ Window load event
                $(window).on('load', function () {
                    setTimeout(function () {
                        if (map) {
                            map.invalidateSize();
                            console.log("Map invalidated on window load");
                        }
                        if (map2) {
                            map2.invalidateSize();
                            console.log("Map invalidated on document ready - 2");
                        }
                    }, 200);
                });

                // ✅ Window resize event
                var resizeTimer;
                $(window).on('resize', function () {
                    clearTimeout(resizeTimer);
                    resizeTimer = setTimeout(function () {
                        if (map) {
                            map.invalidateSize();
                            console.log("Map invalidated on resize");
                        }
                        if (map2) {
                            map2.invalidateSize();
                            console.log("Map invalidated on document ready - 2");
                        }
                    }, 250);
                });

                function showloader() {
                    $(".update_overlay").show();
                }
                function hideloader() {
                    setTimeout(function () {
                        $(".update_overlay").hide();
                    }, 4000);

                }

                function bindMaster() {
                    Fill_FYear("ddlYear");
                    $('[id$=ddlYear]').val("2026");
                    Fill_State("ddlState");
                    var FYear = $("[id$=ddlYear] option:selected").text();
                    var UserlevelRole = '<%= Session["user_level_Role"] %>';
                    if (FYear == '2026-2027' && UserlevelRole == '1') {
                        $('[id$=ddlState]').val("23");
                    }
                    Fill_District("ddlDistrict");

                    var distvalue = '<%= Session["DistrictCodeGIS2026"] %>';
                    if (distvalue == '') {
                        if (FYear == '2026-2027') {
                          
                            $('[id$=ddlDistrict]').val("715EA2AFF7CE4AF080AF7CD81#22.6094#74.5233");
                        }
                        else {
                            $('[id$=ddlDistrict]').val("A1026CAD0051485C86F330F24#26.2455#80.8294");

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
                }

                function bindMasterYear() {

                    Fill_State("ddlState");
                    var FYear = $("[id$=ddlYear] option:selected").text();
                    var UserlevelRole = '<%= Session["user_level_Role"] %>';
                    if (FYear == '2026-2027' && UserlevelRole == '1') {
                        $('[id$=ddlState]').val("23");
                    }
                    Fill_District("ddlDistrict");

                    var distvalue =  '<%= Session["DistrictCodeGIS2026"] %>';
                    if (distvalue == '') {

                        if (FYear == '2026-2027') {
                            $('[id$=ddlDistrict]').val("715EA2AFF7CE4AF080AF7CD81#22.6094#74.5233");
                        }
                        else {
                            $('[id$=ddlDistrict]').val("17A9C3FD23A049BAB30ED17E9#26.2455#80.8294");

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

                    call_function('', '');
                    Get_Details();
                }

                function Fill_FYear(ddlID) {
                    var objvr = {};
                    objvr.ValidID = "";
                    _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_FYear_NextFY2025", "", objvr, true);
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
                    _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_Block2026", "Select", objvr, true);
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
                    return (c >= 48 && c <= 57);
                }

                function bindClick() {
                    if (currentVillagePolygon) {
                        map.removeLayer(currentVillagePolygon);
                    }

                    var fyear = $("[id$=ddlYear]").val();
                    var district = $("[id$=ddlDistrict] option:selected").text();
                    var block = $("[id$=ddlBlock] option:selected").text();

                    $('#MapSummary').off('click', 'input, textarea, select, button')
                        .on('click', 'input, textarea, select, button', function (e) {
                            e.stopPropagation();
                        });

                    $('#MapSummary').off('click', '.mis-row').on('click', '.mis-row', function (e) {
                        if ($(e.target).is('input, textarea, select, button')) {
                            return;
                        }

                        $('#MapSummary .mis-row').removeClass('selected');
                        $(this).addClass('selected');

                        var misName = $(this).data('name');
                        var egVillageCode = $(this).data('id');
                        var lat = $(this).data('lat');
                        var lon = $(this).data('lon');
                        var vcode = $(this).data('vid');
                        var admindistrictname = $(this).data('admindistrictname');
                        var mainblockname = $(this).data('mainblockname');

                        sessionStorage.setItem('misName', misName);
                        sessionStorage.setItem('egVillageCode', egVillageCode);
                        sessionStorage.setItem('lat', lat);
                        sessionStorage.setItem('lon', lon);
                        sessionStorage.setItem('vcode', vcode);
                        sessionStorage.setItem('admindistrictname', admindistrictname);
                        sessionStorage.setItem('mainblockname', mainblockname);

                        $(".update_overlay").show();

                        ajaxPost('GISMapping.aspx/GetMappingVillages',
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

                        addVillagePolygon(vcode, misName);
                    });
                }

                function getAdminDistrict() {
                    var did = $("[id$=ddlDistrict]").val().split("#");
                    var district = did[0];

                    ajaxPost('GISMapping.aspx/getadmindistrict',
                        {
                            district: district
                        },
                        function (res) {
                            sessionStorage.setItem('adminDistrictName', res);
                        }
                    );
                }

                function getAdminBlock() {
                    var did = $("[id$=ddlDistrict]").val().split("#");
                    var district = did[0];

                    var bid = $("[id$=ddlBlock]").val().split("#");
                    var block = bid[0];

                    ajaxPost('GISMapping.aspx/getadminBlock',
                        {
                            district: district,
                            block: block
                        },
                        function (res) {
                            sessionStorage.setItem('adminBlockName', res);
                        }
                    );
                }

                function saveVillages() {

                    var results = [];

                    $("#MapSummary1 tbody tr").each(function () {

                        var code = $(this).find(".gis-code").val().trim();
                        if (code === "") return;

                        if (!/^\d+$/.test(code)) {
                            alert("EG Village Code must be numeric!");
                            $(this).find(".gis-code").focus();
                            results = [];
                            return false;
                        }

                        results.push({
                            egVillageCode: $(this).data("id"),
                            VillageName: $(this).data("name"),
                            VillageCode: code
                        });
                    });

                    if (results.length === 0) {
                        alert("Please enter at least one EG Village Code.");
                        return;
                    }

                    $.ajax({
                        url: "GISVillageMapping.aspx/SaveVillages",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ villages: results }),
                        success: function () {
                            alert("Mapping Updated!");
                            loadUnmappedVillages();
                            bindGISVillages();
                            bindBlockVillage2('', '');
                        }
                    });
                }


                function renderSuggest(list) {
                    var container = $('#MapSummary1').empty();

                    if (!list || list.length === 0) {
                        $(".update_overlay").hide();
                        container.append('<div class="small" style="margin-left: 30px;">No data available in table</div>');
                        return;
                    }

                    var html = `
<table id="suggestTable" class="display compact" style="width:100%;height: 357px;">
    <thead>
        <tr>
            <th>Select</th>
            <th>SN</th>
            <th>VillageID</th>
            <th>Village</th>
            <th>District</th>
            <th>Block</th>
            <th>Match Score</th>
            <th>Distance (KM)</th>
            <th>EG VillageCode</th>
        </tr>
    </thead>
    <tbody>
`;

                    list.forEach(function (s) {
                        var greenClass = (s.EG_VillageCode && s.Flag == "1") ? "match-green" : "";

                        html += `
    <tr class="suggest-row ${greenClass}"
        data-layer-id="${s.VillageID}"
        data-eg-code="${s.EG_VillageCode || ''}">
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
        <td>${Math.round(parseFloat(s.MatchScore))}%</td>
        <td>${s.DistanceKM}</td>
        <td>${s.EG_VillageCode}</td>
    </tr>`;
                    });

                    html += `</tbody></table>`;
                    container.append(html);
                    $(".update_overlay").hide();

                    $('#suggestTable').DataTable({
                        pageLength: 10,
                        ordering: true,
                        searching: false,
                        destroy: true,
                        lengthChange: false
                    });



                    $('#suggestTable tbody').on('click', 'tr', function (e) {
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
                            EGVillageCode: storedEgVillageCode
                        });
                    });

                    return selected;
                }

                $('#btnSaveAll').on('click', function () {
                    var rows = getSelectedSuggestions();

                    if (rows.length === 0) {
                        alert("No rows selected!");
                        return;
                    }

                    ajaxPost("GISMapping.aspx/SaveVillageMappings",
                        { list: rows },
                        function (res) {
                            alert(res);
                            loadAll();
                        });
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
                        tr.append('<td><span class="link edit" data-mapid="' + m.MapID + '">Edit</span> | <span class="link delete" data-mapid="' + m.MapID + '">Unlink</span></td>');
                        c.append(tr);
                    });
                }

                var MIS_CACHE = [], LAYER_CACHE = [];

                function loadVillages() {
                    loadAll();
                }

                function loadAll() {
                    var fyear = $("[id$=ddlYear]").val();
                    var state = $("[id$=ddlState]").val();
                    var district = $("[id$=ddlDistrict]").val()?.split("#")[0];
                    var block = $("[id$=ddlBlock]").val()?.split("#")[0];

                    var did = $("[id$=ddlDistrict]").val().split("#");
                    var district = did[0];

                    var bid = $("[id$=ddlBlock]").val().split("#");
                    var block = bid[0];
                    $(".update_overlay").show();

                    var filters = {
                        query: null,
                        year: $('#<%= ddlYear.ClientID %>').val(),
                        state: $('#<%= ddlState.ClientID %>').val(),
                        district: district,
                        block: block,
                        status: 1,
                    };

                    ajaxPost('GISVillageMapping.aspx/GetMISVillages', filters, function (res) {
                        MIS_CACHE = res || [];
                        renderMis(res);
                    });
                }

                function renderMis(list) {
                    console.log("list", list);

                    list.forEach(function (o) {
                        delete o.__type;
                    });

                    $.ajax({
                        type: "POST",
                        url: "GISVillageMapping.aspx/RenderMisTable",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ list: list }),

                        success: function (res) {

                            $("#MapSummary").html(res.d);
                            console.log(res.d);

                            $("#tblLocDetails").DataTable({
                                paging: false,
                                searching: false,
                                ordering: true,
                                pageLength: 100000,
                                destroy: true,
                                autoWidth: false
                            });
                        },

                        error: function (xhr) {
                            console.log("Server error:\n" + xhr.responseText);
                        }
                    });


                    $(".update_overlay").hide();

                    $("#MapSummary").on("click", "tr.mis-row", function (e) {

                        // Ignore delete button or icon click
                        if ($(e.target).closest(".delete-btn").length) return;

                        var lat = parseFloat($(this).data("lat"));
                        var lon = parseFloat($(this).data("lon"));

                        if (isNaN(lat) || isNaN(lon)) return;

                        console.log("Zoom to:", lat, lon);

                        if (window.currentMarker) {
                            map.removeLayer(window.currentMarker);
                        }

                        window.currentMarker = L.marker([lat, lon]).addTo(map);
                        map.setView([lat, lon], 12);
                    });


                    $("#MapSummary")
                        .off("click", ".delete-btn")
                        .on("click", ".delete-btn", function (e) {

                            e.stopPropagation();

                            var row = $(this).closest("tr");
                            var villageCode = row.data("villagecode");
                            var fyear = $("[id$=ddlYear]").val();

                            $(".update_overlay").show();

                            $.ajax({
                                type: "POST",
                                url: "GISVillageMapping.aspx/DeleteMapping",
                                contentType: "application/json; charset=utf-8",
                                data: JSON.stringify({ villageCode: villageCode, fyear: fyear }),

                                success: function (response) {

                                    if (response.d > 0) {

                                        alert("Mapping removed successfully.");

                                        row.remove(); // ✅ remove after success

                                        if (DistrictVillageLayer) {
                                            map.removeLayer(DistrictVillageLayer);
                                            DistrictVillageLayer = null;
                                        }

                                        if (BlockVillageLayer) {
                                            map.removeLayer(BlockVillageLayer);
                                            BlockVillageLayer = null;
                                        }

                                        if (window.currentMarker) {
                                            map.removeLayer(window.currentMarker);
                                        }

                                    } else {
                                        alert("Error while removing mapping.");
                                    }
                                },

                                error: function () {
                                    alert("Server error.");
                                },

                                complete: function () {
                                    loadVillages();
                                    bindBlockVillage('', '');
                                    $(".update_overlay").hide();
                                }
                            });
                        });


                }

                //function renderUnmappedVillages(list) {
                //    var c = $('#MapSummary1').empty();

                //    var table = $(`
                //             <table class="table table-hover table-bordered table-striped" id="tblLocDetails1">
                //                 <thead>
                //                     <tr>

                //                     <th>Block</th>
                //                     <th>Admin District</th>
                //                         <th>Admin Block</th>
                //                         <th>Village</th>
                //                         <th>EG Village Code</th>
                //                         <th>Layer Village Code</th>
                //                     </tr>
                //                 </thead>
                //                 <tbody></tbody>
                //             </table>
                //         `);

                //    var tbody = table.find("tbody");

                //    (list || []).forEach(function (v) {
                //        tbody.append(`
                //                 <tr class="mis-row"
                //                     data-id="${v.VillageCode}"
                //                     data-name="${v.VillageName}"
                //                     data-admindistrictname="${v.AdminDistrictName}"
                //                     data-mainblockname="${v.MainBlockName}">

                //                    <td>${v.BlockName}</td>
                //                    <td>${v.AdminDistrictName}</td>
                //                    <td>${v.AdminBlockName}</td>
                //                     <td>${v.VillageName}</td>
                //                     <td>${v.VillageCode}</td>
                //                     <td>
                //                         <input type="text"
                //                                class="form-control gis-code"
                //                                maxlength="10"
                //                                onkeypress="return onlyNumbers(event)" />
                //                     </td>
                //                 </tr>
                //             `);
                //    });

                //    c.append(table);

                //    $("#tblLocDetails1").DataTable({
                //        paging: false,
                //        searching: false,
                //        ordering: true,
                //        pageLength: 100000,
                //        destroy: true,
                //        autoWidth: false
                //    });
                //    $(".update_overlay").hide();
                //}

                function loadUnmappedVillages() {

                    var district = $("[id$=ddlDistrict]").val().split("#")[0];
                    var block = $("[id$=ddlBlock]").val().split("#")[0];

                    $(".update_overlay").show();

                    $.ajax({
                        type: "POST",
                        url: "GISVillageMapping.aspx/GetUnmappedVillages",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({
                            query: null,
                            year: $("[id$=ddlYear]").val(),
                            state: $("[id$=ddlState]").val(),
                            district: district,
                            block: block,
                            status: 2
                        }),
                        success: function (res) {

                            $("#MapSummary1").html(res.d);

                            $("#tblLocDetails1").DataTable({
                                paging: false,
                                searching: false,
                                ordering: true,
                                autoWidth: false,
                                destroy: true
                            });
                        },
                        complete: function () {
                            $(".update_overlay").hide();
                        },
                        error: function () {
                            alert("Error loading unmapped villages");
                        }
                    });
                }


                function bindGISVillages() {

                    var fyear = $("[id$=ddlYear]").val();
                    var district = $("[id$=ddlDistrict]").val().split("#")[0];
                    var block = $("[id$=ddlBlock]").val().split("#")[0];

                    $(".update_overlay").show();

                    $.ajax({
                        type: "POST",
                        url: "GISVillageMapping.aspx/GetMappingVillages",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({
                            fyear: fyear,
                            district: district,
                            block: block
                        }),
                        success: function (res) {

                            $("#MapSummary2").html(res.d);

                            $("#suggestTable").DataTable({
                                paging: false,
                                searching: false,
                                ordering: true,
                                autoWidth: false,
                                destroy: true
                            });
                        },
                        complete: function () {
                            $(".update_overlay").hide();
                        },
                        error: function () {
                            alert("Error loading GIS villages");
                        }
                    });
                }

                $("#MapSummary2")
                    .off("click", ".suggest-row")
                    .on("click", ".suggest-row", function () {

                        var lat = parseFloat($(this).data("lat"));
                        var lon = parseFloat($(this).data("lon"));

                        if (isNaN(lat) || isNaN(lon)) return;

                        if (window.currentMarker) {
                            map2.removeLayer(window.currentMarker);
                        }

                        window.currentMarker = L.marker([lat, lon]).addTo(map2);
                        map2.setView([lat, lon], 12);
                    });


                function Map_savedLayer() {
                    var storedlayerid = sessionStorage.getItem('layerid');
                    var layerType = $("#layerTypeFilter").val() || "";
                    var layerid = storedlayerid;

                    if (!storedlayerid || storedlayerid === "") {
                        alert("Please select a Layer");
                        return;
                    }

                    $.ajax({
                        type: "POST",
                        url: "<%= ResolveUrl("GISMapping.aspx/Map_SavedLayer") %>",
                        data: JSON.stringify({ LayerType: layerType, layerid: layerid }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            alert("Mapping Successful");
                        },
                        error: function (xhr, status, error) {
                            console.log("Error occurred: " + error);
                        }
                    });
                }

            </script>

            <script type="text/javascript">
                var currentVillagePolygon = null;

                function addVillagePolygon(villageCode, villageName) {
                    $.ajax({
                        type: "POST",
                        url: "GISMapping.aspx/GetVillagePolygon",
                        data: JSON.stringify({ villageCode: villageCode }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var points = response.d.map(p => [p.Lat, p.Lon]);
                            if (!response.d || response.d.length < 3) {
                                console.warn("Invalid polygon data", response.d);
                                alert("Invalid polygon data");
                                return;
                            }

                            if (currentVillagePolygon) {
                                map.removeLayer(currentVillagePolygon);
                            }

                            currentVillagePolygon = L.polygon(points, {
                                color: 'blue',
                                fillColor: '#03b5fc',
                                fillOpacity: 0.4
                            }).addTo(map);

                            currentVillagePolygon.bindPopup("<b>Village: " + villageName + "</b>");
                            currentVillagePolygon.bindTooltip("<b>Village: " + villageName + "</b>", {
                                permanent: false,
                                sticky: true,
                                offset: [10, 0],
                                opacity: 0.9
                            });

                            map.fitBounds(currentVillagePolygon.getBounds());
                            map.setZoom(12);
                        },
                        error: function (err) {
                            console.error(err);
                        }
                    });
                }
            </script>


            <script type="text/javascript">
                var map;
                var map2 = null;
                var StateMap = L.layerGroup();
                var District_Map = L.layerGroup();
                var BlockMap = L.layerGroup();
                var VillageMap = L.layerGroup();
                var DistrictVillageLayer = null;
                var BlockVillageLayer = null;
                var BlockVillageLayer2 = null;
                var MappingSuggestionLayer = null;
                var MappedVillageLayer = null;
                var currentMarker = null;
                var StreetLyr, GrayLyr, Terrain, Imagery;
                var StreetLyr2, GrayLyr2, Terrain2, Imagery2;
                var layerControl1 = null;
                var layerControl2 = null;
                // ✅ FIXED initMap function
                //function initMap() {
                //    if (map) {
                //        map.remove();
                //        map = null;
                //    }

                //    var state = $("[id$=ddlState]").val();

                //    if (state == "9" || state == "9A" || state == "9B" || state == "9C") {
                //        map = L.map('map', {
                //            maxZoom: 18,
                //            minZoom: 4,
                //            dragging: true,
                //            fullscreenControl: { pseudoFullscreen: false }
                //        }).setView([25.3903, 80.8913], 4.5);
                //    } else if (state == "23") {
                //        map = L.map('map', {
                //            maxZoom: 18,
                //            minZoom: 4,
                //            dragging: true,
                //            fullscreenControl: { pseudoFullscreen: false }
                //        }).setView([23.065833940118736, 74.62120056152345], 4.5);
                //    } else {
                //        map = L.map('map', {
                //            maxZoom: 18,
                //            minZoom: 4,
                //            dragging: true,
                //            fullscreenControl: { pseudoFullscreen: false }
                //        }).setView([25.3903, 80.8913], 4.5);
                //    }

                //    StreetLyr = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                //        maxZoom: 19,
                //        attribution: '&copy; OpenStreetMap contributors'
                //    });

                //    map.setZoom(9);

                //    initializeBaseLayers();

                //    // ✅ CRITICAL: Map ready event
                //    map.whenReady(function () {
                //        console.log("Map is ready");
                //        setTimeout(function () {
                //            if (map) {
                //                map.invalidateSize();
                //                console.log("Map invalidated in whenReady");
                //            }
                //        }, 100);
                //    });

                //    // ✅ Store default position
                //    setTimeout(function () {
                //        if (map) {
                //            window.defaultMapCenter = map.getCenter();
                //            window.defaultMapZoom = map.getZoom();
                //            map.invalidateSize();
                //            console.log("Map invalidated after init");
                //        }
                //    }, 300);
                //}

                function initMap(mapDivId, mapRefName) {

                    var layers = {};

                    // 🔹 Remove existing map
                    if (window[mapRefName]) {
                        window[mapRefName].remove();
                        window[mapRefName] = null;
                    }

                    var state = $("[id$=ddlState]").val();
                    var center = [25.3903, 80.8913];

                    if (state == "23") {
                        center = [23.065833940118736, 74.62120056152345];
                    }

                    // 🔹 Create map
                    var mapInstance = L.map(mapDivId, {
                        maxZoom: 18,
                        minZoom: 4,
                        dragging: true,
                        fullscreenControl: { pseudoFullscreen: false }
                    }).setView(center, 4.5);

                    window[mapRefName] = mapInstance;

                    // 🔹 DEFAULT BASE LAYER (must be added)
                    layers.Street = L.tileLayer(
                        'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
                        {
                            maxZoom: 19,
                            attribution: '&copy; OpenStreetMap contributors'
                        }
                    ).addTo(mapInstance);

                    mapInstance.setZoom(9);

                    // 🔹 Other base layers
                    /* initializeBaseLayers(mapInstance, layers);*/
                    initializeBaseLayers(mapInstance, mapRefName === "map2");

                    // 🔹 store layers on map (for layer control later)
                    mapInstance._baseLayers = layers;

                    // ✅ map ready + invalidate
                    mapInstance.whenReady(function () {
                        setTimeout(function () {
                            mapInstance.invalidateSize();
                        }, 100);
                    });

                    // ✅ store default position
                    setTimeout(function () {
                        window.defaultMapCenter = mapInstance.getCenter();
                        window.defaultMapZoom = mapInstance.getZoom();
                        mapInstance.invalidateSize();
                    }, 300);
                }



                //function initializeBaseLayers() {
                //    var mbAttr = "";
                //    var BaseUrls = 'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1Ijoia3dyaXNhY2l3cm0iLCJhIjoiY2xma3p3NmpoMDBhaTNwbnV1NnVkMGp2ZCJ9.1ASKnwxbjSZxZGiXn0xl4Q';

                //    function BaseLyrOptionsM(ids) {
                //        return {
                //            maxZoom: 18,
                //            attribution: mbAttr,
                //            id: ids,
                //            tileSize: 512,
                //            zoomOffset: -1
                //        };
                //    }

                //    GrayLyr = L.tileLayer(BaseUrls, BaseLyrOptionsM('mapbox/light-v9'));
                //    Terrain = L.tileLayer(BaseUrls, BaseLyrOptionsM('mapbox/outdoors-v11')).addTo(map);
                //    //ImageryLyr = L.esri.basemapLayer('Imagery');
                //}

                //function initializeBaseLayers2() {
                //    var mbAttr = "";
                //    var BaseUrls = 'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1Ijoia3dyaXNhY2l3cm0iLCJhIjoiY2xma3p3NmpoMDBhaTNwbnV1NnVkMGp2ZCJ9.1ASKnwxbjSZxZGiXn0xl4Q';

                //    function BaseLyrOptionsM(ids) {
                //        return {
                //            maxZoom: 18,
                //            attribution: mbAttr,
                //            id: ids,
                //            tileSize: 512,
                //            zoomOffset: -1
                //        };
                //    }

                //    GrayLyr2 = L.tileLayer(BaseUrls, BaseLyrOptionsM('mapbox/light-v9'));
                //    Terrain2 = L.tileLayer(BaseUrls, BaseLyrOptionsM('mapbox/outdoors-v11')).addTo(map2);
                //    //ImageryLyr2 = L.esri.basemapLayer('Imagery');
                //}

                //function initializeBaseLayers(mapInstance, layerStore) {

                //    var BaseUrls =
                //        'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1Ijoia3dyaXNhY2l3cm0iLCJhIjoiY2xma3p3NmpoMDBhaTNwbnV1NnVkMGp2ZCJ9.1ASKnwxbjSZxZGiXn0xl4Q';

                //    function opts(id) {
                //        return {
                //            maxZoom: 18,
                //            id: id,
                //            tileSize: 512,
                //            zoomOffset: -1
                //        };
                //    }

                //    // NOT added by default
                //    layerStore.Gray = L.tileLayer(BaseUrls, opts('mapbox/light-v10'));

                //    // DEFAULT Mapbox layer (already visible)
                //    layerStore.Terrain = L.tileLayer(
                //        BaseUrls,
                //        opts('mapbox/outdoors-v12')
                //    );

                //    // Satellite
                //    layerStore.Imagery = L.esri.basemapLayer('Imagery');
                //}

                function initializeBaseLayers(mapInstance, isSecondMap) {
                    StreetLyr = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                        maxZoom: 19,
                        attribution: '&copy; OpenStreetMap contributors'
                    });
                    ImageryLyr = L.esri.basemapLayer('Imagery');
                    var BaseUrls =
                        'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1Ijoia3dyaXNhY2l3cm0iLCJhIjoiY2xma3p3NmpoMDBhaTNwbnV1NnVkMGp2ZCJ9.1ASKnwxbjSZxZGiXn0xl4Q';

                    function opts(id) {
                        return { maxZoom: 18, id, tileSize: 512, zoomOffset: -1 };
                    }

                    if (!isSecondMap) {
                        GrayLyr = L.tileLayer(BaseUrls, opts('mapbox/light-v9'));
                        Terrain = L.tileLayer(BaseUrls, opts('mapbox/outdoors-v11')).addTo(mapInstance);
                        Street = StreetLyr;
                        Imagery = ImageryLyr;
                    } else {
                        GrayLyr2 = L.tileLayer(BaseUrls, opts('mapbox/light-v9'));
                        Terrain2 = L.tileLayer(BaseUrls, opts('mapbox/outdoors-v11')).addTo(mapInstance);
                        Street2 = StreetLyr;
                        Imagery2 = ImageryLyr;
                    }
                }



                function bindDistrictVillages(flag, locationid) {
                    var MatchingType = $("[id$=ddlMatchingType]").val();
                    if (MatchingType == 1) {
                        return;
                    }
                    $(".update_overlay").show();
                    var districtName = $("[id$=ddlDistrict] option:selected").text();

                    var did = $("[id$=ddlDistrict]").val().split("#");
                    var district = did[0];

                    if (DistrictVillageLayer) {
                        map.removeLayer(DistrictVillageLayer);
                        DistrictVillageLayer = null;
                    }

                    $.ajax({
                        type: "POST",
                        url: "GISVillageMapping.aspx/GetDistrictVillages",
                        data: JSON.stringify({ district: district, districtname: districtName }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            var data = JSON.parse(res.d);

                            DistrictVillageLayer = L.geoJson(data, {
                                style: vlgstyle,
                                onEachFeature: onEachFeatureVillage
                            }).addTo(map);

                            addLayerControl();
                            addLayerControl2();

                            setTimeout(function () {
                                if (map) map.invalidateSize();
                                if (map2) {
                                    map2.invalidateSize();

                                }
                            }, 200);
                        },
                        error: function (xhr) {
                            console.error("Village load failed", xhr.responseText);
                        },
                        complete: function () {
                            $(".update_overlay").hide();
                        }
                    });

                    function vlgstyle(feature) {
                        return {
                            weight: 2,
                            opacity: 1,
                            color: 'black',
                            dashArray: '3',
                            fillOpacity: 0.7,
                            fillColor: CircleColors(feature.properties.mapped)
                        };
                    }

                    function onEachFeatureVillage(feature, layer) {
                        const mappingStatus =
                            feature.properties.mapped === 1
                                ? "<span style='color:green;font-weight:bold;'>Mapped</span>"
                                : "<span style='color:red;font-weight:bold;'>Unmapped</span>";

                        layer.bindTooltip(
                            "<b style='color:#2954A2;font-size:12px;'>" +
                            "Mapping Status: " + mappingStatus + "<br/>" +
                            "EG Village Code: " + (feature.properties.EGVillageCode ?? "N/A") + "<br/>" +
                            "EG Village Name: " + (feature.properties.VillageName ?? "N/A") + "<br/>" +
                            "Admin District: " + (feature.properties.AdminDistrictName ?? "N/A") + "<br/>" +
                            "Admin Block: " + (feature.properties.MainBlockName ?? "N/A") + "<br/>" +
                            "<hr style='margin:4px 0;'/>" +
                            "Village ID: " + (feature.properties.VillageID ?? "N/A") + "<br/>" +
                            "Layer Village Name: " + (feature.properties.lyr_VillageName ?? "N/A") + "<br/>" +
                            "Layer District: " + (feature.properties.DistrictName ?? "N/A") + "<br/>" +
                            "Layer Block: " + (feature.properties.BlockName ?? "N/A") +
                            "</b>",
                            {
                                permanent: false,
                                sticky: true,
                                offset: [10, 0],
                                opacity: 0.9
                            }
                        );

                        layer.on({
                            mouseover: highlightFeatureCluster,
                            mouseout: resetHighlightCluster,
                            preclick: resetStyleCluster,
                            click: zoomToFeatureCluster
                        });
                    }

                    function CircleColors(e) {
                        return (e === 1 ? '#D9E9CF' : e === 0 ? '#D3D3D3' : '#D3D3D3');
                    }

                    function highlightFeatureCluster(e) {
                        var layer = e.target;
                        layer.setStyle({
                            weight: 2,
                            color: '#666',
                            dashArray: '',
                            opacity: 1,
                            fillOpacity: 0.4
                        });
                    }

                    function resetHighlightCluster(e) {
                        if (DistrictVillageLayer) {
                            DistrictVillageLayer.resetStyle(e.target);
                        }
                    }

                    function resetStyleCluster(e) {
                        if (DistrictVillageLayer) {
                            DistrictVillageLayer.resetStyle(e.target);
                        }
                    }

                    function zoomToFeatureCluster(e) {
                        map.fitBounds(e.target.getBounds());
                    }
                }

                // ✅ FIXED bindBlockVillage function
                function bindBlockVillage(flag, locationid) {
                    var fyear = $("[id$=ddlYear]").val();
                    var state = $("[id$=ddlState]").val();
                    var district = $("[id$=ddlDistrict]").val()?.split("#")[0];
                    var block = $("[id$=ddlBlock]").val()?.split("#")[0];

                    if (window.currentMarker) {
                        map.removeLayer(window.currentMarker);
                    }

                    $(".update_overlay").show();

                    var districtName = sessionStorage.getItem('adminDistrictName');
                    var bid = $("[id$=ddlBlock]").val().split("#");
                    var blockCode = bid[0];

                    if (BlockVillageLayer) {
                        map.removeLayer(BlockVillageLayer);
                        BlockVillageLayer = null;
                    }

                    $.ajax({
                        type: "POST",
                        url: "GISVillageMapping.aspx/GetBlockVillages",
                        data: JSON.stringify({
                            district: district,
                            block: blockCode,
                            fyear: fyear
                        }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            var data = JSON.parse(res.d);
                            console.log("Block villages loaded:", data);

                            BlockVillageLayer = L.geoJson(data, {
                                style: vlgstyle,
                                onEachFeature: onEachFeatureVillage
                            }).addTo(map);

                            // ✅ Proper bounds fitting
                            if (BlockVillageLayer && BlockVillageLayer.getBounds().isValid()) {
                                map.fitBounds(BlockVillageLayer.getBounds());
                                console.log("Map fitted to block villages bounds");
                            }

                            addLayerControl();
                            addLayerControl2();

                            // ✅ Map invalidate
                            setTimeout(function () {
                                if (map) {
                                    map.invalidateSize();
                                    console.log("Map invalidated after block villages loaded");
                                }
                                if (map2) {
                                    map2.invalidateSize();
                                    console.log("Map invalidated on document ready - 2");
                                }
                            }, 200);
                        },
                        error: function (xhr) {
                            console.error("Block villages load failed", xhr.responseText);
                        },
                        complete: function () {
                            $(".update_overlay").hide();
                        }
                    });

                    function vlgstyle(feature) {
                        return {
                            weight: 2,
                            opacity: 1,
                            color: 'black',
                            dashArray: '3',
                            fillOpacity: 0.7,
                            fillColor: CircleColors(feature.properties.mapped)
                        };
                    }

                    function CircleColors(e) {
                        return (e === 1 ? '#8AFF8A' : e === 0 ? '#D3D3D3' : '#D3D3D3');
                    }

                    function onEachFeatureVillage(feature, layer) {
                        const mappingStatus = feature.properties.mapped === 1
                            ? "<span style='color:green;font-weight:bold;'>Mapped</span>"
                            : "<span style='color:red;font-weight:bold;'>Unmapped</span>";

                        layer.bindTooltip(
                            "<b style='color:#2954A2;font-size:12px;'>" +
                            "Mapping Status: " + mappingStatus + "<br/>" +
                            "EG Village Code: " + (feature.properties.EGVillageCode ?? "N/A") + "<br/>" +
                            "EG Village Name: " + (feature.properties.VillageName ?? "N/A") + "<br/>" +
                            "Admin District: " + (feature.properties.AdminDistrictName ?? "N/A") + "<br/>" +
                            "Admin Block: " + (feature.properties.MainBlockName ?? "N/A") + "<br/>" +
                            "<hr style='margin:4px 0;'/>" +
                            "Village ID: " + (feature.properties.VillageID ?? "N/A") + "<br/>" +
                            "Layer Village Name: " + (feature.properties.lyr_VillageName ?? "N/A") + "<br/>" +
                            "Layer District: " + (feature.properties.DistrictName ?? "N/A") + "<br/>" +
                            "Layer Block: " + (feature.properties.BlockName ?? "N/A") +
                            "</b>",
                            {
                                permanent: false,
                                sticky: true,
                                offset: [10, 0],
                                opacity: 0.9
                            }
                        );

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
                        });
                    }

                    function resetHighlightCluster(e) {
                        if (BlockVillageLayer) {
                            BlockVillageLayer.resetStyle(e.target);
                        }
                    }

                    function resetStyleCluster(e) {
                        if (BlockVillageLayer) {
                            BlockVillageLayer.resetStyle(e.target);
                        }
                    }

                    function zoomToFeatureCluster(e) {
                        map.fitBounds(e.target.getBounds());
                    }
                }

                function bindBlockVillage2(flag, locationid) {
                    var fyear = $("[id$=ddlYear]").val();
                    var state = $("[id$=ddlState]").val();
                    var district = $("[id$=ddlDistrict]").val()?.split("#")[0];
                    var block = $("[id$=ddlBlock]").val()?.split("#")[0];

                    if (window.currentMarker) {
                        map2.removeLayer(window.currentMarker);
                    }

                    $(".update_overlay").show();

                    var districtName = sessionStorage.getItem('adminDistrictName');
                    var bid = $("[id$=ddlBlock]").val().split("#");
                    var blockCode = bid[0];

                    if (BlockVillageLayer2) {
                        map2.removeLayer(BlockVillageLayer2);
                        BlockVillageLayer2 = null;
                    }

                    $.ajax({
                        type: "POST",
                        url: "GISVillageMapping.aspx/GetBlockVillages",
                        data: JSON.stringify({
                            district: district,
                            block: blockCode,
                            fyear: fyear
                        }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            var data = JSON.parse(res.d);
                            console.log("Block villages loaded:", data);

                            BlockVillageLayer2 = L.geoJson(data, {
                                style: vlgstyle,
                                onEachFeature: onEachFeatureVillage
                            }).addTo(map2);

                            // ✅ Proper bounds fitting
                            if (BlockVillageLayer2 && BlockVillageLayer2.getBounds().isValid()) {
                                map2.fitBounds(BlockVillageLayer2.getBounds());
                                console.log("Map fitted to block villages bounds");
                            }

                            addLayerControl();
                            addLayerControl2();

                            // ✅ Map invalidate
                            setTimeout(function () {

                                if (map2) {
                                    map2.invalidateSize();
                                    console.log("Map invalidated on document ready - 2");
                                }
                            }, 200);
                        },
                        error: function (xhr) {
                            console.error("Block villages load failed", xhr.responseText);
                        },
                        complete: function () {
                            $(".update_overlay").hide();
                        }
                    });

                    function vlgstyle(feature) {
                        return {
                            weight: 2,
                            opacity: 1,
                            color: 'black',
                            dashArray: '3',
                            fillOpacity: 0.7,
                            fillColor: CircleColors(feature.properties.mapped)
                        };
                    }

                    function CircleColors(e) {
                        return (e === 1 ? '#8AFF8A' : e === 0 ? '#D3D3D3' : '#D3D3D3');
                    }

                    function onEachFeatureVillage(feature, layer) {
                        const mappingStatus = feature.properties.mapped === 1
                            ? "<span style='color:green;font-weight:bold;'>Mapped</span>"
                            : "<span style='color:red;font-weight:bold;'>Unmapped</span>";

                        layer.bindTooltip(
                            "<b style='color:#2954A2;font-size:12px;'>" +
                            "Mapping Status: " + mappingStatus + "<br/>" +
                            "EG Village Code: " + (feature.properties.EGVillageCode ?? "N/A") + "<br/>" +
                            "EG Village Name: " + (feature.properties.VillageName ?? "N/A") + "<br/>" +
                            "Admin District: " + (feature.properties.AdminDistrictName ?? "N/A") + "<br/>" +
                            "Admin Block: " + (feature.properties.MainBlockName ?? "N/A") + "<br/>" +
                            "<hr style='margin:4px 0;'/>" +
                            "Village ID: " + (feature.properties.VillageID ?? "N/A") + "<br/>" +
                            "Layer Village Name: " + (feature.properties.lyr_VillageName ?? "N/A") + "<br/>" +
                            "Layer District: " + (feature.properties.DistrictName ?? "N/A") + "<br/>" +
                            "Layer Block: " + (feature.properties.BlockName ?? "N/A") +
                            "</b>",
                            {
                                permanent: false,
                                sticky: true,
                                offset: [10, 0],
                                opacity: 0.9
                            }
                        );

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
                        });
                    }

                    function resetHighlightCluster(e) {
                        if (BlockVillageLayer2) {
                            BlockVillageLayer2.resetStyle(e.target);
                        }
                    }

                    function resetStyleCluster(e) {
                        if (BlockVillageLayer2) {
                            BlockVillageLayer2.resetStyle(e.target);
                        }
                    }

                    function zoomToFeatureCluster(e) {
                        map2.fitBounds(e.target.getBounds());
                    }
                }

                function bindMappedVillage(gisvillageid) {
                    var fyear = $("[id$=ddlYear]").val();
                    var district = $("[id$=ddlDistrict] option:selected").text();
                    var block = $("[id$=ddlBlock] option:selected").text();

                    if (MappedVillageLayer) {
                        map2.removeLayer(MappedVillageLayer);
                    }

                    $.ajax({
                        type: "POST",
                        url: "GISVillageMapping.aspx/GetMappedVillage",
                        data: JSON.stringify({
                            villageid: gisvillageid
                        }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            var data = JSON.parse(res.d);

                            MappedVillageLayer = L.geoJson(data, {
                                style: vlgstyle,
                                onEachFeature: onEachFeaturevlg
                            }).addTo(map2);

                            updateLayerControl11();
                        },
                        error: function (xhr) {
                            console.error("Village suggestion load failed", xhr.responseText);
                        }
                    });

                    function vlgstyle(feature) {
                        return {
                            weight: 2,
                            opacity: 1,
                            color: 'black',
                            fillOpacity: 0.7,
                            fillColor: CircleColors(feature.properties.MatchScore)
                        };
                    }

                    function CircleColors(e) {
                        if (e === null || e === undefined) return '#FFFFFF';

                        return (
                            e >= 100 ? '#008000' :
                                e >= 80 ? '#0000FF' :
                                    e >= 70 ? '#FFFF00' :
                                        e >= 50 ? '#FFA500' :
                                            '#FF0000'
                        );
                    }

                    function onEachFeaturevlg(feature, layer) {
                        layer.bindTooltip("<b style='color: #2954A2;font-size: 12px;'>Village: " + feature.properties.GISVillageName + "<br/> Block: " + feature.properties.BlockName + "<br/> District: " + feature.properties.DistrictName + "</b>",
                            {
                                permanent: false,
                                sticky: true,
                                offset: [10, 0],
                                opacity: 2,
                            });

                        layer.on({
                            mouseover: highlightFeatureCluster,
                            mouseout: resetHighlightBlock,
                            preclick: resetStyleBlock,
                            click: zoomToFeatureCluster
                        });
                    }

                    function resetHighlightBlock(e) {
                        MappingSuggestionLayer.resetStyle(e.target);
                    }
                    function resetStyleBlock(e) {
                        MappingSuggestionLayer.resetStyle(e.target);
                    }
                    function highlightFeatureCluster(e) {
                        var layer = e.target;
                        layer.setStyle({
                            weight: 4,
                            color: '#666',
                            dashArray: '',
                            fillOpacity: 0.4
                        });
                    }
                    function zoomToFeatureCluster(e) {
                        map2.fitBounds(e.target.getBounds());
                    }
                }

                function bindMappingSuggestions() {
                    var fyear = $("[id$=ddlYear]").val();
                    var district = $("[id$=ddlDistrict] option:selected").text();
                    var block = $("[id$=ddlBlock] option:selected").text();

                    var storedEgVillageCode = sessionStorage.getItem('egVillageCode');
                    var storedmisName = sessionStorage.getItem('misName');

                    var admindistrictname = sessionStorage.getItem('admindistrictname');
                    var mainblockname = sessionStorage.getItem('mainblockname');

                    if (MappingSuggestionLayer) {
                        map2.removeLayer(MappingSuggestionLayer);
                    }

                    $.ajax({
                        type: "POST",
                        url: "GISMapping.aspx/GetVillageMappingSuggestions",
                        data: JSON.stringify({
                            villagename: storedmisName,
                            egvillagecode: storedEgVillageCode,
                            fyear: fyear,
                            districtname: admindistrictname,
                            blockname: mainblockname
                        }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            var data = JSON.parse(res.d);

                            MappingSuggestionLayer = L.geoJson(data, {
                                style: vlgstyle,
                                onEachFeature: onEachFeaturevlg
                            }).addTo(map2);

                            addMatchScoreLegend();
                            updateLayerControl();
                        },
                        error: function (xhr) {
                            console.error("Village suggestion load failed", xhr.responseText);
                        }
                    });

                    function vlgstyle(feature) {
                        return {
                            weight: 2,
                            opacity: 1,
                            color: 'black',
                            fillOpacity: 0.7,
                            fillColor: CircleColors(feature.properties.MatchScore)
                        };
                    }

                    function CircleColors(e) {
                        if (e === null || e === undefined) return '#FFFFFF';

                        return (
                            e >= 100 ? '#008000' :
                                e >= 80 ? '#0000FF' :
                                    e >= 70 ? '#FFFF00' :
                                        e >= 50 ? '#FFA500' :
                                            '#FF0000'
                        );
                    }

                    function onEachFeaturevlg(feature, layer) {
                        layer.bindTooltip("<b style='color: #2954A2;font-size: 12px;'>Village: " + feature.properties.GISVillageName + "<br/> Block: " + feature.properties.BlockName + "<br/> District: " + feature.properties.DistrictName + "</b>",
                            {
                                permanent: false,
                                sticky: true,
                                offset: [10, 0],
                                opacity: 2,
                            });

                        layer.on({
                            mouseover: highlightFeatureCluster,
                            mouseout: resetHighlightBlock,
                            preclick: resetStyleBlock,
                            click: zoomToFeatureCluster
                        });
                    }

                    function resetHighlightBlock(e) {
                        MappingSuggestionLayer.resetStyle(e.target);
                    }
                    function resetStyleBlock(e) {
                        MappingSuggestionLayer.resetStyle(e.target);
                    }
                    function highlightFeatureCluster(e) {
                        var layer = e.target;
                        layer.setStyle({
                            weight: 4,
                            color: '#666',
                            dashArray: '',
                            fillOpacity: 0.4
                        });
                    }
                    function zoomToFeatureCluster(e) {
                        map2.fitBounds(e.target.getBounds());
                    }
                }

                //function addLayerControl() {
                //    var overlays = {
                //        "Block Villages": BlockVillageLayer
                //    };

                //    if (!window.layerControl) {
                //        window.layerControl = L.control.layers(
                //            {
                //                "Gray": GrayLyr,
                //                //"Street": StreetLyr,
                //                "Terrain": Terrain
                //                //"Satellite": ImageryLyr
                //            },
                //            overlays
                //        ).addTo(map);
                //    } else {
                //        window.layerControl.remove();
                //        window.layerControl = L.control.layers(
                //            {
                //                "Gray": GrayLyr,
                //                //"Street": StreetLyr,
                //                "Terrain": Terrain
                //                //"Satellite": ImageryLyr
                //            },
                //            overlays
                //        ).addTo(map);
                //    }
                //}

                //function addLayerControl2() {
                //    var overlays = {
                //        "Block Villages": BlockVillageLayer2
                //    };

                //    if (!window.layerControl) {
                //        window.layerControl = L.control.layers(
                //            {
                //                "Gray": GrayLyr2,
                //                //"Street": StreetLyr2,
                //                "Terrain": Terrain2
                //                //"Satellite": ImageryLyr2
                //            },
                //            overlays
                //        ).addTo(map2);
                //    } else {
                //        window.layerControl.remove();
                //        window.layerControl = L.control.layers(
                //            {
                //                "Gray": GrayLyr2,
                //                //"Street": StreetLyr2,
                //                "Terrain": Terrain2
                //                //"Satellite": ImageryLyr2
                //            },
                //            overlays
                //        ).addTo(map2);
                //    }
                //}


                function addLayerControl() {

                    if (!map || !Terrain || !GrayLyr) return;

                    if (layerControl1) layerControl1.remove();

                    layerControl1 = L.control.layers(
                        { "Gray": GrayLyr, "Terrain": Terrain, "Street": Street, "Satellite": Imagery },
                        { "Block Villages": BlockVillageLayer },
                        {
                            collapsed: true,
                            position: "topright"
                        }
                    ).addTo(map);
                }

                function addLayerControl2() {

                    if (!map2 || !Terrain2 || !GrayLyr2) return;

                    if (layerControl2) layerControl2.remove();

                    layerControl2 = L.control.layers(
                        { "Gray": GrayLyr2, "Terrain": Terrain2, "Street": Street2, "Satellite": Imagery2 },
                        { "Block Villages": BlockVillageLayer2 },
                        {
                            collapsed: true,
                            position: "topright"
                        }
                    ).addTo(map2);
                }



                function updateLayerControl11() {
                    var overlays = {};

                    if (BlockVillageLayer) {
                        overlays["Block Villages"] = BlockVillageLayer;
                    }

                    if (window.layerControl) {
                        window.layerControl.remove();
                    }

                    window.layerControl = L.control.layers(
                        {
                            "Gray": GrayLyr,
                            //"Street": StreetLyr,
                            "Terrain": Terrain
                            //"Satellite": ImageryLyr
                        },
                        overlays,
                        { collapsed: false }
                    ).addTo(map2);
                }

                function updateLayerControl() {
                    var overlays = {};

                    if (BlockVillageLayer) {
                        overlays["Block Villages"] = BlockVillageLayer;
                    }

                    if (window.layerControl) {
                        window.layerControl.remove();
                    }

                    window.layerControl = L.control.layers(
                        {
                            "Gray": GrayLyr,
                            "Street": StreetLyr,
                            "Terrain": Terrain,
                            "Satellite": ImageryLyr
                        },
                        overlays,
                        { collapsed: false }
                    ).addTo(map2);
                }

                function addMatchScoreLegend() {
                    if (window.matchScoreLegend) {
                        map2.removeControl(window.matchScoreLegend);
                    }

                    window.matchScoreLegend = L.control({ position: 'bottomleft' });

                    window.matchScoreLegend.onAdd = function () {
                        var div = L.DomUtil.create('div', 'info legend');

                        div.innerHTML = `
        <b>Match Score</b><br>
        <i style="background:#008000"></i> 100+ (Exact Match)<br>
        <i style="background:#0000FF"></i> 80 – 90 (Very High)<br>
        <i style="background:#FFFF00"></i> 70 – 79 (High)<br>
        <i style="background:#FFA500"></i> 50 – 69 (Medium)<br>
        <i style="background:#FF0000"></i> &lt; 50 (Low)<br>
        <i style="background:#FFFFFF"></i> No Score
    `;

                        return div;
                    };

                    window.matchScoreLegend.addTo(map2);
                }

                function gotolatlong() {
                    var latitudelongitudeInput = document.getElementById('latitudelongitudeInput').value;
                    if (latitudelongitudeInput.trim() !== '') {
                        var latlong = latitudelongitudeInput.split(",");
                        var lat = parseFloat(latlong[0]);
                        var long = parseFloat(latlong[1]);
                        if (!isNaN(lat) && !isNaN(long)) {
                            console.log("Zoom to:", lat, long);

                            if (window.currentMarker) {
                                map.removeLayer(window.currentMarker);
                            }

                            window.currentMarker = L.marker([lat, long]).addTo(map);
                            map.setView([lat, long], 12);
                        }
                    }
                }

                function gotolatlong1() {
                    var latitudelongitudeInput1 = document.getElementById('latitudelongitudeInput1').value;
                    if (latitudelongitudeInput1.trim() !== '') {
                        var latlong = latitudelongitudeInput1.split(",");
                        var lat = parseFloat(latlong[0]);
                        var long = parseFloat(latlong[1]);
                        if (!isNaN(lat) && !isNaN(long)) {
                            console.log("Zoom to:", lat, long);

                            if (window.currentMarker) {
                                map2.removeLayer(window.currentMarker);
                            }

                            window.currentMarker = L.marker([lat, long]).addTo(map2);
                            map2.setView([lat, long], 12);
                        }
                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
