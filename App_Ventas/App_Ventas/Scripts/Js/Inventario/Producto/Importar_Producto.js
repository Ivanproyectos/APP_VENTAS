
function Producto_MostrarImportarProducto() {
    var _ID_SUCURSAL = $('#ID_SUCURSAL').val();
    var _DESC_SUCURSAL = $('select[name="ID_SUCURSAL"] option:selected').text();
    if (_ID_SUCURSAL != "") {
        _DESC_SUCURSAL = _DESC_SUCURSAL.replace(/ /g, "+");
        jQuery("#myModalNuevo").html('');
        jQuery("#myModalNuevo").load(baseUrl + "Inventario/Importar/View_Importar?ID_SUCURSAL=" + _ID_SUCURSAL +
            "&DESC_SUCURSAL=" + _DESC_SUCURSAL, function (responseText, textStatus, request) {
                $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
                $.validator.unobtrusive.parse('#myModalNuevo');
                if (request.status != 200) return;
            });
    } else {
        jInfo('Actualmente tu está mirando los productos de todos los almacenes, debes seleccionar uno en específico donde deseas registrar el producto.', 'Atención')
    }
}

function Producto_DescargarPlantillaProducto() {
    jQuery("#myModalDescargar").html('');
    jQuery("#myModalDescargar").load(baseUrl + "Inventario/Importar_Producto/Importar_DescagarPlantilla", function (responseText, textStatus, request) {
    $.validator.unobtrusive.parse('#myModalDescargar');
        if (request.status != 200) return;
    });
    
}

