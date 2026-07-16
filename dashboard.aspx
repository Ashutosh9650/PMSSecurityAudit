<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" MasterPageFile="~/Site.master"
    Inherits="dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <%--<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/bootstrap.js"></script>
<script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
<script type="text/javascript" src="js/bootstrap-multiselect.js"></script>--%>
    <style type="text/css" rel="stylesheet">
        .modalBackground {
            background-color: Gray !important;
            filter: alpha(opacity=50) !important;
            opacity: 0.7 !important;
        }

        #balloon-container {
            height: 100%;
            box-sizing: border-box;
            display: flex;
            flex-wrap: wrap;
        }

        .balloon {
            height: 125px;
            width: 105px;
            border-radius: 75% 75% 70% 70%;
            position: relative;
        }

            .balloon:before {
                content: "";
                height: 75px;
                width: 1px;
                padding: 1px;
                background-color: #FDFD96;
                display: block;
                position: absolute;
                top: 125px;
                left: 0;
                right: 0;
                margin: auto;
            }

        .panel-title {
            font-weight: 600;
        }

        .balloon:after {
            content: "▲";
            text-align: center;
            display: block;
            position: absolute;
            color: inherit;
            top: 120px;
            left: 0;
            right: 0;
            margin: auto;
        }

        @keyframes float {
            from {
                transform: translateY(100vh);
                opacity: 1;
            }

            to {
                transform: translateY(-300vh);
                opacity: 0;
            }
        }

        .min-card {
            display: flex;
            padding: 15px;
            margin-bottom: 15px;
            min-height: 75px;
            align-items: center;
            justify-content: center;
            font-size: 14px;
            background: #b4b4b4;
            border-radius: 8px;
            border: 1px solid #fff;
            color: #fff !important;
            height: 95px;
        }

            .min-card a {
                text-align: center;
                color: #fff !important;
                font-weight: 700;
            }



        body {
            font-family: Verdana, sans-serif;
            margin: 0
        }


        .author {
            color: cornflowerblue;
        }



        .birthday-title {
            display: flex;
            justify-content: space-between;
            flex-direction: row;
            align-items: center;
        }
        .grid__2 {
    display: grid;
    width: 100%;
    grid-template-columns: 40% auto;
    gap: 15px;
    padding: 0px 15px;
}
    </style>
    <style>
        


        .birth-box h2 {
            font-size: 16px;
            margin-top: 0;
        }

        .birth-box p {
            font-weight: 100 !important;
        }

        .birth-box {
            text-align: center;
        }

        .birth-box-sld {
            height: 230px !important;
            display: flex;
            align-items: center;
            padding: 0 32px;
            background: #ffe6cc;
            width: 100%;
}


        a.left-carousel-control, a.right-carousel-control {
    position: absolute;
    left: -7px;
    background: #5db7a8;
    color: #fff !important;
    width: 30px;
    height: 30px;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 5px !important;
    font-size: 11px;
}

a.left-carousel-control {}

a.right-carousel-control {
    left: unset;
    right: -7px;
}

.carousel-inner {
}

