<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Frm_CLT_Report.aspx.cs"  Culture="en-GB" Inherits="Frm_CLT_Report" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<style>
    
   .reportbtn1
        {
            background-color: #a94442;
        }
        .reportbtn2
        {
            background-color: #a94442;
        }
        .reportbtn3
        {
            background-color: #a94442;
        }
        .reportbtn
        {
            width: 100%;
            cursor: pointer; /*background-image:url('./AppImages/btn_null1.png');   background-position: -6px -4px; background-repeat: no-repeat;*/
            color: #fff;
            border-style: none;
            height: 30px;
            border-radius: 7px;
            text-align: left;
        }
        
         .gridnewheadercss
        {
            color: #ffffff;
            vertical-align: middle;
            background-color: #81AB81;
        }
        
</style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<%--<asp:UpdatePanel ID="updmain" runat="server">
<ContentTemplate>--%>

<div class="container-fluid" style="margin-top: 105px !important;">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="thumbnail" style="background-color: #f5f5f5; margin-bottom: 3px !important;">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <h3 class="text-danger" style="margin: 0px;">
                                           Learning Level Report <span class="pull-right" style="font-size: 17px;">
                                                <asp:LinkButton ID="btnexcel"  runat="server" Text="Export to Excel" OnClick="Export_To_Excel"></asp:LinkButton>
                                                
                                             </span>
                                        </h3>
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
                       
                        </div>
                        <ul class="nav navbar-nav" style="margin: 0px">
                            <li class=" active li-width">
                                <asp:LinkButton ID="Button3" runat="server" Text="Learning Outcome Analysis " Style="color: white;"
                                    OnClick="PMS_Click"></asp:LinkButton>
                            </li>
                              <li class=" active li-width">
                                <asp:LinkButton ID="LinkButton1" runat="server" Text="Learning Baseline Analysis " Style="color: white;"
                                    OnClick="PMSBaseline_Click"></asp:LinkButton>
                            </li>
                            <li class="li-width">
                                <asp:LinkButton ID="ff" runat="server" Visible="false" Text="Learning Baseline" Style="color: white;"
                                    OnClick="Report2_Click"></asp:LinkButton></li>
                            <li class="li-width">
                                <asp:LinkButton ID="LinkButton2" runat="server" Text="Learning Baseline Grouping" Style="color: white;"
                                    OnClick="Report3_Click"></asp:LinkButton></li>
                                       <li class="li-width">
                                <asp:LinkButton ID="LinkButton3" runat="server" Text="Retention Report" Style="color: white;"
                                    OnClick="Retention_Click"></asp:LinkButton></li>
              
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
                                                                <asp:DropDownList ID="ddlstate" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
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
                                                                <asp:DropDownList ID="ddlBlock" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" runat="server" 
                                                                    class="form-control " />
                                                            </div>
                                                        </div>
                                                    </div>
                                                   
                                                </div>
                                                
                                                <div class="row">

                                                 <div id="Div1" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                Subject:</label>
                                                            <div class="col-sm-8 padd">
                                                                <asp:DropDownList ID="ddlsubject" runat="server"
                                                                    class="form-control ">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                     <asp:ListItem Text="Hindi" Value="1"></asp:ListItem>
                                                                      <asp:ListItem Text="English" Value="2"></asp:ListItem>
                                                                       <asp:ListItem Text="Math" Value="3"></asp:ListItem>
                                                                    
                                                                    </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="Div2" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                School:</label>
                                                            <div class="col-sm-8 padd">
                                                                <asp:DropDownList ID="ddlSchool" 
                                                                    runat="server" class="form-control " />
                                                                
                                                            </div>
                                                        </div>
                                                    </div>
                                                      <div id="Div3" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                Type:</label>
                                                            <div class="col-sm-8 padd">

                                                                <asp:DropDownList ID="ddlType" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                    <asp:ListItem Value="1">District Level </asp:ListItem>
                                                                        <asp:ListItem Value="2">Block Level </asp:ListItem>
                                                                <asp:ListItem Value="3">School Level </asp:ListItem>
                                                                
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
                                                                <div class="Row" style="width: 100%" runat="server">


                                                                 <rsweb:ReportViewer ID="rptD2D" runat="server" Style="width: 100%; "
                                                                    AsyncRendering="False"   SizeToReportContent="True" PageCountMode="Actual" width="100%" height="100%" >
                                                                </rsweb:ReportViewer>

                                                                  <asp:GridView ID="DGV_Report" AllowPaging="true" PageSize="30" runat="server" ForeColor="#333333" CellPadding="1" OnRowCreated="DGV_Reports_RowCreated"
                                CssClass="table table-striped table table-hover table-bordered  " AutoGenerateColumns="true" Width="99.7%"      >
                               
                                <EmptyDataTemplate>
                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                        Data not found</div>
                                </EmptyDataTemplate>
                                <Columns>
                                </Columns>
                            </asp:GridView>

                            
                                                                  <asp:GridView ID="GridView1" AllowPaging="true" PageSize="30" runat="server" ForeColor="#333333" CellPadding="1" 
                                CssClass="table table-striped table table-hover table-bordered  " AutoGenerateColumns="true" Width="99.7%"      >
                               
                                <EmptyDataTemplate>
                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                        Data not found</div>
                                </EmptyDataTemplate>
                                <Columns>
                                </Columns>
                            </asp:GridView>
                                         <asp:GridView ID="gvRetaion" AllowPaging="true" PageSize="30" runat="server" ForeColor="#333333" CellPadding="1" OnRowCreated="gvRetaion_RowCreated"
                                CssClass="table table-striped table table-hover table-bordered  " AutoGenerateColumns="true" Width="99.7%"      >
                               
                                <EmptyDataTemplate>
                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                        Data not found</div>
                                </EmptyDataTemplate>
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
                        <!-- /#wrapper -->
                        <!-- /#wrapper -->
                    </div>
                </div>
            </div>






       <%--    </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="btnexcel" />
           
            </Triggers>
            </asp:UpdatePanel>--%>
</asp:Content>

