<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="frmChangeMaster.aspx.cs" Inherits="ChangeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .HeaderClassCsss {
            text-align: center !important;
            font-weight: normal !important;
            background-color: #9A9C9A !important;
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
                            <div class="panel-heading" style="padding: 5px 10px">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                        <h3 class="text-danger" style="margin: 0px;">
                                            <asp:Label ID="lblMain" runat="server" Text="Master Change "></asp:Label>
                                        </h3>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">
                                        <input type="image" id="ton-new" class="butt" src="Images/search-not-29.png" title="Search" />
                                        <asp:ImageButton ID="btnDelete" Visible="false" CssClass="btn btn-info pull-right"
                                            ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px; padding: 0px;"
                                            runat="server" />
                                        <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                            ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnsave_Click" ValidationGroup="saves"
                                            Style="margin-right: 5px; padding: 0px;" runat="server" />
                                        <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-right" BackColor="#f5f5f5" Visible="false"
                                            ToolTip="Add" ImageUrl="~/images/add-29-1.png" Style="margin-right: 5px; padding: 0px;"
                                            runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div style="padding: 0px 10px 0px 10px;">
                                    <div class="row marg search-bg">
                                        <div class="form-horizontal">
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
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
                                            <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        District:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                            AutoPostBack="true" class="form-control " />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Block:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                            class="form-control " />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-4 padd linhei" style="padding-top: 2px;">
                                                        Village:</label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlVillage" runat="server" class="form-control " />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-4 padd linhei" style="padding-top: 2px;">
                                                        Type:</label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlType" runat="server" class="form-control ">
                                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                            <%--<asp:ListItem Value="1">Block </asp:ListItem>--%>
                                                            <%--<asp:ListItem Value="2">Cluster </asp:ListItem>--%>
                                                            <asp:ListItem Value="3">Panchayat</asp:ListItem>
                                                            <%-- <asp:ListItem Value="4">Village</asp:ListItem>
                                                                  <asp:ListItem Value="5">School</asp:ListItem>
                                                                    <asp:ListItem Value="6">School Marge</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" runat="server" id="lblShool" visible="false" class="col-sm-4 padd linhei" style="padding-top: 2px;">
                                                        School:</label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlSchool" runat="server" Visible="false" class="form-control " />

                                                    </div>
                                                </div>
                                                <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" Style="margin-top: -13px;" class="btn btn-danger btn-paddd pull-right" ValidationGroup="saves"
                                                    BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />

                                            </div>



                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body" style="padding: 5px 10px 0px 10px;">
                                <div class="row table-responsive">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                        <div style="height: 400px; overflow: auto; width: 100%;" align="center">
                                            <asp:GridView ID="GVBlock" runat="server" Visible="false"
                                                AllowPaging="true" PageSize="300" CssClass="table table-striped table-bordered table-hover" OnRowDataBound="GVBlock_RowDataBound" AutoGenerateColumns="False" Font-Names="Arial"
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

                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="15%" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImgAcc" runat="server" ImageUrl="~/images/delete-29.png" OnClick="btn_PhyDelete_Click"
                                                                Width="15px" Height="15px"></asp:ImageButton>

                                                        </ItemTemplate>
                                                        <HeaderStyle Width="5%" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="District Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDistrictName" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("DistrictName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="District Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDistri55ctNaf1" Visible="false" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                runat="server" Text='<%#Eval("DistrictCode") %>'></asp:Label>


                                                            <asp:Label ID="Label1" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                runat="server" Text='<%#Eval("EGDistrictCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EG Block Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblBlockName" ForeColor="Black" MaxLength="30" class="form-control"
                                                                runat="server" Text='<%#Eval("BlockName") %>'></asp:TextBox>

                                                            <asp:DropDownList ID="ddlGBlockName" runat="server" class="form-control"></asp:DropDownList>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EG Block Code">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblBlockCode" ForeColor="Black" class="form-control" MaxLength="8"
                                                                runat="server" Text='<%#Eval("BlockCode") %>'></asp:TextBox>

                                                            <asp:Label ID="EGBlock" Visible="false" class="labelGrid" ForeColor="Black" MaxLength="8"
                                                                runat="server" Text='<%#Eval("EGBlockCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Main Block Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlMainBlockName" runat="server" class="form-control"></asp:DropDownList>
                                                            <asp:TextBox ID="lblMainMainBlockCode" Visible="false" ForeColor="Black" class="form-control" MaxLength="30"
                                                                runat="server" Text='<%#Eval("MainBlockCode") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Block Code">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblMainCode" ForeColor="Black" class="form-control" MaxLength="8"
                                                                runat="server" Text='<%#Eval("MainBlockCode") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Cluster Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblClusterName" ForeColor="Black" class="form-control" MaxLength="30"
                                                                runat="server" Text='<%#Eval("ClusterName") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cluster Code">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblClusterCode" ForeColor="Black" class="form-control" MaxLength="10"
                                                                runat="server" Text='<%#Eval("ClusterCode") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Panchayat Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblPanchayatName" ForeColor="Black" Enabled="false" class="form-control" MaxLength="30"
                                                                runat="server" Text='<%#Eval("PanchayatName") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Panchayat Code">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblPanchayatCod11e" Visible="false" ForeColor="Black" class="form-control" MaxLength="10"
                                                                runat="server" Text='<%#Eval("EGPanchayatCode") %>'></asp:TextBox>

                                                            <asp:TextBox ID="lblPanchayatCode" Enabled="false" ForeColor="Black" class="form-control" MaxLength="10"
                                                                runat="server" Text='<%#Eval("EGPanchayatCode") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniqueCode" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("PanchayatCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniquePanchayatCode" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("EGPanchayatCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniquePanchayatName" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("PanchayatName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniqueClusterCode" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("ClusterCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniqueClusterName" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("ClusterName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                            <asp:GridView ID="GVVillage" runat="server" Visible="false" CssClass="table table-striped table-bordered table-hover"
                                                OnRowDataBound="GVGVVillage_RowDataBound" AutoGenerateColumns="False" Font-Names="Arial"
                                                AllowPaging="true" PageSize="300" Font-Size="12px" Width="100%">
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

                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="15%" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImgAcc" runat="server" OnClick="btn_Delete_Click" ImageUrl="~/images/delete-29.png"
                                                                Width="15px" Height="15px"></asp:ImageButton>

                                                        </ItemTemplate>
                                                        <HeaderStyle Width="5%" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
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
                                                    <asp:TemplateField HeaderText="EG Block Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlGBlockName" OnSelectedIndexChanged="ddlGBlockName_SelectedIndexChanged" runat="server" AutoPostBack="true" class="form-control"></asp:DropDownList>




                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EG Block Code">
                                                        <ItemTemplate>

                                                            <asp:Label ID="EGBlock" class="labelGrid" ForeColor="Black" MaxLength="8"
                                                                runat="server" Text='<%#Eval("BlockCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>






                                                    <asp:TemplateField HeaderText="Cluster Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClusterCode" class="labelGrid" ForeColor="Black"
                                                                runat="server" Text='<%#Eval("ClusterCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Panchayat Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlPanchayat" ForeColor="Black" runat="server" class="form-control"></asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Panchayat Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPanchayatCode" ForeColor="Black" class="form-control"
                                                                runat="server" Text='<%#Eval("EGPanchayatCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Village Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblVillageName" ForeColor="Black" class="form-control" MaxLength="50"
                                                                runat="server" Text='<%#Eval("VillageName") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Village Code">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblVillageCode" ForeColor="Black" class="form-control" MaxLength="12"
                                                                runat="server" Text='<%#Eval("VillageCode") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniqueVillageName" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("VillageName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniqueVillageCode" class="VillageCode" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("VillageCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniqueCode" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("UniqueCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>


                                            <asp:GridView ID="GvSchool" runat="server" Visible="false" CssClass="table table-striped table-bordered table-hover"
                                                OnRowDataBound="GvSchool_RowDataBound" AutoGenerateColumns="False" Font-Names="Arial"
                                                AllowPaging="true" PageSize="300" Font-Size="12px" Width="100%">
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

                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="15%" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImgAcc" runat="server" OnClick="btn_School_Click" ImageUrl="~/images/delete-29.png"
                                                                Width="15px" Height="15px"></asp:ImageButton>

                                                        </ItemTemplate>
                                                        <HeaderStyle Width="5%" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
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
                                                    <asp:TemplateField HeaderText="EG Block Name" Visible="true">
                                                        <ItemTemplate>

                                                            <asp:Label ID="EGBlockName" class="labelGrid" ForeColor="Black"
                                                                runat="server" Text='<%#Eval("BlockName") %>'></asp:Label>


                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EG Block Code">
                                                        <ItemTemplate>

                                                            <asp:Label ID="EGBlock" class="labelGrid" ForeColor="Black" MaxLength="8"
                                                                runat="server" Text='<%#Eval("BlockCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>





                                                    <asp:TemplateField HeaderText="Cluster Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClname" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                runat="server" Text='<%#Eval("ClusterName") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cluster Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClusterCode" class="labelGrid" ForeColor="Black"
                                                                runat="server" Text='<%#Eval("ClusterCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Panchayat Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPanddayatName" ForeColor="Black" class="form-control"
                                                                runat="server" Text='<%#Eval("PanchayatName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Panchayat Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPanchayatCode" ForeColor="Black" class="form-control"
                                                                runat="server" Text='<%#Eval("EGPanchayatCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Village Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlVillageName" Font-Size="Small" runat="server" class="form-control"></asp:DropDownList>


                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Village Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVillageCode" ForeColor="Black" class="form-control" MaxLength="12"
                                                                runat="server" Text='<%#Eval("VillageCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Dise Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDiseCode" ForeColor="Black" MaxLength="12"
                                                                runat="server" Text='<%#Eval("DISECode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SchoolName">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblSchoolName" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333" class="form-control" MaxLength="100"
                                                                runat="server" Text='<%#Eval("Name") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniqueVillageName" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("VillageName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniqueDISECode" class="VillageCode" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("DISECode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniqueCode" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("UniqueCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniqueName" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>


                                            <asp:GridView ID="gvSchoolMarge" runat="server" Visible="false"
                                                OnRowDataBound="GvSchoolMarge_RowDataBound" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" Font-Names="Arial"
                                                AllowPaging="true" PageSize="300" Font-Size="12px" Width="100%">
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
                                                    <asp:TemplateField HeaderText="EG Block Name" Visible="true">
                                                        <ItemTemplate>

                                                            <asp:Label ID="EGBlockName" class="labelGrid" ForeColor="Black"
                                                                runat="server" Text='<%#Eval("BlockName") %>'></asp:Label>


                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EG Block Code">
                                                        <ItemTemplate>

                                                            <asp:Label ID="EGBlock" class="labelGrid" ForeColor="Black" MaxLength="8"
                                                                runat="server" Text='<%#Eval("BlockCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>





                                                    <asp:TemplateField HeaderText="Cluster Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClname" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                runat="server" Text='<%#Eval("ClusterName") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cluster Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClusterCode" class="labelGrid" ForeColor="Black"
                                                                runat="server" Text='<%#Eval("ClusterCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Panchayat Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPanddayatName" ForeColor="Black" class="form-control"
                                                                runat="server" Text='<%#Eval("PanchayatName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Panchayat Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPanchayatCode" ForeColor="Black" class="form-control"
                                                                runat="server" Text='<%#Eval("EGPanchayatCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Village Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlVillageName" Font-Size="Small" runat="server" class="form-control"></asp:DropDownList>


                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Village Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVillageCode" ForeColor="Black" class="form-control" MaxLength="12"
                                                                runat="server" Text='<%#Eval("VillageCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Dise Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDiseCode" ForeColor="Black" MaxLength="12"
                                                                runat="server" Text='<%#Eval("DISECode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SchoolName">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSchoolName" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333" MaxLength="100"
                                                                runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Marge Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlMargeName" Font-Size="Small" runat="server" class="form-control"></asp:DropDownList>


                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniqueVillageName" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("VillageName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniqueDISECode" class="VillageCode" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("DISECode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniqueCode" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("UniqueCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniqueName" class="labelGrid" ForeColor="Black" runat="server"
                                                                Text='<%# Eval("Name") %>'></asp:Label>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSerach" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
