<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" Culture="en-GB" AutoEventWireup="true" CodeFile="frmweaklyPlan2026.aspx.cs" Inherits="frmweaklyPlan2026" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.2.4/jquery.min.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>


     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>


      <script src="js/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
    <link rel="stylesheet" type="text/css" href="css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="css/eg_styles.css">

    <script src="online-js/jquery-ui.min.js" type="text/javascript"></script>
    <link href="online-js/jquery-ui.css" rel="stylesheet" type="text/css" />
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
<%--    <style>
        
    
         .search-bg {
            background: linear-gradient(to bottom, #ebf1fd 0%,#ffffff 100%) !important;
            /*background-color: rgb(241, 241, 241)!important;*/
            padding-top: 7px !important;
            border: 1px solid rgb(221, 221, 221) !important;
            border-top-left-radius: 4px !important;
            border-top-right-radius: 4px !important;
            margin-bottom: 0px !important;
        }
        .WrapText {  
            width: 100%;  
            word-break: break-all;  
        }  
   
        .container-fluid {
            padding-right: 15px;
            padding-left: 15px;
            margin-right: auto;
            margin-left: auto;
        }

        .row {
            margin-right: -15px;
            margin-left: -15px;
        }

        .panel-heading {
            padding: 10px 15px;
        }

        .edit-bg {
            width: 20px;
            height: 20px;
            background-color: #898989;
            border-radius: 50px;
            float: left;
            display: flex;
            justify-content: center;
            align-items: center;
            color: #fff;
        }

            .edit-bg:hover {
                color: #eee;
                text-decoration: none;
                background-color: #626161;
            }

        .modal {
            position: fixed;
            top: 80px;
            right: 0;
            bottom: 0;
            left: 0;
            z-index: 9999;
            width:62%;
            display: none;
            overflow: hidden;
            -webkit-overflow-scrolling: touch;
            outline: 0;
        }

        .form-group {
            margin-bottom: 15px;
            float: left;
            width: 100%;
        }

         .ajax__calendar_container
        {
            z-index: 1045;
        }
    </style>--%>
       <script language="Javascript" type="text/javascript">

           function onlyAlphabetsAdd(e, t) {
               try {
                   if (window.event) {
                       var charCode = window.event.keyCode;
                   }
                   else if (e) {
                       var charCode = e.which;
                   }
                   else { return true; }
                   if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 32 || charCode == 0 || charCode == 9 || charCode == 08 || charCode == 44 || charCode == 45 || charCode == 48)
                       return true;
                   else
                       return false;
               }
               catch (err) {
                   alert(err.Description);
               }
           }

       </script>
     <script type="text/javascript">
         function validateFristNumeric(txt) {
             debugger;
             var firstChar = txt.value.charAt(0);
             if (firstChar == 0) {
                 //do your stuff

                 txt.value = "";

             }
             else {
                 return true;
             }


         }
     </script>
    <script type="text/javascript">


        function isNumberKey(txt, evt) {
            debugger;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
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

    <script type="text/javascript">
        function SelectAll(headerCheckBox) {
            //Get the reference of GridView.
            var GridView = headerCheckBox.parentNode.parentNode.parentNode;

            //Loop through all GridView Rows except first row.
            for (var i = 1; i < GridView.rows.length; i++) {
                //Reference the CheckBox.
                var checkBox = GridView.rows[i].cells[0].getElementsByTagName("input")[0];
                checkBox.checked = headerCheckBox.checked;
            }
        }
        function checkdataVic(Flag, clsname) {


            var Lngg = "", lid = "";
            var maxSelection = 0;
            $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                Lngg = Lngg + $(this).next().html() + ",";
                lid = lid + $(this).val() + ",";
                maxSelection++;
            });

            Lngg = Lngg.substr(0, Lngg.length - 1);
            lid = lid.substr(0, lid.length - 1);

            if (Flag == 'F') {
                if (maxSelection <= 10) {
                    $('#<%=hdn_PBID.ClientID %>').val(lid);
                    $('#<%=hdn_PBName.ClientID %>').val(Lngg);
                    $('#<%=txt_pbname.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_PBID.ClientID %>').val('');
                    $('#<%=hdn_PBName.ClientID %>').val('');
                    $('#<%=txt_pbname.ClientID %>').val('');
                    $find("Modal_alertB").show();
                    return false;
                }

            }
        }
    </script>

    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


     <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
