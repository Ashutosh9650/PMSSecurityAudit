<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Culture="en-GB" CodeFile="GISMapping.aspx.cs" Inherits="GISMapping" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%--<link href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" rel="stylesheet" />--%>

     <link href="css/CSS/jquery-ui.css" rel="stylesheet" />

    <%--<script type="text/javascript" src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>--%>
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
    <%--<script type="text/javascript" src="https://cdn.jsdelivr.net/npm/leaflet-easybutton@2/src/easy-button.js"></script>--%>
    <script type="text/javascript" src="js/JS/easy-button.js"></script>

    <%--<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/leaflet-easybutton@2/src/easy-button.css">--%>
    <link rel="stylesheet" href="css/CSS/easy-button.css">

    <script type="text/javascript" src="Leaflet2/bundle.js"></script>

    <script type="text/javascript" src="Leaflet2/leaflet.groupedlayercontrol.min.js"></script>

    <script type="text/javascript" src="Leaflet2/leaflet.spin.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="Leaflet2/L.Control.Locate.js"></script>
    <script type="text/javascript" src="Leaflet2/leaflet-search.js"></script>
    <link href="Leaflet2/leaflet-search.css" rel="stylesheet" type="text/css" />

    <%--  <link type="text/css" href="https://cdn.datatables.net/1.13.7/css/dataTables.bootstrap.min.css">
  <link type="text/css" href="https://cdn.datatables.net/fixedheader/3.4.0/css/fixedHeader.bootstrap.min.css">--%>

    <%--<script type="text/javascript" src="https://code.jquery.com/jquery-3.7.0.js"></script>--%>
    <%--<script type="text/javascript" src="Scripts/jquery.dataTables.min.js"></script>--%>
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
    <script type="text/javascript" src="js/JS/esri-leaflet.js"></script>

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

        /*.MapSummary-wrp .dataTables_wrapper .row:nth-child(1) {
            display: none;
        }

        .MapSummary-wrp .dataTables_wrapper .row:nth-child(3) .col-sm-7 {
            display: none;
        }*/
        /*====================================================*/
        /*        #tblLocDetails_wrapper .row:nth-child(1), #tblLocDetails_wrapper .row:nth-child(3) {
            display: none !important;
        }
*/
        #MapSummary table thead {
            width: calc(100% - 10px) !important;
        }

        #MapSummary1 table thead {
            width: calc(100% - 10px) !important;
        }

        #MapSummary table {
            background: linear-gradient(to bottom, #ffe5e6 0%,#fff8f8 100%);
        }

        #MapSummary1 table {
            background: linear-gradient(to bottom, #ffe5e6 0%,#fff8f8 100%);
        }

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

        #MapSummary1 table tbody::-webkit-scrollbar {
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

        #MapSummary table tbody::-webkit-scrollbar-thumb:window-inactive {
            background: #333;
        }

        #MapSummary1 table tbody::-webkit-scrollbar-thumb:window-inactive {
            background: #333;
        }


        #MapSummary table tbody {
            display: block;
            height: 280px;
            width: 100%;
            overflow-y: auto;
            overflow-x: hidden !important
        }

        #MapSummary1 table tbody {
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

        #MapSummary1 table thead, tbody tr {
            display: table;
            width: 100%;
            table-layout: fixed;
        }

        #MapSummary table thead tr th {
            width: 80px !important;
            background: linear-gradient(to bottom, #ffe5e6 0%,#fff8f8 100%);
        }

        #MapSummary1 table thead tr th {
            width: 80px !important;
            background: linear-gradient(to bottom, #ffe5e6 0%,#fff8f8 100%);
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


        #MapSummary table thead tr th:nth-last-child(1) {
            width: 200px !important;
        }

        #MapSummary1 table thead tr th:nth-last-child(1) {
            width: 200px !important;
        }

        #MapSummary table tbody tr td:nth-last-child(1) {
            width: 200px !important
        }

        #MapSummary1 table tbody tr td:nth-last-child(1) {
            width: 200px !important
        }

        #MapSummary table thead tr th:nth-child(1) {
            width: 120px !important
        }

        #MapSummary1 table thead tr th:nth-child(1) {
            width: 120px !important
        }

        #MapSummary table tbody tr td:nth-child(1) {
            width: 120px !important
        }

        #MapSummary1 table tbody tr td:nth-child(1) {
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

            @media (min-width:991px) and (max-width:1134px) {
                #MapSummary1 table thead tr th:nth-last-child(1) {
                    width: 150px !important;
                }

                #MapSummary table tbody tr td:nth-last-child(1) {
                    width: 150px !important
                }

                #MapSummary1 table tbody tr td:nth-last-child(1) {
                    width: 150px !important
                }

                #MapSummary table thead tr th:nth-child(1) {
                    width: 100px !important
                }

                #MapSummary1 table thead tr th:nth-child(1) {
                    width: 100px !important
                }


                #MapSummary table tbody tr td:nth-child(1) {
                    width: 100px !important
                }

                #MapSummary1 table tbody tr td:nth-child(1) {
                    width: 100px !important
                }
            }
        }
        .leaflet-control-zoom {
    display: block !important;
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
    background-color: #d4f5d2 !important; /* light green */
    font-weight: bold;
}
                    .pagination > li > a,
        .pagination > li > span {
            position: relative;
            float: left;
            padding: 4px 12px;
            margin-left: -30px!important;
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
                                                                <label for="ddlBlock" class="col-sm-3  linhei" style="padding-top: 2px; font-weight: bold !important;">Block:</label>
                                                                <div class="col-sm-9 ">
                                                                    <asp:DropDownList ID="ddlBlock" runat="server"
                                                                        onchange="getAdminBlock();loadVillages(); bindDistrictVillages('', '');bindBlockVillage('', '');"
                                                                        class="form-control">
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
                                                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0" style="display:none;">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <input type="button" id="myButton" class="btn btn-danger btn-paddd" style="margin-left: -4rem;" />

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

                    <div class="col-sm-12 p-0">
                        <div class="row">
                            <div class="col-lg-5 col-md-5 col-sm-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading search-bg"><h5><b>MST Villages</b></h5></div>
                                    <div class="panel-body" style="padding-left: 0px; padding-right: 0px">
                                        <div class="row">
                                            <div class="col-sm-12" style="margin-bottom: 12px; padding-left: 0px; padding-right: 0px">
                                                <label class="col-sm-1 linhei" style="padding-top: 2px; font-weight: bold !important;">Status:</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlStatus" runat="server" onchange="loadAll();" class="form-control">
                                                        <asp:ListItem Text="Unmapped" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Mapped" Value="1"></asp:ListItem>

                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-12" style="padding-left: 0px; padding-right: 0px">
                                                <input id="txtSearchMIS" class="search" style="display: none;" placeholder="Search MIS villages..." />
                                                <%--<div id="misdiv"></div>--%>
                                                <div class="MapSummary-wrp">
                                                    <div id="MapSummary" class="">
                                                    </div>
                                                </div>
                                                <div class="margin-top: 10px;">
                                                    <button type="button" onclick="saveVillages()" class="btn btn-primary" style="margin-left: 25px;">
                                                        Save
                                                    </button>
                                                    <%--<span class="small">Click a MIS to get suggested Layer villages. Drag a MIS onto a Layer item to map.</span>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-7 col-md-7 col-sm-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading search-bg"><h5><b>GIS Villages: Suggested Matches</b></h5></div>
                                    <div class="panel-body">
                                        <div class="row" style="margin-left: -28px;">
                                            <div class="col-sm-12">
                                                <input id="txtSearchSuggest" class="search" style="display: none;" placeholder="Type to find suggestions (or click MIS/Layer)..." />
                                                <%--<div id="suggestList" class="list"></div>--%>
                                                <div class="MapSummary-wrp">
                                                    <div id="MapSummary1" class="">
                                                    </div>
                                                </div>


                                                <div style="margin-top: 10px;margin-left: 30px;">
                                                    <button id="btnSaveAll" type="button" class="btn btn-primary">Save</button>
                                                    <%--<button id="btnRefresh" type="button" class="btn1">Un-Map  Village</button>--%>
                                                </div>

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
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-12">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div id="map" style="height: 520px; margin-top: 12px; border: 1px solid #ddd; border-radius: 6px;"></div>
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
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="grid-2" style="display: none">
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
                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Export"
                                                            class=""></asp:LinkButton>
                                                        <button type="button" class="zoom_div" style="padding: 0px 0px 0px 12px; background-color: white; border: none;">
                                                            <i class="fa fa-expand fa-lg text-danger"></i>
                                                        </button>
                                                    </div>
                                                </div>
                                                <%-- <div class="MapSummary-wrp">
                                                    <div id="MapSummary" class="">
                                                    </div>
                                                </div>--%>
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



                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>





                <script type="text/javascript">

                    $(document).ready(function () {
                        $(".update_overlay").show();
                        bindMaster();
                        getAdminDistrict();
                        $(".update_overlay").hide();
                        initMap();
                        
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
                        //Fill_Cluster("ddlGP");
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
                        //Fill_Cluster("ddlGP");
                        call_function('', '');
                        Get_Details();
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
                        var c = $('#MapSummary').empty();

                        var table = $(`
    <table class="table table-hover table-bordered" id="tblLocDetails">
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
            data-vid="${v.VCode}"
            data-name="${v.VillageName}"
            data-lat="${v.lat}"
            data-lon="${v.lon}"
            data-admindistrictname="${v.AdminDistrictName}"
            data-mainblockname="${v.MainBlockName}">
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

                        $("#tblLocDetails").DataTable({
                            paging: true,
                            searching: true,
                            ordering: true,
                            pageLength: 10,     // default rows per page
                            lengthMenu: [5, 10, 20, 50, 100],
                            autoWidth: false
                        });
                        $(".update_overlay").hide();
                        bindClick(); // attach your click event
                    }

                    //function bindClick() {
                    //    if (currentVillagePolygon) {
                    //        map.removeLayer(currentVillagePolygon);
                    //    }

                    //    var fyear = $("[id$=ddlYear]").val();
                    //    var district = $("[id$=ddlDistrict] option:selected").text();
                    //    var block = $("[id$=ddlBlock] option:selected").text();




                    //    $('#MapSummary').off('click', '.mis-row').on('click', '.mis-row', function () {

                    //        $('#MapSummary .mis-row').removeClass('selected');
                    //        $(this).addClass('selected');

                    //        var misName = $(this).data('name');
                    //        var egVillageCode = $(this).data('id');
                    //        var lat = $(this).data('lat');
                    //        var lon = $(this).data('lon');
                    //        var vcode = $(this).data('vid');
                    //        var admindistrictname = $(this).data('admindistrictname');
                    //        var mainblockname = $(this).data('mainblockname');



                    //        sessionStorage.setItem('misName', misName);
                    //        var storedmisName = sessionStorage.getItem('misName');

                    //        //var egVillageCode = $(this).find('.id').val().trim();

                    //        if (egVillageCode === "") egVillageCode = null;

                    //        console.log("Clicked MIS Name:", misName);
                    //        console.log("EG Village Code:", egVillageCode);

                    //        sessionStorage.setItem('egVillageCode', egVillageCode);
                    //        var storedEgVillageCode = sessionStorage.getItem('egVillageCode');
                    //        console.log(storedEgVillageCode);

                    //        sessionStorage.setItem('lat', lat);
                    //        sessionStorage.setItem('lon', lon);
                    //        sessionStorage.setItem('vcode', vcode);
                    //        sessionStorage.setItem('admindistrictname', admindistrictname);
                    //        sessionStorage.setItem('mainblockname', mainblockname);
                    //        var storedlat = sessionStorage.getItem('lat');
                    //        var storedlon = sessionStorage.getItem('lon');
                    //        var storedlon = sessionStorage.getItem('vcode');
                    //        var admindistrictname = sessionStorage.getItem('admindistrictname');
                    //        var mainblockname = sessionStorage.getItem('mainblockname');
                    //        console.log("LAT:", storedlat);
                    //        console.log("LON:", storedlon);
                    //        console.log("vcode:", vcode);
                    //        console.log("AdminDistrictName:", admindistrictname);
                    //        console.log("MainBlockName:", mainblockname);
                    //        $(".update_overlay").show();
                    //        ajaxPost('GISMapping.aspx/GetMappingVillages',
                    //            {
                    //                misName: misName,
                    //                egCode: egVillageCode,
                    //                fyear: fyear,
                    //                district: district,
                    //                block: block
                    //            },
                    //            function (res) {

                    //                renderSuggest(res);
                    //                bindMappingSuggestions();
                    //                highlightSuggestedInLayer(res);

                    //            }
                    //        );

                    //        addVillagePolygon(vcode, misName);
                    //    });
                    //}

                    function bindClick() {

                        if (currentVillagePolygon) {
                            map.removeLayer(currentVillagePolygon);
                        }

                        var fyear = $("[id$=ddlYear]").val();
                        var district = $("[id$=ddlDistrict] option:selected").text();
                        var block = $("[id$=ddlBlock] option:selected").text();

                        // Prevent click bubbling from form controls inside rows
                        $('#MapSummary').off('click', 'input, textarea, select, button')
                            .on('click', 'input, textarea, select, button', function (e) {
                                e.stopPropagation();
                            });

                        $('#MapSummary').off('click', '.mis-row').on('click', '.mis-row', function (e) {

                            // 🛑 Ignore row click if clicking inside input controls
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
                            var storedmisName = sessionStorage.getItem('misName');

                            if (egVillageCode === "") egVillageCode = null;

                            console.log("Clicked MIS Name:", misName);
                            console.log("EG Village Code:", egVillageCode);

                            sessionStorage.setItem('egVillageCode', egVillageCode);
                            var storedEgVillageCode = sessionStorage.getItem('egVillageCode');
                            console.log(storedEgVillageCode);

                            sessionStorage.setItem('lat', lat);
                            sessionStorage.setItem('lon', lon);
                            sessionStorage.setItem('vcode', vcode);
                            sessionStorage.setItem('admindistrictname', admindistrictname);
                            sessionStorage.setItem('mainblockname', mainblockname);

                            var storedlat = sessionStorage.getItem('lat');
                            var storedlon = sessionStorage.getItem('lon');
                            var storedvcode = sessionStorage.getItem('vcode');
                            var admindistrictname = sessionStorage.getItem('admindistrictname');
                            var mainblockname = sessionStorage.getItem('mainblockname');

                            console.log("LAT:", storedlat);
                            console.log("LON:", storedlon);
                            console.log("vcode:", storedvcode);
                            console.log("AdminDistrictName:", admindistrictname);
                            console.log("MainBlockName:", mainblockname);

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
                                var adminDistrictName = sessionStorage.getItem('adminDistrictName');
                                console.log(adminDistrictName);

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
                                var adminBlockName = sessionStorage.getItem('adminBlockName');
                                console.log(adminBlockName);
                            }
                        );
                    }

                    function saveVillages() {
                        debugger;
                        var results = [];

                        $("#MapSummary tbody tr").each(function () {

                            var VillageCode = $(this).find(".gis-code").val().trim();

                            // ❌ Skip empty rows
                            if (VillageCode === "") {
                                return; // continue loop, skip this row
                            }

                            // Validate numeric only for filled rows
                            if (!/^\d+$/.test(VillageCode)) {
                                alert("EG Village Code must be numeric!");
                                $(this).find(".gis-code").focus();
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
                            url: "GISMapping.aspx/SaveVillages",
                            type: "POST",
                            data: JSON.stringify({ villages: results }),
                            contentType: "application/json; charset=utf-8",
                            success: function (r) {
                                alert("Saved!");
                                loadAll();
                            }
                        });

                    }

                    function renderSuggest(list) {

                        var container = $('#MapSummary1').empty();

                        if (!list || list.length === 0) {
                            $(".update_overlay").hide();
                            container.append('<div class="small" style="margin-left: 30px;">No suggestions</div>');
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
        <td>${Math.round(parseFloat(s.MatchScore))}%</td>
        <td>${s.DistanceKM}</td>
        <td>${s.EG_VillageCode}</td>
        
    </tr>
    `;
                        });

                        html += `</tbody></table>`;
                        container.append(html);
                        $(".update_overlay").hide();
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

                        ajaxPost("GISMapping.aspx/SaveVillageMappings",
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


                    function loadVillages() {
                        loadAll();
                    }
                    function loadAll() {
                        debugger;
                        $(".update_overlay").show();
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

                        ajaxPost('GISMapping.aspx/GetMISVillages', filters, function (res) {
                            MIS_CACHE = res || [];
                            renderMis(res);
                            //populateEditSelects();
                            //updateMapFromCaches();
                        });

                        //ajaxPost('GISMapping.aspx/GetLayerVillages', filters, function (res) {
                        //    LAYER_CACHE = res || [];
                        //    renderLayer(res);
                        //    //populateEditSelects();
                        //    //updateMapFromCaches();
                        //});

                        //ajaxPost('GISMapping.aspx/GetMappings', filters, function (res) {
                        //    renderMappings(res);
                        //});
                        
                    }

                </script>

                <script type="text/javascript">
                    var currentVillagePolygon = null; // global variable to track the polygon

                    function addVillagePolygon(villageCode, villageName) {
                        //map.removeLayer(currentVillagePolygon);
                        
                        $.ajax({
                            type: "POST",
                            url: "GISMapping.aspx/GetVillagePolygon",
                            data: JSON.stringify({ villageCode: villageCode }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                var points = response.d.map(p => [p.Lat, p.Lon]); // Leaflet needs [lat, lon]
                                if (!response.d || response.d.length < 3) {
                                    console.warn("Invalid polygon data", response.d);
                                    alert("Invalid polygon data");
                                    return;
                                }

                                // Optional: use convex hull if points are unordered (requires turf.js)
                                // var turfPoints = response.d.map(p => turf.point([p.Lon, p.Lat]));
                                // var hull = turf.convex(turf.featureCollection(turfPoints));
                                // L.geoJSON(hull, {color:'red', fillOpacity:0.4}).addTo(map);

                                // Simple polygon
                                //var polygon = L.polygon(points, {
                                //    color: 'blue',
                                //    fillColor: '#03b5fc',
                                //    fillOpacity: 0.4
                                //}).addTo(map);

                                //map.fitBounds(polygon.getBounds());

                                // Remove existing polygon
                                if (currentVillagePolygon) {
                                    map.removeLayer(currentVillagePolygon);
                                }

                                // Create new polygon
                                currentVillagePolygon = L.polygon(points, {
                                    color: 'blue',
                                    fillColor: '#03b5fc',
                                    fillOpacity: 0.4
                                }).addTo(map);

                                // Bind popup (click) and tooltip (hover)
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

                    // Example: load polygon for a specific village
                    //addVillagePolygon("0000A35D6B954F85BE4E3A461");
                </script>


                <script type="text/javascript">

                    var map;
                    var StateMap = L.layerGroup();
                    var District_Map = L.layerGroup();
                    var BlockMap = L.layerGroup();
                    var VillageMap = L.layerGroup();
                    var DistrictVillageLayer = L.layerGroup();
                    var BlockVillageLayer = L.layerGroup();
                    var MappingSuggestionLayer = null;
                    var currentMarker = null;
                    function initMap() {
                        if (map) {
                            map.remove();
                        }
                        state = $("[id$=ddlState]").val();
                        // default view - will be fitted later to district / markers
                        //map = L.map('map', { preferCanvas: true }).setView([25.3903, 80.8913], 4.5);
                        if (state == "9" || state == "9A" || state == "9B" || state == "9C") {
                            map = L.map('map', { maxZoom: 18, minZoom: 4, dragging: true, fullscreenControl: { pseudoFullscreen: false } }).setView(new L.LatLng(25.3903, 80.8913), 4.5);
                        }
                        if (state == "23") {
                            map = L.map('map', { maxZoom: 18, minZoom: 4, dragging: true, fullscreenControl: { pseudoFullscreen: false } }).setView(new L.LatLng(23.065833940118736, 74.62120056152345), 4.5);
                        }

                        StreetLyr = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
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
                            };

                            if (!window.layerControl) {
                                window.layerControl = L.control.layers(null, overlayMaps).addTo(map);
                            }
                        }

                        initializeBaseLayers();
                    }

                    function bindDistrictVillages(flag, locationid) {

                        var districtName = $("[id$=ddlDistrict] option:selected").text();

                        //var district = sessionStorage.getItem('adminDistrictName');

                        var did = $("[id$=ddlDistrict]").val().split("#");
                        var district = did[0];

                        //var url = "https://geo1server.educategirls.ngo/geoserver/EGTest/ows" +
                        //    "?service=WFS&version=1.0.0&request=GetFeature" +
                        //    "&typeName=EGTest:lyr_layer_Villages" +
                        //    "&maxFeatures=5000&outputFormat=application/json" +
                        //    "&viewparams=DistrictName:" + district;

                        // remove only district layer
                        if (DistrictVillageLayer) {
                            map.removeLayer(DistrictVillageLayer);
                        }

                        $.ajax({
                            type: "POST",
                            url: "GISMapping.aspx/GetDistrictVillages",
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
                            },
                            error: function (xhr) {
                                console.error("Village load failed", xhr.responseText);
                            }
                        });


                        function vlgstyle(feature) { return { weight: 2, opacity: 1, color: 'black', dashArray: '3', fillOpacity: 0.7, fillColor: CircleColors(feature.properties.mapped) }; }
                        function onEachFeatureVillage(feature, layer) {
                            layer.bindTooltip("<b style='color: #2954A2;font-size: 12px;'>EG VillageCode: " + feature.properties.EG_VillageCode + "<br/>Layer District: " + feature.properties.DistrictName + "<br/>Admin District: " + feature.properties.AdminDistrictName + "<br/>Layer Block: " + feature.properties.BlockName + "<br/>Admin Block: " + feature.properties.AdminBlockName + "<br/>Layer Village: " + feature.properties.VillageName + "<br/>Village ID: " + feature.properties.VillageID + "</b>", { permanent: false, sticky: true, offset: [10, 0], opacity: 3, }); layer.on({ mouseover: highlightFeatureCluster, mouseout: resetHighlightCluster, preclick: resetStyleCluster, click: zoomToFeatureCluster });
                        }
                        function CircleColors(e) { return (e === 1 ? '#D9E9CF' : e === 0 ? '#D3D3D3' : e = null ? '#D3D3D3' : '#D3D3D3') }
                        function highlightFeatureCluster(e) {
                            var layer = e.target; layer.setStyle({
                                weight: 2, color: '#666', dashArray: '', opacity: 1, fillOpacity: 0.4
                            });
                        } function resetHighlightCluster(e) { DistrictVillageLayer.resetStyle(e.target); } function resetStyleCluster(e) { DistrictVillageLayer.resetStyle(e.target); } function zoomToFeatureCluster(e) { map.fitBounds(e.target.getBounds()); }


                    }


                    function bindBlockVillage(flag, locationid) {

                        //var district = $("[id$=ddlDistrict] option:selected").text();
                        //var block = $("[id$=ddlBlock] option:selected").text();

                        var district = sessionStorage.getItem('adminDistrictName');
                        //var block = sessionStorage.getItem('adminBlockName');
                        var bid = $("[id$=ddlBlock]").val().split("#");
                        var block = bid[0];
                        // remove only block layer
                        if (BlockVillageLayer) {
                            map.removeLayer(BlockVillageLayer);
                        }

                        $.ajax({
                            type: "POST",
                            url: "GISMapping.aspx/GetBlockVillages",
                            data: JSON.stringify({
                                district: district,
                                block: block
                            }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (res) {

                                var data = JSON.parse(res.d);

                                BlockVillageLayer = L.geoJson(data, {
                                    style: vlgstyle,
                                    onEachFeature: onEachFeatureVillage
                                }).addTo(map);

                                addLayerControl();
                            },
                            error: function (xhr) {
                                console.error("Block villages load failed", xhr.responseText);
                            }
                        });
                        function vlgstyle(feature) { return { weight: 2, opacity: 1, color: 'black', dashArray: '3', fillOpacity: 0.7, fillColor: CircleColors(feature.properties.mapped) }; }
                        function onEachFeatureVillage(feature, layer) {
                            layer.bindTooltip("<b style='color: #2954A2;font-size: 12px;'>EG VillageCode: " + feature.properties.EG_VillageCode + "<br/>Layer District: " + feature.properties.DistrictName + "<br/>Admin District: " + feature.properties.AdminDistrictName + "<br/>Layer Block: " + feature.properties.BlockName + "<br/>Admin Block: " + feature.properties.AdminBlockName + "<br/>Layer Village: " + feature.properties.VillageName + "<br/>Village ID: " + feature.properties.VillageID + "</b>", { permanent: false, sticky: true, offset: [10, 0], opacity: 3, }); layer.on({ mouseover: highlightFeatureCluster, mouseout: resetHighlightCluster, preclick: resetStyleCluster, click: zoomToFeatureCluster });
                        }
                        function CircleColors(e) { return (e === 1 ? '#D9E9CF' : e === 0 ? '#D3D3D3' : e = null ? '#D3D3D3' : '#D3D3D3') }
                        function highlightFeatureCluster(e) {
                            var layer = e.target; layer.setStyle({
                                weight: 2, color: '#666', dashArray: '', opacity: 1, fillOpacity: 0.4
                            });
                        } function resetHighlightCluster(e) { BlockVillageLayer.resetStyle(e.target); } function resetStyleCluster(e) { BlockVillageLayer.resetStyle(e.target); } function zoomToFeatureCluster(e) { map.fitBounds(e.target.getBounds()); }
                    }




                    function bindMappingSuggestions() {
                        /*$(".update_overlay").show();*/
                        var fyear = $("[id$=ddlYear]").val();
                        var district = $("[id$=ddlDistrict] option:selected").text();
                        var block = $("[id$=ddlBlock] option:selected").text();

                        var storedEgVillageCode = sessionStorage.getItem('egVillageCode');
                        var storedmisName = sessionStorage.getItem('misName');

                        var admindistrictname = sessionStorage.getItem('admindistrictname');
                        var mainblockname = sessionStorage.getItem('mainblockname');

                        // remove only suggestions layer
                        if (MappingSuggestionLayer) {
                            map.removeLayer(MappingSuggestionLayer);
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
                                }).addTo(map);

                                addMatchScoreLegend();
                                updateLayerControl();
                                /* $(".update_overlay").hide(); */
                            },
                            error: function (xhr) {
                                console.error("Village suggestion load failed", xhr.responseText);
                            }
                        });

                        // Marker
                        //addMarker(storedlat, storedlong, storedmisName);

                        //function addMarker(lat, lng, name) {
                        //    if (currentMarker) {
                        //        currentMarker.remove();
                        //    }
                        //    currentMarker = L.marker([lat, lng])
                        //        .addTo(map)
                        //        .bindPopup(name)
                        //        .openPopup();
                        //}

                        function vlgstyle(feature) {
                            return {
                                weight: 2,
                                opacity: 1,
                                color: 'black',
                                //dashArray: '3',
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

                        //function CircleColors(e) {
                        //    return (e >= 100 ? '#008000' : e >= 80 && e <= 90 ? '#0000FF' : e >= 70 && e < 80 ? '#FFFF00' : e >= 50 && e < 70 ? '#FFA500' : e < 50 ? '#FF0000' : e = null ? '#FFFFFF' : '"#FFFFFF"')

                        //}
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
                                //fillColor: '',

                            });
                        }
                        function zoomToFeatureCluster(e) {
                            map.fitBounds(e.target.getBounds());
                        }
                    }



                    function addLayerToControl11() {
                        const overlayMaps = {
                            "Gray": GrayLyr,
                            "Street": StreetLyr,
                            "Terrain": Terrain,
                            "Satellite": ImageryLyr,
                            "Village": VillageMap,
                            "Matches": BlockMap
                        };

                        if (!window.layerControl) {
                            window.layerControl = L.control.layers(null, overlayMaps).addTo(map);
                        }
                    }

                    function addLayerControl() {

                        var overlays = {
                            "District Villages": DistrictVillageLayer,
                            "Block Villages": BlockVillageLayer
                        };

                        if (!window.layerControl) {
                            window.layerControl = L.control.layers(
                                {
                                    "Gray": GrayLyr,
                                    "Street": StreetLyr,
                                    "Terrain": Terrain,
                                    "Satellite": ImageryLyr
                                },
                                overlays
                            ).addTo(map);
                        } else {
                            window.layerControl.remove();
                            window.layerControl = L.control.layers(
                                {
                                    "Gray": GrayLyr,
                                    "Street": StreetLyr,
                                    "Terrain": Terrain,
                                    "Satellite": ImageryLyr
                                },
                                overlays
                            ).addTo(map);
                        }
                    }

                    function updateLayerControl() {

                        var overlays = {};

                        if (DistrictVillageLayer) {
                            overlays["District Villages"] = DistrictVillageLayer;
                        }

                        if (BlockVillageLayer) {
                            overlays["Block Villages"] = BlockVillageLayer;
                        }

                        if (MappingSuggestionLayer) {
                            overlays["Mapping Suggestions"] = MappingSuggestionLayer;
                        }

                        if (currentVillagePolygon) {
                            overlays["Selected Village"] = currentVillagePolygon;
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
                        ).addTo(map);
                    }

                    function addMatchScoreLegend() {

                        if (window.matchScoreLegend) {
                            map.removeControl(window.matchScoreLegend);
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

                        window.matchScoreLegend.addTo(map);
                    }


                    //function updateLayerControl() {

                    //    var overlays = {
                    //        "District Villages": DistrictVillageLayer,
                    //        "Block Villages": BlockVillageLayer,
                    //        "Mapping Suggestions": MappingSuggestionLayer,
                    //        "Selected Village": currentVillagePolygon
                    //    };

                    //    if (window.layerControl) {
                    //        window.layerControl.remove();
                    //    }

                    //    window.layerControl = L.control.layers(
                    //        {
                    //            "Gray": GrayLyr,
                    //            "Street": StreetLyr,
                    //            "Terrain": Terrain,
                    //            "Satellite": ImageryLyr
                    //        },
                    //        overlays,
                    //        { collapsed: false }
                    //    ).addTo(map);
                    //}

                </script>
        </ContentTemplate>
        <Triggers>

            <asp:PostBackTrigger ControlID="LinkButton1" />

        </Triggers>
    </asp:UpdatePanel>




</asp:Content>

