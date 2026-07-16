<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" MaintainScrollPositionOnPostback="true"  AutoEventWireup="true" CodeFile="FrmAddMasterCommon.aspx.cs" Inherits="FrmAddMasterCommon" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
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

         function Search_Gridview3(strKey, strGV) {
             debugger;

             var strData = strKey.value.toLowerCase().split(" ");
             var tblData = document.getElementById("ctl00_MainContent_GVFlagMaster");
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
        
 .Mpopup
{
    position: relative;
    background: #f2f2f2;
    color: #404040;
    text-shadow: 0 1px 0 #fff;
    -ms-filter: "progid:DXImageTransform.Microsoft.dropshadow(OffX=0,OffY=1,Color=#ffffffff,Positive=true)";
    filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=0, OffY=1, Color=#ffffffff, Positive=true);
    border-radius:5px;
    box-shadow: 0px 1px 4px rgba(0, 0, 0, 0.1);
    padding:5px;
    font-size: 12px;
}
     .Mpopupnewline{border-top:2px solid #105f77 ;width:100%; height:4px;}
     .modalBackground{
         background-color:rgba(0,0,0,0.5);
     }
     .Mpopupheader 
     {
         width:100%; 
         background-color:#454545; 
         height:25px; 
         font-size: 12px;
    font-weight:500;
    color: #f2f2f2;
    text-shadow: 0 1px 0 #add553;
    -ms-filter: "progid:DXImageTransform.Microsoft.dropshadow(OffX=0,OffY=1,Color=#ffffffff,Positive=true)";
    filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=0, OffY=1, Color=#ffffffff, Positive=true);
         padding:5px;
         
     }
     
     .Mpopupbodycontent{ width:100%;margin:3px 0 3px 0}
     
     .Mpopupfooter{ width:100%;background-color:#454545;padding:3px}
     
     .Requiredvalidate{ font-size:12px; color:Red;}
      .mod-posi {
            position: fixed !important;
            top: 5% !important;
        }


.ModalPopupBG
{
background-color:#000000;
filter:alpha(opacity=80);
-moz-opacity:0.5;
-khtml-opacity: 0.5;
opacity: 0.5;
width :100%;
height:100%
}

.ModalPopupBGmainentry
{
background-color:#000000;
filter:alpha(opacity=10);
-moz-opacity:1.0;
-khtml-opacity:1.0;
opacity:1.0;
width :100%;
height:100%
}


/*************************
Header Start
************************/

		
.HeaderBG
{
background:url(../images/Adminimages/header_bg.jpg) repeat-x;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:UpdatePanel ID="updatePnlX" runat="server">
            <ContentTemplate>
            
     <div class="col-lg-12" >
        <div class="panel panel-default" style="padding-bottom: 5px !important;">
            <div class="panel-heading" style="height:36px" >
                <p class="text-danger" style="margin: 3px;">
                  

                     <asp:LinkButton ID="GoBackToQuesionForm"  class="btn btn-xs btn-primary pull-right"  PostBackUrl="~/SurveyQuestion.aspx"
         runat="server"><span class="glyphicon glyphicon-arrow-left"></span> Go Back To Question Form</asp:LinkButton>
                </p>
                 <p class="text-danger" style="margin: 3px;">
                     </p>
            </div>


           
           


            <div class="panel-body" style="min-height: 50px; margin-bottom: -25px;">
            <div id="Div1" class="col-lg-12 col-md-12 col-xs-12">
                <p>
                    <asp:Label ID="lblheading" runat="server" Text=""></asp:Label>
                </p>
            </div>
            </div>
            <div class="panel-body" style="min-height: 500px; margin-bottom: -25px;">
                <div id="Project" class="col-lg-6 col-md-6 col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <p class="text-danger" style="margin: 0px;">
                                Choices

                                 <asp:LinkButton ID="LbFlag"  class="btn btn-xs btn-primary pull-right" OnClick="LbFlag_Click"
                   runat="server"><span class="glyphicon glyphicon-plus"></span> Add Flag</asp:LinkButton>
                            </p>
                        </div>
                           <div class="form-group">
         <label class="control-label col-lg-1  col-sm-12" style="text-align: left;">
   Search</label>
              <div class="col-lg-9  col-sm-12">
                     <asp:TextBox ID="Txt_VillageDTD" style="width:350px;margin-left: 14px;" Width="350px" onkeyup="Search_Gridview3(this, 'GVFlagMaster')" runat="server" class="form-control" />
   
              </div>
     </div>
                        
                        
                                      
                        <div class="panel-body" style="min-height: 404px; max-height: 404px; width: 100%;
                            overflow-y: scroll;" class="scroll">
                            <asp:GridView ID="GVFlagMaster" Width="100%" runat="server" DataKeyNames="UID,ID,Value"
                                AutoGenerateColumns="False" CellPadding='3' CellSpacing="2" 
                                AllowSorting="True" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                GridLines="none" CssClass="table table-striped table table-hover table-bordered Grid"
                                SelectedRowStyle-BackColor="#e1f4a6" >
                                <Columns>
                                  
                                    <asp:TemplateField HeaderText="ID">
                                       <ItemStyle Width="5%" CssClass="GridHD" />
                                       <ItemTemplate>
                                                    <%--<asp:Label ID="lbl_ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>--%>
                                            <%#Container.DataItemIndex+1 %>.
                                       </ItemTemplate>
                                
                                     </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Value">
                                       <ItemStyle Width="45%" CssClass="GridHD" />
                                       <ItemTemplate>
                                                    <asp:Label ID="lbl_Value" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                               </ItemTemplate>
                                  
                                     </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="EditCategory"   OnClick="EditCategory_Click" ToolTip="Edit"
                                                runat="server"><span class="glyphicon glyphicon-edit"></span> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Define Options">
                                        <ItemTemplate>
                                             <asp:LinkButton ID="Define_Options" OnClick="Define_Options_Click" class="btn btn-xs btn-primary" ToolTip="Define options"
                                                runat="server"><span class=""></span> Define Options</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                               
                                                                     <asp:LinkButton ID="DeleteFlags" OnClientClick="javascript:return confirm('Are you sure you want to delete this record?');" OnClick="DeleteFlags_Click"  class="btn btn-sm btn-warning" runat="server">
                                                                     <span class="glyphicon glyphicon-trash"></span> 
                                                                     </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" CssClass="gvtextcenter" />
                                     </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div id="Activity" class="col-lg-6 col-md-6 col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <p class="text-danger" style="margin: 0px;">
                                 
                                <asp:Label ID="lblHeadingTwo" runat="server" Text="Options of Choices"></asp:Label>  </p>
                        </div>
                        <div class="col-sm-12">
                            <div style="display:flex;justify-content: space-between;gap: 12px;margin-top:10px">
                                <div>
                                    <label>Values</label>
                                <asp:TextBox ID="txtFlagMasterValue" style="height: 150px !important;" runat="server" CssClass="form-control"  TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                                        <br />
                                        <span style=" color:Maroon ">The ';' Will be conisederd as Delimiter So the value will go in next Line </span>
                                    </div>
                                 <div>
                                <label>Score</label>
                                 <asp:TextBox ID="txtScore" runat="server" onkeypress="return isNumberKey(this,event);" CssClass="form-control"  MaxLength="2">

                                        </asp:TextBox></div>
                                 <div style="margin-top:22px;">
                                  <asp:LinkButton ID="Save_Options" class="btn btn-xs btn-primary" ToolTip="Save"
                                           OnClick="Save_Options_click"     runat="server"><span class="glyphicon glyphicon-save" ></span> Save</asp:LinkButton></div>
                            </div>
                        </div>
                        <div>
                        <div class="panel-body scroll" style="min-height: 404px; max-height: 404px;  overflow-y: scroll; " >
                          
                            <asp:HiddenField ID="HFFlagValue" runat="server" />
                            <asp:GridView ID="GVFlagMasterValue" Width="100%" runat="server" DataKeyNames="UID,ID,Value,Score" ShowFooter="true"  ShowHeaderWhenEmpty="true"
                                AutoGenerateColumns="False" CellPadding='3' CellSpacing="2" AllowSorting="True"  EmptyDataText="No Record Found"
                                GridLines="none" CssClass="table table-striped table table-hover table-bordered Grid"
                                AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                SelectedRowStyle-BackColor="#e1f4a6"    >
                                <Columns>
                                    <asp:TemplateField HeaderText="ID">
                                       <ItemStyle Width="5%" CssClass="GridHD" />
                                       <ItemTemplate>
                                             
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                       </ItemTemplate>
                                    <FooterTemplate>
                                                   
                                     </FooterTemplate>
                                     </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value">
                                     
                                       <ItemTemplate>
                                                    <asp:Label ID="lbl_Value" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                                       </ItemTemplate>
                                    <%--<FooterTemplate>
                                        <asp:TextBox ID="txtFlagMasterValue" style="height: 150px !important;" runat="server" CssClass="form-control"  TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                                        <br />
                                        <span style=" color:Maroon ">The ';' Will be conisederd as Delimiter So the value will go in next Line </span>

                                     </FooterTemplate>--%>
                                     </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Score">
                                      
                                       <ItemTemplate>
                                                    <asp:Label ID="lbl_Score" runat="server" Text='<%# Eval("Score") %>'></asp:Label>
                                       </ItemTemplate>
                                      <%--   <FooterTemplate>
                                        <asp:TextBox ID="txtScore" runat="server" onkeypress="return isNumberKey(this,event);" CssClass="form-control"  MaxLength="2">

                                        </asp:TextBox>
                                      

                                     </FooterTemplate>--%>

                                         </asp:TemplateField>
                                     

                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemStyle Width="8%" CssClass="GridHD" />
                                        <ItemTemplate>
                                           <asp:LinkButton ID="EditOptionValue"  class="btn btn-xs btn-success" OnClick="EditOptionValue_Click" ToolTip="Edit"
                                                runat="server"><span class="glyphicon glyphicon-edit"></span> </asp:LinkButton>


                                        </ItemTemplate>
                                       <%-- <FooterTemplate>
                                                 <asp:LinkButton ID="Save_Options" class="btn btn-xs btn-primary" ToolTip="Save"
                                                runat="server"><span class="glyphicon glyphicon-save"></span> Save</asp:LinkButton>
                                     </FooterTemplate>--%>
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                               
                                                                     <asp:LinkButton ID="DeleteOption" OnClientClick="javascript:return confirm('Are you sure you want to delete this record?');" OnClick="DeleteOption_Click"  class="btn btn-sm btn-warning" runat="server">
                                                                     <span class="glyphicon glyphicon-trash"></span> 
                                                                     </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" CssClass="gvtextcenter" />
                                      </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>


            
<asp:Panel ID="pnlFormName" runat="server" CssClass=" model-wid Mpopup mod-posi" Style="height: auto; width: 40% !important; display: none;">

<div style="border: 0px solid #ccc; width: 100%; min-height: 100px; margin: 0 auto;">
<div class="modal-header">
<asp:Label ID="lblFormName" runat="server" Text="" ></asp:Label>

 <asp:LinkButton ID="lblFormNameClose"  class="btn btn-xs btn-danger pull-right" 
                                                runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>
</div>
<div class="modal-body">
<div style="height:100px;">
     <div class="form-horizontal" role="form">

      <div class="form-group">
         <label class="control-label col-lg-3  col-sm-12" style="text-align: left;">
          Survey Name :</label>
              <div class="col-lg-8  col-sm-12">
                      <asp:DropDownList ID="DDLFormName" runat="server" Enabled="false"    CssClass="form-control" >
                      </asp:DropDownList>
                   
                 </div>
     </div>

     <div class="form-group">
         <label class="control-label col-lg-3  col-sm-12" style="text-align: left;">
          Flag Name :<span style="color: Red">*</span></label>
              <div class="col-lg-8  col-sm-12">
                       <asp:TextBox ID="txtFlagName" class="form-control" runat="server"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqFlagName" runat="server" ControlToValidate="txtFlagName"
                                            ErrorMessage="Enter Name" ValidationGroup="rex" Display="None">
                          </asp:RequiredFieldValidator>
              </div>
     </div>

     </div>
</div>
</div>
<div class="modal-footer">
<asp:Button ID="btnFormName" runat="server"  CssClass="btn btn-xs btn-primary" Text="Save" OnClick="btnFormName_Click"  ValidationGroup="rex"  />
</div>
</div>


</asp:Panel>

<asp:ModalPopupExtender ID="MPEFormName" BackgroundCssClass="modalBackground"
 runat="server" PopupControlID="pnlFormName" TargetControlID="HFFormName"  CancelControlID="lblFormNameClose">
</asp:ModalPopupExtender>


<asp:HiddenField ID="HFFormName" runat="server" />
<asp:HiddenField ID="HFFormId" runat="server" />



<asp:Panel ID="PnlFlagOption" runat="server" CssClass=" model-wid Mpopup mod-posi" Style="height: auto; width: 40% !important; display: none;">

<div style="border: 0px solid #ccc; width: 100%; min-height: 100px; margin: 0 auto;">
<div class="modal-header">
<asp:Label ID="lblFlagOption" runat="server" Text="" ></asp:Label>

 <asp:LinkButton ID="LnbFlagOption"  class="btn btn-xs btn-danger pull-right" 
                                                runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>
</div>
<div class="modal-body">
<div style="height:100px;">
     <div class="form-horizontal" role="form">



     <div class="form-group">
         <label class="control-label col-lg-3  col-sm-12" style="text-align: left;">
          Flag Option Name :<span style="color: Red">*</span></label>
              <div class="col-lg-8  col-sm-12">
                       <asp:TextBox ID="txtFlagOption" class="form-control" runat="server"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFlagOption"
                                            ErrorMessage="Enter Option Value" ValidationGroup="rexFlagOption" Display="None">
                          </asp:RequiredFieldValidator>
              </div>
     </div>
         
     <div class="form-group">
         <label class="control-label col-lg-3  col-sm-12" style="text-align: left;">
          Score :<span style="color: Red">*</span></label>
              <div class="col-lg-8  col-sm-12">
                       <asp:TextBox ID="txtEditScore" class="form-control" runat="server"></asp:TextBox>
                     
              </div>
     </div>

     </div>
</div>
</div>
<div class="modal-footer">
<asp:Button ID="BtnFlagOption" runat="server"  CssClass="btn btn-success" Text="Update" OnClick="BtnFlagOption_Click"  ValidationGroup="rexrexFlagOption"  />
</div>
</div>


</asp:Panel>

<asp:ModalPopupExtender ID="MPFFlagOption" BackgroundCssClass="modalBackground"
 runat="server" PopupControlID="PnlFlagOption" TargetControlID="HFFlagOption"  CancelControlID="LnbFlagOption">
</asp:ModalPopupExtender>

<asp:HiddenField ID="HFFlagOption" runat="server" />

<asp:HiddenField ID="HFFlagOptionValueUID" runat="server" />

            
        </div>
    </div>
   
   <asp:ModalPopupExtender ID="MPEaddOrganization" BackgroundCssClass="modalBackground"
        runat="server" PopupControlID="PaneladdOrganization" TargetControlID="HFaddOrganization"
        CancelControlID="lbladdOrganization">
    </asp:ModalPopupExtender>
    <asp:HiddenField ID="HFaddOrganization" runat="server" />
    <asp:Panel ID="PaneladdOrganization" runat="server" CssClass=" model-wid Mpopup mod-posi"
        Style="height: auto; display: none; width: 35% !important;">
        <div style="border: 0px solid #ccc; width: 100%; min-height: 100px; margin: 0 auto;">
            <div class="modal-header">
                <asp:Label ID="Label1" runat="server" Text="Enter New Panchayat Activity"></asp:Label>
                <span style="float: right">
                    <asp:Label ID="lbladdOrganization" runat="server" Text="Close[X]" Style="cursor: pointer"></asp:Label></span>
            </div>
            <div class="modal-body">
                <div style="height: 105px; overflow-y: auto;">
                    <div class="form-group">
                        <label class="control-label col-lg-3  col-sm-12" style="text-align: left;">
                            Project :</label>
                        <div class="col-lg-9  col-sm-12">
                            <asp:Label ID="lblDispProject" runat="server" class="form-control"></asp:Label>
                        </div>
                    </div>
                    <div class=" form-group " style="height: 20px;">
                    </div>
                    <div class="form-group">
                        <label class="control-label col-lg-3  col-sm-12" style="text-align: left;">
                            Activity :<span style="color: Red">*</span></label>
                        <div class="col-lg-9  col-sm-12">
                            <asp:TextBox ID="txtActivity" runat="server" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtActivity"
                                ErrorMessage="Enter the Organization name " ValidationGroup="VAlorg">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnSaveActivity" CssClass="btn btn-danger" runat="server" Text="Save"
                    ValidationGroup="VAlorg" />
            </div>
        </div>
    </asp:Panel>

    <asp:ModalPopupExtender ID="ModalAlert" runat="server" TargetControlID="hdn_alertmodal"
        PopupControlID="pnl_alert" BehaviorID="popup" CancelControlID="btn_cancelalert"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnl_alert" runat="server" Style="display: none;" class="Mpopup" Width="345px">
        <div style="padding: 0 0 10px 0;">
            <div class="Mpopupheader" align="center">
                Message
            </div>
            <div style="width: 332px; text-align: center" class="Mpopupbodycontent">
                <div style="width: 100%; height: 8px;">
                </div>
                <asp:Label ID="lbl_messages" runat="server" CssClass="LabelHeader" Font-Bold="True"
                    Font-Size="11pt" Width="316px"></asp:Label>
                <div style="width: 100%; height: 8px;">
                </div>
            </div>
            <div style="text-align: center;" align="center">
                <asp:Button ID="btn_cancelalert" runat="server" CssClass="butt-new" Text="  OK  "
                    Width="74px" /></div>
        </div>
        <div class="Mpopupfooter" align="right">
        </div>
    </asp:Panel>
   
    <asp:HiddenField ID="hdn_alertmodal" runat="server" />
   
    <asp:HiddenField ID="HFDispProjectID" runat="server" />

   </div>
         </div>

     </ContentTemplate>
 </asp:UpdatePanel>
         


</asp:Content>

