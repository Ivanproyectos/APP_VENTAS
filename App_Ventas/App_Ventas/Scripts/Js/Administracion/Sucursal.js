var Sucursal_Grilla = 'Sucursal_Grilla';
var Sucursal_Barra = 'Sucursal_Barra';

function Sucursal_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Sucursal_Limpiar() {
    $("#Sucursal_Desc_Sucursal").val('');
    $('#Sucursal_Estado').val(2);
    Sucursal_CargarGrilla();
}

function Sucursal_ConfigurarGrilla() {
    DataTable.GridUnload(Sucursal_Grilla);
    var colModels = [
          { data: "ID_SUCURSAL", name: "ID_SUCURSAL", title: "ID_SUCURSAL", autoWidth: false, visible: false, },
          { data: "DESC_SUCURSAL", name: "DESC_SUCURSAL", title: "Sucursal",  autoWidth: true },
          { data: "DIRECCION", name: "DIRECCION", title: "Dirección", autoWidth: false, },
          { data: "TELEFONO", name: "TELEFONO", title: "Telefono", autoWidth: false, visible: false, },
          { data: "CORREO", name: "CORREO", title: "Correo",  autoWidth: true, },
          { data: "URBANIZACION", name: "URBANIZACION", title: "Urbanización", autoWidth: true },
          { data: "DESC_UBIGEO", name: "DESC_UBIGEO", title: "Ubigeo", autoWidth: true },
          {
              data: null, name: "FLG_ESTADO", title: "Activo", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Sucursal_actionActivo(data.FLG_ESTADO, data.ID_SUCURSAL); }
          },
          {
              data: null, sortable: false, title: "Acciones", width: "60px",
              render: function (data, type, row, meta) { return Sucursal_actionAcciones(data.ID_SUCURSAL); }
          },


    ];
    var opciones = {
        GridLocal: true, multiselect: false, sort: "desc", enumerable: false,
        eliminar: false, search: true, rowNumber: 10, rowNumbers: [10, 25, 50], rules: false, responsive: true, processing: true
    };
    DataTable.Grilla(Sucursal_Grilla, '', 'ID_SUCURSAL', colModels, opciones, "ID_SUCURSAL");
}


function Sucursal_actionAcciones(ID_SUCURSAL) {
    var _btn_Editar = "<a class=\"dropdown-item\" onclick='Sucursal_MostrarEditar(" + ID_SUCURSAL + ")'><i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;\"></i>&nbsp;  Editar</a>";
    var _btn_Eliminar = "<a class=\"dropdown-item\" onclick='Sucursal_Eliminar(" + ID_SUCURSAL + ")'><i class=\"bi bi-trash-fill\" style=\"color:#e40613;\"></i>&nbsp;  Eliminar</a>";
    var _btn = "<div class=\"btn-group Group_Acciones\" role=\"group\" title=\"Acciones \" >" +
           "<button  style=\" background: transparent; border: none; color: #000000;font-size: 18px;\" type=\"button\" class=\"btn  dropdown-toggle\" data-toggle=\"dropdown\" aria-expanded=\"false\"><i class=\"bi bi-list\"></i></button>" +
           "<div class=\"dropdown-menu\" x-placement=\"bottom-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 35px, 0px);\">" +
           _btn_Editar +
           _btn_Eliminar +
            "</div>" +
        "</div>";
    return _btn;
}


function Sucursal_actionActivo(FLG_ESTADO, ID_SUCURSAL) {
    var check_ = 'check';
    if (FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = "<input type=\"checkbox\" id=\"Usuarios_chk_" + ID_SUCURSAL + "\"  data-switch=\"state\" onchange=\"Sucursal_Estado(" + ID_SUCURSAL + ",this)\" " + check_ + ">"
              + " <label for=\"Usuarios_chk_" + ID_SUCURSAL + "\" data-on-label=\"Yes\" data-off-label=\"No\"></label>";
    return _btn;
}

function Sucursal_MostrarNuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Sucursal/Mantenimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function Sucursal_MostrarEditar(ID_SUCURSAL) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Sucursal/Mantenimiento?id=" + ID_SUCURSAL + "&Accion=M", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  sucursal **************************************************/

function Sucursal_CargarGrilla() {
    var item =
       {
           //DESC_SUCURSAL: $('#Sucursal_Desc_Sucursal').val(),
           FLG_ESTADO:2
       };
    var url = baseUrl + 'Administracion/Sucursal/Sucursal_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    DataTable.clearGridData(Sucursal_Grilla);
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;
                var myData =
                 {
                     FILA: idgrilla,
                     ID_SUCURSAL: v.ID_SUCURSAL,
                     DESC_SUCURSAL: v.DESC_SUCURSAL,
                     DIRECCION: v.DIRECCION,
                     TELEFONO: v.TELEFONO,
                     CORREO: v.CORREO,
                     URBANIZACION: v.URBANIZACION,
                     DESC_UBIGEO: v.DESC_UBIGEO,
                     FLG_ESTADO: v.FLG_ESTADO,
                     FEC_CREACION: v.FEC_CREACION,
                     USU_CREACION: v.USU_CREACION,
                     FEC_MODIFICACION: v.FEC_MODIFICACION,
                     USU_MODIFICACION: v.USU_MODIFICACION
                 };
                DataTable.addRowData(Sucursal_Grilla, myData);
            });
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}



