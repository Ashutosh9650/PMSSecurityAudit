<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Culture="en-GB" CodeFile="GISCluster.aspx.cs" Inherits="GISCluster" EnableEventValidation="false" %>

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

    <!-- Leaflet EasyButton -->
    <%--<script src="https://cdn.jsdelivr.net/npm/leaflet-easybutton@2/src/easy-button.js"></script>--%>
    <script type="text/javascript" src="js/JS/easy-button.js"></script>
    <%--<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/leaflet-easybutton@2/src/easy-button.css">--%>
    <link type="text/css" rel="stylesheet" href="css/CSS/easy-button.css">

    <script src="Leaflet2/bundle.js"></script>
    <script src="Leaflet2/leaflet.groupedlayercontrol.min.js"></script>
    <script src="Leaflet2/leaflet.spin.min.js" charset="utf-8"></script>
    <script src="Leaflet2/L.Control.Locate.js"></script>
    <script src="Leaflet2/leaflet-search.js"></script>
    <link href="Leaflet2/leaflet-search.css" rel="stylesheet" type="text/css" />

    <!-- DataTables CSS and JS -->
    <%--<link type="text/css" href="https://cdn.datatables.net/1.13.7/css/dataTables.bootstrap.min.css">--%>
    <link type="text/css" href="css/CSS/dataTables.bootstrap.min.css">
    <%--<link type="text/css" href="https://cdn.datatables.net/fixedheader/3.4.0/css/fixedHeader.bootstrap.min.css">--%>
    <link type="text/css" href="css/CSS/fixedHeader.bootstrap.min.css">
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

    <!-- Map Loader -->
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

        .bg-white_1 .dis-flex {
            margin-bottom: 5px;
        }

        .dis-flex h4 {
            font-size: 14px;
            margin: 0;
            font-weight: 700;
        }







        #map {
            min-height: 422px;
            /*min-height: calc(100vh - 212px);*/
            width: 100%;
            /*top: 20px !important;*/
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

        .mod-posi1 {
            position: absolute !important;
            top: 42% !important;
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

        .tit_div {
            float: left;
            width: 100%;
            border: 1px solid #c1c1c1;
            border-radius: 6px;
            padding: 15px 15px 0px 15px;
            box-shadow: 0px 4px 4px #ddd9d9;
            margin-bottom: 15px;
            background-color: rgb(241, 241, 241);
        }

        .grid-2new h3 {
            margin-top: 0px;
        }

        .criteria-title {
            float: left;
            width: 100%;
            height: auto;
            padding: 12px 12px 12px 35px;
            margin-bottom: 15px;
            border-radius: 6px;
        }

            .criteria-title .grid-2new {
                display: grid;
                width: 100%;
                grid-template-columns: auto 80px;
                gap: 15px;
                align-items: center;
            }

        .grid-2new div:nth-last-child(1) {
            text-align: right;
            border-left: 1px solid #767373;
            height: 100%;
            align-items: center;
            justify-content: center;
            display: flex;
        }

        .criteria-title .inp-div {
            width: 80%;
            /*margin:auto;*/
        }

        .criteria-title-bg-pink {
            background-color: #FFC6CC;
            border: 1px solid #eb9ea6;
        }

            .criteria-title-bg-pink input {
                color: #e74568;
            }

        .criteria-title-bg-warn {
            background-color: #FFF4DE;
            border: 1px solid #ff947a;
        }

            .criteria-title-bg-warn input {
                color: #f17f64;
            }

        .criteria-title-bg-succ {
            background-color: #DCFCE7;
            border: 1px solid #3cd856;
        }

            .criteria-title-bg-succ input {
                color: #29bb41;
            }

        .criteria-title-bg-pink h3 {
            color: #e74568
        }

        .criteria-title-bg-warn h3 {
            color: #ff947a
        }

        .criteria-title-bg-succ h3 {
            color: #3cd856
        }

        .inp-div input {
            border-radius: 0px;
            height: 40px;
            font-weight: bold;
            font-size: 24px;
            box-shadow: 0px 0px 4px #c1c1c1;
        }

        .link-button {
            background: none;
            border: none;
            color: blue;
            text-decoration: underline;
            cursor: pointer;
        }

        div#tblLocDetails_filter {
            text-align: end;
        }

        .search-bg {
            background-color: rgb(241, 241, 241) !important;
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
    </style>
    <style type="text/css">
        /*#legend {
                margin-bottom: 10px;
            }*/
        .legend-item {
            display: inline-block;
            width: 20px;
            height: 20px;
            margin-right: 10px;
        }

        .green {
            background-color: green;
        }

        .white {
            background-color: white;
        }

        .red {
            background-color: red;
        }


        /*====================================================*/


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
            height: 265px;
            width: 100%;
            overflow-y: scroll;
            overflow-x: hidden
        }

        #MapSummary table thead, tbody tr {
            display: table;
            width: 100%;
            table-layout: fixed;
        }

            #MapSummary table thead tr th {
                width: 100px !important;
                background: linear-gradient(to bottom, #ffe5e6 0%,#fff8f8 100%);
            }

        table#tblLocDetails {
            margin: 0px;
        }

        #MapSummary table tbody tr td {
            width: 100px !important
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

        .search-bg {
            background: linear-gradient(to bottom, #ebf1fd 0%,#ffffff 100%) !important;
            /* background-color: rgb(241, 241, 241)!important; */
            padding-top: 7px !important;
            border: 1px solid rgb(221, 221, 221) !important;
            border-top-left-radius: 4px !important;
            border-top-right-radius: 4px !important;
            margin-bottom: 0px !important;
        }

        #tblLocDetails_wrapper .row:nth-child(2) {
            margin: 0px !important;
        }

            #tblLocDetails_wrapper .row:nth-child(2) .col-sm-12 {
                padding: 0px !important
            }

        .mx-2 {
            margin: 0px 8px;
        }

        div#div_btnGenerate {
            margin-bottom: 5px;
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
            z-index: 100005;
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
                function ApproveCluster() {
                    try {

                        //Reference the Table.
                        //var grid = document.getElementById("tblLocDetails");

                        ////Reference the CheckBoxes in Table.
                        //var checkBoxes = grid.getElementsByTagName("INPUT");
                        //var cid = "";

                        ////Loop through the CheckBoxes.
                        //for (var i = 0; i < checkBoxes.length; i++) {
                        //    if (checkBoxes[i].checked) {
                        //        var row = checkBoxes[i].parentNode.parentNode;
                        //        cid += row.cells[1].innerHTML;
                        //        cid += ',';

                        //    }
                        //}
                        //cid = cid.slice(0, -1);
                        var username = '<%=Session["username"]%>';
                        var cid = "";
                        var FYear = $("[id$=ddlYear] option:selected").text();
                        var StateID = $("[id$=ddlState]").val();
                        var d = $("[id$=ddlDistrict]").val();
                        var did = "";
                        var DistrictID = "";
                        if (d && d.length > 10) {
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
                        <%--var numberOfVillages = $('#<%=txt_NoofVillages.ClientID%>').val();
                        var DistanceToCover = $("[id$=txt_Distance]").val();
                        var numberofOOSCs = $("[id$=txt_NoofOOSC]").val();--%>

                        if (StateID == "") {
                            Show_ModalAlert("Please select state !");
                            return;
                        }
                        if (DistrictID == "") {
                            Show_ModalAlert("Please select district !");
                            return;
                        }
                        //if (BlockID == "") {
                        //    Show_ModalAlert("Please select block !");
                        //    return;
                        //}
                        //if (cid == "") {
                        //    Show_ModalAlert("Please check cluster to be approved !");
                        //    return;
                        //}

                        var objvr = {};
                        objvr.ValidID = FYear;
                        objvr.ValidID1 = StateID;
                        objvr.ValidID2 = DistrictID;
                        objvr.ValidID3 = BlockID;
                        objvr.ValidID4 = cid;
                        objvr.ValidID5 = username;

                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            url: 'GISCluster.aspx/Approve_Cluster',
                            data: JSON.stringify(objvr),
                            success: function (response) {
                                Show_ModalAlert(response.d);
                                ShowHideButton('1#1');
                                Get_Details();
                            },
                            error: function () {
                                //alert("Error!!")
                                Show_ModalAlert("Please try again !!");
                                return false;
                            }
                        });
                    }


                    catch (e) {
                        //alert(e.message)
                        Show_ModalAlert("Please try again !!");
                    }
                    //setTimeout(logMe, 1000);
                    //Display selected Row data in Alert Box.
                    //alert(message);

                }

                function RejectCluster() {
                    try {

                        //Reference the Table.
                        //var grid = document.getElementById("tblLocDetails");

                        ////Reference the CheckBoxes in Table.
                        //var checkBoxes = grid.getElementsByTagName("INPUT");
                        //var cid = "";

                        ////Loop through the CheckBoxes.
                        //for (var i = 0; i < checkBoxes.length; i++) {
                        //    if (checkBoxes[i].checked) {
                        //        var row = checkBoxes[i].parentNode.parentNode;
                        //        cid += row.cells[1].innerHTML;
                        //        cid += ',';

                        //    }
                        //}
                        //cid = cid.slice(0, -1);
                        var username = '<%=Session["username"]%>';
                        var cid = "";

                        var FYear = $("[id$=ddlYear] option:selected").text();
                        var StateID = $("[id$=ddlState]").val();
                        var d = $("[id$=ddlDistrict]").val();
                        var did = "";
                        var DistrictID = "";
                        if (d && d.length > 10) {
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
                        var RejectReason = $("[id$=txtReason]").val();
                        <%--var numberOfVillages = $('#<%=txt_NoofVillages.ClientID%>').val();
                        var DistanceToCover = $("[id$=txt_Distance]").val();
                        var numberofOOSCs = $("[id$=txt_NoofOOSC]").val();--%>

                        if (StateID == "") {
                            Show_ModalAlert("Please select state !");
                            return;
                        }
                        if (DistrictID == "") {
                            Show_ModalAlert("Please select district !");
                            return;
                        }
                        //if (cid == "") {
                        //    Show_ModalAlert("Please check cluster to be rejected !");
                        //    return;
                        //}
                        //if (BlockID == "") {
                        //    Show_ModalAlert("Please select block !");
                        //    return;
                        //}

                        var objvr = {};
                        objvr.ValidID = FYear;
                        objvr.ValidID1 = StateID;
                        objvr.ValidID2 = DistrictID;
                        objvr.ValidID3 = BlockID;
                        objvr.ValidID4 = cid;
                        objvr.ValidID5 = RejectReason;
                        objvr.ValidID6 = username;

                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            url: 'GISCluster.aspx/Reject_Cluster',
                            data: JSON.stringify(objvr),
                            success: function (response) {
                                Hide_ModalReject();
                                Show_ModalAlert(response.d);
                                ShowHideDiv();
                                ShowHideButton('2#2');
                                Get_Details();
                            },
                            error: function () {
                                //alert("Error!!")
                                Show_ModalAlert("Please try again !!");
                                return false;
                            }
                        });
                    }


                    catch (e) {
                        //alert(e.message)
                        Show_ModalAlert("Please try again !!");
                    }
                    //setTimeout(logMe, 1000);
                    //Display selected Row data in Alert Box.
                    //alert(message);

                }

                function UnlockCluster() {
                    try {


                        var cid = "";
                        var username = '<%=Session["username"]%>';
                        var FYear = $("[id$=ddlYear] option:selected").text();
                        var StateID = $("[id$=ddlState]").val();
                        var d = $("[id$=ddlDistrict]").val();
                        var did = "";
                        var DistrictID = "";
                        if (d && d.length > 10) {
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

                        if (StateID == "") {
                            Show_ModalAlert("Please select state !");
                            return;
                        }
                        if (DistrictID == "") {
                            Show_ModalAlert("Please select district !");
                            return;
                        }

                        //if (BlockID == "") {
                        //    Show_ModalAlert("Please select block !");
                        //    return;
                        //}

                        var objvr = {};
                        objvr.ValidID = FYear;
                        objvr.ValidID1 = StateID;
                        objvr.ValidID2 = DistrictID;
                        objvr.ValidID3 = BlockID;
                        objvr.ValidID4 = username;

                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            url: 'GISCluster.aspx/Unlock_Cluster',
                            data: JSON.stringify(objvr),
                            success: function (response) {
                                Hide_ModalReject();
                                Show_ModalAlert(response.d);
                                ShowHideDiv();
                                ShowHideButton('0#0');
                                //ShowHideButtonStatus('0');
                                Get_Details();
                            },
                            error: function () {
                                //alert("Error!!")
                                Show_ModalAlert("Please try again !!");
                                return false;
                            }
                        });
                    }


                    catch (e) {
                        //alert(e.message)
                        Show_ModalAlert("Please try again !!");
                    }
                    //setTimeout(logMe, 1000);
                    //Display selected Row data in Alert Box.
                    //alert(message);

                }

                function DeleteCluster() {
                    try {
                        var username = '<%=Session["username"]%>';
                        //Reference the Table.
                        var grid = document.getElementById("tblLocDetails");

                        //Reference the CheckBoxes in Table.
                        var checkBoxes = grid.getElementsByTagName("INPUT");
                        var cid = "";

                        //Loop through the CheckBoxes.
                        for (var i = 0; i < checkBoxes.length; i++) {
                            if (checkBoxes[i].checked) {
                                var row = checkBoxes[i].parentNode.parentNode;
                                cid += row.cells[1].innerHTML;
                                cid += ',';

                            }
                        }
                        cid = cid.slice(0, -1);

                        var FYear = $("[id$=ddlYear] option:selected").text();
                        var StateID = $("[id$=ddlState]").val();
                        var d = $("[id$=ddlDistrict]").val();
                        var did = "";
                        var DistrictID = "";
                        if (d && d.length > 10) {
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
                        <%--var numberOfVillages = $('#<%=txt_NoofVillages.ClientID%>').val();
                        var DistanceToCover = $("[id$=txt_Distance]").val();
                        var numberofOOSCs = $("[id$=txt_NoofOOSC]").val();--%>

                        if (StateID == "") {
                            Show_ModalAlert("Please select state !!");
                            return;
                        }
                        if (DistrictID == "") {
                            Show_ModalAlert("Please select district !!");
                            return;
                        }
                        if (BlockID == "") {
                            Show_ModalAlert("Please select Block !!");
                            return;
                        }
                        if (cid == "") {
                            Show_ModalAlert("Please check cluster to be deleted !!");
                            return;
                        }

                        var objvr = {};
                        objvr.ValidID = FYear;
                        objvr.ValidID1 = StateID;
                        objvr.ValidID2 = DistrictID;
                        objvr.ValidID3 = BlockID;
                        objvr.ValidID4 = cid;
                        objvr.ValidID5 = username;

                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            url: 'GISCluster.aspx/Delete_Cluster',
                            data: JSON.stringify(objvr),
                            success: function (response) {
                                Show_ModalAlert(response.d);
                                Get_Details();
                            },
                            error: function () {
                                //alert("Error!!")
                                Show_ModalAlert("Please try again !!");
                                return false;
                            }
                        });
                    }


                    catch (e) {
                        //alert(e.message)
                        Show_ModalAlert("Please try again !!");
                    }
                    //setTimeout(logMe, 1000);
                    //Display selected Row data in Alert Box.
                    //alert(message);

                }

                function SubmitCluster() {
                    try {

                        var username = '<%=Session["username"]%>';
                        var FYear = $("[id$=ddlYear] option:selected").text();
                        var StateID = $("[id$=ddlState]").val();
                        var d = $("[id$=ddlDistrict]").val();
                        var did = "";
                        var DistrictID = "";
                        if (d && d.length > 10) {
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

                        if (StateID == "") {
                            Show_ModalAlert("Please select state !");
                            return;
                        }
                        if (DistrictID == "") {
                            Show_ModalAlert("Please select district !");
                            return;
                        }
                        //if (BlockID == "") {
                        //    Show_ModalAlert("Please select district !");
                        //    return;
                        //}

                        var objvr = {};
                        objvr.ValidID = FYear;
                        objvr.ValidID1 = StateID;
                        objvr.ValidID2 = DistrictID;
                        objvr.ValidID3 = BlockID;
                        objvr.ValidID4 = username;

                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            url: 'GISCluster.aspx/Submit_Cluster',
                            data: JSON.stringify(objvr),
                            success: function (response) {
                                Show_ModalAlert(response.d);
                                $('#lblrejectinfo').hide();
                                $("#btn_Submit_Cluster").hide();
                                $('#btn_Submit_Cluster').prop('disabled', true);
                                $('#btn_Submit_Cluster').html('Cluster Submitted for Approval');

                                $("#btn_Submit_Cluster").show();
                                $("#btn_Gen_Cluster").hide();
                                $("#btn_Unlock_Cluster").show();

                                Get_Details();
                            },
                            error: function () {
                                //alert("Error!!")
                                Show_ModalAlert("Please try again !!");
                                return false;
                            }
                        });
                    }


                    catch (e) {
                        //alert(e.message)
                        Show_ModalAlert("Please try again !!");
                    }
                    //setTimeout(logMe, 1000);
                    //Display selected Row data in Alert Box.
                    //alert(message);

                }

                <%--function SubmitClusterBO() {
                    try {

                        var username = '<%=Session["username"]%>';
                        var FYear = $("[id$=ddlYear] option:selected").text();
                        var StateID = $("[id$=ddlState]").val();
                        var d = $("[id$=ddlDistrict]").val();
                        var did = "";
                        var DistrictID = "";
                        if (d && d.length > 10) {
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

                        if (StateID == "") {
                            Show_ModalAlert("Please select state !");
                            return;
                        }
                        if (DistrictID == "") {
                            Show_ModalAlert("Please select district !");
                            return;
                        }
                        if (BlockID == "") {
                            Show_ModalAlert("Please select block !");
                            return;
                        }

                        var objvr = {};
                        objvr.ValidID = FYear;
                        objvr.ValidID1 = StateID;
                        objvr.ValidID2 = DistrictID;
                        objvr.ValidID3 = BlockID;
                        objvr.ValidID4 = username;

                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            url: 'GISCluster.aspx/Submit_Cluster_BO',
                            data: JSON.stringify(objvr),
                            success: function (response) {
                                Show_ModalAlert(response.d);
                                $("#btn_Submit_Cluster").hide();
                                $("#btn_Submit_Cluster_BO").hide();
                                //incomment for BO
                                //$('#btn_Submit_Cluster_BO').prop('disabled', true);
                                //$('#btn_Submit_Cluster_BO').html('Cluster Submitted');
                                //$("#btn_Submit_Cluster_BO").show();
                                $("#btn_Gen_Cluster").hide();
                                $("#btn_Unlock_Cluster").hide();

                                Get_Details();
                            },
                            error: function () {
                                //alert("Error!!")
                                Show_ModalAlert("Please try again !!");
                                return false;
                            }
                        });
                    }


                    catch (e) {
                        //alert(e.message)
                        Show_ModalAlert("Please try again !!");
                    }
                    //setTimeout(logMe, 1000);
                    //Display selected Row data in Alert Box.
                    //alert(message);

                }--%>

                <%--function ShowHideButton_BO(flag) {

                      var userlevel = '<%=Session["user_level"]%>';
                      $('#btn_Submit_Cluster').hide();
                      $('#btn_Submit_Cluster_BO').hide();
                      $("#btn_Gen_Cluster").hide();
                      $('#btn_approve_Cluster').hide();
                      $('#btn_reject_Cluster').hide();
                      $("#btn_Unlock_Cluster").hide();
                      if (flag == "1" || flag == '2') {
                          $('#btn_Submit_Cluster').hide();
                          $("#btn_Gen_Cluster").hide();
                          $('#btn_approve_Cluster').hide();
                          $('#btn_reject_Cluster').hide();
                          $("#btn_Unlock_Cluster").hide();
                          $('#btn_Submit_Cluster_BO').hide();
                          //uncomment for BO
                          //$('#btn_Submit_Cluster_BO').prop('disabled', true);
                          //$('#btn_Submit_Cluster_BO').html('Cluster Submitted');
                          //$('#btn_Submit_Cluster_BO').show();

                      }
                      else if (flag == "na") {
                          $('#btn_Submit_Cluster').hide();
                          $('#btn_Submit_Cluster_BO').hide();
                          $("#btn_Gen_Cluster").hide();
                          $('#btn_approve_Cluster').hide();
                          $('#btn_reject_Cluster').hide();
                          $("#btn_Unlock_Cluster").hide();

                          $("#btn_Gen_Cluster").show();
                          //incomment for BO
                          //$('#btn_Submit_Cluster_BO').show();

                      }
                      else {
                          $('#btn_Submit_Cluster').hide();
                          $('#btn_Submit_Cluster_BO').hide();
                          $("#btn_Gen_Cluster").hide();
                          $('#btn_approve_Cluster').hide();
                          $('#btn_reject_Cluster').hide();
                          $("#btn_Unlock_Cluster").hide();

                          $("#btn_Gen_Cluster").show();
                          $('#btn_Submit_Cluster_BO').hide();
                      }

                  }--%>


                function ShowHideButton(flag) {
                    debugger;
                    var userlevel = '<%=Session["user_level"]%>';
                    var submitted = "";
                    var approved = "";
                    if (flag && flag.length > 2) {
                        var status = flag.split("#");
                        submitted = status[0];
                        approved = status[0];
                    }
                    $('#btn_Submit_Cluster').hide();
                    $("#btn_Gen_Cluster").hide();
                    $('#btn_approve_Cluster').hide();
                    $('#btn_reject_Cluster').hide();
                    $("#btn_Unlock_Cluster").hide();
                    //////////submitted////////////////
                    if (submitted == "1") {
                        $('#btn_Submit_Cluster').hide();
                        $("#btn_Gen_Cluster").hide();
                        $('#btn_approve_Cluster').hide();
                        $('#btn_reject_Cluster').hide();
                        $("#btn_Unlock_Cluster").hide();
                        $('#lblrejectinfo').hide();
                        $('#btn_Submit_Cluster').prop('disabled', true);
                        $('#btn_Submit_Cluster').html('Cluster Submitted for Approval');
                        $('#btn_Submit_Cluster').show();
                        $('#btn_approve_Cluster').prop('disabled', false);
                        $('#btn_reject_Cluster').prop('disabled', false);
                        $("#btn_approve_Cluster").show();
                        $("#btn_reject_Cluster").show();
                        if (userlevel == "1") {
                            $("#btn_Unlock_Cluster").show();
                        }
                    }
                    else if (submitted == "2") {
                        $('#btn_Submit_Cluster').hide();
                        $("#btn_Gen_Cluster").hide();
                        $('#btn_approve_Cluster').hide();
                        $('#btn_reject_Cluster').hide();
                        $("#btn_Unlock_Cluster").hide();
                        $('#lblrejectinfo').hide();

                    }
                    else {
                        $('#btn_Submit_Cluster').show();
                        $("#btn_Gen_Cluster").hide();
                        $('#btn_approve_Cluster').hide();
                        $('#btn_reject_Cluster').hide();
                        $("#btn_Unlock_Cluster").hide();

                        //$('#btn_Submit_Cluster').prop('disabled', false);
                        //$('#btn_Submit_Cluster').html('Submit Cluster for DOL Approval');
                        $("#btn_Gen_Cluster").show();
                        //$('#btn_Submit_Cluster').show();
                    }
                    var b = $("[id$=ddlBlock]").val();
                    if (b != "") {
                        $('#btn_Submit_Cluster').hide();
                        $('#btn_approve_Cluster').hide();
                        $('#btn_reject_Cluster').hide();
                        $("#btn_Unlock_Cluster").hide();
                        $('#lblrejectinfo').hide();
                    }

                    //////////approved////////////////

                    if (approved == "1") {
                        $('#btn_Submit_Cluster').hide();
                        $("#btn_Gen_Cluster").hide();
                        $('#btn_approve_Cluster').hide();
                        $('#btn_reject_Cluster').hide();
                        $("#btn_Unlock_Cluster").hide();
                        $('#lblrejectinfo').hide();
                        $('#btn_Submit_Cluster').prop('disabled', true);
                        $('#btn_Submit_Cluster').html('Cluster Approved');
                        $('#btn_Submit_Cluster').show();
                        //$("#btn_Gen_Cluster").hide();
                        //$("#btn_approve_Cluster").hide();
                        $('#btn_approve_Cluster').prop('disabled', true);
                        $('#btn_approve_Cluster').html('Cluster Approved');
                        $('#btn_approve_Cluster').show();
                        if (userlevel == "1") {
                            $("#btn_Unlock_Cluster").show();
                        }

                        //$('#btn_reject_Cluster').hide();
                    }
                    else if (approved == "2") {
                        $('#btn_Submit_Cluster').hide();
                        $("#btn_Gen_Cluster").hide();
                        $('#btn_approve_Cluster').hide();
                        $('#btn_reject_Cluster').hide();
                        $("#btn_Unlock_Cluster").hide();

                        $('#btn_Submit_Cluster').prop('disabled', false);
                        $('#btn_Submit_Cluster').html('Submit Cluster for DOL Approval');
                        $('#lblrejectinfo').show();
                        $('#btn_Submit_Cluster').show();
                        $("#btn_Gen_Cluster").show();
                        $('#btn_reject_Cluster').prop('disabled', true);
                        $("#btn_reject_Cluster").html('Cluster Rejected');
                        $("#btn_reject_Cluster").show();
                    }
                    else {
                        $('#btn_Submit_Cluster').hide();
                        $("#btn_Gen_Cluster").hide();
                        $('#btn_approve_Cluster').hide();
                        $('#btn_reject_Cluster').hide();
                        $("#btn_Unlock_Cluster").hide();
                        $('#lblrejectinfo').hide();
                        $('#btn_Submit_Cluster').prop('disabled', false);
                        $('#btn_Submit_Cluster').html('Submit Cluster for DOL Approval');
                        $("#btn_Gen_Cluster").show();
                    }

                }


               <%-- function ShowHideButtonStatus(status) {

                    var userlevel = '<%=Session["user_level"]%>';
                  $('#btn_Submit_Cluster').hide();
                  $("#btn_Gen_Cluster").hide();
                  $('#btn_approve_Cluster').hide();
                  $('#btn_reject_Cluster').hide();
                  $("#btn_Unlock_Cluster").hide();

                  if (status == "1") {
                      $('#btn_Submit_Cluster').hide();
                      $("#btn_Gen_Cluster").hide();
                      $('#btn_approve_Cluster').hide();
                      $('#btn_reject_Cluster').hide();
                      $("#btn_Unlock_Cluster").hide();
                      $('#lblrejectinfo').hide();
                      $('#btn_Submit_Cluster').prop('disabled', true);
                      $('#btn_Submit_Cluster').html('Cluster Approved');
                      $('#btn_Submit_Cluster').show();
                      //$("#btn_Gen_Cluster").hide();
                      //$("#btn_approve_Cluster").hide();
                      $('#btn_approve_Cluster').prop('disabled', true);
                      $('#btn_approve_Cluster').html('Cluster Approved');
                      $('#btn_approve_Cluster').show();
                      if (userlevel == "1") {
                          $("#btn_Unlock_Cluster").show();
                      }

                      //$('#btn_reject_Cluster').hide();
                  }
                  else if (status == "2") {
                      $('#btn_Submit_Cluster').hide();
                      $("#btn_Gen_Cluster").hide();
                      $('#btn_approve_Cluster').hide();
                      $('#btn_reject_Cluster').hide();
                      $("#btn_Unlock_Cluster").hide();

                      $('#btn_Submit_Cluster').prop('disabled', false);
                      $('#btn_Submit_Cluster').html('Submit Cluster for DOL Approval');
                      $('#lblrejectinfo').show();
                      $('#btn_Submit_Cluster').show();
                      $("#btn_Gen_Cluster").show();
                      $('#btn_reject_Cluster').prop('disabled', true);
                      $("#btn_reject_Cluster").html('Cluster Rejected');
                      $("#btn_reject_Cluster").show();
                  }
                  else {
                      $('#btn_Submit_Cluster').hide();
                      $("#btn_Gen_Cluster").hide();
                      $('#btn_approve_Cluster').hide();
                      $('#btn_reject_Cluster').hide();
                      $("#btn_Unlock_Cluster").hide();
                      $('#lblrejectinfo').hide();
                      $('#btn_Submit_Cluster').prop('disabled', false);
                      $('#btn_Submit_Cluster').html('Submit Cluster for DOL Approval');
                      $("#btn_Gen_Cluster").show();
                  }

              }--%>





                function ShowHideDiv() {

                    var userlevel = '<%=Session["user_level"]%>';
                    var FYear = $("[id$=ddlYear] option:selected").text();
                    var StateID = $("[id$=ddlState]").val();
                    var d = $("[id$=ddlDistrict]").val();
                    var did = "";
                    var DistrictID = "";
                    if (d && d.length > 10) {
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

                    if (DistrictID == "") {
                        $('.generate').hide();
                        $('.approve').hide();
                        $('.submitcluster').hide();
                        $('.unlock').hide();
                        //$('#btn_Submit_Cluster').hide();
                        //$("#btn_Gen_Cluster").hide();
                        $("#legend").hide();
                    }
                    else {
                        //if (userlevel == "39" || userlevel == "19") {
                        if (userlevel == "39" || userlevel == 145) {
                            //$('.generate').show();
                            $('.submitcluster').show();
                            $('.approve').hide();
                            $('.unlock').hide();
                        }
                        else if (userlevel == "1") {
                            //$('.generate').show();
                            $('.submitcluster').show();
                            $('.approve').hide();
                            $('.unlock').show();
                        }
                        else if (userlevel == "91") {
                            $('.approve').show();
                            $('.submitcluster').hide();
                            //$('.generate').hide();
                            $('.unlock').hide();
                        }
                        else {
                            $('.approve').hide();
                            $('.generate').hide();
                            $('.unlock').hide();
                        }
                        $("#legend").hide();


                    }
                    if (BlockID == "" && DistrictID != "") {
                        $('.generate').hide();
                        //$('.approve').hide();
                        //$('.unlock').hide();
                        //$('.submitcluster').hide();
                        //$('#btn_Submit_Cluster').hide();
                        //$("#btn_Gen_Cluster").hide();
                        $("#legend").hide();
                    }

                    else {
                        //if (userlevel == "39" || userlevel == "19") {
                        if (userlevel == "39" || userlevel == 145) {
                            $('.generate').show();
                            $('.submitcluster').hide();
                            //$('.approve').hide();
                            $('.unlock').hide();
                        }
                        else if (userlevel == "1") {
                            $('.generate').show();
                            $('.submitcluster').hide();
                            //$('.approve').hide();
                            $('.unlock').hide();
                        }
                        else if (userlevel == "91") {
                            $('.generate').hide();
                            $('.submitcluster').hide();
                            $('.approve').hide();
                            $('.unlock').hide();
                        }
                        else {
                            $('.approve').hide();
                            $('.generate').hide();
                            $('.unlock').hide();
                        }
                        $("#legend").hide();
                    }
                }


                <%--function ShowHideDiv() {
                    
                    var userlevel = '<%=Session["user_level"]%>';
                    var FYear = $("[id$=ddlYear] option:selected").text();
                    var StateID = $("[id$=ddlState]").val();
                    var d = $("[id$=ddlDistrict]").val();
                    var did = "";
                    var DistrictID = "";
                    if (d && d.length > 10) {
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

                    if (BlockID == "") {
                        $('.generate').hide();
                        $('.approve').hide();
                        $('.unlock').hide();
                        //$('#btn_Submit_Cluster').hide();
                        //$("#btn_Gen_Cluster").hide();
                        $("#legend").hide();
                    }
                    else {
                        if (userlevel == "39" || userlevel ==145) {
                            $('.generate').show();
                            $('.approve').hide();
                            $('.unlock').hide();
                        }
                        else if (userlevel == "1") {
                            $('.generate').show();
                            $('.approve').hide();
                            $('.unlock').show();
                        }
                        else if (userlevel == "91") {
                            $('.approve').show();
                            $('.generate').hide();
                            $('.unlock').hide();
                        }
                        else {
                            $('.approve').hide();
                            $('.generate').hide();
                            $('.unlock').hide();
                        }
                        $("#legend").hide();
                    }
                }--%>

                function SubmitCluster_Info() {

                    try {

                        var userlevel = '<%=Session["user_level"]%>';
                        var FYear = $("[id$=ddlYear] option:selected").text();
                        var StateID = $("[id$=ddlState]").val();
                        var d = $("[id$=ddlDistrict]").val();
                        var did = "";
                        var DistrictID = "";
                        if (d && d.length > 10) {
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

                        //if (userlevel == "39") {
                        //    $('.generate').show(500);
                        //    $('.approve').hide(0);
                        //}
                        //if (userlevel == "91" || userlevel == "1") {
                        //    $('.approve').show(500);
                        //    $('.generate').hide(0);
                        //}

                        ShowHideDiv();


                        var objvr = {};
                        objvr.ValidID = FYear;
                        objvr.ValidID1 = StateID;
                        objvr.ValidID2 = DistrictID;
                        objvr.ValidID3 = BlockID;

                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            url: 'GISCluster.aspx/Submit_Cluster_Info',
                            data: JSON.stringify(objvr),
                            success: function (response) {

                                ShowHideButton(response.d);

                                //if (response.d == "1") {
                                //    $('#btn_Submit_Cluster').prop('disabled', true);
                                //    $('#btn_Submit_Cluster').html('Cluster Submitted for Approval');
                                //    $("#btn_Gen_Cluster").hide();
                                //    $("#btn_approve_Cluster").show();
                                //    $("#btn_reject_Cluster").show();
                                //    //$("#btn_Submit_Cluster").hide();
                                //    //$("#btn_Gen_Cluster").hide();
                                //}
                                //else {
                                //    $('#btn_Submit_Cluster').prop('disabled', false);
                                //    $('#btn_Submit_Cluster').html('Submit Cluster for Approval');
                                //    $("#btn_Gen_Cluster").show();
                                //    $("#btn_approve_Cluster").hide();
                                //    $("#btn_reject_Cluster").hide();
                                //}
                                //Get_Details();
                            },
                            error: function () {
                                //alert("Error!!")
                                return false;
                            }
                        });
                    }


                    catch (e) {
                        //alert(e.message)
                    }
                    //setTimeout(logMe, 1000);
                    //Display selected Row data in Alert Box.
                    //alert(message);

                }

                <%--function Submit_Cluster_Info_BO() {

                    try {

                        var userlevel = '<%=Session["user_level"]%>';
                        var FYear = $("[id$=ddlYear] option:selected").text();
                        var StateID = $("[id$=ddlState]").val();
                        var d = $("[id$=ddlDistrict]").val();
                        var did = "";
                        var DistrictID = "";
                        if (d && d.length > 10) {
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

                        //if (userlevel == "39") {
                        //    $('.generate').show(500);
                        //    $('.approve').hide(0);
                        //}
                        //if (userlevel == "91" || userlevel == "1") {
                        //    $('.approve').show(500);
                        //    $('.generate').hide(0);
                        //}

                        ShowHideDiv();


                        var objvr = {};
                        objvr.ValidID = FYear;
                        objvr.ValidID1 = StateID;
                        objvr.ValidID2 = DistrictID;
                        objvr.ValidID3 = BlockID;

                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            url: 'GISCluster.aspx/Submit_Cluster_Info_BO',
                            data: JSON.stringify(objvr),
                            success: function (response) {

                                //ShowHideButton_BO(response.d);


                            },
                            error: function () {
                                //alert("Error!!")
                                return false;
                            }
                        });
                    }


                    catch (e) {
                        //alert(e.message)
                    }
                    //setTimeout(logMe, 1000);
                    //Display selected Row data in Alert Box.
                    //alert(message);

                }--%>

                <%--function Get_ApprovalStatus_Info() {

                    try {

                        var userlevel = '<%=Session["user_level"]%>';
                        var FYear = $("[id$=ddlYear] option:selected").text();
                        var StateID = $("[id$=ddlState]").val();
                        var d = $("[id$=ddlDistrict]").val();
                        var did = "";
                        var DistrictID = "";
                        if (d && d.length > 10) {
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


                        //if (userlevel == "39") {
                        //    $('.generate').show(500);
                        //    $('.approve').hide(0);
                        //}
                        //if (userlevel == "91" || userlevel == "1") {
                        //    $('.approve').show(500);
                        //    $('.generate').hide(0);
                        //}

                        ShowHideDiv();


                        var objvr = {};
                        objvr.ValidID = FYear;
                        objvr.ValidID1 = StateID;
                        objvr.ValidID2 = DistrictID;
                        objvr.ValidID3 = BlockID;

                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            url: 'GISCluster.aspx/Get_Approval_Status_Info',
                            data: JSON.stringify(objvr),
                            success: function (response) {
                                var st = response.d;
                                ShowHideButtonStatus(st);
                                if (st == "0") {
                                    SubmitCluster_Info();
                                }

                                //if (response.d == "1") {  
                                //    $('#btn_Submit_Cluster').prop('disabled', true);
                                //    $('#btn_Submit_Cluster').html('Cluster Submitted for Approval');
                                //    $("#btn_Gen_Cluster").hide();
                                //    $("#btn_approve_Cluster").show();
                                //    $("#btn_reject_Cluster").show();
                                //    //$("#btn_Submit_Cluster").hide();
                                //    //$("#btn_Gen_Cluster").hide();
                                //}
                                //else {
                                //    $('#btn_Submit_Cluster').prop('disabled', false);
                                //    $('#btn_Submit_Cluster').html('Submit Cluster for Approval');
                                //    $("#btn_Gen_Cluster").show();
                                //    $("#btn_approve_Cluster").hide();
                                //    $("#btn_reject_Cluster").hide();
                                //}
                                //Get_Details();
                            },
                            error: function () {
                                //alert("Error!!")
                                return false;
                            }
                        });
                    }


                    catch (e) {
                        //alert(e.message)
                    }
                    //setTimeout(logMe, 1000);
                    //Display selected Row data in Alert Box.
                    //alert(message);

                }--%>

                function Get_Generate_Cluster_Info() {

                    try {

                        var userlevel = '<%=Session["user_level"]%>';
                        var FYear = $("[id$=ddlYear] option:selected").text();
                        var StateID = $("[id$=ddlState]").val();
                        var d = $("[id$=ddlDistrict]").val();
                        var did = "";
                        var DistrictID = "";
                        if (d && d.length > 10) {
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

                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            url: 'GISCluster.aspx/Get_Generate_Cluster_Info',
                            data: JSON.stringify(objvr),
                            success: function (response) {
                                var textboxValues = response.d;
                                var splitValues = textboxValues.split('#');
                                var villages = splitValues[0];
                                var oosc = splitValues[1];
                                var distance = splitValues[2];

                                $('[id$=txt_NoofVillages]').val(villages);
                                $('[id$=txt_NoofOOSC]').val(oosc);
                                $('[id$=txt_Distance]').val(distance);

                                if (villages > 0) {
                                    $("#btn_Gen_Cluster_Submit").hide();
                                    $("#btn_ReGen_Cluster_Submit").show();
                                }
                                else {
                                    $("#btn_Gen_Cluster_Submit").show();
                                    $("#btn_ReGen_Cluster_Submit").hide();
                                }
                            },
                            error: function () {
                                return false;
                            }
                        });
                    }


                    catch (e) {
                        //alert(e.message)
                    }

                }

                function SetFirstVillage(locid, th) {
                    $(".update_overlay").show();
                    try {

                        var a = locid.split("#");

                        var cCode = a[0];
                        var cName = a[1];
                        var vCode = a[2];
                        var vName = a[3];





                        var FYear = $("[id$=ddlYear] option:selected").text();
                        var StateID = $("[id$=ddlState]").val();
                        var d = $("[id$=ddlDistrict]").val();
                        var did = "";
                        var DistrictID = "";
                        if (d && d.length > 10) {
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

                        if (StateID == "") {
                            Show_ModalAlert("Please select state !");
                            return;
                        }
                        if (DistrictID == "") {
                            Show_ModalAlert("Please select district !");
                            return;
                        }
                        if (BlockID == "") {
                            Show_ModalAlert("Please select Block !");
                            return;
                        }

                        var objvr = {};
                        objvr.ValidID = FYear;
                        objvr.ValidID1 = StateID;
                        objvr.ValidID2 = DistrictID;
                        objvr.ValidID3 = BlockID;
                        objvr.ValidID4 = ClusterID;
                        objvr.ValidID5 = vCode;
                        objvr.ValidID6 = vName;

                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            url: 'GISCluster.aspx/UpdateFirstVillage',
                            data: JSON.stringify(objvr),
                            success: function (response) {
                                Fill_Cluster("ddlGP");
                                call_function('', '');
                                Get_Details();
                                Show_ModalAlert(response.d);
                                $(".update_overlay").hide();
                            },
                            error: function () {
                                $(".update_overlay").hide();
                                //alert("Error!!")
                                Show_ModalAlert("Please try again !!");
                                return false;
                            }
                        });
                    }


                    catch (e) {
                        $(".update_overlay").hide();
                        Show_ModalAlert("Please try again !!");
                        //alert(e.message)
                    }

                    //setTimeout(logMe, 1000);
                }
                function confirmRegenerate(clusterId) {
                    // Display confirmation alert
                    var confirmation = confirm("Are you sure you want to regenerate the cluster?");

                    // Check user's choice
                    if (confirmation) {
                        // If user clicks 'OK', call GenerateCluster function
                        GenerateCluster(clusterId);
                    } else {
                        // If user clicks 'Cancel', do nothing or perform other actions if needed
                        // You can add additional logic here if necessary
                    }
                }
                function confirmDelete() {
                    // Display confirmation alert
                    var confirmation = confirm("Are you sure you want to delete the cluster?");

                    // Check user's choice
                    if (confirmation) {
                        // If user clicks 'OK', call GenerateCluster function
                        DeleteCluster();
                    } else {
                        // If user clicks 'Cancel', do nothing or perform other actions if needed
                        // You can add additional logic here if necessary
                    }
                }
                function confirmUnlock() {
                    // Display confirmation alert
                    var confirmation = confirm("Are you sure you want to Unlock the cluster?");

                    // Check user's choice
                    if (confirmation) {
                        // If user clicks 'OK', call GenerateCluster function
                        UnlockCluster();
                    } else {
                        // If user clicks 'Cancel', do nothing or perform other actions if needed
                        // You can add additional logic here if necessary
                    }
                }
                function confirmSubmit() {
                    // Display confirmation alert
                    var confirmation = confirm("Are you sure you want to Submit the cluster for approval?");

                    // Check user's choice
                    if (confirmation) {
                        // If user clicks 'OK', call GenerateCluster function
                        SubmitCluster();
                    } else {
                        // If user clicks 'Cancel', do nothing or perform other actions if needed
                        // You can add additional logic here if necessary
                    }
                }
                function confirmSubmitBO() {
                    // Display confirmation alert
                    var confirmation = confirm("Are you sure you want to Submit the cluster?");

                    // Check user's choice
                    if (confirmation) {
                        // If user clicks 'OK', call GenerateCluster function
                        SubmitClusterBO();
                    } else {
                        // If user clicks 'Cancel', do nothing or perform other actions if needed
                        // You can add additional logic here if necessary
                    }
                }
                $(function () {
                    $('#allcb').change(function () {
                        if ($(this).prop('checked')) {
                            $('tbody tr td input[type="checkbox"]').each(function () {
                                $(this).prop('checked', true);
                            });
                        } else {
                            $('tbody tr td input[type="checkbox"]').each(function () {
                                $(this).prop('checked', false);
                            });
                        }
                    });
                });
            </script>
            <script type="text/javascript">
                function Show_ModalAlert(msg) {
                    $('[id*="lbl_messages"]').text(msg);
                    $find("ModalAlertA").show();
                }
            </script>
            <script type="text/javascript">
                function Show_ModalUpdate(msg) {
                    $('[id*="lbl_Text"]').text(msg);
                    $find("ModalAlertB").show();
                }
                function Hide_ModalUpdate() {
                    $find("ModalAlertB").hide();
                }
                function Hide_ModalReject() {
                    $find("ModalAlertR").hide();
                }
                function Show_ModalReject() {

                    //Reference the Table.
                    //var grid = document.getElementById("tblLocDetails");

                    ////Reference the CheckBoxes in Table.
                    //var checkBoxes = grid.getElementsByTagName("INPUT");
                    //var cid = "";

                    ////Loop through the CheckBoxes.
                    //for (var i = 0; i < checkBoxes.length; i++) {
                    //    if (checkBoxes[i].checked) {
                    //        var row = checkBoxes[i].parentNode.parentNode;
                    //        cid += row.cells[1].innerHTML;
                    //        cid += ',';

                    //    }
                    //}
                    //cid = cid.slice(0, -1);

                    var StateID = $("[id$=ddlState]").val();
                    var DistrictID = $("[id$=ddlDistrict]").val();
                    var BlockID = $("[id$=ddlBlock]").val();
                    if (StateID == "") {
                        Show_ModalAlert("Please select state !");
                        return;
                    }
                    if (DistrictID == "") {
                        Show_ModalAlert("Please select district !");
                        return;
                    }
                    //if (BlockID == "") {
                    //    Show_ModalAlert("Please select block !");
                    //    return;
                    //}
                    $find("ModalAlertR").show();
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
                $(document).ready(function () {

                    $('#btn_approve_Cluster').hide();
                    $('#btn_reject_Cluster').hide();
                    $('#btn_Gen_Cluster').hide();
                    $('#btn_Submit_Cluster_BO').hide();
                    $('#btn_Submit_Cluster').hide();
                    $('#btn_Unlock_Cluster').hide();

                    bindMaster();
                    SubmitCluster_Info();
                    $('#btn_Submit_Cluster_BO').hide();
                    var userlevel = '<%=Session["user_level"]%>';
                    //Get_ApprovalStatus_Info();
                    //if (userlevel == '19') {
                    //    $('#btn_Submit_Cluster_BO').hide();
                    //    Submit_Cluster_Info_BO();
                    //}
                    getmap('', '');
                    Get_Details();

                    //Get_ApprovalStatus_Info();
                    //setTimeout(logMe, 1000);

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
                function GenerateCluster(genflag) {
                    $(".update_overlay").show();
                    try {
                        if (required()) {
                            var FYear = $("[id$=ddlYear] option:selected").text();
                            var StateID = $("[id$=ddlState]").val();
                            var d = $("[id$=ddlDistrict]").val();
                            var did = "";
                            var DistrictID = "";
                            if (d && d.length > 10) {
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
                            var numberOfVillages = $('#<%=txt_NoofVillages.ClientID%>').val();
                            var DistanceToCover = $("[id$=txt_Distance]").val();
                            var numberofOOSCs = $("[id$=txt_NoofOOSC]").val();

                            if (StateID == "") {
                                Show_ModalAlert("Please select state !");
                                return;
                            }
                            if (DistrictID == "") {
                                Show_ModalAlert("Please select district !");
                                return;
                            }
                            if (BlockID == "") {
                                Show_ModalAlert("Please select Block !");
                                return;
                            }

                            var objvr = {};
                            objvr.ValidID = FYear;
                            objvr.ValidID1 = StateID;
                            objvr.ValidID2 = DistrictID;
                            objvr.ValidID3 = BlockID;
                            objvr.ValidID4 = numberOfVillages;
                            objvr.ValidID5 = DistanceToCover;
                            objvr.ValidID6 = numberofOOSCs;
                            objvr.ValidID7 = genflag;

                            $.ajax({
                                type: 'POST',
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                url: 'GISCluster.aspx/Get_Cluster',
                                data: JSON.stringify(objvr),
                                success: function (response) {
                                    Fill_Cluster("ddlGP");
                                    call_function('', '');
                                    Get_Details();
                                    HideModalGenerateCluster();
                                    Show_ModalAlert(response.d);
                                    $(".update_overlay").hide();
                                },
                                error: function () {
                                    $(".update_overlay").hide();
                                    //alert("Error!!")
                                    Show_ModalAlert("Please try again !!");
                                    return false;
                                }
                            });
                        }
                        else {
                            $(".update_overlay").hide();
                        }
                    }


                    catch (e) {
                        $(".update_overlay").hide();
                        //alert(e.message)
                        Show_ModalAlert("Please try again !!");
                    }
                    //setTimeout(logMe, 1000);
                }


                function UpdateCluster(locid, th) {

                    var a = locid.split("#");

                    var cCode = a[0];
                    var cName = a[1];
                    var vCode = a[2];
                    var vName = a[3];
                    Fill_Cluster_Map("ddl_Cluster_Map", cCode);
                    $('[id$=lblCluster]').text(cName);
                    $('[id$=lblVillage]').text(vName);
                    $('[id$=cluster_id]').val(cCode);
                    $('[id$=village_id]').val(vCode);

                    Show_ModalUpdate(locid);

                }
                function UpdateVillageCluster() {
                    $(".update_overlay").show();
                    try {

                        var FYear = $("[id$=ddlYear] option:selected").text();
                        var StateID = $("[id$=ddlState]").val();
                        var d = $("[id$=ddlDistrict]").val();
                        var did = "";
                        var DistrictID = "";
                        if (d && d.length > 10) {
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
                        var VillageID = $("[id$=village_id]").val();
                        var UpdatedClusterid = $("[id$=ddl_Cluster_Map]").val();
                        //var numberOfVillages = $("[id$=Txt_villageNo]").val();
                        //var DistanceToCover = $("[id$=txtdistancemax]").val();
                        //var numberofOOSCs = $("[id$=Txt_OOSCNo]").val();

                        if (StateID == "") {
                            Show_ModalAlert("Please select state !");
                            return;
                        }
                        if (DistrictID == "") {
                            Show_ModalAlert("Please select district !");
                            return;
                        }
                        if (BlockID == "") {
                            Show_ModalAlert("Please select Block !");
                            return;
                        }
                        if (UpdatedClusterid == "") {
                            Show_ModalAlert("Please select Cluster !");
                            return;
                        }

                        var objvr = {};
                        objvr.ValidID = FYear;
                        objvr.ValidID1 = StateID;
                        objvr.ValidID2 = DistrictID;
                        objvr.ValidID3 = BlockID;
                        objvr.ValidID4 = ClusterID;
                        objvr.ValidID5 = VillageID;
                        objvr.ValidID6 = UpdatedClusterid;
                        objvr.ValidID7 = "";
                        objvr.ValidID8 = "";
                        objvr.ValidID9 = "";

                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            url: 'GISCluster.aspx/UpdateVillageCluster',
                            data: JSON.stringify(objvr),
                            success: function (response) {
                                call_function('', '');
                                Hide_ModalUpdate();
                                Get_Details();
                                Show_ModalAlert(response.d);
                                $(".update_overlay").hide();
                            },
                            error: function () {
                                $(".update_overlay").hide();
                                //alert("Error!!")
                                Show_ModalAlert("Please try again !!");
                                return false;
                            }
                        });
                    }


                    catch (e) {
                        $(".update_overlay").hide();
                        Show_ModalAlert("Please try again !!");
                        //alert(e.message)
                    }

                    //setTimeout(logMe, 1000);
                }

                function Show() {
                    $("#div_Cluster").show();
                    //$('[id$=txt_NoofOOSC]').val("200");
                    //$('[id$=txt_NoofVillages]').val("10");
                    //$('[id$=txt_Distance]').val("10");
                    $("#div_btnGenerate").hide();
                }
                function Hide() {
                    $("#div_Cluster").hide();
                    //$('[id$=txt_NoofOOSC]').val("");
                    //$('[id$=txt_NoofVillages]').val("");
                    //$('[id$=txt_Distance]').val("");
                    $("#div_btnGenerate").show();
                }

                function required() {

                    var _oosc = $('[id$=txt_NoofOOSC]').val();
                    var _villages = $('[id$=txt_NoofVillages]').val();
                    var _distance = $('[id$=txt_Distance]').val();

                    if (_villages == "") {
                        alert("Please enter no of villages !");
                        return;
                    }
                    if (_oosc == "") {
                        alert("Please enter no of oosc !");
                        return;
                    }

                    if (_distance == "") {
                        alert("Please enter max distance !");
                        return;
                    }
                    return true;
                }

                function ShowModalGenerateCluster() {
                    Get_Generate_Cluster_Info();
                    $find("ModalAlertG").show();
                    //$('[id$=txt_NoofOOSC]').val("200");
                    //$('[id$=txt_NoofVillages]').val("10");
                    //$('[id$=txt_Distance]').val("10");
                }
                function HideModalGenerateCluster() {
                    //$('[id$=txt_NoofOOSC]').val("");
                    //$('[id$=txt_NoofVillages]').val("");
                    //$('[id$=txt_Distance]').val("");
                    $find("ModalAlertG").hide();
                }


                //setTimeout(function () {
                //    $('#tblLocDetails').dataTable();
                //}, 1000);
                function bindMaster() {
                    Fill_FYear_NextFY("ddlYear");
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
                    Fill_GroupBy("ddlGroupby");
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
                    //Get_Details();
                }

                function Go_to_Location(locid, th) {
                    ZoomToLatLong_Click(locid);
                }
                function Fill_FYear_NextFY(ddlID) {

                    var objvr = {};
                    objvr.ValidID = "";

                    _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_FYear_NextFY2025", "", objvr, true);
                }
                function Fill_State(ddlID) {

                    var objvr = {};
                    objvr.ValidID = $("[id$=ddlYear]").val();

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
                    if (d && d.length > 10) {
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
                    if (d && d.length > 10) {
                        did = $("[id$=ddlDistrict]").val().split("#");
                        DistrictID = did[0];
                    }
                    else {
                        DistrictID = d || "";
                    }

                    var b = $("[id$=ddlBlock]").val();
                    var bid = "";
                    var BlockID = "";
                    if (b && b.length > 10) {
                        bid = $("[id$=ddlBlock]").val().split("#");
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
                function Fill_Cluster_Map(ddlID, clusterid) {
                    var FYear = $("[id$=ddlYear] option:selected").text();
                    var StateID = $("[id$=ddlState]").val();
                    var d = $("[id$=ddlDistrict]").val();
                    var did = "";
                    var DistrictID = "";
                    if (d && d.length > 10) {
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
                    objvr.ValidID4 = clusterid;

                    _Fill_ComboBox_Json(ddlID, "CommonXyz.aspx/Fill_Cluster_Map", "Select", objvr, true);
                }


                function Get_Details() {
                    $(".update_overlay").show();
                    try {
                        debugger;
                        var FYear = $("[id$=ddlYear] option:selected").text();
                        var StateID = $("[id$=ddlState]").val();
                        var d = $("[id$=ddlDistrict]").val();
                        var did = "";
                        var DistrictID = "";
                        if (d && d.length > 10) {
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

                        if (StateID == "") {
                            Show_ModalAlert("Please select state !");
                            return;
                        }
                        if (DistrictID == "") {
                            Show_ModalAlert("Please select district !");
                            return;
                        }

                        var objvr = {};
                        objvr.ValidID = FYear;
                        objvr.ValidID1 = StateID;
                        objvr.ValidID2 = DistrictID;
                        objvr.ValidID3 = BlockID;
                        objvr.ValidID4 = ClusterID;

                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            url: 'GISCluster.aspx/Get_MapDetails',
                            data: JSON.stringify(objvr),
                            success: function (response) {
                                $("#MapSummary").html(response.d);
                                ZoomToLatLong();

                                SubmitCluster_Info();
                                //Get_ApprovalStatus_Info()

                                $(".update_overlay").hide();


                            },
                            error: function () {
                                //alert("Error!!")
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
                    debugger;
                    state = $("[id$=ddlState]").val();
                    dis = $("[id$=ddlDistrict]").val();
                    blk = $("[id$=ddlBlock]").val();
                    gp = $("[id$=ddlGP]").val();
                    setTimeout(function () {
                        if (state != "" && dis == "") {
                            debugger;
                            map.removeLayer(BlockMap);
                            map.removeLayer(VillageMap);

                            if (state == "9" || state == "9A" || state == "9B" || state == "9C" || state == "9D") {
                                var initPosition = [25.3903, 80.8913];
                                map.setView(initPosition, 9);
                            }
                            if (state == "23") {
                                var initPosition = [23.065833940118736, 74.62120056152345];
                                map.setView(initPosition, 9);
                            }
                            if (state == "8") {
                                var initPosition = [27.391277, 73.432617];
                                map.setView(initPosition, 10);
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
                            if (click == 0) {
                                map.addLayer(BlockMap);
                            }
                            gotolatlong(blk);

                        }
                        else if (gp != "" && blk != "") {

                            map.addLayer(VillageMap);
                            map.removeLayer(BlockMap);
                            gotolatlong(blk);

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

                    $('#map-search').hide();

                    state = $("[id$=ddlState]").val();
                    dis = $("[id$=ddlDistrict]").val();
                    blk = $("[id$=ddlBlock]").val();
                    gp = $("[id$=ddlGP]").val();
                    var v_status = $("[id$=ddlVillageStatus]").val();
                    setTimeout(function () {
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

                    }, 1000);

                }

                function ZoomToLatLong_Click(loc) {

                    state = $("[id$=ddlState]").val();
                    dis = $("[id$=ddlDistrict]").val();
                    blk = $("[id$=ddlBlock]").val();
                    gp = $("[id$=ddlGP]").val();
                    if (state != "" && dis == "") {
                        if (state == "9" || state == "9A" || state == "9B" || state == "9C" || state == "9D") {
                            var initPosition = [25.082797868, 81.053105818];
                            map.setView(initPosition, 10);
                        }
                        if (state == "23") {
                            var initPosition = [23.065833940118736, 74.62120056152345];
                            map.setView(initPosition, 10);
                        }
                        if (state == "8") {
                            var initPosition = [27.391277, 73.432617];
                            map.setView(initPosition, 10);
                        }
                        //map.addLayer(StateMap);
                        map.removeLayer(BlockMap);
                        map.removeLayer(VillageMap);
                    }
                    else if (dis != "" && blk == "") {

                        if (loc == "430E78F36C9941E9AFA25449A") {
                            var initPosition = [23.065833940118736, 74.62120056152345];
                            map.setView(initPosition, 10);
                            map.addLayer(BlockMap);
                            map.removeLayer(VillageMap);
                        }
                        else if (loc == "3741BDDEDC1948A69AA1E5C7C") {
                            var initPosition = [25.082797868, 81.053105818];
                            map.setView(initPosition, 10);
                            map.addLayer(BlockMap);
                            map.removeLayer(VillageMap);
                        }
                        else {
                            var initPosition = [25.082797868, 81.053105818];
                            map.setView(initPosition, 10);
                            map.removeLayer(BlockMap);
                            map.removeLayer(VillageMap);
                        }

                    }
                    else if (blk != "" && gp == "") {


                        if (blk == "3741BDDEDC1948A69AA1E5C7C") {
                            var a = loc.split("#");

                            var Lat = a[0];
                            var Long = a[1];

                            var initPosition = [a[0], a[1]];
                            map.setView(initPosition, 12);
                            map.removeLayer(BlockMap);
                            map.addLayer(VillageMap);
                        }
                        else if (blk == "430E78F36C9941E9AFA25449A") {
                            var a = loc.split("#");

                            var Lat = a[0];
                            var Long = a[1];

                            var initPosition = [a[0], a[1]];
                            map.setView(initPosition, 12);
                            map.removeLayer(BlockMap);
                            map.addLayer(VillageMap);
                        }
                        else {
                            var initPosition = [25.082797868, 81.053105818];
                            map.setView(initPosition, 10);
                            map.removeLayer(BlockMap);
                            map.removeLayer(VillageMap);
                        }

                    }
                    else if (gp != "" && blk != "") {
                        var a = loc.split("#");

                        var Lat = a[0];
                        var Long = a[1];

                        var initPosition = [a[0], a[1]];
                        map.setView(initPosition, 14);

                        map.removeLayer(BlockMap);
                        map.addLayer(VillageMap);

                    }
                    else {
                        var initPosition = [23.473324, 77.947998];
                        map.setView(initPosition, 4.5);
                        map.removeLayer(BlockMap);
                        map.removeLayer(VillageMap);
                    }

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
                            <div class="panel-heading" style="background-color: transparent; padding: 5px 0px;">

                                <div class="row">
                                    <div class="col-sm-12" style="padding: 0px;">
                                        <div class="dis-flex">
                                            <h4 class="text-danger1" style="font-weight: bold; font-size: medium;">
                                                <asp:Label ID="lblMain" runat="server" Text="GIS Based Clustering"></asp:Label>
                                            </h4>
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
                                        <div class="col-lg-12  search-bg" style="padding-top: 15px !important;">
                                            <div id="container-target">
                                                <div class="form-horizontal">
                                                    <div class="row" style="margin: 0px -15px;">
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
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
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="ddlState" class="col-sm-3  linhei" style="padding-top: 2px; font-weight: bold !important;">State:<span class="mandatory-label"></span></label>
                                                                <div class="col-sm-9 ">
                                                                    <asp:DropDownList ID="ddlState" runat="server" onchange="Fill_District('ddlDistrict');Fill_Block('ddlBlock');Fill_Cluster('ddlGP');" class="form-control ">
                                                                    </asp:DropDownList>
                                                                    <%--<asp:Label ID="lblNumNo" Visible="false" runat="server" Text="Label"></asp:Label>--%>
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
                                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlGP" runat="server" class="form-control" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12" style="padding: 0px">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <input type="button" id="myButton" class="btn btn-danger btn-paddd" style="margin-left: 7rem;" onclick="SubmitCluster_Info();call_function('','');Get_Details();" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>


                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12" style="text-align: right;">
                                                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                                <ContentTemplate>
                                                                    <div style="float: right" class="unlock">
                                                                        <button type="button" id="btn_Unlock_Cluster" class="btn btn-primary mx-2 unlock" onclick="confirmUnlock();">Unlock Cluster</button>
                                                                    </div>
                                                                    <div style="float: right" class="submitcluster">

                                                                        <button type="button" id="btn_Submit_Cluster" class="btn btn-primary mx-2 submitcluster" onclick="confirmSubmit();">Submit Cluster for DOL Approval</button>
                                                                        <label id="lblrejectinfo" style="display: none;">*Cluster Rejected</label>
                                                                    </div>
                                                                    <div style="float: right" class="submit">
                                                                        <button type="button" id="btn_Submit_Cluster_BO" class="btn btn-primary mx-2 submit" onclick="confirmSubmitBO();">Submit Cluster IO</button>
                                                                    </div>
                                                                    <div style="float: right" class="generate" id="div_btnGenerate">
                                                                        <button type="button" id="btn_Gen_Cluster" class="btn btn-primary mx-2 generate" onclick="ShowModalGenerateCluster();">Generate Cluster</button>
                                                                    </div>
                                                                    <div style="float: right" class="approve">
                                                                        <button type="button" id="btn_reject_Cluster" class="btn btn-primary mx-2 approve" onclick="Show_ModalReject();">Reject Cluster</button>
                                                                    </div>
                                                                    <div style="float: right" class="approve">
                                                                        <button type="button" id="btn_approve_Cluster" class="btn btn-primary mx-2 approve" onclick="ApproveCluster();">Approve Cluster</button>
                                                                    </div>
                                                                    <div style="float: right; display: none;">
                                                                        <button type="button" id="btn_delete_Cluster" class="btn btn-primary" onclick="confirmDelete();">Delete Cluster</button>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <%-- <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12 pull-right unlock" style="padding-left: 7%;">

                                                            


                                                        </div>

                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12 pull-right" style="padding-left: 7%; display: none;">
                                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                                <ContentTemplate>
                                                                    

                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12 pull-right generate" style="padding-left: 2%;">
                                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                                <ContentTemplate>
                                                                    

                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12 pull-right generate" style="padding-left: 6%;" id="div_btnGenerate">
                                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                <ContentTemplate>
                                                                   


                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12 pull-right approve" style="padding-left: 9%;">
                                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                                <ContentTemplate>
                                                                    

                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12 pull-right approve" style="padding-left: 15%;">
                                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                                <ContentTemplate>
                                                                   

                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>--%>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 " id="div_Cluster" style="display: none; padding-bottom: 10px;">
                        <div class="search-bg">
                            <div class="dis-flex" style="padding: 0px;">
                                <div>
                                    <h3 class="text-danger" style="margin: 0px;">
                                        <asp:Label ID="Label1" runat="server" Text="Criteria"></asp:Label>
                                    </h3>
                                </div>

                            </div>
                            <div class="row" style="margin-bottom: 12px;">
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                                    <h4>No of Village </h4>
                                    <div class="inp-div">
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                                    <h4>No of OOSC </h4>
                                    <div class="inp-div">
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6">
                                    <h4>Maximum Distance </h4>
                                    <div class="inp-div">
                                    </div>
                                </div>
                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-6">
                                    <h4></h4>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" style="float: left; padding-right: 10px; padding-top: 32px;">
                                        <ContentTemplate>

                                            <%--<input type="button" id="btn_Gen_Cluster" class="btn btn-danger"  />--%>
                                            <%--<asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-right" OnClientClick="ZoomToLatLong();" OnClick="btnSerach_Click" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />--%>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-6">
                                    <h4></h4>
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server" style="float: left; padding-right: 10px; padding-top: 32px;">
                                        <ContentTemplate>

                                            <%--<input type="button" id="btn_Gen_Cluster" class="btn btn-danger"  />--%>
                                            <%--<asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-right" OnClientClick="ZoomToLatLong();" OnClick="btnSerach_Click" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />--%>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-6">
                                    <h4></h4>
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" style="float: left; padding-top: 32px;">
                                        <ContentTemplate>

                                            <%--<input type="button" id="btn_Gen_Cluster" class="btn btn-danger"  />--%>
                                            <%--<asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-right" OnClientClick="ZoomToLatLong();" OnClick="btnSerach_Click" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />--%>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                                                <h4>Report: GIS Based Clustering</h4>
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
                                                <div id="MapSummary">
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
                                                                <%--<label for="SearchType" class="linhei">Search Type:  </label>--%>
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
                                                                <%--<label for="SchoolType" class="linhei">School Type:   </label>--%>
                                                                <asp:DropDownList ID="ddlSchoolType" runat="server" class="form-control">
                                                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Primary" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Upper Primary" Value="2"></asp:ListItem>
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
                                                                <input type="button" class="btn btn-sm search-mp btn-primary" onclick="callSerchType('','');addLayers();" />
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
                                            var District_Map = L.layerGroup();
                                            var BlockMap = L.layerGroup();
                                            var VillageMap = L.layerGroup();
                                            var schoolMarkers;
                                            var layerControl;
                                            var StateMap = L.layerGroup();
                                            var GrayLyr, StreetLyr, Terrain, ImageryLyr;
                                            var mbAttr = "";
                                            var BaseUrls = 'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1Ijoia3dyaXNhY2l3cm0iLCJhIjoiY2xma3p3NmpoMDBhaTNwbnV1NnVkMGp2ZCJ9.1ASKnwxbjSZxZGiXn0xl4Q';

                                            var overlayMaps = {};
                                            function BaseLyrOptionsM(ids) {
                                                return {
                                                    maxZoom: 18,
                                                    attribution: mbAttr,
                                                    id: ids,
                                                    tileSize: 512,
                                                    zoomOffset: -1
                                                };
                                            }
                                            function initializeBaseLayers() {
                                                //var mbAttr = "";
                                                //var BaseUrls = 'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1Ijoia3dyaXNhY2l3cm0iLCJhIjoiY2xma3p3NmpoMDBhaTNwbnV1NnVkMGp2ZCJ9.1ASKnwxbjSZxZGiXn0xl4Q';


                                                if (!map) return;
                                                // Initialize the layers
                                                GrayLyr = L.tileLayer(BaseUrls, BaseLyrOptionsM('mapbox/light-v9'));
                                                StreetLyr = L.tileLayer(BaseUrls, BaseLyrOptionsM('mapbox/streets-v11'));
                                                Terrain = L.tileLayer(BaseUrls, BaseLyrOptionsM('mapbox/outdoors-v11'));
                                                ImageryLyr = L.esri.basemapLayer('Imagery').addTo(map);

                                                //function BaseLyrOptionsM(ids) {
                                                //    return {
                                                //        maxZoom: 18,
                                                //        attribution: mbAttr,
                                                //        id: ids,
                                                //        tileSize: 512,
                                                //        zoomOffset: -1
                                                //    };
                                                //}

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

                                                if (state == "9" || state == "9A" || state == "9B" || state == "9C" || state == "9D") {
                                                    map = L.map('map', { maxZoom: 18, minZoom: 4, dragging: true, fullscreenControl: { pseudoFullscreen: false } }).setView(new L.LatLng(25.3903, 80.8913), 4.5);
                                                }
                                                if (state == "23") {
                                                    map = L.map('map', { maxZoom: 18, minZoom: 4, dragging: true, fullscreenControl: { pseudoFullscreen: false } }).setView(new L.LatLng(23.065833940118736, 74.62120056152345), 4.5);
                                                }
                                                if (state == "8") {
                                                    map = L.map('map', { maxZoom: 18, minZoom: 4, dragging: true, fullscreenControl: { pseudoFullscreen: false } }).setView(new L.LatLng(27.391277, 73.432617), 4.5);
                                                }


                                                if (District_Map) District_Map.clearLayers();
                                                if (BlockMap) BlockMap.clearLayers();
                                                if (VillageMap) VillageMap.clearLayers();

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
                                                if (d && d.length > 10) {
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
                                                //callStateMap(GrayLyr, StreetLyr, Terrain, ImageryLyr);

                                                //// hideloader();
                                                //bindDistrict();
                                                //bindBlock(flag, locationid);
                                                //bindClusterVillage(flag, locationid);
                                                //bindSchools(flag, locationid);

                                            }
                                            $(".update_overlay").hide();

                                            function call_function(flag, locationid) {
                                                debugger;
                                                if (District_Map) {
                                                    map.removeLayer(District_Map);
                                                }
                                                BlockMap.clearLayers();
                                                VillageMap.clearLayers();
                                                var Fyear = $("[id$=ddlYear]").val();
                                                var d = $("[id$=ddlDistrict]").val();
                                                var did = "";
                                                var DistrictID = "";
                                                if (d && d.length > 10) {
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
                                            }

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
                                                if (d && d.length > 10) {
                                                    did = $("[id$=ddlDistrict]").val().split("#");
                                                    DistrictID = did[0];
                                                }
                                                else {
                                                    DistrictID = d;
                                                }
                                                var DistrictJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_District_Layer_View&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';DistrictID:' + DistrictID + '';

                                                fetch('/GISCluster.aspx/GetGeoJson', {
                                                    method: 'POST',
                                                    headers: { 'Content-Type': 'application/json' },
                                                    body: JSON.stringify({ url: DistrictJSONURL })
                                                })
                                                    .then(res => res.json())
                                                    .then(data => {
                                                        const geojson = JSON.parse(data.d);
                                                        // Remove the existing district layer if it exists
                                                        if (District_Map) {
                                                            map.removeLayer(District_Map);
                                                        }

                                                        // Create a new district layer and add it to the map
                                                        District_Map = L.geoJson(geojson, { style: PLVDistrictstyle });
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
                                                var _gridid = "";
                                                var BlockJSONURL = "";

                                                var Fyear = $("[id$=ddlYear]").val();
                                                var _statecode = $("[id$=ddlState]").val();
                                                var d = $("[id$=ddlDistrict]").val();
                                                var did = "";
                                                var _districtcode = "";

                                                if (d && d.length > 10) {
                                                    did = $("[id$=ddlDistrict]").val().split("#");
                                                    _districtcode = did[0];
                                                } else {
                                                    _districtcode = d || "";
                                                }

                                                //if (d.length > 10) {
                                                //    did = $("[id$=ddlDistrict]").val().split("#");
                                                //    _districtcode = did[0];
                                                //}
                                                //else {
                                                //    _districtcode = d;
                                                //}


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
                                                        BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Block_Lyr_View_FilterNWG&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _locguidBlock + '';
                                                    } else {
                                                        BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Block_Lyr_View_NewNWG&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + '';
                                                    }
                                                }
                                                else {
                                                    BlockJSONURL = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Block_Lyr_View_FilterNWG&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + '';
                                                }
                                                if (map && BlockMap && map.hasLayer(BlockMap)) {
                                                    map.removeLayer(BlockMap);
                                                }
                                                //BlockMap = L.layerGroup();
                                                console.log("Block_" + BlockJSONURL);
                                                fetch('/GISCluster.aspx/GetGeoJson', {
                                                    method: 'POST',
                                                    headers: { 'Content-Type': 'application/json' },
                                                    body: JSON.stringify({ url: BlockJSONURL })
                                                })
                                                    .then(res => res.json())
                                                    .then(data => {
                                                        const geojson = JSON.parse(data.d);

                                                        // Create a GeoJSON layer and add it to the map
                                                        BlockMap = new L.geoJson(geojson, {
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
                                                debugger;
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
                                                console.log("Schoolmarker_" + pUrl);

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

                                                //    //if (typeof layerControl !== "undefined") {
                                                //    //    layerControl.addOverlay(schoolMarkers, "School Markers");
                                                //    //} else {
                                                //    //    layerControl = L.control.layers(null, overlayMaps, { collapsed: false }).addTo(map);
                                                //    //}
                                                //});
                                                //-------------gauravnew--//
                                                fetch('/GISCluster.aspx/GetGeoJson', {
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
                                                        L.geoJson(geojson, {
                                                            pointToLayer: function (feature, latlng) {
                                                                var schoolIcon = L.icon({
                                                                    iconUrl: getIconUrl(feature.properties.SchoolLevel),
                                                                    iconSize: [40, 40]
                                                                });

                                                                var pMarker = L.marker(latlng, { icon: schoolIcon, title: feature.properties.georefcode })
                                                                    .bindPopup(createPopupContent(feature));

                                                                schoolMarkers.addLayer(pMarker);
                                                                return pMarker;
                                                            }
                                                        });

                                                        map.addLayer(schoolMarkers);
                                                        overlayMaps["School Markers"] = schoolMarkers;

                                                        //if (typeof layerControl !== "undefined") {
                                                        //    layerControl.addOverlay(schoolMarkers, "School Markers");
                                                        //} else {
                                                        //    layerControl = L.control.layers(null, overlayMaps, { collapsed: false }).addTo(map);
                                                        //}
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
                                                debugger;
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



                                                //var StateMap = L.layerGroup();
                                                map.spin(true, spinnerOptions);
                                                fetch('/GISCluster.aspx/GetGeoJson', {
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
                                                            "Cluster": VillageMap

                                                        };
                                                        // Remove previous layer control
                                                        if (window.layerControl) {
                                                            map.removeControl(window.layerControl);
                                                        }

                                                        // Add new layer control
                                                        window.layerControl = L.control.layers(null, overlayMaps).addTo(map);

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
                                                debugger;
                                                click = 0;
                                                var VlgClusterJSONURLNew = "";
                                                VillageMap.clearLayers();

                                                var _statecode = $("[id$=ddlState]").val();
                                                var d = $("[id$=ddlDistrict]").val();
                                                var did = "";
                                                var _districtcode = "";
                                                if (d && d.length > 10) {
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
                                                    _clusterid = b[0];
                                                    _locguid = b[2];
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
                                                        VlgClusterJSONURLNew = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_Cluster_View_Filter_2026&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _locguid + ';vstatus:' + vstatus + '';
                                                    } else {
                                                        if (_blockcode == "" || _blockcode == null) {
                                                            VlgClusterJSONURLNew = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_Cluster_View_DistrictNWG&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';vstatus:' + vstatus + '';
                                                        }
                                                        else {
                                                            VlgClusterJSONURLNew = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_Cluster_View_new&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';vstatus:' + vstatus + '';
                                                        }

                                                    }
                                                } else {
                                                    if (_clusterid == "99" && _gridid == "") {
                                                        VlgClusterJSONURLNew = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chirakoot_Cluster_Village_unassinedNWG&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';vstatus:' + vstatus + '';
                                                    }
                                                    else {
                                                        if (_gridid == "villageclick") {
                                                            if (_clusterid == "99") {
                                                                VlgClusterJSONURLNew = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chirakoot_Cluster_Village_unassined_FilterNWG&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';vil:' + _locguid + ';vstatus:' + vstatus + '';
                                                            }
                                                            else {
                                                                VlgClusterJSONURLNew = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chirakoot_Cluster_VillageNWG&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid + ';vil:' + _locguid + ';vstatus:' + vstatus + '';
                                                            }

                                                        }
                                                        else {
                                                            VlgClusterJSONURLNew = 'https://geo1server.educategirls.ngo/geoserver/EG/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=EG%3AEG_Chitrakoot_Cluster_View_Filter_2026&maxFeatures=5000&outputFormat=application%2Fjson&viewparams=Fyear:' + Fyear + ';StateCode:' + _statecode + ';DistrictCode:' + _districtcode + ';BlockCode:' + _blockcode + ';Loc:' + _clusterid + ';vstatus:' + vstatus + '';
                                                        }
                                                    }
                                                }

                                                resetVillageLayer();
                                                console.log("der_" + VlgClusterJSONURLNew);
                                                /////////////////////////Cluster////////VlgClusterJSONURLNew//////
                                                // Fetch GeoJSON data using fetch API
                                                //VillageMap = L.layerGroup();
                                                fetch('/GISCluster.aspx/GetGeoJson', {
                                                    method: 'POST',
                                                    headers: { 'Content-Type': 'application/json' },
                                                    body: JSON.stringify({ url: VlgClusterJSONURLNew })
                                                })
                                                    .then(res => res.json())
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
                                                        //fillColor: getColorClusterNew(feature.properties.ClusterNo),
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
                                                        weight: 4,
                                                        color: '#666',
                                                        dashArray: '',
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
                    <div class="alert-pop-body panel panel-default">
                        <div class="header">
                            <asp:Label ID="lbl_PopUpMessages" runat="server" CssClass="LabelHeader" Font-Bold="True"></asp:Label>
                        </div>
                        <div class="body">
                            <h4>
                                <asp:Label ID="lbl_messages" runat="server" CssClass="LabelHeader" Font-Bold="True"
                                    Font-Size="11pt"></asp:Label>
                            </h4>
                            <div class="text-center">
                                <asp:Button ID="btn_cancelalert" runat="server" CssClass="myButton" Text="  OK  "
                                    Height=" " Width=" " />
                            </div>
                        </div>
                    </div>
                    <%--     <div class="footerCategory" align="right">  </div>--%>
                </asp:Panel>
                <asp:HiddenField ID="hdn_alertmodal" runat="server" />
                <asp:Button ID="DoNothing" runat="server" Text="" Style="display: none" />
            </div>

            <div>
                <asp:ModalPopupExtender ID="MPEFormName1" BackgroundCssClass="modalBackground" BehaviorID="ModalAlertB"
                    runat="server" PopupControlID="pnlFormName1" TargetControlID="HFFormName1" CancelControlID="lblFormNameClose1">
                </asp:ModalPopupExtender>
                <asp:HiddenField ID="HFFormName1" runat="server" />
                <asp:Panel ID="pnlFormName1" runat="server" CssClass="model-wid Mpopup1 popup-bx-main  atp_2" Style="display: none;">
                    <div class="popup-bx">
                        <div class="modal-header">
                            <h3 class="text-danger">Update Cluster           
                            <asp:LinkButton ID="lblFormNameClose1" class="btn btn-xs btn-danger pull-right"
                                runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>
                            </h3>
                        </div>
                        <div class="modal-body">
                            <div class="form-group row">
                                <label class="col-sm-4">Cluster :  </label>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblCluster"></asp:Label>
                                    <asp:HiddenField ID="cluster_id" runat="server" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-4">Village :     </label>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblVillage"></asp:Label>
                                    <asp:HiddenField ID="village_id" runat="server" />
                                </div>
                            </div>
                            <div class="form-group row" style="display: none;">
                                <label class="col-sm-4">
                                    No of Village:<span style="color: Red">*</span>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="Txt_villageNo" MaxLength="3" runat="server" class="form-control ">
                                    </asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                        TargetControlID="Txt_villageNo" ValidChars="0123456789">
                                    </asp:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group row" style="display: none;">
                                <label class="col-sm-4">
                                    No of OOSC:<span style="color: Red">*</span>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="Txt_OOSCNo" MaxLength="4" runat="server" class="form-control ">
                                    </asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                        TargetControlID="Txt_OOSCNo" ValidChars="0123456789">
                                    </asp:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group row" style="display: none;">
                                <label class="col-sm-4">
                                    Max Distance:<span style="color: Red">*</span>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtdistancemax" MaxLength="3" runat="server" class="form-control ">
                                    </asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                        TargetControlID="txtdistancemax" ValidChars="0123456789">
                                    </asp:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="ddl_Cluster_Map" class="col-sm-4 linhei">New Cluster:<span style="color: Red">*</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_Cluster_Map" runat="server" class="form-control " />
                                </div>
                            </div>
                            <div class="row" runat="server" id="Div2">
                                <div class="col-sm-4"></div>
                                <div class="col-sm-8">
                                    <button type="button" id="btn_Submit" class="btn btn-primary btn-sm" onclick="UpdateVillageCluster();call_function('','');">Update</button>
                                    <%--<asp:Button ID="btn_Submit" runat="server" Text="Update" Style="margin-top: 5px" CssClass="btn btn-primary btn-sm pull-right" OnClientClick="UpdateVillageCluster();getmap();"/>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <%--
                            <div class="modal-footer">
                            </div>--%>
                        </div>
                    </div>
                
            </div>

             <div>
                 <asp:ModalPopupExtender ID="MPEFormName2" BackgroundCssClass="modalBackground" BehaviorID="ModalAlertG"
                     runat="server" PopupControlID="pnlFormName2" TargetControlID="HFFormName2" CancelControlID="lblFormNameClose2">
                 </asp:ModalPopupExtender>
                 <asp:HiddenField ID="HFFormName2" runat="server" />

                 <asp:Panel ID="pnlFormName2" runat="server" CssClass="model-wid Mpopup1 popup-bx-main atp_3" Style="display: none;">
                     <div class="popup-bx">
                         <div class="modal-header">
                             <h3 class="text-danger">Generate Cluster        
                            <asp:LinkButton ID="lblFormNameClose2" class="btn btn-xs btn-danger pull-right"
                                runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>
                             </h3>
                         </div>
                         <div class="modal-body">
                             <div class="form-group row">
                                 <label class="col-sm-4">
                                     Max No of Village:<span style="color: Red">*</span>
                                 </label>
                                 <div class="col-sm-8">
                                     <asp:TextBox ID="txt_NoofVillages" MaxLength="2" runat="server" class="form-control">
                                     </asp:TextBox>
                                     <asp:FilteredTextBoxExtender ID="Filteredtxt_NoofVillages" runat="server" Enabled="True"
                                         TargetControlID="txt_NoofVillages" ValidChars="0123456789">
                                     </asp:FilteredTextBoxExtender>
                                 </div>
                             </div>

                             <div class="form-group row">
                                 <label class="col-sm-4">
                                     Max No of OOSG:<span style="color: Red">*</span>
                                 </label>
                                 <div class="col-sm-8">
                                     <asp:TextBox ID="txt_NoofOOSC" MaxLength="6" runat="server" class="form-control">
                                     </asp:TextBox>
                                     <asp:FilteredTextBoxExtender ID="Filteredtxt_NoofOOSC" runat="server" Enabled="True"
                                         TargetControlID="txt_NoofOOSC" ValidChars="0123456789">
                                     </asp:FilteredTextBoxExtender>
                                 </div>
                             </div>


                             <div class="form-group row">
                                 <label class="col-sm-4">
                                     Max Distance:<span style="color: Red">*</span>
                                 </label>
                                 <div class="col-sm-8">
                                     <asp:TextBox ID="txt_Distance" MaxLength="3" runat="server" class="form-control">
                                     </asp:TextBox>
                                     <asp:FilteredTextBoxExtender ID="Filteredtxt_Distance" runat="server" Enabled="True"
                                         TargetControlID="txt_Distance" ValidChars="0123456789">
                                     </asp:FilteredTextBoxExtender>
                                 </div>
                             </div>


                             <div class="row" runat="server" id="Div3">
                                 <div class="col-sm-4">
                                 </div>
                                 <div class="col-sm-8">
                                     <div>
                                         <button type="button" id="btn_Gen_Cluster_Submit" class="btn btn-primary" onclick="GenerateCluster('1');">Generate</button>
                                         <button type="button" id="btn_ReGen_Cluster_Submit" class="btn btn-primary" onclick="confirmRegenerate('2');">Regenerate</button>
                                         <button type="button" id="btn_Cancel" class="btn btn-primary" style="display: none;" onclick="HideModalGenerateCluster();">Cancel</button>

                                     </div>
                                 </div>
                             </div>


                         </div>

                         <%--                            <div class="modal-footer">
                            </div>--%>
                     </div>
                 </asp:Panel>
             </div>

            <div>
                <asp:ModalPopupExtender ID="MPE_Reject" BackgroundCssClass="modalBackground" BehaviorID="ModalAlertR"
                    runat="server" PopupControlID="pnl_Reject" TargetControlID="HF_Reject" CancelControlID="lbl_Reject">
                </asp:ModalPopupExtender>
                <asp:HiddenField ID="HF_Reject" runat="server" />

                <asp:Panel ID="pnl_Reject" runat="server" CssClass="model-wid Mpopup1 popup-bx-main atp_3" Style="display: none;">
                    <div class="popup-bx">
                        <div class="modal-header">
                            <h3 class="text-danger" style="margin: 0;">Rejection Reason       
                            <asp:LinkButton ID="lbl_Reject" class="btn btn-xs btn-danger pull-right"
                                runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>
                            </h3>
                        </div>
                        <div class="modal-body">
                            <div style="height: auto;">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="form-group">
                                            <label for="txtReason" class="col-sm-3 linhei" style="padding-top: 2px;">Reason:<span style="color: Red">*</span></label>
                                            <div class="col-sm-9">
                                                <asp:TextBox TextMode="MultiLine" ID="txtReason" runat="server" class="form-control " />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" runat="server" id="Div1" style="margin-bottom: 15px;">
                                        <div class="form-group">

                                            <div class="col-sm-12">
                                                <button type="button" id="btn_Reject" class="btn btn-primary btn-sm pull-right" style="margin-top: 5px" onclick="RejectCluster();">Submit</button>
                                                <%--<asp:Button ID="btn_Reject" runat="server" Text="Submit" Style="margin-top: 5px" CssClass="btn btn-primary btn-sm pull-right" OnClientClick="RejectCluster();"/>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="modal-footer">
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

        </ContentTemplate>
        <Triggers>

            <asp:PostBackTrigger ControlID="LinkButton1" />

        </Triggers>
    </asp:UpdatePanel>


</asp:Content>
