
//CreateDateRange('Repo_FechaVentaTab2', function (fec1, fec2) {
//    Repo_Ventas_CargarGrilla();
//});

$('#Repo_FechaVentaTab2').daterangepicker({
    opens: 'right',
    ranges: {
        'Hoy': [moment(), moment()],
        'Ayer': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
        'Los últimos 7 días': [moment().subtract(6, 'days'), moment()],
        'Los últimos 30 días': [moment().subtract(29, 'days'), moment()],
        'Este mes': [moment().startOf('month'), moment().endOf('month')],
        'El mes pasado': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
    },
    locale: {
        format: "DD/MM/YYYY",
        applyLabel: "Aplicar",
        cancelLabel: "Cancelar",
        customRangeLabel: "Rango Personalizado",
    }
}, function (start, end, label) {
    Repo_Ventas_CargarGrilla();
});

//var Repo_Ventas_Grilla = 'Repo_Ventas_Grilla';

//function Repo_Ventas_ConfigurarGrilla() {
//    DataTable.GridUnload(Repo_Ventas_Grilla);
//    var colModels = [
//        { data: "STR_FEC_CREACION", name: "STR_FEC_CREACION", title: "FECHA", autoWidth: true },
//        { data: "TOTAL", name: "TOTAL", title: "TOTAL", autoWidth: true },
//    ];
//    var opciones = {
//        GridLocal: true, multiselect: false, sort: "desc", enumerable: false,
//        eliminar: false, search: true, rowNumber: 10, rowNumbers: [10, 25, 50], rules: false, responsive: true, processing: true
//    };
//    DataTable.Grilla(Repo_Ventas_Grilla, '', 'STR_FEC_CREACION', colModels, opciones, "STR_FEC_CREACION");
//}

function Repo_Ventas_CargarGrilla() {
    var item =
    {
        FECHA_INICIO: $('#Repo_FechaVentaTab2').val().split('-')[0],
        FECHA_FIN: $('#Repo_FechaVentaTab2').val().split('-')[1],
    };
    var url = baseUrl + 'Dashboard/Dashboard/Dashboard_Venta_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            Dashboard_Configurar_VentasDia(auditoria.OBJETO);
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
    }
}

function Repo_Compras_CargarGrilla() {
    var item =
    {
        FECHA_INICIO: $('#Repo_FechaVentaTab2').val().split('-')[0],
        FECHA_FIN: $('#Repo_FechaVentaTab2').val().split('-')[1],
    };
    var url = baseUrl + 'Dashboard/Dashboard/Dashboard_Compras_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            Dashboard_Configurar_ComprasDia(auditoria.OBJETO);
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
    }
}

function Dashboard_Configurar_VentasDia(_List) {
    var Model = [];
    var Data = [];
    for (var i = 0; i < _List.length; i++) {
        Model.push(_List[i].STR_FEC_CREACION);
        Data.push(_List[i].TOTAL);
    }
    CargarGrafico_VentasDia(Model, Data);
}

function Dashboard_Configurar_ComprasDia(_List) {
    var Model = [];
    var Data = [];
    for (var i = 0; i < _List.length; i++) {
        Model.push(_List[i].STR_FEC_CREACION);
        Data.push(_List[i].TOTAL);
    }
    CargarGrafico_VentasDia(Model, Data);
}