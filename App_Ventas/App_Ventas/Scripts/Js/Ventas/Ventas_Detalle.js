
var Ventas_Detalle_Grilla = 'Ventas_Detalle_Grilla';
var Ventas_Detalle_Barra = 'Ventas_Detalle_Barra';

function Ventas_Detalle_ConfigurarGrilla(TIPO) {
    var _btnBorrarHidden = false;
    var _btnDevolverHidden = false;
    if (TIPO == "DEVOLVER") {
        _btnBorrarHidden = true;
    } else if (TIPO == "DETALLE") {
        var _btnBorrarHidden = true;
        var _btnDevolverHidden = true;
    }else if(TIPO == "VENTAS"){
        var _btnBorrarHidden = false;
        var _btnDevolverHidden = true;
    }

    $("#" +  Ventas_Detalle_Grilla).GridUnload();
    var colNames = [ 'Eliminar','ID_DETALLE','codigo', 'ID_PRODUCTO','Producto','Precio', 'Cantidad','Importe','flg_devuelto','Devolver','Accion'];
    var colModels = [
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 80, hidden: _btnBorrarHidden, formatter: Ventas_Detalle_FormatterBorrar, sortable: false },
            { name: 'ID_VENTA_DETALLE', index: 'ID_VENTA_DETALLE', align: 'center', width: 100, hidden: true, },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true,  key: true },
            { name: 'ID_PRODUCTO', index: 'ID_PRODUCTO', align: 'center', width: 100, hidden: true },
            { name: 'PRODUCTO', index: 'PRODUCTO', align: 'left', width: 300, hidden: false },
            { name: 'PRECIO', index: 'PRECIO', align: 'left', width: 100, hidden: false },
            { name: 'CANTIDAD', index: 'CANTIDAD', align: 'left', width: 100, hidden: false },
            { name: 'IMPORTE', index: 'IMPORTE', align: 'left', width: 100, hidden: false },
            { name: 'FLG_DEVUELTO', index: 'FLG_DEVUELTO', align: 'left', width: 100, hidden: true },
            { name: 'DEVOLVER', index: 'DEVOLVER', align: 'center', width: 80, hidden: _btnDevolverHidden, formatter: Ventas_Detalle_FormatterDevolver, sortable: false },
            { name: 'ACCION', index: 'ACCION', align: 'left', width: 100, hidden: true },

    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false
    };
    SICA.Grilla(Ventas_Detalle_Grilla, Ventas_Detalle_Barra, Ventas_Detalle_Grilla, 200, '', "", '', 'CODIGO', colNames, colModels, '', opciones);
}

/* eliminar fila */
function Ventas_Detalle_FormatterBorrar(cellvalue, options, rowObject) {
    var _accion = rowObject.ACCION;
    if (_accion == "N") {
       var _btn_Eliminar = "<button title='Eliminar producto'  onclick='Ventas_Detalle_Borrar(" + rowObject.CODIGO + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>"
    } else {
       var _btn_Eliminar = "<button title='Eliminar producto'   class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:gray;font-size:17px\"></i></button>"
    }

    return _btn_Eliminar;
}
    
function Ventas_Detalle_Borrar(id) {
    $("#" + Ventas_Detalle_Grilla).jqGrid('delRowData', id);
    // $("#" + grillaProductos).trigger("reloadGrid");
    CalcularMontoTotalDetalle();
    UpdateRowId();
}

// actualizar rowid despues de eliminar un pro
function UpdateRowId() {
    var ListaDetalleProductos = $("#" + Ventas_Detalle_Grilla).jqGrid('getGridParam', 'data');
    for (var i = 0; i < ListaDetalleProductos.length; i++) {
        var resetId = i + 1;
        var rowData = ListaDetalleProductos[i];
        $("#" + Ventas_Detalle_Grilla).jqGrid('setCell', rowData.CODIGO, 'CODIGO', resetId);
    }

}

