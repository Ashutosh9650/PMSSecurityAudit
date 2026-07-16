<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmInfluencerProfile.aspx.cs" Culture="en-GB" MasterPageFile="~/Site.master"
    Inherits="frmInfluencerProfile" %>

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
            if ($("." + txtid).val() == 0) {
                $("." + txtid).css("border", "solid 1px green")
                return true;
            }
            else if (phoneno.test(inputtxt) && inputtxt.length == 10) {
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
    </script>
    <style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
        }

        .padd {
            padding-left: 15px;
            padding-right: 15px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="mkiiainpnl121">
        <ContentTemplate>

            <div class="container-fluid">
                <%--<input type="image" id="ton-new" class="butt" src="Images/close.png"  />
       <div id="div-show-new"></div> --%>
            </div>

            <div class="container-fluid" style="margin-top: 0px;">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-3" style="padding-right: 0px;">
                        <div class="thumbnail" style="min-height: 510px; width: 228px;">
                            <div style="padding-top: 3px;">
                                -<span style="float: left">
                                    <asp:Label ID="lblsearch" runat="server" Text="Search:" ForeColor="Black"></asp:Label></span>
                                <span style="float: right; padding-right: 1px;">
                                    <asp:TextBox ID="txtSearchName" runat="server" OnTextChanged="txtSearchName_Click" AutoPostBack="true" CssClass="form-control col-lg-1"></asp:TextBox></span>
                            </div>
                            <div style="overflow: auto; margin-top: 35px; height: 503px;">
                                <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="40"
                                    BorderStyle="None" DataKeyNames="UniqueCode" GridLines="None" AutoGenerateColumns="false"
                                    OnRowCommand="GVMain_OnRowCommand" OnPageIndexChanging="GV_Project_PageIndexChanging">
                                    <EmptyDataTemplate>
                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                            Data not found
                                        </div>
                                    </EmptyDataTemplate>
                                    <FooterStyle CssClass="FooterStyle" />
                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="44px" />
                                    <RowStyle HorizontalAlign="Left" />
                                    <PagerStyle CssClass="paging" />
                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                    <%-- <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                    <AlternatingRowStyle BackColor="#f1f1f1" />--%>
                                    <Columns>
                                        <asp:ButtonField HeaderText="Mamber Code " ItemStyle-ForeColor="#333" DataTextField="TBCode"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="Mamber Name " ItemStyle-ForeColor="#333" DataTextField="TBName"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="Name" Visible="false" Text="Button" DataTextField="UniqueCode"></asp:ButtonField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-9">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading" style="padding: 2px 0px;">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <h3 class="text-danger" style="margin: 0px;">Advisory Council Member Profile  </h3>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 " style="padding-right: 10px;">

                                                <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right"
                                                    ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px; padding: 0px;"
                                                    runat="server" />
                                                <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnsave_Click" ValidationGroup="saves"
                                                    Style="margin-right: 5px; padding: 0px;" runat="server" />
                                                <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Add" ImageUrl="~/images/add-29-1.png" OnClick="btnAdd_Click" Style="margin-right: 5px; padding: 0px;"
                                                    runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div style="padding: 5px 15px;">
                                                <div class="row marg search-bg">
                                                    <div class="form-horizontal">
                                                        <%-- <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>--%>

                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei" style="text-align: left;">
                                                                    Year:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" class="form-control ">
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
                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" InitialValue="0" runat="server"
                                                                            Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlState" ErrorMessage="*"
                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
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

                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" InitialValue="0" runat="server"
                                                                            Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlDistrict" ErrorMessage="*"
                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
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

                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" InitialValue="0" runat="server"
                                                                            Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlBlock" ErrorMessage="*"
                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
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
                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" InitialValue="0" runat="server"
                                                                            Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlPanchayat" ErrorMessage="*"
                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
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
                                                                            ValidationGroup="saves" ControlToValidate="ddlVillage" InitialValue="0" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%--</ContentTemplate>
</asp:UpdatePanel>--%>
                                                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 ">
                                                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd"
                                                                BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <asp:Panel ID="pnlMain" Enabled="false" runat="server">
                                                    <div class="form-horizontal">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" style="padding: 0px 15px 0px 5px;">
                                                            <fieldset class="scheduler-border">
                                                                <legend class="scheduler-border">Personal Details </legend>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Member Type</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddInfluencerType" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddInfluencerTyp_SelectedIndexChanged" runat="server" class="form-control">
                                                                        </asp:DropDownList>
                                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldVrrr3" InitialValue="0" runat="server"
                                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddInfluencerType" ErrorMessage="*"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group" id="divType" runat="server" visible="false">
                                                                    <asp:Label class="control-label col-sm-4" runat="server" ID="Label2" Text="Replacement Influencer Name"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlInfuName" runat="server" class="form-control">
                                                                        </asp:DropDownList>

                                                                        </span>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Member Name</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtName" MaxLength="30" autocomplete="off" ondrop="return false;"
                                                                            onkeypress="return onlyAlphabets(event,this);" runat="server" class="form-control" />
                                                                        <span class="reqfield">
                                                                            <asp:RequiredFieldValidator ID="rvtxtSchoolName" runat="server" Display="Dynamic"
                                                                                ValidationGroup="saves" ControlToValidate="txtName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Member Code</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtIDNO" Enabled="false" runat="server" class="form-control" />
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Mobile Number</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtContact" OnKeyUp="javascript:inputtxt();" runat="server" MaxLength="10"
                                                                            onkeypress="return isNumberKey(this,event);" onchange="javascript: phonenumber(this.value,'TeContact1');"
                                                                            autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                        <span class="reqfield">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                                                ValidationGroup="saves" ControlToValidate="txtContact" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Designation</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlDesignation" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged" class="form-control">
                                                                        </asp:DropDownList>
                                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                            <asp:RequiredFieldValidator ID="Required5FieldValidator8" InitialValue="0" runat="server"
                                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlDesignation" ErrorMessage="*"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" id="divOther" runat="server" visible="false">
                                                                    <asp:Label class="control-label col-sm-4" runat="server" ID="Label3" Text="Others"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtDegOther" MaxLength="30" autocomplete="off" ondrop="return false;"
                                                                            onkeypress="return onlyAlphabets(event,this);" runat="server" class="form-control" />
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Father Name/Husband Name</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtFatherName" onkeypress="return onlyAlphabets(event,this);" runat="server"
                                                                            MaxLength="30" class="form-control" />

                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Social Category</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlCategory" runat="server" class="form-control">
                                                                        </asp:DropDownList>
                                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0" runat="server"
                                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlCategory" ErrorMessage="*"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>


                                                            </fieldset>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" style="padding:0px 0px 0px 0px;;">
                                                            <fieldset class="scheduler-border">
                                                                <legend class="scheduler-border">Personal Details </legend>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Gender</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlGender" runat="server" class="form-control">
                                                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                            <asp:ListItem Value="1">1-Male </asp:ListItem>
                                                                            <asp:ListItem Value="2">2-Female</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" InitialValue="0" runat="server"
                                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlGender" ErrorMessage="*"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        DOB Available</label>
                                                                    <div class="col-sm-8">
                                                                        <div style="width: 100%;">
                                                                            <span style="float: left; width: 42%;">
                                                                                <asp:DropDownList ID="ddlDob" runat="server" AutoPostBack="true" Style="width: 85%;"
                                                                                    OnSelectedIndexChanged="ddlDob_SelectedIndexChanged" class="form-control">
                                                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Yes </asp:ListItem>
                                                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </span><span style="float: left; width: 19%; padding-top: 1px;">
                                                                                <asp:Label runat="server" ID="lblAge" class="control-label col-sm-4" Text="Age"></asp:Label>
                                                                            </span>
                                                                            <asp:TextBox ID="txtAge" runat="server" Width="38%" MaxLength="2" onkeypress="return isNumberKey(this,event);"
                                                                                autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <asp:Label class="control-label col-sm-4" runat="server" ID="lblDob" Text="Date"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <div class="input-group">
                                                                            <asp:TextBox runat="server" ID="txtDate" autocomplete="off" ondrop="return false;"
                                                                                class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                            <ajax:CalendarExtender ID="CalendarExtenderTourdate" runat="server" Enabled="True"
                                                                                Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight">
                                                                            </ajax:CalendarExtender>
                                                                            <asp:RequiredFieldValidator ID="ReqTxtDate" runat="server" ControlToValidate="txtDate"
                                                                                Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                                                SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Education</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlEducation" runat="server" class="form-control">
                                                                        </asp:DropDownList>
                                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server"
                                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlEducation" ErrorMessage="*"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Member occupation</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddloccu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddloccu_SelectedIndexChanged" class="form-control">
                                                                        </asp:DropDownList>
                                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="0" runat="server"
                                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddloccu" ErrorMessage="*"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" id="divOccOther" runat="server" visible="false">
                                                                    <asp:Label class="control-label col-sm-4" runat="server" ID="Label4" Text="Others"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtOccOther" MaxLength="30" autocomplete="off" ondrop="return false;"
                                                                            onkeypress="return onlyAlphabets(event,this);" runat="server" class="form-control" />

                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Active Status</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlWorkEx" AutoPostBack="true" OnSelectedIndexChanged="ddlWork_SelectedIndexChanged"
                                                                            runat="server" class="form-control">
                                                                        </asp:DropDownList>
                                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" runat="server"
                                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlWorkEx" ErrorMessage="*"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>

                                                                </div>
                                                                <div class="form-group" runat="server" id="DivActive" visible="false">
                                                                    <asp:Label runat="server" ID="Label5" class="control-label col-sm-4" Text="Membership Date:"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox runat="server" ID="txtActivieDate" autocomplete="off" ondrop="return false;"
                                                                            class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                            TargetControlID="txtActivieDate" OnClientDateSelectionChanged="arrivaldatecheck"
                                                                            PopupPosition="BottomRight">
                                                                        </ajax:CalendarExtender>
                                                                    </div>
                                                                </div>


                                                                <div class="form-group" runat="server" id="rdate" visible="false">
                                                                    <asp:Label runat="server" ID="Label7" class="control-label col-sm-4" Text="Inactive Date:"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox runat="server" ID="txtDropDate" autocomplete="off" ondrop="return false;"
                                                                            class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                        <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                            TargetControlID="txtDropDate" OnClientDateSelectionChanged="arrivaldatecheck"
                                                                            PopupPosition="BottomRight">
                                                                        </ajax:CalendarExtender>
                                                                    </div>
                                                                </div>




                                                                <div class="form-group" id="rregion" runat="server" visible="false">
                                                                    <asp:Label runat="server" ID="Label1" class="control-label col-sm-4" Text="Inactive Reason:"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtReason" runat="server" MaxLength="30" onkeypress="return onlyAlphabets(event,this);"
                                                                            autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                    </div>
                                                                </div>



                                                            </fieldset>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <div class="row">
                                                    <div class="thumbnail" style="float: left; width: 100%;">
                                                        <div class="col-lg-4 col-md-4 col-xs-12 col-sm-6  col-lg-offset-4 col-md-offset-4 col-sm-offset-3 col-xs-offset-0  ">
                                                            <asp:ImageButton Visible="false" ID="btnSUmbit" ToolTip="Save" OnClick="btnSumbit_Click" ValidationGroup="saves"
                                                                ImageUrl="~/images/Sumbit.jpg" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
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
            <asp:Label ID="HdnStartYear" Visible="false" runat="server" />
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
