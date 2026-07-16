<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmTravelMatrix2024HR.aspx.cs" Inherits="frmTravelMatrix2024HR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
        <script type="text/javascript">
      function CheckAll_Fishery(headerCheckbox) {
            var gridView = document.getElementById('<%= gvTravekDatewise.ClientID %>');
            var checkboxes = gridView.getElementsByTagName("input");


            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].type === "checkbox" && checkboxes[i].id.indexOf("chkSelect") > -1) {
                    checkboxes[i].checked = headerCheckbox.checked;
                }
            }
            }
        </script>
       <style>
        .btnStyle {
            border: 1px solid #ccc;
            margin-bottom: 7px;
            margin-right: 16px;
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
                           <asp:Label ID="lblmsg" runat="server" class="text-danger"  Text="Travel Matrix- HR Payment Confirmation"></asp:Label> </h3>
                             
                        </div>
                    </div>
                </div>
  
        <div class="row">
            <div class="col-sm-12" >
                <div class="panel panel-default" style="border: 0px;">
                    <div class="panel-body search-bg">
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
                                       <asp:DropDownList ID="ddlFC" runat="server"
                                                                    class="form-control " />
                                    </div>
                                </div>
                            </div>
                            
                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">Month:</label>
                                    <div class="col-sm-9 padd">
                                          <asp:DropDownList ID="ddlMonth"  runat="server" class="form-control"
                                                            >
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
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;"></label>
                                    <div class="col-sm-9 padd">
                                     <asp:Button id="Button1" class="btn btn-primary" Text="Search" runat="server" OnClick="btnSearch_Click" ></asp:Button>
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
                                     <asp:Button id="btnAdd"  class="btn btn-success" Text="Submit to DOL" Visible="false" runat="server" OnClick="btnAdd_Click" ></asp:Button>
                             <asp:Button id="btnDownload" class="btn btn-info "  Text="Download Summary" Visible="false" runat="server" OnClick="btnDown_Click" ></asp:Button>
                           
                                                       </div>
                            </div>

                            <div class="panel-body">
                                <div class="row">
                               
                                    <div class="col-lg-12 col-md-12 col-sm-12 cpl-xs-12">
                                        <div class="panel panel-default">
                                           
                                            <div class="Row WrapText-tble   table-responsive" style="height: 310px; overflow: auto; width: 100%;" align="center">
                                                 <asp:GridView ID="gvTravekDatewise" ShowFooter="True"  runat="server"  CssClass="table table-striped table-bordered table-hover"   OnRowDataBound="gvnroll_OnRowCommand"   AutoGenerateColumns="False"  Font-Names="Arial"
                                                                        Font-Size="12px" Width="100%" >
                                                                          <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
                                                                        </EmptyDataTemplate>
                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <Columns>
                                                                          


                                                                               <asp:TemplateField HeaderText="All" Visible="false">
                                                <HeaderStyle />
                                                <HeaderTemplate>
                                                    <span>All</span><asp:CheckBox ID="chkSelectAll" runat="server" onclick="CheckAll_Fishery(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                               
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="Action" Visible="true">
                                                                                <ItemTemplate>
                                                                                    
                                                                          <asp:LinkButton ID="LinkButton1" OnClick="lnl_Action" runat="server" Text="Hold"    >
                                                                             
                                                                          </asp:LinkButton>
                                                                     
                                                                                </ItemTemplate>
                                                                                       <ItemStyle CssClass="padding-lef"  Width="4%" />
                                                                            </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Form No"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFromNo"  Text='<%# Eval("FromNo") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate> 
                                                                                     <ItemStyle CssClass="padding-lef"  Width="6%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Emp Code"  Visible="true">
                                                                                <ItemTemplate>
                                                                                     <asp:Label ID="lblUserID" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("UserID") %>'></asp:Label>

                                                                                    <asp:Label ID="lblMyear" Visible="false" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("Myear") %>'></asp:Label>
                                                                                    
                                                                                    <asp:Label ID="lblStatus" Visible="false" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("Status") %>'></asp:Label>
                                                                               
                                                                               
                                                                                   
                                                                                </ItemTemplate>
                                                                                       <ItemStyle CssClass="padding-lef"  Width="6%" />
                                                                            </asp:TemplateField>
                                                                          
                                                                            <asp:TemplateField HeaderText="Employee Name"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLoginTime"  Text='<%# Eval("UserName") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate> 
                                                                                         <ItemStyle CssClass="padding-lef"  Width="8%" />
                                                                            </asp:TemplateField>
                                                                            
                                                                             <asp:TemplateField HeaderText="District Name"  Visible="true">
                                                                                <ItemTemplate>
                                                                                   <asp:Label ID="lblhTravelDate" ForeColor="Black" runat="server"
                                                                                       Text='<%# Eval("DistrictName") %>'></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                                       <ItemStyle CssClass="padding-lef"  Width="10%" />
                                                                            </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Block Name"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLogoutTime"     Text='<%# Eval("BlockName") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate> 
                                                                                   <ItemStyle CssClass="padding-lef"  Width="8%" />
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Cluster Name"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFromVillage"      Text='<%# Eval("ClusterName") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                                                <ItemStyle CssClass="padding-lef"  Width="8%" />
                                                                               </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Designation"  Visible="true" ItemStyle-Wrap="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblOOSG"   Text='<%# Eval("UserRole") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <FooterTemplate>
                                                                                    <asp:Label ID="lbltkkotal" runat="server" Text="Total"></asp:Label>
                                                                                </FooterTemplate>
                                                                                                <ItemStyle CssClass="padding-lef"  Width="7%" />
                                                                            </asp:TemplateField>
                                                                           
                                                                            <asp:TemplateField HeaderText="Travel KM"  >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblObjective"   Text='<%# Eval("ClusterKM") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                                            <ItemStyle CssClass="padding-lef"  Width="5%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Travel Cost-Within cluster"  >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblClusterTotalAmountKM"  Text='<%# Eval("ClusterTotalAmountKM") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                              <FooterTemplate>
                                                                                    <asp:Label ID="lbltotalClusterTotalAmountKM" runat="server" Text="Label"></asp:Label>
                                                                                </FooterTemplate>
                                                                                               <ItemStyle CssClass="padding-lef"  Width="5%" />
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Travel Cost- Out of cluster"  >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblClusteroutTotalAmountKM"  Text='<%# Eval("ClusteroutTotalAmountKM") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lbltotalClusteroutTotalAmountKM" runat="server" Text="Label"></asp:Label>
                                                                                </FooterTemplate>
                                                                                                <ItemStyle CssClass="padding-lef"  Width="5%" />
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Per Diem"  >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPerDim"   Text='<%# Eval("PerDim") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                    <asp:Label ID="lbltotalPerDim" runat="server" Text="Label"></asp:Label>
                                                                                </FooterTemplate>
                                                                                                <ItemStyle CssClass="padding-lef"  Width="5%" />
                                                                            </asp:TemplateField>

                                                                             <asp:TemplateField HeaderText="Accommodation"  >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAccommodation"      Text='<%# Eval("GuestHouseRent") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                    <asp:Label ID="lbltotalAccommodation" runat="server" Text="Label"></asp:Label>
                                                                                </FooterTemplate>
                                                                                                <ItemStyle CssClass="padding-lef"  Width="5%" />
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="Local Conveyance"  >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblvehicle"         Text='<%# Eval("Totalvehicle") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             <asp:Label ID="lblHoldStatus"   Visible="false"       Text='<%# Eval("HoldStatus") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lbltotalvehicle" runat="server" Text="Label"></asp:Label>
                                                                                </FooterTemplate>
                                                                                                 <ItemStyle CssClass="padding-lef"  Width="5%" />
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="Other Expanses"  >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TotalExpensBO"         Text='<%# Eval("TotalExpensBO") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                                      <FooterTemplate>
                                                                                    <asp:Label ID="lbltotalExpens" runat="server" Text="Label"></asp:Label>
                                                                                </FooterTemplate>
                                                                                                 <ItemStyle CssClass="padding-lef"  Width="5%" />
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Total Travel Payable"  >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTotalPay"         Text='<%# Eval("Total") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                              <FooterTemplate>
                                                                                    <asp:Label ID="lbltotal" runat="server" Text="Label"></asp:Label>
                                                                                </FooterTemplate>
                                                                                     <ItemStyle CssClass="padding-lef"  Width="5%" />
                                                                            </asp:TemplateField>
                                                                           
                                                                            <asp:TemplateField HeaderText="Action" Visible="true">
                                                                                <ItemTemplate>
                                                                                    
                                                                                     <asp:ImageButton ID="LinkButtdgg1" Width="20px" Height="16px" ImageUrl="~/images/View.jpg" OnClick="View_Action" runat="server"></asp:ImageButton>

                                                                  
                                                                                </ItemTemplate>
                                                                              
                                                                            </asp:TemplateField>
                                                                           
                                                                        </Columns>
                                                                        
                                                                    </asp:GridView>
                                      
                                       <asp:Label ID="lblFromNoEdit" Visible="false" ForeColor="Black" runat="server"     ></asp:Label>
                                       <asp:Label ID="lblUserIDEdit" Visible="false" ForeColor="Black" runat="server"     ></asp:Label>
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
                        <h4 class="text-danger">Reason  
                            <asp:LinkButton ID="lnkEntryClose" class="btn btn-xs btn-danger" runat="server">
                                <span class="glyphicon glyphicon-remove"></span>
                            </asp:LinkButton>
                        </h4>
                    </div>
                    <div class="modal-body">

                        <div class="form-group">
                            <label class="control-label">Reason: <span style="color: Red">*</span></label>
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
       <asp:PostBackTrigger ControlID="gvTravekDatewise" />
               <asp:PostBackTrigger ControlID="btnDownload" />
        </Triggers>
       
    </asp:UpdatePanel>
</asp:Content>

