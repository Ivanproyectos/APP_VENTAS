
var Tamanio_Valido = 314572800; // 300MB -- Cantidad en MB
var MiSistema =  $("#inputHddNombre_Sistema").val() == "" ? "":  "/" +$("#inputHddNombre_Sistema").val();

/////////// para los menus active open; 
$('.quixnav .metismenu ul a ').click(function () {
    debugger; 
    var _this = $(this).parent().children(); 
    $('.quixnav .metismenu ul a ').removeClass('quixnav_a_active quixnav_a_active_before');
    $(this).addClass('quixnav_a_active quixnav_a_active_before');
    $(this).addClass('quixnav_a_active quixnav_a_active_before');

    var _liPadre = $(this).parent().parent().parent();
    var _aPadre = _liPadre.children();
    var _TextPadre = _aPadre[0].innerText;
    var _TextHijo = $(this).text(); 

    $('#Text_padre').text(_TextPadre);
    $('#Text_Hijo').text(_TextHijo);
    //_liPadre.children();
});

/////////// para los menus sin grupo; 
$('.quixnav .metismenu li .not-before').click(function () {
    var _this = $(this);
    var _TextHijo = _this[0].innerText;
    $('#Text_padre').text('Home');
    $('#Text_Hijo').text(_TextHijo);

});

// block ui
function blockUI_(message) {
message == "" ? "Procesando..." : message; 
jQuery.blockUI({
    message: "<div class=\"css_center_block\">  <div class=\"Loader_block\"> "
                    + "<div class=\"spinner2\">"
                        + " <div class=\"circle1\"></div>"
                        + " <div class=\"circle2\"></div>"
                    + " </div>"
                + " </div> <p style=\"color:white;\">" + message + "</p>  </div> ",
css: { width: "20px", left: "45%", top: "40%", background: "none" }
});
}


// validaciones

function CountCharactersControlTxt(obj, lblObject, max) {
    try {
        var total = max;
        cant = document.getElementById(obj).value.length;
        total = total - cant
        if (cant > max) {
            var aux = document.getElementById(obj).value;
            document.getElementById(obj).value = aux.substring(0, max);
            return;
        }
        $("#" + lblObject).html("Nº Caracteres: " + cant + " restan " + total);
    } catch (e) {
        alert(e.Message);
    }
}

/**********************************************     VALIDACION DE FECHAS          

**********************************************/

function ValidarFormatoFecha(campo) {

    var RegExPattern = /^\d{1,2}\/\d{1,2}\/\d{2,4}$/;
    if ((campo.match(RegExPattern)) && (campo != '')) {
        return true;
    } else {
        return false;
    }
}

function ValidarFecha(fecha) {

    var fechaf = fecha.split("/");
    var day = fechaf[0];
    var month = fechaf[1];
    if (Number(month) > 12 || Number(month) < 1) {
        return false;
    }
    var year = fechaf[2];
    var date = new Date(year, month, '0');
    if ((day - 0) > (date.getDate() - 0)) {
        return false;
    }
    return true;
}

function ValidarFechasInicioFin(fechaini, fechafin, tipo) {

    var valido = true;
    //var fechaini = $('#' + fec_ini).val();
    //var fechafin = $('#' + fec_fin).val();
    if (fechaini != "") {
        if (ValidarFormatoFecha(fechaini) != true) {
            jWarning('La fecha es incorrecta', 'Alerta');
            valido = false;
            return valido;
        }
        if (ValidarFecha(fechaini) != true) {
            jWarning('La fecha no existe', 'Alerta');
            valido = false;
            return valido;
        }
    }
    if (fechafin != "") {
        if (ValidarFormatoFecha(fechafin) != true) {
            jWarning('La fecha fin es incorrecta', 'Alerta');
            valido = false;
            return valido;
        }

        if (ValidarFecha(fechafin) != true) {
            jWarning('La fecha fin no existe', 'Alerta');
            valido = false;
            return valido;
        }
    }
    if ((fechaini != "" || fechafin != "") && tipo) {
        if (fechaini == "") {
            jWarning('La fecha no puede estar vacio si hay fecha final','Alerta');
            valido = false;
            return valido;
        }
        if (fechafin == "") {
            jWarning('La fecha final no puede estar vacio si hay fecha inicio','Alerta');
            valido = false;
            return valido;
        }
        var x = new Date();
        var fecha = fechaini.split("/");
        if (fecha[2].length != 4) {
            jWarning('La fecha tiene el año incompleto', 'Alerta');
            valido = false;
            return valido;
        }

        x.setFullYear(fecha[2], fecha[1] - 1, fecha[0]);

        var x1 = new Date();
        var fecha1 = fechafin.split("/");
        x1.setFullYear(fecha1[2], fecha1[1] - 1, fecha1[0]);
        if (fecha1[2].length != 4) {
            jWarning( 'La fecha fin tiene el año incompleto','Alerta');
            valido = false;
            return valido;
        }
        if (x > x1) {
            jWarning( 'La fecha inicio no puede ser mayor a la final','Alerta');
            valido = false;
            return valido;
        }
    }

    return valido;
}



