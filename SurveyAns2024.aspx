<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="SurveyAns2024.aspx.cs" Inherits="SurveyAns2024" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
 <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/jquery-2.1.1.min.js"></script>
    <%--<script src= "https://cdnjs.cloudflare.com/ajax/libs/jquery-ui-timepicker-addon/1.6.3/jquery-ui-timepicker-addon.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/jquery-ui-timepicker-addon.min.js"></script>

      <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/jquery-1.12.4.min.js"></script>

    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="ie=edge" />
    <title></title>
    <%--<script src="bootstrap4/js/jquery-2.1.4.min.js" type="text/javascript"></script>--%>
    <script src="js_S/jquery.min.js"></script>
    <script src="js_S/bootstrap.min.js"></script>
    <link href="css_s/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css_s/styles.css" rel="stylesheet" type="text/css" />
    <link href="css_s/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="css_s/cssforsurvey.css" rel="stylesheet" />
    <link href="Bootstrap/css/Site.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="css/CSS/font-awesome.css" media="all" />
    <link href="CalenderJquery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="CalenderJquery/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="CalenderJquery/jquery-ui-timepicker-addon.js" type="text/javascript"></script>
    <script src="CalenderJquery/jquery-ui.js" type="text/javascript"></script>
    <script src="CalenderJquery/jquery-ui.min.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="style/style-ed.css" />
    <style type="text/css">
        table#questions{
            width: 100%;
            border-collapse: separate;
            border-spacing: 0 1rem;
            margin-top: -1rem;
            margin-bottom: -1rem;

            font-size:14px;
        }
        table#questions tbody tr:nth-child(odd) td:nth-child(odd){
            background-color: #fff;
            border: 2px solid #3a91b3;
            border-radius: 12px;
            padding: 10px;
            box-shadow: 0px 0px 4px 0px #808080;
        }
        table#questions tbody tr:nth-child(odd) td:nth-child(odd) table tbody tr td{
            background-color: transparent;
            border: 0px solid #3a91b3;
            border-radius: 0px;
            padding: 0px;
            box-shadow: none;
        }
        table#questions tbody tr:nth-child(odd) td:nth-child(1) span{
            border: 1px solid;
            background-color: #3a91b3;
            color: #fff;
            width: 30px;
            float: left;
            border-radius: 0px 15px 15px 0px;
            text-align: center;
        }
        table#questions tbody tr:nth-child(even) td:nth-child(1){
            background-color: #fff;
            border: 2px solid #be7b43;
            border-radius: 12px;
            padding: 10px;
            box-shadow: 0px 0px 4px 0px #808080;
        }
        table#questions tbody tr:nth-child(even) td:nth-child(odd) table tbody tr td{
            background-color: transparent;
            border: 0px solid #3a91b3;
            border-radius: 0px;
            padding: 0px;
            box-shadow: none;
        }
        
        table#questions tbody tr:nth-child(even) td:nth-child(1) span{
            border: 1px solid;
            background-color: #be7b43;
            color: #fff;
            width: 30px;
            float: left;
            border-radius: 0px 15px 15px 0px;
            text-align: center;
        }
        .labs{
           float: left;
            width: calc(100% - 25px);
            margin-bottom: 5px;
            font-style: normal;
        }
        .inp{
           position: relative;
            top: 5px;
            float: left;
        }
        .lab{
            width: 100%;
            height: 100%;

        }
    </style> 
    <script type="text/javascript">
