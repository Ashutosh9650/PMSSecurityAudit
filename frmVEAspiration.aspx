<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmVEAspiration.aspx.cs" Inherits="frmVEAspiration" %>

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
            var phoneno = /^[0-9.]+$/;
           
            if (phoneno.test(inputtxt)) {
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


    <script type="text/javascript">
        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=ddl_aspiration.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    if (i = 3) {
                        isValid = true;
                        break;
                    }
                }
            }
            args.IsValid = isValid;
        }
</script>

<script type = "text/javascript">

    var atLeast = 3

    function Validate() {

        var CHK = document.getElementById("<%=ddl_aspiration.ClientID%>");

        var checkbox = CHK.getElementsByTagName("input");

        var counter = 0;

        for (var i = 0; i < checkbox.length; i++) {

            if (checkbox[i].checked) {

                counter++;

            }

        }

        if (atLeast != counter) {
            if (atLeast < counter) {
                alert("Please select only " + atLeast + " preferences");

                return false;
            }


            alert("Please select atleast " + atLeast + " preferences");

            return false;

        }

        return true;

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
<asp:UpdatePanel runat="server" ID="mkiiainpnl121">
        <ContentTemplate>
  
    <div class="container-fluid">
        <%--<input type="image" id="ton-new" class="butt" src="Images/close.png"  />
       <div id="div-show-new"></div> --%>
    </div>
  
      <div class="container-fluid" style="margin-top: 0px;">
           	<div class="row" >
        <div class="col-lg-2 col-md-2 col-sm-3" style="padding-right: 0px;" >
       <div class="thumbnail" style="min-height:750px;width: 228px;"> 	         
        <div style="padding-top:3px;">
           <%--<span style="float:left"> <asp:Label ID="lblsearch" runat="server" Text="Search:" ForeColor="Black"></asp:Label></span>--%>
             <span style="float:right;padding-right:1px;"><asp:TextBox ID="txtSearchName" Visible="false" runat="server" OnTextChanged="txtSearchName_Click" AutoPostBack="true" CssClass="form-control col-lg-1"></asp:TextBox></span>
        </div>
        <div style="overflow: auto; margin-top:35px; height:750px; ">
            <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="40"
                BorderStyle="None" DataKeyNames="UniqueCode" GridLines="None" AutoGenerateColumns="false"
                OnRowCommand="GVMain_OnRowCommand" OnPageIndexChanging="GV_Project_PageIndexChanging">
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
                <%-- <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                    <AlternatingRowStyle BackColor="#f1f1f1" />--%>
                <Columns>
                    <asp:ButtonField HeaderText="Code " ItemStyle-ForeColor="#333" DataTextField="TBCode"
                        CommandName="GVUIO">
                        <ItemStyle CssClass="padding-lef" Height="30px" />
                        <HeaderStyle CssClass="padding-lef" />
                    </asp:ButtonField>
                    <asp:ButtonField HeaderText="Name " ItemStyle-ForeColor="#333" DataTextField="TBName"
                        CommandName="GVUIO">
                        <ItemStyle CssClass="padding-lef" Height="30px" />
                        <HeaderStyle CssClass="padding-lef" />
                    </asp:ButtonField>
                    <asp:ButtonField HeaderText="Name" Visible="false" Text="Button" DataTextField="UniqueCode">
                    </asp:ButtonField>
                </Columns>
            </asp:GridView>
        </div>
        </div>
    </div>
    <div  class="col-lg-10 col-md-10 col-sm-9">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6">
                                <h3 class="text-danger" style="margin: 0px;">
                                    Team Balika Aspiration</h3>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">
                                <input type="image" id="ton-new" class="butt" src="Images/search-not-29.png" title="Search" />
                                <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right"
                                    ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px;
                                    padding: 0px;" runat="server" />
                                <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                    ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnsave_Click" ValidationGroup="saves"
                                    Style="margin-right: 5px; padding: 0px;" runat="server" />
                                <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
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
                                        <%-- <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>--%>

               <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                <div class="form-group" style="margin-bottom: 7px;">
                                                    <label for="email" class="col-sm-3 padd linhei">
                                                        Year:</label>
                                                    <div class="col-sm-9 padd">
                                                         <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"  class="form-control ">
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
                                        <%--</ContentTemplate>
</asp:UpdatePanel>--%>
                                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-right"
                                                BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <asp:Panel ID="pnlMain" Enabled="false" runat="server">
                                    <div class="form-horizontal">
                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">
                                            <fieldset class="scheduler-border">
                                                <legend class="scheduler-border">Aspiration Details </legend>
                                                <div class="form-group">
                                                    <label class="control-label col-sm-4" for="Name">
                                                        Name of Team Balika</label>
                                                    <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlTbname" OnSelectedIndexChanged="ddlTbname_SelectedIndexChanged"
                                                        AutoPostBack="true" runat="server" class="form-control " Style="width: 85%;" >
                                                          <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                          </asp:DropDownList>
                                                    <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                                            ValidationGroup="saves" ControlToValidate="ddlTbname" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </span>
                                                </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-sm-4" for="Name">
                                                        Last Education</label>
                                                     <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlEducation" runat="server" class="form-control" Style="width: 85%;">
                                                        </asp:DropDownList>
                                                       
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-sm-4" for="Name">
                                                        Education Status</label>
                                                   <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlEducationStatus" runat="server" class="form-control" Style="width: 85%;">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                    <asp:ListItem Value="1"> Pursuing</asp:ListItem>
                                                                    <asp:ListItem Value="2">Completed</asp:ListItem>
                                                        </asp:DropDownList>
                                                        
                                                    </div>
                                                </div>
                                            
                                                
                                                <div class="form-group">
                                                    <label class="control-label col-sm-4" for="Name">
                                                        Livelihood Engagement </label>
                                                    <div class="col-sm-8">
                                                        
                                                                <asp:DropDownList ID="ddlLHE" runat="server" AutoPostBack="true" Style="width: 85%;"
                                                                    OnSelectedIndexChanged="ddlLHE_SelectedIndexChanged" class="form-control">
                                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                    <asp:ListItem Value="1">Yes </asp:ListItem>
                                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                                </asp:DropDownList>
                                                            
                                                            </div>
                                                            </div>
                                                             <div class="form-group">
                                                    <label class="control-label col-sm-4" for="LET">
                                                        Livelihood Engagement Type </label>
                                                    <div class="col-sm-8">
                                                       
                                                                <asp:DropDownList ID="ddlLHEType" runat="server" AutoPostBack="true" Style="width: 85%;"
                                                                    class="form-control">
                                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                    <asp:ListItem Value="1">Part Time </asp:ListItem>
                                                                    <asp:ListItem Value="2">Full Time</asp:ListItem>
                                                                </asp:DropDownList>
                                                           
                                                    </div>
                                                </div>
                                               
                                                <div class="form-group">
                                                    <asp:Label class="control-label col-sm-4" runat="server" ID="lblDob" Text="Monthly Income"></asp:Label>
                                                    <div class="col-sm-8">
                                                        <div class="input-group">
                                                            <asp:TextBox runat="server" ID="txtMI" autocomplete="off"  onkeypress="return isNumberKey(this,event);" onchange="javascript: phonenumber(this.value,'TeContact1');"
                                                             ondrop="return false;" class="form-control TeContact1 "></asp:TextBox>
                                                          
                                                       </div>
                                                    </div>
                                                </div>

                                                 <%-- <div class="form-group">
                                                    <label class="control-label col-sm-4" for="LET">
                                                        Aspiration </label>
                                                    <div class="col-sm-8">
                                                       
                                                                <asp:DropDownList ID="ddlasp" runat="server" AutoPostBack="true" DataTextField="Aspiration" Style="width: 85%;"
                                                                    class="form-control">
                                                                  
                                                                </asp:DropDownList>
                                                           
                                                    </div>
                                                </div>--%>

                                                 <div class="form-group">
                                                    <label class="control-label col-sm-4" for="aspiration">
                                                        Preferences of Aspiration(select any three) </label>
                                                         <%--<span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" InitialValue="0" runat="server"
                                                                Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddl_aspiration" ErrorMessage="*"
                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </span>--%>
                                                    <div class="col-sm-8">
                                                        <div style="width: 100%;">
                                                             <span style="float: left; height: 42%;">
                                                                <asp:CheckBoxList ID="ddl_aspiration"  runat="server"    >
                                                        </asp:CheckBoxList>
                                                       <%-- <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please select at least one item."
    ForeColor="Red"  ClientValidationFunction="ValidateCheckBoxList" runat="server" />--%>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                   
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                    <div class="thumbnail" style="float: left; width: 100%;">
                                        <div class="col-lg-4 col-md-4 col-xs-12 col-sm-6  col-lg-offset-4 col-md-offset-4 col-sm-offset-3 col-xs-offset-0  ">
                                            <asp:ImageButton ID="btnSUmbit" ToolTip="Save"  ValidationGroup="saves" OnClick="btnsave_Click" OnClientClick="if (!Validate()) { return false;};"
                                                ImageUrl="~/images/Sumbit.jpg" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
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
    <asp:Label ID="HdnStartYear" Visible="false" runat="server" />
    </ContentTemplate>
 
    </asp:UpdatePanel>
</asp:Content>
