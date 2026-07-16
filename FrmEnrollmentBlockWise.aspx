<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FrmEnrollmentBlockWise.aspx.cs" Culture="en-GB" Inherits="FrmEnrollmentBlockWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function SetCollaps() {
            setTimeout(function () {
                $('.clcss').hide();
            });

        }
        function togglediv(id) {
            $("#" + id).toggle();
            return false;
        }
    </script>
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
    </style>
    <script type="text/javascript">
        function shrinkandgrow(input) {
            var displayIcon = "img" + input;
            if ($("#" + displayIcon).attr("src") == "Images/grow.jpg") {
                $("#" + displayIcon).closest("tr")
                    .after("<tr><td></td><td colspan = '100%'>" + $("#" + input)
                        .html() + "</td></tr>");
                $("#" + displayIcon).attr("src", "Images/close-29.png");
            } else {
                $("#" + displayIcon).closest("tr").next().remove();
                $("#" + displayIcon).attr("src", "Images/grow.jpg");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="row">
                        <div class="row marg search-bg" runat="server" visible="false">
                            <div class="form-horizontal">
                                <%-- <asp:UpdatePanel runat="server" ID="UpMain" UpdateMode="Conditional">
        <ContentTemplate>--%>
                                <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group" style="margin-bottom: 7px;">
                                            <label for="email" class="col-sm-3 padd linhei">
                                            </label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlBlock" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_OnSelectedIndexChanged"
                                                    runat="server" class="form-control ">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3  col-sm-3 cpl-xs-12">
                                        <asp:Button ID="btnReport" Visible="false" OnClick="btnReport_Click" CssClass="btn btn-success pull-right"
                                            ToolTip="Save" Text="Report" Style="margin-right: 5px; padding: 0px;" runat="server" />
                                        <asp:ImageButton ID="btnSerach" ToolTip="Serach" Visible="false" runat="server" OnClick="btnSerach_Click"
                                            class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png"
                                            Style="margin-left: 5px; padding: 0px;" />
                                        <%-- <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                          <asp:Button ID="Button1"   CssClass="btn btn-success pull-right" 
                                 ToolTip="Save" Text="Report"  
                                Style="margin-right: 5px; padding: 0px;" runat="server" /></div>
                              
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server"  OnClick="btnSerach_Click"  class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1"
                                ImageUrl="~/images/search-29.png"  Style="margin-left: -49px; padding: 0px;"   />
                                </div>
                      <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                          <asp:Button ID="btnApprove"   CssClass="btn btn-success pull-right" 
                                 ToolTip="Save" Text="Approve"  Visible="false"    OnClick="btnApprove_Click" 
                                Style="margin-right: 5px; padding: 0px;" runat="server" /></div>
                                        --%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="panel panel-default" id="pnlMain" runat="server">
                                <div class="panel-heading " style="padding: 0px 0px 0px 15px;">
                                    <h3 class="text-danger" style="margin: 0px 0px 15px 0px;">
                                        <span style="position: relative; top: 7px;">Seal-Sign Validation Summary </span>
                                        <asp:Button ID="btnBack" CssClass="btn btn-success pull-right btn-sm " ToolTip="Save" Text="Back"
                                            OnClick="btnBack_Click" Style="margin-top: 5px; margin-right: 15px;" runat="server" />
                                    </h3>
                                </div>
                                <div id="ColMain">
                                    <div class="panel-body panel-body-new">
                                        <%--/<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">--%>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom: -18px; padding: 0px;">
                                            <div style="overflow: auto; width: 100%;" align="center">
                                                <div>
                                                    <asp:GridView ID="Gv_Profile_Search" runat="server" AutoGenerateColumns="false" DataKeyNames="BlockCode"
                                                        OnRowDataBound="GridView1_OnRowDataBound" CssClass="table table-striped table-bordered table-hover">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <a href="JavaScript:shrinkandgrow('div<%# Eval("BlockCode") %>');">
                                                                        <img alt="Details" id="imgdiv<%# Eval("BlockCode") %>" src="Images/grow.jpg" style="width: 10px; height: 10px;" />
                                                                    </a>
                                                                    <div id="div<%# Eval("BlockCode") %>" style="display: none;">
                                                                        <asp:GridView ID="GridView2" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                            AutoGenerateColumns="false" DataKeyNames="ClusterCode" HeaderStyle-BackColor="#FFA500"
                                                                            HeaderStyle-ForeColor="White">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Cluster Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblBlockCode" ForeColor="Black" Visible="false" runat="server" Text='<%# Eval("BlockCode") %>'>></asp:Label>
                                                                                        <asp:Label ID="lblClusterCode" Visible="false" ForeColor="Black" runat="server" Text='<%# Eval("ClusterCode") %>'>></asp:Label>
                                                                                        <asp:LinkButton ID="lblCategory" runat="server" OnClick="OOD2Dtargetmet_Click" Text='<%# Eval("ClusterName") %>'></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="150px" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField ItemStyle-Width="150px" DataField="ChildrenSealSign" HeaderText="Seal& Sign Pending for validation" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField ItemStyle-Width="150px" DataField="BlockName" HeaderText="BlockName" />
                                                            <asp:BoundField ItemStyle-Width="150px" DataField="ChildrenSealSign" HeaderText="Seal& Sign Pending for validation" />
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
