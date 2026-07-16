<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FrmPhaseMapping.aspx.cs" Inherits="FrmPhaseMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="thumbnail" style="background-color: #f5f5f5; margin-bottom: 3px !important;">
                            <div class="panel-heading" style="padding: 0px 0px;">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <h3 class="text-danger" style="margin: 0px;">Phase Mapping</h3>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-top: 5px;">
                    <div class="thumbnail" style="background-color: #f5f5f5; float: left; width: 100%;">
                        <div class="panel panel-default">
                            <div class="form-horizontal">
                                <div class="row">
                                    <div style="padding: 0px 10px;">
                                        <div class="row marg search-bg" style="padding: 10px 0px 0px 10px;">
                                            <div class="form-horizontal">
                                                <div class="row">
                                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                Year:
                                                            </label>
                                                            <div class="col-sm-8 padd">
                                                                <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                                    class="form-control ">
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
                                                                <asp:DropDownList ID="ddlDistrict" runat="server" class="form-control " />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-10">
                                                        <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" OnClick="btnSerach_Click"
                                                            class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />

                                                        <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/images/save-29-1.png" Text="Save"
                                                            class="btn  btn-paddd" ToolTip="Save" OnClick="btnSave_Click" Style="float: none;"
                                                            ValidationGroup="saves"></asp:ImageButton>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Export to Excel" OnClick="btnExportExcel_Click"
                                                            class="pull-right"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 table table-hover" style="padding: 0px;">
                                            <asp:Panel ID="pnlMain" runat="server">
                                                <asp:UpdatePanel runat="server" ID="UpdatedddddddPanel1">
                                                    <ContentTemplate>
                                                        <div class="form-horizontal">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                                <div style="height: 339px; overflow: auto; width: 100%;" align="center">
                                                                    <div>
                                                                        <div class="Row" style="width: 100%">
                                                                            <asp:GridView ID="gvnroll" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                                OnRowDataBound="gvnroll_OnRowDataBound" AutoGenerateColumns="False" Font-Names="Arial"
                                                                                Font-Size="12px" Width="100%">
                                                                                <EmptyDataTemplate>
                                                                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                        Data not found
                                                                                    </div>
                                                                                </EmptyDataTemplate>
                                                                                <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                <Columns>
                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblPM_GUID" ForeColor="Black" runat="server" Text='<%# Eval("PM_GUID") %>'>
                                                                                            </asp:Label>
                                                                                            <asp:Label ID="lblStateCode" ForeColor="Black" runat="server" Text='<%# Eval("StateCode") %>'>
                                                                                            </asp:Label>
                                                                                            <asp:Label ID="lblDistrictCode" ForeColor="Black" Visible="false" runat="server"
                                                                                                Text='<%# Eval("DistrictCode") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="State Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblStateName" ForeColor="Black" runat="server" Text='<%# Eval("StateName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" Width="25%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="District Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="LblDistrict" ForeColor="Black" runat="server" Text='<%# Eval("DistrictName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" Width="25%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Region">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="TxtRegion" MaxLength="4" CssClass="form-control" ForeColor="Black"
                                                                                                Width="96%" runat="server" Text='<%# Eval("Region") %>'></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" Width="10%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Phase">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlPhase" CssClass="form-control" runat="server" Width="100%">
                                                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                                                <asp:ListItem Value="3">3 </asp:ListItem>
                                                                                                <asp:ListItem Value="4">4 </asp:ListItem>

                                                                                            </asp:DropDownList>
                                                                                            <asp:Label runat="server" Visible="false" ID="lblPhase" Text='<%#Eval("Phase") %>'
                                                                                                Style="text-decoration: none;"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" Width="13%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Program Year">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="TxtProgramYear" MaxLength="2" CssClass="form-control" ForeColor="Black"
                                                                                                runat="server" Width="78%" Text='<%# Eval("Program_Year") %>'></asp:TextBox>
                                                                                            <asp:FilteredTextBoxExtender ID="FilterTxtProgramYear" TargetControlID="TxtProgramYear"
                                                                                                ValidChars="0123456789" runat="server" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" Width="11%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Operational Year">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="TxtOperationalYear" MaxLength="2" CssClass="form-control" ForeColor="Black"
                                                                                                runat="server" Width="77%" Text='<%# Eval("Operational_Year") %>'></asp:TextBox>
                                                                                            <asp:FilteredTextBoxExtender ID="FilterTxtOperationalYear" TargetControlID="TxtOperationalYear"
                                                                                                ValidChars="0123456789" runat="server" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" Width="13%" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="gvnroll" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="LinkButton1" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
