
var Translados_Detalle_Grilla = 'Translados_Detalle_Grilla';
var Translados_Detalle_Barra = 'Translados_Detalle_Barra';
var _CODIGO_GRILLA = 0;

function Translados_Detalle_ConfigurarGrilla() {
    $("#" + Translados_Detalle_Grilla).GridUnload();
    var colNames = ['Eliminar', 'Editar', 'codigo', 'ID_PRODUCTO', 'Producto', 'Unid. Medida', 'Cantidad','id_unidadMedida'];
    var colModels = [
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 70, hidden: false, formatter: Translados_Detalle_FormatterBorrar, sortable: false },
            { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 60, hidden: false, formatter: Translados_Detalle_actionEditar, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, key: true },
            { name: 'ID_PRODUCTO', index: 'ID_PRODUCTO', align: 'center', width: 100, hidden: true },
            { name: 'PRODUCTO', index: 'PRODUCTO', align: 'left', width: 300, hidden: false },
            { name: 'COD_UNIDAD_MEDIDA', index: 'COD_UNIDAD_MEDIDA', align: 'left', width: 100, hidden: false },
            { name: 'CANTIDAD', index: 'CANTIDAD', align: 'left', width: 100, hidden: false },
            { name: 'ID_UNIDAD_MEDIDA', index: 'ID_UNIDAD_MEDIDA', align: 'left', width: 100, hidden: true },

    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, rowNumber: 50, rowNumbers: [50, 100, 200, 300, 500],
    };
    SICA.Grilla(Translados_Detalle_Grilla, Translados_Detalle_Barra, Translados_Detalle_Grilla, 200, '', "", '', 'CODIGO', colNames, colModels, '', opciones);
}


function Translados_Detalle_FormatterBorrar(cellvalue, options, rowObject) {
    var _btn_Eliminar = "<button title='Eliminar producto'  onclick='Translados_Detalle_Borrar(" + rowObject.CODIGO + ");' class=\"btn btn-outline-light\" type=\"button\"  style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:14px\"></i></button>"
    return _btn_Eliminar;
}

function Translados_Detalle_Borrar(id) {
    $("#" + Translados_Detalle_Grilla).jqGrid('delRowData', id);
    Translados_Detalle_UpdateRowId();
}

function Translados_Detalle_UpdateRowId() {
    var ListaDetalleProductos = $("#" + Translados_Detalle_Grilla).jqGrid('getGridParam', 'data');
    for (var i = 0; i < ListaDetalleProductos.length; i++) {
        var resetId = i + 1;
        var rowData = ListaDetalleProductos[i];
        $("#" + Translados_Detalle_Grilla).jqGrid('setCell', rowData.CODIGO, 'CODIGO', resetId);
    }

}

function Translados_Detalle_actionEditar(cellvalue, options, rowObject) {
    var _btn_Editar = "<button title='Editar'  onclick='Translados_Detalle_MostarEditarProducto(" + rowObject.CODIGO + ");' class=\"btn btn-outline-light\" type=\"button\"style=\"text-decoration: none !important; height: 35px; line-height: 3px;\" > <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:14px\"></i></button>";
    return _btn_Editar;
}

function Translados_Detalle_MostarEditarProducto(CODIGO) {
        var Url_ = "Inventario/Producto/View_BuscarProducto";
          _CODIGO_GRILLA = CODIGO;
    var data = jQuery("#" + Translados_Detalle_Grilla).jqGrid('getRowData', CODIGO);
    jQuery("#myModalBuscarProduc").html('');
    jQuery("#myModalBuscarProduc").load(baseUrl + Url_ + "?ID_PRODUCTO=" + data.ID_PRODUCTO + "&_CANTIDAD=" + data.CANTIDAD + "&Accion=M", function (responseText, textStatus, request) {
                $('#myModalBuscarProduc').modal({ show: true, backdrop: 'static', keyboard: false });
                $.validator.unobtrusive.parse('#myModalBuscarProduc');
                if (request.status != 200) return;
            });
}


function Translados_ViewBuscarProducto() {
    jQuery("#myModalBuscarProduc").html('');
    jQuery("#myModalBuscarProduc").load(baseUrl + "Inventario/Producto/View_BuscarProducto?ID_PRODUCTO=0&_CANTIDAD=0&Accion=N",
        function (responseText, textStatus, request) {
            $('#myModalBuscarProduc').modal({ show: true, backdrop: 'static', keyboard: false });
            $.validator.unobtrusive.parse('#myModalBuscarProduc');
            if (request.status != 200) return;
        });
}

function Translados_Detalle_Insertar() {
    if (_Valido) {
        if ($('#frmMantenimiento_BuscarProducto').valid()) {
            var _ID_UNIDAD_MEDIDA = $('#ID_UNIDAD_MEDIDA').val();
            var _CANTIDAD = $("#CANTIDAD").val();
     
            if (_Accion == "N") { // nuevo  producto al detalle 
                var rowKey = jQuery("#" + Translados_Detalle_Grilla).getDataIDs();
                var ix = rowKey.length;
                ix++;
                var myData =
                      {
                          ID_VENTA_DETALLE: 0,
                          CODIGO: ix,
                          ID_PRODUCTO: $("#hfd_ID_PRODUCTO").val(),
                          PRODUCTO: $("#SEARCH_PRODUCTO").val(),
                          CANTIDAD: _CANTIDAD,
                          COD_UNIDAD_MEDIDA: $("#_Info_codigoUnidad").text(),
                          ID_UNIDAD_MEDIDA: $("#ID_UNIDAD_MEDIDA").val(),
                      };

                if (Translados_Detalle_BuscarProducto_Grilla($('#hfd_ID_PRODUCTO').val())) {
                    jError('Producto seleccionado ya se encuentra en la lista.', 'Atención');
                } else {
                    jQuery("#" + Translados_Detalle_Grilla).jqGrid('addRowData', ix, myData);
                }
            } else {  // actualizar  producto al detalle  
                var _IdGrilla = _CODIGO_GRILLA;
                $("#" + Translados_Detalle_Grilla).jqGrid('setCell', _IdGrilla, 'CANTIDAD', _CANTIDAD);
            }
            LimpiarFormulario();
            $('#_DetallePorductos').hide('');
            $('#SEARCH_PRODUCTO').val('');
            $('#SEARCH_PRODUCTO').focus();
        }
    } else {
        jError('Debe buscar un producto para ingresar a la lista', 'Atención');
    }

}


function Translados_Detalle_BuscarProducto_Grilla(ID_PRODUCTO) {
    var buscado = false;
    var ids = $("#" + Translados_Detalle_Grilla).getDataIDs();
    for (var i = 0; i < ids.length; i++) {
        var rowId = ids[i];
        var rowData = $("#" + Translados_Detalle_Grilla).jqGrid('getRowData', rowId);
        if (rowData.ID_PRODUCTO == ID_PRODUCTO) return true;
    }
    return buscado;
}