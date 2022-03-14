
var Ventas_Detalle_Grilla = 'Ventas_Detalle_Grilla';
var Ventas_Detalle_Barra = 'Ventas_Detalle_Barra';

function Ventas_Detalle_ConfigurarGrilla() {
    $("#" +  Ventas_Detalle_Grilla).GridUnload();
    var colNames = [ 'Eliminar','codigo', 'ID','Producto','Precio', 'Cantidad','Importe'];
    var colModels = [
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 80, hidden: false, formatter: Cargo_actionEliminar, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_PRODUCTO', index: 'ID_PRODUCTO', align: 'center', width: 100, hidden: true, key: true },
            { name: 'PRODUCTO', index: 'PRODUCTO', align: 'left', width: 300, hidden: false },
            { name: 'PRECIO', index: 'PRECIO', align: 'left', width: 100, hidden: false },
            { name: 'CANTIDAD', index: 'CANTIDAD', align: 'left', width: 100, hidden: false },
            { name: 'IMPORTE', index: 'IMPORTE', align: 'left', width: 100, hidden: false },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false
    };
    SICA.Grilla( Ventas_Detalle_Grilla, Ventas_Detalle_Barra,  Ventas_Detalle_Grilla, 200, '', "", '', 'ID_CARGO', colNames, colModels, '', opciones);
}


function Cargo_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject.FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = " <label class=\"content_toggle_1\">"
            + "<input id=\"Vehiculos_chk_" + rowObject.ID_CARGO + "\" class=\"toggle_Beatiful_1\" type=\"checkbox\" onchange=\"Cargo_Estado(" + rowObject.ID_CARGO + ",this)\" " + check_ + ">"
            + "<div class=\"content_toggle_2\">"
            + "  <span class=\"Label_toggle_1\" ></span>"
             + "</div>"
            + "</label>";
    return _btn;
}

function Cargo_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar'  onclick='Cargo_MostrarEditar(" + rowObject.ID_CARGO + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\" data-target='#myModalNuevo'> <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:17px\"></i></button>";
    return _btn;
}

function Cargo_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='Cargo_Eliminar(" + rowObject.ID_CARGO + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
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
    var _Igv = 0; 
    var _Total = 0;
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
    _subtotal =_Total - _Igv ;

    $('#Venta_Descuento').text(Number(_descuento).toFixed(2));
    $('#Venta_Igv').text( Number(_Igv).toFixed(2));
    $('#Venta_Subtotal').text( Number(_subtotal).toFixed(2));
    $('#Venta_Total').text( Number(_Total).toFixed(2));
}

