<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="frmReportRejection.aspx.cs" Inherits="frmReportRejection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div id="div-show-new">
                <div class="row marg search-bg">
                    <div class="form-horizontal">
                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                            <div class="form-group">
                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                    State:</label>
                                <div class="col-sm-9 padd">
                                    <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                        AutoPostBack="true" class="form-control ">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblNumNo" Visible="false" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                            <div class="form-group">
                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                    District:</label>
                                <div class="col-sm-9 padd">
                                    <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                        AutoPostBack="true" class="form-control " />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                            <div class="form-group">
                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                    Block:</label>
                                <div class="col-sm-9 padd">
                                    <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                        class="form-control " />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                            <div class="form-group">
                                <label for="email" class="col-sm-4 padd linhei" style="padding-top: 2px;">
                                    Panchayat:</label>
                                <div class="col-sm-8 padd">
                                    <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"
                                        class="form-control " />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                            <div class="form-group">
                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                    Village:</label>
                                <div class="col-sm-9 padd">
                                    <asp:DropDownList ID="ddlVillage" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged"
                                        AutoPostBack="true" runat="server" class="form-control " />
                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                                            ValidationGroup="saves" ControlToValidate="ddlVillage" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-right"
                                BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                <h3 class="text-danger" style="margin: 0px;">
                                    <asp:Label ID="lblMain" runat="server" Text="Import Status Report"></asp:Label>
                                </h3>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row table-responsive">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <asp:GridView ID="GV_rejection" runat="server" AutoGenerateColumns="true" CellPadding="2"
                                    CssClass="Grid" Width="100%">
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
                                        <%--  <asp:TemplateField HeaderText="Session ID" HeaderStyle-Width="13%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbl_ImpSessionID" Text='<%#Eval("SessionID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Imported By" HeaderStyle-Width="16%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Imported_by" Text='<%#Eval("ImportedBy") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="8%" CssClass="textalign" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date of import" HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbl_CreatedOn" Text='<%#Eval("SessionDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date of Rejection " Visible="false" HeaderStyle-Width="21%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbl_UpdatedOn" Text='<%#Eval("ValidatedOn") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="40%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="Txt_Remark" BorderStyle="None" Width="99%" Text='<%#Eval("Remarks") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="25%" />
                                            </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
