<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmTravelMatrix2024.aspx.cs" Inherits="frmTravelMatrix2024" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .btnStyle {
            border: 1px solid #ccc;
            margin-bottom: 7px;
            margin-right: 16px;
        }
      .GridHeader
{
    text-align:center !important;    
}
        .float-r {
            float: right;
        }

        .WrapText {
            width: 100%;
            word-break: break-all;
        }
        /* .modalBg {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }*/
        .modal {
            position: fixed;
            top: 80px;
            right: 0;
            bottom: 0;
            left: 0;
            z-index: 9999;
            width: 62%;
            display: none;
            overflow: hidden;
            -webkit-overflow-scrolling: touch;
            outline: 0;
        }
        /* .modalBackground {
            background-color: rgba(0,0,0,0.5);
        }

        .mod-posi {
            position: fixed !important;
            top: 5% !important;
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
            height: 490px !important;
            z-index: 1350px0001 !important;
        }

        .modal-body {
            background-color: #fff;
            position: relative;
            padding: 15px;
        }*/

        .primaryKK {
            margin-right: 2px;
        }
        /*
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
        }*/

        /*.Mpopupfooter {
            width: 100%;
            background-color: #454545;
            padding: 3px
        }*/

        .Requiredvalidate {
            font-size: 12px;
            color: Red;
        }


        /*.ModalPopupBG {
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
        }*/

        .Training-details-row {
            margin-left: -15px;
            margin-right: -15px;
            margin-top: 10px;
            margin-bottom: 10px;
        }


            .Training-details-row label {
                line-height: initial;
            }

            /*.modal-header {
            padding: 15px;
            border-bottom: 1px solid #0000000d;
        }*/

            /*.modal-body * {
            font-size: 16px;
        }*/

            .Training-details-row .form-group {
                margin-bottom: 12px;
            }

        /*  .Mpopup1 {
            top: 50% !important;
            transform: translateY(-50%) !important;
        }*/

        .part-1 {
            float: left;
            width: calc(50% - 25px);
            min-height: 150px;
            border: 1px solid #ddd;
            border-radius: 6px;
            box-shadow: 0px 0px 4px 0px #545454;
        }

        .part-butt {
            float: left;
            width: 50px;
            min-height: 150px;
            text-align: center;
            position: relative;
            top: 14rem;
        }
    </style>
    <style>
        .page-break {
            page-break-after: always;
        }

        .search-bg {
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



        /* width */
        .da-tble::-webkit-scrollbar, .WrapText-tble::-webkit-scrollbar {
            width: 7px;
        }

        /* Track */
        .da-tble::-webkit-scrollbar-track, .WrapText-tble::-webkit-scrollbar-track {
            background: #f1f1f1;
        }

        /* Handle */
        .da-tble::-webkit-scrollbar-thumb, .WrapText-tble::-webkit-scrollbar-thumb {
            background: #d9d9d9;
        }

            /* Handle on hover */
            .da-tble::-webkit-scrollbar-thumb:hover, .WrapText-tble::-webkit-scrollbar-thumb:hove {
                background: #555;
            }


        @media (min-width: 1200px) {
            .tada-left {
                width: 28%;
            }

            .tada-right {
                width: 72%;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="page_heading_dg" style="text-align:center">
                               <asp:Label ID="lblmsg" runat="server" class="text-danger"  Text="Travel Matrix"></asp:Label> </h3>
                       
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel panel-default  search-bg">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">Year:</label>
                                            <div class="col-sm-9 padd">

                                                <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                    class="form-control ">
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">State:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                    AutoPostBack="true" class="form-control ">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">District:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                    AutoPostBack="true" class="form-control " />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">Block:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                    class="form-control " />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">Cluster:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlCluster" AutoPostBack="true" OnSelectedIndexChanged="ddlCluster_SelectedIndexChanged" runat="server"
                                                    class="form-control " />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">FC:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlFC" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFc_SelectedIndexChanged"
                                                    class="form-control " />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">Month:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlMonth" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" runat="server" class="form-control">
                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">Jan </asp:ListItem>
                                                    <asp:ListItem Value="2">Feb </asp:ListItem>
                                                    <asp:ListItem Value="3">Mar</asp:ListItem>
                                                    <asp:ListItem Value="4">Apr</asp:ListItem>
                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                    <asp:ListItem Value="6">Jun</asp:ListItem>
                                                    <asp:ListItem Value="7">Jul</asp:ListItem>
                                                    <asp:ListItem Value="8">Aug</asp:ListItem>
                                                    <asp:ListItem Value="9">Sep</asp:ListItem>
                                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group">
                                            <label for="email" class="col-sm-3 padd linhei"></label>
                                            <div class="col-sm-9 padd">
                                                <asp:Button ID="Button1" class="btn btn-primary" Text="Search" runat="server" OnClick="btnSearch_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">





                        <div class="panel panel-default">
                            <div class="panel-heading  search-bg" style="padding: 5px 15px; border: 0; border-bottom: 1px solid #ddd;">
                                <div style="text-align: right;">
                                    <asp:Button ID="btnView" class="btn btn-info " Visible="false" Text="View TA/DA Form" OnClick="btnViwe_Click" runat="server"></asp:Button>
                                    <asp:Button ID="btnAdd" class="btn btn-primary" Text="Add New Visit" Visible="false" runat="server" OnClick="btnAdd_Click"></asp:Button>
                                    <asp:Button ID="btnApprove" class="btn btn-success" Text="Approve" OnClick="btnApprove_Click" Visible="false" runat="server"></asp:Button>
                                </div>
                            </div>

                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12  tada-left">
                                        <div class="panel panel-default">
                                            <h4 class="text-center search-bg m-0" style="padding: 10px 15px; border: 0; border-bottom: 1px solid #ddd; margin: 0; font-size: 18px; font-weight: 700;">TA/DA form Detail</h4>

                                            <div class="Row" style="width: 100%">
                                                <div class="Row da-tble table-responsive" style="height: 310px; overflow: auto; width: 100%;" align="center">
                                                    <asp:GridView ID="gvMain" runat="server" DataKeyNames="Fdate,FormSerialNo,Tdate,FromNo,Status,UserName,Clustercode" OnRowDataBound="gvnroll_OnRowCommand" OnRowCommand="GVMain_OnRowCommand" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" Font-Names="Arial"
                                                        Font-Size="12px" Width="100%">
                                                        <EmptyDataTemplate>
                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                Data not found
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <HeaderStyle BackColor="#838383" ForeColor="White" Height="40px"  HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                        <Columns>   <asp:ButtonField HeaderText="Emp ID" ItemStyle-ForeColor="#333" DataTextField="UserName"
                                                                CommandName="GVUIO">

                                                                <HeaderStyle CssClass="padding-lef" />
                                                            </asp:ButtonField>
                                                            <asp:ButtonField HeaderText="Form No" ItemStyle-ForeColor="#333" DataTextField="FromNo"
                                                                CommandName="GVUIO">

                                                                <HeaderStyle CssClass="padding-lef" />
                                                            </asp:ButtonField>
                                                           
                                                            <asp:TemplateField HeaderText="Form No"  Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFromNo" ForeColor="Black" runat="server"
                                                                        Text='<%# Eval("FromNo") %>'></asp:Label>
                                                                    <asp:Label ID="lblStatus" ForeColor="Black" Visible="false" runat="server"
                                                                        Text='<%# Eval("Status") %>'></asp:Label>
                                                                    <asp:Label ID="lblFdate" ForeColor="Black" Visible="false" runat="server"
                                                                        Text='<%# Eval("Fdate") %>'></asp:Label>

                                                                    <asp:Label ID="lblTdate" ForeColor="Black" Visible="false" runat="server"
                                                                        Text='<%# Eval("Tdate") %>'></asp:Label>

                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="padding-lef" />

                                                            </asp:TemplateField>
                                                            <asp:ButtonField HeaderText="Date" ItemStyle-ForeColor="#333" DataTextField="WeekDate"
                                                                CommandName="GVUIO">

                                                                <HeaderStyle CssClass="padding-lef" />
                                                            </asp:ButtonField>
                                                            <asp:TemplateField HeaderText="Date" Visible="false">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblWeekDate" ForeColor="Black" runat="server"
                                                                        Text='<%# Eval("WeekDate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="padding-lef" />

                                                            </asp:TemplateField>
                                                            <asp:ButtonField HeaderText="Amount" ItemStyle-ForeColor="#333" DataTextField="TotalAmout"
                                                                CommandName="GVUIO">

                                                                <HeaderStyle CssClass="padding-lef" />
                                                            </asp:ButtonField>
                                                            <%-- <asp:ButtonField HeaderText="Status" ItemStyle-ForeColor="#333" DataTextField="Status"
                                                                                    CommandName="GVUIO">
                                                            --%>
                                                            <%--                                                                                    <HeaderStyle CssClass="padding-lef" />
                                                                                </asp:ButtonField>--%>
                                                            <asp:TemplateField HeaderText="Amount" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotalAmout" ForeColor="Black" runat="server"
                                                                        Text='<%# Eval("TotalAmout") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="padding-lef" />

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>


                                                                    <asp:Label ID="lblStatus3" ForeColor="Black" runat="server"></asp:Label>

                                                                    <asp:Label ID="lblStatus5" Visible="false" ForeColor="Black" runat="server"
                                                                        Text='<%# Eval("Status") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="padding-lef" />

                                                            </asp:TemplateField>

                                                        </Columns>

                                                    </asp:GridView>


                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-9 cpl-xs-12 tada-right">
                                        <div class="panel panel-default">
                                            <h4 class="text-center search-bg m-0" style="padding: 10px 15px; border: 0; border-bottom: 1px solid #ddd; margin: 0; font-size: 18px; font-weight: 700;">
                                                <asp:Label ID="lblTDDA" runat="server" Text="TA/DA Form No:"></asp:Label>
                                            </h4>
                                            <div class="Row WrapText-tble   table-responsive" style="height: 310px; overflow: auto; width: 100%;" align="center">
                                                <asp:GridView ID="gvTravekDatewise" OnRowDataBound="gvnroll1_OnRowCommand" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" Font-Names="Arial"
                                                    Font-Size="12px" Width="100%" >
                                                    <EmptyDataTemplate>
                                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                            Data not found
                                                        </div>
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle BackColor="#838383" ForeColor="White" Height="40px"  HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Date From" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTravelDate" ForeColor="Black" runat="server"
                                                                    Text='<%# Eval("TravelDate") %>'></asp:Label>

                                                                <asp:Label ID="lblPlanUniqueCode" Visible="false" ForeColor="Black" runat="server"
                                                                    Text='<%# Eval("UniqueCode") %>'></asp:Label>
                                                                <asp:Label ID="lblStatusMain" Visible="false" ForeColor="Black" runat="server"
                                                                    Text='<%# Eval("Status") %>'></asp:Label>
                                                                <asp:Label ID="lblKdata" Visible="false" ForeColor="Black" runat="server"
                                                                    Text='<%# Eval("tdate") %>'></asp:Label>

                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="padding-lef" Width="9%" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Time In" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoginTime" Text='<%# Eval("LoginTime") %>' ForeColor="Black" runat="server"></asp:Label>


                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="padding-lef" Width="7%" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Date To" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblhTravelDate" ForeColor="Black" runat="server"
                                                                    Text='<%# Eval("TravelDate") %>'></asp:Label>


                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="padding-lef" Width="8%" />

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Time Out" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLogoutTime" Text='<%# Eval("LogoutTime") %>' ForeColor="Black" runat="server"></asp:Label>


                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="padding-lef" Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Travelling from" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFromVillage" Text='<%# Eval("FromVillage") %>' ForeColor="Black" runat="server"></asp:Label>


                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="padding-lef" Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Travelling To" Visible="true" ItemStyle-Wrap="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOOSG" Text='<%# Eval("ToVillage") %>' ForeColor="Black" runat="server"></asp:Label>


                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="padding-lef" Width="10%" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Purpose of Visit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblObjective" Text='<%# Eval("Objective") %>' ForeColor="Black" runat="server"></asp:Label>


                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="padding-lef" Width="12%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Visit Type" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVisitTypee" Text='<%# Eval("Visite") %>' ForeColor="Black" runat="server"></asp:Label>



                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="padding-lef" Width="12%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVishhee" Text='<%# Eval("computedFare") %>' ForeColor="Black" runat="server"></asp:Label>


                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="padding-lef" Width="9%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" Visible="true">
                                                            <ItemTemplate>

                                                                <asp:ImageButton ID="LinkButton1" Width="20px" Height="16px" OnClick="LnkBtnBlock_OnClick" runat="server"></asp:ImageButton>

                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="padding-lef" Width="7%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                                            <ItemTemplate>

                                                                <asp:ImageButton ID="LinkBut51" OnClick="LnkBtnDelete_OnClick" Width="20px" Height="20px" ImageUrl="~/images/delete-29.png" runat="server"></asp:ImageButton>

                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="padding-lef" Width="7%" />
                                                        </asp:TemplateField>

                                                    </Columns>

                                                </asp:GridView>

                                                <asp:Label ID="lblEditUniquePlanCode" Visible="false" ForeColor="Black" runat="server"></asp:Label>
                                                <asp:Label ID="lblEditUserName" Visible="false" ForeColor="Black" runat="server"></asp:Label>
                                                <asp:Label ID="lbltdate" Visible="false" ForeColor="Black" runat="server"></asp:Label>
                                                <asp:Label ID="lblVisitTypeeID" Visible="false" ForeColor="Black" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <asp:ModalPopupExtender ID="MPE_Entry" BackgroundCssClass="modalBg"
                runat="server" PopupControlID="Pnl_Entry" TargetControlID="HdnEntry" CancelControlID="lnkEntryClose">
            </asp:ModalPopupExtender>
            <asp:HiddenField ID="HdnEntry" runat="server" />

            <asp:Panel ID="Pnl_Entry" runat="server" CssClass="modal-dialog delete_pop modal-lg" Style="display: none;">
                <div class="modal-pop">
                    <div class="modal-header">
                        <h4 class="text-danger">Delete Reason  
                            <asp:LinkButton ID="lnkEntryClose" class="btn btn-xs btn-danger" runat="server">
                                <span class="glyphicon glyphicon-remove"></span>
                            </asp:LinkButton>
                        </h4>
                    </div>
                    <div class="modal-body">

                        <div class="form-group">
                            <label class="control-label">Delete Reason: <span style="color: Red">*</span></label>
                            <div class="">
                                <asp:TextBox ID="txtResone" runat="server" TextMode="MultiLine" TabIndex="4" MaxLength="150" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtResone" Display="Dynamic" ErrorMessage="Please enter Reason" ForeColor="Red" SetFocusOnError="True" ValidationGroup="QuestionCreate1">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <hr />
                        <asp:LinkButton ID="BtnEntry" OnClick="BtnDelete_Click" ValidationGroup="QuestionCreate1" class="btn btn-success pull-right" ToolTip="Save" Width="55px" runat="server">Save</asp:LinkButton>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnView" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

