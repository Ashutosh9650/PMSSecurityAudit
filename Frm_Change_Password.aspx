<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Frm_Change_Password.aspx.cs" Inherits="Frm_Change_Password" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">

        function gettxt() {

            var psw = document.getElementById('<%=Txtpasswordnew.ClientID %>');
            var repsw = document.getElementById('<%=TxtPasswordconfirm.ClientID %>');
            if (repsw.value != "" && psw.value != "") {
                if (repsw.value != psw.value) {
                    alert("Password not matched.");
                    document.getElementById('<%=TxtPasswordconfirm.ClientID %>').value = "";
                    document.getElementById('<%=TxtPasswordconfirm.ClientID %>').focus();
                    return false;
                }
            }
            return false;

        }
        function resetall() {

            document.getElementById('<%=Txtpasswordnew.ClientID %>').value = "";
            document.getElementById('<%=TxtPasswordconfirm.ClientID %>').value = "";
            document.getElementById('<%=Txtpassword.ClientID %>').value = "";
        // document.getElementById('<%=Txtuser.ClientID %>').value = "";        
            return false;

        }

        function checkcharlength() {
            var pvalue = document.getElementById('<%=Txtpasswordnew.ClientID %>').value;
            if (pvalue.length < 8) {
                document.getElementById('<%=Txtpasswordnew.ClientID %>').value = "";
                $("#Txtpasswordnew").val() = "";
                alert("Password must be 8 digit");
                return false;
            }
        }
        function checksamilartypsd() {
            var oldvalue = document.getElementById('<%=Txtpassword.ClientID %>').value;
            var newvalue = document.getElementById('<%=Txtpasswordnew.ClientID %>').value;
            if (oldvalue != "" && newvalue != "") {
                var newp = newvalue.toLowerCase();
                var oldp = oldvalue.toLowerCase();
                if (newp == oldp) {
                    document.getElementById('<%=Txtpasswordnew.ClientID %>').value = "";
                    alert("Similar Password ! Please try another");
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
                document.getElementById('<%=Txtpasswordnew.ClientID %>').value = "";
                alert(msg);
                return false;
            }
            else { return true; }
        }
        function checkPwd(str) {
            //alert("enter");
            gettxt();
            debugger;

            var msg = "";

            if (str.match(/[\!\@\#\$\%\^\&\(\)\_\.\+]/)) {
                msg += 'Please do not use any special characters in your password';
            }
            if (str.length < 8) {
                msg += 'Minimum 8 character'; //  for min length
            } else if (str.length > 20) {
                msg += 'Maximum 20 character'; //  for max length
            } else if (str.search(/\d/) == -1) {
                msg += '- Atleast one numeric'; // for numeric
            } else if (str.search(/[A-Z]/) == -1) {
                msg += '- Atleast one Upper case'; // for character
            } else if (str.search(/[a-z]/) == -1) {
                msg += '- Atleast one lower case'; // for character
            }
            //  else if (str.search(/[\!\@\#\$\%\^\&\(\)\_\.\+]/) == -1) {
            //      msg += '- Atleast one special character'; // for character
            //  } ]else if (str.search(/[^a-zA-Z0-9\!\@\#\$\%\^\&\(\)\_\.\+]/) != -1) {
            //     msg+='- Password invalid ! Atleast one special character also';// for special character;
            //}
            else if (str.search(/[^a-zA-Z0-9]/) != -1) {
                msg += '- Password invalid ! Please Check.'; // for special character;
            }
            checksamilartypsd();
            if (msg != "") {
                document.getElementById('<%=Txtpasswordnew.ClientID %>').value = "";
                alert(msg);
                return false;
            }
            else { return true; }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="background-color: #edecec;">
        <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <div class="panel panel-default" style="margin-top:11px">
                    <div class="panel-heading header-p" style="padding: 5px 0px 5px 0px">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                <h4 class="text-danger" style="color: Black !important">Change Password</h4>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">
                                <asp:ImageButton ID="Button1" runat="server" CssClass="btn btn-default bgm-cyan pull-right" ImageUrl="~/images/save-29-1.png" OnClick="btn_Save_Click" />
                            </div>
                        </div>

                    </div>
                    <div class="panel-body">

                        <label class="control-label">User Name:</label>
                        <div class="input-group taxt-marging" style="width: 60%;">
                            <span class="input-group-addon user-icon-bg">
                                <i class="glyphicon glyphicon-user"></i>
                            </span>
                            <asp:TextBox ID="Txtuser" runat="server" class="form-control text-padd" placeholder="User Name"></asp:TextBox>
                        </div>

                        <label class="control-label">Old Password:<span class="text-danger">*</span></label>
                        <div class="input-group taxt-marging" style="width: 60%;">
                            <span class="input-group-addon user-icon-bg">
                                <i class="glyphicon glyphicon-lock"></i>
                            </span>
                            <asp:TextBox ID="Txtpassword" runat="server" class="form-control text-padd" TextMode="Password" placeholder="Old Password" onchange="return checksamilartypsd();"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender177" runat="server" Enabled="True" TargetControlID="Txtpassword"
                                ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789[\!\@\#\$\%\^\&\(\)">
                            </asp:FilteredTextBoxExtender>
                        </div>

                        <label class="control-label">New Password:<span class="text-danger">*</span></label>
                        <div class="input-group taxt-marging" style="width: 60%;">
                            <span class="input-group-addon user-icon-bg">
                                <i class="glyphicon glyphicon-lock"></i>
                            </span>
                            <asp:TextBox ID="Txtpasswordnew" TextMode="Password" runat="server" class="form-control text-padd" placeholder="New Password" onchange="return checkPwd(this.value);"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilterTxtpassword2" runat="server" Enabled="True" TargetControlID="Txtpasswordnew"
                                ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789">
                            </asp:FilteredTextBoxExtender>
                        </div>

                        <label class="control-label">Confirm Password:<span class="text-danger">*</span></label>
                        <div class="input-group taxt-marging" style="width: 60%;">
                            <span class="input-group-addon user-icon-bg">
                                <i class="glyphicon glyphicon-lock"></i>
                            </span>
                            <asp:TextBox ID="TxtPasswordconfirm" runat="server" TextMode="Password" class="form-control text-padd" placeholder="Confirm Password" onchange="return gettxt();"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilterTxtpassword3" runat="server" Enabled="True" TargetControlID="TxtPasswordconfirm"
                                ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789">
                            </asp:FilteredTextBoxExtender>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <div class="panel panel-default" style="margin-top:11px">
                    <div class="panel-heading header-p">
                        <h4 class="text-danger" style="margin-left: -9px; color: Black !important">Note -  (<span class="text-danger">*</span>) are all mandatory fields </h4>
                    </div>
                    <div class="panel-body" style="min-height: 240px;">
                        <p class="chp-text-color">
                            1. Minimum 8 character
                        </p>
                        <p class="chp-text-color">
                            2. Maximum 20 character
                        </p>
                        <p class="chp-text-color">
                            3. Atleast one numeric
                        </p>
                        <p class="chp-text-color">
                            4. Atleast one Upper case
                        </p>
                        <p class="chp-text-color">
                            5. Atleast one lower case
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

