
function Ventas_BuscarProducto(COD_PRODUCTO, ID_SUCURSAL) {
    var item =
       {
           COD_PRODUCTO: COD_PRODUCTO,
           ID_SUCURSAL: ID_SUCURSAL

       };
    var url = baseUrl + 'Ventas/Ventas/Producto_Buscar_Listar';
    var Lista = SICA.Ajax(url, item, false);
    if (Lista.length < 2) {
        $.each(Lista, function (i, v) {
            $('#_DetallePorductos').show('slow');
            $("#SEARCH_PRODUCTO").autocomplete("disable"); // DESACTIVA AUTOCOMPLETE
            $("#SEARCH_PRODUCTO").val(v.DESC_PRODUCTO); 
            $("#ID_UNIDAD_MEDIDA").val(v.ID_UNIDAD_MEDIDA);
            $("#COD_PRODUCTO").val(v.COD_PRODUCTO);
            $("#INPUT_STOCK").text(v.STOCK);
            $("#PRECIO_VENTA").val(Number(v.PRECIO_VENTA).toFixed(2));
            $("#DETALLE").val(v.DETALLE);
            $("#hfd_STOCK").val(v.STOCK);
            $('#TOTAL').val(Number(v.PRECIO_VENTA).toFixed(2));
            $('#hfd_ID_PRODUCTO').val(v.ID_PRODUCTO);
            $('#CANTIDAD').val(1);
            $('#CANTIDAD').focus();
            setTimeout(function () { $("#SEARCH_PRODUCTO").autocomplete("enable") }, 800); // ACTIVA AUTOCOMPLETE
            _Valido = true;
        });
    } else {
        jError("Se encontro mas de un producto con este codigo, verifique que el producto no tenga codigos duplicados.", "Atención");
    }
}
/*surcut enter*/
function Ventas_Detalle_Insertar() {
    if (_Valido) {
        if ($('#frmMantenimiento_BuscarProducto').valid()) {
            var rowKey = jQuery("#" + _Grilla).getDataIDs();
            var ix = rowKey.length;
            ix++;
            var myData =
                  {
                      CODIGO: ix,
                      ID_PRODUCTO: $("#hfd_ID_PRODUCTO").val(),
                      PRODUCTO: $("#SEARCH_PRODUCTO").val(),
                      PRECIO: Number($("#PRECIO_VENTA").val()).toFixed(2),
                      CANTIDAD: $("#CANTIDAD").val(),
                      IMPORTE: Number($("#TOTAL").val()).toFixed(2)
                  };

            if (Ventas_Detalle_BuscarProducto_Grilla($('#hfd_ID_PRODUCTO').val())) {
                jError('Producto seleccionado ya se encuentra en la lista.', 'Atención');
            } else {
                jQuery("#" + _Grilla).jqGrid('addRowData', ix, myData);
                CalcularMontoTotalDetalle();
            }
            LimpiarFormulario();
            $('#_DetallePorductos').hide('');
            $('#SEARCH_PRODUCTO').val('');
            $('#SEARCH_PRODUCTO').focus();
        }
    } else {
        jError('Debe buscar un producto para ingresar a la lista. No seas imbecil', 'Atención');
    }

}


function Fn_Ventas_Vuelto() {
    var _Total = isNaN(parseFloat($('#Venta_Total').text())) ? 0 : parseFloat($('#Venta_Total').text());
    var _PagoCon = isNaN(parseFloat($('#TOTAL_RECIBIDO').val())) ? 0 : parseFloat($('#TOTAL_RECIBIDO').val());
    //if (_Total < _PagoCon) {
        var _Vuelto = (_PagoCon - _Total);
    //} else {
    //    jError('Pago no puede ser menor al total.', 'Atención');
    //    _Vuelto = 0.0;
    //}
    $('#VUELTO').val(Number(_Vuelto).toFixed(2));
}