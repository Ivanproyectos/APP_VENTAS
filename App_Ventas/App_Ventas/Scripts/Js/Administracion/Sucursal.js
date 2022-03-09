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
    $("#" + Sucursal_Grilla).GridUnload();
    var colNames = ['Editar', 'Eliminar', 'Estado', 'codigo', 'ID', 'Sucursal', 'Dirección', 'Telefono', 'Correo', 'Urbanización', 'Ubigeo',
        'flg_estado', 'Fecha Creación', 'Usuario Creación', 'Fecha Modificación', 'Usuario Modificación'];
    var colModels = [
            { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 60, hidden: false, formatter: Sucursal_actionEditar, sortable: false },
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 80, hidden: false, formatter: Sucursal_actionEliminar, sortable: false },
            { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 70, hidden: false, sortable: true, formatter: Sucursal_actionActivo, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_SUCURSAL', index: 'ID_SUCURSAL', width: 100, hidden: true, key: true },
            { name: 'DESC_SUCURSAL', index: 'DESC_SUCURSAL', width: 250, hidden: false, align: "left" },
            { name: 'DIRECCION', index: 'DIRECCION', width: 200, hidden: false, align: "left" },
            { name: 'TELEFONO', index: 'TELEFONO', width: 150, hidden: false, align: "left" },
            { name: 'CORREO', index: 'CORREO', width: 150, hidden: false, align: "left",rezible:true },
            { name: 'URBANIZACION', index: 'URBANIZACION', width: 200, hidden: false, align: "left" },
            { name: 'DESC_UBIGEO', index: 'DESC_UBIGEO', width: 250, hidden: false, align: "left" },
            { name: 'FLG_ESTADO', index: 'FLG_ESTADO', width: 300, hidden: true, align: "left" },
            { name: 'FEC_CREACION', index: 'FEC_CREACION', width: 150, hidden: false, align: "left" },
            { name: 'USU_CREACION', index: 'USU_CREACION', width: 150, hidden: false, align: "left" },
            { name: 'FEC_MODIFICACION', index: 'FEC_MODIFICACION', width: 150, hidden: false, align: "left" },
            { name: 'USU_MODIFICACION', index: 'USU_MODIFICACION', width: 150, hidden: false, align: "left" },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, rowNumber: 50, rowNumbers: [50, 100, 200, 300, 500],
    };
    SICA.Grilla(Sucursal_Grilla, Sucursal_Barra, Sucursal_Grilla, 400, '', "Lista de Sucursal", '', 'ID_SUCURSAL', colNames, colModels, '', opciones);
    jqGridResponsive($(".jqGrid"));
}

function Sucursal_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject.FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = " <label class=\"content_toggle_1\">"
            + "<input id=\"Sucursal_chk_" + rowObject.ID_SUCURSAL + "\" class=\"toggle_Beatiful_1\" type=\"checkbox\" onchange=\"Sucursal_Estado(" + rowObject.ID_SUCURSAL + ",this)\" " + check_ + ">"
            + "<div class=\"content_toggle_2\">"
            + "  <span class=\"Label_toggle_1\" ></span>"
             + "</div>"
            + "</label>";
    return _btn;
}

function Sucursal_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar'  onclick='Sucursal_MostrarEditar(" + rowObject.ID_SUCURSAL + ");' class=\"btn btn-outline-light\" type=\"button\" > <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:17px\"></i></button>";
    return _btn;
}

function Sucursal_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='Sucursal_Eliminar(" + rowObject.ID_SUCURSAL + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
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
           DESC_SUCURSAL: $('#Sucursal_Desc_Sucursal').val(),
           FLG_ESTADO: $('#Sucursal_Estado').val()
       };
    var url = baseUrl + 'Administracion/Sucursal/Sucursal_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    jQuery("#" + Sucursal_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;
                var myData =
                 {
                     CODIGO: idgrilla,
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
                jQuery("#" + Sucursal_Grilla).jqGrid('addRowData', i, myData);
            });
            jQuery("#" + Sucursal_Grilla).trigger("reloadGrid");
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