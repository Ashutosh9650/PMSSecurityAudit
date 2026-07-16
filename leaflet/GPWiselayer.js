
function SetColorRange(txtno) {
    
    if (txtno == '0') {
        document.getElementById("txtmax1").value = document.getElementById("txtmax0").value;
        document.getElementById("txtmax2").value = document.getElementById("txtmax0").value;
        document.getElementById("txtmax3").value = document.getElementById("txtmax0").value;
        document.getElementById("txtmax4").value = document.getElementById("txtmax0").value;
        document.getElementById("txtmax5").value = document.getElementById("txtmax0").value;
        document.getElementById("txtmax6").value = document.getElementById("txtmax0").value;
        document.getElementById("txtmax7").value = document.getElementById("txtmax0").value;
    }
    else if (txtno == '1') {

        document.getElementById("txtmax2").value = document.getElementById("txtmax1").value;
        document.getElementById("txtmax3").value = document.getElementById("txtmax1").value;
        document.getElementById("txtmax4").value = document.getElementById("txtmax1").value;
        document.getElementById("txtmax5").value = document.getElementById("txtmax1").value;
        document.getElementById("txtmax6").value = document.getElementById("txtmax1").value;
        document.getElementById("txtmax7").value = document.getElementById("txtmax1").value;
    }
    else if (txtno == '2') {


        document.getElementById("txtmax3").value = document.getElementById("txtmax2").value;
        document.getElementById("txtmax4").value = document.getElementById("txtmax2").value;
        document.getElementById("txtmax5").value = document.getElementById("txtmax2").value;
        document.getElementById("txtmax6").value = document.getElementById("txtmax2").value;
        document.getElementById("txtmax7").value = document.getElementById("txtmax2").value;
    }
    else if (txtno == '3') {


        document.getElementById("txtmax4").value = document.getElementById("txtmax3").value;
        document.getElementById("txtmax5").value = document.getElementById("txtmax3").value;
        document.getElementById("txtmax6").value = document.getElementById("txtmax3").value;
        document.getElementById("txtmax7").value = document.getElementById("txtmax3").value;
    }
    else if (txtno == '4') {


        document.getElementById("txtmax5").value = document.getElementById("txtmax4").value;
        document.getElementById("txtmax6").value = document.getElementById("txtmax4").value;
        document.getElementById("txtmax7").value = document.getElementById("txtmax4").value;
    }
    else if (txtno == '5') {


        document.getElementById("txtmax6").value = document.getElementById("txtmax5").value;
        document.getElementById("txtmax7").value = document.getElementById("txtmax5").value;
    }
    else if (txtno == '6') {


        document.getElementById("txtmax7").value = document.getElementById("txtmax6").value;
    }

    $("#hdmaxV1").val(document.getElementById("txtmax1").value);
    $("#hdmaxV3").val(document.getElementById("txtmax3").value);
    $("#hdmaxV5").val(document.getElementById("txtmax5").value);
    $("#hdmaxV7").val(document.getElementById("txtmax7").value);

}


function GPWiseGetcolor(PanchayatC) {

    GPWiseGetcolorthreshold(PanchayatC);
    var value = $("#hdfillcolor").val();

    var max1 = $("#hdmaxV1").val();
    var max2 = $("#hdmaxV3").val();
    var max3 = $("#hdmaxV5").val();
    var max4 = $("#hdmaxV7").val();

    var Col1 = $("#hdcolor1").val();
    var Col2 = $("#hdcolor2").val();
    var Col3 = $("#hdcolor3").val();
    var Col4 = $("#hdcolor4").val();

    
    if (parseInt(value) > parseInt(max4)) {

        return '#' + Col4;

    } else if (parseInt(value) > parseInt(max3)) {

        return '#' + Col4;

    } else if (parseInt(value) > parseInt(max2)) {

        return '#' + Col3;

    } else if (parseInt(value) > parseInt(max1)) {

        return '#' + Col2;

    } else if (parseInt(value) < parseInt(max1)) {

        return '#' + Col1;

    } else {

        return 'green';

    }
}


function GPWiseGetcolorthreshold(PanchayatC) {
    
    $.ajax({
        type: "POST",
        url: "frmGISlayerMap.aspx/getcolorthreshold",
        data: "{'blockcode':'" + PanchayatC + "'}",
        contentType: "application/json",
        dataType: "json",
        async: false,
        success: function (responce) {

            $("#hdfillcolor").val(responce.d);

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);

        }
    });


}


function countriesStyle2(feature) {
    return {
        fillColor: GPWiseGetcolor(feature.properties.PanchayatC),
        weight: 2,
        opacity: 1,
        color: 'black',
        dashArray: 3,
        fillOpacity: 0.9
    }
}
var highlightStyle = {
                
                 weight: 5,
                 opacity: 0.6,
                 fillOpacity: 0.65,
                
             };
             var defaultStyle = {
                 
                 weight: 3,
                 opacity: 0.6,
                 fillOpacity: 0.65,
                 
             };
function GPWisePopUp(feature, layer) {
    

    (function (layer, properties) {
        // Create a mouseover event
        layer.on("mouseover", function (e) {
            // Change the style to the highlighted version
            layer.setStyle(highlightStyle);
            GPWiseGetcolorthreshold(feature.properties.PanchayatC);
            // Create a popup with a unique ID linked to this record
            var popup = $("<div></div>", {
                id: "popup-" + properties.DISTRICT,
                css: {
                    position: "absolute",
                    bottom: "85px",
                    left: "50px",
                    zIndex: 1002,
                    backgroundColor: "white",
                    padding: "8px",
                    border: "1px solid #ccc"
                }
            });
            // Insert a headline into that popup
            var hed = $("<div></div>", {
                text: "GP " + properties.PanchayatN + ": " + $("#hdfillcolor").val(),
                css: { fontSize: "16px", marginBottom: "3px" }
            }).appendTo(popup);
            // Add the popup to the map
            popup.appendTo("#Map2");
        });
        // Create a mouseout event that undoes the mouseover changes
        layer.on("mouseout", function (e) {
            // Start by reverting the style back
            layer.setStyle(defaultStyle);
            // And then destroying the popup
            $("#popup-" + properties.DISTRICT).remove();
        });
        // Close the "anonymous" wrapper function, and call it while passing
        // in the variables necessary to make the events work the way we want.
        GPWiseGetcolorthreshold(feature.properties.PanchayatC);
        layer.bindPopup('  <h1>' + feature.properties.B_NAME + '&nbsp;&nbsp;<a href="#" onclick="Map2();" return false;" class="speciallink">No of toilet [ ' + $("#hdfillcolor").val() + ' ]</a></h1>');

    })(layer, feature.properties);

}