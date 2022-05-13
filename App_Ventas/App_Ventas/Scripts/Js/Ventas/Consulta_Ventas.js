function Ventas_ExportarVentasExcel() {
    var Params = {
        FECHA_INICIO: jQuery('#Ventas_FechaRange').val() == '' ? null : "'" + jQuery('#Ventas_FechaRange').val().split('-')[0].trim() + "'",
        FECHA_FIN: jQuery('#Ventas_FechaRange').val() == '' ? null : "'" + jQuery('#Ventas_FechaRange').val().split('-')[1].trim() + "'",
        USUARIO: jQuery('#ID_USUARIO').val() == '' ? null : "'" + jQuery('#ID_USUARIO').val() + "'",
        ID_SUCURSAL: jQuery('#ID_SUCURSAL').val() == '' ? null : "'" + jQuery('#ID_SUCURSAL').val() + "'",
        FLG_ANULADO: jQuery('#Ventas_FLG_ANULADO').val() == '' ? null : "'" + jQuery('#Ventas_FLG_ANULADO').val() + "'",
    }
    _blockUI("Descargando archivo...");
    jQuery("#myModalDescargar").html('');
    jQuery("#myModalDescargar").load(baseUrl + "Ventas/Consulta_Ventas/View_ExportarVentasExcel?" + $.param(Params), function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalDescargar');
        if (request.status == 200)
            $.unblockUI();
    });

}

function Ventas_ExportarVentasPDF() {
    var Params = {
        FECHA_INICIO : jQuery('#Ventas_FechaRange').val() == '' ? null : "'" + jQuery('#Ventas_FechaRange').val().split('-')[0].trim() + "'",
        FECHA_FIN : jQuery('#Ventas_FechaRange').val() == '' ? null : "'" + jQuery('#Ventas_FechaRange').val().split('-')[1].trim() + "'",
        ID_USUARIO : jQuery('#ID_USUARIO').val() == '' ? null : "'" + jQuery('#ID_USUARIO').val() + "'",
        ID_SUCURSAL : jQuery('#ID_SUCURSAL').val() == '' ? null : "'" + jQuery('#ID_SUCURSAL').val() + "'",
        FLG_ANULADO : jQuery('#Ventas_FLG_ANULADO').val() == '' ? null : "'" + jQuery('#Ventas_FLG_ANULADO').val() + "'",
    }
    _blockUI("Descargando archivo...");
    jQuery("#myModalDescargar").html('');
    jQuery("#myModalDescargar").load(baseUrl + "Ventas/Consulta_Ventas/View_ExportarVentasPDF?" + $.param(Params), function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalDescargar');
        if (request.status == 200)
            $.unblockUI();
    });

}
