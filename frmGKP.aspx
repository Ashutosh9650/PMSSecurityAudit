<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmGKP.aspx.cs" MasterPageFile="~/Site.master" Inherits="frmGKP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<style type="text/css">
    .HeaderClassCsss
{
text-align:center! important; font-weight:normal !important; background-color:#9A9C9A !important; 
}
</style>
   <script type="text/javascript">


       function isNumberKey(txt, evt) {
           debugger;
           var charCode = (evt.which) ? evt.which : evt.keyCode;
           if (charCode == 46 && charCode == 127) {
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="container-fluid" >
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="panel panel-default" style="height: 550px;">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                        <h3 class="text-danger" style="margin: 0px;">
                                            <asp:Label ID="lblMain" runat="server" Text="GKP Session plan "></asp:Label>
                                        </h3>
                                    </div>
                                   <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">
                                  
                                    
                                    <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                        ToolTip="Save" ImageUrl="~/images/save-29-1.png"  ValidationGroup="saves" Visible="false"
                                        Style="margin-right: 5px; padding: 0px;" runat="server" />
                                 
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
                                                            <div class="col-sm-8 padd">
                                                                <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server"   class="form-control ">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>    

                                                     <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                Subject:
                                                            </label>
                                                            <div class="col-sm-8 padd">
                                                                <asp:DropDownList ID="ddls"  runat="server"   class="form-control ">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                  <asp:ListItem  Value="1" >Hindi</asp:ListItem>
                                                            <asp:ListItem  Value="2">English</asp:ListItem>
                                                             <asp:ListItem  Value="3">Math</asp:ListItem>
                                                           
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>    
                                                    
                                                     <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-10 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                                      <%--  <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server"
                                                            class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />--%>
                                                         <asp:Button ID="btndisplay" Style="margin-right: 8px" runat="server" class="btn btn-danger pull-right" Text="Search"
                                                OnClick="btn_display_Click" />

                                                    </div>  
                                                    
                                                <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-10 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">     
                                                                                         <asp:ImageButton ID="ImageButton11" CssClass="btn btn-info pull-right"
                                        BackColor="#f5f5f5" ToolTip="Add"  OnClick="btnAddGkp_Click" ImageUrl="~/images/add-29-1.png" 
                                        Style="margin-right: 5px; padding: 0px;" runat="server" />     
                                        </div>
                                        </div>
                                    </div>
                                </div>
                            </div>



                       
                            <div class="panel-body">
                                <div class="row table-responsive">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div style="height: 610px; overflow: auto; width: 99%;" align="center">
                                        <asp:GridView ID="DGV_CLT" runat="server" OnRowDataBound="GvReport_RowDataBound" CssClass="table-striped table-bordered table-hover"
                                            AutoGenerateColumns="False"  Width="50%"  >

                                             <FooterStyle CssClass="FooterStyle" />
                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                                        <RowStyle HorizontalAlign="Left" />
                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                            <Columns>
                                     
                                                
                                             

                                                <asp:TemplateField HeaderText="Subject">
                                                    <ItemTemplate>
                                                              <asp:Label ID="lblHindi" BackColor="Transparent"  runat="server" Text='<%# Eval("SubjectName") %>'></asp:Label>
                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="15%" />
                                                    <ItemStyle  HorizontalAlign="Center"/>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Level">
                                                    <ItemTemplate>

                                                         <asp:Label ID="lblLevel" BackColor="Transparent" runat="server" Text='<%# Eval("Level") %>'></asp:Label>
                                                    
                                                      
                                                    </ItemTemplate>
                                                     <HeaderStyle Width="15%" />
                                                    <ItemStyle  HorizontalAlign="Center"/>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Sessions">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtNoOfSeesion"   
                                                            runat="server" MaxLength="25" 
                                                            Text='<%# Bind("NoofLevel") %>' BorderStyle="None"></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                     <HeaderStyle Width="15%" />
                                                    <ItemStyle  HorizontalAlign="Center"/>
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Main" >
                                                    <ItemTemplate>
                                                       <asp:Label ID="txtMain"   
                                                            runat="server" MaxLength="25" 
                                                             BorderStyle="None"></asp:Label>
                                                   </ItemTemplate>
                                                     <HeaderStyle Width="15%" />
                                                    <ItemStyle  HorizontalAlign="Center"/>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Revision" >
                                                    <ItemTemplate>
                                                       <asp:Label ID="TxtRevision"   
                                                            runat="server" MaxLength="25" 
                                                             BorderStyle="None"></asp:Label>
                                                  </ItemTemplate>
                                                     <HeaderStyle Width="15%" />
                                                    <ItemStyle  HorizontalAlign="Center"/>
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Sessions Type" Visible="false">
                                                    <ItemTemplate>
                                                          <asp:DropDownList ID="ddlstt"  runat="server"   class="form-control ">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                  <asp:ListItem  Value="1" >Main</asp:ListItem>
                                                                     <asp:ListItem  Value="2">Revision</asp:ListItem>
                                                                     <asp:ListItem  Value="3">Both</asp:ListItem>
                                                           
                                                                </asp:DropDownList>
                                                         <asp:Label ID="txtMainTypeID"  CssClass="form-controlAbhi"
                                                            runat="server" MaxLength="5"  Visible="false"
                                                            Text='<%# Bind("MainTypeID") %>' BorderStyle="None"></asp:Label>

                                                            <asp:Label ID="txtMainID"  CssClass="form-controlAbhi"
                                                            runat="server" MaxLength="5"  Visible="false"
                                                            Text='<%# Bind("ID") %>' BorderStyle="None"></asp:Label>

                                                              <asp:Label ID="lblSubject"  CssClass="form-controlAbhi"
                                                            runat="server" MaxLength="5"  Visible="false"
                                                            Text='<%# Bind("SubjectID") %>' BorderStyle="None"></asp:Label>
                                                               <asp:Label ID="Label4"  CssClass="form-controlAbhi"
                                                            runat="server" MaxLength="5"  Visible="false"
                                                            Text='<%# Bind("LevelID") %>' BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                     <HeaderStyle Width="15%" />
                                                    <ItemStyle  HorizontalAlign="Center"/>
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

              <asp:ModalPopupExtender ID="ModalAlert" runat="server" TargetControlID="hdn_alertmodal"
                BehaviorID="ModalAlertb" PopupControlID="pnl_alert" CancelControlID="btn_cancelalert"
                BackgroundCssClass="ModalPopupBG">
            </asp:ModalPopupExtender>
            <asp:HiddenField ID="hdn_alertmodal" runat="server" />
            <asp:Panel ID="pnl_alert" runat="server" Style="display: none;" BackColor="#E8E5E2"
                BorderColor="#E8E5E2" BorderStyle="Ridge" BorderWidth="2px" Width="500px" Height="500px">
                <div class="divbgs" style="padding: 0 0 10px 0;">
                    <div class="longnamecsspop" style="background-color: #545454; color: White; font-family: arial,
            Helvetica, sans-serif; font-size: 19px; width: 100%; padding: 5px 10px 0 10px; margin-left: auto; margin-right: auto; height: 34px;">
                       GKP 
                    </div>
                    <div class="row" style="margin-top: 10px">
                        <div class="col-xs-4">
                            <asp:Label ID="Label1" runat="server">Subject</asp:Label>
                            <asp:DropDownList runat="server" ID="DropDownList2" CssClass="form-control">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">Hindi</asp:ListItem>
                                <asp:ListItem Value="2">English</asp:ListItem>
                                <asp:ListItem Value="3">Math</asp:ListItem>
                               
                               
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-4">
                            <asp:Label ID="Label2" runat="server">Level</asp:Label>
                          
                                                              <asp:DropDownList ID="ddMainlLevel"  runat="server"  OnSelectedIndexChanged="ddMainlLevel_SelectedIndexChanged" AutoPostBack="true"   class="form-control ">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                  <asp:ListItem  Value="1" >L0</asp:ListItem>
                                                            <asp:ListItem  Value="2">L1</asp:ListItem>
                                                             <asp:ListItem  Value="3">L2</asp:ListItem>
                                                             <asp:ListItem  Value="4">L3</asp:ListItem>
                                                           
                                                                </asp:DropDownList>

                        </div>
                        <div class="col-xs-4">
                            <asp:Label ID="Label3" runat="server">No Of Session</asp:Label>
                            <asp:TextBox runat="server" ID="TextBox4" OnTextChanged="NoOfSession_Click" AutoPostBack="true" CssClass="form-control" Enabled="True"></asp:TextBox>
                        </div>
                      
                    </div>
                     <div class="row" style="margin-top: 10px">
                         <div class="col-xs-4">
                            <asp:Label ID="La88bel4" runat="server">No Of Recap</asp:Label>
                            <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control"   OnTextChanged="NoOfRecap_Click" AutoPostBack="true" Enabled="True"></asp:TextBox>
                        </div>
                          <div class="col-xs-4">
                            <asp:Label ID="Label5" runat="server">No Of Remedial</asp:Label>
                            <asp:TextBox runat="server" ID="TextBox2" CssClass="form-control"  OnTextChanged="NoOfRemedial_Click" AutoPostBack="true" Enabled="True"></asp:TextBox>
                        </div>
                     </div>
                     <div class="row" style="margin-top: 10px">
                       <div class="col-xs-4">
                        <asp:Label ID="Label6" runat="server"></asp:Label>
                       </div>
                         <div class="col-xs-4">
                        <asp:Label ID="Label7" runat="server"/>
                       </div>
                         <div class="col-xs-4">
                        <asp:Label ID="Label8" runat="server"/>
                       </div>
                     </div>
                     <div class="row" style="margin-top: 10px">
                    
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table-striped table-bordered table-hover"
                                            AutoGenerateColumns="False"  Width="80%"  OnRowDataBound="GvReport1_RowDataBound"  >

                                             <FooterStyle CssClass="FooterStyle" />
                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="Black" Height="40px" />
                                                                        <RowStyle HorizontalAlign="Left" />
                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                            <Columns>
                                     
                                                
                                             

                                                <asp:TemplateField HeaderText="Subject" Visible="false">
                                                    <ItemTemplate>
                                                              <asp:Label ID="lblHindi"  BackColor="Transparent" CssClass="form-controlAbhi" runat="server" Text='<%# Eval("SubjectName") %>'></asp:Label>
                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="15%" />
                                                    <ItemStyle  HorizontalAlign="Center"/>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Level" Visible="false">
                                                    <ItemTemplate>
                                                     <asp:Label ID="lblLevel" BackColor="Transparent" runat="server" Text='<%# Eval("Level") %>'></asp:Label>
                                                    
                                                      
                                                    </ItemTemplate>
                                                     <HeaderStyle Width="15%" />
                                                    <ItemStyle  HorizontalAlign="Center"/>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Sessions">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtNoOfSeesion" 
                                                            runat="server" MaxLength="25" 
                                                            Text='<%# Bind("NoofLevel") %>' BorderStyle="None"></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                     <HeaderStyle Width="15%" />
                                                    <ItemStyle  HorizontalAlign="Center"/>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Main">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkMain"  Enabled="false" 
                                                            runat="server"></asp:CheckBox>
                                                        
                                                    </ItemTemplate>
                                                     <HeaderStyle Width="15%" />
                                                    <ItemStyle  HorizontalAlign="Center"/>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Revision">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkRevision"  Checked ="true"  
                                                            runat="server"></asp:CheckBox>
                                                        
                                                    </ItemTemplate>
                                                     <HeaderStyle Width="15%" />
                                                    <ItemStyle  HorizontalAlign="Center"/>
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Sessions Type" Visible="false">
                                                    <ItemTemplate>
                                                          <asp:DropDownList ID="ddlstt"  runat="server"   class="form-control ">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                  <asp:ListItem  Value="1" >Main</asp:ListItem>
                                                                     <asp:ListItem  Value="2">Revision</asp:ListItem>
                                                                     <asp:ListItem  Value="3">Both</asp:ListItem>
                                                           
                                                                </asp:DropDownList>
                                                         <asp:Label ID="txtMainTypeID"  CssClass="form-controlAbhi"
                                                            runat="server" MaxLength="5"  Visible="false"
                                                            Text='<%# Bind("MainTypeID") %>' BorderStyle="None"></asp:Label>

                                                            <asp:Label ID="txtMainID"  CssClass="form-controlAbhi"
                                                            runat="server" MaxLength="5"  Visible="false"
                                                            Text='<%# Bind("ID") %>' BorderStyle="None"></asp:Label>

                                                              <asp:Label ID="lblSubject"  CssClass="form-controlAbhi"
                                                            runat="server" MaxLength="5"  Visible="false"
                                                            Text='<%# Bind("SubjectID") %>' BorderStyle="None"></asp:Label>
                                                               <asp:Label ID="Label4"  CssClass="form-controlAbhi"
                                                            runat="server" MaxLength="5"  Visible="false"
                                                            Text='<%# Bind("LevelID") %>' BorderStyle="None"></asp:Label>
                                                             <asp:Label ID="lblTypeID"  CssClass="form-controlAbhi"
                                                            runat="server" MaxLength="5"  Visible="false"
                                                            Text='<%# Bind("TypeID") %>' BorderStyle="None"></asp:Label>
                                                            
                                                    </ItemTemplate>
                                                     <HeaderStyle Width="15%" />
                                                    <ItemStyle  HorizontalAlign="Center"/>
                                                </asp:TemplateField>
                                            

                                              
                                            </Columns>
                                        </asp:GridView>
                                  
                     </div>
                      <div class="row" >
                    <div style="Text-align: center; margin-top: 65px; margin-right: 223px;">
                        <asp:Button ID="btn_cancelalert" runat="server"  class="btn btn-danger
            pull-right" Text=" Cancel " Height="33px" Width="59px" />
                        <asp:Button ID="btn_show" runat="server" OnClick="btn_show_Click"  class="btn btn-danger
            pull-right" Text=" Save " Style="margin-right:5px" Height="33px" Width="59px" />
                    </div>
                    </div>
                </div>
            </asp:Panel>
             

            

            <div class="row" style="margin: 0px 0px 10px 0px;">
                            <div class="col-xs-12" style="padding: 0px;">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel4" Style="overflow: auto;" runat="server" Width="100%">
                                            <asp:GridView ID="Dgv_LeftGrid" runat="server"  AutoGenerateColumns="False" Font-Names="Arial"
                                                CssClass="table table-striped table-bordered  table-responsive" Width="100%" Font-Size="12px"
                                                 AllowPaging="true" PageSize="15" OnRowDataBound="Dgv_LeftGrid_RowDataBound">
                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                 <EmptyDataTemplate>
                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                        Data not found</div>
                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Subject" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SubjectID" runat="server" Text='<%# Bind("SubjectID") %>' Style="width: 100%;"></asp:Label>    
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="13%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" CssClass="gridcolpadding"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Level" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlLevelID"  runat="server"   class="form-control ">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                  <asp:ListItem  Value="1" >L0</asp:ListItem>
                                                            <asp:ListItem  Value="2">L1</asp:ListItem>
                                                             <asp:ListItem  Value="3">L2</asp:ListItem>
                                                             <asp:ListItem  Value="4">L3</asp:ListItem>
                                                           
                                                                </asp:DropDownList>

                                                        </ItemTemplate>
                                                        <HeaderStyle Width="13%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" CssClass="gridcolpadding"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" Name" Visible="true">
                                                        <ItemTemplate>
                                                           <asp:TextBox  ID="StudentName" runat="server"  Style="width: 100%;"></asp:TextBox>
                                                        </ItemTemplate>
                                                        
                                                    </asp:TemplateField>
                                                </Columns>

                                                <PagerStyle CssClass="dgvPageing" />
                                                <HeaderStyle BackColor="#A7A2A4" ForeColor="White" />
                                                <FooterStyle BackColor="Transparent" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                    <asp:PostBackTrigger ControlID="Dgv_LeftGrid" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
            </ContentTemplate>
         
    </asp:UpdatePanel>
</asp:Content>
