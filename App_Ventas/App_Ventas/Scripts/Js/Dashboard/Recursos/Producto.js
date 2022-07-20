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
          { data: "CANTIDAD", name: "CANTIDAD", title: "Cantidad", autoWidth: false, width: "80px" },
          { data: "MOVIMIENTO", name: "MOVIMIENTO", title: "Tipo", autoWidth: true },
          { data: "NUMERO_DOCUMENTO", name: "NUMERO_DOCUMENTO", title: "Detalle", autoWidth: false, },       
          { data: "FEC_CREACION", name: "FEC_CREACION", title: "Fecha Registro", autoWidth: true },
          { data: "FEC_CREACION", name: "FEC_CREACION", title: "Usuario", autoWidth: true },

    ];
    var opciones = {
        GridLocal: true, multiselect: false, sort: "desc", enumerable: false,
        eliminar: false, search: true, rowNumber: 10, rowNumbers: [10, 25, 50], rules: false, responsive: true, processing: true
    };
    DataTable.Grilla(Repo_MovimientoProducto_Grilla, '', 'ID_CLIENTE', colModels, opciones, "ID_CLIENTE");
}


///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  movimientos producto **************************************************/

function Repo_MovimientoProducto_CargarGrilla() {
    var item =
       {
           COD_USUARIO: $('#Cliente_NombreYape').val(),
           FECHA_INICIO: $('#Cliente_NumeroDocumento').val(),
           FECHA_FIN: $('#Cliente_NumeroDocumento').val(),
       };
    var url = baseUrl + 'Dashboard/Dashboard/Dashboard_ProductoMovimiento_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    DataTable.clearGridData(Clientes_Grilla);
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;
                var myData =
                 {
                     ID_MOVIMIENTO: v.ID_MOVIMIENTO,
                     MOVIMIENTO: v.MOVIMIENTO,
                     CANTIDAD: v.CANTIDAD,
                     FEC_CREACION: v.FEC_CREACION,

                 };
                DataTable.addRowData(Clientes_Grilla, myData);
            });
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
    }
}



/// translado

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

