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
    $("#" + Usuarios_Grilla).GridUnload();
    var colNames = ['Editar', 'Eliminar', 'Estado', 'codigo', 'ID','Es Jefe', 'Nombre y Apellidos','Clave Usuario','Codigo Usuario' ,'Tipo Documento', 'Dni', 'Celular','Telefono','Correo',
        'flg_estado', 'Fecha Creación', 'Usuario Creación', 'Fecha Modificación', 'Usuario Modificación'];
    var colModels = [
            { name: 'EDITAR', index: 'EDITAR', align: 'center', width: 60, hidden: false, formatter: Usuarios_actionEditar, sortable: false },
            { name: 'ELIMINAR', index: 'ELIMINAR', align: 'center', width: 80, hidden: false, formatter: Usuarios_actionEliminar, sortable: false },
            { name: 'ACTIVO', index: 'ACTIVO', align: 'center', width: 70, hidden: false, sortable: true, formatter: Usuarios_actionActivo, sortable: false },
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, },
            { name: 'ID_USUARIO', index: 'ID_USUARIO', width: 100, hidden: true, key: true },
            { name: 'ES_JEFE', index: 'ES_JEFE', width: 100, hidden: false, align: "left" },
            { name: 'NOMBRES_APE', index: 'NOMBRES_APE', width: 250, hidden: false, align: "left", formatter:Usuario_TextUsuario },
            { name: 'CLAVE_USUARIO', index: 'CLAVE_USUARIO', width: 150, hidden: false, align: "left" },
            { name: 'COD_USUARIO', index: 'COD_USUARIO', width: 100, hidden: true, align: "left" },
            { name: 'DESC_TIPO_DOCUMENTO', index: 'DESC_TIPO_DOCUMENTO', width: 150, hidden: false, align: "left" },
            { name: 'DNI', index: 'DNI', width: 150, hidden: false, align: "left" },
            { name: 'CELULAR', index: 'CELULAR', width: 100, hidden: false, align: "left" },
            { name: 'TELEFONO', index: 'TELEFONO', width: 100, hidden: false, align: "left" },
            { name: 'CORREO', index: 'CORREO', width: 200, hidden: false, align: "left" },
            { name: 'FLG_ESTADO', index: 'FLG_ESTADO', width: 300, hidden: true, align: "left" },
            { name: 'FEC_CREACION', index: 'FEC_CREACION',  width: 150, hidden: false, align: "left" },
            { name: 'USU_CREACION', index: 'USU_CREACION',  width: 150, hidden: false, align: "left" },
            { name: 'FEC_MODIFICACION', index: 'FEC_MODIFICACION',  width: 150, hidden: false, align: "left" },
            { name: 'USU_MODIFICACION', index: 'USU_MODIFICACION', width: 150, hidden: false, align: "left" },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false ,rowNumber: 50, rowNumbers: [50, 100, 200, 300, 500],
    };
    SICA.Grilla(Usuarios_Grilla, Usuarios_Barra, Usuarios_Grilla, 400, '', "Lista de Usuarios", '', 'ID_USUARIO', colNames, colModels, '', opciones);
}

function Usuarios_actionActivo(cellvalue, options, rowObject) {
    var check_ = 'check';
    if (rowObject.FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = " <label class=\"content_toggle_1\">"
            + "<input id=\"Usuarios_chk_" + rowObject.ID_USUARIO + "\" class=\"toggle_Beatiful_1\" type=\"checkbox\" onchange=\"Usuarios_Estado(" + rowObject.ID_USUARIO + ",this)\" " + check_ + ">"
            + "<div class=\"content_toggle_2\">"
            + "  <span class=\"Label_toggle_1\" ></span>"
             + "</div>"
            + "</label>";
    return _btn;
}

function Usuario_TextUsuario(cellvalue, options, rowObject) {
    var Usuario = rowObject.NOMBRES_APE;
    var Cod_usuario = rowObject.COD_USUARIO;
    var _text = '<span>' + Usuario + '</span><br><span style="font-size: 12px; color: #2c7be5;"><i class="bi bi-person"></i>&nbsp;Usuario: ' + Cod_usuario + '</span>';


    return _text
}

function Usuarios_actionEditar(cellvalue, options, rowObject) {
    var _btn = "<button title='Editar'  onclick='Usuarios_MostrarEditar(" + rowObject.ID_USUARIO + ");' class=\"btn btn-outline-light\" type=\"button\"> <i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;font-size:17px\"></i></button>";
    return _btn;
}

function Usuarios_actionEliminar(cellvalue, options, rowObject) {
    var _btn = "<button title='Eliminar'  onclick='Usuarios_Eliminar(" + rowObject.ID_USUARIO + ");' class=\"btn btn-outline-light\" type=\"button\" data-toggle=\"modal\" style=\"text-decoration: none !important;\"> <i class=\"bi bi-x-circle\" style=\"color:#e40613;font-size:17px\"></i></button>";
    return _btn;
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
           NOMBRES_APE: $('#Usuarios_NombresApe').val(),
           DNI: $('#Usuarios_Documento').val(),
           FLG_ESTADO: $('#Usuarios_Estado').val()
       };
    var url = baseUrl + 'Administracion/Usuarios/Usuario_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    jQuery("#" + Usuarios_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;

                var myData =
                 {
                     CODIGO: idgrilla,
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
                     FLG_ADMIN: v.FLG_ADMIN,
                     FLG_ESTADO: v.FLG_ESTADO,
                     FEC_CREACION: v.FEC_CREACION,
                     USU_CREACION: v.USU_CREACION,
                     FEC_MODIFICACION: v.FEC_MODIFICACION,
                     USU_MODIFICACION: v.USU_MODIFICACION,

                 };
                jQuery("#" + Usuarios_Grilla).jqGrid('addRowData', i, myData);
            });
            jQuery("#" + Usuarios_Grilla).trigger("reloadGrid");
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
    debugger; 
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
            $.ajax({
                type: "POST",
                url: "https://snirh.ana.gob.pe/consultaspide/wsGetReniec.asmx/consultaDirectaReniec",
                contentType: "application/json; charset=utf-8",
                data: "{ pDniConsulta: " + _NumeroDocumento.toString() + " }",
                dataType: "json",
                // 20 s espera
                timeout: 20000,
                beforeSend: function () {
                    blockUI_('Buscando persona...');
                },
                response: function (data) {
                },
                success: function (jdata) {
                    jQuery.unblockUI();
                    var json = JSON.parse(jdata.d);
                    debugger;
                    if (json.length > 0) {
                        if (json[0]['codRes'] != 1003) {

                            $('#NOMBRE').val(json[0]['nombres']);  
                            $('#APE_PATERNO').val( json[0]['apePat']);
                            $('#APE_MATERNO').val( json[0]['apeMat']);
                            //$('#DIRECCION').val(json[0]['dir'])
                            GenerarCredencialesUsuario();
                        } else {
                            jWarning(json[0]['desResul'], "Atención");

                        }
                    } else {
                        jWarning("Persona no econtrada", "Atención");

                    }
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    jQuery.unblockUI();
                    console.log(xmlHttpRequest.responseText);
                    console.log(textStatus);
                    console.log(errorThrown);
                    return null;
                }

            });
        } else {
            jWarning("Numero dni debe tener 8 digitos", "Atención");
            return null;
        }

    } else {
        jWarning("Ingrese numero de dni", "Atención");
        return null;
    }
}