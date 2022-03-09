var Clientes_Grilla = 'Clientes_Grilla';
var Clientes_Barra = 'Clientes_Barra';

function Clientes_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Clientes_Limpiar() {
    $("#Cliente_NombreYape").val('');
    $('#Cliente_NumeroDocumento').val('');
    $('#Clientes_Estado').val(2);

    Clientes_CargarGrilla();
}

function Clientes_ConfigurarGrilla() {
    $("#" + Clientes_Grilla).GridUnload();
    var colNames = ['Editar', 'Eliminar', 'Estado', 'codigo', 'ID', 'Nombres y apellidos', 'Tipo documento', 'Número Documento', 'Dirección', 'Telefono','Celular', 'Correo',
         'Ubigeo','Detalle', 'flg_estado', 'Fecha Creación', 'Usuario Creación', 'Fecha Modificación', 'Usuario Modificación'];
    var colModels = [
            { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 60, hidden: false, formatter: Clientes_actionEditar, sortable: false },
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 80, hidden: false, formatter: Clientes_actionEliminar, sortable: false },
            { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 70, hidden: false, sortable: true, formatter: Clientes_actionActivo, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_CLIENTE', index: 'ID_CLIENTE', width: 100, hidden: true, key: true },
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
    SICA.Grilla(Clientes_Grilla, Clientes_Barra, Clientes_Grilla, 400, '', "Lista de Clientes", '', 'ID_CLIENTE', colNames, colModels, '', opciones);
    jqGridResponsive($(".jqGrid"));
}

function Clientes_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject.FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = " <label class=\"content_toggle_1\">"
            + "<input id=\"Clientes_chk_" + rowObject.ID_CLIENTE + "\" class=\"toggle_Beatiful_1\" type=\"checkbox\" onchange=\"Clientes_Estado(" + rowObject.ID_CLIENTE + ",this)\" " + check_ + ">"
            + "<div class=\"content_toggle_2\">"
            + "  <span class=\"Label_toggle_1\" ></span>"
             + "</div>"
            + "</label>";
    return _btn;
}

function Clientes_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar'  onclick='Clientes_MostrarEditar(" + rowObject.ID_CLIENTE + ");' class=\"btn btn-outline-light\" type=\"button\" > <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:17px\"></i></button>";
    return _btn;
}

function Clientes_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='Clientes_Eliminar(" + rowObject.ID_CLIENTE + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
}


function Clientes_MostrarNuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Clientes/Mantenimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function Clientes_MostrarEditar(ID_CLIENTE) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Clientes/Mantenimiento?id=" + ID_CLIENTE + "&Accion=M", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  cliente **************************************************/

function Clientes_CargarGrilla() {
    var item =
       {
           NOMBRES_APE: $('#Cliente_NombreYape').val(),
           NUMERO_DOCUMENTO: $('#Cliente_NumeroDocumento').val(),
           FLG_ESTADO: $('#Clientes_Estado').val()
       };
    var url = baseUrl + 'Administracion/Clientes/Cliente_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    jQuery("#" + Clientes_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;
                var myData =
                 {
                     CODIGO: idgrilla,
                     ID_CLIENTE: v.ID_CLIENTE,
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
                jQuery("#" + Clientes_Grilla).jqGrid('addRowData', i, myData);
            });
            jQuery("#" + Clientes_Grilla).trigger("reloadGrid");
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}



///*********************************************** ----------------- *************************************************/

///*********************************************** Actualiza  clientes  ************************************************/

function Clientes_Actualizar() {
    if ($("#frmMantenimiento_Cliente").valid()) {
        var item =
                {
                    ID_CLIENTE: $("#hfd_ID_CLIENTE").val(),
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
                    Accion: $("#AccionClientes").val()
                };
        jConfirm("¿ Desea actualizar este cliente ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Administracion/Clientes/Cliente_Actualizar';
                var auditoria = SICA.Ajax(url, item, false);
                if (auditoria != null && auditoria != "") {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            Clientes_CargarGrilla();
                            Clientes_Cerrar();
                            jOkas("Clientes actualizado satisfactoriamente", "Proceso");
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

///************************************************ Inserta clientes  **************************************************/

function Clientes_Ingresar() {
    if ($('#AccionClientes').val() != 'N') {
        Clientes_Actualizar();
    } else {
        if ($("#frmMantenimiento_Cliente").valid()) {
            jConfirm("¿ Desea registrar este cliente ?", "Atención", function (r) {
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
                            ACCION: $("#AccionClientes").val()
                        };
                    var url = baseUrl + 'Administracion/Clientes/Cliente_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Clientes_CargarGrilla();
                                Clientes_Cerrar();
                                jOkas("Clientes registrado satisfactoriamente", "Proceso");
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

///*********************************************** Elimina clientes  ***************************************************/

function Clientes_Eliminar(ID_CLIENTE) {
    jConfirm("¿ Desea eliminar este cliente ?", "Atención", function (r) {
        if (r) {
            var item = {
                ID_CLIENTE: ID_CLIENTE
            };
            var url = baseUrl + 'Administracion/Clientes/Cliente_Eliminar';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        Clientes_CargarGrilla();
                        Clientes_Cerrar();
                        jOkas("Clientes eliminado satisfactoriamente", "Proceso");
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

///*********************************************** Cambia estado de clientes  ******************************************/

function Clientes_Estado(ID_CLIENTE, CHECK) {
    var item = {
        ID_CLIENTE: ID_CLIENTE,
        FLG_ESTADO: CHECK.checked == true ? '1' : '0',
        USU_MODIFICACION: $('#input_hdcodusuario').val(),
    };
    var url = baseUrl + 'Administracion/Clientes/Cliente_Estado';
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