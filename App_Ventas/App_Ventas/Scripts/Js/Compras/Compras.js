var Compras_Grilla = 'Compras_Grilla';
var Compras_Barra = 'Compras_Barra';

function Compras_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Compras_Limpiar() {
    $("#Compras_CodigoComprobante").val('');
    $('#ID_PROVEEDOR_SEARCH').val('').trigger('change');
    $('#Compras_FLG_ANULADO').val('');
    $('#Compras_FechaRange').val('');
    Compras_ConfigurarGrilla();
}

function Compras_ConfigurarGrilla() {
    var url = baseUrl + 'Compras/Compras/Compras_Paginado';
    DataTable.GridUnload(Compras_Grilla);
    var colModels = [
          { data: "ID_COMPRA", name: "ID_COMPRA", title: "ID_COMPRA", autoWidth: false, visible: false, },
          {
              data: null, name: "TIPO_COMPROBANTE", title: "Comprobante", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Compras_FormaterComprobante(data.DESC_TIPO_COMPROBANTE, data.COD_COMPROBANTE); }
          },
          { data: "FECHA_COMPROBANTE", name: "FECHA_COMPROBANTE", title: "Fecha Compra", autoWidth: true },
          { data: "Proveedor.NOMBRES_APE", name: "PROVEEDOR", title: "Proveedor", autoWidth: true },
          {
              data: null, name: "DESCUENTO", title: "Descuento", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Compras_FormatterMoneda(data.DESCUENTO); }
          },
          {
              data: null, name: "SUBTOTAL", title: "Subtotal", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Compras_FormatterMoneda(data.SUB_TOTAL); }
          },
          {
              data: null, name: "IGV", title: "Igv", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Compras_FormatterMoneda(data.IGV); }
          },
          {
              data: null, name: "TOTAL", title: "Total", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Compras_FormatterMoneda(data.TOTAL); }
          },
          {
              data: null, name: "DESC_TIPO_PAGO", title: "Tipo Pago", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Compras_FormatterTipoPago(data.DESC_TIPO_PAGO, data.ID_TIPO_PAGO, data.NRO_OPERACION); }
          },
          {
              data: null, name: "DESC_ESTADO_COMPRA", title: "Estado Compra", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Compras_FormatterEstadoCompra(data.DESC_ESTADO_COMPRA, data.FLG_ANULADO, data.FECHA_ANULADO); }
          },
          {
              data: null, sortable: false, title: "Acciones", width: "60px",
              render: function (data, type, row, meta) { return Compras_Acciones(data.ID_COMPRA, data.FLG_ANULADO); }
          },

    ];
    var opciones = {
        GridLocal: false, multiselect: false, sort: "desc", enumerable: true,
        eliminar: false, search: true, rowNumber: 10, rowNumbers: [10, 25, 50], rules: true, responsive: true, processing: true
    };
    DataTable.Grilla(Compras_Grilla, url, 'ID_COMPRA', colModels, opciones, "ID_COMPRA");
}

function GetRules(Compras_Grilla) {
    var rules = new Array();
    var SearchFields = new Array();
    var ID_PROVEEDOR = jQuery('#ID_PROVEEDOR_SEARCH').val() == '' ? null : "'" + jQuery('#ID_PROVEEDOR_SEARCH').val() + "'";
    var FLG_ANULADO = jQuery('#Compras_FLG_ANULADO').val() == '' ? null : "'" + jQuery('#Compras_FLG_ANULADO').val() + "'";
    var FECHA_INICIO = jQuery('#Compras_FechaRange').val() == '' ? null : "'" + jQuery('#Compras_FechaRange').val().split('-')[0].trim() + "'";
    var FECHA_FIN = jQuery('#Compras_FechaRange').val() == '' ? null : "'" + jQuery('#Compras_FechaRange').val().split('-')[1].trim() + "'";
    var ID_SUCURSAL = jQuery('#ID_SUCURSAL').val() == '' ? null : "'" + jQuery('#ID_SUCURSAL').val() + "'";

    var POR = "'%'";
    rules = []
    rules.push({ field: 'FLG_ANULADO', data: '  ISNULL(' + FLG_ANULADO + ',FLG_ANULADO) ', op: " = " });
    rules.push({ field: 'CONVERT(DATE,FECHA_COMPROBANTE,103)', data: 'CONVERT(DATE,ISNULL(' + FECHA_INICIO + ',FECHA_COMPROBANTE),103)  AND CONVERT(DATE,ISNULL(' + FECHA_FIN + ',FECHA_COMPROBANTE),103)  ', op: " BETWEEN " });
    rules.push({ field: 'ID_SUCURSAL', data: '  ISNULL(' + ID_SUCURSAL + ',ID_SUCURSAL) ', op: " = " });

    SearchFields.push({ field: 'UPPER(V.PROVEEDOR)' });
    SearchFields.push({ field: 'UPPER(V.COD_COMPROBANTE)' });

    var ObjectRules = {
        SearchFields: SearchFields,
        rules: rules
    }

    return ObjectRules;
}

