var Ventas_Grilla = 'Ventas_Grilla';
var Ventas_Barra = 'Ventas_Barra';

function Ventas_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Ventas_Limpiar() {
    $("#txtdesVentas").val('');
    $('#cboEstado').val('');

    Ventas_ConfigurarGrilla();
}

function Ventas_ConfigurarGrilla() {
    var url = baseUrl + 'Ventas/Ventas/Ventas_Paginado';
    $("#" + Ventas_Grilla).GridUnload();
    var colNames = ['Acciones', 'Código', 'ID', 'Código Venta', 'Tipo Comprobante','Cliente','Descuento','Subtotal','Igv','Total','Estado Venta','Tipo Pago',
       'Fecha Venta','COD_COMPROBANTE'];
    var colModels = [
            { name: 'ACCION', index: 'ACCION', align: 'center', width: 100, hidden: false, formatter: Ventas_actionAcciones, sortable: false, cellattr: addCellAttrEstilos }, // 0
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },// 1
            { name: 'ID_VENTA', index: 'ID_VENTA', width: 100, hidden: true, key: true }, // 2
            { name: 'COD_VENTA', index: 'COD_VENTA', width: 150, hidden: false, align: "left" }, // 3
            { name: 'TIPO_COMPROBANTE', index: 'TIPO_COMPROBANTE', width: 150, hidden: false, align: "left", formatter: Ventas_FormaterComprobante }, // 4
            { name: 'CLIENTE', index: 'CLIENTE', width: 250, hidden: false, align: "left" }, // 5
            { name: 'DESCUENTO', index: 'DESCUENTO', width: 100, hidden: false, align: "left" }, // 6
            { name: 'SUBTOTAL', index: 'SUBTOTAL', width: 100, hidden: false, align: "left" }, // 7
            { name: 'IGV', index: 'IGV', width: 100, hidden: false, align: "left" }, // 8
            { name: 'TOTAL', index: 'TOTAL', width: 100, hidden: false, align: "left" }, // 9       
            { name: 'FLG_ANULADO', index: 'FLG_ANULADO', width: 150, hidden: false, align: "left", formatter: Ventas_Anulado }, // 10
            { name: 'FLG_TIPO_VENTA', index: 'FLG_TIPO_VENTA', width: 150, hidden: false, align: "left", formatter: Ventas_TipoVenta }, // 11
            { name: 'FEC_CREACION', index: 'FEC_CREACION', width: 150, hidden: false, align: "left" },//12
            { name: 'COD_COMPROBANTE', index: 'COD_COMPROBANTE', width: 150, hidden: true, align: "left" },//13

    ];
    var opciones = {
        GridLocal: false, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false,rules:true, rowNumber: 50, rowNumbers: [50, 100, 200, 300, 500],
    };
    SICA.Grilla(Ventas_Grilla, Ventas_Barra, Ventas_Grilla, 400, '', "Lista de Ventas", url, 'ID_VENTA', colNames, colModels, 'ID_VENTA', opciones);
}

function GetRules(Ventas_Grilla) {
    var rules = new Array();

        rules = []
        //rules.push({ field: 'UPPER(NRO_CUT)', data: POR + ' + ' + CUT + ' + ' + POR, op: " LIKE " });
        //rules.push({ field: 'UPPER(NRO_DOCUMENTO)', data: POR + ' + ' + NRO_DOCUMENTO + ' + ' + POR, op: " LIKE " });
        //rules.push({ field: 'UPPER(ASUNTO)', data: POR + ' + ' + ASUNTO + ' + ' + POR, op: " LIKE " });
        //rules.push({ field: 'FLG_ESTADO', data: '  ISNULL(' + FLG_ESTADO + ',FLG_ESTADO) ', op: " = " });
        //rules.push({ field: 'ID_TIPO_DOCUMENTO', data: '  ISNULL(' + ID_TIPO_DOCUMENTO + ',ID_TIPO_DOCUMENTO) ', op: " = " });
        ////rules.push({ field: 'ID_OFICINA', data: _ID_OFICINA, op: " = " });
        //rules.push({ field: '((ID_SEDE', data: _ID_SEDE + ' OR ID_SEDE_DIGITAL = ' + _ID_SEDE + '))', op: " = " });
        //rules.push({ field: 'FLG_PROCESO', data: 1, op: " = " });
        //rules.push({ field: 'FLG_ARCHIVADO', data: 0, op: " = " });
        //rules.push({ field: 'FLG_ANEXO', data: '  ISNULL(' + FLG_ANEXO + ',FLG_ANEXO) ', op: " = " });
        //rules.push({ field: 'FLG_TIPO', data: 1, op: " = " });

    return rules;
}

