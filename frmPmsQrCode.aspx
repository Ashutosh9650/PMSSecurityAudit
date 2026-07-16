<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="frmPmsQrCode.aspx.cs" Inherits="frmPmsQrCode" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="js/jquery-2.1.0.js"></script>
    <script src="js/Common.js" type="text/javascript"></script>
    <style type="text/css">
        .ajax__calendar_container {
            z-index: 100004;
        }
    </style>
  
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="panel panel-default " style="background-color: #f5f5f5; margin-bottom: 0px !important;">
                    <div class="panel-heading" style="padding: 5px 10px;">
                        <div class="row">

                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                <div id="Div" class="col-lg-10 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                    <div class="">
                                        <h3 class="text-danger" style="margin: 0px;">Generate QR Code
                                        </h3>
                                    </div>
                                </div>
                                 <div class="col-lg-2 col-md-2 col-sm-3 cpl-xs-12" runat="server">

                                   
                                    <asp:LinkButton ID="btndownload" runat="server" Text="Download PDF" OnClick="btnDownload_Click"
                                        class="pull-right" Style="margin-right: 15px;"></asp:LinkButton>

                                </div>
                            </div>
                            <%--<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                <div id="Div2" class="col-lg-10 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                    <div class="">
                                        <h3 class="text-danger" style="margin: 0px;">
                                            Report
                                        </h3>
                                    </div>
                                </div>
                                <div id="Div3" class="col-lg-2 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                    <div class="form-group">
                                        <asp:LinkButton ID="LinkButton1" Visible="false" runat="server" Text="Export to Excel"
                                            class="pull-right"></asp:LinkButton>
                                        <%--</div>
                                         
                                           <span class="pull-right" style="font-size: 17px;"></span>
                                        <div id="Div1" class="col-lg-2 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                        <div class="form-group">
                                        <asp:LinkButton ID="lnkCSV" runat="server" Text="Export to CSV" ></asp:LinkButton>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class=" col-lg-3 col-md-3 col-sm-3 col-xs-12 text-left" style="padding-right: 0px; margin-top: 10px;">
            <div style="overflow: auto; margin-top: 0px; height: 770px;">
                <div class="thumbnail" style="height: 750PX;">
                  <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="40"
                                OnRowCommand="GVMain_OnRowCommand" OnPageIndexChanging="GV_Project_PageIndexChanging"     BorderStyle="None" DataKeyNames="DISECode,Name,Schoolcode" GridLines="None" AutoGenerateColumns="false"
                                    CssClass="table table-striped">
                                    <EmptyDataTemplate>
                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                            Data not found
                                        </div>
                                    </EmptyDataTemplate>
                                    <FooterStyle CssClass="FooterStyle" />
                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="44px" />
                                    <RowStyle HorizontalAlign="Left" />
                                    <PagerStyle CssClass="paging" />
                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                              
                                    <Columns>
                                          
                                        <asp:ButtonField HeaderText="GKP School List" ItemStyle-ForeColor="#333" DataTextField="SchooName"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                      
                                    </Columns>
                                </asp:GridView>
                </div>
            </div>
        </div>
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-12" style="padding-left: 10px; margin-top: 10px;">
            <div class="thumbnail" style="background-color: white; float: left; width: 100%;height: 750PX;">
                
                    <div class="form-horizontal">
                        <div class="row">

                            <asp:HiddenField ID="hdnbtnValue" runat="server" />
                            <div id="div-show" style="display: block; float: right; width: calc(100% - 20px); margin: 0px 10px; position: relative; top:0px;">
                                <div class="row marg search-bg">
                                    <div class="form-horizontal">
                                        <div class="row">
                                        
                                                    <div class="row">
                                                        <div class="col-sm-2 " style="display:none;">
                                                            <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                                Year</label>
                                                            <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                                AutoPostBack="true" class="form-control ">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlTpye" Visible="false" OnSelectedIndexChanged="ddlTpye_SelectedIndexChanged"
                                                                AutoPostBack="true" runat="server" class="form-control">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="District Level" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Village Level" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="School Level" Value="3"></asp:ListItem>
                                                            </asp:DropDownList>
                                                         
                                                            <asp:DropDownList ID="ddlGender"  Visible="false" runat="server" class="form-control ">
                                                            </asp:DropDownList>
                                                            
                                                            <asp:DropDownList ID="ddlGroup"  Visible="false" runat="server" class="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2 ">
                                                            <label for="email" class="padd linhei" style="padding-top: 1px;">
                                                                State</label>
                                                            <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                                    AutoPostBack="true" class="form-control "/>
                                                        </div>
                                                        <div class="col-sm-3 " style="margin-bottom: 15px;">
                                                            <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                                District</label>
                                                               <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                                    AutoPostBack="true" class="form-control " />
                                                        </div>
                                                        <div class="col-sm-3 ">
                                                            <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                                Block</label>
                                                                 <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                                    class="form-control " />
                                                        </div>
                                                        <div class="col-sm-3 ">
                                                            <label for="email" class="padd linhei" style="padding-top: 2px;">
                                                                Cluster</label>

                                                               <asp:DropDownList ID="ddlCLuster" class="form-control " runat="server" ></asp:DropDownList>
                                                            
                                                        </div>

                                                          <div class="col-sm-1 ">
                                                             

                                                               <asp:LinkButton ID="imNewSerach" OnClick="btnSerach_Click" class="btn btn-Sm btn-primary pull-right"
                                                    style="margin-top: 28px;" runat="server">Search</asp:LinkButton>
                             
                                                            
                                                        </div>
                                                        
                                                    </div>
                                            
                                                 
                                        <%--      <div><div id="Div1" class="col-sm-2 "   runat="server">
                                                            
                                                    

                                                                 <div>
                                                                     


                                                                 </div>
                                                              
                                                           
                                                        </div></div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 table table-hover " style="padding: 0px;">
          
                                    <div class="form-horizontal">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 10px 10px 10px;">
                                    
                                            <asp:Label ID="lblTotalCount" Visible="false" ForeColor="#737272" Font-Bold="true"
                                                runat="server"></asp:Label>
                                            <div style="height: 450px; overflow: auto; width: 99%;" align="center">
                                                <div>
                                                    <div class="row" style="width: 100%">
                                                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                            <div class="row">
                                                                <div class="col-sm-12" style="    padding-top: 15px;    padding-bottom: 15px;">
                                                       <asp:Image ID="imgMKS" Visible="false" runat="server" Height="300px" Width="300px" BorderColor="Black"
                                                                                    BorderStyle="Ridge" BorderWidth="1px" />
                                                            </div>
                                                        <div class="col-sm-12">
                                                            <center>
                                                                <p class="mb-0"><asp:Label Visible="false" ID="lblSchhol" runat="server" ></asp:Label> </p>
                                                                <br /><p class="mb-0"<asp:Label Visible="false" ID="lblDisecode" runat="server" ></asp:Label></p>
                                                            </center>
                                                            <asp:Label Visible="false" ID="lblDisecode1" runat="server" />
                                                                <asp:Label Visible="false" ID="lblSchhol1" runat="server"/>
                                                        </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 text-center" style="    height: 310px;">    <div style="    display: flex;    justify-content: center;    align-items: center;    height: 100%;">
                                                        
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
        </div>
    </div>
            
            </ContentTemplate>
          <Triggers>
            <asp:PostBackTrigger ControlID="btndownload" />
           
        </Triggers>
         </asp:UpdatePanel>
    <ajax:ModalPopupExtender ID="MpexdrPopUp" runat="server" BackgroundCssClass="modalBackground "
        PopupControlID="PnlDistrict" CancelControlID="CancelButton" TargetControlID="HdnFild7">
    </ajax:ModalPopupExtender>
    <asp:HiddenField ID="HdnFild7" runat="server"></asp:HiddenField>
    <asp:Panel CssClass="model-wid mod-posi" Style="display: none; width: 76% !important; margin-top: 93px !important;"
        ID="PnlDistrict" runat="server">
        <div style="width: 100%; height: auto; background-color: #f1f1f1">
            <div class="modal-header" style="background-color: #3ac0f2; color: White;">
                <asp:Label ID="lblMsg" runat="server" Visible="false" ForeColor="Black" Font-Names="Verdana"
                    Font-Size="11px"></asp:Label>
                <asp:ImageButton ID="CancelButton" ImageUrl="~/images/close-29.png" Style="float: right;"
                    Width="3%" Height="3%" runat="server" />
            </div>
            <div class="modal-body">
                <div class="form-horizontal">
                  
                    <div style="height: 350px; overflow: auto; width: 99%;" align="center">
                        <div>
                            <div class="Row" style="width: 100%">
                            
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--  <div class="modal-footer">
                                                    <asp:Button ID="CancelButton" runat="server" CssClass="btn bgm-cyan" Text="Close"
                                                        ToolTip="Close" Style="float: none;"></asp:Button></div>--%>
        </div>
    </asp:Panel>

</asp:Content>
