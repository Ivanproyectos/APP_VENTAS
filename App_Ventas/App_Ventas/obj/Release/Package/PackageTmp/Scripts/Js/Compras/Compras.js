var Compras_Grilla = 'Compras_Grilla';
var Compras_Barra = 'Compras_Barra';

function Compras_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Compras_Limpiar() {
    $("#Compras_CodigoComprobante").val('');
    $('#ID_PROVEEDOR_SEARCH').val('');
    $('#Compras_FLG_ANULADO').val('');
    $('#Compras_FechaInicio').val('');
    $('#Compras_FechaFin').val('');
    Compras_ConfigurarGrilla();
}

function Compras_ConfigurarGrilla() {
    var url = baseUrl + 'Compras/Compras/Compras_Paginado';
    $("#" + Compras_Grilla).GridUnload();
    var colNames = ['Acciones', 'Código', 'Código Compra', 'Código Comprobante', 'Fecha Compra', 'Tipo Comprobante', 'Proveedor',
        'Descuento', 'Subtotal', 'Igv', 'Total', 'Tipo Pago','Estado Compra',
        'Detalle', 'Fecha Creación', 'Usuario Creación', 'Flg_Anulado', 'Nro Operacion','Id_TipoPago','Fec_Anulado'];
    var colModels = [
            { name: 'ACCIONES', index: 'ACCIONES', align: 'center', width: 80, hidden: false, formatter: Compras_Acciones, sortable: false }, //0
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, }, //1
            { name: 'ID_COMPRA', index: 'ID_COMPRA', width: 100, hidden: true, key: true },//2
            { name: 'COD_DOCUMENTO', index: 'COD_DOCUMENTO', width: 150, hidden: true, align: "left" }, //3
            { name: 'FCHA_DOCUMENTO', index: 'FCHA_DOCUMENTO', width: 120, hidden: false, align: "left" }, //4
            { name: 'TIPO_COMPROBANTE', index: 'TIPO_COMPROBANTE', width: 200, hidden: false, align: "left", formatter:Compras_FormaterComprobante }, //5
            { name: 'PROVEEDOR', index: 'PROVEEDOR', width: 200, hidden: false, align: "left" },  //6
            { name: 'DESCUENTO', index: 'DESCUENTO', width: 100, hidden: false, align: "left" }, //7
            { name: 'SUBTOTAL', index: 'SUBTOTAL', width: 100, hidden: false, align: "left" },  //8
            { name: 'IGV', index: 'IGV', width: 100, hidden: false, align: "left" }, //9
            { name: 'TOTAL', index: 'TOTAL', width: 100, hidden: false, align: "left" }, //10
            { name: 'DESC_TIPO_PAGO', index: 'DESC_TIPO_PAGO', width: 150, hidden: false, align: "left", formatter: Compras_FormatterTipoPago }, //11
            { name: 'DESC_ESTADO_COMPRA', index: 'DESC_ESTADO_COMPRA', width: 150, hidden: false, align: "left", formatter: Compras_FormatterEstadoCompra }, //12
            { name: 'DETALLE', index: 'DETALLE', width: 300, hidden: false, align: "left" }, //13
            { name: 'FEC_CREACION', index: 'FEC_CREACION', width: 150, hidden: false, align: "left" }, //14
            { name: 'USU_CREACION', index: 'USU_CREACION', width: 150, hidden: false, align: "left" }, //15
            { name: 'FLG_ANULADO', index: 'FLG_ANULADO', width: 150, hidden: true, align: "left" }, //16
            { name: 'NRO_OPERACION', index: 'NRO_OPERACION', width: 150, hidden: true, align: "left" }, //17
            { name: 'ID_TIPO_PAGO', index: 'ID_TIPO_PAGO', width: 150, hidden: true, align: "left" }, //18
            { name: 'FECHA_ANULADO', index: 'FECHA_ANULADO', width: 150, hidden: true, align: "left" }, //19
    ];
    var opciones = {
        GridLocal: false, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, rules: true, rowNumber: 50, rowNumbers: [50, 100, 200, 300, 500],
    };
    SICA.Grilla(Compras_Grilla, Compras_Barra, '', 400, '', "Lista de Compras", url, 'ID_COMPRA', colNames, colModels, 'ID_COMPRA', opciones);
}

