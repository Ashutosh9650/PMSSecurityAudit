<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmFilingSystem.aspx.cs" Inherits="frmFilingSystem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="js/jquery-2.1.0.js"></script>
    <script src="js/Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function PrintPanel2() {
            var pnlNiyamaWali = document.getElementById("<%=pnlNiyamaWali.ClientID %>");
            document.getElementById("<%=pnlNiyamaWali.ClientID %>").style.display = "block";
            var printWindow = window.open('', '', 'height=900,width=1800');
            var i = 0;
            $('.Testtbl').each(function () {
                if (($('.Div12:eq(' + i + ')').html() != '') && ($('.Div13:eq(' + i + ')').html() == '')) {
                    $('.abc:eq(' + i + ')').hide();
                }
                i++;
            });
            printWindow.document.write(pnlNiyamaWali.innerHTML);
            printWindow.document.close();
            setTimeout(function () {
                document.getElementById("<%=pnlNiyamaWali.ClientID %>").style.display = "none";
                printWindow.print();
            }, 2000);
        }


    </script>
    <script src="js/ScrollableGridPlugin.js" type="text/javascript"></script>
    <script src="js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=GVSealSign.ClientID %>').Scrollable({
                ScrollHeight: 100
            });

        });
    </script>
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
    </style>
    <style type="text/css">
        .multiselect.dropdown-toggle.btn.btn-default > div.restricted {
            margin-right: 5px;
            max-width: 100px;
            overflow: hidden;
        }
    </style>
    <style type="text/css">
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
            margin-left: 5px;
            font-weight: bold;
            font-size: small;
            top: 53%;
            left: 3%;
            text-align: left !important;
            color: Black;
            background: white !important;
        }

        .CheckBoxListCssClassNew {
            font-family: calibri;
            margin-left: 5px;
            font-weight: bold;
            font-size: 11.2px !important;
            top: 53%;
            left: 3%;
            text-align: left !important;
            color: Black;
            background: white !important;
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
            width: 100px !important;
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

        .thumbnail ul {
            float: left;
            width: 100%;
            height: auto;
            margin: 0px;
            padding: 0px;
            list-style: none;
        }

            .thumbnail ul li {
                float: left;
                width: 100%;
                height: auto;
            }

                .thumbnail ul li a {
                    float: left;
                    width: 100%;
                    height: auto;
                    padding: 10px;
                    border: 1px solid #ddd;
                }

        .div_container {
            display: flex;
            margin-right: -15px;
            margin-left: -15px;
        }

        .auto_div {
            position: relative;
            width: 100%;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
            flex: 1 1 auto;
        }

        @media (max-width:767px) {
            .div_container {
                display: block;
                margin-right: -15px;
                margin-left: -15px;
            }
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


        input[type="radio"], input[type="checkbox"] {
            margin: 4px 7px 0px !important;
            margin-top: 1px !important;
            line-height: normal !important;
        }

        .gridnewheadercss {
            color: #ffffff;
            vertical-align: middle;
            background-color: #81AB81;
        }

        table#ctl00_MainContent_ddlBlock tbody tr td, table#ctl00_MainContent_ddlVillage tbody tr td, table#ctl00_MainContent_chkVillage tbody tr td {
            display: flex;
            justify-content: start;
            align-items: center;
        }

            table#ctl00_MainContent_ddlBlock tbody tr td label, table#ctl00_MainContent_ddlVillage tbody tr td label, table#ctl00_MainContent_chkVillage tbody tr td label {
                margin: 0px;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="thumbnail" style="background-color: #f5f5f5; margin-bottom: 3px !important;">
                            <div class="panel-heading" style="padding: 0px 0px;">
                                <div class="row">
                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                        <h3 class="text-danger" style="margin: 0px;">Filing System</h3>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                                        <button type="button" id="ton" class="btn btn-primary" style="float: right; position: relative; right: 1px; margin-left: 10px;">
                                            <i class="fa fa-bars"></i>

                                        </button>
                                        <asp:Button ID="btnApprove" CssClass="btn btn-success pull-right btn-sm" ToolTip="Save"
                                            Text="  Back" OnClick="btnApprove_Click" runat="server" />

                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left: -2px;">
                    <div class="panel panel-default">
                        <div class="form-horizontal">
                            <div class="row">

                                <asp:HiddenField ID="hdnbtnValue" runat="server" />
                                <div id="div-show" style="display: block; float: right; width: calc(100% - 20px); margin: 0px 10px; position: relative; top: 0px;">
                                    <div class="row marg search-bg">
                                        <div class="form-horizontal">
                                            <div class="row" style="margin-bottom: 12px">
                                                <div class="col-sm-2">
                                                    <label for="email" class="padd linhei" style="padding-top: 1px;">
                                                        Year</label>

                                                    <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                        class="form-control ">
                                                    </asp:DropDownList>
                                                    <label for="email" class="padd linhei" style="padding-top: 1px;">
                                                        State:</label>

                                                    <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                        AutoPostBack="true" class="form-control ">
                                                    </asp:DropDownList>
                                                    <label for="email" class="padd linhei" style="padding-top: 1px;">
                                                        District:</label>

                                                    <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                        AutoPostBack="true" class="form-control " />
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="email" class="padd linhei" style="padding-top: 1px;">
                                                        Block:</label>

                                                    <div class="CheckBoxListCssClass" style="border: 1px solid #c1c1c1; width: 144px;">
                                                        <div style="overflow: auto; margin-top: 1px; height: 150px; width: 144px;">
                                                            <asp:CheckBoxList ID="ddlBlock" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                                AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>

                                                        <%-- <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                                    class="form-control " />--%>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="email" class="padd linhei" style="padding-top: 1px;">
                                                        Cluster:</label>

                                                    <div class="CheckBoxListCssClass" style="border: 1px solid #c1c1c1; width: 144px;">
                                                        <div style="overflow: auto; margin-top: 1px; height: 150px; width: 144px;">
                                                            <asp:CheckBoxList ID="ddlVillage" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged"
                                                                AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>

                                                        <%--<asp:DropDownList ID="ddlVillage" runat="server" AutoPostBack="true" class="form-control "
                                                                    OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged" />--%>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="email" class="padd linhei" style="padding-top: 1px;">
                                                        Village:</label>

                                                    <div class="CheckBoxListCssClass" style="border: 1px solid #c1c1c1; width: 144px;">
                                                        <div style="overflow: auto; margin-top: 1px; height: 150px; width: 144px;">
                                                            <asp:CheckBoxList ID="chkVillage" RepeatDirection="Vertical"
                                                                AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>


                                                    </div>

                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="email" class="padd linhei" style="padding-top: 1px;">
                                                        FC:</label>

                                                    <asp:DropDownList ID="ddlFc" runat="server" class="form-control" />


                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" OnClick="btnSerach_Click" Style="margin-top: 40px; margin-right: 102px;"
                                                        class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />
                                                </div>
                                            </div>






                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12" style="padding: 0px 10px; margin-top: 8px;">
                                    <asp:Panel ID="pnlMain" runat="server">
                                        <asp:UpdatePanel runat="server" ID="UpdatedddddddPanel1">
                                            <ContentTemplate>
                                                <div class="form-horizontal">
                                                    <div style="height: 500px; overflow: auto;" align="center">
                                                        <div>
                                                            <div class="row" style="width: 100%">
                                                                <asp:GridView ID="GVSealSign" OnRowDataBound="GVSealSign_OnRowDataBound" OnRowCommand="GVSealSign_RowCommand" runat="server"
                                                                    CssClass="table table-striped table-bordered table-hover" DataKeyNames="SchoolCode,SendFile,SealSign_DiseCode,ReceiveFile,DODate,BODate,DORecieveDate,BORecieveDate"
                                                                    AutoGenerateColumns="False" Font-Names="Arial" AllowPaging="true" PageSize="100"
                                                                    OnPageIndexChanging="GVSealSign_PageIndexChanging" Font-Size="12px" Width="100%">
                                                                    <EmptyDataTemplate>
                                                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                            Data not found
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Village Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblVillageName" ForeColor="Black" runat="server" Text='<%# Eval("VillageName") %>'></asp:Label>
                                                                                <asp:Label ID="lblClusterCode" ForeColor="Black" Visible="false" runat="server" Text='<%# Eval("ClusterCode") %>'></asp:Label>
                                                                                <asp:Label ID="lblSealSign" ForeColor="Black" Visible="false" runat="server" Text='<%# Eval("SealSignNew") %>'></asp:Label>
                                                                                <asp:Label ID="lblAprrove" ForeColor="Black" Visible="false" runat="server" Text='<%# Eval("ApprovalStatus") %>'></asp:Label>


                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="padding-lef" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="School Name">
                                                                            <ItemTemplate>

                                                                                <asp:Label ID="Label1" ForeColor="Black" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="padding-lef" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="School Level">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPrimarySecond" ForeColor="Black" runat="server" Text='<%# Eval("PS_UPS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="padding-lef" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Form No">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkPdf1" BackColor="Transparent" CommandArgument="<%# Container.DataItemIndex %>" Text='<%# Eval("SealSign_DiseCode") %>' OnClick="btnMain1_Click" runat="server"
                                                                                    Width="3%" Height="5%"></asp:LinkButton>
                                                                                <asp:Label ID="lblSchoolCode" Visible="false" BackColor="Transparent" runat="server"
                                                                                    Text='<%# Bind("SchoolCode") %>' CssClass="fa fa-file-pdf-o"></asp:Label>


                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Form Generation Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDODatte" ForeColor="Black" runat="server" Text='<%# Eval("SealSignGenerateDate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Form by IA to BO">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDODiiate" ForeColor="Black" runat="server" Text='<%# Eval("DODate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Form by BO to FC">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblBoDiiate" ForeColor="Black" runat="server" Text='<%# Eval("BODate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="S&S Received Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSealSignDate" ForeColor="Black" runat="server" Text='<%# Eval("SealSignDate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="padding-lef" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="S&S Validation Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSealSigtffnDate" ForeColor="Black" runat="server" Text='<%# Eval("ApproveDate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="padding-lef" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Form by FC to BO">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDORecieveDate" ForeColor="Black" runat="server" Text='<%# Eval("BORecieveDate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Form by BO to IA">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblBoRecieveDate" ForeColor="Black" runat="server" Text='<%# Eval("DORecieveDate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Filing Completed">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblBoRecievettDate" ForeColor="Black" runat="server" Text='<%# Eval("FilingCompleted") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Action Button">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkbtnsend" runat="server" CssClass="btn btn-success btn-sm" Text="Send" CommandArgument="<%# Container.DataItemIndex %>" CommandName="LnkSend"></asp:LinkButton>
                                                                                &nbsp;
                                          <asp:LinkButton ID="lnkbtnRecieve" runat="server" CssClass="btn btn-primary btn-sm" Text="Recieve" CommandArgument="<%# Container.DataItemIndex %>" CommandName="LnkRecieve"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                    </Columns>
                                                                    <PagerStyle CssClass="pagination-ys" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GVSealSign" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hdnsend" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnrecieve" runat="server"></asp:HiddenField>
    <asp:Panel ID="pnlNiyamaWali" runat="server" Style="display: none;">
        <%=STRPRINTCONTENT2%>
    </asp:Panel>
    <asp:ModalPopupExtender ID="MpexdrPopUp" runat="server" BackgroundCssClass="modalBackground "
        PopupControlID="PnlDistrict" CancelControlID="CancelButton" TargetControlID="HdnFild7">
    </asp:ModalPopupExtender>
    <asp:HiddenField ID="HdnFild7" runat="server"></asp:HiddenField>
    <asp:Panel CssClass="model-wid mod-posi" Style="display: none; width: 76% !important; margin-top: 93px !important;"
        ID="PnlDistrict" runat="server">
        <div style="width: 100%; height: auto; background-color: #f1f1f1">
            <div class="modal-header" style="background-color: #3ac0f2; color: White;">
                <asp:Label ID="lblMsg" runat="server" Visible="false" ForeColor="Black" Font-Names="Verdana"
                    Font-Size="11px"></asp:Label>
                <asp:ImageButton ID="CancelButton" ImageUrl="~/images/close-29.png" Style="float: right;"
                    Width="3%" Height="3%" runat="server" />
            </div>
            <div class="modal-body">
                <div class="form-horizontal">
                    <asp:LinkButton ID="lnkDownload" Visible="false" runat="server" Text="Export to Excel"
                        Style="float: right;" ToolTip="Download"></asp:LinkButton>
                    <div style="height: 350px; overflow: auto; width: 99%;" align="center">
                        <div>
                            <div class="Row" style="width: 100%">
                                <asp:GridView ID="PopUpGrid" AutoGenerateColumns="true" runat="server" ForeColor="Black"
                                    AllowPaging="true" PageSize="500" ShowHeader="true" CssClass="table table-striped table-bordered table-hover"
                                    Width="100%">
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
                                    <HeaderStyle BackColor="#C1C1C1" Wrap="true" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--  <div class="modal-footer">
                                                    <asp:Button ID="CancelButton" runat="server" CssClass="btn bgm-cyan" Text="Close"
                                                        ToolTip="Close" Style="float: none;"></asp:Button></div>--%>
        </div>
    </asp:Panel>


</asp:Content>

