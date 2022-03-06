
SICA = {
    ShowElement: function (elemento) {
        $(elemento).slideDown(200).animate({ opacity: 1 }, 300);
    },
    HideElement: function (elemento) {
        $(elemento).animate({
            opacity: 0.25
        }, 300, function () {
            $(elemento).slideUp(200);
        });
    },
    Mayuscula: function (e, elemento) {
        elemento.value = elemento.value.toUpperCase();
    },
    MenuSelected: function (idFormulario) {
        if (idFormulario == '')
            idFormulario = "01";
        $("li.pselectedForm").removeClass("pselectedForm");
        $("#form" + idFormulario).addClass("pselectedForm");
    },
    Completar: function (ctrl, len) {
        var numero = ctrl.value;
        if (numero.length == len || numero.length == 0) return true;
        for (var i = 1; numero.length < len; i++) {
            numero = '0' + numero;
        }
        ctrl.value = numero;
        return true;
    },
    RemoveArray: function (arr) {
        var what, a = arguments, l = a.length, ax;
        while (l > 1 && arr.length) {
            what = a[--l];
            while ((ax = arr.indexOf(what)) != -1) {
                arr.splice(ax, 1);
            }
        }
        return arr;
    },
    PadLeft: function (value, len, character) {
        len = len - value.length;
        for (var i = 1; i <= len; i++) {
            value = character + value;
        }
        return value;
    },
    ValidarInput: function (args) {
        var isValid = true;
        $.each(args, function (index, item) {
            if ($('#' + item).val() == '') {
                $('#' + item).parent().append("<span class='error'>*</span>");
                isValid = false;
            }
        });

        return isValid;
    },
    ClearInput: function (args) {
        $.each(args, function (index, item) {
            $('#' + item).val('');
        });
    },
    DisableInput: function (args, estado) {
        $.each(args, function (index, item) {
            $('#' + item).attr("disabled", estado);
        });
    },
    RemoveSpan: function (args) {
        $.each(args, function (index, item) {
            $('#' + item + " span").remove();
        });
    },
    HideMantenimiento: function (estado) {
        if (estado) {
            $("#Lista").show();
            $("#Mantenedor").hide();
            $("#divDetalle").hide();
        } else {
            $("#Lista").hide();
            $("#Mantenedor").show();
            $("#divDetalle").show();
        }
    },
    ReordenarInput: function (args) {
        $.each(args, function (item) {
            $("input[name^=" + item + "]").each(function (index) {
                $(this).attr("id", item + index);
            });
        });
    },
    AgregarClass: function () {
        $("#dialog_btnCancelar").addClass("btn btn-primary");
        $("#dialog_btnAceptar").addClass("btn btn-primary");
        $("#dialog_btnAgregar").addClass("btn btn-primary");
        $("#dialog_btnGuardar").addClass("btn btn-primary");
    },
    // signo: 0: no negativo, 1: positivo, -1: negativo
    ValidarNumero: function (texto, vacio, signo) {
        var cadena = texto != null ? (texto != undefined ? texto.toString() : '') : '';
        if (vacio) {
            cadena = cadena.trim();
        }
        var er_fh;
        if (signo == 0) {
            er_fh = /^([0-9]+)$/;
        }
        else if (signo < 0) {
            er_fh = /^([-][1-9][0-9]*)$/;
        }
        else {//if (signo == 0)
            er_fh = /^([1-9][0-9]*)$/;
        }
        if (cadena == "") {
            return false;
        }
        if (!(er_fh.test(cadena))) {
            return false;
        }
        return true;
    },
    ValidarDecimal: function (texto, vacio, signo) {
        var cadena = texto.toString();
        if (vacio) {
            cadena = cadena.trim();
        }
        var er_fh;
        if (signo == null) {
            er_fh = /^([-]?[0]*[1-9][0-9]*([.][0-9]*)?)$/;
        }
        else {
            if (signo == 0) {
                er_fh = /^([-]?[0-9]*([.][0-9]*)?)$/;
            }
            else if (signo > 0) {
                er_fh = /^([0]*[1-9][0-9]*([.][0-9]*)?)$/;
            }
            else {
                er_fh = /^([-][0]*[1-9][0-9]*([.][0-9]*)?)$/;
            }
        }
        if (cadena == "") {
            return false;
        }
        if (!(er_fh.test(cadena))) {
            return false;
        }
        return true;
    },
    ValidarEmail: function (texto, vacio) {
        var cadena = texto.toString();
        if (vacio) {
            cadena = cadena.trim();
            if (cadena == "") {
                return true;
            }
        }

        var er_fh = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*([\.]\w{2,4})+$/;

        if (er_fh.test(cadena)) {
            return true;
        }
        return false;
    },
    // funciones matematicas
    // Multiplica array de Numeros [2.3, 5.4, 4]
    MultiplicaNumeros: function (ListaNumeros, decimal) {
        var numeroString = "";
        var cantDecimal = 0;
        var posCaracterDecimal = -1;
        var parteEntera = "";
        var parteDecima = "";
        var numeroEntero = 0;
        var Producto = 1;
        var resultado = "";
        $.each(ListaNumeros, function (k, Numero) {
            numeroString = Numero.toString().replace(/\s/g, "").replace(",", ".");
            posCaracterDecimal = numeroString.indexOf(".");
            if (posCaracterDecimal > 0) {
                parteEntera = numeroString.substring(0, posCaracterDecimal);
                parteDecima = numeroString.replace(parteEntera, "").replace(".", "");
                cantDecimal = cantDecimal + parteDecima.toString().length;
                numeroEntero = parseInt(parteEntera) * parseInt("1" + SICA.PadLeft("", parteDecima.toString().length, "0")) + parseInt(parteDecima);
                Producto = Producto * numeroEntero;
            }
            else {
                Producto = Producto * parseInt(Numero);
            }
        });
        if (Producto.toString().length > cantDecimal) {
            var dif = Producto.toString().length - cantDecimal;
            parteEntera = Producto.toString().substring(0, dif);
            parteDecima = Producto.toString().replace(parteEntera, "");
            if (parteDecima.length > 0 && parseInt(parteDecima) > 0) {
                resultado = parteEntera + "." + parteDecima;
            }
            else {
                resultado = parteEntera + ".00";
            }
        }
        else {
            var dif = cantDecimal - Producto.toString().length;
            resultado = "0." + SICA.PadLeft(Producto.toString(), dif, "0");
        }
        if (decimal != null) {
            return parseFloat(resultado).toFixed(decimal).toString();
        }
        else {
            return resultado;
        }
    },

    // suma el valor de la columa "NameColumns" 
    // Estado es para verificar si el registro de la lista se considera en la suma
    SumarMontoColumns: function (Lista, NameColumns, Estado, ColumnEstado, ValueEstado) {
        var SumaTotal = 0;
        $.each(Lista, function (i, v) {
            if (Estado) {
                if (v[ColumnEstado] == ValueEstado) {
                    SumaTotal = parseFloat(SumaTotal) + parseFloat(v[NameColumns]);
                }
            }
            else {
                SumaTotal = parseFloat(SumaTotal) + parseFloat(v[NameColumns]);
            }
        });
        return SumaTotal;
    },
    // verificar existencia de campo en un array, y SI este esta en estado actualizado 
    VarificarExistenciaActualizado: function (Lista, Campos, ValorCampo, Actualizacion, CampoOrden, IdOrdenGrilla) {
        var existe = false;
        var existecampo = true;
        $.each(Lista, function (i, v) {
            $.each(Campos, function (k, c) {
                if (v[c] == ValorCampo[k]) {
                    if (actualizacion) {
                        if (v[CampoOrden] != IdOrdenGrilla) {
                            if (existecampo) {
                                existe = true;
                            }
                        }
                        else {
                            //existecampo = false;
                        }
                    }
                    else {
                        if (existecampo) {
                            existe = true;
                        }
                    }
                }
                else {
                    existe = false;
                    existecampo = false;
                    return;
                }
            });
            if (existe) {
                return true;
            }
            else {
                existecampo = true;
            }
        });
        return existe;
    },
    GrillaVarificarExistencia: function (nombregrilla, Campos, ValorCampo, actualizacion, IdOrdenGrilla) {
        var existe = false;
        var existecampo = true;
        var rowkeys = jQuery("#" + nombregrilla).jqGrid('getGridParam', 'selarrrow');
        $.each(rowkeys, function (i, rowkey) {
            var row = jQuery("#" + nombregrilla).getRowData(rowkey);
            $.each(Campos, function (k, c) {
                if (row[c] == ValorCampo[k]) {
                    if (actualizacion) {
                        if (row[CampoOrden] != IdOrdenGrilla) {
                            if (existecampo) {
                                existe = true;
                            }
                        }
                        else {
                            //existecampo = false;
                        }
                    }
                    else {
                        if (existecampo) {
                            existe = true;
                        }
                    }
                }
                else {
                    existe = false;
                    existecampo = false;
                    return;
                }
            });
            if (existe) {
                return true;
            }
            else {
                existecampo = true;
            }
        });
        return existe;
    },
    //limpia toda la grilla
    ClearGrilla: function (grilla) {
        $("#" + grilla).jqGrid("clearGridData", true);
    },
    SelectRowMSG: function (rowSelect, varios, idioma) {

        var msg = "";
        if (idioma == 'es') {
            if (rowSelect != null && rowSelect != undefined) {
                if (rowSelect.length == 0) {
                    msg = 'Seleccione un item';
                }
                else if (rowSelect.length > 1 && !varios) {
                    msg = 'Seleccione solo un item';
                }
            }
            else {
                msg = 'Genere una búsqueda';
            }
        }
        else {
            if (rowSelect != null && rowSelect != undefined) {
                if (rowSelect.length == 0) {
                    msg = 'Select an Item.';
                }
                else if (rowSelect.length > 1 && !varios) {
                    msg = 'Select only an Item.';
                }
            }
            else {
                msg = 'Generate a Search.';
            }
        }
        return msg;
    },
    ObtenerMax: function (array, IDCampo) {
        var max = 0;
        for (var i = 0; i < array.length; i++) {
            if (array[i][IDCampo] > 0) {
                if (array[i][IDCampo] >= max)
                    max = array[i][IDCampo];
            }
            else {
                if (array[i].ID >= max)
                    max = array[i].ID;
            }
        }
        return max;
    },
    Ajax: function (url, parameters, async, funcionSuccess) {
        var rsp;
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            traditional: true,
            async: async,
            data: JSON.stringify(parameters),
            beforeSend: function () {
                blockUI_("");
            },
            success: function (response) {
                jQuery.unblockUI();
                rsp = response;
                if (typeof (funcionSuccess) == 'function') {
                    funcionSuccess(response);
                }
            },
            failure: function (msg) {
                alert(msg);
                rsp = msg;
            },
            error: function (xhr, status, error) {
                jQuery.unblockUI();
                alert(error);
                rsp = error;
            },
            complete: function () {
                jQuery.unblockUI();
            },
        });

        return rsp;
    },
    EnviarEmailOpacity: function (url, parameters, async, okFunc, noFunc) {
        //$.blockUI({ message: "Enviando Email" });
        $.blockUI({
            message: "Enviando Email"
            , css: {
                border: 'none',
                padding: '15px',
                backgroundColor: '#000',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                opacity: .5,
                color: '#fff',
                'z-index': 50
            }
        });
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: async,
            data: JSON.stringify(parameters),
            success: function (response) {
                if (response.Success) {
                    if (typeof (okFunc) == 'function') { okFunc(response); }
                }
                else {
                    if (typeof (noFunc) == 'function') { noFunc(response); }
                }
                setTimeout($.unblockUI, 100);
            },
            failure: function (msg) {
                rsp = msg;
            },
            error: function (xhr, status, error) {
                rsp = error;
            }
        });

    },
    EnviarEmail: function (url, parameters, async, okFunc, noFunc) {
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: async,
            data: JSON.stringify(parameters),
            success: function (response) {
                if (response.Success) {
                    if (typeof (okFunc) == 'function') { okFunc(response); }
                }
                else {
                    if (typeof (noFunc) == 'function') { noFunc(response); }
                }
            },
            failure: function (msg) {
                //alert(msg);
                rsp = msg;
            },
            error: function (xhr, status, error) {
                //alert(error);
                rsp = error;
            }
        });
    },
    ParseDate: function (date) {
        var parts = date.split("/");
        var fecha = new Date(parts[1] + "/" + parts[0] + "/" + parts[2]);
        return fecha.getTime();
    },
    GetTimeToDate: function (date) {
        date = date != null ? (date != undefined ? date.toString() : '') : '';
        if (date != null && date != "") {
            if (date.indexOf("/Date(") >= 0) {
                var fecha = new Date(parseInt(date.replace("/Date(", "").replace(" )/", ""), 10));
                var fecharecepcion = SICA.getDate(fecha) + '/' + SICA.getMonth(fecha) + '/' + fecha.getFullYear();
                return fecharecepcion;
            }
        }
        return date;
    },
    // date = dd/mm/yyyy
    AddDayDate: function (date, addday) {
        var parts = date.split("/");
        var date = new Date(parts[1] + "/" + parts[0] + "/" + parts[2]);
        date.setDate(date.getDate() + addday);
        var day = parseInt(date.getDate()) >= 10 ? date.getDate() : "0" + date.getDate();
        var month = (parseInt(date.getMonth()) + 1) >= 10 ? (parseInt(date.getMonth()) + 1) : "0" + (parseInt(date.getMonth()) + 1);
        var futDate = day + "/" + month + "/" + date.getFullYear();
        return futDate;
    },
    // FechaInicio = "FechaInicio"   :::  FechaFin ="FechaFin"  --> ambos sin #
    CustomRange: function (input) {
        //var FechaInicio = FechaInicio == "" ? "FechaInicio" : FechaInicio;
        //var FechaFin = FechaFin == "" ? "FechaFin" : FechaFin;
        var FechaInicio = "FECHAINICIO";
        var FechaFin = "FECHAFIN";
        var min = new Date(2008, 11 - 1, 1), //Set this to your absolute minimum date
            dateMin = min,
            dateMax = new Date(3008, 11 - 1, 1),
            dayRange = 1000; // Set this to the range of days you want to restrict to

        if (input.id === FechaInicio) {
            if ($("#" + FechaFin).datepicker("getDate") != null) {
                dateMax = $("#" + FechaFin).datepicker("getDate");
                dateMin = $("#" + FechaFin).datepicker("getDate");
                dateMin.setDate(dateMin.getDate() - dayRange);
                if (dateMin < min) {
                    dateMin = min;
                }
            }
            else {
                dateMax = new Date(3008, 11 - 1, 1); //Set this to your absolute maximum date
            }
        }
        else if (input.id === FechaFin) {
            dateMax = new Date(3008, 11 - 1, 1); //Set this to your absolute maximum date
            if ($("#" + FechaInicio).datepicker("getDate") != null) {
                dateMin = $("#" + FechaInicio).datepicker("getDate");
                var rangeMax = new Date(dateMin.getFullYear(), dateMin.getMonth(), dateMin.getDate() + dayRange);

                if (rangeMax < dateMax) {
                    dateMax = rangeMax;
                }
            }
        }
        return {
            minDate: dateMin,
            maxDate: dateMax
        };
    },

    CustomRange2: function (input, input1) {
        //var FechaInicio = FechaInicio == "" ? "FechaInicio" : FechaInicio;
        //var FechaFin = FechaFin == "" ? "FechaFin" : FechaFin;
        var FechaInicio = "FECHAINICIO";
        var FechaFin = "FECHAFIN";
        var min = new Date(2008, 11 - 1, 1), //Set this to your absolute minimum date
            dateMin = min,
            dateMax = new Date(3008, 11 - 1, 1),
            dayRange = 1000; // Set this to the range of days you want to restrict to

        if (input.id === FechaInicio) {
            if ($("#" + FechaFin).datepicker("getDate") != null) {
                dateMax = $("#" + FechaFin).datepicker("getDate");
                dateMin = $("#" + FechaFin).datepicker("getDate");
                dateMin.setDate(dateMin.getDate() - dayRange);
                if (dateMin < min) {
                    dateMin = min;
                }
            }
            else {
                dateMax = new Date(3008, 11 - 1, 1); //Set this to your absolute maximum date
            }
        }
        else if (input.id === FechaFin) {
            dateMax = new Date(3008, 11 - 1, 1); //Set this to your absolute maximum date
            if ($("#" + FechaInicio).datepicker("getDate") != null) {
                dateMin = $("#" + FechaInicio).datepicker("getDate");
                var rangeMax = new Date(dateMin.getFullYear(), dateMin.getMonth(), dateMin.getDate() + dayRange);

                if (rangeMax < dateMax) {
                    dateMax = rangeMax;
                }
            }
        }
        return {
            minDate: dateMin,
            maxDate: dateMax
        };
    },

    ValidarCombo: function (args) {
        var isvalid = true;
        $.each(args, function (index, item) {
            var combo = jQuery('#' + item);
            var valor = combo.val();

            if (valor == 0 || valor == null) {
                var padre = combo.parent();
                var haySpan = padre.find("span").length;

                if (haySpan == 0) {
                    padre.append("<span class='error'>*</span>");
                }
                isvalid = false;
            }
        });
        return isvalid;
    },

    ShowAlert: function (dialog, mensaje) {
        if (dialog == '') {
            dialog = 'dialog-alert';
        }

        $('#' + dialog).html("");
        $('#' + dialog).append("<br/>" + mensaje);
        $('#' + dialog).dialog("open");
    },

    CreateDialogs: function (arrayDialog) {
        for (var i = 0; i < arrayDialog.length; i++) {
            $("#" + arrayDialog[i].name).dialog({
                autoOpen: false,
                resizable: false,
                height: arrayDialog[i].height,
                width: arrayDialog[i].width,
                title: arrayDialog[i].title,
                modal: true,
                open: function () {
                    $(this).parent().appendTo($('#aspnetForm'));
                }
            });
        }
    },
    CreateDialogsConfirm: function (arrayDialog) {
        for (var i = 0; i < arrayDialog.length; i++) {
            $("#" + arrayDialog[i].name).dialog({
                autoOpen: false,
                resizable: false,
                height: arrayDialog[i].height,
                width: arrayDialog[i].width,
                title: arrayDialog[i].title,
                modal: true,
                open: function () {
                    $(this).parent().appendTo($('#aspnetForm'));
                },
                buttons: [
                        {
                            text: arrayDialog[i].titleBtn1,
                            click: function () {
                                var name = $(this).attr('id');
                                $.each(arrayDialog, function (index, v) {
                                    if (v.name == name) {
                                        var fun = window[v.strFun];
                                        fun();
                                        return;
                                    }
                                });
                            }
                        },
                        {
                            text: arrayDialog[i].titleBtn2,
                            click: function () {
                                $(this).dialog("close");
                            }
                        }
                ]
            });
        }
    },
    CulturaGrilla: function (idioma) {
        langShort = idioma.split('-')[0].toLowerCase();
        if ($.jgrid.hasOwnProperty("regional") && $.jgrid.regional.hasOwnProperty(idioma)) {
            $.extend($.jgrid, $.jgrid.regional[idioma]);
        } else if ($.jgrid.hasOwnProperty("regional") && $.jgrid.regional.hasOwnProperty(langShort)) {
            $.extend($.jgrid, $.jgrid.regional[langShort]);
        }
    },
    Grilla: function (grilla, pager, identificador, height, width, caption, urlListar, id, colsNames, colsModel, sortName, opciones) {
        var grid = jQuery('#' + grilla);
        var estadoSubGrid = false;

        if (opciones.noregistro == null) { noregistro = true; }
        if (opciones.sort == null) { opciones.sort = 'desc'; }
        if (opciones.subGrid != null) { estadoSubGrid = true; }
        if (opciones.rowNumber == null) { opciones.rowNumber = 15; }
        if (opciones.rowNumbers == null) { opciones.rowNumbers = [opciones.rowNumber, 20, 50, 100, 150]; }
        if (opciones.rules == null) { opciones.rules = false; }
        if (opciones.search == null) { opciones.search = false; }
        if (opciones.footerrow == null) { opciones.footerrow = false; }
        if (opciones.multiselect == null) { opciones.multiselect = false; }
        if (opciones.agregarBotones == null) { opciones.agregarBotones = false; }
        if (opciones.GridLocal == null) {
            opciones.GridLocal = false;

            if (opciones.CellEdit == null) {
                opciones.CellEdit = false;
            }
            if (opciones.grouping == null) {
                opciones.grouping = false;
            }
        }

        if (opciones.NuevoCaption == null) opciones.NuevoCaption = "Nuevo";
        if (opciones.EditarCaption == null) opciones.EditarCaption = "Editar";
        if (opciones.EliminarCaption == null) opciones.EliminarCaption = "Eliminar";
        if (opciones.LeyendaCaption == null) opciones.LeyendaCaption = "Leyenda";

        if (opciones.Lenguaje == null) opciones.Lenguaje = "es";

        if (opciones.Lenguaje == "en") {
            if (opciones.dialogDelete == null) { opciones.dialogDelete = 'Delete'; }
            if (opciones.dialogAlert == null) { opciones.dialogAlert = 'Alert'; }
        }
        else if (opciones.Lenguaje == "es") {
            if (opciones.dialogDelete == null) { opciones.dialogDelete = 'Eliminar'; }
            if (opciones.dialogAlert == null) { opciones.dialogAlert = 'Alerta'; }
        }
        //alert(opciones.Lenguaje);

        langShort = opciones.Lenguaje.split('-')[0].toLowerCase();
        if ($.jgrid.hasOwnProperty("regional") && $.jgrid.regional.hasOwnProperty(opciones.Lenguaje)) {
            $.extend($.jgrid, $.jgrid.regional[opciones.Lenguaje]);
        } else if ($.jgrid.hasOwnProperty("regional") && $.jgrid.regional.hasOwnProperty(langShort)) {
            $.extend($.jgrid, $.jgrid.regional[langShort]);
        }

        //opciones.selectFunc = (opciones.selectFunc == null) ? false : opciones.OnSelect;

        var rowKey;
        var lasRowKey;

        if (!opciones.GridLocal) {
        
            $('#' + grilla).jqGrid({
                prmNames: {
                    search: 'isSearch',
                    nd: null,
                    rows: 'rows',
                    page: 'page',
                    sort: 'sortField',
                    order: 'sortOrder',
                    filters: 'filters'
                },

                postData: { searchString: '', searchField: '', searchOper: '', filters: '' },
                jsonReader: {
                    root: 'rows',
                    page: 'page',
                    total: 'total',
                    records: 'records',
                    cell: 'cell',
                    id: id, //index of the column with the PK in it
                    userdata: 'userdata',
                    repeatitems: true,
                    scroll: true
                },
                rowNum: opciones.rowNumber,
                rowList: opciones.rowNumbers,
                pager: '#' + pager,
                sortname: sortName,
                viewrecords: true,
                multiselect: opciones.multiselect,
                rownumbers: true,
                sortorder: opciones.sort,
                height: height,
                footerrow: opciones.footerrow,
                //loadonce: true,
                shrinkToFit: false,
                width: width,
                autowidth: false,
                colNames: colsNames,
                colModel: colsModel,
                caption: caption,
                subGrid: estadoSubGrid,
                editurl: opciones.editurl,
                subGridRowColapsed: function (subgrid_id, row_id) {
                    //if (opciones.subGrid != null)
                    //SubGrid.subGridRowColapsed(subgrid_id, row_id,0);
                    var subgrid_table_id, pager_id;
                    subgrid_table_id = subgrid_id + "_t";
                    pager_id = "p_" + subgrid_table_id;
                    jQuery("#" + subgrid_table_id).remove();
                    jQuery("#" + pager_id).remove();
                },
                subGridRowExpanded: function (subgrid_id, row_id) {
                    $('#hdf_id_grilla').val(row_id);
       
                    //if (opciones.subGrid!=null)
                    //SubGrid.subGridRowExpandedAnexosHijos(subgrid_id, row_id, opciones.subGrid, 0);
                    var subGrid = opciones.subGrid;

                    var subgrid_table_id, pager_id;
                    subgrid_table_id = subgrid_id + "_t";
                    pager_id = "p_" + subgrid_table_id;

                    $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pager_id + "' class='scroll'></div>");
                    
                    var parameters = { ID_DETALLE: row_id };
                    $.ajax({
                        type: "POST",
                        url: subGrid.Url,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(parameters),
                        success: function (rsp) {
                            if (rsp.OBJETO.length > 0) {
                                var data = (typeof rsp.d) == 'string' ? eval('(' + rsp.d + ')') : rsp.d;
                                $("#" + subgrid_table_id).jqGrid({
                                    datatype: "local",
                                    colNames: subGrid.ColNames,
                                    colModel: subGrid.ColModels,
                                    rowNum: 10,
                                    rowList: [10, 20, 50, 100],
                                    sortorder: "desc",
                                    viewrecords: true,
                                    rownumbers: true,
                                    pager: "#" + pager_id,
                                    loadonce: true,
                                    sortable: true,
                                    height: subGrid.Height,
                                    width: subGrid.Width
                                });

                                //for (var i = 0; i <= data.length; i++)
                                //    jQuery("#" + subgrid_table_id).jqGrid('addRowData', i + 1, data[i]);
                                //$("#" + subgrid_table_id).trigger("reloadGrid");

                                jQuery("#" + subgrid_table_id).jqGrid('clearGridData', true).trigger("reloadGrid");
                                if (rsp.EJECUCION_PROCEDIMIENTO) {
                                    if (!rsp.RECHAZAR) {
                                        var max_tamanio = false; 
                                        var count_list = rsp.OBJETO.length;
                                        if (count_list >= 3)
                                            max_tamanio = true; 
                                        //$.each(rsp.OBJETO, function (i, v) {
                                        for (var i = 0; i <= rsp.OBJETO.length; i++) {
                                            var idgrilla = i + 1;
                                            jQuery("#" + subgrid_table_id).jqGrid('addRowData', idgrilla, rsp.OBJETO[i]);
                                        }
                                        jQuery("#" + subgrid_table_id).trigger("reloadGrid");
                                        //   $('.ui-jqgrid-bdiv').css("height", "400px !important")
                                       // if(max_tamanio) 
                                       //jQuery("#" + subgrid_table_id).setGridHeight(400);
                                    }
                                } else {
                                    jAlert(rsp.MENSAJE_SALIDA, "Atención");
                                }
                                $("#" + subgrid_table_id).filterToolbar({ searchOnEnter: true, stringResult: false, defaultSearch: "cn" });
                            }
                        },
                        failure: function (msg) {
                            $('#mensajeFalla').show().fadeOut(8000);
                        }
                    });
                },

                ondblClickRow: function (rowid) {
                    if (opciones.search) {
                        var ret = grid.getRowData(rowid);
                        SelectRow(ret);
                    }
                },
                onSelectRow: function (id, isSelected) {
                    debugger; 
                    rowKey = grid.getGridParam('selrow');

                    if (rowKey != null) {
                        $("#" + identificador).val(rowKey);
                    }
                    if (opciones.selectRowFunc != null) {
                        if (typeof (opciones.selectRowFunc) == 'function') { opciones.selectRowFunc(id, isSelected) }
                    }
                },
                onSelectAll: function (id, isSelected) {
                    rowKey = grid.getGridParam('selrow');
                    if (rowKey != null) {
                        $("#" + identificador).val(rowKey);
                    }
                    if (opciones.selectRowFunc != null) {
                        if (typeof (opciones.selectAllFunc) == 'function') { opciones.selectAllFunc(id, isSelected) }
                    }
                },
                //loadComplete: function () {
                //    if (opciones.loadCompleteFunc != null) {
                //        if (typeof (opciones.selectAllFunc) == 'function') { opciones.loadCompleteFunc() }
                //    }
                //},
                gridComplete: function () {
                    if ($('#' + grilla).getGridParam('records') == 0) {
                        if (opciones.noregistro == true) {
                            SICA.ShowAlert("dialog-alert", "Sin Registro");
                        }
                    }


                    rowKey = null;
                    if (opciones.agregarBotones == true) {
                        AgregarBotones();
                    }

                    if (opciones.gridCompleteFunc != null) {
                        opciones.gridCompleteFunc();
                        //if (typeof (opciones.selectAllFunc) == 'function') { opciones.gridCompleteFunc() }
                    }
                },
                //gridComplete: function () {
                //    if ($('#' + grilla).getGridParam('records') == 0) {
                //        if (opciones.noregistro == true) {
                //            SICA.ShowAlert("dialog-alert", "Sin Registro");
                //        }
                //    }


                //    rowKey = null;
                //    if (opciones.agregarBotones == true) {
                //        AgregarBotones();
                //    }

                //    if (opciones.gridCompleteFunc != null) {
                //        if (typeof (opciones.selectAllFunc) == 'function') { opciones.gridCompleteFunc() }
                //    }
                //},
                ondblClickRow: function () {
                    if (typeof opciones.GridondblClickRowHandler == "function")
                        opciones.GridondblClickRowHandler();

                    if (jQuery.isFunction(opciones.BeforeEditHandler)) {
                        opciones.BeforeEditHandler(grid, rowKey);
                        grid.editRow(rowKey, true, true, true, null, null,
                           function (rowID, responseServer) {
                               AfterSaveRowInline(rowID, grid);
                           });
                        lasRowKey = rowKey;
                    }
                },
                datatype: function (postdata) {
                    var migrilla = new Object();
                    migrilla.page = postdata.page;
                    migrilla.rows = postdata.rows;
                    migrilla.sidx = postdata.sortField;
                    migrilla.sord = postdata.sortOrder;
                    migrilla._search = postdata.isSearch;
                    migrilla.filters = postdata.filters;
                    if (opciones.rules != false) {
                        migrilla.Rules = GetRules(grilla);
                    }

                    if (migrilla._search == true) {
                        migrilla.searchField = postdata.searchField;
                        migrilla.searchOper = postdata.searchOper;
                        migrilla.searchString = postdata.searchString;
                    }
                    migrilla.usu = $("#input_hdidusuario").val();
                    migrilla.ofi = $("#input_hdidoficina").val();
                    migrilla.perfil = $("#input_hdidperfil").val();
                    var params = { grid: migrilla };

                    $.ajax({
                        url: urlListar,
                        type: 'post',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(params),
                        async: false,
                        success: function (data, st) {
                            if (st == 'success') {
                                var jq = $('#' + grilla)[0];
                                jq.addJSONData(data);
                            }
                        },
                        error: function (a, b, c) {
                            alert('Error with AJAX callback');
                        }
                    });
                }
            }).navGrid("#" + pager, { edit: false, add: false, del: false, search: opciones.search, refresh: false },
                {}, // use default settings for edit
                {}, // use default settings for add
                {}, // delete instead that del:false we need this
                {
                    multipleSearch: true,
                    beforeShowSearch: function () {
                        $(".ui-reset").trigger("click");
                        return true;
                    }
                }
            );

        } // fin de NO GridLocal
        else if (opciones.GridLocal) {
            $('#' + grilla).jqGrid({
                datatype: "local",
                colNames: colsNames,
                colModel: colsModel,
                sortorder: opciones.sort,
                rowNum: opciones.rowNumber,
                rownumbers: true,
                cellEdit: opciones.CellEdit,
                cellsubmit: "clientArray",
                rowList: opciones.rowNumbers,
                pager: '#' + pager,
                sortname: sortName,
                viewrecords: true,
                multiselect: opciones.multiselect,
                sortorder: opciones.sort,
                footerrow: opciones.footerrow,
                height: height,
                width: width,
                gridview: true,
                loadonce: true,
                //autowidth: true,
                //shrinkToFit: fals,
                //forceFit: true,
                shrinkToFit: false,
                //colNames: colsNames,
                //colModel: colsModel,
                grouping: opciones.grouping,
                groupingView: {
                    groupField: [opciones.groupingCampo],
                    // groupDataSorted: true,
                    groupColumnShow: false,
                    //  groupCollapse: true,
                    // groupText : ['<b>{0} - {1} Postor(s)</b>'],
                    //groupText: ['<b>{0}</b></div><input type = "button" class = "button" value = "NEW" id = "btnNew" style = "width:100px; hieght:10px" onclick = "javascript:AlertaxD(rowObject)" /><<b>{1} Orders</b>']
                    groupText: ['<b style="background-color:#FFFFDC"> {0} - {1} Documento(s) </b></div>' + opciones.groupfuncionMov + opciones.groupfuncionEstado],
                    //plusicon: 'bullet_toggle_plus',
                    //minusicon: 'bullet_toggle_minus'
                },
                caption: caption,
                subGrid: estadoSubGrid,
                editurl: opciones.editurl,
                subGridRowColapsed: function (subgrid_id, row_id) {
                    var subgrid_table_id, pager_id;
                    subgrid_table_id = subgrid_id + "_t";
                    pager_id = "p_" + subgrid_table_id;
                    jQuery("#" + subgrid_table_id).remove();
                    jQuery("#" + pager_id).remove();
                },
                subGridRowExpanded: function (subgrid_id, row_id) {
                    var subGrid = opciones.subGrid;
                    //debugger;
                    var subgrid_table_id, pager_id;
                    subgrid_table_id = subgrid_id + "_t";
                    pager_id = "p_" + subgrid_table_id;

                    $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pager_id + "' class='scroll'></div>");
                    
                    var datax = jQuery("#" + grilla).jqGrid('getRowData', row_id);
                    var parameters = {
                        ID_DOCUMENTO_USUARIO: datax.ID_DOCUMENTO_USUARIO,
                        ID_DOCUMENTO: datax.ID_DOCUMENTO_PADRE,
                        ID_ESTADO_DOCUMENTO: datax.ID_ESTADO_DOCUMENTO,
                        ID_OFICINA: $("#input_hdidoficina").val(), //ID_OFICINA,
                        ID_USUARIO: $("#input_hdidusuario").val(),
                        ID_PERFIL: $("#input_hdidperfil").val()
                    };
                    $.ajax({
                        type: "POST",
                        url: subGrid.Url,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(parameters),
                        success: function (rsp) {
                            debugger;
                            var data = rsp.OBJETO; // (typeof rsp.d) == 'string' ? eval('(' + rsp.d + ')') : rsp.d;
                            $("#" + subgrid_table_id).jqGrid({
                                datatype: "local",
                                colNames: subGrid.ColNames,
                                colModel: subGrid.ColModels,
                                rowNum: 10,
                                rowList: [10, 20, 50, 100],
                                //sortorder: "desc",
                                viewrecords: true,
                                rownumbers: false,
                                pager: "#" + pager_id,
                                loadonce: true,
                                shrinkToFit: false,
                                sortable: true,
                                height: subGrid.Height,
                                width: width
                            });

                            for (var i = 0; i <= data.length; i++)
                                jQuery("#" + subgrid_table_id).jqGrid('addRowData', i + 1, data[i]);
                            $("#" + subgrid_table_id).trigger("reloadGrid");
                        },
                        failure: function (msg) {
                            $('#mensajeFalla').show().fadeOut(8000);
                        }
                    });
                },
                beforeSelectRow: function (rowid, e) {
                    var valor = false;
                    var rowKey = grid.getGridParam('selrow');

                    var i = $.jgrid.getCellIndex($(e.target).closest('td')[0]),
                        cm = grid.jqGrid('getGridParam', 'colModel');

                    if (cm[i].name === 'cb') {
                        valor = true;

                    } else {

                        valor = false;
                        var s, cantidad, l, ant = 0;
                        s = grid.jqGrid('getGridParam', 'selarrrow');
                        cantidad = s.length;

                        l = s;
                        if (cantidad > 1) {
                            for (var i = 0, il = cantidad; i < il; i++) {
                                grid.jqGrid('setSelection', l[ant], false);
                            }
                            grid.jqGrid('setSelection', rowid, true);
                        } else {

                            grid.jqGrid('setSelection', rowKey, false);
                            grid.jqGrid('setSelection', rowid, true);
                        }

                    }


                    return valor;
                },
                gridComplete: function () {

                    if (typeof opciones.GridCompleteHandler == "function")
                        opciones.GridCompleteHandler();
                    // if (opciones.EditingOptions != null && opciones.EditingOptions.canEditRowInline) {
                    if (jQuery.isFunction(opciones.GridCompleteHandlerGrid))
                        opciones.GridCompleteHandlerGrid(grid);
                    // }
                },
                loadComplete: function () {

                    if (typeof opciones.loadCompleteHandler == "function")
                        opciones.loadCompleteHandler();
                },
                ondblClickRow: function () {
                    if (typeof opciones.GridondblClickRowHandler == "function")
                        opciones.GridondblClickRowHandler();

                    if (jQuery.isFunction(opciones.BeforeEditHandler)) {
                        opciones.BeforeEditHandler(grid, rowKey);
                        grid.editRow(rowKey, true, true, true, null, null,
                           function (rowID, responseServer) {
                               AfterSaveRowInline(rowID, grid);
                           });
                        lasRowKey = rowKey;
                    }
                },
                //BeforeEditHandler: function () {
                //    if (jQuery.isFunction(opciones.BeforeEditHandler)) {
                //        opciones.BeforeEditHandler(grid, rowKey);
                //        grid.editRow(rowKey, true, true, true, null, null,
                //           function (rowID, responseServer) {
                //          AfterSaveRowInline(rowID, grid);
                //           });
                //        lasRowKey = rowKey;
                //    }               
                //},
                beforeSaveCell: function (rowid, celname, value, iRow, iCol) {
                    if (opciones.BeforeEditHandler != null)
                        opciones.BeforeEditHandler(rowid, cellname, value, iRow, iCol);
                },
                afterSaveCell: function (rowid, cellname, value, iRow, iCol) {
                    if (opciones.AfterSaveCellHandler != null)
                        opciones.AfterSaveCellHandler(rowid, cellname, value, iRow, iCol);
                },
                onSelectRow: function () {
                    debugger;
                    rowKey = grid.getGridParam('selrow');

                    if (rowKey != null) {
                        $("#" + identificador).val(rowKey);
                    }
                    if (opciones.selectRowFunc != null) {
                        if (typeof (opciones.selectRowFunc) == 'function') {
                            opciones.selectRowFunc(rowKey)
                        }
                    }
                }

            }).navGrid("#" + pager, { edit: false, add: false, del: false, search: opciones.search, refresh: false },
                {}, // use default settings for edit
                {}, // use default settings for add
                {}, // delete instead that del:false we need this
                {
                    multipleSearch: true,
                    beforeShowSearch: function () {
                        $(".ui-reset").trigger("click");
                        return true;
                    }
                }
            );

        } // fin de GridLocal

        if (opciones.eliminar) {
            $('#' + grilla).navButtonAdd('#' + pager, {
                caption: opciones.btnEliminar != null ? opciones.btnEliminar.Caption : opciones.EliminarCaption,
                title: opciones.btnEliminar != null ? opciones.btnEliminar.Caption : opciones.EliminarCaption,
                buttonicon: 'ui-icon-trash',
                position: 'first',
                onClickButton: function () {
                    if (rowKey != null) {
                        if (opciones.btnEliminar == null) {
                            SICA.confirm(opciones.Mensaje, function () { Eliminar(); }, '', opciones.dialogDelete, opciones.Lenguaje);
                        }
                        else {
                            if (opciones.btnEliminar.Mensaje != null && opciones.btnEliminar.Mensaje != '') {
                                SICA.confirm(opciones.btnEliminar.Mensaje, function () { opciones.btnEliminar.Function(rowKey); }, '', opciones.btnEliminar.titleMensaje, opciones.Lenguaje);
                            }
                            else {
                                opciones.btnEliminar.Function(rowKey);
                            }
                        }
                    } else {
                        if (opciones.Alert == null) {
                            SICA.Alert(opciones.dialogAlert, opciones.Mensaje, '', opciones.Lenguaje);
                        }
                        else {
                            SICA.Alert(opciones.Alert.titleMensaje, opciones.Alert.Mensaje, '', opciones.Lenguaje);
                        }
                    }
                }
            });
        }

        if (opciones.editar) {
            $('#' + grilla).navButtonAdd('#' + pager, {
                caption: opciones.EditarCaption,
                title: opciones.EditarCaption,
                buttonicon: 'ui-icon-pencil',
                position: 'first',
                onClickButton: function () {
                    if (rowKey != null) {
                        if (opciones.btnEditar == null) {
                            Editar(rowKey);
                        }
                        else {
                            opciones.btnEditar.Function(rowKey);
                        }
                    } else {
                        if (opciones.Alert == null) {
                            SICA.Alert(opciones.dialogAlert, opciones.Mensaje, '', opciones.Lenguaje);
                        }
                        else {
                            SICA.Alert(opciones.Alert.titleMensaje, opciones.Alert.Mensaje, '', opciones.Lenguaje);
                        }
                    }
                }
            });
        }
        if (opciones.leyenda) {
            $('#' + grilla).navButtonAdd('#' + pager, {
                caption: opciones.LeyendaCaption,

                title: opciones.LeyendaCaption,
                buttonicon: 'ui-icon-search',
                position: 'first',
                id: "L" + grilla,
                onClickButton: function () {
                    opciones.verLeyenda();
                    //if (opciones.btnNuevo == null) {
                    //    Nuevo(rowKey);
                    //}
                    //else {
                    //    opciones.btnNuevo.Function(rowKey);
                    //}
                }
            });
        }
        if (opciones.nuevo) {
            $('#' + grilla).navButtonAdd('#' + pager, {
                caption: opciones.NuevoCaption,
                title: opciones.NuevoCaption,
                buttonicon: 'ui-icon-plus',
                position: 'first',
                onClickButton: function () {
                    if (opciones.btnNuevo == null) {
                        Nuevo(rowKey);
                    }
                    else {
                        opciones.btnNuevo.Function(rowKey);
                    }
                }
            });
        }
        if (opciones.exportar) {
            $('#' + grilla).navButtonAdd('#' + pager, {
                caption: 'Exportar a excel',
                title: 'Exportar a excel',
                buttonicon: 'ui-icon-arrowreturnthick-1-s',
                position: 'first',
                id: "E" + grilla,
                onClickButton: function () {
                    opciones.exportarExcel('#' + grilla);
                    //if (opciones.btnNuevo == null) {
                    //    Nuevo(rowKey);
                    //}
                    //else {
                    //    opciones.btnNuevo.Function(rowKey);
                    //}
                }
            });
        }
        var funccion = 'SICA.LayoutBarra("' + pager.toString() + '")';
        setTimeout(funccion, 1000);
    },
    GrillaEditable: function (grilla, pager, identificador, height, width, caption, urlListar, id, colsNames, colsModel, sortName, opciones) {
        var grid = jQuery('#' + grilla);
        var estadoSubGrid = false;



        if (opciones.beforeBoolean == null) { beforeBoolean = false; }
        if (opciones.noregistro == null) { noregistro = true; }
        if (opciones.sort == null) { opciones.sort = 'desc'; }
        if (opciones.subGrid != null) { estadoSubGrid = true; }
        if (opciones.rowNumber == null) { opciones.rowNumber = 70; }
        if (opciones.rowNumbers == null) { opciones.rowNumbers = [opciones.rowNumber, 70, 100, 150]; }
        if (opciones.rules == null) { opciones.rules = false; }
        if (opciones.search == null) { opciones.search = false; }
        if (opciones.footerrow == null) { opciones.footerrow = false; }
        if (opciones.multiselect == null) { opciones.multiselect = false; }
        if (opciones.agregarBotones == null) { opciones.agregarBotones = false; }
        if (opciones.GridLocal == null) {
            opciones.GridLocal = false;

            if (opciones.CellEdit == null) {
                opciones.CellEdit = false;
            }
            if (opciones.grouping == null) {
                opciones.grouping = false;
            }
        }

        if (opciones.NuevoCaption == null) opciones.NuevoCaption = "Nuevo";
        if (opciones.EditarCaption == null) opciones.EditarCaption = "Editar";
        if (opciones.EliminarCaption == null) opciones.EliminarCaption = "Eliminar";
        if (opciones.LeyendaCaption == null) opciones.LeyendaCaption = "Ver Leyenda";

        if (opciones.Lenguaje == null) opciones.Lenguaje = "es";

        if (opciones.Lenguaje == "en") {
            if (opciones.dialogDelete == null) { opciones.dialogDelete = 'Delete'; }
            if (opciones.dialogAlert == null) { opciones.dialogAlert = 'Alert'; }
        }
        else if (opciones.Lenguaje == "es") {
            if (opciones.dialogDelete == null) { opciones.dialogDelete = 'Eliminar'; }
            if (opciones.dialogAlert == null) { opciones.dialogAlert = 'Alerta'; }
        }
        //alert(opciones.Lenguaje);

        langShort = opciones.Lenguaje.split('-')[0].toLowerCase();
        if ($.jgrid.hasOwnProperty("regional") && $.jgrid.regional.hasOwnProperty(opciones.Lenguaje)) {
            $.extend($.jgrid, $.jgrid.regional[opciones.Lenguaje]);
        } else if ($.jgrid.hasOwnProperty("regional") && $.jgrid.regional.hasOwnProperty(langShort)) {
            $.extend($.jgrid, $.jgrid.regional[langShort]);
        }

        //opciones.selectFunc = (opciones.selectFunc == null) ? false : opciones.OnSelect;

        var rowKey;
        var lasRowKey;

        if (!opciones.GridLocal) {
            $('#' + grilla).jqGrid({
                prmNames: {
                    search: 'isSearch',
                    nd: null,
                    rows: 'rows',
                    page: 'page',
                    sort: 'sortField',
                    order: 'sortOrder',
                    filters: 'filters'
                },

                postData: { searchString: '', searchField: '', searchOper: '', filters: '' },
                jsonReader: {
                    root: 'rows',
                    page: 'page',
                    total: 'total',
                    records: 'records',
                    cell: 'cell',
                    id: id, //index of the column with the PK in it
                    userdata: 'userdata',
                    repeatitems: true,
                    scroll: true
                },
                rowNum: opciones.rowNumber,
                rowList: opciones.rowNumbers,
                pager: '#' + pager,
                sortname: sortName,
                viewrecords: true,
                multiselect: opciones.multiselect,
                rownumbers: true,
                sortorder: opciones.sort,
                height: height,
                footerrow: opciones.footerrow,
                //loadonce: true,
                shrinkToFit: false,
                width: width,
                autowidth: false,
                colNames: colsNames,
                colModel: colsModel,
                caption: caption,
                subGrid: estadoSubGrid,
                editurl: opciones.editurl,
                subGridRowColapsed: function (subgrid_id, row_id) {
                    var subgrid_table_id, pager_id;
                    subgrid_table_id = subgrid_id + "_t";
                    pager_id = "p_" + subgrid_table_id;
                    jQuery("#" + subgrid_table_id).remove();
                    jQuery("#" + pager_id).remove();
                },
                subGridRowExpanded: function (subgrid_id, row_id) {
                    var subGrid = opciones.subGrid;

                    var subgrid_table_id, pager_id;
                    subgrid_table_id = subgrid_id + "_t";
                    pager_id = "p_" + subgrid_table_id;

                    $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pager_id + "' class='scroll'></div>");

                    var parameters = { cDocNro: row_id };
                    $.ajax({
                        type: "POST",
                        url: subGrid.Url,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(parameters),
                        success: function (rsp) {
                            var data = (typeof rsp.d) == 'string' ? eval('(' + rsp.d + ')') : rsp.d;
                            $("#" + subgrid_table_id).jqGrid({
                                datatype: "local",
                                colNames: subGrid.ColNames,
                                colModel: subGrid.ColModels,
                                rowNum: 10,
                                rowList: [10, 20, 50, 100],
                                sortorder: "desc",
                                viewrecords: true,
                                rownumbers: true,
                                pager: "#" + pager_id,
                                loadonce: true,
                                sortable: true,
                                height: subGrid.Height,
                                width: subGrid.Width
                            });

                            for (var i = 0; i <= data.length; i++)
                                jQuery("#" + subgrid_table_id).jqGrid('addRowData', i + 1, data[i]);
                            $("#" + subgrid_table_id).trigger("reloadGrid");
                        },
                        failure: function (msg) {
                            $('#mensajeFalla').show().fadeOut(8000);
                        }
                    });
                },

                ondblClickRow: function (rowid) {
                    if (opciones.search) {
                        var ret = grid.getRowData(rowid);
                        SelectRow(ret);
                    }
                },

                onSelectRow: function (id, isSelected) {
                    rowKey = grid.getGridParam('selrow');
                    if (rowKey != null) {
                        $("#" + identificador).val(rowKey);
                    }
                    if (opciones.selectRowFunc != null) {
                        if (typeof (opciones.selectRowFunc) == 'function') { opciones.selectRowFunc(id, isSelected) }
                    }
                },

                onSelectAll: function (id, isSelected) {
                    rowKey = grid.getGridParam('selrow');
                    if (rowKey != null) {
                        $("#" + identificador).val(rowKey);
                    }
                    if (opciones.selectRowFunc != null) {
                        if (typeof (opciones.selectAllFunc) == 'function') { opciones.selectAllFunc(id, isSelected) }
                    }
                },

                //loadComplete: function () {
                //    if (opciones.loadCompleteFunc != null) {
                //        if (typeof (opciones.selectAllFunc) == 'function') { opciones.loadCompleteFunc() }
                //    }
                //},
                gridComplete: function () {
                    if ($('#' + grilla).getGridParam('records') == 0) {
                        if (opciones.noregistro == true) {
                            SICA.ShowAlert("dialog-alert", "Sin Registro");
                        }
                    }


                    rowKey = null;
                    if (opciones.agregarBotones == true) {
                        AgregarBotones();
                    }

                    if (opciones.gridCompleteFunc != null) {
                        if (typeof (opciones.selectAllFunc) == 'function') { opciones.gridCompleteFunc() }
                    }
                },
                ondblClickRow: function () {
                    if (typeof opciones.GridondblClickRowHandler == "function")
                        opciones.GridondblClickRowHandler();

                    if (jQuery.isFunction(opciones.BeforeEditHandler)) {
                        opciones.BeforeEditHandler(grid, rowKey);
                        grid.editRow(rowKey, true, true, true, null, null,
                           function (rowID, responseServer) {
                               AfterSaveRowInline(rowID, grid);
                           });
                        lasRowKey = rowKey;
                    }
                },
                datatype: function (postdata) {
                    var migrilla = new Object();
                    migrilla.page = postdata.page;
                    migrilla.rows = postdata.rows;
                    migrilla.sidx = postdata.sortField;
                    migrilla.sord = postdata.sortOrder;
                    migrilla._search = postdata.isSearch;
                    migrilla.filters = postdata.filters;
                    if (opciones.rules != false) {
                        migrilla.Rules = GetRules(grilla);
                    }

                    if (migrilla._search == true) {
                        migrilla.searchField = postdata.searchField;
                        migrilla.searchOper = postdata.searchOper;
                        migrilla.searchString = postdata.searchString;
                    }

                    var params = { grid: migrilla };

                    $.ajax({
                        url: urlListar,
                        type: 'post',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(params),
                        async: false,
                        success: function (data, st) {
                            if (st == 'success') {
                                var jq = $('#' + grilla)[0];
                                jq.addJSONData(data);
                            }
                        },
                        error: function (a, b, c) {
                            alert('Error with AJAX callback');
                        }
                    });
                }
            }).navGrid("#" + pager, { edit: false, add: false, del: false, search: opciones.search, refresh: false },
                {}, // use default settings for edit
                {}, // use default settings for add
                {}, // delete instead that del:false we need this
                {
                    multipleSearch: true,
                    beforeShowSearch: function () {
                        $(".ui-reset").trigger("click");
                        return true;
                    }
                }
            );

        } // fin de NO GridLocal
        else if (opciones.GridLocal) {
            $('#' + grilla).jqGrid({
                datatype: "local",
                colNames: colsNames,
                colModel: colsModel,
                sortorder: opciones.sort,
                rowNum: opciones.rowNumber,
                rownumbers: true,
                cellEdit: opciones.CellEdit,
                cellsubmit: "clientArray",
                rowList: opciones.rowNumbers,
                pager: '#' + pager,
                sortname: sortName,
                viewrecords: true,
                multiselect: opciones.multiselect,
                sortorder: opciones.sort,
                footerrow: opciones.footerrow,
                height: height,
                width: width,
                gridview: true,
                loadonce: true,
                //toolbar: [true,"top"],
                //autowidth: true,
                shrinkToFit: false,
                //forceFit: true,
                //shrinkToFit: false,
                //colNames: colsNames,
                //colModel: colsModel,
                grouping: opciones.grouping,
                groupingView: {
                    groupField: [opciones.groupingCampo],
                    // groupDataSorted: true,
                    groupColumnShow: false,
                    //,
                    //  groupCollapse: true,
                    // groupText : ['<b>{0} - {1} Postor(s)</b>'],
                    groupText: ['<b style="background-color:#FFFFDC"> {0} - {1} Documento(s) </b>'],
                    //plusicon: 'clip-checkbox',
                    //minusicon: 'ui-icon-circlesmall-minus'
                },
                caption: caption,
                subGrid: estadoSubGrid, 
                //editurl: opciones.editurl,
                //subGrid: estadoSubGrid,
                editurl: opciones.editurl,
                subGridRowColapsed: function (subgrid_id, row_id) {
                    var subgrid_table_id, pager_id;
                    subgrid_table_id = subgrid_id + "_t";
                    pager_id = "p_" + subgrid_table_id;
                    jQuery("#" + subgrid_table_id).remove();
                    jQuery("#" + pager_id).remove();
                },
                subGridRowExpanded: function (subgrid_id, row_id) {
                    var subGrid = opciones.subGrid;
                    debugger;
                    var subgrid_table_id, pager_id;
                    subgrid_table_id = subgrid_id + "_t";
                    pager_id = "p_" + subgrid_table_id;

                    $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pager_id + "' class='scroll'></div>");

                    var parameters = { cDocNro: row_id };
                    $.ajax({
                        type: "POST",
                        url: subGrid.Url,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(parameters),
                        success: function (rsp) {
                            var data = (typeof rsp.d) == 'string' ? eval('(' + rsp.d + ')') : rsp.d;
                            $("#" + subgrid_table_id).jqGrid({
                                datatype: "local",
                                colNames: subGrid.ColNames,
                                colModel: subGrid.ColModels,
                                rowNum: 10,
                                rowList: [10, 20, 50, 100],
                                sortorder: "desc",
                                viewrecords: true,
                                rownumbers: true,
                                pager: "#" + pager_id,
                                loadonce: true,
                                sortable: true,
                                height: subGrid.Height,
                                width: subGrid.Width
                            });

                            for (var i = 0; i <= data.length; i++)
                                jQuery("#" + subgrid_table_id).jqGrid('addRowData', i + 1, data[i]);
                            $("#" + subgrid_table_id).trigger("reloadGrid");
                        },
                        failure: function (msg) {
                            $('#mensajeFalla').show().fadeOut(8000);
                        }
                    });
                },

                //editurl: opciones.editurl,
                gridComplete: function () {
                    if (typeof opciones.GridCompleteHandler == "function")
                        opciones.GridCompleteHandler();
                    // if (opciones.EditingOptions != null && opciones.EditingOptions.canEditRowInline) {
                    if (jQuery.isFunction(opciones.GridCompleteHandlerGrid))
                        opciones.GridCompleteHandlerGrid(grid);
                    // }
                },
                loadComplete: function () {
                    if (typeof opciones.loadCompleteHandler == "function") {

                        opciones.loadCompleteHandler();
                    }
                },
                ondblClickRow: function () {
                    if (typeof opciones.GridondblClickRowHandler == "function")
                        opciones.GridondblClickRowHandler();

                    if (jQuery.isFunction(opciones.BeforeEditHandler)) {
                        opciones.BeforeEditHandler(grid, rowKey);
                        grid.editRow(rowKey, true, true, true, null, null,
                           function (rowID, responseServer) {
                               AfterSaveRowInline(rowID, grid);
                           });
                        lasRowKey = rowKey;
                    }
                },
                //BeforeEditHandler: function () {
                //    if (jQuery.isFunction(opciones.BeforeEditHandler)) {
                //        opciones.BeforeEditHandler(grid, rowKey);
                //        grid.editRow(rowKey, true, true, true, null, null,
                //           function (rowID, responseServer) {
                //          AfterSaveRowInline(rowID, grid);
                //           });
                //        lasRowKey = rowKey;
                //    }               
                //},
                beforeSaveCell: function (rowid, celname, value, iRow, iCol) {
                    if (opciones.BeforeEditHandler != null)
                        opciones.BeforeEditHandler(rowid, cellname, value, iRow, iCol);
                },
                afterSaveCell: function (rowid, cellname, value, iRow, iCol) {
                    if (opciones.AfterSaveCellHandler != null)
                        opciones.AfterSaveCellHandler(rowid, cellname, value, iRow, iCol);
                },

                onSelectRow: function () {
                    var rowKey = grid.getGridParam('selrow');
                    //alert(rowKey);
                    if (rowKey != null) {
                        $("#" + identificador).val(rowKey);
                    }
                    if (opciones.selectRowFunc != null) {
                        if (typeof (opciones.selectRowFunc) == 'function') {
                            opciones.selectRowFunc(rowKey)
                        }
                    }
                }


            }).navGrid("#" + pager, { edit: false, add: false, del: false, search: opciones.search, refresh: false },
                {}, // use default settings for edit
                {}, // use default settings for add
                {}, // delete instead that del:false we need this
                {
                    multipleSearch: true,

                    beforeShowSearch: function () {
                        $(".ui-reset").trigger("click");
                        return true;
                    }

                }
            );

        } // fin de GridLocal

        if (opciones.eliminar) {
            $('#' + grilla).navButtonAdd('#' + pager, {
                caption: opciones.btnEliminar != null ? opciones.btnEliminar.Caption : opciones.EliminarCaption,
                title: opciones.btnEliminar != null ? opciones.btnEliminar.Caption : opciones.EliminarCaption,
                buttonicon: 'ui-icon-trash',
                position: 'first',
                onClickButton: function () {
                    if (rowKey != null) {
                        if (opciones.btnEliminar == null) {
                            SICA.confirm(opciones.Mensaje, function () { Eliminar(); }, '', opciones.dialogDelete, opciones.Lenguaje);
                        }
                        else {
                            if (opciones.btnEliminar.Mensaje != null && opciones.btnEliminar.Mensaje != '') {
                                SICA.confirm(opciones.btnEliminar.Mensaje, function () { opciones.btnEliminar.Function(rowKey); }, '', opciones.btnEliminar.titleMensaje, opciones.Lenguaje);
                            }
                            else {
                                opciones.btnEliminar.Function(rowKey);
                            }
                        }
                    } else {
                        if (opciones.Alert == null) {
                            SICA.Alert(opciones.dialogAlert, opciones.Mensaje, '', opciones.Lenguaje);
                        }
                        else {
                            SICA.Alert(opciones.Alert.titleMensaje, opciones.Alert.Mensaje, '', opciones.Lenguaje);
                        }
                    }
                }
            });
        }

        if (opciones.editar) {
            $('#' + grilla).navButtonAdd('#' + pager, {
                caption: opciones.EditarCaption,
                title: opciones.EditarCaption,
                buttonicon: 'ui-icon-pencil',
                position: 'first',
                onClickButton: function () {
                    if (rowKey != null) {
                        if (opciones.btnEditar == null) {
                            Editar(rowKey);
                        }
                        else {
                            opciones.btnEditar.Function(rowKey);
                        }
                    } else {
                        if (opciones.Alert == null) {
                            SICA.Alert(opciones.dialogAlert, opciones.Mensaje, '', opciones.Lenguaje);
                        }
                        else {
                            SICA.Alert(opciones.Alert.titleMensaje, opciones.Alert.Mensaje, '', opciones.Lenguaje);
                        }
                    }
                }
            });
        }

        if (opciones.nuevo) {
            $('#' + grilla).navButtonAdd('#' + pager, {
                caption: opciones.NuevoCaption,
                title: opciones.NuevoCaption,
                buttonicon: 'ui-icon-plus',
                position: 'first',
                onClickButton: function () {
                    if (opciones.btnNuevo == null) {
                        Nuevo(rowKey);
                    }
                    else {
                        opciones.btnNuevo.Function(rowKey);
                    }
                }
            });
        }

        if (opciones.leyenda) {
            $('#' + grilla).navButtonAdd('#' + pager, {
                caption: opciones.LeyendaCaption,

                title: opciones.LeyendaCaption,
                buttonicon: 'ui-icon-search',
                position: 'first',
                id: "L" + grilla,
                onClickButton: function () {
                    opciones.verLeyenda();
                    //if (opciones.btnNuevo == null) {
                    //    Nuevo(rowKey);
                    //}
                    //else {
                    //    opciones.btnNuevo.Function(rowKey);
                    //}
                }
            });
        }
        var funccion = 'SICA.LayoutBarra("' + pager.toString() + '")';
        setTimeout(funccion, 1000);
    },
    LayoutBarra: function (pager) {
        pager = pager != null ? pager.toString() : '';
        // layout barra de la tabla
        if (pager != null && pager != '') {
            jQuery("#" + pager + "_left").removeAttr('style');

            var wb = jQuery("#pg_" + pager).children().width()
            var wl = jQuery("#" + pager + "_left").width();
            var wlc = jQuery("#" + pager + "_left").children().width();
            var wc = jQuery("#" + pager + "_center").width();
            var wcc = jQuery("#" + pager + "_center").children().width();
            var wr = jQuery("#" + pager + "_right").width();
            var wrc = jQuery("#" + pager + "_right").children().width();

            // agregar span con el mismo texto para obtener el width
            var tr = jQuery("#" + pager + "_right").children().html();
            var trs = "<span>" + tr + "</span>";
            jQuery("#" + pager + "_right").children().html(trs);

            // width solo texto numero de pagina
            var ws = jQuery("#" + pager + "_right").children().children().width();

            //  width left
            if (wl < wlc) {
                //jQuery("#" + pager + "_left").attr('style', 'width:' + (wlc + 20) + 'px');
            }

            //  width center
            if (wb < (wlc + wcc + wrc)) {
                jQuery("#" + pager + "_center").attr('style', 'width:' + (wcc) + 'px');
            }

            // right texto --> reducido si no soporta el ancho de la barra
            if (wb < (wlc + wcc + ws)) {
                var list = tr.split(' ');
                var ntr = list[3] + ' ' + list[4] + ' ' + list[5];
                jQuery("#" + pager + "_right").children().children().html(ntr);
            }

            // right texto --> si el ancho de la barra aun no soporta el texto reducido se blanquea
            ws = jQuery("#" + pager + "_right").children().children().width();
            if (wb < (wlc + wcc + ws)) {
                jQuery("#" + pager + "_right").children().children().html("");
            }
        }
    },
    IniPopUp: function (main, mainTitle, alerta, msgdelete, mainHeight, mainWidth) {
        $("#" + main).dialog({
            autoOpen: false,
            resizable: false,
            title: mainTitle,
            height: mainHeight,
            width: mainWidth,
            modal: true,
            open: function () {
                $(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide();
                $(this).parent().appendTo($('#aspnetForm'));
            }
        });

        $("#" + alerta).dialog({
            autoOpen: false,
            resizable: false,
            height: 100,
            width: 280,
            title: "Alert",
            modal: true
        });

        $("#" + msgdelete).dialog({
            autoOpen: false,
            resizable: false,
            title: "Eliminar",
            height: "150",
            width: "380",
            modal: true,
            buttons: [
                    {
                        text: "Eliminar",
                        click: function () {
                            Eliminar();
                        }
                    },
                    {
                        text: "Cancelar",
                        click: function () {
                            $(this).dialog("close");
                        }
                    }
            ]
        });
    },
    LoadDropDownList: function (name, url, parameters, selected, isValIndex, async) {
        var combo = document.getElementById(name);
        combo.options.length = 0;
        combo.options[0] = new Option("");
        combo.selectedIndex = 0;

        $('#' + name).ajaxError(function (event, request, settings) {
            combo.options[0] = new Option("Error al cargar.");
        });
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: async,
            data: JSON.stringify(parameters),
            success: function (items) {
                $.each(items, function (index, item) {
                    combo.options[index] = new Option(item.Nombre, item.Codigo);
                });
                if (selected == undefined) selected = 0;

                if (isValIndex) {
                    $('#' + name).attr('selectedIndex', selected);
                } else {
                    $('#' + name).val(selected);
                }
            },
            failure: function (msg) {
            },
            error: function (xhr, status, error) {
            }
        });
    },
    LoadDropDownListMulti: function (name, url, parameters, selected, async) {
        var combo = document.getElementById(name);

        $('#' + name).ajaxError(function (event, request, settings) {
            combo.options[0] = new Option("Error al cargar.");
        });
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: async,
            data: JSON.stringify(parameters),
            success: function (items) {
                var list = items;
                $.each(list, function (index, item) {
                    combo.options[index] = new Option(item.Nombre, item.Codigo);
                });
                if (selected == undefined) selected = 0;
                $('#' + name).val(selected);
            },
            failure: function (msg) {
            },
            error: function (xhr, status, error) {
            }
        });
    },
    LoadDropDownListSinFormato: function (name, url, parameters, selected, async) {
        var combo = document.getElementById(name);
        combo.options.length = 0;
        combo.options[0] = new Option("");
        combo.selectedIndex = 0;

        //        $('#' + name).ajaxError(function (event, request, settings) {
        //            combo.options[0] = new Option("Error al cargar.");
        //        });
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: async,
            data: JSON.stringify(parameters),
            success: function (items) {
                var list = items;
                $.each(list, function (index, item) {
                    combo.options[index] = new Option(item.Nombre, item.Codigo);
                });
                if (selected == undefined) selected = 0;
                $('#' + name).val(selected);
            },
            failure: function (msg) {
            },
            error: function (xhr, status, error) {
            }
        });
    },
    Clear: function (divName) {
        $('#' + divName + ' select').children().remove();
        var elemt = $('#' + divName);
        $(':input', elemt).each(function () {
            var type = this.type;
            var tag = this.tagName.toLowerCase();
            if (type == 'text' || type == 'password' || tag == 'textarea')
                this.value = "";
            else if (type == 'checkbox' || type == 'radio')
                this.checked = true;
            else if (tag == 'select')
                this.selectedIndex = -1;
        });
    },
    Operacion: function (url) {
        $.ajax({
            url: url,
            dataType: 'html',
            success: function (result) {
                $('#listado').hide();
                $('#informacion').show();
                $('#informacion').html(result);
            },
            error: function (request, status, error) {
                $('#listado').hide();
                $('#informacion').show();
                alert(request.responseText);
            }
        });
    },
    LoadDropDownListItems: function (name, url, parameters, selected) {
        var combo = document.getElementById(name);
        combo.options.length = 0;
        combo.options[0] = new Option("");
        combo.selectedIndex = 0;
        combo.disabled = true;
        var items = "";
        var resultado = SICA.Ajax(url, parameters, false);
        resultado = resultado.items == undefined ? resultado : resultado.items;
        if (resultado != null || resultado != undefined) {
            $.each(resultado, function (index, item) {
                if (item != null)
                    combo.options[index] = new Option(item.Nombre, item.Codigo);
            });
        }
        else {
            items += '<option value = "0">--Sin Datos--</option>';
            $('#' + name).html(items);
        }
        combo.disabled = false;
        if (selected == undefined) selected = 0;
        $('#' + name).val(selected);
    },


    LoadTextBoxFor: function (name, url, parameters, readonly, async) {
        var textBox = document.getElementById(name);
        textBox.value = "";

        $('#' + name).ajaxError(function (event, request, settings) {
            textBox.value = "Error al cargar.";
        });
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: async,
            data: JSON.stringify(parameters),
            success: function (items) {
                textBox.value = items;
                if (readonly == undefined) var selected = 0;
                else {
                    textBox.readonly = readonly
                }
            },
            failure: function (msg) {
            },
            error: function (xhr, status, error) {
            }
        });
    },
    ActualizarPaginadorActual: function (nombreGrilla, accion) {
        if (nombreGrilla == '' || nombreGrilla == undefined || nombreGrilla == null || nombreGrilla == NaN) return;
        var paginaSeleccionada = $('#' + nombreGrilla).getGridParam('page');
        var registroPagina = jQuery("#" + nombreGrilla).getGridParam('rowNum');
        var numeroRegistros = jQuery("#" + nombreGrilla).getGridParam('records');
        var totalPaginas = parseInt(numeroRegistros / registroPagina);
        var paginaActual = "";
        if (numeroRegistros > totalPaginas * registroPagina) {
            paginaActual = totalPaginas + 1;
        } else {
            paginaActual = totalPaginas;
        }
        if (accion == "add") {
            $("#" + nombreGrilla).trigger("reloadGrid", [{ page: paginaActual }]);
        }

        if (accion == "del") {
            if (paginaSeleccionada > paginaActual) {
                $("#" + nombreGrilla).trigger("reloadGrid", [{ page: paginaActual }]);
            }
        }
    },
    BloquearControles: function (nameContenedor) {
        $('#' + nameContenedor).find('input, select, textarea').attr('disabled', true);
        $('#' + nameContenedor + '').find('input').datepicker('disable');
        $('#' + nameContenedor).parent().find("button").attr("disabled", true);
    },
    HabilitarControles: function (nameContenedor) {
        $('#' + nameContenedor + '').find('input, select, textarea').removeAttr('disabled');
        $('#' + nameContenedor + '').find('input').datepicker('enable');
        $('#' + nameContenedor).parent().find("button").removeAttr("disabled");
    },

    BloquearControlesTodos: function (contenedor) {
        jQuery('#' + contenedor).find('input, select, textarea').attr('disabled', true);
        jQuery('#' + contenedor + '').find('input').datepicker('disable');
        jQuery('#' + contenedor).find('input, select, textarea').addClass(".ui-widget-content");
        //jQuery('#' + contenedor).find('input[type="button"], input[type="file"]').hide();
        //jQuery('#' + contenedor).find('input:checkbox').attr('disabled', false);
        jQuery('#' + contenedor).find('input:radio').attr('disabled', true);
        jQuery('#' + contenedor).parent().find("button").attr("disabled", true);
        jQuery('#dialog_btnAceptar').hide();
        jQuery('#dialog_btnCancelar').hide();
        jQuery('#dialog_btnGuardar').hide();
        // buscar por clases
        jQuery('#' + contenedor).find('#btnEliminar,#btnQuitar, #btnRegistrar, #btnAgregar').hide();
        jQuery('#btnCancelar').removeAttr('disabled');
    },

    HabilitarControlesTodos: function (contenedor) {
        jQuery('#' + contenedor).find('input, select, textarea').removeAttr('disabled');
        jQuery('#' + contenedor + '').find('input').datepicker('enable');
        jQuery('#' + contenedor).find('input, select, textarea').removeClass("colorFormulario");
        //jQuery('#' + contenedor).find('input[type="button"], input[type="file"]').hide();
        jQuery('#' + contenedor).find('input:checkbox').removeAttr('disabled');
        jQuery('#' + contenedor).find('input:radio').removeAttr('disabled');
        jQuery('#' + contenedor).parent().find("button").removeAttr('disabled');
        jQuery('#dialog_btnAceptar').show();
        jQuery('#dialog_btnCancelar').show();
        // buscar por clases
        jQuery('#' + contenedor).find('#btnEliminar,#btnQuitar, #btnRegistrar, #btnAgregar').show();
    },
    DescargarArchivo: function (obj, url, parametros) {
        if (url == undefined || url == null) return;
        var frm = '<form id = "frmDescarga" name = "frmDescarga" method = "POST" target = "_blank" action = "' + url + '"></form>';
        var hdn = '<input type = "hidden" id = "url" name = "url" />';
        var hdnFormato = '<input type = "hidden" id = "formato" name = "formato" />';
        var hdnItem = '<input type = "hidden" id = "item" name = "item" />';
        jQuery(obj).append(frm)
        jQuery(hdn).appendTo(jQuery('#frmDescarga'));
        jQuery(hdnFormato).appendTo(jQuery('#frmDescarga'));
        jQuery(hdnItem).appendTo(jQuery('#frmDescarga'));
        jQuery('#frmDescarga #url').val(parametros.Url);
        jQuery('#frmDescarga #formato').val(parametros.Formato);
        jQuery('#frmDescarga #item').val(parametros.Item);
        jQuery('#frmDescarga').submit();
        jQuery('#frmDescarga').remove();
    },

    CargarComboGrilla: function (url, parameters) {
        if (parameters == undefined || parameters == null || parameters == "") parameters = false;
        var cadena = "";
        var respuesta = SICA.Ajax(url, parameters, false);
        if (respuesta != null) {
            var items = respuesta.items;
            if (items == null || items == undefined) items = respuesta;
            if (items.length > 0) {
                for (var j = 0; j < items.length; j++) {
                    cadena = cadena + items[j].Codigo + ":" + items[j].Nombre + ";";
                }
                cadena = cadena.substring(0, cadena.length - 1);
            }
        }
        else {
            alert(respuesta.message);
        }
        return cadena;
    },
    CargarComboGrillaItems: function (items) {
        var cadena = "";
        if (items == undefined || items == null || items == "") return cadena;
        if (items.length > 0) {
            for (var j = 0; j < items.length; j++) {
                cadena = cadena + items[j].Codigo + ":" + items[j].Nombre + ";";
            }
            cadena = cadena.substring(0, cadena.length - 1);
        }
        return cadena;
    },

    crearTabs: function (contenedor) {
        $(contenedor).tabs({
            selected: 0,
            select: function (event, ui) {

            }
        });
        $(contenedor).unbind('tabsshow');
    },
    padLeft: function (nr, n, str) {
        return Array(n - String(nr).length + 1).join(str || '0') + nr;
    },

    confirm: function (dialogText, okFunc, cancelFunc, dialogTitle, idioma) {
        var ok = "Ok";
        var cancelar = "Cancel";
        if (idioma == 'es') {
            ok = "Aceptar";
            cancelar = "Cancelar";
        }
        $('<div style="padding: 10px; max-width: 500px; min-width: 300px; word-wrap: break-word;">' + dialogText + '</div>').dialog({
            //draggable: false,
            modal: true,
            resizable: false,
            width: 'auto',
            title: dialogTitle || 'Confirmación',
            minHeight: 75,
            buttons: [
                {
                    text: 'Aceptar',
                    id: 'dialog_btnAceptar',
                    click: function () {
                        if (typeof (okFunc) == 'function') { setTimeout(okFunc, 50); }
                        $(this).dialog('destroy');
                    }
                },

                {
                    text: 'Cancelar',
                    id: 'dialog_btnCancelar',
                    click: function () {
                        if (typeof (cancelFunc) == 'function') { setTimeout(cancelFunc, 50); }
                        $(this).dialog('destroy');
                    }
                }
            ]
        });
    },

    Alert: function (dialogTitle, dialogText, okFunc, idioma) {
        /*
        $('<div style="padding: 10px; min-width: 250px; word-wrap: break-word;">' + dialogText + '</div>').dialog({
            draggable: false,
            modal: true,
            resizable: false,
            width: '400px',
            title: dialogTitle || 'Alert',
            minHeight: 75,
            buttons:
             [
                {
                    text: 'Aceptar',
                    id: 'dialog_btnAceptar',
                    click: function () {
                        if (typeof (okFunc) == 'function') { setTimeout(okFunc, 50); }
                        $(this).dialog('destroy');
                    }
                }
             ]
        });
        */

        var oAlert = alert;
        var message = '';
        if (dialogText instanceof Object) {
            jQuery.each(dialogText, function (i, item) {
                message += "<div>";
                message += item + '<br />';
                message += "</div>";
            });
        }
        else {
            message = dialogText;
        }
        try {
            jAlert(message, dialogTitle);
        } catch (e) {
            oAlert(message);
        }
    },

    LimpiarValidacion: function (idFrm) {
        var frm = idFrm == null ? "" : (idFrm == "" ? "" : ("#" + idFrm + " "));
        $(frm + ".field-validation-error").html('');
        $(frm + ".field-validation-valid").html('');

        $(frm + "input").removeClass("input-validation-error");
        $(frm + "select").removeClass("input-validation-error");
        $(frm + "textarea").removeClass("input-validation-error");

        //$("input, select, textarea").removeClass("input-validation-error");

    },

    InicializarMsg: function () {
        $(".msgModel").show();
        SICA.msgHide();
        SICA.ShowElement(".msgModel .loading");
    },
    // rpt = bool (true: exito, false: error)
    //Message = mensaje a mostrar
    //funcionout = funcion a ejecutar ejm : 'Cancelar()' or ''
    msgConfirm: function (rpt, Message, funcionout) {
        Message = Message == "" ? " " : Message;
        if (rpt) {
            $(".msgModel").slideUp(200, function () {
                SICA.msgHide();
                $(".msgModel .mexito").html(Message);
                $(".msgModel .mexito").html("");
                SICA.ShowElement(".msgModel .mexito");
                if (typeof (funcionout) == 'function') {
                    setTimeout(funcionout, 200);
                }
            });
        } else {
            $(".msgModel").slideUp(200, function () {
                SICA.msgHide();
                $(".msgModel .mexito").html("");
                $(".msgModel .merror").html(Message);
                SICA.ShowElement(".msgModel .merror");
            });
        }
        $(".msgModel").show();
        // $("#mainContent").animate({ scrollTop: 0 }, 600);
        //   return false;
    },

    msgHide: function () {
        $(".msgModel .tips").hide();
        $(".msgModel .merror, .msgModel .mexito, .msgModel .loading").css({ "border-top": "none" });
        $("#frmModel .merror").css({ "border-top": "none", "border-left": "none", "border-right": "none" });
    },

    getDate: function (fecha) {
        var day = fecha.getDate(); day = day < 10 ? ('0' + day) : day;
        return day;
    },
    getMonth: function (fecha) {
        var month = fecha.getMonth() + 1; month = month < 10 ? ('0' + month) : month;
        return month;
    },
    ObtenerHora: function (hora) {
        jQuery.mask.definitions['H'] = '[012]';
        jQuery.mask.definitions['h'] = '[1023456789]';
        jQuery.mask.definitions['N'] = '[012345]';
        jQuery.mask.definitions['n'] = '[0123456789]';
        return jQuery("#" + hora).mask("Hh:Nn");
    },
    ValidaHora24: function (texto) {
        var cadena = texto.toString();
        if (cadena == null || cadena == "")
            return false;

        var Hora = new String(cadena);
        var er_fh = /^([0]?[0-9]|[1][0-9]|[2][0-3])\:([0-5]0|[0-5][1-9])$/;
        if (Hora == "") {
            return false;
        }
        if (!(er_fh.test(Hora))) {
            return false;
        }

        return true;
    },
    ValidaHora12: function (texto) {
        var cadena = texto.toString();
        if (cadena == null || cadena == "")
            return false;

        var Hora = new String(cadena);
        var er_fh = /^([0]?[1-9]|1[0-2])\:([0-5]0|[0-5][1-9])\s(am|pm|a.m.|p.m.|AM|PM|A.M.|P.M.)$/;
        if (Hora == "") {
            return false;
        }
        if (!(er_fh.test(Hora))) {
            return false;
        }

        return true;
    },

    NumericValidationKeypress: function (event) {
        if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
            (event.keyCode == 65 && event.ctrlKey === true) ||
                (event.keyCode >= 35 && event.keyCode <= 39)) {
            return;
        }
        else {
            if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                event.preventDefault();
            }
        }
    },
    validaCaracter: function (e, param) {
        var caracteres = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789";
        var teclasPermitidos = [32];
        var NoPermitir = false;
        var tecla = (document.all) ? e.keyCode : e.which;
        var caracter = String.fromCharCode(tecla);
        //alert(tecla);

        for (var i in teclasPermitidos) {
            if (tecla == teclasPermitidos[i]) {
                NoPermitir = true;
                break;
            }
        }
        return caracteres.indexOf(caracter) != -1 || NoPermitir;
    },

    validarSoloLetras: function (e) {
        var tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8) return true;
        var patron = /[A-Za-z\s]/;
        var te = String.fromCharCode(tecla);
        return patron.test(te);
    },

    validarSoloNumeros: function (e) {
        var tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8) return true;
        var patron = /[0-9]/;
        var te = String.fromCharCode(tecla);
        return patron.test(te);
    },

    validarSoloDecimales: function (e) {
        var tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8) return true;
        var patron = /([0-9]|[.])/;
        var te = String.fromCharCode(tecla);
        return patron.test(te);
    }
};