const { debug } = require("util");



        //$(function () {
        //    try {
        //        $.ajax({
        //            type: "POST",
        //            url: "Survey_District_Level.aspx/getMonth",
        //            //data: "{StateID:" + $('#ddlstate').val() + "}",
        //            contentType: "application/json; charset=utf-8",
        //            dataType: "json",

        //            success: function (response) {
        //                if (response != 0) {
        //                    var respo = JSON.parse(response.d);
        //                    var list = '<option value="0">Select Month</option>'
        //                    $.each(respo, function (i, n) {
        //                        list += '<option value="' + n.MonthID + '">' + n.Month + '</option>'
        //                    })
        //                    $('#ddlmonths').html(list)
        //                    $('#ddlmonths').val($('#hdnmonth').val());
        //                }
        //                // alert(response.d);

        //            },
        //            failure: function (response) {
        //                alert(response.d);
        //            },
        //        });

        //    } catch (e) {

        //    }
        //});

        function Imageuploaddata(textid, maiID) {

            var fileInput =
                document.getElementById(textid);

            var filePath = fileInput.value;

            // Allowing file type
            var allowedExtensions =
                /(\.jpg|\.jpeg|\.png|\.gif)$/i;

            if (!allowedExtensions.exec(filePath)) {
                alert('Invalid file type');
                fileInput.value = '';
                return false;
            }
            else {

                $.ajax({
                    url: 'HandlerCS.ashx',
                    type: 'POST',
                    data: new FormData($('form')[0]),
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (textid) {

                        var imm = textid.name;
                        maiID.value = imm;
                        //$("#fileProgress").hide();
                        //$("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                    }
                });
            }
            
        }
        function savedata() {
         
          
            var Participate = $('#ddlParticipate').val();
         
            if (Participate == "0") {
                alert('Please enter  Participant');
            }
            else {
              
               
                    var listData = [];
                    // var data = {};
                $('#questions1 tr').each(function () {
                        if ($(this).hasClass('header')) {

                        } else {

                            try {
                             

                                var elem6 = $(this).find('select')[0]



                                if (elem6.name == "Dropdown") {

                                    var e = document.getElementById(elem6.id);

                                    var dlr = e.value;
                                    if (dlr > 0) {
                                        listData.push({ "QuestionId": elem6.id, "QuestionValue": e.value })
                                    }
                                    else {

                                    }

                                }

                            } catch (Exception) {

                            }

                        }
                    })
                $('#questions1 tr').each(function () {
                        if ($(this).hasClass('header')) {

                        } else {

                            try {

                                var elem = $(this).find('input')[0]

                               


                                if (elem.name == "Numeric" || elem.name == "Text" || elem.name == "Date") {

                                    if (elem.value != '')
                                        listData.push({ "QuestionId": elem.id, "QuestionValue": elem.value })
                                    // alert(elem.name + "_" + elem.id + "_" + elem.value)
                                }
                                else {
                                    if (elem.type == "file") {
                                        debugger;
                                        var ValImage = "lbl" + elem.id;
                                        var kk = $('#' + ValImage).val();
                                        // Imageuploaddata(elem.id);
                                        if (kk.length > 0) {
                                            listData.push({ "QuestionId": elem.id, "QuestionValue": kk })
                                        }
                                    }
                                    if (elem.type == "radio") {

                                        for (var i = 0; i < $(this).children()[1].children.length; i++) {
                                            if ($(this).children()[1].children[i].type == "radio") {
                                                debugger;
                                                if ($(this).children()[1].children[i].checked == true)
                                                {
                                                    debugger;
                                                    listData.push({ "QuestionId": $(this).children()[1].children[i].name, "QuestionValue": $(this).children()[1].children[i].value })
                                                        }
                                            }
                                        }
                                    }

                                    else if (elem.type == "checkbox") {
                                        debugger;
                                        var checkedValue = '';
                                       
                                        for (var i = 0; i < $(this).children()[1].children.length; i++) {
                                            if ($(this).children()[1].children[i].type == "checkbox") {
                                            
                                                if ($(this).children()[1].children[i].checked == true) {
                                                    if (checkedValue == '')
                                                        checkedValue = $(this).children()[1].children[i].value;
                                                    else
                                                        checkedValue += "," + $(this).children()[1].children[i].value;
                                                    // alert("Checkbox_"+$(this).children()[2].children[i].name + "_" + $(this).children()[2].children[i].value)
                                                }
                                            }
                                        }
                                        if (checkedValue.length > 0) {
                                     
                                            listData.push({ "QuestionId": $(this).children()[1].children[0].name, "QuestionValue": checkedValue })
                                        }
                                        // alert("Checkbox_" + $(this).children()[2].children[i].name + "_" + checkedValue)
                                    }


                                }
                            } catch (Exception) {

                            }

                        }
                    })
                    try {
                        var stateid, districtid, blockid, formid, FinalFlag;
                        stateid = $('#hdnStateId').val();
                        districtid = $('#ddlParticipate').val();
                        blockid = $('#hdnblock').val();
                        formid = $('#hdnformid').val();
                        FinalFlag = $(':input[type="button"]').val();
                        var Year = $('#ddlyear').val();
                        var Month = $('#ddlmonths').val();
                        ShowProgress();
                        $.ajax({
                            type: "POST",
                            url: "SurveyAns.aspx/Savedata",
                            data: "{data:'" + JSON.stringify(listData) + "',StateID:'" + stateid + "', DistrictID:'" + districtid + "', Blockid:'" + blockid + "', FormID:'" + formid + "',FinalFlag:'" + FinalFlag + "',Year:'" + Year + "',Month:'" + Month + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",

                            success: function (response) {
                                var scrpt = response.d.split('___');
                                var Sd = scrpt[1];
                                if (Sd == 1) {
                                    debugger;
                                    document.getElementById("questions").style.visibility = "hidden";
                                    document.getElementById("IDSub").style.visibility = "hidden";
                                    document.getElementById("ddlParticipate").style.visibility = "hidden";
                                    document.getElementById("MainID").style.visibility = "hidden";
                                    document.getElementById("lblmsg").style.visibility = "hidden";
                                    $('#lblmsg1').text(scrpt[0]);
                                    window.scrollTo(0, 0);
                                }
                                else
                                {
                                    alert(scrpt[0]);
                                }
                                EndProgress();
                                //if (response.d == "Data Submitted Successfully.") {
                                //    window.location = "SurveyAns.aspx";
                                //}
                                //else if (response.d == "Data Update Successfully.") {
                                //    window.location = "SurveyAns.aspx";
                                //}
                                //else if (response.d == "Data Final Submitted.") {
                                //    window.location = "SurveyAns.aspx";
                                //}
                                //else {
                                //    window.location = "SurveyAns.aspx";
                                //}
                            },
                            failure: function (response) {
                                alert(response.d);
                            },
                        });
                    } catch (e) {

                    }
                
            }
            // alert(listData);
        }

        function pageLoad() { $(".mydate").datepicker(); }


        function SetLogic(vid, Questionid) {
            $.ajax({
                type: "POST",
                url: "Survey_District_Level.aspx/getlogic",
                data: "{Questionid:'" + Questionid + "',Option:'" + vid + "',isflagvalue:'N'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    var jalobject = new Array();
                    jalobject = response.d.split("||");

                    var objhide = jalobject[0].split(",");
                    var objShow = jalobject[1].split(",");

                    $.ajax({
                        type: "POST",
                        url: "Survey_District_Level.aspx/getlogic",
                        data: "{Questionid:'" + Questionid + "',Option:'" + vid + "',isflagvalue:'Y'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var rdvalue = response.d;
                            if (vid == rdvalue) {
                                if (objhide != '') {
                                    $.each(objhide, function (indexes, values) {
                                        $('tr.' + values.replace(' ', '') + '').hide();
                                        $('#' + values.replace(' ', '') + '').val("");

                                        $('tr.' + values.replace(' ', '') + '').find('input:text, input:password, input:file, select, textarea').val('');
                                        $('tr.' + values.replace(' ', '') + '').find('input:radio, input:checkbox').removeAttr('checked').removeAttr('selected');

                                    });
                                }
                                if (objShow != '')
                                    $.each(objShow, function (indexes, values) {
                                        $('tr.' + values.replace(' ', '') + '').show();

                                    });
                            }
                            else {
                                $.each(objShow, function (indexes, values) {
                                    $('tr.' + values.replace(' ', '') + '').show();
                                });
                            }

                        },
                        failure: function (response) {
                            alert(response.d);
                        },
                    });


                },
                failure: function (response) {
                    alert(response.d);
                },
            });
        }

        function FN14351(Questionid) {
            debugger;
            var val1 = $('#14343').val();
            var val2 = $('#14344').val();
            var perto = val2 * 100 / val1;

            $('#14351').val(perto);

        }

        function getDistrict() {
            try {
                if ($('#ddlstate').val() != 0) {
                    $.ajax({
                        type: "POST",
                        url: "Survey_District_Level.aspx/getDistrict",
                        data: "{StateID:" + $('#ddlstate').val() + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",

                        success: function (response) {
                            if (response != 0) {
                                var respo = JSON.parse(response.d);
                                var list = '<option value="0">Select District</option>'
                                $.each(respo, function (i, n) {
                                    list += '<option value="' + n.DistrictID + '">' + n.District + '</option>'
                                })
                                $('#ddldistrict').html(list)
                            }
                            // alert(response.d);

                        },
                        failure: function (response) {
                            alert(response.d);
                        },
                    });
                } else {
                    $('#ddldistrict').html('<option value="0">Select District<option>');
                }
            } catch (e) {

            }
        }

        function getBlock() {
            debugger;
            try {
                if ($('#ddlParticipate').val() != 0) {
                    $.ajax({
                        type: "POST",
                        url: "SurveyAns.aspx/getBlock",
                        data: "{DistrictID:" + $('#ddlParticipate').val() + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",

                        success: function (response) {
                            if (response != 0) {
                                if (response.d == "Data Allready Submitted")
                                {
                                    alert(response.d);

                                    document.getElementById("IDSub").style.display = "block";
                                }
                                else
                                {
                                    document.getElementById("IDSub").style.display = "none";

                                }
                                // alert(response.d);

                            }
                            // alert(response.d);

                        },
                        failure: function (response) {
                            alert(response.d);
                        },
                    });
                } else {
                    $('#ddldistrict').html('<option value="0">Select District<option>');
                }
            } catch (e) {

            }

         
      
        }


        function getBlockNew() {
            debugger;
            var gg = $('#txtP').val();
           
            try {
            
                    $.ajax({
                        type: "POST",
                        url: "SurveyAns.aspx/getBlock",
                        data: "{DistrictID:'" + gg + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",

                        success: function (response)
                        {
                            var scrpt = response.d.split('___');
                            var Sd = scrpt[1];
                            var Sdms = scrpt[0];
                            var ParName = scrpt[2];
                            debugger;
                            if (Sdms == 'Submitted')
                            {
                                $('#ddlParticipate').val(Sd);
                                $('#txtP').val(ParName);
                            }
                            if (Sdms == 'Participant not Found')
                            {
                                alert(scrpt[0]);
                                $('#ddlParticipate').val(Sd);
                                $('#txtP').val('');
                            }
                            if (Sdms == 'Data Already Submitted')
                            {
                                $('#txtP').val(ParName);
                                $('#ddlParticipate').val(Sd);
                                alert(scrpt[0]);

                            }
                        },
                        failure: function (response) {
                            alert(response.d);
                        },
                    });
              
            } catch (e)
            {

            }
         
         

        }

        function CheckNumeric(e) {
            var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
            if (keyCode == 8 || (keyCode >= 48 && keyCode <= 57) || (keyCode >= 96 && keyCode <= 105)) {
                return true;
            }
            alert("Only Numbers allowed.");
            event.preventDefault();
            return false;
        }
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
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                alert('Incorrect Number');
                el.value = "";
                return false;
            }
            else {
                return true;
            }
        }
        //function isNumberKey(evt) {
        //    var charCode = (evt.which) ? evt.which : evt.keyCode;
        //    if (charCode != 46 && charCode > 31
        //        && (charCode < 48 || charCode > 57))
        //        return false;

        //    return true;
        //}
        function ValidateEmail(txt) {
           
            var mailformat = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
           // var mailformat = /^w+([.-]?w+)*@w+([.-]?w+)*(.w{2,3})+$/;
            if (txt.value.match(mailformat)) {
                  //The pop up alert for a valid email address
                
                return true;
            }
            else {
                alert("You have entered an invalid email address!");    //The pop up alert for an invalid email address
                txt.value = "";
                return false;
            }
        }
        function validateOnlyText(txt) {
           
            if (!/^[a-zA-Z]*$/g.test(txt.value)) {
                alert("Only characters allow");
                txt.value = "";
                return false;
            }
            else {
                return true;
            }
        }
        function validateFristNumeric(txt) {

            var firstChar = txt.value.charAt(0);
            if (firstChar <= 9 && firstChar >= 0) {
                //do your stuff
                return true;
            }
            else {
                alert("Please input Frist Number only");
                txt.value = "";
            }

           
        }
       
        function alphanumeric(txt) {
            var letters = /^[0-9a-zA-Z]+$/;
            if (txt.value.match(letters)) {
                
                return true;
            }
            else {
                alert('Please input alphanumeric characters only');
                txt.value = "";
                return false;

            }
        }
        function NotallowFeatureDate(txt) {
            var dtToday = new Date();

            var month = dtToday.getMonth() + 1;
            var day = dtToday.getDate();
            var year = dtToday.getFullYear();
            if (month < 10)
                month = '0' + month.toString();
            if (day < 10)
                day = '0' + day.toString();
            var maxDate = year + '-' + month + '-' + day;
            $('#' + txt).attr('max', maxDate);
        }
        function NotallowPastDate(txt) {
            var dtToday = new Date();

            var month = dtToday.getMonth() + 1;
            var day = dtToday.getDate();
            var year = dtToday.getFullYear();
            if (month < 10)
                month = '0' + month.toString();
            if (day < 10)
                day = '0' + day.toString();
            var maxDate = year + '-' + month + '-' + day;
            $('#' + txt).attr('min', maxDate);
        }
        function NotOnlyNumeric(txt) {
            var hh = txt.value;
            if (!isNaN(hh))
            {

                alert('Please input altest one characters only');
                txt.value = "";
                return false;
               
            }
            else {
               
                return true;
               
            }
           
        }
        function CheckMobile(value) {
            var mob = /^[6-9]{1}[0-9]{9}$/;
            var mobile = value.value;
            if (mobile.match(mob)) {
                return true;
            }
            else {
                alert("Invalid Mobile No.");
                value.value = "";
                event.preventDefault();
                return false;
            }
        }
        function CheckSpecial(e) {
            var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
            if (keyCode == 32 || keyCode == 8 || (keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 96 && keyCode <= 105)) {
                return true;
            }
            alert("You are entering a special character. Please remove if not required");
            event.preventDefault();
            return false;
        }

        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        function EndProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.hide();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }

    </script>
    <style>
        table#WebSurtte tr td {
    font-weight: 400;
    font-size: 14px;
}

        .Table-Tr-50 {
            float: left;
            width: 100%;
            height: auto;
        }

            .Table-Tr-50 tbody tr {
                float: left;
                width: auto !important;
                height: auto;
            }

                .Table-Tr-50 tbody tr td {
                    padding: 0px 10px;
                }

        @media(max-width:768px) {
            .Table-Tr-50 tbody tr {
                float: left;
                width: 100% !important;
                height: auto;
            }
        }

        #RBcss table tbody tr {
            float: inherit !important;
        }

        .width-100 {
            width: 100% !important;
        }

        .card-header h6 span {
            font-size: 1rem !important;
            font-weight: 200;
        }

        .modalBackground {
            background-color: Black;
            opacity: 0.4;
        }


        .mod-posi {
            position: fixed !important;
            top: 10% !important;
        }

        .model-wid {
            width: 60% !important;
            border: 2px solid #ddd;
        }

        .Panelcssshow {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 13px;
            border: 1px solid;
            margin: 10px 0px;
            padding: 15px 10px 15px 50px;
            background-repeat: no-repeat;
            background-position: 10px center;
        }

        .Requiredvalidate {
            font-size: 12px;
            color: Red;
        }

        .modal-header {
            padding: 0.5rem 0.5rem;
            border-bottom: 0px solid #dee2e6;
            border-top-left-radius: .0rem;
            border-top-right-radius: .0rem;
        }

        .bg-blue {
            background-color: #354ea0;
        }

        .bg-orange {
            background-color: #ff6a00;
            border-color: #ff6a00;
        }

        .bbme {
            border-bottom: 1px solid #ccc;
            margin-bottom: 10px;
        }

        .Mainheader {
            color: #ffffff;
            font-weight: bold;
            font-size: 22px;
           
        }

        .headerYellow {
            color: #fbf006;
            font-size: 18px;
        }

        .headerWhite {
            color: #ffffff;
            font-style: normal;
            font-size: 18px;
        }
    </style>
    <style type="text/css">
        .row.survey-asp .container {
            width: 100%;
            max-width: 100%;
            margin: 0;
            padding: 0;
        }


        tr.header td {
            font-size: 18px;
        }




        body {
            color: #6a6c6f;
            background-color: #f1f3f6;
        }

        .container {
            max-width: 960px;
        }

        .table > tbody > tr.active > td, .table > tbody > tr.active > th, .table > tbody > tr > td.active, .table > tbody > tr > th.active, .table > tfoot > tr.active > td, .table > tfoot > tr.active > th, .table > tfoot > tr > td.active, .table > tfoot > tr > th.active, .table > thead > tr.active > td, .table > thead > tr.active > th, .table > thead > tr > td.active, .table > thead > tr > th.active {
            background-color: #fff;
        }

        .table-bordered > tbody > tr > td, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > td, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > thead > tr > th {
            border-color: #e4e5e7;
        }

        /*.table tr.header {
            font-weight: bold;
            background-color: #fff;
            cursor: pointer;
            -webkit-user-select: none; 
            -moz-user-select: none;
            -ms-user-select: none; 
            user-select: none; 
        }

        .table tr:not(.header) {
            display: none;
        }

        .table .header td:after {
            content: "\002b";
            position: relative;
            top: 1px;
            display: inline-block;
            font-family: 'Glyphicons Halflings';
            font-style: normal;
            font-weight: 400;
            line-height: 1;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
            float: right;
            color: #999;
            text-align: center;
            padding: 3px;
            transition: transform .25s linear;
            -webkit-transition: -webkit-transform .25s linear;
        }

        .table .header.active td:after {
            content: "\2212";
        }*/

        .faq-section .mb-0 > a {
            display: block;
            position: relative;
        }

            .faq-section .mb-0 > a:after {
                content: "\f067";
                font-family: "Font Awesome 5 Free";
                position: absolute;
                right: 0;
                font-weight: 600;
            }

            .faq-section .mb-0 > a[aria-expanded="true"]:after {
                content: "\f068";
                font-family: "Font Awesome 5 Free";
                font-weight: 600;
            }




        /*tr.header.active > td {
            background: #1e90ff !important;
            color: #fff;
        }

            tr.header.active > td:after {
                color: #fff;
            }

        tr.header > td:after {
            font-size: 22px;
            font-weight: 900 !important;
        }*/
    </style>
