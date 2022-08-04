$('#Reporte_btn_VentasBuscar').click(function () {
    Repo_Ventas_Cargar();
    Repo_Compras_Cargar(); 
});

$('#Repo_Ventas_ExportExcel').click(function () {
    var DESC_SUCURSAL =""; 
    var ID_SUCURSAL = $('#ID_SUCURSAL_INDEX2').val(); 
    if (ID_SUCURSAL != 0) {
        DESC_SUCURSAL = $("#ID_SUCURSAL_INDEX2 option:selected").text();
    } else {
        DESC_SUCURSAL = "TODOS"; 
    }
    var params = {
        FECHA_INICIO: $('#Repo_FechaVentaTab2').val().split('-')[0],
        FECHA_FIN: $('#Repo_FechaVentaTab2').val().split('-')[1],
        ID_SUCURSAL: $('#ID_SUCURSAL_INDEX2').val(),
        DESC_SUCURSAL: DESC_SUCURSAL,
        TIPO: 1// ventas
    }
    DowloandFileAspx(baseUrl + "Recursos/Descargas/DescargarVentasResumenExcel.aspx?" + $.param(params))
});

$('#Repo_Compras_ExportExcel').click(function () {
    var DESC_SUCURSAL = "";
    var ID_SUCURSAL = $('#ID_SUCURSAL_INDEX2').val();
    if (ID_SUCURSAL != 0) {
        DESC_SUCURSAL = $("#ID_SUCURSAL_INDEX2 option:selected").text();
    } else {
        DESC_SUCURSAL = "TODOS";
    }
    var params = {
        FECHA_INICIO: $('#Repo_FechaVentaTab2').val().split('-')[0],
        FECHA_FIN: $('#Repo_FechaVentaTab2').val().split('-')[1],
        ID_SUCURSAL: $('#ID_SUCURSAL_INDEX2').val(),
        DESC_SUCURSAL: DESC_SUCURSAL,
        TIPO: 2// compras
    }
    DowloandFileAspx(baseUrl + "Recursos/Descargas/DescargarVentasResumenExcel.aspx?" + $.param(params))
});


function Repo_Ventas_Cargar() {
    var item =
    {
        FECHA_INICIO: $('#Repo_FechaVentaTab2').val().split('-')[0],
        FECHA_FIN: $('#Repo_FechaVentaTab2').val().split('-')[1],
        ID_SUCURSAL: $('#ID_SUCURSAL_INDEX2').val(),
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

function Repo_Compras_Cargar() {
    var item =
    {
        FECHA_INICIO: $('#Repo_FechaVentaTab2').val().split('-')[0],
        FECHA_FIN: $('#Repo_FechaVentaTab2').val().split('-')[1],
        ID_SUCURAL: $('#ID_SUCURSAL_INDEX2').val(),
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
    CargarGrafico_ComprasDia(Model, Data);
}