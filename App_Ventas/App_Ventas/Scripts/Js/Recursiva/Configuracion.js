
var _SimboloMoneda = $('#inputL_SimboloMoneda').val();
var _Impuesto = $('#inputL_Impuesto').val();
var _NombreImpuesto = $('#inputL_NombreImpuesto').val();

function ConfigurarFormulario() {
    $('#NomImpuesto').text(_NombreImpuesto);
    $('#Impuesto').text(_Impuesto);
    $('.simboloMoneda').text(_SimboloMoneda);
}