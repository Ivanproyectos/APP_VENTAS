
function Producto_MostrarImportarProducto() {
    var _ID_SUCURSAL = $('#ID_SUCURSAL').val();
    var _DESC_SUCURSAL = $('select[name="ID_SUCURSAL"] option:selected').text();
    if (_ID_SUCURSAL != "") {
        _DESC_SUCURSAL = _DESC_SUCURSAL.replace(/ /g, "+");
        jQuery("#myModalNuevo").html('');
        jQuery("#myModalNuevo").load(baseUrl + "Inventario/Importar_Producto/View_Importar?ID_SUCURSAL=" + _ID_SUCURSAL +
            "&DESC_SUCURSAL=" + _DESC_SUCURSAL, function (responseText, textStatus, request) {
                $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
                $.validator.unobtrusive.parse('#myModalNuevo');
                if (request.status != 200) return;
            });
    } else {
        jInfo('Actualmente tu está mirando los productos de todos los almacenes, debes seleccionar uno en específico donde deseas registrar el producto.', 'Atención')
    }
}

function Producto_DescargarPlantillaProducto() {
    jQuery("#myModalDescargar").html('');
    jQuery("#myModalDescargar").load(baseUrl + "Inventario/Importar_Producto/Importar_DescagarPlantilla", function (responseText, textStatus, request) {
    $.validator.unobtrusive.parse('#myModalDescargar');
        if (request.status != 200) return;
    });
    
}

function Producto_ImportarProducto() {
    var _ID_SUCURSAL = $("#ID_SUCURSAL").val();
    var pregunta = "";
    if (_ID_SUCURSAL == "") {
        jAlert("Alamacen no seleccionado, por favor seleccione uno", "Atención");
        return;
    }
    jConfirm("Antes de continuar favor de asegurarse que el archivo no tenga caracteres especiales [;*_\!,etc] en el nombre y el nombre de la hoja sea Hoja1, si ya hizo todo lo mencionado obvie este mensaje presionando el botón Aceptar para seguir con el proceso", "Atención", function (r) {
        if (r) {
            debugger; 
            var url = baseUrl + "Inventario/Importar_Producto/Importar_Producto";
            var options = {
                type: "POST",
                dataType: "json",
                url: url,
                extraData: ({
                }),
                resetForm: true,
                beforeSubmit: function (formData, jqForm, options) {
                    var queryString = $.param(formData);
                    return true;
                },
                success: function (auditoria) {
                    $("#Lbl_Namefile").html("Sin archivo cargado...");
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            jOkas('El proceso de carga ha culminado', 'Alerta');

                        } else {
                            jError( auditoria.MENSAJE_SALIDA, 'Atención');
                        }
                    }
                    else {
                        jError(auditoria.MENSAJE_SALIDA, 'Atención');
                    }

                    //if (auditoria.OBJETO != null)
                    //    Tabla_Resultados(auditoria.OBJETO, ID_TABLA);
                    //else {
                    //    html = "<i class=\"clip-close\" style='color:#f30203'></i>&nbsp; <span style='color:#f30203'>Carga con errores</span>";
                    //    $("#lbl_resultado").html(html);
                    //}

                }
            };
            $("#Form_CargarProductos").ajaxForm(options);
            $("#Form_CargarProductos").submit();
        }
    });
}


