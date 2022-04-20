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
    $('#Producto_flg_servicio').val('');
    $('#Producto_Estado').val('');
    Producto_ConfigurarGrilla();
}

function Producto_ConfigurarGrilla() {
    var url = baseUrl + 'Inventario/Producto/Productos_Paginado';
    $("#" + Producto_Grilla).GridUnload();
    var colNames = ['Opciones', 'Estado', 'codigo', 'ID', 'Imagen', 'Código', 'Producto', 'Unidad Medida', 'Pre. Compra', 'Pre. Venta', 'Stock', 'stock min',
        'Fec. Vencimiento','Marca','Modelo','Detalle',
        'flg_estado', 'Fecha Creación', 'Usuario Creación', 'Fecha Modificación', 'Usuario Modificación', 'ID_UNIDAD_MEDIDA','CODIGO_IMAGE','FLG_SERVICIO'];
    var colModels = [
            { name: 'OPCIONES', index: 'OPCIONES', align: 'center', width: 80, hidden: false, formatter: Producto_actionAcciones, sortable: false }, //0
            { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 70, hidden: false, sortable: true, formatter: Producto_actionActivo, sortable: false }, //1
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, }, //2
            { name: 'ID_PRODUCTO', index: 'ID_PRODUCTO', width: 100, hidden: true, key: true }, //3
            { name: 'IMAGEN', index: 'IMAGEN', width: 100, hidden: false, align: "center", formatter: Producto_FormatterImagenProducto }, //4
            { name: 'COD_PRODUCTO', index: 'COD_PRODUCTO', width: 150, hidden: false, align: "left" }, //5
            { name: 'DESC_PRODUCTO', index: 'DESC_PRODUCTO', width: 250, hidden: false, align: "left" }, //6
            { name: 'DESC_UNIDAD_MEDIDA', index: 'DESC_UNIDAD_MEDIDA', width: 200, hidden: false, align: "left" }, //7
            { name: 'PRECIO_COMPRA', index: 'PRECIO_COMPRA', width: 150, hidden: false, align: "left" }, //8
            { name: 'PRECIO_VENTA', index: 'PRECIO_VENTA', width: 150, hidden: false, align: "left", formatter: Producto_PrecioVentaConcat }, //9
            { name: 'STOCK', index: 'STOCK', width: 100, hidden: false, align: "left", formatter: Producto_StatuStock }, //10
            { name: 'STOCK_MINIMO', index: 'STOCK_MINIMO', width: 150, hidden: true, align: "left" }, //11
            { name: 'FECHA_VENCIMIENTO', index: 'FECHA_VENCIMIENTO', width: 150, hidden: false, align: "left", formatter: Producto_FechaVencimiento }, //12
            { name: 'MARCA', index: 'MARCA', width: 200, hidden: true, align: "left" },  //13
            { name: 'MODELO', index: 'MODELO', width: 200, hidden: true, align: "left" }, //14
            { name: 'DETALLE', index: 'DETALLE', width: 250, hidden: true, align: "left" }, //15
            { name: 'FLG_ESTADO', index: 'FLG_ESTADO', width: 300, hidden: true, align: "left" }, //16
            { name: 'FEC_CREACION', index: 'FEC_CREACION', width: 150, hidden: false, align: "left" },  // 17
            { name: 'USU_CREACION', index: 'USU_CREACION', width: 150, hidden: false, align: "left" }, //18
            { name: 'FEC_MODIFICACION', index: 'FEC_MODIFICACION', width: 150, hidden: true, align: "left" }, // 19
            { name: 'USU_MODIFICACION', index: 'USU_MODIFICACION', width: 150, hidden: true, align: "left" }, //20
            { name: 'ID_UNIDAD_MEDIDA', index: 'ID_UNIDAD_MEDIDA', width: 150, hidden: true, align: "left" }, //21
            { name: 'CODIGO_IMAGE', index: 'CODIGO_IMAGE', width: 0, hidden: true, align: "left" }, //22
            { name: 'FLG_SERVICIO', index: 'FLG_SERVICIO', width: 0, hidden: true, align: "left" }, //23
    ];
    var opciones = {
        GridLocal: false, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, rules:true,rowNumber: 10, rowNumbers: [10, 25, 50, 100],
    };
    SICA.Grilla(Producto_Grilla, Producto_Barra, Producto_Grilla, '', '', "", url, 'ID_PRODUCTO', colNames, colModels, 'ID_PRODUCTO', opciones);
}


