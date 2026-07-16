<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Culture="en-GB"
    CodeFile="frmNewSipAnnual.aspx.cs" Inherits="frmSipAnnual" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">



    <style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
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

                .pagination-ys table > tbody > tr > td > a,
                .pagination-ys table > tbody > tr > td > span {
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

                .pagination-ys table > tbody > tr > td:first-child > a,
                .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a,
                .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover,
                .pagination-ys table > tbody > tr > td > span:hover,
                .pagination-ys table > tbody > tr > td > a:focus,
                .pagination-ys table > tbody > tr > td > span:focus {
                    color: Black;
                    background-color: #eeeeee;
                    border-color: #dddddd;
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


        input[type="radio"], input[type="checkbox"] {
            margin: 4px 7px 0px !important;
            margin-top: 1px !important;
            line-height: normal !important;
        }

        .disp_flex {
            display: flex;
            justify-content: space-between;
            align-items: center;
            gap: 12px;
            padding: 5px 12px;
        }

        table#ctl00_MainContent_rblBlockType tbody tr td label {
            margin-bottom: 0px
        }

        table#ctl00_MainContent_rblBlockType {
            margin-top: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%-- <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>--%>
    <div class="container-fluid">



        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="panel panel-default " style="background-color: #f5f5f5; margin-bottom: 0px !important;">
                    <div class="panel-heading" style="padding: 0px 0px;">
                        <div class="row">
                            <div class="disp_flex">
                                <div id="Div2" runat="server">
                                    <div class="">
                                        <h3 class="text-danger" style="margin: 0px;"> Annual Data 
                                        </h3>
                                    </div>
                                </div>
                                <div id="Div3" runat="server">
                                    <div class="form-group1">

                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Export to Excel" OnClick="btnImport_Click" class="pull-right" Style="margin-left: 12px;"></asp:LinkButton>
                                        <%--</div>
                                           
                                           <span class="pull-right" style="font-size: 17px;"></span>
                                        <div id="Div1" class="col-lg-2 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                        <div class="form-group">--%>

                                        <asp:LinkButton ID="lnkCSV" runat="server" Text="Export to CSV" OnClick="btnCSV_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 text-left" style="padding-right: 5px; margin-top: 10px">
            <div style="overflow: auto; margin-top: 0px; height: 603px;">
                <div class="thumbnail" style="background-color: rgba(20, 18, 18, 0.59); float: left; margin: 0px">
                  <%--  <div class="li-width" style="min-height: 110px;">
                        <img src="images/business-report.jpg" width="100%" />--%>
                        <%-- <div style="width:30%; float:left;">
            <img src="images/report-icon.gif" width="100%" />
        </div>
        <div style="width:70%; float:left; height:100%; background-color:Blue; " >
            Reports
            </div>--%>
                <%--    </div>--%>
                    <ul class="nav navbar-nav" style="margin: 0px; height: 481px;">
                        <li class=" active li-width">
                            <asp:LinkButton ID="Button3" runat="server" Text="School Raw" Style="color: white;"
                                OnClick="btnSerach_Click"></asp:LinkButton>
                        </li>

                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton2" runat="server" Text="Village Raw" Style="color: white;"
                                OnClick="btnVillageRaw_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton3" runat="server" Text="Door To Door  Raw" Style="color: white;"
                                OnClick="btnD2d_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton4" runat="server" Text="Door To Door  Target" Style="color: white;"
                                OnClick="btnD2dTarget_Click"></asp:LinkButton>
                        </li>
                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton7" runat="server" Text="Target Never Enrolled" Style="color: white;"
                                OnClick="btnD2dTargetEnrolled_Click"></asp:LinkButton>
                        </li>

                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton8" runat="server" Text="Target Drop Out" Style="color: white;"
                                OnClick="btnD2dTargetDrop_Click"></asp:LinkButton>
                        </li>

                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton5" runat="server" Text="D2d Target Summary" Style="color: white;"
                                OnClick="btnD2dTargetSummary_Click"></asp:LinkButton>
                        </li>

                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton6" runat="server" Text="D2d Target Year Wise" Style="color: white;"
                                OnClick="btnD2dTargetSummaryYear_Click"></asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12" style="padding-left: 5px; margin-top: 10px">
            <div class="thumbnail" style="background-color: #f5f5f5; float: left; width: 100%;">
                <div class="panel panel-default">
                    <div class="form-horizontal">
                        <div class="row">
                            <div id="div-show-new" style="text-align: left; right: 11px;">
                                <div class="row marg search-bg" style="padding: 15px 5px 0px 5px;">
                                    <div class="form-horizontal">
                                        <div class="row">

                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <div style="width: 100%; float: left; height: auto; padding: 0px 5px;">
                                                        <label for="email" class="col-sm-4 padd linhei">
                                                            Block Type:</label>
                                                        <div class="col-sm-8 padd ">
                                                            <asp:RadioButtonList ID="rblBlockType" AutoPostBack="true" OnSelectedIndexChanged="rblBlockType_SelectedIndexChanged" CssClass="cr-icon" ForeColor="Black" RepeatDirection="Horizontal" runat="server">
                                                                <asp:ListItem Text="EG Block" Selected="True" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Govt Block" Value="2"></asp:ListItem>
                                                            </asp:RadioButtonList>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" style="margin-bottom: 14px;">
                                                    <div style="width: 100%; float: left; height: auto; padding: 0px 5px;">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Year:</label>
                                                        <div class="col-sm-9 padd" style="padding-left: 10px;">
                                                            <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                                AutoPostBack="true" class="form-control ">
                                                            </asp:DropDownList>
                                                        </div>
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
                                            <div id="Div1" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px; padding-left: 10px;">
                                                        Panchayat:</label>
                                                    <div class="col-sm-8 padd CheckBoxListCssClass" style="border: 1px solid #c1c1c1">
                                                        <div style="overflow: auto; margin-top: 2px; height: 79px;">
                                                            <asp:CheckBoxList ID="ddlPanchayat" RepeatDirection="Vertical" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:CheckBoxList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div17" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
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

                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        User:
                                                    </label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlUser" runat="server" class="form-control ">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        From</label>
                                                    <div class="col-sm-9 padd" style="padding-right: 7px;">
                                                        <asp:TextBox runat="server" ID="txtDate" class="form-control" onkeypress="return false;"></asp:TextBox>
                                                        <ajax:CalendarExtender ID="CalendarExtenderTourdate" runat="server" Enabled="True"
                                                            Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight">
                                                        </ajax:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        To</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:TextBox runat="server" ID="txtTodate" class="form-control" onkeypress="return false;"></asp:TextBox>
                                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            TargetControlID="txtTodate" PopupPosition="BottomRight">
                                                        </ajax:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Gender:
                                                    </label>
                                                    <div class="col-sm-9 padd" style="padding-right: 7px;">
                                                        <asp:DropDownList ID="ddlGender" runat="server" class="form-control ">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Male</asp:ListItem>
                                                            <asp:ListItem Value="2">Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12 table table-hover " style="padding: 0px; margin-top: 8px">
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
                                                    <span style="float: right; color: Black; margin-left: 12px;">
                                                        <asp:LinkButton ID="lnkTarget" Visible="false" OnClick="lnkTarget_Click" runat="server">Target Raw Data</asp:LinkButton>
                                                    </span>
                                                </div>
                                                <div style="height: 290px; overflow: auto; width: 99%;" align="center">

                                                    <div class="row" style="width: 99%">
                                                        <asp:GridView ID="gvD2d" runat="server" Visible="false"
                                                            AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                                                            Font-Size="12px" Width="150%">
                                                            <EmptyDataTemplate>
                                                                <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                    Data not found
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <FooterStyle CssClass="FooterStyle" />
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                            <AlternatingRowStyle BackColor="#f1f1f1" />
                                                            <PagerStyle CssClass="paging" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="District Name" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDistrictName" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("DistrictName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="District Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDistrictNaf1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server" Text='<%#Eval("DistrictCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Block Name" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBlockName" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("BlockName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Block Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDistdrictNaf1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server" Text='<%#Eval("BlockCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Panchayat Name" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPanchayatName" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("PanchayatName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Panchayat Code" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPanchayatdName" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("PanchayatCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Village Name" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVillageName" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("VillageName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Village Code" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVillageName" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("VillageCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UniqueId" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lUniqueId" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("UniqueId") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Survay Date" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSurvayDate" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("SurvayDate") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Mauhalla" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMauhalla" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Mauhalla") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="House" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHouse" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("House") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Child Name" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHouse2" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("ChildName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Father Name" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ddlEmployeeCode" class="labelGrid" ForeColor="Black" ItemStyle-ForeColor="#333"
                                                                            runat="server" Text='<%# Eval("FathersName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Gender" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpLWP" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Age" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Txtunit" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Age") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Age Proof" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHRA" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("AgeProof") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Social Category" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSalaryPayable" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("SocialCategory") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Family Occupation" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBasic" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("FamilyOccupation") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Eduation Status" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHRAyy" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("EduationStatus") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="School Name" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblConveyance" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("SchoolName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Class" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAllowance" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Reason" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMedical" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Enrollment Category" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGrossSalary" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("EnrollmentCategory") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Last session status" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGrossSal88ary" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("lastsessionstatus") %>'></asp:Label>


                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Final status" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGrosssSal88ary" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("Finalstatus") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="EnrollUniqueId" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEnrollUniqueId" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("EnrollUniqueId") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Session" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEnrollUSessioneId" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("Session") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>

                                                        <asp:GridView ID="gvD2dTatget" runat="server" Visible="false"
                                                            AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                                                            Font-Size="12px" Width="150%">
                                                            <EmptyDataTemplate>
                                                                <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                    Data not found
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <FooterStyle CssClass="FooterStyle" />
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                            <AlternatingRowStyle BackColor="#f1f1f1" />
                                                            <PagerStyle CssClass="paging" />
                                                            <Columns>


                                                                <asp:TemplateField HeaderText="Mauhalla" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMauhalla" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Mauhalla") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="House" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHouse" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("House") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="UniqueId" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lUniqueId" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("UniqueId") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Child Name" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHouse2" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("ChildName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Father's Name" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ddlEmployeeCode" class="labelGrid" ForeColor="Black" ItemStyle-ForeColor="#333"
                                                                            runat="server" Text='<%# Eval("FathersName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>





                                                                <asp:TemplateField HeaderText="Gender" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpLWP" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Age" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Txtunit" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Age") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Age Proof" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHRA" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("AgeProof") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="C" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSalaryPayable" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("C") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="F" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBasic" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("F") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Form -6" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHRAyy" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("F6") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="E" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblH3RAyy" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("E") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="I" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblH6Ayy" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("I") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="School Name" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblConveyance" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("SchoolName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Class" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAllowance" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sr.No" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMedical" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("SR") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Enr Date." Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGrossSalary" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("E") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>



                                                        <asp:GridView ID="gvD2dTargetDropOut" runat="server" Visible="false"
                                                            AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                                                            Font-Size="12px" Width="150%">
                                                            <EmptyDataTemplate>
                                                                <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                    Data not found
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <FooterStyle CssClass="FooterStyle" />
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                            <AlternatingRowStyle BackColor="#f1f1f1" />
                                                            <PagerStyle CssClass="paging" />
                                                            <Columns>


                                                                <asp:TemplateField HeaderText="Mauhalla" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMauhalla" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Mauhalla") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="House" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHouse" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("House") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="UniqueId" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lUniqueId" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("UniqueId") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Child Name" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHouse2" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("ChildName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Father's Name" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ddlEmployeeCode" class="labelGrid" ForeColor="Black" ItemStyle-ForeColor="#333"
                                                                            runat="server" Text='<%# Eval("FathersName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>





                                                                <asp:TemplateField HeaderText="Gender" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpLWP" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Age" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Txtunit" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Age") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Age Proof" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHRA" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("AgeProof") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="DO School " Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblConveyance" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("SchoolName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DO Class" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAllowance" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="DO Reason" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAllowance" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="C" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSalaryPayable" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("C") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="F" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBasic" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("F") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Form -6" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHRAyy" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("F6") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="E" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblH3RAyy" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("E") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="I" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblH6Ayy" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("I") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Sr.No" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMedical" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("SR") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Class" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMedicael" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Class1") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Enr Date." Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGrossSalary" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("E") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>

                                                        <asp:GridView ID="GV_DynamicGrid" OnRowCreated="GV_DynamicGrid_RowCreated" runat="server" Visible="false" ForeColor="Black" AllowPaging="true"
                                                            PageSize="100" ShowHeader="true" CssClass="table table-striped table-bordered table-hover"
                                                            Width="100%">
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
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /#wrapper -->
                <!-- /#wrapper -->
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(function () {
            $('#datetimepicker4').datetimepicker();
        });
    </script>
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
