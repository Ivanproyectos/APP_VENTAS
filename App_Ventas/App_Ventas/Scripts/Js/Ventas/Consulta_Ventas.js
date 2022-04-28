var Ventas_Grilla = 'Ventas_Grilla';
var Modulo = 'Consulta_Ventas';

function Ventas_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Ventas_Limpiar() {
    $("#Ventas_CodigoVenta").val('');
    $('#ID_TIPO_COMPROBANTE_SEARCH').val('');
    $('#Ventas_ID_TIPO_PAGO').val('');
    $('#Ventas_FLG_ANULADO').val('');
    $('#ID_USUARIO').val('').trigger('change');
    $('#ID_SUCURSAL').val('');
    $('#Ventas_FechaInicio').val('');
    $('#Ventas_FechaFin').val('');

    Ventas_ConfigurarGrilla();
}

function Ventas_ConfigurarGrilla() {
    //var url = baseUrl + 'Ventas/Ventas/Ventas_Paginado';
    //$("#" + Ventas_Grilla).GridUnload();
    //var colNames = ['Acciones', 'Código', 'ID', 'Código Venta', 'Tipo Comprobante', 'Cliente', 'Descuento', 'Subtotal', 'Igv', 'Total', 'Estado Venta', 'Tipo Pago',
    //   'Fecha Venta', 'COD_COMPROBANTE', 'Flg_anulado', 'flg_tipoventa', 'flg_credito','Nro Operacion'];
    //var colModels = [
    //        { name: 'ACCION', index: 'ACCION', align: 'center', width: 80, hidden: false, formatter: Ventas_actionAcciones, sortable: false }, // 0
    //        { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },// 1
    //        { name: 'ID_VENTA', index: 'ID_VENTA', width: 100, hidden: true, key: true }, // 2
    //        { name: 'COD_VENTA', index: 'COD_VENTA', width:100, hidden: false, align: "left" }, // 3
    //        { name: 'TIPO_COMPROBANTE', index: 'TIPO_COMPROBANTE', width: 150, hidden: false, align: "left", formatter: Ventas_FormaterComprobante }, // 4
    //        { name: 'CLIENTE', index: 'CLIENTE', width: 250, hidden: false, align: "left" }, // 5
    //        { name: 'DESCUENTO', index: 'DESCUENTO', width: 100, hidden: false, align: "left" }, // 6
    //        { name: 'SUBTOTAL', index: 'SUBTOTAL', width: 100, hidden: false, align: "left" }, // 7
    //        { name: 'IGV', index: 'IGV', width: 100, hidden: false, align: "left" }, // 8
    //        { name: 'TOTAL', index: 'TOTAL', width: 100, hidden: false, align: "left", formatter: Ventas_FormatterTotal }, // 9      
    //        { name: 'DESC_ESTADO_VENTA', index: 'DESC_ESTADO_VENTA', width: 150, hidden: false, align: "left", formatter: Ventas_Anulado }, // 10
    //        { name: 'DESC_TIPO_VENTA', index: 'DESC_TIPO_VENTA', width: 150, hidden: false, align: "left", formatter: Ventas_TipoPago }, // 11
    //        { name: 'FEC_CREACION', index: 'FEC_CREACION', width: 150, hidden: false, align: "left" },//12
    //        { name: 'COD_COMPROBANTE', index: 'COD_COMPROBANTE', width: 150, hidden: true, align: "left" },//13
    //        { name: 'FLG_ANULADO', index: 'FLG_ANULADO', width: 150, hidden: true, align: "left" },//14
    //        { name: 'ID_TIPO_PAGO', index: 'ID_TIPO_PAGO', width: 150, hidden: true, align: "left" },//15
    //        { name: 'FLG_ESTADO_CREDITO', index: 'FLG_ESTADO_CREDITO', width: 150, hidden: true, align: "left" },//16
    //        { name: 'NRO_OPERACION', index: 'NRO_OPERACION', width: 150, hidden: true, align: "left" },//17

    //];
    //var opciones = {
    //    GridLocal: false, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: true, rules: true, sort: 'desc',
    //    rowNumber: 50, rowNumbers: [50, 100, 200, 300, 500], exportar: true,
    //    exportarExcel: function (_grilla_base) {
    //        var _tituloexportacion = "Ventas Detalle.xlsx";
    //        debugger; 
    //        ExportJQGridPaginacionDataToExcel(_grilla_base, _tituloexportacion, url, 'ID_VENTA', 'desc');
    //        //ExportJQGridPaginacionDataToExcel(_grilla_base, 'Digital.xlsx', url, 'FEC_DERIVACION DESC, ID_DOCUMENTO', 'desc');
    //    }
    //};
    //SICA.Grilla(Ventas_Grilla, Ventas_Barra, Ventas_Grilla, 500, '', "Lista de Ventas", url, 'ID_VENTA', colNames, colModels, 'ID_VENTA', opciones);


    var url = baseUrl + 'Ventas/Ventas/Ventas_Paginado';
    DataTable.GridUnload(Ventas_Grilla);
    var colModels = [
          { data: "ID_VENTA", name: "ID_VENTA", title: "ID_VENTA", autoWidth: false, visible: false, },
          {
              data: null, name: "TIPO_COMPROBANTE", title: "Comprobante", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Ventas_FormaterComprobante(data.DESC_TIPO_COMPROBANTE, data.COD_COMPROBANTE); }
          },
          { data: "Cliente.NOMBRES_APE", name: "CLIENTE", title: "Cliente", autoWidth: false, width: "90px", },
          {
              data: null, name: "DESCUENTO", title: "Descuento", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Ventas_FormatterMoneda(data.DESCUENTO); }
          },
          {
              data: null, name: "SUBTOTAL", title: "Subtotal", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Ventas_FormatterMoneda(data.SUB_TOTAL); }
          },
          {
              data: null, name: "IGV", title: "Igv", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Ventas_FormatterMoneda(data.IGV); }
          },
          {
              data: null, name: "TOTAL", title: "Total", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Ventas_FormatterMoneda(data.TOTAL); }
          },
          {
              data: null, name: "DESC_ESTADO_VENTA", title: "Estado Venta", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Ventas_Anulado(data.DESC_ESTADO_VENTA, data.FLG_ANULADO); }
          },
          {
              data: null, name: "DESC_TIPO_VENTA", title: "Tipo Pago", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Ventas_TipoPago(data.DESC_TIPO_PAGO, data.ID_TIPO_PAGO, data.NRO_OPERACION); }
          },
          {
              data: null, sortable: false, title: "Acciones", width: "60px",
              render: function (data, type, row, meta) { return Ventas_actionAcciones(data.ID_VENTA, data.FLG_ANULADO, data.COD_COMPROBANTE); }
          },

    ];
    var opciones = {
        GridLocal: false, multiselect: false, sort: "desc", enumerable: true,
        eliminar: false, search: true, rowNumber: 10, rowNumbers: [10, 25, 50], rules: true, responsive: true, processing: true
    };
    DataTable.Grilla(Ventas_Grilla, url, 'ID_VENTA', colModels, opciones, "ID_VENTA");


}

