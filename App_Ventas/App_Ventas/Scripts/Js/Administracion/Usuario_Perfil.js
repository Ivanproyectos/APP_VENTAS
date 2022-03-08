
var Usuarios_Perfil_Grilla = 'Usuarios_Perfil_Grilla';
var Usuarios_Perfil_Barra = 'Usuarios_Perfil_Barra';

function Usuarios_Perfil_ConfigurarGrilla() {
    $("#" +  Usuarios_Perfil_Grilla).GridUnload();
    var colNames = ['Eliminar', 'Estado', 'codigo', 'ID', 'Sucursal', 'Perfil', 'flg_estado', 'Fecha Activación', 'Fecha Desactivación'];
    var colModels = [
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 80, hidden: false, formatter: Cargo_actionEliminar, sortable: false },
            { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 70, hidden: false, sortable: false, formatter: Cargo_actionActivo, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_USUARIO_PERFIL', index: 'ID_USUARIO_PERFIL', align: 'center', width: 100, hidden: true, key: true },
            { name: 'DESC_SUCURSAL', index: 'DESC_SUCURSAL', align: 'left', width: 200, hidden: false },
            { name: 'DESC_PERFIL', index: 'DESC_PERFIL', align: 'left', width: 200, hidden: false },
            { name: 'FLG_ESTADO', index: 'FLG_ESTADO', align: 'left', width: 300, hidden: true },
            { name: 'FEC_ACTIVACION', index: 'FEC_ACTIVACION', align: 'left', width: 150, hidden: false },
            { name: 'FEC_DESACTIVACION', index: 'FEC_DESACTIVACION', align: 'left', width: 150, hidden: false },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false
    };
    SICA.Grilla( Usuarios_Perfil_Grilla, Usuarios_Perfil_Barra,  Usuarios_Perfil_Grilla, '', '', "", '', 'ID_USUARIO_PERFIL', colNames, colModels, '', opciones);
}


function Cargo_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject.FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = " <label class=\"content_toggle_1\">"
            + "<input id=\"Vehiculos_chk_" + rowObject.ID_USUARIO_PERFIL + "\" class=\"toggle_Beatiful_1\" type=\"checkbox\" onchange=\"UsuariosPerfil_Estado(" + rowObject.ID_USUARIO_PERFIL + ",this)\" " + check_ + ">"
            + "<div class=\"content_toggle_2\">"
            + "  <span class=\"Label_toggle_1\" ></span>"
             + "</div>"
            + "</label>";
    return _btn;
}


function Cargo_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='UsuariosPerfil_Eliminar(" + rowObject.ID_USUARIO_PERFIL + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
}



function UsuariosPerfil_CargarGrilla() {
    var item =
       {
           ID_USUARIO: $("#hfd_ID_USUARIO").val()
       };
    var url = baseUrl + 'Administracion/Usuario_Perfil/Usuario_Perfil_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    jQuery("#" + Usuarios_Perfil_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;

                var myData =
                 {
                     CODIGO: idgrilla,
                     ID_USUARIO_PERFIL: v.ID_USUARIO_PERFIL,
                     DESC_SUCURSAL: v.DESC_SUCURSAL,
                     DESC_PERFIL: v.DESC_PERFIL,
                     FEC_ACTIVACION: v.FEC_ACTIVACION,
                     FEC_DESACTIVACION: v.FEC_DESACTIVACION,
                     FLG_ESTADO: v.FLG_ESTADO

                 };
                jQuery("#" + Usuarios_Perfil_Grilla).jqGrid('addRowData', i, myData);
            });
            jQuery("#" + Usuarios_Perfil_Grilla).trigger("reloadGrid");
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}





///*********************************************** ----------------- *************************************************/

///************************************************ Inserta usuario perfil  **************************************************/

function UsuariosPerfil_Ingresar() {
  
        if ($("#frmMantenimiento_UsuariosPerfil").valid()) {
            jConfirm("¿ Desea registrar este acceso ?", "Atención", function (r) {
                if (r) {
                    var item =
                        {
                            ID_USUARIO: $("#hfd_ID_USUARIO").val(),
                            ID_SUCURSAL: $("#ID_SUCURSAL").val(),
                            ID_PERFIL: $("#ID_PERFIL").val(),
                            FEC_ACTIVACION: $("#FEC_ACTIVACION").val(),
                            FEC_DESACTIVACION: $("#FEC_DESACTIVACION").val(),    
                            USU_CREACION: $('#input_hdcodusuario').val(),

                        };
                    var url = baseUrl + 'Administracion/Usuario_Perfil/Usuario_Perfil_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                UsuariosPerfil_CargarGrilla();
                                UsuariosPerfil_limpiar(); 
                                //Usuarios_Cerrar();
                                jOkas("Acceso registrado satisfactoriamente para este usuario.", "Proceso");
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

///*********************************************** Elimina cargos  ***************************************************/

function UsuariosPerfil_Eliminar(ID_USUARIO_PERFIL) {
    jConfirm("¿ Desea eliminar este acceso ?", "Atención", function (r) {
        if (r) {
            var item = {
                ID_USUARIO_PERFIL: ID_USUARIO_PERFIL
            };
            var url = baseUrl + 'Administracion/Usuario_Perfil/Usuario_Perfil_Eliminar';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        UsuariosPerfil_CargarGrilla(); 
                        jOkas("acceso eliminado satisfactoriamente", "Proceso");
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

///*********************************************** Cambia estado de cargos  ******************************************/

function UsuariosPerfil_Estado(ID_USUARIO_PERFIL, CHECK) {
    var item = {
        ID_USUARIO_PERFIL: ID_USUARIO_PERFIL,
        FLG_ESTADO: CHECK.checked == true ? '1' : '0',
        USU_MODIFICACION: $('#input_hdcodusuario').val(),
    };
    var url = baseUrl + 'Administracion/Usuario_Perfil/Usuario_Perfil_Estado';
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


function UsuariosPerfil_limpiar() {
    $('ID_SUCURSAL').val();
    $('ID_PERFIL').val();
    $('FEC_ACTIVACION').val();
    $('FEC_DESACTIVACION').val();
}