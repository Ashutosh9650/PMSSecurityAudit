<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" Culture="en-GB" AutoEventWireup="true" CodeFile="frmLSE.aspx.cs" Inherits="frmLSE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.2.4/jquery.min.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>


     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>


      <script src="js/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
    <link rel="stylesheet" type="text/css" href="css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="css/eg_styles.css">

    <script src="online-js/jquery-ui.min.js" type="text/javascript"></script>
    <link href="online-js/jquery-ui.css" rel="stylesheet" type="text/css" />
    <style>
        .asd label {
    margin-bottom: 0px;
    position: relative;
    top: 2px;
}
    
         .search-bg {
            background: linear-gradient(to bottom, #ebf1fd 0%,#ffffff 100%) !important;
            /*background-color: rgb(241, 241, 241)!important;*/
            padding-top: 7px !important;
            border: 1px solid rgb(221, 221, 221) !important;
            border-top-left-radius: 4px !important;
            border-top-right-radius: 4px !important;
            margin-bottom: 0px !important;
        }
        .WrapText {  
            width: 100%;  
            word-break: break-all;  
        }  
   
        .container-fluid {
            padding-right: 15px;
            padding-left: 15px;
            margin-right: auto;
            margin-left: auto;
        }

        .row {
            margin-right: -15px;
            margin-left: -15px;
        }

        .panel-heading {
            padding: 10px 15px;
        }

        .edit-bg {
            width: 20px;
            height: 20px;
            background-color: #898989;
            border-radius: 50px;
            float: left;
            display: flex;
            justify-content: center;
            align-items: center;
            color: #fff;
        }

            .edit-bg:hover {
                color: #eee;
                text-decoration: none;
                background-color: #626161;
            }

        .modal {
            position: fixed;
            top: 80px;
            right: 0;
            bottom: 0;
            left: 0;
            z-index: 9999;
            width:62%;
            display: none;
            overflow: hidden;
            -webkit-overflow-scrolling: touch;
            outline: 0;
        }

        .form-group {
            margin-bottom: 15px;
            float: left;
            width: 100%;
        }

         .ajax__calendar_container
        {
            z-index: 1045;
        }
    </style>
     <script language="Javascript" type="text/javascript">
         $(document).ready(function () {



             $("[id$=txtFromDate]").datepicker({ maxDate: new Date() });
             $("[id$=txtFromDate]").datepicker({
                 dateFormat: 'dd/mm/yy'
             });
             $("[id$=txtFromDate]").datepicker();

             $('#datepickers-container').css('z-index', 1045);
         });
         function loadJSFunction() {

             $("[id$=txtFromDate]").datepicker({
                 dateFormat: 'dd/mm/yy',
                 changeMonth: true,
                 changeYear: true,
                 minDate: '-60Y',
                 yearRange: '1965:2024',
                 defaultDate: new Date()

             });

             $("[id$=txtFromDate]").datepicker();



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
          function Imageuploaddata(textid) {
              debugger;
              var fileInput =
                  document.getElementById(textid);

              var filePath = fileInput.value;

              // Allowing file type
              var allowedExtensions =
                  /(\.jpg|\.jpeg|\.png|\.gif)$/i;

              if (!allowedExtensions.exec(filePath)) {
                  alert('Invalid file type');
                  fileInput.value = '';
                  return false;
              }
              else {


                  $.ajax({
                      url: 'HandlerImageLSE.ashx',
                      type: 'POST',
                      data: new FormData($('form')[0]),
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (textid) {

                          var imm = textid.name;
                          maiID.value = imm;
                          //$("#fileProgress").hide();
                          //$("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                      }
                  });
                  fncsave();

                  return true;
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
                   if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 32 || charCode == 0 || charCode == 9 || charCode == 08 || charCode == 44 || charCode == 45 || charCode == 48)
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
         function validateFristNumeric(txt) {
             debugger;
             var firstChar = txt.value.charAt(0);
             if (firstChar == 0) {
                 //do your stuff
           
                 txt.value = "";

             }
             else {
                 return true;
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

    <script type="text/javascript">
        function SelectAll(headerCheckBox) {
            //Get the reference of GridView.
            var GridView = headerCheckBox.parentNode.parentNode.parentNode;

            //Loop through all GridView Rows except first row.
            for (var i = 1; i < GridView.rows.length; i++) {
                //Reference the CheckBox.
                var checkBox = GridView.rows[i].cells[0].getElementsByTagName("input")[0];
                checkBox.checked = headerCheckBox.checked;
            }
        }
    </script>

    

     <%--   <script language="Javascript" type="text/javascript">
            $(document).ready(function () {



                $("[id$=txtFromDate]").datepicker({ maxDate: new Date() });
                $("[id$=txtFromDate]").datepicker({
                    dateFormat: 'dd/mm/yy'
                });
                $("[id$=txtFromDate]").datepicker();

                $('.txtFromDate').datepicker({
                    beforeShowDay: function (d) {
                        alert("dsad");
                        return [true, "test-class", "Not available"];
                    }
                });

                $('#datepickers-container').css('z-index', 1045);
            });

        </script>

       <script type="text/javascript">
           function loadJSFunction() {

               $("[id$=txtFromDate]").datepicker({
                   dateFormat: 'dd/mm/yy',
                   changeMonth: true,
                   changeYear: true,
                   minDate: 0,
                   minDateTime: new Date(),
                   yearRange: '2024:2025',
                   defaultDate: new Date()

               });

               $("[id$=txtFromDate]").datepicker();



           }

           function loadJSFunction1() {
               debugger;
               var datecond = '-7d';
              
               $('#txtFromDate').datepicker({
                   //format: 'YYYY-MM-DD',
                   //useCurrent: false,
                   //showClose: true,
                 /*  minDate: '2024-08-12',*/
               /*    maxDate: '2024-08-17',*/
                   maxDate: new Date(),
                   format: "dd/mm/yyyy",
                   startDate: datecond,
                   endDate: 'now',
                   autoclose: true
                   //minDate: new Date(2024, 08, 12),
                   //maxDate: new Date(2024, 08, 17),
                   //setDate: new Date(2024, 08, 12)
               });
               //var date = new Date();
               var startDate = new Date(2024, 8, 15); // 15th September 2024 (months are 0-based in JavaScript)
               var endDate = new Date(2024, 8, 21);   // 21st September 2024

               //$("#txtFromDate").datepicker({
               //    beforeShowDay: noWeekendsOrOtherDays,	// Calls to user defined function for disabling days
               //});

               $("#txtFromDate").datepicker({
                   dateFormat: 'yyyy - mm - dd', 

                   beforeShowDay: function (date) {
                       if (date >= startDate && date <= endDate) {
                           return [true, "", "Available"];
                       }
                       return [false, "", "Unavailable"];
                   },
                   dateFormat: 'yyyy/mm/dd'
               });

             
               $("[id$=txtFromDate]").datepicker();

           }

           function noWeekendsOrOtherDays(date) {
               alert("");
               var noWeekend = $.datepicker.noWeekends(date);
               if (noWeekend[0]) {
                   return getDisableDays(date);
               } else {
                   return noWeekend;
               }
           }
       </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


     <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
     <%--       
     <script type="text/javascript">
         $(document).ready(function () {
             $('#search_box').show(500);

         })
     </script>--%>
    <div class="container-fluid">
         <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="thumbnail" style="background-color: #f5f5f5; margin-bottom: 3px !important;">
                            <div class="panel-heading" style="padding: 0px 0px;">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <h3 class="text-danger" style="margin: 0px;">
                                           LSE Assessment</h3>
                                             
                                </div>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
        <div class="row">
          
             <div class="col-sm-12">
                        <div id="search_box">
                            <div class="panel panel-default" style="margin-bottom: 8px;">
                                <div class="panel-body" style="padding-top: 0px; padding-bottom: 0px;">
                                    <div class="row" style="margin: 0px -15px;">


                                        <div class="col-lg-12  search-bg">
                                            <div id="container-target">

                                                <div class="form-horizontal">
                                             <div class="row marg" style="margin-left: -15px; margin-right: -15px">
                   <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" >
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                        State:</label>
                                    <div class="col-sm-9 padd">
                                       <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                                    AutoPostBack="true" class="form-control ">
                                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                        District:</label>
                                    <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlDistrict" runat="server"
                                            OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                            AutoPostBack="true" class="form-control " />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                        Block:</label>
                                    <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlBlock"  OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" runat="server" AutoPostBack="true"
                                            class="form-control " />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" style="display:none">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                        FC:</label>
                                    <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlUser" OnSelectedIndexChanged="ddlFC_SelectedIndexChanged" runat="server" AutoPostBack="true"  class="form-control ">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" >
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                        Panchayat:</label>
                                    <div class="col-sm-9 padd">
                                     <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"
                                                                    class="form-control " />
                                    </div>
                                </div>
                            </div>
                               
                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" style="display:none">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 linhei" style="padding-right: 0px; padding-left: 7px;">
                                        Year:
                                    </label>
                                    <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlYear" Enabled="false" AutoPostBack="true" runat="server"
                                            class="form-control ">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                        </div>

                                 <div class="row marg" style="margin-left: -15px; margin-right: -15px">
                            <div id="Div3" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                        Village:</label>
                                    <div class="col-sm-9 padd">
                                    <asp:DropDownList ID="ddlVillage" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged"
                                                                    AutoPostBack="true" runat="server" class="form-control " />
                                    </div>
                                </div>
                            </div>
                            
                         
                            <div id="Div1" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                        School:</label>
                                    <div class="col-sm-9 padd">
                                         <asp:DropDownList ID="ddlSchool"  runat="server" class="form-control ">
                                                                </asp:DropDownList>
                                      <asp:DropDownList ID="ddlMonth" Visible="false" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"  AutoPostBack="true" runat="server" class="form-control"
                                                            >
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
                            <div id="Div2" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                        Session:</label>
                                    <div class="col-sm-9 padd">
                                              <asp:DropDownList ID="ddlWeeklly"   runat="server"  class="form-control"  >
                                                              <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="228">Session 1  </asp:ListItem>
                                                            <asp:ListItem Value="243">Session 10 </asp:ListItem>
                                                           
                                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                   <asp:LinkButton ID="Button1" OnClick="btnApprove_Click" class="btn btn-sm btn-primary primaryKK"    runat="server">Search</asp:LinkButton>
                                       <asp:LinkButton ID="LinkButton2" Visible="false"  OnClick="btnFinalSave_Click"  class="btn btn-sm btn-primary primaryKK "  style="margin-left:49px;"   runat="server">Save</asp:LinkButton>
                                                     
                                   <%--<asp:Button  type="button" ID="Button1" runat="server" OnClick="btnApprove_Click" Text="Approve" Visible="false" class="btn btn-success"></asp:Button>--%>
                           
                            </div>

                        </div>
                       <div class="row marg" style="margin-left: -15px; margin-right: -15px">

                              <div id="Div4" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                      Date:</label>
                                    <div class="col-sm-9 padd">
                                         <asp:TextBox runat="server" ID="txtFromDate"
                                                        autocomplete="off" ondrop="return false;"   class="form-control" onkeypress="return false;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div id="Div5" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                      Activity:</label>
                                    <div class="col-sm-9 padd" style="margin-top:-4px">
                                            <asp:RadioButtonList ID="rblTBFC" OnSelectedIndexChanged="rvltbc" AutoPostBack="true"  RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                                                                        <asp:ListItem class="radio-inline asd" Value="1">TB</asp:ListItem>
                                                                                            <asp:ListItem class="radio-inline asd"  Value="2">FC</asp:ListItem>
                                                                                         
                                                                                    </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                             <div id="Div6" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                      TB/FC:</label>
                                    <div class="col-sm-9 padd" style="margin-top:-4px">
                                           <asp:DropDownList ID="ddlTbFC"   runat="server"  class="form-control"  >
                                                            
                                                           
                                                        </asp:DropDownList>
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

            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12"  style="display:none">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <h4>User Information</h4>
                          <%--<div style="height: 290px; overflow: auto; width: 100%;" align="center">--%>

                          
                           
                                 <div>
                                  <div class="Row" style="width: 100%">
                                       <div class="Row WrapText table-responsive" style="height: 310px; overflow: auto; width: 100%;" align="center">
                                   <asp:GridView ID="gvWeeklly" runat="server"  CssClass="table table-striped table-bordered table-hover"  OnRowDataBound="gvnroll_OnRowCommand"   AutoGenerateColumns="False"  Font-Names="Arial"
                                                                        Font-Size="12px" Width="100%" >
                                                                          <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
                                                                        </EmptyDataTemplate>
                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <Columns>
                                                                          
                                                                            <asp:TemplateField HeaderText="FC Name"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblUserName" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("UserName") %>'></asp:Label>
                                                                                      <asp:Label ID="lblUserID" ForeColor="Black" Visible="false" runat="server"
                                                                                        Text='<%# Eval("UserID") %>'></asp:Label>
                                                                                     
                                                                                   
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                          
                                                                            <asp:TemplateField HeaderText="Status"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStatus"  ForeColor="Black" runat="server"
                                                                                        ></asp:Label>

                                                                                    <asp:Label ID="lblStatus1" Visible="false" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("Status") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            
                                                                        
                                                                         
                                                                           
                                                                        </Columns>
                                                                        
                                                                    </asp:GridView>


                                           </div>
                                      </div>
                                    </div>
                             <%--</div>--%>
                    </div>
                </div>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" >
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div style="display:flex;justify-content:space-between;align-items:center;gap:12px;display:none">
                        <h4>Weekly Plan</h4>
                         
                                
                    <%--     <asp:Button  type="button" ID="btnAdd"  OnClick="btnAdd_Click" runat="server" Text="Add" Visible="false"  class="btn btn-success"></asp:Button>--%>
                       </div>
                            <%--<div style="height: 290px; overflow: auto; width: 100%;" align="center">--%>
                                 <div >
                                  <div class="Row WrapText table-responsive" style="height: 410px; overflow: auto; width: 100%;" align="center">
                                   <asp:GridView ID="gvWeallyDatewise" runat="server"  CssClass="table table-striped table-bordered table-hover"   OnRowDataBound="gvnroll1_OnRowCommand"    AutoGenerateColumns="False"  Font-Names="Arial"
                                                                        Font-Size="12px" Width="100%" >
                                                                          <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
                                                                        </EmptyDataTemplate>
                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <Columns>
                                                                             
                                                                            <asp:TemplateField HeaderText="Child Name"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblChildName" ForeColor="Black" runat="server"    Text='<%# Eval("Child Name") %>'></asp:Label>
                                                                                     <asp:Label ID="lblUniquePlanCode" Visible="false" ForeColor="Black" runat="server"    Text='<%# Eval("ChildID") %>'></asp:Label>
                                                                                   <asp:Label ID="lblAnswerSheetPhoto" Visible="false" ForeColor="Black" runat="server"    Text='<%# Eval("AnswerSheetPhoto") %>'></asp:Label>
                                                                                  <asp:Label ID="lblPresent" ForeColor="Black" runat="server"    Text='<%# Eval("Present") %>'></asp:Label>
                                                                               <asp:Label ID="lblOMRAnswersEdit" Visible="false" ForeColor="Black" runat="server"    Text='<%# Eval("OMRAnswersEdit") %>'></asp:Label>
                                                                   
                                                                                    <asp:Label ID="lblIsweb" ForeColor="Black" Visible="false" runat="server"
                                                                                        Text='<%# Eval("Isweb") %>'></asp:Label>
                                                                                     <asp:Label ID="Label1" ForeColor="Black" Visible="false" runat="server"
                                                                                        Text='<%# Eval("Assessment") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                

                                                                            </asp:TemplateField>
                                                                          
                                                                            <asp:TemplateField HeaderText="Father Name"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFatherName" ForeColor="Black" runat="server"    Text='<%# Eval("Father Name") %>'></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                               
                                                                            </asp:TemplateField>
                      
                                                                             <asp:TemplateField HeaderText="Gender"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblGender"  ForeColor="Black" runat="server"    Text='<%# Eval("Gender") %>'></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                                

                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Age"  Visible="true">
                                                                                <ItemTemplate>
                                                                                       <asp:Label ID="lblAge"  ForeColor="Black" runat="server"    Text='<%# Eval("Age") %>'></asp:Label>
                                                                             
                                                                                </ItemTemplate>
                                                                                

                                                                            </asp:TemplateField>

                                                                        
                                                                          
                                                                             <asp:TemplateField HeaderText="SR Number"  Visible="true">
                                                                                <ItemTemplate>
                                                                                   <asp:Label ID="lblSRNumber"  ForeColor="Black" runat="server"    Text='<%# Eval("SR Number") %>'></asp:Label>
                                                                             
                                                                                </ItemTemplate>
                                                                               

                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Class"  Visible="true" ItemStyle-Wrap="true">
                                                                                <ItemTemplate>
                                                                                   <asp:Label ID="lblClass"  ForeColor="Black" runat="server"    Text='<%# Eval("Class") %>'></asp:Label>
                                                                             
                                                                                </ItemTemplate>
                                                                              
                                                                            </asp:TemplateField>
                                                                           
                                                                            <asp:TemplateField HeaderText="Date of Attendance"  >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDateofAttendance"  ForeColor="Black" runat="server"    Text='<%# Eval("Date of Attendance") %>'></asp:Label>
                                                                             
                                                                                </ItemTemplate>
                                                                              
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Attendance Status" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlAttStatus"  OnSelectedIndexChanged="ddlWorkingStatus_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control ">
                                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Present </asp:ListItem>
                                                            <asp:ListItem Value="2">Absent </asp:ListItem>
                                                           

                                                        </asp:DropDownList>
                                                                          <asp:Label ID="lblAttendanceStatus"  ForeColor="Black" runat="server" Visible="false"    Text='<%# Eval("Attendance Status") %>'></asp:Label>
                                                                             
                                                                          
                                                                     
                                                                                </ItemTemplate>
                                                                                

                                                                            </asp:TemplateField>

                                                                             <asp:TemplateField HeaderText="Image" Visible="true">
                                                                                <ItemTemplate>
                                                                                    
                                                                                        <asp:ImageButton ID="ImgShow" ToolTip="Show" runat="server" class="btn btn-danger btn-paddd"
                                                    BackColor="#f1f1f1" OnClick="ImgShow_Click" ImageUrl="~/images/iconimage-128.png"
                                                    Height="25px" />
                                                                          
                                                                     
                                                                                </ItemTemplate>
                                                                                

                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Download/View" Visible="true">
                                                                                <ItemTemplate>
                                                                                    
                                                                                          <asp:ImageButton ID="lnkd" ToolTip="Show" runat="server" class="btn btn-danger btn-paddd"
                                                    BackColor="#f1f1f1" OnClick="ImgDownload_Click" ImageUrl="~/images/download.png"
                                                    Height="25px" />
                                                   <asp:LinkButton ID="Button1" OnClick="ImgView_Click" class="btn btn-sm btn-primary primaryKK"   Height="30px"   runat="server">View</asp:LinkButton>
                        
                                                                     
                                                                                </ItemTemplate>
                                                                                

                                                                            </asp:TemplateField>
                                                                        
                                                                        
                                                                       
                                                                           
                                                                        </Columns>
                                                                        
                                                                    </asp:GridView>


                                       <asp:GridView ID="GridView1" runat="server"  CssClass="table table-striped table-bordered table-hover"  OnRowDataBound="gvnroll17_OnRowCommand"      AutoGenerateColumns="False"  Font-Names="Arial"
                                                                        Font-Size="12px" Width="100%" >
                                                                          <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
                                                                        </EmptyDataTemplate>
                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <Columns>
                                                                             
                                                                            <asp:TemplateField HeaderText="Child Name"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblChildName" ForeColor="Black" runat="server"    Text='<%# Eval("Child Name") %>'></asp:Label>
                                                                                     <asp:Label ID="lblUniquePlanCode" Visible="false" ForeColor="Black" runat="server"    Text='<%# Eval("ChildID") %>'></asp:Label>
                                                                                    <asp:Label ID="lblUniqueChildRCode" Visible="false" ForeColor="Black" runat="server"    Text='<%# Eval("UniqueChildRCode") %>'></asp:Label>
                                                                           
                                                                                    <asp:Label ID="lblAnswerSheetPhoto" Visible="false" ForeColor="Black" runat="server"    Text='<%# Eval("AnswerSheetPhoto") %>'></asp:Label>
                                                                                  <asp:Label ID="lblPresent" ForeColor="Black" runat="server"    Text='<%# Eval("Present") %>'></asp:Label>
                                                                               <asp:Label ID="lblOMRAnswersEdit" Visible="false" ForeColor="Black" runat="server"    Text='<%# Eval("OMRAnswersEdit") %>'></asp:Label>
                                                                   
                                                                                    <asp:Label ID="lblIsweb" ForeColor="Black" Visible="false" runat="server"
                                                                                        Text='<%# Eval("Isweb") %>'></asp:Label>
                                                                                     <asp:Label ID="Label1" ForeColor="Black" Visible="false" runat="server"
                                                                                        Text='<%# Eval("Assessment") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                

                                                                            </asp:TemplateField>
                                                                          
                                                                            <asp:TemplateField HeaderText="Father Name"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFatherName" ForeColor="Black" runat="server"    Text='<%# Eval("Father Name") %>'></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                               
                                                                            </asp:TemplateField>
                      
                                                                             <asp:TemplateField HeaderText="Gender"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblGender"  ForeColor="Black" runat="server"    Text='<%# Eval("Gender") %>'></asp:Label>

                                                                             
                                                                                </ItemTemplate>
                                                                                

                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Age"  Visible="true">
                                                                                <ItemTemplate>
                                                                                       <asp:Label ID="lblAge"  ForeColor="Black" runat="server"    Text='<%# Eval("Age") %>'></asp:Label>
                                                                             
                                                                                </ItemTemplate>
                                                                                

                                                                            </asp:TemplateField>

                                                                        
                                                                          
                                                                             <asp:TemplateField HeaderText="SR Number"  Visible="true">
                                                                                <ItemTemplate>
                                                                                   <asp:Label ID="lblSRNumber"  ForeColor="Black" runat="server"    Text='<%# Eval("SR Number") %>'></asp:Label>
                                                                             
                                                                                </ItemTemplate>
                                                                               

                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Class"  Visible="true" ItemStyle-Wrap="true">
                                                                                <ItemTemplate>
                                                                                   <asp:Label ID="lblClass"  ForeColor="Black" runat="server"    Text='<%# Eval("Class") %>'></asp:Label>
                                                                             
                                                                                </ItemTemplate>
                                                                              
                                                                            </asp:TemplateField>
                                                                           
                                                                            <asp:TemplateField HeaderText="Date of Attendance"  >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDateofAttendance"  ForeColor="Black" runat="server"    Text='<%# Eval("Date of Attendance") %>'></asp:Label>
                                                                             
                                                                                </ItemTemplate>
                                                                              
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Attendance Status" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlAttStatus"  OnSelectedIndexChanged="ddlWorkingStatus1_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control ">
                                                            <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Present </asp:ListItem>
                                                            <asp:ListItem Value="2">Absent </asp:ListItem>
                                                           

                                                        </asp:DropDownList>
                                                                          <asp:Label ID="lblAttendanceStatus"  ForeColor="Black" runat="server" Visible="false"    Text='<%# Eval("Attendance Status") %>'></asp:Label>
                                                                             
                                                                          
                                                                     
                                                                                </ItemTemplate>
                                                                                

                                                                            </asp:TemplateField>

                                                                             <asp:TemplateField HeaderText="Image" Visible="true">
                                                                                <ItemTemplate>
                                                                                    
                                                                                        <asp:ImageButton ID="ImgShow1" ToolTip="Show" runat="server" class="btn btn-danger btn-paddd"
                                                    BackColor="#f1f1f1" OnClick="ImgShow_Click1" ImageUrl="~/images/iconimage-128.png"
                                                    Height="25px" />
                                                                          
                                                                     
                                                                                </ItemTemplate>
                                                                                

                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Download/View" Visible="true">
                                                                                <ItemTemplate>
                                                                                    
                                                                                          <asp:ImageButton ID="lnkd1" ToolTip="Show" runat="server" class="btn btn-danger btn-paddd"
                                                    BackColor="#f1f1f1" OnClick="ImgDownload1_Click" ImageUrl="~/images/download.png"
                                                    Height="25px" />
                                                   <asp:LinkButton ID="Button11" OnClick="ImgView1_Click" class="btn btn-sm btn-primary primaryKK"   Height="30px"   runat="server">View</asp:LinkButton>
                        
                                                                     
                                                                                </ItemTemplate>
                                                                                

                                                                            </asp:TemplateField>
                                                                        
                                                                        
                                                                       
                                                                           
                                                                        </Columns>
                                                                        
                                                                    </asp:GridView>
                                      
                                       <asp:Label ID="lblEditUniquePlanCode" Visible="false" ForeColor="Black" runat="server"     ></asp:Label>
                                       <asp:Label ID="lblEditUserName" Visible="false" ForeColor="Black" runat="server"     ></asp:Label>
                                      </div>
                                    </div>
                             <%--</div>--%>
                    </div>
                </div>
                <!-- Modal -->
             <cc1:ModalPopupExtender ID="Modalimages" runat="server" TargetControlID="hdn_images"
                                                PopupControlID="pnl_Images" CancelControlID="btn_cancelalertI" BackgroundCssClass="modalBackground">
                                            </cc1:ModalPopupExtender>
                                            <asp:Panel ID="pnl_Images" runat="server" Style="display: none;overflow:auto" Width="623px" Height="550px"
                                                class="ModalPopup" BackColor="White" BorderColor="Black" BorderStyle="Ridge"
                                                BorderWidth="1">
                                                <div style="margin-bottom: 15px; background-color: #c4c4c4;" align="right">
                                                    <table>
                                                        <tr>
                                                            <td></td>
                                                            <td width="90px" align="right">
                                                                <asp:ImageButton ID="btn_cancelalertI" runat="server" ImageUrl="~/Images/close-29.png" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div style="width: 100%; height: 500px;" >
                                                    <table class="table table-striped" style="margin-bottom:0px">
                                                        <tr>
                                                            <td colspan="2">
                                                                <div id="img1" runat="server" style="width: 100%;  height: auto; border: 1px solid gray; float: left">
                                                                    <asp:Image ID="EduImg" runat="server" Width="100%" style="object-fit:fill;height: auto;" BorderColor="Black"
                                                                        BorderStyle="Ridge" BorderWidth="1px" />
                                                                </div>
                                                            </td>
                                                           
                                                        </tr>
                                                        <tr>

                                                            <td><div style="display:none" > Image Upload</div>
                                                               </td>
                                                            <td><div style="float:left;display:none">
                                                           
                                                                 <asp:LinkButton ID="LinkButton1" Visible="false" OnClick="btnOmg_Click" class="btn btn-sm btn-primary primaryKK"    runat="server">Save</asp:LinkButton>
                                 
                                                                </td>
                                                        </tr>
                                                        
                                                    </table>
                                                </div>
                                            </asp:Panel>
                                            <asp:HiddenField ID="hdn_images" runat="server" />
                                            <asp:HiddenField ID="hdnMKID" runat="server" />
                                            <asp:HiddenField ID="hdnMKID2" runat="server" />
                                            <asp:HiddenField ID="hdnMKID3" runat="server" />
                                            <asp:HiddenField ID="hdnMKID4" runat="server" />
                   <asp:HiddenField ID="hdnMKID5" runat="server" />
                  <asp:HiddenField ID="hdnMKID6" runat="server" />
                  <cc1:ModalPopupExtender ID="MpexdrDistrict1" runat="server" BackgroundCssClass="modalBg "
                                        CancelControlID="CancelButton1" PopupControlID="PnlDistrict1" TargetControlID="HdnFild1">
                                    </cc1:ModalPopupExtender>
                                    <asp:HiddenField ID="HdnFild1" runat="server"></asp:HiddenField>
                                    <asp:Panel CssClass="modal-dialog modal-lg" Style="display: none; height: auto; width: 50% !important;border-style: groove;
                                        margin-top: -75.5px !important;" ID="PnlDistrict1" runat="server">
                                        <div style="width: 100%; height: auto; background-color: #f1f1f1">
                                            <div class="modal-header" >
                                                
                                             <h4 class="modal-title"> <asp:Label ID="lblTpye" runat="server" Text=""></asp:Label>
                                                </h4>
                                                           </div>
                                            <div class="modal-body">
                                            <%--    <div class="row">
                                             <div class="col-lg-6 col-md-6 col-sm-6 cpl-xs-12">
                                             <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                        Plan Type:</label>
                                    <div class="col-sm-9 padd">
                                        <asp:DropDownList ID="ddlPlan" runat="server" OnSelectedIndexChanged="ddlPlanType_Click"
                                            
                                            AutoPostBack="true" class="form-control " >

                                              <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">D2D Contact Plan </asp:ListItem>
                                               <asp:ListItem Value="2">D2D Contact Revisit & Enrolment Support </asp:ListItem>
                                              <asp:ListItem Value="3">D2D Contact Revisit & Retention Support </asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                              </div>
                                  
                                                  </div>
 --%>
                                              <div class="row">

                                                  <div class="panel panel-default" style="margin-bottom: 0px;">
                                                <div class="panel-body">
                      <%--  <h4>Weekly Plan</h4>--%>

                       
                                 <div >

                                  <div class="Row WrapText table-responsive" style="min-height: 100px; height: 340px;overflow: auto; width: 100%;" align="center">
                                   <asp:GridView ID="gvTopvillage" OnRowDataBound="gvnroll1_OnRowCommand3"  runat="server"  CssClass="table table-striped table-bordered table-hover"   AutoGenerateColumns="False"  Font-Names="Arial"
                                                                        Font-Size="12px" Width="90%" style="margin-bottom:0px;" >
                                                                          <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                Data not found</div>
                                                                        </EmptyDataTemplate>
                                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <Columns>
                                                                                 <asp:TemplateField HeaderText="Question no" >
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblVillageghgname"   Text='<%# Eval("srNo") %>'  runat="server"
                                                                                        ></asp:Label>
                                                        </ItemTemplate>
                                                       
                                                    </asp:TemplateField>

                                                                  <asp:TemplateField HeaderText="Answer"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:RadioButtonList ID="rblScore"  RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                                                                        <asp:ListItem class="radio-inline asd" Value="1">A</asp:ListItem>
                                                                                            <asp:ListItem class="radio-inline asd"  Value="2">B</asp:ListItem>
                                                                                            <asp:ListItem class="radio-inline asd"  Value="3">C</asp:ListItem>
                                                                                            <asp:ListItem class="radio-inline asd" Value="4">D</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                    <asp:Label ID="lblVillagename" Visible="false"  Text='<%# Eval("Score") %>'  runat="server"
                                                                                        ></asp:Label>

                                                                              <asp:Label ID="lblFlag" ForeColor="Black" Visible="false" runat="server"
                                                                                        Text='<%# Eval("Flag") %>'></asp:Label>
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
                                               </div>
                                             
                                            <div class="modal-footer" style="padding-top:0px">
                                                <asp:Label ID="lblbImg"  runat="server" Text="Image Upload" Style="margin-Right:539px"> </asp:Label>
                                                  <asp:FileUpload ID="FileuploadAttach" runat="server" onchange="return Imageuploaddata(this.id);" Style="width: 188px;margin-top:-19px;margin-left:190px" Font-Size="Smaller"
                                                                                    TabIndex="16" />
                                                 <asp:LinkButton ID="LinkButton3" data-dismiss="modal" class="btn btn-sm btn-primary primaryKK" OnClick="btnsaveScore_Click"    runat="server">Save</asp:LinkButton>
                           
                                       <asp:LinkButton ID="CancelButton1" data-dismiss="modal" class="btn btn-sm btn-primary primaryKK"    runat="server">Cancel</asp:LinkButton>
                             </div>
                                     <%-- <asp:Button ID="CancelButton1" runat="server" type="button" Text="Cancel" class="btn btn-success"
                                    data-dismiss="modal"></asp:Button>--%>
                             
                                            
                                    </asp:Panel>
            </div>

            </div>
        </div>
 
            </ContentTemplate>
          <Triggers>
            <asp:PostBackTrigger ControlID="LinkButton1" />
         <asp:PostBackTrigger ControlID="gvWeallyDatewise" />
  <asp:PostBackTrigger ControlID="GridView1" />

              

        </Triggers>
         </asp:UpdatePanel>
</asp:Content>


