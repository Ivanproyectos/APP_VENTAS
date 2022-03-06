var Usuarios_Grilla = 'Usuarios_Grilla';
var Usuarios_Barra = 'Usuarios_Barra';

//$(document).ready(function () {
//    Usuarios_ConfigurarGrilla();
//});

function Usuarios_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Usuarios_Limpiar() {
    $("#txtdesUsuarios").val('');
    $('#cboEstado').val('');

    Usuarios_CargarGrilla();
}

function Usuarios_ConfigurarGrilla() {
    $("#" + Usuarios_Grilla).GridUnload();
    var colNames = ['Editar', 'Eliminar', 'Estado', 'codigo', 'ID','Es Jefe', 'Nombre y Apellidos', 'Dni', 'Tipo Documento', 'Celular','Telefono','Correo',
        'flg_estado', 'Fecha Creación', 'Usuario Creación', 'Fecha Modificación', 'Usuario Modificación'];
    var colModels = [
            { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 60, hidden: false, formatter: Usuarios_actionEditar, sortable: false },
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 80, hidden: false, formatter: Usuarios_actionEliminar, sortable: false },
            { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 70, hidden: false, sortable: true, formatter: Usuarios_actionActivo, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_USUARIO', index: 'ID_USUARIO', width: 100, hidden: true, key: true },
            { name: 'FLG_ADMIN', index: 'FLG_ADMIN', width: 100, hidden: false, align: "left" },
            { name: 'NOMBRES_APE', index: 'NOMBRES_APE', width: 300, hidden: false, align: "left" },
            { name: 'DNI', index: 'DNI', width: 100, hidden: false, align: "left" },
            { name: 'TIPO_DOCUMENTO', index: 'TIPO_DOCUMENTO', width: 150, hidden: false, align: "left" },
            { name: 'CELULAR', index: 'CELULAR', width: 100, hidden: false, align: "left" },
            { name: 'TELEFONO', index: 'TELEFONO', width: 100, hidden: false, align: "left" },
            { name: 'CORREO', index: 'CORREO', width: 200, hidden: false, align: "left" },
            { name: 'FLG_ESTADO', index: 'FLG_ESTADO', width: 300, hidden: true, align: "left" },
            { name: 'FEC_CREACION', index: 'FEC_CREACION',  width: 150, hidden: false, align: "left" },
            { name: 'USU_CREACION', index: 'USU_CREACION',  width: 150, hidden: false, align: "left" },
            { name: 'FEC_MODIFICACION', index: 'FEC_MODIFICACION',  width: 150, hidden: false, align: "left" },
            { name: 'USU_MODIFICACION', index: 'USU_MODIFICACION', width: 150, hidden: false, align: "left" },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false ,rowNumber: 50, rowNumbers: [50, 100, 200, 300, 500],
    };
    SICA.Grilla(Usuarios_Grilla, Usuarios_Barra, '', 400, '', "Lista de Usuarios", '', 'ID_USUARIO', colNames, colModels, '', opciones);
}

function Usuarios_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject.FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = " <label class=\"content_toggle_1\">"
            + "<input id=\"Usuarios_chk_" + rowObject.ID_USUARIO + "\" class=\"toggle_Beatiful_1\" type=\"checkbox\" onchange=\"Usuarios_Estado(" + rowObject.ID_USUARIO + ",this)\" " + check_ + ">"
            + "<div class=\"content_toggle_2\">"
            + "  <span class=\"Label_toggle_1\" ></span>"
             + "</div>"
            + "</label>";
    return _btn;
}

function Usuarios_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar'  onclick='Usuarios_MostrarEditar(" + rowObject.ID_USUARIO + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\" data-target='#myModalNuevo'> <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:17px\"></i></button>";
    return _btn;
}

function Usuarios_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='Usuarios_Eliminar(" + rowObject.ID_USUARIO + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
}


function Usuarios_MostrarNuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Usuarios/Mantenimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function Usuarios_MostrarEditar(ID_USUARIO) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Usuarios/Mantenimiento?id=" + ID_USUARIO + "&Accion=M", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  cargo **************************************************/

