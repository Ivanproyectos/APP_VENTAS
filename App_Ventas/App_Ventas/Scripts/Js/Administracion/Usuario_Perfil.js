
var Usuarios_Perfil_Grilla = 'Usuarios_Perfil_Grilla';
var Usuarios_Perfil_Barra = 'Usuarios_Perfil_Barra';

function Usuarios_Perfil_ConfigurarGrilla() {
    $("#" +  Usuarios_Perfil_Grilla).GridUnload();
    var colNames = [ 'Eliminar','Estado','codigo', 'ID','Sucursal','Perfil', 'flg_estado', 'Fecha Creación', 'Usuario Creación',  'Fecha Modificación', 'Usuario Modificación'];
    var colModels = [
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 80, hidden: false, formatter: Cargo_actionEliminar, sortable: false },
            { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 70, hidden: false, sortable: false, formatter: Cargo_actionActivo, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_CARGO', index: 'ID_CARGO', align: 'center', width: 100, hidden: true, key: true },
            { name: 'DESC_SUCURSAL', index: 'DESC_SUCURSAL', align: 'left', width: 200, hidden: false },
             { name: 'DESC_PERFIL', index: 'DESC_PERFIL', align: 'left', width: 200, hidden: false },
            { name: 'FLG_ESTADO', index: 'FLG_ESTADO', align: 'left', width: 300, hidden: true },
            { name: 'FEC_CREACION', index: 'FEC_CREACION', align: 'left', width: 150, hidden: false },
            { name: 'USU_CREACION', index: 'USU_CREACION', align: 'left', width: 150, hidden: false },
            { name: 'FEC_MODIFICACION', index: 'FEC_MODIFICACION', align: 'left', width: 150, hidden: false },
            { name: 'USU_MODIFICACION', index: 'USU_MODIFICACION', align: 'left', width: 150, hidden: false },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false
    };
    SICA.Grilla( Usuarios_Perfil_Grilla, Usuarios_Perfil_Barra,  Usuarios_Perfil_Grilla, '', '', "", '', 'ID_CARGO', colNames, colModels, '', opciones);
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



