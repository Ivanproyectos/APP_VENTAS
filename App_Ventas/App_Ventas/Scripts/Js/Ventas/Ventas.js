var Ventas_Grilla = 'Ventas_Grilla';
var Ventas_Barra = 'Ventas_Barra';
var Items_Motivo = "";
var Modulo = "";

function Ventas_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Ventas_ConfigurarGrilla(_Modulo) {
    Modulo = _Modulo; 
    var url = baseUrl + 'Ventas/Ventas/Ventas_Paginado';
    DataTable.GridUnload(Ventas_Grilla);
    var colModels = [
          { data: "ID_VENTA", name: "ID_VENTA", title: "ID_VENTA", autoWidth: false, visible: false, },
          {
              data: null, name: "TIPO_COMPROBANTE", title: "Comprobante", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Ventas_FormaterComprobante(data.DESC_TIPO_COMPROBANTE, data.COD_COMPROBANTE); }
           },
          { data: "Cliente.NOMBRES_APE", name: "CLIENTE", title: "Cliente", autoWidth: true},
          {
              data: null, name: "DESCUENTO", title: "Descuento", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Ventas_FormatterMoneda(data.DESCUENTO); }
          },
          {
              data: null, name: "SUBTOTAL", title: "Subtotal", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Ventas_FormatterMoneda(data.SUB_TOTAL); }
          },
          {
              data: null, name: "IGV", title: "Igv", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Ventas_FormatterMoneda(data.IGV); }
          },
          {
            data: null, name: "TOTAL", title: "Total", autoWidth: true, sortable: false,
            render: function (data, type, row, meta) { return Ventas_FormatterMoneda(data.TOTAL); }
          },
          {
            data: null, name: "DESC_ESTADO_VENTA", title: "Estado Venta", autoWidth: true, sortable: false,
            render: function (data, type, row, meta) { return Ventas_Anulado(data.DESC_ESTADO_VENTA, data.FLG_ANULADO); }
          },
          {   
             data: null, name: "DESC_TIPO_VENTA", title: "Tipo Pago", autoWidth: true, sortable: false,
             render: function (data, type, row, meta) { return Ventas_TipoPago(data.DESC_TIPO_PAGO, data.ID_TIPO_PAGO, data.NRO_OPERACION); }
          },
          {
            data: null, sortable: false, title: "Acciones", width: "60px",
            render: function (data, type, row, meta) { return Ventas_actionAcciones(data.ID_VENTA, data.FLG_ANULADO, data.COD_COMPROBANTE); }
          },

    ];
    var opciones = {
        GridLocal: false, multiselect: false, sort: "desc", enumerable: true,
        eliminar: false, search: true, rowNumber: 10, rowNumbers: [10, 25, 50], rules: true, responsive: true, processing: true
    };
    DataTable.Grilla(Ventas_Grilla, url, 'ID_VENTA', colModels, opciones, "ID_VENTA");
}