function GetRules(Ventas_Grilla) {
    var rules = new Array();
    var FLG_ESTADO = jQuery('#Producto_Estado').val() == '' ? null : "'" + jQuery('#Producto_Estado').val() + "'";
    var FLG_SERVICIO = jQuery('#Producto_flg_servicio').val() == '' ? null : "'" + jQuery('#Producto_flg_servicio').val() + "'";
    var ID_CATEGORIA = jQuery('#ID_CATEGORIA_SEARCH').val() == '' ? null : "'" + jQuery('#ID_CATEGORIA_SEARCH').val() + "'";
    var DESC_PRODUCTO = "'" + jQuery('#Producto_Desc').val() + "'";
    var CODIGO_PRODUCTO = "'" + jQuery('#Producto_codigo').val() + "'";
    var ID_SUCURSAL = jQuery('#ID_SUCURSAL').val() == '' ? null : "'" + jQuery('#ID_SUCURSAL').val() + "'";

    var POR = "'%'";
    rules = []
    rules.push({ field: 'UPPER(DESC_PRODUCTO)', data: POR + ' + ' + DESC_PRODUCTO + ' + ' + POR, op: " LIKE " });
    rules.push({ field: 'UPPER(COD_PRODUCTO)', data: POR + ' + ' + CODIGO_PRODUCTO + ' + ' + POR, op: " LIKE " });
    rules.push({ field: 'ID_CATEGORIA', data: '  ISNULL(' + ID_CATEGORIA + ',ID_CATEGORIA) ', op: " = " });
    rules.push({ field: 'FLG_SERIVICIO', data: '  ISNULL(' + FLG_SERVICIO + ',FLG_SERIVICIO) ', op: " = " });
    rules.push({ field: 'FLG_ESTADO', data: '  ISNULL(' + FLG_ESTADO + ',FLG_ESTADO) ', op: " = " });
    rules.push({ field: 'ID_SUCURSAL', data: '  ISNULL(' + ID_SUCURSAL + ',ID_SUCURSAL)', op: " = " });

    return rules;
}

function Producto_actionAcciones(cellvalue, options, rowObject) {
    var _ID_PRODUCTO = rowObject[3];
    var  _btn_Editar = "<a class=\"dropdown-item\" onclick='Producto_MostrarEditar(" + _ID_PRODUCTO + ")'><i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;\"></i>&nbsp;  Editar</a>";
    var _btn_Eliminar = "<a class=\"dropdown-item\" onclick='Producto_Eliminar(" + _ID_PRODUCTO + ")'><i class=\"bi bi-trash-fill\" style=\"color:#e40613;\"></i>&nbsp;  Eliminar</a>";
    var _btn = "<div class=\"btn-group Group_Acciones\" role=\"group\" title=\"Acciones \" >" +
           "<button  style=\" background: transparent; border: none; color: #000000;font-size: 18px;\" type=\"button\" class=\"btn btn-primary dropdown-toggle\" data-toggle=\"dropdown\" aria-expanded=\"false\"><i class=\"bi bi-list\"></i></button>" +
           "<div class=\"dropdown-menu\" x-placement=\"bottom-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 35px, 0px);\">" +
           _btn_Editar +
           _btn_Eliminar +
            "</div>" +
        "</div>";
    return _btn;
}

