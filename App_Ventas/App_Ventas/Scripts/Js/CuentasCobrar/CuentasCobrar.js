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
    var url = baseUrl + 'Ventas/Ventas/Ventas_Paginado';
    $("#" + CuentasCobrar_Grilla).GridUnload();
    var colNames = ['Acciones', 'Código', 'ID', 'Código Venta', 'Tipo Comprobante', 'Cliente', 'Descuento', 'Subtotal', 'Igv', 'Total', 'Estado Venta', 'Tipo Pago',
       'Fecha Venta', 'COD_COMPROBANTE', 'Flg_anulado', 'flg_tipoventa', 'flg_credito'];
    var colModels = [
            { name: 'ACCION', index: 'ACCION', align: 'center', width: 100, hidden: false, formatter: CuentasCobrar_actionAcciones, sortable: false }, // 0
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },// 1
            { name: 'ID_VENTA', index: 'ID_VENTA', width: 100, hidden: true, key: true }, // 2
            { name: 'COD_VENTA', index: 'COD_VENTA', width: 150, hidden: false, align: "left" }, // 3
            { name: 'TIPO_COMPROBANTE', index: 'TIPO_COMPROBANTE', width: 150, hidden: false, align: "left", formatter: CuentasCobrar_FormaterComprobante }, // 4
            { name: 'CLIENTE', index: 'CLIENTE', width: 250, hidden: false, align: "left" }, // 5
            { name: 'DESCUENTO', index: 'DESCUENTO', width: 100, hidden: false, align: "left" }, // 6
            { name: 'SUBTOTAL', index: 'SUBTOTAL', width: 100, hidden: false, align: "left" }, // 7
            { name: 'IGV', index: 'IGV', width: 100, hidden: false, align: "left" }, // 8
            { name: 'TOTAL', index: 'TOTAL', width: 100, hidden: false, align: "left" }, // 9       
            { name: 'DESC_ESTADO_VENTA', index: 'DESC_ESTADO_VENTA', width: 150, hidden: false, align: "left", formatter: CuentasCobrar_Anulado }, // 10
            { name: 'DESC_TIPO_VENTA', index: 'DESC_TIPO_VENTA', width: 150, hidden: false, align: "left", formatter: CuentasCobrar_TipoVenta }, // 11
            { name: 'FEC_CREACION', index: 'FEC_CREACION', width: 150, hidden: false, align: "left" },//12
            { name: 'COD_COMPROBANTE', index: 'COD_COMPROBANTE', width: 150, hidden: true, align: "left" },//13
            { name: 'FLG_ANULADO', index: 'FLG_ANULADO', width: 150, hidden: true, align: "left" },//14
            { name: 'FLG_TIPO_VENTA', index: 'FLG_TIPO_VENTA', width: 150, hidden: true, align: "left" },//15
            { name: 'FLG_ESTADO_CREDITO', index: 'FLG_CRED_CANCELADO', width: 150, hidden: true, align: "left" },//16

    ];
    var opciones = {
        GridLocal: false, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, rules: true, rowNumber: 50, rowNumbers: [50, 100, 200, 300, 500],
    };
    SICA.Grilla(CuentasCobrar_Grilla, CuentasCobrar_Barra, CuentasCobrar_Grilla, 400, '', "Lista de CuentasCobrar", url, 'ID_VENTA', colNames, colModels, 'ID_VENTA', opciones);
}


function GetRules(CuentasCobrar_Grilla) {
    var rules = new Array();
    var CODIGO_COMPROBANTE = jQuery('#CuentasCobrar_CodigoVenta').val();
    var FECHA_VENTA = jQuery('#CuentasCobrar_FechaInicio').val();
    var ID_CLIENTE = jQuery('#ID_CLIENTE').val() == '' ? null : "'" + jQuery('#ID_CLIENTE').val() + "'";
    var FLG_ESTADO_CREDITO = jQuery('#CuentasCobrar_FLG_CREDITO').val() == '' ? null : "'" + jQuery('#CuentasCobrar_FLG_CREDITO').val() + "'";
    var ID_SUCURSAL = "'" + jQuery('#ID_SUCURSAL').val() + "'";


    var POR = "'%'";
    rules = []
    rules.push({ field: 'UPPER(COD_COMPROBANTE)', data: POR + ' + ' + CODIGO_COMPROBANTE + ' + ' + POR, op: " LIKE " });
    rules.push({ field: 'FLG_CRED_CANCELADO', data: '  ISNULL(' + FLG_ESTADO_CREDITO + ',FLG_ESTADO_CREDITO) ', op: " = " });
    rules.push({ field: 'ID_CLIENTE', data: '  ISNULL(' + ID_CLIENTE + ',ID_CLIENTE) ', op: " = " });
    rules.push({ field: 'CONVERT(DATE,FEC_CREACION,103)', data: 'CONVERT(DATE,\'' + FECHA_VENTA + '\',103) ', op: " = " });
    rules.push({ field: 'UPPER(COD_COMPROBANTE)', data: POR + ' + ' + CODIGO_VENTA + ' + ' + POR, op: " LIKE " });
    rules.push({ field: 'UPPER(ID_SUCURSAL)', data: ID_SUCURSAL, op: " = " });
    rules.push({ field: 'UPPER(FLG_TIPO_VENTA)', data: 1, op: " = " }); // CREDITO

    return rules;
}



