<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEnrllomentValidation.aspx.cs" Culture="en-GB" MasterPageFile="~/Site.master" Inherits="frmEnrllomentValidation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .HeaderClassCsss {
            text-align: center !important;
            font-weight: normal !important;
            background-color: #9A9C9A !important;
        }
    </style>
        <script type="text/javascript">


        function isNumberKey(txt, evt) {
        
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                if (txt.value.indexOf('.') === 1) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                else {
                    return true;
                }
            }
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="panel panel-default" style="height: 520px;">
                            <div class="panel-heading" style="padding: 0px 6px 2px 0px;">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                        <h3 class="text-danger" style="margin: 0px;">
                                            <asp:Label ID="lblMain" runat="server" Text="Master for Quality Enrolment"></asp:Label>
                                        </h3>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 2px 0px">


                                        <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                            ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnsave_Click" ValidationGroup="saves"
                                            Style="margin-right: 5px; padding: 0px;" runat="server" />

                                    </div>
                                </div>
                            </div>
                            <div class="row" style="display:none">
                                <div id="div-show-new" style="padding: 15px 15px 0px 15px;width: 100%;right: 0px;">
                                    <div class="row  search-bg" style="margin-bottom: 0px;">
                                        <div class="form-horizontal">
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                <div class="form-group" style="margin-bottom: 7px;">
                                                    <label for="email" class="col-sm-3 padd linhei">
                                                        Year:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" class="form-control ">
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
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
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        District:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlDistrict" runat="server"
                                                            AutoPostBack="true" class="form-control " />
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-lg-1 col-md-1 col-sm-1 cpl-xs-12 col-lg-offset-0 col-md-offset-0 col-sm-offset-0 col-xs-offset-0">
                                                <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-right" ValidationGroup="saves"
                                                    BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />

                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>




                            <div class="panel-body">
                                <div class="row table-responsive">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                        <div style="height: 390px; overflow: auto; width: 100%;" align="center">
                                            <asp:GridView ID="GVCluster" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                AllowPaging="true" PageSize="100" OnRowDataBound="GV_luster_OnRowDataBound" AutoGenerateColumns="False" Font-Names="Arial"
                                                Font-Size="12px" Width="100%">
                                                <EmptyDataTemplate>
                                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                        Data not found
                                                    </div>
                                                </EmptyDataTemplate>
                                                <FooterStyle CssClass="FooterStyle" />
                                                <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                <PagerStyle CssClass="paging" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SchoolLevel" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFromName" class="labelGrid" ForeColor="Black" runat="server"
                                                                  Text='<%# Eval("SchoolLevelName") %>'></asp:Label>

                                                            <asp:Label ID="lblMonth" Visible="false" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  
                                                    <asp:TemplateField HeaderText="Mangement" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFromNfame" class="labelGrid" ForeColor="Black" runat="server"
                                                                 Text='<%# Eval("Mangment") %>'></asp:Label>

                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Max Entry" Visible="true">
                                                        <ItemTemplate>

                                                            <asp:TextBox runat="server"  onkeypress="return isNumberKey(this,event);" ID="txtDob" autocomplete="off" MaxLength="2" ondrop="return false;" class="form-control"  Text='<%# Eval("EntryAllowed") %>'></asp:TextBox>

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
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