.panel-body {}

    </style>
    <script type="text/javascript">
        function SetTarget() {
            window.document.forms[0].target = "_blank";
        }
    </script>


    <link rel="shortcut icon" href="#">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:UpdatePanel ID="MainPanel" runat="server">
        <ContentTemplate>
                <div id="balloon-container" >
                <div class="container-fluid" >
            <%--<div id="balloon-container" >
                <div class="container-fluid" style="width: 100% !important">--%>
                    <div class="row" style="display:none">

                        <asp:HiddenField ID="Hdncount" runat="server" />
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="panel panel-default" runat="server" id="div1">
                                <div class="panel-heading">
                                    <h3 class="text-danger" style="margin: 0px;">
                                        

                                        <span class="pull-right" style="font-size: 14px;">
                                            <asp:ImageButton ID="Button3" Text="Report" CssClass="pull-right" ToolTip="GSS MM And SMC" OnClick="btnReport_Click" Height="23px" Width="40px" ImageUrl="~/images/Excel-2-icon.png"
                                                runat="server" Visible="false" />
                                        </span>
                                    </h3>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div id="ColMain">

                        <div class="row">
                            <div style="width:90%;margin:auto;float: left;margin-left: 5%;">
                                <div class="grid__2">
                                    <div>
                                        <div class="panel panel-default" style="margin-bottom: 0px;height: 100%;">
                                    <div class="panel-heading" style="background: #5db7a8;padding: 5px 15px;">
                                        <div class="birthday-title">
                                            <h4 class="panel-title text-left">
                                                <asp:Label ID="LinkButton2" runat="server" Text="Team Balika Birthday" Style="color: #fff;"></asp:Label>
                                            </h4>
                                            <div style="display: flex;justify-content: space-between;align-items: center;gap: 10px;">
                                                <asp:Label ID="lblDate" ForeColor="Black" class="text-danger" Style="font-size: 15px;" runat="server"></asp:Label>
                                            
                                            <asp:ImageButton ID="BtnImgeye" runat="server" ImageUrl="~/images/Eye_icon.png" OnClick="birthAnni_Click1" Style="color: #fff; height: 20px;" />
                                                </div>
                                        </div>



                                    </div>
                                    <div class="panel-body" style="background: #ededed;height: calc(100% - 32px);">

                                        <div id="MainDiv" runat="server" style="width: 100%;display: flex;justify-content: center;align-items: center;height: 100%;">
                                            <%-- <asp:GridView ID="gvReport" Visible="false" runat="server" OnRowCreated="gvReport_RowCreated" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" Font-Names="Arial"
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
                                                    <asp:TemplateField HeaderText="State Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbtn" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="District Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCol_2" ForeColor="Black" Text='<%# Bind("DistrictName") %>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cluster Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblC88ol_6" Text='<%# Bind("BlockName")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FC Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCol_3" ForeColor="Black" Text='<%# Bind("CreateBy") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FC Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCol_4" ForeColor="Black" Text='<%# Bind("FCName") %>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="# Quality Enrolment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCol_5" Text='<%# Bind("Icount") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rank">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCol_6" Text='<%# Bind("CHam")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>--%>






                                            <div id="myCarousel" class="carousel slide birth-box-sld" data-ride="carousel">
                                                         <!-- Indicators -->
                                              <%--  <ol class="carousel-indicators">
                                                    <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                                                    <li data-target="#myCarousel" data-slide-to="1"></li>
                                                    <li data-target="#myCarousel" data-slide-to="2"></li>
                                        
                                                </ol>--%>

                                                <!-- Wrapper for slides -->
                                                <div class="carousel-inner">
                                                    <div class="item active">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_0" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                    <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_1" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                    <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_2" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                    <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_3" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                    <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_4" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                    <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_5" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                    <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_6" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>



                                                     <div class="item">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_7" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                    <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_8" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                    <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_9" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                    <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_10" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                    <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_11" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                    <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_12" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                    <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_13" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                     <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_14" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                    <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_15" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                    <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_16" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                    <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_17" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                    <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_18" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                    <div class="item ">

                                                        <div class="birth-box">
                                                           
                                                            <p id="Id_19" style="font-weight: 600; color: cornflowerblue" runat="server"></p>
                                                        </div>
                                                    </div>

                                                </div>

                                                                                                <!-- Left and right controls -->
                                                <a class="left-carousel-control" href="#myCarousel" data-slide="prev">
                                                    <span class="glyphicon glyphicon-chevron-left"></span>
                                                    <span class="sr-only">Previous</span>
                                                </a>
                                                <a class="right-carousel-control" href="#myCarousel" data-slide="next">
                                                    <span class="glyphicon glyphicon-chevron-right"></span>
                                                    <span class="sr-only">Next</span>
                                                </a>
                                            </div>

                                        </div>


                                    </div>

                                </div>
                                    </div>
                                    <div>
                                        <div class="panel panel-default" style="height: auto; overflow: auto; width: 100%; background: #ededed;margin-bottom: 0px;">
                                    <div class="panel-heading" style="padding: 8px; background: #5db7a8; color: #fff;">
                                        <h4 class="panel-title text-center">
                                            <asp:Label ID="lblDashboard" runat="server" Text="Dashboard "></asp:Label>
                                        </h4>
                                    </div>

                                    <div class="panel-body">
                                        <div class="row card-rwo">
                                            <div class="col-md-3">
                                                <div class="min-card ">

                                                    <a href="urlOfThePage" id="lnkenrollment" runat="server" target="_blank">Enrolment</a>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="min-card ">
                                                    <a href="urlOfThePage" id="lnkPrimary" runat="server" target="_blank">Primary D2D</a>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="min-card    ">
                                                    <a href="urlOfThePage" id="lnkCBL" runat="server" target="_blank">State Check-in</a>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="min-card ">
                                                    <a href="f" id="lnkquality" runat="server" target="_blank">Quality Impact Dashboard</a>

                                                </div>
                                            </div>
                                            <div class="col-md-3" runat="server" visible="false">
                                                <div class="min-card ">
                                                    <a href="lnkqufity" id="lnkBalance" runat="server" target="_blank">Balance Scorecard</a>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="min-card ">
                                                    <a href="lnkqualifty" id="lnlCIOOSHG" runat="server" target="_blank">CIOOSG Survey</a>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="min-card ">
                                                    <a href="lnkquality" id="lnkTrainingDshboard" runat="server" target="_blank">Training Dashboard</a>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="min-card ">
                                                     <a href="lnkquality" id="A1" runat="server" target="_blank">VM Dashboard</a>
                                                  
                                                </div>
                                            </div>
                                             <div class="col-md-3">
                                                <div class="min-card ">
                                                     <a href="lnkquality" id="A2" runat="server" target="_blank">Maitri Cohort 5</a>
                                                  
                                                </div>
                                            </div>
                                             <div class="col-md-3">
                                                <div class="min-card ">
                                                     <a href="lnkquality" id="A3" runat="server" target="_blank"> Maitri UP Cohort 2</a>
                                                  
                                                </div>
                                            </div>
                                             <div class="col-md-3">
                                                <div class="min-card ">
                                                     <a href="lnkquality" id="A4" runat="server" target="_blank">Quality Monitoring Dashboard</a>
                                                  
                                                </div>
                                            </div>
                                             <div class="col-md-3">
                                                <div class="min-card ">
                                                     <a href="lnkquality" id="A5" runat="server" target="_blank">GKP Dashboard</a>
                                                  
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="min-card ">
                                                     <a href="lnkquality" id="A6" runat="server" target="_blank">LSE Dashboard</a>
                                                  
                                                </div>
                                            </div>
                                             <div class="col-md-3">
                                                <div class="min-card ">
                                                     <a href="lnkquality" id="A7" runat="server" target="_blank">D2D Contact Quality Alert</a>
                                                  
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                    <%--
                                <div id="Div2" runat="server" style="height: 475px; overflow: auto; width: 99%;">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" Font-Names="Arial"
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
                                            <asp:TemplateField HeaderText="Dashboard" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbtn" runat="server" Text='<%# Bind("Name") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Link">
                                                <ItemTemplate>

                                                    <asp:HyperLink ID="lblPanchayatName" ForeColor="Blue" runat="server" Text='<%# Eval("WebLink") %>'
                                                        Target="_blank" NavigateUrl='<%# Eval("WebLink") %>'></asp:HyperLink>

                                                </ItemTemplate>

                                            </asp:TemplateField>


                                        </Columns>
                                    </asp:GridView>
                                    </div>--%>
                                </div>
                                    </div>
                                </div>
                            </div>

                            


                        </div>
                    </div>
                </div>
            </div>

            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hdnBirthday"
                PopupControlID="Panel1" CancelControlID="btn_Close" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:HiddenField ID="hdnBirthday" runat="server" />
            <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="#66b746"
                BorderStyle="Ridge" BorderWidth="2px">
                <div class="modal-header" style="color: #000;">
                    <h4 class="modal-title text-center">
                        <asp:Label ID="Label2" runat="server" Style="font-size: 20px; font-weight: 500" Text="Team Balika Birthday List"></asp:Label>
                        <asp:ImageButton ID="btn_Close" CssClass="pull-right" runat="server" ImageUrl="~/Images/close-29.png"
                            ImageAlign="Right" />
                        <asp:ImageButton ID="ImgBirthday" Text="Report" CssClass="pull-right" ToolTip="Birthday wishes" OnClick="btnBirthDay_Report_Click" Height="29px" Width="40px" ImageUrl="~/images/ex1.png"
                            runat="server" Visible="false" />

                    </h4>
                </div>
                <div class="panel-body" style="height: 400px; overflow: scroll;">

                    <asp:GridView ID="GV_birhday" BorderStyle="Solid" runat="server" Font-Names="Arial" AutoGenerateColumns="false"
                        CssClass="table table-striped table-bordered table-hover table-responsive" Width="100%"
                        EmptyDataText="No Employee ">
                        <AlternatingRowStyle BackColor="#f1f1f1" />
                        <PagerStyle CssClass="dgvPageing" />
                        <HeaderStyle BackColor="#A7A2A4" ForeColor="White" />
                        <FooterStyle BackColor="Transparent" />
                        <Columns>
                            <asp:TemplateField HeaderText="Name" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lbtnName" runat="server" Text='<%# Bind("[TB Name]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BirthDay Date" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lbtnDate" runat="server" Text='<%# Bind("DOB") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="District Name" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lbtnDate" runat="server" Text='<%# Bind("[District Name]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Block Name" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lbtnDate" runat="server" Text='<%# Bind("[Block Name]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Village Name" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblvillagename" runat="server" Text='<%# Bind("[Village Name]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:TemplateField HeaderText="Link" Visible="true">
                                <ItemTemplate>
                                    <asp:ImageButton ID="Imgbtn" runat="server" CommandArgument='<%#Eval("EmployeeID")%>' CommandName="rowEdit" ImageUrl="~/images/Eye_icon.png" OnClick="birthAnni_Click1" />

                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>

                </div>
            </asp:Panel>

            <cc1:ModalPopupExtender ID="MpexdrDistrict" runat="server" BackgroundCssClass="modalBackground "
                CancelControlID="CancelButton" PopupControlID="PnlDistrict" TargetControlID="HdnFild">
            </cc1:ModalPopupExtender>
            <asp:HiddenField ID="HdnFild" runat="server"></asp:HiddenField>
            <asp:Panel CssClass="model-wid mod-posi" Style="display: none; height: auto; width: 42% !important; margin-top: 40.5px !important;"
                ID="PnlDistrict" runat="server">
                <div style="width: 100%; height: auto; background-color: #f1f1f1">

                    <div class="modal-body" style="padding: 1px;">
                        <asp:ImageButton ID="CancelButton" ImageUrl="~/images/close-29.png" Style="float: right;" Width="5%" Height="5%" runat="server" />
                        <asp:ImageButton ID="ImageButton1" Width="100%" Height="100%" ImageUrl="~/images/Greetings.jpg" runat="server" />
                    </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button3" />
            <asp:PostBackTrigger ControlID="ImgBirthday" />

        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

