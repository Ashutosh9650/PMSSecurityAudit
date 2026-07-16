<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" MaintainScrollPositionOnPostback="true" Culture="en-GB" AutoEventWireup="true" CodeFile="frmTravelMatrixWithClusters.aspx.cs" Inherits="frmTravelMatrixWithClusters" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .asd label {
            margin-bottom: 0px;
            position: relative;
            top: 2px;
        }

        .btnStyle {
            border: 1px solid #ccc;
            margin-bottom: 7px;
            margin-right: 16px;
        }

        .HeaderClassCsss {
            text-align: center !important;
            font-weight: normal !important;
            background-color: #9A9C9A !important;
        }


        .pdtopGRD {
            padding-top: 0px !important;
        }

        .modalBg {
            background-color: #000;
            opacity: 0.5;
            z-index: 11;
        }
    </style>
    <style>
        . {
            background: linear-gradient(to bottom, #ebf1fd 0%, #ffffff 100%) !important;
            padding-top: 12px;
            padding-bottom: 0px;
        }

            .row {
                margin-right: -15px;
                margin-left: -15px;
            }

            .container-fluid {
                padding-right: 15px;
                padding-left: 15px;
                margin-right: auto;
                margin-left: auto;
            }

            .padd {
                padding-left: 0px;
                padding-right: 0px;
            }

            .form-group {
                margin-bottom: 15px;
                float: left;
                width: 100%;
            }

            .shows {
                display: block !important;
            }

            .hides {
                display: none;
            }

            .panel-default > .panel-heading {
                color: #333;
                background-color: #f5f5f5;
                border-color: #ddd;
                padding: 10px 15px;
            }

        #accordion .panel .panel-heading .panel-title a {
            text-decoration: none;
            font-weight: bold;
        }

            #accordion .panel .panel-heading .panel-title a span {
                float: right;
                width: auto;
            }

                #accordion .panel .panel-heading .panel-title a span:after {
                    content: "\2014";
                    color: #1B5062;
                    font-size: 20px;
                }

            #accordion .panel .panel-heading .panel-title a.collapsed span:after {
                content: "+";
            }
    </style>
    <style>
        . {
            background: linear-gradient(to bottom, #ebf1fd 0%, #ffffff 100%) !important;
            padding-top: 12px;
            padding-bottom: 0px;
        }

            .row {
                margin-right: -15px;
                margin-left: -15px;
            }

            .container-fluid {
                padding-right: 15px;
                padding-left: 15px;
                margin-right: auto;
                margin-left: auto;
            }

            .padd {
                padding-left: 0px;
                padding-right: 0px;
            }

            .form-group {
                margin-bottom: 15px;
                float: left;
                width: 100%;
            }

            .shows {
                display: block !important;
            }

            .hides {
                display: none;
            }

    </style>
    <style>
    .disabled-date {
      background-color: #f0f0f0 !important;
      color: #ccc !important;
      pointer-events: none;
    }
  </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#oed').click(function () {
                $('.oed').toggleClass("shows");
            })
        })
    </script>

    

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.7.14/css/bootstrap-datetimepicker.min.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment-with-locales.js"></script>

   <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.7.14/js/bootstrap-datetimepicker.min.js"></script>
   

    
 

    <script type="text/javascript">
        $(function () {
            $('[id*=txtSTime]').datetimepicker({
                format: 'LT'
            });
            $('[id*=txtTTime]').datetimepicker({
                format: 'LT'
            });
        });
    </script>
    <script>
        function UploadImage(textid1) {
            debugger;

            var file = $("#ctl00_MainContent_Fileupload1").get(0).files[0];
            var fileInput =
                document.getElementById(textid1);

            var filePath = fileInput.value;

            // Allowing file type
            var allowedExtensions =
                /(\.jpg|\.jpeg|\.png|\.gif\.pdf)$/i;

            if (!allowedExtensions.exec(filePath)) {
                alert('Invalid file type');
                fileInput.value = '';
                return false;
            }
            if (file) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var fileData = e.target.result;
                    $.ajax({
                        url: "frmTravelMatrixWithClusters.aspx/SaveImage",
                        type: "POST",
                        contentType: "application/json",
                        data: JSON.stringify({ base64File: fileData, fileName: file.name, File4: file }),
                        success: function (result) {
                            //alert(result.d);
                            //$("#ctl00_MainContent_Fileupload1").val("");
                        },
                        error: function (xhr, status, error) {
                            alert("An error occurred: " + (xhr.responseText || error));
                        }
                    });
                };
                reader.readAsDataURL(file);
            } else {
                alert("No file selected.");
            }



        }
       
    </script>

    <script type="text/javascript">
        function ImageuploaddataGust(textid1) {
            debugger;
            var fileInput =
                document.getElementById(textid1);

            var filePath = fileInput.value;

            // Allowing file type
            var allowedExtensions =
                /(\.jpg|\.jpeg|\.png|\.gif\.pdf)$/i;

            if (!allowedExtensions.exec(filePath)) {
                alert('Invalid file type');
                fileInput.value = '';
                return false;
            }
            else {


                $.ajax({
                    url: 'HandlerImageTravelGust.ashx',
                    type: 'POST',
                    data: new FormData($('form')[0]),
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (textid1) {

                        var imm = textid1.name;
                        maiID.value = imm;
                        //$("#fileProgress").hide();
                        //$("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                    }
                });
                fncsave();

                return true;
            }
        }
        function Imageuploaddata(textid, temp) {
            debugger;
            var fileInput =
                document.getElementById(textid);

            var filePath = fileInput.value;

            // Allowing file type
            var allowedExtensions =
                /(\.jpg|\.jpeg|\.png|\.gif\.pdf)$/i;

            if (!allowedExtensions.exec(filePath)) {
                alert('Invalid file type');
                fileInput.value = '';
                return false;
            }
            else {
                var gg = new FormData($('form')[0]);

                $.ajax({
                    url: 'HandlerImageTravel.ashx',
                    type: 'POST',
                    data: new FormData($('form')[0]),
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (textid) {
                        console.log(new FormData($('form')[0]));
                        var imm = textid.name;
                        /* maiID.value = imm;*/
                        //$("#fileProgress").hide();
                        //$("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                    }
                });


                return true;
            }
        }

    </script>
    <script type="text/javascript">

        function loadJSFunction1() {

            var ddlType = document.getElementById('<%=ddlType.ClientID %>').value;
            $('[id*=txtSTime]').datetimepicker({ format: 'DD-MM-YYYY, h:m a' }).on('dp.hide', function (e) {
                var formatedValue = e.date.format("YYYY-MM-DD HH:mm:ss");
                $("#datetime").val(formatedValue);
                if (ddlType == "2") {
                    document.getElementById("<%=btnSubmit.ClientID %>").click();
                }
              
              });
            $('[id*=txtTTime]').datetimepicker({ format: 'DD-MM-YYYY, h:m a' }).on('dp.hide', function (e) {
                var formatedValue = e.date.format("YYYY-MM-DD HH:mm:ss");
                $("#datetime").val(formatedValue);
                if (ddlType == "2") {
                    document.getElementById("<%=btnSubmit.ClientID %>").click();
                }
             });
        }
        function loadJSFunction() {

            $('[id*=txtSTime]').datetimepicker({
                format: 'LT'
            });
            $('[id*=txtTTime]').datetimepicker({
                format: 'LT'
            });

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
        function checkdataVic() {
            debugger;



            var ddlvehicle = document.getElementById('<%=ddlvehicle.ClientID %>').value;

            var txtdes = document.getElementById('<%=txtdes.ClientID %>').value;
            var txtVIcRent = document.getElementById('<%=txtVIcRent.ClientID %>').value;
            var lblUniqueCodeVe = document.getElementById('<%=lblUniqueCodeVe.ClientID %>').value;

            var str = "";
            if (ddlvehicle == "0") {
                str = str + "\n Please Select  Conveyance Type";
            }
            if (txtdes.trim() == "") {
                str = str + " Please Enter Description";
            }
            if (txtVIcRent == "") {
                str = str + "\n Please Enter Conveyance Fare";
            }
            <%--if (lblUniqueCodeVe.length > 4) {

             }
             else {
                 var xyz = document.getElementById('<%=FileuploadExpensevehicle.ClientID %>');

                 if (xyz.value == "") {
                     str = str + "\n Please Vehicle receipt  Image ";
                 }
             }--%>
            if (str != "") {
                alert(str);
                return false;
            }
        }

        function checkdata() {
            debugger;



            var txtTrotalAmout = document.getElementById('<%=txtTrotalAmout.ClientID %>').value;

            var txtExpense = document.getElementById('<%=txtExpense.ClientID %>').value;
            var hndMaxamt = document.getElementById('<%=hndMaxamt.ClientID %>').value;

            var lblUniqueCodeEx = document.getElementById('<%=lblUniqueCodeEx.ClientID %>').value;
            var str = "";
            if (txtTrotalAmout == "") {
                str = " Please Enter Amount";
            }
            if (txtExpense.trim() == "") {
                str = str + "\n Please Enter Other Expanse Details";
            }
            <%-- if (lblUniqueCodeEx.length > 4) {

             }
             else {
                 var xyz = document.getElementById('<%=FileuploadAttach.ClientID %>');

                 if (xyz.value == "") {
                     str = str + "\n Please Expense receipt Image ";
                 }
             }--%>
            if (str != "") {
                alert(str);
                return false;
            }
        }

        function checkdataMain() {
            debugger;
            var ddlType = document.getElementById('<%=ddlType.ClientID %>').value;
            var txtDate = document.getElementById('<%=txtDate.ClientID %>').value;

            var txtSTime = document.getElementById('<%=txtSTime.ClientID %>').value;
            var txtTTime = document.getElementById('<%=txtTTime.ClientID %>').value;

            var txtObjective = document.getElementById('<%=txtObjective.ClientID %>').value;
            var txtKM = document.getElementById('<%=txtKM.ClientID %>').value;
            var txtTotalFare = document.getElementById('<%=txtTotalFare.ClientID %>').value;
            var txtRemark = document.getElementById('<%=txtRemark.ClientID %>').value;
            var str = "";
            if (txtDate == "") {
                str = " Please Enter Date";
            }
            if (txtSTime == "") {
                str = str + "\n Please Enter Travel Start Time";
            }
            if (txtTTime == "") {
                str = str + "\n Please Enter Travel End Time:";
            }
            if (ddlType == "0") {
                str = str + "\n Travel Type";
            }
            if (ddlType == "1") {
                var ddlFromVillage = document.getElementById('<%=ddlFromVillage.ClientID %>').value;
                var ddlEndVillage = document.getElementById('<%=ddlEndVillage.ClientID %>').value;


                if (ddlFromVillage == "0") {
                    str = str + "\n Please select  Travel Start Place";
                }
                if (ddlEndVillage == "0") {
                    str = str + "\n Please select Travel End Place";
                }
                //if (ddlFromVillage == ddlEndVillage) {
                //    str = str + "\n Traveling start place and Traveling end place same please select other";
                //}

            }
            if (ddlType == "2") {
                var ddlMode = document.getElementById('<%=ddlMode.ClientID %>').value;
                if (ddlMode == "0") {
                    str = str + "\n Please Select  Mode of Travel";
                }

            }
            if (txtObjective.trim() == "") {
                str = str + "\n Please Enter  Travel Objective";
            }
            if (ddlType == "2" && ddlMode == "1") {

                if (txtKM == "") {
                    str = str + "\n Please Enter Distance(in KM)";
                }
            }
            if (ddlType == "1") {
                if (txtKM.trim() == "") {
                    str = str + "\n Please Enter Distance(in KM)";
                }
            }
            if (ddlType == "1") {
                if (txtTotalFare.trim() == "") {
                    str = str + "\n Please Enter Travel Fare";
                }

            }
            if (ddlType == "2") {
                if (ddlMode == "5") {

                }

                else {
                    if (txtTotalFare.trim() == "") {
                        str = str + "\n Please Enter Travel Fare";
                    }
                }
            }

            if (ddlType == "1") {

            }
            if (txtRemark.trim() == "") {
                str = str + "\n Please Enter Edit Reason";
            }


            if (str != "") {
                alert(str);
                return false;
            }
        }
        function CheckMaxAmountkk() {
            debugger;
            var txthoserent = document.getElementById('<%=txthoserent.ClientID %>').value;
            var hndMaxamt = document.getElementById('<%=hndMaxamt.ClientID %>').value;
            var ddlOccupancy = document.getElementById('<%=ddlOccupancy.ClientID %>').value;

            var str = "";
            if (ddlOccupancy == "0") {
                document.getElementById('<%=txthoserent.ClientID %>').value = '';
            }


            if (str != "") {
                alert(str);
                return false;

            }
        }
        function CheckMaxAmount() {
            debugger;
            var txthoserent = document.getElementById('<%=txthoserent.ClientID %>').value;
            var hndMaxamt = document.getElementById('<%=hndMaxamt.ClientID %>').value;
            var ddlOccupancy = document.getElementById('<%=ddlOccupancy.ClientID %>').value;

            var str = "";
            if (ddlOccupancy == "0") {
                document.getElementById('<%=txthoserent.ClientID %>').value = '';
                str = "Accommodation Type "
            }

            if (txthoserent != "" && ddlOccupancy != "0") {
                var Hammoumt;
                if (ddlOccupancy == "2") {
                    Hammoumt = parseInt(hndMaxamt) * 2;
                }
                else {
                    Hammoumt = parseInt(hndMaxamt);
                }
                if (Hammoumt == "0") {
                    str = "Please Select City Type "
                    document.getElementById('<%=txthoserent.ClientID %>').value = '';
                }
                else {
                    if (parseInt(txthoserent) > parseInt(Hammoumt)) {
                        document.getElementById('<%=txthoserent.ClientID %>').value = '';
                        str = "Please Enter Max Value :" + Hammoumt;
                    }
                    else {

                    }
                }

            }

            if (str != "") {
                alert(str);
                return false;

            }
        }
        function checkdataAmount() {
            debugger;
            var ddlType = document.getElementById('<%=ddlType.ClientID %>').value;

            var txtKM = document.getElementById('<%=txtKM.ClientID %>').value;
            var txtTotalFare = document.getElementById('<%=txtTotalFare.ClientID %>').value;
            var txtTotalFare1 = document.getElementById('<%=txtTotalFare.ClientID %>');
            var str = "";
            var ddlMode = document.getElementById('<%=ddlMode.ClientID %>').value;

            if (ddlType == "2" && ddlMode == "1") {
                if (txtKM != "") {
                    if (txtKM > 100) {

                        str = "Plese Enter Less then 100 KM";
                        document.getElementById('<%=txtTotalFare.ClientID %>').value = '';
                        document.getElementById('<%=txtKM.ClientID %>').value = '';
                    }
                    else {

                        var Total = txtKM * 4;
                        document.getElementById('<%=txtTotalFare.ClientID %>').value = Total;
                        document.getElementById('<%=txtTotalFare.ClientID %>').innerText = Total;
                        str = "";

                    }
                }
                else {
                    document.getElementById('<%=txtTotalFare.ClientID %>').value = '';
                }

            }

            if (str != "") {
                alert(str);
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#addperdime').click(function () {
                $('.addperdime').toggleClass("shows");
            });
            $('#oed').click(function () {
                $('.oed').toggleClass("shows");
            })
        })
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="frmTravelMatrixWithClusters_pages">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12 sticky_fl ">
                        <div class="sticky-save"> 
                               <asp:Button ID="Button2" class="btn btn-success" style="margin-right:10px"   Text="Save" runat="server" OnClick="btnSave_Click" OnClientClick="return checkdataMain();"></asp:Button>
     
                            <asp:Button ID="btnAdd" class="btn btn-light "  Text="Back" runat="server" OnClick="btnAdd_Click"></asp:Button> 
                            
                                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                                <div class="panel panel-default">
                                    <div class="panel-heading " role="tab" id="headingOne">

                                        <h4 class="panel-title" style="font-weight: bold">

                                            <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">Travel Detail     </a>
                                        </h4>

                                    </div>

                                    <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei">Travel Type:<span style="color: Red">*</span></label>
                                                        <div class="col-sm-9 padd">
                                                            <asp:DropDownList ID="ddlType" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" runat="server"
                                                                class="form-control " />

                                                            <asp:Button ID="btnSubmit" style="display:none"  Text=""  runat="server" OnClick="Submit" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei">Date:<span style="color: Red">*</span></label>
                                                        <div class="col-sm-9 padd">
                                                        
                                                            <asp:TextBox ID="txtDate" runat="server" class="form-control" OnTextChanged="txtStartDate__Change" AutoPostBack="true" autocomplete="off" ondrop="return false;" onkeypress="return false;">      </asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarExtenderTourdate"
                                                                runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight">
                                                            </ajax:CalendarExtender>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei">Travel Start Time:<span style="color: Red">*</span></label>
                                                        <div class="col-sm-9 padd input-group date">
                                                            <asp:TextBox ID="txtSTime" runat="server" onKeypress="loadJSFunction1()" autocomplete="off" ondrop="return false;"  CssClass="form-control" />
                                                            <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei">Travel End Time:<span style="color: Red">*</span></label>
                                                        <div class="col-sm-9 padd input-group date">
                                                            <asp:TextBox ID="txtTTime" runat="server" autocomplete="off" ondrop="return false;" onkeypress="return false;" CssClass="form-control" />
                                                            <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>
                                                            <span style="width: 5px; float: right; margin: -5px -5px; font-size: 21px;">
                                                                <asp:CompareValidator ID="CompareValidator3chkout" runat="server" ControlToCompare="txtSTime"
                                                                    ControlToValidate="txtTTime" ValidationGroup="Main" Display="Dynamic" ErrorMessage="*"
                                                                    ForeColor="Red" Operator="GreaterThanEqual" Type="String"> </asp:CompareValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei">Travel Start Place:<span style="color: Red">*</span></label>
                                                        <div class="col-sm-9 padd">
                                                            <asp:Label ID="lbllblVillageStart" ForeColor="Black" runat="server"></asp:Label>
                                                            <asp:DropDownList ID="ddlFromVillage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStart_SelectedIndexChanged"
                                                                class="form-control " />

                                                        <asp:Button ID="btnStart" class="btn btn-primary pull-right" Text="Add" Visible="false" runat="server" OnClick="btnStart_click"></asp:Button>
                                                    
                                                            
                                                            <asp:Label ID="lblEditUUniqecode" Style="display: none;" Visible="false" ForeColor="Black" runat="server"></asp:Label>
                                                            <asp:Label ID="lblPerDim" Style="display: none;" Visible="false" ForeColor="Black" runat="server"></asp:Label>
                                                             <asp:Label ID="lblyear" Style="display: none;" Visible="false" ForeColor="Black" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei">Travel End Place:<span style="color: Red">*</span></label>
                                                        <div class="col-sm-9 padd">
                                                            <asp:Label ID="lblVillageEnd" ForeColor="Black" runat="server"></asp:Label>
                                                            <asp:DropDownList ID="ddlEndVillage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlend_SelectedIndexChanged"
                                                                class="form-control " />
                                                              <asp:Button ID="btnend" class="btn btn-primary pull-right" Text="Add" Visible="false" runat="server" OnClick="btnend_click"></asp:Button>
                                                          
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" runat="server" id="divMode">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei">Mode of Travel:<span style="color: Red">*</span></label>
                                                        <div class="col-sm-9 padd">
                                                            <asp:DropDownList ID="ddlMode" AutoPostBack="true" OnSelectedIndexChanged="Mode_SelectedIndexChanged" runat="server"
                                                                class="form-control " />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei">Travel Objective:<span style="color: Red">*</span></label>
                                                        <div class="col-sm-9 padd">
                                                            <asp:TextBox ID="txtObjective" runat="server" onkeypress="return onlyAlphabets(event,this);" autocomplete="off" ondrop="return false;" MaxLength="50" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei">Distance(in KM):</label>
                                                        <div class="col-sm-9 padd">
                                                            <asp:TextBox ID="txtKM" autocomplete="off"  AutoPostBack="true" OnTextChanged="txt_kmclick" onkeypress="return isNumberKey(this,event);" ondrop="return false;" MaxLength="3" runat="server" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei">Travel Fare:<span style="color: Red">*</span></label>
                                                        <div class="col-sm-9 padd">
                                                            <asp:TextBox ID="txtTotalFare" MaxLength="4" onkeypress="return isNumberKey(this,event);" autocomplete="off" ondrop="return false;" runat="server" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" runat="server" id="divExpense" style="display:none">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei">Travel Fare Receipt:<span style="color: Red">*</span></label>
                                                        <div class="col-sm-8 padd">
                                                            <asp:FileUpload ID="Fileupload2"   onchange="return Imageuploaddata(this.id,'1');" runat="server" class="form-control" Font-Size="Smaller"
                                                                TabIndex="16" />

                                                        </div>
                                                        <div class="col-sm-1 padd">
                                                            <asp:ImageButton ID="lnkMain" OnClick="ImgDownloadMani_Click" Visible="false" ToolTip="Show" runat="server" class="btn btn-danger btn-paddd"
                                                                BackColor="#f1f1f1" ImageUrl="~/images/download.png"
                                                                Height="25px" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei">Edit Reason:<span style="color: Red">*</span></label>
                                                        <div class="col-sm-9 padd">
                                                            <asp:TextBox ID="txtRemark" runat="server" onkeypress="return onlyAlphabets(event,this);" autocomplete="off" ondrop="return false;" MaxLength="50" class="form-control" />
                                                            <asp:HiddenField runat="server" ID="hndMaxamt" />
                                                        </div>
                                                    </div>
                                                </div>


                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="oed" runat="server" id="divcityType">
                                <div class="panel panel-default">
                                    <div class="panel-heading " role="tab" id="headingFive">
                                        <h4 class="panel-title">
                                            <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseFive" aria-expanded="true" aria-controls="collapseTwo">Per Diem
                                    <span></span>
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="collapseFive" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingFive">
                                        <div class="panel-body">
                                            <div class="row">

                                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12" runat="server">
                                                    <div class="form-group"  style="margin: 0px;margin-left: 25px;">
                                                       <%-- <label for="email" class="col-sm-4 padd linhei">Last Entry :<span style="color: Red">*</span></label>
                                                        <div class="col-sm-6 padd">--%>
                                                            <p class="text-danger" style="margin: 0px; font-weight: bold;">
                                                                    <asp:CheckBox CssClass="checkbox" ID="chkENtry" AutoPostBack="true" OnCheckedChanged="chkEnty_click"  runat="server" />Mark this as today's last visit
                                                        </p>
                                               
                                                        <%--</div>--%>
                                                       
                                                    </div>
                                                </div>

                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" runat="server">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-4 padd linhei">City Type :<span style="color: Red">*</span></label>
                                                        <div class="col-sm-8 padd">
                                                            <asp:DropDownList ID="ddlcity" runat="server"
                                                                class="form-control " AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlCite_SelectedIndexChanged" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12 " runat="server">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-4 padd linhei">Meal Arrangement by EG :<span style="color: Red">*</span></label>
                                                        <div class="col-sm-8 padd">
                                                            <asp:DropDownList ID="ddlMealArrangement" AutoPostBack="true" OnSelectedIndexChanged="ddlMealArrangement_SelectedIndexChanged" runat="server"
                                                                class="form-control " />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" runat="server"  visible="false">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-4 padd linhei">Per Diem :<span style="color: Red">*</span></label>
                                                        <div class="col-sm-8 padd">
                                                            <asp:TextBox ID="txtPerDim" runat="server" Enabled="false"
                                                                class="form-control " />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="oed" runat="server" id="div1">
                                <div class="panel panel-default ">
                                    <div class="panel-heading " role="tab" id="headingTwo">
                                        <h4 class="panel-title">
                                            <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="true" aria-controls="collapseTwo">Accommodation Details
                                    <span></span>
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="collapseTwo" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingTwo">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="Useaccommodation" class="col-sm-7 padd linhei">Use of Accommodation:<span style="color: Red">*</span></label>
                                                        <div class="col-sm-5 padd">
                                                            <asp:RadioButtonList ID="rblDist" class="radio-inline asd" Style="margin-left: -116px" AutoPostBack="true" OnSelectedIndexChanged="rblDist_SelectedIndexChanged" runat="server" RepeatColumns="2" RepeatLayout="Flow" RepeatDirection="Horizontal" CssClass="radioButtonList">
                                                                <asp:ListItem Text="Yes" class="radio-inline asd" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="No" class="radio-inline asd" Value="2"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:Panel runat="server" ID="pnlAcc1" Enabled="false">
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" runat="server" id="divgtpye">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-4 padd linhei">Accommodation Type:<span style="color: Red">*</span></label>
                                                            <div class="col-sm-8 padd">
                                                                <asp:DropDownList ID="ddlgusttype" AutoPostBack="true" OnSelectedIndexChanged="ddlgusttype_SelectedIndexChanged" runat="server"
                                                                    class="form-control " />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel runat="server" ID="pnlAcc" Enabled="false">
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-4 padd linhei">Payment Type:<span style="color: Red">*</span></label>
                                                            <div class="col-sm-8 padd">
                                                                <asp:DropDownList ID="ddlPayment" AutoPostBack="true" OnSelectedIndexChanged="ddlccce_SelectedIndexChanged" runat="server"
                                                                    class="form-control ">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" runat="server">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-4 padd linhei">Occupancy:<span style="color: Red">*</span></label>
                                                            <div class="col-sm-8 padd">
                                                                <asp:DropDownList ID="ddlOccupancy" runat="server"
                                                                    class="form-control ">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" runat="server">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-4 padd linhei">Accommodation Fare:<span style="color: Red">*</span></label>
                                                            <div class="col-sm-8 padd">
                                                                <asp:TextBox ID="txthoserent" autocomplete="off" onchange="return CheckMaxAmount();" onkeypress="return isNumberKey(this,event);" ondrop="return false;" MaxLength="4" runat="server" class="form-control" />

                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-4 padd linhei">Fare Receipt:<span style="color: Red">*</span></label>
                                                            <div class="col-sm-7 padd">

                                                                <asp:FileUpload ID="Fileupload1" runat="server" Enabled="false" onchange="ImageuploaddataGust(this.id)" class="form-control" Font-Size="Smaller" />

                                                            </div>
                                                            <div class="col-sm-1 padd">
                                                                <asp:ImageButton ID="ImageButton1" OnClick="ImgDownloadAcc_Click" Visible="false" ToolTip="Show" runat="server" class="btn btn-danger btn-paddd"
                                                                    BackColor="#f1f1f1" ImageUrl="~/images/download.png"
                                                                    Height="25px" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="oed" runat="server" id="div2">
                                <asp:Panel ID="pnlvMain" runat="server" Visible="false">
                                    <div class="panel panel-default">
                                        <div class="panel-heading " role="tab" id="headingThree">
                                            <h4 class="panel-title">
                                                <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="true" aria-controls="collapseThree">Local Conveyance
                                     <span></span>
                                                </a>
                                            </h4>
                                        </div>
                                        <asp:UpdatePanel ID="Upghganel1" UpdateMode="Always" runat="server">
                                            <ContentTemplate>
                                                <div id="collapseThree" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingThree">
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                                <div class="form-group">
                                                                    <label for="email" class="col-sm-5 padd linhei">Use of Local Conveyance:</label>
                                                                    <div class="col-sm-6 padd">
                                                                        <div class="col-sm-5 padd">
                                                                            <asp:RadioButtonList ID="rblDist1" Style="margin-left: -65px" class="radio-inline asd" AutoPostBack="true" OnSelectedIndexChanged="rblDist1_SelectedIndexChanged" runat="server" RepeatColumns="2" RepeatLayout="Flow" RepeatDirection="Horizontal" CssClass="radioButtonList">
                                                                                <asp:ListItem Text="Yes" class="radio-inline asd" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="No" class="radio-inline asd" Value="2"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <asp:Panel ID="pndVic" runat="server" Enabled="false">
                                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                                    <div class="form-group">
                                                                        <label for="email" class="col-sm-3 padd linhei">Conveyance Type:<span style="color: Red">*</span></label>
                                                                        <div class="col-sm-9 padd">
                                                                            <asp:DropDownList ID="ddlvehicle" runat="server"
                                                                                class="form-control ">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                                    <div class="form-group">
                                                                        <label for="email" class="col-sm-3 padd linhei">Description:<span style="color: Red">*</span></label>
                                                                        <div class="col-sm-9 padd">
                                                                            <asp:TextBox ID="txtdes" onkeypress="return onlyAlphabets(event,this);" runat="server" autocomplete="off" ondrop="return false;" MaxLength="50" class="form-control" />

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                                    <div class="form-group">
                                                                        <label for="email" class="col-sm-3 padd linhei">Conveyance Fare:<span style="color: Red">*</span></label>
                                                                        <div class="col-sm-9 padd">
                                                                            <asp:TextBox ID="txtVIcRent" autocomplete="off" onkeypress="return isNumberKey(this,event);" ondrop="return false;" MaxLength="4" runat="server" class="form-control" />

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                                    <div class="form-group">
                                                                        <label for="email" class="col-sm-3 padd linhei">Fare Receipt:</label>
                                                                        <div class="col-sm-9 padd">
                                                                            <asp:FileUpload ID="FileuploadExpensevehicle" runat="server" class="form-control" Font-Size="Smaller"
                                                                                TabIndex="16" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                                    <div class="form-group">
                                                                        <label for="email" class="col-sm-3 padd linhei"></label>
                                                                        <div class="col-sm-9 padd">
                                                                            <asp:Button ID="btnAddVehicle"  class="btn btn-primary" OnClientClick="return checkdataVic();" OnClick="btnAdd_Vehicle" Text="Add Local Conveyance" ValidationGroup="Main" runat="server"></asp:Button>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="panel-Conveyance">
                                                            <h4>Local Conveyance</h4>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="Row" style="width: 100%">
                                                                        <div class="Row WrapText table-responsive" style="height: auto; overflow: auto; width: 100%;" align="center">
                                                                            <asp:GridView ID="gvVehicle" OnRowDataBound="gvVehicle_OnRowCommand" runat="server" DataKeyNames="UniqueCode,UniqueChildRCode" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" Font-Names="Arial"
                                                                                Font-Size="12px" Width="100%">
                                                                                <EmptyDataTemplate>
                                                                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                        Data not found
                                                                                    </div>
                                                                                </EmptyDataTemplate>
                                                                                <HeaderStyle BackColor="#838383" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                <Columns>

                                                                                    <asp:TemplateField HeaderText="Conveyance Type" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblUserName" ForeColor="Black" runat="server"
                                                                                                Text='<%# Eval("VehicletypeName") %>'></asp:Label>
                                                                                            <asp:Label ID="lblUniqueCode" ForeColor="Black" Visible="false" runat="server"
                                                                                                Text='<%# Eval("UniqueCode") %>'></asp:Label>
                                                                                            <asp:Label ID="lblUniqueChildRCode" ForeColor="Black" Visible="false" runat="server"
                                                                                                Text='<%# Eval("UniqueChildRCode") %>'></asp:Label>
                                                                                            <asp:Label ID="lblVehicletypeID" ForeColor="Black" Visible="false" runat="server"
                                                                                                Text='<%# Eval("VehicletypeID") %>'></asp:Label>

                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Description" Visible="true">
                                                                                        <ItemTemplate>

                                                                                            <asp:Label ID="lblDescription" ForeColor="Black" runat="server"
                                                                                                Text='<%# Eval("VehicleDescription") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Conveyance Fare" Visible="true">
                                                                                        <ItemTemplate>

                                                                                            <asp:Label ID="lblRent" ForeColor="Black" runat="server"
                                                                                                Text='<%# Eval("VehicleAmout") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Fare Receipt" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="lnkd" ToolTip="Show" runat="server" class="btn btn-danger btn-paddd"
                                                                                                BackColor="#f1f1f1" OnClick="ImgDownloadV_Click" ImageUrl="~/images/download.png"
                                                                                                Height="25px" />
                                                                                            <asp:Label ID="lblImagePath" Visible="false" ForeColor="Black" runat="server"
                                                                                                Text='<%# Eval("ImagePath") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Action" Visible="true">
                                                                                        <ItemTemplate>

                                                                                            <asp:ImageButton ID="LinkButton1" OnClick="LnkBtnBlock_OnClick1" ImageUrl="~/images/edit.png" runat="server"></asp:ImageButton>

                                                                                        </ItemTemplate>

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Delete">
                                                                                        <ItemTemplate>

                                                                                            <asp:LinkButton ID="Delete_Questionttt" OnClientClick="javascript:return confirm('Are you sure you want to delete this record?');" OnClick="Delete_Question_Click3" class="btn btn-sm btn-link" runat="server">
                                                                     <span class="glyphicon glyphicon-trash" data-fa-transform="shrink-10 up-.5" style="color:red"></span>
                                                                        
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />
                                                                                    </asp:TemplateField>

                                                                                </Columns>

                                                                            </asp:GridView>
                                                                            <asp:Label ID="lblImagePathVe" Visible="false" ForeColor="Black" runat="server"></asp:Label>

                                                                            <asp:HiddenField Value="" ID="lblUniqueCodeVe" runat="server" />
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnAddVehicle" />
                                                <asp:PostBackTrigger ControlID="gvVehicle" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="oed">
                                <div class="panel panel-default">
                                    <div class="panel-heading " role="tab" id="headingfour">
                                        <h4 class="panel-title">
                                            <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapsefour" aria-expanded="true" aria-controls="collapsefour">Other Expense Details
                                    <span></span>
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="collapsefour" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingfour">
                                        <asp:UpdatePanel ID="uhh" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei">Amount:<span style="color: Red">*</span></label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:TextBox ID="txtTrotalAmout" onkeypress="return isNumberKey(this,event);" MaxLength="3" autocomplete="off" ondrop="return false;" runat="server" class="form-control" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei">Other Expanse Detail:<span style="color: Red">*</span></label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:TextBox ID="txtExpense" MaxLength="50" autocomplete="off" onkeypress="return onlyAlphabets(event,this);" ondrop="return false;" runat="server" class="form-control" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei">Expense receipt:<span style="color: Red">*</span></label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:FileUpload ID="FileuploadAttach" runat="server" class="form-control" Font-Size="Smaller"
                                                                        TabIndex="16" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei"></label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:Button ID="btnExpense"  class="btn btn-primary" Style="margin-top: -7px; margin-left: -47px;" OnClick="BtnEntry_Click" OnClientClick="return checkdata();" Text="Add Expense" ValidationGroup="Main" runat="server"></asp:Button>
                                                                </div>
                                                            </div>
                                                        </div>


                                                    </div>
                                                    <div class="panel-Conveyance">
                                                        <h4>Other Expense Summary</h4>
                                                        <div class="row">
                                                            <div class="col-sm-12">

                                                                <div class="Row" style="width: 100%">
                                                                    <div class="Row WrapText table-responsive" style="height: auto; overflow: auto; width: 100%;" align="center">
                                                                        <asp:GridView ID="gvExpens" OnRowDataBound="gvExpens_OnRowCommand" runat="server" DataKeyNames="UniqueCode,UniqueChildRCode" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" Font-Names="Arial"
                                                                            Font-Size="12px" Width="100%">
                                                                            <EmptyDataTemplate>
                                                                                <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                    Data not found
                                                                                </div>
                                                                            </EmptyDataTemplate>
                                                                            <HeaderStyle BackColor="#838383" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                            <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                            <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                            <Columns>

                                                                                <asp:TemplateField HeaderText="Other Expanse Detail" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblExpensedetails" ForeColor="Black" runat="server"
                                                                                            Text='<%# Eval("Expensedetails") %>'></asp:Label>
                                                                                        <asp:Label ID="lblUniqueCode" ForeColor="Black" Visible="false" runat="server"
                                                                                            Text='<%# Eval("UniqueCode") %>'></asp:Label>
                                                                                        <asp:Label ID="lblUniqueChildRCode" ForeColor="Black" Visible="false" runat="server"
                                                                                            Text='<%# Eval("UniqueChildRCode") %>'></asp:Label>


                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="padding-lef" />

                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Amount" Visible="true">
                                                                                    <ItemTemplate>

                                                                                        <asp:Label ID="lblTotalAmount" ForeColor="Black" runat="server"
                                                                                            Text='<%# Eval("TotalAmount") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="padding-lef" />

                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Expense receipt" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="lnkd" ToolTip="Show" runat="server" class="btn btn-danger btn-paddd"
                                                                                            BackColor="#f1f1f1" OnClick="ImgDownload_Click" ImageUrl="~/images/download.png"
                                                                                            Height="25px" />
                                                                                        <asp:Label ID="lblImagePath" Visible="false" ForeColor="Black" runat="server"
                                                                                            Text='<%# Eval("ImagePath") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="padding-lef" />

                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Action" Visible="true">
                                                                                    <ItemTemplate>

                                                                                        <asp:ImageButton ID="LinkButton1" OnClick="LnkBtnBlock_OnClick" ImageUrl="~/images/edit.png" runat="server"></asp:ImageButton>

                                                                                    </ItemTemplate>

                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Delete">
                                                                                    <ItemTemplate>

                                                                                        <asp:LinkButton ID="Delete_Questionttt" OnClientClick="javascript:return confirm('Are you sure you want to delete this record?');" OnClick="Delete_Question_Click2" class="btn btn-sm btn-link" runat="server">
                                     <span class="glyphicon glyphicon-trash" data-fa-transform="shrink-10 up-.5" style="color:red"></span>
                                        
                                                                                        </asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="padding-lef" />
                                                                                </asp:TemplateField>
                                                                            </Columns>

                                                                        </asp:GridView>

                                                                        <asp:Label ID="lblImagePathEx" Visible="false" ForeColor="Black" runat="server"></asp:Label>

                                                                        <asp:HiddenField Value="" ID="lblUniqueCodeEx" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <%--<table class="table table-striped table-bordered">
    <thead>
        <tr>
            <th>Details of expenses</th>
            <th>Total amount</th>
            <th>Expense receipt</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Auto</td>
            <td>200</td>
            <td><a href="#"><i class="fa fa-arrow-circle-o-down" aria-hidden="true"></i></a></td>
        </tr>
        <tr>
            <td>Auto</td>
            <td>200</td>
            <td><a href="#"><i class="fa fa-arrow-circle-o-down" aria-hidden="true"></i></a></td>
        </tr>
        <tr>
            <td>Auto</td>
            <td>200</td>
            <td><a href="#"><i class="fa fa-arrow-circle-o-down" aria-hidden="true"></i></a></td>
        </tr>
    </tbody>
</table>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnExpense" />

                                                <asp:PostBackTrigger ControlID="gvExpens" />



                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <ajax:ModalPopupExtender ID="ModelVillageSelect" runat="server" BackgroundCssClass="modalBg "
                    CancelControlID="I_C" PopupControlID="PnlVillageSelect" TargetControlID="HdnVillageSelect">
                </ajax:ModalPopupExtender>
                <asp:HiddenField ID="HdnVillageSelect" runat="server"></asp:HiddenField>
                <asp:Panel CssClass="model-wid mod-posi delete_pop" Style="display: none; width:40% !important"
                    ID="PnlVillageSelect" runat="server">
                    <div class="modal-pop">
                        <div class="modal-header" style="background-color: #ddd; color: #000;">
                            <div style="display: flex; justify-content: space-between; align-items: center">
                                <h4 class="modal-title" style="color: #000">Add Village</h4>
                                <div>
                                    <asp:ImageButton ID="I_C" ImageUrl="~/images/close-29.png" runat="server" Text="Close"
                                        ToolTip="Close" Style="float: none;"></asp:ImageButton>
                                </div>
                            </div>
                        </div>
                        <div class="modal-body" style="display: flex ; flex-flow: column; justify-content: center; align-items: center;">
                            <asp:Label ID="Label7" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                            <%-- <div class="form-horizontal" role="form">--%>
                            <div class="form-group row">
                                <asp:Label ID="Lbl_s" class="control-label col-sm-4 lab-text-left" runat="server"
                                    Text="Date:">Type:<span style="color:Red">*</span>
                                </asp:Label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_S" runat="server" OnSelectedIndexChanged="ddl_S_SelectedIndexChanged"
                                        AutoPostBack="true" class="form-control ">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblSflag" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group row">
                                <asp:Label ID="LBl_D" class="control-label col-sm-4 lab-text-left" runat="server"
                                    Text="">District:<span style="color:Red">*</span></asp:Label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_D" runat="server" OnSelectedIndexChanged="ddl_D_SelectedIndexChanged"
                                        AutoPostBack="true" class="form-control ">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <asp:Label ID="Lbl_B" class="control-label col-sm-4 lab-text-left" runat="server">Block:<span style="color:Red">*</span></asp:Label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_B" runat="server" OnSelectedIndexChanged="ddl_B_SelectedIndexChanged"
                                        AutoPostBack="true" class="form-control ">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <asp:Label ID="Lbl_C" class="control-label col-sm-4 lab-text-left" runat="server"
                                    Text="Cluster:"></asp:Label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_C" runat="server" OnSelectedIndexChanged="ddl_C_SelectedIndexChanged"
                                        AutoPostBack="true" class="form-control ">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <asp:Label ID="Lbl_V" class="control-label col-sm-4 lab-text-left" runat="server"
                                    Text="Village:"></asp:Label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_V" runat="server" class="form-control ">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row" visible="false"  runat="server" id="divCitey">
                                <asp:Label ID="Label3" class="control-label col-sm-4 lab-text-left" runat="server"
                                    Text="Tier Type:"></asp:Label>
                                <div class="col-sm-8">
                                             <asp:DropDownList ID="ddlCityTpyeID" runat="server"
                                       class="form-control " AutoPostBack="true"  OnSelectedIndexChanged="divCitey_SelectedIndexChanged" />
                                                        
                                </div>
                            </div>
                            <div class="form-group row"  visible="false"  runat="server" id="dOtherCitey">
                                <asp:Label ID="Label4" class="control-label col-sm-4 lab-text-left" runat="server"
                                    Text="City:"></asp:Label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlCityloction" runat="server" class="form-control ">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group row" runat="server" visible="false" id="divother">
                                <asp:Label ID="Label1" class="control-label col-sm-4 lab-text-left" runat="server"
                                    Text="Other Place:"></asp:Label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtOtherPlace" onkeypress="return onlyAlphabets(event,this);" runat="server" class="form-control ">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row" runat="server" visible="false" id="divDetail">
                                <asp:Label ID="Label2" class="control-label col-sm-4 lab-text-left" runat="server"
                                    Text="Detail of visited place:"></asp:Label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtDeatils" onkeypress="return onlyAlphabets(event,this);" runat="server" class="form-control ">
                                    </asp:TextBox>
                                </div>
                            </div>
                             
                           <div class="form-group row mb-0" >
                               <div class="col-md-4">

                               </div>
                                <div class="col-md-8">
                               
                                    <asp:Button ID="I_S"  class="btn btn-success" Text="Save" OnClick="btnSave_village" runat="server" ToolTip="Save"></asp:Button>
                               
                                </div>
                            </div>
                        </div>
                    </div>

                </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkMain" />
            <asp:PostBackTrigger ControlID="ImageButton1" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>





