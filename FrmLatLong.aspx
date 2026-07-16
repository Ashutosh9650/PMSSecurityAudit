<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FrmLatLong.aspx.cs" Inherits="FrmLatLong" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="script/geolocation.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDR0GgSSsXk011JN-ERbymQ2P4ec-ykp_E&sensor=true&libraries=places">
    </script>
    <style type="text/css">
        .map {
            height: 50%;
            width: 50%;
            position: absolute !important;
        }
        /* Optional: Makes the sample page fill the window. */
        html, body {
            height: 100%;
            margin: 0;
            padding: 0;
        }
    </style>
    <script type="text/javascript">
        function getLocation() {

            var places = new google.maps.places.Autocomplete(document.getElementById('pac-input'));
            google.maps.event.addListener(places, 'place_changed', function () {
                var place = places.getPlace();
                var address = place.formatted_address;
                var latitude = place.geometry.location.lat();
                var longitude = place.geometry.location.lng();
                var mesg = "[{\"lat\":" + latitude + ",";
                mesg += "\"lng\":" + longitude + "}]";
                Display(mesg, "", "");

            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="thumbnail" style="background-color: #f5f5f5; margin-bottom: 3px !important;">
                    <div class="panel-heading" style="padding: 0px 0px;">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <h3 class="text-danger" style="margin: 0px;">Village Mapping</h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-top: 5px;">
            <div class="thumbnail" style="background-color: #f5f5f5; float: left; width: 100%;">
                <div class="panel panel-default" style="margin-bottom:0px">
                    <div class="form-horizontal">
                        <div class="row">
                            <div style="padding: 0px 10px;">
                                <div class="row marg search-bg" style="padding: 10px 0px 0px 10px;">
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Year:</label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                            AutoPostBack="true" class="form-control ">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        State:</label>
                                                    <div class="col-sm-8 padd">
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
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                            AutoPostBack="true" class="form-control " />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Block:</label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                            class="form-control " />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div id="Divx1" class="col-lg-3 col-md-2 col-sm-3 cpl-xs-12" runat="server">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Panchayat:</label>
                                                    <div class="col-sm-8 padd">
                                                        <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"
                                                            class="form-control " />
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div1" class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12" runat="server">
                                                <div class="form-group">
                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                        Village:</label>
                                                    <div class="col-sm-8 padd">
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

                                                <asp:Button ID="BtnShow" runat="server" CssClass="btn btn-danger  btn-sm" Text="Display"
                                                    OnClick="BtnShow_OnClick" Style="margin-left: -10px;" />

                                                <input type="button" class="btn btn-danger btn-sm " value="Save" id="BtnSave" style="margin-left: 5px;" />

                                                <input type="button" class="btn btn-danger  btn-sm" value="Reset" id="btnMapReset" style="margin-left: 5px;" />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="container-fluid" style="margin-top: 0px;">
                                        <div class="row">
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="float: left; padding: 6px 0px 0px 0px;">
                                                <div class="thumbnail" style="min-height: 715px; width: 100%; margin-bottom: 10px;">
                                                    <div style="overflow: auto; height: 715px;">
                                                        <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="40"
                                                            OnRowCommand="GVMain_OnRowCommand" BorderStyle="None" DataKeyNames="VillageCode"
                                                            GridLines="None" AutoGenerateColumns="false">
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
                                                            <Columns>
                                                                <asp:ButtonField HeaderText="Code " ItemStyle-ForeColor="#333" DataTextField="EGVillageCode"
                                                                    CommandName="GVUIO">
                                                                    <ItemStyle CssClass="padding-lef" Height="30px" />
                                                                    <HeaderStyle CssClass="padding-lef" />
                                                                </asp:ButtonField>
                                                                <asp:ButtonField HeaderText="Name " ItemStyle-ForeColor="#333" DataTextField="VillageName"
                                                                    CommandName="GVUIO">
                                                                    <ItemStyle CssClass="padding-lef" Height="30px" />
                                                                    <HeaderStyle CssClass="padding-lef" />
                                                                </asp:ButtonField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 0px; padding-top: 6px;">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 0px 0px 9px 0px;">
                                                    <input id="pac-input" class="form-control" type="text" placeholder="Search Box" onblur="getLocation();" />
                                                </div>
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding: 5px 0px 9px 0px;">
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" style="padding: 0px;">
                                                        <asp:TextBox ID="Txtlat" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                        <asp:TextBox ID="TxtLong" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                                        <div class="col-lg-4">
                                                            <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-danger pull-left  btn-sm" Text="Lat Long Search"
                                                                OnClick="btnsearch_OnClick" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="thumbnail" style="min-height: 640px; overflow: auto; margin-bottom: 10px;">
                                                    <div id="mapcanv" class="map">
                                                    </div>
                                                    <%--<div id="popupbox" title="title">
                                <p>
                                    <div id="popupcontent">
                                    </div>
                                </p>
                            </div>--%>
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
        <asp:HiddenField ID="HdnLatLong" runat="server" />
    </div>
</asp:Content>
