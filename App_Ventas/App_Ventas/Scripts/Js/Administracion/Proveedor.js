var Proveedor_Grilla = 'Proveedor_Grilla';
var Proveedor_Barra = 'Proveedor_Barra';

function Proveedor_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Proveedor_Limpiar() {
    $("#Proveedor_NombreYape").val('');
    $('#Proveedor_NumeroDocumento').val('');
    $('#Proveedor_Estado').val(2);

    Proveedor_CargarGrilla();
}

function Proveedor_ConfigurarGrilla() {
    $("#" + Proveedor_Grilla).GridUnload();
    var colNames = ['Editar', 'Eliminar', 'Estado', 'codigo', 'ID', 'Nombres y apellidos', 'Tipo documento', 'Número Documento', 'Dirección', 'Telefono', 'Celular', 'Correo',
         'Ubigeo', 'Detalle', 'flg_estado', 'Fecha Creación', 'Usuario Creación', 'Fecha Modificación', 'Usuario Modificación'];
    var colModels = [
            { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 60, hidden: false, formatter: Proveedor_actionEditar, sortable: false },
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 80, hidden: false, formatter: Proveedor_actionEliminar, sortable: false },
            { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 70, hidden: false, sortable: true, formatter: Proveedor_actionActivo, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_PROVEEDOR', index: 'ID_PROVEEDOR', width: 100, hidden: true, key: true },
            { name: 'NOMBRES_APE', index: 'NOMBRES_APE', width: 300, hidden: false, align: "left" },
            { name: 'DESC_TIPO_DOCUMENTO', index: 'DESC_TIPO_DOCUMENTO', width: 200, hidden: false, align: "left" },
            { name: 'NUMERO_DOCUMENTO', index: 'NUMERO_DOCUMENTO', width: 150, hidden: false, align: "left" },
            { name: 'DIRECCION', index: 'DIRECCION', width: 200, hidden: false, align: "left" },
            { name: 'TELEFONO', index: 'TELEFONO', width: 100, hidden: false, align: "left" },
            { name: 'CELULAR', index: 'CELULAR', width: 100, hidden: false, align: "left" },
            { name: 'CORREO', index: 'CORREO', width: 100, hidden: false, align: "left" },
            { name: 'DESC_UBIGEO', index: 'DESC_UBIGEO', width: 200, hidden: false, align: "left" },
            { name: 'DETALLE', index: 'DETALLE', width: 250, hidden: false, align: "left" },
            { name: 'FLG_ESTADO', index: 'FLG_ESTADO', width: 300, hidden: true, align: "left" },
            { name: 'FEC_CREACION', index: 'FEC_CREACION', width: 150, hidden: false, align: "left" },
            { name: 'USU_CREACION', index: 'USU_CREACION', width: 150, hidden: false, align: "left" },
            { name: 'FEC_MODIFICACION', index: 'FEC_MODIFICACION', width: 150, hidden: false, align: "left" },
            { name: 'USU_MODIFICACION', index: 'USU_MODIFICACION', width: 150, hidden: false, align: "left" },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, rowNumber: 50, rowNumbers: [50, 100, 200, 300, 500],
    };
    SICA.Grilla(Proveedor_Grilla, Proveedor_Barra, Proveedor_Grilla, 400, '', "Lista de Proveedor", '', 'ID_PROVEEDOR', colNames, colModels, '', opciones);
    jqGridResponsive($(".jqGrid"));
}

function Proveedor_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject.FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = " <label class=\"content_toggle_1\">"
            + "<input id=\"Proveedor_chk_" + rowObject.ID_PROVEEDOR + "\" class=\"toggle_Beatiful_1\" type=\"checkbox\" onchange=\"Proveedor_Estado(" + rowObject.ID_PROVEEDOR + ",this)\" " + check_ + ">"
            + "<div class=\"content_toggle_2\">"
            + "  <span class=\"Label_toggle_1\" ></span>"
             + "</div>"
            + "</label>";
    return _btn;
}

function Proveedor_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar'  onclick='Proveedor_MostrarEditar(" + rowObject.ID_PROVEEDOR + ");' class=\"btn btn-outline-light\" type=\"button\" > <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:17px\"></i></button>";
    return _btn;
}

function Proveedor_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='Proveedor_Eliminar(" + rowObject.ID_PROVEEDOR + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
}


function Proveedor_MostrarNuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Proveedor/Mantenimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function Proveedor_MostrarEditar(ID_PROVEEDOR) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Proveedor/Mantenimiento?id=" + ID_PROVEEDOR + "&Accion=M", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  Proveedor **************************************************/

