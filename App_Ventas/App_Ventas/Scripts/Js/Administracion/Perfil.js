
var Perfil_Grilla = 'Perfil_Grilla';
var Perfil_Barra = 'Perfil_Barra';

//$(document).ready(function () {
//    Perfil_ConfigurarGrilla();
//});

function Perfil_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Perfil_Limpiar() {
    $("#txtdesPerfil").val('');
    $('#cboEstado').val('');

    Perfil_CargarGrilla();
}

function Perfil_ConfigurarGrilla() {
    $("#" + Perfil_Grilla).GridUnload();
    var colNames = ['Editar', 'Eliminar', 'Estado', 'codigo', 'ID', 'perfil', 
        'flg_estado', 'Fecha Creación', 'Usuario Creación', 'Fecha Modificación', 'Usuario Modificación'];
    var colModels = [
            { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 60, hidden: false, formatter: Perfil_actionEditar, sortable: false },
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 80, hidden: false, formatter: Perfil_actionEliminar, sortable: false },
            { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 70, hidden: false, sortable: true, formatter: Perfil_actionActivo, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_PERFIL', index: 'ID_PERFIL', width: 100, hidden: true, key: true },
            { name: 'DESC_PERFIL', index: 'DESC_PERFIL', width: 300, hidden: false, align: "left"   ,search: true },
            { name: 'FLG_ESTADO', index: 'FLG_ESTADO', width: 300, hidden: true, align: "left" },
            { name: 'FEC_CREACION', index: 'FEC_CREACION', width: 150, hidden: false, align: "left" },
            { name: 'USU_CREACION', index: 'USU_CREACION', width: 150, hidden: false, align: "left" },
            { name: 'FEC_MODIFICACION', index: 'FEC_MODIFICACION', width: 150, hidden: false, align: "left" },
            { name: 'USU_MODIFICACION', index: 'USU_MODIFICACION', width: 150, hidden: false, align: "left" },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, rowNumber: 50, rowNumbers: [50, 100, 200, 300, 500],
        gridCompleteFunc: function () {
     
            var allJQGridData = $("#" + _grilla).jqGrid('getRowData');
            if (allJQGridData.length == 0) {
                $(".ui-jqgrid-hdiv").css("overflow-x", "auto");
            }
        }
    };
    SICA.Grilla(Perfil_Grilla, Perfil_Barra, Perfil_Grilla, 400, '', "Lista de Perfil", '', 'ID_PERFIL', colNames, colModels, '', opciones);
}

function Perfil_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject.FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = " <label class=\"content_toggle_1\">"
            + "<input id=\"Perfil_chk_" + rowObject.ID_PERFIL + "\" class=\"toggle_Beatiful_1\" type=\"checkbox\" onchange=\"Perfil_Estado(" + rowObject.ID_PERFIL + ",this)\" " + check_ + ">"
            + "<div class=\"content_toggle_2\">"
            + "  <span class=\"Label_toggle_1\" ></span>"
             + "</div>"
            + "</label>";
    return _btn;
}

function Perfil_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar'  onclick='Perfil_MostrarEditar(" + rowObject.ID_PERFIL + ");' class=\"btn btn-outline-light\" type=\"button\" > <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:17px\"></i></button>";
    return _btn;
}

function Perfil_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='Perfil_Eliminar(" + rowObject.CODIGO + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
}



function Perfil_MostrarEditar(CODIGO) {
    $('#AccionPerfil').val('M');
    $('#hfd_ID_PERFIL').val(CODIGO);
    var _DataPerfil = jQuery('#' + Perfil_Grilla).jqGrid('getRowData', CODIGO);
    $('#DESC_PERFIL').val(_DataPerfil.DESC_PERFIL);
    $('#hfd_ID_PERFIL').val(_DataPerfil.ID_PERFIL);
    $('#_btnGuardar_text').html('<span class="btn-icon-left text-secondary"><i class="bi bi-pencil-fill"></i> </span>Editar');
    $('#Perfil_btnCancelar').show('slow');

}


function Perfil_CancelarEditar() {
    $('#DESC_PERFIL').val('');
    $('#AccionPerfil').val('N');
    $('#hfd_ID_PERFIL').val('0');
    $('#Perfil_btnCancelar').hide('slow');
    $('#_btnGuardar_text').html('<span class="btn-icon-left text-secondary"><i class="fa fa-plus color-secondary"></i></span>Agregar');
}

///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  perfil **************************************************/