function Right(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else {
        var iLen = String(str).length;
        return String(str).substring(iLen, iLen - n);
    }
}

function InvocarVista(url, item) {
    $.ajax({
        url: url + "?parametro=" + JSON.stringify(item),
        dataType: 'html',
        type: 'post',
        contentType: 'text/xml; charset=utf-8',
        async: false,
        success: function (result) {
            //$('#divPrincipal').html("");
            $('#divProceso').html("");

            //$('#divRequerimiento').hide();
            //$('#divMantenimientoRequerimiento').show();
            $('#divProceso').html(result);

            //$.validator.unobtrusive.parse('#frmModel');

        },
        error: function (request, status, error) {
            $('#divProceso').show();
        }
    });
}


function jqGridResponsive(jqgrid) {
    jqgrid.find('.ui-jqgrid').addClass('clear-margin span12').css('width', '');
    jqgrid.find('.ui-jqgrid-view').addClass('clear-margin span12').css('width', '');
    jqgrid.find('.ui-jqgrid-view > div').eq(1).addClass('clear-margin span12').css('width', '').css('min-height', '0');
    jqgrid.find('.ui-jqgrid-view > div').eq(2).addClass('clear-margin span12').css('width', '').css('min-height', '0');
    jqgrid.find('.ui-jqgrid-sdiv').addClass('clear-margin span12').css('width', '');
    jqgrid.find('.ui-jqgrid-pager').addClass('clear-margin span12').css('width', '');
}

