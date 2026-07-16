<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FrmGKPsessionMaster.aspx.cs" Inherits="FrmGKPsessionMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function isNumberKey(txt, evt) {
            debugger;
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
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="padding: 5px 15px;">
                            <div class="row">
                                <div class="col-lg-4" style="padding-left: 0px;">

                                    <h3 class="text-danger" style="margin: 0px;">
                                        <asp:Label ID="lblMain" runat="server" Text="GKP Master"></asp:Label>
                                    </h3>

                                </div>
                                <div class="col-lg-2">
                                </div>
                                <div class="col-lg-4" runat="server" visible="false">
                                    <div class="pull-right">
                                        <asp:Button ID="btnGKPAssemnet" runat="server" Text="Add Assessment"
                                            OnClick="btnAddAssessment_click" />
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="pull-right">
                                        <asp:ImageButton ID="btnAddSession" Style="border-width: 0px; margin-left: -138px;" runat="server" Text="Add Session" ImageUrl="~/images/add-29-1.png"
                                            OnClick="btnAddSession_click" />
                                    </div>
                                    <div class="pull-right">
                                        <asp:LinkButton ID="LinkddButton1" runat="server" Text="Export to Excel" OnClick="btnReprot_Click"
                                            class="pull-right" Style="margin-top: 5px;"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div>
                                    <div class="row marg search-bg">
                                        <div class="form-horizontal">
                                            <div class="col-lg-2 col-md-3 col-sm-3 cpl-xs-12" runat="server" visible="false">
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
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" style="margin-left: 10px;">
                                                <div class="form-group" style="margin-bottom: 5px;">
                                                    <label for="lblMastertype" class="col-sm-4 padd linhei" style="padding-top: 2px;">
                                                        Master type
                                                    </label>
                                                    <div class="col-sm-7 padd">
                                                        <asp:DropDownList ID="ddlMastertype" OnSelectedIndexChanged="ddlMastertype_SelectedIndexChanged" runat="server" class="form-control ">
                                                            <asp:ListItem Text="GKP Session master" Value="1"> </asp:ListItem>
                                                            <asp:ListItem Text="GKP Assessment master" Value="2"> </asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-2 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                                <asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="btn btn-danger btn-sm"
                                                    OnClick="BtnSearch_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                    <div style="overflow: auto; height: 350px;">
                                        <asp:GridView ID="GridGKPSession" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                            PageSize="20" OnRowDataBound="GKP_RowDataBound" OnPageIndexChanging="GridGKPSessionMaster_PageIndexChanging" OnRowCommand="GridGKPSession_RowCommand"
                                            DataKeyNames="GKIPID,GKPLevelID,SchoolL	,GKPLevel,MainSession,BaselineSession,RevisionSession,Remedial,RemedialL1,	RecapSession,Endline"
                                            CssClass="table table-striped table-bordered table-condensed" Width="100%">
                                            <PagerSettings Position="Bottom" PageButtonCount="5"></PagerSettings>
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemStyle Width="5%" />
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="School GKP Level">
                                                    <ItemTemplate>
                                                        <%--  <asp:Label ID="blbGKPLevelID" runat="server" Text='<%# Eval("GKPLevelID") %>'></asp:Label>--%>
                                                        <asp:Label ID="lblGKIPID" Visible="false" runat="server" Text='<%# Eval("GKIPID") %>'></asp:Label>


                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("SchoolGKPLevel") %>'></asp:Label>
                                                        <asp:DropDownList ID="ddlSchoolGKP" Visible="false" runat="server" class="form-control ">
                                                            <asp:ListItem Text="GKP L0/L1" Value="1"> </asp:ListItem>
                                                            <asp:ListItem Text="GKP L1/L2" Value="2"> </asp:ListItem>
                                                            <asp:ListItem Text="GKP L2/L3" Value="3"> </asp:ListItem>

                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GKP Level">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGKPLessvel" runat="server" Text='<%# Eval("GKPLevel") %>'></asp:Label>
                                                        <%-- <asp:DropDownList ID="ddlGKPLevel" Visible="false"  runat="server" class="form-control ">
                                                            <asp:ListItem Text="Level 0" Value="0"> </asp:ListItem>
                                                            <asp:ListItem Text="Level 1" Value="1"> </asp:ListItem>
                                                            <asp:ListItem Text="Level 2" Value="2"> </asp:ListItem>
                                                            <asp:ListItem Text="Level 3" Value="3"> </asp:ListItem>
                                                        </asp:DropDownList>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="#Baseline Session">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBaselineSession" runat="server" Text='<%# Eval("BaselineSession") %>'></asp:Label>

                                                        <asp:Label ID="lblCUniqueChildCode" Visible="false" runat="server" Text='<%# Eval("GKIPID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="#Main Sessions">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMainSession" runat="server" Text='<%# Eval("MainSession") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="#Revision Session">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRevisionSession" runat="server" Text='<%# Eval("RevisionSession") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="#Remedial">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemedial" runat="server" Text='<%# Eval("Remedial") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="#Remedial L0">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRecapSession" runat="server" Text='<%# Eval("RecapSession") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="#Remedial L1">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRecapSession1" runat="server" Text='<%# Eval("RemedialL1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Endline">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEndline" runat="server" Text='<%# Eval("Endline") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="GvImgbtnEdit" CommandName="EditData" runat="server" ImageUrl="~/images/edit.png"
                                                            CommandArgument='<%# Container.DataItemIndex %>' ToolTip="Edit" Style="margin-top: 10px;"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="10px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="GvImgbtnDelete" runat="server" ImageUrl="~/images/delete-29.png"
                                                            CommandArgument='<%# Eval("GKIPID") %>' OnClick="btn_Delete_Click" OnClientClick="return confirm('Are you sure you want to delete?');"
                                                            ToolTip="Delete"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                            <EmptyDataTemplate>
                                                <table style="border: 0px;">
                                                    <tr>
                                                        <td style="border: 0px;">
                                                            <asp:Label ID="lblEmptySearch" runat="server">No results found</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                        <asp:GridView ID="GridGKPAssessment" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                            PageSize="20" DataKeyNames="GKIPAID,SubjectID,	GKPAssessmentQuestions,	GKPMicroskillQuestion	,MaxScoreAssessment,	MaxScoreMicroskill	" OnRowDataBound="GKP1_RowDataBound"
                                            OnPageIndexChanging="GridGKPAssessmentMaster_PageIndexChanging" OnRowCommand="GridGKPAssessment_RowCommand"
                                            CssClass="table table-striped table-bordered table-condensed" Width="100%">
                                            <PagerSettings Position="Bottom" PageButtonCount="5"></PagerSettings>
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemStyle Width="5%" />
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubjrrrectID" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>

                                                        <asp:Label ID="lblGKIPAID" Visible="false" runat="server" Text='<%# Eval("GKIPAID") %>'></asp:Label>

                                                        <asp:DropDownList ID="DDlSubject" Visible="false" runat="server" class="form-control ">
                                                            <asp:ListItem Text="Hindi" Value="0"> </asp:ListItem>
                                                            <asp:ListItem Text="English" Value="1"> </asp:ListItem>
                                                            <asp:ListItem Text="Math" Value="2"> </asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblCUniqueChildCode" Visible="false" runat="server" Text='<%# Eval("GKIPAID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GKP Assesment Question name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGKPAssessmentQuestions" runat="server" Text='<%# Eval("GKPAssessmentQuestions") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GKP MicroSkill Question name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGKPMicroskillQuestion" runat="server" Text='<%# Eval("GKPMicroskillQuestion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Max Score Assesment Qustion">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMaxScoreAssessment" runat="server" Text='<%# Eval("MaxScoreAssessment") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Max Score MicroSkill Question">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMaxScoreMicroskill" runat="server" Text='<%# Eval("MaxScoreMicroskill") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="GvImgbtnEdit1" CommandName="EditData" runat="server" ImageUrl="~/images/edit.png"
                                                            CommandArgument='<%# Container.DataItemIndex %>' ToolTip="Edit" Style="margin-top: 10px;"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="10px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="GvImgbtnDelete1" runat="server" OnClick="btn_Delete_Click1"
                                                            ImageUrl="~/images/delete-29.png" CommandArgument='<%# Eval("GKIPAID") %>' OnClientClick="return confirm('Are you sure you want to delete?');"
                                                            ToolTip="Delete"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                            <EmptyDataTemplate>
                                                <table style="border: 0px;">
                                                    <tr>
                                                        <td style="border: 0px;">
                                                            <asp:Label ID="lblEmptySearch" runat="server">No results found</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                    <asp:HiddenField ID="HdnGKIPAID" runat="server" />
                                    <asp:HiddenField ID="HdnGKIPID" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <cc1:ModalPopupExtender ID="ModalAddGKPSession" runat="server" BackgroundCssClass="modalBg "
                        CancelControlID="CancelButton" PopupControlID="PnlGKPSession" TargetControlID="HdnFild">
                    </cc1:ModalPopupExtender>
                    <asp:HiddenField ID="HdnFild" runat="server"></asp:HiddenField>
                    <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 80% !important; margin-top: -31%;"
                        ID="PnlGKPSession" runat="server">
                        <div style="width: 100%; height: auto; background-color: #f1f1f1">
                            <div class="modal-header" style="background-color: #ddd; padding: 10px;">
                                <asp:Label ID="lblFormName" runat="server" Text="Add GKP Session Master" CssClass="text-danger"
                                    Font-Bold="true"></asp:Label>
                                <asp:LinkButton ID="CancelButton" CssClass="btn btn-sm btn-danger pull-right" runat="server"> <span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label ID="Label8" runat="server" Text="School GKP Level"></asp:Label>
                                        <asp:DropDownList ID="ddlSchoolGKP" runat="server" class="form-control "
                                            ValidationGroup="ValidGKP">
                                            <asp:ListItem Text="सृजन/बोध" Value="1"> </asp:ListItem>
                                            <asp:ListItem Text="बोध/प्रवाह" Value="2"> </asp:ListItem>
                                            <asp:ListItem Text="प्रवाह/उत्सव" Value="3"> </asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="Label7" runat="server" Text="GKP Level"></asp:Label>
                                        <asp:DropDownList ID="ddlGKPLevel" runat="server" class="form-control "
                                            ValidationGroup="ValidGKP">
                                            <asp:ListItem Text="सृजन" Value="0"> </asp:ListItem>
                                            <asp:ListItem Text="बोध" Value="1"> </asp:ListItem>
                                            <asp:ListItem Text="प्रवाह" Value="2"> </asp:ListItem>
                                            <asp:ListItem Text="उत्सव" Value="3"> </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblMainSession" runat="server" Text="Main Session"></asp:Label>
                                        <asp:TextBox ID="txtMainSession" runat="server" CssClass="form-control" ValidationGroup="ValidGKP"
                                            onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>



                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="red" runat="server" ControlToValidate="txtMainSession"
                                            CssClass="failureNotification" ErrorMessage="*" ValidationGroup="ValidGKP" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblBaselineSession" runat="server" Text="Baseline Session"></asp:Label>
                                        <asp:TextBox ID="TxtBaselineSession" runat="server" CssClass="form-control" ValidationGroup="ValidGKP"
                                            onkeypress="return isNumberKey(this,event);" MaxLength="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="red" runat="server" ControlToValidate="TxtBaselineSession"
                                            CssClass="failureNotification" ErrorMessage="*" ValidationGroup="ValidGKP" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblRevisionSession" runat="server" Text="Revision Session"></asp:Label>
                                        <asp:TextBox ID="TxtRevisionSession" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);"
                                            MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="red" runat="server" ControlToValidate="TxtRevisionSession"
                                            CssClass="failureNotification" ErrorMessage="*" ValidationGroup="ValidGKP" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblRemedial" runat="server" Text="Remedial"></asp:Label>
                                        <asp:TextBox ID="TxtRemedial" runat="server" CssClass="form-control" ValidationGroup="ValidGKP"
                                            onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="red" runat="server" ControlToValidate="TxtRemedial"
                                            CssClass="failureNotification" ErrorMessage="*" ValidationGroup="ValidGKP" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblRecapSession" runat="server" Text="Remedial L0"></asp:Label>
                                        <asp:TextBox ID="TxtRecapSession" runat="server" ValidationGroup="ValidGKP" CssClass="form-control"
                                            onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ForeColor="red" runat="server" ControlToValidate="TxtRecapSession"
                                            CssClass="failureNotification" ErrorMessage="*" ValidationGroup="ValidGKP" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-3">
                                        <asp:Label ID="Label2" runat="server" Text="Remedial L1"></asp:Label>
                                        <asp:TextBox ID="TxtRecapSession1" runat="server" ValidationGroup="ValidGKP" CssClass="form-control"
                                            onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="red" runat="server" ControlToValidate="TxtRecapSession1"
                                            CssClass="failureNotification" ErrorMessage="*" ValidationGroup="ValidGKP" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="col-lg-3">
                                        <asp:Label ID="lblEndline" runat="server" Text="Endline"></asp:Label>
                                        <asp:TextBox ID="TxtEndline" runat="server" CssClass="form-control" ValidationGroup="ValidGKP"
                                            onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ForeColor="red" runat="server" ControlToValidate="TxtEndline"
                                            CssClass="failureNotification" ErrorMessage="*" ValidationGroup="ValidGKP" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <br />
                                <div class="modal-footer" style="background-color: #ddd; padding: 10px;">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success btn-sm"
                                        OnClick="btnSave_Click" ValidationGroup="ValidGKP" />&nbsp;
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-info btn-sm"
                                        OnClick="btnClear_Click" />&nbsp;
                                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" />
                                </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <cc1:ModalPopupExtender ID="ModalAddGKPAssesment" runat="server" BackgroundCssClass="modalBg "
                        CancelControlID="CancelButton" PopupControlID="PnlGKPA" TargetControlID="HdnGKPA">
                    </cc1:ModalPopupExtender>
                    <asp:HiddenField ID="HdnGKPA" runat="server"></asp:HiddenField>
                    <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 80% !important; margin-top: -31%;"
                        ID="PnlGKPA" runat="server">
                        <div style="width: 100%; height: auto; background-color: #f1f1f1">
                            <div class="modal-header" style="background-color: #ddd; padding: 10px;">
                                <asp:Label ID="Label9" runat="server" Text="Add Assessment Master" CssClass="text-danger"
                                    Font-Bold="true"></asp:Label>
                                <asp:LinkButton ID="LinkButton1" CssClass="btn btn-sm btn-danger pull-right" runat="server"> <span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblSubJect" runat="server" Text="Subject"></asp:Label>
                                        <asp:DropDownList ID="DDlSubject" AutoPostBack="true" runat="server" class="form-control "
                                            ValidationGroup="Valid">
                                            <asp:ListItem Text="Hindi" Value="1"> </asp:ListItem>
                                            <asp:ListItem Text="English" Value="2"> </asp:ListItem>
                                            <asp:ListItem Text="Math" Value="3"> </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblGKPAssstions" runat="server" Text="GKPAssessmentQuestions"></asp:Label>
                                        <asp:TextBox ID="TxtGKPAssessmentQuestions" runat="server" CssClass="form-control"
                                            ValidationGroup="Valid"></asp:TextBox>


                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" ForeColor="red" runat="server" ControlToValidate="TxtGKPAssessmentQuestions"
                                            CssClass="failureNotification" ErrorMessage="*" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>


                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblGKPMicroskillQuestion" runat="server" Text="GKPMicroskillQuestion"></asp:Label>
                                        <asp:TextBox ID="TxtGKPMicroskillQuestion" runat="server" CssClass="form-control"
                                            ValidationGroup="Valid"></asp:TextBox>

                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblMaxScoreAssessment" runat="server" Text="MaxScoreAssessment"></asp:Label>
                                        <asp:TextBox ID="TxtMaxScoreAssessment" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);"
                                            MaxLength="2" ValidationGroup="Valid"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="red" runat="server"
                                            ControlToValidate="TxtMaxScoreAssessment"
                                            CssClass="failureNotification" ErrorMessage="*" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblMaxScoreMicroskill" runat="server" Text="MaxScoreMicroskill"></asp:Label>
                                        <asp:TextBox ID="TxtMaxScoreMicroskill" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);"
                                            MaxLength="2" ValidationGroup="Valid"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="red" runat="server"
                                            ControlToValidate="TxtMaxScoreMicroskill"
                                            CssClass="failureNotification" ErrorMessage="*" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-3">
                                    </div>
                                </div>
                                <br />
                                <div class="modal-footer" style="background-color: #ddd; padding: 10px;">
                                    <asp:Button ID="BtnGPKA" runat="server" Text="Save" CssClass="btn btn-success btn-sm"
                                        OnClick="BtnGPKA_Click" ValidationGroup="Valid" />&nbsp;
                                    <asp:Button ID="btnGPKA_Clear" runat="server" Text="Clear" CssClass="btn btn-info btn-sm"
                                        OnClick="btnGPKA_Clear_Click" />&nbsp;
                                    <asp:Button ID="Button3" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" />
                                </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GridGKPSession" />
            <asp:PostBackTrigger ControlID="GridGKPAssessment" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="LinkddButton1" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
