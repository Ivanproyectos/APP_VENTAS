

function _GuardarTemporal() {

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
                CambioImg = true;
                Img_Array = new Array();
                Img_Array.push(response.OBJETO);
                $('#Img_Producto').css('background-image', 'url(' + MiSistema + response.OBJETO.RUTA_LINK + ')');
            }
            else {
                jDanger(response.MENSAJE_SALIDA, 'Atención');
            }
        },
        error: function (jqXHR, textStatus, errorThrown) { window.location = ErrorUrl; jQuery.unblockUI(); }
    };
    $("#frmMantenimiento_ImagenProducto").ajaxForm(options);
    $("#frmMantenimiento_ImagenProducto").submit();

}

function ConvertKilos_Gramos(_KILOS) {
    var Gramos = (_KILOS * 1000);
    return Gramos; 
}

function ConvertGramos_Kilos(_GRAMOS) {
    var Kilos = (_GRAMOS / 1000);
    return Kilos;
}

