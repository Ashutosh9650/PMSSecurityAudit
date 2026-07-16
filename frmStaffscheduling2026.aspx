<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmStaffscheduling2026.aspx.cs"
    MasterPageFile="~/Site.master" Culture="en-GB" Inherits="frmStaffscheduling2026" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
     <script language="Javascript" type="text/javascript">
     function CheckAll(headerCheckbox) {
         var gridView = document.getElementById('<%= GridView1.ClientID %>');
            var checkboxes = gridView.getElementsByTagName("input");


            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].type === "checkbox" && checkboxes[i].id.indexOf("chkSelect") > -1) {
                    checkboxes[i].checked = headerCheckbox.checked;
                }
            }
         }
     </script>
        <script language="Javascript" type="text/javascript">
            function ValidateEmail(inputText) {


                var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
                if (inputText.value.match(mailformat)) {
                    document.form1.text1.focus();
                    return true;
                }
                else {
                    alert("You have entered an invalid email address!");
                    inputText.value = '';
                    inputText.focus();
                    return false;
                }
            }

        </script>
    <style type="text/css">
        .modalBackground {
            background-color: rgba(0,0,0,0.5);
        }
        label {
    display: flex;
    align-items: center;
    margin-top: 6px;
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
        table.td_sty tbody tr td:nth-child(1){
            text-align:center;
        }
       /* .table {
            width: 138% !important;
            max-width: 100% !important;
            margin-bottom: 15px;
            margin-left: 0px;
        }*/

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

        .grid-container_filt {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(220px, 1fr));
            gap: 15px;
            padding: 0px 12px;
        }

        .grid-item {
            border-radius: 10px;
            display: flex;
            justify-content: space-around;
            align-items: center;
            gap: 12px;
            flex-wrap: nowrap;
        }

            .grid-item label {
                width: 110px
            }

        .row {
            margin-right: -15px;
            margin-left: -15px;
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

                        <h3 class="text-danger" style="margin: 3px;">Training Scheduler
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
                                    <div class="grid-container_filt">
                                        <div class="grid-item">
                                            <label class="control-label">
                                                Year : <span style="color: Red">*</span></label>
                                            <asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                runat="server" class="form-control ">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="grid-item" runat="server" id="d1">
                                            <label class="control-label">
                                                State : <span style="color: Red">*</span></label>
                                            <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                AutoPostBack="true" class="form-control ">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblNumNo" Visible="false" runat="server" Text="Label"></asp:Label>
                                        </div>
                                        <div class="grid-item" runat="server" id="d2">
                                            <asp:Label ID="Label2" class="control-label" runat="server"
                                                Text="District">District<span style="color: Red">*</span></asp:Label>

                                            <asp:DropDownList ID="ddlDistrict" runat="server" class="form-control " />
                                        </div>
                                        <div class="grid-item">
                                            <label>Training Type</label>
                                            <asp:DropDownList ID="ddlStype" runat="server" OnSelectedIndexChanged="ddlStype_SelectedIndexChanged"
                                                AutoPostBack="true" class="form-control">
                                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Staff Training</asp:ListItem>
                                                        <asp:ListItem Value="2">Team Balika Training</asp:ListItem>
                                                        
                                                    </asp:DropDownList>
                                            
                                        </div>
                                        <div class="grid-item" style="display: flex; justify-content: end; align-items: center; gap: 15px;">
                                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" Style="margin-top: 0px;" class="btn btn-danger btn-paddd pull-left"
                                                BackColor="transparent" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                                            <asp:LinkButton ID="LinkButton2" Style="margin-top: 0px;" runat="server" Text="Export to Excel" OnClick="btnExportExcel_Click"
                                                class="pull-left"></asp:LinkButton>

                                            <div style="margin-top: 0px;">
                                                <asp:ImageButton ID="btnAdd" CssClass="btn btn-info pull-left" BackColor="#f5f5f5"
                                                    ToolTip="Add" ImageUrl="~/images/add-29-1.png" OnClick="btnAdd_Click" Style="margin-right: 5px; padding: 0px;"
                                                    runat="server" />
                                            </div>
                                        </div>

                                    </div>



                                </div>
                            </div>
                        </div>
                        <div id="Activity" style="padding-top: 15px">

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
                                                            <asp:Label ID="La2belPosition" runat="server" Text='<%# Bind("DistrictName") %>' />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="ddlSearchDist" runat="server" class="form-control " />
                                                        </FooterTemplate>
                                                        
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Training Date">
                                                      
                                                        <ItemTemplate>
                                                            <asp:Label ID="Labe3lTeam" runat="server" Text='<%# Bind("ToDate") %>' />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                      
                                                    </asp:TemplateField>
                                                    
                                               
                                                                 
                                                    <asp:TemplateField HeaderText="Training Outcome">
                                                    
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelP98ositi1on" runat="server" Text='<%# Bind("Outcome") %>' />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                      
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Specific Training Name">

                                                        <ItemTemplate>
                                                             <asp:Label ID="LabelPositt81n" runat="server" Text='<%# Bind("sOutcomeName") %>' />
                                                        </ItemTemplate>
                                                       
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Training Mode">
                                                        
                                                        <ItemTemplate>
                                                            <asp:Label ID="Labe667lPositio1111n" runat="server" Text='<%# Bind("TrainingMode") %>' />
                                                        </ItemTemplate>
                                                       
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Resc Status">
                                                        
                                                        <ItemTemplate>
                                                            <asp:Label ID="Labe66lPosigtio1111n" runat="server" Text='<%# Bind("TrainingName") %>' />
                                                        </ItemTemplate>
                                                       
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Entry Done By">
                                                        <ItemTemplate>
                                                          <asp:Label ID="LabelPositio1111n" runat="server" Text='<%# Bind("SCreatedBy") %>' />
                                                        </ItemTemplate>
                                                      
                                                    </asp:TemplateField>
													 <asp:TemplateField HeaderText="#Participant">
                                                        <ItemTemplate>
                                                          <asp:Label ID="LabelPggositio1111n" runat="server" Text='<%# Bind("TotalP") %>' />
                                                        </ItemTemplate>
                                                      
                                                    </asp:TemplateField>
													
                                                    <asp:TemplateField HeaderText="Specific training" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lab4elPosdition" runat="server" Text='<%# Bind("Other") %>' />
                                                            <asp:Label ID="lblScheduleID" Visible="false" runat="server" Text='<%# Bind("ScheduleID") %>' />
                                                            <asp:Label ID="lblFlag" Visible="false" runat="server" Text='<%# Bind("Flag") %>' />
                                                               <asp:Label ID="lblAssmentFlag" Visible="false" runat="server" Text='<%# Bind("AssmentFlag") %>' />
                                                            <asp:Label ID="lblLockRecord" Visible="false" runat="server" Text='<%# Bind("LockRecord") %>' />
                                                        </ItemTemplate>
                                                      
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                         <ItemTemplate>
                                                            <asp:LinkButton ID="ButtonEdit" runat="server" OnClick="btnEdit_Click" Text="Edit"  />
                                                          
                                                      </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>

                                                              <asp:LinkButton ID="ButtonDelete" OnClick="btnDelete_Click" class="btn btn-sm btn-link" runat="server">
                                                                     <span class="glyphicon glyphicon-trash" data-fa-transform="shrink-10 up-.5" style="color:red"></span>
                                                                        
                                                    </asp:LinkButton>

                                                
                                                        </ItemTemplate>
                                                       
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lock/Unlock" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkLock" OnClick="btnLnk_Click" runat="server" />
                                                        </ItemTemplate>
                                                      
                                                    </asp:TemplateField>

                                                        
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>


                            <cc1:ModalPopupExtender ID="MpexdrDistrict" runat="server" BackgroundCssClass="modalBg "
                                CancelControlID="CancelButton" PopupControlID="PnlDistrict" TargetControlID="HdnFild">
                            </cc1:ModalPopupExtender>
                            <asp:HiddenField ID="HdnFild" runat="server"></asp:HiddenField>
                            <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 90% !important; margin-top: -112px !important;"
                                ID="PnlDistrict" runat="server">
                                <div style="width: 100%; height: auto; background-color: #f1f1f1">
                                   
                                    <div class="modal-header">
                                        <div style="display: flex; justify-content: space-between; align-items: center; gap: 12px">
                                            <h3 class="text-danger" style="margin: 0;">Create Scheduler</h3>
                                            <div style="display: flex; align-items: center;">
                                               
                                                  <asp:ImageButton ID="btnsave" OnClick="btnSaveNew_Click" ValidationGroup="savesNew" CssClass="btn btn-link btn-xs" ToolTip="Save"
                               ImageUrl="~/images/save-29.png" Style="height: 22px;"
                                runat="server" />
                                       
                                                  <asp:ImageButton ID="ImageButton1" OnClick="btDownload_Click" CssClass="btn btn-link btn-xs" ToolTip="Save"
                               ImageUrl="~/images/ex1.png" Style="height: 22px;"
                                runat="server" />                           

                                                
                                                <asp:LinkButton ID="CancelButton" class="btn btn-xs btn-danger pull-right"
                                                    runat="server"><span class="glyphicon glyphicon-remove"></span> </asp:LinkButton>
                                            </div>
                                        </div>






                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">Start Date <span style="color: Red">*</span></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" ID="txtFromDate" autocomplete="off" ondrop="return false;"
                                                                class="form-control" AutoPostBack="true" OnTextChanged="txtdatefrom_TextChanged" onkeypress="return false;"></asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarfffExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="txtFromDate" PopupPosition="BottomRight">
                                                            </ajax:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">End Date <span style="color: Red">*</span></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" OnTextChanged="txtdateto_TextChanged" AutoPostBack="true" ID="txtToDate" autocomplete="off" ondrop="return false;"
                                                                class="form-control" onkeypress="return false;"></asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="txtToDate" PopupPosition="BottomRight">
                                                            </ajax:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">Training Outcome <span style="color: Red">*</span></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlLearning" OnSelectedIndexChanged="ddlLearning_SelectedIndexChanged"
                                                                AutoPostBack="true" runat="server" class="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="divOther" runat="server" visible="false">
                                                    <div class="row">
                                                        <label class="col-sm-4">Specific training <span style="color: Red">*</span></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtOther" class="form-control" runat="server"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="divOther1" visible="false" runat="server">
                                                    <div class="row">
                                                        <label class="col-sm-4">Specific Training Name <span style="color: Red">*</span></label>
                                                        <div class="col-sm-8">

                                                            <asp:DropDownList ID="ddlInducation" runat="server" class="form-control">
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">Training venue <span style="color: Red">*</span></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtLoaction" runat="server" class="form-control">
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">Training Mode <span style="color: Red">*</span></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlTraingMode" runat="server" class="form-control" >
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Online Training</asp:ListItem>
                                                                <asp:ListItem Value="2">Offline Training</asp:ListItem>
                                                                <asp:ListItem Value="3">Refresher Training</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="div2" runat="server">
                                                    <div class="row">
                                                        <label class="col-sm-4">Residencial Status <span style="color: Red">*</span></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlTraining" runat="server" class="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">Trainer Type</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlTrainerTyep" runat="server" class="form-control" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">Internal</asp:ListItem>
                                                    <asp:ListItem Value="2">External</asp:ListItem>
                                                </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" runat="server" visible="false" id="EV1">
                                                    <div class="row">
                                                        <label class="col-sm-4">External Trainer Name</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtTrainename"  class="form-control" runat="server"></asp:TextBox>
                                                            <asp:Label ID="lblShulderID" runat="server" Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" runat="server" visible="false" id="EV2">
                                                    <div class="row">
                                                        <label class="col-sm-4">External Trainer Email</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtEmail"  onchange="javascript:ValidateEmail(this);" class="form-control" runat="server"></asp:TextBox>
                           
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" runat="server" visible="false" id="EV3">
                                                    <div class="row">
                                                        <label class="col-sm-4">External Trainer Contact No.</label>
                                                        <div class="col-sm-8">
                                                         <asp:TextBox ID="txtContact"  OnKeyUp="javascript:inputtxt();" runat="server" MaxLength="10"
                                                    onkeypress="return isNumberKey(this,event);" onchange="javascript: phonenumber(this.value,'TeContact1');"
                                                    autocomplete="off" ondrop="return false;" class="form-control TeContact1 " />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">Participant Type <span style="color: Red">*</span></label>
                                                        <div class="col-sm-8">
                                                             <asp:DropDownList ID="ddlType" runat="server" class="form-control" >
                                                  
                                                             <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">Participants</asp:ListItem>
                                                    <asp:ListItem Value="2">Trainer</asp:ListItem>
                                                                   <asp:ListItem Value="3">Observer</asp:ListItem>
                                                                  
                                                        </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-4">Training Participents <span style="color: Red">*</span></label>
                                                        <div class="col-sm-8">
                                                       
                                                            
                                            <asp:TextBox ID="txtParticipate" runat="server" TextMode="MultiLine" TabIndex="4" CssClass="form-control input-sm" Style="margin-top: 5px; height: 160px !important;"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        
                                                        <div class="col-sm-12 text-right">
                                                            <asp:Button  runat="server" ID="btnhh" OnClick="btnParticipate_Click" Text="Add" class="btn btn-link btn-sm "></asp:Button>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
 <div style="height: 275px; " class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display." AllowSorting="True" GridLines="Both" BorderColor="#e1e1e1" AlternatingRowStyle-BackColor="#F7F7F7"
                                        CssClass="table table-striped table table-hover table-bordered td_sty " SelectedRowStyle-BackColor="#e1f4a6"
                                        AllowPaging="false" Style="color: #333333" ShowHeaderWhenEmpty="true" DataKeyNames="ParticipantCode">
                                        <FooterStyle CssClass="DataGridFooter" />
                                        <PagerStyle CssClass="paging" />
                                        <HeaderStyle CssClass="DataGridHeader" />
                                        <SelectedRowStyle BackColor="#D5D5BF" Font-Bold="True" />
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>

                                             <asp:TemplateField HeaderText="Select">
                                    <HeaderTemplate>
                                        <span></span><asp:CheckBox ID="chkSelectAll" runat="server" onclick="CheckAll(this);" />
                                         <asp:LinkButton ID="Delete_Questionttt" OnClick="btnDeleteSelected_Click"  class="btn btn-sm btn-link" runat="server">
                                                                     <span class="glyphicon glyphicon-trash" data-fa-transform="shrink-10 up-.5" style="color:red"></span>
                                                                        
                                                    </asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
              
                                    </ItemTemplate>
                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Participant Type" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOptisss559eeonse" runat="server" Text='<%#Bind("ParticipantTypeName") %>'></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle Width="20%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Emp Code" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOptisss55eeonse" runat="server" Text='<%#Bind("ParticipantCode") %>'></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle Width="20%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblOptieeo5nse" runat="server" Text='<%#Bind("ParticipantName") %>'></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle Width="20%" CssClass="gvtextcenter" />
                                            </asp:TemplateField>
                                     
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="Delete_Questionttt" OnClick="Delete_Question_Click2" OnClientClick="javascript:return confirm('Are you sure you want to delete this record?');"  class="btn btn-sm btn-link" runat="server">
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
                                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"></asp:Label>

                                        <div class="col-sm-6">
                                            <asp:Label ID="lblUsername2" runat="server" Text=""></asp:Label>
                                            <asp:DropDownList ID="ddlEmployee" Visible="false" runat="server" class="form-control">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="TxtEmployee" Enabled="false" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:TextBox ID="txtEmployeName" Enabled="false" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>


                                            <asp:LinkButton ID="LinkButton1" OnClick="lnkUser_Click" Visible="false" runat="server">Search User</asp:LinkButton>
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
                                </div>
                            
                       

                    </asp:Panel>

                    <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                 
                </div>
            </div>
            </div>
            </div>

            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnsave" />
                        <asp:PostBackTrigger ControlID="ImageButton1" />
               <asp:PostBackTrigger ControlID="LinkButton2" />
            
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker4').datetimepicker();
        });
    </script>
</asp:Content>
