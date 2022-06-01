function BuscarPersonalNatural(_NumeroDocumento) {
    if (_NumeroDocumento != "") {
        if (_NumeroDocumento.length == 8) {
            _blockUI('Buscando persona...');
            setTimeout(function () {
                var item = {
                    DNI: _NumeroDocumento
                };
                var url = baseUrl + 'Recursiva/ServiciosWeb/Service_ConsultaDni';
                var auditoria = SICA.Ajax(url, item, false);
                if (auditoria != null && auditoria != "") {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {                    
                                var Objson = JSON.parse(auditoria.OBJETO);
                                Objson = JSON.parse(Objson);
                                $('#NOMBRES_APE').val(Objson.nombres + " "
                                                        + Objson.apellidoPaterno + " "
                                                            + Objson.apellidoMaterno);
                            } else {
                                jWarning(auditoria.MENSAJE_SALIDA, "Atención");
                            }
                        } else {
                            jWarning(auditoria.MENSAJE_SALIDA, "Atención");
                        }
                    }
            }, 200);
        } else {
            jWarning("Numero dni debe tener 8 digitos", "Atención");
            return null;
        }

    } else {
        jWarning("Ingrese numero de dni", "Atención");
        return null; 
    }
}

function BuscarPersonalJuridica(_NumerDocumento) {
    if (_NumerDocumento != "") {
        if (_NumerDocumento.length == 11) {
        var item = {
            Ruc: _NumerDocumento
        };
        _blockUI('Buscando persona...');
        setTimeout(function () {
        var url = baseUrl + 'Recursiva/ServiciosWeb/Service_ConsultaRuc';
        var auditoria = SICA.Ajax(url, item, false);
        if (auditoria != null && auditoria != "") {
            if (auditoria.EJECUCION_PROCEDIMIENTO) {
                if (!auditoria.RECHAZAR) {
                    if (auditoria.OBJETO.TipoRespuesta == 1) {
                        $('#NOMBRES_APE').val(auditoria.OBJETO.RazonSocial);
                        $('#DIRECCION').val(auditoria.OBJETO.DomicilioFiscal);
                        if (auditoria.OBJETO.EstadoContribuyente == "ACTIVO") {
                            $('.RucActivo').show('slow');
                        } else {
                            $('.RucInactivo').show('slow');
                        }
                    } else {
                        jWarning("Ruc ingresado no existe.", "Atención");
                        $("#NOMBRES_APE").val('');
                    }

                } else {
                    jWarning(auditoria.MENSAJE_SALIDA, "Atención");
                }
            } else {
                jWarning(auditoria.MENSAJE_SALIDA, "Atención");
            }
        }
        }, 200);
        } else {
            jWarning("Numero ruc debe tener 11 digitos", "Atención");
            return null;
        }
    } else {
        jWarning("Ingrese ruc para buscar", "Atención");
    }

}
