<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FrmTB_TrainingMapping.aspx.cs" Inherits="FrmTB_TrainingMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .modal-content
        {
            position: relative;
            background-color: #fefefe;
            margin: auto;
            padding: 0;
            border: 1px solid #888;
            width: 80%;
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
            animation-name: animatetop;
            animation-duration: 0.4s;
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
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <h3 class="text-danger" style="margin: 0px;">
                                    Training Update Module</h3>
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
                            <div id="div-show-new">
                                <div class="row marg search-bg">
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
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
                                            <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12" style="display: none;">
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
                                            <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12" style="display: none;">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        District:</label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" class="form-control " />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-4 padd linhei" style="padding-top: 2px;">
                                                        Training Type:</label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlTraining" runat="server" class="form-control " AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlTraining_OnSelectedIndexChanged">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="TB Training" Value="T"></asp:ListItem>
                                                            <asp:ListItem Text="Staff Training" Value="S"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-4 padd linhei" style="padding-top: 2px;">
                                                        Training Outcome:</label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlOutcomeFilter" runat="server" class="form-control ">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 cpl-xs-11 pull-right">
                                                <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" OnClick="btnSerach_Click"
                                                    class="btn btn-danger btn-paddd" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />
                                                <asp:ImageButton ID="btnAdd" ToolTip="Serach" runat="server" OnClick="btnAdd_Click"
                                                    class="btn btn-danger btn-paddd" BackColor="#f1f1f1" ImageUrl="~/images/add-29-1.png" />
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
                                                        <div style="height: 600px; overflow: auto; width: 99%;" align="center">
                                                            <div>
                                                                <div class="Row" style="width: 100%">
                                                                    <asp:GridView ID="gvnroll" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                        OnRowDataBound="gvnroll_OnRowDataBound" OnRowCreated="gvnroll_RowCreated" AutoGenerateColumns="False"
                                                                        Font-Names="Arial" Font-Size="12px" Width="90%">
                                                                        <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
                                                                        </EmptyDataTemplate>
                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <Columns>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTB_GUID" ForeColor="Black" runat="server" Text='<%# Eval("TB_GUID") %>'>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="lblStateCode" ForeColor="Black" runat="server" Text='<%# Eval("StateCode") %>'>
                                                                                    </asp:Label>
                                                                                    <asp:Label ID="lblDistrictCode" ForeColor="Black" Visible="false" runat="server"
                                                                                        Text='<%# Eval("DistrictCode") %>'></asp:Label>
                                                                                    <asp:Label ID="lblLearningID" ForeColor="Black" Visible="false" runat="server" Text='<%# Eval("LearningID") %>'></asp:Label>
                                                                                    <asp:Label ID="lblOutComeID" ForeColor="Black" Visible="false" runat="server" Text='<%# Eval("OutcomeID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Training Outcome" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblOutcome" ForeColor="Black" runat="server" Text='<%# Eval("OutcomeName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" Width="15%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Specific Training Name" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl" ForeColor="Black" runat="server" Text='<%# Eval("TrainingOutcome") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" Width="20%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Training Outcome" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTrainingOutcome" ForeColor="Black" runat="server" Text='<%# Eval("TrainingOutcome") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" Width="35%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Y1">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="TxtN_P1_Y1" MaxLength="3" CssClass="form-control" ForeColor="Black"
                                                                                        Width="96%" runat="server" Text='<%# Eval("N_P1_Y1") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Y2">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="TxtN_P1_Y2" MaxLength="3" CssClass="form-control" ForeColor="Black"
                                                                                        Width="96%" runat="server" Text='<%# Eval("N_P1_Y2") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Y3">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="TxtN_P1_Y3" MaxLength="3" CssClass="form-control" ForeColor="Black"
                                                                                        Width="96%" runat="server" Text='<%# Eval("N_P1_Y3") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Y4">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="TxtN_P2_Y1" MaxLength="3" CssClass="form-control" ForeColor="Black"
                                                                                        Width="96%" runat="server" Text='<%# Eval("N_P2_Y1") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Y5">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="TxtN_P2_Y2" MaxLength="3" CssClass="form-control" ForeColor="Black"
                                                                                        Width="96%" runat="server" Text='<%# Eval("N_P2_Y2") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="TxtN_P2_Y3" MaxLength="3" CssClass="form-control" ForeColor="Black"
                                                                                        Width="96%" runat="server" Text='<%# Eval("N_P2_Y3") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Y6">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="TxtN_P3_Y1" MaxLength="3" CssClass="form-control" ForeColor="Black"
                                                                                        Width="96%" runat="server" Text='<%# Eval("N_P3_Y1") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Y7">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="TxtN_P3_Y2" MaxLength="3" CssClass="form-control" ForeColor="Black"
                                                                                        Width="96%" runat="server" Text='<%# Eval("N_P3_Y2") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>

                                                                               <asp:TemplateField HeaderText="Y8">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="TxtN_P4_Y2" MaxLength="3" CssClass="form-control" ForeColor="Black"
                                                                                        Width="96%" runat="server" Text='<%# Eval("N_P4_Y2") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Y9">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="TxtN_P4_Y3" MaxLength="3" CssClass="form-control" ForeColor="Black"
                                                                                        Width="96%" runat="server" Text='<%# Eval("N_P4_Y3") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Y3" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="TxtN_P3_Y3" MaxLength="3" CssClass="form-control" ForeColor="Black"
                                                                                        Width="96%" runat="server" Text='<%# Eval("N_P3_Y3") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>

                                                                         

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="btnActiveDeactive" runat="server" OnClick="btnActiveDeactive_Click" />
                                                                                    <asp:HiddenField ID="hdnActiveDeactive" Value='<%# Eval("ActiveStatus") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="7%" />
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
                                <cc1:ModalPopupExtender ID="MpexdrDistrict" runat="server" BackgroundCssClass="modal-content"
                                    CancelControlID="CancelButton" PopupControlID="PnlDistrict" TargetControlID="HdnFild">
                                </cc1:ModalPopupExtender>
                                <asp:HiddenField ID="HdnFild" runat="server"></asp:HiddenField>
                                <asp:Panel CssClass="model-wid" Style="display: none; height: auto; width: 45% !important;
                                    margin-top: -112px !important; top: 452px !important;" ID="PnlDistrict" runat="server">
                                    <div style="width: 100%; height: auto; background-color: #f1f1f1">
                                        <div class="modal-header" style="background-color: #ddd; color: Black;">
                                            <h4 class="modal-title" style="forecolor: Black">
                                                Training Outcome</h4>
                                        </div>
                                        <div class="modal-body">
                                            <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                            <div class="form-horizontal" role="form">
                                                <div id="divoutcome" runat="server" class="form-group">
                                                    <asp:Label ID="Label2" class="control-label col-sm-4 lab-text-left" runat="server" ForeColor="Black"
                                                        Text="Training Outcome"></asp:Label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList runat="server" ID="ddlOutCome" autocomplete="off" ondrop="return false;"
                                                            class="form-control" onkeypress="return false;">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label1" class="control-label col-sm-4 lab-text-left" ForeColor="Black"
                                                        runat="server" Text="Specific Training Name"></asp:Label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" ID="txtTrainingOutCome" autocomplete="off" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <asp:ImageButton ID="btnNewUserSave" runat="server" ImageUrl="~/images/save-29-1.png"
                                                Text="Save" ToolTip="Save" OnClientClick="return SaveDataVali();" OnClick="btnSaveNew_Click"
                                                Style="float: none;" ValidationGroup="saves"></asp:ImageButton>&nbsp;
                                            <asp:ImageButton ID="CancelButton" ImageUrl="~/images/close-29.png" runat="server"
                                                Style="float: none;"></asp:ImageButton></div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <!-- /#wrapper -->
                    <!-- /#wrapper -->
                </div>
            </div>
        </div>
    </div>
</asp:Content>
