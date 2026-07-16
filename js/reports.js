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
    $("#village_search").button().bind("click", Page.village_search);
    Page.BtnExcel = $("#btnExcel");
    Page.BtnExcel.bind("click", Page.Startexcel);
    $("#BtnHour").bind("click", Page.BtnHour);
    $("#BtnDaysWork").bind("click", Page.BtnDaysWork);
    $("#BtnException").bind("click", Page.BtnException);
    $("#BtnReason").bind("click", Page.BtnReason);
    $("#Btnvillages").bind("click", Page.Btnvillages);
    $("#BtnMap").bind("click", Page.BtnMap);
    $("#dd").bind("click", Page.dd);
    $("#village_hour").bind("click", Page.village_hour);
    $("#village_emp").bind("click", Page.village_emp);
    $("#village_exception").bind("click", Page.village_exception);
    Page.BtnDetails = $("#btnDetails");

    Page.BtnDetails.bind("click", Page.StartDetailsExcel)

    Page.isGridCreate = false;

    $('#mainSplitter').jqxSplitter({ width: "100%", height: 480, theme: "", panels: [{ size: 200 }, { size: 300 }] });

};

Page.village_exception = function () {

    tab = "exception";
    if (!Page.CheckDates()) {
        return;
    }
    var empID = $(".ddEmpList").val();

    $('#container').show();
    $('#map').hide();
    $('.ddvilglist').show();
    $('#village_search').show();
    $('#container').hide();
    $('#map').hide();


}




//location employee
Page.village_emp = function () {

    tab = "employees";
    if (!Page.CheckDates()) {
        return;
    }

   


    var empID = $(".ddEmpList").val();
    $('#container').show();
    $('#map').hide();
    $('.ddvilglist').show();
    $('#village_search').show();
    $('#container').hide();
    $('#map').hide();
    if (empID > 0) {
       return;
}
else{
        $('#container').show();
        $('#map').hide();
        $('.ddvilglist').show();
        $('#village_search').show();
        $('#container').hide();
        $('#map').hide();
  
}
}

Page.village_hour = function () {

    tab = "hour";
    if (!Page.CheckDates()) {
        return;
    }
    var e = $(".ddEmpList").val();
    var empID = $(".ddEmpList").val();
    console.log("e"+e);
    $('#container').show();
    $('#map').hide();
    $('.ddvilglist').show();
    $('#village_search').show();
    $('#container').hide();
    $('#map').hide();

}

// search village hour 
Page.village_search = function () {
    var fromDate = $("#txtFromDate").val();
    var toDate = $("#txtToDate").val();
    var empID = $(".ddEmpList").val();
  
    var Vil_id = $(".ddvilglist").val();
    var request = {};

    if (tab == "hour") {
        request.req = "village_hours";
        request.fromdate = fromDate;
        request.todate = toDate;
        request.empid = empID;
        request.Vil_id = Vil_id;
        console.log("village_id"+Vil_id);
        console.log("emp_id" + empID);
        Page.GetHRReport(request, Page.OnHRvillage_hoursRec);  //location hours

    }

    else if (tab == "employees") {

        request.req = "villgae_employees";
        request.fromdate = fromDate;
        request.todate = toDate;
        request.empid = empID;
        request.Vil_id = Vil_id;
        Page.GetHRReport(request, Page.OnHRvillage_empRec);  //location employees

    }
    else if (tab == "exception") {

        request.req = "village_exception";
        request.fromdate = fromDate;
        request.todate = toDate;
        request.empid = empID;
        request.Vil_id = Vil_id;
        Page.GetHRReport(request, Page.OnHRvillage_exceptionRec);  //location exception

    }


}