function GetRules(Ventas_Grilla) {
    var rules = new Array();
    var FECHA_INICIO = jQuery('#Ventas_FechaInicio').val() == '' ? null : "'" + jQuery('#Ventas_FechaInicio').val() + "'";
    var FECHA_FIN = jQuery('#Ventas_FechaFin').val() == '' ? null : "'" + jQuery('#Ventas_FechaFin').val() + "'";
    var ID_TIPO_COMPROBANTE = jQuery('#ID_TIPO_COMPROBANTE_SEARCH').val() == '' ? null : "'" + jQuery('#ID_TIPO_COMPROBANTE_SEARCH').val() + "'";
    var ID_TIPO_PAGO = jQuery('#ID_TIPO_PAGO_SEARCH').val() == '' ? null : "'" + jQuery('#ID_TIPO_PAGO_SEARCH').val() + "'";
    var CODIGO_VENTA = "'" + jQuery('#Ventas_CodigoVenta').val() + "'";
    var _USUARIO = jQuery('#ID_USUARIO').val() == '' ? null : "'" + jQuery('#ID_USUARIO').val() + "'";
    var _ID_SUCURSAL = jQuery('#ID_SUCURSAL').val() == '' ? null : "'" + jQuery('#ID_SUCURSAL').val() + "'";
    var _FLG_ANULADO = jQuery('#Ventas_FLG_ANULADO').val() == '' ? null : "'" + jQuery('#Ventas_FLG_ANULADO').val() + "'";

    var POR = "'%'";
    rules = []
    rules.push({ field: 'UPPER(COD_COMPROBANTE)', data: POR + ' + ' + CODIGO_VENTA + ' + ' + POR, op: " LIKE " });
    rules.push({ field: 'ID_TIPO_COMPROBANTE', data: '  ISNULL(' + ID_TIPO_COMPROBANTE + ',ID_TIPO_COMPROBANTE) ', op: " = " });
    rules.push({ field: 'ID_TIPO_PAGO', data: '  ISNULL(' + ID_TIPO_PAGO + ',ID_TIPO_PAGO) ', op: " = " });
    rules.push({ field: 'USU_CREACION', data: '  ISNULL(' + _USUARIO + ',USU_CREACION) ', op: " = " });
    rules.push({ field: 'ID_SUCURSAL', data: '  ISNULL(' + _ID_SUCURSAL + ',ID_SUCURSAL) ', op: " = " });
    rules.push({ field: 'FLG_ANULADO', data: '  ISNULL(' + _FLG_ANULADO + ',FLG_ANULADO) ', op: " = " });
    rules.push({ field: 'CONVERT(DATE,FEC_CREACION,103)', data: 'CONVERT(DATE,ISNULL(' + FECHA_INICIO + ',FEC_CREACION),103)  AND CONVERT(DATE,ISNULL(' + FECHA_FIN + ',FEC_CREACION),103)  ', op: " BETWEEN " });
    rules.push({ field: 'UPPER(COD_COMPROBANTE)', data: POR + ' + ' + CODIGO_VENTA + ' + ' + POR, op: " LIKE " });

    return rules;
}


