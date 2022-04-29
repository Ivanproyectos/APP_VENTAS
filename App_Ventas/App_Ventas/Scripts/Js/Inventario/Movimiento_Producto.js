
function Producto_Movimiento_Insertar(Tipo) {
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
                                DETALLE:  $("#DETALLE").val(),
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
}