function GetRules(Compras_Grilla) {
    var rules = new Array();
    //var FECHA_COMPRA = jQuery('#ID_PROVEEDOR_SEARCH').val() == '' ? null : "'" + jQuery('#ID_PROVEEDOR_SEARCH').val() + "'";
    var ID_PROVEEDOR = jQuery('#ID_PROVEEDOR_SEARCH').val() == '' ? null : "'" + jQuery('#ID_PROVEEDOR_SEARCH').val() + "'";
    var FLG_ANULADO = jQuery('#Compras_FLG_ANULADO').val() == '' ? null : "'" + jQuery('#Compras_FLG_ANULADO').val() + "'";
    var FECHA_INICIO = jQuery('#Compras_FechaInicio').val() == '' ? null : "'" + jQuery('#Compras_FechaInicio').val() + "'";
    var FECHA_FIN = jQuery('#Compras_FechaFin').val() == '' ? null : "'" + jQuery('#Compras_FechaFin').val() + "'";
    var CODIGO_COMPROBANTE = "'" + jQuery('#Compras_CodigoComprobante').val() + "'";
    var ID_SUCURSAL = jQuery('#ID_SUCURSAL').val() == '' ? null : "'" + jQuery('#ID_SUCURSAL').val() + "'";

    var POR = "'%'";
    rules = []
    rules.push({ field: 'UPPER(COD_COMPROBANTE)', data: POR + ' + ' + CODIGO_COMPROBANTE + ' + ' + POR, op: " LIKE " });
    rules.push({ field: 'FLG_ANULADO', data: '  ISNULL(' + FLG_ANULADO + ',FLG_ANULADO) ', op: " = " });
    rules.push({ field: 'CONVERT(DATE,FECHA_COMPROBANTE,103)', data: 'CONVERT(DATE,ISNULL(' + FECHA_INICIO + ',FECHA_COMPROBANTE),103)  AND CONVERT(DATE,ISNULL(' + FECHA_FIN + ',FECHA_COMPROBANTE),103)  ', op: " BETWEEN " });
    rules.push({ field: 'ID_SUCURSAL', data: '  ISNULL(' + ID_SUCURSAL + ',ID_SUCURSAL) ', op: " = " });
    return rules;
}

function Compras_Acciones(cellvalue, options, rowObject) {
    var _ID_COMPRA= rowObject[2];
    var _FLG_FLG_ANULADO = rowObject[16];
    var _btn_Anular = "";
    if (_FLG_FLG_ANULADO == 0) {
        _btn_Anular = "<a class=\"dropdown-item\" onclick='Compras_AnularVenta(" + _ID_COMPRA + ")'><i class=\"bi bi-basket-fill\" style=\"color:red;\"></i>&nbsp;  Anular Venta</a>";
    }
    var _btn = "<div class=\"btn-group Group_Acciones\" role=\"group\" title=\"Acciones \" >" +
           "<button  style=\" background: transparent; border: none; color: #000000;font-size: 18px;\" type=\"button\" class=\"btn btn-primary dropdown-toggle\" data-toggle=\"dropdown\" aria-expanded=\"false\"><i class=\"bi bi-list\"></i></button>" +
           "<div class=\"dropdown-menu\" x-placement=\"bottom-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 35px, 0px);\">" +
           "<a class=\"dropdown-item\" onclick='Compras_ViewDetalleCompra(" + _ID_COMPRA + ")'><i class=\"bi bi-stickies\" style=\"color:#2c7be5\"></i>&nbsp;  Detalle Compra</a>" +
           _btn_Anular +
            "</div>" +
        "</div>";
    return _btn;
}

function Compras_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='Compras_Eliminar(" + rowObject.ID_COMPRA + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
}

function Compras_FormaterComprobante(cellvalue, options, rowObject) {
    var _DESC_COMPROBANTE = rowObject[5];
    var _DESC_COD_COMPROBANTE = rowObject[3];
    var _text = "";
    var _text = '<span>' + _DESC_COMPROBANTE + '</span><br><span style="font-size: 12px; color: #2c7be5;"><i class="bi bi-upc"></i>&nbsp;Código: ' + _DESC_COD_COMPROBANTE + '</span>';
    return _text;
}

