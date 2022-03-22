var Producto_Grilla = 'Producto_Grilla';
var Producto_Barra = 'Producto_Barra';

function Producto_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Producto_Limpiar() {
    $("#Producto_Desc").val('');
    $('#Producto_codigo').val('');
    $('#ID_CATEGORIA_SEARCH').val('');
    $('#Producto_flg_servicio').val('2');
    $('#Producto_Estado').val('2');
    Producto_CargarGrilla();
}

function Producto_ConfigurarGrilla() {
    $("#" + Producto_Grilla).GridUnload();
    var colNames = ['Editar', 'Eliminar', 'Estado', 'codigo', 'ID', 'Código','Producto' ,'Unidad Medida', 'Pre. Compra', 'Pre. Venta', 'Stock','stock min',
        'Fec. Vencimiento','Marca','Modelo','Detalle',
        'flg_estado', 'Fecha Creación', 'Usuario Creación', 'Fecha Modificación', 'Usuario Modificación'];
    var colModels = [
            { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 60, hidden: false, formatter: Producto_actionEditar, sortable: false },
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 80, hidden: false, formatter: Producto_actionEliminar, sortable: false },
            { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 70, hidden: false, sortable: true, formatter: Producto_actionActivo, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_PRODUCTO', index: 'ID_PRODUCTO', width: 100, hidden: true, key: true },
            { name: 'COD_PRODUCTO', index: 'COD_PRODUCTO', width: 150, hidden: false, align: "left" },
            { name: 'DESC_PRODUCTO', index: 'DESC_PRODUCTO', width: 250, hidden: false, align: "left" },
            { name: 'DESC_UNIDAD_MEDIDA', index: 'DESC_UNIDAD_MEDIDA', width: 200, hidden: false, align: "left" },
            { name: 'PRECIO_COMPRA', index: 'PRECIO_COMPRA', width: 150, hidden: false, align: "left" },
            { name: 'PRECIO_VENTA', index: 'PRECIO_VENTA', width: 150, hidden: false, align: "left"  ,formatter: Producto_PrecioVentaConcat },
            { name: 'STOCK', index: 'STOCK', width: 100, hidden: false, align: "left", formatter: Producto_StatuStock },
            { name: 'STOCK_MINIMO', index: 'STOCK_MINIMO', width: 150, hidden: true, align: "left" },
            { name: 'FECHA_VENCIMIENTO', index: 'FECHA_VENCIMIENTO', width: 150, hidden: false, align: "left", formatter: Producto_FechaVencimiento },
            { name: 'MARCA', index: 'MARCA', width: 200, hidden: false, align: "left" },
            { name: 'MODELO', index: 'MODELO', width: 200, hidden: false, align: "left" },
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
    SICA.Grilla(Producto_Grilla, Producto_Barra, Producto_Grilla, 400, '', "Lista de Producto", '', 'ID_PRODUCTO', colNames, colModels, '', opciones);
}

function Producto_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject.FLG_ESTADO == 1)
        check_ = 'checked';
    var _btn = " <label class=\"content_toggle_1\">"
            + "<input id=\"Producto_chk_" + rowObject.ID_PRODUCTO + "\" class=\"toggle_Beatiful_1\" type=\"checkbox\" onchange=\"Producto_Estado(" + rowObject.ID_PRODUCTO + ",this)\" " + check_ + ">"
            + "<div class=\"content_toggle_2\">"
            + "  <span class=\"Label_toggle_1\" ></span>"
             + "</div>"
            + "</label>";
    return _btn;
}


function Producto_PrecioVentaConcat(cellvalue, options, rowObject) {
    var _Precioventa = rowObject.PRECIO_VENTA;
     var _text = _SimboloMoneda +' '+ _Precioventa;  
    return _text;
}


function Producto_FechaVencimiento(cellvalue, options, rowObject) {
    var _FechaVencimiento= rowObject.FECHA_VENCIMIENTO;
    var _FechaActual = moment().format('DD/MM/YYYY');
    debugger;
    var _text = ""; 
    if (_FechaActual == _FechaVencimiento) {
        _text = "<span class=\"badge badge-danger \" data-bs-toggle=\"tooltip\" title=\"Producto vencido\">" + rowObject.FECHA_VENCIMIENTO + " <i class=\"bi bi-exclamation-circle\"></i></span>";
    } else {
        var Dias= DifferenceDaysFechas(_FechaActual, _FechaVencimiento);
        if (Dias == 0) {
            _text = rowObject.FECHA_VENCIMIENTO;
        } else if (Dias <= 5) {
            _text = "<span class=\"badge badge-warning \" data-bs-toggle=\"tooltip\" title=\"Producto a punto de vencer!\">" + rowObject.FECHA_VENCIMIENTO + " <i class=\"bi bi-exclamation-triangle\"></i></span>";
        }

    }

    return _text;
}