function GetRules(Ventas_Grilla) {
    var rules = new Array();
    var SearchFields = new Array();
    var POR = "'%'";

    rules = []
    if (Modulo == "Ventas") {
        var FECHA_VENTA = moment().format('DD/MM/YYYY');
        var ID_TIPO_COMPROBANTE = jQuery('#ID_TIPO_COMPROBANTE_SEARCH').val() == '' ? null : "'" + jQuery('#ID_TIPO_COMPROBANTE_SEARCH').val() + "'";
        var ID_TIPO_PAGO = jQuery('#ID_TIPO_PAGO_SEARCH').val() == '' ? null : "'" + jQuery('#ID_TIPO_PAGO_SEARCH').val() + "'";
        var CODIGO_VENTA = "'" + jQuery('#Ventas_CodigoVenta').val() + "'";
        var _USUARIO_LOGEADO = "'" + jQuery('#input_hdcodusuario').val() + "'";


        rules.push({ field: 'UPPER(V.COD_COMPROBANTE)', data: POR + ' + ' + CODIGO_VENTA + ' + ' + POR, op: " LIKE " });
        rules.push({ field: 'V.ID_TIPO_COMPROBANTE', data: '  ISNULL(' + ID_TIPO_COMPROBANTE + ',V.ID_TIPO_COMPROBANTE) ', op: " = " });
        rules.push({ field: 'V.ID_TIPO_PAGO', data: '  ISNULL(' + ID_TIPO_PAGO + ',V.ID_TIPO_PAGO) ', op: " = " });
        rules.push({ field: 'CONVERT(DATE,V.FEC_CREACION,103)', data: 'CONVERT(DATE,\'' + FECHA_VENTA + '\',103) ', op: " = " });
        rules.push({ field: 'UPPER(V.USU_CREACION)', data: _USUARIO_LOGEADO, op: " = " });

    } else if (Modulo == "Consulta_Venta") {
        var FECHA_INICIO = jQuery('#Ventas_FechaRange').val() == '' ? null : "'" + jQuery('#Ventas_FechaRange').val().split('-')[0].trim() + "'";
        var FECHA_FIN = jQuery('#Ventas_FechaRange').val() == '' ? null : "'" + jQuery('#Ventas_FechaRange').val().split('-')[1].trim() + "'";
        var _USUARIO = jQuery('#ID_USUARIO').val() == '' ? null : "'" + jQuery('#ID_USUARIO').val() + "'";
        var _ID_SUCURSAL = jQuery('#ID_SUCURSAL').val() == '' ? null : "'" + jQuery('#ID_SUCURSAL').val() + "'";
        var _FLG_ANULADO = jQuery('#Ventas_FLG_ANULADO').val() == '' ? null : "'" + jQuery('#Ventas_FLG_ANULADO').val() + "'";

        rules.push({ field: 'V.USU_CREACION', data: '  ISNULL(' + _USUARIO + ',V.USU_CREACION) ', op: " = " });
        rules.push({ field: 'V.ID_SUCURSAL', data: '  ISNULL(' + _ID_SUCURSAL + ',V.ID_SUCURSAL) ', op: " = " });
        rules.push({ field: 'V.FLG_ANULADO', data: '  ISNULL(' + _FLG_ANULADO + ',V.FLG_ANULADO) ', op: " = " });
        rules.push({ field: 'CONVERT(DATE,V.FEC_CREACION,103)', data: 'CONVERT(DATE,ISNULL(' + FECHA_INICIO + ',V.FEC_CREACION),103)  AND CONVERT(DATE,ISNULL(' + FECHA_FIN + ',V.FEC_CREACION),103)  ', op: " BETWEEN " });
    }

    SearchFields.push({ field: 'UPPER(V.CLIENTE)' });
    SearchFields.push({ field: 'UPPER(V.COD_COMPROBANTE)' });
    SearchFields.push({ field: 'UPPER(V.DESC_TIPO_PAGO)' });
    SearchFields.push({ field: 'UPPER(V.DESC_TIPO_COMPROBANTE)' });

    var ObjectRules = {
        SearchFields: SearchFields,
        rules: rules
    }

    return ObjectRules;
}

function Ventas_actionAcciones(ID_VENTA, FLG_ANULADO, COD_COMPROBANTE) {
    var _ID_VENTA = ID_VENTA;
    var _FLG_FLG_ANULADO = FLG_ANULADO;
    var _COD_COMPROBANTE = '"' + COD_COMPROBANTE + '"';
    var _btn_Anular =""; 
    var _btn_Devolver = "";

    if (_FLG_FLG_ANULADO == 0) {
        _btn_Imprimir = "<a class=\"dropdown-item\" onclick='Ventas_ImprimirComprobante(" + _ID_VENTA + "," + _COD_COMPROBANTE + ")'><i class=\"bi bi-printer\" style=\"color:gray;\"></i>&nbsp;  Imprimir Comprobante</a>";
        _btn_Anular = "<a class=\"dropdown-item\" onclick='Ventas_AnularVenta(" + _ID_VENTA + ")'><i class=\"bi bi-cart-x\" style=\"color:red;\"></i>&nbsp;  Anular Venta</a>";
        _btn_Devolver = "<a class=\"dropdown-item\" onclick=\"Ventas_MostrarDevolverProducto(" + _ID_VENTA + ")\" ><i class=\"bi bi-box-arrow-in-down-left\" style=\"color:green;\"></i>&nbsp;  Devolver Producto</a>";
    }
    var _btn = "<div class=\"btn-group Group_Acciones\" role=\"group\" title=\"Acciones \" >" +
           "<button  style=\" background: transparent; border: none; color: #000000;font-size: 18px;\" type=\"button\" class=\"btn  dropdown-toggle\" data-toggle=\"dropdown\" aria-expanded=\"false\"><i class=\"bi bi-list\"></i></button>" +
           "<div class=\"dropdown-menu\" x-placement=\"bottom-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 35px, 0px);\">" +
           "<a class=\"dropdown-item\" onclick='Ventas_ViewDetalleVenta(" + _ID_VENTA + ")'><i class=\"bi bi-stickies\" style=\"color:#2c7be5\"></i>&nbsp;  Detalle Venta</a>" +
           _btn_Imprimir + 
           _btn_Anular +
            _btn_Devolver + 
            "</div>" +
        "</div>"; 
    return _btn;
}

