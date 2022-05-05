
var Caja_Grilla = 'Caja_Grilla';
var Barra_Grilla = 'Barra_Grilla';

function Caja_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Caja_Limpiar() {
    $("#Caja_FechaRange").val(Fecha_Actual);
    //$('#Caja_FechaFin').val(Fecha_Actual);
    $('#ID_USUARIO').val("");
    $('#ID_SUCURSAL_SEARCH').val("");

    Caja_CargarGrilla();
    Caja_Movimiento_CargarGrilla();
}


function Caja_ConfigurarGrilla() {
    DataTable.GridUnload(Caja_Grilla);
    var colModels = [
          { data: "ID_TIPO_MOVIMIENTO", name: "ID_TIPO_MOVIMIENTO", title: "ID_TIPO_MOVIMIENTO", autoWidth: false, visible: false, },
          { data: "FEC_CREACION", name: "FEC_CREACION", title: "Fecha y Hora", autoWidth: false, width: "90px", },
          { data: "TIPO", name: "TIPO", title: "Tipo", autoWidth: false, },
          { data: "DESC_MOVIMIENTO", name: "DESC_MOVIMIENTO", title: "Descripción", autoWidth: true },
          { data: "MONTO", name: "MONTO", title: "Monto", autoWidth: true },
          { data: "USU_CREACION", name: "USU_CREACION", title: "Usuario Creación", autoWidth: true },
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
    var _btn_Editar = "<a class=\"dropdown-item\" onclick='Caja_Movimiento_MostrarEditar(" + ID_TIPO_MOVIMIENTO + ")'><i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;\"></i>&nbsp;  Editar</a>";
    var _btn_Eliminar = "<a class=\"dropdown-item\" onclick='Caja_Movimiento_Eliminar(" + ID_TIPO_MOVIMIENTO + ")'><i class=\"bi bi-trash-fill\" style=\"color:#e40613;\"></i>&nbsp;  Eliminar</a>";
    var _btn = "<div class=\"btn-group Group_Acciones\" role=\"group\" title=\"Acciones \" >" +
           "<button  style=\" background: transparent; border: none; color: #000000;font-size: 18px;\" type=\"button\" class=\"btn  dropdown-toggle\" data-toggle=\"dropdown\" aria-expanded=\"false\"><i class=\"bi bi-list\"></i></button>" +
           "<div class=\"dropdown-menu\" x-placement=\"bottom-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 35px, 0px);\">" +
           _btn_Editar +
           _btn_Eliminar +
            "</div>" +
        "</div>";
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
                     MONTO: v.MONTO, 
                     FEC_CREACION: v.FEC_CREACION,
                     USU_CREACION: v.USU_CREACION,
                     FEC_MODIFICACION: v.FEC_MODIFICACION,
                     USU_MODIFICACION: v.USU_MODIFICACION,

                 };
                DataTable.addRowData(Caja_Grilla, myData);
            });
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