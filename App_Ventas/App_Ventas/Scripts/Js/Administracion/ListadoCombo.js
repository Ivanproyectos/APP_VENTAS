
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


function Cliente_TipoDocumento_Listar(ID_TIPO_DOCUMENTO, _SelectInput) {

    var item = {
        FLG_ESTADO: 1,
        ID_TIPO_DOCUMENTO: _TipoDoc
    }
    var url = baseUrl + 'Administracion/Clientes/Cliente_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            var items = "<option value=\"" + "" + "\"> --Seleccione-- </option>";
            $.each(auditoria.OBJETO, function (i, v) {
                items += "<option value=\"" + v.ID_CLIENTE + "\" > " + v.NOMBRES_APE + " - " + v.NUMERO_DOCUMENTO + " </option>";
            });
            items += "</select>";
            $("#" + _SelectInput).html(items);
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}
