﻿var Ventas_Grilla = 'Ventas_Grilla';
var Ventas_Barra = 'Ventas_Barra';

function Ventas_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Ventas_Limpiar() {
    $("#Ventas_CodigoVenta").val('');
    $('#ID_TIPO_COMPROBANTE_SEARCH').val('');
    $('#Ventas_FLG_TIPO_VENTA').val('');
    Ventas_ConfigurarGrilla();
}

function Ventas_ConfigurarGrilla() {
    var url = baseUrl + 'Ventas/Ventas/Ventas_Paginado';
    $("#" + Ventas_Grilla).GridUnload();
    var colNames = ['Acciones', 'Código', 'ID', 'Código Venta', 'Tipo Comprobante','Cliente','Descuento','Subtotal','Igv','Total','Estado Venta','Tipo Pago',
       'Fecha Venta','COD_COMPROBANTE','Flg_anulado','flg_tipoventa','flg_credito'];
    var colModels = [
            { name: 'ACCION', index: 'ACCION', align: 'center', width: 100, hidden: false, formatter: Ventas_actionAcciones, sortable: false}, // 0
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },// 1
            { name: 'ID_VENTA', index: 'ID_VENTA', width: 100, hidden: true, key: true }, // 2
            { name: 'COD_VENTA', index: 'COD_VENTA', width: 150, hidden: false, align: "left" }, // 3
            { name: 'TIPO_COMPROBANTE', index: 'TIPO_COMPROBANTE', width: 150, hidden: false, align: "left", formatter: Ventas_FormaterComprobante }, // 4
            { name: 'CLIENTE', index: 'CLIENTE', width: 250, hidden: false, align: "left" }, // 5
            { name: 'DESCUENTO', index: 'DESCUENTO', width: 100, hidden: false, align: "left" }, // 6
            { name: 'SUBTOTAL', index: 'SUBTOTAL', width: 100, hidden: false, align: "left" }, // 7
            { name: 'IGV', index: 'IGV', width: 100, hidden: false, align: "left" }, // 8
            { name: 'TOTAL', index: 'TOTAL', width: 100, hidden: false, align: "left" }, // 9       
            { name: 'DESC_ESTADO_VENTA', index: 'DESC_ESTADO_VENTA', width: 150, hidden: false, align: "left", formatter: Ventas_Anulado }, // 10
            { name: 'DESC_TIPO_VENTA', index: 'DESC_TIPO_VENTA', width: 150, hidden: false, align: "left", formatter: Ventas_TipoVenta }, // 11
            { name: 'FEC_CREACION', index: 'FEC_CREACION', width: 150, hidden: false, align: "left" },//12
            { name: 'COD_COMPROBANTE', index: 'COD_COMPROBANTE', width: 150, hidden: true, align: "left" },//13
            { name: 'FLG_ANULADO', index: 'FLG_ANULADO', width: 150, hidden: true, align: "left" },//14
            { name: 'FLG_TIPO_VENTA', index: 'FLG_TIPO_VENTA', width: 150, hidden: true, align: "left" },//15
            { name: 'FLG_ESTADO_CREDITO', index: 'FLG_ESTADO_CREDITO', width: 150, hidden: true, align: "left" },//16

    ];
    var opciones = {
        GridLocal: false, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false,rules:true, rowNumber: 50, rowNumbers: [50, 100, 200, 300, 500],
    };
    SICA.Grilla(Ventas_Grilla, Ventas_Barra, Ventas_Grilla, 400, '', "Lista de Ventas", url, 'ID_VENTA', colNames, colModels, 'ID_VENTA', opciones);
}

function GetRules(Ventas_Grilla) {
    var rules = new Array();
    var FECHA_VENTA = moment().format('DD/MM/YYYY');
    var ID_TIPO_COMPROBANTE = jQuery('#ID_TIPO_COMPROBANTE_SEARCH').val() == '' ? null : "'" + jQuery('#ID_TIPO_COMPROBANTE_SEARCH').val() + "'";
    var FLG_TIPO_VENTA = jQuery('#Ventas_FLG_TIPO_VENTA').val() == '' ? null : "'" + jQuery('#Ventas_FLG_TIPO_VENTA').val() + "'";
    var CODIGO_VENTA = "'" + jQuery('#Ventas_CodigoVenta').val() + "'";
    var _USUARIO_LOGEADO = "'" + jQuery('#input_hdcodusuario').val() + "'"; 

    var POR = "'%'";
    rules = []
    rules.push({ field: 'UPPER(COD_COMPROBANTE)', data: POR + ' + ' + CODIGO_VENTA + ' + ' + POR, op: " LIKE " });
    rules.push({ field: 'ID_TIPO_COMPROBANTE', data: '  ISNULL(' + ID_TIPO_COMPROBANTE + ',ID_TIPO_COMPROBANTE) ', op: " = " });
    rules.push({ field: 'FLG_TIPO_VENTA', data: '  ISNULL(' + FLG_TIPO_VENTA + ',FLG_TIPO_VENTA) ', op: " = " });
    rules.push({ field: 'CONVERT(DATE,FEC_CREACION,103)', data: 'CONVERT(DATE,\'' + FECHA_VENTA + '\',103) ', op: " = " });
    rules.push({ field: 'UPPER(COD_COMPROBANTE)', data: POR + ' + ' + CODIGO_VENTA + ' + ' + POR, op: " LIKE " });
    rules.push({ field: 'UPPER(USU_CREACION)', data: _USUARIO_LOGEADO, op: " = " });

    return rules;
}