function Ventas_actionAcciones(ID_VENTA, FLG_ANULADO, COD_COMPROBANTE) {
    var _ID_VENTA = ID_VENTA;
    var _FLG_FLG_ANULADO = FLG_ANULADO;
    var _COD_COMPROBANTE = '"' + COD_COMPROBANTE + '"';
    var _btn_Anular = "";
    var _btn_Devolver = "";

    if (_FLG_FLG_ANULADO == 0) {
        _btn_Imprimir = "<a class=\"dropdown-item\" onclick='Ventas_ImprimirComprobante(" + _ID_VENTA + "," + _COD_COMPROBANTE + ")'><i class=\"bi bi-printer\" style=\"color:gray;\"></i>&nbsp;  Imprimir Comprobante</a>";
        _btn_Anular = "<a class=\"dropdown-item\" onclick='Ventas_AnularVenta(" + _ID_VENTA + ")'><i class=\"bi bi-cart-x\" style=\"color:red;\"></i>&nbsp;  Anular Venta</a>";
        _btn_Devolver = "<a class=\"dropdown-item\" onclick=\"Ventas_MostrarDevolverProducto(" + _ID_VENTA + ")\" ><i class=\"bi bi-box-arrow-in-down-left\" style=\"color:green;\"></i>&nbsp;  Devolver Producto</a>";
    }
    var _btn = "<div class=\"btn-group Group_Acciones\" role=\"group\" title=\"Acciones \" >" +
           "<button  style=\" background: transparent; border: none; color: #000000;font-size: 18px;\" type=\"button\" class=\"btn  dropdown-toggle\" data-toggle=\"dropdown\" aria-expanded=\"false\"><i class=\"bi bi-list\"></i></button>" +
           "<div class=\"dropdown-menu\" x-placement=\"bottom-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 35px, 0px);\">" +
           "<a class=\"dropdown-item\" onclick='Ventas_ViewDetalleVenta(" + _ID_VENTA + ")'><i class=\"bi bi-stickies\" style=\"color:#2c7be5\"></i>&nbsp;  Detalle Venta</a>" +
           _btn_Imprimir +
           _btn_Anular +
            _btn_Devolver +
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
        _text = "<span class=\"badge badge-success\" data-bs-toggle=\"tooltip\" title=\"Venta Realiazada\">" + _DESC_ESTADO_VENTA + "</span>";
    }
    return _text;
}

function Ventas_TipoPago(cellvalue, options, rowObject) {
    var _DESC_TIPO_VENTA = rowObject[11];
    var _ID_TIPO_PAGO = rowObject[15];
    var _NRO_OPERACION = rowObject[17];
    var _text = "";
    if (_ID_TIPO_PAGO == 2) {
        _text = "<span class=\"badge badge-warning \" data-bs-toggle=\"tooltip\" title=\"Esta venta fue es al credito.\">" + _DESC_TIPO_VENTA + "</span>";
    }
    else if (_ID_TIPO_PAGO == 1) {
        _text = _DESC_TIPO_VENTA;
    } else if (_ID_TIPO_PAGO == 3) {
        _text = '<span>' + _DESC_TIPO_VENTA + '</span><br><span style="font-size: 12px; color: #2c7be5;"><i class="bi bi-credit-card"></i>&nbsp;Nro. Operación: ' + _NRO_OPERACION + '</span>';;
    }
    return _text;
}

function Ventas_FormatterMoneda(MONTO) {
    var _TOTAL = Number(MONTO).toFixed(2);
    var _text = _SimboloMoneda + " " + _TOTAL;
    return _text;
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
    var _TIPO_DETALLE = "DEVOLVER";
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Ventas/Ventas/Mantenimiento_ViewDetalleProducto?ID_VENTA=" + ID_VENTA + "&TIPO=" + _TIPO_DETALLE, function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function Ventas_ViewDetalleVenta(ID_VENTA) {
    var _TIPO_DETALLE = "DETALLE";
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Ventas/Ventas/Mantenimiento_ViewDetalleProducto?ID_VENTA=" + ID_VENTA + "&TIPO=" + _TIPO_DETALLE, function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


