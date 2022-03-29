var CuentasCobrar_Grilla = 'CuentasCobrar_Grilla';
var CuentasCobrar_Barra = 'CuentasCobrar_Barra';

function CuentasCobrar_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function CuentasCobrar_Limpiar() {
    $("#CuentasCobrar_CodigoVenta").val('');
    $('#CuentasCobrar_FLG_CREDITO').val('');
    $('#ID_CLIENTE').val('');
    $('#CuentasCobrar_FechaInicio').val('');

    CuentasCobrar_ConfigurarGrilla();
}

function CuentasCobrar_ConfigurarGrilla() {
    var url = baseUrl + 'CuentasCobrar/CuentasCobrar/CuentasCobrar_Paginado';
    $("#" + CuentasCobrar_Grilla).GridUnload();
    var colNames = ['Acciones', 'Código', 'ID', 'Código Venta', 'Tipo Comprobante', 'Cliente', 'Descuento','Adelanto', 'Total', 'Debe', 
       'Fecha Venta', 'COD_COMPROBANTE',  'flg_credito','Estado Credito','flg_anulado','Detalle','Fecha Cancelado'];
    var colModels = [
            { name: 'ACCION', index: 'ACCION', align: 'center', width: 100, hidden: false, formatter: CuentasCobrar_actionAcciones, sortable: false }, // 0
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },// 1
            { name: 'ID_VENTA', index: 'ID_VENTA', width: 100, hidden: true, key: true }, // 2
            { name: 'COD_VENTA', index: 'COD_VENTA', width: 150, hidden: false, align: "left" }, // 3
            { name: 'TIPO_COMPROBANTE', index: 'TIPO_COMPROBANTE', width: 150, hidden: false, align: "left", formatter: CuentasCobrar_FormaterComprobante }, // 4
            { name: 'CLIENTE', index: 'CLIENTE', width: 250, hidden: false, align: "left" }, // 5
            { name: 'DESCUENTO', index: 'DESCUENTO', width: 100, hidden: false, align: "left" }, // 6
            { name: 'ADELANTO', index: 'ADELANTO', width: 100, hidden: false, align: "left" }, // 7
            { name: 'TOTAL', index: 'TOTAL', width: 100, hidden: false, align: "left", formatter: Ventas_FormatterTotal }, // 8
            { name: 'DEBE', index: 'DEBE', width: 100, hidden: false, align: "left", formatter: Ventas_FormatterDebe }, // 9
            { name: 'FEC_CREACION', index: 'FEC_CREACION', width: 150, hidden: false, align: "left" },//10
            { name: 'COD_COMPROBANTE', index: 'COD_COMPROBANTE', width: 150, hidden: true, align: "left" },//11
            { name: 'FLG_ESTADO_CREDITO', index: 'FLG_CRED_CANCELADO', width: 150, hidden: true, align: "left" },//12
            { name: 'DESC_ESTADO_CREDITO', index: 'DESC_ESTADO_CREDITO', width: 150, hidden: false, align: "left", formatter: CuentasCobrar_formatterEstadoCredito },//13
            { name: 'FLG_ANULADO', index: 'FLG_ANULADO', width: 150, hidden: true, align: "left"},//14
            { name: 'DETALLE', index: 'DETALLE', width: 200, hidden: false, align: "left" },//15
            { name: 'STR_FECHA_CREDITO_CANCELADO', index: 'STR_FECHA_CREDITO_CANCELADO', width: 200, hidden: true, align: "left" },//16
            
    ];
    var opciones = {
        GridLocal: false, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, rules: true, rowNumber: 50, rowNumbers: [50, 100, 200, 300, 500],
    };
    SICA.Grilla(CuentasCobrar_Grilla, CuentasCobrar_Barra, CuentasCobrar_Grilla, 400, '', "Lista de Cuentas por Cobrar", url, 'ID_VENTA', colNames, colModels, 'ID_VENTA', opciones);
}


