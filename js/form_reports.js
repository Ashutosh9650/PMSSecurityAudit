/* File Created: September 24, 2012 */
var Page = {};
Page.PiePlot = null;
Page.isGridCreate = false;
Page.BtnExcel = null;
Page.BtnDetails = null;
var tab;
Page.Init = function () {

    $("#txtFromDate").datepicker({ dateFormat: "dd/mm/yy" });
    $("#txtToDate").datepicker({ dateFormat: "dd/mm/yy" });
    $("#btnSearch").button().bind("click", Page.StartSearch);
    $("#btnFormSearch").button().bind("click", Page.StartFormSearch);
    Page.BtnExcel = $("#btnExcel");
    Page.BtnExcel.bind("click", Page.Startexcel);
    Page.BtnDetails = $("#btnDetails");

    Page.BtnDetails.bind("click", Page.StartDetailsExcel)

    Page.isGridCreate = false;


};






Page.Startexcel = function () {
    var fromDate = $("#txtFromDate").val();
    var toDate = $("#txtToDate").val();
    var empID = $(".ddEmpList").val();
    var FormName = $(".ddFormType").val();

    if (fromDate == "") {
        alert("Invalid from date");
        return;
    }
    if (toDate == "") {
        alert("Invalid to date");
        return;
    }
    window.open("formreports.ashx?req=excel&fromdate=" + fromDate + "&FormName=" + FormName + "&todate=" + toDate + "&empid=" + empID);
}

Page.StartDetailsExcel = function () {
    var empID = $(".ddEmpList").val();
    window.open("formreports.ashx?req=reportExcel&empid=" + empID);
}

