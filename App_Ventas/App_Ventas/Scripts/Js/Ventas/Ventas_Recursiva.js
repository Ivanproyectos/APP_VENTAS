
///*********************************************** ----------------- *************************************************/

///*********************************************** anular ventas  ***************************************************/

function Ventas_AnularVenta(ID_VENTA) {
    jConfirm("¿ Desea anular esta venta ?, al anular la venta todos los productos de la venta retornan al almacen.", "Anular Venta", function (r) {
        if (r) {
            var item = {
                ID_VENTA: ID_VENTA,
                USU_MODIFICACION: $('#input_hdcodusuario').val(),
            };
            var url = baseUrl + 'Ventas/Ventas/Ventas_AnularVenta';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        Ventas_ConfigurarGrilla();
                        //Ventas_Cerrar();
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

function Ventas_DevolverProducto(CODIGO) {
    var data = jQuery("#" + Ventas_Detalle_Grilla).jqGrid('getRowData', CODIGO);
    var _CANTIDAD_GRID = data.CANTIDAD;
    var _ID_VENTA_DETALLE = data.ID_VENTA_DETALLE;
    var _COD_UNIDAD_MEDIDA = data.COD_UNIDAD_MEDIDA;
    var _disabled_cantidad = "";
    if (data.ID_UNIDAD_MEDIDA == 1)
        _disabled_cantidad = "disabled"
    Ventas_Llenar_ComboMotivo();
    var url = baseUrl + 'Ventas/Ventas/Ventas_Detalle_DevolverProducto';
    var _html = "¿ Desea devolver este producto ?, al devolver el producto la cantidad ingresada retornara al almacen. </br>"
                + "  <div class=\"basic-list-group\">"
                + "  <ul class=\"list-group list-group-flush\" style=\"color: #5a5a5a;text-align: left;\">"
                + "    <li class=\"list-group-item\"><strong><b>Resumen:</b> </strong></li>"
                + "    <li class=\"list-group-item\">"
                + "    <span class=\"\">Producto:</span> <span>" + data.PRODUCTO + "</span>"
                + "  </li>    "
                + "    <li class=\"list-group-item\">"
                + "    <span class=\"\">Cantidad Vendida:</span> <span> " + data.CANTIDAD + " " + _COD_UNIDAD_MEDIDA + "</span>"
                + "  </li>    "
                + "  </li>    "
                + "  </ul>"
                + "<div class=\"form-row\" style=\"margin-top:20px;width:454px;padding: 0px 20px;\"> "
                + "  <div class=\"form-group col-md-3\">"
                + " <label style=\"width: 100%; text-align: left;\">Cantidad:</label>"
                + "<input id=\"CANTIDAD\" class=\"form-control\"  autocomplete=\"off\" value=\"" + _CANTIDAD_GRID + "\" " + _disabled_cantidad + " type=\"number\">"
                + "</div>"
                + "  <div class=\"form-group col-md-9\">"
                + " <label style=\"width: 100%; text-align: left;\">Motivo:</label>"
                + "<select id=\"ID_DEVOLUCION\" class=\"form-control\" > " + Items_Motivo + " </select> "
                + "</div>"
                + "</div>"
    "</div> ";
    swal.fire({
        title: "Devolver Producto",
        showCancelButton: true,
        confirmButtonText: 'Cancelar',
        confirmButtonText: 'Devolver',
        icon: "warning",
        html: _html,
        //inputOptions: options,
        preConfirm: function () {
            return new Promise(function (resolve) {
                // Validate input
                var _CANTIDAD = $('#CANTIDAD').val();
                var _MOTIVO = $('#ID_DEVOLUCION').val();
                var _Mensaje = "";
                var _valido = false;

               

                if (_CANTIDAD == "") {
                    _Mensaje += "Cantidad es oblitario. </br>"
                } else {
                    if (data.ID_UNIDAD_MEDIDA == 1) {
                        _CANTIDAD = ConvertKilos_Gramos(_CANTIDAD); // kilos a gramos
                        _CANTIDAD_GRID = ConvertKilos_Gramos(_CANTIDAD_GRID); // kilos a gramos
                    }
                }

                if (_MOTIVO == "")
                    _Mensaje += "Motivo es oblitario. </br>"

                if (_Mensaje != "") {
                    swal.showValidationMessage(_Mensaje);
                    $(".swal2-confirm").attr('disabled', false);
                    $(".swal2-cancel").attr('disabled', false);
                    return null;
                } else {
                    if (_CANTIDAD_GRID < _CANTIDAD) {
                        swal.showValidationMessage("La cantidad a devolver no puede ser mayor a la cantidad vendida.");
                        $(".swal2-confirm").attr('disabled', false);
                        $(".swal2-cancel").attr('disabled', false);
                        return null;
                    }
                    else
                        _valido = true;
                }

                if (_valido) {
                    swal.resetValidationMessage();

                    var item = {
                        ID_VENTA_DETALLE: _ID_VENTA_DETALLE,
                        USU_MODIFICACION: $('#input_hdcodusuario').val(),
                        CANTIDAD: _CANTIDAD,
                        ID_DEVOLUCION: _MOTIVO
                    };
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                resolve(auditoria);
                            } else {
                                swal.showValidationMessage(auditoria.MENSAJE_SALIDA)
                            }
                        } else {
                            swal.showValidationMessage(auditoria.MENSAJE_SALIDA)
                        }
                    } else {
                        resolve(auditoria);
                    }

                } else {
                    debugger;
                    swal.showValidationMessage("Error al validar");
                }
            })
        },
        onOpen: function (e) {
            debugger;
            setTimeout(function () {
                $('#swal-input1').focus();
                $('#swal-input1').val(_CANTIDAD_GRID);
            }, 500)
        }
    }).then(function (result) {
        debugger;
        //If validation fails, the value is undefined. Break out here.
        if (typeof (result.value) == 'undefined') {
            return false;
        }
        jOkas("Producto devuelto con exito!", "Proceso");
        if (_Modulo == "VENTAS")
            Ventas_ConfigurarGrilla();
        else if (_Modulo == "COBRAR")
            CuentasCobrar_ConfigurarGrilla();
        Ventas_Detalle_CargarGrilla($('#hfd_ID_VENTA').val());

    }).catch(swal.noop)
}

/////*********************************************** ----------------- *************************************************/

function Ventas_Llenar_ComboMotivo() {
    var item = { FLG_ESTADO: 1 }
    var url = baseUrl + 'Administracion/Devolucion/Devolucion_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            var items = "<option value=\"" + "" + "\"> --Seleccione-- </option>";
            $.each(auditoria.OBJETO, function (i, v) {
                items += "<option value=\"" + v.ID_DEVOLUCION + "\" > " + v.DESC_DEVOLUCION + " </option>";
            });
            items += "</select>";
            //$("#" + _SelectInput).html(items);
            Items_Motivo = items;
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}

function Ventas_GenerarVistaComprobante(ID_VENTA) {
    blockUI_('Generando vista previa...');
    setTimeout(function () { jQuery.unblockUI() }, 1000);
    //Ventas_Cerrar();
    var _Html = "<div class=\"row\" style=\"width: 454px;\" >"
               + " <div class=\"form-group col-sm-12\" style=\"padding:2px;\"> "
               + "<button type=\"button\" onclick=\"Ventas_VisualizarComprobante(" + ID_VENTA + ",1)\" class=\"btn btn-outline-dark\" style=\"font-size: 20px;\"><i class=\"bi bi-filetype-pdf\"style=\"color:red\"></i> "
               + " A4</button>  "
               + "<button onclick=\"Ventas_VisualizarComprobante(" + ID_VENTA +",0)\" type=\"button\" class=\"btn btn-outline-dark\" style=\"font-size: 20px;\"><i class=\"bi bi-ticket-detailed\"style=\"color:#2c7be5\"></i> "
               + " Ticket</button>  "
                + "</div>"  
               +  " </div>"
              + "<iframe width=\"100%\" height=\"300\" src='" + baseUrl + "Recursos/Forms/GenerarComprobante.aspx?ID_VENTA=" + ID_VENTA + "&TIPO_COMPROBANTE=0' frameborder=\"1\"></iframe>";
    jOkas(_Html, "Venta realizada con exito!");

}

///*********************************************** ----------------- *************************************************/

///*********************************************** abrir otra pestaña ******************************************/

function Ventas_VisualizarComprobante(ID_VENTA, TIPO_COMPROBANTE) {
    var url = baseUrl + "Recursos/Forms/GenerarComprobante.aspx?ID_VENTA=" + ID_VENTA + "&TIPO_COMPROBANTE=" + TIPO_COMPROBANTE + "";
    // Abrir nuevo tab
    var win = window.open(url, '_blank');
    // Cambiar el foco al nuevo tab (punto opcional)
    win.focus();
}


function Ventas_ImprimirComprobante(ID_VENTA, _COD_COMPROBANTE) {
    blockUI_('Generando vista previa...');
    setTimeout(function () { jQuery.unblockUI() }, 1000);
    var _Html = "<b style=\"color:#2c7be5;\">Nro. Comprobante: " + _COD_COMPROBANTE + "</b> </br> </br>"
           + "<div class=\"row\" style=\"width: 454px;\" >"
           + " <div class=\"form-group col-sm-12\" style=\"padding:2px;\"> "
           + "<button type=\"button\" onclick=\"Ventas_VisualizarComprobante(" + ID_VENTA + ",1)\" class=\"btn btn-outline-dark\" style=\"font-size: 20px;\"><i class=\"bi bi-filetype-pdf\"style=\"color:red\"></i> "
           + " A4</button>  "
           + "<button onclick=\"Ventas_VisualizarComprobante(" + ID_VENTA + ",0)\" type=\"button\" class=\"btn btn-outline-dark\" style=\"font-size: 20px;\"><i class=\"bi bi-ticket-detailed\"style=\"color:#2c7be5\"></i> "
           + " Ticket</button>  "
            + "</div>"
           + " </div>"
          + "<iframe width=\"100%\" height=\"300\" src='" + baseUrl + "Recursos/Forms/GenerarComprobante.aspx?ID_VENTA=" + ID_VENTA + "&TIPO_COMPROBANTE=0' frameborder=\"1\"></iframe>";
    jSweetModal(_Html, "Imprimir Comprobante");

}



///*********************************************** ----------------- *************************************************/

///*********************************************** validar cliente credito   ***************************************************/

function Ventas_ValidarCliente_Credito(ID_CLIENTE, ID_SUCURSAL) {
    var item = {
        ID_CLIENTE: ID_CLIENTE,
        ID_SUCURSAL: ID_SUCURSAL,
    };
    var url = baseUrl + 'Ventas/Ventas/Ventas_ValidarCliente_Credito';
    var auditoria = SICA.Ajax(url, item, false);
    if (auditoria != null && auditoria != "") {
        if (auditoria.EJECUCION_PROCEDIMIENTO) {
            if (auditoria.RECHAZAR) {
                _FLG_CREDITO_PENDIENTE = false;
                _ID_VENTA_CREDITO = 0; 
                $('#Ventas_AlertCredito').hide('slow');
            } else {
                _FLG_CREDITO_PENDIENTE = true;
                _ID_VENTA_CREDITO = auditoria.OBJETO;
                $('#Ventas_AlertCredito').show('slow');
            }
        } else {
            jError(auditoria.MENSAJE_SALIDA, "Atención");
        }
    }
}


function Ventas_ClientesXComprobante(ID_COMPROBANTE) {
    var item = { }
    var url = baseUrl + 'Ventas/Ventas/Ventas_ClientesXComprobante?ID_TIPO_COMPROBANTE=' + ID_COMPROBANTE;
    var auditoria = SICA.Ajax(url, item, false);
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            var items = "<option value=\"" + "" + "\"> --Seleccione-- </option>";
            $.each(auditoria.OBJETO, function (i, v) {
                if (v.NUMERO_DOCUMENTO == "ALP970742760") // al publico
                    items += "<option selected value=\"" + v.ID_CLIENTE + "\" >"+ v.NOMBRES_APE + "</option>";
                else {
                    items += "<option value=\"" + v.ID_CLIENTE + "\" >" + v.NOMBRES_APE + " - Nro. Doc. " + v.NUMERO_DOCUMENTO + "</option>";
                }
            });
            items += "</select>";
            $('#ID_CLIENTE').html(items);
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}