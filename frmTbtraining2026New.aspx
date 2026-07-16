<%@ Page Language="C#" AutoEventWireup="true" Culture="en-GB" CodeFile="frmTbtraining2026New.aspx.cs"
    MasterPageFile="~/Site.master" Inherits="frmTbtraining2026New" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <script src="online-js/jquery-ui.min.js" type="text/javascript"></script>
    <link href="online-js/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="online-js/jquery-ui-timepicker-addon.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="css/eg_styles.css">
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

        .btn-xs, .btn-group-xs > .btn {
            padding: 4px 5px;
        }

        .glyphicon.glyphicon-remove {
            position: relative;
            top: -1px;
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
    <title>Document</title>
    <script type="text/javascript">





        function bindPicker() {
            $(".JQCallfd").timepicker();
        }
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindPicker);
            bindPicker();

        });
    </script>
    <script language="Javascript" type="text/javascript">
        function ValidateEmail(inputText) {


            var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            if (inputText.value.match(mailformat)) {
                document.form1.text1.focus();
                return true;
            }
            else {
                alert("You have entered an invalid email address!");
                inputText.value = '';
                inputText.focus();
                return false;
            }
        }

    </script>
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
        .new-card {
            float: left;
            width: 100%;
            height: auto;
            border-radius: 4px;
            border: 1px solid #ddd;
            box-shadow: 0px 4px 6px 0px #7e7e7e;
            padding: 10px;
        }

        .col-sm-3, .col-xs-12 {
            padding: 0px 6px;
        }

        .tf-title {
            float: left;
            width: 100%;
            height: auto;
            background-color: #ffffff;
            text-align: center;
            font-weight: bold;
            font-size: 18px;
            padding: 4px 15px;
            color: #000;
            box-shadow: 0px 4px 4px -2px #ccc;
        }

        .p-0 {
            padding: 0px;
        }

        .form-group {
            float: left;
            width: 100%;
        }

        .training_details_card {
/*            border: 1px solid #cda7a859;
            border-radius: 8px;
            padding: 6px 12px;
            background-image: linear-gradient(to right, #ffeaeb, #f9f9f9);
            font-size: 18px;
            display: flex;
            justify-content: start;
            align-items: center;
            gap: 12px;
            margin-bottom:12px;*/
border: 1px solid #00bcd463;
    border-radius: 8px;
    padding: 6px 12px;
    background-image: linear-gradient(to right, #00bcd412, #f9f9f9);
    font-size: 14px;
    display: flex;
    justify-content: start;
    align-items: center;
    gap: 12px;
    margin-bottom: 12px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="mkiiainpnl121">
        <ContentTemplate>

            <div class="col-sm-12">
                <div class="panel panel-default" style="padding-bottom: 0px !important;">

                    <div class="panel-heading" style="padding: 2px;">
                        <div style="display: flex; justify-content: space-between; gap: 12px; align-items: center">
                            <h3 class="text-danger" style="margin: 3px; padding-left: 10px;">Team Balika Training
                            </h3>
                            <div style="display: flex; justify-content: space-between; gap: 12px; align-items: center">
                                <label class="col-sm-2  padd linhei" style="font-size: 16px; white-space: nowrap; width: 215px;">Please select training schedular</label>
                                <asp:DropDownList ID="ddlSchedue" AutoPostBack="true" OnSelectedIndexChanged="ddlSchedue_SelectedIndexChanged"
                                    runat="server" class="form-control ">
                                </asp:DropDownList>
                                <asp:LinkButton ID="btnsave" OnClick="btnsave_Click" class="btn btn-sm btn-primary pull-right"
                                    ToolTip="Save" ValidationGroup="savesNew"
                                    Style="margin-right: 5px; margin-top: 5px;" runat="server">Save</asp:LinkButton>

                                <asp:ImageButton ID="btnDelete" CssClass="btn btn-info pull-right" ToolTip="Delete"
                                    BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px; padding: 0px;"
                                    runat="server" />
                                <asp:ImageButton ID="btnAdd" Visible="false" OnClick="btnAdd_Click" CssClass="btn btn-info pull-right"
                                    BackColor="#f5f5f5" ToolTip="Add" ImageUrl="~/images/add-29-1.png" Style="margin-right: 5px; padding: 0px;"
                                    runat="server" />
                            </div>
                        </div>



                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12" style="padding: 0px 12px 12px 12px;">
                    <div class="new-card" style="height: 895px">
                        <div style="overflow: auto; margin-top: 0px; height: 885px;">
                            <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="40"
                                BorderStyle="None" DataKeyNames="UniqueCode" OnPageIndexChanging="GV_Project_PageIndexChanging"
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
                                    <asp:ButtonField HeaderText="Location" ItemStyle-ForeColor="#333" DataTextField="DistrictName"
                                        CommandName="GVUIO">
                                        <ItemStyle CssClass="padding-lef"  />
                                        <HeaderStyle CssClass="padding-lef" />
                                    </asp:ButtonField>
                                    <asp:ButtonField HeaderText="From Date" ItemStyle-ForeColor="#333" DataTextField="FromDate"
                                        CommandName="GVUIO">
                                        <ItemStyle CssClass="padding-lef"  />
                                        <HeaderStyle CssClass="padding-lef" />
                                    </asp:ButtonField>
                                    <asp:ButtonField HeaderText="To  Date" ItemStyle-ForeColor="#333" DataTextField="todate"
                                        CommandName="GVUIO">
                                        <ItemStyle CssClass="padding-lef"  />
                                        <HeaderStyle CssClass="padding-lef" />
                                    </asp:ButtonField>
                                    <asp:ButtonField HeaderText="Name" Visible="false" Text="Button" DataTextField="UniqueCode"></asp:ButtonField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="col-lg-10 col-md-10 col-sm-9 col-xs-12" style="padding: 0px;">
                    <div class="new-card " style="height: 895px;">
                        <table class="table table-striped table-bordered" style="font-size: 12px;">
                            <tbody>
                                <tr>
                                    <th colspan="9" class="text-danger">Training  Details</th>
                                </tr>

                                <tr>
                                    <td colspan="9">
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


                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="linhei">
                                                        District :
                                                    </label>
                                                    <div class="">
                                                        <asp:DropDownList ID="ddlDistrictSearch" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDist_SelectedIndexChanged"
                                                            class="form-control">
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                <div class="form-group">
                                                    <label class="linhei" for="Name" style="color: black;">
                                                        Training OutCome :
                                                    </label>
                                                    <div class="">
                                                        <asp:Label ID="lbloutcomde" runat="server"></asp:Label>
                                                        <asp:DropDownList ID="ddlTraingOutcome" Enabled="false" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">
                                                        Training Start Date : <span style="color: Red">*</span></label>
                                                    <asp:TextBox runat="server" ID="txtFromDate"
                                                        autocomplete="off" Enabled="false" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>

                                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server"
                                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDate" PopupPosition="BottomRight">
                                                    </ajax:CalendarExtender>
                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate"
                                                            Display="Dynamic" ForeColor="Red"
                                                            SetFocusOnError="True" ValidationGroup="savesNew">* </asp:RequiredFieldValidator>
                                                    </span>
                                                </div>
                                            </div>


                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">
                                                        Training End Date : <span style="color: Red">*</span></label>
                                                    <asp:TextBox runat="server" ID="txtToDate" Enabled="false" autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>
                                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server"
                                                        Format="dd/MM/yyyy" TargetControlID="txtToDate" PopupPosition="BottomRight">
                                                    </ajax:CalendarExtender>
                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtToDate"
                                                            Display="Dynamic" ForeColor="Red"
                                                            SetFocusOnError="True" ValidationGroup="savesNew">* </asp:RequiredFieldValidator></span>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">
                                                        Location : <span style="color: Red">*</span></label>
                                                    <asp:TextBox Enabled="false" runat="server" ID="txtLocation" class="form-control"></asp:TextBox>

                                                </div>
                                            </div>
                                            <%-- <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">
                                                        Training Start Time : <span style="color: Red">*</span></label>
                                                    <asp:TextBox ID="txttimein" onpaste="return false;" runat="server"
                                                        CssClass="JQCallfd form-control" Style="background-color: Transparent" onkeydown="return false;"
                                                        onkeypress="return false;"></asp:TextBox>
                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttimein"
                                                            Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                            SetFocusOnError="True" ValidationGroup="savesNew"></asp:RequiredFieldValidator>
                                                    </span>
                                                </div>
                                            </div>--%>
                                        </div>
                                        <div class="row">

                                            <%-- <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label class="control-label">
                                                        Training End Time : <span style="color: Red">*</span></label>
                                                    <asp:TextBox ID="txttimeout" onpaste="return false;" runat="server"
                                                        CssClass="JQCallfd form-control" Style="background-color: Transparent" onkeydown="return false;"
                                                        onkeypress="return false;"></asp:TextBox>
                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txttimeout"
                                                            Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                            SetFocusOnError="True" ValidationGroup="savesNew"></asp:RequiredFieldValidator>
                                                    </span>
                                                </div>
                                            </div>--%>



                                            <div class="col-sm-3" runat="server" id="div5">
                                                <div class="form-group">
                                                    <label class="control-label">
                                                        Training Mode	: <span style="color: Red">*</span></label>
                                                    <asp:DropDownList ID="ddlTraingMode" Enabled="false" runat="server" TabIndex="1" CssClass="form-control input-sm">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Online Training</asp:ListItem>
                                                        <asp:ListItem Value="2">Offline Training</asp:ListItem>
                                                        <asp:ListItem Value="3">Refresher Training</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>



                                            <div class="col-sm-3" runat="server" id="div1">
                                                <div class="form-group">
                                                    <label class="control-label">
                                                        Training Type	: <span style="color: Red">*</span></label>
                                                    <asp:DropDownList ID="ddlTraining" Enabled="false" runat="server" TabIndex="1" CssClass="form-control input-sm">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>

                                            <div class="col-sm-3" runat="server" id="div2">
                                                <div class="form-group">
                                                    <label class="control-label">
                                                    </label>


                                                    <asp:LinkButton ID="imNewSerach" style="margin-top: 20px;" OnClick="btnNewSerach_Click" class="btn btn-sm btn-primary pull-right"
                                                        runat="server">Serach</asp:LinkButton>

                                                </div>

                                            </div>
                                    </td>
                                </tr>
                                <tr style="width: 50%; display: none">
                                    <th colspan="9" style="text-align: center;">Trainer Details</th>
                                </tr>
                                <tr style="width: 50%; display: none">
                                    <td colspan="2" style="width: 50%; display: none" runat="server">
                                        <div class="form-group">
                                            <label class="col-sm-4  padd linhei">Trainer Type</label>
                                            <div class="col-sm-8 p-0">
                                                <asp:DropDownList ID="ddlType" runat="server" Width="50%" class="form-control" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">Internal</asp:ListItem>
                                                    <asp:ListItem Value="2">External</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:LinkButton ID="btnAddTrain" OnClick="LnkEntry_Click" Visible="false" class="btn btn-sm btn-primary pull-right"
                                                    Style="margin-right: 5px; margin-top: -25px;" runat="server">Add Trainer</asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="hh1" visible="false" style="overflow: auto; margin-top: 2px; height: 170px;">
                                            <%--<div style="overflow: auto; margin-top: -5px; height: 350px;">--%>
                                            <asp:GridView ID="GvEntryNew" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display." AllowSorting="True" GridLines="Both" BorderColor="#e1e1e1" AlternatingRowStyle-BackColor="#F7F7F7"
                                                CssClass="table table-striped table table-hover table-bordered" SelectedRowStyle-BackColor="#e1f4a6"
                                                AllowPaging="false" Style="color: #333333" ShowHeaderWhenEmpty="true" DataKeyNames="ParticiparticipateName,EntryDoneByName">
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

                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                        <div class="form-group" runat="server" id="r1">
                                            <label class="col-sm-4  padd linhei">External Trainer Name</label>
                                            <div class="col-sm-8 p-0">
                                                <asp:TextBox ID="txtTrainename" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="r2">
                                            <label class="col-sm-4  padd linhei">External Trainer Email</label>
                                            <div class="col-sm-8 p-0">
                                                <asp:TextBox ID="txtEmail" Enabled="false" onchange="javascript:ValidateEmail(this);" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="r3" style="margin-bottom: 5px;">
                                            <label class="col-sm-4  padd linhei">External Trainer Contact No.</label>
                                            <div class="col-sm-8 p-0">
                                                <asp:TextBox ID="txtContact" Enabled="false" OnKeyUp="javascript:inputtxt();" runat="server" MaxLength="10"
                                                    onkeypress="return isNumberKey(this,event);" onchange="javascript: phonenumber(this.value,'TeContact1');"
                                                    autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                            </div>
                                        </div>
                                    </td>
                                    <td colspan="2">
                                        <div class="form-group" style="margin-bottom: 5px;">
                                            <label>Training Discription</label>
                                            <asp:TextBox ID="txtDesc" TextMode="MultiLine" runat="server" Rows="7" class="form-control"></asp:TextBox>

                                        </div>
                                    </td>
                                </tr>
                           
                                <tr>
                                    <th colspan="9" style="text-align: center;">Attendance
                                 <asp:LinkButton ID="Butteon2" OnClick="LnkImport_Click" class="btn btn-sm btn-primary pull-right"
                                     Style="margin-right: 5px;" runat="server">Add Participant</asp:LinkButton>

                                        <asp:LinkButton ID="Butteon1" OnClick="btDownload_Click" class="btn btn-sm btn-primary pull-right"
                                            Style="margin-right: 5px;" runat="server">Download Employee</asp:LinkButton>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="9">
                                        <div class="asd" id="tt" runat="server" style="margin-bottom: 0px; height: 357Px;">
                                            <div style="float: left; height: 347px; width: 100%; overflow: auto">

                                               
                                                <asp:GridView ID="gvRightSearch" Width="100%" Style="margin-bottom: 0px" runat="server" CssClass="table table-striped table-bordered" OnRowDataBound="gvTb_RowDataBound"
                                                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" DataKeyNames="Participant,ParticipantName,UserType"
                                                    GridLines="None">
                                                    <FooterStyle CssClass="FooterStyle" />
                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White"  />
                                                    <RowStyle HorizontalAlign="Left" />
                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Participant Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTBCode1" runat="server" Text='<%#Eval("Participant") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="padding-lef" />
                                                            <HeaderStyle CssClass="padding-lef" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Participant Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName1" runat="server" Text='<%#Eval("ParticipantName") %>'></asp:Label>

                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="padding-lef"  />
                                                            <HeaderStyle CssClass="padding-lef" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_final1" runat="server" />
                                                                <asp:Label ID="lblTday1" Visible="false" runat="server" Text='<%#Eval("Day1") %>'></asp:Label>
                                                                <asp:Label ID="lblUserType" Visible="false" runat="server" Text='<%#Eval("UserType") %>'></asp:Label>
                                                                  <asp:Label ID="lblFlag" Visible="false" runat="server" Text='<%#Eval("Flag") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                      <asp:CheckBox ID="Chk_final2" runat="server" />
                                              <asp:Label ID="lblTday2" Visible="false" runat="server" Text='<%#Eval("Day2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_final3" runat="server" />
                                                                <asp:Label ID="lblTday3" Visible="false" runat="server" Text='<%#Eval("Day3") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_final4" runat="server" />
                                                                <asp:Label ID="lblTday4" Visible="false" runat="server" Text='<%#Eval("Day4") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_final5" runat="server" />
                                                                <asp:Label ID="lblTday5" Visible="false" runat="server" Text='<%#Eval("Day5") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_final6" runat="server" />
                                                                <asp:Label ID="lblTday6" Visible="false" runat="server" Text='<%#Eval("Day6") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_final7" runat="server" />
                                                                <asp:Label ID="lblTday7" Visible="false" runat="server" Text='<%#Eval("Day7") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>




                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <ajax:ModalPopupExtender ID="MPE_Entry" BackgroundCssClass="modalBackground"
                runat="server" PopupControlID="Pnl_Entry" TargetControlID="HdnEntry" CancelControlID="lnkEntryClose">
            </ajax:ModalPopupExtender>
            <asp:HiddenField ID="HdnEntry" runat="server" />

            <asp:Panel ID="Pnl_Entry" runat="server" CssClass=" model-wid Mpopup1 mod-posi" Style="height: 610px  !important; width: 40% !important; display: none;">

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
                                <div class="row" runat="server" id="Div3">
                                    <div class="form-group">
                                        <label class="control-label" style="margin-top: 10px; text-align: left;">
                                            Entry Done By  : <span style="color: Red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" TabIndex="4" CssClass="form-control input-sm" Style="margin-top: 5px; height: 80px !important;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidato22r4" runat="server" ControlToValidate="TextBox1" Display="Dynamic" ErrorMessage="Please enter Participate" ForeColor="Red" SetFocusOnError="True" ValidationGroup="QuestionCreate1">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                </div>
                                <div class="row" runat="server" id="Div4" style="margin-bottom: 15px;">
                                    <div class="form-group">

                                        <div class="col-sm-12">
                                            <%-- <asp:ImageButton ID="BtnEntry" runat ="server" ImageUrl="~/images/Excel-2-icon.png" height="33px" OnClick="btnExcel_Onclick" CssClass="left" />--%>

                                            <asp:LinkButton ID="LinkButBtnEntryton1" OnClick="BtnEntry_Click" class="btn btn-xs btn-primary pull-right"
                                                ToolTip="Save" Width="55px"
                                                Style="margin-right: 5px; margin-top: 5px; padding: 0px;" runat="server">Save</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>

                                <div class="form-group" style="overflow: auto; margin-top: 2px; height: 270px;">
                                    <%--<div style="overflow: auto; margin-top: -5px; height: 350px;">--%>
                                    <asp:GridView ID="GvEntry" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display." AllowSorting="True" GridLines="Both" BorderColor="#e1e1e1" AlternatingRowStyle-BackColor="#F7F7F7"
                                        CssClass="table table-striped table table-hover table-bordered" SelectedRowStyle-BackColor="#e1f4a6"
                                        AllowPaging="false" Style="color: #333333" ShowHeaderWhenEmpty="true" DataKeyNames="ParticiparticipateName,EntryDoneByName">
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
            <ajax:ModalPopupExtender ID="MPEFormName1" BackgroundCssClass="modalBackground"
                runat="server" PopupControlID="pnlFormName1" TargetControlID="HFFormName1" CancelControlID="lblFormNameClose1">
            </ajax:ModalPopupExtender>
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
                                <div class="row" runat="server" id="Div6" style="margin-bottom: 15px;">
                                    <div class="form-group">

                                        <div class="col-sm-12">
                                            <asp:LinkButton ID="btnexcel" runat="server" OnClick="btnExcel_Onclick" Text="Export to Excel"
                                                class="pull-left"></asp:LinkButton>



                                            <asp:LinkButton ID="Butt77on1" OnClick="btnParticipate_Click" class="btn btn-xs btn-primary pull-right"
                                                ToolTip="Save" Width="55px"
                                                Style="margin-right: 5px; margin-top: 5px; padding: 0px;" runat="server">Save</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>

                                <div class="form-group" style="overflow: auto; margin-top: 2px; height: 270px;">
                                    <%--<div style="overflow: auto; margin-top: -5px; height: 350px;">--%>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display."
                                        AllowSorting="True" GridLines="Both" BorderColor="#e1e1e1" AlternatingRowStyle-BackColor="#F7F7F7"
                                        CssClass="table table-striped table table-hover table-bordered" SelectedRowStyle-BackColor="#e1f4a6"
                                        AllowPaging="false" Style="color: #333333" ShowHeaderWhenEmpty="true" DataKeyNames="TBCode">
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
                                                    <asp:Label ID="lblOptieeo5nse" runat="server" Text='<%#Bind("TBCode") %>'></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle Width="20%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Participate Name" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOptisss55eeonse" runat="server" Text='<%#Bind("TBName") %>'></asp:Label>
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
        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="Butteon1" />
            <asp:PostBackTrigger ControlID="btnexcel" />

        </Triggers>
    </asp:UpdatePanel>

</asp:Content>



