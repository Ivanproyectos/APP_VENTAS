var Usuarios_Grilla = 'Usuarios_Grilla';
var Usuarios_Barra = 'Usuarios_Barra';


function Usuarios_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Usuarios_Limpiar() {
    $("#Usuarios_NombresApe").val('');
    $("#Usuarios_Documento").val('');
    $('#Usuarios_Estado').val(2);
    Usuarios_CargarGrilla();
}

function Usuarios_ConfigurarGrilla() {
    DataTable.GridUnload(Usuarios_Grilla);
    var colModels = [
           {
               data: null, sortable: true, title: "Nombre y Apellidos", autoWidth: true,
               render: function (data, type, row, meta) { return Usuario_TextUsuario(data.NOMBRES_APE , data.COD_USUARIO); }
           },
          { data: "CLAVE_USUARIO", name: "CLAVE_USUARIO", title: "Clave Usuario", autoWidth: true },
          { data: "ES_JEFE", name: "ES_JEFE", title: "Es Jefe", autoWidth: false, width: "90px", },
          { data: "ID_USUARIO", name: "ID_USUARIO", title: "ID_PRODUCTO", autoWidth: false, visible: false, },
          { data: "DESC_TIPO_DOCUMENTO", name: "DESC_TIPO_DOCUMENTO", title: "Tipo Documento", autoWidth: true, },
          { data: "DNI", name: "DNI", title: "Número Documento",  autoWidth: true },
          { data: "CELULAR", name: "CELULAR", title: "Celular",  autoWidth: true },
          { data: "TELEFONO", name: "TELEFONO", title: "Telefono",  autoWidth: true },
          { data: "CORREO", name: "CORREO", title: "Correo",  autoWidth: true },
          {
              data: null, name: "FLG_ESTADO", title: "Activo", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Usuarios_actionActivo(data.FLG_ESTADO, data.ID_USUARIO); }
          },
          {
            data: null, sortable: false, title: "Acciones", width: "60px",
            render: function (data, type, row, meta) { return Usuarios_actionAcciones(data.ID_USUARIO); }
          },

    ];
    var opciones = {
        GridLocal: true, multiselect: false, sort: "desc", enumerable: false,
        eliminar: false, search: true, rowNumber: 10, rowNumbers: [10, 25, 50], rules: false, responsive: true, processing: true
    };
    DataTable.Grilla(Usuarios_Grilla, '', 'ID_USUARIO', colModels, opciones, "ID_USUARIO");

}

function Usuarios_actionAcciones(ID_USUARIO) {
    var _btn_Editar = "<a class=\"dropdown-item\" onclick='Usuarios_MostrarEditar(" + ID_USUARIO + ")'><i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;\"></i>&nbsp;  Editar</a>";
    var _btn_Eliminar = "<a class=\"dropdown-item\" onclick='Usuarios_Eliminar(" + ID_USUARIO + ")'><i class=\"bi bi-trash-fill\" style=\"color:#e40613;\"></i>&nbsp;  Eliminar</a>";
    var _btn = "<div class=\"btn-group Group_Acciones\" role=\"group\" title=\"Acciones \" >" +
           "<button  style=\" background: transparent; border: none; color: #000000;font-size: 18px;\" type=\"button\" class=\"btn  dropdown-toggle\" data-toggle=\"dropdown\" aria-expanded=\"false\"><i class=\"bi bi-list\"></i></button>" +
           "<div class=\"dropdown-menu\" x-placement=\"bottom-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 35px, 0px);\">" +
           _btn_Editar +
           _btn_Eliminar +
            "</div>" +
        "</div>";
    return _btn;
}

