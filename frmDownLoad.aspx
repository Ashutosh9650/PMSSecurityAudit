<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="frmDownLoad.aspx.cs" Inherits="frmDownLoad" %>

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
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <%--<input type="image" id="ton-new" class="butt" src="Images/close.png"  />
       <div id="div-show-new"></div> --%>
            </div>
            <div class="container-fluid">
                <div id="x" class="wapp" style="margin-left: 10.5%; top:0;">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-default">
                                <div class="panel-heading" style="padding: 5px 10px;">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6">
                                            <h3 class="text-danger" style="margin: 0px;">
                                                Download</h3>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">
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
                                                    <div class="row">
                                                        <%--<div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
            	<div class="form-group" >
                <label for="email" class="col-sm-3 padd linhei"  style="padding-top: 2px;" >UserName:</label>
                <div class="col-sm-9 padd">
                       <asp:TextBox ID="txtUserName"  autocomplete="off" ondrop="return false;"  runat="server"  class="form-control TeCont "/>

    					
                				</div>
    								</div>
            							</div>
                <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
            	<div class="form-group" >
    				<label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">Password:</label>
    					 <div class="col-sm-9 padd">
    				 <asp:TextBox ID="txtPassword"  autocomplete="off" ondrop="return false;"  runat="server"  class="form-control TeCont "/>

                				</div>
  									</div>
            							</div> --%>
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                </label>
                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                    <asp:GridView ID="GV_Report" Width="100%" AllowPaging="true" CssClass="Grid" PageSize="8"
                                                                        runat="server" AutoGenerateColumns="false">
                                                                        <EmptyDataTemplate>
                                                                            <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                                                                Data not found
                                                                            </div>
                                                                        </EmptyDataTemplate>
                                                                        <FooterStyle CssClass="FooterStyle" />
                                                                        <HeaderStyle BackColor="#C1C1C1" Height="44px" />
                                                                        <RowStyle HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#897A7A" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Sr No" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl4Pan" ForeColor="Black" runat="server" Text='<%# Eval("SrNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Date" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPan" ForeColor="Black" runat="server" Text='<%# Eval("UpdateDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Remark" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPanchayatggName" ForeColor="Black" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Path" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:HyperLink ID="lblPanchayatName" ForeColor="Black" runat="server" Text='<%# Eval("Path") %>'
                                                                                        NavigateUrl='<%# Eval("Path") %>'></asp:HyperLink>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                    </div>
                                                    <%--</ContentTemplate>
</asp:UpdatePanel>
                                                    --%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /#wrapper -->
                                <!-- /#wrapper -->
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
