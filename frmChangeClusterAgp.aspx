<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="frmChangeClusterAgp.aspx.cs" Inherits="frmChangeClusterAgp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .HeaderClassCsss {
            text-align: center !important;
            font-weight: normal !important;
            background-color: #9A9C9A !important;
        }

        .btnmargin {
            margin-left: 10px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>

            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="panel panel-default" style="height: 530px;">
                            <div class="panel-heading" style="padding: 5px 15px 5px 5px;">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                        <h3 class="text-danger" style="margin: 0px;">
                                            <asp:Label ID="lblMain" runat="server" Text="AGP Master Update Module"></asp:Label>
                                        </h3>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">

                                        <asp:ImageButton ID="btnDelete" Visible="false" CssClass="btn btn-info pull-right"
                                            ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px; padding: 0px;"
                                            runat="server" />
                                        <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right btn-sm" BackColor="#f5f5f5"
                                            ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnsave_Click" ValidationGroup="saves"
                                            Style="margin-right: 5px; padding: 0px;" runat="server" />
                                        <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-right btn-sm" BackColor="#f5f5f5" Visible="false"
                                            ToolTip="Add" ImageUrl="~/images/add-29-1.png" Style="margin-right: 5px; padding: 0px;"
                                            runat="server" />


                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div style="padding: 0px 10px;">
                                    <div class="row marg search-bg">
                                        <div class="form-horizontal">
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                <div class="form-group" style="margin-bottom: 7px;">
                                                    <label for="email" class="col-sm-3 padd linhei">
                                                        Year:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" class="form-control ">
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
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
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        District:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                            AutoPostBack="true" class="form-control " />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Block:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                            class="form-control " />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Type:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlType" runat="server" class="form-control ">
                                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="4">Block Mapping  </asp:ListItem>
                                                            <asp:ListItem Value="1">Village </asp:ListItem>
                                                            <asp:ListItem Value="3">Unassigned Cluster  </asp:ListItem>
                                                            <asp:ListItem Value="2">School </asp:ListItem>

                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>
                                            <%-- <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" runat="server" id="lblShool" visible="false" class="col-sm-4 padd linhei" style="padding-top: 2px;">
                                                        School:</label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlSchool" runat="server" Visible="false" class="form-control " />
                                                          
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <div class="col-lg-9 col-md-1 col-sm-1 cpl-xs-12 ">
                                                <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-left" ValidationGroup="saves"
                                                    BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />


                                                <asp:Button ID="LinkButton1" CssClass="btn-success btn-sm pull-left btnmargin" OnClick="btnAddCluster" Text="Create AGP Cluster" runat="server"></asp:Button>

                                                <asp:Button ID="LinkButton2" CssClass="btn-success btn-sm pull-left btnmargin" OnClick="btnDeleteCluster" Text="Delete AGP Cluster" runat="server"></asp:Button>

                                                <asp:Button ID="LinkButton3" CssClass="btn-success btn-sm pull-left btnmargin" OnClick="btnAddBlock" Text="Create AGP Block" runat="server"></asp:Button>

                                                <asp:Button ID="Button1" CssClass="btn-success btn-sm pull-left btnmargin" OnClick="btnDeleteBlock" Text="Delete AGP Block" runat="server"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <div class="row table-responsive">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <div class="row">
                                        <div class="row marg search-bg">
                                            <div class="form-horizontal">
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Panchayat :</label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged" class="form-control " />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Village :</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlVillage" runat="server" AutoPostBack="true" class="form-control " OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="row table-responsive">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div style="height: 290px; overflow: auto; width: 109%;" align="center">
                                            <asp:GridView ID="GVCluster" runat="server" Visible="false" OnPageIndexChanging="GV_Cluster_PageIndexChanging"
                                                AllowPaging="true" PageSize="100" OnRowDataBound="GV_luster_OnRowDataBound" AutoGenerateColumns="False" Font-Names="Arial"
                                                Font-Size="12px" Width="100%">
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
                                                            <asp:Label ID="lblDistri55ctNaf1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                runat="server" Text='<%#Eval("DistrictCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Admin Block Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDistri55ctName" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("MainBlockName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Block Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBlockName" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("BlockName") %>'></asp:Label>

                                                            <asp:DropDownList ID="ddlBlockName" class="form-control " runat="server"></asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ClusterName" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPanchffghame" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("ClusterName") %>'></asp:Label>
                                                        </ItemTemplate>
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
                                                            <asp:Label ID="lblVillageN9ame" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("VillageName") %>'></asp:Label>
                                                            <asp:DropDownList ID="ddlVillageName" class="form-control " runat="server"></asp:DropDownList>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Village Code" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVillageCode" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("VillageCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SchoolName" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lUniqueId" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("SchoolName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DISE Code" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDISECode" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("DISECode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Working Status" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlWorkingStatus" OnSelectedIndexChanged="ddlWorkingStatus_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control ">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Operational </asp:ListItem>
                                                                <asp:ListItem Value="2">Non-Operational </asp:ListItem>
                                                                <asp:ListItem Value="3">Close </asp:ListItem>
                                                                <asp:ListItem Value="4">Marge</asp:ListItem>

                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="School Level" Visible="true">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlManagement" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Primary </asp:ListItem>
                                                                <asp:ListItem Value="2">Upper Primary </asp:ListItem>
                                                                <asp:ListItem Value="3">Secondary</asp:ListItem>
                                                                <asp:ListItem Value="4">Senior Secondary</asp:ListItem>

                                                                <asp:ListItem Value="6">Madarsa</asp:ListItem>
                                                                <asp:ListItem Value="7">Maa Badi</asp:ListItem>
                                                                <asp:ListItem Value="9">ANGANWARI</asp:ListItem>
                                                                <asp:ListItem Value="10">KGBV with school</asp:ListItem>
                                                                <asp:ListItem Value="11">KGBV without school</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cluster Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlClusterCode" runat="server" class="form-control ">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Village Operational Status" Visible="true">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlVillageOperational" OnSelectedIndexChanged="ddlVillageOperational_SelectedIndexChanged" runat="server" AutoPostBack="true" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Operational Village </asp:ListItem>
                                                                <asp:ListItem Value="2">Non-Operational Village </asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Village Geography" Visible="true">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlVillageGeography" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Rural</asp:ListItem>
                                                                <asp:ListItem Value="2">Urban</asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="School Type" Visible="true">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlSchoolType" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Formal </asp:ListItem>
                                                                <asp:ListItem Value="2">Informal</asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="GKP School" Visible="true">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlGKP" runat="server" OnSelectedIndexChanged="ddlGKP_SelectedIndexChanged" AutoPostBack="true" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">GKP School </asp:ListItem>
                                                                <asp:ListItem Value="2">Non GKP School</asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="GKP School Level" Visible="true">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlGKPLevel" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">L0 and L1</asp:ListItem>
                                                                <asp:ListItem Value="2">L1 and L2</asp:ListItem>
                                                                <asp:ListItem Value="3">L2 and L3</asp:ListItem>

                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Balsabha" Visible="true">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlBalsabha" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Balsabha school </asp:ListItem>
                                                                <asp:ListItem Value="2">Non Balsabha school</asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CBL Village" Visible="true">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlCblVillage" OnSelectedIndexChanged="ddlCblVillage_SelectedIndexChanged" runat="server" AutoPostBack="true" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">CBL Village</asp:ListItem>
                                                                <asp:ListItem Value="2">Non CBL Village</asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Village Functional Status" Visible="true">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlFunctionalStatus" OnSelectedIndexChanged="ddlFunctionalStatus_SelectedIndexChanged" runat="server" AutoPostBack="true" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Functional Village </asp:ListItem>
                                                                <asp:ListItem Value="2">Non Functional Village </asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="AGP Village flag" Visible="false">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlAGP" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">AGP Village</asp:ListItem>
                                                                <asp:ListItem Value="2">Non AGP Village</asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="School Campus" Visible="true">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlSchoolCampus" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">NA</asp:ListItem>
                                                                <asp:ListItem Value="2">Same Campus PS and UPS </asp:ListItem>
                                                                <asp:ListItem Value="3">Same Campus PS and Senior Secondary</asp:ListItem>
                                                                <asp:ListItem Value="4">Same Campus PS and Secondary</asp:ListItem>
                                                                <asp:ListItem Value="5">Same Campus UPS and Senior Secondary</asp:ListItem>

                                                                <asp:ListItem Value="6">Same Campus UPS and Secondary</asp:ListItem>
                                                                <asp:ListItem Value="7">Same Campus with PS and UPS and Secondary</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWorkingStatus" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("WorkingStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblManagement" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("Management") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClusterCode" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("ClusterCode") %>'></asp:Label>
                                                            <asp:Label ID="lblTempClusterCode" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("TempClusterCode") %>'></asp:Label>
                                                            <asp:Label ID="lblTempVillageCode" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("TempVillageCode") %>'></asp:Label>

                                                            <asp:Label ID="lblVillageGeography" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("VillageGeography") %>'></asp:Label>
                                                            <asp:Label ID="lblVillageGeographyOperational" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("VillageGeographyOperational") %>'></asp:Label>

                                                            <asp:Label ID="lblGKP" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("GKP") %>'></asp:Label>
                                                            <asp:Label ID="lblGKPLevel" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("GKPLevel") %>'></asp:Label>
                                                            <asp:Label ID="lblSchoolType" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("SchoolType") %>'></asp:Label>
                                                            <asp:Label ID="lblBAlVal" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("BAlVal") %>'></asp:Label>


                                                            <asp:Label ID="lblCBlVillage" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("CBlVillage") %>'></asp:Label>

                                                            <asp:Label ID="lblFunctionalStatus" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("FunctionalStatus") %>'></asp:Label>

                                                            <asp:Label ID="lblAGPStatus" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("AGPStatus") %>'></asp:Label>
                                                            <asp:Label ID="lblSchoolCampus" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("SchoolCampus") %>'></asp:Label>


                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Block Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBlockCode" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                runat="server" Text='<%#Eval("BlockCode") %>'></asp:Label>

                                                            <asp:Label ID="lblTempBlockCode" Visible="false" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                runat="server" Text='<%#Eval("TempBlockCode") %>'></asp:Label>
                                                            <asp:Label ID="Label1" Visible="false" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                runat="server" Text='<%#Eval("TempBlockCode") %>'></asp:Label>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                            <asp:GridView ID="GVCluster1" runat="server" Visible="false" OnPageIndexChanging="GV_Cluster_PageIndexChanging"
                                                AllowPaging="true" PageSize="100" OnRowDataBound="GV_luster1_OnRowDataBound" AutoGenerateColumns="False" Font-Names="Arial"
                                                Font-Size="12px" Width="100%">
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
                                                            <asp:Label ID="lblDistri55ctNaf1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                runat="server" Text='<%#Eval("DistrictCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Admin Block Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDistri55ctName" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("MainBlockName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Block Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBlockCode" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                runat="server" Text='<%#Eval("BlockCode") %>'></asp:Label>

                                                            <asp:Label ID="lblTempBlockCode" Visible="false" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                runat="server" Text='<%#Eval("TempBlockCode") %>'></asp:Label>
                                                            <asp:Label ID="Label1" Visible="false" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                runat="server" Text='<%#Eval("TempBlockCode") %>'></asp:Label>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ClusterName" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPanchffghame" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("ClusterName") %>'></asp:Label>
                                                        </ItemTemplate>
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
                                                    <asp:TemplateField HeaderText="AGP Block Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBlockName" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("BlockName") %>'></asp:Label>

                                                            <asp:DropDownList ID="ddlBlockName" class="form-control " runat="server"></asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Village Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVillageN9ame" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("VillageName") %>'></asp:Label>
                                                            <asp:DropDownList ID="ddlVillageName" class="form-control " runat="server"></asp:DropDownList>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Village Code" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVillageCode" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("VillageCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SchoolName" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lUniqueId" class="labelGrid" ForeColor="Black" runat="server" Text='<%# Eval("SchoolName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDISECode" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("DISECode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Working Status" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlWorkingStatus" OnSelectedIndexChanged="ddlWorkingStatus_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control ">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Operational </asp:ListItem>
                                                                <asp:ListItem Value="2">Non-Operational </asp:ListItem>
                                                                <asp:ListItem Value="3">Close </asp:ListItem>
                                                                <asp:ListItem Value="4">Marge</asp:ListItem>

                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="School Level" Visible="false">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlManagement" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Primary </asp:ListItem>
                                                                <asp:ListItem Value="2">Upper Primary </asp:ListItem>
                                                                <asp:ListItem Value="3">Secondary</asp:ListItem>
                                                                <asp:ListItem Value="4">Senior Secondary</asp:ListItem>

                                                                <asp:ListItem Value="6">Madarsa</asp:ListItem>
                                                                <asp:ListItem Value="7">Maa Badi</asp:ListItem>
                                                                <asp:ListItem Value="9">ANGANWARI</asp:ListItem>
                                                                <asp:ListItem Value="10">KGBV with school</asp:ListItem>
                                                                <asp:ListItem Value="11">KGBV without school</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cluster Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlClusterCode" runat="server" class="form-control ">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Village Operational Status" Visible="false">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlVillageOperational" OnSelectedIndexChanged="ddlVillageOperational_SelectedIndexChanged" runat="server" AutoPostBack="true" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Operational Village </asp:ListItem>
                                                                <asp:ListItem Value="2">Non-Operational Village </asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Village Geography" Visible="false">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlVillageGeography" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Rural</asp:ListItem>
                                                                <asp:ListItem Value="2">Urban</asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="School Type" Visible="false">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlSchoolType" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Formal </asp:ListItem>
                                                                <asp:ListItem Value="2">Informal</asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="GKP School" Visible="false">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlGKP" runat="server" OnSelectedIndexChanged="ddlGKP_SelectedIndexChanged" AutoPostBack="true" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">GKP School </asp:ListItem>
                                                                <asp:ListItem Value="2">Non GKP School</asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="GKP School Level" Visible="false">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlGKPLevel" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">L0 and L1</asp:ListItem>
                                                                <asp:ListItem Value="2">L1 and L2</asp:ListItem>
                                                                <asp:ListItem Value="3">L2 and L3</asp:ListItem>

                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Balsabha" Visible="false">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlBalsabha" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Balsabha school </asp:ListItem>
                                                                <asp:ListItem Value="2">Non Balsabha school</asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CBL Village" Visible="false">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlCblVillage" OnSelectedIndexChanged="ddlCblVillage_SelectedIndexChanged" runat="server" AutoPostBack="true" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">CBL Village</asp:ListItem>
                                                                <asp:ListItem Value="2">Non CBL Village</asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Village Functional Status" Visible="false">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlFunctionalStatus" OnSelectedIndexChanged="ddlFunctionalStatus_SelectedIndexChanged" runat="server" AutoPostBack="true" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Functional Village </asp:ListItem>
                                                                <asp:ListItem Value="2">Non Functional Village </asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="AGP Village flag" Visible="false">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlAGP" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">AGP Village</asp:ListItem>
                                                                <asp:ListItem Value="2">Non AGP Village</asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="School Campus" Visible="false">
                                                        <ItemTemplate>


                                                            <asp:DropDownList ID="ddlSchoolCampus" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Same Campus PS and UPS </asp:ListItem>
                                                                <asp:ListItem Value="2">Same Campus PS and Senior Secondary</asp:ListItem>
                                                                <asp:ListItem Value="3">Same Campus PS and Secondary</asp:ListItem>
                                                                <asp:ListItem Value="4">Same Campus UPS and Senior Secondary</asp:ListItem>

                                                                <asp:ListItem Value="5">Same Campus UPS and Secondary</asp:ListItem>
                                                                <asp:ListItem Value="6">Same Campus with PS and UPS and Secondary</asp:ListItem>

                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWorkingStatus" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("WorkingStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblManagement" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("Management") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClusterCode" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("ClusterCode") %>'></asp:Label>
                                                            <asp:Label ID="lblTempClusterCode" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("TempClusterCode") %>'></asp:Label>
                                                            <asp:Label ID="lblTempVillageCode" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("TempVillageCode") %>'></asp:Label>

                                                            <asp:Label ID="lblVillageGeography" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("VillageGeography") %>'></asp:Label>
                                                            <asp:Label ID="lblVillageGeographyOperational" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("VillageGeographyOperational") %>'></asp:Label>

                                                            <asp:Label ID="lblGKP" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("GKP") %>'></asp:Label>
                                                            <asp:Label ID="lblGKPLevel" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("GKPLevel") %>'></asp:Label>
                                                            <asp:Label ID="lblSchoolType" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("SchoolType") %>'></asp:Label>
                                                            <asp:Label ID="lblBAlVal" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("BAlVal") %>'></asp:Label>


                                                            <asp:Label ID="lblCBlVillage" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("CBlVillage") %>'></asp:Label>

                                                            <asp:Label ID="lblFunctionalStatus" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("FunctionalStatus") %>'></asp:Label>

                                                            <asp:Label ID="lblAGPStatus" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("AGPStatus") %>'></asp:Label>
                                                            <asp:Label ID="lblSchoolCampus" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("SchoolCampus") %>'></asp:Label>


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
                </div>
            </div>


            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Hdn_model4"
                PopupControlID="pnlpopup4" CancelControlID="CancelButton" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:HiddenField ID="Hdn_model4" runat="server" />
            <asp:Panel ID="pnlpopup4" runat="server" Style="display: none;">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-header" style="height: 45px;">
                            <asp:ImageButton ID="CancelButton" ImageUrl="~/images/close-29.png" runat="server" CssClass="btn bgm-cyan pull-right" Text="Close"
                                ToolTip="Close" Style="margin-right: 5px; padding: 0px;"></asp:ImageButton>
                            <asp:ImageButton ID="ImageButton9" CssClass="btn btn-info pull-right" OnClick="btnSaveClick_Click" BackColor="#f5f5f5"
                                ToolTip="Save" ImageUrl="~/images/save-29-1.png" Style="margin-right: 5px; padding: 0px;"
                                runat="server" />

                            <h4 class="modal-title">Village</h4>

                        </div>

                        <div class="row">

                            <div class="row marg search-bg" style="margin-bottom: 15px;">

                                <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                                    <div class="col-lg-8 col-md-8  col-sm-8 cpl-xs-12">
                                        <div class="form-group" style="margin-bottom: 2px;">
                                            <label for="email" class="col-sm-3 padd linhei">
                                                Village:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlCLusterVillage" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>


                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>


            <cc1:ModalPopupExtender ID="ModalPopupExtender6" runat="server" TargetControlID="Hdn_model664"
                PopupControlID="pnlpopup434" CancelControlID="CancelButton" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:HiddenField ID="Hdn_model664" runat="server" />
            <asp:Panel ID="pnlpopup434" runat="server" Style="display: none;">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-header" style="height: 45px;">
                            <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/close-29.png" runat="server" CssClass="btn bgm-cyan pull-right" Text="Close"
                                ToolTip="Close" Style="margin-right: 5px; padding: 0px;"></asp:ImageButton>
                            <asp:ImageButton ID="ImageButton5" CssClass="btn btn-info pull-right" OnClick="btnSavedd_Click" BackColor="#f5f5f5"
                                ToolTip="Save" ImageUrl="~/images/save-29-1.png" Style="margin-right: 5px; padding: 0px;"
                                runat="server" />

                            <h4 class="modal-title">Delete AGP Block</h4>

                        </div>

                        <div class="row">

                            <div class="row marg search-bg" style="margin-bottom: 15px;">

                                <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                                    <div class="col-lg-8 col-md-8  col-sm-8 cpl-xs-12">
                                        <div class="form-group" style="margin-bottom: 2px;">
                                            <label for="email" class="col-sm-3 padd linhei">
                                                AGP Block Name</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlDeleteBlock" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>


                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Hdn_model41"
                PopupControlID="pnlpopup55" CancelControlID="CancelButton" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:HiddenField ID="Hdn_model41" runat="server" />
            <asp:Panel ID="pnlpopup55" runat="server" Style="display: none;">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-header" style="height: 45px;">
                            <asp:ImageButton ID="ImageButton2" ImageUrl="~/images/close-29.png" runat="server" CssClass="btn bgm-cyan pull-right" Text="Close"
                                ToolTip="Close" Style="margin-right: 5px; padding: 0px;"></asp:ImageButton>
                            <asp:ImageButton ID="ImageButton3" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnAddBlock_Click" Style="margin-right: 5px; padding: 0px;"
                                runat="server" />

                            <h4 class="modal-title">AGP Block</h4>

                        </div>

                        <div class="row">

                            <div class="row marg search-bg" style="margin-bottom: 15px;">

                                <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">

                                    <div class="form-group" style="margin-bottom: 2px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            Admin Block</label>
                                        <div class="col-sm-9 padd">
                                            <asp:DropDownList ID="ddlAdminBlock" runat="server" OnSelectedIndexChanged="ddlAdminBlock_SelectedIndexChanged" AutoPostBack="true" class="form-control">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblSerial" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group" style="margin-bottom: 2px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            AGP Block Name</label>
                                        <div class="col-sm-9 padd">
                                            <asp:TextBox ID="txtBlockName" MaxLength="25" runat="server" class="form-control">
                                        
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group" style="margin-bottom: 2px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            AGP Block Code</label>
                                        <div class="col-sm-9 padd">
                                            <asp:TextBox ID="txtBlockCOde" MaxLength="7" Enabled="false" runat="server" class="form-control">
                                        
                                            </asp:TextBox>
                                        </div>
                                    </div>


                                </div>

                            </div>


                        </div>
                    </div>
                </div>
            </asp:Panel>


            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="HiddenField2"
                PopupControlID="pnlpopup5" CancelControlID="CancelButton" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:HiddenField ID="HiddenField2" runat="server" />
            <asp:Panel ID="pnlpopup5" runat="server" Style="display: none;">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-header" style="height: 0px;">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/close-29.png" runat="server" CssClass="btn bgm-cyan pull-right" Text="Close"
                                ToolTip="Close" Style="margin-right: 5px; padding: 0px;"></asp:ImageButton>
                            <asp:ImageButton ID="ImageButton7" CssClass="btn btn-info pull-right" OnClick="btnDeleteClick_Click" BackColor="#f5f5f5"
                                ToolTip="Save" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px; padding: 0px;"
                                runat="server" />

                            <h4 class="modal-title">Cluster</h4>

                        </div>

                        <div class="row">

                            <div class="row marg search-bg">

                                <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                                    <div class="col-lg-8 col-md-8  col-sm-8 cpl-xs-12">
                                        <div class="form-group" style="margin-bottom: 2px;">
                                            <label for="email" class="col-sm-3 padd linhei">
                                                Cluster:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlDeleteCluster" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>


                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
