<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FrmSealSign.aspx.cs" Inherits="FrmSealSign" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--  <meta http-equiv="Content-Type" content="text/html;charset=UTF-8"></meta>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%--<meta http-equiv="Content-Type" content="text/html;charset=UTF-8"></meta>--%>
    <script type="text/javascript" src="js/jquery-2.1.0.js"></script>
    <script src="js/Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function PrintPanel2() {
            debugger;
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


    <style type="text/css">
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

        input[type="radio"], input[type="checkbox"] {
            margin: 4px 7px 0px !important;
            margin-top: 1px !important;
            line-height: normal !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="container-fluid">

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="thumbnail" style="background-color: #f5f5f5; margin-bottom: 3px !important;">
                        <div class="panel-heading" style="padding: 0px 0px;">
                            <div class="row">
                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                    <h3 class="text-danger" style="margin: 0px;">Seal Sign Generation</h3>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12" style="padding-right: 5px;">


                                    <button type="button" id="ton" class="btn btn-primary" style="margin: 0px 10px; float: right; height: 30px;">
                                        <i class="fa fa-bars"></i>

                                    </button>
                                    <asp:Button ID="btnApprove" CssClass="btn btn-success pull-right btn-sm " ToolTip="Save"
                                        Text="  Back" OnClick="btnApprove_Click" runat="server" />
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 5px;">

                    <div class="form-horizontal">
                        <%--<div class="row">--%>

                        <asp:HiddenField ID="hdnbtnValue" runat="server" />
                        <div id="div-show" style="display: block; float: right; width: calc(100% - 20px); margin: 0px 10px; position: relative; top: -8px;">
                            <div class="row marg search-bg" style="padding: 5px 0px;">
                                <div class="form-horizontal">
                                    <div class="row" style="padding: 5px 15px;">
                                        <div class="col-sm-2 ">
                                            <div class="form-group">
                                                <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                    Year</label>

                                                <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                    class="form-control ">
                                                </asp:DropDownList>

                                                <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                    State:</label>

                                                <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                    AutoPostBack="true" class="form-control ">
                                                </asp:DropDownList>

                                                <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                    District:</label>

                                                <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                    AutoPostBack="true" class="form-control " />

                                            </div>
                                        </div>

                                        <%-- <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                <div class="form-group">
                                                    
                                                </div>
                                            </div>--%>
                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                Block:</label>
                                            <div class="form-group">

                                                <div class="col-sm-8 padd">
                                                    <div class="padd CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                        <div style="overflow: auto; margin-top: 2px; height: 150px;">
                                                            <asp:CheckBoxList ID="ddlBlock" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                                AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                    <%-- <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                                    class="form-control " />--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                Cluster:</label>
                                            <div class="form-group">

                                                <div class="col-sm-8 padd">
                                                    <div class="padd CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                        <div style="overflow: auto; margin-top: 2px; height: 150px;">
                                                            <asp:CheckBoxList ID="ddlVillage" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged"
                                                                AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                    <%--<asp:DropDownList ID="ddlVillage" runat="server" AutoPostBack="true" class="form-control "
                                                                    OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged" />--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                Village:</label>
                                            <div class="form-group">

                                                <div class="col-sm-8 padd">
                                                    <div class="padd CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                        <div style="overflow: auto; margin-top: 2px; height: 150px;">
                                                            <asp:CheckBoxList ID="chkVillage" RepeatDirection="Vertical"
                                                                AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                    <%--<asp:DropDownList ID="ddlVillage" runat="server" AutoPostBack="true" class="form-control "
                                                                    OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged" />--%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                FC:</label>
                                            <div class="form-group">

                                                <div class="col-sm-8 padd">
                                                    <asp:DropDownList ID="ddlFc" runat="server" class="form-control" />
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">

                                            <div class="form-group">
                                                <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" OnClick="btnSerach_Click"
                                                    class="btn btn-danger btn-paddd" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%-- </div>--%>
                        </div>
                        <div class="col-lg-12" style="padding: 0px 10px;">
                            <asp:Panel ID="pnlMain" runat="server">
                                <asp:UpdatePanel runat="server" ID="UpdatedddddddPanel1">
                                    <ContentTemplate>
                                        <div class="form-horizontal">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 0px;">
                                                <div style="height: 350px; overflow: auto; width: 100%;" align="center">
                                                    <div>
                                                        <div class="row" style="width: 100%">
                                                            <asp:GridView ID="GVSealSign" OnRowDataBound="GVSealSign_OnRowDataBound" runat="server"
                                                                CssClass="table table-striped table-bordered table-hover" DataKeyNames="SchoolCode"
                                                                AutoGenerateColumns="False" Font-Names="Arial" AllowPaging="true" PageSize="50"
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
                                                                    <asp:TemplateField HeaderText="School Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblClusterCode" Visible="false" runat="server" Text='<%# Eval("ClusterCode") %>'></asp:Label>
                                                                            <asp:Label ID="Label1" ForeColor="Black" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" HorizontalAlign="left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Village Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVillageName" ForeColor="Black" runat="server" Text='<%# Eval("VillageName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" HorizontalAlign="left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="School Level">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPrimarySecond" ForeColor="Black" runat="server" Text='<%# Eval("PS_UPS") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" HorizontalAlign="left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="#Children Pending Seal-Sign">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lblCategory" runat="server" OnClick="OOD2Dtargetmet_Click" Text='<%# Eval("Children_Pending_Seal_Sign") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Generate">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkGenerate" runat="server" OnClick="LnkGenerate_Click" Text="Generate"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Last Visit Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEnrolmentDate" ForeColor="Black" runat="server" Text='<%# Eval("Lastvisit") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Generate Seal-sign pdf" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkPdf" BackColor="Transparent" OnClick="btnMain_Click" runat="server"
                                                                                Width="3%" Height="5%" CssClass="fa fa-file-pdf-o"></asp:LinkButton>
                                                                            <asp:Label ID="lblSchoolCode" Visible="false" BackColor="Transparent" runat="server"
                                                                                Text='<%# Bind("SchoolCode") %>' CssClass="fa fa-file-pdf-o"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Seal-sign Form ">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlRe" CssClass="form-control" runat="server">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="padding-lef" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Generate Seal-sign pdf">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkPdf1" BackColor="Transparent" OnClick="btnMain1_Click" runat="server"
                                                                                Width="3%" Height="5%" CssClass="fa fa-file-pdf-o"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle CssClass="pagination-ys" />
                                                            </asp:GridView>
                                                        </div>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GVSealSign" />
        </Triggers>
    </asp:UpdatePanel>
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
            <div class="modal-header" style="background-color: #dddd; color: White; padding: 5px;">
                <asp:Label ID="lblMsg" runat="server" Visible="false" ForeColor="Black" Font-Names="Verdana"
                    Font-Size="11px"></asp:Label>
                <asp:ImageButton ID="CancelButton" ImageUrl="~/images/close-29.png" Style="float: right;"
                    Width="3%" Height="3%" runat="server" />
            </div>
            <div class="modal-body">
                <div class="form-horizontal">
                    <asp:LinkButton ID="lnkDownload" Visible="false" runat="server" Text="Export to Excel"
                        Style="float: right;" ToolTip="Download"></asp:LinkButton>
                    <div style="height: 350px; overflow: auto; width: 100%;" align="center">
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
