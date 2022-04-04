
var Cargo_Grilla = 'Cargo_Grilla';
var Cargo_Barra = 'Cargo_Barra';

function Cargo_ConfigurarGrilla() {
    $("#" + Cargo_Grilla).GridUnload();
    var colNames = [ 'Editar','Eliminar','Estado','codigo', 'ID', 'DESCRIPCION', 'flg_estado', 'Fecha Creación', 'Usuario Creación', 'IP Creación', 'Fecha Modificación', 'Usuario Modificación', 'IP Modifiación'];
    var colModels = [
            { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 60, hidden: false, formatter: Cargo_actionEditar, sortable: false },
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 80, hidden: false, formatter: Cargo_actionEliminar, sortable: false },
            { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 70, hidden: false, sortable: false, formatter: Cargo_actionActivo, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_CARGO', index: 'ID_CARGO', align: 'center', width: 100, hidden: true, key: true },
            { name: 'DESC_CARGO', index: 'DESC_CARGO', align: 'left', width: 300, hidden: false },
            { name: 'FLG_ESTADO', index: 'FLG_ESTADO', align: 'left', width: 200, hidden: true },
            { name: 'FEC_CREACION', index: 'FEC_CREACION', align: 'left', width: 200, hidden: false },
            { name: 'USU_CREACION', index: 'USU_CREACION', align: 'left', width: 200, hidden: false },
            { name: 'IP_CREACION', index: 'IP_CREACION', align: 'left', width: 200, hidden: true },
            { name: 'FEC_MODIFICACION', index: 'FEC_MODIFICACION', align: 'left', width: 200, hidden: false },
            { name: 'USU_MODIFICACION', index: 'USU_MODIFICACION', align: 'left', width: 200, hidden: false },
            { name: 'IP_MODIFICACION', index: 'IP_MODIFICACION', align: 'left', width: 200, hidden: true },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, autowidth: true,
    };
    SICA.Grilla(Cargo_Grilla, Cargo_Barra, Cargo_Grilla, 400, '', "Lista de Cargos", '', 'ID_CARGO', colNames, colModels, '', opciones);
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




function Cargo_CargarGrilla() {
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

       jQuery("#" + Cargo_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
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
                jQuery("#" + Cargo_Grilla).jqGrid('addRowData', idgrilla, myData);
            });
            //jQuery("#" + Cargo_Grilla).trigger("reloadGrid");
  
}


function Cargo_MostrarNuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Admin/Prueba/Mantenimiento", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


function Preguntar() {
    debugger; 
    if ($('#frmMantenimientoCargo').valid()) {
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