function Producto_StatuStock(cellvalue, options, rowObject) {
    var _Stock = parseInt(rowObject.STOCK);
    var _StockMinimo = parseInt(rowObject.STOCK_MINIMO);
    debugger;
    var _text = "";
    if (_Stock <= _StockMinimo) {
        _text = "<span class=\"badge badge-danger \" data-bs-toggle=\"tooltip\" title=\"Producto con el stock minimo\">" + rowObject.STOCK + "</span>";
    } else {      
        _text = "<span class=\"badge badge-success\">" + rowObject.STOCK + "</span>";
    }
    return _text;
}

function Producto_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar'  onclick='Producto_MostrarEditar(" + rowObject.ID_PRODUCTO + ");' class=\"btn btn-outline-light\" type=\"button\" style=\"height: 39px;line-height: 5px;\" > <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:17px\"></i></button>";
    return _btn;
}

function Producto_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='Producto_Eliminar(" + rowObject.ID_PRODUCTO + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
}


function Producto_MostrarNuevo() {
    var _ID_SUCURSAL = $('#ID_SUCURSAL').val();
    var _DESC_SUCURSAL = $('select[name="ID_SUCURSAL"] option:selected').text(); 
    if (_ID_SUCURSAL != "") {
        _DESC_SUCURSAL = _DESC_SUCURSAL.replace(/ /g, "+");
        jQuery("#myModalNuevo").html('');
        jQuery("#myModalNuevo").load(baseUrl + "Inventario/Producto/Mantenimiento?id=0&Accion=N&ID_SUCURSAL=" + _ID_SUCURSAL + "&DESC_SUCURSAL=" + _DESC_SUCURSAL, function (responseText, textStatus, request) {
            $('#myModalNuevo').modal({ show: true , backdrop: 'static', keyboard: false  });
            $.validator.unobtrusive.parse('#myModalNuevo');
            if (request.status != 200) return;
        });
    } else {
        jInfo('Para registrar un nuevo producto selecione el almacen donde se registrara este nuevo producto.','Atención')
    }
}

function Producto_MostrarEditar(ID_PRODUCTO) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Inventario/Producto/Mantenimiento?id=" + ID_PRODUCTO + "&Accion=M&ID_SUCURSAL=0&DESC_SUCURSAL=x", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  producto **************************************************/

function Producto_CargarGrilla() {
    var item =
       {
           DESC_PRODUCTO: $('#Producto_Desc').val(),
           COD_PRODUCTO: $('#Producto_codigo').val(),
           ID_CATEGORIA: $('#ID_CATEGORIA_SEARCH').val(),
           FLG_SERVICIO: $('#Producto_flg_servicio').val(),
           FLG_ESTADO: $('#Producto_Estado').val()
       };
    var url = baseUrl + 'Inventario/Producto/Producto_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    jQuery("#" + Producto_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v)   {
                var idgrilla = i + 1;
                var _STOCK = v.STOCK;
                var _STOCK_MINIMO = v.STOCK_MINIMO;
                if (v.ID_UNIDAD_MEDIDA == 1) // si es kilos
                {
                    _STOCK = (v.STOCK / 1000) // gramo a kilos
                    _STOCK_MINIMO = (v.STOCK_MINIMO / 1000) // gramo a kilos
                }
                var myData =
                 {
                     CODIGO: idgrilla,
                     ID_PRODUCTO: v.ID_PRODUCTO,
                     DESC_PRODUCTO: v.DESC_PRODUCTO,
                     COD_PRODUCTO: v.COD_PRODUCTO,
                     DESC_UNIDAD_MEDIDA: v.DESC_UNIDAD_MEDIDA,
                     DESC_CATEGORIA: v.DESC_CATEGORIA,
                     PRECIO_COMPRA:   Number(v.PRECIO_COMPRA).toFixed(2),
                     PRECIO_VENTA:    Number(v.PRECIO_VENTA).toFixed(2), 
                     STOCK: _STOCK,
                     STOCK_MINIMO: _STOCK_MINIMO,
                     FLG_SERIVICIO: v.FLG_SERIVICIO,
                     FLG_VENCE: v.FLG_VENCE,
                     FECHA_VENCIMIENTO: v.FECHA_VENCIMIENTO,
                     MARCA: v.MARCA,
                     MODELO: v.MODELO,
                     DETALLE: v.DETALLE,
                     FLG_ESTADO : v.FLG_ESTADO,
                     FEC_CREACION: v.FEC_CREACION,
                     USU_CREACION: v.USU_CREACION,
                     FEC_MODIFICACION: v.FEC_MODIFICACION,
                     USU_MODIFICACION: v.USU_MODIFICACION,

                 };
                jQuery("#" + Producto_Grilla).jqGrid('addRowData', i, myData);
            });
            jQuery("#" + Producto_Grilla).trigger("reloadGrid");
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}



