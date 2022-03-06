var CuentasCobrar_Grilla = 'CuentasCobrar_Grilla';
var CuentasCobrar_Barra = 'CuentasCobrar_Barra';

function CuentasCobrar_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function CuentasCobrar_Limpiar() {
    $("#txtdesCuentasCobrar").val('');
    $('#cboEstado').val('');

    CuentasCobrar_CargarGrilla();
}

function CuentasCobrar_ConfigurarGrilla() {
    $("#" + CuentasCobrar_Grilla).GridUnload();
    var colNames = ['Acciones', 'Código', 'ID', 'Código Venta', 'Tipo Comprobante','Cliente','Descuento','Adelanto','Total','Fecha Compra',
        'flg_estado', 'Fecha Creación', 'Usuario Creación'];
    var colModels = [
            { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 100, hidden: false, formatter: CuentasCobrar_actionEditar, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_VENTA', index: 'ID_VENTA', width: 100, hidden: true, key: true },
            { name: 'COD_VENTA', index: 'COD_VENTA', width: 200, hidden: false, align: "left" },
            { name: 'TIPO_COMPROBANTE', index: 'TIPO_COMPROBANTE', width: 200, hidden: false, align: "left" },
            { name: 'CLIENTE', index: 'CLIENTE', width: 300, hidden: false, align: "left" },
            { name: 'DESCUENTO', index: 'DESCUENTO', width: 200, hidden: false, align: "left" },
            { name: 'ADELANTO', index: 'ADELANTO', width: 100, hidden: false, align: "left" },
            { name: 'TOTAL', index: 'TOTAL', width: 100, hidden: false, align: "left" },
            { name: 'FECHA_COMPRA', index: 'FECHA_COMPRA', width: 100, hidden: false, align: "left" },
            { name: 'FLG_ESTADO', index: 'FLG_ESTADO', width: 300, hidden: true, align: "left" },
            { name: 'FEC_CREACION', index: 'FEC_CREACION', width: 150, hidden: false, align: "left" },
            { name: 'USU_CREACION', index: 'USU_CREACION', width: 150, hidden: false, align: "left" },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, rowNumber: 50, rowNumbers: [50, 100, 200, 300, 500],
    };
    SICA.Grilla(CuentasCobrar_Grilla, CuentasCobrar_Barra, '', 400, '', "Lista de Cuentas por Cobrar", '', 'ID_VENTA', colNames, colModels, '', opciones);
}

function CuentasCobrar_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject.FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = " <label class=\"content_toggle_1\">"
            + "<input id=\"CuentasCobrar_chk_" + rowObject.ID_VENTA + "\" class=\"toggle_Beatiful_1\" type=\"checkbox\" onchange=\"CuentasCobrar_Estado(" + rowObject.ID_VENTA + ",this)\" " + check_ + ">"
            + "<div class=\"content_toggle_2\">"
            + "  <span class=\"Label_toggle_1\" ></span>"
             + "</div>"
            + "</label>";
    return _btn;
}

function CuentasCobrar_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar'  onclick='CuentasCobrar_MostrarEditar(" + rowObject.ID_VENTA + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\" data-target='#myModalNuevo'> <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:17px\"></i></button>";
    return _btn;
}

function CuentasCobrar_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='CuentasCobrar_Eliminar(" + rowObject.ID_VENTA + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
}


function CuentasCobrar_MostrarNuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "CuentasCobrar/CuentasCobrar/Mantenimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function CuentasCobrar_MostrarEditar(ID_VENTA) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "CuentasCobrar/CuentasCobrar/Mantenimiento?id=" + ID_VENTA + "&Accion=M", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  cargo **************************************************/

function CuentasCobrar_CargarGrilla() {
    var item =
       {
           ID_ENTIDAD: $("#input_hdid_entidad").val() != 1 ? $("#input_hdid_entidad").val() : 0,
           //$("#input_hdid_entidad").val(),
           DESC_CARGO: $('#txtdesCuentasCobrar').val(),
           FLG_ESTADO: $('#cboEstado').val()
       };
    var url = baseUrl + 'CuentasCobrar/CuentasCobrar/CuentasCobrar_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    jQuery("#" + CuentasCobrar_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;
                var myData =
                 {
                     CODIGO: idgrilla,
                     ID_VENTA: v.ID_VENTA,
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
                jQuery("#" + CuentasCobrar_Grilla).jqGrid('addRowData', i, myData);
            });
            jQuery("#" + CuentasCobrar_Grilla).trigger("reloadGrid");
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}



///*********************************************** ----------------- *************************************************/

///*********************************************** Actualiza  cargos  ************************************************/

function CuentasCobrar_Actualizar() {
    if ($("#frmMantenimientoCuentasCobrar").valid()) {
        var item =
                {
                    ID_VENTA: $("#hdfID_VENTA").val(),
                    ID_ENTIDAD: $("#input_hdid_entidad").val() != 1 ? $("#input_hdid_entidad").val() : $("#ID_ENTIDAD").val(),
                    //ID_ENTIDAD: $("#ID_ENTIDAD").val(),
                    DESC_CARGO: $("#DESC_CARGO").val(),
                    USU_MODIFICACION: $('#input_hdcodusuario').val(),
                    Accion: $("#AccionCuentasCobrar").val()
                };
        jConfirm("¿ Desea actualizar este cargo ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'CuentasCobrar/CuentasCobrar/CuentasCobrar_Actualizar';
                var auditoria = SICA.Ajax(url, item, false);
                if (auditoria != null && auditoria != "") {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            CuentasCobrar_CargarGrilla();
                            CuentasCobrar_Cerrar();
                            jOkas("CuentasCobrar actualizado satisfactoriamente", "Proceso");
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

function CuentasCobrar_Ingresar() {
    if ($('#AccionCuentasCobrar').val() != 'N') {
        CuentasCobrar_Actualizar();
    } else {
        if ($("#frmMantenimientoCuentasCobrar").valid()) {
            jConfirm("¿ Desea registrar este cargo ?", "Atención", function (r) {
                if (r) {
                    var item =
                        {
                            ID_ENTIDAD: $("#input_hdid_entidad").val() != 1 ? $("#input_hdid_entidad").val() : $("#ID_ENTIDAD").val(),
                            DESC_CARGO: $("#DESC_CARGO").val(),
                            USU_CREACION: $('#input_hdcodusuario').val(),
                            ACCION: $("#AccionCuentasCobrar").val()
                        };
                    var url = baseUrl + 'CuentasCobrar/CuentasCobrar/CuentasCobrar_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                CuentasCobrar_CargarGrilla();
                                CuentasCobrar_Cerrar();
                                jOkas("CuentasCobrar registrado satisfactoriamente", "Proceso");
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

function CuentasCobrar_Eliminar(ID_VENTA) {
    jConfirm("¿ Desea eliminar este cargo ?", "Atención", function (r) {
        if (r) {
            var item = {
                ID_VENTA: ID_VENTA
            };
            var url = baseUrl + 'CuentasCobrar/CuentasCobrar/CuentasCobrar_Eliminar';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        CuentasCobrar_CargarGrilla();
                        CuentasCobrar_Cerrar();
                        jOkas("CuentasCobrar eliminado satisfactoriamente", "Proceso");
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

function CuentasCobrar_Estado(ID_VENTA, CHECK) {
    var item = {
        ID_VENTA: ID_VENTA,
        FLG_ESTADO: CHECK.checked == true ? '1' : '0',
        USU_MODIFICACION: $('#input_hdcodusuario').val(),
    };
    var url = baseUrl + 'CuentasCobrar/CuentasCobrar/CuentasCobrar_Estado';
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