<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="SurveyReport.aspx.cs" Inherits="SurveyReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="js/jquery-2.1.0.js"></script>
    <script src="js/Common.js" type="text/javascript"></script>
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 100004;
        }
    </style>
    <script type="text/javascript">

        function SetText(txtcls, txttotalcls) {
            if (txttotalcls == '') {
                txtcls.text = "Child summaries-(Process Monitoring)"
            }

            else {
                txtcls.text = " Process Monitoring"
            }
        }
        function SetText1(txtcls, txttotalcls) {
            if (txttotalcls == '') {
                txtcls.text = "Child summaries-(Target Monitoring)"
            }

            else {
                txtcls.text = "Target Monitoring"
            }
        }
    </script>
    <style>
        .pagination-ys
        {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }
        
        .pagination-ys table > tbody > tr > td
        {
            display: inline;
        }
        
        .pagination-ys table > tbody > tr > td > a, .pagination-ys table > tbody > tr > td > span
        {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;
            color: #3ac0f2;
            background-color: #ffffff;
            border: 1px solid #dddddd;
            margin-left: -1px;
        }
        
        .pagination-ys table > tbody > tr > td > span
        {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;
            margin-left: -1px;
            z-index: 2;
            color: #3ac0f2;
            background-color: #f5f5f5;
            border-color: #dddddd;
            cursor: default;
        }
        
        .pagination-ys table > tbody > tr > td:first-child > a, .pagination-ys table > tbody > tr > td:first-child > span
        {
            margin-left: 0;
            border-bottom-left-radius: 4px;
            border-top-left-radius: 4px;
        }
        
        .pagination-ys table > tbody > tr > td:last-child > a, .pagination-ys table > tbody > tr > td:last-child > span
        {
            border-bottom-right-radius: 4px;
            border-top-right-radius: 4px;
        }
        
        .pagination-ys table > tbody > tr > td > a:hover, .pagination-ys table > tbody > tr > td > span:hover, .pagination-ys table > tbody > tr > td > a:focus, .pagination-ys table > tbody > tr > td > span:focus
        {
            color: Black;
            background-color: #eeeeee;
            border-color: #dddddd;
        }
    </style>
    <style type="text/css">
        .multiselect.dropdown-toggle.btn.btn-default > div.restricted
        {
            margin-right: 5px;
            max-width: 100px;
            overflow: hidden;
        }
    </style>
    <style type="text/css">
        .radio .cr
        {
            border-radius: 75%;
            border-color: #333;
        }
        
        .checkbox .cr .cr-icon, .radio .cr .cr-icon
        {
            position: absolute;
            font-size: .8em;
            line-height: 0;
            top: 50%;
            left: 15%;
        }
        
        .radio .cr .cr-icon
        {
            margin-left: 0.04em;
        }
        
        .checkbox label input[type="checkbox"], .radio label input[type="radio"]
        {
            display: none;
        }
        
        .checkbox label input[type="checkbox"] + .cr > .cr-icon, .radio label input[type="radio"] + .cr > .cr-icon
        {
            transform: scale(3) rotateZ(-220deg);
            opacity: 0;
            transition: all .7s ease-in;
        }
        
        .checkbox label input[type="checkbox"]:checked + .cr > .cr-icon, .radio label input[type="radio"]:checked + .cr > .cr-icon
        {
            transform: scale(1) rotateZ(0deg);
            opacity: 1;
        }
        
        .checkbox label input[type="checkbox"]:disabled + .cr, .radio label input[type="radio"]:disabled + .cr
        {
            opacity: .5;
        }
        
        .new-navbutt
        {
            float: left !important;
            margin-top: 0px !important;
        }
        
        .row-border
        {
            border-bottom: 1px dotted rgb(221, 221, 221);
            margin-bottom: 15px;
        }
        
        .checkbox
        {
            position: relative;
            display: block;
            margin-top: 2px !important;
            margin-bottom: 5px !important;
        }
        
        
        .new-navbutt
        {
            float: left !important;
            margin-top: 0px !important;
        }
        
        .row-border
        {
            border-bottom: 1px dotted rgb(221, 221, 221);
            margin-bottom: 15px;
        }
        .checkbox .cr .cr-icon, .radio .cr .cr-icon
        {
            position: absolute;
            font-size: .8em;
            line-height: 0;
            top: 50%;
            left: 15%;
        }
        
        .radio .cr .cr-icon
        {
            margin-left: 0.04em;
        }
        
        .checkbox label input[type="checkbox"], .radio label input[type="radio"]
        {
            display: none;
        }
        
        .checkbox label input[type="checkbox"] + .cr > .cr-icon, .radio label input[type="radio"] + .cr > .cr-icon
        {
            transform: scale(3) rotateZ(-220deg);
            opacity: 0;
            transition: all .7s ease-in;
        }
        
        .checkbox label input[type="checkbox"]:checked + .cr > .cr-icon, .radio label input[type="radio"]:checked + .cr > .cr-icon
        {
            transform: scale(1) rotateZ(0deg);
            opacity: 1;
        }
        
        .checkbox label input[type="checkbox"]:disabled + .cr, .radio label input[type="radio"]:disabled + .cr
        {
            opacity: .5;
        }
        
        .new-navbutt
        {
            float: left !important;
            margin-top: 0px !important;
        }
        
        .row-border
        {
            border-bottom: 1px dotted rgb(221, 221, 221);
            margin-bottom: 15px;
        }
        
        .checkbox
        {
            position: relative;
            display: block;
            margin-top: 2px !important;
            margin-bottom: 5px !important;
        }
        
        .CheckBoxListCssClass
        {
            font-family: calibri;
            margin-left: 5px;
            font-weight: bold;
            font-size: small;
            top: 53%;
            left: 3%;
            text-align: left !important;
            color: Black;
            background: white !important;
        }
        .CheckBoxListCssClassNew
        {
            font-family: calibri;
            margin-left: 5px;
            font-weight: bold;
            font-size: 11.2px !important;
            top: 53%;
            left: 3%;
            text-align: left !important;
            color: Black;
            background: white !important;
        }
        .checkboxlist
        {
            position: absolute;
            font-size: .8em;
            margin-left: 10px;
            line-height: 0;
            top: 50%;
            left: 15%;
        }
        .td-widt
        {
            width: auto !important;
        }
        
        .td-width1
        {
            width: 100px !important;
        }
        
        @media (min-width:10px) and (max-width:640px)
        {
            .td-widt
            {
                width: 90px !important;
            }
        
        
            .td-width1
            {
                width: 90px !important;
            }
        }
        
        .table-mb
        {
            margin-bottom: 2px !important;
        }
        
        .thnail
        {
            padding: 0px !important;
            border-radius: 0px !important;
            margin-bottom: 0px !important;
            min-height: 60px;
        }
        .thumbnail ul
        {
            float: left;
            width: 100%;
            height: auto;
            margin: 0px;
            padding: 0px;
            list-style: none;
        }
        .thumbnail ul li
        {
            float: left;
            width: 100%;
            height: auto;
        }
        .thumbnail ul li a
        {
            float: left;
            width: 100%;
            height: auto;
            padding: 10px;
            border: 1px solid #ddd;
        }
    </style>
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
        .modalpopupcss
        {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        .modalPopup
        {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
            z-index: 999999;
        }
        
        
        input[type="radio"], input[type="checkbox"]
        {
            margin: 4px 7px 0px !important;
            margin-top: 1px !important;
            line-height: normal !important;
        }
        .gridnewheadercss
        {
            color: #ffffff;
            vertical-align: middle;
            background-color: #81AB81;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
    <div class="container-fluid" style="margin-top: 110px !important;">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="panel panel-default " style="background-color: #f5f5f5;padding-right: 10px;">
                    <div class="panel-heading" style="padding: 5px 0px;">
                        <div class="row">
                            <%--<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                <div id="Div2" class="col-lg-10 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                    <div class="">
                                        <h3 class="text-danger" style="margin: 0px;">
                                            Report
                                        </h3>
                                    </div>
                                </div>
                                <div id="Div3" class="col-lg-2 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                    <div class="form-group">
                                        <asp:LinkButton ID="LinkButton1" Visible="false" runat="server" Text="Export to Excel"
                                            class="pull-right"></asp:LinkButton>
                                        <%--</div>
                                         
                                           <span class="pull-right" style="font-size: 17px;"></span>
                                        <div id="Div1" class="col-lg-2 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                        <div class="form-group">
                                        <asp:LinkButton ID="lnkCSV" runat="server" Text="Export to CSV" ></asp:LinkButton>
                                    </div>
                                </div>
                            </div>--%>
                            <asp:LinkButton ID="LinkButton1" runat="server" Text="Export to Excel" OnClick="btnImport_Click"
                                class="pull-right"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 text-left" style="padding-right: 0px;">
            <div style="overflow: auto; margin-top: 0px; height: 586px;">
                <div class="thumbnail" style="height: 565PX;">
                    <ul style="margin: 0px">
                      
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton7" runat="server" OnClick="Lnkpf_OnClick" Style="color: gren;
                                color: blue;" Text="Summary Report"></asp:LinkButton>
                        </li>
                          <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton8" runat="server"  OnClick="LnkDetails_OnClick" Style="color: gren;
                                color: blue;" Text="Details Report"></asp:LinkButton>
                        </li>

                           <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton2" runat="server"  OnClick="LnkDetails_OnClick1" Style="color: gren;
                                color: blue;" Text="Employee wise Report"></asp:LinkButton>
                        </li>
                         
                         
                      

                    </ul>
                </div>
            </div>
        </div>
        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12" >
            <div class="thumbnail" style="background-color: #f5f5f5; float: left; width: 100%;">
                <div class="panel panel-default">
                    <div class="form-horizontal">
                        <div class="row">
                            <button type="button" id="ton" class="btn btn-primary" style="float: right; position: relative;
                                right: 1px;">
                                <i class="fa fa-bars"></i>
                            </button>
                            <asp:HiddenField ID="hdnbtnValue" runat="server" />
                            <div id="div-show" style="display: block; float: right; width: calc(100% - 20px);
                                margin: 0px 10px; position: relative; top: -8px;">
                                <div class="row marg search-bg" style=" padding-bottom: 10px; ">
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <asp:UpdatePanel runat="server" ID="Upnl" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-sm-3 ">
                                                            <label for="email"  style="padding-top: 3px;">
                                                                 Assessment Type:</label>
                                                           <asp:DropDownList ID="ddlLevel" runat="server" TabIndex="1" CssClass="form-control input-sm" Style="margin-top: 0px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                                    </asp:DropDownList>
                                                           
                                                        </div>
                                                        <div class="col-sm-4 ">
                                                            <label for="email"  style="padding-top: 1px;">
                                                                Survey</label>
                                                               <asp:DropDownList ID="ddlForm" runat="server" CssClass="form-control input-sm" Style="margin-top: 0px"
                                                                      >
                                                                </asp:DropDownList>
                                                        </div>
                                                    
                                                        
                                                        
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 table table-hover " style="padding: 0px 10px;">
                                <asp:Panel ID="pnlMain" Enabled="true" runat="server">
                                    <div class="form-horizontal">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                            <div class="panel-default search-bg" style="height: 30px">
                                                <span style="float: left; color: Black; margin-left: 12px;">
                                                    <asp:Label ID="lblTotal" Text="Total:" runat="server"></asp:Label>
                                                </span><span style="float: left; color: Black; margin-left: 12px;"></span>
                                            </div>
                                            <asp:Label ID="lblTotalCount" Visible="false" ForeColor="#737272" Font-Bold="true"
                                                runat="server"></asp:Label>
                                            <div style="height: 450px; overflow: auto; width: 99%;" align="center">
                                                <div>
                                                    <div class="row" style="width: 100%">
                                                     
                                                        <asp:GridView ID="GVChildTarget" AutoGenerateColumns="true" runat="server" ForeColor="Black"
                                                            AllowPaging="true" PageSize="100" ShowHeader="true" CssClass="table table-striped table-bordered table-hover"
                                                            Width="100%">
                                                            <EmptyDataTemplate>
                                                            </EmptyDataTemplate>
                                                            <FooterStyle CssClass="FooterStyle" />
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                            <RowStyle HorizontalAlign="Left" />
                                                            <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                            <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#C1C1C1" Wrap="true" ForeColor="White" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                            <AlternatingRowStyle BackColor="#f1f1f1" />
                                                            <Columns>
                                                                
                                                            </Columns>
                                                            <PagerStyle CssClass="pagination-ys" />
                                                        </asp:GridView>
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
        </div>
    </div>
    </ContentTemplate>
           <Triggers>
            <asp:PostBackTrigger ControlID="LinkButton8" />
               <asp:PostBackTrigger ControlID="LinkButton1" />
       <asp:PostBackTrigger ControlID="LinkButton2" />
     
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
