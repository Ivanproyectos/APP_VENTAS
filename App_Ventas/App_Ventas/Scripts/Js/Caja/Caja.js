
var Caja_Grilla = 'Caja_Grilla';
var Barra_Grilla = 'Barra_Grilla';

function Caja_ConfigurarGrilla() {
    $("#" + Caja_Grilla).GridUnload();
    var colNames = ['Editar','Eliminar','codigo', 'ID', 'Fecha y Hora', 'Tipo', 'Descripción','Monto', 'Usuario Creación'];
    var colModels = [
            { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 70, hidden: false, sortable: false, formatter: Caja_actionEditar, sortable: false },
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 70, hidden: false, sortable: false, formatter: Caja_actionEliminar, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_TIPO_MOVIMIENTO', index: 'ID_TIPO_MOVIMIENTO', align: 'center', width: 100, hidden: true, key: true },
            { name: 'FEC_CREACION', index: 'FEC_CREACION', align: 'left', width: 200, hidden: false },
            { name: 'TIPO', index: 'TIPO', align: 'left', width: 200, hidden: false },
            { name: 'DESC_MOVIMIENTO', index: 'DESC_MOVIMIENTO', align: 'left', width: 200, hidden: false },
            { name: 'MONTO', index: 'MONTO', align: 'left', width: 100, hidden: false },
            { name: 'USU_CREACION', index: 'USU_CREACION', align: 'left', width: 200, hidden: false },
        
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false
    };
    SICA.Grilla(Caja_Grilla, Barra_Grilla, Caja_Grilla, 400, '', "", '', 'ID_TIPO_MOVIMIENTO', colNames, colModels, '', opciones);
}


function Caja_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar'  onclick='Caja_MostrarEditar(" + rowObject.ID_TIPO_MOVIMIENTO + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\" data-target='#myModalNuevo'> <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:17px\"></i></button>";
    return _btn;
}

function Caja_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='Caja_Eliminar(" + rowObject.ID_TIPO_MOVIMIENTO + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
}



function Caja_Movimieto_MostrarNuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Caja/Caja/View_Movimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function Clientes_MostrarEditar(ID_TIPO_MOVIMIENTO) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Caja/Caja/View_Movimiento?id=" + ID_TIPO_MOVIMIENTO + "&Accion=M", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  producto **************************************************/

function Caja_CargarGrilla() {
    var item =
       {
           FEC_INICIO: $('#Caja_FechaInicio').val(),
           FEC_FIN: $('#Caja_FechaFin').val(),
           COD_USUARIO: $('#ID_USUARIO').val(),
           ID_SUCURSAL: $('#ID_SUCURSAL_SEARCH').val(),
       };
    var url = baseUrl + 'Caja/Caja/Caja_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            if (auditoria.OBJETO != null) {
                debugger;
                var _TotalVenta = auditoria.OBJETO.TOTAL_VENTA;
                var _TotalAdelanto = auditoria.OBJETO.TOTAL_ADELANTO;
                var _TotalCobrar = auditoria.OBJETO.TOTAL_COBRAR;
                var TotalIngresos = 0;
                var TotalEgresos = 0;
                var Total = (_TotalVenta + _TotalAdelanto + _TotalCobrar);
                $('#Caja_countVenta').text(auditoria.OBJETO.COUNT_VENTA);
                $('#Caja_countAdelanto').text(auditoria.OBJETO.COUNT_COBRAR);
                $('#Caja_countCobrar').text(auditoria.OBJETO.COUNT_ADELANTO);

                $('#Caja_TotalVeta').text(Number(_TotalVenta).toFixed(2));
                $('#Caja_TotalAdelanto').text(Number(_TotalAdelanto).toFixed(2));
                $('#Caja_TotalCobrar').text(Number(_TotalCobrar).toFixed(2));
                $('#Caja_Total').text(Number(Total).toFixed(2));          
            }
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}


