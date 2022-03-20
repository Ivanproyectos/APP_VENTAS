
var Ventas_Detalle_Grilla = 'Ventas_Detalle_Grilla';
var Ventas_Detalle_Barra = 'Ventas_Detalle_Barra';
var _CODIGO_GRILLA = 0;

function Ventas_Detalle_ConfigurarGrilla(TIPO) {
    var _btnBorrarHidden = false;
    var _btnDevolverHidden = false;
    var _btnEditar = false;

    if (TIPO == "DEVOLVER") {
        _btnBorrarHidden = true;
        _btnEditar = true;
    } else if (TIPO == "DETALLE") {
         _btnBorrarHidden = true;
         _btnDevolverHidden = true;
         _btnEditar = true;
    }else if(TIPO == "VENTAS"){
         _btnBorrarHidden = false;
         _btnDevolverHidden = true;
         _btnEditar = false;
    }

    $("#" +  Ventas_Detalle_Grilla).GridUnload();
    var colNames = [ 'Eliminar','Editar','ID_DETALLE','codigo', 'ID_PRODUCTO','Producto','Unid. Medida','Precio', 'Cantidad','Importe','flg_devuelto','Devolver'];
    var colModels = [
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 70, hidden: _btnBorrarHidden, formatter: Ventas_Detalle_FormatterBorrar, sortable: false },
            { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 60, hidden: _btnEditar, formatter: Ventas_Detalle_actionEditar, sortable: false },
            { name: 'ID_VENTA_DETALLE', index: 'ID_VENTA_DETALLE', align: 'center', width: 100, hidden: true, },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true,  key: true },
            { name: 'ID_PRODUCTO', index: 'ID_PRODUCTO', align: 'center', width: 100, hidden: true },
            { name: 'PRODUCTO', index: 'PRODUCTO', align: 'left', width: 300, hidden: false },
            { name: 'COD_UNIDAD_MEDIDA', index: 'COD_UNIDAD_MEDIDA', align: 'left', width: 100, hidden: false },
            { name: 'PRECIO', index: 'PRECIO', align: 'left', width: 100, hidden: false },
            { name: 'CANTIDAD', index: 'CANTIDAD', align: 'left', width: 100, hidden: false },
            { name: 'IMPORTE', index: 'IMPORTE', align: 'left', width: 100, hidden: false },
            { name: 'FLG_DEVUELTO', index: 'FLG_DEVUELTO', align: 'left', width: 100, hidden: true },
            { name: 'DEVOLVER', index: 'DEVOLVER', align: 'center', width: 80, hidden: _btnDevolverHidden, formatter: Ventas_Detalle_FormatterDevolver, sortable: false },

    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false
    };
    SICA.Grilla(Ventas_Detalle_Grilla, Ventas_Detalle_Barra, Ventas_Detalle_Grilla, 200, '', "", '', 'CODIGO', colNames, colModels, '', opciones);
}

/* eliminar fila */
function Ventas_Detalle_FormatterBorrar(cellvalue, options, rowObject) {
       var _btn_Eliminar = "<button title='Eliminar producto'  onclick='Ventas_Detalle_Borrar(" + rowObject.CODIGO + ");' class=\"btn btn-outline-light\" type=\"button\"  style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:14px\"></i></button>"
    return _btn_Eliminar;
}

function Ventas_Detalle_actionEditar(cellvalue, options, rowObject) {
    var _btn_Editar = "<button title='Editar'  onclick='Ventas_Detalle_MostarEditarProducto(" + rowObject.CODIGO + ");' class=\"btn btn-outline-light\" type=\"button\"style=\"text-decoration: none !important; height: 35px; line-height: 3px;\" > <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:14px\"></i></button>";
    return _btn_Editar;
}


