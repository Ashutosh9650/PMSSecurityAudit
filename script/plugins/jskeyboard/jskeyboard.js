var JSKeyboard = {};
//2305 //2416
JSKeyboard.Config = {};
//2947//3058
JSKeyboard.Config.Language = { Start: 2305, End: 2416, Extra: [{ keyCode: 0, key: "Sp" }, {keyCode:8,key:"Bk"}] };
JSKeyboard.Config.KeySize = 30;
JSKeyboard.SelTxt = null;
JSKeyboard.MainBoard = null;
JSKeyboard.HideTimer = 0;
JSKeyboard.Enable = true;
JSKeyboard.Attach = function (id) {
    $(id).bind('focus', JSKeyboard.Activate);
    $(id).bind('focusout', JSKeyboard.OutFocus);
    var dd = "௲";
    //alert(dd.charCodeAt(0));
};
JSKeyboard.Activate = function (data) {
    if (JSKeyboard.Enable == false) { return;}
    JSKeyboard.SelTxt = $(data.target);
    var txTop = JSKeyboard.SelTxt.offset().top + JSKeyboard.SelTxt.height();
    //console.log(JSKeyboard.SelTxt.offset().height)
    var txLeft = JSKeyboard.SelTxt.offset().left;
    var maxLeft = document.body.clientWidth- JSKeyboard.MainBoard.width() - 30;
    //console.log("maxLeft=" + maxLeft + "   txLeft=" + txLeft);
    if (txLeft > maxLeft) { txLeft = maxLeft; };
    JSKeyboard.MainBoard.css({
        top: txTop + 8,
        left: txLeft,
        "z-index":1005
    });
    JSKeyboard.MainBoard.show("slow");
};
JSKeyboard.OutFocus = function (data) {
    if (JSKeyboard.Enable == false) { return; }
    JSKeyboard.HideTimer = setTimeout(JSKeyboard.MouseLeave, 300);
};
JSKeyboard.KeyPress = function (data) {
    var keyBox = $(data.target);
    var keyCode = parseInt(keyBox.attr("keycode"));
    var oldVal = JSKeyboard.SelTxt.val();
    if (keyCode == 8) {
        oldVal = oldVal.toString().substr(0, oldVal.toString().length - 1);
        JSKeyboard.SelTxt.val(oldVal);
    } else if (keyCode == 0) {
        JSKeyboard.SelTxt.val(oldVal + " ");
    } else {
        JSKeyboard.SelTxt.val(oldVal + String.fromCharCode(keyCode));
    };
    
    keyBox[0].focus();
};
JSKeyboard.MouseMove = function (){
    clearTimeout(JSKeyboard.HideTimer);
};
JSKeyboard.MouseLeave = function () {
    JSKeyboard.MainBoard.hide("slow");
};
JSKeyboard.Init = function () {
    var mBody = $(document.body);
    mBody.append("<div class='jskeyboardbase' id='keyboadrbase'></div>");
    JSKeyboard.MainBoard = $("#keyboadrbase");
    var left = 10;
    var top = 10;
    var count = 0;    
    for (var i = JSKeyboard.Config.Language.Start; i <= JSKeyboard.Config.Language.End + JSKeyboard.Config.Language.Extra.length ; i++) {
        var keyID = i;
        if (keyID > JSKeyboard.Config.Language.End) {
            keyID = JSKeyboard.Config.Language.Extra[i - JSKeyboard.Config.Language.End - 1].keyCode;
            kkey = JSKeyboard.Config.Language.Extra[i - JSKeyboard.Config.Language.End - 1].key;
            JSKeyboard.MainBoard.append("<div keycode='" + keyID + "' class='jskeyboardkey unselectable' style='top:" + top + "px;left:" + left + "px'>" + kkey + "</div>")
        } else {
            JSKeyboard.MainBoard.append("<div keycode='" + keyID + "' class='jskeyboardkey unselectable' style='top:" + top + "px;left:" + left + "px'>" + String.fromCharCode(keyID) + "</div>")
        };
        
        left = left + 10 + JSKeyboard.Config.KeySize;
        count++;
        if (count >= 19)
        {
            count = 0;
            top = top + 15 + JSKeyboard.Config.KeySize;
            left = 10;
        };
    };
    $(".jskeyboardkey").css({
        width: JSKeyboard.Config.KeySize,
        height: JSKeyboard.Config.KeySize
    });
    $(".jskeyboardkey").bind("click", JSKeyboard.KeyPress);
    JSKeyboard.MainBoard.bind("mouseleave", JSKeyboard.MouseLeave);
    JSKeyboard.MainBoard.bind("mousemove", JSKeyboard.MouseMove);
    JSKeyboard.MainBoard.hide();
};