function Proveedor_CargarGrilla() {
    var item =
       {
           NOMBRES_APE: $('#Proveedor_NombreYape').val(),
           NUMERO_DOCUMENTO: $('#Proveedor_NumeroDocumento').val(),
           FLG_ESTADO: $('#Proveedor_Estado').val()
       };
    var url = baseUrl + 'Administracion/Proveedor/Proveedor_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    jQuery("#" + Proveedor_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;
                var myData =
                 {
                     CODIGO: idgrilla,
                     ID_PROVEEDOR: v.ID_PROVEEDOR,
                     NOMBRES_APE: v.NOMBRES_APE,
                     NUMERO_DOCUMENTO: v.NUMERO_DOCUMENTO,
                     DIRECCION: v.DIRECCION,
                     CORREO: v.CORREO,
                     DESC_TIPO_DOCUMENTO: v.DESC_TIPO_DOCUMENTO,
                     DIRECCION: v.DIRECCION,
                     CORREO: v.CORREO,
                     TELEFONO: v.TELEFONO,
                     CELULAR: v.CELULAR,
                     DESC_UBIGEO: v.DESC_UBIGEO,
                     DETALLE: v.DETALLE,

                     FLG_ESTADO: v.FLG_ESTADO,
                     FEC_CREACION: v.FEC_CREACION,
                     USU_CREACION: v.USU_CREACION,
                     FEC_MODIFICACION: v.FEC_MODIFICACION,
                     USU_MODIFICACION: v.USU_MODIFICACION

                 };
                jQuery("#" + Proveedor_Grilla).jqGrid('addRowData', i, myData);
            });
            jQuery("#" + Proveedor_Grilla).trigger("reloadGrid");
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}



///*********************************************** ----------------- *************************************************/

///*********************************************** Actualiza  Proveedors  ************************************************/

function Proveedor_Actualizar() {
    if ($("#frmMantenimiento_Proveedor").valid()) {
        var item =
                {
                    ID_PROVEEDOR: $("#hfd_ID_PROVEEDOR").val(),
                    ID_TIPO_DOCUMENTO: $("#ID_TIPO_DOCUMENTO").val(),
                    NUMERO_DOCUMENTO: $("#NUMERO_DOCUMENTO").val(),
                    NOMBRES_APE: $("#NOMBRES_APE").val(),
                    TELEFONO: $("#TELEFONO").val(),
                    CORREO: $("#CORREO").val(),
                    DIRECCION: $("#DIRECCION").val(),
                    CELULAR: $("#CELULAR").val(),
                    COD_UBIGEO: $("#COD_UBIGEO").val(),
                    DETALLE: $("#DETALLE").val(),
                    USU_MODIFICACION: $('#input_hdcodusuario').val(),
                    Accion: $("#AccionProveedor").val()
                };
        jConfirm("¿ Desea actualizar este Proveedor ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Administracion/Proveedor/Proveedor_Actualizar';
                var auditoria = SICA.Ajax(url, item, false);
                if (auditoria != null && auditoria != "") {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            Proveedor_CargarGrilla();
                            Proveedor_Cerrar();
                            jOkas("Proveedor actualizado satisfactoriamente", "Proceso");
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

///************************************************ Inserta Proveedors  **************************************************/

function Proveedor_Ingresar() {
    debugger; 
    if ($('#AccionProveedor').val() != 'N') {
        Proveedor_Actualizar();
    } else {
        if ($("#frmMantenimiento_Proveedor").valid()) {
            jConfirm("¿ Desea registrar este Proveedor ?", "Atención", function (r) {
                if (r) {
                    var item =
                        {
                            ID_TIPO_DOCUMENTO: $("#ID_TIPO_DOCUMENTO").val(),
                            NUMERO_DOCUMENTO: $("#NUMERO_DOCUMENTO").val(),
                            NOMBRES_APE: $("#NOMBRES_APE").val(),
                            TELEFONO: $("#TELEFONO").val(),
                            CORREO: $("#CORREO").val(),
                            DIRECCION: $("#DIRECCION").val(),
                            CELULAR: $("#CELULAR").val(),
                            COD_UBIGEO: $("#COD_UBIGEO").val(),
                            DETALLE: $("#DETALLE").val(),
                            USU_CREACION: $('#input_hdcodusuario').val(),
                            ACCION: $("#AccionProveedor").val()
                        };
                    var url = baseUrl + 'Administracion/Proveedor/Proveedor_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Proveedor_CargarGrilla();
                                Proveedor_Cerrar();
                                jOkas("Proveedor registrado satisfactoriamente", "Proceso");
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

///*********************************************** Elimina Proveedors  ***************************************************/

function Proveedor_Eliminar(ID_PROVEEDOR) {
    jConfirm("¿ Desea eliminar este Proveedor ?", "Atención", function (r) {
        if (r) {
            var item = {
                ID_PROVEEDOR: ID_PROVEEDOR
            };
            var url = baseUrl + 'Administracion/Proveedor/Proveedor_Eliminar';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        Proveedor_CargarGrilla();
                        Proveedor_Cerrar();
                        jOkas("Proveedor eliminado satisfactoriamente", "Proceso");
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

///*********************************************** Cambia estado de Proveedors  ******************************************/

function Proveedor_Estado(ID_PROVEEDOR, CHECK) {
    var item = {
        ID_PROVEEDOR: ID_PROVEEDOR,
        FLG_ESTADO: CHECK.checked == true ? '1' : '0',
        USU_MODIFICACION: $('#input_hdcodusuario').val(),
    };
    var url = baseUrl + 'Administracion/Proveedor/Proveedor_Estado';
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