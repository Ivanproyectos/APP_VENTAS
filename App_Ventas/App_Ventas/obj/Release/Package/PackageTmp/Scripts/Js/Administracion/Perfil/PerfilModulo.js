var Modulos = null;
 function ListarModulos() {
    var item = { };
    var url = baseUrl + 'Login/Modulos/Modulos_Listar';
     Modulos = SICA.Ajax(url, item, false);
}

 function Perfiles_CargarModulos() {
    if (Modulos != null) {
        if (!Modulos.RECHAZAR) {
            var items = ""; 
            var n_ = "";
            var Imge_ = "";
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
                            Imge_ =""; 
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
                            if(data.NIVEL == 2){
                                n_ += "&nbsp&nbsp&nbsp&nbsp&nbsp";
                            } else if (data.NIVEL == 1) {
                                Imge_ = "<i class=\"" + data.IMAGEN + "\"></i>"
                            }
                            items += n_ + "<input onchange=\"Perfiles_MarcarModulos(" + data.ID_MODULO + ",this);\" id=\"chk_" + data.ID_MODULO + "\" type=\"checkbox\" name=\"MisModulos\" value=\"" + data.ID_MODULO + "\" " + check_ + "> "
                                    + " <label class=\"control-label\" for=\"chk_" + data.ID_MODULO + "\" style=\"margin-left: 2px;text-align:left\">"+ Imge_+ " " + data.DESC_MODULO + flg_estado_ + "</label><br>";
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

 function Perfiles_MarcarModulos(ID_MODULO, MiCheck) {
     for (i_ = 0; i_ < Modulos.OBJETO.length ; i_++) {
        var data = Modulos.OBJETO[i_];    
        if (ID_MODULO == data.ID_MODULO_PADRE) {
            $("#chk_" + data.ID_MODULO).prop("checked", MiCheck.checked);
        }
    }

    var item =
    {
        ID_PERFIL:  $("#hfd_ID_PERFIL").val(),
        ID_MODULO: ID_MODULO,
        USU_CREACION: $("#input_hdcodusuario").val()
    };
    var url = "";
    if (MiCheck.checked)
        url = baseUrl + 'Login/Modulos/Modulos_ModulosPerfil_Registrar';
    else
        url = baseUrl + 'Login/Modulos/Modulos_ModulosPerfil_Eliminar';
    var auditoria = SICA.Ajax(url, item, false);
    if (auditoria != null) {
        if (auditoria.EJECUCION_PROCEDIMIENTO) {
            if (!auditoria.RECHAZAR) {
                // Sin proceso
            } else {
                jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
            }
        } else {
            jAlert(auditoria.MENSAJE_SALIDA, 'Atención');
        }
    }
}