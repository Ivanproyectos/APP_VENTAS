function Perfiles_CargarModulos() {
    var url = baseUrl + 'Login/Modulos/Modulos_Listar';
    var Modulos = SICA.Ajax(url, item, false);
    var items = "";
    if (Modulos != null) {
        if (!Modulos.RECHAZAR) {
            var n_ = "";
            var item =
            {
                ID_PERFIL: $("#hfd_ID_PERFIL").val()
            };
            var url = baseUrl + 'Login/Modulos/Modulos_ModulosPerfil_Listar';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null) {
                if (!auditoria.EJECUCION_PROCEDIMIENTO) {
                    jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
                }
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        for (i_ = 0; i_ < Modulos.OBJETO.length ; i_++) {
                            var data = Modulos.OBJETO[i_]; 
                            n_ = "";
                            var check_ = "";
                            var flg_estado_ = "";
                            auditoria.OBJETO.map(function (x) {
                                if (x.ID_MODULO == data.ID_MODULO) {
                                    check_ = "checked";

                                    if (x.FLG_ESTADO == "0") {
                                        flg_estado_ = " <a style=\"text-decoration: none;\">(Desactivado)</a>";
                                    }
                                }
                            });
                            for (ix = 1; ix < data.NIVEL; ix++) {
                                n_ += "&nbsp&nbsp&nbsp";
                            }
                            items += n_ + "<input onchange=\"Perfiles_MarcarModulos(" + data.ID_MODULO + ",this);\" id=\"chk_" + data.ID_MODULO + "\" type=\"checkbox\" name=\"MisModulos\" value=\"" + data.ID_MODULO + "\" " + check_ + "> "
                                    + " <label class=\"control-label\" for=\"chk_" + data.ID_MODULO + "\" style=\"margin-left: 2px;text-align:left\">" + data.DESC_MODULO + flg_estado_ + "</label><br>";
                        }
                    } else {
                        jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
                    }
                } else {
                    items = "<div class=\"col-sm-12\" style=\"text-align: center\">";
                    items += "<span style=\"color: #999999; font-size: 11px;\"><i class=\"clip-close\"></i>&nbsp Sin registros que mostrar</span>";
                    items += "</div>";
                }
            }
            $("#Perfiles_Div_Modulos").html(items);
        }
    }

}