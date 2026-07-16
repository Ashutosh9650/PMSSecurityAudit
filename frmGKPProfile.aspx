<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  Culture="en-GB" CodeFile="frmGKPProfile.aspx.cs"
    Inherits="frmGKPProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
    <style type="text/css">
        .checkbox label:after, .radio label:after
        {
            content: '';
            display: table;
            clear: both;
        }
        .checkbox .cr, .radio .cr
        {
            position: relative;
            display: inline-block;
            border: 2px solid #333;
            border-radius: .25em;
            width: 1.3em;
            height: 1.3em;
            float: left;
            margin-right: .5em;
            color: red;
        }
        
        .radio .cr
        {
            border-radius: 75%;
            border-color: #333;
        }
        
        .checkbox .cr .cr-icon, .radio .cr .cr-icon
        {
            position: absolute;
            font-size: .8em;
            line-height: 0;
            top: 50%;
            left: 15%;
        }
        
        .radio .cr .cr-icon
        {
            margin-left: 0.04em;
        }
        
        .checkbox label input[type="checkbox"], .radio label input[type="radio"]
        {
            display: none;
        }
        
        .checkbox label input[type="checkbox"] + .cr > .cr-icon, .radio label input[type="radio"] + .cr > .cr-icon
        {
            transform: scale(3) rotateZ(-220deg);
            opacity: 0;
            transition: all .7s ease-in;
        }
        
        .checkbox label input[type="checkbox"]:checked + .cr > .cr-icon, .radio label input[type="radio"]:checked + .cr > .cr-icon
        {
            transform: scale(1) rotateZ(0deg);
            opacity: 1;
        }
        
        .checkbox label input[type="checkbox"]:disabled + .cr, .radio label input[type="radio"]:disabled + .cr
        {
            opacity: .5;
        }
        
        .new-navbutt
        {
            float: left !important;
            margin-top: 0px !important;
        }
        
        .row-border
        {
            border-bottom: 1px dotted rgb(221, 221, 221);
            margin-bottom: 15px;
        }
        
        .checkbox
        {
            position: relative;
            display: block;
            margin-top: 2px !important;
            margin-bottom: 5px !important;
        }
        
        
        .new-navbutt
        {
            float: left !important;
            margin-top: 0px !important;
        }
        
        .row-border
        {
            border-bottom: 1px dotted rgb(221, 221, 221);
            margin-bottom: 15px;
        }
        .checkbox .cr .cr-icon, .radio .cr .cr-icon
        {
            position: absolute;
            font-size: .8em;
            line-height: 0;
            top: 50%;
            left: 15%;
        }
        
        .radio .cr .cr-icon
        {
            margin-left: 0.04em;
        }
        
        .checkbox label input[type="checkbox"], .radio label input[type="radio"]
        {
            display: none;
        }
        
        .checkbox label input[type="checkbox"] + .cr > .cr-icon, .radio label input[type="radio"] + .cr > .cr-icon
        {
            transform: scale(3) rotateZ(-220deg);
            opacity: 0;
            transition: all .7s ease-in;
        }
        
        .checkbox label input[type="checkbox"]:checked + .cr > .cr-icon, .radio label input[type="radio"]:checked + .cr > .cr-icon
        {
            transform: scale(1) rotateZ(0deg);
            opacity: 1;
        }
        
        .checkbox label input[type="checkbox"]:disabled + .cr, .radio label input[type="radio"]:disabled + .cr
        {
            opacity: .5;
        }
        
        .new-navbutt
        {
            float: left !important;
            margin-top: 0px !important;
        }
        
        .row-border
        {
            border-bottom: 1px dotted rgb(221, 221, 221);
            margin-bottom: 15px;
        }
        
        .checkbox
        {
            position: relative;
            display: block;
            margin-top: 2px !important;
            margin-bottom: 5px !important;
        }
        .CheckBoxListCssClass
        {
            font-family: calibri;
            margin-left: 5p .checkbox { position: relative;
            display: block;
            margin-top: 2px !important;
            margin-bottom: 5px !important;
        }
        .CheckBoxListCssClass
        {
            font-family: calibri;
            margin-left: 5px;
            font-weight: bold;
            font-size: small;
            top: 53%;
            left: 3%;
        }
        .checkboxlist
        {
            position: absolute;
            font-size: .8em;
            margin-left: 10px;
            line-height: 0;
            top: 50%;
            left: 15%;
        }
        .td-widt
        {
            width: auto !important;
        }
        
        .td-width1
        {
            width: 150px !important;
        }
        
        @media (min-width:10px) and (max-width:640px)
        {
            .td-widt
            {
                width: 90px !important;
            }
        
        
            .td-width1
            {
                width: 90px !important;
            }
        }
        
        .table-mb
        {
            margin-bottom: 2px !important;
        }
        
        .thnail
        {
            padding: 0px !important;
            border-radius: 0px !important;
            margin-bottom: 0px !important;
            min-height: 60px;
        }
    </style>
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
<style type="text/css">
    .multiselect {
    width:20em;
    height:15em;
    border:solid 1px #c0c0c0;
    overflow:auto;
}
 
.multiselect label {
    display:block;
}
 
.multiselect-on {
    color:#ffffff;
    background-color:#000099;
     </style>
}
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
        <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>
    <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 style="margin: 0px;">
                         GKP</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="row marg search-bg" style="margin-left: -11px;">
                            <div class="form-horizontal">
                         
                               <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                    <div class="form-group" style="margin-bottom: 7px;">
                        <label for="email" class="col-sm-3 padd linhei">
                            FC:</label>
                        <div class="col-sm-9 padd">
                            <asp:DropDownList ID="ddlUser" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged"
                                                runat="server" AutoPostBack="true" class="form-control ">
                                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                    <div class="form-group" style="margin-bottom: 7px;">
                        <label for="email" class="col-sm-3 padd linhei">
                            Village:</label>
                        <div class="col-sm-9 padd">
                             <asp:DropDownList ID="ddlVilage" OnSelectedIndexChanged="ddlVilage_SelectedIndexChanged"
                                                runat="server" AutoPostBack="true" class="form-control " />
                        </div>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                    <div class="form-group" style="margin-bottom: 7px;">
                        <label for="email" class="col-sm-3 padd linhei">
                            Date:</label>
                        <div class="col-sm-9 padd">
                           <asp:TextBox runat="server" ID="txtDate"  autocomplete="off" ondrop="return false;"
                                                class="form-control" onkeypress="return false;"></asp:TextBox>
                                          
                                            <ajax:CalendarExtender ID="CalendarExtenderTourdate" OnClientDateSelectionChanged="arrivaldatecheck" runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight">
                                            </ajax:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="ReqTxtDate" runat="server" ControlToValidate="txtDate"
                                                Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
                            <span id="ctl00_MainContent_ReqTxtDate" style="color:Red;font-size:9px;font-weight:normal;display:none;">*</span>
                        </div>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                    <div class="form-group" style="margin-bottom: 7px;">
                        <label for="email" class="col-sm-3 padd linhei">
                            School:</label>
                        <div class="col-sm-9 padd">
                          <asp:DropDownList ID="ddlSchool"      runat="server" class="form-control " />
                        </div>
                    </div>
                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                               <asp:Button ID="btnApprove"   CssClass="btn btn-success pull-right " 
                                 ToolTip="Save" Text="  Back"   OnClick="btnApprove_Click" 
                                Style="margin-right: 5px; padding: 0px;" runat="server" />   
                                 <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-right"
                                        BackColor="#f5f5f5" ToolTip="Add"  OnClick="btnAddGkp_Click" ImageUrl="~/images/add-29-1.png" Visible="false" 
                                        Style="margin-right: 5px; padding: 0px;" runat="server" />

                                  
                                   
                                          <asp:ImageButton ID="ImageButton1" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                        ToolTip="Add" Visible="false" ImageUrl="~/images/add-29-1.png" OnClick="btnAdd_Click" Style="margin-right: 5px;
                                        padding: 0px;" runat="server" />
                                         <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" 
                                        OnClick="btnSerach_Click" class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1"
                                        ImageUrl="~/images/search-29.png" />

                                                                    
                 <asp:ImageButton ID="btnEdit"  ToolTip="Edit" OnClick="btnEdit_Click" runat="server" class="btn btn-danger btn-paddd pull-right"
                                                    BackColor="#f1f1f1" ImageUrl="~/images/edit.png" />                                 
                                                     </div>
                                </div>

                            </div>
                        </div>
                      
                  
                    </div>
                    <div class="row"> 
                  
                    <div class="col-lg-12 table table-hover " style="margin: 15px;" align="center">
                       <asp:Panel ID="pnlMain" runat="server">
<%--                                <asp:UpdatePanel runat="server" ID="UpdatedddddddPanel1">
                                                <ContentTemplate>--%>
                                            <div class="form-horizontal">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                    <div style=" height: 290px; overflow:auto;  width: 60%;" align="center">
                                                        <div>
                                                            <div class="Row" style="width: 100% ">
                                                                <asp:GridView ID="gvGkp" runat="server"  CssClass="table table-striped table-bordered table-hover" DataKeyNames="GUID_GKP"    AutoGenerateColumns="False"  Font-Names="Arial"
                                                                        Font-Size="12px" Width="100%" >
                                                                          <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
                                                                        </EmptyDataTemplate>
                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <Columns>
                                                                          <asp:TemplateField >
                                                                           <ItemTemplate>
                                                                             <asp:LinkButton ID="lbtn"   runat="server" Text="EDIT" OnClick="LnkBtnBlock_OnClick"  CommandArgument='<%# Bind("GUID_GKP") %>'  ></asp:LinkButton>
                                                                                <asp:Label ID="lblCUniqueChildCode" Visible="false" BackColor="Transparent" runat="server" Text='<%# Bind("GUID_GKP") %>' CssClass="form-controlAbhi"></asp:Label>
                                                                             </ItemTemplate>
                                                                              </asp:TemplateField>

                                                                                 <asp:TemplateField HeaderText="Action" Visible="false"  HeaderStyle-Width="15%" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImgAcc" runat="server"  OnClick="btn_Delete_Click" ImageUrl="~/images/delete-29.png"
                                                                Width="15px" Height="15px"></asp:ImageButton>
                                                            
                                                        </ItemTemplate>
                                                       <HeaderStyle Width="5%" />
                                                        <ItemStyle  HorizontalAlign="Center"/>
                                                    </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="SubjectName"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSubjectName" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("SubjectName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                    <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Level"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLevelID" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("LevelID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>

                                                                              <asp:TemplateField HeaderText="Session"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSession" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("Doc") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="TBorFC"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTBFC" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("TBFC") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="subjectid" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsubjectid"  ForeColor="Black" runat="server" Text='<%# Eval("SubjectID") %>'></asp:Label>
                                                                                    <asp:Label ID="lblgkp_fc"  ForeColor="Black" runat="server" Text='<%# Eval("gkp_fc") %>'></asp:Label>
                                                                                          <asp:Label ID="lblgkp_tb"  ForeColor="Black" runat="server" Text='<%# Eval("gkp_tb") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            
                                                                            
                                                                         
                                                                         
                                                                           
                                                                        </Columns>
                                                                        
                                                                    </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                          <%--  </ContentTemplate>
                                             <Triggers>
            <asp:PostBackTrigger ControlID="gvGkp" />
            </Triggers>
                                            </asp:UpdatePanel>--%>
                          </asp:Panel>
                      </div>
       
                </div>
            </div>


         
        </div>
              
 
            <asp:Label ID="lblGuId"  Visible="false" ForeColor="Black" runat="server" ></asp:Label>
            
          <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Hdn_model4"
            PopupControlID="pnlpopup4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>

        <asp:HiddenField ID="Hdn_model4" runat="server" />
           <asp:Panel ID="pnlpopup4" runat="server" Style="display: none;">
           <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header" style="height: 0px;">
                    <asp:ImageButton ID="ImageButton8" CssClass="btn btn-info pull-right"  OnClick="btnReset_Click" BackColor="#f5f5f5"
                            ToolTip="Add" ImageUrl="~/images/close-29.png"  Style="margin-right: 5px;
                            padding: 0px;" runat="server" />
                        <h4 class="modal-title">
                            Remarks</h4>
                        
                    </div>

                       <div class="row">

                        <div class="row marg search-bg">
               
                                <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                                    <div class="col-lg-8 col-md-8  col-sm-8 cpl-xs-12">
                                    <div class="form-group" style="margin-bottom: 2px;">
                                        <label for="email" class="col-sm-3 padd linhei">
                                            Remarks:</label>
                                        <div class="col-sm-9 padd">
                                           <asp:DropDownList ID="ddlRemark" runat="server" class="form-control">
                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Format not available</asp:ListItem>
                                            <asp:ListItem Value="2">Wrongly activity selected </asp:ListItem>
                                            <asp:ListItem Value="3">Typing error</asp:ListItem>

                                             <asp:ListItem Value="4">Counting error</asp:ListItem>
                                               <asp:ListItem Value="5">C Phone not available</asp:ListItem>
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


             <cc1:ModalPopupExtender ID="MpexdrDistrict8" runat="server" BackgroundCssClass="modalBg "
                    CancelControlID="CancelButton" PopupControlID="PnlDistrict8" TargetControlID="HdnFild8">
                </cc1:ModalPopupExtender>
                <asp:HiddenField ID="HdnFild8" runat="server"></asp:HiddenField>

                <asp:Panel cssclass="model-wid mod-posi"  Style="display: none;height:auto;width: 45% !important; margin-top: 220px !important;" ID="PnlDistrict8" runat="server">
                   
                    <div style="width:100%;height:auto;background-color:#f1f1f1">
                    <div class="modal-header"  style="background-color:#ddd;color:White;">
                    <h4 class="modal-title" style="ForeColor:White">GKP</h4>
                    </div>
                   <div class="modal-body">
                   <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                   <div class="form-horizontal" role="form">

                    <div class="form-group">
 
                      <asp:Label ID="Label12" class="control-label col-sm-4 lab-text-left" runat="server" Text="TBorFC"></asp:Label>
                                 <div class="col-sm-6 ">
                               <asp:RadioButtonList RepeatDirection="Horizontal" ForeColor="Black" ID="rblApprove" runat="server">
                                                      
                                                         <asp:ListItem Selected="True" Value="1">FC   </asp:ListItem>
                                                            <asp:ListItem  Value="2">TB</asp:ListItem>
                                                         
                                                        </asp:RadioButtonList>
                                        
                                   
                           </div>
                      </div>


                  <div class="form-group" id="statediv" runat="server">
 
                     <asp:Label ID="Label10" class="control-label col-sm-4 lab-text-left" runat="server" Text="Subject"></asp:Label>
                    <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlSubject" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" CssClass="form-control"
                                                            Font-Names="Verdana" Font-Size="11px" 
                                                            >
                                                        </asp:DropDownList>
                                        
                                     
                    </div>
                  </div>
 
 

                      <div class="form-group" id="blockdiv" runat="server">
 
                         <asp:Label ID="lblBlock" class="control-label col-sm-4 lab-text-left" runat="server" Text="Level"></asp:Label>
                        <div class="col-sm-6">
                                                                   <asp:DropDownList ID="ddlLevel"  AutoPostBack="true" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged" runat="server"  class="form-control">
      
                                                            </asp:DropDownList>
                                        
                        </div>
                      </div>



                              <div class="form-group">
 
                                 <asp:Label ID="Label11" class="control-label col-sm-4 lab-text-left" runat="server" Text="Session"></asp:Label>
                                <div class="col-sm-6">
                                                                      <asp:DropDownList ID="ddlSSession" runat="server" CssClass="form-control" Font-Names="Verdana"
                                                                        Font-Size="11px"  >
                                                                    </asp:DropDownList>
                                       
                                </div>
                              </div>

 
  
</div>

                  
                   </div>
                    <div class="modal-footer">
                     <asp:ImageButton ID="btnNewUserSave" OnClick="btnSave_Click"  ImageUrl="~/images/save-29-1.png"  runat="server"
                           ToolTip="Save"  Style="float: none;" ValidationGroup="validatemanageuser">
                            </asp:ImageButton>&nbsp;
                            <asp:ImageButton ID="CancelButton" ImageUrl="~/images/close-29.png" runat="server"  Text="Close"
                              ToolTip="Close" Style="float: none;"></asp:ImageButton></div>
                    </div>
                       
                       
                </asp:Panel>


             </ContentTemplate>
            </asp:UpdatePanel>

 
           


             
</asp:Content>
