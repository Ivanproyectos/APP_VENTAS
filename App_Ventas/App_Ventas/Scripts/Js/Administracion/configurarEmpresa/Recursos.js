

$("#fileField-isotipo").change(function () {
    var TIPO = 1; // ISOTIPO
    var input = document.getElementById('fileField-isotipo');
    var file = input.files[0];
    if (file != undefined) {
        var PesodeArchivo = parseFloat(file.size);
        var ext = input.files[0].name.split('.').pop();
        var nombre = input.files[0].name;
        if (nombre.length > 100) {
            jWarning("El nombre del documento es muy largo", 'Alerta');
            $(this).val('');
            return false;
        }
        else {
            var valido = false;
            if (ext.toLowerCase() == "png" || ext.toLowerCase() == "jpeg" || ext.toLowerCase() == "jpg")
                valido = true;
            if (PesodeArchivo > Tamanio_Valido || !valido) {
                $(this).val('');
                if (!valido)
                    jWarning("Solo se permite documentos en formato word(.png,.jpeg,.jpg)", 'Alerta');
                else
                    jWarning("La cantidad de el archivo que va adjuntar no pueden pesar más de " + Tamanio_Valido / 1024 / 1024 + "Mb", 'Alerta');
                return false;
            } else {
                //$("#lbl_file").html(nombre);
                ConfigurarEmpresa_GuardarTemporal(TIPO);
            }
            //archivo_cambiado = true;
        }
    } else {
        $(this).val('');
        //DocumentoArchivo_Error();
        return false;
    }

});





$("#fileField-logo").change(function () {
    //$("#NOMBRE_ARCHIVO").val('');
    //$("#lbl_file").html("Seleccionar archivo");
    //if ($("#FrmPlantillaNuevoArchivo").valid()) {
    var TIPO = 2; // ISOTIPO
    var input = document.getElementById('fileField-logo');
    var file = input.files[0];
    if (file != undefined) {
        var PesodeArchivo = parseFloat(file.size);
        var ext = input.files[0].name.split('.').pop();
        var nombre = input.files[0].name;
        if (nombre.length > 100) {
            jWarning("El nombre del documento es muy largo", 'Alerta');
            $(this).val('');
            return false;
        }
        else {
            var valido = false;
            if (ext.toLowerCase() == "png" || ext.toLowerCase() == "jpeg" || ext.toLowerCase() == "jpg")
                valido = true;
            if (PesodeArchivo > Tamanio_Valido || !valido) {
                $(this).val('');
                if (!valido)
                    jWarning("Solo se permite documentos en formato Imagen(.png,.jpeg,.jpg)", 'Alerta');
                else
                    jWarning("La cantidad de el archivo que va adjuntar no pueden pesar más de " + Tamanio_Valido / 1024 / 1024 + "Mb", 'Alerta');
                return false;
            } else {
                //$("#lbl_file").html(nombre);
                ConfigurarEmpresa_GuardarTemporal(TIPO);
            }
            //archivo_cambiado = true;
        }
    } else {
        $(this).val('');
        //DocumentoArchivo_Error();
        return false;
    }

});




function ConfigurarEmpresa_GuardarTemporal(TIPO) {
   
    var ErrorUrl = '';
    var url = baseUrl + "Administracion/Archivo/Guardar_Temporal_Archivo";
    var options = {
        type: "POST",
        dataType: "json",
        contentType: false,
        url: url,
        resetForm: false,
        beforeSubmit: function (formData, jqForm, options) {
            blockUI_("Subiendo Archivo...");
        },
        success: function (response, textStatus, jqXHR) {
            if (response.EJECUCION_PROCEDIMIENTO) {
                jQuery.unblockUI();
                if (TIPO == 1) {
                    configurarEmpresaIsotipo = true;
                    IsotipoNuevo_Array = new Array();
                    IsotipoNuevo_Array.push(response.OBJETO);
                    $('#Isotipo_Img').css('background-image', 'url(' + MiSistema + response.OBJETO.RUTA_LINK + ')');
                } else if (TIPO == 2) {
                    configurarEmpresaLogo = true;
                    LogoNuevo_Array = new Array();
                    LogoNuevo_Array.push(response.OBJETO);
                    $('#Logo_Img').css('background-image', 'url(' + MiSistema + response.OBJETO.RUTA_LINK + ')');
                }
            }
            else {
                jDanger(response.MENSAJE_SALIDA, 'Atención');
            }
        },
        error: function (jqXHR, textStatus, errorThrown) { window.location = ErrorUrl; jQuery.unblockUI(); }
    };
    if (TIPO == 1) {
        $("#FrmIsotipoArchivo").ajaxForm(options);
        $("#FrmIsotipoArchivo").submit();
    } else if (TIPO == 2) {
        $("#FrmLogoArchivo").ajaxForm(options);
        $("#FrmLogoArchivo").submit();
    }
}






$("#file-upload").change(function () {

    //$("#NOMBRE_ARCHIVO").val('');
    //$("#lbl_file").html("Seleccionar archivo");
    //if ($("#FrmPlantillaNuevoArchivo").valid()) {
    var input = document.getElementById('fileField-logo');
    var file = input.files[0];
    if (file != undefined) {
        var PesodeArchivo = parseFloat(file.size);
        var ext = input.files[0].name.split('.').pop();
        var nombre = input.files[0].name;
        if (nombre.length > 100) {
            jAlert("El nombre del documento es muy largo", 'Alerta');
            $(this).val('');
            return false;
        }
        else {
            var valido = false;
            if (ext.toLowerCase() == "png" || ext.toLowerCase() == "jpeg" || ext.toLowerCase() == "jpg")
                valido = true;
            if (PesodeArchivo > Tamanio_Valido || !valido) {
                $(this).val('');
                if (!valido)
                    jAlert("Solo se permite documentos en formato Imagen(.png,.jpeg,.jpg)", 'Alerta');
                else
                    jAlert("La cantidad de el archivo que va adjuntar no pueden pesar más de " + Tamanio_Valido / 1024 / 1024 + "Mb", 'Alerta');
                return false;
            } else {
                //$("#lbl_file").html(nombre);
                CabeceraNuevo_GuardarTemporal();
            }
            archivo_cambiado = true;
        }
    } else {
        $(this).val('');
        //DocumentoArchivo_Error();
        return false;
    }

});