//Location Exception
Page.OnHRvillage_exceptionRec = function (e){
    
    var data = JSON.parse(e);

    console.log(data);
    var Exception = [];
    var Exception_count = [];
    var j;
    for (var i = 0; i < data.length; i++) {
        var indx = Exception.indexOf(data[i].empl);

        if (indx >= 0) {
            var h = (data[i].exception);
            Exception_count[indx] += h;

        }
        else {

            Exception.push(data[i].empl);
            var h = (data[i].exception);
            Exception_count.push(h);

        }
    //    Exception.push(data[i].empl);
     //   Exception_count.push(data[i].exception);

    }
    if (data.length > 8) {
        j = 7;
    }
    else {
        j = data.length - 1;
    }

    $("#grid_list").hide();
    $("#jqxWidget").show();

    $('#container').highcharts({
        chart: {
            type: 'column'
        },
        title: {
            text: 'Total Exception Count'
        },
        scrollbar: {
            enabled: true
        },
        subtitle: {
            text: 'Source: Educategirls.in'
        },
        xAxis: {
            categories: Exception,
            min: 0,
            max: j
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Village Exception Count'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: [
            {
                name: 'Total Exception Count',
                data: Exception_count

            }
        ]
    });
    $('.ddvilglist').show();
    $('#village_search').show();
}






//location employees
Page.OnHRvillage_empRec = function (e) {
    var data = JSON.parse(e);

    console.log(data);
    var vilg = [];
    var emp_count = [];
    var count = []
    for (var i = 0; i < data.length; i++) {
        var indx = vilg.indexOf(data[i].vilge);
       
        if (indx >= 0) {
            var h = (data[i].counts);
            emp_count[indx] += h;
           
        }
        else {
           
            vilg.push(data[i].vilge);
            var h = (data[i].counts); 
            emp_count.push(h);
   
        }
     
    }
    $("#grid_list").hide();
    $("#jqxWidget").show();

    $('#container').highcharts({
        chart: {
            type: 'column'
        },
        title: {
            text: 'Total Employee Visit Count'
        },
        scrollbar: {
            enabled: true
        },
        subtitle: {
            text: 'Source: Educategirls.in'
        },
        xAxis: {
            categories: vilg,
            min: 0,
            max: 7
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Village Visit Count'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: [
            {
                name: 'Total Count',
                data: emp_count

            }
        ]
    });
    $('.ddvilglist').show();
    $('#village_search').show();
}

//location hours
Page.OnHRvillage_hoursRec = function (e) {

    var data = JSON.parse(e);
    console.log(data);
    var vilg = [];
    var Hours = [];



    for (var i = 0; i < data.length; i++) {
        var indx = vilg.indexOf(data[i].vilge);
        if (indx >= 0) {
            var h = (data[i].hours);
            Hours[indx] += h;


        } else {
            vilg.push(data[i].vilge);
            var h = (data[i].hours);
            Hours.push(h);
        }

    }
    $("#grid_list").hide();
    $("#jqxWidget").show();

    $('#container').highcharts({
        chart: {
            type: 'column'
        },
        title: {
            text: 'Total Minute for Village'
        },
        scrollbar: {
            enabled: true
        },
        subtitle: {
            text: 'Source: Educategirls.in'
        },
        xAxis: {
            categories: vilg,
            min: 0,
            max: 7
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Minutes'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: [
            {
                name: 'Minutes',
                data: Hours.sort(function (a, b) { return b - a })

            }
        ]
    });
    $('.ddvilglist').show();
    $('#village_search').show();
}






//Map Graph
Page.BtnMap = function () {
    $('.ddvilglist').hide();
    $('#village_search').hide();
    $('#container').show();
    $('#map').hide();
    if (!Page.CheckDates()) {
        return;
    }

    var fromDate = $("#txtFromDate").val();
    var toDate = $("#txtToDate").val();
    var empID = $(".ddEmpList").val();


    if (empID == 0) {

        alert("please select single employee only...");
    }
    else {
        var request = {};
        request.req = "map_graph";
        request.fromdate = fromDate;
        request.todate = toDate;
        request.empid = empID;
        console.log(empID);
        Page.GetHRReport(request, Page.OnHREmpMapRec);  //Map
    }
}

