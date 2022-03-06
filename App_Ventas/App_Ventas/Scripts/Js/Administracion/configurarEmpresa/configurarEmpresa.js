
///*********************************************** ----------------- *************************************************/

///*********************************************** Actualiza  cargos  ************************************************/

function ConfigurarEmpresa_Actualizar() {
    if ($("#FrmconfigEmpresa").valid()) {
        var item =
                {
                    ID_CONFIGURACION: $("#hfd_ID_CONFIGURACION").val(),
                    RUC: $("#RUC").val(),
                    RAZON_SOCIAL: $("#RAZON_SOCIAL").val(),
                    NOMBRE_COMERCIAL: $('#NOMBRE_COMERCIAL').val(),
                    DIRECCION_FISCAL: $('#DIRECCION_FISCAL').val(),
                    URBANIZACION: $('#URBANIZACION').val(),
                    COD_UBIGEO: $('#COD_UBIGEO').val(),
                    TELEFONO: $('#TELEFONO').val(),
                    NOMBRE_IMPUESTO: $('#NOMBRE_IMPUESTO').val(),
                    IMPUESTO: $('#IMPUESTO').val(),
                    FLG_IMPRIMIR: $('#FLG_IMPRIMIR').is(":checked")? 1 : 0,
                    FLG_IMPUESTO: $('#FLG_IMPUESTO').is(":checked") ? 1 : 0,
                    SIMBOLO_MONEDA: $('#SIMBOLO_MONEDA').val(),
                    CORREO: $('#CORREO').val(),
                    Archivo_Isotipo: configurarEmpresaIsotipo = true ? IsotipoNuevo_Array[0] : null,
                    Archivo_Logo: configurarEmpresaLogo = true ? LogoNuevo_Array[0] : null,
                    USU_MODIFICACION: $('#input_hdcodusuario').val(),
                    ACCION: $("#AccionClientes").val()
           
                };
            jConfirm("¿ Desea actualizar este registro ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Administracion/ConfigurarEmpresa/ConfigurarEmpresa_Actualizar';
                var auditoria = SICA.Ajax(url, item, false);
                if (auditoria != null && auditoria != "") {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            jOkas("Configuración actualizado satisfactoriamente", "Proceso");
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

///*********************************************** ----------------- *************************************************/

///************************************************ Configuracion  Insertar  **************************************************/

function ConfigurarEmpresa_Insertar() {
    if ($('#hfd_ID_CONFIGURACION').val() != '0') {
        ConfigurarEmpresa_Actualizar();
    } else {
        if ($("#FrmconfigEmpresa").valid()) {
            jConfirm("¿ Desea registrar esta configuración ?", "Atención", function (r) {
                if (r) {
                    var item =
                        {
                            RUC: $("#RUC").val(),
                            RAZON_SOCIAL: $("#RAZON_SOCIAL").val(),
                            NOMBRE_COMERCIAL: $('#NOMBRE_COMERCIAL').val(),
                            DIRECCION_FISCAL: $('#DIRECCION_FISCAL').val(),
                            URBANIZACION: $('#URBANIZACION').val(),
                            COD_UBIGEO: $('#COD_UBIGEO').val(),
                            TELEFONO: $('#TELEFONO').val(),
                            NOMBRE_IMPUESTO: $('#NOMBRE_IMPUESTO').val(),
                            IMPUESTO: $('#IMPUESTO').val(),
                            FLG_IMPRIMIR: $('#FLG_IMPRIMIR').is(":checked")? 1 : 0,
                            FLG_IMPUESTO: $('#FLG_IMPUESTO').is(":checked") ? 1 : 0,
                            SIMBOLO_MONEDA: $('#SIMBOLO_MONEDA').val(),
                            CORREO: $('#CORREO').val(),
                            Archivo_Isotipo: configurarEmpresaIsotipo = true ? IsotipoNuevo_Array[0] : null,
                            Archivo_Logo: configurarEmpresaLogo = true ? LogoNuevo_Array[0] : null,
                            USU_CREACION: $('#input_hdcodusuario').val(),
                            ACCION: $("#AccionClientes").val()
                        };
                    var url = baseUrl + 'Administracion/ConfigurarEmpresa/ConfigurarEmpresa_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                jOkas("Configuración registrado satisfactoriamente", "Proceso");
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
}
