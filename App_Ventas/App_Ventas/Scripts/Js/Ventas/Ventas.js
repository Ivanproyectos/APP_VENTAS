var Ventas_Grilla = 'Ventas_Grilla';
var Ventas_Barra = 'Ventas_Barra';
var Items_Motivo = ""; 

function Ventas_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Ventas_Limpiar() {
    $("#Ventas_CodigoVenta").val('');
    $('#ID_TIPO_COMPROBANTE_SEARCH').val('');
    $('#Ventas_FLG_TIPO_PAGO').val('');
    Ventas_ConfigurarGrilla();
}

function Ventas_ConfigurarGrilla() {
    var url = baseUrl + 'Ventas/Ventas/Ventas_Paginado';
    $("#" + Ventas_Grilla).GridUnload();
    var colNames = ['Acciones', 'Código', 'ID', 'Código Venta', 'Tipo Comprobante','Cliente','Descuento','Subtotal','Igv','Total','Estado Venta','Tipo Pago',
       'Fecha Venta','COD_COMPROBANTE','Flg_anulado','flg_tipoventa','flg_credito','Nro Operacion'];
    var colModels = [
            { name: 'ACCION', index: 'ACCION', align: 'center', width: 80, hidden: false, formatter: Ventas_actionAcciones, sortable: false}, // 0
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },// 1
            { name: 'ID_VENTA', index: 'ID_VENTA', width: 100, hidden: true, key: true }, // 2
            { name: 'COD_VENTA', index: 'COD_VENTA', width: 100, hidden: false, align: "left" }, // 3
            { name: 'TIPO_COMPROBANTE', index: 'TIPO_COMPROBANTE', width: 150, hidden: false, align: "left", formatter: Ventas_FormaterComprobante }, // 4
            { name: 'CLIENTE', index: 'CLIENTE', width: 250, hidden: false, align: "left" }, // 5
            { name: 'DESCUENTO', index: 'DESCUENTO', width: 100, hidden: false, align: "left" }, // 6
            { name: 'SUBTOTAL', index: 'SUBTOTAL', width: 100, hidden: false, align: "left" }, // 7
            { name: 'IGV', index: 'IGV', width: 100, hidden: false, align: "left" }, // 8
            { name: 'TOTAL', index: 'TOTAL', width: 100, hidden: false, align: "left", formatter: Ventas_FormatterTotal }, // 9       
            { name: 'DESC_ESTADO_VENTA', index: 'DESC_ESTADO_VENTA', width: 150, hidden: false, align: "left", formatter: Ventas_Anulado }, // 10
            { name: 'DESC_TIPO_VENTA', index: 'DESC_TIPO_VENTA', width: 150, hidden: false, align: "left", formatter: Ventas_TipoPago }, // 11
            { name: 'FEC_CREACION', index: 'FEC_CREACION', width: 150, hidden: false, align: "left" },//12
            { name: 'COD_COMPROBANTE', index: 'COD_COMPROBANTE', width: 150, hidden: true, align: "left" },//13
            { name: 'FLG_ANULADO', index: 'FLG_ANULADO', width: 150, hidden: true, align: "left" },//14
            { name: 'FLG_TIPO_PAGO', index: 'FLG_TIPO_PAGO', width: 150, hidden: true, align: "left" },//15
            { name: 'FLG_ESTADO_CREDITO', index: 'FLG_ESTADO_CREDITO', width: 150, hidden: true, align: "left" },//16
            { name: 'NRO_OPERACION', index: 'NRO_OPERACION', width: 150, hidden: true, align: "left" },//17
            
    ];
    var opciones = {
        GridLocal: false, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false,rules:true, rowNumber: 50, rowNumbers: [50, 100, 200, 300, 500],
    };
    SICA.Grilla(Ventas_Grilla, Ventas_Barra, Ventas_Grilla, 400, '', "Lista de Ventas", url, 'ID_VENTA', colNames, colModels, 'ID_VENTA', opciones);

    //jQuery("#" + Ventas_Grilla).jqGrid('setGroupHeaders', {
    //    useColSpanStyle: true,
    //    groupHeaders: [
    //        { startColumnName: 'TIPO_COMPROBANTE', align: 'center', numberOfColumns: 2, titleText: '<center id="PUB">Fecha de Publicación  <br/> Evaluaciones</center>' },
    //        { startColumnName: 'DESCUENTO', align: 'center', numberOfColumns: 4, titleText: '<center id="INI1" >Evaluación  <br/> Totales</center>' },

    //    ]
    //});
}

