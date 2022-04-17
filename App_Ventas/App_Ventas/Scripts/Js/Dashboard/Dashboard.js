
///*********************************************** ----------------- *************************************************/

///*********************************************** Lista count dash **************************************************/

function Dashboard_Cargar() {
    var item =
       {
           ID_SUCURSAL: $('#ID_SUCURSAL').val(),
           ANIO: $('#ID_ANIO').val(),
       };
    var url = baseUrl + 'Dashboard/Dashboard/Dashboard_Count_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
                var options = {
                    decimalPlaces: 2
                }
                var VentaTotal = new countUp.CountUp('LblVentasTotal', parseFloat(auditoria.OBJETO.MONTO_TOTAL_VENTAS), options);
                VentaTotal.start();
                var Devolucion = new countUp.CountUp('LblDevolucion', auditoria.OBJETO.TOTAL_DEVOLUCIONES, null);
                Devolucion.start();
                var Ventas = new countUp.CountUp('LblVentas', auditoria.OBJETO.TOTAL_VENTAS, null);
                Ventas.start();
                var Compras = new countUp.CountUp('LblCompras', auditoria.OBJETO.TOTAL_COMPRAS, null);
                Compras.start();
                Dashboard_Configurar_GraficoVentaMeses(auditoria.OBJETO.Lista_VentaMes);
                Dashboard_Configurar_GraficoComparativa(auditoria.OBJETO.Lista_Comparativa);
                Dashboard_Configurar_GraficoTipoPago(auditoria.OBJETO.Lista_TipoPago);
                Dashboard_Configurar_GraficoProductoMv(auditoria.OBJETO.Lista_ProductosMV);
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}


function Dashboard_Configurar_GraficoVentaMeses(_List) {
    var Model = ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Set", "Oct", "Nov", "Dic"];
    var Data = []; 
    var _Encontro = false; 
    for (var i = 1; i <= Model.length ; i++) {
        for (var j = 0; j < _List.length ; j++) {
            if (_List[j].NUMERO_MES == i) {
                _Encontro = true;
                Data.push(Number(_List[j].TOTAL).toFixed(2));
            }
        }
        if (!_Encontro) {
            Data.push(0);
        } 
        _Encontro = false; 
    }
    CargarGrafico_VentasMes(Model, Data);
}

function Dashboard_Configurar_GraficoComparativa(_List) {
    var Model = ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Set", "Oct", "Nov", "Dic"];
    var Data_Line1 = [];
    var Data_Line2 = [];
    var _Encontro_Lin1 = false;
    var _Encontro_Lin2 = false;

    for (var i = 1; i <= Model.length ; i++) {
        for (var j = 0; j < _List.length ; j++) {
            if (_List[j].NUMERO_MES == i && _List[j].TIPO == "PRODUCTO") {
                _Encontro_Lin1 = true;
                Data_Line1.push(Number(_List[j].TOTAL).toFixed(2));
            }
            if (_List[j].NUMERO_MES == i && _List[j].TIPO == "SERVICIO") {
                _Encontro_Lin2 = true;
                Data_Line2.push(Number(_List[j].TOTAL).toFixed(2));
            }
        }
        if (!_Encontro_Lin1) {
            Data_Line1.push(0);
        }
        if (!_Encontro_Lin2) {
            Data_Line2.push(0);
        }

        _Encontro_Lin1 = false;
        _Encontro_Lin2 = false; 
    }
    CargarGrafico_ComparativaLine(Model, Data_Line1, Data_Line2);
}

function Dashboard_Configurar_GraficoTipoPago(_List) {
    var Model = [];
    var Data = [];
    for (var i = 0; i < _List.length ; i++) {
        Model.push(_List[i].TIPO);
        Data.push(_List[i].PORCENTAJE);
        }
    CargarGrafico_TipoPago(Model, Data);
}

function Dashboard_Configurar_GraficoProductoMv(_List) {
    var Model = [];
    var Data = [];
    for (var i = 0; i < _List.length ; i++) {
        Model.push(_List[i].PRODUCTO);
        Data.push(_List[i].CANTIDAD);
    }
    CargarGrafico_ProductoMv(Model, Data);
}