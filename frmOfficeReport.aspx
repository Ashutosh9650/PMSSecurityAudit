<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Culture="en-GB"
    CodeFile="frmOfficeReport.aspx.cs" Inherits="frmOfficeReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
        }

        .modalpopupcss {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }

        .modalPopup {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
            z-index: 999999;
        }
    </style>
    <script type="text/javascript">
        function arrivaldatecheck(sender, args) {
            var depdate = 'dep';

            var departuredate = $('.' + depdate).val();
            var arrivaldate = sender._selectedDate;
            var today = new Date();




            if (sender._selectedDate > today) {
                alert("Should not be future date.");
                sender._textbox.set_Value("")

                return false;

            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="row marg search-bg">
                        <div class="form-horizontal">
                            <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                <div class="form-group" style="margin-bottom: 7px;">
                                    <label for="email" class="col-sm-3 padd linhei">
                                        FC:</label>
                                    <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlUser" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged"
                                            runat="server" AutoPostBack="true" class="form-control ">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                <div class="form-group" style="margin-bottom: 7px;">
                                    <label for="email" class="col-sm-3 padd linhei">
                                        Village:</label>
                                    <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlVilage" OnSelectedIndexChanged="ddlVilage_SelectedIndexChanged"
                                            runat="server" AutoPostBack="true" class="form-control " />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                <div class="form-group" style="margin-bottom: 7px;">
                                    <label for="email" class="col-sm-3 padd linhei">
                                        Date:</label>
                                    <div class="col-sm-9 padd">
                                        <asp:TextBox runat="server" ID="txtDate" autocomplete="off" ondrop="return false;"
                                            class="form-control" onkeypress="return false;"></asp:TextBox>
                                        <ajax:CalendarExtender ID="CalendarExtenderTourdate" OnClientDateSelectionChanged="arrivaldatecheck"
                                            runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight">
                                        </ajax:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="ReqTxtDate" runat="server" ControlToValidate="txtDate"
                                            Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                            SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
                                        <span id="ctl00_MainContent_ReqTxtDate" style="color: Red; font-size: 9px; font-weight: normal; display: none;">*</span>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-6 col-md-6 col-sm-6 cpl-xs-12">
                                <asp:Button ID="btnApprove" CssClass="btn btn-success pull-right "
                                    ToolTip="Save" Text="  Back" OnClick="btnApprove_Click"
                                    Style="margin-right: 5px;" runat="server" />
                                <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right" ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px; padding: 0px;" runat="server" />

                                <asp:Button ID="btnAddVillage" Visible="false" CssClass="btn btn-success pull-right "
                                    ToolTip="Save" Text="Add Village" OnClick="btnAddVillage_Click"
                                    Style="margin-right: 5px; padding: 0px;" runat="server" />

                                <asp:ImageButton ID="Btnsave" CssClass="btn btn-info pull-right" OnClick="BtnSave_Click"
                                    BackColor="#f5f5f5" ToolTip="Save" ImageUrl="~/images/save-29-1.png"
                                    Style="margin-right: 5px; padding: 0px;" runat="server" />
                                <asp:ImageButton ID="btnSerach" OnClick="btnSerach_Click" ToolTip="Serach" runat="server"
                                    class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />
                                <asp:ImageButton ID="btnEdit" ToolTip="Edit" Style="margin-top: 5px;" OnClick="btnEdit_Click" runat="server" class="btn btn-danger btn-paddd pull-right"
                                    BackColor="#f1f1f1" ImageUrl="~/images/edit.png" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel ID="pnlMain" runat="server">
                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                <span class="">
                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                        Meetings
                                    </p>
                                </span>
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="Div5" style="padding: 0px;">
                            <div class="thumbnail" style="overflow: auto; height: 153px">
                                <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                    <asp:CheckBox ID="chkMeetings" runat="server" />
                                                    Meetings
                                                </p>
                                            </td>
                                            <td>

                                                <asp:CheckBox ID="rblMeetingsFC" Style="margin-left: 0px;"
                                                    CssClass="radio" runat="server" />
                                                F.C.
                                            </td>
                                        </tr>
                                        <%--
                        <tr style="text-align:left">
                           
                            <td>
                                <asp:RadioButton ID="rblBalsabaTB" Checked="true" style="margin-left: 19px;" GroupName="TestBal" CssClass="radio" runat="server" />
                                T.B.
                            </td>
                            <td>
                                 <asp:RadioButton ID="rblBalsabaFC"  style="margin-left: 19px;" GroupName="TestBal" CssClass="radio" runat="server" />
                               F.C.
                            </td>
                        </tr>--%>
                                        <tr style="text-align: left">
                                            <td>
                                                <asp:CheckBox ID="chk_FC_For" runat="server" />
                                                <label for="ctl00_MainContent_chkbalsabha">
                                                    &nbsp;F.C.</label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chk_BO" runat="server" />
                                                <label for="ctl00_MainContent_CheckBox28">
                                                    &nbsp;B.O.</label>
                                            </td>
                                        </tr>
                                        <tr style="text-align: left">
                                            <td>
                                                <asp:CheckBox ID="chk_Goverment" runat="server" />
                                                <label for="ctl00_MainContent_CheckBox30">
                                                    &nbsp;Goverment</label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chk_Other" runat="server" OnCheckedChanged="chk_Other_Click" AutoPostBack="true" />
                                                <label for="ctl00_MainContent_CheckBox32">
                                                    &nbsp;Other(Description)</label>
                                            </td>
                                        </tr>

                                        <tr style="text-align: left">
                                            <td>
                                                <asp:TextBox ID="txtTraingOtherDec" Visible="false" runat="server"></asp:TextBox>
                                            </td>
                                            <td>

                                                <%-- <asp:CheckBox ID="CheckBox5" runat="server" />
                                                <label for="ctl00_MainContent_CheckBox32">
                                                    &nbsp;Other(Description)</label>--%>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" style="padding: 0px;">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                <span class="">
                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                        Meetings
                                    </p>
                                </span>
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="Div1" style="padding: 0px;">
                            <div class="thumbnail" style="overflow: auto; height: 153px">
                                <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                    <asp:CheckBox ID="Chk_Training" runat="server" />
                                                    Training
                                                </p>
                                            </td>
                                            <td>

                                                <asp:CheckBox ID="rdTrainingFC" Style="margin-left: 0px;"
                                                    CssClass="radio" runat="server" />
                                                F.C.
                                            </td>
                                        </tr>
                                        <%--
                        <tr style="text-align:left">
                           
                            <td>
                                <asp:RadioButton ID="rblBalsabaTB" Checked="true" style="margin-left: 19px;" GroupName="TestBal" CssClass="radio" runat="server" />
                                T.B.
                            </td>
                            <td>
                                 <asp:RadioButton ID="rblBalsabaFC"  style="margin-left: 19px;" GroupName="TestBal" CssClass="radio" runat="server" />
                               F.C.
                            </td>
                        </tr>--%>
                                        <tr style="text-align: left">
                                            <td>
                                                <asp:CheckBox ID="CHkTB" runat="server" />
                                                <label for="ctl00_MainContent_chkbalsabha">
                                                    &nbsp;Training Team Balika</label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ChkStaffTraining" runat="server" />
                                                <label for="ctl00_MainContent_CheckBox28">
                                                    &nbsp;Staff Training</label>
                                            </td>
                                        </tr>
                                        <tr style="text-align: left">
                                            <td>
                                                <asp:CheckBox ID="Chk_Other_Training" OnCheckedChanged="ccCritica_CheckedChanged" AutoPostBack="true" runat="server" />
                                                <label for="ctl00_MainContent_CheckBox30">
                                                    &nbsp;Other(Description)</label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTraingOther" Visible="false" runat="server"></asp:TextBox>
                                                <%-- <asp:CheckBox ID="CheckBox5" runat="server" />
                                                <label for="ctl00_MainContent_CheckBox32">
                                                    &nbsp;Other(Description)</label>--%>
                                            </td>
                                        </tr>


                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                <span class="">
                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                        Meetings
                                    </p>
                                </span>
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="Div2" style="padding: 0px;">
                            <div class="thumbnail" style="overflow: auto; height: 153px">
                                <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                    <asp:CheckBox ID="chk_Other_Desc" runat="server" />
                                                    Other(Description)
                                                </p>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_OtherDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>




            <cc1:ModalPopupExtender ID="MpexdrDistrict" runat="server" BackgroundCssClass="modalBg "
                CancelControlID="CancelButton" PopupControlID="PnlDistrict" TargetControlID="HdnFild">
            </cc1:ModalPopupExtender>
            <asp:HiddenField ID="HdnFild" runat="server"></asp:HiddenField>

            <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important; margin-top: 162.5px !important" ID="PnlDistrict" runat="server">

                <div style="width: 100%; height: auto; background-color: #f1f1f1">
                    <div class="modal-header" style="background-color: #3ac0f2; color: White;">
                        <asp:Button ID="ImageButton1" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                            ToolTip="Add" Text="View" OnClick="btnView_Click" Style="margin-right: 5px; ol
                            padding: 0px;"
                            runat="server" />
                        <h4 class="modal-title" style="forecolor: White">Add Village</h4>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                        <div class="form-horizontal" role="form">
                            <asp:Panel ID="pnlView" runat="server">
                                <div class="form-group" id="statediv" runat="server">

                                    <asp:Label ID="Label10" class="control-label col-sm-4 lab-text-left" runat="server" Text="State:"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                            AutoPostBack="true" CssClass="form-control"
                                            Font-Names="Verdana" Font-Size="11px">
                                        </asp:DropDownList>
                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="0" runat="server"
                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlState" ErrorMessage="*"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>

                                    </div>
                                </div>





                                <div class="form-group">

                                    <asp:Label ID="Label11" class="control-label col-sm-4 lab-text-left" runat="server" Text="District:"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlDistrict" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                            AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                            Font-Size="11px">
                                        </asp:DropDownList>
                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0" runat="server"
                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlDistrict" ErrorMessage="*"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                </div>

                                <div class="form-group">

                                    <asp:Label ID="Label12" class="control-label col-sm-4 lab-text-left" runat="server" Text="Block"></asp:Label>
                                    <div class="col-sm-6 ">
                                        <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" CssClass="form-control"
                                            Font-Names="Verdana" Font-Size="11px">
                                        </asp:DropDownList>
                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" runat="server"
                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlBlock" ErrorMessage="*"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                </div>

                                <div class="form-group">

                                    <asp:Label ID="Label13" class="control-label col-sm-4 lab-text-left" runat="server" Text="Panchayat:"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged" CssClass="form-control"
                                            Font-Names="Verdana" Font-Size="11px">
                                        </asp:DropDownList>
                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" runat="server"
                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlPanchayat" ErrorMessage="*"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>

                                    </div>
                                </div>

                                <div class="form-group">

                                    <asp:Label ID="lblRseti" class="control-label col-sm-4 lab-text-left" runat="server" Text="Village"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlAddVillage" runat="server" CssClass="form-control"
                                            Font-Names="Verdana" Font-Size="11px">
                                        </asp:DropDownList>
                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server"
                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlAddVillage" ErrorMessage="*"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>

                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlGridView" Visible="false" runat="server">

                                <asp:GridView ID="gvVillage" runat="server" AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
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
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="15%" HeaderStyle-CssClass="GridHeaderClass">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgAcc" runat="server" OnClick="btn_Delete_Click" ImageUrl="~/images/delete-29.png"
                                                    Width="15px" Height="15px"></asp:ImageButton>
                                                <asp:Label ID="lblCUniqueChildCode" Visible="false" BackColor="Transparent" runat="server" Text='<%# Bind("NewVillageCode") %>' CssClass="form-controlAbhi"></asp:Label>
                                                <asp:Label ID="lblUserId" Visible="false" BackColor="Transparent" runat="server" Text='<%# Bind("UserId") %>' CssClass="form-controlAbhi"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="District Name" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDistrictName" class="labelGrid" ForeColor="Black" runat="server"
                                                    Text='<%# Eval("DistrictName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Block Name" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBlockName" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("BlockName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Panchayat Name" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPanchayatName" class="labelGrid" ForeColor="Black" runat="server"
                                                    Text='<%# Eval("PanchayatName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Village Code" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVis9ame" class="labelGrid" ForeColor="Black" runat="server"
                                                    Text='<%# Eval("VillageCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Village Name" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVillageN9ame" class="labelGrid" ForeColor="Black" runat="server"
                                                    Text='<%# Eval("VillageName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>






                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>


                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnNewUserSave" runat="server" CssClass="btn bgm-cyan" OnClick="btnNewUserSave_Click" ValidationGroup="saves"
                            Text="Save" ToolTip="Save" Style="float: none;"></asp:Button>&nbsp;
                            <asp:Button ID="CancelButton" runat="server" CssClass="btn bgm-cyan" Text="Close"
                                ToolTip="Close" Style="float: none;"></asp:Button>
                    </div>
                </div>


            </asp:Panel>



        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAddVillage" />

        </Triggers>
    </asp:UpdatePanel>

    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Hdn_model4"
        PopupControlID="pnlpopup4" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="Hdn_model4" runat="server" />
    <asp:Panel ID="pnlpopup4" runat="server" Style="display: none;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="height: 0px;">
                    <asp:ImageButton ID="ImageButton8" CssClass="btn btn-info pull-right" OnClick="btnReset_Click" BackColor="#f5f5f5"
                        ToolTip="Add" ImageUrl="~/images/close-29.png" Style="margin-right: 5px; padding: 0px;"
                        runat="server" />
                    <h4 class="modal-title">Remarks</h4>

                </div>

                <div class="row">

                    <div class="row marg search-bg">

                        <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                            <div class="col-lg-8 col-md-8  col-sm-8 cpl-xs-12">
                                <div class="form-group" style="margin-bottom: 2px;">
                                    <label for="email" class="col-sm-3 padd linhei">
                                        Remarks:</label>
                                    <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlRemark" runat="server" class="form-control">
                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Format not available</asp:ListItem>
                                            <asp:ListItem Value="2">Wrongly activity selected </asp:ListItem>
                                            <asp:ListItem Value="3">Typing error</asp:ListItem>

                                            <asp:ListItem Value="4">Counting error</asp:ListItem>
                                            <asp:ListItem Value="5">C Phone not available</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                        </div>


                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>



</asp:Content>
