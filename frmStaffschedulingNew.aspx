<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmStaffschedulingNew.aspx.cs"
    MasterPageFile="~/Site.master" Culture="en-GB" Inherits="frmStaffschedulingNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: rgba(0,0,0,0.5);
        }

        .checkbox, .radio {
            position: relative;
            display: block;
            margin-top: 10px;
            margin-bottom: 10px;
        }

        th {
            text-align: center;
        }

        .input, button, select, textarea {
            font-family: inherit;
            font-size: inherit;
            line-height: 20px;
        }

        .table {
            width: 138% !important;
            max-width: 102% !important;
            margin-bottom: 96px;
            margin-left: -13px;
        }

        .butt_new_grid1 {
            border: 1px solid #08c !important;
            padding: 3px 10px !important;
            border-radius: 6px !important;
            color: #fff !important;
            margin-top: 3px !important;
            line-height: 28px !important;
            background: linear-gradient(to bottom, #87e0fd 0%,#53cbf1 40%,#05abe0 100%);
        }


            .butt_new_grid1:hover {
                /* Permalink - use to edit and share this gradient: http://colorzilla.com/gradient-editor/#05abe0+0,53cbf1+40,87e0fd+100 */
                background: #05abe0; /* Old browsers */
                background: -moz-linear-gradient(top, #05abe0 0%, #53cbf1 40%, #87e0fd 100%); /* FF3.6-15 */
                background: -webkit-linear-gradient(top, #05abe0 0%,#53cbf1 40%,#87e0fd 100%); /* Chrome10-25,Safari5.1-6 */
                background: linear-gradient(to bottom, #05abe0 0%,#53cbf1 40%,#87e0fd 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
                filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#05abe0', endColorstr='#87e0fd',GradientType=0 ); /* IE6-9 */
                color: #ddd;
            }


        .Mpopup {
            position: relative;
            background: #f2f2f2;
            color: #404040;
            text-shadow: 0 1px 0 #fff;
            -ms-filter: "progid:DXImageTransform.Microsoft.dropshadow(OffX=0,OffY=1,Color=#ffffffff,Positive=true)";
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=0, OffY=1, Color=#ffffffff, Positive=true);
            border-radius: 5px;
            box-shadow: 0px 1px 4px rgba(0, 0, 0, 0.1);
            padding: 5px;
            font-size: 12px;
            height: auto !important;
            z-index: 1350px0001 !important;
        }

        .Mpopup1 {
            position: relative;
            background: #f2f2f2;
            color: #404040;
            text-shadow: 0 1px 0 #fff;
            -ms-filter: "progid:DXImageTransform.Microsoft.dropshadow(OffX=0,OffY=1,Color=#ffffffff,Positive=true)";
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=0, OffY=1, Color=#ffffffff, Positive=true);
            border-radius: 5px;
            box-shadow: 0px 1px 4px rgba(0, 0, 0, 0.1);
            padding: 5px;
            font-size: 12px;
            height: 365px !important;
            z-index: 1350px0001 !important;
        }

        .Mpopupnewline {
            border-top: 2px solid #105f77;
            width: 100%;
            height: 4px;
        }

        .Mpopupheader {
            width: 100%;
            background-color: #454545;
            height: 25px;
            font-size: 12px;
            font-weight: 500;
            color: #f2f2f2;
            text-shadow: 0 1px 0 #add553;
            -ms-filter: "progid:DXImageTransform.Microsoft.dropshadow(OffX=0,OffY=1,Color=#ffffffff,Positive=true)";
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=0, OffY=1, Color=#ffffffff, Positive=true);
            padding: 5px;
        }

        .Mpopupbodycontent {
            width: 100%;
            margin: 3px 0 3px 0
        }

        .Mpopupfooter {
            width: 100%;
            background-color: #454545;
            padding: 3px
        }

        .Requiredvalidate {
            font-size: 12px;
            color: Red;
        }


        .ModalPopupBG {
            background-color: #000000;
            filter: alpha(opacity=80);
            -moz-opacity: 0.5;
            -khtml-opacity: 0.5;
            opacity: 0.5;
            width: 100%;
            height: 100%
        }

        .ModalPopupBGmainentry {
            background-color: #000000;
            filter: alpha(opacity=10);
            -moz-opacity: 1.0;
            -khtml-opacity: 1.0;
            opacity: 1.0;
            width: 100%;
            height: 100%
        }

        table#WebSurtte tr td {
            font-weight: 400;
            font-size: 14px;
        }

        tr.header td table tr td.fs {
            font-size: 14px;
        }






        label, .control-label {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: 400 !important;
            font-size: 12px;
        }
    </style>
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
                if ((charCode > 64 && charCode < 91) || (charCode > 48 && charCode < 57) || (charCode > 96 && charCode < 123) || charCode == 32 || charCode == 0 || charCode == 9 || charCode == 08 || charCode == 44 || charCode == 45 || charCode == 48)
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }


        function onlyAlphabetsHH(e, t) {
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
        function SetMultilanguage(Flag, clsname) {
            var Lngg = "", lid = "";
            var maxSelection = 0;
            $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                Lngg = Lngg + $(this).next().html() + ",";
                lid = lid + $(this).val() + ",";
                maxSelection++;
            });

            Lngg = Lngg.substr(0, Lngg.length - 1);
            lid = lid.substr(0, lid.length - 1);
            if (Flag == 'M') {
                if (maxSelection <= 10) {
                    $('#<%=hhmuhulaid.ClientID %>').val(lid);
                    $('#<%=HidName.ClientID %>').val(Lngg);
                    $('#<%=txtMuhala.ClientID %>').val(Lngg);
                }

                else {
                    $('.' + clsname + ' input[type="checkbox"]:checked').each(function () {
                        $(this).attr("checked", false);
                    });
                    $('#<%=hhmuhulaid.ClientID %>').val('');
                    $('#<%=HidName.ClientID %>').val('');
                    $('#<%=txtMuhala.ClientID %>').val('');


                    $find("Modal_alertB").show();
                    return false;
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
        .ajax__calendar_container {
            z-index: 1000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="mkiiainpnl121">
        <ContentTemplate>
            <input type="hidden" id="HdnCount" value="0" />
            <div class="container-fluid">
                <%--<input type="image" id="ton-new" class="butt" src="Images/close.png"  />
       <div id="div-show-new"></div> --%>
            </div>
            <div class="col-lg-12">
                <div class="panel panel-default" style="padding-bottom: 0px !important;">
                    <div class="panel-heading" style="padding-left: 15px;">

                        <h3 class="text-danger" style="margin: 3px;">Staff Training Scheduler
                        </h3>

                    </div>
                    <div class="panel-body" style="min-height: 500px; margin-bottom: 0px; padding: 10px;">
                        <div id="Project">
                            <div class="panel panel-default" style="margin-bottom: 0px;">
                                <%--   <div class="panel-heading">
                                    <div class="row" style="margin-top: -5px; margin-bottom: -5px; margin-right: 5px; padding: 5px 0;">
                                        <div class="col-12 pull-right">
                                        </div>
                                    </div>--%>
                                <%--<p class="text-danger" style="margin: 0px;">
                                <asp:Label ID="lblHeadingOne" runat="server" Text=""></asp:Label>
                            </p>--%>
                                <%--      </div>--%>
                                <div class="panel-body" style="width: 100%; padding: 10px 0px 15px;">

                                    <div class="form-group">

                                        <div class="col-sm-3">
                                            <label class="control-label">
                                                Year : <span style="color: Red">*</span></label>
                                            <asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                runat="server" class="form-control ">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-sm-3" runat="server" id="d1">
                                            <label class="control-label">
                                                State : <span style="color: Red">*</span></label>
                                            <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                AutoPostBack="true" class="form-control ">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblNumNo" Visible="false" runat="server" Text="Label"></asp:Label>

                                        </div>

                                        <div class="col-sm-3" runat="server" id="d2">
                                            <asp:Label ID="Label2" class="control-label" runat="server"
                                                Text="District">District<span style="color: Red">*</span></asp:Label>

                                            <asp:DropDownList ID="ddlDistrict" runat="server" class="form-control " />
                                        </div>



                                        <div class="col-sm-3">

                                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" Style="margin-top: 22px;" class="btn btn-danger btn-paddd pull-left"
                                                BackColor="transparent" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                                            <asp:LinkButton ID="LinkButton2" Style="margin-top: 22px; margin-left: 15px" runat="server" Text="Export to Excel" OnClick="btnExportExcel_Click"
                                                class="pull-left"></asp:LinkButton>

                                        </div>


                                    </div>

                                </div>
                            </div>
                        </div>
                        <div id="Activity">
                            <div class="panel panel-default" style="margin-top: 10px; margin-bottom: 0px;">
                                <div class="panel-heading">
                                    <div class="row" style="margin-top: -5px; margin-bottom: -5px; margin-right: 5px; padding: 5px 0;">
                                        <div class="col-12 pull-right">
                                            <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-left" BackColor="#f5f5f5"
                                                ToolTip="Add" ImageUrl="~/images/add-29-1.png" OnClick="btnAdd_Click" Style="margin-right: 5px; padding: 0px;"
                                                runat="server" />
                                        </div>
                                    </div>

                                </div>

                                <div class="panel-body scroll" style="min-height: 404px; max-height: 404px;">
                                    <asp:Panel ID="pnlMain" Enabled="true" runat="server">
                                        <div style="height: 375px; overflow: auto; width: 100%;" align="center">
                                            <div>
                                                <div class="Row" style="width: 100%">
                                                    <asp:GridView ID="gvStaffScheduling" ShowFooter="false" CssClass="table table-striped table-bordered table-hover"
                                                        Width="100%" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvStaffScheduling_OnRowCommand">
                                                        <EmptyDataTemplate>
                                                        </EmptyDataTemplate>
                                                        <FooterStyle CssClass="FooterStyle" />
                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="40px" />
                                                        <RowStyle HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                        <HeaderStyle BackColor="#C1C1C1" ForeColor="White" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                        <AlternatingRowStyle BackColor="#f1f1f1" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="District">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="La2belPosition" runat="server" Text='<%# Bind("District") %>' />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList ID="ddlSearchDist" runat="server" class="form-control " />
                                                                </FooterTemplate>
                                                                <HeaderStyle Width="10%" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Start Date">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtEditFromDate" Style="width: 100%; margin-left: -18px;"
                                                                        autocomplete="off" ondrop="return false;" class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" OnClientDateSelectionChanged="arrivaldatecheck"
                                                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtEditFromDate" PopupPosition="BottomRight">
                                                                    </ajax:CalendarExtender>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Labe3lTeam" runat="server" Text='<%# Bind("FromDate") %>' />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                </FooterTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Date">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox runat="server" ID="txtEditToDate" autocomplete="off" ondrop="return false;"
                                                                        class="form-control" onkeypress="return false;"></asp:TextBox>
                                                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" OnClientDateSelectionChanged="arrivaldatecheck"
                                                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtEditToDate" PopupPosition="BottomRight">
                                                                    </ajax:CalendarExtender>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelTeam" runat="server" Text='<%# Bind("ToDate") %>' />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                </FooterTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Outcome">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlLearningOutCome" runat="server" class="form-control">
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelPositi1on" runat="server" Text='<%# Bind("Outcome") %>' />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                </FooterTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Training Mode">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelPosit81n" runat="server" Text='<%# Bind("TrainingMode") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Training Type">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlEditddlTraining" runat="server" class="form-control">
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelPositio1111n" runat="server" Text='<%# Bind("TrainingName") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Entry Done By">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lab4elPosition" runat="server" Text='<%# Bind("UserName") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Specific training">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lab4elPosdition" runat="server" Text='<%# Bind("Other") %>' />
                                                                    <asp:Label ID="lblScheduleID" Visible="false" runat="server" Text='<%# Bind("ScheduleID") %>' />
                                                                    <asp:Label ID="lblFlag" Visible="false" runat="server" Text='<%# Bind("Flag") %>' />
                                                                    <asp:Label ID="lblLockRecord" Visible="false" runat="server" Text='<%# Bind("LockRecord") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ButtonEdit" Visible="false" runat="server" ImageUrl="~/images/edit.png" />
                                                                    <asp:ImageButton ID="ButtonDelete" runat="server" OnClick="btnDelete_Click" ImageUrl="~/images/delete-29.png" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Lock/Unlock" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkLock" OnClick="btnLnk_Click" runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                </div>
                            </div>
                            <cc1:ModalPopupExtender ID="MpexdrDistrict" runat="server" BackgroundCssClass="modalBg "
                                CancelControlID="CancelButton" PopupControlID="PnlDistrict" TargetControlID="HdnFild">
                            </cc1:ModalPopupExtender>
                            <asp:HiddenField ID="HdnFild" runat="server"></asp:HiddenField>
                            <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 45% !important; margin-top: -112px !important;"
                                ID="PnlDistrict" runat="server">
                                <div style="width: 100%; height: auto; background-color: #f1f1f1">
                                    <%-- <div class="modal-header" style="background-color: #ddd; color: White;">
                                                    <h4 class="modal-title" style="forecolor: White">
                                                        Staff Scheduling</h4>
                                                </div>--%>
                                    <div class="modal-header">
                                        <h3 class="text-danger" style="margin: 0;">Staff Scheduling
                                            
                            <asp:LinkButton ID="CancelButton" class="btn btn-xs btn-danger pull-right"
                                runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>
                                        </h3>

                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                        <div class="form-horizontal" role="form">

                                            <div class="form-group">
                                                <asp:Label ID="Label1" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Start Date:">Start Date<span style="color: Red">*</span></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" ID="txtFromDate" autocomplete="off" ondrop="return false;"
                                                        class="form-control" AutoPostBack="true" OnTextChanged="txtdatefrom_TextChanged" onkeypress="return false;"></asp:TextBox>
                                                    <ajax:CalendarExtender ID="CalendarfffExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        TargetControlID="txtFromDate" PopupPosition="BottomRight">
                                                    </ajax:CalendarExtender>
                                                </div>
                                            </div>
                                            <div class="form-group" id="statediv" runat="server">
                                                <asp:Label ID="Label10" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="End Date">End Date<span style="color: Red">*</span></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" OnTextChanged="txtdateto_TextChanged" AutoPostBack="true" ID="txtToDate" autocomplete="off" ondrop="return false;"
                                                        class="form-control" onkeypress="return false;"></asp:TextBox>
                                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        TargetControlID="txtToDate" PopupPosition="BottomRight">
                                                    </ajax:CalendarExtender>
                                                </div>
                                            </div>
                                            <div class="form-group" id="distdiv" runat="server">
                                                <asp:Label ID="lbldist" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Outcome">Outcome<span style="color: Red">*</span></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlLearning" OnSelectedIndexChanged="ddlLearning_SelectedIndexChanged"
                                                        AutoPostBack="true" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" id="divOther" runat="server" visible="false">
                                                <asp:Label ID="Label3" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Specific training"></asp:Label><asp:Label runat="server" Text="*" Style="color: Red"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtOther" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group" id="divOther1" visible="false" runat="server">
                                                <asp:Label ID="Label4" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Specific training"></asp:Label><asp:Label runat="server" Text="*" Style="color: Red; margin-left: -459px; padding: -30px; margin-top: -19px;"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlInducation" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group" id="div1" runat="server">
                                                <asp:Label ID="lbl" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Location"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtLoaction" runat="server" class="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" id="div3" runat="server">
                                                <asp:Label ID="Label5" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Training Mode">Training Mode<span style="color: Red">*</span></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlTraingMode" runat="server" class="form-control" OnSelectedIndexChanged="ddlTraingMode_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Online Training</asp:ListItem>
                                                        <asp:ListItem Value="2">Offline Training</asp:ListItem>
                                                        <asp:ListItem Value="3">Refresher Training</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group" id="div2" runat="server">
                                                <asp:Label ID="Label6" class="control-label col-sm-4 lab-text-left" runat="server"
                                                    Text="Training"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlTraining" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" id="partnerdiv" runat="server">

                                                <asp:LinkButton ID="LnkEntry" class="control-label col-sm-4 lab-text-left" runat="server" OnClick="LnkEntry_Click">
                                   <%-- <span class="glyphicon glyphicon-floppy-save">--%></span>  Entry Done By
                                                </asp:LinkButton>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblUsername2" runat="server" Text=""></asp:Label>
                                                    <asp:DropDownList ID="ddlEmployee" Visible="false" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="TxtEmployee" Enabled="false" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:TextBox ID="txtEmployeName" Enabled="false" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>


                                                    <asp:LinkButton ID="LinkButton1" OnClick="lnkUser_Click" Visible="false" runat="server">Search User</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="overflow: auto; margin-top: 35px; height: 150px;">
                                            <asp:GridView ID="Gv_Display" Width="100%" runat="server"
                                                CssClass=" table table-striped table-bordered table-hover " OnRowDataBound="Gv_Display_OnRowCommand" AutoGenerateColumns="false">

                                                <FooterStyle CssClass="FooterStyle" />
                                                <HeaderStyle BackColor="#C1C1C1" Height="44px" />
                                                <RowStyle HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#897A7A" Font-Bold="True" ForeColor="WhiteSmoke" />
                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUniqueCode" runat="server" Text='<%#Eval("TodayDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Day" HeaderStyle-CssClass="GridHeaderClass">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Full Day</asp:ListItem>
                                                                <asp:ListItem Value="2">First Half</asp:ListItem>
                                                                <asp:ListItem Value="3">Second Half </asp:ListItem>

                                                            </asp:DropDownList>
                                                            <asp:Label runat="server" Visible="false" ID="lbStatus"
                                                                Style="text-decoration: none;"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <div class="modal-footer">
                                        <%--<asp:ImageButton ID="btnNewUserSave" runat="server" ImageUrl="~/images/save-29-1.png"
                                                        Text="Save" ToolTip="Save" OnClientClick="return SaveDataVali();" OnClick="btnSaveNew_Click"
                                                        Style="float: none;" ValidationGroup="saves"></asp:ImageButton>--%>
                                        <asp:LinkButton ID="LinkButton3" OnClientClick="return SaveDataVali();" OnClick="btnSaveNew_Click" ValidationGroup="saves" class="btn btn-xs btn-primary pull-right"
                                            ToolTip="Save" Width="55px"
                                            Style="margin-top: -4px; width: 70px; height: 26px;" runat="server">Save</asp:LinkButton>


                                        &nbsp;

                                                  <%--  <asp:ImageButton ID="CancelButton666" ImageUrl="~/images/close-29.png" runat="server"
                                                        Style="float: none;"></asp:ImageButton>--%>
                                    </div>
                                </div>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="MpexdrDistrict1" runat="server" BackgroundCssClass="modalBg "
                                CancelControlID="CancelButton" PopupControlID="PnlDistrict1" TargetControlID="HiddenField1">
                            </cc1:ModalPopupExtender>
                            <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                            <asp:Panel ID="PnlDistrict1" Style="display: none; height: auto; width: 68% ! important; left: 22% ! important;"
                                runat="server" CssClass="model-wid mod-posi">
                                <div class="modal-header" style="background-color: White; color: Black;">
                                    <asp:ImageButton ID="ImageButton1" CssClass="float-r  pull-right" ImageUrl="~/images/close-29.png"
                                        runat="server" />
                                    <div>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Role:</label>
                                                    <div class="col-sm-9 padd" style="padding-left: 38px;">
                                                        <asp:DropDownList ID="ddlRole" Class="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        State:</label>
                                                    <div class="col-sm-9 padd" style="padding-left: 38px;">
                                                        <asp:ListBox ID="lstState" AutoPostBack="true" SelectionMode="Multiple" OnTextChanged="lstState_TextChanged"
                                                            Height="50px" Width="150px" runat="server"></asp:ListBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 cpl-xs-12" style="padding-left: 5px;">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px; padding-left: 26px;">
                                                        District:</label>
                                                    <div class="col-sm-6 padd" style="padding-left: 16px;">
                                                        <asp:TextBox ID="txtMuhala" runat="server" Width="220%" autocomplete="off" ondrop="return false;"
                                                            CssClass="form-control" onkeypress="return false;" TabIndex="5"></asp:TextBox>
                                                        <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="txtMuhala"
                                                            PopupControlID="pnt_Muhula" OffsetY="22">
                                                        </cc1:PopupControlExtender>
                                                        <asp:Panel ID="pnt_Muhula" runat="server" Direction="LeftToRight" Style="display: none; min-height: 60px; max-height: 300px; overflow: auto; z-index: 999999; background-color: #F1F1F1; border: solid 1px #cccccc; width: 184%"
                                                            CssClass="panel">
                                                            <span>
                                                                <asp:CheckBoxList ID="CBL_Muhula" CssClass="_bookformat1 radio" runat="server" onclick="SetMultilanguage('M','_bookformat1');">
                                                                </asp:CheckBoxList>
                                                            </span>
                                                            <asp:HiddenField runat="server" ID="hhmuhulaid" />
                                                            <asp:HiddenField runat="server" ID="HidName" />
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                                                <div class="col-lg-5 col-md-5 col-sm-5 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Type:</label>
                                                        <div class="col-sm-9 padd">
                                                            <asp:DropDownList ID="ddlType" runat="server" class="form-control ">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Employee Code </asp:ListItem>
                                                                <asp:ListItem Value="2">Name </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-lg-5 col-md-5 col-sm-5 cpl-xs-12">
                                                    <div class="form-group">
                                                        <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                            Name/UserID:</label>
                                                        <div class="col-sm-9 padd" style="padding-left: 38px;">
                                                            <asp:TextBox ID="txtSearchUser" runat="server" class="form-control ">
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-2 cpl-xs-12">
                                                    <div class="form-group">
                                                        <div class="col-sm-9 padd">
                                                            <asp:ImageButton ID="ImageButton2" ToolTip="Serach" runat="server" OnClick="btn_AddEmp"
                                                                class="btn btn-danger btn-paddd pull-right" BackColor="#f1f1f1" ImageUrl="~/images/search-29.png" />
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-body" style="background: #FFFFFF; padding: 0px;">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 " style="margin-top: 15px;">
                                        <div class="row">
                                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                                <div class="panel panel-success" style="width: 328%; padding-left: 0px;">
                                                    <div class="search-bg">
                                                        Selected Staff
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="panel panel-default" style="margin-bottom: 0px; overflow: auto; min-height: 100px; max-height: 150px; width: 20%;">
                                                            <asp:CheckBoxList ID="lstUser" Width="200px" runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                        <div class="row">
                                                            <asp:Button ID="BtnOK" Text="Ok" OnClick="btnUser_Click" runat="server" CssClass="btn btn-danger pull-right" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                    </div>
                                </div>
                            </asp:Panel>


                            <ajax:ModalPopupExtender ID="MPE_Entry" BackgroundCssClass="modalBackground"
                                runat="server" PopupControlID="Pnl_Entry" TargetControlID="HdnEntry">
                            </ajax:ModalPopupExtender>
                            <asp:HiddenField ID="HdnEntry" runat="server" />

                            <asp:Panel ID="Pnl_Entry" runat="server" CssClass=" model-wid Mpopup1 mod-posi" Style="height: 590px  !important; width: 40% !important; margin-top: -112px !important; display: none;">

                                <div style="border: 0px solid #ccc; width: 100%; min-height: 100px; margin: 0 auto;">
                                    <div class="modal-header">
                                        <h3 class="text-danger" style="margin: 0;">Add Entry Done By 
                                            
                            <asp:LinkButton ID="lnkEntryClose" OnClick="BtnEntry2_Click" class="btn btn-xs btn-danger pull-right"
                                runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>



                                        </h3>

                                    </div>
                                    <div class="modal-body">
                                        <div style="height: auto;">
                                            <div class="form-group">
                                                <div class="row" runat="server" id="Div4">
                                                    <div class="form-group">
                                                        <label class="control-label" style="margin-top: 10px; text-align: left;">
                                                            Entry Done By  : <span style="color: Red">*</span></label>
                                                        <div class="">
                                                            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" TabIndex="4" CssClass="form-control input-sm" Style="margin-top: 5px; height: 80px !important;"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox1" Display="Dynamic" ErrorMessage="Please enter Participate" ForeColor="Red" SetFocusOnError="True" ValidationGroup="QuestionCreate1">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row" runat="server" id="Div5" style="margin-bottom: 15px;">
                                                    <div class="form-group">

                                                        <div class="col-sm-12">
                                                            <asp:LinkButton ID="BtnEntry" OnClick="BtnEntry_Click" class="btn btn-xs btn-primary pull-right"
                                                                ToolTip="Save" Width="55px"
                                                                Style="margin-top: -4px; width: 70px; height: 26px;" runat="server">Save</asp:LinkButton>


                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" style="overflow: auto; margin-top: 2px; height: 270px;">
                                                    <%--<div style="overflow: auto; margin-top: -5px; height: 350px;">--%>
                                                    <asp:GridView ID="GvEntry" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display." AllowSorting="True" GridLines="Both" BorderColor="#e1e1e1" AlternatingRowStyle-BackColor="#F7F7F7"
                                                        CssClass="table table-striped table table-hover table-bordered" SelectedRowStyle-BackColor="#e1f4a6"
                                                        AllowPaging="false" Style="color: #333333" ShowHeaderWhenEmpty="true" DataKeyNames="ParticiparticipateName,EntryDoneByName">
                                                        <FooterStyle CssClass="DataGridFooter" />
                                                        <PagerStyle CssClass="paging" />
                                                        <HeaderStyle CssClass="DataGridHeader" />
                                                        <SelectedRowStyle BackColor="#D5D5BF" Font-Bold="True" />
                                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="1%">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Entry Code" ItemStyle-Width="1%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEntryCode" runat="server" Text='<%#Bind("ParticiparticipateName") %>'></asp:Label>
                                                                </ItemTemplate>

                                                                <ItemStyle Width="20%" CssClass="gvtextcenter" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Entry Name" ItemStyle-Width="1%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEntryName" runat="server" Text='<%#Bind("EntryDoneByName") %>'></asp:Label>
                                                                </ItemTemplate>

                                                                <ItemStyle Width="20%" CssClass="gvtextcenter" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>

                                                                    <asp:LinkButton ID="Delete_QuestionEntry" OnClientClick="javascript:return confirm('Are you sure you want to delete this record?');" OnClick="Delete_Question_Click1" class="btn btn-sm btn-warning" runat="server">
                                                                     <span class="glyphicon glyphicon-trash" data-fa-transform="shrink-10 up-.5" style="color:red"></span>
                                                                        
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="5%" CssClass="gvtextcenter" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>

                                                </div>

                                            </div>
                                        </div>

                                        <div class="modal-footer">
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>

            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="LinkButton2" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker4').datetimepicker();
        });
    </script>
</asp:Content>
