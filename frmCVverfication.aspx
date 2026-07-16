<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="frmCVverfication.aspx.cs" Inherits="frmCVverfication" %>

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
                <div class="row" >
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <h3 class="text-danger" style="margin: 0px;">
                                                    <asp:Label ID="lblMain" runat="server" Text="Cross Verfication"></asp:Label>
                                                </h3>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">
                                                <%--     <input type="image" id="ton-new" class="butt" src="Images/search-not-29.png" title="Search"   />--%>
                                                <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right"
                                                    ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px;
                                                    padding: 0px;" runat="server" />
                                                <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnsave_Click" ValidationGroup="saves"
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
                                                    <div class="col-lg-1">
                                                    </div>
                                                    <div class="col-lg-10">
                                                        <asp:Panel ID="pnlMain" Enabled="true" runat="server">
                                                            <div class="panel-body" style="padding: 0px;">
                                                                <div class="row">
                                                                    <div class="form-horizontal" role="form">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 3px 0px 5px;">
                                                                            <asp:GridView ID="Gv_Profile_Search" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                                AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                                                                                Font-Size="12px" Width="100%" OnRowDataBound="Gv_Profile_Search_RowDataBound">
                                                                                <EmptyDataTemplate>
                                                                                    <div style="font-family: Arial; font-size: 11px; font-weight: bold;">
                                                                                        Data not found</div>
                                                                                </EmptyDataTemplate>
                                                                                <FooterStyle CssClass="FooterStyle" />
                                                                                <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                                                <RowStyle HorizontalAlign="Left" />
                                                                                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />
                                                                                <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                                                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                <Columns>
                                                                                    <asp:TemplateField Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblCol_2" ForeColor="Black" Font-Names="Calibri" Text='<%#Eval("CV_UID") %>'
                                                                                                Visible="false" ItemStyle-ForeColor="#333" Style="width: 100%;" runat="server"></asp:Label>
                                                                                            <asp:Label ID="Label1" Text='<%#Eval("CV_FieldName") %>' runat="server" Style="width: 100%;"></asp:Label>
                                                                                            <asp:Label ID="lblFlag" runat="server" Text='<%#Eval("CV_Flag") %>' Visible="false"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:RadioButtonList ID="RadioButtonList1" Visible="false"    runat="server">
                                                                                            </asp:RadioButtonList>
                                                                                            <asp:TextBox ID="txt1" class="form-control"   runat="server" MaxLength='<%#Convert.ToInt32(Eval("CV_MaxLimit"))%>' onkeypress='<%# Eval("CV_Validation").ToString() == "T" ? "return onlyAlphabets(this,event);" : "return isNumberKey(this,event);"  %>' Visible='<%# Eval("CV_FieldType").ToString() == "T" %>'></asp:TextBox>
                                                                                           <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt1"
                                                                                                 ErrorMessage="Numeric Only Please">
                                
                                                                                            </asp:RegularExpressionValidator>--%>
                                                                                            <asp:TextBox ID="txtDate" class="form-control" Visible='<%# Eval("CV_FieldType").ToString() == "DT" %>'
                                                                                                MaxLength='<%#Convert.ToInt32(Eval("CV_MaxLimit"))%>' runat="server" onfocus="this.blur();"></asp:TextBox>
                                                                                            <ajax:CalendarExtender ID="CalendarExtenderTourdate" Enabled="true" runat="server"
                                                                                                Format="dd/MM/yyyy" TargetControlID="TxtDate" PopupPosition="BottomRight">
                                                                                            </ajax:CalendarExtender>
                                                                                            <asp:DropDownList ID="ddl" Visible="false" class="form-control " runat="server">
                                                                                                <asp:ListItem Value="0" Text="--- select --"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                        <div class="row" runat="server" visible="false">
                                                                            <div class="thumbnail" style="float: left; width: 100%;">
                                                                                <div class="col-lg-4 col-md-4 col-xs-12 col-sm-6  col-lg-offset-4 col-md-offset-4 col-sm-offset-3 col-xs-offset-0  ">
                                                                                    <asp:ImageButton ID="btnSUmbit" ToolTip="Save" OnClick="btnsave_Click" ValidationGroup="saves"
                                                                                        ImageUrl="~/images/Sumbit.jpg" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                        </asp:Panel>
                                                    </div>
                                                    <div class="col-lg-1">
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
