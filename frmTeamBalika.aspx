<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTeamBalika.aspx.cs" Culture="en-GB" MasterPageFile="~/Site.master"
    Inherits="frmTeamBalika" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">


    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
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
        $(document).ready(function () {
            $("[id$=txtJoingDate]").datepicker({ maxDate: new Date() });
            $("[id$=txtJoingDate]").datepicker({
                dateFormat: 'dd/mm/yy'
            });
            $("[id$=txtJoingDate]").datepicker();

            $("[id$=txtAlumniDate]").datepicker({
                dateFormat: 'dd/mm/yy',
                maxDate: new Date()
            });


            $("[id$=txtAlumniDate]").datepicker();
            $('#datepickers-container').css('z-index', 1045);
        });

    </script>
    <script type="text/javascript">
        function loadJSFunction() {
            $("[id$=txtJoingDate]").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                minDate: '-60Y',
                yearRange: '1965:2026',
                defaultDate: new Date()

            });

            $("[id$=txtJoingDate]").datepicker();

            $("[id$=txtAlumniDate]").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                minDate: '-60Y',
                yearRange: '1965:2026',
                defaultDate: new Date()
            });

            $("[id$=txtAlumniDate]").datepicker();


            $("[id$=txtDropDate]").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                minDate: '-60Y',
                yearRange: '1965:2026',
                defaultDate: new Date()
            });

            $("[id$=txtDropDate]").datepicker();


            $("[id$=txtDate]").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                minDate: '-60Y',
                yearRange: '1965:2026',
                defaultDate: new Date()
            });

            $("[id$=txtDate]").datepicker();






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




        //$type = Sys.UI.Point = function Point(x, y) {
        //    /// <summary locid="M:J#Sys.UI.Point.#ctor"></summary>
        //    /// <param name="x" type="Number" integer="true"></param>
        //    /// <param name="y" type="Number" integer="true"></param>
        //    /// <field name="x" type="Number" integer="true" locid="F:J#Sys.UI.Point.x"></field>
        //    /// <field name="y" type="Number" integer="true" locid="F:J#Sys.UI.Point.y"></field>
        //    var e = Function._validateParams(arguments, [
        //        { name: "x", type: Number, integer: true },
        //        { name: "y", type: Number, integer: true }
        //    ]);
        //    if (e) throw e;
        //    this.x = x;
        //    this.y = y;
        //}


        //function DomElement$getLocation(element)
        //var ex, ownerDoc = element.ownerDocument, documentElement = ownerDoc.documentElement,
        //    offsetX = Math.round(clientRect.left) + (documentElement.scrollLeft || (ownerDoc.body ? ownerDoc.body.scrollLeft : 0)),
        //    offsetY = Math.round(clientRect.top) + (documentElement.scrollTop || (ownerDoc.body ? ownerDoc.body.scrollTop : 0));

        //return new Sys.UI.Point(offsetX, offsetY);
    </script>

    <%-- <script type="text/javascript" language="javascript">
        //There's a bug in Microsoft's Ajax script that stops the modal popups from working
        //This overrides the the code that causes the error
        if (typeof (Sys) !== 'undefined') {
            Sys.UI.Point = function Sys$UI$Point(x, y) {

                x = Math.round(x);
                y = Math.round(y);

                var e = Function._validateParams(arguments, [
                    { name: "x", type: Number, integer: true },
                    { name: "y", type: Number, integer: true }
                ]);
                if (e) throw e;
                this.x = x;
                this.y = y;
            }
        }
    </script>--%>
    <style type="text/css">
        .ui-datepicker {
            z-index: 99 !important
        }
        /* #ui-datepicker-div
    {
        z-index: 9999999;
    }
        .ajax__calendar_container
        {
            z-index: 1045;
        }*/
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
                        <div class="thumbnail" style="min-height: 904px; width: 228px;">
                            <div style="padding-top: 0px;">
                                <%--<span style="float:left"> <asp:Label ID="lblsearch" runat="server" Text="Search:" ForeColor="Black"></asp:Label></span>--%>
                                <span style="float: right; padding-right: 1px;">
                                    <asp:TextBox ID="txtSearchName" Visible="false" runat="server" OnTextChanged="txtSearchName_Click" AutoPostBack="true" CssClass="form-control col-lg-1"></asp:TextBox></span>
                            </div>
                            <div style="overflow: auto; margin-top: 0px; height: 815px;">
                                <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="40"
                                    BorderStyle="None" DataKeyNames="UniqueCode" GridLines="None" AutoGenerateColumns="false"
                                    OnRowCommand="GVMain_OnRowCommand" OnPageIndexChanging="GV_Project_PageIndexChanging" CssClass="table table-striped">
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
                                        <asp:ButtonField HeaderText="Code " ItemStyle-ForeColor="#333" DataTextField="TBCode"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="Name " ItemStyle-ForeColor="#333" DataTextField="TBName"
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
                                    <div class="panel-heading" style="padding: 5px;">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <h3 class="text-danger" style="margin: 0px;">Team Balika Profile Format</h3>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 ">
                                                <%-- <input type="image" id="" class="butt" src="Images/search-not-29.png" title="Search" />--%>

                                                <button type="button" id="ton-new" class="btn btn-primary" style="float: right; position: relative; right: 1px; color: #fff; background-color: #337ab7; border-color: #2e6da4;">
                                                    <i class="fa fa-bars"></i>
                                                </button>
                                                <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right"
                                                    ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px; padding: 0px;"
                                                    runat="server" />
                                                <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnsave_Click" ValidationGroup="saves"
                                                    Style="margin-right: 5px; padding: 0px;" runat="server" />
                                                <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Add" ImageUrl="~/images/add-29-1.png" OnClick="btnAdd_Click" Style="margin-right: 5px; padding: 0px;"
                                                    runat="server" />


                                                 <asp:ImageButton ID="ImageButton1" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Add" ImageUrl="~/images/add-29-1.png" OnClick="btnAdd2_Click" Style="margin-right: 5px; padding: 0px;"
                                                    runat="server" />

                                               
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div id="div-show-new" style="text-align: left; width: calc(100% - 30px); right: 15px;">
                                                <div class="row marg search-bg" style="padding-top: 15px;">
                                                    <div class="form-horizontal">
                                                        <%-- <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>--%>

                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
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
                                                        <%--</ContentTemplate>
