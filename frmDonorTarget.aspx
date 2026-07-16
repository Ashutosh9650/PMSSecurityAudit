<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="frmDonorTarget.aspx.cs" Culture="en-GB" Inherits="frmDonorTarget" %>

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
        .ajax__calendar_container {
            z-index: 1000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="thumbnail" style="background-color: #f5f5f5; margin-bottom: 3px !important;">
                            <div class="panel-heading" style="padding: 0px;">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <h3 class="text-danger" style="margin: 0px;">Donor Target </h3>

                                         <asp:LinkButton ID="LinkddButton1" runat="server" Style="margin-right: 40px; margin-top: -20px;" Text="Export to Excel" OnClick="btnReprot_Click"
                                            class="pull-right"></asp:LinkButton>
                                        <asp:ImageButton ID="ImageButton2" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                            ToolTip="Add" ImageUrl="~/images/save-29-1.png" OnClick="btSave_Click"
                                            Style="margin-top: -23px; padding: 0px;" runat="server" />
                                       

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left: -2px;">
                    <div class="thumbnail" style="background-color: #f5f5f5; float: left; width: 100%;">
                        <div class="panel panel-default" style="margin-bottom: 0px;">
                            <div class="form-horizontal">
                                <div class="row">
                                    <div style="padding: 0px 10px;">
                                        <div class="row marg search-bg" style="padding: 10px 15px;">
                                            <div class="form-horizontal">
                                                <%-- <asp:UpdatePanel runat="server" ID="UpMain">
        <ContentTemplate>--%>
                                                <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
	<ContentTemplate>
                                                --%>
                                                <div class="row">
                                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                Donor:
                                                            </label>
                                                            <div class="col-sm-8 padd">
                                                                <asp:DropDownList ID="ddlDonor" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                                    class="form-control ">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: -2px;">
                                                                State:</label>
                                                            <div class="col-sm-9 padd">
                                                                <asp:Label runat="server" ForeColor="Black" ID="lblState"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: -2px;">
                                                                District:</label>
                                                            <div class="col-sm-9 padd">
                                                                <asp:Label runat="server" ForeColor="Black" ID="lblDistrict"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: -2px;">
                                                                Block:</label>
                                                            <div class="col-sm-9 padd">
                                                                <asp:Label runat="server" ForeColor="Black" ID="lblBlock"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>


                                                </div>
                                                <div class="row">

                                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-3 padd linhei" style="padding-top: -2px;">
                                                                Frequency:</label>
                                                            <div class="col-sm-9 padd">
                                                                <asp:Label runat="server" ForeColor="Black" ID="lblFrequency"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-6 col-sm-6 cpl-xs-12">
                                                        <div class="form-group">
                                                            <label for="email" class="col-sm-8 padd linhei" style="padding-top: -2px;">
                                                                Reporting Start Year and Month:
                                                            </label>
                                                            <div class="col-sm-4 padd">
                                                                <asp:Label runat="server" ForeColor="Black" ID="lblTarget"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-10">
                                                        <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" OnClick="btnSerach_Click"
                                                            class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-lg-12 table table-hover " style="padding: 0px;">
                                            <asp:Panel ID="pnlMain" runat="server">
                                                <asp:UpdatePanel runat="server" ID="UpdatedddddddPanel1">
                                                    <ContentTemplate>
                                                        <div class="form-horizontal">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                                <div style="height: 290px; overflow: auto; width:100%;" align="center">
                                                                    <div>
                                                                        <div class="Row" style="width: 100%">
                                                                            <asp:GridView ID="gvnroll" runat="server"
                                                                                CssClass="table table-striped table-bordered table-hover"
                                                                                OnRowDataBound="gvnroll_OnRowCommand" AutoGenerateColumns="False" Font-Names="Arial"
                                                                                Font-Size="12px" Width="100%">
                                                                                <EmptyDataTemplate>
                                                                                    <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                        Data not found
                                                                                    </div>
                                                                                </EmptyDataTemplate>
                                                                                <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" HorizontalAlign="Center" />
                                                                                <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                                <Columns>



                                                                                    <asp:TemplateField HeaderText="Reporting Outcome" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblReportingOutcome" ForeColor="Black" runat="server"
                                                                                                Text='<%# Eval("OutcomeName") %>'></asp:Label>

                                                                                            <asp:Label ID="lblMainID" ForeColor="Black" runat="server" Visible="false"
                                                                                                Text='<%# Eval("OSID") %>'></asp:Label>

                                                                                            <asp:Label ID="lblSubID" ForeColor="Black" runat="server" Visible="false"
                                                                                                Text='<%# Eval("OSubID") %>'></asp:Label>

                                                                                            <asp:Label ID="lblFrequencyID" ForeColor="Black" runat="server" Visible="false"
                                                                                                Text='<%# Eval("FrequencyID") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Reporting Indicator" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblReportingIndicator" ForeColor="Black" runat="server"
                                                                                                Text='<%# Eval("SSubOutcomeName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Target Q1" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtQ1" MaxLength="5" onkeypress="return isNumberKey(this,event);" CssClass="form-control" runat="server"
                                                                                                Text='<%# Eval("Q1") %>'></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>

                                                                                     <asp:TemplateField HeaderText="Target Q2" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtQ2" MaxLength="5" onkeypress="return isNumberKey(this,event);" CssClass="form-control" runat="server"
                                                                                                Text='<%# Eval("Q2") %>'></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>
                                                                                 
                                                                                    <asp:TemplateField HeaderText="Target Q3" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtQ3" MaxLength="5" onkeypress="return isNumberKey(this,event);" CssClass="form-control" runat="server"
                                                                                                Text='<%# Eval("Q3") %>'></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="padding-lef" />

                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Target Q4" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtQ4" MaxLength="5" onkeypress="return isNumberKey(this,event);" CssClass="form-control" runat="server"
                                                                                                Text='<%# Eval("Q4") %>'></asp:TextBox>
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
            <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important; margin-top: -75.5px !important;"
                ID="PnlDistrict" runat="server">
                <div style="width: 100%; height: auto; background-color: #f1f1f1">
                    <div class="modal-header" style="background-color: #ddd; color: White;">
                        <h4 class="modal-title" style="forecolor: White"></h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">


                            <div id="Div3" class="col-lg-12 col-md-12 col-sm-12 cpl-xs-12" visible="false" runat="server">
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 padd linhei">
                                        School:</label>
                                    <div style="padding-left: 15px;">
                                        <asp:Label ID="lblSchool" class="padd " ForeColor="Black" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </div>
                            </div>




                        </div>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                            <div class="form-horizontal">
                                <div class="row">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name" style="padding-top: 14px">Student Name  <span class="req">*</span></label>

                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtChildName" class="form-control" onkeypress="return onlyAlphabets(event,this);" autocomplete="off" ondrop="return false;" ForeColor="Black" runat="server"></asp:TextBox>


                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name" style="padding-top: 14px">Father Name <span class="req">*</span></label>

                                        <div class="col-sm-6">

                                            <asp:TextBox ID="txtFatherName" class="form-control" onkeypress="return onlyAlphabets(event,this);" autocomplete="off" ondrop="return false;" ForeColor="Black" runat="server"></asp:TextBox>


                                        </div>
                                    </div>
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
                                            <asp:TextBox ID="txtSrno" class="form-control" ForeColor="Black" runat="server" MaxLength="9" onchange="checkPwd(this.value);" autocomplete="off" ondrop="return false;"></asp:TextBox>

                                        </div>
                                    </div>

                                </div>

                                <div id="Div6" runat="server" class="row">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name" id="Label4" runat="server">Admission Date<span class="req">*</span>  </label>

                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtBirth" Width="73%" autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>

                                            <ajax:CalendarExtender ID="clk" runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" TargetControlID="txtBirth" OnClientDateSelectionChanged="arrivaldatecheck" PopupPosition="BottomRight">
                                            </ajax:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBirth"
                                                Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                                SetFocusOnError="True" ValidationGroup="saves"></asp:RequiredFieldValidator>



                                        </div>
                                    </div>
                                </div>
                                <div id="Divkj2" runat="server" class="row">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name" id="Label1" runat="server">DOB<span class="req">*</span> </label>

                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtDobDate" Width="73%" autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>

                                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" TargetControlID="txtDobDate" OnClientDateSelectionChanged="arrivaldatecheck" PopupPosition="BottomRight">
                                            </ajax:CalendarExtender>
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
                                            <asp:DropDownList ID="ddlGender" CssClass="form-control" runat="server">
                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                <asp:ListItem Value="1">1-Male </asp:ListItem>
                                                <asp:ListItem Value="2">2-Female</asp:ListItem>
                                            </asp:DropDownList>


                                            <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorlevel" runat="server"
                                                    ControlToValidate="ddlGender" InitialValue="0" ErrorMessage="*" ForeColor="Red"
                                                    ValidationGroup="Valid">

                                                </asp:RequiredFieldValidator></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="Div9" runat="server">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name" id="Label10" runat="server">Samgra ID<span class="req">*</span></label>

                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtSamgra" onkeypress="return isNumberKey(this,event);" MaxLength="9" class="form-control" runat="server"></asp:TextBox>




                                        </div>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="Name">House/Family No</label>

                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtHHNo" class="form-control" onkeypress="return onlyAlphabetsHH(event,this);" onchange="checkPwd(this.value);" autocomplete="off" ondrop="return false;" ForeColor="Black" runat="server"></asp:TextBox>




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

                                <div class="row" runat="server" visible="false">
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
                                                <asp:TextBox ID="txtSurveyVillage" class="form-control" MaxLength="50" runat="server"></asp:TextBox>



                                            </div>
                                        </div>


                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div id="Div11" class="col-lg-12 col-md-12 col-sm-12 cpl-xs-12" runat="server">


                            <asp:ImageButton ID="CancelButton" ImageUrl="~/images/close-29.png" runat="server"
                                Text="Close" ToolTip="Close" Style="float: none;"></asp:ImageButton>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>

            <asp:PostBackTrigger ControlID="LinkddButton1" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