Page.OnHREmpMapRec = function (e) {
    $('#container').hide();
    $('#map').show();
    var data = JSON.parse(e);
    var dataaa = [];
    var locations = [];
    var j = [];


    for (var i = 0; i < data.length; i++) {
        if (data[i].location != null) {
            //   console.log(data[i].village_name + " Rajesthan");
            dataaa.push(data[i].village_name + "," + " Rajasthan");
            var locations = (data[i].location);
            if (locations != null) {
                var lat = locations.split(",");

                dataaa.push(lat[0]);
                dataaa.push(lat[1]);
            }
            else { };
            console.log(dataaa);
            j.push(dataaa);
            console.log("map:" + j);
            var dataaa = [];
        }
    }
    // console.log(j);
    var locations = j;

    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 60,
        center: new google.maps.LatLng(26.5727, 73.8390),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    var infowindow = new google.maps.InfoWindow();

    var marker, i;
    var markers = new Array();

    for (i = 0; i < locations.length; i++) {
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(locations[i][1], locations[i][2]),
            map: map
        });

        markers.push(marker);

        google.maps.event.addListener(marker, 'click', (function (marker, i) {
            return function () {
                infowindow.setContent(locations[i][0]);
                infowindow.open(map, marker);
            }
        })(marker, i));
    }

    function AutoCenter() {
        //  Create a new viewpoint bound
        var bounds = new google.maps.LatLngBounds();
        //  Go through each...
        $.each(markers, function (index, marker) {
            bounds.extend(marker.position);
        });

        map.fitBounds(bounds);
        var listener = google.maps.event.addListener(map, "idle", function () {
            map.setZoom(8);
            google.maps.event.removeListener(listener);
        });
    }

    AutoCenter();
}


//Villages
Page.Btnvillages = function () {
    $('.ddvilglist').hide();
    $('#village_search').hide();
    $('#map').hide();
    $('#container').show();

    if (!Page.CheckDates()) {
        return;
    }

    var fromDate = $("#txtFromDate").val();
    var toDate = $("#txtToDate").val();
    var empID = $(".ddEmpList").val();


    if (empID == 0) {

        alert("please select single employee only...");
    }
    else {
        var request = {};
        request.req = "village_graph";
        request.fromdate = fromDate;
        request.todate = toDate;
        request.empid = empID;
        console.log(empID);
        Page.GetHRReport(request, Page.OnHREmpVillageRec);  //villages
    }
}

Page.OnHREmpVillageRec = function (e) {

    var data = JSON.parse(e);
    console.log(data);
    console.log("villages");
    var village_name = [];
    var villages_count = [];
    var j;
    for (var i = 0; i < data.length; i++) {

        village_name.push(data[i].village_name);
        villages_count.push(data[i].total_visit);

    }
    if (data.length > 8) {
        j = 7;
    }
    else {
        j = data.length - 1;
    }
    $("#grid_list").hide();
    $("#jqxWidget").show();

    $('#container').highcharts({
        chart: {
            type: 'column'
        },
        title: {
            text: 'Total Number of Village Visit'
        },
        scrollbar: {
            enabled: true
        },
        subtitle: {
            text: 'Source: Educategirls.in'
        },
        xAxis: {
            categories: village_name,
            min: 0,
            max: j
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Total Number of Visit'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: [
            {
                name: 'Number of Visit',
                data: villages_count

            }
        ]
    });

}


//Exception
Page.BtnException = function () {
    $('.ddvilglist').hide();
    $('#village_search').hide();
    $('#container').show();
    $('#map').hide();
    if (!Page.CheckDates()) {
        return;
    }
    var fromDate = $("#txtFromDate").val();
    var toDate = $("#txtToDate").val();
    var empID = $(".ddEmpList").val();

    var request = {};
    request.req = "Exception";
    request.fromdate = fromDate;
    request.todate = toDate;
    request.empid = empID;
    console.log(empID);
    Page.GetHRReport(request, Page.OnHREmpExceptionRec);  //exception

}

