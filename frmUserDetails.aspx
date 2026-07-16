<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Culture="en-GB" CodeFile="frmUserDetails.aspx.cs" Inherits="frmUserDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <%--<script src="https://maps.googleapis.com/maps/api/js?v=3.11&sensor=false" type="text/javascript"></script>--%>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDR0GgSSsXk011JN-ERbymQ2P4ec-ykp_E&sensor=true&libraries=places">  </script>



    <script src="https://code.jquery.com/jquery-3.1.1.min.js"></script>
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/highcharts-more.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <style type="text/css">
        .map {
            height: 50%;
            width: 90%;
            position: absolute !important;
        }
        /* Optional: Makes the sample page fill the window. */
        html, body {
            height: 100%;
            margin: 0;
            padding: 0;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker4').datetimepicker();
        });
    </script>

    <style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate--%>
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="panel panel-default " style="background-color: #f5f5f5; margin-bottom: 0px !important;">
                    <div class="panel-heading" style="padding: 2px 0px;">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                <div id="Div2" class="col-lg-10 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                    <div>
                                        <h3 class="text-danger" style="margin: 3px;">User Detail Report
                                        </h3>
                                    </div>
                                </div>
                                <div id="Div3" class="col-lg-2 col-md-2 col-sm-3 cpl-xs-12" runat="server" style="margin: 8px 0px 0px 0px;">
                                    <div class="">
                                        <asp:LinkButton ID="LinkButton1" Visible="false" runat="server" Text="Export to Excel" OnClick="btnImport_Click"
                                            class="pull-right"></asp:LinkButton>
                                        <%--</div>
                                         
                                           <span class="pull-right" style="font-size: 17px;"></span>
                                        <div id="Div1" class="col-lg-2 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                        <div class="form-group">--%>
                                        <asp:LinkButton ID="lnkCSV" runat="server" class="pull-right" Text="Export to CSV" OnClick="btnCSV_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 text-left" style="padding-right: 0px; margin-top: 10px">
            <div style="overflow: auto; margin-top: 0px; height: 480px;">
                <div class="thumbnail" style="background-color: rgba(20, 18, 18, 0.59); float: left;">
                   <%-- <div class="li-width" style="min-height: 110px;">
                        <img src="images/business-report.jpg" width="100%" />--%>
                        <%-- <div style="width:30%; float:left;">
            <img src="images/report-icon.gif" width="100%" />
        </div>
        <div style="width:70%; float:left; height:100%; background-color:Blue; " >
            Reports
            </div>--%>
                   <%-- </div>--%>
                    <ul class="nav navbar-nav" style="margin: 0px">
                        <li class=" active li-width">
                            <asp:LinkButton ID="Button3" runat="server" Visible="false" Text="UserWise Entry Summary "
                                Style="color: white;" OnClick="btnSerach_Click"></asp:LinkButton>
                        </li>
                        <li class="li-width">
                            <asp:LinkButton ID="ff" runat="server" Visible="false" Text="UserWise Entry detail"
                                Style="color: white;" OnClick="btnUser_Click"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="Button4" runat="server" Text="Door to Door" Visible="false" Style="color: white;"
                                OnClick="btnD2d_Click"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="LinkButton5" runat="server" Visible="false" Text="Out of  Door to Door" Style="color: white;"
                                OnClick="btnOuterD2d_Click"></asp:LinkButton></li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton2" Visible="false" runat="server" Text="Enrollment User Summary "
                                Style="color: white;" OnClick="btnEnroll_Click"></asp:LinkButton>
                        </li>
                        <li class="li-width">
                            <asp:LinkButton ID="LinkButton3" Visible="false" runat="server" Text="Enrollment UserWise detail"
                                Style="color: white;" OnClick="btnUserDeatils_Click"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="LinkButton4" Visible="false" runat="server" Text="Enrollment"
                                Style="color: white; font-size: 15px;" OnClick="btnEnrolllment_Click"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="LinkButton9" Visible="false" runat="server" Text="Ineligible"
                                Style="color: white; font-size: 15px;" OnClick="btnInEligible_Click"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="LinkButton12" Visible="false" runat="server" Text="Enrollment Daily status"
                                Style="color: white;" OnClick="LnkMobileDataReport_OnClick"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="LinkButton8" Visible="false" runat="server" Text="Learning Baseline"
                                Style="color: white;" OnClick="btnLearningBaseline_Click"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="LinkButton15" Visible="false" runat="server" Text="Learning Baseline(IO)"
                                Style="color: white;" OnClick="btnLearningBaselineIO_Click"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="LinkButton18" Visible="false" runat="server" Text="Learning Baseline(IO) Endline"
                                Style="color: white;" OnClick="btnLearningBaselineIOEnd_Click"></asp:LinkButton></li>

                        <li class="li-width">
                            <asp:LinkButton ID="LinkButton13" Visible="false" runat="server" Text="Retention Individual"
                                Style="color: white;" OnClick="btnRetention_Click"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="LinkButton14" runat="server" Visible="false" Text="Retention Aggregate" Style="color: white;"
                                OnClick="Retention_Click"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="LinkButton11" Visible="false" runat="server" Text="SIP Detail"
                                Style="color: white;" OnClick="btnSipdetail_Click"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="LinkButton16" Visible="false" runat="server" Text="Re-Enrollment Detail"
                                Style="color: white;" OnClick="btnReo_Click"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="LinkButton17" Visible="false" runat="server" Text="Re-Enrollment Difference"
                                Style="color: white;" OnClick="btnReoe_Click"></asp:LinkButton></li>


                        <li class="li-width">
                            <asp:LinkButton ID="LnkMasterDate" Visible="false" runat="server" Text="Location Master"
                                Style="color: white;" OnClick="LnkMasterData_OnClick"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="LnkTeamBalika" Visible="false" runat="server" Text="Team Balika"
                                Style="color: white;" OnClick="LnkTeamBalika_OnClick"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="LinkButton10" Visible="false" runat="server" Text="Team Balika Training"
                                Style="color: white;" OnClick="LnkTeamBalikaTraining_OnClick"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="LnkUserRole" Visible="true" runat="server" Text="User Details"
                                Style="color: white;" OnClick="LnkUserRole_OnClick"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="LinkButton6" Visible="true" runat="server" Text="Village Tagging Report"
                                Style="color: white;" OnClick="village_Profile_Click"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="LinkButton7" Visible="false" runat="server" Text="SIC Baseline Data"
                                Style="color: white;" OnClick="SIC_Data_Click"></asp:LinkButton></li>

                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton19" runat="server" Visible="false" Text="Employee Exceptions " Style="color: white;"
                                OnClick="btnEmployee_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton20" runat="server" Visible="false" Text="Employee Visit Count " Style="color: white;"
                                OnClick="btnVisit_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton21" runat="server" Visible="false" Text="Map " Style="color: white;"
                                OnClick="BtnShow_OnClick"></asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12" style="padding-left: 10px; margin-top: 10px">
            <div class="thumbnail" style="background-color: #f5f5f5; float: left; width: 100%;">
                <div class="panel panel-default">
                    <div class="form-horizontal">
                        <div class="row">
                            <div style="padding: 0px 10px 0px 10px;">
                                <div class="row marg search-bg" style="padding-left: 5px;">
                                    <div class="form-horizontal">

                                        <div class="row">

                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Block Type:</label>
                                                    <div class="col-sm-8 padd ">
                                                        <asp:RadioButtonList ID="rblBlockType" AutoPostBack="true" OnSelectedIndexChanged="rblBlockType_SelectedIndexChanged" CssClass="cr-icon" ForeColor="Black" RepeatDirection="Horizontal" runat="server">
                                                            <asp:ListItem Text="EG Block" Selected="True" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Govt Block" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Year:</label>
                                                    <div class="col-sm-9 padd" style="padding: 0px 10px 0px 14px;">
                                                        <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                            AutoPostBack="true" class="form-control ">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        State:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass " style="border: 1px solid #c1c1c1">

                                                        <div style="overflow: auto; margin-top: 2px; height: 50px;">
                                                            <asp:CheckBoxList ID="ChkState" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        District:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass " style="border: 1px solid #c1c1c1">
                                                        <div style="overflow: auto; margin-top: 2px; height: 50px;">
                                                            <asp:CheckBoxList ID="chkDistrict" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>

                                        <div class="row">
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Block:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                        <div style="overflow: auto; margin-top: 2px; height: 50px;">
                                                            <asp:CheckBoxList ID="chkBlock" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div1" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Panchayat:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                        <div style="overflow: auto; margin-top: 2px; height: 50px;">
                                                            <asp:CheckBoxList ID="ddlPanchayat" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Village:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                        <div style="overflow: auto; margin-top: 2px; height: 50px;">
                                                            <asp:CheckBoxList ID="chkVillage" RepeatDirection="Vertical" runat="server">
                                                            </asp:CheckBoxList>

                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        User:
                                                    </label>
                                                    <div class="col-sm-9 padd" style="padding: 0px 10px 0px 14px;">
                                                        <asp:DropDownList ID="ddlUser" runat="server" class="form-control ">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        From</label>
                                                    <div class="col-sm-9 padd" style="padding: 0px 10px 0px 14px;">
                                                        <asp:TextBox runat="server" ID="txtDate" class="form-control" onkeypress="return false;"></asp:TextBox>
                                                        <ajax:CalendarExtender ID="CalendarExtenderTourdate" runat="server" Enabled="True"
                                                            Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight">
                                                        </ajax:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        To</label>
                                                    <div class="col-sm-9 padd" style="padding: 0px 10px 0px 14px;">
                                                        <asp:TextBox runat="server" ID="txtTodate" class="form-control" onkeypress="return false;"></asp:TextBox>
                                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            TargetControlID="txtTodate" PopupPosition="BottomRight">
                                                        </ajax:CalendarExtender>
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
                                                <div class="thumbnail" style="min-height: 750px; overflow: auto;" id="MapId" runat="server" visible="false">


                                                    <div id="mapcanv" class="map">
                                                    </div>
                                                    <asp:Literal runat="server" ID="LitChrtDistWise"></asp:Literal>
                                                </div>
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
                                                            <asp:GridView ID="gvUserReport" Visible="false" CssClass="table table-striped table-bordered table-hover"
                                                                Width="100%" ShowFooter="true" runat="server" AutoGenerateColumns="false">
                                                                <EmptyDataTemplate>
                                                                </EmptyDataTemplate>
                                                                <FooterStyle CssClass="FooterStyle" />
                                                                <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                                <RowStyle HorizontalAlign="Left" />
                                                                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="State Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStateName1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("StateName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="District Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDistrictName1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("DistrictName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="User Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblName" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Role">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRole" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Role") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="12%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Create Date">
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblphoneN5" Font-Names="Calibri" ForeColor="Black" runat="server"
                                                                                Text='<%#Eval("CreateDate") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="HeaderStyle GridHeaderClass" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Records added">
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAddress1" Font-Names="Calibri" ForeColor="Black" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CountCreate") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="HeaderStyle GridHeaderClass" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:GridView ID="GvReport" Visible="false"
                                                                CssClass="table table-striped table-bordered table-hover" Width="100%" ShowFooter="true"
                                                                runat="server" AutoGenerateColumns="false">
                                                                <EmptyDataTemplate>
                                                                </EmptyDataTemplate>
                                                                <FooterStyle CssClass="FooterStyle" />
                                                                <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                                <RowStyle HorizontalAlign="Left" />
                                                                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="District Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDistrictName" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("DistrictName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Block Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBlockName" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("BlockName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Panchaya tName">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBPanchayatNamee" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PanchayatName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="LatLong">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblName" ForeColor="Black" ItemStyle-ForeColor="#333" Font-Names="Calibri"
                                                                                runat="server" Text='<%#Eval("Village_GeoLocation") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Exceptional Visit Count ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRole" ForeColor="Black" ItemStyle-ForeColor="#333" Font-Names="Calibri"
                                                                                runat="server" Text='<%#Eval("NumberExceptions") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="12%" />
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:GridView ID="gvD2d" runat="server" Visible="false" OnPageIndexChanging="gvD2d_PageIndexChanging"
                                                                AllowPaging="true" PageSize="100" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" Font-Names="Arial"
                                                                Font-Size="12px" Width="300%">
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
                                                                    <asp:TemplateField HeaderText="District Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDistrictName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("DistrictName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="District Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDistrictNaf1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("DistrictCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Block Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBlockName" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("BlockName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Block Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDistdrictNaf1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("BlockCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Panchayat Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPanchayatName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("PanchayatName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Panchayat Code" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPanchayatdName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("PanchayatCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Village Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVillageName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("VillageName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Village Code" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVillageName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("VillageCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Unique ID" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lUniqueId" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("UniqueId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Survey Date" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSurvayDate" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("SurveyDate") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Mauhalla" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMauhalla" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Mauhalla") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="House" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHouse" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("House") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Child Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHouse2" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("ChildName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Father Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ddlEmployeeCode" class="labelGrid" ForeColor="Black" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%# Eval("FathersName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Gender" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmpLWP" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Age" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Txtunit" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Age") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Age Proof" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHRA" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("AgeProof") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Social Category" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSalaryPayable" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("SocialCategory") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Family Occupation" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBasic" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("FamilyOccupation") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Eduation Status" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHRAyy" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("EduationStatus") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="School Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblConveyance" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("SchoolName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="SchoolLevel" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblConveyance" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("SchoolLevel") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Class" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAllowance" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Reason" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMedical" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Migration " Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblpfc" runat="server" class="labelGrid" ForeColor="Black" Text='<%# Eval("Migration") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Enrollment Category" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGrossSalary" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("EnrollmentCategory") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:GridView ID="gvnroll" CssClass="table table-striped table-bordered table-hover" runat="server" Visible="false" AllowPaging="true" PageSize="100"
                                                                OnPageIndexChanging="gvnroll_OnPageIndexChanging" AutoGenerateColumns="False"
                                                                Font-Names="Arial" Font-Size="12px" Width="200%">
                                                                <EmptyDataTemplate>
                                                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                        Data not found
                                                                    </div>
                                                                </EmptyDataTemplate>
                                                                <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="District Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDistrictName" ForeColor="Black" runat="server" Text='<%# Eval("DistrictName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="District Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDistrddictNaf1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("DistrictCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Block Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBlockName" ForeColor="Black" runat="server" Text='<%# Eval("BlockName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Block Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblssbf1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("BlockCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="ClusterName" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPanchggayatName" ForeColor="Black" runat="server" Text='<%# Eval("ClusterName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Panchayat Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPanchayatName" ForeColor="Black" runat="server" Text='<%# Eval("PanchayatName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Panchayat Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblssDisddtrddictNaf1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PanchayatCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Village Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVillageName" ForeColor="Black" runat="server" Text='<%# Eval("VillageName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Village Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVillage1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Villagecode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Unique ID" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSurvayD3ate" ForeColor="Black" runat="server" Text='<%# Eval("Uniqueid") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Flag" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSFlage" ForeColor="Black" runat="server" Text='<%# Eval("Flag") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="HHNo" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMauhalla2" ForeColor="Black" runat="server" Text='<%# Eval("HHNo1") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Student Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHouse2" ForeColor="Black" runat="server" Text='<%# Eval("ChildName1") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Father Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ddlEmployee2Code" class="labelGrid" ForeColor="Black" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%# Eval("FathersName1") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Class" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmp2LWP" ForeColor="Black" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SR. NO." Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Txtun2iqqt" ForeColor="Black" runat="server" Text='<%# Eval("Serial") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Admission Date" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHR3A" ForeColor="Black" runat="server" Text='<%# Eval("EnrolmentDate") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Social Category" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSalaryPayaeble" ForeColor="Black" runat="server" Text='<%# Eval("SocialCategory1") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Gender" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBasirrc" ForeColor="Black" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="DOB" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHRAyye" ForeColor="Black" runat="server" Text='<%# Eval("DOB1") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="School Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblConveyaence" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("School") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="DISECode" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCDISECodee" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("DISECode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="GovtDISECode" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblConveyaence" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("GovtDISECode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SchoolLevel" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCogence" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("SchoolLevel") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Enrollment Category" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAlflowaeecee" ForeColor="Black" runat="server" Text='<%# Eval("EnrolmentCategory") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Education Status" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMediecal" ForeColor="Black" runat="server" Text='<%# Eval("EduationStatus") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>


                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="width: 200%">
                                                        <asp:GridView ID="gvRetaion" AllowPaging="true" PageSize="30" runat="server" ForeColor="#333333" CellPadding="1" OnRowCreated="gvRetaion_RowCreated"
                                                            CssClass="table table-striped table table-hover table-bordered  " AutoGenerateColumns="true" Width="99.7%">

                                                            <EmptyDataTemplate>
                                                                <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                                                    Data not found
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="row" style="width: 200%">
                                                        <asp:GridView ID="GV_DynamicGrid" runat="server" ForeColor="Black" AllowPaging="true"
                                                            OnPageIndexChanging="GV_DynamicGrid_OnPageIndexChanging" PageSize="100" ShowHeader="true"
                                                            Visible="false" CssClass="table table-striped table-bordered table-hover" Width="100%">
                                                            <EmptyDataTemplate>
                                                            </EmptyDataTemplate>
                                                            <FooterStyle CssClass="FooterStyle" />
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                            <RowStyle HorizontalAlign="Left" />
                                                            <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                            <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                            <AlternatingRowStyle BackColor="#f1f1f1" />
                                                            <Columns>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="row" style="width: 250%">
                                                        <asp:GridView ID="GV_DynamicGrid1" runat="server" ForeColor="Black" AllowPaging="true"
                                                            OnPageIndexChanging="GV_DynamicGrid1_OnPageIndexChanging" PageSize="100" ShowHeader="true"
                                                            Visible="false" CssClass="table table-striped table-bordered table-hover" Width="100%">
                                                            <EmptyDataTemplate>
                                                            </EmptyDataTemplate>
                                                            <FooterStyle CssClass="FooterStyle" />
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                            <RowStyle HorizontalAlign="Left" />
                                                            <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                            <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                            <AlternatingRowStyle BackColor="#f1f1f1" />
                                                            <Columns>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="row" style="width: 100%">
                                                        <asp:GridView ID="GV_DynamicGrid2" runat="server" ForeColor="Black" AllowPaging="true"
                                                            OnPageIndexChanging="GV_DynamicGrid2_OnPageIndexChanging" PageSize="100" ShowHeader="true"
                                                            Visible="false" CssClass="table table-striped table-bordered table-hover" Width="100%">
                                                            <EmptyDataTemplate>
                                                            </EmptyDataTemplate>
                                                            <FooterStyle CssClass="FooterStyle" />
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                            <RowStyle HorizontalAlign="Left" />
                                                            <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                            <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                            <AlternatingRowStyle BackColor="#f1f1f1" />
                                                            <Columns>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="row" style="width: 100%">
                                                        <asp:GridView ID="gvvillageschoolgrid" runat="server" ForeColor="Black" AllowPaging="true"
                                                            PageSize="100" ShowHeader="true" Visible="false" CssClass="table table-striped table-bordered table-hover"
                                                            OnPageIndexChanging="gvvillageschoolgrid_pageindexchanging" Width="100%">
                                                            <EmptyDataTemplate>
                                                            </EmptyDataTemplate>
                                                            <FooterStyle CssClass="FooterStyle" />
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                            <RowStyle HorizontalAlign="Left" />
                                                            <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                            <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
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
    <%--</ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="LinkButton1" />
           <asp:PostBackTrigger ControlID="lnkCSV" />
             
                                           
            </Triggers>
  </asp:UpdatePanel>--%>
</asp:Content>
