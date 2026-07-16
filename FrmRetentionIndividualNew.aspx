<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FrmRetentionIndividualNew.aspx.cs" Culture="en-GB" Inherits="FrmRetentionIndividualNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .HeaderClassCsss {
            text-align: center !important;
            font-weight: normal !important;
            background-color: #9A9C9A !important;
        }

        .label1 {
            display: inline;
            margin-right: 1em;
        }

        .chandan_girpade td label {
            margin-right: 15px;
        }

        .chandan_girpade td input {
            margin-right: 5px;
            position: relative;
            top: 2px;
        }

        .padd {
            padding-left: 15px;
            padding-right: 15px;
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
    <script type="text/javascript" language="javascript">

        function enabledisablePresent(lnk) {
            debugger;
            var row = lnk.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            var HdnSearch = document.getElementById('<%=Hdnsearchall.ClientID %>').value;

            if (HdnSearch == "1") {

                var Present = row.cells[9].getElementsByTagName("select")[0].value;

                if (Present == "0") {
                    row.cells[10].getElementsByTagName("select")[0].value = "0";
                    row.cells[11].getElementsByTagName("select")[0].value = "0";
                }
                else if (Present == "1") {
                    row.cells[10].getElementsByTagName("select")[0].disabled = true;
                    row.cells[11].getElementsByTagName("select")[0].disabled = false;
                }
                else if (Present == "2") {
                    row.cells[10].getElementsByTagName("select")[0].disabled = false;
                    row.cells[11].getElementsByTagName("select")[0].disabled = true;
                }
            }
            else {
                var Present = row.cells[6].getElementsByTagName("select")[0].value;

                if (Present == "0") {
                    row.cells[7].getElementsByTagName("select")[0].value = "0";
                    row.cells[8].getElementsByTagName("select")[0].value = "0";
                }
                else if (Present == "1") {
                    row.cells[7].getElementsByTagName("select")[0].disabled = true;
                    row.cells[8].getElementsByTagName("select")[0].disabled = false;
                    row.cells[7].getElementsByTagName("select")[0].value = "0";
                }
                else if (Present == "2") {
                    row.cells[7].getElementsByTagName("select")[0].disabled = false;
                    row.cells[8].getElementsByTagName("select")[0].disabled = true;
                    row.cells[8].getElementsByTagName("select")[0].value = "0";
                }
            }
        }
        //---------------------//
        function enabledisableChildPrestent_Last2Week(lnk) {
            debugger;
            var row = lnk.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var HdnSearch = document.getElementById('<%=Hdnsearchall.ClientID %>').value;

            if (HdnSearch == "1") {
                var Prestent_Last2Week = row.cells[10].getElementsByTagName("select")[0].value;
                if (Prestent_Last2Week == "1") {
                    row.cells[11].getElementsByTagName("select")[0].disabled = false;
                }
                else if (Prestent_Last2Week == "2") {
                    row.cells[11].getElementsByTagName("select")[0].disabled = true;
                    row.cells[11].getElementsByTagName("select")[0].value = "0";
                }
                else if (Prestent_Last2Week == "0") {
                    row.cells[11].getElementsByTagName("select")[0].disabled = true;
                    row.cells[11].getElementsByTagName("select")[0].value = "0";
                }
            } else {
                var Prestent_Last2Week = row.cells[7].getElementsByTagName("select")[0].value;

                if (Prestent_Last2Week == "2") {
                    row.cells[8].getElementsByTagName("select")[0].disabled = true;
                    row.cells[8].getElementsByTagName("select")[0].value = "0";
                }
                else if (Prestent_Last2Week == "1") {
                    row.cells[8].getElementsByTagName("select")[0].disabled = false;
                }
            }
        }

        //-------------------------------------------------------------------//

        function enabledisableNameofChildAvailable(lnk) {

            var row = lnk.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var HdnSearch = document.getElementById('<%=Hdnsearchall.ClientID %>').value;
            var NameofChildAvailable = row.cells[6].getElementsByTagName("select")[0].value;
            if (HdnSearch == "1") {
                if (NameofChildAvailable == "10") {
                    row.cells[7].getElementsByTagName("select")[0].value = "0";
                    row.cells[8].getElementsByTagName("select")[0].value = "0";
                    row.cells[11].getElementsByTagName("select")[0].value = "0";

                } else if (NameofChildAvailable == "1") {
                    row.cells[7].getElementsByTagName("select")[0].disabled = false;
                    row.cells[8].getElementsByTagName("select")[0].disabled = true;
                    row.cells[8].getElementsByTagName("select")[0].value = "0";
                }
                else if (NameofChildAvailable == "2") {
                    row.cells[7].getElementsByTagName("select")[0].value = "0";
                    row.cells[7].getElementsByTagName("select")[0].disabled = true;
                    row.cells[8].getElementsByTagName("select")[0].disabled = false;
                    row.cells[11].getElementsByTagName("select")[0].value = "0";
                }
            }
            else {

                if (NameofChildAvailable == "1") {
                    row.cells[7].getElementsByTagName("select")[0].disabled = false;
                    row.cells[8].getElementsByTagName("select")[0].disabled = true;
                    row.cells[8].getElementsByTagName("select")[0].value = "0";

                }
                else if (NameofChildAvailable == "2") {

                    row.cells[7].getElementsByTagName("select")[0].value = "0";
                    row.cells[7].getElementsByTagName("select")[0].disabled = true;
                    row.cells[8].getElementsByTagName("select")[0].disabled = false;
                    row.cells[9].getElementsByTagName("select")[0].value = "0";
                    row.cells[9].getElementsByTagName("select")[0].disabled = true;
                }
            }
        }
        //--------------------------------------------------------------------------//
        function enabledisableSupportforChildRegularty(lnk) {
            debugger;
            var row = lnk.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var SupportforChildRegularty = row.cells[7].getElementsByTagName("select")[0].value;
            var HdnSearch = document.getElementById('<%=Hdnsearchall.ClientID %>').value;

            if (HdnSearch == "1") {
                if (SupportforChildRegularty > 0) {
                    row.cells[11].getElementsByTagName("select")[0].disabled = false;
                }
            }

            else {
                if (SupportforChildRegularty > 0) {
                    row.cells[9].getElementsByTagName("select")[0].disabled = false;
                }
            }
        }
        //-------------------------------------------------------------------------//
        function enabledisableReasonforchildnotinReg(lnk) {
            debugger;
            var row = lnk.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var ReasonforchildnotinReg = row.cells[8].getElementsByTagName("select")[0].value;
            var HdnSearch = document.getElementById('<%=Hdnsearchall.ClientID %>').value;

            if (HdnSearch == "1") {
                if (ReasonforchildnotinReg > 0) {
                    row.cells[11].getElementsByTagName("select")[0].disabled = true;
                    row.cells[11].getElementsByTagName("select")[0].value = "0";
                }
            }
            else {
                if (ReasonforchildnotinReg > 0) {
                    row.cells[10].getElementsByTagName("select")[0].disabled = true;
                    row.cells[10].getElementsByTagName("select")[0].value = "0";
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
                        <div class="panel panel-default" style="height: 573px;">
                            <div class="panel-heading" style="padding: 4px 10px 4px 15px;">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" style="padding-left: 0px;">
                                        <h3 class="text-danger" style="margin: 0px;">
                                            <asp:Label ID="lblMain" runat="server" Text="Retention Individual"></asp:Label>
                                        </h3>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">
                                        <input type="image" id="ton-new" class="butt" src="Images/search-not-29.png" title="Search" />
                                        <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right"
                                            ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px; padding: 0px;"
                                            runat="server" />
                                        <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                            ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnsave_Click" ValidationGroup="saves"
                                            Style="margin-right: 5px; padding: 0px;" runat="server" />
                                        <%--<asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                            Visible="false" ToolTip="Add" ImageUrl="~/images/add-29-1.png" Style="margin-right: 5px;
                                            padding: 0px;" runat="server" />--%>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="padding: 0px 10px 0px 10px;">
                                <div id="">
                                    <div class="row marg search-bg">
                                        <div class="form-horizontal">
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Year:
                                                    </label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                            runat="server" class="form-control ">
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
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        District:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                            AutoPostBack="true" class="form-control " />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Block:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                            class="form-control " />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Village:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlVillage" runat="server" AutoPostBack="true" class="form-control "
                                                            OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3  linhei" style="padding-top: 2px; padding-left: 15px; padding-right: 15px;">
                                                        School:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlSchool" runat="server" AutoPostBack="true" class="form-control "
                                                            OnSelectedIndexChanged="ddlschool_SelectedIndexChanged" />
                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                                                                ValidationGroup="saves" ControlToValidate="ddlSchool" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                          

                                             <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3  linhei" style="padding-top: 2px; padding-left: 15px; padding-right: 15px;">
                                                        School Status:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlMarge" runat="server" AutoPostBack="true" class="form-control "
                                                            OnSelectedIndexChanged="ddlschoolgg_SelectedIndexChanged" />
                                                          </span>
                                                    </div>
                                                </div>
                                            </div>
                                               <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server" id="divMar" visible="false">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3  linhei" style="padding-top: 2px; padding-left: 15px; padding-right: 15px;">
                                                        Marge School:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlSchoolMarger" OnSelectedIndexChanged="ddlschoolmm_SelectedIndexChanged"  runat="server" AutoPostBack="true" class="form-control "
                                                           />
                                                       
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-1 col-md-1 col-sm-1 cpl-xs-12 col-lg-offset-0 col-md-offset-0 col-sm-offset-0 col-xs-offset-0">
                                                <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" Visible="false" class="btn btn-danger btn-paddd pull-right"
                                                    ValidationGroup="saves" BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                                            </div>
                                            <%-- <div class="col-lg-1 col-md-1 col-sm-1 cpl-xs-12 col-lg-offset-0 col-md-offset-0 col-sm-offset-0 col-xs-offset-0">
                                                <asp:ImageButton ID="btnadd" ToolTip="Add" runat="server" class="btn btn-danger btn-paddd pull-right"
                                                    BackColor="#f1f1f1" OnClick="btnAdd_Click" ImageUrl="~/images/Add11.png" />
                                            </div>--%>
                                            <div class="col-lg-1 col-md-1 col-sm-1 cpl-xs-12 col-lg-offset-0 col-md-offset-0 col-sm-offset-0 col-xs-offset-0">
                                                <asp:ImageButton ID="ImgShow" ToolTip="Show" Visible="false" runat="server" class="btn btn-danger btn-paddd pull-right"
                                                    BackColor="#f1f1f1" OnClick="ImgShow_Click" ImageUrl="~/images/iconimage-128.png"
                                                    Height="34px" />
                                            </div>
                                            <div class="col-lg-1 col-md-1 col-sm-1 cpl-xs-12" style="padding: 0px;">
                                                <asp:Button ID="LnkDownloadPDF" Visible="false" runat="server" OnClick="LnkDownloadPDF_OnClick"
                                                    Text="Export To PDF" CssClass="btn-danger"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="div2" runat="server">
                                        <div class="row marg search-bg">
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label id="lblattendance" runat="server" class="col-sm-12 padd linhei" style="padding-top: 2px;">
                                                        Can you take data from attendance register
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group chandan_girpade">
                                                    <asp:RadioButtonList ID="ddlFCTakeDataAttendance" runat="server" OnSelectedIndexChanged="FCTakeDataAttendance_CheckedChanged"
                                                        Enabled="true" ForeColor="Black" RepeatDirection="Horizontal" AutoPostBack="true">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                       
                                                        
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label id="Lblreason" runat="server" class="col-sm-12 padd linhei" style="padding-top: 2px;">
                                                        Reason for not taking data from attendance register
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlreson" runat="server" class="form-control " OnSelectedIndexChanged="ddlreson_CheckedChanged"
                                                        AutoPostBack="true">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Attendance register not available " Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Attendance register not updated" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Teacher denies giving it to us" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label id="lblreason2" runat="server" class="col-sm-12 padd linhei" style="padding-top: 2px;">
                                                        Teacher giving you permission to you take data from attendance register
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <asp:RadioButtonList ID="ddlTeacherallow" runat="server" Enabled="true" OnSelectedIndexChanged="ddlTeacherallow_CheckedChanged"
                                                        AutoPostBack="true" ForeColor="Black" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Yes" Value="1" style="margin-inline: 7px"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div id="Div1" class="row" runat="server" visible="false">
                                    <div class="col-lg-12">
                                        <table class="table table-bordered">
                                            <tr>
                                                <td style="width: 30%">
                                                    <b>Total Children:</b>
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblTotalChildren" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td style="width: 20%">
                                                    <b>Entry Completed:</b>
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblEntryComplete" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 30%">
                                                    <asp:Button ID="btnDataApprove" runat="server" Text="Approve" CssClass="btn btn-success btn-sm"
                                                        Enabled="false" OnClick="btnDataApprove_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div >
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                        <div style="height: 310px; overflow: auto; width: 100%;" align="center">
                                            <asp:GridView ID="GV_Retention" Width="100%" AllowPaging="true" CssClass="table table-striped table-bordered table-hover"
                                                PageSize="70" DataKeyNames="Retention_ID,Uniquecode,SchoolCode,GradeResone,FCTakeDataAttendance,ReasonnotTakingData,Teacherallow,UniqueChildCode,NameofChildAvailable,SupportforChildRegularty,ReasonforchildnotinReg,IsChildAvailableClassToday,LastPresentDate,ChildPrestent_Last2Week,PresentClass,ImageName,ChildUniqueID,IsSRRight,NewSR"
                                                runat="server" AutoGenerateColumns="false" OnPageIndexChanging="GV_Retention_PageIndexChanging"
                                                OnRowDataBound="GV_Retention_RowDataBound">
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
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="UniqueID" HeaderStyle-CssClass="GridHeaderClass" HeaderText="UniqueID">
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="StudentName" HeaderStyle-CssClass="GridHeaderClass" HeaderText="Student Name">
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FathersName" HeaderStyle-CssClass="GridHeaderClass" HeaderText="Father Name">
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Class" HeaderStyle-CssClass="GridHeaderClass" HeaderText="Class">
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SRNO" HeaderStyle-CssClass="GridHeaderClass" Visible="false" HeaderText="SRNO">
                                                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LBLTempId" runat="server" Visible="true" Text='<%# Bind("ChildUniqueID") %>'> </asp:Label>
                                                             <asp:Label ID="lblClass" runat="server" Visible="true" Text='<%# Bind("Class") %>'> </asp:Label>
                                                            <asp:Label ID="LBLTempId1" runat="server" Visible="true">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="GridHeaderClass" HeaderText="Is Name of Child Available in Attendance Register 2026-2027?">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlNameofChildAvailable" runat="server" class="form-control"
                                                                OnSelectedIndexChanged="ddlAnnual444_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="--Select--" Value="0"> </asp:ListItem>
                                                                <asp:ListItem Text="Yes" Value="1"> </asp:ListItem>
                                                                <asp:ListItem Text="No" Value="2"> </asp:ListItem>
                                                                 <asp:ListItem Text="Yes, but with wrong SR Number" Value="3"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="GridHeaderClass" HeaderText="Is the child's SR number correct?">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlsr" runat="server" class="form-control"
                                                                OnSelectedIndexChanged="ddlAnnual4444_SelectedIndexChanged" Enabled="false"  AutoPostBack="true">
                                                                <asp:ListItem Text="--Select--" Value="0"> </asp:ListItem>
                                                                <asp:ListItem Text="Yes" Value="1"> </asp:ListItem>
                                                                <asp:ListItem Text="No" Value="2"> </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderStyle-CssClass="GridHeaderClass" HeaderText=" New SR No">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtSr" Enabled="false" runat="server" class="form-control"  ></asp:TextBox>                                                
                                                           
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="GridHeaderClass" HeaderText="Does the child need any support for regularization?">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlSupportforChildRegularty" runat="server" class="form-control"
                                                                OnSelectedIndexChanged="onselected_SupportforChildRegularty" AutoPostBack="true">
                                                                <asp:ListItem Text="--Select--" Value="0"> </asp:ListItem>
                                                                <asp:ListItem Text="Yes" Value="1"> </asp:ListItem>
                                                                <asp:ListItem Text="No" Value="2"> </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="GridHeaderClass" HeaderText="Select the reason for not getting the child in the register by checking it from the SR register">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlReasonforchildnotinReg"  AutoPostBack="true" OnSelectedIndexChanged="ddlReasonforchildnotinReg_SupportforChildRegularty"  runat="server" class="form-control"
                                                                >
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="GridHeaderClass" HeaderText="Dropout Reason">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlIsChildAvailableClassToday" runat="server" 
                                                                class="form-control">
                                                               
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="9%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="GridHeaderClass" HeaderText=" Has Child Come to School in the Last 2 Weeks?">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlChildPrestent_Last2Week" runat="server" onchange="return enabledisableChildPrestent_Last2Week(this);"
                                                                class="form-control">
                                                                <asp:ListItem Text="--Select--" Value="0"> </asp:ListItem>
                                                                <asp:ListItem Text="Yes" Value="1"> </asp:ListItem>
                                                                <asp:ListItem Text="No" Value="2"> </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="GridHeaderClass" HeaderText="Last Attendance Date ">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" OnClientDateSelectionChanged="arrivaldatecheck" ID="txtAttendanceLastdate" autocomplete="off" ondrop="return false;"
                                                                class="form-control" onkeypress="return false;" Text='<%# Bind("LastPresentDate") %>'></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="txtAttendanceLastdate" PopupPosition="BottomRight">
                                                            </asp:CalendarExtender>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="GridHeaderClass" HeaderText="Please enter the current class of the child">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlPresentClass"  AutoPostBack="true" OnSelectedIndexChanged="ddlPresentClass_SupportforChildRegularty"  runat="server" class="form-control">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderStyle-CssClass="GridHeaderClass" HeaderText="What is the reason for the child not progressing to the next grade">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlGradeResone" Enabled="false" runat="server" 
                                                                class="form-control">
                                                               
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="9%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div>
                                            <asp:ModalPopupExtender ID="Modalimages" runat="server" TargetControlID="hdn_images"
                                                PopupControlID="pnl_Images" CancelControlID="btn_cancelalertI" BackgroundCssClass="modalBackground">
                                            </asp:ModalPopupExtender>
                                            <asp:Panel ID="pnl_Images" runat="server" Style="display: none;" Width="623px" Height="470px"
                                                class="ModalPopup" BackColor="White" BorderColor="Black" BorderStyle="Ridge"
                                                BorderWidth="1">
                                                <div style="margin-bottom: 15px; background-color: #c4c4c4;" align="right">
                                                    <table>
                                                        <tr>
                                                            <td></td>
                                                            <td width="90px" align="right">
                                                                <asp:ImageButton ID="btn_cancelalertI" runat="server" ImageUrl="~/Images/close-29.png" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div style="width: 600px; height: 500px">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <div id="img1" runat="server" style="width: 290px; margin-left: 15px; height: 200px; border: 1px solid gray; float: left">
                                                                    <asp:Image ID="EduImg" runat="server" Height="198px" Width="288px" BorderColor="Black"
                                                                        BorderStyle="Ridge" BorderWidth="1px" />
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="width: 290px; margin-left: 10px; height: 200px; border: 1px solid gray; float: right">
                                                                    <asp:Image ID="EduImg2" runat="server" Height="198px" Width="288px" BorderColor="Black"
                                                                        BorderStyle="Ridge" BorderWidth="1px" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div style="width: 290px; margin-top: 12px; margin-left: 15px; height: 200px; border: 1px solid gray; float: left">
                                                                    <asp:Image ID="EduImg3" runat="server" Height="198px" Width="288px" BorderColor="Black"
                                                                        BorderStyle="Ridge" BorderWidth="1px" />
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="width: 290px; margin-top: 12px; margin-left: 10px; height: 200px; border: 1px solid gray; float: right">
                                                                    <asp:Image ID="EduImg4" runat="server" Height="198px" Width="288px" BorderColor="Black"
                                                                        BorderStyle="Ridge" BorderWidth="1px" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </asp:Panel>
                                            <asp:HiddenField ID="hdn_images" runat="server" />
                                            <asp:HiddenField ID="hdnMKID" runat="server" />
                                            <asp:HiddenField ID="hdnMKID2" runat="server" />
                                            <asp:HiddenField ID="hdnMKID3" runat="server" />
                                            <asp:HiddenField ID="hdnMKID4" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="Hdnsearchall" runat="server" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="LnkDownloadPDF" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
