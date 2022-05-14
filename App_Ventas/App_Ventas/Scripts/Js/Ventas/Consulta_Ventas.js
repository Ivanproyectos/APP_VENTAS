
function Ventas_Exportar(TIPO) {
    var Params = {
        FECHA_INICIO : jQuery('#Ventas_FechaRange').val() == '' ? null : "'" + jQuery('#Ventas_FechaRange').val().split('-')[0].trim() + "'",
        FECHA_FIN : jQuery('#Ventas_FechaRange').val() == '' ? null : "'" + jQuery('#Ventas_FechaRange').val().split('-')[1].trim() + "'",
        ID_USUARIO : jQuery('#ID_USUARIO').val() == '' ? null : "'" + jQuery('#ID_USUARIO').val() + "'",
        ID_SUCURSAL : jQuery('#ID_SUCURSAL').val() == '' ? null : "'" + jQuery('#ID_SUCURSAL').val() + "'",
        FLG_ANULADO: jQuery('#Ventas_FLG_ANULADO').val() == '' ? 2 : "'" + jQuery('#Ventas_FLG_ANULADO').val() + "'", // TODOS
        TIPO_EXPORT: TIPO
    }
    _blockUI("Descargando archivo...");
    jQuery("#myModalDescargar").html('');
    jQuery("#myModalDescargar").load(baseUrl + "Ventas/Consulta_Ventas/View_Exportar?" + $.param(Params), function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalDescargar');
        if (request.status == 200)
            $.unblockUI();
    });

}