function SoloDecimal(e, field) {
    debugger; 
    key = e.keyCode ? e.keyCode : e.which
    // backspace
    if (key == 8) return true
    // 0-9
    if (key > 47 && key < 58) {
        if (field.value == "") return true
        regexp = /.[0-9]{3}$/     // configurar cuantos numeros como max 
        return !(regexp.test(field.value))
    }
    // .
    if (key == 46) {
        if (field.value == "") return false
        regexp = /^[0-9]+$/
        return regexp.test(field.value)
    }
}

function rand_code(lon) {
    var chars = "0123456789abcdefABCDEF";
    code = "";
    for (x = 0; x < lon; x++) {
        rand = Math.floor(Math.random() * chars.length);
        code += chars.substr(rand, 1);
    }
    return code.toUpperCase();
}

/* card collapse*/
function CollapsearchCard (_this) {
    var _this = $(_this).parent().children(); // listado card
    var _body = _this[1]; // body card
    $(_this).toggleClass('rotate-icon'); 
    $(_body).toggleClass('card-body-hide');
}
  

/*export grilla a excel*/

function ExportJQGridPaginacionDataToExcel(tableCtrl, excelFilename, urlListar, _ORDEN, _SORT_ORDEN) {
    debugger; 
    var migrilla = new Object();
    migrilla.page = 1;
    migrilla.rows = 999999;
    migrilla.sidx = _ORDEN;
    migrilla.sord = _SORT_ORDEN;
    migrilla._search = false;
    migrilla.filters = "";
    //if (opciones.rules != false) {
    migrilla.Rules = GetRules(tableCtrl);
    //}

    //if (migrilla._search == true) {
    //    migrilla.searchField = postdata.searchField;
    //    migrilla.searchOper = postdata.searchOper;
    //    migrilla.searchString = postdata.searchString;
    //}
    migrilla.usu = $("#input_hdidusuario").val();
    migrilla.ofi = $("#input_hdidoficina").val();
    migrilla.perfil = $("#input_hdidperfil").val();
    var params = { grid: migrilla };
    var allJQGridData;
    $.ajax({
        url: urlListar,
        type: 'post',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(params),
        async: false,
        success: function (data, st) {
            if (st == 'success') {

                allJQGridData = data;
                //var jq = $('#' + grilla)[0];
                //jq.addJSONData(data);
            }
        },
        error: function (a, b, c) {
            alert('Error with AJAX callback');
        }
    });
    debugger;
    //var allJQGridData = $(tableCtrl).jqGrid('getRowData'); 
    var jqgridRowIDs = $(tableCtrl).getDataIDs();                // Fetch the RowIDs for this grid
    var headerData = $(tableCtrl).getRowData(jqgridRowIDs[0]);   // Fetch the list of "name" values in our colModel

    //  For each visible column in our jqGrid, fetch it's Name, and it's Header-Text value
    var columnNames = new Array();       //  The "name" values from our jqGrid colModel
    var columnHeaders = new Array();     //  The Header-Text, from the jqGrid "colNames" section
    var inx = 1;
    var allColumnNames = $(tableCtrl).jqGrid('getGridParam', 'colNames');

    //  If our jqGrid has "MultiSelect" set to true, remove the first (checkbox) column, otherwise we'll
    //  create an exception of: "A potentially dangerous Request.Form value was detected from the client."
    var bIsMultiSelect = $(tableCtrl).jqGrid('getGridParam', 'multiselect');
    if (bIsMultiSelect) {
        inx++;
    }
    var ihd = 0;
    for (var headerValue in headerData) {
        //  If this column ISN'T hidden, and DOES have a column-name, then we'll export its data to Excel.

        var column_ = $(tableCtrl).jqGrid("getColProp", headerValue);
        var isColumnHidden = $(tableCtrl).jqGrid("getColProp", headerValue).hidden;
        //debugger;
        if (!isColumnHidden && headerValue != null && column_.index != '') {

            columnNames.push(headerValue);
            columnHeaders.push(allColumnNames[inx]);
        } else
            ihd++;
        inx++;
    }

    //  We now need to build up a (potentially very long) tab-separated string containing all of the data (and a header row)
    //  which we'll want to export to Excel.

    //  First, let's append the header row...
    var excelData = '';
    for (var k = 0; k < columnNames.length; k++) {
        excelData += columnHeaders[k] + "\t";
    }
    excelData = removeLastChar(excelData) + "\r\n";

    //  ..then each row of data to be exported.
    var cellValue = ''; 
    for (i = 0; i < allJQGridData.rows.length; i++) {
        //for (i = 0; i < 2; i++) {

        var data = allJQGridData.rows[i];

        for (var j = 0; j < columnNames.length; j++) {

            // Fetch one jqGrid cell's data, but make sure it's a string
            cellValue = '' + data.cell[j + ihd];
            cellValue = cellValue.replace(/(\r\n|\n|\r|\t|\nb)/gm, "");
            //cellValue = '-';
            //if (cellValue == 'INFORME 0075-2021-ANA-OA-UATD') {
            //    debugger;
            //    var sadas  = 1;
            //}
            //if (j = 0) cellValue = 'Hacemos todo';
            //if (j == 1) cellValue = 'Hacemos todo';
            //if (j == 2) cellValue = 'Hacemos todo';
            //if (j == 3) cellValue = 'Hacemos todo';
            //if (j == 4) cellValue = 'Hacemos todo';
            //if (j == 4) {
            //    debugger;
            //    cellValue = 'Hacemos todo';
            //}
            //if (j == 5) cellValue = 'Hacemos todo';
            //if (j == 6) cellValue = 'Hacemos todo';
            //if (j == 7) cellValue = 'Hacemos todo';

            //if (cellValue == '' || cellValue == '-') cellValue = 'Hacemos todo';
            //excelData += "\t";
            if (cellValue == null)
                excelData += "\t";
            else {
                if (cellValue.indexOf("a href") > -1) {
                    //  Some of my cells have a jqGrid cell with a formatter in them, making them hyperlinks.
                    //  We don't want to export the "<a href..> </a>" tags to our Excel file, just the cell's text.
                    cellValue = $(cellValue).text();
                }
                //  Make sure we are able to POST data containing apostrophes in it
                cellValue = cellValue.replace(/'/g, "&apos;");
                cellValue = cellValue.replace(/r'/g, "&apos;");
                excelData += cellValue + "\t";
            }
        }
        excelData = removeLastChar(excelData) + "\r\n";
    }

    //  Now, we need to POST our Excel Data to our .ashx file *and* redirect to the .ashx file.
    //postAndRedirect("/Fermin/Handlers/Exportar_Grilla_Excel.ashx?filename=" + excelFilename, { excelData: excelData });
    postAndRedirect(MiSistema+"/Handlers/Exportar_Grilla_Excel.ashx?filename=" + excelFilename, { excelData: excelData });
}

function removeLastChar(str) {
    //  Remove the last character from a string
    return str.substring(0, str.length - 1);
}


function postAndRedirect(url, postData) {
    debugger;
    //  Redirect to a URL, and POST some data to it.
    //  Taken from:
    //  http://stackoverflow.com/questions/8389646/send-post-data-on-redirect-with-javascript-jquery
    //
    var postFormStr = "<form method='POST' action='" + url + "'>\n";

    for (var key in postData) {
        if (postData.hasOwnProperty(key)) {
            postFormStr += "<input type='hidden' name='" + key + "' value='" + postData[key] + "'></input>";
        }
    }

    postFormStr += "</form>";

    var formElement = $(postFormStr);

    $('body').append(formElement);
    $(formElement).submit();
}
