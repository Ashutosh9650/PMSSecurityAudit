<%@ Page Title="" Language="C#" AutoEventWireup="true" Culture="en-GB" EnableEventValidation="false" MasterPageFile="~/Site.master" CodeFile="FrmPayment.aspx.cs" Inherits="FrmPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <script type="text/javascript" src="js/jquery-2.1.0.js"></script>
    <script src="js/Common.js" type="text/javascript"></script>
    <style type="text/css">
        .ajax__calendar_container {
            z-index: 100004;
        }
    </style>
    <style>
        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a, .pagination-ys table > tbody > tr > td > span {
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

                .pagination-ys table > tbody > tr > td > span {
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

                .pagination-ys table > tbody > tr > td:first-child > a, .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a, .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover, .pagination-ys table > tbody > tr > td > span:hover, .pagination-ys table > tbody > tr > td > a:focus, .pagination-ys table > tbody > tr > td > span:focus {
                    color: Black;
                    background-color: #eeeeee;
                    border-color: #dddddd;
                }
    </style>
    <style type="text/css">
        .multiselect.dropdown-toggle.btn.btn-default > div.restricted {
            margin-right: 5px;
            max-width: 100px;
            overflow: hidden;
        }
    </style>
    <style type="text/css">
        .radio .cr {
            border-radius: 75%;
            border-color: #333;
        }

            .checkbox .cr .cr-icon, .radio .cr .cr-icon {
                position: absolute;
                font-size: .8em;
                line-height: 0;
                top: 50%;
                left: 15%;
            }

            .radio .cr .cr-icon {
                margin-left: 0.04em;
            }

        .checkbox label input[type="checkbox"], .radio label input[type="radio"] {
            display: none;
        }

            .checkbox label input[type="checkbox"] + .cr > .cr-icon, .radio label input[type="radio"] + .cr > .cr-icon {
                transform: scale(3) rotateZ(-220deg);
                opacity: 0;
                transition: all .7s ease-in;
            }

            .checkbox label input[type="checkbox"]:checked + .cr > .cr-icon, .radio label input[type="radio"]:checked + .cr > .cr-icon {
                transform: scale(1) rotateZ(0deg);
                opacity: 1;
            }

            .checkbox label input[type="checkbox"]:disabled + .cr, .radio label input[type="radio"]:disabled + .cr {
                opacity: .5;
            }

        .new-navbutt {
            float: left !important;
            margin-top: 0px !important;
        }

        .row-border {
            border-bottom: 1px dotted rgb(221, 221, 221);
            margin-bottom: 15px;
        }

        .checkbox {
            position: relative;
            display: block;
            margin-top: 2px !important;
            margin-bottom: 5px !important;
        }


        .new-navbutt {
            float: left !important;
            margin-top: 0px !important;
        }

        .row-border {
            border-bottom: 1px dotted rgb(221, 221, 221);
            margin-bottom: 15px;
        }

        .checkbox .cr .cr-icon, .radio .cr .cr-icon {
            position: absolute;
            font-size: .8em;
            line-height: 0;
            top: 50%;
            left: 15%;
        }

        .radio .cr .cr-icon {
            margin-left: 0.04em;
        }

        .checkbox label input[type="checkbox"], .radio label input[type="radio"] {
            display: none;
        }

            .checkbox label input[type="checkbox"] + .cr > .cr-icon, .radio label input[type="radio"] + .cr > .cr-icon {
                transform: scale(3) rotateZ(-220deg);
                opacity: 0;
                transition: all .7s ease-in;
            }

            .checkbox label input[type="checkbox"]:checked + .cr > .cr-icon, .radio label input[type="radio"]:checked + .cr > .cr-icon {
                transform: scale(1) rotateZ(0deg);
                opacity: 1;
            }

            .checkbox label input[type="checkbox"]:disabled + .cr, .radio label input[type="radio"]:disabled + .cr {
                opacity: .5;
            }

        .new-navbutt {
            float: left !important;
            margin-top: 0px !important;
        }

        .row-border {
            border-bottom: 1px dotted rgb(221, 221, 221);
            margin-bottom: 15px;
        }

        .checkbox {
            position: relative;
            display: block;
            margin-top: 2px !important;
            margin-bottom: 5px !important;
        }

        .CheckBoxListCssClass {
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

        .CheckBoxListCssClassNew {
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

        .checkboxlist {
            position: absolute;
            font-size: .8em;
            margin-left: 10px;
            line-height: 0;
            top: 50%;
            left: 15%;
        }

        .td-widt {
            width: auto !important;
        }

        .td-width1 {
            width: 100px !important;
        }

        @media (min-width:10px) and (max-width:640px) {
            .td-widt {
                width: 90px !important;
            }


            .td-width1 {
                width: 90px !important;
            }
        }

        .table-mb {
            margin-bottom: 2px !important;
        }

        .thnail {
            padding: 0px !important;
            border-radius: 0px !important;
            margin-bottom: 0px !important;
            min-height: 60px;
        }

        .thumbnail ul {
            float: left;
            width: 100%;
            height: auto;
            margin: 0px;
            padding: 0px;
            list-style: none;
        }

            .thumbnail ul li {
                float: left;
                width: 100%;
                height: auto;
            }

                .thumbnail ul li a {
                    float: left;
                    width: 100%;
                    height: auto;
                    padding: 10px;
                    border: 1px solid #ddd;
                }
    </style>
    <style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
        }

        .modalpopupcss {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }

        .modalPopup {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
            z-index: 999999;
        }


        input[type="radio"], input[type="checkbox"] {
            margin: 4px 7px 0px !important;
            margin-top: 1px !important;
            line-height: normal !important;
        }

        .gridnewheadercss {
            color: #ffffff;
            vertical-align: middle;
            background-color: #81AB81;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%-- <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate--%>
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="panel panel-default " style="background-color: #f5f5f5; margin-bottom: 0px !important;">
                    <div class="panel-heading" style="padding: 5px 15px;">
                        <div class="row">
                            <div id="Div" class="col-lg-10 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                <div class="">
                                    <h3 class="text-danger" style="margin: 0px;">Team Balika Payment Sheet
                                    </h3>
                                </div>
                            </div>
                            <%-- <asp:LinkButton ID="btnexcel"  runat="server" Text="Export to Excel" ></asp:LinkButton>--%>

                            <button type="button" id="ton" class="btn btn-primary" style="float: right; position: relative; right: 1px;">
                                <i class="fa fa-bars"></i>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 text-left" style="padding-right: 0px; margin-top: 10px">
            <div style="overflow: auto; margin-top: 0px; height: 770px;">
                <div class="thumbnail" style="height: 750PX;">
                    <ul style="margin: 0px">
                        <li class=" active li-width">
                            <asp:LinkButton ID="Button3" runat="server" Text="Payment Report " Style="color: green;" OnClick="PMS_Click"></asp:LinkButton>
                        </li>

                    </ul>
                </div>
            </div>
        </div>
        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12" style="padding-left: 10px; margin-top: 10px">
            <div class="thumbnail" style="background-color: #f5f5f5; float: left; width: 100%;">
                <div class="panel panel-default" style="margin-bottom: 0px">
                    <div class="form-horizontal">
                        <div class="row">

                            <asp:HiddenField ID="hdnbtnValue" runat="server"/>
                            <div id="div-show" style="display: block; float: right; width: calc(100% - 20px); margin: 0px 10px; position: relative; top: 0px;">
                                <div class="row marg search-bg">
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <asp:UpdatePanel runat="server" ID="Upnl" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-sm-2 ">
                                                            <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                                Year</label>
                                                            <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                                AutoPostBack="true" class="form-control ">
                                                            </asp:DropDownList>

                                                            <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                                From Month:</label>
                                                            <asp:DropDownList ID="ddlFMonth" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="01">Jan </asp:ListItem>
                                                                <asp:ListItem Value="02">Feb </asp:ListItem>
                                                                <asp:ListItem Value="03">Mar</asp:ListItem>
                                                                <asp:ListItem Value="04">Apr</asp:ListItem>
                                                                <asp:ListItem Value="05">May</asp:ListItem>
                                                                <asp:ListItem Value="06">Jun</asp:ListItem>
                                                                <asp:ListItem Value="07">Jul</asp:ListItem>
                                                                <asp:ListItem Value="08">Aug</asp:ListItem>
                                                                <asp:ListItem Value="09">Sep</asp:ListItem>
                                                                <asp:ListItem Value="10">Oct</asp:ListItem>
                                                                <asp:ListItem Value="11">Nov</asp:ListItem>
                                                                <asp:ListItem Value="12">Dec</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <label for="email" class="padd linhei" style="padding-top: 5px;">
                                                                To Month:</label>
                                                            <asp:DropDownList ID="ddlToMonth" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="01">Jan </asp:ListItem>
                                                                <asp:ListItem Value="02">Feb </asp:ListItem>
                                                                <asp:ListItem Value="03">Mar</asp:ListItem>
                                                                <asp:ListItem Value="04">Apr</asp:ListItem>
                                                                <asp:ListItem Value="05">May</asp:ListItem>
                                                                <asp:ListItem Value="06">Jun</asp:ListItem>
                                                                <asp:ListItem Value="07">Jul</asp:ListItem>
                                                                <asp:ListItem Value="08">Aug</asp:ListItem>
                                                                <asp:ListItem Value="09">Sep</asp:ListItem>
                                                                <asp:ListItem Value="10">Oct</asp:ListItem>
                                                                <asp:ListItem Value="11">Nov</asp:ListItem>
                                                                <asp:ListItem Value="12">Dec</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2 ">
                                                            <label for="email" class="padd linhei" style="padding-top: 1px;">
                                                                State</label>
                                                            <div class="padd CheckBoxListCssClass " style="border: 1px solid #c1c1c1">
                                                                <div style="overflow: auto; margin-top: 1px; height: 150px;">
                                                                    <asp:CheckBoxList ID="ChkState" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                                        AutoPostBack="true" runat="server">
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2 " style="margin-bottom: 15px">
                                                            <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                                District</label>
                                                            <div class="CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                                <div style="overflow: auto; margin-top: 1px; height: 150px;">
                                                                    <asp:CheckBoxList ID="chkDistrict" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                                        AutoPostBack="true" runat="server">
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2 ">
                                                            <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                                Block</label>
                                                            <div class="CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                                <div style="overflow: auto; margin-top: 2px; height: 150px;">
                                                                    <asp:CheckBoxList ID="chkBlock" RepeatDirection="Vertical"
                                                                        AutoPostBack="true" runat="server">
                                                                    </asp:CheckBoxList>
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

                            <div class="col-lg-12 table table-hover " style="padding: 0px 10px 10px 10px;">
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


</asp:Content>

