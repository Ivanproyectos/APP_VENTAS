function BuscarPersonalNatural(_NumeroDocumento) {
    if (_NumeroDocumento != "") {
        if (_NumeroDocumento.length == 8) {
            $.ajax({
                type: "POST",
                url: "https://snirh.ana.gob.pe/consultaspide/wsGetReniec.asmx/consultaDirectaReniec",
                contentType: "application/json; charset=utf-8",
                data: "{ pDniConsulta: " + _NumeroDocumento.toString() + " }",
                dataType: "json",
                // 20 s espera
                timeout: 20000,
                beforeSend: function () {
                    blockUI_('Buscando persona...'); 
                },
                response: function (data) {
                },
                success: function (jdata) {
                    jQuery.unblockUI();
                    var json = JSON.parse(jdata.d);
                    debugger; 
                    if (json.length > 0) {
                        if (json[0]['codRes'] != 1003) {
                          
                                $('#NOMBRES_APE').val(json[0]['nombres'] + ' ' + json[0]['apePat'] + ' ' + json[0]['apeMat'])
                                $('#DIRECCION').val(json[0]['dir'])
         
                        } else {
                            jWarning(json[0]['desResul'], "Atención");
                         
                        }
                    } else {
                        jWarning("Persona no econtrada", "Atención");
                        
                    }
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    jQuery.unblockUI();
                    console.log(xmlHttpRequest.responseText);
                    console.log(textStatus);
                    console.log(errorThrown);
                    return null;
                }

            });
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
            NUMERO_DOCUMENTO: _NumerDocumento
        };
        var url = baseUrl + 'Recursiva/ServiciosWeb/ConsultaRuc';
        var auditoria = SICA.Ajax(url, item, false);
        if (auditoria != null && auditoria != "") {
            if (auditoria.EJECUCION_PROCEDIMIENTO) {
                if (!auditoria.RECHAZAR) {
                    debugger;
                    //var XmlDatos = auditoria.OBJETO;
                    var json = JSON.parse(auditoria.OBJETO);
                    var json = JSON.parse(json.d);
                    if (json.length > 0) {
                        $('#NOMBRES_APE').val(json[0]['razon_social'].trim());
                        $('#DIRECCION').val(json[0]['domicilio'].trim());
                        $('#COD_UBIGEO').val(json[0]['ubigeo'].trim()).trigger('change');

                        if (json[0]['activo'].trim() == "ACTIVO") {
                            $('.RucActivo').show('slow'); 
                        } else {
                            $('.RucInactivo').show('slow');
                        }
   
                    } else {
                        jWarning("Ruc no econtrada", "Atención");
                        $("#NOMBRES_APE").val('');
                    }

                } else {
                    jWarning(auditoria.MENSAJE_SALIDA, "Atención");
                }
            } else {
                jWarning(auditoria.MENSAJE_SALIDA, "Atención");
            }
        }
        } else {
            jWarning("Numero ruc debe tener 11 digitos", "Atención");
            return null;
        }
    } else {
        jWarning("Ingrese ruc para buscar", "Atención");
    }

}
