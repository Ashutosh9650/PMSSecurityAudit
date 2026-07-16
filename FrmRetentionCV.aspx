<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FrmRetentionCV.aspx.cs" Culture="en-GB" Inherits="FrmRetentionCV" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function SetCollaps() {
            setTimeout(function () {
                $('.clcss').hide();
            });

        }
        function togglediv(id) {
            $("#" + id).toggle();
            return false;
        }
    </script>
    <style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
        }

        .modalpopupcss {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }

        .btn-margin {
            margin: 0px 5px;
        }

        .modalPopup {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
            z-index: 999999;
        }


        .panel-heading .accordion-toggle:after {
            /* symbol for "opening" panels */
            font-family: 'Glyphicons Halflings'; /* essential for enabling glyphicon */
            content: "\e114"; /* adjust as needed, taken from bootstrap.css */
            float: right; /* adjust as needed */
            color: grey; /* adjust as needed */
        }

        .panel-heading .accordion-toggle.collapsed:after {
            /* symbol for "collapsed" panels */
            content: "\e080"; /* adjust as needed, taken from bootstrap.css */
        }
    </style>
    <script type="text/javascript">
        debugger;
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
    <script>
        $(document).on('click', '.chkQCSelectAll', function () {
            if ($(this).find('input').is(':checked')) {
                $('.chkQCFormQues input').prop('checked', true);
            }
            else {
                $('.chkQCFormQues input').prop('checked', false);
            }
        })



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="padding: 5px 20px;">
                            <h3 class="text-danger" style="margin: 0px;">Retention Course Correction
                            </h3>
                        </div>
                    </div>
                    <div class="row">
                        <div class="row search-bg">
                            <div class="form-horizontal">
                                <%-- <asp:UpdatePanel runat="server" ID="UpMain" UpdateMode="Conditional">
        <ContentTemplate>--%>
                                <div class="col-lg-12 col-md-10 col-sm-10 cpl-xs-12">
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                                <div class="form-group">
                                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                        Year:
                                                                    </label>
                                                                    <div class="col-sm-9 padd">
                                                                        <asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"    runat="server"   class="form-control ">
                                                                                 </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                       <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group" style="margin-bottom: 7px;">
                                            <label for="email" class="col-sm-3 padd linhei">
                                                State:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlState"  OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                            AutoPostBack="true" runat="server" class="form-control ">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                   <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group" style="margin-bottom: 7px;">
                                            <label for="email" class="col-sm-3 padd linhei">
                                                District:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlDistrict" runat="server"  OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                            AutoPostBack="true" class="form-control ">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                        <div class="form-group" style="margin-bottom: 7px;">
                                            <label for="email" class="col-sm-3 padd linhei">
                                                Block:</label>
                                            <div class="col-sm-9 padd">
                                                <asp:DropDownList ID="ddlBlock" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_OnSelectedIndexChanged" runat="server" class="form-control ">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                     </div>
                                     <div class="col-lg-12 col-md-10 col-sm-10 cpl-xs-12">
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
                                      <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Village:</label>
                                                    <div class="col-sm-9 padd">
                                                        <asp:DropDownList ID="ddlVillage" runat="server" class="form-control " />
                                                    </div>
                                                </div>
                                            </div>
                                    <div class="col-lg-6 col-md-6  col-sm-2 cpl-xs-12">


                                        <asp:Button ID="btnReport" OnClick="btnReport_Click" Style="display: none" CssClass="btn btn-success pull-right btn-margin"
                                            ToolTip="Save" Text="Report" runat="server" />
                                        
                                        <asp:LinkButton ID="btnBack" Visible="false" class="btn btn-primary pull-right btn-sm"
                                            OnClick="btnBack_Click"
                                            runat="server">Back</asp:LinkButton>
                                        <asp:LinkButton ID="Button3" class="btn btn-primary btn-sm pull-right btn-margin"
                                            ToolTip="Add" runat="server" OnClick="btnDOwnload_Click">Download Excel</asp:LinkButton>
                                        <asp:LinkButton ID="btnSerach" runat="server" OnClick="btnSerach_Click"
                                            class="btn btn-primary btn-sm pull-right btn-margin"
                                            ToolTip="Add" Style="">Search</asp:LinkButton>

                                        

                                        <%-- <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                        <%-- <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                          <asp:Button ID="Button1"   CssClass="btn btn-success pull-right" 
                                 ToolTip="Save" Text="Report"  
                                Style="margin-right: 5px; padding: 0px;" runat="server" /></div>
                              
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server"  OnClick="btnSerach_Click"  class="btn btn-danger btn-paddd pull-left" BackColor="#f1f1f1"
                                ImageUrl="~/images/search-29.png"  Style="margin-left: -49px; padding: 0px;"   />
                                </div>
                      <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                          <asp:Button ID="btnApprove"   CssClass="btn btn-success pull-right" 
                                 ToolTip="Save" Text="Approve"  Visible="false"    OnClick="btnApprove_Click" 
                                Style="margin-right: 5px; padding: 0px;" runat="server" /></div>
                                        --%>
                                    </div>
                                </div>
                            </div>
                        </div>



                        <div class="row">
                            <div class="col-sm-12" style="padding: 0px">
                                <div class="panel-group" id="accordion">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <%-- <h4 class="panel-title">
        <%--<a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
         <span style="color:blue"> School Activity </span>
        </a>
      </h4>--%>
                                        </div>
                                        <div id="collapseOnedd" class="">

                                            <div class="panel-body">

                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 pull-right" style="margin-bottom: 10px;" id="dvMain" runat="server" visible="false">
                                                    <asp:LinkButton ID="btnReject" class="btn btn-primary btn-sm pull-right" Width="70px"
                                                        ToolTip="Add" Style="margin-right: 15px; padding: 0px;" runat="server" OnClick="btnReject_Click">  Reject</asp:LinkButton>

                                                    <asp:LinkButton ID="btnApprove" class="btn btn-primary btn-sm pull-right" Width="70px"
                                                        ToolTip="Add" Style="margin-right: 15px; padding: 0px;" runat="server" OnClick="btnApprove_Click"> Approve</asp:LinkButton>




                                                </div>
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px;">
                                                    <div style="height: 430px; overflow: auto; width: 100%;" align="center">
                                                        <asp:GridView ID="Gv_Profile_Search" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                            AllowPaging="true" PageSize="100" AutoGenerateColumns="False" Font-Names="Arial"
                                                            Font-Size="11px" Width="100%">
                                                            <EmptyDataTemplate>
                                                                <div style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                    Data not found
                                                                </div>
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
                                                                        <asp:CheckBox ID="chkHeader" class="chkQCSelectAll" runat="server" Text="Select All" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkFormName" class="chkQCFormQues" runat="server" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="5%" CssClass="gvtextcenter" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="School">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol2_2" ForeColor="Black" Text='<%# Eval("School") %>' Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>

                                                                        <asp:Label ID="lblUniqueChildCode" ForeColor="Black" Visible="false" Text='<%# Eval("UniqueChildCode") %>' Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>
                                                                        <asp:Label ID="lblFlag" ForeColor="Black" Visible="false" Text='<%# Eval("Flag") %>' Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>
                                                                        <asp:Label ID="lblClass" ForeColor="Black" Visible="false" Text='<%# Eval("ClassID") %>' Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>
                                                                        <asp:Label ID="lblRe" ForeColor="Black" Visible="false"   Text='<%# Eval("Re") %>' Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>

                                                                        <asp:Label ID="lblAtt" ForeColor="Black" Visible="false"    Text='<%# Eval("last_attendanceS") %>' Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>
                                                                        <asp:Label ID="lblchild_key" ForeColor="Black" Visible="false"    Text='<%# Eval("child_key") %>' Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DISECode">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_22" ForeColor="Black" Text='<%# Eval("Dise Code") %>' Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Uniqueid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol422" ForeColor="Black" Text='<%# Eval("UID") %>' Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Child Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_2" ForeColor="Black" Text='<%# Eval("Child Name") %>' Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CV Confirm">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblChildName" ForeColor="Black" Text='<%# Eval("CV Confirm") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Ret Not Done">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_4" ForeColor="Black" Text='<%# Eval("Ret Not Done") %>' Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Is Child name Available in Attendance Register">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFatherName" ForeColor="Black" Text='<%# Eval("Is Child name Available in Attendance Register") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Is Child name Available in Attendance Register CV">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_41" ForeColor="Black" Text='<%# Eval("is_child_name_available_in_attendance_registerCV") %>' Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Current Year Registration">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMotherName" ForeColor="Black" Text='<%# Eval("Current Year Registration") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Ret-Current Class">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_123" ForeColor="Black" Text='<%# Eval("Ret-Current Class") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CV-Current Class">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcorrect_class" ForeColor="Black" Text='<%# Eval("CV-Current Class") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Ret-Last Attendance Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_411" ForeColor="Black" Text='<%# Eval("Ret-Last Attendance Date") %>' Font-Names="Calibri" ItemStyle-ForeColor="#333"
                                                                            runat="server"></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CV-Last Attendance Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_511" ForeColor="Black" Text='<%# Eval("CV-Last Attendance Date") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Ret-Reason-Not Found Child in Attendance Register">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCol_6" Text='<%# Eval("Ret-Reason-Not Found Child in Attendance Register") %>' ForeColor="Black" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CV-Reason-Not Found Child in Attendance Register">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSrNo" ForeColor="Black" Text='<%# Eval("CV-Reason-Not Found Child in Attendance Register") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Ret-Support Required for Regular Attendance">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDOB199" ForeColor="Black" Text='<%# Eval("Ret-Support Required for Regular Attendance") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CV-Support Required for Regular Attendance">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDOB1" ForeColor="Black" Text='<%# Eval("CV-Support Required for Regular Attendance") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CV-Enrolment Year">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcorrect99_dob" ForeColor="Black" runat="server" Text='<%# Eval("CV-Enrolment Year") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CV-Class Error">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCVClassError" ForeColor="Black" Text='<%# Eval("CV-Class Error") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="regular_attendance">
                                                                    <ItemTemplate>

                                                                        <asp:Label ID="lblregularattendance" ForeColor="Black" Text='<%# Eval("regular_attendance") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="CV-Retention Error">
                                                                    <ItemTemplate>

                                                                        <asp:Label ID="lblCol_12" ForeColor="Black" Text='<%# Eval("CV-Retention Error") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CV-Last Attendance Error">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCVLastAttendanceError" ForeColor="Black" Text='<%# Eval("CV-Last Attendance Error") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Condition Applied">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcorrect_soc_8cat" ForeColor="Black" Text='<%# Eval("Condition Applied") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>

                                                                     
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button3" />


        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
