
$('#ChangeProfile').click(function () {
    var item = {};
    InvocarVista(baseUrl + 'Administracion/MiCuenta/Index', item);
});


function MyCount_CambiarClave() {
    if ($("#FrmCambioClave").valid()) {
        if ($("#NUEVA_CLAVE").val() == $("#REPITA_CLAVE").val()) {
            jConfirm("¿ Desea cambiar tu contrseña ?", "Atención", function (r) {
                if (r) {
                    var item =
                    {
                        CLAVE_ACTUAL: $("#CLAVE_ACTUAL").val(),
                        NUEVA_CLAVE: $("#NUEVA_CLAVE").val(),
                        REPITA_CLAVE: $("#REPITA_CLAVE").val(),
                    };
                    var url = baseUrl + 'Administracion/MiCuenta/MiCuenta_CambiarContraseña';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                $("#REPITA_CLAVE").val('');
                                $("#NUEVA_CLAVE").val(''); 
                                $("#CLAVE_ACTUAL").val('');
                                jOkas("Clave actualizado exitosamente", "Proceso");
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
            jWarning('Las claves no coinciden.','Atención')
        }
    }
}