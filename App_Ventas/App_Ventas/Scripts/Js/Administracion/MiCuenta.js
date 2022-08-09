
$('#ChangeProfile').click(function () {
    var item = {};
    InvocarVista(baseUrl + 'Administracion/MiCuenta/Index', item);
});


function MyCount_CambiarClave() {
    if ($("#FrmCambioClave").valid()) {
        if ($("#NUEVA_CLAVE").valid() != $("#REPITA_CLAVE").valid()) {
            jConfirm("¿ Desea registrar este cliente ?", "Atención", function (r) {
                if (r) {
                    var item =
                    {
                        ID_TIPO_DOCUMENTO: $("#ID_TIPO_DOCUMENTO").val(),
                        NUMERO_DOCUMENTO: $("#NUMERO_DOCUMENTO").val(),
                        NOMBRES_APE: $("#NOMBRES_APE").val(),
                        TELEFONO: $("#TELEFONO").val(),
                        CORREO: $("#CORREO").val(),
                        DIRECCION: $("#DIRECCION").val(),
                        CELULAR: $("#CELULAR").val(),
                        COD_UBIGEO: $("#COD_UBIGEO").val(),
                        DETALLE: $("#DETALLE").val(),
                        USU_CREACION: $('#input_hdcodusuario').val(),
                        ACCION: $("#AccionClientes").val()
                    };
                    var url = baseUrl + 'Administracion/Clientes/Cliente_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Clientes_CargarGrilla();
                                Clientes_Cerrar();
                                jOkas("Clientes registrado satisfactoriamente", "Proceso");
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