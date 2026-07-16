<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FrmSealSignApproval.aspx.cs" Culture="en-GB" Inherits="FrmSealSignApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="js/jquery-2.1.0.js"></script>
    <script src="js/Common.js" type="text/javascript"></script>
    <script src="js/Zoom.js" type="text/javascript"></script>

    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
        }

        .zoom-area {
            width: 518px;
            margin: 50px auto;
            position: relative;
            cursor: none
        }
        /* for create magnify glass */
        .large {
            width: 175px;
            height: 175px;
            position: absolute;
            border-radius: 100%;
            /* for box shadow for glass effect */
            box-shadow: 0 0 0 7px rgba(255, 255, 255, 0.85), 0 0 7px 7px rgba(0, 0, 0, 0.25), inset 0 0 40px 2px rgba(0, 0, 0, 0.25);
            /*for hide the glass by default*/
            display: none;
        }

        .small {
            display: block;
        }
    </style>
    <style>
        .checkbox {
            padding-left: 20px;
        }

            .checkbox label {
                display: inline-block;
                vertical-align: middle;
                position: relative;
                padding-left: 5px;
            }

                .checkbox label::before {
                    content: "";
                    display: inline-block;
                    position: absolute;
                    width: 17px;
                    height: 17px;
                    left: 0;
                    margin-left: -20px;
                    border: 1px solid #cccccc;
                    border-radius: 3px;
                    background-color: #fff;
                    -webkit-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
                    -o-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
                    transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
                }

                .checkbox label::after {
                    display: inline-block;
                    position: absolute;
                    width: 16px;
                    height: 16px;
                    left: 0;
                    top: 0;
                    margin-left: -20px;
                    padding-left: 3px;
                    padding-top: 1px;
                    font-size: 11px;
                    color: #555555;
                }

            .checkbox input[type="checkbox"] {
                opacity: 0;
                z-index: 1;
            }

                .checkbox input[type="checkbox"]:focus + label::before {
                    outline: thin dotted;
                    outline: 5px auto -webkit-focus-ring-color;
                    outline-offset: -2px;
                }

                .checkbox input[type="checkbox"]:checked + label::after {
                    font-family: 'FontAwesome';
                    content: "\f00c";
                }

                .checkbox input[type="checkbox"]:checked + label::after {
                    font-family: 'FontAwesome';
                    content: "\f00c";
                }

                .checkbox input[type="checkbox"]:disabled + label {
                    opacity: 0.65;
                }

                    .checkbox input[type="checkbox"]:disabled + label::before {
                        background-color: #eeeeee;
                        cursor: not-allowed;
                    }

            .checkbox.checkbox-circle label::before {
                border-radius: 50%;
            }

            .checkbox.checkbox-inline {
                margin-top: 0;
            }

        .checkbox-primary input[type="checkbox"]:checked + label::before {
            background-color: #428bca;
            border-color: #428bca;
        }

        .checkbox-primary input[type="checkbox"]:checked + label::after {
            color: #fff;
        }

        .checkbox-danger input[type="checkbox"]:checked + label::before {
            background-color: #d9534f;
            border-color: #d9534f;
        }

        .checkbox-danger input[type="checkbox"]:checked + label::after {
            color: #FF0000;
            content: "\f00d";
        }

        .checkbox-info input[type="checkbox"]:checked + label::before {
            background-color: #5bc0de;
            border-color: #5bc0de;
        }

        .checkbox-info input[type="checkbox"]:checked + label::after {
            color: #fff;
        }

        .checkbox-warning input[type="checkbox"]:checked + label::before {
            background-color: #f0ad4e;
            border-color: #f0ad4e;
        }

        .checkbox-warning input[type="checkbox"]:checked + label::after {
            color: #fff;
        }

        .checkbox-success input[type="checkbox"]:checked + label::before {
            background-color: #5cb85c;
            border-color: #5cb85c;
        }

        .checkbox-success input[type="checkbox"]:checked + label::after {
            color: #fff;
        }

        .radio {
            padding-left: 20px;
        }

            .radio label {
                display: inline-block;
                vertical-align: middle;
                position: relative;
                padding-left: 5px;
            }

                .radio label::before {
                    content: "";
                    display: inline-block;
                    position: absolute;
                    width: 17px;
                    height: 17px;
                    left: 0;
                    margin-left: -20px;
                    border: 1px solid #cccccc;
                    border-radius: 50%;
                    background-color: #fff;
                    -webkit-transition: border 0.15s ease-in-out;
                    -o-transition: border 0.15s ease-in-out;
                    transition: border 0.15s ease-in-out;
                }

                .radio label::after {
                    display: inline-block;
                    position: absolute;
                    content: " ";
                    width: 11px;
                    height: 11px;
                    left: 3px;
                    top: 3px;
                    margin-left: -20px;
                    border-radius: 50%;
                    background-color: #555555;
                    -webkit-transform: scale(0, 0);
                    -ms-transform: scale(0, 0);
                    -o-transform: scale(0, 0);
                    transform: scale(0, 0);
                    -webkit-transition: -webkit-transform 0.1s cubic-bezier(0.8, -0.33, 0.2, 1.33);
                    -moz-transition: -moz-transform 0.1s cubic-bezier(0.8, -0.33, 0.2, 1.33);
                    -o-transition: -o-transform 0.1s cubic-bezier(0.8, -0.33, 0.2, 1.33);
                    transition: transform 0.1s cubic-bezier(0.8, -0.33, 0.2, 1.33);
                }

            .radio input[type="radio"] {
                opacity: 0;
                z-index: 1;
            }

                .radio input[type="radio"]:focus + label::before {
                    outline: thin dotted;
                    outline: 5px auto -webkit-focus-ring-color;
                    outline-offset: -2px;
                }

                .radio input[type="radio"]:checked + label::after {
                    -webkit-transform: scale(1, 1);
                    -ms-transform: scale(1, 1);
                    -o-transform: scale(1, 1);
                    transform: scale(1, 1);
                }

                .radio input[type="radio"]:disabled + label {
                    opacity: 0.65;
                }

                    .radio input[type="radio"]:disabled + label::before {
                        cursor: not-allowed;
                    }

            .radio.radio-inline {
                margin-top: 0;
            }

        .radio-primary input[type="radio"] + label::after {
            background-color: #428bca;
        }

        .radio-primary input[type="radio"]:checked + label::before {
            border-color: #428bca;
        }

        .radio-primary input[type="radio"]:checked + label::after {
            background-color: #428bca;
        }

        .radio-danger input[type="radio"] + label::after {
            background-color: #d9534f;
        }

        .radio-danger input[type="radio"]:checked + label::before {
            border-color: #d9534f;
        }

        .radio-danger input[type="radio"]:checked + label::after {
            background-color: #d9534f;
        }

        .radio-info input[type="radio"] + label::after {
            background-color: #5bc0de;
        }

        .radio-info input[type="radio"]:checked + label::before {
            border-color: #5bc0de;
        }

        .radio-info input[type="radio"]:checked + label::after {
            background-color: #5bc0de;
        }

        .radio-warning input[type="radio"] + label::after {
            background-color: #f0ad4e;
        }

        .radio-warning input[type="radio"]:checked + label::before {
            border-color: #f0ad4e;
        }

        .radio-warning input[type="radio"]:checked + label::after {
            background-color: #f0ad4e;
        }

        .radio-success input[type="radio"] + label::after {
            background-color: #5cb85c;
        }

        .radio-success input[type="radio"]:checked + label::before {
            border-color: #5cb85c;
        }

        .radio-success input[type="radio"]:checked + label::after {
            background-color: #5cb85c;
        }
    </style>
    <style>
        /* The container */
        .container {
            display: block;
            position: relative;
            padding-left: 35px;
            margin-bottom: 12px;
            cursor: pointer;
            font-size: 22px;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }

            /* Hide the browser's default checkbox */
            .container input {
                position: absolute;
                opacity: 0;
                cursor: pointer;
                height: 0;
                width: 0;
            }

        /* Create a custom checkbox */
        .checkmark {
            position: absolute;
            top: 0;
            left: 0;
            height: 25px;
            width: 25px;
            background-color: #eee;
        }

        /* On mouse-over, add a grey background color */
        .container:hover input ~ .checkmark {
            background-color: #ccc;
        }

        /* When the checkbox is checked, add a blue background */
        .container input:checked ~ .checkmark {
            background-color: green;
        }

        /* Create the checkmark/indicator (hidden when not checked) */
        .checkmark:after {
            content: "";
            position: absolute;
            display: none;
        }

        /* Show the checkmark when checked */
        .container input:checked ~ .checkmark:after {
            display: block;
        }

        /* Style the checkmark/indicator */
        .container .checkmark:after {
            left: 9px;
            top: 5px;
            width: 5px;
            height: 10px;
            border: solid white;
            border-width: 0 3px 3px 0;
            -webkit-transform: rotate(45deg);
            -ms-transform: rotate(45deg);
            transform: rotate(45deg);
        }
    </style>
    <style>
        .modalBackground {
            background-color: Gray !important;
            filter: alpha(opacity=50) !important;
            opacity: 0.7 !important;
        }
    </style>
    <style>
        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a, .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    color: #3ac0f2;
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    margin-left: -1px;
                }

                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    margin-left: -1px;
                    z-index: 2;
                    color: #3ac0f2;
                    background-color: #f5f5f5;
                    border-color: #dddddd;
                    cursor: default;
                }

                .pagination-ys table > tbody > tr > td:first-child > a, .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a, .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover, .pagination-ys table > tbody > tr > td > span:hover, .pagination-ys table > tbody > tr > td > a:focus, .pagination-ys table > tbody > tr > td > span:focus {
                    color: Black;
                    background-color: #eeeeee;
                    border-color: #dddddd;
                }

        .cls-search-button {
            float: right;
            width: 40px;
            height: 30px;
            background-color: red;
            color: #fff;
            line-height: 30px;
            position: relative;
            right: -10px;
            border-radius: 15px 0px 0px 15px;
            text-align: center;
        }
    </style>
    <script type="text/javascript">

        //$(function () {
        //  $("#Imag").elevateZoom({
        //    zoomType: "inner",
        //  cursor: "crosshair"
        //});

        //});
        function checkAll(chkall) {
            var chkall = $('#' + chkall).is(':checked');
            $('[id=checkbox2]').prop('checked', false);



            $('[id*=GVSealSign] .ClassChkAll input[type="radio"]').each(function () {
                //                if ($(this).closest('tr').find('.rdoReject input[type="radio"]').is(':checked')) {
                //                }
                //                else {
                var Rid = $(this).attr('id').replace('chkApprove', 'ddLRejectReasion');
                if (chkall) {
                    $(this).prop('checked', true);
                    $('#' + Rid).hide();
                }
                else {
                    $(this).prop('checked', false);
                    $('#' + Rid).show();
                }
                //                }


            });

        }
        function checkRjectAll(ClassChkRAll) {
            var chkall = $('#' + ClassChkRAll).is(':checked');
            $('[id=CheckBox1]').prop('checked', false);
            $('[id*=GVSealSign] .ClassChkRAll input[type="radio"]').each(function () {
                var Rid = $(this).attr('id').replace('chkReject', 'ddLRejectReasion');
                if (chkall) {
                    debugger;
                    $(this).prop('checked', true);
                    $('#' + Rid).show();
                }
                else {
                    $(this).prop('checked', false);
                    $('#' + Rid).hide();
                }


            });

        }
        //.replace('chkReject', 'ddLRejectReasion')
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
        function validateFristNumeric1(txt) {
            debugger;
            var firstChar = txt.value.charAt(0);
            if (firstChar == 0) {
                //do your stuff
                alert("Please enter correct SR No");
                txt.value = "";

            }
            else {
                return true;
            }


        }
        function validateFristNumeric2(txt) {
            debugger;
            var firstChar = txt.value.charAt(0);
            if (firstChar == 0) {
                //do your stuff
                alert("Please enter correct Samgra ID");
                txt.value = "";

            }
            else {
                return true;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="container-fluid">

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="thumbnail" style="background-color: #f5f5f5; margin-bottom: 3px !important;">
                        <div class="panel-heading" style="padding: 0px;">
                            <div class="row">
                                <div class="col-lg-9 col-md-8 col-sm-8 col-xs-12">
                                    <h3 class="text-danger" style="margin: 0px;">Seal Sign Validation</h3>
                                </div>

                                <div class="col-lg-3 col-md-2 col-sm-2 col-xs-12">
                                    <button type="button" id="ton" class="btn btn-primary pull-right" style="height: 30px; margin-left: 5px; margin-right: 6px;">
                                        <i class="fa fa-bars"></i>

                                    </button>
                                    <asp:Button ID="btnApprove" CssClass="btn btn-success pull-right btn-sm " Style="margin-left: 5px;" ToolTip="Save"
                                        Text="  Back" OnClick="btnApprove_Click" runat="server" />
                                    <asp:Button ID="btnMain" OnClick="btnMain_Click" runat="server" Text="Submit"
                                        CssClass="btn btn-success pull-right btn-sm " />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div class="row">


                                <asp:HiddenField ID="hdnbtnValue" runat="server" />
                                <%--   <a class="cls-search-button collapsed" role="button" data-toggle="collapse" data-parent="#accordion" 
                                                            href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo"><i class="fa fa-plus" style="margin-top: 8px;font-size: 16px;"></i>
                                                        </a>--%>
                                <div id="div-show" style="display: block; width: 100%;">
                                    <div class="row search-bg">
                                        <div class="form-horizontal">
                                            <asp:UpdatePanel runat="server" ID="Upnl" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="form-horizontal">
                                                        <div class="row">
                                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                                <div class="form-group">
                                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                        Year:
                                                                    </label>
                                                                    <div class="col-sm-8 padd">
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
                                                                    <div class="col-sm-8 padd">
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
                                                                    <div class="col-sm-8 padd">
                                                                        <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                                            AutoPostBack="true" class="form-control " />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                                <div class="form-group">
                                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                        Block:</label>
                                                                    <div class="col-sm-8 padd">
                                                                        <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                                            class="form-control " />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                                <div class="form-group">
                                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                        Cluster:</label>
                                                                    <div class="col-sm-8 padd">
                                                                        <asp:DropDownList ID="ddlVillage" runat="server" AutoPostBack="true" class="form-control "
                                                                            OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                                <div class="form-group">
                                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                        FC:</label>
                                                                    <div class="col-sm-8 padd">
                                                                        <asp:DropDownList ID="ddlFc" runat="server" class="form-control" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                                <div class="form-group">
                                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                        Village:</label>
                                                                    <div class="col-sm-8 padd">
                                                                        <asp:DropDownList ID="ddlVillageNew" OnSelectedIndexChanged="ddlVillageNew_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                                <div class="form-group">
                                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                        School:</label>
                                                                    <div class="col-sm-8 padd">
                                                                        <asp:DropDownList ID="ddlSchool" runat="server" class="form-control" />
                                                                    </div>
                                                                </div>
                                                            </div>


                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-10 col-md-2 col-sm-2 cpl-xs-10 ">
                                                            </div>
                                                            <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-10 " style="padding-right: 30px;">
                                                                <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" OnClick="btnSerach_Click"
                                                                    class="btn btn-danger btn-paddd pull-right" Style="background-color: #F1F1F1; border-width: 0px;" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-12" style="padding: 0px;">
                                    <asp:Panel ID="pnlMain" runat="server">
                                        <asp:UpdatePanel runat="server" ID="UpdatedddddddPanel1">
                                            <ContentTemplate>
                                                <div class="form-horizontal">
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-12" style="padding-left: 0px;">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <span style="float: left;">
                                                                    <label for="checkbox3" style="color: Black; font-size: medium;">
                                                                        School Name:
                                                                    </label>
                                                                    <asp:Label ID="lblSchoolName" AutoPostBack="true" ForeColor="Green" runat="server" />

                                                                </span>

                                                                <span style="float: right;">
                                                                    <asp:CheckBox ID="chkApproveAll" AutoPostBack="true" OnCheckedChanged="chkApproveAll_OnCheckedChanged"
                                                                        runat="server" />
                                                                    <label for="checkbox3" style="color: Black; font-size: medium;">
                                                                        Approve All
                                                                    </label>
                                                                    <asp:CheckBox ID="chkRejectAll" AutoPostBack="true" OnCheckedChanged="chkRejectAl444l_OnCheckedChanged"
                                                                        runat="server" />
                                                                    <label for="checkbox3" style="color: Black; font-size: medium;">
                                                                        Reject All
                                                                    </label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div style="height: 550px; overflow: auto; width: 99%;" align="center">
                                                            <div>
                                                                <div class="Row" style="width: 150%">
                                                                    <asp:GridView ID="GVSealSign" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                        DataKeyNames="UniqueChildCode" AutoGenerateColumns="False" Font-Names="Arial"
                                                                        OnRowDataBound="GVSealSign_OnRowDataBound" Font-Size="12px" Width="100%">
                                                                        <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found
                                                                            </div>
                                                                        </EmptyDataTemplate>
                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <Columns>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCUniqueChildCode" Visible="false" BackColor="Transparent" runat="server"
                                                                                        Text='<%# Bind("UniqueChildCode") %>' CssClass="form-controlAbhi"></asp:Label>
                                                                                    <asp:Label ID="lblSealSign" Visible="false" BackColor="Transparent" runat="server"
                                                                                        Text='<%# Bind("SealSign") %>' CssClass="form-controlAbhi"></asp:Label>

                                                                                    <asp:Label ID="lblSchoolCode" Visible="false" BackColor="Transparent" runat="server"
                                                                                        Text='<%# Bind("SchoolCode") %>' CssClass="form-controlAbhi"></asp:Label>
                                                                                    <asp:Label ID="lblDisecode" Visible="false" BackColor="Transparent" runat="server"
                                                                                        Text='<%# Bind("Disecode") %>' CssClass="form-controlAbhi"></asp:Label>


                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Child Name">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lblChildName" OnClick="LnkBtnBlock_OnClickNew" ForeColor="Black" runat="server" Text='<%# Eval("ChildName") %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" Width="10%" />
                                                                            </asp:TemplateField>
                                                                             
                                                                            <asp:TemplateField HeaderText="Father Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFathersName" ForeColor="Black" runat="server" Text='<%# Eval("FathersName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" Width="10%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Mother Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFathersNfame" ForeColor="Black" runat="server" Text='<%# Eval("MotherName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" Width="10%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Sr No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSerial" ForeColor="Black" runat="server" Text='<%# Eval("Serial") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" Width="8%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Social Category">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCategory" ForeColor="Black" runat="server" Text='<%# Eval("SocialCategory") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" Width="8%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="DOB">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDOB" ForeColor="Black" runat="server" Text='<%# Eval("DOB") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Enrolment Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEnrolmentDate" ForeColor="Black" runat="server" Text='<%# Eval("EnrolmentDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Class">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblClass" ForeColor="Black" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Approve/Reject">
                                                                                <ItemTemplate>
                                                                                    <span style="margin-left: 10px;">
                                                                                        <asp:RadioButton ID="chkApprove" GroupName="rdiogrp" runat="server" AutoPostBack="true"
                                                                                            CssClass="rdoApp ClassChkAll" OnCheckedChanged="chkReject_OnCheckedChanged" />
                                                                                        <span style="color: Black;">Approve</span> </span><span style="margin-left: 10px;">
                                                                                            <asp:HiddenField ID="hdnApprov" Value='<%# Eval("ApprovalStatus") %>' runat="server" />
                                                                                            <asp:RadioButton ID="chkReject" CssClass="rdoReject ClassChkRAll" AutoPostBack="true"
                                                                                                OnCheckedChanged="chkReject_OnCheckedChanged" GroupName="rdiogrp" runat="server" />
                                                                                            <span style="color: Black;">Reject</span> </span>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Reason">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddLRejectReasion" AutoPostBack="true" Visible="false" runat="server"
                                                                                        OnSelectedIndexChanged="ddLRejectReasion_OnSelectedIndexChanged" CssClass="form-control">
                                                                                    </asp:DropDownList>

                                                                                    <asp:HiddenField ID="hdnRejectReasion" Value='<%# Eval("RejectReason") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="20%" />
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Reason">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlSubReasion" Visible="false" runat="server" CssClass="form-control">
                                                                                    </asp:DropDownList>
                                                                                    <asp:HiddenField ID="hdnSubReasion" Value='<%# Eval("RejectSubReason") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="20%" />
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="SealSign Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSealSigns" ForeColor="Black" runat="server" Text='<%# Eval("SealSign") %>'></asp:Label>

                                                                                    <asp:Label ID="lblRejectFlag" Visible="false" ForeColor="Black" runat="server" Text='<%# Eval("ReasonFlag") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <PagerStyle CssClass="pagination-ys" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        </span>
                                                    </div>
                                                    <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12" id="DivImage" visible="false" runat="server"
                                                        style="padding: 0px; margin-top: 29px;">
                                                        <div class="row" style="background-color: #ddd; height: 51px;">
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                                <span style="line-height: 51px;">
                                                                    <asp:Label ID="lblDiscode" runat="server" Style="color: Green; font-size: 22px; align: center;"
                                                                        Text="1234567898_M_1"></asp:Label>
                                                                </span>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" style="margin-top: 10px;">
                                                                <asp:LinkButton ID="lnkPrev" runat="server" OnClick="lnkPrev_OnClick" CssClass="fa fa-step-backward fa-2x"
                                                                    ToolTip="Prev"></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_OnClick" Style="margin-left: 1pc; float: right;"
                                                                    CssClass="fa fa-step-forward fa-2x" ToolTip="Next"></asp:LinkButton>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                                <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_OnClick" CssClass="text-success fa fa-arrow-circle-o-down fa-2x"
                                                                    Style="float: right; margin-top: 10px;" ToolTip="Download"></asp:LinkButton>
                                                            </div>
                                                        </div>

                                                        <div class="row">


                                                            <asp:ImageButton ID="Imag" class="small" data-zoom-image="images/large/image1.jpg" OnClick="btnPreview_OnClick"
                                                                runat="server" Width="100%" Height="465px" />
                                                            <asp:Label ID="lblDisplay" Visible="false" Style="color: Green; font-size: 26px;"
                                                                runat="server" />

                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkDownload" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </div>
                                <ajax:ModalPopupExtender ID="MpexdrDistrict" runat="server" BackgroundCssClass="modalBackground "
                                    PopupControlID="PnlDistrict" CancelControlID="CancelButton" TargetControlID="HdnFild7">
                                </ajax:ModalPopupExtender>
                                <asp:HiddenField ID="HdnFild7" runat="server"></asp:HiddenField>
                                <asp:Panel CssClass="model-wid mod-posi" Style="display: none; width: 45% !important; margin-top: -60px !important;"
                                    ID="PnlDistrict" runat="server">
                                    <div style="width: 100%; height: auto; background-color: #f1f1f1">
                                        <div class="modal-header" style="background-color: #dddd; color: White;">
                                            <asp:ImageButton ID="CancelButton" ImageUrl="~/images/close-29.png" Style="float: right;"
                                                Width="3%" Height="3%" runat="server" />
                                        </div>
                                        <div class="modal-body">
                                            <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                            <div class="form-horizontal">
                                            </div>
                                            <asp:ImageMap ID="imgMKS" runat="server" Height="680px" Width="100%" BorderColor="Black"
                                                BorderStyle="Ridge" BorderWidth="1px" />
                                        </div>
                                        <%--  <div class="modal-footer">
                                                    <asp:Button ID="CancelButton" runat="server" CssClass="btn bgm-cyan" Text="Close"
                                                        ToolTip="Close" Style="float: none;"></asp:Button></div>--%>
                                    </div>
                                </asp:Panel>


                                <ajax:ModalPopupExtender ID="MpexdrDistrictAdd" runat="server" BackgroundCssClass="modalBg "
                                    CancelControlID="CancelButton" PopupControlID="PnlDistrictADD" TargetControlID="HdnFildAdd">
                                </ajax:ModalPopupExtender>
                                <asp:HiddenField ID="HdnFildAdd" runat="server"></asp:HiddenField>

                                <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important; margin-top: -75.5px !important;"
                                    ID="PnlDistrictADD" runat="server">
                                    <div style="width: 100%; height: auto; background-color: #f1f1f1">
                                        <div class="modal-header" style="background-color: #ddd; color: White;">
                                            <h4 class="modal-title" style="forecolor: White"></h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                            </div>
                                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                <div class="form-horizontal">
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-4" for="Name" style="padding-top: 14px">Student Name  <span class="req">*</span></label>

                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtChildName" class="form-control" onkeypress="return onlyAlphabets(event,this);" autocomplete="off" ondrop="return false;" ForeColor="Black" runat="server"></asp:TextBox>


                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-4" for="Name" style="padding-top: 14px">Father Name <span class="req">*</span></label>

                                                            <div class="col-sm-6">

                                                                <asp:TextBox ID="txtFatherName" class="form-control" onkeypress="return onlyAlphabets(event,this);" autocomplete="off" ondrop="return false;" ForeColor="Black" runat="server"></asp:TextBox>


                                                            </div>
                                                        </div>
                                                    </div>

                                                     <div class="row">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-4" for="Name" style="padding-top: 14px">Mother Name <span class="req">*</span></label>

                                                            <div class="col-sm-6">

                                                                <asp:TextBox ID="txtMonthName" class="form-control" onkeypress="return onlyAlphabets(event,this);" autocomplete="off" ondrop="return false;" ForeColor="Black" runat="server"></asp:TextBox>


                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" id="Div4" runat="server">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-4" for="Name" id="Label2" runat="server">Class </label>

                                                            <div class="col-sm-6">

                                                                <asp:DropDownList ID="dllClass" class="form-control" runat="server">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row" id="Div5" runat="server">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-4" for="Name" id="Label3" runat="server">SR NO.<span class="req">*</span></label>

                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtSrno" class="form-control" ForeColor="Black"  runat="server" MaxLength="9" onchange="return validateFristNumeric1(this);" autocomplete="off" ondrop="return false;"></asp:TextBox>

                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div id="Div6" runat="server" class="row">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-4" for="Name" id="Label4" runat="server">Admission Date<span class="req">*</span>  </label>

                                                            <div class="col-sm-8">
                                                                <asp:TextBox runat="server" ID="txtBirth" Width="73%" autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>

                                                                <ajax:CalendarExtender ID="clk" runat="server" Enabled="True"
                                                                    Format="dd/MM/yyyy" TargetControlID="txtBirth" OnClientDateSelectionChanged="arrivaldatecheck" PopupPosition="BottomRight">
                                                                </ajax:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBirth"
                                                                    Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                                    SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>



                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="Divkj2" runat="server" class="row">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-4" for="Name" id="Lagggbel2" runat="server">DOB<span class="req">*</span> </label>

                                                            <div class="col-sm-8">
                                                                <asp:TextBox runat="server" ID="txtDobDate" Width="73%" autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>

                                                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                                    Format="dd/MM/yyyy" TargetControlID="txtDobDate" OnClientDateSelectionChanged="arrivaldatecheck" PopupPosition="BottomRight">
                                                                </ajax:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDobDate"
                                                                    Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                                    SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>



                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" id="Div7" runat="server">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-4" for="Name" id="Label5" runat="server">Social Category<span class="req">*</span></label>

                                                            <div class="col-sm-6">
                                                                <asp:DropDownList ID="ddlScat" class="form-control" runat="server"></asp:DropDownList>

                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div id="a" runat="server" class="row">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-4" for="Name">Gender  <span class="req">*</span> </label>

                                                            <div class="col-sm-6">
                                                                <asp:DropDownList ID="ddlGender" CssClass="form-control" runat="server">
                                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                    <asp:ListItem Value="1">1-Male </asp:ListItem>
                                                                    <asp:ListItem Value="2">2-Female</asp:ListItem>
                                                                </asp:DropDownList>


                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel" runat="server"
                                                                        ControlToValidate="ddlGender" InitialValue="0" ErrorMessage="*" ForeColor="Red"
                                                                        ValidationGroup="Valid">

                                                                    </asp:RequiredFieldValidator></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" id="Div9" runat="server">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-4" for="Name" id="Label10" runat="server">Samgra ID<span class="req">*</span></label>

                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtSamgra"  onchange="return validateFristNumeric2(this);" onkeypress="return isNumberKey(this,event);" MaxLength="9" class="form-control" runat="server"></asp:TextBox>




                                                            </div>
                                                        </div>


                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-4" for="Name">House/Family No</label>

                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtHHNo" class="form-control" onkeypress="return onlyAlphabetsHH(event,this);" onchange="checkPwd(this.value);" autocomplete="off" ondrop="return false;" ForeColor="Black" runat="server"></asp:TextBox>




                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" id="Div10" runat="server" visible="false">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-4" for="Name" id="Label8" runat="server">Image</label>

                                                            <div class="col-sm-6">
                                                                <asp:FileUpload ID="FileuploadAttach" runat="server" Width="160px" Font-Size="Smaller"
                                                                    TabIndex="16" />



                                                            </div>
                                                        </div>


                                                    </div>






                                                    <div class="row" id="Div8" runat="server" visible="false">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-4" for="Name" id="Label6" runat="server">Previous Educational Status</label>

                                                            <div class="col-sm-6">
                                                                <asp:DropDownList ID="ddlEnroll" class="form-control" runat="server">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div id="Div1" class="row" runat="server" visible="false">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-4" for="Name" id="Label7" runat="server">Enrollment Category</label>

                                                            <div class="col-sm-6">
                                                                <asp:DropDownList ID="ddlEduationStatus" class="form-control" runat="server">
                                                                </asp:DropDownList>

                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <div id="Div11" class="col-lg-12 col-md-12 col-sm-12 cpl-xs-12" runat="server">
                                                <asp:Button ID="ImageButton2" ValidationGroup="Valid" CssClass="btn btn-success pull-pull-right" Text="Save"
                                                    ToolTip="Save" runat="server" OnClick="btSave_Click" /></span>

                                                <asp:ImageButton ID="ImageButton3" ImageUrl="~/images/close-29.png" runat="server"
                                                    Text="Close" ToolTip="Close" Style="float: none;"></asp:ImageButton>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <ajax:ModalPopupExtender ID="MpexdrPopUp" runat="server" BackgroundCssClass="modalBackground "
                PopupControlID="pnlCl" CancelControlID="CancelButton" TargetControlID="hhk">
            </ajax:ModalPopupExtender>
            <asp:HiddenField ID="hhk" runat="server"></asp:HiddenField>
            <asp:Panel CssClass="model-wid mod-posi" Style="display: none; width: 56% !important; margin-top: 93px !important;"
                ID="pnlCl" runat="server">
                <div style="width: 100%; height: auto; background-color: #f1f1f1">
                    <div class="modal-header" style="background-color: #3ac0f2; color: White;">
                        <asp:Label ID="Label1" runat="server" Visible="false" ForeColor="Black" Font-Names="Verdana"
                            Font-Size="11px"></asp:Label>
                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/close-29.png" Style="float: right;"
                            Width="3%" Height="3%" runat="server" />
                        <asp:Button ID="btnMainAll" runat="server" Width="10%" OnClick="btnAll_Click" Text="Save"
                            Style="float: right; margin-right: 36px;" CssClass="btn btn-success pull-right " />
                    </div>
                    <div class="modal-body">
                        <div class="form-horizontal">
                            <div style="height: 100px; overflow: auto; width: 99%;" align="center">
                                <div class="col-lg-6 col-md-6 col-sm-6 cpl-xs-12">
                                    <div class="form-group">
                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                            Reason:
                                        </label>
                                        <div class="col-sm-8 padd">
                                            <asp:DropDownList ID="ddlAllResone" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAllResone_SelectedIndexChanged"
                                                class="form-control ">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 cpl-xs-12">
                                    <div class="form-group">
                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                            Reason:
                                        </label>
                                        <div class="col-sm-8 padd">
                                            <asp:DropDownList ID="ddlAllResoneSub" runat="server" class="form-control ">
                                            </asp:DropDownList>
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
            <asp:PostBackTrigger ControlID="ImageButton2" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
