<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmTrackerNewOld.aspx.cs" Inherits="frmTrackerNewOld" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ddlCars').multiselect();
            $('#ddlCars1').multiselect({
                numberDisplayed: 2

            });
            $('#ddlCars2').multiselect({
                includeSelectAllOption: true,
                enableFiltering: true

            });
            $('#ddlCars3').multiselect({
                nonSelectedText: 'Select Cars'

            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ddlCars').multiselect();
            $('#ddlCars1').multiselect({
                numberDisplayed: 2

            });
            $('#ddlCars2').multiselect({
                includeSelectAllOption: true,
                enableFiltering: true

            });
            $('#ddlCars3').multiselect({
                nonSelectedText: 'Select Cars'

            });
        });
    </script>
    <script language="Javascript" type="text/javascript">

        function onlyAlphabets(t, e) {
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

        function onlyAlphabetsAdd(t, e) {
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


        function onlyAlphabetsHH(t, e) {
            try {


                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 48 && charCode < 57) || (charCode > 96 && charCode < 123) || charCode == 32 || charCode == 32 || charCode == 0 || charCode == 9)
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
    <script type="text/javascript">


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

       function arrivaldate(arrivaldate) {

           var arrivaldate = $('#' + arrivaldate).val();

           var today = new Date();
           alert(arrivaldate);
           alert(today.getDate());
           if (arrivaldate > today.getDate()) {
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
               $('#' + arrivaldate).val = '';
           }

       }


   <%--     function checkPwd(str) {


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
       } --%>
    </script>
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="mkiiainpnl121">
        <ContentTemplate>
            <div class="container-fluid">
            </div>
            <div class="container-fluid" style="margin-top: 0px;">
                <div class="row" style="margin-top: 115px;">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <h3 class="text-danger" style="margin: 0px;">
                                                    <asp:Label ID="lblMain" runat="server" Text="DOOR-TO-DOOR  SURVEY"></asp:Label>

                                                       <asp:Label ID="lblNum" Visible="false"  runat="server" Text=""></asp:Label>
                                                </h3>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">
                                             <asp:Label ID="lblStatus" runat="server" Font-Bold="true" Text="DOOR-TO-DOOR  SURVEY" CssClass="pull-left" ></asp:Label>
                                                <%--     <input type="image" id="ton-new" class="butt" src="Images/search-not-29.png" title="Search"   />--%>
                                                <asp:ImageButton Visible="false" ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right"
                                                    ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px;
                                                    padding: 0px;" runat="server" />
                                                
                                                
                                           <asp:Button ID="btnReject" CssClass="btn btn-success pull-right" 
                                                    ToolTip="Reject" Text="Reject"
                                                    Style="margin-right: 5px; padding: 0px;" Visible="false" Width="50px" Height="25px"  runat="server" />
                                        

                                                   <asp:Button ID="btnApprove" Visible="false" CssClass="btn btn-success pull-right" 
                                                    ToolTip="Approve" Width="50px" Height="25px"  Text="Approve" ValidationGroup="saves"
                                                    Style="margin-right: 5px; padding: 0px;" runat="server" />


                                                   

                                                       <asp:Button ID="btnSumbit" CssClass="btn btn-success pull-right" 
                                                    ToolTip="Sumbit"  Width="50px" Height="25px" Text="Sumbit"  Style="margin-right: 5px; padding: 0px;" runat="server" />

                                                     <asp:Button ID="btnsave" CssClass="btn btn-success pull-right" 
                                                    ToolTip="Save" OnClick="btnsave_Click" Width="50px" Height="25px" Text="Save" ValidationGroup="saves"
                                                    Style="margin-right: 5px; padding: 0px;" runat="server" />

                                                <asp:ImageButton ID="btnAdd" Visible="false" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Add" ImageUrl="~/images/add-29-1.png" OnClick="btnAdd_Click" Style="margin-right: 5px;
                                                    padding: 0px;" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div id="div-show-new">
                                                <div class="row marg search-bg">
                                                    <div class="form-horizontal">
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="fromType" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                    Form Type:
                                                                </label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlformatype" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                                                        runat="server" class="form-control ">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
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
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                    State:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
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
                                                                    <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                                        class="form-control " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                    Panchayat:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"
                                                                        class="form-control " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                    Village:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlVillage" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged"
                                                                        AutoPostBack="true" runat="server" class="form-control " />
                                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                                                                            ValidationGroup="saves" ControlToValidate="ddlVillage" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblSchool" runat="server" class="col-sm-3 padd linhei" Visible="false">School:</asp:Label>
                                                                <%-- <label for="email" class="col-sm-3 padd linhei" runat="server" visible="false"  style="padding-top: 2px;">School:</label>--%>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlschool" runat="server" Visible="false" AutoPostBack="true"
                                                                        class="form-control " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-right"
                                                                BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="MainPanel" runat="server">
                                                <ContentTemplate>
                                                    <%--<div class="col-lg-1">
                                                    </div>--%>
                                                    <div class="col-lg-12">
                                                        <asp:Panel ID="pnlMain" Enabled="true" runat="server">
                                                            <div class="panel-body" style="padding: 0px;">
                                                                <div class="row">
                                                                    <div class="form-horizontal" role="form">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">
                                                                          
                                                                        </div>
                                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">

                                                                          <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1" style="padding: 0px 3px 0px 5px;">
                                                                          </div>

                                                                        <%--form 6--%>

                                                                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10" style="padding: 0px 3px 0px 5px;">
                                                                                <asp:GridView ID="Grdform6" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                                    AllowPaging="true" PageSize="100" AutoGenerateColumns="false" Font-Names="Arial"
                                                                                    Font-Size="12px" Width="100%" ShowFooter="true" OnRowDataBound ="GvForm6_RowDataBound">
                                                                                    <EmptyDataTemplate>
                                                                                        <div style="font-family: Arial; font-size: 11px; font-weight: bold;">
                                                                                            Data not found</div>
                                                                                    </EmptyDataTemplate>
                                                                            
                                                                                    <RowStyle HorizontalAlign="Left" />
                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />
                                                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Left" />
                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Unique Code" Visible="false" >
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblUniqueCode" runat="server" Text='<%#Eval("UniqueCode") %>' Visible="false"></asp:Label>
                                                                                                
                                                                                            </ItemTemplate>

                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                          
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Form 6 Received Date" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtrecivedate" runat="server"  Text='<%#Eval("ReceivedDate") %>'
                                                                                                    Visible="true"  onfocus="this.blur();"></asp:TextBox>
                                                                                                
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtrecivedate" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtrecivedate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Collection Date" >
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtcollectiondate" runat="server" Text='<%#Eval("CollectionDate") %>'  onfocus="this.blur();"></asp:TextBox>
                                                                                                
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtcollectiondate" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtcollectiondate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="No. Of Girls" >
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtnoofgirols" runat="server" Text='<%#Eval("NoOfGirls") %>' MaxLength="3"
                                                                                                    onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                                                
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="No. Of Boys" >
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtNoOfBoys" runat="server" MaxLength="3" Text='<%#Eval("NoOfBoys") %>'
                                                                                                    onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                                                
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Code"  >
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="txtcode" runat="server" Text='<%#Eval("FormCode") %>' Visible="false"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                              <FooterTemplate>
                                                                                                <asp:LinkButton ID="btnAddIndividualEducation6" OnClick="btnAddForm6row_Click"
                                                                                                    runat="server" Font-Bold="true" Width="100px" ForeColor="Black" Text="Add New Row" />
                                                                                            </FooterTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                      
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>

                                                                            <%--form 7--%>

                                                                             <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">
                                                                                <asp:GridView ID="grdform7" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                                    AllowPaging="true" PageSize="100" AutoGenerateColumns="false" Font-Names="Arial"
                                                                                    Font-Size="12px" Width="100%" ShowFooter="true" OnRowDataBound ="GvForm7_RowDataBound">
                                                                                    <EmptyDataTemplate>
                                                                                        <div style="font-family: Arial; font-size: 11px; font-weight: bold;">
                                                                                            Data not found</div>
                                                                                    </EmptyDataTemplate>
                                                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                                                    <RowStyle HorizontalAlign="Left" />
                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />
                                                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Unique Code" Visible="false" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblUniqueCode" runat="server" Text='<%#Eval("UniqueCode") %>' Visible="false"></asp:Label>
                                                                                                
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Form 7 Received Date" Visible="true" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtrecivedate" runat="server"  Text='<%#Eval("ReceivedDate") %>'
                                                                                                    Visible="true"  onfocus="this.blur();"></asp:TextBox>
                                                                                                
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtrecivedate" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtrecivedate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Collection Date" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtcollectiondate" runat="server" Text='<%#Eval("CollectionDate") %>'  onfocus="this.blur();"></asp:TextBox>
                                                                                                
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtcollectiondate" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtcollectiondate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Code" Visible="false" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="txtcode" runat="server" Text='<%#Eval("FormCode") %>' Visible="false"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField Visible="true">
                                                                                            <FooterTemplate>
                                                                                                <asp:LinkButton ID="btnAddIndividualEducation7" OnClick="btnAddForm7row_Click"
                                                                                                    runat="server" Font-Bold="true" Width="100px" ForeColor="Black" Text="Add New Row" />
                                                                                            </FooterTemplate>
                                                                                            <FooterStyle HorizontalAlign="Right" />
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>


                                                                            <%--form 8--%>

                                                                              <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">
                                                                                <asp:GridView ID="grdform8" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                                    AllowPaging="true" PageSize="100" AutoGenerateColumns="false" Font-Names="Arial"
                                                                                    Font-Size="12px" Width="100%" ShowFooter="true" OnRowDataBound ="GvForm8_RowDataBound">
                                                                                    <EmptyDataTemplate>
                                                                                        <div style="font-family: Arial; font-size: 11px; font-weight: bold;">
                                                                                            Data not found</div>
                                                                                    </EmptyDataTemplate>
                                                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                                                    <RowStyle HorizontalAlign="Left" />
                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />
                                                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Unique Code" Visible="false" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblUniqueCode" runat="server" Text='<%#Eval("UniqueCode") %>' Visible="false"></asp:Label>
                                                                                                
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Form 9 Received Date" Visible="true" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtrecivedate" runat="server"  Text='<%#Eval("ReceivedDate") %>'
                                                                                                    Visible="true"  onfocus="this.blur();"></asp:TextBox>
                                                                                                
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtrecivedate" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtrecivedate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Collection Date" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtcollectiondate" runat="server" Text='<%#Eval("CollectionDate") %>'  onfocus="this.blur();"></asp:TextBox>
                                                                                                
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtcollectiondate" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtcollectiondate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Code" Visible="false" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="txtcode" runat="server" Text='<%#Eval("FormCode") %>' Visible="false"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField Visible="true">
                                                                                            <FooterTemplate>
                                                                                                <asp:LinkButton ID="btnAddIndividualEducation8" OnClick="btnAddForm8row_Click"
                                                                                                    runat="server" Font-Bold="true" Width="100px" ForeColor="Black" Text="Add New Row" />
                                                                                            </FooterTemplate>
                                                                                            <FooterStyle HorizontalAlign="Right" />
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>


                                                                            <%--form 9--%>


                                                                             <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">
                                                                                <asp:GridView ID="grdform9" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                                    AllowPaging="true" PageSize="100" AutoGenerateColumns="false" Font-Names="Arial"
                                                                                    Font-Size="12px" Width="100%" ShowFooter="true" OnRowDataBound ="GvForm9_RowDataBound">
                                                                                    <EmptyDataTemplate>
                                                                                        <div style="font-family: Arial; font-size: 11px; font-weight: bold;">
                                                                                            Data not found</div>
                                                                                    </EmptyDataTemplate>
                                                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                                                    <RowStyle HorizontalAlign="Left" />
                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />
                                                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Unique Code" Visible="false" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblUniqueCode" runat="server" Text='<%#Eval("UniqueCode") %>' Visible="false"></asp:Label>
                                                                                                
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Form 9 Received Date" Visible="true" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtrecivedate" runat="server"  Text='<%#Eval("ReceivedDate") %>'
                                                                                                    Visible="true"  onfocus="this.blur();"></asp:TextBox>
                                                                                                
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtrecivedate" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtrecivedate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Collection Date" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtcollectiondate" runat="server" Text='<%#Eval("CollectionDate") %>'  onfocus="this.blur();"></asp:TextBox>
                                                                                                
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtcollectiondate" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtcollectiondate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Code" Visible="false" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="txtcode" runat="server" Text='<%#Eval("FormCode") %>' Visible="false"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField Visible="true">
                                                                                            <FooterTemplate>
                                                                                                <asp:LinkButton ID="btnAddIndividualEducation9" OnClick="btnAddForm9row_Click"
                                                                                                    runat="server" Font-Bold="true" Width="100px" ForeColor="Black" Text="Add New Row" />
                                                                                            </FooterTemplate>
                                                                                            <FooterStyle HorizontalAlign="Right" />
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>


                                                                             <%--form 12--%>


                                                                             <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">
                                                                              <div style="height: auto; overflow: auto; width: 99%;" align="center">
                                                    
                                                    <div class="row" style="width: 200%">
                                                                                <asp:GridView ID="grdform12" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                                    AllowPaging="true" PageSize="100" AutoGenerateColumns="false" Font-Names="Arial"
                                                                                    Font-Size="12px" Width="100%" ShowFooter="true" OnRowDataBound ="GvForm12_RowDataBound">
                                                                                    <EmptyDataTemplate>
                                                                                        <div style="font-family: Arial; font-size: 11px; font-weight: bold;">
                                                                                            Data not found</div>
                                                                                    </EmptyDataTemplate>
                                                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                                                    <RowStyle HorizontalAlign="Left" />
                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />
                                                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Unique Code" Visible="false" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblUniqueCode" runat="server" Text='<%#Eval("UniqueCode") %>' Visible="false"></asp:Label>
                                                                                                
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="TB Name" Visible="true" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                 <asp:DropDownList ID="ddltb"   runat="server" class="form-control "> </asp:DropDownList>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Baseline Receiving Date" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtrecivedate" runat="server" Text='<%#Eval("ReceivedDate") %>'  onfocus="this.blur();"></asp:TextBox>
                                                                                                
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtcollectiondate1" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtrecivedate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>

                                                                                         <asp:TemplateField HeaderText="Baseline Collection Date" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtcollectiondate" runat="server" Text='<%#Eval("CollectionDate") %>'  onfocus="this.blur();"></asp:TextBox>
                                                                                                
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtcollectiondate2" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtcollectiondate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>


                                                                                         <asp:TemplateField HeaderText="No. Of Girls" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtnoofgirls" runat="server" Text='<%#Eval("NoOfGirls") %>' MaxLength="3"
                                                                                                    onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                                                
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="No. Of Boys" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtNoOfBoys" runat="server" MaxLength="3" Text='<%#Eval("NoOfBoys") %>'
                                                                                                    onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                                                
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>


                                                                                         <asp:TemplateField HeaderText="Endline Collection Date" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtendrecivedate" runat="server" Text='<%#Eval("EndlineReceivingDate") %>'  onfocus="this.blur();"></asp:TextBox>
                                                                                                
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtcollectiondate12" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtendrecivedate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>

                                                                                         <asp:TemplateField HeaderText="Endline No. of Girls Present" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtendcollectiondate" runat="server" Text='<%#Eval("EndlineCollectionDate") %>'  onfocus="this.blur();"></asp:TextBox>
                                                                                                
                                                                                                <ajax:CalendarExtender ID="CalendarExtendertxtcollectiondate3" Enabled="true" runat="server"
                                                                                                    Format="dd/MM/yyyy" TargetControlID="txtendcollectiondate" PopupPosition="BottomRight">
                                                                                                </ajax:CalendarExtender>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>


                                                                                        <asp:TemplateField HeaderText="Endline No. of Girls Present" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtenoofgirls" runat="server" Text='<%#Eval("EndlineNoofGirls") %>' MaxLength="3"
                                                                                                    onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                                                
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Endline No. of Boys Present" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txteNoOfBoys" runat="server" MaxLength="3" Text='<%#Eval("EndlineNoofBoysPresent") %>'
                                                                                                    onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                                                
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Code" Visible="false" HeaderStyle-CssClass="GridHeaderClass">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="txtcode" runat="server" Text='<%#Eval("FormCode") %>' Visible="false"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField Visible="true">
                                                                                            <FooterTemplate>
                                                                                                <asp:LinkButton ID="btnAddIndividualEducation12" OnClick="btnAddForm12row_Click"
                                                                                                    runat="server" Font-Bold="true" Width="100px" ForeColor="Black" Text="Add New Row" />
                                                                                            </FooterTemplate>
                                                                                            <FooterStyle HorizontalAlign="Right" />
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                                </div>
                                                                                </div>
                                                                            </div>
                                                                              <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1" style="padding: 0px 3px 0px 5px;">
                                                                          </div>
                                                                            
                                                                        </div>
                                                                    </div>
                                                        </asp:Panel>
                                                    </div>
                                                    <%--<div class="col-lg-1">
                                                    </div>--%>
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
            <asp:HiddenField ID="hdnschoolcode" runat="server" />
            <asp:HiddenField ID="hdnvillagecode" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker4').datetimepicker();
        });
    </script>
</asp:Content>