function Usuarios_CargarGrilla() {
    var item =
       {
           ID_ENTIDAD: $("#input_hdid_entidad").val() != 1 ? $("#input_hdid_entidad").val() : 0,
           //$("#input_hdid_entidad").val(),
           DESC_CARGO: $('#txtdesUsuarios').val(),
           FLG_ESTADO: $('#cboEstado').val()
       };
    var url = baseUrl + 'Administracion/Usuarios/Usuarios_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    jQuery("#" + Usuarios_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;
                var myData =
                 {
                     CODIGO: idgrilla,
                     ID_USUARIO: v.ID_USUARIO,
                     DESC_CARGO: v.DESC_CARGO,
                     DESC_ENTIDAD: v.DESC_ENTIDAD,
                     FLG_ESTADO: v.FLG_ESTADO,
                     FEC_CREACION: v.FEC_CREACION,
                     USU_CREACION: v.USU_CREACION,
                     FEC_MODIFICACION: v.FEC_MODIFICACION,
                     USU_MODIFICACION: v.USU_MODIFICACION,
                     IP_CREACION: v.IP_CREACION,
                     IP_MODIFICACION: v.IP_MODIFICACION
                 };
                jQuery("#" + Usuarios_Grilla).jqGrid('addRowData', i, myData);
            });
            jQuery("#" + Usuarios_Grilla).trigger("reloadGrid");
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}



///*********************************************** ----------------- *************************************************/

///*********************************************** Actualiza  cargos  ************************************************/

function Usuarios_Actualizar() {
    if ($("#frmMantenimientoUsuarios").valid()) {
        var item =
                {
                    ID_USUARIO: $("#hdfID_USUARIO").val(),
                    ID_ENTIDAD: $("#input_hdid_entidad").val() != 1 ? $("#input_hdid_entidad").val() : $("#ID_ENTIDAD").val(),
                    //ID_ENTIDAD: $("#ID_ENTIDAD").val(),
                    DESC_CARGO: $("#DESC_CARGO").val(),
                    USU_MODIFICACION: $('#input_hdcodusuario').val(),
                    Accion: $("#AccionUsuarios").val()
                };
        jConfirm("¿ Desea actualizar este cargo ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Administracion/Usuarios/Usuarios_Actualizar';
                var auditoria = SICA.Ajax(url, item, false);
                if (auditoria != null && auditoria != "") {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            Usuarios_CargarGrilla();
                            Usuarios_Cerrar();
                            jOkas("Usuarios actualizado satisfactoriamente", "Proceso");
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

///************************************************ Inserta cargos  **************************************************/

function Usuarios_Ingresar() {
    debugger; 
    if ($('#AccionUsuarios').val() != 'N') {
        Usuarios_Actualizar();
    } else {
        if ($("#frmMantenimiento_Usuarios").valid()) {
            jConfirm("¿ Desea registrar este cargo ?", "Atención", function (r) {
                if (r) {
                    var item =
                        {
                            ID_ENTIDAD: $("#input_hdid_entidad").val() != 1 ? $("#input_hdid_entidad").val() : $("#ID_ENTIDAD").val(),
                            DESC_CARGO: $("#DESC_CARGO").val(),
                            USU_CREACION: $('#input_hdcodusuario').val(),
                            ACCION: $("#AccionUsuarios").val()
                        };
                    var url = baseUrl + 'Administracion/Usuarios/Usuarios_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Usuarios_CargarGrilla();
                                Usuarios_Cerrar();
                                jOkas("Usuarios registrado satisfactoriamente", "Proceso");
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

///*********************************************** Elimina cargos  ***************************************************/

function Usuarios_Eliminar(ID_USUARIO) {
    jConfirm("¿ Desea eliminar este cargo ?", "Atención", function (r) {
        if (r) {
            var item = {
                ID_USUARIO: ID_USUARIO
            };
            var url = baseUrl + 'Administracion/Usuarios/Usuarios_Eliminar';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        Usuarios_CargarGrilla();
                        Usuarios_Cerrar();
                        jOkas("Usuarios eliminado satisfactoriamente", "Proceso");
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

function Usuarios_Estado(ID_USUARIO, CHECK) {
    var item = {
        ID_USUARIO: ID_USUARIO,
        FLG_ESTADO: CHECK.checked == true ? '1' : '0',
        USU_MODIFICACION: $('#input_hdcodusuario').val(),
    };
    var url = baseUrl + 'Administracion/Usuarios/Usuarios_Estado';
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