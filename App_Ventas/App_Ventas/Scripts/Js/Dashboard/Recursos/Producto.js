var _FECHA_INICIO3 = "";
var _FECHA_FIN3 = "";

var _FECHA_INICIO4 = "";
var _FECHA_FIN4 ="";


CreateDateRange('Repo_FechaProductoTab3', function (fec1, fec2) {
    Repo_MovimientoProducto_CargarGrilla();
});

CreateDateRange('Repo_FechaProductoTab4', function (fec1, fec2) {
    //Repo_MovimientoProducto_CargarGrilla();
});


var Repo_MovimientoProducto_Grilla = 'Repo_MovimientoProducto_Grilla';
var Repo_MovimientoProducto_Grilla = 'Repo_MovimientoProducto_Grilla';

var Repo_TransladoProducto_Grilla = 'Repo_TransladoProducto_Grilla';
var Repo_TransladoProducto_Grilla = 'Repo_TransladoProducto_Grilla';

function Repo_MovimientoProducto_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Repo_MovimientoProducto_Limpiar() {
    $("#Cliente_NombreYape").val('');
    $('#Cliente_NumeroDocumento').val('');
    $('#Repo_MovimientoProducto_Estado').val(2);
    Repo_MovimientoProducto_CargarGrilla();
}

function Repo_MovimientoProducto_ConfigurarGrilla() {
    DataTable.GridUnload(Repo_MovimientoProducto_Grilla);
    var colModels = [
          { data: "ID_MOVIMIENTO", name: "ID_MOVIMIENTO", title: "ID_MOVIMIENTO", autoWidth: false, visible: false, },
          { data: "DESC_PRODUCTO", name: "DESC_PRODUCTO", title: "Producto", autoWidth: true },
          { data: "CANTIDAD", name: "CANTIDAD", title: "Cantidad", autoWidth: false, width: "15%" },
          {
              data:null, name: "MOVIMIENTO", title: "Tipo", autoWidth: true,
              render: function (data, type, row, meta) { return Repo_FormatterTipo(data.MOVIMIENTO); }
          },
          { data: "DETALLE", name: "DETALLE", title: "Detalle", autoWidth: false, width: "300px" },
          { data: "FEC_CREACION", name: "FEC_CREACION", title: "Fecha Registro", autoWidth: true,  width: "150px" },
          { data: "USU_CREACION", name: "USU_CREACION", title: "Usuario Registro", autoWidth: true ,width: "150px"  },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, sort: "desc", enumerable: false,
        eliminar: false, search: true, rowNumber: 10, rowNumbers: [10, 25, 50], rules: false, responsive: true, processing: true
    };
    DataTable.Grilla(Repo_MovimientoProducto_Grilla, '', 'ID_MOVIMIENTO', colModels, opciones, "ID_MOVIMIENTO");
}

///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  movimientos producto **************************************************/

function Repo_MovimientoProducto_CargarGrilla() {
    var item =
       {
           COD_USUARIO: $('#ID_USUARIO_INDEX3').val(),
           FECHA_INICIO: $('#Repo_FechaProductoTab3').val().split('-')[0],
           FECHA_FIN: $('#Repo_FechaProductoTab3').val().split('-')[1],
       };
    var url = baseUrl + 'Dashboard/Dashboard/Dashboard_ProductoMovimiento_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    DataTable.clearGridData(Repo_MovimientoProducto_Grilla);
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;
                var myData =
                 {
                     ID_MOVIMIENTO: v.ID_MOVIMIENTO,
                     MOVIMIENTO: v.MOVIMIENTO,
                     DESC_PRODUCTO : v.DESC_PRODUCTO, 
                     CANTIDAD: ConvertGramos_Kilostwo(v.CANTIDAD, v.ID_UNIDAD_MEDIDA) + ' ' + v.COD_UNIDAD_MEDIDA,
                     FEC_CREACION: v.FEC_CREACION,
                     USU_CREACION: v.USU_CREACION,
                     DETALLE: v.DETALLE

                 };
                DataTable.addRowData(Repo_MovimientoProducto_Grilla, myData);
            });
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
    }
}

function Repo_FormatterTipo(TIPO) {
    var text = TIPO; 
    if (TIPO == "SALIDA") {
        text = "<span class=\"text-danger\"><i class=\"bi bi-arrow-bar-down\"></i>&nbsp;" + TIPO + "</span>";
    } else if (TIPO == "INGRESO") {
        text = "<span class=\"text-success\"><i class=\"bi bi-arrow-bar-up\"></i>&nbsp;" + TIPO + "</span>";
    } else if (TIPO == "COMPRAS") {
        text = "<span class=\"text-success\"><i class=\"bi bi-basket3\"></i>&nbsp;" + TIPO + "</span>";
    }
    return text; 
}

///*********************************************** ----------------- *************************************************/

///*********************************************** Translados **************************************************/

function Repo_TransladoProducto_ConfigurarGrilla() {
    DataTable.GridUnload(Repo_TransladoProducto_Grilla);
    var colModels = [
          { data: "ID_CLIENTE", name: "ID_CLIENTE", title: "ID_TRANSLADO", autoWidth: false, visible: false, },
          { data: "NOMBRES_APE", name: "NOMBRES_APE", title: "Sucursal Origen", autoWidth: true },
          { data: "DESC_TIPO_DOCUMENTO", name: "DESC_TIPO_DOCUMENTO", title: "Sucursal Destino", autoWidth: false, width: "50px" },
          { data: "DIRECCION", name: "DIRECCION", title: "Detalle", autoWidth: true },
          { data: "NUMERO_DOCUMENTO", name: "USUARIO RES", title: "Usuario Registro", autoWidth: false, },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, sort: "desc", enumerable: false,
        eliminar: false, search: true, rowNumber: 10, rowNumbers: [10, 25, 50], rules: false, responsive: true, processing: true
    };
    DataTable.Grilla(Repo_TransladoProducto_Grilla, '', 'ID_CLIENTE', colModels, opciones, "ID_CLIENTE");
}

