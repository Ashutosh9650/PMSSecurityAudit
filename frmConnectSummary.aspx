<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="frmConnectSummary.aspx.cs" Inherits="frmConnectSummary" %>

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
    <style type="text/css">
        .checkbox label:after, .radio label:after {
            content: '';
            display: table;
            clear: both;
        }

        .checkbox .cr, .radio .cr {
            position: relative;
            display: inline-block;
            border: 2px solid #333;
            border-radius: .25em;
            width: 1.3em;
            height: 1.3em;
            float: left;
            margin-right: .5em;
            color: red;
        }

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

    <script type="text/javascript">

        function SetMultilanguage(Flag, clsname) {
            var Lngg = "", lid = "";
            var maxSelection = 0;
            $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                Lngg = Lngg + $(this).next().html() + ",";
                lid = lid + $(this).val() + ",";
                maxSelection++;
            });

            Lngg = Lngg.substr(0, Lngg.length - 1);
            lid = lid.substr(0, lid.length - 1);
            if (Flag == 'F') {
                if (maxSelection <= 10) {
                    $('#<%=hdn_PBID.ClientID %>').val(lid);
                    $('#<%=hdn_PBName.ClientID %>').val(Lngg);
                    $('#<%=txt_pbname.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_PBID.ClientID %>').val('');
                    $('#<%=hdn_PBName.ClientID %>').val('');
                    $('#<%=txt_pbname.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }


            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%-- <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate--%>
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="panel panel-default " style="background-color: #f5f5f5; margin-bottom: 0px !important;">
                    <div class="panel-heading" style="padding: 5px 10px;">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                <div id="Div" class="col-lg-10 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                    <div class="">
                                        <h3 class="text-danger" style="margin: 0px;">Contact Summary Report
                                        </h3>
                                    </div>
                                </div>
                                <div id="fdf" class="col-lg-2 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Export to Excel" OnClick="btnImport_Click"
                                        class="pull-right"></asp:LinkButton>
                                </div>
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

        <div class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 text-left" style="padding-right: 0px; margin-top: 10px">
            <div style="overflow: auto; margin-top: 0px; height: 586px;">
                <div class="thumbnail" style="height: 565PX;">

                    <ul style="margin: 0px">
                        <li class=" active li-width" runat="server" id="A1">
                            <asp:LinkButton ID="Button3" runat="server" OnClick="LnkAnnualPlan_OnClick" Style="color: gren; color: blue;" Text="Contact Report District Summary "></asp:LinkButton>
                        </li>

                        <li runat="server" id="A2">
                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LnkAnnualPlanFC_OnClick" Style="color: gren; color: blue;" Text="Contact Report Block Summary"></asp:LinkButton>
                        </li>

                        <li runat="server" id="A3">
                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LnkAnnual_OnClick" Style="color: gren; color: blue;" Text="Contact Quality Alert"></asp:LinkButton>
                        </li>


                        <li runat="server" id="A4">
                            <asp:LinkButton ID="LinkButton5" runat="server" Text="Contact Detail Report" Style="color: gren; color: blue;"
                                OnClick="ContactReport_Click"></asp:LinkButton>
                        </li>
                        <li runat="server" id="A5">
                            <asp:LinkButton ID="LinkButton4" runat="server" Text="Contact Report(4 Year)" Style="color: gren; color: blue;"
                                OnClick="ContactReport4_Click"></asp:LinkButton>
                        </li>
                        <li runat="server" id="A6">
                            <asp:LinkButton ID="LinkButton6" runat="server" Text="Contact Report(15to18)" Style="color: gren; color: blue;"
                                OnClick="ContactReport15_Click"></asp:LinkButton>
                        </li>
                        <li runat="server" id="A7">

                            <asp:LinkButton ID="LinkButton12" runat="server" Text="Enrollment Daily status"
                                Style="color: gren; color: blue;" OnClick="LnkMobileDataReport_OnClick"></asp:LinkButton></li>
                        <li runat="server" id="A8">
                            <asp:LinkButton ID="LinkButton7" runat="server" Text="Enrollment Daily status(15to18)"
                                Style="color: gren; color: blue;" OnClick="LnkMobileDataReport15_OnClick"></asp:LinkButton></li>
                        <li class=" active li-width" runat="server" id="A9">
                            <asp:LinkButton ID="LinkButton25" runat="server" Text="Contact- Block Wise Summary" Style="color: gren; color: blue;"
                                OnClick="ContactSummary_Click"></asp:LinkButton>
                        </li>


                        <li class=" active li-width" runat="server" id="A10">
                            <asp:LinkButton ID="LinkButton26" runat="server" Text="Contact-Cluster Wise Summary" Style="color: gren; color: blue;"
                                OnClick="ClusterWise_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width" runat="server" id="A15">
                            <asp:LinkButton ID="LinkButton27" runat="server" Text="Contact- Block Wise Outreach" Style="color: gren; color: blue;"
                                OnClick="Outreach_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width" runat="server" id="A11">
                            <asp:LinkButton ID="LinkButton28" runat="server" Text="Contact- Cluster wise Outreach" Style="color: gren; color: blue;"
                                OnClick="OutreachCluster_Click"></asp:LinkButton>
                        </li>

                        <li runat="server" id="A12" visible="false">
                            <asp:LinkButton ID="LinkButton8" runat="server" Visible="false" Text="OOSC Contact Report" Style="color: gren; color: blue;"
                                OnClick="ContactReport_1"></asp:LinkButton>
                        </li>

                        <li class=" active li-width" runat="server" visible="false" id="A13">
                            <asp:LinkButton ID="LinkButton9" runat="server" Visible="false" Text="Contact Summary" Style="color: gren; color: blue;"
                                OnClick="ContactSummaryNew_Click"></asp:LinkButton>
                        </li>
                        <li class="li-width">
                            <asp:LinkButton ID="LinkButtggon10" Visible="true" runat="server" Text="Enrollment Target Raw Data"
                                Style="color: gren; color: blue;" OnClick="LnkEnrolment_OnClick"></asp:LinkButton></li>

                        <li runat="server" id="Li1">
                            <asp:LinkButton ID="LinkButton10" runat="server" Text="Contact Status Report" Style="color: gren; color: blue;"
                                OnClick="ContactRepordt_Click"></asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12" style="padding-left: 10px; margin-top: 10px">
            <div class="thumbnail" style="background-color: #f5f5f5; float: left; width: 100%;">
                <div class="panel panel-default" style="margin-bottom: 0px">
                    <div class="form-horizontal">
                        <div class="row" style="margin-bottom: 5px;">
                            <div id="div-show-new">
                                <div class="row marg search-bg">
                                    <div class="form-horizontal">

                                        <div class="row" style="margin-bottom: 10px;">

                                            <div class="col-sm-2 ">

                                                <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                    Year</label>

                                                <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                    AutoPostBack="true" class="form-control ">
                                                </asp:DropDownList>

                                                <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                    Contact Type</label>

                                                <asp:DropDownList ID="ddlTpye"
                                                    AutoPostBack="true" runat="server" class="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Contact" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Not -Contact" Value="2"></asp:ListItem>

                                                </asp:DropDownList>

                                            </div>


                                            <div class="col-sm-2  ">
                                                <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                    Gender</label>

                                                <asp:DropDownList ID="ddlGender"
                                                    AutoPostBack="true" runat="server" class="form-control">
                                                    <asp:ListItem Text="--All--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Female" Value="2"></asp:ListItem>

                                                </asp:DropDownList>
                                                <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                    Age(Contact Status)
                                                </label>
                                                <div class="CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                    <asp:TextBox ID="txt_pbname" autocomplete="off" ondrop="return false;" runat="server"
                                                        CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>

                                                    <ajax:PopupControlExtender ID="PopupControltxt_pbname" runat="server" TargetControlID="txt_pbname"
                                                        PopupControlID="pnt_bookformat" OffsetY="22">
                                                    </ajax:PopupControlExtender>
                                                    <asp:Panel ID="pnt_bookformat" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; color: Black; background-color: #F1F1F1; border: solid 1px #cccccc; width: 89.5%"
                                                        CssClass="panel">
                                                        <span>
                                                            <asp:CheckBoxList ID="chkAge" CssClass="_bookformat radio" runat="server"
                                                                onclick="SetMultilanguage('F','_bookformat');">
                                                            </asp:CheckBoxList>
                                                        </span>
                                                        <asp:HiddenField runat="server" ID="hdn_PBName" />
                                                        <asp:HiddenField runat="server" ID="hdn_PBID" />
                                                    </asp:Panel>
                                                </div>
                                            </div>


                                            <div class="col-sm-2 ">

                                                <label for="email" class="padd linhei" style="padding-top: 1px;">
                                                    State</label>
                                                <div class="padd CheckBoxListCssClass " style="border: 1px solid #c1c1c1">

                                                    <div style="overflow: auto; margin-top: 1px; height: 100px;">
                                                        <asp:CheckBoxList ID="ChkState" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-2 ">

                                                <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                    District</label>
                                                <div class="CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                    <div style="overflow: auto; margin-top: 1px; height: 100px;">
                                                        <asp:CheckBoxList ID="chkDistrict" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                        </asp:CheckBoxList>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-2 ">

                                                <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                    Block</label>
                                                <div class="CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                    <div style="overflow: auto; margin-top: 2px; height: 100px;">
                                                        <asp:CheckBoxList ID="chkBlock" RepeatDirection="Vertical" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                    </div>



                                </div>

                            </div>
                            <%--        <div id="Div1" class="Row" style="width: 100%" runat="server">


                                            

                                                      <asp:GridView ID="DGV_Report" OnRowDataBound="DGV_Report_RowDataBound"   AutoGenerateColumns="false" AllowPaging="true" PageSize="30" runat="server" ForeColor="#333333" CellPadding="1"  
                                                                       CssClass="table table-striped table table-hover table-bordered  " Width="99.7%"      >
                               
                                <EmptyDataTemplate>
                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                        Data not found</div>
                                </EmptyDataTemplate>
                                <Columns>
                                     <asp:TemplateField HeaderText="OutCome">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTarOutCome1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("OutCome") %>'></asp:Label>
                                                                                 <asp:LinkButton ID="LinkButton4" OnClick="btn_Life_Click" Visible="false" runat="server" Text='<%#Eval("OutCome") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target (Till month)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTargetTillmonth1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("TargetTillmonth") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="Achievement (Till Date)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTarge" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("AchievementTillDate") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            

                                                       </div>--%>
                        </div>
                    </div>
                </div>
            </div>

            <div id="Div18" class="Row" style="width: 100%" runat="server">




                <asp:GridView ID="gvReportCluster" runat="server" OnRowCreated="gvReportCluster_RowCreated" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" Font-Names="Arial"
                    Font-Size="12px" Width="100%">
                    <EmptyDataTemplate>
                        <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                            Data not found
                        </div>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                    <AlternatingRowStyle BackColor="#f1f1f1" />
                    <Columns>
                        <asp:TemplateField HeaderText="BlockName" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbBlockName" ForeColor="Black" runat="server" Text='<%# Bind("BlockName") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5 to 6 Yrs" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbtn" ForeColor="Black" runat="server" Text='<%# Bind("EnSRG5to6Yrs") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_2" ForeColor="Black" Text='<%# Bind("EnSRG10to14Yrs") %>'
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_3" ForeColor="Black" Text='<%# Bind("FOG5to6Yrs") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_4" ForeColor="Black" Text='<%# Bind("FOG7to9Yrs") %>'
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_5" ForeColor="Black" Text='<%# Bind("FOG7to9Yrs") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_6" Text='<%# Bind("FOG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_7" Text='<%# Bind("ING5to6Yrs") %>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_8" Text='<%# Bind("ING7to9Yrs") %>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_9" Text='<%# Bind("ING10to14Yrs") %>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_10" Text='<%# Bind("EnM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_11" Text='<%# Bind("EnM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_12" Text='<%# Bind("EnM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol3_11" Text='<%# Bind("FOM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_312" Text='<%# Bind("FOM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCoeel3_11" Text='<%# Bind("FOM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblColee_312" Text='<%# Bind("INM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCol3_11" Text='<%# Bind("INM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_312" Text='<%# Bind("INM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_11" Text='<%# Bind("NRSTCG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_12" Text='<%# Bind("NRSTCG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCol333_11" Text='<%# Bind("NRSTCG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_4312" Text='<%# Bind("KGBVG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblqCol_11" Text='<%# Bind("KGBVG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC3ol_12" Text='<%# Bind("KGBVG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl2Col_4312" Text='<%# Bind("AnaG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblq2Col_11" Text='<%# Bind("AnaG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC23ol_12" Text='<%# Bind("AnaG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22Col_4312" Text='<%# Bind("MAG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblq32Col_11" Text='<%# Bind("MAG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC223ol_12" Text='<%# Bind("MAG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22CNRSTCM5to6Yrsol_4312" Text='<%# Bind("NRSTCM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblq32Col_11NRSTCM7to9Yrs" Text='<%# Bind("NRSTCM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC223ol_12NRSTCM10to14Yrs" Text='<%# Bind("NRSTCM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22CM5to6Yrsol_4312" Text='<%# Bind("KGBVM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblq32C1NRSTCM7to9Yrs" Text='<%# Bind("KGBVM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC22M10to14Yrs" Text='<%# Bind("KGBVM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22CM5toYrsol_4312" Text='<%# Bind("AnaM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblq32C1NRSM7to9Yrs" Text='<%# Bind("AnaM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC22M10t4Yrs" Text='<%# Bind("AnaM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lb22CM5toYrsol_4312" Text='<%# Bind("MAM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblq3C1NRSM7to9Yrs" Text='<%# Bind("MAM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbl22M10t4Yrs" Text='<%# Bind("MAM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22CM5trsol_4312" Text='<%# Bind("EPG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblq32C1NR7to9Yrs" Text='<%# Bind("EPG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC22M0t4Yrs" Text='<%# Bind("EPG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22CM5trso2" Text='<%# Bind("REG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblq32C1s" Text='<%# Bind("REG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC22M0s" Text='<%# Bind("REG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22NRSG10to14Yrs2" Text='<%# Bind("NRSG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblNRSG10to14Yrss" Text='<%# Bind("NRSG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC22NRSG10to14YrsM0s" Text='<%# Bind("NRSG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22NRSto14Yrs2" Text='<%# Bind("NROG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblSG10to14Yrss" Text='<%# Bind("NROG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC210to14YrsM0s" Text='<%# Bind("NROG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22NSt14Yrs2" Text='<%# Bind("EPM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblSG10toEPM10to14Yrs14Yrss" Text='<%# Bind("EPM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC210trsM0s" Text='<%# Bind("EPM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl2REM5to6Yrs" Text='<%# Bind("REM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblSG10tREM7to9Yrss" Text='<%# Bind("REM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC210trsM0s" Text='<%# Bind("REM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl2REM5to6s" Text='<%# Bind("NRSM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblSG10tREM9Yrss" Text='<%# Bind("NRSM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC210tM0s" Text='<%# Bind("NRSM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl2REM95to6s" Text='<%# Bind("NROM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblSG810tREM9Yrss" Text='<%# Bind("NROM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC2710tM0s" Text='<%# Bind("NROM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl2RE95to6s" Text='<%# Bind("GigG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblSG810tRYrss" Text='<%# Bind("GigG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC2710ts" Text='<%# Bind("GigG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="ll2RE95to6s" Text='<%# Bind("OverG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbSG810tRYrss" Text='<%# Bind("OverG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbC2710ts" Text='<%# Bind("OverG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="ll2RE95o6s" Text='<%# Bind("UndG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbSG81tRYrss" Text='<%# Bind("UndG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbC271ts" Text='<%# Bind("UndG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="ll2RTEG10to14Yrso6s" Text='<%# Bind("TEG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbSG81TEG10to14YrstRYrss" Text='<%# Bind("TEG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbC2TEG10to14Yrs71ts" Text='<%# Bind("TEG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="ll2RTEG10to14Yo6s" Text='<%# Bind("DEG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbSG81TEG108to14RYrss" Text='<%# Bind("DEG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbC2TEG10to14Yrs71ts" Text='<%# Bind("DEG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl2R2E95to6s" Text='<%# Bind("GigM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblSG8110tRYrss" Text='<%# Bind("GigM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC21710ts" Text='<%# Bind("GigM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="ll2RE915to6s" Text='<%# Bind("OverM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbSG810t1RYrss" Text='<%# Bind("OverM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbC2710ts" Text='<%# Bind("OverM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="ll2RE195o6s" Text='<%# Bind("UndM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbSG811tRYrss" Text='<%# Bind("UndM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbC1271ts" Text='<%# Bind("UndM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="ll21RTEM10to14Yrso6s" Text='<%# Bind("TEM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbSG811TEM10to14YrstRYrss" Text='<%# Bind("TEM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbC2TEM101to14Yrs71ts" Text='<%# Bind("TEM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="ll2RTEM110to14Yo6s" Text='<%# Bind("DEM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbSG81TE1M108to14RYrss" Text='<%# Bind("DEM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbC2TEM10to14Y1rs71ts" Text='<%# Bind("DEM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>



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







                <asp:GridView ID="gvReportNew" runat="server" OnRowCreated="gvReportNew_RowCreated" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" Font-Names="Arial"
                    Font-Size="12px" Width="100%">
                    <EmptyDataTemplate>
                        <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                            Data not found
                        </div>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                    <AlternatingRowStyle BackColor="#f1f1f1" />
                    <Columns>
                        <asp:TemplateField HeaderText="BlockName" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbBlockName" ForeColor="Black" runat="server" Text='<%# Bind("BlockName") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ClusterName" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbBloClusterName" ForeColor="Black" runat="server" Text='<%# Bind("ClusterName") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbtn" ForeColor="Black" runat="server" Text='<%# Bind("TargetG5to6Yrs") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Achievement">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_2" ForeColor="Black" Text='<%# Bind("TargetAG5to6Yrs") %>'
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_3" ForeColor="Black" Text='<%# Bind("TargetG7to9Yrs") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Achievement">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_4" ForeColor="Black" Text='<%# Bind("TargetAG7to9Yrs") %>'
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_5" ForeColor="Black" Text='<%# Bind("TargetG10to14Yrs") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Achievement">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_6" Text='<%# Bind("TargetAG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_7" Text='<%# Bind("TotalG5to14Yrs") %>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Achievement">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_8" Text='<%# Bind("TotalAchG5to14Yrs") %>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_9" Text='<%# Bind("TargetM5to6Yrs") %>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Achievement">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_10" Text='<%# Bind("TargetAM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_11" Text='<%# Bind("TargetM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Achievement">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_12" Text='<%# Bind("TargetAM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Target">
                            <ItemTemplate>
                                <asp:Label ID="lblCol3_11" Text='<%# Bind("TargetM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Achievement">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_312" Text='<%# Bind("TargetAM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>




                        <asp:TemplateField HeaderText="Target">
                            <ItemTemplate>
                                <asp:Label ID="lblCoeel3_11" Text='<%# Bind("TotalM5to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Achievement">
                            <ItemTemplate>
                                <asp:Label ID="lblColee_312" Text='<%# Bind("TotalAchM5to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblCol3_11" Text='<%# Bind("ReamingTotalTargetG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_312" Text='<%# Bind("ReamingtotalTargetG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_11" Text='<%# Bind("ReamingtotalTargetG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_12" Text='<%# Bind("totalReaming")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblCol333_11" Text='<%# Bind("ReamingTargetM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_4312" Text='<%# Bind("ReamingTargetM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblqCol_11" Text='<%# Bind("ReamingTargetM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblC3ol_12" Text='<%# Bind("ReamingTotaltM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                    </Columns>
                </asp:GridView>


                <asp:GridView ID="gvReport" runat="server" OnRowCreated="gvReport_RowCreated" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" Font-Names="Arial"
                    Font-Size="12px" Width="100%">
                    <EmptyDataTemplate>
                        <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                            Data not found
                        </div>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                    <AlternatingRowStyle BackColor="#f1f1f1" />
                    <Columns>
                        <asp:TemplateField HeaderText="BlockName" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbBlockName" ForeColor="Black" runat="server" Text='<%# Bind("BlockName") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Target" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbtn" ForeColor="Black" runat="server" Text='<%# Bind("TargetG5to6Yrs") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Achievement">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_2" ForeColor="Black" Text='<%# Bind("TargetAG5to6Yrs") %>'
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_3" ForeColor="Black" Text='<%# Bind("TargetG7to9Yrs") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Achievement">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_4" ForeColor="Black" Text='<%# Bind("TargetAG7to9Yrs") %>'
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_5" ForeColor="Black" Text='<%# Bind("TargetG10to14Yrs") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Achievement">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_6" Text='<%# Bind("TargetAG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_7" Text='<%# Bind("TotalG5to14Yrs") %>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Achievement">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_8" Text='<%# Bind("TotalAchG5to14Yrs") %>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_9" Text='<%# Bind("TargetM5to6Yrs") %>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Achievement">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_10" Text='<%# Bind("TargetAM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_11" Text='<%# Bind("TargetM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Achievement">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_12" Text='<%# Bind("TargetAM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Target">
                            <ItemTemplate>
                                <asp:Label ID="lblCol3_11" Text='<%# Bind("TargetM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Achievement">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_312" Text='<%# Bind("TargetAM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>




                        <asp:TemplateField HeaderText="Target">
                            <ItemTemplate>
                                <asp:Label ID="lblCoeel3_11" Text='<%# Bind("TotalM5to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Achievement">
                            <ItemTemplate>
                                <asp:Label ID="lblColee_312" Text='<%# Bind("TotalAchM5to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblCol3_11" Text='<%# Bind("ReamingTotalTargetG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_312" Text='<%# Bind("ReamingtotalTargetG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_11" Text='<%# Bind("ReamingtotalTargetG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_12" Text='<%# Bind("totalReaming")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblCol333_11" Text='<%# Bind("ReamingTargetM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_4312" Text='<%# Bind("ReamingTargetM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblqCol_11" Text='<%# Bind("ReamingTargetM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblC3ol_12" Text='<%# Bind("ReamingTotaltM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                    </Columns>
                </asp:GridView>






                <asp:GridView ID="gvReportClusterOutrich" runat="server" OnRowCreated="gvReportClusterOutrich_RowCreated" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" Font-Names="Arial"
                    Font-Size="12px" Width="100%">
                    <EmptyDataTemplate>
                        <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                            Data not found
                        </div>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                    <AlternatingRowStyle BackColor="#f1f1f1" />
                    <Columns>
                        <asp:TemplateField HeaderText="BlockName" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbBlockName" ForeColor="Black" runat="server" Text='<%# Bind("BlockName") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ClusterName" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbBlock4Name" ForeColor="Black" runat="server" Text='<%# Bind("ClusterName") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5 to 6 Yrs" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbtn" ForeColor="Black" runat="server" Text='<%# Bind("EnSRG5to6Yrs") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_2" ForeColor="Black" Text='<%# Bind("EnSRG10to14Yrs") %>'
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_3" ForeColor="Black" Text='<%# Bind("FOG5to6Yrs") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_4" ForeColor="Black" Text='<%# Bind("FOG7to9Yrs") %>'
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_5" ForeColor="Black" Text='<%# Bind("FOG7to9Yrs") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_6" Text='<%# Bind("FOG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_7" Text='<%# Bind("ING5to6Yrs") %>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_8" Text='<%# Bind("ING7to9Yrs") %>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_9" Text='<%# Bind("ING10to14Yrs") %>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_10" Text='<%# Bind("EnM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_11" Text='<%# Bind("EnM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_12" Text='<%# Bind("EnM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol3_11" Text='<%# Bind("FOM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_312" Text='<%# Bind("FOM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCoeel3_11" Text='<%# Bind("FOM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblColee_312" Text='<%# Bind("INM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCol3_11" Text='<%# Bind("INM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_312" Text='<%# Bind("INM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_11" Text='<%# Bind("NRSTCG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_12" Text='<%# Bind("NRSTCG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblCol333_11" Text='<%# Bind("NRSTCG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lblCol_4312" Text='<%# Bind("KGBVG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblqCol_11" Text='<%# Bind("KGBVG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC3ol_12" Text='<%# Bind("KGBVG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl2Col_4312" Text='<%# Bind("AnaG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblq2Col_11" Text='<%# Bind("AnaG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC23ol_12" Text='<%# Bind("AnaG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22Col_4312" Text='<%# Bind("MAG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblq32Col_11" Text='<%# Bind("MAG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC223ol_12" Text='<%# Bind("MAG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22CNRSTCM5to6Yrsol_4312" Text='<%# Bind("NRSTCM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblq32Col_11NRSTCM7to9Yrs" Text='<%# Bind("NRSTCM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC223ol_12NRSTCM10to14Yrs" Text='<%# Bind("NRSTCM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22CM5to6Yrsol_4312" Text='<%# Bind("KGBVM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblq32C1NRSTCM7to9Yrs" Text='<%# Bind("KGBVM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC22M10to14Yrs" Text='<%# Bind("KGBVM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22CM5toYrsol_4312" Text='<%# Bind("AnaM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblq32C1NRSM7to9Yrs" Text='<%# Bind("AnaM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC22M10t4Yrs" Text='<%# Bind("AnaM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lb22CM5toYrsol_4312" Text='<%# Bind("MAM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblq3C1NRSM7to9Yrs" Text='<%# Bind("MAM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbl22M10t4Yrs" Text='<%# Bind("MAM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22CM5trsol_4312" Text='<%# Bind("EPG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblq32C1NR7to9Yrs" Text='<%# Bind("EPG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC22M0t4Yrs" Text='<%# Bind("EPG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22CM5trso2" Text='<%# Bind("REG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblq32C1s" Text='<%# Bind("REG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC22M0s" Text='<%# Bind("REG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22NRSG10to14Yrs2" Text='<%# Bind("NRSG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblNRSG10to14Yrss" Text='<%# Bind("NRSG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC22NRSG10to14YrsM0s" Text='<%# Bind("NRSG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22NRSto14Yrs2" Text='<%# Bind("NROG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblSG10to14Yrss" Text='<%# Bind("NROG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC210to14YrsM0s" Text='<%# Bind("NROG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl22NSt14Yrs2" Text='<%# Bind("EPM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblSG10toEPM10to14Yrs14Yrss" Text='<%# Bind("EPM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC210trsM0s" Text='<%# Bind("EPM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl2REM5to6Yrs" Text='<%# Bind("REM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblSG10tREM7to9Yrss" Text='<%# Bind("REM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC210trsM0s" Text='<%# Bind("REM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl2REM5to6s" Text='<%# Bind("NRSM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblSG10tREM9Yrss" Text='<%# Bind("NRSM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC210tM0s" Text='<%# Bind("NRSM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl2REM95to6s" Text='<%# Bind("NROM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblSG810tREM9Yrss" Text='<%# Bind("NROM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC2710tM0s" Text='<%# Bind("NROM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl2RE95to6s" Text='<%# Bind("GigG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblSG810tRYrss" Text='<%# Bind("GigG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC2710ts" Text='<%# Bind("GigG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="ll2RE95to6s" Text='<%# Bind("OverG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbSG810tRYrss" Text='<%# Bind("OverG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbC2710ts" Text='<%# Bind("OverG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="ll2RE95o6s" Text='<%# Bind("UndG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbSG81tRYrss" Text='<%# Bind("UndG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbC271ts" Text='<%# Bind("UndG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="ll2RTEG10to14Yrso6s" Text='<%# Bind("TEG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbSG81TEG10to14YrstRYrss" Text='<%# Bind("TEG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbC2TEG10to14Yrs71ts" Text='<%# Bind("TEG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="ll2RTEG10to14Yo6s" Text='<%# Bind("DEG5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbSG81TEG108to14RYrss" Text='<%# Bind("DEG7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbC2TEG10to14Yrs71ts" Text='<%# Bind("DEG10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="lbl2R2E95to6s" Text='<%# Bind("GigM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblSG8110tRYrss" Text='<%# Bind("GigM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lblC21710ts" Text='<%# Bind("GigM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="ll2RE915to6s" Text='<%# Bind("OverM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbSG810t1RYrss" Text='<%# Bind("OverM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbC2710ts" Text='<%# Bind("OverM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="ll2RE195o6s" Text='<%# Bind("UndM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbSG811tRYrss" Text='<%# Bind("UndM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbC1271ts" Text='<%# Bind("UndM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="ll21RTEM10to14Yrso6s" Text='<%# Bind("TEM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbSG811TEM10to14YrstRYrss" Text='<%# Bind("TEM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbC2TEM101to14Yrs71ts" Text='<%# Bind("TEM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="5 to 6 Yrs">
                            <ItemTemplate>
                                <asp:Label ID="ll2RTEM110to14Yo6s" Text='<%# Bind("DEM5to6Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7 to 9 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbSG81TE1M108to14RYrss" Text='<%# Bind("DEM7to9Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10 to 14 Yr">
                            <ItemTemplate>
                                <asp:Label ID="lbC2TEM10to14Y1rs71ts" Text='<%# Bind("DEM10to14Yrs")%>' ForeColor="Black" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
</asp:Content>