function CuentasCobrar_actionAcciones(cellvalue, options, rowObject) {
    var _ID_VENTA = rowObject[2];
    var _FLG_FLG_ANULADO = rowObject[10];
    var _btn_Anular = "";
    if (_FLG_FLG_ANULADO == 0)
        _btn_Anular = " <a class=\"dropdown-item\" onclick='CuentasCobrar_AnularVenta(" + _ID_VENTA + ")'><i class=\"bi bi-bag-x\" style=\"color:red;\"></i>&nbsp;  Anular</a>";


    var _btn = "<div class=\"btn-group\" role=\"group\" title=\"Acciones \" >" +
           " <button  style=\" background: transparent; border: none; color: #000000;font-size: 18px;\" type=\"button\" class=\"btn btn-primary dropdown-toggle\" data-toggle=\"dropdown\" aria-expanded=\"false\"><i class=\"bi bi-list\"></i></button> " +
           " <div class=\"dropdown-menu\" x-placement=\"bottom-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 35px, 0px);\">" +
            _btn_Anular +
            "<a class=\"dropdown-item\" onclick='CuentasCobrar_MostrarDevolverProducto(" + _ID_VENTA + ")' ><i class=\"bi bi-box-arrow-in-down-left\" style=\"color:green;\"></i>&nbsp;  Devolver Producto</a>" +
            "</div>" +
        "</div>";
    return _btn;
}

function CuentasCobrar_FormaterComprobante(cellvalue, options, rowObject) {
    var _DESC_COMPROBANTE = rowObject[4];
    var _DESC_COD_COMPROBANTE = rowObject[13];
    var _text = "";
    var _text = '<span>' + _DESC_COMPROBANTE + '</span><br><span style="font-size: 12px; color: #2c7be5;"><i class="bi bi-upc"></i>&nbsp;Código: ' + _DESC_COD_COMPROBANTE + '</span>';
    return _text;
}

function CuentasCobrar_Anulado(cellvalue, options, rowObject) {
    var _FLG_ANULADO = rowObject[10];
    var _text = "";
    if (_FLG_ANULADO == 1) {
        _text = "<span class=\"badge badge-danger \" data-bs-toggle=\"tooltip\" title=\"Esta venta fue anulada.\">Anulado</span>";
    }
    else if (_FLG_ANULADO == 0) {
        _text = "<span class=\"badge badge-success\" data-bs-toggle=\"tooltip\" title=\"Venta Realiazada\">Realizado</span>";
    }
    return _text;
}

function CuentasCobrar_TipoVenta(cellvalue, options, rowObject) {
    var _FLG_TIPO_VENTA = rowObject[11];
    var _text = "";
    if (_FLG_TIPO_VENTA == 1) {
        _text = "<span class=\"badge badge-warning \" data-bs-toggle=\"tooltip\" title=\"Esta venta fue es al credito.\">Credito</span>";
    }
    else if (_FLG_TIPO_VENTA == 0) {
        _text = "Al Contado"
    }
    return _text;
}

//function CuentasCobrar_actionEditar(cellvalue, options, rowObject) {
//    var _btn = "<button title='Editar'  onclick='CuentasCobrar_MostrarEditar(" + rowObject.ID_VENTA + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\" data-target='#myModalNuevo'> <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:17px\"></i></button>";
//    return _btn;
//}

function CuentasCobrar_MostarBuscarProducto() {
    var _ID_SUCURSAL = $('#inputL_Id_Sucursal').val();
    jQuery("#myModalBuscarProduc").html('');
    jQuery("#myModalBuscarProduc").load(baseUrl + "CuentasCobrar/CuentasCobrar/Mantenimiento_BuscarProducto?ID_SUCURSAL=" + _ID_SUCURSAL + "&GrillaCarga=" + CuentasCobrar_Detalle_Grilla, function (responseText, textStatus, request) {
        $('#myModalBuscarProduc').modal({ show: true, backdrop: 'static', keyboard: false });
        $.validator.unobtrusive.parse('#myModalBuscarProduc');
        if (request.status != 200) return;
    });
}


function CuentasCobrar_MostrarDevolverProducto(ID_VENTA) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "CuentasCobrar/CuentasCobrar/Mantenimiento_DevolverProducto?ID_VENTA=" + ID_VENTA, function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


///*********************************************** ----------------- *************************************************/

///*********************************************** anular ventas  ***************************************************/

function CuentasCobrar_AnularVenta(ID_VENTA) {
    jConfirm("¿ Desea anular esta venta ?, al anular la venta todos los productos de la venta retornan.", "Anular Venta", function (r) {
        if (r) {
            var item = {
                ID_VENTA: ID_VENTA,
                USU_MODIFICACION: $('#input_hdcodusuario').val(),
            };
            var url = baseUrl + 'CuentasCobrar/CuentasCobrar/CuentasCobrar_AnularVenta';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        CuentasCobrar_CargarGrilla();
                        //CuentasCobrar_Cerrar();
                        jOkas("Venta anulada con exito!", "Proceso");
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

///*********************************************** ----------------- *************************************************/

///*********************************************** devolver producto ******************************************/

function CuentasCobrar_DevolverProducto(ID_VENTA_DETALLE) {
    jConfirm("¿ Desea devolver este producto ?, al devolver el producto retornara al almacen.", "Devolver Producto", function (r) {
        if (r) {
            var item = {
                ID_VENTA_DETALLE: ID_VENTA_DETALLE,
                USU_MODIFICACION: $('#input_hdcodusuario').val(),
            };
            var url = baseUrl + 'CuentasCobrar/CuentasCobrar/CuentasCobrar_Detalle_DevolverProducto';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        CuentasCobrar_ConfigurarGrilla();
                        CuentasCobrar_Detalle_CargarGrilla($('#hfd_ID_VENTA').val());
                        //CuentasCobrar_Cerrar();
                        jOkas("Producto devuelto con exito!, la cantidad se devolvio al almacen correspondiente.", "Proceso");
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

/////*********************************************** ----------------- ************************