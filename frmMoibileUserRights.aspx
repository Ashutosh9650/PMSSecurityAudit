<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="frmMoibileUserRights.aspx.cs" Inherits="frmMoibileUserRights" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .CommonControlText {
            border: none;
            overflow: auto;
        }
    </style>
    <style type="text/css">
        .Grid th {
            color: White;
            background-color: #C1C1C1;
            text-align: center;
        }

        /* CSS to change the GridLines color */
        .Grid, .Grid th, .Grid td {
            border: 1px solid #F1F1F1 !important;
            padding: 5px 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="panel panel-default" style="height: 500px;">
                    <div class="panel-heading" style="padding: 5px 5px;">
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                <h3 class="text-danger" style="margin: 0px;">
                                    <asp:Label ID="lblMain" runat="server" Text="Mobile Access Management"></asp:Label>
                                </h3>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12">
                                <asp:ImageButton ID="BtnSave" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                    ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="BtnSave_Click" ValidationGroup="saves"
                                    Style="margin-right: 5px; padding: 0px;" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-top: 15px;">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12" style="padding: 0px;">
                                <div class="panel panel-default" style="padding-bottom: 19px;">
                                    <div class="panel-heading">
                                        <h4 class="text-danger" style="margin: 4px 0px;">User role</h4>
                                    </div>
                                    <div class="panel-body">
                                        <asp:ListBox ID="userlist" runat="server" Width="100%" Height="340px" CssClass="CommonControlText"
                                            AutoPostBack="True" OnSelectedIndexChanged="userlist_SelectedIndexChanged"></asp:ListBox>


                                        <asp:ImageButton ID="btn_Add" Visible="false" OnClientClick="return CheckAdd();" class="btn btn-info pull-right"
                                            runat="server" BackColor="#f5f5f5" ToolTip="Add" ImageUrl="~/images/add-29-1.png"
                                            OnClick="btn_Add_click" Style="margin-right: 5px; padding: 0px;" />

                                        <asp:ImageButton ID="btnDelete" Visible="false" OnClick="btnDelete_Click" class="btn btn-info pull-right"
                                            runat="server" BackColor="#f5f5f5" ToolTip="Delete" ImageUrl="~/images/delete-29.png"
                                            Style="margin-right: 5px; padding: 0px;" />

                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-9 col-md-9 col-sm-9 col-xs-12" style="padding-top: 15px; padding: 0px;">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width: 100%; padding-right: 0px;">
                                    <div style="height: 430px; overflow: auto; width: 100%;" align="center">
                                        <div>
                                            <div class="Row" style="width: 100%">
                                                <asp:GridView ID="GV_UserPermission" runat="server" OnRowCreated="GVUserPermission_RowCreated" CssClass="Grid" AutoGenerateColumns="False"
                                                    Width="100%" GridLines="None" CellPadding="4">
                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                    <RowStyle HorizontalAlign="Left" />
                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                    <EmptyDataTemplate>
                                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                                            Data not found
                                                        </div>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Modules" DataField="menu" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left" />
                                                        <asp:TemplateField HeaderText="View">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="view_check" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Add">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Add_check" runat="server" Visible="false" />
                                                            </ItemTemplate>
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="edit_check" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="delete_check" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_perid" runat="server" Text='<%# Bind("menu_id") %>'>
                                                                </asp:Label>
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
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Hdn_model4"
        CancelControlID="ImageButton9" PopupControlID="pnlpopup4" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="Hdn_model4" runat="server" />
    <asp:Panel ID="pnlpopup4" runat="server" Style="display: none;">
        <div class="modal-dialog modal-md" style="width: 481px;">
            <div class="modal-content">
                <div class="modal-header" style="height: 45px; margin-top: -9px; background-color: #C1C1C1; color: white;">
                    <asp:ImageButton ID="ImageButton9" CssClass="btn btn-info pull-right" BackColor="#C1C1C1"
                        ToolTip="Add" ImageUrl="~/images/close-29.png" Style="margin-right: 5px; padding: 0px; margin-top: -5px;"
                        runat="server" />
                    <asp:ImageButton ID="ImgSave" CssClass="btn btn-info pull-right" BackColor="#C1C1C1"
                        ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="ImgSave_Click" ValidationGroup="saves"
                        Style="margin-right: 5px; padding: 0px; margin-top: -5px;" runat="server" />
                    <h4 class="modal-title" style="margin-top: -7px;">Add Role</h4>
                </div>
                <div class="row">
                    <div class="row marg search-bg" style="margin-left: 8px; margin-right: 8px; height: 105px;">
                        <div class="col-lg-12 col-md-12 col-sm-12 cpl-xs-12">
                            <div class="row" id="DivRole" runat="server" style="margin-top: 9px; height: 1%;">
                                <div class="col-md-6">
                                    <asp:Label ID="LblRole" runat="server" Text="Role Name:"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="TxtRole" MaxLength="50" runat="server" class="form-control" />
                                </div>
                            </div>
                            <div class="row" id="DivRegion" runat="server" style="margin-top: 9px; height: 1%;">
                                <div class="col-md-6">
                                    <asp:Label ID="lblLevel" runat="server" Text="Level :"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlRoleLevel" runat="server" Width="100%" class="form-control">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Region</asp:ListItem>
                                        <asp:ListItem Value="2">State Level</asp:ListItem>
                                        <asp:ListItem Value="3">District Level</asp:ListItem>
                                        <asp:ListItem Value="4">Block Level</asp:ListItem>
                                        <asp:ListItem Value="5">Cluster Level</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
