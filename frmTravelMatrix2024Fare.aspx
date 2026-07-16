<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmTravelMatrix2024Fare.aspx.cs" Inherits="frmTravelMatrix2024Fare" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>

      <style>
        .btnStyle {
            border: 1px solid #ccc;
            margin-bottom: 7px;
            margin-right: 16px;
        }
      .GridHeader
{
    text-align:center !important;    
}
        .float-r {
            float: right;
        }

        .WrapText {
            width: 100%;
            word-break: break-all;
        }
        /* .modalBg {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }*/
        .modal {
            position: fixed;
            top: 80px;
            right: 0;
            bottom: 0;
            left: 0;
            z-index: 9999;
            width: 62%;
            display: none;
            overflow: hidden;
            -webkit-overflow-scrolling: touch;
            outline: 0;
        }
        /* .modalBackground {
            background-color: rgba(0,0,0,0.5);
        }

        .mod-posi {
            position: fixed !important;
            top: 5% !important;
        }

        .Mpopup1 {
            position: relative;
            background: #f2f2f2;
            color: #404040;
            text-shadow: 0 1px 0 #fff;
            -ms-filter: "progid:DXImageTransform.Microsoft.dropshadow(OffX=0,OffY=1,Color=#ffffffff,Positive=true)";
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=0, OffY=1, Color=#ffffffff, Positive=true);
            border-radius: 5px;
            box-shadow: 0px 1px 4px rgba(0, 0, 0, 0.1);
            padding: 5px;
            font-size: 12px;
            height: 490px !important;
            z-index: 1350px0001 !important;
        }

        .modal-body {
            background-color: #fff;
            position: relative;
            padding: 15px;
        }*/

        .primaryKK {
            margin-right: 2px;
        }
        /*
        .Mpopupnewline {
            border-top: 2px solid #105f77;
            width: 100%;
            height: 4px;
        }

        .Mpopupheader {
            width: 100%;
            background-color: #454545;
            height: 25px;
            font-size: 12px;
            font-weight: 500;
            color: #f2f2f2;
            text-shadow: 0 1px 0 #add553;
            -ms-filter: "progid:DXImageTransform.Microsoft.dropshadow(OffX=0,OffY=1,Color=#ffffffff,Positive=true)";
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=0, OffY=1, Color=#ffffffff, Positive=true);
            padding: 5px;
        }

        .Mpopupbodycontent {
            width: 100%;
            margin: 3px 0 3px 0
        }*/

        /*.Mpopupfooter {
            width: 100%;
            background-color: #454545;
            padding: 3px
        }*/

        .Requiredvalidate {
            font-size: 12px;
            color: Red;
        }


        /*.ModalPopupBG {
            background-color: #000000;
            filter: alpha(opacity=80);
            -moz-opacity: 0.5;
            -khtml-opacity: 0.5;
            opacity: 0.5;
            width: 100%;
            height: 100%
        }

        .ModalPopupBGmainentry {
            background-color: #000000;
            filter: alpha(opacity=10);
            -moz-opacity: 1.0;
            -khtml-opacity: 1.0;
            opacity: 1.0;
            width: 100%;
            height: 100%
        }*/

        .Training-details-row {
            margin-left: -15px;
            margin-right: -15px;
            margin-top: 10px;
            margin-bottom: 10px;
        }


            .Training-details-row label {
                line-height: initial;
            }

            /*.modal-header {
            padding: 15px;
            border-bottom: 1px solid #0000000d;
        }*/

            /*.modal-body * {
            font-size: 16px;
        }*/

            .Training-details-row .form-group {
                margin-bottom: 12px;
            }

        /*  .Mpopup1 {
            top: 50% !important;
            transform: translateY(-50%) !important;
        }*/

        .part-1 {
            float: left;
            width: calc(50% - 25px);
            min-height: 150px;
            border: 1px solid #ddd;
            border-radius: 6px;
            box-shadow: 0px 0px 4px 0px #545454;
        }

        .part-butt {
            float: left;
            width: 50px;
            min-height: 150px;
            text-align: center;
            position: relative;
            top: 14rem;
        }
    </style>
    <style>
        .page-break {
            page-break-after: always;
        }

        .search-bg {
            background: linear-gradient(to bottom, #ebf1fd 0%, #ffffff 100%) !important;
            padding-top: 12px;
            padding-bottom: 0px;
        }

        .row {
            margin-right: -15px;
            margin-left: -15px;
        }

        .container-fluid {
            padding-right: 15px;
            padding-left: 15px;
            margin-right: auto;
            margin-left: auto;
        }

        .padd {
            padding-left: 0px;
            padding-right: 0px;
        }

        .form-group {
            margin-bottom: 15px;
            float: left;
            width: 100%;
        }



        /* width */
        .da-tble::-webkit-scrollbar, .WrapText-tble::-webkit-scrollbar {
            width: 7px;
        }

        /* Track */
        .da-tble::-webkit-scrollbar-track, .WrapText-tble::-webkit-scrollbar-track {
            background: #f1f1f1;
        }

        /* Handle */
        .da-tble::-webkit-scrollbar-thumb, .WrapText-tble::-webkit-scrollbar-thumb {
            background: #d9d9d9;
        }

            /* Handle on hover */
            .da-tble::-webkit-scrollbar-thumb:hover, .WrapText-tble::-webkit-scrollbar-thumb:hove {
                background: #555;
            }


        @media (min-width: 1200px) {
            .tada-left {
                width: 28%;
            }

            .tada-right {
                width: 72%;
            }
        }
    </style>
    <style>
        .float-r {
            float: right;
        }

        .modalBg {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }

        .modalBackground {
            background-color: rgba(0,0,0,0.5);
        }

        .mod-posi {
            position: fixed !important;
            top: 5% !important;
        }

        .Mpopup1 {
            position: relative;
            background: #f2f2f2;
            color: #404040;
            text-shadow: 0 1px 0 #fff;
            -ms-filter: "progid:DXImageTransform.Microsoft.dropshadow(OffX=0,OffY=1,Color=#ffffffff,Positive=true)";
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=0, OffY=1, Color=#ffffffff, Positive=true);
            border-radius: 5px;
            box-shadow: 0px 1px 4px rgba(0, 0, 0, 0.1);
            padding: 5px;
            font-size: 12px;
            height: 490px !important;
            z-index: 1350px0001 !important;
        }

        .modal-body {
            background-color: #fff;
            position: relative;
            padding: 15px;
        }

        .primaryKK {
            margin-right: 2px;
        }

        .Mpopupnewline {
            border-top: 2px solid #105f77;
            width: 100%;
            height: 4px;
        }

        .Mpopupheader {
            width: 100%;
            background-color: #454545;
            height: 25px;
            font-size: 12px;
            font-weight: 500;
            color: #f2f2f2;
            text-shadow: 0 1px 0 #add553;
            -ms-filter: "progid:DXImageTransform.Microsoft.dropshadow(OffX=0,OffY=1,Color=#ffffffff,Positive=true)";
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=0, OffY=1, Color=#ffffffff, Positive=true);
            padding: 5px;
        }

        .Mpopupbodycontent {
            width: 100%;
            margin: 3px 0 3px 0
        }

        .Mpopupfooter {
            width: 100%;
            background-color: #454545;
            padding: 3px
        }

        .Requiredvalidate {
            font-size: 12px;
            color: Red;
        }


        .ModalPopupBG {
            background-color: #000000;
            filter: alpha(opacity=80);
            -moz-opacity: 0.5;
            -khtml-opacity: 0.5;
            opacity: 0.5;
            width: 100%;
            height: 100%
        }

        .ModalPopupBGmainentry {
            background-color: #000000;
            filter: alpha(opacity=10);
            -moz-opacity: 1.0;
            -khtml-opacity: 1.0;
            opacity: 1.0;
            width: 100%;
            height: 100%
        }

        .Training-details-row {
            margin-left: -15px;
            margin-right: -15px;
            margin-top: 10px;
            margin-bottom: 10px;
        }


            .Training-details-row label {
                line-height: initial;
            }

        .modal-header {
            padding: 15px;
            border-bottom: 1px solid #0000000d;
        }

        /*.modal-body * {
            font-size: 16px;
        }*/

        .Training-details-row .form-group {
            margin-bottom: 12px;
        }

        .Mpopup1 {
            top: 50% !important;
            transform: translateY(-50%) !important;
        }

        .part-1 {
            float: left;
            width: calc(50% - 25px);
            min-height: 150px;
            border: 1px solid #ddd;
            border-radius: 6px;
            box-shadow: 0px 0px 4px 0px #545454;
        }

        .part-butt {
            float: left;
            width: 50px;
            min-height: 150px;
            text-align: center;
            position: relative;
            top: 14rem;
        }
    </style>
    <style>
        /* Main GridView Style */
        .gridview-style {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
            font-size: 16px;
            text-align: left;
        }

        /* Header Row Style */
        .gridview-header {
            background-color: #4CAF50;
            color: white;
            font-weight: bold;
            text-align: center;
        }

        /* Normal Rows Style */
        .gridview-row {
            background-color: #f9f9f9;
        }

        /* Alternating Rows Style */
        .gridview-alt-row {
            background-color: #eaf2f8;
        }

            /* Hover Effect */
            .gridview-row:hover, .gridview-alt-row:hover {
                background-color: #d4edda;
                cursor: pointer;
            }


        .textbox {
            padding: 10px;
            border: 2px solid #ccc;
            border-radius: 4px;
            font-size: 16px;
            transition: box-shadow 0.3s ease; /* Smooth transition for shadow */
        }

            .textbox:focus {
                outline: none; /* Removes default outline */
                box-shadow: 0 0 8px rgba(0, 123, 255, 0.6); /* Blue shadow on focus */
                border-color: #007bff; /* Optional border color change */
            }
    </style>

    <style>
        .search-bg {
            background: linear-gradient(to bottom, #ebf1fd 0%, #ffffff 100%) !important;
            padding-top: 12px;
            padding-bottom: 0px;
        }

        .row {
            margin-right: -15px;
            margin-left: -15px;
        }

        .container-fluid {
            padding-right: 15px;
            padding-left: 15px;
            margin-right: auto;
            margin-left: auto;
        }

        .padd {
            padding-left: 0px;
            padding-right: 0px;
        }

        .form-group {
            margin-bottom: 15px;
            float: left;
            width: 100%;
        }
    </style>

    <script type="text/javascript">


        function validateInput(event) {

            var input = event.target;
            var value = input.value;
            if (isNaN(value) || value === "") {
                input.value = "";
                return;
            }
            var value = parseInt(input.value, 10);

            if (value < 1) {
                input.value = 1;
            } else if (value > 100) {
                input.value = 100;
            }

            //document.addEventListener("input", function (e) {
               
            //    if (e.target.tagName !== "INPUT") return;

            //    let td = e.target.closest("td");
            //    let tr = td.parentElement;

            //    let table = document.getElementById("tblrpt");

            //    // Row index
            //    let rowIndex = tr.rowIndex - 1; // minus header

            //    // Column index
            //    let colIndex = td.cellIndex;

            //    // Find reverse row (B)
            //    let reverseRow = table.rows[colIndex + 1]; // +1 for header

            //    if (!reverseRow) return;

            //    // Find reverse cell (A)
            //    let reverseCell = reverseRow.cells[rowIndex];

            //    if (!reverseCell) return;

            //    let reverseInput = reverseCell.querySelector("input");

            //    if (reverseInput) {
            //        reverseInput.value = e.target.value;
            //    }

            //});
        }



        function UpdaterptTravelFare() {
            var TableData1 = [];
            var headers = [];
            $("#tblrpt").find("thead th").each(function () {
                var headerText = $(this).text().trim();
                headers.push(headerText);
            });


            $("#tblrpt").find("tbody tr").each(function () {
                var row = $(this);
                var obj = {};


                row.find("td").each(function (colIndex) {
                    var cell = $(this);
                    var inputValue = cell.find("input").val();
                    var textValue = cell.text().trim();


                    var headerName = headers[colIndex];
                    obj[headerName] = inputValue || textValue;
                });


                if (Object.keys(obj).length > 0) {
                    TableData1.push(obj);
                }
            });
            var clustercode = $("[id$=ddlCluster]").val();


            if (TableData1.length > 0) {
                var TableDataGuest = JSON.stringify(TableData1);

                $.ajax({
                    url: 'frmTravelMatrix2024Fare.aspx/LoadDataTravelFare',
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ JSONDATA: TableDataGuest, clustercode }),
                    success: function (result) {
                        var STATUS = result.d;
                        if (STATUS === "OK") {
                            alert("Data Updated Successfully");
                           <%-- var ff = document.getElementById('<%=rptTravelfare.ClientID %>');
                            document.getElementById('<%=rptTravelfare.ClientID %>').value = '';--%>
                            <%--document.getElementById('<%=rptTravelfare.ClientID %>').hide();
                            alert(ff);
                           
                            $("#ctl00_MainContent_rptTravelfare").hide();--%>
                            //$("#lbl-css").hide();
                            //$("#tblrpt tr").remove();


                            getrptTravelFareDetails();


                        } else {
                            alert("Data not updated");
                        }
                    },
                    error: function (xhr, status, error) {
                        console.error("AJAX Error:", xhr.responseText);
                        alert("Error: " + xhr.responseText);
                    }
                });
            } else {
                alert("No data to update");
            }
        }




    </script>
    <script language="javascript">
        function getrptTravelFareDetails() {

            var clustercode = $("[id$=ddlCluster]").val();
            var Temp = 0;

            if (clustercode == null) {
                alert("Please select Cluster");

            }
            else {
                if (clustercode.length > 20) {

                    Temp = 1;
                }
                else {
                    Temp = 0;
                    alert("Please select Cluster");
                }
            }
            if (Temp == 1) {
                $.ajax({
                    url: 'frmTravelMatrix2024Fare.aspx/LoadData',
                    type: "POST",
                    dataType: "json",
                    data: JSON.stringify({ clustercode: clustercode }),
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    success: function (result) {
                      

                        var scrpt = result.d.split('___');
                        var data = scrpt[0];
                        if (data != null) {
                            $("#ctl00_MainContent_rptTravelfare").html(data);
                          
                            var Total = scrpt[1];
                          //  alert(Total);
                            if (parseInt(Total) > 0 && parseInt(Total) <=12) {
                                document.getElementById('ctl00_MainContent_divkk').setAttribute("style", "width:150% !important;height: 390px !important");
                              //  alert('A');
                            }
                            else if (parseInt(Total) > 12 && parseInt(Total) <= 20) {
                                document.getElementById('ctl00_MainContent_divkk').setAttribute("style", "width:200% !important;height: 390px !important");
                                //alert('B');
                            }
                            else if (parseInt(Total) > 20 && parseInt(Total) <= 35) {
                             
                               // alert('C');
                                document.getElementById('ctl00_MainContent_divkk').setAttribute("style", "width:310% !important;height: 390px !important");
                            }
                            else if (parseInt(Total) > 35 && parseInt(Total) <= 50) {
                               
                                document.getElementById('ctl00_MainContent_divkk').setAttribute("style", "width:400% !important;height: 390px !important");

                            } else if (parseInt(Total) > 50 && parseInt(Total) <= 80) {

                                document.getElementById('ctl00_MainContent_divkk').setAttribute("style", "width:800% !important;height: 390px !important");

                            }
                            else if (parseInt(Total) > 80 && parseInt(Total) < 100) {

                                document.getElementById('ctl00_MainContent_divkk').setAttribute("style", "width:900% !important;height: 390px !important");

                            }

                        }
                    },

                    error: function (xhr, status, error) {
                        alert("Error: " + xhr.responseText);
                    }
                })
            }
        }
    </script>
    <%--<script>
         $(document).ready(function () {
             $('.textbox').on('input', function () {
                 let input = $(this).val();
                 debugger;
                 if (input === '' || (Number(input) >= 1 && Number(input) <= 100)) {
                     $(this).data('valid', true); 
                 } else {
                     $(this).data('valid', false); 
                 }
             });

             $('.textbox').on('blur', function () {
                 debugger
                 if (!$(this).data('valid')) {
                     $(this).val('');
                   
                 }
             });
         });
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="page_heading_dg" style="text-align:center">
                               <asp:Label ID="Label1" runat="server" class="text-danger"  Text="Travel Matrix Fare"></asp:Label> </h3>
                       
                        </div>
                    </div>
                </div>

          
                <div class="row">
                    <div class="col-sm-12" style="margin-bottom: 8px">
                        <div class="panel panel-default" style="border: 0px;">
                            <div class="panel-body search-bg">
                                <div class="row">
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">Year:</label>
                                            <div class="col-sm-9 padd">

                                                <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                    class="form-control ">
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">State:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                    AutoPostBack="true" class="form-control ">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">District:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                    AutoPostBack="true" class="form-control " />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">Block:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                    class="form-control " />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">Cluster:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlCluster" AutoPostBack="true" OnSelectedIndexChanged="ddlCluster_SelectedIndexChanged" runat="server"
                                                    class="form-control " />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server" visible="false">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">FC:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlFC" runat="server"
                                                    class="form-control " />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server" visible="false">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">Month:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">Jan </asp:ListItem>
                                                    <asp:ListItem Value="2">Feb </asp:ListItem>
                                                    <asp:ListItem Value="3">Mar</asp:ListItem>
                                                    <asp:ListItem Value="4">Apr</asp:ListItem>
                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                    <asp:ListItem Value="6">Jun</asp:ListItem>
                                                    <asp:ListItem Value="7">Jul</asp:ListItem>
                                                    <asp:ListItem Value="8">Aug</asp:ListItem>
                                                    <asp:ListItem Value="9">Sep</asp:ListItem>
                                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;"></label>
                                            <div class="col-sm-9 padd">

                                                <button id="Button1"  class="btn btn-primary"   style="font-size:14px;"  type="button" onclick="getrptTravelFareDetails();">Search</button>


                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-sm-6 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;"></label>
                                            <div class="col-sm-9 padd">
                                                <button class="btn btn-success" style="margin-left: 262px;font-size:14px;" onclick="UpdaterptTravelFare()">
                                                    Save</button>

                                                <asp:Button ID="btnDownload"  class="btn btn-info " Text="Download" runat="server" OnClick="btnDown_Click"></asp:Button>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel panel-default" style="border: 0px;">
                            <div class="panel-heading">
                                <div style="text-align: right;">
                                </div>
                            </div>
                            <div class="panel-body search-bg">
                                <div class="row">

                                    <style>
                                        .Row.WrapText.lbl-css tr td:nth-child(4), .Row.WrapText.lbl-css tr th:nth-child(4) {
                                            width: 120px !important;
                                        }

                                         .Row.WrapText.lbl-css tr td:nth-child(3), .Row.WrapText.lbl-css tr th:nth-child(3) {
                                            width: 120px !important;
                                        }
                                       
                                         .Row.WrapText.lbl-css table 
                                         {
                                            width: 300% !important;
                                        }

                                       
                                       
                                    </style>


                                    <div class="col-lg-12 col-md-12 col-sm-12 cpl-xs-12">
                                        <div class="panel panel-default">
                                             <div class="Row WrapText-tble   table-responsive" id="tblMain">

                                            <div  class ="Row WrapText-tble   table-responsive" id="divkk" runat="server" style="height: 390px; overflow: auto; width: 100%;" align="center">
                                                <div runat="server" id="rptTravelfare" class="Row da-tble table-responsive"></div>
                                                <asp:Label ID="lblFromNoEdit" Visible="false" ForeColor="Black" runat="server"></asp:Label>
                                                <asp:Label ID="lblUserIDEdit" Visible="false" ForeColor="Black" runat="server"></asp:Label>
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
            <asp:ModalPopupExtender ID="MPE_Entry" BackgroundCssClass="modalBackground"
                runat="server" PopupControlID="Pnl_Entry" TargetControlID="HdnEntry" CancelControlID="lnkEntryClose">
            </asp:ModalPopupExtender>
            <asp:HiddenField ID="HdnEntry" runat="server" />

            <asp:Panel ID="Pnl_Entry" runat="server" CssClass=" model-wid Mpopup1 mod-posi" Style="height: 310px  !important; position: fixed !important; width: 40% !important; display: none;">

                <div style="border: 0px solid #ccc; width: 100%; min-height: 100px; margin: 0 auto;">
                    <div class="modal-header">
                        <h3 class="text-danger" style="margin: 0;">Reason 
                                            
                            <asp:LinkButton ID="lnkEntryClose" class="btn btn-xs btn-danger pull-right"
                                runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>
                        </h3>

                    </div>
                    <div class="modal-body">
                        <div style="height: auto;">
                            <div class="form-group">
                                <div class="row" runat="server" id="Div1">
                                    <div class="form-group">
                                        <label class="control-label" style="margin-top: 10px; text-align: left;">
                                            Reason   : <span style="color: Red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtResone" runat="server" TextMode="MultiLine" TabIndex="4" MaxLength="150" CssClass="form-control input-sm" Style="margin-top: 5px; height: 80px !important;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtResone" Display="Dynamic" ErrorMessage="Please enter Reason" ForeColor="Red" SetFocusOnError="True" ValidationGroup="QuestionCreate1">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                </div>
                                <div class="row" runat="server" id="Div4" style="margin-bottom: 15px;">
                                    <div class="form-group">

                                        <div class="col-sm-12">
                                            <asp:LinkButton ID="BtnEntry" OnClick="BtnDelete_Click" ValidationGroup="QuestionCreate1" class="btn btn-xs btn-primary pull-right"
                                                ToolTip="Save" Width="55px"
                                                Style="margin-top: -4px; width: 70px; height: 26px;" runat="server">Save</asp:LinkButton>


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
        </ContentTemplate>

        <Triggers>


            <asp:PostBackTrigger ControlID="btnDownload" />



        </Triggers>

    </asp:UpdatePanel>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">

        $(document).on("keyup", "#tblrpt .textbox", function () {

            console.log("Typing detected ✅");

            let currentTd = $(this).closest("td");
            let currentTr = $(this).closest("tr");

            let rowIndex = currentTr.index();
            let colIndex = currentTd.index();

            let value = $(this).val();

            // Skip first 4 columns
            if (colIndex < 4) return;

            let reverseRowIndex = colIndex - 4;
            let reverseColIndex = rowIndex + 4;

            let reverseRow = $("#tblrpt tbody tr").eq(reverseRowIndex);
            let reverseTd = reverseRow.find("td").eq(reverseColIndex);
            let reverseInput = reverseTd.find("input");

            if (reverseInput.length) {
                reverseInput.val(value);
            }

        });

    </script>
</asp:Content>

