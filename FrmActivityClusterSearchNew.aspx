<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FrmActivityClusterSearchNew.aspx.cs" Culture="en-GB" Inherits="FrmActivityClusterSearchNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function SetCollaps() {
            setTimeout(function () {
                $('.clcss').hide();
            });

        }
        function togglediv(id) {
            $("#" + id).toggle();
            return false;
        }
    </script>
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


        .panel-heading .accordion-toggle:after {
            /* symbol for "opening" panels */
            font-family: 'Glyphicons Halflings'; /* essential for enabling glyphicon */
            content: "\e114"; /* adjust as needed, taken from bootstrap.css */
            float: right; /* adjust as needed */
            color: grey; /* adjust as needed */
        }

        /*.padd {
            padding-left: 15px;
            padding-right: 15px;
        }*/

        .panel-heading .accordion-toggle.collapsed:after {
            /* symbol for "collapsed" panels */
            content: "\e080"; /* adjust as needed, taken from bootstrap.css */
        }
    </style>
    <script type="text/javascript">
        debugger;
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
    </script>
    <script type="text/javascript">
        function arrivaldatecheck(sender, args) {
            var depdate = 'dep';

            var departuredate = $('.' + depdate).val();
            var arrivaldate = sender._selectedDate;
            var today = new Date();




            if (sender._selectedDate > today) {
                alert("Should not be future date.");
                sender._textbox.set_Value("")

                return false;

            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="padding: 5px 15px;">
                            <h3 class="text-danger" style="margin: 0px;">School Activity <span class="pull-right" style="font-size: 17px;">
                                <asp:LinkButton ID="btnexcel" Visible="false" runat="server" Text="Export to Excel"
                                    OnClick="Export_To_Excel"></asp:LinkButton>
                            </span>
                            </h3>
                        </div>
                    </div>
                    <div class="row">
                        <div class="row marg search-bg" style="margin-top: 0px">
                            <div class="form-horizontal">
                                <%-- <asp:UpdatePanel runat="server" ID="UpMain" UpdateMode="Conditional">
        <ContentTemplate>--%>
                                <%--          <div class="col-lg-12 col-md-10 col-sm-10 cpl-xs-12">--%>
                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                    <div class="form-group" style="margin-bottom: 7px; margin-left: 1px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            Block:</label>
                                        <div class="col-sm-9 padd">
                                            <asp:DropDownList ID="ddlBlock" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_OnSelectedIndexChanged" runat="server" class="form-control ">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                    <div class="form-group" style="margin-bottom: 7px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            From Date:</label>
                                        <div class="col-sm-9 padd">
                                            <asp:TextBox runat="server" ID="TxtFromDate" Enabled="false" autocomplete="off" ondrop="return false;"
                                                class="form-control" onkeypress="return false;"></asp:TextBox>
                                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                TargetControlID="TxtFromDate" PopupPosition="BottomRight">
                                            </ajax:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtFromDate"
                                                Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                    <div class="form-group" style="margin-bottom: 7px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            Date:</label>
                                        <div class="col-sm-9 padd">
                                            <asp:TextBox runat="server" ID="txtDate" autocomplete="off" ondrop="return false;"
                                                class="form-control" onkeypress="return false;"></asp:TextBox>
                                            <ajax:CalendarExtender ID="CalendarExtenderTourdate" OnClientDateSelectionChanged="arrivaldatecheck"
                                                runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight">
                                            </ajax:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="ReqTxtDate" runat="server" ControlToValidate="txtDate"
                                                Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3  col-sm-3 cpl-xs-12">
                                    <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" OnClick="btnSerach_Click"
                                        class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png"
                                        Style="margin-left: 5px;" />
                                    <asp:Button ID="btnApprove" CssClass="btn btn-success pull-left" ToolTip="Save"
                                        Text="Approve" Visible="false" OnClick="btnApprove_Click" Style="margin-right: 5px; margin-left: 8px;"
                                        runat="server" />
                                    <asp:Button ID="btnReport" OnClick="btnReport_Click" Visible="false" CssClass="btn btn-success pull-left"
                                        ToolTip="Save" Text="Report" Style="margin-right: 5px;" runat="server" />
                                    <asp:Button ID="btnBack" CssClass="btn btn-success pull-left " ToolTip="Save" Text="Back"
                                        Visible="false" OnClick="btnBack_Click" Style="margin-left: 8px;display:none; margin-right: 5px;"
                                        runat="server" />


                                    <%-- <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                          <asp:Button ID="Button1"   CssClass="btn btn-success pull-right" 
                                 ToolTip="Save" Text="Report"  
                                Style="margin-right: 5px; padding: 0px;" runat="server" /></div>
                              
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server"  OnClick="btnSerach_Click"  class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1"
                                ImageUrl="~/images/search-29.png"  Style="margin-left: -49px; padding: 0px;"   />
                                </div>
                      <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                          <asp:Button ID="btnApprove"   CssClass="btn btn-success pull-right" 
                                 ToolTip="Save" Text="Approve"  Visible="false"    OnClick="btnApprove_Click" 
                                Style="margin-right: 5px; padding: 0px;" runat="server" /></div>
                                    --%>
                                </div>
                                <%--</div>--%>
                            </div>
                        </div>



                        <div class="row">
                            <div class="col-sm-12" style="padding: 0px">
                                <div class="panel-group" id="accordion">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
                                                    <span style="color: blue">School Activity </span>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapseOne" class="panel-collapse collapse">
                                            <div class="panel-body">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                    <div style="height: 330px; overflow: auto; width: 100%;" align="center">
                                                        <asp:GridView ID="Gv_Profile_Search" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                            AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                                                            Font-Size="11px" Width="100%">
                                                            <EmptyDataTemplate>
                                                                <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                    Data not found
                                                                </div>
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
                                                                <asp:TemplateField HeaderText="School Activity" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtn" OnClick="LnkSchool_OnClick" runat="server" Text='<%# Bind("School") %>'
                                                                            CommandArgument='<%# Bind("School") %>'></asp:LinkButton>
                                                                        <asp:Label ID="lblUn1" Visible="false" ForeColor="Black" Text='<%# Bind("School") %>'
                                                                            Font-Names="Calibri" ItemStyle-ForeColor="#333" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_2" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_3" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_4" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="5%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_5" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_6" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_7" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_8" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_9" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_10" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_11" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_12" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_13" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_14" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_15" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_16" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_17" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_18" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_19" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_20" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_21" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_22" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_23" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_24" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_25" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="TxtTotla" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">
                                                    <span style="color: blue">Village Activity</span>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapseTwo" class="panel-collapse collapse">
                                            <div class="panel-body">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                    <div style="height: 390px; overflow: auto; width: 100%;" align="center">
                                                        <asp:GridView ID="gvVillageActivity" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                            AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                                                            Font-Size="11px" Width="100%">
                                                            <EmptyDataTemplate>
                                                                <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                    Data not found
                                                                </div>
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
                                                                <asp:TemplateField HeaderText="Village Activity" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnVillage" OnClick="LnkVillage_OnClick" runat="server" Text='<%# Bind("Village") %>'
                                                                            CommandArgument='<%# Bind("Village") %>'></asp:LinkButton>
                                                                        <asp:Label ID="lblvllV_2" Text='<%# Bind("Village") %>' Visible="false" ForeColor="Black"
                                                                            Font-Names="Calibri" ItemStyle-ForeColor="#333" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_2" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_3" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_4" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_5" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_6" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_7" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_8" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_9" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_10" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_11" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_12" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_13" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_14" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_15" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_16" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_17" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_18" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_19" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColV_20" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_21" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_22" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_23" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_24" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_25" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="TxtTotlaV" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseThree">
                                                    <span style="color: blue">Office Activity </span>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapseThree" class="panel-collapse collapse">
                                            <div class="panel-body">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                    <div style="height: 220px; overflow: auto; width: 100%;" align="center">
                                                        <asp:GridView ID="gvOffice" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                            AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                                                            Font-Size="11px" Width="100%">
                                                            <EmptyDataTemplate>
                                                                <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                    Data not found
                                                                </div>
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
                                                                <asp:TemplateField HeaderText="Office Activity" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnOffice" OnClick="LnkOffice_OnClick" runat="server" Text='<%# Bind("Village") %>'
                                                                            CommandArgument='<%# Bind("Village") %>'></asp:LinkButton>
                                                                        <asp:Label ID="lbooff" Text='<%# Bind("Village") %>' Visible="false" ForeColor="Black"
                                                                            Font-Names="Calibri" ItemStyle-ForeColor="#333" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_2" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_3" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_4" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_5" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_6" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_7" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_8" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_9" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_10" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_11" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_12" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_13" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_14" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_15" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_16" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_17" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_18" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_19" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_20" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_21" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_22" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_23" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_24" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColO_25" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="TxtTotlaO" ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="Hdn_model3"
                PopupControlID="pnlpopup3" CancelControlID="btnAdd" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:HiddenField ID="Hdn_model3" runat="server" />
            <asp:Panel ID="pnlpopup3" runat="server">
                <div class=" modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">

                            <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                ToolTip="Add" ImageUrl="~/images/close-29.png" Style="margin-right: 5px; padding: 0px;"
                                runat="server" />
                            <h4 class="modal-title"></h4>
                        </div>

                        <div class="row table-responsive">
                            <div style="height: 400px; overflow: auto; width: 100%;" align="center">
                                <asp:GridView ID="gvVillageWise" runat="server" CssClass="table table-striped table-bordered table-hover"
                                    AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                                    Font-Size="11px" Width="100%">
                                    <EmptyDataTemplate>
                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                            Data not found
                                        </div>
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
                                        <asp:TemplateField HeaderText="School Activity" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSchool" runat="server" Text='<%# Eval("ActivityDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Village">
                                            <ItemTemplate>
                                                <asp:Label ID="lblvillage" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                    Text='<%# Eval("VillageName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="School">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSchool" Text='<%# Eval("SchoolName") %>' ForeColor="Black" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <asp:GridView ID="gvVillageDeatial" runat="server" CssClass="table table-striped table-bordered table-hover"
                                    AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                                    Font-Size="11px" Width="100%">
                                    <EmptyDataTemplate>
                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                            Data not found
                                        </div>
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
                                        <asp:TemplateField HeaderText="Village Activity" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrrchool" runat="server" Text='<%# Eval("ActivityDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Village">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrrvillage" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                    Text='<%# Eval("VillageName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <asp:GridView ID="gvVillageOffice" runat="server" CssClass="table table-striped table-bordered table-hover"
                                    AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                                    Font-Size="11px" Width="100%">
                                    <EmptyDataTemplate>
                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                            Data not found
                                        </div>
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
                                        <asp:TemplateField HeaderText="Office Activity" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblhhool" runat="server" Text='<%# Eval("ActivityDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Village">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffvillage" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                    Text='<%# Eval("VillageName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <cc1:ModalPopupExtender ID="ModalPopupExtender43" runat="server" TargetControlID="Hdn_model43"
                PopupControlID="pnlpopup43" CancelControlID="btnAdd" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:HiddenField ID="Hdn_model43" runat="server" />
            <asp:Panel ID="pnlpopup43" runat="server">
                <div class=" modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">

                            <asp:ImageButton ID="ImageButton1" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                ToolTip="Add" ImageUrl="~/images/close-29.png" Style="margin-right: 5px; padding: 0px;"
                                runat="server" />
                            <h4 class="modal-title"></h4>
                        </div>

                        <div class="row table-responsive">
                            <div style="height: 400px; overflow: auto; width: 99%;" align="center">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-hover"
                                    AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                                    Font-Size="11px" Width="100%">
                                    <EmptyDataTemplate>
                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                            Data not found
                                        </div>
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
                                        <asp:TemplateField HeaderText="School Activity" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSchool" runat="server" Text='<%# Eval("ActivityDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Village">
                                            <ItemTemplate>
                                                <asp:Label ID="lblvillage" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                    Text='<%# Eval("VillageName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="School">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSchool" Text='<%# Eval("SchoolName") %>' ForeColor="Black" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <asp:GridView ID="GridView2" runat="server" CssClass="table table-striped table-bordered table-hover"
                                    AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                                    Font-Size="11px" Width="100%">
                                    <EmptyDataTemplate>
                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                            Data not found
                                        </div>
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
                                        <asp:TemplateField HeaderText="Village Activity" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrrchool" runat="server" Text='<%# Eval("ActivityDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Village">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrrvillage" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                    Text='<%# Eval("VillageName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <asp:GridView ID="GridView3" runat="server" CssClass="table table-striped table-bordered table-hover"
                                    AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                                    Font-Size="11px" Width="100%">
                                    <EmptyDataTemplate>
                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                            Data not found
                                        </div>
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
                                        <asp:TemplateField HeaderText="Office Activity" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblhhool" runat="server" Text='<%# Eval("ActivityDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Village">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffvillage" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                    Text='<%# Eval("VillageName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
