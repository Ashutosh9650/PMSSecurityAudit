<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="frmMobileTargetReport.aspx.cs" Culture="en-GB" Inherits="frmMobileTargetReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<asp:UpdatePanel ID="updmain" runat="server">--%>
    <%--<ContentTemplate>--%>

    <div class="container-fluid">

        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="panel panel-default " style="background-color: #f5f5f5; margin-bottom: 0px !important;">
                    <div class="panel-heading" style="padding: 5px 10px">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                <div id="Div" class="col-lg-10 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                    <div class="">
                                        <h3 class="text-danger" style="margin: 0px;">Activity Report
                                        </h3>
                                    </div>
                                </div>
                                <div id="fdf" class="col-lg-2 col-md-2 col-sm-3 cpl-xs-12" runat="server" style="padding-right: 0px;">
                                    <%--<div class="form-group">
                                        <asp:LinkButton ID="LinkButton1" Visible="false" runat="server" Text="Export to Excel"
                                            class="pull-right"></asp:LinkButton>
                                       </div>
                                    --%>
                                    <%--<span class="pull-right" style="font-size: 17px;"></span>
                                        <div id="Div1" class="col-lg-2 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                        <div class="form-group">
                                        <asp:LinkButton ID="lnkCSV" runat="server" Text="Export to CSV" ></asp:LinkButton>
                                    </div>--%>

                                    <asp:LinkButton ID="btnexcel" class="pull-right" runat="server" Text="Export to Excel" OnClick="Export_To_Excel" Style="padding-right: 15px;"></asp:LinkButton>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 text-left" style="margin-top: 10px; padding: 0px;">
            <div style="overflow: auto; margin-top: 0px; padding-left: 15px; height: 628px;">
                <div class="thumbnail" style="height: 1082PX; margin-bottom: 0px;">
                    <ul style="margin: 0px">
                        <li class=" active li-width" runat="server" visible="false">
                            <asp:LinkButton ID="Button3" runat="server" Text="Village Report Card " Style="color: green; color: blue;"
                                OnClick="PMS_Click"></asp:LinkButton>

                        </li>

                        <li>
                            <asp:Label ID="ddsd" Text="---Activity Report---" runat="server" Font-Size="Medium" Style="color: Black; font: size:larger;"></asp:Label></li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton17" runat="server" Text="Activity-School Raw Data" Style="color: green; color: blue;"
                                OnClick="ActivitySchoolRaw_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton19" runat="server" Text="Activity-Village Raw Data" Style="color: green; color: blue;"
                                OnClick="ActivityVillage_Click"></asp:LinkButton>


                        </li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton38" runat="server" Text="Activity-Village GSS Raw Data" Style="color: green; color: blue;"
                                OnClick="ActivityVillageGSS_Click"></asp:LinkButton>


                        </li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton39" runat="server" Text="Activity-Village MM Raw Data" Style="color: green; color: blue;"
                                OnClick="ActivityVillageMM_Click"></asp:LinkButton>


                        </li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton24" runat="server" Text="Activity-Baseline Raw Data" Style="color: green; color: blue;"
                                OnClick="ActivityBaselineVillage_Click"></asp:LinkButton>
                        </li>

                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton2" runat="server" visible="false" Text="Activity Update Reports" Style="color: green; color: blue;"
                                OnClick="UpdateSchool_Click"></asp:LinkButton>
                        </li>

                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton37" runat="server" Text="School Contact Raw Data" Style="color: green; color: blue;"
                                OnClick="ActivitySchoolRaw3_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton22" runat="server" Text="Reason Report" Style="color: green; color: blue;"
                                OnClick="Reason_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton11" runat="server" Text="Approve Status" Style="color: green; color: blue;"
                                OnClick="Approve_Click"></asp:LinkButton>
                        </li>

                         <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton25" runat="server" Text="Panchayat Meeting Report" Style="color: green; color: blue;"
                                OnClick="PanchayatMeeting_Click"></asp:LinkButton>


                        </li>
                         <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton26" runat="server" Text="Ratri Chaupal Report" Style="color: green; color: blue;"
                                OnClick="ActivityVillageRatri_Click"></asp:LinkButton>


                        </li>
                         <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton27" runat="server" Text="Namankan Rally Report" Style="color: green; color: blue;"
                                OnClick="ActivityVillageNamankan_Click"></asp:LinkButton>


                        </li>


                        <li>
                            <asp:Label ID="Label2" Text="---SMC Report---" runat="server" Font-Size="Medium" Style="color: Black; font: size:larger;"></asp:Label></li>

                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton20" runat="server" Text="SMC Raw Data" Style="color: gren; color: blue;"
                                OnClick="SMCe_Click"></asp:LinkButton>
                        </li>

                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButtonr40" runat="server" Text="SMC Members Detail" Style="color: gren; color: blue;"
                                OnClick="SMCeMeeting_Click"></asp:LinkButton>
                        </li>
                           <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton42" runat="server" Text="SMC Members Attendance" Style="color: gren; color: blue;"
                                OnClick="SMCeMeetin99g1_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton18" runat="server" Text="SMC Meeting Block Summary" Style="color: gren; color: blue;"
                                OnClick="AnnaualFCReport_Click"></asp:LinkButton></li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton34" runat="server" Text="SMC Meeting School Summary" Style="color: gren; color: blue;"
                                OnClick="AnnaualSMCMeetingSchoolSummary"></asp:LinkButton></li>

                        <li>
                            <asp:Label ID="Label3" Text="---Balsabha Report---" runat="server" Font-Size="Medium" Style="color: Black; font: size:larger;"></asp:Label></li>


                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton32" runat="server" Text="Balsabha- Detail" Style="color: gren; color: blue;"
                                OnClick="Balsabasumll_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton35" runat="server" Text="Balsabha- Child Registration" Style="color: gren; color: blue;"
                                OnClick="BalsabaRawData_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width" runat="server" visible="false">
                            <asp:LinkButton ID="LinkButton31" runat="server" Text="Balsabha-District Wise Summary" Style="color: gren; color: blue;"
                                OnClick="BalsabaDistrict_Click"></asp:LinkButton>
                        </li>

                        <li class=" active li-width" runat="server" visible="false">
                            <asp:LinkButton ID="LinkButton30" runat="server" Text="Balsabha-Block Wise Summary" Style="color: gren; color: blue;"
                                OnClick="BalsabaBlock_Click"></asp:LinkButton>
                        </li>


                        <li class=" active li-width" runat="server" visible="false">
                            <asp:LinkButton ID="LinkButton29" runat="server" Text="Balsabha-Cluster Wise Summary" Style="color: gren; color: blue;"
                                OnClick="Balsaba_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width" runat="server" visible="false">
                            <asp:LinkButton ID="LinkButton23" runat="server" Visible="false" Text="Life Skill Game Summary" Style="color: gren; color: blue;"
                                OnClick="SMSSummary_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton36" OnClick="BalsabaRawDataLiff_Click" runat="server" Text="LSE Attendance Detail" Style="color: gren; color: blue;"></asp:LinkButton>
                        </li>

                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton5" OnClick="BalsabaRawDataLiff_Click1" runat="server" Text="LSE Assessment report" Style="color: gren; color: blue;"></asp:LinkButton>
                        </li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton012" Visible="false" OnClick="BalsabaRawDataLiff_Click2" runat="server" Text="LSE Assessment Score report" Style="color: gren; color: blue;"></asp:LinkButton>
                        </li>
                           <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton43" OnClick="BalsabaRawDatahf_Click2" runat="server" Text="LSE Summary" Style="color: gren; color: blue;"></asp:LinkButton>
                        </li>

                           <li>
                            <asp:Label ID="Labhel6" Text="---KGBV LSE Report---" runat="server" Font-Size="Medium" Style="color: Black; font: size:larger;"></asp:Label></li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton40" runat="server" Text="KGBV- Child Registration" Style="color: gren; color: blue;"
                                OnClick="BalsabaRawDataKG_Click"></asp:LinkButton></li>

                         <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton41" OnClick="BalsabaRawDataLifftt_Click" runat="server" Text="KGBV LSE Attendance Detail" Style="color: gren; color: blue;"></asp:LinkButton>
                        </li>
                         <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton28" OnClick="BalsabaRawDataLifftt_Click1" runat="server" Text="KGBV Assessment Detail report" Style="color: gren; color: blue;"></asp:LinkButton>
                        </li>
                       

                        <li>
                            <asp:Label ID="Label4" Text="---SIP Report---" runat="server" Font-Size="Medium" Style="color: Black; font: size:larger;"></asp:Label></li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton33" runat="server" Text="SIP Details" Style="color: gren; color: blue;"
                                OnClick="SMSIP_Click1"></asp:LinkButton>
                        </li>

                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton21" runat="server" Text="SIP Status Report" Style="color: gren; color: blue;"
                                OnClick="SMSIP_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton14" runat="server" Text="SIP last Year and Current Status" Style="color: gren; color: blue;"
                                OnClick="SACLastCurren_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton16" runat="server" Text="SAC quarter Wise" Style="color: gren; color: blue;"
                                OnClick="SACquarterWise_Click"></asp:LinkButton>
                        </li>
                        <%--  <li class=" active li-width">
                                <asp:LinkButton ID="bnt" runat="server" Text="Quarterly SIP Report"  style="color: gren;color: blue;" 
                                    OnClick="Annaul_Click"></asp:LinkButton>
                            </li>--%>
                        <%-- <li ><asp:Label ID="Label6" Text="---Contact Report---" runat="server"  Font-Size="Medium" style="color:Black;font:size:larger;" ></asp:Label></li>--%>

                        <li class=" active li-width" runat="server" visible="false">
                            <asp:LinkButton ID="LinkButton1" runat="server" Text="School Report Card " Style="color: gren; color: blue;"
                                OnClick="School_Click"></asp:LinkButton></li>




                        <%--    <li class=" active li-width">
                                <asp:LinkButton ID="LinkButton5" runat="server" Text="Contact Report"  style="color: gren;color: blue;" 
                                    OnClick="ContactReport_Click"></asp:LinkButton>
                            </li>

                           <li class=" active li-width">
                                <asp:LinkButton ID="LinkButton25"  runat="server"  Text="Contact- Block Wise Summary"  style="color: gren;color: blue;" 
                                    OnClick="ContactSummary_Click"></asp:LinkButton>
                            </li>

                             
                               <li class=" active li-width">
                                <asp:LinkButton ID="LinkButton26"  runat="server"  Text="Contact-Cluster Wise Summary"  style="color: gren;color: blue;" 
                                    OnClick="ClusterWise_Click"></asp:LinkButton>
                            </li>
                              <li class=" active li-width">
                                <asp:LinkButton ID="LinkButton27"  runat="server"   Text="Contact- Block Wise Outreach"  style="color: gren;color: blue;" 
                                    OnClick="Outreach_Click"></asp:LinkButton>
                            </li>
                              <li class=" active li-width">
                                <asp:LinkButton ID="LinkButton28"  runat="server"  Text="Contact- Cluster wise Outreach"  style="color: gren;color: blue;" 
                                    OnClick="OutreachCluster_Click"></asp:LinkButton>
                            </li>--%>
                        <%--  <li ><asp:Label ID="Label11" Text="---GKP Report---" runat="server"  Font-Size="Medium" style="color:Black;font:size:larger;" ></asp:Label></li>
                             <li class=" active li-width">
                                <asp:LinkButton ID="LinkButton12" runat="server" Text="GKP Report"  style="color: gren;color: blue;" 
                                    OnClick="GKP_Click"></asp:LinkButton>
                               </li>--%>

                        <li class=" active li-width" runat="server" visible="false">
                            <asp:LinkButton ID="LinkButton13" runat="server" Visible="false" Text="SAC Current Status" Style="color: gren; color: blue;"
                                OnClick="SACCurren_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width" runat="server" visible="false">
                            <asp:LinkButton ID="LinkButton15" Visible="false" runat="server" Text="SAC Cluster Summary" Style="color: gren; color: blue;"
                                OnClick="SSACCluster_Click"></asp:LinkButton>
                        </li>



                        <li class=" active li-width" runat="server" visible="false">
                            <asp:LinkButton ID="LinkButton6" runat="server" Visible="false" Text="Age & Gender Wise" Style="color: gren; color: blue;"
                                OnClick="Age_Click"></asp:LinkButton>
                        </li>

                        <li class=" active li-width" runat="server" visible="false">
                            <asp:LinkButton ID="LinkButton7" runat="server" Visible="false" Text="Social Category Wise" Style="color: gren; color: blue;"
                                OnClick="Social_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width" runat="server" visible="false">
                            <asp:LinkButton ID="LinkButton8" runat="server" Visible="false" Text="Family Occupation" Style="color: gren; color: blue;"
                                OnClick="Family_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width" runat="server" visible="false">
                            <asp:LinkButton ID="LinkButton9" runat="server" Visible="false" Text="Achievement-Age-Class" Style="color: gren; color: blue;"
                                OnClick="Achievemen_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width" runat="server" visible="false">
                            <asp:LinkButton ID="LinkButton10" runat="server" Visible="false" Text="Class Wise Target" Style="color: gren; color: blue;"
                                OnClick="Class_Click"></asp:LinkButton>
                        </li>


                        <li class=" active li-width" runat="server" visible="false">
                            <asp:LinkButton ID="SAC" runat="server" Visible="false" Text="SAC quarter Clusterise Wise" Style="color: gren; color: blue;"
                                OnClick="SACquarter_Click"></asp:LinkButton>
                        </li>



                    </ul>
                </div>
            </div>
        </div>

        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12" style="margin-top: 10px; padding-left: 10px;">
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

                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px; padding-right: 15px;">
                                                        Year:</label>
                                                    <div class="col-sm-9 padd" style="padding: 0px 10px 0px 12px;">
                                                        <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                            AutoPostBack="true" class="form-control ">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group" runat="server" id="div14">
                                                    <label for="email" class="col-sm-3 padd linhei">
                                                        Contact
                                                    </label>
                                                    <div class="col-sm-9 padd" style="padding: 0px 10px 0px 12px;">
                                                        <asp:DropDownList ID="ddlContact" runat="server"
                                                            class="form-control ">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Ineligible Contact Status Child" Value="1"></asp:ListItem>

                                                            <asp:ListItem Text="Ready For Enrolled Status Child Detail" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Enrolled Contact Status Child" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Enrolled Info By Parent Status Child" Value="4"></asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>



                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px; padding-right: 30px;">
                                                        State:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass " style="border: 1px solid #c1c1c1">

                                                        <div style="overflow: auto; margin-top: 2px; height: 79px;">
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
                                                        <div style="overflow: auto; margin-top: 2px; height: 79px;">
                                                            <asp:CheckBoxList ID="chkDistrict" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Block:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                        <div style="overflow: auto; margin-top: 2px; height: 79px;">
                                                            <asp:CheckBoxList ID="chkBlock" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div id="Div5" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Cluster:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                        <div style="overflow: auto; margin-top: 2px; height: 79px;">

                                                            <asp:CheckBoxList ID="chkCluster" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlCluster_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div2" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Panchayat:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                        <div style="overflow: auto; margin-top: 2px; height: 79px;">
                                                            <asp:CheckBoxList ID="ddlPanchayat" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div3" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Village:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                        <div style="overflow: auto; margin-top: 2px; height: 79px;">
                                                            <asp:CheckBoxList ID="chkVillage" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px; padding-left: 16px;">
                                                        Approve Type:
                                                    </label>
                                                    <div class="col-sm-8 padd" style="padding-left: 30px;">
                                                        <asp:RadioButtonList RepeatDirection="Vertical" ForeColor="Black" CssClass="cr-icon" ID="rblApprove" runat="server">

                                                            <asp:ListItem Selected="True" Value="1">Submit(FC)</asp:ListItem>
                                                            <asp:ListItem Value="2">Approve(BO)</asp:ListItem>
                                                            <asp:ListItem Value="3">Approve(IO)</asp:ListItem>

                                                        </asp:RadioButtonList>

                                                    </div>
                                                </div>
                                            </div>

                                        </div>


                                        <div class="row">
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        School:
                                                    </label>
                                                    <div class="col-sm-9 padd" style="padding: 0px 10px 0px 12px;">
                                                        <asp:DropDownList ID="ddlScholl" runat="server"
                                                            class="form-control ">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px; padding-right: 30px;">
                                                        Type:
                                                    </label>
                                                    <div class="col-sm-9 padd" style="padding: 0px 10px 0px 12px;">
                                                        <asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true"
                                                            class="form-control ">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Date Wise" Value="1"></asp:ListItem>

                                                            <asp:ListItem Text="Weekly" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Month Wise" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Quarter wise" Value="4"></asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group" runat="server" id="divDate">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        From :</label>
                                                    <div class="col-sm-9 padd" style="padding: 0px 10px 0px 12px;">
                                                        <asp:TextBox runat="server" ID="txtDate" class="form-control" onkeypress="return false;"></asp:TextBox>
                                                        <ajax:CalendarExtender ID="CalendarExtenderTourdate" runat="server" Enabled="True"
                                                            Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight">
                                                        </ajax:CalendarExtender>
                                                    </div>



                                                </div>

                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group" runat="server" id="divDate1">
                                                    <label for="email" class="col-sm-3 padd linhei">
                                                        To :</label>
                                                    <div class="col-sm-9 padd" style="padding: 0px 10px 0px 12px;">
                                                        <asp:TextBox runat="server" ID="txtTodate" class="form-control" onkeypress="return false;"></asp:TextBox>
                                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            TargetControlID="txtTodate" PopupPosition="BottomRight">
                                                        </ajax:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>





                                        </div>
                                        <div class="row">

                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group" id="divMonth" runat="server">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px; padding-right: 4px;">
                                                        Month:</label>
                                                    <div class="col-sm-9 padd" style="padding: 0px 10px 0px 12px;">
                                                        <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
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
                                                </div>


                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group" id="divToMonth" runat="server">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px; padding-right: 15px;">
                                                        Month:</label>
                                                    <div class="col-sm-9 padd" style="padding: 0px 10px 0px 12px;">
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
                                                </div>

                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-12 table table-hover " style="padding: 0px;">
                                    <asp:Panel ID="pnlMain" Enabled="true" runat="server">
                                        <div class="form-horizontal">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                <div class="panel-default search-bg" style="height: 30px">
                                                    <span style="float: left; color: Black; margin-left: 12px;">
                                                        <asp:Label ID="lblTotal" Text="Total:" runat="server"></asp:Label>
                                                    </span>
                                                    <span style="float: left; color: Black; margin-left: 12px;">
                                                        <asp:Label ID="lblTotalCount" ForeColor="#737272" Font-Bold="true" runat="server"></asp:Label>
                                                    </span>
                                                </div>
                                                <div style="height: 290px; overflow: auto; width: 99%;" align="center">
                                                    <div>





                                                        <div id="Div19" class="Row" style="width: 100%" runat="server">




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
                                                        </div>




                                                        <div id="Div16" class="Row" style="width: 100%" runat="server">




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
                                                        </div>

                                                        <div id="Div17" class="Row" style="width: 100%" runat="server">




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
                                                        </div>

                                                        <div id="Div13" class="Row" style="width: 100%" runat="server">




                                                            <asp:GridView ID="gvQuerltyAnnual" OnRowDataBound="gvQuerltyAnnual_Report_RowDataBound" AutoGenerateColumns="false" AllowPaging="true" PageSize="30" runat="server" ForeColor="#333333" CellPadding="1"
                                                                CssClass="table table-striped table table-hover table-bordered  " Width="99.7%">

                                                                <EmptyDataTemplate>
                                                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                                                        Data not found
                                                                    </div>
                                                                </EmptyDataTemplate>
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="District Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblD8me" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("DistrictName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="District Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDistri3ctNaf1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("DistrictCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Block Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBlockN3ame" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("BlockName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Block Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDis3tdrictNaf1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("BlockCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ClusterCode" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblP3anchaye" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("ClusterCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ClusterName" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPa3nClusterName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("ClusterName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Panchayat Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPanch3ayatName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("PanchayatName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Panchayat Code" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPanch3ayatdName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("PanchayatCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Village Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVilla3geName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("VillageName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Village Code" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("VillageCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblViNameme" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="DISECODE" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblViDISECodee" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("DISECode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Outcome">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTarOu3tCome1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("DocName") %>'></asp:Label>

                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Annual">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtAnnual" Style="width: 30px; border: none; height: 30px; border-radius: 4px;"
                                                                                runat="server"></asp:TextBox>

                                                                            <asp:Label ID="lblAnnual" Visible="false" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Apr To Jun">
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtQ1" Style="width: 30px; border: none; height: 30px; border-radius: 4px;"
                                                                                runat="server"></asp:TextBox>

                                                                            <asp:Label ID="lblQ1" ForeColor="Black" Visible="false" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Q1") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Jul-Sep">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtQ2" Style="width: 30px; border: none; height: 30px; border-radius: 4px;"
                                                                                runat="server"></asp:TextBox>
                                                                            <asp:Label ID="lblQ2" ForeColor="Black" Visible="false" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Q2") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Oct-Dec">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtQ3" Style="width: 30px; border: none; height: 30px; border-radius: 4px;"
                                                                                runat="server"></asp:TextBox>
                                                                            <asp:Label ID="lblQ3" ForeColor="Black" Visible="false" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Q3") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Jan-Mar">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtQ4" Style="width: 30px; border: none; height: 30px; border-radius: 4px;"
                                                                                runat="server"></asp:TextBox>
                                                                            <asp:Label ID="lblQ4" ForeColor="Black" Visible="false" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Q4") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>



                                                        </div>


                                                        <div id="Div15" class="Row" style="width: 100%" runat="server">
                                                            <asp:GridView ID="GvSip" AutoGenerateColumns="false" AllowPaging="true" PageSize="30" runat="server" ForeColor="#333333" CellPadding="1"
                                                                CssClass="table table-striped table table-hover table-bordered  " Width="99.7%">

                                                                <EmptyDataTemplate>
                                                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                                                        Data not found
                                                                    </div>
                                                                </EmptyDataTemplate>
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="District Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblD8me" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("DistrictName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="District Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDistri3ctNaf1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("DistrictCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Block Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBlockN3ame" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("BlockName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Block Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDis3tdrictNaf1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("BlockCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ClusterCode" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblP3anchaye" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("ClusterCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ClusterName" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPa3nClusterName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("ClusterName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Panchayat Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPanch3ayatName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("PanchayatName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Panchayat Code" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPanch3ayatdName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("PanchayatCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Village Code" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVilla3geName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("VillageName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Village Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("VillageCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblViNameme" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="DISECODE" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblViDISECodee" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("DISECode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblAnnual" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblQ1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("GPrepared") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblQ2" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CPrepared") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblAnPDriking" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPDriking" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PDriking") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblCDriking" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CDriking") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblA3nPDri2king" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPD3riking" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PKitchen") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbl4CDriking" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CKitchen") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>




                                                                    <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblA3nPDri2king" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPDw3riking" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PPtrDriking") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbl4C4Driking" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CPtrDriking") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblA3nPPlaygroundng" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPDPPlaygrounding" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PPlayground") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbl4PPlaygroundg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CPlayground") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblAPElectricityg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPDPPlPElectricityg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PElectricity") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbl4PPPElectricityg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CElectricity") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblAPPHealthCheckUPicityg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPPHealthCheckUPyg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PHealthCheckUP") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPHealthCheckUP" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CHealthCheckUP") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbPClassroomicityg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPClassroomg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PClassroom") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPHePClassroom" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CClassroom") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>






                                                                    <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbPClPSwingsandSlidersityg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPPSwingsandSliders" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PSwingsandSliders") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPHePSwingsandSlidersm" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CSwingsandSliders") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbPBoundaryWalltyg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPPSPBoundaryWallers" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PBoundaryWall") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPBoundaryWall" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CBoundaryWall") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>




                                                                    <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbPPOthersSipltyg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPOthersSips" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("POthersSip") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPPOthersSipyWall" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("COthersSip") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbPClaPGkpityg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPPGkp" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PGkp") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblPHePGkpom" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CGkp") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                            </asp:GridView>



                                                            <%--    <asp:GridView ID="GvSip"     AutoGenerateColumns="false" AllowPaging="true" PageSize="30" runat="server" ForeColor="#333333" CellPadding="1"  
                                                                       CssClass="table table-striped table table-hover table-bordered  " Width="99.7%"      >
                               
                                <EmptyDataTemplate>
                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                        Data not found</div>
                                </EmptyDataTemplate>
                                <Columns>
                                  <asp:TemplateField HeaderText="District Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblD8me" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("DistrictName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="District Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDistri3ctNaf1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("DistrictCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Block Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBlockN3ame" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("BlockName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Block Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDis3tdrictNaf1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("BlockCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ClusterCode" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblP3anchaye" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("ClusterCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ClusterName" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPa3nClusterName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("ClusterName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="Panchayat Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPanch3ayatName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("PanchayatName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Panchayat Code" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPanch3ayatdName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("PanchayatCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Village Code" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVilla3geName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("VillageName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Village Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("VillageCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                     <asp:TemplateField HeaderText="Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblViNameme" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="DISECODE" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblViDISECodee" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("DISECode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                            
                                                                           <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>
                                                                         
                                                                            <asp:Label ID="lblAnnual" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                         <asp:Label ID="lblQ1" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("GPrepared") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>
                                                                          
                                                                            <asp:Label ID="lblQ2" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CPrepared") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                  

                                                                       <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>
                                                                         
                                                                            <asp:Label ID="lblAnPDriking" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                         <asp:Label ID="lblPDriking" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PDriking") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>
                                                                          
                                                                            <asp:Label ID="lblCDriking" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CDriking") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>



                                                                     <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>
                                                                         
                                                                            <asp:Label ID="lblA3nPDri2king" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                         <asp:Label ID="lblPD3riking" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PKitchen") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>
                                                                          
                                                                            <asp:Label ID="lbl4CDriking" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CKitchen") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>




                                                                       <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>
                                                                         
                                                                            <asp:Label ID="lblA3nPDri2king" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                         <asp:Label ID="lblPDw3riking" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PPtrDriking") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>
                                                                          
                                                                            <asp:Label ID="lbl4C4Driking" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CPtrDriking") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>


                                                                    
                                                                       <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>
                                                                         
                                                                            <asp:Label ID="lblA3nPPlaygroundng" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                         <asp:Label ID="lblPDPPlaygrounding" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PPlayground") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>
                                                                          
                                                                            <asp:Label ID="lbl4PPlaygroundg" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CPlayground") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>


                                                                      <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>
                                                                         
                                                                            <asp:Label ID="lblAPElectricityg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                         <asp:Label ID="lblPDPPlPElectricityg" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PElectricity") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>
                                                                          
                                                                            <asp:Label ID="lbl4PPPElectricityg" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CElectricity") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>



                                                                     <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>
                                                                         
                                                                            <asp:Label ID="lblAPPHealthCheckUPicityg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                         <asp:Label ID="lblPPHealthCheckUPyg" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PHealthCheckUP") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>
                                                                          
                                                                            <asp:Label ID="lblPHealthCheckUP" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CHealthCheckUP") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>


                                                                    
                                                                     <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>
                                                                         
                                                                            <asp:Label ID="lbPClassroomicityg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                         <asp:Label ID="lblPClassroomg" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PClassroom") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>
                                                                          
                                                                            <asp:Label ID="lblPHePClassroom" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CClassroom") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>



                                                                    

                                                                    
                                                                      <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>
                                                                         
                                                                            <asp:Label ID="lbPClPSwingsandSlidersityg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                         <asp:Label ID="lblPPSwingsandSliders" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PSwingsandSliders") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>
                                                                          
                                                                            <asp:Label ID="lblPHePSwingsandSlidersm" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CSwingsandSliders") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>



                                                                     <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>
                                                                         
                                                                            <asp:Label ID="lbPBoundaryWalltyg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                         <asp:Label ID="lblPPSPBoundaryWallers" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PBoundaryWall") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>
                                                                          
                                                                            <asp:Label ID="lblPBoundaryWall" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CBoundaryWall") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>



                                                                    
                                                                     <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>
                                                                         
                                                                            <asp:Label ID="lbPPOthersSipltyg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                         <asp:Label ID="lblPOthersSips" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("POthersSip") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>
                                                                          
                                                                            <asp:Label ID="lblPPOthersSipyWall" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("COthersSip") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>


                                                                      <asp:TemplateField HeaderText="Target">
                                                                        <ItemTemplate>
                                                                         
                                                                            <asp:Label ID="lbPClaPGkpityg" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="Prepared">
                                                                        <ItemTemplate>

                                                                         <asp:Label ID="lblPPGkp" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("PGkp") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Completed">
                                                                        <ItemTemplate>
                                                                          
                                                                            <asp:Label ID="lblPHePGkpom" ForeColor="Black"  Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("CGkp") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                  </Columns>
                                                              </asp:GridView>--%>
                                                        </div>
                                                        <div id="Div1" class="Row" style="width: 100%" runat="server">




                                                            <asp:GridView ID="DGV_Report" OnRowDataBound="DGV_Report_RowDataBound" AutoGenerateColumns="false" AllowPaging="true" PageSize="30" runat="server" ForeColor="#333333" CellPadding="1"
                                                                CssClass="table table-striped table table-hover table-bordered  " Width="99.7%">

                                                                <EmptyDataTemplate>
                                                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                                                        Data not found
                                                                    </div>
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



                                                        </div>
                                                        <div id="Div4" class="Row" style="width: 100%" runat="server">




                                                            <asp:GridView ID="gvWeaklly" OnRowDataBound="grdSearchResult_RowDataBound" AutoGenerateColumns="false" AllowPaging="true" PageSize="30" runat="server" ForeColor="#333333" CellPadding="1"
                                                                CssClass="table table-striped table table-hover table-bordered  " Width="99.7%">

                                                                <EmptyDataTemplate>
                                                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                                                        Data not found
                                                                    </div>
                                                                </EmptyDataTemplate>
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="District Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblD8me" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("[District Name]") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="District Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDistrictNaf1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("[District Code]") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Block Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBlockName" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("[Block Name]") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Block Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDistdrictNaf1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("[Block Code]") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cluster Code" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPanchaye" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("[Cluster Code]") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cluster Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPanClusterName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("[Cluster Name]") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Panchayat Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPanchayatName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("[Panchayat Name]") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Panchayat Code" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPanchayatdName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("[Panchayat Code]") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Village Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVillageName" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("[Village Name]") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Village Code" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVillddme" class="labelGrid" ForeColor="Black" runat="server"
                                                                                Text='<%# Eval("[Village Code]") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="OutCome">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTarOutCome" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("OutCome") %>'></asp:Label>
                                                                            <asp:LinkButton ID="LinkButton3" Visible="false" runat="server" Text='<%#Eval("OutCome") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Annual">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldsQ1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Annual") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Q1">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTargQ1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Q1") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Q2">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTargQ2" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Q2") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Q3">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTargQ3" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Q3") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Q4">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblT8argQ3" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Q4") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Q5">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTa78rgQ5" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("Q5") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>



                                                        </div>
                                                        <div class="row" style="width: 100%">
                                                            <asp:GridView ID="GV_DynamicGrid2" runat="server" ForeColor="Black" AllowPaging="true"
                                                                PageSize="100" ShowHeader="true"
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
    </div>
    <cc1:ModalPopupExtender ID="MpexdrDistrict" runat="server" BackgroundCssClass="modalBg "
        CancelControlID="CancelButton" PopupControlID="PnlDistrict" TargetControlID="HdnFild">
    </cc1:ModalPopupExtender>

    <asp:HiddenField ID="HdnFild" runat="server"></asp:HiddenField>

    <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important; margin-top: 88px !important;" ID="PnlDistrict" runat="server">

        <div style="width: 100%; height: auto; background-color: #f1f1f1">
            <div class="modal-header" style="background-color: #3ac0f2; color: White;">
                <h4 class="modal-title" style="forecolor: White">Life Skill Game</h4>
            </div>
            <div class="modal-body">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                <div class="form-horizontal" role="form">
                    <div class="form-group">
                        <label class="control-label col-sm-4 lab-text-left">Life Skill Game 1:</label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="l1" Font-Bold="true"></asp:Label>

                        </div>
                    </div>
                    <div class="form-group" id="statediv" runat="server">

                        <asp:Label ID="Label10" class="control-label col-sm-4 lab-text-left" runat="server" Text="Life Skill Game 2:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" Font-Bold="true" ForeColor="Black" ID="l2"></asp:Label>



                        </div>
                    </div>

                    <div class="form-group" id="distdiv" runat="server">

                        <asp:Label ID="lbldist" class="control-label col-sm-4 lab-text-left" runat="server" Text="Life Skill Game 3:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="l3" Font-Bold="true"></asp:Label>


                        </div>
                    </div>

                    <div class="form-group" id="blockdiv" runat="server">

                        <asp:Label ID="lblBlock" class="control-label col-sm-4 lab-text-left" runat="server" Text="Life Skill Game 4:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="l4" Font-Bold="true"></asp:Label>


                        </div>
                    </div>

                    <div class="form-group" id="partnerdiv" runat="server">

                        <asp:Label ID="lblpartner" class="control-label col-sm-4 lab-text-left" runat="server" Text="Life Skill Game 5:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="l5" Font-Bold="true"></asp:Label>

                        </div>
                    </div>

                </div>


            </div>
            <div class="modal-footer">

                <asp:Button ID="CancelButton" runat="server" CssClass="btn bgm-cyan" Text="Close"
                    ToolTip="Close" Style="float: none;"></asp:Button>
            </div>
        </div>


    </asp:Panel>


    <cc1:ModalPopupExtender ID="MpexdrDistrict1" runat="server" BackgroundCssClass="modalBg "
        CancelControlID="CancelButton" PopupControlID="PnlDistrict1" TargetControlID="HiddenField1">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>

    <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important; margin-top: 88px !important;" ID="PnlDistrict1" runat="server">

        <div style="width: 100%; height: auto; background-color: #f1f1f1">
            <div class="modal-header" style="background-color: #3ac0f2; color: White;">
                <h4 class="modal-title" style="forecolor: White"></h4>
            </div>
            <div class="modal-body">
                <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                <div class="form-horizontal" role="form">
                    <div class="form-group">
                        <label class="control-label col-sm-4 lab-text-left">DRINKING WATER:</label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="A1" Font-Bold="true"></asp:Label>

                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-4 lab-text-left">TOILETS:</label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="A2" Font-Bold="true"></asp:Label>

                        </div>
                    </div>
                    <div class="form-group" id="Div6" runat="server">

                        <asp:Label ID="fff" class="control-label col-sm-4 lab-text-left" runat="server" Text="KITCHEN:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" Font-Bold="true" ForeColor="Black" ID="A3"></asp:Label>



                        </div>
                    </div>

                    <div class="form-group" id="Div7" runat="server">

                        <asp:Label ID="Label5" class="control-label col-sm-4 lab-text-left" runat="server" Text="ELECTRICITY:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="A4" Font-Bold="true"></asp:Label>


                        </div>
                    </div>

                    <div class="form-group" id="Div8" runat="server">

                        <asp:Label ID="Label7" class="control-label col-sm-4 lab-text-left" runat="server" Text="PLAY GROUND:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="A5" Font-Bold="true"></asp:Label>


                        </div>
                    </div>

                    <div class="form-group" id="Div9" runat="server">

                        <asp:Label ID="Label9" class="control-label col-sm-4 lab-text-left" runat="server" Text="SWINGS:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="A6" Font-Bold="true"></asp:Label>

                        </div>
                    </div>


                    <div class="form-group" id="Div10" runat="server">

                        <asp:Label ID="Label12" class="control-label col-sm-4 lab-text-left" runat="server" Text="BOUNDRY WALL:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="A7" Font-Bold="true"></asp:Label>

                        </div>

                    </div>
                    <div class="form-group" id="Div11" runat="server">

                        <asp:Label ID="Label14" class="control-label col-sm-4 lab-text-left" runat="server" Text="Books:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="A8" Font-Bold="true"></asp:Label>

                        </div>
                    </div>
                    <div class="form-group" id="Div12" runat="server">

                        <asp:Label ID="Label16" class="control-label col-sm-4 lab-text-left" runat="server" Text="CLT_Kit :"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="A9" Font-Bold="true"></asp:Label>

                        </div>
                    </div>
                </div>
            </div>



            <div class="modal-footer">

                <asp:Button ID="Button1" runat="server" CssClass="btn bgm-cyan" Text="Close"
                    ToolTip="Close" Style="float: none;"></asp:Button>
            </div>
        </div>








    </asp:Panel>



    <%--        </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="btnexcel" />
           
            </Triggers>
            </asp:UpdatePanel>--%>
</asp:Content>

