$(document).ready(function () {
    var sub_width = 0;
    var sub_height = 0;
    $(".large").css("background", "url('" + $(".small").attr("src") + "') no-repeat");

    $(".zoom-area").mousemove(function (e) {
        if (!sub_width && !sub_height) {
            var image_object = new Image();
            image_object.src = $(".small").attr("src");
            sub_width = image_object.width;
            sub_height = image_object.height;
        }
        else {
            var magnify_position = $(this).offset();

            var mx = e.pageX - magnify_position.left;
            var my = e.pageY - magnify_position.top;

            if (mx < $(this).width() && my < $(this).height() && mx > 0 && my > 0) {
                $(".large").fadeIn(100);
            }
            else {
                $(".large").fadeOut(100);
            }
            if ($(".large").is(":visible")) {
                var rx = Math.round(mx / $(".small").width() * sub_width - $(".large").width() / 2) * -1;
                var ry = Math.round(my / $(".small").height() * sub_height - $(".large").height() / 2) * -1;

                var bgp = rx + "px " + ry + "px";

                var px = mx - $(".large").width() / 2;
                var py = my - $(".large").height() / 2;

                $(".large").css({ left: px, top: py, backgroundPosition: bgp });
            }
        }
    })
})