<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FrmEnrollmentClusterWise.aspx.cs" Culture="en-GB" Inherits="FrmEnrollmentClusterWise" %>

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
                        Enrollment <span class="pull-right" style="font-size: 17px;">
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
                                        Block:</label>
                                    <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlBlock" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_OnSelectedIndexChanged" runat="server" class="form-control ">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                           
                            <div class="col-lg-3 col-md-3  col-sm-3 cpl-xs-12">
                                <asp:Button ID="btnBack" CssClass="btn btn-success pull-right "  ToolTip="Save" Text="Back"
                                    Visible="false" OnClick="btnBack_Click" Style="margin-right: 5px; padding: 0px;"
                                    runat="server" />
                                <asp:Button ID="btnReport" Visible="false" OnClick="btnReport_Click" CssClass="btn btn-success pull-right"
                                    ToolTip="Save" Text="Report" Style="margin-right: 5px; padding: 0px;" runat="server" />
                                <asp:ImageButton ID="btnSerach" ToolTip="Serach"   Visible="false" runat="server" OnClick="btnSerach_Click"
                                    class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png"
                                    Style="margin-left: 5px; padding: 0px;" />
                              
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
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="panel panel-default" id="pnlMain" runat="server">
                        <div class="panel-heading pln-head1" >
                            <h4 class="panel-title">
                                <a 
                                   >Seal-sign validation </a>
                            </h4>
                        </div>
                        <div id="ColMain">
                            <div class="panel-body panel-body-new">
                                
                         
                                    <%--/<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">--%>
                                     <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                         <div style="height: 150px; overflow: auto; width: 99%;" align="center">
                                        <asp:GridView ID="Gv_Profile_Search" runat="server" CssClass="table table-striped table-bordered table-hover"
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
                                                <asp:TemplateField HeaderText="Activity" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbtn"  runat="server" Text='<%# Bind("School") %>'   ></asp:Label>
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
                                        </asp:TemplateField>   <asp:TemplateField Visible="false">
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
                                            
                                            </Columns>
                                        </asp:GridView>
                                       </div>
                                    </div>
                               
                              
                            </div>
                        </div>
                        </div>
                        
                          <div class="panel panel-default" id="pnlMain1" runat="server">
                        <div class="panel-heading pln-head1" >
                            <h4 class="panel-title">
                                <a 
                                   >Seal-sign generation</a>
                            </h4>
                        </div>
                        <div id="Div1">
                            <div class="panel-body panel-body-new">
                                
                         
                                    <%--/<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">--%>
                                     <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                         <div style="height: 150px; overflow: auto; width: 99%;" align="center">
                                        <asp:GridView ID="gvGenerAtion" runat="server" CssClass="table table-striped table-bordered table-hover"
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
                                                <asp:TemplateField HeaderText="Activity" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbtn"  runat="server" Text='<%# Bind("School") %>'   ></asp:Label>
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
                                        </asp:TemplateField>   <asp:TemplateField Visible="false">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
