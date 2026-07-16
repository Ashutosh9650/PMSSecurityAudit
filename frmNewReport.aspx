<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" Culture="en-GB" AutoEventWireup="true"
    CodeFile="frmNewReport.aspx.cs" Inherits="frmNewReport" %>

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
                    <asp:LinkButton ID="Button4"  runat="server" Text="Education status" style="color: white;" OnClick="Education_Click"></asp:LinkButton></li>
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
                                            <%-- <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>--%>
                                            <div class="row">

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
                                                        <div class="col-sm-8 padd">
                                                            <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                                AutoPostBack="true" class="form-control ">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            District:</label>
                                                        <div class="col-sm-8 padd">
                                                            <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                                AutoPostBack="true" class="form-control " />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Block:</label>
                                                        <div class="col-sm-8 padd">
                                                            <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                                class="form-control " />
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                            </div>
                                            <div class="row">
                                            <div id="Div1" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Panchayat:</label>
                                                        <div class="col-sm-8 padd">
                                                            <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"
                                                                class="form-control " />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Village:</label>
                                                        <div class="col-sm-8 padd">
                                                            <asp:DropDownList ID="ddlVillage" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged"
                                                                AutoPostBack="true" runat="server" class="form-control " />
                                                            <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                                                                    ValidationGroup="saves" ControlToValidate="ddlVillage" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
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
                                             

                                                       <div class="row">
                                        
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Grouping:
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