function openVentana(url, w, h, name) {
    var leftScreen = (screen.width - w) / 2;
    var topScreen = (screen.height - h) / 2;
    var opciones = "directories=no,menubar=no,scrollbars=yes,status=yes,resizable=yes,width=" + w + ",height=" + h + ",left=" + leftScreen + ",top=" + topScreen;
    ventana = window.open(url, name, opciones);
    //ventana.focus();
}

var SubGrid = {
    subGridRowExpandedAnexosHijos: function (subgrid_id, row_id, subGrid, subgridindex) {
        var url = baseUrl + 'Firma/DocumentoAnexo/DocumentoAnexo_Listar?ID_MODULO=16'; // + _ID_MODULO;

        var subgrid_table_id, pager_id;
        subgrid_table_id = subgrid_id + "_t";
        pager_id = "p_" + subgrid_table_id;

        $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll' style='margin-bottom:10px;margin-left: 15px;'></table><div id='" + pager_id + "' class='scroll'></div>");

        // var parameters = { cDocNro: row_id };
        //subgrid_table_id
        $("#" + subgrid_table_id).jqGrid({
            prmNames: {
                search: 'isSearch',
                nd: null,
                rows: 'rows',
                page: 'page',
                sort: 'sortField',
                order: 'sortOrder',
                filters: 'filters'
            },

            postData: { searchString: '', searchField: '', searchOper: '', filters: '' },
            jsonReader: {
                root: 'rows',
                page: 'page',
                total: 'total',
                records: 'records',
                cell: 'cell',
                id: 'ID_DOCUMENTO_PADRE', //index of the column with the PK in it
                userdata: 'userdata',
                repeatitems: true,
                scroll: true
            },
            multiboxonly: false,
            datatype: "local",
            colNames: subGrid.ColNames,
            colModel: subGrid.ColModels,
            rowNum: 10,
            rowList: [10, 20, 50, 100],
            sortorder: "desc",
            viewrecords: true,
            rownumbers: false,
            pager: "#" + pager_id,
            loadonce: true,
            sortable: true,
            height: '',
            width: '',
            autowidth: true,
            subGrid: false,
            multiselect: false,
            sortname: "ID_DOCUMENTO",
            //subGridRowExpanded: function (subgrid_id, row_id) { SubGrid.subGridRowExpandedAnexosHijos(subgrid_id, row_id, subGrid, 1) },
            onSelectRow: function (id, isSelected) { SubGrid.subGridonSelectRow(subgrid_table_id, "FrmDocumentoAnexoPadre", id) },
            // subGridRowColapsed: SubGrid.subGridRowColapsed(subgrid_id, row_id),
            datatype: function (postdata) {
                var migrilla = new Object();
                migrilla.page = postdata.page;
                migrilla.rows = postdata.rows;
                migrilla.sidx = postdata.sortField;
                migrilla.sord = postdata.sortOrder;
                migrilla._search = postdata.isSearch;
                migrilla.filters = postdata.filters;
                rules = [];
                var _gs_ID_DOCUMENTO_PADRE = row_id;
                rules.push({ field: 'UPPER(V.ID_DOCUMENTO_PADRE)', data: _gs_ID_DOCUMENTO_PADRE, op: " = " });
                migrilla.Rules = rules;

                if (migrilla._search == true) {
                    migrilla.searchField = postdata.searchField;
                    migrilla.searchOper = postdata.searchOper;
                    migrilla.searchString = postdata.searchString;
                }

                var params = { grid: migrilla };

                $.ajax({
                    url: url,
                    type: 'post',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(params),
                    async: false,
                    success: function (data, st) {
                        if (st == 'success') {
                            var jq = $('#' + subgrid_table_id)[0];
                            if (data.rows.length == 0) {
                                SubGrid.subGridRowColapsed(subgrid_id, row_id, subgridindex);
                            } else {
                                jq.addJSONData(data);
                                var grid = $("#" + subgrid_table_id);
                                var sd = grid[0].grid;
                                var sdsd = sd.hDiv;
                                sdsd.remove();
                                var barr = $("#p_" + subgrid_table_id);
                                barr[0].hidden = true;
                                var sadsa = document.getElementById(subgrid_table_id).getElementsByClassName('ui-widget-content subgrid-data');
                            }
                        }
                        $("#gbox_" + subgrid_table_id).css("border", "none");
                        //   + subgrid_table_id
                    },
                    error: function (a, b, c) {
                        alert('Error with AJAX callback');
                    }
                });
            },
        });
    },

    subGridRowColapsed: function (subgrid_id, row_id, subgridindex) {
        var subgrid_table_id, pager_id;
        var subgridSub_table_id = "";
        if (subgridindex == 0)
            subgridSub_table_id = subgrid_id;
        else
            subgridSub_table_id = subgrid_id;

        subgrid_table_id = subgrid_id + "_t";
        pager_id = "p_" + subgrid_table_id;
        jQuery("#" + subgrid_table_id).remove();
        jQuery("#" + subgridSub_table_id).remove();
        jQuery("#" + pager_id).remove();
    },

    subGridonSelectRow: function (subgrid, _frm, id) {
        DocumentoAnexoPadre_ID_DOCUMENTO = id;
        DocumentoAnexoPadre_GRILLA_SELECT = subgrid;
        var input_table = document.getElementById(_frm).getElementsByTagName('table');
        if (input_table.length > 0) {
            $.each(input_table, function (i, v) {
                if (v.id != "") {
                    if (v.id != subgrid)
                        jQuery("#" + v.id).jqGrid('resetSelection');
                }
            });
        }
    }
}