var Perfil_Grilla = 'Perfil_Grilla';
var Perfil_Barra = 'Perfil_Barra';

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
    DataTable.GridUnload(Perfil_Grilla);
    var colModels = [
          { data: "ID_PERFIL", name: "ID_PERFIL", title: "Código", autoWidth: false, visible: true, },
          { data: "DESC_PERFIL", name: "DESC_PERFIL", title: "Perfil", autoWidth: true },
          { data: "FEC_CREACION", name: "FEC_CREACION", title: "Fecha Creación", autoWidth: true, },
          { data: "USU_CREACION", name: "USU_CREACION", title: "Usuacion Creación", autoWidth: true, },
          {
              data: null, name: "FLG_ESTADO", title: "Activo", width: "80px", sortable: false,
              render: function (data, type, row, meta) { return Perfil_actionActivo(data.FLG_ESTADO, data.ID_PERFIL); }
          },
          {
              data: null, sortable: false, title: "Acciones", width: "80px",
              render: function (data, type, row, meta) { return Perfil_actionAcciones(data.ID_PERFIL); }
          },

    ];
    var opciones = {
        GridLocal: true, multiselect: false, sort: "desc", enumerable: false,
        eliminar: false, search: true, rowNumber: 10, rowNumbers: [10, 25, 50], rules: false, responsive: true, processing: true
    };
    DataTable.Grilla(Perfil_Grilla, '', 'ID_PERFIL', colModels, opciones, "ID_PERFIL");
}

function Perfil_actionAcciones(ID_PERFIL) {
    var _btn_Editar = "<a class=\"dropdown-item\" onclick='Perfil_MostrarEditar(" + ID_PERFIL + ")'><i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;\"></i>&nbsp;  Editar</a>";
    var _btn_Eliminar = "<a class=\"dropdown-item\" onclick='Perfil_Eliminar(" + ID_PERFIL + ")'><i class=\"bi bi-trash-fill\" style=\"color:#e40613;\"></i>&nbsp;  Eliminar</a>";
    var _btn = "<div class=\"btn-group Group_Acciones dropleft\" role=\"group\" title=\"Acciones \" >" +
             "<button  style=\" background: transparent; border: none; color: #000000;font-size: 18px;\" type=\"button\" class=\"btn btn-link dropdown-toggle text-dark nobefore\" data-toggle=\"dropdown\" aria-expanded=\"false\"><i class=\"bi bi-three-dots-vertical\"></i></button>" +
           "<div class=\"dropdown-menu\" x-placement=\"left-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(-162px, 0px, 0px);\">" +
           _btn_Editar +
           _btn_Eliminar +
            "</div>" +
        "</div>";
    return _btn;
}

function Perfil_actionActivo(FLG_ESTADO, ID_PERFIL) {
    var check_ = 'check';
    if (FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = "<input type=\"checkbox\" id=\"Perfil_chk_" + ID_PERFIL + "\"  data-switch=\"state\" onchange=\"Perfil_Estado(" + ID_PERFIL + ",this)\" " + check_ + ">"
              + " <label for=\"Perfil_chk_" + ID_PERFIL + "\" data-on-label=\"Yes\" data-off-label=\"No\"></label>";
    return _btn;
}

function Perfil_MostrarNuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Perfil/View_Mantenimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function Perfil_MostrarEditar(ID_PERFIL) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Perfil/View_Mantenimiento?id=" + ID_PERFIL + "&Accion=M", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
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
    DataTable.clearGridData(Perfil_Grilla);
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
                DataTable.addRowData(Perfil_Grilla, myData);
            });
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
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
                            jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
                        }
                    } else {
                        jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
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
                                jOkas("Perfil registrado satisfactoriamente, a continuación configuré acceso a los modulos para este perfil.", "Proceso");
                                $('#hfd_ID_PERFIL').val(auditoria.OBJETO);
                                $('#PerfilTab, #PerfilPanel').removeClass('active');
                                $('#PerfilPanel').removeClass('show ');

                                Perfiles_CargarModulos();
                                $('#PerfilModulosTab').removeClass('DisabledContent');
                                $('#PerfilModulosTab, #PerfilModulosPanel').addClass('active');
                                $('#PerfilModulosPanel').addClass('show');
                                $('#PerfilTab').addClass('DisabledContent');
                                $('#Perfil_Btn_Guardar').hide();

                            } else {
                                jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
                            }
                        } else {
                            jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
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
                        jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
                    }
                } else {
                    jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
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
                jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
            }
        } else {
            jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
        }
    }
}

///*********************************************** ----------------- *************************************************/



function test_culqi() {
    var item = {
    };
    var url = baseUrl + 'Recursiva/ServiciosWeb/CreateCharge';
    var auditoria = SICA.Ajax(url, item, false);
    if (auditoria != null && auditoria != "") {
        if (auditoria.EJECUCION_PROCEDIMIENTO) {
            if (auditoria.RECHAZAR) {
                jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
            }
        } else {
            jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
        }
    }
}