function GetRules(Ventas_Grilla) {
    var rules = new Array();
    var FECHA_VENTA = moment().format('DD/MM/YYYY');
    var ID_TIPO_COMPROBANTE = jQuery('#ID_TIPO_COMPROBANTE_SEARCH').val() == '' ? null : "'" + jQuery('#ID_TIPO_COMPROBANTE_SEARCH').val() + "'";
    var FLG_TIPO_PAGO = jQuery('#Ventas_FLG_TIPO_PAGO').val() == '' ? null : "'" + jQuery('#Ventas_FLG_TIPO_PAGO').val() + "'";
    var CODIGO_VENTA = "'" + jQuery('#Ventas_CodigoVenta').val() + "'";
    var _USUARIO_LOGEADO = "'" + jQuery('#input_hdcodusuario').val() + "'"; 

    var POR = "'%'";
    rules = []
    rules.push({ field: 'UPPER(COD_COMPROBANTE)', data: POR + ' + ' + CODIGO_VENTA + ' + ' + POR, op: " LIKE " });
    rules.push({ field: 'ID_TIPO_COMPROBANTE', data: '  ISNULL(' + ID_TIPO_COMPROBANTE + ',ID_TIPO_COMPROBANTE) ', op: " = " });
    rules.push({ field: 'FLG_TIPO_PAGO', data: '  ISNULL(' + FLG_TIPO_PAGO + ',FLG_TIPO_PAGO) ', op: " = " });
    rules.push({ field: 'CONVERT(DATE,FEC_CREACION,103)', data: 'CONVERT(DATE,\'' + FECHA_VENTA + '\',103) ', op: " = " });
    rules.push({ field: 'UPPER(COD_COMPROBANTE)', data: POR + ' + ' + CODIGO_VENTA + ' + ' + POR, op: " LIKE " });
    rules.push({ field: 'UPPER(USU_CREACION)', data: _USUARIO_LOGEADO, op: " = " });

    return rules;
}

function Ventas_actionAcciones(cellvalue, options, rowObject) {
    var _ID_VENTA = rowObject[2];
    var _FLG_FLG_ANULADO = rowObject[14];
    var _COD_COMPROBANTE = '"' + rowObject[13] + '"'; 
    var _btn_Anular =""; 
    var _btn_Devolver =""; 
    if (_FLG_FLG_ANULADO == 0) {
        _btn_Imprimir = "<a class=\"dropdown-item\" onclick='Ventas_ImprimirComprobante(" + _ID_VENTA + "," + _COD_COMPROBANTE + ")'><i class=\"bi bi-printer\" style=\"color:gray;\"></i>&nbsp;  Imprimir Comprobante</a>";
        _btn_Anular = "<a class=\"dropdown-item\" onclick='Ventas_AnularVenta(" + _ID_VENTA + ")'><i class=\"bi bi-cart-x\" style=\"color:red;\"></i>&nbsp;  Anular Venta</a>";
        _btn_Devolver ="<a class=\"dropdown-item\" onclick='Ventas_MostrarDevolverProducto(" + _ID_VENTA + ")' ><i class=\"bi bi-box-arrow-in-down-left\" style=\"color:green;\"></i>&nbsp;  Devolver Producto</a>" ; 
    }
    var _btn = "<div class=\"btn-group Group_Acciones\" role=\"group\" title=\"Acciones \" >" +
           "<button  style=\" background: transparent; border: none; color: #000000;font-size: 18px;\" type=\"button\" class=\"btn btn-primary dropdown-toggle\" data-toggle=\"dropdown\" aria-expanded=\"false\"><i class=\"bi bi-list\"></i></button>" +
           "<div class=\"dropdown-menu\" x-placement=\"bottom-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 35px, 0px);\">" +
           "<a class=\"dropdown-item\" onclick='Ventas_ViewDetalleVenta(" + _ID_VENTA + ")'><i class=\"bi bi-stickies\" style=\"color:#2c7be5\"></i>&nbsp;  Detalle Venta</a>" +
           _btn_Imprimir + 
           _btn_Anular +
            _btn_Devolver + 
            "</div>" +
        "</div>"; 
    return _btn;
}

