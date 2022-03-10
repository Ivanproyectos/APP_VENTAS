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
    var colNames = ['Editar', 'Eliminar', 'Estado', 'codigo', 'ID', 'Código','Producto' ,'Unidad Medida', 'Pre. Compra', 'Pre. Venta', 'Stock',
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
            { name: 'PRECIO_VENTA', index: 'PRECIO_VENTA', width: 150, hidden: false, align: "left" },
            { name: 'STOCK', index: 'STOCK', width: 100, hidden: false, align: "left" },
            { name: 'FECHA_VENCIMIENTO', index: 'FECHA_VENCIMIENTO', width: 150, hidden: false, align: "left" },
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
    SICA.Grilla(Producto_Grilla, Producto_Barra, '', 400, '', "Lista de Producto", '', 'ID_PRODUCTO', colNames, colModels, '', opciones);
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

function Producto_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar'  onclick='Producto_MostrarEditar(" + rowObject.ID_PRODUCTO + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\" data-target='#myModalNuevo'> <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:17px\"></i></button>";
    return _btn;
}

function Producto_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='Producto_Eliminar(" + rowObject.ID_PRODUCTO + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
}


function Producto_MostrarNuevo() {
    debugger;
    var _ID_SUCURSAL = $('#ID_SUCURSAL').val();
    var _DESC_SUCURSAL = $('select[name="ID_SUCURSAL"] option:selected').text(); 
    if (_ID_SUCURSAL != "") {
        jQuery("#myModalNuevo").html('');
        jQuery("#myModalNuevo").load(baseUrl + "Inventario/Producto/Mantenimiento?id=0&Accion=N&ID_SUCURSAL=" + _ID_SUCURSAL + "&DESC_SUCURSAL=" + _DESC_SUCURSAL, function (responseText, textStatus, request) {
            $('#myModalNuevo').modal({ show: true });
            $.validator.unobtrusive.parse('#myModalNuevo');
            if (request.status != 200) return;
        });
    } else {
        jInfo('Para registrar un nuevo producto selecione el almacen donde se registrara este nuevo producto.','Atención')
    }
}

function Producto_MostrarEditar(ID_PRODUCTO) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Inventario/Producto/Mantenimiento?id=" + ID_PRODUCTO + "&Accion=M", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
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
           FLG_SERIVICIO: $('#Producto_flg_servicio').val(),
           FLG_ESTADO: $('#Producto_Estado').val()
       };
    var url = baseUrl + 'Inventario/Producto/Producto_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    jQuery("#" + Producto_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;
                var myData =
                 {
                     CODIGO: idgrilla,
                     ID_PRODUCTO: v.ID_PRODUCTO,
                     DESC_PRODUCTO: v.DESC_PRODUCTO,
                     COD_PRODUCTO: v.COD_PRODUCTO,
                     DESC_UNIDAD_MEDIDA: v.DESC_UNIDAD_MEDIDA,
                     DESC_CATEGORIA: v.DESC_CATEGORIA,
                     PRECIO_COMPRA: v.PRECIO_COMPRA ,
                     PRECIO_VENTA: v.PRECIO_VENTA,
                     STOCK: v.STOCK,
                     STOCK_MINIMO: v.STOCK_MINIMO,
                     FLG_SERIVICIO: v.FLG_SERIVICIO,
                     FLG_VENCE: v.FLG_VENCE,
                     FECHA_VENCIMIENTO: v.FECHA_VENCIMIENTO,
                     MARCA: v.MARCA,
                     MODELO: v.MODELO,
                     DETALLE: v.DETALLE,
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

        if (_FlgServicio) {
            var item =
                {
                    COD_PRODUCTO: $("#COD_PRODUCTO").val(),
                    DESC_PRODUCTO: $("#DESC_PRODUCTO").val(),
                    ID_UNIDAD_MEDIDA: $("#ID_UNIDAD_MEDIDA").val(),
                    STOCK: $("#STOCK").val(),
                    PRECIO_COMPRA: $("#PRECIO_COMPRA").val(),
                    PRECIO_VENTA: $("#PRECIO_VENTA").val(),
                    STOCK_MINIMO: $("#STOCK_MINIMO").val(),
                    MARCA: $("#MARCA").val(),
                    MODELO: $("#MODELO").val(),
                    FLG_VENCE: $("#FLG_VENCE").is(':checked') ? 1 : 0,
                    FECHA_VENCIMIENTO: $("#FECHA_VENCIMIENTO").val(),
                    FLG_SERVICIO: 0, // producto
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
                    PRECIO_VENTA: $("#PRECIO_VENTA_SERVICIO").val(),
                    DETALLE: $("#DETALLE").val(),
                    FLG_SERVICIO: 1, // producto                         
                    USU_CREACION: $('#input_hdcodusuario').val(),
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

                    if (_FlgServicio) {
                        var item =
                            {
                                COD_PRODUCTO: $("#COD_PRODUCTO").val(),
                                DESC_PRODUCTO: $("#DESC_PRODUCTO").val(),
                                ID_UNIDAD_MEDIDA: $("#ID_UNIDAD_MEDIDA").val(),
                                STOCK: $("#STOCK").val(),
                                PRECIO_COMPRA: $("#PRECIO_COMPRA").val(),
                                PRECIO_VENTA: $("#PRECIO_VENTA").val(),
                                STOCK_MINIMO: $("#STOCK_MINIMO").val(),
                                MARCA: $("#MARCA").val(),
                                MODELO: $("#MODELO").val(),
                                FLG_VENCE: $("#FLG_VENCE").is(':checked') ? 1 :0,
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