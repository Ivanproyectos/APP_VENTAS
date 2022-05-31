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
      DataTable.GridUnload(Producto_Grilla);
    var url = baseUrl + 'Inventario/Producto/Productos_Paginado';
    var colModels = [
           {
            data: null, sortable: false,title: "Producto", autoWidth: true,
            render: function (data, type, row, meta) { return Producto_FormatterImagenProducto(data.DESC_PRODUCTO, (data.MiArchivo.CODIGO_ARCHIVO + '' + data.MiArchivo.EXTENSION)); }
           },
          { data: "ID_PRODUCTO", name: "ID_PRODUCTO", title: "ID_PRODUCTO", autoWidth: false  ,visible: false,  },
          { data: "COD_PRODUCTO", name: "COD_PRODUCTO", title: "Código Producto", autoWidth: false, width: "150px", },
          { data: "DESC_UNIDAD_MEDIDA", name: "DESC_UNIDAD_MEDIDA", title: "Unidad Medida", autoWidth: false, width: "120px" },
          {
                data: null, sortable: false, title: "Pre. Compra", width: "90px",
                render: function (data, type, row, meta) { return Producto_FormatterMoneda(data.PRECIO_COMPRA); }
          },
          {
            data: null, sortable: false, title: "Pre. Venta",  width: "80px",
            render: function (data, type, row, meta) { return Producto_FormatterMoneda(data.PRECIO_VENTA); }
           },
           {
             data: null,  sortable: false, title: "Stock", width: "60px",
             render: function (data, type, row, meta) { return Producto_StatuStock(data.STOCK, data.STOCK_MINIMO, data.ID_UNIDAD_MEDIDA,data.FLG_SERVICIO); }
           },
           {
                data: null, sortable: false, title: "Fec. Vencimiento", width: "120px",
                render: function (data, type, row, meta) { return Producto_FormatterFechaVecimiento(data.FECHA_VENCIMIENTO, data.FLG_VENCE); } 
           },
            {
                data: null, sortable: false, title: "Activo", width: "60px",
                render: function (data, type, row, meta) { return Producto_actionActivo(data.FLG_ESTADO,data.ID_PRODUCTO); }
            },
           {
             data: null, sortable: false, title: "Acciones",  width: "60px",
             render: function (data, type, row, meta) { return Producto_actionAcciones(data.ID_PRODUCTO); }
           },

    ];
    var opciones = {
        GridLocal: false, multiselect: false, sort: "desc",enumerable : true,
        eliminar: false, search: true, rowNumber: 10, rowNumbers: [10, 25, 50], rules: true, responsive: true, processing: false
    };
    DataTable.Grilla(Producto_Grilla, url, 'ID_PRODUCTO', colModels, opciones, "ID_PRODUCTO");
}


function GetRules(Ventas_Grilla) {
    var rules = new Array();
    var SearchFields = new Array();
    var ID_SUCURSAL = jQuery('#ID_SUCURSAL').val() == '' ? null : "'" + jQuery('#ID_SUCURSAL').val() + "'";

    var POR = "'%'";
    rules = []
    rules.push({ field: 'ID_SUCURSAL', data: '  ISNULL(' + ID_SUCURSAL + ',ID_SUCURSAL)', op: " = " });

    SearchFields.push({ field: 'UPPER(COD_PRODUCTO)' });
    SearchFields.push({ field: 'UPPER(DESC_PRODUCTO)' });

    var ObjectRules = {
        SearchFields: SearchFields,
        rules: rules
    }

    return ObjectRules;
}

function Producto_actionAcciones(_ID_PRODUCTO) {
    var _ID_PRODUCTO = _ID_PRODUCTO
    var  _btn_Editar = "<a class=\"dropdown-item\" onclick='Producto_MostrarEditar(" + _ID_PRODUCTO + ")'><i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;\"></i>&nbsp;  Editar</a>";
    var _btn_Eliminar = "<a class=\"dropdown-item\" onclick='Producto_Eliminar(" + _ID_PRODUCTO + ")'><i class=\"bi bi-trash-fill\" style=\"color:#e40613;\"></i>&nbsp;  Eliminar</a>";
    var _btn = "<div class=\"btn-group Group_Acciones\" role=\"group\" title=\"Acciones \" >" +
           "<button  style=\" background: transparent; border: none; color: #000000;font-size: 18px;\" type=\"button\" class=\"btn  dropdown-toggle\" data-toggle=\"dropdown\" aria-expanded=\"false\"><i class=\"bi bi-list\"></i></button>" +
           "<div class=\"dropdown-menu \" x-placement=\"bottom-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 35px, 0px);\">" +
           _btn_Editar +
           _btn_Eliminar +
            "</div>" +
        "</div>";
    return _btn;
}

