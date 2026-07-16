<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTeamBalikaTbtraining.aspx.cs"  Culture="en-GB" MasterPageFile="~/Site.master"
    Inherits="frmTeamBalikaTbtraining" %>

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
    </script>
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid">
        <%--<input type="image" id="ton-new" class="butt" src="Images/close.png"  />
       <div id="div-show-new"></div> --%>
    </div>
    <div class="container-fluid" style="margin-top: 0px;">
        <input type="image" id="left-pln" class="left-butt" src="Images/close-29.png" />
        <div id="div-show-left">
            <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="40"
                BorderStyle="None" DataKeyNames="UniqueCode" GridLines="None" AutoGenerateColumns="false" OnRowCommand="GVMain_OnRowCommand">
                <EmptyDataTemplate>
                    <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                        Data not found
                    </div>
                </EmptyDataTemplate>
                <FooterStyle CssClass="FooterStyle" />
                <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="44px" />
                <RowStyle HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                <AlternatingRowStyle BackColor="#f1f1f1" />
            
                <Columns>
                    <asp:ButtonField HeaderText="Location" ItemStyle-ForeColor="#333" DataTextField="DistrictName"
                        CommandName="GVUIO">
                        <ItemStyle CssClass="padding-lef" Height="30px" />
                        <HeaderStyle CssClass="padding-lef" />
                    </asp:ButtonField>
                    <asp:ButtonField HeaderText="From Date" ItemStyle-ForeColor="#333" DataTextField="FromDate"
                        CommandName="GVUIO">
                        <ItemStyle CssClass="padding-lef" Height="30px" />
                        <HeaderStyle CssClass="padding-lef" />
                    </asp:ButtonField>

                      <asp:ButtonField HeaderText="Todate" ItemStyle-ForeColor="#333" DataTextField="todate"
                        CommandName="GVUIO">
                        <ItemStyle CssClass="padding-lef" Height="30px" />
                        <HeaderStyle CssClass="padding-lef" />
                    </asp:ButtonField>

                    <asp:ButtonField HeaderText="Name" Visible="false" Text="Button" DataTextField="UniqueCode">
                    </asp:ButtonField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="x" class="wapp">
            <div class="row">
                <div class="col-lg-12" >
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6">
                                    <h3 class="text-danger" style="margin: 0px;">
                                        Team Balika Training
                                    </h3>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">
                                    <input type="image" id="ton-new" class="butt" src="Images/search-not-29.png" title="Search" />
                                    <asp:ImageButton ID="btnDelete" CssClass="btn btn-info pull-right" ToolTip="Delete"
                                        BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px;
                                        padding: 0px;" runat="server" />
                                    <asp:ImageButton ID="btnsave"  OnClick="btnsave_Click" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                        ToolTip="Save" ImageUrl="~/images/save-29-1.png" ValidationGroup="saves" Style="margin-right: 5px;
                                        padding: 0px;" runat="server" />
                                    <asp:ImageButton ID="btnAdd" OnClick="btnAdd_Click" Visible="false"  CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                        ToolTip="Add" ImageUrl="~/images/add-29-1.png" Style="margin-right: 5px; padding: 0px;"
                                        runat="server" />
                                </div>
                            </div>
                        </div>
                        <div>
                        </div>

                           <div id="div-show-new">
                    <div class="row marg search-bg">
  <div  class="form-horizontal">
  <%-- <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>--%>
      
                                           
               <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                <div class="form-group">
                                                    <label class="control-label col-sm-4" for="Name" style="color: black; width: 39%; padding-left: 0px; padding-right: 0px;">
                                                        Learning Outcome<span class="req">*</span></label>
                                                    <div class="col-sm-7">
                                                     
                                                        <asp:DropDownList ID="ddlLearning"  runat="server" class="form-controlNew">
                                                         
                                                        </asp:DropDownList>
                                         <span style="width: 5px;float: right;margin: -27px 27px;font-size: 21px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server" Display="Dynamic" ValidationGroup="saves" 
                             ControlToValidate="ddlTraining" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                             </span>
                                         
                                                    </div>
                                                </div>
                                            </div>
                           <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                <div class="form-group">
                                                    <label class="control-label col-sm-4 " for="Name" style="color: black;">
                                                        Training type
                                                    </label>
                                                    <div class="col-sm-7 ">
                                                   
                                                        <asp:DropDownList ID="ddlTraining" style="margin-left: -32px;" runat="server" class="form-controlNew">
                                                          
                                                        </asp:DropDownList>
                                                                                           <span style="width: 5px;float: right;margin: -27px 27px;font-size: 21px;">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" runat="server" Display="Dynamic" ValidationGroup="saves" 
                             ControlToValidate="ddlTraining" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                             </span>
                                                    </div>
                                                </div>
                                            </div>
                  
<%--</ContentTemplate>
</asp:UpdatePanel>--%>



                                        	<div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                            	 <asp:ImageButton ID="imNewSerach" OnClick="btnNewSerach_Click" ToolTip="Serach" runat="server"  class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />
 
                                                            </div> 
                                            </div>           
                                                                                  
            	</div>
                    </div>

                           
                      <div class="col-lg-12 table table-hover " style="padding: 0px;">
                                        <asp:Panel ID="pnlMain" Enabled="true" runat="server">
                                            <div class="form-horizontal">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                    <div style=" height: 490px; overflow:auto;  width: 99%;" align="center">
                                                        <div>
                                                            <div class="Row" style="width: 100% ">
                                                               <asp:GridView ID="gvnroll" OnRowDataBound="gvnroll_RowDataBound" Width="100%"  runat="server"
                                                CssClass="Grid"  AutoGenerateColumns="false">
                                                <EmptyDataTemplate>
                                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                                        Data not found
                                                    </div>
                                                </EmptyDataTemplate>
                                                <FooterStyle CssClass="FooterStyle" />
                                                <HeaderStyle BackColor="#C1C1C1" Height="44px" />
                                                <RowStyle HorizontalAlign="Left" />
                                                <SelectedRowStyle BackColor="#897A7A" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                <Columns>
                                                  <asp:TemplateField HeaderText="TB Name"  HeaderStyle-CssClass="GridHeaderClass" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTBName" runat="server" Text='<%#Eval("TBName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="TB Code" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text='<%#Eval("TBCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Block Name" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSerial" runat="server" Text='<%#Eval("BlockName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Village Name" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVillageCode" runat="server" Text='<%#Eval("VillageName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                  
                                                      <asp:TemplateField HeaderText="TBDate" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTBDate" runat="server" Text='<%#Eval("TBDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="TBDate" Visible="false" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniqueCode" runat="server" Text='<%#Eval("UniqueCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="TBDate" Visible="false" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTBID" runat="server" Text='<%#Eval("TBID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="Attendance"  HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChkClose" runat="server"  />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                                    
                                                                      
                                                                   
                                                                   
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                       
                    </div>
                </div>
                <!-- /#page-content-wrapper -->
            </div>
            <!-- /#wrapper -->
            <!-- /#wrapper -->
        </div>
    </div>


</asp:Content>