function Usuarios_actionActivo(FLG_ESTADO, ID_USUARIO) {
    var check_ = 'check';
    if (FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = "<input type=\"checkbox\" id=\"Usuarios_chk_" + ID_USUARIO + "\"  data-switch=\"state\" onchange=\"Usuarios_Estado(" + ID_USUARIO + ",this)\" " + check_ + ">"
              + " <label for=\"Usuarios_chk_" + ID_USUARIO + "\" data-on-label=\"Yes\" data-off-label=\"No\"></label>";
    return _btn;
}

function Usuario_TextUsuario(NOMBRES_APE, COD_USUARIO) {
    var Usuario = NOMBRES_APE;
    var Cod_usuario = COD_USUARIO;
    var _text = '<span>' + Usuario + '</span><br><span style="font-size: 12px; color: #2c7be5;"><i class="bi bi-person"></i>&nbsp;Usuario: ' + Cod_usuario + '</span>';

    return _text
}


function Usuarios_MostrarNuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Usuarios/Mantenimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function Usuarios_MostrarEditar(ID_USUARIO) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Usuarios/Mantenimiento?id=" + ID_USUARIO + "&Accion=M", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  cargo **************************************************/

function Usuarios_CargarGrilla() {
    var item =
       {
           //NOMBRES_APE: $('#Usuarios_NombresApe').val(),
           //DNI: $('#Usuarios_Documento').val(),
           FLG_ESTADO: 2
       };
    var url = baseUrl + 'Administracion/Usuarios/Usuario_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    DataTable.clearGridData(Usuarios_Grilla);
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;

                var myData =
                 {
                     FILA: idgrilla,
                     ID_USUARIO: v.ID_USUARIO,
                     ES_JEFE : v.FLG_ADMIN == 1? 'SI' : 'NO',
                     NOMBRES_APE: v.NOMBRES_APE,
                     DESC_TIPO_DOCUMENTO: v.DESC_TIPO_DOCUMENTO,
                     DNI: v.DNI,
                     CELULAR: v.CELULAR,
                     TELEFONO: v.TELEFONO,
                     CORREO: v.CORREO,
                     COD_USUARIO: v.COD_USUARIO,
                     CLAVE_USUARIO: v.CLAVE_USUARIO,
                     FLG_ESTADO: v.FLG_ESTADO,
                     FEC_CREACION: v.FEC_CREACION,
                     USU_CREACION: v.USU_CREACION,
                     FEC_MODIFICACION: v.FEC_MODIFICACION,
                     USU_MODIFICACION: v.USU_MODIFICACION,

                 };
                DataTable.addRowData(Usuarios_Grilla, myData);
            });
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}



///*********************************************** ----------------- *************************************************/

///*********************************************** Actualiza  cargos  ************************************************/

function Usuarios_Actualizar() {
    if ($("#frmMantenimiento_Usuarios").valid()) {
        var item =
                {
                    ID_USUARIO: $("#hfd_ID_USUARIO").val(),
                    DNI: $("#DNI").val(),
                    NOMBRE: $("#NOMBRE").val(),
                    APE_PATERNO: $("#APE_PATERNO").val(),
                    APE_MATERNO: $("#APE_MATERNO").val(),
                    CELULAR: $("#CELULAR").val(),
                    TELEFONO: $("#TELEFONO").val(),
                    CORREO: $("#CORREO").val(),
                    FLG_ADMIN: $("#FLG_ADMIN").is(':checked') ? 1 : 0,
                    COD_USUARIO: $("#COD_USUARIO").val(),
                    CLAVE_USUARIO: $("#CLAVE_USUARIO").val(),
                    ID_TIPO_DOCUMENTO: $("#ID_TIPO_DOCUMENTO").val(),
                    USU_MODIFICACION: $('#input_hdcodusuario').val(),
                    Accion: $("#AccionUsuarios").val()
                };
        jConfirm("¿ Desea actualizar este usuario ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Administracion/Usuarios/Usuario_Actualizar';
                var auditoria = SICA.Ajax(url, item, false);
                if (auditoria != null && auditoria != "") {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            Usuarios_CargarGrilla();
                            Usuarios_Cerrar();
                            jOkas("Usuarios actualizado satisfactoriamente", "Proceso");
                        } else {
                            jError(auditoria.MENSAJE_SALIDA, "Atención");
                        }
                    } else {
                        jError(auditoria.MENSAJE_SALIDA, "Atención");
                    }
                }
            }
        });
    }
}

///*********************************************** ----------------- *************************************************/

///************************************************ Inserta cargos  **************************************************/

