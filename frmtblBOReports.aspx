<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Culture="en-GB" CodeFile="frmtblBOReports.aspx.cs" Inherits="frmtblBOReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .mapaddress {
            padding: 5px;
            border-style: solid;
            border-width: thin;
        }

        ui-corner-all, .ui-corner-bottom, .ui-corner-right, .ui-corner-br {
            -moz-border-radius-bottomright: 6px;
            -webkit-border-bottom-right-radius: 6px;
            -khtml-border-bottom-right-radius: 6px;
            border-bottom-right-radius: 6px;
        }

        .ui-corner-all, .ui-corner-bottom, .ui-corner-left, .ui-corner-bl {
            -moz-border-radius-bottomleft: 6px;
            -webkit-border-bottom-left-radius: 6px;
            -khtml-border-bottom-left-radius: 6px;
            border-bottom-left-radius: 6px;
        }

        .ui-corner-all, .ui-corner-top, .ui-corner-right, .ui-corner-tr {
            -moz-border-radius-topright: 6px;
            -webkit-border-top-right-radius: 6px;
            -khtml-border-top-right-radius: 6px;
            border-top-right-radius: 6px;
        }

        .ui-corner-all, .ui-corner-top, .ui-corner-left, .ui-corner-tl {
            -moz-border-radius-topleft: 6px;
            -webkit-border-top-left-radius: 6px;
            -khtml-border-top-left-radius: 6px;
            border-top-left-radius: 6px;
        }

        .ui-widget-content {
            border: 1px solid #cbaeae;
            background: #ffffff url(images/ui-bg_flat_75_ffffff_40x100.png) 50% 50% repeat-x;
            color: #333333;
        }

        .ui-widget {
            font-family: Arial,sans-serif;
            font-size: 1.1em;
        }

        .ui-dialog {
            position: absolute;
            padding: .2em;
            width: 300px;
            overflow: hidden;
        }

            .ui-dialog .ui-dialog-titlebar {
                padding: .4em 1em;
                position: relative;
            }

            .ui-dialog .ui-dialog-title {
                float: left;
                margin: .1em 16px .1em 0;
            }

            .ui-dialog .ui-dialog-titlebar-close {
                position: absolute;
                right: .3em;
                top: 50%;
                width: 19px;
                margin: -10px 0 0 0;
                padding: 1px;
                height: 18px;
            }

                .ui-dialog .ui-dialog-titlebar-close span {
                    display: block;
                    margin: 1px;
                }

                .ui-dialog .ui-dialog-titlebar-close:hover, .ui-dialog .ui-dialog-titlebar-close:focus {
                    padding: 0;
                }

            .ui-dialog .ui-dialog-content {
                position: relative;
                border: 0;
                padding: .5em 1em;
                background: none;
                overflow: auto;
                zoom: 1;
            }

            .ui-dialog .ui-dialog-buttonpane {
                text-align: left;
                border-width: 1px 0 0 0;
                background-image: none;
                margin: .5em 0 0 0;
                padding: .3em 1em .5em .4em;
            }

                .ui-dialog .ui-dialog-buttonpane .ui-dialog-buttonset {
                    float: right;
                }

                .ui-dialog .ui-dialog-buttonpane button {
                    margin: .5em .4em .5em 0;
                    cursor: pointer;
                }

            .ui-dialog .ui-resizable-se {
                width: 14px;
                height: 14px;
                right: 3px;
                bottom: 3px;
            }

        .ui-draggable .ui-dialog-titlebar {
            cursor: move;
        }
    </style>

    <%--<link href="css/jquery-ui-1.8.23.custom.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="js/jquery-ui-1.8.23.custom.min.js"></script>
    <script src="js/form_reports.js" type="text/javascript"></script>
    <script src="js/reports.js" type="text/javascript"></script>
    <script src="js/jquery1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDR0GgSSsXk011JN-ERbymQ2P4ec-ykp_E&sensor=true">
      
    </script>



    <script type="text/javascript">
        function show(elementId) {
            document.getElementById("mapviewer").style.display = "none";
            document.getElementById("MainDiv").style.display = "none";

        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#MainDiv").hide();
            $("#mapviewer").hide();
            $("#mapcanv").hide();

        });
    </script>


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
            width: 150px !important;
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




    <style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%-- <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>--%>
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="panel panel-default " style="background-color: #f5f5f5; margin-bottom: 0px !important;">
                    <div class="panel-heading" style="padding: 5px 0px;">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                <div id="Div2" class="col-lg-10 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                    <div class="">
                                        <h3 class="text-danger" style="margin: 0px;">Employee Tracking BO
                                        </h3>
                                    </div>
                                </div>
                                <div id="Div3" class="col-lg-2 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                    <div class="form-group">
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Export to Excel" OnClick="btnImport_Click"
                                            class="pull-right"></asp:LinkButton>
                                        <%--</div>
                                           
                                           <span class="pull-right" style="font-size: 17px;"></span>
                                        <div id="Div1" class="col-lg-2 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                        <div class="form-group">--%>
                                        <asp:LinkButton ID="lnkCSV" runat="server" Text="Export to CSV" Visible="false" OnClick="btnCSV_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 text-left" style="padding-right: 0px; margin-top: 10px">
            <div class="thumbnail" style="background-color: rgba(20, 18, 18, 0.59); float: left; width: 210px">
                <%--    <div class="li-width" style="min-height: 110px;">
                    <img src="images/business-report.jpg" width="100%" />--%>
                <%-- <div style="width:30%; float:left;">
            <img src="images/report-icon.gif" width="100%" />
        </div>
        <div style="width:70%; float:left; height:100%; background-color:Blue; " >
            Reports
            </div>--%>
                <%--  </div>--%>
                <ul class="nav navbar-nav" style="margin: 0px">
                    <li class=" active li-width">
                        <asp:LinkButton ID="Button3" runat="server" Text="Employee Tracking " Style="color: white;"
                            OnClick="btnSerach_Click"></asp:LinkButton>
                    </li>

                </ul>
            </div>
        </div>
        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12" style="padding-left: 10px; margin-top: 10px">
            <div class="thumbnail" style="background-color: #f5f5f5; float: left; width: 100%;">
                <div class="panel panel-default" style="margin-bottom: 0px">
                    <div class="form-horizontal">
                        <div class="row">
                            <div id="div-show-new">
                                <div class="row marg search-bg">
                                    <div class="form-horizontal">
                                        <%-- <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>--%>
                                        <div class="row">
                                            <div class="col-lg-2 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Year:</label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                            AutoPostBack="true" class="form-control ">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        User:
                                                    </label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlUser" runat="server" class="form-control ">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        From</label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:TextBox runat="server" ID="txtDate" class="form-control" onkeypress="return false;"></asp:TextBox>
                                                        <ajax:CalendarExtender ID="CalendarExtenderTourdate" runat="server" Enabled="True"
                                                            Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight">
                                                        </ajax:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        To</label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:TextBox runat="server" ID="txtTodate" class="form-control" onkeypress="return false;"></asp:TextBox>
                                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            TargetControlID="txtTodate" PopupPosition="BottomRight">
                                                        </ajax:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-8 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="col-lg-4 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            State:</label>
                                                        <div class="col-sm-8 padd CheckBoxListCssClass " style="border: 1px solid #c1c1c1">

                                                            <div style="overflow: auto; margin-top: 2px; height: 125px;">
                                                                <asp:CheckBoxList ID="ChkState" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                                </asp:CheckBoxList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            District:</label>
                                                        <div class="col-sm-8 padd CheckBoxListCssClass " style="border: 1px solid #c1c1c1">
                                                            <div style="overflow: auto; margin-top: 2px; height: 125px;">
                                                                <asp:CheckBoxList ID="chkDistrict" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                                </asp:CheckBoxList>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-lg-4 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Block:</label>
                                                        <div class="col-sm-8 padd CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                            <div style="overflow: auto; margin-top: 2px; height: 125px;">
                                                                <asp:CheckBoxList ID="chkBlock" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                                </asp:CheckBoxList>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <%-- <div class="row">
                        <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" style="padding-right:0px;">1</div>
                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" style="padding-right:0px;">2</div>
                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" style="padding-right:0px;">3</div>
                            </div>
                        </div>
                        <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding-right:0px;">4</div>
                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding-right:0px;">5</div>
                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding-right:0px;">6</div>
                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding-right:0px;">7</div>
                            </div>
                        </div>
                    </div>--%>
                                        <%--</ContentTemplate>
