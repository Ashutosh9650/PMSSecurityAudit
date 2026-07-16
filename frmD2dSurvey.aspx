<%@ Page Language="C#" AutoEventWireup="true"  Culture="en-GB" CodeFile="frmD2dSurvey.aspx.cs" MasterPageFile="~/Site.master"
    Inherits="frmD2dSurvey" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
  
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
$(document).ready(function() {
     $('#ddlCars').multiselect();
      $('#ddlCars1').multiselect({ 
         numberDisplayed: 2
          
     });
       $('#ddlCars2').multiselect({ 
         includeSelectAllOption: true,
           enableFiltering:true         
           
     });
        $('#ddlCars3').multiselect({  
           nonSelectedText :'Select Cars'
           
     });
});
</script>
   <script type="text/javascript">
$(document).ready(function() {
     $('#ddlCars').multiselect();
      $('#ddlCars1').multiselect({ 
         numberDisplayed: 2
          
     });
       $('#ddlCars2').multiselect({ 
         includeSelectAllOption: true,
           enableFiltering:true         
           
     });
        $('#ddlCars3').multiselect({  
           nonSelectedText :'Select Cars'
           
     });
});
</script>

  <script language="Javascript" type="text/javascript">

      function onlyAlphabets(e, t) {
          try {
              if (window.event) {
                  var charCode = window.event.keyCode;
              }
              else if (e) {
                  var charCode = e.which;
              }
              else { return true; }
              if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 0 || charCode == 127 || charCode == 32 || charCode == 08 || charCode == 09 || charCode == 13)
                  return true;
              else
                  return false;
          }
          catch (err) {
              alert(err.Description);
          }
      }

 
    </script>

     <script language="Javascript" type="text/javascript">

         function onlyAlphabetsAdd(e, t) {
             try {
                 if (window.event) {
                     var charCode = window.event.keyCode;
                 }
                 else if (e) {
                     var charCode = e.which;
                 }
                 else { return true; }
                 if ((charCode > 64 && charCode < 91) || (charCode > 48 && charCode < 57) || (charCode > 96 && charCode < 123) || charCode == 32 || charCode == 0 || charCode == 9 || charCode == 08 || charCode == 44 || charCode == 45 || charCode == 48)
                     return true;
                 else
                     return false;
             }
             catch (err) {
                 alert(err.Description);
             }
         }


         function onlyAlphabetsHH(e, t) {
             try {

               
                 if (window.event) {
                     var charCode = window.event.keyCode;
                 }
                 else if (e) {
                     var charCode = e.which;
                 }
                 else { return true; }
                 if ((charCode > 64 && charCode < 91) || (charCode > 48 && charCode < 57) || (charCode > 96 && charCode < 123) || charCode == 32  || charCode == 32 || charCode == 0 || charCode == 9)
                     return true;
                 else
                     return false;

             }
             catch (err) {
                 alert(err.Description);
             }
         }
 
    </script>
