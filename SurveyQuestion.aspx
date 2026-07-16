<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SurveyQuestion.aspx.cs" Inherits="SurveyQuestion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/jquery-2.1.1.min.js"></script>
    <%--<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-ui-timepicker-addon/1.6.3/jquery-ui-timepicker-addon.min.js"></script>--%>
    <script type="text/javascript" src="js/JS/jquery-ui-timepicker-addon.min.js"></script>

    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>--%>
    <%--<script type="text/javascript" src="js/JS/jquery-1.12.4.min.js"></script>--%>

    <%--     <link href="css/Admin.css" type="text/css" rel="stylesheet" />
    <link href="css/maincss.css" rel="stylesheet" type="text/css" />
      <%--  <link href="css/Site.css" rel="stylesheet" type="text/css" />--%>
    <%--    <script src="js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="js/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="js/jquery-1.9.1.js" type="text/javascript"></script>--%>--%>

         <script type="text/javascript">
       function Search_Gridview3(strKey, strGV) {
             debugger;

             var strData = strKey.value.toLowerCase().split(" ");
           var tblData = document.getElementById("ctl00_MainContent_GVANs");
             var rowData;
             for (var i = 1; i < tblData.rows.length; i++) {
                 rowData = tblData.rows[i].innerHTML;
                 var styleDisplay = 'none';
                 for (var j = 0; j < strData.length; j++) {
                     if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                         styleDisplay = '';
                     else {
                         styleDisplay = 'none';
                         break;
                     }
                 }
                 tblData.rows[i].style.display = styleDisplay;
             }
         }
         </script>
    <style type="text/css">
        .modalBackground {
            background-color: rgba(0,0,0,0.5);
        }

        .checkbox, .radio {
            position: relative;
            display: block;
            margin-top: 10px;
            margin-bottom: 10px;
        }

        th {
            text-align: center;
        }

        .input, button, select, textarea {
            font-family: inherit;
            font-size: inherit;
            line-height: 20px;
        }

        .butt_new_grid1 {
            border: 1px solid #08c !important;
            padding: 3px 10px !important;
            border-radius: 6px !important;
            color: #fff !important;
            margin-top: 3px !important;
            line-height: 28px !important;
            background: linear-gradient(to bottom, #87e0fd 0%,#53cbf1 40%,#05abe0 100%);
        }


            .butt_new_grid1:hover {
                /* Permalink - use to edit and share this gradient: http://colorzilla.com/gradient-editor/#05abe0+0,53cbf1+40,87e0fd+100 */
                background: #05abe0; /* Old browsers */
                background: -moz-linear-gradient(top, #05abe0 0%, #53cbf1 40%, #87e0fd 100%); /* FF3.6-15 */
                background: -webkit-linear-gradient(top, #05abe0 0%,#53cbf1 40%,#87e0fd 100%); /* Chrome10-25,Safari5.1-6 */
                background: linear-gradient(to bottom, #05abe0 0%,#53cbf1 40%,#87e0fd 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
                filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#05abe0', endColorstr='#87e0fd',GradientType=0 ); /* IE6-9 */
                color: #ddd;
            }


        .Mpopup {
            position: relative;
            background: #f2f2f2;
            color: #404040;
            text-shadow: 0 1px 0 #fff;
            -ms-filter: "progid:DXImageTransform.Microsoft.dropshadow(OffX=0,OffY=1,Color=#ffffffff,Positive=true)";
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=0, OffY=1, Color=#ffffffff, Positive=true);
            border-radius: 5px;
            box-shadow: 0px 1px 4px rgba(0, 0, 0, 0.1);
            padding: 5px;
            font-size: 12px;
            height: auto !important;
            z-index: 100007 !important;
        }

        .Mpopup1 {
            position: relative;
            background: #f2f2f2;
            color: #404040;
            text-shadow: 0 1px 0 #fff;
            -ms-filter: "progid:DXImageTransform.Microsoft.dropshadow(OffX=0,OffY=1,Color=#ffffffff,Positive=true)";
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=0, OffY=1, Color=#ffffffff, Positive=true);
            border-radius: 5px;
            box-shadow: 0px 1px 4px rgba(0, 0, 0, 0.1);
            padding: 5px;
            font-size: 12px;
            height: 365px !important;
            z-index: 1350px0001 !important;
        }

        .Mpopupnewline {
            border-top: 2px solid #105f77;
            width: 100%;
            height: 4px;
        }

        .Mpopupheader {
            width: 100%;
            background-color: #454545;
            height: 25px;
            font-size: 12px;
            font-weight: 500;
            color: #f2f2f2;
            text-shadow: 0 1px 0 #add553;
            -ms-filter: "progid:DXImageTransform.Microsoft.dropshadow(OffX=0,OffY=1,Color=#ffffffff,Positive=true)";
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=0, OffY=1, Color=#ffffffff, Positive=true);
            padding: 5px;
        }

        .Mpopupbodycontent {
            width: 100%;
            margin: 3px 0 3px 0
        }

        .Mpopupfooter {
            width: 100%;
            background-color: #454545;
            padding: 3px
        }

        .Requiredvalidate {
            font-size: 12px;
            color: Red;
        }


        .ModalPopupBG {
            background-color: #000000;
            filter: alpha(opacity=80);
            -moz-opacity: 0.5;
            -khtml-opacity: 0.5;
            opacity: 0.5;
            width: 100%;
            height: 100%
        }

        .ModalPopupBGmainentry {
            background-color: #000000;
            filter: alpha(opacity=10);
            -moz-opacity: 1.0;
            -khtml-opacity: 1.0;
            opacity: 1.0;
            width: 100%;
            height: 100%
        }

        table#WebSurtte tr td {
            font-weight: 400;
            font-size: 14px;
        }

        tr.header td table tr td.fs {
            font-size: 14px;
        }
    </style>

    <script type="text/javascript" language="javascript">
        $(document).on('click', '.chkQCSelectAll', function () {
            if ($(this).find('input').is(':checked')) {
                $('.chkQCFormQues input').prop('checked', true);
            }
            else {
                $('.chkQCFormQues input').prop('checked', false);
            }
        })

        $(document).on('click', '.chkSQSelectAll', function () {
            if ($(this).find('input').is(':checked')) {
                $('.chkSQFormQues input').prop('checked', true);
            }
            else {
                $('.chkSQFormQues input').prop('checked', false);
            }
        })

    </script>
    <script type="text/javascript" language="javascript">

        function NewTabPreView() {
            var panel = document.getElementById("<%=ddlForm.ClientID %>");
            window.open(
                "https://pms.educategirls.ngo/SurveyAnstest.aspx?ID=" + panel.value + "", "_blank");
        }

        function Imageuploaddata(textid) {
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
                    url: 'HandlerImageSurvey.ashx',
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


                return true;
            }
        }
        function PrintPanel() {
            var panel = document.getElementById("<%=panel.ClientID %>");
            document.getElementById("<%=panel.ClientID %>").style.display = "block";
            var printWindow = window.open('', '', 'height=620,width=1000');

            printWindow.document.write(panel.innerHTML);

            printWindow.document.close();
            setTimeout(function () {
                document.getElementById("<%=panel.ClientID %>").style.display = "none";
                printWindow.print();
            }, 2000);

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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="updatePnlX" runat="server">
        <ContentTemplate>
            <div class="col-lg-12" style="margin-top: -18px;">
                <div class="panel panel-default" style="padding-bottom: 0px !important;">
                    <div class="panel-heading" style="padding: 5px 15px;">
                        <h3 class="text-danger" style="margin: 0px;">Assessment Question Master
                        </h3>
                    </div>
                    <div class="row" style="margin-top: 8px;">
                        <asp:Panel ID="pnlQ" runat="server" >
                        <div id="Activity" runat="server" class="col-lg-4 col-md-4 col-xs-12 scroll" style="padding-right: 0px; padding-top: 5px;">
                            <div class="panel panel-default" style="margin-bottom: 0px;">
                                <div class="panel-heading" style="padding-left: 15px;">
                                    <p class="text-danger" style="margin: 0px;">

                                        <asp:Label ID="lblHeadingTwo" runat="server" Text="Add/Edit Question"></asp:Label>

                                        <asp:LinkButton ID="LinkButton1" runat="server" Style="margin-top: -21px; margin-left: 300px;" TabIndex="4" class="btn btn-sm btn-primary" OnClick="LinkButton1_Click">Add Question</asp:LinkButton>

                                                
                                    </p>
                                </div>
                                <div class="panel-body scroll" style="min-height: 510px; max-height: 510px; overflow-y: scroll;">

                                    <div class="row form-group">

                                        <label class="control-label col-sm-5">
                                            Assessment Name:</label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ddlQuestionForm" runat="server" Enabled="false" CssClass="form-control input-sm">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlQuestionForm"
                                                Display="Dynamic" InitialValue="0" ErrorMessage="Please select Form" ForeColor="Red"
                                                SetFocusOnError="True" ValidationGroup="FormName">* </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlQuestionForm"
                                                Display="Dynamic" InitialValue="0" ErrorMessage="Please select Form" ForeColor="Red"
                                                SetFocusOnError="True" ValidationGroup="QuestionCreate">* </asp:RequiredFieldValidator>

                                            <asp:HiddenField ID="HFQuestionID" runat="server" />
                                        </div>


                                    </div>

                                    <div class="row form-group">
                                        <label class="control-label col-sm-5">
                                            Category : <span style="color: Red">*</span></label>
                                        <div class="col-sm-7">
                                             <asp:DropDownList ID="ddlMainCategory" runat="server"  TabIndex="5"  CssClass="form-control input-sm">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlMainCategory"
                                                Display="Dynamic" InitialValue="0" ErrorMessage="*" ForeColor="Red"
                                                SetFocusOnError="True" ValidationGroup="FormName">* </asp:RequiredFieldValidator>
                                        </div>


                                    </div>

                                    <div class="row form-group">
                                        <label class="control-label col-sm-5">
                                            Question No: <span style="color: Red">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtquestionno" runat="server" MaxLength="500"  TabIndex="6"  CssClass="form-control input-sm">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtquestionno" Display="Dynamic" ErrorMessage="Please enter question no" ForeColor="Red" SetFocusOnError="True" ValidationGroup="QuestionCreate">
                                            </asp:RequiredFieldValidator>
                                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtquestionno" FilterType="Numbers">
                                            </asp:FilteredTextBoxExtender>
                                        </div>


                                    </div>

                                    <div class="row form-group" runat="server" visible="false">
                                        <label class="control-label col-sm-5">
                                            Display Sequence: <span style="color: Red">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtSeq" runat="server" TabIndex="7" CssClass="form-control input-sm"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="TextBox1_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtSeq" FilterType="Numbers">
                                            </asp:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSeq" Display="Dynamic" InitialValue="0" ErrorMessage="Please Enter Sequence" ForeColor="Red" SetFocusOnError="True" ValidationGroup="QuestionCreate"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                    <div class="row form-group">
                                        <label class="control-label col-sm-5">
                                            Question Type: <span style="color: Red">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ddlQuestionType"  TabIndex="8"  AutoPostBack="true" OnSelectedIndexChanged="ddlQuestionType_Change"
                                                runat="server" Visible="true" CssClass="form-control input-sm">
                                                <asp:ListItem Selected="True" Value="1">Text</asp:ListItem>
                                                <asp:ListItem Value="2">Image</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>

                                    </div>
                                    <div class="row form-group" runat="server" id="Q1">

                                        <label class="control-label col-sm-5">
                                            Question: <span style="color: Red">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtquestion" runat="server" MaxLength="1000" TextMode="MultiLine" TabIndex="9" CssClass="form-control input-sm"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="row form-group" runat="server" id="Q2" visible="false">

                                        <label class="control-label col-sm-5">
                                            Upload Image: <span style="color: Red">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:FileUpload ID="FileuploadAttach" onchange="return Imageuploaddata(this.id);" TabIndex="10" runat="server" Width="160px" Font-Size="Smaller"
                                                 />
                                            <asp:Image ID="imgMKS" runat="server" Height="80px" Width="100px" BorderColor="Black"
                                                BorderStyle="Ridge" BorderWidth="1px" />

                                        </div>

                                    </div>

                                    <div class="row form-group" id="DependentQuestion" runat="server">
                                        <label class="control-label col-sm-5">
                                            Parent Question: <span style="color: Red">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ddlparentQuest" TabIndex="11" AutoPostBack="true" OnSelectedIndexChanged="ddlparentQuest_Change"
                                                runat="server" Visible="true" CssClass="form-control input-sm">
                                            </asp:DropDownList>

                                        </div>

                                    </div>


                                    <div class="row form-group">
                                        <label class="control-label col-sm-5">Answer Type: <span style="color: Red">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ddlAnswerTypeID"  AutoPostBack="true" TabIndex="12" OnSelectedIndexChanged="ddlAnswerTypeID_Change"
                                                runat="server" Visible="true" CssClass="form-control input-sm">
                                            </asp:DropDownList>

                                            <%--<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ConnectionStrings:DBConnection %>"
                                    ProviderName="System.Data.SqlClient" SelectCommand="select ID,Value  From  MSTCommon where Flag = '6' and LanguageID = 1 order by ID"></asp:SqlDataSource>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAnswerTypeID"
                                                Display="Dynamic" InitialValue="0" ErrorMessage="Please select answer Type" ForeColor="Red" SetFocusOnError="True" ValidationGroup="QuestionCreate">
                                            </asp:RequiredFieldValidator>

                                        </div>

                                    </div>


                                    <div class="row form-group" id="divmask" runat="server">
                                        <label class="control-label col-sm-5">
                                            Mask Validation: <span style="color: Red">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ddlMaskValidation" runat="server" TabIndex="13" CssClass="form-control input-sm"></asp:DropDownList>
                                        </div>
                                    </div>



                                    <div class="row form-group" id="divMaxLenght" runat="server">
                                        <label class="control-label col-sm-5">
                                            Maximum length:
                                        </label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtMaxLenght" runat="server" TabIndex="14" CssClass="form-control input-sm">
                                            </asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="row form-group" id="divMaster" runat="server">
                                        <asp:LinkButton ID="lblddlFlag"  OnClick="lnkbtn1_Click" class="control-label col-sm-5"  runat="server">Select Option source:  <span style="color: Red">*</span></asp:LinkButton>
                                       
                                       <%-- <label class="control-label col-sm-5" id="lblddlFlag" runat="server">
                                            Select Option source: <span style="color: Red">*</span>
                                        </label>--%>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ddlFlag" runat="server" TabIndex="15" AutoPostBack="false" CssClass="form-control input-sm selectpicker">
                                                <asp:ListItem Value="0">------Select------ </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="row form-group">
                                        <div class=" col-sm-offset-5" id="divredirect" runat="server">
                                            <asp:LinkButton ID="lnkbtn" CssClass="btn btn-link" Font-Size="12px" TabIndex="16" ValidationGroup="FormName" runat="server" OnClick="lnkbtn_Click">Define Choices for the Question
                                            </asp:LinkButton>
                                        </div>
                                    </div>


                                    <div class="row form-group">
                                        <label class="control-label col-sm-5" id="lblchkMandatory" runat="server">
                                            Is Question Mandatory:</label>
                                        <div class="col-sm-7">
                                            <asp:CheckBox ID="chkMandatory" runat="server" TabIndex="17" Checked="false" Font-Bold="true" />
                                        </div>
                                    </div>




                                    <div class="row form-group hidden" id="dependquestion">
                                        <div class="col-sm-8">
                                            <label class="control-label col-sm-11">
                                                Is Dependent Question:</label>
                                            <asp:CheckBox ID="chkIsdepQues" runat="server" Checked="false" Font-Bold="true" TabIndex="18" AutoPostBack="true" OnCheckedChanged="chkIsdepQues_Click" Visible="true" />
                                        </div>
                                    </div>



                                    <div class="row form-group" id="div1Grop" runat="server">
                                        <label class="control-label col-sm-5" id="Label7" runat="server">
                                            Group: 
                                        </label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ddlGroup" runat="server" TabIndex="17" AutoPostBack="false" CssClass="form-control input-sm">
                                                <asp:ListItem Value="0">------Select------ </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                  
                                    <div class="row form-group pull-right">
                                        <div class="col-lg-12">
                                            <asp:LinkButton ID="btnSave" runat="server" TabIndex="19"  Visible="false" Width="84px"  class="btn btn-sm btn-primary" OnClick="btnSave_Click" ValidationGroup="QuestionCreate">Save</span></asp:LinkButton>
                                            <asp:LinkButton ID="btnNew" runat="server" TabIndex="20"  Visible="false" Width="84px" class="btn btn-sm btn-primary" OnClick="btnNew_Click">Clear</asp:LinkButton>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                            </asp:Panel>
                        <div id="Project" class="col-lg-8 col-md-8 col-xs-12" style="padding-top: 5px;">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <p class="text-danger text-left" style="margin: 0px;">

                                                <asp:Label ID="lblHeadingOne" Visible="false" runat="server" Text="Assessment Question Master"></asp:Label>

                                            </p>
                                        </div>
                                         <div class="col-md-4 text-right" style="padding: 0px;">
                                            <asp:LinkButton ID="lnkCategory" class="btn btn-sm btn-primary"
                                                runat="server"    OnClick="ddlCategory_SelectedIndexChanged"> Add Question Category </asp:LinkButton>


                                        </div>
                                        <div class="col-md-2 text-right" style="padding: 0px;">
                                            <asp:LinkButton ID="lnkPreview" class="btn btn-sm btn-primary"
                                                runat="server" OnClientClick="NewTabPreView()"><span class="glyphicon glyphicon-eye-open"></span> Preview</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel panel-default" style="min-height: 86px; width: 100%; display: none;">
                                    <div class="form-group">
                                    </div>


                                    <div id="divparentdisplay" runat="server" visible="false">
                                        <asp:Label ID="lblparentdisplay" runat="Server" ForeColor="maroon" Font-Bold="True"></asp:Label>
                                        <asp:HiddenField ID="hdnparentid" runat="server"></asp:HiddenField>
                                    </div>

                                </div>

                                <div style="min-height: 86px; width: 100%; border: 0px solid; padding: 5px;">
                                    <div class="row">

                                <div class="col-sm-3">
                                    <label class="control-label" style="margin-top: 3px; text-align: left;">
                                        Year : <span style="color: Red">*</span></label>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control input-sm"  Style="margin-top: 2px" AutoPostBack="true"  OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">    
                                    </asp:DropDownList>
                                    <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="$ConnectionStrings:DBConnection"
                                                ProviderName="System.Data.SqlClient" SelectCommand="select id,Value  From  MSTCommon where Flag = '2' order by ID">
                                            </asp:SqlDataSource>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlYear"
                                        Display="Dynamic" InitialValue="0" ErrorMessage="Please select Year" ForeColor="Red"
                                        SetFocusOnError="True" ValidationGroup="Surname">* </asp:RequiredFieldValidator>
                                </div>

                                        <div class="col-md-3">
                                            <label class="control-label" style="margin-top: 3px; text-align: left;">
                                                Assessment Type: <span style="color: Red">*</span></label>
                                            <div>
                                                <asp:DropDownList ID="ddlLevel" TabIndex="1" runat="server" CssClass="form-control input-sm" Style="margin-top: 2px"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ConnectionStrings:DBConnection %>"
                                ProviderName="System.Data.SqlClient" SelectCommand="select id,Value  From  MSTCommon where Flag = '2' order by ID"></asp:SqlDataSource>--%>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlLevel"
                                                    Display="Dynamic" InitialValue="0" ErrorMessage="Please select Project" ForeColor="Red"
                                                    SetFocusOnError="True" ValidationGroup="Surname">* </asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                       
                                        <div class="col-md-3">
                                            <label class="control-label" style="margin-top: 3px; text-align: left;">
                                                Assessment Name: <span style="color: Red">*</span></label>
                                            <div>
                                                <asp:DropDownList ID="ddlForm" TabIndex="2" runat="server" CssClass="form-control input-sm" Style="margin-top: 2px"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlForm_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlForm"
                                                    Display="Dynamic" InitialValue="0" ErrorMessage="Please select Form" ForeColor="Red"
                                                    SetFocusOnError="True" ValidationGroup="FormName">* </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                         <div class="col-md-3">
                                            <label class="control-label" style="margin-top: 3px; text-align: left;">
                                                Category Name: <span style="color: Red">*</span></label>
                                            <div>
                                                <asp:DropDownList ID="ddlcat" TabIndex="3" runat="server" OnSelectedIndexChanged="ddlcat_SelectedIndexChanged" CssClass="form-control input-sm" Style="margin-top: 2px"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlcat"
                                                    Display="Dynamic" InitialValue="0" ErrorMessage="Please select Form" ForeColor="Red"
                                                    SetFocusOnError="True" ValidationGroup="FormName">* </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-3">
                                            <asp:LinkButton ID="lnkbtnChild" Visible="false" runat="server" Style="margin-top: 30px;" class="btn btn-xs btn-primary pull-left" OnClick="lnkbtnChild_Click">
                                    Select Parent Question</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row" id="divparentchildquestion" visible="false" runat="server" style="padding-top: 10px;">
                                        <div class="form-group" style="float: left; width: 100%;" id="ChildQuestionSection" runat="server">

                                            <label class="control-label col-sm-3" style="margin-top: 10px; text-align: left;">
                                                Parent Question: <span style="color: Red">*</span></label>

                                            <div class="col-sm-5">

                                                <asp:TextBox ID="lblParentQuestion" runat="server" MaxLength="1000" TextMode="MultiLine" Height="50px"
                                                    TabIndex="4" CssClass="form-control input-sm" Style="margin-top: 5px" Enabled="false"></asp:TextBox>
                                            </div>
                                             
                                            <div class="col-sm-2">
                                                <asp:LinkButton ID="lnkAddChildQuestion" runat="server" class="btn btn-xs btn-primary" OnClick="lnkAddChildQuestion_Click" Style="margin-top: 10px;">
                                        <span class="glyphicon glyphicon-plus"></span>Child Question</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body scroll" style="min-height: 440px; padding: 0; max-height: 440px; width: 100%; margin-top: -14px; overflow-y: scroll;">
                                    <asp:Label ID="Label1" runat="Server" ForeColor="maroon" Font-Bold="True"></asp:Label>
                                    <asp:GridView ID="GvQuestion" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display."
                                        AllowSorting="True" OnRowDataBound="gvnroll_OnRowCommand" GridLines="Both" BorderColor="#e1e1e1" AlternatingRowStyle-BackColor="#F7F7F7"
                                        DataKeyNames="QuestionID,QuestionNo,Question,QestionTypeID,QuestionAns,ImageUpload,Sequence,Flag,IsQuestionMandatory,MaxLenght,MaskValidation,UID,GroupID,QuestionType,QCategoryID"
                                        CssClass="table table-striped table-bordered table-condensed" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                        AllowPaging="false" ShowFooter="false">

                                        <FooterStyle CssClass="DataGridFooter" />
                                        <PagerStyle CssClass="paging" />
                                        <HeaderStyle CssClass="DataGridHeader" />
                                        <SelectedRowStyle BackColor="#D5D5BF" Font-Bold="True" />
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Q No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuestionNo" runat="server" Text='<%#Bind("QuestionNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Seq">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSequence" runat="server" Text='<%#Bind("Sequence") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="CategoryName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChildQuestion" runat="server" Text='<%#Bind("CategoryName") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Question">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuestion" runat="server" Text='<%#Bind("Question") %>'></asp:Label></br>
                                    <asp:Label ID="lblQuestionType" Visible="false" runat="server" Text='<%#Bind("QuestionType") %>'></asp:Label>
                                                    <asp:Label ID="lblImageUpload" Visible="false" runat="server" Text='<%#Bind("ImageUpload") %>'></asp:Label>
                                                    <asp:Image ID="imgMKSG" runat="server" Height="80px" Width="100px" BorderColor="Black"
                                                        BorderStyle="Ridge" BorderWidth="1px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Edit_Question" OnClick="Edit_Question_Click" class="btn btn-xs btn-info" runat="server">
                                                                    <span class="fa fa-pencil-square-o"> 
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButtonEdit" Visible='<%# Convert.ToInt32(Eval("QestionTypeID")) ==4 || Convert.ToInt32(Eval("QestionTypeID")) ==5 || Convert.ToInt32(Eval("QestionTypeID")) ==10 ? true : false %>'
                                                        CommandArgument='<%#Eval("QestionTypeID") %>'
                                                        runat="server" OnClick="update_Question_Click"><i class="fa fa-eye" aria-hidden="true"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>

                                                    <%--                                    <asp:LinkButton ID="Delete_Question" OnClientClick="javascript:return confirm('Are you sure you want to delete this record?');" OnClick="Delete_Question_Click" class="btn btn-sm btn-warning" runat="server">
                                                                     <span class="glyphicon glyphicon-trash" data-fa-transform="shrink-10 up-.5" style="color:red"></span>
                                                                   
                                    </asp:LinkButton> --%>
                                                    <asp:ImageButton ID="Delete_Question" ImageUrl="~/images/delete-29.png" Height="22px" OnClientClick="javascript:return confirm('Are you sure you want to delete this record?');"
                                                        OnClick="Delete_Question_Click" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="6%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUp" CommandArgument="up" runat="server" Text="&#x25B2;" CssClass="btn-link-new" OnClick="ChangePreferenceUP">
                                 <%--  <i class="fa fa-arrow-circle-o-up" aria-hidden="true"></i>--%>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="6%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDown" CommandArgument="down" runat="server" Text="&#x25BC;" CssClass="btn-link-new" OnClick="ChangePreferenceDown">
                                      <%--  <i class="fa fa-arrow-circle-o-down" aria-hidden="true"></i>--%>

                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <asp:HiddenField ID="hdnOrignalChildQuestionID" runat="server" />

                                    <asp:GridView ID="GvQuestionChild" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display."
                                        AllowSorting="True" GridLines="Both" BorderColor="#e1e1e1" AlternatingRowStyle-BackColor="#F7F7F7"
                                        DataKeyNames="ChildQuestionID,QuestionID,FormID,QuestionNo,Question,QestionTypeID,GroupID,Sequence,Flag,IsQuestionMandatory,
                                MaxLenght,Qtype,MaskValidation"
                                        CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                        AllowPaging="false" ShowFooter="false">
                                        <FooterStyle CssClass="DataGridFooter" />
                                        <PagerStyle CssClass="paging" />
                                        <HeaderStyle CssClass="DataGridHeader" />
                                        <SelectedRowStyle BackColor="#D5D5BF" Font-Bold="True" />
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="Q No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblchildQuestionNo" runat="server" Text='<%#Bind("QuestionNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Q type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblchildQtype" runat="server" Text='<%#Bind("Qtype") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="9%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Seq">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChildSequence" runat="server" Text='<%#Bind("Sequence") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQCategoryn" runat="server" Text='<%#Bind("Category") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           

                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Edit_ChildQuestion" OnClick="Edit_ChildQuestion_Click" class="btn btn-xs btn-info"
                                                        runat="server">
                                                                    <span class="fa fa-pencil-square-o"></span></span>  
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Delete_ChildQuestion" ImageUrl="~/images/delete-29.png" OnClientClick="javascript:return confirm('Are you sure you want to delete this record?');"
                                                        OnClick="Delete_ChildQuestion_Click" runat="server"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="6%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpChild" CssClass="butt_new_grid1" CommandArgument="up" runat="server" BackColor="Green"
                                                        Text="&#x25B2;" OnClick="ChangePreferenceUP1" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="6%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownChild" CssClass="butt_new_grid1" CommandArgument="down" runat="server" BackColor="Green"
                                                        Text="&#x25BC;" OnClick="ChangePreferenceDown1" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>




            <!-- Alert -->
            <div>
                <asp:ModalPopupExtender ID="ModalAlert" runat="server" TargetControlID="hdn_alertmodal"
                    PopupControlID="pnl_alert" CancelControlID="btn_cancelalert" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnl_alert" runat="server" Style="display: none;" class="Mpopup" Width="345px">

                    <div style="padding: 0 0 10px 0;">
                        <div class="Mpopupheader" align="center">
                            Message
                        </div>
                        <div style="width: 332px; text-align: center" class="Mpopupbodycontent">
                            <div style="width: 100%; height: 8px;">
                            </div>
                            <asp:Label ID="lbl_messages" runat="server" CssClass="LabelHeader" Font-Bold="True"
                                Font-Size="11pt" Width="316px"></asp:Label>
                            <div style="width: 100%; height: 8px;">
                            </div>
                        </div>
                        <div style="text-align: center;" align="center">
                            <asp:Button ID="btn_cancelalert" runat="server" CssClass="butt-new" Text="  OK  "
                                Width="74px" />
                        </div>
                    </div>
                    <div class="Mpopupfooter" align="right">
                    </div>
                </asp:Panel>
                <asp:HiddenField ID="hdn_alertmodal" runat="server" />
                <asp:Button ID="DoNothing" runat="server" Text="" Style="display: none" />
            </div>

            <asp:Panel runat="server" ID="panel" Style="display: none; height: auto">
                <%--    <%=STRPRINTCONTENT %>--%>
            </asp:Panel>

            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hdn_alertmodal"
                PopupControlID="pnl_alert" BehaviorID="popup" CancelControlID="btn_cancelalert"
                BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

            <asp:Panel ID="Panel2" runat="server" Style="display: none;" class="Mpopup" Width="345px">
                <div style="padding: 0 0 10px 0;">
                    <div class="Mpopupheader" align="center">
                        Message
                    </div>
                    <div style="width: 332px; text-align: center" class="Mpopupbodycontent">
                        <div style="width: 100%; height: 8px;">
                        </div>
                        <asp:Label ID="Label2" runat="server" CssClass="LabelHeader" Font-Bold="True" Font-Size="11pt"
                            Width="316px"></asp:Label>
                        <div style="width: 100%; height: 8px;">
                        </div>
                    </div>
                    <div style="text-align: center;" align="center">
                        <asp:Button ID="Button1" runat="server" CssClass="butt-new" Text="  OK  " Width="74px" />
                    </div>
                </div>
                <div class="Mpopupfooter" align="right">
                </div>
            </asp:Panel>

            <asp:HiddenField ID="HiddenField1" runat="server" />


            <div>

                <asp:Panel ID="pnlPreview" runat="server" CssClass=" model-wid Mpopup mod-posi" Style="height: auto; display: none;">
                    <div style="border: 0px solid #ccc; width: 100%; min-height: 200px; margin: 0 auto; background: azure;">
                        <div class="modal-header">
                            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>

                            <asp:LinkButton ID="lblPreviewClose" class="btn btn-sm btn-danger pull-right"
                                runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>

                            <%--<span style="float: right">
<asp:Label ID="lblFormNameClose" runat="server" Text="Close[X]" Style="cursor: pointer"></asp:Label>
</span>--%>
                        </div>
                        <div class="modal-body">
                            <div style="height: 440px; overflow-y: auto;">
                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                <div id="dialog" runat="server" style="width: 700px" align="center">
                                    <asp:GridView ID="GVQuestionList" runat="server" AutoGenerateColumns="False"
                                        EmptyDataText="There are no data records to display." AllowSorting="True" GridLines="Both"
                                        BorderColor="#e1e1e1" AlternatingRowStyle-BackColor="#F7F7F7"
                                        DataKeyNames="QuestionID,QuestionNo,Question,QestionTypeID,Sequence,Flag,IsQuestionMandatory,MaxLenght"
                                        AllowPaging="false" ShowFooter="false">
                                        <FooterStyle CssClass="DataGridFooter" />
                                        <PagerStyle CssClass="paging" />
                                        <HeaderStyle CssClass="DataGridHeader" />
                                        <SelectedRowStyle BackColor="#D5D5BF" Font-Bold="True" />
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>


                                            <asp:TemplateField HeaderText="Q No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuestionNo" runat="server" Text='<%#Bind("QuestionNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Seq">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSequence" runat="server" Text='<%#Bind("Sequence") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                            

                                            <asp:TemplateField HeaderText="Question">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuestion" runat="server" Text='<%#Bind("Question") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkQuestion" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderStyle-Width="6%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUp" CssClass="butt_new_grid1" CommandArgument="up" runat="server" Text="&#x25B2;" OnClick="ChangePreferenceUP" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="6%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDown" CssClass="butt_new_grid1" CommandArgument="down" runat="server" Text="&#x25BC;" OnClick="ChangePreferenceDown" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </div>


                </asp:Panel>

                <asp:ModalPopupExtender ID="MPPreview" BackgroundCssClass="modalBackground"
                    runat="server" PopupControlID="pnlPreview" TargetControlID="HFPreview" CancelControlID="lblPreviewClose">
                </asp:ModalPopupExtender>
                <asp:HiddenField ID="HFPreview" runat="server" />

            </div>


            <div>
                <asp:ModalPopupExtender ID="modelQuestion" runat="server" TargetControlID="hdn_alertmodal1"
                    PopupControlID="pnl_alert12" CancelControlID="btn_cancelalert">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnl_alert12" runat="server" Style="display: none; z-index: 900;"
                    class="modalPopup" Width="500px">
                    <div class="col-lg-12 col-md-12 col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <p class="text-danger" style="margin: 0px;">
                                    <asp:Label ID="Label5" runat="server" Text="Add/Edit Child Question"></asp:Label>
                                    <asp:Label ID="Label6" runat="server" Style="float: right;" Text="(*) Mandatory"></asp:Label>
                                </p>
                            </div>
                            <div class="panel-body" style="min-height: 40px;">

                                <div class="form-group">
                                    <label class="control-label col-sm-4" style="margin-top: 10px; text-align: left;">
                                        Parent Question: <span style="color: Red">*</span></label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlParentQuestion" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12 col-md-12 col-xs-12 panel-footer" align="right">

                        <asp:Button ID="btnQuestionchild" CssClass="btn btn-success" runat="server" Text="Select"
                            OnClick="btnQuestionchild_Click" />
                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" />
                    </div>
                </asp:Panel>
                <asp:HiddenField ID="hdn_alertmodal1" runat="server" />
                <asp:Button ID="Button3" runat="server" Text="" Style="display: none" />
            </div>



            <asp:ModalPopupExtender ID="MPEFormName" BackgroundCssClass="modalBackground"
                runat="server" PopupControlID="pnlFormName" TargetControlID="HFFormName" CancelControlID="lblFormNameClose">
            </asp:ModalPopupExtender>


            <asp:HiddenField ID="HFFormName" runat="server" />
            <asp:HiddenField ID="HFFormId" runat="server" />
            <asp:Panel ID="pnlFormName" runat="server" CssClass=" model-wid Mpopup1 mod-posi" Style="height: 500px !important; width: 40% !important; display: none;">

                <div style="border: 0px solid #ccc; width: 100%; min-height: 304px; margin: 0 auto;">
                    <div class="modal-header">


                        <asp:LinkButton ID="lblFormNameClose" class="btn btn-xs btn-danger pull-right"
                            runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>
                    </div>

                    <div class="modal-body">
                        <div>
                            <asp:Label ID="lblFormNamerr" runat="server" Text="" Style="word-wrap: normal; word-break: break-all;"></asp:Label>
                            <div class="form-horizontal" role="form">

                                <div class="form-group">
                                    <div class="panel-body scroll" style="min-height: 304px; max-height: 304px; overflow-y: auto;">

                                        <asp:GridView ID="GVOptions" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display." AllowSorting="True" GridLines="Both" BorderColor="#e1e1e1" AlternatingRowStyle-BackColor="#F7F7F7"
                                            DataKeyNames="UId,Flag,ID" OnRowEditing="GVOptions_RowEditing" OnRowUpdating="GVOptions_RowUpdating" OnRowCancelingEdit="GVOptions_RowCancelingEdit"
                                            CssClass="table table-striped table table-hover table-bordered" SelectedRowStyle-BackColor="#e1f4a6"
                                            AllowPaging="false" Style="color: #333333" ShowHeaderWhenEmpty="true">
                                            <FooterStyle CssClass="DataGridFooter" />
                                            <PagerStyle CssClass="paging" />
                                            <HeaderStyle CssClass="DataGridHeader" />
                                            <SelectedRowStyle BackColor="#D5D5BF" Font-Bold="True" />
                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1%" CssClass="gvtextcenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Options" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOptions" runat="server" Text='<%#Bind("Value") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtOptions" runat="server" Text='<%#Bind("Value") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle Width="25%" CssClass="gvtextcenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Score" ItemStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblScore" runat="server" Text='<%#Bind("Score") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtScore" runat="server" onkeypress="return isNumberKey(this,event);" Text='<%#Bind("Score") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle Width="2%" CssClass="gvtextcenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Update" ItemStyle-Width="1%" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btn_Edit" runat="server" CommandName="Edit"><i class="ace-icon fa fa-pencil-square-o bigger-230"></i>
                            
                                                        </asp:LinkButton>

                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update" class="btn btn-success" />
                                                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" class="btn" />
                                                    </EditItemTemplate>
                                                    <ItemStyle Width="2%" CssClass="gvtextcenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="1%">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkHeader" class="chkQCSelectAll" runat="server" Text="All" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkFormName" class="chkQCFormQues" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="2%" CssClass="gvtextcenter" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>



                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="Button2" OnClick="btnParticipate_Click" runat="server" Style="margin-top: -5px; margin-left: 397px" class="btn btn-sm btn-primary">Save</asp:LinkButton>

                </div>
               </asp:Panel>
                  <asp:ModalPopupExtender ID="MPE_Entry" BackgroundCssClass="modalBackground"
                runat="server" PopupControlID="Pnl_Entry" TargetControlID="HdnEntry" CancelControlID="lnkEntryClose">
            </asp:ModalPopupExtender>
            <asp:HiddenField ID="HdnEntry" runat="server" />

            <asp:Panel ID="Pnl_Entry" runat="server" CssClass=" model-wid Mpopup1 mod-posi" Style="height: 610px  !important; position: fixed !important; width: 40% !important; display: none;">

                <div style="border: 0px solid #ccc; width: 100%; min-height: 100px; margin: 0 auto;">
                    <div class="modal-header">
                        <h3 class="text-danger" style="margin: 0;">Add Question Category
                                            
                            <asp:LinkButton ID="lnkEntryClose" class="btn btn-xs btn-danger pull-right"
                                runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>
                        </h3>

                    </div>
                    <div class="modal-body">
                        <div style="height: auto;">
                            <div class="form-group">
                                <div class="row" runat="server" id="Div1">
                                   
                                    <div class="row form-group">
                                        <label class="control-label col-sm-3">
                                           Assessment Type: <span style="color: Red">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ddlcLevel" runat="server" CssClass="form-control input-sm" Style="margin-top: 2px"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlcLevel_SelectedIndexChanged">
                                                </asp:DropDownList>
                                  
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlcLevel"
                                                    Display="Dynamic" InitialValue="0" ErrorMessage="Please select Project" ForeColor="Red"
                                                    SetFocusOnError="True" ValidationGroup="cSave">* </asp:RequiredFieldValidator>
                                       
                                        </div>


                                    </div>
                                     <div class="row form-group">
                                        <label class="control-label col-sm-3">
                                           Assessment Name: <span style="color: Red">*</span></label>
                                        <div class="col-sm-7">
                                        

                                              <asp:DropDownList ID="ddlcForm" runat="server" CssClass="form-control input-sm" Style="margin-top: 2px"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlcForm_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlcForm"
                                                    Display="Dynamic" InitialValue="0" ErrorMessage="Please select Form" ForeColor="Red"
                                                    SetFocusOnError="True" ValidationGroup="cSave">* </asp:RequiredFieldValidator>
                                       
                                        </div>


                                    </div>
                                     <div class="row form-group">
                                        <label class="control-label col-sm-3">
                                            Question Category: <span style="color: Red">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtcat" runat="server" MaxLength="100"  CssClass="form-control input-sm">
                                            </asp:TextBox>
                                       
                                          
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" ControlToValidate="txtcat" Display="Dynamic" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="txtcat">
                                            </asp:RequiredFieldValidator>
                                        </div>


                                    </div>
                                </div>
                                <div class="row" runat="server" id="Div4" style="margin-bottom: 15px;">
                                    <div class="form-group">

                                        <div class="col-sm-12">
                                            <asp:LinkButton ID="BtnEntry"  class="btn btn-xs btn-primary pull-right"
                                                ToolTip="Save" Width="55px" OnClick="btnsaveCAT_Click" ValidationGroup="cSave"
                                                Style="margin-top: -4px; width: 70px; height: 26px;" runat="server">Save</asp:LinkButton>


                                        </div>
                                    </div>
                                </div>

                                <div class="form-group" style="overflow: auto; margin-top: 2px; height: 270px;">
                                    <%--<div style="overflow: auto; margin-top: -5px; height: 350px;">--%>
                                    <asp:GridView ID="GvEntry" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display." AllowSorting="True" GridLines="Both" BorderColor="#e1e1e1" AlternatingRowStyle-BackColor="#F7F7F7"
                                        CssClass="table table-striped table table-hover table-bordered" SelectedRowStyle-BackColor="#e1f4a6"
                                        AllowPaging="false" Style="color: #333333" ShowHeaderWhenEmpty="true" >
                                        <FooterStyle CssClass="DataGridFooter" />
                                        <PagerStyle CssClass="paging" />
                                        <HeaderStyle CssClass="DataGridHeader" />
                                        <SelectedRowStyle BackColor="#D5D5BF" Font-Bold="True" />
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Assessment Type" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEntryCode" runat="server" Text='<%#Bind("Description") %>'></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle Width="20%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Assessment Name" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEntryName" runat="server" Text='<%#Bind("FormName") %>'></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle Width="20%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Category Name" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEntryName11" runat="server" Text='<%#Bind("CategoryName") %>'></asp:Label>
                                                      <asp:Label ID="lblCat" runat="server" Text='<%#Bind("CategoryID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle Width="20%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="Delete_QuestionEntry11" OnClick="btncDelete_Click" class="btn btn-sm btn-warning" runat="server">
                                                                     <span class="glyphicon glyphicon-trash" data-fa-transform="shrink-10 up-.5" style="color:red"></span>
                                                                        
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>

                            </div>
                        </div>

                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </asp:Panel>

           

                <asp:ModalPopupExtender ID="MPEFormNameQ" BackgroundCssClass="modalBackground"
                runat="server" PopupControlID="pnlFormNameQ" TargetControlID="HFFormNameQ" CancelControlID="lblFormNameCloseQ">
            </asp:ModalPopupExtender>


            <asp:HiddenField ID="HFFormNameQ" runat="server" />
            
            <asp:Panel ID="pnlFormNameQ" runat="server" CssClass=" model-wid Mpopup1 mod-posi" Style="height: 500px !important; width: 40% !important; display: none;">

                <div style="border: 0px solid #ccc; width: 100%; min-height: 304px; margin: 0 auto;">
                    <div class="modal-header">


                        <asp:LinkButton ID="lblFormNameCloseQ" class="btn btn-xs btn-danger pull-right"
                            runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>
                    </div>

                    <div class="modal-body">
                        <div>
                                 <div class="row">
         <label class="control-label col-lg-1  col-sm-12" style="text-align: left;">
   Search</label>
              <div class="col-lg-9  col-sm-12">
                     <asp:TextBox ID="Txt_VillageDTD" style="width:350px;margin-left: 14px;" Width="350px" onkeyup="Search_Gridview3(this, 'GVFlagMaster')" runat="server" class="form-control" />
   
              </div>
     </div>
                              <%--    <div>
                            <label>Search</label>
                                             <asp:TextBox ID="Txt_VillageDTD" Width="350px" onkeyup="Search_Gridview3(this, 'GVANs')" runat="server" class="form-control" />
                              </div> --%> 
                            <asp:Label ID="lblForhherr" runat="server" Text="" Style="word-wrap: normal; word-break: break-all;"></asp:Label>
                            <div class="form-horizontal" role="form">

                                <div class="form-group">
                                    <div class="panel-body scroll" style="min-height: 304px; max-height: 304px; overflow-y: auto;">

                                        <asp:GridView ID="GVANs" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display." AllowSorting="True" GridLines="Both" BorderColor="#e1e1e1" AlternatingRowStyle-BackColor="#F7F7F7"
                                            DataKeyNames="UID,ID"  
                                            CssClass="table table-striped table table-hover table-bordered" SelectedRowStyle-BackColor="#e1f4a6"
                                            AllowPaging="false" Style="color: #333333" ShowHeaderWhenEmpty="true">
                                            <FooterStyle CssClass="DataGridFooter" />
                                            <PagerStyle CssClass="paging" />
                                            <HeaderStyle CssClass="DataGridHeader" />
                                            <SelectedRowStyle BackColor="#D5D5BF" Font-Bold="True" />
                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                               
                                                <asp:TemplateField HeaderText="Ans" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                          <asp:LinkButton ID="EditOptionValue"  Text='<%#Bind("Value") %>' OnClick="EditOptionValue_Click" ToolTip="Edit"
                                                runat="server"></span> </asp:LinkButton>
                                                
                                                          <asp:Label ID="Label3" Visible="false" runat="server" Text='<%#Bind("UID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle Width="100%" CssClass="gvtextcenter" />
                                                </asp:TemplateField>
                                               
                                            </Columns>
                                        </asp:GridView>
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