</asp:UpdatePanel>--%>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd"
                                                                BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12" style="padding-top: 10px;">
                                                <asp:Panel ID="pnlMain" Enabled="false" runat="server">
                                                    <div class="form-horizontal rows">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <fieldset class="scheduler-border">
                                                                <legend class="scheduler-border">Personal Details </legend>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        TB Code</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtIDNO" Enabled="false" runat="server" class="form-control" />
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Name of Team Balika</label>
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
                                                                        Contact Number</label>
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
                                                                        Alternate Mobile Number</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtxAlternate" OnKeyUp="javascript:inputtxt();" runat="server" MaxLength="10"
                                                                            onkeypress="return isNumberKey(this,event);" onchange="javascript: phonenumber(this.value,'TeContact2');"
                                                                            autocomplete="off" ondrop="return false;" class="form-control TeContact2 " />
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        TB have Smartphone</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlSmart" runat="server" class="form-control">
                                                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                            <asp:ListItem Value="1">1-Yes </asp:ListItem>
                                                                            <asp:ListItem Value="2">2-No</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" InitialValue="0" runat="server"
                                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlSmart" ErrorMessage="*"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Father Name</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtFatherName" onkeypress="return onlyAlphabets(event,this);" runat="server"
                                                                            MaxLength="30" class="form-control" />

                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Mother Name</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtMotherName" onkeypress="return onlyAlphabets(event,this);" runat="server"
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
                                                                        Physical Status</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlPhysicalStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlSp_SelectedIndexChanged" runat="server" class="form-control">
                                                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                            <asp:ListItem Value="1">Specially Abled </asp:ListItem>
                                                                            <asp:ListItem Value="2">Not Applicable</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" InitialValue="0" runat="server"
                                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlPhysicalStatus" ErrorMessage="*"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                                 <div class="form-group" runat="server" visible="false" id="divSp">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                         Type of Specially Abled</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlSpecially" runat="server" class="form-control">
                                                                           
                                                                        </asp:DropDownList>
                                                                      
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

                                                                            <%-- <ajax:CalendarExtender ID="CalendarExtenderTourdate" runat="server" Enabled="True"
                                                                Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight">
                                                            </ajax:CalendarExtender>--%>
                                                                            <asp:CompareValidator ID="CompareValidator1" ValidationGroup="saves" Display="Dynamic" ForeColor="Red" runat="server"
                                                                                ControlToValidate="txtDate" ControlToCompare="txtEndDate" Operator="LessThanEqual"
                                                                                Type="Date" ErrorMessage="Should not be future date"></asp:CompareValidator>
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
                                                                        <asp:DropDownList ID="ddlEducation" AutoPostBack="true" OnSelectedIndexChanged="ddlSpecialization_SelectedIndexChanged" runat="server" class="form-control">
                                                                        </asp:DropDownList>
                                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server"
                                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlEducation" ErrorMessage="*"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group" runat="server" id="divSpc" visible="false">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Specialization</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlSpecialization"  runat="server" class="form-control">
                                                                        </asp:DropDownList>
                                                                        
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Family Occupation</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddloccu" runat="server" class="form-control">
                                                                        </asp:DropDownList>
                                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="0" runat="server"
                                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddloccu" ErrorMessage="*"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>

                                                            </fieldset>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <fieldset class="scheduler-border" style="min-height: 626px;">
                                                                <legend class="scheduler-border">Enrolment status </legend>
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel ID="Image" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                Image</label>
                                                                            <div class="col-sm-8">
                                                                                <asp:FileUpload ID="FileuploadAttach" runat="server" Width="160px" Font-Size="Smaller"
                                                                                    TabIndex="16" />
                                                                                <asp:Image ID="imgMKS" runat="server" Height="80px" Width="100px" BorderColor="Black"
                                                                                    BorderStyle="Ridge" BorderWidth="1px" />
                                                                            </div>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="btnsave" />
                                                                            <asp:PostBackTrigger ControlID="btnSUmbit" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Reason of Becoming</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlReason" runat="server" class="form-control">
                                                                        </asp:DropDownList>
                                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" InitialValue="0" runat="server"
                                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlReason" ErrorMessage="*"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Recruitment Process</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlSours" runat="server" class="form-control">
                                                                        </asp:DropDownList>
                                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" InitialValue="0" runat="server"
                                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlSours" ErrorMessage="*"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Work Experience</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlWorkEx" AutoPostBack="true" OnSelectedIndexChanged="ddlWork_SelectedIndexChanged"
                                                                            runat="server" class="form-control">
                                                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                            <asp:ListItem Value="1">Yes </asp:ListItem>
                                                                            <asp:ListItem Value="2">No</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Year</label>
                                                                    <div class="col-sm-8">
                                                                        <div style="width: 100%;">
                                                                            <span style="float: left; width: 42%;">
                                                                                <asp:TextBox ID="txtDuartion" Enabled="false" Style="width: 85%;" runat="server"
                                                                                    MaxLength="2" onkeypress="return isNumberKey(this,event);" autocomplete="off"
                                                                                    ondrop="return false;" class="form-control TeContact1 " />
                                                                            </span><span style="float: left; width: 19%; padding-top: 6px;">
                                                                                <label>
                                                                                    Month:</label>
                                                                            </span>
                                                                            <asp:TextBox ID="txtMonth" Enabled="false" Width="39%" runat="server" MaxLength="2"
                                                                                onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;"
                                                                                class="form-control TeContact1 " />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="Label5" class="control-label col-sm-4" Text="Status:"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlWorkingStatus" OnSelectedIndexChanged="ddlWorkingStatus_SelectedIndexChanged" AutoPostBack="true"
                                                                            runat="server" class="form-control">
                                                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                            <asp:ListItem Value="1">Working </asp:ListItem>
                                                                            <asp:ListItem Value="2">DropOut</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group" runat="server" id="Resone" visible="false">
                                                                    <asp:Label runat="server" ID="Label6" class="control-label col-sm-4" Text="Reason:"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlStatusReasone" OnSelectedIndexChanged="ddlStatusReasone_SelectedIndexChanged" AutoPostBack="true"
                                                                            runat="server" class="form-control">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" runat="server" id="rdate" visible="false">
                                                                    <asp:Label runat="server" ID="Label7" class="control-label col-sm-4" Text="Dropout Date:"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox runat="server" ID="txtDropDate" autocomplete="off" ondrop="return false;"
                                                                            class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                        <asp:CompareValidator ID="CompareValidator2" ValidationGroup="saves" Display="Dynamic" ForeColor="Red" runat="server"
                                                                            ControlToValidate="txtDropDate" ControlToCompare="txtEndDate" Operator="LessThanEqual"
                                                                            Type="Date" ErrorMessage="Should not be future date"></asp:CompareValidator>

                                                                    
                                                                    </div>
                                                                </div>

                                                                  <div class="form-group" runat="server" id="divJob"  visible="false">
                                                                    <asp:Label runat="server" ID="Label11" class="control-label col-sm-4" Text="Job:"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtJob" runat="server" MaxLength="20" onkeypress="return onlyAlphabets(event,this);"
                                                                            autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                    </div>
                                                                </div>
                                                                 <div class="form-group" runat="server" id="divbus"  visible="false">
                                                                    <asp:Label runat="server" ID="Label12" class="control-label col-sm-4" Text="Business:"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtBus"  runat="server" MaxLength="20" onkeypress="return onlyAlphabets(event,this);"
                                                                            autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" runat="server" id="divJobOp"  visible="false">
                                                                    <asp:Label runat="server" ID="Label13" class="control-label col-sm-4" Text="Job Opportunity Through:"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlJobOpportunity" OnSelectedIndexChanged="ddlOther_SelectedIndexChanged" AutoPostBack="true"
                                                                            runat="server" class="form-control">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                                 <div class="form-group" runat="server" id="divOtherJob" visible="false">
                                                                    <asp:Label runat="server" ID="Label14" class="control-label col-sm-4" Text="Other (Specify):"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtotherjob" runat="server" MaxLength="50" onkeypress="return onlyAlphabets(event,this);"
                                                                            autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" runat="server" id="EmpID" visible="false">
                                                                    <asp:Label runat="server" ID="Label8" class="control-label col-sm-4" Text="EG Employee ID"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtEmployeeID" runat="server" MaxLength="30"
                                                                            autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="Label1" class="control-label col-sm-4" Text="Expectation:"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtExp" runat="server" MaxLength="30" onkeypress="return onlyAlphabets(event,this);"
                                                                            autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                    </div>
                                                                </div>


                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="Label2" class="control-label col-sm-4" Text="Ambition:"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtAbv" runat="server" MaxLength="30" onkeypress="return onlyAlphabets(event,this);"
                                                                            autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <asp:Label class="control-label col-sm-4" runat="server" ID="Label3" Text="Joining date"></asp:Label>
                                                                    <div class="col-sm-8">

                                                                        <asp:TextBox runat="server" ID="txtJoingDate" AutoPostBack="true" autocomplete="off" ondrop="return false;" OnTextChanged="txtJoingDate_OnTextChanged"
                                                                            class="form-control" onkeypress="return false;"></asp:TextBox>

                                                                        <%-- <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="txtJoingDate" OnClientDateSelectionChanged="arrivaldatecheck"
                                                                PopupPosition="BottomRight">
                                                            </ajax:CalendarExtender>--%>
                                                                        <asp:CompareValidator ID="CompareValidator3" ValidationGroup="saves" Display="Dynamic" ForeColor="Red" runat="server"
                                                                            ControlToValidate="txtJoingDate" ControlToCompare="txtEndDate" Operator="LessThanEqual"
                                                                            Type="Date" ErrorMessage="Should not be future date"></asp:CompareValidator>

                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtJoingDate"
                                                                            Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                                            SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>

                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="Label4" class="control-label col-sm-4" Text="No of Traning Days"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtday" Enabled="false" runat="server" MaxLength="30" onkeypress="return onlyAlphabets(event,this);"
                                                                            autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                    </div>
                                                                </div>


                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        TB Recruited For
                                                                    </label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddltbRecruited" runat="server" class="form-control">
                                                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                            <asp:ListItem Value="1">Enrolment </asp:ListItem>
                                                                            <asp:ListItem Value="2">Learning</asp:ListItem>
                                                                            <asp:ListItem Value="3">Enrolment + Learning</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" runat="server"
                                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddltbRecruited" ErrorMessage="*"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group" runat="server" visible="false" id="divAlumni">
                                                                    <asp:Label runat="server" ID="Label9" class="control-label col-sm-4" Text="Is Team Balika Alumni:"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlAlumni" OnSelectedIndexChanged="ddlAlumni_SelectedIndexChanged" AutoPostBack="true"
                                                                            runat="server" class="form-control">
                                                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                            <asp:ListItem Value="1">Yes </asp:ListItem>
                                                                            <asp:ListItem Value="2">No</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group" visible="false" runat="server" id="divAlumni1">
                                                                    <asp:Label class="control-label col-sm-4" runat="server" ID="Label10" Text="Team Balika Alumni Date"></asp:Label>
                                                                    <div class="col-sm-8">
                                                                        <div class="input-group">
                                                                            <asp:TextBox runat="server" ID="txtAlumniDate" autocomplete="off" ondrop="return false;"
                                                                                class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                            <asp:CompareValidator ID="CompareValidator4" ValidationGroup="saves" Display="Dynamic" ForeColor="Red" runat="server"
                                                                                ControlToValidate="txtAlumniDate" ControlToCompare="txtEndDate" Operator="LessThanEqual"
                                                                                Type="Date" ErrorMessage="Should not be future date"></asp:CompareValidator>

                                                                            <%--<ajax:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="txtAlumniDate" OnClientDateSelectionChanged="arrivaldatecheck"
                                                                PopupPosition="BottomRight">
                                                            </ajax:CalendarExtender>--%>
                                                                        </div>
                                                                    </div>
                                                                </div>


                                                            </fieldset>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <div class="row" >
                                                    <div class="thumbnail" style="float: left; width: 100%;">
                                                        <div class="col-lg-4 col-md-4 col-xs-12 col-sm-6  col-lg-offset-4 col-md-offset-4 col-sm-offset-3 col-xs-offset-0  ">
                                                            <asp:ImageButton ID="btnSUmbit" ToolTip="Save" OnClick="btnSumbit_Click" ValidationGroup="saves"
                                                                ImageUrl="~/images/Sumbit.jpg" runat="server" />

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:TextBox ID="txtEndDate" Width="0px" Style="border: 0px" runat="server" CssClass="d-none"></asp:TextBox>
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