function GetRules(CuentasCobrar_Grilla) {
    var rules = new Array();
    var CODIGO_COMPROBANTE = jQuery('#CuentasCobrar_CodigoVenta').val();
    var FECHA_VENTA = jQuery('#CuentasCobrar_FechaVenta').val() == '' ? null : "'" + jQuery('#CuentasCobrar_FechaVenta').val() + "'";
    var ID_CLIENTE = jQuery('#ID_CLIENTE').val() == '' ? null : "'" + jQuery('#ID_CLIENTE').val() + "'";
    var FLG_ESTADO_CREDITO = jQuery('#CuentasCobrar_FLG_CREDITO').val() == '' ? null : "'" + jQuery('#CuentasCobrar_FLG_CREDITO').val() + "'";
    var ID_SUCURSAL = "'" + jQuery('#ID_SUCURSAL').val() + "'";


    var POR = "'%'";
    rules = []
    rules.push({ field: 'UPPER(COD_COMPROBANTE)', data: POR + ' + ' + CODIGO_COMPROBANTE + ' + ' + POR, op: " LIKE " });
    rules.push({ field: 'FLG_ESTADO_CREDITO', data: '  ISNULL(' + FLG_ESTADO_CREDITO + ',FLG_ESTADO_CREDITO) ', op: " = " });
    rules.push({ field: 'ID_CLIENTE', data: '  ISNULL(' + ID_CLIENTE + ',ID_CLIENTE) ', op: " = " });
    rules.push({ field: 'CONVERT(DATE,FEC_CREACION,103)', data: 'CONVERT(DATE,ISNULL(' + FECHA_VENTA + ',FEC_CREACION),103)  ', op: " = " });
    rules.push({ field: 'UPPER(COD_COMPROBANTE)', data: POR + ' + ' + CODIGO_COMPROBANTE + ' + ' + POR, op: " LIKE " });
    rules.push({ field: 'ID_SUCURSAL', data: ID_SUCURSAL, op: " = " });
    rules.push({ field: '(FLG_TIPO_PAGO', data: '2 OR FLG_ESTADO_CREDITO  = 1 )', op: " = " }); // CREDITO
    rules.push({ field: 'FLG_ANULADO', data: 0, op: " = " }); // NO ANULADOS

    return rules;
}

function CuentasCobrar_actionAcciones(cellvalue, options, rowObject) {
    var _ID_VENTA = rowObject[2];
    var _FLG_FLG_ANULADO = rowObject[14];
    var _FLG_ESTADO_CREDITO = rowObject[12];

    var _btn_Cobrar =""; 
    var _btn_Anular = "";
    var _btn_Notificar = ""; 
    if (_FLG_FLG_ANULADO == 0 && _FLG_ESTADO_CREDITO == 0) 
        _btn_Anular = "<a class=\"dropdown-item\" onclick='Ventas_AnularVenta(" + _ID_VENTA + ")'><i class=\"bi bi-cart-x\" style=\"color:red;\"></i>&nbsp;Anular</a>";

    if (_FLG_ESTADO_CREDITO == 0) {
        _btn_Cobrar = "<a class=\"dropdown-item\" onclick='CuentasCobrar_MostrarCobrarCredito(" + _ID_VENTA + ")'><i class=\"bi bi-cash-coin\" style=\"color:#2c7be5\"></i>&nbsp;Cobrar</a>";
        _btn_Notificar = "<a class=\"dropdown-item\" onclick='CuentasCobrar_MostrarCobrarCredito(" + _ID_VENTA + ")'><i class=\"bi bi-send\" style=\"color:#D34320\"></i>&nbsp;Notificar Credito</a>";
    }

    var _btn = "<div class=\"btn-group Group_Acciones\" role=\"group\" title=\"Acciones \" >" +
           " <button  style=\" background: transparent; border: none; color: #000000;font-size: 18px;\" type=\"button\" class=\"btn btn-primary dropdown-toggle\" data-toggle=\"dropdown\" aria-expanded=\"false\"><i class=\"bi bi-list\"></i></button> " +
           " <div class=\"dropdown-menu\" x-placement=\"bottom-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 35px, 0px);\">" +
            "<a class=\"dropdown-item\" onclick='Ventas_ViewDetalleVenta(" + _ID_VENTA + ")'><i class=\"bi bi-stickies\" style=\"color:#2c7be5\"></i>&nbsp;  Detalle Venta</a>" +
               _btn_Notificar +
               _btn_Cobrar +
               _btn_Anular +              
            "<a class=\"dropdown-item\" onclick='CuentasCobrar_MostrarDevolverProducto(" + _ID_VENTA + ")' ><i class=\"bi bi-box-arrow-in-down-left\" style=\"color:green;\"></i>&nbsp;  Devolver Producto</a>" +
            "</div>" +
        "</div>";
    return _btn;
}