function Usuarios_Ingresar() {
     
    if ($('#AccionUsuarios').val() != 'N') {
        Usuarios_Actualizar();
    } else {
        if ($("#frmMantenimiento_Usuarios").valid()) {
            jConfirm("¿ Desea registrar este usuario ?", "Atención", function (r) {
                if (r) {
                    var item =
                        {
                            DNI: $("#DNI").val(),
                            NOMBRE: $("#NOMBRE").val(),
                            APE_PATERNO: $("#APE_PATERNO").val(),
                            APE_MATERNO: $("#APE_MATERNO").val(),
                            CELULAR: $("#CELULAR").val(),
                            TELEFONO: $("#TELEFONO").val(),
                            CORREO: $("#CORREO").val(),
                            FLG_ADMIN: $("#FLG_ADMIN").is(':checked') ? 1 : 0,
                            COD_USUARIO: $("#COD_USUARIO").val(),
                            CLAVE_USUARIO: $("#CLAVE_USUARIO").val(),
                            ID_TIPO_DOCUMENTO: $("#ID_TIPO_DOCUMENTO").val(),
                            
                            USU_CREACION: $('#input_hdcodusuario').val(),
                            ACCION: $("#AccionUsuarios").val()
                        };
                    var url = baseUrl + 'Administracion/Usuarios/Usuario_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Usuarios_CargarGrilla();
                                //Usuarios_Cerrar();
                                jOkas("Usuario registrado correctamente, a continuación configuré acceso al sistema para este usuario.", "Proceso");
                                $('#hfd_ID_USUARIO').val(auditoria.OBJETO);
                                $('#UsuariosTab, #Usuariospanel').removeClass('active');
                                $('#Usuariospanel').removeClass('show ');

                                $('#UsuariosAccesoTab').removeClass('DisabledContent'); 
                                $('#UsuariosAccesoTab, #UsuariosAccesoPanel').addClass('active');
                                $('#UsuariosAccesoPanel').addClass('show');
                                $('#UsuariosTab').addClass('DisabledContent');
                                $('#Usuarios_btn_Guardar').hide();

                            } else {
                                jError(auditoria.MENSAJE_SALIDA, "Atención");
                            }
                        } else {
                            jError(auditoria.MENSAJE_SALIDA, "Atención");
                        }
                    }
                }
            });
        }
    }
}

///*********************************************** ----------------- *************************************************/

///*********************************************** Elimina cargos  ***************************************************/

function Usuarios_Eliminar(ID_USUARIO) {
    jConfirm("¿ Desea eliminar este usuario ?", "Atención", function (r) {
        if (r) {
            var item = {
                ID_USUARIO: ID_USUARIO
            };
            var url = baseUrl + 'Administracion/Usuarios/Usuario_Eliminar';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        Usuarios_CargarGrilla();
                        Usuarios_Cerrar();
                        jOkas("Usuarios eliminado satisfactoriamente", "Proceso");
                    } else {
                        jError(auditoria.MENSAJE_SALIDA, "Atención");
                    }
                } else {
                    jError(auditoria.MENSAJE_SALIDA, "Atención");
                }
            }
        }
    });
}

///*********************************************** ----------------- *************************************************/

///*********************************************** Cambia estado de cargos  ******************************************/

function Usuarios_Estado(ID_USUARIO, CHECK) {
    var item = {
        ID_USUARIO: ID_USUARIO,
        FLG_ESTADO: CHECK.checked == true ? '1' : '0',
        USU_MODIFICACION: $('#input_hdcodusuario').val(),
    };
    var url = baseUrl + 'Administracion/Usuarios/Usuario_Estado';
    var auditoria = SICA.Ajax(url, item, false);
    if (auditoria != null && auditoria != "") {
        if (auditoria.EJECUCION_PROCEDIMIENTO) {
            if (auditoria.RECHAZAR) {
                jError(auditoria.MENSAJE_SALIDA, "Atención");
            }
        } else {
            jError(auditoria.MENSAJE_SALIDA, "Atención");
        }
    }
}

///*********************************************** ----------------- *************************************************/


function BuscarPersonalNatural(_NumeroDocumento) {
    if (_NumeroDocumento != "") {
        if (_NumeroDocumento.length == 8) {
            _blockUI('Buscando persona...');
            setTimeout(function () {
                var item = {
                    DNI: _NumeroDocumento
                };
                var url = baseUrl + 'Recursiva/ServiciosWeb/Service_ConsultaDni';
                var auditoria = SICA.Ajax(url, item, false);
                if (auditoria != null && auditoria != "") {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            var Objson = JSON.parse(auditoria.OBJETO);
                            Objson = JSON.parse(Objson);
                            $('#NOMBRE').val(Objson.nombres); 
                            $('#APE_PATERNO').val(Objson.apellidoPaterno); 
                            $('#APE_MATERNO').val(Objson.apellidoMaterno); 
                        } else {
                            jWarning(auditoria.MENSAJE_SALIDA, "Atención");
                        }
                    } else {
                        jWarning(auditoria.MENSAJE_SALIDA, "Atención");
                    }
                }
            }, 200);
        } else {
            jWarning("Numero dni debe tener 8 digitos", "Atención");
            return null;
        }

    } else {
        jWarning("Ingrese numero de dni", "Atención");
        return null;
    }
}
