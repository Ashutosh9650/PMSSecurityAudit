<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" Culture="en-GB" AutoEventWireup="true"
    CodeFile="frmanalysisNewReport.aspx.cs" Inherits="frmanalysisNewReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
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
    </script>
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
            text-align:left  !important;
           color:Black;
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
            width: 150px !important;
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
        
              
 input[type="radio"], input[type="checkbox"] {
    margin: 4px 7px 0px!important;
    margin-top: 1px !important;
    line-height: normal !important;
    </style>
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <%-- <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>--%>
    <div class="container-fluid" >
    <div class="row">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
    <div class="thumbnail" style="background-color: #f5f5f5; margin-bottom:3px !important;">
                           <div class="panel-heading">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h3 class="text-danger" style="margin: 0px;">
                                        Door to Door Analysis</h3>
                                </div>
                              
                            </div>
                        </div>
                   </div>
   
    </div>
    </div>
        <div  class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 text-left" style="padding-right: 0px;">
        <div class="thumbnail" style="background-color: rgba(20, 18, 18, 0.59); float: left;">
       
        <div class="li-width" style="min-height: 110px;">
         <img src="images/business-report.jpg" width="100%" />
       <%-- <div style="width:30%; float:left;">
            <img src="images/report-icon.gif" width="100%" />
        </div>
        <div style="width:70%; float:left; height:100%; background-color:Blue; " >
            Reports
            </div>--%>
        </div>
            <ul class="nav navbar-nav" style="margin: 0px">
                <li class=" active li-width">
                    <asp:LinkButton ID="Button3"  runat="server" Text="Gender and Age wise " style="color: white;" OnClick="btnAge_Click"></asp:LinkButton>
                </li>
                <li class="li-width">
                    <asp:LinkButton ID="ff"  runat="server" Text="Social Category " style="color: white;" OnClick="btnSerach_Click"></asp:LinkButton></li>
                <li class="li-width">
                    <asp:LinkButton ID="Button4"  runat="server" Text="Education Status" style="color: white;" OnClick="Education_Click"></asp:LinkButton></li>
                <li class="li-width">
                    <asp:LinkButton ID="Button2" runat="server" Text="Family Occupation" style="color: white;" OnClick="btnUser_Click"></asp:LinkButton></li>
                <li class="li-width">
                    <asp:LinkButton ID="Button5" runat="server" Text="Grade wise Category" style="color: white;" OnClick="btnGrade_Click"></asp:LinkButton></li>

                    <li class="li-width">
                    <asp:LinkButton ID="LinkButton1" runat="server" Visible="false" Text="Enrollment Plan" style="color: white;" OnClick="btnEnroll_Click"></asp:LinkButton></li>

                    <li class="li-width">
                    <asp:LinkButton ID="LinkButton2" runat="server" Text="Reason Analysis OOS"  style="color: white;" OnClick="btnReason_Click"></asp:LinkButton></li>
                <li class="li-width">
                    <asp:LinkButton ID="Button1" runat="server" Visible="false" Text="Report D2D" style="color: white;" OnClick="btnD2d_Click"></asp:LinkButton></li>

                <li class="li-width">
                    <asp:LinkButton ID="LinkButton3" runat="server" Text="Enrollment Analysis " Visible="false" style="color: white;" OnClick="btnAnalayis_Click"></asp:LinkButton></li>

            </ul>
            </div>
        </div>
                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12" style="padding-left: 1px;">
                <div class="thumbnail" style="background-color: #f5f5f5;float: left; width: 100%;">
                    <div class="panel panel-default">
                       
                        <div class="form-horizontal">
                            <div class="row">
                                <div id="div-show-new">
                                    <div class="row marg search-bg">
                                        <div class="form-horizontal">
                                              <div class="row">
                                        
                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Block Type:</label>
                                                    <div class="col-sm-8 padd ">
                                                        <asp:RadioButtonList ID="rblBlockType" AutoPostBack="true" OnSelectedIndexChanged="rblBlockType_SelectedIndexChanged"  CssClass="cr-icon" ForeColor="Black" RepeatDirection="Horizontal" runat="server">
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
                                                    <div class="col-sm-8 padd">
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
                                                     <div class="col-sm-8 padd CheckBoxListCssClass " style="border:1px solid #c1c1c1"">

                                                        <div style="overflow: auto; margin-top:2px; height:50px; ">
                                                        <asp:CheckBoxList ID="ChkState" RepeatDirection="Vertical"  OnSelectedIndexChanged="ddlState_SelectedIndexChanged"   AutoPostBack="true" runat="server">
                                                        </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        District:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass " style="border:1px solid #c1c1c1">
                                                    <div style="overflow: auto; margin-top:2px; height:50px; ">
                                                        <asp:CheckBoxList ID="chkDistrict" RepeatDirection="Vertical"  OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"   AutoPostBack="true" runat="server">
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
                                                    <div class="col-sm-8 padd CheckBoxListCssClass" style="border:1px solid #c1c1c1">
                                                    <div style="overflow: auto; margin-top:2px; height:50px; ">
                                                    <asp:CheckBoxList ID="chkBlock" RepeatDirection="Vertical"   OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                        </asp:CheckBoxList>
                                                      
                                                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                          <div id="Div1" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Panchayat:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass" style="border:1px solid #c1c1c1">
                                                     <div style="overflow: auto; margin-top:2px; height:50px; ">
                                                    <asp:CheckBoxList ID="ddlPanchayat" RepeatDirection="Vertical"   OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"  AutoPostBack="true" runat="server">
                                                        </asp:CheckBoxList>
                                                       
                                                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div17" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Village:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass" style="border:1px solid #c1c1c1">
                                                     <div style="overflow: auto; margin-top:2px; height:50px; ">
                                                      <asp:CheckBoxList ID="chkVillage" RepeatDirection="Vertical"    runat="server">
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
                                                    <div class="col-sm-8 padd">
                                                         <asp:DropDownList ID="ddlUser" runat="server"  OnSelectedIndexChanged="ddlUser_SelectedIndexChanged" AutoPostBack="true" class="form-control ">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">District Wise </asp:ListItem>
                                                                <asp:ListItem Value="2">Block Wise</asp:ListItem>
                                                                <asp:ListItem Value="3">Panchayat Wise</asp:ListItem>
                                                                <asp:ListItem Value="4">Village Wise</asp:ListItem>
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
                                                    <div class="col-sm-8 padd">
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
                                                    <div class="col-sm-8 padd">
                                                        <asp:TextBox runat="server" ID="txtTodate" class="form-control" onkeypress="return false;"></asp:TextBox>
                                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            TargetControlID="txtTodate" PopupPosition="BottomRight">
                                                        </ajax:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                          
                                      
                                        </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12 table table-hover " style="padding: 0px;">
                                        <asp:Panel ID="pnlMain" Enabled="false" runat="server">
                                            <div class="form-horizontal">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                    <div style=" height: 290px; overflow:auto;  width: 99%;" align="center">
                                                        <div>
                                                            <div class="Row" style="width: 100% ">
                                                                <rsweb:ReportViewer ID="rptD2D"  runat="server" Style="width: 100%; "
                                                                    AsyncRendering="False" SizeToReportContent="True" PageCountMode="Actual" width="100%" height="100%" >
                                                                </rsweb:ReportViewer>



<%--
<rsweb:ReportViewer ID="rptD2D" runat="server" Width="740" Height="550" BackColor="#F2F5F2" SizeToReportContent="true"
 ExportContentDisposition="AlwaysInline" ProcessingMode="Remote" ShowToolBar="true" ShowPageNavigationControls="true" 
 ShowZoomControl="true" ShowDocumentMapButton="true" ShowFindControls="true" ShowBackButton="true" ShowExportControls="true" 
 ShowPrintButton="true" ShowParameterPrompts="true" ToolBarItemBorderStyle="Solid" ShowRefreshButton="false" ToolBarItemBorderColor="Black" EnableTheming="true" AsyncRendering="true"  >
</rsweb:ReportViewer>--%>
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
                        <!-- /#wrapper -->
                        <!-- /#wrapper -->
                    </div>
                </div></div>
       
        <script type="text/javascript">
            $(function () {
                $('#datetimepicker4').datetimepicker();
            });
        </script>
    </div>
   <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
