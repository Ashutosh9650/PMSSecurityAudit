<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="frmBulkInsertUser.aspx.cs" Inherits="frmBulkInsertUser" %>

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
                            <div class="panel-heading" style="padding: 5px 10px;">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                        <h3 class="text-danger" style="margin: 0px;">

                                            <asp:Label ID="lblMain" runat="server" Text="User Interface "></asp:Label>

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
                                        <div class="form-horizontal" style="padding-left: 10px;">

                                            <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-4 padd linhei" style="padding-top: 2px;">
                                                        Type:</label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlType" runat="server" class="form-control ">
                                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">New User </asp:ListItem>
                                                            <asp:ListItem Value="3">Promotion and Transfer </asp:ListItem>


                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-4 padd linhei" style="padding-top: 2px;">
                                                        District:</label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-4 padd linhei" style="padding-top: 2px;">
                                                        Designation:</label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlDesignation" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-1 col-md-1 col-sm-1 cpl-xs-12 col-lg-offset-0 col-md-offset-0 col-sm-offset-0 col-xs-offset-0">
                                                <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-right" ValidationGroup="saves"
                                                    BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />

                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="row table-responsive">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    </div>
                                    <asp:Label ID="Label1" Text="Total:   " runat="server"></asp:Label>
                                    <asp:Label ID="lblCount" ForeColor="Black" Font-Bold="true" runat="server"></asp:Label>
                                    <div style="height: 400px; overflow: auto; width: 99%;" align="center">


                                        <asp:GridView ID="GVVillage" runat="server" OnPageIndexChanging="gvD2d_PageIndexChanging" CssClass="table table-striped table-bordered table-hover"
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

                                                <asp:TemplateField HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="Chk_Location" CssClass="ChkChild" />

                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UserName" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbUserName" class="labelGrid" ForeColor="Black" runat="server"
                                                            Text='<%# Eval("EmployeeCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Designation" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbUffe" class="labelGrid" ForeColor="Black" runat="server"
                                                            Text='<%# Eval("Designation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="State">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblState" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                            runat="server" Text='<%#Eval("State") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="District ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDistrict" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                            runat="server" Text='<%#Eval("District") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Block ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblb" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                            runat="server" Text='<%#Eval("Block") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Name ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmployeeName" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                            runat="server" Text='<%#Eval("Employee Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date Joined ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmfffployeeName" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                            runat="server" Text='<%#Eval("DateJoined") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Password ">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblPass" ForeColor="Black" CssClass="form-control"
                                                            runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Role" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlRole" CssClass="form-control" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" runat="server" AutoPostBack="true" class="form-control"></asp:DropDownList>

                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="State" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlState" Enabled="false" CssClass="form-control" OnSelectedIndexChanged="ddlSate_SelectedIndexChanged" runat="server" AutoPostBack="true" class="form-control"></asp:DropDownList>

                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="District" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlDistrict" Enabled="false" CssClass="form-control" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" runat="server" AutoPostBack="true" class="form-control"></asp:DropDownList>

                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText=" Block" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlGBlockName" Enabled="false" CssClass="form-control" OnSelectedIndexChanged="ddlGBlockName_SelectedIndexChanged" runat="server" AutoPostBack="true" class="form-control"></asp:DropDownList>




                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cluster" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlCluster" CssClass="form-control" Enabled="false" runat="server" class="form-control"></asp:DropDownList>




                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="DISE Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPMSRole" class="VillageCode" ForeColor="Black" runat="server"
                                                            Text='<%# Eval("PMSRole") %>'></asp:Label>
                                                        <asp:Label ID="lblPMSState" class="VillageCode" ForeColor="Black" runat="server"
                                                            Text='<%# Eval("PMSState") %>'></asp:Label>
                                                        <asp:Label ID="lblPMSDist" class="VillageCode" ForeColor="Black" runat="server"
                                                            Text='<%# Eval("PMSDist") %>'></asp:Label>

                                                        <asp:Label ID="lblPMSBlock" class="VillageCode" ForeColor="Black" runat="server"
                                                            Text='<%# Eval("PMSBlock") %>'></asp:Label>


                                                        <asp:Label ID="lblEMail" class="VillageCode" ForeColor="Black" runat="server"
                                                            Text='<%# Eval("E-Mail") %>'></asp:Label>
                                                        <asp:Label ID="lblContactNo" class="VillageCode" ForeColor="Black" runat="server"
                                                            Text='<%# Eval("ContactNo") %>'></asp:Label>
                                                        <asp:Label ID="lblGender" class="VillageCode" ForeColor="Black" runat="server"
                                                            Text='<%# Eval("Gender") %>'></asp:Label>

                                                        <asp:Label ID="lblJoined" class="VillageCode" ForeColor="Black" runat="server"
                                                            Text='<%# Eval("Date Joined") %>'></asp:Label>

                                                        <asp:Label ID="lblLeaving" class="VillageCode" ForeColor="Black" runat="server"
                                                            Text='<%# Eval("Date of Leaving") %>'></asp:Label>


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
