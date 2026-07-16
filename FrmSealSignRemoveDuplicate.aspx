<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FrmSealSignRemoveDuplicate.aspx.cs" Inherits="FrmSealSignRemoveDuplicate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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

        #div-show-new {
            float: right;
            width: calc(100% - 24px);
            min-height: 35px;
            /* background-color: #ddd; */
            color: #fff;
            text-align: center;
            text-decoration: underline;
            /* border: 2px solid #ccc; */
            border-radius: 4px;
            display: block;
            position: relative;
            right: 12px;
            top: 0px;
            z-index: 1;
        }
    </style>

    <script type="text/javascript">
        function CheckOne(rb) {
            var gv = document.getElementById("<%=gvD2d.ClientID%>");
            var row = rb.parentNode.parentNode;
            var rbs = row.getElementsByTagName("input");
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "checkbox") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }
        function CheckOne(rb) {
            var gv = document.getElementById("<%=gvReport.ClientID%>");
            var row = rb.parentNode.parentNode;
            var rbs = row.getElementsByTagName("input");
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "checkbox") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="mkiiainpnl121">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="panel panel-default" style="height: 820px;">
                            <div class="panel-heading" style="padding:5px 15px">
                                <div class="row">
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" style="padding: 0px;">
                                        <h3 class="text-danger" style="margin: 0px;">
                                            <asp:Label ID="lblMain" runat="server" Text="Enrollment Duplicate"></asp:Label>
                                        </h3>
                                    </div>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12 " style="padding: 0px; text-align: right;">
                                        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" CssClass="btn btn-success btn-sm" Text="Back"></asp:Button>
                                        <asp:ImageButton ID="ImageButton1" Visible="false" CssClass="btn btn-info pull-right btn-sm"
                                            BackColor="#f5f5f5" ToolTip="Save" ImageUrl="~/images/save-29-1.png" Style="margin-right: 5px; padding: 0px;"
                                            runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div id="div-show-new">
                                    <div class="row marg search-bg" style="padding: 12px;">
                                        <div class="form-horizontal">
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                <div class="form-group" style="margin-bottom: 7px;">
                                                    <label for="email" class="col-sm-3 padd linhei" style="text-align: justify;">
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
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;text-align: justify;">
                                                        Clutser:</label>
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
                                                        <%--<asp:DropDownList ID="ddlVillage" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged"
                                                             runat="server" class="form-control " />
                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                                                                ValidationGroup="saves" ControlToValidate="ddlVillage" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </span>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 ">
                                                <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-left"
                                                    BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlmain" runat="server">
                                <div class="panel-body" style="padding: 12px;">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" style="padding: 0px;" >
                                                <span class="col-lg-6" style="padding: 0px;">
                                                    <h class="text-danger">Current Enrolment  </h>
                                                </span>
                                                <%--   <span style="padding-right:4px;" >
                                                     R- Remove  </span>
                                                     <span style="padding-left:4px">M- Matching  </span>
                                                     <span style="padding-left:4px" >UN- Unique 
                                                </span>--%>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" >
                                                <span class="col-lg-12" >
                                                    <h class="text-danger">Potential matches (found by algorithm in current and previous years enrolment data)</h>
                                                </span>
                                                <%--  <span style="padding-right:4px;" >
                                                     R- Remove  </span>
                                                     <span style="padding-left:4px">M- Matching  </span>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row table-responsive">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" style="padding: 0px;">
                                            <div class="row">
                                                <div class="row marg search-bg">
                                                    <div class="form-horizontal">
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;padding-left: 15px;">
                                                                    Search :</label>
                                                                <div class="col-sm-7">
                                                                    <asp:DropDownList ID="ddl_MatchByOut" runat="server" class="form-control ">
                                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="Village Name"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="House No."></asp:ListItem>
                                                                        <asp:ListItem Value="3" Text="Child Name"></asp:ListItem>
                                                                        <asp:ListItem Value="4" Text="Father Name"></asp:ListItem>
                                                                        <asp:ListItem Value="5" Text="UniqueId"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-4 cpl-xs-12">
                                                            <div class="form-group">
                                                                <div class="col-sm-12">
                                                                    <asp:TextBox ID="Txt_VillageOUT" runat="server" class="form-control" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 ">
                                                            <asp:ImageButton ID="ImgOutDur" ToolTip="Serach" runat="server" Enabled="false" class="btn btn-danger btn-paddd pull-left"
                                                                BackColor="#f1f1f1" OnClick="ImgOutDur_Click" ImageUrl="~/images/search-29.png" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Label ID="lblUniqueCode" runat="server" Visible="false"></asp:Label>
                                            <div id="TabledivGrid" style="overflow: auto; max-height: 270px;">
                                                <asp:GridView ID="gvReport" runat="server" AllowSorting="true" CssClass="Grid" OnRowCommand="GVMain_OnRowCommand" PageSize="500"
                                                    AutoGenerateColumns="False" Font-Names="Arial" Font-Size="12px" Width="100%" DataKeyNames="EnrollCode">
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

                                                        <asp:TemplateField HeaderText="M" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk2" runat="server" onclick="CheckOne(this)" AutoPostBack="true" OnCheckedChanged="lnk_Onclick" />
                                                                <asp:Label ID="lblUniqueCode" Visible="false" runat="server" Text='<%# Eval("EnrollCode") %>'></asp:Label>
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
                                                        <asp:ButtonField HeaderText="School Name" ItemStyle-ForeColor="#0000FF" DataTextField="SchoolName"
                                                            CommandName="GVUIO">
                                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                                            <HeaderStyle CssClass="padding-lef" />
                                                        </asp:ButtonField>
                                                        <asp:ButtonField HeaderText="UniqueId" Visible="false" ItemStyle-ForeColor="#0000FF" DataTextField="NewUniqueId"
                                                            CommandName="GVUIO">
                                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                                            <HeaderStyle CssClass="padding-lef" />
                                                        </asp:ButtonField>
                                                        <asp:ButtonField HeaderText="Session" ItemStyle-ForeColor="#0000FF" Visible="false" DataTextField="Session"
                                                            CommandName="GVUIO">
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
                                                        <asp:ButtonField HeaderText="Sr.No" ItemStyle-ForeColor="#0000FF" DataTextField="Serial"
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
                                                        <asp:ButtonField HeaderText="Class" ItemStyle-ForeColor="#0000FF" DataTextField="Class"
                                                            CommandName="GVUIO">
                                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                                            <HeaderStyle CssClass="padding-lef" />
                                                        </asp:ButtonField>
                                                        <asp:ButtonField HeaderText="Social Category" ItemStyle-ForeColor="#0000FF" DataTextField="SocialCategory"
                                                            CommandName="GVUIO">
                                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                                            <HeaderStyle CssClass="padding-lef" />
                                                        </asp:ButtonField>



                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" style="padding-right: 0px;">
                                            <div class="row">
                                                <div class="row marg search-bg">
                                                    <div class="form-horizontal">
                                                        <div class="col-lg-6 col-md-6 col-sm-6 cpl-xs-12" >
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;padding-left: 15px; ">
                                                                    Search :</label>
                                                                <div class="col-sm-7">
                                                                    <asp:DropDownList ID="ddl_MatchDTD" runat="server" class="form-control ">
                                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="Village Name"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="House No."></asp:ListItem>
                                                                        <asp:ListItem Value="3" Text="Child Name"></asp:ListItem>
                                                                        <asp:ListItem Value="4" Text="Father Name"></asp:ListItem>
                                                                        <asp:ListItem Value="5" Text="UniqueId"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-4 cpl-xs-12">
                                                            <div class="form-group">
                                                                <div class="col-sm-11">
                                                                    <asp:TextBox ID="Txt_VillageDTD" runat="server" class="form-control" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12">
                                                            <asp:ImageButton ID="IMG_DTDSerch" ToolTip="Serach" Enabled="false" runat="server"
                                                                class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1" OnClick="IMG_DTDSerch_Click"
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
                                                            <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                            <AlternatingRowStyle BackColor="#f1f1f1" />
                                                            <PagerStyle CssClass="paging" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="M" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkD2d" runat="server" onclick="CheckOne(this)" />
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

                                                                <asp:TemplateField HeaderText="School Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNerrewUniqueffId" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("SchoolName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UniqueId" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNewUniqueffId" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("UniqueId") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Session" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSession" class="labelGrid" ForeColor="Black" runat="server"
                                                                            Text='<%# Eval("Session") %>'></asp:Label>
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
                                                                <asp:TemplateField HeaderText="Father's Name" SortExpression="FathersName">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ddlEmployeeCode" class="labelGrid" ForeColor="Black" ItemStyle-ForeColor="#333"
                                                                            runat="server" Text='<%# Eval("FathersName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Sr.No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ddlEmpsde" class="labelGrid" ForeColor="Black" ItemStyle-ForeColor="#333"
                                                                            runat="server" Text='<%# Eval("Serial") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Age" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Txtunit" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Age") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Gender">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGender" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Class" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMauhallaw" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Social Category" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMauhalla" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("SocialCategory") %>'></asp:Label>
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
                                                <asp:Button ID="btnMove" runat="server" Visible="false" Width="20%" Text="Transfer"
                                                    CssClass="btn-danger btn-sm" OnClick="btnMatch_Click" />
                                                <asp:Button ID="btnSumbit" runat="server" Width="20%" OnClick="btnMatch_Click" CssClass="btn btn-success btn-sm" OnClientClick="javascript:return(confirm('Are you sure you want to submit!'))"
                                                    Text="Submit" /> 
                                                <asp:Button ID="btnSubmitToBO" runat="server" Width="20%" Style="margin-left: 5px;" OnClick="btnSubmitToBO_Click" CssClass="btn btn-success btn-sm" OnClientClick="javascript:return(confirm('Are you sure Not a duplicate record!'))"
                                                    Text="Not a duplicate" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

