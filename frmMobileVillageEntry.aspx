<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" Culture="en-GB"
    CodeFile="frmMobileVillageEntry.aspx.cs" Inherits="frmMobileVillageEntry" %>

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
    </style>
    <script type="text/javascript">
        function SetMultilanguageNew(Flag, clsname) {
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
            if (Flag == 'FO') {
                if (maxSelection <= 10) {
                    $('#<%=hdn_AwarenessFo.ClientID %>').val(lid);
                    $('#<%=hdn_PBAwarenessFo.ClientID %>').val(Lngg);
                    $('#<%=txtAwarenessFo.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_AwarenessFo.ClientID%>').val('');
                    $('#<%=hdn_PBAwarenessFo.ClientID %>').val('');
                    $('#<%=txtAwarenessFo.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }
            }

            if (Flag == 'KO') {
                if (maxSelection <= 10) {
                    $('#<%=HiddenField1.ClientID%>').val('');
                    $('#<%=HiddenField2.ClientID %>').val(Lngg);
                    $('#<%=txtBanding.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=HiddenField1.ClientID%>').val('');
                    $('#<%=HiddenField2.ClientID %>').val('');
                    $('#<%=txtBanding.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }
            }
            if (Flag == 'IO') {
                if (maxSelection <= 10) {
                    $('#<%=hdn_AwarenessIo.ClientID %>').val(lid);
                    $('#<%=hdn_PBAwarenessIo.ClientID %>').val(Lngg);
                    $('#<%=txtAwarenessIo.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_AwarenessIo.ClientID%>').val('');
                    $('#<%=hdn_PBAwarenessIo.ClientID %>').val('');
                    $('#<%=txtAwarenessIo.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }

            }

            if (Flag == 'EO') {
                if (maxSelection <= 10) {
                    $('#<%=hdn_AwarenessEo.ClientID %>').val(lid);
                    $('#<%=hdn_PBAwarenessEo.ClientID %>').val(Lngg);
                    $('#<%=txtAwarenessEo.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_AwarenessEo.ClientID%>').val('');
                    $('#<%=hdn_PBAwarenessEo.ClientID %>').val('');
                    $('#<%=txtAwarenessEo.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }

            }
        }
        function arrivaldatecheck(sender, args) {
            var depdate = 'dep';

            var departuredate = $('.' + depdate).val();
            var arrivaldate = sender._selectedDate;
            var today = new Date();
            debugger;
            var day = sender._selectedDate.getDate();
            var month = sender._selectedDate.getMonth() + 1;
            var year = sender._selectedDate.getFullYear();

            var date = year + "-" + month + "-" + day;



            var day1 = today.getDate();
            var month1 = today.getMonth() + 1;
            var year1 = today.getFullYear();

            var date1 = year1 + "-" + month1 + "-" + day1;

            var D1 = new Date(date);
            var D2 = new Date(date1);
            if (D1 >= D2) {
                alert("Should not be future date or today date.");
                sender._textbox.set_Value("")

                return false;

            }

        }
    </script>
    <script type="text/javascript">
        function arrivaldatecheckB(sender, args) {
            var depdate = 'dep';
            var Div787 = $('#DivI5');
            debugger;




            var departuredate = $('.' + depdate).val();
            var arrivaldate = sender._selectedDate;
            var today = new Date();

            var day = sender._selectedDate.getDate();
            var month = sender._selectedDate.getMonth() + 1;
            var year = sender._selectedDate.getFullYear();

            var date = year + "-" + month + "-" + day;



            var day1 = today.getDate();
            var month1 = today.getMonth() + 1;
            var year1 = today.getFullYear();

            var date1 = year1 + "-" + month1 + "-" + day1;

            var D1 = new Date(date);
            var D2 = new Date(date1);
            if (D1 >= D2) {
                alert("Should not be future date or today date.");
                sender._textbox.set_Value("")
                $('.clsD').hide();
                return false;

            }
            else {
                $('.clsD').show();
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

                if (Lngg.toLowerCase().indexOf("others (specify)") >= 0) {

                    $('#<%=txt_bookformatOther.ClientID %>').val('');
                    $('#<%=txt_bookformatOther.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txt_bookformatOther.ClientID %>').val('');
                    $('#<%=txt_bookformatOther.ClientID %>').attr('disabled', true);
                }
            }
            else if (Flag == 'FN') {


                if (Lngg.toLowerCase().indexOf("other(detail)") >= 0) {

                    $('#<%=txt_bookformatOther1.ClientID %>').val('');
                    $('#<%=txt_bookformatOther1.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txt_bookformatOther1.ClientID %>').val('');
                    $('#<%=txt_bookformatOther1.ClientID %>').attr('disabled', true);
                }
            }
            else if (Flag == 'FN1') {


            }
            else if (Flag == 'M') {

                if (Lngg.toLowerCase().indexOf("others (specify)") >= 0) {

                    $('#<%=txtmOther.ClientID %>').val('');
                    $('#<%=txtmOther.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txtmOther.ClientID %>').val('');
                    $('#<%=txtmOther.ClientID %>').attr('disabled', true);
                }
            }
            else if (Flag == 'MN') {


                if (Lngg.toLowerCase().indexOf("other(detail)") >= 0) {


                    $('#<%=txtmOther1.ClientID %>').val('');
                    $('#<%=txtmOther1.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txtmOther1.ClientID %>').val('');
                    $('#<%=txtmOther1.ClientID %>').attr('disabled', true);
                }
            }
            else if (Flag == 'MN1') {


            }
            else if (Flag == 'OC') {


                if (Lngg.toLowerCase().indexOf("others (specify)") >= 0) {

                    $('#<%=txtOtherComm.ClientID %>').val('');
                    $('#<%=txtOtherComm.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txtOtherComm.ClientID %>').val('');
                    $('#<%=txtOtherComm.ClientID %>').attr('disabled', true);
                }
            }
            else if (Flag == 'OCN') {


                if (Lngg.toLowerCase().indexOf("other(detail)") >= 0) {

                    $('#<%=txtOtherComm1.ClientID %>').val('');
                    $('#<%=txtOtherComm1.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txtOtherComm1.ClientID %>').val('');
                    $('#<%=txtOtherComm1.ClientID %>').attr('disabled', true);
                }
            }
            else if (Flag == 'FO') {



            }
            else if (Flag == 'CC') {

                if (Lngg.toLowerCase().indexOf("others (specify)") >= 0) {

                    $('#<%=txtOtherCon.ClientID %>').val('');
                    $('#<%=txtOtherCon.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txtOtherCon.ClientID %>').val('');
                    $('#<%=txtOtherCon.ClientID %>').attr('disabled', true);
                }
            }
            else if (Flag == 'CCC') {

                if (Lngg.toLowerCase().indexOf("others") >= 0) {


                    $('#<%=txt_con_other.ClientID %>').val('');
                    $('#<%=txt_con_other.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txt_con_other.ClientID %>').val('');
                    $('#<%=txt_con_other.ClientID %>').attr('disabled', true);
                }
            }

            else if (Flag == 'CC2') {



                if (Lngg.toLowerCase().indexOf("others (specify)") >= 0) {


                    $('#<%=txtoC111.ClientID %>').val('');
                    $('#<%=txtoC111.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txtoC111.ClientID %>').val('');
                    $('#<%=txtoC111.ClientID %>').attr('disabled', true);
                }
            }
            else if (Flag == 'SS') {

                if (Lngg.toLowerCase().indexOf("other") >= 0) {

                    $('#<%=txtOtherSupport.ClientID %>').val('');
                    $('#<%=txtOtherSupport.ClientID %>').attr('disabled', false);
                }
                else {

                    $('#<%=txtOtherSupport.ClientID %>').val('');
                    $('#<%=txtOtherSupport.ClientID %>').attr('disabled', true);
                }
            }


        }



    </script>
    <style type="text/css">
        .checkbox label:after, .radio label:after {
            content: '';
            display: table;
            clear: both;
        }

        .checkbox .cr, .radio .cr {
            position: relative;
            display: inline-block;
            border: 2px solid #333;
            border-radius: .25em;
            width: 1.3em;
            height: 1.3em;
            float: left;
            margin-right: .5em;
            color: red;
        }

        .radio .cr {
            border-radius: 75%;
            border-color: #333;
        }

            .checkbox .cr .cr-icon, .radio .cr .cr-icon {
                position: absolute;
                font-size: .8em;
                line-height: 0;
                top: 50%;
                left: 15%;
            }

            .radio .cr .cr-icon {
                margin-left: 0.04em;
            }

        .checkbox label input[type="checkbox"], .radio label input[type="radio"] {
            display: none;
        }

            .checkbox label input[type="checkbox"] + .cr > .cr-icon, .radio label input[type="radio"] + .cr > .cr-icon {
                transform: scale(3) rotateZ(-220deg);
                opacity: 0;
                transition: all .7s ease-in;
            }

            .checkbox label input[type="checkbox"]:checked + .cr > .cr-icon, .radio label input[type="radio"]:checked + .cr > .cr-icon {
                transform: scale(1) rotateZ(0deg);
                opacity: 1;
            }

            .checkbox label input[type="checkbox"]:disabled + .cr, .radio label input[type="radio"]:disabled + .cr {
                opacity: .5;
            }

        .new-navbutt {
            float: left !important;
            margin-top: 0px !important;
        }

        .row-border {
            border-bottom: 1px dotted rgb(221, 221, 221);
            margin-bottom: 15px;
        }

        .checkbox {
            position: relative;
            display: block;
            margin-top: 2px !important;
            margin-bottom: 5px !important;
        }


        .new-navbutt {
            float: left !important;
            margin-top: 0px !important;
        }

        .row-border {
            border-bottom: 1px dotted rgb(221, 221, 221);
            margin-bottom: 15px;
        }

        .checkbox .cr .cr-icon, .radio .cr .cr-icon {
            position: absolute;
            font-size: .8em;
            line-height: 0;
            top: 50%;
            left: 15%;
        }

        .radio .cr .cr-icon {
            margin-left: 0.04em;
        }

        .checkbox label input[type="checkbox"], .radio label input[type="radio"] {
            display: none;
        }

            .checkbox label input[type="checkbox"] + .cr > .cr-icon, .radio label input[type="radio"] + .cr > .cr-icon {
                transform: scale(3) rotateZ(-220deg);
                opacity: 0;
                transition: all .7s ease-in;
            }

            .checkbox label input[type="checkbox"]:checked + .cr > .cr-icon, .radio label input[type="radio"]:checked + .cr > .cr-icon {
                transform: scale(1) rotateZ(0deg);
                opacity: 1;
            }

            .checkbox label input[type="checkbox"]:disabled + .cr, .radio label input[type="radio"]:disabled + .cr {
                opacity: .5;
            }

        .new-navbutt {
            float: left !important;
            margin-top: 0px !important;
        }

        .row-border {
            border-bottom: 1px dotted rgb(221, 221, 221);
            margin-bottom: 15px;
        }

        .checkbox {
            position: relative;
            display: block;
            margin-top: 2px !important;
            margin-bottom: 5px !important;
        }

        .CheckBoxListCssClass {
            font-family: calibri;
            margin-left: 5p .checkbox

        {
            position: relative;
            display: block;
            margin-top: 2px !important;
            margin-bottom: 5px !important;
        }

        .CheckBoxListCssClass {
            font-family: calibri;
            margin-left: 5px;
            font-weight: bold;
            font-size: small;
            top: 53%;
            left: 3%;
        }

        .checkboxlist {
            position: absolute;
            font-size: .8em;
            margin-left: 10px;
            line-height: 0;
            top: 50%;
            left: 15%;
        }

        .td-widt {
            width: auto !important;
        }

        .td-width1 {
            width: 150px !important;
        }

        @media (min-width:10px) and (max-width:640px) {
            .td-widt {
                width: 90px !important;
            }


            .td-width1 {
                width: 90px !important;
            }
        }

        .table-mb {
            margin-bottom: 2px !important;
        }

        .thnail {
            padding: 0px !important;
            border-radius: 0px !important;
            margin-bottom: 0px !important;
            min-height: 60px;
        }
    </style>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="text-danger" style="margin: 0px;">Village Activity instead of village</h3>
                        </div>
                        <div class="panel-body" style="padding: 0px 10px 4px 10px;">
                            <div class="row">
                                <div class="row marg search-bg" style="margin-left: 0px;">
                                    <div class="form-horizontal">
                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
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
                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                            <div class="form-group" style="margin-bottom: 7px;">
                                                <label for="email" class="col-sm-3 padd linhei">
                                                    Village:</label>
                                                <div class="col-sm-9 padd">
                                                    <asp:DropDownList ID="ddlVilage" OnSelectedIndexChanged="ddlVilage_SelectedIndexChanged" AutoPostBack="true"
                                                        runat="server" class="form-control " />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                            <div class="form-group" style="margin-bottom: 7px;">
                                                <label for="email" class="col-sm-3 padd linhei">
                                                    Date:</label>
                                                <div class="col-sm-9 padd">
                                                    <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="currencyTextBox_TextChanged" ID="txtDate" autocomplete="off" ondrop="return false;"
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
                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                            <div class="form-group" style="margin-bottom: 7px;">
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
                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                            <asp:Button ID="btnApprove" CssClass="btn btn-success pull-right " ToolTip="Save"
                                                Text="  Back" OnClick="btnApprove_Click" Style="margin-right: 5px;"
                                                runat="server" />
                                            <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right"
                                                ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px; padding: 0px;"
                                                runat="server" />

                                            <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" OnClick="btnSave_Click"
                                                BackColor="#f5f5f5" ToolTip="Save" ImageUrl="~/images/save-29-1.png" ValidationGroup="saves"
                                                Style="margin-right: 5px; padding: 0px;" runat="server" />
                                            <asp:ImageButton ID="ImageButton1" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                ToolTip="Add" Visible="false" ImageUrl="~/images/add-29-1.png" OnClick="btnAdd_Click"
                                                Style="margin-right: 5px; padding: 0px;" runat="server" />
                                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" OnClick="btnSerach_Click"
                                                class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />
                                            <asp:ImageButton ID="btnEdit" Visible="false" ToolTip="Edit" OnClick="btnEdit_Click" runat="server"
                                                class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1" ImageUrl="~/images/edit.png" />
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>

                    </div>
                </div>
                <asp:Label ID="lblMM" Visible="false" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblGG" Visible="false" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblCom" Visible="false" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblCom1" Visible="false" runat="server" Text="Label"></asp:Label>

            </div>
            <div>
                <asp:CheckBox ID="rblTbhold" Enabled="false" Visible="false" CssClass="cr-icon" runat="server" />

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width: 100%; padding-left: 15px;">
                    <table class="table table table-bordered table-hover">
                        <tr>
                            <td class="td-width1">T.B.Hand Holding
                                               <asp:CheckBox ID="rblFcHold" Style="padding    -right: 1103px; float: right;" GroupName="A" CssClass="cr-icon" runat="server" />
                            </td>

                        </tr>

                    </table>
                </div>
                <asp:Panel ID="pnlMain" runat="server" Enabled="false">
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                <span class="">
                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                        GSS
                                    </p>
                                </span>
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="Div4" style="padding: 0px;">
                            <div class="thumbnail" style="height: 875px; overflow: auto">
                                <asp:ImageButton ID="ImageButton2" runat="server" OnClick="btnCLT_Click" Width="30"
                                    Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                <table class="table table table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td style="width: 150px;">
                                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                    <asp:CheckBox Enabled="false" ID="chkmcommmeting" runat="server" />
                                                    GSS
                                                </p>
                                            </td>
                                            <td style="padding        -left: 33px;">
                                                <asp:RadioButton ID="chkcommmetingTB" GroupName="B" OnCheckedChanged="rblTb_Click" AutoPostBack="true" CssClass="radio" runat="server" />
                                                <%--<input name="" value="" type="radio">--%>
                                                    TB
                                            </td>
                                            <td style="padding        -left: 33px;">
                                                <asp:RadioButton ID="chkcommmetingFC" GroupName="B" OnCheckedChanged="rblTb_Click" AutoPostBack="true" CssClass="radio" runat="server" />
                                                FC
                                            </td>
                                        </tr>

                                        <tr runat="server" id="trGssId" visible="false">
                                            <td style="width: 150px;">
                                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                    TB Name
                                                </p>
                                            </td>
                                            <td colspan="2" style="padding        -left: 33px;">
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlGssTbname"></asp:DropDownList>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Muhalla </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtMumaullGss" autocomplete="off" ondrop="return false;" MaxLength="50"
                                                    CssClass="form-control" runat="server"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td>Was the BO informed 2 days ago

                                            </td>

                                            <td colspan="2">
                                                <asp:DropDownList ID="ddlBo" CssClass="form-control" runat="server">

                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes </asp:ListItem>
                                                    <asp:ListItem Value="2">No </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="rdEnrollMent" GroupName="rdEnRe" Text="Enrollment" runat="server" />
                                            </td>
                                            <td colspan="2">
                                                <asp:RadioButton ID="rdRetention" Text="Retention" GroupName="rdEnRe" runat="server" />
                                            </td>

                                        </tr>

                                        <tr>


                                            <td colspan="3">
                                                <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                    ID="Label7" Text="Objective of Meeting" runat="server"></asp:Label>

                                                <asp:CheckBoxList ID="CBL_bookformat" runat="server" onclick="SetMultilanguage('F','_bookformat');" CssClass="chkBoxList _bookformat" RepeatColumns="2" RepeatDirection="Vertical"></asp:CheckBoxList>
                                                <asp:TextBox runat="server" ID="txt_bookformatOther" CssClass="form-control" MaxLength="30"
                                                    TabIndex="18"></asp:TextBox>
                                                <asp:TextBox ID="txt_pbname" Visible="false" autocomplete="off" ondrop="return false;" runat="server"
                                                    CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                <cc1:PopupControlExtender ID="PopupControltxt_pbname" runat="server" TargetControlID="txt_pbname"
                                                    PopupControlID="pnt_bookformat" OffsetY="22">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="pnt_bookformat" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 45%"
                                                    CssClass="panel">
                                                    <span>
                                                        <asp:CheckBoxList ID="CBL_bookformat11" CssClass="_bookformat radio" runat="server"
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
                                                <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                    ID="dd" Text="Highlights of Discussion" runat="server"></asp:Label>
                                                <asp:CheckBoxList ID="CBL_bookformatNew" onclick="SetMultilanguage('FN','_bookformat9');" CssClass="_bookformat9" runat="server" RepeatColumns="2"
                                                    RepeatDirection="Vertical">
                                                </asp:CheckBoxList>
                                                <asp:TextBox runat="server" ID="txt_bookformatOther1" CssClass="form-control" MaxLength="30"
                                                    TabIndex="18"></asp:TextBox>
                                                <asp:TextBox ID="txt_pbnameNew" Visible="false" autocomplete="off" ondrop="return false;" runat="server"
                                                    CssClass="form-control col-sm-1" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                <cc1:PopupControlExtender ID="PopupControltxt_pbnameNew" runat="server" TargetControlID="txt_pbnameNew"
                                                    PopupControlID="pnt_txt_pbnameNew" OffsetY="22">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="pnt_txt_pbnameNew" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 45%"
                                                    CssClass="panel">
                                                    <span>
                                                        <asp:CheckBoxList ID="CBL_bookformatNew99" CssClass="_bookformat9 radio" runat="server"
                                                            onclick="SetMultilanguage('FN','_bookformat9');">
                                                        </asp:CheckBoxList>
                                                    </span>
                                                    <asp:HiddenField runat="server" ID="hdn_pbnameNew" />
                                                    <asp:HiddenField runat="server" ID="hdn_PBIDNew" />
                                                </asp:Panel>
                                            </td>
                                        </tr>

                                        <tr>

                                            <td colspan="3">
                                                <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                    ID="Label8" Text="Key Participants" runat="server"></asp:Label>

                                                <asp:CheckBoxList ID="CBL_bookformatNew1" onclick="SetMultilanguage('FN1','_btxt_pbnameNew1');" CssClass="chkBoxList _btxt_pbnameNew1" runat="server" RepeatColumns="2"
                                                    RepeatDirection="Vertical">
                                                </asp:CheckBoxList>
                                                <asp:TextBox ID="txt_pbnameNew1" Visible="false" autocomplete="off" ondrop="return false;" runat="server"
                                                    CssClass="form-control col-sm-1" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                <cc1:PopupControlExtender ID="PopupControltxt_pbnameNew1" runat="server" TargetControlID="txt_pbnameNew1"
                                                    PopupControlID="pnt_txt_pbnameNew1" OffsetY="22">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="pnt_txt_pbnameNew1" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 50%"
                                                    CssClass="panel">
                                                    <span>
                                                        <asp:CheckBoxList ID="CBL_bookformatNew19999" CssClass="_btxt_pbnameNew1 radio" runat="server"
                                                            onclick="SetMultilanguage('FN1','_btxt_pbnameNew1');">
                                                        </asp:CheckBoxList>
                                                    </span>
                                                    <asp:HiddenField runat="server" ID="hdntxt_pbnameNew1_Name" />
                                                    <asp:HiddenField runat="server" ID="hdntxt_pbnameNew1_ID" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Attendance-Female
                                            </td>

                                            <td colspan="2">
                                                <asp:TextBox ID="TxtGSS_FeMale" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                    CssClass="form-control" runat="server"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilterTxtGSS_FeMale" TargetControlID="TxtGSS_FeMale"
                                                    ValidChars="0123456789" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Attendance-Male
                                            </td>

                                            <td colspan="2">
                                                <asp:TextBox ID="TxtGSS_Male" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                    CssClass="form-control" runat="server"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilterTxtGSS_Male" TargetControlID="TxtGSS_Male"
                                                    ValidChars="0123456789" runat="server" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Present OOSC/No. of parents of irregular childeren

                                            </td>

                                            <td colspan="2">
                                                <asp:TextBox ID="txtV1illager" autocomplete="off" ondrop="return false;" MaxLength="2"
                                                    onkeypress="return isNumberKey(this,event);" CssClass="form-control" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td>Image </td>

                                            <td colspan="2">
                                                <asp:ImageButton ID="imgGSS" runat="server" Width="30" Height="25" OnClick="btnImgGss_Click"
                                                    Visible="false" CssClass="pull-right" ImageUrl="~/images/iconimage-128.png"></asp:ImageButton></td>

                                        </tr>
                                    </tbody>
                                </table>


                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" style="padding: 0px;">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                <span class="">
                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                        Mauhalla Meeting
                                    </p>
                                </span>
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="Div1" style="padding: 0px;">
                            <div class="thumbnail" style="height: 875px; overflow: auto">
                                <asp:ImageButton ID="ImageButton4" runat="server" OnClick="btnmm_Click" Width="30"
                                    Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                <table class="table table table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td style="width: 150px;">
                                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                    <asp:CheckBox Enabled="false" ID="chkmuhala" runat="server" />
                                                    Mauhalla Meeting
                                                </p>
                                            </td>
                                            <td style="padding        -left: 33px;">
                                                <asp:RadioButton ID="rblmuhulaTb" GroupName="C" OnCheckedChanged="rblTbmm_Click" AutoPostBack="true" CssClass="radio" runat="server" />

                                                TB
                                            </td>
                                            <td style="padding        -left: 33px;">
                                                <asp:RadioButton ID="rblmuhulaFC" GroupName="C" OnCheckedChanged="rblTbmm_Click" AutoPostBack="true" CssClass="radio" runat="server" />
                                                FC
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trmmId" visible="false">
                                            <td style="width: 150px;">
                                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                    TB Name
                                                </p>
                                            </td>
                                            <td colspan="2" style="padding        -left: 33px;">
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlMMTb"></asp:DropDownList>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="rdEnrollment1" Text="Enrollment" GroupName="rdEnRek" runat="server" />
                                            </td>
                                            <td colspan="2">
                                                <asp:RadioButton ID="rdRetantion1" Text="Retention" GroupName="rdEnRek" runat="server" />
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Muhalla </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtMumaullmm" autocomplete="off" ondrop="return false;" MaxLength="50"
                                                    CssClass="form-control" runat="server"></asp:TextBox></td>

                                        </tr>
                                        <tr>

                                            <td colspan="3">
                                                <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                    ID="Label9" Text="Objective of Meeting" runat="server"></asp:Label>

                                                <asp:CheckBoxList ID="CBL_Muhula" CssClass="chkBoxList _bookformat1" runat="server" onclick="SetMultilanguage('M','_bookformat1');" RepeatColumns="2" RepeatDirection="Vertical"></asp:CheckBoxList>

                                                <asp:TextBox ID="txtMuhala" Visible="false" runat="server" autocomplete="off" ondrop="return false;"
                                                    CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="txtMuhala"
                                                    PopupControlID="pnt_Muhula" OffsetY="22">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="pnt_Muhula" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 35%"
                                                    CssClass="panel">
                                                    <span>
                                                        <asp:CheckBoxList ID="CBL_Muhula999" CssClass="_bookformat1 radio" runat="server" onclick="SetMultilanguage('M','_bookformat1');">
                                                        </asp:CheckBoxList>
                                                    </span>
                                                    <asp:HiddenField runat="server" ID="hhmuhulaid" />
                                                    <asp:HiddenField runat="server" ID="HidName" />
                                                </asp:Panel>
                                                <asp:TextBox runat="server" ID="txtmOther" autocomplete="off" ondrop="return false;"
                                                    CssClass="form-control" MaxLength="30" TabIndex="18"></asp:TextBox>
                                            </td>
                                        </tr>



                                        <tr>

                                            <td colspan="3">

                                                <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                    ID="Label1" Text=" Highlights of Discussion" runat="server"></asp:Label>

                                                <asp:CheckBoxList ID="CBL_MuhulaNew" onclick="SetMultilanguage('MN','_bookformat10');" CssClass="_bookformat10" runat="server" RepeatColumns="2" RepeatDirection="Vertical"></asp:CheckBoxList>

                                                <asp:TextBox ID="txtMuhalaNew" Visible="false" autocomplete="off" ondrop="return false;" runat="server"
                                                    CssClass="form-control col-sm-1" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                <cc1:PopupControlExtender ID="PopupControltxtMuhalaNew" runat="server" TargetControlID="txtMuhalaNew"
                                                    PopupControlID="pnt_txtMuhalaNew" OffsetY="22">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="pnt_txtMuhalaNew" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 45%"
                                                    CssClass="panel">
                                                    <span>
                                                        <asp:CheckBoxList ID="CBL_MuhulaNew9645" CssClass="_bookformat10 radio" runat="server"
                                                            onclick="SetMultilanguage('MN','_bookformat10');">
                                                        </asp:CheckBoxList>
                                                    </span>
                                                    <asp:HiddenField runat="server" ID="hdn_txtMuhalaNew_Name" />
                                                    <asp:HiddenField runat="server" ID="hnd_txtMuhalaNew_ID" />
                                                </asp:Panel>
                                                <asp:TextBox runat="server" ID="txtmOther1" CssClass="form-control" MaxLength="30"
                                                    TabIndex="18"></asp:TextBox>
                                            </td>

                                        </tr>

                                        <tr>


                                            <td colspan="3">
                                                <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                    ID="Label10" Text=" Key Participants" runat="server"></asp:Label>

                                                <asp:CheckBoxList ID="CBL_MuhulaNew1" runat="server" RepeatColumns="2" CssClass="chkBoxList" RepeatDirection="Vertical"></asp:CheckBoxList>

                                                <asp:TextBox ID="txtMuhalaNew1" Visible="false" autocomplete="off" ondrop="return false;" runat="server"
                                                    CssClass="form-control col-sm-1" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                <cc1:PopupControlExtender ID="PopupControltxtMuhalaNew1" runat="server" TargetControlID="txtMuhalaNew1"
                                                    PopupControlID="pnt_txtMuhalaNew1" OffsetY="22">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="pnt_txtMuhalaNew1" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 50%"
                                                    CssClass="panel">
                                                    <span>
                                                        <asp:CheckBoxList ID="CBL_MuhulaNew13333333" CssClass="_btxtMuhalaNew1 radio" runat="server"
                                                            onclick="SetMultilanguage('MN1','_btxtMuhalaNew1');">
                                                        </asp:CheckBoxList>
                                                    </span>
                                                    <asp:HiddenField runat="server" ID="hdn_Name_txtMuhalaNew1" />
                                                    <asp:HiddenField runat="server" ID="hdn_ID_txtMuhalaNew1" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Attendance-Female
                                            </td>

                                            <td colspan="2">
                                                <asp:TextBox ID="TxtMM_FeMale" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                    CssClass="form-control" runat="server"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTxtMM_FeMale" TargetControlID="TxtMM_FeMale"
                                                    ValidChars="0123456789" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Attendance-Male
                                            </td>

                                            <td colspan="2">
                                                <asp:TextBox ID="TxtMM_Male" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                    CssClass="form-control" runat="server"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTxtMM_Male" TargetControlID="TxtMM_Male"
                                                    ValidChars="0123456789" runat="server" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Present OOSC/No. of parents of irregular childeren

                                            </td>

                                            <td colspan="2">
                                                <asp:TextBox ID="txtVillager2" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                    onkeypress="return isNumberKey(this,event);" CssClass="form-control" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Image </td>

                                            <td colspan="2">
                                                <asp:ImageButton ID="ImgMM" runat="server" Width="30" Height="25" OnClick="btnImgMM_Click"
                                                    Visible="false" CssClass="pull-right" ImageUrl="~/images/iconimage-128.png"></asp:ImageButton></td>
                                        </tr>



                                    </tbody>
                                </table>


                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" style="padding: 0px;">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                <span class="">
                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                        Other  Community Meeting 1
                                    </p>
                                </span>
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="Div2">
                            <div class="thumbnail" style="height: 875px; overflow: auto">
                                <asp:ImageButton ID="ImageBdeeeutton6" runat="server" OnClick="btnOther_Click" Width="30"
                                    Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                <table class="table table table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td style="width: 150px;">
                                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                    <asp:CheckBox Enabled="false" ID="chkothercomm" runat="server" />
                                                    Other Community Meeting 1
                                                </p>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="rblothercommTb" AutoPostBack="true" OnCheckedChanged="rblothercom_Click" GroupName="D" runat="server" />

                                                TB
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="rblothercommfc" AutoPostBack="true" OnCheckedChanged="rblothercom_Click" GroupName="D" runat="server" />
                                                FC
                                            </td>
                                        </tr>
                                        </tr>

                                                   <tr runat="server" id="tr1" visible="false">
                                                       <td style="width: 150px;">
                                                           <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                               TB Name
                                                           </p>
                                                       </td>
                                                       <td colspan="2" style="padding        -left: 33px;">
                                                           <asp:DropDownList runat="server" CssClass="form-control" ID="ddltbCom1"></asp:DropDownList>
                                                       </td>

                                                   </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="rdEnrollment2" Text="Enrollment" AutoPostBack="true" OnCheckedChanged="rblothem_Click" GroupName="rdEnRe1" runat="server" />
                                            </td>
                                            <td colspan="1">
                                                <asp:RadioButton ID="rdRetantion2" Text="Retention" AutoPostBack="true" OnCheckedChanged="rblothem_Click" GroupName="rdEnRe1" runat="server" />
                                            </td>
                                             <td colspan="1">
                                                <asp:RadioButton ID="rpSocialMapping" Text="Social Mapping" AutoPostBack="true" OnCheckedChanged="rblothem_Click" GroupName="rdEnRe1" runat="server" />
                                            </td>

                                        </tr>
                                        <tr>

                                            <td>Meeting Name
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="tc1" autocomplete="off" ondrop="return false;" MaxLength="50" CssClass="form-control"
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td colspan="3">
                                                <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                    ID="Label11" Text="Objective of Meeting" runat="server"></asp:Label>

                                                <asp:CheckBoxList ID="chk_othercom" CssClass="chkBoxList _bookformat2" onclick="SetMultilanguage('OC','_bookformat2');" runat="server" RepeatColumns="2" RepeatDirection="Vertical"></asp:CheckBoxList>

                                                <asp:TextBox ID="txtOtherComminuty" Visible="false" autocomplete="off" ondrop="return false;" runat="server"
                                                    CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>

                                                <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="txtOtherComminuty"
                                                    PopupControlID="pnt_OtherComm" OffsetY="22">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="pnt_OtherComm" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 35%"
                                                    CssClass="panel">
                                                    <span>
                                                        <asp:CheckBoxList ID="chk_othercom88" CssClass="_bookformat2 radio" runat="server"
                                                            onclick="SetMultilanguage('OC','_bookformat2');">
                                                        </asp:CheckBoxList>
                                                    </span>
                                                    <asp:HiddenField runat="server" ID="hhOtherComid" />
                                                    <asp:HiddenField runat="server" ID="hhOtherComName" />
                                                </asp:Panel>
                                                <asp:TextBox runat="server" ID="txtOtherComm" autocomplete="off" ondrop="return false;"
                                                    CssClass="form-control" MaxLength="30" TabIndex="18"></asp:TextBox>
                                            </td>
                                        </tr>



                                        <tr>

                                            <td colspan="3">
                                                <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                    ID="Label2" Text="Highlights of Discussion" runat="server"></asp:Label>

                                                <asp:CheckBoxList ID="chk_othercom_New" runat="server" CssClass="_bookformat11" onclick="SetMultilanguage('OCN','_bookformat11');" RepeatColumns="2" RepeatDirection="Vertical"></asp:CheckBoxList>

                                                <asp:TextBox ID="txtOtherComminutyNew" Visible="false" autocomplete="off" ondrop="return false;"
                                                    runat="server" CssClass="form-control col-sm-1" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                <cc1:PopupControlExtender ID="PopupControlExtender7" runat="server" TargetControlID="txtOtherComminutyNew"
                                                    PopupControlID="pnt_txtOtherComminutyNew" OffsetY="22">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="pnt_txtOtherComminutyNew" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 45%"
                                                    CssClass="panel">
                                                    <span>
                                                        <asp:CheckBoxList ID="chk_othercom_New88" CssClass="_bookformat11 radio" runat="server"
                                                            onclick="SetMultilanguage('OCN','_bookformat11');">
                                                        </asp:CheckBoxList>
                                                    </span>
                                                    <asp:HiddenField runat="server" ID="hdnOtherComNew_ID" />
                                                    <asp:HiddenField runat="server" ID="hdnOtherComNew_Name" />
                                                </asp:Panel>
                                                <asp:TextBox runat="server" ID="txtOtherComm1" CssClass="form-control" MaxLength="30"
                                                    TabIndex="18"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>


                                            <td colspan="3">
                                                <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                    ID="Label12" Text="Key Participants" runat="server"></asp:Label>

                                                <asp:CheckBoxList ID="chk_othercom_New1" runat="server" RepeatColumns="2" CssClass="chkBoxList" RepeatDirection="Vertical"></asp:CheckBoxList>

                                                <asp:TextBox ID="txtOtherComminutyNew1" Visible="false" autocomplete="off" ondrop="return false;"
                                                    runat="server" CssClass="form-control col-sm-1" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                <cc1:PopupControlExtender ID="PopupControltxtOtherComminutyNew1" runat="server" TargetControlID="txtOtherComminutyNew1"
                                                    PopupControlID="pnt_txtOtherComminutyNew1" OffsetY="22">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="pnt_txtOtherComminutyNew1" runat="server" Direction="LeftToRight"
                                                    Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 50%"
                                                    CssClass="panel">
                                                    <span>
                                                        <asp:CheckBoxList ID="chk_othercom_New199" CssClass="_btxtOtherComminutyNew1 radio"
                                                            runat="server" onclick="SetMultilanguage('OCN1','_btxtOtherComminutyNew1');">
                                                        </asp:CheckBoxList>
                                                    </span>
                                                    <asp:HiddenField runat="server" ID="hdn_ID_txtOtherComminutyNew1" />
                                                    <asp:HiddenField runat="server" ID="hdn_Name_txtOtherComminutyNew1" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Attendance-Female
                                            </td>

                                            <td colspan="2">
                                                <asp:TextBox ID="TxtCm1_FeMale" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                    CssClass="form-control" runat="server"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTxtCm1_FeMale" TargetControlID="TxtCm1_FeMale"
                                                    ValidChars="0123456789" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Attendance-Male
                                            </td>

                                            <td colspan="2">
                                                <asp:TextBox ID="TxtCm1_Male" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                    CssClass="form-control" runat="server"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTxtCm1_Male" TargetControlID="TxtCm1_Male"
                                                    ValidChars="0123456789" runat="server" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Present OOSC/No. of parents of irregular childeren

                                            </td>

                                            <td colspan="2">
                                                <asp:TextBox ID="txtvillager3" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                    onkeypress="return isNumberKey(this,event);" CssClass="form-control" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Image       </td>

                                            <td colspan="2">
                                                <asp:ImageButton ID="imgComm1" runat="server" Width="30" Height="25" OnClick="btnimgComm1_Click"
                                                    Visible="false" CssClass="pull-right" ImageUrl="~/images/iconimage-128.png"></asp:ImageButton></td>
                                        </tr>
                                    </tbody>
                                </table>


                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlMain11" runat="server" Enabled="false">
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                <span class="">
                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                        Other Community Meeting 2
                                    </p>
                                </span>
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="Div3" style="padding: 0px;">
                            <div class="thumbnail" style="height: 455px; overflow: auto">
                                <asp:ImageButton ID="ImageddssfButton3" runat="server" OnClick="btnOtherss1_Click" Width="30"
                                    Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                <table class="table table table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td style="width: 150px;">
                                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                    <asp:CheckBox ID="CheckBox1" Enabled="false" runat="server" />
                                                    Other Community Meeting 2
                                                </p>
                                            </td>
                                            <td style="padding        -left: 33px;">
                                                <asp:RadioButton ID="rblc1" AutoPostBack="true" OnCheckedChanged="rblothercom2_Click" GroupName="c1" runat="server" />

                                                TB
                                            </td>
                                            <td style="padding        -left: 33px;">
                                                <asp:RadioButton ID="rblc2" AutoPostBack="true" OnCheckedChanged="rblothercom2_Click" GroupName="c1" runat="server" />
                                                FC
                                            </td>
                                        </tr>
                                        </tr>

                                                   <tr runat="server" id="tr2" visible="false">
                                                       <td style="width: 150px;">
                                                           <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                               TB Name
                                                           </p>
                                                       </td>
                                                       <td colspan="2" style="padding        -left: 33px;">
                                                           <asp:DropDownList runat="server" CssClass="form-control" ID="ddltbCom2"></asp:DropDownList>
                                                       </td>

                                                   </tr>
                                        <tr>
                                            <td>Meeting Name
                                            </td>

                                            <td colspan="2">
                                                <asp:TextBox ID="txtoC1" autocomplete="off" ondrop="return false;" MaxLength="50"
                                                    CssClass="form-control" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>


                                            <td colspan="3">
                                                <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                    ID="Label13" Text=" Objective of Meeting" runat="server"></asp:Label>

                                                <asp:CheckBoxList ID="chk_c2" CssClass="chkBoxList _bookformat11" onclick="SetMultilanguage('CC2','_bookformat11');" runat="server" RepeatColumns="2" RepeatDirection="Vertical"></asp:CheckBoxList>

                                                <asp:TextBox ID="txtOtherCC1" Visible="false" autocomplete="off" ondrop="return false;" runat="server"
                                                    CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOtherComminuty"
                                        Display="Dynamic" ErrorMessage="*"  txtOtherCC1  hc1  hc2 txtoC111
                                        >*</asp:RequiredFieldValidator>--%>
                                                <cc1:PopupControlExtender ID="PopupControlExtender6" runat="server" TargetControlID="txtOtherCC1"
                                                    PopupControlID="pnt_CommC1" OffsetY="22">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="pnt_CommC1" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 40%"
                                                    CssClass="panel">
                                                    <span>
                                                        <asp:CheckBoxList ID="chk_c2444" CssClass="_bookformat11 radio" runat="server" onclick="SetMultilanguage('CC2','_bookformat11');">
                                                        </asp:CheckBoxList>
                                                    </span>
                                                    <asp:HiddenField runat="server" ID="hc1" />
                                                    <asp:HiddenField runat="server" ID="hc2" />
                                                </asp:Panel>
                                                <asp:TextBox runat="server" ID="txtoC111" autocomplete="off" ondrop="return false;"
                                                    CssClass="form-control" MaxLength="60" TabIndex="18"></asp:TextBox>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td>People Attended

                                            </td>

                                            <td colspan="2">
                                                <asp:TextBox ID="txtAtt1" autocomplete="off" ondrop="return false;" MaxLength="3"
                                                    onkeypress="return isNumberKey(this,event);" CssClass="form-control" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Image
                                            </td>
                                            <td colspan="2">
                                                <asp:ImageButton ID="imgComm2" runat="server" Width="30" Height="25" OnClick="btnimgComm2_Click"
                                                    Visible="false" CssClass="pull-right" ImageUrl="~/images/iconimage-128.png"></asp:ImageButton>
                                            </td>
                                        </tr>



                                    </tbody>
                                </table>


                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" style="padding: 0px;">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                <span class="">
                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                        Community Contact
                                    </p>
                                </span>
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="Div5" style="padding: 0px;">
                            <div class="thumbnail" style="height: 455px; overflow: auto">
                                <asp:ImageButton ID="ImageBudsdfsddtton5" runat="server" OnClick="btnOthe88r_Click" Width="30"
                                    Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                <table class="table table table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td style="width: 150px;">
                                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                    <asp:CheckBox ID="chkcoom" Enabled="false" runat="server" />
                                                    Community Contact
                                                </p>
                                            </td>
                                            <td style="padding        -left: 33px;">
                                                <asp:RadioButton ID="rblcommtb" Visible="false" GroupName="c11" runat="server" />


                                            </td>
                                            <td style="padding        -left: 33px;">
                                                <asp:RadioButton ID="rblCommFC" GroupName="c11" runat="server" />
                                                FC
                                            </td>
                                        </tr>


                                        <tr>

                                            <td colspan="3">
                                                <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                    ID="Label3" Text="Reason" runat="server"></asp:Label>

                                                <asp:CheckBoxList ID="chk_comm" CssClass="chkBoxList _bookformat3" onclick="SetMultilanguage('CC','_bookformat3');" runat="server" RepeatColumns="2" RepeatDirection="Vertical"></asp:CheckBoxList>

                                                <asp:TextBox ID="txtOtherConnect" Visible="false" autocomplete="off" ondrop="return false;" runat="server"
                                                    CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" TargetControlID="txtOtherConnect"
                                                    PopupControlID="pnt_CommConnect" OffsetY="22">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="pnt_CommConnect" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 40%"
                                                    CssClass="panel">
                                                    <span>
                                                        <asp:CheckBoxList ID="chk_com555m" CssClass="_bookformat3 radio" runat="server" onclick="SetMultilanguage('CC','_bookformat3');">
                                                        </asp:CheckBoxList>
                                                    </span>
                                                    <asp:HiddenField runat="server" ID="hhcommconectId" />
                                                    <asp:HiddenField runat="server" ID="hhcommconectName" />
                                                </asp:Panel>
                                                <asp:TextBox runat="server" autocomplete="off" ondrop="return false;" ID="txtOtherCon"
                                                    CssClass="form-control" MaxLength="60" TabIndex="18"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>

                                            <td colspan="3">
                                                <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                    ID="Label4" Text="Community Contact" runat="server"></asp:Label>

                                                <asp:CheckBoxList ID="chk_chkconn" CssClass="chkBoxList _bookformat4" runat="server" onclick="SetMultilanguage('CCC','_bookformat4');" RepeatColumns="2" RepeatDirection="Vertical"></asp:CheckBoxList>

                                                <asp:TextBox ID="txt_conn" Visible="false" autocomplete="off" ondrop="return false;" runat="server"
                                                    CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>

                                                <cc1:PopupControlExtender ID="PopupControlExtender4" runat="server" TargetControlID="txt_conn"
                                                    PopupControlID="pnt_Com" OffsetY="22">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="pnt_Com" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 40%"
                                                    CssClass="panel">
                                                    <span>
                                                        <asp:CheckBoxList ID="chk_ctthkconn" CssClass="_bookformat4 radio" runat="server" onclick="SetMultilanguage('CCC','_bookformat4');">
                                                        </asp:CheckBoxList>
                                                    </span>
                                                    <asp:HiddenField runat="server" ID="hh_connID" />
                                                    <asp:HiddenField runat="server" ID="hh_connName" />
                                                </asp:Panel>
                                                <asp:TextBox runat="server" autocomplete="off" ondrop="return false;" ID="txt_con_other"
                                                    CssClass="form-control" MaxLength="60" TabIndex="18"></asp:TextBox>
                                            </td>
                                        </tr>




                                    </tbody>
                                </table>


                            </div>
                        </div>
                    </div>

                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle new-navbutt" data-toggle="collapse" data-target="#myNavbar1">
                            Enrolled/Ineligible
                        </button>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="Hdn_model3"
                            PopupControlID="pnlpopup3" CancelControlID="btnAdd" BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>
                        <asp:HiddenField ID="Hdn_model3" runat="server" />
                        <asp:Panel ID="pnlpopup3" runat="server" Style="display: none;">
                            <div class=" modal-lg">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                            ToolTip="Add" ImageUrl="~/images/close-29.png" Style="margin-    right: 5px; padding: 0px;"
                                            runat="server" />
                                        <asp:ImageButton ID="ImageButton10" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                            ToolTip="Save" ImageUrl="~/images/save-29-1.png" Visible="false" Style="margin-    right: 5px; padding: 0px;"
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
                                                    <asp:ImageButton ID="ImageButton7" OnClick="btnD2dSerach_Click" ToolTip="Serach"
                                                        runat="server" class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1"
                                                        ImageUrl="~/images/search-29.png" Style="margin-    left: -49px; padding: 0px;" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row table-responsive">
                                        <div style="overflo        w: auto; margin-top: 35px; height: 380px;">
                                            <asp:GridView ID="Gv_Display" Width="100%" runat="server" OnRowDataBound="Gv_Display_RowDataBound"
                                                CssClass=" table table-striped table-bordered table-hover " AutoGenerateColumns="false">
                                                <EmptyDataTemplate>
                                                    <div style="font-fa        mily: Arial; font-size: 12px; font-weight: bold; color: Red;">
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
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HH No." HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text='<%#Eval("HHNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Child Name" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbtn_ProducerD" Text='<%#Eval("ChildName") %>' Style="text-de        coration: none;"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Father name" DataField="FathersName" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Contact" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlStatus" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                                                                AutoPostBack="true" runat="server" class="form-control">
                                                                <%-- <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">C-Contact </asp:ListItem>
                                              <asp:ListItem Value="2">F-Follow up</asp:ListItem>
                                            <asp:ListItem Value="3">I-Ineligible </asp:ListItem>
                                            <asp:ListItem Value="4">P-Pending Format 6</asp:ListItem>

                                            <asp:ListItem Value="5">E-Enrolled</asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                            <asp:Label runat="server" Visible="false" ID="lbStatus" Text='<%#Eval("Status") %>'
                                                                Style="text-de        coration: none;"></asp:Label>
                                                            <asp:Label runat="server" Visible="false" ID="lbStatusNew" Text='<%#Eval("Status") %>'
                                                                Style="text-de        coration: none;"></asp:Label>
                                                            <asp:Label runat="server" Visible="false" ID="lblActivityDate" Text='<%#Eval("ActivityDate") %>'
                                                                Style="text-de        coration: none;"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TBorFC" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:RadioButtonList ID="rblTBFC" OnSelectedIndexChanged="btnrblTBFC_Click" AutoPostBack="true" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True" Value="2">FC</asp:ListItem>
                                                                <asp:ListItem Value="1">TB</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="TBName" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlContactTb" Enabled="false"
                                                                runat="server" class="form-control">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEditEnroll" ToolTip="Edit" OnClick="btnEditEnroll_Click"
                                                                runat="server" class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1"
                                                                ImageUrl="~/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbUniqueCode" Text='<%#Eval("UniqueCode") %>' Style="text-de        coration: none;"></asp:Label>
                                                            <asp:Label runat="server" ID="lblTBFC" Text='<%#Eval("TBorFC") %>' Style="text-de        coration: none;"></asp:Label>
                                                            <asp:Label runat="server" ID="lblDtdUniqid" Text='<%#Eval("GUIDDTDActivityID") %>'
                                                                Style="text-de        coration: none;"></asp:Label>
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
                        <cc1:ModalPopupExtender ID="MpexdrFollowup" runat="server" BackgroundCssClass="modalBg "
                            CancelControlID="ImageButton6" PopupControlID="PnlFollowup" TargetControlID="HdnFollowup">
                        </cc1:ModalPopupExtender>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Hdn_model33"
                            PopupControlID="pnlpopup43" CancelControlID="btnAddNew" BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>
                        <asp:HiddenField ID="Hdn_model33" runat="server" />
                        <asp:Panel ID="pnlpopup43" runat="server" Style="display: none;">
                            <div class=" modal-lg">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <asp:ImageButton ID="btnAddNew" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                            ToolTip="Add" ImageUrl="~/images/close-29.png" Style="margin-    right: 5px; padding: 0px;"
                                            runat="server" />
                                        <h4 class="modal-title">D2D</h4>
                                    </div>
                                    <div class="row">
                                        <div class="row marg search-bg">
                                            <div id="Div7" class="col-lg-12 col-md-12 col-sm-12 cpl-xs-12" runat="server">
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group" style="margin-bottom: 2px;">
                                                        <label for="email" class="col-sm-3 padd linhei">
                                                            Date:</label>
                                                        <div class="col-sm-9 padd">
                                                            <asp:TextBox runat="server" ID="txtFdate" autocomplete="off" ondrop="return false;"
                                                                class="form-control" onkeypress="return false;"></asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="txtFdate" PopupPosition="BottomRight">
                                                            </ajax:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group" style="margin-bottom: 7px;">
                                                        <div class="col-sm-10 padd">
                                                            <asp:TextBox runat="server" ID="TxtToDate" autocomplete="off" ondrop="return false;"
                                                                class="form-control" onkeypress="return false;"></asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="TxtToDate" PopupPosition="BottomRight">
                                                            </ajax:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group" style="margin-bottom: 7px;">
                                                        <div class="col-sm-10 padd">
                                                            <asp:DropDownList ID="ddlSearchEnroll" OnSelectedIndexChanged="ddlSearchEnroll_SelectedIndexChanged"
                                                                AutoPostBack="true" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Follow Up</asp:ListItem>
                                                                <asp:ListItem Value="2">Ineligible </asp:ListItem>

                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group" style="margin-bottom: 7px;">
                                                        <div class="col-sm-10 padd">
                                                            <asp:DropDownList ID="ddlSubContact" OnSelectedIndexChanged="ddlSubContact_SelectedIndexChanged"
                                                                AutoPostBack="true" runat="server" class="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12 cpl-xs-12">
                                                <div class="col-lg-4 col-md-4 col-sm-4 cpl-xs-12">
                                                    <div class="form-group" style="margin-bottom: 2px;">
                                                        <label for="email" class="col-sm-3 padd linhei">
                                                            Search:</label>
                                                        <div class="col-sm-9 padd">
                                                            <asp:DropDownList ID="ddlStatusSearch" runat="server" class="form-control">
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
                                                            <asp:TextBox runat="server" ID="txtSearchNew" autocomplete="off" ondrop="return false;"
                                                                class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4 col-md-4  col-sm-4 cpl-xs-12">
                                                    <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                                        <asp:ImageButton ID="ImageButton9" OnClick="btnD2dSerachNew_Click" ToolTip="Serach"
                                                            runat="server" class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1"
                                                            ImageUrl="~/images/search-29.png" Style="margin-    left: -49px; padding: 0px;" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row table-responsive">
                                        <div style="overflo        w: auto; margin-top: 35px; height: 380px;">
                                            <asp:GridView ID="Gv_DisplayNew" Width="100%" runat="server" OnRowDataBound="Gv_DisplayNew_RowDataBound"
                                                CssClass=" table table-striped table-bordered table-hover " AutoGenerateColumns="false">
                                                <EmptyDataTemplate>
                                                    <div style="font-fa        mily: Arial; font-size: 12px; font-weight: bold; color: Red;">
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
                                                            <asp:Label ID="lblUniqueCode1" runat="server" Text='<%#Eval("UniqueId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HH No." HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno11" runat="server" Text='<%#Eval("HHNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Child Name" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbtn_ProducerD" Text='<%#Eval("ChildName") %>' Style="text-de        coration: none;"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Father name" DataField="FathersName" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbStatusNew" Text='<%#Eval("Status") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Undo" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnEditUndo" ToolTip="Undo" Text="Undo" OnClick="btnEditUndo_Click"
                                                                runat="server" class="btn btn-danger btn-paddd pull-right" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbUniqueCode11" Text='<%#Eval("UniqueCode") %>' Style="text-de        coration: none;"></asp:Label>
                                                            <asp:Label runat="server" ID="lblGUIDDTDMobileActivity" Text='<%#Eval("GUIDDTDMobileActivity") %>'
                                                                Style="text-de        coration: none;"></asp:Label>
                                                            <asp:Label runat="server" ID="Label40" Text='<%#Eval("FollowUPID") %>' Style="text-de        coration: none;"></asp:Label>
                                                            <asp:Label runat="server" ID="lblActivityStatus1" Text='<%#Eval("ActivityStatus") %>'
                                                                Style="text-de        coration: none;"></asp:Label>
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
                        <asp:HiddenField ID="HdnFollowup" runat="server"></asp:HiddenField>
                        <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important; margin-top: -75.5px !important;"
                            ID="PnlFollowup" runat="server">
                            <div style="width: 100%; height: 650px; overflow: auto; background-color: #f1f1f1">
                                <div class="modal-header" style="backgro        und-color: #ddd; color: White;">
                                    <h4 class="modal-title" style="forecol        or: White">
                                        <asp:Label ID="lblStst" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label></h4>
                                    <asp:Label ID="lblEditActivtive" Visible="false" runat="server" ForeColor="Red" Font-Names="Verdana"
                                        Font-Size="11px"></asp:Label>
                                    <asp:Label ID="lblEditRow" Visible="false" runat="server" ForeColor="Red" Font-Names="Verdana"
                                        Font-Size="11px"></asp:Label>
                                    <asp:Label ID="lblGuID" Visible="false" runat="server" ForeColor="Red" Font-Names="Verdana"
                                        Font-Size="11px"></asp:Label>
                                    <asp:Label ID="lblEnrollId" Visible="false" runat="server" ForeColor="Red" Font-Names="Verdana"
                                        Font-Size="11px"></asp:Label>
                                    <asp:Label ID="lblRtbFc" runat="server" Visible="false" ForeColor="Red" Font-Names="Verdana"
                                        Font-Size="11px"></asp:Label>
                                    <asp:Label ID="lblD2dUniqeCode" Visible="false" runat="server" ForeColor="Red" Font-Names="Verdana"
                                        Font-Size="11px"></asp:Label>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="Label19" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                    <div class="form-horizontal" role="form">

                                        <div id="dvidFollowp" runat="server">
                                            <div class="form-group" id="div19" runat="server">
                                                <asp:Label ID="Label52" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Relationship of Respondent  "></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlRelationFo"
                                                        runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" id="div10" runat="server">
                                                <asp:Label ID="lblFoabali" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Availability  "></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlAvaiFO" OnSelectedIndexChanged="ddlAvaiFO_SelectedIndexChanged"
                                                        AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                        <asp:ListItem Value="3">Yes but not available</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group" id="divF1" runat="server" visible="false">
                                                <asp:Label ID="Label20" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Govt. ID"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" ID="txtGovtID" autocomplete="off" onkeypress="return onlyAlphabets(event,this);"
                                                        ondrop="return false;" class="form-control" MaxLength="15"></asp:TextBox>
                                                </div>
                                            </div>


                                            <div class="form-group" id="divF2" runat="server" visible="false">
                                                <asp:Label ID="Label21" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Samgra ID"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" MaxLength="9" onkeypress="return isNumberKey(this,event);"
                                                        ID="txtSamgraID" autocomplete="off" ondrop="return false;" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" id="div11" runat="server">
                                                <asp:Label ID="Label14" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Consent for Mobile No"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlAvialMobile" OnSelectedIndexChanged="ddlAvialMobile_SelectedIndexChanged"
                                                        AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group" id="divMobile" runat="server" visible="false">
                                                <asp:Label ID="Label15" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Mobile No"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" MaxLength="10" onkeypress="return isNumberKey(this,event);"
                                                        ID="txtMobileFO" autocomplete="off" ondrop="return false;" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group" id="div25" runat="server" visible="false">
                                                <asp:Label ID="Label59" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Mobile No. Conformation"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" MaxLength="10" onkeypress="return isNumberKey(this,event);"
                                                        ID="txtCMobileFO" autocomplete="off" ondrop="return false;" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group" id="divResonFo" runat="server">
                                                <asp:Label ID="Label49" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Reason for not sharing mobile number"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlResoneMobileFo"
                                                        runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>


                                            <div class="form-group" id="divFoAlter" runat="server">
                                                <asp:Label ID="Label68" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Alternate Mobile Number"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" MaxLength="10" onkeypress="return isNumberKey(this,event);"
                                                        ID="txtFoAlternateMobile" autocomplete="off" ondrop="return false;" class="form-control"
                                                        OnTextChanged="txtatalter_mobile" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" id="divFoOwner" runat="server" visible="false">
                                                <asp:Label ID="Label69" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text=" Alternate mobile Owner Name"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" ID="txtFoOwnerRelationChild" autocomplete="off" onkeypress="return onlyAlphabets(event,this);"
                                                        ondrop="return false;" class="form-control" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" id="divForRelation" runat="server" visible="false">
                                                <asp:Label ID="Label70" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text=" Relation with Child"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" ID="txtRelation" autocomplete="off" onkeypress="return onlyAlphabets(event,this);"
                                                        ondrop="return false;" class="form-control" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>




                                            <div class="form-group" id="divF3" runat="server">
                                                <asp:Label ID="Label22" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Reasons"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlFo" OnSelectedIndexChanged="ddlFo_SelectedIndexChanged"
                                                        AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>


                                            <div class="form-group" runat="server" id="divF4">
                                                <asp:Label ID="Label23" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Village"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlOtherVillage" OnSelectedIndexChanged="ddlFOtherVillage_SelectedIndexChanged"
                                                        AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Same village </asp:ListItem>
                                                        <asp:ListItem Value="2">Other village</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" id="divF7">
                                                <asp:Label ID="Label36" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="School"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlSchool" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" id="divF5">
                                                <asp:Label ID="lbldist" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Other Village"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtOtherVillage" autocomplete="off" ondrop="return false;" onkeypress="return onlyAlphabets(event,this);"
                                                        CssClass="form-control" runat="server" MaxLength="50" BorderStyle="None"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" id="divF6">
                                                <asp:Label ID="Label35" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="School"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtOtherSchool" autocomplete="off" ondrop="return false;" onkeypress="return onlyAlphabets(event,this);"
                                                        CssClass="form-control" runat="server" MaxLength="50" BorderStyle="None"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" runat="server" id="divF18">
                                                <asp:Label ID="Label48" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Other Reason detail"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtOtherResone" autocomplete="off" ondrop="return false;" onkeypress="return onlyAlphabets(event,this);"
                                                        CssClass="form-control" runat="server" MaxLength="50" BorderStyle="None"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" runat="server" id="div21">
                                                <asp:Label ID="Label55" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Implementer"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlImplementerFo" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" id="div22">
                                                <asp:Label ID="Label56" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Joint Visit"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlJoinFo" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group" id="dirrvhh33" runat="server">
                                                <asp:Label ID="Label71" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Ready for CBL"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:RadioButtonList ID="rblFoCBL" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Value="2">No</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                            <div class="form-group" runat="server" id="dfiv22">
                                                <asp:Label ID="Label57" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Awareness during D2D Contact"></asp:Label>
                                                <div class="col-sm-6">


                                                    <asp:TextBox ID="txtAwarenessFo" runat="server" autocomplete="off" ondrop="return false;"
                                                        CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                    <ajax:PopupControlExtender ID="PopupControlExtender9" runat="server" TargetControlID="txtAwarenessFo"
                                                        PopupControlID="pnt_Fo" OffsetY="22">
                                                    </ajax:PopupControlExtender>
                                                    <asp:Panel ID="pnt_Fo" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 100%"
                                                        CssClass="panel">
                                                        <span>
                                                            <asp:CheckBoxList ID="chkAwarenessFo" OnTextChanged="txtState_TextChanged" AutoPostBack="true" CssClass="_bookformatttt radio" runat="server"
                                                                onclick="SetMultilanguageNew('FO','_bookformatttt');">
                                                            </asp:CheckBoxList>
                                                        </span>
                                                        <asp:HiddenField runat="server" ID="hdn_AwarenessFo" />
                                                        <asp:HiddenField runat="server" ID="hdn_PBAwarenessFo" />
                                                    </asp:Panel>



                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" id="div24" visible="false">
                                                <asp:Label ID="Label58" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Enrolment Category"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlEnrolmentCategory" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group" runat="server" id="div331" visible="false">
                                                <asp:Label ID="Label79" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Enrolment Session"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" id="div332" visible="false">
                                                <asp:Label ID="Label80" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Document Availability for Enrolment"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlDocAva" OnSelectedIndexChanged="ddlDocAva_SelectedIndexChanged"
                                                        AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes </asp:ListItem>
                                                        <asp:ListItem Value="2">No </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" id="Div333">
                                                <asp:Label ID="Label81" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="A list of pending document "></asp:Label>
                                                <div class="col-sm-6">


                                                    <asp:TextBox ID="txtBanding" runat="server" autocomplete="off" ondrop="return false;"
                                                        CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                    <ajax:PopupControlExtender ID="PopupControlExtender11" runat="server" TargetControlID="txtBanding"
                                                        PopupControlID="pnt_Fo55" OffsetY="22">
                                                    </ajax:PopupControlExtender>
                                                    <asp:Panel ID="pnt_Fo55" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 100%"
                                                        CssClass="panel">
                                                        <span>
                                                            <asp:CheckBoxList ID="chktBanding" CssClass="_bookBanding radio" runat="server"
                                                                onclick="SetMultilanguageNew('KO','_bookBanding');">
                                                            </asp:CheckBoxList>
                                                        </span>
                                                        <asp:HiddenField runat="server" ID="HiddenField1" />
                                                        <asp:HiddenField runat="server" ID="HiddenField2" />
                                                    </asp:Panel>



                                                </div>
                                            </div>

                                        </div>
                                        <div id="dvIngilible" runat="server">
                                            <div class="form-group" id="div18" runat="server">
                                                <asp:Label ID="Label53" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Relationship of Respondent  "></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlRelationIN"
                                                        runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div id="DivI2" class="form-group" runat="server">
                                                <asp:Label ID="Label24" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Reasons"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlIReasons" OnSelectedIndexChanged="ddlIReasons_SelectedIndexChanged"
                                                        AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" id="div12" runat="server">
                                                <asp:Label ID="lblAvialIO" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Availability  "></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlAvilIO" OnSelectedIndexChanged="ddlAvilIO_SelectedIndexChanged"
                                                        AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                        <asp:ListItem Value="3">Yes but not available</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group" id="DivI10" runat="server" visible="false">
                                                <asp:Label ID="Label46" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Govt. ID"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" ID="txtIGovtID" autocomplete="off" onkeypress="return onlyAlphabets(event,this);"
                                                        ondrop="return false;" class="form-control" MaxLength="15"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group" id="DivI11" runat="server" visible="false">
                                                <asp:Label ID="Label47" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Samgra ID"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" MaxLength="9" onkeypress="return isNumberKey(this,event);"
                                                        ID="txtSamgra" autocomplete="off" ondrop="return false;" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" id="div13" runat="server">
                                                <asp:Label ID="Label6" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Consent for Mobile No"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlAvialMobileIO" OnSelectedIndexChanged="ddlAvialMobileIO_SelectedIndexChanged"
                                                        AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group" id="div14" runat="server" visible="false">
                                                <asp:Label ID="Label16" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Mobile No"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" MaxLength="10" onkeypress="return isNumberKey(this,event);"
                                                        ID="txtMobileIO" autocomplete="off" ondrop="return false;" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group" id="disv23" runat="server" visible="false">
                                                <asp:Label ID="Label60" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Mobile No. Conformation"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" MaxLength="10" onkeypress="return isNumberKey(this,event);"
                                                        ID="txtCMobileIO" autocomplete="off" ondrop="return false;" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" id="divResonIN" runat="server">
                                                <asp:Label ID="Label50" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Reason for not sharing mobile number"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlResoneMobileIN"
                                                        runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>





                                            <div id="DivI3" class="form-group" runat="server">
                                                <asp:Label ID="Label25" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Months"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group" id="divIoAlter" runat="server">
                                                <asp:Label ID="Label72" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Alternate Mobile Number"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" MaxLength="10" onkeypress="return isNumberKey(this,event);"
                                                        ID="txtIoAlternateMobile" autocomplete="off" ondrop="return false;" class="form-control"
                                                        OnTextChanged="txtatalterIO_mobile" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" id="divIoOwner" runat="server" visible="false">
                                                <asp:Label ID="Label73" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text=" Alternate mobile Owner Name"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" ID="txtIoOwnerRelationChild" autocomplete="off" onkeypress="return onlyAlphabets(event,this);"
                                                        ondrop="return false;" class="form-control" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" id="divIorRelation" runat="server" visible="false">
                                                <asp:Label ID="Label74" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text=" Relation with Child"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" ID="txtIORelation" autocomplete="off" onkeypress="return onlyAlphabets(event,this);"
                                                        ondrop="return false;" class="form-control" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>


                                            <div class="form-group" id="DivI4" runat="server">
                                                <asp:Label ID="Label26" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Birth Date:"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" ID="txtBDate" autocomplete="off" ondrop="return false;"
                                                        class="form-control" onkeypress="return false;"></asp:TextBox>
                                                    <ajax:CalendarExtender ID="CalendarExftender1" runat="server" OnClientDateSelectionChanged="arrivaldatecheckB"
                                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtBDate" PopupPosition="BottomRight">
                                                    </ajax:CalendarExtender>
                                                </div>
                                            </div>
                                            <div id="DivI5" class="form-group  clsD" runat="server">
                                                <asp:Label ID="Label27" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="DOB proof"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlDOproof" OnSelectedIndexChanged="ddlDOproof_SelectedIndexChanged"
                                                        AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div id="DivI6" class="form-group" runat="server">
                                                <asp:Label ID="Label39" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="DOB proof"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtOther" autocomplete="off" ondrop="return false;" onkeypress="return onlyAlphabets(event,this);"
                                                        CssClass="form-control" runat="server" MaxLength="50" BorderStyle="None"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div id="DivI7" class="form-group" runat="server">
                                                <asp:Label ID="Label43" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Migration Place"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlMigration" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>


                                            <div class="form-group" runat="server" id="div23">
                                                <asp:Label ID="Label61" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Implementer"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlImplementerIo" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" id="div26">
                                                <asp:Label ID="Label62" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Joint Visit"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlJoinIo" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>



                                            <div id="Div27" class="form-group" runat="server">
                                                <asp:Label ID="Label63" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Awareness during D2D Contact"></asp:Label>
                                                <div class="col-sm-6">


                                                    <asp:TextBox ID="txtAwarenessIo" runat="server" autocomplete="off" ondrop="return false;"
                                                        CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                    <ajax:PopupControlExtender ID="PopupControlExtender8" runat="server" TargetControlID="txtAwarenessIo"
                                                        PopupControlID="pnt_Io" OffsetY="22">
                                                    </ajax:PopupControlExtender>
                                                    <asp:Panel ID="pnt_Io" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 100%"
                                                        CssClass="panel">
                                                        <span>
                                                            <asp:CheckBoxList ID="chkAwarenessIo" OnTextChanged="txtState_TextChangedIo" AutoPostBack="true" CssClass="_bookformatIO radio" runat="server"
                                                                onclick="SetMultilanguageNew('IO','_bookformatIO');">
                                                            </asp:CheckBoxList>
                                                        </span>
                                                        <asp:HiddenField runat="server" ID="hdn_AwarenessIo" />
                                                        <asp:HiddenField runat="server" ID="hdn_PBAwarenessIo" />
                                                    </asp:Panel>



                                                </div>
                                            </div>
                                        </div>


                                        <div id="dvEnrollment" runat="server">
                                            <div class="form-group" id="div20" runat="server">
                                                <asp:Label ID="Label54" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Relationship of Respondent  "></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlRelationEN"
                                                        runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div id="DivE1" class="form-group" runat="server">
                                                <asp:Label ID="Label29" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Village"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlEotherVillage" runat="server" OnSelectedIndexChanged="ddlEFOtherVillage_SelectedIndexChanged"
                                                        AutoPostBack="true" CssClass="form-control" Font-Names="Verdana" Font-Size="11px">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Same village </asp:ListItem>
                                                        <asp:ListItem Value="2">Other village</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" id="DivE2">
                                                <asp:Label ID="Label28" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="School"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlESchool" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" id="DivE3">
                                                <asp:Label ID="Label37" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Other Village"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtEvillage" autocomplete="off" ondrop="return false;" onkeypress="return onlyAlphabets(event,this);"
                                                        CssClass="form-control" runat="server" MaxLength="50" BorderStyle="None"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" id="DivE4">
                                                <asp:Label ID="Label38" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="School"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtSschool" autocomplete="off" ondrop="return false;" onkeypress="return onlyAlphabets(event,this);"
                                                        CssClass="form-control" runat="server" MaxLength="50" BorderStyle="None"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div id="DivE5" class="form-group" runat="server">
                                                <asp:Label ID="Label30" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Panding Form Status"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlFromStatus" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" id="div15" runat="server">
                                                <asp:Label ID="lblAvailEO" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Availability  "></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlAvilEO" OnSelectedIndexChanged="ddlAvilEO_SelectedIndexChanged"
                                                        AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                        <asp:ListItem Value="3">Yes but not available</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" id="DivE12" runat="server" visible="false">
                                                <asp:Label ID="Label44" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Govt. ID"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" ID="txtEGovtID" autocomplete="off" onkeypress="return onlyAlphabets(event,this);"
                                                        ondrop="return false;" class="form-control" MaxLength="15"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group" id="DivE13" runat="server" visible="false">
                                                <asp:Label ID="Label45" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Samgra ID"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" MaxLength="9" onkeypress="return isNumberKey(this,event);"
                                                        ID="txtEsamgranID" autocomplete="off" ondrop="return false;" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group" id="div16" runat="server">
                                                <asp:Label ID="Label17" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Consent for Mobile No"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlAvialMobileEO" OnSelectedIndexChanged="ddlAvialMobileEO_SelectedIndexChanged"
                                                        AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>

                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group" id="div17" runat="server" visible="false">
                                                <asp:Label ID="Label18" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Mobile No"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" MaxLength="10" onkeypress="return isNumberKey(this,event);"
                                                        ID="txtMobileEO" autocomplete="off" ondrop="return false;" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group" id="Div31" runat="server" visible="false">
                                                <asp:Label ID="Label67" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Mobile No. Conformation"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" MaxLength="10" onkeypress="return isNumberKey(this,event);"
                                                        ID="txtCMobileEO" autocomplete="off" ondrop="return false;" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group" id="divResonEN" runat="server">
                                                <asp:Label ID="Label51" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Reason for not sharing mobile number"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlResoneMobileEN"
                                                        runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" id="divEnAlter" runat="server">
                                                <asp:Label ID="Label75" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Alternate Mobile Number"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" MaxLength="10" onkeypress="return isNumberKey(this,event);"
                                                        ID="txtEnAlternateMobile" autocomplete="off" ondrop="return false;" class="form-control"
                                                        OnTextChanged="txtatalterEn_mobile" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" id="divEnOwner" runat="server" visible="false">
                                                <asp:Label ID="Label76" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text=" Alternate mobile Owner Name"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" ID="txtEnOwnerRelationChild" autocomplete="off" onkeypress="return onlyAlphabets(event,this);"
                                                        ondrop="return false;" class="form-control" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" id="divEnrRelation" runat="server" visible="false">
                                                <asp:Label ID="Label77" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text=" Relation with Child"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" ID="txtEnRelation" autocomplete="off" onkeypress="return onlyAlphabets(event,this);"
                                                        ondrop="return false;" class="form-control" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div id="DivE6" class="form-group" runat="server" visible="false">
                                                <asp:Label ID="Label31" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Enrollment Category"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlCategory" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                                        AutoPostBack="true" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div id="DivE11" class="form-group" runat="server">
                                                <asp:Label ID="Label42" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Other"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtEnrommentOther" autocomplete="off" ondrop="return false;" onkeypress="return onlyAlphabets(event,this);"
                                                        CssClass="form-control" runat="server" MaxLength="50" BorderStyle="None"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div id="DivE7" class="form-group" runat="server">
                                                <asp:Label ID="Label32" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Scholar No"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtSchoolar" autocomplete="off" ondrop="return false;" onkeypress="return onlyAlphabets(event,this);"
                                                        CssClass="form-control" runat="server" MaxLength="5" BorderStyle="None"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div id="DivE8" class="form-group" runat="server">
                                                <asp:Label ID="Label33" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Class"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlClass" runat="server" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"
                                                        AutoPostBack="true" CssClass="form-control" Font-Names="Verdana" Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div id="DivE10" class="form-group" runat="server">
                                                <asp:Label ID="Label41" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Class Other"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtClassOther" autocomplete="off" ondrop="return false;" CssClass="form-control"
                                                        runat="server" MaxLength="50" BorderStyle="None"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div id="DivE9" class="form-group" runat="server">
                                                <asp:Label ID="Label34" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Enrollment Date"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtErollmentDate" autocomplete="off" ondrop="return false;" CssClass="form-control"
                                                        runat="server" MaxLength="30" BorderStyle="None"></asp:TextBox>
                                                    <ajax:CalendarExtender ID="CalendarEdxtender1" OnClientDateSelectionChanged="arrivaldatecheck"
                                                        runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtErollmentDate"
                                                        PopupPosition="BottomRight">
                                                    </ajax:CalendarExtender>
                                                </div>
                                            </div>

                                            <div class="form-group" runat="server" id="div28">
                                                <asp:Label ID="Label64" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Implementer"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlImplementerEo" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" id="div29">
                                                <asp:Label ID="Label65" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Joint Visit"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlJoinEo" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                        Font-Size="11px">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" id="Div32" runat="server">
                                                <asp:Label ID="Label78" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Ready for CBL"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:RadioButtonList ID="rblEnCBL" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Value="2">No</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div id="Div30" class="form-group" runat="server">
                                                <asp:Label ID="Label66" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Awareness during D2D Contact"></asp:Label>
                                                <div class="col-sm-6">


                                                    <asp:TextBox ID="txtAwarenessEo" runat="server" autocomplete="off" ondrop="return false;"
                                                        CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                    <ajax:PopupControlExtender ID="PopupControlExtender10" runat="server" TargetControlID="txtAwarenessEo"
                                                        PopupControlID="pnt_Eo" OffsetY="22">
                                                    </ajax:PopupControlExtender>
                                                    <asp:Panel ID="pnt_Eo" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 100%"
                                                        CssClass="panel">
                                                        <span>
                                                            <asp:CheckBoxList ID="chkAwarenessEo" OnTextChanged="txtStateEO_TextChanged" AutoPostBack="true" CssClass="_bookformatEO radio" runat="server"
                                                                onclick="SetMultilanguageNew('EO','_bookformatEO');">
                                                            </asp:CheckBoxList>
                                                        </span>
                                                        <asp:HiddenField runat="server" ID="hdn_AwarenessEo" />
                                                        <asp:HiddenField runat="server" ID="hdn_PBAwarenessEo" />
                                                    </asp:Panel>



                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:ImageButton ID="btnNewUserSave" OnClick="btnClose_Click" ImageUrl="~/images/save-29-1.png"
                                            runat="server" ToolTip="Save" Style="float: none;" ValidationGroup="validatemanageuser"></asp:ImageButton>&nbsp;
                                                <asp:ImageButton ID="ImageButton6" ImageUrl="~/images/close-29.png" runat="server"
                                                    Text="Close" ToolTip="Close" Style="float: none;"></asp:ImageButton>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" visible="false" runat="server" id="d2dContact">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                <span class="">
                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                        Contact/Enrolled/Ineligible
                                    </p>
                                </span>
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="Div6" style="padding: 0px;">
                            <div class="thumbnail" style="height: 80px; overflow: auto">
                                <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                    <asp:LinkButton Visible="false" ID="lnkEnrool" OnClick="lnkEnrool_OnClick" runat="server">Contact/Enrolled/Ineligible</asp:LinkButton>
                                                </p>
                                            </td>
                                            <td style="padding        -left: 33px;">
                                                <asp:RadioButton Visible="false" ID="rblemrolltb" GroupName="F" runat="server" />


                                            </td>
                                            <td style="padding        -left: 33px;">
                                                <asp:RadioButton ID="rblenrollFC" Visible="false" GroupName="F" runat="server" />

                                            </td>
                                        </tr>


                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="LinkButton1" OnClick="lnkListStaus_OnClick" runat="server">List Of Status</asp:LinkButton>

                                            </td>
                                            <td colspan="2"></td>
                                        </tr>




                                    </tbody>
                                </table>


                            </div>
                        </div>


                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel1" runat="server" Enabled="false">
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                <span class="">
                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                        Support
                                    </p>
                                </span>
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="Div8" style="padding: 0px;">
                            <div class="thumbnail" style="height: 190px; overflow: auto">
                                <table class="table table table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                    <asp:RadioButton ID="chkSupoort" Enabled="false" runat="server" />
                                                    Support
                                                </p>
                                            </td>
                                            <td style="padding        -left: 33px;">
                                                <asp:RadioButton ID="rblsupportfc" GroupName="F1" runat="server" />
                                                FC
                                            </td>
                                            <td style="padding        -left: 33px;">
                                                <asp:ImageButton ID="ImageBuddtton3" runat="server" OnClick="btnOthesrss1_Click" Width="30"
                                                    Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                            </td>
                                        </tr>


                                        <tr>

                                            <td colspan="3">
                                                <asp:Label class="text-center" Style="float: left; width: 100%; font-weight: bold; border-bottom: 1px solid #ddd;"
                                                    ID="Label5" Text="Support" runat="server"></asp:Label>

                                                <asp:CheckBoxList ID="chk_Suport" CssClass="chkBoxList _bookformat6" onclick="SetMultilanguage('SS','_bookformat6');" runat="server" RepeatColumns="2" RepeatDirection="Vertical"></asp:CheckBoxList>

                                                <asp:TextBox ID="txtSuport" Visible="false" autocomplete="off" ondrop="return false;" runat="server"
                                                    CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>

                                                <cc1:PopupControlExtender ID="PopupControlExtender5" runat="server" TargetControlID="txtSuport"
                                                    PopupControlID="pnt_Suport" OffsetY="22">
                                                </cc1:PopupControlExtender>
                                                <asp:Panel ID="pnt_Suport" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 40%"
                                                    CssClass="panel">
                                                    <span>
                                                        <asp:CheckBoxList ID="chk_Su44port" CssClass="_bookformat6 radio" runat="server" onclick="SetMultilanguage('SS','_bookformat6');">
                                                        </asp:CheckBoxList>
                                                    </span>
                                                    <asp:HiddenField runat="server" ID="hhSuportId" />
                                                    <asp:HiddenField runat="server" ID="hhSuportName" />
                                                </asp:Panel>
                                                <asp:TextBox runat="server" autocomplete="off" ondrop="return false;" ID="txtOtherSupport"
                                                    CssClass="form-control" MaxLength="60" TabIndex="18"></asp:TextBox>
                                            </td>
                                        </tr>


                                    </tbody>
                                </table>





                            </div>
                        </div>


                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                <span class="">
                                    <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                        Support
                                    </p>
                                </span>
                            </button>
                        </div>
                        <div class="collapse navbar-collapse" id="Div9" style="padding: 0px;">
                            <div class="thumbnail" style="height: 170px; overflow: auto">
                                <asp:ImageButton ID="ImageButeton3" runat="server" OnClick="btnOt44hesrss1_Click" Width="30"
                                    Height="25" CssClass="pull-right" ImageUrl="~/images/Reset.png"></asp:ImageButton>
                                <table class="table table-striped table-bordered table-hover">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                    <asp:RadioButton ID="chkother" Enabled="false" runat="server" />
                                                    Other - Specify
                                                </p>
                                            </td>
                                            <td style="padding        -left: 33px;">
                                                <asp:RadioButton ID="rblothertb" GroupName="H" runat="server" />

                                                TB
                                            </td>
                                            <td style="padding        -left: 33px;">
                                                <asp:RadioButton ID="rblotherfc" GroupName="H" runat="server" />
                                                FC
                                            </td>
                                        </tr>


                                        <tr>
                                            <td>Other - Specify
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtmainother" CssClass="form-control" autocomplete="off" ondrop="return false;"
                                                    Style="height: 53px; width: 100%;" TextMode="MultiLine" runat="server"></asp:TextBox>

                                            </td>
                                        </tr>


                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <cc1:ModalPopupExtender ID="MpexdrDistrict" runat="server" BackgroundCssClass="modalBg "
                    PopupControlID="PnlDistrict" CancelControlID="CancelButton" TargetControlID="HdnFild7">
                </cc1:ModalPopupExtender>
                <asp:HiddenField ID="HdnFild7" runat="server"></asp:HiddenField>
                <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important; margin-top: 125px !important;"
                    ID="PnlDistrict" runat="server">
                    <div style="width: 100%; height: auto; background-color: #f1f1f1">
                        <div class="modal-header" style="backgro        und-color: #3ac0f2; color: White;">
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
                                <asp:ImageButton ID="ImageButton8" CssClass="btn btn-info pull-right" OnClick="btnReset_Click"
                                    BackColor="#f5f5f5" ToolTip="Add" ImageUrl="~/images/close-29.png" Style="margin-    right: 5px; padding: 0px;"
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
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkEnrool" />

        </Triggers>


    </asp:UpdatePanel>

</asp:Content>
