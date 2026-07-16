<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" Culture="en-GB" AutoEventWireup="true" CodeFile="SurveyTrainingProcess.aspx.cs" Inherits="SurveyTrainingProcess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="js/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
    <link rel="stylesheet" type="text/css" href="css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="css/eg_styles.css">

    <script src="online-js/jquery-ui.min.js" type="text/javascript"></script>
    <link href="online-js/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="online-js/jquery-ui-timepicker-addon.js" type="text/javascript"></script>
    <style>
                
.radioButtonList
{
}
 
.radioButtonList input[type="radio"]
{
	width: 20px;
    padding: 0;
     
}
	
.radioButtonList label
{
	margin-right: 25px;    
    white-space: nowrap;
}
 
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

    <%--  <script type="text/javascript">
        $(document).ready(function () {
            $('#ddlCars').multiselect();
            $('#ddlCars1').multiselect({
                numberDisplayed: 2

            });
            $('#ddlCars2').multiselect({
                includeSelectAllOption: true,
                enableFiltering: true

            });
            $('#ddlCars3').multiselect({
                nonSelectedText: 'Select Cars'

            });
        });
    </script>--%>
    <script language="Javascript" type="text/javascript">

        function onlyAlphabets(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 0 || charCode == 127 || charCode == 32 || charCode == 08 || charCode == 09 || charCode == 13)
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }

    </script>
    <script language="Javascript" type="text/javascript">

        function onlyAlphabetsAdd(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 32 || charCode == 0 || charCode == 9 || charCode == 08 || charCode == 44 || charCode == 45 || charCode == 48)
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }

    </script>

    <script type="text/javascript">


        function phonenumber(inputtxt, txtid) {
            var phoneno = /^\d{10}$/;
            if (phoneno.test(inputtxt) && inputtxt.length == 10) {
                $("." + txtid).css("border", "solid 1px green")
                return true;
            }
            else {
                $("." + txtid).css("border", "solid 1px red")
                $("." + txtid).val('');
                alert("Mobile No. should be 10 digit");

                return false;
            }
        }
        function NewTabPreView() {

            var panel = document.getElementById("<%=lblUni.ClientID %>");
            var Vdist = document.getElementById("<%=ddlLevel.ClientID %>");

            if (Vdist.value == '2') {
                window.open(
                    "https://pms.educategirls.ngo/SurveyAnspre.aspx?ID=" + panel.value + "", "_blank");
            }
            else {
                window.open(
                    "https://pms.educategirls.ngo/SurveyAns2024.aspx?ID=" + panel.value + "", "_blank");
            }
        }

    </script>
    <script type="text/javascript">

        function Valdation(txtcls, txtaBoy) {
            var Eboy = 0;
            var Aboy = 0;
            $("." + txtcls).each(function (index, value) {
                if ($.trim($(this).val()) != "")
                    if (!isNaN($(this).val()))

                        Eboy = parseFloat($("." + txtaBoy).val());
                Aboy = parseFloat($("." + txtcls).val());

                if (Aboy < Eboy) {

                    alert("Enrollment  should be higher or equal to Appeared");
                    $("." + txtcls).focus();
                    $("." + txtaBoy).val('');
                    return true;
                }
                else {
                    return true;
                }

            });




        }
    </script>
    <script type="text/javascript">

        function calculate_totals(txtcls, txttotalcls) {
            var TotalCamt = 0;
            $("." + txtcls).each(function (index, value) {
                if ($.trim($(this).val()) != "")
                    if (!isNaN($(this).val()))
                        TotalCamt = TotalCamt + parseFloat($(this).val());
            });
            $("." + txttotalcls).val(TotalCamt);
            return false;
        }

        function arrivaldate(arrivaldate) {

            var arrivaldate = $('#' + arrivaldate).val();

            var today = new Date();
            alert(arrivaldate);
            alert(today.getDate());
            if (arrivaldate > today.getDate()) {
                alert("Should not be future date.");
                document.getElementById("" + sender + "").value = null;
                return false;
            }


        }

        function checkDate(arrivaldate) {
            var EnteredDate = $('#' + arrivaldate).val();

            var date = EnteredDate.substring(0, 2);

            var month = EnteredDate.substring(3, 5);
            var year = EnteredDate.substring(6, 10);

            var myDate = new Date(year, month - 1, date);

            var today = new Date();

            if (myDate > today) {
                alert("Should not be future date.");
                $('#' + arrivaldate).val = '';
            }

        }
        function isNumberKey(txt, evt) {
            debugger;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46 && charCode == 127) {
                if (txt.value.indexOf('.') === 1) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                else {
                    return true;
                }
            }
        }
        function checkkvalidation() {

            var startdate = document.getElementById('<%=txtstartdatecopy.ClientID %>').value;
            var enddate = document.getElementById('<%=txtenddateCopy.ClientID %>').value;
            var str = "";
            if (startdate == "") {
                str = "-- Enter Start Date";
            }
            if (enddate == "") {
                str = str + "\n-- Enter End Date";
            }
            if (str != "") {
                alert(str);
                return false;
            }
        }
    </script>
    <style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
        }
    </style>
    <style type="text/css">
        .PromptCSS {
            color: Snow;
            font-size: large;
            font-style: italic;
            font-weight: bold;
            background-color: DeepPink;
            font-family: Courier New;
            border: solid 1px Pink;
            height: 28px;
        }
    </style>
    <script language="Javascript" type="text/javascript">
        $(document).ready(function () {



            $("[id$=txtFromDate]").datepicker({ maxDate: new Date() });
            $("[id$=txtFromDate]").datepicker({
                dateFormat: 'dd/mm/yy'
            });
            $("[id$=txtFromDate]").datepicker();

            $("[id$=txtToDate]").datepicker({
                dateFormat: 'dd/mm/yy',
                maxDate: new Date()
            });


            $("[id$=txtToDate]").datepicker();
            $('#datepickers-container').css('z-index', 1045);
        });

    </script>
    <script type="text/javascript">
        function loadJSFunction() {

            $("[id$=txtFromDate]").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                minDate: 0,
                minDateTime: new Date(),
                yearRange: '1965:2026',
                defaultDate: new Date()

            });

            $("[id$=txtFromDate]").datepicker();

            $("[id$=txtToDate]").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                minDate: 0,
                minDateTime: new Date(),
                yearRange: '1965:2026',
                defaultDate: new Date()
            });

            $("[id$=txtToDate]").datepicker();


        }
        function arrivaldatecheck(sender, args) {
            var depdate = 'dep';
            var departuredate = $('.' + depdate).val();
            var arrivaldate = sender._selectedDate;
            var today = new Date();
            if (sender._selectedDate > today) {
                alert("Should not be future date.");
                sender._textbox.set_Value("");
                return false;

            }
        }
    </script>
    <script type="text/javascript">
        $("[id*=chkHeader]").on("click", function () {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function () {
                if (chkHeader.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
        });
        $("[id*=chkFormName]").on("click", function () {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=chkHeader]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkFormName]", grid).length == $("[id*=chkFormName]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                }
            }
        });
    </script>

    <script type="text/javascript">
        $("[id*=chkHeader2]").on("click", function () {
            var chkHeader2 = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function () {
                if (chkHeader2.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
        });
        $("[id*=ChkHR]").on("click", function () {
            var grid = $(this).closest("table");
            var chkHeader2 = $("[id*=chkHeader2]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader2.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=ChkHR]", grid).length == $("[id*=ChkHR]:checked", grid).length) {
                    chkHeader3.attr("checked", "checked");
                }
            }
        });

        function confirm_meth() {
            if (confirm("Do you want to Delete?") == true) {
                return true;
            }
            else {
                return false;
            }
        }



    </script>
    <script>
        $(document).on('click', '.chkQCSelectAll', function () {
            if ($(this).find('input').is(':checked')) {
                $('.chkQCFormQues input').prop('checked', true);
            }
            else {
                $('.chkQCFormQues input').prop('checked', false);
            }
        })

        $(document).on('click', '.chkSQSelectAll', function () {
            if ($(this).find('input').is(':checked')) {
                $('.chkSQFormQues input').prop('checked', true);
            }
            else {
                $('.chkSQFormQues input').prop('checked', false);
            }
        })

    </script>

    <script>
        $(document).on('click', '.chkQCSelectAll2', function () {
            if ($(this).find('input').is(':checked')) {
                $('.chkQCFormQues2 input').prop('checked', true);
            }
            else {
                $('.chkQCFormQues2 input').prop('checked', false);
            }
        })

        $(document).on('click', '.chkSQSelectAll2', function () {
            if ($(this).find('input').is(':checked')) {
                $('.chkSQFormQues2 input').prop('checked', true);
            }
            else {
                $('.chkSQFormQues2 input').prop('checked', false);
            }
        })

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="mkiiainpnl121">
        <ContentTemplate>

            <div class="container-fluid" style="margin-top: 0px;">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-3" style="padding-right: 0px;">
                        <div class="thumbnail" style="min-height: 757px; width: 233px;">
                            <div style="overflow: auto; height: 757px;">
                                <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="40"
                                    BorderStyle="None" DataKeyNames="Tarining_ID,Todate" OnPageIndexChanging="GVMain_PageIndexChanging"
                                    GridLines="None" AutoGenerateColumns="false" OnRowCommand="GVMain_OnRowCommand">
                                    <EmptyDataTemplate>
                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                            Data not found
                                        </div>
                                    </EmptyDataTemplate>
                                    <FooterStyle CssClass="FooterStyle" />
                                    <PagerStyle CssClass="paging" />
                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="44px" />
                                    <RowStyle HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                    <Columns>
                                        <asp:ButtonField HeaderText="Batch ID" ItemStyle-ForeColor="#333" DataTextField="BatchID"
                                            CommandName="GVMainEdit">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="Training </br>Type" ItemStyle-ForeColor="#333" DataTextField="AssessmentFor"
                                            CommandName="GVMainEdit">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="From </br> Date" ItemStyle-ForeColor="#333" DataTextField="FromDate"
                                            CommandName="GVMainEdit">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="To </br> Date" ItemStyle-ForeColor="#333" DataTextField="Todate"
                                            CommandName="GVMainEdit">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-9">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading" style="padding: 6px 0px;">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <h3 class="text-danger" style="margin: 0px;">Assessment Planner
                                                </h3>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 " style="margin-top: 3px;">
                                                <input type="image" id="ton-new" class="butt" src="Images/search-not-29.png" style="display: none;"
                                                    title="Search" />
                                                <asp:LinkButton ID="btnDelete" OnClick="btnDelete_Click" Visible="false" class="btn btn-primary btn-sm pull-right"
                                                    ToolTip="Delete" Style="margin-right: 5px;"
                                                    runat="server">Delete</asp:LinkButton>

                                                <asp:LinkButton ID="btnsave" OnClick="btnsave_Click" class="btn btn-sm btn-primary pull-right"
                                                    ToolTip="Save" ValidationGroup="saves"
                                                    Style="margin-right: 5px;" runat="server">Save</asp:LinkButton>
                                                <asp:LinkButton ID="btnAdd" OnClick="btnAdd_Click" class="btn btn-primary btn-sm pull-right"
                                                    ToolTip="Add" Style="margin-right: 5px;"
                                                    runat="server">Add</asp:LinkButton>


                                                <asp:LinkButton ID="LinkButton1" Visible="false" OnClick="Unlock_Click" class="btn btn-primary btn-sm pull-right"
                                                    ToolTip="Add" Style="margin-right: 5px;"
                                                    runat="server">Unlock</asp:LinkButton>



                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">




                                        <asp:Panel ID="pnlMain1" runat="server">

                                            <div class="col-lg-12" style="padding: 0px; margin-bottom: -3px;">
                                                <div class="form-horizonta-new">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                        <fieldset class="scheduler-border" runat="server" id="stmid">
                                                            <legend class="scheduler-border">Training Details </legend>

                                                            <div class="Training-details">

                                                                <div>
                                                                    <div class="row">

                                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                                            <div class="form-group">
                                                                                <label for="email" class="linhei">
                                                                                    Year :</label>
                                                                                <div class=" ">
                                                                                    <asp:DropDownList ID="ddlYear" AutoPostBack="true" Enabled="false" runat="server"
                                                                                        OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" class="form-control ">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                                            <div class="form-group">
                                                                                <label for="email" class="linhei">
                                                                                    State :
                                                                                </label>
                                                                                <div class="">
                                                                                    <asp:DropDownList ID="ddlState" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                                                        AutoPostBack="true" runat="server" class="form-control">
                                                                                    </asp:DropDownList>
                                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator10" InitialValue="0" runat="server"
                                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlState" ErrorMessage="*"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                                                    </span>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                                            <div class="form-group">
                                                                                <label class="linhei" for="Name" style="color: black;">
                                                                                    District :
                                                                                </label>
                                                                                <div class="">
                                                                                    <asp:DropDownList ID="ddlDistrictSearch" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDist_SelectedIndexChanged"
                                                                                        class="form-controlNew">
                                                                                    </asp:DropDownList>
                                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" InitialValue="0" runat="server"
                                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlDistrictSearch"
                                                                                ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-3" runat="server" id="div5">
                                                                            <div class="form-group">
                                                                                <label class="linhei">
                                                                                    Training Mode	: <span style="color: Red">*</span></label>
                                                                                <asp:DropDownList ID="ddlTraingMode" runat="server" TabIndex="1" CssClass="form-control input-sm">
                                                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Online Training</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Offline Training</asp:ListItem>
                                                                                    <asp:ListItem Value="3">Refresher Training</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12" runat="server" visible="false">
                                                                            <div class="form-group">
                                                                                <label class="control-label" for="Name" style="color: black;">
                                                                                    Block :
                                                                                </label>
                                                                                <div class="">
                                                                                    <asp:DropDownList ID="ddlMainBlock" Width="100%" runat="server"
                                                                                        class="form-control " />
                                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator12" InitialValue="0" runat="server"
                                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlMainBlock"
                                                                                ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>



                                                                <div class="row">

                                                                    <div class="col-sm-3">
                                                                        <label class="control-label">
                                                                            Training Start Date : <span style="color: Red">*</span></label>
                                                                        <asp:TextBox runat="server" ID="txtFromDate"
                                                                            autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>

                                                                        <%--                                                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server"
                                                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDate" PopupPosition="BottomRight">
                                                                        </ajax:CalendarExtender>--%>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate"
                                                                            Display="Dynamic" ErrorMessage="*" ForeColor="Red"
                                                                            ValidationGroup="saves">* </asp:RequiredFieldValidator>
                                                                    </div>


                                                                    <div class="col-sm-3">
                                                                        <label class="control-label">
                                                                            Training End Date : <span style="color: Red">*</span></label>
                                                                        <asp:TextBox runat="server" ID="txtToDate" autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                        <%--    <ajax:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtToDate" PopupPosition="BottomRight">
                                                                        </ajax:CalendarExtender>--%>

                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtToDate"
                                                                            Display="Dynamic" ErrorMessage="*" ForeColor="Red"
                                                                            ValidationGroup="saves">* </asp:RequiredFieldValidator>
                                                                    </div>



                                                                    <div class="col-sm-3">
                                                                        <label class="control-label">
                                                                            Location : <span style="color: Red">*</span></label>
                                                                        <asp:TextBox runat="server" ID="txtLocation" class="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLocation"
                                                                            Display="Dynamic" InitialValue="0" ErrorMessage="Please Enter Location" ForeColor="Red"
                                                                            SetFocusOnError="True" ValidationGroup="saves">* </asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="col-sm-3">

                                                                        <label class="control-label" for="Name">
                                                                            Training Type :
                                                                        </label>

                                                                        <asp:DropDownList ID="ddlTraining" runat="server" class="form-control">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlTraining"
                                                                            Display="Dynamic" InitialValue="0" ErrorMessage="Please Select Training type" ForeColor="Red"
                                                                            SetFocusOnError="True" ValidationGroup="saves">* </asp:RequiredFieldValidator>

                                                                    </div>
                                                                </div>

                                                                <div class="row" style="margin-top: 12px;     margin-bottom: 10px;">

                                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                                        <label class="control-label">
                                                                            Assessment For : <span style="color: Red">*</span></label>
                                                                        <asp:DropDownList ID="ddlLevel" runat="server" TabIndex="1" CssClass="form-control input-sm"
                                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                                                                        </asp:DropDownList>

                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlLevel"
                                                                            Display="Dynamic" InitialValue="0" ErrorMessage="Please select Assessment For" ForeColor="Red"
                                                                            SetFocusOnError="True" ValidationGroup="saves">* </asp:RequiredFieldValidator>

                                                                    </div>
                                                                    <div class="col-sm-3" runat="server" id="d1" visible="false">
                                                                        <label class="control-label">
                                                                            Training OutCome : <span style="color: Red">*</span></label>
                                                                        <asp:DropDownList ID="ddlLearning" AutoPostBack="true" OnSelectedIndexChanged="ddlLearning_SelectedIndexChanged" runat="server" CssClass="form-control input-sm">
                                                                        </asp:DropDownList>
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlLearning"
                                                                Display="Dynamic" InitialValue="0" ErrorMessage="Please select Training OutCome" ForeColor="Red"
                                                                SetFocusOnError="True" ValidationGroup="saves">* </asp:RequiredFieldValidator>--%>
                                                                    </div>

                                                                    <div class="col-sm-3" runat="server" id="d2" visible="false">
                                                                        <asp:Label ID="Label2" Style="margin-bottom: 5px; float: left" class="control-label" runat="server"
                                                                            Text="Specific training"><span style="color: Red">*</span></asp:Label>

                                                                        <asp:DropDownList ID="ddlTraingOutcome" runat="server" TabIndex="1" CssClass="form-control"
                                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlTraingOutcome_SelectedIndexChanged">
                                                                        </asp:DropDownList>

                                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTraingOutcome"
                                                                Display="Dynamic" InitialValue="0" ErrorMessage="Please select Training name" ForeColor="Red"
                                                                SetFocusOnError="True" ValidationGroup="saves">* </asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                    <div class="col-sm-3" runat="server" id="divassemnt" visible="false">
                                                                        <label class="control-label">
                                                                            Assessment Type	: <span style="color: Red">*</span></label>
                                                                        <asp:DropDownList ID="ddlassement" runat="server" TabIndex="1" Enabled="false" CssClass="form-control">
                                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="Baseline" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="Endline" Value="2"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlassement"
                                                                Display="Dynamic" InitialValue="0" ErrorMessage="Please select Assessment Type" ForeColor="Red"
                                                                SetFocusOnError="True" ValidationGroup="saves">* </asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <asp:Label ID="lblother" Visible="false" runat="server" class="control-label">
                                                                        Others : </asp:Label>

                                                                        <asp:TextBox ID="txtOthersName" Visible="false" runat="server" MaxLength="90" CssClass="form-control input-sm" Style="margin-top: 3px"></asp:TextBox>

                                                                    </div>


                                                                    <div class="col-lg-3" runat="server" visible="false">
                                                                        <label class="control-label">
                                                                            Type of Survey : <span style="color: Red"></span>
                                                                        </label>
                                                                        <asp:DropDownList ID="ddlsurveytype" runat="server" TabIndex="1" onchange="Gettablename()" CssClass="form-control input-sm" AutoPostBack="false">
                                                                        </asp:DropDownList>


                                                                    </div>
                                                                    <div class="col-lg-3" id="divother" runat="server" visible="false">
                                                                        <label class="control-label">
                                                                            Total No. of Questions : <span style="color: Red">*</span></label>
                                                                        <asp:TextBox ID="txtTotalQuestions" onkeypress="return isNumberKey(this,event);" MaxLength="2" runat="server" TabIndex="1" CssClass="form-control">
                                                                        </asp:TextBox>



                                                                    </div>
                                                                      <div class="col-lg-4" runat="server" id="div7" visible="false" style="margin-top:25px;" >
                                                                        <label class="control-label">
                                                                           
                                                                                      <asp:RadioButtonList ID="ddlMainID"  runat="server" RepeatColumns="2" RepeatLayout="Flow" RepeatDirection="Horizontal" CssClass="radioButtonList">
                                            <asp:ListItem Text="Main Training"  Value="1"></asp:ListItem>
                                                                                    <asp:ListItem Text="Reorientation" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                                                       
                                                     
                                                                    </div>
                                                                      
                                                                </div>
                                               
                                                                </div>


                                                            </div>

                                                        </fieldset>
                                                    </div>

                                                </div>
                                          
                                        </asp:Panel>




                                        <asp:Panel ID="pnltb" runat="server">
                                            <div class="row">
                                                <div class="col-md-12" style="padding: 0px;">
                                                    <div class="panel panel-default " style="margin-bottom: 0px">
                                                        <div class="panel-heading" style="padding: 5px 0;">
                                                            <div class="row">
                                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                                                                    <h3 class="text-danger" style="margin: 0; font-size: 20px;">Add Participants
                                                                    </h3>
                                                                </div>
                                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 text-right">
                                                                    <asp:LinkButton ID="linkSurvey" runat="server" OnClientClick="NewTabPreView()">
                                                       Preview
                                                                    </asp:LinkButton>
                                                                    <asp:TextBox ID="lblUni" runat="server" Style="display: none;"></asp:TextBox>
                                                                </div>

                                                                <div class="col-lg-3 col-md-5 col-sm-5 col-xs-12 text-center">

                                                                    <asp:TextBox ID="txtLink" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12">
                                                                    <div class="btn-group pull-right" style="margin-top: 3px">
                                                                        <asp:LinkButton ID="ln66kCopy" OnClick="btDownload_Click" class="btn btn-sm btn-primary primaryKK" runat="server" Style="margin-left: 4px;">
                                                                       Download User
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkCopy" Visible="false" class="btn btn-sm btn-primary primaryKK" runat="server" OnClick="LnkCopydata_Click" Style="margin-left: 4px;">
                                   <%-- <span class="glyphicon glyphicon-floppy-save"></span> --%> Copy Endline
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton ID="LnkEntry" class="btn btn-sm btn-primary primaryKK" runat="server" OnClick="LnkEntry_Click" Style="margin-left: 4px;">
                                   <%-- <span class="glyphicon glyphicon-floppy-save">--%></span>  Entry Done By
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton ID="LnkImport" class="btn btn-sm btn-primary" runat="server" OnClick="LnkImport_Click" Style="margin-left: 4px;">
                                    <%--<span class="glyphicon glyphicon-floppy-save">--%></span> Add Participants
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="col-sm-12" style="padding: 0px;">
                                                                <div class="part-1">


                                                                    <div class="search-bg" style="padding-bottom: 7px; margin-bottom: 0px;">
                                                                        <div class="row" style="display: flex; align-items: center;">
                                                                            <asp:Label ID="Label1" runat="server" CssClass="col-sm-4 font-weight-bold" Text="Assessment Name :"></asp:Label>
                                                                            <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                                                                <asp:DropDownList ID="ddlCategory" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control input input-sm"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="">
                                                                        <div class="" style="margin-bottom: 0px; height: 330px; overflow: auto;">
                                                                            <asp:GridView ID="GvQuestion" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display."
                                                                                AllowSorting="True" GridLines="Both" BorderColor="#e1e1e1" AlternatingRowStyle-BackColor="#F7F7F7"
                                                                                DataKeyNames="QuestionID,QuestionNo,Question,Sequence,FormID,CategoryName"
                                                                                CssClass="table table-striped table-bordered table-condensed" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                                                                AllowPaging="false" ShowFooter="false">
                                                                                <FooterStyle CssClass="DataGridFooter" />
                                                                                <PagerStyle CssClass="paging" />
                                                                                <HeaderStyle CssClass="DataGridHeader" />
                                                                                <SelectedRowStyle BackColor="#D5D5BF" Font-Bold="True" />
                                                                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkHeader" class="chkQCSelectAll" runat="server" Text="Select All" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkFormName" class="chkQCFormQues" runat="server" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="5%" CssClass="gvtextcenter" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Q No">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblQuestionNo" runat="server" Text='<%#Bind("QuestionNo") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="7%" CssClass="gvtextcenter" />
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Seq" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSequence" runat="server" Text='<%#Bind("Sequence") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="7%" CssClass="gvtextcenter" />
                                                                                    </asp:TemplateField>
                                                                                      <asp:TemplateField HeaderText="CategoryName">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblQuestgn" runat="server" Text='<%#Bind("CategoryName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Question">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblQuestion" runat="server" Text='<%#Bind("Question") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>




                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="part-butt">
                                                                    <span>
                                                                        <asp:LinkButton ID="btnprevone" Width="60" Height="40" class="fa fa-arrow-right" OnClick="btnprevone_onclick"
                                                                            aria-hidden="true" runat="server" />
                                                                        <asp:LinkButton ID="btnnextone" Width="60" Height="40" class="fa fa-arrow-left" OnClick="btnnextone_onclick"
                                                                            runat="server" />
                                                                    </span>
                                                                </div>
                                                                <div class="part-1">
                                                                    <div class="search-bg" style="padding-bottom: 7px; margin-bottom: 0px; height: 46px; line-height: 33px;">
                                                                        <div class="row">
                                                                            <asp:Label ID="Label3" runat="server" CssClass="col-sm-6 font-weight-bold" Text="Selected Assessment Question"></asp:Label>
                                                                            <asp:Label ID="Label4" runat="server" CssClass="col-sm-3 font-weight-blod" Text="Total Question:"></asp:Label>
                                                                            <asp:Label ID="lblTotal" runat="server" CssClass="col-sm-3 font-weight-bold" Text="0"></asp:Label>
                                                                            <div class="col-lg-6 col-md-8 col-sm-12 col-xs-12">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="">
                                                                        <div class="" style="margin-bottom: 0px; height: 330px; overflow: auto;">
                                                                            <asp:GridView ID="gvRightSearch" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display."
                                                                                AllowSorting="True" GridLines="Both" BorderColor="#e1e1e1" AlternatingRowStyle-BackColor="#F7F7F7"
                                                                                DataKeyNames="QuestionID,QuestionNo,Sequence,Question,FormID,CategoryName"
                                                                                CssClass="table table-striped table-bordered table-condensed" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                                                                AllowPaging="false" ShowFooter="false">
                                                                                <FooterStyle CssClass="DataGridFooter" />
                                                                                <PagerStyle CssClass="paging" />
                                                                                <HeaderStyle CssClass="DataGridHeader" />
                                                                                <SelectedRowStyle BackColor="#D5D5BF" Font-Bold="True" />
                                                                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkHeader2" Text="Select All" CssClass="chkQCSelectAll2" runat="server" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="ChkHR" runat="server" class="chkQCFormQues2" Checked="false" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="5%" CssClass="gvtextcenter" />
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderStyle-Width="6%">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnkUp" CommandArgument="up" runat="server" Text="&#x25B2;" CssClass="btn-link-new" OnClick="ChangePreferenceUP"></asp:LinkButton>
                                                                                            <asp:LinkButton ID="lnkDown" CommandArgument="down" runat="server" Text="&#x25BC;" CssClass="btn-link-new" OnClick="ChangePreferenceDown"></asp:LinkButton>


                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Q No">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblQuestionNo1" runat="server" Text='<%#Bind("QuestionNo") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="7%" CssClass="gvtextcenter" />
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Seq" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSequence1" runat="server" Text='<%#Bind("Sequence") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="7%" CssClass="gvtextcenter" />
                                                                                    </asp:TemplateField>
                                                                                      <asp:TemplateField HeaderText="CategoryName">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblQuestgn4" runat="server" Text='<%#Bind("CategoryName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Question">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblQuestion1" runat="server" Text='<%#Bind("Question") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>



                                                                                </Columns>
                                                                            </asp:GridView>
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
                            </div>
                        </div>
                    </div>
                    <!-- /#page-content-wrapper -->
                </div>
                <!-- /#wrapper -->
                <!-- /#wrapper -->
            </div>
            <div>
                <asp:ModalPopupExtender ID="ModalAlert" runat="server" TargetControlID="hdn_alertmodal"
                    PopupControlID="pnl_alert" CancelControlID="btn_cancelalert" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnl_alert" runat="server" Style="display: none; background: cadetblue;" class="modalPopup"
                    Width="345px">
                    <div style="padding: 0 0 10px 0;">
                        <div class="header">
                            <asp:Label ID="lbl_PopUpMessages" runat="server" CssClass="LabelHeader" Font-Bold="True"
                                Font-Size="11pt" Width="316px"></asp:Label>
                        </div>
                        <div style="width: 332px; text-align: center" class="body">
                            <div style="width: 100%; height: 8px;">
                            </div>
                            <asp:Label ID="lbl_messages" runat="server" CssClass="LabelHeader" Font-Bold="True"
                                Font-Size="11pt" Width="316px"></asp:Label>
                        </div>
                        <div style="text-align: center;">
                            <asp:Button ID="btn_cancelalert" runat="server" CssClass="myButton" Text="  OK  "
                                Height="25px" Width="74px" />
                        </div>
                    </div>
                    <div class="footerCategory" align="right">
                    </div>
                </asp:Panel>
                <asp:HiddenField ID="hdn_alertmodal" runat="server" />
                <asp:Button ID="DoNothing" runat="server" Text="" Style="display: none" />





            </div>

            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="hdnfilenm" runat="server" />
            <asp:HiddenField runat="server" ID="hdn_PBName" />
            <asp:HiddenField runat="server" ID="hdn_PBID" />

            <asp:ModalPopupExtender ID="MPEFormName1" BackgroundCssClass="modalBackground"
                runat="server" PopupControlID="pnlFormName1" TargetControlID="HFFormName1" CancelControlID="lblFormNameClose1">
            </asp:ModalPopupExtender>
            <asp:HiddenField ID="HFFormName1" runat="server" />

            <asp:Panel ID="pnlFormName1" runat="server" CssClass=" model-wid Mpopup1 mod-posi" Style="height: 610px  !important; position: fixed !important; width: 40% !important; display: none;">

                <div style="border: 0px solid #ccc; width: 100%; min-height: 100px; margin: 0 auto;">
                    <div class="modal-header">
                        <h3 class="text-danger" style="margin: 0;">Add Participate
                                            
                            <asp:LinkButton ID="lblFormNameClose1" class="btn btn-xs btn-danger pull-right"
                                runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>
                        </h3>

                    </div>
                    <div class="modal-body">
                        <div style="height: auto;">
                            <div class="form-group">
                                <div class="row" runat="server" id="Q1">
                                    <div class="form-group">
                                        <label class="control-label" style="margin-top: 10px; text-align: left;">
                                            Participate  : <span style="color: Red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtParticipate" runat="server" TextMode="MultiLine" TabIndex="4" CssClass="form-control input-sm" Style="margin-top: 5px; height: 80px !important;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtParticipate" Display="Dynamic" ErrorMessage="Please enter Participate" ForeColor="Red" SetFocusOnError="True" ValidationGroup="QuestionCreate1">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                </div>
                                <div class="row" runat="server" id="Div2" style="margin-bottom: 15px;">
                                    <div class="form-group">

                                        <div class="col-sm-12">
                                            <asp:LinkButton ID="btnexcel" runat="server" Text="Export to Excel" OnClick="btnExcel_Onclick"
                                                class="pull-left"></asp:LinkButton>
                                            <asp:Label ID="lblgl" runat="server" Style="margin-left: 47px; font-size: medium;" Text="Total :"></asp:Label>
                                            <asp:Label ID="lblPtotal" Style="margin-left: 8px; font-size: medium;" runat="server" Text="0"></asp:Label>
                                            <asp:Button ID="Button1" OnClick="btnParticipate_Click" runat="server" Text="Save" Style="margin-top: 5px" CssClass="btn btn-primary btn-sm pull-right" />



                                        </div>

                                    </div>
                                </div>

                                <div class="form-group" style="overflow: auto; margin-top: 2px; height: 270px;">
                                    <%--<div style="overflow: auto; margin-top: -5px; height: 350px;">--%>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display." AllowSorting="True" GridLines="Both" BorderColor="#e1e1e1" AlternatingRowStyle-BackColor="#F7F7F7"
                                        CssClass="table table-striped table table-hover table-bordered" SelectedRowStyle-BackColor="#e1f4a6"
                                        AllowPaging="false" Style="color: #333333" ShowHeaderWhenEmpty="true" DataKeyNames="ParticiparticipateName">
                                        <FooterStyle CssClass="DataGridFooter" />
                                        <PagerStyle CssClass="paging" />
                                        <HeaderStyle CssClass="DataGridHeader" />
                                        <SelectedRowStyle BackColor="#D5D5BF" Font-Bold="True" />
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Participate Code" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOptieeo5nse" runat="server" Text='<%#Bind("ParticiparticipateName") %>'></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle Width="20%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Participate Name" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOptisss55eeonse" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle Width="20%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="Delete_Questionttt" OnClientClick="javascript:return confirm('Are you sure you want to delete this record?');" OnClick="Delete_Question_Click2" class="btn btn-sm btn-link" runat="server">
                                                                     <span class="glyphicon glyphicon-trash" data-fa-transform="shrink-10 up-.5" style="color:red"></span>
                                                                        
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>




                            </div>
                        </div>

                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </asp:Panel>




            <asp:ModalPopupExtender ID="MPECopyEndline" BackgroundCssClass="modalBackground"
                runat="server" PopupControlID="pnlcopydata" TargetControlID="hdncopy" CancelControlID="lnkFormNameClosecopy">
            </asp:ModalPopupExtender>
            <asp:HiddenField ID="hdncopy" runat="server" />

            <asp:Panel ID="pnlcopydata" runat="server" CssClass=" model-wid Mpopup1 mod-posi" Style="height: 350px  !important; width: 40% !important; display: none;">

                <div style="border: 0px solid #ccc; width: 100%; min-height: 100px; margin: 0 auto;">
                    <div class="modal-header">
                        <h3 class="text-danger" style="margin: 0;">Copy Endline

                             <asp:LinkButton ID="lnkFormNameClosecopy" class="btn btn-xs btn-danger pull-right"
                                 runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>
                        </h3>

                    </div>
                    <div class="modal-body">
                        <div style="height: 225px;">
                            <div class="form-group">
                                <div class="row">

                                    <div class="col-sm-6">
                                        <label class="control-label" style="margin-bottom: 0px;">
                                            Start Date : <span style="color: Red">*</span></label>
                                        <asp:TextBox runat="server" ID="txtstartdatecopy" Style="margin-top: 3px"
                                            autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>

                                        <ajax:CalendarExtender ID="CalendarExtender3" runat="server"
                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtstartdatecopy" PopupPosition="BottomRight">
                                        </ajax:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtstartdatecopy"
                                            Display="Dynamic" InitialValue="0" ErrorMessage="Please Enter From Date" ForeColor="Red"
                                            SetFocusOnError="True">* </asp:RequiredFieldValidator>
                                    </div>


                                    <div class="col-sm-6">
                                        <label class="control-label">
                                            End Date : <span style="color: Red">*</span></label>
                                        <asp:TextBox runat="server" ID="txtenddateCopy" autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>
                                        <ajax:CalendarExtender ID="CalendarExtender4" runat="server"
                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtenddateCopy" PopupPosition="BottomRight">
                                        </ajax:CalendarExtender>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtenddateCopy"
                                            Display="Dynamic" InitialValue="0" ErrorMessage="Please Enter To Date" ForeColor="Red"
                                            SetFocusOnError="True">* </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="Div3" style="margin-bottom: 15px;">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" style="margin-top: 10px; text-align: left;">
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:Button ID="BtnCopy" OnClick="btnCopy_Click" OnClientClick="return checkkvalidation();" runat="server" ValidationGroup="csave" Text="Save" Style="margin-top: 5px" CssClass="btn btn-success btn-sm pull-right" />
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



            <asp:ModalPopupExtender ID="MPE_Entry" BackgroundCssClass="modalBackground"
                runat="server" PopupControlID="Pnl_Entry" TargetControlID="HdnEntry" CancelControlID="lnkEntryClose">
            </asp:ModalPopupExtender>
            <asp:HiddenField ID="HdnEntry" runat="server" />

            <asp:Panel ID="Pnl_Entry" runat="server" CssClass=" model-wid Mpopup1 mod-posi" Style="height: 610px  !important; position: fixed !important; width: 40% !important; display: none;">

                <div style="border: 0px solid #ccc; width: 100%; min-height: 100px; margin: 0 auto;">
                    <div class="modal-header">
                        <h3 class="text-danger" style="margin: 0;">Add Entry Done By 
                                            
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
                                            Entry Done By  : <span style="color: Red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" TabIndex="4" CssClass="form-control input-sm" Style="margin-top: 5px; height: 80px !important;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtParticipate" Display="Dynamic" ErrorMessage="Please enter Participate" ForeColor="Red" SetFocusOnError="True" ValidationGroup="QuestionCreate1">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                </div>
                                <div class="row" runat="server" id="Div4" style="margin-bottom: 15px;">
                                    <div class="form-group">

                                        <div class="col-sm-12">
                                            <asp:LinkButton ID="BtnEntry" OnClick="BtnEntry_Click" class="btn btn-xs btn-primary pull-right"
                                                ToolTip="Save" Width="55px"
                                                Style="margin-top: -4px; width: 70px; height: 26px;" runat="server">Save</asp:LinkButton>


                                        </div>
                                    </div>
                                </div>

                                <div class="form-group" style="overflow: auto; margin-top: 2px; height: 270px;">
                                    <%--<div style="overflow: auto; margin-top: -5px; height: 350px;">--%>
                                    <asp:GridView ID="GvEntry" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display." AllowSorting="True" GridLines="Both" BorderColor="#e1e1e1" AlternatingRowStyle-BackColor="#F7F7F7"
                                        CssClass="table table-striped table table-hover table-bordered" SelectedRowStyle-BackColor="#e1f4a6"
                                        AllowPaging="false" Style="color: #333333" ShowHeaderWhenEmpty="true" DataKeyNames="ParticiparticipateName">
                                        <FooterStyle CssClass="DataGridFooter" />
                                        <PagerStyle CssClass="paging" />
                                        <HeaderStyle CssClass="DataGridHeader" />
                                        <SelectedRowStyle BackColor="#D5D5BF" Font-Bold="True" />
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Entry Code" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEntryCode" runat="server" Text='<%#Bind("ParticiparticipateName") %>'></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle Width="20%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Entry Name" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEntryName" runat="server" Text='<%#Bind("EntryDoneByName") %>'></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle Width="20%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="Delete_QuestionEntry" OnClientClick="javascript:return confirm('Are you sure you want to delete this record?');" OnClick="Delete_Question_Click1" class="btn btn-sm btn-warning" runat="server">
                                                                     <span class="glyphicon glyphicon-trash" data-fa-transform="shrink-10 up-.5" style="color:red"></span>
                                                                        
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>

                            </div>
                        </div>

                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </asp:Panel>




            <asp:ModalPopupExtender ID="MPECopyEndline1" BackgroundCssClass="modalBackground"
                runat="server" PopupControlID="pnlcopydata1" TargetControlID="hdncopy1" CancelControlID="lnkFormNameClosecopy1">
            </asp:ModalPopupExtender>
            <asp:HiddenField ID="hdncopy1" runat="server" />

            <asp:Panel ID="pnlcopydata1" runat="server" CssClass=" model-wid Mpopup1 mod-posi" Style="height: 240px  !important; width: 40% !important; display: none;">

                <div style="border: 0px solid #ccc; width: 100%; min-height: 100px; margin: 0 auto;">
                    <div class="modal-header">
                        <h3 class="text-danger" style="margin: 0;">Unlock

                             <asp:LinkButton ID="lnkFormNameClosecopy1" class="btn btn-xs btn-danger pull-right"
                                 runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>
                        </h3>

                    </div>
                    <div class="modal-body">
                        <div style="height: 105px;">
                            <div class="form-group">
                                <div class="row">

                                    <div class="col-sm-6">
                                        <label class="control-label" style="margin-bottom: 0px;">
                                            Unlock Date : <span style="color: Red">*</span></label>
                                        <asp:TextBox runat="server" ID="txtLockDate" Style="margin-top: 3px"
                                            autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>

                                        <ajax:CalendarExtender ID="CalendarExtender5" runat="server"
                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtLockDate" PopupPosition="BottomRight">
                                        </ajax:CalendarExtender>
                                    </div>



                                </div>
                                <div class="row" runat="server" id="Div6" style="margin-bottom: 15px;">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" style="margin-top: 10px; text-align: left;">
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:LinkButton ID="Bt3nCopy" OnClick="LockSave" runat="server" Text="Save" Style="margin-top: 21px; margin-left: -73px;" CssClass="btn btn-primary btn-sm">Save
                                            </asp:LinkButton>
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
            <asp:PostBackTrigger ControlID="btnexcel" />
            <asp:PostBackTrigger ControlID="ln66kCopy" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

