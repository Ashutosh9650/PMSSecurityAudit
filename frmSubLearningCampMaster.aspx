<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmSubLearningCampMaster.aspx.cs" Inherits="frmSubLearningCampMaster" %>

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
                        <div class="panel-heading" style="padding: 5px 5px 5px 10px;">
                            <div class="row">
                                <div class="col-lg-4">
                                    <h3 class="text-danger" style="margin: 0px;">
                                        <asp:Label ID="lblMain" runat="server" Text="SBL Session Master"></asp:Label>
                                    </h3>

                                </div>
                                <div class="col-lg-4"></div>
                                <div class="col-lg-4">
                                    <div class="pull-right">
                                        <asp:Button ID="btnAddCamp" runat="server" Text="Add Camp" CssClass="btn btn-danger btn-sm" Style="margin-right: 15px;" OnClick="btnAddCamp_Click" />
                                        <asp:LinkButton ID="LinkddButton1" runat="server" Text="Export to Excel" OnClick="btnReprot_Click"
                                            class="pull-right"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                    <div style="overflow: auto; height: 350px;">
                                        <asp:GridView ID="GridLearningCampMaster" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="12"
                                            OnPageIndexChanging="GridLearningCampMaster_PageIndexChanging" OnRowCommand="GridLearningCampMaster_RowCommand" DataKeyNames="CampID,CampNumber,CampNumberName,CampDurationInWeek,SessionInCamp,SessionInWeek,HindiBaselineSessionNo,HindiEndSessionNo,MathBaselineSessionNo,MathEndSessionNo,HindiBaselineHeading1Name,HindiBaselineHeading2Name,MathBaselineHeading1Name,MathBaselineHeading2Name,HindiBaselineEndlineMaxScore,MathBaselineEndlineMaxScore,HindiBaselineEndlineHeading2Active,MathBaselineEndlineHeading2Active,HindiBaselineEndlineMaxScore1,MathBaselineEndlineMaxScore1,HindiBaselineHeading3Name   , MathBaselineHeading3Name    , HindiBaselineEndlineHeading3Active, MathBaselineEndlineHeading3Active,HindiBaselineEndlineMaxScore2, MathBaselineEndlineMaxScore2   ,HindiBaselineHeading4Name   ,MathBaselineHeading4Name    ,HindiBaselineEndlineMaxScore4      ,MathBaselineEndlineMaxScore4   ,MathBaselineEndlineHeading4Active      ,HindiBaselineEndlineHeading4Active" CssClass="table table-striped table-bordered table-condensed" Width="100%">
                                            <PagerSettings Position="Bottom" PageButtonCount="5"></PagerSettings>
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemStyle Width="5%" />
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Camp No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbCampNumber" runat="server" Text='<%# Eval("CampNumberName") %>'></asp:Label>
                                                        <asp:Label ID="Label2" Visible="false" runat="server" Text='<%# Eval("CampID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Camp Duretion(Week)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbCampDurationInWeek" runat="server" Text='<%# Eval("CampDurationInWeek") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="#Session">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbSessionInCamp" runat="server" Text='<%# Eval("SessionInCamp") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="#Sessions Per Week">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbSessionInWeek" runat="server" Text='<%# Eval("SessionInWeek") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hindi- BL Session No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbHindiBaselineSessionNo" runat="server" Text='<%# Eval("HindiBaselineSessionNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hindi- EL Session No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbHindiEndSessionNo" runat="server" Text='<%# Eval("HindiEndSessionNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Math- BL Session No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbMathBaselineSessionNo" runat="server" Text='<%# Eval("MathBaselineSessionNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Math- EL Session No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbMathEndSessionNo" runat="server" Text='<%# Eval("MathEndSessionNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Assessment H1-Hindi">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbHindiBaselineHeading1Name" runat="server" Text='<%# Eval("HindiBaselineHeading1Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Assessment H2-Hindi">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbHindiBaselineHeading2Name" runat="server" Text='<%# Eval("HindiBaselineHeading2Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Assessment H3-Hindi">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ff" runat="server" Text='<%# Eval("HindiBaselineHeading3Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Assessment H4-Hindi">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ffddddw" runat="server" Text='<%# Eval("HindiBaselineHeading4Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Assessment H1-Math">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbMathBaselineHeading1Name" runat="server" Text='<%# Eval("MathBaselineHeading1Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Assessment H2-Math">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbMathBaselineHeading2Name" runat="server" Text='<%# Eval("MathBaselineHeading2Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Assessment H3-Math">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbMathBaselineHeadingf2Name" runat="server" Text='<%# Eval("MathBaselineHeading3Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Assessment H4-Math">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbMaryngf2Name" runat="server" Text='<%# Eval("MathBaselineHeading4Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Hindi-H1 max Score">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbHindiBaselineEndlineMaxScore" runat="server" Text='<%# Eval("HindiBaselineEndlineMaxScore") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Math-H1 Max Score">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbMathBaselineEndlineMaxScore" runat="server" Text='<%# Eval("MathBaselineEndlineMaxScore") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Hindi-H2 max Score">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbHindiBaselineEndlineMaxScore1" runat="server" Text='<%# Eval("HindiBaselineEndlineMaxScore1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Math-H2 max Score">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbMathBaselineEndlineMaxScore1" runat="server" Text='<%# Eval("MathBaselineEndlineMaxScore1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Hindi-H3 max Score">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbHindiBaselineEndlineMaxScore2" runat="server" Text='<%# Eval("HindiBaselineEndlineMaxScore2") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Math-H3 max Score">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbMathBaselineEndlineMaxScore2" runat="server" Text='<%# Eval("MathBaselineEndlineMaxScore2") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Hindi-H4 max Score">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbHiteScore2" runat="server" Text='<%# Eval("HindiBaselineEndlineMaxScore4") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Math-H4 max Score">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbMathB6re2" runat="server" Text='<%# Eval("MathBaselineEndlineMaxScore4") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Hindi H2 Active">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbHindiBaselineEndlineHeading2Active" runat="server" Text='<%# Eval("HindiBaselineEndlineHeading2Active") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Math H2 Active">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbMathBaselineEndlineHeading2Active" runat="server" Text='<%# Eval("MathBaselineEndlineHeading2Active") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Hindi H3 Active">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbHindiBaselineEndlineHeading3Active" runat="server" Text='<%# Eval("HindiBaselineEndlineHeading3Active") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Math H3 Active">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbMathBasdActive" runat="server" Text='<%# Eval("MathBaselineEndlineHeading3Active") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Hindi H4 Active">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbHindinddsdftive" runat="server" Text='<%# Eval("HindiBaselineEndlineHeading4Active") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Math H4 Active">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GvlbMathBasedddive" runat="server" Text='<%# Eval("MathBaselineEndlineHeading4Active") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    <HeaderStyle Font-Bold="true" Height="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="GvImgbtnEdit" CommandName="EditData" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Container.DataItemIndex %>'
                                                            ToolTip="Edit" Style="margin-top: 10px;"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="10px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="GvImgbtnDelete" OnClick="btn_Delete_Click" runat="server" ImageUrl="~/images/delete-29.png" CommandArgument='<%# Eval("CampID") %>'
                                                            OnClientClick="return confirm('Are you sure you want to delete?');" ToolTip="Delete"></asp:ImageButton>
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
                                    <asp:HiddenField ID="hdnCampID" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <cc1:ModalPopupExtender ID="ModalLearningCamp" runat="server" BackgroundCssClass="modalBg " CancelControlID="CancelButton" PopupControlID="PnlLearningCamp" TargetControlID="HdnFild">
                    </cc1:ModalPopupExtender>
                    <asp:HiddenField ID="HdnFild" runat="server"></asp:HiddenField>
                    <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 80% !important; margin-top: -40%;" ID="PnlLearningCamp" runat="server">
                        <div style="width: 100%; height: auto; background-color: #f1f1f1">
                            <div class="modal-header" style="background-color: #ddd; padding: 10px;">
                                <asp:Label ID="lblFormName" runat="server" Text="Add Sub Learning Master" CssClass="text-danger" Font-Bold="true"></asp:Label>
                                <asp:LinkButton ID="CancelButton" CssClass="btn btn-sm btn-danger pull-right" runat="server"> <span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblCampNo" runat="server" Text="Camp Number"></asp:Label>
                                        <asp:DropDownList ID="ddlCampNo" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="red" runat="server" ControlToValidate="ddlCampNo"
                                            CssClass="failureNotification" ErrorMessage="Select Camp No" ToolTip="Camp No" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblCampDurationInWeek" runat="server" Text="Camp Duretion(Week)"></asp:Label>
                                        <asp:TextBox ID="txtCampDurationInWeek" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="red" runat="server" ControlToValidate="txtCampDurationInWeek"
                                            CssClass="failureNotification" ErrorMessage="Enter Camp Duration In Week" ToolTip="Camp Duration In Week." ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblSessioninCamp" runat="server" Text="#Session"></asp:Label>
                                        <asp:TextBox ID="txtSessioninCamp" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="red" runat="server" ControlToValidate="txtSessioninCamp"
                                            CssClass="failureNotification" ErrorMessage="Enter Session in Camp" ToolTip="Session in Camp" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblSessioninWeek" runat="server" Text="#Sessions Per Week"></asp:Label>
                                        <asp:TextBox ID="txtSessioninWeek" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="red" runat="server" ControlToValidate="txtSessioninWeek"
                                            CssClass="failureNotification" ErrorMessage="Enter Session in Week" ToolTip="Session in Week" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblHindiBaselineSessionNo" runat="server" Text="Hindi- BL Session No."></asp:Label>
                                        <asp:TextBox ID="txtHindiBaselineSessionNo" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="red" runat="server" ControlToValidate="txtHindiBaselineSessionNo"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Baseline Session No." ToolTip="Hindi Baseline Session No." ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblHindiEndlineSessionNo" runat="server" Text="Hindi- EL Session No."></asp:Label>
                                        <asp:TextBox ID="txtHindiEndlineSessionNo" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="red" runat="server" ControlToValidate="txtHindiEndlineSessionNo"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Endline Session No." ToolTip="Hindi Endline Session No." ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-3">
                                        <asp:Label ID="lblMathBaselineSessionNo" runat="server" Text="Math Baseline Session No."></asp:Label>
                                        <asp:TextBox ID="txtMathBaselineSessionNo" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="red" runat="server" ControlToValidate="txtMathBaselineSessionNo"
                                            CssClass="failureNotification" ErrorMessage="Enter Math Baseline Session No." ToolTip="Math Baseline Session No." ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblMathEndlineSessionNo" runat="server" Text="Math- EL Session No."></asp:Label>
                                        <asp:TextBox ID="txtMathEndlineSessionNo" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ForeColor="red" runat="server" ControlToValidate="txtMathEndlineSessionNo"
                                            CssClass="failureNotification" ErrorMessage="Enter Math Endline Session No." ToolTip="Math Endline Session No." ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblHindiBaselineHeading1" runat="server" Text="Assessment H1-Hindi"></asp:Label>
                                        <asp:TextBox ID="txtHindiBaselineHeading1" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ForeColor="red" runat="server" ControlToValidate="txtHindiBaselineHeading1"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Baseline Heading 1" ToolTip="Hindi Baseline Heading 1" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblHindiBaselineHeading2" runat="server" Text="Assessment H2-Hindi"></asp:Label>
                                        <asp:TextBox ID="txtHindiBaselineHeading2" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ForeColor="red" runat="server" ControlToValidate="txtHindiBaselineHeading2"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Baseline Heading 2" ToolTip="Hindi Baseline Heading 2" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-3">
                                        <asp:Label ID="Label5" runat="server" Text="Assessment H3-Hindi"></asp:Label>
                                        <asp:TextBox ID="txtHindiBaselineHeading3" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ForeColor="red" runat="server" ControlToValidate="txtHindiBaselineHeading3"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Baseline Heading 2" ToolTip="Hindi Baseline Heading 3" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="Label11" runat="server" Text="Assessment H4-Hindi"></asp:Label>
                                        <asp:TextBox ID="txtHindiBaselineHeading4" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" ForeColor="red" runat="server" ControlToValidate="txtHindiBaselineHeading4"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Baseline Heading 2" ToolTip="Hindi Baseline Heading 4" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblMathBaselineHeading1" runat="server" Text="Assessment H1-Math"></asp:Label>
                                        <asp:TextBox ID="txtMathBaselineHeading1" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ForeColor="red" runat="server" ControlToValidate="txtMathBaselineHeading1"
                                            CssClass="failureNotification" ErrorMessage="Enter Math Baseline Heading 1" ToolTip="Math Baseline Heading 1" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblMathBaselineHeading2" runat="server" Text="Assessment H2-Math"></asp:Label>
                                        <asp:TextBox ID="txtMathBaselineHeading2" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ForeColor="red" runat="server" ControlToValidate="txtMathBaselineHeading2"
                                            CssClass="failureNotification" ErrorMessage="Enter Math Baseline Heading 2" ToolTip="Math Baseline Heading 2" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-3">
                                        <asp:Label ID="Label6" runat="server" Text="Assessment H3-Math"></asp:Label>
                                        <asp:TextBox ID="txtMathBaselineHeading3" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ForeColor="red" runat="server" ControlToValidate="txtMathBaselineHeading3"
                                            CssClass="failureNotification" ErrorMessage="Enter Math Baseline Heading 3" ToolTip="Math Baseline Heading 3" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-3">
                                        <asp:Label ID="Label12" runat="server" Text="Assessment H4-Math"></asp:Label>
                                        <asp:TextBox ID="txtMathBaselineHeading4" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" ForeColor="red" runat="server" ControlToValidate="txtMathBaselineHeading4"
                                            CssClass="failureNotification" ErrorMessage="Enter Math Baseline Heading 4" ToolTip="Math Baseline Heading 4" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                                <br />
                                <div class="row">

                                    <div class="col-lg-3">
                                        <asp:Label ID="lblHindiBaselineEndlineMaxScore" runat="server" Text="Hindi-H1 max Score"></asp:Label>
                                        <asp:TextBox ID="txtHindiBaselineEndlineMaxScore" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="red" runat="server" ControlToValidate="txtHindiBaselineEndlineMaxScore"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Baseline/Endline Max Score" ToolTip="Hindi Baseline/Endline Max Score" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblMathBaselineEndlineMaxScore" runat="server" Text="Math-H1 Max Score"></asp:Label>
                                        <asp:TextBox ID="txtMathBaselineEndlineMaxScore" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ForeColor="red" runat="server" ControlToValidate="txtMathBaselineEndlineMaxScore"
                                            CssClass="failureNotification" ErrorMessage="Enter Math Baseline/Endline Max Score" ToolTip="Math Baseline/Endline Max Score" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="Label3" runat="server" Text="Hindi-H2 max Score"></asp:Label>
                                        <asp:TextBox ID="txtHindiBaselineEndlineMaxScore1" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ForeColor="red" runat="server" ControlToValidate="txtHindiBaselineEndlineMaxScore1"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Baseline/Endline Max Score" ToolTip="Hindi Baseline/Endline Max Score" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="Label4" runat="server" Text="Math-H2 Max Score"></asp:Label>
                                        <asp:TextBox ID="txtMathBaselineEndlineMaxScore1" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ForeColor="red" runat="server" ControlToValidate="txtMathBaselineEndlineMaxScore1"
                                            CssClass="failureNotification" ErrorMessage="Enter Math Baseline/Endline Max Score" ToolTip="Math Baseline/Endline Max Score" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>

                                </div>

                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label ID="Label7" runat="server" Text="Hindi-H3 max Score"></asp:Label>
                                        <asp:TextBox ID="txtHindiBaselineEndlineMaxScore2" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ForeColor="red" runat="server" ControlToValidate="txtHindiBaselineEndlineMaxScore2"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Baseline/Endline Max Score" ToolTip="Hindi Baseline/Endline Max Score" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="Label8" runat="server" Text="Math-H3 Max Score"></asp:Label>
                                        <asp:TextBox ID="txtHindiBaselineEndlineMaxScore22" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ForeColor="red" runat="server" ControlToValidate="txtHindiBaselineEndlineMaxScore22"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Baseline/Endline Max Score" ToolTip="Hindi Baseline/Endline Max Score" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-3">
                                        <asp:Label ID="Label13" runat="server" Text="Hindi-H4 max Score"></asp:Label>
                                        <asp:TextBox ID="txtHindiBaselineEndlineMaxHinidiScore3" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" ForeColor="red" runat="server" ControlToValidate="txtHindiBaselineEndlineMaxHinidiScore3"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Baseline/Endline Max Score" ToolTip="Hindi Baseline/Endline Max Score" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-3">
                                        <asp:Label ID="Label14" runat="server" Text="Math-H4 Max Score"></asp:Label>
                                        <asp:TextBox ID="txtHindiBaselineEndlineMaxScore3" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" ForeColor="red" runat="server" ControlToValidate="txtHindiBaselineEndlineMaxScore22"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Baseline/Endline Max Score" ToolTip="Hindi Baseline/Endline Max Score" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblHindiBaselineEndlineHeading2Active" runat="server" Text="Hindi H2 Active"></asp:Label>
                                        <asp:TextBox ID="txtHindiBaselineEndlineHeading2Active" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ForeColor="red" runat="server" ControlToValidate="txtHindiBaselineEndlineHeading2Active"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Baseline/Endline Heading 2 Active" ToolTip="Hindi Baseline/Endline Heading 2 Active" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="Label1" runat="server" Text="Math H2 Active"></asp:Label>
                                        <asp:TextBox ID="txtMathBaselineEndlineHeading2Active" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ForeColor="red" runat="server" ControlToValidate="txtMathBaselineEndlineHeading2Active"
                                            CssClass="failureNotification" ErrorMessage="Enter Math Baseline/Endline Heading 2 Active" ToolTip="Math Baseline/Endline Heading 2 Active" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="col-lg-3">
                                        <asp:Label ID="Label9" runat="server" Text="Hindi H3 Active"></asp:Label>
                                        <asp:TextBox ID="txtHindiBaselineEndlineHeading3Active" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ForeColor="red" runat="server" ControlToValidate="txtHindiBaselineEndlineHeading3Active"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Baseline/Endline Heading 2 Active" ToolTip="Hindi Baseline/Endline Heading 3 Active" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="Label10" runat="server" Text="Math H3 Active"></asp:Label>
                                        <asp:TextBox ID="txtMathBaselineEndlineHeading3Active" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ForeColor="red" runat="server" ControlToValidate="txtMathBaselineEndlineHeading3Active"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Baseline/Endline Heading 2 Active" ToolTip="Math Baseline/Endline Heading 3 Active" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label ID="Label15" runat="server" Text="Hindi H4 Active"></asp:Label>
                                        <asp:TextBox ID="txtHindiBaselineEndlineHeading4Active" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="txtHindirrrrBaselineEndlineHeading4Active" ForeColor="red" runat="server" ControlToValidate="txtHindiBaselineEndlineHeading4Active"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Baseline/Endline Heading 2 Active" ToolTip="Hindi Baseline/Endline Heading 3 Active" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="Label16" runat="server" Text="Math H4 Active"></asp:Label>
                                        <asp:TextBox ID="txtMathBaselineEndlineHeading4Active" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this,event);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" ForeColor="red" runat="server" ControlToValidate="txtMathBaselineEndlineHeading4Active"
                                            CssClass="failureNotification" ErrorMessage="Enter Hindi Baseline/Endline Heading 2 Active" ToolTip="Math Baseline/Endline Heading 3 Active" ValidationGroup="Valid" Font-Size="Small" Font-Bold="true"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                                <div class="modal-footer" style="background-color: #ddd; padding: 10px;">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success btn-sm" OnClick="btnSave_Click" ValidationGroup="Valid" />&nbsp;
       <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-info btn-sm" OnClick="btnClear_Click" />&nbsp;
       <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" />
                                </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="GridLearningCampMaster" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="LinkddButton1" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

