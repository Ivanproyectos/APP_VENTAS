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
    DataTable.GridUnload(Categoria_Grilla);
    var colModels = [
          { data: "ID_CATEGORIA", name: "ID_CATEGORIA", title: "Código", autoWidth: false, visible: true, },
          { data: "DESC_CATEGORIA", name: "DESC_CATEGORIA", title: "Categoria", autoWidth: true },
          { data: "DESCRIPCION", name: "DESCRIPCION", title: "Descripción", autoWidth: false, },
          {
              data: null, name: "FLG_ESTADO", title: "Activo", width: "80px", sortable: false,
              render: function (data, type, row, meta) { return Categoria_actionActivo(data.FLG_ESTADO, data.ID_CATEGORIA); }
          },
          {
              data: null, sortable: false, title: "Acciones", width: "80px",
              render: function (data, type, row, meta) { return Categoria_actionAcciones(data.ID_CATEGORIA); }
          },

    ];
    var opciones = {
        GridLocal: true, multiselect: false, sort: "desc", enumerable: false,
        eliminar: false, search: true, rowNumber: 10, rowNumbers: [10, 25, 50], rules: false, responsive: true, processing: false
    };
    DataTable.Grilla(Categoria_Grilla, '', 'ID_CATEGORIA', colModels, opciones, "ID_CATEGORIA");

}

function Categoria_actionAcciones(ID_CATEGORIA) {
    var _btn_Editar = "<a class=\"dropdown-item\" onclick='Categoria_MostrarEditar(" + ID_CATEGORIA + ")'><i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;\"></i>&nbsp;  Editar</a>";
    var _btn_Eliminar = "<a class=\"dropdown-item\" onclick='Categoria_Eliminar(" + ID_CATEGORIA + ")'><i class=\"bi bi-trash-fill\" style=\"color:#e40613;\"></i>&nbsp;  Eliminar</a>";
    var _btn = "<div class=\"btn-group Group_Acciones\" role=\"group\" title=\"Acciones \" >" +
           "<button  style=\" background: transparent; border: none; color: #000000;font-size: 18px;\" type=\"button\" class=\"btn  dropdown-toggle\" data-toggle=\"dropdown\" aria-expanded=\"false\"><i class=\"bi bi-list\"></i></button>" +
           "<div class=\"dropdown-menu\" x-placement=\"bottom-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 35px, 0px);\">" +
           _btn_Editar +
           _btn_Eliminar +
            "</div>" +
        "</div>";
    return _btn;
}


function Categoria_actionActivo(FLG_ESTADO, ID_CATEGORIA) {
    var check_ = 'check';
    if (FLG_ESTADO == 1)
        check_ = 'checked';
    var _btn = "<input type=\"checkbox\" id=\"Categoria_chk_" + ID_CATEGORIA + "\"  data-switch=\"state\" onchange=\"Categoria_Estado(" + ID_CATEGORIA + ",this)\" " + check_ + ">"
              + " <label for=\"Categoria_chk_" + ID_CATEGORIA + "\" data-on-label=\"Yes\" data-off-label=\"No\"></label>";
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
           //DESC_CATEGORIA: $('#Categoria_Desc').val(),
           FLG_ESTADO:2
       };
    var url = baseUrl + 'Inventario/Categoria/Categoria_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    DataTable.clearGridData(Categoria_Grilla);
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
                DataTable.addRowData(Categoria_Grilla, myData);
            });
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