</head>
<body>
    <form id="form1" runat="server">



        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            <Scripts>
                <%--<asp:ScriptReference Path="FusionCharts/updatepanelhook.fusioncharts.js" />--%>
            </Scripts>
        </asp:ToolkitScriptManager>

        <div class="container">
            <div class="row">

                <div class="col-sm-12  pt-4 text-center">
                    <h3 style="font-size: 24px; font-style: normal; color: #f45f19; text-shadow: 0px 6px 7px #00000080;" id="header3" runat="server"> 
                      <asp:Label runat="server" ID="lblmsg"></asp:Label> </h3>

                     <h3 style="font-size: 24px; font-style: normal; color: green; text-shadow: 0px 6px 7px #00000080;" id="h1" runat="server"> 
                      <asp:Label runat="server" ID="lblmsg1"></asp:Label> </h3>
                </div>
                <div class="col-sm-12  pt-4 text-center" style="display: none;">
                    <asp:Button ID="BtnSwitch" CssClass="btn btn-success btn-sm" runat="server" Visible="false" Text="Switch(Hindi)"></asp:Button>
                      
                </div>
            </div>
            <asp:HiddenField ID="hdnformid" runat="server" />
            <asp:HiddenField ID="hdndistrict" runat="server" />
            <asp:HiddenField ID="hdnStateId" runat="server" />
            <asp:HiddenField ID="hdnconditions" runat="server" />
            <asp:HiddenField ID="hdnblock" runat="server" />
            <asp:HiddenField ID="hdncallstatusid" runat="server" />
            <asp:HiddenField ID="hdnRespondentList" runat="server" />
            <asp:HiddenField ID="hdnmonth" runat="server" />
        </div>

        <div class="container" runat="server" id="MainID">
            <div class="row mt-4 table-head-bg">
                <div class="col-sm-12 ">
                    <div class="card-body">
                        <div class="row">
                           <div class="col-lg-6 col-md-6 col-sm-6 cpl-xs-12">

                                <div class="form-group">
                                    <div class="row">

                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                Participant:</label>
                                                            <div class="col-sm-9 padd">
                                                                  <asp:TextBox ID="txtP" runat="server"  class="form-control " AutoPostBack="true"   OnTextChanged="txtSearchName_Click"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlParticipate" style="display:none;"  runat="server" 
                                                                    class="form-control " />
                                                              
                                                            </div>
                                                           
                                        </div>
                                </div>
                                 

                               <div class="form-group" runat="server" Visible="false">
                                    <label class="text_color">
                                                    
                                        State
                                    </label>
                                          <button id="upload" runat="server" Visible="false" type="submit">Upload</button>
                                    <asp:DropDownList ID="ddlstate" CssClass="form-control" runat="server"  onchange="getDistrict()">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblstste" CssClass="form-control" Visible="false" runat="server"></asp:Label>
                                    <%--<span id="Errorstate" runat="server" visible="false" style="font-size: 12px; color: red;">*Required</span>--%>
                                </div>
                            </div>

                            <div class="col-xl-3 col-lg-3 col-md-3 col-sm-12 divsm"  runat="server" Visible="false" >
                                <div class="form-group">
                                    <label class="text_color">
                                        District
                                    </label>
                                    <asp:DropDownList ID="ddldistrict" CssClass="form-control" runat="server" Visible="false" onchange="getBlock()">
                                    </asp:DropDownList>
                                      <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <asp:Label ID="lbldistrict" CssClass="form-control" runat="server"></asp:Label>
                                    <%--<span id="Errordistrict" runat="server" visible="false" style="font-size: 12px; color: red;">*Required</span>--%>
                                </div>
                            </div>
                            <div class="col-xl-3 col-lg-3 col-md-3 col-sm-12 divsm"  runat="server" Visible="false">
                                <div class="form-group">
                                    <label class="text_color">
                                        Year
                                    </label>
                                    <select name="" id="ddlyear" runat="server" class="form-control">
                                        <option value="0">Select Year</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-xl-3 col-lg-3 col-md-3 col-sm-12 divsm"  runat="server" Visible="false">
                                <div class="form-group">
                                    <label class="text_color">
                                        Month
                                    </label>
                                    <select name="" id="ddlmonths" runat="server" class="form-control">
                                        <option value="0">Select Month</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-xl-3 col-lg-3 col-md-3 col-sm-12 divsm" style="display: none;"  runat="server" Visible="false">
                                <div class="form-group">
                                    <label class="text_color">
                                        Age
                                    </label>
                                    <asp:TextBox ID="txtage" CssClass="form-control" runat="server" placeholer="Age"
                                        OnTextChanged="txtage_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <span id="ErrorAge" runat="server" visible="false" style="font-size: 12px; color: red;">(Age Should be 5 to 100)</span>
                                </div>
                            </div>
                            <div class="col-xl-3 col-lg-3 col-md-3 col-sm-12 divsm" style="display: none;"  runat="server" Visible="false">
                                <div class="form-group">
                                    <label class="text_color">
                                        Gender
                                    </label>
                                    <asp:DropDownList ID="ddlgender" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Male</asp:ListItem>
                                        <asp:ListItem Value="2">Female</asp:ListItem>
                                        <asp:ListItem Value="3">Other</asp:ListItem>
                                    </asp:DropDownList>
                                    <span id="Errorgender" runat="server" visible="false" style="font-size: 12px; color: red;">*Required</span>
                                </div>
                            </div>
                             <div class="col-xl-3 col-lg-3 col-md-3 col-sm-12 divsm"  runat="server" Visible="false">
                                <div class="form-group">
                                    <label class="text_color">
                                        Survey
                                    </label>
                                   <asp:DropDownList ID="ddlForm" CssClass="form-control" runat="server" Visible="false" onchange="getBlock()">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="float: right; width: auto; position: fixed; right: 0px;">
                                <div class="form-group " style="margin-top: 30px; display: none;">
                                    <asp:Button ID="btnPartiallySave" CssClass="btn btn-success" runat="server" Text="Partially Save" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row survey-asp">
                <asp:HiddenField ID="HFFormEvalGUID" runat="server" />
                <asp:Literal ID="dialog" runat="server"></asp:Literal>
                <%--<div id="dialog" runat="server"></div>--%>
            </div>
            <asp:HiddenField ID="hdnExtension" runat="server" />
            <asp:HiddenField ID="hdnfilename" runat="server" />
            <div class="row my-4">
                <div class="col-sm-12 text-center">
                    <asp:Literal ID="Savebutton" runat="server"></asp:Literal>
                    <%--<input type="button" name="Submit" class="btn btn-success px-5" value="Submit" onclick="savedata()" />--%>
                </div>
            </div>
        </div>

        <div class="loading">
            <div class="lds-facebook">
                <div></div>
                <div></div>
                <div></div>
            </div>
            <span>Loading. Please wait...</span>
        </div>
        <div id="Selecttabtemp">
            <asp:ModalPopupExtender ID="ModalPopfs" BackgroundCssClass="modalBackground" runat="server"
                PopupControlID="pnl_fs" TargetControlID="Hdn_fs" CancelControlID="lblFormNameClose">
            </asp:ModalPopupExtender>
            <asp:HiddenField ID="Hdn_fs" runat="server" />
            <asp:Panel ID="pnl_fs" runat="server" CssClass="mod-posi modalPopup model-wid" Style="height: auto; display: none;">
                <div class="card" style="margin-bottom: 0px; border: 0px;">
                    <div class="card-heading bg-blue modal-header text-white" style="height: auto;">
                        <asp:Label ID="lblFormName" runat="server" Text="Call Status"></asp:Label>
                        <asp:LinkButton ID="lblFormNameClose" CssClass="btn btn-sm btn-danger pull-right"
                            runat="server"> <span class="fa fa-remove"></span></asp:LinkButton>
                    </div>
                    <div class="card-body modal-body">
                        <asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="card-footer modal-footer bg-blue" style="padding: 0.5rem;">
                        <asp:Button ID="btnOK" runat="server" Text="OK" OnClick="btnOK_Click" />
                    </div>
                </div>

            </asp:Panel>
        </div>
        <div id="2ndmodal">
            <asp:ModalPopupExtender ID="mdlMendetory" BackgroundCssClass="modalBackground" runat="server"
                PopupControlID="pnlmandotry" TargetControlID="hdn_m" CancelControlID="lblmandotryclose">
            </asp:ModalPopupExtender>
            <asp:HiddenField ID="hdn_m" runat="server" />
            <asp:Panel ID="pnlmandotry" runat="server" CssClass="mod-posi modalPopup model-wid"
                Style="height: auto; display: none;">
                <div class="card" style="margin-bottom: 0px; border: 0px;">
                    <div class="card-heading bg-blue modal-header text-white" style="height: auto;">
                        <asp:Label ID="lblmandotry" runat="server" Text="Call Status"></asp:Label>
                        <asp:LinkButton ID="lblmandotryclose" CssClass="btn btn-sm btn-danger pull-right"
                            runat="server"> <span class="fa fa-remove"></span></asp:LinkButton>
                    </div>
                    <div class="card-body modal-body">
                        <asp:Label ID="lblmandotrymsg" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="card-footer modal-footer bg-blue" style="padding: 0.5rem;">
                    </div>
                </div>
            </asp:Panel>
        </div>
        <%--<asp:HiddenField ID="hfState" runat="server" />         
        </ContentTemplate></asp:UpdatePanel>--%>
    </form>
</body>
</html>
