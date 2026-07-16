<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FrmDistrictMaster.aspx.cs" Inherits="FrmDistrictMaster" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid" >
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="thumbnail" style="background-color: #f5f5f5; margin-bottom: 3px !important;">
                    <div class="panel-heading">
                        <div class="row">
                               <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                    <h3 class="text-danger" style="margin: 0px;">
                                        District master</h3>
                                </div>
                              <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                              <asp:Button ID="BtnSave" CssClass="btn-danger pull-right" runat="server" Text="Save" OnClick="Btn_Save_OnClick" />

                              </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left: -2px;">
            <div class="thumbnail" style="background-color: #f5f5f5; float: left; width: 100%;">
                <div class="panel panel-default">
                    <div class="form-horizontal">
                        <div class="row">
                            <div class="col-lg-12 table table-hover " style="padding: 0px;">
                                <asp:Panel ID="pnlMain" runat="server">
                                    <div class="form-horizontal">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                            <div style="height: 450px; overflow: auto; width: 99%;" align="center">
                                                <div>
                                                    <div class="Row" style="width: 100%">
                                                        <asp:GridView ID="GV_District" runat="server"  CssClass="table table-striped table-bordered table-hover"
                                                            AutoGenerateColumns="False" Font-Names="Arial" Font-Size="12px" Width="100%">
                                                            <EmptyDataTemplate>
                                                                <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                    Data not found</div>
                                                            </EmptyDataTemplate>
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                            <AlternatingRowStyle BackColor="#f1f1f1" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr.no">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LblDistrictCode" runat="server" Text='<%#Bind("DistrictCode") %>'></asp:Label>
                                                                    
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="District Name" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LblDistrictName" runat="server" Text='<%#Bind("DistrictName") %>'></asp:Label>
                                                                      
                                                                    </ItemTemplate>
                                                                     <%--<ItemStyle HorizontalAlign="Center" />--%>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Start Year">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TxtYear" CssClass="form-control" Text='<%#Bind("StartYear") %>' Width="20%" runat="server"></asp:TextBox>
                                                                        <ajax:CalendarExtender ID="CalenderDOJ" runat="server" Enabled="True" Format="yyyy"
                                                                            TargetControlID="TxtYear" PopupPosition="BottomRight">
                                                                        </ajax:CalendarExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%-- Text='<%#Bind("DOJ") %>'--%>
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
</asp:Content>