///*********************************************** ----------------- *************************************************/

///*********************************************** Actualiza  productos  ************************************************/

function Producto_Actualizar() {
    if ($("#frmMantenimiento_Productos").valid()) {
        
        if (!_FlgServicio) {

                var STOCK = $("#STOCK").val();
                var STOCK_MINIMO = $("#STOCK_MINIMO").val();
                if ($("#ID_UNIDAD_MEDIDA").val() == 1) // si es kilos guardo en gramos 
                {
                    STOCK = (STOCK * 1000) // convertir a gramos 
                    STOCK_MINIMO = (STOCK_MINIMO * 1000) // convertir a gramos 
                }
            var item =
                {
                    ID_PRODUCTO: $("#hfd_ID_PRODUCTO").val(),
                    COD_PRODUCTO: $("#COD_PRODUCTO").val(),
                    DESC_PRODUCTO: $("#DESC_PRODUCTO").val(),
                    ID_UNIDAD_MEDIDA: $("#ID_UNIDAD_MEDIDA").val(),  
                    ID_SUCURSAL: $("#hfd_ID_SUCURSAL").val(),
                    STOCK: $("#STOCK").val(),
                    PRECIO_COMPRA: parseFloat($("#PRECIO_COMPRA").val()),
                    PRECIO_VENTA: parseFloat($("#PRECIO_VENTA").val()),
                    STOCK_MINIMO: $("#STOCK_MINIMO").val(),
                    MARCA: $("#MARCA").val(),
                    MODELO: $("#MODELO").val(),
                    FLG_VENCE: $("#FLG_VENCE").is(':checked') ? 1 : 0,
                    FECHA_VENCIMIENTO: $("#FECHA_VENCIMIENTO").val(),
                    FLG_SERVICIO: 0, // producto
                    MiArchivo: CambioImg == true ? Img_Array[0] : null,

                    USU_MODIFICACION: $('#input_hdcodusuario').val(),
                    ACCION: $("#AccionProducto").val()
                };
        } else {
            var item =
                {
                    ID_PRODUCTO: $("#hfd_ID_PRODUCTO").val(),
                    COD_PRODUCTO: $("#COD_PRODUCTO_SERVICIO").val(),
                    DESC_PRODUCTO: $("#DESC_SERVICIO").val(),
                    ID_UNIDAD_MEDIDA: $("#ID_UNIDAD_MEDIDA_SERVICIO").val(),
                    ID_SUCURSAL: $("#hfd_ID_SUCURSAL").val(),
                    PRECIO_VENTA: parseFloat($("#PRECIO_VENTA_SERVICIO").val()),
                    DETALLE: $("#DETALLE").val(),
                    FLG_SERVICIO: 1, // producto                         
                    USU_MODIFICACION: $('#input_hdcodusuario').val(),
                    ACCION: $("#AccionProducto").val()
                };
        }
        jConfirm("¿ Desea actualizar este producto ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Inventario/Producto/Producto_Actualizar';
                var auditoria = SICA.Ajax(url, item, false);
                if (auditoria != null && auditoria != "") {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            Producto_CargarGrilla();
                            Producto_Cerrar();
                            jOkas("Producto actualizado satisfactoriamente", "Proceso");
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

///************************************************ Inserta productos  **************************************************/

function Producto_Ingresar() {
    if ($('#AccionProducto').val() != 'N') {
        Producto_Actualizar();
    } else {
        if ($("#frmMantenimiento_Productos").valid()) {
            jConfirm("¿ Desea registrar este producto ?", "Atención", function (r) {
                if (r) {
                    if (!_FlgServicio) {

                        var STOCK = $("#STOCK").val(); 
                        var STOCK_MINIMO = $("#STOCK_MINIMO").val();
                        if ($("#ID_UNIDAD_MEDIDA").val() == 1) // si es kilos guardo en gramos 
                            {
                                STOCK = (STOCK * 1000) // convertir a gramos 
                                STOCK_MINIMO = (STOCK_MINIMO * 1000) // convertir a gramos 
                            }

                        var item =
                            {
                                COD_PRODUCTO: $("#COD_PRODUCTO").val(),
                                DESC_PRODUCTO: $("#DESC_PRODUCTO").val(),
                                ID_UNIDAD_MEDIDA: $("#ID_UNIDAD_MEDIDA").val(),
                                ID_SUCURSAL: $("#hfd_ID_SUCURSAL").val(),
                                STOCK: STOCK,
                                PRECIO_COMPRA: $("#PRECIO_COMPRA").val(),
                                PRECIO_VENTA: $("#PRECIO_VENTA").val(),
                                STOCK_MINIMO: STOCK_MINIMO,
                                MARCA: $("#MARCA").val(),
                                MODELO: $("#MODELO").val(),
                                FLG_VENCE: $("#FLG_VENCE").is(':checked') ? 1 : 0,
                                FECHA_VENCIMIENTO: $("#FECHA_VENCIMIENTO").val(),
                                FLG_SERVICIO: 0 , // producto
                                MiArchivo: CambioImg == true ? Img_Array[0] : null,

                                USU_CREACION: $('#input_hdcodusuario').val(),
                                ACCION: $("#AccionProducto").val()
                            };
                    } else {
                        var item =
                            {
                                COD_PRODUCTO: $("#COD_PRODUCTO_SERVICIO").val(),
                                DESC_PRODUCTO: $("#DESC_SERVICIO").val(),
                                ID_UNIDAD_MEDIDA: $("#ID_UNIDAD_MEDIDA_SERVICIO").val(),
                                ID_SUCURSAL: $("#hfd_ID_SUCURSAL").val(),
                                PRECIO_VENTA: $("#PRECIO_VENTA_SERVICIO").val(),
                                DETALLE: $("#DETALLE").val(),
                                FLG_SERVICIO: 1, // producto                         
                                USU_CREACION: $('#input_hdcodusuario').val(),
                                ACCION: $("#AccionProducto").val()
                            };
                    }

                    var url = baseUrl + 'Inventario/Producto/Producto_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Producto_CargarGrilla();
                                Producto_Cerrar();
                                jOkas("Producto registrado satisfactoriamente", "Proceso");
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

///*********************************************** Elimina productos  ***************************************************/

function Producto_Eliminar(ID_PRODUCTO) {
    jConfirm("¿ Desea eliminar este producto ?", "Atención", function (r) {
        if (r) {
            var item = {
                ID_PRODUCTO: ID_PRODUCTO
            };
            var url = baseUrl + 'Inventario/Producto/Producto_Eliminar';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        Producto_CargarGrilla();
                        Producto_Cerrar();
                        jOkas("Producto eliminado satisfactoriamente", "Proceso");
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

///*********************************************** Cambia estado de productos  ******************************************/

function Producto_Estado(ID_PRODUCTO, CHECK) {
    var item = {
        ID_PRODUCTO: ID_PRODUCTO,
        FLG_ESTADO: CHECK.checked == true ? '1' : '0',
        USU_MODIFICACION: $('#input_hdcodusuario').val(),
    };
    var url = baseUrl + 'Inventario/Producto/Producto_Estado';
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



function DifferenceDaysFechas(Fecha_ini, Fecha_fin) {
    // First we split the values to arrays date1[0] is the year, [1] the month and [2] the day date1 = date1.split('-'); date2 = date2.split('-'); // Now we convert the array to a Date object, which has several helpful methods date1 = new Date(date1[0], date1[1], date1[2]); date2 = new Date(date2[0], date2[1], date2[2]); // We use the getTime() method and get the unixtime (in milliseconds, but we want seconds, therefore we divide it through 1000) date1_unixtime = parseInt(date1.getTime() / 1000); date2_unixtime = parseInt(date2.getTime() / 1000); // This is the calculated difference in seconds var timeDifference = date2_unixtime - date1_unixtime; // in Hours var timeDifferenceInHours = timeDifference / 60 / 60; // and finaly, in days :) var timeDifferenceInDays = timeDifferenceInHours / 24; alert(timeDifferenceInDays);
    var date1 = Fecha_ini;
    var date2 = Fecha_fin;
    // First we split the values to arrays date1[0] is the year, [1] the month and [2] the day 
    date1 = date1.split('/'); date2 = date2.split('/'); // Now we convert the array to a Date object, which has several helpful methods 
    date1 = new Date(date1[2], date1[1], date1[0]);
    date2 = new Date(date2[2], date2[1], date2[0]); // We use the getTime() method and get the unixtime (in milliseconds, but we want seconds, therefore we divide it through 1000) 
    date1_unixtime = parseInt(date1.getTime() / 1000);
    date2_unixtime = parseInt(date2.getTime() / 1000); // This is the calculated difference in seconds 
    var timeDifference = date2_unixtime - date1_unixtime; // in Hours 
    var timeDifferenceInHours = timeDifference / 60 / 60; // and finaly, in days :)
    var timeDifferenceInDays = timeDifferenceInHours / 24;

    if ( isNaN(timeDifferenceInDays) || timeDifferenceInDays < 0)
        timeDifferenceInDays = 0;
 
    return timeDifferenceInDays;

}