Page.OnHREmpExceptionRec = function (e) {

    var data = JSON.parse(e);
    console.log(data);
    if (data == "fail") {
        alert("fail");
    }
    console.log(data);
    console.log("entered exception");
    var Categories = [];
    var exception = [];

    for (var i = 0; i < data.length; i++) {
        Categories.push(data[i].empl);
        exception.push(data[i].exception);

    }

    $("#grid_list").hide();
    $("#jqxWidget").show();

    $('#container').highcharts({
        chart: {
            type: 'column'
        },
        title: {
            text: 'Total Number of Exceptions'
        },
        scrollbar: {
            enabled: true
        },
        subtitle: {
            text: 'Source: Educategirls.in'
        },
        xAxis: {
            categories: Categories,
            min: 0,
            max: 4
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Number Exceptions'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: [
            {
                name: 'Exception',
                data: exception

            }
        ]
    });

}

//Days Worked
Page.BtnDaysWork = function () {
    $('.ddvilglist').hide();
    $('#village_search').hide();
    $('#container').show();
    $('#map').hide();
    if (!Page.CheckDates()) {
        return;
    }

    var fromDate = $("#txtFromDate").val();
    var toDate = $("#txtToDate").val();
    var empID = $(".ddEmpList").val();

    var request = {};
    request.req = "emp_worked_days";
    request.fromdate = fromDate;
    request.todate = toDate;
    request.empid = empID;
    console.log(empID);
    Page.GetHRReport(request, Page.OnHREmpDayDataRec1);
}

Page.OnHREmpDayDataRec1 = function (e) {
    var data = JSON.parse(e);
    console.log(data);
    var Categories = [];
    var Days = [];
    for (var i = 0; i < data.length; i++) {
        Categories.push(data[i].empl);
        Days.push(data[i].Days);
        
    }
    console.log(Categories + Days);
    $("#grid_list").hide();
    $("#jqxWidget").show();

    $('#container').highcharts({
        chart: {
            type: 'column'
        },
        title: {
            text: 'Employee Days Worked'
        },
        scrollbar: {
            enabled: true
        },
        subtitle: {
            text: 'Source: Educategirls.in'
        },
        xAxis: {
            categories: Categories,
            min: 0,
            max: 7
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Days'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: [
            {
                name: 'Days',
                data: Days

            },

        ]
    });

}
Page.CheckDates = function () {
    var fromDate = $("#txtFromDate").val();
    var toDate = $("#txtToDate").val();
    var empID = $(".ddEmpList").val();
    // alert(fromDate)
    if (fromDate == "" || toDate == "") {
        alert("Date required");
        return false;
    }
    return true;
};

//hour
Page.GetHRReport = function (request, callback) {
    $('#container').show();
    $('.ddvilglist').hide();
    $('#village_search').hide();
    $('#map').hide();
    var clbk = callback;
    $.ajax({
        url: "hrreport.ashx",
        type: "POST",
        data: request,
        success: function (a, b, c) {
            clbk(a);
        }
    });
};
Page.BtnHour = function () {
    if (!Page.CheckDates()) {
        return;
    }

    var fromDate = $("#txtFromDate").val();
    var toDate = $("#txtToDate").val();
    var empID = $(".ddEmpList").val();

    var request = {};
    request.req = "emp_hour";
    request.fromdate = fromDate;
    request.todate = toDate;
    request.empid = empID;
    console.log(empID);
    Page.GetHRReport(request, Page.OnHREmpHourDataRec);
}
Page.OnHREmpHourDataRec = function (e) {
    var data = JSON.parse(e);

    var Categories = [];
    var Hours = [];


    for (var i = 0; i < data.length; i++) {
        var indx = Categories.indexOf(data[i].empl);

        Categories.push(data[i].empl);
        var h = (data[i].hours);
        Hours.push(h);
         console.log(data[i].hours)


    }

    $("#grid_list").hide();
    $("#jqxWidget").show();

    $('#container').highcharts({
        chart: {
            type: 'column'
        },
        scrollbar: {
            enabled: true
        },
        title: {
            text: 'Employee Working Hours'
        },
        subtitle: {
            text: 'Source: educategirls.in'
        },
        xAxis: {
            categories: Categories,
            min: 0,
            max: 7
        },

        yAxis: {
            min: 0,
            title: {
                text: 'Minutes'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.2f}</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },

        series: [{
            name: 'Minutes',
            data: Hours

        }]

    }

    );

}

