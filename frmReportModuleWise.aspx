<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"   Culture="en-GB"
    CodeFile="frmReportModuleWise.aspx.cs" Inherits="frmReportModuleWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
    </style>
    <style>
        .pagination-ys
        {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }
        
        .pagination-ys table > tbody > tr > td
        {
            display: inline;
        }
        
        .pagination-ys table > tbody > tr > td > a, .pagination-ys table > tbody > tr > td > span
        {
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
        
        .pagination-ys table > tbody > tr > td > span
        {
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
        
        .pagination-ys table > tbody > tr > td:first-child > a, .pagination-ys table > tbody > tr > td:first-child > span
        {
            margin-left: 0;
            border-bottom-left-radius: 4px;
            border-top-left-radius: 4px;
        }
        
        .pagination-ys table > tbody > tr > td:last-child > a, .pagination-ys table > tbody > tr > td:last-child > span
        {
            border-bottom-right-radius: 4px;
            border-top-right-radius: 4px;
        }
        
        .pagination-ys table > tbody > tr > td > a:hover, .pagination-ys table > tbody > tr > td > span:hover, .pagination-ys table > tbody > tr > td > a:focus, .pagination-ys table > tbody > tr > td > span:focus
        {
            color: Black;
            background-color: #eeeeee;
            border-color: #dddddd;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid" >
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="panel panel-default " style="background-color: #f5f5f5; margin-bottom: 0px !important;">
                    <div class="panel-heading" style="padding: 5px 0px;">
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
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Export to Excel" OnClick="btnImport_Click"
                                            class="pull-right"></asp:LinkButton>
                                        <%--</div>
                                           
                                           <span class="pull-right" style="font-size: 17px;"></span>
                                        <div id="Div1" class="col-lg-2 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                        <div class="form-group">--%>
                                        <asp:LinkButton ID="lnkCSV" runat="server" Text="Export to CSV" OnClick="btnCSV_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%-- <div class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 text-left" style="padding-right: 0px;">
            <div style="overflow: auto; margin-top: 0px; height: 480px;">
                <div class="thumbnail" style="background-color: rgba(20, 18, 18, 0.59); float: left;">
                    <div class="li-width" style="min-height: 110px;">
                        <img src="images/business-report.jpg" width="100%" />
                    </div>
                    <ul class="nav navbar-nav" style="margin: 0px">
                        <li class="li-width">
                            <asp:LinkButton ID="BtnDateWiseAll" runat="server" Text="Date Wise All Modules report of a User"
                                Style="color: white;" OnClick="BtnDateWiseAll_Click"></asp:LinkButton></li>
                        <li class="li-width">
                            <asp:LinkButton ID="LnkAllUsers" runat="server" Text="Date Wise All Users report of a Module"
                                Style="color: white;" OnClick="LnkAllUsers_Click"></asp:LinkButton></li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LnkAllModuleTimePeriod" runat="server" Text="All Module report of All Users for specified time period"
                                Style="color: white;" OnClick="LnkAllModuleTimePeriod_Click"></asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>--%>
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left: 1px;">
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
                                                            Type:</label>
                                                        <div class="col-sm-8 padd">
                                                            <asp:DropDownList ID="ddlType" runat="server" class="form-control" OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged"
                                                                AutoPostBack="true">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Module Wise" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="User Wise" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Period:</label>
                                                        <div class="col-sm-8 padd">
                                                            <asp:DropDownList ID="ddlPeriod" runat="server" class="form-control " AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlPeriod_OnSelectedIndexChanged">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Month" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Year" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Specify Period" Value="3"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                 <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server" id="divYear">
                                                    <div class="form-group">
                                                        <div class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            <asp:Label ID="lblTestYear" Text="Year" runat="server" ForeColor="Black"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-8 padd">
                                                       
                                                                <asp:DropDownList ID="ddlYear" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="true" runat="server" Visible="false" class="form-control " />

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server" id="divfDate">
                                                    <div class="form-group">
                                                        <div class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            <asp:Label ID="LblMonth" runat="server" Visible="false" ForeColor="Black"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-8 padd">
                                                    
                                                            <asp:TextBox ID="TxtFromDate" runat="server" class="form-control " Visible="false"></asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="TxtFromDate" PopupPosition="BottomRight">
                                                            </ajax:CalendarExtender>
                                                            <asp:DropDownList ID="ddlYear1" runat="server" Visible="false" class="form-control " />

                                                        </div>
                                                    </div>
                                                </div>
                                              
                                                   
                                            </div>
                                            <div class="row">
                                              <div id="Div1" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                                    <div class="form-group">
                                                        <div class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            <asp:Label ID="LblToDate" Text="To :" runat="server" Visible="false" ForeColor="Black"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-8 padd">
                                                              <asp:DropDownList ID="ddlMonth" runat="server" Visible="false" class="form-control " />
                                                          
                                                            <asp:TextBox ID="TxtD" runat="server" class="form-control " Visible="false"></asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="TxtD" PopupPosition="BottomRight">
                                                            </ajax:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="DivMaster" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                                    <div class="form-group">
                                                        <div class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            <asp:Label ID="lblCountry" Text="Level :" runat="server"  ForeColor="Black"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-8 padd">
                                                            <asp:DropDownList ID="ddlCountry" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                                                AutoPostBack="true" runat="server" class="form-control ">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Country" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Region" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="District" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="Employee" Value="4"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <div class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            <asp:Label ID="LblState1" Text="State :"  runat="server" Visible="false" ForeColor="Black"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-8 padd">
                                                            <asp:DropDownList ID="ddlState1" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" class="form-control " />
                                                               <asp:DropDownList ID="ddlRegion" runat="server" Visible="false" class="form-control " />
                                                           
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <div class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            <asp:Label ID="Lbl1" Text="To :" runat="server" Visible="false" ForeColor="Black"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-8 padd">
                                                         
                                                            <asp:DropDownList ID="ddlDistrict1" runat="server" Visible="false" class="form-control " AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <div class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            <asp:Label ID="Lbl2" Text="To :" runat="server" Visible="false" ForeColor="Black"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-8 padd">
                                                            <asp:DropDownList ID="ddlBlock1" runat="server" Visible="false" class="form-control" />
                                                            <asp:DropDownList ID="ddlEmployee1" runat="server" Visible="false" class="form-control" />
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
                                                    <div class="panel-default search-bg" style="height: 41px">
                                                        <span style="float: left; color: Black; margin-left: 12px;">
                                                            <asp:Label ID="lblTotal" Visible="false" Text="Total:" runat="server"></asp:Label>
                                                        </span><span style="float: left; color: Black; margin-left: 12px;">
                                                            <asp:Label ID="lblTotalCount" ForeColor="#737272" Font-Bold="true" runat="server"></asp:Label>
                                                        </span>
                                                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0" style="float: right;">
                                                    <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-right"
                                                        BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                                                </div>
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
                                                            </div>
                                                        </div>
                                                        <div class="row" style="width: 100%">
                                                            <asp:GridView ID="GV_Report" runat="server" ForeColor="Black" AllowPaging="true"
                                                                OnPageIndexChanging="GV_Report_OnPageIndexChanging" PageSize="100" ShowHeader="true"
                                                                CssClass="table table-striped table-bordered table-hover" Width="100%">
                                                                <EmptyDataTemplate>
                                                                    No Record Found!
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
                                                        <div class="row" style="width: 200%">
                                                            <asp:GridView ID="GV_Report1" runat="server" ForeColor="Black" AllowPaging="true"
                                                                OnPageIndexChanging="GV_Report1_OnPageIndexChanging" PageSize="100" ShowHeader="true"
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
                                                            <asp:GridView ID="GV_Report3" runat="server" ForeColor="Black" AllowPaging="true"
                                                                OnPageIndexChanging="GV_Report3_OnPageIndexChanging" PageSize="100" ShowHeader="true"
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
                                                            <asp:GridView ID="GV_Report4" runat="server" ForeColor="Black" AllowPaging="true"
                                                                PageSize="100" ShowHeader="true" Visible="false" CssClass="table table-striped table-bordered table-hover"
                                                                OnPageIndexChanging="GV_Report4_pageindexchanging" Width="100%">
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
                                                            <asp:GridView ID="GV_Report5" runat="server" Visible="false" OnPageIndexChanging="GV_Report5_PageIndexChanging"
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
    </div>
    <script type="text/javascript">        $(function () {
            $('#datetimepicker4').datetimepicker();
        }); </script>
</asp:Content>
