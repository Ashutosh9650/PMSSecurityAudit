
function MakeStaticHeader(gridId, width, headerHeight, isFooter, DivH, DivM, DivF) {
    var tbl = document.getElementById(gridId);
    if (tbl) {
     
        //                 var DivHR = document.getElementById('DivHeaderRow');
        //                 var DivMC = document.getElementById('DivMainContent');
        //                 var DivFR = document.getElementById('DivFooterRow');
        //  var pp = 'DivHeaderRow';
        var DivHR = document.getElementById(DivH);
        var DivMC = document.getElementById(DivM);
        var DivFR = document.getElementById(DivF);

        //*** Set divheaderRow Properties ****
        DivHR.style.height = headerHeight + 'px';
        DivHR.style.width = (parseInt(width) - 16) + 'px';
        DivHR.style.position = 'relative';
        DivHR.style.top = '0px';
        DivHR.style.zIndex = '10';
        DivHR.style.verticalAlign = 'top';

        //*** Set divMainContent Properties ****
        DivMC.style.width = parseInt(width) + 'px';
        //                 DivMC.style.height = 500 + 'px';
        DivMC.style.position = 'relative';
        DivMC.style.top = -headerHeight + 'px';
        DivMC.style.zIndex = '1';

        //*** Set divFooterRow Properties ****
        DivFR.style.width = (parseInt(width) - 16) + 'px';
        DivFR.style.position = 'relative';
        DivFR.style.top = -0 + 'px';
        DivFR.style.verticalAlign = 'top';
        DivFR.style.paddingtop = '2px';

        if (isFooter) {
            var tblfr = tbl.cloneNode(true);
            tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
            var tblBody = document.createElement('tbody');
            tblfr.style.width = '100%';
            tblfr.cellSpacing = "0";

            tblfr.border = "0px";
            tblfr.rules = "none";
            //*****In the case of Footer Row *******
            // tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
            // tblfr.appendChild(tblBody);
            // DivFR.appendChild(tblfr);s
        }
        //****Copy Header in divHeaderRow****
        DivHR.appendChild(tbl.cloneNode(true));
    }
}



function OnScrollDiv(Scrollablediv, DiHR, DiFR) {

    //             document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
    //             document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
    document.getElementById(DiHR).scrollLeft = Scrollablediv.scrollLeft;
    document.getElementById(DiFR).scrollLeft = Scrollablediv.scrollLeft;
}


