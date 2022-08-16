
var Caja_Grilla = 'Caja_Grilla';
var Barra_Grilla = 'Barra_Grilla';

function Caja_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Caja_Limpiar() {
    $("#Caja_FechaRange").val(Fecha_Actual);
    $('#ID_USUARIO').val("").trigger('change');
    $('#ID_SUCURSAL_SEARCH').val("").trigger('change');
    Caja_CargarGrilla();
    Caja_Movimiento_CargarGrilla();
}


function Caja_ConfigurarGrilla() {
    DataTable.GridUnload(Caja_Grilla);
    var colModels = [
          { data: "ID_TIPO_MOVIMIENTO", name: "ID_TIPO_MOVIMIENTO", title: "ID_TIPO_MOVIMIENTO", autoWidth: false, visible: false, },
          { data: "FEC_CREACION", name: "FEC_CREACION", title: "Fecha y Hora", autoWidth: true },
          { data: "TIPO", name: "TIPO", title: "Tipo", autoWidth: true, },
          { data: "DESC_MOVIMIENTO", name: "DESC_MOVIMIENTO", title: "Descripción", autoWidth: true },
          { data: "MONTO", name: "MONTO", title: "Monto", autoWidth: true },
          { data: "USU_CREACION", name: "USU_CREACION", title: "Usuario Creación", visible: false },
          {
              data: null, sortable: false, title: "Acciones", width: "60px",
              render: function (data, type, row, meta) { return Caja_Movimiento_actionAcciones(data.ID_TIPO_MOVIMIENTO); }
          },

    ];
    var opciones = {
        GridLocal: true, multiselect: false, sort: "desc", enumerable: false,
        eliminar: false, search: true, rowNumber: 10, rowNumbers: [10, 25, 50], rules: false, responsive: true, processing: true
    };
    DataTable.Grilla(Caja_Grilla, '', 'ID_TIPO_MOVIMIENTO', colModels, opciones, "ID_TIPO_MOVIMIENTO");
}

function Caja_Movimiento_actionAcciones(ID_TIPO_MOVIMIENTO) {
    var _btn_Eliminar = "<a href=\"javascript:void()\" onclick='Caja_Movimiento_Eliminar(" + ID_TIPO_MOVIMIENTO + ")' data-toggle=\"tooltip\" data-placement=\"top\" title=\"Eliminar Movimiento\"><i class=\"bi bi-trash-fill\"style=\"color:#e40613;\"></i></a>"; 
    return _btn_Eliminar;
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
           FEC_INICIO:  $('#Caja_FechaRange').val().split('-')[0].trim(),
           FEC_FIN:  $('#Caja_FechaRange').val().split('-')[0].trim(),
           COD_USUARIO: $('#ID_USUARIO').val(),
           ID_SUCURSAL: $('#ID_SUCURSAL_SEARCH').val(),
       };
    var url = baseUrl + 'Caja/Caja/Caja_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            if (auditoria.OBJETO != null) {

                $('#Caja_countVenta').text(auditoria.OBJETO.COUNT_VENTA);
                $('#Caja_countAdelanto').text(auditoria.OBJETO.COUNT_COBRAR);
                $('#Caja_countCobrar').text(auditoria.OBJETO.COUNT_ADELANTO);
                $('#Caja_countEgresos').text(auditoria.OBJETO.COUNT_EGRESO);
                $('#Caja_countIngresos').text(auditoria.OBJETO.COUNT_INGRESO);

                $('#Caja_TotalVenta').text(Number(auditoria.OBJETO.TOTAL_VENTA).toFixed(2));
                $('#Caja_TotalAdelanto').text(Number(auditoria.OBJETO.TOTAL_ADELANTO).toFixed(2));
                $('#Caja_TotalCobrar').text(Number(auditoria.OBJETO.TOTAL_COBRAR).toFixed(2));
                $('#Caja_TotalEgresos').text(Number(auditoria.OBJETO.TOTAL_EGRESO).toFixed(2));
                $('#Caja_TotalIgresos').text(Number(auditoria.OBJETO.TOTAL_INGRESO).toFixed(2));
                $('#Caja_countCompras').text(auditoria.OBJETO.COUNT_COMPRAS);
                $('#Caja_TotalCompras').text(Number(auditoria.OBJETO.TOTAL_COMPRAS).toFixed(2));
                $('#Caja_Egresos').text(Number(auditoria.OBJETO.EGRESOS_NETO).toFixed(2));
                $('#Caja_Ingresos').text(Number(auditoria.OBJETO.INGRESOS_NETO).toFixed(2));
                $('#Caja_Total').text(Number(auditoria.OBJETO.TOTAL_NETO).toFixed(2));

              
            }
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
    }
}

///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  movimientos **************************************************/

function Caja_Movimiento_CargarGrilla() {
    var item =
       {
           FEC_INICIO: $('#Caja_FechaRange').val().split('-')[0].trim(),
           FEC_FIN: $('#Caja_FechaRange').val().split('-')[0].trim(),
           COD_USUARIO: $('#ID_USUARIO').val(),
           ID_SUCURSAL: $('#ID_SUCURSAL_SEARCH').val(),
       };
    var url = baseUrl + 'Caja/Caja/Caja_Movimiento_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    DataTable.clearGridData(Caja_Grilla);
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
                     MONTO: _SimboloMoneda + " " +v.MONTO,
                     FEC_CREACION: v.FEC_CREACION,
                     USU_CREACION: v.USU_CREACION,
                     FEC_MODIFICACION: v.FEC_MODIFICACION,
                     USU_MODIFICACION: v.USU_MODIFICACION,

                 };
                DataTable.addRowData(Caja_Grilla, myData);
            });
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
    }
}



///*********************************************** ----------------- *************************************************/

///*********************************************** Actualiza  caja  ************************************************/

function Caja_Movimiento_Actualizar() {
    if ($("#frmMantenimiento_Caja").valid()) {
        var TipoMoviento = 1;  // ingreso 
        if ($('#Movimiento_TipoCheck').is(':checked')) {
            TipoMoviento = 2;  // egreso
        }
        var item =
                {
                    ID_SUCURSAL: $("#ID_SUCURSAL").val(),
                    ID_TIPO_MOVIMIENTO: $("#hfd_ID_TIPO_MOVIMIENTO").val(),
                    FLG_TIPO: TipoMoviento,
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
                            Caja_CargarGrilla(); 
                            Caja_Cerrar();
                            jOkas("Movimiento actualizado satisfactoriamente", "Proceso");
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

///************************************************ Inserta caja  **************************************************/

function Caja_Movimiento_Insertar() {
    if ($('#AccionCaja').val() != 'N') {
        Caja_Actualizar();
    } else {
        if ($("#frmMantenimiento_Caja").valid()) {
            jConfirm("¿ Desea registrar este movimiento ?", "Atención", function (r) {
                if (r) {
                    var TipoMoviento = 1;  // ingreso 
                    if ($('#Movimiento_TipoCheck').is(':checked')) {
                        TipoMoviento = 2;  // egreso
                    }
                    var item =
                        {
                            ID_SUCURSAL: $("#ID_SUCURSAL").val(),
                            FLG_TIPO: TipoMoviento,
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
                                Caja_CargarGrilla(); 
                                Caja_Cerrar();
                                jOkas("Movimiento registrado satisfactoriamente", "Proceso");
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
                        Caja_Cerrar();
                        jOkas("Movimiento eliminado satisfactoriamente", "Proceso");
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