<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="frmTravelMatrixApprove.aspx.cs" Culture="en-GB" Inherits="frmTravelMatrixApprove" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

   
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="online-js/jquery-ui.min.js" type="text/javascript"></script>
    <link href="online-js/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="online-js/jquery-ui-timepicker-addon.js" type="text/javascript"></script>
    <style type="text/css">
        .HeaderClassCsss
        {
            text-align: center !important;
            font-weight: normal !important;
            background-color: #9A9C9A !important;
        }
        
        
        .pdtopGRD
        {
            padding-top: 0px !important;
        }
        .modalBg
        {
            background-color: #000;
            opacity: 0.5;
            z-index: 11;
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="container-fluid" >
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="panel panel-default" style="height: 930px;">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                        <h3 class="text-danger" style="margin: 0px;">
                                            <asp:Label ID="lblMain" runat="server" Text="Travel Matrix"></asp:Label>
                                        </h3>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">
                         
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div id="div-show-new">
                                    <div class="row marg search-bg">
                                        <div class="form-horizontal">
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Year:
                                                    </label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                            runat="server" class="form-control ">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        State:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState1_SelectedIndexChanged"
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
                                                        <asp:DropDownList ID="ddlBlock" runat="server" 
                                                            class="form-control " />
                                                    </div>
                                                </div>
                                            </div>
                                          
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Month:</label>
                                                    <div class="col-sm-9 padd">
                                                       
    <asp:DropDownList ID="ddlMonth" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlmonthselectindex">
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
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Date Range:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:TextBox ID="txtfd" runat="server" class="form-control">
                                                        </asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtfdcal"  runat="server" Format="dd/MM/yyyy" TargetControlID="txtfd">
                                                        </cc1:CalendarExtender>
                                                       
                                                    </div>
                                                </div>
                                            </div>

                                                     <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                <div class="form-group">
                                                   
                                                    <div class="col-sm-9 padd">
                                                        <asp:TextBox ID="txttd" runat="server"   class="form-control"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txttdcal" runat="server" Format="dd/MM/yyyy" TargetControlID="txttd">
                                                        </cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm21 cpl-xs-12 col-lg-offset-0 col-md-offset-0 col-sm-offset-0 col-xs-offset-0">
                                                <asp:ImageButton ID="btnSerach" ToolTip="Search" runat="server" class="btn btn-danger btn-paddd pull-left"
                                                    ValidationGroup="saves" BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                                             <asp:Button ID="btnsubmit" ToolTip="Approve" Text="Approval" runat="server" class="btn btn-danger btn-paddd pull-right"
                                                    OnClick="btnSubmit_Click" ImageUrl="~/images/statusG.png" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="row table-responsive">
                                    <div style="min-height: 500px;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <asp:Label ID="lblB1DA"  runat="server" Visible="false"></asp:Label>
                                                 <asp:Label ID="lblFromBlock"  runat="server" Visible="false"></asp:Label>
                                                          <asp:Label ID="lblToBlock"  runat="server" Visible="false"></asp:Label>
                                            <div style="overflow: auto; margin-top: 0px; height: 730px;">
                                                <asp:GridView ID="GV_TravelMatrix" Width="60%" CssClass="table  table-bordered"
                                                    runat="server" AutoGenerateColumns="false" ShowFooter="false" 
                                                    ShowHeader="true">
                                                   
                                                    <FooterStyle CssClass="FooterStyle" />
                                                    <HeaderStyle BackColor="#C1C1C1" Height="44px" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#897A7A" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                    <AlternatingRowStyle BackColor="Transparent" />
                                                    <Columns>
                                                   
                                                      
                                                        <asp:TemplateField HeaderText="Block Name" HeaderStyle-Width="13%">
                                                            <ItemTemplate>
                                                              
                                                                <asp:Label ID="lblVillage" Text='<%#Bind("BlockName") %>' runat="server"> </asp:Label>
                                                     <asp:Label ID="lblBlockCOde" Visible="false" Text='<%#Bind("BlockCOde") %>' runat="server">  </asp:Label>
                                                              
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" />
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Cluster Name" HeaderStyle-Width="13%">
                                                            <ItemTemplate>
                                                                       <asp:Label ID="Label1"  Text='<%#Bind("ClusterName") %>' runat="server"></asp:Label>
                                                               <asp:LinkButton ID="lnlClusterNames"  Visible="false" OnClick="LnkStatus_OnClick" BorderStyle="None" Width="100%" Text='<%#Bind("ClusterName") %>' 
                                                                    runat="server" ></asp:LinkButton>
                                                                <asp:Label ID="lblClusterCode" Visible="false" Text='<%#Bind("ClusterCode") %>' runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Employee Code" HeaderStyle-Width="13%">
                                                            <ItemTemplate>
                                                               <asp:LinkButton ID="lnlUserNames" OnClick="LnkStatus_OnClick" BorderStyle="None" Width="100%" Text='<%#Bind("UserName") %>' 
                                                                    runat="server" ></asp:LinkButton>
                                                          
                                                                  <asp:Label ID="lblUserID" Visible="false" Text='<%#Bind("UserID") %>' runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" />
                                                        </asp:TemplateField>
                                                         
                                                         
                                                        <asp:TemplateField HeaderText="TA" HeaderStyle-Width="13%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtFare" BorderStyle="None" Width="100%" Text='<%#Bind("Fare") %>'
                                                                    runat="server" Wrap="true"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="DA" HeaderStyle-Width="13%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTADA" BorderStyle="None" Width="100%" Text='<%#Bind("TADA") %>'
                                                                    runat="server" Wrap="true"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        
                                                             <asp:TemplateField HeaderText="Status" HeaderStyle-Width="13%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgg" BorderStyle="None" Width="100%" Text='<%#Bind("Status") %>'
                                                                    runat="server" Wrap="true"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>




                                                    <asp:GridView ID="GvAccount" Width="100%" CssClass="table  table-bordered"
                                                    runat="server" AutoGenerateColumns="false" ShowFooter="false" 
                                                    ShowHeader="true">
                                                    
                                                    <FooterStyle CssClass="FooterStyle" />
                                                    <HeaderStyle BackColor="#C1C1C1" Height="44px" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#897A7A" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                    <AlternatingRowStyle BackColor="Transparent" />
                                                    <Columns>
                                                   
                                                      

                                                       <asp:TemplateField HeaderText="Sr No" HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                              
                                                          
                                                                  <asp:Label ID="lblUgD"  Text='<%#Bind("SrNo") %>' runat="server"/>     
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" />
                                                        </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Emp ID" HeaderStyle-Width="13%">
                                                            <ItemTemplate>
                                                              
                                                                
                                                                
                                                              <asp:LinkButton ID="lnlUserNames" OnClick="LnkStatus_OnClick1" BorderStyle="None" Width="100%" Text='<%#Bind("EmpID") %>' 
                                                                    runat="server" ></asp:LinkButton>
                                                          
                                                                  
                                                   
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" />
                                                        </asp:TemplateField>

                                                          <asp:TemplateField HeaderText="Emp Name" HeaderStyle-Width="13%">
                                                            <ItemTemplate>
                                                              
                                                          
                                                                
                                                            <asp:LinkButton ID="lnlUserNames8" OnClick="LnkStatus_OnClick1" BorderStyle="None" Width="100%" Text='<%#Bind("EmpName") %>' 
                                                                    runat="server" ></asp:LinkButton>
                                                          
                                                                  <asp:Label ID="lblUserID1" Visible="false" Text='<%#Bind("UserID") %>' runat="server"/>

                                                            
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" />
                                                        </asp:TemplateField>



                                                         <asp:TemplateField HeaderText="Designation." HeaderStyle-Width="13%">
                                                            <ItemTemplate>
                                                              
                                                          
                                                                  <asp:Label ID="lblU4gggdD"  Text='<%#Bind("Designation") %>' runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" />
                                                        </asp:TemplateField>

                                                          
                                                        <asp:TemplateField HeaderText="Block Name" HeaderStyle-Width="13%">
                                                            <ItemTemplate>
                                                              
                                                                <asp:Label ID="lblVillage" Text='<%#Bind("BlockName") %>' runat="server"> </asp:Label>
                                                     <asp:Label ID="lblBlockCOde" Visible="false" Text='<%#Bind("BlockCode") %>' runat="server">  </asp:Label>
                                                              
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" />
                                                        </asp:TemplateField>

                                                         


                                                          <asp:TemplateField HeaderText="Cluster Name" HeaderStyle-Width="13%">
                                                            <ItemTemplate>
                                                             <asp:Label ID="Label2" Text='<%#Bind("ClusterName") %>' runat="server">
                                                                </asp:Label>
                                                               <asp:LinkButton ID="lnlClusterNames"  Visible="false" OnClick="LnkStatus_OnClick1" BorderStyle="None" Width="100%" Text='<%#Bind("ClusterName") %>' 
                                                                    runat="server" ></asp:LinkButton>
                                                                <asp:Label ID="lblClusterCode" Visible="false" Text='<%#Bind("ClusterCode") %>' runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" />
                                                        </asp:TemplateField>

                                                        
                                                           <asp:TemplateField HeaderText="Travel Period" HeaderStyle-Width="13%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txt7Fare" BorderStyle="None" Width="100%" Text='<%#Bind("TravelPeriod") %>'
                                                                    runat="server" Wrap="true"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>

                                                         
                                                        <asp:TemplateField HeaderText="TA" HeaderStyle-Width="13%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtFa9re" BorderStyle="None" Width="100%" Text='<%#Bind("LocalConveyance") %>'
                                                                    runat="server" Wrap="true"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="DA" HeaderStyle-Width="13%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTADA" BorderStyle="None" Width="100%" Text='<%#Bind("DA") %>'
                                                                    runat="server" Wrap="true"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Total" HeaderStyle-Width="13%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtrrrTADA" BorderStyle="None" Width="100%" Text='<%#Bind("Total") %>'
                                                                    runat="server" Wrap="true"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        
                                                             <asp:TemplateField HeaderText="Status" HeaderStyle-Width="13%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgg" BorderStyle="None" Width="100%" Text='<%#Bind("Status") %>'
                                                                    runat="server" Wrap="true"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
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
            </div>
            <asp:HiddenField ID="hdnL" runat="server" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBg "
                CancelControlID="CancelButton" PopupControlID="PnlDistrict1" TargetControlID="HdnFild1">
            </cc1:ModalPopupExtender>
            <asp:HiddenField ID="HdnFild1" runat="server"></asp:HiddenField>
            <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 34% !important;
                margin-top: 105px !important;" ID="PnlDistrict1" runat="server">
                <div style="width: 100%; height: auto; background-color: #f1f1f1">
                    <div class="modal-header" style="background-color: #ddd; color: White;">
                        <h4 class="modal-title" style="forecolor: White">
                            Process For Payment</h4>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="Label8" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                        <div class="form-horizontal" role="form">
                            <div class="form-group">
                                <asp:Label ID="Label9" class="control-label col-sm-4 lab-text-left" runat="server"
                                    Text="Payment Process Date:"></asp:Label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" ID="txtPaymentDate" autocomplete="off" ondrop="return false;"
                                        class="form-control" onkeypress="return false;"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender8rdate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtPaymentDate" PopupPosition="BottomRight">
                                    </cc1:CalendarExtender>


                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPaymentDate"
                                        Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                        SetFocusOnError="True" ValidationGroup="SaveP"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="ImageButton2" OnClick="btnPayment_Click" Text="Process For Payment"
                            runat="server" class="btn btn-danger btn-paddd" ValidationGroup="SaveP" Style="float: none;" ToolTip="Save">
                        </asp:Button>&nbsp;
                        <asp:ImageButton ID="CancelButton" ImageUrl="~/images/close-29.png" runat="server"
                            Text="Close" ToolTip="Close" Style="float: none;"></asp:ImageButton></div>
                </div>
            </asp:Panel>

        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
