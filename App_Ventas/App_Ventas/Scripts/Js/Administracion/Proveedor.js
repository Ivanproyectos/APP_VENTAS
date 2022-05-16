var Proveedor_Grilla = 'Proveedor_Grilla';
var Proveedor_Barra = 'Proveedor_Barra';

function Proveedor_Cerrar() {
    $('#myModalNuevo').modal('hide');
    jQuery("#myModalNuevo").html('');
}

function Proveedor_Limpiar() {
    $("#Proveedor_NombreYape").val('');
    $('#Proveedor_NumeroDocumento').val('');
    $('#Proveedor_Estado').val(2);

    Proveedor_CargarGrilla();
}

function Proveedor_ConfigurarGrilla() {
    DataTable.GridUnload(Proveedor_Grilla);
    var colModels = [
          { data: "ID_PROVEEDOR", name: "ID_PROVEEDOR", title: "ID_PROVEEDOR", autoWidth: false, visible: false, },
          { data: "NOMBRES_APE", name: "NOMBRES_APE", title: "Nombres y apellidos", autoWidth: true },
          { data: "DESC_TIPO_DOCUMENTO", name: "DESC_TIPO_DOCUMENTO", title: "Tipo Doc.", autoWidth: false, width: "90px", },
          { data: "NUMERO_DOCUMENTO", name: "NUMERO_DOCUMENTO", title: "NumDoc", autoWidth: false, },
          { data: "DIRECCION", name: "DIRECCION", title: "Dirección", autoWidth: true, visible: false},
          { data: "CELULAR", name: "CELULAR", title: "Celular", autoWidth: true },
          { data: "TELEFONO", name: "TELEFONO", title: "Telefono", autoWidth: true },
          { data: "CORREO", name: "CORREO", title: "Correo", autoWidth: true },
          { data: "DESC_UBIGEO", name: "DESC_UBIGEO", title: "Ubigeo", autoWidth: true },
          {
              data: null, name: "FLG_ESTADO", title: "Activo", autoWidth: true, sortable: false,
              render: function (data, type, row, meta) { return Cliente_actionActivo(data.FLG_ESTADO, data.ID_PROVEEDOR); }
          },
          {
              data: null, sortable: false, title: "Acciones", width: "60px",
              render: function (data, type, row, meta) { return Cliente_actionAcciones(data.ID_PROVEEDOR); }
          },

    ];
    var opciones = {
        GridLocal: true, multiselect: false, sort: "desc", enumerable: false,
        eliminar: false, search: true, rowNumber: 10, rowNumbers: [10, 25, 50], rules: false, responsive: true, processing: true
    };
    DataTable.Grilla(Proveedor_Grilla, '', 'ID_PROVEEDOR', colModels, opciones, "ID_PROVEEDOR");
}



function Cliente_actionAcciones(ID_PROVEEDOR) {
    var _btn_Editar = "<a class=\"dropdown-item\" onclick='Proveedor_MostrarEditar(" + ID_PROVEEDOR + ")'><i class=\"bi bi-pencil-fill\" style=\"color:#f59d3f;\"></i>&nbsp;  Editar</a>";
    var _btn_Eliminar = "<a class=\"dropdown-item\" onclick='Proveedor_Eliminar(" + ID_PROVEEDOR + ")'><i class=\"bi bi-trash-fill\" style=\"color:#e40613;\"></i>&nbsp;  Eliminar</a>";
    var _btn = "<div class=\"btn-group Group_Acciones\" role=\"group\" title=\"Acciones \" >" +
           "<button  style=\" background: transparent; border: none; color: #000000;font-size: 18px;\" type=\"button\" class=\"btn  dropdown-toggle\" data-toggle=\"dropdown\" aria-expanded=\"false\"><i class=\"bi bi-list\"></i></button>" +
           "<div class=\"dropdown-menu\" x-placement=\"bottom-start\" style=\"position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 35px, 0px);\">" +
           _btn_Editar +
           _btn_Eliminar +
            "</div>" +
        "</div>";
    return _btn;
}


function Cliente_actionActivo(FLG_ESTADO, ID_PROVEEDOR) {
    var check_ = 'check';
    if (FLG_ESTADO == 1)
        check_ = 'checked';

    var _btn = "<input type=\"checkbox\" id=\"Proveedor_chk_" + ID_PROVEEDOR + "\"  data-switch=\"state\" onchange=\"Proveedor_Estado(" + ID_PROVEEDOR + ",this)\" " + check_ + ">"
              + " <label for=\"Proveedor_chk_" + ID_PROVEEDOR + "\" data-on-label=\"Yes\" data-off-label=\"No\"></label>";
    return _btn;
}



