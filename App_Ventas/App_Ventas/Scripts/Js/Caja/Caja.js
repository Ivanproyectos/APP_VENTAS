
var Caja_Grilla = 'Caja_Grilla';
var Barra_Grilla = 'Barra_Grilla';

function Caja_ConfigurarGrilla() {
    $("#" + Caja_Grilla).GridUnload();
    var colNames = ['codigo', 'ID', 'Fecha y Hora', 'Tipo', 'Efec. Anterior', 'Cantidad', 'Efec. Actual', 'flg_estado',
        'Usuario Creación', 'Motivo'];
    var colModels = [
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_CARGO', index: 'ID_CARGO', align: 'center', width: 100, hidden: true, key: true },
            { name: 'FECH_HORA', index: 'FECH_HORA', align: 'left', width: 200, hidden: false },
            { name: 'TIPO', index: 'TIPO', align: 'left', width: 200, hidden: false },
            { name: 'EFECTIVO_ANTERIOR', index: 'EFECTIVO_ANTERIOR', align: 'left', width: 200, hidden: false },
            { name: 'CANTIDAD', index: 'CANTIDAD', align: 'left', width: 100, hidden: false },
            { name: 'EFECTIVO_ACTUAL', index: 'CANTIDAD', align: 'left', width: 200, hidden: false },
            { name: 'FLG_ESTADO', index: 'FLG_ESTADO', align: 'left', width: 300, hidden: true },
            { name: 'USU_CREACION', index: 'USU_CREACION', align: 'left', width: 150, hidden: false },
            { name: 'MOTIVO', index: 'MOTIVO', align: 'center', width: 70, hidden: false, sortable: false, formatter: Caja_actionActivo, sortable: false },          
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false
    };
    SICA.Grilla(Caja_Grilla, Barra_Grilla, Caja_Grilla, 400, '', "", '', 'ID_CARGO', colNames, colModels, '', opciones);
}


function Caja_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject.FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = " <label class=\"content_toggle_1\">"
            + "<input id=\"Vehiculos_chk_" + rowObject.ID_CARGO + "\" class=\"toggle_Beatiful_1\" type=\"checkbox\" onchange=\"Caja_Estado(" + rowObject.ID_CARGO + ",this)\" " + check_ + ">"
            + "<div class=\"content_toggle_2\">"
            + "  <span class=\"Label_toggle_1\" ></span>"
             + "</div>"
            + "</label>";
    return _btn;
}

function Caja_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar'  onclick='Caja_MostrarEditar(" + rowObject.ID_CARGO + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\" data-target='#myModalNuevo'> <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:17px\"></i></button>";
    return _btn;
}

function Caja_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='Caja_Eliminar(" + rowObject.ID_CARGO + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
}




///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  producto **************************************************/

function Caja_CargarGrilla() {
    var item =
       {
           FEC_INICIO: $('#Caja_FechaInicio').val(),
           FEC_FIN: $('#Caja_FechaFin').val(),
           COD_USUARIO: $('#ID_USUARIO').val(),
           ID_SUCURSAL: $('#ID_SUCURSAL').val(),
       };
    var url = baseUrl + 'Caja/Caja/Caja_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            if (auditoria.OBJETO != null) {
                debugger; 
                $('#Caja_countVenta').text(auditoria.OBJETO.COUNT_VENTA);
                $('#Caja_countAdelanto').text(auditoria.OBJETO.COUNT_COBRAR);
                $('#Caja_countCobrar').text(auditoria.OBJETO.COUNT_ADELANTO);

                $('#Caja_TotalVeta').text(Number(auditoria.OBJETO.TOTAL_VENTA).toFixed(2));
                $('#Caja_TotalAdelanto').text(Number(auditoria.OBJETO.TOTAL_COBRAR).toFixed(2));
                $('#Caja_TotalCobrar').text(Number(auditoria.OBJETO.TOTAL_ADELANTO).toFixed(2));

            }
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}
