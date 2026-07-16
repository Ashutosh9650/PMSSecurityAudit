<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FrmGovtTarget.aspx.cs" Inherits="FrmGovtTarget" %>

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



        
  .thumbnail ul
        {
            float: left;
            width: 100%;
            height: auto;
            margin: 0px;
            padding: 0px;
            list-style: none;
        }
        .thumbnail ul li
        {
            float: left;
            width: 100%;
            height: auto;
        }
        .thumbnail ul li a
        {
            float: left;
            width: 100%;
            height: auto;
            padding: 10px;
            border: 1px solid #ddd;
            color:#333;
        }
        .thumbnail ul li a:hover{
            text-decoration:none;
            color: red !important;
            background-color: #f1f1f1;
        }
    </style>
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
                                        <h3 class="text-danger" style="margin: 0px;">Government Data Update
                                        </h3>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>  

        <div class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 text-left" style="padding-right: 0px;margin-top:10px">
            <div style="overflow: auto; margin-top: 0px; height: 510px;">
                <div class="thumbnail" style="height: 490px;">

                    <ul style="margin: 0px">
                        <li class=" active li-width">
                            <asp:LinkButton ID="Button3" runat="server" Style="color: gren; color: blue;" OnClick="btnNewImport_Click" Text="Government D2D Target Upload format"></asp:LinkButton>


                        </li>

                        <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton1" runat="server" Style="color: gren; color: blue;" OnClick="btnNewImport1_Click" Text="Government Reporting- Data Update Format CSV"></asp:LinkButton>


                        </li>
                        <%-- <li class=" active li-width">
                            <asp:LinkButton ID="LinkButton5" runat="server"  Visible="false" style="color: gren;color: blue;" OnClick="btnNewImport2_Click"  Text="Government Reporting- Data Update Format Excel"
                               ></asp:LinkButton>

                              
                        </li>--%>


                        <li>
                            <asp:LinkButton ID="LinkButton2" runat="server" Style="color: gren; color: blue;" OnClick="LnkExport_Click" Text="Government Target D2D"></asp:LinkButton>

                        </li>

                        <%-- <li >
          
                                  <asp:LinkButton ID="LinkButton4" Visible="false" OnClick="LnktTrackerExport_Click"  runat="server" Text=" Government Report Data"
                                style="color: gren;color: blue;" ></asp:LinkButton></li>--%>

                        <li>

                            <asp:LinkButton ID="LinkButton3" Visible="true" OnClick="LnktTrackerExpor6t_Click" runat="server" Text="Government Report Data CSV"
                                Style="color: gren; color: blue;"></asp:LinkButton></li>



                    </ul>
                </div>
            </div>
        </div>
        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12" style="padding-left: 10px; margin-top:10px">
            <div class="thumbnail" style="background-color: #f5f5f5; float: left; width: 100%;">
                <div class="panel panel-default" style="margin-bottom:0px">
                    <div class="form-horizontal">
                        <div class="row">
                            <div class="row marg search-bg" style="width: 98%; margin-left: 11px;">
                                <div class="form-horizontal">


                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" style="padding-left: 22px;">
                                        <div class="form-group" style="margin-right: -15px; margin-left: 0px;">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">State:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                    AutoPostBack="true" class="form-control ">
                                                </asp:DropDownList>


                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">District:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlDistrict" runat="server"
                                                    AutoPostBack="true" class="form-control " />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">Month:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlMonth" runat="server"
                                                    class="form-control " />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">Type:</label>
                                            <div class="col-sm-9 padd" style="padding-left: 10px; /* padding-right: 5px;">
                                                <asp:DropDownList ID="ddlType" runat="server" Style="width: 85%;" class="form-control">
                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">Government Target Data </asp:ListItem>
                                                    <asp:ListItem Value="2">Government Data Upload</asp:ListItem>


                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-1 col-sm-1 cpl-xs-12" style="padding: 0px 25px 20px;">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12" style="padding: 0px;">
                                        <asp:Button ID="btnImport" runat="server" Text="Upload" CssClass="btn-success btn-sm" OnClick="btnImport_Click" />

                                    </div>

                                    <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 " style="padding: 0px;">
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn-success btn-sm" Visible="true"
                                            OnClick="btnApprove_Click" />
                                    </div>

                                </div>


                            </div>
                            <div class="col-lg-12 table table-hover " style="padding: 10px;">
                                <asp:Panel ID="pnlMain" Enabled="true" runat="server">
                                    <div class="form-horizontal">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                            <div class="panel-default search-bg" style="height: 30px">
                                                <span style="float: left; color: Black; margin-left: 12px;">
                                                    <asp:Label ID="lblTotal" Text="" runat="server"></asp:Label>
                                                </span><span style="float: left; color: Black; margin-left: 12px;"></span>
                                            </div>
                                            <asp:Label ID="lblTotalCount" Visible="false" ForeColor="#737272" Font-Bold="true" runat="server"></asp:Label>
                                            <div style="height: 290px; overflow: auto; width: 99%;" align="center">
                                                <div>

                                                    <div class="row" style="width: 100%">
                                                        <asp:GridView ID="GV_DynamicGrid" runat="server" ForeColor="Black" AllowPaging="true"
                                                            PageSize="100" ShowHeader="true" Visible="false" CssClass="table table-striped table-bordered table-hover"
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
                                                            <PagerStyle CssClass="pagination-ys" />
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
