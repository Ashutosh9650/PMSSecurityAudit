<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmEmployeemaster2026.aspx.cs" Inherits="frmEmployeemaster2026" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .container-fluid {
            padding-right: 15px;
            padding-left: 15px;
            margin-right: auto;
            margin-left: auto;
        }

        .row {
            margin-left: -15px;
            margin-right: -15px;
        }

        sup {
            color: red;
        }

        .card_style {
            height: 250px;
            border: 2px dashed #ddd;
            display: flex;
            justify-content: center;
            align-items: center;
            object-fit: fill;
            object-position: center;
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
    <div class="container-fluid">
        <div class="row g-3">
            <div class="col-lg-8 coll-md-8 col-sm-12 col-xs-12">
                <div class="panel panel-info shadow">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">Employee code <sup>*</sup></label>
                                    <asp:TextBox ID="txtEmpCode" MaxLength="10" OnChange="UserAvailability()" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <span class="reqfield">
                                                        <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidatoruname" runat="server"
                                                            ControlToValidate="txtEmpCode" ErrorMessage="*" ForeColor="Red"
                                                            ValidationGroup="Valid">

                                                        </asp:RequiredFieldValidator></span>
                                </div>
                            </div>
                            <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">Employee Name <sup>*</sup></label>
                                             <asp:TextBox ID="txtFristName" onkeypress="return onlyAlphabets(event,this);" CssClass="form-control" MaxLength="30" runat="server" AutoComplete="off"></asp:TextBox>
                                                    <span class="reqfield">
                                                        <asp:RequiredFieldValidator ID="Req22" runat="server"
                                                            ControlToValidate="txtFristName" Display="None" ErrorMessage="*"
                                                            ValidationGroup="Valid">

                                                        </asp:RequiredFieldValidator></span>
                                </div>
                            </div>
                            <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">Designation <sup>*</sup></label>
                                    <asp:DropDownList ID="ddllevel" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel" runat="server"
                                                            ControlToValidate="ddllevel" InitialValue="0" ErrorMessage="*" ForeColor="Red"
                                                            ValidationGroup="Valid">

                                                        </asp:RequiredFieldValidator></span>
                                </div>
                            </div>
                            <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">DEPT <sup>*</sup></label>
                                   <label class="">Designation <sup>*</sup></label>
                                    <asp:DropDownList ID="ddldept" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                            ControlToValidate="ddldept" InitialValue="0" ErrorMessage="*" ForeColor="Red"
                                                            ValidationGroup="Valid">

                                                        </asp:RequiredFieldValidator></span>
                                </div>
                            </div>
                            
                            <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">Division Name </label>
                                                <asp:TextBox ID="txtDivision" runat="server" CssClass="form-control" MaxLength="100" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">Level </label>
                                     <asp:DropDownList ID="ddlEMplevel" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">M/F <sup>*</sup></label>
                                       <asp:DropDownList ID="ddlGender" runat="server" class="form-control">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">1-Male </asp:ListItem>
                                                        <asp:ListItem Value="2">2-Female</asp:ListItem>


                                                    </asp:DropDownList>
                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" InitialValue="0" runat="server" Display="Dynamic" ValidationGroup="saves"
                                                            ControlToValidate="ddlGender" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">Contact No. <sup>*</sup></label>
                                     <asp:TextBox ID="txtMobile" MaxLength="10" onkeypress="return isNumberKey(this,event);" onchange="javascript: phonenumber(this.value,'TeContact1');" CssClass="form-control TeContact1" runat="server"
                                                        AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">E-mail <sup>*</sup></label>
                          <asp:TextBox ID="txtEmail" MaxLength="50" CssClass="form-control" onchange="javascript:ValidateEmail(this);" runat="server"
                                                        AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                   <asp:LinkButton ID="btnSave" ValidationGroup="Valid" class="btn btn-link btn-xs" OnClick="btn_Save_Click"
                                            ToolTip="Save" runat="server"   Style="padding-right: 0px;margin-top:18px;font-size:large;margin-left:115px" >Save</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-info shadow">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">Date Joined <sup>*</sup></label>
                                    <asp:TextBox runat="server" ID="txtJoingDate" autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>

                                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                        Format="dd/MM/yyyy" TargetControlID="txtJoingDate" OnClientDateSelectionChanged="arrivaldatecheck" PopupPosition="BottomRight">
                                                    </ajax:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtJoingDate"
                                                        Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                        SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">Date of Leaving </label>
                                      <asp:TextBox runat="server" ID="txtDateLeaving" autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>

                                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                                        Format="dd/MM/yyyy" TargetControlID="txtDateLeaving" OnClientDateSelectionChanged="arrivaldatecheck" PopupPosition="BottomRight">
                                                    </ajax:CalendarExtender>
                                
                                </div>
                            </div>
                                <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">Date of Birth</label>
                                      <asp:TextBox runat="server" ID="txtBirth" autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>

                                                    <ajax:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True"
                                                        Format="dd/MM/yyyy" TargetControlID="txtBirth" OnClientDateSelectionChanged="arrivaldatecheck" PopupPosition="BottomRight">
                                                    </ajax:CalendarExtender>
                                
                                </div>
                            </div>
                            <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">Employment Type </label>
                                      <asp:DropDownList ID="ddlEMpType" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">Employee Status </label>
                                    <asp:DropDownList ID="ddlEMpstatus" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                </div>
                            </div>
                            <%--<div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">Reporting To <sup>*</sup></label>
                                    <select class="form-control">
                                        <option>Select Option</option>
                                    </select>
                                </div>
                            </div>--%>
                            <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">Work location </label>
                                     <asp:TextBox ID="txtworkLocation"  CssClass="form-control" runat="server" MaxLength="100" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">Employee Category </label>
                                    <asp:DropDownList ID="ddlEmpcat" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-3 coll-md-3 col-sm-4 col-xs-12">
                                <div class="form-group">
                                    <label class="">Remark <sup>*</sup></label>
                                    <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 coll-md-4 col-sm-12 col-xs-12">
                <div class="panel panel-info shadow">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="panel panel-default card_style">
                                     <asp:Image ID="imgMKS" runat="server"
                                                                                    BorderStyle="Ridge" BorderWidth="0px" Width="57%" />
                                </div>
                                <div class="col-sm-12">
                                       <asp:FileUpload ID="FileuploadAttach" runat="server" Width="160px" Font-Size="Smaller"
                                                                                    TabIndex="16" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="col-sm-12">
                <div class="table-responsive">
                     <asp:GridView ID="dgvleftgrid" Width="100%" AllowPaging="true" PageSize="10" runat="server"
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
                                     <asp:BoundField DataField="DivisionName" HeaderText="DivisionName"></asp:BoundField>
                                     <asp:BoundField DataField="Role" HeaderText="Designation" ></asp:BoundField>
                                        <asp:BoundField DataField="DeptName" HeaderText="DEPT"></asp:BoundField>

                                     <asp:BoundField DataField="deptLevel" HeaderText="Level" ></asp:BoundField>
                                     <asp:BoundField DataField="MobileNo" HeaderText="Contact No" ></asp:BoundField>
                                     <asp:BoundField DataField="gender" HeaderText="M/F" ></asp:BoundField>
                                     <asp:BoundField DataField="EmaillID" HeaderText="E-Mail" ></asp:BoundField>
                                </Columns>
                                <PagerSettings Position="Bottom" />
                            </asp:GridView>

                <%--    <table class="table table-striped table-bordered">
                        <thead>
                            <tr  class="info">
                                <th>Employee Code</th>
                                <th>State</th>
                                <th>District</th>
                                <th>Block/Office</th>
                                <th>Division Name</th>
                                <th>Designation</th>
                                <th>DEPT</th>
                                <th>Level</th>
                                <th>Date Joined</th>
                                <th>Date of Leaving</th>
                                <th>Contact No.</th>
                                <th>M/F</th>
                                <th>E-Mail</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>--</td>
                                <td>--</td>
                                <td>--</td>
                                <td>--</td>
                                <td>--</td>
                                <td>--</td>
                                <td>--</td>
                                <td>--</td>
                                <td>--</td>
                                <td>--</td>
                                <td>--</td>
                                <td>--</td>
                                <td>--</td>
                            </tr>
                        </tbody>
                    </table>--%>
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