function CuentasCobrar_FormaterComprobante(cellvalue, options, rowObject) {
    var _DESC_COMPROBANTE = rowObject[4];
    var _DESC_COD_COMPROBANTE = rowObject[11];
    var _text = "";
    var _text = '<span>' + _DESC_COMPROBANTE + '</span><br><span style="font-size: 12px; color: #2c7be5;"><i class="bi bi-upc"></i>&nbsp;Código: ' + _DESC_COD_COMPROBANTE + '</span>';
    return _text;
}

function Ventas_FormatterTotal(cellvalue, options, rowObject) {
    var _TOTAL = rowObject[8];
    var _text = _SimboloMoneda + " " + _TOTAL;
    return _text;
}

function Ventas_FormatterDebe(cellvalue, options, rowObject) {
    var _DEBE = rowObject[9];
    var _text = _SimboloMoneda + " " + _DEBE;
    return _text;
}


function CuentasCobrar_formatterEstadoCredito(cellvalue, options, rowObject) {
    var _FLG_ESTADO_CREDITO = rowObject[12];
    var _DESC_ESTADO_CREDITO = rowObject[13];
    var _FECHA_CANCELADO = rowObject[16];
    var _text = "";
    if (_FLG_ESTADO_CREDITO == 0) {
        _text = "<span class=\"badge badge-danger \" data-bs-toggle=\"tooltip\" title=\"Este credito aun esta pendiente.\">" + _DESC_ESTADO_CREDITO + "</span>";
    }
    else if (_FLG_ESTADO_CREDITO == 1) {
        _text = "<span class=\"badge badge-success\" data-bs-toggle=\"tooltip\" title=\"Este credito ya fue cancelado.\">" + _DESC_ESTADO_CREDITO + "</span>"
                + " <br><span style=\"font-size: 12px; color: #2c7be5;\"><i class=\"bi bi-calendar-week\"></i>&nbsp;Fecha/Hora: " + _FECHA_CANCELADO + "</span>";
    }
    return _text;
}

function CuentasCobrar_MostrarCobrarCredito(ID_VENTA) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "CuentasCobrar/CuentasCobrar/Mantenimiento?ID_VENTA=" + ID_VENTA, function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


function CuentasCobrar_MostrarDevolverProducto(ID_VENTA) {
    var _TIPO_DETALLE = "DEVOLVER";
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Ventas/Ventas/Mantenimiento_ViewDetalleProducto?ID_VENTA=" + ID_VENTA + "&TIPO=" + _TIPO_DETALLE, function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

///*********************************************** ----------------- *************************************************/

///*********************************************** anular ventas  ***************************************************/

//// calcular vuelto
function Fn_CuentasCobrar_Vuelto() {
    var _Total = isNaN(parseFloat($('#Venta_TotalDebe').text())) ? 0 : parseFloat($('#Venta_TotalDebe').text());
    var _PagoCon = isNaN(parseFloat($('#TOTAL_RECIBIDO').val())) ? 0 : parseFloat($('#TOTAL_RECIBIDO').val());
    var _Vuelto = (_PagoCon - _Total); +
    $('#VUELTO').val(Number(_Vuelto).toFixed(2));
}


///*********************************************** ----------------- *************************************************/

///*********************************************** Insertar Cobranza  **************************************************/

function CuentasCobrar_Ingresar() {
    if ($("#frmMantenimiento_CuentasCobrar").valid() && $("#frmMantenimiento_Detalle").valid()) {
            jConfirm("¿ Desea cancelar este credito. ?", "Atención", function (r) {
                if (r) {
                    var item =
                        {
                            ID_VENTA: $("#hfd_ID_VENTA").val(),
                            ID_CLIENTE: $("#hfd_ID_CLIENTE").val(),
                            TOTAL: parseFloat($("#Venta_TotalDebe").text()),
                            USU_CREACION: $('#input_hdcodusuario').val(),
                            FLG_TIPO_PAGO: $("#FLG_TIPO_PAGO").val(),
                            NRO_OPERACION: $("#NRO_OPERACION").val(),
                  
                        };
                    var url = baseUrl + 'CuentasCobrar/CuentasCobrar/CuentasCobrar_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                CuentasCobrar_ConfigurarGrilla();
                                CuentasCobrar_Cerrar();
                                Ventas_GenerarVistaComprobante(auditoria.OBJETO)
                            } else {
                                jError(auditoria.MENSAJE_SALIDA, "Atención");
                            }
                        } else {
                            jError(auditoria.MENSAJE_SALIDA, "Atención");
                        }
                    }
                }
            });
    }
}

