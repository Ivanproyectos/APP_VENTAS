

function _GuardarTemporal() {

    var ErrorUrl = '';
    var url = baseUrl + "Administracion/Archivo/Guardar_Temporal_Archivo";
    var options = {
        type: "POST",
        dataType: "json",
        contentType: false,
        url: url,
        resetForm: false,
        beforeSubmit: function (formData, jqForm, options) {
            blockUI_("Subiendo Archivo...");
        },
        success: function (response, textStatus, jqXHR) {
            if (response.EJECUCION_PROCEDIMIENTO) {
                jQuery.unblockUI();
                CambioImg = true;
                Img_Array = new Array();
                Img_Array.push(response.OBJETO);
                $('#Img_Producto').css('background-image', 'url(' + MiSistema + response.OBJETO.RUTA_LINK + ')');
            }
            else {
                jDanger(response.MENSAJE_SALIDA, 'Atención');
            }
        },
        error: function (jqXHR, textStatus, errorThrown) { window.location = ErrorUrl; jQuery.unblockUI(); }
    };
    $("#frmMantenimiento_ImagenProducto").ajaxForm(options);
    $("#frmMantenimiento_ImagenProducto").submit();

}

function ConvertKilos_Gramos(_KILOS) {
    var Gramos = (_KILOS * 1000);
    return Gramos; 
}

function ConvertGramos_Kilos(_GRAMOS) {
    var Kilos = (_GRAMOS / 1000);
    return Kilos;
}

function Producto_BuscarProducto(COD_PRODUCTO, ID_SUCURSAL) {
    var item =
       {
           COD_PRODUCTO: COD_PRODUCTO,
           ID_SUCURSAL: ID_SUCURSAL

       };
    var url = baseUrl + 'Ventas/Ventas/Producto_Buscar_Listar';
    var Lista = SICA.Ajax(url, item, false);
    if (Lista.length < 2) {
        $.each(Lista, function (i, v) {
            $('#_DetallePorductos').show('slow');
            var _STOCK = v.STOCK;
            var _Cantidad = 1;
            $("#SEARCH_PRODUCTO").autocomplete("disable"); // DESACTIVA AUTOCOMPLETE
            $("#SEARCH_PRODUCTO").val(v.DESC_PRODUCTO);
            $("#ID_UNIDAD_MEDIDA").val(v.ID_UNIDAD_MEDIDA);
            $("#HDF_COD_UNIDAD_MEDIDA").val(v.COD_UNIDAD_MEDIDA);
            $("#_Info_codigoUnidad").text(v.COD_UNIDAD_MEDIDA);
            $("#COD_PRODUCTO").val(v.COD_PRODUCTO);
            if (ui.item.ID_UNIDAD_MEDIDA == 1) {
                _STOCK = ConvertGramos_Kilos(_STOCK);
            }
            $("#hfd_STOCK").val(_STOCK);
            $("#STOCK").val(_STOCK);
            $("#PRECIO_COMPRA").val(Number(ui.item.PRECIO_COMPRA).toFixed(2));
            $('#hfd_ID_PRODUCTO').val(ui.item.ID_PRODUCTO);
            $('#CANTIDAD').val(0);
            $('#CANTIDAD').focus();
            setTimeout(function () { $("#SEARCH_PRODUCTO").autocomplete("enable") }, 800); // ACTIVA AUTOCOMPLETE
            _Valido = true;
        });
    } else {
        jError("Se encontro mas de un producto con este codigo, verifique que el producto no tenga codigos duplicados.", "Atención");
    }
}

function Producto_MovientoProducto_Total(TIPO_MOVIMIENTO) {
    var _TOTAL = 0;
    var _ID_UNIDAD_MEDIDA = $('#ID_UNIDAD_MEDIDA').val();
    var _CANTIDAD = parseInt($('#CANTIDAD').val());
    var _CodUnidadMedida = $('#HDF_COD_UNIDAD_MEDIDA').val();
    var _Stock = $('#STOCK').val();
    if (TIPO_MOVIMIENTO == 1) // ingresos
    {
        if (_ID_UNIDAD_MEDIDA != 1) {
            _TOTAL = parseInt(_CANTIDAD) + parseInt(_Stock);
        } else {
            _TOTAL = parseFloat(_CANTIDAD) + parseFloat(_Stock);
        }

    } else if (TIPO_MOVIMIENTO == 2) // salidas
    {
        if (_ID_UNIDAD_MEDIDA != 1) {
            _TOTAL = parseInt(_Stock) - parseInt(_CANTIDAD);
        } else {
            _TOTAL = parseFloat(_Stock) - parseFloat(_CANTIDAD);
        }

    }
    return _TOTAL; 
}

function DifferenceDaysFechas(Fecha_ini, Fecha_fin) {
    // First we split the values to arrays date1[0] is the year, [1] the month and [2] the day date1 = date1.split('-'); date2 = date2.split('-'); // Now we convert the array to a Date object, which has several helpful methods date1 = new Date(date1[0], date1[1], date1[2]); date2 = new Date(date2[0], date2[1], date2[2]); // We use the getTime() method and get the unixtime (in milliseconds, but we want seconds, therefore we divide it through 1000) date1_unixtime = parseInt(date1.getTime() / 1000); date2_unixtime = parseInt(date2.getTime() / 1000); // This is the calculated difference in seconds var timeDifference = date2_unixtime - date1_unixtime; // in Hours var timeDifferenceInHours = timeDifference / 60 / 60; // and finaly, in days :) var timeDifferenceInDays = timeDifferenceInHours / 24; alert(timeDifferenceInDays);
    var date1 = Fecha_ini;
    var date2 = Fecha_fin;
    // First we split the values to arrays date1[0] is the year, [1] the month and [2] the day 
    date1 = date1.split('/'); date2 = date2.split('/'); // Now we convert the array to a Date object, which has several helpful methods 
    date1 = new Date(date1[2], date1[1], date1[0]);
    date2 = new Date(date2[2], date2[1], date2[0]); // We use the getTime() method and get the unixtime (in milliseconds, but we want seconds, therefore we divide it through 1000) 
    date1_unixtime = parseInt(date1.getTime() / 1000);
    date2_unixtime = parseInt(date2.getTime() / 1000); // This is the calculated difference in seconds 
    var timeDifference = date2_unixtime - date1_unixtime; // in Hours 
    var timeDifferenceInHours = timeDifference / 60 / 60; // and finaly, in days :)
    var timeDifferenceInDays = timeDifferenceInHours / 24;

    if (isNaN(timeDifferenceInDays) || timeDifferenceInDays < 0)
        timeDifferenceInDays = 0;

    return timeDifferenceInDays;

}