function Ventas_Detalle_MostarEditarProducto(CODIGO) {
    var ID_SUCURSAL = _Id_Sucursal;
     _CODIGO_GRILLA = CODIGO; 
    var data = jQuery("#" + Ventas_Detalle_Grilla).jqGrid('getRowData', CODIGO);
    jQuery("#myModalBuscarProduc").html('');
    jQuery("#myModalBuscarProduc").load(baseUrl + "Ventas/Ventas/Mantenimiento_BuscarProducto?ID_SUCURSAL=" + ID_SUCURSAL + "&ID_PRODUCTO=" + data.ID_PRODUCTO +
            "&PRECIO="+ data.PRECIO +"&IMPORTE="+  data.IMPORTE +"&CANTIDAD="+ data.CANTIDAD+"&Accion=M", function (responseText, textStatus, request) {
        $('#myModalBuscarProduc').modal({ show: true, backdrop: 'static', keyboard: false });
        $.validator.unobtrusive.parse('#myModalBuscarProduc');
        if (request.status != 200) return;
    });
}

    
function Ventas_Detalle_Borrar(id) {
    $("#" + Ventas_Detalle_Grilla).jqGrid('delRowData', id);
    // $("#" + grillaProductos).trigger("reloadGrid");
    Ventas_Detalle_CalcularMontoTotalDetalle();
    Ventas_Detalle_UpdateRowId();
}

// actualizar rowid despues de eliminar un pro
function Ventas_Detalle_UpdateRowId() {
    var ListaDetalleProductos = $("#" + Ventas_Detalle_Grilla).jqGrid('getGridParam', 'data');
    for (var i = 0; i < ListaDetalleProductos.length; i++) {
        var resetId = i + 1;
        var rowData = ListaDetalleProductos[i];
        $("#" + Ventas_Detalle_Grilla).jqGrid('setCell', rowData.CODIGO, 'CODIGO', resetId);
    }

}

function Ventas_Detalle_BuscarProducto_Grilla(ID_PRODUCTO) {
    debugger;
    var buscado = false;
    var ids = $("#" + Ventas_Detalle_Grilla).getDataIDs();
    for (var i = 0; i < ids.length; i++) {
        var rowId = ids[i];
        var rowData = $("#" + Ventas_Detalle_Grilla).jqGrid('getRowData', rowId);
        if (rowData.ID_PRODUCTO == ID_PRODUCTO) return true;
    }
    return buscado;
}


function Ventas_Detalle_CalcularMontoTotalDetalle() {
    var ids = $("#" + Ventas_Detalle_Grilla).getDataIDs();
    var _subtotal = 0;
    var _descuento = isNaN(parseFloat($('#DESCUENTO').val())) ? 0 : parseFloat($('#DESCUENTO').val());
    var _Adelanto = isNaN(parseFloat($('#ADELANTO').val())) ? 0 : parseFloat($('#ADELANTO').val());
    var _Igv = 0; 
    var _Total = 0;
    var _TotalDebe = 0; 
    for (var i = 0; i < ids.length; i++) {
        var rowId = ids[i];
        var rowData = $("#" + Ventas_Detalle_Grilla).jqGrid('getRowData', rowId);
        _Total += parseFloat(rowData.IMPORTE)
    }
    if (_descuento < _Total)
        _Total = (_Total - _descuento);
    else {
        jError('El descuento no puede ser mayor al total.', 'Atención');
        _descuento = 0.0; 
    }

    _Igv = Math.floor(_Total * _Impuesto) / 100;
    _subtotal = _Total - _Igv;
    _TotalDebe = (_Total - _Adelanto);

    $('#Venta_Descuento').text(Number(_descuento).toFixed(2));
    $('#Venta_Igv').text( Number(_Igv).toFixed(2));
    $('#Venta_Subtotal').text( Number(_subtotal).toFixed(2));
    $('#Venta_Total').text(Number(_Total).toFixed(2));

    $('#Venta_TotalDebe').text(Number(_TotalDebe).toFixed(2));
}