</asp:UpdatePanel>
                                        --%>
                                    </div>
                                </div>
                                <div class="col-lg-12 table table-hover " style="padding: 0px;">
                                    <asp:Panel ID="pnlMain" Enabled="true" runat="server">
                                        <div class="form-horizontal">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                <div class="panel-default search-bg" style="height: 30px">
                                                    <span style="float: left; color: Black; margin-left: 12px;">
                                                        <asp:Label ID="lblTotal" Text="Total:" runat="server"></asp:Label>
                                                    </span><span style="float: left; color: Black; margin-left: 12px;">
                                                        <asp:Label ID="lblTotalCount" ForeColor="#737272" Font-Bold="true" runat="server"></asp:Label>
                                                    </span>
                                                </div>
                                                <div style="height: 290px; overflow: auto; width: 99%;" align="center">
                                                    <div>
                                                        <div class="Row" style="width: 100%">
                                                            <asp:GridView ID="gvD2d" runat="server" OnPageIndexChanging="gvD2d_PageIndexChanging"
                                                                AllowPaging="true" PageSize="100" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False"
                                                                Font-Names="Arial" Font-Size="12px" Width="100%">
                                                                <EmptyDataTemplate>
                                                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                        Data not found
                                                                    </div>
                                                                </EmptyDataTemplate>
                                                                <FooterStyle CssClass="FooterStyle" />
                                                                <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                <PagerStyle CssClass="paging" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="User Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDistDate" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Employee Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDistddDate" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("FristName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Activity Date" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDistrDatee" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("Date") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="District Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDistrictName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("DistrictName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Block Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBlockName" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("BlockName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Panchayat Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPanchayatName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("PanchayatName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Village Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVillageName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("VillageName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Start Time" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lUniqueId" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Starttime") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Start Time Location" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSurvayDate" class="labelGrid" ForeColor="Black" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="End Time" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHouse" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Endtime") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="End Time Location" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHouse2" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("EndtimeLocation") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Hours" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHours" class="labelGrid" ForeColor="Black" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%# Eval("TotalHours") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false" HeaderText="Start Time Location">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStarttimeLocation" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("StarttimeLocation") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField Visible="false" HeaderText="Start Time Location">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEndtimeLocation" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("EndtimeLocation") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false" HeaderText="Start Time Location">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVillage_GeoLocation" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("Village_GeoLocation") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField Visible="false" HeaderText="Start Time Location">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTimeSheet_StartTime" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("TimeSheet_StartTime") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField Visible="false" HeaderText="Start Time Location">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTimeSheet_EndTime" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("TimeSheet_EndTime") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="User  District">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTittmeSheet_EndTime" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("DistName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                </Columns>
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
                <!-- /#wrapper -->
                <!-- /#wrapper -->
            </div>
        </div>
    </div>
    <div id="MainDiv" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable " style="display: block; z-index: 1002; outline: 0px none; height: auto; width: 410px; top: 112.5px; left: 464.5px;" tabindex="-1" aria-labelledby="ui-dialog-title-mapviewer" role="dialog">
        <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">

            <div id="mapviewer" title="Map view">
                <p>
                    <span style="float: right">

                        <button type="button" onclick="show('mapviewer');">Close</button>
                    </span>
                    <div id="mapcanv" style="width: 400px; height: 300px">
                    </div>
                    <div class="mapaddress" id="mapaddress">
                        Address
                    </div>
                </p>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker4').datetimepicker();
        });
    </script>
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