function Ventas_FormaterComprobante(TIPO_COMPROBANTE, COD_COMPROBANTE) {
    var _DESC_COMPROBANTE = TIPO_COMPROBANTE;
    var _DESC_COD_COMPROBANTE = COD_COMPROBANTE; 
    var _text = "";
    var _text = '<span>' + _DESC_COMPROBANTE + '</span><br><span style="font-size: 12px; color: #2c7be5;"><i class="bi bi-upc"></i>&nbsp;Nro: ' + _DESC_COD_COMPROBANTE + '</span>';
    return _text;
}

function Ventas_Anulado(DESC_ESTADO_VENTA, FLG_ANULADO) {
    var _DESC_ESTADO_VENTA = DESC_ESTADO_VENTA;
    var _FLG_ANULADO = FLG_ANULADO;
    var _text = "";
    if (_FLG_ANULADO == 1) {
        _text = "<span class=\"badge badge-danger \" data-bs-toggle=\"tooltip\" title=\"Esta venta fue anulada.\">" + _DESC_ESTADO_VENTA + "</span>";
    }
    else if (_FLG_ANULADO == 0) {
        _text = "<span class=\"badge badge-success\" data-bs-toggle=\"tooltip\" title=\"Venta Realiazada\">"+ _DESC_ESTADO_VENTA +"</span>";
    }
    return _text;
}

function Ventas_TipoPago(DESC_TIPO_VENTA, ID_TIPO_PAGO, NRO_OPERACION) {
    var _DESC_TIPO_VENTA = DESC_TIPO_VENTA;
    var _ID_TIPO_PAGO = ID_TIPO_PAGO;
    var _NRO_OPERACION = NRO_OPERACION;
    var _text = "";
    if (_ID_TIPO_PAGO == 2) {
        _text = "<span class=\"badge badge-warning \" data-bs-toggle=\"tooltip\" title=\"Esta venta fue es al credito.\">"+_DESC_TIPO_VENTA+"</span>";
    }
    else if (_ID_TIPO_PAGO == 1) {
        _text = _DESC_TIPO_VENTA; 
    } else if (_ID_TIPO_PAGO == 3) {
        _text = '<span>' + _DESC_TIPO_VENTA + '</span><br><span style="font-size: 12px; color: #2c7be5;"><i class="bi bi-credit-card"></i>&nbsp;Nro. Operación: ' + _NRO_OPERACION + '</span>';;
    }
    return _text;
}


function Ventas_FormatterMoneda(MONTO) {
    var _TOTAL = Number(MONTO).toFixed(2);
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
    jQuery("#myModalBuscarProduc").load(baseUrl + "Ventas/Ventas/View_BuscarProducto?ID_SUCURSAL=" + ID_SUCURSAL + "&ID_PRODUCTO=0&PRECIO=0&IMPORTE=0&_CANTIDAD=0&Accion=N&TIPO_PROCESO=VENTAS",
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
                                    ID_TIPO_PAGO: $("#ID_TIPO_PAGO").val(),
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

