var CuentasCobrar_Grilla = 'CuentasCobrar_Grilla';
var CuentasCobrar_Barra = 'CuentasCobrar_Barra';
var _Modulo = "COBRAR";

function CuentasCobrar_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function CuentasCobrar_Limpiar() {
    $("#CuentasCobrar_CodigoVenta").val('');
    $('#CuentasCobrar_FLG_CREDITO').val('');
    $('#ID_CLIENTE').val('');
    $('#CuentasCobrar_FechaVenta').val('');

    CuentasCobrar_ConfigurarGrilla();
}

function CuentasCobrar_ConfigurarGrilla() {
    var url = baseUrl + 'CuentasCobrar/CuentasCobrar/CuentasCobrar_Paginado';
    DataTable.GridUnload(CuentasCobrar_Grilla);
    var colModels = [
          { data: "ID_VENTA", name: "ID_VENTA", title: "ID_VENTA", autoWidth: false, visible: false, },
          {
              data: null, name: "TIPO_COMPROBANTE", title: "Comprobante", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return CuentasCobrar_FormaterComprobante(data.DESC_TIPO_COMPROBANTE, data.COD_COMPROBANTE); }
          },
          { data: "Cliente.NOMBRES_APE", name: "CLIENTE", title: "Cliente", autoWidth: true, },
          {
              data: null, name: "DESCUENTO", title: "Descuento", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return CuentasCobrar_FormatterMoneda(data.DESCUENTO); }
          },
          {
              data: null, name: "SUBTOTAL", title: "Subtotal", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return CuentasCobrar_FormatterMoneda(data.SUB_TOTAL); }
          },
          {
              data: null, name: "IGV", title: "Igv", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return CuentasCobrar_FormatterMoneda(data.IGV); }
          },
          {
              data: null, name: "TOTAL", title: "Total", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return CuentasCobrar_FormatterMoneda(data.TOTAL); }
          },
          {
              data: null, name: "DEBE", title: "Total", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return CuentasCobrar_FormatterMoneda(data.DEBE); }
          },
          {
              data: null, name: "DESC_ESTADO_CREDITO", title: "Estado", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return CuentasCobrar_formatterEstadoCredito(data.DESC_ESTADO_CREDITO, data.STR_FECHA_CREDITO_CANCELADO, data.FLG_ESTADO_CREDITO); }
          },
          { data: "DETALLE", name: "DETALLE", title: "Detalle", autoWidth: true, },
          {
              data: null, sortable: false, title: "Acciones", width: "60px",
              render: function (data, type, row, meta) { return CuentasCobrar_actionAcciones(data.ID_VENTA, data.FLG_ANULADO, data.FLG_ESTADO_CREDITO); }
          },

    ];
    var opciones = {
        GridLocal: false, multiselect: false, sort: "desc", enumerable: true,
        eliminar: false, search: true, rowNumber: 10, rowNumbers: [10, 25, 50], rules: true, responsive: true, processing: true
    };
    DataTable.Grilla(CuentasCobrar_Grilla, url, 'ID_VENTA', colModels, opciones, "ID_VENTA");
}