function Compras_Acciones(ID_COMPRA, FLG_ANULADO) {
    var _ID_COMPRA = ID_COMPRA;
    var _FLG_FLG_ANULADO = FLG_ANULADO;
    var _btn_Anular = "";
    if (_FLG_FLG_ANULADO == 0) {
        _btn_Anular = "<a class=\"dropdown-item\" onclick='Compras_AnularVenta(" + _ID_COMPRA + ")'><i class=\"bi bi-basket-fill\" style=\"color:red;\"></i>&nbsp;  Anular Compra</a>";
    } 
    var _btn = "<div class=\"btn-group Group_Acciones dropleft\" role=\"group\" title=\"Acciones \" >" +
            "<button  style=\" background: transparent; border: none; color: #000000;font-size: 18px;\" type=\"button\" class=\"btn btn-link dropdown-toggle text-dark nobefore\" data-toggle=\"dropdown\" aria-expanded=\"false\"><i class=\"bi bi-three-dots-vertical\"></i></button>" +
           "<div class=\"dropdown-menu\" x-placement=\"left-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(-162px, 0px, 0px);\">" +
           "<a class=\"dropdown-item\" onclick='Compras_ViewDetalleCompra(" + _ID_COMPRA + ")'><i class=\"bi bi-stickies\" style=\"color:#2c7be5\"></i>&nbsp;  Detalle Compra</a>" +
           _btn_Anular +
            "</div>" +
        "</div>";
    return _btn;
}

function Compras_FormaterComprobante(DESC_TIPO_COMPROBANTE, COD_COMPROBANTE) {
    var _DESC_COMPROBANTE = DESC_TIPO_COMPROBANTE;
    var _DESC_COD_COMPROBANTE = COD_COMPROBANTE;
    var _text = "";
    var _text = '<span>' + _DESC_COMPROBANTE + '</span><br><span style="font-size: 12px; color: #2c7be5;"><i class="bi bi-upc"></i>&nbsp;Nro: ' + _DESC_COD_COMPROBANTE + '</span>';
    return _text;
}

function Compras_FormatterTipoPago(DESC_TIPO_PAGO, ID_TIPO_PAGO, NRO_OPERACION) {
    var _DESC_TIPO_COMPRA = DESC_TIPO_PAGO;
    var _ID_TIPO_PAGO = ID_TIPO_PAGO;
    var _NRO_OPERACION = NRO_OPERACION;
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

function Compras_FormatterEstadoCompra(DESC_ESTADO_COMPRA, FLG_ANULADO, FECHA_ANULADO) {
    var _DESC_ESTADO_COMPRA = DESC_ESTADO_COMPRA;
    var _FLG_ANULADO = FLG_ANULADO;
    var _FECHA_ANULADO = FECHA_ANULADO;
    var _text = "";
    if (_FLG_ANULADO == 1) {
        _text = "<span class=\"badge badge-danger \" data-bs-toggle=\"tooltip\" title=\"Esta venta fue anulada.\">" + _DESC_ESTADO_COMPRA + "</span>";
    }
    else if (_FLG_ANULADO == 0) {
        _text = "<span class=\"badge badge-success\" data-bs-toggle=\"tooltip\" title=\"Venta Realiazada\">" + _DESC_ESTADO_COMPRA + "</span>";
    }
    return _text;
}

function Compras_FormatterMoneda(MONTO) {
    var _TOTAL = Number(MONTO).toFixed(2);
    var _text = _SimboloMoneda + " " + _TOTAL;
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
                                jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
                            }
                        } else {
                            jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
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
        jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
    }
}
