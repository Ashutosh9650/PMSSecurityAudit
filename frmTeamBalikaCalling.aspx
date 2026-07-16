<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTeamBalikaCalling.aspx.cs" Culture="en-GB" MasterPageFile="~/Site.master"
    Inherits="frmTeamBalikaCalling" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .chkBoxList tr {
            height: 25px;
        }


        .chkBoxList td {
            width: 150px;
        }

        .w-100 {
            font-family: Calibri;
            font-size: 12PX;
            width: 100%
        }
    </style>
    <style type="text/css">
        .form-group {
            margin-right: -15px;
            margin-left: -15px;
        }

        .multiselect {
            width: 20em;
            height: 15em;
            border: solid 1px #c0c0c0;
            overflow: auto;
        }

            .multiselect label {
                display: block;
            }

        .multiselect-on {
            color: #ffffff;
            background-color: #000099;
    </style>
    }

        <script type="text/javascript">
            function SetMultilanguage(Flag, clsname) {

                var Lngg = "", lid = "";
                var maxSelection = 0;
                $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {

                    Lngg = Lngg + $(this).next().html() + ",";
                    lid = lid + $(this).val() + ",";
                    maxSelection++;

                });

                Lngg = Lngg.substr(0, Lngg.length - 1);
                lid = lid.substr(0, lid.length - 1);

                if (Flag == 'F1') {
                    debugger;
                    if (maxSelection <= 20) {
                        $('#<%=hdntxt_pbname1_ID.ClientID %>').val(lid);
                        $('#<%=hdntxt_pbname1_Name.ClientID %>').val(Lngg);
                        $('#<%=txt_pbname1.ClientID %>').val(Lngg);
                        if (Lngg.toLowerCase().indexOf("other") >= 0) {

                            $('#<%=txtCallOther.ClientID %>').val('');
                            $('#<%=txtCallOther.ClientID %>').attr('disabled', false);
                        }
                        else {

                            $('#<%=txtCallOther.ClientID %>').val('');
                            $('#<%=txtCallOther.ClientID %>').attr('disabled', true);
                        }
                    }

                    else {

                        $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                            $(this).attr("checked", false);
                        });
                        $('#<%=hdntxt_pbname1_ID.ClientID %>').val('');
                        $('#<%=hdntxt_pbname1_Name.ClientID %>').val('');
                        $('#<%=txt_pbname1.ClientID %>').val('');


                    }
                    //$find("Modal_alertB").show();

                    return false;
                }



                if (Flag == 'F2') {

                    if (maxSelection <= 20) {
                        $('#<%=hdntxt_pbname2_ID.ClientID %>').val(lid);
                        $('#<%=hdntxt_pbname2_Name.ClientID %>').val(Lngg);
                        $('#<%=txt_pbname2.ClientID %>').val(Lngg);
                        var v3 = $('#<%=lblTest.ClientID %>').val();

                        if (v3 == '0') {
                            if (Lngg.toLowerCase().indexOf("other") >= 0) {

                                $('#<%=txtDiscuOther.ClientID %>').val('');
                                $('#<%=txtDiscuOther.ClientID %>').attr('disabled', false);
                            }
                            else {

                                $('#<%=txtDiscuOther.ClientID %>').val('');
                                $('#<%=txtDiscuOther.ClientID %>').attr('disabled', true);
                            }
                        }

                    }

                    else {
                        $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                            $(this).attr("checked", false);
                        });
                        $('#<%=hdntxt_pbname2_ID.ClientID %>').val('');
                        $('#<%=hdntxt_pbname2_Name.ClientID %>').val('');
                        $('#<%=txt_pbname2.ClientID %>').val('');




                        $find("Modal_alertB").show();
                        return false;
                    }

                }
                if (Flag == 'F3') {

                    if (maxSelection <= 20) {
                        $('#<%=hdntxt_pbname3_ID.ClientID %>').val(lid);
                        $('#<%=hdntxt_pbname3_Name.ClientID %>').val(Lngg);
                        $('#<%=txt_pbname3.ClientID %>').val(Lngg);
                        var v1 = '';
                        var v2 = '';
                        var v3 = '';
                        var v2 = '';
                        var v3 = $('#<%=lblTest.ClientID %>').val();

                        if (v3 == '0') {
                            $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {



                                var text = $(this).closest("td").find("label").html();

                                if (text == 'Issues Sharing') {
                                    v1 = text;
                                }

                                if (text == 'Support Required') {
                                    v2 = text;
                                }
                                if (text == 'Other') {
                                    v3 = text;
                                }

                            });



                            if (v1 == 'Issues Sharing') {

                                $('#<%=txtIssue.ClientID %>').attr('disabled', false);
                            }
                            else {
                                $('#<%=txtIssue.ClientID %>').val('');
                                $('#<%=txtIssue.ClientID %>').attr('disabled', true);
                            }

                            if (v2 == 'Support Required') {

                                $('#<%=txtSupport.ClientID %>').attr('disabled', false);
                            }
                            else {
                                $('#<%=txtSupport.ClientID %>').val('');
                                $('#<%=txtSupport.ClientID %>').attr('disabled', true);
                            }


                            if (v3 == 'Other') {

                                $('#<%=txtNoOther.ClientID %>').attr('disabled', false);
                            }
                            else {
                                $('#<%=txtNoOther.ClientID %>').val('');
                                $('#<%=txtNoOther.ClientID %>').attr('disabled', true);
                            }

                        }

                    }

                    else {
                        $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                            $(this).attr("checked", false);
                        });
                        $('#<%=hdntxt_pbname3_ID.ClientID %>').val('');
                        $('#<%=hdntxt_pbname3_Name.ClientID %>').val('');
                        $('#<%=txt_pbname3.ClientID %>').val('');
                    }
                }

                if (Flag == 'F4') {

                    if (maxSelection <= 20) {
                        var v3 = $('#<%=lblTest.ClientID %>').val();

                        if (v3 == '0') {

                            $('#<%=hdntxt_pbname4_ID.ClientID %>').val(lid);
                            $('#<%=hdntxt_pbname4_Name.ClientID %>').val(Lngg);
                            $('#<%=txt_pbname4.ClientID %>').val(Lngg);

                            if (Lngg.toLowerCase().indexOf("other") >= 0) {

                                $('#<%=txtOther1.ClientID %>').val('');
                                $('#<%=txtOther1.ClientID %>').attr('disabled', false);
                            }
                            else {

                                $('#<%=txtOther1.ClientID %>').val('');
                                $('#<%=txtOther1.ClientID %>').attr('disabled', true);
                            }




                            var v2 = '';
                            $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {



                                var text = $(this).closest("td").find("label").html();

                                if (text == 'Nothing') {
                                    v2 = text;
                                }


                            });

                            $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                                var text = $(this).closest("td").find("label").html();

                                if (v2 == 'Nothing') {

                                    if (text == 'Nothing') {

                                    }
                                    else {

                                        this.checked = false;
                                        text.attr('disabled', false);

                                    }

                                }




                            });

                            //$('.checkbox').each(function () {
                            //    this.checked = true;
                            //});
                        }

                    }

                    else {
                        $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                            $(this).attr("checked", false);
                        });
                        $('#<%=hdntxt_pbname4_ID.ClientID %>').val('');
                        $('#<%=hdntxt_pbname4_Name.ClientID %>').val('');
                        $('#<%=txt_pbname4.ClientID %>').val('');



                        $find("Modal_alertB").show();
                        return false;
                    }

                }

            }
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

            <div class="container-fluid" style="margin-        top: 0px;">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-3" style="padding        -right: 0px;">
                        <div class="thumbnail" style="min-hei        ght: 750px; width: 228px;">
                            <div style="padding        -top: 3px;">
                                <%--<span style="float:left"> <asp:Label ID="lblsearch" runat="server" Text="Search:" ForeColor="Black"></asp:Label></span>--%>
                                <span style="float: right; padding-right: 1px;">
                                    <asp:TextBox ID="txtSearchName" Visible="false" runat="server" OnTextChanged="txtSearchName_Click" AutoPostBack="true" CssClass="form-control col-lg-1"></asp:TextBox></span>
                            </div>
                            <div style="overflo        w: auto; margin-top: 35px; height: 750px;">
                                <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="40"
                                    BorderStyle="None" DataKeyNames="UniqueId" GridLines="None" AutoGenerateColumns="false"
                                    OnRowCommand="GVMain_OnRowCommand" OnPageIndexChanging="GV_Project_PageIndexChanging">
                                    <EmptyDataTemplate>
                                        <div style="font-fa        mily: Arial; font-size: 12px; font-weight: bold; color: Red;">
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
                                        <asp:ButtonField HeaderText="TB Name " ItemStyle-ForeColor="#333" DataTextField="TBName"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>

                                        <asp:ButtonField HeaderText="Date " ItemStyle-ForeColor="#333" DataTextField="Calling_Date"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>

                                        <asp:ButtonField HeaderText="Name" Visible="false" Text="Button" DataTextField="UniqueId"></asp:ButtonField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-9">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading" style="padding: 5px">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <h3 class="text-danger" style="margin: 0px;">TeamBalika Calling</h3>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px 8px;">
                                                  <button type="button" id="ton-new" class="btn btn-primary" style="float: right; position: relative; right: 1px; color: #fff; background-color: #337ab7; border-color: #2e6da4;">
                                                    <i class="fa fa-bars"></i>
                                                </button>
                                              <%--  <input type="image" id="ton-new" class="butt" src="Images/search-not-29.png" title="Search" />--%>
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
                                            <div id="div-show-new">
                                                <div class="row marg search-bg">
                                                    <div class="form-horizontal">
                                                        <%-- <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>--%>

                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                            <div class="form-group" style="margin-        bottom: 7px;">
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
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding    -top: 2px;">
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
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding    -top: 2px;">
                                                                    District:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                                        AutoPostBack="true" class="form-control " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding    -top: 2px;">
                                                                    Block:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                                        class="form-control " />

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding    -top: 2px;">
                                                                    Panchayat:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"
                                                                        class="form-control " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding    -top: 2px;">
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
                                                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-right"
                                                                BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" Visible="False" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12" style="padding: 5px; margin-bottom: -12px;">
                                                <asp:Panel ID="pnlMain" Enabled="false" runat="server">
                                                    <div class="form-horizontal">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">
                                                            <fieldset class="scheduler-border" >
                                                              
                                                                <div class="form-group" style="    margin-top: 17px;">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Village Exit Readiness Status</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlVillageExit" runat="server" class="form-control">
                                                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                            <asp:ListItem Value="1">NA</asp:ListItem>
                                                                            <asp:ListItem Value="2">Exit Ready Village</asp:ListItem>
                                                                            <asp:ListItem Value="3">Village Exited</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        TB Name</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlTBCode" runat="server" class="form-control">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <asp:Label class="control-label col-sm-4" runat="server" ID="lblDob" Text="Calling Date"></asp:Label>
                                                                    <div class="col-sm-8">

                                                                        <asp:TextBox Enabled="false" runat="server" ID="txtDate" autocomplete="off" ondrop="return false;"
                                                                            class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                        <ajax:CalendarExtender ID="CalendarExtenderTourdate" runat="server" Enabled="True"
                                                                            Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight">
                                                                        </ajax:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="ReqTxtDate" runat="server" ControlToValidate="txtDate"
                                                                            Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                                            SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>

                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Call Type</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlCalltype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCalltype_SelectedIndexChanged" class="form-control">
                                                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                            <asp:ListItem Value="1">Outgoing Call </asp:ListItem>
                                                                            <asp:ListItem Value="2">Incoming Call</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" id="IScall" runat="server" visible="false">
                                                                    <label class="control-label col-sm-4" for="Name">
                                                                        Is Call Connected</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="ddlIscall" AutoPostBack="true" OnSelectedIndexChanged="ddlIscall_SelectedIndexChanged" runat="server" class="form-control">
                                                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                            <asp:ListItem Value="1">Yes </asp:ListItem>
                                                                            <asp:ListItem Value="2">No</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                </div>
                                                                <asp:Panel ID="pnlCall" runat="server" Visible="false">


                                                                    <div class="form-group">

                                                                        <div class="col-sm-12">
                                                                            <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                                                ID="Label4" Text="Objective of Calling" runat="server"></asp:Label>

                                                                            <asp:CheckBoxList ID="CBL_bookformat1" runat="server" CssClass="chkBoxList _bookformat1 w-100" onclick="SetMultilanguage('F1','_bookformat1');" RepeatColumns="2" RepeatDirection="Vertical"></asp:CheckBoxList>
                                                                            <%-- onclick="SetMultilanguage('F1','_bookformat1');"--%>
                                                                            <asp:TextBox ID="txt_pbname1" Visible="false" autocomplete="off" ondrop="return false;" runat="server"
                                                                                CssClass="form-control" onkeypress="return false;" TabIndex="5" TextMode="MultiLine"></asp:TextBox>
                                                                            <cc1:PopupControlExtender ID="PopupControltxt_pbname1" runat="server" TargetControlID="txt_pbname1"
                                                                                PopupControlID="pnt_bookformat1" OffsetY="22">
                                                                            </cc1:PopupControlExtender>
                                                                            <asp:Panel ID="pnt_bookformat1" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 40.5%"
                                                                                CssClass="panel">
                                                                                <span>
                                                                                    <asp:CheckBoxList ID="CBL_bookformahht1" CssClass="_bookformat1 radio" runat="server"
                                                                                        onclick="SetMultilanguage('F1','_bookformat1');">
                                                                                    </asp:CheckBoxList>
                                                                                </span>
                                                                                <asp:HiddenField runat="server" ID="hdntxt_pbname1_ID" />
                                                                                <asp:HiddenField runat="server" ID="hdntxt_pbname1_Name" />
                                                                            </asp:Panel>


                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group" id="divOther" runat="server">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Other</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtCallOther" Enabled="false" onkeypress="return onlyAlphabets(event,this);" runat="server"
                                                                                MaxLength="50" class="form-control" TextMode="MultiLine" />


                                                                        </div>

                                                                    </div>


                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlCallNo" runat="server" Visible="false">

                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            How Many Times Tried for Calling</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtHowCall" runat="server" MaxLength="1" onkeypress="return isNumberKey(this,event);"
                                                                                autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />

                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Reason for not Completing Call</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlReasonNot" AutoPostBack="true" OnSelectedIndexChanged="ddlContact_SelectedIndexChanged" runat="server" class="form-control">
                                                                            </asp:DropDownList>

                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Any Alternate Mobile Number</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtContact"
                                                                                OnKeyUp="javascript:inputtxt();" Enabled="false" runat="server" MaxLength="10"
                                                                                onkeypress="return isNumberKey(this,event);" onchange="javascript: phonenumber(this.value,'TeContact1');"
                                                                                autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Any Action Point</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtAnyAction" MaxLength="50" autocomplete="off" ondrop="return false;"
                                                                                onkeypress="return onlyAlphabets(event,this);" runat="server" class="form-control" TextMode="MultiLine" />

                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>

                                                                <asp:Panel ID="pnlNo" runat="server" Visible="false">
                                                                    <div class="form-group">

                                                                        <div class="col-sm-12">
                                                                            <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                                                ID="Label3" Text="Objective of Calling" runat="server"></asp:Label>

                                                                            <asp:CheckBoxList ID="chkobjCall" runat="server" CssClass="chkBoxList _bookformat3 w-100" onclick="SetMultilanguage('F3','_bookformat3');" RepeatColumns="2" RepeatDirection="Vertical"></asp:CheckBoxList>
                                                                            <%-- onclick="SetMultilanguage('F1','_bookformat1');"--%>

                                                                            <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="txt_pbname3"
                                                                                PopupControlID="pnt_bookformat1" OffsetY="22">
                                                                            </cc1:PopupControlExtender>
                                                                            <asp:Panel ID="txt_pbname3" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 40.5%"
                                                                                CssClass="panel">
                                                                                <span>
                                                                                    <asp:CheckBoxList ID="CheckBoxList1" CssClass="_bookformat2 radio" runat="server"
                                                                                        onclick="SetMultilanguage('F3','_bookformat3');">
                                                                                    </asp:CheckBoxList>
                                                                                </span>

                                                                                <asp:HiddenField runat="server" ID="hdntxt_pbname3_ID" />
                                                                                <asp:HiddenField runat="server" ID="hdntxt_pbname3_Name" />
                                                                            </asp:Panel>


                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Issues Sharing
                                                                        </label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtIssue" Enabled="false" MaxLength="100" autocomplete="off" ondrop="return false;"
                                                                                onkeypress="return onlyAlphabets(event,this);" runat="server" class="form-control" TextMode="MultiLine" />

                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Support Required
                                                                        </label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtSupport" Enabled="false" MaxLength="100" autocomplete="off" ondrop="return false;"
                                                                                onkeypress="return onlyAlphabets(event,this);" runat="server" class="form-control" TextMode="MultiLine" />

                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Other
                                                                        </label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtNoOther" Enabled="false" MaxLength="100" autocomplete="off" ondrop="return false;"
                                                                                onkeypress="return onlyAlphabets(event,this);" runat="server" class="form-control" TextMode="MultiLine" />

                                                                        </div>
                                                                    </div>


                                                                </asp:Panel>


                                                            </fieldset>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">
                                                            <fieldset class="scheduler-border">
                                                                <legend class="scheduler-border" style="margin: 0px;"></legend>
                                                                <asp:Panel ID="pnlCall1" runat="server" Visible="false">
                                                                    <div class="form-group" id="pnlC881" runat="server" visible="false">

                                                                        <div class="col-sm-12">
                                                                            <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                                                ID="Label1" Text=" Other Discussion Points" runat="server"></asp:Label>

                                                                            <asp:CheckBoxList ID="chkOtherDicu" runat="server" CssClass="chkBoxList _bookformat2 w-100" onclick="SetMultilanguage('F2','_bookformat2');" RepeatColumns="2" RepeatDirection="Vertical"></asp:CheckBoxList>
                                                                            <%-- onclick="SetMultilanguage('F1','_bookformat1');"--%>

                                                                            <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="txt_pbname2"
                                                                                PopupControlID="pnt_bookformat1" OffsetY="22">
                                                                            </cc1:PopupControlExtender>
                                                                            <asp:Panel ID="txt_pbname2" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 40.5%"
                                                                                CssClass="panel">
                                                                                <span>
                                                                                    <asp:CheckBoxList ID="CheckBoxList2" CssClass="_bookformat2 radio" runat="server"
                                                                                        onclick="SetMultilanguage('F2','_bookformat2');">
                                                                                    </asp:CheckBoxList>
                                                                                </span>

                                                                                <asp:HiddenField runat="server" ID="hdntxt_pbname2_ID" />
                                                                                <asp:HiddenField runat="server" ID="hdntxt_pbname2_Name" />
                                                                            </asp:Panel>


                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">

                                                                        <div class="col-sm-12">
                                                                            <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                                                ID="Label7" Text=" Other Discussion Points" runat="server"></asp:Label>

                                                                            <table id="ctl00_MainConte" class="chkBoxList _bookformat2 w-100" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkY1" runat="server" Text="D2D Survey" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkY8" runat="server" Text="GKP" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkY2" runat="server" Text="D2D Contact" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkY9" runat="server" Text="Bal Sabha &amp; LSE" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkY3" runat="server" Text="GSS/MM meeting" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkY10" runat="server" Text="PRIs/Influencer" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkY4" runat="server" Text="SMC meeting" />

                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkY11" runat="server" Text="Engagement in Activities" />

                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkY5" runat="server" Text="CBL" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkY12" AutoPostBack="true" OnCheckedChanged="chkY12_CheckedChanged" runat="server" Text="Nothing" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkY6" runat="server" Text="Enrolment" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkY13" AutoPostBack="true" OnCheckedChanged="chkY13_CheckedChanged" runat="server" Text="Other" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkY7" runat="server" Text="Individual retention" />
                                                                                        </td>
                                                                                        <td></td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>

                                                                    </div>
                                                                    <div class="form-group" id="div2" runat="server">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Other</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtDiscuOther" Enabled="false" onkeypress="return onlyAlphabets(event,this);" runat="server"
                                                                                MaxLength="50" class="form-control" TextMode="MultiLine" />


                                                                        </div>
                                                                        <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                                            ID="Label2" Text="" runat="server"></asp:Label>

                                                                    </div>
                                                                    <div class="form-group" id="div1" runat="server">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Any Feedback from Team Balika</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtFeedback" onkeypress="return onlyAlphabets(event,this);" runat="server"
                                                                                MaxLength="100" class="form-control" TextMode="MultiLine" />


                                                                        </div>

                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Any Critical Concern Raised by Team Balika</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtAnyCritical" MaxLength="100" autocomplete="off" ondrop="return false;"
                                                                                onkeypress="return onlyAlphabets(event,this);" runat="server" class="form-control" TextMode="MultiLine" />

                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Is Critical Concern needs to be shared with DPO</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlIsCrit" runat="server" class="form-control">
                                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                <asp:ListItem Value="1">Yes </asp:ListItem>
                                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                                            </asp:DropDownList>

                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Remark</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtRemark" MaxLength="100" autocomplete="off" ondrop="return false;"
                                                                                onkeypress="return onlyAlphabets(event,this);" runat="server" class="form-control" TextMode="MultiLine" />

                                                                        </div>
                                                                    </div>

                                                                </asp:Panel>

                                                                <asp:Panel ID="pnlYCritical" runat="server" Visible="false">
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Critical Concern Status</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlCriticalStatus" runat="server" class="form-control">
                                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                <asp:ListItem Value="1">Resolved  </asp:ListItem>
                                                                                <asp:ListItem Value="2">Not Resolved </asp:ListItem>
                                                                            </asp:DropDownList>

                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>

                                                                <asp:Panel ID="pnlNo1" runat="server" Visible="false">

                                                                    <div class="form-group" runat="server" id="pnhuNo1" visible="false">

                                                                        <div class="col-sm-12">
                                                                            <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                                                ID="Label5" Text="Other Discussion Points" runat="server"></asp:Label>

                                                                            <asp:CheckBoxList ID="chkobjdiuOther" runat="server" CssClass="chkBoxList _bookformat4 w-100" onclick="SetMultilanguage('F4','_bookformat4');" RepeatColumns="2" RepeatDirection="Vertical"></asp:CheckBoxList>
                                                                            <%-- onclick="SetMultilanguage('F1','_bookformat1');"--%>

                                                                            <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" TargetControlID="txt_pbname4"
                                                                                PopupControlID="pnt_bookformat1" OffsetY="22">
                                                                            </cc1:PopupControlExtender>
                                                                            <asp:Panel ID="txt_pbname4" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 40.5%"
                                                                                CssClass="panel">
                                                                                <span>
                                                                                    <asp:CheckBoxList ID="CheckBoxList3" CssClass="_bookformat2 radio" runat="server"
                                                                                        onclick="SetMultilanguage('F4','_bookformat4');">
                                                                                    </asp:CheckBoxList>
                                                                                </span>

                                                                                <asp:HiddenField runat="server" ID="hdntxt_pbname4_ID" />
                                                                                <asp:HiddenField runat="server" ID="hdntxt_pbname4_Name" />
                                                                            </asp:Panel>


                                                                        </div>



                                                                    </div>
                                                                    <div class="form-group">

                                                                        <div class="col-sm-12">
                                                                            <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                                                ID="Label8" Text=" Other Discussion Points" runat="server"></asp:Label>

                                                                            <table id="ctl00_Made" class="chkBoxList _bookformat2 w-100" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkN1" runat="server" Text="D2D Survey" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkN8" runat="server" Text="GKP" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkN2" runat="server" Text="D2D Contact" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkN9" runat="server" Text="Bal Sabha &amp; LSE" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkN3" runat="server" Text="GSS/MM meeting" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkN10" runat="server" Text="PRIs/Influencer" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkN4" runat="server" Text="SMC meeting" />

                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkN11" runat="server" Text="Engagement in Activities" />

                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkN5" runat="server" Text="CBL" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkN12" AutoPostBack="true" OnCheckedChanged="chkN12_CheckedChanged" runat="server" Text="Nothing" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkN6" runat="server" Text="Enrolment" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkN13" AutoPostBack="true" OnCheckedChanged="chkN13_CheckedChanged" runat="server" Text="Other" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkN7" runat="server" Text="Individual retention" />
                                                                                        </td>
                                                                                        <td></td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>

                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Other
                                                                        </label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtOther1" MaxLength="100" Enabled="false" autocomplete="off" ondrop="return false;"
                                                                                onkeypress="return onlyAlphabets(event,this);" runat="server" class="form-control" TextMode="MultiLine" />

                                                                        </div>
                                                                        <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                                            ID="Label6" Text="" runat="server"></asp:Label>

                                                                    </div>


                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Any Critical Concern Raised by Team Balika
                                                                        </label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtCritical" MaxLength="100" autocomplete="off" ondrop="return false;"
                                                                                onkeypress="return onlyAlphabets(event,this);" runat="server" class="form-control" TextMode="MultiLine" />

                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Is Critical Concern needs to be shared with DPO</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlDPO" runat="server" class="form-control">
                                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                <asp:ListItem Value="1">Yes  </asp:ListItem>
                                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                                            </asp:DropDownList>


                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Remark</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtDBORepark" MaxLength="100" autocomplete="off" ondrop="return false;"
                                                                                onkeypress="return onlyAlphabets(event,this);" runat="server" class="form-control" TextMode="MultiLine" />

                                                                        </div>
                                                                    </div>

                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlNCritical" runat="server" Visible="false">
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Critical Concern Status</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlCriticalConcern" runat="server" class="form-control">
                                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                <asp:ListItem Value="1">Resolved  </asp:ListItem>
                                                                                <asp:ListItem Value="2">Not Resolved </asp:ListItem>
                                                                            </asp:DropDownList>

                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                        </div>
                                                    </fieldset>
                                                </asp:Panel>
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


            <asp:Label ID="HdnStartYear" Visible="false" runat="server" />

            <asp:HiddenField runat="server" ID="lblTest" />
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