function GetRules(CuentasCobrar_Grilla) {
    debugger; 
    var rules = new Array();
    var SearchFields = new Array();
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
    rules.push({ field: '(ID_TIPO_PAGO', data: '2 OR FLG_ESTADO_CREDITO  = 1 )', op: " = " }); // CREDITO
    rules.push({ field: 'FLG_ANULADO', data: 0, op: " = " }); // NO ANULADOS


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

function CuentasCobrar_actionAcciones(ID_VENTA, FLG_ANULADO, FLG_ESTADO_CREDITO) {
    var _ID_VENTA = ID_VENTA; 
    var _FLG_FLG_ANULADO = FLG_ANULADO; 
    var _FLG_ESTADO_CREDITO = FLG_ESTADO_CREDITO; 
    var _btn_Cobrar =""; 
    var _btn_Anular = "";
    var _btn_Devolver = "";
    var _btn_Notificar = "";

    if (_FLG_FLG_ANULADO == 0 && _FLG_ESTADO_CREDITO == 0) 
        _btn_Anular = "<a class=\"dropdown-item\" onclick='Ventas_AnularVenta(" + _ID_VENTA + ")'><i class=\"bi bi-cart-x\" style=\"color:red;\"></i>&nbsp;Anular</a>";
         _btn_Devolver = "<a class=\"dropdown-item\" onclick=\"CuentasCobrar_MostrarDevolverProducto(" + _ID_VENTA + ")\" ><i class=\"bi bi-box-arrow-in-down-left\" style=\"color:green;\"></i>&nbsp;  Devolver Producto</a>";

    if (_FLG_ESTADO_CREDITO == 0) {
        _btn_Cobrar = "<a class=\"dropdown-item\" onclick='CuentasCobrar_MostrarCobrarCredito(" + _ID_VENTA + ")'><i class=\"bi bi-cash-coin\" style=\"color:#2c7be5\"></i>&nbsp;Cobrar</a>";
        _btn_Notificar = "<a class=\"dropdown-item\" onclick='CuentasCobrar_NotificarCredito(" + _ID_VENTA + ")'><i class=\"bi bi-send\" style=\"color:#D34320\"></i>&nbsp;Notificar Credito</a>";
    }

    var _btn = "<div class=\"btn-group Group_Acciones\" role=\"group\" title=\"Acciones \" >" +
           " <button  style=\" background: transparent; border: none; color: #000000;font-size: 18px;\" type=\"button\" class=\"btn dropdown-toggle\" data-toggle=\"dropdown\" aria-expanded=\"false\"><i class=\"bi bi-list\"></i></button> " +
           " <div class=\"dropdown-menu\" x-placement=\"bottom-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 35px, 0px);\">" +
            "<a class=\"dropdown-item\" onclick='Ventas_ViewDetalleVenta(" + _ID_VENTA + ")'><i class=\"bi bi-stickies\" style=\"color:#2c7be5\"></i>&nbsp;  Detalle Venta</a>" +
               _btn_Notificar +
               _btn_Cobrar +
               _btn_Anular +
               _btn_Devolver + 
            "</div>" +
        "</div>";
    return _btn;
}

function CuentasCobrar_FormaterComprobante(DESC_TIPO_COMPROBANTE ,COD_COMPROBANTE) {
    var _DESC_COMPROBANTE = DESC_TIPO_COMPROBANTE;
    var _DESC_COD_COMPROBANTE = COD_COMPROBANTE; 
    var _text = "";
    var _text = '<span>' + _DESC_COMPROBANTE + '</span><br><span style="font-size: 12px; color: #2c7be5;"><i class="bi bi-upc"></i>&nbsp;Nro: ' + _DESC_COD_COMPROBANTE + '</span>';
    return _text;
}

function CuentasCobrar_FormatterMoneda(MONTO) {
    var _TOTAL = Number(MONTO).toFixed(2);
    var _text = _SimboloMoneda + " " + _TOTAL;
    return _text;
}

function CuentasCobrar_FormatterDebe(cellvalue, options, rowObject) {
    var _DEBE = rowObject[9];
    var _text = _SimboloMoneda + " " + _DEBE;
    return _text;
}


function CuentasCobrar_formatterEstadoCredito(DESC_ESTADO_CREDITO, STR_FECHA_CREDITO_CANCELADO, FLG_ESTADO_CREDITO) {
    var _FLG_ESTADO_CREDITO = FLG_ESTADO_CREDITO; 
    var _DESC_ESTADO_CREDITO = DESC_ESTADO_CREDITO;
    var _FECHA_CANCELADO = STR_FECHA_CREDITO_CANCELADO; 
    var _text = "";
    if (_FLG_ESTADO_CREDITO == 0) {
        _text = "<span class=\"badge badge-danger \" data-bs-toggle=\"tooltip\" title=\"Este credito aun esta pendiente.\">" + _DESC_ESTADO_CREDITO + "</span>";
    }
    else if (_FLG_ESTADO_CREDITO == 1) {
        _text = "<span class=\"badge badge-success\" data-bs-toggle=\"tooltip\" title=\"Fecha: " + _FECHA_CANCELADO + "\">" + _DESC_ESTADO_CREDITO + "</span>";
               // + " <br><span style=\"font-size: 12px; color: #2c7be5;\"><i class=\"bi bi-calendar-week\"></i>&nbsp;Fecha/Hora: " +  + "</span>";
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
                            ID_TIPO_PAGO: $("#ID_TIPO_PAGO").val(),
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


function CuentasCobrar_NotificarCredito(ID_VENTA) {
        jConfirm("¿ Desea enviar correo para notificar este credito. ?", "Atención", function (r) {
            if (r) {
                _blockUI("Enviando Correo..."); 
                setTimeout(function () {
                            var item =
                       {
                           ID_VENTA: ID_VENTA,
                       };
                    var url = baseUrl + 'CuentasCobrar/CuentasCobrar/CuentasCobrar_NotificarCredito';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                jQuery.unblockUI(); 
                                jOkas('Correo enviado con exito', 'Atención');
                           
                            } else {
                                jQuery.unblockUI(); 
                                jError(auditoria.MENSAJE_SALIDA, "Atención");
                            }
                        } else {
                            jQuery.unblockUI(); 
                            jError(auditoria.MENSAJE_SALIDA, "Atención");
                        }
                    }
                }, 500);      
            }
        });
}