function Producto_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject[16])
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
    var _Precioventa = rowObject[9];
     var _text = _SimboloMoneda +' '+ _Precioventa;  
    return _text;
}


function Producto_FechaVencimiento(cellvalue, options, rowObject) {
    var _FechaVencimiento= rowObject[12];
    var _FechaActual = moment().format('DD/MM/YYYY');
    debugger;
    var _text = ""; 
    if (_FechaActual == _FechaVencimiento) {
        _text = "<span class=\"badge badge-danger \" data-bs-toggle=\"tooltip\" title=\"Producto vencido\">" + rowObject[12] + " <i class=\"bi bi-exclamation-circle\"></i></span>";
    } else {
        var Dias= DifferenceDaysFechas(_FechaActual, _FechaVencimiento);
        if (Dias == 0) {
            _text = rowObject[12]
        } else if (Dias <= 5) {
            _text = "<span class=\"badge badge-warning \" data-bs-toggle=\"tooltip\" title=\"Producto a punto de vencer!\">" + rowObject[12] + " <i class=\"bi bi-exclamation-triangle\"></i></span>";
        }

    }

    return _text;
}

function Producto_FormatterImagenProducto(cellvalue, options, rowObject) {
    var _CodigoImage = rowObject[22];
    var _Noimage = "no-image.png";
    var _RutaImage = baseUrl + "Recursos/ImagenProducto/";
    if (_CodigoImage != null && _CodigoImage != "") {
        _RutaImage = _RutaImage + _CodigoImage;
    } else {
        _RutaImage = _RutaImage + _Noimage;
    }
    var ImgFrame = "<img src=\"" + _RutaImage + "\" alt=\"image fail\" class=\"img-produc\" height=\"48\">"
    return ImgFrame;
}

function Producto_StatuStock(cellvalue, options, rowObject) {
    var _Stock = parseInt(rowObject[10]);
    var _StockMinimo = parseInt(rowObject[11]);
    var _IdUnidadMedida = parseInt(rowObject[21]);
    var _Flg_servicio = parseInt(rowObject[23]);

    if (_IdUnidadMedida == 1) {
        _Stock = ConvertGramos_Kilos(_Stock);
        _StockMinimo = ConvertGramos_Kilos(_StockMinimo);
    }

    var _text = "";
    if (_Stock <= _StockMinimo) {
        if (_Flg_servicio != 1)
            _text = "<span class=\"badge badge-danger \" data-bs-toggle=\"tooltip\" title=\"Producto con el stock minimo\">" + _Stock + "</span>";
        else
            _text = _Stock;
    } else {    
            _text = "<span class=\"badge badge-success\">" + _Stock + "</span>";  
    }
    return _text;
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


function Producto_MostrarIngresoProducto() {
    var _ID_SUCURSAL = $('#ID_SUCURSAL').val();
    var _DESC_SUCURSAL = $('select[name="ID_SUCURSAL"] option:selected').text();
    if (_ID_SUCURSAL != "") {
        _DESC_SUCURSAL = _DESC_SUCURSAL.replace(/ /g, "+");
        jQuery("#myModalNuevo").html('');
        jQuery("#myModalNuevo").load(baseUrl + "Inventario/Producto/View_Ingreso?ID_SUCURSAL=" + _ID_SUCURSAL +
            "&DESC_SUCURSAL=" + _DESC_SUCURSAL, function (responseText, textStatus, request) {
            $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
            $.validator.unobtrusive.parse('#myModalNuevo');
            if (request.status != 200) return;
        });
    } else {
        jInfo('Para registrar un ingreso de producto selecione el almacen donde se registrara.', 'Atención')
    }
}

function Producto_MostrarSalidasProducto() {
    var _ID_SUCURSAL = $('#ID_SUCURSAL').val();
    var _DESC_SUCURSAL = $('select[name="ID_SUCURSAL"] option:selected').text();
    if (_ID_SUCURSAL != "") {
        _DESC_SUCURSAL = _DESC_SUCURSAL.replace(/ /g, "+");
        jQuery("#myModalNuevo").html('');
        jQuery("#myModalNuevo").load(baseUrl + "Inventario/Producto/View_Salidas?ID_SUCURSAL=" + _ID_SUCURSAL +
            "&DESC_SUCURSAL=" + _DESC_SUCURSAL, function (responseText, textStatus, request) {
                $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
                $.validator.unobtrusive.parse('#myModalNuevo');
                if (request.status != 200) return;
            });
    } else {
        jInfo('Para registrar una salida de producto selecione el almacen donde se registrara.', 'Atención')
    }
}


function Producto_MostrarTranslado() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Inventario/Producto/View_Translados", function (responseText, textStatus, request) {
            $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
            $.validator.unobtrusive.parse('#myModalNuevo');
            if (request.status != 200) return;
        });
}

