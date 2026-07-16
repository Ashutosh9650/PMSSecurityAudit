<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="frmEnrollmentForm6.aspx.cs"  Culture="en-GB" Inherits="frmEnrollmentForm6" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
    <script type="text/javascript">
        function validateFristNumeric(txt) {
            debugger;
            var firstChar = txt.value.charAt(0);
            if (firstChar == 0) {
                //do your stuff
                alert("Please enter correct DISE Code");
                txt.value = "";
               
            }
            else {
                return true;
            }


        }
        function validateFristNumeric1(txt) {
            debugger;
            var firstChar = txt.value.charAt(0);
            if (firstChar == 0) {
                //do your stuff
                alert("Please enter correct SR No");
                txt.value = "";

            }
            else {
                return true;
            }


        }
        function validateFristNumeric2(txt) {
            debugger;
            var firstChar = txt.value.charAt(0);
            if (firstChar == 0) {
                //do your stuff
                alert("Please enter correct Samgra ID");
                txt.value = "";

            }
            else {
                return true;
            }


        }
        function checkSpace() {
           
            var val = document.getElementById('<%=txtFatherName.ClientID %>').value;
            var val1 = document.getElementById('<%=txtChildName.ClientID %>').value;
            var val2 = document.getElementById('<%=txtmotherName.ClientID %>').value;
            var val3 = document.getElementById('<%=txtSrno.ClientID %>').value;
            var val4 = document.getElementById('<%=txtHHNo.ClientID %>').value;
          
            if (val.charAt(0) == " ") {
                alert("First space cannot be blank")
                return false;
            }
            if (val1.charAt(0) == " ") {
                alert("First space cannot be blank")
                return false;
            }

            if (val2.charAt(0) == " ") {
                alert("First space cannot be blank")
                return false;
            }

            if (val3.charAt(0) == " ") {
                alert("First space cannot be blank")
                return false;
            }

            if (val4.charAt(0) == " ") {
                alert("First space cannot be blank")
                return false;
            }


            if (!/^[a-zA-Z ]*$/gm.test(val)) {
                alert("Only characters allow");
            
                return false;
            }
           

            if (!/^[a-zA-Z ]*$/gm.test(val1)) {
                alert("Only characters allow");

                return false;
            }
            
            if (!/^[a-zA-Z ]*$/gm.test(val2)) {
                alert("Only characters allow");
            
                return false;
            }
           
            if (!/^[a-zA-Z0-9 ]*$/gm.test(val3)) {
                alert("Only characters allow");

                return false;
            }
           


            if (!/^[a-zA-Z0-9 ]*$/gm.test(val4)) {
                alert("Only characters allow");

                return false;
            }
            disableBtn();
            return true;
        }
    </script>
     <script language="Javascript" type="text/javascript">
         function disableBtn() {
        
        document.getElementById('ImageButton1').disabled = true;
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
    
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="container-fluid" >
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="thumbnail" style="background-color: #f5f5f5; margin-bottom: 3px !important;">
                            <div class="panel-heading" style="padding: 0px 0px;">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <h3 class="text-danger" style="margin: 0px;">
                                            Enrollment Entry</h3>
                                              <asp:ImageButton ID="ImageButton2" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                ToolTip="Add" ImageUrl="~/images/add-29-1.png"  OnClick="Add_Click"
                                                Style="margin-right: 5px; padding: 0px;margin-top:-29px;" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top:8px;">
                    <div class="thumbnail" style="background-color: #f5f5f5; float: left; width: 100%;">
                        <div class="panel panel-default" style="margin-bottom:0px">
                            <div class="form-horizontal">
                                <div class="row" style="padding: 0px 10px;">
                                    <div id="div-show-new" style="width: 100%;right:0%;text-align: left;">
                                        <div class="row marg search-bg">
                                            <div class="form-horizontal">
                                                <%-- <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>--%>
                                                <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
	<ContentTemplate>
                                                --%>
                                                <div class="row">
                                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 linhei" style="padding-right: 0px;    padding-left: 7px;">
                                                                Year:
                                                            </label>
                                                            <div class="col-sm-9 padd">
                                                                <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                                    class="form-control ">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
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
                                                                <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                                    AutoPostBack="true" class="form-control " />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                                                Block:</label>
                                                            <div class="col-sm-9 padd">
                                                                <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                                    class="form-control " />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div id="Div1" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                                                Panchayat:</label>
                                                            <div class="col-sm-9 padd">
                                                                <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"
                                                                    class="form-control " />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="Div2" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
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
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                                                School:
                                                            </label>
                                                            <div class="col-sm-9 padd">
                                                                <asp:DropDownList ID="ddlSchool"  OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged"
                                                                    AutoPostBack="true" runat="server" class="form-control ">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server" id="IDschool" visible="false">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                                                School Name:
                                                            </label>
                                                            <div class="col-sm-9 padd">
                                                                 <asp:TextBox ID="txtSchooName"   MaxLength="50" autocomplete="off" ondrop="return false;" runat="server"
                                                                CssClass="form-control" TabIndex="5"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                      <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server" id="IDDise" visible="false">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                                                Govt DiseCode:
                                                            </label>
                                                            <div class="col-sm-9 padd">
                                                                <asp:TextBox ID="txtDiseCode" onchange="return validateFristNumeric(this);"   onkeypress="return isNumberKey(this,event);" 
                                                                MaxLength="11" autocomplete="off" ondrop="return false;" runat="server"
                                                                CssClass="form-control" TabIndex="5"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                      <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server" id="Div13" visible="false">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                                               School Level:
                                                            </label>
                                                            <div class="col-sm-9 padd">
                                                              
                                                                                        <asp:DropDownList ID="ddlschoolLevel" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Primary </asp:ListItem>
                                                                <asp:ListItem Value="2">Upper Primary </asp:ListItem>
                                                                <asp:ListItem Value="3">Secondary</asp:ListItem>
                                                                <asp:ListItem Value="4">Senior Secondary</asp:ListItem>
                                                              
                                                                                 <asp:ListItem Value="6">Madrasa with FLN</asp:ListItem>
                                                                          <asp:ListItem Value="7">Maa Badi</asp:ListItem>
                                                                             <asp:ListItem Value="9">ANGANWARI</asp:ListItem>
                                                                           <asp:ListItem Value="10">KGBV with school</asp:ListItem>
                                                                          <%--  <asp:ListItem Value="11">KGBV without school</asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                      <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server" id="Div14" visible="false">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-right: 0px;">
                                                               Management:
                                                            </label>
                                                            <div class="col-sm-9 padd">
                                                              
                                                                                        <asp:DropDownList ID="ddlManagement" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">GOVERNMENT </asp:ListItem>
                                                                <asp:ListItem Value="2">PRIVATE </asp:ListItem>
                                                              
                                                            </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                 <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                        <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" OnClick="btnSerach_Click"
                                                            class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />
                                                     </div>
                                                    
                                                </div>
                                                
                                                <%--</ContentTemplate>
</asp:UpdatePanel>
                                                --%>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 table table-hover " style="padding: 0px;">
                                            <asp:Panel ID="pnlMain" runat="server">
                                                <asp:UpdatePanel runat="server" ID="UpdatedddddddPanel1">
                                                    <ContentTemplate>
                                                        <div class="form-horizontal">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                                <div style="height: 290px; overflow: auto; width: 100%;" align="center">
                                                                    <div>
                                                                        <div class="Row" style="width: 100%">
                                                                           <asp:GridView ID="gvnroll" runat="server"  CssClass="table table-striped table-bordered table-hover" DataKeyNames="UniqueChildCode" OnRowDataBound="gvnroll_OnRowCommand"   AutoGenerateColumns="False"  Font-Names="Arial"
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
                                                                             <asp:LinkButton ID="lbtn"   runat="server" Text="EDIT" OnClick="LnkBtnBlock_OnClick"  CommandArgument='<%# Bind("UniqueChildCode") %>'  ></asp:LinkButton>
                                                                                <asp:Label ID="lblCUniqueChildCode" Visible="false" BackColor="Transparent" runat="server" Text='<%# Bind("UniqueChildCode") %>' CssClass="form-controlAbhi"></asp:Label>
                                                                             </ItemTemplate>
                                                                              </asp:TemplateField>

                                                                                 <asp:TemplateField HeaderText="Action"  HeaderStyle-Width="15%" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImgAcc" runat="server"  OnClick="btn_Delete_Click" ImageUrl="~/images/delete-29.png"
                                                                Width="15px" Height="15px"></asp:ImageButton>
                                                            
                                                        </ItemTemplate>
                                                       <HeaderStyle Width="5%" />
                                                        <ItemStyle  HorizontalAlign="Center"/>
                                                    </asp:TemplateField>

                                                                              <asp:TemplateField >
                                                                           <ItemTemplate>
                                                                             <asp:LinkButton ID="ldddbtn"   runat="server" Text="Unmatch"  OnClick="btn_Un_Click"   CommandArgument='<%# Bind("UniqueChildCode") %>'  ></asp:LinkButton>
                                                                                
                                                                             </ItemTemplate>
                                                                              </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Unique ID"  Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDistrictName" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("Uniqueid") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                    <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Village Name"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPanchayatName" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("VillageName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                          
                                                                            <asp:TemplateField HeaderText="Student Name"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVillageName"  ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("ChildName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Father Name" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSurvayD3ate"  ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("FathersName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="HHNo" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblBlockName"  ForeColor="Black" runat="server" Text='<%# Eval("HHNo1") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Class" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMauhalla2"  ForeColor="Black" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            
                                                                            <asp:TemplateField HeaderText="SR.NO"  Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblHouse2"  ForeColor="Black" runat="server" Text='<%# Eval("Serial") %>'></asp:Label>
                                                                                                          </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Admission Date" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ddlEmployee2Code" class="labelGrid" ForeColor="Black" ItemStyle-ForeColor="#333"
                                                                                        runat="server" Text='<%# Eval("EnrolmentDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            
                                                                           <asp:TemplateField HeaderText="DOB" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblHRAyye"  ForeColor="Black" runat="server" Text='<%# Eval("DOB") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                          
                                                                           
                                                                            <asp:TemplateField HeaderText="Social Category" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSalaryPayaeble" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("SocialCategory") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Gender" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblBasirrc" ForeColor="Black" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                               <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            
                                                                            <asp:TemplateField HeaderText="School Name" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSchool" class="labelGrid" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("School") %>'></asp:Label>
                                                                                          <asp:Label ID="lblSchoolID" Visible="false" class="labelGrid" ForeColor="Black" runat="server"
                                                                                        Text='<%# Eval("SchoolCode") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>

                                                                              <asp:TemplateField HeaderText="Remark" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblBasgrc" ForeColor="Black" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                               <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>

                                                                              <asp:TemplateField HeaderText="Enrollment Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMediecal"  ForeColor="Black" runat="server" Text='<%# Eval("EduationStatus") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Enrollment Category" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAlflowaeecee"  ForeColor="Black" runat="server" Text='<%# Eval("EnrolmentCategory") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                          
                                                                          
                                                                            <asp:TemplateField HeaderText="Education Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblUniqueChildCode"  ForeColor="Black" runat="server" Text='<%# Eval("UniqueChildCode") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                  <ItemStyle CssClass="padding-lef" />

                                                                            </asp:TemplateField>
                                                                          <asp:TemplateField HeaderText="Education Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStatus"  ForeColor="Black" runat="server" Text='<%# Eval("Status") %>'></asp:Label>

                                                                                    <asp:Label ID="lblD2dChildCode"  ForeColor="Black" runat="server" Text='<%# Eval("D2dChildCode") %>'></asp:Label>
                                                                            <asp:Label ID="lblCreatedate"  ForeColor="Black" Visible="false" runat="server" Text='<%# Eval("Createdate") %>'></asp:Label>
                                                                       
                                                                          
                                                                          
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
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="gvnroll" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- /#wrapper -->
                            <!-- /#wrapper -->
                        </div>
                    </div>
                </div>
                <script type="text/javascript">
                    $(function () {
                        $('#datetimepicker4').datetimepicker();
                    });
                </script>
            </div>


             <cc1:ModalPopupExtender ID="MpexdrDistrict" runat="server" BackgroundCssClass="modalBg "
                                        CancelControlID="CancelButton" PopupControlID="PnlDistrict" TargetControlID="HdnFild">
                                    </cc1:ModalPopupExtender>
                                    <asp:HiddenField ID="HdnFild" runat="server"></asp:HiddenField>
                                    <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important;
                                        margin-top: -75.5px !important;" ID="PnlDistrict" runat="server">
                                        <div style="width: 100%; height: auto; background-color: #f1f1f1">
                                            <div class="modal-header" style="background-color: #ddd; color: White;">
                                                <h4 class="modal-title" style="forecolor: White">
                                                    </h4>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row" >
                                               
                                            
                                                <div id="Div3" class="col-lg-12 col-md-12 col-sm-12 cpl-xs-12" visible="false" runat="server">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" >
                                                            School:</label>
                                                        <div   style="padding-left: 15px;">
                                                             <asp:Label ID="lblSchool" class="padd " ForeColor="Black"  runat="server" Text="Label" ></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                  
                                    
                                             

                                                                  
        

                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">


                                                                <div   class="form-horizontal">
                        <div class="row">
                                      <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name"style="padding-top:14px">TBFC <span class="req">*</span></label>
                                      
                                       <div class="col-sm-6">
                          <asp:DropDownList ID="ddlFC" class="form-control" runat="server"  OnSelectedIndexChanged="ddlFC_SelectedIndexChanged" AutoPostBack="true">
                               <asp:ListItem Value="0">--Select--</asp:ListItem>
                              <asp:ListItem Value="1">FC</asp:ListItem>
                               <asp:ListItem Value="2">TB</asp:ListItem>
                               <asp:ListItem Value="3">Join Visit</asp:ListItem>
                          </asp:DropDownList>
                             <span style="width: 5px;float: right;margin: -27px 27px;font-size: 21px;">
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="ddlFC" InitialValue="0" ErrorMessage="*" ForeColor="Red"
                        ValidationGroup="Valid">

                        </asp:RequiredFieldValidator></span>
                                </div>
                            </div>
                        </div>

                                                       <div   class="form-horizontal" runat="server" id="pnlTb" visible="false">
                        <div class="row">
                                      <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name"style="padding-top:14px">TBName <span class="req">*</span></label>
                                      
                                       <div class="col-sm-6">
                          <asp:DropDownList ID="ddlTbName" class="form-control" runat="server"></asp:DropDownList>
                      

                                </div>
                            </div>
                        </div></div>

                                      

                    <div   class="form-horizontal">
                        <div class="row">
                                      <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name"style="padding-top:14px">Student Name  <span class="req">*</span></label>
                                      
                                       <div class="col-sm-6">
                       <asp:TextBox ID="txtChildName" class="form-control" onpaste="return false" onkeypress="return onlyAlphabets(event,this);" autocomplete="off" ondrop="return false;" ForeColor="Black" runat="server"
                                                                                       ></asp:TextBox>
                      

                                </div></div>
                                      </div>
                                  

                                    <div  class="row">
                                      <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name"style="padding-top:14px">Father Name <span class="req">*</span></label>
                                      
                                       <div class="col-sm-6">
                             
                                 <asp:TextBox ID="txtFatherName" class="form-control" onpaste="return false" onkeypress="return onlyAlphabets(event,this);" autocomplete="off" ondrop="return false;" ForeColor="Black" runat="server"
                                                                                        ></asp:TextBox>
                                                    
                   
</div></div>
                                </div>

                               <div  class="row">
                                      <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name"style="padding-top:14px">Mother Name <span class="req">*</span></label>
                                      
                                       <div class="col-sm-6">
                             
                                 <asp:TextBox ID="txtmotherName" class="form-control" onpaste="return false" onkeypress="return onlyAlphabets(event,this);" autocomplete="off" ondrop="return false;" ForeColor="Black" runat="server"
                                                                                        ></asp:TextBox>
                                                    
                   
</div></div>
                                </div>
                                     <div class="row" id="Div4" runat="server">
                                <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name" id="Label2" runat="server">Class <span class="req">*</span></label>
                                      
                                       <div class="col-sm-6">
                                                              
             <asp:DropDownList ID="dllClass" class="form-control" runat="server">
                                                                                    </asp:DropDownList>
                                
                                </div>
                                </div>
                                
                                </div>
                                         <div class="row" id="Div5" runat="server">
                                <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name" id="Label3" runat="server">SR NO.<span class="req">*</span></label>
                                      
                                       <div class="col-sm-6">
                              <asp:TextBox ID="txtSrno" class="form-control"  onpaste="return false"  ForeColor="Black" runat="server" onchange="return validateFristNumeric1(this);"   autocomplete="off" ondrop="return false;" ></asp:TextBox>
                                                           
                                </div>
                                </div>
                                
                                </div>

                                <div id="Div6" runat="server"  class="row">
                                	<div class="form-group">
                                      <label class="control-label col-sm-4" for="Name" id="Label4" runat="server">Admission Date<span class="req">*</span>  </label>
                                      
                                       <div class="col-sm-8">
                           <asp:TextBox runat="server"  ID="txtBirth" Width="73%"  onpaste="return false" autocomplete="off" ondrop="return false;"  class="form-control" onkeypress="return false;"                     
                                               ></asp:TextBox>
                                         
                                            <ajax:CalendarExtender ID="clk"  runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" TargetControlID="txtBirth" OnClientDateSelectionChanged="arrivaldatecheck" PopupPosition="BottomRight"></ajax:CalendarExtender>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBirth"
                                                Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
      

                    
                                </div>
                                </div>
                                </div>
                               <div id="Divkj2" runat="server"  class="row">
                                	<div class="form-group">
                                      <label class="control-label col-sm-4" for="Name" id="Label1" runat="server">DOB<span class="req">*</span> </label>
                                      
                                       <div class="col-sm-8">
                           <asp:TextBox runat="server"  ID="txtDobDate"  onpaste="return false" Width="73%"  autocomplete="off" ondrop="return false;"  class="form-control" onkeypress="return false;"                     
                                               ></asp:TextBox>
                                         
                                            <ajax:CalendarExtender ID="CalendarExtender1"  runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" TargetControlID="txtDobDate" OnClientDateSelectionChanged="arrivaldatecheck" PopupPosition="BottomRight"></ajax:CalendarExtender>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDobDate"
                                                Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>
      

                        
                                </div>
                                </div>
                                </div>
                                        <div class="row" id="Div7" runat="server">
                                <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name" id="Label5" runat="server">Social Category<span class="req">*</span></label>
                                      
                                       <div class="col-sm-6">
                          <asp:DropDownList ID="ddlScat" class="form-control" runat="server"></asp:DropDownList>
                                
                                </div>
                                </div>
                                
                                </div>

                                    <div id="a" runat="server" class="row">
                                	<div class="form-group">
                                      <label class="control-label col-sm-4" for="Name">Gender  <span class="req">*</span> </label>
                                      
                                       <div class="col-sm-6">
                               <asp:DropDownList ID="ddlGender" CssClass="form-control"  runat="server"  >
                                  <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">1-Male </asp:ListItem>
                                         <asp:ListItem Value="2">2-Female</asp:ListItem>
                               </asp:DropDownList>
                                              
                               
                                <span style="width: 5px;float: right;margin: -27px 27px;font-size: 21px;">
                               <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel" runat="server" 
                        ControlToValidate="ddlGender" InitialValue="0" ErrorMessage="*" ForeColor="Red"
                        ValidationGroup="Valid">

                        </asp:RequiredFieldValidator></span>
                                </div></div>
                                </div>
                                   <div class="row" id="Div9" runat="server">
                                <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name" id="Label10" runat="server">Samgra ID<span class="req">*</span></label>

                                       <div class="col-sm-6">
                                        <asp:TextBox ID="txtSamgra" onpaste="return false"  onchange="return validateFristNumeric2(this);" onkeypress="return isNumberKey(this,event);" MaxLength="9" class="form-control" runat="server" ></asp:TextBox>
                                                  
                                                  
                            
                                 
                                </div>
                                </div>


                                </div>
                                  <div class="row">
                                	<div class="form-group">
                                      <label class="control-label col-sm-4" for="Name">House/Family No</label>
                                     
                                       <div class="col-sm-6">
                                                     <asp:TextBox ID="txtHHNo" onpaste="return false" class="form-control" onkeypress="return onlyAlphabetsHH(event,this);"  onchange="checkPwd(this.value);"  autocomplete="off" ondrop="return false;"  ForeColor="Black" runat="server"></asp:TextBox>
                                                       
                                                                                                                                  
                                
                                                    
                                </div>
                                </div>
                                </div>
                                   
                        <div id="Div12" runat="server" class="row">
                                	<div class="form-group">
                                      <label class="control-label col-sm-4" for="Name">Remarks   </label>
                                      
                                       <div class="col-sm-6">
                               <asp:DropDownList ID="ddlRemarks" CssClass="form-control"  runat="server"  >
                                  <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Not in SR register but record available in portal</asp:ListItem>
                                         <asp:ListItem Value="2">Not in SR register but record available in attendance register</asp:ListItem>
                                    <asp:ListItem Value="3">Not in SR register but record available in temp SR register</asp:ListItem>
                               </asp:DropDownList>
                                              
                            
                                </div>

                                	</div>
                                </div>
                             
                               

                              <div class="row" id="Div8" runat="server" visible="false">
                                <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name" id="Label6" runat="server">Previous Educational Status</label>

                                       <div class="col-sm-6">
                               <asp:DropDownList ID="ddlEnroll" class="form-control" runat="server">
                                                                                    </asp:DropDownList>
                                
                                </div>
                                </div>
                                
                                </div>

                                  <div class="row"  runat="server" >
                                <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name" id="Label7" runat="server">Enrollment Category</label>

                                       <div class="col-sm-6">
                                       <asp:DropDownList ID="ddlEduationStatus" class="form-control" runat="server">
                                        </asp:DropDownList>
                                 
                                </div>

                                </div>
                                   <div class="row" id="Div10" runat="server" visible="false">
                                <div class="form-group">
                                      <label class="control-label col-sm-4" for="Name" id="Label8" runat="server">D2D Survey Village</label>

                                       <div class="col-sm-6">
                                          <asp:TextBox ID="txtSurveyVillage" class="form-control" MaxLength="50" runat="server" ></asp:TextBox>
                                                  
                            
                                 
                                </div>
                                </div>
                               
                                
                                </div>

                                </div>
                </div>
            </div>
                                            </div>
                                            <div class="modal-footer">
                                              <div id="Div11" class="col-lg-12 col-md-12 col-sm-12 cpl-xs-12" runat="server">
                                              <asp:ImageButton ID="ImageButton1" Style="margin-top:-18px" ValidationGroup="Valid" class="btn btn-primary pull-pull-right" 
                                                             OnClientClick="return checkSpace();"                   ToolTip="Save" runat="server" OnClick="btSave_Click" BackColor="#f5f5f5"  ImageUrl="~/images/save-29-1.png" /></span>

                                                <asp:ImageButton ID="CancelButton" ImageUrl="~/images/close-29.png" runat="server"
                                                    Text="Close" ToolTip="Close" Style="float: none;"></asp:ImageButton></div>
                                                    </div>
                                        </div>
                                    </asp:Panel>
        </ContentTemplate>
        <Triggers>
      
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