function Ventas_actionAcciones(cellvalue, options, rowObject) {
    var _ID_VENTA = rowObject[2];
    var _FLG_FLG_ANULADO = rowObject[14];
    var _btn_Anular =""; 
    if (_FLG_FLG_ANULADO == 0)
        _btn_Anular = " <a class=\"dropdown-item\" onclick='Ventas_AnularVenta(" + _ID_VENTA + ")'><i class=\"bi bi-bag-x\" style=\"color:red;\"></i>&nbsp;  Anular</a>"; 
    

    var _btn = "<div class=\"btn-group\" role=\"group\" title=\"Acciones \" >" +
           " <button  style=\" background: transparent; border: none; color: #000000;font-size: 18px;\" type=\"button\" class=\"btn btn-primary dropdown-toggle\" data-toggle=\"dropdown\" aria-expanded=\"false\"><i class=\"bi bi-list\"></i></button> " +
           " <div class=\"dropdown-menu\" x-placement=\"bottom-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 35px, 0px);\">" +
            _btn_Anular +
            "<a class=\"dropdown-item\" onclick='Ventas_MostrarDevolverProducto(" + _ID_VENTA + ")' ><i class=\"bi bi-box-arrow-in-down-left\" style=\"color:green;\"></i>&nbsp;  Devolver Producto</a>" +
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
    var _DESC_ESTADO_VENTA = rowObject[10];
    var _FLG_ANULADO = rowObject[14];
    var _text = "";
    if (_FLG_ANULADO == 1) {
        _text = "<span class=\"badge badge-danger \" data-bs-toggle=\"tooltip\" title=\"Esta venta fue anulada.\">" + _DESC_ESTADO_VENTA + "</span>";
    }
    else if (_FLG_ANULADO == 0) {
        _text = "<span class=\"badge badge-success\" data-bs-toggle=\"tooltip\" title=\"Venta Realiazada\">"+ _DESC_ESTADO_VENTA +"</span>";
    }
    return _text;
}

function Ventas_TipoVenta(cellvalue, options, rowObject) {
    var _DESC_TIPO_VENTA = rowObject[11];
    var _FLG_TIPO_VENTA = rowObject[15];
    var _text = "";
    if (_FLG_TIPO_VENTA == 1) {
        _text = "<span class=\"badge badge-warning \" data-bs-toggle=\"tooltip\" title=\"Esta venta fue es al credito.\">"+_DESC_TIPO_VENTA+"</span>";
    }
    else if (_FLG_TIPO_VENTA == 0) {
        _text = _DESC_TIPO_VENTA; 
    }
    return _text;
}


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


function Ventas_MostrarDevolverProducto(ID_VENTA) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Ventas/Ventas/Mantenimiento_DevolverProducto?ID_VENTA=" + ID_VENTA, function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}





///*********************************************** ----------------- *************************************************/

///************************************************ Inserta ventas  **************************************************/

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
                            //ADELANTO: $("#ADELANTO").val(),
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

///*********************************************** anular ventas  ***************************************************/

function Ventas_AnularVenta(ID_VENTA) {
    jConfirm("¿ Desea anular esta venta ?, al anular la venta todos los productos de la venta retornan.", "Anular Venta", function (r) {
        if (r) {
            var item = {
                ID_VENTA: ID_VENTA,
                USU_MODIFICACION: $('#input_hdcodusuario').val(),
            };
            var url = baseUrl + 'Ventas/Ventas/Ventas_AnularVenta';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        Ventas_CargarGrilla();
                        //Ventas_Cerrar();
                        jOkas("Venta anulada con exito!", "Proceso");
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

///*********************************************** devolver producto ******************************************/

function Ventas_DevolverProducto(ID_VENTA_DETALLE) {
    jConfirm("¿ Desea devolver este producto ?, al devolver el producto retornara al almacen.", "Devolver Producto", function (r) {
        if (r) {
            var item = {
                ID_VENTA_DETALLE: ID_VENTA_DETALLE,
                USU_MODIFICACION: $('#input_hdcodusuario').val(),
            };
            var url = baseUrl + 'Ventas/Ventas/Ventas_Detalle_DevolverProducto';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        Ventas_ConfigurarGrilla();
                        Ventas_Detalle_CargarGrilla($('#hfd_ID_VENTA').val());
                        //Ventas_Cerrar();
                        jOkas("Producto devuelto con exito!, la cantidad se devolvio al almacen correspondiente.", "Proceso");
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

/////*********************************************** ----------------- *************************************************/