function Ventas_Detalle_BuscarProducto_Grilla(ID_PRODUCTO) {
    debugger;
    var buscado = false;
    var ids = $("#" + Ventas_Detalle_Grilla).getDataIDs();
    for (var i = 0; i < ids.length; i++) {
        var rowId = ids[i];
        var rowData = $("#" + Ventas_Detalle_Grilla).jqGrid('getRowData', rowId);
        if (rowData.ID_PRODUCTO == ID_PRODUCTO) return true;
    }
    return buscado;
}


function CalcularMontoTotalDetalle() {
    var ids = $("#" + Ventas_Detalle_Grilla).getDataIDs();
    var _subtotal = 0;
    var _descuento = isNaN(parseFloat($('#DESCUENTO').val())) ? 0 : parseFloat($('#DESCUENTO').val());
    var _Adelanto = isNaN(parseFloat($('#ADELANTO').val())) ? 0 : parseFloat($('#ADELANTO').val());
    var _Igv = 0; 
    var _Total = 0;
    var _TotalDebe = 0; 
    for (var i = 0; i < ids.length; i++) {
        var rowId = ids[i];
        var rowData = $("#" + Ventas_Detalle_Grilla).jqGrid('getRowData', rowId);
        _Total += parseFloat(rowData.IMPORTE)
    }
    if (_descuento < _Total)
        _Total = (_Total - _descuento);
    else {
        jError('El descuento no puede ser mayor al total.', 'Atención');
        _descuento = 0.0; 
    }

    _Igv = Math.floor(_Total * _Impuesto) / 100;
    _subtotal = _Total - _Igv;
    _TotalDebe = (_Total - _Adelanto);

    $('#Venta_Descuento').text(Number(_descuento).toFixed(2));
    $('#Venta_Igv').text( Number(_Igv).toFixed(2));
    $('#Venta_Subtotal').text( Number(_subtotal).toFixed(2));
    $('#Venta_Total').text(Number(_Total).toFixed(2));

    $('#Venta_TotalDebe').text(Number(_TotalDebe).toFixed(2));
}


function Ventas_Detalle_FormatterDevolver(cellvalue, options, rowObject) {
    var _FLG_DEVUELTO = rowObject.FLG_DEVUELTO
    if (_FLG_DEVUELTO == 0) {
        var _btn_devolver = "<button title='Devolver producto'  onclick='Ventas_DevolverProducto(" + rowObject.ID_VENTA_DETALLE + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-box-arrow-in-down-left\" style=\"color:green;font-size:17px\"></i></button>"
    } else {
        var _btn_devolver = "<button title='Este producto ya fue devuelto.'  class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-box-arrow-in-down-left\" style=\"color:gray;font-size:17px\"></i></button>"
    }
    return _btn_devolver;
}



function Ventas_Detalle_CargarGrilla(ID_VENTA) {
    var item =
       {
           ID_VENTA: ID_VENTA,
       };
    var url = baseUrl + 'Ventas/Ventas/ventas_Detalle_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    jQuery("#" + Ventas_Detalle_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;
                var myData =
                 {
                     CODIGO: idgrilla,
                     ID_VENTA_DETALLE : v.ID_VENTA_DETALLE,
                     ID_PRODUCTO: v.ID_PRODUCTO,
                     PRODUCTO: v.DESC_PRODUCTO,
                     PRECIO: Number(v.PRECIO).toFixed(2), 
                     CANTIDAD: v.CANTIDAD,
                     IMPORTE:  Number(v.IMPORTE).toFixed(2), 
                     FLG_DEVUELTO: v.FLG_DEVUELTO,
                     ACCION : "M"
                 };
                jQuery("#" + Ventas_Detalle_Grilla).jqGrid('addRowData', idgrilla, myData);
            });
            jQuery("#" + Ventas_Detalle_Grilla).trigger("reloadGrid");
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}

