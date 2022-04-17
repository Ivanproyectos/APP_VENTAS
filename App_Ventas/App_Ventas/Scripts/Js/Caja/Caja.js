
var Caja_Grilla = 'Caja_Grilla';
var Barra_Grilla = 'Barra_Grilla';

function Caja_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Caja_Limpiar() {
    $("#Caja_FechaInicio").val(Fecha_Actual);
    $('#Caja_FechaFin').val(Fecha_Actual);
    $('#ID_USUARIO').val("");
    $('#ID_SUCURSAL_SEARCH').val("");

    Caja_CargarGrilla();
    Caja_Movimiento_CargarGrilla();
}


function Caja_ConfigurarGrilla() {
    $("#" + Caja_Grilla).GridUnload();
    var colNames = ['Editar','Eliminar','codigo', 'ID', 'Fecha y Hora', 'Tipo', 'Descripción','Monto', 'Usuario Creación'];
    var colModels = [
            { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 70, hidden: false, sortable: false, formatter: Caja_Movimiento_actionEditar, sortable: false },
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 70, hidden: false, sortable: false, formatter: Caja_Movimiento_actionEliminar, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_TIPO_MOVIMIENTO', index: 'ID_TIPO_MOVIMIENTO', align: 'center', width: 100, hidden: true, key: true },
            { name: 'FEC_CREACION', index: 'FEC_CREACION', align: 'left', width: 200, hidden: false },
            { name: 'TIPO', index: 'TIPO', align: 'left', width: 200, hidden: false },
            { name: 'DESC_MOVIMIENTO', index: 'DESC_MOVIMIENTO', align: 'left', width: 200, hidden: false },
            { name: 'MONTO', index: 'MONTO', align: 'left', width: 100, hidden: false },
            { name: 'USU_CREACION', index: 'USU_CREACION', align: 'left', width: 200, hidden: false },
        
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false
    };
    SICA.Grilla(Caja_Grilla, Barra_Grilla, Caja_Grilla, 400, '', "", '', 'ID_TIPO_MOVIMIENTO', colNames, colModels, '', opciones);
}


function Caja_Movimiento_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar'  onclick='Caja_Movimiento_MostrarEditar(" + rowObject.ID_TIPO_MOVIMIENTO + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\" data-target='#myModalNuevo'> <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:17px\"></i></button>";
    return _btn;
}

function Caja_Movimiento_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='Caja_Movimiento_Eliminar(" + rowObject.ID_TIPO_MOVIMIENTO + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
}

function Caja_Movimieto_MostrarNuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Caja/Caja/View_Movimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function Caja_Movimiento_MostrarEditar(ID_TIPO_MOVIMIENTO) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Caja/Caja/View_Movimiento?id=" + ID_TIPO_MOVIMIENTO + "&Accion=M", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

///*********************************************** ----------------- *************************************************/

///*********************************************** Lista caja **************************************************/

function Caja_CargarGrilla() {
    var item =
       {
           FEC_INICIO: $('#Caja_FechaInicio').val(),
           FEC_FIN: $('#Caja_FechaFin').val(),
           COD_USUARIO: $('#ID_USUARIO').val(),
           ID_SUCURSAL: $('#ID_SUCURSAL_SEARCH').val(),
       };
    var url = baseUrl + 'Caja/Caja/Caja_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            if (auditoria.OBJETO != null) {
                var _TotalVenta = auditoria.OBJETO.TOTAL_VENTA;
                var _TotalAdelanto = auditoria.OBJETO.TOTAL_ADELANTO;
                var _TotalCobrar = auditoria.OBJETO.TOTAL_COBRAR;
                var TotalIngresos = auditoria.OBJETO.TOTAL_INGRESO;
                var TotalEgresos = auditoria.OBJETO.TOTAL_EGRESO;

                var Total_Ingresos = (_TotalVenta + _TotalAdelanto + _TotalCobrar + TotalIngresos) ;
                var Total = (Total_Ingresos - TotalEgresos);

                $('#Caja_countVenta').text(auditoria.OBJETO.COUNT_VENTA);
                $('#Caja_countAdelanto').text(auditoria.OBJETO.COUNT_COBRAR);
                $('#Caja_countCobrar').text(auditoria.OBJETO.COUNT_ADELANTO);
                $('#Caja_countEgresos').text(auditoria.OBJETO.COUNT_EGRESO);
                $('#Caja_countIngresos').text(auditoria.OBJETO.COUNT_INGRESO);

                $('#Caja_TotalVenta').text(Number(_TotalVenta).toFixed(2));
                $('#Caja_TotalAdelanto').text(Number(_TotalAdelanto).toFixed(2));
                $('#Caja_TotalCobrar').text(Number(_TotalCobrar).toFixed(2));
                $('#Caja_TotalEgresos').text(Number(TotalEgresos).toFixed(2));
                $('#Caja_TotalIgresos').text(Number(TotalIngresos).toFixed(2));

                $('#Caja_Egresos').text(Number(TotalEgresos).toFixed(2));
                $('#Caja_Ingresos').text(Number(Total_Ingresos).toFixed(2));
                $('#Caja_Total').text(Number(Total).toFixed(2));

              
            }
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}