Page.StartSearch = function () {
    $("#jqxWidget").hide();
    $("#grid_list").show();

    if (Page.PiePlot != null) {
        Page.PiePlot.destroy();
    }
    var fromDate = $("#txtFromDate").val();
    var toDate = $("#txtToDate").val();
    var empID = $(".ddEmpList").val();
    var FormName = $(".ddFormType").val();
   
    if (fromDate == "") {
        alert("Invalid from date");
        return;
    }
    if (toDate == "") {
        alert("Invalid to date");
        return;
    }
    Page.ReDrawTable(fromDate, toDate, empID, FormName);
    return;

    $.ajax({
        url: "formreports.ashx",
        type: "POST",
        data: { req: "search", fromdate: fromDate, todate: toDate, empid: empID, FormName: FormName },
        success: function (a, b, c) {
            console.log(a);
            var msg = a.split("|~|");
            Page.ReDrawTable(msg[0]);
            Page.DrawPie(Number(msg[1]), Number(msg[2]));
        }
    });
}
Page.ReDrawTable = function (fromDate, toDate, empID, FormName) {
    //reslist
    if (Page.isGridCreate) {
        $("#list1").jqGrid('setGridParam', {
            url: "formreports.ashx?req=search&fromdate=" + fromDate + "&FormName=" + FormName + "&todate=" + toDate + "&empid=" + empID,
            page: 1
        }).trigger("reloadGrid");
    } else {
        $("#list1").jqGrid({
            url: "formreports.ashx?req=search&fromdate=" + fromDate + "&todate=" + toDate + "&FormName=" + FormName + "&empid=" + empID,
            datatype: "xml",
            colNames: ["#", "Date", "Employee", "Emp Type", "Block", "Grampanchayat",
                "Village", "School", "Start Time", "End Time", " Form Name", "Question", "Answer", "Hours"],
            colModel: [
            { name: 'id', index: 'id', width: 40 },
            { name: 'date', index: 'date', width: 75 },
            { name: 'employee', index: 'employee', width: 100 },
             { name: 'Emp_type', index: 'Emp_type', width: 100 },
            { name: 'block', index: 'block', width: 100 },
            { name: 'grampanchayat', index: 'grampanchayat', width: 100 },
            { name: 'village', index: 'village', width: 75 },
            { name: 'school', index: 'school', width: 100 },
            { name: 'startdate', index: 'startdate', width: 75 },
            { name: 'Form', index: 'Form', width: 75 },
            { name: 'Question', index: 'Question', width: 100 },
             { name: 'Answer', index: 'Answer', width: 180 },
              { name: 'endtime', index: 'endtime', width: 70 },
            { name: 'hours', index: 'hours', width: 75 }
            ],
            rowNum: 10,
            autowidth: false,
            height: 250,
            rowList: [10, 20, 30],
            pager: $('#pager1'),
            sortname: "id",
            viewrecords: true,
            sortorder: "asc",
            caption: "Report"
        }).navGrid('#pager1', { edit: false, add: false, del: false });
        Page.isGridCreate = true;
    }
}
Page.DrawPie = function (working, absent) {
    if (Page.PiePlot != null) {
        Page.PiePlot.destroy();
    }

    Page.PiePlot = $.jqplot('piegraph', [[['working', working], ['non working', absent]]], {
        gridPadding: { top: 0, bottom: 38, left: 0, right: 0 },
        seriesDefaults: {
            renderer: $.jqplot.PieRenderer,
            trendline: { show: false },
            rendererOptions: { padding: 8, showDataLabels: true }
        },
        legend: {
            show: true,
            placement: 'outside',
            rendererOptions: {
                numberRows: 1
            },
            location: 's',
            marginTop: '15px'

        }
    });
}
Page.Map = {};
Page.Map.Zoom = 14;
Page.Map.GeoCoder = null;
Page.Map.MapObj = null;
Page.Replace = function (mainstr, strtoreplace, replacewith) {
    do {
        mainstr = mainstr.replace(strtoreplace, replacewith);
    } while (mainstr.indexOf(strtoreplace) !== -1);
    return mainstr;
};
Page.ShowLocation = function (locData, villageDotArray) {
    debugger;
    $("#mapcanv").html("");
    $("#mapcanv").show();
    $(function () {
        $("#mapviewer").dialog({
            resizable: false,
            height: 400,
            width: 430,
            modal: true
        });
    });

    var PolyArray = new Array();
    var latlngStr = locData.split(',', 2);
    var lat = parseFloat(latlngStr[0]);
    var lng = parseFloat(latlngStr[1]);
    var latlng = new google.maps.LatLng(lat, lng);
    var mapOptions = {
        zoom: 8,
        center: latlng,
        mapTypeId: 'roadmap'
    }
    Page.Map.MapObj = new google.maps.Map(document.getElementById('mapcanv'), mapOptions);
    Page.Map.GeoCoder = new google.maps.Geocoder();

    if (villageDotArray && villageDotArray != "") {

        var aData;
        if (typeof villageDotArray === "string") {
            aData = JSON.parse(villageDotArray);
        } else {
            aData = villageDotArray;
        }

        for (var k = 0; k < aData.length; k++) {
            var mData = aData[k];
            var nlatlang;
            if ('jb' in mData) {
                nlatlang = new google.maps.LatLng(mData.jb, mData.kb);
            } else if ('mb' in mData) {
                nlatlang = new google.maps.LatLng(mData.lb, mData.mb);
            } else {
                nlatlang = new google.maps.LatLng(mData.lat, mData.lng);
            };
            PolyArray.push(nlatlang);
        }
        var Poly = new google.maps.Polygon({
            paths: PolyArray,
            strokeColor: '#FF0000',
            strokeOpacity: 0.8,
            strokeWeight: 3,
            fillColor: '#FF0000',
            fillOpacity: 0.35
        });

        Poly.setMap(Page.Map.MapObj);
    }


    Page.Map.GeoCoder.geocode({ 'latLng': latlng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            if (results[1]) {

                Page.Map.MapObj.setZoom(14);
                marker = new google.maps.Marker({
                    position: latlng,
                    map: Page.Map.MapObj
                });
                //infowindow.setContent(results[1].formatted_address);
                //infowindow.open(map, marker);
                //console.log(results[1]);
                var addArray = results[1].address_components;
                var addString = "";
                for (var i = 0; i < addArray.length; i++) {
                    var compObj = addArray[i];
                    //console.log(compObj);
                    if (addString == "") {
                        addString = compObj.long_name;
                    } else {
                        addString = addString + ", " + compObj.long_name;
                    }
                }
                //console.log(addArray);
                $("#mapaddress").html(addString);
            } else {
                alert('No results found');
            }
        } else {
            alert('Geocoder failed due to: ' + status);
        }
    });
}
$(function () {
    var theme = "";

    Page.Init();

});
//grap div tag dragging
var isResizing = false,
 lastDownX = 0;

$(function () {
    var container = $('#container'),
        left = $('#left'),
        right = $('#right'),
        handle = $('#handle');

    handle.on('mousedown', function (e) {
        isResizing = true;
        lastDownX = e.clientX;

    });

    $(document).on('mousemove', function (e) {
        if (!isResizing)
            return;

        var offsetRight = container.width() - (e.clientX - container.offset().left);

        if (offsetRight > 100 && offsetRight < 900) {
            left.css('right', offsetRight);
            right.css('width', offsetRight);
        }

    }).on('mouseup', function (e) {

        isResizing = false;
    });
});

//width resize closed