<%-- <script language="Javascript" type="text/javascript">


     function SetMultilanguage3(Flag, clsname) {
         debugger;
         var Lngg = "", lid = "";
         var maxSelection = 0;
         $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
             Lngg = Lngg + $(this).next().html() + ",";
             lid = lid + $(this).val() + ",";
             maxSelection++;
         });

         Lngg = Lngg.substr(0, Lngg.length - 1);
         lid = lid.substr(0, lid.length - 1);
         if (Flag == 'F') {
             if (maxSelection <= 10) {
                 $('#<%=hdn_PBID.ClientID %>').val(lid);
                         $('#<%=hdn_PBName.ClientID %>').val(Lngg);
                         $('#<%=txt_pbname.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_PBID.ClientID %>').val('');
                    $('#<%=hdn_PBName.ClientID %>').val('');
                    $('#<%=txt_pbname.ClientID %>').val('');
                 $find("Modal_alertB").show();
                 return false;
             }


         }





 </script>--%>
    <div class="container-fluid">
            <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="page_heading_dg" style="text-align:center">
                               <asp:Label ID="lblmsg" runat="server" class="text-danger"  Text="FC Weekly Plan Approval"></asp:Label> </h3>
                       
                        </div>
                    </div>
                </div>
        <div class="row">
                
             <div class="col-sm-12">
                        <div id="search_box">
                            <div class="panel panel-default" style="margin-bottom: 8px;">
                                <div class="panel-body" style="padding-top: 0px; padding-bottom: 0px;">
                                    <div class="row" style="margin: 0px -15px;">


                                        <div class="col-lg-12  search-bg">
                                            <div id="container-target">

                                                <div class="form-horizontal">
                                             <div class="row marg" style="margin-left: -15px; margin-right: -15px">

                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                        District:</label>
                                    <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlDistrict" runat="server"
                                            OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                            AutoPostBack="true" class="form-control " />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                        Block:</label>
                                    <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlBlock"  OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" runat="server" AutoPostBack="true"
                                            class="form-control " />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                        FC:</label>
                                    <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlUser" OnSelectedIndexChanged="ddlFC_SelectedIndexChanged" runat="server" AutoPostBack="true"  class="form-control ">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 linhei" style="padding-right: 0px; padding-left: 7px;">
                                        Year:
                                    </label>
                                    <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlYear" Enabled="false" AutoPostBack="true" runat="server"
                                            class="form-control ">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                        </div>

                                             <div class="row marg" style="margin-left: -15px; margin-right: -15px">
                            <div id="Div1" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                        Month:</label>
                                    <div class="col-sm-9 padd">
                                      <asp:DropDownList ID="ddlMonth" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"  AutoPostBack="true" runat="server" class="form-control"
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
                            <div id="Div2" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                        Week:</label>
                                    <div class="col-sm-9 padd">
                                              <asp:DropDownList ID="ddlWeeklly"   OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged" runat="server" AutoPostBack="true" class="form-control"  >
                                                            
                                                          
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                   <asp:LinkButton ID="Button1" OnClick="btnApprove_Click" class="btn btn-sm btn-primary primaryKK"  Visible="false"  runat="server">Approve</asp:LinkButton>
                                                          
                                   <%--<asp:Button  type="button" ID="Button1" runat="server" OnClick="btnApprove_Click" Text="Approve" Visible="false" class="btn btn-success"></asp:Button>--%>
                           
                            </div>

                        </div>
                  
                  
                                                </div>





                                            </div>



                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
              </div>

                          <div class="row" >
                    <div class="col-sm-12">





                        <div class="panel panel-default">
                            <div class="panel-heading  search-bg" style="padding: 5px 15px; border: 0; border-bottom: 1px solid #ddd;">
                                <div style="text-align: right;">
								<asp:LinkButton ID="btnAdd" OnClick="btnAdd_Click" class="btn btn-sm btn-primary primaryKK"  Visible="false"  runat="server">Add</asp:LinkButton>
                
                                </div>
                            </div>

                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12  tada-left">
                                        <div class="panel panel-default"  style="margin-top: -16px !important;">
                                            <h4 class="text-center search-bg m-0" style="padding: 10px 15px; border: 0; border-bottom: 1px solid #ddd; margin: 0; font-size: 18px; font-weight: 700;">User Information</h4>

                                            <div class="Row" style="width: 100%">
                                                <div class="Row da-tble table-responsive" style="height: 280px; overflow: auto; width: 100%;" align="center">
                                                         <asp:GridView ID="gvWeeklly" runat="server"  CssClass="table table-striped table-bordered table-hover"  OnRowDataBound="gvnroll_OnRowCommand"   AutoGenerateColumns="False"  Font-Names="Arial"
                                                                        Font-Size="12px" Width="100%" >
                                                                          <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
                                                                        </EmptyDataTemplate>
                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <Columns>
                                                                          
                                                                            <asp:TemplateField HeaderText="FC Name"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblUserName" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("UserName") %>'></asp:Label>
                                                                                      <asp:Label ID="lblUserID" ForeColor="Black" Visible="false" runat="server"
                                                                                        Text='<%# Eval("UserID") %>'></asp:Label>
                                                                                   
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                          
                                                                            <asp:TemplateField HeaderText="Status"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStatus"  ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                                    <asp:Label ID="lblStatus1" Visible="false" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("Status") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Action" Visible="true">
                                                                                <ItemTemplate>
                                                                                      <asp:LinkButton ID="lbtn" onClick="btn_Un_Click"  runat="server" Text="View"   ></asp:LinkButton>
                                                                         
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
                                        <div class="panel panel-default"  style="margin-top: -16px !important;">
                                            <h4 class="text-center search-bg m-0" style="padding: 10px 15px; border: 0; border-bottom: 1px solid #ddd; margin: 0; font-size: 18px; font-weight: 700;">
                                                <asp:Label ID="lblTDDA" runat="server" Text="Weekly Plan:"></asp:Label>
                                            </h4>
                                            <div class="Row WrapText-tble   table-responsive" style="height: 280px; overflow: auto; width: 100%;" align="center">
                                               <asp:GridView ID="gvWeallyDatewise" runat="server"  CssClass="table table-striped table-bordered table-hover"  OnRowDataBound="gvnroll1_OnRowCommand"    AutoGenerateColumns="False"  Font-Names="Arial"
                                                                        Font-Size="12px" Width="100%" >
                                                                          <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
                                                                        </EmptyDataTemplate>
                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <Columns>
                                                                             
                                                                            <asp:TemplateField HeaderText="Date"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPlanDate" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("PlanDate") %>'></asp:Label>
                                                                                     <asp:Label ID="lblUniquePlanCode" Visible="false" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("UniquePlanCode") %>'></asp:Label>
                                                                                     <asp:Label ID="lblStatus" ForeColor="Black" Visible="false" runat="server"
                                                                                        Text='<%# Eval("Status") %>'></asp:Label>
                                                                                    <asp:Label ID="Lvillagecode" ForeColor="Black" Visible="false" runat="server"
                                                                                        Text='<%# Eval("villagecode") %>'></asp:Label>
                                                                                      <asp:Label ID="lblGKPLevel" ForeColor="Black" Visible="false" runat="server"
                                                                                        Text='<%# Eval("GKPLevel") %>'></asp:Label>
                                                                                      <asp:Label ID="lblBAlVal" ForeColor="Black" Visible="false" runat="server"
                                                                                        Text='<%# Eval("BAlVal") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef"  Width="10%"/>

                                                                            </asp:TemplateField>
                                                                          
                                                                            <asp:TemplateField HeaderText="Village Name"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVillagename"  Text='<%# Eval("Villagename") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef"  Width="14%" />

                                                                            </asp:TemplateField>
                                                                             <%--<asp:TemplateField HeaderText="Day"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDayes"   Text='<%# Eval("Dayes") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef"   Width="9%"/>

                                                                            </asp:TemplateField>--%>
                                                                             <asp:TemplateField HeaderText="TB/BO"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTBName"  Text='<%# Eval("TBBO") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef"  Width="12%" />

                                                                            </asp:TemplateField>
                                                                           <%-- <asp:TemplateField HeaderText="BO"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTBN6ame"  Text='<%# Eval("FristName") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef"  Width="10%"/>

                                                                            </asp:TemplateField>--%>
                                                                             <asp:TemplateField HeaderText="Activity"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDescription"  Text='<%# Eval("Activity") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef" Width="15%"/>

                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="OOSG"  Visible="true" ItemStyle-Wrap="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblOOSG"  Text='<%# Eval("OOSG") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef"  Wrap="true" Width="17%"/>

                                                                            </asp:TemplateField>
                                                                           
                                                                            <asp:TemplateField HeaderText="Remarks"  >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRemarks"  Text='<%# Eval("Remarks") %>' ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef" Wrap="true" Width="15%" />

                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Action" Visible="true">
                                                                                <ItemTemplate>
                                                                                    
                                                                          <asp:ImageButton ID="LinkButton1"  ImageUrl="~/images/edit.png" OnClick="LnkBtnBlock_OnClick" runat="server"    >
                                                                             
                                                                          </asp:ImageButton>
                                                                     
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" Width="8%" />

                                                                            </asp:TemplateField>
                                                                        
                                                                           <asp:TemplateField HeaderText="Delete" Visible="true">
                                                                                <ItemTemplate>
                                                                                    
                                                                          <asp:ImageButton ID="LinkBut51" Width="20px" Height="20px" OnClick="Lnkdelete_OnClick" ImageUrl="~/images/delete-29.png"  runat="server"    >
                                                                             
                                                                          </asp:ImageButton>
                                                                     
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" Width="8%" />

                                                                            </asp:TemplateField>
                                                                           
                                                                        </Columns>
                                                                        
                                                                    </asp:GridView>
                                      
                                       <asp:Label ID="lblEditUniquePlanCode" Visible="false" ForeColor="Black" runat="server"     ></asp:Label>
                                       <asp:Label ID="lblEditUserName" Visible="false" ForeColor="Black" runat="server"     ></asp:Label>
                                       <asp:Label ID="lblRound" Visible="false" ForeColor="Black" runat="server"     ></asp:Label>
                                            </div>
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
                                    <asp:Panel  CssClass="modal-dialog modal-lg" Style="display: none; height: auto; width: 50% !important;border-style: groove;"
                                         ID="PnlDistrict" runat="server">
                                        <div style="width: 100%; height: auto; background-color: #f1f1f1">
                                            <div class="modal-header" >
                                                
                                             <h4 class="modal-title">Weekly Plan</h4>
                                                           </div>
                                            <div class="modal-body">
                                                <div class="row">

                                       <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" runat="server" visible="false" id="divViallage">
                                        <div class="form-group">
                                            <label class="">Village<span style="color: Red">*</span></label>
                                                <asp:DropDownList ID="ddlVillage"  runat="server" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged" AutoPostBack="true"  CssClass ="form-control" />
                
                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="">Date <span style="color: Red">*</span></label>
                                              <asp:TextBox ID="txtPlanDate"  runat="server" class="form-control"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txttdcal" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPlanDate">
                                                        </cc1:CalendarExtender>
                                                <%--<asp:TextBox runat="server" ID="txtFromDate"    autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>--%>

                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="">Activity Planned <span style="color: Red">*</span></label>
                                             <asp:DropDownList ID="ddlActivity" OnSelectedIndexChanged="ddlActivity_SelectedIndexChanged"  AutoPostBack="true" runat="server" class="form-control" />
                                        </div>
                                    </div>

                                   <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"  runat="server" style="display:none;" id="divaa">
                                        <div class="form-group">
                                            <label class="">Activity Planned <span style="color: Red">*</span></label>
                                             <asp:DropDownList ID="ddlActivity1" OnSelectedIndexChanged="ddlActivity1_SelectedIndexChanged"   AutoPostBack="true" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" runat="server" style="display:none;" id="divSup">
                                        <div class="form-group"   >

                                            <label class="col-sm-12" style="padding: 0px">Support required</label>
                                            <div class="col-sm-12" style="padding: 0px">
                                                <label class="checkbox-inline">
                                                    <asp:CheckBox runat="server" ID="chkTB" AutoPostBack="true"  OnCheckedChanged="CBContacts_SelectedIndexChanged" type="checkbox" value="" /><span style="position: relative; top: 2px;">TB</span>
                                                </label>
                                                <label class="checkbox-inline">
                                                     <asp:CheckBox runat="server" ID="chkBO"  AutoPostBack="true"  OnCheckedChanged="CBContacts1_SelectedIndexChanged" /><span style="position: relative; top: 2px;">BO</span></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"  runat="server" style="display:none;" id="divSup1">
                                        <div class="form-group">
                                            <label class="">Team Balika</label>
                                              <asp:DropDownList ID="ddlTB" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"  runat="server" style="display:none;" id="divSup2">
                                        <div class="form-group">
                                            <label class="">BO</label>
                                             <asp:DropDownList ID="ddlBo" runat="server" class="form-control" />
                                        </div>
                                    </div>
                             
                                 

                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" style="display:none;" runat="server" id="divTr">
                                        <div class="form-group">
                                            <label class="">Training OutCome <span style="color: Red">*</span></label>
                                                          <asp:DropDownList ID="ddlOutcomde" OnSelectedIndexChanged="ddlOutcomde_SelectedIndexChanged"  AutoPostBack="true" runat="server" class="form-control" />
                         
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" runat="server"  style="display:none;" id="divTr1">
                                        <div class="form-group">
                                            <label class="">Specific Training :<span style="color: Red">*</span></label>
                                                          <asp:DropDownList ID="ddlSpecific"  runat="server" class="form-control" />
                         
                                        </div>
                                    </div>

                                   <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"  style="display:none;" runat="server"  id="divlev" >
                                        <div class="form-group"  style="margin-bottom: 20px;">
                                            <asp:Label ID="lblType" runat="server" Text=""> </asp:Label><span style="color: Red">*</span>
                                          
                                           <asp:DropDownList ID="ddlLeave"  runat="server" class="form-control" />
                                        </div>
                                    </div>
                                       <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"  style="display:none;" runat="server"  id="divlev1" >
                                        <div class="form-group"  style="margin-bottom: 20px;">
                                            <asp:Label ID="Label1" runat="server" Text=""> </asp:Label>Leave Type<span style="color: Red">*</span>
                                          
                                           <asp:DropDownList ID="ddllevelType"  runat="server" class="form-control" />
                                        </div>
                                    </div>

                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" runat="server"  style="display:none;" id="divH0" >
                                        <div class="form-group">
                                            <label class="">Holiday:<span style="color: Red">*</span></label>
                                        <asp:DropDownList ID="ddlHoldday"  runat="server" class="form-control" />
                                        </div>
                                    </div>
                           

                               
                                                 <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" runat="server"  style="display:none;" id="divTravel">
                                        <div class="form-group">
                                            <label class="">Travel:<span style="color: Red">*</span></label>
                                           <asp:TextBox  ID="txtTravel" runat="server"  onkeypress="return onlyAlphabets(event,this);"  MaxLength="50"  class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                                      <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" runat="server"  style="display:none;" id="divm">
                                        <div class="form-group">
                                            <label class="">Meeting:<span style="color: Red">*</span></label>
                                           <asp:TextBox  ID="txtMeeting" runat="server"   onkeypress="return onlyAlphabets(event,this);" MaxLength="50"  class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                                         <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" runat="server"  style="display:none;" id="div6">
                                        <div class="form-group">
                                            <label class="">Training:<span style="color: Red">*</span></label>
                                           <asp:TextBox  ID="txtTraning" runat="server"   onkeypress="return onlyAlphabets(event,this);" MaxLength="50"  class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                                           <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" runat="server" id="divossg" style="display:none;">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID ="lblOOSC"></asp:Label><span style="color: Red">*</span>
                                        <%--    <label class="">OOSG<span style="color: Red">*</span></label>--%>
                                            <asp:TextBox  id="txtoosg"  Visible="false"  onkeypress="return isNumberKey(this,event);"  onchange="return validateFristNumeric(this);"  MaxLength="2" runat="server"  CssClass="form-control" />
                                               <asp:TextBox  id="txtReation"  Visible="false" onkeypress="return isNumberKey(this,event);"  onchange="return validateFristNumeric(this);"  MaxLength="2" runat="server"  CssClass="form-control" />
                                                         <asp:TextBox  id="txtEnrllment"  Visible="false" onkeypress="return isNumberKey(this,event);"  onchange="return validateFristNumeric(this);"  MaxLength="2" runat="server"  CssClass="form-control" />
                                                <asp:TextBox  id="txtsmc"  Visible="false" onkeypress="return isNumberKey(this,event);"  onchange="return validateFristNumeric(this);"  MaxLength="2" runat="server"  CssClass="form-control" />
                                                  <asp:TextBox  id="txtGKp"  Visible="false" onkeypress="return isNumberKey(this,event);"  onchange="return validateFristNumeric(this);"  MaxLength="2" runat="server"  CssClass="form-control" />
                                              <asp:TextBox  id="txtBal"  Visible="false" onkeypress="return isNumberKey(this,event);"  onchange="return validateFristNumeric(this);"  MaxLength="2" runat="server"  CssClass="form-control" />

                                        </div>
                                    </div>
                                                       <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" runat="server"  style="display:none;" id="div3">
                                                     <div class="form-group">
                                            <label class="">School:<span style="color: Red">*</span></label>
                                            
                                                                                <asp:TextBox ID="txt_pbname" autocomplete="off" ondrop="return false;" runat="server"
                                                                                    CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                                                <cc1:PopupControlExtender ID="PopupControltxt_pbname" runat="server" TargetControlID="txt_pbname"
                                                                                    PopupControlID="pnt_bookformat" OffsetY="22">
                                                                                </cc1:PopupControlExtender>
                                                                                <asp:Panel ID="pnt_bookformat" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 100%"
                                                                                    CssClass="panel">
                                                                                    <span>
                                                                                        <%--   <asp:CheckBoxList ID="ChkSchool" CssClass="_bookformat radio" runat="server" OnClientClick="SetMultilanguage3('F','_bookformat');">
                                                                                        </asp:CheckBoxList>--%>
                                                                                                 <asp:CheckBoxList ID="ChkSchool" CssClass="_bookformat radio" Onclick="return checkdataVic('F','_bookformat');"  runat="server"  >
                                                                                        </asp:CheckBoxList>
                                                                                    </span>
                                                                                    <asp:HiddenField runat="server" ID="hdn_PBName" />
                                                                                    <asp:HiddenField runat="server" ID="hdn_PBID" />
                                                                                </asp:Panel>

                                                                       

                                                       </div>
                                                     </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" runat="server"  style="display:none;" id="div7">
                                                     <div class="form-group">
                                            <label class="">School:<span style="color: Red">*</span></label>
                                                          <asp:DropDownList ID="ddlschool"  runat="server" class="form-control" />
                                                         </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" runat="server"  style="display:none;" id="div97">
                                        <div class="form-group">
                                            <label class="">Other:<span style="color: Red">*</span></label>
                                           <asp:TextBox  ID="txtOther" runat="server"   onkeypress="return onlyAlphabets(event,this);" MaxLength="50"  class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                                   <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="">Remarks</label>
                                           <asp:TextBox  ID="txtRemark" runat="server" TextMode="MultiLine" MaxLength="80"  class="form-control" rows="4"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                                </div>
                                            <div class="modal-footer">

                                                   <asp:LinkButton ID="btnSave" OnClick="btnsave_Click" class="btn btn-sm btn-primary primaryKK"    runat="server">Save</asp:LinkButton>
                           
                      <%--              <asp:Button  type="button" ID="btnSave" runat="server" OnClick="btnsave_Click" Text="Save" class="btn btn-success"></asp:Button>--%>
                                                   <asp:LinkButton ID="CancelButton" data-dismiss="modal" class="btn btn-sm btn-primary primaryKK"    runat="server">Cancel</asp:LinkButton>
                           
                                    <%--  <asp:Button ID="CancelButton" runat="server" type="button" Text="Cancel" class="btn btn-success"
                                    data-dismiss="modal"></asp:Button>--%>
                                </div>
                                            </div>
            
                                    </asp:Panel>


                  <cc1:ModalPopupExtender ID="MpexdrDistrict1" runat="server" BackgroundCssClass="modalBg "
                                        CancelControlID="CancelButton1" PopupControlID="PnlDistrict1" TargetControlID="HdnFild1">
                                    </cc1:ModalPopupExtender>
                                    <asp:HiddenField ID="HdnFild1" runat="server"></asp:HiddenField>
                                    <asp:Panel  CssClass="modal-dialog modal-lg"  Style="display: none; height: auto; width: 50% !important;border-style: groove;
                                        margin-top: -75.5px !important;" ID="PnlDistrict1" runat="server">
                                        <div style="width: 100%; height: auto; background-color: #f1f1f1">
                                            <div class="modal-header" >
                                                
                                             <h4 class="modal-title"> <asp:Label ID="lblTpye" runat="server" Text="Weekly Plan"></asp:Label>
                                                </h4>
                                                           </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                                   </div>
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"  runat="server"  id="div4">
                                        <div class="form-group">
                                            <label class="">Activity Planned <span style="color: Red">*</span></label>
                                             <asp:DropDownList ID="ddlAct"  AutoPostBack="true" runat="server"  OnSelectedIndexChanged="ddlPlanType2_Click" class="form-control" />
                                        </div>
                                    </div>
                                         <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"  runat="server" visible="false"  id="div5">
                                        <div class="form-group">
                                            <label class="">Plan Type <span style="color: Red">*</span></label>
                                            <asp:DropDownList ID="ddlPlan" runat="server" OnSelectedIndexChanged="ddlPlanType_Click"
                                            
                                            AutoPostBack="true" class="form-control " >

                                              <asp:ListItem Selected="True" Value="1">--Select--</asp:ListItem>
                                                     <%--               <asp:ListItem Value="1">Round 1 </asp:ListItem> 
                                              <asp:ListItem Value="4">Round 4 </asp:ListItem>
                                       
                                              <asp:ListItem Value="3">Round 3 </asp:ListItem>
                                             <asp:ListItem Value="4">Round 4</asp:ListItem>--%>

                                        </asp:DropDownList>
                                        </div>
                                    </div>

                                   
                                  
                                   
                                              <div class="row">

                                                  <div class="panel panel-default" style="margin-bottom: 0px;">
                                                <div class="panel-body">
                      <%--  <h4>Weekly Plan</h4>--%>

                       
                                 <div >

                                  <div class="Row WrapText table-responsive" style="min-height: 100px; height: 210px;overflow: auto; width: 100%;" align="center">
                                   <asp:GridView ID="gvTopvillage"  runat="server" DataKeyNames="VillageCode" CssClass="table table-striped table-bordered table-hover"   AutoGenerateColumns="False"  Font-Names="Arial"
                                                                        Font-Size="12px" Width="100%" style="margin-bottom:0px;" >
                                                                          <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
                                                                        </EmptyDataTemplate>
                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <Columns>
                                                                      

                                                                  <asp:TemplateField HeaderText="Village Name"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lblVillagename"  OnClick="LnkBtnBlockSc_OnClick" Text='<%# Eval("Villagename") %>'  runat="server"
                                                                                        ></asp:LinkButton>

                                                                                <asp:Label ID="lblVillagecode"  Visible="false"  Text='<%# Eval("VillageCode") %>'   runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="OOSC Univers"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbljDayes"   Text='<%# Eval("OSSCUni") %>'  OnClick="LnkBtnBlockSc_OnClick" runat="server"
                                                                                        ></asp:LinkButton>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef"  />

                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="OOSC Contacted"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lblTjBName"  Text='<%# Eval("OsscContact") %>'  OnClick="LnkBtnBlockSc_OnClick" runat="server"
                                                                                        ></asp:LinkButton>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef"  />

                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Remaining Univers"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lblTBN6ame"  Text='<%# Eval("TotalRem") %>'  OnClick="LnkBtnBlockSc_OnClick" runat="server"
                                                                                        ></asp:LinkButton>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef"  />

                                                                                

                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Retention Univers"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="dxhh"  Text='<%# Eval("RetionTarget") %>'  OnClick="LnkBtnBlockSc_OnClick" runat="server"
                                                                                        ></asp:LinkButton>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef"  />

                                                                                

                                                                            </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Score"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lblTB5N6ame"  OnClick="LnkBtnBlockSc_OnClick"  Text='<%# Eval("Score") %>'   runat="server"
                                                                                        ></asp:LinkButton>
                                                                                   
                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef"  />

                                                                                

                                                                            </asp:TemplateField>
                                                                        
                                                                         
                                                                           
                                                                        </Columns>
                                                                        
                                                                    </asp:GridView>
                                      
                                            <asp:GridView ID="gvTopvillageround4"  runat="server" DataKeyNames="VillageCode" CssClass="table table-striped table-bordered table-hover"   AutoGenerateColumns="False"  Font-Names="Arial"
                                                                        Font-Size="12px" Width="100%" style="margin-bottom:0px;" >
                                                                          <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
                                                                        </EmptyDataTemplate>
                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <Columns>
                                                                      

                                                                  <asp:TemplateField HeaderText="Village Name"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lblVillagename"  OnClick="LnkBtnBlockSc1_OnClick" Text='<%# Eval("Villagename") %>'  runat="server"
                                                                                        ></asp:LinkButton>

                                                                                <asp:Label ID="lblVillagecode"  Visible="false"  Text='<%# Eval("VillageCode") %>'   runat="server"
                                                                                        ></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="OOSC Univers"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbljDayes"   Text='<%# Eval("Taget") %>'  OnClick="LnkBtnBlockSc1_OnClick" runat="server"
                                                                                        ></asp:LinkButton>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef"  />

                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Low hanging Universe"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lblTjBName"  Text='<%# Eval("TL") %>'  OnClick="LnkBtnBlockSc1_OnClick" runat="server"
                                                                                        ></asp:LinkButton>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef"  />

                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Incomplete SR"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lblTBN6ame"  Text='<%# Eval("IncompleteSR") %>'  OnClick="LnkBtnBlockSc1_OnClick" runat="server"
                                                                                        ></asp:LinkButton>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef"  />

                                                                                

                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="School without enrollment"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="dxhh"  Text='<%# Eval("outEnrollSchool") %>'  OnClick="LnkBtnBlockSc1_OnClick" runat="server"
                                                                                        ></asp:LinkButton>

                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef"  />

                                                                                

                                                                            </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Score"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="TotalGKP"  OnClick="LnkBtnBlockSc1_OnClick"  Text='<%# Eval("TotalGKP") %>'   runat="server"
                                                                                        ></asp:LinkButton>
                                                                                   
                                                                             
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef"  />

                                                                                

                                                                            </asp:TemplateField>
                                                                        
                                                                         
                                                                           
                                                                        </Columns>
                                                                        
                                                                    </asp:GridView>
                          
                                      </div>
                                    </div>
                           
                    </div>
                                              </div>

                                              </div>
                                               </div>
                                             
                                            <div class="modal-footer" style="padding-top:0px">
                                       <asp:LinkButton ID="CancelButton1" data-dismiss="modal" class="btn btn-sm btn-primary primaryKK"    runat="server">Cancel</asp:LinkButton>
                           
                                     <%-- <asp:Button ID="CancelButton1" runat="server" type="button" Text="Cancel" class="btn btn-success"
                                    data-dismiss="modal"></asp:Button>--%>
                                </div>
                                            </div>          
                                    </asp:Panel>
            </ContentTemplate>
         </asp:UpdatePanel>
</asp:Content>


