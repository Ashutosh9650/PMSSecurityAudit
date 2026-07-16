<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="frmTrackerNew.aspx.cs" Inherits="frmTrackerNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ddlCars').multiselect();
            $('#ddlCars1').multiselect({
                numberDisplayed: 2

            });
            $('#ddlCars2').multiselect({
                includeSelectAllOption: true,
                enableFiltering: true

            });
            $('#ddlCars3').multiselect({
                nonSelectedText: 'Select Cars'

            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ddlCars').multiselect();
            $('#ddlCars1').multiselect({
                numberDisplayed: 2

            });
            $('#ddlCars2').multiselect({
                includeSelectAllOption: true,
                enableFiltering: true

            });
            $('#ddlCars3').multiselect({
                nonSelectedText: 'Select Cars'

            });
        });
    </script>
    <script language="Javascript" type="text/javascript">

        function onlyAlphabets(t, e) {
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

        function onlyAlphabetsAdd(t, e) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 48 && charCode < 57) || (charCode > 96 && charCode < 123) || charCode == 32 || charCode == 0 || charCode == 9 || charCode == 08 || charCode == 44 || charCode == 45 || charCode == 48)
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }


        function onlyAlphabetsHH(t, e) {
            try {


                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 48 && charCode < 57) || (charCode > 96 && charCode < 123) || charCode == 32 || charCode == 32 || charCode == 0 || charCode == 9)
                    return true;
                else
                    return false;

            }
            catch (err) {
                alert(err.Description);
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

        function Valdation(txtcls, txtaBoy) {
            var Eboy = 0;
            var Aboy = 0;
            $("." + txtcls).each(function (index, value) {
                if ($.trim($(this).val()) != "")
                    if (!isNaN($(this).val()))

                        Eboy = parseFloat($("." + txtaBoy).val());
                Aboy = parseFloat($("." + txtcls).val());

                if (Aboy < Eboy) {

                    alert("Enrollment  should be higher or equal to Appeared");
                    $("." + txtcls).focus();
                    $("." + txtaBoy).val('');
                    return true;
                }
                else {
                    return true;
                }

            });




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
    <script type="text/javascript">

        function calculate_totals(txtcls, txttotalcls) {
            var TotalCamt = 0;
            $("." + txtcls).each(function (index, value) {
                if ($.trim($(this).val()) != "")
                    if (!isNaN($(this).val()))
                        TotalCamt = TotalCamt + parseFloat($(this).val());
            });
            $("." + txttotalcls).val(TotalCamt);
            return false;
        }

        function arrivaldate(arrivaldate) {

            var arrivaldate = $('#' + arrivaldate).val();

            var today = new Date();
            alert(arrivaldate);
            alert(today.getDate());
            if (arrivaldate > today.getDate()) {
                alert("Should not be future date.");
                document.getElementById("" + sender + "").value = null;
                return false;
            }


        }

        function checkDate(arrivaldate) {
            var EnteredDate = $('#' + arrivaldate).val();

            var date = EnteredDate.substring(0, 2);

            var month = EnteredDate.substring(3, 5);
            var year = EnteredDate.substring(6, 10);

            var myDate = new Date(year, month - 1, date);

            var today = new Date();

            if (myDate > today) {
                alert("Should not be future date.");
                $('#' + arrivaldate).val = '';
            }

        }


   <%--     function checkPwd(str) {


           var msg = "";
           if (str.search(/\d/) == -1) {

               msg += 'Please enter atleast one number'; // for numeric
           }

           if (msg != "") {
               document.getElementById('<%=txtHouse.ClientID %>').value = "";

               alert(msg);
               return false;
           }
           else { return true; }
       } --%>
    </script>
    <style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="mkiiainpnl121">
        <ContentTemplate>
            <div class="container-fluid">
            </div>
            <div class="container-fluid" style="margin-top: 0px;">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading" style="padding: 5px 15px 5px 5px;">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <h3 class="text-danger" style="margin: 0px;">
                                                    <asp:Label ID="lblMain" runat="server" Text="Form Assessment Tracker"></asp:Label>
                                                    <asp:Label ID="lblNum" Visible="false" runat="server" Text=""></asp:Label>
                                                    <asp:Button ID="Button1" runat="server" Text="T" Style="margin-right: -653px;" class="pull-right" OnClick="btnS5_Click" />
                                                </h3>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">
                                                <asp:Label ID="lblStatus" runat="server" Font-Bold="true" Text="" CssClass="pull-left"></asp:Label>

                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div style="padding: 0px 10px 0px 10px;">
                                                <div class="row marg search-bg">
                                                    <div class="form-horizontal">
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="fromType" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                    Form Type:
                                                                </label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlformatype" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                                                        runat="server" class="form-control ">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
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
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
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
                                                                    Panchayat:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"
                                                                        class="form-control " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                    Village:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlVillage" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged"
                                                                        AutoPostBack="true" runat="server" class="form-control " />
                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                                                                            ValidationGroup="saves" ControlToValidate="ddlVillage" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblSchool" runat="server" class="col-sm-3 padd linhei" Visible="false">School:</asp:Label>
                                                                <%-- <label for="email" class="col-sm-3 padd linhei" runat="server" visible="false"  style="padding-top: 2px;">School:</label>--%>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlschool" runat="server" Visible="false" AutoPostBack="true"
                                                                        class="form-control " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-left"
                                                                BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                                                            <asp:Button ID="Btn" runat="server" OnClick="btnS_Click" />
                                                            <asp:Label ID="lblNag" runat="server" BackColor="Black" Font-Bold="true" Text="" CssClass="pull-left"></asp:Label>


                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="MainPanel" runat="server">
                                                <ContentTemplate>
                                                    <%--<div class="col-lg-1">
                                                    </div>--%>
                                                    <div class="col-lg-12">
                                                        <asp:Panel ID="pnlMain" Enabled="true" runat="server">
                                                            <div class="panel-body" style="padding: 0px;">
                                                                <div class="row">
                                                                    <div class="form-horizontal" role="form">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">
                                                                        </div>
                                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">
                                                                            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1" style="padding: 0px 3px 0px 5px;">
                                                                            </div>
                                                                            <%--form 6--%>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">
                                                                                <asp:GridView ID="Grdform6" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                                    AllowPaging="true" PageSize="100" AutoGenerateColumns="false" Font-Names="Arial"
                                                                                    Font-Size="12px" Width="100%" ShowFooter="true" OnRowDataBound="GvForm6_RowDataBound">

                                                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                                                    <RowStyle HorizontalAlign="Left" />
                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />

                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Unique Code" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblUniqueCode" runat="server" Text='<%#Eval("UniqueCode") %>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>

                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Form 6 Received Date" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtrecivedate" runat="server" CssClass="form-control" Text='<%#Eval("ReceivedDate") %>'
                                                                                                    Visible="true" onkeypress="return false;"></asp:TextBox>
                                                                                                <div style="position: absolute; left: 596px; width: 45px; top: 37px; height: 18px; z-index: 121;">
                                                                                                    <ajax:CalendarExtender ID="CalendarExtendertxtrecivedate" Enabled="true" runat="server"
                                                                                                        Format="dd/MM/yyyy" TargetControlID="txtrecivedate" PopupPosition="BottomRight">
                                                                                                    </ajax:CalendarExtender>
                                                                                                </div>
                                                                                                <span class="reqfield" style="margin: -10px -5px;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel11" runat="server" ControlToValidate="txtrecivedate"
                                                                                                        ErrorMessage="*" ForeColor="Red" ValidationGroup="Save6">
                                                                                                    </asp:RequiredFieldValidator></span>

                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Collection Date">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtcollectiondate" runat="server" CssClass="form-control" Text='<%#Eval("CollectionDate") %>'
                                                                                                    onkeypress="return false;"></asp:TextBox>
                                                                                                <span class="reqfield" style="margin: -10px -5px;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel12" runat="server" ControlToValidate="txtcollectiondate"
                                                                                                        ErrorMessage="*" ForeColor="Red" ValidationGroup="Save6">
                                                                                                    </asp:RequiredFieldValidator></span>
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtcollectiondate" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtcollectiondate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="No. Of Girls ">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtnoofgirols" runat="server" CssClass="form-control" Text='<%#Eval("NoOfGirls") %>' MaxLength="3"
                                                                                                    onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                                                <span class="reqfield" style="margin: -10px -5px;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel13" Display="Dynamic"
                                                                                                        runat="server" ControlToValidate="txtnoofgirols" ErrorMessage="*" ForeColor="Red"
                                                                                                        ValidationGroup="Save6">
                                                                                                    </asp:RequiredFieldValidator></span>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="No. Of Boys ">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtNoOfBoys" runat="server" CssClass="form-control" MaxLength="3" Text='<%#Eval("NoOfBoys") %>'
                                                                                                    onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                                                <span class="reqfield" style="margin: -10px -5px;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel14" Display="Dynamic"
                                                                                                        runat="server" ControlToValidate="txtNoOfBoys" ErrorMessage="*" ForeColor="Red"
                                                                                                        ValidationGroup="Save6">
                                                                                                    </asp:RequiredFieldValidator></span>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Code">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="txtcode" runat="server" Text='<%#Eval("FormCode") %>' Visible="false"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Status">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="txtcodFStatuse" runat="server" Text='<%#Eval("FStatus") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField Visible="true">
                                                                                            <FooterTemplate>
                                                                                                <asp:LinkButton ID="btnAddIndividualEducation6" OnClick="btnAddForm6row_Click" runat="server"
                                                                                                    Font-Bold="true" Width="100px" ForeColor="Black" Text="Add New Row" />
                                                                                            </FooterTemplate>
                                                                                            <ItemTemplate>


                                                                                                <asp:ImageButton Width="25px" Height="25px" Visible="false" ID="ImageButton1" OnClick="btnApprove6_Click" ToolTip="Approve" runat="server"
                                                                                                    ImageUrl="~/images/statusG.png" />

                                                                                                <asp:ImageButton Width="25px" Height="25px" ID="ImageButton2" OnClick="Reject6_Click" Visible="false" ToolTip="Reject" runat="server"
                                                                                                    ImageUrl="~/images/reject.png" />

                                                                                                <asp:ImageButton Width="25px" Height="25px" ID="btnSave6" Visible="false" ValidationGroup="Save6" ToolTip="Save" runat="server" OnClick="btnSave6_Click"
                                                                                                    ImageUrl="~/images/save-29-1.png" />

                                                                                                <asp:ImageButton Width="25px" Height="25px" ID="btnDeleteTab1" Visible="false" ToolTip="Delete" runat="server" OnClick="btnDeleteTab1_Click"
                                                                                                    ImageUrl="~/images/delete-29.png" />
                                                                                            </ItemTemplate>
                                                                                            <FooterStyle HorizontalAlign="left" />
                                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                            <%--form 7--%>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">
                                                                                <asp:GridView ID="grdform7" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                                    AllowPaging="true" PageSize="100" AutoGenerateColumns="false" Font-Names="Arial"
                                                                                    Font-Size="12px" Width="100%" ShowFooter="true" OnRowDataBound="GvForm7_RowDataBound">

                                                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                                                    <RowStyle HorizontalAlign="Left" />
                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />

                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Unique Code" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblUniqueCode" runat="server" Text='<%#Eval("UniqueCode") %>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>

                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Form 7 Received Date" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtrecivedate" CssClass="form-control" runat="server" Text='<%#Eval("ReceivedDate") %>'
                                                                                                    Visible="true" onkeypress="return false;"></asp:TextBox>
                                                                                                <span class="reqfield" style="margin: -10px -5px;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel15" Display="Dynamic"
                                                                                                        runat="server" ControlToValidate="txtrecivedate" ErrorMessage="*" ForeColor="Red"
                                                                                                        ValidationGroup="saves">
                                                                                                    </asp:RequiredFieldValidator></span>
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtrecivedate" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtrecivedate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Collection Date">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtcollectiondate" CssClass="form-control" runat="server" Text='<%#Eval("CollectionDate") %>'
                                                                                                    onkeypress="return false;"></asp:TextBox>
                                                                                                <span class="reqfield" style="margin: -10px -5px;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel16" Display="Dynamic"
                                                                                                        runat="server" ControlToValidate="txtcollectiondate" ErrorMessage="*" ForeColor="Red"
                                                                                                        ValidationGroup="saves">
                                                                                                    </asp:RequiredFieldValidator></span>

                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtcollectiondate" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtcollectiondate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>

                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Code">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="txtcode" runat="server" Text='<%#Eval("FormCode") %>' Visible="false"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Status">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="txtcodFStatuse" runat="server" Text='<%#Eval("FStatus") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField Visible="true">
                                                                                            <FooterTemplate>
                                                                                                <asp:LinkButton ID="btnAddIndividualEducation7" OnClick="btnAddForm7row_Click" runat="server"
                                                                                                    Font-Bold="true" Width="100px" ForeColor="Black" Text="Add New Row" />
                                                                                            </FooterTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton Width="25px" Height="25px" Visible="false" ID="ImageButton1" OnClick="btnApprove7_Click" ToolTip="Approve" runat="server"
                                                                                                    ImageUrl="~/images/statusG.png" />

                                                                                                <asp:ImageButton Width="25px" Height="25px" ID="ImageButton2" OnClick="Reject7_Click" Visible="false" ToolTip="Reject" runat="server"
                                                                                                    ImageUrl="~/images/reject.png" />


                                                                                                <asp:ImageButton Width="25px" Height="25px" ID="btnSave7" ValidationGroup="saves" ToolTip="Save" runat="server" OnClick="btnSave7_Click"
                                                                                                    ImageUrl="~/images/save-29-1.png" />
                                                                                                <asp:ImageButton Width="25px" Height="25px" ID="btnDeleteTab2" ToolTip="Delete" runat="server" OnClick="btnDeleteTab2_Click"
                                                                                                    ImageUrl="~/images/delete-29.png" />
                                                                                            </ItemTemplate>
                                                                                            <FooterStyle HorizontalAlign="Right" />
                                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                            <%--form 8--%>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">
                                                                                <asp:GridView ID="grdform8" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                                    AllowPaging="true" PageSize="100" AutoGenerateColumns="false" Font-Names="Arial"
                                                                                    Font-Size="12px" Width="100%" ShowFooter="true" OnRowDataBound="GvForm8_RowDataBound">

                                                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                                                    <RowStyle HorizontalAlign="Left" />
                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />

                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Unique Code" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblUniqueCode" runat="server" Text='<%#Eval("UniqueCode") %>' Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>

                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Form 8 Received Date" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtrecivedate" CssClass="form-control" runat="server" Text='<%#Eval("ReceivedDate") %>'
                                                                                                    Visible="true" onkeypress="return false;"></asp:TextBox>
                                                                                                <span class="reqfield" style="margin: -10px -5px;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel17" Display="Dynamic"
                                                                                                        runat="server" ControlToValidate="txtrecivedate" ErrorMessage="*" ForeColor="Red"
                                                                                                        ValidationGroup="saves">
                                                                                                    </asp:RequiredFieldValidator></span>
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtrecivedate" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtrecivedate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Collection Date">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtcollectiondate" CssClass="form-control" runat="server" Text='<%#Eval("CollectionDate") %>'
                                                                                                    onkeypress="return false;"></asp:TextBox>
                                                                                                <span class="reqfield" style="margin: -10px -5px;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel18" Display="Dynamic"
                                                                                                        runat="server" ControlToValidate="txtcollectiondate" ErrorMessage="*" ForeColor="Red"
                                                                                                        ValidationGroup="saves">
                                                                                                    </asp:RequiredFieldValidator></span>
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtcollectiondate" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtcollectiondate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Code">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="txtcode" runat="server" Text='<%#Eval("FormCode") %>' Visible="false"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Status">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="txtcodFStatuse" runat="server" Text='<%#Eval("FStatus") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField Visible="true">
                                                                                            <FooterTemplate>
                                                                                                <asp:LinkButton ID="btnAddIndividualEducation8" OnClick="btnAddForm8row_Click" runat="server"
                                                                                                    Font-Bold="true" Width="100px" ForeColor="Black" Text="Add New Row" />
                                                                                            </FooterTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton Width="25px" Height="25px" Visible="false" ID="ImageButton1" OnClick="btnApprove8_Click" ToolTip="Approve" runat="server"
                                                                                                    ImageUrl="~/images/statusG.png" />

                                                                                                <asp:ImageButton Width="25px" Height="25px" ID="ImageButton2" Visible="false" OnClick="Reject8_Click" ToolTip="Reject" runat="server"
                                                                                                    ImageUrl="~/images/reject.png" />

                                                                                                <asp:ImageButton Width="25px" Height="25px" ID="btnSave8" ValidationGroup="saves" ToolTip="Save" runat="server" OnClick="btnSave8_Click"
                                                                                                    ImageUrl="~/images/save-29-1.png" />

                                                                                                <asp:ImageButton Width="25px" Height="25px" ID="btnDeleteTab3" ToolTip="Delete" runat="server" OnClick="btnDeleteTab3_Click"
                                                                                                    ImageUrl="~/images/delete-29.png" />
                                                                                            </ItemTemplate>
                                                                                            <FooterStyle HorizontalAlign="Right" />
                                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                            <%--form 9--%>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">
                                                                                <asp:GridView ID="grdform9" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                                    AllowPaging="true" PageSize="100" AutoGenerateColumns="false" Font-Names="Arial"
                                                                                    Font-Size="12px" Width="100%" ShowFooter="true" OnRowDataBound="GvForm9_RowDataBound">
                                                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                                                    <RowStyle HorizontalAlign="Left" />
                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />

                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Unique Code" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblUniqueCode" runat="server" Text='<%#Eval("UniqueCode") %>' Visible="false"></asp:Label>

                                                                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>

                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Form 9 Received Date" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtrecivedate" CssClass="form-control" runat="server" Text='<%#Eval("ReceivedDate") %>'
                                                                                                    Visible="true" onkeypress="return false;"></asp:TextBox>
                                                                                                <span class="reqfield" style="margin: -10px -5px;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel20" Display="Dynamic"
                                                                                                        runat="server" ControlToValidate="txtrecivedate" ErrorMessage="*" ForeColor="Red"
                                                                                                        ValidationGroup="saves">
                                                                                                    </asp:RequiredFieldValidator></span>
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtrecivedate" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtrecivedate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Collection Date">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtcollectiondate" CssClass="form-control" runat="server" Text='<%#Eval("CollectionDate") %>'
                                                                                                    onkeypress="return false;"></asp:TextBox>
                                                                                                <span class="reqfield" style="margin: -10px -5px;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel21" Display="Dynamic"
                                                                                                        runat="server" ControlToValidate="txtcollectiondate" ErrorMessage="*" ForeColor="Red"
                                                                                                        ValidationGroup="saves">
                                                                                                    </asp:RequiredFieldValidator></span>
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtcollectiondate" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtcollectiondate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Code">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="txtcode" runat="server" Text='<%#Eval("FormCode") %>' Visible="false"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Status">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="txtcodFStatuse" runat="server" Text='<%#Eval("FStatus") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField Visible="true">
                                                                                            <FooterTemplate>
                                                                                                <asp:LinkButton ID="btnAddIndividualEducation9" OnClick="btnAddForm9row_Click" runat="server"
                                                                                                    Font-Bold="true" Width="100px" ForeColor="Black" Text="Add New Row" />
                                                                                            </FooterTemplate>
                                                                                            <ItemTemplate>

                                                                                                <asp:ImageButton Width="25px" Height="25px" Visible="false" ID="ImageButton1" OnClick="btnApprove9_Click" ToolTip="Approve" runat="server"
                                                                                                    ImageUrl="~/images/statusG.png" />

                                                                                                <asp:ImageButton Width="25px" Height="25px" ID="ImageButton2" Visible="false" OnClick="Reject9_Click" ToolTip="Reject" runat="server"
                                                                                                    ImageUrl="~/images/reject.png" />

                                                                                                <asp:ImageButton Width="25px" Height="25px" ID="btnSave9" ValidationGroup="saves" ToolTip="Save" runat="server" OnClick="btnSave9_Click"
                                                                                                    ImageUrl="~/images/save-29-1.png" />

                                                                                                <asp:ImageButton Width="25px" Height="25px" ID="btnDeleteTab4" ToolTip="Delete" runat="server" OnClick="btnDeleteTab4_Click"
                                                                                                    ImageUrl="~/images/delete-29.png" />
                                                                                            </ItemTemplate>
                                                                                            <FooterStyle HorizontalAlign="Right" />
                                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                            <%--form 12--%>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">

                                                                                <div class="row">
                                                                                    <asp:GridView ID="grdform12" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                                        AllowPaging="true" PageSize="100" AutoGenerateColumns="false" Font-Names="Arial"
                                                                                        Font-Size="12px" Width="100%" ShowFooter="true" OnRowDataBound="GvForm12_RowDataBound">

                                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                                                        <RowStyle HorizontalAlign="Left" />
                                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                        <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />

                                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Unique Code" Visible="false">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblUniqueCode" runat="server" Text='<%#Eval("UniqueCode") %>' Visible="false"></asp:Label>
                                                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="FC Code" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:DropDownList ID="ddltb" Width="116px" runat="server" class="form-control ">
                                                                                                    </asp:DropDownList>
                                                                                                    <span class="reqfield" style="margin: -10px -5px;">
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel22" Display="Dynamic"
                                                                                                            runat="server" ControlToValidate="ddltb" ErrorMessage="*" ForeColor="Red" ValidationGroup="saves">
                                                                                                        </asp:RequiredFieldValidator></span>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Baseline Receiving Date">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtrecivedate" CssClass="form-control" runat="server" Text='<%#Eval("ReceivedDate") %>'
                                                                                                        onkeypress="return false;"></asp:TextBox>
                                                                                                    <span class="reqfield" style="margin: -10px -5px;">
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel23" Display="Dynamic"
                                                                                                            runat="server" ControlToValidate="txtrecivedate" ErrorMessage="*" ForeColor="Red"
                                                                                                            ValidationGroup="saves">
                                                                                                        </asp:RequiredFieldValidator></span>
                                                                                                    <ajax:CalendarExtender ID="CalendarExtendertxtcollectiondate1" Enabled="true" runat="server"
                                                                                                        Format="dd/MM/yyyy" TargetControlID="txtrecivedate" PopupPosition="BottomRight">
                                                                                                    </ajax:CalendarExtender>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Baseline Collection Date">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtcollectiondate" CssClass="form-control" runat="server" Text='<%#Eval("CollectionDate") %>'
                                                                                                        onkeypress="return false;"></asp:TextBox>
                                                                                                    <span class="reqfield" style="margin: -10px -5px;">
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel24" Display="Dynamic"
                                                                                                            runat="server" ControlToValidate="txtcollectiondate" ErrorMessage="*" ForeColor="Red"
                                                                                                            ValidationGroup="saves">
                                                                                                        </asp:RequiredFieldValidator></span>
                                                                                                    <ajax:CalendarExtender ID="CalendarExtendertxtcollectiondate2" Enabled="true" runat="server"
                                                                                                        Format="dd/MM/yyyy" TargetControlID="txtcollectiondate" PopupPosition="BottomRight">
                                                                                                    </ajax:CalendarExtender>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="No. Of Girls Present">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtnoofgirls" CssClass="form-control" runat="server" Text='<%#Eval("NoOfGirls") %>' MaxLength="3"
                                                                                                        onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                                                    <span class="reqfield" style="margin: -10px -5px;">
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel25" Display="Dynamic"
                                                                                                            runat="server" ControlToValidate="txtnoofgirls" ErrorMessage="*" ForeColor="Red"
                                                                                                            ValidationGroup="saves">
                                                                                                        </asp:RequiredFieldValidator></span>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="No. Of Boys Present">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtNoOfBoys" CssClass="form-control" runat="server" MaxLength="3" Text='<%#Eval("NoOfBoys") %>'
                                                                                                        onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                                                    <span class="reqfield" style="margin: -10px -5px;">
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel26" Display="Dynamic"
                                                                                                            runat="server" ControlToValidate="txtNoOfBoys" ErrorMessage="*" ForeColor="Red"
                                                                                                            ValidationGroup="saves">
                                                                                                        </asp:RequiredFieldValidator></span>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Code">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="txtcode" runat="server" Text='<%#Eval("FormCode") %>' Visible="false"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Status">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="txtcodFStatuse" runat="server" Text='<%#Eval("FStatus") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField Visible="true">
                                                                                                <FooterTemplate>
                                                                                                    <asp:LinkButton ID="btnAddIndividualEducation12" OnClick="btnAddForm12row_Click"
                                                                                                        runat="server" Font-Bold="true" Width="100px" ForeColor="Black" Text="Add New Row" />
                                                                                                </FooterTemplate>
                                                                                                <ItemTemplate>

                                                                                                    <asp:ImageButton Width="25px" Height="25px" Visible="false" ID="ImageButton1" OnClick="btnApprove12_Click" ToolTip="Approve" runat="server"
                                                                                                        ImageUrl="~/images/statusG.png" />

                                                                                                    <asp:ImageButton Width="25px" Height="25px" ID="ImageButton2" Visible="false" OnClick="btnReject12_Click" ToolTip="Reject" runat="server"
                                                                                                        ImageUrl="~/images/reject.png" />

                                                                                                    <asp:ImageButton Width="25px" Height="25px" ID="btnSave10" ValidationGroup="saves" ToolTip="Save" runat="server" OnClick="btnSave10_Click"
                                                                                                        ImageUrl="~/images/save-29-1.png" />


                                                                                                    <asp:ImageButton Width="25px" Height="25px" ID="btnDeleteTab5" ToolTip="Delete" runat="server" OnClick="btnDeleteTab5_Click"
                                                                                                        ImageUrl="~/images/delete-29.png" />
                                                                                                </ItemTemplate>
                                                                                                <FooterStyle HorizontalAlign="Right" />
                                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>

                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">

                                                                                <div class="row">
                                                                                    <asp:GridView ID="grdform13" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                                        AllowPaging="true" PageSize="100" AutoGenerateColumns="false" Font-Names="Arial"
                                                                                        Font-Size="12px" Width="100%" ShowFooter="true" OnRowDataBound="grdform13_RowDataBound">

                                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                                                        <RowStyle HorizontalAlign="Left" />
                                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                        <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />

                                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Unique Code" Visible="false">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblUniqueCode" runat="server" Text='<%#Eval("UniqueCode") %>' Visible="false"></asp:Label>
                                                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="FC Code" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:DropDownList ID="ddltb" Width="116px" runat="server" class="form-control ">
                                                                                                    </asp:DropDownList>
                                                                                                    <span class="reqfield" style="margin: -10px -5px;">
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel22" Display="Dynamic"
                                                                                                            runat="server" ControlToValidate="ddltb" ErrorMessage="*" ForeColor="Red" ValidationGroup="saves">
                                                                                                        </asp:RequiredFieldValidator></span>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>




                                                                                            <asp:TemplateField HeaderText="Endline Collection Date">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtendrecivedate" CssClass="form-control" runat="server" Text='<%#Eval("EndlineReceivingDate") %>'
                                                                                                        onfocus="this.blur();"></asp:TextBox>
                                                                                                    <span class="reqfield" style="margin: -10px -5px;">
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel27" Display="Dynamic"
                                                                                                            runat="server" ControlToValidate="txtendrecivedate" ErrorMessage="*" ForeColor="Red"
                                                                                                            ValidationGroup="saves">
                                                                                                        </asp:RequiredFieldValidator></span>
                                                                                                    <ajax:CalendarExtender ID="CalendarExtendertxtcollectiondate12" Enabled="true" runat="server"
                                                                                                        Format="dd/MM/yyyy" TargetControlID="txtendrecivedate" PopupPosition="BottomRight">
                                                                                                    </ajax:CalendarExtender>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Endline No. of Girls Present">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtendcollectiondate" CssClass="form-control" runat="server" Text='<%#Eval("EndlineCollectionDate") %>'
                                                                                                        onfocus="this.blur();"></asp:TextBox>
                                                                                                    <span class="reqfield" style="margin: -10px -5px;">
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel28" Display="Dynamic"
                                                                                                            runat="server" ControlToValidate="txtendcollectiondate" ErrorMessage="*" ForeColor="Red"
                                                                                                            ValidationGroup="saves">
                                                                                                        </asp:RequiredFieldValidator></span>
                                                                                                    <ajax:CalendarExtender ID="CalendarExtendertxtcollectiondate3" Enabled="true" runat="server"
                                                                                                        Format="dd/MM/yyyy" TargetControlID="txtendcollectiondate" PopupPosition="BottomRight">
                                                                                                    </ajax:CalendarExtender>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Endline No. of Girls Present">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtenoofgirls" CssClass="form-control" runat="server" Text='<%#Eval("EndlineNoofGirls") %>'
                                                                                                        MaxLength="3" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                                                    <span class="reqfield" style="margin: -10px -5px;">
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel29" Display="Dynamic"
                                                                                                            runat="server" ControlToValidate="txtenoofgirls" ErrorMessage="*" ForeColor="Red"
                                                                                                            ValidationGroup="saves">
                                                                                                        </asp:RequiredFieldValidator></span>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Endline No. of Boys Present">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txteNoOfBoys" CssClass="form-control" runat="server" MaxLength="3" Text='<%#Eval("EndlineNoofBoysPresent") %>'
                                                                                                        onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                                                    <span class="reqfield" style="margin: -10px -5px;">
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel30" Display="Dynamic"
                                                                                                            runat="server" ControlToValidate="txteNoOfBoys" ErrorMessage="*" ForeColor="Red"
                                                                                                            ValidationGroup="saves">
                                                                                                        </asp:RequiredFieldValidator></span>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Code">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="txtcode" runat="server" Text='<%#Eval("FormCode") %>' Visible="false"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Status">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="txtcodFStatuse" runat="server" Text='<%#Eval("FStatus") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField Visible="true">
                                                                                                <FooterTemplate>
                                                                                                    <asp:LinkButton ID="btnAddIndividualEducation12" Visible="false" OnClick="btnAddForm12row_Click"
                                                                                                        runat="server" Font-Bold="true" Width="100px" ForeColor="Black" Text="Add New Row" />
                                                                                                </FooterTemplate>
                                                                                                <ItemTemplate>

                                                                                                    <asp:ImageButton Width="25px" Height="25px" Visible="false" ID="ImageButton1" OnClick="btnApprove15_Click" ToolTip="Approve" runat="server"
                                                                                                        ImageUrl="~/images/statusG.png" />

                                                                                                    <asp:ImageButton Width="25px" Height="25px" ID="ImageButton2" Visible="false" OnClick="btnReject15_Click" ToolTip="Reject" runat="server"
                                                                                                        ImageUrl="~/images/reject.png" />

                                                                                                    <asp:ImageButton Width="25px" Height="25px" ID="btnSave10" ToolTip="Save" runat="server" OnClick="btnSave15_Click"
                                                                                                        ImageUrl="~/images/save-29-1.png" />


                                                                                                </ItemTemplate>
                                                                                                <FooterStyle HorizontalAlign="Right" />
                                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>

                                                                            </div>

                                                                            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1" style="padding: 0px 3px 0px 5px;">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                        </asp:Panel>
                                                    </div>
                                                    <%--<div class="col-lg-1">
                                                    </div>--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <!-- /#page-content-wrapper -->
                                </div>
                                <!-- /#wrapper -->
                                <!-- /#wrapper -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnschoolcode" runat="server" />
            <asp:HiddenField ID="hdnvillagecode" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker4').datetimepicker();
        });
    </script>
</asp:Content>
