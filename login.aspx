<%@ Page Language="C#" AutoEventWireup="true"   CodeFile="login.aspx.cs" Inherits="login" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
    <head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Educate_Girls-New</title>

    <link rel="stylesheet" type="text/css" href="css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="css/font-awesome.css" />
    <link rel="stylesheet" type="text/css" href="css/bootstrap-multiselect.css" />
        <link rel="stylesheet" href="css/font-awesome.css" />
    <script type="text/javascript" src="js/jquery-2.1.0.js"></script>
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.js"></script>
    <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
    <link rel="stylesheet" type="text/css" href="css/pk_login.css" />



</head>

<%--<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Educate_Girls-New</title>

    <link rel="stylesheet" type="text/css" href="css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="css/font-awesome.css" />
    <link rel="stylesheet" type="text/css" href="css/bootstrap-multiselect.css" />

    <script type="text/javascript" src="js/jquery-2.1.0.js"></script>
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.js"></script>
    <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
    <link rel="stylesheet" type="text/css" href="css/pk_login.css" />


</head>--%>

<body>






    <section class="login-section-wrp">
        <div id="carousel-example-generic" class="carousel-fade carousel slide" data-ride="carousel">
            <!-- Indicators -->
            <ol class="carousel-indicators">
                <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                <li data-target="#carousel-example-generic" data-slide-to="1"></li>
                <li data-target="#carousel-example-generic" data-slide-to="2"></li>
            </ol>
            <!-- Wrapper for slides -->
            <div class="carousel-inner" role=" ">
                <div class="item active">
                    <img src="Image/Slider_2.jpg" />

                </div>
                <div class="item">
                    <img src="Image/Slider_3.jpg" />

                </div>
                <div class="item">
                    <img src="Image/Slider_9.jpg" />
                </div>
            </div>
        </div>
    </section>

    <form id="Form1" runat="server">
        <div class="login-box">
            <div class="login-box-inner">
                <div class="login-logo">
                    <img src="Image/educate-girls-logo.png" />
                </div>
                <h2>Sign In</h2>
                <%--  <p>Program Management System for Educate Girls NGO</p>--%>

                <div class="form-group">
                    <label>Username</label>
                    <div class="input-feild-wrp">
                        <span class="icon-pic" id="basic-addon1"><i class="fa fa-user"></i></span>
                        <asp:TextBox ID="txtUserName" class="form-control" runat="server" MaxLength="20" CssClass="CommonControlText"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVName" runat="server" Display="Dynamic" Font-Size="11px"
                            ErrorMessage="Please enter UserId" ForeColor="Red" ControlToValidate="txtUserName"
                            ValidationGroup="VGLogin" class="alert-border"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label>Password</label>
                    <div class="input-feild-wrp">
                        <span class="icon-pic" id="Span1"><i class="fa fa-lock"></i></span>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="20"
                            CssClass="CommonControlText"></asp:TextBox>
                        <i class="show-password fa fa-eye"></i>
                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" ControlToValidate="txtPassword"
                            runat="server" ValidationGroup="VGLogin" Font-Size="11px" ForeColor="Red" ErrorMessage="Please enter Password" class="alert-border"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">

   <div class="form-group">
    <div style="display:flex;align-items:center;justify-content:left;gap:15px;">

        <img id="imgCaptcha"
             src="CaptchaImage.aspx"
             alt="CAPTCHA"
             style="width:90px;height:35px;border:1px solid #ccc;" />

        <asp:ImageButton
            ID="btnRefresh"
            runat="server"
            ImageUrl="~/images/Reset.png"
            Width="28"
            Height="28"
            CausesValidation="false"
            ToolTip="Refresh CAPTCHA"
            Style="border:none;background:none;margin-right:10px;"
            OnClientClick="document.getElementById('imgCaptcha').src='CaptchaImage.aspx?'+new Date().getTime();return false;" />

        <asp:TextBox
            ID="txtCaptcha"
            runat="server"
            CssClass="form-control"
            MaxLength="6"
            placeholder="Enter CAPTCHA"
            Style="width:130px;height:35px;" />

    </div>
</div>

</div>
            <asp:Button runat="server" CssClass="btn   btn-block btn btn-black" OnClick="btnLogin_Click" Text="Sign In" />
            </div>


        </div> 
    </form>
    
</body>

<%--<script src="https://code.jquery.com/jquery-3.7.1.min.js">--%>
    <script >
    $('.show-password').click(function () {

        if ($(this).hasClass('fa-eye')) {

            $(this).removeClass('fa-eye');

            $(this).addClass('fa-eye-slash');

            $('#txtPassword').attr('type', 'text');

        } else {

            $(this).removeClass('fa-eye-slash');

            $(this).addClass('fa-eye');

            $('#txtPassword').attr('type', 'password');
        }
    });
</script>

</html>







