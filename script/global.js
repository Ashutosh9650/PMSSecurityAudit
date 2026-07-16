var Global = {};
Global.LoaderImage = "images/ajax-loader.gif";
Global.CreateTemplate = function (TemplateString) {
    return TemplateString.replace(/temp_/g, "");
};