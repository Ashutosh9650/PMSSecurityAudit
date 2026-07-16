<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Culture="en-GB" CodeFile="frmUserEntryReports.aspx.cs" Inherits="frmUserEntryReports" %>

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
    <script language="Javascript" type="text/javascript">

        function onlyAlphabets(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 0 || charCode == 127 || charCode == 32 || charCode == 08 || charCode == 09 || charCode == 13)
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }
 
    </script>
    <script language="Javascript" type="text/javascript">

        function onlyAlphabetsAdd(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 32 || charCode == 0 || charCode == 9 || charCode == 08 || charCode == 44 || charCode == 45 || charCode == 48)
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }
 
    </script>
    <script type="text/javascript">


        function isNumberKey(txt, evt) {
            debugger;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                if (txt.value.indexOf('.') === 1) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                else {
                    return true;
                }
            }
        }
    </script>
    <script type="text/javascript">


        function phonenumber(inputtxt, txtid) {
            var phoneno = /^\d{10}$/;
            if (phoneno.test(inputtxt) && inputtxt.length == 10) {
                $("." + txtid).css("border", "solid 1px green")
                return true;
            }
            else {
                $("." + txtid).css("border", "solid 1px red")
                $("." + txtid).val('');
                alert("Mobile No. should be 10 digit");

                return false;
            }
        }  
    
    </script>
    <script type="text/javascript">

        function Valdation(txtcls, txtaBoy) {
            var Eboy = 0;
            var Aboy = 0;
            $("." + txtcls).each(function (index, value) {
                if ($.trim($(this).val()) != "")
                    if (!isNaN($(this).val()))

                        Eboy = parseFloat($("." + txtaBoy).val());
                Aboy = parseFloat($("." + txtcls).val());

                if (Aboy < Eboy) {

                    alert("Enrollment  should be higher or equal to Appeared");
                    $("." + txtcls).focus();
                    $("." + txtaBoy).val('');
                    return true;
                }
                else {
                    return true;
                }

            });




        }
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
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="panel panel-default " style="background-color: #f5f5f5; margin-bottom: 0px !important;">
                            <div class="panel-heading"  style="padding: 5px 0px;">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                    <div id="Div2" class="col-lg-10 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                        <div class="">
                                        <h3 class="text-danger" style="margin: 0px;">
                                            Report
                                            </h3>
                                            </div>
                                            </div> 
                                            <div id="Div3" class="col-lg-2 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                        <div class="form-group">
                                            
                                                <asp:LinkButton ID="LinkButton1" runat="server" Text="Export to Excel" OnClick="btnImport_Click" class="pull-right"></asp:LinkButton>
                                           <%--</div>
                                           
                                           <span class="pull-right" style="font-size: 17px;"></span>
                                        <div id="Div1" class="col-lg-2 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                        <div class="form-group">--%>
                                                        
                                            <asp:LinkButton ID="lnkCSV" runat="server" Text="Export to CSV" OnClick="btnCSV_Click" ></asp:LinkButton>
                                            </div>
                                            </div>  
                                            </div>              
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
               
                <div class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 text-left" style="padding-right: 0px;">
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
                                <asp:LinkButton ID="Button3" runat="server" Text="Door to Door Summary " Style="color: white;"
                                    OnClick="btnSerach_Click"></asp:LinkButton>
                            </li>
                            <li class="li-width">
                                <asp:LinkButton ID="ff" runat="server" Text="Door to Door Date-wise" Style="color: white;"
                                    OnClick="btnUser_Click"></asp:LinkButton></li>
                            <li class="li-width">
                                <asp:LinkButton ID="Button4" Visible="false"  runat="server" Text="Report D2D" Style="color: white;"
                                    OnClick="btnD2d_Click"></asp:LinkButton></li>
                                    <li class="li-width">
                                <asp:LinkButton ID="LinkButton5" Visible="false" runat="server" Text="Out of D2D Servey" Style="color: white;"
                                    OnClick="btnOuterD2d_Click"></asp:LinkButton></li>
                            <li class=" active li-width">
                                <asp:LinkButton ID="LinkButton2" Visible="true" runat="server" Text="Enrollment Summary "
                                    Style="color: white;" OnClick="btnEnroll_Click"></asp:LinkButton>
                            </li>
                            <li class="li-width">
                                <asp:LinkButton ID="LinkButton3"  runat="server" Text="Enrollment Date-wise"
                                    Style="color: white;" OnClick="btnUserDeatils_Click"></asp:LinkButton></li>
                            <li class="li-width">
                                <asp:LinkButton ID="LinkButton4" Visible="false" runat="server" Text="Enrolllment"
                                    Style="color: white;" OnClick="btnEnrolllment_Click"></asp:LinkButton></li>
                            <li class="li-width">
                                <asp:LinkButton ID="LnkMasterDate" Visible="false" runat="server" Text="Master Data"
                                    Style="color: white;" OnClick="LnkMasterData_OnClick"></asp:LinkButton></li>
                            <li class="li-width">
                                <asp:LinkButton ID="LnkTeamBalika" Visible="false" runat="server" Text="Team Balika"
                                    Style="color: white;" OnClick="LnkTeamBalika_OnClick"></asp:LinkButton></li>
                            <li class="li-width">
                                <asp:LinkButton ID="LnkUserRole" Visible="false" runat="server" Text="User Role" Style="color: white;"
                                    OnClick="LnkUserRole_OnClick"></asp:LinkButton></li>

                                    <li class=" active li-width">
                                <asp:LinkButton ID="LinkButton6" runat="server" Visible="false" Text="Village Report Card " Style="color: white;"
                                    OnClick="PMS_Click"></asp:LinkButton>

                            </li>
                               <li class=" active li-width">
                                <asp:LinkButton ID="LinkButton7" runat="server" Visible="false" Text="School Report Card " Style="color: white;"
                                    OnClick="School_Click"></asp:LinkButton>

                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12" style="padding-left: 1px;">
                    <div class="thumbnail" style="background-color: #f5f5f5; float: left; width: 100%;">
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

                                          <div id="Div14" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Panchayat:</label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"
                                                            class="form-control " />
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Diwv14" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
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
                                                        User:
                                                    </label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlUser" runat="server" class="form-control ">
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
                                            <asp:Panel ID="pnlMain" Enabled="true" runat="server">
                                                <div class="form-horizontal">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                    <div class="panel-default search-bg" style="height:30px">
                                                    <span style=" float:left; color:Black; margin-left:12px;">
                                                    <asp:Label ID="lblTotal" Text="Total:" runat="server"></asp:Label>
                                                    </span>
                                                      <span style=" float:left; color:Black; margin-left:12px;" >
                                                    <asp:Label ID="lblTotalCount" ForeColor="#737272" Font-Bold="true"  runat="server"></asp:Label>
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
                                                                            <asp:TemplateField HeaderText="Creation Date">
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
                                                                    <asp:GridView ID="GvReport" OnRowDataBound="GvReport_RowDataBound" Visible="false"
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
                                                                            <asp:TemplateField HeaderText="State Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStateName" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                        runat="server" Text='<%#Eval("StateName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                            </asp:TemplateField>
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
                                                                            <asp:TemplateField HeaderText="User Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblName" ForeColor="Black" ItemStyle-ForeColor="#333" Font-Names="Calibri"
                                                                                        runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Role">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRole" ForeColor="Black" ItemStyle-ForeColor="#333" Font-Names="Calibri"
                                                                                        runat="server" Text='<%#Eval("Role") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle HorizontalAlign="Left" Width="12%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Records added">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAddress" class="labelGrid" ForeColor="Black" Font-Names="Calibri"
                                                                                        ItemStyle-ForeColor="#333" runat="server" Text='<%#Eval("CountCreate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="HeaderStyle GridHeaderClass" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Records Modified">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblphoneNo" class="labelGrid" ForeColor="Black" runat="server" Font-Names="Calibri"
                                                                                        Text='<%#Eval("countModify") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="HeaderStyle GridHeaderClass" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Records Delete">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblphoneNo1" class="labelGrid" ForeColor="Black" Font-Names="Calibri"
                                                                                        runat="server" Text='<%#Eval("countDelete") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="HeaderStyle GridHeaderClass" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Records Verify">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblphoneNo2" class="labelGrid" ForeColor="Black" runat="server" Font-Names="Calibri"
                                                                                        Text='<%#Eval("CountVerify") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="HeaderStyle GridHeaderClass" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <asp:GridView ID="gvD2d" runat="server" Visible="false" OnPageIndexChanging="gvD2d_PageIndexChanging"
                                                                        AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                                                                        Font-Size="12px" Width="250%">
                                                                        <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
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
                                                                            <asp:TemplateField HeaderText="UniqueId" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lUniqueId" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("UniqueId") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Survay Date" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSurvayDate" class="labelGrid" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("SurvayDate") %>'></asp:Label>
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
                                                                            <asp:TemplateField HeaderText="Fathers Name" Visible="true">
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
                                                                    <asp:GridView ID="gvnroll" runat="server" Visible="false" AllowPaging="true" PageSize="100" OnPageIndexChanging="gvnroll_OnPageIndexChanging"
                                                                        AutoGenerateColumns="False" Font-Names="Arial" Font-Size="12px" Width="200%">
                                                                        <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
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
                                                                            <asp:TemplateField HeaderText="Panchayat Name" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPanchayatName" ForeColor="Black" runat="server" Text='<%# Eval("PanchayatName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Block Name" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblBlockName" ForeColor="Black" runat="server" Text='<%# Eval("BlockName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Village Name" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVillageName" ForeColor="Black" runat="server" Text='<%# Eval("VillageName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Unique ID" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSurvayD3ate" ForeColor="Black" runat="server" Text='<%# Eval("Uniqueid") %>'></asp:Label>
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
                                                                            <asp:TemplateField HeaderText="Fathers Name" Visible="true">
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
                                                                <asp:GridView ID="GV_DynamicGrid" runat="server" ForeColor="Black" AllowPaging="true" OnPageIndexChanging="GV_DynamicGrid_OnPageIndexChanging"
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
                                                                </asp:GridView>
                                                            </div>
                                                            <div class="row" style="width: 250%">
                                                                <asp:GridView ID="GV_DynamicGrid1" runat="server" ForeColor="Black" AllowPaging="true" OnPageIndexChanging="GV_DynamicGrid1_OnPageIndexChanging"
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
                                                                </asp:GridView>
                                                            </div>
                                                            <div class="row" style="width: 100%">
                                                                <asp:GridView ID="GV_DynamicGrid2" runat="server" ForeColor="Black" AllowPaging="true"  OnPageIndexChanging="GV_DynamicGrid2_OnPageIndexChanging"
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
                                                                </asp:GridView>
                                                            </div>
                                                            
                                                            <div class="row" style="width: 100%">
                                                             

                                            

                                        <asp:GridView ID="DGV_Report" AutoGenerateColumns="false" AllowPaging="true" PageSize="30" runat="server" ForeColor="#333333" CellPadding="1"  
                                                                       CssClass="table table-striped table table-hover table-bordered  " Width="99.7%"      >
                               
                                <EmptyDataTemplate>
                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                        Data not found</div>
                                </EmptyDataTemplate>
                                <Columns>
                                     <asp:TemplateField HeaderText="OutCome">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTarOutCome" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("OutCome") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target (Till month)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTargetTillmonth" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("TargetTillmonth") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="Achievement (Till Date)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTargetTillmonth" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%#Eval("AchievementTillDate") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:TemplateField>
                                </Columns>
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
            <script type="text/javascript">
                $(function () {
                    $('#datetimepicker4').datetimepicker();
                });
            </script>
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