///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  movimientos **************************************************/

function Caja_Movimiento_CargarGrilla() {
    var item =
       {
           FEC_INICIO: $('#Caja_FechaInicio').val(),
           FEC_FIN: $('#Caja_FechaFin').val(),
           COD_USUARIO: $('#ID_USUARIO').val(),
           ID_SUCURSAL: $('#ID_SUCURSAL_SEARCH').val(),
       };
    var url = baseUrl + 'Caja/Caja/Caja_Movimiento_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    jQuery("#" + Caja_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;
                var Tipo = ""; 
                if (v.FLG_TIPO == 1) {
                    Tipo = "<span>Ingreso &nbsp; <i class=\"bi bi-box-arrow-in-right text-success\"></i></span>"
                } else {
                    Tipo = "<span>Egreso &nbsp; <i class=\"bi bi-box-arrow-in-left text-danger\"></i></span>"
                }
                var myData =
                 {
                     CODIGO: idgrilla,
                     ID_TIPO_MOVIMIENTO: v.ID_TIPO_MOVIMIENTO,
                     ID_SUCURSAL: v.ID_SUCURSAL,
                     TIPO: Tipo,
                     DESC_MOVIMIENTO: v.DESC_MOVIMIENTO,
                     MONTO: v.MONTO, 
                     FEC_CREACION: v.FEC_CREACION,
                     USU_CREACION: v.USU_CREACION,
                     FEC_MODIFICACION: v.FEC_MODIFICACION,
                     USU_MODIFICACION: v.USU_MODIFICACION,

                 };
                jQuery("#" + Caja_Grilla).jqGrid('addRowData', i, myData);
            });
            jQuery("#" + Caja_Grilla).trigger("reloadGrid");
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}



///*********************************************** ----------------- *************************************************/

///*********************************************** Actualiza  caja  ************************************************/

function Caja_Movimiento_Actualizar() {
    if ($("#frmMantenimiento_Caja").valid()) {
        var item =
                {
                    ID_SUCURSAL: $("#ID_SUCURSAL").val(),
                    ID_TIPO_MOVIMIENTO: $("#hfd_ID_TIPO_MOVIMIENTO").val(),
                    FLG_TIPO: _valor = $('input:radio[name=Movimiento_Tipo]:checked').val(),
                    DESC_MOVIMIENTO: $("#DESC_MOVIMIENTO").val(),
                    MONTO: $("#MONTO").val(),
                    USU_CREACION: $('#input_hdcodusuario').val(),
                    USU_MODIFICACION: $('#input_hdcodusuario').val(),
                    Accion: $("#AccionCaja").val()
                };
        jConfirm("¿ Desea actualizar este movimiento ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Caja/Caja/Caja_Movimiento_Actualizar';
                var auditoria = SICA.Ajax(url, item, false);
                if (auditoria != null && auditoria != "") {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            Caja_Movimiento_CargarGrilla();
                            Caja_Cerrar();
                            jOkas("Movimiento actualizado satisfactoriamente", "Proceso");
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

///************************************************ Inserta caja  **************************************************/

function Caja_Movimiento_Insertar() {
    if ($('#AccionCaja').val() != 'N') {
        Caja_Actualizar();
    } else {
        if ($("#frmMantenimiento_Caja").valid()) {
            jConfirm("¿ Desea registrar este movimiento ?", "Atención", function (r) {
                if (r) {
                    var item =
                        {
                            ID_SUCURSAL: $("#ID_SUCURSAL").val(),
                            FLG_TIPO: _valor = $('input:radio[name=Movimiento_Tipo]:checked').val(),
                            DESC_MOVIMIENTO: $("#DESC_MOVIMIENTO").val(),
                            MONTO: $("#MONTO").val(),
                            USU_CREACION: $('#input_hdcodusuario').val(),
                            ACCION: $("#AccionCaja").val()
                        };
                    var url = baseUrl + 'Caja/Caja/Caja_Movimiento_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Caja_Movimiento_CargarGrilla();
                                Caja_Cerrar();
                                jOkas("Movimiento registrado satisfactoriamente", "Proceso");
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

///*********************************************** Elimina caja  ***************************************************/

function Caja_Movimiento_Eliminar(ID_TIPO_MOVIMIENTO) {
    jConfirm("¿ Desea eliminar este movimiento ?", "Atención", function (r) {
        if (r) {
            var item = {
                ID_TIPO_MOVIMIENTO: ID_TIPO_MOVIMIENTO
            };
            var url = baseUrl + 'Caja/Caja/Caja_Movimiento_Eliminar';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        Caja_Movimiento_CargarGrilla();
                        Categoria_Cerrar();
                        jOkas("Movimiento eliminado satisfactoriamente", "Proceso");
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