///*********************************************** ----------------- *************************************************/

///*********************************************** Actualiza  sucursals  ************************************************/

function Sucursal_Actualizar() {
    if ($("#frmMantenimiento_Sucursal").valid()) {
        var item =
                {
                    ID_SUCURSAL: $("#hfd_ID_SUCURSAL").val(),
                    DESC_SUCURSAL: $("#DESC_SUCURSAL").val(),
                    DIRECCION: $("#DIRECCION").val(),
                    TELEFONO: $("#TELEFONO").val(),
                    CORREO: $("#CORREO").val(),
                    URBANIZACION: $("#URBANIZACION").val(),
                    COD_UBIGEO: $("#COD_UBIGEO").val(),
                    USU_MODIFICACION: $('#input_hdcodusuario').val(),
                    Accion: $("#AccionSucursal").val()
                };
        jConfirm("¿ Desea actualizar este sucursal ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Administracion/Sucursal/Sucursal_Actualizar';
                var auditoria = SICA.Ajax(url, item, false);
                if (auditoria != null && auditoria != "") {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            Sucursal_CargarGrilla();
                            Sucursal_Cerrar();
                            jOkas("Sucursal actualizado satisfactoriamente", "Proceso");
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

///************************************************ Inserta sucursals  **************************************************/

function Sucursal_Ingresar() {
    if ($('#AccionSucursal').val() != 'N') {
        Sucursal_Actualizar();
    } else {
        if ($("#frmMantenimiento_Sucursal").valid()) {
            jConfirm("¿ Desea registrar este sucursal ?", "Atención", function (r) {
                if (r) {
                    var item =
                        {
                            DESC_SUCURSAL: $("#DESC_SUCURSAL").val(),
                            DIRECCION: $("#DIRECCION").val(),
                            TELEFONO: $("#TELEFONO").val(),
                            CORREO: $("#CORREO").val(),
                            URBANIZACION: $("#URBANIZACION").val(),
                            COD_UBIGEO: $("#COD_UBIGEO").val(),
                            USU_CREACION: $('#input_hdcodusuario').val(),
                            ACCION: $("#AccionSucursal").val()
                        };
                    var url = baseUrl + 'Administracion/Sucursal/Sucursal_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Sucursal_CargarGrilla();
                                Sucursal_Cerrar();
                                jOkas("Sucursal registrado satisfactoriamente", "Proceso");
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

///*********************************************** Elimina sucursals  ***************************************************/

function Sucursal_Eliminar(ID_SUCURSAL) {
    jConfirm("¿ Desea eliminar este sucursal ?", "Atención", function (r) {
        if (r) {
            var item = {
                ID_SUCURSAL: ID_SUCURSAL
            };
            var url = baseUrl + 'Administracion/Sucursal/Sucursal_Eliminar';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        Sucursal_CargarGrilla();
                        Sucursal_Cerrar();
                        jOkas("Sucursal eliminado satisfactoriamente", "Proceso");
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

///*********************************************** Cambia estado de sucursals  ******************************************/

function Sucursal_Estado(ID_SUCURSAL, CHECK) {
    var item = {
        ID_SUCURSAL: ID_SUCURSAL,
        FLG_ESTADO: CHECK.checked == true ? '1' : '0',
        USU_MODIFICACION: $('#input_hdcodusuario').val(),
    };
    var url = baseUrl + 'Administracion/Sucursal/Sucursal_Estado';
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