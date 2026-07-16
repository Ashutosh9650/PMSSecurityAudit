<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="frmLogSummary.aspx.cs" Culture="en-GB" Inherits="frmLogSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker4').datetimepicker();
        });
    </script>

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
            margin-bottom: 16px;
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
            margin-bottom: 16px;
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
            margin-bottom: 16px;
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
                    color: #333;
                }

                    .thumbnail ul li a:hover {
                        text-decoration: none;
                        color: red !important;
                        background-color: #f1f1f1;
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
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                <div id="Div" class="col-lg-10 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                    <div class="">
                                        <h3 class="text-danger" style="margin: 0px;">Log Details Report
                                        </h3>
                                    </div>
                                </div>


                                <asp:LinkButton ID="LinkButton1" runat="server" Text="Export to CSV" OnClick="btnImport_Click"
                                    class="pull-right"></asp:LinkButton>

                            </div>

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
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class=" col-lg-3 col-md-3 col-sm-3 col-xs-12 text-left" style="padding-right: 0px; margin-top: 10px">
            <div style="overflow: auto; margin-top: 0px; height: 782px;">
                <div class="thumbnail" style="height: 1915px; margin-bottom: 1px;">

                    <ul style="margin: 0px">

                        <li>
                            <asp:LinkButton ID="Button3" runat="server" OnClick="LnkAnnualPlan_OnClick" Style="font-size: 16px; color: blue;" Text="Log Summary"></asp:LinkButton>
                        </li>

                        <li>
                            <asp:LinkButton ID="LinkButton38" runat="server" OnClick="LnkAgis_OnClick" Style="font-size: 16px; color: blue;" Text="GIS Summary"></asp:LinkButton>
                        </li>

                          <li>
                            <asp:LinkButton ID="LinkButton39" runat="server" OnClick="Lnkfc_OnClick" Style="font-size: 16px; color: blue;" Text="FC Weekly Plan"></asp:LinkButton>
                        </li>



                        <li>
                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LnkAnnualPlanFC_OnClick" Style="font-size: 16px; color: blue;" Text="User Registration Tracker"></asp:LinkButton>
                        </li>

                        <li>
                            <asp:Label ID="Label1" Text="--Contact Report---" runat="server" Font-Size="Medium" Style="color: Black;"></asp:Label></li>
                        <li>
                            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LnkAnnualPl_OnClick" Style="font-size: 16px; color: blue;" Text="Contact Summary"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LnkAnnualPl1_OnClick" Style="font-size: 16px; color: blue;" Text="Contact Quality Alert"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LnkAnnualPl2_OnClick" Style="font-size: 16px; color: blue;" Text="Contact Detail Report"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LnkAnnualPl3_OnClick" Style="font-size: 16px; color: blue;" Text="Enrollment Target Raw Data"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LnkAnnualPl4_OnClick" Style="font-size: 16px; color: blue;" Text="Contact Status Report"></asp:LinkButton>
                        </li>

                        <li>
                            <asp:Label ID="Label8" Text="--Enrolment trackers---" runat="server" Font-Size="Medium" Style="color: Black;"></asp:Label></li>

                        <li>
                            <asp:LinkButton ID="LinkButton32" runat="server" OnClick="LnkAnnualPl74_OnClick" Style="font-size: 16px; color: blue;" Text="Enrolment Details"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton33" runat="server" OnClick="LnkAnnualP84_OnClick" Style="font-size: 16px; color: blue;" Text="Enrolment Quality Alert"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton34" runat="server" OnClick="LnkAnnualP94_OnClick" Style="font-size: 16px; color: blue;" Text="Enrolment Summary"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LnkEnrollementHistory" runat="server" OnClick="LnkEnrollementHistory_OnClick" Style="font-size: 16px; color: blue;" Text="Enrolment Changes Log"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:Label ID="Label9" Text="--Mobile Reports Log---" runat="server" Font-Size="Medium" Style="color: Black;"></asp:Label></li>
                        <li>
                            <asp:LinkButton ID="LinkButton35" runat="server" OnClick="LnkAnnualPlan_OnClick2" Style="font-size: 16px; color: blue;" Text="Enrolment & CV summary log"></asp:LinkButton>

                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton37" runat="server" OnClick="LnkAnnualPlan_OnClick4" Style="font-size: 16px; color: blue;" Text="Enrolment & CV summary BO log"></asp:LinkButton>

                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton36" runat="server" OnClick="LnkAnnualPlan_OnClick3" Style="font-size: 16px; color: blue;" Text="Contact Summary log"></asp:LinkButton></li>
                        <li>
                            <asp:Label ID="Label2" Text="--Activity Report---" runat="server" Font-Size="Medium" Style="color: Black;"></asp:Label></li>

                        <li>
                            <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LnkAnnualPl5_OnClick" Style="font-size: 16px; color: blue;" Text="Activity-School Raw Data"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton9" runat="server" OnClick="LnkAnnualPl6_OnClick" Style="font-size: 16px; color: blue;" Text="Activity-Village Raw Data"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton10" runat="server" OnClick="LnkAnnualPl7_OnClick" Style="font-size: 16px; color: blue;" Text="Activity-Village GSS Raw Data"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton11" runat="server" OnClick="LnkAnnualPl8_OnClick" Style="font-size: 16px; color: blue;" Text="Activity-Village MM Raw Data"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton12" runat="server" OnClick="LnkAnnualPl9_OnClick" Style="font-size: 16px; color: blue;" Text="Approve Status"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:Label ID="Label3" Text="--Balsabha Report---" runat="server" Font-Size="Medium" Style="color: Black;"></asp:Label></li>

                        <li>
                            <asp:LinkButton ID="LinkButton13" runat="server" OnClick="LnkAnnualPl10_OnClick" Style="font-size: 16px; color: blue;" Text="Balsabha- Detail"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton14" runat="server" OnClick="LnkAnnualPl11_OnClick" Style="font-size: 16px; color: blue;" Text="Balsabha- Child Registration"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton15" runat="server" OnClick="LnkAnnualPl12_OnClick" Style="font-size: 16px; color: blue;" Text="LSE Attendance Detail"></asp:LinkButton>
                        </li>

                        <li>
                            <asp:Label ID="Label4" Text="--Activity Summary ---" runat="server" Font-Size="Medium" Style="color: Black;"></asp:Label></li>

                        <li>
                            <asp:LinkButton ID="LinkButton16" runat="server" OnClick="LnkAnnualPl13_OnClick" Style="font-size: 16px; color: blue;" Text="SIP Target vs Achv"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton17" runat="server" OnClick="LnkAnnualPl14_OnClick" Style="font-size: 16px; color: blue;" Text="SAC Report"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:Label ID="Label5" Text="--GKP Report---" runat="server" Font-Size="Medium" Style="color: Black;"></asp:Label></li>

                        <li>
                            <asp:LinkButton ID="LinkButton18" runat="server" OnClick="LnkAnnualPl15_OnClick" Style="font-size: 16px; color: blue;" Text="GKP Child Registration"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton19" runat="server" OnClick="LnkAnnualPl16_OnClick" Style="font-size: 16px; color: blue;" Text="GKP Child Registration Class 2"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton20" runat="server" OnClick="LnkAnnualPl17_OnClick" Style="font-size: 16px; color: blue;" Text="GKP Child Attendence"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton21" runat="server" OnClick="LnkAnnualPl18_OnClick" Style="font-size: 16px; color: blue;" Text="GKP Assessment"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton22" runat="server" OnClick="LnkAnnualPl19_OnClick" Style="font-size: 16px; color: blue;" Text="GKP Assessment Class 2"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton23" runat="server" OnClick="LnkAnnualPl120_OnClick" Style="font-size: 16px; color: blue;" Text="GKP Summary"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton24" runat="server" OnClick="LnkAnnualPl121_OnClick" Style="font-size: 16px; color: blue;" Text="GKP Assessment Summary"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton25" runat="server" OnClick="LnkAnnualPl122_OnClick" Style="font-size: 16px; color: blue;" Text="GKP Quality Alert"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:Label ID="Label6" Text="---Door to Door Survey---" runat="server" Font-Size="Medium" Style="color: Black;"></asp:Label></li>
                        <li>
                            <asp:LinkButton ID="LinkButton26" runat="server" OnClick="LnkAnnualPl123_OnClick" Style="font-size: 16px; color: blue;" Text="Door to Door Survey"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:Label ID="Label7" Text="--Annual Plan---" runat="server" Font-Size="Medium" Style="color: Black;"></asp:Label></li>

                        <li>
                            <asp:LinkButton ID="LinkButton27" runat="server" OnClick="LnkAnnualPl124_OnClick" Style="font-size: 16px; color: blue;" Text="Annual Plan Target Sheet"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton28" runat="server" OnClick="LnkAnnualPl125_OnClick" Style="font-size: 16px; color: blue;" Text="Annual Plan Summary"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton29" runat="server" OnClick="LnkAnnualPl126_OnClick" Style="font-size: 16px; color: blue;" Text="Enrollment Target Raw Data"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton30" runat="server" OnClick="LnkAnnualPl127_OnClick" Style="font-size: 16px; color: blue;" Text="Approval Process Report"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton31" runat="server" OnClick="LnkAnnualPl128_OnClick" Style="font-size: 16px; color: blue;" Text="Annual Plan Quality Alert"></asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-12" style="padding-left: 10px; margin-top: 10px">
            <div class="thumbnail" style="background-color: #f5f5f5; float: left; width: 100%; height: 782px">
                <div class="panel panel-default">
                    <div class="form-horizontal">
                        <div class="row" style="height: 769px;">
                            <div id="div-show-new" style="text-align: left;">
                                <div class="row marg search-bg">
                                    <div class="form-horizontal" style="padding: 8px 8px;">

                                        <div class="row" style="margin-bottom: 14px;">

                                            <div class="col-sm-2 ">

                                                <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                    Year</label>

                                                <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                    AutoPostBack="true" class="form-control ">
                                                </asp:DropDownList>

                                                <label for="email" class="padd linhei" style="padding-top: 5px;">
                                                    UserID</label>

                                                <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control">
                                                </asp:TextBox>
                                                <label for="email" class="padd linhei" style="padding-top: 5px;">
                                                    User Name</label>

                                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control">
                                                </asp:TextBox>


                                            </div>
                                            <div class="col-sm-2 ">

                                                <label for="email" class="padd linhei" style="padding-top: 1px;">
                                                    State</label>
                                                <div class="padd CheckBoxListCssClass " style="border: 1px solid #c1c1c1">

                                                    <div style="overflow: auto; margin-top: 1px; height: 150px;">
                                                        <asp:CheckBoxList ID="ChkState" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>

                                                </div>

                                            </div>

                                            <div class="col-sm-2 ">

                                                <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                    District</label>
                                                <div class="CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                    <div style="overflow: auto; margin-top: 1px; height: 150px;">
                                                        <asp:CheckBoxList ID="chkDistrict" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                        </asp:CheckBoxList>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-2 ">

                                                <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                    Block</label>
                                                <div class="CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                    <div style="overflow: auto; margin-top: 2px; height: 150px;">
                                                        <asp:CheckBoxList ID="chkBlock" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-2 ">

                                                <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                    Form date</label>

                                                <asp:TextBox runat="server" ID="txtFromDate"
                                                    autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>

                                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDate" PopupPosition="BottomRight">
                                                </ajax:CalendarExtender>

                                                <label for="email" class="padd linhei" style="padding-top: 5px;">
                                                    To Date</label>

                                                <asp:TextBox runat="server" ID="txtToDate" autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>
                                                <ajax:CalendarExtender ID="CalendarExtender2" runat="server"
                                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtToDate" PopupPosition="BottomRight">
                                                </ajax:CalendarExtender>



                                            </div>



                                        </div>



                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 table table-hover " style="padding: 0px;">
                                <asp:Panel ID="pnlMain" Enabled="true" runat="server">
                                    <div class="form-horizontal">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 10px;">
                                            <div class="panel-default search-bg" style="height: 30px">
                                                <span style="float: left; color: Black; margin-left: 12px;">
                                                    <asp:Label ID="lblTotal" Text="Total:" runat="server"></asp:Label>
                                                </span><span style="float: left; color: Black; margin-left: 12px;"></span>
                                            </div>
                                            <asp:Label ID="lblTotalCount" Visible="false" ForeColor="#737272" Font-Bold="true" runat="server"></asp:Label>
                                            <div style="height: 498px; overflow: auto; width: 100%;" align="center">
                                                <div>

                                                    <div class="row" style="width: 100%">
                                                        <asp:GridView ID="GV_DynamicGrid" runat="server" OnPageIndexChanging="GV_DynamicGrid1_OnPageIndexChanging" ForeColor="Black" AllowPaging="true"
                                                            PageSize="100" ShowHeader="true" Visible="false" CssClass="table table-striped table-bordered table-hover"
                                                            Width="100%">
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