function Producto_actionActivo(FLG_ESTADO, ID_PRODUCTO) {
    var check_ = 'check';
    if (FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = "<input type=\"checkbox\" id=\"Producto_chk_" + ID_PRODUCTO + "\"  data-switch=\"bool\" onchange=\"Producto_Estado(" +ID_PRODUCTO + ",this)\" " + check_ + ">"
               + " <label for=\"Producto_chk_" + ID_PRODUCTO + "\" data-on-label=\"Yes\" data-off-label=\"No\"></label>"; 
    return _btn;
}

function Producto_FormatterMoneda(PRECIO) {
    var _Precio = Number(PRECIO).toFixed(2);
    var _text = _SimboloMoneda + ' ' + _Precio;
    return _text;
}

function Producto_FormatterFechaVecimiento(FECHA_VENCIMIENTO, FLG_VENCE) {
    var _text = "";
    if (FLG_VENCE == 1 && FECHA_VENCIMIENTO != "") {
        var _FechaVencimiento = FECHA_VENCIMIENTO
        var _FechaActual = moment().format('DD/MM/YYYY');
        if ((new Date(parseInt(_FechaActual.split('/')[2]), parseInt(_FechaActual.split('/')[1]) - 1 , parseInt(_FechaActual.split('/')[0]))) >=
            (new Date(parseInt(_FechaVencimiento.split('/')[2]), parseInt(_FechaVencimiento.split('/')[1] - 1), parseInt(_FechaVencimiento.split('/')[0])))) {
            _text = "<span class=\"badge badge-danger \" data-bs-toggle=\"tooltip\" title=\"Producto vencido\">" + FECHA_VENCIMIENTO + " <i class=\"bi bi-exclamation-circle\"></i></span>";
        } else {
            var Dias = DifferenceDaysFechas(_FechaActual, _FechaVencimiento);
            
            if (Dias == 0) {
                _text = FECHA_VENCIMIENTO
            } else if (Dias <= 5) {
                _text = "<span class=\"badge badge-warning\" data-bs-toggle=\"tooltip\" title=\"Producto a punto de vencer!\">" + FECHA_VENCIMIENTO + " <i class=\"bi bi-exclamation-triangle\"></i></span>";
            } else {
                _text = FECHA_VENCIMIENTO
            }
        }
    } else {
        _text = FECHA_VENCIMIENTO; 
    }

    return _text;
}

function Producto_FormatterImagenProducto(DESC_PRODUCTO, CODIGO_IMAGE) {
    var _CodigoImage = CODIGO_IMAGE;
    var _Noimage = "no-image.png";
    var _RutaImage = baseUrl + "Recursos/ImagenProducto/";
    if (_CodigoImage != null && _CodigoImage != "") {
        _RutaImage = _RutaImage + _CodigoImage;
    } else {
        _RutaImage = _RutaImage + _Noimage;
    }
    var ImgFrame = "<div style=\"display:flex;align-items: center;\"><div style=\"background-image:url(" + _RutaImage + ");\"  class=\"img-produc\"></div>  "
                    + "<p style=\"margin-bottom: 0px;\">" + DESC_PRODUCTO + "</p></div>"
    return ImgFrame;
}

function Producto_StatuStock(STOCK, STOCK_MINIMO, ID_UNIDAD_MEDIDA, FLG_SERVICIO) {
    var _Stock = parseInt(STOCK);
    var _StockMinimo = parseInt(STOCK_MINIMO);
    var _IdUnidadMedida = parseInt(ID_UNIDAD_MEDIDA);
    var _Flg_servicio = parseInt(FLG_SERVICIO);

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

///*********************************************** ----------------- *************************************************/

///*********************************************** Actualiza  productos  ************************************************/

function Producto_Actualizar() {
    if (!_FlgServicio)
        var formvalid = "frmMantenimiento_Productos";
    else
        var formvalid = "frmMantenimiento_Servicios";

    if ($("#" + formvalid).valid()) {
        if (!_FlgServicio) {
                var STOCK_MINIMO = $("#STOCK_MINIMO").val();
                if ($("#ID_UNIDAD_MEDIDA").val() == 1) // si es kilos guardo en gramos 
                {
                    STOCK_MINIMO = ConvertKilos_Gramos(STOCK_MINIMO) // convertir a gramos 
                }
            var item =
                {
                    ID_PRODUCTO: $("#hfd_ID_PRODUCTO").val(),
                    COD_PRODUCTO: $("#COD_PRODUCTO").val(),
                    DESC_PRODUCTO: $("#DESC_PRODUCTO").val(),
                    ID_UNIDAD_MEDIDA: $("#ID_UNIDAD_MEDIDA").val(),  
                    ID_SUCURSAL: $("#hfd_ID_SUCURSAL").val(),
                    ID_CATEGORIA: $("#ID_CATEGORIA").val(),
                    PRECIO_COMPRA: parseFloat($("#PRECIO_COMPRA").val()),
                    PRECIO_VENTA: parseFloat($("#PRECIO_VENTA").val()),
                    STOCK_MINIMO: STOCK_MINIMO,
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
                            Producto_ConfigurarGrilla();
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
    if (!_FlgServicio)
        var formvalid = "frmMantenimiento_Productos";
    else
        var formvalid = "frmMantenimiento_Servicios";

    if ($('#AccionProducto').val() != 'N') {
        Producto_Actualizar();
    } else {
        if ($("#" + formvalid).valid()) {
            jConfirm("¿ Desea registrar este producto ?", "Atención", function (r) {
                if (r) {
                    if (!_FlgServicio) {

                        var STOCK = $("#STOCK").val(); 
                        var STOCK_MINIMO = $("#STOCK_MINIMO").val();
                        if ($("#ID_UNIDAD_MEDIDA").val() == 1) // si es kilos guardo en gramos 
                            {
                                STOCK = ConvertKilos_Gramos(STOCK) // convertir a gramos 
                                STOCK_MINIMO = ConvertKilos_Gramos(STOCK_MINIMO)// convertir a gramos 
                            }

                        var item =
                            {
                                COD_PRODUCTO: $("#COD_PRODUCTO").val(),
                                DESC_PRODUCTO: $("#DESC_PRODUCTO").val(),
                                ID_UNIDAD_MEDIDA: $("#ID_UNIDAD_MEDIDA").val(),
                                ID_SUCURSAL: $("#hfd_ID_SUCURSAL").val(),
                                ID_CATEGORIA: $("#ID_CATEGORIA").val(),
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
                                Producto_ConfigurarGrilla();
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
                        Producto_ConfigurarGrilla();
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



