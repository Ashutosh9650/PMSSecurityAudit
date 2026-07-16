<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SurveyLink.aspx.cs" Inherits="SurveyLink" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
  <style type="text/css">

      
.modalBackground{
         background-color:rgba(0,0,0,0.5);
     }

      
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
    height:auto !important;
    z-index:1350px0001 !important;
}
 .Mpopup1
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
    height:365px !important;
    z-index:1350px0001 !important;
}
     .Mpopupnewline{border-top:2px solid #105f77 ;width:100%; height:4px;}
     
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

  </style>

     <script type="text/javascript">
      function isNumberKey(txt, evt) {
            
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
         <div class="row" style="margin-top: 120px;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"  >
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-lg-4">
                                    <asp:Label ID="Label17" runat="server" class="text-danger" Style="font-weight: bold;
                                        font-size: larger">Change Sequence</asp:Label>
                                </div>
                                <div class="col-lg-2">
                                </div>
                                <div class="col-lg-4" runat="server" visible="false">
                                    <div class="pull-right">
                                      
                                    </div>
                                </div>
                                
                            </div>
                            <div class="row">
                                <div id="div-show-new">
                                    <div class="row marg search-bg">
                                        <div class="form-horizontal">
                                            <div class="col-lg-4 col-md-4 col-sm-4 cpl-xs-12" runat="server">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Assessment Type:
                                                    </label>
                                                    <div class="col-sm-8 padd">
                                                         <asp:DropDownList ID="ddlLevel" runat="server" TabIndex="1" CssClass="form-control input-sm" Style="margin-top: 3px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                                    </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="lblMastertype" class="col-sm-4 padd linhei"  style="padding-top: 2px;">
                                                        Survey
                                                    </label>
                                                    <div class="col-sm-7 padd">
                                                        <asp:DropDownList ID="ddlForm" runat="server" CssClass="form-control input-sm" Style="margin-top: 2px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlForm_SelectedIndexChanged">
                                </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                       
                                            <div class="col-lg-2 col-md-3 col-sm-3 cpl-xs-12" runat="server" >
                                                 <asp:LinkButton ID="LnkImport" class="btn btn-xs btn-primary" runat="server" OnClick="LnkImport_Click">
                                    <span class="glyphicon glyphicon-floppy-save"></span> Export
                                        </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div style="overflow: auto; margin-top: -5px; height: 350px;">
                                        
                                          <asp:GridView ID="GvQuestion" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display."
                        AllowSorting="True"  GridLines="Both"  OnRowDataBound="gvnroll_OnRowCommand" BorderColor="#e1e1e1" AlternatingRowStyle-BackColor="#F7F7F7"
                        DataKeyNames="QuestionID,QuestionNo,Question,QestionTypeID,Sequence,Flag,IsQuestionMandatory,MaxLenght,MaskValidation,UID,GroupID,QuestionType"
                         CssClass="table table-striped table-bordered table-condensed" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                        AllowPaging="false" ShowFooter="false">
                        <FooterStyle CssClass="DataGridFooter" />
                        <PagerStyle CssClass="paging" />
                        <HeaderStyle CssClass="DataGridHeader" />
                        <SelectedRowStyle BackColor="#D5D5BF" Font-Bold="True" />
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:TemplateField HeaderText="Q No">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuestionNo" runat="server" Text='<%#Bind("QuestionNo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="7%" CssClass="gvtextcenter" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Seq">
                                <ItemTemplate>
                                    <asp:Label ID="lblSequence" runat="server" Text='<%#Bind("Sequence") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="7%" CssClass="gvtextcenter" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Question">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuestion" runat="server" Text='<%#Bind("Question") %>'></asp:Label>
                                    <asp:Label ID="lblQuestionType" Visible="false" runat="server" Text='<%#Bind("QuestionType") %>'></asp:Label>
                                        <asp:Image ID="imgMKSG" runat="server" Height="80px" Width="100px" BorderColor="Black"
                                                            BorderStyle="Ridge" BorderWidth="1px" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Edit_Question" OnClick="Edit_Question_Click" class="btn btn-xs btn-info" runat="server">
                                                                    <span class="fa fa-pencil-square-o"> 
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="5%" CssClass="gvtextcenter" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonEdit" Visible='<%# Convert.ToInt32(Eval("QestionTypeID")) ==4 || Convert.ToInt32(Eval("QestionTypeID")) ==5 || Convert.ToInt32(Eval("QestionTypeID")) ==10 ? true : false %>'
                                                CommandArgument='<%#Eval("QestionTypeID") %>'
                                                
                                                runat="server" OnClick="update_Question_Click"  ><i class="fa fa-eye" aria-hidden="true"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" CssClass="gvtextcenter" />
                                    </asp:TemplateField>
                           
                            <asp:TemplateField HeaderStyle-Width="6%" HeaderText="UP">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkUp" CommandArgument="up" runat="server" Text="&#x25B2;"   CssClass="btn-link-new"  OnClick="ChangePreferenceUP" >
                                 <%--  <i class="fa fa-arrow-circle-o-up" aria-hidden="true"></i>--%>
                                        </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="6%" HeaderText="DOWN">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDown"  CommandArgument="down" runat="server" Text="&#x25BC;" CssClass="btn-link-new"  OnClick="ChangePreferenceDown" >
                                      <%--  <i class="fa fa-arrow-circle-o-down" aria-hidden="true"></i>--%>

                                    </asp:LinkButton>
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
            

            <asp:ModalPopupExtender ID="MPEFormName" BackgroundCssClass="modalBackground"
 runat="server" PopupControlID="pnlFormName" TargetControlID="HFFormName"  CancelControlID="lblFormNameClose">
</asp:ModalPopupExtender>


<asp:HiddenField ID="HFFormName" runat="server" />
<asp:HiddenField ID="HFFormId" runat="server" />
<asp:Panel ID="pnlFormName" runat="server" CssClass=" model-wid Mpopup1 mod-posi" Style="height: auto; width: 40% !important; display: none;">

<div style="border: 0px solid #ccc; width: 100%; min-height: 100px; margin: 0 auto;">
<div class="modal-header">
<asp:Label ID="lblFormName" runat="server" Text="Change Sequence " ></asp:Label>

 <asp:LinkButton ID="lblFormNameClose"  class="btn btn-xs btn-danger pull-right" 
                                                runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>
</div>
<div class="modal-body">
<div style="height:100px;">
     <div class="form-horizontal" role="form">

      <div class="form-group">
             
                     <%--   <div class="panel-heading" style="margin: auto; padding: 10px 15px;">
                            <div class="row">
                                <label class="font-weight-bold">Change Sequence : </label>
                                <span style="color: Red">*</span>
                                <asp:Button ID="btnSave" runat="server" Text="Save" Style="margin-top: 5px" CssClass="btn btn-success btn-sm pull-right"  />
                            </div>
                        </div>--%>

                        <div class="panel-body" style="height: 400px;">
                            <div class="form-group" style="float: left; width: 100%;" id="divquestion" runat="server" >
                                <label class="control-label">Question : <span style="color: Red">*</span></label>
                                <asp:TextBox ID="lblDependQuest" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                            </div>

                            <div class="form-group" style="float: left; width: 100%;" runat="server" id="DivPrsntSquence" >
                                <label class="control-label">Present Sequence : <span style="color: Red">*</span></label>
                                <asp:TextBox ID="txtprsntSequence" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                            </div>

                            <div class="form-group" style="float: left; width: 100%;" runat="server" id="DivEditSqunce" >
                                <label class="control-label">Edit Sequence : <span style="color: Red">*</span></label>
                                <asp:TextBox ID="txtEditSequence" onkeypress="return isNumberKey(this,event);" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2"  runat="server" ControlToValidate="txtEditSequence" Display="Dynamic" ErrorMessage="Please enter question no" ForeColor="Red" SetFocusOnError="True" ValidationGroup="QuestionCreate">
                                </asp:RequiredFieldValidator>
                            </div>
                        <div class="form-group" style="float: left; width: 100%;" runat="server" id="Div1" >
                              <asp:Button ID="btnSave" runat="server" Text="Save" Style="margin-top: 5px" OnClick="btnSave_Click" ValidationGroup="QuestionCreate" CssClass="btn btn-success btn-sm pull-right"  />

                            </div>
                       </div>
                   
                 </div>
     </div>

     

     </div>
</div>
</div>
   
<div class="modal-footer">
                                    
   </div>
 </asp:Panel>


              <div>
        <asp:ModalPopupExtender ID="ModalAlert" runat="server" TargetControlID="hdn_alertmodal"
            PopupControlID="pnl_alert" CancelControlID="btn_cancelalert"  BackgroundCssClass="modalBackground">
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
        <asp:Button ID="DoNothing" runat="server" Text="" Style="display: none" />
    </div>
                   <asp:HiddenField ID="hdnNrmlquestionid" Value="" runat="server" />
                                        <asp:HiddenField ID="hdnQuestionbankid" Value="" runat="server" />



        </ContentTemplate>
           <Triggers>
            <asp:PostBackTrigger ControlID="LnkImport" />
     
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

