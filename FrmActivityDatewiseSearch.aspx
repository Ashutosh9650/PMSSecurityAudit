
    <%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" Culture="en-GB" AutoEventWireup="true" CodeFile="FrmActivityDatewiseSearch.aspx.cs" Inherits="FrmActivityDatewiseSearch" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
        .modalpopupcss
        {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        .modalPopup
        {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
            z-index: 999999;
        }
        
        .panel-heading .accordion-toggle:after {
    /* symbol for "opening" panels */
    font-family: 'Glyphicons Halflings';  /* essential for enabling glyphicon */
    content: "\e114";    /* adjust as needed, taken from bootstrap.css */
    float: right;        /* adjust as needed */
    color: grey;         /* adjust as needed */
}
.panel-heading .accordion-toggle.collapsed:after {
    /* symbol for "collapsed" panels */
    content: "\e080";    /* adjust as needed, taken from bootstrap.css */
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
    <div class="row" >
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="text-danger" style="margin: 0px;">
                        School and Village Activity <span class="pull-right" style="font-size: 17px;">
                            <asp:LinkButton ID="btnexcel" Visible="false" runat="server" Text="Export to Excel"
                                OnClick="Export_To_Excel"></asp:LinkButton>
                        </span>
                    </h3>
                </div>
            </div>
            <div class="row">
                <div class="row marg search-bg">
                    <div class="form-horizontal">
                        <%-- <asp:UpdatePanel runat="server" ID="UpMain" UpdateMode="Conditional">
        <ContentTemplate>--%>
                        <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                               <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                            <div class="form-group" style="margin-bottom: 7px;">
                                <label for="email" class="col-sm-3 padd linhei">
                                    Cluseter:</label>
                                <div class="col-sm-9 padd">
                                    <asp:DropDownList ID="ddlCulster"  AutoPostBack="true"  OnSelectedIndexChanged="ddlCulster_SelectedIndexChanged"
                                        runat="server" class="form-control ">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                            
                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                <div class="form-group" style="margin-bottom: 7px;">
                                    <label for="email" class="col-sm-3 padd linhei">
                                        From Date:</label>
                                    <div class="col-sm-9 padd">
                                        <asp:TextBox runat="server" Enabled="false" ID="TxtFromDate" autocomplete="off" ondrop="return false;"
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
                                        <asp:TextBox runat="server" ID="txtDate" Enabled="false" autocomplete="off" ondrop="return false;"
                                            class="form-control" onkeypress="return false;"></asp:TextBox>
                                        <ajax:CalendarExtender ID="CalendarExtenderTourdate" runat="server" Enabled="True"
                                            Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight">
                                        </ajax:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="ReqTxtDate" runat="server" ControlToValidate="txtDate"
                                            Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                            SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                             <div class="col-lg-3 col-md-3  col-sm-3 cpl-xs-12">
                            
                                <asp:Button ID="btnAddVillage"   CssClass="btn btn-success pull-right " 
                                 ToolTip="Save" Text="Add Village"   OnClick="btnAddVillage_Click" 
                                Style="margin-right: 5px; padding: 0px;" runat="server" /> 
                                <asp:Button ID="btnApprove" CssClass="btn btn-success pull-right " ToolTip="Save"
                                    Text="  Back" OnClick="btnApprove_Click" Style="margin-right: 5px; padding: 0px;"
                                    runat="server" />
                           
                                <asp:Button ID="btnsave" CssClass="btn btn-success pull-right " OnClick="btnSave_Click"
                                    ToolTip="Save" Text="Report" Visible="false" Style="margin-right:5px; padding: 0px;" runat="server" />

                                <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" OnClick="btnSerach_Click"
                                    class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png"
                                    Visible="false" Style="margin-left:5px; padding: 0px;" />

                                    <asp:LinkButton ID="LinkButton1"  Style="margin-right: 5px; padding: 0px;" CssClass="pull-left "  OnClick="lnkView_Click"  runat="server">Mark No Work Reason</asp:LinkButton>
                          
                          
                          
                        </div>
                        </div>
                       
                    </div>
                </div>

                       <div class="row">
                    <div class="col-sm-12" style="padding:0px">
                        <div class="panel-group" id="accordion">
  <div class="panel panel-default">
    <div class="panel-heading">
      <h4 class="panel-title">
        <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
         <span style="color:blue"> School Activity </span>
        </a>
      </h4>
    </div>
    <div id="collapseOne" class="panel-collapse collapse">
      <div class="panel-body">
      <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                         <div style="height: 320px; overflow: auto; width: 99%;" align="center">
                                        <asp:GridView ID="Gv_Profile_Search" runat="server" CssClass="table table-striped table-bordered table-hover"
                            AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                            Font-Size="11px" Width="100%">
                            <EmptyDataTemplate>
                                <div style="font-family: Arial; font-size: 11px; font-weight: bold;">
                                    Data not found</div>
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
                                            <asp:LinkButton ID="lbtn" OnClick="LnkSchool_OnClick"    runat="server" Text='<%# Bind("School") %>'  CommandArgument='<%# Bind("School") %>'  ></asp:LinkButton>
                                                                        <asp:Label ID="lblUn1" Visible="false" ForeColor="Black"  Text='<%# Bind("School") %>' Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                        runat="server" ></asp:Label>
                                                                           
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
                                <asp:TemplateField HeaderText="Total" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="TxtTotla" ForeColor="Black" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                     
                        <asp:GridView ID="DGV_Report" AllowPaging="true" PageSize="30" runat="server" ForeColor="#333333"
                            CellPadding="1" CssClass="table table-striped table table-hover table-bordered"
                            AutoGenerateColumns="true" Width="99.7%">
                            <EmptyDataTemplate>
                                <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                    Data not found</div>
                            </EmptyDataTemplate>
                            <Columns>
                            </Columns>
                        </asp:GridView>
                                       </div>
                                    </div>      </div>
    </div>
  </div>
  <div class="panel panel-default">
    <div class="panel-heading">
      <h4 class="panel-title">
        <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">
        <span style="color:blue"> Village Activity</span>
        </a>
      </h4>
    </div>
    <div id="collapseTwo" class="panel-collapse collapse">
      <div class="panel-body">
       <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                               <div style="height: 390px; overflow: auto; width: 99%;" align="center">
                               <asp:GridView ID="gvVillageActivity" runat="server" CssClass="table table-striped table-bordered table-hover"
                            AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                            Font-Size="11px" Width="100%">
                            <EmptyDataTemplate>
                                <div style="font-family: Arial; font-size: 11px; font-weight: bold;">
                                    Data not found</div>
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
                                           <asp:LinkButton ID="lbtnVillage" OnClick="LnkVillage_OnClick"    runat="server" Text='<%# Bind("Village") %>'    CommandArgument='<%# Bind("Village") %>'  ></asp:LinkButton>
                                                                      
                                          <asp:Label ID="lblvllV_2" Text='<%# Bind("Village") %>' Visible="false" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                   runat="server" ></asp:Label>
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
                                <asp:TemplateField HeaderText="Total" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="TxtTotlaV" ForeColor="Black" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                                </div>
                            </div>      </div>
    </div>
  </div>
  <div class="panel panel-default">
    <div class="panel-heading">
      <h4 class="panel-title">
        <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseThree">
        <span style="color:blue"> Office Activity </span>
        </a>
      </h4>
    </div>
    <div id="collapseThree" class="panel-collapse collapse">
      <div class="panel-body">
       <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                               <div style="height: 240px; overflow: auto; width: 99%;" align="center">
                                        <asp:GridView ID="gvOffice" runat="server" CssClass="table table-striped table-bordered table-hover"
                            AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                            Font-Size="11px" Width="99%">
                            <EmptyDataTemplate>
                                <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                    Data not found</div>
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
                                                                 <asp:LinkButton ID="lbtnOffice" OnClick="LnkOffice_OnClick"    runat="server" Text='<%# Bind("Village") %>'  CommandArgument='<%# Bind("Village") %>'  ></asp:LinkButton>
                                                                               <asp:Label ID="lbooff"  Text='<%# Bind("Village") %>' Visible="false"  ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                        runat="server" ></asp:Label>
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
                                <asp:TemplateField HeaderText="Total" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="TxtTotlaO" ForeColor="Black" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                                </div>
                            </div>      </div>
    </div>
  </div>
</div>
                    </div>
                </div>


              
    </div>
         <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Hdn_model3"
            PopupControlID="pnlpopup3" CancelControlID="btnAdd"  BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:HiddenField ID="Hdn_model3" runat="server" />
        <asp:Panel ID="pnlpopup3" runat="server" Style="display: none;">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                    <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-right" 
                            ToolTip="Add" ImageUrl="~/images/close-29.png"  Style="margin-right: 5px;
                            padding: 0px;" runat="server" />
                        <h4 class="modal-title">
                            Activity</h4>
                        
                    </div>
                  <div class="row">

                        <div class="row marg search-bg">
               
                                <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                                    <div class="col-lg-6 col-md-6 col-sm-6 cpl-xs-12">
                                    <div class="form-group" style="margin-bottom: 2px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            </label>
                                        <div class="col-sm-9 padd">
                                          
                                      
                                        </div>
                                    </div>
                                </div>

                               
                                </div>

                                 <div class="col-lg-2 col-md-2  col-sm-2 cpl-xs-12">
                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                            <asp:ImageButton ID="ImageButton8" ToolTip="Save" runat="server" OnClick="btnSaveData_Click"  class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1"
                                ImageUrl="~/images/save-29-1.png"  Style="margin-left: -49px; padding: 0px;"   />
                                </div>
                
                           
                          
                        </div>
                        </div>
                  </div>
                    <div class="row table-responsive">
                     <div style="overflow: auto; margin-top:35px; height:480px;">
                                  <asp:GridView ID="Gv_Display" Width="100%" runat="server" OnRowDataBound="Gv_Display_RowDataBound"
                            CssClass=" table table-striped table-bordered table-hover " AutoGenerateColumns="false">
                            <EmptyDataTemplate>
                                <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                    Data not found
                                </div>
                            </EmptyDataTemplate>
                            <FooterStyle CssClass="FooterStyle" />
                            <HeaderStyle BackColor="#C1C1C1" Height="44px" />
                            <RowStyle HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#897A7A" Font-Bold="True" ForeColor="WhiteSmoke" />
                            <AlternatingRowStyle BackColor="#f1f1f1" />
                            <Columns>
                                <asp:TemplateField HeaderText="Activity" HeaderStyle-CssClass="GridHeaderClass">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUniqueCode" runat="server" Text='<%#Eval("ActivityDate") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                
                               
                                <asp:TemplateField HeaderText="Reason" HeaderStyle-CssClass="GridHeaderClass">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                               <asp:ListItem Value="1">Holiday</asp:ListItem>
                                              <asp:ListItem Value="2">CL</asp:ListItem>
                                            <asp:ListItem Value="3">PL </asp:ListItem>
                                             <asp:ListItem Value="4">SL </asp:ListItem>
                                            <asp:ListItem Value="5">C-Off </asp:ListItem>
                                             <asp:ListItem Value="6">Support </asp:ListItem>
                                              <asp:ListItem Value="7">No FC </asp:ListItem>
                                                  <asp:ListItem Value="8">LWP</asp:ListItem>
                                                    <asp:ListItem Value="9">Maternity Leave</asp:ListItem>
                                                      <asp:ListItem Value="10">Paternity Leave</asp:ListItem>
                                              </asp:DropDownList>
                                        <asp:Label runat="server" Visible="false" ID="lbStatus" Text='<%#Eval("Status") %>'
                                            Style="text-decoration: none;"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
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


        
             <cc1:ModalPopupExtender ID="MpexdrDistrict" runat="server" BackgroundCssClass="modalBg "
                  PopupControlID="PnlDistrict" TargetControlID="HdnFild">
                </cc1:ModalPopupExtender>
                <asp:HiddenField ID="HdnFild" runat="server"></asp:HiddenField>

<asp:Panel cssclass="model-wid mod-posi"  Style="display: none;height:auto;width: 45% !important; margin-top: 30.5px !important" ID="PnlDistrict" runat="server">
                   
                    <div style="width:100%;height:auto;background-color:#f1f1f1">
                    <div class="modal-header"  style="background-color:#3ac0f2;color:White;">
                           <asp:Button ID="ImageButton1" CssClass="btn btn-success pull-right"  
                            ToolTip="Add" Text="View"  OnClick="btnView_Click"  Style="margin-right: 5px; olpadding: 0px;" runat="server" />
                    <h4 class="modal-title" style="ForeColor:White">Add Village</h4>
                    </div>
                   <div class="modal-body">
                   <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                   <div class="form-horizontal" role="form">
  <asp:Panel ID="pnlView" runat="server" >
  <div class="form-group" id="statediv" runat="server">
 
     <asp:Label ID="Label10" class="control-label col-sm-4 lab-text-left" runat="server" Text="State:"></asp:Label>
    <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                            AutoPostBack="true"  CssClass="form-control"
                                            Font-Names="Verdana" Font-Size="11px" 
                                            >
                                        </asp:DropDownList>
                                           <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="0" runat="server"
                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlState" ErrorMessage="*"
                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </span> 
                                     
    </div>
  </div>
 

  

  
  <div class="form-group">
 
     <asp:Label ID="Label11" class="control-label col-sm-4 lab-text-left" runat="server" Text="District:"></asp:Label>
    <div class="col-sm-6">
                                          <asp:DropDownList ID="ddlDistrict" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                            AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                            Font-Size="11px"  >
                                        </asp:DropDownList>
                                         <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0" runat="server"
                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlDistrict" ErrorMessage="*"
                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </span> 
    </div>
  </div>

  <div class="form-group">
 
     <asp:Label ID="Label12" class="control-label col-sm-4 lab-text-left" runat="server" Text="Block"></asp:Label>
    <div class="col-sm-6 ">
                                        <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" CssClass="form-control"
                                            Font-Names="Verdana" Font-Size="11px">
                                        </asp:DropDownList>
                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" runat="server"
                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlBlock" ErrorMessage="*"
                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </span> 
    </div>
  </div>

  <div class="form-group">
 
     <asp:Label ID="Label13" class="control-label col-sm-4 lab-text-left" runat="server" Text="Panchayat:"></asp:Label>
    <div class="col-sm-6">
                                         <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged" CssClass="form-control"
                                            Font-Names="Verdana" Font-Size="11px">
                                        </asp:DropDownList>
                                           <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server"
                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlPanchayat" ErrorMessage="*"
                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </span>  
                                        
    </div>
  </div>

  <div class="form-group" >
 
    <asp:Label ID="lblRseti" class="control-label col-sm-4 lab-text-left" runat="server" Text="Village"></asp:Label>
    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlAddVillage" runat="server" CssClass="form-control"
                                            Font-Names="Verdana" Font-Size="11px">
                                        </asp:DropDownList>
                                      <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                            <asp:RequiredFieldValidator ID="RequiredFie99or3" InitialValue="0" runat="server"
                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlAddVillage" ErrorMessage="*"
                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </span>  
                                        
    </div>
  </div>
  </asp:Panel>
   <asp:Panel ID="pnlGridView" Visible="false" runat="server" >

     <asp:GridView ID="gvVillage" runat="server" AllowPaging="true" PageSize="100"   AutoGenerateColumns="False" Font-Names="Arial"
                                                                        Font-Size="12px" Width="100%">
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
                                                                          <asp:TemplateField HeaderText="Action"  HeaderStyle-Width="15%" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImgAcc" runat="server"  OnClick="btn_Delete_Click" ImageUrl="~/images/delete-29.png"
                                                                Width="15px" Height="15px"></asp:ImageButton>
                                                               <asp:Label ID="lblCUniqueChildCode" Visible="false" BackColor="Transparent" runat="server" Text='<%# Bind("NewVillageCode") %>' CssClass="form-controlAbhi"></asp:Label>
                                                                 <asp:Label ID="lblUserId" Visible="false" BackColor="Transparent" runat="server" Text='<%# Bind("UserId") %>' CssClass="form-controlAbhi"></asp:Label>
                                                        </ItemTemplate>
                                                       <HeaderStyle Width="5%" />
                                                        <ItemStyle  HorizontalAlign="Center"/>
                                                    </asp:TemplateField>
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

                                                                            
                                                                               <asp:TemplateField HeaderText="Village Code" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVis9ame" class="labelGrid" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("VillageCode") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             
                                                                            <asp:TemplateField HeaderText="Village Name" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVillageN9ame" class="labelGrid" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("VillageName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             
                                                                            
                                                                             
                                                                            
                                                                            
                                                                             
                                                                        </Columns>
                                                                    </asp:GridView>
   </asp:Panel>
</div>

                  
                   </div>
                    <div class="modal-footer">
                     <asp:Button ID="btnNewUserSave"  runat="server" CssClass="btn bgm-cyan" OnClick="btnNewUserSave_Click"  ValidationGroup="saves"
                                Text="Save" ToolTip="Save"  Style="float: none;" >
                            </asp:Button>&nbsp;
                           <%-- <asp:Button ID="CancelButton" runat="server"   CssClass="btn bgm-cyan" Text="Close"
                                ToolTip="Close" Style="float: none;"></asp:Button>--%>
                                     <asp:Button ID="Button1" runat="server"   OnClick="btnRest_Click" CssClass="btn bgm-cyan" Text="Close"
                                ToolTip="Close" Style="float: none;"></asp:Button></div>
                                </div>
                    
                       
                       
                </asp:Panel>

              <cc1:ModalPopupExtender ID="ModalPopupExtender43" runat="server" TargetControlID="Hdn_model43"
                                    PopupControlID="pnlpopup43" CancelControlID="btnAdd" BackgroundCssClass="modalBackground">
                                </cc1:ModalPopupExtender>
                                <asp:HiddenField ID="Hdn_model43" runat="server" />
                                <asp:Panel ID="pnlpopup43" runat="server" >
                                    <div class=" modal-lg">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                               
                                                 <asp:ImageButton ID="ImageButton2" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Add" ImageUrl="~/images/close-29.png" OnClick="hhd_click" Style="margin-right: 5px; padding: 0px;"
                                                    runat="server" />
                                                <h4 class="modal-title">
                                                    </h4>
                                            </div>
                                           
                                            <div class="row table-responsive">
                                              <div style="height: 400px; overflow: auto; width: 99%;" align="center">
                                     <asp:GridView ID="gvVillageWise" runat="server" CssClass="table table-striped table-bordered table-hover"
                                            AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                                            Font-Size="11px" Width="100%">
                                            <EmptyDataTemplate>
                                                <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                    Data not found</div>
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
                                            Data not found</div>
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
                                            Data not found</div>
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
           <Triggers>
            <asp:PostBackTrigger ControlID="btnAddVillage" />
           
            </Triggers>
        
    </asp:UpdatePanel>
</asp:Content>

