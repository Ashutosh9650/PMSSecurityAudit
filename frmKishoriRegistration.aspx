<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmKishoriRegistration.aspx.cs"
    Culture="en-GB" MasterPageFile="~/Site.master" Inherits="frmKishoriRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        table.rdbtn {
            width: 100%;
        }
    </style>
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
    <script type="text/javascript">
        function SetMultilanguage(Flag, clsname) {
            debugger;
            var Lngg = "", lid = "";
            var maxSelection = 0;
            $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                Lngg = Lngg + $(this).next().html() + ",";
                lid = lid + $(this).val() + ",";
                maxSelection++;
            });

            Lngg = Lngg.substr(0, Lngg.length - 1);
            lid = lid.substr(0, lid.length - 1);
            if (Flag == 'F') {
                if (maxSelection <= 10) {
                    $('#<%=hdn_PBID.ClientID %>').val(lid);
                    $('#<%=hdn_PBName.ClientID %>').val(Lngg);
                    $('#<%=txt_pbname.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_PBID.ClientID %>').val('');
                    $('#<%=hdn_PBName.ClientID %>').val('');
                    $('#<%=txt_pbname.ClientID %>').val('');
                    $find("Modal_alertB").show();
                    return false;
                }


            }
            else if (Flag == 'FN') {
                if (maxSelection <= 10) {
                    $('#<%=hdn_PBIDNew.ClientID %>').val(lid);
                    $('#<%=hdn_pbnameNew.ClientID %>').val(Lngg);
                    $('#<%=txt_pbnameNew.ClientID %>').val(Lngg);

                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_PBIDNew.ClientID %>').val('');
                    $('#<%=hdn_pbnameNew.ClientID %>').val('');
                    $('#<%=txt_pbnameNew.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }


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


        function phonenumber1(inputtxt, txtid) {
            var phoneno = /^\d{10}$/;
            var phoneno1 = /^\d{11}$/;
            var phoneno2 = /^\d{12}$/;
            if (phoneno.test(inputtxt) && inputtxt.length == 10) {
                $("." + txtid).css("border", "solid 1px green")
                return true;
            }
            else if (phoneno1.test(inputtxt) && inputtxt.length == 11) {
                $("." + txtid).css("border", "solid 1px green")
                return true;
            }
            else if (phoneno2.test(inputtxt) && inputtxt.length == 12) {
                $("." + txtid).css("border", "solid 1px green")
                return true;
            }
            else {
                $("." + txtid).css("border", "solid 1px red")
                $("." + txtid).val('');
                alert("Registration ID should be 10 to 12 digit");

                return false;
            }
        }


    </script>
    <script type="text/javascript">
        function checkdataT() {
            debugger;
            var ddlReason = document.getElementById('<%=ddlReason.ClientID %>').value;

            if (ddlReason == "9") {
                $('#<%=txtOther.ClientID %>').attr('disabled', false);
                $('#<%=txtOther.ClientID %>').val('');
            }
            else {
                $('#<%=txtOther.ClientID %>').attr('disabled', true);
                $('#<%=txtOther.ClientID %>').val('');
            }
            return true;
        }
        function checkdata() {
            debugger;
            var ddlVillage = document.getElementById('<%=ddlVillage.ClientID %>').value;
            var ddlCampID = document.getElementById('<%=ddlCampID.ClientID %>').value;
            var ddlType = document.getElementById('<%=ddlType.ClientID %>').value;

            var txtPrerakName = document.getElementById('<%=ddlPrerakName.ClientID %>').value;

            //var txtPrerakCode = document.getElementById('<%=txtPrerakCode.ClientID %>').value;
            var Category = document.getElementById('<%=ddlCategory.ClientID %>').value;
            var txtRegistrationDate = document.getElementById('<%=txtRegistrationDate.ClientID %>').value;

            var txtDate = document.getElementById('<%=txtDate.ClientID %>').value;
            var ddlCategory = document.getElementById('<%=ddlCategory.ClientID %>').value;
            var ddlKishoricontact = document.getElementById('<%=ddlKishoricontact.ClientID %>').value;

            var txtFatherName = document.getElementById('<%=txtFatherName.ClientID %>').value;
            var txtMotherName = document.getElementById('<%=txtMotherName.ClientID %>').value;
            var txtMobile = document.getElementById('<%=txtMobile.ClientID %>').value;

            var txtxAlternate = document.getElementById('<%=txtxAlternate.ClientID %>').value;
            var ddlSmart = document.getElementById('<%=ddlSmart.ClientID %>').value;
            var txtKishoriMobileNumber = document.getElementById('<%=txtKishoriMobileNumber.ClientID %>').value;
            var ddlLastClass = document.getElementById('<%=ddlLastClass.ClientID %>').value;
            var ddlReason = document.getElementById('<%=ddlReason.ClientID %>').value;
            var ddlGender = document.getElementById('<%=ddlGender.ClientID %>').value;


            var txtOther = document.getElementById('<%=txtOther.ClientID %>').value;
            var ddlExamType = document.getElementById('<%=ddlExamType.ClientID %>').value;

            var txtDOBReg = document.getElementById('<%=txtDOBReg.ClientID %>').value;
            var txtRegistration = document.getElementById('<%=txtRegistration.ClientID %>').value;
            var Class = document.getElementById('<%=ddlClass.ClientID %>').value;
            var txtDOBReg1 = document.getElementById('<%=ddlCompletionYr.ClientID %>').value;
            var txt_pbname = document.getElementById('<%=txt_pbname.ClientID %>').value;
            var txt_pbnameNew = document.getElementById('<%=txt_pbnameNew.ClientID %>').value;


            var str = "";
            if (ddlCampID == "0") {
                str = " Please Select Camp";
            }
            if (ddlType == "0") {
                str = str + "\n Please Select Type";
            }
            if (ddlVillage == "0") {
                str = str + "\n Please Select Village";
            }
            if (txtPrerakName == "0") {
                str = str + "\n Please Type Prerak Name";
            }
            //            if (txtPrerakCode == "") {
            //                str = str + "\n Please Type Prerak Code";
            //            }
            if (txtRegistrationDate == "") {
                str = str + "\n Please Select  Registration Date";
            }
            if (txtDate == "") {
                str = str + "\n Please Select  DOB Date";
            }

            if (ddlCategory == "0") {
                str = str + "\n Please Select Social Category";
            }
            if (ddlKishoricontact == "0") {
                str = str + "\n Please Select-How did you make contact with Kishori";
            }
            if (txtFatherName == "") {
                str = str + "\n Please Enter Father Name";
            }
            if (txtMotherName == "") {
                str = str + "\n Please Enter Mother Name";
            }
            if (txtMobile == "") {
                str = str + "\n Please Enter Parents Mobile Number";
            }
            if (txtxAlternate == "") {
                str = str + "\n Please Enter Parents WhatsApp Number";
            }
            if (ddlSmart == "1") {
                if (txtKishoriMobileNumber == "") {

                    str = str + "\n Please Enter Kishori Mobile Number";
                }
            }
            if (ddlLastClass == "0") {
                str = str + "\n Please Select Last Class Completed";
            }
            if (txtDOBReg1 == "0") {
                str = str + "\n Please select Last Class Completion Year";
            }
            if (ddlReason == "0") {
                str = str + "\n Please Select Reason";
            }
            if (ddlReason == "9") {
                if (txtOther == "") {

                    str = str + "\n Please Enter Other info";
                }
            }


            if (ddlGender == "0") {
                str = str + "\n Please Select Gender";
            }
            if (ddlExamType == "0") {
                str = str + "\n Please Select Exam Type";
            }
            if (txtDOBReg == "") {
                str = str + "\n Please select Date";
            }
            if (txtRegistration == "") {
                str = str + "\n Please Enter Registration ID";
            }
            if (Class == "0") {
                str = str + "\n Please Select Class of Admission";
            }
            if (txt_pbname == "") {
                str = str + "\n Please Select Document Availability";
            }
            if (txt_pbnameNew == "") {
                str = str + "\n Please select at lease 1 Subject";
            }
            if (txtRegistration.length >= 10) {

            }
            else {
                str = str + "\n Registration ID should be 10 to 12 digit";
            }

            if (txtRegistrationDate != "" && txtDate != "") {

                var txtRegistrationDate = txtRegistrationDate;
                var dateParts = txtRegistrationDate.split("/");

                var Birth = txtDate;
                var BirthD = Birth.split("/");
                var Age = dateParts[2] - BirthD[2];
                if (Age < 14) {
                    str = str + "\n Please ensure Age is between 14 and 24 years";
                }
                if (Age > 24) {
                    str = str + "\n Please ensure Age is between 14 and 24 years";
                }
            }

            //if (ddlExamType == "2") {
            //    if (txt_pbnameNew == "") {

            //    }
            //    else {


            //        var str1 = txt_pbnameNew;
            //        var numbers = str1.split(',');
            //        var sum = 0;
            //        for (var i = 0; i < numbers.length; i++) {
            //            sum = parseInt(i) + 1;
            //        }
            //        if (sum >= 5) {

            //        }
            //        else {
            //            str = str + "\n Please select Minimum 5 Subjects";
            //        }
            //    }
            //}




            //if (Class == "0") {
            //    str = str + "\n Please Select Class";
            //}
            //if (SRNumber == "") {
            //    str = str + "\n Please Fill SR Number";
            //}

            if (str != "") {
                alert(str);
                return false;
            }
        }


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
            <div class="container-fluid" style="margin-top: 0px;">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-3" style="padding-right: 0px;">
                        <div class="thumbnail" style="min-height: 860px; width: 228px;">
                            <div style="padding-top: 3px;">
                                <%--<span style="float:left"> <asp:Label ID="lblsearch" runat="server" Text="Search:" ForeColor="Black"></asp:Label></span>--%>
                                <span style="float: right; padding-right: 1px;">
                                    <asp:TextBox ID="txtSearchName" Visible="false" runat="server" OnTextChanged="txtSearchName_Click"
                                        AutoPostBack="true" CssClass="form-control col-lg-1"></asp:TextBox></span>
                            </div>
                            <div style="overflow: auto; margin-top: 35px; height: 840px;">
                                <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="40"
                                    BorderStyle="None" DataKeyNames="UniqueChildRCode" GridLines="None" AutoGenerateColumns="false"
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
                                        <asp:ButtonField HeaderText="Kishori Name" ItemStyle-ForeColor="#333" DataTextField="KishoriName"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="Class" ItemStyle-ForeColor="#333" DataTextField="Description"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="Name" Visible="false" Text="Button" DataTextField="UniqueChildRCode"></asp:ButtonField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-9">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading" style="padding: 3px 0px">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <h3 class="text-danger" style="margin: 0px;">Pragati CBL</h3>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">
                                                <button type="button" id="ton-new" class="btn btn-primary" style="float: right; position: relative; right: 1px; color: #fff; background-color: #337ab7; border-color: #2e6da4;">
                                                    <i class="fa fa-bars"></i>
                                                </button>

                                                <%-- <input type="image" id="ton-new" class="butt" src="Images/search-not-29.png" title="Search" />--%>
                                                <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right"
                                                    ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px; padding: 0px;"
                                                    runat="server" />
                                                <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClientClick="return checkdata();"
                                                    OnClick="btnsave_Click" ValidationGroup="saves" Style="margin-right: 5px; padding: 0px;"
                                                    runat="server" />
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
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                    Year:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                                        class="form-control ">
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
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                    Camp ID:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlCampID" runat="server" class="form-control ">
                                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                        <asp:ListItem Value="1">Camp-A</asp:ListItem>
                                                                        <asp:ListItem Value="2">Camp-B</asp:ListItem>
                                                                        <asp:ListItem Value="3">Camp-C</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                    Type:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                                                        class="form-control ">
                                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                        <asp:ListItem Value="1">Kishori Registration</asp:ListItem>
                                                                        <asp:ListItem Value="2">Kishori Attendance</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12" style="margin-bottom: 12px;">
                                                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd"
                                                                BackColor="#f1f1f1" OnClick="btnSerach_Click" Style="float: right;" ImageUrl="~/images/search-29.png" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12" style="margin-top: 5px; padding: 0px 10px;">
                                                <asp:Panel ID="pnlMain" Enabled="false" runat="server">
                                                    <div class="form-horizontal">
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" style="padding: 0px 5px 0px 0px;">
                                                                <fieldset class="scheduler-border" style="min-height: 666px;">
                                                                    <legend class="scheduler-border">Personal Details </legend>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Prerak Name</label>
                                                                        <div class="col-sm-7">
                                                                            <asp:DropDownList ID="ddlPrerakName" runat="server" class="form-control">
                                                                            </asp:DropDownList>
                                                                            <asp:TextBox ID="txtPrerakName" Width="93%" MaxLength="70" Visible="false" runat="server"
                                                                                class="form-control" />
                                                                        </div>
                                                                        <div class="col-sm-1">
                                                                            <asp:ImageButton ID="ImageButton1" CssClass="btn btn-info pull-right" BackColor="transparent"
                                                                                ToolTip="Add" ImageUrl="~/images/add-29-1.png" Style="margin-right: 5px,; padding: 0px;"
                                                                                runat="server" OnClick="btn_sapark" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group" runat="server" visible="false">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Prerak Code</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtPrerakCode" MaxLength="70" Enabled="false" autocomplete="off"
                                                                                ondrop="return false;" runat="server" class="form-control" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <asp:Label class="control-label col-sm-4" runat="server" ID="Label9" Text="Registration Date"></asp:Label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox runat="server" ID="txtRegistrationDate" autocomplete="off" ondrop="return false;"
                                                                                class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                            <ajax:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                                OnClientDateSelectionChanged="arrivaldatecheck" TargetControlID="txtRegistrationDate"
                                                                                PopupPosition="BottomRight">
                                                                            </ajax:CalendarExtender>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Kishori Name</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtKishoriName" runat="server" MaxLength="70" autocomplete="off"
                                                                                ondrop="return false;" class="form-control " />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <asp:Label class="control-label col-sm-4" runat="server" ID="lblDob" Text="Date of Birth"></asp:Label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox runat="server" ID="txtDate" autocomplete="off" ondrop="return false;"
                                                                                class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                            <ajax:CalendarExtender ID="CalendarExtenderTourdate" runat="server" Enabled="True"
                                                                                Format="dd/MM/yyyy" OnClientDateSelectionChanged="arrivaldatecheck" TargetControlID="txtDate"
                                                                                PopupPosition="BottomRight">
                                                                            </ajax:CalendarExtender>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Social Category</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlCategory" runat="server" class="form-control">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            How did you make contact with Kishori</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlKishoricontact" runat="server" class="form-control">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Father Name</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtFatherName" onkeypress="return onlyAlphabets(event,this);" runat="server"
                                                                                MaxLength="40" class="form-control" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Mother Name</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtMotherName" onkeypress="return onlyAlphabets(event,this);" runat="server"
                                                                                MaxLength="40" class="form-control" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Parents Mobile Number</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtMobile" OnKeyUp="javascript:inputtxt();" runat="server" MaxLength="10"
                                                                                onkeypress="return isNumberKey(this,event);" onchange="javascript: phonenumber(this.value,'TeContact2');"
                                                                                autocomplete="off" ondrop="return false;" class="form-control TeContact2 " />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Parents WhatsApp Number</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtxAlternate" OnKeyUp="javascript:inputtxt();" runat="server" MaxLength="10"
                                                                                onkeypress="return isNumberKey(this,event);" onchange="javascript: phonenumber(this.value,'TeContact3');"
                                                                                autocomplete="off" ondrop="return false;" class="form-control TeContact3 " />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Does Kishori have a WhatsApp phone available for 2-3 hours a day to study?</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlSmart" runat="server" class="form-control">
                                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                <asp:ListItem Value="1">1-Yes </asp:ListItem>
                                                                                <asp:ListItem Value="2">2-No</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Kishori Mobile Number</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtKishoriMobileNumber" OnKeyUp="javascript:inputtxt();" runat="server"
                                                                                MaxLength="10" onkeypress="return isNumberKey(this,event);" onchange="javascript: phonenumber(this.value,'TeContact4');"
                                                                                autocomplete="off" ondrop="return false;" class="form-control TeContact4" />
                                                                        </div>
                                                                    </div>
                                                                </fieldset>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" style="padding: 9px 0px 0px 5px;">
                                                                <fieldset class="scheduler-border" style="padding-top: 10px !important;">
                                                                    <legend class="scheduler-border"></legend>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Last Class Completed by Kishori</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlLastClass" runat="server" class="form-control">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Last Class Completion Year</label>
                                                                        <div class="col-sm-8">
                                                                            <%-- <asp:TextBox runat="server" ID="txtDOBReg1" autocomplete="off" ondrop="return false;"
                                                                            class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="yyyy"
                                                                            DefaultView="Years" TargetControlID="txtDOBReg1"  PopupPosition="BottomRight">
                                                                        </ajax:CalendarExtender>--%>

                                                                            <asp:DropDownList ID="ddlCompletionYr" runat="server" class="form-control">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Reason for Dropout</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlReason" onclick="return checkdataT();" runat="server" class="form-control">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group" runat="server" id="Resone">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Other Info</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtOther" Enabled="false" onkeypress="return onlyAlphabets(event,this);"
                                                                                runat="server" MaxLength="40" class="form-control" />
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
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Availability of Document</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txt_pbname" autocomplete="off" ondrop="return false;" runat="server"
                                                                                CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                                            <ajax:PopupControlExtender ID="PopupControltxt_pbname" runat="server" TargetControlID="txt_pbname"
                                                                                PopupControlID="pnt_bookformat" OffsetY="22">
                                                                            </ajax:PopupControlExtender>
                                                                            <asp:Panel ID="pnt_bookformat" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 100%"
                                                                                CssClass="panel">
                                                                                <span>
                                                                                    <asp:CheckBoxList ID="CBL_bookformat" CssClass="_bookformat radio" runat="server"
                                                                                        onclick="SetMultilanguage('F','_bookformat');">
                                                                                    </asp:CheckBoxList>
                                                                                </span>
                                                                                <asp:HiddenField runat="server" ID="hdn_PBName" />
                                                                                <asp:HiddenField runat="server" ID="hdn_PBID" />
                                                                            </asp:Panel>
                                                                        </div>
                                                                    </div>
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
                                                                            Exam Type</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlExamType" runat="server" class="form-control">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Subject</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txt_pbnameNew" autocomplete="off" ondrop="return false;" runat="server"
                                                                                CssClass="form-control col-sm-1" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                                            <ajax:PopupControlExtender ID="PopupControltxt_pbnameNew" runat="server" TargetControlID="txt_pbnameNew"
                                                                                PopupControlID="pnt_txt_pbnameNew" OffsetY="22">
                                                                            </ajax:PopupControlExtender>
                                                                            <asp:Panel ID="pnt_txt_pbnameNew" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 100%"
                                                                                CssClass="panel">
                                                                                <span>
                                                                                    <asp:CheckBoxList ID="CBL_bookformatNew" CssClass="_bookformat9 radio" runat="server"
                                                                                        onclick="SetMultilanguage('FN','_bookformat9');">
                                                                                    </asp:CheckBoxList>
                                                                                </span>
                                                                                <asp:HiddenField runat="server" ID="hdn_pbnameNew" />
                                                                                <asp:HiddenField runat="server" ID="hdn_PBIDNew" />
                                                                            </asp:Panel>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            DOB as per registration form</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox runat="server" ID="txtDOBReg" autocomplete="off" ondrop="return false;"
                                                                                class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                            <ajax:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                                OnClientDateSelectionChanged="arrivaldatecheck" TargetControlID="txtDOBReg" PopupPosition="BottomRight">
                                                                            </ajax:CalendarExtender>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="control-label col-sm-4" for="Name">
                                                                            Registration ID</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtRegistration" onchange="javascript: phonenumber1(this.value,'TeCont2');" runat="server" MaxLength="12" onkeypress="return isNumberKey(this,event);"
                                                                                autocomplete="off" ondrop="return false;" class="form-control TeCont2" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <asp:Label runat="server" ID="Label6" class="control-label col-sm-4" Text="Class of Admission:"></asp:Label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlClass" runat="server" class="form-control">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </fieldset>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlattendance" runat="server" Visible="false">
                                                    <fieldset class="scheduler-border">
                                                        <legend class="scheduler-border">Attendance Details </legend>
                                                        <div class="form-group">
                                                            <div class="col-lg-4 col-md-3 col-sm-3 cpl-xs-12">
                                                                <div class="col-sm-3 padd">
                                                                    Attendance Date
                                                                </div>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:TextBox runat="server" ID="TxtAttendanceDate" autocomplete="off" ondrop="return false;"
                                                                        class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                        TargetControlID="TxtAttendanceDate" PopupPosition="BottomRight">
                                                                    </ajax:CalendarExtender>
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtAttendanceDate"
                                                                        Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-3 col-sm-3 cpl-xs-12">
                                                                <div class="col-sm-3 padd">
                                                                    Session
                                                                </div>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlsession" runat="server" class="form-control" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlsession_OnSelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-3 col-sm-3 cpl-xs-12">
                                                                <div class="col-sm-3 padd">
                                                                    Prerak Name
                                                                </div>
                                                                <div class="col-sm-7 padd">
                                                                    <asp:DropDownList ID="ddlattendancePrarak" runat="server" class="form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <asp:ImageButton ID="ImageButton2" CssClass="btn btn-info" BackColor="#f5f5f5"
                                                                        ToolTip="Add" ImageUrl="~/images/add-29-1.png" Style="margin-right: 5px,; padding: 0px;"
                                                                        runat="server" OnClick="btn_sapark1" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                    <div class="form-horizontal">
                                                        <div style="overflow: auto; margin-top: 35px; height: 300px;">
                                                            <asp:GridView ID="Gvattendance" runat="server" Width="100%" AllowPaging="true" PageSize="50"
                                                                CssClass="table table-striped table-bordered table-condensed" OnRowDataBound="Gvattendance_OnRowDataBound"
                                                                BorderStyle="None" GridLines="None" AutoGenerateColumns="false" DataKeyNames="UniqueCode, UniqueChildRCode,VillageCode,CampID">
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
                                                                <PagerSettings Position="Bottom" PageButtonCount="5"></PagerSettings>
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Kishori Name" Visible="true">
                                                                        <ItemStyle Width="17%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIsDropOut" Visible="false" ForeColor="Black" Font-Names="Calibri"
                                                                                ItemStyle-ForeColor="#333" runat="server" Text='<%# Bind("DropOut") %>'></asp:Label>
                                                                            <asp:Label ID="lblPresent" Visible="false" ForeColor="Black" Font-Names="Calibri"
                                                                                ItemStyle-ForeColor="#333" runat="server" Text='<%# Bind("Present") %>'></asp:Label>
                                                                            <asp:Label ID="lblDropoutReason" Visible="false" ForeColor="Black" Font-Names="Calibri"
                                                                                ItemStyle-ForeColor="#333" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                                                                            <asp:Label ID="lblKishoriName" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%# Bind("KishoriName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Class" Visible="true">
                                                                        <ItemStyle Width="8%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblClass" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%# Bind("Class") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Registration Type" Visible="true">
                                                                        <ItemStyle Width="15%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRegistrationType" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                runat="server" Text='<%# Bind("RegistrationType") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Present/Absent" Visible="true">
                                                                        <ItemStyle Width="15%" />
                                                                        <ItemTemplate>
                                                                            <asp:RadioButtonList ID="rdbtn_Present" runat="server" CssClass="rdbtn" RepeatDirection="Horizontal">
                                                                                <asp:ListItem Text="Present" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Absent" Value="2"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Is Dropout" Visible="true">
                                                                        <ItemStyle Width="12%" />
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="IsDropOut" runat="server" AutoPostBack="true" OnCheckedChanged="IsDropOut_OnCheckedChanged" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Dropout Reason" Visible="true">
                                                                        <ItemStyle Width="18%" />
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlDropoutReason" runat="server" Enabled="false" class="form-control"
                                                                                AutoPostBack="true" OnSelectedIndexChanged="DropoutReason_OnSelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Other Dropout Reason">
                                                                        <ItemStyle Width="15%" />
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtOtherDropoutReason" runat="server" Enabled="false" Text='<%# Bind("Sub_reason") %>'
                                                                                class="form-control" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <div class="row" runat="server" id="btndiv" visible="false">
                                                    <div class="thumbnail" style="float: left; width: 100%;">
                                                        <div class="col-lg-4 col-md-4 col-xs-12 col-sm-6  col-lg-offset-4 col-md-offset-4 col-sm-offset-3 col-xs-offset-0  ">
                                                            <asp:ImageButton ID="btnSUmbit" ToolTip="Save" OnClientClick="return checkdata();"
                                                                OnClick="btnSumbit_Click" ValidationGroup="saves" ImageUrl="~/images/Sumbit.jpg"
                                                                runat="server" />
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
            <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Hdn_model4"
                PopupControlID="pnlpopup4" CancelControlID="CancelButton" BackgroundCssClass="modalBackground">
            </ajax:ModalPopupExtender>
            <asp:HiddenField ID="Hdn_model4" runat="server" />
            <asp:Panel ID="pnlpopup4" runat="server" Style="display: none;">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-header" style="height: 0px;">
                            <asp:ImageButton ID="CancelButton" ImageUrl="~/images/close-29.png" runat="server"
                                CssClass="btn bgm-cyan pull-right" Text="Close" ToolTip="Close" Style="margin-right: 5px; padding: 0px;"></asp:ImageButton>
                            <asp:ImageButton ID="ImageButton9" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                OnClick="ImageButton9_Click" ToolTip="Save" ValidationGroup="Save2" ImageUrl="~/images/save-29-1.png"
                                Style="margin-right: 5px; padding: 0px;" runat="server" />
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="row">
                            <div class="row marg search-bg">
                                <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                                    <div class="col-lg-8 col-md-8  col-sm-8 cpl-xs-12">
                                        <div class="form-group">
                                            <label class="control-label col-sm-4" for="Name">
                                                Prerak Name</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtMPrerakName" MaxLength="70" runat="server" class="form-control" />
                                                <asp:RequiredFieldValidator ID="vtxtMPrerakCode" ValidationGroup="Save2" runat="server" ControlToValidate="txtMPrerakName"
                                                    ErrorMessage="*" />
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-sm-4" for="Name">
                                                Prerak Code</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtMPrerakCode" MaxLength="70" autocomplete="off" ondrop="return false;"
                                                    runat="server" class="form-control" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save2" runat="server" ControlToValidate="txtMPrerakCode"
                                                    ErrorMessage="*" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
