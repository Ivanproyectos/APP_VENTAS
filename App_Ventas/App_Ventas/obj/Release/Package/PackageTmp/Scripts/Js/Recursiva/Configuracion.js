
var _SimboloMoneda = $('#inputL_SimboloMoneda').val();
var _Impuesto = parseInt($('#inputL_Impuesto').val());
var _NombreImpuesto = $('#inputL_NombreImpuesto').val();
var _Id_Sucursal = $('#inputL_Id_Sucursal').val();

function ConfiguracionEmpresa() {
    $('#NomImpuesto').text(_NombreImpuesto);
    $('#Impuesto').text(_Impuesto);
    $('.simboloMoneda').text(_SimboloMoneda);
}