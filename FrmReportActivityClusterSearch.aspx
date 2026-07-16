<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"  Culture="en-GB"
    CodeFile="FrmReportActivityClusterSearch.aspx.cs" Inherits="FrmReportActivityClusterSearch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

<style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
        .modalpopupcss
        {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        .modalPopup
        {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
            z-index: 999999;
        }
    </style>
      <script type="text/javascript">
          debugger;
          function calculate_totals(txtcls, txttotalcls) {
              var TotalCamt = 0;
              $("." + txtcls).each(function (index, value) {
                  if ($.trim($(this).val()) != "")
                      if (!isNaN($(this).val()))
                          TotalCamt = TotalCamt + parseFloat($(this).val());
              });
              $("." + txttotalcls).val(TotalCamt);
              return false;
          }
    </script>
    <script type="text/javascript">
        function arrivaldatecheck(sender, args) {
            var depdate = 'dep';

            var departuredate = $('.' + depdate).val();
            var arrivaldate = sender._selectedDate;
            var today = new Date();




            if (sender._selectedDate > today) {
                alert("Should not be future date.");
                sender._textbox.set_Value("")

                return false;

            }

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<%--<asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>--%>
    <div class="row" >
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="text-danger" style="margin: 0px;">
                                          School Activity <span class="pull-right" style="font-size: 17px;">
                                                <asp:LinkButton ID="btnexcel" Visible="false" runat="server" Text="Export to Excel" ></asp:LinkButton>
                                                
                                             </span>
                                        </h3>
                </div>
            </div>
            <div class="row">
                <div class="row marg search-bg">
                    <div class="form-horizontal">
                        <%-- <asp:UpdatePanel runat="server" ID="UpMain" UpdateMode="Conditional">
        <ContentTemplate>--%>
                        <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                   <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                            <div class="form-group" style="margin-bottom: 7px;">
                                <label for="email" class="col-sm-3 padd linhei">
                                    Block:</label>
                                <div class="col-sm-9 padd">
                                    <asp:DropDownList ID="ddlBlock"  
                                        runat="server" class="form-control ">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                            <div class="form-group" style="margin-bottom: 7px;">
                                <label for="email" class="col-sm-3 padd linhei">
                                   Year:</label>
                                <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlYear" runat="server"  class="form-control ">
                                                                     </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                            <div class="form-group" style="margin-bottom: 7px;">
                                <label for="email" class="col-sm-3 padd linhei">
                                    Month:</label>
                                <div class="col-sm-9 padd">
                                     <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
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

                        
                    </div>
                        <div class="col-lg-2 col-md-2  col-sm-2 cpl-xs-12">
                      
                         <asp:Button ID="btnBack" CssClass="btn btn-success pull-right " Text="Back"
                                     OnClick="btnBack_Click" Style="margin-right: 5px; padding: 0px;"
                                    runat="server" />
                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server"  OnClick="btnSerach_Click"  class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1"
                                ImageUrl="~/images/search-29.png"  Style="margin-right: 5px; padding: 0px;"   />


                                      
                               
                   
            </div>
            </div>
            
            </div>
            </div>
            <div class="row">
                  <h4> <asp:Label ID="lblSchool" Visible="false" runat="server" Text="School Activity"></asp:Label> </h4>
          
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12">
             <%--/<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">--%>
                        
                     <asp:GridView ID="Gv_Profile_Search" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                        AllowPaging="true" PageSize="100"  AutoGenerateColumns="False" Font-Names="Arial"
                                                                        Font-Size="12px" Width="100%">
                                                                        <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
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
                                                                            <asp:TemplateField HeaderText="School Activity" Visible="true">
                                                                                <ItemTemplate>
                                                                                   
                                                                                         <asp:LinkButton ID="lbtn" OnClick="LnkSchool_OnClick"    runat="server" Text='<%# Bind("School") %>'  CommandArgument='<%# Bind("School") %>'  ></asp:LinkButton>
                                                                        <asp:Label ID="lblUn1" Visible="false" ForeColor="Black"  Text='<%# Bind("School") %>' Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                        runat="server" ></asp:Label>
                                                                           
                                                                                </ItemTemplate>
                                                         
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCol_2" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                        runat="server" ></asp:Label>
                                                                                </ItemTemplate>
                                                                        
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCol_3"  ForeColor="Black" runat="server" ></asp:Label>
                                                                                </ItemTemplate>
                                                                           
                                                                               
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCol_4" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                        runat="server" ></asp:Label>
                                                                                </ItemTemplate>
                                                                                      <HeaderStyle Width="5%" />
                                                                               
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCol_5"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                  
                                                                            </asp:TemplateField>

                                                                             <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCol_6"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCol_7"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCol_8"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>

                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCol_9"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                         <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCol_10"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCol_11"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCol_12"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCol_13"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                          <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCol_14"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCol_15"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                               <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCol_16"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Total" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TxtTotla"  ForeColor="Black" runat="server" ></asp:Label>
                                                                                </ItemTemplate>
                                                                           
                                                                            </asp:TemplateField>
                                                                           
                                                                        </Columns>
                                                                    </asp:GridView>


                        
                </div>
                     <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
             <%--/<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">--%>
                          <div style="height: 700px; overflow: auto; width: 99%;" align="center">
                     <asp:GridView ID="gvVillageWise" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                        AllowPaging="true" PageSize="100"  AutoGenerateColumns="False" Font-Names="Arial"
                                                                        Font-Size="12px" Width="100%">
                                                                        <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
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
                                                                            <asp:TemplateField HeaderText="School Activity" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSchool"  runat="server"
                                                                                        Text='<%# Eval("ActivityDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField  HeaderText="Village" >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblvillage" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                      Text= '<%# Eval("VillageName") %>'   runat="server" ></asp:Label>
                                                                                </ItemTemplate>
                                                                               
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField   HeaderText="School">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSchool"     Text='<%# Eval("SchoolName") %>'  ForeColor="Black" runat="server" ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                            
                                                                           
                                                                        </Columns>
                                                                    </asp:GridView>

</div>
                        
                </div>
            </div>

              <div class="row">
                <h4> <asp:Label ID="lblVillage" Visible="false" runat="server" Text="Village Activity"></asp:Label> </h4>
              
            <%--    <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">--%>
             <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12">
                 
        
                       <asp:GridView ID="gvVillageActivity" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                        AllowPaging="true" PageSize="100"  AutoGenerateColumns="False" Font-Names="Arial"
                                                                        Font-Size="12px" Width="100%">
                                                                        <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
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
                                                                            <asp:TemplateField HeaderText="Village Activity" Visible="true">
                                                                                <ItemTemplate>
                                                                                  

                                                     <asp:LinkButton ID="lbtnVillage" OnClick="LnkVillage_OnClick"    runat="server" Text='<%# Bind("Village") %>'    CommandArgument='<%# Bind("Village") %>'  ></asp:LinkButton>
                                                                      
                                                                                 <asp:Label ID="lblvllV_2" Text='<%# Bind("Village") %>' Visible="false" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                        runat="server" ></asp:Label>
                                                                           
                                                                                </ItemTemplate>
                                                                                
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColV_2" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                        runat="server" ></asp:Label>
                                                                                </ItemTemplate>
                                                                              
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColV_3"  ForeColor="Black" runat="server" ></asp:Label>
                                                                                </ItemTemplate>
                                                                                  
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColV_4" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                        runat="server" ></asp:Label>
                                                                                </ItemTemplate>
                                                                                    
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColV_5"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                  
                                                                            </asp:TemplateField>

                                                                             <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColV_6"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColV_7"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColV_8"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>

                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColV_9"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                         <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColV_10"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColV_11"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColV_12"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColV_13"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                          <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColV_14"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColV_15"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                               <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColV_16"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Total" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TxtTotlaV"  ForeColor="Black" runat="server" ></asp:Label>
                                                                                </ItemTemplate>
                                                                                                                                                 </asp:TemplateField>
                                                                           
                                                                        </Columns>
                                                                    </asp:GridView>          
                </div>

                  <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
             <%--/<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">--%>
                           <div style="height: 450px; overflow: auto; width: 99%;" align="center">
                     <asp:GridView ID="gvVillageDeatial" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                        AllowPaging="true" PageSize="100"  AutoGenerateColumns="False" Font-Names="Arial"
                                                                        Font-Size="12px" Width="100%">
                                                                        <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
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
                                                                            <asp:TemplateField HeaderText="Village Activity" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSrrchool"  runat="server"
                                                                                        Text='<%# Eval("ActivityDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField  HeaderText="Village" >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblrrvillage" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                      Text= '<%# Eval("VillageName") %>'   runat="server" ></asp:Label>
                                                                                </ItemTemplate>
                                                                               
                                                                            </asp:TemplateField>
                                                                           
                                                                            
                                                                           
                                                                        </Columns>
                                                                    </asp:GridView>
</div>

                        
                </div>
                 
            </div>


              <div class="row">
            <%--    <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12">--%>
                <h4> <asp:Label ID="lblOffice" Visible="false" runat="server" Text="Office Activity"></asp:Label> </h4>
            
            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12">
                 
                 
                        
                

                 <asp:GridView ID="gvOffice" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                        AllowPaging="true" PageSize="100"  AutoGenerateColumns="False" Font-Names="Arial"
                                                                        Font-Size="12px" Width="60%">
                                                                        <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
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
                                                                            <asp:TemplateField HeaderText="Office Activity" Visible="true">
                                                                                <ItemTemplate>
                                                                                                           <asp:LinkButton ID="lbtnOffice" OnClick="LnkOffice_OnClick"    runat="server" Text='<%# Bind("Village") %>'  CommandArgument='<%# Bind("Village") %>'  ></asp:LinkButton>
                                                                               <asp:Label ID="lbooff"  Text='<%# Bind("Village") %>' Visible="false"  ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                        runat="server" ></asp:Label>
                                                                                </ItemTemplate>
                                                        
                                                   
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColO_2" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                        runat="server" ></asp:Label>
                                                                                </ItemTemplate>
                                                                              
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColO_3"  ForeColor="Black" runat="server" ></asp:Label>
                                                                                </ItemTemplate>
                                                                                  
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColO_4" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                        runat="server" ></asp:Label>
                                                                                </ItemTemplate>
                                                                                    
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColO_5"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                  
                                                                            </asp:TemplateField>

                                                                             <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColO_6"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColO_7"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColO_8"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>

                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColO_9"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                         <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColO_10"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColO_11"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColO_12"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColO_13"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                                 
                                                                            </asp:TemplateField>
                                                                          <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColO_14"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                           <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColO_15"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                               <asp:TemplateField  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColO_16"  ForeColor="Black" runat="server"
                                                                                       ></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Total" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TxtTotlaO"  ForeColor="Black" runat="server" ></asp:Label>
                                                                                </ItemTemplate>
                                                                                                                                                 </asp:TemplateField>
                                                                           
                                                                        </Columns>
                                                                    </asp:GridView>    
                </div>

                  <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
             <%--/<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">--%>
                                                           <div style="height: 350px; overflow: auto; width: 99%;" align="center">
                     <asp:GridView ID="gvVillageOffice" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                        AllowPaging="true" PageSize="100"  AutoGenerateColumns="False" Font-Names="Arial"
                                                                        Font-Size="12px" Width="100%">
                                                                        <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
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
                                                                            <asp:TemplateField HeaderText="Office Activity" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblhhool"  runat="server"
                                                                                        Text='<%# Eval("ActivityDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField  HeaderText="Village" >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblOffvillage" ForeColor="Black" Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                                      Text= '<%# Eval("VillageName") %>'   runat="server" ></asp:Label>
                                                                                </ItemTemplate>
                                                                               
                                                                            </asp:TemplateField>
                                                                           
                                                                            
                                                                           
                                                                        </Columns>
                                                                    </asp:GridView>

</div>
                        
                </div>
                 
                 
            </div>
        </div>
    </div>
     

<%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
