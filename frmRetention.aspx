<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmRetention.aspx.cs" Culture="en-GB" MasterPageFile="~/Site.master" Inherits="frmRetention" %>

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
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode > 48 && charCode < 57) || charCode == 32 || charCode == 0 || charCode == 9 || charCode == 08)
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
            if (charCode == 46 && charCode == 127) {
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


        function DiscCode(inputtxt, txtid) {
            var phoneno = /^\d{11}$/;
            if (phoneno.test(inputtxt) && inputtxt.length == 11) {
                $("." + txtid).css("border", "solid 1px green")
                return true;
            }
            else {
                $("." + txtid).css("border", "solid 1px red")
                $("." + txtid).val('');
                alert("DISE Code. should be 11 digit");

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
        debugger;
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
                <%--<input type="image" id="ton-new" class="butt" src="Images/close.png"  />
       <div id="div-show-new"></div> --%>
            </div>
            <div class="container-fluid" style="margin-top: 0px; height">
                <%--  <input type="image" id="left-pln" class="left-butt" src="Images/close-29.png" />--%>
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-3" style="padding-right: 0px;">
                        <div class="thumbnail" style="min-height: 730px; width: 225px;">
                            <div style="overflow: auto; margin-top: 35px; height: 689px;">
                                <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="40"
                                    BorderStyle="None" DataKeyNames="schoolcode" GridLines="None" AutoGenerateColumns="false">
                                    <EmptyDataTemplate>
                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                            Data not found
                                        </div>
                                    </EmptyDataTemplate>
                                    <FooterStyle CssClass="FooterStyle" />
                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="44px" />
                                    <RowStyle HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                    <%-- <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                    <AlternatingRowStyle BackColor="#f1f1f1" />--%>
                                    <Columns>
                                        <asp:ButtonField HeaderText="Code " ItemStyle-ForeColor="#333" DataTextField="SchoolCodeId"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="Name " ItemStyle-ForeColor="#333" DataTextField="Name"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="Name" Visible="false" Text="Button" DataTextField="schoolcode"></asp:ButtonField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-9">
                        <div class="row">
                            <%--<div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-6"><h3 class="text-danger" style="margin:0px;">SIC Baseline Data Entry</h3></div>
                    <div class="col-lg-6 col-md-6 col-sm-6">
                    <asp:Button ID="Button1" CssClass="btn btn-info pull-right" runat="server" Text="Save" />
                    <asp:Button ID="Button2" CssClass="btn btn-info pull-right" runat="server" Text="Save" />
                    <asp:Button ID="Button3" CssClass="btn btn-info pull-right" runat="server" Text="Save" />
                    </div>
                    </div>--%>
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading" style="padding: 5px 10px 5px 5px;">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <h3 class="text-danger" style="margin: 0px;">
                                                    <asp:Label ID="lblMain" runat="server" Text="Retention"></asp:Label></h3>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">
                                                <%-- <input type="image" id="ton-new" class="butt" src="Images/search-not-29.png" title="Search" />--%>
                                                <asp:ImageButton ID="btnDelete" CssClass="btn btn-info pull-right"
                                                    ToolTip="Delete" BackColor="#f5f5f5" OnClick="btnDelete_Click" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px; padding: 0px;"
                                                    runat="server" />
                                                <asp:ImageButton ID="btnsave" OnClick="btnsave_Click" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Save" ImageUrl="~/images/save-29-1.png"
                                                    Style="margin-right: 5px; padding: 0px;" runat="server" />
                                                <asp:ImageButton ID="btnAdd" OnClick="btnAdd_Click" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Add" ImageUrl="~/images/add-29-1.png" Style="margin-right: 5px; padding: 0px;"
                                                    runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div id="div-show-new">
                                                <div class="row marg search-bg">
                                                    <div class="form-horizontal">
                                                        <%-- <asp:UpdatePanel runat="server" ID="UpMain" UpdateMode="Conditional">
        <ContentTemplate>--%>


                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-right: 30px;">
                                                                    Year:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" class="form-control ">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                    State:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                                        AutoPostBack="true" class="form-control ">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                    District:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                                        AutoPostBack="true" class="form-control " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                    Block:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                                        class="form-control " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                    Panchayat:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"
                                                                        class="form-control " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
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
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                    School:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlSchool" runat="server" AutoPostBack="true" class="form-control " />
                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                                            ValidationGroup="saves" ControlToValidate="ddlSchool" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%--</ContentTemplate>                                            	 
</asp:UpdatePanel>--%>
                                                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" OnClick="btnSerach_Click" class="btn btn-danger btn-paddd pull-right"
                                                                BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <asp:Panel ID="pnlMain1" Enabled="false" runat="server">

                                            <div class="row">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                    <fieldset class="scheduler-border m-h" runat="server" id="stid" style="padding: 0px!important;">
                                                        <legend class="scheduler-border">Student Enrollment Details</legend>
                                                        <div class="row">
                                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 hidden-xs padd">
                                                                <div class="row">
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd ">
                                                                        <p>
                                                                            <strong>Class</strong><span style="visibility: hidden">Total Enrollment
                                                                                <br />
                                                                                (Previous Session)</span>
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd ">
                                                                        <p>
                                                                            .
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                        <p class="p-height">
                                                                            Class 1
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                        <p class="p-height">
                                                                            Class 2
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                        <p class="p-height">
                                                                            Class 3
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                        <p class="p-height">
                                                                            Class 4
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                        <p class="p-height">
                                                                            Class 5
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                        <p class="p-height">
                                                                            Class 6
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                        <p class="p-height">
                                                                            Class 7
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                        <p class="p-height">
                                                                            Class 8
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                        <p class="p-height">
                                                                            Total
                                                                        </p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 padd">
                                                                <button type="button" class="navbar-toggle flo" data-toggle="collapse" data-target="#myNavbarch1">
                                                                    <span class="fa fa-plus"><strong>Enrollment
                                                                        <br />
                                                                        (Previous Session)</strong></span>
                                                                </button>
                                                                <p class="hidden-xs">
                                                                    <strong>Enrollment<br />
                                                                        (Previous Session)</strong>
                                                                </p>
                                                                <div class="row collapse navbar-collapse inrool" id="myNavbarch1">
                                                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-3 hidden-lg hidden-md hidden-sm padd">
                                                                        <div class="row">
                                                                            <p style="margin: 0px;">
                                                                                <strong>Class</strong>
                                                                            </p>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 hidden-xs padd marg1">
                                                                                <p>
                                                                                    .
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 1
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 2
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 3
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 4
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 5
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 6
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 7
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 8
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Total
                                                                                </p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-9 text-box-padd">
                                                                        <div class="row">
                                                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 text-box-padd">
                                                                                <p class="p-marg">
                                                                                    <strong>Boys</strong>
                                                                                </p>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtB1" MaxLength="3" OnKeyUp="javascript:calculate_totals('tobys11c','tobysto11c');"
                                                                                        onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                        runat="server" class="form-control tobys11c  cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtB2" MaxLength="3" OnKeyUp="javascript:calculate_totals('tobys11c','tobysto11c');"
                                                                                        onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                        runat="server" class="form-control tobys11c cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtB3" MaxLength="3" OnKeyUp="javascript:calculate_totals('tobys11c','tobysto11c');"
                                                                                        onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                        runat="server" class="form-control tobys11c cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtB4" MaxLength="3" OnKeyUp="javascript:calculate_totals('tobys11c','tobysto11c');"
                                                                                        onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                        runat="server" class="form-control tobys11c cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtB5" MaxLength="3" OnKeyUp="javascript:calculate_totals('tobys11c','tobysto11c');"
                                                                                        onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                        runat="server" class="form-control tobys11c  cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtB6" MaxLength="3" OnKeyUp="javascript:calculate_totals('tobys11c','tobysto11c');"
                                                                                        onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                        runat="server" class="form-control tobys11c cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtB7" MaxLength="3" OnKeyUp="javascript:calculate_totals('tobys11c','tobysto11c');"
                                                                                        onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                        runat="server" class="form-control tobys11c cont-form-padd" />
                                                                                    <span class="reqfield">
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtB8" MaxLength="3" OnKeyUp="javascript:calculate_totals('tobys11c','tobysto11c');"
                                                                                        onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                        runat="server" class="form-control tobys11c cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="TxtIPBET1" MaxLength="3" autocomplete="off" BackColor="#C0FFFF"
                                                                                        ondrop="return false;" runat="server" class="form-control tobysto11c cont-form-padd" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 text-box-padd">
                                                                                <p class="p-marg">
                                                                                    <strong>Girls</strong>
                                                                                </p>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtG1" MaxLength="3" OnKeyUp="javascript:calculate_totals('tobysG11c','tobysG1c');"
                                                                                        onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                        runat="server" class="form-control tobysG11c  cont-form-padd " />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtG2" MaxLength="3" OnKeyUp="javascript:calculate_totals('tobysG11c','tobysG1c');"
                                                                                        onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                        runat="server" class="form-control tobysG11c cont-form-padd " />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtG3" MaxLength="3" OnKeyUp="javascript:calculate_totals('tobysG11c','tobysG1c');"
                                                                                        onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                        runat="server" class="form-control tobysG11c cont-form-padd " />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtG4" MaxLength="3" OnKeyUp="javascript:calculate_totals('tobysG11c','tobysG1c');"
                                                                                        onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                        runat="server" class="form-control tobysG11c cont-form-padd " />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtG5" MaxLength="3" OnKeyUp="javascript:calculate_totals('tobysG11c','tobysG1c');"
                                                                                        onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                        runat="server" class="form-control tobysG11c  cont-form-padd " />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtG6" MaxLength="3" OnKeyUp="javascript:calculate_totals('tobysG11c','tobysG1c');"
                                                                                        onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                        runat="server" class="form-control tobysG11c cont-form-padd " />
                                                                                    <span class="reqfield">
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtG7" MaxLength="3" OnKeyUp="javascript:calculate_totals('tobysG11c','tobysG1c');"
                                                                                        onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                        runat="server" class="form-control tobysG11c cont-form-padd " />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtG8" MaxLength="3" OnKeyUp="javascript:calculate_totals('tobysG11c','tobysG1c');"
                                                                                        onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                        runat="server" class="form-control tobysG11c cont-form-padd " />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="TxtIPBGT1" MaxLength="3" autocomplete="off" BackColor="#C0FFFF"
                                                                                        ondrop="return false;" runat="server" class="form-control tobysG1c cont-form-padd" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 padd">
                                                                <button type="button" class="navbar-toggle flo" data-toggle="collapse" data-target="#myNavbarch2">
                                                                    <span class="fa fa-plus"><strong>No. of Students appeared in Final Exam<br />
                                                                        (Previous Session)</strong></span>

                                                                </button>
                                                                <p class="hidden-xs">
                                                                    <strong>No. of Students appeared in Final Exam<br />
                                                                        (Previous Session)</strong>
                                                                </p>
                                                                <div class="row collapse navbar-collapse inrool" id="myNavbarch2">
                                                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-3 hidden-lg hidden-md hidden-sm padd">
                                                                        <div class="row">
                                                                            <p style="margin: 0px;">
                                                                                <strong>Class</strong>
                                                                            </p>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 hidden-xs padd marg1">
                                                                                <p>
                                                                                    .
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 1
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 2
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 3
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 4
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 5
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 6
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 7
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 8
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Total
                                                                                </p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-9 text-box-padd">
                                                                        <div class="row">
                                                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 text-box-padd">
                                                                                <p class="p-marg">
                                                                                    <strong>Boys</strong>
                                                                                </p>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtFB1" MaxLength="3" onchange="javascript:Valdation('tobys11','tobysto11');"
                                                                                        OnKeyUp="javascript:calculate_totals('tobys','tobysto');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys tobys11 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtFB2" MaxLength="3" onchange="javascript:Valdation('tobys2','tobysto2');"
                                                                                        OnKeyUp="javascript:calculate_totals('tobys','tobysto');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys tobys2  cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtFB3" MaxLength="3" onchange="javascript:Valdation('tobys3','tobysto3');"
                                                                                        OnKeyUp="javascript:calculate_totals('tobys','tobysto');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys tobys3 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtFB4" MaxLength="3" onchange="javascript:Valdation('tobys4','tobysto4');"
                                                                                        OnKeyUp="javascript:calculate_totals('tobys','tobysto');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys tobys4 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtFB5" MaxLength="3" onchange="javascript:Valdation('tobys5','tobysto5');"
                                                                                        OnKeyUp="javascript:calculate_totals('tobys','tobysto');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys tobys5 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtFB6" MaxLength="3" onchange="javascript:Valdation('tobys6','tobysto6');"
                                                                                        OnKeyUp="javascript:calculate_totals('tobys','tobysto');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys tobys6 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtFB7" MaxLength="3" OnKeyUp="javascript:calculate_totals('tobys','tobysto');"
                                                                                        onchange="javascript:Valdation('tobys8','tobysto8');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys tobys8 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtFB8" MaxLength="3" onchange="javascript:Valdation('tobys9','tobysto9');"
                                                                                        OnKeyUp="javascript:calculate_totals('tobys','tobysto');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys tobys9 cont-form-padd" />

                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="TxtIET1" MaxLength="3" autocomplete="off" BackColor="#C0FFFF" ondrop="return false;"
                                                                                        runat="server" class="form-control tobysto cont-form-padd" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 text-box-padd">
                                                                                <p class="p-marg">
                                                                                    <strong>Girls</strong>
                                                                                </p>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtFG1" MaxLength="3" onchange="javascript:Valdation('tobysG1','tobystoG1');"
                                                                                        OnKeyUp="javascript:calculate_totals('togrl','togrlsto');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl tobysG1 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtFG2" MaxLength="3" onchange="javascript:Valdation('tobysG2','tobystoG2');"
                                                                                        OnKeyUp="javascript:calculate_totals('togrl','togrlsto');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl tobysG2 cont-form-padd " />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtFG3" MaxLength="3" onchange="javascript:Valdation('tobysG3','tobystoG3');"
                                                                                        OnKeyUp="javascript:calculate_totals('togrl','togrlsto');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl tobysG3 cont-form-padd " />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtFG4" MaxLength="3" onchange="javascript:Valdation('tobysG4','tobystoG4');"
                                                                                        OnKeyUp="javascript:calculate_totals('togrl','togrlsto');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl tobysG4 cont-form-padd " />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtFG5" MaxLength="3" onchange="javascript:Valdation('tobys5','tobysto5');"
                                                                                        OnKeyUp="javascript:calculate_totals('togrl','togrlsto');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl tobysG5 cont-form-padd " />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtFG6" MaxLength="3" onchange="javascript:Valdation('tobysG6','tobystoG6');"
                                                                                        OnKeyUp="javascript:calculate_totals('togrl','togrlsto');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl tobysG6 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtFG7" MaxLength="3" OnKeyUp="javascript:calculate_totals('togrl','togrlsto');"
                                                                                        onchange="javascript:Valdation('tobysG8','tobystoG8');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl tobysG8 cont-form-padd " />
                                                                                    <span class="reqfield">
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtFG8" MaxLength="3" onchange="javascript:Valdation('tobysG9','tobystoG9');"
                                                                                        OnKeyUp="javascript:calculate_totals('togrl','togrlsto');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl tobysG9 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="TxtIE2" MaxLength="3" autocomplete="off" BackColor="#C0FFFF" ondrop="return false;"
                                                                                        runat="server" class="form-control togrlsto cont-form-padd" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 padd">
                                                                <button type="button" class="navbar-toggle flo" data-toggle="collapse" data-target="#myNavbarch3">
                                                                    <span class="fa fa-plus"><strong>No. of Newly Enrolled Girls (Overall)<br />
                                                                        (Previous Session)</strong></span>
                                                                </button>
                                                                <p class="hidden-xs">
                                                                    <strong>No. of Newly Enrolled Girls (Overall)<br />
                                                                        (Previous Session)</strong>
                                                                </p>
                                                                <div class="row collapse navbar-collapse inrool" id="myNavbarch3">
                                                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-3 hidden-lg hidden-md hidden-sm padd">
                                                                        <div class="row">
                                                                            <p style="margin: 0px;">
                                                                                <strong>Class</strong>
                                                                            </p>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 hidden-xs padd marg1">
                                                                                <p>
                                                                                    .
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 1
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 2
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 3
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 4
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 5
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 6
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 7
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Class 8
                                                                                </p>
                                                                            </div>
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padd marg1">
                                                                                <p class="p-height">
                                                                                    Total
                                                                                </p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <%--   <div class="col-lg-12 col-md-12 col-sm-12 col-xs-9 text-box-padd">
                                                            <div class="row">
                                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 text-box-padd">
                                                                    <p class="p-marg">
                                                                        <strong>Girls</strong></p>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="txtOG1" MaxLength="3" onchange="javascript:Valdation('tobys11','tobysto11');"
                                                                            OnKeyUp="javascript:calculate_totals('tobys1','tobysto1');" onkeypress="return isNumberKey(this,event);"
                                                                            autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys1 tobysto11 cont-form-padd" />
                                                                            </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="txtOG2" MaxLength="3" onchange="javascript:Valdation('tobys2','tobysto2');"
                                                                            OnKeyUp="javascript:calculate_totals('tobys1','tobysto1');" onkeypress="return isNumberKey(this,event);"
                                                                            autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys1 tobysto2 cont-form-padd" />
                                                                         </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="txtOG3" MaxLength="3" onchange="javascript:Valdation('tobys3','tobysto3');"
                                                                            OnKeyUp="javascript:calculate_totals('tobys1','tobysto1');" onkeypress="return isNumberKey(this,event);"
                                                                            autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys1 tobysto3 cont-form-padd" />
                                                                              </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="txtOG4" MaxLength="3" onchange="javascript:Valdation('tobys4','tobysto4');"
                                                                            OnKeyUp="javascript:calculate_totals('tobys1','tobysto1');" onkeypress="return isNumberKey(this,event);"
                                                                            autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys1 tobysto4 cont-form-padd" />
                                                                              </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="txtOG5" MaxLength="3" onchange="javascript:Valdation('tobys5','tobysto5');"
                                                                            OnKeyUp="javascript:calculate_totals('tobys1','tobysto1');" onkeypress="return isNumberKey(this,event);"
                                                                            autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys1 tobysto5 cont-form-padd" />
                                                                          </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="txtOG6" MaxLength="3" onchange="javascript:Valdation('tobys6','tobysto6');"
                                                                            OnKeyUp="javascript:calculate_totals('tobys1','tobysto1');" onkeypress="return isNumberKey(this,event);"
                                                                            autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys1 tobysto6 cont-form-padd" />
                                                                              </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="txtOG7" MaxLength="3" onchange="javascript:Valdation('tobys8','tobysto8');"
                                                                            OnKeyUp="javascript:calculate_totals('tobys1','tobysto1');" onkeypress="return isNumberKey(this,event);"
                                                                            autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys1 tobysto8 cont-form-padd" />
                                                                                         </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="txtOG8" MaxLength="3" onchange="javascript:Valdation('tobys9','tobysto9');"
                                                                            OnKeyUp="javascript:calculate_totals('tobys1','tobysto1');" onkeypress="return isNumberKey(this,event);"
                                                                            autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys1 tobysto9 cont-form-padd" />
                                                                                    </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="TxtIA1" MaxLength="3" autocomplete="off" BackColor="#C0FFFF" ondrop="return false;"
                                                                            runat="server" class="form-control tobysto1 cont-form-padd" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 text-box-padd">
                                                                    <p class="p-marg">
                                                                        <strong>Girls</strong></p>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="txtAG1" MaxLength="3" onchange="javascript:Valdation('tobysG1','tobystoG11');"
                                                                            OnKeyUp="javascript:calculate_totals('togrl1','togrlsto1');" onkeypress="return isNumberKey(this,event);"
                                                                            autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl1 tobystoG11 cont-form-padd " />
                                                                                        </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="txtAG2" MaxLength="3" onchange="javascript:Valdation('tobysG2','tobystoG2');"
                                                                            OnKeyUp="javascript:calculate_totals('togrl1','togrlsto1');" onkeypress="return isNumberKey(this,event);"
                                                                            autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl1 tobystoG2 cont-form-padd " />
                                                                            </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="txtAG3" MaxLength="3" onchange="javascript:Valdation('tobysG3','tobystoG3');"
                                                                            OnKeyUp="javascript:calculate_totals('togrl1','togrlsto1');" onkeypress="return isNumberKey(this,event);"
                                                                            autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl1 tobystoG3 cont-form-padd " />
                                                                                   </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="txtAG4" MaxLength="3" onchange="javascript:Valdation('tobysG4','tobystoG4');"
                                                                            OnKeyUp="javascript:calculate_totals('togrl1','togrlsto1');" onkeypress="return isNumberKey(this,event);"
                                                                            autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl1 tobystoG4 cont-form-padd" />
                                                                               </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="txtAG5" MaxLength="3" onchange="javascript:Valdation('tobysG5','tobystoG5');"
                                                                            OnKeyUp="javascript:calculate_totals('togrl1','togrlsto1');" onkeypress="return isNumberKey(this,event);"
                                                                            autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl1 tobystoG5 cont-form-padd" />
                                                                             </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="txtAG6" MaxLength="3" onchange="javascript:Valdation('tobysG6','tobystoG6');"
                                                                            OnKeyUp="javascript:calculate_totals('togrl1','togrlsto1');" onkeypress="return isNumberKey(this,event);"
                                                                            autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl1 tobystoG6 cont-form-padd" />
                                                                                  </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="txtAG7" MaxLength="3" OnKeyUp="javascript:calculate_totals('togrl1','togrlsto1');"
                                                                            onchange="javascript:Valdation('tobysG8','tobystoG8');" onkeypress="return isNumberKey(this,event);"
                                                                            autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl1 tobystoG8 cont-form-padd" />
                                                                              </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="txtAG8" MaxLength="3" onchange="javascript:Valdation('tobysG9','tobystoG9');"
                                                                            OnKeyUp="javascript:calculate_totals('togrl1','togrlsto1');" onkeypress="return isNumberKey(this,event);"
                                                                            autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl1 tobystoG9 cont-form-padd" />
                                                                              </div>
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                        <asp:TextBox ID="TxtIA2" MaxLength="3" autocomplete="off" BackColor="#C0FFFF" ondrop="return false;"
                                                                            runat="server" class="form-control togrlsto1 cont-form-padd" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                                    --%>

                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-9 text-box-padd">
                                                                        <div class="row">
                                                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 text-box-padd">
                                                                                <p class="p-marg">
                                                                                    <strong>Girls</strong>
                                                                                </p>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtOG1" MaxLength="3"
                                                                                        OnKeyUp="javascript:calculate_totals('tobys1','tobysto1');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys1 tobysto11 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtOG2" MaxLength="3"
                                                                                        OnKeyUp="javascript:calculate_totals('tobys1','tobysto1');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys1 tobysto2 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtOG3" MaxLength="3"
                                                                                        OnKeyUp="javascript:calculate_totals('tobys1','tobysto1');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys1 tobysto3 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtOG4" MaxLength="3"
                                                                                        OnKeyUp="javascript:calculate_totals('tobys1','tobysto1');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys1 tobysto4 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtOG5" MaxLength="3"
                                                                                        OnKeyUp="javascript:calculate_totals('tobys1','tobysto1');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys1 tobysto5 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtOG6" MaxLength="3"
                                                                                        OnKeyUp="javascript:calculate_totals('tobys1','tobysto1');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys1 tobysto6 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtOG7" MaxLength="3"
                                                                                        OnKeyUp="javascript:calculate_totals('tobys1','tobysto1');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys1 tobysto8 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtOG8" MaxLength="3"
                                                                                        OnKeyUp="javascript:calculate_totals('tobys1','tobysto1');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control tobys1 tobysto9 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="TxtIA1" MaxLength="3" autocomplete="off" BackColor="#C0FFFF" ondrop="return false;"
                                                                                        runat="server" class="form-control tobysto1 cont-form-padd" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 text-box-padd">
                                                                                <p class="p-marg">
                                                                                    <strong>Girls</strong>
                                                                                </p>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtAG1" MaxLength="3"
                                                                                        OnKeyUp="javascript:calculate_totals('togrl1','togrlsto1');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl1 tobystoG11 cont-form-padd " />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtAG2" MaxLength="3"
                                                                                        OnKeyUp="javascript:calculate_totals('togrl1','togrlsto1');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl1 tobystoG2 cont-form-padd " />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtAG3" MaxLength="3"
                                                                                        OnKeyUp="javascript:calculate_totals('togrl1','togrlsto1');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl1 tobystoG3 cont-form-padd " />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtAG4" MaxLength="3"
                                                                                        OnKeyUp="javascript:calculate_totals('togrl1','togrlsto1');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl1 tobystoG4 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtAG5" MaxLength="3"
                                                                                        OnKeyUp="javascript:calculate_totals('togrl1','togrlsto1');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl1 tobystoG5 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtAG6" MaxLength="3"
                                                                                        OnKeyUp="javascript:calculate_totals('togrl1','togrlsto1');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl1 tobystoG6 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtAG7" MaxLength="3" OnKeyUp="javascript:calculate_totals('togrl1','togrlsto1');"
                                                                                        onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl1 tobystoG8 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="txtAG8" MaxLength="3"
                                                                                        OnKeyUp="javascript:calculate_totals('togrl1','togrlsto1');" onkeypress="return isNumberKey(this,event);"
                                                                                        autocomplete="off" ondrop="return false;" runat="server" class="form-control togrl1 tobystoG9 cont-form-padd" />
                                                                                </div>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-box-padd  marg1">
                                                                                    <asp:TextBox ID="TxtIA2" MaxLength="3" autocomplete="off" BackColor="#C0FFFF" ondrop="return false;"
                                                                                        runat="server" class="form-control togrlsto1 cont-form-padd" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                    </fieldset>
                                                    <asp:Label ID="lblStudentId" Visible="false"
                                                        runat="server" />
                                                </div>


                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="thumbnail" style="float: left; width: 100%;">
                                        <div class="col-lg-4 col-md-4 col-xs-12 col-sm-6  col-lg-offset-4 col-md-offset-4 col-sm-offset-3 col-xs-offset-0  ">
                                            <asp:ImageButton ID="btnSUmbit" ToolTip="Save" ValidationGroup="saves"
                                                ImageUrl="~/images/Sumbit.jpg" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker4').datetimepicker();
        });
    </script>

</asp:Content>

