
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




function Caja_CargarGrilla() {
    var lista = new Array();
    var item = {
        ID_CARGO: '1',
        DESC_CARGO: 'cargo XD',
        FLG_ESTADO: 1,
        FEC_CREACION: '12/02/2022'
    }

    var item2= {
        ID_CARGO: '1',
        DESC_CARGO: 'cargo XD',
        FLG_ESTADO: 1,
        FEC_CREACION: '12/02/2022'

    }

    lista.push(item); 
    lista.push(item2);

       jQuery("#" + Caja_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
       $.each(lista, function (i, v) {
                var idgrilla = i + 1;
                var myData =
                 {
                     CODIGO: idgrilla,
                     ID_CARGO: v.ID_CARGO,
                     DESC_CARGO: v.DESC_CARGO,
                     FLG_ESTADO: v.FLG_ESTADO,
                     FEC_CREACION: v.FEC_CREACION,
                     //USU_CREACION: v.USU_CREACION,
                     //FEC_MODIFICACION: v.FEC_MODIFICACION,
                     //USU_MODIFICACION: v.USU_MODIFICACION,
                     //IP_CREACION: v.IP_CREACION,
                     //IP_MODIFICACION: v.IP_MODIFICACION
                 };
                jQuery("#" + Caja_Grilla).jqGrid('addRowData', idgrilla, myData);
            });
            //jQuery("#" + Caja_Grilla).trigger("reloadGrid");
  
}


function Caja_MostrarNuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Admin/Prueba/Mantenimiento", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


function Preguntar() {
    debugger; 
    if ($('#frmMantenimientoCaja').valid()) {
        jConfirm("¿ Desea actualizar este cargo ?", "Atención", function (r) {
            if (r) {
                $('#myModalNuevo').modal('hide'); 
                jOkas('Registro guardado con exito', 'Atención');
                

            } else {
                jError('Ocurrio un error', 'Atención');
            }
        });
    }

}