function Compras_FormatterTipoPago(cellvalue, options, rowObject) {
    var _DESC_TIPO_COMPRA = rowObject[11];
    var _ID_TIPO_PAGO = rowObject[18];
    var _NRO_OPERACION = rowObject[17];
    var _text = "";
    if (_ID_TIPO_PAGO == 2) {
        _text = "<span class=\"badge badge-warning \" data-bs-toggle=\"tooltip\" title=\"Esta venta fue es al credito.\">" + _DESC_TIPO_COMPRA + "</span>";
    }
    else if (_ID_TIPO_PAGO == 1) {
        _text = _DESC_TIPO_COMPRA;
    } else if (_ID_TIPO_PAGO == 3 || _ID_TIPO_PAGO == 4) {
        _text = '<span>' + _DESC_TIPO_COMPRA + '</span><br><span style="font-size: 12px; color: #2c7be5;"><i class="bi bi-credit-card"></i>&nbsp;Nro. Operación: ' + _NRO_OPERACION + '</span>';;
    }
    return _text;
}

function Compras_FormatterEstadoCompra(cellvalue, options, rowObject) {
    var _DESC_ESTADO_COMPRA = rowObject[12];
    var _FLG_ANULADO = rowObject[16];
    var _FECHA_ANULADO = rowObject[19];

    var _text = "";
    if (_FLG_ANULADO == 1) {
        _text = "<span class=\"badge badge-danger \" data-bs-toggle=\"tooltip\" title=\"Esta venta fue anulada.\">" + _DESC_ESTADO_COMPRA + "</span>";
    }
    else if (_FLG_ANULADO == 0) {
        _text = "<span class=\"badge badge-success\" data-bs-toggle=\"tooltip\" title=\"Venta Realiazada\">" + _DESC_ESTADO_COMPRA + "</span>";
    }
    return _text;
}


function Compras_MostrarNuevo() {
    var _ID_SUCURSAL = $('#ID_SUCURSAL').val();
    var _DESC_SUCURSAL = $('select[name="ID_SUCURSAL"] option:selected').text(); 
    if (_ID_SUCURSAL != "") {
        _DESC_SUCURSAL = _DESC_SUCURSAL.replace(/ /g, "+");
        jQuery("#myModalNuevo").html('');
        jQuery("#myModalNuevo").load(baseUrl + "Compras/Compras/Mantenimiento?ID_SUCURSAL=" + _ID_SUCURSAL +
            "&DESC_SUCURSAL=" + _DESC_SUCURSAL, function (responseText, textStatus, request) {
                $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
            $.validator.unobtrusive.parse('#myModalNuevo');
            if (request.status != 200) return;
        });
    } else {
        jInfo('Para registrar una compra selecione el almacen donde se registrara esta nueva compra.', 'Atención')
    }
}

function Compras_ViewBuscarProducto() {
    var ID_SUCURSAL = _Id_Sucursal;
    jQuery("#myModalBuscarProduc").html('');
    jQuery("#myModalBuscarProduc").load(baseUrl + "Compras/Compras/View_BuscarProducto?ID_SUCURSAL=" + ID_SUCURSAL + "&ID_PRODUCTO=0&PRECIO=0&IMPORTE=0&_CANTIDAD=0&Accion=N&TIPO_PROCESO=COMPRAS",
        function (responseText, textStatus, request) {
            $('#myModalBuscarProduc').modal({ show: true, backdrop: 'static', keyboard: false });
            $.validator.unobtrusive.parse('#myModalBuscarProduc');
            if (request.status != 200) return;
        });
}

function Compras_MostrarEditar(ID_COMPRA) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Compras/Compras/Mantenimiento?id=" + ID_COMPRA + "&Accion=M", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