<script type="text/javascript">


    function isNumberKey(txt, evt) {
        debugger;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode == 46) {
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
  
  

    <script  type="text/javascript">
   
    
        function phonenumber(inputtxt, txtid) {
            var phoneno = /^\d{10}$/;
            if (phoneno.test(inputtxt) && inputtxt.length == 10) {
                $("." + txtid).css("border", "solid 1px green")
                return true;
            }
            else {
                $("." + txtid).css("border", "solid 1px red")
                $("." + txtid).val('');
                alert("Mobile No. should be 10 digit");

                return false;
            }
        }  
    
    </script>
    <script type="text/javascript">

        function Valdation(txtcls, txtaBoy) {
            var Eboy = 0;
            var Aboy = 0;
            $("." + txtcls).each(function (index, value) {
                if ($.trim($(this).val()) != "")
                    if (!isNaN($(this).val()))

                        Eboy = parseFloat($("." + txtaBoy).val());
                Aboy = parseFloat($("." + txtcls).val());
               
                if (Aboy < Eboy) {

                    alert("Enrollment  should be higher or equal to Appeared");
                    $("." + txtcls).focus();
                    $("." + txtaBoy).val('');
                    return true;
                }
                else {
                    return true;
                }

            });

                     
                  
           
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
   <script type="text/javascript">

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

       function arrivaldate(arrivaldate)
        {

            var arrivaldate = $('#' + arrivaldate).val();
           
            var today = new Date();
            alert(arrivaldate);
            alert(today.getDate());
           if (arrivaldate > today.getDate())
              {
               alert("Should not be future date.");
               document.getElementById("" + sender + "").value = null;
               return false;
             }


       }

       function checkDate(arrivaldate) {
           var EnteredDate = $('#' + arrivaldate).val();
           
           var date = EnteredDate.substring(0, 2);
         
           var month = EnteredDate.substring(3, 5);
           var year = EnteredDate.substring(6, 10);

           var myDate = new Date(year, month - 1, date);

           var today = new Date();
           
           if (myDate > today) {
               alert("Should not be future date.");
               $('#' + arrivaldate).val= '';
           }

       }


       function checkPwd(str) {

           
           var msg = "";
          if (str.search(/\d/) == -1) {

              msg += 'Please enter atleast one number'; // for numeric
           } 

           if (msg != "") {
               document.getElementById('<%=txtHouse.ClientID %>').value = "";
               
               alert(msg);
               return false;
           }
           else { return true; }
       }
    </script>
     <style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server" >
 <asp:UpdatePanel runat="server" ID="mkiiainpnl121">
        <ContentTemplate>
  
    <div class="container-fluid" >

<%--<input type="image" id="ton-new" class="butt" src="Images/close.png"  />
       <div id="div-show-new"></div> --%>       
  </div>       
      
           <div class="container-fluid" style="margin-top: 0px;">
           		         
        <div class="row" >
        <div class="col-lg-2 col-md-2 col-sm-3" style="padding-right: 0px;" >
       <div class="thumbnail" style="min-height:750px;width: 225px;"> 
        
       <div>
        <asp:TextBox ID="txtSearchName" OnTextChanged="txtSearchName_Click" AutoPostBack="true" runat="server"  class="form-control"></asp:TextBox> 
      <%-- <div class="col-lg-12 col-md-12 col-sm-12  col-xs-12"  > 
          </div>--%>
               <div style="overflow: auto; margin-top:35px; height:750px; ">
                 <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="40" BorderStyle="None" DataKeyNames="UniqueCode"
                GridLines="None" AutoGenerateColumns="false" OnPageIndexChanging="GV_Project_PageIndexChanging" OnRowCommand="GVMain_OnRowCommand" >
                <EmptyDataTemplate>
                    <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                        Data not found
                    </div>
                </EmptyDataTemplate>
                <FooterStyle CssClass="FooterStyle" />
                       <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="44px"   />
                    <RowStyle HorizontalAlign="Left"/>
                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                    <AlternatingRowStyle BackColor="#f1f1f1" />
                   <PagerStyle CssClass="paging" />
                <%-- <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                    <AlternatingRowStyle BackColor="#f1f1f1" />--%>

                <Columns>

                    <asp:ButtonField HeaderText="HHNo" ItemStyle-ForeColor="#333" DataTextField="HHNo" CommandName="GVUIO">
                    <ItemStyle CssClass="padding-lef" Height="30px" />

                      <HeaderStyle CssClass="padding-lef" />
                    </asp:ButtonField>
                     
                    <asp:ButtonField HeaderText="Child Name"    ItemStyle-ForeColor="#333" DataTextField="ChildName" CommandName="GVUIO">
                    
                     <ItemStyle CssClass="padding-lef" Height="30px" />

                      <HeaderStyle CssClass="padding-lef" />
                    </asp:ButtonField>
                    
                    <asp:ButtonField HeaderText="Name" Visible="false" Text="Button" DataTextField="UniqueCode"></asp:ButtonField>

                </Columns>

            </asp:GridView>
            <%--</div>--%>
            </div>            
                 </div> 
       </div>
       </div>
       
     
          
       <div  class="col-lg-10 col-md-10 col-sm-9">
            
                <div class="row">
                

                    <div class="col-lg-12">

                    

                    <div class="panel panel-default">
                    <div class="panel-heading">
                    
                    <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-6"><h3 class="text-danger" style="margin:0px;">
                     <asp:Label ID="lblMain" runat="server" Text="DOOR-TO-DOOR  SURVEY"></asp:Label>
                    </h3></div>
                    <div class="col-lg-6 col-md-6 col-sm-6 " style="padding:0px">
               <%--     <input type="image" id="ton-new" class="butt" src="Images/search-not-29.png" title="Search"   />--%>
                    <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right" ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png"  style="margin-right: 5px; padding:0px;" runat="server" />
                   
                    <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right"  BackColor="#f5f5f5" ToolTip="Save"  ImageUrl="~/images/save-29-1.png"  OnClick="btnsave_Click" ValidationGroup="saves" style="margin-right: 5px; padding:0px;" runat="server"  />
                     <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-right"  BackColor="#f5f5f5" ToolTip="Add" ImageUrl="~/images/add-29-1.png"  OnClick="btnAdd_Click"  style="margin-right: 5px; padding:0px;" runat="server" />
                   
                    </div>
                    </div>
                     </div>
                    <div>
                   
                    </div>
                   
                   
                    <div class="form-horizontal" >
                                        <div class="row">
                                        <div id="div-show-new">
                                       <%--    <asp:UpdatePanel runat="server" ID="UpMaiddn" >
        <ContentTemplate>--%>
                    <div class="row marg search-bg">
  <div  class="form-horizontal">
   <%--<asp:UpdatePanel runat="server" ID="UpMaiddn" >
        <ContentTemplate>--%>
        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                                <div class="form-group">
                                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                        Year:
                                                                    </label>
                                                                    <div class="col-sm-9 padd">
                                                                        <asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"   runat="server"   class="form-control ">
                                                                                 </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
            	<div class="form-group" >
                <label for="email" class="col-sm-3 padd linhei"  style="padding-top: 2px;" >State:</label>
                <div class="col-sm-9 padd">
                       <asp:DropDownList ID="ddlState" runat="server" onselectedindexchanged="ddlState_SelectedIndexChanged" AutoPostBack="true" class="form-control ">
                    </asp:DropDownList>
                    <asp:Label ID="lblNumNo" Visible="false" runat="server" Text="Label"></asp:Label>
    					
    					
                				</div>
    								</div>
            							</div>
                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
            	<div class="form-group" >
    				<label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">District:</label>
    					 <div class="col-sm-9 padd">
    					<asp:DropDownList ID="ddlDistrict" runat="server"  onselectedindexchanged="ddlDistrict_SelectedIndexChanged"  AutoPostBack="true" class="form-control "/>
              
                				</div>
  									</div>
            							</div> 
                      <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
            	<div class="form-group" >
    				<label for="email" class="col-sm-3 padd linhei"  style="padding-top: 2px;">Block:</label>
    					 <div class="col-sm-9 padd">
    						<asp:DropDownList ID="ddlBlock" runat="server"   AutoPostBack="true" onselectedindexchanged="ddlBlock_SelectedIndexChanged" class="form-control "/>
                				</div>
  									</div>
            							</div>
                           <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
            	<div class="form-group">
    				<label for="email" class="col-sm-3 padd linhei"  style="padding-top: 2px;">Panchayat:</label>
    					<div class="col-sm-9 padd">
    					<asp:DropDownList ID="ddlPanchayat" runat="server"  AutoPostBack="true" onselectedindexchanged="ddlPanchayat_SelectedIndexChanged"  class="form-control "/>
                				</div>
  									</div>
            							</div>
                                 <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
            	<div class="form-group" >
    				<label for="email" class="col-sm-3 padd linhei"  style="padding-top: 2px;">Village:</label>
    					<div class="col-sm-9 padd">
    					<asp:DropDownList ID="ddlVillage" onselectedindexchanged="ddlVillage_SelectedIndexChanged" AutoPostBack="true"  runat="server"  class="form-control "/>
                		 <span style="width: 5px;float: right;margin: -27px 27px;font-size: 21px;">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic" ValidationGroup="saves" 
                             ControlToValidate="ddlVillage" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                             </span>
                				</div>
  									</div>
            							</div>  



                                        	<div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                            	 <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server"  class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1" OnClick="btnSerach_Click"  ImageUrl="~/images/search-29.png" />
                                             
                                                            </div> 
                                            </div>           
                                                                                  
            	</div>
                    </div>
                    <%--</ContentTemplate>
</asp:UpdatePanel>--%>

                 <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>                
                    <div class="col-lg-12">
                  
                    <asp:Panel ID="pnlMain" Enabled="false" runat="server" >
                       
                          <div class="panel-body" style="padding:0px;">
                    <div class="row">
                    <div class="form-horizontal" role="form">
            	<div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">
                <fieldset class="scheduler-border">
    						<legend class="scheduler-border"> Personal Details </legend>
                            <div class="form-group">
      <label class="control-label col-sm-4" for="Name">Survey Date<span class="req">*</span></label>
      <div class="col-sm-8">
         <asp:TextBox runat="server"  ID="txtSarveyDate"  autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"                     
                                              ></asp:TextBox>
                                         
                                            <ajax:CalendarExtender ID="CalendarExtenderTourdate"   runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" TargetControlID="txtSarveyDate" OnClientDateSelectionChanged="arrivaldatecheck"  PopupPosition="BottomRight"></ajax:CalendarExtender>
      <span class="reqfield">   <asp:RequiredFieldValidator ID="ReqTxtDate" runat="server" ControlToValidate="txtSarveyDate"
                                                Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
      </span>
      </div></div>
      
      <div class="form-group">
      <label class="control-label col-sm-4" for="Name">Mauhalla/Dhani/Fali<span class="req">*</span></label>
      <div class="col-sm-8">
          <asp:TextBox ID="txtMauhalla" autocomplete="off" ondrop="return false;"  onkeypress="return onlyAlphabetsAdd(event,this);" runat="server" MaxLength="30" class="form-control"   />
          <span class="reqfield">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="Dynamic" ValidationGroup="saves" 
                             ControlToValidate="txtMauhalla" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                             </span>
      </div></div>
      
      <div class="form-group">
      <label class="control-label col-sm-4" for="Name">House/Family No<span class="req">*</span></label>
      <div class="col-sm-8">
                 <asp:TextBox ID="txtHouse" autocomplete="off" ondrop="return false;"  onkeypress="return onlyAlphabetsHH(event,this);"  onchange="checkPwd(this.value);"   runat="server" MaxLength="9" class="form-control"   />
          <span class="reqfield">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ValidationGroup="saves" 
                             ControlToValidate="txtHouse" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                             </span>
      </div></div>
      
      <div class="form-group">
      <label class="control-label col-sm-4" for="Name">Unique ID</label>
      <div class="col-sm-8">
         <asp:TextBox ID="txtUnique" onkeypress="return onlyAlphabets(event,this);" Enabled="false" runat="server" MaxLength="30" class="form-control"   />

      </div></div>
      
  
             <div class="form-group">
      <label class="control-label col-sm-4" for="Name">Child Name<span class="req">*</span></label>
      <div class="col-sm-8">
                 <asp:TextBox ID="txtChildName" autocomplete="off" ondrop="return false;"  onkeypress="return onlyAlphabets(event,this);" runat="server" MaxLength="30" class="form-control"   />
          <span class="reqfield">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ValidationGroup="saves" 
                             ControlToValidate="txtChildName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                             </span>
      </div></div>


            <div class="form-group">
      <label class="control-label col-sm-4" for="Name">Fathers Name<span class="req">*</span></label>
      <div class="col-sm-8">
                 <asp:TextBox ID="txtFatherName" autocomplete="off" ondrop="return false;"  onkeypress="return onlyAlphabets(event,this);" runat="server" MaxLength="30" class="form-control"   />
          <span class="reqfield">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ValidationGroup="saves" 
                             ControlToValidate="txtFatherName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                             </span>
      </div></div>

          <div class="form-group">
      <label class="control-label col-sm-4" for="Name">Gender<span class="req">*</span></label>
      <div class="col-sm-8">
     
                 <asp:DropDownList ID="ddlGender" runat="server"  class="form-control">
            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
            <asp:ListItem Value="1">1-Male </asp:ListItem>
             <asp:ListItem Value="2">2-Female</asp:ListItem>
             
        
        </asp:DropDownList>
           <span style="width: 5px;float: right;margin: -27px 27px;font-size: 21px;">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator65" InitialValue="0" runat="server" Display="Dynamic" ValidationGroup="saves" 
                             ControlToValidate="ddlGender" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                             </span>
                            
      </div></div>

      <div class="form-group">
      <label class="control-label col-sm-4" for="Name">DOB Available</label>
      <div class="col-sm-8">
      <div style="width:100%;">
      <span style="float:left; width:42%;">
      
                <asp:DropDownList ID="ddlDob" runat="server" AutoPostBack="true"  style="width: 85%;"  onselectedindexchanged="ddlDob_SelectedIndexChanged"  class="form-control">
            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
            <asp:ListItem Value="1">Yes </asp:ListItem>
             <asp:ListItem Value="2">No</asp:ListItem>
             
        
        </asp:DropDownList>
                                                                          </span>
                                                                           <span style="float: left; width: 19%; padding-top: 1px;">
                                                                               <asp:Label  runat="server" ID="lblAge" class="control-label col-sm-4"  Text="Age"></asp:Label>
                                                                           </span>
          
            
        
                 <asp:TextBox ID="txtAge"  runat="server"  Width="38%"  Enabled="false"  MaxLength="2" onkeypress="return isNumberKey(this,event);" 
                                                                autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />                                                    
                                                                 
                    </div>
      </div></div>

         

        
                         <div class="form-group">
 
                          <asp:Label class="control-label col-sm-4"  runat="server" ID="lblDob" Text="Date" ></asp:Label>
 
      <div class="col-sm-8">
    <asp:TextBox runat="server"  ID="txtDate"  autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"    ></asp:TextBox>
                                             
                                         
                                            <ajax:CalendarExtender ID="CalendarExtender1"  runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight"></ajax:CalendarExtender>
      <span class="reqfield">   <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDate"
                                                Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
      </span>
      </div></div>
                  
                </fieldset>
                
      
                </div>
                
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" style="padding: 0px 5px 0px 3px;">
                <fieldset class="scheduler-border">
    						<legend class="scheduler-border">  Identification & Occupation  </legend>
                            <div class="form-group">
      <label class="control-label col-sm-4" for="Name">Age Proof<span class="req">*</span></label>
      <div class="col-sm-8">
        
                <asp:DropDownList ID="cmbAgeproof"  onselectedindexchanged="cmbAgeproof_SelectedIndexChanged" runat="server" AutoPostBack="true"   class="form-control">
            
        </asp:DropDownList>

         <span style="width: 5px;float: right;margin: -27px 27px;font-size: 21px;">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="0" runat="server" Display="Dynamic" ValidationGroup="saves" 
                             ControlToValidate="cmbAgeproof" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                             </span>  
      </div></div>
      
      <div class="form-group">
      <label class="control-label col-sm-4" for="Name">Specify (if other)</label>
      <div class="col-sm-8">
                      <asp:TextBox ID="txtOtherAge" autocomplete="off" ondrop="return false;"  Enabled="false" onkeypress="return onlyAlphabets(event,this);" runat="server" MaxLength="30" class="form-control"   />
 
      </div></div>
      
      <div class="form-group">
      <label class="control-label col-sm-4" for="Name">Social Category<span class="req">*</span></label>
      <div class="col-sm-8">
        
            <asp:DropDownList ID="ddlCategory" runat="server" class="form-control">
            </asp:DropDownList>
            <span style="width: 5px;float: right;margin: -27px 27px;font-size: 21px;">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator8" InitialValue="0" runat="server" Display="Dynamic" ValidationGroup="saves" 
                             ControlToValidate="ddlCategory" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                             </span>  
      </div></div>
      
      <div class="form-group">
      <label class="control-label col-sm-4" for="Name">Family Occupation<span class="req">*</span></label>
      <div class="col-sm-8">
         <asp:DropDownList ID="ddloccu" runat="server" AutoPostBack="true"  onselectedindexchanged="ddloccu_SelectedIndexChanged" class="form-control">
            </asp:DropDownList>

             <span style="width: 5px;float: right;margin: -27px 27px;font-size: 21px;">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator9" InitialValue="0" runat="server" Display="Dynamic" ValidationGroup="saves" 
                             ControlToValidate="ddloccu" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                             </span>  
      </div></div>
      
        <div class="form-group">
      <label class="control-label col-sm-4" for="Name">Specify (if other)</label>
      <div class="col-sm-8">
             <asp:TextBox ID="txtFOther" autocomplete="off" ondrop="return false;" Enabled="false" onkeypress="return onlyAlphabets(event,this);"  runat="server" MaxLength="30" class="form-control"   />
 
      </div></div>  
                            
                </fieldset>
                
      <fieldset class="scheduler-border">
    						<legend class="scheduler-border"> Enrollment status </legend>
          <div class="form-group">
      <label class="control-label col-sm-4" for="Name">Enrollment status<span class="req">*</span></label>
      <div class="col-sm-8">
         <asp:DropDownList ID="ddlEducation" AutoPostBack="true" onselectedindexchanged="ddlEducation_SelectedIndexChanged"  runat="server" class="form-control">
            </asp:DropDownList>
              <span style="width: 5px;float: right;margin: -27px 27px;font-size: 21px;">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator10" InitialValue="0" runat="server" Display="Dynamic" ValidationGroup="saves" 
                             ControlToValidate="ddlEducation" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                             </span>  
      </div></div>
      
       <div class="form-group">
      <label class="control-label col-sm-4" for="Name">School if enrolled or drop out</label>
      <div class="col-sm-8">
        <asp:DropDownList ID="cmbSchool" runat="server" class="form-control" AutoPostBack="true" onselectedindexchanged="cmbSchool_SelectedIndexChanged">
            </asp:DropDownList>

          
      </div></div>

       <div class="form-group">
      <label class="control-label col-sm-4" for="Name">Specify (if other)</label>
      <div class="col-sm-8">
             <asp:TextBox ID="txtNewSchool" autocomplete="off" ondrop="return false;" onkeypress="return onlyAlphabets(event,this);" Enabled="false" runat="server" MaxLength="30" class="form-control"   />
 
      </div></div>


       <div class="form-group">
      <label class="control-label col-sm-4" for="Name">Class of Enrolled / DO Child<span class="req">*</span></label>
      <div class="col-sm-8">
      
            <asp:DropDownList ID="ddlClass" runat="server" class="form-control">
            </asp:DropDownList>

          
      </div></div>


         <div class="form-group">
      <label class="control-label col-sm-4" for="Name">Reason of DO && NE <span class="req">*</span></label>
      <div class="col-sm-8">
        <asp:DropDownList ID="cmbReason" runat="server" class="form-control" AutoPostBack="true" onselectedindexchanged="cmbReason_SelectedIndexChanged">
            </asp:DropDownList>

            
      </div></div>

              <div class="form-group">
      <label class="control-label col-sm-4" for="Name">Migration Duration(#Month):</label>
      <div class="col-sm-8">
      <div style="width:100%;">
      <span>
         <asp:TextBox ID="txtMigration" Enabled="false" autocomplete="off" ondrop="return false;"  runat="server" MaxLength="2" onkeypress="return isNumberKey(this,event);" 
                                                                class="form-control TeContact1 " />  
                                                                          </span>
                                                                        
          
            
       
            </div>
      </div></div>

       <div class="form-group">
      <label class="control-label col-sm-4" for="Name">Enrollment Category </label>
      <div class="col-sm-8">
        <asp:DropDownList ID="ddlEnrollCat" runat="server" class="form-control">
            </asp:DropDownList>
      </div></div>
      </fieldset>
                </div>
                
                </div>
            </div>
                    </div>
                     </asp:Panel>
                    </div>
           </ContentTemplate>
       </asp:UpdatePanel>            
         
            </div>
        </div>
       
        <!-- /#page-content-wrapper -->

    </div>
    <!-- /#wrapper -->
    <!-- /#wrapper -->
           </div>
            
                 
            
                    </div>
               
             
            </div>
     </div>
         </div>
         </ContentTemplate>
         </asp:UpdatePanel>
      
  
 <script type="text/javascript">
     $(function () {
         $('#datetimepicker4').datetimepicker();
     });
        </script>
        
</asp:Content>
