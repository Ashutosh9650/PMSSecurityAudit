<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SurveyAddCategory.aspx.cs" Inherits="SurveyAddCategory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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

        .table {
            width: 138% !important;
            max-width: 102% !important;
            margin-bottom: 96px;
            margin-left: -13px;
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
            z-index: 1350px0001 !important;
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






        label, .control-label {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: 400 !important;
            font-size: 12px;
        }
    </style>
    <script type="text/javascript">
        function alphanumeric(inputtxt) {
            var alphaletters = /^[A-Za-z]+$/i;
            var letters = /^[0-9a-zA-Z]+$/;
            if (!inputtxt.value.match(alphaletters)) {
                $('#<%=txttablename.ClientID %>').val('');
                alert('Please input alphabet characters only');
                return false;
            }
        }
        <%--$(document).ready(function () {
            $("#<%=lbltablename.ClientID %>").hide();
            $("#<%=txttablename.ClientID %>").hide();
        });--%>
        function Gettablename() {
            var value = $("#<%=ddlsurveytype.ClientID %> option:selected").val();
            if (value == '0') {
                $("#<%=lbltablename.ClientID %>").show();
                $("#<%=txttablename.ClientID %>").hide();
                document.getElementById('<%=lbltablename.ClientID %>').innerText = 'Table Name';
            }
            else if (value == '1') {
                $("#<%=lbltablename.ClientID %>").show();
                $("#<%=txttablename.ClientID %>").hide();
                document.getElementById('<%=lbltablename.ClientID %>').innerText = 'Table Name';
                $('#myModal').modal({
                    show: true
                });

            }
            else if (value == '2') {
                $('#myModal').modal({
                    show: false
                });
                $("#<%=lbltablename.ClientID %>").hide();
                $("#<%=txttablename.ClientID %>").show();
            }
        }

        function SelectTable(id) {
            var tablenameid = id.replace("spanselecttable", "chkbxfortable");
            var tablename = document.getElementById(tablenameid).innerText;
            $("#<%=hdnfdtablename.ClientID %>").val(tablename);
            $("#<%=lbltablename.ClientID %>").show();
            $("#<%=txttablename.ClientID %>").hide();
            document.getElementById('<%=lbltablename.ClientID %>').innerText = $("#<%=hdnfdtablename.ClientID %>").val();
            $('#myModal').modal('hide');

        }

        function Validation() {
            if ($('#<%=ddlLevel.ClientID%>').val() == '0') {
                alert('Please select form level');
                return false;
            }
            else if ($('#<%=txtFormName.ClientID%>').val() == '') {
                alert('Please provide survey name');
                return false;
            }
            else if ($('#<%=ddlLevel.ClientID%>').val() == '1') {
                if ($('#<%=ddlLearning.ClientID%>').val() == '0') {
                    alert('Please select Specific training');
                    return false;
                }
                if ($('#<%=ddlTraingOutcome.ClientID%>').val() == '0') {
                    alert('Please select Traing Outcome');
                    return false;
                }

            }
            else if ($('#<%=ddlLevel.ClientID%>').val() == '2') {
                if ($('#<%=ddlTraingOutcome.ClientID%>').val() == '0') {
                    alert('Please select Traing Outcome');
                    return false;
                }


            }
            else if ($('#<%=LnkFormNameSave.ClientID%>').text() == ' Save') {
                if ($('#<%=ddlsurveytype.ClientID%>').val() == '0') {
                    alert('Please select survey type');
                    return false;
                }

               <%-- else if ($('#<%=ddlsurveytype.ClientID%>').val() == '2') {
                    var regex = new RegExp("^[a-zA-Z0-9]+$");
                    var str = $('#<%=txttablename.ClientID%>').val();
                    if (regex.test(str)) {
                        return true;
                    }
                    else {
                        alert('Please enter table name in english');
                        return false;
                    }
                }--%>
                else {
                    return true;
                }
            }
            else {
                return true;
            }

            return true;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="col-lg-12">
        <div class="panel panel-default" style="padding-bottom: 0px !important;">
            <div class="panel-heading">
                <p class="text-danger" style="margin: 3px;">
                    Define Assessment Category
                </p>
            </div>
            <div class="panel-body" style="min-height: 500px; margin-bottom: -25px;">
                <div id="Project">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row" style="margin-top: -5px; margin-bottom: -5px; margin-right: 5px; padding: 5px 0;">
                                <div class="col-12 pull-right">
                                    <asp:LinkButton ID="LnkFormNameSave" class="btn btn-sm btn-primary" runat="server" OnClientClick="return Validation();" OnClick="LnkFormNameSave_Click"><span class="glyphicon glyphicon-floppy-disk"></span>&nbsp;Save</asp:LinkButton>
                                </div>
                            </div>
                            <%--<p class="text-danger" style="margin: 0px;">
                                <asp:Label ID="lblHeadingOne" runat="server" Text=""></asp:Label>
                            </p>--%>
                        </div>
                        <div class="panel-body" style="width: 100%;">

                            <div class="form-group">


                                    <div class="col-sm-3">
                                    <label class="control-label">
                                        Year : <span style="color: Red">*</span></label>
                                    <asp:DropDownList ID="ddlYear" runat="server" TabIndex="1" CssClass="form-control input-sm"   AutoPostBack="true"  OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">    
                                    </asp:DropDownList>
                                    <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="$ConnectionStrings:DBConnection"
                                                ProviderName="System.Data.SqlClient" SelectCommand="select id,Value  From  MSTCommon where Flag = '2' order by ID">
                                            </asp:SqlDataSource>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlYear"
                                        Display="Dynamic" InitialValue="0" ErrorMessage="Please select Year" ForeColor="Red"
                                        SetFocusOnError="True" ValidationGroup="Surname">* </asp:RequiredFieldValidator>
                                </div>

                                <div class="col-sm-3">
                                    <label class="control-label">
                                        Assessment Type : <span style="color: Red">*</span></label>
                                    <asp:DropDownList ID="ddlLevel" runat="server" TabIndex="1" CssClass="form-control input-sm"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="$ConnectionStrings:DBConnection"
                                                ProviderName="System.Data.SqlClient" SelectCommand="select id,Value  From  MSTCommon where Flag = '2' order by ID">
                                            </asp:SqlDataSource>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlLevel"
                                        Display="Dynamic" InitialValue="0" ErrorMessage="Please select Project" ForeColor="Red"
                                        SetFocusOnError="True" ValidationGroup="Surname">* </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-3" runat="server" id="d1" visible="false">
                                    <label class="control-label">
                                        Training Outcome : <span style="color: Red">*</span></label>
                                    <asp:DropDownList ID="ddlLearning" OnSelectedIndexChanged="ddlLearning_SelectedIndexChanged"
                                        AutoPostBack="true" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>


                                </div>

                                <div class="col-sm-3" runat="server" id="d2" visible="false">
                                    <asp:Label ID="Label2" class="control-label" runat="server"
                                        Text="Specific Training Name:"><span style="color: Red">*</span></asp:Label>

                                    <asp:DropDownList ID="ddlTraingOutcome" runat="server" TabIndex="1" CssClass="form-control input-sm"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlTraingOutcome_SelectedIndexChanged">
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTraingOutcome"
                                        Display="Dynamic" InitialValue="0" ErrorMessage="Please select Project" ForeColor="Red"
                                        SetFocusOnError="True" ValidationGroup="Surname">* </asp:RequiredFieldValidator>
                                </div>



                                <div class="col-sm-3">
                                    <label class="control-label">
                                        Assessment Name : <span style="color: Red">*</span></label>
                                    <asp:TextBox ID="txtFormName" runat="server" MaxLength="90" CssClass="form-control input-sm"></asp:TextBox>

                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFormName" Display="Dynamic" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Surname">
                                            </asp:RequiredFieldValidator>

                                </div>

                                <div class="col-sm-3" runat="server" visible="false">
                                    <label class="control-label">
                                        Last Date : <span style="color: Red">*</span></label>
                                    <asp:TextBox runat="server" OnClientDateSelectionChanged="arrivaldatecheck" ID="txtDate" autocomplete="off" ondrop="return false;"
                                        class="form-control" onkeypress="return false;"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtDate" PopupPosition="BottomRight">
                                    </asp:CalendarExtender>
                                </div>

                                <div class="col-lg-3" runat="server" visible="false">
                                    <label class="control-label">
                                        Type of Survey : <span style="color: Red"></span>
                                    </label>
                                    <asp:DropDownList ID="ddlsurveytype" runat="server" TabIndex="1" onchange="Gettablename()" CssClass="form-control input-sm" AutoPostBack="false">
                                    </asp:DropDownList>


                                </div>

                                <div class="col-lg-3" runat="server" visible="false">
                                    <label class="control-label">
                                        Table Name :
                                    </label>
                                    <asp:TextBox ID="txttablename" Style="display: none;" onkeyup="return alphabet(this)" runat="server"
                                        Placeholder="Enter Survey Name Only" CssClass="form-control input-sm" />


                                    <asp:Label ID="lbltablename" CssClass="form-control input-sm" runat="server" Style="line-height: 1.7; margin: 3px;" />

                                </div>
                                <%--//////////////////////////--%>
                            </div>

                        </div>
                    </div>
                </div>
                <div id="Activity">
                    <div class="panel panel-default" style="margin-bottom: 25px;">
                        <div class="panel-heading" style="padding-left: 15px;">
                            <p class="text-danger" style="margin: 0px;">
                                List of Surveys
                            </p>
                        </div>
                        <div class="panel-body scroll" style="min-height: 350px; max-height: 375px; overflow: auto; padding: 0px 10px 0px 15px;">
                            <asp:GridView ID="GVFormName" Width="100%" runat="server" DataKeyNames="FormLevel,FormID,FormName,FormEvaluationTableName,LastDate,TrainingOutcome,StaffTraingOutcome"
                                AutoGenerateColumns="False" CellPadding='3' CellSpacing="2" AllowSorting="True"
                                GridLines="none" CssClass="table table-striped table table-hover table-bordered Grid"
                                AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                SelectedRowStyle-BackColor="#e1f4a6">
                                <Columns>
                                    <asp:TemplateField HeaderText="Serial No.">
                                        <ItemStyle Width="5%" CssClass="GridHD" />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>.
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AgencyName" HeaderText="Assessment Type" ItemStyle-CssClass="GridHD"
                                        ItemStyle-Width="20%" />

                                    <asp:BoundField DataField="FormName" HeaderText="Assessment Name" ItemStyle-CssClass="GridHD"
                                        ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="TrainingOutcomeName" HeaderText="Training Outcome" ItemStyle-CssClass="GridHD"
                                        ItemStyle-Width="20%" />



                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemStyle Width="5%" CssClass="GridHD" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="EditFormName" OnClick="EditFormName_Click" class="btn btn-xs btn-info" runat="server" ToolTip="Edit">
                                                                     <span class="glyphicon glyphicon-new-window"></span> 
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Copy" Visible="false">
                                        <ItemStyle Width="5%" CssClass="GridHD" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="CopyFormName" OnClick="CopyFormName_Click" class="btn btn-xs btn-warning" runat="server" ToolTip="Copy">
                                                                     <span class="glyphicon glyphicon-copy"></span> 
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Delete" Visible="false">
                                        <ItemStyle Width="5%" CssClass="GridHD" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="DeleteFormName" OnClick="DeleteFormName_Click" class="btn btn-xs btn-danger" runat="server" ToolTip="Delete">
                                                                     <span class="glyphicon glyphicon-trash"></span> 
                                            </asp:LinkButton>
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

    <asp:ModalPopupExtender ID="MPEaddOrganization" BackgroundCssClass="modalBackground"
        runat="server" PopupControlID="PaneladdOrganization" TargetControlID="HFaddOrganization"
        CancelControlID="lbladdOrganization">
    </asp:ModalPopupExtender>
    <asp:HiddenField ID="HFaddOrganization" runat="server" />
    <asp:Panel ID="PaneladdOrganization" runat="server" CssClass=" model-wid Mpopup mod-posi"
        Style="height: auto; display: none; width: 35% !important;">
        <div style="border: 0px solid #ccc; width: 100%; min-height: 100px; margin: 0 auto;">
            <div class="modal-header">
                <asp:Label ID="Label1" runat="server" Text="Enter New Panchayat Activity"></asp:Label>
                <span style="float: right">
                    <asp:Label ID="lbladdOrganization" runat="server" Text="Close[X]" Style="cursor: pointer"></asp:Label></span>
            </div>
            <div class="modal-body">
                <div style="height: 105px; overflow-y: auto;">
                    <div class="form-group">
                        <label class="control-label col-lg-3  col-sm-12" style="text-align: left;">
                            Project :</label>
                        <div class="col-lg-9  col-sm-12">
                            <asp:Label ID="lblDispProject" runat="server" class="form-control"></asp:Label>
                        </div>
                    </div>
                    <div class=" form-group " style="height: 20px;">
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-3  col-sm-12" style="text-align: left;">
                            Activity :<span style="color: Red">*</span></label>
                        <div class="col-lg-9  col-sm-12">
                            <asp:TextBox ID="txtActivity" runat="server" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtActivity"
                                ErrorMessage="Enter the Organization name " ValidationGroup="VAlorg">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnSaveActivity" CssClass="btn btn-danger" runat="server" Text="Save"
                    ValidationGroup="VAlorg" />
            </div>
        </div>
    </asp:Panel>
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
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:Button ID="DoNothing" runat="server" Text="" Style="display: none" />
    <asp:HiddenField ID="hdn_alertmodal" runat="server" />
    <asp:HiddenField ID="HFDispProjectID" runat="server" />
    <asp:HiddenField ID="HFActivityID" runat="server" />

    <asp:HiddenField ID="HFFormNameID" runat="server" />

    <div>

        <asp:Panel ID="pnlPreview" runat="server" CssClass=" model-wid Mpopup mod-posi" Style="height: auto; display: none;">

            <div style="border: 0px solid #ccc; width: 100%; min-height: 200px; margin: 0 auto;">
                <div class="modal-header">
                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>

                    <asp:LinkButton ID="lblPreviewClose" class="btn btn-sm btn-danger pull-right"
                        runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>

                    <%--<span style="float: right">
<asp:Label ID="lblFormNameClose" runat="server" Text="Close[X]" Style="cursor: pointer"></asp:Label>
</span>--%>
                </div>
                <div class="modal-body">
                    <div style="height: 80px; overflow-y: auto;">

                        <div class="form-group">
                            <label class="control-label col-sm-6" style="margin-top: 10px; text-align: left;">
                                New Survey Name : <span style="color: Red">*</span></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtNewformName" runat="server" CssClass="form-control" Style="margin-top: 5px"></asp:TextBox>
                            </div>
                        </div>


                    </div>
                </div>
                <div class="modal-footer">
                    <div class="form-group">

                        <div class="col-sm-12">
                            <asp:LinkButton ID="LnkBtnSaveNew" OnClick="LnkBtnSaveNew_Click" class="btn btn-xs btn-info" runat="server">
                                                                     <span class="glyphicon glyphicon-new-window"></span> Save 
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>


        </asp:Panel>



        <asp:ModalPopupExtender ID="MPPreview" BackgroundCssClass="modalBackground"
            runat="server" PopupControlID="pnlPreview" TargetControlID="HFPreview" CancelControlID="lblPreviewClose">
        </asp:ModalPopupExtender>
        <asp:HiddenField ID="HFPreview" runat="server" />
        <asp:HiddenField ID="HFCopyFormNameId" runat="server" />

    </div>
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Table For Survey</h4>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="GV_AllTenFormEvalTable" Width="100%" runat="server" DataKeyNames="ID,Tablename"
                        AutoGenerateColumns="False" CellPadding='3' CellSpacing="2" AllowSorting="True"
                        GridLines="none" CssClass="table table-striped table table-bordered"
                        SelectedRowStyle-BackColor="#e1f4a6">
                        <Columns>
                            <asp:TemplateField HeaderText="#.">
                                <ItemStyle Width="5%" CssClass="GridHD" />
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>.
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select Table">
                                <ItemStyle Width="5%" CssClass="GridHD" />
                                <ItemTemplate>
                                    <asp:Label ID="chkbxfortable" Text='<%# Eval("TableName") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Survey">
                                <ItemStyle Width="5%" CssClass="GridHD" />
                                <ItemTemplate>
                                    <asp:Label ID="lbltotalsurvey" Text='<%# Eval("TotalSurvey") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Response">
                                <ItemStyle Width="5%" CssClass="GridHD" />
                                <ItemTemplate>
                                    <asp:Label ID="lbltotalresponse" Text='<%# Eval("TotalResponse") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select Table">
                                <ItemStyle Width="5%" CssClass="GridHD" />
                                <ItemTemplate>
                                    <%--<asp:Label ID="spanselecttable" runat="server"><a Class="btn btn-xs btn-success"><span class="glyphicon glyphicon-hand-up"></span> Select</a></asp:Label>--%>
                                    <asp:LinkButton ID="lnkbtnselecttable" CssClass="btn btn-xs btn-success" Text='Select' OnClick="lnkbtnselecttable_Click" runat="server">
                                        <span class="glyphicon glyphicon-hand-up"></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnfdtablename" runat="server" />
    <asp:HiddenField ID="hdnfdtableid" runat="server" />
</asp:Content>

