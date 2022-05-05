
function Producto_Movimiento_Insertar(Tipo) {
    if (!isNaN($("#CANTIDAD").val()) && $("#CANTIDAD").val() != 0) {
        var mensaje = "";
        if (Tipo == 1)
            mensaje = "<span class=\"text-success\">aumentar</span>";
        else if (Tipo == 2)
            mensaje = "<span class=\"text-danger\">disminuir</span>";
        if ($("#frmMantenimiento_BuscarProducto").valid()) {
            jConfirm("¿ Desea " + mensaje + " este producto en  <b>" + $("#CANTIDAD").val() + " " +
                $("#HDF_COD_UNIDAD_MEDIDA").val() + "</b> al stock actual ?", "Atención", function (r) {
                    if (r) {
                        var STOCK = $("#CANTIDAD").val();
                        if ($("#ID_UNIDAD_MEDIDA").val() == 1) // si es kilos guardo en gramos 
                        {
                            STOCK = ConvertKilos_Gramos(STOCK);
                        }
                        var item =
                            {
                                FLG_MOVIMIENTO: Tipo,
                                CANTIDAD: STOCK,
                                ID_PRODUCTO: $("#hfd_ID_PRODUCTO").val(),
                                DETALLE: $("#DETALLE").val(),
                                USU_CREACION: $('#input_hdcodusuario').val(),
                                ACCION: $("#AccionProducto").val()
                            };

                        var url = baseUrl + 'Inventario/Producto/Producto_Movimiento_Insertar';
                        var auditoria = SICA.Ajax(url, item, false);
                        if (auditoria != null && auditoria != "") {
                            if (auditoria.EJECUCION_PROCEDIMIENTO) {
                                if (!auditoria.RECHAZAR) {
                                    Producto_ConfigurarGrilla();
                                    Producto_Cerrar();
                                    jOkas("Proceso culminado satisfactoriamente.", "Proceso");
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
    } else {
        jError("La cantidad a ingresar debe ser mayor a cero.", "Atención");
    }
}


function Producto_Translados_Insertar() {
    if ($("#frmMantenimiento_TransladoOrgigen").valid() && $("#frmMantenimiento_TransladoDestino").valid()) {
        var ListaDetalleProductos = new Array();
        var ListaDetalle = new Array();
        ListaDetalleProductos = $("#" + Translados_Detalle_Grilla).jqGrid('getGridParam', 'data');
        for (var i = 0; i < ListaDetalleProductos.length; i++) {
            var rowData = ListaDetalleProductos[i];
            var _CANTIDAD = rowData.CANTIDAD;
            if (rowData.ID_UNIDAD_MEDIDA == 1) { // si es kilos convertir a gramos
                _CANTIDAD = ConvertKilos_Gramos(_CANTIDAD);
            }
            var myData =
            {
                ID_PRODUCTO: rowData.ID_PRODUCTO,
                CANTIDAD: _CANTIDAD,
            };
            ListaDetalle.push(myData);
        }
        if (ListaDetalle.length > 0) {
            if (ValidarRepeatSucursal()) {
                jConfirm("Se transladarán productos del almacen <span class='text-info'> " + $('select[name="ID_SUCURSAL_ORIGEN"] option:selected').text()
                    + " </span> al almacen destino <span class='text-success'> " + $('select[name="ID_SUCURSAL_DESTINO"] option:selected').text() +
                    " </span> <br/>¿Desea continuar con el traslado de los productos ?", "Atención", function (r) {
                    if (r) {
                        var item =
                            {
                                ID_SUCURSAL_ORIGEN: $("#ID_SUCURSAL_ORIGEN").val(),
                                ID_SUCURSAL_DESTINO: $("#ID_SUCURSAL_DESTINO").val(),
                                DETALLE: $("#DETALLE").val(),
                                ListaDetalle: ListaDetalle,
                                USU_CREACION: $('#input_hdcodusuario').val(),
                            };
                        var url = baseUrl + 'Inventario/Translado_Producto/Producto_Translado_Insertar';
                        var auditoria = SICA.Ajax(url, item, false);
                        if (auditoria != null && auditoria != "") {
                            if (auditoria.EJECUCION_PROCEDIMIENTO) {
                                if (!auditoria.RECHAZAR) {
                                    Producto_ConfigurarGrilla();
                                    Producto_Cerrar();
                                    jOkas("Translado realizado correctamente", "Proceso");
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
        } else {
            jError("La lista de productos no puede estar vacia.", "Atención");
        }
    }
}

function Producto_MostrarIngresoProducto() {
    var _ID_SUCURSAL = $('#ID_SUCURSAL').val();
    var _DESC_SUCURSAL = $('select[name="ID_SUCURSAL"] option:selected').text();
    if (_ID_SUCURSAL != "") {
        _DESC_SUCURSAL = _DESC_SUCURSAL.replace(/ /g, "+");
        jQuery("#myModalNuevo").html('');
        jQuery("#myModalNuevo").load(baseUrl + "Inventario/Producto/View_Ingreso?ID_SUCURSAL=" + _ID_SUCURSAL +
            "&DESC_SUCURSAL=" + _DESC_SUCURSAL, function (responseText, textStatus, request) {
                $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
                $.validator.unobtrusive.parse('#myModalNuevo');
                if (request.status != 200) return;
            });
    } else {
        jInfo('Para registrar un ingreso de producto selecione el almacen donde se registrara.', 'Atención')
    }
}

function Producto_MostrarSalidasProducto() {
    var _ID_SUCURSAL = $('#ID_SUCURSAL').val();
    var _DESC_SUCURSAL = $('select[name="ID_SUCURSAL"] option:selected').text();
    if (_ID_SUCURSAL != "") {
        _DESC_SUCURSAL = _DESC_SUCURSAL.replace(/ /g, "+");
        jQuery("#myModalNuevo").html('');
        jQuery("#myModalNuevo").load(baseUrl + "Inventario/Producto/View_Salidas?ID_SUCURSAL=" + _ID_SUCURSAL +
            "&DESC_SUCURSAL=" + _DESC_SUCURSAL, function (responseText, textStatus, request) {
                $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
                $.validator.unobtrusive.parse('#myModalNuevo');
                if (request.status != 200) return;
            });
    } else {
        jInfo('Para registrar una salida de producto selecione el almacen donde se registrara.', 'Atención')
    }
}


function Producto_MostrarTranslado() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Inventario/Translado_Producto/View_Translados", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}