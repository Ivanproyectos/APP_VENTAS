﻿
var Tamanio_Valido = 314572800; // 300MB -- Cantidad en MB
var MiSistema =  $("#inputHddNombre_Sistema").val() == "" ? "":  "/" +$("#inputHddNombre_Sistema").val();

/////////// para los menus active open; 
$('.quixnav .metismenu ul a ').click(function () {
     
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
function _blockUI(message) {
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
            $("#" + lblObject).html("<span class=\"text-success\">Nº Caracteres: " + cant + " restan " + total + "</span>");

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
  

/*ocultar password*/

//$('div[data-password="false"]').click(function () {
//     
//    var _Elements = $(this).parent().children();
//    var _Input = _Elements[0].type = "password";
//    var _div = _Elements[1]; 
//    _div.dataset.password = "true";
//    $(_div).removeClass('show-password');
//}); 

$('div[data-password]').click(function () {
    var _Elements = $(this).parent().children();
    var _div = _Elements[1];
    var _Input = _Elements[0]
    if (_div.dataset.password == "true") {
        _Input.type = "text";
        _div.dataset.password = "false";
        $(_div).addClass('show-password');
    } else if (_div.dataset.password == "false") {
        _Input.type = "password";
        _div.dataset.password = "true";
        $(_div).removeClass('show-password');
    }

});

function CreateDateRange(_input, calback) {
    var fec_inicio = "";
    var fec_fin = "";
    $('#' + _input).daterangepicker({
        opens: 'right',
        ranges: {
            'Hoy': [moment(), moment()],
            'Ayer': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Los últimos 7 días': [moment().subtract(6, 'days'), moment()],
            'Los últimos 30 días': [moment().subtract(29, 'days'), moment()],
            'Este mes': [moment().startOf('month'), moment().endOf('month')],
            'El mes pasado': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        },
        locale: {
            format: "DD/MM/YYYY",
            applyLabel: "Aplicar",
            cancelLabel: "Cancelar",
            customRangeLabel: "Rango Personalizado",
        }
    }, function (start, end, label) {
        fec_inicio = start.format("DD/MM/YYYY");
        fec_fin = end.format("DD/MM/YYYY");
        calback(fec_inicio, fec_fin)
    });
}


