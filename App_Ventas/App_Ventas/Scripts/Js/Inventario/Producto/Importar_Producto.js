var Importar_ErroresCarga_Grilla = 'Importar_ErroresCarga_Grilla';
var Importar_ErroresCarga_Barra = 'Importar_ErroresCarga_Barra';
function Importar_ErroresCarga_ConfigurarGrilla() {
    $("#" + Importar_ErroresCarga_Grilla).GridUnload();
    var colNames = ['codigo', 'Nro Fila', 'Descrición'];
    var colModels = [
            { name: 'CODIGO', index: 'CODIGO', align: 'center', width: 100, hidden: true, key: true },
            { name: 'NRO_FILA', index: 'NRO_FILA', align: 'left', width: 80, hidden: false },
            { name: 'DESCRIPCION', index: 'DESCRIPCION', align: 'left', width: 400, hidden: false },
    ];
    var opciones = {
        GridLocal: true, multiselect: false, CellEdit: false, Editar: false, nuevo: false, eliminar: false, search: false, rowNumber:50 ,rowNumbers:[ 20, 50, 100, 300],
    };
    SICA.Grilla(Importar_ErroresCarga_Grilla, Importar_ErroresCarga_Barra, Importar_ErroresCarga_Grilla, 150, '', "", '', 'CODIGO', colNames, colModels, '', opciones);
}

function Producto_MostrarImportarProducto() {
    var _ID_SUCURSAL = $('#ID_SUCURSAL').val();
    var _DESC_SUCURSAL = $('select[name="ID_SUCURSAL"] option:selected').text();
    if (_ID_SUCURSAL != "") {
        _DESC_SUCURSAL = _DESC_SUCURSAL.replace(/ /g, "+");
        jQuery("#myModalNuevo").html('');
        jQuery("#myModalNuevo").load(baseUrl + "Inventario/Importar/View_Importar?ID_SUCURSAL=" + _ID_SUCURSAL +
            "&DESC_SUCURSAL=" + _DESC_SUCURSAL, function (responseText, textStatus, request) {
                $('#myModalNuevo').modal({ show: true, backdrop: 'static', keyboard: false });
                $.validator.unobtrusive.parse('#myModalNuevo');
                if (request.status != 200) return;
            });
    } else {
        jInfo('Actualmente tu está mirando los productos de todos los almacenes, debes seleccionar uno en específico donde deseas registrar el producto.', 'Atención')
    }
}

function Producto_DescargarPlantillaProducto() {
    jQuery("#myModalDescargar").html('');
    jQuery("#myModalDescargar").load(baseUrl + "Inventario/Importar/Importar_DescagarPlantilla", function (responseText, textStatus, request) {
    $.validator.unobtrusive.parse('#myModalDescargar');
        if (request.status != 200) return;
    });
    
}

function Producto_ImportarProducto() {
    var _ID_SUCURSAL = $("#ID_SUCURSAL").val();
    var pregunta = "";
    if (_ID_SUCURSAL == "") {
        jAlert("Alamacen no seleccionado, por favor seleccione uno", "Atención");
        return;
    }
    jConfirm("Antes de continuar favor de asegurarse que el archivo no tenga caracteres especiales [;*_\!,etc] en el nombre y el nombre de la hoja sea Hoja1,"
              + "si ya hizo todo lo mencionado obvie este mensaje presionando el botón Aceptar para seguir con el proceso", "Atención", function (r) {
                  if (r) {
                      var url = baseUrl + "Inventario/Importar/Importar_CargarExcel";
                      var options = {
                          type: "POST",
                          dataType: "json",
                          url: url,
                          extraData: ({
                          }),
                          resetForm: true,
                          beforeSubmit: function (formData, jqForm, options) {
                              var queryString = $.param(formData);
                              blockUI_("Importando productos, espere un momento por favor...");
                              return true;
                          },
                          success: function (auditoria) {
                              jQuery.unblockUI();
                              $("#Lbl_Namefile").html("Sin archivo cargado...");
                              if (auditoria.EJECUCION_PROCEDIMIENTO) {
                                  if (!auditoria.RECHAZAR) {
                                      jOkas('El proceso de carga ha culminado', 'Alerta');

                                  } else {
                                      jError(auditoria.MENSAJE_SALIDA, 'Atención');
                                      if (auditoria.OBJETO != null)
                                          Importar_TablaResutaldos(auditoria.OBJETO);
                                  
                                  }
                              }
                              else {
                                  jError(auditoria.MENSAJE_SALIDA, 'Atención');
                              }

                        
           
                          },
                          error: function (jqXHR, textStatus, errorThrown) {
                              jQuery.unblockUI();
                              alert(jqXHR);
                              window.location = ErrorUrl;
                          }

                      };
                      $("#frmMantenimiento_ImportarExcel").ajaxForm(options);
                      $("#frmMantenimiento_ImportarExcel").submit();
                  }
              });
}

function Importar_TablaResutaldos(Listado) {
    $('#Importar_GirdErrores').show('slow'); 
    jQuery("#" + Importar_ErroresCarga_Grilla).jqGrid('clearGridData', true).trigger("reloadGrid");
    $.each(Listado, function (i, v) {
        var idgrilla = i + 1;
        var myData =
            {
                CODIGO: idgrilla,
                NRO_FILA: v.NRO_FILA,
                DESCRIPCION: v.DESCRIPCION
            };
        jQuery("#" + Importar_ErroresCarga_Grilla).jqGrid('addRowData', idgrilla, myData);
    });
    jQuery("#" + Importar_ErroresCarga_Grilla).trigger("reloadGrid");
 
}