Page.Startexcel = function () {
    var fromDate = $("#txtFromDate").val();
    var toDate = $("#txtToDate").val();
    var empID = $(".ddEmpList").val();
    var searchDistrict = $(".ddRDistrict").val();
    

    if (fromDate == "") {
        alert("Invalid from date");
        return;
    }
    if (toDate == "") {
        alert("Invalid to date");
        return;
    }
    window.open("reports.ashx?req=excel&fromdate=" + fromDate + "&todate=" + toDate + "&empid=" + empID + "&searchDistrict=" + searchDistrict);
}

Page.StartDetailsExcel = function () {
    var empID = $(".ddEmpList").val();
    window.open("reports.ashx?req=reportExcel&empid=" + empID);
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
    var searchDistrict = $(".ddRDistrict").val();
  //  alert(searchDistrict);
    if (fromDate == "") {
        alert("Invalid from date");
        return;
    }
    if (toDate == "") {
        alert("Invalid to date");
        return;
    }
    Page.ReDrawTable(fromDate, toDate, empID, searchDistrict);
    return;

    $.ajax({
        url: "reports.ashx",
        type: "POST",
        data: { req: "search", fromdate: fromDate, todate: toDate, empid: empID, searchDistrict: searchDistrict },
        success: function (a, b, c) {
            console.log(a);
            var msg = a.split("|~|");
            Page.ReDrawTable(msg[0]);
            Page.DrawPie(Number(msg[1]), Number(msg[2]));
        }
    });
}
Page.ReDrawTable = function (fromDate, toDate, empID, searchDistrict) {
    //reslist
    if (Page.isGridCreate) {
        $("#list1").jqGrid('setGridParam', {
            url: "reports.ashx?req=search&fromdate=" + fromDate + "&todate=" + toDate + "&empid=" + empID + "&searchDistrict=" + searchDistrict,
            page: 1
        }).trigger("reloadGrid");
    } else {
        $("#list1").jqGrid({
            url: "reports.ashx?req=search&fromdate=" + fromDate + "&todate=" + toDate + "&empid=" + empID + "&searchDistrict=" + searchDistrict,
            datatype: "xml",
            colNames: ["#", "Date", "Employee", "District", "Block", "Grampanchayat", "Village", "Start Time", "Start Time Entry Location", "Reason", "End Time", "End Time Entry Location", "Reason", "Hours"],
            colModel: [
            { name: 'id', index: 'id', width: 40 },
            { name: 'date', index: 'date', width: 75 },
            { name: 'employee', index: 'employee', width: 100 },
             { name: 'District', index: 'District', width: 100 },
            { name: 'block', index: 'block', width: 100 },
            { name: 'grampanchayat', index: 'grampanchayat', width: 100 },
            { name: 'village', index: 'village', width: 100 },
            { name: 'startdate', index: 'startdate', width: 75 },
            { name: 'startlocation', index: 'startlocation', width: 130 },
            { name: 'startdateReason', index: 'startdateReason', width: 85 },
            { name: 'endtime', index: 'endtime', width: 75 },
            { name: 'endlocation', index: 'endlocation', width: 130 },
            { name: 'endtimeReason', index: 'endtimeReason', width: 85 },
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
   
    $("#mapcanv").html("");
    $("#MainDiv").show();
    $("#mapviewer").show();
    $("#mapcanv").show();


//    $(function () {
//        $("#mapviewer").dialog({
//            resizable: false,
//            height: 400,
//            width: 430,
//            modal: true
//        });
//    });

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

    if (villageDotArray != "") {

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
    $('#mainSplitter').jqxSplitter({ width: 1005, height: 480, theme: theme, panels: [{ size: 200 }, { size: 300 }] });

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