var Compras_Grilla = 'Compras_Grilla';
var Compras_Barra = 'Compras_Barra';

function Compras_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Compras_Limpiar() {
    $("#txtdesCompras").val('');
    $('#cboEstado').val('');

    Compras_CargarGrilla();
}

function Compras_ConfigurarGrilla() {
    $("#" + Compras_Grilla).GridUnload();
    var colNames = ['Editar', 'Eliminar', 'Estado', 'Código', 'ID', 'Código Comprobante', 'Fecha Compra', 'Tipo Comprobante','Proveedor','Descuento','Igv','Total','Detalle',
        'flg_estado', 'Fecha Creación', 'Usuario Creación', 'Fecha Modificación', 'Usuario Modificación'];
    var colModels = [
            { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 60, hidden: false, formatter: Compras_actionEditar, sortable: false },
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 80, hidden: false, formatter: Compras_actionEliminar, sortable: false },
            { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 70, hidden: false, sortable: true, formatter: Compras_actionActivo, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_COMPRA', index: 'ID_COMPRA', width: 100, hidden: true, key: true },
            { name: 'COD_DOCUMENTO', index: 'COD_DOCUMENTO', width: 200, hidden: false, align: "left" },
            { name: 'FCHA_DOCUMENTO', index: 'FCHA_DOCUMENTO', width: 100, hidden: false, align: "left" },
            { name: 'TIPO_COMPROBANTE', index: 'FCHA_DOCUMENTO', width: 200, hidden: false, align: "left" },
            { name: 'PROVEEDOR', index: 'PROVEEDOR', width: 200, hidden: false, align: "left" },
            { name: 'DESCUENTO', index: 'DESCUENTO', width: 200, hidden: false, align: "left" },
            { name: 'IGV', index: 'IGV', width: 200, hidden: false, align: "left" },
            { name: 'TOTAL', index: 'TOTAL', width: 100, hidden: false, align: "left" },
            { name: 'DETALLE', index: 'DETALLE', width: 300, hidden: false, align: "left" },
            { name: 'FLG_ESTADO', index: 'FLG_ESTADO', width: 300, hidden: true, align: "left" },
            { name: 'FEC_CREACION', index: 'FEC_CREACION', width: 150, hidden: false, align: "left" },
            { name: 'USU_CREACION', index: 'USU_CREACION', width: 150, hidden: false, align: "left" },
            { name: 'FEC_MODIFICACION', index: 'FEC_MODIFICACION', width: 150, hidden: false, align: "left" },
            { name: 'USU_MODIFICACION', index: 'USU_MODIFICACION', width: 150, hidden: false, align: "left" },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, rowNumber: 50, rowNumbers: [50, 100, 200, 300, 500],
    };
    SICA.Grilla(Compras_Grilla, Compras_Barra, '', 400, '', "Lista de Compras", '', 'ID_COMPRA', colNames, colModels, '', opciones);
}

function Compras_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject.FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = " <label class=\"content_toggle_1\">"
            + "<input id=\"Compras_chk_" + rowObject.ID_COMPRA + "\" class=\"toggle_Beatiful_1\" type=\"checkbox\" onchange=\"Compras_Estado(" + rowObject.ID_COMPRA + ",this)\" " + check_ + ">"
            + "<div class=\"content_toggle_2\">"
            + "  <span class=\"Label_toggle_1\" ></span>"
             + "</div>"
            + "</label>";
    return _btn;
}

function Compras_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar'  onclick='Compras_MostrarEditar(" + rowObject.ID_COMPRA + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\" data-target='#myModalNuevo'> <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:17px\"></i></button>";
    return _btn;
}

function Compras_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='Compras_Eliminar(" + rowObject.ID_COMPRA + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
}


function Compras_MostrarNuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Compras/Compras/Mantenimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function Compras_MostrarEditar(ID_COMPRA) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Compras/Compras/Mantenimiento?id=" + ID_COMPRA + "&Accion=M", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  cargo **************************************************/

function Compras_CargarGrilla() {
    var item =
       {
           ID_ENTIDAD: $("#input_hdid_entidad").val() != 1 ? $("#input_hdid_entidad").val() : 0,
           //$("#input_hdid_entidad").val(),
           DESC_CARGO: $('#txtdesCompras').val(),
           FLG_ESTADO: $('#cboEstado').val()
       };
    var url = baseUrl + 'Compras/Compras/Compras_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    jQuery("#" + Compras_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;
                var myData =
                 {
                     CODIGO: idgrilla,
                     ID_COMPRA: v.ID_COMPRA,
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
                jQuery("#" + Compras_Grilla).jqGrid('addRowData', i, myData);
            });
            jQuery("#" + Compras_Grilla).trigger("reloadGrid");
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}



///*********************************************** ----------------- *************************************************/

///*********************************************** Actualiza  cargos  ************************************************/

function Compras_Actualizar() {
    if ($("#frmMantenimientoCompras").valid()) {
        var item =
                {
                    ID_COMPRA: $("#hdfID_COMPRA").val(),
                    ID_ENTIDAD: $("#input_hdid_entidad").val() != 1 ? $("#input_hdid_entidad").val() : $("#ID_ENTIDAD").val(),
                    //ID_ENTIDAD: $("#ID_ENTIDAD").val(),
                    DESC_CARGO: $("#DESC_CARGO").val(),
                    USU_MODIFICACION: $('#input_hdcodusuario').val(),
                    Accion: $("#AccionCompras").val()
                };
        jConfirm("¿ Desea actualizar este cargo ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Compras/Compras/Compras_Actualizar';
                var auditoria = SICA.Ajax(url, item, false);
                if (auditoria != null && auditoria != "") {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            Compras_CargarGrilla();
                            Compras_Cerrar();
                            jOkas("Compras actualizado satisfactoriamente", "Proceso");
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

function Compras_Ingresar() {
    if ($('#AccionCompras').val() != 'N') {
        Compras_Actualizar();
    } else {
        if ($("#frmMantenimientoCompras").valid()) {
            jConfirm("¿ Desea registrar este cargo ?", "Atención", function (r) {
                if (r) {
                    var item =
                        {
                            ID_ENTIDAD: $("#input_hdid_entidad").val() != 1 ? $("#input_hdid_entidad").val() : $("#ID_ENTIDAD").val(),
                            DESC_CARGO: $("#DESC_CARGO").val(),
                            USU_CREACION: $('#input_hdcodusuario').val(),
                            ACCION: $("#AccionCompras").val()
                        };
                    var url = baseUrl + 'Compras/Compras/Compras_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Compras_CargarGrilla();
                                Compras_Cerrar();
                                jOkas("Compras registrado satisfactoriamente", "Proceso");
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

function Compras_Eliminar(ID_COMPRA) {
    jConfirm("¿ Desea eliminar este cargo ?", "Atención", function (r) {
        if (r) {
            var item = {
                ID_COMPRA: ID_COMPRA
            };
            var url = baseUrl + 'Compras/Compras/Compras_Eliminar';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        Compras_CargarGrilla();
                        Compras_Cerrar();
                        jOkas("Compras eliminado satisfactoriamente", "Proceso");
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

function Compras_Estado(ID_COMPRA, CHECK) {
    var item = {
        ID_COMPRA: ID_COMPRA,
        FLG_ESTADO: CHECK.checked == true ? '1' : '0',
        USU_MODIFICACION: $('#input_hdcodusuario').val(),
    };
    var url = baseUrl + 'Compras/Compras/Compras_Estado';
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