function Ventas_Detalle_FormatterDevolver(cellvalue, options, rowObject) {
    var _FLG_DEVUELTO = rowObject.FLG_DEVUELTO
    if (_FLG_DEVUELTO == 0) {
        var _btn_devolver = "<button title='Devolver producto'  onclick='Ventas_DevolverProducto(" + rowObject.CODIGO + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-box-arrow-in-down-left\" style=\"color:green;font-size:17px\"></i></button>"
    } else {
        var _btn_devolver = "<button title='Este producto ya fue devuelto.'  class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-box-arrow-in-down-left\" style=\"color:gray;font-size:17px\"></i></button>"
    }
    return _btn_devolver;
}



function Ventas_Detalle_CargarGrilla(ID_VENTA) {
    var item =
       {
           ID_VENTA: ID_VENTA,
       };
    var url = baseUrl + 'Ventas/Ventas/ventas_Detalle_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    jQuery("#" + Ventas_Detalle_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;
                var myData =
                 {
                     CODIGO: idgrilla,
                     ID_VENTA_DETALLE : v.ID_VENTA_DETALLE,
                     ID_PRODUCTO: v.ID_PRODUCTO,
                     PRODUCTO: v.DESC_PRODUCTO,
                     PRECIO: Number(v.PRECIO).toFixed(2), 
                     CANTIDAD: v.CANTIDAD,
                     IMPORTE:  Number(v.IMPORTE).toFixed(2), 
                     FLG_DEVUELTO: v.FLG_DEVUELTO,
                     COD_UNIDAD_MEDIDA: v.COD_UNIDAD_MEDIDA,
                     ACCION : "M"
                 };
                jQuery("#" + Ventas_Detalle_Grilla).jqGrid('addRowData', idgrilla, myData);
            });
            jQuery("#" + Ventas_Detalle_Grilla).trigger("reloadGrid");
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}