function Proveedor_MostrarNuevo() {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Proveedor/Mantenimiento?id=0&Accion=N", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}

function Proveedor_MostrarEditar(ID_PROVEEDOR) {
    jQuery("#myModalNuevo").html('');
    jQuery("#myModalNuevo").load(baseUrl + "Administracion/Proveedor/Mantenimiento?id=" + ID_PROVEEDOR + "&Accion=M", function (responseText, textStatus, request) {
        $('#myModalNuevo').modal({ show: true });
        $.validator.unobtrusive.parse('#myModalNuevo');
        if (request.status != 200) return;
    });
}


///*********************************************** ----------------- *************************************************/

///*********************************************** Lista los  Proveedor **************************************************/

function Proveedor_CargarGrilla() {
    var item =
       {
           //NOMBRES_APE: $('#Proveedor_NombreYape').val(),
           //NUMERO_DOCUMENTO: $('#Proveedor_NumeroDocumento').val(),
           FLG_ESTADO: 2
       };
    var url = baseUrl + 'Administracion/Proveedor/Proveedor_Listar';
    var auditoria = SICA.Ajax(url, item, false);
    DataTable.clearGridData(Proveedor_Grilla);
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            $.each(auditoria.OBJETO, function (i, v) {
                var idgrilla = i + 1;
                var myData =
                 {
                     CODIGO: idgrilla,
                     ID_PROVEEDOR: v.ID_PROVEEDOR,
                     NOMBRES_APE: v.NOMBRES_APE,
                     NUMERO_DOCUMENTO: v.NUMERO_DOCUMENTO,
                     DIRECCION: v.DIRECCION,
                     CORREO: v.CORREO,
                     DESC_TIPO_DOCUMENTO: v.DESC_TIPO_DOCUMENTO,
                     DIRECCION: v.DIRECCION,
                     CORREO: v.CORREO,
                     TELEFONO: v.TELEFONO,
                     CELULAR: v.CELULAR,
                     DESC_UBIGEO: v.DESC_UBIGEO,
                     DETALLE: v.DETALLE,

                     FLG_ESTADO: v.FLG_ESTADO,
                     FEC_CREACION: v.FEC_CREACION,
                     USU_CREACION: v.USU_CREACION,
                     FEC_MODIFICACION: v.FEC_MODIFICACION,
                     USU_MODIFICACION: v.USU_MODIFICACION

                 };
                DataTable.addRowData(Proveedor_Grilla, myData);
            });
        }
    } else {
        jError(auditoria.MENSAJE_SALIDA, "Atención");
    }
}



///*********************************************** ----------------- *************************************************/

///*********************************************** Actualiza  Proveedors  ************************************************/

function Proveedor_Actualizar() {
    if ($("#frmMantenimiento_Proveedor").valid()) {
        var item =
                {
                    ID_PROVEEDOR: $("#hfd_ID_PROVEEDOR").val(),
                    ID_TIPO_DOCUMENTO: $("#ID_TIPO_DOCUMENTO").val(),
                    NUMERO_DOCUMENTO: $("#NUMERO_DOCUMENTO").val(),
                    NOMBRES_APE: $("#NOMBRES_APE").val(),
                    TELEFONO: $("#TELEFONO").val(),
                    CORREO: $("#CORREO").val(),
                    DIRECCION: $("#DIRECCION").val(),
                    CELULAR: $("#CELULAR").val(),
                    COD_UBIGEO: $("#COD_UBIGEO").val(),
                    DETALLE: $("#DETALLE").val(),
                    USU_MODIFICACION: $('#input_hdcodusuario').val(),
                    Accion: $("#AccionProveedor").val()
                };
        jConfirm("¿ Desea actualizar este Proveedor ?", "Atención", function (r) {
            if (r) {
                var url = baseUrl + 'Administracion/Proveedor/Proveedor_Actualizar';
                var auditoria = SICA.Ajax(url, item, false);
                if (auditoria != null && auditoria != "") {
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            Proveedor_CargarGrilla();
                            Proveedor_Cerrar();
                            jOkas("Proveedor actualizado satisfactoriamente", "Proceso");
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

///************************************************ Inserta Proveedors  **************************************************/

function Proveedor_Ingresar() {
    debugger; 
    if ($('#AccionProveedor').val() != 'N') {
        Proveedor_Actualizar();
    } else {
        if ($("#frmMantenimiento_Proveedor").valid()) {
            jConfirm("¿ Desea registrar este Proveedor ?", "Atención", function (r) {
                if (r) {
                    var item =
                        {
                            ID_TIPO_DOCUMENTO: $("#ID_TIPO_DOCUMENTO").val(),
                            NUMERO_DOCUMENTO: $("#NUMERO_DOCUMENTO").val(),
                            NOMBRES_APE: $("#NOMBRES_APE").val(),
                            TELEFONO: $("#TELEFONO").val(),
                            CORREO: $("#CORREO").val(),
                            DIRECCION: $("#DIRECCION").val(),
                            CELULAR: $("#CELULAR").val(),
                            COD_UBIGEO: $("#COD_UBIGEO").val(),
                            DETALLE: $("#DETALLE").val(),
                            USU_CREACION: $('#input_hdcodusuario').val(),
                            ACCION: $("#AccionProveedor").val()
                        };
                    var url = baseUrl + 'Administracion/Proveedor/Proveedor_Insertar';
                    var auditoria = SICA.Ajax(url, item, false);
                    if (auditoria != null && auditoria != "") {
                        if (auditoria.EJECUCION_PROCEDIMIENTO) {
                            if (!auditoria.RECHAZAR) {
                                Proveedor_CargarGrilla();
                                Proveedor_Cerrar();
                                jOkas("Proveedor registrado satisfactoriamente", "Proceso");
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

///*********************************************** Elimina Proveedors  ***************************************************/

function Proveedor_Eliminar(ID_PROVEEDOR) {
    jConfirm("¿ Desea eliminar este Proveedor ?", "Atención", function (r) {
        if (r) {
            var item = {
                ID_PROVEEDOR: ID_PROVEEDOR
            };
            var url = baseUrl + 'Administracion/Proveedor/Proveedor_Eliminar';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        Proveedor_CargarGrilla();
                        Proveedor_Cerrar();
                        jOkas("Proveedor eliminado satisfactoriamente", "Proceso");
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

///*********************************************** Cambia estado de Proveedors  ******************************************/

function Proveedor_Estado(ID_PROVEEDOR, CHECK) {
    var item = {
        ID_PROVEEDOR: ID_PROVEEDOR,
        FLG_ESTADO: CHECK.checked == true ? '1' : '0',
        USU_MODIFICACION: $('#input_hdcodusuario').val(),
    };
    var url = baseUrl + 'Administracion/Proveedor/Proveedor_Estado';
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