<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDonorMaster.aspx.cs" Culture="en-GB"
    MasterPageFile="~/Site.master" Inherits="frmDonorMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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


</script>
    <script type="text/javascript">
        function SelectAllCheckboxes1(chk) {

            $('#<%=GV_DynamicGrid.ClientID%>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                }
            });
        }

        function DateCom(chk) {

            var startDate = $('#<%=txtFromDate.ClientID %>').val();
            var endDate = $('#<%=txtTodate.ClientID %>').val();

            alert(Date.parse(startDate));
            alert(Date.parse(endDate));
            if ((Date.parse(startDate) <= Date.parse(endDate))) {
                alert("End date should be greater than Start date");
                document.getElementById("EndDate").value = "";
            }
        }



        function SelectAllCheckboxes2(chk) {

            $('#<%=GvRight.ClientID%>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                }
            });
        }

        function SetMultilanguage(Flag, clsname) {
            debugger;
            var Lngg = "", lid = "";
            var maxSelection = 0;
            $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                Lngg = Lngg + $(this).next().html() + ",";
                lid = lid + $(this).val() + ",";
                maxSelection++;
            });

            Lngg = Lngg.substr(0, Lngg.length - 1);
            lid = lid.substr(0, lid.length - 1);
            if (Flag == 'F') {
                if (maxSelection <= 20) {
                    $('#<%=hdn_PBID.ClientID %>').val(lid);
                    $('#<%=hdn_PBName.ClientID %>').val(Lngg);
                    $('#<%=txt_pbname.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_PBID.ClientID %>').val('');
                    $('#<%=hdn_PBName.ClientID %>').val('');
                    $('#<%=txt_pbname.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }

                UploadFile();


            }
            else if (Flag == 'M') {
                if (maxSelection <= 26) {
                    $('#<%=hhmuhulaid.ClientID %>').val(lid);
                    $('#<%=HidName.ClientID %>').val(Lngg);
                    $('#<%=txtMuhala.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_PBID.ClientID %>').val('');
                    $('#<%=HidName.ClientID %>').val('');
                    $('#<%=txtMuhala.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }
                UploadFileDist();

            }
            else if (Flag == 'B') {
                if (maxSelection <= 30) {
                    $('#<%=hdn_PBID2.ClientID %>').val(lid);
                    $('#<%=hdn_PBName2.ClientID %>').val(Lngg);
                    $('#<%=txtMuhala1.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_PBID2.ClientID %>').val('');
                    $('#<%=hdn_PBName2.ClientID %>').val('');
                    $('#<%=txtMuhala1.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
                }


            }
            else if (Flag == 'C') {

                if (maxSelection <= 10) {

                    //                        if ($('.' + clsname + ' input[type="checkbox"]:checked')[0].checked == true && maxSelection>=1) {

                    //                            $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                    //                                $(this).attr("checked", false);
                    //                            });
                    //                            $('#ctl00_MainContent_chkID_0').prop("checked", true);                         

                    //                        }
                    //                        else {
                    //                            $('#<%=hdn_PBID3.ClientID %>').val(lid);
                    //                            $('#<%=hdn_PBName3.ClientID %>').val(Lngg);
                    //                            $('#<%=txtMuhala5.ClientID %>').val(Lngg);
                    //                        }
                    $('#<%=hdn_PBID3.ClientID %>').val(lid);
                    $('#<%=hdn_PBName3.ClientID %>').val(Lngg);
                    $('#<%=txtMuhala5.ClientID %>').val(Lngg);
                }

                else {


                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hdn_PBID3.ClientID %>').val('');
                    $('#<%=hdn_PBName3.ClientID %>').val('');
                    $('#<%=txtMuhala5.ClientID %>').val('');
                    $find("Modal_alertB").show();
                    return false;
                }


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
            if ($("." + txtid).val() == 0) {
                $("." + txtid).css("border", "solid 1px green")
                return true;
            }
            else if (phoneno.test(inputtxt) && inputtxt.length == 10) {
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
         
.radioButtonList
{
}
 
.radioButtonList input[type="radio"]
{
	width: 20px;
    padding: 0;
     
}
	
.radioButtonList label
{
	margin-right: 25px;    
    white-space: nowrap;
}
 
.divA
{
	clear: both;
	margin-bottom: 30px;
}
 
.divB
{
	float:left;
}
 
.labelA
{
	text-align: left;
	float: left;
	width: 180px;
	font-size: 10pt;
	font-weight: 600;
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
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-3" style="padding-right: 0px;">
                        <div class="thumbnail" style="min-height: 560px; width: 228px;">
                            <div style="overflow: auto; margin-top: 35px; height: 757px;">
                                <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="30"
                                    BorderStyle="None" DataKeyNames="DID" OnRowCommand="GVMain_OnRowCommand" GridLines="None"
                                    AutoGenerateColumns="false">
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
                                        <asp:ButtonField HeaderText="Donor Name " ItemStyle-ForeColor="#333" DataTextField="DonorName"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="60px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="From Date " ItemStyle-ForeColor="#333" DataTextField="FromDate"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="20px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="To Date " ItemStyle-ForeColor="#333" DataTextField="todate"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="20px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
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
                                            <div class="col-lg-9 col-md-6 col-sm-6" style="padding: 0px;">
                                                <h3 class="text-danger" style="margin: 0px;">Donor Master</h3>
                                            </div>

                                            <div class="col-lg-3 col-md-3 col-sm-3 " style="padding: 0px">
                                                <asp:Button ID="Button1" OnClick="btnReprot_Click" CssClass="btn btn-success pull-right"
                                                    Text="Report" runat="server" Style="margin-left: 6px;" />
                                                <asp:Button ID="btnApprove" CssClass="btn btn-success pull-right" Text="Add"
                                                    OnClick="btnApprove_Click" runat="server" />
                                                <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" OnClick="btnSave_Click"
                                                    BackColor="#f5f5f5" ToolTip="Save" ImageUrl="~/images/save-29-1.png" ValidationGroup="saves"
                                                    Style="margin-right: 5px; padding: 0px;" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:Panel ID="pnlMain" runat="server">
                                                    <div class="form-horizontal">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 10px 3px 0px 5px;">
                                                            <fieldset class="scheduler-border">
                                                                <legend class="scheduler-border">Donor Master </legend>
                                                                <div class="Row">
                                                                      <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                Year</label>
                                                                            <div class="col-sm-8">
                                                                                  <asp:DropDownList ID="ddlYear" OnSelectedIndexChanged="ddlStartYear_SelectedIndexChanged"    AutoPostBack="true" runat="server" class="form-control">
                                                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                        <asp:ListItem Value="2025">2026-2027</asp:ListItem>
                                                                                        <asp:ListItem Value="2025">2025-2026</asp:ListItem>
                                                                                    <asp:ListItem Value="2024">2024-2025</asp:ListItem>
                                                                                  
                                                                                   

                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                Donor name</label>
                                                                            <div class="col-sm-8">
                                                                                <asp:TextBox ID="txtDonorName" MaxLength="50" autocomplete="off" ondrop="return false;"
                                                                                    onkeypress="return onlyAlphabets(event,this);" runat="server" class="form-control" />
                                                                                <span class="reqfield">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                                                                        ValidationGroup="saves" ControlToValidate="txtDonorName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                Project Start Date
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <asp:TextBox runat="server" ID="txtFromDate" OnTextChanged="txtdatefrom_TextChanged"
                                                                                    AutoPostBack="true" autocomplete="off" ondrop="return false;" class="form-control"
                                                                                    onkeypress="return false;"></asp:TextBox>
                                                                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                                    TargetControlID="txtFromDate" PopupPosition="BottomRight">
                                                                                </ajax:CalendarExtender>
                                                                                <span class="reqfield">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                                                        ValidationGroup="saves" ControlToValidate="txtFromDate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                Project End Date
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <asp:TextBox runat="server" ID="txtTodate" OnTextChanged="txtTodate_TextChanged"
                                                                                    AutoPostBack="true" autocomplete="off" ondrop="return false;" class="form-control"
                                                                                    onkeypress="return false;"></asp:TextBox>
                                                                                <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                                    TargetControlID="txtTodate" PopupPosition="BottomRight">
                                                                                </ajax:CalendarExtender>
                                                                                <span class="reqfield">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                                                                        ValidationGroup="saves" ControlToValidate="txtTodate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                Geography
                                                                            </label>
                                                                            <div class="col-sm-8">

                                                                                    
                                                                                <asp:DropDownList ID="ddInGeography" AutoPostBack="true" OnSelectedIndexChanged="ddInGeography_SelectedIndexChanged"
                                                                                    runat="server" class="form-control">
                                                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Organization wide</asp:ListItem>
                                                                                    <asp:ListItem Value="2">District</asp:ListItem>
                                                                                    <asp:ListItem Value="3">Block</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="0" runat="server"
                                                                                        Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddInGeography" ErrorMessage="*"
                                                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                     <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" id="divDistype" runat="server"
                                                                        visible="false">
                                                                        <div class="form-group" style="margin-bottom: 20px;">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                District Type
                                                                            </label>
                                                                            <div class="col-sm-8" style="font-size: 11px;">

                                                                                   <asp:RadioButtonList ID="rblDist" AutoPostBack="true" OnSelectedIndexChanged="rblDist_SelectedIndexChanged" runat="server" RepeatColumns="2" RepeatLayout="Flow" RepeatDirection="Horizontal" CssClass="radioButtonList">
                                            <asp:ListItem Text="EG District" Selected="True" Value="1"></asp:ListItem>
                                                                                    <asp:ListItem Text="Admin District" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                                                              <%--  <asp:RadioButtonList ID="rblDist" AutoPostBack="true" OnSelectedIndexChanged="rblDist_SelectedIndexChanged"
                                                                                    CssClass="cr-icon" ForeColor="Black" RepeatDirection="Horizontal" runat="server">
                                                                                    <asp:ListItem Text="EG District" Selected="True" Value="1"></asp:ListItem>
                                                                                    <asp:ListItem Text="Admin District" Value="2"></asp:ListItem>
                                                                                </asp:RadioButtonList>--%>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" id="divState" runat="server"
                                                                        visible="false">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                State
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <asp:TextBox ID="txt_pbname" autocomplete="off" ondrop="return false;" runat="server"
                                                                                    CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                                                <ajax:PopupControlExtender ID="PopupControltxt_pbname" runat="server" TargetControlID="txt_pbname"
                                                                                    PopupControlID="pnt_bookformat" OffsetY="22">
                                                                                </ajax:PopupControlExtender>
                                                                                <asp:Panel ID="pnt_bookformat" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 100%"
                                                                                    CssClass="panel">
                                                                                    <span>
                                                                                        <asp:CheckBoxList ID="ChkState" OnTextChanged="txtState_TextChanged" AutoPostBack="true" CssClass="_bookformat radio" runat="server" onclick="SetMultilanguage('F','_bookformat');">
                                                                                        </asp:CheckBoxList>
                                                                                    </span>
                                                                                    <asp:HiddenField runat="server" ID="hdn_PBName" />
                                                                                    <asp:HiddenField runat="server" ID="hdn_PBID" />
                                                                                </asp:Panel>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                   
                                                                    <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" id="divDist" runat="server" visible="false">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                District
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <asp:TextBox ID="txtMuhala" runat="server" autocomplete="off" ondrop="return false;"
                                                                                    CssClass="form-control"></asp:TextBox>
                                                                                <ajax:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="txtMuhala"
                                                                                    PopupControlID="pnt_Muhula" OffsetY="22">
                                                                                </ajax:PopupControlExtender>
                                                                                <asp:Panel ID="pnt_Muhula" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 100%"
                                                                                    CssClass="panel">
                                                                                    <span>
                                                                                        <asp:CheckBoxList ID="chkDistrict" CssClass="_bookformat1 radio" runat="server" OnTextChanged="txtDist_TextChanged" AutoPostBack="true" onclick="SetMultilanguage('M','_bookformat1');">
                                                                                        </asp:CheckBoxList>
                                                                                    </span>
                                                                                    <asp:HiddenField runat="server" ID="hhmuhulaid" />
                                                                                    <asp:HiddenField runat="server" ID="HidName" />
                                                                                </asp:Panel>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" id="divBlock" runat="server"
                                                                        visible="false">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                Block
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <asp:TextBox ID="txtMuhala1" runat="server" autocomplete="off" ondrop="return false;"
                                                                                    CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                                                <ajax:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="txtMuhala1"
                                                                                    PopupControlID="pnt_Muhula1" OffsetY="22">
                                                                                </ajax:PopupControlExtender>
                                                                                <asp:Panel ID="pnt_Muhula1" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 100%"
                                                                                    CssClass="panel">
                                                                                    <span>
                                                                                        <asp:CheckBoxList ID="chkBlock" CssClass="_bookformat2 radio" runat="server" onclick="SetMultilanguage('B','_bookformat2');">
                                                                                        </asp:CheckBoxList>
                                                                                    </span>
                                                                                    <asp:HiddenField runat="server" ID="hdn_PBName2" />
                                                                                    <asp:HiddenField runat="server" ID="hdn_PBID2" />
                                                                                </asp:Panel>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                Frequency
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <asp:DropDownList ID="ddlFrequency" runat="server" class="form-control">
                                                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Quarterly</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Half Yearly</asp:ListItem>
                                                                                    <asp:ListItem Value="3">Yearly</asp:ListItem>
                                                                                      <asp:ListItem Value="4">Monthly</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" runat="server"
                                                                                        Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlFrequency" ErrorMessage="*"
                                                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" runat="server" visible="false">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                Qualitative
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <asp:DropDownList ID="ddlQualitative" runat="server" class="form-control">
                                                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" InitialValue="0" runat="server"
                                                                                        Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlQualitative"
                                                                                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" runat="server" visible="false">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                AGP</label>
                                                                            <div class="col-sm-8">
                                                                                <asp:DropDownList ID="ddlAGP" runat="server" class="form-control">
                                                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" InitialValue="0" runat="server"
                                                                                        Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlAGP" ErrorMessage="*"
                                                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" runat="server" visible="false">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                Phase 3:
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <asp:DropDownList ID="ddlPhage" runat="server" class="form-control">
                                                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" InitialValue="0" runat="server"
                                                                                        Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlPhage" ErrorMessage="*"
                                                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                Status
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <asp:DropDownList ID="ddlStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                                                                                    runat="server" class="form-control">
                                                                                </asp:DropDownList>
                                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" InitialValue="0" runat="server"
                                                                                        Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlStatus" ErrorMessage="*"
                                                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" runat="server" id="IDActive">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                Active Date
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <asp:TextBox runat="server" ID="txtActiveDate" autocomplete="off" ondrop="return false;"
                                                                                    class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                                <ajax:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                                    TargetControlID="txtActiveDate"
                                                                                    PopupPosition="BottomRight">
                                                                                </ajax:CalendarExtender>
                                                                                <span class="reqfield">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic"
                                                                                        ValidationGroup="saves" ControlToValidate="txtActiveDate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12" runat="server" visible="false"
                                                                        id="DActive">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                DeActive Date
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <asp:TextBox runat="server" ID="txtDeAvtiveDate" autocomplete="off" ondrop="return false;"
                                                                                    class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                                <ajax:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                                    TargetControlID="txtDeAvtiveDate"
                                                                                    PopupPosition="BottomRight">
                                                                                </ajax:CalendarExtender>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                Reporting Year</label>
                                                                            <div class="col-sm-8">
                                                                                <asp:DropDownList ID="ddlStartYear" runat="server" class="form-control">
                                                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                     <asp:ListItem Value="2025">2025</asp:ListItem>
                                                                                    <asp:ListItem Value="2024">2024</asp:ListItem>
                                                                                    <asp:ListItem Value="2023">2023</asp:ListItem>
                                                                                 

                                                                                </asp:DropDownList>
                                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" InitialValue="0" runat="server"
                                                                                        Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlStartYear" ErrorMessage="*"
                                                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-lg-4 col-md-4  col-sm-4  cpl-xs-12">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="Name">
                                                                                Reporting Start Month</label>
                                                                            <div class="col-sm-8">
                                                                                <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Jan</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Feb</asp:ListItem>
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
                                                                                <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" InitialValue="0" runat="server"
                                                                                        Display="Dynamic" ValidationGroup="saves" ControlToValidate="ddlMonth" ErrorMessage="*"
                                                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </fieldset>
                                                        </div>
                                                    </div>
                                                    <div class="form-horizontal">
                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 3px;">
                                                                <fieldset class="scheduler-border">
                                                                    <legend class="scheduler-border">Search </legend>
                                                                    <div class="row">
                                                                         <div class="col-lg-4 col-md-12 col-sm-12 cpl-xs-12">
                                                                             <div class="form-group">
                                                                                <label for="email" class="col-sm-1 padd linhei" style="padding-top: 2px;margin-right: 42px;">
                                                                                    Outcome:</label>
                                                                                <div class="col-sm-9 padd">
                                                                                     <asp:DropDownList ID="ddlOutcome" AutoPostBack="true" OnSelectedIndexChanged="ddlOutcome_SelectedIndexChanged"
                                                                                    runat="server" class="form-control">
                                                                                </asp:DropDownList>
                                                                                    </div>
                                                                                 </div>
                                                                             </div>
                                                                       
                                                                        <div class="col-lg-8 col-md-12 col-sm-12 cpl-xs-12">
                                                                            <div class="form-group">
                                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                                    Specific Indicator Outcome:</label>
                                                                                <div class="col-sm-8 padd">
                                                                                    <asp:TextBox ID="txtMuhala5" runat="server" autocomplete="off" ondrop="return false;"
                                                                                        CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                                                    <ajax:PopupControlExtender ID="PopupControlExtender3" runat="server" TargetControlID="txtMuhala5"
                                                                                        PopupControlID="pnt_Muhula6" OffsetY="22">
                                                                                    </ajax:PopupControlExtender>
                                                                                    <asp:Panel ID="pnt_Muhula6" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 100%"
                                                                                        CssClass="panel">
                                                                                        <span>
                                                                                            <asp:CheckBoxList ID="chkID" OnTextChanged="txtDistfff_TextChanged" AutoPostBack="true" CssClass="_bookformat3 radio" runat="server" onclick="SetMultilanguage('C','_bookformat3');">
                                                                                            </asp:CheckBoxList>
                                                                                        </span>
                                                                                        <asp:HiddenField runat="server" ID="hdn_PBName3" />
                                                                                        <asp:HiddenField runat="server" ID="hdn_PBID3" />
                                                                                    </asp:Panel>
                                                                                </div>
                                                                                <asp:ImageButton ID="imNewSerach" OnClick="btnNewSerach_Click" ToolTip="Serach" runat="server"
                                                                                    class="btn btn-danger btn-paddd" BackColor="transparent" ImageUrl="~/images/search-29.png" />

                                                                            </div>
                                                                        </div>
                                                                       
                                                                    </div>
                                                                </fieldset>
                                                            </div>
                                                        </div>
                                                        <fieldset class="scheduler-border">
                                                            <legend class="scheduler-border">Indicator Mapping</legend>
                                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" style="padding: 0px 3px 0px 5px;">
                                                                <asp:GridView ID="GV_DynamicGrid" AutoGenerateColumns="False" DataKeyNames="MainID, SoutComeID,OutcomeName, DOutcomeID ,SubID,SSubOutcomeName"
                                                                    runat="server" ForeColor="Black" AllowPaging="true" PageSize="300" ShowHeader="true"
                                                                    CssClass="table table-striped table-bordered table-hover" Width="100%">
                                                                    <EmptyDataTemplate>
                                                                    </EmptyDataTemplate>
                                                                    <FooterStyle CssClass="FooterStyle" />
                                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                                    <RowStyle HorizontalAlign="Left" />
                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                    <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />
                                                                    <HeaderStyle BackColor="#C1C1C1" Wrap="true" ForeColor="White" HorizontalAlign="Center" />
                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <input id="chkAll" class="cbAll" onclick="javascript:SelectAllCheckboxes1(this);"
                                                                                    runat="server" type="checkbox" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="rptCB" runat="server" CssClass="cbChild" /><br />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="10%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Outcome">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblClusterCode" runat="server" Text='<%# Eval("OutcomeName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="padding-center" />
                                                                            <HeaderStyle Width="30%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Data element">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFathersName" runat="server" Text='<%# Eval("SSubOutcomeName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="padding-lef" />
                                                                            <HeaderStyle Width="60%" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerStyle CssClass="pagination-ys" />
                                                                </asp:GridView>
                                                            </div>
                                                            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-12" style="padding-top: 90px;">
                                                                <span>
                                                                    <asp:LinkButton ID="btnprevone" Width="60" OnClick="btnprevone_Click" Height="40"
                                                                        class="fa fa-arrow-right" aria-hidden="true" runat="server" />
                                                                    <asp:LinkButton ID="btnnextone" Width="60" Height="40" OnClick="btnnextone_Click"
                                                                        class="fa fa-arrow-left" runat="server" />
                                                                </span>
                                                            </div>
                                                            <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12" style="padding: 0px 3px 0px 5px;">
                                                                <asp:GridView ID="GvRight" AutoGenerateColumns="False" runat="server" DataKeyNames="MainID, SoutComeID,OutcomeName, DOutcomeID ,SubID,SSubOutcomeName"
                                                                    ForeColor="Black" AllowPaging="true" PageSize="300" ShowHeader="true" CssClass="table table-striped table-bordered table-hover"
                                                                    Width="100%">
                                                                    <EmptyDataTemplate>
                                                                    </EmptyDataTemplate>
                                                                    <FooterStyle CssClass="FooterStyle" />
                                                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                                    <RowStyle HorizontalAlign="Left" />
                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                                    <AlternatingRowStyle BackColor="#f1f1f1" ForeColor="Black" />
                                                                    <HeaderStyle BackColor="#C1C1C1" Wrap="true" ForeColor="White" HorizontalAlign="Center" />
                                                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="Black" />
                                                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <input id="chkAll" class="cbAll" onclick="javascript:SelectAllCheckboxes2(this);"
                                                                                    runat="server" type="checkbox" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="rptCB" runat="server" CssClass="cbChild" /><br />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="10%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Outcome">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblClusterCode" runat="server" Text='<%# Eval("OutcomeName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="padding-center" />
                                                                            <HeaderStyle Width="30%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Data element">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFathersName" runat="server" Text='<%# Eval("SSubOutcomeName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="padding-lef" />
                                                                            <HeaderStyle Width="60%" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerStyle CssClass="pagination-ys" />
                                                                </asp:GridView>
                                                            </div>
                                                        </fieldset>
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
                    </div>
                </div>
            </div>
            <asp:Label ID="HdnStartYear" Visible="false" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button1" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
