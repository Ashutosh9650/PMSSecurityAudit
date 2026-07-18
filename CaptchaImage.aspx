<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaptchaImage.aspx.cs"  Inherits="CaptchaImage" %>
<div class="form-group text-center">

    <table style="margin:auto;">
        <tr>
            <td>
                <img id="imgCaptcha"
                     src="CaptchaImage.aspx"
                     alt="CAPTCHA"
                     style="height:40px;width:140px;border:1px solid #ccc;" />
            </td>

            <td style="padding-left:8px;">
                <asp:LinkButton ID="btnRefresh"
                    runat="server"
                    CausesValidation="false"
                    ToolTip="Refresh CAPTCHA"
                    OnClientClick="document.getElementById('imgCaptcha').src='CaptchaImage.aspx?'+new Date().getTime();return false;">
                    <i class="fa fa-refresh fa-2x"></i>
                </asp:LinkButton>
            </td>
        </tr>

        <tr>
            <td colspan="2" style="padding-top:8px;">
                <asp:TextBox
                    ID="txtCaptcha"
                    runat="server"
                    CssClass="form-control"
                    MaxLength="6"
                    placeholder="Enter CAPTCHA"
                    Style="width:140px;height:34px;font-size:14px;margin:auto;">
                </asp:TextBox>
            </td>
        </tr>
    </table>

    <asp:Label
        ID="lblMessage"
        runat="server"
        ForeColor="Red">
    </asp:Label>

</div>


