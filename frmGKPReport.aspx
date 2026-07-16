<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmGKPReport.aspx.cs" MasterPageFile="~/Site.master" Inherits="frmGKPReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
            debugger;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46 && charCode == 127) {
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
                        <div class="panel panel-default" style="height: 550px;">
                            <div class="panel-heading" style="padding: 5px;">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                        <h3 class="text-danger" style="margin: 0px;">
                                            <asp:Label ID="lblMain" runat="server" Text="GKP Report "></asp:Label>
                                        </h3>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">

                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Export to Excel" OnClick="btnImport_Click"
                                            class="pull-right"></asp:LinkButton>
                                        <asp:ImageButton ID="btnsave" Visible="false" CssClass="btn btn-info pull-right btn-sm" BackColor="#f5f5f5"
                                            ToolTip="Save" ImageUrl="~/images/save-29-1.png" ValidationGroup="saves" OnClick="btnSave_Click"
                                            Style="margin-right: 5px; padding: 0px;" runat="server" />

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div style="padding: 0px 10px 5px 10px;">
                                    <div class="row marg search-bg">
                                        <div class="form-horizontal">
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Year:
                                                    </label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" class="form-control ">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-10 ">
                                                <%--  <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server"
                                                            class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />--%>
                                                <asp:Button ID="btndisplay" Style="margin-right: 8px" runat="server" class="btn btn-danger pull-left btn-sm" Text="Report"
                                                    OnClick="btn_display_Click" />

                                            </div>

                                            <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-10 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                                <asp:ImageButton ID="ImageButton11" CssClass="btn btn-info pull-right"
                                                    BackColor="#f5f5f5" ToolTip="Add" OnClick="btnAddGkp_Click" Visible="false" ImageUrl="~/images/add-29-1.png"
                                                    Style="margin-right: 5px; padding: 0px;" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>




                            <div class="panel-body">
                                <div class="row table-responsive">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div style="height: 610px; overflow: auto; width: 99%;" align="center">
                                            <asp:GridView ID="DGV_CLT" runat="server" CssClass="table-striped table-bordered table-hover"
                                                AutoGenerateColumns="False" Width="50%">

                                                <FooterStyle CssClass="FooterStyle" />
                                                <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                <RowStyle HorizontalAlign="Left" />
                                                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                <Columns>




                                                    <asp:TemplateField HeaderText="Subject">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHindi" BackColor="Transparent" CssClass="form-controlAbhi" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>

                                                        </ItemTemplate>
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Level">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHiLevelndi" BackColor="Transparent" CssClass="form-controlAbhi" runat="server" Text='<%# Eval("Level") %>'></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sessions">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHiLevelndi" BackColor="Transparent" CssClass="form-controlAbhi" runat="server" Text='<%# Eval("Session") %>'></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Main">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHiLevddelndi" BackColor="Transparent" CssClass="form-controlAbhi" runat="server" Text='<%# Eval("Main") %>'></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Revision">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHiLevRevisiondi" BackColor="Transparent" CssClass="form-controlAbhi" runat="server" Text='<%# Eval("Revision") %>'></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle HorizontalAlign="Center" />
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

                <asp:ModalPopupExtender ID="ModalAlert" runat="server" TargetControlID="hdn_alertmodal"
                    BehaviorID="ModalAlertb" PopupControlID="pnl_alert" CancelControlID="btn_cancelalert"
                    BackgroundCssClass="ModalPopupBG">
                </asp:ModalPopupExtender>
                <asp:HiddenField ID="hdn_alertmodal" runat="server" />
                <asp:Panel ID="pnl_alert" runat="server" Style="display: none;" BackColor="#E8E5E2"
                    BorderColor="#E8E5E2" BorderStyle="Ridge" BorderWidth="2px" Width="500px" Height="200px">
                    <div class="divbgs" style="padding: 0 0 10px 0;">
                        <div class="longnamecsspop" style="background-color: #545454; color: White; font-family: arial,
            Helvetica, sans-serif; font-size: 19px; width: 100%; padding: 5px 10px 0 10px; margin-left: auto; margin-right: auto; height: 34px;">
                            Alert !
                        </div>
                        <div class="row" style="margin-top: 10px">
                            <div class="col-xs-4">
                                <asp:Label ID="Label1" runat="server">Subject</asp:Label>
                                <asp:DropDownList runat="server" ID="DropDownList2" CssClass="form-control">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">Hindi</asp:ListItem>
                                    <asp:ListItem Value="2">English</asp:ListItem>
                                    <asp:ListItem Value="3">Maths</asp:ListItem>


                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-4">
                                <asp:Label ID="Label2" runat="server">Level</asp:Label>
                                <asp:DropDownList ID="ddMainlLevel" runat="server" class="form-control ">
                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">L0</asp:ListItem>
                                    <asp:ListItem Value="2">L1</asp:ListItem>
                                    <asp:ListItem Value="3">L2</asp:ListItem>
                                    <asp:ListItem Value="4">L3</asp:ListItem>

                                </asp:DropDownList>


                            </div>
                            <div class="col-xs-4">
                                <asp:Label ID="Label3" runat="server">No Of Session</asp:Label>
                                <asp:TextBox runat="server" ID="TextBox4" CssClass="form-control" Enabled="True"></asp:TextBox>
                            </div>

                        </div>
                        <div style="text-align: center; margin-top: 65px; margin-right: 223px;">
                            <asp:Button ID="btn_cancelalert" runat="server" class="btn btn-danger
            pull-right"
                                Text=" Cancel " Height="33px" Width="59px" />
                            <asp:Button ID="btn_show" runat="server" class="btn btn-danger
            pull-right"
                                Text=" Show " Style="margin-right: 5px" Height="33px" Width="59px" />
                        </div>
                    </div>
                </asp:Panel>




                <div class="row" style="margin: 0px 0px 10px 0px;">
                    <div class="col-xs-12" style="padding: 0px;">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel ID="Panel4" Style="overflow: auto;" runat="server" Width="100%">
                                    <asp:GridView ID="Dgv_LeftGrid" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                                        CssClass="table table-striped table-bordered  table-responsive" Width="100%" Font-Size="12px"
                                        AllowPaging="true" PageSize="15" OnRowDataBound="Dgv_LeftGrid_RowDataBound">
                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                        <EmptyDataTemplate>
                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                                Data not found
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Subject" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="SubjectID" runat="server" Text='<%# Bind("SubjectID") %>' Style="width: 100%;"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="13%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" CssClass="gridcolpadding"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Level" Visible="true">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlLevelID" runat="server" class="form-control ">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">L0</asp:ListItem>
                                                        <asp:ListItem Value="2">L1</asp:ListItem>
                                                        <asp:ListItem Value="3">L2</asp:ListItem>
                                                        <asp:ListItem Value="4">L3</asp:ListItem>

                                                    </asp:DropDownList>

                                                </ItemTemplate>
                                                <HeaderStyle Width="13%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" CssClass="gridcolpadding"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Name" Visible="true">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="StudentName" runat="server" Style="width: 100%;"></asp:TextBox>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>

                                        <PagerStyle CssClass="dgvPageing" />
                                        <HeaderStyle BackColor="#A7A2A4" ForeColor="White" />
                                        <FooterStyle BackColor="Transparent" />
                                    </asp:GridView>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="Dgv_LeftGrid" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="LinkButton1" />



        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
