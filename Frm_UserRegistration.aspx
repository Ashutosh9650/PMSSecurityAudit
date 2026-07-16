<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Frm_UserRegistration.aspx.cs" Culture="en-GB" Inherits="Frm_UserRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }


        .btn {
            display: inline-block;
            padding: 6px 3px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: normal;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
        }

        .brder {
            border: 1px solid transparent;
            border-color: #ddd;
            border-radius: 4px;
            box-shadow: 0 1px 1px rgba(0, 0, 0, .05);
        }
    </style>
    <script type="text/javascript">
        function raiseEvent() {
            __doPostBack('<%= btnTemp.UniqueID%>', '');

        }
    </script>
    <script type="text/javascript">
        function gettxt() {

            var psw = document.getElementById('<%=txtpw.ClientID %>');
            var repsw = document.getElementById('<%=txtcpassword.ClientID %>');

            if (repsw.value != psw.value) {

                alert("Mismatch in Confirm password. Please reenter.");
                psw.value = "";
                repsw.value = "";
                psw.focus();
                return false;
            }

        }
        function checksimilar() {
            var tpass = document.getElementById('<%=txtpw.ClientID %>').valuF;
            var tuserid = document.getElementById('<%=txtuname.ClientID %>').value;
            if (tpass != "" && tuserid != "") {
                if (tpass.toLowerCase() == tuserid.toLowerCase()) {

                    document.getElementById('<%=txtpw.ClientID %>').value;
                    document.getElementById('<%=txtuname.ClientID %>').value;
                    alert("User ID and Password cannot be similar");
                    return false;
                }
            }
        }
        function checkPwdKey(str) {
            var msg = "";


            if (str.match(/[\!\@\#\$\%\^\&\(\)\_\.\+]/)) {
                msg += 'Please do not use any special characters in your password';
            }

            if (msg != "") {
                document.getElementById('<%=txtpw.ClientID %>').value = "";
                alert(msg);
                return false;
            }
            else { return true; }
        }
        function checkPwd(str) {

            if (str != "") { checksimilar(); }
            var msg = "";
            if (str.match(/[\!\@\#\$\%\^\&\(\)\_\.\+]/)) {
                msg += 'Please do not use any special characters in your password';
            }
            else if (str.length < 8) {
                msg += 'Password Minimum 8 character'; //  for min length
            } else if (str.length > 20) {
                msg += 'Password Maximum 20 character'; //  for max length
            } else if (str.search(/\d/) == -1) {

                msg += '- Atleast one numeric in Password'; // for numeric
            } else if (str.search(/[a-z]/) == -1) {
                msg += '- Atleast one Lower case alphabet in Password'; // for character
            } else if (str.search(/[A-Z]/) == -1) {
                msg += '- Atleast one upper case alphabet in Password'; // for character
            }


            if (msg != "") {
                document.getElementById('<%=txtpw.ClientID %>').value = "";
                document.getElementById('<%=txtcpassword.ClientID %>').value = "";
                document.getElementById('<%=txtpw.ClientID %>').focus();
                alert(msg);
                return false;
            }
            else { return true; }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="padding: 0px;">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6">
                                    <h3 class="text-danger" style="margin: 5px 0px 0px 0px;">User Registration</h3>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6">


                                    <asp:ImageButton ID="btnSave" ValidationGroup="Valid" class="btn btn-primary pull-right" OnClick="btn_Save_Click"
                                        ToolTip="Save" runat="server" BackColor="#f5f5f5" ImageUrl="~/images/save-29-1.png" /></span>
                      <span style="float: right">

                          <asp:ImageButton ID="btnfp" class="btn btn-primary pull-right marg-butt"
                              runat="server" BackColor="#f5f5f5" ToolTip="Add" ImageUrl="~/images/add-29-1.png" OnClientClick="return raiseEvent();" />

                          <asp:Button ID="btnTemp" Visible="false" runat="server" OnClick="btn_Add_click" />
                          <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" Visible="false" class="btn btn-primary pull-right"
                              ToolTip="Save" runat="server" BackColor="#f5f5f5" ImageUrl="~/images/Delete-29.png" />
                          <asp:LinkButton ID="lnkActivate" Visible="false" OnClick="btnlnk_Click" runat="server">Activate User </asp:LinkButton>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="padding: 5px 0px 0px 0px;">

                            <div class="row">

                                <div id="Div4" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                    <div class="form-group">
                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                            Type:</label>
                                        <div class="col-sm-8 padd">
                                            <asp:DropDownList ID="ddlType" runat="server" class="form-control ">
                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                <asp:ListItem Value="1">Employee Code </asp:ListItem>
                                                <asp:ListItem Value="2">Name </asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                    <div class="form-group">
                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                            Name/UserID:
                                        </label>
                                        <div class="col-sm-8 padd">
                                            <asp:TextBox ID="txtSearchUser" runat="server" class="form-control ">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-1 col-md-1 col-sm-1 cpl-xs-12 col-lg-offset-0 col-md-offset-0 col-sm-offset-0 col-xs-offset-0" style="margin-top: -6px;">
                                    <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-left" ValidationGroup="saves"
                                        BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />

                                </div>
                            </div>


                        </div>
                    </div>
                </div>

            </div>


            <div class="row brder" style="padding-top: 15px; margin: 0px 15px 15px 15px; height: 670px;">
                <div class="col-lg-4 col-md-3 col-sm-4 col-xs-12">
                <div style="height: 470px; overflow: auto">
                    <asp:GridView ID="dgvleftgrid" Width="100%" runat="server" OnPageIndexChanging="GV_Project_PageIndexChanging" AllowPaging="true" PageSize="25" BorderStyle="None" GridLines="None" AutoGenerateColumns="false" OnRowCommand="dgvleftgrid_rowcommand" DataKeyNames="UserID,ActiveStatus">
                        <EmptyDataTemplate>
                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                Data not found
                            </div>
                        </EmptyDataTemplate>
                        <FooterStyle CssClass="FooterStyle" />
                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="44px" />
                        <RowStyle HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                        <AlternatingRowStyle BackColor="#f1f1f1" />
                        <PagerStyle CssClass="paging" HorizontalAlign="Center" />
                        <Columns>

                            <asp:ButtonField DataTextField="FristName" ItemStyle-ForeColor="#333"
                                HeaderText="Name" Text="Button" CommandName="Show">
                                <ItemStyle CssClass="padding-lef" Height="30px" />
                                <HeaderStyle CssClass="padding-lef" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="UserID" HeaderText="UserID" Visible="False"></asp:BoundField>

                            <asp:ButtonField DataTextField="UserName" ItemStyle-ForeColor="#333"
                                HeaderText="User ID" Text="Button" CommandName="Show">
                                <ItemStyle CssClass="padding-lef" Height="30px" />
                                <HeaderStyle CssClass="padding-lef" />
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="Reset" Visible="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblCategory" OnClick="OOD2Dtargetmet_Click" runat="server">Reset</asp:LinkButton>

                                    <asp:Label ID="lblUser" runat="server" Visible="false" Text='<%# Eval("UserName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="padding-lef" Height="30px" />
                            </asp:TemplateField>



                        </Columns>
                        <PagerSettings Position="Bottom" />

                    </asp:GridView>
                </div>
                </div>

                <div class="col-lg-5 col-md-6 col-sm-8 col-xs-12 brder">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                            <div class="form-horizontal">

                                <div id="Div2" runat="server" class="row">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name">Type <span class="req">*</span> </label>

                                        <div class="col-sm-8 radio">
                                            <span style="padding-left: 15px;">
                                                <asp:RadioButton ID="rblInternal" OnCheckedChanged="rblInternal_CheckedChanged" Checked="true" Text=" Internal"
                                                    GroupName="YB" runat="server" AutoPostBack="true" /></span>
                                            <span style="margin-left: 0px; padding-left: 29px;">
                                                <asp:RadioButton ID="rblExternal" OnCheckedChanged="rblExternal_CheckedChanged" runat="server" AutoPostBack="true" GroupName="YB"
                                                    Text="External" /></span>

                                        </div>
                                    </div>
                                </div>

                                <div id="Div1" runat="server" class="row">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name">Valid for <span class="req">*</span> </label>

                                        <div class="col-sm-8 radio">
                                            <span style="padding-left: 15px;">
                                                <asp:CheckBox ID="chkOnline" Checked="true" Text="Online" runat="server" />
                                            </span>
                                            <span style="margin-left: 0px; padding-left: 29px;">
                                                <asp:CheckBox ID="chkOffline" runat="server" Text="Offline" /></span>

                                        </div>
                                    </div>
                                </div>
                                <div id="a" runat="server" class="row">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name">User Level  <span class="req">*</span> </label>

                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddllevel" CssClass="form-control" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddllevel_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <span class="reqfield" style="margin: -27px -15px;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel" InitialValue="0" runat="server"
                                                    ControlToValidate="ddllevel" ErrorMessage="*" ForeColor="Red"
                                                    ValidationGroup="Valid">

                                                </asp:RequiredFieldValidator></span>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" id="demp" runat="server">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name" id="Label1" runat="server">Employee  <span class="req">*</span></label>

                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlemployee" AutoPostBack="true" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <%--             <span class="reqfield">
                               <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp" runat="server" 
                        ControlToValidate="ddlemployee" Display="None" ErrorMessage="*" ForeColor="Red"
                        ValidationGroup="Valid">

                        </asp:RequiredFieldValidator></span>--%>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name">User ID  <span class="req">*</span></label>

                                        <div class="col-sm-8" style="float: right">

                                            <asp:TextBox ID="txtuname" OnTextChanged="Txtuser_TextChanged" CssClass="form-control" runat="server" AutoPostBack="True"></asp:TextBox>
                                            <%--             <span class="reqfield">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatoruname" runat="server" 
                        ControlToValidate="txtuname" Display="None" ErrorMessage="*" ForeColor="Red"
                        ValidationGroup="Valid">

                        </asp:RequiredFieldValidator></span>--%>
                                        </div>
                                    </div>
                                </div>


                                <div class="row" runat="server" id="IM">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name">IMEINo  <span class="req">*</span></label>

                                        <div class="col-sm-8" style="float: right">

                                            <asp:TextBox ID="txtImi" CssClass="form-control" runat="server"></asp:TextBox>
                                            <%--   <span class="reqfield"  style="margin: -27px -15px;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatoruname" runat="server" 
                        ControlToValidate="txtImi"  ErrorMessage="*" ForeColor="Red"
                        ValidationGroup="Valid">

                        </asp:RequiredFieldValidator></span>--%>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" runat="server" id="Div3">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name">AndroidID  </label>

                                        <div class="col-sm-8" style="float: right">

                                            <asp:TextBox ID="txtAndroidID" CssClass="form-control" runat="server"></asp:TextBox>
                                            <%--   <span class="reqfield"  style="margin: -27px -15px;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatoruname" runat="server" 
                        ControlToValidate="txtImi"  ErrorMessage="*" ForeColor="Red"
                        ValidationGroup="Valid">

                        </asp:RequiredFieldValidator></span>--%>
                                        </div>
                                    </div>
                                </div>



                                <div class="row">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name" style="padding-top: 14px">User Name  <span class="req">*</span></label>

                                        <div class="col-sm-8">
                                            <%--   <asp:TextBox ID="txtpw" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtFristName" onkeypress="return onlyAlphabets(event,this);" CssClass="form-control" MaxLength="30" runat="server" AutoComplete="off"></asp:TextBox>
                                            <span class="reqfield" style="margin: -27px -15px;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtFristName" ErrorMessage="*"
                                                    ValidationGroup="Valid">

                                                </asp:RequiredFieldValidator></span>


                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name" style="padding-top: 14px">Password  <span class="req">*</span></label>

                                        <div class="col-sm-8">
                                            <%--   <asp:TextBox ID="txtpw" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtpw" CssClass="form-control" runat="server" AutoComplete="off"
                                                MaxLength="15" onkeypress="return checkPwdKey(this.value);" onchange="checkPwd(this.value);"
                                                TextMode="Password"
                                                ToolTip="Password must be 8 charecter with- 1-upper case letter, 1-lower case letter and 1-digit"></asp:TextBox>
                                            <%--    <span class="reqfield" style="margin: -27px -15px;">
                    <%--<asp:RequiredFieldValidator ID="Req22" runat="server" 
                        ControlToValidate="txtpw" ErrorMessage="Enter password" 
                        ValidationGroup="Valid">

                        </asp:RequiredFieldValidator></span>--%>
                                            <asp:FilteredTextBoxExtender ID="FilterTxtpassword" runat="server" Enabled="True" TargetControlID="txtpw"
                                                ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789[\!\@\#\$\%\^\&\(\)">
                                            </asp:FilteredTextBoxExtender>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name" style="padding-top: 14px">Confirm Password  <span class="req">*</span> </label>

                                        <div class="col-sm-8">


                                            <asp:TextBox ID="txtcpassword" CssClass="form-control" runat="server"
                                                ToolTip="Password must be 8 charecter with- 1-upper case letter, 1-lower case letter and 1-digit"
                                                TextMode="Password" onchange="return gettxt(this.value);" AutoComplete="off"
                                                MaxLength="15"></asp:TextBox>
                                            <span class="reqfield">
                                                <%--    <asp:RequiredFieldValidator ID="Req23" runat="server" 
                        ControlToValidate="txtcpassword" Display="None" ErrorMessage="Enter confirm password " 
                        ValidationGroup="Valid">

                        </asp:RequiredFieldValidator></span>--%>
                                                <asp:FilteredTextBoxExtender ID="FilteredTxtCPassword" runat="server" Enabled="True" TargetControlID="txtcpassword"
                                                    ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789">
                                                </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <%--      <asp:UpdatePanel UpdateMode="Conditional" runat="server" ID="Ups1">
                                    <ContentTemplate>--%>


                                <div class="row" id="b" runat="server">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name" id="lblstate" runat="server">State  <span class="req">*</span> </label>

                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlstate" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_selectindexchnaged" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <span class="reqfield" style="margin: -27px -15px;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorstate" runat="server"
                                                    ControlToValidate="ddlstate" InitialValue="0" ErrorMessage="*" ForeColor="Red"
                                                    ValidationGroup="Valid">

                                                </asp:RequiredFieldValidator></span>
                                        </div>
                                    </div>

                                </div>

                                <div class="row" id="M1" runat="server">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name" id="Label3" runat="server">State  <span class="req">*</span> </label>

                                        <div class="col-sm-8">
                                            <asp:ListBox ID="lstState" AutoPostBack="true" SelectionMode="Multiple" OnSelectedIndexChanged="lstState_selectindexchnaged" CssClass="form-control" runat="server"></asp:ListBox>

                                        </div>
                                    </div>

                                </div>

                                <div id="c" runat="server" class="row">

                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name" id="lbldistrict" runat="server">District  <span class="req">*</span> </label>

                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddldistrict" AutoPostBack="true" OnSelectedIndexChanged="ddldistrict_selectindexchnaged" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <span class="reqfield" style="margin: -27px -15px;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatordist" runat="server"
                                                    ControlToValidate="ddldistrict" InitialValue="0" ErrorMessage="*" ForeColor="Red"
                                                    ValidationGroup="Valid">

                                                </asp:RequiredFieldValidator></span>
                                        </div>
                                    </div>
                                </div>


                                <div class="row" id="M2" runat="server">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name" id="Label4" runat="server">District  <span class="req">*</span> </label>

                                        <div class="col-sm-8">
                                            <asp:ListBox ID="lstDistrict" AutoPostBack="true" SelectionMode="Multiple" CssClass="form-control" runat="server"></asp:ListBox>



                                        </div>
                                    </div>

                                </div>
                                <div id="d" runat="server" class="row">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name" id="lblblock" runat="server">Block  <span class="req">*</span> </label>

                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlblbock" AutoPostBack="true" OnSelectedIndexChanged="ddlblock_selectindexchnaged" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <span class="reqfield" style="margin: -27px -15px;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorblock" runat="server"
                                                    ControlToValidate="ddlblbock" InitialValue="0" ErrorMessage="*" ForeColor="Red"
                                                    ValidationGroup="Valid">

                                                </asp:RequiredFieldValidator></span>
                                        </div>
                                    </div>
                                </div>

                                <div id="ec" runat="server" class="row">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name" id="Label2" runat="server">Cluster  <span class="req">*</span> </label>

                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlCluster" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <span class="reqfield" style="margin: -27px -15px;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="ddlCluster" InitialValue="0" ErrorMessage="*" ForeColor="Red"
                                                    ValidationGroup="Valid">

                                                </asp:RequiredFieldValidator></span>
                                        </div>
                                    </div>
                                </div>

                                <div id="divBas" runat="server" visible="false" class="row">

                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name" id="Label5" runat="server">Base District  <span class="req">*</span> </label>

                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlBaseDist" CssClass="form-control" runat="server">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>

                                <%--  </ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="col-lg-3 col-md-3 col-sm-4 col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <p>
                                <b style="color: Red;">Password Instructions -
                                </b>



                                <p style="padding-left: 30px">
                                    <b>1.&nbsp; </b>Password must be 8 character
                                    <br />
                                    <br />
                                    <b>2.&nbsp; </b>Atleast one Lower case letter
                                    <br />
                                    <br />
                                    <b>3.&nbsp; </b>Atleast one Upper case letter
                                    <br />
                                    <br />
                                    <b>4.&nbsp; </b>Atleast one numeric
                                    <br />
                                    <br />
                                </p>

                            </p>
                        </div>
                    </div>
                </div>
            </div>



            <!-- Modal -->
            <div id="myModal" class="modal fade" role="dialog" runat="server">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Alert!</h4>
                        </div>
                        <div class="modal-body">
                            <p>
                                <asp:Label ID="lbl_message" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

