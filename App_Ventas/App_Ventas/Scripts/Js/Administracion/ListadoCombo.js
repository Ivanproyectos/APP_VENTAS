
///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  ubigeos **************************************************/

function Ubigeo_Listar(_SelectInput) {
    var item = {}
    var url = baseUrl + 'Administracion/ListadoCombo/Ubigeo_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            var items = "<option value=\"" + "" + "\"> --Seleccione-- </option>";
            $.each(auditoria.OBJETO, function (i, v) {
                items += "<option value=\"" + v.ID_UBIGEO + "\" > " + v.DESC_UBIGEO + " </option>";
            });
            items += "</select>";
            $("#" + _SelectInput).html(items);
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}
