//$(document).ready(function () {
//    $(document).data('is_show', true);
//    $("#ton").click(function () {
//        if ($(document).data('is_show') == true) {
//            $('#div-show').hide(1000);
//            $(document).data('is_show', false);
//        } else {
//            $('#div-show').show(1000);
//            $(document).data('is_show', true);
//        }
//    });
//});

function fnNew(pValue) {
    debugger;
    $(document).data('is_show1', pValue);
    $("#ton").click(function () {

        if ($(document).data('is_show1') == true) {
            $('#div-show').hide(1000);
            $("#ctl00_MainContent_hdnbtnValue").val(1);
            $(document).data('is_show1', false);
        } else {
            $('#div-show').show(1000);
            $(document).data('is_show1', true);
            $("#ctl00_MainContent_hdnbtnValue").val(2);
        }
    });
    if ($(document).data('is_show1') == true) {
        $('#div-show').hide(100);
        $("#ctl00_MainContent_hdnbtnValue").val(1);
        $(document).data('is_show1', false);
    } else {
        $('#div-show').show(100);
        $(document).data('is_show1', true);
        $("#ctl00_MainContent_hdnbtnValue").val(2);
    }
}