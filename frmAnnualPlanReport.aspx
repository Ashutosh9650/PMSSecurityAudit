<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmAnnualPlanReport.aspx.cs" Inherits="frmAnnualPlanReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    
 
   
    <style type="text/css">
        .ajax__calendar_container
        {
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
 
.pagination-ys table > tbody > tr > td > a,
.pagination-ys table > tbody > tr > td > span {
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
 
.pagination-ys table > tbody > tr > td:first-child > a,
.pagination-ys table > tbody > tr > td:first-child > span {
    margin-left: 0;
    border-bottom-left-radius: 4px;
    border-top-left-radius: 4px;
}
 
.pagination-ys table > tbody > tr > td:last-child > a,
.pagination-ys table > tbody > tr > td:last-child > span {
    border-bottom-right-radius: 4px;
    border-top-right-radius: 4px;
}
 
.pagination-ys table > tbody > tr > td > a:hover,
.pagination-ys table > tbody > tr > td > span:hover,
.pagination-ys table > tbody > tr > td > a:focus,
.pagination-ys table > tbody > tr > td > span:focus {
    color:Black;
    background-color: #eeeeee;
    border-color: #dddddd;
}
    </style>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <%-- <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>--%>
            <div class="container-fluid" >
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
                                                        
                                            <asp:LinkButton ID="lnkCSV" runat="server" Visible="false"  Text="Export to CSV" OnClick="btnCSV_Click" ></asp:LinkButton>
                                            </div>
                                            </div>  
                                            </div>              
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                
                <div class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 text-left" style="padding-right: 0px;">
                  <div style="overflow: auto; margin-top:0px; height:480px;">
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
                                 <asp:LinkButton ID="LinkButton18" runat="server"   Text="Annual FC Report" Style="color: white;"
                                    OnClick="AnnaualFCReport_Click"></asp:LinkButton>
                            </li>
                            
                        <li class="li-width">
                            <asp:LinkButton ID="LnkMasterDate" Visible="true" runat="server" Text="Location Master"
                                Style="color: white;" OnClick="LnkMasterData_OnClick"></asp:LinkButton></li>
                          
                        


                         <li class="li-width">
                            <asp:LinkButton ID="LinkButton2" Visible="true" runat="server" Text="Annual Plan Detail"
                                Style="color: white;" OnClick="LnkAnnualPlan_OnClick"></asp:LinkButton></li>
                          
                       

                         <li class="li-width">
                            <asp:LinkButton ID="LinkButton3" Visible="true" runat="server" Text="Annual Plan Cluster Summary"
                                Style="color: white;" OnClick="LnkAnnualPlanSummary_OnClick"></asp:LinkButton></li>
                            <li class="li-width">
                            <asp:LinkButton ID="LinkButton4" Visible="true" runat="server" Text="Annual Plan Summary"
                                Style="color: white;" OnClick="LnkAnSummary_OnClick"></asp:LinkButton></li>
                        </ul>

                    </div>
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
                                                        Tpye</label>
                                                    <div class="col-sm-8 padd">
                                                         <asp:DropDownList ID="ddlTpye" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">District Wise </asp:ListItem>
                                                                <asp:ListItem Value="2">Block Wise </asp:ListItem>
                                                                <asp:ListItem Value="3">Cluster Wise</asp:ListItem>
                                                            
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
                                                  
                                                     
                                                    </div>
                                                        <div style="height: 290px; overflow: auto; width: 99%;" align="center">
                                                            
                                                            <div class="row" style="width: 99%">
                                                       
                                                                <asp:GridView ID="GV_DynamicGrid"  runat="server" ForeColor="Black" AllowPaging="true"
                                                                    PageSize="100" ShowHeader="true"  CssClass="table table-striped table-bordered table-hover"
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
            </script></asp:Content>