function Perfil_CargarGrilla() {
    var item =
       {

           //DESC_PERFIL: $('#DESC_PERFIL').val(),
           FLG_ESTADO: 2 // todos
       };
    var url = baseUrl + 'Administracion/Perfil/Perfil_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    jQuery("#" + Perfil_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;
                var myData =
                 {
                     CODIGO: idgrilla,
                     ID_PERFIL: v.ID_PERFIL,
                     DESC_PERFIL: v.DESC_PERFIL,
                     FLG_ESTADO: v.FLG_ESTADO,
                     FEC_CREACION: v.FEC_CREACION,
                     USU_CREACION: v.USU_CREACION,
                     FEC_MODIFICACION: v.FEC_MODIFICACION,
                     USU_MODIFICACION: v.USU_MODIFICACION,
                 };
                jQuery("#" + Perfil_Grilla).jqGrid('addRowData', i, myData);
            });
            jQuery("#" + Perfil_Grilla).trigger("reloadGrid");
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}



///*********************************************** ----------------- *************************************************/

///*********************************************** Actualiza  perfils  ************************************************/

function Perfil_Actualizar() {
    if ($("#frmMantenimiento_Perfil").valid()) {
        var item =
                {
                    ID_PERFIL: $("#hfd_ID_PERFIL").val(),
                    DESC_PERFIL: $("#DESC_PERFIL").val(),
                    USU_MODIFICACION: $('#input_hdcodusuario').val(),
                    Accion: $("#AccionPerfil").val()
                };
        jConfirm("¿ Desea actualizar este perfil ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Administracion/Perfil/Perfil_Actualizar';
                var auditoria = SICA.Ajax(url, item, false);
                if (auditoria != null && auditoria != "") {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            Perfil_CargarGrilla();
                            Perfil_CancelarEditar();
                            jOkas("Perfil actualizado satisfactoriamente", "Proceso");
                        } else {
                            jError(auditoria.MENSAJE_SALIDA, "Atención");
                        }
                    } else {
                        jError(auditoria.MENSAJE_SALIDA, "Atención");
                    }
                }
            }
        });
    }
}

///*********************************************** ----------------- *************************************************/

///************************************************ Inserta perfils  **************************************************/

function Perfil_Ingresar() {
    if ($('#AccionPerfil').val() != 'N') {
        Perfil_Actualizar();
    } else {
        if ($("#frmMantenimiento_Perfil").valid()) {
            jConfirm("¿ Desea registrar este perfil ?", "Atención", function (r) {
                if (r) {
                    var item =
                        {
                            ID_ENTIDAD: $("#input_hdid_entidad").val() != 1 ? $("#input_hdid_entidad").val() : $("#ID_ENTIDAD").val(),
                            DESC_PERFIL: $("#DESC_PERFIL").val(),
                            USU_CREACION: $('#input_hdcodusuario').val(),
                            ACCION: $("#AccionPerfil").val()
                        };
                    var url = baseUrl + 'Administracion/Perfil/Perfil_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Perfil_CargarGrilla();
                                jOkas("Perfil registrado satisfactoriamente", "Proceso");
                            } else {
                                jError(auditoria.MENSAJE_SALIDA, "Atención");
                            }
                        } else {
                            jError(auditoria.MENSAJE_SALIDA, "Atención");
                        }
                    }
                }
            });
        }
    }
}

///*********************************************** ----------------- *************************************************/

///*********************************************** Elimina perfils  ***************************************************/

function Perfil_Eliminar(ID_PERFIL) {
    jConfirm("¿ Desea eliminar este perfil ?", "Atención", function (r) {
        if (r) {
            var item = {
                ID_PERFIL: ID_PERFIL
            };
            var url = baseUrl + 'Administracion/Perfil/Perfil_Eliminar';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        Perfil_CargarGrilla();
                        Perfil_Cerrar();
                        jOkas("Perfil eliminado satisfactoriamente", "Proceso");
                    } else {
                        jError(auditoria.MENSAJE_SALIDA, "Atención");
                    }
                } else {
                    jError(auditoria.MENSAJE_SALIDA, "Atención");
                }
            }
        }
    });
}

///*********************************************** ----------------- *************************************************/

///*********************************************** Cambia estado de perfils  ******************************************/

function Perfil_Estado(ID_PERFIL, CHECK) {
    var item = {
        ID_PERFIL: ID_PERFIL,
        FLG_ESTADO: CHECK.checked == true ? '1' : '0',
        USU_MODIFICACION: $('#input_hdcodusuario').val(),
    };
    var url = baseUrl + 'Administracion/Perfil/Perfil_Estado';
    var auditoria = SICA.Ajax(url, item, false);
    if (auditoria != null && auditoria != "") {
        if (auditoria.EJECUCION_PROCEDIMIENTO) {
            if (auditoria.RECHAZAR) {
                jError(auditoria.MENSAJE_SALIDA, "Atención");
            }
        } else {
            jError(auditoria.MENSAJE_SALIDA, "Atención");
        }
    }
}

///*********************************************** ----------------- *************************************************/