function Ventas_FormaterComprobante(cellvalue, options, rowObject) {
    var _DESC_COMPROBANTE = rowObject[4];
    var _DESC_COD_COMPROBANTE = rowObject[13];
    var _text = "";
    var _text = '<span>' + _DESC_COMPROBANTE + '</span><br><span style="font-size: 12px; color: #2c7be5;"><i class="bi bi-upc"></i>&nbsp;Código: ' + _DESC_COD_COMPROBANTE + '</span>';
    return _text;
}

function Ventas_Anulado(cellvalue, options, rowObject) {
    var _DESC_ESTADO_VENTA = rowObject[10];
    var _FLG_ANULADO = rowObject[14];
    var _text = "";
    if (_FLG_ANULADO == 1) {
        _text = "<span class=\"badge badge-danger \" data-bs-toggle=\"tooltip\" title=\"Esta venta fue anulada.\">" + _DESC_ESTADO_VENTA + "</span>";
    }
    else if (_FLG_ANULADO == 0) {
        _text = "<span class=\"badge badge-success\" data-bs-toggle=\"tooltip\" title=\"Venta Realiazada\">"+ _DESC_ESTADO_VENTA +"</span>";
    }
    return _text;
}

function Ventas_TipoPago(cellvalue, options, rowObject) {
    var _DESC_TIPO_VENTA = rowObject[11];
    var _FLG_TIPO_PAGO = rowObject[15];
    var _NRO_OPERACION = rowObject[17];
    var _text = "";
    if (_FLG_TIPO_PAGO == 2) {
        _text = "<span class=\"badge badge-warning \" data-bs-toggle=\"tooltip\" title=\"Esta venta fue es al credito.\">"+_DESC_TIPO_VENTA+"</span>";
    }
    else if (_FLG_TIPO_PAGO == 1) {
        _text = _DESC_TIPO_VENTA; 
    } else if (_FLG_TIPO_PAGO == 3) {
        _text = '<span>' + _DESC_TIPO_VENTA + '</span><br><span style="font-size: 12px; color: #2c7be5;"><i class="bi bi-credit-card"></i>&nbsp;Nro. Operación: ' + _NRO_OPERACION + '</span>';;
    }
    return _text;
}


function Ventas_FormatterTotal(cellvalue, options, rowObject) {
    var _TOTAL = rowObject[9];
    var _text = _SimboloMoneda + " " +_TOTAL ;
    return _text;
}


function Ventas_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='Ventas_Eliminar(" + rowObject.ID_VENTA + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
}


function Ventas_MostrarNuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Ventas/Ventas/Mantenimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}



function Ventas_MostarBuscarProducto() {
    var ID_SUCURSAL = _Id_Sucursal; 
    jQuery("#myModalBuscarProduc").html('');
    jQuery("#myModalBuscarProduc").load(baseUrl + "Ventas/Ventas/Mantenimiento_BuscarProducto?ID_SUCURSAL=" + ID_SUCURSAL + "&ID_PRODUCTO=0&PRECIO=0&IMPORTE=0&_CANTIDAD=0&Accion=N",
        function (responseText, textStatus, request) {
        $('#myModalBuscarProduc').modal({ show: true, backdrop: 'static', keyboard: false });
        $.validator.unobtrusive.parse('#myModalBuscarProduc');
        if (request.status != 200) return;
    });
}


