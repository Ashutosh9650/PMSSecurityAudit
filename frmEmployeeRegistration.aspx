<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Culture="en-GB" CodeFile="frmEmployeeRegistration.aspx.cs" Inherits="Frm_frmEmployeeRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .padd {
            padding-left: 15px;
            padding-right: 15px;
        }

        .rows {
            margin-left: -15px;
            margin-right: -15px;
        }

        legend.scheduler-border {
            padding: 0px 10px;
        }

        fieldset.scheduler-border {
            padding: 10px 1.4em 10px 1.4em !important;
        }

        .d-none {
            display: none;
        }
    </style>
    <script type="text/javascript">

        function UserAvailability() { //This function call on text change.             
            $.ajax({
                type: "POST",
                url: "frmEmployeeRegistration.aspx/CheckUserID", // this for calling the web method function in cs code.  
                data: '{useroremail: "' + $("#<%=txtEmpCode.ClientID%>")[0].value + '" }', // user name  
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response);
                }
            });

            function OnSuccess(response) {

                switch (response.d) {
                    case "true":

                        document.getElementById('<%=txtEmpCode.ClientID %>').value = "";
                        document.getElementById('<%=txtEmpCode.ClientID %>').focus();
                        alert("Employee Code Already Exist!.");
                        return false;


                        break;
                    case "false":


                        break;
                }
            }

        }



    </script>
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
    <script type="text/javascript">


        function phonenumber(inputtxt, txtid) {
            var phoneno = /^\d{10}$/;
            if (phoneno.test(inputtxt) && inputtxt.length == 10) {
                $("." + txtid).css("border", "solid 1px green")
                return true;
            }
            else {
                $("." + txtid).css("border", "solid 1px red")
                $("." + txtid).val('');
                alert("Mobile No. should be 10 digit");

                return false;
            }
        }

    </script>
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
    <script language="Javascript" type="text/javascript">
        function ValidateEmail(inputText) {


            var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            if (inputText.value.match(mailformat)) {
                document.form1.text1.focus();
                return true;
            }
            else {
                alert("You have entered an invalid email address!");
                inputText.value = '';
                inputText.focus();
                return false;
            }
        }
    </script>
    <script language="Javascript" type="text/javascript">

        function onlyAlphabets(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 0 || charCode == 127 || charCode == 32 || charCode == 08 || charCode == 09 || charCode == 13)
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }

    </script>

    <script language="Javascript" type="text/javascript">

        function onlyAlphabetsAdd(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 32 || charCode == 0 || charCode == 9 || charCode == 08 || charCode == 44 || charCode == 45 || charCode == 48)
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }

    </script>
    <style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading" style="padding: 5px;">

                        <div class="row">

                            <div id="Div5" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
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

                            <div class="col-lg-1 col-md-1 col-sm-1 cpl-xs-12">
                                <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-left"
                                    BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />

                            </div>
                        </div>


                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12" style="padding-right: 0px;">
                    <div style="height: 441px; overflow: auto;">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="padding: 1px 15px">
                                <h3 class="text-danger" style="margin: 7px 0px;">Employee Detail</h3>
                            </div>

                            <asp:GridView ID="dgvleftgrid" Width="100%" AllowPaging="true" PageSize="25" runat="server"
                                OnPageIndexChanging="GV_Project_PageIndexChanging" BorderStyle="None" GridLines="None" AutoGenerateColumns="false"
                                OnRowCommand="dgvleftgrid_rowcommand" DataKeyNames="EmployeeID">
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

                                    <asp:ButtonField DataTextField="EmployeeID" ItemStyle-ForeColor="#333"
                                        HeaderText="Employee Code" Text="Button" CommandName="Show">
                                        <ItemStyle CssClass="padding-lef" Height="30px" />
                                        <HeaderStyle CssClass="padding-lef" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="EmployeeID" HeaderText="UserID" Visible="False"></asp:BoundField>

                                    <asp:ButtonField DataTextField="Firstname" ItemStyle-ForeColor="#333"
                                        HeaderText="Name" Text="Button" CommandName="Show">
                                        <ItemStyle CssClass="padding-lef" Height="30px" />
                                        <HeaderStyle CssClass="padding-lef" />
                                    </asp:ButtonField>

                                </Columns>
                                <PagerSettings Position="Bottom" />
                            </asp:GridView>

                        </div>
                    </div>
                </div>

                <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="padding: 0px 0px">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6">
                                    <h3 class="text-danger" style="margin: 6px 0px;">Employee Master</h3>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6">
                                    <span style="float: right">
                                        <asp:ImageButton ID="btnSave" ValidationGroup="Valid" class="btn btn-primary pull-right" OnClick="btn_Save_Click"
                                            ToolTip="Save" runat="server" BackColor="#f5f5f5" ImageUrl="~/images/save-29-1.png" Style="padding-right: 0px;" /></span>
                                    <asp:ImageButton ID="Button1" class="btn btn-primary pull-right marg-butt"
                                        runat="server" BackColor="#f5f5f5" ToolTip="Add" ImageUrl="~/images/add-29-1.png" OnClick="btn_Add_click" Style="padding-right: 0px;" />


                                </div>
                            </div>
                        </div>


                        <div class="panel-body">
                            <div class="row .rows">
                                <div class="col-lg-8 col-md-8 col-sm-10 col-xs-12" style="padding-left: 2px;">
                                    <div class="form-horizontal">
                                        <div id="a" runat="server" class="row">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4" for="Name">Employee Type  <span class="req">*</span> </label>

                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddllevel" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel" runat="server"
                                                            ControlToValidate="ddllevel" InitialValue="0" ErrorMessage="*" ForeColor="Red"
                                                            ValidationGroup="Valid">

                                                        </asp:RequiredFieldValidator></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4" for="Name">Employee Code<span class="req">*</span></label>

                                                <div class="col-sm-8">

                                                    <asp:TextBox ID="txtEmpCode" MaxLength="10" OnChange="UserAvailability()" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <span class="reqfield">
                                                        <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidatoruname" runat="server"
                                                            ControlToValidate="txtEmpCode" ErrorMessage="*" ForeColor="Red"
                                                            ValidationGroup="Valid">

                                                        </asp:RequiredFieldValidator></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4" for="Name">Name  <span class="req">*</span></label>

                                                <div class="col-sm-8">
                                                    <%--   <asp:TextBox ID="txtpw" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtFristName" onkeypress="return onlyAlphabets(event,this);" CssClass="form-control" MaxLength="30" runat="server" AutoComplete="off"></asp:TextBox>
                                                    <span class="reqfield">
                                                        <asp:RequiredFieldValidator ID="Req22" runat="server"
                                                            ControlToValidate="txtFristName" Display="None" ErrorMessage="*"
                                                            ValidationGroup="Valid">

                                                        </asp:RequiredFieldValidator></span>


                                                </div>
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4" for="Name">Gender </label>

                                                <div class="col-sm-8">


                                                    <asp:DropDownList ID="ddlGender" runat="server" class="form-control">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">1-Male </asp:ListItem>
                                                        <asp:ListItem Value="2">2-Female</asp:ListItem>


                                                    </asp:DropDownList>
                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" InitialValue="0" runat="server" Display="Dynamic" ValidationGroup="saves"
                                                            ControlToValidate="ddlGender" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </span>

                                                </div>
                                            </div>
                                        </div>


                                        <div class="row" id="Div2" runat="server">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4" for="Name" id="Label2" runat="server">Email </label>

                                                <div class="col-sm-8">

                                                    <asp:TextBox ID="txtEmail" MaxLength="50" CssClass="form-control" onchange="javascript:ValidateEmail(this);" runat="server"
                                                        AutoComplete="off"></asp:TextBox>

                                                </div>
                                            </div>

                                        </div>

                                        <div class="row" id="Div3" runat="server">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4" for="Name" id="Label3" runat="server">Contact Number</label>

                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtMobile" MaxLength="10" onkeypress="return isNumberKey(this,event);" onchange="javascript: phonenumber(this.value,'TeContact1');" CssClass="form-control TeContact1" runat="server"
                                                        AutoComplete="off"></asp:TextBox>

                                                </div>
                                            </div>

                                        </div>






                                        <div id="Div4" runat="server" class="row">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4" for="Name" id="Label4" runat="server">Date of Birth   </label>

                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txtBirth" autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>

                                                    <ajax:CalendarExtender ID="clk" runat="server" Enabled="True"
                                                        Format="dd/MM/yyyy" TargetControlID="txtBirth" OnClientDateSelectionChanged="arrivaldatecheck" PopupPosition="BottomRight">
                                                    </ajax:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBirth"
                                                        Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                        SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>



                                                </div>
                                            </div>
                                        </div>

                                        <div id="Div1" runat="server" class="row">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4" for="Name" id="Label1" runat="server">Date of Joining  <span class="req">*</span> </label>

                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="txtJoingDate" autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>

                                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                        Format="dd/MM/yyyy" TargetControlID="txtJoingDate" OnClientDateSelectionChanged="arrivaldatecheck" PopupPosition="BottomRight">
                                                    </ajax:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtJoingDate"
                                                        Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                        SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>



                                                </div>
                                            </div>
                                        </div>



                                    </div>
                                </div>
                                 <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <fieldset class="scheduler-border" style="min-height: 226px;">
                                                                <legend class="scheduler-border">Image</legend>
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel ID="Image" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                Image</label>
                                                                            <div class="col-sm-8">
                                                                                <asp:FileUpload ID="FileuploadAttach" runat="server" Width="160px" Font-Size="Smaller"
                                                                                    TabIndex="16" />
                                                                                <asp:Image ID="imgMKS" runat="server" Height="140px" Width="180px" BorderColor="Black"
                                                                                    BorderStyle="Ridge" BorderWidth="1px" />
                                                                            </div>
                                                                        </ContentTemplate>
                                                                       
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                </fieldset>
                                     </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>




            <%-- <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>--%>



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
         <Triggers>
                                                                            <asp:PostBackTrigger ControlID="btnsave" />
                                                                
                                                                        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