function addCellAttrEstilos(rowId, val, rawObject, cm, rdata) {
    return "style='overflow:none !important'";
}


function Ventas_actionAcciones(cellvalue, options, rowObject) {
    var _btn = "<div class=\"btn-group\" role=\"group\">" +
           " <button type=\"button\" class=\"btn btn-primary dropdown-toggle\" data-toggle=\"dropdown\" aria-expanded=\"false\">Primary</button> " +
           " <div class=\"dropdown-menu\" x-placement=\"bottom-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 35px, 0px);\">" +
               " <a class=\"dropdown-item\" href=\"javascript:void()\">Dropdown link</a>" +
                "<a class=\"dropdown-item\" href=\"javascript:void()\">Dropdown link</a>" +
            "</div>" +
        "</div>"; 
    return _btn;
}


function Ventas_FormaterComprobante(cellvalue, options, rowObject) {
    var _DESC_COMPROBANTE = rowObject[4];
    var _DESC_COD_COMPROBANTE = rowObject[13];
    var _text = "";
    var _text = '<span>' + _DESC_COMPROBANTE + '</span><br><span style="font-size: 12px; color: #2c7be5;"><i class="bi bi-upc"></i>&nbsp;Código: ' + _DESC_COD_COMPROBANTE + '</span>';
    return _text;
}



function Ventas_Anulado(cellvalue, options, rowObject) {
    var _FLG_ANULADO = rowObject[10];
    var _text = "";
    if (_FLG_ANULADO == 1) {
        _text = "<span class=\"badge badge-danger \" data-bs-toggle=\"tooltip\" title=\"Esta venta fue anulada.\">Anulado</span>";
    }
    else if (_FLG_ANULADO == 0) {
        _text = "<span class=\"badge badge-success\">Realizado</span>";
    }
    return _text;
}

function Ventas_TipoVenta(cellvalue, options, rowObject) {
    var _FLG_TIPO_VENTA = rowObject[11];
    var _text = "";
    if (_FLG_TIPO_VENTA == 1) {
        _text = "<span class=\"badge badge-warning \" data-bs-toggle=\"tooltip\" title=\"Esta venta fue es al credito.\">Credito</span>";
    }
    else if (_FLG_TIPO_VENTA == 0) {
        _text = "Al Contado"
    }
    return _text;
}


//function Ventas_actionEditar(cellvalue, options, rowObject) {
//    var _btn = "<button title='Editar'  onclick='Ventas_MostrarEditar(" + rowObject.ID_VENTA + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\" data-target='#myModalNuevo'> <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:17px\"></i></button>";
//    return _btn;
//}

function Ventas_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='Ventas_Eliminar(" + rowObject.ID_VENTA + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
}


function Ventas_MostrarNuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Ventas/Ventas/Mantenimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}



function Ventas_MostarBuscarProducto() {
    var _ID_SUCURSAL = $('#inputL_Id_Sucursal').val();
    jQuery("#myModalBuscarProduc").html('');
    jQuery("#myModalBuscarProduc").load(baseUrl + "Ventas/Ventas/Mantenimiento_BuscarProducto?ID_SUCURSAL=" + _ID_SUCURSAL + "&GrillaCarga=" + Ventas_Detalle_Grilla, function (responseText, textStatus, request) {
        $('#myModalBuscarProduc').modal({ show: true, backdrop: 'static', keyboard: false });
        $.validator.unobtrusive.parse('#myModalBuscarProduc');
        if (request.status != 200) return;
    });
}

///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  venta **************************************************/

function Ventas_CargarGrilla() {
    var item =
       {
           ID_ENTIDAD: $("#input_hdid_entidad").val() != 1 ? $("#input_hdid_entidad").val() : 0,
           //$("#input_hdid_entidad").val(),
           DESC_CARGO: $('#txtdesVentas').val(),
           FLG_ESTADO: $('#cboEstado').val()
       };
    var url = baseUrl + 'Ventas/Ventas/Ventas_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    jQuery("#" + Ventas_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
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
                jQuery("#" + Ventas_Grilla).jqGrid('addRowData', i, myData);
            });
            jQuery("#" + Ventas_Grilla).trigger("reloadGrid");
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}



///*********************************************** ----------------- *************************************************/

///*********************************************** Actualiza  cargos  ************************************************/