function Ventas_MostrarDevolverProducto(ID_VENTA) {
    var _TIPO_DETALLE ="DEVOLVER"; 
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Ventas/Ventas/Mantenimiento_ViewDetalleProducto?ID_VENTA=" + ID_VENTA + "&TIPO=" + _TIPO_DETALLE, function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function Ventas_ViewDetalleVenta(ID_VENTA) {
    var _TIPO_DETALLE ="DETALLE"; 
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Ventas/Ventas/Mantenimiento_ViewDetalleProducto?ID_VENTA=" + ID_VENTA + "&TIPO=" +_TIPO_DETALLE, function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


///*********************************************** ----------------- *************************************************/

///************************************************ Inserta ventas  **************************************************/

function Ventas_Ingresar() {
    if ($("#frmMantenimiento_Ventas").valid() && $("#frmMantenimiento_DetalleVenta").valid()) {
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
                    jConfirm("¿ Desea realizar este venta ?", "Atención", function (r) {
                        if (r) {
                            debugger;
                            var item =
                                {
                                    ID_TIPO_COMPROBANTE: $("#ID_TIPO_COMPROBANTE").val(),
                                    FECHA_VENTA: $("#FECHA_VENTA").val(),
                                    ID_CLIENTE: $("#ID_CLIENTE").val(),
                                    ID_SUCURSAL: $("#inputL_Id_Sucursal").val(),
                                    NRO_OPERACION: $("#NRO_OPERACION").val(),
                                    FLG_TIPO_PAGO: $("#FLG_TIPO_PAGO").val(),
                                    FLG_CREDITO_PENDIENTE: _FLG_CREDITO_PENDIENTE,
                                    ID_VENTA_CREDITO: _ID_VENTA_CREDITO,
                                    DESCUENTO: parseFloat($("#Venta_Descuento").text()),
                                    SUB_TOTAL: parseFloat($("#Venta_Subtotal").text()),
                                    IGV: parseFloat($("#Venta_Igv").text()),
                                    TOTAL: parseFloat($("#Venta_Total").text()),
                                    ADELANTO: parseFloat($("#ADELANTO").val()),
                                    DETALLE: $("#DETALLE_VENTA").val(),
                                    ListaDetalle: ListaDetalle,
                                    USU_CREACION: $('#input_hdcodusuario').val(),
                                    ACCION: $("#AccionVentas").val()
                                };
                            var url = baseUrl + 'Ventas/Ventas/Ventas_Insertar';
                            var auditoria = SICA.Ajax(url, item, false);
                            if (auditoria != null && auditoria != "") {
                                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                                    if (!auditoria.RECHAZAR) {
                                        Ventas_ConfigurarGrilla();
                                        Ventas_Cerrar();
                                        //jOkas("Ventas registrado satisfactoriamente", "Proceso"); 
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

            } else {
                jError("La lista de productos no puede estar vacia.", "Atención");
            }
        }
}



// CALCULAR VUELTO VENTA
function Fn_Ventas_Vuelto() {
    var _Total = isNaN(parseFloat($('#Venta_Total').text())) ? 0 : parseFloat($('#Venta_Total').text());
    var _PagoCon = isNaN(parseFloat($('#TOTAL_RECIBIDO').val())) ? 0 : parseFloat($('#TOTAL_RECIBIDO').val());
    var _Vuelto = (_PagoCon - _Total);
    if (_Vuelto < 0)
        _Vuelto = 0;
    $('#VUELTO').val(Number(_Vuelto).toFixed(2));
}