function Compras_ViewDetalleCompra(ID_COMPRA) {
    var _TIPO_DETALLE = "DETALLE";
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Compras/Compras/View_DetalleCompra?ID_COMPRA=" + ID_COMPRA + "&TIPO=" + _TIPO_DETALLE, function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


///*********************************************** ----------------- *************************************************/

///************************************************ Inserta cargos  **************************************************/

function Compras_Ingresar() {
    if ($("#frmMantenimiento_Compras").valid() && $("#frmMantenimiento_DetalleCompras").valid()) {
        var ListaDetalleProductos = new Array();
        var ListaDetalle = new Array();
        ListaDetalleProductos = $("#" + Ventas_Detalle_Grilla).jqGrid('getGridParam', 'data');
        for (var i = 0; i < ListaDetalleProductos.length; i++) {
            var rowData = ListaDetalleProductos[i];
            var _CANTIDAD = rowData.CANTIDAD;
            if (rowData.ID_UNIDAD_MEDIDA == 1) { // si es kilos convertir a gramos
                _CANTIDAD = ConvertKilos_Gramos(_CANTIDAD);
            }
            var myData =
            {
                ID_PRODUCTO: rowData.ID_PRODUCTO,
                PRECIO: parseFloat(rowData.PRECIO),
                CANTIDAD: _CANTIDAD,
                IMPORTE: parseFloat(rowData.IMPORTE),
            };
            ListaDetalle.push(myData);
        }
        if (ListaDetalle.length > 0) {
            jConfirm("¿ Desea registrar esta compra ?", "Atención", function (r) {
                if (r) {
                    debugger;
                    var item =
                        {
                            ID_TIPO_COMPROBANTE: $("#ID_TIPO_COMPROBANTE").val(),
                            COD_COMPROBANTE: $("#COD_COMPROBANTE").val(),
                            FECHA_COMPROBANTE: $("#FECHA_COMPRA").val(),
                            ID_PROVEEDOR: $("#ID_PROVEEDOR").val(),
                            ID_SUCURSAL: $("#ID_SUCURSAL").val(),
                            NRO_OPERACION: $("#NRO_OPERACION").val(),
                            ID_TIPO_PAGO: $("#ID_TIPO_PAGO").val(),
                            DESCUENTO: parseFloat($("#Venta_Descuento").text()),
                            SUB_TOTAL: parseFloat($("#Venta_Subtotal").text()),
                            IGV: parseFloat($("#Venta_Igv").text()),
                            TOTAL: parseFloat($("#Venta_Total").text()),
                            DETALLE: $("#DETALLE_VENTA").val(),
                            ListaDetalle: ListaDetalle,
                            USU_CREACION: $('#input_hdcodusuario').val(),
                            ACCION: $("#AccionCompras").val()
                        };
                    var url = baseUrl + 'Compras/Compras/Compras_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Compras_ConfigurarGrilla();
                                Compras_Cerrar();
                                jOkas("Compra registrado correctamente", "Proceso"); 
                            } else {
                                jError(auditoria.MENSAJE_SALIDA, "Atención");
                            }
                        } else {
                            jError(auditoria.MENSAJE_SALIDA, "Atención");
                        }
                    }
                }
            });

        } else {
            jError("La lista de productos no puede estar vacia.", "Atención");
        }
    }
}

///*********************************************** ----------------- *************************************************/

///*********************************************** Elimina cargos  ***************************************************/


function Compras_Detalle_CargarGrilla(ID_COMPRA) {
    var item =
       {
           ID_COMPRA: ID_COMPRA,
       };
    var url = baseUrl + 'Compras/Compras/Compras_Detalle_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    jQuery("#" + Ventas_Detalle_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;
                var _Cantidad = v.CANTIDAD;
                if (v.ID_UNIDAD_MEDIDA == 1) // gramos a kilos 
                {
                    _Cantidad = ConvertGramos_Kilos(_Cantidad);
                }
                var myData =
                 {
                     CODIGO: idgrilla,
                     ID_VENTA_DETALLE: v.ID_VENTA_DETALLE,
                     ID_PRODUCTO: v.ID_PRODUCTO,
                     PRODUCTO: v.DESC_PRODUCTO,
                     PRECIO: Number(v.PRECIO).toFixed(2),
                     CANTIDAD: _Cantidad,
                     IMPORTE: Number(v.IMPORTE).toFixed(2),
                     FLG_DEVUELTO: v.FLG_DEVUELTO,
                     COD_UNIDAD_MEDIDA: v.COD_UNIDAD_MEDIDA,
                     ID_UNIDAD_MEDIDA: v.ID_UNIDAD_MEDIDA,
                     ACCION: "M"
                 };
                jQuery("#" + Ventas_Detalle_Grilla).jqGrid('addRowData', idgrilla, myData);
            });
            jQuery("#" + Ventas_Detalle_Grilla).trigger("reloadGrid");
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}
