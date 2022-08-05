﻿
DataTable = {
    Grilla: function (grilla, urlListar, id, colModels, opciones, SortColunm) {
         
        var DataTable = null; 
        var grid = jQuery('#' + grilla);
        var estadoSubGrid = false;
        var typeSelect = "api";
        if (opciones.responsive == null) { responsive = false; }
        if (opciones.sort == null) { opciones.sort = 'desc'; }
        //if (opciones.PositionColumnSort == null) { opciones.PositionColumnSort = 0; }
        if (opciones.rowNumber == null) { opciones.rowNumber = 10; }
        if (opciones.rowNumbers == null) { opciones.rowNumbers = [opciones.rowNumber, 10, 25, 50]; }
        if (opciones.rules == null) { opciones.rules = false; }
        if (opciones.search == null) { opciones.search = false; }
        if (opciones.processing == null) { opciones.processing = false; }
        if (opciones.multiselect == null) { opciones.multiselect = false; }
        if (opciones.enumerable == null) { opciones.enumerable = false; }
        if (opciones.lengthChange == null) { opciones.lengthChange = true; }

        if (opciones.GridLocal == null) 
            opciones.GridLocal = false;
        var language = {
            "lengthMenu": "Mostrar _MENU_ registros",
            "zeroRecords": "No se encontró nada",
            "info": "Mostrando del _START_ al _END_ de un total de _TOTAL_",
            "infoEmpty": "No hay registros",
            "emptyTable": "No hay datos para mostrar",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Filtrar:",
            "searchPlaceholder": "Escriba para filtrar...",
            "infoFiltered": "(filtrado de un total de _MAX_ registros)",
            "paginate": {
                "first": "Primera",
                "last": "Última",
                "next": "<i class=\"bi bi-chevron-right\"></i>",
                "previous": "<i class=\"bi bi-chevron-left\"></i>"
            }
        };

       // checkbox select
        if (opciones.multiselect) {
            var MultiselectTh = {
                data: id,
                targets: 0,
                checkboxes: {
                    selectRow: true
                }
            }
            typeSelect = "multi";
            colModels.unshift(MultiselectTh);
        }

        // enumarar filas paginado
        if (opciones.enumerable ) {
            var enumerableTh = {
                data: "FILA",
                searchable: false,
                orderable: false,
                name: "NRO_FILA", 
                title: "#",
            }
            colModels.unshift(enumerableTh);
        }
        var rowKey;
        var PositionColumnSort =  colModels.findIndex(colModels => colModels.name === SortColunm);
        if(PositionColumnSort != -1){
            //var DataTable = null; 
            if (!opciones.GridLocal) {
                DataTable = grid.DataTable({
                    responsive: opciones.responsive,
                    processing: opciones.processing,
                    serverSide: true,      
                    autoWidth : false, 
                    lengthChange: opciones.lengthChange,
                    lengthMenu: opciones.rowNumbers,
                    pageLength: opciones.rowNumber,
                    order: [[PositionColumnSort, opciones.sort]],
                    searching: opciones.search,
                    rowId: id,
                    language: language,
                    select: {
                        style: typeSelect
                    },
                    columns: colModels,
                    ajax: {
                        type: "POST",
                        url: urlListar,
                        contentType: 'application/json; charset=utf-8',
                        dataType: "json",
                        data: function (dtParms) {
                            ObjectRules =  GetRules(grilla);
                            var migrilla = new Object();
                            migrilla.draw = dtParms.draw;
                            migrilla.rows = dtParms.length;
                            migrilla.start = dtParms.start;
                            migrilla.sidx = dtParms.columns[dtParms.order[0].column].name;
                            migrilla.sord =  dtParms.order[0].dir;
                            migrilla._search = opciones.search;
                            if (opciones.rules != false) {
                                migrilla.Rules = ObjectRules.rules;
                            }
                            if (migrilla._search == true) {
                                migrilla.SearchFields = ObjectRules.SearchFields;
                                migrilla.searchString = dtParms.search.value;
                            }
                        
                            var params = { grid: migrilla };
                            return JSON.stringify(params);
                        },
                        dataFilter: function (res) {
                            //recibimos del servidor
                            if (res != null && res != "") {
                                var parsed = JSON.parse(res);
                                return JSON.stringify(parsed);

                            }
                            else {
                                alert('Error with AJAX callback');
                            }
                        },
                        error: function (x, y) {
                            console.log(x);
                        }
                    },
                    filter: true,
               
    
                });

                //DataTable.on('order.dt search.dt', function () {
                //    let i = 1;
                //    DataTable.cells(null, 0, {search:'applied', order:'applied'}).every( function (cell) {
                //        this.data(i++);
                //    } );
                //} ).draw();
  
            } // fin de NO GridLocal
            else if (opciones.GridLocal) {
                 
                DataTable = grid.DataTable({
                    responsive: opciones.responsive,
                    processing: opciones.processing,
                    serverSide: false,
                    autoWidth : false, 
                    lengthChange: opciones.lengthChange,
                    lengthMenu: opciones.rowNumbers,
                    pageLength: opciones.rowNumber,
                    order: [[PositionColumnSort, opciones.sort]],
                    searching: opciones.search,
                    rowId: id,
                    language: language,
                    select: {
                        style: typeSelect
                    },
                    filter: false,
                    columns: colModels,
                });

            } // fin de GridLocal
        }else{
            alert('DataTable: sort column does not exist'); 
        }
        //return DataTable;
    },

    clearGridData: function (table) {
        $('#'+table).DataTable().clear().draw();
    },

    selarrrow: function (table) {
        var Ids = new Array();
        var rows_selected = $('#' + table).DataTable().column(0).checkboxes.selected();
        $.each(rows_selected, function (index, rowId) {
            Ids.push(rowId);
        });
        return Ids; 
    },

    getGridData: function (table) {
        var ArrayData = new Array();
        $('#' + table).DataTable().rows().data().each(function (value, index) {
            var rowData = {
                index: index,
                value: value
            }
            ArrayData.push(rowData); 
        });
        return ArrayData; 
    },

    addRowData: function (table, Item) {
        $('#' + table).DataTable().row.add(Item).draw();
    },
    GridUnload: function (table) {
        if ($.fn.DataTable.isDataTable('#' + table)) {
            $("#" + table).dataTable().fnDestroy();
            $("#" + table).html("");
        }
    }

}