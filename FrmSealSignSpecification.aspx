<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="FrmSealSignSpecification.aspx.cs" Inherits="FrmSealSignSpecification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="js/jquery-2.1.4.min.js" type="text/javascript"></script>
      <style>
       
       

        .modal {
            position: fixed;
            top: 80px;
            right: 0;
            bottom: 0;
            left: 0;
            z-index: 9999;
            display: none;
            overflow: hidden;
            -webkit-overflow-scrolling: touch;
            outline: 0;
        }
       

    </style>
    <style type="text/css">
        .header {
            position: absolute;
            margin-top: -44px;
        }
    </style>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        table {
            border: 1px solid #ccc;
        }

            table th {
                background-color: #F7F7F7;
                color: #333;
                font-weight: bold;
            }

            table th, table td {
                padding: 5px;
                border-color: #ccc;
            }

        form-group {
            float: left;
            width: 100%;
        }
    </style>


    <script type="text/javascript">


        function Search_Gridview(strKey, strGV) {
            debugger;

            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("ctl00_MainContent_TabContainer1_TabPanel2_GridView2");
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }
    </script>
    <script type="text/javascript">


        function Search_Gridview1(strKey, strGV) {
            debugger;

            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("ctl00_MainContent_TabContainer1_TabPanel2_GridView1");
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }

        function Search_Gridview2(strKey, strGV) {
            debugger;

            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("ctl00_MainContent_TabContainer1_TabPanel1_gvReport");
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }

        function Search_Gridview3(strKey, strGV) {
            debugger;

            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("ctl00_MainContent_TabContainer1_TabPanel1_gvD2d");
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }
    </script>
    <style type="text/css">
        .Grid th {
            color: White;
            background-color: #C1C1C1;
        }

        /* CSS to change the GridLines color */
        .Grid, .Grid th, .Grid td {
            border: 1px solid #F1F1F1 !important;
            padding: 5px;
        }

        .cls input {
            padding: 100%;
            float: left;
            width: 20px;
            height: 20px;
        }

        .ajax__tab_xp .ajax__tab_header .ajax__tab_tab {
            height: 22px !important;
        }

        .FixedHeader {
            position: absolute;
        }

        .FixedHeader1 {
            position: absolute;
        }
    </style>

    <script type="text/javascript">
        function toggleSelectionGrid1(id) {
            debugger;
            var sum = 0



            $("[id*=GridView1]").find("input[id*='Chk1']").each(function (index) {

                if ($(this).is(':checked')) {
                    var x = $(this).replace('Chk1', 'ChkOutD2d');
                    $(x).attr('checked', false);
                    sum = sum + 1;
                }
                $(this).attr('checked', false);

            });


            var sumNew = 0
            $("[id*=GridView1]").find("input[id*='ChkOutD2d']").each(function (index) {

                if ($(this).is(':checked')) {
                    sumNew = sumNew + 1;
                }


            });


            if (sum == 0) {
                $(id.childNodes[0]).prop("checked", false);

            }
            else {
                $(id.childNodes[0]).prop("checked", true);

            }
        }
        function toggleSelectionGrid2(id) {
            var sum = 0
            $("[id*=GridView2]").find("input[id*='ChkD2d']").each(function (index) {
                if ($(this).is(':checked')) {
                    sum = sum + 1;
                }
                $(this).attr('checked', false);

            });
            if (sum == 0) {
                $(id.childNodes[0]).prop("checked", false);

            }
            else {
                $(id.childNodes[0]).prop("checked", true);

            }
        }
        function DisplayConfirmationMessage(arg1) {
            if (confirm("The 1st argument is \"" + arg1 + "\" . Do you really want to proceed?"))
            {
              /*  __doPostBack('', "true");*/
                return false;
            }
            return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="mkiiainpnl121">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="padding: 5px 15px;">
                                <div class="row">
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" style="padding: 0px;">
                                        <h3 class="text-danger" style="margin: 0px;">
                                            <asp:Label ID="lblMain" runat="server" Text="Enrollment Manual Matching"></asp:Label>
                                        </h3>
                                    </div>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12 " style="padding: 0px; text-align: right;">
                                        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" CssClass="btn btn-success btn-sm"
                                            Text="Back"></asp:Button>
                                        <asp:ImageButton ID="ImageButton1" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                            Visible="false" ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnsave_Click"
                                            Style="margin-right: 5px; padding: 0px;" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-left: -5px;">
                                <div id="div-show-new">
                                    <div class="row marg search-bg" style="padding: 12px;">
                                        <div class="form-horizontal" style="text-align: left">
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                <div class="form-group" style="margin-bottom: 7px;">
                                                    <label for="email" class="col-sm-3 padd linhei">
                                                        Year:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                            class="form-control ">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        State:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                            AutoPostBack="true" class="form-control ">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblNumNo" Visible="false" runat="server" Text="Label"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        District:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                            AutoPostBack="true" class="form-control " />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Block:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                            class="form-control " />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Cluster:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"
                                                            class="form-control " />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Village:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:ListBox ID="ddlVillage" ForeColor="Black" SelectionMode="Multiple" Height="100px"
                                                            Width="100%" runat="server"></asp:ListBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlMainddd" runat="server" Visible="false">
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Block:</label>
                                                        <div class="col-sm-9 padd">
                                                            <asp:DropDownList ID="ddlserachblock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlockserachblock_SelectedIndexChanged"
                                                                class="form-control " />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Cluster:</label>
                                                        <div class="col-sm-9 padd">
                                                            <asp:DropDownList ID="ddlCluster" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCluster_SelectedIndexChanged"
                                                                class="form-control " />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Village:</label>
                                                        <div class="col-sm-9 padd">
                                                            <asp:ListBox ID="ddlVillageD2d" ForeColor="Black" SelectionMode="Multiple" Height="100px"
                                                                Width="100%" runat="server"></asp:ListBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <%--  <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                
                                                        <asp:CheckBox ID="chkMatch" Text="Match"  runat="server"></asp:CheckBox>
                                                      
                                                    
                                            </div>--%>
                                            <div style="margin-left: -6px;" class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                                <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-right"
                                                    BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                                                <asp:LinkButton ID="lnk" runat="server" OnClick="btnSdh_Click">>></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12" style="margin-bottom:15px">
                                    <div style="width: 100%;  overflow:auto">
                                        <cc1:TabContainer ID="TabContainer1" runat="server" Style="margin-top: 15px; padding-left: 0px;">
                                            <cc1:TabPanel runat="server" HeaderText="Potential match" ID="TabPanel1">
                                                <ContentTemplate>
                                                    <asp:Panel ID="pnlmain" runat="server" Width="100%">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                                        <span class="col-lg-6" style="padding: 0px;">
                                                                            <h class="text-danger">Out Of Door To Door </h>
                                                                        </span>
                                                                        <%--   <span style="padding-right:4px;" >
                                                     R- Remove  </span>
                                                     <span style="padding-left:4px">M- Matching  </span>
                                                     <span style="padding-left:4px" >UN- Unique 
                                                </span>--%>
                                                                    </div>
                                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                                        <span class="col-lg-12" style="padding: 0px;">
                                                                            <h class="text-danger">Potential matches (found by algorithm in current year D2D target list)  </h>
                                                                        </span>
                                                                        <%--  <span style="padding-right:4px;" >
                                                     R- Remove  </span>
                                                     <span style="padding-left:4px">M- Matching  </span>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row table-responsive">
                                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                                    <div class="row">
                                                                        <div class="row marg search-bg">
                                                                            <div class="form-horizontal">
                                                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                                                                                    <div class="form-group">
                                                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                                            Search</label>
                                                                                        <div class="col-sm-7" style="display: none">
                                                                                            <asp:DropDownList ID="ddl_MatchByOut" runat="server" class="form-control ">
                                                                                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                                                <asp:ListItem Value="1" Text="Village Name"></asp:ListItem>
                                                                                                <asp:ListItem Value="2" Text="House No."></asp:ListItem>
                                                                                                <asp:ListItem Value="3" Text="Child Name"></asp:ListItem>
                                                                                                <asp:ListItem Value="4" Text="Father Name"></asp:ListItem>
                                                                                                <%--  <asp:ListItem Value="5" Text="UniqueId"></asp:ListItem>--%>
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-lg-6 col-md-6 col-sm-6 cpl-xs-12">
                                                                                    <div class="form-group">
                                                                                        <div class="col-sm-12">
                                                                                            <asp:TextBox ID="Txt_VillageOUT" onkeyup="Search_Gridview2(this, 'gvReport')" runat="server" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0" style="display: none">
                                                                                    <asp:ImageButton ID="ImgOutDur" ToolTip="Serach" runat="server" Enabled="false" class="btn btn-danger btn-paddd pull-right"
                                                                                        BackColor="#f1f1f1" OnClick="ImgOutDur_Click" ImageUrl="~/images/search-29.png" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <asp:UpdatePanel ID="UpdGrdLeftID" runat="server">
                                                                        <ContentTemplate>
                                                                            <div style="overflow: auto; max-height: 270px;">
                                                                                <asp:GridView ID="gvReport" runat="server" AllowSorting="true" CssClass="Grid" OnRowCommand="GVMain_OnRowCommand"
                                                                                    PageSize="500" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="12px"
                                                                                    PagerSettings-Position="TopAndBottom" Width="100%" DataKeyNames="UniqueCode">
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
                                                                                        <asp:TemplateField HeaderText="R" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="Chk1" runat="server" onclick="CheckOne(this)" />
                                                                                                <asp:Label ID="lbl1" Visible="false" runat="server" Text='<%# Eval("U") %>'></asp:Label>
                                                                                                <asp:Label ID="lblUniqueCode" runat="server" Text='<%# Eval("UniqueCode") %>'></asp:Label>
                                                                                           <asp:Label ID="lblDDob" runat="server" Text='<%# Eval("DOB") %>'></asp:Label>
                                                                                                
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="M" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="Chk2" runat="server" onclick="CheckOne(this)" />
                                                                                                <asp:Label ID="lbl2" Visible="false" runat="server" Text='<%# Eval("K") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="UN" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="ChkOutD2d" runat="server" onclick="CheckOne(this)" />
                                                                                                <asp:Label ID="lblOutD2d" Visible="false" runat="server" Text='<%# Eval("TempId") %>'></asp:Label>
                                                                                                 <asp:Label ID="lblTempDob" Visible="false" runat="server" Text='<%# Eval("DOB") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:ButtonField HeaderText="Panchayat Name " ItemStyle-ForeColor="#0000FF" DataTextField="PanchayatName"
                                                                                            CommandName="GVUIO">
                                                                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                                                                            <HeaderStyle CssClass="padding-lef" />
                                                                                        </asp:ButtonField>
                                                                                        <asp:ButtonField HeaderText="Village Name" ItemStyle-ForeColor="#0000FF" DataTextField="VillageName"
                                                                                            CommandName="GVUIO">
                                                                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                                                                            <HeaderStyle CssClass="padding-lef" />
                                                                                        </asp:ButtonField>
                                                                                        <asp:ButtonField HeaderText="UniqueId" ItemStyle-ForeColor="#0000FF" Visible="false"
                                                                                            DataTextField="NewUniqueId" CommandName="GVUIO">
                                                                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                                                                            <HeaderStyle CssClass="padding-lef" />
                                                                                        </asp:ButtonField>
                                                                                        <asp:ButtonField HeaderText="HH No." ItemStyle-ForeColor="#0000FF" DataTextField="House"
                                                                                            CommandName="GVUIO">
                                                                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                                                                            <HeaderStyle CssClass="padding-lef" />
                                                                                        </asp:ButtonField>
                                                                                        <asp:ButtonField HeaderText="Child Name" ItemStyle-ForeColor="#0000FF" DataTextField="ChildName"
                                                                                            CommandName="GVUIO">
                                                                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                                                                            <HeaderStyle CssClass="padding-lef" />
                                                                                        </asp:ButtonField>
                                                                                        <asp:ButtonField HeaderText="Father Name" ItemStyle-ForeColor="#0000FF" DataTextField="FathersName"
                                                                                            CommandName="GVUIO">
                                                                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                                                                            <HeaderStyle CssClass="padding-lef" />
                                                                                        </asp:ButtonField>
                                                                                        <asp:ButtonField HeaderText="Age" ItemStyle-ForeColor="#0000FF" DataTextField="Age"
                                                                                            CommandName="GVUIO">
                                                                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                                                                            <HeaderStyle CssClass="padding-lef" />
                                                                                        </asp:ButtonField>
                                                                                        <asp:ButtonField HeaderText="Gender" ItemStyle-ForeColor="#0000FF" DataTextField="Gender"
                                                                                            CommandName="GVUIO">
                                                                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                                                                            <HeaderStyle CssClass="padding-lef" />
                                                                                        </asp:ButtonField>
                                                                                        <asp:ButtonField HeaderText="Social Category" ItemStyle-ForeColor="#0000FF" DataTextField="SocialCategory"
                                                                                            CommandName="GVUIO">
                                                                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                                                                            <HeaderStyle CssClass="padding-lef" />
                                                                                        </asp:ButtonField>
                                                                                        <asp:ButtonField HeaderText="Class" ItemStyle-ForeColor="#0000FF" DataTextField="Class"
                                                                                            CommandName="GVUIO">
                                                                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                                                                            <HeaderStyle CssClass="padding-lef" />
                                                                                        </asp:ButtonField>
                                                                                        <asp:ButtonField HeaderText="Match Count" ItemStyle-ForeColor="#0000FF" DataTextField="MatchingCount"
                                                                                            CommandName="GVUIO">
                                                                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                                                                            <HeaderStyle CssClass="padding-lef" />
                                                                                        </asp:ButtonField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <asp:Label ID="lblUniqueCode" runat="server" Visible="false"></asp:Label>
                                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                                    <div class="row">
                                                                        <div class="row marg search-bg">
                                                                            <div class="form-horizontal">
                                                                                <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                                                    <div class="form-group">
                                                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                                            Search</label>
                                                                                        <div class="col-sm-7" style="display: none">
                                                                                            <asp:DropDownList ID="ddl_MatchDTD" runat="server" class="form-control ">
                                                                                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                                                <asp:ListItem Value="1" Text="Village Name"></asp:ListItem>
                                                                                                <asp:ListItem Value="2" Text="House No."></asp:ListItem>
                                                                                                <asp:ListItem Value="3" Text="Child Name"></asp:ListItem>
                                                                                                <asp:ListItem Value="4" Text="Father Name"></asp:ListItem>
                                                                                                <%-- <asp:ListItem Value="5" Text="UniqueId"></asp:ListItem>--%>
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-lg-6 col-md-6 col-sm-6 cpl-xs-12">
                                                                                    <div class="form-group">
                                                                                        <div class="col-sm-11">
                                                                                            <asp:TextBox ID="Txt_VillageDTD" onkeyup="Search_Gridview3(this, 'gvD2d')" runat="server" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div style="display: none" class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                                                                    <asp:ImageButton ID="IMG_DTDSerch" ToolTip="Serach" Enabled="false" runat="server"
                                                                                        class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1" OnClick="IMG_DTDSerch_Click"
                                                                                        ImageUrl="~/images/search-29.png" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div style="overflow: auto; max-height: 270px;">
                                                                        <div>
                                                                            <div class="row table-responsive">
                                                                                <asp:GridView ID="gvD2d" runat="server" CssClass="Grid" AllowSorting="true" PageSize="800"
                                                                                    AutoGenerateColumns="False" Font-Names="Arial" Font-Size="12px" Width="100%">
                                                                                    <EmptyDataTemplate>
                                                                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                            Data not found
                                                                                        </div>
                                                                                    </EmptyDataTemplate>
                                                                                    <FooterStyle CssClass="FooterStyle" />
                                                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" Width="95%" HorizontalAlign="Center" />
                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                    <PagerStyle CssClass="paging" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="M">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="ChkD2d" runat="server" onclick="CheckOne(this)" CssClass="cls"
                                                                                                    Style="width: 15px; height: 20px;" />
                                                                                                <asp:Label ID="lblD2d" Visible="false" runat="server" Text='<%# Eval("TempID") %>'></asp:Label>
                                                                                                <asp:Label ID="lblD2dUniqueCode" class="labelGrid" ForeColor="Black" runat="server"
                                                                                                    Visible="false" Text='<%# Eval("UniqueCode") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Panchayat Name">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblHofdduse" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("PanchayatName") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Village Name">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblHoffdduse" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("VillageName") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="UniqueId" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblNewUniqueffId" class="labelGrid" ForeColor="Black" runat="server"
                                                                                                    Text='<%# Eval("UniqueId") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="HH No." SortExpression="House">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblHouse" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("House") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Child Name" SortExpression="ChildName">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblHouse2" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("ChildName") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Father Name" SortExpression="FathersName">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="ddlEmployeeCode" class="labelGrid" ForeColor="Black" ItemStyle-ForeColor="#333"
                                                                                                    runat="server" Text='<%# Eval("FathersName") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Current Age" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Txtunit" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Age") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Gender">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblGender" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Social Category" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblSocialCategory" class="labelGrid" ForeColor="Black" runat="server"
                                                                                                    Text='<%# Eval("SocialCategory") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Class" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblClass" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
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
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                                            <div class="row">
                                                                <div class="col-lg-6 col-lg-offset-8  col-md-6 col-md-offset-8 col-sm-6 col-sm-offset-8 col-xs-12 col-xs-offset-0"
                                                                    style="text-align: left; margin-left: 35%">
                                                                    <span>
                                                                        <asp:Button ID="btnMove" runat="server" Width="20%" Text="Submit" Style="margin-left: 5px;" CssClass="btn btn-success btn-sm"
                                                                            OnClientClick="javascript:return(confirm('Are you sure you want to submit!'))"
                                                                            OnClick="btnMatch_Click" />
                                                                        <asp:Button ID="btnSumbit" runat="server" Width="20%" Style="margin-left: 5px;" OnClick="btnSumbit_Click" Visible="false"
                                                                            OnClientClick="javascript:return(confirm('Are you sure you want to submit!'))"
                                                                            CssClass="btn btn-success  btn-sm" Text="Not Match" />
                                                                        <asp:Button ID="btnrest" runat="server" Visible="false" Style="margin-left: 5px;" OnClick="btnRest_Click" Width="20%"
                                                                            CssClass="btn-danger btn-sm" Text="Reset" />
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Manual matching">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                                        <span class="col-lg-6" style="padding: 0px;">
                                                                            <h class="text-danger">Out Of Door To Door </h>
                                                                        </span>
                                                                        <%--   <span style="padding-right:4px;" >
                                                     R- Remove  </span>
                                                     <span style="padding-left:4px">M- Matching  </span>
                                                     <span style="padding-left:4px" >UN- Unique 
                                                </span>--%>
                                                                    </div>
                                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                                        <span class="col-lg-12" style="padding: 0px;">
                                                                            <h class="text-danger">Manual matching D2D list  </h>
                                                                        </span>
                                                                        <%--  <span style="padding-right:4px;" >
                                                     R- Remove  </span>
                                                     <span style="padding-left:4px">M- Matching  </span>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row table-responsive">
                                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                                    <div class="row">
                                                                        <div class="row marg search-bg">
                                                                            <div class="form-horizontal">
                                                                                <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                                                    <label for="email" class="col-sm-4 padd linhei" style="padding-top: 2px;">Search</label>
                                                                                </div>
                                                                                <div class="col-lg-3 col-md-6 col-sm-6 cpl-xs-12" style="display: none">
                                                                                    <asp:DropDownList ID="ddlSearch1" runat="server" class="form-control ">
                                                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                                        <asp:ListItem Value="1" Text="Village Name"></asp:ListItem>
                                                                                        <asp:ListItem Value="2" Text="HH No."></asp:ListItem>
                                                                                        <asp:ListItem Value="3" Text="Child Name"></asp:ListItem>
                                                                                        <asp:ListItem Value="4" Text="Father Name"></asp:ListItem>
                                                                                        <%--  <asp:ListItem Value="5" Text="UniqueId"></asp:ListItem>--%>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-lg-6 col-md-6 col-sm-6 cpl-xs-12">
                                                                                    <asp:TextBox ID="TxtSearch1" runat="server" onkeyup="Search_Gridview1(this, 'GridView1')" class="form-control" placeholder="Search" Style="font-size: 10px;" />
                                                                                </div>
                                                                                <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12" style="display: none">
                                                                                    <asp:ImageButton ID="ImageButton2" ToolTip="Serach" runat="server" Enabled="false"
                                                                                        class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1" OnClick="ImgOutDur1_Click"
                                                                                        ImageUrl="~/images/search-29.png" />
                                                                                </div>
                                                                                <div class="col-lg-3 col-md-4 col-sm-4 cpl-xs-12" style="display: none">
                                                                                    <asp:TextBox ID="txtSearchHHNO2" runat="server" class="form-control" placeholder="Search" Style="font-size: 10px;" Enabled="false" />
                                                                                </div>
                                                                                <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12" style="display: none">
                                                                                    <asp:ImageButton ID="ImageButton5" ToolTip="Serach" Enabled="false" runat="server" class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" OnClick="ImageButton5_Click" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row">
                                                                        <%--<div>
                                                                <table class="table table-striped table-bordered" style="margin-bottom: 0px;">
                                                                <thead>
                                                                    <tr align="center" style="color: White; background-color: #C1C1C1; height:40px;font-size:11px;">
                                                                        <th scope="col">&nbsp;</th>
                                                                        <th scope="col" style="width: 130px;">Panchayat</th>
                                                                        <th scope="col" style="width: 130px;">Village</th>
                                                                        <th scope="col" style="width: 130px;">HH No.</th>
                                                                        <th scope="col" style="width: 130px;">Child Name</th>
                                                                        <th scope="col" style="width: 130px;">Father Name</th>
                                                                        <th scope="col" style="width: 130px;">Age</th>
                                                                        <th scope="col" style="width: 130px;">Gender</th>
                                                                        <th scope="col" style="width: 130px;">Social Category</th>
                                                                        <th scope="col" style="width: 100px;">Class</th>
                                                                        <%--<th scope="col" style="width: 100px;">Education Activity</th>
                                                                    </tr>                                                                  
                                                                </thead>
                                                            </table>
                                                               </div>--%>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div style="overflow-y: scroll; height: 250px; border: 0px LightGray solid; margin-left: 0px">
                                                                            <asp:GridView ID="GridView1" runat="server" AllowSorting="true" CssClass="Grid" PageSize="500"
                                                                                AutoGenerateColumns="False" Font-Names="Arial" Font-Size="12px" PagerSettings-Position="TopAndBottom"
                                                                                Width="99%" DataKeyNames="UniqueCode" ShowHeader="true">
                                                                                <EmptyDataTemplate>
                                                                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                        Data not found
                                                                                    </div>
                                                                                </EmptyDataTemplate>
                                                                                <FooterStyle CssClass="FooterStyle" />
                                                                                <HeaderStyle BackColor="#C1C1C1" Width="95%" ForeColor="White" Height="32px" HorizontalAlign="Center" />
                                                                                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                <PagerStyle CssClass="paging" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="M">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="Chk1" runat="server" CssClass="cls" onchange="toggleSelectionGrid1(this);" />
                                                                                            <asp:Label ID="lbl1" Visible="false" runat="server" Text='<%# Eval("U") %>'></asp:Label>
                                                                                            <asp:Label ID="lblUniqueCode" Visible="false" runat="server" Text='<%# Eval("UniqueCode") %>'></asp:Label>
                                                                                        </ItemTemplate>

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="M" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="Chk2" runat="server" onclick="CheckOne(this)" />
                                                                                            <asp:Label ID="lbl2" Visible="false" runat="server" Text='<%# Eval("K") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="H">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="ChkOutD2d" CssClass="cls" runat="server" />
                                                                                            <asp:Label ID="lblOutD2d" Visible="false" runat="server" Text='<%# Eval("TempId") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Other Village">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblPanchayatName" runat="server" Text='<%# Eval("otherVillageName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="130px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Village Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblVillageName" runat="server" Text='<%# Eval("VillageName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="130px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="UniqueId" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblUniqueId" Visible="false" runat="server" Text='<%# Eval("UniqueId") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="130px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="HH No.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblHouse" runat="server" Text='<%# Eval("House") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="130px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Child Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblChildName" runat="server" Text='<%# Eval("ChildName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="130px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Father Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblFathersName" runat="server" Text='<%# Eval("FathersName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="130px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Age">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblAge" runat="server" Text='<%# Eval("Age") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="130px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Gender">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblGender" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="130px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Social Category">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSocialCategory" runat="server" Text='<%# Eval("SocialCategory") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="130px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Class">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblClass" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="100px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Match Count" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblMatchingCount" runat="server" Text='<%# Eval("MatchingCount") %>'></asp:Label>

                                                                                             <asp:Label ID="lblDOB" runat="server" Text='<%# Eval("DOB") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                                    <div class="row">
                                                                        <div class="row marg search-bg">
                                                                            <div class="form-horizontal">
                                                                                <div class="col-lg-2 col-md-2 col-sm-6 cpl-xs-12">
                                                                                    <label for="email" class="padd linhei" style="padding-top: 2px;">Search : </label>
                                                                                </div>
                                                                                <div class="col-lg-3 col-md-6 col-sm-6 cpl-xs-12" style="display: none;">
                                                                                    <asp:DropDownList ID="ddlS2" runat="server" class="form-control" OnSelectedIndexChanged="ddlS2_SelectedIndexChanged">
                                                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                                        <asp:ListItem Value="1" Text="Village Name"></asp:ListItem>
                                                                                        <asp:ListItem Value="2" Text="HH No."></asp:ListItem>
                                                                                        <asp:ListItem Value="3" Text="Child Name"></asp:ListItem>
                                                                                        <asp:ListItem Value="4" Text="Father Name"></asp:ListItem>
                                                                                        <%-- <asp:ListItem Value="5" Text="UniqueId"></asp:ListItem>--%>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-lg-6 col-md-6 col-sm-6 cpl-xs-12">
                                                                                    <asp:TextBox ID="TxtSearch2" runat="server" onkeyup="Search_Gridview(this, 'GridView2')" class="form-control" placeholder="Search" Style="font-size: 10px;" />

                                                                                </div>
                                                                                <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12" style="display: none;">
                                                                                    <asp:ImageButton ID="ImageButton3" ToolTip="Serach" Enabled="false" runat="server"
                                                                                        class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1" OnClick="IMG_DTDSerch1_Click"
                                                                                        ImageUrl="~/images/search-29.png" />
                                                                                </div>
                                                                                <div class="col-lg-3 col-md-4 col-sm-4 cpl-xs-12" style="display: none;">
                                                                                    <asp:TextBox ID="txtSearchHHNo" runat="server" class="form-control" placeholder="Search" Style="font-size: 10px;" Enabled="false" />
                                                                                </div>
                                                                                <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12" style="display: none;">
                                                                                    <asp:ImageButton ID="ImageButton4" ToolTip="Serach" Enabled="false" runat="server"
                                                                                        class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1"
                                                                                        ImageUrl="~/images/search-29.png" OnClick="ImageBn4_Click" />

                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>


                                                                    <div class="row">

                                                                        <div style="overflow-y: scroll; height: 250px; border: 0px LightGray solid; margin-left: 0px">
                                                                            <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-responsive Grid" AllowSorting="true" PageSize="170" OnPageIndexChanging="GridView2_PageIndexChanging"
                                                                                AutoGenerateColumns="False" ShowHeader="true" Font-Names="Arial" Font-Size="11px" Width="99%">
                                                                                <EmptyDataTemplate>
                                                                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                        Data not found
                                                                                    </div>
                                                                                </EmptyDataTemplate>
                                                                                <FooterStyle CssClass="FooterStyle" />
                                                                                <HeaderStyle BackColor="#C1C1C1" Width="93%" ForeColor="White" Height="20px" HorizontalAlign="Center" />
                                                                                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                <PagerStyle CssClass="paging" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="M" HeaderStyle-Width="35px">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="ChkD2d" runat="server" CssClass="cls" onchange="toggleSelectionGrid2(this);"
                                                                                                Style="width: 8px; height: 8px;" />
                                                                                            <asp:Label ID="lblD2d" Visible="false" runat="server" Text='<%# Eval("TempID") %>'></asp:Label>
                                                                                            <asp:Label ID="lblD2dUniqueCode" class="labelGrid" ForeColor="Black" runat="server"
                                                                                                Visible="false" Text='<%# Eval("UniqueCode") %>'></asp:Label>

                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="35px" />
                                                                                    </asp:TemplateField>

                                                                                    <%--  <asp:TemplateField HeaderText="Panchayat" >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblHofdduse" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("PanchayatName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="130px" />
                                                                            </asp:TemplateField>--%>
                                                                                    <asp:BoundField DataField="VillageName" HeaderText="VillageName" ItemStyle-Width="100" />
                                                                                    <%--<asp:TemplateField HeaderText="Village" HeaderStyle-Width="62px">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblHoffdduse" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("VillageName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="62px" />
                                                                            </asp:TemplateField>--%>
                                                                                    <asp:TemplateField HeaderText="UniqueId" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblNewUniqueffId" class="labelGrid" ForeColor="Black" runat="server"
                                                                                                Text='<%# Eval("UniqueId") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="HH No." SortExpression="House" HeaderStyle-Width="30px">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblHouse" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("House") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="30px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Child Name" SortExpression="ChildName" HeaderStyle-Width="72px">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblChildName" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("ChildName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="72px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Father Name" SortExpression="FathersName" HeaderStyle-Width="72px">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="ddlEmployeeCode" class="labelGrid" ForeColor="Black" ItemStyle-ForeColor="#333"
                                                                                                runat="server" Text='<%# Eval("FathersName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="72px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Current Age" Visible="true" HeaderStyle-Width="30px">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Txtunit" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Age") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="30px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Gender" HeaderStyle-Width="30px">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblGender" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="30px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Social Category" Visible="true" HeaderStyle-Width="40px">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSocialCategory" class="labelGrid" ForeColor="Black" runat="server"
                                                                                                Text='<%# Eval("SocialCategory") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="40px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Class" HeaderStyle-Width="30px">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblClass" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="30px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Education Status" HeaderStyle-Width="30px">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblClasdds" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("EduationStatus") %>'></asp:Label>
                                                                                        
                                                                                        <asp:Label ID="lblDOBNew" class="labelGrid" Visible="false" ForeColor="Black" runat="server" Text='<%# Eval("DOBNew") %>'></asp:Label>
                                                                                         <asp:Label ID="lblCChildName" class="labelGrid" Visible="false" ForeColor="Black" runat="server" Text='<%# Eval("CChildName") %>'></asp:Label>
                                                                                        <asp:Label ID="lblFatherName" class="labelGrid" Visible="false" ForeColor="Black" runat="server" Text='<%# Eval("FatherName") %>'></asp:Label>
                                                                                <asp:Label ID="lblIsD2dContact" class="labelGrid" Visible="false" ForeColor="Black" runat="server" Text='<%# Eval("IsD2dContact") %>'></asp:Label>
                                                             
                                                                                        
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="40px" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                                            <div class="row">
                                                                <div class="col-lg-6 col-lg-offset-8  col-md-6 col-md-offset-8 col-sm-6 col-sm-offset-8 col-xs-12 col-xs-offset-0"
                                                                    style="text-align: left; margin-left: 35%">
                                                                    <span>
                                                                        <asp:Button ID="Button1" runat="server" Width="20%" Text="Hide" Style="margin-left: 5px;" CssClass="btn btn-success  btn-sm"
                                                                            OnClick="BtnHIde_Click" OnClientClick="javascript:return(confirm('Are you sure you want to Hide!'))" />
                                                                        <asp:Button ID="BtnBoSubmit" runat="server" Width="20%" Style="margin-left: 5px;" Text="Submit" CssClass="btn btn-success  btn-sm"
                                                                            OnClick="BtnBoSubmit_Click"  OnClientClick="javascript:return(confirm('Are you sure you want to submit!'))"/>

                                                                        <asp:Button ID="Button3" runat="server" Visible="false" Style="margin-left: 5px;" OnClick="btnRest_Click" Width="20%"
                                                                            CssClass="btn-danger  btn-sm" Text="Reset" />
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>

                    <%-- <div class="row" id="Pnldel" runat="server">
                    <div class="col-xs-12">
                        <div class="modal-dialog modal-lg" role="dialog">
                            <!-- Modal content-->
                            
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">
                                        <asp:Label ID="lbldel" runat="server" Text="Confirm ?" Font-Bold="True"></asp:Label>
                                        <div class="row">
                                        </div>
                                </div>
                            </div>


                           
                        <div class="row">
                                      <asp:Label ID="Label4" runat="server" Text="" Font-Bold="True"></asp:Label>
                                      
                                   </div>

                            <div class="modal-footer" style="background-color: #ffffff;">
                                <asp:Label ID="Label2" runat="server" Text="" Font-Bold="True"></asp:Label>
                                <asp:Button ID="BtnDeleteYes" runat="server" type="button" Text="Yes" class="btn btn-success  btn-sm"
                                    OnClick="BtnBoSubmit1_Click"></asp:Button>
                                <asp:Button ID="BtnDeleteNo" runat="server" type="button" Text="No" class="btn btn-success  btn-sm"
                                    data-dismiss="modal"></asp:Button>
                            </div>
                        </div>
                        <cc1:ModalPopupExtender ID="ModalPopupExtenderDelPartner" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="BtnDeleteNo" PopupControlID="Pnldel" TargetControlID="HiddenFieldDelPartner">
                        </cc1:ModalPopupExtender>
                        <asp:HiddenField ID="HiddenFieldDelPartner" runat="server"></asp:HiddenField>
                        <br />
                    </div>
                </div>
                                             --%>
  


                      
                                                       </ContentTemplate>

                                            </cc1:TabPanel>
                                        </cc1:TabContainer>

                                         <cc1:ModalPopupExtender ID="MpexdrDistrict" runat="server" BackgroundCssClass="modalBg "
                                        CancelControlID="CancelButton" PopupControlID="PnlDistrict" TargetControlID="HdnFild">
                                    </cc1:ModalPopupExtender>
                                    <asp:HiddenField ID="HdnFild" runat="server"></asp:HiddenField>
                                    <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important;
                                        margin-top: -75.5px !important;" ID="PnlDistrict" runat="server">
                                        <div style="width: 100%; height: auto; background-color: #f1f1f1">
                                            <div class="modal-header" style="background-color: #ddd; color: White;">
                                                <h4 class="modal-title" style="forecolor: White">
                                                    </h4>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row" >
                                               
                                            
                                                <div id="Div3" class="col-lg-12 col-md-12 col-sm-12 cpl-xs-12" visible="false" runat="server">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" >
                                                            School:</label>
                                                        <div   style="padding-left: 15px;">
                                                             <asp:Label ID="lblSchool" class="padd " ForeColor="Black"  runat="server" Text="Label" ></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                  
                                                     <div id="Div1" class="col-lg-12 col-md-12 col-sm-12 cpl-xs-12" runat="server">
                                                          <asp:Label ID="Label3" runat="server" Text="" Font-Bold="True"></asp:Label><br />
                                                            <asp:Label ID="Label4" runat="server" Text="" Font-Bold="True"></asp:Label><br />
                                                            <asp:Label ID="Label5" runat="server" Text="" Font-Bold="True"></asp:Label>
                                                        </div>
                                              
                                            <div class="modal-footer">
                                              <div id="Div11" class="col-lg-12 col-md-12 col-sm-12 cpl-xs-12" runat="server">
                                                   
                                <asp:Button ID="Button2" runat="server" type="button" Text="Submit" class="btn btn-success  btn-sm"
                                    OnClick="BtnBoSubmit1_Click"></asp:Button>
                                                  <asp:Button ID="Button4" runat="server" type="button" Text="Submit" class="btn btn-success  btn-sm"
                                    OnClick="btnMatch1_Click"></asp:Button>
                                <asp:Button ID="CancelButton" runat="server" type="button" Text="No" class="btn btn-success  btn-sm"
                                    data-dismiss="modal"></asp:Button>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

