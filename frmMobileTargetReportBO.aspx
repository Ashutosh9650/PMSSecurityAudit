<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="frmMobileTargetReportBO.aspx.cs" Culture="en-GB" Inherits="frmMobileTargetReportBO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<asp:UpdatePanel ID="updmain" runat="server">--%>
    <%--<ContentTemplate>--%>

    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="thumbnail" style="background-color: #f5f5f5; margin-bottom: 0px !important;">
                    <div class="panel-heading" style="padding: 0px;">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <h3 class="text-danger" style="margin: 0px;">Activity Report BO
                                    <span class="pull-right" style="font-size: 17px;    margin-top: 5px;">
                                        <asp:LinkButton ID="btnexcel" runat="server" Text="Export to CSV" OnClick="Export_To_Excel"></asp:LinkButton>

                                    </span>
                                </h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 text-left" style="padding-right: 0px; margin-top: 10px;">
            <div style="overflow: auto; margin-top: 0px; height: 480px;">
                <div class="thumbnail" style="background-color: rgba(20, 18, 18, 0.59); float: left;">
                   <%-- <div class="li-width" style="min-height: 110px;">
                        <img src="images/business-report.jpg" width="100%" />

                    </div>--%>
                    <ul class="nav navbar-nav" style="margin: 0px">
                        <li class=" active li-width">
                            <asp:LinkButton ID="Button3" runat="server" Text="Activity Village Wise" Style="color: white;"
                                OnClick="PMS_Click"></asp:LinkButton>

                        </li>

                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton1" runat="server" Text="Activity School Wise" Style="color: white;"
                                OnClick="PMSSchool_Click"></asp:LinkButton>

                        </li>

                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton2" runat="server" Text="Activity Office Wise" Style="color: white;"
                                OnClick="PMSOffice_Click"></asp:LinkButton>

                        </li>


                    </ul>
                </div>
            </div>
        </div>

        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12" style="margin-top: 10px; padding-left: 10px;">
            <div class="thumbnail" style="background-color: #f5f5f5; float: left; width: 100%;">
                <div class="panel panel-default" style="margin-bottom:0px">
                    <div class="form-horizontal">
                        <div class="row">
                            <div id="div-show-new">
                                <div class="row marg search-bg">
                                    <div class="form-horizontal">
                                        <%-- <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>--%>
                                        <div class="row">

                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px; padding-right: 15px;">
                                                        Year:</label>
                                                    <div class="col-sm-9 padd" style="padding: 0px 8px 0px 12px;">
                                                        <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                            AutoPostBack="true" class="form-control ">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        State:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass " style="border: 1px solid #c1c1c1">

                                                        <div style="overflow: auto; margin-top: 2px; height: 79px;">
                                                            <asp:CheckBoxList ID="ChkState" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        District:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass " style="border: 1px solid #c1c1c1">
                                                        <div style="overflow: auto; margin-top: 2px; height: 79px;">
                                                            <asp:CheckBoxList ID="chkDistrict" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Block:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                        <div style="overflow: auto; margin-top: 2px; height: 79px;">
                                                            <asp:CheckBoxList ID="chkBlock" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div id="Div5" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Cluster:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                        <div style="overflow: auto; margin-top: 2px; height: 79px;">

                                                            <asp:CheckBoxList ID="chkCluster" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlCluster_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div2" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Panchayat:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                        <div style="overflow: auto; margin-top: 2px; height: 79px;">
                                                            <asp:CheckBoxList ID="ddlPanchayat" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div3" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Village:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                        <div style="overflow: auto; margin-top: 2px; height: 79px;">
                                                            <asp:CheckBoxList ID="chkVillage" RepeatDirection="Vertical" runat="server">
                                                            </asp:CheckBoxList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server" visible="false">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        ApproveType:
                                                    </label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:RadioButtonList RepeatDirection="Vertical" ForeColor="Black" CssClass="cr-icon" ID="rblApprove" runat="server">

                                                            <asp:ListItem Selected="True" Value="1">Submit(FC)</asp:ListItem>
                                                            <asp:ListItem Value="2">Approve(BO)</asp:ListItem>
                                                            <asp:ListItem Value="3">Approve(IO)</asp:ListItem>

                                                        </asp:RadioButtonList>

                                                    </div>
                                                </div>
                                            </div>

                                        </div>



                                    </div>
                                </div>

                                <div class="col-lg-12 table table-hover " style="padding: 0px;">
                                    <asp:Panel ID="pnlMain" Enabled="true" runat="server">
                                        <div class="form-horizontal">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                <div class="panel-default search-bg" style="height: 30px">
                                                    <span style="float: left; color: Black; margin-left: 12px;">
                                                        <asp:Label ID="lblTotal" Text="Total:" runat="server"></asp:Label>
                                                    </span>
                                                    <span style="float: left; color: Black; margin-left: 12px;">
                                                        <asp:Label ID="lblTotalCount" ForeColor="#737272" Font-Bold="true" runat="server"></asp:Label>
                                                    </span>
                                                </div>
                                                <div style="height: 290px; overflow: auto; width: 99%;" align="center">
                                                    <div>



                                                        <div class="row" style="width: 100%">
                                                            <asp:GridView ID="GV_DynamicGrid2" runat="server" ForeColor="Black" AllowPaging="true"
                                                                PageSize="100" ShowHeader="true"
                                                                Visible="false" CssClass="table table-striped table-bordered table-hover" Width="100%">
                                                                <EmptyDataTemplate>
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
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>


                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <cc1:ModalPopupExtender ID="MpexdrDistrict" runat="server" BackgroundCssClass="modalBg "
        CancelControlID="CancelButton" PopupControlID="PnlDistrict" TargetControlID="HdnFild">
    </cc1:ModalPopupExtender>

    <asp:HiddenField ID="HdnFild" runat="server"></asp:HiddenField>

    <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important; margin-top: 88px !important;" ID="PnlDistrict" runat="server">

        <div style="width: 100%; height: auto; background-color: #f1f1f1">
            <div class="modal-header" style="background-color: #3ac0f2; color: White;">
                <h4 class="modal-title" style="forecolor: White">Life Skill Game</h4>
            </div>
            <div class="modal-body">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                <div class="form-horizontal" role="form">
                    <div class="form-group">
                        <label class="control-label col-sm-4 lab-text-left">Life Skill Game 1:</label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="l1" Font-Bold="true"></asp:Label>

                        </div>
                    </div>
                    <div class="form-group" id="statediv" runat="server">

                        <asp:Label ID="Label10" class="control-label col-sm-4 lab-text-left" runat="server" Text="Life Skill Game 2:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" Font-Bold="true" ForeColor="Black" ID="l2"></asp:Label>



                        </div>
                    </div>

                    <div class="form-group" id="distdiv" runat="server">

                        <asp:Label ID="lbldist" class="control-label col-sm-4 lab-text-left" runat="server" Text="Life Skill Game 3:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="l3" Font-Bold="true"></asp:Label>


                        </div>
                    </div>

                    <div class="form-group" id="blockdiv" runat="server">

                        <asp:Label ID="lblBlock" class="control-label col-sm-4 lab-text-left" runat="server" Text="Life Skill Game 4:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="l4" Font-Bold="true"></asp:Label>


                        </div>
                    </div>

                    <div class="form-group" id="partnerdiv" runat="server">

                        <asp:Label ID="lblpartner" class="control-label col-sm-4 lab-text-left" runat="server" Text="Life Skill Game 5:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="l5" Font-Bold="true"></asp:Label>

                        </div>
                    </div>

                </div>


            </div>
            <div class="modal-footer">

                <asp:Button ID="CancelButton" runat="server" CssClass="btn bgm-cyan" Text="Close"
                    ToolTip="Close" Style="float: none;"></asp:Button>
            </div>
        </div>


    </asp:Panel>


    <cc1:ModalPopupExtender ID="MpexdrDistrict1" runat="server" BackgroundCssClass="modalBg "
        CancelControlID="CancelButton" PopupControlID="PnlDistrict1" TargetControlID="HiddenField1">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>

    <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important; margin-top: 88px !important;" ID="PnlDistrict1" runat="server">

        <div style="width: 100%; height: auto; background-color: #f1f1f1">
            <div class="modal-header" style="background-color: #3ac0f2; color: White;">
                <h4 class="modal-title" style="forecolor: White"></h4>
            </div>
            <div class="modal-body">
                <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                <div class="form-horizontal" role="form">
                    <div class="form-group">
                        <label class="control-label col-sm-4 lab-text-left">DRINKING WATER:</label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="A1" Font-Bold="true"></asp:Label>

                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-4 lab-text-left">TOILETS:</label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="A2" Font-Bold="true"></asp:Label>

                        </div>
                    </div>
                    <div class="form-group" id="Div6" runat="server">

                        <asp:Label ID="fff" class="control-label col-sm-4 lab-text-left" runat="server" Text="KITCHEN:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" Font-Bold="true" ForeColor="Black" ID="A3"></asp:Label>



                        </div>
                    </div>

                    <div class="form-group" id="Div7" runat="server">

                        <asp:Label ID="Label5" class="control-label col-sm-4 lab-text-left" runat="server" Text="ELECTRICITY:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="A4" Font-Bold="true"></asp:Label>


                        </div>
                    </div>

                    <div class="form-group" id="Div8" runat="server">

                        <asp:Label ID="Label7" class="control-label col-sm-4 lab-text-left" runat="server" Text="PLAY GROUND:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="A5" Font-Bold="true"></asp:Label>


                        </div>
                    </div>

                    <div class="form-group" id="Div9" runat="server">

                        <asp:Label ID="Label9" class="control-label col-sm-4 lab-text-left" runat="server" Text="SWINGS:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="A6" Font-Bold="true"></asp:Label>

                        </div>
                    </div>


                    <div class="form-group" id="Div10" runat="server">

                        <asp:Label ID="Label12" class="control-label col-sm-4 lab-text-left" runat="server" Text="BOUNDRY WALL:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="A7" Font-Bold="true"></asp:Label>

                        </div>

                    </div>
                    <div class="form-group" id="Div11" runat="server">

                        <asp:Label ID="Label14" class="control-label col-sm-4 lab-text-left" runat="server" Text="Books:"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="A8" Font-Bold="true"></asp:Label>

                        </div>
                    </div>
                    <div class="form-group" id="Div12" runat="server">

                        <asp:Label ID="Label16" class="control-label col-sm-4 lab-text-left" runat="server" Text="CLT_Kit :"></asp:Label>
                        <div class="col-sm-6">
                            <asp:Label runat="server" ID="A9" Font-Bold="true"></asp:Label>

                        </div>
                    </div>
                </div>
            </div>



            <div class="modal-footer">

                <asp:Button ID="Button1" runat="server" CssClass="btn bgm-cyan" Text="Close"
                    ToolTip="Close" Style="float: none;"></asp:Button>
            </div>
        </div>








    </asp:Panel>



    <%--        </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="btnexcel" />
           
            </Triggers>
            </asp:UpdatePanel>--%>
</asp:Content>

