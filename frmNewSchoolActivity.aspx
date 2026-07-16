<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmNewSchoolActivity.aspx.cs"
    Culture="en-GB" MasterPageFile="~/Site.master" Inherits="frmNewSchoolActivity" %>

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

        .que {
            float: left;
            width: 40px;
        }

            .que label {
                padding-left: 3px;
                vertical-align: middle;
            }

        .tabl-que tr th, .tabl-que tr td {
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript">
        function checkdataSmc() {
            debugger;
            var gender = document.getElementById('<%=ddlSgender.ClientID %>').value;
            var GirlChildName = document.getElementById('<%=txtMemberSC.ClientID %>').value;
            var Mobil2 = document.getElementById('<%=txtmobile.ClientID %>').value;


            var str = "";

            if (GirlChildName == "") {
                str = str + "\n Please Fill Member Name";
            }
            if (Mobil2 != "") {
                if (Mobil2.length == 10) {

                }
                else {
                    str = str + "\n mobile number should be 10 digit ";
                }

            }

            if (gender == "0") {
                str = str + "\n Please Select Gender";
            }

            if (str != "") {
                alert(str);
                return false;
            }

        }
        function checkdata() {
            debugger;
            var gender = document.getElementById('<%=ddlgender.ClientID %>').value;
            var Registration = document.getElementById('<%=txtRegistration.ClientID %>').value;
            var dobavail = document.getElementById('<%=ddldobavail.ClientID %>').value;

            var GirlChildName = document.getElementById('<%=txtGirlChildName.ClientID %>').value;
            var Category = document.getElementById('<%=ddlCategory.ClientID %>').value;
            var Fathername = document.getElementById('<%=txtFathername.ClientID %>').value;
            var Class = document.getElementById('<%=ddlClass.ClientID %>').value;
            var SRNumber = document.getElementById('<%=txtSRNumber.ClientID %>').value;
            var str = "";
            if (Registration == "") {
                str = " Please Select Registration date";
            }
            if (GirlChildName == "") {
                str = str + "\n Please Fill Girl Child Name";
            }
            if (Fathername == "") {
                str = str + "\n Please Fill Father’s Name";
            }
            if (gender == "0") {
                str = str + "\n Please Select Gender";
            }
            if (dobavail == "0") {
                str = str + "\n Please Select DOB Availability";
            }
            if (dobavail == "1") {
                if (DOB == "") {
                    var DOB = document.getElementById('<%=txtDOB.ClientID %>').value;
                    str = str + "\n Please Fill DOB";
                }

            }
            if (dobavail == "2") {
                var Age = document.getElementById('<%=txtage.ClientID %>').value;
                if (Age == "") {
                    str = str + "\n Please Fill Age";
                }
                if (Age != "" && (Number(Age) > 18 || Number(Age) < 9)) {


                    str = str + "\n Please Fill Age between 9 to 18";
                }
            }

            if (Category == "0") {
                str = str + "\n Please Select Social Category";
            }

            if (Class == "0") {
                str = str + "\n Please Select Class";
            }
            if (SRNumber == "") {
                str = str + "\n Please Fill SR Number";
            }

            if (str != "") {
                alert(str);
                return false;
            }
        }


        function test() {
            debugger;
            var dobavail1 = document.getElementById('<%=ddldobavail.ClientID %>').value;


            if (dobavail1 == "1") {

                $('#ctl00_MainContent_lblage').innerHTML.style.display = "none";
                $('#ctl00_MainContent_lblDOB').innerHTML.style.display = "block";
                $('#ctl00_MainContent_txtDOB').innerHTML.style.display = "block";
                $('#ctl00_MainContent_txtage').innerHTML.style.display = "none";
                var x = document.getElementById('ctl00_MainContent_lblDOB');
                x.style.display = 'block'
            }
            else if (dobavail1 == "2") {

                $('#ctl00_MainContent_lblage').innerHTML.style.display = "block";
                $('#ctl00_MainContent_lblDOB').innerHTML.style.display = "none";
                $('#ctl00_MainContent_txtDOB').innerHTML.style.display = "none";
                $('#ctl00_MainContent_txtage').innerHTML.style.display = "block";


            }
            return false;
        }
        function Checkage() {
            debugger;
            var dobavail1 = document.getElementById('<%=ddldobavail.ClientID %>').value;
            var age = document.getElementById('<%=txtage.ClientID %>').value;


            var str = "";

            if (dobavail1 == "2" && age == "") {
                str = str + "\n Please Fill Age";
            }
            if (age != "" && (Number(age) > 18 || Number(age) < 9)) {

                str = str + "\n Please Fill Age between 9 to 18";
                document.getElementById('<%=txtage.ClientID %>').value = "";
            }
            if (str != "") {
                alert(str);
                return false;
            }
        }


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

//                if (Lngg.toLowerCase().indexOf("others (specify)") >= 0) {

//                    $('#<%=TxtSmcOther.ClientID %>').val('');
//                    $('#<%=TxtSmcOther.ClientID %>').attr('disabled', false);
//                }
//                else {

//                    $('#<%=TxtSmcOther.ClientID %>').val('');
//                    $('#<%=TxtSmcOther.ClientID %>').attr('disabled', true);
                //                }
            }
            if (Flag == 'F1') {
                if (maxSelection <= 10) {
                    $('#<%=hdntxt_pbname1_ID.ClientID %>').val(lid);
                    $('#<%=hdntxt_pbname1_Name.ClientID %>').val(Lngg);
                    $('#<%=txt_pbname1.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdntxt_pbname1_ID.ClientID %>').val('');
                    $('#<%=hdntxt_pbname1_Name.ClientID %>').val('');
                    $('#<%=txt_pbname1.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }

                if (Lngg.toLowerCase().indexOf("others (specify)") >= 0) {

                    $('#<%=TxtSmcOther.ClientID %>').val('');
                    $('#<%=TxtSmcOther.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=TxtSmcOther.ClientID %>').val('');
                    $('#<%=TxtSmcOther.ClientID %>').attr('disabled', true);
                }
            }
        }
    </script>
    <script type="text/javascript">
        function checkFilledNew(id, lablId, lblDr, lblMain) {

            var inputVal = document.getElementById(id);
            var icount = $("." + lblDr).val();
            //  var iwaterpre = $("." + lablId).val();
            var iwaterpre = $("." + lblMain).val();

            debugger;
            if (iwaterpre == 1) {
                if (icount == 0) {
                    icount = 3;
                }
                else if (icount == 1) {
                    icount = 3;
                }
                else if (icount == 2) {
                    icount = 3;
                }
                if (icount == 3) {

                    inputVal.style.backgroundColor = "Green"
                    //btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);

                    icount++;
                    $("." + lblDr).val(icount);
                    $("." + lablId).val(1);


                }
                else if (icount == 4) {

                    inputVal.style.backgroundColor = "Blue"


                    $("." + lablId).val(4);
                    icount = 3;
                    $("." + lblDr).val(icount);
                }

            }
            else if (iwaterpre == 2) {
                if (icount == 0) {
                    icount = 2;
                }
                else if (icount == 1) {
                    icount = 2;
                }
                if (icount == 1) {

                    //    btn_water.setBackgroundResource(R.drawable.bg_buttonroundred);
                    icount++;
                    inputVal.style.backgroundColor = "Red"


                    $("." + lablId).val(3);

                    $("." + lblDr).val(icount);
                }
                else if (icount == 2) {

                    //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundorane);


                    inputVal.style.backgroundColor = "Orange"

                    icount++;


                    $("." + lablId).val(2);

                    $("." + lblDr).val(icount);
                }
                else if (icount == 3) {

                    //btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);
                    icount = 2;

                    inputVal.style.backgroundColor = "Green"


                    $("." + lablId).val(1);
                    $("." + lblDr).val(icount);
                }

            }
            else if (iwaterpre == 3) {

                if (icount == 0) {

                    icount = 3;
                }

                if (icount == 2) {

                    //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundorane);
                    icount = 4;


                    $("." + lblDr).val(icount);
                    inputVal.style.backgroundColor = "Orange"


                    $("." + lablId).val(2);

                }
                //                         if (icount == 1) {

                //                             //  btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);

                //                             icount = 0;
                //                           
                //                             $("." + lblDr).val(icount);

                //                             inputVal.style.backgroundColor = "Green"


                //                             $("." + lablId).val(1);
                //                         }

                else

                    if (icount == 1) {

                        //  btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);

                        icount = 0;

                        $("." + lblDr).val(icount);

                        inputVal.style.backgroundColor = "Green"


                        $("." + lablId).val(1);
                    }
                    else if (icount == 2) {

                        //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundorane);
                        icount++;

                        $("." + lblDr).val(icount);
                        inputVal.style.backgroundColor = "Orange"


                        $("." + lablId).val(2);

                    }
                    else if (icount == 3) {

                        //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundorane);
                        inputVal.style.backgroundColor = "Red"

                        icount = 2;
                        $("." + lablId).val(3);
                        $("." + lblDr).val(icount);

                    }
                    else if (icount == 4) {

                        //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundblue);
                        //2020/04/15
                        //inputVal.style.backgroundColor = "Blue"

                        icount = 1;
                        $("." + lablId).val(4);

                        $("." + lblDr).val(icount);
                    }


            }
            else if (iwaterpre == 4) {
                if (icount == 0) {
                    icount = 4;
                }
                else if (icount == 1) {
                    icount = 4;
                }
                else if (icount == 2) {
                    icount = 4;
                }
                if (icount == 3) {
                    //btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);

                    inputVal.style.backgroundColor = "Green"

                    icount++;
                    $("." + lablId).val(1);

                    $("." + lblDr).val(icount);
                }
                else if (icount == 4) {

                    //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundblue);

                    inputVal.style.backgroundColor = "Blue"


                    $("." + lablId).val(4);
                    icount = 3;
                    $("." + lblDr).val(icount);
                }

            }
            else {
                if (icount == 1) {

                    //btn_water.setBackgroundResource(R.drawable.bg_buttonroundred);
                    icount++;


                    inputVal.style.backgroundColor = "Red"


                    $("." + lablId).val(3);
                    $("." + lblDr).val(icount);
                }
                else if (icount == 2) {

                    //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundorane);
                    icount++;



                    inputVal.style.backgroundColor = "Orange"


                    $("." + lablId).val(2);
                    $("." + lblDr).val(icount);
                }
                else if (icount == 3) {

                    // btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);

                    // btn_water.setBackgroundResource(R.drawable.green_btn_radio_holo_light);
                    icount++;



                    inputVal.style.backgroundColor = "Green"


                    $("." + lablId).val(1);
                    $("." + lblDr).val(icount);
                }
                else if (icount == 4) {
                    //  btn_water.setBackgroundResource(R.drawable.bg_buttonroundblue);

                    // btn_water.setBackgroundResource(R.drawable.purple_btn_radio_holo_light);
                    icount++;




                    inputVal.style.backgroundColor = "Blue"


                    $("." + lablId).val(4);
                    icount = 0;
                    $("." + lblDr).val(icount);
                }
                else if (icount == 0) {

                    inputVal.style.backgroundColor = "White"
                    /// btn_water.setBackgroundResource(R.drawable.bg_buttonroundwhite);

                    icount++;
                    $("." + lblDr).val(icount);

                }

            }

        }
    </script>
    <script type="text/javascript">
        function checkFilled(id, lablId) {
            var inputVal = document.getElementById(id);

            if (inputVal.style.backgroundColor == "white") {
                inputVal.style.backgroundColor = "RED";
                $("." + lablId).val(3);
            }

            else if (inputVal.style.backgroundColor == "RED") {
                inputVal.style.backgroundColor = "Orange";
                $("." + lablId).val(2);

            }
            else if (inputVal.style.backgroundColor == "Orange") {

                inputVal.style.backgroundColor = "Blue";
                $("." + lablId).val(4);

            }
            else if (inputVal.style.backgroundColor == "Blue") {
                inputVal.style.backgroundColor = "Green";
                $("." + lablId).val(1);
            }
            else if (inputVal.style.backgroundColor == "Green") {
                inputVal.style.backgroundColor = "white";

            }
            else {
                inputVal.style.backgroundColor = "white";

            }
        }
    </script>
    <script type="text/javascript">




        function DiscCode(inputtxt) {

            var lodgingtot = document.getElementById('<%=txtOtherSIPFC.ClientID %>');



            if (lodgingtot.value <= 4) {

                return true;
            }
            else {

                lodgingtot.value = '';
                alert("Please ensure that  Critical SIP prepared number should be less than 4 ");

                return false;
            }
        }

    </script>
    <script type="text/javascript">


        function SMC(inputtxt, ID) {

            var lodgingtot1 = document.getElementById('<%=txtSMCMeeting.ClientID %>');

            var lodgingtot = inputtxt;

            if (inputtxt <= 25) {

                return true;
            }
            else {


                alert("Please ensure that  number should be less than 25 ");

                // var inputVal = document.getElementById(ID);
                lodgingtot1.value = '';
                return false;
            }
        }
        function SMCSep(inputtxt, ID) {

            var lodgingtot1 = document.getElementById('<%=txtSepSMCMeeting.ClientID %>');

            var lodgingtot = inputtxt;

            if (inputtxt <= 25) {

                return true;
            }
            else {


                alert("Please ensure that  number should be less than 25 ");

                // var inputVal = document.getElementById(ID);
                lodgingtot1.value = '';
                return false;
            }
        }
    </script>
    <script type="text/javascript">


        function SMCOrient(inputtxt) {

            var lodgingtot = document.getElementById('<%=txtTotalMember.ClientID %>');


            if (lodgingtot.value <= 16) {

                return true;
            }
            else {

                lodgingtot.value = '';
                alert("Please ensure that  Total Trained Member number should be less than 16 ");

                return false;
            }
        }

    </script>
    <script type="text/javascript">


        function SMCOrientNew1(inputtxt) {
            var lodgingtot = document.getElementById('<%=txtTotalFmember.ClientID %>');

            if (lodgingtot.value >= 6) {
            }
            else {
                lodgingtot.value = '';
                alert("Please ensure that Total Trained Female Member number should be greater than 6 ");

                return false;
            }

            if (lodgingtot.value <= 16) {

                return true;
            }
            else {

                lodgingtot.value = '';
                alert("Please ensure that Total Trained Female Member number should be less than 16 ");

                return false;
            }
        }

    </script>
    <script type="text/javascript">


        function SMCOrientNew(inputtxt) {

            var lodgingtot = document.getElementById('<%=txtTotalFmember.ClientID %>');


            if (lodgingtot.value <= 16) {

                return true;
            }
            else {

                lodgingtot.value = '';
                alert("Please ensure that Total Trained Female Member number should be less than 16 ");

                return false;
            }
        }

    </script>
    <script type="text/javascript">


        function OtherSIp(inputtxt) {

            var lodgingtot = document.getElementById('<%=txtsmcmeetinFC.ClientID %>');



            if (lodgingtot.value <= 6) {

                return true;
            }
            else {

                lodgingtot.value = '';
                alert("Please ensure that  Other Critical SIP prepared number should be less than 6  ");

                return false;
            }
        }
        function OtherMember(inputtxt) {

            var lodgingtot = document.getElementById('<%=txtmembers.ClientID %>');



            if (lodgingtot.value <= 18) {

                return true;
            }
            else {

                lodgingtot.value = '';
                alert("Please ensure that  physical present number should be less than 18  ");

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
    <style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
        }

        .modalpopupcss {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }

        .modalPopup {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
            z-index: 999999;
        }
    </style>
    <style type="text/css">
        .ajax__calendar .ajax__calendar_invalid .ajax__calendar_day {
            background-color: gray;
            color: White;
            text-decoration: none;
            cursor: default;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="row">

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="row marg search-bg">
                        <div class="form-horizontal" style="padding: 10px;">
                            <div class="col-lg-3 col-md-2 col-sm-2 cpl-xs-12">
                                <div class="form-group" style="margin-bottom: 7px;">
                                    <label for="email" class="col-sm-3 padd linhei">
                                        FC:</label>
                                    <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlUser" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged"
                                            runat="server" AutoPostBack="true" class="form-control ">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-2 col-sm-2 cpl-xs-12">
                                <div class="form-group" style="margin-bottom: 7px;">
                                    <label for="email" class="col-sm-3 padd linhei">
                                        Village:</label>
                                    <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlVilage" OnSelectedIndexChanged="ddlVilage_SelectedIndexChanged"
                                            runat="server" AutoPostBack="true" class="form-control " />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-2 col-sm-2 cpl-xs-12">
                                <div class="form-group" style="margin-bottom: 7px;">
                                    <label for="email" class="col-sm-3 padd linhei">
                                        Date:</label>
                                    <div class="col-sm-9 padd">
                                        <asp:TextBox runat="server" ID="txtDate" OnTextChanged="txtchdate" AutoPostBack="true" autocomplete="off" ondrop="return false;"
                                            class="form-control" onkeypress="return false;"></asp:TextBox>
                                        <%--  OnClientDateSelectionChanged="arrivaldatecheck"--%>
                                        <ajax:CalendarExtender ID="CalendarExtenderTourdate"
                                            runat="server" Enabled="True" Format="dd/MM/yyyy" OnClientDateSelectionChanged="arrivaldatecheck" TargetControlID="txtDate" PopupPosition="BottomRight">
                                        </ajax:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="ReqTxtDate" runat="server" ControlToValidate="txtDate"
                                            Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                            SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
                                        <span id="ctl00_MainContent_ReqTxtDate" style="color: Red; font-size: 9px; font-weight: normal; display: none;">*</span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-2 col-sm-2 cpl-xs-12">
                                <div class="form-group" style="margin-bottom: 7px;">
                                    <label for="email" class="col-sm-3 padd linhei">
                                        School:</label>
                                    <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlSchool" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged"
                                            AutoPostBack="true" runat="server" class="form-control " />
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-3 col-md-2 col-sm-2 cpl-xs-12" runat="server" id="divMar">
                                <div class="form-group" style="margin-bottom: 7px;">
                                    <asp:Label for="email" class="col-sm-3 padd linhei" Text="School Merge:" runat="server" ID="lblend">
                                       </asp:Label>
                                    <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlMarge" runat="server" class="form-control "
                                            OnSelectedIndexChanged="ddlMarge_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">PS School</asp:ListItem>
                                            <asp:ListItem Value="2">UPS School </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-2 col-sm-2 cpl-xs-12">

                                  <asp:ImageButton ID="btnEdit" ToolTip="Edit" OnClick="btnEdit_Click" runat="server"
                                    class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1" Style="padding-top: 6px;" ImageUrl="~/images/edit.png" />

                                 <asp:ImageButton ID="btnSerach" OnClick="btnSerach_Click" ToolTip="Serach" runat="server"
                                    class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1" Style="padding-top: 4px;" ImageUrl="~/images/search-29.png" />

                               <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-left" OnClick="btnSave_Click"
                                    BackColor="#f5f5f5" ToolTip="Save" ImageUrl="~/images/save-29-1.png" ValidationGroup="saves"
                                    Style="margin-right: 5px;margin-bottom: 4px;" runat="server" />


                                <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-left"
                                    ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px;"
                                    runat="server" />

                               
                               

                                <asp:Button ID="btnApprove" CssClass="btn btn-success pull-left " ToolTip="Save"
                                    Text="  Back" OnClick="btnApprove_Click" Style="margin-right: 5px;margin-top: 3px;"
                                    runat="server" />
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="row" id="idImage" runat="server" visible="false">
                <%-- <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>--%>
                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                    <div class="form-group" style="margin-bottom: 7px;">
                        <label for="email" class="col-sm-3 padd linhei">
                            Image :</label>
                        <div class="col-sm-4 padd" style="padding-left: 31px;">
                            <asp:ImageButton ID="imgComm1" runat="server" Width="30" Height="25" OnClick="btnimgComm1_Click"
                                Visible="false" CssClass="pull-right" ImageUrl="~/images/iconimage-128.png"></asp:ImageButton>
                            <asp:Label ID="lblMM" Visible="false" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                </div>

                <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
            </div>
            <asp:Panel ID="pnlMain" runat="server">
                <%--  <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
            <ContentTemplate>--%>
                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" style="padding: 0px;">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                <span class="">
                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                        T.B. Handholding
                                    </p>
                                </span>
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="Div6">
                            <div style="overflow: auto;">
                                <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td colspan="3">
                                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                    <asp:CheckBox ID="chkHolding" runat="server" />
                                                    T.B. Handholding
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>

                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" runat="server" visible="false" style="padding: 0px;">
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                    <span class="">
                                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                            Enrolled/Ineligible
                                        </p>
                                    </span>
                                </button>
                            </div>
                            <div class="collapse navbar-collapse" id="Div7">
                                <div>
                                    <table class="table table-striped table-bordered table-hover">
                                        <tbody>
                                            <tr>
                                                <td colspan="2">
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Enrolled/Ineligible
                                                    </p>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkEnrool" OnClick="lnkEnrool_OnClick" Visible="false" runat="server">Click Here</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" style="padding: 0px;">
                        <div class="navbar-header">

                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                <span class="">
                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                        Others
                                    </p>
                                </span>
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="Div9" >
                            <div style="overflow: auto; min-height: 65px">
                                <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                    <asp:RadioButton ID="chkother" Enabled="false" runat="server" />
                                                    Other - Specify
                                                </p>
                                            </td>
                                            <td style="padding-left: 33px;">
                                                <asp:RadioButton ID="rblothertb" GroupName="H" runat="server" />

                                                TB
                                            </td>
                                            <td style="padding-left: 33px;">
                                                <asp:RadioButton ID="rblotherfc" GroupName="H" runat="server" />
                                                FC
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 7px;">
                                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                    Others
                                                </p>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtOther" Style="height: 22px;" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="Hdn_model3"
                    PopupControlID="pnlpopup3" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:HiddenField ID="Hdn_model3" runat="server" />
                <asp:Panel ID="pnlpopup3" runat="server" Style="display: none;">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                    ToolTip="Add" ImageUrl="~/images/close-29.png" Style="margin-right: 5px; padding: 0px;"
                                    runat="server" />
                                <asp:ImageButton ID="ImageButton10" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                    ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnClose_Click" Style="margin-right: 5px; padding: 0px;"
                                    runat="server" />
                                <h4 class="modal-title">D2D</h4>
                            </div>
                            <div class="row">
                                <div class="row marg search-bg">
                                    <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                                        <div class="col-lg-6 col-md-6 col-sm-6 cpl-xs-12">
                                            <div class="form-group" style="margin-bottom: 2px;">
                                                <label for="email" class="col-sm-3 padd linhei">
                                                    Search:</label>
                                                <div class="col-sm-9 padd">
                                                    <asp:DropDownList ID="ddlSearch" runat="server" class="form-control">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Unique no</asp:ListItem>
                                                        <asp:ListItem Value="2">HH Code </asp:ListItem>
                                                        <asp:ListItem Value="3">Child Name</asp:ListItem>
                                                        <asp:ListItem Value="4">Father Name</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 cpl-xs-12">
                                            <div class="form-group" style="margin-bottom: 7px;">
                                                <div class="col-sm-10 padd">
                                                    <asp:TextBox runat="server" ID="txtSearch" autocomplete="off" ondrop="return false;"
                                                        class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-2  col-sm-2 cpl-xs-12">
                                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                            <asp:ImageButton ID="ImageButton8" OnClick="btnD2dSerach_Click" ToolTip="Serach"
                                                runat="server" class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1"
                                                ImageUrl="~/images/search-29.png" Style="margin-left: -49px; padding: 0px;" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row table-responsive">
                                <div style="overflow: auto; margin-top: 35px; height: 437px;">
                                    <asp:GridView ID="Gv_Display" Width="100%" runat="server" OnRowDataBound="Gv_Display_RowDataBound"
                                        CssClass=" table table-striped table-bordered table-hover " AutoGenerateColumns="false">
                                        <EmptyDataTemplate>
                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                                Data not found
                                            </div>
                                        </EmptyDataTemplate>
                                        <FooterStyle CssClass="FooterStyle" />
                                        <HeaderStyle BackColor="#C1C1C1" Height="44px" />
                                        <RowStyle HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#897A7A" Font-Bold="True" ForeColor="WhiteSmoke" />
                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Unique Code" HeaderStyle-CssClass="GridHeaderClass">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUniqueCode" runat="server" Text='<%#Eval("UniqueId") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HH No." HeaderStyle-CssClass="GridHeaderClass">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text='<%#Eval("HHNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Child Name" HeaderStyle-CssClass="GridHeaderClass">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbtn_ProducerD" Text='<%#Eval("ChildName") %>' Style="text-decoration: none;"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Father name" DataField="FathersName" HeaderStyle-CssClass="GridHeaderClass">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Contact" HeaderStyle-CssClass="GridHeaderClass">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlStatus" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                                                        AutoPostBack="true" runat="server" class="form-control">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">C-Contact </asp:ListItem>
                                                        <asp:ListItem Value="2">F-Follow up</asp:ListItem>
                                                        <asp:ListItem Value="3">I-Ineligible </asp:ListItem>
                                                        <asp:ListItem Value="4">P-Pending Format 6</asp:ListItem>
                                                        <asp:ListItem Value="5">E-Enrolled</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Label runat="server" Visible="false" ID="lbStatus" Text='<%#Eval("Status") %>'
                                                        Style="text-decoration: none;"></asp:Label>
                                                    <asp:Label runat="server" Visible="false" ID="lbStatusNew" Text='<%#Eval("Status") %>'
                                                        Style="text-decoration: none;"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TBorFC" HeaderStyle-CssClass="GridHeaderClass">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rblTBFC" runat="server">
                                                        <asp:ListItem Selected="True" Value="1">FC</asp:ListItem>
                                                        <asp:ListItem Value="2">TB</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" HeaderStyle-CssClass="GridHeaderClass">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbUniqueCode" Text='<%#Eval("UniqueCode") %>' Style="text-decoration: none;"></asp:Label>
                                                    <asp:Label runat="server" ID="lblTBFC" Text='<%#Eval("TBorFC") %>' Style="text-decoration: none;"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="modal-footer">
                            </div>
                        </div>
                    </div>
                </asp:Panel>
     
                <div class="row">
                    <asp:Panel ID="pnlSmc" runat="server">
                    
                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                    <span class="">
                                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                            SMC Meeting
                                        </p>
                                    </span>
                                </button>
                            </div>
                            <asp:Label ID="lblCom1" runat="server" Visible="false" Text="Label"></asp:Label>
                            <asp:Label ID="lblCom22" runat="server" Visible="false" Text="Label"></asp:Label>
                           <div class="row">
                               <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">

                                     <div class="collapse navbar-collapse" id="Div4" style="padding: 0px;">
                                <div class="thumbnail" style="height: 344px; overflow: auto">
                                    <asp:ImageButton ID="ImageButton1" runat="server" OnClick="btnSmc_Click" Width="30"
                                        Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                    <table class="table table table-bordered table-hover">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        <asp:CheckBox ID="chkSMC" runat="server"  OnCheckedChanged="rblTbr_Click" AutoPostBack="true" />
                                                        SMC Meeting
                                                    </p>
                                                </td>
                                                <td style="padding-left: 33px;">
                                                    <asp:RadioButton ID="rblSMCTB" OnCheckedChanged="rblTb_Click" AutoPostBack="true" GroupName="smcTB" CssClass="radio" runat="server" />
                                                    <%--<input name="" value="" type="radio">--%>
                                                    Team Baika
                                                </td>
                                                <td style="padding-left: 33px;">
                                                    <asp:RadioButton ID="rblSMCFC" OnCheckedChanged="rblTb_Click" AutoPostBack="true" GroupName="smcTB" CssClass="radio" runat="server" />
                                                    FC
                                                </td>
                                            </tr>

                                            <tr runat="server" id="trGssId" visible="false">
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Team Baika Name
                                                    </p>
                                                </td>
                                                <td colspan="2">
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlGssTbname"></asp:DropDownList>
                                                </td>

                                            </tr>

                                             <tr runat="server" id="tr3" >
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                       Meeting Agenda Prepared by
                                                    </p>
                                                </td>
                                                <td colspan="2">
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlMeetingPrepare"></asp:DropDownList>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Presence of SMC President
                                                    </p>
                                                </td>
                                                <td style="padding-left: 33px;">
                                                    <asp:RadioButton ID="rdPSMCPY" GroupName="Psmc" CssClass="radio" runat="server" />
                                                    <%--<input name="" value="" type="radio">--%>
                                                    Yes
                                                </td>
                                                <td style="padding-left: 33px;">
                                                    <asp:RadioButton ID="rdPSMCPN" GroupName="Psmc" CssClass="radio" runat="server" />
                                                    No
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Is  SMC register available in the school?
                                                    </p>
                                                </td>
                                                <td style="padding-left: 33px;">
                                                    <asp:RadioButton ID="rdRegisterY" GroupName="RegisterP" CssClass="radio" runat="server" />
                                                    <%--<input name="" value="" type="radio">--%>
                                                    Yes
                                                </td>
                                                <td style="padding-left: 33px;">
                                                    <asp:RadioButton ID="rdRegisterN" GroupName="RegisterP" CssClass="radio" runat="server" />
                                                    No
                                                </td>
                                            </tr>


                                            <tr>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Is Team Baika available in meeting?
                                                    </p>
                                                </td>
                                                <td style="padding-left: 33px;" runat="server" id="k1" visible="false">
                                                    <asp:RadioButton ID="rdTeamY" GroupName="TrP" OnCheckedChanged="rblisTb_Click" AutoPostBack="true" CssClass="radio" runat="server" />

                                                    Yes
                                                </td>
                                                <td style="padding-left: 33px;" runat="server" id="k2" visible="false">
                                                    <asp:RadioButton ID="rdTeamN" GroupName="TrP" OnCheckedChanged="rblisTb_Click" AutoPostBack="true" CssClass="radio" runat="server" />
                                                    No
                                                </td>
                                            </tr>
                                            <tr runat="server" id="tre1" visible="false">
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Team Baika Name
                                                    </p>
                                                </td>
                                                <td colspan="2">
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlMMTb"></asp:DropDownList>
                                                </td>

                                            </tr>
                                            <tr>

                                                <td colspan="3">


                                                    <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;" ID="Label3" Text="Objective of Meeting" runat="server"></asp:Label>

                                                    <asp:CheckBoxList ID="CBL_bookformat" runat="server" CssClass="chkBoxList _bookformat" onclick="SetMultilanguage('F','_bookformat');" RepeatColumns="2" RepeatDirection="Vertical"></asp:CheckBoxList>

                                                    <asp:TextBox ID="txt_pbname" Visible="false" autocomplete="off" ondrop="return false;" runat="server"
                                                        CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                    <cc1:PopupControlExtender ID="PopupControltxt_pbname" runat="server" TargetControlID="txt_pbname"
                                                        PopupControlID="pnt_bookformat" OffsetY="22">
                                                    </cc1:PopupControlExtender>
                                                    <asp:Panel ID="pnt_bookformat" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 40.5%"
                                                        CssClass="panel">
                                                        <span>
                                                            <asp:CheckBoxList ID="CBL_bookformat77" CssClass="_bookformat radio" runat="server"
                                                                onclick="SetMultilanguage('F','_bookformat');">
                                                            </asp:CheckBoxList>
                                                        </span>
                                                        <asp:HiddenField runat="server" ID="hdn_PBName" />
                                                        <asp:HiddenField runat="server" ID="hdn_PBID" />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>

                                                <td colspan="3">
                                                    <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;" ID="Label4" Text="Main Discussion point" runat="server"></asp:Label>

                                                    <asp:CheckBoxList ID="CBL_bookformat1" runat="server" CssClass="chkBoxList _bookformat1" onclick="SetMultilanguage('F1','_bookformat1');" RepeatColumns="2" RepeatDirection="Vertical"></asp:CheckBoxList>

                                                    <asp:TextBox ID="txt_pbname1" Visible="false" autocomplete="off" ondrop="return false;" runat="server"
                                                        CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
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
                                                    <asp:TextBox ID="TxtSmcOther" Enabled="false" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>

                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="width: 60%;">Critical SIP prepared
                                                </td>

                                                <td colspan="2">
                                                    <asp:TextBox ID="txtOtherSIPFC" onchange="javascript:DiscCode(this.value);" autocomplete="off"
                                                        ondrop="return false;" MaxLength="1" onkeypress="return isNumberKey(this,event);"
                                                        runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 60%;">Other Critical SIP prepared
                                                </td>

                                                <td colspan="2">
                                                    <asp:TextBox ID="txtsmcmeetinFC" onchange="javascript:OtherSIp(this.value);" autocomplete="off"
                                                        ondrop="return false;" MaxLength="1" onkeypress="return isNumberKey(this,event);"
                                                        runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr runat="server" visible="false">
                                                <td style="width: 60%;">Total SMC Members
                                                </td>

                                                <td colspan="2">
                                                    <asp:TextBox ID="txtTotalMember"
                                                        autocomplete="off" ondrop="return false;" MaxLength="2" onkeypress="return isNumberKey(this,event);"
                                                        runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr runat="server" visible="false">
                                                <td style="width: 60%;">Female Members
                                                </td>

                                                <td colspan="2">
                                                    <asp:TextBox ID="txtTotalFmember"
                                                        autocomplete="off" ondrop="return false;" MaxLength="2" onkeypress="return isNumberKey(this,event);"
                                                        runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>

                                            </tr>
                                                <tr runat="server" id="tr1">
                                                    <td>
                                                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                            In which type of SMC register the meeting details are entered?
                                                        </p>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlrec"></asp:DropDownList>
                                                    </td>

                                                </tr>
                                            </tr>
                                             <tr runat="server" id="tr2">
                                                 <td>
                                                     <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                         Whether the meeting date along with the meeting details is entered in the meeting register?
                                                     </p>
                                                 </td>
                                                 <td colspan="2">
                                                     <asp:DropDownList runat="server" CssClass="form-control" ID="ddlDatemeeting"></asp:DropDownList>
                                                 </td>

                                             </tr>

                                            <tr>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Has the proposal taken in the meeting been written in the meeting register of the school as per Form-5
                                                    </p>
                                                </td>
                                                <td colspan="2">
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlWrite">

                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Whether all the members have signed in the register of the school as per the signature in Form-05?
                                                    </p>
                                                </td>
                                                <td colspan="2">
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlF5">

                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                           

                                            <tr>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                    </p>
                                                </td>
                                            
                                            </tr>
                                             
                                        </tbody>
                                    </table>

                                    <table class="table table-striped table-bordered table-hover" style="display: none;">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        <asp:CheckBox ID="chkNewSmc" runat="server" />
                                                        SMC Orientation
                                                    </p>
                                                </td>
                                                <td style="padding-left: 33px;">
                                                    <asp:RadioButton ID="rblSmcNew" GroupName="smcTB1" CssClass="radio" runat="server" />
                                                    <%--<input name="" value="" type="radio">--%>
                                                    TB
                                                </td>
                                                <td style="padding-left: 33px;">
                                                    <asp:RadioButton ID="rblSmcNew1" GroupName="smcTB1" CssClass="radio" runat="server" />
                                                    FC
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>


                               </div>
                                    <div class="navbar-header">
                                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                    <span class="">
                                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                            SMC Meeting Attention 
                                        </p>
                                    </span>
                                </button>
                            </div>
                               <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">

                                      <div class="collapse navbar-collapse" id="Div4" style="padding: 0px;">
                                <div class="thumbnail" style="height: 344px; overflow: auto">

                                    <table class="table table table-bordered table-hover">
                                         <tr><td colspan="3">    <p class="text-danger" style="margin: 0px; font-weight: bold;">SMC Meeting Attention </p></td></tr>
                                         <tr>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        How many members were physically present in the meeting
                                                    </p>
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox runat="server" CssClass="form-control" onchange="javascript:OtherMember(this.value);" MaxLength="2" onkeypress="return isNumberKey(this,event);" ID="txtmembers">

                                                    </asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="3">


                                                    <asp:GridView ID="gvSmc" runat="server"  OnRowDataBound="gv_scOnDataBound" CssClass="table table-striped table-bordered table-hover"
                                                        DataKeyNames="UniqueCode" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="12px"
                                                        Width="100%">
                                                        <EmptyDataTemplate>
                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                Data not found
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtn" runat="server" Text="EDIT" OnClick="LnkBtnBlockSMC_OnClick"
                                                                        CommandArgument='<%# Bind("UniqueCode") %>'></asp:LinkButton>
                                                                    <asp:Label ID="lblCUniqueChildCode" Visible="false" BackColor="Transparent" runat="server"
                                                                        Text='<%# Bind("UniqueCode") %>' CssClass="form-controlAbhi"></asp:Label>
                                                                     <asp:Label ID="lblStatus" Visible="false" BackColor="Transparent" runat="server"
                                                                        Text='<%# Bind("Present") %>' CssClass="form-controlAbhi"></asp:Label>
                                                                       <asp:Label ID="lblIsPrevEntry" Visible="false" BackColor="Transparent" runat="server"
                                                                        Text='<%# Bind("IsPrevEntry") %>' CssClass="form-controlAbhi"></asp:Label>
                                                                     <asp:Label ID="lblUniqueMemberCode" Visible="false" BackColor="Transparent" runat="server"
                                                                        Text='<%# Bind("UniqueMemberCode") %>' CssClass="form-controlAbhi"></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action" Visible="false" HeaderStyle-Width="15%" HeaderStyle-CssClass="GridHeaderClass">
                                                                <ItemTemplate>
                                                                          <asp:LinkButton ID="Delete_Questionttt"  OnClick="SMCDelete_OnClick" class="btn btn-sm btn-link" runat="server">
                                                                     <span class="glyphicon glyphicon-trash" data-fa-transform="shrink-10 up-.5" style="color:red"></span>
                                                                        
                                                    </asp:LinkButton>
                                                                   
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="5%" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Member Name" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" ForeColor="Black" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="padding-lef" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Gender" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLevelID" ForeColor="Black" runat="server" Text='<%# Eval("TBFC") %>'></asp:Label>
                                                                    <asp:Label ID="lblGender" Visible="false" ForeColor="Black" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="padding-lef" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Attendance">
                                                                    <ItemTemplate>

                                                                        <asp:CheckBox ID="ddlAttendanceSmc" runat="server"  />Present
                                                                                                                                <%--     <asp:DropDownList ID="ddlAttendanceSmc" runat="server" class="form-control" >
                                                                            <asp:ListItem Text="--Select--" Value="0"> </asp:ListItem>
                                                                            <asp:ListItem Text="Present" Value="1"> </asp:ListItem>
                                                                            <asp:ListItem Text="Absent" Value="2"> </asp:ListItem>
                                                                        </asp:DropDownList>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Mobile Number " Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSession" ForeColor="Black" runat="server" Text='<%# Eval("Mobile") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="padding-lef" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>


                                                </td>
                                            </tr>

                                            <td colspan="3">
                                                    <asp:ImageButton ID="ImageButton13"  CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                        ToolTip="Add" OnClick="btnAddSmc_Click"  ImageUrl="~/images/add-29-1.png" Style="margin-right: 5px; padding: 0px;"
                                                        runat="server" />

                                                </td>
                                            <tr>
                                                <td>Total :
                                                    <asp:Label ID="lblTottal" runat="server" Text=""></asp:Label></td>
                                                <td>Female :
                                                    <asp:Label ID="lblFemale" runat="server" Text=""></asp:Label></td>
                                                <td>Male :
                                                    <asp:Label ID="lblmale" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Meeting Image 
                                                </td>
                                                <td colspan="2">
                                                    <asp:ImageButton ID="imgComm2" runat="server" Width="30" Height="25" OnClick="btnimgComm2_Click"
                                                        Visible="false" CssClass="pull-right" ImageUrl="~/images/iconimage-128.png"></asp:ImageButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Image From 5
                                                </td>
                                                <td colspan="2">
                                                    <asp:ImageButton ID="imgComm22" runat="server" Width="30" Height="25" OnClick="btnimgComm22_Click"
                                                        Visible="false" CssClass="pull-right" ImageUrl="~/images/iconimage-128.png"></asp:ImageButton>
                                                </td>
                                            </tr>
                                                  </table>
                                    </div>
                                          </div>
                               </div>
                           </div>
                          
                        </div>
                     
                    </asp:Panel>
                    <asp:Panel ID="pnlClt" runat="server">
                   
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" >
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                    <span class="">
                                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                            CLT Activity
                                        </p>
                                    </span>
                                </button>
                            </div>
                            <div class="collapse navbar-collapse" id="Div2" style="padding: 0px;">
                                <div class="thumbnail" style="overflow: auto; height: 344px; padding: 10px;" >
                                    <asp:ImageButton ID="ImageButton2" Visible="false" runat="server" OnClick="btnCLT_Click" Width="30"
                                        Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                    <asp:ImageButton ID="ImageButton12" runat="server" Width="30"
                                        Height="25" CssClass="pull-right" OnClick="btnContactSchool" ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                    <table class="table table-striped table-bordered table-hover" runat="server" visible="false">
                                        <tbody>
                                            <tr runat="server">
                                                <td>GKP
                                                    <asp:CheckBox ID="chkClT" Visible="false" runat="server" />
                                                    <asp:ImageButton ID="ImageButton11" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                        ToolTip="Add" OnClick="btnAddGkp_Click" ImageUrl="~/images/add-29-1.png" Style="margin-right: 5px; padding: 0px;"
                                                        runat="server" />
                                                </td>
                                                <td style="padding-left: 33px;" runat="server" visible="false">
                                                    <asp:RadioButton ID="rblCLTTB" Visible="false" GroupName="CLTTB" CssClass="radio"
                                                        runat="server" />
                                                </td>
                                                <td style="padding-left: 33px;" runat="server" visible="false">
                                                    <asp:RadioButton ID="rblCLTFC" Visible="false" GroupName="CLTTB" CssClass="radio"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                            <tr style="text-align: center">
                                                <td>
                                                    <asp:GridView ID="gvGkp" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                        DataKeyNames="GUID_GKP" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="12px"
                                                        Width="100%">
                                                        <EmptyDataTemplate>
                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                Data not found
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtn" runat="server" Text="EDIT" OnClick="LnkBtnBlock_OnClick"
                                                                        CommandArgument='<%# Bind("GUID_GKP") %>'></asp:LinkButton>
                                                                    <asp:Label ID="lblCUniqueChildCode" Visible="false" BackColor="Transparent" runat="server"
                                                                        Text='<%# Bind("GUID_GKP") %>' CssClass="form-controlAbhi"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="15%" HeaderStyle-CssClass="GridHeaderClass">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImgAcc" runat="server" OnClick="GKPDelete_OnClick" ImageUrl="~/images/delete-29.png"
                                                                        Width="15px" Height="15px"></asp:ImageButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="5%" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SubjectName" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSubjectName" ForeColor="Black" runat="server" Text='<%# Eval("SubjectName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="padding-lef" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Level" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLevelID" ForeColor="Black" runat="server" Text='<%# Eval("LevelID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="padding-lef" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Session" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSession" ForeColor="Black" runat="server" Text='<%# Eval("Doc") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="padding-lef" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TB/FC" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTBFC" ForeColor="Black" runat="server" Text='<%# Eval("TBFC") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="padding-lef" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="subjectid" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsubjectid" ForeColor="Black" runat="server" Text='<%# Eval("SubjectID") %>'></asp:Label>
                                                                    <asp:Label ID="lblgkp_fc" ForeColor="Black" runat="server" Text='<%# Eval("gkp_fc") %>'></asp:Label>
                                                                    <asp:Label ID="lblgkp_tb" ForeColor="Black" runat="server" Text='<%# Eval("gkp_tb") %>'></asp:Label>
                                                                    <asp:Label ID="lblSessionType" ForeColor="Black" runat="server" Text='<%# Eval("SessionType") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="padding-lef" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                                <%-- <td>
                      English
                      </td>
                      <td>
                      Maths
                      </td>--%>
                                            </tr>
                                            <tr style="text-align: center" runat="server" visible="false">
                                                <td>
                                                    <asp:CheckBox ID="chkHindiA" runat="server" />
                                                    <label for="ctl00_MainContent_CheckBox11">
                                                        &nbsp;A</label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkEnglishA" runat="server" />
                                                    <label for="ctl00_MainContent_CheckBox1">
                                                        &nbsp;A</label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkMathA" runat="server" /><label for="ctl00_MainContent_CheckBox2">&nbsp;A</label>
                                                </td>
                                            </tr>
                                            <tr style="text-align: center" runat="server" visible="false">
                                                <td>
                                                    <asp:CheckBox ID="chkHindiB" runat="server" />
                                                    <label for="ctl00_MainContent_CheckBox3">
                                                        &nbsp;B</label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkEnglishB" runat="server" />
                                                    <label for="ctl00_MainContent_CheckBox4">
                                                        &nbsp;B</label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkMathB" runat="server" />
                                                    <label for="ctl00_MainContent_CheckBox5">
                                                        &nbsp;B</label>
                                                </td>
                                            </tr>
                                            <tr style="text-align: center" runat="server" visible="false">
                                                <td>
                                                    <asp:CheckBox ID="chkHindiC" runat="server" /><label for="ctl00_MainContent_CheckBox6">&nbsp;C</label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkEnglishC" runat="server" /><label for="ctl00_MainContent_CheckBox7">&nbsp;C</label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkMathC" runat="server" /><label for="ctl00_MainContent_CheckBox8">&nbsp;C</label>
                                                </td>
                                            </tr>
                                            <tr style="text-align: center" runat="server" visible="false">
                                                <td>
                                                    <asp:CheckBox ID="chkHindiD" runat="server" /><label for="ctl00_MainContent_CheckBox9">&nbsp;D</label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkEnglishD" runat="server" /><label for="ctl00_MainContent_CheckBox10">&nbsp;D</label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkMathD" runat="server" /><label for="ctl00_MainContent_CheckBox12">&nbsp;D</label>
                                                </td>
                                            </tr>
                                            <tr style="text-align: center" runat="server" visible="false">
                                                <td>
                                                    <asp:CheckBox ID="chkHindiE" runat="server" /><label for="ctl00_MainContent_CheckBox13">&nbsp;E</label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkEnglishE" runat="server" /><label for="ctl00_MainContent_CheckBox14">&nbsp;E</label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkMathE" runat="server" /><label for="ctl00_MainContent_CheckBox15">&nbsp;E</label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table class="table table table-bordered table-hover">
                                        <tbody>
                                            <tr>

                                                <td colspan="2" style="padding-left: 33px;">
                                                    <asp:RadioButton ID="rblConTB" GroupName="rTB" CssClass="radio" runat="server" />
                                                    <%--<input name="" value="" type="radio">--%>
                                                    Joint Visit with TB
                                                </td>
                                                <td style="padding-left: 33px;" runat="server" visible="false">
                                                    <asp:RadioButton ID="rblConFC" GroupName="rTrrB" CssClass="radio" runat="server" />
                                                    FC
                                                </td>
                                            </tr>
                                            <tr runat="server" visible="false">

                                                <td style="padding-left: 33px;">
                                                    <asp:RadioButton ID="rbloption1" GroupName="ROTB" CssClass="" runat="server" />
                                                    <%--<input name="" value="" type="radio">--%>
                                                    Option
                                                </td>
                                                <td style="padding-left: 33px;">
                                                    <asp:RadioButton ID="rbloption2" GroupName="ROTB" CssClass="radio" runat="server" />
                                                    Option 2
                                                </td>
                                            </tr>

                                            <tr>

                                                <td colspan="3">


                                                    <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;" ID="Label6" Text="School Contact Objective " runat="server"></asp:Label>

                                                    <asp:CheckBoxList ID="chkSchoolCOntact" runat="server" CssClass="chkBoxList _bookformat" style=" text-wrap:normal;width:100%" onclick="SetMultilanguage('F','_bookformat');" RepeatColumns="1" RepeatDirection="Vertical"></asp:CheckBoxList>


                                                </td>
                                            </tr>



                                        </tbody>
                                    </table>
                                    <table class="table table-striped table-bordered table-hover" runat="server" visible="false">
                                        <tbody>
                                            <tr>
                                                <td colspan="3">&nbsp;
                                                </td>
                                            </tr>
                                            <tr style="text-align: center; background-color: #f7f7f7">
                                                <td>Baseline-Test
                                                </td>
                                                <td>Midline-Test
                                                </td>
                                                <td>Endline-Test
                                                </td>
                                            </tr>
                                            <tr style="text-align: center; background-color: transparent !important">
                                                <td>
                                                    <asp:RadioButton ID="rblTestTBPre" GroupName="Test1" CssClass="radio" runat="server" /><label
                                                        for="ctl00_MainContent_CheckBox16">&nbsp;T.B.</label>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rblTestTBMid" GroupName="Test2" CssClass="radio" Enabled="false"
                                                        runat="server" /><label for="ctl00_MainContent_CheckBox17">&nbsp;T.B.</label>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rblTestTBPost" GroupName="Test3" CssClass="radio" Enabled="false"
                                                        runat="server" /><label for="ctl00_MainContent_CheckBox18">&nbsp;T.B.</label>
                                                </td>
                                            </tr>
                                            <tr style="text-align: center; background-color: #f7f7f7  !important">
                                                <td>
                                                    <asp:RadioButton ID="rblTestpreFC" GroupName="Test1" CssClass="radio" runat="server" /><label
                                                        for="ctl00_MainContent_RadioButton1">&nbsp;F.C.</label>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rblTestMidFC" GroupName="Test2" CssClass="radio" Enabled="false"
                                                        runat="server" /><label for="ctl00_MainContent_RadioButton2">&nbsp;F.C.</label>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rblTestPostFC" GroupName="Test3" CssClass="radio" Enabled="false"
                                                        runat="server" />
                                                    <label for="ctl00_MainContent_RadioButton3">
                                                        &nbsp;F.C.</label>
                                                </td>
                                            </tr>
                                            <tr style="text-align: center; background-color: #Transparent !important">
                                                <td>
                                                    <asp:RadioButton ID="rblPartialPre" GroupName="Test6" CssClass="radio" runat="server" />
                                                    <label for="ctl00_MainContent_RadioButton4">
                                                        &nbsp;Partial</label>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rblPartialMid" CssClass="radio" Enabled="false" GroupName="Test7"
                                                        runat="server" /><label for="ctl00_MainContent_RadioButton5">&nbsp;Partial</label>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rblPartialPost" CssClass="radio" Enabled="false" GroupName="Test8"
                                                        runat="server" /><label for="ctl00_MainContent_RadioButton6">&nbsp;Partial</label>
                                                </td>
                                            </tr>
                                            <tr style="text-align: center; background-color: #f7f7f7  !important">
                                                <td>
                                                    <asp:RadioButton ID="rblCompletePre" CssClass="radio" GroupName="Test6" runat="server" /><label
                                                        for="ctl00_MainContent_RadioButton7">&nbsp;Complete</label>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rblCompleteMid" CssClass="radio" Enabled="false" GroupName="Test7"
                                                        runat="server" /><label for="ctl00_MainContent_RadioButton8">&nbsp;Complete</label>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rblCompletePost" CssClass="radio" Enabled="false" GroupName="Test8"
                                                        runat="server" /><label for="ctl00_MainContent_RadioButton9">&nbsp;Complete</label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="lblGuId" Visible="false" ForeColor="Black" runat="server"></asp:Label>
                        <asp:Label ID="lblSCGuId" Visible="false" ForeColor="Black" runat="server"></asp:Label>
                        <cc1:ModalPopupExtender ID="MpexdrDistrict8" runat="server" BackgroundCssClass="modalBg "
                            CancelControlID="CancelButton1" PopupControlID="PnlDistrict8" TargetControlID="HdnFild8">
                        </cc1:ModalPopupExtender>
                        <asp:HiddenField ID="HdnFild8" runat="server"></asp:HiddenField>
                        <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important; margin-top: 220px !important;"
                            ID="PnlDistrict8" runat="server">
                            <div style="width: 100%; height: auto; background-color: #f1f1f1">
                                <div class="modal-header" style="background-color: #ddd; color: White;">
                                    <h4 class="modal-title" style="forecolor: White">GKP</h4>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                    <div class="form-horizontal" role="form">
                                        <div class="form-group">
                                            <asp:Label ID="Label12" class="control-label col-sm-4 lab-text-left" runat="server"
                                                Text="TBorFC"></asp:Label>
                                            <div class="col-sm-6 ">
                                                <asp:RadioButtonList RepeatDirection="Horizontal" ForeColor="Black" ID="rblApprove"
                                                    runat="server">
                                                    <asp:ListItem Selected="True" Value="1">FC   </asp:ListItem>
                                                    <asp:ListItem style="padding-right: -55px; margin-left: 9px;" Value="2">TB</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="form-group" id="statediv" runat="server">
                                            <asp:Label ID="Label10" class="control-label col-sm-4 lab-text-left" runat="server"
                                                Text="Subject"></asp:Label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"
                                                    CssClass="form-control" Font-Names="Verdana" Font-Size="11px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" id="blockdiv" runat="server">
                                            <asp:Label ID="lblBlock" class="control-label col-sm-4 lab-text-left" runat="server"
                                                Text="Level"></asp:Label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlLevel" AutoPostBack="true" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged"
                                                    runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label11" class="control-label col-sm-4 lab-text-left" runat="server"
                                                Text="Session"></asp:Label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlSSession" OnSelectedIndexChanged="ddlSSession_SelectedIndexChanged"
                                                    AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                    Font-Size="11px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label2" class="control-label col-sm-4 lab-text-left" runat="server"
                                                Text="Session Type"></asp:Label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlSessionType" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                    Font-Size="11px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:ImageButton ID="btnNewUserSave" OnClick="btnSaveGkp_Click" ImageUrl="~/images/save-29-1.png"
                                        runat="server" ToolTip="Save" Style="float: none;" ValidationGroup="validatemanageuser"></asp:ImageButton>&nbsp;
                                    <asp:ImageButton ID="CancelButton1" ImageUrl="~/images/close-29.png" runat="server"
                                        Text="Close" ToolTip="Close" Style="float: none;"></asp:ImageButton>
                                </div>
                            </div>
                        </asp:Panel>
                        <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
                    </asp:Panel>


                    <cc1:ModalPopupExtender ID="MpexdrDistrict9" runat="server" BackgroundCssClass="modalBg "
                        CancelControlID="CancelButton2" PopupControlID="PnlDistrict9" TargetControlID="HdnFild9">
                    </cc1:ModalPopupExtender>
                    <asp:HiddenField ID="HdnFild9" runat="server"></asp:HiddenField>
                    <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important; margin-top: 220px !important;"
                        ID="PnlDistrict9" runat="server">
                        <div style="width: 100%; height: auto; background-color: #f1f1f1">
                            <div class="modal-header" style="background-color: #ddd; color: White;">
                                <h4 class="modal-title" style="forecolor: White">SMC Meeting Attendence</h4>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="Label7" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                <div class="form-horizontal" role="form">

                                    <div class="form-group" id="Div10" runat="server">
                                        <asp:Label ID="Label8" class="control-label col-sm-4 lab-text-left" runat="server"
                                            Text="Member Name"></asp:Label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtMemberSC" runat="server"
                                                CssClass="form-control" Font-Names="Verdana" Font-Size="11px">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group" id="Div11" runat="server">
                                        <asp:Label ID="Label9" class="control-label col-sm-4 lab-text-left" runat="server"
                                            Text="Gender"></asp:Label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlSgender"
                                                runat="server" class="form-control">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                <asp:ListItem Value="1">Male</asp:ListItem>
                                                <asp:ListItem Value="2">Female</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label13" class="control-label col-sm-4 lab-text-left" runat="server"
                                            Text="Mobile Number "></asp:Label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtmobile" MaxLength="10" onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                Font-Size="11px">
                                            </asp:TextBox>


                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:ImageButton ID="ImageButton14" ImageUrl="~/images/save-29-1.png" OnClientClick="return checkdataSmc();"
                                    OnClick="btnSaveSmc_Click" runat="server" ToolTip="Save" Style="float: none;"></asp:ImageButton>&nbsp;
                                    <asp:ImageButton ID="CancelButton2" ImageUrl="~/images/close-29.png" runat="server"
                                        Text="Close" ToolTip="Close" Style="float: none;"></asp:ImageButton>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlBalshaba" runat="server" Visible="false">
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <%--  <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>--%>
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                    <span class="">
                                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                            Balsabha
                                        </p>
                                    </span>
                                </button>
                            </div>
                            <div class="collapse navbar-collapse" id="Div5">
                                <div class="thumbnail" style="overflow: auto; height: 344px">
                                    <asp:ImageButton ID="ImageButton3" runat="server" OnClick="btnBalSab_Click" Width="30"
                                        Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                    <table class="table table-striped table-bordered table-hover">
                                        <tbody>
                                            <tr runat="server" visible="false">
                                                <td runat="server" style="padding-left: 33px;" visible="false">
                                                    <asp:RadioButton ID="rblPossiblie" runat="server" AutoPostBack="true" CssClass="radio" GroupName="TestPossiblie" OnCheckedChanged="Group1_CheckedChanged" />
                                                    Possible
                                                </td>
                                                <td runat="server" style="padding-left: 33px;" visible="false">
                                                    <asp:RadioButton ID="rblIMPossiblie" GroupName="TestPossiblie"
                                                        CssClass="radio" runat="server" AutoPostBack="true" OnCheckedChanged="Group1_CheckedChanged" />
                                                    Impossible
                                                </td>
                                            </tr>


                                            <tr>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        <asp:CheckBox ID="chkBalsabha" Visible="false" runat="server" />
                                                        Balsabha
                                                    </p>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rblBalsabaTB" AutoPostBack="true" OnCheckedChanged="divBTB_Click" Style="margin-left: 19px;" GroupName="TestBal"
                                                        CssClass="radio" runat="server" />
                                                    T.B.
                                                    <asp:RadioButton ID="rblBalsabaFC" AutoPostBack="true" OnCheckedChanged="divBTB_Click" Style="margin-left: 19px;" GroupName="TestBal"
                                                        CssClass="radio" runat="server" />
                                                    F.C.
                                                </td>
                                            </tr>
                                            <tr runat="server" visible="false" id="divBTB">
                                                <td>TB Name</td>
                                                <td>
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBalSabaTB"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <asp:Panel ID="pnlBalTessst" runat="server" Visible="true">
                                                <tr style="text-align: left">
                                                    <td runat="server" visible="false">
                                                        <asp:CheckBox ID="chkBalSabhaFor" runat="server" Enabled="false" />
                                                        <label for="ctl00_MainContent_chkbalsabha">
                                                            &nbsp;Balsabha Formation</label>





                                                    </td>

                                                    <td runat="server" visible="false">
                                                        <asp:CheckBox ID="chkOrientation" runat="server" Visible="false" />


                                                        <label for="ctl00_MainContent_CheckBox28">
                                                            &nbsp;Orientation</label>
                                                    </td>
                                                </tr>
                                                <tr style="text-align: left">
                                                    <td>

                                                        <asp:CheckBox ID="chkSession1" runat="server" Visible="true" />
                                                        <asp:Label ID="lblsession1" runat="server" Text="Session1"> </asp:Label>
                                                    </td>
                                                    <td>

                                                        <asp:CheckBox ID="chkSession2" OnCheckedChanged="chkSession2_OnCheckedChanged" runat="server" Visible="true" AutoPostBack="true" />
                                                        <asp:Label ID="lblSession2" runat="server" Text="Session2"> </asp:Label>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" Visible="false" />
                                                    </td>
                                                    <td runat="server" visible="false">
                                                        <asp:CheckBox ID="chkChat" runat="server" />
                                                        <label for="ctl00_MainContent_CheckBox30">
                                                            &nbsp;Chart</label>
                                                    </td>
                                                    <td runat="server" visible="false">
                                                        <asp:CheckBox ID="chkKit" runat="server" />
                                                        <label for="ctl00_MainContent_CheckBox32">
                                                            &nbsp;Kit</label>
                                                    </td>
                                                </tr>
                                                <tr style="text-align: left">
                                                    <td></td>
                                                    <td>
                                                        <asp:Button ID="Imgaddclass" OnClick="onclick_btnaddclass" runat="server" Text="Add Child" CssClass="btn btn-success pull-left " />
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlBalTest1" runat="server" Visible="false">
                                                <tr style="text-align: left">
                                                    <td>
                                                        <label for="ctl00_MainContent_CheckBox32">
                                                            &nbsp;Reason</label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlreasons" runat="server" class="form-control " />
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <tr>
                                                <td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton4" OnClick="btnLife" runat="server" Width="30" Height="25"
                                                            CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                                    </td>
                                                </td>
                                            </tr>
                                            <asp:Panel ID="pnlLife" runat="server" Enabled="false">
                                                <tr>
                                                    <td>
                                                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                            <asp:CheckBox ID="chklife" runat="server" />
                                                            Life Skill Education
                                                        </p>
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="rblLifeTB" AutoPostBack="true" OnCheckedChanged="divLiff_Click" Style="margin-left: 19px;" GroupName="TestLife" CssClass="radio"
                                                            runat="server" />
                                                        T.B.
                                                        <asp:RadioButton ID="rblLifeFC" AutoPostBack="true" OnCheckedChanged="divLiff_Click" Style="margin-left: 19px;" GroupName="TestLife" CssClass="radio"
                                                            runat="server" />
                                                        F.C.
                                                    </td>
                                                </tr>
                                                <tr runat="server" visible="false" id="DivLiff">
                                                    <td>TB Name</td>
                                                    <td colspan="2">
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlliffTb"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr style="text-align: left">
                                                    <td>
                                                        <asp:Label ID="lblSessionAttendance" runat="server" Text="Session"> </asp:Label>
                                                        <asp:CheckBox ID="chkGame1" runat="server" Visible="false" AutoPostBack="true" OnCheckedChanged="GroupLiff_CheckedChanged" /><%--<label
                                                            for="ctl00_MainContent_CheckBox27">&nbsp;Game1</label>--%>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlsession" runat="server" OnSelectedIndexChanged="ddlsession_OnSelectedIndexChanged" class="form-control"
                                                            AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        <asp:CheckBox ID="chkGame2" runat="server" Visible="false" AutoPostBack="true" OnCheckedChanged="GroupLiff_CheckedChanged" /><%--<label
                                                            for="ctl00_MainContent_CheckBox29">&nbsp;Game2</label>--%>
                                                    </td>
                                                </tr>
                                                <tr style="text-align: left">
                                                    <td>
                                                        <asp:CheckBox ID="chkGame3" runat="server" Visible="false" AutoPostBack="true" OnCheckedChanged="GroupLiff_CheckedChanged" /><%--<label
                                                            for="ctl00_MainContent_CheckBox31">&nbsp;Game3</label>--%>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkGame4" runat="server" Visible="false" AutoPostBack="true" OnCheckedChanged="GroupLiff_CheckedChanged" /><%--<label 
                                                            for="ctl00_MainContent_CheckBox33">&nbsp;Game4</label>--%>
                                                    </td>
                                                </tr>
                                                <tr style="text-align: left">
                                                    <td>
                                                        <asp:CheckBox ID="chkGame5" runat="server" Visible="false" AutoPostBack="true" OnCheckedChanged="GroupLiff_CheckedChanged" /><%--<label
                                                            for="ctl00_MainContent_CheckBox33">--%><%--<label for="ctl00_MainContent_CheckBox34">&nbsp;Game5</label>--%>
                                                    </td>
                                                    <td></td>
                                                    <asp:HiddenField ID="hdnsession1" runat="server" />
                                                    <asp:HiddenField ID="hdnsession2" runat="server" />
                                                    <asp:HiddenField ID="hdnUniqueChildRCode" runat="server" />
                                                </tr>
                                            </asp:Panel>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel runat="server" ID="asdjn">

                                                        <asp:GridView ID="GvReg" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                            DataKeyNames="Schoolcode,UniqueChildRCode,VillageCode,Registrationdate" AllowPaging="true" PageSize="13"
                                                            AutoGenerateColumns="False" Font-Names="Arial" OnRowDataBound="gv_regOnDataBound"
                                                            OnPageIndexChanging="GvReg_PageIndexChanging" Font-Size="11px" Width="100%">
                                                            <EmptyDataTemplate>
                                                                <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                    Data not found
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <FooterStyle CssClass="FooterStyle" />
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                            <RowStyle HorizontalAlign="Left" />
                                                            <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                            <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                            <AlternatingRowStyle BackColor="#f1f1f1" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Child Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblChildname" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server" Text='<%# Bind("ChildName") %>'></asp:Label>
                                                                        <asp:Label ID="lblUniqueChildCode" ForeColor="Black" Font-Names="Calibri" Visible="false"
                                                                            ItemStyle-ForeColor="#333" runat="server" Text='<%# Bind("UniqueCode") %>'></asp:Label>
                                                                        <asp:Label ID="lblUniqueChildRCode" ForeColor="Black" Font-Names="Calibri" Visible="false"
                                                                            ItemStyle-ForeColor="#333" runat="server" Text='<%# Bind("UniqueChildRCode") %>'></asp:Label>
                                                                        <asp:Label ID="lblVillageCd" ForeColor="Black" Font-Names="Calibri" Visible="false"
                                                                            ItemStyle-ForeColor="#333" runat="server" Text='<%# Bind("VillageCode") %>'></asp:Label>
                                                                        <asp:Label ID="lblschoolcode" ForeColor="Black" Font-Names="Calibri" Visible="false"
                                                                            ItemStyle-ForeColor="#333" runat="server" Text='<%# Bind("Schoolcode") %>'></asp:Label>
                                                                        <asp:Label ID="lblPresent" ForeColor="Black" Font-Names="Calibri" Visible="false"
                                                                            ItemStyle-ForeColor="#333" runat="server" Text='<%# Bind("present") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Class">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblClass" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server" Text='<%# Bind("Class") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SR Number">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSRNumber" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server" Text='<%# Bind("SRnumber") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Attendance">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlAttendance" runat="server" class="form-control" OnSelectedIndexChanged="ddlAttendance_Changed" AutoPostBack="true">
                                                                            <asp:ListItem Text="--Select--" Value="0"> </asp:ListItem>
                                                                            <asp:ListItem Text="Present" Value="1"> </asp:ListItem>
                                                                            <asp:ListItem Text="Absent" Value="2"> </asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Edit" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtn" OnClick="LnkBtnBlock_ffOnClick" runat="server" Text="Edit"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="View" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtn1" OnClick="LnkBtnView_ffOnClick" runat="server" Text="View"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                    <cc1:ModalPopupExtender ID="ModalAddclass" runat="server" TargetControlID="hdn_addclass"
                                        PopupControlID="pnl_Addclass" CancelControlID="BtnEditPopClose" BackgroundCssClass="modalBackground">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnl_Addclass" runat="server" Style="display: none;" Width="730px"
                                        Height="300px" class="ModalPopup" BackColor="White" BorderColor="Black" BorderStyle="Ridge"
                                        BorderWidth="1">
                                        <div style="margin-bottom: 15px; background-color: #c4c4c4;" align="right">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblregistration" runat="server" Text="Registration detail" Font-Bold="true"
                                                            CssClass="control-label"></asp:Label>
                                                    </td>
                                                    <td style="width: 195px"></td>
                                                    <td width="90px" align="right">
                                                        <asp:ImageButton ID="BtnEditPopClose" runat="server" ImageUrl="~/Images/close-29.png"
                                                            ImageAlign="Right" Width="30px" Height="28px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="width: 685px; height: 234px; margin-left: 20px; border: 1px solid gray">
                                            <table style="margin-left: 18px">
                                                <tr style="height: 35px">
                                                    <td>
                                                        <asp:Label ID="lblRegistrationd6" runat="server" Text="Registration Date" CssClass="control-label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtRegistration" class="form-control " runat="server"></asp:TextBox>
                                                        <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            TargetControlID="txtRegistration" PopupPosition="BottomRight">
                                                        </ajax:CalendarExtender>

                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <asp:Label ID="lbldobavailability" Text="DOB availability" runat="server" CssClass="control-label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddldobavail" runat="server" OnSelectedIndexChanged="dddl_DO" AutoPostBack="true" class="form-control">
                                                            <asp:ListItem Text="--Select--" Value="0"> </asp:ListItem>
                                                            <asp:ListItem Text="Yes" Value="1"> </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2"> </asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr style="height: 35px">
                                                    <td>
                                                        <asp:Label ID="lblGirlChildName" runat="server" Text="Girl Child Name" CssClass="control-label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtGirlChildName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <asp:Label ID="lblage" runat="server" Text="Age" CssClass="control-label clslblage"></asp:Label>
                                                        <asp:Label ID="lblDOB" runat="server" Text="DOB" CssClass="control-label clslblDOB"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtage" onchange="Checkage();" CssClass="form-control"></asp:TextBox>
                                                        <asp:TextBox runat="server" ID="txtDOB" autocomplete="off"
                                                            ondrop="return false;" onkeypress="return false;" CssClass="form-control"></asp:TextBox>
                                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            TargetControlID="txtDOB" PopupPosition="BottomRight">
                                                        </ajax:CalendarExtender>
                                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" FilterType="Numbers" runat="server" TargetControlID="txtage">
                                                        </ajax:FilteredTextBoxExtender>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr style="height: 35px">
                                                    <td>
                                                        <asp:Label ID="lblFathername" runat="server" Text="Father’s Name " CssClass="control-label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFathername" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <asp:Label ID="lblCategory" runat="server" Text="Social Category " CssClass="control-label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCategory" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr style="height: 35px">
                                                    <td>
                                                        <asp:Label ID="lblParentMobileNumber" runat="server" Text="Parent's Mobile Number "
                                                            CssClass="control-label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtParentMobileNumber" class="form-control " runat="server"
                                                            MaxLength="10"></asp:TextBox>
                                                        <ajax:FilteredTextBoxExtender ID="ftmobile" FilterType="Numbers" runat="server" TargetControlID="txtParentMobileNumber">
                                                        </ajax:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="width: 70px"></td>
                                                    <td>
                                                        <asp:Label ID="lblclass" runat="server" Text="Class " CssClass="control-label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlClass" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr style="height: 35px">
                                                    <td>
                                                        <asp:Label ID="lblgender" runat="server" Text="Gender " CssClass="control-label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlgender" runat="server" class="form-control ">
                                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Selected="True" Value="1">Male</asp:ListItem>
                                                            <asp:ListItem Selected="True" Value="2">Female</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <asp:Label ID="lblSRNumberne" runat="server" Text="SR Number" CssClass="control-label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSRNumber" runat="server" MaxLength="8"
                                                            CssClass="form-control"></asp:TextBox>
                                                        <ajax:FilteredTextBoxExtender ID="FtxtSRNumber" runat="server" FilterMode="ValidChars"
                                                            ValidChars="QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm0123456789" TargetControlID="txtSRNumber">
                                                        </ajax:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr style="height: 35px">
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td style="width: 107px">
                                                        <asp:Button ID="BtnsaveReg" runat="server" OnClientClick="return checkdata();" OnClick="onclick_savedata" Text="Save" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                    <asp:HiddenField ID="hdn_addclass" runat="server" />

                                    <cc1:ModalPopupExtender ID="ModalAddclass1" runat="server" TargetControlID="hdn_addclass1"
                                        PopupControlID="pnl_Addclass1" CancelControlID="BtnEditPopClose" BackgroundCssClass="modalBackground">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnl_Addclass1" runat="server" Style="display: none;" Width="830px"
                                        Height="600px" class="ModalPopup" BackColor="White" BorderColor="Black" BorderStyle="Ridge"
                                        BorderWidth="1">
                                        <div style="margin-bottom: 15px; background-color: #c4c4c4;" align="right" id="dvBaseline">
                                            <div class="table-responsive" style="height: 590px">
                                                <table class="tabl-que table table-bordered table-striped">
                                                    <tr>
                                                        <th colspan="6" class="text-center">बेसलाइन मुल्यंकान</th>
                                                        <th width="90px" align="right">
                                                            <asp:ImageButton ID="ImageButton16" runat="server" ImageUrl="~/Images/close-29.png"
                                                                ImageAlign="Right" Width="30px" Height="28px" /></th>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 50px">Q.No.</th>
                                                        <th>प्रश्न</th>
                                                        <th colspan="5" style="width: 220px">प्राप्त अंक</th>
                                                    </tr>
                                                    <tr>
                                                        <th>Q1</th>
                                                        <th>मैं अपनी भावनाओं को व्यक्त न करके उन्हें रोक लेती हूं ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ1_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ2" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ1_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ2" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ1_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ2" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ1_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ2" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ1_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ2" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ2" runat="server">
                                                        <th>Q2</th>
                                                        <th>जब मुझे बुरा लगता है (उदाहरण , उदास, क्रोधित, या चिंतित), तो मैं ध्यान रखती हूं कि इसे व्यक्त न करूं </th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ2_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" GroupName="trQ3" OnCheckedChanged="rdb_Click" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ2_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" GroupName="trQ3" OnCheckedChanged="rdb_Click" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ2_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" GroupName="trQ3" OnCheckedChanged="rdb_Click" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ2_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" GroupName="trQ3" OnCheckedChanged="rdb_Click" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ2_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" GroupName="trQ3" OnCheckedChanged="rdb_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ3" runat="server">
                                                        <th>Q3</th>
                                                        <th>जब मेरा मूड खराब होता है, तो मुझे खुद को बेहतर महसूस कराना अच्छी तरह से आता है।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ3_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" GroupName="trQ4" OnCheckedChanged="rdb_Click" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ3_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" GroupName="trQ4" OnCheckedChanged="rdb_Click" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ3_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" GroupName="trQ4" OnCheckedChanged="rdb_Click" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ3_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" GroupName="trQ4" OnCheckedChanged="rdb_Click" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ3_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" GroupName="trQ4" OnCheckedChanged="rdb_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ4" runat="server">
                                                        <th>Q4</th>
                                                        <th>मैं मुश्किल समय में भी शांत रहने की कोशिश करती हूं।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ4_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ5" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ4_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ5" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ4_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ5" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ4_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ5" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ4_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ5" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ5" runat="server">
                                                        <th>Q5</th>
                                                        <th>जब मैं दुखी होती हूं, तो मैं किसी से इस बारें में बात करती हूं।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ5_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ6" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ5_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ6" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ5_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ6" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ5_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ6" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ5_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ6" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ6" runat="server">
                                                        <th>Q6</th>
                                                        <th>जब मैं दुखी होती हैं, तो में दूसरों से सांत्वना की अपेक्षा करती हूँ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ6_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ7" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ6_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ7" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ6_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ7" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ6_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ7" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ6_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ7" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ7" runat="server">
                                                        <th>Q7</th>
                                                        <th>मुझे अपने गुस्से को शांत करने में कठिनाई होती है।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ7_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ8" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ7_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ8" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ7_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ8" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ7_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ8" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ7_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ8" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ8" runat="server">
                                                        <th>Q8</th>
                                                        <th>जब मैं किसी परेशानी में होती हूँ, तब दोस्तों से बात करने से मुझे तुरंत बेहतर महसूस होता है।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ8_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ9" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ8_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ9" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ8_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ9" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ8_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ9" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ8_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ9" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ9" runat="server">
                                                        <th>Q9</th>
                                                        <th>मुझे अपने गुस्से पर काबू पाना मुश्किल लगता है।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ9_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ10" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ9_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ10" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ9_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ10" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ9_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ10" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ9_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ10" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ10" runat="server">
                                                        <th>Q10</th>
                                                        <th>मैं अपनी परेशानियों को अपने तक ही सीमित रखती हूँ ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ10_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ11" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ10_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ11" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ10_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ11" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ10_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ11" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ10_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ11" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ11" runat="server">
                                                        <th>Q11</th>
                                                        <th>मुझे तनावपूर्ण घटनाओं को संभालना मुश्किल लगता है ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ11_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ12" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ11_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ12" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ11_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ12" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ11_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ12" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ11_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ12" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ12" runat="server">
                                                        <th>Q12</th>
                                                        <th>मुझे अपने जीवन में आई समस्याओं से उभरने में काफी समय लगता है।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ12_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ13" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ12_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ13" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ12_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ13" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ12_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ13" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ12_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ13" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ13" runat="server">
                                                        <th>Q13</th>
                                                        <th>मैं मुश्किल समय में आसानी से हार मान लेती हूँ ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ13_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ14" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ13_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ14" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ13_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ14" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ13_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ14" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ13_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ14" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ14" runat="server">
                                                        <th>Q14</th>
                                                        <th>मुझे कठिन समस्याएं परेशान करती है ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ14_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ15" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ14_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ15" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ14_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ15" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ14_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ15" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ14_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ15" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ15" runat="server">
                                                        <th>Q15</th>
                                                        <th>मुझे विश्वास है की मेरे जीवन में आने वाली हर कठिनाईयां का मैं सामना कर सकती हूँ ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ15_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ16" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ15_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ16" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ15_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ16" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ15_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ16" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ15_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ16" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ16" runat="server">
                                                        <th>Q16</th>
                                                        <th>मैं अपनी जीवन में आने वाली किसी भी मुश्किल समय से बाहर निकलने का रास्ता खोज लेती हूँ ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ16_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ17" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ16_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ17" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ16_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ17" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ16_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ17" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ16_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ17" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ17" runat="server">
                                                        <th>Q17</th>
                                                        <th>जब मैं परेशान होती हूं, तो मैं स्पष्ट रूप से नहीं सोचती ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ17_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ18" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ17_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ18" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ17_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ18" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ17_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ18" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ17_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ18" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ18" runat="server">
                                                        <th>Q18</th>
                                                        <th>मैं किसी समस्या के बारे में ध्यान से सोचने के बाद ही कुछ करती हूं ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ18_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ19" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ18_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ19" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ18_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ19" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ18_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ19" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ18_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ19" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ19" runat="server">
                                                        <th>Q19</th>
                                                        <th>जब मुझे कोई चीज़ समझ में नहीं आती है और मैं अपनी शिक्षिका से पूछती हूँ, तो वह उसे टाल देते है ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ19_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ20" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ19_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ20" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ19_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ20" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ19_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ20" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ19_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ20" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ20" runat="server">
                                                        <th>Q20</th>
                                                        <th>मेरी एक ऐसी भाई बहन है जो मुझे अच्छे से समझाती है।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ20_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ21" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ20_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ21" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ20_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ21" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ20_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ21" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ20_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ21" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ21" runat="server">
                                                        <th>Q21</th>
                                                        <th>जो बातें मेरी समझ में नहीं आती है, उन्हें मेरे मित्र समझा देते है।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ21_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ22" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ21_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ22" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ21_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ22" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ21_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ22" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ21_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ22" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ22" runat="server">
                                                        <th>Q22</th>
                                                        <th>मेरे माता-पिता मुझे अच्छी तरह समझते हैं ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ22_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ23" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ22_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ23" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ22_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ23" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ22_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ23" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ22_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ23" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ23" runat="server">
                                                        <th>Q23</th>
                                                        <th>मेरा परिवार मेरी मदद करने की कोशिश करता है ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ23_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ24" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ23_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ24" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ23_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ24" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ23_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ24" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ23_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ24" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ24" runat="server">
                                                        <th>Q24</th>
                                                        <th>जरूरत पड़ने पर मेरी मदद करने के लिए मेरे आस-पास लोग होते हैं।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ24_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ25" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ24_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ25" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ24_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ25" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ24_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ25" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ24_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ25" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ25" runat="server">
                                                        <th>Q25</th>
                                                        <th>मैं अपने आस-पास के लोगो के दुःखी होने पर उन्हें खुश करने की कोशिश करती हूँ ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ25_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ26" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ25_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ26" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ25_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ26" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ25_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ26" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ25_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ26" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ26" runat="server">
                                                        <th>Q26</th>
                                                        <th>जब अन्य लोगो को समस्याएं होती है तो मैं उनके लिए परेशान नहीं होती हूँ ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ26_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ27" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ26_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ27" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ26_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ27" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ26_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ27" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ26_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ27" />
                                                        </td>
                                                    </tr>

                                                    <tr id="trQ27" runat="server">
                                                        <th>Q27</th>
                                                        <th>जब मेरे आसपास के लोग दुःखी होते है तो मैं उदास हो जाती हूँ ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ27_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ28" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ27_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ28" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ27_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ28" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ27_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ28" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ27_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ28" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ28" runat="server">
                                                        <th>Q28</th>
                                                        <th>किसी के साथ दुर्व्यवहार होता देख मुझे बहुत दुःख होता है ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ28_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ29" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ28_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ29" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ28_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ29" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ28_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ29" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ28_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ29" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ29" runat="server">
                                                        <th>Q29</th>
                                                        <th>जब कोई रोता है तो मैं उदास हो जाती हूँ ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ29_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ30" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ29_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ30" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ29_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ30" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ29_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ30" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ29_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ30" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ30" runat="server">
                                                        <th>Q30</th>
                                                        <th>लोगों के कुछ न कहने पर भी मैं समझ जाती हूँ कि वे परेशान/दुःखी है।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ30_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ31" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ30_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ31" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ30_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ31" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ30_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ31" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ30_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ31" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ31" runat="server">
                                                        <th>Q31</th>
                                                        <th>मुझे दूसरों को खुश करना अच्छा लगता</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ31_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ32" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ31_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ32" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ31_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ32" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ31_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ32" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ31_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ32" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ32" runat="server">
                                                        <th>Q32</th>
                                                        <th>मैं दोस्तों की समस्याओं से भावनात्मक रूप से जुड़ जाती हूं।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ32_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ33" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ32_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ33" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ32_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ33" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ32_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ33" /></td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ32_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ33" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ33" runat="server">
                                                        <th>Q33</th>
                                                        <th>मुझे दोस्त बनाना मुश्किल लगता है ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ33_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ34" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ33_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ34" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ33_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ34" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ33_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ34" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ33_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ34" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ34" runat="server">
                                                        <th>Q34</th>
                                                        <th>मैं अपनी उम्र के अन्य बच्चों के साथ रहने के बजाय अकेले रहना पसंद करती हूँ ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ34_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ35" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ34_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ35" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ34_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ35" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ34_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ35" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ34_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ35" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ35" runat="server">
                                                        <th>Q35</th>
                                                        <th>मैं दूसरों की राय का सम्मान करती हूँ, भले ही मैं उनकी राय मुझे स्वीकार न हो।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ35_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ36" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ35_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ36" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ35_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ36" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ35_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ36" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ35_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ36" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ36" runat="server">
                                                        <th>Q36</th>
                                                        <th>जब हम एक टीम के रूप में कोई काम करते हैं, तो मैं दूसरों को अवसर देती हूं</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ36_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ37" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ36_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ37" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ36_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ37" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ36_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ37" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ36_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ37" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ37" runat="server">
                                                        <th>Q37</th>
                                                        <th>मैं सम्मिलित कार्य के बजाय अकेले काम करना पसंद करती हूँ ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ37_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ38" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ37_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ38" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ37_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ38" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ37_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ38" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ37_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ38" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ38" runat="server">
                                                        <th>Q38</th>
                                                        <th>मेरे मित्र मेरे साथ काम करने में सहज महसूस करते हैं।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ38_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ39" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ38_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ39" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ38_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ39" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ38_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ39" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ38_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ39" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ39" runat="server">
                                                        <th>Q39</th>
                                                        <th>मैं अपने दोस्तों के विवादों को सुलझाने में मदद करती हूँ ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ39_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ40" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ39_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ40" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ39_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ40" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ39_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ40" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ39_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ40" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ40" runat="server">
                                                        <th>Q40</th>
                                                        <th>मुझे उन गतिविधियों में भाग लेना पसंद है, जिनमें दूसरे लोग भी भाग लेते है।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ40_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ41" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ40_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ41" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ40_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ41" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ40_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ41" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ40_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ41" />
                                                        </td>
                                                    </tr>

                                                    <tr id="trQ41" runat="server">
                                                        <th>Q41</th>
                                                        <th>अपनी समस्याओं को हल करने के लिए मैं कड़ी मेहनत करती हूँ ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ41_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ42" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ41_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ42" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ41_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ42" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ41_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ42" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ41_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ42" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ42" runat="server">
                                                        <th>Q42</th>
                                                        <th>मैं अपने निर्णय खुद लेता लेती हूँ ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ42_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ43" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ42_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ43" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ42_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ43" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ42_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ43" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ42_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ43" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ43" runat="server">
                                                        <th>Q43</th>
                                                        <th>दूसरे लोगों की तरह मैं चीजे कर सकती हूं।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ43_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ44" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ43_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ44" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ43_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ44" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ43_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ44" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ43_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ44" />
                                                        </td>
                                                    </tr>

                                                    <tr id="trQ44" runat="server">
                                                        <th>Q44</th>
                                                        <th>मुझे अपने आप पर भरोसा है कि मेरे जीवन में अचानक घटित होने वाली घटनाओं से मैं अच्छी तरह से निपट सकती हूँ ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ44_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ45" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ44_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ45" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ44_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ45" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ44_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ45" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ44_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ45" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ45" runat="server">
                                                        <th>Q45</th>
                                                        <th>मेरे लिए अपने लक्ष्य पर टिके रहना और अपने लक्ष्यों को पूरा करना आसान है ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ45_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ46" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ45_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ46" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ45_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ46" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ45_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ46" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ45_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ46" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ46" runat="server">
                                                        <th>Q46</th>
                                                        <th>जब मैं संकट में होती हूं तो समाधान के बारे में सोच लेती हूं ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ46_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ47" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ46_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ47" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ46_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ47" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ46_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ47" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ46_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ47" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ47" runat="server">
                                                        <th>Q47</th>
                                                        <th>मैं अपने जीवन की चुनौतियों को पार कर सकती हूं ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ47_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ48" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ47_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ48" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ47_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ48" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ47_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ48" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ47_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ48" />
                                                        </td>
                                                    </tr>

                                                    <tr id="trQ48" runat="server">
                                                        <th>Q48</th>
                                                        <th>जब मुझे मुश्किल समस्या हल करनी होती है, तो मैं कई समाधान ढूंढ लेती हूँ ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ48_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ49" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ48_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ49" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ48_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ49" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ48_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ49" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ48_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ49" />
                                                        </td>
                                                    </tr>

                                                    <tr id="trQ49" runat="server">
                                                        <th>Q49</th>
                                                        <th>मैं कठिन परिस्थितियां में निर्णय लेने में सक्षम हूँ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ49_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ50" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ49_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ50" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ49_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ50" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ49_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ50" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ49_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ50" />
                                                        </td>
                                                    </tr>

                                                    <tr id="trQ50" runat="server">
                                                        <th>Q50</th>
                                                        <th>मुझे निर्णय लेने में कठिनाई होती है ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ50_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ51" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ50_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ51" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ50_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ51" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ50_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ51" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ50_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ51" />
                                                        </td>
                                                    </tr>

                                                    <tr id="trQ51" runat="server">
                                                        <th>Q51</th>
                                                        <th>मुझे अपने फैसलों पर भरोसा नहीं होता है ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ51_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ52" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ51_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ52" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ51_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ52" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ51_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ52" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ51_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ52" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ52" runat="server">
                                                        <th>Q52</th>
                                                        <th>मुझे पता है कि स्कूली शिक्षा के बाद में, मैं अपने जीवन में क्या करना चाहती हूँ</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ52_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ53" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ52_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ53" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ52_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ53" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ52_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ53" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ52_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ53" />
                                                        </td>
                                                    </tr>

                                                    <tr id="trQ53" runat="server">
                                                        <th>Q53</th>
                                                        <th>मैं योजनाएं बनाकर उन्हें साकार करने के लिए काम करती हूँ ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ53_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ54" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ53_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ54" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ53_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ54" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ53_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ54" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ53_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ54" />
                                                        </td>
                                                    </tr>


                                                    <tr id="trQ54" runat="server">
                                                        <th>Q54</th>
                                                        <th>मेरे दोस्त ने जो निर्णय लिया, मैंने भी वही निर्णय लिया ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ54_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ55" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ54_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ55" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ54_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ55" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ54_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ55" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ54_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ55" />
                                                        </td>
                                                    </tr>

                                                    <tr id="trQ55" runat="server">
                                                        <th>Q55</th>
                                                        <th>मैं सही सोच और अपने ऊपर भरोसा रखनेवालों में से हूँ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ55_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ56" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ55_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ56" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ55_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ56" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ55_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ56" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ55_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" GroupName="trQ56" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trQ56" runat="server">
                                                        <th>Q56</th>
                                                        <th>जब मेरे मित्र या परिवार के लोग मेरे निर्णयों से असहमत होते है, तो मैं अपना
निर्णय बदल लेता हूँ ।</th>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ56_5" Text="5" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ56_4" Text="4" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ56_3" Text="3" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ56_2" Text="2" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rdbQ56_1" Text="1" AutoPostBack="true" CssClass="que" runat="server" OnCheckedChanged="rdb_Click" />
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <th colspan="7" class="text-right">
                                                            <asp:Button ID="btnSaveAttendance" runat="server" OnClick="btnSaveAttendance_savedata" Text="Save" /></th>

                                                    </tr>
                                                </table>
                                            </div>
                                        </div>

                                    </asp:Panel>
                                    <asp:HiddenField ID="hdn_addclass1" runat="server" />


                                </div>
                            </div>

                            <%--  </ContentTemplate>
                    </asp:UpdatePanel>--%>
                        </div>
                    </asp:Panel>
                </div>
                <div class="row" id="dvidnew" runat="server">
                    <asp:Panel ID="pnlSACUpdate" runat="server">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" id="dvidnew45" runat="server">
                            <%--  <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>--%>
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                    <span class="">
                                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                            <input name="" value="" type="checkbox">SAC Update
                                        </p>
                                    </span>
                                </button>
                            </div>
                            <div class="collapse navbar-collapse" id="Div1">
                                <div class="thumbnail" style="height: 715px">
                                    <asp:ImageButton ID="ImageButton5" OnClick="btnSacUpdate_Click" runat="server" Width="30"
                                        Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                    <table class="table table-striped table-bordered table-hover">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        <asp:CheckBox ID="chkSACUpdate" runat="server" />
                                                        SAC Quarter Update
                                                    </p>
                                                </td>
                                                <td colspan="5">
                                                    <asp:RadioButton ID="rblSacTB" Visible="false" Style="margin-left: 19px;" GroupName="shhTB" CssClass="radio"
                                                        runat="server" />

                                                    <asp:RadioButton ID="rblSacFB" Style="margin-left: 19px;" GroupName="sacTB" CssClass="radio"
                                                        runat="server" />
                                                    FC
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Initial Status
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Apr-Jun
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Apr-Sep
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Apr-Dec
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Apr-Mar
                                                    </p>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="width: 60%;"># S.M.C. Meetings
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPreSMCMeeting" Enabled="false" onchange="javascript:SMC(this.value);" autocomplete="off"
                                                        ondrop="return false;" MaxLength="2" onkeypress="return isNumberKey(this,event);"
                                                        Width="60px" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSMCMeeting" onchange="javascript:SMC(this.value,this.id);" autocomplete="off"
                                                        ondrop="return false;" MaxLength="2" onkeypress="return isNumberKey(this,event);"
                                                        Width="60px" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtSepSMCMeeting" onchange="javascript:SMCSep(this.value,this.id);" autocomplete="off"
                                                        ondrop="return false;" MaxLength="2" onkeypress="return isNumberKey(this,event);"
                                                        Width="60px" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDescSMCMeeting" onchange="javascript:SMC(this.value,this.id);" autocomplete="off"
                                                        ondrop="return false;" MaxLength="2" onkeypress="return isNumberKey(this,event);"
                                                        Width="60px" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMarSMCMeeting" onchange="javascript:SMC(this.value,this.id);" autocomplete="off"
                                                        ondrop="return false;" MaxLength="2" onkeypress="return isNumberKey(this,event);"
                                                        Width="60px" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="width: 60%;">Regular Health Checkup
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPrvHealth" Enabled="false" autocomplete="off" ondrop="return false;" MaxLength="1"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtHealth" autocomplete="off" ondrop="return false;" MaxLength="1"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtSepHealth" autocomplete="off" ondrop="return false;" MaxLength="1"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDescHealth" autocomplete="off" ondrop="return false;" MaxLength="1"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMarHealth" autocomplete="off" ondrop="return false;" MaxLength="1"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="6" style="text-align: center; font-weight: bold; padding: 12px;">Admission in School</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Initial Status
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Status- 30 Jun
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Status- 30 Sep
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Status-31 Dec
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Status-Mar
                                                    </p>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="width: 60%;">Admission of girls
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPreAdgirls" Enabled="false" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtAdgirls" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtsepAdgirls" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDescAdgirls" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMarAdgirls" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 60%;">Admission of boys
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPrvAdBoy" Enabled="false" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAdBoy" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSepAdBoy" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDescAdBoy" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMarAdBoy" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="6" style="text-align: center; font-weight: bold; padding: 12px;">Regularity/Retention</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Initial Status
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Apr-Jun
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Jul-Sep
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Oct-Dec
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Jan-Mar
                                                    </p>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="width: 60%;"># Girls left school
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPrvleftGirl" Enabled="false" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtleftGirl" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSepleftGirl" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDescleftGirl" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMarleftGirl" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 60%;"># Boys left school
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPrevleftBoy" Enabled="false" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtleftBoy" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSepleftBoy" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtdescleftBoy" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMarleftBoy" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 60%;">Girls- Who needs to be Regularized
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPrvGirlNot" Enabled="false" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGirlNot" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSepGirlNot" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDescGirlNot" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMarGirlNot" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 60%;">Boys- Who needs to be Regularized
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtprvBoyNot" Enabled="false" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBoyNot" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSepBoyNot" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDecBoyNot" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMarBoyNot" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <%--  </ContentTemplate>
                    </asp:UpdatePanel>--%>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlinfrastructure" runat="server">
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <%--  <asp:UpdatePanel ID="UpdatePaneel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>--%>
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                    <span class="">
                                        <p class="text-danger" style="margin: 0px; font-weight: bold; width: 159px;">
                                            School infrastructure facility
                                        </p>
                                    </span>
                                </button>
                            </div>
                            <div class="collapse navbar-collapse" id="myNavbar">
                                <div class="thumbnail" style="overflow: auto; height: 740px; width: 502px;">
                                    <asp:ImageButton ID="ImageButton6" OnClick="btninfrastructure_Click" runat="server"
                                        Width="30" Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                    <table class="table table-striped table-bordered table-hover">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        <asp:CheckBox ID="chkPhysical" runat="server" />
                                                        School infrastructure
                                                    </p>
                                                </td>
                                                <td colspan="3">
                                                    <asp:LinkButton ID="lnkCopy" runat="server" OnClick="lnl_click">Copy</asp:LinkButton>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        SMC President Name
                                                    </p>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtSMCPre" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rblPhysicalTB" Style="margin-left: 19px;" Visible="false" GroupName="sa4c2TB"
                                                        CssClass="radio" runat="server" />


                                                    <asp:RadioButton ID="rblPhysicalFC" Style="margin-left: 19px;" GroupName="sac2TB"
                                                        CssClass="radio" runat="server" />
                                                    FC
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Initial Status
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Previous
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        Current
                                                    </p>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td style="width: 60%;">Class Room
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtClassRoom2" Width="60px" onkeypress="return isNumberKey(this,event);" autocomplete="off"
                                                        ondrop="return false;" MaxLength="2" runat="server" Enabled="false" class="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtClassRoom1" Width="60px" onkeypress="return isNumberKey(this,event);" autocomplete="off"
                                                        ondrop="return false;" MaxLength="2" runat="server" Enabled="false" class="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtClassRoom" Width="60px" onkeypress="return isNumberKey(this,event);" autocomplete="off"
                                                        ondrop="return false;" MaxLength="2" runat="server" class="form-control"></asp:TextBox>
                                                </td>
                                                <td></td>
                                            </tr>

                                            <tr>
                                                <td style="width: 60%;">Safe drinking water
                                                </td>
                                                <td align="center">
                                                    <div runat="server">
                                                        <asp:TextBox ID="txtdrinking2" autocomplete="off" ondrop="return false;" Enabled="false"
                                                            onClick="checkFilledNew(this.id,'addcss','dr1','v1');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                    </div>
                                                </td>

                                                <td align="center">
                                                    <div runat="server">
                                                        <asp:TextBox ID="txtdrinking1" autocomplete="off" ondrop="return false;" Enabled="false"
                                                            onClick="checkFilledNew(this.id,'addcss','dr1','v1');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                    </div>
                                                </td>

                                                <td align="center">
                                                    <div id="Div8" runat="server">
                                                        <asp:TextBox ID="txtdrinking" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss','dr1','v1');"
                                                            runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>

                                                        <cc1:FilteredTextBoxExtender ID="FilteredTxtCm1_Male" TargetControlID="txtdrinking"
                                                            ValidChars="|" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 60%;">Seprate Toilet for girls
                                                </td>

                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtToilet2" runat="server" Enabled="false" autocomplete="off" ondrop="return false;"
                                                            onClick="checkFilledNew(this.id,'addcss1','t1','v2');" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtToilet1" runat="server" Enabled="false" autocomplete="off" ondrop="return false;"
                                                            onClick="checkFilledNew(this.id,'addcss1','t1','v2');" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtToilet" runat="server" autocomplete="off" ondrop="return false;"
                                                            onClick="checkFilledNew(this.id,'addcss1','t1','v2');" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtToilet"
                                                            ValidChars="|" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 60%;">Electricity
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtElectricity2" runat="server" Enabled="false" autocomplete="off"
                                                            ondrop="return false;" onClick="checkFilledNew(this.id,'addcss1','t1','v2');"
                                                            Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtElectricity1" runat="server" Enabled="false" autocomplete="off"
                                                            ondrop="return false;" onClick="checkFilledNew(this.id,'addcss1','t1','v2');"
                                                            Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtElectricity" autocomplete="off" ondrop="return false;" runat="server"
                                                            onClick="checkFilledNew(this.id,'addcss2','t2','v3');" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtElectricity"
                                                            ValidChars="|" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 60%;">Play Ground
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtPlay2" runat="server" Enabled="false" autocomplete="off" ondrop="return false;"
                                                            onClick="checkFilledNew(this.id,'addcss1','t1','v2');" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtPlay1" runat="server" Enabled="false" autocomplete="off" ondrop="return false;"
                                                            onClick="checkFilledNew(this.id,'addcss1','t1','v2');" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtPlay" autocomplete="off" ondrop="return false;" runat="server"
                                                            onClick="checkFilledNew(this.id,'addcss3','t3','v4');" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtPlay"
                                                            ValidChars="|" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="Slides">
                                                <td style="width: 60%;">Swings &amp; Slides
                                                </td>

                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtSlides2" runat="server" Enabled="false" autocomplete="off" ondrop="return false;"
                                                            onClick="checkFilledNew(this.id,'addcss1','t1','v2');" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                    </div>
                                                </td>

                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtSlides1" runat="server" Enabled="false" autocomplete="off" ondrop="return false;"
                                                            onClick="checkFilledNew(this.id,'addcss1','t1','v2');" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtSlides" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss4','t4','v5');"
                                                            runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtSlides"
                                                            ValidChars="|" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 60%;">Boundarywall
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtBoundaryWall2" runat="server" Enabled="false" autocomplete="off"
                                                            ondrop="return false;" onClick="checkFilledNew(this.id,'addcss1','t1','v2');"
                                                            Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtBoundaryWall1" runat="server" Enabled="false" autocomplete="off"
                                                            ondrop="return false;" onClick="checkFilledNew(this.id,'addcss1','t1','v2');"
                                                            Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtBoundaryWall" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss5','t5','v6');"
                                                            runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="dddd" TargetControlID="txtBoundaryWall"
                                                            ValidChars="|" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 60%;">Kitchen
                                                </td>

                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtKitchen2" runat="server" Enabled="false" autocomplete="off" ondrop="return false;"
                                                            onClick="checkFilledNew(this.id,'addcss1','t1','v2');" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtKitchen1" runat="server" Enabled="false" autocomplete="off" ondrop="return false;"
                                                            onClick="checkFilledNew(this.id,'addcss1','t1','v2');" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtKitchen" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss6','t6','v7');"
                                                            runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtKitchen"
                                                            ValidChars="|" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 60%;">Male Teacher
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMaleTeacher2" autocomplete="off" Width="60px" ondrop="return false;" MaxLength="2"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMaleTeacher1" autocomplete="off" Width="60px" ondrop="return false;" MaxLength="2"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMaleTeacher" autocomplete="off" Width="60px" ondrop="return false;" MaxLength="2"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" class="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 60%;">Female Teacher
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFemaleTeacher2" autocomplete="off" Width="60px" ondrop="return false;" MaxLength="2"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFemaleTeacher1" autocomplete="off" Width="60px" ondrop="return false;" MaxLength="2"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFemaleTeacher" autocomplete="off" Width="60px" ondrop="return false;" MaxLength="2"
                                                        onkeypress="return isNumberKey(this,event);" runat="server" class="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 60%;">Availablity of books
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtbook2" runat="server" Enabled="false" autocomplete="off" ondrop="return false;"
                                                            onClick="checkFilledNew(this.id,'addcss1','t1','v2');" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>

                                                    </div>
                                                </td>

                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtbook1" runat="server" Enabled="false" autocomplete="off" ondrop="return false;"
                                                            onClick="checkFilledNew(this.id,'addcss1','t1','v2');" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>

                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtbook" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss7','t7','v9');"
                                                            Style="width: 60px; border: none; height: 30px; border-radius: 4px;" runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtbook"
                                                            ValidChars="|" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 60%;">Use of GKP Kit
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtCltKit2" runat="server" Enabled="false" autocomplete="off" ondrop="return false;"
                                                            onClick="checkFilledNew(this.id,'addcss1','t1','v2');" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtCltKit1" runat="server" Enabled="false" autocomplete="off" ondrop="return false;"
                                                            onClick="checkFilledNew(this.id,'addcss1','t1','v2');" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div>
                                                        <asp:TextBox ID="txtCltKit" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss8','t8','v8');"
                                                            Style="width: 60px; border: none; height: 30px; border-radius: 4px;" runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtCltKit"
                                                            ValidChars="|" runat="server" />
                                                    </div>
                                                </td>

                                                <asp:TextBox ID="lbldriking" Width="1" BorderStyle="None" runat="server" class="addcss"></asp:TextBox>
                                                <asp:TextBox ID="lblToilet" BorderStyle="None" Width="1" runat="server" class="addcss1"></asp:TextBox>
                                                <asp:TextBox ID="lblElectricity" BorderStyle="None" Width="1" runat="server" class="addcss2"></asp:TextBox>
                                                <asp:TextBox ID="lblCltKit" BorderStyle="None" Width="1" runat="server" class="addcss8"></asp:TextBox>
                                                <asp:TextBox ID="lblbook" BorderStyle="None" Width="1" runat="server" class="addcss7"></asp:TextBox>
                                                <asp:TextBox ID="lblKitchen" BorderStyle="None" Width="1" runat="server" class="addcss6"></asp:TextBox>
                                                <asp:TextBox ID="lblBoundaryWall" BorderStyle="None" Width="1" runat="server" class="addcss5"></asp:TextBox>
                                                <asp:TextBox ID="lblSlides" BorderStyle="None" Width="1" runat="server" class="addcss4"></asp:TextBox>
                                                <asp:TextBox ID="lblPlay" BorderStyle="None" Width="1" runat="server" class="addcss3"></asp:TextBox>

                                                <asp:TextBox ID="lblBoysToilet" BorderStyle="None" Width="1" runat="server" class="addcss9"></asp:TextBox>
                                                <asp:TextBox ID="lblWaterSupply" BorderStyle="None" Width="1" runat="server" class="addcss10"></asp:TextBox>
                                                <asp:TextBox ID="lblTilingToilet" BorderStyle="None" Width="1" runat="server" class="addcss11"></asp:TextBox>
                                                <asp:TextBox ID="lblHandicappedAccessibleToilet" BorderStyle="None" Width="1" runat="server" class="addcss12"></asp:TextBox>
                                                <asp:TextBox ID="lblMultipleHandwashingUnit" BorderStyle="None" Width="1" runat="server" class="addcss13"></asp:TextBox>
                                                <asp:TextBox ID="lblTilingClassroomFloor" BorderStyle="None" Width="1" runat="server" class="addcss14"></asp:TextBox>
                                                <asp:TextBox ID="lblBlackboards" BorderStyle="None" Width="1" runat="server" class="addcss15"></asp:TextBox>
                                                <asp:TextBox ID="lblProperPainting" BorderStyle="None" Width="1" runat="server" class="addcss16"></asp:TextBox>
                                                <asp:TextBox ID="lblDisabledAccessibleRamp" BorderStyle="None" Width="1" runat="server" class="addcss17"></asp:TextBox>
                                                <asp:TextBox ID="lblAppropriateElectricalWiring" BorderStyle="None" Width="1" runat="server" class="addcss18"></asp:TextBox>
                                                <asp:TextBox ID="lblBoysUrinal" BorderStyle="None" Width="1" runat="server" class="addcss19"></asp:TextBox>
                                                <asp:TextBox ID="lblGirlsUrinal" BorderStyle="None" Width="1" runat="server" class="addcss20"></asp:TextBox>
                                                <asp:TextBox ID="lblFurniture" BorderStyle="None" Width="1" runat="server" class="addcss21"></asp:TextBox>
                                                <asp:TextBox ID="lblTapWaterFacility" BorderStyle="None" Width="1" runat="server" class="addcss22"></asp:TextBox>
                                            </tr>
                                            <div id="DV" style="display: none" runat="server">

                                                <tr>
                                                    <td style="width: 60%;">Boys Toilet
                                                    </td>
                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtBoysToilet2" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtBoysToilet1" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div id="Div12" runat="server">
                                                            <asp:TextBox ID="txtBoysToilet" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss9','t9','v9');"
                                                                runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" TargetControlID="txtBoysToilet"
                                                                ValidChars="|" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 60%;">Tap water supply in Toilets/Urinals
                                                    </td>
                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="TextTapWater2" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="TextTapWater1" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div id="Div13" runat="server">
                                                            <asp:TextBox ID="TextTapWater" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss10','t10','v10');"
                                                                runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" TargetControlID="TextTapWater"
                                                                ValidChars="|" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 60%;">Tiling of Toilet/Urinal
                                                    </td>
                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="TxtTiling2" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="TxtTiling1" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div id="Div14" runat="server">
                                                            <asp:TextBox ID="TxtTiling" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss11','t11','v11');"
                                                                runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" TargetControlID="TxtTiling"
                                                                ValidChars="|" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 60%;">Handicapped accessible Toilet
                                                    </td>
                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtHandicapped2" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtHandicapped1" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div id="Div15" runat="server">
                                                            <asp:TextBox ID="txtHandicapped" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss12','t12','v12');"
                                                                runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" TargetControlID="txtHandicapped"
                                                                ValidChars="|" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 60%;">Multiple Handwashing Unit
                                                    </td>
                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtMultipleHandwashing2" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtMultipleHandwashing1" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div id="Div16" runat="server">
                                                            <asp:TextBox ID="txtMultipleHandwashing" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss13','t13','v13');"
                                                                runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" TargetControlID="txtMultipleHandwashing"
                                                                ValidChars="|" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 60%;">Tiling of classroom floor
                                                    </td>
                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtTilingclassroom2" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtTilingclassroom1" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div id="Div17" runat="server">
                                                            <asp:TextBox ID="txtTilingclassroom" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss14','t14','v14');"
                                                                runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" TargetControlID="txtTilingclassroom"
                                                                ValidChars="|" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 60%;">Blackboards in all classrooms
                                                    </td>
                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtBlackboards2" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtBlackboards1" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div id="Div18" runat="server">
                                                            <asp:TextBox ID="txtBlackboards" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss15','t15','v15');"
                                                                runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" TargetControlID="txtBlackboards"
                                                                ValidChars="|" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 60%;">Proper painting of the school
                                                    </td>
                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtProperpainting2" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtProperpainting1" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div id="Div19" runat="server">
                                                            <asp:TextBox ID="txtProperpainting" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss16','t16','v16');"
                                                                runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" TargetControlID="txtProperpainting"
                                                                ValidChars="|" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 60%;">Disabled accessible ramp/railing in the school premises
                                                    </td>
                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtDisabledaccessible2" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtDisabledaccessible1" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div id="Div20" runat="server">
                                                            <asp:TextBox ID="txtDisabledaccessible" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss17','t17','v17');"
                                                                runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" TargetControlID="txtDisabledaccessible"
                                                                ValidChars="|" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 60%;">Appropriate electrical wiring and equipment in the classroom
                                                    </td>
                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtAppropriateelectrical2" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtAppropriateelectrical1" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div id="Div21" runat="server">
                                                            <asp:TextBox ID="txtAppropriateelectrical" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss18','t18','v18');"
                                                                runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" TargetControlID="txtAppropriateelectrical"
                                                                ValidChars="|" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 60%;">Boys Urinal
                                                    </td>
                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtBoysUrinal2" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtBoysUrinal1" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div id="Div22" runat="server">
                                                            <asp:TextBox ID="txtBoysUrinal" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss19','t19','v19');"
                                                                runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" TargetControlID="txtBoysUrinal"
                                                                ValidChars="|" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 60%;">Girls Urinal
                                                    </td>
                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtGirlsUrinal2" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtGirlsUrinal1" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div id="Div23" runat="server">
                                                            <asp:TextBox ID="txtGirlsUrinal" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss20','t20','v20');"
                                                                runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" TargetControlID="txtGirlsUrinal"
                                                                ValidChars="|" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 60%;">Furniture (Desk-Bench)
                                                    </td>
                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtFurniture2" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtFurniture1" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div id="Div24" runat="server">
                                                            <asp:TextBox ID="txtFurniture" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss21','t21','v21');"
                                                                runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" TargetControlID="txtFurniture"
                                                                ValidChars="|" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 60%;">Tap-water facility with water storage
                                                    </td>
                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtWaterStorage2" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div runat="server">
                                                            <asp:TextBox ID="txtWaterStorage1" autocomplete="off" ondrop="return false;" Enabled="false"
                                                                onClick="checkFilledNew(this.id,'addcss1','t1','v2');" runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td align="center">
                                                        <div id="Div25" runat="server">
                                                            <asp:TextBox ID="txtWaterStorage" autocomplete="off" ondrop="return false;" onClick="checkFilledNew(this.id,'addcss22','t22','v22');"
                                                                runat="server" Style="width: 60px; border: none; height: 30px; border-radius: 4px;"></asp:TextBox>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" TargetControlID="txtWaterStorage"
                                                                ValidChars="|" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </div>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <%--  </ContentTemplate>
                    </asp:UpdatePanel>--%>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlAnnual" runat="server" Visible="false">
                        <%-- <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>--%>
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" runat="server">
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                    <span class="">
                                        <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                            Annual Data
                                        </p>
                                    </span>
                                </button>
                            </div>
                            <div class="collapse navbar-collapse" id="Div3">
                                <div class="thumbnail" style="overflow: auto; height: 277px">
                                    <asp:ImageButton ID="ImageButton7" OnClick="btnAnnual_Click" runat="server" Width="30"
                                        Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                    <table class="table table-striped table-bordered table-hover">
                                        <tbody>
                                            <tr>
                                                <td colspan="3">
                                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                        <asp:CheckBox ID="chkAnnual" runat="server" />
                                                        Annual Data
                                                    </p>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td>
                                                    <asp:CheckBox ID="chkSIPAnnaul" runat="server" />
                                                    SIP Annual Data
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkRetention" runat="server" />
                                                    Retention Annual Data
                                                </td>
                                            </tr>
                                            <tr style="text-align: center; background-color: #f7f7f7  !important">
                                                <td>
                                                    <asp:CheckBox ID="chkSIPTB" Enabled="false" CssClass="radio" runat="server" /><label
                                                        for="ctl00_MainContent_CheckBox19">&nbsp;T.B.</label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkRenTB" Enabled="false" CssClass="radio" runat="server" /><label
                                                        for="ctl00_MainContent_CheckBox20">&nbsp;T.B.</label>
                                                </td>
                                            </tr>
                                            <tr style="text-align: center; background-color: #f7f7f7  !important">
                                                <td>
                                                    <asp:CheckBox ID="chkSIPFC" CssClass="radio" runat="server" /><label for="ctl00_MainContent_CheckBox23">&nbsp;F.C.</label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkRenFC" CssClass="radio" runat="server" /><label for="ctl00_MainContent_CheckBox24">&nbsp;F.C.</label>
                                                </td>
                                            </tr>
                                            <tr style="text-align: center; background-color: #f7f7f7  !important">
                                                <td>
                                                    <asp:RadioButton ID="chkSipPartial" GroupName="SIP" CssClass="radio" runat="server" /><label
                                                        for="ctl00_MainContent_CheckBox21">&nbsp;Partial</label>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="chkRenPartial" GroupName="REN" CssClass="radio" runat="server" /><label
                                                        for="ctl00_MainContent_CheckBox22">&nbsp;Partial</label>
                                                </td>
                                            </tr>
                                            <tr style="text-align: center; background-color: #f7f7f7  !important">
                                                <td>
                                                    <asp:RadioButton ID="chkSipComplete" GroupName="SIP" CssClass="radio" runat="server" /><label
                                                        for="ctl00_MainContent_CheckBox25">&nbsp;Complete</label>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="chkComplete" GroupName="REN" CssClass="radio" runat="server" /><label
                                                        for="ctl00_MainContent_CheckBox26">&nbsp;Complete</label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                    </asp:Panel>
                    <asp:Panel ID="pnlSchoolContact" runat="server" Visible="false">
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                </button>
                            </div>
                            <asp:Label ID="Label5" runat="server" Visible="false" Text="Label"></asp:Label>
                            <div class="collapse navbar-collapse" id="Didv4">
                                <div class="thumbnail" style="height: 344px; overflow: auto">
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </asp:Panel>

            <asp:TextBox ID="txtCountDriking" Width="1" BorderStyle="None" runat="server" class="dr1"></asp:TextBox>
            <asp:TextBox ID="TextBox1" BorderStyle="None" Width="1" runat="server" class="t1"></asp:TextBox>
            <asp:TextBox ID="TextBox2" BorderStyle="None" Width="1" runat="server" class="t2"></asp:TextBox>
            <asp:TextBox ID="TextBox3" BorderStyle="None" Width="1" runat="server" class="t3"></asp:TextBox>
            <asp:TextBox ID="TextBox4" BorderStyle="None" Width="1" runat="server" class="t4"></asp:TextBox>
            <asp:TextBox ID="TextBox5" BorderStyle="None" Width="1" runat="server" class="t5"></asp:TextBox>
            <asp:TextBox ID="TextBox6" BorderStyle="None" Width="1" runat="server" class="t6"></asp:TextBox>
            <asp:TextBox ID="TextBox7" BorderStyle="None" Width="1" runat="server" class="t7"></asp:TextBox>
            <asp:TextBox ID="TextBox8" BorderStyle="None" Width="1" runat="server" class="t8"></asp:TextBox>
            <asp:TextBox ID="TextBox9" BorderStyle="None" Width="1" runat="server" class="t9"></asp:TextBox>
            <asp:TextBox ID="TextBox10" BorderStyle="None" Width="1" runat="server" class="t10"></asp:TextBox>
            <asp:TextBox ID="TextBox11" BorderStyle="None" Width="1" runat="server" class="t11"></asp:TextBox>
            <asp:TextBox ID="TextBox12" BorderStyle="None" Width="1" runat="server" class="t12"></asp:TextBox>
            <asp:TextBox ID="TextBox13" BorderStyle="None" Width="1" runat="server" class="t13"></asp:TextBox>
            <asp:TextBox ID="TextBox14" BorderStyle="None" Width="1" runat="server" class="t14"></asp:TextBox>
            <asp:TextBox ID="TextBox15" BorderStyle="None" Width="1" runat="server" class="t15"></asp:TextBox>
            <asp:TextBox ID="TextBox16" BorderStyle="None" Width="1" runat="server" class="t16"></asp:TextBox>
            <asp:TextBox ID="TextBox17" BorderStyle="None" Width="1" runat="server" class="t17"></asp:TextBox>
            <asp:TextBox ID="TextBox18" BorderStyle="None" Width="1" runat="server" class="t18"></asp:TextBox>
            <asp:TextBox ID="TextBox19" BorderStyle="None" Width="1" runat="server" class="t19"></asp:TextBox>
            <asp:TextBox ID="TextBox20" BorderStyle="None" Width="1" runat="server" class="t20"></asp:TextBox>
            <asp:TextBox ID="TextBox21" BorderStyle="None" Width="1" runat="server" class="t21"></asp:TextBox>
            <asp:TextBox ID="TextBox22" BorderStyle="None" Width="1" runat="server" class="t22"></asp:TextBox>


            <asp:TextBox ID="txt1" BorderStyle="None" Width="1" runat="server" class="v1"></asp:TextBox>
            <asp:TextBox ID="txt2" BorderStyle="None" Width="1" runat="server" class="v2"></asp:TextBox>
            <asp:TextBox ID="txt3" BorderStyle="None" Width="1" runat="server" class="v3"></asp:TextBox>
            <asp:TextBox ID="txt4" BorderStyle="None" Width="1" runat="server" class="v4"></asp:TextBox>
            <asp:TextBox ID="txt5" BorderStyle="None" Width="1" runat="server" class="v5"></asp:TextBox>
            <asp:TextBox ID="txt6" BorderStyle="None" Width="1" runat="server" class="v6"></asp:TextBox>
            <asp:TextBox ID="txt7" BorderStyle="None" Width="1" runat="server" class="v7"></asp:TextBox>
            <asp:TextBox ID="txt8" BorderStyle="None" Width="1" runat="server" class="v8"></asp:TextBox>
            <asp:TextBox ID="txt9" BorderStyle="None" Width="1" runat="server" class="v9"></asp:TextBox>
            <asp:TextBox ID="txt10" BorderStyle="None" Width="1" runat="server" class="v10"></asp:TextBox>
            <asp:TextBox ID="txt11" BorderStyle="None" Width="1" runat="server" class="v11"></asp:TextBox>
            <asp:TextBox ID="txt12" BorderStyle="None" Width="1" runat="server" class="v12"></asp:TextBox>
            <asp:TextBox ID="txt13" BorderStyle="None" Width="1" runat="server" class="v13"></asp:TextBox>
            <asp:TextBox ID="txt14" BorderStyle="None" Width="1" runat="server" class="v14"></asp:TextBox>
            <asp:TextBox ID="txt15" BorderStyle="None" Width="1" runat="server" class="v15"></asp:TextBox>
            <asp:TextBox ID="txt16" BorderStyle="None" Width="1" runat="server" class="v16"></asp:TextBox>
            <asp:TextBox ID="txt17" BorderStyle="None" Width="1" runat="server" class="v17"></asp:TextBox>
            <asp:TextBox ID="txt18" BorderStyle="None" Width="1" runat="server" class="v18"></asp:TextBox>
            <asp:TextBox ID="txt19" BorderStyle="None" Width="1" runat="server" class="v19"></asp:TextBox>
            <asp:TextBox ID="txt20" BorderStyle="None" Width="1" runat="server" class="v20"></asp:TextBox>
            <asp:TextBox ID="txt21" BorderStyle="None" Width="1" runat="server" class="v2118"></asp:TextBox>
            <asp:TextBox ID="txt22" BorderStyle="None" Width="1" runat="server" class="v22"></asp:TextBox>
            <asp:TextBox ID="txt23" BorderStyle="None" Width="1" runat="server" class="v23"></asp:TextBox>

            <cc1:ModalPopupExtender ID="MpexdrDistrict" runat="server" BackgroundCssClass="modalBg "
                PopupControlID="PnlDistrict" CancelControlID="CancelButton" TargetControlID="HdnFild7">
            </cc1:ModalPopupExtender>
            <asp:HiddenField ID="HdnFild7" runat="server"></asp:HiddenField>
            <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important; margin-top: 125px !important;"
                ID="PnlDistrict" runat="server">
                <div style="width: 100%; height: auto; background-color: #f1f1f1">
                    <div class="modal-header" style="background-color: #3ac0f2; color: White;">
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                        <div class="form-horizontal">
                        </div>
                        <asp:ImageMap ID="imgMKS" runat="server" Height="250px" Width="400px" BorderColor="Black"
                            BorderStyle="Ridge" BorderWidth="1px" />
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="CancelButton" runat="server" CssClass="btn bgm-cyan" Text="Close"
                            ToolTip="Close" Style="float: none;"></asp:Button>
                    </div>
                </div>
            </asp:Panel>

            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Hdn_model4"
                PopupControlID="pnlpopup4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:HiddenField ID="Hdn_model4" runat="server" />
            <asp:Panel ID="pnlpopup4" runat="server" Style="display: none;">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-header" style="height: 0px;">
                            <asp:ImageButton ID="ImageButton9" CssClass="btn btn-info pull-right" OnClick="btnReset_Click"
                                BackColor="#f5f5f5" ToolTip="Add" ImageUrl="~/images/close-29.png" Style="margin-right: 5px; padding: 0px;"
                                runat="server" />
                            <h4 class="modal-title">Remarks</h4>
                        </div>
                        <div class="row">
                            <div class="row marg search-bg">
                                <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                                    <div class="col-lg-8 col-md-8  col-sm-8 cpl-xs-12">
                                        <div class="form-group" style="margin-bottom: 2px;">
                                            <label for="email" class="col-sm-3 padd linhei">
                                                Remarks:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlRemark" runat="server" class="form-control">
                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">Format not available</asp:ListItem>
                                                    <asp:ListItem Value="2">Wrongly activity selected </asp:ListItem>
                                                    <asp:ListItem Value="3">Typing error</asp:ListItem>
                                                    <asp:ListItem Value="4">Counting error</asp:ListItem>
                                                    <asp:ListItem Value="5">C Phone not available</asp:ListItem>
                                                </asp:DropDownList>
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
