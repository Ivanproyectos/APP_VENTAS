
var Compras_Detalle_Grilla = 'Compras_Detalle_Grilla';
var Compras_Detalle_Barra = 'Compras_Detalle_Barra';

function Compras_Detalle_ConfigurarGrilla() {
    $("#" +  Compras_Detalle_Grilla).GridUnload();
    var colNames = [ 'Eliminar','codigo', 'ID','Producto','Precio', 'Cantidad', 'Igv', 'Importe'];
    var colModels = [
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 80, hidden: false, formatter: Cargo_actionEliminar, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_CARGO', index: 'ID_CARGO', align: 'center', width: 100, hidden: true, key: true },
            { name: 'PRODUCTO', index: 'PRODUCTO', align: 'left', width: 200, hidden: false },
            { name: 'PRECIO', index: 'PRECIO', align: 'left', width: 100, hidden: false },
            { name: 'CANTIDAD', index: 'CANTIDAD', align: 'left', width: 100, hidden: false },
            { name: 'IGV', index: 'IGV', align: 'left', width: 100, hidden: false },
            { name: 'IMPORTE', index: 'IMPORTE', align: 'left', width: 100, hidden: false },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false
    };
    SICA.Grilla( Compras_Detalle_Grilla, Compras_Detalle_Barra,  Compras_Detalle_Grilla, 200, '', "", '', 'ID_CARGO', colNames, colModels, '', opciones);
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