function Ventas_Actualizar() {
    if ($("#frmMantenimiento_Ventas").valid()) {
        var item =
                {
                    DESC_CARGO: $("#DESC_CARGO").val(),
                    DESC_CARGO: $("#DESC_CARGO").val(),
                    DESC_CARGO: $("#DESC_CARGO").val(),
                    DESC_CARGO: $("#DESC_CARGO").val(),
                    DESC_CARGO: $("#DESC_CARGO").val(),
                    DESC_CARGO: $("#DESC_CARGO").val(),
                    DESC_CARGO: $("#DESC_CARGO").val(),
                    DESC_CARGO: $("#DESC_CARGO").val(),
                    DESC_CARGO: $("#DESC_CARGO").val(),
                    DESC_CARGO: $("#DESC_CARGO").val(),
                    USU_MODIFICACION: $('#input_hdcodusuario').val(),
                    Accion: $("#AccionVentas").val()
                };
        jConfirm("¿ Desea actualizar este venta ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Ventas/Ventas/Ventas_Actualizar';
                var auditoria = SICA.Ajax(url, item, false);
                if (auditoria != null && auditoria != "") {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            Ventas_CargarGrilla();
                            Ventas_Cerrar();
                            jOkas("Ventas actualizado satisfactoriamente", "Proceso");
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

function Ventas_Ingresar() {
        if ($("#frmMantenimiento_Ventas").valid()) {
            var ListaDetalleProductos = new Array();
            var ListaDetalle = new Array();
            ListaDetalleProductos = $("#" + Ventas_Detalle_Grilla).jqGrid('getGridParam', 'data');
            for (var i = 0; i < ListaDetalleProductos.length; i++) {
                var rowData = ListaDetalleProductos[i];
                var myData =
                {
                    ID_PRODUCTO: rowData.ID_PRODUCTO,
                    PRECIO: parseFloat(rowData.PRECIO),
                    CANTIDAD: rowData.CANTIDAD,
                    IMPORTE: parseFloat(rowData.IMPORTE),
                };
                ListaDetalle.push(myData);
            }

            jConfirm("¿ Desea realizar este venta ?", "Atención", function (r) {
                if (r) {


                    var item =
                        {
                            ID_TIPO_COMPROBANTE: $("#ID_TIPO_COMPROBANTE").val(),
                            FECHA_VENTA: $("#FECHA_VENTA").val(),
                            ID_CLIENTE: $("#ID_CLIENTE").val(),
                            ID_SUCURSAL: $("#inputL_Id_Sucursal").val(),
                            //TOTAL_RECIBIDO: $("#TOTAL_RECIBIDO").val(),
                            //VUELTO: $("#VUELTO").val(),
                            FLG_TIPO_VENTA: $("#FLG_TIPO_VENTA").is(':checked')? 1 : 0,
                            DESCUENTO: parseFloat($("#Venta_Descuento").text()),
                            SUB_TOTAL: parseFloat($("#Venta_Subtotal").text()),
                            IGV: parseFloat($("#Venta_Igv").text()),
                            TOTAL: parseFloat($("#Venta_Total").text()),
                            ADELANTO: parseFloat($("#ADELANTO").val()),
                            ListaDetalle : ListaDetalle, 
                            USU_CREACION: $('#input_hdcodusuario').val(),
                            ACCION: $("#AccionVentas").val()
                        };
                    var url = baseUrl + 'Ventas/Ventas/Ventas_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Ventas_ConfigurarGrilla();
                                Ventas_Cerrar();
                                jOkas("Ventas registrado satisfactoriamente", "Proceso");
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

///*********************************************** Elimina cargos  ***************************************************/

function Ventas_Eliminar(ID_VENTA) {
    jConfirm("¿ Desea eliminar este venta ?", "Atención", function (r) {
        if (r) {
            var item = {
                ID_VENTA: ID_VENTA
            };
            var url = baseUrl + 'Ventas/Ventas/Ventas_Eliminar';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        Ventas_CargarGrilla();
                        Ventas_Cerrar();
                        jOkas("Ventas eliminado satisfactoriamente", "Proceso");
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

function Ventas_Estado(ID_VENTA, CHECK) {
    var item = {
        ID_VENTA: ID_VENTA,
        FLG_ESTADO: CHECK.checked == true ? '1' : '0',
        USU_MODIFICACION: $('#input_hdcodusuario').val(),
    };
    var url = baseUrl + 'Ventas/Ventas/Ventas_Estado';
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