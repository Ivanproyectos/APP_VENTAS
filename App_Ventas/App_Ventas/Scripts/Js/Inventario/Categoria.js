var Categoria_Grilla = 'Categoria_Grilla';
var Categoria_Barra = 'Categoria_Barra';

function Categoria_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Categoria_Limpiar() {
    $("#Categoria_Desc").val('');
    $('#Categoria_Estado').val(2);

    Categoria_CargarGrilla();
}

function Categoria_ConfigurarGrilla() {
    $("#" + Categoria_Grilla).GridUnload();
    var colNames = ['Editar', 'Eliminar', 'Estado', 'codigo', 'ID', 'Categoria', 'Descripción',
        'flg_estado', 'Fecha Creación', 'Usuario Creación', 'Fecha Modificación', 'Usuario Modificación'];
    var colModels = [
            { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 60, hidden: false, formatter: Categoria_actionEditar, sortable: false },
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 80, hidden: false, formatter: Categoria_actionEliminar, sortable: false },
            { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 70, hidden: false, sortable: true, formatter: Categoria_actionActivo, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_CATEGORIA', index: 'ID_CATEGORIA', width: 100, hidden: true, key: true },
            { name: 'DESC_CATEGORIA', index: 'DESC_CATEGORIA', width: 200, hidden: false, align: "left" },
            { name: 'DESCRIPCION', index: 'DESCRIPCION', width: 300, hidden: false, align: "left" },
            { name: 'FLG_ESTADO', index: 'FLG_ESTADO', width: 300, hidden: true, align: "left" },
            { name: 'FEC_CREACION', index: 'FEC_CREACION', width: 150, hidden: false, align: "left" },
            { name: 'USU_CREACION', index: 'USU_CREACION', width: 150, hidden: false, align: "left" },
            { name: 'FEC_MODIFICACION', index: 'FEC_MODIFICACION', width: 150, hidden: false, align: "left" },
            { name: 'USU_MODIFICACION', index: 'USU_MODIFICACION', width: 150, hidden: false, align: "left" },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, rowNumber: 50, rowNumbers: [50, 100, 200, 300, 500],
    };
    SICA.Grilla(Categoria_Grilla, Categoria_Barra, '', 400, '', "Lista de Categoria", '', 'ID_CATEGORIA', colNames, colModels, '', opciones);
}

function Categoria_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject.FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = " <label class=\"content_toggle_1\">"
            + "<input id=\"Categoria_chk_" + rowObject.ID_CATEGORIA + "\" class=\"toggle_Beatiful_1\" type=\"checkbox\" onchange=\"Categoria_Estado(" + rowObject.ID_CATEGORIA + ",this)\" " + check_ + ">"
            + "<div class=\"content_toggle_2\">"
            + "  <span class=\"Label_toggle_1\" ></span>"
             + "</div>"
            + "</label>";
    return _btn;
}

function Categoria_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar'  onclick='Categoria_MostrarEditar(" + rowObject.ID_CATEGORIA + ");' class=\"btn btn-outline-light\" type=\"button\"  style=\"height: 39px;line-height: 5px;\"> <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:17px\"></i></button>";
    return _btn;
}

function Categoria_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='Categoria_Eliminar(" + rowObject.ID_CATEGORIA + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
}


function Categoria_MostrarNuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Inventario/Categoria/Mantenimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function Categoria_MostrarEditar(ID_CATEGORIA) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Inventario/Categoria/Mantenimiento?id=" + ID_CATEGORIA + "&Accion=M", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  categoria **************************************************/

function Categoria_CargarGrilla() {
    var item =
       {
           DESC_CATEGORIA: $('#Categoria_Desc').val(),
           FLG_ESTADO: $('#Categoria_Estado').val()
       };
    var url = baseUrl + 'Inventario/Categoria/Categoria_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    jQuery("#" + Categoria_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;
                var myData =
                 {
                     CODIGO: idgrilla,
                     ID_CATEGORIA: v.ID_CATEGORIA,
                     DESC_CATEGORIA: v.DESC_CATEGORIA,
                     DESCRIPCION: v.DESCRIPCION,
                     FLG_ESTADO: v.FLG_ESTADO,
                     FEC_CREACION: v.FEC_CREACION,
                     USU_CREACION:v.USU_CREACION,
                     FEC_MODIFICACION: v.FEC_MODIFICACION,
                     USU_MODIFICACION: v.USU_MODIFICACION,
   
                 };
                jQuery("#" + Categoria_Grilla).jqGrid('addRowData', i, myData);
            });
            jQuery("#" + Categoria_Grilla).trigger("reloadGrid");
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}



///*********************************************** ----------------- *************************************************/

///*********************************************** Actualiza  categorias  ************************************************/

function Categoria_Actualizar() {
    if ($("#frmMantenimiento_Categoria").valid()) {
        var item =
                {
                    ID_CATEGORIA: $("#hfd_ID_CATEGORIA").val(),
                    DESC_CATEGORIA: $("#DESC_CATEGORIA").val(),
                    DESCRIPCION: $("#DESCRIPCION").val(),
                    USU_MODIFICACION: $('#input_hdcodusuario').val(),
                    Accion: $("#AccionCategoria").val()
                };
        jConfirm("¿ Desea actualizar este categoria ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Inventario/Categoria/Categoria_Actualizar';
                var auditoria = SICA.Ajax(url, item, false);
                if (auditoria != null && auditoria != "") {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            Categoria_CargarGrilla();
                            Categoria_Cerrar();
                            jOkas("Categoria actualizado satisfactoriamente", "Proceso");
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

///************************************************ Inserta categorias  **************************************************/

function Categoria_Ingresar() {
    if ($('#AccionCategoria').val() != 'N') {
        Categoria_Actualizar();
    } else {
        if ($("#frmMantenimiento_Categoria").valid()) {
            jConfirm("¿ Desea registrar este categoria ?", "Atención", function (r) {
                if (r) {
                    var item =
                        {
                            DESC_CATEGORIA: $("#DESC_CATEGORIA").val(),
                            DESCRIPCION: $("#DESCRIPCION").val(),
                            USU_CREACION: $('#input_hdcodusuario').val(),
                            ACCION: $("#AccionCategoria").val()
                        };
                    var url = baseUrl + 'Inventario/Categoria/Categoria_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Categoria_CargarGrilla();
                                Categoria_Cerrar();
                                jOkas("Categoria registrado satisfactoriamente", "Proceso");
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

///*********************************************** Elimina categorias  ***************************************************/

function Categoria_Eliminar(ID_CATEGORIA) {
    jConfirm("¿ Desea eliminar este categoria ?", "Atención", function (r) {
        if (r) {
            var item = {
                ID_CATEGORIA: ID_CATEGORIA
            };
            var url = baseUrl + 'Inventario/Categoria/Categoria_Eliminar';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        Categoria_CargarGrilla();
                        Categoria_Cerrar();
                        jOkas("Categoria eliminado satisfactoriamente", "Proceso");
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

///*********************************************** Cambia estado de categorias  ******************************************/

function Categoria_Estado(ID_CATEGORIA, CHECK) {
    var item = {
        ID_CATEGORIA: ID_CATEGORIA,
        FLG_ESTADO: CHECK.checked == true ? '1' : '0',
        USU_MODIFICACION: $('#input_hdcodusuario').val(),
    };
    var url = baseUrl + 'Inventario/Categoria/Categoria_Estado';
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