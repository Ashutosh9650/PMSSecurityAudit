<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"  Culture="en-GB"
    CodeFile="frmVillageprofile.aspx.cs" Inherits="frmVillageprofile" %>
        <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode > 48 && charCode < 57) || charCode == 32 || charCode == 0 || charCode == 9 || charCode == 08)
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


        function DiscCode(inputtxt, txtid) {
            var phoneno = /^\d{11}$/;
            if (phoneno.test(inputtxt) && inputtxt.length == 11) {
                $("." + txtid).css("border", "solid 1px green")
                return true;
            }
            else {
                $("." + txtid).css("border", "solid 1px red")
                $("." + txtid).val('');
                alert("DISE Code. should be 11 digit");

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
            debugger;
            var TotalCamt = 0;
            $("." + txtcls).each(function (index, value) {
                if ($.trim($(this).val()) != "")
                    if (!isNaN($(this).val()))
                        TotalCamt = TotalCamt + parseFloat($(this).val());
            });

            $("." + txttotalcls).val(TotalCamt);
            return false;


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
                    <div class="col-lg-2 col-md-2 col-sm-3" style="padding-right: 0px;">
                        <div class="thumbnail" style="min-height: 750px; width: 233px;">
                            <div style="overflow: auto; margin-top: 35px; height: 750px;">
                                <asp:GridView ID="GvVillage" runat="server" Width="100%" AllowPaging="true" PageSize="40"
                                    BorderStyle="None" DataKeyNames="NewVillageCode" GridLines="None" AutoGenerateColumns="false"
                                    OnRowCommand="GvVillage_OnRowCommand" OnPageIndexChanging="GvVillage_PageIndexChanging">
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
                                    <%-- <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                    <AlternatingRowStyle BackColor="#f1f1f1" />--%>
                                    <Columns>
                                        <asp:ButtonField HeaderText="Code " ItemStyle-ForeColor="#333" DataTextField="VillageCode"
                                            CommandName="GV_VIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="Name " ItemStyle-ForeColor="#333" DataTextField="VillageName"
                                            CommandName="GV_VIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <%--<asp:ButtonField HeaderText="Name" Visible="false" Text="Button" DataTextField="schoolcode">
                    </asp:ButtonField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-9">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <h3 class="text-danger" style="margin: 0px;">
                                                    <asp:Label ID="lblMain" runat="server" Text="Village Profile"></asp:Label></h3>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">
                                                <input type="image" id="ton-new" class="butt" src="Images/search-not-29.png" title="Search" />
                                                <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right"
                                                    ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px;
                                                    padding: 0px;" runat="server" />
                                                <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnsave_Click" ValidationGroup="saves"
                                                    Style="margin-right: 5px; padding: 0px;" runat="server" />
                                                <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-right" Visible="false" BackColor="#f5f5f5"
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
                                                        <%-- <asp:UpdatePanel runat="server" ID="UpMain" UpdateMode="Conditional">
        <ContentTemplate>--%>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                    Year:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                                        class="form-control ">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                    State:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                                        AutoPostBack="true" class="form-control ">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                    District:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                                        AutoPostBack="true" class="form-control " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                    Block:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                                        class="form-control " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                    Panchayat:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"
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
                                            <asp:Panel ID="pnlMain" Enabled="false" runat="server">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                    <fieldset class="scheduler-border" runat="server" id="stmid">
                                                        <legend class="scheduler-border">Village Details </legend>
                                                        <div class="row">
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-5" for="Name">
                                                                        VillageCode <span class="req">*</span></label>
                                                                    <div class="col-sm-7">
                                                                        <asp:TextBox ID="txtVillageCode" Enabled="false" runat="server" class="form-control" />
                                                                        <span class="reqfield">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Display="Dynamic"
                                                                                ValidationGroup="saves" ControlToValidate="txtVillageCode" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-5 text-danger" style="font-weight: bold !important;"
                                                                        for="Name">
                                                                        Village Name <span class="req">*</span>
                                                                    </label>
                                                                    <div class="col-sm-7">
                                                                        <asp:TextBox ID="TxtVillageName" MaxLength="50" Enabled="false" autocomplete="off"
                                                                            ondrop="return false;" onkeypress="return onlyAlphabetsAdd(event,this);" runat="server"
                                                                            class="form-control" />
                                                                        <span class="reqfield">
                                                                            <asp:RequiredFieldValidator ID="rvtxtSchoolName" runat="server" Display="Dynamic"
                                                                                ValidationGroup="saves" ControlToValidate="TxtVillageName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-5" for="Name">
                                                                        Sarpanch Name <span class="req">*</span></label>
                                                                    <div class="col-sm-7">
                                                                        <asp:TextBox ID="txtSarpanch" autocomplete="off" ondrop="return false;" onkeypress="return onlyAlphabets(event,this);"
                                                                            runat="server" MaxLength="30" class="form-control" />
                                                                        <span class="reqfield">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                                                ValidationGroup="saves" ControlToValidate="txtSarpanch" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-5" for="Name">
                                                                        Mobile No. <span class="req">*</span></label>
                                                                    <div class="col-sm-7">
                                                                        <asp:TextBox ID="TxtCont" OnKeyUp="javascript:inputtxt();" runat="server" MaxLength="10"
                                                                            onkeypress="return isNumberKey(this,event);" onchange="javascript: phonenumber(this.value,'TeContact1');"
                                                                            autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                                        <span class="reqfield">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                                                                ValidationGroup="saves" ControlToValidate="TxtCont" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-5" for="Name">
                                                                        Total Fali/Dhani <span class="req">*</span>
                                                                    </label>
                                                                    <div class="col-sm-7">
                                                                        <asp:TextBox ID="TxtDhani" autocomplete="off" ondrop="return false;" runat="server"
                                                                            onkeypress="return isNumberKey(this,event);" MaxLength="2" class="form-control" />
                                                                        <span class="reqfield">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                                                                ValidationGroup="saves" ControlToValidate="TxtDhani" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel ID="Image" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <label class="control-label col-sm-5" for="Name">
                                                                                Image
                                                                            </label>
                                                                            <div class="col-sm-7">
                                                                                <asp:FileUpload ID="FileuploadAttach" runat="server" Width="160px" Font-Size="Smaller"
                                                                                    TabIndex="16" />
                                                                                <asp:Image ID="imgMKS" runat="server" Height="80px" Width="100px" BorderColor="Black"
                                                                                    BorderStyle="Ridge" BorderWidth="1px" />
                                                                            </div>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="btnsave" />
                                                                             <asp:PostBackTrigger ControlID="btnSUmbit" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                            <div class="form-group">
                                                                <label class="control-label col-sm-5" for="Name">
                                                                </label>
                                                                <div class="col-sm-7">
                                                                    <asp:TextBox runat="server" ID="txtDate" Visible="false" autocomplete="off" ondrop="return false;"
                                                                        class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                    <ajax:CalendarExtender ID="CalendarExtenderTourdate" OnClientDateSelectionChanged="arrivaldatecheck"
                                                                        runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="BottomRight">
                                                                    </ajax:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    
                                                </div>
                                                <div>   <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                        <fieldset class="scheduler-border " runat="server" id="stbdfid">
                                            <legend class="scheduler-border">Add Fali/Dhani  </legend>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                <ContentTemplate>
                                                    <div >
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="GV_name_Add" Width="100%" ShowFooter="true" runat="server"
                                                                        BorderStyle="None" GridLines="None" AutoGenerateColumns="false">
                                                                        <EmptyDataTemplate>
                                                                        </EmptyDataTemplate>
                                                                        <FooterStyle CssClass="FooterStyle" />
                                                                        <HeaderStyle BackColor="#f5f5f5" ForeColor="Black" Height="25px" />
                                                                        <RowStyle HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#fad669" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Name">
                                                                                <ItemStyle Width="30%" />
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="lblName" MaxLength="30" onkeypress="return onlyAlphabets(event,this);"
                                                                                        CssClass="form-control BorderTextGrid" Width="90%" runat="server" Text='<%#Eval("DhaniName") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:LinkButton ID="btnAdd1" OnClick="btnAdd_Click1" runat="server" ForeColor="#0e7a9e"
                                                                                        ToolTip="Add new Row" Text="Add" Font-Size="12px" Font-Italic="true" Font-Underline="true" />
                                                                                </FooterTemplate>
                                                                                <HeaderStyle CssClass="HeaderStyle GridHeaderClass" />
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                      
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemStyle Width="9%" />
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="Img_btn_delete" runat="server" ToolTip="Delete Plan" CssClass="BorderTextGrid"
                                                                                        ImageUrl="~/images/delete-29.png" Style="height: 15px;" OnClick="Img_btn_delete_Click" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                                        </div></div>
                                            </fieldset>
                                            </asp:Panel>
                                        </div>
                                        
                                        <asp:Panel ID="pnlMain1" Enabled="false" runat="server">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading" style="padding: 5px 5px;">
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-sm-8 col-xs-12" style="padding: 0px;">
                                                                <div class="form-group" style="margin: 0px">
                                                                    <label class="control-label col-sm-3" for="Name">
                                                                        <h4 style="margin: 0px">
                                                                        </h4>
                                                                    </label>
                                                                    <div class="col-sm-7 col-xs-9" style="padding-right: 2px">
                                                                    </div>
                                                                    <div class="col-sm-2 col-xs-3" style="padding-left: 2px">
                                                                        <asp:Button ID="btnYearAdd" Visible="false" OnClick="btnYearAdd_Click" CssClass="btn btn-default btn-padd"
                                                                            runat="server" Text="+" />
                                                                    </div>
                                                                    <div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <fieldset class="scheduler-border" runat="server" id="Fieldset1">
                                                <legend class="scheduler-border">Village at Glance </legend>
                                                <div class="row">
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-7" for="Name">
                                                                Distance from District HQ(Km)<span class="req">*</span></label>
                                                            <div class="col-sm-5">
                                                                <asp:TextBox ID="TxtDistance" MaxLength="3" onkeypress="return isNumberKey(this,event);"
                                                                    runat="server" class="form-control" />
                                                                <span class="reqfield">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                                                        ValidationGroup="saves" ControlToValidate="TxtDistance" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-7" for="Name">
                                                                Primary Occupation <span class="req">*</span></label>
                                                            <div class="col-sm-5">
                                                                <asp:DropDownList ID="ddlPrimaryOccupation" runat="server" class="form-control ">
                                                                </asp:DropDownList>
                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="0" runat="server"
                                                                        Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlPrimaryOccupation"
                                                                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-7" for="Name">
                                                                No. of Anganwari <span class="req">*</span></label>
                                                            <div class="col-sm-5">
                                                                <asp:TextBox runat="server" ID="txtNoofAnganwari" MaxLength="1" onkeypress="return isNumberKey(this,event);"
                                                                    class="form-control"></asp:TextBox>
                                                                <span class="reqfield">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="Dynamic"
                                                                        ValidationGroup="saves" ControlToValidate="txtNoofAnganwari" ErrorMessage="*"
                                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-7" for="Name">
                                                                Main Castes1 <span class="req">*</span></label>
                                                            <div class="col-sm-5">
                                                                <asp:DropDownList ID="ddlMainCaste1" runat="server" class="form-control ">
                                                                </asp:DropDownList>
                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" InitialValue="0" runat="server"
                                                                        Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlMainCaste1" ErrorMessage="*"
                                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-7" for="Name">
                                                                Secondary Occupation<span class="req">*</span></label>
                                                            <div class="col-sm-5">
                                                                <asp:DropDownList ID="ddlSecondaryOccupation" runat="server" class="form-control ">
                                                                </asp:DropDownList>
                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" InitialValue="0" runat="server"
                                                                        Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlSecondaryOccupation"
                                                                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-7" for="Name">
                                                                Conectivity from Main Road<span class="req">*</span></label>
                                                            <div class="col-sm-5">
                                                                <asp:DropDownList ID="ddlConectivityfromMainRoad" runat="server" class="form-control ">
                                                                </asp:DropDownList>
                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" InitialValue="0" runat="server"
                                                                        Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlConectivityfromMainRoad"
                                                                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-7" for="Name">
                                                                Main Castes2 <span class="req">*</span></label>
                                                            <div class="col-sm-5">
                                                                <asp:DropDownList ID="ddlMainCastes2" runat="server" class="form-control ">
                                                                </asp:DropDownList>
                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" InitialValue="0" runat="server"
                                                                        Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlMainCastes2"
                                                                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-7 text-danger" style="font-weight: bold !important;"
                                                                for="Name">
                                                                Other Occupation <span class="req">*</span>
                                                            </label>
                                                            <div class="col-sm-5">
                                                                <asp:DropDownList ID="ddlOtherOccupation" runat="server" class="form-control ">
                                                                </asp:DropDownList>
                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator65" InitialValue="0" runat="server"
                                                                        Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlOtherOccupation"
                                                                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-7" for="Name">
                                                                Mode of Transportation<span class="req">*</span></label>
                                                            <div class="col-sm-5">
                                                                <asp:DropDownList ID="ddlModeoftrans" runat="server" class="form-control ">
                                                                </asp:DropDownList>
                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" InitialValue="0" runat="server"
                                                                        Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlModeoftrans"
                                                                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-7" for="Name">
                                                                Main Castes3 <span class="req">*</span></label>
                                                            <div class="col-sm-5">
                                                                <asp:DropDownList ID="ddlMainCastes3" runat="server" class="form-control ">
                                                                </asp:DropDownList>
                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" InitialValue="0" runat="server"
                                                                        Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlMainCastes3"
                                                                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-7" for="Name">
                                                                Total Households <span class="req">*</span></label>
                                                            <div class="col-sm-5">
                                                                <asp:TextBox ID="txtTotalHouseholds" MaxLength="5" onkeypress="return isNumberKey(this,event);"
                                                                    runat="server" class="form-control" />
                                                                <span class="reqfield">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" Display="Dynamic"
                                                                        ValidationGroup="saves" ControlToValidate="txtTotalHouseholds" ErrorMessage="*"
                                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-7" for="Name">
                                                                Total population <span class="req">*</span></label>
                                                            <div class="col-sm-5">
                                                                <asp:TextBox ID="txtTotalpopulation" MaxLength="5" onkeypress="return isNumberKey(this,event);"
                                                                    runat="server" class="form-control" />
                                                                <span class="reqfield">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" Display="Dynamic"
                                                                        ValidationGroup="saves" ControlToValidate="txtTotalpopulation" ErrorMessage="*"
                                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                            <fieldset class="scheduler-border" runat="server" id="fldSchoolType">
                                                <legend class="scheduler-border">School Type</legend>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <label class="control-label col-sm-9" for="Name">
                                                            Type of Schools</label>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                        <label class="control-label col-sm-9" for="Name">
                                                            Govt. Schools</label>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                        <label class="control-label col-sm-9" for="Name">
                                                            Private Schools</label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-9" for="Name">
                                                                Primary School (1-5)<span class="req">*</span></label>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="TxtGovt1" onkeypress="return isNumberKey(this,event);" OnKeyUp="javascript:calculate_totals('txtGovt','TxtGovtT');"
                                                                runat="server" class="form-control txtGovt" />
                                                            <span class="reqfield">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                                                                    ValidationGroup="saves" ControlToValidate="TxtGovt1" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="TxtPvt1" onkeypress="return isNumberKey(this,event);" OnKeyUp="javascript:calculate_totals('txtPvt','TxtPvtT');"
                                                                runat="server" class="form-control txtPvt" />
                                                            <span class="reqfield">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" Display="Dynamic"
                                                                    ValidationGroup="saves" ControlToValidate="TxtPvt1" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-9" for="Name">
                                                                Upper Primary School (1-8)<span class="req">*</span></label>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="TxtGovt2" onkeypress="return isNumberKey(this,event);" OnKeyUp="javascript:calculate_totals('txtGovt','TxtGovtT');"
                                                                MaxLength="2" runat="server" class="form-control txtGovt" />
                                                            <span class="reqfield">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" Display="Dynamic"
                                                                    ValidationGroup="saves" ControlToValidate="TxtGovt2" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="TxtPvt2" onkeypress="return isNumberKey(this,event);" MaxLength="2"
                                                                OnKeyUp="javascript:calculate_totals('txtPvt','TxtPvtT');" runat="server" class="form-control txtPvt" />
                                                            <span class="reqfield">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" Display="Dynamic"
                                                                    ValidationGroup="saves" ControlToValidate="TxtPvt2" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-9" for="Name">
                                                                Secondary (1-10)<span class="req">*</span></label>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="TxtGovt3" onkeypress="return isNumberKey(this,event);" OnKeyUp="javascript:calculate_totals('txtGovt','TxtGovtT');"
                                                                MaxLength="2" runat="server" class="form-control txtGovt " />
                                                            <span class="reqfield">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" Display="Dynamic"
                                                                    ValidationGroup="saves" ControlToValidate="TxtGovt3" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="TxtPvt3" onkeypress="return isNumberKey(this,event);" MaxLength="2"
                                                                OnKeyUp="javascript:calculate_totals('txtPvt','TxtPvtT');" runat="server" class="form-control txtPvt" />
                                                            <span class="reqfield">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" Display="Dynamic"
                                                                    ValidationGroup="saves" ControlToValidate="TxtPvt3" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-9" for="Name">
                                                                Senior Secondary (1-12)<span class="req">*</span></label>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="TxtGovt4" onkeypress="return isNumberKey(this,event);" OnKeyUp="javascript:calculate_totals('txtGovt','TxtGovtT');"
                                                                MaxLength="2" runat="server" class="form-control txtGovt" />
                                                            <span class="reqfield">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" Display="Dynamic"
                                                                    ValidationGroup="saves" ControlToValidate="TxtGovt4" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="TxtPvt4" onkeypress="return isNumberKey(this,event);" MaxLength="2"
                                                                OnKeyUp="javascript:calculate_totals('txtPvt','TxtPvtT');" runat="server" class="form-control txtPvt" />
                                                            <span class="reqfield">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" Display="Dynamic"
                                                                    ValidationGroup="saves" ControlToValidate="TxtPvt4" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-9" for="Name">
                                                                Total</label>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="TxtGovt5" Enabled="false" OnKeyUp="javascript:calculate_totals('TxtGovtT','TxtTotal');"
                                                                runat="server" class="form-control TxtGovtT" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="TxtPvt5" Enabled="false" OnKeyUp="javascript:calculate_totals('TxtPvtT','TxtTotal');"
                                                                runat="server" class="form-control TxtPvtT" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-9" for="Name">
                                                                Total</label>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                        <div class="col-sm-12">
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="TxtPvt6" Enabled="false" runat="server" class="form-control TxtTotal" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                            <fieldset class="scheduler-border" runat="server" id="Fieldset2">
                                                <legend class="scheduler-border">Basic facilities</legend>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-12" for="Name">
                                                                Out of village School Distance(KM)<span class="req">*</span></label>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="TextBox1" onkeypress="return isNumberKey(this,event);" MaxLength="2"
                                                                runat="server" class="form-control" />
                                                            <span class="reqfield">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Display="Dynamic"
                                                                    ValidationGroup="saves" ControlToValidate="TextBox1" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-12" for="Name">
                                                                Electricity<span class="req">*</span></label>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:DropDownList ID="ddlElect" runat="server" class="form-control ">
                                                            </asp:DropDownList>
                                                            <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" InitialValue="0" runat="server"
                                                                    Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlElect" ErrorMessage="*"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-12" for="Name">
                                                                Source of Drinking water<span class="req">*</span></label>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:DropDownList ID="ddlSourceofdrinkingwater" runat="server" class="form-control ">
                                                            </asp:DropDownList>
                                                            <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" InitialValue="0" runat="server"
                                                                    Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlSourceofdrinkingwater"
                                                                    ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-12" for="Name">
                                                                No. of Community Centre/hall <span class="req">*</span></label>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="TxtHall" onkeypress="return isNumberKey(this,event);" MaxLength="2"
                                                                runat="server" class="form-control" />
                                                            <span class="reqfield">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" Display="Dynamic"
                                                                    ValidationGroup="saves" ControlToValidate="TxtHall" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-12" for="Name">
                                                                Total No.of Youth Groups<span class="req">*</span></label>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="TxtYouth" onkeypress="return isNumberKey(this,event);" MaxLength="2"
                                                                runat="server" class="form-control" />
                                                            <span class="reqfield">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Display="Dynamic"
                                                                    ValidationGroup="saves" ControlToValidate="TxtYouth" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-12" for="Name">
                                                                Availablity of Female Group<span class="req">*</span></label>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:DropDownList ID="ddlAvailablity" runat="server" class="form-control ">
                                                            </asp:DropDownList>
                                                            <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" InitialValue="0" runat="server"
                                                                    Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlAvailablity"
                                                                    ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-12" for="Name">
                                                                Distance of Nearest Hospital<span class="req">*</span></label>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="TxtHospital" onkeypress="return isNumberKey(this,event);" MaxLength="2"
                                                                runat="server" class="form-control" />
                                                            <span class="reqfield">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Display="Dynamic"
                                                                    ValidationGroup="saves" ControlToValidate="TxtHospital" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-12" for="Name">
                                                                Name of Nearest Bank<span class="req">*</span></label>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="TxtBank" onkeypress="return onlyAlphabetsAdd(event,this);" MaxLength="30"
                                                                runat="server" class="form-control" />
                                                            <span class="reqfield">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Display="Dynamic"
                                                                    ValidationGroup="saves" ControlToValidate="TxtBank" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-12" for="Name">
                                                                Nearest Market Name<span class="req">*</span></label>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="TxtMarket" onkeypress="return onlyAlphabetsAdd(event,this);" MaxLength="30"
                                                                runat="server" class="form-control" />
                                                            <span class="reqfield">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Display="Dynamic"
                                                                    ValidationGroup="saves" ControlToValidate="TxtMarket" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                             </asp:Panel>
                                        </div>
                                       
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="thumbnail" style="float: left; width: 100%;">
                                    <div class="col-lg-4 col-md-4 col-xs-12 col-sm-6  col-lg-offset-4 col-md-offset-4 col-sm-offset-3 col-xs-offset-0  ">
                                        <asp:ImageButton ID="btnSUmbit" ToolTip="Save" OnClick="btnSumbit_Click" ValidationGroup="saves"
                                            ImageUrl="~/images/Sumbit.jpg" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
            <script type="text/javascript">
                $(function () {
                    $('#datetimepicker4').datetimepicker();
                });
            </script>
            <asp:HiddenField ID="hdnFlag" runat="server" />


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