///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  producto **************************************************/

//function Producto_CargarGrilla() {
//    var item =
//       {
//           DESC_PRODUCTO: $('#Producto_Desc').val(),
//           COD_PRODUCTO: $('#Producto_codigo').val(),
//           ID_CATEGORIA: $('#ID_CATEGORIA_SEARCH').val(),
//           FLG_SERVICIO: $('#Producto_flg_servicio').val(),
//           FLG_ESTADO: $('#Producto_Estado').val()
//       };
//    var url = baseUrl + 'Inventario/Producto/Producto_Listar';
//    var auditoria = SICA.Ajax(url, item, false);
//    jQuery("#" + Producto_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
//    if (auditoria.EJECUCION_PROCEDIMIENTO) {
//        if (!auditoria.RECHAZAR) {
//            $.each(auditoria.OBJETO, function (i, v)   {
//                var idgrilla = i + 1;
//                var _STOCK = v.STOCK;
//                var _STOCK_MINIMO = v.STOCK_MINIMO;
//                if (v.ID_UNIDAD_MEDIDA == 1) // si es kilos
//                {
//                    _STOCK = (v.STOCK / 1000) // gramo a kilos
//                    _STOCK_MINIMO = (v.STOCK_MINIMO / 1000) // gramo a kilos
//                }
//                var myData =
//                 {
//                     CODIGO: idgrilla,
//                     ID_PRODUCTO: v.ID_PRODUCTO,
//                     DESC_PRODUCTO: v.DESC_PRODUCTO,
//                     COD_PRODUCTO: v.COD_PRODUCTO,
//                     DESC_UNIDAD_MEDIDA: v.DESC_UNIDAD_MEDIDA,
//                     DESC_CATEGORIA: v.DESC_CATEGORIA,
//                     PRECIO_COMPRA:   Number(v.PRECIO_COMPRA).toFixed(2),
//                     PRECIO_VENTA:    Number(v.PRECIO_VENTA).toFixed(2), 
//                     STOCK: _STOCK,
//                     STOCK_MINIMO: _STOCK_MINIMO,
//                     FLG_SERIVICIO: v.FLG_SERIVICIO,
//                     FLG_VENCE: v.FLG_VENCE,
//                     FECHA_VENCIMIENTO: v.FECHA_VENCIMIENTO,
//                     MARCA: v.MARCA,
//                     MODELO: v.MODELO,
//                     DETALLE: v.DETALLE,
//                     FLG_ESTADO : v.FLG_ESTADO,
//                     FEC_CREACION: v.FEC_CREACION,
//                     USU_CREACION: v.USU_CREACION,
//                     FEC_MODIFICACION: v.FEC_MODIFICACION,
//                     USU_MODIFICACION: v.USU_MODIFICACION,

//                 };
//                jQuery("#" + Producto_Grilla).jqGrid('addRowData', i, myData);
//            });
//            jQuery("#" + Producto_Grilla).trigger("reloadGrid");
//        }
//    } else {
//        jError(auditoria.MENSAJE_SALIDA, "Atención");
//    }
//}



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