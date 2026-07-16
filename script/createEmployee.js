/* File Created: September 24, 2012 */
var Page = {};
Page.SearchDiag = false;
Page.EmptypeID = null;
Page.BtnDetails = null;
Page.Init = function () {
    $(".button").button();
    $(".button").button();
     $(".field").hide();

    $("#dialog-form").dialog({ autoOpen: false, modal: true });
    $("form").append($(".ui-dialog"));
    if (Page.SearchDiag == true) {
        $("#dialog-form").dialog('open');
    } else {
        $("#dialog-form").dialog('close');
    }

    var tempBlock = "";

    $("p").click(function (){
        tempBlock = $(this).text();
    }); 
    $("#addBtn").click(function (){
        $(".blockAssaigned").append("<p style='padding:1px 6px; margin:0; cursor:pointer;'>" + tempBlock + "</p>");
    });

    Page.BtnDetails = $("#btnDetails");
    Page.btnMobileDetails = $("#btnMobileDetails");
    Page.BtnDetails.bind("click", Page.StartDetailsExcel)
    Page.btnMobileDetails.bind("click", Page.StartMobileDetailsExcel)
   
};
Page.StartDetailsExcel = function () {
    //var empID = $(".ddEmpList").val();
    window.open("Reports/reports.ashx?req=reportExcel&empid=0");
}
Page.StartMobileDetailsExcel = function () {
    //var empID = $(".ddEmpList").val();
    window.open("Reports/reports.ashx?req=MobilereportExcel&empid=0");
}
Page.CallBack = function (args, context) {

};
Page.ShowSearxchDiag = function () {
    //alert("here");
    Page.SearchDiag = true;
};
Page.GenerateHindi = function () {
    var url = "https://translate.google.com/translate_a/t?client=t&text=hello world&hl=en&sl=en&tl=hi&ie=UTF-8&oe=UTF-8&multires=1&otf=1&ssel=3&tsel=3&sc=1&callback=Page.test";
    
};
Page.test = function () {
    alert("done");
};
$(function () {
    
});