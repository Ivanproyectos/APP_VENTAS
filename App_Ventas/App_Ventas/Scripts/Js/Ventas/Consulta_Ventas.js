function Producto_ExportarProductoExcel() {
    var Params = {
        ID_SUCURSAL: _ID_SUCURSAL,
        FLG_SERVICIO: $('#Producto_FLG_SERVICIO').val(),
    }
    _blockUI("Descargando archivo...");
    jQuery("#myModalDescargar").html('');
    jQuery("#myModalDescargar").load(baseUrl + "Inventario/Producto/View_ExportarProductoExcel?" + $.param(Params), function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalDescargar');
        if (request.status == 200)
            $.unblockUI();
    });

}


function Producto_ExportarProductoPDF() {
    var Params = {
        ID_SUCURSAL: _ID_SUCURSAL,
        FLG_SERVICIO: $('#Producto_FLG_SERVICIO').val(),
    }
    _blockUI("Descargando archivo...");
    jQuery("#myModalDescargar").html('');
    jQuery("#myModalDescargar").load(baseUrl + "Inventario/Producto/View_ExportarProductoPDF?" + $.param(Params), function (responseText, textStatus, request) {
        $.validator.unobtrusive.parse('#myModalDescargar');
        if (request.status == 200)
            $.unblockUI();
    });

}
