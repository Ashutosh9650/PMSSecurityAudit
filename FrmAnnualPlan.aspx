<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FrmAnnualPlan.aspx.cs"  Inherits="FrmAnnualPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
 <script type="text/javascript">
     function onEvent() {

         var Grid_Table = document.getElementById('<%= GV_AnnualPlan.ClientID %>');
         var inputs = Grid_Table.getElementsByTagName("input");
         var vv = $('.cMay').val();
         var msg = "";
         for (var row = 0; row < Grid_Table.rows.length; row++) {
             if (row == 3) {
                 for (var col = 2; col < Grid_Table.rows[row].cells.length; col++) {
                     var uy = 0
                     for (var i = 0; i < inputs.length; i++) {
                         if (inputs[i].type == "text") {
                             if (i >= 24 && i <= 27) {

                                 if (inputs[i].value > 1) {
                                     alert('value cannot be greater then one');
                                     inputs[i].value = 0;
                                 }
                                 else if (inputs[i].value == 1 && inputs[i].value != 0) {
                                     uy = uy + 1;
                                     if (uy > 1) {
                                         alert('Only 1 entry should be allowed in any month');
                                         inputs[i].value = 0;
                                     }

                                 }

                             }
                         }
                     }
                 }
             }
             if (row == 12) {
                 for (var col = 2; col < Grid_Table.rows[row].cells.length; col++) {
                     var uy = 0;
                     for (var i = 0; i < inputs.length; i++) {
                         if (inputs[i].type == "text") {
                             if (i >= 132 && i <= 135) {
                                 if (inputs[i].value > 1) {
                                     alert('value cannot be greater then one');
                                     inputs[i].value = 0;
                                 }
                                 else if (inputs[i].value == 1 && inputs[i].value != 0) {
                                     uy = uy + 1;
                                     if (uy > 1) {
                                         alert('Only 1 entry should be allowed in any month');
                                         inputs[i].value = 0;
                                     }

                                 }
                             }
                         }
                     }
                 }
             }
             if (row == 13) {
                 for (var col = 2; col < Grid_Table.rows[row].cells.length; col++) {
                     for (var i = 0; i < inputs.length; i++) {
                         if (inputs[i].type == "text") {
                             if (i >= 144 && i <= 150) {
                                 if (inputs[i].value > 1) {
                                     alert('value cannot be greater then one');
                                     inputs[i].value = 0;
                                 }
                             }
                         }
                     }
                 }
             }

             if (row == 15) {
                 for (var col = 2; col < Grid_Table.rows[row].cells.length; col++) {
                     for (var i = 0; i < inputs.length; i++) {
                         if (inputs[i].type == "text") {
                             if (i >= 168 && i <= 179) {
                                 if (inputs[i].value > 1) {
                                     alert('value cannot be greater then one');
                                     inputs[i].value = 0;
                                 }
                             }
                         }
                     }
                 }
             }
             if (row == 16) {
                 for (var col = 2; col < Grid_Table.rows[row].cells.length; col++) {
                     for (var i = 0; i < inputs.length; i++) {
                         if (inputs[i].type == "text") {
                             if (i >= 186 && i <= 190) {
                                 if (inputs[i].value > 1) {
                                     alert('value cannot be greater then one');
                                     inputs[i].value = 0;
                                 }
                             }
                         }
                     }
                 }
             }

             if (row == 18) {

                 for (var col = 2; col < Grid_Table.rows[row].cells.length; col++) {
                     var uy = 0;
                     for (var i = 0; i < inputs.length; i++) {
                         if (inputs[i].type == "text") {
                             if (i >= 204 && i <= 215) {

                                 if (inputs[i].value > 1) {
                                     alert('value cannot be greater then one');
                                     inputs[i].value = 0;

                                 }
                                 else if (inputs[i].value == 1 && inputs[i].value != 0) {
                                     uy = uy + 1;
                                     if (uy > 4) {
                                         alert('max four  value');
                                         inputs[i].value = 0;
                                     }
                                 }

                             }
                         }
                     }
                 }

             }
             if (row == 19) {

                 for (var col = 2; col < Grid_Table.rows[row].cells.length; col++) {
                     var uy = 0;
                     for (var i = 0; i < inputs.length; i++) {

                         if (inputs[i].type == "text") {

                             if (i >= 219 && i <= 221) {
                                 debugger;
                                 if (inputs[219].value == 0) {
                                     if (i >= 291 && i <= 299) {
                                         $(inputs[i]).attr("disabled", "disabled");


                                     }
                                 }

                                 if (inputs[i].value > 1) {

                                     alert('value cannot be greater then one');
                                     inputs[i].value = 0;

                                 }
                                 else if (inputs[i].value == 1 && inputs[i].value != 0) {

                                     uy = uy + 1;
                                     if (uy > 1) {
                                      
                                         alert('max one  value');
                                         inputs[i].value = 0;

                                     }
                                     if (uy == 1) {

                                         LearningLevel(inputs[219].value, inputs[220].value, inputs[221].value);

                                     }
                                 }
                                 else if (inputs[219].value == 0 && inputs[220].value == 0 && inputs[221].value == 0) {
                                     inputs[251].value = 0;
                                     inputs[235].value = 0;
                                     inputs[236].value = 0;
                                     inputs[237].value = 0;
                                 }

                             }
                         }
                     }
                 }
             }

             if (row == 22) {
                 for (var col = 2; col < Grid_Table.rows[row].cells.length; col++) {
                     var uy = 0;
                     for (var i = 0; i < inputs.length; i++) {
                         if (inputs[i].type == "text") {
                             if (i == 268 || i == 269 || i == 271 || i == 273) {
                                 if (inputs[i].value > 1) {
                                     alert('value cannot be greater then one');
                                     inputs[i].value = 0;
                                 }
                                 else if (inputs[i].value == 1 && inputs[i].value != 0) {
                                     uy = uy + 1;
                                     if (uy > 4) {
                                         alert('max four  value');
                                         inputs[i].value = 0;
                                     }
                                 }
                             }
                         }
                     }
                 }
             }
             if (row == 23) {
                 for (var col = 2; col < Grid_Table.rows[row].cells.length; col++) {
                     var uy = 0;
                     for (var i = 0; i < inputs.length; i++) {
                         if (inputs[i].type == "text") {
                             if (i == 267 || i == 268 || i == 269) {
                                 if (inputs[i].value > 1) {
                                     alert('value cannot be greater then one');
                                     inputs[i].value = 0;
                                 }
                                 else if (inputs[i].value == 1) {

                                     uy = uy + 1;
                                     if (uy > 1) {
                                         alert('Only 1 entry should be allowed in any month');
                                         inputs[i].value = 0;
                                     }
                                     if (uy == 1) {

                                         Balsabha(inputs[267].value, inputs[268].value, inputs[269].value)
                                     }
                                 }
                             }
                         }
                     }
                 }

             }
             if (row == 26) {
                 for (var col = 2; col < Grid_Table.rows[row].cells.length; col++) {
                     var uy = 0;
                     for (var i = 0; i < inputs.length; i++) {
                         if (inputs[i].type == "text") {
                             if (i >= 300) {
                                 if (inputs[i].value > 1) {
                                     alert('value cannot be greater then one');
                                     inputs[i].value = 0;
                                 }
                                 else if (inputs[i].value == 1) {
                                     uy = uy + 1;
                                     if (uy > 5) {
                                         alert('Only Five entry should be allowed in any month');
                                         inputs[i].value = 0;
                                     }

                                 }
                             }
                         }
                     }
                 }
             }
         }
         for (var row = 0; row < Grid_Table.rows.length; row++) {
             if (row == 2) {
                 for (var col = 2; col < Grid_Table.rows[row].cells.length; col++) {
                     for (var i = 0; i < inputs.length; i++) {
                         if (inputs[i].type == "text") {
                             if (inputs[0].value == 1) {
                                 if (i >= 12 && i <= 23) {
                                     inputs[i].value = parseFloat(vv) * 2;
                                 }
                             }
                             else if (inputs[0].value != 1) {
                                 if (i >= 12 && i <= 23) {
                                     inputs[i].value = 0 * 2;


                                 }
                                 inputs[0].value = 0;

                             }
                         }
                     }
                 }
             }
         }
         if (msg != "") {
             alert(msg);
         }
         return true;
     }
     function Balsabha(TxtJu, TxtAu, TxtSe) {
         var Grid_Table = document.getElementById('<%= GV_AnnualPlan.ClientID %>');
         var inputs = Grid_Table.getElementsByTagName("input");
         for (var row = 0; row < Grid_Table.rows.length; row++) {
             if (row == 25) {
                 for (var col = 2; col < Grid_Table.rows[row].cells.length; col++) {
                     var uy = 0;
                     for (var i = 0; i < inputs.length; i++) {
                         if (inputs[i].type == "text") {
                             if (i >= 291 && i <= 299) {
                                 if (TxtJu > 0 && TxtJu == 1) {

                                     if (i >= 291 && i <= 299) {
                                         $(inputs[i]).removeAttr("disabled", true);
                                     }

                                 }
                                 else if (TxtAu > 0 && TxtAu == 1) {
                                     if (i >= 292 && i <= 299) {
                                         $(inputs[291]).attr("disabled", "disabled");
                                         $(inputs[i]).removeAttr("disabled", true);
                                     }
                                 } else if (TxtSe > 0 && TxtSe == 1) {
                                     if (i >= 293 && i <= 299) {
                                         $(inputs[292]).attr("disabled", "disabled");
                                         $(inputs[291]).attr("disabled", "disabled");
                                        
                                         $(inputs[i]).removeAttr("disabled", true);
                                     }
                                 }
                                 if (inputs[i].value > 1) {
                                     alert('value cannot be greater then one');
                                     inputs[i].value = 0;
                                 }
                                 else if (inputs[i].value == 1) {
                                     uy = uy + 1;
                                     if (uy > 5) {
                                         alert('Only five entry should be allowed in any month');
                                         inputs[i].value = 0;
                                     }

                                 }
                             }
                         }
                     }
                 }
             }
         }
     }
     function LearningLevel(TxtJu, TxtAu, TxtSe) {

         var Grid_Table = document.getElementById('<%= GV_AnnualPlan.ClientID %>');
         var inputs = Grid_Table.getElementsByTagName("input");
         for (var row = 0; row < Grid_Table.rows.length; row++) {
             if (row == 20) {
                 for (var col = 2; col < Grid_Table.rows[row].cells.length; col++) {
                     for (var i = 0; i < inputs.length; i++) {
                         if (inputs[i].type == "text") {
                             if (i == 235 || i == 236 || i == 237) {
                                 inputs[235].value = TxtJu;
                                 inputs[236].value = TxtAu;
                                 inputs[237].value = TxtSe;
                             }
                         }
                     }
                 }
             }
             if (row == 21) {
                 for (var col = 2; col < Grid_Table.rows[row].cells.length; col++) {
                     for (var i = 0; i < inputs.length; i++) {
                         if (inputs[i].type == "text") {

                             if (i == 251) {


                                 if (TxtJu > 0) {
                                     inputs[251].value = TxtJu;
                                 }
                                 else if (TxtAu > 0) {
                                     inputs[251].value = TxtAu;
                                 }
                                 else if (TxtSe > 0) {
                                     inputs[251].value = TxtSe;
                                 }


                             }

                         }
                     }
                 }
             }
             if (row == 24) {
                 for (var col = 2; col < Grid_Table.rows[row].cells.length; col++) {
                     for (var i = 0; i < inputs.length; i++) {
                         if (inputs[i].type == "text") {
                             if (i >= 279 && i <= 288) {

                                 if (TxtJu == 1) {
                                     if (i >= 279 && i <= 287) {
                                         $(inputs[i]).removeAttr("disabled", true);
                                     }

                                 }
                                 else if (TxtAu == 1) {
                                     if (i >= 280 && i <= 287) {
                                         $(inputs[279]).attr("disabled", "disabled");
                                         $(inputs[i]).removeAttr("disabled", true);
                                     }

                                 }
                                 else if (TxtSe == 1) {
                                     if (i >= 280 && i <= 287) {
                                        
                                         $(inputs[279]).attr("disabled", "disabled");
                                         $(inputs[280]).attr("disabled", "disabled");
                                         $(inputs[i]).removeAttr("disabled", true);
                                     }


                                 }


                             }
                         }
                     }
                 }
             }

         }

     }
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="mkiiainpnl121">
        <ContentTemplate>
            <div class="container-fluid">
            </div>
            <div class="container-fluid" style="margin-top: 0px;">
                <div class="row" >
                    <div class="col-lg-2 col-md-2 col-sm-3" style="padding-right: 0px;">
                        <div class="thumbnail" style="min-height: 750px; width: 228px;">
                            <div style="padding-top: 3px;">
                                <%--<span style="float:left"> <asp:Label ID="lblsearch" runat="server" Text="Search:" ForeColor="Black"></asp:Label></span>--%>
                                <span style="float: right; padding-right: 1px;">
                                    <asp:TextBox ID="txtSearchName" Visible="false" runat="server" OnTextChanged="txtSearchName_Click"
                                        AutoPostBack="true" CssClass="form-control col-lg-1"></asp:TextBox></span>
                            </div>
                            <div style="overflow: auto; margin-top: 35px; height: 750px;">
                                <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="40"
                                    BorderStyle="None" DataKeyNames="VillageCode,DISECode,RowNo,SchooLevel" GridLines="None"
                                    AutoGenerateColumns="false" OnRowCommand="GVMain_OnRowCommand" OnPageIndexChanging="GV_Project_PageIndexChanging">
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
                                        <asp:ButtonField HeaderText="Village Name " ItemStyle-ForeColor="#333" DataTextField="VillageName"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="School Name" ItemStyle-ForeColor="#333" DataTextField="SchoolName"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
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
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <h3 class="text-danger" style="margin: 0px;">
                                                    Annual Plan</h3>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">
                                                <input type="image" id="ton-new" class="butt" src="Images/search-not-29.png" title="Search" />
                                                <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right"
                                                    ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px;
                                                    padding: 0px;" runat="server" />
                                                <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnsave_Click" ValidationGroup="saves"
                                                    Style="margin-right: 5px; padding: 0px;" runat="server" />
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
                                                                    <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                                        class="form-control ">
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
                                                    <div class="row">
                                                        <div class="thumbnail" style="float: left; width: 100%;">
                                                            <asp:GridView ID="GV_AnnualPlan" Width="100%" ShowFooter="true" runat="server" BorderStyle="None"
                                                                GridLines="None" AutoGenerateColumns="false">
                                                                <EmptyDataTemplate>
                                                                </EmptyDataTemplate>
                                                                <FooterStyle CssClass="FooterStyle" />
                                                                <HeaderStyle BackColor="#f5f5f5" ForeColor="Black" Height="25px" />
                                                                <RowStyle HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#fad669" ForeColor="WhiteSmoke" />
                                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblDesc" Text='<%#Bind("Description") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Look Up" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblLookUp" Text='<%#Bind("LookupCode") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--   <asp:ButtonField HeaderText="Description" ItemStyle-ForeColor="#333" DataTextField="Description">
                                                                        <ItemStyle CssClass="padding-lef" Height="30px" Width="14%" />
                                                                        <HeaderStyle CssClass="padding-lef" />
                                                                    </asp:ButtonField>--%>
                                                                    <%-- <asp:ButtonField HeaderText="Look Up" ItemStyle-ForeColor="#333" DataTextField="LookupCode">
                                                                        <ItemStyle CssClass="padding-lef" Height="30px" />
                                                                        <HeaderStyle CssClass="padding-lef" />
                                                                    </asp:ButtonField>--%>
                                                                    <asp:TemplateField HeaderText="Apr">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtApr" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent();" Text='<%#Bind("Apr") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="May">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtMay" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent();" Text='<%#Bind("May") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Jun">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtJun" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent();" Text='<%#Bind("Jun") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Jul">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtJul" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent();" Text='<%#Bind("Jul") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Aug">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtAug" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent();" Text='<%#Bind("Aug") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sep">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtSep" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent();" Text='<%#Bind("Sep") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Oct">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtOct" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent();" Text='<%#Bind("Oct") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nov">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtNov" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent();" Text='<%#Bind("Nov") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Dec">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtDec" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent();" Text='<%#Bind("Dec") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Jan">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtJan" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent();" Text='<%#Bind("Jan") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Feb">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtFeb" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent();" Text='<%#Bind("Feb") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Mar">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtMar" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent();" Text='<%#Bind("Mar") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <div class="row">
                                                    <div class="thumbnail" style="float: left; width: 100%;">
                                                        <div class="col-lg-4 col-md-4 col-xs-12 col-sm-6  col-lg-offset-4 col-md-offset-4 col-sm-offset-3 col-xs-offset-0  ">
                                                            <asp:ImageButton ID="btnSUmbit" Visible="false" ToolTip="Save" OnClick="btnSumbit_Click" ValidationGroup="saves"
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
