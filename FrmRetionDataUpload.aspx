<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" Culture="en-GB" AutoEventWireup="true"
    CodeFile="FrmRetionDataUpload.aspx.cs" Inherits="FrmRetionDataUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" language="javascript">
        function Fun1() {
            alert("Panchayat code should be equal to village code")
        }

        function Fun2() {
            alert("panchait name should be equal to village name")
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="panel panel-default " style="background-color: #f5f5f5; margin-bottom: 0px !important;">
                    <div class="panel-heading" style="padding: 5px 0px;">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                <div id="Div2" class="col-lg-10 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                    <div class="">
                                        <h3 class="text-danger" style="margin: 0px;">Retention 
                                        </h3>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">

            <div class="col-md-12">
                <div class="panel panel-default " style="background-color: #f5f5f5; margin-bottom: 0px !important; margin-top: 6px;">
                    <div class="panel-heading" style="padding: 9px 0px;">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
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
                                            <asp:DropDownList ID="ddlDistrict" runat="server"
                                                AutoPostBack="true" class="form-control " />
                                        </div>
                                    </div>


                                </div>


                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">

                                    <asp:Button ID="Button1" runat="server" Text="Export format" OnClick="btnNewImport_Click" CssClass="btn-danger btn-sm" />


                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-default " style="background-color: #f5f5f5; margin-bottom: 0px !important; margin-top: 6px;">
                        <div class="panel-heading" style="padding: 10px 0px;">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                        <asp:Button ID="btnImport" runat="server" Text="Import Retention Data" CssClass="btn-danger btn-sm Pull" OnClick="btnImport_Click" />
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn-danger btn-sm" Visible="false"
                                            OnClick="btnApprove_Click" />
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">

                                        <asp:LinkButton ID="LnkExport" runat="server" Text="Retention Target" CssClass="pull-right"
                                            OnClick="LnkExport_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <cc1:ModalPopupExtender ID="ModalAlert" runat="server" TargetControlID="hdn_alertmodal"
            PopupControlID="pnl_alert" CancelControlID="btn_cancelalert" BackgroundCssClass="ModalPopupBG">
        </cc1:ModalPopupExtender>
        <asp:HiddenField ID="hdn_alertmodal" runat="server" />
        <asp:Panel ID="pnl_alert" runat="server" Style="display: none; background-color: #F1F1F1;"
            BorderColor="#D9D9D9" BorderStyle="Ridge" BorderWidth="2px" Width="380px">
            <div class="divbgs" style="padding: 0 0 10px 0;">
                <div class="longnamecsspop" style="background-color: Black; text-align: left; font-family: arial, Helvetica, sans-serif; color: White; font-size: 19px; width: 100%; padding: 4px 10px 0 10px; margin-left: auto; margin-right: auto; height: 27px;">
                    Alert !
                </div>
                <div style="width: 373px; text-align: center">
                    <div style="width: 100%; height: 8px;">
                    </div>
                    <asp:Label ID="lbl_messages" runat="server" CssClass="LabelHeader" Font-Bold="True"
                        Font-Size="12px" Style="width: 316px"></asp:Label>
                </div>
                <div style="text-align: center; padding-top: 10px;">
                    <asp:Button ID="btn_cancelalert" runat="server" CssClass="btncss" Text="  OK  " Style="width: 74px" />
                </div>
            </div>
        </asp:Panel>
</asp:Content>