function Ventas_BuscarProducto(COD_PRODUCTO, ID_SUCURSAL) {
    var item =
       {
           COD_PRODUCTO: COD_PRODUCTO,
           ID_SUCURSAL: ID_SUCURSAL

       };
    var url = baseUrl + 'Ventas/Ventas/Producto_Buscar_Listar';
    var Lista = SICA.Ajax(url, item, false);
    if (Lista.length < 2) {
        $.each(Lista, function (i, v) {
            $('#_DetallePorductos').show('slow');
            $("#SEARCH_PRODUCTO").autocomplete("disable"); // DESACTIVA AUTOCOMPLETE
            $("#SEARCH_PRODUCTO").val(v.DESC_PRODUCTO);
            $("#ID_UNIDAD_MEDIDA").val(v.ID_UNIDAD_MEDIDA);
            $("#HDF_COD_UNIDAD_MEDIDA").val(ui.item.COD_UNIDAD_MEDIDA);
            $("#COD_PRODUCTO").val(v.COD_PRODUCTO);
            $("#INPUT_STOCK").text(v.STOCK);
            $("#PRECIO_VENTA").val(Number(v.PRECIO_VENTA).toFixed(2));
            $("#DETALLE").val(v.DETALLE);
            $("#hfd_STOCK").val(v.STOCK);
            $('#TOTAL').val(Number(v.PRECIO_VENTA).toFixed(2));
            $('#hfd_ID_PRODUCTO').val(v.ID_PRODUCTO);
            $('#CANTIDAD').val(1);
            $('#CANTIDAD').focus();
            setTimeout(function () { $("#SEARCH_PRODUCTO").autocomplete("enable") }, 800); // ACTIVA AUTOCOMPLETE
            _Valido = true;
        });
    } else {
        jError("Se encontro mas de un producto con este codigo, verifique que el producto no tenga codigos duplicados.", "Atención");
    }
}
/*surcut enter*/
function Ventas_Detalle_Insertar() {
    if (_Valido) {
        if ($('#frmMantenimiento_BuscarProducto').valid()) {
            if (_Accion == "N") { // nuevo  producto al detalle 
                var rowKey = jQuery("#" + Ventas_Detalle_Grilla).getDataIDs();
                var ix = rowKey.length;
                ix++;
                var myData =
                      {
                          ID_VENTA_DETALLE: 0,
                          CODIGO: ix,
                          ID_PRODUCTO: $("#hfd_ID_PRODUCTO").val(),
                          PRODUCTO: $("#SEARCH_PRODUCTO").val(),
                          PRECIO: Number($("#PRECIO_VENTA").val()).toFixed(2),
                          CANTIDAD: $("#CANTIDAD").val(),
                          IMPORTE: Number($("#TOTAL").val()).toFixed(2), 
                          COD_UNIDAD_MEDIDA: $("#HDF_COD_UNIDAD_MEDIDA").val(),
                      };

                if (Ventas_Detalle_BuscarProducto_Grilla($('#hfd_ID_PRODUCTO').val())) {
                    jError('Producto seleccionado ya se encuentra en la lista.', 'Atención');
                } else {
                    jQuery("#" + Ventas_Detalle_Grilla).jqGrid('addRowData', ix, myData);
                    Ventas_Detalle_CalcularMontoTotalDetalle();
                }
            } else {  // actualizar  producto al detalle  
                debugger; 
                var _IdGrilla = _CODIGO_GRILLA;
                $("#" + Ventas_Detalle_Grilla).jqGrid('setCell', _IdGrilla, 'PRECIO', Number($("#PRECIO_VENTA").val()).toFixed(2));
                $("#" + Ventas_Detalle_Grilla).jqGrid('setCell', _IdGrilla, 'CANTIDAD', $("#CANTIDAD").val());
                $("#" + Ventas_Detalle_Grilla).jqGrid('setCell', _IdGrilla, 'IMPORTE', Number($("#TOTAL").val()).toFixed(2));
                Ventas_Detalle_CalcularMontoTotalDetalle();
            }
            LimpiarFormulario();
            $('#_DetallePorductos').hide('');
            $('#SEARCH_PRODUCTO').val('');
            $('#SEARCH_PRODUCTO').focus();
        }
    } else {
        jError('Debe buscar un producto para ingresar a la lista. No seas imbecil', 'Atención');
    }

}


function Ventas_BuscarProductoxId(ID_PRODUCTO) {
    var item =
       {
           ID_PRODUCTO: ID_PRODUCTO,

       };
    var url = baseUrl + 'Inventario/Producto/Producto_ListarxId';
    var Lista = SICA.Ajax(url, item, false);
    if (Lista != null) {
        $.each(Lista, function (i, v) {
            $('#_DetallePorductos').show('slow');
            $("#SEARCH_PRODUCTO").autocomplete("disable"); // DESACTIVA AUTOCOMPLETE
            $("#SEARCH_PRODUCTO").val(v.DESC_PRODUCTO);
            $("#ID_UNIDAD_MEDIDA").val(v.ID_UNIDAD_MEDIDA);
            $("#COD_PRODUCTO").val(v.COD_PRODUCTO);
            $("#INPUT_STOCK").text(v.STOCK);
            $("#PRECIO_VENTA").val(Number(v.PRECIO_VENTA).toFixed(2));
            $("#DETALLE").val(v.DETALLE);
            $("#hfd_STOCK").val(v.STOCK);
            $('#TOTAL').val(Number(v.PRECIO_VENTA).toFixed(2));
            $('#hfd_ID_PRODUCTO').val(v.ID_PRODUCTO);
            $('#CANTIDAD').val(1);
            $('#CANTIDAD').focus();
            setTimeout(function () { $("#SEARCH_PRODUCTO").autocomplete("enable") }, 800); // ACTIVA AUTOCOMPLETE
            _Valido = true;
        });
    } else {
        jError("Se encontro mas de un producto con este codigo, verifique que el producto no tenga codigos duplicados.", "Atención");
    }
}